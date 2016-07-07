//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Crud{
	public class ApptViewCrud {
		///<summary>Gets one ApptView object from the database using the primary key.  Returns null if not found.</summary>
		public static ApptView SelectOne(long apptViewNum){
			string command="SELECT * FROM apptview "
				+"WHERE ApptViewNum = "+POut.Long(apptViewNum);
			List<ApptView> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one ApptView object from the database using a query.</summary>
		public static ApptView SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<ApptView> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of ApptView objects from the database using a query.</summary>
		public static List<ApptView> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<ApptView> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<ApptView> TableToList(DataTable table){
			List<ApptView> retVal=new List<ApptView>();
			ApptView apptView;
			foreach(DataRow row in table.Rows) {
				apptView=new ApptView();
				apptView.ApptViewNum        = PIn.Long  (row["ApptViewNum"].ToString());
				apptView.Description        = PIn.String(row["Description"].ToString());
				apptView.ItemOrder          = PIn.Int   (row["ItemOrder"].ToString());
				apptView.RowsPerIncr        = PIn.Byte  (row["RowsPerIncr"].ToString());
				apptView.OnlyScheduledProvs = PIn.Bool  (row["OnlyScheduledProvs"].ToString());
				apptView.OnlySchedBeforeTime= PIn.Time(row["OnlySchedBeforeTime"].ToString());
				apptView.OnlySchedAfterTime = PIn.Time(row["OnlySchedAfterTime"].ToString());
				apptView.StackBehavUR       = (OpenDentBusiness.ApptViewStackBehavior)PIn.Int(row["StackBehavUR"].ToString());
				apptView.StackBehavLR       = (OpenDentBusiness.ApptViewStackBehavior)PIn.Int(row["StackBehavLR"].ToString());
				apptView.ClinicNum          = PIn.Long  (row["ClinicNum"].ToString());
				apptView.ApptTimeScrollStart= PIn.Time(row["ApptTimeScrollStart"].ToString());
				retVal.Add(apptView);
			}
			return retVal;
		}

		///<summary>Inserts one ApptView into the database.  Returns the new priKey.</summary>
		public static long Insert(ApptView apptView){
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				apptView.ApptViewNum=DbHelper.GetNextOracleKey("apptview","ApptViewNum");
				int loopcount=0;
				while(loopcount<100){
					try {
						return Insert(apptView,true);
					}
					catch(Oracle.DataAccess.Client.OracleException ex){
						if(ex.Number==1 && ex.Message.ToLower().Contains("unique constraint") && ex.Message.ToLower().Contains("violated")){
							apptView.ApptViewNum++;
							loopcount++;
						}
						else{
							throw ex;
						}
					}
				}
				throw new ApplicationException("Insert failed.  Could not generate primary key.");
			}
			else {
				return Insert(apptView,false);
			}
		}

		///<summary>Inserts one ApptView into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(ApptView apptView,bool useExistingPK){
			if(!useExistingPK && PrefC.RandomKeys) {
				apptView.ApptViewNum=ReplicationServers.GetKey("apptview","ApptViewNum");
			}
			string command="INSERT INTO apptview (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="ApptViewNum,";
			}
			command+="Description,ItemOrder,RowsPerIncr,OnlyScheduledProvs,OnlySchedBeforeTime,OnlySchedAfterTime,StackBehavUR,StackBehavLR,ClinicNum,ApptTimeScrollStart) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(apptView.ApptViewNum)+",";
			}
			command+=
				 "'"+POut.String(apptView.Description)+"',"
				+    POut.Int   (apptView.ItemOrder)+","
				+    POut.Byte  (apptView.RowsPerIncr)+","
				+    POut.Bool  (apptView.OnlyScheduledProvs)+","
				+    POut.Time  (apptView.OnlySchedBeforeTime)+","
				+    POut.Time  (apptView.OnlySchedAfterTime)+","
				+    POut.Int   ((int)apptView.StackBehavUR)+","
				+    POut.Int   ((int)apptView.StackBehavLR)+","
				+    POut.Long  (apptView.ClinicNum)+","
				+    POut.Time  (apptView.ApptTimeScrollStart)+")";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				apptView.ApptViewNum=Db.NonQ(command,true);
			}
			return apptView.ApptViewNum;
		}

		///<summary>Inserts one ApptView into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(ApptView apptView){
			if(DataConnection.DBtype==DatabaseType.MySql) {
				return InsertNoCache(apptView,false);
			}
			else {
				if(DataConnection.DBtype==DatabaseType.Oracle) {
					apptView.ApptViewNum=DbHelper.GetNextOracleKey("apptview","ApptViewNum"); //Cacheless method
				}
				return InsertNoCache(apptView,true);
			}
		}

		///<summary>Inserts one ApptView into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(ApptView apptView,bool useExistingPK){
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO apptview (";
			if(!useExistingPK && isRandomKeys) {
				apptView.ApptViewNum=ReplicationServers.GetKeyNoCache("apptview","ApptViewNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="ApptViewNum,";
			}
			command+="Description,ItemOrder,RowsPerIncr,OnlyScheduledProvs,OnlySchedBeforeTime,OnlySchedAfterTime,StackBehavUR,StackBehavLR,ClinicNum,ApptTimeScrollStart) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(apptView.ApptViewNum)+",";
			}
			command+=
				 "'"+POut.String(apptView.Description)+"',"
				+    POut.Int   (apptView.ItemOrder)+","
				+    POut.Byte  (apptView.RowsPerIncr)+","
				+    POut.Bool  (apptView.OnlyScheduledProvs)+","
				+    POut.Time  (apptView.OnlySchedBeforeTime)+","
				+    POut.Time  (apptView.OnlySchedAfterTime)+","
				+    POut.Int   ((int)apptView.StackBehavUR)+","
				+    POut.Int   ((int)apptView.StackBehavLR)+","
				+    POut.Long  (apptView.ClinicNum)+","
				+    POut.Time  (apptView.ApptTimeScrollStart)+")";
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command);
			}
			else {
				apptView.ApptViewNum=Db.NonQ(command,true);
			}
			return apptView.ApptViewNum;
		}

		///<summary>Updates one ApptView in the database.</summary>
		public static void Update(ApptView apptView){
			string command="UPDATE apptview SET "
				+"Description        = '"+POut.String(apptView.Description)+"', "
				+"ItemOrder          =  "+POut.Int   (apptView.ItemOrder)+", "
				+"RowsPerIncr        =  "+POut.Byte  (apptView.RowsPerIncr)+", "
				+"OnlyScheduledProvs =  "+POut.Bool  (apptView.OnlyScheduledProvs)+", "
				+"OnlySchedBeforeTime=  "+POut.Time  (apptView.OnlySchedBeforeTime)+", "
				+"OnlySchedAfterTime =  "+POut.Time  (apptView.OnlySchedAfterTime)+", "
				+"StackBehavUR       =  "+POut.Int   ((int)apptView.StackBehavUR)+", "
				+"StackBehavLR       =  "+POut.Int   ((int)apptView.StackBehavLR)+", "
				+"ClinicNum          =  "+POut.Long  (apptView.ClinicNum)+", "
				+"ApptTimeScrollStart=  "+POut.Time  (apptView.ApptTimeScrollStart)+" "
				+"WHERE ApptViewNum = "+POut.Long(apptView.ApptViewNum);
			Db.NonQ(command);
		}

		///<summary>Updates one ApptView in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(ApptView apptView,ApptView oldApptView){
			string command="";
			if(apptView.Description != oldApptView.Description) {
				if(command!=""){ command+=",";}
				command+="Description = '"+POut.String(apptView.Description)+"'";
			}
			if(apptView.ItemOrder != oldApptView.ItemOrder) {
				if(command!=""){ command+=",";}
				command+="ItemOrder = "+POut.Int(apptView.ItemOrder)+"";
			}
			if(apptView.RowsPerIncr != oldApptView.RowsPerIncr) {
				if(command!=""){ command+=",";}
				command+="RowsPerIncr = "+POut.Byte(apptView.RowsPerIncr)+"";
			}
			if(apptView.OnlyScheduledProvs != oldApptView.OnlyScheduledProvs) {
				if(command!=""){ command+=",";}
				command+="OnlyScheduledProvs = "+POut.Bool(apptView.OnlyScheduledProvs)+"";
			}
			if(apptView.OnlySchedBeforeTime != oldApptView.OnlySchedBeforeTime) {
				if(command!=""){ command+=",";}
				command+="OnlySchedBeforeTime = "+POut.Time  (apptView.OnlySchedBeforeTime)+"";
			}
			if(apptView.OnlySchedAfterTime != oldApptView.OnlySchedAfterTime) {
				if(command!=""){ command+=",";}
				command+="OnlySchedAfterTime = "+POut.Time  (apptView.OnlySchedAfterTime)+"";
			}
			if(apptView.StackBehavUR != oldApptView.StackBehavUR) {
				if(command!=""){ command+=",";}
				command+="StackBehavUR = "+POut.Int   ((int)apptView.StackBehavUR)+"";
			}
			if(apptView.StackBehavLR != oldApptView.StackBehavLR) {
				if(command!=""){ command+=",";}
				command+="StackBehavLR = "+POut.Int   ((int)apptView.StackBehavLR)+"";
			}
			if(apptView.ClinicNum != oldApptView.ClinicNum) {
				if(command!=""){ command+=",";}
				command+="ClinicNum = "+POut.Long(apptView.ClinicNum)+"";
			}
			if(apptView.ApptTimeScrollStart != oldApptView.ApptTimeScrollStart) {
				if(command!=""){ command+=",";}
				command+="ApptTimeScrollStart = "+POut.Time  (apptView.ApptTimeScrollStart)+"";
			}
			if(command==""){
				return false;
			}
			command="UPDATE apptview SET "+command
				+" WHERE ApptViewNum = "+POut.Long(apptView.ApptViewNum);
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if Update(ApptView,ApptView) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(ApptView apptView,ApptView oldApptView) {
			if(apptView.Description != oldApptView.Description) {
				return true;
			}
			if(apptView.ItemOrder != oldApptView.ItemOrder) {
				return true;
			}
			if(apptView.RowsPerIncr != oldApptView.RowsPerIncr) {
				return true;
			}
			if(apptView.OnlyScheduledProvs != oldApptView.OnlyScheduledProvs) {
				return true;
			}
			if(apptView.OnlySchedBeforeTime != oldApptView.OnlySchedBeforeTime) {
				return true;
			}
			if(apptView.OnlySchedAfterTime != oldApptView.OnlySchedAfterTime) {
				return true;
			}
			if(apptView.StackBehavUR != oldApptView.StackBehavUR) {
				return true;
			}
			if(apptView.StackBehavLR != oldApptView.StackBehavLR) {
				return true;
			}
			if(apptView.ClinicNum != oldApptView.ClinicNum) {
				return true;
			}
			if(apptView.ApptTimeScrollStart != oldApptView.ApptTimeScrollStart) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one ApptView from the database.</summary>
		public static void Delete(long apptViewNum){
			string command="DELETE FROM apptview "
				+"WHERE ApptViewNum = "+POut.Long(apptViewNum);
			Db.NonQ(command);
		}

	}
}