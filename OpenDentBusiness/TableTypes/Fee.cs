using System;
using System.Collections;

namespace OpenDentBusiness{

	///<summary>There is one entry in this table for each fee for a single procedurecode.  So if there are 5 different fees stored for one procedurecode, then there will be five entries here.</summary>
	[Serializable]
	[CrudTable(IsSynchable=true)]
	public class Fee:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long FeeNum;
		///<summary>The amount usually charged.  If an amount is unknown, then the entire Fee entry is deleted from the database.  
		///The absence of a fee is shown in the user interface as a blank entry.
		///For clinic and/or provider fees, amount can be set to -1 which indicates that their fee should be blank and not use the default fee.</summary>
		public double Amount;
		///<summary>Do not use.</summary>
		public string OldCode;
		///<summary>FK to feesched.FeeSchedNum.</summary>
		public long FeeSched;
		///<summary>Not used.</summary>
		public bool UseDefaultFee;
		///<summary>Not used.</summary>
		public bool UseDefaultCov;
		///<summary>FK to procedurecode.CodeNum.</summary>
		public long CodeNum;
		///<summary>FK to clinic.ClinicNum.  (Used if localization of fees for a feesched is enabled)</summary>
		public long ClinicNum;
		///<summary>FK to provider.ProvNum.  (Used if localization of fees for a feesched is enabled)</summary>
		public long ProvNum;

		///<summary></summary>
		public Fee Copy(){
			return (Fee)MemberwiseClone();
		}

	}

	

}













