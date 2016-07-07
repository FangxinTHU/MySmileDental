//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Crud{
	public class XChargeTransactionCrud {
		///<summary>Gets one XChargeTransaction object from the database using the primary key.  Returns null if not found.</summary>
		public static XChargeTransaction SelectOne(long xChargeTransactionNum){
			string command="SELECT * FROM xchargetransaction "
				+"WHERE XChargeTransactionNum = "+POut.Long(xChargeTransactionNum);
			List<XChargeTransaction> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one XChargeTransaction object from the database using a query.</summary>
		public static XChargeTransaction SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<XChargeTransaction> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of XChargeTransaction objects from the database using a query.</summary>
		public static List<XChargeTransaction> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<XChargeTransaction> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<XChargeTransaction> TableToList(DataTable table){
			List<XChargeTransaction> retVal=new List<XChargeTransaction>();
			XChargeTransaction xChargeTransaction;
			foreach(DataRow row in table.Rows) {
				xChargeTransaction=new XChargeTransaction();
				xChargeTransaction.XChargeTransactionNum= PIn.Long  (row["XChargeTransactionNum"].ToString());
				xChargeTransaction.TransType            = PIn.String(row["TransType"].ToString());
				xChargeTransaction.Amount               = PIn.Double(row["Amount"].ToString());
				xChargeTransaction.CCEntry              = PIn.String(row["CCEntry"].ToString());
				xChargeTransaction.PatNum               = PIn.Long  (row["PatNum"].ToString());
				xChargeTransaction.Result               = PIn.String(row["Result"].ToString());
				xChargeTransaction.ClerkID              = PIn.String(row["ClerkID"].ToString());
				xChargeTransaction.ResultCode           = PIn.String(row["ResultCode"].ToString());
				xChargeTransaction.Expiration           = PIn.String(row["Expiration"].ToString());
				xChargeTransaction.CCType               = PIn.String(row["CCType"].ToString());
				xChargeTransaction.CreditCardNum        = PIn.String(row["CreditCardNum"].ToString());
				xChargeTransaction.BatchNum             = PIn.String(row["BatchNum"].ToString());
				xChargeTransaction.ItemNum              = PIn.String(row["ItemNum"].ToString());
				xChargeTransaction.ApprCode             = PIn.String(row["ApprCode"].ToString());
				xChargeTransaction.TransactionDateTime  = PIn.DateT (row["TransactionDateTime"].ToString());
				retVal.Add(xChargeTransaction);
			}
			return retVal;
		}

		///<summary>Inserts one XChargeTransaction into the database.  Returns the new priKey.</summary>
		public static long Insert(XChargeTransaction xChargeTransaction){
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				xChargeTransaction.XChargeTransactionNum=DbHelper.GetNextOracleKey("xchargetransaction","XChargeTransactionNum");
				int loopcount=0;
				while(loopcount<100){
					try {
						return Insert(xChargeTransaction,true);
					}
					catch(Oracle.DataAccess.Client.OracleException ex){
						if(ex.Number==1 && ex.Message.ToLower().Contains("unique constraint") && ex.Message.ToLower().Contains("violated")){
							xChargeTransaction.XChargeTransactionNum++;
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
				return Insert(xChargeTransaction,false);
			}
		}

		///<summary>Inserts one XChargeTransaction into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(XChargeTransaction xChargeTransaction,bool useExistingPK){
			if(!useExistingPK && PrefC.RandomKeys) {
				xChargeTransaction.XChargeTransactionNum=ReplicationServers.GetKey("xchargetransaction","XChargeTransactionNum");
			}
			string command="INSERT INTO xchargetransaction (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="XChargeTransactionNum,";
			}
			command+="TransType,Amount,CCEntry,PatNum,Result,ClerkID,ResultCode,Expiration,CCType,CreditCardNum,BatchNum,ItemNum,ApprCode,TransactionDateTime) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(xChargeTransaction.XChargeTransactionNum)+",";
			}
			command+=
				 "'"+POut.String(xChargeTransaction.TransType)+"',"
				+"'"+POut.Double(xChargeTransaction.Amount)+"',"
				+"'"+POut.String(xChargeTransaction.CCEntry)+"',"
				+    POut.Long  (xChargeTransaction.PatNum)+","
				+"'"+POut.String(xChargeTransaction.Result)+"',"
				+"'"+POut.String(xChargeTransaction.ClerkID)+"',"
				+"'"+POut.String(xChargeTransaction.ResultCode)+"',"
				+"'"+POut.String(xChargeTransaction.Expiration)+"',"
				+"'"+POut.String(xChargeTransaction.CCType)+"',"
				+"'"+POut.String(xChargeTransaction.CreditCardNum)+"',"
				+"'"+POut.String(xChargeTransaction.BatchNum)+"',"
				+"'"+POut.String(xChargeTransaction.ItemNum)+"',"
				+"'"+POut.String(xChargeTransaction.ApprCode)+"',"
				+    POut.DateT (xChargeTransaction.TransactionDateTime)+")";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				xChargeTransaction.XChargeTransactionNum=Db.NonQ(command,true);
			}
			return xChargeTransaction.XChargeTransactionNum;
		}

		///<summary>Inserts one XChargeTransaction into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(XChargeTransaction xChargeTransaction){
			if(DataConnection.DBtype==DatabaseType.MySql) {
				return InsertNoCache(xChargeTransaction,false);
			}
			else {
				if(DataConnection.DBtype==DatabaseType.Oracle) {
					xChargeTransaction.XChargeTransactionNum=DbHelper.GetNextOracleKey("xchargetransaction","XChargeTransactionNum"); //Cacheless method
				}
				return InsertNoCache(xChargeTransaction,true);
			}
		}

		///<summary>Inserts one XChargeTransaction into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(XChargeTransaction xChargeTransaction,bool useExistingPK){
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO xchargetransaction (";
			if(!useExistingPK && isRandomKeys) {
				xChargeTransaction.XChargeTransactionNum=ReplicationServers.GetKeyNoCache("xchargetransaction","XChargeTransactionNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="XChargeTransactionNum,";
			}
			command+="TransType,Amount,CCEntry,PatNum,Result,ClerkID,ResultCode,Expiration,CCType,CreditCardNum,BatchNum,ItemNum,ApprCode,TransactionDateTime) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(xChargeTransaction.XChargeTransactionNum)+",";
			}
			command+=
				 "'"+POut.String(xChargeTransaction.TransType)+"',"
				+"'"+POut.Double(xChargeTransaction.Amount)+"',"
				+"'"+POut.String(xChargeTransaction.CCEntry)+"',"
				+    POut.Long  (xChargeTransaction.PatNum)+","
				+"'"+POut.String(xChargeTransaction.Result)+"',"
				+"'"+POut.String(xChargeTransaction.ClerkID)+"',"
				+"'"+POut.String(xChargeTransaction.ResultCode)+"',"
				+"'"+POut.String(xChargeTransaction.Expiration)+"',"
				+"'"+POut.String(xChargeTransaction.CCType)+"',"
				+"'"+POut.String(xChargeTransaction.CreditCardNum)+"',"
				+"'"+POut.String(xChargeTransaction.BatchNum)+"',"
				+"'"+POut.String(xChargeTransaction.ItemNum)+"',"
				+"'"+POut.String(xChargeTransaction.ApprCode)+"',"
				+    POut.DateT (xChargeTransaction.TransactionDateTime)+")";
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command);
			}
			else {
				xChargeTransaction.XChargeTransactionNum=Db.NonQ(command,true);
			}
			return xChargeTransaction.XChargeTransactionNum;
		}

		///<summary>Updates one XChargeTransaction in the database.</summary>
		public static void Update(XChargeTransaction xChargeTransaction){
			string command="UPDATE xchargetransaction SET "
				+"TransType            = '"+POut.String(xChargeTransaction.TransType)+"', "
				+"Amount               = '"+POut.Double(xChargeTransaction.Amount)+"', "
				+"CCEntry              = '"+POut.String(xChargeTransaction.CCEntry)+"', "
				+"PatNum               =  "+POut.Long  (xChargeTransaction.PatNum)+", "
				+"Result               = '"+POut.String(xChargeTransaction.Result)+"', "
				+"ClerkID              = '"+POut.String(xChargeTransaction.ClerkID)+"', "
				+"ResultCode           = '"+POut.String(xChargeTransaction.ResultCode)+"', "
				+"Expiration           = '"+POut.String(xChargeTransaction.Expiration)+"', "
				+"CCType               = '"+POut.String(xChargeTransaction.CCType)+"', "
				+"CreditCardNum        = '"+POut.String(xChargeTransaction.CreditCardNum)+"', "
				+"BatchNum             = '"+POut.String(xChargeTransaction.BatchNum)+"', "
				+"ItemNum              = '"+POut.String(xChargeTransaction.ItemNum)+"', "
				+"ApprCode             = '"+POut.String(xChargeTransaction.ApprCode)+"', "
				+"TransactionDateTime  =  "+POut.DateT (xChargeTransaction.TransactionDateTime)+" "
				+"WHERE XChargeTransactionNum = "+POut.Long(xChargeTransaction.XChargeTransactionNum);
			Db.NonQ(command);
		}

		///<summary>Updates one XChargeTransaction in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(XChargeTransaction xChargeTransaction,XChargeTransaction oldXChargeTransaction){
			string command="";
			if(xChargeTransaction.TransType != oldXChargeTransaction.TransType) {
				if(command!=""){ command+=",";}
				command+="TransType = '"+POut.String(xChargeTransaction.TransType)+"'";
			}
			if(xChargeTransaction.Amount != oldXChargeTransaction.Amount) {
				if(command!=""){ command+=",";}
				command+="Amount = '"+POut.Double(xChargeTransaction.Amount)+"'";
			}
			if(xChargeTransaction.CCEntry != oldXChargeTransaction.CCEntry) {
				if(command!=""){ command+=",";}
				command+="CCEntry = '"+POut.String(xChargeTransaction.CCEntry)+"'";
			}
			if(xChargeTransaction.PatNum != oldXChargeTransaction.PatNum) {
				if(command!=""){ command+=",";}
				command+="PatNum = "+POut.Long(xChargeTransaction.PatNum)+"";
			}
			if(xChargeTransaction.Result != oldXChargeTransaction.Result) {
				if(command!=""){ command+=",";}
				command+="Result = '"+POut.String(xChargeTransaction.Result)+"'";
			}
			if(xChargeTransaction.ClerkID != oldXChargeTransaction.ClerkID) {
				if(command!=""){ command+=",";}
				command+="ClerkID = '"+POut.String(xChargeTransaction.ClerkID)+"'";
			}
			if(xChargeTransaction.ResultCode != oldXChargeTransaction.ResultCode) {
				if(command!=""){ command+=",";}
				command+="ResultCode = '"+POut.String(xChargeTransaction.ResultCode)+"'";
			}
			if(xChargeTransaction.Expiration != oldXChargeTransaction.Expiration) {
				if(command!=""){ command+=",";}
				command+="Expiration = '"+POut.String(xChargeTransaction.Expiration)+"'";
			}
			if(xChargeTransaction.CCType != oldXChargeTransaction.CCType) {
				if(command!=""){ command+=",";}
				command+="CCType = '"+POut.String(xChargeTransaction.CCType)+"'";
			}
			if(xChargeTransaction.CreditCardNum != oldXChargeTransaction.CreditCardNum) {
				if(command!=""){ command+=",";}
				command+="CreditCardNum = '"+POut.String(xChargeTransaction.CreditCardNum)+"'";
			}
			if(xChargeTransaction.BatchNum != oldXChargeTransaction.BatchNum) {
				if(command!=""){ command+=",";}
				command+="BatchNum = '"+POut.String(xChargeTransaction.BatchNum)+"'";
			}
			if(xChargeTransaction.ItemNum != oldXChargeTransaction.ItemNum) {
				if(command!=""){ command+=",";}
				command+="ItemNum = '"+POut.String(xChargeTransaction.ItemNum)+"'";
			}
			if(xChargeTransaction.ApprCode != oldXChargeTransaction.ApprCode) {
				if(command!=""){ command+=",";}
				command+="ApprCode = '"+POut.String(xChargeTransaction.ApprCode)+"'";
			}
			if(xChargeTransaction.TransactionDateTime != oldXChargeTransaction.TransactionDateTime) {
				if(command!=""){ command+=",";}
				command+="TransactionDateTime = "+POut.DateT(xChargeTransaction.TransactionDateTime)+"";
			}
			if(command==""){
				return false;
			}
			command="UPDATE xchargetransaction SET "+command
				+" WHERE XChargeTransactionNum = "+POut.Long(xChargeTransaction.XChargeTransactionNum);
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if Update(XChargeTransaction,XChargeTransaction) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(XChargeTransaction xChargeTransaction,XChargeTransaction oldXChargeTransaction) {
			if(xChargeTransaction.TransType != oldXChargeTransaction.TransType) {
				return true;
			}
			if(xChargeTransaction.Amount != oldXChargeTransaction.Amount) {
				return true;
			}
			if(xChargeTransaction.CCEntry != oldXChargeTransaction.CCEntry) {
				return true;
			}
			if(xChargeTransaction.PatNum != oldXChargeTransaction.PatNum) {
				return true;
			}
			if(xChargeTransaction.Result != oldXChargeTransaction.Result) {
				return true;
			}
			if(xChargeTransaction.ClerkID != oldXChargeTransaction.ClerkID) {
				return true;
			}
			if(xChargeTransaction.ResultCode != oldXChargeTransaction.ResultCode) {
				return true;
			}
			if(xChargeTransaction.Expiration != oldXChargeTransaction.Expiration) {
				return true;
			}
			if(xChargeTransaction.CCType != oldXChargeTransaction.CCType) {
				return true;
			}
			if(xChargeTransaction.CreditCardNum != oldXChargeTransaction.CreditCardNum) {
				return true;
			}
			if(xChargeTransaction.BatchNum != oldXChargeTransaction.BatchNum) {
				return true;
			}
			if(xChargeTransaction.ItemNum != oldXChargeTransaction.ItemNum) {
				return true;
			}
			if(xChargeTransaction.ApprCode != oldXChargeTransaction.ApprCode) {
				return true;
			}
			if(xChargeTransaction.TransactionDateTime != oldXChargeTransaction.TransactionDateTime) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one XChargeTransaction from the database.</summary>
		public static void Delete(long xChargeTransactionNum){
			string command="DELETE FROM xchargetransaction "
				+"WHERE XChargeTransactionNum = "+POut.Long(xChargeTransactionNum);
			Db.NonQ(command);
		}

	}
}