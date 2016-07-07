using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary>Cache pattern only used for updates.</summary>
	public class Cdcrecs{
		//If this table type will exist as cached data, uncomment the CachePattern region below.
		/*
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all Cdcrecs.</summary>
		private static List<Cdcrec> listt;

		///<summary>A list of all Cdcrecs.</summary>
		public static List<Cdcrec> Listt{
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
			string command="SELECT * FROM cdcrec ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Cdcrec";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.CdcrecCrud.TableToList(table);
		}
		#endregion
		*/

		///<summary></summary>
		public static long Insert(Cdcrec cdcrec){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				cdcrec.CdcrecNum=Meth.GetLong(MethodBase.GetCurrentMethod(),cdcrec);
				return cdcrec.CdcrecNum;
			}
			return Crud.CdcrecCrud.Insert(cdcrec);
		}

		///<summary></summary>
		public static void Update(Cdcrec cdcrec){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),cdcrec);
				return;
			}
			Crud.CdcrecCrud.Update(cdcrec);
		}

		///<summary>Returns a list of just the codes for use in update or insert logic.</summary>
		public static List<string> GetAllCodes() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<string>>(MethodBase.GetCurrentMethod());
			}
			List<string> retVal=new List<string>();
			string command="SELECT CdcRecCode FROM cdcrec";
			DataTable table=DataCore.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++){
				retVal.Add(table.Rows[i].ItemArray[0].ToString());
			}
			return retVal;
		}

		public static string GetByPatRace(PatRace patRace) {
			string retval="";
			switch(patRace) {
				case PatRace.AfricanAmerican:
					retval="2054-5";//R3 BLACK OR AFRICAN AMERICAN
					break;
				case PatRace.AmericanIndian:
					retval="1002-5";//R1 AMERICAN INDIAN OR ALASKA NATIVE
					break;
				case PatRace.Asian:
					retval="2028-9";//R2 ASIAN
					break;
				case PatRace.HawaiiOrPacIsland:
					retval="2076-8";//R4 NATIVE HAWAIIAN OR OTHER PACIFIC ISLANDER
					break;
				case PatRace.Hispanic:
					retval="2135-2";//E1 HISPANIC OR LATINO
					break;
				case PatRace.NotHispanic:
					retval="2186-5";//E2 NOT HISPANIC OR LATINO
					break;
				case PatRace.Other:
					retval="2131-1";//R9 OTHER RACE
					break;
				case PatRace.White:
					retval="2106-3";//R5 WHITE
					break;
			}
			return retval;
		}

		///<summary>Returns the total count of CDCREC codes.  CDCREC codes cannot be hidden.</summary>
		public static long GetCodeCount() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetLong(MethodBase.GetCurrentMethod());
			}
			string command="SELECT COUNT(*) FROM cdcrec";
			return PIn.Long(Db.GetCount(command));
		}

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<Cdcrec> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Cdcrec>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM cdcrec WHERE PatNum = "+POut.Long(patNum);
			return Crud.CdcrecCrud.SelectMany(command);
		}

		///<summary>Gets one Cdcrec from the db.</summary>
		public static Cdcrec GetOne(long cdcrecNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<Cdcrec>(MethodBase.GetCurrentMethod(),cdcrecNum);
			}
			return Crud.CdcrecCrud.SelectOne(cdcrecNum);
		}

		///<summary></summary>
		public static void Delete(long cdcrecNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),cdcrecNum);
				return;
			}
			string command= "DELETE FROM cdcrec WHERE CdcrecNum = "+POut.Long(cdcrecNum);
			Db.NonQ(command);
		}
		*/




	}
}