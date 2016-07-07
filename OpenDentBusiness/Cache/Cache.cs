﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness {
	public class Cache {
		/// <summary>This is only used in the RefreshCache methods.  Used instead of Meth.  The command is only used if not ClientWeb.</summary>
		public static DataTable GetTableRemotelyIfNeeded(MethodBase methodBase,string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(methodBase);
			}
			else {
				return Db.GetTable(command);
			}
		}

		///<summary>This is only called from the UI.  Its purpose is to refresh the cache for one type on both the workstation and server.  DataValid.SetInvalid() should be used when all other workstations in the office need to refresh their cache as well.</summary>
		public static void Refresh(InvalidType itype) {
			int intItype=(int)itype;
			RefreshCache(intItype.ToString());
		}
			 
		///<summary>itypesStr= comma-delimited list of int.  Called directly from UI in one spot.  Called from above repeatedly.  The end result is that both server and client have been properly refreshed.</summary>
		public static void RefreshCache(string itypesStr){
			DataSet ds=GetCacheDs(itypesStr);
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				//Because otherwise it was handled just fine as part of the GetCacheDs
				FillCache(ds,itypesStr);
			}
		}

		///<summary>This is an incomplete stub and should not be used very much yet.  This will get used when switching databases.  Switching databases is allowed from ClientWeb in the sense that the user can connect to a different server from the ChooseDatabase window.</summary>
		public static void ClearAllCache() {
			//AccountingAutoPays
			AccountingAutoPays.ClearCache();
			//AutoCodes
			AutoCodes.ClearCache();
			AutoCodeItems.ClearCache();
			AutoCodeConds.ClearCache();
			//etc...



			Prefs.ClearCache();
			//etc...


		}

		///<summary>If ClientWeb, then this method is instead run on the server, and the result passed back to the client.  And since it's ClientWeb, FillCache will be run on the client.</summary>
		public static DataSet GetCacheDs(string itypesStr){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetDS(MethodBase.GetCurrentMethod(),itypesStr);
			}
			//so this part below only happens if direct or server------------------------------------------------
			List<int> itypes=new List<int>();
			string[] strArray=itypesStr.Split(',');
			for(int i=0;i<strArray.Length;i++){
				itypes.Add(PIn.Int(strArray[i]));
			}
			bool isAll=false;
			if(itypes.Contains((int)InvalidType.AllLocal)){
				isAll=true;
			}
			DataSet ds=new DataSet();
			//All Internal OD Tables that are cached go here
			if(Prefs.IsODHQ()) {
				if(itypes.Contains((int)InvalidType.JobPermission) || isAll) {
					ds.Tables.Add(JobPermissions.RefreshCache());
				}
			}
			if(itypes.Contains((int)InvalidType.AccountingAutoPays) || isAll) {
				ds.Tables.Add(AccountingAutoPays.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.AppointmentTypes) || isAll) {
				ds.Tables.Add(AppointmentTypes.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.AutoCodes) || isAll){
				ds.Tables.Add(AutoCodes.RefreshCache());
				ds.Tables.Add(AutoCodeItems.RefreshCache());
				ds.Tables.Add(AutoCodeConds.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Automation) || isAll) {
				ds.Tables.Add(Automations.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.AutoNotes) || isAll) {
				ds.Tables.Add(AutoNotes.RefreshCache());
				ds.Tables.Add(AutoNoteControls.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Carriers) || isAll){
				ds.Tables.Add(Carriers.RefreshCache());//run on startup, after telephone reformat, after list edit.
			}
			if(itypes.Contains((int)InvalidType.ClaimForms) || isAll){
				ds.Tables.Add(ClaimFormItems.RefreshCache());
				ds.Tables.Add(ClaimForms.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.ClearHouses) || isAll){
				ds.Tables.Add(Clearinghouses.RefreshCacheHq());//kh wants to add an EasyHideClearHouses to disable this
			}
			//InvalidType.Clinics see InvalidType.Providers
			if(itypes.Contains((int)InvalidType.Computers) || isAll){
				ds.Tables.Add(Computers.RefreshCache());
				ds.Tables.Add(Printers.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Defs) || isAll){
				ds.Tables.Add(Defs.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.DentalSchools) || isAll){
				ds.Tables.Add(SchoolClasses.RefreshCache());
				ds.Tables.Add(SchoolCourses.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.DictCustoms) || isAll) {
				ds.Tables.Add(DictCustoms.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Diseases) || isAll) {
				ds.Tables.Add(DiseaseDefs.RefreshCache());
				ds.Tables.Add(ICD9s.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.DisplayFields) || isAll) {
				ds.Tables.Add(DisplayFields.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Ebills) || isAll) {
				ds.Tables.Add(Ebills.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.EhrCodes)) {
				EhrCodes.UpdateList();//Unusual pattern for an unusual "table".  Not really a table, but a mishmash of hard coded partial code systems that are needed for CQMs.
			}
			if(itypes.Contains((int)InvalidType.ElectIDs) || isAll) {
				ds.Tables.Add(ElectIDs.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Email) || isAll){
				ds.Tables.Add(EmailAddresses.RefreshCache());
				ds.Tables.Add(EmailTemplates.RefreshCache());
				ds.Tables.Add(EmailAutographs.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Employees) || isAll){
				ds.Tables.Add(Employees.RefreshCache());
				ds.Tables.Add(PayPeriods.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Employers) || isAll) {
				ds.Tables.Add(Employers.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Fees) || isAll){
				ds.Tables.Add(Fees.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.FeeScheds) || isAll){
				ds.Tables.Add(FeeScheds.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.HL7Defs) || isAll) {
				ds.Tables.Add(HL7Defs.RefreshCache());
				ds.Tables.Add(HL7DefMessages.RefreshCache());
				ds.Tables.Add(HL7DefSegments.RefreshCache());
				ds.Tables.Add(HL7DefFields.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.InsCats) || isAll){
				ds.Tables.Add(CovCats.RefreshCache());
				ds.Tables.Add(CovSpans.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.InsFilingCodes) || isAll){
				ds.Tables.Add(InsFilingCodes.RefreshCache());
				ds.Tables.Add(InsFilingCodeSubtypes.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Languages) || isAll) {
				if(CultureInfo.CurrentCulture.Name!="en-US") {
					ds.Tables.Add(Lans.RefreshCache());
				}
			}
			if(itypes.Contains((int)InvalidType.Letters) || isAll){
				ds.Tables.Add(Letters.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.LetterMerge) || isAll){
				ds.Tables.Add(LetterMergeFields.RefreshCache());
				ds.Tables.Add(LetterMerges.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Medications) || isAll) {
				ds.Tables.Add(Medications.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Operatories) || isAll){
				ds.Tables.Add(Operatories.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.PatFields) || isAll) {
				ds.Tables.Add(PatFieldDefs.RefreshCache());
				ds.Tables.Add(ApptFieldDefs.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Pharmacies) || isAll){
				ds.Tables.Add(Pharmacies.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Prefs) || isAll){
				ds.Tables.Add(Prefs.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.ProcButtons) || isAll) {
				ds.Tables.Add(ProcButtons.RefreshCache());
				ds.Tables.Add(ProcButtonItems.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.ProcCodes) || isAll){
				ds.Tables.Add(ProcedureCodes.RefreshCache());
				ds.Tables.Add(ProcCodeNotes.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Programs) || isAll){
				ds.Tables.Add(Programs.RefreshCache());
				ds.Tables.Add(ProgramProperties.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.ProviderErxs) || isAll) {
				ds.Tables.Add(ProviderErxs.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.ProviderIdents) || isAll) {
				ds.Tables.Add(ProviderIdents.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Providers) || isAll){
				ds.Tables.Add(Providers.RefreshCache());
				//Refresh the clinics as well because InvalidType.Providers has a comment that says "also includes clinics".  Also, there currently isn't an itype for Clinics.
				ds.Tables.Add(Clinics.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.QuickPaste) || isAll){
				ds.Tables.Add(QuickPasteNotes.RefreshCache());
				ds.Tables.Add(QuickPasteCats.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.RecallTypes) || isAll){
				ds.Tables.Add(RecallTypes.RefreshCache());
				ds.Tables.Add(RecallTriggers.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.ReplicationServers) || isAll) {
				ds.Tables.Add(ReplicationServers.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.RequiredFields) || isAll) {
				ds.Tables.Add(RequiredFields.RefreshCache());
				ds.Tables.Add(RequiredFieldConditions.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Security) || isAll){
				ds.Tables.Add(Userods.RefreshCache());
				ds.Tables.Add(UserGroups.RefreshCache());
				ds.Tables.Add(GroupPermissions.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Sheets) || isAll){
				ds.Tables.Add(SheetDefs.RefreshCache());
				ds.Tables.Add(SheetFieldDefs.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Signals) || isAll) {
				ds.Tables.Add(SigElementDefs.RefreshCache());
				ds.Tables.Add(SigButDefElements.RefreshCache());
				ds.Tables.Add(SigButDefs.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Sites) || isAll){
				ds.Tables.Add(Sites.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Sops) || isAll) {  //InvalidType.Sops is currently never used 11/14/2014
				ds.Tables.Add(Sops.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.StateAbbrs) || isAll) {
				ds.Tables.Add(StateAbbrs.RefreshCache());
			}
			//InvalidTypes.Tasks not handled here.
			if(itypes.Contains((int)InvalidType.TimeCardRules) || isAll) {
				ds.Tables.Add(TimeCardRules.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.ToolBut) || isAll){
				ds.Tables.Add(ToolButItems.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Vaccines) || isAll) {
				ds.Tables.Add(VaccineDefs.RefreshCache());
				ds.Tables.Add(DrugManufacturers.RefreshCache());
				ds.Tables.Add(DrugUnits.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Views) || isAll){
				ds.Tables.Add(ApptViews.RefreshCache());
				ds.Tables.Add(ApptViewItems.RefreshCache());
				ds.Tables.Add(AppointmentRules.RefreshCache());
				ds.Tables.Add(ProcApptColors.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Wiki) || isAll) {
				ds.Tables.Add(WikiListHeaderWidths.RefreshCache());
				ds.Tables.Add(WikiPages.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.ZipCodes) || isAll){
				ds.Tables.Add(ZipCodes.RefreshCache());
			}
			return ds;
		}

		///<summary>only if ClientWeb</summary>
		public static void FillCache(DataSet ds,string itypesStr) {
			List<int> itypes=new List<int>();
			string[] strArray=itypesStr.Split(',');
			for(int i=0;i<strArray.Length;i++) {
				itypes.Add(PIn.Int(strArray[i]));
			}
			bool isAll=false;
			if(itypes.Contains((int)InvalidType.AllLocal)) {
				isAll=true;
			}
			//All Internal OD Tables that are cached go here
			if(Prefs.IsODHQ()) {
				if(itypes.Contains((int)InvalidType.JobPermission) || isAll) {
					ds.Tables.Add(JobPermissions.RefreshCache());
				}
			}
			if(itypes.Contains((int)InvalidType.AccountingAutoPays) || isAll) {
				AccountingAutoPays.FillCache(ds.Tables["AccountingAutoPay"]);
			}
			if(itypes.Contains((int)InvalidType.AppointmentTypes) || isAll) {
				AppointmentTypes.FillCache(ds.Tables["AppointmentType"]);
			}
			if(itypes.Contains((int)InvalidType.AutoCodes) || isAll) {
				AutoCodes.FillCache(ds.Tables["AutoCode"]);
				AutoCodeItems.FillCache(ds.Tables["AutoCodeItem"]);
				AutoCodeConds.FillCache(ds.Tables["AutoCodeCond"]);
			}
			if(itypes.Contains((int)InvalidType.Automation) || isAll) {
				Automations.FillCache(ds.Tables["Automation"]);
			}
			if(itypes.Contains((int)InvalidType.AutoNotes) || isAll) {
				AutoNotes.FillCache(ds.Tables["AutoNote"]);
				AutoNoteControls.FillCache(ds.Tables["AutoNoteControl"]);
			}
			if(itypes.Contains((int)InvalidType.Carriers) || isAll) {
				Carriers.FillCache(ds.Tables["Carrier"]);//run on startup, after telephone reformat, after list edit.
			}
			if(itypes.Contains((int)InvalidType.ClaimForms) || isAll) {
				ClaimFormItems.FillCache(ds.Tables["ClaimFormItem"]);
				ClaimForms.FillCache(ds.Tables["ClaimForm"]);
			}
			if(itypes.Contains((int)InvalidType.ClearHouses) || isAll) {
				Clearinghouses.FillCacheHq(ds.Tables["Clearinghouse"]);//kh wants to add an EasyHideClearHouses to disable this
			}
			if(itypes.Contains((int)InvalidType.Computers) || isAll) {
				Computers.FillCache(ds.Tables["Computer"]);
				Printers.FillCache(ds.Tables["Printer"]);
			}
			if(itypes.Contains((int)InvalidType.Defs) || isAll) {
				Defs.FillCache(ds.Tables["Def"]);
			}
			if(itypes.Contains((int)InvalidType.DentalSchools) || isAll) {
				SchoolClasses.FillCache(ds.Tables["SchoolClass"]);
				SchoolCourses.FillCache(ds.Tables["SchoolCourse"]);
			}
			if(itypes.Contains((int)InvalidType.DictCustoms) || isAll) {
				DictCustoms.FillCache(ds.Tables["DictCustom"]);
			}
			if(itypes.Contains((int)InvalidType.Diseases) || isAll) {
				DiseaseDefs.FillCache(ds.Tables["DiseaseDef"]);
				ICD9s.FillCache(ds.Tables["ICD9"]);
			}
			if(itypes.Contains((int)InvalidType.DisplayFields) || isAll) {
				DisplayFields.FillCache(ds.Tables["DisplayField"]);
			}
			if(itypes.Contains((int)InvalidType.Ebills) || isAll) {
				Ebills.FillCache(ds.Tables["Ebill"]);
			}
			if(itypes.Contains((int)InvalidType.ElectIDs) || isAll) {
				ElectIDs.FillCache(ds.Tables["ElectID"]);
			}
			if(itypes.Contains((int)InvalidType.Email) || isAll) {
				EmailAddresses.FillCache(ds.Tables["EmailAddress"]);
				EmailTemplates.FillCache(ds.Tables["EmailTemplate"]);
			}
			if(itypes.Contains((int)InvalidType.Employees) || isAll) {
				Employees.FillCache(ds.Tables["Employee"]);
				PayPeriods.FillCache(ds.Tables["PayPeriod"]);
			}
			if(itypes.Contains((int)InvalidType.Employers) || isAll) {
				Employers.FillCache(ds.Tables["Employer"]);
			}
			if(itypes.Contains((int)InvalidType.Fees) || isAll) {
				Fees.FillCache(ds.Tables["Fee"]);
			}
			if(itypes.Contains((int)InvalidType.FeeScheds) || isAll) {
				FeeScheds.FillCache(ds.Tables["FeeSched"]);
			}
			if(itypes.Contains((int)InvalidType.HL7Defs) || isAll) {
				HL7Defs.FillCache(ds.Tables["HL7Def"]);
				HL7DefMessages.FillCache(ds.Tables["HL7DefMessage"]);
				HL7DefSegments.FillCache(ds.Tables["HL7DefSegment"]);
				HL7DefFields.FillCache(ds.Tables["HL7DefField"]);
			}
			if(itypes.Contains((int)InvalidType.InsCats) || isAll) {
				CovCats.FillCache(ds.Tables["CovCat"]);
				CovSpans.FillCache(ds.Tables["CovSpan"]);
			}
			if(itypes.Contains((int)InvalidType.InsFilingCodes) || isAll){
				InsFilingCodes.FillCache(ds.Tables["InsFilingCode"]);
				InsFilingCodeSubtypes.FillCache(ds.Tables["InsFilingCodeSubtype"]);
			}
			if(itypes.Contains((int)InvalidType.Languages) || isAll) {
				Lans.FillCache(ds.Tables["Language"]);
			}
			if(itypes.Contains((int)InvalidType.Letters) || isAll) {
				Letters.FillCache(ds.Tables["Letter"]);
			}
			if(itypes.Contains((int)InvalidType.LetterMerge) || isAll) {
				LetterMergeFields.FillCache(ds.Tables["LetterMergeField"]);
				LetterMerges.FillCache(ds.Tables["LetterMerge"]);
			}
			if(itypes.Contains((int)InvalidType.Medications) || isAll) {
				Medications.FillCache(ds.Tables["Medications"]);
			}
			if(itypes.Contains((int)InvalidType.Operatories) || isAll) {
				Operatories.FillCache(ds.Tables["Operatory"]);
			}
			if(itypes.Contains((int)InvalidType.PatFields) || isAll) {
				PatFieldDefs.FillCache(ds.Tables["PatFieldDef"]);
				ApptFieldDefs.FillCache(ds.Tables["ApptFieldDef"]);
			}
			if(itypes.Contains((int)InvalidType.Pharmacies) || isAll) {
				Pharmacies.FillCache(ds.Tables["Pharmacy"]);
			}
			if(itypes.Contains((int)InvalidType.Prefs) || isAll) {
				Prefs.FillCache(ds.Tables["Pref"]);
			}
			if(itypes.Contains((int)InvalidType.ProcButtons) || isAll) {
				ProcButtons.FillCache(ds.Tables["ProcButton"]);
				ProcButtonItems.FillCache(ds.Tables["ProcButtonItem"]);
			}
			if(itypes.Contains((int)InvalidType.ProcCodes) || isAll) {
				ProcedureCodes.FillCache(ds.Tables["ProcedureCode"]);
				ProcCodeNotes.FillCache(ds.Tables["ProcCodeNote"]);
			}
			if(itypes.Contains((int)InvalidType.Programs) || isAll) {
				Programs.FillCache(ds.Tables["Program"]);
				ProgramProperties.FillCache(ds.Tables["ProgramProperty"]);
			}
			if(itypes.Contains((int)InvalidType.ProviderErxs) || isAll) {
				ProviderErxs.FillCache(ds.Tables["ProviderErx"]);
			}
			if(itypes.Contains((int)InvalidType.ProviderIdents) || isAll) {
				ProviderIdents.FillCache(ds.Tables["ProviderIdent"]);
			}
			if(itypes.Contains((int)InvalidType.Providers) || isAll) {
				Providers.FillCache(ds.Tables["Provider"]);
				//Refresh the clinics as well because InvalidType.Providers has a comment that says "also includes clinics".  Also, there currently isn't an itype for Clinics.
				Clinics.FillCache(ds.Tables["clinic"]);//Case must match the table name in Clinics.RefrechCache().
			}
			if(itypes.Contains((int)InvalidType.QuickPaste) || isAll) {
				QuickPasteNotes.FillCache(ds.Tables["QuickPasteNote"]);
				QuickPasteCats.FillCache(ds.Tables["QuickPasteCat"]);
			}
			if(itypes.Contains((int)InvalidType.RecallTypes) || isAll) {
				RecallTypes.FillCache(ds.Tables["RecallType"]);
				RecallTriggers.FillCache(ds.Tables["RecallTrigger"]);
			}
			if(itypes.Contains((int)InvalidType.ReplicationServers) || isAll) {
				ReplicationServers.FillCache(ds.Tables["ReplicationServer"]);
			}
			//if(itypes.Contains((int)InvalidType.RequiredFields) || isAll) {
			//	RequiredFields.FillCache(ds.Tables["RequiredField"]);
			//}
			if(itypes.Contains((int)InvalidType.Security) || isAll) {
				Userods.FillCache(ds.Tables["Userod"]);
				UserGroups.FillCache(ds.Tables["UserGroup"]);
			}
			if(itypes.Contains((int)InvalidType.Sheets) || isAll) {
				SheetDefs.FillCache(ds.Tables["SheetDef"]);
				SheetFieldDefs.FillCache(ds.Tables["SheetFieldDef"]);
			}
			if(itypes.Contains((int)InvalidType.Signals) || isAll) {
				SigElementDefs.FillCache(ds.Tables["SigElementDef"]);
				SigButDefs.FillCache(ds.Tables["SigButDef"]);//includes SigButDefElements.Refresh()
			}
			if(itypes.Contains((int)InvalidType.Sites) || isAll) {
				Sites.FillCache(ds.Tables["Site"]);
			}
			if(itypes.Contains((int)InvalidType.Sops) || isAll) {
				Sops.FillCache(ds.Tables["Sop"]);
			}
			if(itypes.Contains((int)InvalidType.StateAbbrs) || isAll) {
				StateAbbrs.FillCache(ds.Tables["StateAbbr"]);
			}
			if(itypes.Contains((int)InvalidType.TimeCardRules) || isAll) {
				TimeCardRules.FillCache(ds.Tables["TimeCardRule"]);
			}
			//InvalidTypes.Tasks not handled here.
			if(itypes.Contains((int)InvalidType.ToolBut) || isAll) {
				ToolButItems.FillCache(ds.Tables["ToolButItem"]);
			}
			if(itypes.Contains((int)InvalidType.Vaccines) || isAll) {
				VaccineDefs.FillCache(ds.Tables["VaccineDef"]);
				DrugManufacturers.FillCache(ds.Tables["DrugManufacturer"]);
				DrugUnits.FillCache(ds.Tables["DrugUnit"]);
			}
			if(itypes.Contains((int)InvalidType.Views) || isAll) {
				ApptViews.FillCache(ds.Tables["ApptView"]);
				ApptViewItems.FillCache(ds.Tables["ApptViewItem"]);
				AppointmentRules.FillCache(ds.Tables["AppointmentRule"]);
				ProcApptColors.FillCache(ds.Tables["ProcApptColor"]);
			}
			if(itypes.Contains((int)InvalidType.Wiki) || isAll) {
				WikiListHeaderWidths.FillCache(ds.Tables["WikiListHeaderWidth"]);
				WikiPages.FillCache(ds.Tables["WikiPage"]);
			}
			if(itypes.Contains((int)InvalidType.ZipCodes) || isAll) {
				ZipCodes.FillCache(ds.Tables["ZipCode"]);
			}
		}

		///<summary>Returns a list of all invalid types that are used for the cache.  Currently only called from DBM.</summary>
		public static List<InvalidType> GetAllCachedInvalidTypes() {
			List<InvalidType> listInvalidTypes=new List<InvalidType>();
			//Below is a list of all invalid types in the same order the appear in the InvalidType enum.  
			//Comment out any rows that are not used for cache table refreshes.  See Cache.GetCacheDs() for more info.
			//listInvalidTypes.Add(InvalidType.None);  //No need to send a signal
			//listInvalidTypes.Add(InvalidType.Date);  //Not used with any other flags, not cached
			//listInvalidTypes.Add(InvalidType.AllLocal);  //Deprecated
			//listInvalidTypes.Add(InvalidType.Task);  //Not used with any other flags, not cached
			listInvalidTypes.Add(InvalidType.ProcCodes);
			listInvalidTypes.Add(InvalidType.Prefs);
			listInvalidTypes.Add(InvalidType.Views);
			listInvalidTypes.Add(InvalidType.AutoCodes);
			listInvalidTypes.Add(InvalidType.Carriers);
			listInvalidTypes.Add(InvalidType.ClearHouses);
			listInvalidTypes.Add(InvalidType.Computers);
			listInvalidTypes.Add(InvalidType.InsCats);
			listInvalidTypes.Add(InvalidType.Employees);
			//listInvalidTypes.Add(InvalidType.StartupOld);  //Deprecated
			listInvalidTypes.Add(InvalidType.Defs);
			listInvalidTypes.Add(InvalidType.Email);
			listInvalidTypes.Add(InvalidType.Fees);
			listInvalidTypes.Add(InvalidType.Letters);
			listInvalidTypes.Add(InvalidType.QuickPaste);
			listInvalidTypes.Add(InvalidType.Security);
			listInvalidTypes.Add(InvalidType.Programs);
			listInvalidTypes.Add(InvalidType.ToolBut);
			listInvalidTypes.Add(InvalidType.Providers);
			listInvalidTypes.Add(InvalidType.ClaimForms);
			listInvalidTypes.Add(InvalidType.ZipCodes);
			listInvalidTypes.Add(InvalidType.LetterMerge);
			listInvalidTypes.Add(InvalidType.DentalSchools);
			listInvalidTypes.Add(InvalidType.Operatories);
			//listInvalidTypes.Add(InvalidType.TaskPopup);  //Not needed, not cached
			listInvalidTypes.Add(InvalidType.Sites);
			listInvalidTypes.Add(InvalidType.Pharmacies);
			listInvalidTypes.Add(InvalidType.Sheets);
			listInvalidTypes.Add(InvalidType.RecallTypes);
			listInvalidTypes.Add(InvalidType.FeeScheds);
			//listInvalidTypes.Add(InvalidType.PhoneNumbers);  //Internal only, not cached
			listInvalidTypes.Add(InvalidType.Signals);
			listInvalidTypes.Add(InvalidType.DisplayFields);
			listInvalidTypes.Add(InvalidType.PatFields);
			listInvalidTypes.Add(InvalidType.AccountingAutoPays);
			listInvalidTypes.Add(InvalidType.ProcButtons);
			listInvalidTypes.Add(InvalidType.Diseases);
			listInvalidTypes.Add(InvalidType.Languages);
			listInvalidTypes.Add(InvalidType.AutoNotes);
			listInvalidTypes.Add(InvalidType.ElectIDs);
			listInvalidTypes.Add(InvalidType.Employers);
			listInvalidTypes.Add(InvalidType.ProviderIdents);
			//listInvalidTypes.Add(InvalidType.ShutDownNow);  //Do not want to send shutdown signal
			listInvalidTypes.Add(InvalidType.InsFilingCodes);
			listInvalidTypes.Add(InvalidType.ReplicationServers);
			listInvalidTypes.Add(InvalidType.Automation);
			//listInvalidTypes.Add(InvalidType.PhoneAsteriskReload);  //Internal only, not cached
			listInvalidTypes.Add(InvalidType.TimeCardRules);
			listInvalidTypes.Add(InvalidType.Vaccines);
			listInvalidTypes.Add(InvalidType.HL7Defs);
			listInvalidTypes.Add(InvalidType.DictCustoms);
			listInvalidTypes.Add(InvalidType.Wiki);
			listInvalidTypes.Add(InvalidType.Sops);
			listInvalidTypes.Add(InvalidType.EhrCodes);
			listInvalidTypes.Add(InvalidType.AppointmentTypes);
			listInvalidTypes.Add(InvalidType.Medications);
			//listInvalidTypes.Add(InvalidType.SmsTextMsgReceivedUnreadCount);  //Special InvalidType that would break things if we sent, not cached
			listInvalidTypes.Add(InvalidType.ProviderErxs);
			//listInvalidTypes.Add(InvalidType.Jobs);  //Internal only, not needed
			//listInvalidTypes.Add(InvalidType.JobRoles);  //Internal only, not needed
			listInvalidTypes.Add(InvalidType.StateAbbrs);
			listInvalidTypes.Add(InvalidType.RequiredFields);
			listInvalidTypes.Add(InvalidType.Ebills);
			return listInvalidTypes;
		}

	}
}
