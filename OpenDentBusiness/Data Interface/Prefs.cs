using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Prefs{
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM preference";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Pref";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			Dictionary<string,Pref> dictPrefs=new Dictionary<string,Pref>();
			Pref pref;
			//PrefName enumpn;
			//Can't use Crud.PrefCrud.TableToList(table) because it will fail the first time someone runs 7.6 before conversion.
			List<string> listDuplicatePrefs=new List<string>();
			for(int i=0;i<table.Rows.Count;i++) {
				pref=new Pref();
				if(table.Columns.Contains("PrefNum")) {
					pref.PrefNum=PIn.Long(table.Rows[i]["PrefNum"].ToString());
				}
				pref.PrefName=PIn.String(table.Rows[i]["PrefName"].ToString());
				pref.ValueString=PIn.String(table.Rows[i]["ValueString"].ToString());
				//no need to load up the comments.  Especially since this will fail when user first runs version 5.8.
				if(dictPrefs.ContainsKey(pref.PrefName)) {
					listDuplicatePrefs.Add(pref.PrefName);//The current preference is a duplicate preference.
				}
				else {
					dictPrefs.Add(pref.PrefName,pref);
				}
			}
			if(listDuplicatePrefs.Count>0 &&																				//Duplicate preferences found, and
				dictPrefs.ContainsKey(PrefName.CorruptedDatabase.ToString()) &&				//CorruptedDatabase preference exists (only v3.4+), and
				dictPrefs[PrefName.CorruptedDatabase.ToString()].ValueString!="0")		//The CorruptedDatabase flag is set.
			{
				throw new ApplicationException(Lans.g("Prefs","Your database is corrupted because an update failed.  Please contact us.  This database is unusable and you will need to restore from a backup."));
			}
			else if(listDuplicatePrefs.Count>0) {//Duplicate preferences, but the CorruptedDatabase flag is not set.
				throw new ApplicationException(Lans.g("Prefs","Duplicate preferences found in database")+": "+String.Join(",",listDuplicatePrefs));
			}
			PrefC.Dict=dictPrefs;
		}

		///<summary>Gets a pref of type bool without using the cache.</summary>
		public static bool GetBoolNoCache(PrefName prefName) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod(),prefName);
			}
			string command="SELECT ValueString FROM preference WHERE PrefName = '"+POut.String(prefName.ToString())+"'";
			return PIn.Bool(Db.GetScalar(command));
		}

		///<summary></summary>
		public static void ClearCache() {
			PrefC.Dict=null;
		}

		///<summary></summary>
		public static void Update(Pref pref) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),pref);
				return;
			}
			//Don't use CRUD here because we want to update based on PrefName instead of PrefNum.  Otherwise, it might fail the first time someone runs 7.6.
			string command= "UPDATE preference SET "
				+"ValueString = '"+POut.String(pref.ValueString)+"' "
				+" WHERE PrefName = '"+POut.String(pref.PrefName)+"'";
			Db.NonQ(command);
		}

		///<summary>Updates a pref of type int.  Returns true if a change was required, or false if no change needed.</summary>
		public static bool UpdateInt(PrefName prefName,int newValue) {
			return UpdateLong(prefName,newValue);
		}

		///<summary>Updates a pref of type byte.  Returns true if a change was required, or false if no change needed.</summary>
		public static bool UpdateByte(PrefName prefName,byte newValue) {
			return UpdateLong(prefName,newValue);
		}

		///<summary>Updates a pref of type int without using the cache.  Useful for multithreaded connections.</summary>
		public static void UpdateIntNoCache(PrefName prefName,int newValue) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),prefName,newValue);
				return;
			}
			string command="UPDATE preference SET ValueString='"+POut.Long(newValue)+"' WHERE PrefName='"+POut.String(prefName.ToString())+"'";
			Db.NonQ(command);
		}

		///<summary>Updates a pref of type long.  Returns true if a change was required, or false if no change needed.</summary>
		public static bool UpdateLong(PrefName prefName,long newValue) {
			//Very unusual.  Involves cache, so Meth is used further down instead of here at the top.
			Dictionary<string,Pref> dictPrefs=PrefC.GetDict();
			if(!dictPrefs.ContainsKey(prefName.ToString())) {
				throw new ApplicationException(prefName+" is an invalid pref name.");
			}
			if(PrefC.GetLong(prefName)==newValue) {
				return false;//no change needed
			}
			string command= "UPDATE preference SET "
				+"ValueString = '"+POut.Long(newValue)+"' "
				+"WHERE PrefName = '"+POut.String(prefName.ToString())+"'";
			bool retVal=true;
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				retVal=Meth.GetBool(MethodBase.GetCurrentMethod(),prefName,newValue);
			}
			else{
				Db.NonQ(command);
			}
			Pref pref=new Pref();
			pref.PrefName=prefName.ToString();
			pref.ValueString=newValue.ToString();
			Dictionary<string,Pref> dictPrefsUpdated=PrefC.GetDict();
			dictPrefsUpdated[prefName.ToString()]=pref;//in some cases, we just want to change the pref in local memory instead of doing a refresh afterwards.
			PrefC.Dict=dictPrefsUpdated;
			return retVal;
		}

		///<summary>Updates a pref of type double.  Returns true if a change was required, or false if no change needed.</summary>
		public static bool UpdateDouble(PrefName prefName,double newValue) {
			//Very unusual.  Involves cache, so Meth is used further down instead of here at the top.
			Dictionary<string,Pref> dictPrefs=PrefC.GetDict();
			if(!dictPrefs.ContainsKey(prefName.ToString())) {
				throw new ApplicationException(prefName+" is an invalid pref name.");
			}
			if(PrefC.GetDouble(prefName)==newValue) {
				return false;//no change needed
			}
			string command = "UPDATE preference SET "
				+"ValueString = '"+POut.Double(newValue)+"' "
				+"WHERE PrefName = '"+POut.String(prefName.ToString())+"'";
			bool retVal=true;
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				retVal=Meth.GetBool(MethodBase.GetCurrentMethod(),prefName,newValue);
			}
			else{
				Db.NonQ(command);
			}
			Pref pref=new Pref();
			pref.PrefName=prefName.ToString();
			pref.ValueString=newValue.ToString();
			Dictionary<string,Pref> dictPrefsUpdated=dictPrefs;
			dictPrefsUpdated[prefName.ToString()]=pref;
			PrefC.Dict=dictPrefsUpdated;
			return retVal;
		}

		///<summary>Returns true if a change was required, or false if no change needed.</summary>
		public static bool UpdateBool(PrefName prefName,bool newValue) {
			//No need to check RemotingRole; no call to db.
			return UpdateBool(prefName,newValue,false);
		}

		///<summary>Returns true if a change was required, or false if no change needed.</summary>
		public static bool UpdateBool(PrefName prefName,bool newValue,bool isForced) {
			//Very unusual.  Involves cache, so Meth is used further down instead of here at the top.
			Dictionary<string,Pref> dictPrefs=PrefC.GetDict();
			if(!dictPrefs.ContainsKey(prefName.ToString())) {
				throw new ApplicationException(prefName+" is an invalid pref name.");
			}
			if(!isForced && PrefC.GetBool(prefName)==newValue) {
				return false;//no change needed
			}
			string command="UPDATE preference SET "
				+"ValueString = '"+POut.Bool(newValue)+"' "
				+"WHERE PrefName = '"+POut.String(prefName.ToString())+"'";
			bool retVal=true;
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				retVal=Meth.GetBool(MethodBase.GetCurrentMethod(),prefName,newValue,isForced);
			}
			else{			
				Db.NonQ(command);
			}
			Pref pref=new Pref();
			pref.PrefName=prefName.ToString();
			pref.ValueString=POut.Bool(newValue);
			Dictionary<string,Pref> dictPrefsUpdated=PrefC.GetDict();
			dictPrefsUpdated[prefName.ToString()]=pref;
			PrefC.Dict=dictPrefsUpdated;
			return retVal;
		}

		///<summary>Updates a bool without using cache classes.  Useful for multithreaded connections.</summary>
		public static void UpdateBoolNoCache(PrefName prefName,bool newValue) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),prefName,newValue);
				return;
			}
			string command="UPDATE preference SET ValueString='"+POut.Bool(newValue)+"' WHERE PrefName='"+POut.String(prefName.ToString())+"'";
			Db.NonQ(command);
		}

		///<summary>Returns true if a change was required, or false if no change needed.</summary>
		public static bool UpdateString(PrefName prefName,string newValue) {
			//Very unusual.  Involves cache, so Meth is used further down instead of here at the top.
			Dictionary<string,Pref> dictPrefs=PrefC.GetDict();
			if(!dictPrefs.ContainsKey(prefName.ToString())) {
				throw new ApplicationException(prefName+" is an invalid pref name.");
			}
			if(PrefC.GetString(prefName)==newValue) {
				return false;//no change needed
			}
			string command = "UPDATE preference SET "
				+"ValueString = '"+POut.String(newValue)+"' "
				+"WHERE PrefName = '"+POut.String(prefName.ToString())+"'";
			bool retVal=true;
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				retVal=Meth.GetBool(MethodBase.GetCurrentMethod(),prefName,newValue);
			}
			else {
				Db.NonQ(command);
			}
			Pref pref=new Pref();
			pref.PrefName=prefName.ToString();
			pref.ValueString=newValue;
			Dictionary<string,Pref> dictPrefsUpdated=PrefC.GetDict();
			dictPrefsUpdated[prefName.ToString()]=pref;
			PrefC.Dict=dictPrefsUpdated;
			return retVal;
		}

		///<summary>Updates a pref string without using the cache classes.  Useful for multithreaded connections.</summary>
		public static void UpdateStringNoCache(PrefName prefName,string newValue) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),prefName,newValue);
				return;
			}
			string command="UPDATE preference SET ValueString='"+POut.String(newValue)+"' WHERE PrefName='"+POut.String(prefName.ToString())+"'";
			Db.NonQ(command);
		}

		///<summary>Used for prefs that are non-standard.  Especially by outside programmers. Returns true if a change was required, or false if no change needed.</summary>
		public static bool UpdateRaw(string prefName,string newValue) {
			//Very unusual.  Involves cache, so Meth is used further down instead of here at the top.
			Dictionary<string,Pref> dictPrefs=PrefC.GetDict();
			if(!dictPrefs.ContainsKey(prefName)) {
				throw new ApplicationException(prefName+" is an invalid pref name.");
			}
			if(PrefC.GetRaw(prefName)==newValue) {
				return false;//no change needed
			}
			string command = "UPDATE preference SET "
				+"ValueString = '"+POut.String(newValue)+"' "
				+"WHERE PrefName = '"+POut.String(prefName)+"'";
			bool retVal=true;
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				retVal=Meth.GetBool(MethodBase.GetCurrentMethod(),prefName,newValue);
			}
			else {
				Db.NonQ(command);
			}
			Pref pref=new Pref();
			pref.PrefName=prefName;
			pref.ValueString=newValue;
			Dictionary<string,Pref> dictPrefsUpdated=PrefC.GetDict();
			dictPrefsUpdated[prefName.ToString()]=pref;
			PrefC.Dict=dictPrefsUpdated;
			return retVal;
		}

		///<summary>Returns true if a change was required, or false if no change needed.</summary>
		public static bool UpdateDateT(PrefName prefName,DateTime newValue) {
			//Very unusual.  Involves cache, so Meth is used further down instead of here at the top.
			Dictionary<string,Pref> dictPrefs=PrefC.GetDict();
			if(!dictPrefs.ContainsKey(prefName.ToString())) {
				throw new ApplicationException(prefName+" is an invalid pref name.");
			}
			if(PrefC.GetDateT(prefName)==newValue) {
				return false;//no change needed
			}
			string command = "UPDATE preference SET "
				+"ValueString = '"+POut.DateT(newValue,false)+"' "
				+"WHERE PrefName = '"+POut.String(prefName.ToString())+"'";
			bool retVal=true;
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				retVal=Meth.GetBool(MethodBase.GetCurrentMethod(),prefName,newValue);
			}
			else{
				Db.NonQ(command);
			}
			Pref pref=new Pref();
			pref.PrefName=prefName.ToString();
			pref.ValueString=POut.DateT(newValue,false);
			Dictionary<string,Pref> dictPrefsUpdated=PrefC.GetDict();
			dictPrefsUpdated[prefName.ToString()]=pref;//in some cases, we just want to change the pref in local memory instead of doing a refresh afterwards.
			PrefC.Dict=dictPrefsUpdated;
			return retVal;
		}

		///<summary>Only run from PrefL.CheckMySqlVersion41().</summary>
		public static void ConvertToMySqlVersion41() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod());
				return;
			}
			string command="SHOW FULL TABLES WHERE Table_type='BASE TABLE'";//Tables, not views.  Does not work in MySQL 4.1, however we test for MySQL version >= 5.0 in PrefL.
			DataTable table=Db.GetTable(command);//not MySQL 4.1 compatible. Should not be a problem if following reccomended update process.
			string[] tableNames=new string[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				tableNames[i]=table.Rows[i][0].ToString();
			}
			for(int i=0;i<tableNames.Length;i++) {
				if(tableNames[i]!="procedurecode") {
					command="ALTER TABLE "+tableNames[i]+" CONVERT TO CHARACTER SET utf8";
					Db.NonQ(command);
				}
			}
			string[] commands=new string[]
				{
					//"ALTER TABLE procedurecode CHANGE OldCode OldCode varchar(15) character set utf8 collate utf8_bin NOT NULL"
					//,"ALTER TABLE procedurecode DEFAULT character set utf8"
					"ALTER TABLE procedurecode MODIFY Descript varchar(255) character set utf8 NOT NULL"
					,"ALTER TABLE procedurecode MODIFY AbbrDesc varchar(50) character set utf8 NOT NULL"
					,"ALTER TABLE procedurecode MODIFY ProcTime varchar(24) character set utf8 NOT NULL"
					,"ALTER TABLE procedurecode MODIFY DefaultNote text character set utf8 NOT NULL"
					,"ALTER TABLE procedurecode MODIFY AlternateCode1 varchar(15) character set utf8 NOT NULL"
					//,"ALTER TABLE procedurelog MODIFY OldCode varchar(15) character set utf8 collate utf8_bin NOT NULL"
					//,"ALTER TABLE autocodeitem MODIFY OldCode varchar(15) character set utf8 collate utf8_bin NOT NULL"
					//,"ALTER TABLE procbuttonitem MODIFY OldCode varchar(15) character set utf8 collate utf8_bin NOT NULL"
					//,"ALTER TABLE covspan MODIFY FromCode varchar(15) character set utf8 collate utf8_bin NOT NULL"
					//,"ALTER TABLE covspan MODIFY ToCode varchar(15) character set utf8 collate utf8_bin NOT NULL"
					//,"ALTER TABLE fee MODIFY OldCode varchar(15) character set utf8 collate utf8_bin NOT NULL"
				};
			Db.NonQ(commands);
			//and set the default too
			command="ALTER DATABASE CHARACTER SET utf8";
			Db.NonQ(command);
			command="INSERT INTO preference VALUES('DatabaseConvertedForMySql41','1')";
			Db.NonQ(command);
		}

		///<summary>Gets a Pref object when the PrefName is provided</summary>
		public static Pref GetPref(String PrefName) {
			Dictionary<string,Pref> dictPrefs=PrefC.GetDict();
			Pref pref=dictPrefs[PrefName];
			return pref;
		}

		///<summary>Returns true if DockPhonePanelShow is enabled. Convenience function that should be used if for ODHQ only, and not resellers.</summary>
		/// <returns></returns>
		public static bool IsODHQ() {
			if(PrefC.GetBool(PrefName.DockPhonePanelShow)){
				return true;
			}
			return false;
		}

	}

	


	


}










