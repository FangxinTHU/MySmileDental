//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Crud{
	public class DictCustomCrud {
		///<summary>Gets one DictCustom object from the database using the primary key.  Returns null if not found.</summary>
		public static DictCustom SelectOne(long dictCustomNum){
			string command="SELECT * FROM dictcustom "
				+"WHERE DictCustomNum = "+POut.Long(dictCustomNum);
			List<DictCustom> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one DictCustom object from the database using a query.</summary>
		public static DictCustom SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<DictCustom> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of DictCustom objects from the database using a query.</summary>
		public static List<DictCustom> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<DictCustom> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<DictCustom> TableToList(DataTable table){
			List<DictCustom> retVal=new List<DictCustom>();
			DictCustom dictCustom;
			foreach(DataRow row in table.Rows) {
				dictCustom=new DictCustom();
				dictCustom.DictCustomNum= PIn.Long  (row["DictCustomNum"].ToString());
				dictCustom.WordText     = PIn.String(row["WordText"].ToString());
				retVal.Add(dictCustom);
			}
			return retVal;
		}

		///<summary>Inserts one DictCustom into the database.  Returns the new priKey.</summary>
		public static long Insert(DictCustom dictCustom){
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				dictCustom.DictCustomNum=DbHelper.GetNextOracleKey("dictcustom","DictCustomNum");
				int loopcount=0;
				while(loopcount<100){
					try {
						return Insert(dictCustom,true);
					}
					catch(Oracle.DataAccess.Client.OracleException ex){
						if(ex.Number==1 && ex.Message.ToLower().Contains("unique constraint") && ex.Message.ToLower().Contains("violated")){
							dictCustom.DictCustomNum++;
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
				return Insert(dictCustom,false);
			}
		}

		///<summary>Inserts one DictCustom into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(DictCustom dictCustom,bool useExistingPK){
			if(!useExistingPK && PrefC.RandomKeys) {
				dictCustom.DictCustomNum=ReplicationServers.GetKey("dictcustom","DictCustomNum");
			}
			string command="INSERT INTO dictcustom (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="DictCustomNum,";
			}
			command+="WordText) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(dictCustom.DictCustomNum)+",";
			}
			command+=
				 "'"+POut.String(dictCustom.WordText)+"')";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				dictCustom.DictCustomNum=Db.NonQ(command,true);
			}
			return dictCustom.DictCustomNum;
		}

		///<summary>Inserts one DictCustom into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(DictCustom dictCustom){
			if(DataConnection.DBtype==DatabaseType.MySql) {
				return InsertNoCache(dictCustom,false);
			}
			else {
				if(DataConnection.DBtype==DatabaseType.Oracle) {
					dictCustom.DictCustomNum=DbHelper.GetNextOracleKey("dictcustom","DictCustomNum"); //Cacheless method
				}
				return InsertNoCache(dictCustom,true);
			}
		}

		///<summary>Inserts one DictCustom into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(DictCustom dictCustom,bool useExistingPK){
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO dictcustom (";
			if(!useExistingPK && isRandomKeys) {
				dictCustom.DictCustomNum=ReplicationServers.GetKeyNoCache("dictcustom","DictCustomNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="DictCustomNum,";
			}
			command+="WordText) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(dictCustom.DictCustomNum)+",";
			}
			command+=
				 "'"+POut.String(dictCustom.WordText)+"')";
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command);
			}
			else {
				dictCustom.DictCustomNum=Db.NonQ(command,true);
			}
			return dictCustom.DictCustomNum;
		}

		///<summary>Updates one DictCustom in the database.</summary>
		public static void Update(DictCustom dictCustom){
			string command="UPDATE dictcustom SET "
				+"WordText     = '"+POut.String(dictCustom.WordText)+"' "
				+"WHERE DictCustomNum = "+POut.Long(dictCustom.DictCustomNum);
			Db.NonQ(command);
		}

		///<summary>Updates one DictCustom in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(DictCustom dictCustom,DictCustom oldDictCustom){
			string command="";
			if(dictCustom.WordText != oldDictCustom.WordText) {
				if(command!=""){ command+=",";}
				command+="WordText = '"+POut.String(dictCustom.WordText)+"'";
			}
			if(command==""){
				return false;
			}
			command="UPDATE dictcustom SET "+command
				+" WHERE DictCustomNum = "+POut.Long(dictCustom.DictCustomNum);
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if Update(DictCustom,DictCustom) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(DictCustom dictCustom,DictCustom oldDictCustom) {
			if(dictCustom.WordText != oldDictCustom.WordText) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one DictCustom from the database.</summary>
		public static void Delete(long dictCustomNum){
			string command="DELETE FROM dictcustom "
				+"WHERE DictCustomNum = "+POut.Long(dictCustomNum);
			Db.NonQ(command);
		}

	}
}