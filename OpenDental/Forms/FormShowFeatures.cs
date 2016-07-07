using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormShowFeatures : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.CheckBox checkCapitation;
		private System.Windows.Forms.CheckBox checkMedicaid;
		private System.Windows.Forms.CheckBox checkAdvancedIns;
		private System.Windows.Forms.CheckBox checkClinical;
		private System.Windows.Forms.CheckBox checkBasicModules;
		private System.Windows.Forms.CheckBox checkPublicHealth;
		private System.Windows.Forms.CheckBox checkNoClinics;
		private System.Windows.Forms.CheckBox checkDentalSchools;
		private System.Windows.Forms.CheckBox checkRepeatCharges;
		private CheckBox checkInsurance;
		private CheckBox checkHospitals;
		private CheckBox checkMedicalIns;
		private CheckBox checkEhr;
		private CheckBox checkSuperFam;
		private Label label1;
		private CheckBox checkPatClone;
		private CheckBox checkQuestionnaire;
		private CheckBox checkTrojanCollect;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormShowFeatures()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormShowFeatures));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.checkCapitation = new System.Windows.Forms.CheckBox();
			this.checkMedicaid = new System.Windows.Forms.CheckBox();
			this.checkAdvancedIns = new System.Windows.Forms.CheckBox();
			this.checkClinical = new System.Windows.Forms.CheckBox();
			this.checkBasicModules = new System.Windows.Forms.CheckBox();
			this.checkPublicHealth = new System.Windows.Forms.CheckBox();
			this.checkNoClinics = new System.Windows.Forms.CheckBox();
			this.checkDentalSchools = new System.Windows.Forms.CheckBox();
			this.checkRepeatCharges = new System.Windows.Forms.CheckBox();
			this.checkInsurance = new System.Windows.Forms.CheckBox();
			this.checkHospitals = new System.Windows.Forms.CheckBox();
			this.checkMedicalIns = new System.Windows.Forms.CheckBox();
			this.checkEhr = new System.Windows.Forms.CheckBox();
			this.checkSuperFam = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.checkPatClone = new System.Windows.Forms.CheckBox();
			this.checkQuestionnaire = new System.Windows.Forms.CheckBox();
			this.checkTrojanCollect = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(377, 422);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 24);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(377, 381);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 24);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// checkCapitation
			// 
			this.checkCapitation.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkCapitation.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkCapitation.Location = new System.Drawing.Point(12, 37);
			this.checkCapitation.Name = "checkCapitation";
			this.checkCapitation.Size = new System.Drawing.Size(258, 19);
			this.checkCapitation.TabIndex = 2;
			this.checkCapitation.Text = "Capitation";
			this.checkCapitation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkMedicaid
			// 
			this.checkMedicaid.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkMedicaid.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkMedicaid.Location = new System.Drawing.Point(12, 61);
			this.checkMedicaid.Name = "checkMedicaid";
			this.checkMedicaid.Size = new System.Drawing.Size(258, 19);
			this.checkMedicaid.TabIndex = 3;
			this.checkMedicaid.Text = "Medicaid";
			this.checkMedicaid.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkAdvancedIns
			// 
			this.checkAdvancedIns.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkAdvancedIns.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAdvancedIns.Location = new System.Drawing.Point(12, 427);
			this.checkAdvancedIns.Name = "checkAdvancedIns";
			this.checkAdvancedIns.Size = new System.Drawing.Size(258, 19);
			this.checkAdvancedIns.TabIndex = 4;
			this.checkAdvancedIns.Text = "Advanced Insurance Fields (deprecated)";
			this.checkAdvancedIns.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkAdvancedIns.Visible = false;
			// 
			// checkClinical
			// 
			this.checkClinical.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkClinical.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkClinical.Location = new System.Drawing.Point(12, 181);
			this.checkClinical.Name = "checkClinical";
			this.checkClinical.Size = new System.Drawing.Size(258, 19);
			this.checkClinical.TabIndex = 5;
			this.checkClinical.Text = "Clinical (computers in operatories)";
			this.checkClinical.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkBasicModules
			// 
			this.checkBasicModules.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBasicModules.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBasicModules.Location = new System.Drawing.Point(12, 205);
			this.checkBasicModules.Name = "checkBasicModules";
			this.checkBasicModules.Size = new System.Drawing.Size(258, 19);
			this.checkBasicModules.TabIndex = 6;
			this.checkBasicModules.Text = "Basic Modules Only";
			this.checkBasicModules.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkPublicHealth
			// 
			this.checkPublicHealth.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkPublicHealth.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkPublicHealth.Location = new System.Drawing.Point(12, 85);
			this.checkPublicHealth.Name = "checkPublicHealth";
			this.checkPublicHealth.Size = new System.Drawing.Size(258, 19);
			this.checkPublicHealth.TabIndex = 7;
			this.checkPublicHealth.Text = "Public Health";
			this.checkPublicHealth.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkNoClinics
			// 
			this.checkNoClinics.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkNoClinics.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkNoClinics.Location = new System.Drawing.Point(12, 229);
			this.checkNoClinics.Name = "checkNoClinics";
			this.checkNoClinics.Size = new System.Drawing.Size(258, 19);
			this.checkNoClinics.TabIndex = 8;
			this.checkNoClinics.Text = "Clinics (multiple office locations)";
			this.checkNoClinics.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkNoClinics.Click += new System.EventHandler(this.checkNoClinics_Click);
			// 
			// checkDentalSchools
			// 
			this.checkDentalSchools.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkDentalSchools.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkDentalSchools.Location = new System.Drawing.Point(12, 109);
			this.checkDentalSchools.Name = "checkDentalSchools";
			this.checkDentalSchools.Size = new System.Drawing.Size(258, 19);
			this.checkDentalSchools.TabIndex = 9;
			this.checkDentalSchools.Text = "Dental Schools";
			this.checkDentalSchools.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkDentalSchools.Click += new System.EventHandler(this.checkDentalSchools_Click);
			// 
			// checkRepeatCharges
			// 
			this.checkRepeatCharges.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkRepeatCharges.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkRepeatCharges.Location = new System.Drawing.Point(12, 253);
			this.checkRepeatCharges.Name = "checkRepeatCharges";
			this.checkRepeatCharges.Size = new System.Drawing.Size(258, 19);
			this.checkRepeatCharges.TabIndex = 10;
			this.checkRepeatCharges.Text = "Repeating Charges";
			this.checkRepeatCharges.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkInsurance
			// 
			this.checkInsurance.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkInsurance.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkInsurance.Location = new System.Drawing.Point(12, 157);
			this.checkInsurance.Name = "checkInsurance";
			this.checkInsurance.Size = new System.Drawing.Size(258, 19);
			this.checkInsurance.TabIndex = 11;
			this.checkInsurance.Text = "All Insurance";
			this.checkInsurance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkInsurance.Click += new System.EventHandler(this.checkInsurance_Click);
			// 
			// checkHospitals
			// 
			this.checkHospitals.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkHospitals.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkHospitals.Location = new System.Drawing.Point(12, 133);
			this.checkHospitals.Name = "checkHospitals";
			this.checkHospitals.Size = new System.Drawing.Size(258, 19);
			this.checkHospitals.TabIndex = 12;
			this.checkHospitals.Text = "Hospitals";
			this.checkHospitals.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkMedicalIns
			// 
			this.checkMedicalIns.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkMedicalIns.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkMedicalIns.Location = new System.Drawing.Point(12, 278);
			this.checkMedicalIns.Name = "checkMedicalIns";
			this.checkMedicalIns.Size = new System.Drawing.Size(258, 19);
			this.checkMedicalIns.TabIndex = 13;
			this.checkMedicalIns.Text = "Medical Insurance";
			this.checkMedicalIns.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkEhr
			// 
			this.checkEhr.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkEhr.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkEhr.Location = new System.Drawing.Point(12, 303);
			this.checkEhr.Name = "checkEhr";
			this.checkEhr.Size = new System.Drawing.Size(258, 19);
			this.checkEhr.TabIndex = 14;
			this.checkEhr.Text = "EHR";
			this.checkEhr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkEhr.Click += new System.EventHandler(this.checkEhr_Click);
			// 
			// checkSuperFam
			// 
			this.checkSuperFam.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkSuperFam.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkSuperFam.Location = new System.Drawing.Point(12, 328);
			this.checkSuperFam.Name = "checkSuperFam";
			this.checkSuperFam.Size = new System.Drawing.Size(258, 19);
			this.checkSuperFam.TabIndex = 15;
			this.checkSuperFam.Text = "Super Families";
			this.checkSuperFam.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkSuperFam.Click += new System.EventHandler(this.checkSuperFam_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(443, 18);
			this.label1.TabIndex = 16;
			this.label1.Text = "The following settings will affect all computers.";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// checkPatClone
			// 
			this.checkPatClone.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkPatClone.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkPatClone.Location = new System.Drawing.Point(12, 352);
			this.checkPatClone.Name = "checkPatClone";
			this.checkPatClone.Size = new System.Drawing.Size(258, 19);
			this.checkPatClone.TabIndex = 17;
			this.checkPatClone.Text = "Patient Clone";
			this.checkPatClone.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkPatClone.Click += new System.EventHandler(this.checkPatClone_Click);
			// 
			// checkQuestionnaire
			// 
			this.checkQuestionnaire.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkQuestionnaire.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkQuestionnaire.Location = new System.Drawing.Point(12, 377);
			this.checkQuestionnaire.Name = "checkQuestionnaire";
			this.checkQuestionnaire.Size = new System.Drawing.Size(258, 19);
			this.checkQuestionnaire.TabIndex = 18;
			this.checkQuestionnaire.Text = "Questionnaire";
			this.checkQuestionnaire.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkTrojanCollect
			// 
			this.checkTrojanCollect.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkTrojanCollect.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkTrojanCollect.Location = new System.Drawing.Point(12, 402);
			this.checkTrojanCollect.Name = "checkTrojanCollect";
			this.checkTrojanCollect.Size = new System.Drawing.Size(258, 19);
			this.checkTrojanCollect.TabIndex = 19;
			this.checkTrojanCollect.Text = "Trojan Express Collect";
			this.checkTrojanCollect.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// FormShowFeatures
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(467, 467);
			this.Controls.Add(this.checkTrojanCollect);
			this.Controls.Add(this.checkQuestionnaire);
			this.Controls.Add(this.checkPatClone);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.checkSuperFam);
			this.Controls.Add(this.checkEhr);
			this.Controls.Add(this.checkMedicalIns);
			this.Controls.Add(this.checkHospitals);
			this.Controls.Add(this.checkInsurance);
			this.Controls.Add(this.checkRepeatCharges);
			this.Controls.Add(this.checkDentalSchools);
			this.Controls.Add(this.checkNoClinics);
			this.Controls.Add(this.checkPublicHealth);
			this.Controls.Add(this.checkBasicModules);
			this.Controls.Add(this.checkClinical);
			this.Controls.Add(this.checkAdvancedIns);
			this.Controls.Add(this.checkMedicaid);
			this.Controls.Add(this.checkCapitation);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormShowFeatures";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Show Features";
			this.Load += new System.EventHandler(this.FormShowFeatures_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormShowFeatures_Load(object sender, System.EventArgs e) {
			checkCapitation.Checked=!PrefC.GetBool(PrefName.EasyHideCapitation);
			checkMedicaid.Checked=!PrefC.GetBool(PrefName.EasyHideMedicaid);
			checkPublicHealth.Checked=!PrefC.GetBool(PrefName.EasyHidePublicHealth);
			checkDentalSchools.Checked=!PrefC.GetBool(PrefName.EasyHideDentalSchools);
			checkHospitals.Checked=!PrefC.GetBool(PrefName.EasyHideHospitals);
			checkInsurance.Checked=!PrefC.GetBool(PrefName.EasyHideInsurance);
			checkClinical.Checked=!PrefC.GetBool(PrefName.EasyHideClinical);
			checkBasicModules.Checked=PrefC.GetBool(PrefName.EasyBasicModules);
			checkNoClinics.Checked=!PrefC.GetBool(PrefName.EasyNoClinics);
			checkRepeatCharges.Checked=!PrefC.GetBool(PrefName.EasyHideRepeatCharges);
			checkMedicalIns.Checked=PrefC.GetBool(PrefName.ShowFeatureMedicalInsurance);
			checkEhr.Checked=PrefC.GetBool(PrefName.ShowFeatureEhr);
			checkSuperFam.Checked=PrefC.GetBool(PrefName.ShowFeatureSuperfamilies);
			checkPatClone.Checked=PrefC.GetBool(PrefName.ShowFeaturePatientClone);
			checkQuestionnaire.Checked=PrefC.GetBool(PrefName.AccountShowQuestionnaire);
			checkTrojanCollect.Checked=PrefC.GetBool(PrefName.AccountShowTrojanExpressCollect);
		}

		private void checkDentalSchools_Click(object sender,EventArgs e) {
			if(PrefC.GetBool(PrefName.EasyHideDentalSchools) && checkDentalSchools.Checked) {
				MsgBox.Show(this,"You will need to restart the program for the change to take effect.");
			}
		}

		private void checkNoClinics_Click(object sender,EventArgs e) {
			//Do not allow a change if any form of integrated texting is still enabled.
			if(SmsPhones.IsIntegratedTextingEnabled()) {
				MsgBox.Show(this,"Integrated texting still enabled. Unsign texting agreement(s) from eServices | Integrated Texting.");
				//Change it back to what the db has.
				checkNoClinics.Checked=!PrefC.GetBool(PrefName.EasyNoClinics);
				return;
			}
			MsgBox.Show(this,"You will need to restart the program for the change to take effect.");
		}

		private void checkEhr_Click(object sender,EventArgs e) {
			if(checkEhr.Checked && !File.Exists(ODFileUtils.CombinePaths(Application.StartupPath,"EHR.dll"))){
				checkEhr.Checked=false;
				MsgBox.Show(this,"EHR.dll could not be found.");
				return;
			}
			MsgBox.Show(this,"You will need to restart the program for the change to take effect.");
		}

		private void checkSuperFam_Click(object sender,EventArgs e) {
			if(PrefC.GetBool(PrefName.ShowFeatureSuperfamilies)!=checkSuperFam.Checked) {
				MsgBox.Show(this,"You will need to restart the program for the change to take effect.");
			}
		}

		private void checkPatClone_Click(object sender,EventArgs e) {
			if(PrefC.GetBool(PrefName.ShowFeaturePatientClone)!=checkPatClone.Checked) {
				MsgBox.Show(this,"You will need to restart the program for the change to take effect.");
			}
		}

		private void checkInsurance_Click(object sender,EventArgs e) {
			if(PrefC.GetBool(PrefName.EasyHideInsurance)!=checkInsurance.Checked) {
				MsgBox.Show(this,"You will need to restart the program for the change to take effect.");
			}
		}
		
		private void butOK_Click(object sender, System.EventArgs e) {
			Prefs.UpdateBool(PrefName.EasyHideCapitation,!checkCapitation.Checked);
			Prefs.UpdateBool(PrefName.EasyHideMedicaid,!checkMedicaid.Checked);
			Prefs.UpdateBool(PrefName.EasyHidePublicHealth,!checkPublicHealth.Checked);
			Prefs.UpdateBool(PrefName.EasyHideDentalSchools,!checkDentalSchools.Checked);
			Prefs.UpdateBool(PrefName.EasyHideHospitals,!checkHospitals.Checked);
			Prefs.UpdateBool(PrefName.EasyHideInsurance,!checkInsurance.Checked);
			Prefs.UpdateBool(PrefName.EasyHideClinical,!checkClinical.Checked);
			Prefs.UpdateBool(PrefName.EasyBasicModules,checkBasicModules.Checked);
			Prefs.UpdateBool(PrefName.EasyNoClinics,!checkNoClinics.Checked);
			Prefs.UpdateBool(PrefName.EasyHideRepeatCharges,!checkRepeatCharges.Checked);
			Prefs.UpdateBool(PrefName.ShowFeatureMedicalInsurance,checkMedicalIns.Checked);
			Prefs.UpdateBool(PrefName.ShowFeatureEhr,checkEhr.Checked);
			Prefs.UpdateBool(PrefName.ShowFeatureSuperfamilies,checkSuperFam.Checked);
			Prefs.UpdateBool(PrefName.ShowFeaturePatientClone,checkPatClone.Checked);
			Prefs.UpdateBool(PrefName.AccountShowQuestionnaire,checkQuestionnaire.Checked);
			Prefs.UpdateBool(PrefName.AccountShowTrojanExpressCollect,checkTrojanCollect.Checked);
			DataValid.SetInvalid(InvalidType.Prefs);
			//We should use ToolBut invalidation to redraw toolbars that could've been just enabled and stop forcing customers restarting.
			//DataValid.SetInvalid(InvalidType.ToolBut);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

	}
}





















