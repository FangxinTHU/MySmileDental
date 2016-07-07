﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenDentBusiness {
	///<summary>This table is not part of the general release.  User would have to add it manually.  
	///All schema changes are done directly on our live database as needed.
	///This table is used to provide a history of a job based on when the status has changed.  
	///It is also used to display the current status of the job and the description. 
	///These will be created when the Owner or the Status change.</summary>
	[Serializable]
	[CrudTable(IsMissingInGeneral=true,IsSynchable=true)]
	//[CrudTable(IsSynchable=true)]
	public class JobEvent:TableBase {//JobHistory?
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long JobEventNum;
		///<summary>FK to job.JobNum.  Links this event to the source job.</summary>
		public long JobNum;
		///<summary>FK to userod.UserNum.  The owner of the job at the time the entry was made.  
		///Stored for viewing changes made to a job.</summary>
		public long OwnerNum;
		///<summary>Date/Time the event was created.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.DateTEntry)]
		public DateTime DateTimeEntry;
		///<summary>Copy of the job description at the time of the event creation.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TextIsClob)]
		public string Description;
		///<summary>The status of the referenced job at the time the entry was made.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.EnumAsString)]
		public JobStat JobStatus;

		///<summary></summary>
		public JobEvent Copy() {
			return (JobEvent)this.MemberwiseClone();
		}
	}

}



