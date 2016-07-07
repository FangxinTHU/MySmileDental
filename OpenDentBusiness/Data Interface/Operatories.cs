using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Operatories {
		#region CachePattern
		///<summary>Refresh all operatories</summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM operatory "
				+"ORDER BY ItemOrder";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Operatory";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			List<Operatory> listOpsShort=new List<Operatory>();
			List<Operatory> listOpsLong=Crud.OperatoryCrud.TableToList(table);
			for(int i=0;i<listOpsLong.Count;i++) {
				if(!listOpsLong[i].IsHidden) {
					listOpsShort.Add(listOpsLong[i]);
				}
			}
			OperatoryC.Listt=listOpsLong;
			OperatoryC.ListShort=listOpsShort;
		}
		#endregion

		#region Sync Pattern

		///<summary>Inserts, updates, or deletes database rows to match supplied list.</summary>
		public static void Sync(List<Operatory> listNew) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),listNew);//never pass DB list through the web service
				return;
			}
			Operatories.RefreshCache();//To guarantee freshness
			List<Operatory> listOperatories=OperatoryC.GetListt();
			Crud.OperatoryCrud.Sync(listNew,listOperatories);
		}

		#endregion

		///<summary></summary>
		public static long Insert(Operatory operatory) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				operatory.OperatoryNum=Meth.GetLong(MethodBase.GetCurrentMethod(),operatory);
				return operatory.OperatoryNum;
			}
			return Crud.OperatoryCrud.Insert(operatory);
		}

		///<summary></summary>
		public static void Update(Operatory operatory) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),operatory);
				return;
			}
			Crud.OperatoryCrud.Update(operatory);
		}

		//<summary>Checks dependencies first.  Throws exception if can't delete.</summary>
		//public void Delete(){//no such thing as delete.  Hide instead
		//}

		public static List<Operatory> GetChangedSince(DateTime changedSince) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Operatory>>(MethodBase.GetCurrentMethod(),changedSince);
			}
			string command="SELECT * FROM operatory WHERE DateTStamp > "+POut.DateT(changedSince);
			return Crud.OperatoryCrud.SelectMany(command);
		}

		public static string GetAbbrev(long operatoryNum) {
			//No need to check RemotingRole; no call to db.
			List<Operatory> listOpsLong=OperatoryC.GetListt();
			for(int i=0;i<listOpsLong.Count;i++) {
				if(listOpsLong[i].OperatoryNum==operatoryNum) {
					return listOpsLong[i].Abbrev;
				}
			}
			return "";
		}

		///<summary>Gets the order of the op within ListShort or -1 if not found.</summary>
		public static int GetOrder(long opNum) {
			//No need to check RemotingRole; no call to db.
			List<Operatory> listOpsShort=OperatoryC.GetListShort();
			for(int i=0;i<listOpsShort.Count;i++) {
				if(listOpsShort[i].OperatoryNum==opNum) {
					return i;
				}
			}
			return -1;
		}

		///<summary>Gets operatory from the cache.</summary>
		public static Operatory GetOperatory(long operatoryNum) {
			//No need to check RemotingRole; no call to db.
			List<Operatory> listOpsLong=OperatoryC.GetListt();
			for(int i=0;i<listOpsLong.Count;i++) {
				if(listOpsLong[i].OperatoryNum==operatoryNum) {
					return listOpsLong[i].Copy();
				}
			}
			return null;
		}

		///<summary>Get all non-hidden operatories for the clinic passed in.</summary>
		public static List<Operatory> GetOpsForClinic(long clinicNum) {
			//No need to check RemotingRole; no call to db.
			List<Operatory> listRetVal=new List<Operatory>();
			List<Operatory> listOpsShort=OperatoryC.GetListShort();
			for(int i=0;i<listOpsShort.Count;i++) {
				if(listOpsShort[i].ClinicNum==clinicNum) {
					listRetVal.Add(listOpsShort[i].Copy());
				}
			}
			return listRetVal;
		}

		public static List<Operatory> GetOpsForWebSched() {
			//No need to check RemotingRole; no call to db.
			List<Operatory> listOpsShort=OperatoryC.GetListShort();
			//Only return the ops flagged as IsWebSched.
			return listOpsShort.FindAll(x => x.IsWebSched);
		}

		///<summary>Gets a list of all future appointments for a given Operatory.  Ordered by dateTime</summary>
		public static bool HasFutureApts(long operatoryNum,params ApptStatus[] arrayIgnoreStatuses) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod(),operatoryNum);
			}
			string command="SELECT COUNT(*) FROM appointment "
				+"WHERE Op = "+POut.Long(operatoryNum)+" ";
			if(arrayIgnoreStatuses.Length > 0) {
				command+="AND AptStatus NOT IN (";
				for(int i=0;i<arrayIgnoreStatuses.Length;i++) {
					if(i > 0) {
						command+=",";
					}
					command+=POut.Int((int)arrayIgnoreStatuses[i]);
				}
				command+=") ";
			}
			command+="AND AptDateTime > "+DbHelper.Now();
			return PIn.Int(Db.GetScalar(command))>0;
		}
	
	}
	


}













