//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Crud{
	public class DeletedObjectCrud {
		///<summary>Gets one DeletedObject object from the database using the primary key.  Returns null if not found.</summary>
		public static DeletedObject SelectOne(long deletedObjectNum){
			string command="SELECT * FROM deletedobject "
				+"WHERE DeletedObjectNum = "+POut.Long(deletedObjectNum);
			List<DeletedObject> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one DeletedObject object from the database using a query.</summary>
		public static DeletedObject SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<DeletedObject> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of DeletedObject objects from the database using a query.</summary>
		public static List<DeletedObject> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<DeletedObject> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<DeletedObject> TableToList(DataTable table){
			List<DeletedObject> retVal=new List<DeletedObject>();
			DeletedObject deletedObject;
			foreach(DataRow row in table.Rows) {
				deletedObject=new DeletedObject();
				deletedObject.DeletedObjectNum= PIn.Long  (row["DeletedObjectNum"].ToString());
				deletedObject.ObjectNum       = PIn.Long  (row["ObjectNum"].ToString());
				deletedObject.ObjectType      = (OpenDentBusiness.DeletedObjectType)PIn.Int(row["ObjectType"].ToString());
				deletedObject.DateTStamp      = PIn.DateT (row["DateTStamp"].ToString());
				retVal.Add(deletedObject);
			}
			return retVal;
		}

		///<summary>Inserts one DeletedObject into the database.  Returns the new priKey.</summary>
		public static long Insert(DeletedObject deletedObject){
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				deletedObject.DeletedObjectNum=DbHelper.GetNextOracleKey("deletedobject","DeletedObjectNum");
				int loopcount=0;
				while(loopcount<100){
					try {
						return Insert(deletedObject,true);
					}
					catch(Oracle.DataAccess.Client.OracleException ex){
						if(ex.Number==1 && ex.Message.ToLower().Contains("unique constraint") && ex.Message.ToLower().Contains("violated")){
							deletedObject.DeletedObjectNum++;
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
				return Insert(deletedObject,false);
			}
		}

		///<summary>Inserts one DeletedObject into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(DeletedObject deletedObject,bool useExistingPK){
			if(!useExistingPK && PrefC.RandomKeys) {
				deletedObject.DeletedObjectNum=ReplicationServers.GetKey("deletedobject","DeletedObjectNum");
			}
			string command="INSERT INTO deletedobject (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="DeletedObjectNum,";
			}
			command+="ObjectNum,ObjectType) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(deletedObject.DeletedObjectNum)+",";
			}
			command+=
				     POut.Long  (deletedObject.ObjectNum)+","
				+    POut.Int   ((int)deletedObject.ObjectType)+")";
				//DateTStamp can only be set by MySQL
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				deletedObject.DeletedObjectNum=Db.NonQ(command,true);
			}
			return deletedObject.DeletedObjectNum;
		}

		///<summary>Inserts one DeletedObject into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(DeletedObject deletedObject){
			if(DataConnection.DBtype==DatabaseType.MySql) {
				return InsertNoCache(deletedObject,false);
			}
			else {
				if(DataConnection.DBtype==DatabaseType.Oracle) {
					deletedObject.DeletedObjectNum=DbHelper.GetNextOracleKey("deletedobject","DeletedObjectNum"); //Cacheless method
				}
				return InsertNoCache(deletedObject,true);
			}
		}

		///<summary>Inserts one DeletedObject into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(DeletedObject deletedObject,bool useExistingPK){
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO deletedobject (";
			if(!useExistingPK && isRandomKeys) {
				deletedObject.DeletedObjectNum=ReplicationServers.GetKeyNoCache("deletedobject","DeletedObjectNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="DeletedObjectNum,";
			}
			command+="ObjectNum,ObjectType) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(deletedObject.DeletedObjectNum)+",";
			}
			command+=
				     POut.Long  (deletedObject.ObjectNum)+","
				+    POut.Int   ((int)deletedObject.ObjectType)+")";
				//DateTStamp can only be set by MySQL
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command);
			}
			else {
				deletedObject.DeletedObjectNum=Db.NonQ(command,true);
			}
			return deletedObject.DeletedObjectNum;
		}

		///<summary>Updates one DeletedObject in the database.</summary>
		public static void Update(DeletedObject deletedObject){
			string command="UPDATE deletedobject SET "
				+"ObjectNum       =  "+POut.Long  (deletedObject.ObjectNum)+", "
				+"ObjectType      =  "+POut.Int   ((int)deletedObject.ObjectType)+" "
				//DateTStamp can only be set by MySQL
				+"WHERE DeletedObjectNum = "+POut.Long(deletedObject.DeletedObjectNum);
			Db.NonQ(command);
		}

		///<summary>Updates one DeletedObject in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(DeletedObject deletedObject,DeletedObject oldDeletedObject){
			string command="";
			if(deletedObject.ObjectNum != oldDeletedObject.ObjectNum) {
				if(command!=""){ command+=",";}
				command+="ObjectNum = "+POut.Long(deletedObject.ObjectNum)+"";
			}
			if(deletedObject.ObjectType != oldDeletedObject.ObjectType) {
				if(command!=""){ command+=",";}
				command+="ObjectType = "+POut.Int   ((int)deletedObject.ObjectType)+"";
			}
			//DateTStamp can only be set by MySQL
			if(command==""){
				return false;
			}
			command="UPDATE deletedobject SET "+command
				+" WHERE DeletedObjectNum = "+POut.Long(deletedObject.DeletedObjectNum);
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if Update(DeletedObject,DeletedObject) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(DeletedObject deletedObject,DeletedObject oldDeletedObject) {
			if(deletedObject.ObjectNum != oldDeletedObject.ObjectNum) {
				return true;
			}
			if(deletedObject.ObjectType != oldDeletedObject.ObjectType) {
				return true;
			}
			//DateTStamp can only be set by MySQL
			return false;
		}

		///<summary>Deletes one DeletedObject from the database.</summary>
		public static void Delete(long deletedObjectNum){
			string command="DELETE FROM deletedobject "
				+"WHERE DeletedObjectNum = "+POut.Long(deletedObjectNum);
			Db.NonQ(command);
		}

	}
}