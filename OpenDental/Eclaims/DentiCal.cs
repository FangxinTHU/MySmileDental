using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;
using OpenDentBusiness;
using Tamir.SharpSsh.jsch;

namespace OpenDental.Eclaims {
	/// <summary>
	/// aka Denti-Cal.
	/// </summary>
	public class DentiCal {

		private static string remoteHost="ftp.delta.org";
		public static string ErrorMessage="";

		///<summary></summary>
		public DentiCal() {
		}

		///<summary>Returns true if the communications were successful, and false if they failed. Both sends and retrieves.</summary>
		public static bool Launch(Clearinghouse clearinghouseClin,int batchNum) { //called from FormClaimReports and Eclaims.cs. clinic-level clearinghouse passed in.
			//Before this function is called, the X12 file for the current batch has already been generated in
			//the clearinghouse export folder. The export folder will also contain batch files which have failed
			//to upload from previous attempts and we must attempt to upload these older batch files again if
			//there are any.
			//Step 1: Retrieve reports regarding the existing pending claim statuses.
			//Step 2: Send new claims in a new batch.
			bool success=true;
			//Connect to the Denti-Cal SFTP server.
			Session session=null;
			Channel channel=null;
			ChannelSftp ch=null;
			JSch jsch=new JSch();
			try {
				session=jsch.getSession(clearinghouseClin.LoginID,remoteHost);
				session.setPassword(clearinghouseClin.Password);
				Hashtable config=new Hashtable();
				config.Add("StrictHostKeyChecking","no");
				session.setConfig(config);
				session.connect();
				channel=session.openChannel("sftp");
				channel.connect();
				ch=(ChannelSftp)channel;
			}
			catch(Exception ex) {
				ErrorMessage=Lan.g("DentiCal","Connection Failed")+": "+ex.Message;
				return false;
			}
			try {
				string homeDir="/Home/"+clearinghouseClin.LoginID+"/";
				//At this point we are connected to the Denti-Cal SFTP server.
				if(batchNum==0) { //Retrieve reports.
					if(!Directory.Exists(clearinghouseClin.ResponsePath)) {
						throw new Exception("Clearinghouse response path is invalid.");
					}
					//Only retrieving reports so do not send new claims.
					string retrievePath=homeDir+"out/";
					Tamir.SharpSsh.java.util.Vector fileList=ch.ls(retrievePath);
					for(int i=0;i<fileList.Count;i++) {
						string listItem=fileList[i].ToString().Trim();
						if(listItem[0]=='d') {
							continue;//Skip directories and focus on files.
						}
						Match fileNameMatch=Regex.Match(listItem,".*\\s+(.*)$");
						string getFileName=fileNameMatch.Result("$1");
						string getFilePath=retrievePath+getFileName;
						string exportFilePath=CodeBase.ODFileUtils.CombinePaths(clearinghouseClin.ResponsePath,getFileName);
						Tamir.SharpSsh.java.io.InputStream fileStream=null;
						FileStream exportFileStream=null;
						try {
							fileStream=ch.get(getFilePath);
							exportFileStream=File.Open(exportFilePath,FileMode.Create,FileAccess.Write);//Creates or overwrites.
							byte[] dataBytes=new byte[4096];
							int numBytes=fileStream.Read(dataBytes,0,dataBytes.Length);
							while(numBytes>0) {
								exportFileStream.Write(dataBytes,0,numBytes);
								numBytes=fileStream.Read(dataBytes,0,dataBytes.Length);
							}
						}
						catch {
							success=false;
						}
						finally {
							if(exportFileStream!=null) {
								exportFileStream.Dispose();
							}
							if(fileStream!=null) {
								fileStream.Dispose();
							}
						}
						if(success) {
							//Removed the processed report from the Denti-Cal SFTP so it does not get processed again in the future.
							try {
								ch.rm(getFilePath);
							}
							catch {
							}
						}
					}
				}
				else { //Send batch of claims.
					if(!Directory.Exists(clearinghouseClin.ExportPath)) {
						throw new Exception("Clearinghouse export path is invalid.");
					}
					string[] files=Directory.GetFiles(clearinghouseClin.ExportPath);
					for(int i=0;i<files.Length;i++) {
						//First upload the batch file to a temporary file name. Denti-Cal does not process file names unless they start with the Login ID.
						//Uploading to a temporary file and then renaming the file allows us to avoid partial file uploads if there is connection loss.
						string tempRemoteFilePath=homeDir+"in/temp_"+Path.GetFileName(files[i]);
						ch.put(files[i],tempRemoteFilePath);
						//Denti-Cal requires the file name to start with the Login ID followed by a period and end with a .txt extension.
						//The middle part of the file name can be anything.
						string remoteFilePath=homeDir+"in/"+clearinghouseClin.LoginID+"."+Path.GetFileName(files[i]);
						ch.rename(tempRemoteFilePath,remoteFilePath);
						File.Delete(files[i]);//Remove the processed file.
					}
				}
			}
			catch(Exception ex) {
				success=false;
				ErrorMessage+=ex.Message;
			}
			finally {
				//Disconnect from the Denti-Cal SFTP server.
				channel.disconnect();
				ch.disconnect();
				session.disconnect();
			}
			return success;
		}

	}
}
