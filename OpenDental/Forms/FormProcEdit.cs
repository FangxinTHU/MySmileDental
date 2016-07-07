/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using OpenDentBusiness;
using CodeBase;
using SparksToothChart;
using OpenDental.UI;
using System.Threading;
using System.Linq;


namespace OpenDental{
///<summary></summary>
	public class FormProcEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Label labelDate;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label labelAmount;
		private System.Windows.Forms.TextBox textProc;
		private System.Windows.Forms.TextBox textSurfaces;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textDesc;
		private System.Windows.Forms.Label label7;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butDelete;
		private System.Windows.Forms.TextBox textRange;
		private System.Windows.Forms.Label labelTooth;
		private System.Windows.Forms.Label labelRange;
		private System.Windows.Forms.Label labelSurfaces;
		private System.Windows.Forms.GroupBox groupQuadrant;
		private System.Windows.Forms.RadioButton radioUR;
		private System.Windows.Forms.RadioButton radioUL;
		private System.Windows.Forms.RadioButton radioLL;
		private System.Windows.Forms.RadioButton radioLR;
		private System.Windows.Forms.GroupBox groupArch;
		private System.Windows.Forms.RadioButton radioU;
		private System.Windows.Forms.RadioButton radioL;
		private System.Windows.Forms.GroupBox groupSextant;
		private System.Windows.Forms.RadioButton radioS1;
		private System.Windows.Forms.RadioButton radioS3;
		private System.Windows.Forms.RadioButton radioS2;
		private System.Windows.Forms.RadioButton radioS4;
		private System.Windows.Forms.RadioButton radioS5;
		private System.Windows.Forms.RadioButton radioS6;
		private System.Windows.Forms.Label label9;
		///<summary>Mostly used for permissions.</summary>
		public bool IsNew;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label labelClaim;
		private System.Windows.Forms.ListBox listBoxTeeth;
		private System.Windows.Forms.ListBox listBoxTeeth2;
		private OpenDental.UI.Button butChange;
		//private ProcStat OriginalStatus;
		private ErrorProvider errorProvider2=new ErrorProvider();
		private System.Windows.Forms.TextBox textTooth;
		private OpenDental.UI.Button butEditAnyway;
		private System.Windows.Forms.Label labelDx;
		private System.Windows.Forms.ComboBox comboPlaceService;
		private System.Windows.Forms.Label labelPlaceService;
		private OpenDental.UI.Button butSetComplete;
		private System.Windows.Forms.Label labelPriority;
		private ProcedureCode ProcedureCode2;
		private System.Windows.Forms.Label labelSetComplete;
		private OpenDental.UI.Button butAddEstimate;
		private Procedure ProcCur;
		private Procedure ProcOld;
		//private List<ClaimProc> ClaimProcList;
		private OpenDental.ValidDouble textProcFee;
		private System.Windows.Forms.CheckBox checkNoBillIns;
		private OpenDental.ODtextBox textNotes;
		private List<ClaimProc> ClaimProcsForProc;
		//private Adjustment[] AdjForProc;
		private ArrayList PaySplitsForProc;
		private ArrayList AdjustmentsForProc;
		private Patient PatCur;
		private Family FamCur;
		private OpenDental.UI.Button butAddAdjust;
		private List <InsPlan> PlanList;
		private System.Windows.Forms.Label labelIncomplete;
		private OpenDental.ValidDate textDateEntry;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.ComboBox comboClinic;
		private System.Windows.Forms.Label labelClinic;
		///<summary>List of all payments (not paysplits) that this procedure is attached to.</summary>
		private List<Payment> PaymentsForProc;
		private Userod CurUser;
		//private uint m_autoAPIMsg;//ENP
		private const string APPBAR_AUTOMATION_API_MESSAGE = "EZNotes.AppBarStandalone.Auto.API.Message"; 
		private const uint MSG_RESTORE=2;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textMedicalCode;
		private System.Windows.Forms.Label labelDiagnosticCode1;
		private System.Windows.Forms.TextBox textDiagnosticCode;//ENP
		private const uint MSG_GETLASTNOTE=3;
		private System.Windows.Forms.CheckBox checkIsPrincDiag;//ENP
		private List<PatPlan> PatPlanList;
		private Label label14;
		private Label label15;
		private Label label16;
		private OpenDental.UI.Button butClearSig;
		private OpenDental.UI.SignatureBox sigBox;
		private List <Benefit> BenefitList;
		private bool SigChanged;
		private ComboBox comboProvNum;
		private ComboBox comboDx;
		private ComboBox comboPriority;
		private TextBox textUser;
		//private Label label17;
		//private Label label18;
		private Label label19;
		private TextBox textCodeMod1;
		private ComboBox comboBillingTypeTwo;
		private Label labelBillingTypeTwo;
		private ComboBox comboBillingTypeOne;
		private Label labelBillingTypeOne;
		private TextBox textCodeMod4;
		private TextBox textCodeMod3;
		private TextBox textCodeMod2;
		private TextBox textRevCode;
		private Label label22;
		private TextBox textUnitQty;
		private Label label21;
		private OpenDental.UI.Button buttonUseAutoNote;
		private ToolTip toolTip1;
		private IContainer components;
		private ValidDate textDateTP;
		private Label label27;
		private Label label26;
		///<summary>This keeps the noteChanged event from erasing the signature when first loading.</summary>
		private bool IsStartingUp;
		private List<Claim> ClaimList;
		//private OpenDental.UI.Button butTopazSign;
		private Panel panelSurfaces;
		private OpenDental.UI.Button butD;
		private OpenDental.UI.Button butBF;
		private OpenDental.UI.Button butL;
		private OpenDental.UI.Button butM;
		private OpenDental.UI.Button butV;
		private OpenDental.UI.Button butOI;
		private OpenDental.UI.Button butTopazSign;
		private Label labelInvalidSig;
		private Control sigBoxTopaz;
    //private bool allowTopaz;
		private OpenDental.UI.Button butPickSite;
		private TextBox textSite;
		private Label labelSite;
		private ODGrid gridIns;
		private bool StartedAttachedToClaim;
		public List<ClaimProcHist> HistList;
		private OpenDental.UI.Button butPickProv;
		private CheckBox checkHideGraphics;
		private Label label3;
		private Label label4;
		private ValidDate textDateOriginalProsth;
		private ListBox listProsth;
		private GroupBox groupProsth;
		private CheckBox checkTypeCodeA;
		private CheckBox checkTypeCodeB;
		private CheckBox checkTypeCodeC;
		private CheckBox checkTypeCodeE;
		private CheckBox checkTypeCodeL;
		private CheckBox checkTypeCodeX;
		private CheckBox checkTypeCodeS;
		private GroupBox groupCanadianProcTypeCode;
		private Label labelDateStop;
		private Label labelDateSched;
		private Label labelDPC;
		private Label labelStatus;
		private ComboBox comboStatus;
		private ValidDate textDateScheduled;
		private ComboBox comboDPC;
		private ValidDate textDateStop;
		private CheckBox checkIsEffComm;
		private CheckBox checkIsOnCall;
		private CheckBox checkIsRepair;
		public List<ClaimProcHist> LoopList;
		private Label labelEndTime;
		private OpenDental.UI.Button butNow;
		private ValidDate textDate;
		private TextBox textTimeEnd;
		private Label labelScheduleBy;
		private OrionProc OrionProcCur;
		private OrionProc OrionProcOld;
		private DateTime CancelledScheduleByDate;
		public long OrionProvNum;
		public bool OrionDentist;
		private TextBox textTimeStart;
		private Label labelStartTime;
		private Label labelDPCpost;
		private ComboBox comboDPCpost;
		private ComboBox comboPrognosis;
		private Label labelPrognosis;
		private ComboBox comboProcStatus;
		private ComboBox comboDrugUnit;
		private Label label1;
		private Label label5;
		private TextBox textDrugNDC;
		private Label label10;
		private TextBox textDrugQty;
		private Label label13;
		private TextBox textReferral;
		private UI.Button butReferral;
		private Label labelClaimNote;
		private ODtextBox textClaimNote;
		private TextBox textTimeFinal;
		private Label labelTimeFinal;
		private TabControl tabControl;
		private TabPage tabPageFinancial;
		private TabPage tabPageMedical;
		private TabPage tabPageMisc;
		private TabPage tabPageCanada;
		private TabPage tabPageOrion;
		private Label label17;
		private ComboBox comboUnitType;
		private Label labelCanadaLabFee2;
		private Label labelCanadaLabFee1;
		private ValidDouble textCanadaLabFee2;
		private ValidDouble textCanadaLabFee1;
		private List<InsSub> SubList;
		private Label labelLocked;
		private UI.Button butAppend;
		private UI.Button butLock;
		private UI.Button butInvalidate;
		private Label label18;
		private TextBox textBillingNote;
		private UI.Button butSearch;
		private UI.Button butSnomedBodySiteSelect;
		private Label labelSnomedCtBodySite;
		private TextBox textSnomedBodySite;
		private List<Procedure> canadaLabFees;
		private UI.Button butNoneSnomedBodySite;
		private TextBox textDiagnosticCode2;
		private Label labelDiagnosticCode2;
		private TextBox textDiagnosticCode3;
		private Label labelDiagnosticCode3;
		private TextBox textDiagnosticCode4;
		private Label labelDiagnosticCode4;
		private UI.Button butNoneDiagnosisCode1;
		private UI.Button butDiagnosisCode1;
		private UI.Button butNoneDiagnosisCode2;
		private UI.Button butDiagnosisCode2;
		private UI.Button butNoneDiagnosisCode4;
		private UI.Button butDiagnosisCode4;
		private UI.Button butNoneDiagnosisCode3;
		private UI.Button butDiagnosisCode3;
		private Label label20;
		private ValidDouble textDiscount;
		private UI.Button butNoneProvOrdering;
		private UI.Button butPickProvOrdering;
		private ComboBox comboProvNumOrdering;
		private Label label95;
		private Snomed _snomedBodySite=null;
		private CheckBox checkIsDateProsthEst;
		private CheckBox checkIcdVersion;
		private Label label11;
		private ODGrid gridAdj;
		private ODGrid gridPay;
		private long _provNumOrderingSelected;
		private bool _isQuickAdd=false;
		
