using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Signalods {
		///<summary>Gets all Signals and Acks Since a given DateTime.  If it can't connect to the database, then it no longer throws an error, but instead returns a list of length 0.  Remeber that the supplied dateTime is server time.  This has to be accounted for.</summary>
		public static List<Signalod> RefreshTimed(DateTime sinceDateT) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Signalod>>(MethodBase.GetCurrentMethod(),sinceDateT);
			}
			//This command was written to take into account the fact that MySQL truncates seconds to the the whole second on DateTime columns. (newer versions support fractional seconds)
			//By selecting signals less than Now() we avoid missing signals the next time this function is called. Without the addition of Now() it was possible
			//to miss up to ((N-1)/N)% of the signals generated in the worst case scenario.
			string command="SELECT * FROM signalod "
				+"WHERE (SigDateTime>"+POut.DateT(sinceDateT)+" AND SigDateTime< "+DbHelper.Now()+") "
				+"OR (AckTime>"+POut.DateT(sinceDateT)+" AND AckTime< "+DbHelper.Now()+") "
				+"ORDER BY SigDateTime";
			//note: this might return an occasional row that has both times newer.
			List<Signalod> sigList=new List<Signalod>();
			try {
				sigList=Crud.SignalodCrud.SelectMany(command);
				sigList.Sort();
			} 
			catch {
				//we don't want an error message to show, because that can cause a cascade of a large number of error messages.
			}
			SigElement[] sigElementsAll=SigElements.GetElements(sigList);
			for(int i=0;i<sigList.Count;i++) {
				sigList[i].ElementList=SigElements.GetForSig(sigElementsAll,sigList[i].SignalNum);
			}
			return sigList;
		}

		///<summary>Process all Signals and Acks Since a given DateTime.  Only to be used by OpenDentalWebService.  Returns latest valid signal Date/Time.  Can throw exception.</summary>
		public static void RefreshForWeb(ref DateTime sinceDateT) {
			//No need to check RemotingRole; no call to db.
			try {
				if(sinceDateT.Year<1880) {
					sinceDateT=MiscData.GetNowDateTime();
				}
				//Get all invalid types since given time.
				List<int> itypes=Signalods.GetInvalidTypes(Signalods.RefreshTimed(sinceDateT));
				if(itypes.Count<=0) {
					return;
				}
				string itypesStr="";
				for(int i=0;i<itypes.Count;i++) {
					if(i>0) {
						itypesStr+=",";
					}
					itypesStr+=((int)itypes[i]).ToString();
				}
				//Refresh the cache for the given invalid types.
				Cache.RefreshCache(itypesStr);
			}
			catch(Exception e) {
				//Most likely cause for an exception here would be a thread collision between 2 consumers trying to refresh the cache at the exact same instant.
				//There is a chance that performing as subsequent refresh here would cause yet another collision but it's the best we can do without redesigning the entire cache pattern.
				Cache.Refresh(InvalidType.AllLocal);
				throw new Exception("Server cache may be invalid. Please try again. Error: "+e.Message);
			}
			finally {
				DateTime dateTimeNow=DateTime.Now;
				try {
					dateTimeNow=OpenDentBusiness.MiscData.GetNowDateTime();
				}
				catch(Exception) { }
				sinceDateT=dateTimeNow;
			}
		}

		///<summary>This excludes all Invalids.  It is only concerned with text and button messages.  It includes all messages, whether acked or not.  It's up to the UI to filter out acked if necessary.  Also includes all unacked messages regardless of date.</summary>
		public static List<Signalod> RefreshFullText(DateTime sinceDateT) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Signalod>>(MethodBase.GetCurrentMethod(),sinceDateT);
			}
			string command="SELECT * FROM signalod "
				+"WHERE (SigDateTime>"+POut.DateT(sinceDateT)+" "
				+"OR AckTime>"+POut.DateT(sinceDateT)+" "
				+"OR AckTime<"+POut.Date(new DateTime(1880,1,1),true)+") "//always include all unacked.
				+"AND SigType="+POut.Long((int)SignalType.Button)
				+" ORDER BY SigDateTime";
			//note: this might return an occasional row that has both times newer.
			List<Signalod> sigList=new List<Signalod>();
			try {
				sigList=Crud.SignalodCrud.SelectMany(command);
				sigList.Sort();
			} 
			catch {
				//we don't want an error message to show, because that can cause a cascade of a large number of error messages.
			}
			SigElement[] sigElementsAll=SigElements.GetElements(sigList);
			for(int i=0;i<sigList.Count;i++) {
				sigList[i].ElementList=SigElements.GetForSig(sigElementsAll,sigList[i].SignalNum);
			}
			return sigList;//retVal;
		}

		///<summary>Only used when starting up to get the current button state.  Only gets unacked messages.  There may well be extra and useless messages included.  But only the lights will be used anyway, so it doesn't matter.</summary>
		public static List<Signalod> RefreshCurrentButState() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Signalod>>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM signalod "
				+"WHERE SigType=0 "//buttons only
				+"AND AckTime<"+POut.Date(new DateTime(1880,1,1),true)+" "
				+"ORDER BY SigDateTime";
			List<Signalod> sigList=new List<Signalod>();
			try {
				sigList=Crud.SignalodCrud.SelectMany(command);
				sigList.Sort();
			} 
			catch {
				//we don't want an error message to show, because that can cause a cascade of a large number of error messages.
			}
			SigElement[] sigElementsAll=SigElements.GetElements(sigList);
			for(int i=0;i<sigList.Count;i++) {
				sigList[i].ElementList=SigElements.GetForSig(sigElementsAll,sigList[i].SignalNum);
			}
			return sigList;
		}
	
		///<summary></summary>
		public static void Update(Signalod sig) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),sig);
				return;
			}
			Crud.SignalodCrud.Update(sig);
		}

		///<summary></summary>
		public static long Insert(Signalod sig) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				sig.SignalNum=Meth.GetLong(MethodBase.GetCurrentMethod(),sig);
				return sig.SignalNum;
			}
			//we need to explicitly get the server time in advance rather than using NOW(),
			//because we need to update the signal object soon after creation.
			sig.SigDateTime=MiscData.GetNowDateTime();
			return Crud.SignalodCrud.Insert(sig);
		}

		///<summary>Inserts a signal which tells all client machines to update the received unread SMS message count inside the Text button of the main toolbar.  To get the current count from the database, use SmsFromMobiles.GetSmsNotification().</summary>
		public static long InsertSmsNotification(long smsReceivedUnreadCount) {
			Signalod sig=new Signalod();
			sig.SigType=SignalType.Invalid;
			sig.ITypes=((int)InvalidType.SmsTextMsgReceivedUnreadCount).ToString();
			if(smsReceivedUnreadCount<=0) {
				sig.SigText="";
			}
			else if(smsReceivedUnreadCount>=99) {
				sig.SigText="99";
			}
			else {
				sig.SigText=smsReceivedUnreadCount.ToString();
			}
			return Signalods.Insert(sig);
		}

		//<summary>There's no such thing as deleting a signal</summary>
		/*public void Delete(){
			string command= "DELETE from Signalod WHERE SignalNum = '"
				+POut.PInt(SignalNum)+"'";
			DataConnection dcon=new DataConnection();
 			Db.NonQ(command);
		}*/

		///<summary>After a refresh, this is used to determine whether the Appt Module needs to be refreshed.  Must supply the current date showing as well as the recently retrieved signal list.</summary>
		public static bool ApptNeedsRefresh(List<Signalod> signalList,DateTime dateTimeShowing) {
			//No need to check RemotingRole; no call to db.
			List<string> iTypeList;
			for(int i=0;i<signalList.Count;i++){
				iTypeList=new List<string>(signalList[i].ITypes.Split(','));
				if(iTypeList.Contains(((int)InvalidType.Date).ToString()) && signalList[i].DateViewing.Date==dateTimeShowing){
					return true;
				}
			}
			return false;
		}

		///<summary>After a refresh, this is used to determine whether the Current user has received any new tasks through subscription.  Must supply the current usernum as well as the recently retrieved signal list.  The signal list will include any task changes including status changes and deletions.</summary>
		public static List<Task> GetNewTaskPopupsThisUser(List<Signalod> signalList,long userNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Task>>(MethodBase.GetCurrentMethod(),signalList,userNum);
			}
			List<Signalod> sigListFiltered=new List<Signalod>();
			for(int i=0;i<signalList.Count;i++){
				if(signalList[i].ITypes==((int)InvalidType.TaskPopup).ToString()){
					sigListFiltered.Add(signalList[i]);
				}
			}
			if(sigListFiltered.Count==0){//no task popup signals
				return new List<Task>();
			}
			string command="SELECT task.* FROM taskancestor,task,tasklist,tasksubscription "
				+"WHERE taskancestor.TaskListNum=tasklist.TaskListNum "
				+"AND task.TaskNum=taskancestor.TaskNum "
				+"AND tasksubscription.TaskListNum=tasklist.TaskListNum "
				+"AND tasksubscription.UserNum="+POut.Long(userNum)
				+" AND (";
			for(int i=0;i<sigListFiltered.Count;i++){
				if(i>0){
					command+=" OR ";
				}
				command+="task.TaskNum= "+POut.Long(sigListFiltered[i].TaskNum);
			}
			command+=")";
			////The signals are checked on a time interval.  If a task was edited twice in the same interval, then there can be multiple instances of the same task in the list.  Group by the task num to get unique results.
			//if(DataConnection.DBtype==DatabaseType.MySql) {
			//	command+="GROUP BY task.TaskNum";
			//}
			//else {//oracle
			//	command+="GROUP BY task.TaskNum,task.TaskListNum,task.DateTask,task.KeyNum,task.Descript,task.TaskStatus,"
			//		+"task.IsRepeating,task.DateType,task.FromNum,task.ObjectType,task.DateTimeEntry,task.UserNum,task.DateTimeFinished";
			//}
			return Crud.TaskCrud.SelectMany(command);
		}
	
		///<summary>After a refresh, this is used to get a list containing all flags of types that need to be refreshed.   Types of Date and Task are not included.  Because type are an enumeration, the returned list is int32, not int64.</summary>
		public static List<int> GetInvalidTypes(List<Signalod> signalodList) {
			//No need to check RemotingRole; no call to db.
			List<int> retVal=new List<int>();
			string[] strArray;
			for(int i=0;i<signalodList.Count;i++){
				if(signalodList[i].SigType!=SignalType.Invalid){
					continue;
				}
				if(signalodList[i].ITypes==((int)InvalidType.Date).ToString()){
					continue;
				}
				if(signalodList[i].ITypes==((int)InvalidType.Task).ToString()){
					continue;
				}
				if(signalodList[i].ITypes==((int)InvalidType.TaskPopup).ToString()){
					continue;
				}
				if(signalodList[i].ITypes==((int)InvalidType.SmsTextMsgReceivedUnreadCount).ToString()) {
					continue;
				}
				strArray=signalodList[i].ITypes.Split(',');
				for(int t=0;t<strArray.Length;t++){
					if(!retVal.Contains(PIn.Int(strArray[t]))){
						retVal.Add(PIn.Int(strArray[t]));
					}
				}
			}
			return retVal;
		}


		///<summary>After a refresh, this gets a list of only the button signals.</summary>
		public static List<Signalod> GetButtonSigs(List<Signalod> signalodList) {
			//No need to check RemotingRole; no call to db.
			List<Signalod> list=new List<Signalod>();
			for(int i=0;i<signalodList.Count;i++){
				if(signalodList[i].SigType!=SignalType.Button){
					continue;
				}
				list.Add(signalodList[i]);
			}
			return list;
		}

		///<summary>When user clicks on a colored light, they intend to ack it to turn it off.  This acks all signals with the specified index.  This is in case multiple signals have been created from different workstations.  This acks them all in one shot.  Must specify a time because you only want to ack signals earlier than the last time this workstation was refreshed.  A newer signal would not get acked.
		///If this seems slow, then I will need to check to make sure all these tables are properly indexed.</summary>
		public static void AckButton(int buttonIndex,DateTime time){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),buttonIndex,time);
				return;
			}
			//FIXME:UPDATE-MULTIPLE-TABLES
			/*string command= "UPDATE signalod,sigelement,sigelementdef "
				+"SET signalod.AckTime = ";
				if(FormChooseDatabase.DBtype==DatabaseType.Oracle) {
					command+="(SELECT CURRENT_TIMESTAMP FROM DUAL)";
				}else{//Assume MySQL
					command+="NOW()";
				}
				command+=" "
				+"WHERE signalod.AckTime < '1880-01-01' "
				+"AND SigDateTime <= '"+POut.PDateT(time)+"' "
				+"AND signalod.SignalNum=sigelement.SignalNum "
				+"AND sigelement.SigElementDefNum=sigelementdef.SigElementDefNum "
				+"AND sigelementdef.LightRow="+POut.PInt(buttonIndex);
			Db.NonQ(command);*/
			//Rewritten so that the SQL is compatible with both Oracle and MySQL.
			string command= "SELECT signalod.SignalNum FROM signalod,sigelement,sigelementdef "
				+"WHERE signalod.AckTime < "+POut.Date(new DateTime(1880,1,1),true)+" "
				+"AND SigDateTime <= "+POut.DateT(time)+" "
				+"AND signalod.SignalNum=sigelement.SignalNum "
				+"AND sigelement.SigElementDefNum=sigelementdef.SigElementDefNum "
				+"AND sigelementdef.LightRow="+POut.Long(buttonIndex);
			DataTable table=Db.GetTable(command);
			if(table.Rows.Count==0){
				return;
			}
			command="UPDATE signalod SET AckTime = ";
			if(DataConnection.DBtype==DatabaseType.Oracle){
				command+=POut.DateT(MiscData.GetNowDateTime());
			}else {//Assume MySQL
				command+="NOW()";
			}
			command+=" WHERE ";
			for(int i=0;i<table.Rows.Count;i++){
				command+="SignalNum="+table.Rows[i][0].ToString();
				if(i<table.Rows.Count-1){
					command+=" OR ";
				}
			}
			Db.NonQ(command);
		}

		/// <summary>Won't work with InvalidType.Date, InvalidType.Task, or InvalidType.TaskPopup  yet.</summary>
		public static void SetInvalid(params InvalidType[] itypes) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),itypes);
				return;
			}
			string itypeString="";
			for(int i=0;i<itypes.Length;i++) {
				if(i>0) {
					itypeString+=",";
				}
				itypeString+=((int)itypes[i]).ToString();
			}
			Signalod sig=new Signalod();
			sig.ITypes=itypeString;
			sig.DateViewing=DateTime.MinValue;
			sig.SigType=SignalType.Invalid;
			sig.TaskNum=0;
			Insert(sig);
		}

		///<summary>Insertion logic that doesn't use the cache. Has special cases for generating random PK's and handling Oracle insertions.</summary>
		public static void SetInvalidNoCache(params InvalidType[] itypes) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),itypes);
				return;
			}
			string itypeString="";
			for(int i=0;i<itypes.Length;i++) {
				if(i>0) {
					itypeString+=",";
				}
				itypeString+=((int)itypes[i]).ToString();
			}
			Signalod sig=new Signalod();
			sig.ITypes=itypeString;
			sig.DateViewing=DateTime.MinValue;
			sig.SigType=SignalType.Invalid;
			sig.TaskNum=0;
			sig.SigDateTime=MiscData.GetNowDateTime();
			Crud.SignalodCrud.InsertNoCache(sig);
		}

		///<summary>Acknowledge one signal from the manage module grid</summary>
		public static void AckSignal(long signalNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),signalNum);
				return;
			}
			string command="UPDATE signalod SET AckTime = ";
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				command+=POut.DateT(MiscData.GetNowDateTime());
			}
			else {//Assume MySQL
				command+="NOW()";
			}
			command+=" WHERE SignalNum="+POut.Long(signalNum)+" ";
			Db.NonQ(command);
		}

		///<summary>Must be called after Preference cache has been filled.  Deletes all signals older than 2 days if this has not been run within the last week.  Will fail silently if anything goes wrong.</summary>
		public static void ClearOldSignals() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod());
				return;
			}
			try {
				if(PrefC.GetDict().ContainsKey(PrefName.SignalLastClearedDate.ToString())
					&& PrefC.GetDateT(PrefName.SignalLastClearedDate)>MiscData.GetNowDateTime().AddDays(-7)) //Has already been run in the past week. This is all server based time.
				{
					return;//Do not run this process again.
				}
				string command="";
				if(DataConnection.DBtype==DatabaseType.MySql) {//easier to read that using the DbHelper Functions and it also matches the ConvertDB3 script
					command="DELETE FROM signalod WHERE SigType = 1 AND SigDateTime < DATE_ADD(NOW(),INTERVAL -2 DAY)";//Itypes only older than 2 days
					Db.NonQ(command);
					command="DELETE FROM signalod WHERE SigType = 0 AND AckTime != '0001-01-01' AND SigDateTime < DATE_ADD(NOW(),INTERVAL -2 DAY)";//Only unacknowledged buttons older than 2 days
					Db.NonQ(command);
				}
				else {//oracle
					command="DELETE FROM signalod WHERE SigType = 1 AND SigDateTime < CURRENT_TIMESTAMP -2";//Itypes only older than 2 days
					Db.NonQ(command);
					command="DELETE FROM signalod WHERE SigType = 0 AND AckTime != TO_DATE('0001-01-01','YYYY-MM-DD') AND SigDateTime < CURRENT_TIMESTAMP -2";//Only unacknowledged buttons older than 2 days
					Db.NonQ(command);
				}
				SigElements.DeleteOrphaned();
				Prefs.UpdateDateT(PrefName.SignalLastClearedDate,MiscData.GetNowDateTime());//Set Last cleared to now.
			}
			catch(Exception ex) {
				//fail silently
			}
		}
	}

	

	


}




















