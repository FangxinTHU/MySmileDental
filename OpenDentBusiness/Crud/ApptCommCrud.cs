//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Crud{
	public class ApptCommCrud {
		///<summary>Gets one ApptComm object from the database using the primary key.  Returns null if not found.</summary>
		public static ApptComm SelectOne(long apptCommNum){
			string command="SELECT * FROM apptcomm "
				+"WHERE ApptCommNum = "+POut.Long(apptCommNum);
			List<ApptComm> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one ApptComm object from the database using a query.</summary>
		public static ApptComm SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<ApptComm> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of ApptComm objects from the database using a query.</summary>
		public static List<ApptComm> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<ApptComm> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<ApptComm> TableToList(DataTable table){
			List<ApptComm> retVal=new List<ApptComm>();
			ApptComm apptComm;
			foreach(DataRow row in table.Rows) {
				apptComm=new ApptComm();
				apptComm.ApptCommNum = PIn.Long  (row["ApptCommNum"].ToString());
				apptComm.ApptNum     = PIn.Long  (row["ApptNum"].ToString());
				apptComm.ApptCommType= (OpenDentBusiness.IntervalType)PIn.Int(row["ApptCommType"].ToString());
				apptComm.DateTimeSend= PIn.DateT (row["DateTimeSend"].ToString());
				retVal.Add(apptComm);
			}
			return retVal;
		}

		///<summary>Inserts one ApptComm into the database.  Returns the new priKey.</summary>
		public static long Insert(ApptComm apptComm){
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				apptComm.ApptCommNum=DbHelper.GetNextOracleKey("apptcomm","ApptCommNum");
				int loopcount=0;
				while(loopcount<100){
					try {
						return Insert(apptComm,true);
					}
					catch(Oracle.DataAccess.Client.OracleException ex){
						if(ex.Number==1 && ex.Message.ToLower().Contains("unique constraint") && ex.Message.ToLower().Contains("violated")){
							apptComm.ApptCommNum++;
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
				return Insert(apptComm,false);
			}
		}

		///<summary>Inserts one ApptComm into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(ApptComm apptComm,bool useExistingPK){
			if(!useExistingPK && PrefC.RandomKeys) {
				apptComm.ApptCommNum=ReplicationServers.GetKey("apptcomm","ApptCommNum");
			}
			string command="INSERT INTO apptcomm (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="ApptCommNum,";
			}
			command+="ApptNum,ApptCommType,DateTimeSend) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(apptComm.ApptCommNum)+",";
			}
			command+=
				     POut.Long  (apptComm.ApptNum)+","
				+    POut.Int   ((int)apptComm.ApptCommType)+","
				+    POut.DateT (apptComm.DateTimeSend)+")";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				apptComm.ApptCommNum=Db.NonQ(command,true);
			}
			return apptComm.ApptCommNum;
		}

		///<summary>Inserts one ApptComm into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(ApptComm apptComm){
			if(DataConnection.DBtype==DatabaseType.MySql) {
				return InsertNoCache(apptComm,false);
			}
			else {
				if(DataConnection.DBtype==DatabaseType.Oracle) {
					apptComm.ApptCommNum=DbHelper.GetNextOracleKey("apptcomm","ApptCommNum"); //Cacheless method
				}
				return InsertNoCache(apptComm,true);
			}
		}

		///<summary>Inserts one ApptComm into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(ApptComm apptComm,bool useExistingPK){
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO apptcomm (";
			if(!useExistingPK && isRandomKeys) {
				apptComm.ApptCommNum=ReplicationServers.GetKeyNoCache("apptcomm","ApptCommNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="ApptCommNum,";
			}
			command+="ApptNum,ApptCommType,DateTimeSend) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(apptComm.ApptCommNum)+",";
			}
			command+=
				     POut.Long  (apptComm.ApptNum)+","
				+    POut.Int   ((int)apptComm.ApptCommType)+","
				+    POut.DateT (apptComm.DateTimeSend)+")";
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command);
			}
			else {
				apptComm.ApptCommNum=Db.NonQ(command,true);
			}
			return apptComm.ApptCommNum;
		}

		///<summary>Updates one ApptComm in the database.</summary>
		public static void Update(ApptComm apptComm){
			string command="UPDATE apptcomm SET "
				+"ApptNum     =  "+POut.Long  (apptComm.ApptNum)+", "
				+"ApptCommType=  "+POut.Int   ((int)apptComm.ApptCommType)+", "
				+"DateTimeSend=  "+POut.DateT (apptComm.DateTimeSend)+" "
				+"WHERE ApptCommNum = "+POut.Long(apptComm.ApptCommNum);
			Db.NonQ(command);
		}

		///<summary>Updates one ApptComm in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(ApptComm apptComm,ApptComm oldApptComm){
			string command="";
			if(apptComm.ApptNum != oldApptComm.ApptNum) {
				if(command!=""){ command+=",";}
				command+="ApptNum = "+POut.Long(apptComm.ApptNum)+"";
			}
			if(apptComm.ApptCommType != oldApptComm.ApptCommType) {
				if(command!=""){ command+=",";}
				command+="ApptCommType = "+POut.Int   ((int)apptComm.ApptCommType)+"";
			}
			if(apptComm.DateTimeSend != oldApptComm.DateTimeSend) {
				if(command!=""){ command+=",";}
				command+="DateTimeSend = "+POut.DateT(apptComm.DateTimeSend)+"";
			}
			if(command==""){
				return false;
			}
			command="UPDATE apptcomm SET "+command
				+" WHERE ApptCommNum = "+POut.Long(apptComm.ApptCommNum);
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if Update(ApptComm,ApptComm) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(ApptComm apptComm,ApptComm oldApptComm) {
			if(apptComm.ApptNum != oldApptComm.ApptNum) {
				return true;
			}
			if(apptComm.ApptCommType != oldApptComm.ApptCommType) {
				return true;
			}
			if(apptComm.DateTimeSend != oldApptComm.DateTimeSend) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one ApptComm from the database.</summary>
		public static void Delete(long apptCommNum){
			string command="DELETE FROM apptcomm "
				+"WHERE ApptCommNum = "+POut.Long(apptCommNum);
			Db.NonQ(command);
		}

	}
}