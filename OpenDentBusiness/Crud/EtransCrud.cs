//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Crud{
	public class EtransCrud {
		///<summary>Gets one Etrans object from the database using the primary key.  Returns null if not found.</summary>
		public static Etrans SelectOne(long etransNum){
			string command="SELECT * FROM etrans "
				+"WHERE EtransNum = "+POut.Long(etransNum);
			List<Etrans> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one Etrans object from the database using a query.</summary>
		public static Etrans SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<Etrans> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of Etrans objects from the database using a query.</summary>
		public static List<Etrans> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<Etrans> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<Etrans> TableToList(DataTable table){
			List<Etrans> retVal=new List<Etrans>();
			Etrans etrans;
			foreach(DataRow row in table.Rows) {
				etrans=new Etrans();
				etrans.EtransNum           = PIn.Long  (row["EtransNum"].ToString());
				etrans.DateTimeTrans       = PIn.DateT (row["DateTimeTrans"].ToString());
				etrans.ClearingHouseNum    = PIn.Long  (row["ClearingHouseNum"].ToString());
				etrans.Etype               = (OpenDentBusiness.EtransType)PIn.Int(row["Etype"].ToString());
				etrans.ClaimNum            = PIn.Long  (row["ClaimNum"].ToString());
				etrans.OfficeSequenceNumber= PIn.Int   (row["OfficeSequenceNumber"].ToString());
				etrans.CarrierTransCounter = PIn.Int   (row["CarrierTransCounter"].ToString());
				etrans.CarrierTransCounter2= PIn.Int   (row["CarrierTransCounter2"].ToString());
				etrans.CarrierNum          = PIn.Long  (row["CarrierNum"].ToString());
				etrans.CarrierNum2         = PIn.Long  (row["CarrierNum2"].ToString());
				etrans.PatNum              = PIn.Long  (row["PatNum"].ToString());
				etrans.BatchNumber         = PIn.Int   (row["BatchNumber"].ToString());
				etrans.AckCode             = PIn.String(row["AckCode"].ToString());
				etrans.TransSetNum         = PIn.Int   (row["TransSetNum"].ToString());
				etrans.Note                = PIn.String(row["Note"].ToString());
				etrans.EtransMessageTextNum= PIn.Long  (row["EtransMessageTextNum"].ToString());
				etrans.AckEtransNum        = PIn.Long  (row["AckEtransNum"].ToString());
				etrans.PlanNum             = PIn.Long  (row["PlanNum"].ToString());
				etrans.InsSubNum           = PIn.Long  (row["InsSubNum"].ToString());
				etrans.TranSetId835        = PIn.String(row["TranSetId835"].ToString());
				etrans.CarrierNameRaw      = PIn.String(row["CarrierNameRaw"].ToString());
				etrans.PatientNameRaw      = PIn.String(row["PatientNameRaw"].ToString());
				retVal.Add(etrans);
			}
			return retVal;
		}

		///<summary>Inserts one Etrans into the database.  Returns the new priKey.</summary>
		public static long Insert(Etrans etrans){
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				etrans.EtransNum=DbHelper.GetNextOracleKey("etrans","EtransNum");
				int loopcount=0;
				while(loopcount<100){
					try {
						return Insert(etrans,true);
					}
					catch(Oracle.DataAccess.Client.OracleException ex){
						if(ex.Number==1 && ex.Message.ToLower().Contains("unique constraint") && ex.Message.ToLower().Contains("violated")){
							etrans.EtransNum++;
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
				return Insert(etrans,false);
			}
		}

		///<summary>Inserts one Etrans into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(Etrans etrans,bool useExistingPK){
			if(!useExistingPK && PrefC.RandomKeys) {
				etrans.EtransNum=ReplicationServers.GetKey("etrans","EtransNum");
			}
			string command="INSERT INTO etrans (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="EtransNum,";
			}
			command+="DateTimeTrans,ClearingHouseNum,Etype,ClaimNum,OfficeSequenceNumber,CarrierTransCounter,CarrierTransCounter2,CarrierNum,CarrierNum2,PatNum,BatchNumber,AckCode,TransSetNum,Note,EtransMessageTextNum,AckEtransNum,PlanNum,InsSubNum,TranSetId835,CarrierNameRaw,PatientNameRaw) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(etrans.EtransNum)+",";
			}
			command+=
				     DbHelper.Now()+","
				+    POut.Long  (etrans.ClearingHouseNum)+","
				+    POut.Int   ((int)etrans.Etype)+","
				+    POut.Long  (etrans.ClaimNum)+","
				+    POut.Int   (etrans.OfficeSequenceNumber)+","
				+    POut.Int   (etrans.CarrierTransCounter)+","
				+    POut.Int   (etrans.CarrierTransCounter2)+","
				+    POut.Long  (etrans.CarrierNum)+","
				+    POut.Long  (etrans.CarrierNum2)+","
				+    POut.Long  (etrans.PatNum)+","
				+    POut.Int   (etrans.BatchNumber)+","
				+"'"+POut.String(etrans.AckCode)+"',"
				+    POut.Int   (etrans.TransSetNum)+","
				+"'"+POut.String(etrans.Note)+"',"
				+    POut.Long  (etrans.EtransMessageTextNum)+","
				+    POut.Long  (etrans.AckEtransNum)+","
				+    POut.Long  (etrans.PlanNum)+","
				+    POut.Long  (etrans.InsSubNum)+","
				+"'"+POut.String(etrans.TranSetId835)+"',"
				+"'"+POut.String(etrans.CarrierNameRaw)+"',"
				+"'"+POut.String(etrans.PatientNameRaw)+"')";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				etrans.EtransNum=Db.NonQ(command,true);
			}
			return etrans.EtransNum;
		}

		///<summary>Inserts one Etrans into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(Etrans etrans){
			if(DataConnection.DBtype==DatabaseType.MySql) {
				return InsertNoCache(etrans,false);
			}
			else {
				if(DataConnection.DBtype==DatabaseType.Oracle) {
					etrans.EtransNum=DbHelper.GetNextOracleKey("etrans","EtransNum"); //Cacheless method
				}
				return InsertNoCache(etrans,true);
			}
		}

		///<summary>Inserts one Etrans into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(Etrans etrans,bool useExistingPK){
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO etrans (";
			if(!useExistingPK && isRandomKeys) {
				etrans.EtransNum=ReplicationServers.GetKeyNoCache("etrans","EtransNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="EtransNum,";
			}
			command+="DateTimeTrans,ClearingHouseNum,Etype,ClaimNum,OfficeSequenceNumber,CarrierTransCounter,CarrierTransCounter2,CarrierNum,CarrierNum2,PatNum,BatchNumber,AckCode,TransSetNum,Note,EtransMessageTextNum,AckEtransNum,PlanNum,InsSubNum,TranSetId835,CarrierNameRaw,PatientNameRaw) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(etrans.EtransNum)+",";
			}
			command+=
				     DbHelper.Now()+","
				+    POut.Long  (etrans.ClearingHouseNum)+","
				+    POut.Int   ((int)etrans.Etype)+","
				+    POut.Long  (etrans.ClaimNum)+","
				+    POut.Int   (etrans.OfficeSequenceNumber)+","
				+    POut.Int   (etrans.CarrierTransCounter)+","
				+    POut.Int   (etrans.CarrierTransCounter2)+","
				+    POut.Long  (etrans.CarrierNum)+","
				+    POut.Long  (etrans.CarrierNum2)+","
				+    POut.Long  (etrans.PatNum)+","
				+    POut.Int   (etrans.BatchNumber)+","
				+"'"+POut.String(etrans.AckCode)+"',"
				+    POut.Int   (etrans.TransSetNum)+","
				+"'"+POut.String(etrans.Note)+"',"
				+    POut.Long  (etrans.EtransMessageTextNum)+","
				+    POut.Long  (etrans.AckEtransNum)+","
				+    POut.Long  (etrans.PlanNum)+","
				+    POut.Long  (etrans.InsSubNum)+","
				+"'"+POut.String(etrans.TranSetId835)+"',"
				+"'"+POut.String(etrans.CarrierNameRaw)+"',"
				+"'"+POut.String(etrans.PatientNameRaw)+"')";
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command);
			}
			else {
				etrans.EtransNum=Db.NonQ(command,true);
			}
			return etrans.EtransNum;
		}

		///<summary>Updates one Etrans in the database.</summary>
		public static void Update(Etrans etrans){
			string command="UPDATE etrans SET "
				+"DateTimeTrans       =  "+POut.DateT (etrans.DateTimeTrans)+", "
				+"ClearingHouseNum    =  "+POut.Long  (etrans.ClearingHouseNum)+", "
				+"Etype               =  "+POut.Int   ((int)etrans.Etype)+", "
				+"ClaimNum            =  "+POut.Long  (etrans.ClaimNum)+", "
				+"OfficeSequenceNumber=  "+POut.Int   (etrans.OfficeSequenceNumber)+", "
				+"CarrierTransCounter =  "+POut.Int   (etrans.CarrierTransCounter)+", "
				+"CarrierTransCounter2=  "+POut.Int   (etrans.CarrierTransCounter2)+", "
				+"CarrierNum          =  "+POut.Long  (etrans.CarrierNum)+", "
				+"CarrierNum2         =  "+POut.Long  (etrans.CarrierNum2)+", "
				+"PatNum              =  "+POut.Long  (etrans.PatNum)+", "
				+"BatchNumber         =  "+POut.Int   (etrans.BatchNumber)+", "
				+"AckCode             = '"+POut.String(etrans.AckCode)+"', "
				+"TransSetNum         =  "+POut.Int   (etrans.TransSetNum)+", "
				+"Note                = '"+POut.String(etrans.Note)+"', "
				+"EtransMessageTextNum=  "+POut.Long  (etrans.EtransMessageTextNum)+", "
				+"AckEtransNum        =  "+POut.Long  (etrans.AckEtransNum)+", "
				+"PlanNum             =  "+POut.Long  (etrans.PlanNum)+", "
				+"InsSubNum           =  "+POut.Long  (etrans.InsSubNum)+", "
				+"TranSetId835        = '"+POut.String(etrans.TranSetId835)+"', "
				+"CarrierNameRaw      = '"+POut.String(etrans.CarrierNameRaw)+"', "
				+"PatientNameRaw      = '"+POut.String(etrans.PatientNameRaw)+"' "
				+"WHERE EtransNum = "+POut.Long(etrans.EtransNum);
			Db.NonQ(command);
		}

		///<summary>Updates one Etrans in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(Etrans etrans,Etrans oldEtrans){
			string command="";
			if(etrans.DateTimeTrans != oldEtrans.DateTimeTrans) {
				if(command!=""){ command+=",";}
				command+="DateTimeTrans = "+POut.DateT(etrans.DateTimeTrans)+"";
			}
			if(etrans.ClearingHouseNum != oldEtrans.ClearingHouseNum) {
				if(command!=""){ command+=",";}
				command+="ClearingHouseNum = "+POut.Long(etrans.ClearingHouseNum)+"";
			}
			if(etrans.Etype != oldEtrans.Etype) {
				if(command!=""){ command+=",";}
				command+="Etype = "+POut.Int   ((int)etrans.Etype)+"";
			}
			if(etrans.ClaimNum != oldEtrans.ClaimNum) {
				if(command!=""){ command+=",";}
				command+="ClaimNum = "+POut.Long(etrans.ClaimNum)+"";
			}
			if(etrans.OfficeSequenceNumber != oldEtrans.OfficeSequenceNumber) {
				if(command!=""){ command+=",";}
				command+="OfficeSequenceNumber = "+POut.Int(etrans.OfficeSequenceNumber)+"";
			}
			if(etrans.CarrierTransCounter != oldEtrans.CarrierTransCounter) {
				if(command!=""){ command+=",";}
				command+="CarrierTransCounter = "+POut.Int(etrans.CarrierTransCounter)+"";
			}
			if(etrans.CarrierTransCounter2 != oldEtrans.CarrierTransCounter2) {
				if(command!=""){ command+=",";}
				command+="CarrierTransCounter2 = "+POut.Int(etrans.CarrierTransCounter2)+"";
			}
			if(etrans.CarrierNum != oldEtrans.CarrierNum) {
				if(command!=""){ command+=",";}
				command+="CarrierNum = "+POut.Long(etrans.CarrierNum)+"";
			}
			if(etrans.CarrierNum2 != oldEtrans.CarrierNum2) {
				if(command!=""){ command+=",";}
				command+="CarrierNum2 = "+POut.Long(etrans.CarrierNum2)+"";
			}
			if(etrans.PatNum != oldEtrans.PatNum) {
				if(command!=""){ command+=",";}
				command+="PatNum = "+POut.Long(etrans.PatNum)+"";
			}
			if(etrans.BatchNumber != oldEtrans.BatchNumber) {
				if(command!=""){ command+=",";}
				command+="BatchNumber = "+POut.Int(etrans.BatchNumber)+"";
			}
			if(etrans.AckCode != oldEtrans.AckCode) {
				if(command!=""){ command+=",";}
				command+="AckCode = '"+POut.String(etrans.AckCode)+"'";
			}
			if(etrans.TransSetNum != oldEtrans.TransSetNum) {
				if(command!=""){ command+=",";}
				command+="TransSetNum = "+POut.Int(etrans.TransSetNum)+"";
			}
			if(etrans.Note != oldEtrans.Note) {
				if(command!=""){ command+=",";}
				command+="Note = '"+POut.String(etrans.Note)+"'";
			}
			if(etrans.EtransMessageTextNum != oldEtrans.EtransMessageTextNum) {
				if(command!=""){ command+=",";}
				command+="EtransMessageTextNum = "+POut.Long(etrans.EtransMessageTextNum)+"";
			}
			if(etrans.AckEtransNum != oldEtrans.AckEtransNum) {
				if(command!=""){ command+=",";}
				command+="AckEtransNum = "+POut.Long(etrans.AckEtransNum)+"";
			}
			if(etrans.PlanNum != oldEtrans.PlanNum) {
				if(command!=""){ command+=",";}
				command+="PlanNum = "+POut.Long(etrans.PlanNum)+"";
			}
			if(etrans.InsSubNum != oldEtrans.InsSubNum) {
				if(command!=""){ command+=",";}
				command+="InsSubNum = "+POut.Long(etrans.InsSubNum)+"";
			}
			if(etrans.TranSetId835 != oldEtrans.TranSetId835) {
				if(command!=""){ command+=",";}
				command+="TranSetId835 = '"+POut.String(etrans.TranSetId835)+"'";
			}
			if(etrans.CarrierNameRaw != oldEtrans.CarrierNameRaw) {
				if(command!=""){ command+=",";}
				command+="CarrierNameRaw = '"+POut.String(etrans.CarrierNameRaw)+"'";
			}
			if(etrans.PatientNameRaw != oldEtrans.PatientNameRaw) {
				if(command!=""){ command+=",";}
				command+="PatientNameRaw = '"+POut.String(etrans.PatientNameRaw)+"'";
			}
			if(command==""){
				return false;
			}
			command="UPDATE etrans SET "+command
				+" WHERE EtransNum = "+POut.Long(etrans.EtransNum);
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if Update(Etrans,Etrans) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(Etrans etrans,Etrans oldEtrans) {
			if(etrans.DateTimeTrans != oldEtrans.DateTimeTrans) {
				return true;
			}
			if(etrans.ClearingHouseNum != oldEtrans.ClearingHouseNum) {
				return true;
			}
			if(etrans.Etype != oldEtrans.Etype) {
				return true;
			}
			if(etrans.ClaimNum != oldEtrans.ClaimNum) {
				return true;
			}
			if(etrans.OfficeSequenceNumber != oldEtrans.OfficeSequenceNumber) {
				return true;
			}
			if(etrans.CarrierTransCounter != oldEtrans.CarrierTransCounter) {
				return true;
			}
			if(etrans.CarrierTransCounter2 != oldEtrans.CarrierTransCounter2) {
				return true;
			}
			if(etrans.CarrierNum != oldEtrans.CarrierNum) {
				return true;
			}
			if(etrans.CarrierNum2 != oldEtrans.CarrierNum2) {
				return true;
			}
			if(etrans.PatNum != oldEtrans.PatNum) {
				return true;
			}
			if(etrans.BatchNumber != oldEtrans.BatchNumber) {
				return true;
			}
			if(etrans.AckCode != oldEtrans.AckCode) {
				return true;
			}
			if(etrans.TransSetNum != oldEtrans.TransSetNum) {
				return true;
			}
			if(etrans.Note != oldEtrans.Note) {
				return true;
			}
			if(etrans.EtransMessageTextNum != oldEtrans.EtransMessageTextNum) {
				return true;
			}
			if(etrans.AckEtransNum != oldEtrans.AckEtransNum) {
				return true;
			}
			if(etrans.PlanNum != oldEtrans.PlanNum) {
				return true;
			}
			if(etrans.InsSubNum != oldEtrans.InsSubNum) {
				return true;
			}
			if(etrans.TranSetId835 != oldEtrans.TranSetId835) {
				return true;
			}
			if(etrans.CarrierNameRaw != oldEtrans.CarrierNameRaw) {
				return true;
			}
			if(etrans.PatientNameRaw != oldEtrans.PatientNameRaw) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one Etrans from the database.</summary>
		public static void Delete(long etransNum){
			string command="DELETE FROM etrans "
				+"WHERE EtransNum = "+POut.Long(etransNum);
			Db.NonQ(command);
		}

	}
}