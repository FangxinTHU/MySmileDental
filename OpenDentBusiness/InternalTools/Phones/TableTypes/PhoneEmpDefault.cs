using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness{
	///<summary>This table is not part of the general release.  User would have to add it manually.</summary>
	[Serializable]
	[CrudTable(IsMissingInGeneral=true)]
	public class PhoneEmpDefault:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long EmployeeNum;
		///<summary></summary>
		public bool IsGraphed;
		///<summary></summary>
		public bool HasColor;
		///<summary>Enum:AsteriskRingGroups 0=all, 1=none, 2=backup</summary>
		public AsteriskRingGroups RingGroups;
		///<summary>Just makes management easier.  Not used by the program.</summary>
		public string EmpName;
		///<summary>The phone extension for the employee.  e.g. 101,102,etc.  Used to be in the employee table.  This can be changed daily by staff who float from workstation to workstation.  Can be 0 in order to keep two rows from sharing the same extension.</summary>
		public int PhoneExt;
		///<summary>Enum:PhoneEmpStatusOverride </summary>
		public PhoneEmpStatusOverride StatusOverride;
		///<summary>Used to be stored as phoneoverride.Explanation.</summary>
		public string Notes;
		///<summary>This is used by the cameras.  Only necessary when the ip address doesn't match the 10.10.1.2xx pattern that we normally use.  For example, if Jordan sets this value to JORDANS, then the camera on JORDANS(.186) will send its images to the phone table where extension=104.  The second consequence is that .204 will not send any camera images.  This is used heavily by remote users working from home.  If a staff floats to another .2xx workstation, then this does not need to be set since it will match their changed extension with their current workstation ip address because if follows the normal pattern.  If there are multiple ip addresses, and the camera picks up the wrong one, setting this field can fix it.</summary>
		public string ComputerName;
		///<summary>Deprecated.  Always set to true because we no longer capture screen shots.</summary>
		public bool IsPrivateScreen;
		///<summary>Used to launch a task window instead of a commlog window when user clicks on name/phone number on the bottom left.</summary>
		public bool IsTriageOperator;
		///<summary>Order of escalation importantance. Employees are ranked 1-n in order of importance. 1 is most important, 'n' is least important. -1 means employee is not included in escalation.</summary>
		public int EscalationOrder;

		///<summary></summary>
		public PhoneEmpDefault Clone() {
			return (PhoneEmpDefault)this.MemberwiseClone();
		}
	}

	public enum AsteriskRingGroups {
		///<summary>0 - All really means both regular and backup. Most techs.  Default. This setting is used for employees with no entries in this table</summary>
		All,
		///<summary>1 - For example, Jordan and developers.</summary>
		None,
		///<summary>2 - For example, Nathan.</summary>
		Backup
	}

	public enum PhoneEmpStatusOverride {
		///<summary>0 - None.</summary>
		None,
		///<summary>1 </summary>
		Unavailable,
		///<summary>2</summary>
		OfflineAssist
	}

	/*CREATE TABLE phoneempdefault (  
		EmployeeNum BIGINT NOT NULL,      
		IsGraphed TINYINT NOT NULL,      
		HasColor TINYINT NOT NULL,      
		RingGroups INT NOT NULL,      
		EmpName VARCHAR(255) NOT NULL,      
		PRIMARY KEY (EmployeeNum)      
		) DEFAULT CHARSET=utf8; */

	
}




