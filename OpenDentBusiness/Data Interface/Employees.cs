using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Employees{
		private static Employee[] listLong;
		private static Employee[] listShort;

		public static Employee[] ListLong {
			//No need to check RemotingRole; no call to db.
			get {
				if(listLong==null) {
					RefreshCache();
				}
				return listLong;
			}
			set {
				listLong=value;
			}
		}

		///<summary>Does not include hidden employees</summary>
		public static Employee[] ListShort {
			//No need to check RemotingRole; no call to db.
			get {
				if(listShort==null) {
					RefreshCache();
				}
				return listShort;
			}
			set {
				listShort=value;
			}
		}

		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM employee ORDER BY IsHidden,FName,LName";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Employee";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			listLong=Crud.EmployeeCrud.TableToList(table).ToArray();
			List<Employee> tempList=new List<Employee>();
			for(int i=0;i<listLong.Length;i++) {
				if(!listLong[i].IsHidden) {
					tempList.Add(listLong[i]);
				}
			}
			listShort=tempList.ToArray();
		}

		///<summary>Does not include hidden employees</summary>
		public static List<Employee> GetListShort() {
			List<Employee> listEmpsShort=new List<Employee>();
			for(int i=0;i<ListShort.Length;i++) {
				listEmpsShort.Add(ListShort[i].Copy());
			}
			return listEmpsShort;
		}

		///<summary>Instead of using the cache, which sorts by FName, LName.</summary>
		public static List<Employee> GetForTimeCard() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Employee>>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM employee WHERE IsHidden=0 ORDER BY LName,Fname";
			return Crud.EmployeeCrud.SelectMany(command);
		}

		/*public static Employee[] GetListByExtension(){
			if(ListShort==null){
				return new Employee[0];
			}
			Employee[] arrayCopy=new Employee[ListShort.Length];
			ListShort.CopyTo(arrayCopy,0);
			int[] arrayKeys=new int[ListShort.Length];
			for(int i=0;i<ListShort.Length;i++){
				arrayKeys[i]=ListShort[i].PhoneExt;
			}
			Array.Sort(arrayKeys,arrayCopy);
			//List<Employee> retVal=new List<Employee>(ListShort);
			//retVal.Sort(
			return arrayCopy;
		}*/

		///<summary></summary>
		public static void Update(Employee Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			if(Cur.LName=="" && Cur.FName=="") {
				throw new ApplicationException(Lans.g("FormEmployeeEdit","Must include either first name or last name"));
			}
			Crud.EmployeeCrud.Update(Cur);
		}

		///<summary></summary>
		public static long Insert(Employee Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Cur.EmployeeNum=Meth.GetLong(MethodBase.GetCurrentMethod(),Cur);
				return Cur.EmployeeNum;
			}if(Cur.LName=="" && Cur.FName=="") {
				throw new ApplicationException(Lans.g("FormEmployeeEdit","Must include either first name or last name"));
			}
			return Crud.EmployeeCrud.Insert(Cur);
		}

		///<summary>Surround with try-catch</summary>
		public static void Delete(long employeeNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),employeeNum);
				return;
			}
			//appointment.Assistant will not block deletion
			//schedule.EmployeeNum will not block deletion
			string command="SELECT COUNT(*) FROM clockevent WHERE EmployeeNum="+POut.Long(employeeNum);
			if(Db.GetCount(command)!="0"){
				throw new ApplicationException(Lans.g("FormEmployeeSelect",
					"Not allowed to delete employee because of attached clock events."));
			}
			command="SELECT COUNT(*) FROM timeadjust WHERE EmployeeNum="+POut.Long(employeeNum);
			if(Db.GetCount(command)!="0") {
				throw new ApplicationException(Lans.g("FormEmployeeSelect",
					"Not allowed to delete employee because of attached time adjustments."));
			}
			command="SELECT COUNT(*) FROM userod WHERE EmployeeNum="+POut.Long(employeeNum);
			if(Db.GetCount(command)!="0") {
				throw new ApplicationException(Lans.g("FormEmployeeSelect",
					"Not allowed to delete employee because of attached user."));
			}
			command="UPDATE appointment SET Assistant=0 WHERE Assistant="+POut.Long(employeeNum);
			Db.NonQ(command);
			command="SELECT ScheduleNum FROM schedule WHERE EmployeeNum="+POut.Long(employeeNum);
			DataTable table=Db.GetTable(command);
			List<string> listScheduleNums=new List<string>();//Used for deleting scheduleops below
			for(int i=0;i<table.Rows.Count;i++) {
				//Add entry to deletedobjects table if it is a provider schedule type
				DeletedObjects.SetDeleted(DeletedObjectType.ScheduleProv,PIn.Long(table.Rows[i]["ScheduleNum"].ToString()));
				listScheduleNums.Add(table.Rows[i]["ScheduleNum"].ToString());
			}
			if(listScheduleNums.Count>0) {
				command="DELETE FROM scheduleop WHERE ScheduleNum IN("+POut.String(String.Join(",",listScheduleNums))+")";
				Db.NonQ(command);
			}
			//command="DELETE FROM scheduleop WHERE ScheduleNum IN(SELECT ScheduleNum FROM schedule WHERE EmployeeNum="+POut.Long(employeeNum)+")";
			//Db.NonQ(command);
			command="DELETE FROM schedule WHERE EmployeeNum="+POut.Long(employeeNum);
			Db.NonQ(command);
			command= "DELETE FROM employee WHERE EmployeeNum ="+POut.Long(employeeNum);
			Db.NonQ(command);
			command="DELETE FROM timecardrule WHERE EmployeeNum="+POut.Long(employeeNum);
			Db.NonQ(command);
		}

		/*
		///<summary>Returns LName,FName MiddleI for the provided employee.</summary>
		public static string GetNameLF(Employee emp){
			return(emp.LName+", "+emp.FName+" "+emp.MiddleI);
		}

		///<summary>Loops through List to find matching employee, and returns LName,FName MiddleI.</summary>
		public static string GetNameLF(int employeeNum){
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].EmployeeNum==employeeNum){
					return GetNameLF(ListLong[i]);
				}
			}
			return "";
		}*/

		///<summary>Returns FName MiddleI LName for the provided employee.</summary>
		public static string GetNameFL(Employee emp) {
			//No need to check RemotingRole; no call to db.
			return (emp.FName+" "+emp.MiddleI+" "+emp.LName);
		}

		///<summary>Loops through List to find matching employee, and returns FName MiddleI LName.</summary>
		public static string GetNameFL(long employeeNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<ListLong.Length;i++) {
				if(ListLong[i].EmployeeNum==employeeNum) {
					return GetNameFL(ListLong[i]);
				}
			}
			return "";
		}

		///<summary>Loops through List to find matching employee, and returns first 2 letters of first name.  Will later be improved with abbr field.</summary>
		public static string GetAbbr(long employeeNum) {
			//No need to check RemotingRole; no call to db.
			string retVal="";
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].EmployeeNum==employeeNum){
					retVal=ListLong[i].FName;
					if(retVal.Length>2)
						retVal=retVal.Substring(0,2);
					return retVal;
				}
			}
			return "";
		}

		///<summary>From cache</summary>
		public static Employee GetEmp(long employeeNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<ListLong.Length;i++) {
				if(ListLong[i].EmployeeNum==employeeNum) {
					return ListLong[i];
				}
			}
			return null;
		}

		///<summary>Find formatted name in list.  Takes in a name that was previously formatted by Employees.GetNameFL and finds a match in the list.  If no match is found then returns null.</summary>
		public static Employee GetEmp(string nameFL,List<Employee> employees) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<employees.Count;i++) {
				if(GetNameFL(employees[i])==nameFL) {
					return employees[i];
				}
			}
			return null;
		}

		///<summary>Gets all employees associated to users that have a clinic set to the clinic passed in.  Passing in 0 will get a list of employees not assigned to any clinic.  Gets employees from the cache which is sorted by FName, LastName.</summary>
		public static List<Employee> GetEmpsForClinic(long clinicNum) {
			//No need to check RemotingRole; no call to db.
			return GetEmpsForClinic(clinicNum,false);
		}

		///<summary>Gets all the employees for a specific clinicNum, according to their associated user.  Pass in a clinicNum of 0 to get the list of unassigned or "all" employees (depending on isAll flag).  In addition to setting clinicNum to 0, set isAll true to get a list of all employees or false to get a list of employees that are not associated to any clinics.  Always gets the list of employees from the cache which is sorted by FName, LastName.</summary>
		public static List<Employee> GetEmpsForClinic(long clinicNum,bool isAll) {
			//No need to check RemotingRole; no call to db.
			if(clinicNum==0 && isAll) {//Simply return all employees.
				return Employees.GetListShort();
			}
			List<Employee> listEmpsShort=Employees.GetListShort();
			List<Employee> listEmpsWithClinic=new List<Employee>();
			List<long> listEmpNumsWithClinic=new List<long>();
			List<Employee> listEmpsUnassigned=new List<Employee>();
			List<long> listEmpNumsUnassigned=new List<long>();
			for(int i=0;i<listEmpsShort.Count;i++) {
				List<Userod> listUsers=Userods.GetUsersByEmployeeNum(listEmpsShort[i].EmployeeNum);
				if(listUsers.Count==0) {
					if(listEmpNumsUnassigned.Contains(listEmpsShort[i].EmployeeNum)) {
						continue;//Employee already added to the results.
					}
					listEmpNumsUnassigned.Add(listEmpsShort[i].EmployeeNum);
					listEmpsUnassigned.Add(listEmpsShort[i]);
					continue;
				}
				//At this point we know there is at least one Userod associated to this employee.
				for(int j=0;j<listUsers.Count;j++) {
					//Check if the user is associated to a clinic
					if(listUsers[j].ClinicNum==0) {//Unassigned
						if(listEmpNumsUnassigned.Contains(listEmpsShort[i].EmployeeNum)) {
							continue;//Employee already added to the results.
						}
						listEmpNumsUnassigned.Add(listEmpsShort[i].EmployeeNum);
						listEmpsUnassigned.Add(listEmpsShort[i]);
						continue;
					}
					//User is associated to a clinic.  Make sure it matches the clinicNum passed in before adding them to the list of results.
					if(listUsers[j].ClinicNum==clinicNum) {
						if(listEmpNumsWithClinic.Contains(listEmpsShort[i].EmployeeNum)) {
							continue;//Employee already added to the results.
						}
						listEmpNumsWithClinic.Add(listEmpsShort[i].EmployeeNum);
						listEmpsWithClinic.Add(listEmpsShort[i]);
					}
				}
			}
			//Returning the 'All' employee list was handled above.  We now only care about two scenarios.
			//1 - Returning a list of 'unassigned' employees.  This is used for the 'Headquarters' clinic filter.
			//2 - Returning a list of employees associated to the specific clinic passed in.
			if(clinicNum==0 && !isAll) {
				return listEmpsUnassigned;
			}
			return listEmpsWithClinic;
		}

		/// <summary> Returns -1 if employeeNum is not found.  0 if not hidden and 1 if hidden.</summary>		
		public static int IsHidden(long employeeNum) {
			//No need to check RemotingRole; no call to db.
			int rValue = -1;
			if (ListLong != null){
				for (int i = 0; i < ListLong.Length; i++){
					if (ListLong[i].EmployeeNum == employeeNum){
						rValue = (ListLong[i].IsHidden ? 1 : 0);
						i = ListLong.Length;
					}
				}
			}
			return rValue;
		}

		///<summary>Loops through List to find the given extension and returns the employeeNum if found.  Otherwise, returns -1;</summary>
		public static long GetEmpNumAtExtension(int phoneExt) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].PhoneExt==phoneExt){
					return ListLong[i].EmployeeNum;
				}
			}
			return -1;
		}

		public static int SortByLastName(Employee x,Employee y) {
			return x.LName.CompareTo(y.LName);
		}

		public static int SortByFirstName(Employee x,Employee y) {
			return x.FName.CompareTo(y.FName);
		}
		
		/// <summary>sorting class used to sort Employee in various ways</summary>
		public class EmployeeComparer:IComparer<Employee> {
		
			private SortBy SortOn=SortBy.lastName;
			
			public EmployeeComparer(SortBy sortBy) {
				SortOn=sortBy;
			}
			
			public int Compare(Employee x,Employee y) {
				int ret=0;
				switch(SortOn) {
					case SortBy.empNum:
						ret=x.EmployeeNum.CompareTo(y.EmployeeNum); 
						break;
					case SortBy.ext:
						ret=x.PhoneExt.CompareTo(y.PhoneExt); 
						break;
					case SortBy.firstName:
						ret=x.FName.CompareTo(y.FName); 
						break;
					case SortBy.LFName:
						ret=x.LName.CompareTo(y.LName);
						if(ret==0) {
							ret=x.FName.CompareTo(y.FName);
						}
						break;
					case SortBy.lastName:
					default:
						ret=x.LName.CompareTo(y.LName); 
						break;
				}
				if(ret==0) {//last name is tie breaker
					return x.LName.CompareTo(y.LName);
				}
				//we got here so our sort was successful
				return ret;
			}

			public enum SortBy {
				///<summary>0 - By Extension.</summary>
				ext,
				///<summary>1 - By EmployeeNum.</summary>
				empNum,
				///<summary>2 - By FName.</summary>
				firstName,
				///<summary>3 - By LName.</summary>
				lastName,
				///<summary>4 - By LName, then FName.</summary>
				LFName
			};
		}
	}

	

	
	

}













