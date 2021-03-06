//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Crud{
	public class JobCrud {
		///<summary>Gets one Job object from the database using the primary key.  Returns null if not found.</summary>
		public static Job SelectOne(long jobNum){
			string command="SELECT * FROM job "
				+"WHERE JobNum = "+POut.Long(jobNum);
			List<Job> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one Job object from the database using a query.</summary>
		public static Job SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<Job> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of Job objects from the database using a query.</summary>
		public static List<Job> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<Job> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<Job> TableToList(DataTable table){
			List<Job> retVal=new List<Job>();
			Job job;
			foreach(DataRow row in table.Rows) {
				job=new Job();
				job.JobNum       = PIn.Long  (row["JobNum"].ToString());
				job.ExpertNum    = PIn.Long  (row["ExpertNum"].ToString());
				job.OwnerNum     = PIn.Long  (row["OwnerNum"].ToString());
				job.ParentNum    = PIn.Long  (row["ParentNum"].ToString());
				string priority=row["Priority"].ToString();
				if(priority==""){
					job.Priority   =(JobPriority)0;
				}
				else try{
					job.Priority   =(JobPriority)Enum.Parse(typeof(JobPriority),priority);
				}
				catch{
					job.Priority   =(JobPriority)0;
				}
				string category=row["Category"].ToString();
				if(category==""){
					job.Category   =(JobCategory)0;
				}
				else try{
					job.Category   =(JobCategory)Enum.Parse(typeof(JobCategory),category);
				}
				catch{
					job.Category   =(JobCategory)0;
				}
				job.JobVersion   = PIn.String(row["JobVersion"].ToString());
				job.HoursEstimate= PIn.Int   (row["HoursEstimate"].ToString());
				job.HoursActual  = PIn.Int   (row["HoursActual"].ToString());
				job.DateTimeEntry= PIn.DateT (row["DateTimeEntry"].ToString());
				job.Description  = PIn.String(row["Description"].ToString());
				job.Title        = PIn.String(row["Title"].ToString());
				string jobStatus=row["JobStatus"].ToString();
				if(jobStatus==""){
					job.JobStatus  =(JobStat)0;
				}
				else try{
					job.JobStatus  =(JobStat)Enum.Parse(typeof(JobStat),jobStatus);
				}
				catch{
					job.JobStatus  =(JobStat)0;
				}
				retVal.Add(job);
			}
			return retVal;
		}

		///<summary>Inserts one Job into the database.  Returns the new priKey.</summary>
		public static long Insert(Job job){
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				job.JobNum=DbHelper.GetNextOracleKey("job","JobNum");
				int loopcount=0;
				while(loopcount<100){
					try {
						return Insert(job,true);
					}
					catch(Oracle.DataAccess.Client.OracleException ex){
						if(ex.Number==1 && ex.Message.ToLower().Contains("unique constraint") && ex.Message.ToLower().Contains("violated")){
							job.JobNum++;
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
				return Insert(job,false);
			}
		}

		///<summary>Inserts one Job into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(Job job,bool useExistingPK){
			if(!useExistingPK && PrefC.RandomKeys) {
				job.JobNum=ReplicationServers.GetKey("job","JobNum");
			}
			string command="INSERT INTO job (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="JobNum,";
			}
			command+="ExpertNum,OwnerNum,ParentNum,Priority,Category,JobVersion,HoursEstimate,HoursActual,DateTimeEntry,Description,Title,JobStatus) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(job.JobNum)+",";
			}
			command+=
				     POut.Long  (job.ExpertNum)+","
				+    POut.Long  (job.OwnerNum)+","
				+    POut.Long  (job.ParentNum)+","
				+"'"+POut.String(job.Priority.ToString())+"',"
				+"'"+POut.String(job.Category.ToString())+"',"
				+"'"+POut.String(job.JobVersion)+"',"
				+    POut.Int   (job.HoursEstimate)+","
				+    POut.Int   (job.HoursActual)+","
				+    DbHelper.Now()+","
				+    DbHelper.ParamChar+"paramDescription,"
				+"'"+POut.String(job.Title)+"',"
				+"'"+POut.String(job.JobStatus.ToString())+"')";
			if(job.Description==null) {
				job.Description="";
			}
			OdSqlParameter paramDescription=new OdSqlParameter("paramDescription",OdDbType.Text,job.Description);
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command,paramDescription);
			}
			else {
				job.JobNum=Db.NonQ(command,true,paramDescription);
			}
			return job.JobNum;
		}

		///<summary>Inserts one Job into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(Job job){
			if(DataConnection.DBtype==DatabaseType.MySql) {
				return InsertNoCache(job,false);
			}
			else {
				if(DataConnection.DBtype==DatabaseType.Oracle) {
					job.JobNum=DbHelper.GetNextOracleKey("job","JobNum"); //Cacheless method
				}
				return InsertNoCache(job,true);
			}
		}

		///<summary>Inserts one Job into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(Job job,bool useExistingPK){
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO job (";
			if(!useExistingPK && isRandomKeys) {
				job.JobNum=ReplicationServers.GetKeyNoCache("job","JobNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="JobNum,";
			}
			command+="ExpertNum,OwnerNum,ParentNum,Priority,Category,JobVersion,HoursEstimate,HoursActual,DateTimeEntry,Description,Title,JobStatus) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(job.JobNum)+",";
			}
			command+=
				     POut.Long  (job.ExpertNum)+","
				+    POut.Long  (job.OwnerNum)+","
				+    POut.Long  (job.ParentNum)+","
				+"'"+POut.String(job.Priority.ToString())+"',"
				+"'"+POut.String(job.Category.ToString())+"',"
				+"'"+POut.String(job.JobVersion)+"',"
				+    POut.Int   (job.HoursEstimate)+","
				+    POut.Int   (job.HoursActual)+","
				+    DbHelper.Now()+","
				+    DbHelper.ParamChar+"paramDescription,"
				+"'"+POut.String(job.Title)+"',"
				+"'"+POut.String(job.JobStatus.ToString())+"')";
			if(job.Description==null) {
				job.Description="";
			}
			OdSqlParameter paramDescription=new OdSqlParameter("paramDescription",OdDbType.Text,job.Description);
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command,paramDescription);
			}
			else {
				job.JobNum=Db.NonQ(command,true,paramDescription);
			}
			return job.JobNum;
		}

		///<summary>Updates one Job in the database.</summary>
		public static void Update(Job job){
			string command="UPDATE job SET "
				+"ExpertNum    =  "+POut.Long  (job.ExpertNum)+", "
				+"OwnerNum     =  "+POut.Long  (job.OwnerNum)+", "
				+"ParentNum    =  "+POut.Long  (job.ParentNum)+", "
				+"Priority     = '"+POut.String(job.Priority.ToString())+"', "
				+"Category     = '"+POut.String(job.Category.ToString())+"', "
				+"JobVersion   = '"+POut.String(job.JobVersion)+"', "
				+"HoursEstimate=  "+POut.Int   (job.HoursEstimate)+", "
				+"HoursActual  =  "+POut.Int   (job.HoursActual)+", "
				//DateTimeEntry not allowed to change
				+"Description  =  "+DbHelper.ParamChar+"paramDescription, "
				+"Title        = '"+POut.String(job.Title)+"', "
				+"JobStatus    = '"+POut.String(job.JobStatus.ToString())+"' "
				+"WHERE JobNum = "+POut.Long(job.JobNum);
			if(job.Description==null) {
				job.Description="";
			}
			OdSqlParameter paramDescription=new OdSqlParameter("paramDescription",OdDbType.Text,job.Description);
			Db.NonQ(command,paramDescription);
		}

		///<summary>Updates one Job in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(Job job,Job oldJob){
			string command="";
			if(job.ExpertNum != oldJob.ExpertNum) {
				if(command!=""){ command+=",";}
				command+="ExpertNum = "+POut.Long(job.ExpertNum)+"";
			}
			if(job.OwnerNum != oldJob.OwnerNum) {
				if(command!=""){ command+=",";}
				command+="OwnerNum = "+POut.Long(job.OwnerNum)+"";
			}
			if(job.ParentNum != oldJob.ParentNum) {
				if(command!=""){ command+=",";}
				command+="ParentNum = "+POut.Long(job.ParentNum)+"";
			}
			if(job.Priority != oldJob.Priority) {
				if(command!=""){ command+=",";}
				command+="Priority = '"+POut.String(job.Priority.ToString())+"'";
			}
			if(job.Category != oldJob.Category) {
				if(command!=""){ command+=",";}
				command+="Category = '"+POut.String(job.Category.ToString())+"'";
			}
			if(job.JobVersion != oldJob.JobVersion) {
				if(command!=""){ command+=",";}
				command+="JobVersion = '"+POut.String(job.JobVersion)+"'";
			}
			if(job.HoursEstimate != oldJob.HoursEstimate) {
				if(command!=""){ command+=",";}
				command+="HoursEstimate = "+POut.Int(job.HoursEstimate)+"";
			}
			if(job.HoursActual != oldJob.HoursActual) {
				if(command!=""){ command+=",";}
				command+="HoursActual = "+POut.Int(job.HoursActual)+"";
			}
			//DateTimeEntry not allowed to change
			if(job.Description != oldJob.Description) {
				if(command!=""){ command+=",";}
				command+="Description = "+DbHelper.ParamChar+"paramDescription";
			}
			if(job.Title != oldJob.Title) {
				if(command!=""){ command+=",";}
				command+="Title = '"+POut.String(job.Title)+"'";
			}
			if(job.JobStatus != oldJob.JobStatus) {
				if(command!=""){ command+=",";}
				command+="JobStatus = '"+POut.String(job.JobStatus.ToString())+"'";
			}
			if(command==""){
				return false;
			}
			if(job.Description==null) {
				job.Description="";
			}
			OdSqlParameter paramDescription=new OdSqlParameter("paramDescription",OdDbType.Text,job.Description);
			command="UPDATE job SET "+command
				+" WHERE JobNum = "+POut.Long(job.JobNum);
			Db.NonQ(command,paramDescription);
			return true;
		}

		///<summary>Returns true if Update(Job,Job) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(Job job,Job oldJob) {
			if(job.ExpertNum != oldJob.ExpertNum) {
				return true;
			}
			if(job.OwnerNum != oldJob.OwnerNum) {
				return true;
			}
			if(job.ParentNum != oldJob.ParentNum) {
				return true;
			}
			if(job.Priority != oldJob.Priority) {
				return true;
			}
			if(job.Category != oldJob.Category) {
				return true;
			}
			if(job.JobVersion != oldJob.JobVersion) {
				return true;
			}
			if(job.HoursEstimate != oldJob.HoursEstimate) {
				return true;
			}
			if(job.HoursActual != oldJob.HoursActual) {
				return true;
			}
			//DateTimeEntry not allowed to change
			if(job.Description != oldJob.Description) {
				return true;
			}
			if(job.Title != oldJob.Title) {
				return true;
			}
			if(job.JobStatus != oldJob.JobStatus) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one Job from the database.</summary>
		public static void Delete(long jobNum){
			string command="DELETE FROM job "
				+"WHERE JobNum = "+POut.Long(jobNum);
			Db.NonQ(command);
		}

	}
}