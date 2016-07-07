using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormModuleSetup:System.Windows.Forms.Form {
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private IContainer components;
		private System.Windows.Forms.TextBox textTreatNote;
		private System.Windows.Forms.CheckBox checkTreatPlanShowGraphics;
		private System.Windows.Forms.CheckBox checkTreatPlanShowCompleted;
		private System.Windows.Forms.CheckBox checkEclaimsSeparateTreatProv;
		private System.Windows.Forms.CheckBox checkBalancesDontSubtractIns;
		private System.Windows.Forms.CheckBox checkInsurancePlansShared;
		private CheckBox checkMedicalEclaimsEnabled;
		private CheckBox checkSolidBlockouts;
		private CheckBox checkAgingMonthly;
		private CheckBox checkBrokenApptNote;
		private ToolTip toolTip1;
		private CheckBox checkApptBubbleDelay;
		private CheckBox checkStoreCCnumbers;
		private CheckBox checkAppointmentBubblesDisabled;
		private Label label7;
		private ComboBox comboBrokenApptAdjType;
		private Label label12;
		private ComboBox comboFinanceChargeAdjType;
		private System.Windows.Forms.Label label1;
		private CheckBox checkApptExclamation;
		private CheckBox checkProviderIncomeShows;
		private TextBox textClaimAttachPath;
		private CheckBox checkAutoClearEntryStatus;
		private CheckBox checkShowFamilyCommByDefault;
		private Label label20;
		private CheckBox checkPPOpercentage;
		private ComboBox comboToothNomenclature;
		private Label labelToothNomenclature;
		private CheckBox checkClaimFormTreatDentSaysSigOnFile;
		private CheckBox checkAllowSettingProcsComplete;
		private Label label4;
		private Label label6;
		private ComboBox comboTimeDismissed;
		private Label label5;
		private ComboBox comboTimeSeated;
		private Label label3;
		private ComboBox comboTimeArrived;
		private CheckBox checkApptRefreshEveryMinute;
		private ComboBox comboBillingChargeAdjType;
		private CheckBox checkAllowedFeeSchedsAutomate;
		private CheckBox checkCoPayFeeScheduleBlankLikeZero;
		private CheckBox checkClaimsValidateACN;
		private CheckBox checkInsDefaultShowUCRonClaims;
		private CheckBox checkImagesModuleTreeIsCollapsed;
		private CheckBox checkRxSendNewToQueue;
		private TabControl tabControl1;
		private TabPage tabAppts;
		private TabPage tabFamily;
		private TabPage tabAccount;
		private TabPage tabTreatPlan;
		private TabPage tabChart;
		private TabPage tabImages;
		private TabPage tabManage;
		private UI.Button butProblemsIndicateNone;
		private TextBox textProblemsIndicateNone;
		private Label label8;
		private List<Def> listPosAdjTypes;
		private List<Def> listNegAdjTypes;
		private UI.Button butMedicationsIndicateNone;
		private TextBox textMedicationsIndicateNone;
		private Label label9;
		private CheckBox checkStoreCCTokens;
		private UI.Button butAllergiesIndicateNone;
		private TextBox textAllergiesIndicateNone;
		private Label label14;
		private Label label13;
		private ComboBox comboSearchBehavior;
		private CheckBox checkClaimMedTypeIsInstWhenInsPlanIsMedical;
		private CheckBox checkProcGroupNoteDoesAggregate;
		private Label label15;
		private ComboBox comboCobRule;
		private CheckBox checkMedicalFeeUsedForNewProcs;
		private bool _changed;
		private CheckBox checkAccountShowPaymentNums;
		private ComboBox comboTimeCardOvertimeFirstDayOfWeek;
		private Label label16;
		private CheckBox checkAppointmentTimeIsLocked;
		private CheckBox checkTextMsgOkStatusTreatAsNo;
		private TextBox textICD9DefaultForNewProcs;
		private Label labelIcdCodeDefault;
		private CheckBox checkInsDefaultAssignmentOfBenefits;
		private GroupBox groupBox1;
		private CheckBox checkIntermingleDefault;
		private CheckBox checkStatementShowReturnAddress;
		private CheckBox checkStatementShowProcBreakdown;
		private CheckBox checkShowCC;
		private CheckBox checkStatementShowNotes;
		private Label label2;
		private ComboBox comboUseChartNum;
		private Label label10;
		private Label label18;
		private ValidNumber textStatementsCalcDueDate;
		private ValidNum textPayPlansBillInAdvanceDays;
		private CheckBox checkStatementShowAdjNotes;
		private CheckBox checkProcLockingIsAllowed;
		private CheckBox checkTimeCardADP;
		private TextBox textDiscountPercentage;
		private Label labelDiscountPercentage;
		private ComboBox comboProcDiscountType;
		private Label label19;
		private CheckBox checkChartNonPatientWarn;
		private CheckBox checkTreatPlanItemized;
		private CheckBox checkFamPhiAccess;
		private CheckBox checkStatementsUseSheets;
		private CheckBox checkWaitingRoomFilterByView;
		private TextBox textApptBubNoteLength;
		private Label label21;
		private CheckBox checkBrokenApptCommLogWithProcedure;
		private CheckBox checkInsPPOsecWriteoffs;
		private TextBox textWaitRoomWarn;
		private Label label22;
		private Label label23;
		private Button butColor;
		private ColorDialog colorDialog;
		private Label label24;
		private ValidNum textBillingElectBatchMax;
		private CheckBox checkGoogleAddress;
		private Label label11;
		private TextBox textMedDefaultStopDays;
		private CheckBox checkDxIcdVersion;
		private UI.Button butDiagnosisCode;
		private CheckBox checkClaimsSendWindowValidateOnLoad;
		private CheckBox checkProvColorChart;
		private CheckBox checkPromptAutoSplit;
		private TextBox textInsWriteoffDescript;
		private Label label17;
		private CheckBox checkBrokenApptAdjustmentWithProcedure;
		private CheckBox checkTPSaveSigned;
		private CheckBox checkSelectProv;
		private CheckBox checkTreatPlanUseSheets;
		private CheckBox checkApptModuleDefaultToWeek;
		///<summary>Used to determine a specific tab to have opened upon load.  Only set via the constructor and only used during load.</summary>
		private int _selectedTab;

		///<summary>Default constructor.  Opens the form with the Appts tab selected.</summary>
		public FormModuleSetup():this(0) {
		}

		///<summary>Opens the form with the a specific tab selected.  Currently 0-6 are the only valid values.  Defaults to Appts tab if invalid value passed in.</summary>
		///<param name="selectedTab">0=Appts, 1=Family, 2=Account, 3=Treat' Plan, 4=Chart, 5=Images, 6=Manage</param>
		public FormModuleSetup(int selectedTab) {
			InitializeComponent();
			Lan.F(this);
			if(selectedTab<0 || selectedTab>6) {
				selectedTab=0;//Default to Appts tab.
			}
			_selectedTab=selectedTab;
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		private void InitializeComponent(){
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormModuleSetup));
			this.textTreatNote = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.checkTreatPlanShowGraphics = new System.Windows.Forms.CheckBox();
			this.checkTreatPlanShowCompleted = new System.Windows.Forms.CheckBox();
			this.checkClaimsValidateACN = new System.Windows.Forms.CheckBox();
			this.comboBillingChargeAdjType = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.checkClaimFormTreatDentSaysSigOnFile = new System.Windows.Forms.CheckBox();
			this.textClaimAttachPath = new System.Windows.Forms.TextBox();
			this.label20 = new System.Windows.Forms.Label();
			this.checkShowFamilyCommByDefault = new System.Windows.Forms.CheckBox();
			this.checkProviderIncomeShows = new System.Windows.Forms.CheckBox();
			this.checkEclaimsSeparateTreatProv = new System.Windows.Forms.CheckBox();
			this.label12 = new System.Windows.Forms.Label();
			this.comboFinanceChargeAdjType = new System.Windows.Forms.ComboBox();
			this.checkStoreCCnumbers = new System.Windows.Forms.CheckBox();
			this.checkAgingMonthly = new System.Windows.Forms.CheckBox();
			this.checkBalancesDontSubtractIns = new System.Windows.Forms.CheckBox();
			this.checkInsurancePlansShared = new System.Windows.Forms.CheckBox();
			this.checkMedicalEclaimsEnabled = new System.Windows.Forms.CheckBox();
			this.checkSolidBlockouts = new System.Windows.Forms.CheckBox();
			this.checkApptRefreshEveryMinute = new System.Windows.Forms.CheckBox();
			this.label6 = new System.Windows.Forms.Label();
			this.comboTimeDismissed = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.comboTimeSeated = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.comboTimeArrived = new System.Windows.Forms.ComboBox();
			this.checkApptExclamation = new System.Windows.Forms.CheckBox();
			this.label7 = new System.Windows.Forms.Label();
			this.checkBrokenApptNote = new System.Windows.Forms.CheckBox();
			this.comboBrokenApptAdjType = new System.Windows.Forms.ComboBox();
			this.checkApptBubbleDelay = new System.Windows.Forms.CheckBox();
			this.checkAppointmentBubblesDisabled = new System.Windows.Forms.CheckBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.labelToothNomenclature = new System.Windows.Forms.Label();
			this.checkAllowSettingProcsComplete = new System.Windows.Forms.CheckBox();
			this.comboToothNomenclature = new System.Windows.Forms.ComboBox();
			this.checkAutoClearEntryStatus = new System.Windows.Forms.CheckBox();
			this.checkPPOpercentage = new System.Windows.Forms.CheckBox();
			this.checkInsDefaultShowUCRonClaims = new System.Windows.Forms.CheckBox();
			this.checkCoPayFeeScheduleBlankLikeZero = new System.Windows.Forms.CheckBox();
			this.checkAllowedFeeSchedsAutomate = new System.Windows.Forms.CheckBox();
			this.checkImagesModuleTreeIsCollapsed = new System.Windows.Forms.CheckBox();
			this.checkRxSendNewToQueue = new System.Windows.Forms.CheckBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabAppts = new System.Windows.Forms.TabPage();
			this.checkBrokenApptAdjustmentWithProcedure = new System.Windows.Forms.CheckBox();
			this.label23 = new System.Windows.Forms.Label();
			this.butColor = new System.Windows.Forms.Button();
			this.textWaitRoomWarn = new System.Windows.Forms.TextBox();
			this.label22 = new System.Windows.Forms.Label();
			this.checkBrokenApptCommLogWithProcedure = new System.Windows.Forms.CheckBox();
			this.textApptBubNoteLength = new System.Windows.Forms.TextBox();
			this.label21 = new System.Windows.Forms.Label();
			this.checkWaitingRoomFilterByView = new System.Windows.Forms.CheckBox();
			this.label13 = new System.Windows.Forms.Label();
			this.comboSearchBehavior = new System.Windows.Forms.ComboBox();
			this.checkAppointmentTimeIsLocked = new System.Windows.Forms.CheckBox();
			this.tabFamily = new System.Windows.Forms.TabPage();
			this.checkSelectProv = new System.Windows.Forms.CheckBox();
			this.checkGoogleAddress = new System.Windows.Forms.CheckBox();
			this.checkInsPPOsecWriteoffs = new System.Windows.Forms.CheckBox();
			this.checkFamPhiAccess = new System.Windows.Forms.CheckBox();
			this.checkInsDefaultAssignmentOfBenefits = new System.Windows.Forms.CheckBox();
			this.checkTextMsgOkStatusTreatAsNo = new System.Windows.Forms.CheckBox();
			this.label15 = new System.Windows.Forms.Label();
			this.comboCobRule = new System.Windows.Forms.ComboBox();
			this.tabAccount = new System.Windows.Forms.TabPage();
			this.textInsWriteoffDescript = new System.Windows.Forms.TextBox();
			this.label17 = new System.Windows.Forms.Label();
			this.checkPromptAutoSplit = new System.Windows.Forms.CheckBox();
			this.checkStatementsUseSheets = new System.Windows.Forms.CheckBox();
			this.checkStoreCCTokens = new System.Windows.Forms.CheckBox();
			this.checkAccountShowPaymentNums = new System.Windows.Forms.CheckBox();
			this.checkClaimMedTypeIsInstWhenInsPlanIsMedical = new System.Windows.Forms.CheckBox();
			this.tabTreatPlan = new System.Windows.Forms.TabPage();
			this.checkTreatPlanUseSheets = new System.Windows.Forms.CheckBox();
			this.checkTPSaveSigned = new System.Windows.Forms.CheckBox();
			this.checkTreatPlanItemized = new System.Windows.Forms.CheckBox();
			this.textDiscountPercentage = new System.Windows.Forms.TextBox();
			this.labelDiscountPercentage = new System.Windows.Forms.Label();
			this.comboProcDiscountType = new System.Windows.Forms.ComboBox();
			this.label19 = new System.Windows.Forms.Label();
			this.tabChart = new System.Windows.Forms.TabPage();
			this.checkProvColorChart = new System.Windows.Forms.CheckBox();
			this.label11 = new System.Windows.Forms.Label();
			this.textMedDefaultStopDays = new System.Windows.Forms.TextBox();
			this.butDiagnosisCode = new OpenDental.UI.Button();
			this.checkDxIcdVersion = new System.Windows.Forms.CheckBox();
			this.checkChartNonPatientWarn = new System.Windows.Forms.CheckBox();
			this.checkProcLockingIsAllowed = new System.Windows.Forms.CheckBox();
			this.textICD9DefaultForNewProcs = new System.Windows.Forms.TextBox();
			this.checkMedicalFeeUsedForNewProcs = new System.Windows.Forms.CheckBox();
			this.checkProcGroupNoteDoesAggregate = new System.Windows.Forms.CheckBox();
			this.butAllergiesIndicateNone = new OpenDental.UI.Button();
			this.textAllergiesIndicateNone = new System.Windows.Forms.TextBox();
			this.labelIcdCodeDefault = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.butMedicationsIndicateNone = new OpenDental.UI.Button();
			this.textMedicationsIndicateNone = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.butProblemsIndicateNone = new OpenDental.UI.Button();
			this.textProblemsIndicateNone = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.tabImages = new System.Windows.Forms.TabPage();
			this.tabManage = new System.Windows.Forms.TabPage();
			this.checkClaimsSendWindowValidateOnLoad = new System.Windows.Forms.CheckBox();
			this.checkTimeCardADP = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label24 = new System.Windows.Forms.Label();
			this.textBillingElectBatchMax = new OpenDental.ValidNum();
			this.checkStatementShowAdjNotes = new System.Windows.Forms.CheckBox();
			this.checkIntermingleDefault = new System.Windows.Forms.CheckBox();
			this.checkStatementShowReturnAddress = new System.Windows.Forms.CheckBox();
			this.checkStatementShowProcBreakdown = new System.Windows.Forms.CheckBox();
			this.checkShowCC = new System.Windows.Forms.CheckBox();
			this.checkStatementShowNotes = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.comboUseChartNum = new System.Windows.Forms.ComboBox();
			this.label10 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.textStatementsCalcDueDate = new OpenDental.ValidNumber();
			this.textPayPlansBillInAdvanceDays = new OpenDental.ValidNum();
			this.comboTimeCardOvertimeFirstDayOfWeek = new System.Windows.Forms.ComboBox();
			this.label16 = new System.Windows.Forms.Label();
			this.colorDialog = new System.Windows.Forms.ColorDialog();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.checkApptModuleDefaultToWeek = new System.Windows.Forms.CheckBox();
			this.tabControl1.SuspendLayout();
			this.tabAppts.SuspendLayout();
			this.tabFamily.SuspendLayout();
			this.tabAccount.SuspendLayout();
			this.tabTreatPlan.SuspendLayout();
			this.tabChart.SuspendLayout();
			this.tabImages.SuspendLayout();
			this.tabManage.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// textTreatNote
			// 
			this.textTreatNote.AcceptsReturn = true;
			this.textTreatNote.Location = new System.Drawing.Point(77, 7);
			this.textTreatNote.Multiline = true;
			this.textTreatNote.Name = "textTreatNote";
			this.textTreatNote.Size = new System.Drawing.Size(363, 53);
			this.textTreatNote.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(28, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 52);
			this.label1.TabIndex = 35;
			this.label1.Text = "Default Note";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkTreatPlanShowGraphics
			// 
			this.checkTreatPlanShowGraphics.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkTreatPlanShowGraphics.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkTreatPlanShowGraphics.Location = new System.Drawing.Point(81, 62);
			this.checkTreatPlanShowGraphics.Name = "checkTreatPlanShowGraphics";
			this.checkTreatPlanShowGraphics.Size = new System.Drawing.Size(359, 17);
			this.checkTreatPlanShowGraphics.TabIndex = 46;
			this.checkTreatPlanShowGraphics.Text = "Show Graphical Tooth Chart";
			this.checkTreatPlanShowGraphics.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkTreatPlanShowCompleted
			// 
			this.checkTreatPlanShowCompleted.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkTreatPlanShowCompleted.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkTreatPlanShowCompleted.Location = new System.Drawing.Point(81, 79);
			this.checkTreatPlanShowCompleted.Name = "checkTreatPlanShowCompleted";
			this.checkTreatPlanShowCompleted.Size = new System.Drawing.Size(359, 17);
			this.checkTreatPlanShowCompleted.TabIndex = 47;
			this.checkTreatPlanShowCompleted.Text = "Show Completed Work on Graphical Tooth Chart";
			this.checkTreatPlanShowCompleted.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkClaimsValidateACN
			// 
			this.checkClaimsValidateACN.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkClaimsValidateACN.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkClaimsValidateACN.Location = new System.Drawing.Point(44, 215);
			this.checkClaimsValidateACN.Name = "checkClaimsValidateACN";
			this.checkClaimsValidateACN.Size = new System.Drawing.Size(396, 17);
			this.checkClaimsValidateACN.TabIndex = 194;
			this.checkClaimsValidateACN.Text = "Require ACN# in remarks on claims with ADDP group name";
			this.checkClaimsValidateACN.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboBillingChargeAdjType
			// 
			this.comboBillingChargeAdjType.FormattingEnabled = true;
			this.comboBillingChargeAdjType.Location = new System.Drawing.Point(278, 101);
			this.comboBillingChargeAdjType.MaxDropDownItems = 30;
			this.comboBillingChargeAdjType.Name = "comboBillingChargeAdjType";
			this.comboBillingChargeAdjType.Size = new System.Drawing.Size(163, 21);
			this.comboBillingChargeAdjType.TabIndex = 199;
			// 
			// label4
			// 
			this.label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label4.Location = new System.Drawing.Point(56, 82);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(221, 15);
			this.label4.TabIndex = 198;
			this.label4.Text = "Finance charge adj type";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkClaimFormTreatDentSaysSigOnFile
			// 
			this.checkClaimFormTreatDentSaysSigOnFile.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkClaimFormTreatDentSaysSigOnFile.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkClaimFormTreatDentSaysSigOnFile.Location = new System.Drawing.Point(59, 156);
			this.checkClaimFormTreatDentSaysSigOnFile.Name = "checkClaimFormTreatDentSaysSigOnFile";
			this.checkClaimFormTreatDentSaysSigOnFile.Size = new System.Drawing.Size(381, 17);
			this.checkClaimFormTreatDentSaysSigOnFile.TabIndex = 197;
			this.checkClaimFormTreatDentSaysSigOnFile.Text = "Claim Form treating provider shows Signature On File rather than name";
			this.checkClaimFormTreatDentSaysSigOnFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textClaimAttachPath
			// 
			this.textClaimAttachPath.Location = new System.Drawing.Point(243, 176);
			this.textClaimAttachPath.Name = "textClaimAttachPath";
			this.textClaimAttachPath.Size = new System.Drawing.Size(197, 20);
			this.textClaimAttachPath.TabIndex = 189;
			// 
			// label20
			// 
			this.label20.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label20.Location = new System.Drawing.Point(54, 179);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(188, 13);
			this.label20.TabIndex = 190;
			this.label20.Text = "Claim Attachment Export Path";
			this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkShowFamilyCommByDefault
			// 
			this.checkShowFamilyCommByDefault.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkShowFamilyCommByDefault.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowFamilyCommByDefault.Location = new System.Drawing.Point(59, 139);
			this.checkShowFamilyCommByDefault.Name = "checkShowFamilyCommByDefault";
			this.checkShowFamilyCommByDefault.Size = new System.Drawing.Size(381, 17);
			this.checkShowFamilyCommByDefault.TabIndex = 75;
			this.checkShowFamilyCommByDefault.Text = "Show Family Comm Entries By Default";
			this.checkShowFamilyCommByDefault.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkProviderIncomeShows
			// 
			this.checkProviderIncomeShows.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkProviderIncomeShows.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkProviderIncomeShows.Location = new System.Drawing.Point(59, 122);
			this.checkProviderIncomeShows.Name = "checkProviderIncomeShows";
			this.checkProviderIncomeShows.Size = new System.Drawing.Size(381, 17);
			this.checkProviderIncomeShows.TabIndex = 74;
			this.checkProviderIncomeShows.Text = "Show provider income transfer window after entering insurance payment";
			this.checkProviderIncomeShows.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkEclaimsSeparateTreatProv
			// 
			this.checkEclaimsSeparateTreatProv.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkEclaimsSeparateTreatProv.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkEclaimsSeparateTreatProv.Location = new System.Drawing.Point(94, 198);
			this.checkEclaimsSeparateTreatProv.Name = "checkEclaimsSeparateTreatProv";
			this.checkEclaimsSeparateTreatProv.Size = new System.Drawing.Size(346, 17);
			this.checkEclaimsSeparateTreatProv.TabIndex = 53;
			this.checkEclaimsSeparateTreatProv.Text = "On e-claims, send treating provider info for each separate procedure";
			this.checkEclaimsSeparateTreatProv.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label12
			// 
			this.label12.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label12.Location = new System.Drawing.Point(56, 104);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(221, 15);
			this.label12.TabIndex = 73;
			this.label12.Text = "Billing charge adj type";
			this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// comboFinanceChargeAdjType
			// 
			this.comboFinanceChargeAdjType.FormattingEnabled = true;
			this.comboFinanceChargeAdjType.Location = new System.Drawing.Point(278, 77);
			this.comboFinanceChargeAdjType.MaxDropDownItems = 30;
			this.comboFinanceChargeAdjType.Name = "comboFinanceChargeAdjType";
			this.comboFinanceChargeAdjType.Size = new System.Drawing.Size(163, 21);
			this.comboFinanceChargeAdjType.TabIndex = 72;
			// 
			// checkStoreCCnumbers
			// 
			this.checkStoreCCnumbers.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkStoreCCnumbers.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkStoreCCnumbers.Location = new System.Drawing.Point(72, 41);
			this.checkStoreCCnumbers.Name = "checkStoreCCnumbers";
			this.checkStoreCCnumbers.Size = new System.Drawing.Size(368, 17);
			this.checkStoreCCnumbers.TabIndex = 67;
			this.checkStoreCCnumbers.Text = "Allow storing credit card numbers (this is a security risk)";
			this.checkStoreCCnumbers.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkStoreCCnumbers.UseVisualStyleBackColor = true;
			// 
			// checkAgingMonthly
			// 
			this.checkAgingMonthly.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkAgingMonthly.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAgingMonthly.Location = new System.Drawing.Point(72, 24);
			this.checkAgingMonthly.Name = "checkAgingMonthly";
			this.checkAgingMonthly.Size = new System.Drawing.Size(368, 17);
			this.checkAgingMonthly.TabIndex = 57;
			this.checkAgingMonthly.Text = "Aging calculated monthly instead of daily";
			this.checkAgingMonthly.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkBalancesDontSubtractIns
			// 
			this.checkBalancesDontSubtractIns.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBalancesDontSubtractIns.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBalancesDontSubtractIns.Location = new System.Drawing.Point(72, 7);
			this.checkBalancesDontSubtractIns.Name = "checkBalancesDontSubtractIns";
			this.checkBalancesDontSubtractIns.Size = new System.Drawing.Size(368, 17);
			this.checkBalancesDontSubtractIns.TabIndex = 55;
			this.checkBalancesDontSubtractIns.Text = "Balances don\'t subtract insurance estimate";
			this.checkBalancesDontSubtractIns.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkInsurancePlansShared
			// 
			this.checkInsurancePlansShared.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkInsurancePlansShared.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkInsurancePlansShared.Location = new System.Drawing.Point(34, 24);
			this.checkInsurancePlansShared.Name = "checkInsurancePlansShared";
			this.checkInsurancePlansShared.Size = new System.Drawing.Size(406, 17);
			this.checkInsurancePlansShared.TabIndex = 58;
			this.checkInsurancePlansShared.Text = "InsPlan option at bottom, \'Change Plan for all subscribers\', is default.";
			this.checkInsurancePlansShared.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkMedicalEclaimsEnabled
			// 
			this.checkMedicalEclaimsEnabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkMedicalEclaimsEnabled.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkMedicalEclaimsEnabled.Location = new System.Drawing.Point(94, 7);
			this.checkMedicalEclaimsEnabled.Name = "checkMedicalEclaimsEnabled";
			this.checkMedicalEclaimsEnabled.Size = new System.Drawing.Size(346, 17);
			this.checkMedicalEclaimsEnabled.TabIndex = 60;
			this.checkMedicalEclaimsEnabled.Text = "Enable medical e-claims";
			this.checkMedicalEclaimsEnabled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkMedicalEclaimsEnabled.Visible = false;
			// 
			// checkSolidBlockouts
			// 
			this.checkSolidBlockouts.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkSolidBlockouts.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkSolidBlockouts.Location = new System.Drawing.Point(32, 41);
			this.checkSolidBlockouts.Name = "checkSolidBlockouts";
			this.checkSolidBlockouts.Size = new System.Drawing.Size(408, 17);
			this.checkSolidBlockouts.TabIndex = 66;
			this.checkSolidBlockouts.Text = "Use solid blockouts instead of outlines on the appointment book";
			this.checkSolidBlockouts.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkSolidBlockouts.UseVisualStyleBackColor = true;
			// 
			// checkApptRefreshEveryMinute
			// 
			this.checkApptRefreshEveryMinute.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkApptRefreshEveryMinute.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkApptRefreshEveryMinute.Location = new System.Drawing.Point(34, 222);
			this.checkApptRefreshEveryMinute.Name = "checkApptRefreshEveryMinute";
			this.checkApptRefreshEveryMinute.Size = new System.Drawing.Size(406, 17);
			this.checkApptRefreshEveryMinute.TabIndex = 198;
			this.checkApptRefreshEveryMinute.Text = "Refresh every 60 seconds.  Keeps waiting room times refreshed.";
			this.checkApptRefreshEveryMinute.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label6.Location = new System.Drawing.Point(29, 201);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(247, 15);
			this.label6.TabIndex = 78;
			this.label6.Text = "Time Dismissed trigger";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// comboTimeDismissed
			// 
			this.comboTimeDismissed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboTimeDismissed.FormattingEnabled = true;
			this.comboTimeDismissed.Location = new System.Drawing.Point(278, 197);
			this.comboTimeDismissed.MaxDropDownItems = 30;
			this.comboTimeDismissed.Name = "comboTimeDismissed";
			this.comboTimeDismissed.Size = new System.Drawing.Size(163, 21);
			this.comboTimeDismissed.TabIndex = 77;
			// 
			// label5
			// 
			this.label5.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label5.Location = new System.Drawing.Point(29, 179);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(247, 15);
			this.label5.TabIndex = 76;
			this.label5.Text = "Time Seated (in op) trigger";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// comboTimeSeated
			// 
			this.comboTimeSeated.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboTimeSeated.FormattingEnabled = true;
			this.comboTimeSeated.Location = new System.Drawing.Point(278, 175);
			this.comboTimeSeated.MaxDropDownItems = 30;
			this.comboTimeSeated.Name = "comboTimeSeated";
			this.comboTimeSeated.Size = new System.Drawing.Size(163, 21);
			this.comboTimeSeated.TabIndex = 75;
			// 
			// label3
			// 
			this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label3.Location = new System.Drawing.Point(29, 157);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(247, 15);
			this.label3.TabIndex = 74;
			this.label3.Text = "Time Arrived trigger";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// comboTimeArrived
			// 
			this.comboTimeArrived.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboTimeArrived.FormattingEnabled = true;
			this.comboTimeArrived.Location = new System.Drawing.Point(278, 153);
			this.comboTimeArrived.MaxDropDownItems = 30;
			this.comboTimeArrived.Name = "comboTimeArrived";
			this.comboTimeArrived.Size = new System.Drawing.Size(163, 21);
			this.comboTimeArrived.TabIndex = 73;
			// 
			// checkApptExclamation
			// 
			this.checkApptExclamation.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkApptExclamation.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkApptExclamation.Location = new System.Drawing.Point(55, 133);
			this.checkApptExclamation.Name = "checkApptExclamation";
			this.checkApptExclamation.Size = new System.Drawing.Size(385, 17);
			this.checkApptExclamation.TabIndex = 72;
			this.checkApptExclamation.Text = "Show ! at upper right of appts for ins not sent (might cause slowdown)";
			this.checkApptExclamation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkApptExclamation.UseVisualStyleBackColor = true;
			// 
			// label7
			// 
			this.label7.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label7.Location = new System.Drawing.Point(15, 115);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(221, 15);
			this.label7.TabIndex = 71;
			this.label7.Text = "Broken appt default adj type";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkBrokenApptNote
			// 
			this.checkBrokenApptNote.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBrokenApptNote.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBrokenApptNote.Location = new System.Drawing.Point(32, 58);
			this.checkBrokenApptNote.Name = "checkBrokenApptNote";
			this.checkBrokenApptNote.Size = new System.Drawing.Size(408, 17);
			this.checkBrokenApptNote.TabIndex = 67;
			this.checkBrokenApptNote.Text = "Put broken appt note in Commlog instead of Adj (not recommended)";
			this.checkBrokenApptNote.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBrokenApptNote.UseVisualStyleBackColor = true;
			// 
			// comboBrokenApptAdjType
			// 
			this.comboBrokenApptAdjType.FormattingEnabled = true;
			this.comboBrokenApptAdjType.Location = new System.Drawing.Point(238, 111);
			this.comboBrokenApptAdjType.MaxDropDownItems = 30;
			this.comboBrokenApptAdjType.Name = "comboBrokenApptAdjType";
			this.comboBrokenApptAdjType.Size = new System.Drawing.Size(203, 21);
			this.comboBrokenApptAdjType.TabIndex = 70;
			// 
			// checkApptBubbleDelay
			// 
			this.checkApptBubbleDelay.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkApptBubbleDelay.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkApptBubbleDelay.Location = new System.Drawing.Point(32, 24);
			this.checkApptBubbleDelay.Name = "checkApptBubbleDelay";
			this.checkApptBubbleDelay.Size = new System.Drawing.Size(408, 17);
			this.checkApptBubbleDelay.TabIndex = 69;
			this.checkApptBubbleDelay.Text = "Appointment bubble popup delay";
			this.checkApptBubbleDelay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkApptBubbleDelay.UseVisualStyleBackColor = true;
			// 
			// checkAppointmentBubblesDisabled
			// 
			this.checkAppointmentBubblesDisabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkAppointmentBubblesDisabled.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAppointmentBubblesDisabled.Location = new System.Drawing.Point(34, 7);
			this.checkAppointmentBubblesDisabled.Name = "checkAppointmentBubblesDisabled";
			this.checkAppointmentBubblesDisabled.Size = new System.Drawing.Size(406, 17);
			this.checkAppointmentBubblesDisabled.TabIndex = 70;
			this.checkAppointmentBubblesDisabled.Text = "Appointment bubble popup disabled";
			this.checkAppointmentBubblesDisabled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkAppointmentBubblesDisabled.UseVisualStyleBackColor = true;
			// 
			// toolTip1
			// 
			this.toolTip1.AutomaticDelay = 0;
			this.toolTip1.AutoPopDelay = 600000;
			this.toolTip1.InitialDelay = 0;
			this.toolTip1.IsBalloon = true;
			this.toolTip1.ReshowDelay = 0;
			this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			this.toolTip1.ToolTipTitle = "Help";
			// 
			// labelToothNomenclature
			// 
			this.labelToothNomenclature.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.labelToothNomenclature.Location = new System.Drawing.Point(41, 48);
			this.labelToothNomenclature.Name = "labelToothNomenclature";
			this.labelToothNomenclature.Size = new System.Drawing.Size(144, 13);
			this.labelToothNomenclature.TabIndex = 194;
			this.labelToothNomenclature.Text = "Tooth Nomenclature";
			this.labelToothNomenclature.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkAllowSettingProcsComplete
			// 
			this.checkAllowSettingProcsComplete.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkAllowSettingProcsComplete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAllowSettingProcsComplete.Location = new System.Drawing.Point(26, 26);
			this.checkAllowSettingProcsComplete.Name = "checkAllowSettingProcsComplete";
			this.checkAllowSettingProcsComplete.Size = new System.Drawing.Size(414, 15);
			this.checkAllowSettingProcsComplete.TabIndex = 74;
			this.checkAllowSettingProcsComplete.Text = "Allow setting procedures complete.  (It\'s better to only set appointments complet" +
    "e)";
			this.checkAllowSettingProcsComplete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkAllowSettingProcsComplete.UseVisualStyleBackColor = true;
			// 
			// comboToothNomenclature
			// 
			this.comboToothNomenclature.FormattingEnabled = true;
			this.comboToothNomenclature.Location = new System.Drawing.Point(187, 45);
			this.comboToothNomenclature.Name = "comboToothNomenclature";
			this.comboToothNomenclature.Size = new System.Drawing.Size(254, 21);
			this.comboToothNomenclature.TabIndex = 193;
			// 
			// checkAutoClearEntryStatus
			// 
			this.checkAutoClearEntryStatus.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkAutoClearEntryStatus.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAutoClearEntryStatus.Location = new System.Drawing.Point(59, 9);
			this.checkAutoClearEntryStatus.Name = "checkAutoClearEntryStatus";
			this.checkAutoClearEntryStatus.Size = new System.Drawing.Size(381, 15);
			this.checkAutoClearEntryStatus.TabIndex = 73;
			this.checkAutoClearEntryStatus.Text = "Reset entry status to TreatPlan when switching patients";
			this.checkAutoClearEntryStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkAutoClearEntryStatus.UseVisualStyleBackColor = true;
			// 
			// checkPPOpercentage
			// 
			this.checkPPOpercentage.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkPPOpercentage.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkPPOpercentage.Location = new System.Drawing.Point(27, 41);
			this.checkPPOpercentage.Name = "checkPPOpercentage";
			this.checkPPOpercentage.Size = new System.Drawing.Size(413, 17);
			this.checkPPOpercentage.TabIndex = 192;
			this.checkPPOpercentage.Text = "Insurance defaults to PPO percentage instead of category percentage plan type";
			this.checkPPOpercentage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkInsDefaultShowUCRonClaims
			// 
			this.checkInsDefaultShowUCRonClaims.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkInsDefaultShowUCRonClaims.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkInsDefaultShowUCRonClaims.Location = new System.Drawing.Point(27, 92);
			this.checkInsDefaultShowUCRonClaims.Name = "checkInsDefaultShowUCRonClaims";
			this.checkInsDefaultShowUCRonClaims.Size = new System.Drawing.Size(413, 17);
			this.checkInsDefaultShowUCRonClaims.TabIndex = 196;
			this.checkInsDefaultShowUCRonClaims.Text = "Insurance plans default to show UCR fee on claims.";
			this.checkInsDefaultShowUCRonClaims.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkInsDefaultShowUCRonClaims.Click += new System.EventHandler(this.checkInsDefaultShowUCRonClaims_Click);
			// 
			// checkCoPayFeeScheduleBlankLikeZero
			// 
			this.checkCoPayFeeScheduleBlankLikeZero.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkCoPayFeeScheduleBlankLikeZero.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkCoPayFeeScheduleBlankLikeZero.Location = new System.Drawing.Point(27, 75);
			this.checkCoPayFeeScheduleBlankLikeZero.Name = "checkCoPayFeeScheduleBlankLikeZero";
			this.checkCoPayFeeScheduleBlankLikeZero.Size = new System.Drawing.Size(413, 17);
			this.checkCoPayFeeScheduleBlankLikeZero.TabIndex = 195;
			this.checkCoPayFeeScheduleBlankLikeZero.Text = "Co-pay fee schedules treat blank entries as zero.";
			this.checkCoPayFeeScheduleBlankLikeZero.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkAllowedFeeSchedsAutomate
			// 
			this.checkAllowedFeeSchedsAutomate.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkAllowedFeeSchedsAutomate.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAllowedFeeSchedsAutomate.Location = new System.Drawing.Point(27, 58);
			this.checkAllowedFeeSchedsAutomate.Name = "checkAllowedFeeSchedsAutomate";
			this.checkAllowedFeeSchedsAutomate.Size = new System.Drawing.Size(413, 17);
			this.checkAllowedFeeSchedsAutomate.TabIndex = 193;
			this.checkAllowedFeeSchedsAutomate.Text = "Use Blue Book";
			this.checkAllowedFeeSchedsAutomate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkAllowedFeeSchedsAutomate.Click += new System.EventHandler(this.checkAllowedFeeSchedsAutomate_Click);
			// 
			// checkImagesModuleTreeIsCollapsed
			// 
			this.checkImagesModuleTreeIsCollapsed.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkImagesModuleTreeIsCollapsed.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkImagesModuleTreeIsCollapsed.Location = new System.Drawing.Point(81, 7);
			this.checkImagesModuleTreeIsCollapsed.Name = "checkImagesModuleTreeIsCollapsed";
			this.checkImagesModuleTreeIsCollapsed.Size = new System.Drawing.Size(359, 17);
			this.checkImagesModuleTreeIsCollapsed.TabIndex = 47;
			this.checkImagesModuleTreeIsCollapsed.Text = "Document tree collapses when patient changes";
			this.checkImagesModuleTreeIsCollapsed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkRxSendNewToQueue
			// 
			this.checkRxSendNewToQueue.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkRxSendNewToQueue.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkRxSendNewToQueue.Location = new System.Drawing.Point(81, 7);
			this.checkRxSendNewToQueue.Name = "checkRxSendNewToQueue";
			this.checkRxSendNewToQueue.Size = new System.Drawing.Size(359, 17);
			this.checkRxSendNewToQueue.TabIndex = 47;
			this.checkRxSendNewToQueue.Text = "Send all new prescriptions to electronic queue";
			this.checkRxSendNewToQueue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabAppts);
			this.tabControl1.Controls.Add(this.tabFamily);
			this.tabControl1.Controls.Add(this.tabAccount);
			this.tabControl1.Controls.Add(this.tabTreatPlan);
			this.tabControl1.Controls.Add(this.tabChart);
			this.tabControl1.Controls.Add(this.tabImages);
			this.tabControl1.Controls.Add(this.tabManage);
			this.tabControl1.Location = new System.Drawing.Point(20, 10);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(474, 505);
			this.tabControl1.TabIndex = 196;
			// 
			// tabAppts
			// 
			this.tabAppts.BackColor = System.Drawing.SystemColors.Window;
			this.tabAppts.Controls.Add(this.checkApptModuleDefaultToWeek);
			this.tabAppts.Controls.Add(this.checkBrokenApptAdjustmentWithProcedure);
			this.tabAppts.Controls.Add(this.label23);
			this.tabAppts.Controls.Add(this.butColor);
			this.tabAppts.Controls.Add(this.textWaitRoomWarn);
			this.tabAppts.Controls.Add(this.label22);
			this.tabAppts.Controls.Add(this.checkBrokenApptCommLogWithProcedure);
			this.tabAppts.Controls.Add(this.textApptBubNoteLength);
			this.tabAppts.Controls.Add(this.label21);
			this.tabAppts.Controls.Add(this.checkWaitingRoomFilterByView);
			this.tabAppts.Controls.Add(this.label13);
			this.tabAppts.Controls.Add(this.comboSearchBehavior);
			this.tabAppts.Controls.Add(this.checkAppointmentTimeIsLocked);
			this.tabAppts.Controls.Add(this.checkApptRefreshEveryMinute);
			this.tabAppts.Controls.Add(this.checkAppointmentBubblesDisabled);
			this.tabAppts.Controls.Add(this.label6);
			this.tabAppts.Controls.Add(this.checkSolidBlockouts);
			this.tabAppts.Controls.Add(this.comboTimeDismissed);
			this.tabAppts.Controls.Add(this.checkApptBubbleDelay);
			this.tabAppts.Controls.Add(this.label5);
			this.tabAppts.Controls.Add(this.comboBrokenApptAdjType);
			this.tabAppts.Controls.Add(this.comboTimeSeated);
			this.tabAppts.Controls.Add(this.checkBrokenApptNote);
			this.tabAppts.Controls.Add(this.label3);
			this.tabAppts.Controls.Add(this.label7);
			this.tabAppts.Controls.Add(this.comboTimeArrived);
			this.tabAppts.Controls.Add(this.checkApptExclamation);
			this.tabAppts.Location = new System.Drawing.Point(4, 22);
			this.tabAppts.Name = "tabAppts";
			this.tabAppts.Padding = new System.Windows.Forms.Padding(3);
			this.tabAppts.Size = new System.Drawing.Size(466, 479);
			this.tabAppts.TabIndex = 0;
			this.tabAppts.Text = "Appts";
			// 
			// checkBrokenApptAdjustmentWithProcedure
			// 
			this.checkBrokenApptAdjustmentWithProcedure.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBrokenApptAdjustmentWithProcedure.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBrokenApptAdjustmentWithProcedure.Location = new System.Drawing.Point(32, 92);
			this.checkBrokenApptAdjustmentWithProcedure.Name = "checkBrokenApptAdjustmentWithProcedure";
			this.checkBrokenApptAdjustmentWithProcedure.Size = new System.Drawing.Size(408, 17);
			this.checkBrokenApptAdjustmentWithProcedure.TabIndex = 217;
			this.checkBrokenApptAdjustmentWithProcedure.Text = "Make adjustment in addition to broken appt procedure";
			this.checkBrokenApptAdjustmentWithProcedure.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBrokenApptAdjustmentWithProcedure.UseVisualStyleBackColor = true;
			// 
			// label23
			// 
			this.label23.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label23.Location = new System.Drawing.Point(167, 362);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(246, 16);
			this.label23.TabIndex = 216;
			this.label23.Text = "Waiting room alert color";
			this.label23.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butColor
			// 
			this.butColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.butColor.Location = new System.Drawing.Point(416, 358);
			this.butColor.Name = "butColor";
			this.butColor.Size = new System.Drawing.Size(24, 21);
			this.butColor.TabIndex = 215;
			this.butColor.Click += new System.EventHandler(this.butColor_Click);
			// 
			// textWaitRoomWarn
			// 
			this.textWaitRoomWarn.Location = new System.Drawing.Point(358, 332);
			this.textWaitRoomWarn.Name = "textWaitRoomWarn";
			this.textWaitRoomWarn.Size = new System.Drawing.Size(83, 20);
			this.textWaitRoomWarn.TabIndex = 214;
			// 
			// label22
			// 
			this.label22.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label22.Location = new System.Drawing.Point(109, 335);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(246, 16);
			this.label22.TabIndex = 213;
			this.label22.Text = "Waiting room alert time in minutes (0 to disable)";
			this.label22.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkBrokenApptCommLogWithProcedure
			// 
			this.checkBrokenApptCommLogWithProcedure.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBrokenApptCommLogWithProcedure.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBrokenApptCommLogWithProcedure.Location = new System.Drawing.Point(32, 75);
			this.checkBrokenApptCommLogWithProcedure.Name = "checkBrokenApptCommLogWithProcedure";
			this.checkBrokenApptCommLogWithProcedure.Size = new System.Drawing.Size(408, 17);
			this.checkBrokenApptCommLogWithProcedure.TabIndex = 61;
			this.checkBrokenApptCommLogWithProcedure.Text = "Put broken appt note in Commlog in addition to broken appt procedure";
			this.checkBrokenApptCommLogWithProcedure.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBrokenApptCommLogWithProcedure.UseVisualStyleBackColor = true;
			// 
			// textApptBubNoteLength
			// 
			this.textApptBubNoteLength.Location = new System.Drawing.Point(357, 287);
			this.textApptBubNoteLength.Name = "textApptBubNoteLength";
			this.textApptBubNoteLength.Size = new System.Drawing.Size(83, 20);
			this.textApptBubNoteLength.TabIndex = 211;
			// 
			// label21
			// 
			this.label21.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label21.Location = new System.Drawing.Point(108, 290);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(246, 16);
			this.label21.TabIndex = 210;
			this.label21.Text = "Appointment bubble max note length (0 for no limit)";
			this.label21.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkWaitingRoomFilterByView
			// 
			this.checkWaitingRoomFilterByView.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkWaitingRoomFilterByView.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkWaitingRoomFilterByView.Location = new System.Drawing.Point(34, 309);
			this.checkWaitingRoomFilterByView.Name = "checkWaitingRoomFilterByView";
			this.checkWaitingRoomFilterByView.Size = new System.Drawing.Size(406, 17);
			this.checkWaitingRoomFilterByView.TabIndex = 201;
			this.checkWaitingRoomFilterByView.Text = "Filter the waiting room based on the selected appointment view.";
			this.checkWaitingRoomFilterByView.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label13
			// 
			this.label13.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label13.Location = new System.Drawing.Point(6, 246);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(230, 15);
			this.label13.TabIndex = 200;
			this.label13.Text = "Search Behavior";
			this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// comboSearchBehavior
			// 
			this.comboSearchBehavior.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboSearchBehavior.FormattingEnabled = true;
			this.comboSearchBehavior.Location = new System.Drawing.Point(238, 243);
			this.comboSearchBehavior.MaxDropDownItems = 30;
			this.comboSearchBehavior.Name = "comboSearchBehavior";
			this.comboSearchBehavior.Size = new System.Drawing.Size(203, 21);
			this.comboSearchBehavior.TabIndex = 199;
			// 
			// checkAppointmentTimeIsLocked
			// 
			this.checkAppointmentTimeIsLocked.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkAppointmentTimeIsLocked.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAppointmentTimeIsLocked.Location = new System.Drawing.Point(34, 268);
			this.checkAppointmentTimeIsLocked.Name = "checkAppointmentTimeIsLocked";
			this.checkAppointmentTimeIsLocked.Size = new System.Drawing.Size(406, 17);
			this.checkAppointmentTimeIsLocked.TabIndex = 198;
			this.checkAppointmentTimeIsLocked.Text = "Appointment time locked by default";
			this.checkAppointmentTimeIsLocked.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkAppointmentTimeIsLocked.MouseUp += new System.Windows.Forms.MouseEventHandler(this.checkAppointmentTimeIsLocked_MouseUp);
			// 
			// tabFamily
			// 
			this.tabFamily.BackColor = System.Drawing.SystemColors.Window;
			this.tabFamily.Controls.Add(this.checkSelectProv);
			this.tabFamily.Controls.Add(this.checkGoogleAddress);
			this.tabFamily.Controls.Add(this.checkInsPPOsecWriteoffs);
			this.tabFamily.Controls.Add(this.checkFamPhiAccess);
			this.tabFamily.Controls.Add(this.checkInsDefaultAssignmentOfBenefits);
			this.tabFamily.Controls.Add(this.checkTextMsgOkStatusTreatAsNo);
			this.tabFamily.Controls.Add(this.label15);
			this.tabFamily.Controls.Add(this.comboCobRule);
			this.tabFamily.Controls.Add(this.checkInsDefaultShowUCRonClaims);
			this.tabFamily.Controls.Add(this.checkMedicalEclaimsEnabled);
			this.tabFamily.Controls.Add(this.checkCoPayFeeScheduleBlankLikeZero);
			this.tabFamily.Controls.Add(this.checkInsurancePlansShared);
			this.tabFamily.Controls.Add(this.checkAllowedFeeSchedsAutomate);
			this.tabFamily.Controls.Add(this.checkPPOpercentage);
			this.tabFamily.Location = new System.Drawing.Point(4, 22);
			this.tabFamily.Name = "tabFamily";
			this.tabFamily.Padding = new System.Windows.Forms.Padding(3);
			this.tabFamily.Size = new System.Drawing.Size(466, 479);
			this.tabFamily.TabIndex = 1;
			this.tabFamily.Text = "Family";
			// 
			// checkSelectProv
			// 
			this.checkSelectProv.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkSelectProv.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkSelectProv.Location = new System.Drawing.Point(6, 218);
			this.checkSelectProv.Name = "checkSelectProv";
			this.checkSelectProv.Size = new System.Drawing.Size(434, 17);
			this.checkSelectProv.TabIndex = 216;
			this.checkSelectProv.Text = "Primary Provider defaults to \'Select Provider\' in patient edit and add family";
			this.checkSelectProv.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkGoogleAddress
			// 
			this.checkGoogleAddress.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkGoogleAddress.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkGoogleAddress.Location = new System.Drawing.Point(58, 201);
			this.checkGoogleAddress.Name = "checkGoogleAddress";
			this.checkGoogleAddress.Size = new System.Drawing.Size(382, 17);
			this.checkGoogleAddress.TabIndex = 215;
			this.checkGoogleAddress.Text = "Show Google Maps in patient edit";
			this.checkGoogleAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkInsPPOsecWriteoffs
			// 
			this.checkInsPPOsecWriteoffs.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkInsPPOsecWriteoffs.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkInsPPOsecWriteoffs.Location = new System.Drawing.Point(58, 184);
			this.checkInsPPOsecWriteoffs.Name = "checkInsPPOsecWriteoffs";
			this.checkInsPPOsecWriteoffs.Size = new System.Drawing.Size(382, 17);
			this.checkInsPPOsecWriteoffs.TabIndex = 214;
			this.checkInsPPOsecWriteoffs.Text = "Calculate secondary insurance PPO writeoffs (not recommended, see manual)";
			this.checkInsPPOsecWriteoffs.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkInsPPOsecWriteoffs.UseVisualStyleBackColor = true;
			// 
			// checkFamPhiAccess
			// 
			this.checkFamPhiAccess.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkFamPhiAccess.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkFamPhiAccess.Location = new System.Drawing.Point(27, 167);
			this.checkFamPhiAccess.Name = "checkFamPhiAccess";
			this.checkFamPhiAccess.Size = new System.Drawing.Size(413, 17);
			this.checkFamPhiAccess.TabIndex = 206;
			this.checkFamPhiAccess.Text = "Allow Guarantor access to family health information in patient portal";
			this.checkFamPhiAccess.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkInsDefaultAssignmentOfBenefits
			// 
			this.checkInsDefaultAssignmentOfBenefits.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkInsDefaultAssignmentOfBenefits.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkInsDefaultAssignmentOfBenefits.Location = new System.Drawing.Point(27, 109);
			this.checkInsDefaultAssignmentOfBenefits.Name = "checkInsDefaultAssignmentOfBenefits";
			this.checkInsDefaultAssignmentOfBenefits.Size = new System.Drawing.Size(413, 17);
			this.checkInsDefaultAssignmentOfBenefits.TabIndex = 204;
			this.checkInsDefaultAssignmentOfBenefits.Text = "Insurance plans default to assignment of benefits.";
			this.checkInsDefaultAssignmentOfBenefits.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkInsDefaultAssignmentOfBenefits.Click += new System.EventHandler(this.checkInsDefaultAssignmentOfBenefits_Click);
			// 
			// checkTextMsgOkStatusTreatAsNo
			// 
			this.checkTextMsgOkStatusTreatAsNo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkTextMsgOkStatusTreatAsNo.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkTextMsgOkStatusTreatAsNo.Location = new System.Drawing.Point(27, 151);
			this.checkTextMsgOkStatusTreatAsNo.Name = "checkTextMsgOkStatusTreatAsNo";
			this.checkTextMsgOkStatusTreatAsNo.Size = new System.Drawing.Size(413, 17);
			this.checkTextMsgOkStatusTreatAsNo.TabIndex = 203;
			this.checkTextMsgOkStatusTreatAsNo.Text = "Text Msg OK status, treat ?? as No instead of Yes";
			this.checkTextMsgOkStatusTreatAsNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label15
			// 
			this.label15.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label15.Location = new System.Drawing.Point(61, 132);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(247, 15);
			this.label15.TabIndex = 202;
			this.label15.Text = "Coordination of Benefits (COB) Rule";
			this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// comboCobRule
			// 
			this.comboCobRule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboCobRule.FormattingEnabled = true;
			this.comboCobRule.Location = new System.Drawing.Point(312, 128);
			this.comboCobRule.MaxDropDownItems = 30;
			this.comboCobRule.Name = "comboCobRule";
			this.comboCobRule.Size = new System.Drawing.Size(128, 21);
			this.comboCobRule.TabIndex = 201;
			this.comboCobRule.SelectionChangeCommitted += new System.EventHandler(this.comboCobRule_SelectionChangeCommitted);
			// 
			// tabAccount
			// 
			this.tabAccount.BackColor = System.Drawing.SystemColors.Window;
			this.tabAccount.Controls.Add(this.textInsWriteoffDescript);
			this.tabAccount.Controls.Add(this.label17);
			this.tabAccount.Controls.Add(this.checkPromptAutoSplit);
			this.tabAccount.Controls.Add(this.checkStatementsUseSheets);
			this.tabAccount.Controls.Add(this.checkStoreCCTokens);
			this.tabAccount.Controls.Add(this.checkAccountShowPaymentNums);
			this.tabAccount.Controls.Add(this.checkClaimMedTypeIsInstWhenInsPlanIsMedical);
			this.tabAccount.Controls.Add(this.checkClaimsValidateACN);
			this.tabAccount.Controls.Add(this.comboBillingChargeAdjType);
			this.tabAccount.Controls.Add(this.checkBalancesDontSubtractIns);
			this.tabAccount.Controls.Add(this.label4);
			this.tabAccount.Controls.Add(this.checkAgingMonthly);
			this.tabAccount.Controls.Add(this.checkClaimFormTreatDentSaysSigOnFile);
			this.tabAccount.Controls.Add(this.checkStoreCCnumbers);
			this.tabAccount.Controls.Add(this.comboFinanceChargeAdjType);
			this.tabAccount.Controls.Add(this.textClaimAttachPath);
			this.tabAccount.Controls.Add(this.label12);
			this.tabAccount.Controls.Add(this.label20);
			this.tabAccount.Controls.Add(this.checkEclaimsSeparateTreatProv);
			this.tabAccount.Controls.Add(this.checkShowFamilyCommByDefault);
			this.tabAccount.Controls.Add(this.checkProviderIncomeShows);
			this.tabAccount.Location = new System.Drawing.Point(4, 22);
			this.tabAccount.Name = "tabAccount";
			this.tabAccount.Size = new System.Drawing.Size(466, 479);
			this.tabAccount.TabIndex = 2;
			this.tabAccount.Text = "Account";
			// 
			// textInsWriteoffDescript
			// 
			this.textInsWriteoffDescript.Location = new System.Drawing.Point(278, 302);
			this.textInsWriteoffDescript.Name = "textInsWriteoffDescript";
			this.textInsWriteoffDescript.Size = new System.Drawing.Size(163, 20);
			this.textInsWriteoffDescript.TabIndex = 207;
			// 
			// label17
			// 
			this.label17.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label17.Location = new System.Drawing.Point(16, 304);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(261, 16);
			this.label17.TabIndex = 208;
			this.label17.Text = "PPO writeoff description (blank for \"Writeoff\")";
			this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkPromptAutoSplit
			// 
			this.checkPromptAutoSplit.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkPromptAutoSplit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkPromptAutoSplit.Location = new System.Drawing.Point(44, 282);
			this.checkPromptAutoSplit.Name = "checkPromptAutoSplit";
			this.checkPromptAutoSplit.Size = new System.Drawing.Size(396, 17);
			this.checkPromptAutoSplit.TabIndex = 206;
			this.checkPromptAutoSplit.Text = "Prompt for autosplits on Payments";
			this.checkPromptAutoSplit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkStatementsUseSheets
			// 
			this.checkStatementsUseSheets.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkStatementsUseSheets.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkStatementsUseSheets.Location = new System.Drawing.Point(44, 266);
			this.checkStatementsUseSheets.Name = "checkStatementsUseSheets";
			this.checkStatementsUseSheets.Size = new System.Drawing.Size(396, 17);
			this.checkStatementsUseSheets.TabIndex = 204;
			this.checkStatementsUseSheets.Text = "Statements use Sheets";
			this.checkStatementsUseSheets.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkStoreCCTokens
			// 
			this.checkStoreCCTokens.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkStoreCCTokens.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkStoreCCTokens.Location = new System.Drawing.Point(72, 58);
			this.checkStoreCCTokens.Name = "checkStoreCCTokens";
			this.checkStoreCCTokens.Size = new System.Drawing.Size(368, 17);
			this.checkStoreCCTokens.TabIndex = 203;
			this.checkStoreCCTokens.Text = "Automatically store credit card tokens";
			this.checkStoreCCTokens.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkStoreCCTokens.UseVisualStyleBackColor = true;
			// 
			// checkAccountShowPaymentNums
			// 
			this.checkAccountShowPaymentNums.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkAccountShowPaymentNums.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAccountShowPaymentNums.Location = new System.Drawing.Point(44, 249);
			this.checkAccountShowPaymentNums.Name = "checkAccountShowPaymentNums";
			this.checkAccountShowPaymentNums.Size = new System.Drawing.Size(396, 17);
			this.checkAccountShowPaymentNums.TabIndex = 194;
			this.checkAccountShowPaymentNums.Text = "Show Payment Numbers in Account Module";
			this.checkAccountShowPaymentNums.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkClaimMedTypeIsInstWhenInsPlanIsMedical
			// 
			this.checkClaimMedTypeIsInstWhenInsPlanIsMedical.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkClaimMedTypeIsInstWhenInsPlanIsMedical.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkClaimMedTypeIsInstWhenInsPlanIsMedical.Location = new System.Drawing.Point(44, 232);
			this.checkClaimMedTypeIsInstWhenInsPlanIsMedical.Name = "checkClaimMedTypeIsInstWhenInsPlanIsMedical";
			this.checkClaimMedTypeIsInstWhenInsPlanIsMedical.Size = new System.Drawing.Size(396, 17);
			this.checkClaimMedTypeIsInstWhenInsPlanIsMedical.TabIndex = 194;
			this.checkClaimMedTypeIsInstWhenInsPlanIsMedical.Text = "Set medical claims to institutional when using medical insurance.";
			this.checkClaimMedTypeIsInstWhenInsPlanIsMedical.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tabTreatPlan
			// 
			this.tabTreatPlan.BackColor = System.Drawing.SystemColors.Window;
			this.tabTreatPlan.Controls.Add(this.checkTreatPlanUseSheets);
			this.tabTreatPlan.Controls.Add(this.checkTPSaveSigned);
			this.tabTreatPlan.Controls.Add(this.checkTreatPlanItemized);
			this.tabTreatPlan.Controls.Add(this.textDiscountPercentage);
			this.tabTreatPlan.Controls.Add(this.labelDiscountPercentage);
			this.tabTreatPlan.Controls.Add(this.comboProcDiscountType);
			this.tabTreatPlan.Controls.Add(this.label19);
			this.tabTreatPlan.Controls.Add(this.label1);
			this.tabTreatPlan.Controls.Add(this.textTreatNote);
			this.tabTreatPlan.Controls.Add(this.checkTreatPlanShowCompleted);
			this.tabTreatPlan.Controls.Add(this.checkTreatPlanShowGraphics);
			this.tabTreatPlan.Location = new System.Drawing.Point(4, 22);
			this.tabTreatPlan.Name = "tabTreatPlan";
			this.tabTreatPlan.Size = new System.Drawing.Size(466, 479);
			this.tabTreatPlan.TabIndex = 3;
			this.tabTreatPlan.Text = "Treat\' Plan";
			// 
			// checkTreatPlanUseSheets
			// 
			this.checkTreatPlanUseSheets.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkTreatPlanUseSheets.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkTreatPlanUseSheets.Location = new System.Drawing.Point(138, 186);
			this.checkTreatPlanUseSheets.Name = "checkTreatPlanUseSheets";
			this.checkTreatPlanUseSheets.Size = new System.Drawing.Size(302, 17);
			this.checkTreatPlanUseSheets.TabIndex = 214;
			this.checkTreatPlanUseSheets.Text = "Treatment Plans use Sheets";
			this.checkTreatPlanUseSheets.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkTreatPlanUseSheets.UseVisualStyleBackColor = false;
			this.checkTreatPlanUseSheets.Click += new System.EventHandler(this.checkTreatPlanUseSheets_Click);
			// 
			// checkTPSaveSigned
			// 
			this.checkTPSaveSigned.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkTPSaveSigned.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkTPSaveSigned.Location = new System.Drawing.Point(138, 169);
			this.checkTPSaveSigned.Name = "checkTPSaveSigned";
			this.checkTPSaveSigned.Size = new System.Drawing.Size(302, 17);
			this.checkTPSaveSigned.TabIndex = 213;
			this.checkTPSaveSigned.Text = "Save Signed Treatment Plans to PDF";
			this.checkTPSaveSigned.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkTPSaveSigned.UseVisualStyleBackColor = false;
			// 
			// checkTreatPlanItemized
			// 
			this.checkTreatPlanItemized.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkTreatPlanItemized.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkTreatPlanItemized.Location = new System.Drawing.Point(300, 152);
			this.checkTreatPlanItemized.Name = "checkTreatPlanItemized";
			this.checkTreatPlanItemized.Size = new System.Drawing.Size(140, 17);
			this.checkTreatPlanItemized.TabIndex = 212;
			this.checkTreatPlanItemized.Text = "Itemize Treatment Plan";
			this.checkTreatPlanItemized.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkTreatPlanItemized.UseVisualStyleBackColor = false;
			this.checkTreatPlanItemized.Click += new System.EventHandler(this.checkTreatPlanItemized_Click);
			// 
			// textDiscountPercentage
			// 
			this.textDiscountPercentage.Location = new System.Drawing.Point(387, 129);
			this.textDiscountPercentage.Name = "textDiscountPercentage";
			this.textDiscountPercentage.Size = new System.Drawing.Size(53, 20);
			this.textDiscountPercentage.TabIndex = 211;
			// 
			// labelDiscountPercentage
			// 
			this.labelDiscountPercentage.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.labelDiscountPercentage.Location = new System.Drawing.Point(135, 132);
			this.labelDiscountPercentage.Name = "labelDiscountPercentage";
			this.labelDiscountPercentage.Size = new System.Drawing.Size(246, 16);
			this.labelDiscountPercentage.TabIndex = 210;
			this.labelDiscountPercentage.Text = "Procedure discount percentage";
			this.labelDiscountPercentage.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// comboProcDiscountType
			// 
			this.comboProcDiscountType.FormattingEnabled = true;
			this.comboProcDiscountType.Location = new System.Drawing.Point(277, 102);
			this.comboProcDiscountType.MaxDropDownItems = 30;
			this.comboProcDiscountType.Name = "comboProcDiscountType";
			this.comboProcDiscountType.Size = new System.Drawing.Size(163, 21);
			this.comboProcDiscountType.TabIndex = 201;
			// 
			// label19
			// 
			this.label19.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label19.Location = new System.Drawing.Point(55, 105);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(221, 15);
			this.label19.TabIndex = 200;
			this.label19.Text = "Procedure discount adj type";
			this.label19.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// tabChart
			// 
			this.tabChart.BackColor = System.Drawing.SystemColors.Window;
			this.tabChart.Controls.Add(this.checkProvColorChart);
			this.tabChart.Controls.Add(this.label11);
			this.tabChart.Controls.Add(this.textMedDefaultStopDays);
			this.tabChart.Controls.Add(this.butDiagnosisCode);
			this.tabChart.Controls.Add(this.checkDxIcdVersion);
			this.tabChart.Controls.Add(this.checkChartNonPatientWarn);
			this.tabChart.Controls.Add(this.checkProcLockingIsAllowed);
			this.tabChart.Controls.Add(this.textICD9DefaultForNewProcs);
			this.tabChart.Controls.Add(this.checkMedicalFeeUsedForNewProcs);
			this.tabChart.Controls.Add(this.checkProcGroupNoteDoesAggregate);
			this.tabChart.Controls.Add(this.butAllergiesIndicateNone);
			this.tabChart.Controls.Add(this.textAllergiesIndicateNone);
			this.tabChart.Controls.Add(this.labelIcdCodeDefault);
			this.tabChart.Controls.Add(this.label14);
			this.tabChart.Controls.Add(this.butMedicationsIndicateNone);
			this.tabChart.Controls.Add(this.textMedicationsIndicateNone);
			this.tabChart.Controls.Add(this.label9);
			this.tabChart.Controls.Add(this.butProblemsIndicateNone);
			this.tabChart.Controls.Add(this.textProblemsIndicateNone);
			this.tabChart.Controls.Add(this.label8);
			this.tabChart.Controls.Add(this.checkAutoClearEntryStatus);
			this.tabChart.Controls.Add(this.comboToothNomenclature);
			this.tabChart.Controls.Add(this.labelToothNomenclature);
			this.tabChart.Controls.Add(this.checkAllowSettingProcsComplete);
			this.tabChart.Location = new System.Drawing.Point(4, 22);
			this.tabChart.Name = "tabChart";
			this.tabChart.Size = new System.Drawing.Size(466, 479);
			this.tabChart.TabIndex = 4;
			this.tabChart.Text = "Chart";
			// 
			// checkProvColorChart
			// 
			this.checkProvColorChart.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkProvColorChart.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkProvColorChart.Location = new System.Drawing.Point(240, 287);
			this.checkProvColorChart.Name = "checkProvColorChart";
			this.checkProvColorChart.Size = new System.Drawing.Size(201, 15);
			this.checkProvColorChart.TabIndex = 214;
			this.checkProvColorChart.Text = "Use Provider Color in Chart";
			this.checkProvColorChart.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkProvColorChart.UseVisualStyleBackColor = true;
			// 
			// label11
			// 
			this.label11.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label11.Location = new System.Drawing.Point(3, 265);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(394, 16);
			this.label11.TabIndex = 213;
			this.label11.Text = "Medication order default days until stop date (0 for no automatic stop date)";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textMedDefaultStopDays
			// 
			this.textMedDefaultStopDays.Location = new System.Drawing.Point(402, 261);
			this.textMedDefaultStopDays.Name = "textMedDefaultStopDays";
			this.textMedDefaultStopDays.Size = new System.Drawing.Size(39, 20);
			this.textMedDefaultStopDays.TabIndex = 212;
			// 
			// butDiagnosisCode
			// 
			this.butDiagnosisCode.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDiagnosisCode.Autosize = true;
			this.butDiagnosisCode.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDiagnosisCode.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDiagnosisCode.CornerRadius = 4F;
			this.butDiagnosisCode.Location = new System.Drawing.Point(419, 198);
			this.butDiagnosisCode.Name = "butDiagnosisCode";
			this.butDiagnosisCode.Size = new System.Drawing.Size(22, 22);
			this.butDiagnosisCode.TabIndex = 213;
			this.butDiagnosisCode.Text = "...";
			this.butDiagnosisCode.Click += new System.EventHandler(this.butDiagnosisCode_Click);
			// 
			// checkDxIcdVersion
			// 
			this.checkDxIcdVersion.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkDxIcdVersion.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkDxIcdVersion.Location = new System.Drawing.Point(60, 179);
			this.checkDxIcdVersion.Name = "checkDxIcdVersion";
			this.checkDxIcdVersion.Size = new System.Drawing.Size(381, 15);
			this.checkDxIcdVersion.TabIndex = 212;
			this.checkDxIcdVersion.Text = "Use ICD-10 Diagnosis Codes (uncheck for ICD-9)";
			this.checkDxIcdVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkDxIcdVersion.UseVisualStyleBackColor = true;
			this.checkDxIcdVersion.Click += new System.EventHandler(this.checkDxIcdVersion_Click);
			// 
			// checkChartNonPatientWarn
			// 
			this.checkChartNonPatientWarn.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkChartNonPatientWarn.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkChartNonPatientWarn.Location = new System.Drawing.Point(310, 241);
			this.checkChartNonPatientWarn.Name = "checkChartNonPatientWarn";
			this.checkChartNonPatientWarn.Size = new System.Drawing.Size(131, 15);
			this.checkChartNonPatientWarn.TabIndex = 211;
			this.checkChartNonPatientWarn.Text = "Non Patient Warning";
			this.checkChartNonPatientWarn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkChartNonPatientWarn.UseVisualStyleBackColor = true;
			this.checkChartNonPatientWarn.Click += new System.EventHandler(this.checkChartNonPatientWarn_Click);
			// 
			// checkProcLockingIsAllowed
			// 
			this.checkProcLockingIsAllowed.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkProcLockingIsAllowed.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkProcLockingIsAllowed.Location = new System.Drawing.Point(60, 224);
			this.checkProcLockingIsAllowed.Name = "checkProcLockingIsAllowed";
			this.checkProcLockingIsAllowed.Size = new System.Drawing.Size(381, 15);
			this.checkProcLockingIsAllowed.TabIndex = 210;
			this.checkProcLockingIsAllowed.Text = "Procedure locking is allowed";
			this.checkProcLockingIsAllowed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkProcLockingIsAllowed.UseVisualStyleBackColor = true;
			this.checkProcLockingIsAllowed.Click += new System.EventHandler(this.checkProcLockingIsAllowed_Click);
			// 
			// textICD9DefaultForNewProcs
			// 
			this.textICD9DefaultForNewProcs.Location = new System.Drawing.Point(332, 199);
			this.textICD9DefaultForNewProcs.Name = "textICD9DefaultForNewProcs";
			this.textICD9DefaultForNewProcs.Size = new System.Drawing.Size(83, 20);
			this.textICD9DefaultForNewProcs.TabIndex = 209;
			// 
			// checkMedicalFeeUsedForNewProcs
			// 
			this.checkMedicalFeeUsedForNewProcs.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkMedicalFeeUsedForNewProcs.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkMedicalFeeUsedForNewProcs.Location = new System.Drawing.Point(60, 162);
			this.checkMedicalFeeUsedForNewProcs.Name = "checkMedicalFeeUsedForNewProcs";
			this.checkMedicalFeeUsedForNewProcs.Size = new System.Drawing.Size(381, 15);
			this.checkMedicalFeeUsedForNewProcs.TabIndex = 208;
			this.checkMedicalFeeUsedForNewProcs.Text = "Use medical fee for new procedures";
			this.checkMedicalFeeUsedForNewProcs.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkMedicalFeeUsedForNewProcs.UseVisualStyleBackColor = true;
			// 
			// checkProcGroupNoteDoesAggregate
			// 
			this.checkProcGroupNoteDoesAggregate.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkProcGroupNoteDoesAggregate.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkProcGroupNoteDoesAggregate.Location = new System.Drawing.Point(60, 145);
			this.checkProcGroupNoteDoesAggregate.Name = "checkProcGroupNoteDoesAggregate";
			this.checkProcGroupNoteDoesAggregate.Size = new System.Drawing.Size(381, 15);
			this.checkProcGroupNoteDoesAggregate.TabIndex = 206;
			this.checkProcGroupNoteDoesAggregate.Text = "Procedure Group Note Does Aggregate";
			this.checkProcGroupNoteDoesAggregate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkProcGroupNoteDoesAggregate.UseVisualStyleBackColor = true;
			// 
			// butAllergiesIndicateNone
			// 
			this.butAllergiesIndicateNone.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAllergiesIndicateNone.Autosize = true;
			this.butAllergiesIndicateNone.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAllergiesIndicateNone.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAllergiesIndicateNone.CornerRadius = 4F;
			this.butAllergiesIndicateNone.Location = new System.Drawing.Point(419, 118);
			this.butAllergiesIndicateNone.Name = "butAllergiesIndicateNone";
			this.butAllergiesIndicateNone.Size = new System.Drawing.Size(22, 21);
			this.butAllergiesIndicateNone.TabIndex = 205;
			this.butAllergiesIndicateNone.Text = "...";
			this.butAllergiesIndicateNone.Click += new System.EventHandler(this.butAllergiesIndicateNone_Click);
			// 
			// textAllergiesIndicateNone
			// 
			this.textAllergiesIndicateNone.Location = new System.Drawing.Point(270, 119);
			this.textAllergiesIndicateNone.Name = "textAllergiesIndicateNone";
			this.textAllergiesIndicateNone.ReadOnly = true;
			this.textAllergiesIndicateNone.Size = new System.Drawing.Size(145, 20);
			this.textAllergiesIndicateNone.TabIndex = 204;
			// 
			// labelIcdCodeDefault
			// 
			this.labelIcdCodeDefault.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.labelIcdCodeDefault.Location = new System.Drawing.Point(80, 202);
			this.labelIcdCodeDefault.Name = "labelIcdCodeDefault";
			this.labelIcdCodeDefault.Size = new System.Drawing.Size(246, 16);
			this.labelIcdCodeDefault.TabIndex = 203;
			this.labelIcdCodeDefault.Text = "Default ICD-10 code for new procedures";
			this.labelIcdCodeDefault.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label14
			// 
			this.label14.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label14.Location = new System.Drawing.Point(19, 122);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(246, 16);
			this.label14.TabIndex = 203;
			this.label14.Text = "Indicator that patient has No Allergies";
			this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butMedicationsIndicateNone
			// 
			this.butMedicationsIndicateNone.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butMedicationsIndicateNone.Autosize = true;
			this.butMedicationsIndicateNone.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butMedicationsIndicateNone.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butMedicationsIndicateNone.CornerRadius = 4F;
			this.butMedicationsIndicateNone.Location = new System.Drawing.Point(419, 95);
			this.butMedicationsIndicateNone.Name = "butMedicationsIndicateNone";
			this.butMedicationsIndicateNone.Size = new System.Drawing.Size(22, 21);
			this.butMedicationsIndicateNone.TabIndex = 202;
			this.butMedicationsIndicateNone.Text = "...";
			this.butMedicationsIndicateNone.Click += new System.EventHandler(this.butMedicationsIndicateNone_Click);
			// 
			// textMedicationsIndicateNone
			// 
			this.textMedicationsIndicateNone.Location = new System.Drawing.Point(270, 96);
			this.textMedicationsIndicateNone.Name = "textMedicationsIndicateNone";
			this.textMedicationsIndicateNone.ReadOnly = true;
			this.textMedicationsIndicateNone.Size = new System.Drawing.Size(145, 20);
			this.textMedicationsIndicateNone.TabIndex = 201;
			// 
			// label9
			// 
			this.label9.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label9.Location = new System.Drawing.Point(19, 99);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(246, 16);
			this.label9.TabIndex = 200;
			this.label9.Text = "Indicator that patient has No Medications";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butProblemsIndicateNone
			// 
			this.butProblemsIndicateNone.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butProblemsIndicateNone.Autosize = true;
			this.butProblemsIndicateNone.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butProblemsIndicateNone.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butProblemsIndicateNone.CornerRadius = 4F;
			this.butProblemsIndicateNone.Location = new System.Drawing.Point(419, 72);
			this.butProblemsIndicateNone.Name = "butProblemsIndicateNone";
			this.butProblemsIndicateNone.Size = new System.Drawing.Size(22, 21);
			this.butProblemsIndicateNone.TabIndex = 199;
			this.butProblemsIndicateNone.Text = "...";
			this.butProblemsIndicateNone.Click += new System.EventHandler(this.butProblemsIndicateNone_Click);
			// 
			// textProblemsIndicateNone
			// 
			this.textProblemsIndicateNone.Location = new System.Drawing.Point(270, 73);
			this.textProblemsIndicateNone.Name = "textProblemsIndicateNone";
			this.textProblemsIndicateNone.ReadOnly = true;
			this.textProblemsIndicateNone.Size = new System.Drawing.Size(145, 20);
			this.textProblemsIndicateNone.TabIndex = 198;
			// 
			// label8
			// 
			this.label8.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label8.Location = new System.Drawing.Point(19, 76);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(246, 16);
			this.label8.TabIndex = 197;
			this.label8.Text = "Indicator that patient has No Problems";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// tabImages
			// 
			this.tabImages.BackColor = System.Drawing.SystemColors.Window;
			this.tabImages.Controls.Add(this.checkImagesModuleTreeIsCollapsed);
			this.tabImages.Location = new System.Drawing.Point(4, 22);
			this.tabImages.Name = "tabImages";
			this.tabImages.Size = new System.Drawing.Size(466, 479);
			this.tabImages.TabIndex = 5;
			this.tabImages.Text = "Images";
			// 
			// tabManage
			// 
			this.tabManage.BackColor = System.Drawing.SystemColors.Window;
			this.tabManage.Controls.Add(this.checkClaimsSendWindowValidateOnLoad);
			this.tabManage.Controls.Add(this.checkTimeCardADP);
			this.tabManage.Controls.Add(this.groupBox1);
			this.tabManage.Controls.Add(this.comboTimeCardOvertimeFirstDayOfWeek);
			this.tabManage.Controls.Add(this.label16);
			this.tabManage.Controls.Add(this.checkRxSendNewToQueue);
			this.tabManage.Location = new System.Drawing.Point(4, 22);
			this.tabManage.Name = "tabManage";
			this.tabManage.Size = new System.Drawing.Size(466, 479);
			this.tabManage.TabIndex = 6;
			this.tabManage.Text = "Manage";
			// 
			// checkClaimsSendWindowValidateOnLoad
			// 
			this.checkClaimsSendWindowValidateOnLoad.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkClaimsSendWindowValidateOnLoad.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkClaimsSendWindowValidateOnLoad.Location = new System.Drawing.Point(20, 74);
			this.checkClaimsSendWindowValidateOnLoad.Name = "checkClaimsSendWindowValidateOnLoad";
			this.checkClaimsSendWindowValidateOnLoad.Size = new System.Drawing.Size(421, 17);
			this.checkClaimsSendWindowValidateOnLoad.TabIndex = 199;
			this.checkClaimsSendWindowValidateOnLoad.Text = "Claims Send window validate on load (can cause slowness)";
			this.checkClaimsSendWindowValidateOnLoad.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkTimeCardADP
			// 
			this.checkTimeCardADP.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkTimeCardADP.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkTimeCardADP.Location = new System.Drawing.Point(82, 57);
			this.checkTimeCardADP.Name = "checkTimeCardADP";
			this.checkTimeCardADP.Size = new System.Drawing.Size(359, 17);
			this.checkTimeCardADP.TabIndex = 198;
			this.checkTimeCardADP.Text = "ADP export includes employee name";
			this.checkTimeCardADP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label24);
			this.groupBox1.Controls.Add(this.textBillingElectBatchMax);
			this.groupBox1.Controls.Add(this.checkStatementShowAdjNotes);
			this.groupBox1.Controls.Add(this.checkIntermingleDefault);
			this.groupBox1.Controls.Add(this.checkStatementShowReturnAddress);
			this.groupBox1.Controls.Add(this.checkStatementShowProcBreakdown);
			this.groupBox1.Controls.Add(this.checkShowCC);
			this.groupBox1.Controls.Add(this.checkStatementShowNotes);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.comboUseChartNum);
			this.groupBox1.Controls.Add(this.label10);
			this.groupBox1.Controls.Add(this.label18);
			this.groupBox1.Controls.Add(this.textStatementsCalcDueDate);
			this.groupBox1.Controls.Add(this.textPayPlansBillInAdvanceDays);
			this.groupBox1.Location = new System.Drawing.Point(38, 91);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(413, 238);
			this.groupBox1.TabIndex = 197;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Billing and Statements";
			// 
			// label24
			// 
			this.label24.Location = new System.Drawing.Point(25, 210);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(316, 20);
			this.label24.TabIndex = 217;
			this.label24.Text = "Max number of electronic statements per batch (0 for no limit)";
			this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBillingElectBatchMax
			// 
			this.textBillingElectBatchMax.Location = new System.Drawing.Point(342, 211);
			this.textBillingElectBatchMax.MaxVal = 255;
			this.textBillingElectBatchMax.MinVal = 0;
			this.textBillingElectBatchMax.Name = "textBillingElectBatchMax";
			this.textBillingElectBatchMax.Size = new System.Drawing.Size(60, 20);
			this.textBillingElectBatchMax.TabIndex = 216;
			this.textBillingElectBatchMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// checkStatementShowAdjNotes
			// 
			this.checkStatementShowAdjNotes.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkStatementShowAdjNotes.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkStatementShowAdjNotes.Location = new System.Drawing.Point(34, 62);
			this.checkStatementShowAdjNotes.Name = "checkStatementShowAdjNotes";
			this.checkStatementShowAdjNotes.Size = new System.Drawing.Size(368, 17);
			this.checkStatementShowAdjNotes.TabIndex = 215;
			this.checkStatementShowAdjNotes.Text = "Show notes for adjustments";
			this.checkStatementShowAdjNotes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkIntermingleDefault
			// 
			this.checkIntermingleDefault.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIntermingleDefault.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIntermingleDefault.Location = new System.Drawing.Point(25, 189);
			this.checkIntermingleDefault.Name = "checkIntermingleDefault";
			this.checkIntermingleDefault.Size = new System.Drawing.Size(377, 16);
			this.checkIntermingleDefault.TabIndex = 214;
			this.checkIntermingleDefault.Text = "Default to all types of statements printing in intermingled mode";
			this.checkIntermingleDefault.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkStatementShowReturnAddress
			// 
			this.checkStatementShowReturnAddress.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkStatementShowReturnAddress.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkStatementShowReturnAddress.Location = new System.Drawing.Point(125, 11);
			this.checkStatementShowReturnAddress.Name = "checkStatementShowReturnAddress";
			this.checkStatementShowReturnAddress.Size = new System.Drawing.Size(277, 17);
			this.checkStatementShowReturnAddress.TabIndex = 206;
			this.checkStatementShowReturnAddress.Text = "Show return address";
			this.checkStatementShowReturnAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkStatementShowProcBreakdown
			// 
			this.checkStatementShowProcBreakdown.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkStatementShowProcBreakdown.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkStatementShowProcBreakdown.Location = new System.Drawing.Point(34, 79);
			this.checkStatementShowProcBreakdown.Name = "checkStatementShowProcBreakdown";
			this.checkStatementShowProcBreakdown.Size = new System.Drawing.Size(368, 17);
			this.checkStatementShowProcBreakdown.TabIndex = 212;
			this.checkStatementShowProcBreakdown.Text = "Show procedure breakdown";
			this.checkStatementShowProcBreakdown.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkShowCC
			// 
			this.checkShowCC.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkShowCC.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowCC.Location = new System.Drawing.Point(34, 28);
			this.checkShowCC.Name = "checkShowCC";
			this.checkShowCC.Size = new System.Drawing.Size(368, 17);
			this.checkShowCC.TabIndex = 203;
			this.checkShowCC.Text = "Show credit card info";
			this.checkShowCC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkStatementShowNotes
			// 
			this.checkStatementShowNotes.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkStatementShowNotes.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkStatementShowNotes.Location = new System.Drawing.Point(34, 45);
			this.checkStatementShowNotes.Name = "checkStatementShowNotes";
			this.checkStatementShowNotes.Size = new System.Drawing.Size(368, 17);
			this.checkStatementShowNotes.TabIndex = 211;
			this.checkStatementShowNotes.Text = "Show notes for payments";
			this.checkStatementShowNotes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label2.Location = new System.Drawing.Point(22, 126);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(318, 27);
			this.label2.TabIndex = 204;
			this.label2.Text = "Days to calculate due date.  Usually 10 or 15.  Leave blank to show \"Due on Recei" +
    "pt\"";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// comboUseChartNum
			// 
			this.comboUseChartNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboUseChartNum.FormattingEnabled = true;
			this.comboUseChartNum.Location = new System.Drawing.Point(273, 99);
			this.comboUseChartNum.Name = "comboUseChartNum";
			this.comboUseChartNum.Size = new System.Drawing.Size(130, 21);
			this.comboUseChartNum.TabIndex = 207;
			// 
			// label10
			// 
			this.label10.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label10.Location = new System.Drawing.Point(76, 102);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(195, 15);
			this.label10.TabIndex = 208;
			this.label10.Text = "Account Numbers use";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label18
			// 
			this.label18.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label18.Location = new System.Drawing.Point(23, 158);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(318, 27);
			this.label18.TabIndex = 209;
			this.label18.Text = "Days in advance to bill payment plan amounts due.\r\nUsually 10 or 15.";
			this.label18.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textStatementsCalcDueDate
			// 
			this.textStatementsCalcDueDate.Location = new System.Drawing.Point(343, 130);
			this.textStatementsCalcDueDate.MaxVal = 255;
			this.textStatementsCalcDueDate.MinVal = 0;
			this.textStatementsCalcDueDate.Name = "textStatementsCalcDueDate";
			this.textStatementsCalcDueDate.Size = new System.Drawing.Size(60, 20);
			this.textStatementsCalcDueDate.TabIndex = 205;
			this.textStatementsCalcDueDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textPayPlansBillInAdvanceDays
			// 
			this.textPayPlansBillInAdvanceDays.Location = new System.Drawing.Point(343, 162);
			this.textPayPlansBillInAdvanceDays.MaxVal = 255;
			this.textPayPlansBillInAdvanceDays.MinVal = 0;
			this.textPayPlansBillInAdvanceDays.Name = "textPayPlansBillInAdvanceDays";
			this.textPayPlansBillInAdvanceDays.Size = new System.Drawing.Size(60, 20);
			this.textPayPlansBillInAdvanceDays.TabIndex = 210;
			this.textPayPlansBillInAdvanceDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// comboTimeCardOvertimeFirstDayOfWeek
			// 
			this.comboTimeCardOvertimeFirstDayOfWeek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboTimeCardOvertimeFirstDayOfWeek.FormattingEnabled = true;
			this.comboTimeCardOvertimeFirstDayOfWeek.Location = new System.Drawing.Point(270, 30);
			this.comboTimeCardOvertimeFirstDayOfWeek.Name = "comboTimeCardOvertimeFirstDayOfWeek";
			this.comboTimeCardOvertimeFirstDayOfWeek.Size = new System.Drawing.Size(170, 21);
			this.comboTimeCardOvertimeFirstDayOfWeek.TabIndex = 195;
			// 
			// label16
			// 
			this.label16.BackColor = System.Drawing.SystemColors.Window;
			this.label16.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label16.Location = new System.Drawing.Point(17, 34);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(248, 13);
			this.label16.TabIndex = 196;
			this.label16.Text = "Time Card first day of week for overtime";
			this.label16.TextAlign = System.Drawing.ContentAlignment.TopRight;
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
			this.butCancel.Location = new System.Drawing.Point(441, 533);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 24);
			this.butCancel.TabIndex = 8;
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
			this.butOK.Location = new System.Drawing.Point(336, 533);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 24);
			this.butOK.TabIndex = 7;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// checkApptModuleDefaultToWeek
			// 
			this.checkApptModuleDefaultToWeek.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkApptModuleDefaultToWeek.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkApptModuleDefaultToWeek.Location = new System.Drawing.Point(34, 382);
			this.checkApptModuleDefaultToWeek.Name = "checkApptModuleDefaultToWeek";
			this.checkApptModuleDefaultToWeek.Size = new System.Drawing.Size(406, 17);
			this.checkApptModuleDefaultToWeek.TabIndex = 221;
			this.checkApptModuleDefaultToWeek.Text = "Appointment Module Defaults to Week View";
			this.checkApptModuleDefaultToWeek.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// FormModuleSetup
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(543, 570);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormModuleSetup";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Module Preferences";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormModuleSetup_FormClosing);
			this.Load += new System.EventHandler(this.FormModuleSetup_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabAppts.ResumeLayout(false);
			this.tabAppts.PerformLayout();
			this.tabFamily.ResumeLayout(false);
			this.tabAccount.ResumeLayout(false);
			this.tabAccount.PerformLayout();
			this.tabTreatPlan.ResumeLayout(false);
			this.tabTreatPlan.PerformLayout();
			this.tabChart.ResumeLayout(false);
			this.tabChart.PerformLayout();
			this.tabImages.ResumeLayout(false);
			this.tabManage.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion

		private void FormModuleSetup_Load(object sender, System.EventArgs e) {
			try {//try/catch used to prevent setup form from partially loading and filling controls.  Causes UEs, Example: TimeCardOvertimeFirstDayOfWeek set to -1 because UI control not filled properly.
				FillControlsHelper();
			}
			catch(Exception ex) {
				MessageBox.Show(Lan.g(this,"An error has occured while attempting to load preferences.  Run database maintenance and try again."));
				DialogResult=DialogResult.Abort;
				return;
			}
			//Now that all the tabs are filled, use _selectedTab to open a specific tab that the user is trying to view.
			tabControl1.SelectedTab=tabControl1.TabPages[_selectedTab];//Garunteed to be a valid tab.  Validated in constructor.
			Plugins.HookAddCode(this,"FormModuleSetup.FormModuleSetup_Load_end");
		}

		private void FillControlsHelper() {
			_changed=false;
			#region Appointment Module
			//Appointment module---------------------------------------------------------------
			ProcedureCode procCodeBrokenApt=ProcedureCodes.GetProcCode("D9986");
			if(ProcedureCodes.IsValidCode("D9986")) {//Has the ADA procedure code D9986 for broken appointments
				checkBrokenApptNote.Enabled=false;//Disable the old preference that makes it seem like an adjustment will be created.
			}
			else {
				//Could be a foreign user.  Disable the new preference that makes it seem like a procedure will be created.
				checkBrokenApptCommLogWithProcedure.Enabled=false;
				checkBrokenApptAdjustmentWithProcedure.Enabled=false;
			}
			checkSolidBlockouts.Checked=PrefC.GetBool(PrefName.SolidBlockouts);
			checkBrokenApptNote.Checked=PrefC.GetBool(PrefName.BrokenApptCommLogNotAdjustment);
			checkBrokenApptAdjustmentWithProcedure.Checked=PrefC.GetBool(PrefName.BrokenApptAdjustmentWithProcedure);
			checkBrokenApptCommLogWithProcedure.Checked=PrefC.GetBool(PrefName.BrokenApptCommLogWithProcedure);
			checkApptBubbleDelay.Checked = PrefC.GetBool(PrefName.ApptBubbleDelay);
			checkAppointmentBubblesDisabled.Checked=PrefC.GetBool(PrefName.AppointmentBubblesDisabled);
			listPosAdjTypes=DefC.GetPositiveAdjTypes();
			listNegAdjTypes=DefC.GetNegativeAdjTypes();
			long financeChargeAdjDefNum=PrefC.GetLong(PrefName.FinanceChargeAdjustmentType);
			long billingChargeAdjDefNum=PrefC.GetLong(PrefName.BillingChargeAdjustmentType);
			long brokenApptAdjDefNum=PrefC.GetLong(PrefName.BrokenAppointmentAdjustmentType);
			long treatPlanDiscountAdjDefNum=PrefC.GetLong(PrefName.TreatPlanDiscountAdjustmentType);
			for(int i=0;i<listPosAdjTypes.Count;i++) {
				comboFinanceChargeAdjType.Items.Add(listPosAdjTypes[i].ItemName);
				if(financeChargeAdjDefNum==listPosAdjTypes[i].DefNum) {
					comboFinanceChargeAdjType.SelectedIndex=i;
				}
				comboBillingChargeAdjType.Items.Add(listPosAdjTypes[i].ItemName);
				if(billingChargeAdjDefNum==listPosAdjTypes[i].DefNum) {
					comboBillingChargeAdjType.SelectedIndex=i;
				}
				comboBrokenApptAdjType.Items.Add(listPosAdjTypes[i].ItemName);
				if(brokenApptAdjDefNum==listPosAdjTypes[i].DefNum) {
					comboBrokenApptAdjType.SelectedIndex=i;
				}
			}
			for(int i=0;i<listNegAdjTypes.Count;i++) {
				comboProcDiscountType.Items.Add(listNegAdjTypes[i].ItemName);
				if(treatPlanDiscountAdjDefNum==listNegAdjTypes[i].DefNum) {
					comboProcDiscountType.SelectedIndex=i;
				}
			}
			//Check to see if any adjustment type preferences are hidden.
			if(financeChargeAdjDefNum>0 && comboFinanceChargeAdjType.SelectedIndex==-1) {
				comboFinanceChargeAdjType.Text=DefC.GetDef(DefCat.AdjTypes,financeChargeAdjDefNum).ItemName+" ("+Lan.g(this,"hidden")+")";
			}
			if(billingChargeAdjDefNum>0 && comboBillingChargeAdjType.SelectedIndex==-1) {
				comboBillingChargeAdjType.Text=DefC.GetDef(DefCat.AdjTypes,billingChargeAdjDefNum).ItemName+" ("+Lan.g(this,"hidden")+")";
			}
			if(brokenApptAdjDefNum>0 && comboBrokenApptAdjType.SelectedIndex==-1) {
				comboBrokenApptAdjType.Text=DefC.GetDef(DefCat.AdjTypes,brokenApptAdjDefNum).ItemName+" ("+Lan.g(this,"hidden")+")";
			}
			if(treatPlanDiscountAdjDefNum>0 && comboProcDiscountType.SelectedIndex==-1) {
				comboProcDiscountType.Text=DefC.GetDef(DefCat.AdjTypes,treatPlanDiscountAdjDefNum).ItemName+" ("+Lan.g(this,"hidden")+")";
			}
			textDiscountPercentage.Text=PrefC.GetDouble(PrefName.TreatPlanDiscountPercent).ToString();
			checkApptExclamation.Checked=PrefC.GetBool(PrefName.ApptExclamationShowForUnsentIns);
			comboTimeArrived.Items.Add(Lan.g(this,"none"));
			comboTimeArrived.SelectedIndex=0;
			for(int i=0;i<DefC.Short[(int)DefCat.ApptConfirmed].Length;i++) {
				comboTimeArrived.Items.Add(DefC.Short[(int)DefCat.ApptConfirmed][i].ItemName);
				if(DefC.Short[(int)DefCat.ApptConfirmed][i].DefNum==PrefC.GetLong(PrefName.AppointmentTimeArrivedTrigger)) {
					comboTimeArrived.SelectedIndex=i+1;
				}
			}
			comboTimeSeated.Items.Add(Lan.g(this,"none"));
			comboTimeSeated.SelectedIndex=0;
			for(int i=0;i<DefC.Short[(int)DefCat.ApptConfirmed].Length;i++) {
				comboTimeSeated.Items.Add(DefC.Short[(int)DefCat.ApptConfirmed][i].ItemName);
				if(DefC.Short[(int)DefCat.ApptConfirmed][i].DefNum==PrefC.GetLong(PrefName.AppointmentTimeSeatedTrigger)) {
					comboTimeSeated.SelectedIndex=i+1;
				}
			}
			comboTimeDismissed.Items.Add(Lan.g(this,"none"));
			comboTimeDismissed.SelectedIndex=0;
			for(int i=0;i<DefC.Short[(int)DefCat.ApptConfirmed].Length;i++) {
				comboTimeDismissed.Items.Add(DefC.Short[(int)DefCat.ApptConfirmed][i].ItemName);
				if(DefC.Short[(int)DefCat.ApptConfirmed][i].DefNum==PrefC.GetLong(PrefName.AppointmentTimeDismissedTrigger)) {
					comboTimeDismissed.SelectedIndex=i+1;
				}
			}
			checkApptRefreshEveryMinute.Checked=PrefC.GetBool(PrefName.ApptModuleRefreshesEveryMinute);
			for(int i=0;i<Enum.GetNames(typeof(SearchBehaviorCriteria)).Length;i++) {
				comboSearchBehavior.Items.Add(Enum.GetNames(typeof(SearchBehaviorCriteria))[i]);
			}
			comboSearchBehavior.SelectedIndex=PrefC.GetInt(PrefName.AppointmentSearchBehavior);
			checkAppointmentTimeIsLocked.Checked=PrefC.GetBool(PrefName.AppointmentTimeIsLocked);
			textApptBubNoteLength.Text=PrefC.GetInt(PrefName.AppointmentBubblesNoteLength).ToString();
			checkWaitingRoomFilterByView.Checked=PrefC.GetBool(PrefName.WaitingRoomFilterByView);
			textWaitRoomWarn.Text=PrefC.GetInt(PrefName.WaitingRoomAlertTime).ToString();
			butColor.BackColor=PrefC.GetColor(PrefName.WaitingRoomAlertColor);
			checkApptModuleDefaultToWeek.Checked=PrefC.GetBool(PrefName.ApptModuleDefaultToWeek);
			#endregion
			#region Family Module
			//Family module-----------------------------------------------------------------------
			checkInsurancePlansShared.Checked=PrefC.GetBool(PrefName.InsurancePlansShared);
			checkPPOpercentage.Checked=PrefC.GetBool(PrefName.InsDefaultPPOpercent);
			checkAllowedFeeSchedsAutomate.Checked=PrefC.GetBool(PrefName.AllowedFeeSchedsAutomate);
			checkCoPayFeeScheduleBlankLikeZero.Checked=PrefC.GetBool(PrefName.CoPay_FeeSchedule_BlankLikeZero);
			checkInsDefaultShowUCRonClaims.Checked=PrefC.GetBool(PrefName.InsDefaultShowUCRonClaims);
			checkInsDefaultAssignmentOfBenefits.Checked=PrefC.GetBool(PrefName.InsDefaultAssignBen);
			checkInsPPOsecWriteoffs.Checked=PrefC.GetBool(PrefName.InsPPOsecWriteoffs);
			for(int i=0;i<Enum.GetNames(typeof(EnumCobRule)).Length;i++) {
				comboCobRule.Items.Add(Lan.g("enumEnumCobRule",Enum.GetNames(typeof(EnumCobRule))[i]));
			}
			comboCobRule.SelectedIndex=PrefC.GetInt(PrefName.InsDefaultCobRule);
			checkTextMsgOkStatusTreatAsNo.Checked=PrefC.GetBool(PrefName.TextMsgOkStatusTreatAsNo);
			checkFamPhiAccess.Checked=PrefC.GetBool(PrefName.FamPhiAccess);
			checkGoogleAddress.Checked=PrefC.GetBool(PrefName.ShowFeatureGoogleMaps);
			checkSelectProv.Checked=PrefC.GetBool(PrefName.PriProvDefaultToSelectProv);
			#endregion
			#region Account Module
			//Account module-----------------------------------------------------------------------
			checkBalancesDontSubtractIns.Checked=PrefC.GetBool(PrefName.BalancesDontSubtractIns);
			checkAgingMonthly.Checked=PrefC.GetBool(PrefName.AgingCalculatedMonthlyInsteadOfDaily);
			checkEclaimsSeparateTreatProv.Checked=PrefC.GetBool(PrefName.EclaimsSeparateTreatProv);
			checkStoreCCnumbers.Checked=PrefC.GetBool(PrefName.StoreCCnumbers);
			checkStoreCCTokens.Checked=PrefC.GetBool(PrefName.StoreCCtokens);
			checkProviderIncomeShows.Checked=PrefC.GetBool(PrefName.ProviderIncomeTransferShows);
			textClaimAttachPath.Text=PrefC.GetString(PrefName.ClaimAttachExportPath);
			checkShowFamilyCommByDefault.Checked=PrefC.GetBool(PrefName.ShowAccountFamilyCommEntries);
			checkClaimFormTreatDentSaysSigOnFile.Checked=PrefC.GetBool(PrefName.ClaimFormTreatDentSaysSigOnFile);
			checkClaimsValidateACN.Checked=PrefC.GetBool(PrefName.ClaimsValidateACN);
			checkClaimMedTypeIsInstWhenInsPlanIsMedical.Checked=PrefC.GetBool(PrefName.ClaimMedTypeIsInstWhenInsPlanIsMedical);
			checkAccountShowPaymentNums.Checked=PrefC.GetBool(PrefName.AccountShowPaymentNums);
			checkStatementsUseSheets.Checked=PrefC.GetBool(PrefName.StatementsUseSheets);
			checkPromptAutoSplit.Checked=PrefC.GetBool(PrefName.PaymentsPromptForAutoSplit);
			textInsWriteoffDescript.Text=PrefC.GetString(PrefName.InsWriteoffDescript);
			#endregion
			#region TP Module
			//TP module-----------------------------------------------------------------------
			textTreatNote.Text=PrefC.GetString(PrefName.TreatmentPlanNote);
			checkTreatPlanShowGraphics.Checked=PrefC.GetBool(PrefName.TreatPlanShowGraphics);
			checkTreatPlanShowCompleted.Checked=PrefC.GetBool(PrefName.TreatPlanShowCompleted);
			if(Clinics.IsMedicalPracticeOrClinic(FormOpenDental.ClinicNum)) {
				checkTreatPlanShowGraphics.Visible=false;
				checkTreatPlanShowCompleted.Visible=false;
			}
			else {
				checkTreatPlanShowGraphics.Checked=PrefC.GetBool(PrefName.TreatPlanShowGraphics);
				checkTreatPlanShowCompleted.Checked=PrefC.GetBool(PrefName.TreatPlanShowCompleted);
			}
			checkTreatPlanItemized.Checked=PrefC.GetBool(PrefName.TreatPlanItemized);
			checkTPSaveSigned.Checked=PrefC.GetBool(PrefName.TreatPlanSaveSignedToPdf);
			checkTreatPlanUseSheets.Checked=PrefC.GetBool(PrefName.TreatPlanUseSheets);
			#endregion
			#region Chart Module
			//Chart module-----------------------------------------------------------------------
			comboToothNomenclature.Items.Add(Lan.g(this,"Universal (Common in the US, 1-32)"));
			comboToothNomenclature.Items.Add(Lan.g(this,"FDI Notation (International, 11-48)"));
			comboToothNomenclature.Items.Add(Lan.g(this,"Haderup (Danish)"));
			comboToothNomenclature.Items.Add(Lan.g(this,"Palmer (Ortho)"));
			comboToothNomenclature.SelectedIndex = PrefC.GetInt(PrefName.UseInternationalToothNumbers);
			if(Clinics.IsMedicalPracticeOrClinic(FormOpenDental.ClinicNum)) {
				labelToothNomenclature.Visible=false;
				comboToothNomenclature.Visible=false;
			}
			checkAutoClearEntryStatus.Checked=PrefC.GetBool(PrefName.AutoResetTPEntryStatus);
			checkAllowSettingProcsComplete.Checked=PrefC.GetBool(PrefName.AllowSettingProcsComplete);
			//checkChartQuickAddHideAmalgam.Checked=PrefC.GetBool(PrefName.ChartQuickAddHideAmalgam); //Deprecated.
			//checkToothChartMoveMenuToRight.Checked=PrefC.GetBool(PrefName.ToothChartMoveMenuToRight);
			textProblemsIndicateNone.Text		=DiseaseDefs.GetName(PrefC.GetLong(PrefName.ProblemsIndicateNone)); //DB maint to fix corruption
			textMedicationsIndicateNone.Text=Medications.GetDescription(PrefC.GetLong(PrefName.MedicationsIndicateNone)); //DB maint to fix corruption
			textAllergiesIndicateNone.Text	=AllergyDefs.GetDescription(PrefC.GetLong(PrefName.AllergiesIndicateNone)); //DB maint to fix corruption
			checkProcGroupNoteDoesAggregate.Checked=PrefC.GetBool(PrefName.ProcGroupNoteDoesAggregate);
			checkChartNonPatientWarn.Checked=PrefC.GetBool(PrefName.ChartNonPatientWarn);
			//checkChartAddProcNoRefreshGrid.Checked=PrefC.GetBool(PrefName.ChartAddProcNoRefreshGrid);//Not implemented.  May revisit some day.
			checkMedicalFeeUsedForNewProcs.Checked=PrefC.GetBool(PrefName.MedicalFeeUsedForNewProcs);
			checkProvColorChart.Checked=PrefC.GetBool(PrefName.UseProviderColorsInChart);
			if(PrefC.GetByte(PrefName.DxIcdVersion)==9) {
				checkDxIcdVersion.Checked=false;
			}
			else {//ICD-10
				checkDxIcdVersion.Checked=true;
			}
			SetIcdLabels();
			textICD9DefaultForNewProcs.Text=PrefC.GetString(PrefName.ICD9DefaultForNewProcs);
			checkProcLockingIsAllowed.Checked=PrefC.GetBool(PrefName.ProcLockingIsAllowed);
			textMedDefaultStopDays.Text=PrefC.GetString(PrefName.MedDefaultStopDays);
			#endregion
			#region Image Module
			//Image module-----------------------------------------------------------------------
			checkImagesModuleTreeIsCollapsed.Checked=PrefC.GetBool(PrefName.ImagesModuleTreeIsCollapsed);
			#endregion
			#region Manage Module
			//Manage module----------------------------------------------------------------------
			checkRxSendNewToQueue.Checked=PrefC.GetBool(PrefName.RxSendNewToQueue);
			for(int i=0;i<7;i++) {
				comboTimeCardOvertimeFirstDayOfWeek.Items.Add(Lan.g("enumDayOfWeek",Enum.GetNames(typeof(DayOfWeek))[i]));
			}
			comboTimeCardOvertimeFirstDayOfWeek.SelectedIndex=PrefC.GetInt(PrefName.TimeCardOvertimeFirstDayOfWeek);
			checkTimeCardADP.Checked=PrefC.GetBool(PrefName.TimeCardADPExportIncludesName);
			checkClaimsSendWindowValidateOnLoad.Checked=PrefC.GetBool(PrefName.ClaimsSendWindowValidatesOnLoad);
			//Statements
			checkStatementShowReturnAddress.Checked=PrefC.GetBool(PrefName.StatementShowReturnAddress);
			checkShowCC.Checked=PrefC.GetBool(PrefName.StatementShowCreditCard);
			checkStatementShowNotes.Checked=PrefC.GetBool(PrefName.StatementShowNotes);
			checkStatementShowAdjNotes.Checked=PrefC.GetBool(PrefName.StatementShowAdjNotes);
			checkStatementShowProcBreakdown.Checked=PrefC.GetBool(PrefName.StatementShowProcBreakdown);
			comboUseChartNum.Items.Add(Lan.g(this,"PatNum"));
			comboUseChartNum.Items.Add(Lan.g(this,"ChartNumber"));
			if(PrefC.GetBool(PrefName.StatementAccountsUseChartNumber)) {
				comboUseChartNum.SelectedIndex=1;
			}
			else {
				comboUseChartNum.SelectedIndex=0;
			}
			if(PrefC.GetLong(PrefName.StatementsCalcDueDate)!=-1) {
				textStatementsCalcDueDate.Text=PrefC.GetLong(PrefName.StatementsCalcDueDate).ToString();
			}
			textPayPlansBillInAdvanceDays.Text=PrefC.GetLong(PrefName.PayPlansBillInAdvanceDays).ToString();
			textBillingElectBatchMax.Text=PrefC.GetInt(PrefName.BillingElectBatchMax).ToString();
			checkIntermingleDefault.Checked=PrefC.GetBool(PrefName.IntermingleFamilyDefault);
			#endregion
		}

		private void checkTreatPlanUseSheets_Click(object sender,EventArgs e) {
			if(checkTreatPlanUseSheets.Checked) {
				checkTPSaveSigned.Checked=true;
			}
		}

		private void checkAllowedFeeSchedsAutomate_Click(object sender,EventArgs e) {
			if(!checkAllowedFeeSchedsAutomate.Checked){
				return;
			}
			if(!MsgBox.Show(this,true,"Allowed fee schedules will now be set up for all insurance plans that do not already have one.\r\nThe name of each fee schedule will exactly match the name of the carrier.\r\nOnce created, allowed fee schedules can be easily managed from the fee schedules window.\r\nContinue?")){
				checkAllowedFeeSchedsAutomate.Checked=false;
				return;
			}
			Cursor=Cursors.WaitCursor;
			long schedsAdded=InsPlans.GenerateAllowedFeeSchedules();
			Cursor=Cursors.Default;
			MessageBox.Show(Lan.g(this,"Done.  Allowed fee schedules added: ")+schedsAdded.ToString());
			DataValid.SetInvalid(InvalidType.FeeScheds);
		}

		private void checkInsDefaultShowUCRonClaims_Click(object sender,EventArgs e) {
			if(!checkInsDefaultShowUCRonClaims.Checked) {
				return;
			}
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Would you like to immediately change all category percentage plans to show office UCR fees on claims?")) {
				return;
			}
			long plansAffected=InsPlans.SetAllPlansToShowUCR();
			MessageBox.Show(Lan.g(this,"Plans affected: ")+plansAffected.ToString());
		}

		private void checkInsDefaultAssignmentOfBenefits_Click(object sender,EventArgs e) {
			if(checkInsDefaultAssignmentOfBenefits.Checked) {
				return;
			}
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Would you like to immediately change all plans to not use assignment of benefits?")) {
				return;
			}
			long subsAffected=InsSubs.SetAllSubsAssignBen();
			MessageBox.Show(Lan.g(this,"Plans affected: ")+subsAffected.ToString());
		}

		private void butProblemsIndicateNone_Click(object sender,EventArgs e) {
			FormDiseaseDefs formD=new FormDiseaseDefs();
			formD.IsSelectionMode=true;
			formD.ShowDialog();
			if(formD.DialogResult!=DialogResult.OK) {
				return;
			}
			if(Prefs.UpdateLong(PrefName.ProblemsIndicateNone,formD.SelectedDiseaseDefNum)) {
				_changed=true;
			}
			textProblemsIndicateNone.Text=DiseaseDefs.GetName(formD.SelectedDiseaseDefNum);
		}

		private void butMedicationsIndicateNone_Click(object sender,EventArgs e) {
			FormMedications formM=new FormMedications();
			formM.IsSelectionMode=true;
			formM.ShowDialog();
			if(formM.DialogResult!=DialogResult.OK) {
				return;
			}
			if(Prefs.UpdateLong(PrefName.MedicationsIndicateNone,formM.SelectedMedicationNum)) {
				_changed=true;
			}
			textMedicationsIndicateNone.Text=Medications.GetDescription(formM.SelectedMedicationNum);
		}

		private void butAllergiesIndicateNone_Click(object sender,EventArgs e) {
			FormAllergySetup formA=new FormAllergySetup();
			formA.IsSelectionMode=true;
			formA.ShowDialog();
			if(formA.DialogResult!=DialogResult.OK) {
				return;
			}
			if(Prefs.UpdateLong(PrefName.AllergiesIndicateNone,formA.SelectedAllergyDefNum)) {
				_changed=true;
			}
			textAllergiesIndicateNone.Text=AllergyDefs.GetOne(formA.SelectedAllergyDefNum).Description;
		}

		private void butColor_Click(object sender,EventArgs e) {
			colorDialog.Color=butColor.BackColor;//Pre-select current pref color
			if(colorDialog.ShowDialog()==DialogResult.OK) {
				butColor.BackColor=colorDialog.Color;
			}
		}

		private void checkChartNonPatientWarn_Click(object sender,EventArgs e) {
			if(Prefs.UpdateBool(PrefName.ChartNonPatientWarn,checkChartNonPatientWarn.Checked)) {
				_changed=true;
			}
		}

		private void checkTreatPlanItemized_Click(object sender,EventArgs e) {
			if(Prefs.UpdateBool(PrefName.TreatPlanItemized,checkTreatPlanItemized.Checked)) {
				_changed=true;
			}
		}

		private void checkAppointmentTimeIsLocked_MouseUp(object sender,MouseEventArgs e) {
			if(checkAppointmentTimeIsLocked.Checked) {
				if(MsgBox.Show(this,MsgBoxButtons.YesNo,"Would you like to lock appointment times for all existing appointments?")){
					Appointments.SetAptTimeLocked();
				}
			}
		}

		private void comboCobRule_SelectionChangeCommitted(object sender,EventArgs e) {
			if(MsgBox.Show(this,MsgBoxButtons.YesNo,"Would you like to change the COB rule for all existing insurance plans?")) {
				InsPlans.UpdateCobRuleForAll((EnumCobRule)comboCobRule.SelectedIndex);
			}
		}

		private void checkProcLockingIsAllowed_Click(object sender,EventArgs e) {
			if(checkProcLockingIsAllowed.Checked) {//if user is checking box			
				if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"This option is not normally used, because all notes are already locked internally, and all changes to notes are viewable in the audit mode of the Chart module.  This option is only for offices that insist on locking each procedure and only allowing notes to be appended.  Using this option, there really is no way to unlock a procedure, regardless of security permission.  So locked procedures can instead be marked as invalid in the case of mistakes.  But it's a hassle to mark procedures invalid, and they also cause clutter.  This option can be turned off later, but locked procedures will remain locked.\r\n\r\nContinue anyway?")) {
					checkProcLockingIsAllowed.Checked=false;
				}
			}
			else {//unchecking box
				MsgBox.Show(this,"Turning off this option will not affect any procedures that are already locked or invalidated.");
			}
		}

		private void butDiagnosisCode_Click(object sender,EventArgs e) {
			if(checkDxIcdVersion.Checked) {//ICD-10
				FormIcd10s formI=new FormIcd10s();
				formI.IsSelectionMode=true;
				if(formI.ShowDialog()==DialogResult.OK) {
					textICD9DefaultForNewProcs.Text=formI.SelectedIcd10.Icd10Code;
				}
			}
			else {//ICD-9
				FormIcd9s formI=new FormIcd9s();
				formI.IsSelectionMode=true;
				if(formI.ShowDialog()==DialogResult.OK) {
					textICD9DefaultForNewProcs.Text=formI.SelectedIcd9.ICD9Code;
				}
			}
		}

		private void SetIcdLabels() {
			byte icdVersion=9;
			if(checkDxIcdVersion.Checked) {
				icdVersion=10;
			}
			labelIcdCodeDefault.Text=Lan.g(this,"Default ICD")+"-"+icdVersion+" "+Lan.g(this,"code for new procedures");
		}

		private void checkDxIcdVersion_Click(object sender,EventArgs e) {
			SetIcdLabels();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			float percent=0;//Placeholder
			if(!float.TryParse(textDiscountPercentage.Text,out percent)) {
				MsgBox.Show(this,"Procedure discount percent is invalid. Please enter a valid number to continue.");
				return;
			}
			if(comboBrokenApptAdjType.SelectedIndex==-1){
				MsgBox.Show(this,"The selected Broken Appointment adjustment type in Appointment Module Preferences is hidden. Choose an adjustment type "+
					"that is not hidden in order to proceed.");
				return;
			}
			if(comboFinanceChargeAdjType.SelectedIndex==-1) {
				MsgBox.Show(this,"The selected Finance Charge adjustment type in Account Module Preferences is hidden. Choose an adjustment type that is "
					+"not hidden in order to proceed.");
				return;
			}
			if(comboBillingChargeAdjType.SelectedIndex==-1) {
				MsgBox.Show(this,"The selected Billing Charge adjustment type in Account Module Preferences is hidden. Choose an adjustment type that is "
					+"not hidden in order to proceed.");
				return;
			}
			if(comboProcDiscountType.SelectedIndex==-1) {
				MsgBox.Show(this,"The selected Procedure Discount adjustment type in Treatment Plan Module Preferences is hidden. Choose an adjustment "
					+"type that is not hidden in order to proceed.");
				return;
			}
			if(textStatementsCalcDueDate.errorProvider1.GetError(textStatementsCalcDueDate)!=""
				| textPayPlansBillInAdvanceDays.errorProvider1.GetError(textPayPlansBillInAdvanceDays)!=""
				| textBillingElectBatchMax.errorProvider1.GetError(textBillingElectBatchMax)!="")
			{
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			int noteLength=0;//Placeholder
			if(!int.TryParse(textApptBubNoteLength.Text,out noteLength)) {
				MsgBox.Show(this,"Max appointment note length is invalid. Please enter a valid number to continue.");
				return;
			}
			if(noteLength<0) {
				MsgBox.Show(this,"Max appointment note length cannot be a negative number.");
				return;
			}
			int waitingRoomAlertTime=0;
			try {
				waitingRoomAlertTime=PIn.Int(textWaitRoomWarn.Text);
				if(waitingRoomAlertTime<0) {
					throw new ApplicationException("Waiting room time cannot be negative");//User never sees this message.
				}
			}
			catch {
				MsgBox.Show(this,"Waiting room alert time is invalid.");
				return;
			}
			int daysStop=0;
			if(!int.TryParse(textMedDefaultStopDays.Text,out daysStop)) {
				MsgBox.Show(this,"Days until medication order stop date entered was is invalid. Please enter a valid number to continue.");
				return;
			}
			if(daysStop<0) {
				MsgBox.Show(this,"Days until medication order stop date cannot be a negative number.");
				return;
			}
			if(PrefC.GetString(PrefName.TreatmentPlanNote)!=textTreatNote.Text) {
				List<long> listTreatPlanNums=TreatPlans.GetNumsByNote(PrefC.GetString(PrefName.TreatmentPlanNote));//Find active/inactive TP's that match exactly.
				if(listTreatPlanNums.Count>0) {
					DialogResult dr=MessageBox.Show(Lan.g(this,"Unsaved treatment plans found with default notes")+": "+listTreatPlanNums.Count+"\r\n"
						+Lan.g(this,"Would you like to change them now?"),"",MessageBoxButtons.YesNoCancel);
					switch(dr) {
						case DialogResult.Cancel:
							return;
						case DialogResult.Yes:
						case DialogResult.OK:
							TreatPlans.UpdateNotes(textTreatNote.Text,listTreatPlanNums);//change tp notes
							break;
						default://includes "No"
							//do nothing
							break;
					}
				}
			}//end if TP Note Changed
			if(
				#region Appointment Module
				Prefs.UpdateBool(PrefName.AppointmentBubblesDisabled,checkAppointmentBubblesDisabled.Checked)
				| Prefs.UpdateBool(PrefName.ApptBubbleDelay,checkApptBubbleDelay.Checked)
				| Prefs.UpdateBool(PrefName.SolidBlockouts,checkSolidBlockouts.Checked)
				| Prefs.UpdateBool(PrefName.BrokenApptCommLogNotAdjustment,checkBrokenApptNote.Checked)
				| Prefs.UpdateBool(PrefName.BrokenApptAdjustmentWithProcedure,checkBrokenApptAdjustmentWithProcedure.Checked)
				| Prefs.UpdateBool(PrefName.BrokenApptCommLogWithProcedure,checkBrokenApptCommLogWithProcedure.Checked)
				| Prefs.UpdateLong(PrefName.BrokenAppointmentAdjustmentType,listPosAdjTypes[comboBrokenApptAdjType.SelectedIndex].DefNum)
				| Prefs.UpdateBool(PrefName.ApptExclamationShowForUnsentIns,checkApptExclamation.Checked)
				| Prefs.UpdateBool(PrefName.ApptModuleRefreshesEveryMinute,checkApptRefreshEveryMinute.Checked)
				| Prefs.UpdateInt(PrefName.AppointmentSearchBehavior,comboSearchBehavior.SelectedIndex)
				| Prefs.UpdateBool(PrefName.AppointmentTimeIsLocked,checkAppointmentTimeIsLocked.Checked)
				| Prefs.UpdateInt(PrefName.AppointmentBubblesNoteLength,noteLength)
				| Prefs.UpdateBool(PrefName.WaitingRoomFilterByView,checkWaitingRoomFilterByView.Checked)
				| Prefs.UpdateInt(PrefName.WaitingRoomAlertTime,waitingRoomAlertTime)
				| Prefs.UpdateInt(PrefName.WaitingRoomAlertColor,butColor.BackColor.ToArgb())
				| Prefs.UpdateBool(PrefName.ApptModuleDefaultToWeek,checkApptModuleDefaultToWeek.Checked)
				#endregion
				#region Family Module
				//| Prefs.UpdateBool(PrefName.MedicalEclaimsEnabled,checkMedicalEclaimsEnabled.Checked)
				| Prefs.UpdateBool(PrefName.InsurancePlansShared,checkInsurancePlansShared.Checked)
				| Prefs.UpdateBool(PrefName.InsDefaultPPOpercent,checkPPOpercentage.Checked)
				| Prefs.UpdateBool(PrefName.AllowedFeeSchedsAutomate,checkAllowedFeeSchedsAutomate.Checked)
				| Prefs.UpdateBool(PrefName.CoPay_FeeSchedule_BlankLikeZero,checkCoPayFeeScheduleBlankLikeZero.Checked)
				| Prefs.UpdateBool(PrefName.InsDefaultShowUCRonClaims,checkInsDefaultShowUCRonClaims.Checked)
				| Prefs.UpdateBool(PrefName.InsDefaultAssignBen,checkInsDefaultAssignmentOfBenefits.Checked)
				| Prefs.UpdateInt(PrefName.InsDefaultCobRule,comboCobRule.SelectedIndex)
				| Prefs.UpdateBool(PrefName.TextMsgOkStatusTreatAsNo,checkTextMsgOkStatusTreatAsNo.Checked)
				| Prefs.UpdateBool(PrefName.FamPhiAccess,checkFamPhiAccess.Checked)
				| Prefs.UpdateBool(PrefName.InsPPOsecWriteoffs,checkInsPPOsecWriteoffs.Checked)
				| Prefs.UpdateBool(PrefName.ShowFeatureGoogleMaps,checkGoogleAddress.Checked)
				| Prefs.UpdateBool(PrefName.PriProvDefaultToSelectProv,checkSelectProv.Checked)
				#endregion
				#region Account Module
				| Prefs.UpdateBool(PrefName.BalancesDontSubtractIns,checkBalancesDontSubtractIns.Checked)
				| Prefs.UpdateBool(PrefName.AgingCalculatedMonthlyInsteadOfDaily,checkAgingMonthly.Checked)
				| Prefs.UpdateBool(PrefName.StoreCCnumbers,checkStoreCCnumbers.Checked)
				| Prefs.UpdateBool(PrefName.StoreCCtokens,checkStoreCCTokens.Checked)
				| Prefs.UpdateLong(PrefName.FinanceChargeAdjustmentType,listPosAdjTypes[comboFinanceChargeAdjType.SelectedIndex].DefNum)
				| Prefs.UpdateLong(PrefName.BillingChargeAdjustmentType,listPosAdjTypes[comboBillingChargeAdjType.SelectedIndex].DefNum)
				| Prefs.UpdateBool(PrefName.ProviderIncomeTransferShows,checkProviderIncomeShows.Checked)
				| Prefs.UpdateBool(PrefName.ShowAccountFamilyCommEntries,checkShowFamilyCommByDefault.Checked)
				| Prefs.UpdateBool(PrefName.ClaimFormTreatDentSaysSigOnFile,checkClaimFormTreatDentSaysSigOnFile.Checked)
				| Prefs.UpdateString(PrefName.ClaimAttachExportPath,textClaimAttachPath.Text)
				| Prefs.UpdateBool(PrefName.EclaimsSeparateTreatProv,checkEclaimsSeparateTreatProv.Checked)
				| Prefs.UpdateBool(PrefName.ClaimsValidateACN,checkClaimsValidateACN.Checked)
				| Prefs.UpdateBool(PrefName.ClaimMedTypeIsInstWhenInsPlanIsMedical,checkClaimMedTypeIsInstWhenInsPlanIsMedical.Checked)
				| Prefs.UpdateBool(PrefName.AccountShowPaymentNums,checkAccountShowPaymentNums.Checked)
				| Prefs.UpdateBool(PrefName.StatementsUseSheets,checkStatementsUseSheets.Checked)
				| Prefs.UpdateBool(PrefName.PaymentsPromptForAutoSplit,checkPromptAutoSplit.Checked)
				| Prefs.UpdateString(PrefName.InsWriteoffDescript,textInsWriteoffDescript.Text)
				#endregion
				#region TP Module
				| Prefs.UpdateString(PrefName.TreatmentPlanNote,textTreatNote.Text)
				| Prefs.UpdateBool(PrefName.TreatPlanShowGraphics,checkTreatPlanShowGraphics.Checked)
				| Prefs.UpdateBool(PrefName.TreatPlanShowCompleted,checkTreatPlanShowCompleted.Checked)
				| Prefs.UpdateLong(PrefName.TreatPlanDiscountAdjustmentType,listNegAdjTypes[comboProcDiscountType.SelectedIndex].DefNum)
				| Prefs.UpdateDouble(PrefName.TreatPlanDiscountPercent,percent)
				| Prefs.UpdateBool(PrefName.TreatPlanSaveSignedToPdf,checkTPSaveSigned.Checked)
				| Prefs.UpdateBool(PrefName.TreatPlanUseSheets,checkTreatPlanUseSheets.Checked)
				#endregion
				#region Chart Module
				| Prefs.UpdateBool(PrefName.AutoResetTPEntryStatus,checkAutoClearEntryStatus.Checked)
				| Prefs.UpdateBool(PrefName.AllowSettingProcsComplete,checkAllowSettingProcsComplete.Checked)
				| Prefs.UpdateLong(PrefName.UseInternationalToothNumbers,comboToothNomenclature.SelectedIndex)
				| Prefs.UpdateBool(PrefName.ProcGroupNoteDoesAggregate,checkProcGroupNoteDoesAggregate.Checked)
				| Prefs.UpdateBool(PrefName.MedicalFeeUsedForNewProcs,checkMedicalFeeUsedForNewProcs.Checked)
				| Prefs.UpdateByte(PrefName.DxIcdVersion,(byte)(checkDxIcdVersion.Checked?10:9))
				| Prefs.UpdateString(PrefName.ICD9DefaultForNewProcs,textICD9DefaultForNewProcs.Text)
				| Prefs.UpdateBool(PrefName.ProcLockingIsAllowed,checkProcLockingIsAllowed.Checked)
				| Prefs.UpdateInt(PrefName.MedDefaultStopDays,daysStop)
				| Prefs.UpdateBool(PrefName.UseProviderColorsInChart,checkProvColorChart.Checked)
				//| Prefs.UpdateBool(PrefName.ToothChartMoveMenuToRight,checkToothChartMoveMenuToRight.Checked)
				//| Prefs.UpdateBool(PrefName.ChartQuickAddHideAmalgam, checkChartQuickAddHideAmalgam.Checked) //Deprecated.
				//| Prefs.UpdateBool(PrefName.ChartAddProcNoRefreshGrid,checkChartAddProcNoRefreshGrid.Checked)//Not implemented.  May revisit someday.
				#endregion
				#region Image Module
				| Prefs.UpdateBool(PrefName.ImagesModuleTreeIsCollapsed,checkImagesModuleTreeIsCollapsed.Checked)
				#endregion
				#region Manage Module
				| Prefs.UpdateBool(PrefName.RxSendNewToQueue,checkRxSendNewToQueue.Checked)
				| Prefs.UpdateInt(PrefName.TimeCardOvertimeFirstDayOfWeek,comboTimeCardOvertimeFirstDayOfWeek.SelectedIndex)
				| Prefs.UpdateBool(PrefName.TimeCardADPExportIncludesName,checkTimeCardADP.Checked)
				| Prefs.UpdateBool(PrefName.ClaimsSendWindowValidatesOnLoad,checkClaimsSendWindowValidateOnLoad.Checked)
				| Prefs.UpdateBool(PrefName.StatementShowReturnAddress,checkStatementShowReturnAddress.Checked)
				| Prefs.UpdateBool(PrefName.StatementShowCreditCard,checkShowCC.Checked)
				| Prefs.UpdateBool(PrefName.StatementShowNotes,checkStatementShowNotes.Checked)
				| Prefs.UpdateBool(PrefName.StatementShowAdjNotes,checkStatementShowAdjNotes.Checked)
				| Prefs.UpdateBool(PrefName.StatementShowProcBreakdown,checkStatementShowProcBreakdown.Checked)
				| Prefs.UpdateBool(PrefName.StatementAccountsUseChartNumber,comboUseChartNum.SelectedIndex==1)
				| Prefs.UpdateLong(PrefName.PayPlansBillInAdvanceDays,PIn.Long(textPayPlansBillInAdvanceDays.Text))
				| Prefs.UpdateBool(PrefName.IntermingleFamilyDefault,checkIntermingleDefault.Checked)
				| Prefs.UpdateInt(PrefName.BillingElectBatchMax,PIn.Int(textBillingElectBatchMax.Text))
				#endregion
				)//end big if statement
			{
				_changed=true;
			}
			if(textStatementsCalcDueDate.Text==""){
				if(Prefs.UpdateLong(PrefName.StatementsCalcDueDate,-1)){
					_changed=true;
				}
			}
			else{
				if(Prefs.UpdateLong(PrefName.StatementsCalcDueDate,PIn.Long(textStatementsCalcDueDate.Text))){
					_changed=true;
				}
			}
			long timeArrivedTrigger=0;
			if(comboTimeArrived.SelectedIndex>0){
				timeArrivedTrigger=DefC.Short[(int)DefCat.ApptConfirmed][comboTimeArrived.SelectedIndex-1].DefNum;
			}
			if(Prefs.UpdateLong(PrefName.AppointmentTimeArrivedTrigger,timeArrivedTrigger)){
				_changed=true;
			}
			long timeSeatedTrigger=0;
			if(comboTimeSeated.SelectedIndex>0){
				timeSeatedTrigger=DefC.Short[(int)DefCat.ApptConfirmed][comboTimeSeated.SelectedIndex-1].DefNum;
			}
			if(Prefs.UpdateLong(PrefName.AppointmentTimeSeatedTrigger,timeSeatedTrigger)){
				_changed=true;
			}
			long timeDismissedTrigger=0;
			if(comboTimeDismissed.SelectedIndex>0){
				timeDismissedTrigger=DefC.Short[(int)DefCat.ApptConfirmed][comboTimeDismissed.SelectedIndex-1].DefNum;
			}
			if(Prefs.UpdateLong(PrefName.AppointmentTimeDismissedTrigger,timeDismissedTrigger)){
				_changed=true;
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormModuleSetup_FormClosing(object sender,FormClosingEventArgs e) {
			if(_changed){
				DataValid.SetInvalid(InvalidType.Prefs);
			}
		}

		

		

		

		

		

		

		
	}
}






