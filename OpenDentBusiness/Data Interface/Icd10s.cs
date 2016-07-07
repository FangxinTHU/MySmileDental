using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Icd10s{
		//If this table type will exist as cached data, uncomment the CachePattern region below.
		/*
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all Icd10s.</summary>
		private static List<Icd10> listt;

		///<summary>A list of all Icd10s.</summary>
		public static List<Icd10> Listt{
			get {
				if(listt==null) {
					RefreshCache();
				}
				return listt;
			}
			set {
				listt=value;
			}
		}

		///<summary></summary>
		public static DataTable RefreshCache(){
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM icd10 ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Icd10";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.Icd10Crud.TableToList(table);
		}
		#endregion
		*/

		///<summary></summary>
		public static long Insert(Icd10 icd10){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				icd10.Icd10Num=Meth.GetLong(MethodBase.GetCurrentMethod(),icd10);
				return icd10.Icd10Num;
			}
			return Crud.Icd10Crud.Insert(icd10);
		}

		///<summary>Returns a list of just the codes for use in update or insert logic.</summary>
		public static List<string> GetAllCodes() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<string>>(MethodBase.GetCurrentMethod());
			}
			List<string> retVal=new List<string>();
			string command="SELECT Icd10Code FROM icd10";
			DataTable table=DataCore.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++){
				retVal.Add(table.Rows[i][0].ToString());
			}
			return retVal;
		}

		///<summary>Returns the total number of ICD10 codes.  Some rows in the ICD10 table based on the IsCode column.</summary>
		public static long GetCodeCount() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetLong(MethodBase.GetCurrentMethod());
			}
			string command="SELECT COUNT(*) FROM icd10 WHERE IsCode!=0";
			return PIn.Long(Db.GetCount(command));
		}
		
		///<summary>Gets one ICD10 object directly from the database by CodeValue.  If code does not exist, returns null.</summary>
		public static Icd10 GetByCode(string Icd10Code) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Icd10>(MethodBase.GetCurrentMethod(),Icd10Code);
			}
			string command="SELECT * FROM icd10 WHERE Icd10Code='"+POut.String(Icd10Code)+"'";
			return Crud.Icd10Crud.SelectOne(command);
		}

		///<summary>Directly from db.</summary>
		public static bool CodeExists(string Icd10Code) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod(),Icd10Code);
			}
			string command="SELECT COUNT(*) FROM icd10 WHERE Icd10Code='"+POut.String(Icd10Code)+"'";
			string count=Db.GetCount(command);
			if(count=="0") {
				return false;
			}
			return true;
		}

		public static List<Icd10> GetBySearchText(string searchText) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Icd10>>(MethodBase.GetCurrentMethod(),searchText);
			}
			string[] searchTokens=searchText.Split(' ');
			string command=@"SELECT * FROM icd10 ";
			for(int i=0;i<searchTokens.Length;i++) {
				command+=(i==0?"WHERE ":"AND ")+"(Icd10Code LIKE '%"+POut.String(searchTokens[i])+"%' OR Description LIKE '%"+POut.String(searchTokens[i])+"%') ";
			}
			return Crud.Icd10Crud.SelectMany(command);
		}

		///<summary>Gets one Icd10 from the db.</summary>
		public static Icd10 GetOne(long icd10Num){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<Icd10>(MethodBase.GetCurrentMethod(),icd10Num);
			}
			return Crud.Icd10Crud.SelectOne(icd10Num);
		}

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<Icd10> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Icd10>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM icd10 WHERE PatNum = "+POut.Long(patNum);
			return Crud.Icd10Crud.SelectMany(command);
		}

		///<summary></summary>
		public static void Update(Icd10 icd10){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),icd10);
				return;
			}
			Crud.Icd10Crud.Update(icd10);
		}

		///<summary></summary>
		public static void Delete(long icd10Num) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),icd10Num);
				return;
			}
			string command= "DELETE FROM icd10 WHERE Icd10Num = "+POut.Long(icd10Num);
			Db.NonQ(command);
		}
		*/



	}
}