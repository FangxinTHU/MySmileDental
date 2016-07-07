using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class OrthoCharts{

		///<summary></summary>
		public static List<OrthoChart> GetAll() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<OrthoChart>>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM orthochart";
			return Crud.OrthoChartCrud.SelectMany(command);
		}

		///<summary></summary>
		public static List<OrthoChart> GetAllForPatient(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<OrthoChart>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM orthochart WHERE PatNum ="+POut.Long(patNum)
				+" AND FieldValue!=''";//FieldValue='' were stored as a result of a bug. DBM now removes those rows from the DB. This prevents them from being seen until DBM is run.
			return Crud.OrthoChartCrud.SelectMany(command);
		}

		///<summary>Useful for distinct display fields.</summary>
		public static List<OrthoChart> GetByDistinctFieldNames() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<OrthoChart>>(MethodBase.GetCurrentMethod());
			}
			//This is the simple querry that doesn't work with oracle
			//string command="SELECT * FROM orthochart GROUP BY FieldName";
			//This query was rewritten for Oracle support, it will provide the same results weather it is run in MySql or Oracle.
			string command="SELECT * FROM orthochart, (SELECT MAX(OrthoChartNum) OrthoChartNum, FieldName FROM orthochart GROUP BY FieldName) uniqueSubTable WHERE orthochart.OrthoChartNum = uniqueSubTable.OrthoChartNum";
			return Crud.OrthoChartCrud.SelectMany(command);
		}

		///<summary></summary>
		public static long Insert(OrthoChart orthoChart) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				orthoChart.OrthoChartNum=Meth.GetLong(MethodBase.GetCurrentMethod(),orthoChart);
				return orthoChart.OrthoChartNum;
			}
			return Crud.OrthoChartCrud.Insert(orthoChart);
		}

		///<summary></summary>
		public static void Update(OrthoChart orthoChart) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),orthoChart);
				return;
			}
			Crud.OrthoChartCrud.Update(orthoChart);
		}

		///<summary></summary>
		public static void Update(OrthoChart orthoChart,OrthoChart oldOrthoChart) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),orthoChart,oldOrthoChart);
				return;
			}
			string command="";
			if(orthoChart.PatNum != oldOrthoChart.PatNum) {
				if(command!=""){ command+=",";}
				command+="PatNum = "+POut.Long(orthoChart.PatNum)+"";
			}
			if(orthoChart.DateService != oldOrthoChart.DateService) {
				if(command!=""){ command+=",";}
				command+="DateService = "+POut.Date(orthoChart.DateService)+"";
			}
			if(orthoChart.FieldName != oldOrthoChart.FieldName) {
				if(command!=""){ command+=",";}
				command+="FieldName = '"+POut.String(orthoChart.FieldName)+"'";
			}
			if(orthoChart.FieldValue != oldOrthoChart.FieldValue) {
				if(command!=""){ command+=",";}
				command+="FieldValue = '"+POut.String(orthoChart.FieldValue)+"'";
			}
			if(command==""){
				return;
			}
			command="UPDATE orthochart SET "+command
				+" WHERE OrthoChartNum = "+POut.Long(oldOrthoChart.OrthoChartNum);
			Db.NonQ(command);
			//Crud.OrthoChartCrud.Update(orthoChartNew,orthoChartOld);
		}
		  
		///<summary>Ortho charts were briefly not deleted between 05/06/2014 and 01/02/2015.  Deleting occurs regularly when FieldValue="".</summary>
		public static void Delete(long orthoChartNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),orthoChartNum);
				return;
			}
			string command= "DELETE FROM orthochart WHERE OrthoChartNum = "+POut.Long(orthoChartNum);
			Db.NonQ(command);
		}

		///<summary>Used for ortho chart audit trail.  Attempts to parse the DateOfService from the security log text. If it is unable to parse the date, it will return MinDate.
		///<para>Returning MinDate from this function results in the audit trail entries for multiple dates of service displaying intermingled on the date "0001-01-01", harmless.</para></summary>
		public static DateTime GetOrthoDateFromLog(SecurityLog securityLog) {
			//There are 3 cases to try, in order of ascending complexity. If a simple case succeeds at parsing a date, that date is returned.
			//1) Using the new log text, there should be an 8 digit number at the end of each log entry. This is in the format "YYYYMMDD" and should be culture invariant.
			//2) Using the old log text, the Date of service appeared as a string in the middle of the text block.
			//3) Using the old log text, the Date of service appeared as a string in the middle of the text block in a culture dependant format.
			DateTime retVal=DateTime.MinValue;
			#region Ideal Case, Culture invariant
			try {
				string dateString=securityLog.LogText.Substring(securityLog.LogText.Length-8,8);
				retVal=new DateTime(int.Parse(dateString.Substring(0,4)),int.Parse(dateString.Substring(4,2)),int.Parse(dateString.Substring(6,2)));
				if(retVal!=DateTime.MinValue) {
					return retVal;
				}
			}
			catch(Exception ex) { }
			#endregion
			#region Depricated, log written in english
			try {
				if(securityLog.LogText.StartsWith("Ortho chart field edited.  Field date: ")) {
					retVal=DateTime.Parse(securityLog.LogText.Substring("Ortho chart field edited.  Field date: ".Length,10));//Date usually in the format MM/DD/YYYY, unless using en-UK for example
					if(retVal!=DateTime.MinValue) {
						return retVal;
					}
				}
			}
			catch(Exception ex) { }
			#endregion
			#region Depricated, log written in current culture
			try {
				if(securityLog.LogText.StartsWith(Lans.g("FormOrthoChart","Ortho chart field edited.  Field date"))) {
					string[] tokens=securityLog.LogText.Split(new string[] { ": " },StringSplitOptions.None);
					retVal=DateTime.Parse(tokens[1].Replace(Lans.g("FormOrthoChart","Field name"),""));
					if(retVal!=DateTime.MinValue) {
						return retVal;
					}
				}
			}
			catch(Exception ex) { }
			#endregion
			#region Depricated, log written in non-english non-current culture
				//not particularly common or useful.
			#endregion
			return retVal;//Should be DateTime.MinVal if we are returning here.
		}

		public static int SortDateField(OrthoChart x,OrthoChart y) {
			if(x.DateService!=y.DateService) {
				return x.DateService.CompareTo(y.DateService);
			}
			return x.FieldName.CompareTo(y.FieldName);
		}

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<OrthoChart> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<OrthoChart>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM orthochart WHERE PatNum = "+POut.Long(patNum);
			return Crud.OrthoChartCrud.SelectMany(command);
		}

		///<summary>Gets one OrthoChart from the db.</summary>
		public static OrthoChart GetOne(long orthoChartNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<OrthoChart>(MethodBase.GetCurrentMethod(),orthoChartNum);
			}
			return Crud.OrthoChartCrud.SelectOne(orthoChartNum);
		}

		
		*/



	}
}