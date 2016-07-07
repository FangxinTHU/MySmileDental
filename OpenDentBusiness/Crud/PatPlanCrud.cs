//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Crud{
	public class PatPlanCrud {
		///<summary>Gets one PatPlan object from the database using the primary key.  Returns null if not found.</summary>
		public static PatPlan SelectOne(long patPlanNum){
			string command="SELECT * FROM patplan "
				+"WHERE PatPlanNum = "+POut.Long(patPlanNum);
			List<PatPlan> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one PatPlan object from the database using a query.</summary>
		public static PatPlan SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<PatPlan> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of PatPlan objects from the database using a query.</summary>
		public static List<PatPlan> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<PatPlan> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<PatPlan> TableToList(DataTable table){
			List<PatPlan> retVal=new List<PatPlan>();
			PatPlan patPlan;
			foreach(DataRow row in table.Rows) {
				patPlan=new PatPlan();
				patPlan.PatPlanNum  = PIn.Long  (row["PatPlanNum"].ToString());
				patPlan.PatNum      = PIn.Long  (row["PatNum"].ToString());
				patPlan.Ordinal     = PIn.Byte  (row["Ordinal"].ToString());
				patPlan.IsPending   = PIn.Bool  (row["IsPending"].ToString());
				patPlan.Relationship= (OpenDentBusiness.Relat)PIn.Int(row["Relationship"].ToString());
				patPlan.PatID       = PIn.String(row["PatID"].ToString());
				patPlan.InsSubNum   = PIn.Long  (row["InsSubNum"].ToString());
				retVal.Add(patPlan);
			}
			return retVal;
		}

		///<summary>Inserts one PatPlan into the database.  Returns the new priKey.</summary>
		public static long Insert(PatPlan patPlan){
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				patPlan.PatPlanNum=DbHelper.GetNextOracleKey("patplan","PatPlanNum");
				int loopcount=0;
				while(loopcount<100){
					try {
						return Insert(patPlan,true);
					}
					catch(Oracle.DataAccess.Client.OracleException ex){
						if(ex.Number==1 && ex.Message.ToLower().Contains("unique constraint") && ex.Message.ToLower().Contains("violated")){
							patPlan.PatPlanNum++;
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
				return Insert(patPlan,false);
			}
		}

		///<summary>Inserts one PatPlan into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(PatPlan patPlan,bool useExistingPK){
			if(!useExistingPK && PrefC.RandomKeys) {
				patPlan.PatPlanNum=ReplicationServers.GetKey("patplan","PatPlanNum");
			}
			string command="INSERT INTO patplan (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="PatPlanNum,";
			}
			command+="PatNum,Ordinal,IsPending,Relationship,PatID,InsSubNum) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(patPlan.PatPlanNum)+",";
			}
			command+=
				     POut.Long  (patPlan.PatNum)+","
				+    POut.Byte  (patPlan.Ordinal)+","
				+    POut.Bool  (patPlan.IsPending)+","
				+    POut.Int   ((int)patPlan.Relationship)+","
				+"'"+POut.String(patPlan.PatID)+"',"
				+    POut.Long  (patPlan.InsSubNum)+")";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				patPlan.PatPlanNum=Db.NonQ(command,true);
			}
			return patPlan.PatPlanNum;
		}

		///<summary>Inserts one PatPlan into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(PatPlan patPlan){
			if(DataConnection.DBtype==DatabaseType.MySql) {
				return InsertNoCache(patPlan,false);
			}
			else {
				if(DataConnection.DBtype==DatabaseType.Oracle) {
					patPlan.PatPlanNum=DbHelper.GetNextOracleKey("patplan","PatPlanNum"); //Cacheless method
				}
				return InsertNoCache(patPlan,true);
			}
		}

		///<summary>Inserts one PatPlan into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(PatPlan patPlan,bool useExistingPK){
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO patplan (";
			if(!useExistingPK && isRandomKeys) {
				patPlan.PatPlanNum=ReplicationServers.GetKeyNoCache("patplan","PatPlanNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="PatPlanNum,";
			}
			command+="PatNum,Ordinal,IsPending,Relationship,PatID,InsSubNum) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(patPlan.PatPlanNum)+",";
			}
			command+=
				     POut.Long  (patPlan.PatNum)+","
				+    POut.Byte  (patPlan.Ordinal)+","
				+    POut.Bool  (patPlan.IsPending)+","
				+    POut.Int   ((int)patPlan.Relationship)+","
				+"'"+POut.String(patPlan.PatID)+"',"
				+    POut.Long  (patPlan.InsSubNum)+")";
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command);
			}
			else {
				patPlan.PatPlanNum=Db.NonQ(command,true);
			}
			return patPlan.PatPlanNum;
		}

		///<summary>Updates one PatPlan in the database.</summary>
		public static void Update(PatPlan patPlan){
			string command="UPDATE patplan SET "
				+"PatNum      =  "+POut.Long  (patPlan.PatNum)+", "
				+"Ordinal     =  "+POut.Byte  (patPlan.Ordinal)+", "
				+"IsPending   =  "+POut.Bool  (patPlan.IsPending)+", "
				+"Relationship=  "+POut.Int   ((int)patPlan.Relationship)+", "
				+"PatID       = '"+POut.String(patPlan.PatID)+"', "
				+"InsSubNum   =  "+POut.Long  (patPlan.InsSubNum)+" "
				+"WHERE PatPlanNum = "+POut.Long(patPlan.PatPlanNum);
			Db.NonQ(command);
		}

		///<summary>Updates one PatPlan in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(PatPlan patPlan,PatPlan oldPatPlan){
			string command="";
			if(patPlan.PatNum != oldPatPlan.PatNum) {
				if(command!=""){ command+=",";}
				command+="PatNum = "+POut.Long(patPlan.PatNum)+"";
			}
			if(patPlan.Ordinal != oldPatPlan.Ordinal) {
				if(command!=""){ command+=",";}
				command+="Ordinal = "+POut.Byte(patPlan.Ordinal)+"";
			}
			if(patPlan.IsPending != oldPatPlan.IsPending) {
				if(command!=""){ command+=",";}
				command+="IsPending = "+POut.Bool(patPlan.IsPending)+"";
			}
			if(patPlan.Relationship != oldPatPlan.Relationship) {
				if(command!=""){ command+=",";}
				command+="Relationship = "+POut.Int   ((int)patPlan.Relationship)+"";
			}
			if(patPlan.PatID != oldPatPlan.PatID) {
				if(command!=""){ command+=",";}
				command+="PatID = '"+POut.String(patPlan.PatID)+"'";
			}
			if(patPlan.InsSubNum != oldPatPlan.InsSubNum) {
				if(command!=""){ command+=",";}
				command+="InsSubNum = "+POut.Long(patPlan.InsSubNum)+"";
			}
			if(command==""){
				return false;
			}
			command="UPDATE patplan SET "+command
				+" WHERE PatPlanNum = "+POut.Long(patPlan.PatPlanNum);
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if Update(PatPlan,PatPlan) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(PatPlan patPlan,PatPlan oldPatPlan) {
			if(patPlan.PatNum != oldPatPlan.PatNum) {
				return true;
			}
			if(patPlan.Ordinal != oldPatPlan.Ordinal) {
				return true;
			}
			if(patPlan.IsPending != oldPatPlan.IsPending) {
				return true;
			}
			if(patPlan.Relationship != oldPatPlan.Relationship) {
				return true;
			}
			if(patPlan.PatID != oldPatPlan.PatID) {
				return true;
			}
			if(patPlan.InsSubNum != oldPatPlan.InsSubNum) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one PatPlan from the database.</summary>
		public static void Delete(long patPlanNum){
			string command="DELETE FROM patplan "
				+"WHERE PatPlanNum = "+POut.Long(patPlanNum);
			Db.NonQ(command);
		}

	}
}