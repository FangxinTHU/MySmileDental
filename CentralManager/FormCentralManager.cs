﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;
using OpenDental;
using CodeBase;
using System.Globalization;
using System.Threading;

namespace CentralManager {
	public partial class FormCentralManager:Form {
		public static byte[] EncryptionKey;
		///<summary>A full list of connections.</summary>
		private List<CentralConnection> _listConns;
		private List<ConnectionGroup> _listConnectionGroups;
		private string _progVersion;
		///<summary>A copy of the preference cache on load so that we don't waste time grabbing a deep copy of the cache which never updates.
		///As soon as the CEMT starts to process signals this can be removed and implemented differently.</summary>
		private Dictionary<string,Pref> _dictPrefs;

		public FormCentralManager() {
			InitializeComponent();
			UTF8Encoding enc=new UTF8Encoding();
			EncryptionKey=enc.GetBytes("mQlEGebnokhGFEFV");
		}

		private void FormCentralManager_Load(object sender,EventArgs e) {
			if(!GetConfigAndConnect()){
				return;
			}
			_dictPrefs=PrefC.GetDict();
			Version storedVersion=new Version(PrefC.GetString(PrefName.ProgramVersion,_dictPrefs));
			Version currentVersion=Assembly.GetAssembly(typeof(Db)).GetName().Version;
			if(storedVersion.CompareTo(currentVersion)!=0){
				MessageBox.Show(Lan.g(this,"Program version")+": "+currentVersion.ToString()+"\r\n"
					+Lan.g(this,"Database version")+": "+storedVersion.ToString()+"\r\n"
					+Lan.g(this,"Versions must match.  Please manually connect to the database through the main program in order to update the version."));
				Application.Exit();
				return;
			}
			FormCentralLogOn FormCLO=new FormCentralLogOn();
			if(FormCLO.ShowDialog()!=DialogResult.OK) {
				Application.Exit();
				return;
			}
			if(CultureInfo.CurrentCulture.Name=="en-US") {
				CultureInfo cInfo=(CultureInfo)CultureInfo.CurrentCulture.Clone();
				cInfo.DateTimeFormat.ShortDatePattern="MM/dd/yyyy";
				Application.CurrentCulture=cInfo;
			}
			this.Text+=" - "+Security.CurUser.UserName;
			FillComboGroups(PrefC.GetLong(PrefName.ConnGroupCEMT,_dictPrefs));
			_listConns=CentralConnections.GetConnections();
			_progVersion=PrefC.GetString(PrefName.ProgramVersion,_dictPrefs);
			labelVersion.Text="Version: "+_progVersion;
			FillGrid();
		}

		///<summary>Gets the settings from the config file and attempts to connect.</summary>
		private bool GetConfigAndConnect(){
			string xmlPath=Path.Combine(Application.StartupPath,"CentralManagerConfig.xml");
			if(!File.Exists(xmlPath)){
				MessageBox.Show("Please create CentralManagerConfig.xml according to the manual before using this tool.");
				Application.Exit();
				return false;
			}
			XmlDocument document=new XmlDocument();
			string computerName="";
			string database="";
			string user="";
			string password="";
			try{
				document.Load(xmlPath);
				XPathNavigator Navigator=document.CreateNavigator();
				XPathNavigator nav;
				DataConnection.DBtype=DatabaseType.MySql;	
				//See if there's a DatabaseConnection
				nav=Navigator.SelectSingleNode("//DatabaseConnection");
				if(nav==null) {
					MessageBox.Show("DatabaseConnection element missing from CentralManagerConfig.xml.");
					Application.Exit();
					return false;
				}
				computerName=nav.SelectSingleNode("ComputerName").Value;
				database=nav.SelectSingleNode("Database").Value;
				user=nav.SelectSingleNode("User").Value;
				password=nav.SelectSingleNode("Password").Value;
			}
			catch(Exception ex) {
				//Common error: root element is missing
				MessageBox.Show(ex.Message);
				Application.Exit();
				return false;
			}
			DataConnection.DBtype=DatabaseType.MySql;
			OpenDentBusiness.DataConnection dcon=new OpenDentBusiness.DataConnection();
			//Try to connect to the database directly
			try {
				dcon.SetDb(computerName,database,user,password,"","",DataConnection.DBtype);
				RemotingClient.RemotingRole=RemotingRole.ClientDirect;
				return true;
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);
				Application.Exit();
				return false;
			}
		}

