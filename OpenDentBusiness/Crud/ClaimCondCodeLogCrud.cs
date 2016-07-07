//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Crud{
	public class ClaimCondCodeLogCrud {
		///<summary>Gets one ClaimCondCodeLog object from the database using the primary key.  Returns null if not found.</summary>
		public static ClaimCondCodeLog SelectOne(long claimCondCodeLogNum){
			string command="SELECT * FROM claimcondcodelog "
				+"WHERE ClaimCondCodeLogNum = "+POut.Long(claimCondCodeLogNum);
			List<ClaimCondCodeLog> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one ClaimCondCodeLog object from the database using a query.</summary>
		public static ClaimCondCodeLog SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<ClaimCondCodeLog> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of ClaimCondCodeLog objects from the database using a query.</summary>
		public static List<ClaimCondCodeLog> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<ClaimCondCodeLog> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<ClaimCondCodeLog> TableToList(DataTable table){
			List<ClaimCondCodeLog> retVal=new List<ClaimCondCodeLog>();
			ClaimCondCodeLog claimCondCodeLog;
			foreach(DataRow row in table.Rows) {
				claimCondCodeLog=new ClaimCondCodeLog();
				claimCondCodeLog.ClaimCondCodeLogNum= PIn.Long  (row["ClaimCondCodeLogNum"].ToString());
				claimCondCodeLog.ClaimNum           = PIn.Long  (row["ClaimNum"].ToString());
				claimCondCodeLog.Code0              = PIn.String(row["Code0"].ToString());
				claimCondCodeLog.Code1              = PIn.String(row["Code1"].ToString());
				claimCondCodeLog.Code2              = PIn.String(row["Code2"].ToString());
				claimCondCodeLog.Code3              = PIn.String(row["Code3"].ToString());
				claimCondCodeLog.Code4              = PIn.String(row["Code4"].ToString());
				claimCondCodeLog.Code5              = PIn.String(row["Code5"].ToString());
				claimCondCodeLog.Code6              = PIn.String(row["Code6"].ToString());
				claimCondCodeLog.Code7              = PIn.String(row["Code7"].ToString());
				claimCondCodeLog.Code8              = PIn.String(row["Code8"].ToString());
				claimCondCodeLog.Code9              = PIn.String(row["Code9"].ToString());
				claimCondCodeLog.Code10             = PIn.String(row["Code10"].ToString());
				retVal.Add(claimCondCodeLog);
			}
			return retVal;
		}

		///<summary>Inserts one ClaimCondCodeLog into the database.  Returns the new priKey.</summary>
		public static long Insert(ClaimCondCodeLog claimCondCodeLog){
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				claimCondCodeLog.ClaimCondCodeLogNum=DbHelper.GetNextOracleKey("claimcondcodelog","ClaimCondCodeLogNum");
				int loopcount=0;
				while(loopcount<100){
					try {
						return Insert(claimCondCodeLog,true);
					}
					catch(Oracle.DataAccess.Client.OracleException ex){
						if(ex.Number==1 && ex.Message.ToLower().Contains("unique constraint") && ex.Message.ToLower().Contains("violated")){
							claimCondCodeLog.ClaimCondCodeLogNum++;
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
				return Insert(claimCondCodeLog,false);
			}
		}

		///<summary>Inserts one ClaimCondCodeLog into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(ClaimCondCodeLog claimCondCodeLog,bool useExistingPK){
			if(!useExistingPK && PrefC.RandomKeys) {
				claimCondCodeLog.ClaimCondCodeLogNum=ReplicationServers.GetKey("claimcondcodelog","ClaimCondCodeLogNum");
			}
			string command="INSERT INTO claimcondcodelog (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="ClaimCondCodeLogNum,";
			}
			command+="ClaimNum,Code0,Code1,Code2,Code3,Code4,Code5,Code6,Code7,Code8,Code9,Code10) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(claimCondCodeLog.ClaimCondCodeLogNum)+",";
			}
			command+=
				     POut.Long  (claimCondCodeLog.ClaimNum)+","
				+"'"+POut.String(claimCondCodeLog.Code0)+"',"
				+"'"+POut.String(claimCondCodeLog.Code1)+"',"
				+"'"+POut.String(claimCondCodeLog.Code2)+"',"
				+"'"+POut.String(claimCondCodeLog.Code3)+"',"
				+"'"+POut.String(claimCondCodeLog.Code4)+"',"
				+"'"+POut.String(claimCondCodeLog.Code5)+"',"
				+"'"+POut.String(claimCondCodeLog.Code6)+"',"
				+"'"+POut.String(claimCondCodeLog.Code7)+"',"
				+"'"+POut.String(claimCondCodeLog.Code8)+"',"
				+"'"+POut.String(claimCondCodeLog.Code9)+"',"
				+"'"+POut.String(claimCondCodeLog.Code10)+"')";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				claimCondCodeLog.ClaimCondCodeLogNum=Db.NonQ(command,true);
			}
			return claimCondCodeLog.ClaimCondCodeLogNum;
		}

		///<summary>Inserts one ClaimCondCodeLog into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(ClaimCondCodeLog claimCondCodeLog){
			if(DataConnection.DBtype==DatabaseType.MySql) {
				return InsertNoCache(claimCondCodeLog,false);
			}
			else {
				if(DataConnection.DBtype==DatabaseType.Oracle) {
					claimCondCodeLog.ClaimCondCodeLogNum=DbHelper.GetNextOracleKey("claimcondcodelog","ClaimCondCodeLogNum"); //Cacheless method
				}
				return InsertNoCache(claimCondCodeLog,true);
			}
		}

		///<summary>Inserts one ClaimCondCodeLog into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(ClaimCondCodeLog claimCondCodeLog,bool useExistingPK){
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO claimcondcodelog (";
			if(!useExistingPK && isRandomKeys) {
				claimCondCodeLog.ClaimCondCodeLogNum=ReplicationServers.GetKeyNoCache("claimcondcodelog","ClaimCondCodeLogNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="ClaimCondCodeLogNum,";
			}
			command+="ClaimNum,Code0,Code1,Code2,Code3,Code4,Code5,Code6,Code7,Code8,Code9,Code10) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(claimCondCodeLog.ClaimCondCodeLogNum)+",";
			}
			command+=
				     POut.Long  (claimCondCodeLog.ClaimNum)+","
				+"'"+POut.String(claimCondCodeLog.Code0)+"',"
				+"'"+POut.String(claimCondCodeLog.Code1)+"',"
				+"'"+POut.String(claimCondCodeLog.Code2)+"',"
				+"'"+POut.String(claimCondCodeLog.Code3)+"',"
				+"'"+POut.String(claimCondCodeLog.Code4)+"',"
				+"'"+POut.String(claimCondCodeLog.Code5)+"',"
				+"'"+POut.String(claimCondCodeLog.Code6)+"',"
				+"'"+POut.String(claimCondCodeLog.Code7)+"',"
				+"'"+POut.String(claimCondCodeLog.Code8)+"',"
				+"'"+POut.String(claimCondCodeLog.Code9)+"',"
				+"'"+POut.String(claimCondCodeLog.Code10)+"')";
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command);
			}
			else {
				claimCondCodeLog.ClaimCondCodeLogNum=Db.NonQ(command,true);
			}
			return claimCondCodeLog.ClaimCondCodeLogNum;
		}

		///<summary>Updates one ClaimCondCodeLog in the database.</summary>
		public static void Update(ClaimCondCodeLog claimCondCodeLog){
			string command="UPDATE claimcondcodelog SET "
				+"ClaimNum           =  "+POut.Long  (claimCondCodeLog.ClaimNum)+", "
				+"Code0              = '"+POut.String(claimCondCodeLog.Code0)+"', "
				+"Code1              = '"+POut.String(claimCondCodeLog.Code1)+"', "
				+"Code2              = '"+POut.String(claimCondCodeLog.Code2)+"', "
				+"Code3              = '"+POut.String(claimCondCodeLog.Code3)+"', "
				+"Code4              = '"+POut.String(claimCondCodeLog.Code4)+"', "
				+"Code5              = '"+POut.String(claimCondCodeLog.Code5)+"', "
				+"Code6              = '"+POut.String(claimCondCodeLog.Code6)+"', "
				+"Code7              = '"+POut.String(claimCondCodeLog.Code7)+"', "
				+"Code8              = '"+POut.String(claimCondCodeLog.Code8)+"', "
				+"Code9              = '"+POut.String(claimCondCodeLog.Code9)+"', "
				+"Code10             = '"+POut.String(claimCondCodeLog.Code10)+"' "
				+"WHERE ClaimCondCodeLogNum = "+POut.Long(claimCondCodeLog.ClaimCondCodeLogNum);
			Db.NonQ(command);
		}

		///<summary>Updates one ClaimCondCodeLog in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(ClaimCondCodeLog claimCondCodeLog,ClaimCondCodeLog oldClaimCondCodeLog){
			string command="";
			if(claimCondCodeLog.ClaimNum != oldClaimCondCodeLog.ClaimNum) {
				if(command!=""){ command+=",";}
				command+="ClaimNum = "+POut.Long(claimCondCodeLog.ClaimNum)+"";
			}
			if(claimCondCodeLog.Code0 != oldClaimCondCodeLog.Code0) {
				if(command!=""){ command+=",";}
				command+="Code0 = '"+POut.String(claimCondCodeLog.Code0)+"'";
			}
			if(claimCondCodeLog.Code1 != oldClaimCondCodeLog.Code1) {
				if(command!=""){ command+=",";}
				command+="Code1 = '"+POut.String(claimCondCodeLog.Code1)+"'";
			}
			if(claimCondCodeLog.Code2 != oldClaimCondCodeLog.Code2) {
				if(command!=""){ command+=",";}
				command+="Code2 = '"+POut.String(claimCondCodeLog.Code2)+"'";
			}
			if(claimCondCodeLog.Code3 != oldClaimCondCodeLog.Code3) {
				if(command!=""){ command+=",";}
				command+="Code3 = '"+POut.String(claimCondCodeLog.Code3)+"'";
			}
			if(claimCondCodeLog.Code4 != oldClaimCondCodeLog.Code4) {
				if(command!=""){ command+=",";}
				command+="Code4 = '"+POut.String(claimCondCodeLog.Code4)+"'";
			}
			if(claimCondCodeLog.Code5 != oldClaimCondCodeLog.Code5) {
				if(command!=""){ command+=",";}
				command+="Code5 = '"+POut.String(claimCondCodeLog.Code5)+"'";
			}
			if(claimCondCodeLog.Code6 != oldClaimCondCodeLog.Code6) {
				if(command!=""){ command+=",";}
				command+="Code6 = '"+POut.String(claimCondCodeLog.Code6)+"'";
			}
			if(claimCondCodeLog.Code7 != oldClaimCondCodeLog.Code7) {
				if(command!=""){ command+=",";}
				command+="Code7 = '"+POut.String(claimCondCodeLog.Code7)+"'";
			}
			if(claimCondCodeLog.Code8 != oldClaimCondCodeLog.Code8) {
				if(command!=""){ command+=",";}
				command+="Code8 = '"+POut.String(claimCondCodeLog.Code8)+"'";
			}
			if(claimCondCodeLog.Code9 != oldClaimCondCodeLog.Code9) {
				if(command!=""){ command+=",";}
				command+="Code9 = '"+POut.String(claimCondCodeLog.Code9)+"'";
			}
			if(claimCondCodeLog.Code10 != oldClaimCondCodeLog.Code10) {
				if(command!=""){ command+=",";}
				command+="Code10 = '"+POut.String(claimCondCodeLog.Code10)+"'";
			}
			if(command==""){
				return false;
			}
			command="UPDATE claimcondcodelog SET "+command
				+" WHERE ClaimCondCodeLogNum = "+POut.Long(claimCondCodeLog.ClaimCondCodeLogNum);
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if Update(ClaimCondCodeLog,ClaimCondCodeLog) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(ClaimCondCodeLog claimCondCodeLog,ClaimCondCodeLog oldClaimCondCodeLog) {
			if(claimCondCodeLog.ClaimNum != oldClaimCondCodeLog.ClaimNum) {
				return true;
			}
			if(claimCondCodeLog.Code0 != oldClaimCondCodeLog.Code0) {
				return true;
			}
			if(claimCondCodeLog.Code1 != oldClaimCondCodeLog.Code1) {
				return true;
			}
			if(claimCondCodeLog.Code2 != oldClaimCondCodeLog.Code2) {
				return true;
			}
			if(claimCondCodeLog.Code3 != oldClaimCondCodeLog.Code3) {
				return true;
			}
			if(claimCondCodeLog.Code4 != oldClaimCondCodeLog.Code4) {
				return true;
			}
			if(claimCondCodeLog.Code5 != oldClaimCondCodeLog.Code5) {
				return true;
			}
			if(claimCondCodeLog.Code6 != oldClaimCondCodeLog.Code6) {
				return true;
			}
			if(claimCondCodeLog.Code7 != oldClaimCondCodeLog.Code7) {
				return true;
			}
			if(claimCondCodeLog.Code8 != oldClaimCondCodeLog.Code8) {
				return true;
			}
			if(claimCondCodeLog.Code9 != oldClaimCondCodeLog.Code9) {
				return true;
			}
			if(claimCondCodeLog.Code10 != oldClaimCondCodeLog.Code10) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one ClaimCondCodeLog from the database.</summary>
		public static void Delete(long claimCondCodeLogNum){
			string command="DELETE FROM claimcondcodelog "
				+"WHERE ClaimCondCodeLogNum = "+POut.Long(claimCondCodeLogNum);
			Db.NonQ(command);
		}

	}
}