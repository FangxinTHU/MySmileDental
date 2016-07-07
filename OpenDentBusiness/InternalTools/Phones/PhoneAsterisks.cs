using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection;

namespace OpenDentBusiness {
	///<summary>This entire class is only used at Open Dental, Inc HQ.  So for that special environment, many things are hard-coded.</summary>
	public class PhoneAsterisks {
		///<summary>The asterisk server ip setting can change at any time via the phone tracking server application.</summary>
		public static string AsteriskServerIp {
			get {
				return PrefC.GetString(PrefName.AsteriskServerIp);
			}
		}

		public static void SetToDefaultRingGroups(int extension,long employeeNum) {
			//First, figure out what the defaults are for this employee
			AsteriskRingGroups ringGroups=PhoneEmpDefaults.GetRingGroup(employeeNum);
			/*if(ringGroup==AsteriskRingGroups.All) {
				SetToAllRingGroups(extension,employeeNum);
			}
			if(ringGroup==AsteriskRingGroups.None) {
				RemoveFromRingGroups(extension,employeeNum);
			}
			if(ringGroup==AsteriskRingGroups.Backup) {
				SetToBackupRingGroupOnly(extension,employeeNum);
			}*/
			SetRingGroups(extension,ringGroups);
		}

		public static void SetRingGroups(int extension,AsteriskRingGroups ringGroups) {
			#region Old Ring Group Code
			//The following code is the old way of updating our ring groups where we would have each workstation send a query to the asterisk db and then
			//insert a signal into the database that would notify the PhoneTrackingServer to send a 'reload' command to Asterisk.
			//We believe that this is causing call quality loss and is also causing the 'reload' command to get called too much which 
			//might be the reason that we have noticed more and more call events getting missed.  E.g. link and unlink (answer / hang up) events are missed.
			//Also, Nathan mentioned to me that the column in the db that holds the ring group extensions is hitting its limit.
			/***************************************************************************************************************
			DataConnection dcon=new DataConnection(AsteriskServerIp,"asterisk","opendental","secret",DatabaseType.MySql);
			string command="SELECT grpnum,grplist FROM ringgroups WHERE grpnum = '601' OR grpnum = '609'";
			DataTable table=null;
			try {
				table=dcon.GetTable(command);
			}
			catch {//if remotely connecting from home
				return;
			}
			string rawExtensions601="";
			string rawExtensions609="";
			string[] arrayExtensions601=new string[0];
			string[] arrayExtensions609=new string[0];		
			for(int i=0;i<table.Rows.Count;i++) {
				if(table.Rows[i]["grpnum"].ToString()=="601") {//there should always be exactly one
					rawExtensions601=table.Rows[i]["grplist"].ToString();
					arrayExtensions601=rawExtensions601.Split(new char[] { '-' },StringSplitOptions.RemoveEmptyEntries);
				}
				if(table.Rows[i]["grpnum"].ToString()=="609") {//there should always be exactly one
					rawExtensions609=table.Rows[i]["grplist"].ToString();
					arrayExtensions609=rawExtensions609.Split(new char[] { '-' },StringSplitOptions.RemoveEmptyEntries);
				}
			}
			List<string> listExtension601=new List<string>();
			bool isIn601=false;
			for(int i=0;i<arrayExtensions601.Length;i++){
				//we won't test to make sure each item is a pure number.
				listExtension601.Add(arrayExtensions601[i]);
				if(arrayExtensions601[i]==extension.ToString()) {
					isIn601=true;
				}
			}
			List<string> listExtension609=new List<string>();
			bool isIn609=false;
			for(int i=0;i<arrayExtensions609.Length;i++) {
				//we won't test to make sure each item is a pure number.
				listExtension609.Add(arrayExtensions609[i]);
				if(arrayExtensions609[i]==extension.ToString()) {
					isIn609=true;
				}
			}
			if(ringGroups==AsteriskRingGroups.All) {
				if(!isIn601) {
					AddToRingGroup("601",extension.ToString(),rawExtensions601);
				}
				if(!isIn609) {
					AddToRingGroup("609",extension.ToString(),rawExtensions609);
				}
			}
			if(ringGroups==AsteriskRingGroups.None) {
				if(isIn601) {
					RemoveFromRingGroup("601",extension.ToString(),listExtension601,rawExtensions601);
				}
				if(isIn609) {
					RemoveFromRingGroup("609",extension.ToString(),listExtension609,rawExtensions609);
				}
			}
			if(ringGroups==AsteriskRingGroups.Backup) {
				if(isIn601) {
					RemoveFromRingGroup("601",extension.ToString(),listExtension601,rawExtensions601);
				}
				if(!isIn609) {
					AddToRingGroup("609",extension.ToString(),rawExtensions609);
				}
			}
			**************************************************************************************************************/
			#endregion
			//Create a custom signalod so that the queue system (new way of doing ring groups) knows how to handle this extension.
			Signalod sig=new Signalod();
			sig.ITypes=((int)InvalidType.PhoneAsteriskReload).ToString();
			sig.DateViewing=DateTime.MinValue;
			sig.SigType=SignalType.Invalid;
			sig.TaskNum=0;
			sig.SigText="Ext: "+POut.Int(extension)+",RingGroup: "+Enum.GetName(typeof(AsteriskRingGroups),ringGroups);
			Signalods.Insert(sig);
		}

