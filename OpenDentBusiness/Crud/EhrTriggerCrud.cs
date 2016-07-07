//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Crud{
	public class EhrTriggerCrud {
		///<summary>Gets one EhrTrigger object from the database using the primary key.  Returns null if not found.</summary>
		public static EhrTrigger SelectOne(long ehrTriggerNum){
			string command="SELECT * FROM ehrtrigger "
				+"WHERE EhrTriggerNum = "+POut.Long(ehrTriggerNum);
			List<EhrTrigger> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one EhrTrigger object from the database using a query.</summary>
		public static EhrTrigger SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<EhrTrigger> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of EhrTrigger objects from the database using a query.</summary>
		public static List<EhrTrigger> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<EhrTrigger> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<EhrTrigger> TableToList(DataTable table){
			List<EhrTrigger> retVal=new List<EhrTrigger>();
			EhrTrigger ehrTrigger;
			foreach(DataRow row in table.Rows) {
				ehrTrigger=new EhrTrigger();
				ehrTrigger.EhrTriggerNum    = PIn.Long  (row["EhrTriggerNum"].ToString());
				ehrTrigger.Description      = PIn.String(row["Description"].ToString());
				ehrTrigger.ProblemSnomedList= PIn.String(row["ProblemSnomedList"].ToString());
				ehrTrigger.ProblemIcd9List  = PIn.String(row["ProblemIcd9List"].ToString());
				ehrTrigger.ProblemIcd10List = PIn.String(row["ProblemIcd10List"].ToString());
				ehrTrigger.ProblemDefNumList= PIn.String(row["ProblemDefNumList"].ToString());
				ehrTrigger.MedicationNumList= PIn.String(row["MedicationNumList"].ToString());
				ehrTrigger.RxCuiList        = PIn.String(row["RxCuiList"].ToString());
				ehrTrigger.CvxList          = PIn.String(row["CvxList"].ToString());
				ehrTrigger.AllergyDefNumList= PIn.String(row["AllergyDefNumList"].ToString());
				ehrTrigger.DemographicsList = PIn.String(row["DemographicsList"].ToString());
				ehrTrigger.LabLoincList     = PIn.String(row["LabLoincList"].ToString());
				ehrTrigger.VitalLoincList   = PIn.String(row["VitalLoincList"].ToString());
				ehrTrigger.Instructions     = PIn.String(row["Instructions"].ToString());
				ehrTrigger.Bibliography     = PIn.String(row["Bibliography"].ToString());
				ehrTrigger.Cardinality      = (OpenDentBusiness.MatchCardinality)PIn.Int(row["Cardinality"].ToString());
				retVal.Add(ehrTrigger);
			}
			return retVal;
		}

		///<summary>Inserts one EhrTrigger into the database.  Returns the new priKey.</summary>
		public static long Insert(EhrTrigger ehrTrigger){
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				ehrTrigger.EhrTriggerNum=DbHelper.GetNextOracleKey("ehrtrigger","EhrTriggerNum");
				int loopcount=0;
				while(loopcount<100){
					try {
						return Insert(ehrTrigger,true);
					}
					catch(Oracle.DataAccess.Client.OracleException ex){
						if(ex.Number==1 && ex.Message.ToLower().Contains("unique constraint") && ex.Message.ToLower().Contains("violated")){
							ehrTrigger.EhrTriggerNum++;
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
				return Insert(ehrTrigger,false);
			}
		}

		///<summary>Inserts one EhrTrigger into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(EhrTrigger ehrTrigger,bool useExistingPK){
			if(!useExistingPK && PrefC.RandomKeys) {
				ehrTrigger.EhrTriggerNum=ReplicationServers.GetKey("ehrtrigger","EhrTriggerNum");
			}
			string command="INSERT INTO ehrtrigger (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="EhrTriggerNum,";
			}
			command+="Description,ProblemSnomedList,ProblemIcd9List,ProblemIcd10List,ProblemDefNumList,MedicationNumList,RxCuiList,CvxList,AllergyDefNumList,DemographicsList,LabLoincList,VitalLoincList,Instructions,Bibliography,Cardinality) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(ehrTrigger.EhrTriggerNum)+",";
			}
			command+=
				 "'"+POut.String(ehrTrigger.Description)+"',"
				+"'"+POut.String(ehrTrigger.ProblemSnomedList)+"',"
				+"'"+POut.String(ehrTrigger.ProblemIcd9List)+"',"
				+"'"+POut.String(ehrTrigger.ProblemIcd10List)+"',"
				+"'"+POut.String(ehrTrigger.ProblemDefNumList)+"',"
				+"'"+POut.String(ehrTrigger.MedicationNumList)+"',"
				+"'"+POut.String(ehrTrigger.RxCuiList)+"',"
				+"'"+POut.String(ehrTrigger.CvxList)+"',"
				+"'"+POut.String(ehrTrigger.AllergyDefNumList)+"',"
				+"'"+POut.String(ehrTrigger.DemographicsList)+"',"
				+"'"+POut.String(ehrTrigger.LabLoincList)+"',"
				+"'"+POut.String(ehrTrigger.VitalLoincList)+"',"
				+"'"+POut.String(ehrTrigger.Instructions)+"',"
				+"'"+POut.String(ehrTrigger.Bibliography)+"',"
				+    POut.Int   ((int)ehrTrigger.Cardinality)+")";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				ehrTrigger.EhrTriggerNum=Db.NonQ(command,true);
			}
			return ehrTrigger.EhrTriggerNum;
		}

		///<summary>Inserts one EhrTrigger into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(EhrTrigger ehrTrigger){
			if(DataConnection.DBtype==DatabaseType.MySql) {
				return InsertNoCache(ehrTrigger,false);
			}
			else {
				if(DataConnection.DBtype==DatabaseType.Oracle) {
					ehrTrigger.EhrTriggerNum=DbHelper.GetNextOracleKey("ehrtrigger","EhrTriggerNum"); //Cacheless method
				}
				return InsertNoCache(ehrTrigger,true);
			}
		}

		///<summary>Inserts one EhrTrigger into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(EhrTrigger ehrTrigger,bool useExistingPK){
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO ehrtrigger (";
			if(!useExistingPK && isRandomKeys) {
				ehrTrigger.EhrTriggerNum=ReplicationServers.GetKeyNoCache("ehrtrigger","EhrTriggerNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="EhrTriggerNum,";
			}
			command+="Description,ProblemSnomedList,ProblemIcd9List,ProblemIcd10List,ProblemDefNumList,MedicationNumList,RxCuiList,CvxList,AllergyDefNumList,DemographicsList,LabLoincList,VitalLoincList,Instructions,Bibliography,Cardinality) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(ehrTrigger.EhrTriggerNum)+",";
			}
			command+=
				 "'"+POut.String(ehrTrigger.Description)+"',"
				+"'"+POut.String(ehrTrigger.ProblemSnomedList)+"',"
				+"'"+POut.String(ehrTrigger.ProblemIcd9List)+"',"
				+"'"+POut.String(ehrTrigger.ProblemIcd10List)+"',"
				+"'"+POut.String(ehrTrigger.ProblemDefNumList)+"',"
				+"'"+POut.String(ehrTrigger.MedicationNumList)+"',"
				+"'"+POut.String(ehrTrigger.RxCuiList)+"',"
				+"'"+POut.String(ehrTrigger.CvxList)+"',"
				+"'"+POut.String(ehrTrigger.AllergyDefNumList)+"',"
				+"'"+POut.String(ehrTrigger.DemographicsList)+"',"
				+"'"+POut.String(ehrTrigger.LabLoincList)+"',"
				+"'"+POut.String(ehrTrigger.VitalLoincList)+"',"
				+"'"+POut.String(ehrTrigger.Instructions)+"',"
				+"'"+POut.String(ehrTrigger.Bibliography)+"',"
				+    POut.Int   ((int)ehrTrigger.Cardinality)+")";
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command);
			}
			else {
				ehrTrigger.EhrTriggerNum=Db.NonQ(command,true);
			}
			return ehrTrigger.EhrTriggerNum;
		}

		///<summary>Updates one EhrTrigger in the database.</summary>
		public static void Update(EhrTrigger ehrTrigger){
			string command="UPDATE ehrtrigger SET "
				+"Description      = '"+POut.String(ehrTrigger.Description)+"', "
				+"ProblemSnomedList= '"+POut.String(ehrTrigger.ProblemSnomedList)+"', "
				+"ProblemIcd9List  = '"+POut.String(ehrTrigger.ProblemIcd9List)+"', "
				+"ProblemIcd10List = '"+POut.String(ehrTrigger.ProblemIcd10List)+"', "
				+"ProblemDefNumList= '"+POut.String(ehrTrigger.ProblemDefNumList)+"', "
				+"MedicationNumList= '"+POut.String(ehrTrigger.MedicationNumList)+"', "
				+"RxCuiList        = '"+POut.String(ehrTrigger.RxCuiList)+"', "
				+"CvxList          = '"+POut.String(ehrTrigger.CvxList)+"', "
				+"AllergyDefNumList= '"+POut.String(ehrTrigger.AllergyDefNumList)+"', "
				+"DemographicsList = '"+POut.String(ehrTrigger.DemographicsList)+"', "
				+"LabLoincList     = '"+POut.String(ehrTrigger.LabLoincList)+"', "
				+"VitalLoincList   = '"+POut.String(ehrTrigger.VitalLoincList)+"', "
				+"Instructions     = '"+POut.String(ehrTrigger.Instructions)+"', "
				+"Bibliography     = '"+POut.String(ehrTrigger.Bibliography)+"', "
				+"Cardinality      =  "+POut.Int   ((int)ehrTrigger.Cardinality)+" "
				+"WHERE EhrTriggerNum = "+POut.Long(ehrTrigger.EhrTriggerNum);
			Db.NonQ(command);
		}

		///<summary>Updates one EhrTrigger in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(EhrTrigger ehrTrigger,EhrTrigger oldEhrTrigger){
			string command="";
			if(ehrTrigger.Description != oldEhrTrigger.Description) {
				if(command!=""){ command+=",";}
				command+="Description = '"+POut.String(ehrTrigger.Description)+"'";
			}
			if(ehrTrigger.ProblemSnomedList != oldEhrTrigger.ProblemSnomedList) {
				if(command!=""){ command+=",";}
				command+="ProblemSnomedList = '"+POut.String(ehrTrigger.ProblemSnomedList)+"'";
			}
			if(ehrTrigger.ProblemIcd9List != oldEhrTrigger.ProblemIcd9List) {
				if(command!=""){ command+=",";}
				command+="ProblemIcd9List = '"+POut.String(ehrTrigger.ProblemIcd9List)+"'";
			}
			if(ehrTrigger.ProblemIcd10List != oldEhrTrigger.ProblemIcd10List) {
				if(command!=""){ command+=",";}
				command+="ProblemIcd10List = '"+POut.String(ehrTrigger.ProblemIcd10List)+"'";
			}
			if(ehrTrigger.ProblemDefNumList != oldEhrTrigger.ProblemDefNumList) {
				if(command!=""){ command+=",";}
				command+="ProblemDefNumList = '"+POut.String(ehrTrigger.ProblemDefNumList)+"'";
			}
			if(ehrTrigger.MedicationNumList != oldEhrTrigger.MedicationNumList) {
				if(command!=""){ command+=",";}
				command+="MedicationNumList = '"+POut.String(ehrTrigger.MedicationNumList)+"'";
			}
			if(ehrTrigger.RxCuiList != oldEhrTrigger.RxCuiList) {
				if(command!=""){ command+=",";}
				command+="RxCuiList = '"+POut.String(ehrTrigger.RxCuiList)+"'";
			}
			if(ehrTrigger.CvxList != oldEhrTrigger.CvxList) {
				if(command!=""){ command+=",";}
				command+="CvxList = '"+POut.String(ehrTrigger.CvxList)+"'";
			}
			if(ehrTrigger.AllergyDefNumList != oldEhrTrigger.AllergyDefNumList) {
				if(command!=""){ command+=",";}
				command+="AllergyDefNumList = '"+POut.String(ehrTrigger.AllergyDefNumList)+"'";
			}
			if(ehrTrigger.DemographicsList != oldEhrTrigger.DemographicsList) {
				if(command!=""){ command+=",";}
				command+="DemographicsList = '"+POut.String(ehrTrigger.DemographicsList)+"'";
			}
			if(ehrTrigger.LabLoincList != oldEhrTrigger.LabLoincList) {
				if(command!=""){ command+=",";}
				command+="LabLoincList = '"+POut.String(ehrTrigger.LabLoincList)+"'";
			}
			if(ehrTrigger.VitalLoincList != oldEhrTrigger.VitalLoincList) {
				if(command!=""){ command+=",";}
				command+="VitalLoincList = '"+POut.String(ehrTrigger.VitalLoincList)+"'";
			}
			if(ehrTrigger.Instructions != oldEhrTrigger.Instructions) {
				if(command!=""){ command+=",";}
				command+="Instructions = '"+POut.String(ehrTrigger.Instructions)+"'";
			}
			if(ehrTrigger.Bibliography != oldEhrTrigger.Bibliography) {
				if(command!=""){ command+=",";}
				command+="Bibliography = '"+POut.String(ehrTrigger.Bibliography)+"'";
			}
			if(ehrTrigger.Cardinality != oldEhrTrigger.Cardinality) {
				if(command!=""){ command+=",";}
				command+="Cardinality = "+POut.Int   ((int)ehrTrigger.Cardinality)+"";
			}
			if(command==""){
				return false;
			}
			command="UPDATE ehrtrigger SET "+command
				+" WHERE EhrTriggerNum = "+POut.Long(ehrTrigger.EhrTriggerNum);
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if Update(EhrTrigger,EhrTrigger) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(EhrTrigger ehrTrigger,EhrTrigger oldEhrTrigger) {
			if(ehrTrigger.Description != oldEhrTrigger.Description) {
				return true;
			}
			if(ehrTrigger.ProblemSnomedList != oldEhrTrigger.ProblemSnomedList) {
				return true;
			}
			if(ehrTrigger.ProblemIcd9List != oldEhrTrigger.ProblemIcd9List) {
				return true;
			}
			if(ehrTrigger.ProblemIcd10List != oldEhrTrigger.ProblemIcd10List) {
				return true;
			}
			if(ehrTrigger.ProblemDefNumList != oldEhrTrigger.ProblemDefNumList) {
				return true;
			}
			if(ehrTrigger.MedicationNumList != oldEhrTrigger.MedicationNumList) {
				return true;
			}
			if(ehrTrigger.RxCuiList != oldEhrTrigger.RxCuiList) {
				return true;
			}
			if(ehrTrigger.CvxList != oldEhrTrigger.CvxList) {
				return true;
			}
			if(ehrTrigger.AllergyDefNumList != oldEhrTrigger.AllergyDefNumList) {
				return true;
			}
			if(ehrTrigger.DemographicsList != oldEhrTrigger.DemographicsList) {
				return true;
			}
			if(ehrTrigger.LabLoincList != oldEhrTrigger.LabLoincList) {
				return true;
			}
			if(ehrTrigger.VitalLoincList != oldEhrTrigger.VitalLoincList) {
				return true;
			}
			if(ehrTrigger.Instructions != oldEhrTrigger.Instructions) {
				return true;
			}
			if(ehrTrigger.Bibliography != oldEhrTrigger.Bibliography) {
				return true;
			}
			if(ehrTrigger.Cardinality != oldEhrTrigger.Cardinality) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one EhrTrigger from the database.</summary>
		public static void Delete(long ehrTriggerNum){
			string command="DELETE FROM ehrtrigger "
				+"WHERE EhrTriggerNum = "+POut.Long(ehrTriggerNum);
			Db.NonQ(command);
		}

	}
}