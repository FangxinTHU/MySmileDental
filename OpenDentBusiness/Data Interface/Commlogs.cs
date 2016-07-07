using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Commlogs {

		///<summary>Gets all items for the current patient ordered by date.</summary>
		public static List<Commlog> Refresh(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Commlog>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command=
				"SELECT * FROM commlog"
				+" WHERE PatNum = '"+patNum+"'"
				+" ORDER BY CommDateTime";
			return Crud.CommlogCrud.SelectMany(command);
		}

		///<summary>Gets one commlog item from database.</summary>
		public static Commlog GetOne(long commlogNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Commlog>(MethodBase.GetCurrentMethod(),commlogNum);
			}
			return Crud.CommlogCrud.SelectOne(commlogNum);
		}

		///<summary>If a commlog exists with today's date for the current user and has no stop time, then that commlog is returned so it can be reopened.  Otherwise, return null.</summary>
		public static Commlog GetIncompleteEntry(long userNum,long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Commlog>(MethodBase.GetCurrentMethod(),userNum,patNum);
			}
			//no need for Oracle compatibility
			string command="SELECT * FROM commlog WHERE DATE(CommDateTime)=CURDATE() "
				+"AND UserNum="+POut.Long(userNum)+" "
				+"AND PatNum="+POut.Long(patNum)+" "
				+"AND (CommType=292 OR CommType=441) "//support call or chat, DefNums
				+"AND Mode_="+POut.Int((int)CommItemMode.Phone)+" "//mode=phone
				+"AND DateTimeEnd < '1880-01-01' LIMIT 1";
			return Crud.CommlogCrud.SelectOne(command);
		}

		///<summary></summary>
		public static long Insert(Commlog comm) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				comm.CommlogNum=Meth.GetLong(MethodBase.GetCurrentMethod(),comm);
				return comm.CommlogNum;
			}
			return Crud.CommlogCrud.Insert(comm);
		}

		///<summary></summary>
		public static void Update(Commlog comm) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),comm);
				return;
			}
			Crud.CommlogCrud.Update(comm);
		}

		///<summary>Updates only the changed fields (if any).</summary>
		public static bool Update(Commlog comm,Commlog oldCommlog) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod(),comm,oldCommlog);
			}
			return Crud.CommlogCrud.Update(comm,oldCommlog);
		}

		///<summary></summary>
		public static void Delete(Commlog comm) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),comm);
				return;
			}
			Crud.CommlogCrud.Delete(comm.CommlogNum);
		}

		///<summary>Used when printing or emailing recall to make a commlog entry without any display.</summary>
		public static void InsertForRecall(long patNum,CommItemMode _mode,int numberOfReminders,long defNumNewStatus) {
			//No need to check RemotingRole; no call to db.
			InsertForRecall(patNum,_mode,numberOfReminders,defNumNewStatus,CommItemSource.User,Security.CurUser.UserNum);//Recall commlog not associated to the Web Sched app.
		}

		///<summary>Used when printing or emailing recall to make a commlog entry without any display.  
		///Set commSource to the corresponding entity that is making this recall.  E.g. Web Sched.
		///If the commSource is a 3rd party, set it to ProgramLink and make an overload that accepts the ProgramNum.</summary>
		public static void InsertForRecall(long patNum,CommItemMode _mode,int numberOfReminders,long defNumNewStatus,CommItemSource commSource,long userNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),patNum,_mode,numberOfReminders,defNumNewStatus,commSource,userNum);
				return;
			}
			long recallType=Commlogs.GetTypeAuto(CommItemTypeAuto.RECALL);
			string command;
			string datesql="CURDATE()";
			if(DataConnection.DBtype==DatabaseType.Oracle){
				datesql="(SELECT CURRENT_DATE FROM dual)";
			}
			if(recallType!=0){
				command="SELECT COUNT(*) FROM commlog WHERE ";
				command+=DbHelper.DtimeToDate("CommDateTime")+" = "+datesql;
				command+=" AND PatNum="+POut.Long(patNum)+" AND CommType="+POut.Long(recallType)
					+" AND Mode_="+POut.Long((int)_mode)
					+" AND SentOrReceived=1";
				if(Db.GetCount(command)!="0"){
					return;
				}
			}
			Commlog com=new Commlog();
			com.PatNum=patNum;
			com.CommDateTime=DateTime.Now;
			com.CommType=recallType;
			com.Mode_=_mode;
			com.SentOrReceived=CommSentOrReceived.Sent;
			com.Note="";
			if(numberOfReminders==0){
				com.Note=Lans.g("FormRecallList","Recall reminder.");
			}
			else if(numberOfReminders==1) {
				com.Note=Lans.g("FormRecallList","Second recall reminder.");
			}
			else if(numberOfReminders==2) {
				com.Note=Lans.g("FormRecallList","Third recall reminder.");
			}
			else {
				com.Note=Lans.g("FormRecallList","Recall reminder:")+" "+(numberOfReminders+1).ToString();
			}
			if(defNumNewStatus==0) {
				com.Note+="  "+Lans.g("Commlogs","Status None");
			}
			else {
				com.Note+="  "+DefC.GetName(DefCat.RecallUnschedStatus,defNumNewStatus);
			}
			com.UserNum=userNum;
			com.CommSource=commSource;
			Insert(com);
			EhrMeasureEvent newMeasureEvent=new EhrMeasureEvent();
			newMeasureEvent.DateTEvent=com.CommDateTime;
			newMeasureEvent.EventType=EhrMeasureEventType.ReminderSent;
			newMeasureEvent.PatNum=com.PatNum;
			newMeasureEvent.MoreInfo=com.Note;
			EhrMeasureEvents.Insert(newMeasureEvent);
		}

		///<Summary>Returns a defnum.  If no match, then it returns the first one in the list in that category.</Summary>
		public static long GetTypeAuto(CommItemTypeAuto typeauto) {
			//No need to check RemotingRole; no call to db.
			Def[][] arrayDefs=DefC.GetArrayLong();
			for(int i=0;i<arrayDefs[(int)DefCat.CommLogTypes].Length;i++) {
				if(arrayDefs[(int)DefCat.CommLogTypes][i].ItemValue==typeauto.ToString()) {
					return arrayDefs[(int)DefCat.CommLogTypes][i].DefNum;
				}
			}
			if(arrayDefs[(int)DefCat.CommLogTypes].Length>0) {
				return arrayDefs[(int)DefCat.CommLogTypes][0].DefNum;
			}
			return 0;
		}

		public static int GetRecallUndoCount(DateTime date) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod(),date);
			}
			string command="SELECT COUNT(*) FROM commlog "
				+"WHERE "+DbHelper.DtimeToDate("CommDateTime")+" = "+POut.Date(date)+" "
				+"AND (SELECT ItemValue FROM definition WHERE definition.DefNum=commlog.CommType) ='"+CommItemTypeAuto.RECALL.ToString()+"'";
			return PIn.Int(Db.GetScalar(command));
		}

		public static void RecallUndo(DateTime date) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),date);
				return;
			}
			string command="DELETE FROM commlog "
				+"WHERE "+DbHelper.DtimeToDate("CommDateTime")+" = "+POut.Date(date)+" "
				+"AND (SELECT ItemValue FROM definition WHERE definition.DefNum=commlog.CommType) ='"+CommItemTypeAuto.RECALL.ToString()+"'";
			Db.NonQ(command);
		}

		///<summary>Returns the message used to ask if the user would like to save the appointment/patient note as a commlog when deleting an appointment/patient note.  Only returns up to the first 30 characters of the note.</summary>
		public static string GetDeleteApptCommlogMessage(string noteText,ApptStatus apptStatus) {
			//No need to check RemotingRole; no call to db.
			string commlogMsgText="";
			if(noteText!="") {
				if(apptStatus==ApptStatus.PtNote || apptStatus==ApptStatus.PtNoteCompleted){
					commlogMsgText=Lans.g("Commlogs","Save patient note in CommLog?")+"\r\n"+"\r\n";
				}
				else{
					commlogMsgText=Lans.g("Commlogs","Save appointment note in CommLog?")+"\r\n"+"\r\n";
				}
				//Show up to 30 characters of the note because they can get rather large thus pushing the buttons off the screen.
				commlogMsgText+=noteText.Substring(0,Math.Min(noteText.Length,30));
				commlogMsgText+=(noteText.Length>30)?"...":"";//Append ... to the end of the message so that they know there is more to the note than what is displayed.
			}
			return commlogMsgText;
		}

		///<summary>Gets all commlogs for family that contain a DateTimeEnd entry.  Used internally to keep track of how long calls lasted.</summary>
		public static List<Commlog> GetTimedCommlogsForPat(long guarantor) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Commlog>>(MethodBase.GetCurrentMethod(),guarantor);
			}
			string command="SELECT commlog.* FROM commlog "
				+"INNER JOIN patient ON commlog.PatNum=patient.PatNum AND patient.Guarantor="+POut.Long(guarantor)+" "
				+"WHERE "+DbHelper.Year("commlog.DateTimeEnd")+">1";
			return Crud.CommlogCrud.SelectMany(command);
		}

	}

	///<summary></summary>
	public enum CommItemTypeAuto {
		APPT,
		FIN,
		RECALL,
		MISC
	}




}

