		///<summary>Refreshes _listConnectionGroups with the current cache (could have been updated) and then selects the conn group passed in.</summary>
		private void FillComboGroups(long connGroupNum) {
			_listConnectionGroups=ConnectionGroups.GetListt();
			comboConnectionGroups.Items.Clear();
			comboConnectionGroups.Items.Add("All");
			comboConnectionGroups.SelectedIndex=0;
			for(int i=0;i<_listConnectionGroups.Count;i++) {
				comboConnectionGroups.Items.Add(_listConnectionGroups[i].Description);
				if(_listConnectionGroups[i].ConnectionGroupNum==connGroupNum) {
					comboConnectionGroups.SelectedIndex=i+1;//0 is "All"
				}
			}
		}

		private void FillGrid() {
			FillGrid(null);
		}

		///<summary>connNums is a list of CentralConnectionNums which is used primarily for filtering out connections through the Refresh button press.</summary>
		private void FillGrid(List<long> connNums){
			List<CentralConnection> listConnsFiltered=null;
			if(comboConnectionGroups.SelectedIndex>0) {
				listConnsFiltered=CentralConnections.FilterConnections(_listConns,textConnSearch.Text,_listConnectionGroups[comboConnectionGroups.SelectedIndex-1]);
			}
			else {
				listConnsFiltered=CentralConnections.FilterConnections(_listConns,textConnSearch.Text,null);
			}
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn("#",40);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Database",320);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Note",300);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Status",100,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			foreach(CentralConnection conn in listConnsFiltered) {
				if(connNums!=null && !connNums.Contains(conn.CentralConnectionNum)) {
					continue; //We only want certain connections showing up in the grid.
				}
				string status=conn.ConnectionStatus;
				row=new ODGridRow();
				row.Cells.Add(conn.ItemOrder.ToString());
				if(conn.DatabaseName=="") {//uri
					row.Cells.Add(conn.ServiceURI);
				}
				else {
					row.Cells.Add(conn.ServerName+", "+conn.DatabaseName);
				}
				row.Cells.Add(conn.Note);
				if(status=="") {
					row.Cells.Add("Not checked");
					row.Cells[3].ColorText=Color.DarkGoldenrod;
				}
				else if(status=="OK") {
					row.Cells.Add(status);
					row.Cells[3].ColorText=Color.Green;
					row.Cells[3].Bold=YN.Yes;
				}
				else if(status=="OFFLINE") {
					row.Cells.Add(status);
					row.Bold=true;
					row.ColorText=Color.Red;
				}
				else {
					row.Cells.Add(status);
					row.Cells[3].ColorText=Color.Red;
				}
				row.Tag=conn;
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			CentralConnection conn=(CentralConnection)gridMain.Rows[e.Row].Tag;
			if(conn.ConnectionStatus=="OFFLINE") {
				MsgBox.Show(this,"Server Offline.  Fix connection and check status again to connect.");
				return;
			}
			if(conn.ConnectionStatus!="OK") {
				MsgBox.Show(this,"Version mismatch.  Either update your program or update the remote server's program and check status again to connect.");
				return;
			}
			string args="";
			if(conn.DatabaseName!="") {
				//ServerName=localhost DatabaseName=opendental MySqlUser=root MySqlPassword=
				args+="ServerName=\""+conn.ServerName+"\" "
					+"DatabaseName=\""+conn.DatabaseName+"\" "
					+"MySqlUser=\""+conn.MySqlUser+"\" ";
				if(conn.MySqlPassword!="") {
					args+="MySqlPassword=\""+CentralConnections.Decrypt(conn.MySqlPassword,EncryptionKey)+"\" ";
				}
			}
			else if(conn.ServiceURI!="") {
				args+="WebServiceUri=\""+conn.ServiceURI+"\" ";//Command line args expects WebServiceUri explicitly. Case sensitive.
				if(conn.WebServiceIsEcw){
					args+="WebServiceIsEcw=True ";
				}
			}
			else {
				MessageBox.Show("Either a database or a Middle Tier URI must be specified in the connection.");
				return;
			}
      if(checkAutoLog.Checked) {
			  args+="UserName=\""+Security.CurUser.UserName+"\" ";
			  args+="OdPassword=\""+Security.PasswordTyped+"\" ";
      }
			#if DEBUG
				Process.Start("C:\\Development\\OPEN DENTAL SUBVERSION\\head\\OpenDental\\bin\\Debug\\OpenDental.exe",args);
			#else
				Process.Start("OpenDental.exe",args);
			#endif
		}
		
		private void comboConnectionGroups_SelectionChangeCommitted(object sender,EventArgs e) {
			FillGrid();
		}

		#region Menu Setup

		private void menuConnSetup_Click(object sender,EventArgs e) {
			FormCentralConnections FormCC=new FormCentralConnections();
			foreach(CentralConnection conn in _listConns) {
				FormCC.ListConns.Add(conn.Copy());
			}
			FormCC.LabelText.Text=Lans.g("FormCentralConnections","Double click an existing connection to edit or click the 'Add' button to add a new connection.");
			FormCC.Text=Lans.g("FormCentralConnections","Connection Setup");
			if(FormCC.ShowDialog()==DialogResult.OK) {
				_listConns=CentralConnections.GetConnections();
				FillGrid();
			}
		}

		private void menuGroups_Click(object sender,EventArgs e) {
			ConnectionGroup connGroupCur=null;
			if(comboConnectionGroups.SelectedIndex>0) {
				connGroupCur=_listConnectionGroups[comboConnectionGroups.SelectedIndex-1];
			}
			FormCentralConnectionGroups FormCCG=new FormCentralConnectionGroups();
			FormCCG.ShowDialog();
			ConnectionGroups.RefreshCache();
			FillComboGroups(connGroupCur==null ? 0 : connGroupCur.ConnectionGroupNum);//Reselect the connection group that the user had before.
			FillGrid();
		}

		private void menuItemSecurity_Click(object sender,EventArgs e) {
			FormCentralSecurity FormCUS=new FormCentralSecurity();
			foreach(CentralConnection conn in _listConns) {
				FormCUS.ListConns.Add(conn.Copy());
			}
			FormCUS.ShowDialog();
			_listConns=CentralConnections.GetConnections();//List may have changed when syncing security settings.
			FillGrid();
		}

		#endregion

		#region Menu Reports

		private void menuProdInc_Click(object sender,EventArgs e) {
			List<CentralConnection> listSelectedConn=new List<CentralConnection>();
			for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
				listSelectedConn.Add((CentralConnection)gridMain.Rows[gridMain.SelectedIndices[i]].Tag);//The tag of this grid is the CentralConnection object
			}
			if(listSelectedConn.Count==0) {
				MsgBox.Show(this,"Please select at least one connection to run this report against.");
				return;
			}
			FormCentralProdInc FormCPI=new FormCentralProdInc();
			FormCPI.ConnList=listSelectedConn;
			FormCPI.EncryptionKey=EncryptionKey;
			FormCPI.ShowDialog();
			GetConfigAndConnect();//Set the connection settings back to the central manager db.
		}

		#endregion

		private void menuItemLogoff_Click(object sender,EventArgs e) {
			FormCentralLogOn FormCLO=new FormCentralLogOn();
			if(FormCLO.ShowDialog()!=DialogResult.OK) {
				Application.Exit();
				return;
			}
			this.Text="Central Manager - "+Security.CurUser.UserName;
		}
		
		private void menuItemPassword_Click(object sender,EventArgs e) {
			FormCentralUserPasswordEdit FormCPE=new FormCentralUserPasswordEdit(false,Security.CurUser.UserName);
			if(FormCPE.ShowDialog()==DialogResult.Cancel){
				return;
			}
			Security.CurUser.Password=FormCPE.HashedResult;
			Userods.Update(Security.CurUser);
		}

		private void butAdd_Click(object sender,EventArgs e) {
			CentralConnection conn=new CentralConnection();
			conn.IsNew=true;
			FormCentralConnectionEdit FormCCS=new FormCentralConnectionEdit();
			FormCCS.CentralConnectionCur=conn;
			if(FormCCS.ShowDialog()==DialogResult.OK) {//Will insert conn on OK.
				_listConns=CentralConnections.GetConnections();
				FillGrid();
			}
		}

		private void butEdit_Click(object sender,EventArgs e) {
			if(gridMain.SelectedIndices.Length==0) {
				MsgBox.Show(this,"Please select a connection to edit first.");
				return;
			}
			FormCentralConnectionEdit FormCCE=new FormCentralConnectionEdit();
			FormCCE.CentralConnectionCur=(CentralConnection)gridMain.Rows[gridMain.SelectedIndices[0]].Tag;//No support for editing multiple.
			if(FormCCE.ShowDialog()==DialogResult.OK) {
				_listConns=CentralConnections.GetConnections();
				FillGrid();
			}
		}

		private void butRefreshConns_Click(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
			if(gridMain.SelectedIndices.Length==0) {
				gridMain.SetSelected(true);
			}
			for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
				CentralConnection conn=(CentralConnection)gridMain.Rows[gridMain.SelectedIndices[i]].Tag;
				if(conn.DatabaseName=="" && conn.ServerName=="" && conn.ServiceURI=="") {
					continue;
				}
				ODThread odThread=new ODThread(ConnectAndVerify,new object[] { conn,_progVersion });
				odThread.Name="VerifyThread"+i;
				odThread.GroupName="Verify";
				odThread.Start();
			}
			ODThread.JoinThreadsByGroupName(Timeout.Infinite,"Verify");
			List<ODThread> listComplThreads=ODThread.GetThreadsByGroupName("Verify");
			for(int i=0;i<listComplThreads.Count;i++) {
				object[] obj=(object[])listComplThreads[i].Tag;
				CentralConnection conn=((CentralConnection)obj[0]);
				string status=((string)obj[1]);
				CentralConnection connection=_listConns.Find(x => x.CentralConnectionNum==conn.CentralConnectionNum);
				connection.ConnectionStatus=status;
			}
			ODThread.QuitSyncThreadsByGroupName(100,"Verify");
			Cursor=Cursors.Default;
			FillGrid();
		}

		private void ConnectAndVerify(ODThread odThread) {
			CentralConnection connection=(CentralConnection)odThread.Parameters[0];
			try {
				string progVersion=(string)odThread.Parameters[1];
				if(!CentralConnectionHelper.UpdateCentralConnection(connection,false)) {
					odThread.Tag=new object[] { connection,"OFFLINE" };//Can't connect
					return;
				}
				string progVersionRemote=PrefC.GetStringNoCache(PrefName.ProgramVersion);
				string err="";
				if(progVersionRemote!=progVersion) {
					err=progVersionRemote;
				}
				else {
					err="OK";
				}
				odThread.Tag=new object[] { connection,err };
			}
			catch(Exception ex) {
				odThread.Tag=new object[] { connection,"OFFLINE" };//Can't connect
			}
		}

		private void butRefresh_Click(object sender,EventArgs e) {
			if(textProviderSearch.Text=="" && textClinicSearch.Text=="") {
				FillGrid();
				return;
			}
			for(int i=0;i<gridMain.Rows.Count;i++) {
				CentralConnection conn=(CentralConnection)gridMain.Rows[i].Tag;
				if(conn.ConnectionStatus!="OK") {
					continue;
				}
				ODThread odThread=new ODThread(ConnectAndSearch,new object[] { conn });
				odThread.Name="SearchThread"+i;
				odThread.GroupName="Search";
				odThread.Start();
			}
			ODThread.JoinThreadsByGroupName(Timeout.Infinite,"Search");
			List<ODThread> listComplThreads=ODThread.GetThreadsByGroupName("Search");
			List<long> connNums=new List<long>();
			for(int i=0;i<listComplThreads.Count;i++) {
				object[] obj=(object[])listComplThreads[i].Tag;
				CentralConnection conn=((CentralConnection)obj[0]);
				bool result=((bool)obj[1]);
				if(result) {
					connNums.Add(conn.CentralConnectionNum);
				}
				listComplThreads[i].QuitAsync();
			}
			FillGrid(connNums);
		}

		private void ConnectAndSearch(ODThread odThread) {
			CentralConnection connection=(CentralConnection)odThread.Parameters[0];
			if(!CentralConnectionHelper.UpdateCentralConnection(connection,false)) {//No updating the cache since we're going to be connecting to multiple remote servers at the same time.
				odThread.Tag=new object[] { connection,false };//Can't connect, just return false to not include the connection.
				connection.ConnectionStatus="OFFLINE";
				return;
			}
			List<Provider> listProvs=Providers.GetProvsNoCache();
			//If clinic and provider are both entered is it good enough to find one match among the two?  I'm going to assume yes for now, and maybe later
			//if we decide that if both boxes have something entered that both entries need to be in the db we're searching I can change it.
			bool provMatch=false;
			for(int i=0;i<listProvs.Count;i++) {
				if(textProviderSearch.Text=="") {
					provMatch=true;
					break;
				}
				if(listProvs[i].Abbr.ToLower().Contains(textProviderSearch.Text.ToLower())
					|| listProvs[i].LName.ToLower().Contains(textProviderSearch.Text.ToLower())
					|| listProvs[i].FName.ToLower().Contains(textProviderSearch.Text.ToLower())) 
				{
					provMatch=true;
					break;
				}
			}
			List<Clinic> listClinics=Clinics.GetClinicsNoCache();
			bool clinMatch=false;
			for(int i=0;i<listClinics.Count;i++) {
				if(textClinicSearch.Text=="") {
					clinMatch=true;
					break;
				}
				if(listClinics[i].Description.ToLower().Contains(textClinicSearch.Text.ToLower())) {
					clinMatch=true;
					break;
				}
			}
			if(clinMatch && provMatch) {
				odThread.Tag=new object[] { connection,true };//Match found
			}
			else {
				odThread.Tag=new object[] { connection,false };//No match found
			}
		}

		private void butSecurity_Click(object sender,EventArgs e) {
			if(gridMain.SelectedIndices.Length==0) {
				MsgBox.Show(this,"Please select at least one connection.");
				return;
			}
			List<CentralConnection> listSelectedConns=new List<CentralConnection>();
			for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
				if(((CentralConnection)gridMain.Rows[gridMain.SelectedIndices[i]].Tag).ConnectionStatus!="OK") {
					continue;
				}
				listSelectedConns.Add((CentralConnection)gridMain.Rows[gridMain.SelectedIndices[i]].Tag);
			}
			CentralSyncHelper.SyncAll(listSelectedConns);
			FillGrid();
		}

		private void butUsers_Click(object sender,EventArgs e) {
			if(gridMain.SelectedIndices.Length==0) {
				MsgBox.Show(this,"Please select at least one connection.");
				return;
			}
			List<CentralConnection> listSelectedConns=new List<CentralConnection>();
			for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
				if(((CentralConnection)gridMain.Rows[gridMain.SelectedIndices[i]].Tag).ConnectionStatus!="OK") {
					continue;
				}
				listSelectedConns.Add((CentralConnection)gridMain.Rows[gridMain.SelectedIndices[i]].Tag);
			}
			CentralSyncHelper.SyncUsers(listSelectedConns);
			FillGrid();
		}

		private void butLocks_Click(object sender,EventArgs e) {
			if(gridMain.SelectedIndices.Length==0) {
				MsgBox.Show(this,"Please select at least one connection.");
				return;
			}
			List<CentralConnection> listSelectedConns=new List<CentralConnection>();
			for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
				if(((CentralConnection)gridMain.Rows[gridMain.SelectedIndices[i]].Tag).ConnectionStatus!="OK") {
					continue;
				}
				listSelectedConns.Add((CentralConnection)gridMain.Rows[gridMain.SelectedIndices[i]].Tag);
			}
			CentralSyncHelper.SyncLocks(listSelectedConns);
			FillGrid();
		}

		private void butPtSearch_Click(object sender,EventArgs e) {
			if(gridMain.SelectedIndices.Length==0) {
				MsgBox.Show(this,"Please select at least one connection to search first.");
				return;
			}
			List<CentralConnection> listConns=new List<CentralConnection>();
			for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
				if(((CentralConnection)gridMain.Rows[gridMain.SelectedIndices[i]].Tag).ConnectionStatus!="OK") {
					continue;
				}
				listConns.Add((CentralConnection)gridMain.Rows[gridMain.SelectedIndices[i]].Tag);
			}
			FormCentralPatientSearch FormCPS=new FormCentralPatientSearch();
			FormCPS.ListConns=listConns;
			FormCPS.ShowDialog();
			FillGrid();
		}
		
		private void FormCentralManager_FormClosing(object sender,FormClosingEventArgs e) {
			ODThread.QuitSyncAllOdThreads();
		}

		
	}
}