		///<summary>Inserts are not done within this dialog, but must be done ahead of time from outside.  You must specify a procedure to edit, and only the changes that are made in this dialog get saved.  Only used when double click in Account, Chart, TP, and in ContrChart.AddProcedure().  The procedure may be deleted if new, and user hits Cancel.</summary>
		public FormProcEdit(Procedure proc,Patient patCur,Family famCur,bool isQuickAdd=false){
			ProcCur=proc;
			ProcOld=proc.Copy();
			PatCur=patCur;
			FamCur=famCur;
			SubList=InsSubs.RefreshForFam(FamCur);
			PlanList=InsPlans.RefreshForSubList(SubList);
			//HistList=null;
			//LoopList=null;
			InitializeComponent();
			Lan.F(this);
			Lan.C(this,new Control[] {
				tabPageCanada,
				tabPageFinancial,
				tabPageMedical,
				tabPageMisc,
				tabPageOrion,
			});
			//allowTopaz=(Environment.OSVersion.Platform!=PlatformID.Unix && !CodeBase.ODEnvironment.Is64BitOperatingSystem());
			sigBox.SetTabletState(1);
			//if(!allowTopaz) {
			//	butTopazSign.Visible=false;
			//	sigBox.Visible=true;
			//}
			//else{
				//Add signature box for Topaz signatures.
				sigBoxTopaz=CodeBase.TopazWrapper.GetTopaz();
				sigBoxTopaz.Location=sigBox.Location;//this puts both boxes in the same spot.
				sigBoxTopaz.Name="sigBoxTopaz";
				sigBoxTopaz.Size=new System.Drawing.Size(362,79);
				sigBoxTopaz.TabIndex=92;
				sigBoxTopaz.Text="sigPlusNET1";
				sigBoxTopaz.Visible=false;
				sigBoxTopaz.Leave+=new EventHandler(sigBoxTopaz_Leave);
				Controls.Add(sigBoxTopaz);
				//It starts out accepting input. It will be set to 0 if a sig is already present.  It will be set back to 1 if note changes or if user clicks Clear.
				CodeBase.TopazWrapper.SetTopazState(sigBoxTopaz,1);
			//}
			if(!PrefC.GetBool(PrefName.ShowFeatureMedicalInsurance)) {
				tabControl.TabPages.Remove(tabPageMedical);
				//groupMedical.Visible=false;
			}
			_isQuickAdd=isQuickAdd;
			if(isQuickAdd) {
				this.WindowState=FormWindowState.Minimized;
			}
		}

		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		private void InitializeComponent(){
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProcEdit));
			this.labelDate = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.labelTooth = new System.Windows.Forms.Label();
			this.labelSurfaces = new System.Windows.Forms.Label();
			this.labelAmount = new System.Windows.Forms.Label();
			this.textProc = new System.Windows.Forms.TextBox();
			this.textTooth = new System.Windows.Forms.TextBox();
			this.textSurfaces = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textDesc = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.labelRange = new System.Windows.Forms.Label();
			this.textRange = new System.Windows.Forms.TextBox();
			this.groupQuadrant = new System.Windows.Forms.GroupBox();
			this.radioLR = new System.Windows.Forms.RadioButton();
			this.radioLL = new System.Windows.Forms.RadioButton();
			this.radioUL = new System.Windows.Forms.RadioButton();
			this.radioUR = new System.Windows.Forms.RadioButton();
			this.groupArch = new System.Windows.Forms.GroupBox();
			this.radioL = new System.Windows.Forms.RadioButton();
			this.radioU = new System.Windows.Forms.RadioButton();
			this.panelSurfaces = new System.Windows.Forms.Panel();
			this.groupSextant = new System.Windows.Forms.GroupBox();
			this.radioS6 = new System.Windows.Forms.RadioButton();
			this.radioS5 = new System.Windows.Forms.RadioButton();
			this.radioS4 = new System.Windows.Forms.RadioButton();
			this.radioS2 = new System.Windows.Forms.RadioButton();
			this.radioS3 = new System.Windows.Forms.RadioButton();
			this.radioS1 = new System.Windows.Forms.RadioButton();
			this.label9 = new System.Windows.Forms.Label();
			this.labelDx = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.textTimeFinal = new System.Windows.Forms.TextBox();
			this.textTimeStart = new System.Windows.Forms.TextBox();
			this.textTimeEnd = new System.Windows.Forms.TextBox();
			this.label27 = new System.Windows.Forms.Label();
			this.label26 = new System.Windows.Forms.Label();
			this.listBoxTeeth = new System.Windows.Forms.ListBox();
			this.label12 = new System.Windows.Forms.Label();
			this.labelStartTime = new System.Windows.Forms.Label();
			this.labelEndTime = new System.Windows.Forms.Label();
			this.listBoxTeeth2 = new System.Windows.Forms.ListBox();
			this.labelTimeFinal = new System.Windows.Forms.Label();
			this.textDrugQty = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textDrugNDC = new System.Windows.Forms.TextBox();
			this.comboDrugUnit = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textRevCode = new System.Windows.Forms.TextBox();
			this.label22 = new System.Windows.Forms.Label();
			this.textUnitQty = new System.Windows.Forms.TextBox();
			this.label21 = new System.Windows.Forms.Label();
			this.textCodeMod4 = new System.Windows.Forms.TextBox();
			this.textCodeMod3 = new System.Windows.Forms.TextBox();
			this.textCodeMod2 = new System.Windows.Forms.TextBox();
			this.label19 = new System.Windows.Forms.Label();
			this.textCodeMod1 = new System.Windows.Forms.TextBox();
			this.checkIsPrincDiag = new System.Windows.Forms.CheckBox();
			this.labelDiagnosticCode1 = new System.Windows.Forms.Label();
			this.textDiagnosticCode = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.textMedicalCode = new System.Windows.Forms.TextBox();
			this.labelClaim = new System.Windows.Forms.Label();
			this.comboPlaceService = new System.Windows.Forms.ComboBox();
			this.labelPlaceService = new System.Windows.Forms.Label();
			this.labelPriority = new System.Windows.Forms.Label();
			this.labelSetComplete = new System.Windows.Forms.Label();
			this.checkNoBillIns = new System.Windows.Forms.CheckBox();
			this.labelIncomplete = new System.Windows.Forms.Label();
			this.comboClinic = new System.Windows.Forms.ComboBox();
			this.labelClinic = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.comboPriority = new System.Windows.Forms.ComboBox();
			this.comboDx = new System.Windows.Forms.ComboBox();
			this.comboProvNum = new System.Windows.Forms.ComboBox();
			this.textUser = new System.Windows.Forms.TextBox();
			this.comboBillingTypeTwo = new System.Windows.Forms.ComboBox();
			this.labelBillingTypeTwo = new System.Windows.Forms.Label();
			this.comboBillingTypeOne = new System.Windows.Forms.ComboBox();
			this.labelBillingTypeOne = new System.Windows.Forms.Label();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.labelInvalidSig = new System.Windows.Forms.Label();
			this.textSite = new System.Windows.Forms.TextBox();
			this.labelSite = new System.Windows.Forms.Label();
			this.checkHideGraphics = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.listProsth = new System.Windows.Forms.ListBox();
			this.groupProsth = new System.Windows.Forms.GroupBox();
			this.checkIsDateProsthEst = new System.Windows.Forms.CheckBox();
			this.checkTypeCodeA = new System.Windows.Forms.CheckBox();
			this.checkTypeCodeB = new System.Windows.Forms.CheckBox();
			this.checkTypeCodeC = new System.Windows.Forms.CheckBox();
			this.checkTypeCodeE = new System.Windows.Forms.CheckBox();
			this.checkTypeCodeL = new System.Windows.Forms.CheckBox();
			this.checkTypeCodeX = new System.Windows.Forms.CheckBox();
			this.checkTypeCodeS = new System.Windows.Forms.CheckBox();
			this.groupCanadianProcTypeCode = new System.Windows.Forms.GroupBox();
			this.labelDPCpost = new System.Windows.Forms.Label();
			this.comboDPCpost = new System.Windows.Forms.ComboBox();
			this.labelScheduleBy = new System.Windows.Forms.Label();
			this.checkIsRepair = new System.Windows.Forms.CheckBox();
			this.checkIsEffComm = new System.Windows.Forms.CheckBox();
			this.checkIsOnCall = new System.Windows.Forms.CheckBox();
			this.comboDPC = new System.Windows.Forms.ComboBox();
			this.comboStatus = new System.Windows.Forms.ComboBox();
			this.labelStatus = new System.Windows.Forms.Label();
			this.labelDateStop = new System.Windows.Forms.Label();
			this.labelDateSched = new System.Windows.Forms.Label();
			this.labelDPC = new System.Windows.Forms.Label();
			this.comboPrognosis = new System.Windows.Forms.ComboBox();
			this.labelPrognosis = new System.Windows.Forms.Label();
			this.comboProcStatus = new System.Windows.Forms.ComboBox();
			this.label13 = new System.Windows.Forms.Label();
			this.textReferral = new System.Windows.Forms.TextBox();
			this.labelClaimNote = new System.Windows.Forms.Label();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPageFinancial = new System.Windows.Forms.TabPage();
			this.gridPay = new OpenDental.UI.ODGrid();
			this.gridAdj = new OpenDental.UI.ODGrid();
			this.label20 = new System.Windows.Forms.Label();
			this.gridIns = new OpenDental.UI.ODGrid();
			this.tabPageMedical = new System.Windows.Forms.TabPage();
			this.label11 = new System.Windows.Forms.Label();
			this.checkIcdVersion = new System.Windows.Forms.CheckBox();
			this.comboProvNumOrdering = new System.Windows.Forms.ComboBox();
			this.label95 = new System.Windows.Forms.Label();
			this.textDiagnosticCode2 = new System.Windows.Forms.TextBox();
			this.labelDiagnosticCode2 = new System.Windows.Forms.Label();
			this.textDiagnosticCode3 = new System.Windows.Forms.TextBox();
			this.labelDiagnosticCode3 = new System.Windows.Forms.Label();
			this.textDiagnosticCode4 = new System.Windows.Forms.TextBox();
			this.labelDiagnosticCode4 = new System.Windows.Forms.Label();
			this.labelSnomedCtBodySite = new System.Windows.Forms.Label();
			this.textSnomedBodySite = new System.Windows.Forms.TextBox();
			this.label17 = new System.Windows.Forms.Label();
			this.comboUnitType = new System.Windows.Forms.ComboBox();
			this.tabPageMisc = new System.Windows.Forms.TabPage();
			this.textBillingNote = new System.Windows.Forms.TextBox();
			this.label18 = new System.Windows.Forms.Label();
			this.tabPageCanada = new System.Windows.Forms.TabPage();
			this.labelCanadaLabFee2 = new System.Windows.Forms.Label();
			this.labelCanadaLabFee1 = new System.Windows.Forms.Label();
			this.tabPageOrion = new System.Windows.Forms.TabPage();
			this.labelLocked = new System.Windows.Forms.Label();
			this.butSearch = new OpenDental.UI.Button();
			this.butLock = new OpenDental.UI.Button();
			this.butInvalidate = new OpenDental.UI.Button();
			this.butAppend = new OpenDental.UI.Button();
			this.textDiscount = new OpenDental.ValidDouble();
			this.butAddEstimate = new OpenDental.UI.Button();
			this.butAddAdjust = new OpenDental.UI.Button();
			this.butNoneProvOrdering = new OpenDental.UI.Button();
			this.butPickProvOrdering = new OpenDental.UI.Button();
			this.butNoneDiagnosisCode1 = new OpenDental.UI.Button();
			this.butDiagnosisCode1 = new OpenDental.UI.Button();
			this.butNoneDiagnosisCode2 = new OpenDental.UI.Button();
			this.butDiagnosisCode2 = new OpenDental.UI.Button();
			this.butNoneDiagnosisCode4 = new OpenDental.UI.Button();
			this.butDiagnosisCode4 = new OpenDental.UI.Button();
			this.butNoneDiagnosisCode3 = new OpenDental.UI.Button();
			this.butDiagnosisCode3 = new OpenDental.UI.Button();
			this.butNoneSnomedBodySite = new OpenDental.UI.Button();
			this.butSnomedBodySiteSelect = new OpenDental.UI.Button();
			this.butPickSite = new OpenDental.UI.Button();
			this.textCanadaLabFee2 = new OpenDental.ValidDouble();
			this.textCanadaLabFee1 = new OpenDental.ValidDouble();
			this.textDateStop = new OpenDental.ValidDate();
			this.textDateScheduled = new OpenDental.ValidDate();
			this.textClaimNote = new OpenDental.ODtextBox();
			this.butReferral = new OpenDental.UI.Button();
			this.butPickProv = new OpenDental.UI.Button();
			this.butTopazSign = new OpenDental.UI.Button();
			this.buttonUseAutoNote = new OpenDental.UI.Button();
			this.sigBox = new OpenDental.UI.SignatureBox();
			this.butClearSig = new OpenDental.UI.Button();
			this.textDateOriginalProsth = new OpenDental.ValidDate();
			this.textNotes = new OpenDental.ODtextBox();
			this.butSetComplete = new OpenDental.UI.Button();
			this.butEditAnyway = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.textDate = new OpenDental.ValidDate();
			this.butNow = new OpenDental.UI.Button();
			this.butD = new OpenDental.UI.Button();
			this.butBF = new OpenDental.UI.Button();
			this.butL = new OpenDental.UI.Button();
			this.butM = new OpenDental.UI.Button();
			this.butV = new OpenDental.UI.Button();
			this.butOI = new OpenDental.UI.Button();
			this.textDateTP = new OpenDental.ValidDate();
			this.textDateEntry = new OpenDental.ValidDate();
			this.textProcFee = new OpenDental.ValidDouble();
			this.butChange = new OpenDental.UI.Button();
			this.groupQuadrant.SuspendLayout();
			this.groupArch.SuspendLayout();
			this.panelSurfaces.SuspendLayout();
			this.groupSextant.SuspendLayout();
			this.panel1.SuspendLayout();
			this.groupProsth.SuspendLayout();
			this.groupCanadianProcTypeCode.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.tabPageFinancial.SuspendLayout();
			this.tabPageMedical.SuspendLayout();
			this.tabPageMisc.SuspendLayout();
			this.tabPageCanada.SuspendLayout();
			this.tabPageOrion.SuspendLayout();
			this.SuspendLayout();
			// 
			// labelDate
			// 
			this.labelDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelDate.Location = new System.Drawing.Point(8, 44);
			this.labelDate.Name = "labelDate";
			this.labelDate.Size = new System.Drawing.Size(96, 14);
			this.labelDate.TabIndex = 0;
			this.labelDate.Text = "Date";
			this.labelDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(26, 63);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(79, 12);
			this.label2.TabIndex = 1;
			this.label2.Text = "Procedure";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelTooth
			// 
			this.labelTooth.Location = new System.Drawing.Point(68, 107);
			this.labelTooth.Name = "labelTooth";
			this.labelTooth.Size = new System.Drawing.Size(36, 12);
			this.labelTooth.TabIndex = 2;
			this.labelTooth.Text = "Tooth";
			this.labelTooth.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.labelTooth.Visible = false;
			// 
			// labelSurfaces
			// 
			this.labelSurfaces.Location = new System.Drawing.Point(33, 135);
			this.labelSurfaces.Name = "labelSurfaces";
			this.labelSurfaces.Size = new System.Drawing.Size(73, 16);
			this.labelSurfaces.TabIndex = 3;
			this.labelSurfaces.Text = "Surfaces";
			this.labelSurfaces.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.labelSurfaces.Visible = false;
			// 
			// labelAmount
			// 
			this.labelAmount.Location = new System.Drawing.Point(30, 158);
			this.labelAmount.Name = "labelAmount";
			this.labelAmount.Size = new System.Drawing.Size(75, 16);
			this.labelAmount.TabIndex = 4;
			this.labelAmount.Text = "Amount";
			this.labelAmount.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textProc
			// 
			this.textProc.Location = new System.Drawing.Point(106, 61);
			this.textProc.Name = "textProc";
			this.textProc.ReadOnly = true;
			this.textProc.Size = new System.Drawing.Size(76, 20);
			this.textProc.TabIndex = 6;
			// 
			// textTooth
			// 
			this.textTooth.Location = new System.Drawing.Point(106, 105);
			this.textTooth.Name = "textTooth";
			this.textTooth.Size = new System.Drawing.Size(35, 20);
			this.textTooth.TabIndex = 7;
			this.textTooth.Visible = false;
			this.textTooth.Validating += new System.ComponentModel.CancelEventHandler(this.textTooth_Validating);
			// 
			// textSurfaces
			// 
			this.textSurfaces.Location = new System.Drawing.Point(106, 133);
			this.textSurfaces.Name = "textSurfaces";
			this.textSurfaces.Size = new System.Drawing.Size(68, 20);
			this.textSurfaces.TabIndex = 4;
			this.textSurfaces.Visible = false;
			this.textSurfaces.TextChanged += new System.EventHandler(this.textSurfaces_TextChanged);
			this.textSurfaces.Validating += new System.ComponentModel.CancelEventHandler(this.textSurfaces_Validating);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(2, 81);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(103, 16);
			this.label6.TabIndex = 13;
			this.label6.Text = "Description";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDesc
			// 
			this.textDesc.BackColor = System.Drawing.SystemColors.Control;
			this.textDesc.Location = new System.Drawing.Point(106, 81);
			this.textDesc.Name = "textDesc";
			this.textDesc.Size = new System.Drawing.Size(216, 20);
			this.textDesc.TabIndex = 16;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(429, 163);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(73, 16);
			this.label7.TabIndex = 0;
			this.label7.Text = "&Notes";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelRange
			// 
			this.labelRange.Location = new System.Drawing.Point(24, 107);
			this.labelRange.Name = "labelRange";
			this.labelRange.Size = new System.Drawing.Size(82, 16);
			this.labelRange.TabIndex = 33;
			this.labelRange.Text = "Tooth Range";
			this.labelRange.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.labelRange.Visible = false;
			// 
			// textRange
			// 
			this.textRange.Location = new System.Drawing.Point(106, 105);
			this.textRange.Name = "textRange";
			this.textRange.Size = new System.Drawing.Size(100, 20);
			this.textRange.TabIndex = 34;
			this.textRange.Visible = false;
			// 
			// groupQuadrant
			// 
			this.groupQuadrant.Controls.Add(this.radioLR);
			this.groupQuadrant.Controls.Add(this.radioLL);
			this.groupQuadrant.Controls.Add(this.radioUL);
			this.groupQuadrant.Controls.Add(this.radioUR);
			this.groupQuadrant.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupQuadrant.Location = new System.Drawing.Point(104, 99);
			this.groupQuadrant.Name = "groupQuadrant";
			this.groupQuadrant.Size = new System.Drawing.Size(108, 56);
			this.groupQuadrant.TabIndex = 36;
			this.groupQuadrant.TabStop = false;
			this.groupQuadrant.Text = "Quadrant";
			this.groupQuadrant.Visible = false;
			// 
			// radioLR
			// 
			this.radioLR.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioLR.Location = new System.Drawing.Point(12, 36);
			this.radioLR.Name = "radioLR";
			this.radioLR.Size = new System.Drawing.Size(40, 16);
			this.radioLR.TabIndex = 3;
			this.radioLR.Text = "LR";
			this.radioLR.Click += new System.EventHandler(this.radioLR_Click);
			// 
			// radioLL
			// 
			this.radioLL.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioLL.Location = new System.Drawing.Point(64, 36);
			this.radioLL.Name = "radioLL";
			this.radioLL.Size = new System.Drawing.Size(40, 16);
			this.radioLL.TabIndex = 1;
			this.radioLL.Text = "LL";
			this.radioLL.Click += new System.EventHandler(this.radioLL_Click);
			// 
			// radioUL
			// 
			this.radioUL.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioUL.Location = new System.Drawing.Point(64, 16);
			this.radioUL.Name = "radioUL";
			this.radioUL.Size = new System.Drawing.Size(40, 16);
			this.radioUL.TabIndex = 0;
			this.radioUL.Text = "UL";
			this.radioUL.Click += new System.EventHandler(this.radioUL_Click);
			// 
			// radioUR
			// 
			this.radioUR.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioUR.Location = new System.Drawing.Point(12, 16);
			this.radioUR.Name = "radioUR";
			this.radioUR.Size = new System.Drawing.Size(40, 16);
			this.radioUR.TabIndex = 0;
			this.radioUR.Text = "UR";
			this.radioUR.Click += new System.EventHandler(this.radioUR_Click);
			// 
			// groupArch
			// 
			this.groupArch.Controls.Add(this.radioL);
			this.groupArch.Controls.Add(this.radioU);
			this.groupArch.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupArch.Location = new System.Drawing.Point(104, 99);
			this.groupArch.Name = "groupArch";
			this.groupArch.Size = new System.Drawing.Size(60, 56);
			this.groupArch.TabIndex = 3;
			this.groupArch.TabStop = false;
			this.groupArch.Text = "Arch";
			this.groupArch.Visible = false;
			// 
			// radioL
			// 
			this.radioL.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioL.Location = new System.Drawing.Point(12, 36);
			this.radioL.Name = "radioL";
			this.radioL.Size = new System.Drawing.Size(28, 16);
			this.radioL.TabIndex = 1;
			this.radioL.Text = "L";
			this.radioL.Click += new System.EventHandler(this.radioL_Click);
			// 
			// radioU
			// 
			this.radioU.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioU.Location = new System.Drawing.Point(12, 16);
			this.radioU.Name = "radioU";
			this.radioU.Size = new System.Drawing.Size(32, 16);
			this.radioU.TabIndex = 0;
			this.radioU.Text = "U";
			this.radioU.Click += new System.EventHandler(this.radioU_Click);
			// 
			// panelSurfaces
			// 
			this.panelSurfaces.Controls.Add(this.butD);
			this.panelSurfaces.Controls.Add(this.butBF);
			this.panelSurfaces.Controls.Add(this.butL);
			this.panelSurfaces.Controls.Add(this.butM);
			this.panelSurfaces.Controls.Add(this.butV);
			this.panelSurfaces.Controls.Add(this.butOI);
			this.panelSurfaces.Location = new System.Drawing.Point(188, 106);
			this.panelSurfaces.Name = "panelSurfaces";
			this.panelSurfaces.Size = new System.Drawing.Size(96, 66);
			this.panelSurfaces.TabIndex = 100;
			this.panelSurfaces.Visible = false;
			// 
			// groupSextant
			// 
			this.groupSextant.Controls.Add(this.radioS6);
			this.groupSextant.Controls.Add(this.radioS5);
			this.groupSextant.Controls.Add(this.radioS4);
			this.groupSextant.Controls.Add(this.radioS2);
			this.groupSextant.Controls.Add(this.radioS3);
			this.groupSextant.Controls.Add(this.radioS1);
			this.groupSextant.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupSextant.Location = new System.Drawing.Point(104, 99);
			this.groupSextant.Name = "groupSextant";
			this.groupSextant.Size = new System.Drawing.Size(156, 56);
			this.groupSextant.TabIndex = 5;
			this.groupSextant.TabStop = false;
			this.groupSextant.Text = "Sextant";
			this.groupSextant.Visible = false;
			// 
			// radioS6
			// 
			this.radioS6.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioS6.Location = new System.Drawing.Point(12, 36);
			this.radioS6.Name = "radioS6";
			this.radioS6.Size = new System.Drawing.Size(36, 16);
			this.radioS6.TabIndex = 5;
			this.radioS6.Text = "6";
			this.radioS6.Click += new System.EventHandler(this.radioS6_Click);
			// 
			// radioS5
			// 
			this.radioS5.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioS5.Location = new System.Drawing.Point(60, 36);
			this.radioS5.Name = "radioS5";
			this.radioS5.Size = new System.Drawing.Size(36, 16);
			this.radioS5.TabIndex = 4;
			this.radioS5.Text = "5";
			this.radioS5.Click += new System.EventHandler(this.radioS5_Click);
			// 
			// radioS4
			// 
			this.radioS4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioS4.Location = new System.Drawing.Point(108, 36);
			this.radioS4.Name = "radioS4";
			this.radioS4.Size = new System.Drawing.Size(36, 16);
			this.radioS4.TabIndex = 1;
			this.radioS4.Text = "4";
			this.radioS4.Click += new System.EventHandler(this.radioS4_Click);
			// 
			// radioS2
			// 
			this.radioS2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioS2.Location = new System.Drawing.Point(60, 16);
			this.radioS2.Name = "radioS2";
			this.radioS2.Size = new System.Drawing.Size(36, 16);
			this.radioS2.TabIndex = 2;
			this.radioS2.Text = "2";
			this.radioS2.Click += new System.EventHandler(this.radioS2_Click);
			// 
			// radioS3
			// 
			this.radioS3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioS3.Location = new System.Drawing.Point(108, 16);
			this.radioS3.Name = "radioS3";
			this.radioS3.Size = new System.Drawing.Size(36, 16);
			this.radioS3.TabIndex = 0;
			this.radioS3.Text = "3";
			this.radioS3.Click += new System.EventHandler(this.radioS3_Click);
			// 
			// radioS1
			// 
			this.radioS1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioS1.Location = new System.Drawing.Point(12, 16);
			this.radioS1.Name = "radioS1";
			this.radioS1.Size = new System.Drawing.Size(36, 16);
			this.radioS1.TabIndex = 0;
			this.radioS1.Text = "1";
			this.radioS1.Click += new System.EventHandler(this.radioS1_Click);
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label9.Location = new System.Drawing.Point(5, 199);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(100, 14);
			this.label9.TabIndex = 45;
			this.label9.Text = "Provider";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelDx
			// 
			this.labelDx.Location = new System.Drawing.Point(5, 242);
			this.labelDx.Name = "labelDx";
			this.labelDx.Size = new System.Drawing.Size(100, 14);
			this.labelDx.TabIndex = 46;
			this.labelDx.Text = "Diagnosis";
			this.labelDx.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel1
			// 
			this.panel1.AllowDrop = true;
			this.panel1.Controls.Add(this.textTimeFinal);
			this.panel1.Controls.Add(this.textTimeStart);
			this.panel1.Controls.Add(this.textTimeEnd);
			this.panel1.Controls.Add(this.textDate);
			this.panel1.Controls.Add(this.butNow);
			this.panel1.Controls.Add(this.panelSurfaces);
			this.panel1.Controls.Add(this.textDateTP);
			this.panel1.Controls.Add(this.label27);
			this.panel1.Controls.Add(this.label26);
			this.panel1.Controls.Add(this.listBoxTeeth);
			this.panel1.Controls.Add(this.textDesc);
			this.panel1.Controls.Add(this.textDateEntry);
			this.panel1.Controls.Add(this.label12);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.labelTooth);
			this.panel1.Controls.Add(this.labelSurfaces);
			this.panel1.Controls.Add(this.labelAmount);
			this.panel1.Controls.Add(this.textSurfaces);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.groupQuadrant);
			this.panel1.Controls.Add(this.textProcFee);
			this.panel1.Controls.Add(this.textTooth);
			this.panel1.Controls.Add(this.labelStartTime);
			this.panel1.Controls.Add(this.labelEndTime);
			this.panel1.Controls.Add(this.labelRange);
			this.panel1.Controls.Add(this.labelDate);
			this.panel1.Controls.Add(this.textProc);
			this.panel1.Controls.Add(this.listBoxTeeth2);
			this.panel1.Controls.Add(this.textRange);
			this.panel1.Controls.Add(this.butChange);
			this.panel1.Controls.Add(this.groupSextant);
			this.panel1.Controls.Add(this.groupArch);
			this.panel1.Controls.Add(this.labelTimeFinal);
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(397, 177);
			this.panel1.TabIndex = 2;
			// 
			// textTimeFinal
			// 
			this.textTimeFinal.Location = new System.Drawing.Point(314, 61);
			this.textTimeFinal.Name = "textTimeFinal";
			this.textTimeFinal.Size = new System.Drawing.Size(55, 20);
			this.textTimeFinal.TabIndex = 104;
			this.textTimeFinal.Visible = false;
			// 
			// textTimeStart
			// 
			this.textTimeStart.Location = new System.Drawing.Point(236, 40);
			this.textTimeStart.Name = "textTimeStart";
			this.textTimeStart.Size = new System.Drawing.Size(55, 20);
			this.textTimeStart.TabIndex = 102;
			this.textTimeStart.TextChanged += new System.EventHandler(this.textTimeStart_TextChanged);
			// 
			// textTimeEnd
			// 
			this.textTimeEnd.Location = new System.Drawing.Point(314, 40);
			this.textTimeEnd.Name = "textTimeEnd";
			this.textTimeEnd.Size = new System.Drawing.Size(55, 20);
			this.textTimeEnd.TabIndex = 102;
			this.textTimeEnd.Visible = false;
			this.textTimeEnd.TextChanged += new System.EventHandler(this.textTimeEnd_TextChanged);
			// 
			// label27
			// 
			this.label27.Location = new System.Drawing.Point(34, 25);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(70, 14);
			this.label27.TabIndex = 98;
			this.label27.Text = "Date TP";
			this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label26
			// 
			this.label26.Location = new System.Drawing.Point(184, 3);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(125, 18);
			this.label26.TabIndex = 97;
			this.label26.Text = "(for security)";
			this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// listBoxTeeth
			// 
			this.listBoxTeeth.AllowDrop = true;
			this.listBoxTeeth.ColumnWidth = 16;
			this.listBoxTeeth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.listBoxTeeth.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16"});
			this.listBoxTeeth.Location = new System.Drawing.Point(106, 101);
			this.listBoxTeeth.MultiColumn = true;
			this.listBoxTeeth.Name = "listBoxTeeth";
			this.listBoxTeeth.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.listBoxTeeth.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listBoxTeeth.Size = new System.Drawing.Size(272, 17);
			this.listBoxTeeth.TabIndex = 1;
			this.listBoxTeeth.Visible = false;
			this.listBoxTeeth.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBoxTeeth_MouseDown);
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(3, 3);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(103, 18);
			this.label12.TabIndex = 96;
			this.label12.Text = "Date Entry";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelStartTime
			// 
			this.labelStartTime.Location = new System.Drawing.Point(181, 44);
			this.labelStartTime.Name = "labelStartTime";
			this.labelStartTime.Size = new System.Drawing.Size(56, 14);
			this.labelStartTime.TabIndex = 0;
			this.labelStartTime.Text = "Time Start";
			this.labelStartTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelEndTime
			// 
			this.labelEndTime.Location = new System.Drawing.Point(259, 44);
			this.labelEndTime.Name = "labelEndTime";
			this.labelEndTime.Size = new System.Drawing.Size(56, 14);
			this.labelEndTime.TabIndex = 0;
			this.labelEndTime.Text = "End";
			this.labelEndTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.labelEndTime.Visible = false;
			// 
			// listBoxTeeth2
			// 
			this.listBoxTeeth2.ColumnWidth = 16;
			this.listBoxTeeth2.Items.AddRange(new object[] {
            "32",
            "31",
            "30",
            "29",
            "28",
            "27",
            "26",
            "25",
            "24",
            "23",
            "22",
            "21",
            "20",
            "19",
            "18",
            "17"});
			this.listBoxTeeth2.Location = new System.Drawing.Point(106, 115);
			this.listBoxTeeth2.MultiColumn = true;
			this.listBoxTeeth2.Name = "listBoxTeeth2";
			this.listBoxTeeth2.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listBoxTeeth2.Size = new System.Drawing.Size(272, 17);
			this.listBoxTeeth2.TabIndex = 2;
			this.listBoxTeeth2.Visible = false;
			this.listBoxTeeth2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBoxTeeth2_MouseDown);
			// 
			// labelTimeFinal
			// 
			this.labelTimeFinal.Location = new System.Drawing.Point(259, 65);
			this.labelTimeFinal.Name = "labelTimeFinal";
			this.labelTimeFinal.Size = new System.Drawing.Size(56, 14);
			this.labelTimeFinal.TabIndex = 103;
			this.labelTimeFinal.Text = "Final";
			this.labelTimeFinal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.labelTimeFinal.Visible = false;
			// 
			// textDrugQty
			// 
			this.textDrugQty.Location = new System.Drawing.Point(123, 149);
			this.textDrugQty.Name = "textDrugQty";
			this.textDrugQty.Size = new System.Drawing.Size(59, 20);
			this.textDrugQty.TabIndex = 174;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(4, 150);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(118, 16);
			this.label10.TabIndex = 173;
			this.label10.Text = "Drug Qty";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(7, 110);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(115, 16);
			this.label5.TabIndex = 170;
			this.label5.Text = "Drug NDC";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDrugNDC
			// 
			this.textDrugNDC.Location = new System.Drawing.Point(123, 108);
			this.textDrugNDC.Name = "textDrugNDC";
			this.textDrugNDC.ReadOnly = true;
			this.textDrugNDC.Size = new System.Drawing.Size(109, 20);
			this.textDrugNDC.TabIndex = 171;
			this.textDrugNDC.Text = "12345678901";
			// 
			// comboDrugUnit
			// 
			this.comboDrugUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboDrugUnit.FormattingEnabled = true;
			this.comboDrugUnit.Location = new System.Drawing.Point(123, 128);
			this.comboDrugUnit.Name = "comboDrugUnit";
			this.comboDrugUnit.Size = new System.Drawing.Size(92, 21);
			this.comboDrugUnit.TabIndex = 169;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(7, 130);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(115, 16);
			this.label1.TabIndex = 168;
			this.label1.Text = "Drug Unit";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textRevCode
			// 
			this.textRevCode.Location = new System.Drawing.Point(123, 88);
			this.textRevCode.MaxLength = 48;
			this.textRevCode.Name = "textRevCode";
			this.textRevCode.Size = new System.Drawing.Size(59, 20);
			this.textRevCode.TabIndex = 112;
			// 
			// label22
			// 
			this.label22.Location = new System.Drawing.Point(7, 90);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(115, 17);
			this.label22.TabIndex = 111;
			this.label22.Text = "Revenue Code";
			this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textUnitQty
			// 
			this.textUnitQty.Location = new System.Drawing.Point(123, 47);
			this.textUnitQty.MaxLength = 15;
			this.textUnitQty.Name = "textUnitQty";
			this.textUnitQty.Size = new System.Drawing.Size(29, 20);
			this.textUnitQty.TabIndex = 110;
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(7, 49);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(115, 17);
			this.label21.TabIndex = 108;
			this.label21.Text = "Unit Quantity";
			this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textCodeMod4
			// 
			this.textCodeMod4.Location = new System.Drawing.Point(210, 27);
			this.textCodeMod4.MaxLength = 2;
			this.textCodeMod4.Name = "textCodeMod4";
			this.textCodeMod4.Size = new System.Drawing.Size(29, 20);
			this.textCodeMod4.TabIndex = 106;
			this.textCodeMod4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textCodeMod3
			// 
			this.textCodeMod3.Location = new System.Drawing.Point(181, 27);
			this.textCodeMod3.MaxLength = 2;
			this.textCodeMod3.Name = "textCodeMod3";
			this.textCodeMod3.Size = new System.Drawing.Size(29, 20);
			this.textCodeMod3.TabIndex = 105;
			this.textCodeMod3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textCodeMod2
			// 
			this.textCodeMod2.Location = new System.Drawing.Point(152, 27);
			this.textCodeMod2.MaxLength = 2;
			this.textCodeMod2.Name = "textCodeMod2";
			this.textCodeMod2.Size = new System.Drawing.Size(29, 20);
			this.textCodeMod2.TabIndex = 104;
			this.textCodeMod2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(7, 29);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(115, 17);
			this.label19.TabIndex = 102;
			this.label19.Text = "Mods";
			this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textCodeMod1
			// 
			this.textCodeMod1.Location = new System.Drawing.Point(123, 27);
			this.textCodeMod1.MaxLength = 2;
			this.textCodeMod1.Name = "textCodeMod1";
			this.textCodeMod1.Size = new System.Drawing.Size(29, 20);
			this.textCodeMod1.TabIndex = 103;
			this.textCodeMod1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// checkIsPrincDiag
			// 
			this.checkIsPrincDiag.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsPrincDiag.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIsPrincDiag.Location = new System.Drawing.Point(347, 28);
			this.checkIsPrincDiag.Name = "checkIsPrincDiag";
			this.checkIsPrincDiag.Size = new System.Drawing.Size(166, 15);
			this.checkIsPrincDiag.TabIndex = 101;
			this.checkIsPrincDiag.Text = "Princ Diag";
			this.checkIsPrincDiag.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelDiagnosticCode1
			// 
			this.labelDiagnosticCode1.Location = new System.Drawing.Point(336, 66);
			this.labelDiagnosticCode1.Name = "labelDiagnosticCode1";
			this.labelDiagnosticCode1.Size = new System.Drawing.Size(164, 16);
			this.labelDiagnosticCode1.TabIndex = 99;
			this.labelDiagnosticCode1.Text = "ICD-10 Diagnosis Code 1";
			this.labelDiagnosticCode1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDiagnosticCode
			// 
			this.textDiagnosticCode.Location = new System.Drawing.Point(501, 64);
			this.textDiagnosticCode.Name = "textDiagnosticCode";
			this.textDiagnosticCode.Size = new System.Drawing.Size(76, 20);
			this.textDiagnosticCode.TabIndex = 100;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(7, 9);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(115, 16);
			this.label8.TabIndex = 97;
			this.label8.Text = "Medical Code";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textMedicalCode
			// 
			this.textMedicalCode.Location = new System.Drawing.Point(123, 6);
			this.textMedicalCode.Name = "textMedicalCode";
			this.textMedicalCode.Size = new System.Drawing.Size(76, 20);
			this.textMedicalCode.TabIndex = 98;
			// 
			// labelClaim
			// 
			this.labelClaim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelClaim.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelClaim.Location = new System.Drawing.Point(111, 652);
			this.labelClaim.Name = "labelClaim";
			this.labelClaim.Size = new System.Drawing.Size(480, 44);
			this.labelClaim.TabIndex = 50;
			this.labelClaim.Text = "This procedure is attached to a claim, so certain fields should not be edited.  Y" +
    "ou should reprint the claim if any significant changes are made.";
			this.labelClaim.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.labelClaim.Visible = false;
			// 
			// comboPlaceService
			// 
			this.comboPlaceService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboPlaceService.Location = new System.Drawing.Point(119, 98);
			this.comboPlaceService.MaxDropDownItems = 30;
			this.comboPlaceService.Name = "comboPlaceService";
			this.comboPlaceService.Size = new System.Drawing.Size(177, 21);
			this.comboPlaceService.TabIndex = 6;
			// 
			// labelPlaceService
			// 
			this.labelPlaceService.Location = new System.Drawing.Point(4, 101);
			this.labelPlaceService.Name = "labelPlaceService";
			this.labelPlaceService.Size = new System.Drawing.Size(114, 16);
			this.labelPlaceService.TabIndex = 53;
			this.labelPlaceService.Text = "Place of Service";
			this.labelPlaceService.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelPriority
			// 
			this.labelPriority.Location = new System.Drawing.Point(32, 263);
			this.labelPriority.Name = "labelPriority";
			this.labelPriority.Size = new System.Drawing.Size(72, 16);
			this.labelPriority.TabIndex = 56;
			this.labelPriority.Text = "Priority";
			this.labelPriority.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelSetComplete
			// 
			this.labelSetComplete.Location = new System.Drawing.Point(724, 23);
			this.labelSetComplete.Name = "labelSetComplete";
			this.labelSetComplete.Size = new System.Drawing.Size(157, 16);
			this.labelSetComplete.TabIndex = 58;
			this.labelSetComplete.Text = "changes date and adds note.";
			// 
			// checkNoBillIns
			// 
			this.checkNoBillIns.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkNoBillIns.Location = new System.Drawing.Point(142, 12);
			this.checkNoBillIns.Name = "checkNoBillIns";
			this.checkNoBillIns.Size = new System.Drawing.Size(152, 18);
			this.checkNoBillIns.TabIndex = 9;
			this.checkNoBillIns.Text = "Do Not Bill to Ins";
			this.checkNoBillIns.ThreeState = true;
			this.checkNoBillIns.Click += new System.EventHandler(this.checkNoBillIns_Click);
			// 
			// labelIncomplete
			// 
			this.labelIncomplete.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelIncomplete.ForeColor = System.Drawing.Color.DarkRed;
			this.labelIncomplete.Location = new System.Drawing.Point(724, 138);
			this.labelIncomplete.Name = "labelIncomplete";
			this.labelIncomplete.Size = new System.Drawing.Size(123, 18);
			this.labelIncomplete.TabIndex = 73;
			this.labelIncomplete.Text = "Incomplete";
			this.labelIncomplete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboClinic
			// 
			this.comboClinic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboClinic.Location = new System.Drawing.Point(106, 217);
			this.comboClinic.MaxDropDownItems = 30;
			this.comboClinic.Name = "comboClinic";
			this.comboClinic.Size = new System.Drawing.Size(177, 21);
			this.comboClinic.TabIndex = 74;
			this.comboClinic.SelectionChangeCommitted += new System.EventHandler(this.comboClinic_SelectionChangeCommitted);
			// 
			// labelClinic
			// 
			this.labelClinic.Location = new System.Drawing.Point(-10, 219);
			this.labelClinic.Name = "labelClinic";
			this.labelClinic.Size = new System.Drawing.Size(114, 16);
			this.labelClinic.TabIndex = 75;
			this.labelClinic.Text = "Clinic";
			this.labelClinic.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(403, 20);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(99, 16);
			this.label14.TabIndex = 77;
			this.label14.Text = "Procedure Status";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(389, 327);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(110, 41);
			this.label15.TabIndex = 79;
			this.label15.Text = "Signature /\r\nInitials";
			this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(429, 138);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(73, 16);
			this.label16.TabIndex = 80;
			this.label16.Text = "User";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboPriority
			// 
			this.comboPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboPriority.Location = new System.Drawing.Point(106, 259);
			this.comboPriority.MaxDropDownItems = 30;
			this.comboPriority.Name = "comboPriority";
			this.comboPriority.Size = new System.Drawing.Size(177, 21);
			this.comboPriority.TabIndex = 98;
			// 
			// comboDx
			// 
			this.comboDx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboDx.Location = new System.Drawing.Point(106, 238);
			this.comboDx.MaxDropDownItems = 30;
			this.comboDx.Name = "comboDx";
			this.comboDx.Size = new System.Drawing.Size(177, 21);
			this.comboDx.TabIndex = 99;
			// 
			// comboProvNum
			// 
			this.comboProvNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboProvNum.Location = new System.Drawing.Point(106, 195);
			this.comboProvNum.MaxDropDownItems = 30;
			this.comboProvNum.Name = "comboProvNum";
			this.comboProvNum.Size = new System.Drawing.Size(158, 21);
			this.comboProvNum.TabIndex = 100;
			this.comboProvNum.SelectionChangeCommitted += new System.EventHandler(this.comboProvNum_SelectionChangeCommitted);
			this.comboProvNum.KeyUp += new System.Windows.Forms.KeyEventHandler(this.comboProvNum_KeyUp);
			// 
			// textUser
			// 
			this.textUser.Location = new System.Drawing.Point(504, 137);
			this.textUser.Name = "textUser";
			this.textUser.ReadOnly = true;
			this.textUser.Size = new System.Drawing.Size(116, 20);
			this.textUser.TabIndex = 101;
			// 
			// comboBillingTypeTwo
			// 
			this.comboBillingTypeTwo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBillingTypeTwo.FormattingEnabled = true;
			this.comboBillingTypeTwo.Location = new System.Drawing.Point(119, 33);
			this.comboBillingTypeTwo.MaxDropDownItems = 30;
			this.comboBillingTypeTwo.Name = "comboBillingTypeTwo";
			this.comboBillingTypeTwo.Size = new System.Drawing.Size(198, 21);
			this.comboBillingTypeTwo.TabIndex = 102;
			// 
			// labelBillingTypeTwo
			// 
			this.labelBillingTypeTwo.Location = new System.Drawing.Point(15, 35);
			this.labelBillingTypeTwo.Name = "labelBillingTypeTwo";
			this.labelBillingTypeTwo.Size = new System.Drawing.Size(102, 16);
			this.labelBillingTypeTwo.TabIndex = 103;
			this.labelBillingTypeTwo.Text = "Billing Type 2";
			this.labelBillingTypeTwo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboBillingTypeOne
			// 
			this.comboBillingTypeOne.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBillingTypeOne.FormattingEnabled = true;
			this.comboBillingTypeOne.Location = new System.Drawing.Point(119, 12);
			this.comboBillingTypeOne.MaxDropDownItems = 30;
			this.comboBillingTypeOne.Name = "comboBillingTypeOne";
			this.comboBillingTypeOne.Size = new System.Drawing.Size(198, 21);
			this.comboBillingTypeOne.TabIndex = 104;
			// 
			// labelBillingTypeOne
			// 
			this.labelBillingTypeOne.Location = new System.Drawing.Point(13, 14);
			this.labelBillingTypeOne.Name = "labelBillingTypeOne";
			this.labelBillingTypeOne.Size = new System.Drawing.Size(104, 16);
			this.labelBillingTypeOne.TabIndex = 105;
			this.labelBillingTypeOne.Text = "Billing Type 1";
			this.labelBillingTypeOne.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelInvalidSig
			// 
			this.labelInvalidSig.BackColor = System.Drawing.SystemColors.Window;
			this.labelInvalidSig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelInvalidSig.Location = new System.Drawing.Point(589, 337);
			this.labelInvalidSig.Name = "labelInvalidSig";
			this.labelInvalidSig.Size = new System.Drawing.Size(196, 59);
			this.labelInvalidSig.TabIndex = 109;
			this.labelInvalidSig.Text = "Invalid Signature -  Note or user has changed since it was signed.";
			this.labelInvalidSig.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// textSite
			// 
			this.textSite.AcceptsReturn = true;
			this.textSite.Location = new System.Drawing.Point(119, 77);
			this.textSite.Name = "textSite";
			this.textSite.ReadOnly = true;
			this.textSite.Size = new System.Drawing.Size(153, 20);
			this.textSite.TabIndex = 111;
			// 
			// labelSite
			// 
			this.labelSite.Location = new System.Drawing.Point(4, 78);
			this.labelSite.Name = "labelSite";
			this.labelSite.Size = new System.Drawing.Size(114, 17);
			this.labelSite.TabIndex = 110;
			this.labelSite.Text = "Site";
			this.labelSite.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkHideGraphics
			// 
			this.checkHideGraphics.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkHideGraphics.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkHideGraphics.Location = new System.Drawing.Point(5, 178);
			this.checkHideGraphics.Name = "checkHideGraphics";
			this.checkHideGraphics.Size = new System.Drawing.Size(114, 18);
			this.checkHideGraphics.TabIndex = 162;
			this.checkHideGraphics.Text = "HideGraphics";
			this.checkHideGraphics.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(2, 14);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(90, 41);
			this.label3.TabIndex = 0;
			this.label3.Text = "Crown, Bridge, Denture, or RPD";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(5, 61);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(84, 16);
			this.label4.TabIndex = 4;
			this.label4.Text = "Original Date";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// listProsth
			// 
			this.listProsth.Location = new System.Drawing.Point(91, 14);
			this.listProsth.Name = "listProsth";
			this.listProsth.Size = new System.Drawing.Size(163, 43);
			this.listProsth.TabIndex = 0;
			// 
			// groupProsth
			// 
			this.groupProsth.Controls.Add(this.checkIsDateProsthEst);
			this.groupProsth.Controls.Add(this.listProsth);
			this.groupProsth.Controls.Add(this.textDateOriginalProsth);
			this.groupProsth.Controls.Add(this.label4);
			this.groupProsth.Controls.Add(this.label3);
			this.groupProsth.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupProsth.Location = new System.Drawing.Point(15, 283);
			this.groupProsth.Name = "groupProsth";
			this.groupProsth.Size = new System.Drawing.Size(269, 80);
			this.groupProsth.TabIndex = 7;
			this.groupProsth.TabStop = false;
			this.groupProsth.Text = "Prosthesis Replacement";
			// 
			// checkIsDateProsthEst
			// 
			this.checkIsDateProsthEst.Location = new System.Drawing.Point(169, 61);
			this.checkIsDateProsthEst.Name = "checkIsDateProsthEst";
			this.checkIsDateProsthEst.Size = new System.Drawing.Size(96, 16);
			this.checkIsDateProsthEst.TabIndex = 181;
			this.checkIsDateProsthEst.Text = "Is Estimated";
			this.checkIsDateProsthEst.UseVisualStyleBackColor = true;
			// 
			// checkTypeCodeA
			// 
			this.checkTypeCodeA.Location = new System.Drawing.Point(10, 16);
			this.checkTypeCodeA.Name = "checkTypeCodeA";
			this.checkTypeCodeA.Size = new System.Drawing.Size(268, 17);
			this.checkTypeCodeA.TabIndex = 0;
			this.checkTypeCodeA.Text = "Not initial placement.  Repair of a prior service.";
			this.checkTypeCodeA.UseVisualStyleBackColor = true;
			// 
			// checkTypeCodeB
			// 
			this.checkTypeCodeB.Location = new System.Drawing.Point(10, 33);
			this.checkTypeCodeB.Name = "checkTypeCodeB";
			this.checkTypeCodeB.Size = new System.Drawing.Size(239, 17);
			this.checkTypeCodeB.TabIndex = 1;
			this.checkTypeCodeB.Text = "Temporary placement or service.";
			this.checkTypeCodeB.UseVisualStyleBackColor = true;
			// 
			// checkTypeCodeC
			// 
			this.checkTypeCodeC.Location = new System.Drawing.Point(10, 50);
			this.checkTypeCodeC.Name = "checkTypeCodeC";
			this.checkTypeCodeC.Size = new System.Drawing.Size(239, 17);
			this.checkTypeCodeC.TabIndex = 2;
			this.checkTypeCodeC.Text = "Correction of TMJ";
			this.checkTypeCodeC.UseVisualStyleBackColor = true;
			// 
			// checkTypeCodeE
			// 
			this.checkTypeCodeE.Location = new System.Drawing.Point(10, 67);
			this.checkTypeCodeE.Name = "checkTypeCodeE";
			this.checkTypeCodeE.Size = new System.Drawing.Size(268, 17);
			this.checkTypeCodeE.TabIndex = 3;
			this.checkTypeCodeE.Text = "Implant, or in conjunction with implants";
			this.checkTypeCodeE.UseVisualStyleBackColor = true;
			// 
			// checkTypeCodeL
			// 
			this.checkTypeCodeL.Location = new System.Drawing.Point(10, 84);
			this.checkTypeCodeL.Name = "checkTypeCodeL";
			this.checkTypeCodeL.Size = new System.Drawing.Size(113, 17);
			this.checkTypeCodeL.TabIndex = 4;
			this.checkTypeCodeL.Text = "Appliance lost";
			this.checkTypeCodeL.UseVisualStyleBackColor = true;
			// 
			// checkTypeCodeX
			// 
			this.checkTypeCodeX.Location = new System.Drawing.Point(10, 118);
			this.checkTypeCodeX.Name = "checkTypeCodeX";
			this.checkTypeCodeX.Size = new System.Drawing.Size(239, 17);
			this.checkTypeCodeX.TabIndex = 5;
			this.checkTypeCodeX.Text = "None of the above are applicable";
			this.checkTypeCodeX.UseVisualStyleBackColor = true;
			// 
			// checkTypeCodeS
			// 
			this.checkTypeCodeS.Location = new System.Drawing.Point(10, 101);
			this.checkTypeCodeS.Name = "checkTypeCodeS";
			this.checkTypeCodeS.Size = new System.Drawing.Size(113, 17);
			this.checkTypeCodeS.TabIndex = 6;
			this.checkTypeCodeS.Text = "Appliance stolen";
			this.checkTypeCodeS.UseVisualStyleBackColor = true;
			// 
			// groupCanadianProcTypeCode
			// 
			this.groupCanadianProcTypeCode.Controls.Add(this.checkTypeCodeS);
			this.groupCanadianProcTypeCode.Controls.Add(this.checkTypeCodeX);
			this.groupCanadianProcTypeCode.Controls.Add(this.checkTypeCodeL);
			this.groupCanadianProcTypeCode.Controls.Add(this.checkTypeCodeE);
			this.groupCanadianProcTypeCode.Controls.Add(this.checkTypeCodeC);
			this.groupCanadianProcTypeCode.Controls.Add(this.checkTypeCodeB);
			this.groupCanadianProcTypeCode.Controls.Add(this.checkTypeCodeA);
			this.groupCanadianProcTypeCode.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupCanadianProcTypeCode.Location = new System.Drawing.Point(18, 16);
			this.groupCanadianProcTypeCode.Name = "groupCanadianProcTypeCode";
			this.groupCanadianProcTypeCode.Size = new System.Drawing.Size(316, 142);
			this.groupCanadianProcTypeCode.TabIndex = 163;
			this.groupCanadianProcTypeCode.TabStop = false;
			this.groupCanadianProcTypeCode.Text = "Procedure Type Code";
			// 
			// labelDPCpost
			// 
			this.labelDPCpost.Location = new System.Drawing.Point(9, 28);
			this.labelDPCpost.Name = "labelDPCpost";
			this.labelDPCpost.Size = new System.Drawing.Size(100, 16);
			this.labelDPCpost.TabIndex = 107;
			this.labelDPCpost.Text = "DPC Post Visit";
			this.labelDPCpost.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboDPCpost
			// 
			this.comboDPCpost.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboDPCpost.DropDownWidth = 177;
			this.comboDPCpost.FormattingEnabled = true;
			this.comboDPCpost.Location = new System.Drawing.Point(111, 27);
			this.comboDPCpost.MaxDropDownItems = 30;
			this.comboDPCpost.Name = "comboDPCpost";
			this.comboDPCpost.Size = new System.Drawing.Size(177, 21);
			this.comboDPCpost.TabIndex = 106;
			// 
			// labelScheduleBy
			// 
			this.labelScheduleBy.Location = new System.Drawing.Point(193, 70);
			this.labelScheduleBy.Name = "labelScheduleBy";
			this.labelScheduleBy.Size = new System.Drawing.Size(148, 16);
			this.labelScheduleBy.TabIndex = 105;
			this.labelScheduleBy.Text = "No Schedule by Date";
			this.labelScheduleBy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.labelScheduleBy.Visible = false;
			// 
			// checkIsRepair
			// 
			this.checkIsRepair.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsRepair.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIsRepair.Location = new System.Drawing.Point(10, 145);
			this.checkIsRepair.Name = "checkIsRepair";
			this.checkIsRepair.Size = new System.Drawing.Size(114, 18);
			this.checkIsRepair.TabIndex = 104;
			this.checkIsRepair.Text = "Repair";
			this.checkIsRepair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkIsEffComm
			// 
			this.checkIsEffComm.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsEffComm.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIsEffComm.Location = new System.Drawing.Point(10, 128);
			this.checkIsEffComm.Name = "checkIsEffComm";
			this.checkIsEffComm.Size = new System.Drawing.Size(114, 18);
			this.checkIsEffComm.TabIndex = 103;
			this.checkIsEffComm.Text = "Effective Comm";
			this.checkIsEffComm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkIsOnCall
			// 
			this.checkIsOnCall.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsOnCall.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIsOnCall.Location = new System.Drawing.Point(10, 111);
			this.checkIsOnCall.Name = "checkIsOnCall";
			this.checkIsOnCall.Size = new System.Drawing.Size(114, 18);
			this.checkIsOnCall.TabIndex = 102;
			this.checkIsOnCall.Text = "On Call";
			this.checkIsOnCall.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboDPC
			// 
			this.comboDPC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboDPC.DropDownWidth = 177;
			this.comboDPC.FormattingEnabled = true;
			this.comboDPC.Location = new System.Drawing.Point(111, 6);
			this.comboDPC.MaxDropDownItems = 30;
			this.comboDPC.Name = "comboDPC";
			this.comboDPC.Size = new System.Drawing.Size(177, 21);
			this.comboDPC.TabIndex = 8;
			this.comboDPC.SelectionChangeCommitted += new System.EventHandler(this.comboDPC_SelectionChangeCommitted);
			// 
			// comboStatus
			// 
			this.comboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboStatus.DropDownWidth = 230;
			this.comboStatus.FormattingEnabled = true;
			this.comboStatus.Location = new System.Drawing.Point(111, 48);
			this.comboStatus.MaxDropDownItems = 30;
			this.comboStatus.Name = "comboStatus";
			this.comboStatus.Size = new System.Drawing.Size(230, 21);
			this.comboStatus.TabIndex = 7;
			this.comboStatus.SelectionChangeCommitted += new System.EventHandler(this.comboStatus_SelectionChangeCommitted);
			// 
			// labelStatus
			// 
			this.labelStatus.Location = new System.Drawing.Point(9, 49);
			this.labelStatus.Name = "labelStatus";
			this.labelStatus.Size = new System.Drawing.Size(100, 16);
			this.labelStatus.TabIndex = 3;
			this.labelStatus.Text = "Status";
			this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelDateStop
			// 
			this.labelDateStop.Location = new System.Drawing.Point(11, 90);
			this.labelDateStop.Name = "labelDateStop";
			this.labelDateStop.Size = new System.Drawing.Size(100, 16);
			this.labelDateStop.TabIndex = 2;
			this.labelDateStop.Text = "Date Stop Clock";
			this.labelDateStop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelDateSched
			// 
			this.labelDateSched.Location = new System.Drawing.Point(10, 70);
			this.labelDateSched.Name = "labelDateSched";
			this.labelDateSched.Size = new System.Drawing.Size(100, 16);
			this.labelDateSched.TabIndex = 1;
			this.labelDateSched.Text = "Scheduled By";
			this.labelDateSched.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelDPC
			// 
			this.labelDPC.Location = new System.Drawing.Point(9, 7);
			this.labelDPC.Name = "labelDPC";
			this.labelDPC.Size = new System.Drawing.Size(100, 16);
			this.labelDPC.TabIndex = 0;
			this.labelDPC.Text = "DPC";
			this.labelDPC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboPrognosis
			// 
			this.comboPrognosis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboPrognosis.Location = new System.Drawing.Point(119, 55);
			this.comboPrognosis.MaxDropDownItems = 30;
			this.comboPrognosis.Name = "comboPrognosis";
			this.comboPrognosis.Size = new System.Drawing.Size(153, 21);
			this.comboPrognosis.TabIndex = 165;
			// 
			// labelPrognosis
			// 
			this.labelPrognosis.Location = new System.Drawing.Point(3, 57);
			this.labelPrognosis.Name = "labelPrognosis";
			this.labelPrognosis.Size = new System.Drawing.Size(114, 17);
			this.labelPrognosis.TabIndex = 166;
			this.labelPrognosis.Text = "Prognosis";
			this.labelPrognosis.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboProcStatus
			// 
			this.comboProcStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboProcStatus.FormattingEnabled = true;
			this.comboProcStatus.Location = new System.Drawing.Point(504, 19);
			this.comboProcStatus.Name = "comboProcStatus";
			this.comboProcStatus.Size = new System.Drawing.Size(133, 21);
			this.comboProcStatus.TabIndex = 167;
			this.comboProcStatus.SelectionChangeCommitted += new System.EventHandler(this.comboProcStatus_SelectionChangeCommitted);
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(418, 80);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(84, 16);
			this.label13.TabIndex = 168;
			this.label13.Text = "Referral";
			this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textReferral
			// 
			this.textReferral.BackColor = System.Drawing.SystemColors.Control;
			this.textReferral.ForeColor = System.Drawing.Color.DarkRed;
			this.textReferral.Location = new System.Drawing.Point(504, 77);
			this.textReferral.Name = "textReferral";
			this.textReferral.Size = new System.Drawing.Size(198, 20);
			this.textReferral.TabIndex = 169;
			this.textReferral.Text = "test";
			// 
			// labelClaimNote
			// 
			this.labelClaimNote.Location = new System.Drawing.Point(0, 364);
			this.labelClaimNote.Name = "labelClaimNote";
			this.labelClaimNote.Size = new System.Drawing.Size(104, 41);
			this.labelClaimNote.TabIndex = 174;
			this.labelClaimNote.Text = "E-claim Note (keep it very short)";
			this.labelClaimNote.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabPageFinancial);
			this.tabControl.Controls.Add(this.tabPageMedical);
			this.tabControl.Controls.Add(this.tabPageMisc);
			this.tabControl.Controls.Add(this.tabPageCanada);
			this.tabControl.Controls.Add(this.tabPageOrion);
			this.tabControl.Location = new System.Drawing.Point(1, 424);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(962, 244);
			this.tabControl.TabIndex = 175;
			// 
			// tabPageFinancial
			// 
			this.tabPageFinancial.Controls.Add(this.gridPay);
			this.tabPageFinancial.Controls.Add(this.gridAdj);
			this.tabPageFinancial.Controls.Add(this.label20);
			this.tabPageFinancial.Controls.Add(this.textDiscount);
			this.tabPageFinancial.Controls.Add(this.butAddEstimate);
			this.tabPageFinancial.Controls.Add(this.checkNoBillIns);
			this.tabPageFinancial.Controls.Add(this.butAddAdjust);
			this.tabPageFinancial.Controls.Add(this.gridIns);
			this.tabPageFinancial.Location = new System.Drawing.Point(4, 22);
			this.tabPageFinancial.Name = "tabPageFinancial";
			this.tabPageFinancial.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageFinancial.Size = new System.Drawing.Size(954, 218);
			this.tabPageFinancial.TabIndex = 0;
			this.tabPageFinancial.Text = "Financial";
			this.tabPageFinancial.UseVisualStyleBackColor = true;
			// 
			// gridPay
			// 
			this.gridPay.HasMultilineHeaders = false;
			this.gridPay.HScrollVisible = false;
			this.gridPay.Location = new System.Drawing.Point(3, 137);
			this.gridPay.Name = "gridPay";
			this.gridPay.ScrollValue = 0;
			this.gridPay.Size = new System.Drawing.Size(449, 72);
			this.gridPay.TabIndex = 117;
			this.gridPay.Title = "Patient Payments";
			this.gridPay.TranslationName = "TableProcPay";
			this.gridPay.WrapText = false;
			this.gridPay.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridPay_CellDoubleClick);
			// 
			// gridAdj
			// 
			this.gridAdj.HasMultilineHeaders = false;
			this.gridAdj.HScrollVisible = false;
			this.gridAdj.Location = new System.Drawing.Point(458, 137);
			this.gridAdj.Name = "gridAdj";
			this.gridAdj.ScrollValue = 0;
			this.gridAdj.Size = new System.Drawing.Size(494, 72);
			this.gridAdj.TabIndex = 116;
			this.gridAdj.Title = "Adjustments";
			this.gridAdj.TranslationName = "TableProcAdj";
			this.gridAdj.WrapText = false;
			this.gridAdj.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridAdj_CellDoubleClick);
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(807, 12);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(75, 16);
			this.label20.TabIndex = 114;
			this.label20.Text = "Discount";
			this.label20.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// gridIns
			// 
			this.gridIns.HasMultilineHeaders = false;
			this.gridIns.HScrollVisible = false;
			this.gridIns.Location = new System.Drawing.Point(3, 32);
			this.gridIns.Name = "gridIns";
			this.gridIns.ScrollValue = 0;
			this.gridIns.Size = new System.Drawing.Size(949, 102);
			this.gridIns.TabIndex = 113;
			this.gridIns.Title = "Insurance Estimates and Payments";
			this.gridIns.TranslationName = "TableProcIns";
			this.gridIns.WrapText = false;
			this.gridIns.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridIns_CellDoubleClick);
			// 
			// tabPageMedical
			// 
			this.tabPageMedical.Controls.Add(this.label11);
			this.tabPageMedical.Controls.Add(this.checkIcdVersion);
			this.tabPageMedical.Controls.Add(this.butNoneProvOrdering);
			this.tabPageMedical.Controls.Add(this.butPickProvOrdering);
			this.tabPageMedical.Controls.Add(this.comboProvNumOrdering);
			this.tabPageMedical.Controls.Add(this.label95);
			this.tabPageMedical.Controls.Add(this.butNoneDiagnosisCode1);
			this.tabPageMedical.Controls.Add(this.butDiagnosisCode1);
			this.tabPageMedical.Controls.Add(this.butNoneDiagnosisCode2);
			this.tabPageMedical.Controls.Add(this.butDiagnosisCode2);
			this.tabPageMedical.Controls.Add(this.butNoneDiagnosisCode4);
			this.tabPageMedical.Controls.Add(this.butDiagnosisCode4);
			this.tabPageMedical.Controls.Add(this.butNoneDiagnosisCode3);
			this.tabPageMedical.Controls.Add(this.butDiagnosisCode3);
			this.tabPageMedical.Controls.Add(this.textDiagnosticCode2);
			this.tabPageMedical.Controls.Add(this.labelDiagnosticCode2);
			this.tabPageMedical.Controls.Add(this.textDiagnosticCode3);
			this.tabPageMedical.Controls.Add(this.labelDiagnosticCode3);
			this.tabPageMedical.Controls.Add(this.textDiagnosticCode4);
			this.tabPageMedical.Controls.Add(this.labelDiagnosticCode4);
			this.tabPageMedical.Controls.Add(this.butNoneSnomedBodySite);
			this.tabPageMedical.Controls.Add(this.butSnomedBodySiteSelect);
			this.tabPageMedical.Controls.Add(this.labelSnomedCtBodySite);
			this.tabPageMedical.Controls.Add(this.textSnomedBodySite);
			this.tabPageMedical.Controls.Add(this.label17);
			this.tabPageMedical.Controls.Add(this.comboUnitType);
			this.tabPageMedical.Controls.Add(this.textDrugQty);
			this.tabPageMedical.Controls.Add(this.label10);
			this.tabPageMedical.Controls.Add(this.label8);
			this.tabPageMedical.Controls.Add(this.label5);
			this.tabPageMedical.Controls.Add(this.textMedicalCode);
			this.tabPageMedical.Controls.Add(this.textDrugNDC);
			this.tabPageMedical.Controls.Add(this.textDiagnosticCode);
			this.tabPageMedical.Controls.Add(this.comboDrugUnit);
			this.tabPageMedical.Controls.Add(this.labelDiagnosticCode1);
			this.tabPageMedical.Controls.Add(this.label1);
			this.tabPageMedical.Controls.Add(this.checkIsPrincDiag);
			this.tabPageMedical.Controls.Add(this.textRevCode);
			this.tabPageMedical.Controls.Add(this.textCodeMod1);
			this.tabPageMedical.Controls.Add(this.label22);
			this.tabPageMedical.Controls.Add(this.label19);
			this.tabPageMedical.Controls.Add(this.textUnitQty);
			this.tabPageMedical.Controls.Add(this.textCodeMod2);
			this.tabPageMedical.Controls.Add(this.label21);
			this.tabPageMedical.Controls.Add(this.textCodeMod3);
			this.tabPageMedical.Controls.Add(this.textCodeMod4);
			this.tabPageMedical.Location = new System.Drawing.Point(4, 22);
			this.tabPageMedical.Name = "tabPageMedical";
			this.tabPageMedical.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageMedical.Size = new System.Drawing.Size(954, 218);
			this.tabPageMedical.TabIndex = 3;
			this.tabPageMedical.Text = "Medical";
			this.tabPageMedical.UseVisualStyleBackColor = true;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(514, 45);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(115, 15);
			this.label11.TabIndex = 288;
			this.label11.Text = "(uncheck for ICD-9)";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// checkIcdVersion
			// 
			this.checkIcdVersion.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIcdVersion.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIcdVersion.Location = new System.Drawing.Point(307, 45);
			this.checkIcdVersion.Name = "checkIcdVersion";
			this.checkIcdVersion.Size = new System.Drawing.Size(206, 15);
			this.checkIcdVersion.TabIndex = 287;
			this.checkIcdVersion.Text = "Use ICD-10 Diagnosis Codes";
			this.checkIcdVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIcdVersion.Click += new System.EventHandler(this.checkIcdVersion_Click);
			// 
			// comboProvNumOrdering
			// 
			this.comboProvNumOrdering.Location = new System.Drawing.Point(501, 152);
			this.comboProvNumOrdering.MaxDropDownItems = 30;
			this.comboProvNumOrdering.Name = "comboProvNumOrdering";
			this.comboProvNumOrdering.Size = new System.Drawing.Size(272, 21);
			this.comboProvNumOrdering.TabIndex = 284;
			this.comboProvNumOrdering.SelectionChangeCommitted += new System.EventHandler(this.comboProvNumOrdering_SelectionChangeCommitted);
			// 
			// label95
			// 
			this.label95.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label95.Location = new System.Drawing.Point(350, 152);
			this.label95.Name = "label95";
			this.label95.Size = new System.Drawing.Size(151, 17);
			this.label95.TabIndex = 283;
			this.label95.Text = "Ordering Provider Override";
			this.label95.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDiagnosticCode2
			// 
			this.textDiagnosticCode2.Location = new System.Drawing.Point(501, 85);
			this.textDiagnosticCode2.Name = "textDiagnosticCode2";
			this.textDiagnosticCode2.Size = new System.Drawing.Size(76, 20);
			this.textDiagnosticCode2.TabIndex = 186;
			// 
			// labelDiagnosticCode2
			// 
			this.labelDiagnosticCode2.Location = new System.Drawing.Point(336, 87);
			this.labelDiagnosticCode2.Name = "labelDiagnosticCode2";
			this.labelDiagnosticCode2.Size = new System.Drawing.Size(164, 16);
			this.labelDiagnosticCode2.TabIndex = 185;
			this.labelDiagnosticCode2.Text = "ICD-10 Diagnosis Code 2";
			this.labelDiagnosticCode2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDiagnosticCode3
			// 
			this.textDiagnosticCode3.Location = new System.Drawing.Point(501, 106);
			this.textDiagnosticCode3.Name = "textDiagnosticCode3";
			this.textDiagnosticCode3.Size = new System.Drawing.Size(76, 20);
			this.textDiagnosticCode3.TabIndex = 184;
			// 
			// labelDiagnosticCode3
			// 
			this.labelDiagnosticCode3.Location = new System.Drawing.Point(336, 108);
			this.labelDiagnosticCode3.Name = "labelDiagnosticCode3";
			this.labelDiagnosticCode3.Size = new System.Drawing.Size(164, 16);
			this.labelDiagnosticCode3.TabIndex = 183;
			this.labelDiagnosticCode3.Text = "ICD-10 Diagnosis Code 3";
			this.labelDiagnosticCode3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDiagnosticCode4
			// 
			this.textDiagnosticCode4.Location = new System.Drawing.Point(501, 126);
			this.textDiagnosticCode4.Name = "textDiagnosticCode4";
			this.textDiagnosticCode4.Size = new System.Drawing.Size(76, 20);
			this.textDiagnosticCode4.TabIndex = 182;
			// 
			// labelDiagnosticCode4
			// 
			this.labelDiagnosticCode4.Location = new System.Drawing.Point(336, 128);
			this.labelDiagnosticCode4.Name = "labelDiagnosticCode4";
			this.labelDiagnosticCode4.Size = new System.Drawing.Size(164, 16);
			this.labelDiagnosticCode4.TabIndex = 181;
			this.labelDiagnosticCode4.Text = "ICD-10 Diagnosis Code 4";
			this.labelDiagnosticCode4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelSnomedCtBodySite
			// 
			this.labelSnomedCtBodySite.Location = new System.Drawing.Point(326, 7);
			this.labelSnomedCtBodySite.Name = "labelSnomedCtBodySite";
			this.labelSnomedCtBodySite.Size = new System.Drawing.Size(172, 20);
			this.labelSnomedCtBodySite.TabIndex = 178;
			this.labelSnomedCtBodySite.Text = "SNOMED CT Body Site";
			this.labelSnomedCtBodySite.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textSnomedBodySite
			// 
			this.textSnomedBodySite.Location = new System.Drawing.Point(501, 7);
			this.textSnomedBodySite.Name = "textSnomedBodySite";
			this.textSnomedBodySite.ReadOnly = true;
			this.textSnomedBodySite.Size = new System.Drawing.Size(272, 20);
			this.textSnomedBodySite.TabIndex = 177;
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(7, 69);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(115, 17);
			this.label17.TabIndex = 176;
			this.label17.Text = "Unit Type";
			this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboUnitType
			// 
			this.comboUnitType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboUnitType.FormattingEnabled = true;
			this.comboUnitType.Location = new System.Drawing.Point(123, 67);
			this.comboUnitType.Name = "comboUnitType";
			this.comboUnitType.Size = new System.Drawing.Size(117, 21);
			this.comboUnitType.TabIndex = 175;
			// 
			// tabPageMisc
			// 
			this.tabPageMisc.Controls.Add(this.textBillingNote);
			this.tabPageMisc.Controls.Add(this.label18);
			this.tabPageMisc.Controls.Add(this.comboBillingTypeOne);
			this.tabPageMisc.Controls.Add(this.labelBillingTypeTwo);
			this.tabPageMisc.Controls.Add(this.comboBillingTypeTwo);
			this.tabPageMisc.Controls.Add(this.labelBillingTypeOne);
			this.tabPageMisc.Controls.Add(this.comboPrognosis);
			this.tabPageMisc.Controls.Add(this.labelPrognosis);
			this.tabPageMisc.Controls.Add(this.textSite);
			this.tabPageMisc.Controls.Add(this.labelSite);
			this.tabPageMisc.Controls.Add(this.butPickSite);
			this.tabPageMisc.Controls.Add(this.comboPlaceService);
			this.tabPageMisc.Controls.Add(this.labelPlaceService);
			this.tabPageMisc.Location = new System.Drawing.Point(4, 22);
			this.tabPageMisc.Name = "tabPageMisc";
			this.tabPageMisc.Size = new System.Drawing.Size(954, 218);
			this.tabPageMisc.TabIndex = 4;
			this.tabPageMisc.Text = "Misc";
			this.tabPageMisc.UseVisualStyleBackColor = true;
			// 
			// textBillingNote
			// 
			this.textBillingNote.Location = new System.Drawing.Point(119, 120);
			this.textBillingNote.Multiline = true;
			this.textBillingNote.Name = "textBillingNote";
			this.textBillingNote.Size = new System.Drawing.Size(259, 83);
			this.textBillingNote.TabIndex = 168;
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(6, 122);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(111, 14);
			this.label18.TabIndex = 167;
			this.label18.Text = "Billing Note";
			this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tabPageCanada
			// 
			this.tabPageCanada.Controls.Add(this.labelCanadaLabFee2);
			this.tabPageCanada.Controls.Add(this.labelCanadaLabFee1);
			this.tabPageCanada.Controls.Add(this.groupCanadianProcTypeCode);
			this.tabPageCanada.Controls.Add(this.textCanadaLabFee2);
			this.tabPageCanada.Controls.Add(this.textCanadaLabFee1);
			this.tabPageCanada.Location = new System.Drawing.Point(4, 22);
			this.tabPageCanada.Name = "tabPageCanada";
			this.tabPageCanada.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageCanada.Size = new System.Drawing.Size(954, 218);
			this.tabPageCanada.TabIndex = 1;
			this.tabPageCanada.Text = "Canada";
			this.tabPageCanada.UseVisualStyleBackColor = true;
			// 
			// labelCanadaLabFee2
			// 
			this.labelCanadaLabFee2.Location = new System.Drawing.Point(340, 37);
			this.labelCanadaLabFee2.Name = "labelCanadaLabFee2";
			this.labelCanadaLabFee2.Size = new System.Drawing.Size(75, 20);
			this.labelCanadaLabFee2.TabIndex = 167;
			this.labelCanadaLabFee2.Text = "Lab Fee 2";
			this.labelCanadaLabFee2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelCanadaLabFee1
			// 
			this.labelCanadaLabFee1.Location = new System.Drawing.Point(340, 16);
			this.labelCanadaLabFee1.Name = "labelCanadaLabFee1";
			this.labelCanadaLabFee1.Size = new System.Drawing.Size(75, 20);
			this.labelCanadaLabFee1.TabIndex = 166;
			this.labelCanadaLabFee1.Text = "Lab Fee 1";
			this.labelCanadaLabFee1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tabPageOrion
			// 
			this.tabPageOrion.Controls.Add(this.labelDPCpost);
			this.tabPageOrion.Controls.Add(this.comboDPCpost);
			this.tabPageOrion.Controls.Add(this.labelDPC);
			this.tabPageOrion.Controls.Add(this.labelScheduleBy);
			this.tabPageOrion.Controls.Add(this.labelDateSched);
			this.tabPageOrion.Controls.Add(this.checkIsRepair);
			this.tabPageOrion.Controls.Add(this.labelDateStop);
			this.tabPageOrion.Controls.Add(this.checkIsEffComm);
			this.tabPageOrion.Controls.Add(this.labelStatus);
			this.tabPageOrion.Controls.Add(this.checkIsOnCall);
			this.tabPageOrion.Controls.Add(this.comboStatus);
			this.tabPageOrion.Controls.Add(this.comboDPC);
			this.tabPageOrion.Controls.Add(this.textDateStop);
			this.tabPageOrion.Controls.Add(this.textDateScheduled);
			this.tabPageOrion.Location = new System.Drawing.Point(4, 22);
			this.tabPageOrion.Name = "tabPageOrion";
			this.tabPageOrion.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageOrion.Size = new System.Drawing.Size(954, 218);
			this.tabPageOrion.TabIndex = 2;
			this.tabPageOrion.Text = "Orion";
			this.tabPageOrion.UseVisualStyleBackColor = true;
			// 
			// labelLocked
			// 
			this.labelLocked.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelLocked.ForeColor = System.Drawing.Color.DarkRed;
			this.labelLocked.Location = new System.Drawing.Point(834, 115);
			this.labelLocked.Name = "labelLocked";
			this.labelLocked.Size = new System.Drawing.Size(123, 18);
			this.labelLocked.TabIndex = 176;
			this.labelLocked.Text = "Locked";
			this.labelLocked.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			this.labelLocked.Visible = false;
			// 
			// butSearch
			// 
			this.butSearch.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSearch.Autosize = true;
			this.butSearch.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSearch.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSearch.CornerRadius = 4F;
			this.butSearch.Location = new System.Drawing.Point(443, 232);
			this.butSearch.Name = "butSearch";
			this.butSearch.Size = new System.Drawing.Size(59, 24);
			this.butSearch.TabIndex = 180;
			this.butSearch.Text = "Search";
			this.butSearch.Click += new System.EventHandler(this.butSearch_Click);
			// 
			// butLock
			// 
			this.butLock.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butLock.Autosize = true;
			this.butLock.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLock.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLock.CornerRadius = 4F;
			this.butLock.Location = new System.Drawing.Point(874, 91);
			this.butLock.Name = "butLock";
			this.butLock.Size = new System.Drawing.Size(80, 22);
			this.butLock.TabIndex = 178;
			this.butLock.Text = "Lock";
			this.butLock.Click += new System.EventHandler(this.butLock_Click);
			// 
			// butInvalidate
			// 
			this.butInvalidate.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butInvalidate.Autosize = true;
			this.butInvalidate.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butInvalidate.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butInvalidate.CornerRadius = 4F;
			this.butInvalidate.Location = new System.Drawing.Point(879, 77);
			this.butInvalidate.Name = "butInvalidate";
			this.butInvalidate.Size = new System.Drawing.Size(80, 22);
			this.butInvalidate.TabIndex = 179;
			this.butInvalidate.Text = "Invalidate";
			this.butInvalidate.Visible = false;
			this.butInvalidate.Click += new System.EventHandler(this.butInvalidate_Click);
			// 
			// butAppend
			// 
			this.butAppend.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAppend.Autosize = true;
			this.butAppend.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAppend.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAppend.CornerRadius = 4F;
			this.butAppend.Location = new System.Drawing.Point(874, 136);
			this.butAppend.Name = "butAppend";
			this.butAppend.Size = new System.Drawing.Size(80, 22);
			this.butAppend.TabIndex = 177;
			this.butAppend.Text = "Append";
			this.butAppend.Visible = false;
			this.butAppend.Click += new System.EventHandler(this.butAppend_Click);
			// 
			// textDiscount
			// 
			this.textDiscount.Location = new System.Drawing.Point(883, 9);
			this.textDiscount.MaxVal = 100000000D;
			this.textDiscount.MinVal = -100000000D;
			this.textDiscount.Name = "textDiscount";
			this.textDiscount.Size = new System.Drawing.Size(68, 20);
			this.textDiscount.TabIndex = 115;
			// 
			// butAddEstimate
			// 
			this.butAddEstimate.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAddEstimate.Autosize = true;
			this.butAddEstimate.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddEstimate.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddEstimate.CornerRadius = 4F;
			this.butAddEstimate.Image = global::OpenDental.Properties.Resources.Add;
			this.butAddEstimate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddEstimate.Location = new System.Drawing.Point(3, 6);
			this.butAddEstimate.Name = "butAddEstimate";
			this.butAddEstimate.Size = new System.Drawing.Size(111, 24);
			this.butAddEstimate.TabIndex = 60;
			this.butAddEstimate.Text = "Add Estimate";
			this.butAddEstimate.Click += new System.EventHandler(this.butAddEstimate_Click);
			// 
			// butAddAdjust
			// 
			this.butAddAdjust.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAddAdjust.Autosize = true;
			this.butAddAdjust.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddAdjust.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddAdjust.CornerRadius = 4F;
			this.butAddAdjust.Image = global::OpenDental.Properties.Resources.Add;
			this.butAddAdjust.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddAdjust.Location = new System.Drawing.Point(458, 6);
			this.butAddAdjust.Name = "butAddAdjust";
			this.butAddAdjust.Size = new System.Drawing.Size(126, 24);
			this.butAddAdjust.TabIndex = 72;
			this.butAddAdjust.Text = "Add Adjustment";
			this.butAddAdjust.Click += new System.EventHandler(this.butAddAdjust_Click);
			// 
			// butNoneProvOrdering
			// 
			this.butNoneProvOrdering.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butNoneProvOrdering.Autosize = true;
			this.butNoneProvOrdering.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNoneProvOrdering.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNoneProvOrdering.CornerRadius = 4F;
			this.butNoneProvOrdering.Location = new System.Drawing.Point(801, 151);
			this.butNoneProvOrdering.Name = "butNoneProvOrdering";
			this.butNoneProvOrdering.Size = new System.Drawing.Size(51, 22);
			this.butNoneProvOrdering.TabIndex = 286;
			this.butNoneProvOrdering.Text = "None";
			this.butNoneProvOrdering.Click += new System.EventHandler(this.butNoneProvOrdering_Click);
			// 
			// butPickProvOrdering
			// 
			this.butPickProvOrdering.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPickProvOrdering.Autosize = true;
			this.butPickProvOrdering.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPickProvOrdering.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPickProvOrdering.CornerRadius = 4F;
			this.butPickProvOrdering.Location = new System.Drawing.Point(776, 151);
			this.butPickProvOrdering.Name = "butPickProvOrdering";
			this.butPickProvOrdering.Size = new System.Drawing.Size(22, 22);
			this.butPickProvOrdering.TabIndex = 285;
			this.butPickProvOrdering.Text = "...";
			this.butPickProvOrdering.Click += new System.EventHandler(this.butPickProvOrdering_Click);
			// 
			// butNoneDiagnosisCode1
			// 
			this.butNoneDiagnosisCode1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butNoneDiagnosisCode1.Autosize = true;
			this.butNoneDiagnosisCode1.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNoneDiagnosisCode1.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNoneDiagnosisCode1.CornerRadius = 4F;
			this.butNoneDiagnosisCode1.Location = new System.Drawing.Point(605, 61);
			this.butNoneDiagnosisCode1.Name = "butNoneDiagnosisCode1";
			this.butNoneDiagnosisCode1.Size = new System.Drawing.Size(51, 22);
			this.butNoneDiagnosisCode1.TabIndex = 194;
			this.butNoneDiagnosisCode1.Text = "None";
			this.butNoneDiagnosisCode1.Click += new System.EventHandler(this.butNoneDiagnosisCode1_Click);
			// 
			// butDiagnosisCode1
			// 
			this.butDiagnosisCode1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDiagnosisCode1.Autosize = true;
			this.butDiagnosisCode1.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDiagnosisCode1.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDiagnosisCode1.CornerRadius = 4F;
			this.butDiagnosisCode1.Location = new System.Drawing.Point(580, 61);
			this.butDiagnosisCode1.Name = "butDiagnosisCode1";
			this.butDiagnosisCode1.Size = new System.Drawing.Size(22, 22);
			this.butDiagnosisCode1.TabIndex = 193;
			this.butDiagnosisCode1.Text = "...";
			this.butDiagnosisCode1.Click += new System.EventHandler(this.butDiagnosisCode1_Click);
			// 
			// butNoneDiagnosisCode2
			// 
			this.butNoneDiagnosisCode2.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butNoneDiagnosisCode2.Autosize = true;
			this.butNoneDiagnosisCode2.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNoneDiagnosisCode2.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNoneDiagnosisCode2.CornerRadius = 4F;
			this.butNoneDiagnosisCode2.Location = new System.Drawing.Point(605, 83);
			this.butNoneDiagnosisCode2.Name = "butNoneDiagnosisCode2";
			this.butNoneDiagnosisCode2.Size = new System.Drawing.Size(51, 22);
			this.butNoneDiagnosisCode2.TabIndex = 192;
			this.butNoneDiagnosisCode2.Text = "None";
			this.butNoneDiagnosisCode2.Click += new System.EventHandler(this.butNoneDiagnosisCode2_Click);
			// 
			// butDiagnosisCode2
			// 
			this.butDiagnosisCode2.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDiagnosisCode2.Autosize = true;
			this.butDiagnosisCode2.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDiagnosisCode2.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDiagnosisCode2.CornerRadius = 4F;
			this.butDiagnosisCode2.Location = new System.Drawing.Point(580, 83);
			this.butDiagnosisCode2.Name = "butDiagnosisCode2";
			this.butDiagnosisCode2.Size = new System.Drawing.Size(22, 22);
			this.butDiagnosisCode2.TabIndex = 191;
			this.butDiagnosisCode2.Text = "...";
			this.butDiagnosisCode2.Click += new System.EventHandler(this.butDiagnosisCode2_Click);
			// 
			// butNoneDiagnosisCode4
			// 
			this.butNoneDiagnosisCode4.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butNoneDiagnosisCode4.Autosize = true;
			this.butNoneDiagnosisCode4.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNoneDiagnosisCode4.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNoneDiagnosisCode4.CornerRadius = 4F;
			this.butNoneDiagnosisCode4.Location = new System.Drawing.Point(605, 127);
			this.butNoneDiagnosisCode4.Name = "butNoneDiagnosisCode4";
			this.butNoneDiagnosisCode4.Size = new System.Drawing.Size(51, 22);
			this.butNoneDiagnosisCode4.TabIndex = 190;
			this.butNoneDiagnosisCode4.Text = "None";
			this.butNoneDiagnosisCode4.Click += new System.EventHandler(this.butNoneDiagnosisCode4_Click);
			// 
			// butDiagnosisCode4
			// 
			this.butDiagnosisCode4.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDiagnosisCode4.Autosize = true;
			this.butDiagnosisCode4.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDiagnosisCode4.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDiagnosisCode4.CornerRadius = 4F;
			this.butDiagnosisCode4.Location = new System.Drawing.Point(580, 127);
			this.butDiagnosisCode4.Name = "butDiagnosisCode4";
			this.butDiagnosisCode4.Size = new System.Drawing.Size(22, 22);
			this.butDiagnosisCode4.TabIndex = 189;
			this.butDiagnosisCode4.Text = "...";
			this.butDiagnosisCode4.Click += new System.EventHandler(this.butDiagnosisCode4_Click);
			// 
			// butNoneDiagnosisCode3
			// 
			this.butNoneDiagnosisCode3.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butNoneDiagnosisCode3.Autosize = true;
			this.butNoneDiagnosisCode3.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNoneDiagnosisCode3.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNoneDiagnosisCode3.CornerRadius = 4F;
			this.butNoneDiagnosisCode3.Location = new System.Drawing.Point(605, 105);
			this.butNoneDiagnosisCode3.Name = "butNoneDiagnosisCode3";
			this.butNoneDiagnosisCode3.Size = new System.Drawing.Size(51, 22);
			this.butNoneDiagnosisCode3.TabIndex = 188;
			this.butNoneDiagnosisCode3.Text = "None";
			this.butNoneDiagnosisCode3.Click += new System.EventHandler(this.butNoneDiagnosisCode3_Click);
			// 
			// butDiagnosisCode3
			// 
			this.butDiagnosisCode3.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDiagnosisCode3.Autosize = true;
			this.butDiagnosisCode3.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDiagnosisCode3.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDiagnosisCode3.CornerRadius = 4F;
			this.butDiagnosisCode3.Location = new System.Drawing.Point(580, 105);
			this.butDiagnosisCode3.Name = "butDiagnosisCode3";
			this.butDiagnosisCode3.Size = new System.Drawing.Size(22, 22);
			this.butDiagnosisCode3.TabIndex = 187;
			this.butDiagnosisCode3.Text = "...";
			this.butDiagnosisCode3.Click += new System.EventHandler(this.butDiagnosisCode3_Click);
			// 
			// butNoneSnomedBodySite
			// 
			this.butNoneSnomedBodySite.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butNoneSnomedBodySite.Autosize = true;
			this.butNoneSnomedBodySite.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNoneSnomedBodySite.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNoneSnomedBodySite.CornerRadius = 4F;
			this.butNoneSnomedBodySite.Location = new System.Drawing.Point(801, 6);
			this.butNoneSnomedBodySite.Name = "butNoneSnomedBodySite";
			this.butNoneSnomedBodySite.Size = new System.Drawing.Size(51, 22);
			this.butNoneSnomedBodySite.TabIndex = 180;
			this.butNoneSnomedBodySite.Text = "None";
			this.butNoneSnomedBodySite.Click += new System.EventHandler(this.butNoneSnomedBodySite_Click);
			// 
			// butSnomedBodySiteSelect
			// 
			this.butSnomedBodySiteSelect.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSnomedBodySiteSelect.Autosize = true;
			this.butSnomedBodySiteSelect.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSnomedBodySiteSelect.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSnomedBodySiteSelect.CornerRadius = 4F;
			this.butSnomedBodySiteSelect.Location = new System.Drawing.Point(776, 6);
			this.butSnomedBodySiteSelect.Name = "butSnomedBodySiteSelect";
			this.butSnomedBodySiteSelect.Size = new System.Drawing.Size(22, 22);
			this.butSnomedBodySiteSelect.TabIndex = 179;
			this.butSnomedBodySiteSelect.Text = "...";
			this.butSnomedBodySiteSelect.Click += new System.EventHandler(this.butSnomedBodySiteSelect_Click);
			// 
			// butPickSite
			// 
			this.butPickSite.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPickSite.Autosize = true;
			this.butPickSite.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPickSite.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPickSite.CornerRadius = 2F;
			this.butPickSite.Location = new System.Drawing.Point(273, 76);
			this.butPickSite.Name = "butPickSite";
			this.butPickSite.Size = new System.Drawing.Size(19, 21);
			this.butPickSite.TabIndex = 112;
			this.butPickSite.TabStop = false;
			this.butPickSite.Text = "...";
			this.butPickSite.Click += new System.EventHandler(this.butPickSite_Click);
			// 
			// textCanadaLabFee2
			// 
			this.textCanadaLabFee2.Location = new System.Drawing.Point(421, 37);
			this.textCanadaLabFee2.MaxVal = 100000000D;
			this.textCanadaLabFee2.MinVal = -100000000D;
			this.textCanadaLabFee2.Name = "textCanadaLabFee2";
			this.textCanadaLabFee2.Size = new System.Drawing.Size(68, 20);
			this.textCanadaLabFee2.TabIndex = 165;
			// 
			// textCanadaLabFee1
			// 
			this.textCanadaLabFee1.Location = new System.Drawing.Point(421, 16);
			this.textCanadaLabFee1.MaxVal = 100000000D;
			this.textCanadaLabFee1.MinVal = -100000000D;
			this.textCanadaLabFee1.Name = "textCanadaLabFee1";
			this.textCanadaLabFee1.Size = new System.Drawing.Size(68, 20);
			this.textCanadaLabFee1.TabIndex = 164;
			// 
			// textDateStop
			// 
			this.textDateStop.Location = new System.Drawing.Point(111, 89);
			this.textDateStop.Name = "textDateStop";
			this.textDateStop.Size = new System.Drawing.Size(76, 20);
			this.textDateStop.TabIndex = 10;
			// 
			// textDateScheduled
			// 
			this.textDateScheduled.Location = new System.Drawing.Point(111, 69);
			this.textDateScheduled.Name = "textDateScheduled";
			this.textDateScheduled.ReadOnly = true;
			this.textDateScheduled.Size = new System.Drawing.Size(76, 20);
			this.textDateScheduled.TabIndex = 9;
			// 
			// textClaimNote
			// 
			this.textClaimNote.AcceptsTab = true;
			this.textClaimNote.DetectUrls = false;
			this.textClaimNote.Location = new System.Drawing.Point(106, 364);
			this.textClaimNote.MaxLength = 80;
			this.textClaimNote.Name = "textClaimNote";
			this.textClaimNote.QuickPasteType = OpenDentBusiness.QuickPasteType.Procedure;
			this.textClaimNote.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textClaimNote.Size = new System.Drawing.Size(277, 43);
			this.textClaimNote.TabIndex = 173;
			this.textClaimNote.Text = "";
			// 
			// butReferral
			// 
			this.butReferral.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butReferral.Autosize = false;
			this.butReferral.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butReferral.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butReferral.CornerRadius = 2F;
			this.butReferral.Location = new System.Drawing.Point(707, 77);
			this.butReferral.Name = "butReferral";
			this.butReferral.Size = new System.Drawing.Size(18, 21);
			this.butReferral.TabIndex = 170;
			this.butReferral.Text = "...";
			this.butReferral.Click += new System.EventHandler(this.butReferral_Click);
			// 
			// butPickProv
			// 
			this.butPickProv.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPickProv.Autosize = false;
			this.butPickProv.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPickProv.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPickProv.CornerRadius = 2F;
			this.butPickProv.Location = new System.Drawing.Point(265, 195);
			this.butPickProv.Name = "butPickProv";
			this.butPickProv.Size = new System.Drawing.Size(18, 21);
			this.butPickProv.TabIndex = 161;
			this.butPickProv.Text = "...";
			this.butPickProv.Click += new System.EventHandler(this.butPickProv_Click);
			// 
			// butTopazSign
			// 
			this.butTopazSign.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butTopazSign.Autosize = true;
			this.butTopazSign.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butTopazSign.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butTopazSign.CornerRadius = 4F;
			this.butTopazSign.Location = new System.Drawing.Point(873, 356);
			this.butTopazSign.Name = "butTopazSign";
			this.butTopazSign.Size = new System.Drawing.Size(81, 24);
			this.butTopazSign.TabIndex = 108;
			this.butTopazSign.Text = "Sign Topaz";
			this.butTopazSign.UseVisualStyleBackColor = true;
			this.butTopazSign.Click += new System.EventHandler(this.butTopazSign_Click);
			// 
			// buttonUseAutoNote
			// 
			this.buttonUseAutoNote.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.buttonUseAutoNote.Autosize = true;
			this.buttonUseAutoNote.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buttonUseAutoNote.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buttonUseAutoNote.CornerRadius = 4F;
			this.buttonUseAutoNote.Location = new System.Drawing.Point(622, 136);
			this.buttonUseAutoNote.Name = "buttonUseAutoNote";
			this.buttonUseAutoNote.Size = new System.Drawing.Size(80, 22);
			this.buttonUseAutoNote.TabIndex = 106;
			this.buttonUseAutoNote.Text = "Auto Note";
			this.buttonUseAutoNote.Click += new System.EventHandler(this.buttonUseAutoNote_Click);
			// 
			// sigBox
			// 
			this.sigBox.Location = new System.Drawing.Point(505, 325);
			this.sigBox.Name = "sigBox";
			this.sigBox.Size = new System.Drawing.Size(362, 79);
			this.sigBox.TabIndex = 86;
			this.sigBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.sigBox_MouseUp);
			// 
			// butClearSig
			// 
			this.butClearSig.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClearSig.Autosize = true;
			this.butClearSig.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClearSig.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClearSig.CornerRadius = 4F;
			this.butClearSig.Location = new System.Drawing.Point(873, 325);
			this.butClearSig.Name = "butClearSig";
			this.butClearSig.Size = new System.Drawing.Size(81, 24);
			this.butClearSig.TabIndex = 85;
			this.butClearSig.Text = "Clear Sig";
			this.butClearSig.Click += new System.EventHandler(this.butClearSig_Click);
			// 
			// textDateOriginalProsth
			// 
			this.textDateOriginalProsth.Location = new System.Drawing.Point(91, 58);
			this.textDateOriginalProsth.Name = "textDateOriginalProsth";
			this.textDateOriginalProsth.Size = new System.Drawing.Size(73, 20);
			this.textDateOriginalProsth.TabIndex = 1;
			// 
			// textNotes
			// 
			this.textNotes.AcceptsTab = true;
			this.textNotes.BackColor = System.Drawing.SystemColors.Window;
			this.textNotes.DetectUrls = false;
			this.textNotes.Location = new System.Drawing.Point(504, 157);
			this.textNotes.Name = "textNotes";
			this.textNotes.QuickPasteType = OpenDentBusiness.QuickPasteType.Procedure;
			this.textNotes.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textNotes.Size = new System.Drawing.Size(450, 164);
			this.textNotes.TabIndex = 1;
			this.textNotes.Text = "";
			this.textNotes.TextChanged += new System.EventHandler(this.textNotes_TextChanged);
			// 
			// butSetComplete
			// 
			this.butSetComplete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSetComplete.Autosize = true;
			this.butSetComplete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSetComplete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSetComplete.CornerRadius = 4F;
			this.butSetComplete.Location = new System.Drawing.Point(643, 19);
			this.butSetComplete.Name = "butSetComplete";
			this.butSetComplete.Size = new System.Drawing.Size(79, 22);
			this.butSetComplete.TabIndex = 54;
			this.butSetComplete.Text = "Set Complete";
			this.butSetComplete.Click += new System.EventHandler(this.butSetComplete_Click);
			// 
			// butEditAnyway
			// 
			this.butEditAnyway.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butEditAnyway.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butEditAnyway.Autosize = true;
			this.butEditAnyway.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEditAnyway.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEditAnyway.CornerRadius = 4F;
			this.butEditAnyway.Location = new System.Drawing.Point(594, 671);
			this.butEditAnyway.Name = "butEditAnyway";
			this.butEditAnyway.Size = new System.Drawing.Size(104, 24);
			this.butEditAnyway.TabIndex = 51;
			this.butEditAnyway.Text = "&Edit Anyway";
			this.butEditAnyway.Visible = false;
			this.butEditAnyway.Click += new System.EventHandler(this.butEditAnyway_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(2, 671);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(83, 24);
			this.butDelete.TabIndex = 8;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(870, 671);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(76, 24);
			this.butCancel.TabIndex = 13;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(779, 671);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(76, 24);
			this.butOK.TabIndex = 12;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(106, 40);
			this.textDate.Name = "textDate";
			this.textDate.Size = new System.Drawing.Size(76, 20);
			this.textDate.TabIndex = 102;
			// 
			// butNow
			// 
			this.butNow.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butNow.Autosize = false;
			this.butNow.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNow.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNow.CornerRadius = 4F;
			this.butNow.Location = new System.Drawing.Point(369, 40);
			this.butNow.Name = "butNow";
			this.butNow.Size = new System.Drawing.Size(27, 20);
			this.butNow.TabIndex = 101;
			this.butNow.Text = "Now";
			this.butNow.UseVisualStyleBackColor = true;
			this.butNow.Visible = false;
			this.butNow.Click += new System.EventHandler(this.butNow_Click);
			// 
			// butD
			// 
			this.butD.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butD.Autosize = true;
			this.butD.BackColor = System.Drawing.SystemColors.Control;
			this.butD.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butD.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butD.CornerRadius = 4F;
			this.butD.Location = new System.Drawing.Point(61, 23);
			this.butD.Name = "butD";
			this.butD.Size = new System.Drawing.Size(24, 20);
			this.butD.TabIndex = 27;
			this.butD.Text = "D";
			this.butD.UseVisualStyleBackColor = false;
			this.butD.Click += new System.EventHandler(this.butD_Click);
			// 
			// butBF
			// 
			this.butBF.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butBF.Autosize = true;
			this.butBF.BackColor = System.Drawing.SystemColors.Control;
			this.butBF.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBF.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBF.CornerRadius = 4F;
			this.butBF.Location = new System.Drawing.Point(22, 3);
			this.butBF.Name = "butBF";
			this.butBF.Size = new System.Drawing.Size(28, 20);
			this.butBF.TabIndex = 28;
			this.butBF.Text = "B/F";
			this.butBF.UseVisualStyleBackColor = false;
			this.butBF.Click += new System.EventHandler(this.butBF_Click);
			// 
			// butL
			// 
			this.butL.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butL.Autosize = true;
			this.butL.BackColor = System.Drawing.SystemColors.Control;
			this.butL.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butL.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butL.CornerRadius = 4F;
			this.butL.Location = new System.Drawing.Point(32, 43);
			this.butL.Name = "butL";
			this.butL.Size = new System.Drawing.Size(24, 20);
			this.butL.TabIndex = 29;
			this.butL.Text = "L";
			this.butL.UseVisualStyleBackColor = false;
			this.butL.Click += new System.EventHandler(this.butL_Click);
			// 
			// butM
			// 
			this.butM.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butM.Autosize = true;
			this.butM.BackColor = System.Drawing.SystemColors.Control;
			this.butM.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butM.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butM.CornerRadius = 4F;
			this.butM.Location = new System.Drawing.Point(3, 23);
			this.butM.Name = "butM";
			this.butM.Size = new System.Drawing.Size(24, 20);
			this.butM.TabIndex = 25;
			this.butM.Text = "M";
			this.butM.UseVisualStyleBackColor = false;
			this.butM.Click += new System.EventHandler(this.butM_Click);
			// 
			// butV
			// 
			this.butV.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butV.Autosize = true;
			this.butV.BackColor = System.Drawing.SystemColors.Control;
			this.butV.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butV.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butV.CornerRadius = 4F;
			this.butV.Location = new System.Drawing.Point(50, 3);
			this.butV.Name = "butV";
			this.butV.Size = new System.Drawing.Size(17, 20);
			this.butV.TabIndex = 30;
			this.butV.Text = "V";
			this.butV.UseVisualStyleBackColor = false;
			this.butV.Click += new System.EventHandler(this.butV_Click);
			// 
			// butOI
			// 
			this.butOI.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOI.Autosize = true;
			this.butOI.BackColor = System.Drawing.SystemColors.Control;
			this.butOI.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOI.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOI.CornerRadius = 4F;
			this.butOI.Location = new System.Drawing.Point(27, 23);
			this.butOI.Name = "butOI";
			this.butOI.Size = new System.Drawing.Size(34, 20);
			this.butOI.TabIndex = 26;
			this.butOI.Text = "O/I";
			this.butOI.UseVisualStyleBackColor = false;
			this.butOI.Click += new System.EventHandler(this.butOI_Click);
			// 
			// textDateTP
			// 
			this.textDateTP.Location = new System.Drawing.Point(106, 21);
			this.textDateTP.Name = "textDateTP";
			this.textDateTP.Size = new System.Drawing.Size(76, 20);
			this.textDateTP.TabIndex = 99;
			// 
			// textDateEntry
			// 
			this.textDateEntry.Location = new System.Drawing.Point(106, 1);
			this.textDateEntry.Name = "textDateEntry";
			this.textDateEntry.ReadOnly = true;
			this.textDateEntry.Size = new System.Drawing.Size(76, 20);
			this.textDateEntry.TabIndex = 95;
			// 
			// textProcFee
			// 
			this.textProcFee.Location = new System.Drawing.Point(106, 155);
			this.textProcFee.MaxVal = 100000000D;
			this.textProcFee.MinVal = -100000000D;
			this.textProcFee.Name = "textProcFee";
			this.textProcFee.Size = new System.Drawing.Size(68, 20);
			this.textProcFee.TabIndex = 6;
			this.textProcFee.Validating += new System.ComponentModel.CancelEventHandler(this.textProcFee_Validating);
			// 
			// butChange
			// 
			this.butChange.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butChange.Autosize = true;
			this.butChange.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butChange.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butChange.CornerRadius = 4F;
			this.butChange.Location = new System.Drawing.Point(184, 61);
			this.butChange.Name = "butChange";
			this.butChange.Size = new System.Drawing.Size(74, 20);
			this.butChange.TabIndex = 37;
			this.butChange.Text = "C&hange";
			this.butChange.Click += new System.EventHandler(this.butChange_Click);
			// 
			// FormProcEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(962, 696);
			this.Controls.Add(this.butSearch);
			this.Controls.Add(this.butLock);
			this.Controls.Add(this.butInvalidate);
			this.Controls.Add(this.butAppend);
			this.Controls.Add(this.labelLocked);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.labelClaimNote);
			this.Controls.Add(this.textClaimNote);
			this.Controls.Add(this.butReferral);
			this.Controls.Add(this.textReferral);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.comboProcStatus);
			this.Controls.Add(this.comboProvNum);
			this.Controls.Add(this.checkHideGraphics);
			this.Controls.Add(this.butPickProv);
			this.Controls.Add(this.labelInvalidSig);
			this.Controls.Add(this.butTopazSign);
			this.Controls.Add(this.buttonUseAutoNote);
			this.Controls.Add(this.textUser);
			this.Controls.Add(this.comboDx);
			this.Controls.Add(this.comboPriority);
			this.Controls.Add(this.comboClinic);
			this.Controls.Add(this.labelClinic);
			this.Controls.Add(this.labelPriority);
			this.Controls.Add(this.labelDx);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.sigBox);
			this.Controls.Add(this.butClearSig);
			this.Controls.Add(this.groupProsth);
			this.Controls.Add(this.textNotes);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.labelIncomplete);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.butSetComplete);
			this.Controls.Add(this.butEditAnyway);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.labelSetComplete);
			this.Controls.Add(this.labelClaim);
			this.Controls.Add(this.panel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormProcEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Procedure Info";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormProcEdit_FormClosing);
			this.Load += new System.EventHandler(this.FormProcInfo_Load);
			this.groupQuadrant.ResumeLayout(false);
			this.groupArch.ResumeLayout(false);
			this.panelSurfaces.ResumeLayout(false);
			this.groupSextant.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.groupProsth.ResumeLayout(false);
			this.groupProsth.PerformLayout();
			this.groupCanadianProcTypeCode.ResumeLayout(false);
			this.tabControl.ResumeLayout(false);
			this.tabPageFinancial.ResumeLayout(false);
			this.tabPageFinancial.PerformLayout();
			this.tabPageMedical.ResumeLayout(false);
			this.tabPageMedical.PerformLayout();
			this.tabPageMisc.ResumeLayout(false);
			this.tabPageMisc.PerformLayout();
			this.tabPageCanada.ResumeLayout(false);
			this.tabPageCanada.PerformLayout();
			this.tabPageOrion.ResumeLayout(false);
			this.tabPageOrion.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormProcInfo_Load(object sender,System.EventArgs e) {
			ClaimProcsForProc=new List<ClaimProc>();
			//Set the title bar to show the patient's name much like the main screen does.
			this.Text+=" - "+PatCur.GetNameLF();
			//richTextBox1.Text="This is a test of the functions of a rich text box.";
			//webBrowser1.
			//richTextBox1.Select(10,4);
			//richTextBox1.SelectionFont=new Font(FontFamily.GenericMonospace,8);
			//richTextBox1.Select(22,9);
			//richTextBox1.SelectionFont=new Font(FontFamily.GenericMonospace,8,FontStyle.Underline);
			textDateEntry.Text=ProcCur.DateEntryC.ToShortDateString();
			if(PrefC.GetBool(PrefName.EasyHidePublicHealth)){
				labelPlaceService.Visible=false;
				comboPlaceService.Visible=false;
				labelSite.Visible=false;
				textSite.Visible=false;
				butPickSite.Visible=false;
			}
			if(PrefC.GetLong(PrefName.UseInternationalToothNumbers)==1){
				listBoxTeeth.Items.Clear();
				listBoxTeeth.Items.AddRange(new string[] {"18","17","16","15","14","13","12","11","21","22","23","24","25","26","27","28"});
				listBoxTeeth2.Items.Clear();
				listBoxTeeth2.Items.AddRange(new string[] {"48","47","46","45","44","43","42","41","31","32","33","34","35","36","37","38"});
			}
			if(PrefC.GetLong(PrefName.UseInternationalToothNumbers)==3){
				listBoxTeeth.Items.Clear();
				listBoxTeeth.Items.AddRange(new string[] {"8","7","6","5","4","3","2","1","1","2","3","4","5","6","7","8"});
				listBoxTeeth2.Items.Clear();
				listBoxTeeth2.Items.AddRange(new string[] {"8","7","6","5","4","3","2","1","1","2","3","4","5","6","7","8"});
			}
			if(!Security.IsAuthorized(Permissions.ProcEditShowFee,true)){
				labelAmount.Visible=false;
				textProcFee.Visible=false;
			}
			if(!Security.IsAuthorized(Permissions.ProcedureNote,true)) {
				textNotes.Enabled=false;
				buttonUseAutoNote.Enabled=false;
			}
			ClaimList=Claims.Refresh(PatCur.PatNum);
			ProcedureCode2=ProcedureCodes.GetProcCode(ProcCur.CodeNum);
			if(ProcCur.ProcStatus==ProcStat.C && PrefC.GetBool(PrefName.ProcLockingIsAllowed) && !ProcCur.IsLocked) {
				butLock.Visible=true;
			}
			else {
				butLock.Visible=false;
			}
			if(IsNew){
				if(ProcCur.ProcStatus==ProcStat.C){
					if(!_isQuickAdd && !Security.IsAuthorized(Permissions.ProcComplCreate)){
						DialogResult=DialogResult.Cancel;
						return;
					}
				}
				//SetControls();
				//return;
			}
			else{
				if(ProcCur.ProcStatus==ProcStat.C){
					textDiscount.Enabled=false;
					if(ProcCur.IsLocked) {//Whether locking is currently allowed, this proc may have been locked previously.
						butOK.Enabled=false;//use this state to cascade permission to any form opened from here
						butDelete.Enabled=false;
						butChange.Enabled=false;
						butEditAnyway.Enabled=false;
						butSetComplete.Enabled=false;
						butSnomedBodySiteSelect.Enabled=false;
						butNoneSnomedBodySite.Enabled=false;
						labelLocked.Visible=true;
						butAppend.Visible=true;
						textNotes.ReadOnly=true;//just for visual cue.  No way to save changes, anyway.
						textNotes.BackColor=SystemColors.Control;
						butInvalidate.Visible=true;
						butInvalidate.Location=butLock.Location;
					}
					else{
						butInvalidate.Visible=false;
						//because islocked overrides security:
						if(!Security.IsAuthorized(Permissions.ProcComplEdit,ProcCur.DateEntryC)){
							butOK.Enabled=false;//use this state to cascade permission to any form opened from here
							butDelete.Enabled=false;
							butChange.Enabled=false;
							butEditAnyway.Enabled=false;
							butSetComplete.Enabled=false;
						}
					}
				}
			}
			//ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			ClaimProcsForProc=ClaimProcs.RefreshForProc(ProcCur.ProcNum);
			PatPlanList=PatPlans.Refresh(PatCur.PatNum);
			BenefitList=Benefits.Refresh(PatPlanList,SubList);
			if(Procedures.IsAttachedToClaim(ProcCur,ClaimProcsForProc)){
				StartedAttachedToClaim=true;
				//however, this doesn't stop someone from creating a claim while this window is open,
				//so this is checked at the end, too.
				panel1.Enabled=false;
				comboProcStatus.Enabled=false;
				checkNoBillIns.Enabled=false;
				butChange.Enabled=false;
				butDelete.Enabled=false;
				butEditAnyway.Visible=true;
				labelClaim.Visible=true;
				butSetComplete.Enabled=false;
			}
			if(PrefC.GetBool(PrefName.EasyHideClinical)){
				labelDx.Visible=false;
				comboDx.Visible=false;
				labelPrognosis.Visible=false;
				comboPrognosis.Visible=false;
			}
			if(PrefC.GetBool(PrefName.EasyHideMedicaid)) {
				comboBillingTypeOne.Visible=false;
				labelBillingTypeOne.Visible=false;
				comboBillingTypeTwo.Visible=false;
				labelBillingTypeTwo.Visible=false;
			}
			if(CultureInfo.CurrentCulture.Name.EndsWith("CA")) {//Canadian. en-CA or fr-CA
				//groupCanadianProcType.Location=new Point(106,301);
				groupProsth.Visible=false;
				labelClaimNote.Visible=false;
				textClaimNote.Visible=false;
				butBF.Text=Lan.g(this,"B/V");//vestibular instead of facial
				butV.Text=Lan.g(this,"5");
				if(ProcedureCode2.IsCanadianLab) { //Prevent lab fees from having lab fees attached.
					labelCanadaLabFee1.Visible=false;
					textCanadaLabFee1.Visible=false;
					labelCanadaLabFee2.Visible=false;
					textCanadaLabFee2.Visible=false;
				}
				else {
					canadaLabFees=Procedures.GetCanadianLabFees(ProcCur.ProcNum);
					if(canadaLabFees.Count>0) {
						textCanadaLabFee1.Text=canadaLabFees[0].ProcFee.ToString("n");
						if(canadaLabFees[0].ProcStatus==ProcStat.C) {
							textCanadaLabFee1.ReadOnly=true;
						}
					}
					if(canadaLabFees.Count>1) {
						textCanadaLabFee2.Text=canadaLabFees[1].ProcFee.ToString("n");
						if(canadaLabFees[1].ProcStatus==ProcStat.C) {
							textCanadaLabFee2.ReadOnly=true;
						}
					}
				}
			}
			else {
				tabControl.Controls.Remove(tabPageCanada);
				//groupCanadianProcType.Visible=false;
			}
			if(Programs.UsingOrion) {
				if(IsNew) {
					OrionProcCur=new OrionProc();
					OrionProcCur.ProcNum=ProcCur.ProcNum;
					if(ProcCur.ProcStatus==ProcStat.EO) {
						OrionProcCur.Status2=OrionStatus.E;
					}
					else {
						OrionProcCur.Status2=OrionStatus.TP;
					}
				}
				else {
					OrionProcCur=OrionProcs.GetOneByProcNum(ProcCur.ProcNum);
					if(ProcCur.DateTP<MiscData.GetNowDateTime().Date && 
						(OrionProcCur.Status2==OrionStatus.CA_EPRD
						|| OrionProcCur.Status2==OrionStatus.CA_PD
						|| OrionProcCur.Status2==OrionStatus.CA_Tx
						|| OrionProcCur.Status2==OrionStatus.R)) {//Not allowed to edit procedures with these statuses that are older than a day.
						MsgBox.Show(this,"You cannot edit refused or cancelled procedures.");
						DialogResult=DialogResult.Cancel;
					}
					if(OrionProcCur.Status2==OrionStatus.C || OrionProcCur.Status2==OrionStatus.CR || OrionProcCur.Status2==OrionStatus.CS) {
						textNotes.Enabled=false;
					}
				}
				textDateTP.ReadOnly=true;
				//panelOrion.Visible=true;
				butAddEstimate.Visible=false;
				checkNoBillIns.Visible=false;
				gridIns.Visible=false;
				butAddAdjust.Visible=false;
				gridPay.Visible=false;
				gridAdj.Visible=false;
				comboProcStatus.Enabled=false;
				labelAmount.Visible=false;
				textProcFee.Visible=false;
				labelPriority.Visible=false;
				comboPriority.Visible=false;
				butSetComplete.Visible=false;
				labelSetComplete.Visible=false;
			}
			else {
				tabControl.Controls.Remove(tabPageOrion);
			}
			if(Programs.UsingOrion || PrefC.GetBool(PrefName.ShowFeatureMedicalInsurance)) {
				labelEndTime.Visible=true;
				textTimeEnd.Visible=true;
				butNow.Visible=true;
				labelTimeFinal.Visible=true;
				textTimeFinal.Visible=true;
			}
			if(PrefC.GetBool(PrefName.ShowFeatureEhr)) {
				textNotes.HideSelection=false;//When text is selected programmatically using our Search function, this causes the selection to be visible to the users.
			}
			else {
				butSearch.Visible=false;
				labelSnomedCtBodySite.Visible=false;
				textSnomedBodySite.Visible=false;
				butSnomedBodySiteSelect.Visible=false;
				butNoneSnomedBodySite.Visible=false;
			}
			if(CultureInfo.CurrentCulture.Name.EndsWith("CA")) {//Canadian. en-CA or fr-CA
				radioS1.Text="03";//Sextant 1 in the United States is sextant 03 in Canada.
				radioS2.Text="04";//Sextant 2 in the United States is sextant 04 in Canada.
				radioS3.Text="05";//Sextant 3 in the United States is sextant 05 in Canada.
				radioS4.Text="06";//Sextant 4 in the United States is sextant 06 in Canada.
				radioS5.Text="07";//Sextant 5 in the United States is sextant 07 in Canada.
				radioS6.Text="08";//Sextant 6 in the United States is sextant 08 in Canada.
			}
			_provNumOrderingSelected=ProcCur.ProvOrderOverride;
			comboProvNumOrdering.Items.Clear();
			for(int i=0;i<ProviderC.ListShort.Count;i++) {
				comboProvNumOrdering.Items.Add(ProviderC.ListShort[i].GetLongDesc());//Only visible provs added to combobox.
				if(ProviderC.ListShort[i].ProvNum==ProcCur.ProvOrderOverride) {
					comboProvNumOrdering.SelectedIndex=i;//Sets combo text too.
				}
			}
			IsStartingUp=true;
			FillControlsOnStartup();
			SetControlsUpperLeft();
			FillReferral();
			FillIns(false);
			FillPayments();
			FillAdj();
			IsStartingUp=false;
			//string retVal=ProcCur.Note+ProcCur.UserNum.ToString();
			//MsgBoxCopyPaste msgb=new MsgBoxCopyPaste(retVal);
			//msgb.ShowDialog();
			if(_isQuickAdd) {
				textDate.Enabled=false;
				butOK_Click(this,new EventArgs());
				if(this.DialogResult!=DialogResult.OK) {
					this.WindowState=FormWindowState.Normal;
					this.CenterToScreen();
					this.BringToFront();
				}
			}
		}

		private void sigBoxTopaz_Leave(object sender,EventArgs e) {
			//If the Topaz state does not get set to 0 before trying to accept input again (e.g. from another Topaz object), BSB Topaz signature pads 
			//	will not be able to accept input from a new Topaz signature box instance.
			//E.g. launching FormProcNoteAppend causes two Topaz signature boxes to be present.
			if(CodeBase.TopazWrapper.GetTopazState(sigBoxTopaz)==1) {//if accepting input.
				CodeBase.TopazWrapper.SetTopazState(sigBoxTopaz,0);
			}
		}

		///<summary>ONLY run on startup. Fills the basic controls, except not the ones in the upper left panel which are handled in SetControlsUpperLeft.</summary>
		private void FillControlsOnStartup(){
			comboProcStatus.Items.Clear();
			comboProcStatus.Items.Add(Lan.g(this,"Treatment Planned"));
			comboProcStatus.Items.Add(Lan.g(this,"Complete"));
			if(!PrefC.GetBool(PrefName.EasyHideClinical)) {
				comboProcStatus.Items.Add(Lan.g(this,"Existing-Current Prov"));
				comboProcStatus.Items.Add(Lan.g(this,"Existing-Other Prov"));
				comboProcStatus.Items.Add(Lan.g(this,"Referred Out"));
				//comboProcStatus.Items.Add(Lan.g(this,"Deleted"));
				comboProcStatus.Items.Add(Lan.g(this,"Condition"));
			}
			if(ProcCur.ProcStatus==ProcStat.TP){
				comboProcStatus.SelectedIndex=0;
			}
			if(ProcCur.ProcStatus==ProcStat.C) {
				comboProcStatus.SelectedIndex=1;
			}
			if(!PrefC.GetBool(PrefName.EasyHideClinical)) {
				if(ProcCur.ProcStatus==ProcStat.EC) {
					comboProcStatus.SelectedIndex=2;
				}
				if(ProcCur.ProcStatus==ProcStat.EO) {
					comboProcStatus.SelectedIndex=3;
				}
				if(ProcCur.ProcStatus==ProcStat.R) {
					comboProcStatus.SelectedIndex=4;
				}
				if(ProcCur.ProcStatus==ProcStat.Cn) {
					comboProcStatus.SelectedIndex=5;
				}
			}
			if(ProcCur.ProcStatus==ProcStat.TPi) {
				comboProcStatus.Items.Add(Lan.g("TreatPlan","Treatment Planned Inactive"));
				comboProcStatus.SelectedIndex=comboProcStatus.Items.Count-1;
				comboProcStatus.Enabled=false;
				butSetComplete.Enabled=false;
			}
			if(ProcCur.ProcStatus==ProcStat.D && ProcCur.IsLocked){//an invalidated proc
				comboProcStatus.Items.Clear();
				comboProcStatus.Items.Add(Lan.g(this,"Invalidated"));
				comboProcStatus.SelectedIndex=0;
				comboProcStatus.Enabled=false;
				butInvalidate.Visible=false;
				butOK.Enabled=false;
				butDelete.Enabled=false;
				butChange.Enabled=false;
				butEditAnyway.Enabled=false;
				butSetComplete.Enabled=false;
				butAddEstimate.Enabled=false;
				butAddAdjust.Enabled=false;
			}
			//if clinical is hidden, then there's a chance that no item is selected at this point.
			comboDx.Items.Clear();
			for(int i=0;i<DefC.Short[(int)DefCat.Diagnosis].Length;i++){
				comboDx.Items.Add(DefC.Short[(int)DefCat.Diagnosis][i].ItemName);
				if(DefC.Short[(int)DefCat.Diagnosis][i].DefNum==ProcCur.Dx)
					comboDx.SelectedIndex=i;
			}
			comboPrognosis.Items.Clear();
			comboPrognosis.Items.Add(Lan.g(this,"no prognosis"));
			comboPrognosis.SelectedIndex=0;
			for(int i=0;i<DefC.Short[(int)DefCat.Prognosis].Length;i++) {
				comboPrognosis.Items.Add(DefC.Short[(int)DefCat.Prognosis][i].ItemName);
				if(DefC.Short[(int)DefCat.Prognosis][i].DefNum==ProcCur.Prognosis)
					comboPrognosis.SelectedIndex=i+1;
			}
			checkHideGraphics.Checked=ProcCur.HideGraphics;
			if(Programs.UsingOrion && this.IsNew && !OrionDentist){
				ProcCur.ProvNum=Providers.GetOrionProvNum(ProcCur.ProvNum);//Returns 0 if logged in as non provider.
			}//ProvNum of 0 will be required to change before closing form.
			comboProvNum.Items.Clear();
			for(int i=0;i<ProviderC.ListShort.Count;i++){
				comboProvNum.Items.Add(ProviderC.ListShort[i].Abbr);
				if(ProviderC.ListShort[i].ProvNum==ProcCur.ProvNum) {
					comboProvNum.SelectedIndex=i;
				}
			}
			comboPriority.Items.Clear();
			comboPriority.Items.Add(Lan.g(this,"no priority"));
			comboPriority.SelectedIndex=0;
			for(int i=0;i<DefC.Short[(int)DefCat.TxPriorities].Length;i++){
				comboPriority.Items.Add(DefC.Short[(int)DefCat.TxPriorities][i].ItemName);
				if(DefC.Short[(int)DefCat.TxPriorities][i].DefNum==ProcCur.Priority)
					comboPriority.SelectedIndex=i+1;
			}
			comboBillingTypeOne.Items.Clear();
			comboBillingTypeOne.Items.Add(Lan.g(this,"none"));
			comboBillingTypeOne.SelectedIndex=0;
			for(int i=0;i<DefC.Short[(int)DefCat.BillingTypes].Length;i++) {
				comboBillingTypeOne.Items.Add(DefC.Short[(int)DefCat.BillingTypes][i].ItemName);
				if(DefC.Short[(int)DefCat.BillingTypes][i].DefNum==ProcCur.BillingTypeOne)
					comboBillingTypeOne.SelectedIndex=i+1;
			}
			comboBillingTypeTwo.Items.Clear();
			comboBillingTypeTwo.Items.Add(Lan.g(this,"none"));
			comboBillingTypeTwo.SelectedIndex=0;
			for(int i=0;i<DefC.Short[(int)DefCat.BillingTypes].Length;i++) {
				comboBillingTypeTwo.Items.Add(DefC.Short[(int)DefCat.BillingTypes][i].ItemName);
				if(DefC.Short[(int)DefCat.BillingTypes][i].DefNum==ProcCur.BillingTypeTwo)
					comboBillingTypeTwo.SelectedIndex=i+1;
			}
			textBillingNote.Text=ProcCur.BillingNote;
			textNotes.Text=ProcCur.Note;
			//Scroll to the end of the note for procedures for today (or completed today).
			if(ProcCur.DateEntryC.Date==DateTime.Today) {
				textNotes.Select(textNotes.Text.Length,0);
			}
			CheckForCompleteNote();
			comboPlaceService.Items.Clear();
			comboPlaceService.Items.AddRange(Enum.GetNames(typeof(PlaceOfService)));
			comboPlaceService.SelectedIndex=(int)ProcCur.PlaceService;
			//checkHideGraphical.Checked=ProcCur.HideGraphical;
			if(PrefC.GetBool(PrefName.EasyNoClinics)) {
				comboClinic.Visible=false;
				labelClinic.Visible=false;
			}
			else {
				comboClinic.Items.Add("none");
				comboClinic.SelectedIndex=0;
				for(int i=0;i<Clinics.List.Length;i++) {
					comboClinic.Items.Add(Clinics.List[i].Description);
					if(Clinics.List[i].ClinicNum==ProcCur.ClinicNum) {
						comboClinic.SelectedIndex=i+1;
					}
				}
			}
			textSite.Text=Sites.GetDescription(ProcCur.SiteNum);
			if(CultureInfo.CurrentCulture.Name.EndsWith("CA")) {//Canadian. en-CA or fr-CA
				if(ProcCur.CanadianTypeCodes==null || ProcCur.CanadianTypeCodes=="") {
					checkTypeCodeX.Checked=true;
				}
				else {
					if(ProcCur.CanadianTypeCodes.Contains("A")) {
						checkTypeCodeA.Checked=true;
					}
					if(ProcCur.CanadianTypeCodes.Contains("B")) {
						checkTypeCodeB.Checked=true;
					}
					if(ProcCur.CanadianTypeCodes.Contains("C")) {
						checkTypeCodeC.Checked=true;
					}
					if(ProcCur.CanadianTypeCodes.Contains("E")) {
						checkTypeCodeE.Checked=true;
					}
					if(ProcCur.CanadianTypeCodes.Contains("L")) {
						checkTypeCodeL.Checked=true;
					}
					if(ProcCur.CanadianTypeCodes.Contains("S")) {
						checkTypeCodeS.Checked=true;
					}
					if(ProcCur.CanadianTypeCodes.Contains("X")) {
						checkTypeCodeX.Checked=true;
					}
				}
			}
			else{
				if(ProcedureCode2.IsProsth){
					listProsth.Items.Add(Lan.g(this,"No"));
					listProsth.Items.Add(Lan.g(this,"Initial"));
					listProsth.Items.Add(Lan.g(this,"Replacement"));
					switch(ProcCur.Prosthesis){
						case "":
							listProsth.SelectedIndex=0;
							break;
						case "I":
							listProsth.SelectedIndex=1;
							break;
						case "R":
							listProsth.SelectedIndex=2;
							break;
					}
					if(ProcCur.DateOriginalProsth.Year>1880){
						textDateOriginalProsth.Text=ProcCur.DateOriginalProsth.ToShortDateString();
					}
					checkIsDateProsthEst.Checked=ProcCur.IsDateProsthEst;
				}
				else{
					groupProsth.Visible=false;
				}
			}
			textDiscount.Text=ProcCur.Discount.ToString("f");
			//medical
			textMedicalCode.Text=ProcCur.MedicalCode;
			if(ProcCur.IcdVersion==9) {
				checkIcdVersion.Checked=false;
			}
			else {//ICD-10
				checkIcdVersion.Checked=true;
			}
			SetIcdLabels();
			textDiagnosticCode.Text=ProcCur.DiagnosticCode;
			textDiagnosticCode2.Text=ProcCur.DiagnosticCode2;
			textDiagnosticCode3.Text=ProcCur.DiagnosticCode3;
			textDiagnosticCode4.Text=ProcCur.DiagnosticCode4;
			checkIsPrincDiag.Checked=ProcCur.IsPrincDiag;
			textCodeMod1.Text = ProcCur.CodeMod1;
			textCodeMod2.Text = ProcCur.CodeMod2;
			textCodeMod3.Text = ProcCur.CodeMod3;
			textCodeMod4.Text = ProcCur.CodeMod4;
			textUnitQty.Text = ProcCur.UnitQty.ToString();
			comboUnitType.Items.Clear();
			_snomedBodySite=Snomeds.GetByCode(ProcCur.SnomedBodySite);
			if(_snomedBodySite==null) {
				textSnomedBodySite.Text="";
			}
			else {
				textSnomedBodySite.Text=_snomedBodySite.Description;
			}
			for(int i=0;i<Enum.GetNames(typeof(ProcUnitQtyType)).Length;i++) {
				comboUnitType.Items.Add(Enum.GetNames(typeof(ProcUnitQtyType))[i]);
			}
			comboUnitType.SelectedIndex=(int)ProcCur.UnitQtyType;
			textRevCode.Text = ProcCur.RevCode;
			//DrugNDC is handled in SetControlsUpperLeft
			comboDrugUnit.Items.Clear();
			for(int i=0;i<Enum.GetNames(typeof(EnumProcDrugUnit)).Length;i++){
				comboDrugUnit.Items.Add(Enum.GetNames(typeof(EnumProcDrugUnit))[i]);
			}
			comboDrugUnit.SelectedIndex=(int)ProcCur.DrugUnit;
			if(ProcCur.DrugQty!=0){
				textDrugQty.Text=ProcCur.DrugQty.ToString();
			}
			textClaimNote.Text=ProcCur.ClaimNote;
			textUser.Text=Userods.GetName(ProcCur.UserNum);//might be blank. Will change automatically if user changes note or alters sig.
			labelInvalidSig.Visible=false;
			sigBox.Visible=true;
			if(ProcCur.SigIsTopaz) {
				if(ProcCur.Signature!="") {
					//if(allowTopaz){
					sigBox.Visible=false;
					sigBoxTopaz.Visible=true;
					CodeBase.TopazWrapper.ClearTopaz(sigBoxTopaz);
					CodeBase.TopazWrapper.SetTopazCompressionMode(sigBoxTopaz,0);
					CodeBase.TopazWrapper.SetTopazEncryptionMode(sigBoxTopaz,0);
					CodeBase.TopazWrapper.SetTopazKeyString(sigBoxTopaz,"0000000000000000");
					CodeBase.TopazWrapper.SetTopazEncryptionMode(sigBoxTopaz,2);//high encryption
					CodeBase.TopazWrapper.SetTopazCompressionMode(sigBoxTopaz,2);//high compression
					CodeBase.TopazWrapper.SetTopazSigString(sigBoxTopaz,ProcCur.Signature);
					//older notes may have been signed with zeros due to a bug.  We still want to show the sig in that case.
					//but if a sig is not showing, then set the key string to try to get it to show.
					if(CodeBase.TopazWrapper.GetTopazNumberOfTabletPoints(sigBoxTopaz)==0) {
						CodeBase.TopazWrapper.SetTopazAutoKeyData(sigBoxTopaz,ProcCur.Note+ProcCur.UserNum.ToString());
						CodeBase.TopazWrapper.SetTopazSigString(sigBoxTopaz,ProcCur.Signature);
					}
					//If sig is not showing, then try encryption mode 3 for signatures signed with old SigPlusNet.dll.
					if(CodeBase.TopazWrapper.GetTopazNumberOfTabletPoints(sigBoxTopaz)==0) {
						CodeBase.TopazWrapper.SetTopazEncryptionMode(sigBoxTopaz,3);//Unknown mode (told to use via TopazSystems)
						CodeBase.TopazWrapper.SetTopazSigString(sigBoxTopaz,ProcCur.Signature);
					}
					//if still not showing, then it must be invalid
					if(CodeBase.TopazWrapper.GetTopazNumberOfTabletPoints(sigBoxTopaz)==0) {
						labelInvalidSig.Visible=true;
					}
					CodeBase.TopazWrapper.SetTopazState(sigBoxTopaz,0);
					//}
				}
			}
			else {
				if(ProcCur.Signature!=null && ProcCur.Signature!="") {
					sigBox.Visible=true;
					if(sigBoxTopaz!=null) {
						sigBoxTopaz.Visible=false;
					}
					sigBox.ClearTablet();
					//sigBox.SetSigCompressionMode(0);
					//sigBox.SetEncryptionMode(0);
					sigBox.SetKeyString("0000000000000000");
					//Note.Replace("\r\n","\n") will only revert the changes Middle Tier made to the note back it's original form, which would invalidate this signature. No effect when not using Middle Tier.
					sigBox.SetAutoKeyData(ProcCur.Note.Replace("\r\n","\n")+ProcCur.UserNum.ToString());
					//sigBox.SetEncryptionMode(2);//high encryption
					//sigBox.SetSigCompressionMode(2);//high compression
					sigBox.SetSigString(ProcCur.Signature);
					if(sigBox.NumberOfTabletPoints()==0) {  //Signature invalid.
						//At this point we think the signature is invalid.  We must now recheck signature without replacing \r\n with \n.  This is because old 
						//	signatures were captured with \r\n instead of \n, and updating to a newer version would invalidate all valid signatures.
						sigBox.SetAutoKeyData(ProcCur.Note+ProcCur.UserNum.ToString());
						sigBox.SetSigString(ProcCur.Signature);
						if(sigBox.NumberOfTabletPoints()==0) {  //Both signature checks were invalid.
							labelInvalidSig.Visible=true;
						}
						else { //The first signature check was invalid, but the second was valid.
							labelInvalidSig.Visible=false;
						}
					}
					sigBox.SetTabletState(0);//not accepting input.  To accept input, change the note, or clear the sig.
				}
			}
			if(Programs.UsingOrion) {//panelOrion.Visible) {
				comboDPC.Items.Clear();
				//comboDPC.Items.AddRange(Enum.GetNames(typeof(OrionDPC)));
				comboDPC.Items.Add("Not Specified");
				comboDPC.Items.Add("None");
				comboDPC.Items.Add("1A-within 1 day");
				comboDPC.Items.Add("1B-within 30 days");
				comboDPC.Items.Add("1C-within 60 days");
				comboDPC.Items.Add("2-within 120 days");
				comboDPC.Items.Add("3-within 1 year");
				comboDPC.Items.Add("4-no further treatment/appt");
				comboDPC.Items.Add("5-no appointment needed");
				comboDPCpost.Items.Clear();
				comboDPCpost.Items.Add("Not Specified");
				comboDPCpost.Items.Add("None");
				comboDPCpost.Items.Add("1A-within 1 day");
				comboDPCpost.Items.Add("1B-within 30 days");
				comboDPCpost.Items.Add("1C-within 60 days");
				comboDPCpost.Items.Add("2-within 120 days");
				comboDPCpost.Items.Add("3-within 1 year");
				comboDPCpost.Items.Add("4-no further treatment/appt");
				comboDPCpost.Items.Add("5-no appointment needed");
				comboStatus.Items.Clear();
				comboStatus.Items.Add("TP-treatment planned");
				comboStatus.Items.Add("C-completed");
				comboStatus.Items.Add("E-existing prior to incarceration");
				comboStatus.Items.Add("R-refused treatment");
				comboStatus.Items.Add("RO-referred out to specialist");
				comboStatus.Items.Add("CS-completed by specialist");
				comboStatus.Items.Add("CR-completed by registry");
				comboStatus.Items.Add("CA_Tx-cancelled, tx plan changed");
				comboStatus.Items.Add("CA_EPRD-cancelled, eligible parole");
				comboStatus.Items.Add("CA_P/D-cancelled, parole/discharge");
				comboStatus.Items.Add("S-suspended, unacceptable plaque");
				comboStatus.Items.Add("ST-stop clock, multi visit");
				comboStatus.Items.Add("W-watch");
				comboStatus.Items.Add("A-alternative");
				comboStatus.SelectedIndex=0;
				ProcedureCode pc=ProcedureCodes.GetProcCodeFromDb(ProcCur.CodeNum);
				checkIsRepair.Visible=pc.IsProsth;
				//DateTP doesn't get set sometimes and calculations are made based on the DateTP. So set it to the current date as fail-safe.
				if(ProcCur.DateTP.Year<1880) {
					textDateTP.Text=MiscData.GetNowDateTime().ToShortDateString();
				}
				else {
					textDateTP.Text=ProcCur.DateTP.ToShortDateString();
				}
				BitArray ba=new BitArray(new int[] { (int)OrionProcCur.Status2 });//should nearly always be non-zero
				for(int i=0;i<ba.Length;i++) {
					if(ba[i]) {
						comboStatus.SelectedIndex=i;
						break;
					}
				}
				if(!IsNew) {
					OrionProcOld=OrionProcCur.Copy();
					comboDPC.SelectedIndex=(int)OrionProcCur.DPC;
					comboDPCpost.SelectedIndex=(int)OrionProcCur.DPCpost;
					if(OrionProcCur.DPC==OrionDPC.NotSpecified ||
						OrionProcCur.DPC==OrionDPC.None ||
						OrionProcCur.DPC==OrionDPC._4 ||
						OrionProcCur.DPC==OrionDPC._5) {
						labelScheduleBy.Visible=true;
					}
					if(OrionProcCur.DateScheduleBy.Year>1880) {
						textDateScheduled.Text=OrionProcCur.DateScheduleBy.ToShortDateString();
					}
					if(OrionProcCur.DateStopClock.Year>1880) {
						textDateStop.Text=OrionProcCur.DateStopClock.ToShortDateString();
					}
					checkIsOnCall.Checked=OrionProcCur.IsOnCall;
					checkIsEffComm.Checked=OrionProcCur.IsEffectiveComm;
					checkIsRepair.Checked=OrionProcCur.IsRepair;
				}
				else {
					labelScheduleBy.Visible=true;
					comboDPC.SelectedIndex=0;
					comboDPCpost.SelectedIndex=0;
					textDateStop.Text="";
				}
			}
		}

		private void SetSurfButtons(){
			if(textSurfaces.Text.Contains("B") || textSurfaces.Text.Contains("F")) butBF.BackColor=Color.White;
			if(CultureInfo.CurrentCulture.Name.EndsWith("CA")) {//Canadian. en-CA or fr-CA
				if(textSurfaces.Text.Contains("V")) butBF.BackColor=Color.White;
			}
			if(textSurfaces.Text.Contains("O") || textSurfaces.Text.Contains("I")) butOI.BackColor=Color.White;
			if(textSurfaces.Text.Contains("M")) butM.BackColor=Color.White;
			if(textSurfaces.Text.Contains("D")) butD.BackColor=Color.White;
			if(textSurfaces.Text.Contains("L")) butL.BackColor=Color.White;
			if(CultureInfo.CurrentCulture.Name.EndsWith("CA")) {//Canadian. en-CA or fr-CA
				if(textSurfaces.Text.Contains("5")) butV.BackColor=Color.White;
			}
			else{
				if(textSurfaces.Text.Contains("V")) butV.BackColor=Color.White;
			}
		}

		///<summary>Called on open and after changing code.  Sets the visibilities and the data of all the fields in the upper left panel.</summary>
		private void SetControlsUpperLeft(){
			textDateTP.Text=ProcCur.DateTP.ToString("d");
			DateTime dateT;
			if(IsStartingUp){
				textDate.Text=ProcCur.ProcDate.ToString("d");
				dateT=PIn.DateT(ProcCur.ProcTime.ToString());
				if(dateT.ToShortTimeString()!="12:00 AM"){
					textTimeStart.Text+=dateT.ToShortTimeString();
				}
				if(Programs.UsingOrion || PrefC.GetBool(PrefName.ShowFeatureMedicalInsurance)) {
					dateT=PIn.DateT(ProcCur.ProcTimeEnd.ToString());
					if(dateT.ToShortTimeString()!="12:00 AM") {
						textTimeEnd.Text=dateT.ToShortTimeString();
					}
					UpdateFinalMin();			
					if(ProcCur.DateTP.Year<1880) {
						textDateTP.Text=MiscData.GetNowDateTime().ToShortDateString();
					}
				}
			}
			textProc.Text=ProcedureCode2.ProcCode;
			textDesc.Text=ProcedureCode2.Descript;
			textDrugNDC.Text=ProcedureCode2.DrugNDC;
			switch (ProcedureCode2.TreatArea){
				case TreatmentArea.Surf:
					this.textTooth.Visible=true;
					this.labelTooth.Visible=true;
					this.textSurfaces.Visible=true;
					this.labelSurfaces.Visible=true;
					this.panelSurfaces.Visible=true;
					if(Tooth.IsValidDB(ProcCur.ToothNum)) {
						errorProvider2.SetError(textTooth,"");
						textTooth.Text=Tooth.ToInternat(ProcCur.ToothNum);
						textSurfaces.Text=Tooth.SurfTidyFromDbToDisplay(ProcCur.Surf,ProcCur.ToothNum);
						SetSurfButtons();
					}
					else{
						errorProvider2.SetError(textTooth,Lan.g(this,"Invalid tooth number."));
						textTooth.Text=ProcCur.ToothNum;
						//textSurfaces.Text=Tooth.SurfTidy(ProcCur.Surf,"");//only valid toothnums allowed
					}
					if(textSurfaces.Text=="")
						errorProvider2.SetError(textSurfaces,"No surfaces selected.");
					else
						errorProvider2.SetError(textSurfaces,"");
					break;
				case TreatmentArea.Tooth:
					this.textTooth.Visible=true;
					this.labelTooth.Visible=true;
					if(Tooth.IsValidDB(ProcCur.ToothNum)){
						errorProvider2.SetError(textTooth,"");
						textTooth.Text=Tooth.ToInternat(ProcCur.ToothNum);
					}
					else{
						errorProvider2.SetError(textTooth,Lan.g(this,"Invalid tooth number."));
						textTooth.Text=ProcCur.ToothNum;
					}
					break;
				case TreatmentArea.Mouth:
						break;
				case TreatmentArea.Quad:
					this.groupQuadrant.Visible=true;
					switch (ProcCur.Surf){
						case "UR": this.radioUR.Checked=true; break;
						case "UL": this.radioUL.Checked=true; break;
						case "LR": this.radioLR.Checked=true; break;
						case "LL": this.radioLL.Checked=true; break;
						//default : 
					}
					break;
				case TreatmentArea.Sextant:
					this.groupSextant.Visible=true;
					switch (ProcCur.Surf){
						case "1": this.radioS1.Checked=true; break;
						case "2": this.radioS2.Checked=true; break;
						case "3": this.radioS3.Checked=true; break;
						case "4": this.radioS4.Checked=true; break;
						case "5": this.radioS5.Checked=true; break;
						case "6": this.radioS6.Checked=true; break;
						//default:
					}
					break;
				case TreatmentArea.Arch:
					this.groupArch.Visible=true;
					switch (ProcCur.Surf){
						case "U": this.radioU.Checked=true; break;
						case "L": this.radioL.Checked=true; break;
					}
					break;
				case TreatmentArea.ToothRange:
					this.labelRange.Visible=true;
					this.listBoxTeeth.Visible=true;
					this.listBoxTeeth2.Visible=true;
					listBoxTeeth.SelectionMode=SelectionMode.MultiExtended;
					listBoxTeeth2.SelectionMode=SelectionMode.MultiExtended;
					if(ProcCur.ToothRange==null){
						break;
					}
   			  string[] sArray=ProcCur.ToothRange.Split(',');//in american
					int idxAmer;
          for(int i=0;i<sArray.Length;i++)  {
						idxAmer=Array.IndexOf(Tooth.labelsUniversal,sArray[i]);
						if(idxAmer==-1){
							continue;
						}
						if(idxAmer<16){
							listBoxTeeth.SetSelected(idxAmer,true);
						}
						else if(idxAmer<32){//ignore anything after 32.
							listBoxTeeth2.SetSelected(idxAmer-16,true);
						}
            /*for(int j=0;j<listBoxTeeth.Items.Count;j++)  {
              if(Tooth.ToInternat(sArray[i])==listBoxTeeth.Items[j].ToString())
				 		    listBoxTeeth.SelectedItem=Tooth.ToInternat(sArray[i]);
					  }
  			    for(int j=0;j<listBoxTeeth2.Items.Count;j++)  {
              if(Tooth.ToInternat(sArray[i])==listBoxTeeth2.Items[j].ToString())
				 		    listBoxTeeth2.SelectedItem=Tooth.ToInternat(sArray[i]);
            }*/
					} 
					break;
			}//end switch
			textProcFee.Text=ProcCur.ProcFee.ToString("n");
		}//end SetControls

		private void FillReferral() {
			List<RefAttach> refsList=RefAttaches.RefreshFiltered(ProcCur.PatNum,false,ProcCur.ProcNum);
			if(refsList.Count==0) {
				textReferral.Text="";
			}
			else {
				Referral referral=Referrals.GetReferral(refsList[0].ReferralNum);
				textReferral.Text=referral.LName+", ";
				if(refsList[0].DateProcComplete.Year<1880) {
					textReferral.Text+=refsList[0].RefDate.ToShortDateString();
				}
				else{
					textReferral.Text+=Lan.g(this,"done:")+refsList[0].DateProcComplete.ToShortDateString();
				}
				if(refsList[0].RefToStatus!=ReferralToStatus.None){
					textReferral.Text+=refsList[0].RefToStatus.ToString();
				}
			}
		}

		private void butReferral_Click(object sender,EventArgs e) {
			FormReferralsPatient FormRP=new FormReferralsPatient();
			FormRP.PatNum=ProcCur.PatNum;
			FormRP.ProcNum=ProcCur.ProcNum;
			FormRP.ShowDialog();
			FillReferral();
		}

		private void FillIns(){
			FillIns(true);
		}

		private void FillIns(bool refreshClaimProcsFirst){
			if(refreshClaimProcsFirst) {
				//ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
				//}
				ClaimProcsForProc=ClaimProcs.RefreshForProc(ProcCur.ProcNum);
			}
			gridIns.BeginUpdate();
			gridIns.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableProcIns","Ins Plan"),190);
			gridIns.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProcIns","Pri/Sec"),50,HorizontalAlignment.Center);
			gridIns.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProcIns","Status"),50,HorizontalAlignment.Center);
			gridIns.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProcIns","NoBill"),45,HorizontalAlignment.Center);
			gridIns.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProcIns","Copay"),55,HorizontalAlignment.Right);
			gridIns.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProcIns","Deduct"),55,HorizontalAlignment.Right);
			gridIns.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProcIns","Percent"),55,HorizontalAlignment.Center);
			gridIns.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProcIns","Ins Est"),55,HorizontalAlignment.Right);
			gridIns.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProcIns","Ins Pay"),55,HorizontalAlignment.Right);
			gridIns.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProcIns","WriteOff"),55,HorizontalAlignment.Right);
			gridIns.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProcIns","Estimate Note"),100);
			gridIns.Columns.Add(col);		 
			col=new ODGridColumn(Lan.g("TableProcIns","Remarks"),165);
			gridIns.Columns.Add(col);		 
			gridIns.Rows.Clear();
			ODGridRow row;
			checkNoBillIns.CheckState=CheckState.Unchecked;
			bool allNoBillIns=true;
			InsPlan plan;
			//ODGridCell cell;
			for(int i=0;i<ClaimProcsForProc.Count;i++) {
				if(ClaimProcsForProc[i].NoBillIns){
					checkNoBillIns.CheckState=CheckState.Indeterminate;
				}
				else{
					allNoBillIns=false;
				}
				row=new ODGridRow();

				row.Cells.Add(InsPlans.GetDescript(ClaimProcsForProc[i].PlanNum,FamCur,PlanList,ClaimProcsForProc[i].InsSubNum,SubList));
				plan=InsPlans.GetPlan(ClaimProcsForProc[i].PlanNum,PlanList);
				if(plan.IsMedical) {
					row.Cells.Add("Med");
				}
				else if(ClaimProcsForProc[i].InsSubNum==PatPlans.GetInsSubNum(PatPlanList,PatPlans.GetOrdinal(PriSecMed.Primary,PatPlanList,PlanList,SubList))){
					row.Cells.Add("Pri");
				}
				else if(ClaimProcsForProc[i].InsSubNum==PatPlans.GetInsSubNum(PatPlanList,PatPlans.GetOrdinal(PriSecMed.Secondary,PatPlanList,PlanList,SubList))) {
					row.Cells.Add("Sec");
				}
				else {
					row.Cells.Add("");
				}
				switch(ClaimProcsForProc[i].Status) {
					case ClaimProcStatus.Received:
						row.Cells.Add("Recd");
						break;
					case ClaimProcStatus.NotReceived:
						row.Cells.Add("NotRec");
						break;
					//adjustment would never show here
					case ClaimProcStatus.Preauth:
						row.Cells.Add("PreA");
						break;
					case ClaimProcStatus.Supplemental:
						row.Cells.Add("Supp");
						break;
					case ClaimProcStatus.CapClaim:
						row.Cells.Add("CapClaim");
						break;
					case ClaimProcStatus.Estimate:
						row.Cells.Add("Est");
						break;
					case ClaimProcStatus.CapEstimate:
						row.Cells.Add("CapEst");
						break;
					case ClaimProcStatus.CapComplete:
						row.Cells.Add("CapComp");
						break;
					default:
						row.Cells.Add("");
						break;
				}
				if(ClaimProcsForProc[i].NoBillIns) {
					row.Cells.Add("X");
					if(ClaimProcsForProc[i].Status!=ClaimProcStatus.CapComplete	&& ClaimProcsForProc[i].Status!=ClaimProcStatus.CapEstimate) {
						row.Cells.Add("");
						row.Cells.Add("");
						row.Cells.Add("");
						row.Cells.Add("");
						row.Cells.Add("");
						row.Cells.Add("");
						row.Cells.Add("");
						row.Cells.Add("");
						row.Cells.Add("");
						gridIns.Rows.Add(row);
						continue;
					}
				}
				else {
					row.Cells.Add("");
				}
				row.Cells.Add(ClaimProcs.GetCopayDisplay(ClaimProcsForProc[i]));
				double ded=ClaimProcs.GetDeductibleDisplay(ClaimProcsForProc[i]);
				if(ded>0) {
					row.Cells.Add(ded.ToString("n"));
				}
				else {
					row.Cells.Add("");
				}
				row.Cells.Add(ClaimProcs.GetPercentageDisplay(ClaimProcsForProc[i]));
				row.Cells.Add(ClaimProcs.GetEstimateDisplay(ClaimProcsForProc[i]));
				if(ClaimProcsForProc[i].Status==ClaimProcStatus.Estimate
					|| ClaimProcsForProc[i].Status==ClaimProcStatus.CapEstimate) 
				{
					row.Cells.Add("");
					row.Cells.Add(ClaimProcs.GetWriteOffEstimateDisplay(ClaimProcsForProc[i]));
				}
				else {
					row.Cells.Add(ClaimProcsForProc[i].InsPayAmt.ToString("n"));
					row.Cells.Add(ClaimProcsForProc[i].WriteOff.ToString("n"));
				}
				row.Cells.Add(ClaimProcsForProc[i].EstimateNote);
				row.Cells.Add(ClaimProcsForProc[i].Remarks);			  
				gridIns.Rows.Add(row);
			}
			gridIns.EndUpdate();
			if(ClaimProcsForProc.Count==0) {
				checkNoBillIns.CheckState=CheckState.Unchecked;
			}
			else if(allNoBillIns) {
				checkNoBillIns.CheckState=CheckState.Checked;
			}
		}

		private void gridIns_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormClaimProc FormC=new FormClaimProc(ClaimProcsForProc[e.Row],ProcCur,FamCur,PatCur,PlanList,HistList,ref LoopList,PatPlanList,true,SubList);
			if(!butOK.Enabled){
				FormC.NoPermissionProc=true;
			}
			FormC.ShowDialog();
			FillIns();
		}

		void butNow_Click(object sender,EventArgs e) {
			if(textTimeStart.Text.Trim()=="") {
				textTimeStart.Text=MiscData.GetNowDateTime().ToShortTimeString();
			}
			else {
				textTimeEnd.Text=MiscData.GetNowDateTime().ToShortTimeString();
			}
		}

		private void butAddEstimate_Click(object sender, System.EventArgs e) {
			FormInsPlanSelect FormIS=new FormInsPlanSelect(PatCur.PatNum);
			FormIS.ShowDialog();
			if(FormIS.DialogResult==DialogResult.Cancel){
				return;
			}
			InsPlan plan=FormIS.SelectedPlan;
			InsSub sub=FormIS.SelectedSub;
			List <Benefit> benList=Benefits.Refresh(PatPlanList,SubList);
			ClaimProc cp=new ClaimProc();
			ClaimProcs.CreateEst(cp,ProcCur,plan,sub);
			if(plan.PlanType=="c") {//capitation
				double allowed=PIn.Double(textProcFee.Text);
				cp.BaseEst=allowed;
				cp.InsEstTotal=allowed;
				cp.CopayAmt=InsPlans.GetCopay(ProcCur.CodeNum,plan.FeeSched,plan.CopayFeeSched,plan.CodeSubstNone,ProcCur.ToothNum,ProcCur.ClinicNum,ProcCur.ProvNum);
				if(cp.CopayAmt > allowed) {//if the copay is greater than the allowed fee calculated above
					cp.CopayAmt=allowed;//reduce the copay
				}
				if(cp.CopayAmt==-1) {
					cp.CopayAmt=0;
				}
				cp.WriteOffEst=cp.BaseEst-cp.CopayAmt;
				if(cp.WriteOffEst<0) {
					cp.WriteOffEst=0;
				}
				cp.WriteOff=cp.WriteOffEst;
				ClaimProcs.Update(cp);
			}
			long patPlanNum=PatPlans.GetPatPlanNum(sub.InsSubNum,PatPlanList);
			if(patPlanNum > 0){
				double paidOtherInsTotal=ClaimProcs.GetPaidOtherInsTotal(cp,PatPlanList);
				double writeOffOtherIns=ClaimProcs.GetWriteOffOtherIns(cp,PatPlanList);
				ClaimProcs.ComputeBaseEst(cp,ProcCur,plan,patPlanNum,benList,
					HistList,LoopList,PatPlanList,paidOtherInsTotal,paidOtherInsTotal,PatCur.Age,writeOffOtherIns);	
			}
			FormClaimProc FormC=new FormClaimProc(cp,ProcCur,FamCur,PatCur,PlanList,HistList,ref LoopList,PatPlanList,true,SubList);
			//FormC.NoPermission not needed because butAddEstimate not enabled
			FormC.ShowDialog();
			if(FormC.DialogResult==DialogResult.Cancel){
				ClaimProcs.Delete(cp);
			}
			FillIns();
		}

		private void FillPayments(){
			PaySplit[] PaySplitList=PaySplits.Refresh(ProcCur.PatNum);
			PaySplitsForProc=PaySplits.GetForProc(ProcCur.ProcNum,PaySplitList);
			List<long> payNums=new List<long>();//[];
			for(int i=0;i<PaySplitsForProc.Count;i++) {
				payNums.Add(((PaySplit)PaySplitsForProc[i]).PayNum);
			}
			PaymentsForProc=Payments.GetPayments(payNums);
			gridPay.BeginUpdate();
			gridPay.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableProcPay","Entry Date"),70,HorizontalAlignment.Center);
			gridPay.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProcPay","Amount"),55,HorizontalAlignment.Right);
			gridPay.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProcPay","Tot Amt"),55,HorizontalAlignment.Right);
			gridPay.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProcPay","Note"),250,HorizontalAlignment.Left);
			gridPay.Columns.Add(col);
			gridPay.Rows.Clear();
			ODGridRow row;
			Payment PaymentCur;//used in loop
			for(int i=0;i<PaySplitsForProc.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(((PaySplit)PaySplitsForProc[i]).DatePay.ToShortDateString());
				row.Cells.Add(((PaySplit)PaySplitsForProc[i]).SplitAmt.ToString("F"));
				row.Cells[row.Cells.Count-1].Bold=YN.Yes;
				PaymentCur=Payments.GetFromList(((PaySplit)PaySplitsForProc[i]).PayNum,PaymentsForProc);
				row.Cells.Add(PaymentCur.PayAmt.ToString("F"));
				row.Cells.Add(PaymentCur.PayNote);
				gridPay.Rows.Add(row);
			}
			gridPay.EndUpdate();
		}

		private void gridPay_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			Payment PaymentCur=Payments.GetFromList(((PaySplit)PaySplitsForProc[e.Row]).PayNum,PaymentsForProc);
			FormPayment FormP=new FormPayment(PatCur,FamCur,PaymentCur);
			FormP.InitialPaySplit=((PaySplit)PaySplitsForProc[e.Row]).SplitNum;
			FormP.ShowDialog();
			FillPayments();
		}

		private void FillAdj(){
			Adjustment[] AdjustmentList=Adjustments.Refresh(ProcCur.PatNum);
			AdjustmentsForProc=Adjustments.GetForProc(ProcCur.ProcNum,AdjustmentList);
			gridAdj.BeginUpdate();
			gridAdj.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableProcAdj","Entry Date"),70,HorizontalAlignment.Center);
			gridAdj.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProcAdj","Amount"),55,HorizontalAlignment.Right);
			gridAdj.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProcAdj","Type"),100,HorizontalAlignment.Left);
			gridAdj.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProcAdj","Note"),250,HorizontalAlignment.Left);
			gridAdj.Columns.Add(col);
			gridAdj.Rows.Clear();
			ODGridRow row;
			double discountAmt=0;//Total discount amount from all adjustments of default type.
			for(int i=0;i<AdjustmentsForProc.Count;i++){
				row=new ODGridRow();
				Adjustment adjustment=(Adjustment)AdjustmentsForProc[i];
				row.Cells.Add(adjustment.AdjDate.ToShortDateString());
				row.Cells.Add(adjustment.AdjAmt.ToString("F"));
				row.Cells[row.Cells.Count-1].Bold=YN.Yes;
				row.Cells.Add(DefC.GetName(DefCat.AdjTypes,adjustment.AdjType));
				row.Cells.Add(adjustment.AdjNote);
				gridAdj.Rows.Add(row);
				if(adjustment.AdjType==PrefC.GetLong(PrefName.TreatPlanDiscountAdjustmentType)) {
					discountAmt-=adjustment.AdjAmt;//Discounts are stored as negatives, we want a positive discount value.
				}
			}
			gridAdj.EndUpdate();
			//Because we keep the discount field in sync with the discount adjustment when the procedure has a status of TP,
			//we considered it a bug that the opposite didn't happen once the procedure was set complete.
			if(ProcCur.ProcStatus==ProcStat.C) {
				//Updating the discount text box will cause the procedure to get updated if the user clicks OK.
				//This is fine because the Discount column is not designed for accuracy (after being set complete) and is loosely kept updated.
				textDiscount.Text=discountAmt.ToString("F");//Calculated based on all adjustments of type if complete
			}
		}

		private void gridAdj_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormAdjust FormA=new FormAdjust(PatCur,(Adjustment)AdjustmentsForProc[e.Row]);
			FormA.ShowDialog();
			FillAdj();
		}

		private void butAddAdjust_Click(object sender, System.EventArgs e) {
			if(ProcCur.ProcStatus!=ProcStat.C){
				MsgBox.Show(this,"Adjustments may only be added to completed procedures.");
				return;
			}
			Adjustment adj=new Adjustment();
			adj.PatNum=PatCur.PatNum;
			adj.ProvNum=ProcCur.ProvNum;
			adj.DateEntry=DateTime.Today;//but will get overwritten to server date
			adj.AdjDate=DateTime.Today;
			adj.ProcDate=ProcCur.ProcDate;
			adj.ProcNum=ProcCur.ProcNum;
			adj.ClinicNum=ProcCur.ClinicNum;
			FormAdjust FormA=new FormAdjust(PatCur,adj);
			FormA.IsNew=true;
			FormA.ShowDialog();
			FillAdj();
		}

		private void textProcFee_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
			if(textProcFee.errorProvider1.GetError(textProcFee)!=""){
				return;
			}
			double procFee;
			if(textProcFee.Text==""){
				procFee=0;
			}
			else{
				procFee=PIn.Double(textProcFee.Text);
			}
			if(ProcCur.ProcFee==procFee){
				return;
			}
			ProcCur.ProcFee=procFee;
			Procedures.ComputeEstimates(ProcCur,PatCur.PatNum,ClaimProcsForProc,false,PlanList,PatPlanList,BenefitList,PatCur.Age,SubList);
			FillIns();
		}

		private void textTooth_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
			textTooth.Text=textTooth.Text.ToUpper();
			if(!Tooth.IsValidEntry(textTooth.Text))
				errorProvider2.SetError(textTooth,Lan.g(this,"Invalid tooth number."));
			else
				errorProvider2.SetError(textTooth,"");
		}

		private void textSurfaces_TextChanged(object sender, System.EventArgs e) {
			int cursorPos = textSurfaces.SelectionStart;
			textSurfaces.Text=textSurfaces.Text.ToUpper();
			textSurfaces.SelectionStart=cursorPos;
		}

		private void textSurfaces_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
			if(Tooth.IsValidEntry(textTooth.Text)){
				textSurfaces.Text=Tooth.SurfTidyForDisplay(textSurfaces.Text,Tooth.FromInternat(textTooth.Text));
			}
			else{
				textSurfaces.Text=Tooth.SurfTidyForDisplay(textSurfaces.Text,"");
			}
			if(textSurfaces.Text=="")
				errorProvider2.SetError(textSurfaces,"No surfaces selected.");
			else
				errorProvider2.SetError(textSurfaces,"");
		}

		private void listBoxTeeth2_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
		  listBoxTeeth.SelectedIndex=-1;
		}

		private void listBoxTeeth_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
		  listBoxTeeth2.SelectedIndex=-1;
		}

		private void butChange_Click(object sender, System.EventArgs e) {
			FormProcCodes FormP=new FormProcCodes();
      FormP.IsSelectionMode=true;
      FormP.ShowDialog();
      if(FormP.DialogResult!=DialogResult.OK){
				return;
			}
			ProcedureCode procCodeOld=ProcedureCodes.GetProcCode(ProcCur.CodeNum);
			ProcedureCode procCodeNew=ProcedureCodes.GetProcCode(FormP.SelectedCodeNum);
			if(procCodeOld.TreatArea != procCodeNew.TreatArea) {
				MsgBox.Show(this,"Not allowed due to treatment area mismatch.");
				return;
			}
      ProcCur.CodeNum=FormP.SelectedCodeNum;
      ProcedureCode2=ProcedureCodes.GetProcCode(FormP.SelectedCodeNum);
      textDesc.Text=ProcedureCode2.Descript;
			long priSubNum=PatPlans.GetInsSubNum(PatPlanList,1);
			InsSub prisub=InsSubs.GetSub(priSubNum,SubList);//can handle an inssubnum=0
			//long priPlanNum=PatPlans.GetPlanNum(PatPlanList,1);
			InsPlan priplan=InsPlans.GetPlan(prisub.PlanNum,PlanList);//can handle a plannum=0
			double insfee=Fees.GetAmount0(ProcCur.CodeNum,Fees.GetFeeSched(PatCur,PlanList,PatPlanList,SubList),ProcCur.ClinicNum,ProcCur.ProvNum);
			if(priplan!=null && priplan.PlanType=="p") {//PPO
				double standardfee=Fees.GetAmount0(ProcCur.CodeNum,Providers.GetProv(Patients.GetProvNum(PatCur)).FeeSched,ProcCur.ClinicNum,ProcCur.ProvNum);
				if(standardfee>insfee) {
					ProcCur.ProcFee=standardfee;
				}
				else {
					ProcCur.ProcFee=insfee;
				}
			}
			else {
				ProcCur.ProcFee=insfee;
			}
			switch(ProcedureCode2.TreatArea){ 
				case TreatmentArea.Quad:
					ProcCur.Surf="UR";
					radioUR.Checked=true;
					break;
				case TreatmentArea.Sextant:
					ProcCur.Surf="1";
					radioS1.Checked=true;
					break;
				case TreatmentArea.Arch:
					ProcCur.Surf="U";
					radioU.Checked=true;
					break;
			}
			for(int i=0;i<ClaimProcsForProc.Count;i++) {
				if(ClaimProcsForProc[i].ClaimPaymentNum!=0) {
					continue;//this shouldn't be possible, but it's a good check to make.
				}
				ClaimProcs.Delete(ClaimProcsForProc[i]);//that way, completely new ones will be added back, and NoBillIns will be accurate.
			}
			ClaimProcsForProc=new List<ClaimProc>();
			Procedures.ComputeEstimates(ProcCur,PatCur.PatNum,ClaimProcsForProc,false,PlanList,PatPlanList,BenefitList,PatCur.Age,SubList);
			FillIns();
      SetControlsUpperLeft();
		}

		private void butEditAnyway_Click(object sender, System.EventArgs e) {
			DateTime dateOldestClaim=Procedures.GetOldestClaimDate(ClaimProcsForProc);
			if(!Security.IsAuthorized(Permissions.ClaimSentEdit,dateOldestClaim)) {
				return;
			}
			panel1.Enabled=true;
			comboProcStatus.Enabled=true;
			checkNoBillIns.Enabled=true;
			butDelete.Enabled=true;
			butSetComplete.Enabled=true;
			//butChange.Enabled=true;//No. We no longer allow this because part of "change" is to delete all the claimprocs.  This is a terrible idea for a completed proc attached to a claim.
			//checkIsCovIns.Enabled=true;
		}

		private void listProcStatus_Click(object sender,EventArgs e) {
			
		}

		private void comboProcStatus_SelectionChangeCommitted(object sender,EventArgs e) {
			//status cannot be changed for completed procedures attached to a claim, except we allow changing status for preauths.
			//cannot edit status for TPi procedures.
			if(Procedures.IsAttachedToClaim(ProcOld,ClaimProcsForProc,false) && ProcOld.ProcStatus==ProcStat.C) {
				MsgBox.Show(this,"This is a completed procedure that is attached to a claim.  You must remove the procedure from the claim"+
					" or delete the claim before editing the status.");
				comboProcStatus.SelectedIndex=1;//Complete
				return;
			}
			if(comboProcStatus.SelectedIndex==0) {//fee starts out 0 if EO, EC, etc.  This updates fee if changing to TP so it won't stay 0.
				ProcCur.ProcStatus=ProcStat.TP;
				if(ProcCur.ProcFee==0) {
					double insfee=0;
					bool isMed=false;
					if(ProcCur.MedicalCode!=null && ProcCur.MedicalCode!="") {
						isMed=true;
					}
					//Get fee schedule for medical or dental.
					long feeSch;
					if(isMed) {
						feeSch=Fees.GetMedFeeSched(PatCur,PlanList,PatPlanList,SubList);
					}
					else {
						feeSch=Fees.GetFeeSched(PatCur,PlanList,PatPlanList,SubList);
					}
					//Get the fee amount for medical or dental.
					if(PrefC.GetBool(PrefName.MedicalFeeUsedForNewProcs) && isMed) {
						insfee=Fees.GetAmount0(ProcedureCodes.GetProcCode(ProcCur.MedicalCode).CodeNum,feeSch,ProcCur.ClinicNum,ProcCur.ProvNum);
					}
					else {
						insfee=Fees.GetAmount0(ProcCur.CodeNum,feeSch,ProcCur.ClinicNum,ProcCur.ProvNum);
					}
					InsPlan priplan=null;
					InsSub prisub=null;
					if(PatPlanList.Count>0) {
						prisub=InsSubs.GetSub(PatPlanList[0].InsSubNum,SubList);
						priplan=InsPlans.GetPlan(prisub.PlanNum,PlanList);
					}
					if(priplan!=null && priplan.PlanType=="p") {//PPO
						double standardfee=Fees.GetAmount0(ProcCur.CodeNum,Providers.GetProv(Patients.GetProvNum(PatCur)).FeeSched,ProcCur.ClinicNum,ProcCur.ProvNum);
						if(standardfee>insfee) {
							ProcCur.ProcFee=standardfee;
						}
						else {
							ProcCur.ProcFee=insfee;
						}
					}
					else {
						ProcCur.ProcFee=insfee;
					}
					textProcFee.Text=ProcCur.ProcFee.ToString("f");
				}
			}
			if(comboProcStatus.SelectedIndex==1){//C
				if(!Security.IsAuthorized(Permissions.ProcComplCreate)) {
					//set it back to whatever it was before
					if(ProcCur.ProcStatus==ProcStat.TP) {
						comboProcStatus.SelectedIndex=0;
					}
					else if(PrefC.GetBool(PrefName.EasyHideClinical)) {
						comboProcStatus.SelectedIndex=-1;//original status must not be visible
					}
					else {
						if(ProcCur.ProcStatus==ProcStat.EC) {
							comboProcStatus.SelectedIndex=2;
						}
						if(ProcCur.ProcStatus==ProcStat.EO) {
							comboProcStatus.SelectedIndex=3;
						}
						if(ProcCur.ProcStatus==ProcStat.R) {
							comboProcStatus.SelectedIndex=4;
						}
						if(ProcCur.ProcStatus==ProcStat.Cn) {
							comboProcStatus.SelectedIndex=5;
						}
					}
					return;
				}
				if(ProcCur.AptNum!=0) {//if attached to an appointment
					Appointment apt=Appointments.GetOneApt(ProcCur.AptNum);
					if(apt.AptDateTime.Date > MiscData.GetNowDateTime().Date) {//if appointment is in the future
						MessageBox.Show(Lan.g(this,"Not allowed because procedure is attached to a future appointment with a date of ")
							+apt.AptDateTime.ToShortDateString());
						return;
					}
					if(apt.AptDateTime.Year<1880) {
						textDate.Text=MiscData.GetNowDateTime().ToShortDateString();
					}
					else {
						textDate.Text=apt.AptDateTime.ToShortDateString();
					}
				}
				else {
					textDate.Text=MiscData.GetNowDateTime().ToShortDateString();
				}
				Procedures.SetDateFirstVisit(DateTimeOD.Today,2,PatCur);
				ProcCur.ProcStatus=ProcStat.C;
			}
			if(comboProcStatus.SelectedIndex==2) {
				ProcCur.ProcStatus=ProcStat.EC;
			}
			if(comboProcStatus.SelectedIndex==3) {
				ProcCur.ProcStatus=ProcStat.EO;
			}
			if(comboProcStatus.SelectedIndex==4) {
				ProcCur.ProcStatus=ProcStat.R;
			}
			if(comboProcStatus.SelectedIndex==5) {
				ProcCur.ProcStatus=ProcStat.Cn;
			}
			//If it's already locked, there's simply no way to save the changes made to this control.
			//If status was just changed to C, then we should show the lock button.
			if(ProcCur.ProcStatus==ProcStat.C) {
				if(PrefC.GetBool(PrefName.ProcLockingIsAllowed) && !ProcCur.IsLocked) {
					butLock.Visible=true;
				}
			}
		}

		private void butSetComplete_Click(object sender, System.EventArgs e) {
			//can't get to here if attached to a claim, even if use the Edit Anyway button.
			if(ProcOld.ProcStatus==ProcStat.C){
				MsgBox.Show(this,"Procedure was already set complete.");
				return;
			}
			if(!Security.IsAuthorized(Permissions.ProcComplCreate)){
				return;
			}
			//If user is trying to change status to complete and using eCW.
			if((IsNew || ProcOld.ProcStatus!=ProcStat.C) && Programs.UsingEcwTightOrFullMode()) {
				MsgBox.Show(this,"Procedures cannot be set complete in this window.  Set the procedure complete by setting the appointment complete.");
				return;
			}
			Procedures.SetDateFirstVisit(DateTimeOD.Today,2,PatCur);
			if(ProcCur.AptNum!=0){//if attached to an appointment
				Appointment apt=Appointments.GetOneApt(ProcCur.AptNum);
				if(apt.AptDateTime.Date > MiscData.GetNowDateTime().Date){//if appointment is in the future
					MessageBox.Show(Lan.g(this,"Not allowed because procedure is attached to a future appointment with a date of ")
						+apt.AptDateTime.ToShortDateString());
					return;
				}
				if(apt.AptDateTime.Year<1880) {
					textDate.Text=MiscData.GetNowDateTime().ToShortDateString();
				}
				else {
					textDate.Text=apt.AptDateTime.ToShortDateString();
				}
			}
			else{
				textDate.Text=MiscData.GetNowDateTime().ToShortDateString();
			}
			if(ProcedureCode2.PaintType==ToothPaintingType.Extraction){//if an extraction, then mark previous procs hidden
				//Procedures.SetHideGraphical(ProcCur);//might not matter anymore
				ToothInitials.SetValue(ProcCur.PatNum,ProcCur.ToothNum,ToothInitialType.Missing);
			}
			//long provNum=ProviderC.List[comboProvNum.SelectedIndex].ProvNum;
			long provNum=ProcCur.ProvNum;
			textNotes.Text+=ProcCodeNotes.GetNote(provNum,ProcCur.CodeNum);
			Plugins.HookAddCode(this,"FormProcEdit.butSetComplete_Click_end",ProcCur,ProcOld,textNotes);
			comboProcStatus.SelectedIndex=-1;
			//radioStatusC.Checked=true;
			ProcCur.ProcStatus=ProcStat.C;
			ProcCur.SiteNum=PatCur.SiteNum;
			comboPlaceService.SelectedIndex=PrefC.GetInt(PrefName.DefaultProcedurePlaceService);
			if(EntriesAreValid()){
				SaveAndClose();
			}
		}

		private void radioUR_Click(object sender, System.EventArgs e) {
			ProcCur.Surf="UR";
		}

		private void radioUL_Click(object sender, System.EventArgs e) {
			ProcCur.Surf="UL";
		}

		private void radioLR_Click(object sender, System.EventArgs e) {
			ProcCur.Surf="LR";
		}

		private void radioLL_Click(object sender, System.EventArgs e) {
			ProcCur.Surf="LL";
		}

		private void radioU_Click(object sender, System.EventArgs e) {
			ProcCur.Surf="U";
		}

		private void radioL_Click(object sender, System.EventArgs e) {
			ProcCur.Surf="L";
		}

		private void radioS1_Click(object sender, System.EventArgs e) {
			ProcCur.Surf="1";
		}

		private void radioS2_Click(object sender, System.EventArgs e) {
			ProcCur.Surf="2";
		}

		private void radioS3_Click(object sender, System.EventArgs e) {
			ProcCur.Surf="3";
		}

		private void radioS4_Click(object sender, System.EventArgs e) {
			ProcCur.Surf="4";
		}

		private void radioS5_Click(object sender, System.EventArgs e) {
			ProcCur.Surf="5";
		}

		private void radioS6_Click(object sender, System.EventArgs e) {
			ProcCur.Surf="6";
		}

		private void checkNoBillIns_Click(object sender, System.EventArgs e) {
			if(checkNoBillIns.CheckState==CheckState.Indeterminate){
				//not allowed to set to indeterminate, so move on
				checkNoBillIns.CheckState=CheckState.Unchecked;
			}
			for(int i=0;i<ClaimProcsForProc.Count;i++) {
				//ignore CapClaim,NotReceived,PreAuth,Received,Supplemental
				if(ClaimProcsForProc[i].Status==ClaimProcStatus.Estimate
					|| ClaimProcsForProc[i].Status==ClaimProcStatus.CapComplete
					|| ClaimProcsForProc[i].Status==ClaimProcStatus.CapEstimate)
				{
					if(checkNoBillIns.CheckState==CheckState.Checked){
						ClaimProcsForProc[i].NoBillIns=true;
					}
					else{//unchecked
						ClaimProcsForProc[i].NoBillIns=false;
					}
					ClaimProcs.Update(ClaimProcsForProc[i]);
				}
			}
			//next line is needed to recalc BaseEst, etc, for claimprocs that are no longer NoBillIns
			//also, if they are NoBillIns, then it clears out the other values.
			Procedures.ComputeEstimates(ProcCur,PatCur.PatNum,ClaimProcsForProc,false,PlanList,PatPlanList,BenefitList,PatCur.Age,SubList);
			FillIns();
		}

		private void textNotes_TextChanged(object sender, System.EventArgs e) {
			CheckForCompleteNote();
			if(!IsStartingUp//so this happens only if user changes the note
				&& !SigChanged)//and the original signature is still showing.
			{
				sigBox.ClearTablet();
				//if(allowTopaz){
					CodeBase.TopazWrapper.ClearTopaz(sigBoxTopaz);
					sigBoxTopaz.Visible=false;//until user explicitly starts it.
				//}
				sigBox.SetTabletState(1);//on-screen box is now accepting input.
				SigChanged=true;
				ProcCur.UserNum=Security.CurUser.UserNum;
				textUser.Text=Userods.GetName(ProcCur.UserNum);
			}
		}

		private void CheckForCompleteNote(){
			if(textNotes.Text.IndexOf("\"\"")==-1){
				//no occurances of ""
				labelIncomplete.Visible=false;
			}
			else{
				labelIncomplete.Visible=true;
			}
		}

		private void butSearch_Click(object sender,EventArgs e) {
			InputBox input=new InputBox(Lan.g(this,"Search for"));
			input.ShowDialog();
			if(input.DialogResult!=DialogResult.OK) {
				return;
			}
			string searchText=input.textResult.Text;
			int index=textNotes.Find(input.textResult.Text);//Gets the location of the first character in the control.
			if(index<0) {//-1 is returned when the text is not found.
				textNotes.DeselectAll();
				MessageBox.Show("\""+searchText+"\"\r\n"+Lan.g(this,"was not found in the notes")+".");
				return;
			}
			textNotes.Select(index,searchText.Length);
		}

		private void butClearSig_Click(object sender,EventArgs e) {
			sigBox.ClearTablet();
			sigBox.Visible=true;
			//if(allowTopaz) {
				CodeBase.TopazWrapper.ClearTopaz(sigBoxTopaz);
				sigBoxTopaz.Visible=false;//until user explicitly starts it.
			//}
			sigBox.SetTabletState(1);//on-screen box is now accepting input.
			SigChanged=true;
			labelInvalidSig.Visible=false;
			ProcCur.UserNum=Security.CurUser.UserNum;
			textUser.Text=Userods.GetName(ProcCur.UserNum);
		}

		private void butTopazSign_Click(object sender,EventArgs e) {
			sigBox.Visible=false;
			sigBoxTopaz.Visible=true;
			sigBoxTopaz.Focus();//If the Topaz signature box does not have focus, the leave event will not work correctly.
			//if(allowTopaz){
				CodeBase.TopazWrapper.ClearTopaz(sigBoxTopaz); 
				CodeBase.TopazWrapper.SetTopazEncryptionMode(sigBoxTopaz,0);
				CodeBase.TopazWrapper.SetTopazState(sigBoxTopaz,1);
			//}
			SigChanged=true;
			labelInvalidSig.Visible=false;
			ProcCur.UserNum=Security.CurUser.UserNum;
			textUser.Text=Userods.GetName(ProcCur.UserNum);
		}

		private void sigBox_MouseUp(object sender,MouseEventArgs e) {
			//this is done on mouse up so that the initial pen capture won't be delayed.
			if(sigBox.GetTabletState()==1//if accepting input.
				&& !SigChanged)//and sig not changed yet
			{
				//sigBox handles its own pen input.
				SigChanged=true;
				ProcCur.UserNum=Security.CurUser.UserNum;
				textUser.Text=Userods.GetName(ProcCur.UserNum);
			}
		}

		private void buttonUseAutoNote_Click(object sender,EventArgs e) {
			FormAutoNoteCompose FormA=new FormAutoNoteCompose();
			FormA.ShowDialog();
			if(FormA.DialogResult==DialogResult.OK) {
				textNotes.AppendText(FormA.CompletedNote);
			}
		}

		/*private void butShowMedical_Click(object sender,EventArgs e) {
			if(groupMedical.Height<200) {
				groupMedical.Height=200;
				butShowMedical.Text="^";
			}
			else {
				groupMedical.Height=170;
				butShowMedical.Text="V";
			}
		}*/

		private void comboDPC_SelectionChangeCommitted(object sender,EventArgs e) {
			DateTime tempDate=PIn.Date(textDateTP.Text);
			switch(comboDPC.SelectedIndex) {
				case 2:
					tempDate=tempDate.Date.AddDays(1);
					if(CancelledScheduleByDate.Year>1880 && CancelledScheduleByDate<tempDate) {
						tempDate=CancelledScheduleByDate;
					}
					break;
				case 3:
					tempDate=tempDate.Date.AddDays(30);
					if(CancelledScheduleByDate.Year>1880 && CancelledScheduleByDate<tempDate) {
						tempDate=CancelledScheduleByDate;
					}
					break;
				case 4:
					tempDate=tempDate.Date.AddDays(60);
					if(CancelledScheduleByDate.Year>1880 && CancelledScheduleByDate<tempDate) {
						tempDate=CancelledScheduleByDate;
					}
					break;
				case 5:
					tempDate=tempDate.Date.AddDays(120);
					if(CancelledScheduleByDate.Year>1880 && CancelledScheduleByDate<tempDate) {
						tempDate=CancelledScheduleByDate;
					}
					break;
				case 6:
					tempDate=tempDate.Date.AddYears(1);
					if(CancelledScheduleByDate.Year>1880 && CancelledScheduleByDate<tempDate) {
						tempDate=CancelledScheduleByDate;
					}
					break;
			}
			textDateScheduled.Text=tempDate.ToShortDateString();
			labelScheduleBy.Visible=false;
			if(comboDPC.SelectedIndex==0
				|| comboDPC.SelectedIndex==1
				|| comboDPC.SelectedIndex==7
				|| comboDPC.SelectedIndex==8) {
				textDateScheduled.Text="";
				labelScheduleBy.Visible=true;
			}
		}

		private void comboStatus_SelectionChangeCommitted(object sender,EventArgs e) {
			switch(comboStatus.SelectedIndex) {
				case 0:
					comboProcStatus.SelectedIndex=0;
					ProcCur.ProcStatus=ProcStat.TP;
					break;
				case 1:
					if(!Security.IsAuthorized(Permissions.ProcComplCreate)) {
						//set it back to whatever it was before
						if(OrionProcCur.Status2==OrionStatus.TP) {
							comboStatus.SelectedIndex=0;
						}
						if(OrionProcCur.Status2==OrionStatus.E) {
							comboStatus.SelectedIndex=2;
						}
						if(OrionProcCur.Status2==OrionStatus.R) {
							comboStatus.SelectedIndex=3;
						}
						if(OrionProcCur.Status2==OrionStatus.RO) {
							comboStatus.SelectedIndex=4;
						}
						if(OrionProcCur.Status2==OrionStatus.CS) {
							comboStatus.SelectedIndex=5;
						}
						if(OrionProcCur.Status2==OrionStatus.CR) {
							comboStatus.SelectedIndex=6;
						}
						if(OrionProcCur.Status2==OrionStatus.CA_Tx) {
							comboStatus.SelectedIndex=7;
						}
						if(OrionProcCur.Status2==OrionStatus.CA_EPRD) {
							comboStatus.SelectedIndex=8;
						}
						if(OrionProcCur.Status2==OrionStatus.CA_PD) {
							comboStatus.SelectedIndex=9;
						}
						if(OrionProcCur.Status2==OrionStatus.S) {
							comboStatus.SelectedIndex=10;
						}
						if(OrionProcCur.Status2==OrionStatus.ST) {
							comboStatus.SelectedIndex=11;
						}
						if(OrionProcCur.Status2==OrionStatus.W) {
							comboStatus.SelectedIndex=12;
						}
						if(OrionProcCur.Status2==OrionStatus.A) {
							comboStatus.SelectedIndex=13;
						}
						return;
					}
					comboProcStatus.SelectedIndex=1;
					ProcCur.ProcStatus=ProcStat.C;
					textTimeStart.Text=MiscData.GetNowDateTime().ToShortTimeString();
					break;
				case 2:
					comboProcStatus.SelectedIndex=3;
					ProcCur.ProcStatus=ProcStat.EO;
					break;
				case 3:
					comboProcStatus.SelectedIndex=0;
					ProcCur.ProcStatus=ProcStat.TP;
					break;
				case 4:
					comboProcStatus.SelectedIndex=4;
					ProcCur.ProcStatus=ProcStat.R;
					break;
				case 5:
					comboProcStatus.SelectedIndex=3;
					ProcCur.ProcStatus=ProcStat.EO;
					break;
				case 6:
					comboProcStatus.SelectedIndex=3;
					ProcCur.ProcStatus=ProcStat.EO;
					break;
				case 7:
					comboProcStatus.SelectedIndex=0;
					ProcCur.ProcStatus=ProcStat.TP;
					break;
				case 8:
					comboProcStatus.SelectedIndex=0;
					ProcCur.ProcStatus=ProcStat.TP;
					break;
				case 9:
					comboProcStatus.SelectedIndex=0;
					ProcCur.ProcStatus=ProcStat.TP;
					break;
				case 10:
					comboProcStatus.SelectedIndex=0;
					ProcCur.ProcStatus=ProcStat.TP;
					break;
				case 11:
					comboProcStatus.SelectedIndex=0;
					ProcCur.ProcStatus=ProcStat.TP;
					break;
				case 12:
					comboProcStatus.SelectedIndex=5;
					ProcCur.ProcStatus=ProcStat.Cn;
					break;
				case 13:
					comboProcStatus.SelectedIndex=0;
					ProcCur.ProcStatus=ProcStat.TP;
					break;
			}
			OrionProcCur.Status2=(OrionStatus)((int)(Math.Pow(2d,(double)(comboStatus.SelectedIndex))));
			//Do not automatically set the stop clock date if status is set to treatment planned, existing, or watch.
			if(OrionProcCur.Status2==OrionStatus.TP || OrionProcCur.Status2==OrionStatus.E || OrionProcCur.Status2==OrionStatus.W) {
				//Clear the stop the clock date if there was no stop the clock date defined in a previous edit. Therefore, for a new procedure, always clear.
				if(OrionProcCur.DateStopClock.Year<1880){
					textDateStop.Text="";
				}
			}
			else {//Set the stop the clock date for all other statuses.
				//Use the previously set stop the clock date if one exists. Will never be true if this is a new procedure.
				if(OrionProcCur.DateStopClock.Year>1880) {
					textDateStop.Text=OrionProcCur.DateStopClock.ToShortDateString();
				}
				else {
					//When the stop the clock date has not already been set, set to the ProcDate for the procedure.
					textDateStop.Text=textDate.Text.Trim();
				}
			}
		}

		private void textTimeStart_TextChanged(object sender,EventArgs e) {
			UpdateFinalMin();			
		}

		private void textTimeEnd_TextChanged(object sender,EventArgs e) {
			UpdateFinalMin();
		}

		///<summary>Populates final time box with total number of minutes.</summary>
		private void UpdateFinalMin() {
			if(textTimeStart.Text=="" || textTimeEnd.Text=="") {
				return;
			}
			int startTime=0;
			int stopTime=0;
			try {
				startTime=PIn.Int(textTimeStart.Text);
			}
			catch { 
				try {//Try DateTime format.
					DateTime sTime=DateTime.Parse(textTimeStart.Text);
					startTime=(sTime.Hour*(int)Math.Pow(10,2))+sTime.Minute;
				}
				catch {//Not a valid time.
					return;
				}
			}
			try {
				stopTime=PIn.Int(textTimeEnd.Text);
			}
			catch { 
				try {//Try DateTime format.
					DateTime eTime=DateTime.Parse(textTimeEnd.Text);
					stopTime=(eTime.Hour*(int)Math.Pow(10,2))+eTime.Minute;
				}
				catch {//Not a valid time.
					return;
				}
			}
			int total=(((stopTime/100)*60)+(stopTime%100))-(((startTime/100)*60)+(startTime%100));
			textTimeFinal.Text=total.ToString();
		}

		///<summary>Empty string is considered valid.</summary>
		private bool ValidateTime(string time) {
			string militaryTime=time;
			if(militaryTime=="") {
				return true;
			}
			if(militaryTime.Length<4) {
				militaryTime=militaryTime.PadLeft(4,'0');
			}
			//Test if user typed in military time. Ex: 0830 or 1536
			try {
				int hour=PIn.Int(militaryTime.Substring(0,2));
				int minute=PIn.Int(militaryTime.Substring(2,2));
				if(hour>23) {
					return false;
				}
				if(minute>59) {
					return false;
				}
				return true;
			}
			catch { }
			//Test typical DateTime format. Ex: 1:00 PM
			try { 
				DateTime.Parse(time);
				return true;
			}
			catch { 
				return false;
			}
		}

		///<summary>Returns min value if blank or invalid string passed in.</summary>
		private DateTime ParseTime(string time) {
			string militaryTime=time;
			DateTime dTime=DateTime.MinValue;
			if(militaryTime=="") {
				return dTime;
			}
			if(militaryTime.Length<4) {
				militaryTime=militaryTime.PadLeft(4,'0');
			}
			//Test if user typed in military time. Ex: 0830 or 1536
			try {
				int hour=PIn.Int(militaryTime.Substring(0,2));
				int minute=PIn.Int(militaryTime.Substring(2,2));
				dTime=new DateTime(1,1,1,hour,minute,0);
				return dTime;
			}
			catch { }
			//Test if user typed in a typical DateTime format. Ex: 1:00 PM
			try { 
				return DateTime.Parse(time);
			}
			catch { }
			return dTime;
		}

		private void UpdateSurf() {
			if(!Tooth.IsValidEntry(textTooth.Text)){
				return;
			}
			errorProvider2.SetError(textSurfaces,"");
			textSurfaces.Text="";
			if(butM.BackColor==Color.White) {
				textSurfaces.AppendText("M");
			}
			if(butOI.BackColor==Color.White) {
				//if(ToothGraphic.IsAnterior(Tooth.FromInternat(textTooth.Text))) {
				if(Tooth.IsAnterior(Tooth.FromInternat(textTooth.Text))) {
					textSurfaces.AppendText("I");
				}
				else {
					textSurfaces.AppendText("O");
				}
			}
			if(butD.BackColor==Color.White) {
				textSurfaces.AppendText("D");
			}
			if(butV.BackColor==Color.White) {
				if(CultureInfo.CurrentCulture.Name.EndsWith("CA")) {//Canadian. en-CA or fr-CA
					textSurfaces.AppendText("5");
				}
				else {
					textSurfaces.AppendText("V");
				}
			}
			if(butBF.BackColor==Color.White) {
				//if(ToothGraphic.IsAnterior(Tooth.FromInternat(textTooth.Text))) {
				if(Tooth.IsAnterior(Tooth.FromInternat(textTooth.Text))) {
					if(CultureInfo.CurrentCulture.Name.EndsWith("CA")) {//Canadian. en-CA or fr-CA
						textSurfaces.AppendText("V");//vestibular
					}
					else {
						textSurfaces.AppendText("F");
					}
				}
				else {
					textSurfaces.AppendText("B");
				}
			}
			if(butL.BackColor==Color.White) {
				textSurfaces.AppendText("L");
			}
		}

		private void butM_Click(object sender,EventArgs e) {
			if(butM.BackColor==Color.White) {
				butM.BackColor=SystemColors.Control;
			}
			else {
				butM.BackColor=Color.White;
			}
			UpdateSurf();
		}

		private void butOI_Click(object sender,EventArgs e) {
			if(butOI.BackColor==Color.White) {
				butOI.BackColor=SystemColors.Control;
			}
			else {
				butOI.BackColor=Color.White;
			}
			UpdateSurf();
		}

		private void butL_Click(object sender,EventArgs e) {
			if(butL.BackColor==Color.White) {
				butL.BackColor=SystemColors.Control;
			}
			else {
				butL.BackColor=Color.White;
			}
			UpdateSurf();
		}

		private void butV_Click(object sender,EventArgs e) {
			if(butV.BackColor==Color.White) {
				butV.BackColor=SystemColors.Control;
			}
			else {
				butV.BackColor=Color.White;
			}
			UpdateSurf();
		}

		private void butBF_Click(object sender,EventArgs e) {
			if(butBF.BackColor==Color.White) {
				butBF.BackColor=SystemColors.Control;
			}
			else {
				butBF.BackColor=Color.White;
			}
			UpdateSurf();
		}

		private void butD_Click(object sender,EventArgs e) {
			if(butD.BackColor==Color.White) {
				butD.BackColor=SystemColors.Control;
			}
			else {
				butD.BackColor=Color.White;
			}
			UpdateSurf();
		}

		private void butPickSite_Click(object sender,EventArgs e) {
			FormSites FormS=new FormSites();
			FormS.IsSelectionMode=true;
			FormS.SelectedSiteNum=ProcCur.SiteNum;
			FormS.ShowDialog();
			if(FormS.DialogResult!=DialogResult.OK){
				return;
			}
			ProcCur.SiteNum=FormS.SelectedSiteNum;
			textSite.Text=Sites.GetDescription(ProcCur.SiteNum);
		}

		private void butPickProv_Click(object sender,EventArgs e) {
			FormProviderPick formp=new FormProviderPick();
			if(comboProvNum.SelectedIndex > -1) {
				formp.SelectedProvNum=ProviderC.ListShort[comboProvNum.SelectedIndex].ProvNum;
			}
			formp.ShowDialog();
			if(formp.DialogResult!=DialogResult.OK) {
				return;
			}
			comboProvNum.SelectedIndex=Providers.GetIndex(formp.SelectedProvNum);
			ProcCur.ProvNum=formp.SelectedProvNum;
		}

		private void comboProvNum_SelectionChangeCommitted(object sender,EventArgs e) {
			ProcCur.ProvNum=ProviderC.ListShort[comboProvNum.SelectedIndex].ProvNum;
			UpdateClaimProcsProv();//This seems unnecessary here.  Should probably move to OK click.
		}

		///<summary>Changes provider if user uses key shortcuts to change the dropdown.</summary>
		private void comboProvNum_KeyUp(object sender,KeyEventArgs e) {
			if(comboProvNum.SelectedIndex==-1) { //If loaded with a provider not in the list, and the provider wasn't changed
				return;
			}
			ProcCur.ProvNum=ProviderC.ListShort[comboProvNum.SelectedIndex].ProvNum;
			UpdateClaimProcsProv();//This seems unnecessary here.  Should probably move to OK click.
		}

		///<summary>Updates all claim procs for procedure based on ProcCur.ProvNum.</summary>
		private void UpdateClaimProcsProv() {
			for(int i=0;i<ClaimProcsForProc.Count;i++) {
				ClaimProcsForProc[i].ProvNum=ProcCur.ProvNum;
			}
		}

		///<summary>This button is only visible if 1. Pref ProcLockingIsAllowed is true, 2. Proc isn't already locked, 3. Proc status is C.</summary>
		private void butLock_Click(object sender,EventArgs e) {
			if(!EntriesAreValid()) {
				return;
			}
			ProcCur.IsLocked=true;
			SaveAndClose();//saves all the other various changes that the user made
			DialogResult=DialogResult.OK;
		}

		///<summary>This button is only visible when proc IsLocked.</summary>
		private void butInvalidate_Click(object sender,EventArgs e) {
			//What this will really do is "delete" the procedure.
			if(!Security.IsAuthorized(Permissions.ProcComplEdit,ProcCur.DateEntryC)) {
				return;
			}
			if(Procedures.IsAttachedToClaim(ProcCur,ClaimProcsForProc)) {
				MsgBox.Show(this,"This procedure is attached to a claim and cannot be invalidated without first deleting the claim.");
				return;
			}
			try {
				Procedures.Delete(ProcCur.ProcNum);//also deletes any claimprocs (other than ins payments of course).
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
				return;
			}
			SecurityLogs.MakeLogEntry(Permissions.ProcComplEdit,PatCur.PatNum,Lan.g(this,"Invalidated: ")+
				ProcedureCodes.GetStringProcCode(ProcCur.CodeNum).ToString()+", "+ProcCur.ProcDate.ToShortDateString()+", "+ProcCur.ProcFee.ToString("c")+", Deleted");
			DialogResult=DialogResult.OK;
		}

		///<summary>This button is only visible when proc IsLocked.</summary>
		private void butAppend_Click(object sender,EventArgs e) {
			FormProcNoteAppend formPNA=new FormProcNoteAppend();
			formPNA.ProcCur=ProcCur;
			formPNA.ShowDialog();
			if(formPNA.DialogResult!=DialogResult.OK) {
				return;
			}
			DialogResult=DialogResult.OK;//exit out of this window.  Change already saved, and OK button is disabled in this window, anyway.
		}

		private void comboClinic_SelectionChangeCommitted(object sender,EventArgs e) {
			if(comboClinic.SelectedIndex==0) {
				ProcCur.ClinicNum=0;
			}
			else {
				ProcCur.ClinicNum=Clinics.List[comboClinic.SelectedIndex-1].ClinicNum;
			}
			for(int i=0;i<ClaimProcsForProc.Count;i++) {
				ClaimProcsForProc[i].ClinicNum=ProcCur.ClinicNum;
			}
		}

		private void butSnomedBodySiteSelect_Click(object sender,EventArgs e) {
			FormSnomeds formS=new FormSnomeds();
			formS.IsSelectionMode=true;
			if(formS.ShowDialog()==DialogResult.OK) {
				_snomedBodySite=formS.SelectedSnomed;
				textSnomedBodySite.Text=_snomedBodySite.Description;
			}
		}

		private void butNoneSnomedBodySite_Click(object sender,EventArgs e) {
			_snomedBodySite=null;
			textSnomedBodySite.Text="";
		}

		private void SetIcdLabels() {
			byte icdVersion=9;
			if(checkIcdVersion.Checked) {
				icdVersion=10;
			}
			labelDiagnosticCode1.Text=Lan.g(this,"ICD")+"-"+icdVersion+" "+Lan.g(this,"Diagnosis Code 1");
			labelDiagnosticCode2.Text=Lan.g(this,"ICD")+"-"+icdVersion+" "+Lan.g(this,"Diagnosis Code 2");
			labelDiagnosticCode3.Text=Lan.g(this,"ICD")+"-"+icdVersion+" "+Lan.g(this,"Diagnosis Code 3");
			labelDiagnosticCode4.Text=Lan.g(this,"ICD")+"-"+icdVersion+" "+Lan.g(this,"Diagnosis Code 4");
		}

		private void checkIcdVersion_Click(object sender,EventArgs e) {
			SetIcdLabels();
		}

		private void PickDiagnosisCode(TextBox textBoxDiagnosisCode) {
			if(checkIcdVersion.Checked) {//ICD-10
				FormIcd10s formI=new FormIcd10s();
				formI.IsSelectionMode=true;
				if(formI.ShowDialog()==DialogResult.OK) {
					textBoxDiagnosisCode.Text=formI.SelectedIcd10.Icd10Code;
				}
			}
			else {//ICD-9
				FormIcd9s formI=new FormIcd9s();
				formI.IsSelectionMode=true;
				if(formI.ShowDialog()==DialogResult.OK) {
					textBoxDiagnosisCode.Text=formI.SelectedIcd9.ICD9Code;
				}
			}
		}

		private void butDiagnosisCode1_Click(object sender,EventArgs e) {
			PickDiagnosisCode(textDiagnosticCode);
		}

		private void butNoneDiagnosisCode1_Click(object sender,EventArgs e) {
			textDiagnosticCode.Text="";
		}

		private void butDiagnosisCode2_Click(object sender,EventArgs e) {
			PickDiagnosisCode(textDiagnosticCode2);
		}

		private void butNoneDiagnosisCode2_Click(object sender,EventArgs e) {
			textDiagnosticCode2.Text="";
		}

		private void butDiagnosisCode3_Click(object sender,EventArgs e) {
			PickDiagnosisCode(textDiagnosticCode3);
		}

		private void butNoneDiagnosisCode3_Click(object sender,EventArgs e) {
			textDiagnosticCode3.Text="";
		}

		private void butDiagnosisCode4_Click(object sender,EventArgs e) {
			PickDiagnosisCode(textDiagnosticCode4);
		}

		private void butNoneDiagnosisCode4_Click(object sender,EventArgs e) {
			textDiagnosticCode4.Text="";
		}

		private void comboProvNumOrdering_SelectionChangeCommitted(object sender,EventArgs e) {
			_provNumOrderingSelected=ProviderC.ListShort[comboProvNumOrdering.SelectedIndex].ProvNum;
		}

		private void butPickProvOrdering_Click(object sender,EventArgs e) {
			FormProviderPick formP=new FormProviderPick();
			if(comboProvNumOrdering.SelectedIndex > -1) {//Initial formP selection if selected prov is not hidden.
				formP.SelectedProvNum=_provNumOrderingSelected;
			}
			formP.ShowDialog();
			if(formP.DialogResult!=DialogResult.OK) {
				return;
			}
			comboProvNumOrdering.SelectedIndex=Providers.GetIndex(formP.SelectedProvNum);
			_provNumOrderingSelected=formP.SelectedProvNum;
		}

		private void butNoneProvOrdering_Click(object sender,EventArgs e) {
			_provNumOrderingSelected=0;
			comboProvNumOrdering.SelectedIndex=-1;
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(IsNew) {
				DialogResult=DialogResult.Cancel;//verified that this triggers a delete when window closed from all places where FormProcEdit is used, and where proc could be new.
				return;
			}
			//If this is an existing completed proc, then this delete button is only enabled if the user has permission for ProcComplEdit based on the DateEntryC.
			if(ProcOld.ProcStatus!=ProcStat.C && !Security.IsAuthorized(Permissions.ProcDelete,ProcCur.DateEntryC)) {//This should be a much more lenient permission since completed procedures are already safeguarded.
				return;
			}
			if(ProcOld.ProcStatus==ProcStat.C && !Security.IsAuthorized(Permissions.ProcComplEdit,ProcOld.DateEntryC)) {
				return;
			}
			if(MessageBox.Show(Lan.g(this,"Delete Procedure?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
				return;
			}
			try{
				Procedures.Delete(ProcCur.ProcNum);//also deletes the claimProcs and adjustments. Might throw exception.
				Recalls.Synch(ProcCur.PatNum);//needs to be moved into Procedures.Delete
				if(ProcOld.ProcStatus==ProcStat.C) {
					SecurityLogs.MakeLogEntry(Permissions.ProcComplEdit,ProcOld.PatNum,ProcedureCodes.GetProcCode(ProcOld.CodeNum).ProcCode+", "+ProcOld.ProcFee.ToString("c")+", Deleted");
				}
				else {
					SecurityLogs.MakeLogEntry(Permissions.ProcDelete,ProcOld.PatNum,ProcedureCodes.GetProcCode(ProcOld.CodeNum).ProcCode+", "+ProcOld.ProcFee.ToString("c"));
				}
				DialogResult=DialogResult.OK;
				Plugins.HookAddCode(this,"FormProcEdit.butDelete_Click_end",ProcCur);
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);
			}
		}

		private bool EntriesAreValid(){
			if(  textDateTP.errorProvider1.GetError(textDateTP)!=""
				|| textDate.errorProvider1.GetError(textDate)!=""
				|| textProcFee.errorProvider1.GetError(textProcFee)!=""
				|| textDateOriginalProsth.errorProvider1.GetError(textDateOriginalProsth)!=""
				|| textDiscount.errorProvider1.GetError(textDiscount)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return false;
			}
			if(textDate.Text==""){
				MessageBox.Show(Lan.g(this,"Please enter a date first."));
				return false;
			}
			//There have been 2 or 3 cases where a customer entered a note with thousands of new lines and when OD tries to display such a note in the chart, a GDI exception occurs because the progress notes grid is very tall and takes up too much video memory. To help prevent this issue, we block the user from entering any note where there are 50 or more consecutive new lines anywhere in the note. Any number of new lines less than 50 are considered to be intentional.
			StringBuilder tooManyNewLines=new StringBuilder();
			for(int i=0;i<50;i++) {
				tooManyNewLines.Append("\r\n");
			}
			if(textNotes.Text.Contains(tooManyNewLines.ToString())) {
				MsgBox.Show(this,"The notes contain 50 or more consecutive blank lines. Probably unintentional and must be fixed.");
				return false;
			}
			if(Programs.UsingOrion || PrefC.GetBool(PrefName.ShowFeatureMedicalInsurance)) {
				if(!ValidateTime(textTimeStart.Text)) {
					MessageBox.Show(Lan.g(this,"Start time is invalid."));
					return false;
				}
				if(!ValidateTime(textTimeEnd.Text)) {
					MessageBox.Show(Lan.g(this,"End time is invalid."));
					return false;
				}
			}
			else {
				if(textTimeStart.Text!="") {
					try {
						DateTime.Parse(textTimeStart.Text);
					}
					catch {
						MessageBox.Show(Lan.g(this,"Start time is invalid."));
						return false;
					}
				}
			}
			try{
				int unitqty=int.Parse(textUnitQty.Text);
				if(unitqty<1){
					MsgBox.Show(this,"Qty not valid.  Typical value is 1.");
					return false;
				}
			}
			catch{
				MsgBox.Show(this,"Qty not valid.  Typical value is 1.");
				return false;
			}
			if(ProcCur.ProvNum==0){//this works because ProvNum gets set when the user changes the combobox.
				MsgBox.Show(this,"You must select a provider first.");
				return false;
			}
			if(errorProvider2.GetError(textSurfaces)!=""
				|| errorProvider2.GetError(textTooth)!="")
			{
				MsgBox.Show(this,"Please fix tooth number or surfaces first.");
				return false;
			}
			if(textMedicalCode.Text!="" && !ProcedureCodeC.HList.Contains(textMedicalCode.Text)){
				MsgBox.Show(this,"Invalid medical code.  It must refer to an existing procedure code.");
				return false;
			}
			if(textDrugNDC.Text!=""){
				if(comboDrugUnit.SelectedIndex==(int)EnumProcDrugUnit.None || textDrugQty.Text==""){
					if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Drug quantity and unit are not entered.  Continue anyway?")){
						return false;
					}
				}
			}
			if(textDrugQty.Text!=""){
				try{
					float.Parse(textDrugQty.Text);
				}
				catch{
					MsgBox.Show(this,"Please fix drug qty first.");
					return false;
				}
			}
			//If user is trying to change status to complete and using eCW.
			if(ProcCur.ProcStatus==ProcStat.C && (IsNew || ProcOld.ProcStatus!=ProcStat.C) && Programs.UsingEcwTightOrFullMode()) {
				MsgBox.Show(this,"Procedures cannot be set complete in this window.  Set the procedure complete by setting the appointment complete.");
				return false;
			}
			if(ProcOld.ProcStatus!=ProcStat.C && ProcCur.ProcStatus==ProcStat.C){//if status was changed to complete
				if(ProcCur.AptNum!=0) {//if attached to an appointment
					Appointment apt=Appointments.GetOneApt(ProcCur.AptNum);
					if(apt.AptDateTime.Date > MiscData.GetNowDateTime().Date) {//if appointment is in the future
						MessageBox.Show(Lan.g(this,"Not allowed because procedure is attached to a future appointment with a date of ")
							+apt.AptDateTime.ToShortDateString());
						return false;
					}
					if(apt.AptDateTime.Year>=1880) {
						textDate.Text=apt.AptDateTime.ToShortDateString();
					}
				}
				if(!_isQuickAdd && !Security.IsAuthorized(Permissions.ProcComplCreate,PIn.Date(textDate.Text))){//use the new date
					return false;
				}
			}
			else if(!_isQuickAdd && IsNew && ProcCur.ProcStatus==ProcStat.C) {//if new procedure is complete
				if(!Security.IsAuthorized(Permissions.ProcComplCreate,PIn.Date(textDate.Text))){
					return false;
				}
			}
			else if(!IsNew){//an old procedure
				if(ProcOld.ProcStatus==ProcStat.C){//that was already complete
					//It's not possible for the user to get to this point unless they have permission for ProcComplEdit based on the DateEntryC.
					//The following 2 checks are not redundant because they check different dates.
					if(!Security.IsAuthorized(Permissions.ProcComplEdit,ProcOld.ProcDate)){//block old date
						return false;
					}
					if(ProcCur.ProcStatus==ProcStat.C){//if it's still complete
						if(!Security.IsAuthorized(Permissions.ProcComplEdit,PIn.Date(textDate.Text))){//block new date, too
							return false;
						}
					}
				}
			}
			if(CultureInfo.CurrentCulture.Name.EndsWith("CA")) {//Canadian. en-CA or fr-CA
				if(checkTypeCodeX.Checked) {
					if(checkTypeCodeA.Checked
						|| checkTypeCodeB.Checked
						|| checkTypeCodeC.Checked
						|| checkTypeCodeE.Checked
						|| checkTypeCodeL.Checked
						|| checkTypeCodeS.Checked) 
					{
						MsgBox.Show(this,"If type code 'none' is checked, no other type codes may be checked.");
						return false;
					}
				}
				if(ProcedureCode2.IsProsth
					&& !checkTypeCodeA.Checked
					&& !checkTypeCodeB.Checked
					&& !checkTypeCodeC.Checked
					&& !checkTypeCodeE.Checked
					&& !checkTypeCodeL.Checked
					&& !checkTypeCodeS.Checked
					&& !checkTypeCodeX.Checked) 
				{
					if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"At least one type code should be checked for prosthesis.  Continue anyway?")) {
						return false;
					}
				}
				if(textCanadaLabFee1.errorProvider1.GetError(textCanadaLabFee1)!="" || textCanadaLabFee2.errorProvider1.GetError(textCanadaLabFee2)!="") {
					MessageBox.Show(Lan.g(this,"Please fix lab fees."));
					return false;
				}
			}
			else {
				if(ProcedureCode2.IsProsth) {
					if(listProsth.SelectedIndex==0
					|| (listProsth.SelectedIndex==2 && textDateOriginalProsth.Text=="")) {
						if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Prosthesis date not entered. Continue anyway?")){
							return false;
						}
					}
				}
			}
			if(Programs.UsingOrion) {
			//if(panelOrion.Visible) {
				if(comboStatus.SelectedIndex==-1) {
					MsgBox.Show(this,"Invalid status.");
					return false;
				}
				if(textDateScheduled.errorProvider1.GetError(textDateScheduled)!="") {
					MsgBox.Show(this,"Invalid schedule date.");
					return false;
				}
				if(textDateStop.errorProvider1.GetError(textDateStop)!="") {
					MsgBox.Show(this,"Invalid stop clock date.");
					return false;
				}
			}
			if(ProcedureCode2.TreatArea==TreatmentArea.Quad) {
				if(!radioUL.Checked && !radioUR.Checked && !radioLL.Checked && !radioLR.Checked) {
					MsgBox.Show(this,"Please select a quadrant.");
					return false;
				}
			}
			return true;
		}

		///<summary>MUST call EntriesAreValid first.  Used from OK_Click and from butSetComplete_Click</summary>
		private void SaveAndClose(){
			if(textProcFee.Text==""){
				textProcFee.Text="0";
			}
			ProcCur.PatNum=PatCur.PatNum;
			//ProcCur.Code=this.textProc.Text;
			ProcedureCode2=ProcedureCodes.GetProcCode(textProc.Text);
			ProcCur.CodeNum=ProcedureCode2.CodeNum;
			ProcCur.MedicalCode=textMedicalCode.Text;
			ProcCur.Discount=PIn.Double(textDiscount.Text);
			if(_snomedBodySite==null) {
				ProcCur.SnomedBodySite="";
			}
			else {
				ProcCur.SnomedBodySite=_snomedBodySite.SnomedCode;
			}
			ProcCur.IcdVersion=9;
			if(checkIcdVersion.Checked) {
				ProcCur.IcdVersion=10;
			}
			ProcCur.DiagnosticCode="";
			ProcCur.DiagnosticCode2="";
			ProcCur.DiagnosticCode3="";
			ProcCur.DiagnosticCode4="";
			List<string> diagnosticCodes=new List<string>();//A list of all the diagnostic code boxes.
			if(textDiagnosticCode.Text!="") {
				diagnosticCodes.Add(textDiagnosticCode.Text);
			}
			if(textDiagnosticCode2.Text!="") {
				diagnosticCodes.Add(textDiagnosticCode2.Text);
			}
			if(textDiagnosticCode3.Text!="") {
				diagnosticCodes.Add(textDiagnosticCode3.Text);
			}
			if(textDiagnosticCode4.Text!="") {
				diagnosticCodes.Add(textDiagnosticCode4.Text);
			}
			if(diagnosticCodes.Count>0) {
				ProcCur.DiagnosticCode=diagnosticCodes[0];
			}
			if(diagnosticCodes.Count>1) {
				ProcCur.DiagnosticCode2=diagnosticCodes[1];
			}
			if(diagnosticCodes.Count>2) {
				ProcCur.DiagnosticCode3=diagnosticCodes[2];
			}
			if(diagnosticCodes.Count>3) {
				ProcCur.DiagnosticCode4=diagnosticCodes[3];
			}
			ProcCur.IsPrincDiag=checkIsPrincDiag.Checked;
			ProcCur.ProvOrderOverride=_provNumOrderingSelected;
			ProcCur.CodeMod1 = textCodeMod1.Text;
			ProcCur.CodeMod2 = textCodeMod2.Text;
			ProcCur.CodeMod3 = textCodeMod3.Text;
			ProcCur.CodeMod4 = textCodeMod4.Text;
			ProcCur.UnitQty = PIn.Int(textUnitQty.Text);
			ProcCur.UnitQtyType=(ProcUnitQtyType)comboUnitType.SelectedIndex;
			ProcCur.RevCode = textRevCode.Text;
			ProcCur.DrugUnit=(EnumProcDrugUnit)comboDrugUnit.SelectedIndex;
			ProcCur.DrugQty=PIn.Float(textDrugQty.Text);
			if(ProcOld.ProcStatus!=ProcStat.C && ProcCur.ProcStatus==ProcStat.C){//Proc set complete.
				ProcCur.DateEntryC=DateTime.Now;//this triggers it to set to server time NOW().
				if(ProcCur.DiagnosticCode=="") {
					ProcCur.DiagnosticCode=PrefC.GetString(PrefName.ICD9DefaultForNewProcs);
				}
			}
			else if(ProcOld.ProcStatus==ProcStat.C && ProcCur.ProcStatus!=ProcStat.C 
				&& Adjustments.GetForProc(ProcCur.ProcNum,Adjustments.Refresh(ProcCur.PatNum)).Count!=0
				&&!MsgBox.Show(this,MsgBoxButtons.YesNo,"This procedure has adjustments attached to it. Changing the status from completed will delete any adjustments for the procedure. Continue?")) 
			{
				return;
			}
			ProcCur.DateTP=PIn.Date(this.textDateTP.Text);
			ProcCur.ProcDate=PIn.Date(this.textDate.Text);
			DateTime dateT=PIn.DateT(this.textTimeStart.Text);
			ProcCur.ProcTime=new TimeSpan(dateT.Hour,dateT.Minute,0);
			if(Programs.UsingOrion || PrefC.GetBool(PrefName.ShowFeatureMedicalInsurance)) {
				dateT=ParseTime(textTimeStart.Text);
				ProcCur.ProcTime=new TimeSpan(dateT.Hour,dateT.Minute,0);
				dateT=ParseTime(textTimeEnd.Text);
				ProcCur.ProcTimeEnd=new TimeSpan(dateT.Hour,dateT.Minute,0);
			}
			ProcCur.ProcFee=PIn.Double(textProcFee.Text);
			//ProcCur.LabFee=PIn.PDouble(textLabFee.Text);
			//ProcCur.LabProcCode=textLabCode.Text;
			//MessageBox.Show(ProcCur.ProcFee.ToString());
			//Dx taken care of when radio pushed
			switch(ProcedureCode2.TreatArea){
				case TreatmentArea.Surf:
					ProcCur.ToothNum=Tooth.FromInternat(textTooth.Text);
					ProcCur.Surf=Tooth.SurfTidyFromDisplayToDb(textSurfaces.Text,ProcCur.ToothNum);
					break;
				case TreatmentArea.Tooth:
					ProcCur.Surf="";
					ProcCur.ToothNum=Tooth.FromInternat(textTooth.Text);
					break;
				case TreatmentArea.Mouth:
					ProcCur.Surf="";
					ProcCur.ToothNum="";	
					break;
				case TreatmentArea.Quad:
					//surf set when radio pushed
					ProcCur.ToothNum="";	
					break;
				case TreatmentArea.Sextant:
					//surf taken care of when radio pushed
					ProcCur.ToothNum="";	
					break;
				case TreatmentArea.Arch:
					//don't HAVE to select arch
					//taken care of when radio pushed
					ProcCur.ToothNum="";	
					break;
				case TreatmentArea.ToothRange:
					if (listBoxTeeth.SelectedItems.Count<1 && listBoxTeeth2.SelectedItems.Count<1) {
						MessageBox.Show(Lan.g(this,"Must pick at least 1 tooth"));
						return;
					}
          string range="";
					int idxAmer;
					for(int j=0;j<listBoxTeeth.SelectedIndices.Count;j++){
						idxAmer=listBoxTeeth.SelectedIndices[j];
						if(j!=0){
							range+=",";
						}
            range+=Tooth.labelsUniversal[idxAmer];
					}
					for(int j=0;j<listBoxTeeth2.SelectedIndices.Count;j++){
						idxAmer=listBoxTeeth2.SelectedIndices[j]+16;
						if(j!=0){
							range+=",";
						}
            range+=Tooth.labelsUniversal[idxAmer];
          }
			    ProcCur.ToothRange=range;
					ProcCur.Surf="";
					ProcCur.ToothNum="";	
					break;
			}
			//Status taken care of when list pushed
			ProcCur.Note=this.textNotes.Text;
			try {
				SaveSignature();
			}
			catch(Exception ex){
				MessageBox.Show(Lan.g(this,"Error saving signature.")+"\r\n"+ex.Message);
				//and continue with the rest of this method
			}
			ProcCur.HideGraphics=checkHideGraphics.Checked;
			//provnum already handled.
			//if(comboProvNum.SelectedIndex!=-1) {
			//	ProcCur.ProvNum=ProviderC.List[comboProvNum.SelectedIndex].ProvNum;
			//}
			//clinicNum already handled.
			if(comboDx.SelectedIndex!=-1) {
				ProcCur.Dx=DefC.Short[(int)DefCat.Diagnosis][comboDx.SelectedIndex].DefNum;
			}
			if(comboPrognosis.SelectedIndex==0) {
				ProcCur.Prognosis=0;
			}
			else {
				ProcCur.Prognosis=DefC.Short[(int)DefCat.Prognosis][comboPrognosis.SelectedIndex-1].DefNum;
			}
			if(comboPriority.SelectedIndex==0) {
				ProcCur.Priority=0;
			}
			else {
				ProcCur.Priority=DefC.Short[(int)DefCat.TxPriorities][comboPriority.SelectedIndex-1].DefNum;
			}
			ProcCur.PlaceService=(PlaceOfService)comboPlaceService.SelectedIndex;
			//if(comboClinic.SelectedIndex==0){
			//	ProcCur.ClinicNum=0;
			//}
			//else{
			//	ProcCur.ClinicNum=Clinics.List[comboClinic.SelectedIndex-1].ClinicNum;
			//}
			//site set when user picks from list.
			if(comboBillingTypeOne.SelectedIndex==0){
				ProcCur.BillingTypeOne=0;
			}
			else{
				ProcCur.BillingTypeOne=DefC.Short[(int)DefCat.BillingTypes][comboBillingTypeOne.SelectedIndex-1].DefNum;
			}
			if(comboBillingTypeTwo.SelectedIndex==0) {
				ProcCur.BillingTypeTwo=0;
			}
			else {
				ProcCur.BillingTypeTwo=DefC.Short[(int)DefCat.BillingTypes][comboBillingTypeTwo.SelectedIndex-1].DefNum;
			}
			ProcCur.BillingNote=textBillingNote.Text;
			//ProcCur.HideGraphical=checkHideGraphical.Checked;
			if(CultureInfo.CurrentCulture.Name.EndsWith("CA")) {//Canadian. en-CA or fr-CA
				ProcCur.CanadianTypeCodes="";
				if(checkTypeCodeA.Checked) {
					ProcCur.CanadianTypeCodes+="A";
				}
				if(checkTypeCodeB.Checked) {
					ProcCur.CanadianTypeCodes+="B";
				}
				if(checkTypeCodeC.Checked) {
					ProcCur.CanadianTypeCodes+="C";
				}
				if(checkTypeCodeE.Checked) {
					ProcCur.CanadianTypeCodes+="E";
				}
				if(checkTypeCodeL.Checked) {
					ProcCur.CanadianTypeCodes+="L";
				}
				if(checkTypeCodeS.Checked) {
					ProcCur.CanadianTypeCodes+="S";
				}
				if(checkTypeCodeX.Checked) {
					ProcCur.CanadianTypeCodes+="X";
				}
				double canadaLabFee1=0;
				if(textCanadaLabFee1.Text!="") {
					canadaLabFee1=PIn.Double(textCanadaLabFee1.Text);
				}
				if(canadaLabFee1==0) {
					if(textCanadaLabFee1.Visible && canadaLabFees.Count>0) { //Don't worry about deleting child lab fees if we are editing a lab fee. No such concept.
						Procedures.Delete(canadaLabFees[0].ProcNum);
					}
				}
				else { //canadaLabFee1!=0
					if(canadaLabFees.Count>0) { //Retain the old lab code if present.
						Procedure labFee1Old=canadaLabFees[0].Copy();
						canadaLabFees[0].ProcFee=canadaLabFee1;
						Procedures.Update(canadaLabFees[0],labFee1Old);
					}
					else {
						Procedure labFee1=new Procedure();
						labFee1.PatNum=ProcCur.PatNum;
						labFee1.ProcDate=ProcCur.ProcDate;
						labFee1.ProcFee=canadaLabFee1;
						labFee1.ProcStatus=ProcCur.ProcStatus;
						labFee1.ProvNum=ProcCur.ProvNum;
						labFee1.DateEntryC=DateTime.Now;
						labFee1.ClinicNum=ProcCur.ClinicNum;
						labFee1.ProcNumLab=ProcCur.ProcNum;
						labFee1.CodeNum=ProcedureCodes.GetCodeNum("99111");
						if(labFee1.CodeNum==0) { //Code does not exist.
							ProcedureCode code99111=new ProcedureCode();
							code99111.ProcCode="99111";
							code99111.Descript="+L Commercial Laboratory Procedures";
							code99111.AbbrDesc="Lab Fee";
							code99111.ProcCat=DefC.GetByExactNameNeverZero(DefCat.ProcCodeCats,"Adjunctive General Services");
							ProcedureCodes.Insert(code99111);
							labFee1.CodeNum=code99111.CodeNum;
						}
						Procedures.Insert(labFee1);
					}
				}
				double canadaLabFee2=0;
				if(textCanadaLabFee2.Text!="") {
					canadaLabFee2=PIn.Double(textCanadaLabFee2.Text);
				}
				if(canadaLabFee2==0) {
					if(textCanadaLabFee2.Visible && canadaLabFees.Count>1) { //Don't worry about deleting child lab fees if we are editing a lab fee. No such concept.
						Procedures.Delete(canadaLabFees[1].ProcNum);
					}
				}
				else { //canadaLabFee2!=0
					if(canadaLabFees.Count>1) { //Retain the old lab code if present.
						Procedure labFee2Old=canadaLabFees[1].Copy();
						canadaLabFees[1].ProcFee=canadaLabFee2;
						Procedures.Update(canadaLabFees[1],labFee2Old);
					}
					else {
						Procedure labFee2=new Procedure();
						labFee2.PatNum=ProcCur.PatNum;
						labFee2.ProcDate=ProcCur.ProcDate;
						labFee2.ProcFee=canadaLabFee2;
						labFee2.ProcStatus=ProcCur.ProcStatus;
						labFee2.ProvNum=ProcCur.ProvNum;
						labFee2.DateEntryC=DateTime.Now;
						labFee2.ClinicNum=ProcCur.ClinicNum;
						labFee2.ProcNumLab=ProcCur.ProcNum;
						labFee2.CodeNum=ProcedureCodes.GetCodeNum("99111");
						if(labFee2.CodeNum==0) { //Code does not exist.
							ProcedureCode code99111=new ProcedureCode();
							code99111.ProcCode="99111";
							code99111.Descript="+L Commercial Laboratory Procedures";
							code99111.AbbrDesc="Lab Fee";
							code99111.ProcCat=DefC.GetByExactNameNeverZero(DefCat.ProcCodeCats,"Adjunctive General Services");
							ProcedureCodes.Insert(code99111);
							labFee2.CodeNum=code99111.CodeNum;
						}
						Procedures.Insert(labFee2);
					}
				}
			}
			else {
				if(ProcedureCode2.IsProsth) {
					switch(listProsth.SelectedIndex) {
						case 0:
							ProcCur.Prosthesis="";
							break;
						case 1:
							ProcCur.Prosthesis="I";
							break;
						case 2:
							ProcCur.Prosthesis="R";
							break;
					}
					ProcCur.DateOriginalProsth=PIn.Date(textDateOriginalProsth.Text);
					ProcCur.IsDateProsthEst=checkIsDateProsthEst.Checked;
				}
				else {
					ProcCur.Prosthesis="";
					ProcCur.DateOriginalProsth=DateTime.MinValue;
					ProcCur.IsDateProsthEst=false;
				}
			}
			ProcCur.ClaimNote=textClaimNote.Text;
			//Last chance to run this code before Proc gets updated.
			if(Programs.UsingOrion){//Ask for an explanation. If they hit cancel here, return and don't save.
				OrionProcCur.DPC=(OrionDPC)comboDPC.SelectedIndex;
				OrionProcCur.DPCpost=(OrionDPC)comboDPCpost.SelectedIndex;
				OrionProcCur.DateScheduleBy=PIn.Date(textDateScheduled.Text);
				OrionProcCur.DateStopClock=PIn.Date(textDateStop.Text);
				OrionProcCur.IsOnCall=checkIsOnCall.Checked;
				OrionProcCur.IsEffectiveComm=checkIsEffComm.Checked;
				OrionProcCur.IsRepair=checkIsRepair.Checked;
				if(IsNew) {
					OrionProcs.Insert(OrionProcCur);
				}
				else {//Is not new.
					if(FormProcEditExplain.GetChanges(ProcCur,ProcOld,OrionProcCur,OrionProcOld)!="") {//Checks if any changes were made. Also sets static variable Changes.
						//If a day old and the orion procedure status did not go from TP to C, CS or CR, then show explaination window.
						if((ProcOld.DateTP.Date<MiscData.GetNowDateTime().Date &&
							(OrionProcOld.Status2!=OrionStatus.TP || (OrionProcCur.Status2!=OrionStatus.C && OrionProcCur.Status2!=OrionStatus.CS && OrionProcCur.Status2!=OrionStatus.CR))))
						{
							FormProcEditExplain FormP=new FormProcEditExplain();
							FormP.dpcChange=((int)OrionProcOld.DPC!=(int)OrionProcCur.DPC);
							if(FormP.ShowDialog()!=DialogResult.OK) {
								return;
							}
							Procedure ProcPreExplain=ProcOld.Copy();
							ProcOld.Note=FormProcEditExplain.Explanation;
							Procedures.Update(ProcOld,ProcPreExplain);
							Thread.Sleep(1100);
						}
					}
					OrionProcs.Update(OrionProcCur);
					//Date entry needs to be updated when status changes to cancelled or refused and at least a day old.
					if(ProcOld.DateTP.Date<MiscData.GetNowDateTime().Date &&
						OrionProcCur.Status2==OrionStatus.CA_EPRD ||
						OrionProcCur.Status2==OrionStatus.CA_PD ||
						OrionProcCur.Status2==OrionStatus.CA_Tx ||
						OrionProcCur.Status2==OrionStatus.R) 
					{
						ProcCur.DateEntryC=MiscData.GetNowDateTime().Date;
					}
				}//End of "is not new."
			}
			//The actual update----------------------------------------------------------------------------------------------------------------------------------
			Procedures.Update(ProcCur,ProcOld);
			if(ProcCur.ProcStatus==ProcStat.TP) {
				//if proc is TP status, update priority on any TreatPlanAttach objects if they are attaching this proc to the active TP
				TreatPlan activePlan=TreatPlans.GetActiveForPat(ProcCur.PatNum);
				if(activePlan!=null) {
					List<TreatPlanAttach> listTpAttaches=TreatPlanAttaches.GetAllForTreatPlan(activePlan.TreatPlanNum);
					//should only be 0 or one TPAttach on this TP with this ProcNum
					listTpAttaches.FindAll(x => x.ProcNum==ProcCur.ProcNum).ForEach(x => x.Priority=ProcCur.Priority);
					TreatPlanAttaches.Sync(listTpAttaches,activePlan.TreatPlanNum);
				}
			}
			if(ProcCur.AptNum>0 || ProcCur.PlannedAptNum>0) {
				//Update the ProcDescript on the appointment if procedure is attached to one.
				//The ApptProcDescript region is also in FormApptEdit.UpdateToDB() and FormDatabaseMaintenance.butApptProcs_Click()  Make any changes there as well.
				#region ApptProcDescript
				Appointment apt;
				DataTable procTable;
				if(ProcCur.AptNum>0) {
					apt=Appointments.GetOneApt(ProcCur.AptNum);
					procTable=Appointments.GetProcTable(ProcCur.PatNum.ToString(),apt.AptNum.ToString(),((int)apt.AptStatus).ToString(),apt.AptDateTime.ToString());
				}
				else {
					apt=Appointments.GetOneApt(ProcCur.PlannedAptNum);
					procTable=Appointments.GetProcTable(ProcCur.PatNum.ToString(),ProcCur.PlannedAptNum.ToString(),((int)apt.AptStatus).ToString(),apt.AptDateTime.ToString());
				}
				Appointment aptOld=apt.Clone();
				apt.ProcDescript="";
				apt.ProcsColored="";
				int count=0;
				for(int i=0;i<procTable.Rows.Count;i++) {
					if(procTable.Rows[i]["attached"].ToString()!="1") {
						continue;
					}
					string procDescOne="";
					string procCode=procTable.Rows[i]["ProcCode"].ToString();
					if(count>0) {
						apt.ProcDescript+=", ";
					}
					switch(procTable.Rows[i]["TreatArea"].ToString()) {
						case "1"://TreatmentArea.Surf:
							procDescOne+="#"+Tooth.GetToothLabel(procTable.Rows[i]["ToothNum"].ToString())+"-"
								+procTable.Rows[i]["Surf"].ToString()+"-";//""#12-MOD-"
							break;
						case "2"://TreatmentArea.Tooth:
							procDescOne+="#"+Tooth.GetToothLabel(procTable.Rows[i]["ToothNum"].ToString())+"-";//"#12-"
							break;
						default://area 3 or 0 (mouth)
							break;
						case "4"://TreatmentArea.Quad:
							procDescOne+=procTable.Rows[i]["Surf"].ToString()+"-";//"UL-"
							break;
						case "5"://TreatmentArea.Sextant:
							procDescOne+="S"+procTable.Rows[i]["Surf"].ToString()+"-";//"S2-"
							break;
						case "6"://TreatmentArea.Arch:
							procDescOne+=procTable.Rows[i]["Surf"].ToString()+"-";//"U-"
							break;
						case "7"://TreatmentArea.ToothRange:
							//strLine+=table.Rows[j][13].ToString()+" ";//don't show range
							break;
					}
					procDescOne+=procTable.Rows[i]["AbbrDesc"].ToString();
					apt.ProcDescript+=procDescOne;
					//Color and previous date are determined by ProcApptColor object
					ProcApptColor pac=ProcApptColors.GetMatch(procCode);
					System.Drawing.Color pColor=System.Drawing.Color.Black;
					string prevDateString="";
					if(pac!=null) {
						pColor=pac.ColorText;
						if(pac.ShowPreviousDate) {
							prevDateString=Procedures.GetRecentProcDateString(apt.PatNum,apt.AptDateTime,pac.CodeRange);
							if(prevDateString!="") {
								prevDateString=" ("+prevDateString+")";
							}
						}
					}
					apt.ProcsColored+="<span color=\""+pColor.ToArgb().ToString()+"\">"+procDescOne+prevDateString+"</span>";
					count++;
				}
				#endregion
				Appointments.Update(apt,aptOld);
			}
			for(int i=0;i<ClaimProcsForProc.Count;i++) {
				ClaimProcsForProc[i].ClinicNum=ProcCur.ClinicNum;
			}
			//Recall synch--------------------------------------------------------------------------------------------------------------------------------------
			Recalls.Synch(ProcCur.PatNum);
			//Auto-insert default encounter ---------------------------------------------------------------------------------------------------------------------------
			if(ProcOld.ProcStatus!=ProcStat.C && ProcCur.ProcStatus==ProcStat.C) {
				Encounters.InsertDefaultEncounter(ProcCur.PatNum,ProcCur.ProvNum,ProcCur.ProcDate);
			}
			//Security logs------------------------------------------------------------------------------------------------------------------------------------
			if(ProcCur.Discount!=ProcOld.Discount) {//Discount was changed
				string message=Lan.g(this,"Discount created or changed from Proc Edit window for procedure")
						+": "+ProcedureCodes.GetProcCode(ProcCur.CodeNum).ProcCode+"  "+Lan.g(this,"Dated")
						+": "+ProcCur.ProcDate.ToShortDateString()+"  "+Lan.g(this,"With a Fee of")+": "+ProcCur.ProcFee.ToString("c")+".  "
						+Lan.g(this,"Changed the discount value from")+" "+ProcOld.Discount.ToString("c")+" "+Lan.g(this,"to")+" "+ProcCur.Discount.ToString("c");
				SecurityLogs.MakeLogEntry(Permissions.TreatPlanDiscountEdit,ProcCur.PatNum,message);
			}
			if(ProcOld.ProcStatus!=ProcStat.C && ProcCur.ProcStatus==ProcStat.C){
				//if status was changed to complete
				ProcedureL.LogProcComplCreate(PatCur.PatNum,ProcCur,ProcCur.ToothNum);
				List<string> procCodeList=new List<string>();
				procCodeList.Add(ProcedureCodes.GetStringProcCode(ProcCur.CodeNum));
				AutomationL.Trigger(AutomationTrigger.CompleteProcedure,procCodeList,ProcCur.PatNum);
			}
			else if(IsNew && ProcCur.ProcStatus==ProcStat.C){
				//if new procedure is complete
				ProcedureL.LogProcComplCreate(PatCur.PatNum,ProcCur,ProcCur.ToothNum);
			}
			else if(!IsNew){
				if(ProcOld.ProcStatus==ProcStat.C){//If was complete before the window loaded.
					string logText=ProcedureCode2.ProcCode+", ";
					if(listBoxTeeth.Text!=null && listBoxTeeth.Text.Trim()!="") {
						logText+=Lan.g("FormProcEdit","Teeth")+": "+listBoxTeeth.Text+", ";
					}
					logText+=Lan.g("FormProcEdit","Fee")+": "+ProcCur.ProcFee.ToString("F")+", "+ProcedureCode2.Descript;
					SecurityLogs.MakeLogEntry(Permissions.ProcComplEdit,PatCur.PatNum,logText);
				}
			}
			if((ProcCur.ProcStatus==ProcStat.C || ProcCur.ProcStatus==ProcStat.EC || ProcCur.ProcStatus==ProcStat.EO)
				&& ProcedureCodes.GetProcCode(ProcCur.CodeNum).PaintType==ToothPaintingType.Extraction) {
				//if an extraction, then mark previous procs hidden
				//Procedures.SetHideGraphical(ProcCur);//might not matter anymore
				ToothInitials.SetValue(ProcCur.PatNum,ProcCur.ToothNum,ToothInitialType.Missing);
			}
			//Canadian lab fees complete-----------------------------------------------------------------------------------------------------------------------
			if(CultureInfo.CurrentCulture.Name.EndsWith("CA") && ProcCur.ProcStatus==ProcStat.C) {//Canada
				Procedures.SetCanadianLabFeesCompleteForProc(ProcCur);
			}
			//Canadian lab fees not complete-----------------------------------------------------------------------------------------------------------------------
			if(CultureInfo.CurrentCulture.Name.EndsWith("CA") && ProcCur.ProcStatus!=ProcStat.C) {//Canada
				Procedures.SetCanadianLabFeesStatusForProc(ProcCur);
			}
			//Autocodes----------------------------------------------------------------------------------------------------------------------------------------
			ProcOld=ProcCur.Copy();//in case we now make more changes.
			//these areas have no autocodes
			if(ProcedureCode2.TreatArea==TreatmentArea.Mouth
				|| ProcedureCode2.TreatArea==TreatmentArea.Quad
				|| ProcedureCode2.TreatArea==TreatmentArea.Sextant
				|| Procedures.IsAttachedToClaim(ProcCur,ClaimProcsForProc))
			{
				DialogResult=DialogResult.OK;
				return;
			}
			//this represents the suggested code based on the autocodes set up.
			long verifyCode;
			AutoCode AutoCodeCur=null;
			if(ProcedureCode2.TreatArea==TreatmentArea.Arch){
				if(ProcCur.Surf==""){
					DialogResult=DialogResult.OK;
					return;
				}
				if(ProcCur.Surf=="U"){
					verifyCode=AutoCodeItems.VerifyCode
						(ProcedureCode2.CodeNum,"1","",false,PatCur.PatNum,PatCur.Age,out AutoCodeCur);//max
				}
				else{
					verifyCode=AutoCodeItems.VerifyCode
						(ProcedureCode2.CodeNum,"32","",false,PatCur.PatNum,PatCur.Age,out AutoCodeCur);//mand
				}
			}
			else if(ProcedureCode2.TreatArea==TreatmentArea.ToothRange){
				//test for max or mand.
				if(listBoxTeeth.SelectedItems.Count<1)
					verifyCode=AutoCodeItems.VerifyCode
						(ProcedureCode2.CodeNum,"32","",false,PatCur.PatNum,PatCur.Age,out AutoCodeCur);//mand
				else
					verifyCode=AutoCodeItems.VerifyCode
						(ProcedureCode2.CodeNum,"1","",false,PatCur.PatNum,PatCur.Age,out AutoCodeCur);//max
			}
			else{//surf or tooth
				string claimSurf=Tooth.SurfTidyForClaims(ProcCur.Surf,ProcCur.ToothNum);
				verifyCode=AutoCodeItems.VerifyCode
					(ProcedureCode2.CodeNum,ProcCur.ToothNum,claimSurf,false,PatCur.PatNum,PatCur.Age,out AutoCodeCur);
			}
			if(ProcedureCode2.CodeNum!=verifyCode){
				string desc=ProcedureCodes.GetProcCode(verifyCode).Descript;
				FormAutoCodeLessIntrusive FormA=new FormAutoCodeLessIntrusive();
				FormA.mainText=ProcedureCodes.GetProcCode(verifyCode).ProcCode
					+" ("+desc+") "+Lan.g(this,"is the recommended procedure code for this procedure.  Change procedure code and fee?");
				FormA.ShowDialog();
				if(FormA.DialogResult!=DialogResult.OK){
					DialogResult=DialogResult.OK;
					return;
				}
				//No longer allow users to hide auto code reminders from the procedure edit window.  A label lets them know to change it via Auto Codes.
				//if(FormA.CheckedBox){
				//  AutoCodeCur.LessIntrusive=true;
				//  AutoCodes.Update(AutoCodeCur);
				//  DataValid.SetInvalid(InvalidType.AutoCodes);
				//}
				ProcCur.CodeNum=verifyCode;
				//ProcedureCode2=ProcedureCodes.GetProcCode(ProcCur.CodeNum);
				//ProcCur.Code=verifyCode;
				InsSub prisub=null;
				InsPlan priplan=null;
				if(PatPlanList.Count>0) {
					prisub=InsSubs.GetSub(PatPlanList[0].InsSubNum,SubList);
					priplan=InsPlans.GetPlan(prisub.PlanNum,PlanList);
				}
				double insfee=Fees.GetAmount0(ProcCur.CodeNum,Fees.GetFeeSched(PatCur,PlanList,PatPlanList,SubList),ProcCur.ClinicNum,ProcCur.ProvNum);
				if(priplan!=null && priplan.PlanType=="p") {//PPO
					double standardfee=Fees.GetAmount0(ProcCur.CodeNum,Providers.GetProv(Patients.GetProvNum(PatCur)).FeeSched,ProcCur.ClinicNum,ProcCur.ProvNum);
					if(standardfee>insfee) {
						ProcCur.ProcFee=standardfee;
					}
					else {
						ProcCur.ProcFee=insfee;
					}
				}
				else {
					ProcCur.ProcFee=insfee;
				}
				//ProcCur.ProcFee=Fees.GetAmount0(ProcedureCode2.CodeNum,Fees.GetFeeSched(PatCur,PlanList,PatPlanList));
				Procedures.Update(ProcCur,ProcOld);
				Recalls.Synch(ProcCur.PatNum);
				if(ProcCur.ProcStatus==ProcStat.C){
					string logText=ProcedureCode2.ProcCode+", ";
					if(listBoxTeeth.Text!=null && listBoxTeeth.Text.Trim()!="") {
						logText+=Lan.g("FormProcEdit","Teeth")+": "+listBoxTeeth.Text+", ";
					}
					logText+=Lan.g("FormProcEdit","Fee")+": "+ProcCur.ProcFee.ToString("F")+", "+ProcedureCode2.Descript;
					SecurityLogs.MakeLogEntry(Permissions.ProcComplEdit,PatCur.PatNum,logText);
				}
			}
      DialogResult=DialogResult.OK;
			//it is assumed that we will do an immediate refresh after closing this window.
		}

		private void SaveSignature(){
			if(SigChanged){
				//Topaz boxes are written in Windows native code.
				//if(allowTopaz && sigBoxTopaz.Visible){
				if(sigBoxTopaz.Visible){
					ProcCur.SigIsTopaz=true;
					if(CodeBase.TopazWrapper.GetTopazNumberOfTabletPoints(sigBoxTopaz)==0){
						ProcCur.Signature="";
						return;
					}
					CodeBase.TopazWrapper.SetTopazCompressionMode(sigBoxTopaz,0);
					CodeBase.TopazWrapper.SetTopazEncryptionMode(sigBoxTopaz,0);
					CodeBase.TopazWrapper.SetTopazKeyString(sigBoxTopaz,"0000000000000000");
					CodeBase.TopazWrapper.SetTopazAutoKeyData(sigBoxTopaz,ProcCur.Note+ProcCur.UserNum.ToString());
					CodeBase.TopazWrapper.SetTopazEncryptionMode(sigBoxTopaz,2);
					CodeBase.TopazWrapper.SetTopazCompressionMode(sigBoxTopaz,2);
					ProcCur.Signature=CodeBase.TopazWrapper.GetTopazString(sigBoxTopaz);
				}
				else{
					ProcCur.SigIsTopaz=false;
					if(sigBox.NumberOfTabletPoints()==0) {
						ProcCur.Signature="";
						return;
					}
					//sigBox.SetSigCompressionMode(0);
					//sigBox.SetEncryptionMode(0);
					sigBox.SetKeyString("0000000000000000");
					sigBox.SetAutoKeyData(ProcCur.Note+ProcCur.UserNum.ToString());
					//sigBox.SetEncryptionMode(2);
					//sigBox.SetSigCompressionMode(2);
					ProcCur.Signature=sigBox.GetSigString();
				}
			}
		}

		private void butOK_Click(object sender,System.EventArgs e) {
			if(!EntriesAreValid()) {
				return;
			}
			if(Programs.UsingOrion) {
				if(OrionProcOld!=null && OrionProcOld.DateStopClock.Year>1880) {
					if(PIn.Date(textDateStop.Text)>OrionProcOld.DateStopClock.Date) {
						MsgBox.Show(this,"Future date not allowed for Date Stop Clock.");
						return;
					}
				}
				else if(PIn.Date(textDateStop.Text)>MiscData.GetNowDateTime().Date) {
					MsgBox.Show(this,"Future date not allowed for Date Stop Clock.");
					return;
				}
				//Strange logic for Orion for setting sched by date to a scheduled date from a previously cancelled TP on the same tooth/surf.
				if(ProcCur.Surf!="" || textTooth.Text!="" || textSurfaces.Text!="") {
					DataTable table=OrionProcs.GetCancelledScheduleDateByToothOrSurf(ProcCur.PatNum,textTooth.Text.ToString(),ProcCur.Surf);
					if(table.Rows.Count>0) {
						if(textDateScheduled.Text!="" && DateTime.Parse(textDateScheduled.Text)>PIn.DateT(table.Rows[0]["DateScheduleBy"].ToString())) {
							//If the cancelled sched by date is in the past then do nothing.
							if(PIn.DateT(table.Rows[0]["DateScheduleBy"].ToString())>MiscData.GetNowDateTime().Date) {
								textDateScheduled.Text=((DateTime)table.Rows[0]["DateScheduleBy"]).ToShortDateString();
								CancelledScheduleByDate=DateTime.Parse(textDateScheduled.Text);
								MsgBox.Show(this,"Schedule by date cannot be later than: "+textDateScheduled.Text+".");
								return;
							}
						}
					}
				}
				if(comboDPC.SelectedIndex==0) {
					MsgBox.Show(this,"DPC should not be \"Not Specified\".");
					return;
				}
			}
			SaveAndClose();
			Plugins.HookAddCode(this,"FormProcEdit.butOK_Click_end",ProcCur); 
		}

		private void butCancel_Click(object sender,System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormProcEdit_FormClosing(object sender,FormClosingEventArgs e) {
			//We need to update the CPOE status even if the user is cancelling out of the window.
			if(Userods.IsUserCpoe(Security.CurUser) && !ProcOld.IsCpoe) {
				//There's a possibility that we are making a second, unnecessary call to the database here but it is worth it to help meet EHR measures.
				Procedures.UpdateCpoeForProc(ProcCur.ProcNum,true);
				//Make a log that we edited this procedure's CPOE flag.
				SecurityLogs.MakeLogEntry(Permissions.ProcEdit,ProcCur.PatNum,ProcedureCodes.GetProcCode(ProcCur.CodeNum).ProcCode
					+", "+ProcCur.ProcFee.ToString("c")+", "+Lan.g(this,"automatically flagged as CPOE."));
			}
			if(DialogResult==DialogResult.OK) {
				//this catches date,prov,fee,status,etc for all claimProcs attached to this proc.
				if(!StartedAttachedToClaim
					&& Procedures.IsAttachedToClaim(ProcCur.ProcNum)) 
				{
					return;//unless they got attached to a claim while this window was open.  Then it doesn't touch them.
				}
				if(StartedAttachedToClaim
					&& !Procedures.IsAttachedToClaim(ProcCur.ProcNum)) 
				{
					return;//We don't want to allow ComputeEstimates to reattach the procedure to the old claim which could have deleted.
				}
				//Now we have to double check that every single claimproc is attached to the same claim that they were originally attached to.
				if(ClaimProcs.IsAttachedToDifferentClaim(ProcCur.ProcNum,ClaimProcsForProc)) {
					return;//The claimproc is not attached to the same claim it was originally pointing to.  Do not run ComputeEstimates which would point it back to the old (potentially deleted) claim.
				}
				Procedures.ComputeEstimates(ProcCur,PatCur.PatNum,ClaimProcsForProc,false,PlanList,PatPlanList,BenefitList,PatCur.Age,SubList);
				return;
			}
			if(IsNew) {//if cancelling on a new procedure
				//delete any newly created claimprocs
				for(int i=0;i<ClaimProcsForProc.Count;i++) {
					//if(ClaimProcsForProc[i].ProcNum==ProcCur.ProcNum) {
					ClaimProcs.Delete(ClaimProcsForProc[i]);
					//}
				}
			}
		}



		

		

		
	

		

		

		



		

		

		

		

		

		

	

	

	}
}
