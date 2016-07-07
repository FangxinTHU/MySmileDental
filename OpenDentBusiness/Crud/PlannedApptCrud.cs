//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Crud{
	public class PlannedApptCrud {
		///<summary>Gets one PlannedAppt object from the database using the primary key.  Returns null if not found.</summary>
		public static PlannedAppt SelectOne(long plannedApptNum){
			string command="SELECT * FROM plannedappt "
				+"WHERE PlannedApptNum = "+POut.Long(plannedApptNum);
			List<PlannedAppt> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one PlannedAppt object from the database using a query.</summary>
		public static PlannedAppt SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<PlannedAppt> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of PlannedAppt objects from the database using a query.</summary>
		public static List<PlannedAppt> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<PlannedAppt> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<PlannedAppt> TableToList(DataTable table){
			List<PlannedAppt> retVal=new List<PlannedAppt>();
			PlannedAppt plannedAppt;
			foreach(DataRow row in table.Rows) {
				plannedAppt=new PlannedAppt();
				plannedAppt.PlannedApptNum= PIn.Long  (row["PlannedApptNum"].ToString());
				plannedAppt.PatNum        = PIn.Long  (row["PatNum"].ToString());
				plannedAppt.AptNum        = PIn.Long  (row["AptNum"].ToString());
				plannedAppt.ItemOrder     = PIn.Int   (row["ItemOrder"].ToString());
				retVal.Add(plannedAppt);
			}
			return retVal;
		}

		///<summary>Inserts one PlannedAppt into the database.  Returns the new priKey.</summary>
		public static long Insert(PlannedAppt plannedAppt){
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				plannedAppt.PlannedApptNum=DbHelper.GetNextOracleKey("plannedappt","PlannedApptNum");
				int loopcount=0;
				while(loopcount<100){
					try {
						return Insert(plannedAppt,true);
					}
					catch(Oracle.DataAccess.Client.OracleException ex){
						if(ex.Number==1 && ex.Message.ToLower().Contains("unique constraint") && ex.Message.ToLower().Contains("violated")){
							plannedAppt.PlannedApptNum++;
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
				return Insert(plannedAppt,false);
			}
		}

		///<summary>Inserts one PlannedAppt into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(PlannedAppt plannedAppt,bool useExistingPK){
			if(!useExistingPK && PrefC.RandomKeys) {
				plannedAppt.PlannedApptNum=ReplicationServers.GetKey("plannedappt","PlannedApptNum");
			}
			string command="INSERT INTO plannedappt (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="PlannedApptNum,";
			}
			command+="PatNum,AptNum,ItemOrder) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(plannedAppt.PlannedApptNum)+",";
			}
			command+=
				     POut.Long  (plannedAppt.PatNum)+","
				+    POut.Long  (plannedAppt.AptNum)+","
				+    POut.Int   (plannedAppt.ItemOrder)+")";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				plannedAppt.PlannedApptNum=Db.NonQ(command,true);
			}
			return plannedAppt.PlannedApptNum;
		}

		///<summary>Inserts one PlannedAppt into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(PlannedAppt plannedAppt){
			if(DataConnection.DBtype==DatabaseType.MySql) {
				return InsertNoCache(plannedAppt,false);
			}
			else {
				if(DataConnection.DBtype==DatabaseType.Oracle) {
					plannedAppt.PlannedApptNum=DbHelper.GetNextOracleKey("plannedappt","PlannedApptNum"); //Cacheless method
				}
				return InsertNoCache(plannedAppt,true);
			}
		}

		///<summary>Inserts one PlannedAppt into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(PlannedAppt plannedAppt,bool useExistingPK){
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO plannedappt (";
			if(!useExistingPK && isRandomKeys) {
				plannedAppt.PlannedApptNum=ReplicationServers.GetKeyNoCache("plannedappt","PlannedApptNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="PlannedApptNum,";
			}
			command+="PatNum,AptNum,ItemOrder) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(plannedAppt.PlannedApptNum)+",";
			}
			command+=
				     POut.Long  (plannedAppt.PatNum)+","
				+    POut.Long  (plannedAppt.AptNum)+","
				+    POut.Int   (plannedAppt.ItemOrder)+")";
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command);
			}
			else {
				plannedAppt.PlannedApptNum=Db.NonQ(command,true);
			}
			return plannedAppt.PlannedApptNum;
		}

		///<summary>Updates one PlannedAppt in the database.</summary>
		public static void Update(PlannedAppt plannedAppt){
			string command="UPDATE plannedappt SET "
				+"PatNum        =  "+POut.Long  (plannedAppt.PatNum)+", "
				+"AptNum        =  "+POut.Long  (plannedAppt.AptNum)+", "
				+"ItemOrder     =  "+POut.Int   (plannedAppt.ItemOrder)+" "
				+"WHERE PlannedApptNum = "+POut.Long(plannedAppt.PlannedApptNum);
			Db.NonQ(command);
		}

		///<summary>Updates one PlannedAppt in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(PlannedAppt plannedAppt,PlannedAppt oldPlannedAppt){
			string command="";
			if(plannedAppt.PatNum != oldPlannedAppt.PatNum) {
				if(command!=""){ command+=",";}
				command+="PatNum = "+POut.Long(plannedAppt.PatNum)+"";
			}
			if(plannedAppt.AptNum != oldPlannedAppt.AptNum) {
				if(command!=""){ command+=",";}
				command+="AptNum = "+POut.Long(plannedAppt.AptNum)+"";
			}
			if(plannedAppt.ItemOrder != oldPlannedAppt.ItemOrder) {
				if(command!=""){ command+=",";}
				command+="ItemOrder = "+POut.Int(plannedAppt.ItemOrder)+"";
			}
			if(command==""){
				return false;
			}
			command="UPDATE plannedappt SET "+command
				+" WHERE PlannedApptNum = "+POut.Long(plannedAppt.PlannedApptNum);
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if Update(PlannedAppt,PlannedAppt) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(PlannedAppt plannedAppt,PlannedAppt oldPlannedAppt) {
			if(plannedAppt.PatNum != oldPlannedAppt.PatNum) {
				return true;
			}
			if(plannedAppt.AptNum != oldPlannedAppt.AptNum) {
				return true;
			}
			if(plannedAppt.ItemOrder != oldPlannedAppt.ItemOrder) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one PlannedAppt from the database.</summary>
		public static void Delete(long plannedApptNum){
			string command="DELETE FROM plannedappt "
				+"WHERE PlannedApptNum = "+POut.Long(plannedApptNum);
			Db.NonQ(command);
		}

	}
}