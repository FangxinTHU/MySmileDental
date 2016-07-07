//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Crud{
	public class VaccineDefCrud {
		///<summary>Gets one VaccineDef object from the database using the primary key.  Returns null if not found.</summary>
		public static VaccineDef SelectOne(long vaccineDefNum){
			string command="SELECT * FROM vaccinedef "
				+"WHERE VaccineDefNum = "+POut.Long(vaccineDefNum);
			List<VaccineDef> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one VaccineDef object from the database using a query.</summary>
		public static VaccineDef SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<VaccineDef> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of VaccineDef objects from the database using a query.</summary>
		public static List<VaccineDef> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<VaccineDef> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<VaccineDef> TableToList(DataTable table){
			List<VaccineDef> retVal=new List<VaccineDef>();
			VaccineDef vaccineDef;
			foreach(DataRow row in table.Rows) {
				vaccineDef=new VaccineDef();
				vaccineDef.VaccineDefNum      = PIn.Long  (row["VaccineDefNum"].ToString());
				vaccineDef.CVXCode            = PIn.String(row["CVXCode"].ToString());
				vaccineDef.VaccineName        = PIn.String(row["VaccineName"].ToString());
				vaccineDef.DrugManufacturerNum= PIn.Long  (row["DrugManufacturerNum"].ToString());
				retVal.Add(vaccineDef);
			}
			return retVal;
		}

		///<summary>Inserts one VaccineDef into the database.  Returns the new priKey.</summary>
		public static long Insert(VaccineDef vaccineDef){
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				vaccineDef.VaccineDefNum=DbHelper.GetNextOracleKey("vaccinedef","VaccineDefNum");
				int loopcount=0;
				while(loopcount<100){
					try {
						return Insert(vaccineDef,true);
					}
					catch(Oracle.DataAccess.Client.OracleException ex){
						if(ex.Number==1 && ex.Message.ToLower().Contains("unique constraint") && ex.Message.ToLower().Contains("violated")){
							vaccineDef.VaccineDefNum++;
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
				return Insert(vaccineDef,false);
			}
		}

		///<summary>Inserts one VaccineDef into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(VaccineDef vaccineDef,bool useExistingPK){
			if(!useExistingPK && PrefC.RandomKeys) {
				vaccineDef.VaccineDefNum=ReplicationServers.GetKey("vaccinedef","VaccineDefNum");
			}
			string command="INSERT INTO vaccinedef (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="VaccineDefNum,";
			}
			command+="CVXCode,VaccineName,DrugManufacturerNum) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(vaccineDef.VaccineDefNum)+",";
			}
			command+=
				 "'"+POut.String(vaccineDef.CVXCode)+"',"
				+"'"+POut.String(vaccineDef.VaccineName)+"',"
				+    POut.Long  (vaccineDef.DrugManufacturerNum)+")";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				vaccineDef.VaccineDefNum=Db.NonQ(command,true);
			}
			return vaccineDef.VaccineDefNum;
		}

		///<summary>Inserts one VaccineDef into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(VaccineDef vaccineDef){
			if(DataConnection.DBtype==DatabaseType.MySql) {
				return InsertNoCache(vaccineDef,false);
			}
			else {
				if(DataConnection.DBtype==DatabaseType.Oracle) {
					vaccineDef.VaccineDefNum=DbHelper.GetNextOracleKey("vaccinedef","VaccineDefNum"); //Cacheless method
				}
				return InsertNoCache(vaccineDef,true);
			}
		}

		///<summary>Inserts one VaccineDef into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(VaccineDef vaccineDef,bool useExistingPK){
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO vaccinedef (";
			if(!useExistingPK && isRandomKeys) {
				vaccineDef.VaccineDefNum=ReplicationServers.GetKeyNoCache("vaccinedef","VaccineDefNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="VaccineDefNum,";
			}
			command+="CVXCode,VaccineName,DrugManufacturerNum) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(vaccineDef.VaccineDefNum)+",";
			}
			command+=
				 "'"+POut.String(vaccineDef.CVXCode)+"',"
				+"'"+POut.String(vaccineDef.VaccineName)+"',"
				+    POut.Long  (vaccineDef.DrugManufacturerNum)+")";
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command);
			}
			else {
				vaccineDef.VaccineDefNum=Db.NonQ(command,true);
			}
			return vaccineDef.VaccineDefNum;
		}

		///<summary>Updates one VaccineDef in the database.</summary>
		public static void Update(VaccineDef vaccineDef){
			string command="UPDATE vaccinedef SET "
				+"CVXCode            = '"+POut.String(vaccineDef.CVXCode)+"', "
				+"VaccineName        = '"+POut.String(vaccineDef.VaccineName)+"', "
				+"DrugManufacturerNum=  "+POut.Long  (vaccineDef.DrugManufacturerNum)+" "
				+"WHERE VaccineDefNum = "+POut.Long(vaccineDef.VaccineDefNum);
			Db.NonQ(command);
		}

		///<summary>Updates one VaccineDef in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(VaccineDef vaccineDef,VaccineDef oldVaccineDef){
			string command="";
			if(vaccineDef.CVXCode != oldVaccineDef.CVXCode) {
				if(command!=""){ command+=",";}
				command+="CVXCode = '"+POut.String(vaccineDef.CVXCode)+"'";
			}
			if(vaccineDef.VaccineName != oldVaccineDef.VaccineName) {
				if(command!=""){ command+=",";}
				command+="VaccineName = '"+POut.String(vaccineDef.VaccineName)+"'";
			}
			if(vaccineDef.DrugManufacturerNum != oldVaccineDef.DrugManufacturerNum) {
				if(command!=""){ command+=",";}
				command+="DrugManufacturerNum = "+POut.Long(vaccineDef.DrugManufacturerNum)+"";
			}
			if(command==""){
				return false;
			}
			command="UPDATE vaccinedef SET "+command
				+" WHERE VaccineDefNum = "+POut.Long(vaccineDef.VaccineDefNum);
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if Update(VaccineDef,VaccineDef) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(VaccineDef vaccineDef,VaccineDef oldVaccineDef) {
			if(vaccineDef.CVXCode != oldVaccineDef.CVXCode) {
				return true;
			}
			if(vaccineDef.VaccineName != oldVaccineDef.VaccineName) {
				return true;
			}
			if(vaccineDef.DrugManufacturerNum != oldVaccineDef.DrugManufacturerNum) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one VaccineDef from the database.</summary>
		public static void Delete(long vaccineDefNum){
			string command="DELETE FROM vaccinedef "
				+"WHERE VaccineDefNum = "+POut.Long(vaccineDefNum);
			Db.NonQ(command);
		}

	}
}