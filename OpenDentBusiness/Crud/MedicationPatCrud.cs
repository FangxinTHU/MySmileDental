//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Crud{
	public class MedicationPatCrud {
		///<summary>Gets one MedicationPat object from the database using the primary key.  Returns null if not found.</summary>
		public static MedicationPat SelectOne(long medicationPatNum){
			string command="SELECT * FROM medicationpat "
				+"WHERE MedicationPatNum = "+POut.Long(medicationPatNum);
			List<MedicationPat> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one MedicationPat object from the database using a query.</summary>
		public static MedicationPat SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<MedicationPat> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of MedicationPat objects from the database using a query.</summary>
		public static List<MedicationPat> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<MedicationPat> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<MedicationPat> TableToList(DataTable table){
			List<MedicationPat> retVal=new List<MedicationPat>();
			MedicationPat medicationPat;
			foreach(DataRow row in table.Rows) {
				medicationPat=new MedicationPat();
				medicationPat.MedicationPatNum= PIn.Long  (row["MedicationPatNum"].ToString());
				medicationPat.PatNum          = PIn.Long  (row["PatNum"].ToString());
				medicationPat.MedicationNum   = PIn.Long  (row["MedicationNum"].ToString());
				medicationPat.PatNote         = PIn.String(row["PatNote"].ToString());
				medicationPat.DateTStamp      = PIn.DateT (row["DateTStamp"].ToString());
				medicationPat.DateStart       = PIn.Date  (row["DateStart"].ToString());
				medicationPat.DateStop        = PIn.Date  (row["DateStop"].ToString());
				medicationPat.ProvNum         = PIn.Long  (row["ProvNum"].ToString());
				medicationPat.MedDescript     = PIn.String(row["MedDescript"].ToString());
				medicationPat.RxCui           = PIn.Long  (row["RxCui"].ToString());
				medicationPat.NewCropGuid     = PIn.String(row["NewCropGuid"].ToString());
				medicationPat.IsCpoe          = PIn.Bool  (row["IsCpoe"].ToString());
				retVal.Add(medicationPat);
			}
			return retVal;
		}

		///<summary>Inserts one MedicationPat into the database.  Returns the new priKey.</summary>
		public static long Insert(MedicationPat medicationPat){
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				medicationPat.MedicationPatNum=DbHelper.GetNextOracleKey("medicationpat","MedicationPatNum");
				int loopcount=0;
				while(loopcount<100){
					try {
						return Insert(medicationPat,true);
					}
					catch(Oracle.DataAccess.Client.OracleException ex){
						if(ex.Number==1 && ex.Message.ToLower().Contains("unique constraint") && ex.Message.ToLower().Contains("violated")){
							medicationPat.MedicationPatNum++;
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
				return Insert(medicationPat,false);
			}
		}

		///<summary>Inserts one MedicationPat into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(MedicationPat medicationPat,bool useExistingPK){
			if(!useExistingPK && PrefC.RandomKeys) {
				medicationPat.MedicationPatNum=ReplicationServers.GetKey("medicationpat","MedicationPatNum");
			}
			string command="INSERT INTO medicationpat (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="MedicationPatNum,";
			}
			command+="PatNum,MedicationNum,PatNote,DateStart,DateStop,ProvNum,MedDescript,RxCui,NewCropGuid,IsCpoe) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(medicationPat.MedicationPatNum)+",";
			}
			command+=
				     POut.Long  (medicationPat.PatNum)+","
				+    POut.Long  (medicationPat.MedicationNum)+","
				+"'"+POut.String(medicationPat.PatNote)+"',"
				//DateTStamp can only be set by MySQL
				+    POut.Date  (medicationPat.DateStart)+","
				+    POut.Date  (medicationPat.DateStop)+","
				+    POut.Long  (medicationPat.ProvNum)+","
				+"'"+POut.String(medicationPat.MedDescript)+"',"
				+    POut.Long  (medicationPat.RxCui)+","
				+"'"+POut.String(medicationPat.NewCropGuid)+"',"
				+    POut.Bool  (medicationPat.IsCpoe)+")";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				medicationPat.MedicationPatNum=Db.NonQ(command,true);
			}
			return medicationPat.MedicationPatNum;
		}

		///<summary>Inserts one MedicationPat into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(MedicationPat medicationPat){
			if(DataConnection.DBtype==DatabaseType.MySql) {
				return InsertNoCache(medicationPat,false);
			}
			else {
				if(DataConnection.DBtype==DatabaseType.Oracle) {
					medicationPat.MedicationPatNum=DbHelper.GetNextOracleKey("medicationpat","MedicationPatNum"); //Cacheless method
				}
				return InsertNoCache(medicationPat,true);
			}
		}

		///<summary>Inserts one MedicationPat into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(MedicationPat medicationPat,bool useExistingPK){
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO medicationpat (";
			if(!useExistingPK && isRandomKeys) {
				medicationPat.MedicationPatNum=ReplicationServers.GetKeyNoCache("medicationpat","MedicationPatNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="MedicationPatNum,";
			}
			command+="PatNum,MedicationNum,PatNote,DateStart,DateStop,ProvNum,MedDescript,RxCui,NewCropGuid,IsCpoe) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(medicationPat.MedicationPatNum)+",";
			}
			command+=
				     POut.Long  (medicationPat.PatNum)+","
				+    POut.Long  (medicationPat.MedicationNum)+","
				+"'"+POut.String(medicationPat.PatNote)+"',"
				//DateTStamp can only be set by MySQL
				+    POut.Date  (medicationPat.DateStart)+","
				+    POut.Date  (medicationPat.DateStop)+","
				+    POut.Long  (medicationPat.ProvNum)+","
				+"'"+POut.String(medicationPat.MedDescript)+"',"
				+    POut.Long  (medicationPat.RxCui)+","
				+"'"+POut.String(medicationPat.NewCropGuid)+"',"
				+    POut.Bool  (medicationPat.IsCpoe)+")";
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command);
			}
			else {
				medicationPat.MedicationPatNum=Db.NonQ(command,true);
			}
			return medicationPat.MedicationPatNum;
		}

		///<summary>Updates one MedicationPat in the database.</summary>
		public static void Update(MedicationPat medicationPat){
			string command="UPDATE medicationpat SET "
				+"PatNum          =  "+POut.Long  (medicationPat.PatNum)+", "
				+"MedicationNum   =  "+POut.Long  (medicationPat.MedicationNum)+", "
				+"PatNote         = '"+POut.String(medicationPat.PatNote)+"', "
				//DateTStamp can only be set by MySQL
				+"DateStart       =  "+POut.Date  (medicationPat.DateStart)+", "
				+"DateStop        =  "+POut.Date  (medicationPat.DateStop)+", "
				+"ProvNum         =  "+POut.Long  (medicationPat.ProvNum)+", "
				+"MedDescript     = '"+POut.String(medicationPat.MedDescript)+"', "
				+"RxCui           =  "+POut.Long  (medicationPat.RxCui)+", "
				+"NewCropGuid     = '"+POut.String(medicationPat.NewCropGuid)+"', "
				+"IsCpoe          =  "+POut.Bool  (medicationPat.IsCpoe)+" "
				+"WHERE MedicationPatNum = "+POut.Long(medicationPat.MedicationPatNum);
			Db.NonQ(command);
		}

		///<summary>Updates one MedicationPat in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(MedicationPat medicationPat,MedicationPat oldMedicationPat){
			string command="";
			if(medicationPat.PatNum != oldMedicationPat.PatNum) {
				if(command!=""){ command+=",";}
				command+="PatNum = "+POut.Long(medicationPat.PatNum)+"";
			}
			if(medicationPat.MedicationNum != oldMedicationPat.MedicationNum) {
				if(command!=""){ command+=",";}
				command+="MedicationNum = "+POut.Long(medicationPat.MedicationNum)+"";
			}
			if(medicationPat.PatNote != oldMedicationPat.PatNote) {
				if(command!=""){ command+=",";}
				command+="PatNote = '"+POut.String(medicationPat.PatNote)+"'";
			}
			//DateTStamp can only be set by MySQL
			if(medicationPat.DateStart != oldMedicationPat.DateStart) {
				if(command!=""){ command+=",";}
				command+="DateStart = "+POut.Date(medicationPat.DateStart)+"";
			}
			if(medicationPat.DateStop != oldMedicationPat.DateStop) {
				if(command!=""){ command+=",";}
				command+="DateStop = "+POut.Date(medicationPat.DateStop)+"";
			}
			if(medicationPat.ProvNum != oldMedicationPat.ProvNum) {
				if(command!=""){ command+=",";}
				command+="ProvNum = "+POut.Long(medicationPat.ProvNum)+"";
			}
			if(medicationPat.MedDescript != oldMedicationPat.MedDescript) {
				if(command!=""){ command+=",";}
				command+="MedDescript = '"+POut.String(medicationPat.MedDescript)+"'";
			}
			if(medicationPat.RxCui != oldMedicationPat.RxCui) {
				if(command!=""){ command+=",";}
				command+="RxCui = "+POut.Long(medicationPat.RxCui)+"";
			}
			if(medicationPat.NewCropGuid != oldMedicationPat.NewCropGuid) {
				if(command!=""){ command+=",";}
				command+="NewCropGuid = '"+POut.String(medicationPat.NewCropGuid)+"'";
			}
			if(medicationPat.IsCpoe != oldMedicationPat.IsCpoe) {
				if(command!=""){ command+=",";}
				command+="IsCpoe = "+POut.Bool(medicationPat.IsCpoe)+"";
			}
			if(command==""){
				return false;
			}
			command="UPDATE medicationpat SET "+command
				+" WHERE MedicationPatNum = "+POut.Long(medicationPat.MedicationPatNum);
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if Update(MedicationPat,MedicationPat) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(MedicationPat medicationPat,MedicationPat oldMedicationPat) {
			if(medicationPat.PatNum != oldMedicationPat.PatNum) {
				return true;
			}
			if(medicationPat.MedicationNum != oldMedicationPat.MedicationNum) {
				return true;
			}
			if(medicationPat.PatNote != oldMedicationPat.PatNote) {
				return true;
			}
			//DateTStamp can only be set by MySQL
			if(medicationPat.DateStart != oldMedicationPat.DateStart) {
				return true;
			}
			if(medicationPat.DateStop != oldMedicationPat.DateStop) {
				return true;
			}
			if(medicationPat.ProvNum != oldMedicationPat.ProvNum) {
				return true;
			}
			if(medicationPat.MedDescript != oldMedicationPat.MedDescript) {
				return true;
			}
			if(medicationPat.RxCui != oldMedicationPat.RxCui) {
				return true;
			}
			if(medicationPat.NewCropGuid != oldMedicationPat.NewCropGuid) {
				return true;
			}
			if(medicationPat.IsCpoe != oldMedicationPat.IsCpoe) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one MedicationPat from the database.</summary>
		public static void Delete(long medicationPatNum){
			string command="DELETE FROM medicationpat "
				+"WHERE MedicationPatNum = "+POut.Long(medicationPatNum);
			Db.NonQ(command);
		}

	}
}