		///<summary>Deprecated</summary>
		private static void AddToRingGroup(string ringGroup,string extension,string rawExtensions) {
			string newExtensions=rawExtensions+"-"+extension;
			string command="UPDATE ringgroups SET grplist='"+POut.String(newExtensions)+"' "
				+"WHERE grpnum='"+ringGroup+"' "
				+"AND grplist = '"+POut.String(rawExtensions)+"'";//this ensures it hasn't changed since we checked it.  If it has, then this silently fails.
				//A transaction would be better, but no time.
			DataConnection dcon=new DataConnection(AsteriskServerIp,"asterisk","opendental","secret",DatabaseType.MySql);
			dcon.NonQ(command);
		}

		///<summary>Deprecated</summary>
		private static void RemoveFromRingGroup(string ringGroup,string extension,List<string> listExtensions,string rawExtensions) {
			string newExtensions="";
			for(int i=0;i<listExtensions.Count;i++) {
				if(listExtensions[i]==extension) {//skip this extension
					continue;
				}
				if(newExtensions!="") {
					newExtensions=newExtensions+"-";
				}
				newExtensions=newExtensions+listExtensions[i];
			}
			string command="UPDATE ringgroups SET grplist='"+POut.String(newExtensions)+"' "
				+"WHERE grpnum='"+ringGroup+"' "
				+"AND grplist = '"+POut.String(rawExtensions)+"'";
			DataConnection dcon=new DataConnection(AsteriskServerIp,"asterisk","opendental","secret",DatabaseType.MySql);
			dcon.NonQ(command);
		}

		///<summary>Deprecated.  For a given date, gets a list of dateTimes of missed calls.  Gets directly from the Asterisk database, hard-coded.</summary>
		public static List<DateTime> GetMissedCalls(DateTime date) {
			DataConnection dcon=new DataConnection(AsteriskServerIp,"asteriskcdrdb","opendental","secret",DatabaseType.MySql);
			string command="SELECT calldate FROM cdr WHERE "+DbHelper.DtimeToDate("calldate")+" = "+POut.Date(date)+" "
				+"AND (dcontext='ext-group' OR dcontext='ext-local') AND dst='vmu998'";
			List<DateTime> retVal=new List<DateTime>();
			DataTable table=dcon.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++) {
				retVal.Add(PIn.DateT(table.Rows[i][0].ToString()));
			}
			return retVal;
		}
		


	}


}



