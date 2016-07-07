//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Crud{
	public class SmsPhoneCrud {
		///<summary>Gets one SmsPhone object from the database using the primary key.  Returns null if not found.</summary>
		public static SmsPhone SelectOne(long smsPhoneNum){
			string command="SELECT * FROM smsphone "
				+"WHERE SmsPhoneNum = "+POut.Long(smsPhoneNum);
			List<SmsPhone> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one SmsPhone object from the database using a query.</summary>
		public static SmsPhone SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<SmsPhone> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of SmsPhone objects from the database using a query.</summary>
		public static List<SmsPhone> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<SmsPhone> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<SmsPhone> TableToList(DataTable table){
			List<SmsPhone> retVal=new List<SmsPhone>();
			SmsPhone smsPhone;
			foreach(DataRow row in table.Rows) {
				smsPhone=new SmsPhone();
				smsPhone.SmsPhoneNum     = PIn.Long  (row["SmsPhoneNum"].ToString());
				smsPhone.ClinicNum       = PIn.Long  (row["ClinicNum"].ToString());
				smsPhone.PhoneNumber     = PIn.String(row["PhoneNumber"].ToString());
				smsPhone.DateTimeActive  = PIn.DateT (row["DateTimeActive"].ToString());
				smsPhone.DateTimeInactive= PIn.DateT (row["DateTimeInactive"].ToString());
				smsPhone.InactiveCode    = PIn.String(row["InactiveCode"].ToString());
				smsPhone.CountryCode     = PIn.String(row["CountryCode"].ToString());
				retVal.Add(smsPhone);
			}
			return retVal;
		}

		///<summary>Inserts one SmsPhone into the database.  Returns the new priKey.</summary>
		public static long Insert(SmsPhone smsPhone){
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				smsPhone.SmsPhoneNum=DbHelper.GetNextOracleKey("smsphone","SmsPhoneNum");
				int loopcount=0;
				while(loopcount<100){
					try {
						return Insert(smsPhone,true);
					}
					catch(Oracle.DataAccess.Client.OracleException ex){
						if(ex.Number==1 && ex.Message.ToLower().Contains("unique constraint") && ex.Message.ToLower().Contains("violated")){
							smsPhone.SmsPhoneNum++;
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
				return Insert(smsPhone,false);
			}
		}

		///<summary>Inserts one SmsPhone into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(SmsPhone smsPhone,bool useExistingPK){
			if(!useExistingPK && PrefC.RandomKeys) {
				smsPhone.SmsPhoneNum=ReplicationServers.GetKey("smsphone","SmsPhoneNum");
			}
			string command="INSERT INTO smsphone (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="SmsPhoneNum,";
			}
			command+="ClinicNum,PhoneNumber,DateTimeActive,DateTimeInactive,InactiveCode,CountryCode) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(smsPhone.SmsPhoneNum)+",";
			}
			command+=
				     POut.Long  (smsPhone.ClinicNum)+","
				+"'"+POut.String(smsPhone.PhoneNumber)+"',"
				+    POut.DateT (smsPhone.DateTimeActive)+","
				+    POut.DateT (smsPhone.DateTimeInactive)+","
				+"'"+POut.String(smsPhone.InactiveCode)+"',"
				+"'"+POut.String(smsPhone.CountryCode)+"')";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				smsPhone.SmsPhoneNum=Db.NonQ(command,true);
			}
			return smsPhone.SmsPhoneNum;
		}

		///<summary>Inserts one SmsPhone into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(SmsPhone smsPhone){
			if(DataConnection.DBtype==DatabaseType.MySql) {
				return InsertNoCache(smsPhone,false);
			}
			else {
				if(DataConnection.DBtype==DatabaseType.Oracle) {
					smsPhone.SmsPhoneNum=DbHelper.GetNextOracleKey("smsphone","SmsPhoneNum"); //Cacheless method
				}
				return InsertNoCache(smsPhone,true);
			}
		}

		///<summary>Inserts one SmsPhone into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(SmsPhone smsPhone,bool useExistingPK){
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO smsphone (";
			if(!useExistingPK && isRandomKeys) {
				smsPhone.SmsPhoneNum=ReplicationServers.GetKeyNoCache("smsphone","SmsPhoneNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="SmsPhoneNum,";
			}
			command+="ClinicNum,PhoneNumber,DateTimeActive,DateTimeInactive,InactiveCode,CountryCode) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(smsPhone.SmsPhoneNum)+",";
			}
			command+=
				     POut.Long  (smsPhone.ClinicNum)+","
				+"'"+POut.String(smsPhone.PhoneNumber)+"',"
				+    POut.DateT (smsPhone.DateTimeActive)+","
				+    POut.DateT (smsPhone.DateTimeInactive)+","
				+"'"+POut.String(smsPhone.InactiveCode)+"',"
				+"'"+POut.String(smsPhone.CountryCode)+"')";
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command);
			}
			else {
				smsPhone.SmsPhoneNum=Db.NonQ(command,true);
			}
			return smsPhone.SmsPhoneNum;
		}

		///<summary>Updates one SmsPhone in the database.</summary>
		public static void Update(SmsPhone smsPhone){
			string command="UPDATE smsphone SET "
				+"ClinicNum       =  "+POut.Long  (smsPhone.ClinicNum)+", "
				+"PhoneNumber     = '"+POut.String(smsPhone.PhoneNumber)+"', "
				+"DateTimeActive  =  "+POut.DateT (smsPhone.DateTimeActive)+", "
				+"DateTimeInactive=  "+POut.DateT (smsPhone.DateTimeInactive)+", "
				+"InactiveCode    = '"+POut.String(smsPhone.InactiveCode)+"', "
				+"CountryCode     = '"+POut.String(smsPhone.CountryCode)+"' "
				+"WHERE SmsPhoneNum = "+POut.Long(smsPhone.SmsPhoneNum);
			Db.NonQ(command);
		}

		///<summary>Updates one SmsPhone in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(SmsPhone smsPhone,SmsPhone oldSmsPhone){
			string command="";
			if(smsPhone.ClinicNum != oldSmsPhone.ClinicNum) {
				if(command!=""){ command+=",";}
				command+="ClinicNum = "+POut.Long(smsPhone.ClinicNum)+"";
			}
			if(smsPhone.PhoneNumber != oldSmsPhone.PhoneNumber) {
				if(command!=""){ command+=",";}
				command+="PhoneNumber = '"+POut.String(smsPhone.PhoneNumber)+"'";
			}
			if(smsPhone.DateTimeActive != oldSmsPhone.DateTimeActive) {
				if(command!=""){ command+=",";}
				command+="DateTimeActive = "+POut.DateT(smsPhone.DateTimeActive)+"";
			}
			if(smsPhone.DateTimeInactive != oldSmsPhone.DateTimeInactive) {
				if(command!=""){ command+=",";}
				command+="DateTimeInactive = "+POut.DateT(smsPhone.DateTimeInactive)+"";
			}
			if(smsPhone.InactiveCode != oldSmsPhone.InactiveCode) {
				if(command!=""){ command+=",";}
				command+="InactiveCode = '"+POut.String(smsPhone.InactiveCode)+"'";
			}
			if(smsPhone.CountryCode != oldSmsPhone.CountryCode) {
				if(command!=""){ command+=",";}
				command+="CountryCode = '"+POut.String(smsPhone.CountryCode)+"'";
			}
			if(command==""){
				return false;
			}
			command="UPDATE smsphone SET "+command
				+" WHERE SmsPhoneNum = "+POut.Long(smsPhone.SmsPhoneNum);
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if Update(SmsPhone,SmsPhone) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(SmsPhone smsPhone,SmsPhone oldSmsPhone) {
			if(smsPhone.ClinicNum != oldSmsPhone.ClinicNum) {
				return true;
			}
			if(smsPhone.PhoneNumber != oldSmsPhone.PhoneNumber) {
				return true;
			}
			if(smsPhone.DateTimeActive != oldSmsPhone.DateTimeActive) {
				return true;
			}
			if(smsPhone.DateTimeInactive != oldSmsPhone.DateTimeInactive) {
				return true;
			}
			if(smsPhone.InactiveCode != oldSmsPhone.InactiveCode) {
				return true;
			}
			if(smsPhone.CountryCode != oldSmsPhone.CountryCode) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one SmsPhone from the database.</summary>
		public static void Delete(long smsPhoneNum){
			string command="DELETE FROM smsphone "
				+"WHERE SmsPhoneNum = "+POut.Long(smsPhoneNum);
			Db.NonQ(command);
		}

		///<summary>Inserts, updates, or deletes database rows to match supplied list.  Returns true if db changes were made.</summary>
		public static bool Sync(List<SmsPhone> listNew,List<SmsPhone> listDB) {
			//Adding items to lists changes the order of operation. All inserts are completed first, then updates, then deletes.
			List<SmsPhone> listIns    =new List<SmsPhone>();
			List<SmsPhone> listUpdNew =new List<SmsPhone>();
			List<SmsPhone> listUpdDB  =new List<SmsPhone>();
			List<SmsPhone> listDel    =new List<SmsPhone>();
			listNew.Sort((SmsPhone x,SmsPhone y) => { return x.SmsPhoneNum.CompareTo(y.SmsPhoneNum); });//Anonymous function, sorts by compairing PK.  Lambda expressions are not allowed, this is the one and only exception.  JS approved.
			listDB.Sort((SmsPhone x,SmsPhone y) => { return x.SmsPhoneNum.CompareTo(y.SmsPhoneNum); });//Anonymous function, sorts by compairing PK.  Lambda expressions are not allowed, this is the one and only exception.  JS approved.
			int idxNew=0;
			int idxDB=0;
			int rowsUpdatedCount=0;
			SmsPhone fieldNew;
			SmsPhone fieldDB;
			//Because both lists have been sorted using the same criteria, we can now walk each list to determine which list contians the next element.  The next element is determined by Primary Key.
			//If the New list contains the next item it will be inserted.  If the DB contains the next item, it will be deleted.  If both lists contain the next item, the item will be updated.
			while(idxNew<listNew.Count || idxDB<listDB.Count) {
				fieldNew=null;
				if(idxNew<listNew.Count) {
					fieldNew=listNew[idxNew];
				}
				fieldDB=null;
				if(idxDB<listDB.Count) {
					fieldDB=listDB[idxDB];
				}
				//begin compare
				if(fieldNew!=null && fieldDB==null) {//listNew has more items, listDB does not.
					listIns.Add(fieldNew);
					idxNew++;
					continue;
				}
				else if(fieldNew==null && fieldDB!=null) {//listDB has more items, listNew does not.
					listDel.Add(fieldDB);
					idxDB++;
					continue;
				}
				else if(fieldNew.SmsPhoneNum<fieldDB.SmsPhoneNum) {//newPK less than dbPK, newItem is 'next'
					listIns.Add(fieldNew);
					idxNew++;
					continue;
				}
				else if(fieldNew.SmsPhoneNum>fieldDB.SmsPhoneNum) {//dbPK less than newPK, dbItem is 'next'
					listDel.Add(fieldDB);
					idxDB++;
					continue;
				}
				//Both lists contain the 'next' item, update required
				listUpdNew.Add(fieldNew);
				listUpdDB.Add(fieldDB);
				idxNew++;
				idxDB++;
			}
			//Commit changes to DB
			for(int i=0;i<listIns.Count;i++) {
				Insert(listIns[i]);
			}
			for(int i=0;i<listUpdNew.Count;i++) {
				if(Update(listUpdNew[i],listUpdDB[i])){
					rowsUpdatedCount++;
				}
			}
			for(int i=0;i<listDel.Count;i++) {
				Delete(listDel[i].SmsPhoneNum);
			}
			if(rowsUpdatedCount>0 || listIns.Count>0 || listDel.Count>0) {
				return true;
			}
			return false;
		}

	}
}