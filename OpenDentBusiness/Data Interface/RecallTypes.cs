﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class RecallTypes{
		///<summary></summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string c="SELECT * FROM recalltype ORDER BY Description";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),c);
			table.TableName="RecallType";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			List<RecallType> list=Crud.RecallTypeCrud.TableToList(table);
			//reorder rows for better usability
			List<RecallType> listRecallTypes=new List<RecallType>();
			for(int i=0;i<list.Count;i++){
				if(list[i].RecallTypeNum==PrefC.GetLong(PrefName.RecallTypeSpecialProphy)){
					listRecallTypes.Add(list[i]);
					break;
				}
			}
			for(int i=0;i<list.Count;i++){
				if(list[i].RecallTypeNum==PrefC.GetLong(PrefName.RecallTypeSpecialChildProphy)){
					listRecallTypes.Add(list[i]);
					break;
				}
			}
			for(int i=0;i<list.Count;i++){
				if(list[i].RecallTypeNum==PrefC.GetLong(PrefName.RecallTypeSpecialPerio)){
					listRecallTypes.Add(list[i]);
					break;
				}
			}
			for(int i=0;i<list.Count;i++){//now add the rest
				if(!listRecallTypes.Contains(list[i])){
					listRecallTypes.Add(list[i]);
				}
			}
			RecallTypeC.Listt=listRecallTypes;
		}

		///<summary></summary>
		public static long Insert(RecallType recallType) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				recallType.RecallTypeNum=Meth.GetLong(MethodBase.GetCurrentMethod(),recallType);
				return recallType.RecallTypeNum;
			}
			return Crud.RecallTypeCrud.Insert(recallType);
		}

		///<summary></summary>
		public static void Update(RecallType recallType) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),recallType);
				return;
			}
			Crud.RecallTypeCrud.Update(recallType);
		}

		public static string GetDescription(long recallTypeNum) {
			//No need to check RemotingRole; no call to db.
			List<RecallType> listRecallTypes=RecallTypeC.GetListt();
			if(recallTypeNum==0){
				return "";
			}
			for(int i=0;i<listRecallTypes.Count;i++){
				if(listRecallTypes[i].RecallTypeNum==recallTypeNum){
					return listRecallTypes[i].Description;
				}
			}
			return "";
		}

		public static Interval GetInterval(long recallTypeNum) {
			//No need to check RemotingRole; no call to db.
			List<RecallType> listRecallTypes=RecallTypeC.GetListt();
			if(recallTypeNum==0){
				return new Interval(0,0,0,0);
			}
			for(int i=0;i<listRecallTypes.Count;i++){
				if(listRecallTypes[i].RecallTypeNum==recallTypeNum){
					return listRecallTypes[i].DefaultInterval;
				}
			}
			return new Interval(0,0,0,0);
		}

		///<summary>Returns a collection of proccodes (D####).  Count could be zero.</summary>
		public static List<string> GetProcs(long recallTypeNum) {
			//No need to check RemotingRole; no call to db.
			List<string> retVal=new List<string>();
			List<RecallType> listRecallTypes=RecallTypeC.GetListt();
			if(recallTypeNum==0){
				return retVal;
			}
			for(int i=0;i<listRecallTypes.Count;i++){
				if(listRecallTypes[i].RecallTypeNum==recallTypeNum){
					if(listRecallTypes[i].Procedures==""){
						return retVal;
					}
					string[] strArray=listRecallTypes[i].Procedures.Split(',');
					retVal.AddRange(strArray);
					return retVal;
				}
			}
			return retVal;
		}

		///<summary>Also makes sure both types are defined as special types.</summary>
		public static bool PerioAndProphyBothHaveTriggers(){
			//No need to check RemotingRole; no call to db.
			if(RecallTypes.PerioType==0 || RecallTypes.ProphyType==0){
				return false;
			}
			if(RecallTriggers.GetForType(RecallTypes.PerioType).Count==0){
				return false;
			}
			if(RecallTriggers.GetForType(RecallTypes.ProphyType).Count==0){
				return false;
			}
			return true;
		}

		public static string GetTimePattern(long recallTypeNum) {
			//No need to check RemotingRole; no call to db.
			List<RecallType> listRecallTypes=RecallTypeC.GetListt();
			if(recallTypeNum==0){
				return "";
			}
			for(int i=0;i<listRecallTypes.Count;i++){
				if(listRecallTypes[i].RecallTypeNum==recallTypeNum){
					return listRecallTypes[i].TimePattern;
				}
			}
			return "";
		}

		///<summary>Converts the passed in time pattern into 5 minute increments.
		///The time pattern returned is altered based on the AppointmentTimeIncrement preference.
		///E.g. "/XX/" passed in with 10 minute increments set will return "//XXXX//"
		///If an empty timePattern is passed in, a default pattern of //XX// will be returned (unless using 15 min inc, then ///XXX///)</summary>
		public static string ConvertTimePattern(string timePattern) {
			//convert time pattern to 5 minute increment
			StringBuilder patternConverted=new StringBuilder();
			for(int i=0;i<timePattern.Length;i++) {
				patternConverted.Append(timePattern.Substring(i,1));
				if(PrefC.GetLong(PrefName.AppointmentTimeIncrement)==10) {
					patternConverted.Append(timePattern.Substring(i,1));
				}
				if(PrefC.GetLong(PrefName.AppointmentTimeIncrement)==15) {
					patternConverted.Append(timePattern.Substring(i,1));
					patternConverted.Append(timePattern.Substring(i,1));
				}
			}
			if(patternConverted.ToString()=="") {
				if(PrefC.GetLong(PrefName.AppointmentTimeIncrement)==15) {
					patternConverted.Append("///XXX///");
				}
				else {
					patternConverted.Append("//XX//");
				}
			}
			return patternConverted.ToString();
		}

		public static string GetSpecialTypeStr(long recallTypeNum) {
			//No need to check RemotingRole; no call to db.
			if(recallTypeNum==PrefC.GetLong(PrefName.RecallTypeSpecialProphy)){
				return Lans.g("FormRecallTypeEdit","Prophy");
			}
			if(recallTypeNum==PrefC.GetLong(PrefName.RecallTypeSpecialChildProphy)){
				return Lans.g("FormRecallTypeEdit","ChildProphy");
			}
			if(recallTypeNum==PrefC.GetLong(PrefName.RecallTypeSpecialPerio)){
				return Lans.g("FormRecallTypeEdit","Perio");
			}
			return "";
		}

		public static bool IsSpecialRecallType(long recallTypeNum) {
			//No need to check RemotingRole; no call to db.
			if(recallTypeNum==PrefC.GetLong(PrefName.RecallTypeSpecialProphy)) {
				return true;
			}
			if(recallTypeNum==PrefC.GetLong(PrefName.RecallTypeSpecialChildProphy)) {
				return true;
			}
			if(recallTypeNum==PrefC.GetLong(PrefName.RecallTypeSpecialPerio)) {
				return true;
			}
			return false;
		}

		///<summary>Gets a list of all active recall types.  Those without triggers are excluded.  Perio and Prophy are both included.  One of them should later be removed from the collection.</summary>
		public static List<RecallType> GetActive(){
			//No need to check RemotingRole; no call to db.
			List<RecallType> retVal=new List<RecallType>();
			List<RecallTrigger> triggers;
			List<RecallType> listRecallTypes=RecallTypeC.GetListt();
			for(int i=0;i<listRecallTypes.Count;i++){
				triggers=RecallTriggers.GetForType(listRecallTypes[i].RecallTypeNum);
				if(triggers.Count>0){
					retVal.Add(listRecallTypes[i].Copy());
				}
			}
			return retVal;
		}

		/*
		///<summary>Gets a list of all inactive recall types.  Only those without triggers are included.</summary>
		public static List<RecallType> GetInactive(){
			//No need to check RemotingRole; no call to db.
			List<RecallType> retVal=new List<RecallType>();
			List<RecallTrigger> triggers;
			for(int i=0;i<RecallTypeC.Listt.Count;i++){
				triggers=RecallTriggers.GetForType(RecallTypeC.Listt[i].RecallTypeNum);
				if(triggers.Count==0){
					retVal.Add(RecallTypeC.Listt[i].Clone());
				}
			}
			return retVal;
		}*/

		///<summary>Gets the pref table RecallTypeSpecialProphy RecallTypeNum.</summary>
		public static long ProphyType{
			//No need to check RemotingRole; no call to db.
			get{
				return PrefC.GetLong(PrefName.RecallTypeSpecialProphy);
			}
		}

		///<summary>Gets the pref table RecallTypeSpecialPerio RecallTypeNum.</summary>
		public static long PerioType{
			//No need to check RemotingRole; no call to db.
			get{
				return PrefC.GetLong(PrefName.RecallTypeSpecialPerio);
			}
		}

		///<summary>Gets the pref table RecallTypeSpecialChildProphy RecallTypeNum.</summary>
		public static long ChildProphyType{
			//No need to check RemotingRole; no call to db.
			get{
				return PrefC.GetLong(PrefName.RecallTypeSpecialChildProphy);
			}
		}

		/// <summary>Deletes the current recalltype and recalltrigger tables and fills them with our default.  Typically ran to switch T codes to D codes.</summary>
		public static void SetToDefault() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod());
				return;
			}
			string command="DELETE FROM recalltype WHERE RecallTypeNum >= 1 AND RecallTypeNum <= 6";//Don't delete manually added recall types
			Db.NonQ(command);
			command="INSERT INTO recalltype (RecallTypeNum,Description,DefaultInterval,TimePattern,Procedures) VALUES (1,'Prophy',393217,'/XXXX/','D0120,D1110')";
			Db.NonQ(command);
			command="INSERT INTO recalltype (RecallTypeNum,Description,DefaultInterval,TimePattern,Procedures) VALUES (2,'Child Prophy',0,'XXX','D0120,D1120,D1208')";
			Db.NonQ(command);
			command="INSERT INTO recalltype (RecallTypeNum,Description,DefaultInterval,TimePattern,Procedures) VALUES (3,'Perio',262144,'/XXXX/','D4910')";
			Db.NonQ(command);
			command="INSERT INTO recalltype (RecallTypeNum,Description,DefaultInterval,Procedures) VALUES (4,'4BW',16777216,'D0274')";
			Db.NonQ(command);
			command="INSERT INTO recalltype (RecallTypeNum,Description,DefaultInterval,Procedures) VALUES (5,'Pano',83886080,'D0330')";
			Db.NonQ(command);
			command="INSERT INTO recalltype (RecallTypeNum,Description,DefaultInterval,Procedures) VALUES (6,'FMX',83886080,'D0210')";
			Db.NonQ(command);
			command="DELETE FROM recalltrigger";//OK to delete triggers for manually added recalls, because deleting the triggers disables the recall type.
			Db.NonQ(command);
			//command="INSERT INTO recalltrigger (RecallTriggerNum,RecallTypeNum,CodeNum) VALUES (1,1,"+ProcedureCodes.GetCodeNum("D0415")+")";//collection of microorg for culture
			//Db.NonQ(command);
			command="INSERT INTO recalltrigger (RecallTriggerNum,RecallTypeNum,CodeNum) VALUES (1,1,"+ProcedureCodes.GetCodeNum("D0150")+")";
			Db.NonQ(command);
			command="INSERT INTO recalltrigger (RecallTriggerNum,RecallTypeNum,CodeNum) VALUES (2,4,"+ProcedureCodes.GetCodeNum("D0274")+")";
			Db.NonQ(command);
			command="INSERT INTO recalltrigger (RecallTriggerNum,RecallTypeNum,CodeNum) VALUES (3,5,"+ProcedureCodes.GetCodeNum("D0330")+")";
			Db.NonQ(command);
			command="INSERT INTO recalltrigger (RecallTriggerNum,RecallTypeNum,CodeNum) VALUES (4,6,"+ProcedureCodes.GetCodeNum("D0210")+")";
			Db.NonQ(command);
			command="INSERT INTO recalltrigger (RecallTriggerNum,RecallTypeNum,CodeNum) VALUES (5,1,"+ProcedureCodes.GetCodeNum("D1110")+")";
			Db.NonQ(command);
			command="INSERT INTO recalltrigger (RecallTriggerNum,RecallTypeNum,CodeNum) VALUES (6,1,"+ProcedureCodes.GetCodeNum("D1120")+")";
			Db.NonQ(command);
			command="INSERT INTO recalltrigger (RecallTriggerNum,RecallTypeNum,CodeNum) VALUES (7,3,"+ProcedureCodes.GetCodeNum("D4910")+")";
			Db.NonQ(command);
			command="INSERT INTO recalltrigger (RecallTriggerNum,RecallTypeNum,CodeNum) VALUES (8,3,"+ProcedureCodes.GetCodeNum("D4341")+")";
			Db.NonQ(command);
			command="INSERT INTO recalltrigger (RecallTriggerNum,RecallTypeNum,CodeNum) VALUES (9,1,"+ProcedureCodes.GetCodeNum("D0120")+")";
			Db.NonQ(command);
			//Update the special types in preference table.
			command="UPDATE preference SET ValueString='1' WHERE PrefName='RecallTypeSpecialProphy'";
			Db.NonQ(command);
			command="UPDATE preference SET ValueString='2' WHERE PrefName='RecallTypeSpecialChildProphy'";
			Db.NonQ(command);
			command="UPDATE preference SET ValueString='3' WHERE PrefName='RecallTypeSpecialPerio'";
			Db.NonQ(command);
			command="UPDATE preference SET ValueString='1,2,3' WHERE PrefName='RecallTypesShowingInList'";
			Db.NonQ(command);
			//Delete recalls for manually added recall types.  This is the same strategy we use in FormRecallTypeEdit
			//Types 1 through 6 were reinserted above, and thus the foreign keys will still be correct.
			command="DELETE FROM recall WHERE RecallTypeNum < 1 OR RecallTypeNum > 6";
			Db.NonQ(command);
		}

		///<summary>Returns true if any recall types that are not the default types are in use in patient recalls.</summary>
		public static bool IsUsingManuallyAddedTypes() {
			string command="SELECT COUNT(*) "
				+"FROM recall "
				+"WHERE RecallTypeNum < 1 OR RecallTypeNum > 6";//1 through 6 are the default recall types
			if(Db.GetCount(command)=="0") {
				return false;
			}
			return true;
		}



	}
}