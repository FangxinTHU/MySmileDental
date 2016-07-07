//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Crud{
	public class RequiredFieldCrud {
		///<summary>Gets one RequiredField object from the database using the primary key.  Returns null if not found.</summary>
		public static RequiredField SelectOne(long requiredFieldNum){
			string command="SELECT * FROM requiredfield "
				+"WHERE RequiredFieldNum = "+POut.Long(requiredFieldNum);
			List<RequiredField> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one RequiredField object from the database using a query.</summary>
		public static RequiredField SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<RequiredField> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of RequiredField objects from the database using a query.</summary>
		public static List<RequiredField> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<RequiredField> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<RequiredField> TableToList(DataTable table){
			List<RequiredField> retVal=new List<RequiredField>();
			RequiredField requiredField;
			foreach(DataRow row in table.Rows) {
				requiredField=new RequiredField();
				requiredField.RequiredFieldNum= PIn.Long  (row["RequiredFieldNum"].ToString());
				requiredField.FieldType       = (OpenDentBusiness.RequiredFieldType)PIn.Int(row["FieldType"].ToString());
				string fieldName=row["FieldName"].ToString();
				if(fieldName==""){
					requiredField.FieldName     =(RequiredFieldName)0;
				}
				else try{
					requiredField.FieldName     =(RequiredFieldName)Enum.Parse(typeof(RequiredFieldName),fieldName);
				}
				catch{
					requiredField.FieldName     =(RequiredFieldName)0;
				}
				retVal.Add(requiredField);
			}
			return retVal;
		}

		///<summary>Inserts one RequiredField into the database.  Returns the new priKey.</summary>
		public static long Insert(RequiredField requiredField){
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				requiredField.RequiredFieldNum=DbHelper.GetNextOracleKey("requiredfield","RequiredFieldNum");
				int loopcount=0;
				while(loopcount<100){
					try {
						return Insert(requiredField,true);
					}
					catch(Oracle.DataAccess.Client.OracleException ex){
						if(ex.Number==1 && ex.Message.ToLower().Contains("unique constraint") && ex.Message.ToLower().Contains("violated")){
							requiredField.RequiredFieldNum++;
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
				return Insert(requiredField,false);
			}
		}

		///<summary>Inserts one RequiredField into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(RequiredField requiredField,bool useExistingPK){
			if(!useExistingPK && PrefC.RandomKeys) {
				requiredField.RequiredFieldNum=ReplicationServers.GetKey("requiredfield","RequiredFieldNum");
			}
			string command="INSERT INTO requiredfield (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="RequiredFieldNum,";
			}
			command+="FieldType,FieldName) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(requiredField.RequiredFieldNum)+",";
			}
			command+=
				     POut.Int   ((int)requiredField.FieldType)+","
				+"'"+POut.String(requiredField.FieldName.ToString())+"')";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				requiredField.RequiredFieldNum=Db.NonQ(command,true);
			}
			return requiredField.RequiredFieldNum;
		}

		///<summary>Inserts one RequiredField into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(RequiredField requiredField){
			if(DataConnection.DBtype==DatabaseType.MySql) {
				return InsertNoCache(requiredField,false);
			}
			else {
				if(DataConnection.DBtype==DatabaseType.Oracle) {
					requiredField.RequiredFieldNum=DbHelper.GetNextOracleKey("requiredfield","RequiredFieldNum"); //Cacheless method
				}
				return InsertNoCache(requiredField,true);
			}
		}

		///<summary>Inserts one RequiredField into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(RequiredField requiredField,bool useExistingPK){
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO requiredfield (";
			if(!useExistingPK && isRandomKeys) {
				requiredField.RequiredFieldNum=ReplicationServers.GetKeyNoCache("requiredfield","RequiredFieldNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="RequiredFieldNum,";
			}
			command+="FieldType,FieldName) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(requiredField.RequiredFieldNum)+",";
			}
			command+=
				     POut.Int   ((int)requiredField.FieldType)+","
				+"'"+POut.String(requiredField.FieldName.ToString())+"')";
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command);
			}
			else {
				requiredField.RequiredFieldNum=Db.NonQ(command,true);
			}
			return requiredField.RequiredFieldNum;
		}

		///<summary>Updates one RequiredField in the database.</summary>
		public static void Update(RequiredField requiredField){
			string command="UPDATE requiredfield SET "
				+"FieldType       =  "+POut.Int   ((int)requiredField.FieldType)+", "
				+"FieldName       = '"+POut.String(requiredField.FieldName.ToString())+"' "
				+"WHERE RequiredFieldNum = "+POut.Long(requiredField.RequiredFieldNum);
			Db.NonQ(command);
		}

		///<summary>Updates one RequiredField in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(RequiredField requiredField,RequiredField oldRequiredField){
			string command="";
			if(requiredField.FieldType != oldRequiredField.FieldType) {
				if(command!=""){ command+=",";}
				command+="FieldType = "+POut.Int   ((int)requiredField.FieldType)+"";
			}
			if(requiredField.FieldName != oldRequiredField.FieldName) {
				if(command!=""){ command+=",";}
				command+="FieldName = '"+POut.String(requiredField.FieldName.ToString())+"'";
			}
			if(command==""){
				return false;
			}
			command="UPDATE requiredfield SET "+command
				+" WHERE RequiredFieldNum = "+POut.Long(requiredField.RequiredFieldNum);
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if Update(RequiredField,RequiredField) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(RequiredField requiredField,RequiredField oldRequiredField) {
			if(requiredField.FieldType != oldRequiredField.FieldType) {
				return true;
			}
			if(requiredField.FieldName != oldRequiredField.FieldName) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one RequiredField from the database.</summary>
		public static void Delete(long requiredFieldNum){
			string command="DELETE FROM requiredfield "
				+"WHERE RequiredFieldNum = "+POut.Long(requiredFieldNum);
			Db.NonQ(command);
		}

	}
}