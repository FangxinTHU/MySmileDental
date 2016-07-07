//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Crud{
	public class DepositCrud {
		///<summary>Gets one Deposit object from the database using the primary key.  Returns null if not found.</summary>
		public static Deposit SelectOne(long depositNum){
			string command="SELECT * FROM deposit "
				+"WHERE DepositNum = "+POut.Long(depositNum);
			List<Deposit> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one Deposit object from the database using a query.</summary>
		public static Deposit SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<Deposit> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of Deposit objects from the database using a query.</summary>
		public static List<Deposit> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<Deposit> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<Deposit> TableToList(DataTable table){
			List<Deposit> retVal=new List<Deposit>();
			Deposit deposit;
			foreach(DataRow row in table.Rows) {
				deposit=new Deposit();
				deposit.DepositNum     = PIn.Long  (row["DepositNum"].ToString());
				deposit.DateDeposit    = PIn.Date  (row["DateDeposit"].ToString());
				deposit.BankAccountInfo= PIn.String(row["BankAccountInfo"].ToString());
				deposit.Amount         = PIn.Double(row["Amount"].ToString());
				deposit.Memo           = PIn.String(row["Memo"].ToString());
				retVal.Add(deposit);
			}
			return retVal;
		}

		///<summary>Inserts one Deposit into the database.  Returns the new priKey.</summary>
		public static long Insert(Deposit deposit){
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				deposit.DepositNum=DbHelper.GetNextOracleKey("deposit","DepositNum");
				int loopcount=0;
				while(loopcount<100){
					try {
						return Insert(deposit,true);
					}
					catch(Oracle.DataAccess.Client.OracleException ex){
						if(ex.Number==1 && ex.Message.ToLower().Contains("unique constraint") && ex.Message.ToLower().Contains("violated")){
							deposit.DepositNum++;
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
				return Insert(deposit,false);
			}
		}

		///<summary>Inserts one Deposit into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(Deposit deposit,bool useExistingPK){
			if(!useExistingPK && PrefC.RandomKeys) {
				deposit.DepositNum=ReplicationServers.GetKey("deposit","DepositNum");
			}
			string command="INSERT INTO deposit (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="DepositNum,";
			}
			command+="DateDeposit,BankAccountInfo,Amount,Memo) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(deposit.DepositNum)+",";
			}
			command+=
				     POut.Date  (deposit.DateDeposit)+","
				+"'"+POut.String(deposit.BankAccountInfo)+"',"
				+"'"+POut.Double(deposit.Amount)+"',"
				+"'"+POut.String(deposit.Memo)+"')";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				deposit.DepositNum=Db.NonQ(command,true);
			}
			return deposit.DepositNum;
		}

		///<summary>Inserts one Deposit into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(Deposit deposit){
			if(DataConnection.DBtype==DatabaseType.MySql) {
				return InsertNoCache(deposit,false);
			}
			else {
				if(DataConnection.DBtype==DatabaseType.Oracle) {
					deposit.DepositNum=DbHelper.GetNextOracleKey("deposit","DepositNum"); //Cacheless method
				}
				return InsertNoCache(deposit,true);
			}
		}

		///<summary>Inserts one Deposit into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(Deposit deposit,bool useExistingPK){
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO deposit (";
			if(!useExistingPK && isRandomKeys) {
				deposit.DepositNum=ReplicationServers.GetKeyNoCache("deposit","DepositNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="DepositNum,";
			}
			command+="DateDeposit,BankAccountInfo,Amount,Memo) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(deposit.DepositNum)+",";
			}
			command+=
				     POut.Date  (deposit.DateDeposit)+","
				+"'"+POut.String(deposit.BankAccountInfo)+"',"
				+"'"+POut.Double(deposit.Amount)+"',"
				+"'"+POut.String(deposit.Memo)+"')";
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command);
			}
			else {
				deposit.DepositNum=Db.NonQ(command,true);
			}
			return deposit.DepositNum;
		}

		///<summary>Updates one Deposit in the database.</summary>
		public static void Update(Deposit deposit){
			string command="UPDATE deposit SET "
				+"DateDeposit    =  "+POut.Date  (deposit.DateDeposit)+", "
				+"BankAccountInfo= '"+POut.String(deposit.BankAccountInfo)+"', "
				+"Amount         = '"+POut.Double(deposit.Amount)+"', "
				+"Memo           = '"+POut.String(deposit.Memo)+"' "
				+"WHERE DepositNum = "+POut.Long(deposit.DepositNum);
			Db.NonQ(command);
		}

		///<summary>Updates one Deposit in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(Deposit deposit,Deposit oldDeposit){
			string command="";
			if(deposit.DateDeposit != oldDeposit.DateDeposit) {
				if(command!=""){ command+=",";}
				command+="DateDeposit = "+POut.Date(deposit.DateDeposit)+"";
			}
			if(deposit.BankAccountInfo != oldDeposit.BankAccountInfo) {
				if(command!=""){ command+=",";}
				command+="BankAccountInfo = '"+POut.String(deposit.BankAccountInfo)+"'";
			}
			if(deposit.Amount != oldDeposit.Amount) {
				if(command!=""){ command+=",";}
				command+="Amount = '"+POut.Double(deposit.Amount)+"'";
			}
			if(deposit.Memo != oldDeposit.Memo) {
				if(command!=""){ command+=",";}
				command+="Memo = '"+POut.String(deposit.Memo)+"'";
			}
			if(command==""){
				return false;
			}
			command="UPDATE deposit SET "+command
				+" WHERE DepositNum = "+POut.Long(deposit.DepositNum);
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if Update(Deposit,Deposit) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(Deposit deposit,Deposit oldDeposit) {
			if(deposit.DateDeposit != oldDeposit.DateDeposit) {
				return true;
			}
			if(deposit.BankAccountInfo != oldDeposit.BankAccountInfo) {
				return true;
			}
			if(deposit.Amount != oldDeposit.Amount) {
				return true;
			}
			if(deposit.Memo != oldDeposit.Memo) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one Deposit from the database.</summary>
		public static void Delete(long depositNum){
			string command="DELETE FROM deposit "
				+"WHERE DepositNum = "+POut.Long(depositNum);
			Db.NonQ(command);
		}

	}
}