/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;
using CodeBase;
using System.Threading;

namespace OpenDental {

	///<summary></summary>
	public class ContrAccount:System.Windows.Forms.UserControl {
		private System.Windows.Forms.Label labelFamFinancial;
		private System.ComponentModel.IContainer components=null;// Required designer variable.
		private System.Windows.Forms.Label labelUrgFinNote;
		private OpenDental.ODtextBox textUrgFinNote;
		private System.Windows.Forms.ContextMenu contextMenuIns;
		private System.Windows.Forms.MenuItem menuInsOther;
		private System.Windows.Forms.MenuItem menuInsPri;
		private System.Windows.Forms.MenuItem menuInsSec;
		private OpenDental.UI.ODToolBar ToolBarMain;
		private System.Windows.Forms.ImageList imageListMain;
		private System.Windows.Forms.Panel panelSplitter;
		private OpenDental.ODtextBox textFinNotes;
		private System.Windows.Forms.ContextMenu contextMenuStatement;
		private System.Windows.Forms.MenuItem menuItemStatementWalkout;
		private System.Windows.Forms.MenuItem menuItemStatementMore;
		private OpenDental.UI.ODGrid gridComm;
		private OpenDental.UI.ODGrid gridAcctPat;
		private OpenDental.UI.ODGrid gridAccount;
		private OpenDental.UI.ODGrid gridRepeat;
		private System.Windows.Forms.MenuItem menuInsMedical;
		private ContextMenu contextMenuRepeat;
		private MenuItem menuItemRepeatStand;
		private MenuItem menuItemRepeatEmail;
		private Panel panelProgNotes;
		private ODGrid gridProg;
		private GroupBox groupBox6;
		private CheckBox checkShowE;
		private CheckBox checkShowR;
		private CheckBox checkShowC;
		private CheckBox checkShowTP;
		private GroupBox groupBox7;
		private CheckBox checkAppt;
		private CheckBox checkLabCase;
		private CheckBox checkRx;
		private CheckBox checkComm;
		private CheckBox checkNotes;
		private OpenDental.UI.Button butShowAll;
		private OpenDental.UI.Button butShowNone;
		private CheckBox checkExtraNotes;
		private CheckBox checkShowTeeth;
		private CheckBox checkAudit;
		private Panel panelAging;
		private Label labelBalance;
		private Label labelInsEst;
		private Label labelTotal;
		private Label label7;
		private TextBox text0_30;
		private Label label6;
		private TextBox text31_60;
		private Label label5;
		private TextBox text61_90;
		private Label label3;
		private TextBox textOver90;
		private Label label2;
		private MenuItem menuItemStatementEmail;
		private Label labelBalanceAmt;
		private TabControl tabControlShow;
		private TabPage tabMain;
		private TabPage tabShow;
		private ODGrid gridPayPlan;
		private ValidDate textDateEnd;
		private ValidDate textDateStart;
		private Label labelEndDate;
		private Label labelStartDate;
		private OpenDental.UI.Button butRefresh;
		private OpenDental.UI.Button but90days;
		private OpenDental.UI.Button but45days;
		private OpenDental.UI.Button butDatesAll;
		private CheckBox checkShowDetail;
		private OpenDental.UI.Button butToday;
		private CheckBox checkShowFamilyComm;
		private FormPayPlan FormPayPlan2;
		private Label labelTotalAmt;
		private Label labelInsEstAmt;
		private Panel panelAgeLine;
		private Panel panel2;
		private Panel panel1;
		private ToolTip toolTip1;
		private Label labelPatEstBal;
		private Label labelPatEstBalAmt;
		private Panel panelTotalOwes;
		private Label label21;
		private Label labelTotalPtOwes;
		private Label labelUnearnedAmt;
		private Label labelUnearned;
		private Label labelInsRem;
		private decimal PPBalanceTotal;
		private PatField[] _patFieldList;
		private Def[] _acctProcQuickAddDefs;
		private ODToolBarButton _butQuickProcs;
		///<summary>Gets updated to PatCur.PatNum that the last security log was made with so that we don't make too many security logs for this patient.  When _patNumLast no longer matches PatCur.PatNum (e.g. switched to a different patient within a module), a security log will be entered.  Gets reset (cleared and the set back to PatCur.PatNum) any time a module button is clicked which will cause another security log to be entered.</summary>
		private long _patNumLast;

		#region UserVariables
		///<summary>This holds nearly all of the data needed for display.  It is retrieved in one call to the database.</summary>
		private DataSet DataSetMain;
		private Family FamCur;
		///<summary></summary>
		private Patient PatCur;
		private PatientNote PatientNoteCur;
		///<summary></summary>
		[Category("Data"),Description("Occurs when user changes current patient, usually by clicking on the Select Patient button.")]
		public event PatientSelectedEventHandler PatientSelected=null;
		private RepeatCharge[] RepeatChargeList;
		private int OriginalMousePos;
		private bool MouseIsDownOnSplitter;
		private int SplitterOriginalY;
		private bool FinNoteChanged;
		private bool CCChanged;
		private bool UrgFinNoteChanged;
		private int Actscrollval;
		///<summary>Set to true if this control is placed in the recall edit window. This affects the control behavior.</summary>
		public bool ViewingInRecall=false;
		private List<DisplayField> fieldsForMainGrid;
		private GroupBox groupBoxIndIns;
		private TextBox textPriDed;
		private TextBox textPriUsed;
		private TextBox textPriDedRem;
		private TextBox textPriPend;
		private TextBox textPriRem;
		private TextBox textPriMax;
		private TextBox textSecRem;
		private Label label10;
		private TextBox textSecPend;
		private Label label11;
		private Label label18;
		private Label label12;
		private Label label13;
		private TextBox textSecDedRem;
		private Label label14;
		private Label label15;
		private TextBox textSecMax;
		private Label label16;
		private TextBox textSecDed;
		private TextBox textSecUsed;
		private GroupBox groupBoxFamilyIns;
		private TextBox textFamPriMax;
		private TextBox textFamPriDed;
		private Label label4;
		private Label label8;
		private TextBox textFamSecMax;
		private Label label9;
		private TextBox textFamSecDed;
		private Label label17;
		private UI.Button butCreditCard;
		private MenuItem menuItemRepeatMobile;
		private MenuItem menuItemReceipt;
		private MenuItem menuItemRepeatCanada;
		private MenuItem menuItemInvoice;
		private ODGrid gridPatInfo;
		private bool InitializedOnStartup;
		private MenuItem menuItemRepeatWebSched;
		private ContextMenu contextMenuQuickProcs;
		private TextBox textQuickProcs;
		private List<DisplayField> _patInfoDisplayFields;
		#endregion UserVariables

		///<summary></summary>
		public ContrAccount() {
			Logger.openlog.Log("Initializing account module...",Logger.Severity.INFO);
			InitializeComponent();// This call is required by the Windows.Forms Form Designer.
		}

		///<summary></summary>
		protected override void Dispose(bool disposing) {
			if(disposing) {
				if(components!= null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContrAccount));
			this.labelFamFinancial = new System.Windows.Forms.Label();
			this.labelUrgFinNote = new System.Windows.Forms.Label();
			this.contextMenuIns = new System.Windows.Forms.ContextMenu();
			this.menuInsPri = new System.Windows.Forms.MenuItem();
			this.menuInsSec = new System.Windows.Forms.MenuItem();
			this.menuInsMedical = new System.Windows.Forms.MenuItem();
			this.menuInsOther = new System.Windows.Forms.MenuItem();
			this.imageListMain = new System.Windows.Forms.ImageList(this.components);
			this.panelSplitter = new System.Windows.Forms.Panel();
			this.contextMenuStatement = new System.Windows.Forms.ContextMenu();
			this.menuItemStatementWalkout = new System.Windows.Forms.MenuItem();
			this.menuItemStatementEmail = new System.Windows.Forms.MenuItem();
			this.menuItemReceipt = new System.Windows.Forms.MenuItem();
			this.menuItemInvoice = new System.Windows.Forms.MenuItem();
			this.menuItemStatementMore = new System.Windows.Forms.MenuItem();
			this.contextMenuRepeat = new System.Windows.Forms.ContextMenu();
			this.menuItemRepeatStand = new System.Windows.Forms.MenuItem();
			this.menuItemRepeatEmail = new System.Windows.Forms.MenuItem();
			this.menuItemRepeatMobile = new System.Windows.Forms.MenuItem();
			this.menuItemRepeatCanada = new System.Windows.Forms.MenuItem();
			this.menuItemRepeatWebSched = new System.Windows.Forms.MenuItem();
			this.panelProgNotes = new System.Windows.Forms.Panel();
			this.butShowNone = new OpenDental.UI.Button();
			this.butShowAll = new OpenDental.UI.Button();
			this.checkNotes = new System.Windows.Forms.CheckBox();
			this.groupBox7 = new System.Windows.Forms.GroupBox();
			this.checkShowTeeth = new System.Windows.Forms.CheckBox();
			this.checkAudit = new System.Windows.Forms.CheckBox();
			this.checkExtraNotes = new System.Windows.Forms.CheckBox();
			this.checkAppt = new System.Windows.Forms.CheckBox();
			this.checkLabCase = new System.Windows.Forms.CheckBox();
			this.checkRx = new System.Windows.Forms.CheckBox();
			this.checkComm = new System.Windows.Forms.CheckBox();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.checkShowE = new System.Windows.Forms.CheckBox();
			this.checkShowR = new System.Windows.Forms.CheckBox();
			this.checkShowC = new System.Windows.Forms.CheckBox();
			this.checkShowTP = new System.Windows.Forms.CheckBox();
			this.gridProg = new OpenDental.UI.ODGrid();
			this.panelAging = new System.Windows.Forms.Panel();
			this.labelInsRem = new System.Windows.Forms.Label();
			this.labelUnearnedAmt = new System.Windows.Forms.Label();
			this.labelUnearned = new System.Windows.Forms.Label();
			this.labelBalanceAmt = new System.Windows.Forms.Label();
			this.labelTotalAmt = new System.Windows.Forms.Label();
			this.panelTotalOwes = new System.Windows.Forms.Panel();
			this.label21 = new System.Windows.Forms.Label();
			this.labelTotalPtOwes = new System.Windows.Forms.Label();
			this.labelPatEstBalAmt = new System.Windows.Forms.Label();
			this.labelPatEstBal = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panelAgeLine = new System.Windows.Forms.Panel();
			this.labelInsEstAmt = new System.Windows.Forms.Label();
			this.labelBalance = new System.Windows.Forms.Label();
			this.labelInsEst = new System.Windows.Forms.Label();
			this.labelTotal = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.text0_30 = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.text31_60 = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.text61_90 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textOver90 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.tabControlShow = new System.Windows.Forms.TabControl();
			this.tabMain = new System.Windows.Forms.TabPage();
			this.butCreditCard = new OpenDental.UI.Button();
			this.textUrgFinNote = new OpenDental.ODtextBox();
			this.gridAcctPat = new OpenDental.UI.ODGrid();
			this.textFinNotes = new OpenDental.ODtextBox();
			this.tabShow = new System.Windows.Forms.TabPage();
			this.checkShowFamilyComm = new System.Windows.Forms.CheckBox();
			this.butToday = new OpenDental.UI.Button();
			this.checkShowDetail = new System.Windows.Forms.CheckBox();
			this.butDatesAll = new OpenDental.UI.Button();
			this.but90days = new OpenDental.UI.Button();
			this.but45days = new OpenDental.UI.Button();
			this.butRefresh = new OpenDental.UI.Button();
			this.labelEndDate = new System.Windows.Forms.Label();
			this.labelStartDate = new System.Windows.Forms.Label();
			this.textDateEnd = new OpenDental.ValidDate();
			this.textDateStart = new OpenDental.ValidDate();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.groupBoxIndIns = new System.Windows.Forms.GroupBox();
			this.textPriDed = new System.Windows.Forms.TextBox();
			this.textPriUsed = new System.Windows.Forms.TextBox();
			this.textPriDedRem = new System.Windows.Forms.TextBox();
			this.textPriPend = new System.Windows.Forms.TextBox();
			this.textPriRem = new System.Windows.Forms.TextBox();
			this.textPriMax = new System.Windows.Forms.TextBox();
			this.textSecRem = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.textSecPend = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.textSecDedRem = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.textSecMax = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.textSecDed = new System.Windows.Forms.TextBox();
			this.textSecUsed = new System.Windows.Forms.TextBox();
			this.groupBoxFamilyIns = new System.Windows.Forms.GroupBox();
			this.textFamPriMax = new System.Windows.Forms.TextBox();
			this.textFamPriDed = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.textFamSecMax = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.textFamSecDed = new System.Windows.Forms.TextBox();
			this.label17 = new System.Windows.Forms.Label();
			this.gridPayPlan = new OpenDental.UI.ODGrid();
			this.gridRepeat = new OpenDental.UI.ODGrid();
			this.gridAccount = new OpenDental.UI.ODGrid();
			this.gridComm = new OpenDental.UI.ODGrid();
			this.gridPatInfo = new OpenDental.UI.ODGrid();
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			this.contextMenuQuickProcs = new System.Windows.Forms.ContextMenu();
			this.textQuickProcs = new System.Windows.Forms.TextBox();
			this.panelProgNotes.SuspendLayout();
			this.groupBox7.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.panelAging.SuspendLayout();
			this.panelTotalOwes.SuspendLayout();
			this.tabControlShow.SuspendLayout();
			this.tabMain.SuspendLayout();
			this.tabShow.SuspendLayout();
			this.groupBoxIndIns.SuspendLayout();
			this.groupBoxFamilyIns.SuspendLayout();
			this.SuspendLayout();
			// 
			// labelFamFinancial
			// 
			this.labelFamFinancial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelFamFinancial.Location = new System.Drawing.Point(0, 318);
			this.labelFamFinancial.Name = "labelFamFinancial";
			this.labelFamFinancial.Size = new System.Drawing.Size(154, 16);
			this.labelFamFinancial.TabIndex = 9;
			this.labelFamFinancial.Text = "Family Financial Notes";
			this.labelFamFinancial.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// labelUrgFinNote
			// 
			this.labelUrgFinNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelUrgFinNote.Location = new System.Drawing.Point(0, 0);
			this.labelUrgFinNote.Name = "labelUrgFinNote";
			this.labelUrgFinNote.Size = new System.Drawing.Size(165, 17);
			this.labelUrgFinNote.TabIndex = 10;
			this.labelUrgFinNote.Text = "Fam Urgent Fin Note";
			this.labelUrgFinNote.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// contextMenuIns
			// 
			this.contextMenuIns.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuInsPri,
            this.menuInsSec,
            this.menuInsMedical,
            this.menuInsOther});
			// 
			// menuInsPri
			// 
			this.menuInsPri.Index = 0;
			this.menuInsPri.Text = "Primary";
			this.menuInsPri.Click += new System.EventHandler(this.menuInsPri_Click);
			// 
			// menuInsSec
			// 
			this.menuInsSec.Index = 1;
			this.menuInsSec.Text = "Secondary";
			this.menuInsSec.Click += new System.EventHandler(this.menuInsSec_Click);
			// 
			// menuInsMedical
			// 
			this.menuInsMedical.Index = 2;
			this.menuInsMedical.Text = "Medical";
			this.menuInsMedical.Click += new System.EventHandler(this.menuInsMedical_Click);
			// 
			// menuInsOther
			// 
			this.menuInsOther.Index = 3;
			this.menuInsOther.Text = "Other";
			this.menuInsOther.Click += new System.EventHandler(this.menuInsOther_Click);
			// 
			// imageListMain
			// 
			this.imageListMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMain.ImageStream")));
			this.imageListMain.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListMain.Images.SetKeyName(0, "Pat.gif");
			this.imageListMain.Images.SetKeyName(1, "");
			this.imageListMain.Images.SetKeyName(2, "");
			this.imageListMain.Images.SetKeyName(3, "Umbrella.gif");
			this.imageListMain.Images.SetKeyName(4, "");
			// 
			// panelSplitter
			// 
			this.panelSplitter.Cursor = System.Windows.Forms.Cursors.SizeNS;
			this.panelSplitter.Location = new System.Drawing.Point(0, 425);
			this.panelSplitter.Name = "panelSplitter";
			this.panelSplitter.Size = new System.Drawing.Size(769, 5);
			this.panelSplitter.TabIndex = 49;
			this.panelSplitter.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelSplitter_MouseDown);
			this.panelSplitter.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelSplitter_MouseMove);
			this.panelSplitter.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelSplitter_MouseUp);
			// 
			// contextMenuStatement
			// 
			this.contextMenuStatement.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemStatementWalkout,
            this.menuItemStatementEmail,
            this.menuItemReceipt,
            this.menuItemInvoice,
            this.menuItemStatementMore});
			// 
			// menuItemStatementWalkout
			// 
			this.menuItemStatementWalkout.Index = 0;
			this.menuItemStatementWalkout.Text = "Walkout";
			this.menuItemStatementWalkout.Click += new System.EventHandler(this.menuItemStatementWalkout_Click);
			// 
			// menuItemStatementEmail
			// 
			this.menuItemStatementEmail.Index = 1;
			this.menuItemStatementEmail.Text = "Email";
			this.menuItemStatementEmail.Click += new System.EventHandler(this.menuItemStatementEmail_Click);
			// 
			// menuItemReceipt
			// 
			this.menuItemReceipt.Index = 2;
			this.menuItemReceipt.Text = "Receipt";
			this.menuItemReceipt.Click += new System.EventHandler(this.menuItemReceipt_Click);
			// 
			// menuItemInvoice
			// 
			this.menuItemInvoice.Index = 3;
			this.menuItemInvoice.Text = "Invoice";
			this.menuItemInvoice.Click += new System.EventHandler(this.menuItemInvoice_Click);
			// 
			// menuItemStatementMore
			// 
			this.menuItemStatementMore.Index = 4;
			this.menuItemStatementMore.Text = "More Options";
			this.menuItemStatementMore.Click += new System.EventHandler(this.menuItemStatementMore_Click);
			// 
			// contextMenuRepeat
			// 
			this.contextMenuRepeat.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemRepeatStand,
            this.menuItemRepeatEmail,
            this.menuItemRepeatMobile,
            this.menuItemRepeatCanada,
            this.menuItemRepeatWebSched});
			// 
			// menuItemRepeatStand
			// 
			this.menuItemRepeatStand.Index = 0;
			this.menuItemRepeatStand.Text = "Standard Monthly";
			this.menuItemRepeatStand.Click += new System.EventHandler(this.MenuItemRepeatStand_Click);
			// 
			// menuItemRepeatEmail
			// 
			this.menuItemRepeatEmail.Index = 1;
			this.menuItemRepeatEmail.Text = "Email Monthly";
			this.menuItemRepeatEmail.Click += new System.EventHandler(this.MenuItemRepeatEmail_Click);
			// 
			// menuItemRepeatMobile
			// 
			this.menuItemRepeatMobile.Index = 2;
			this.menuItemRepeatMobile.Text = "Mobile Monthly";
			this.menuItemRepeatMobile.Click += new System.EventHandler(this.menuItemRepeatMobile_Click);
			// 
			// menuItemRepeatCanada
			// 
			this.menuItemRepeatCanada.Index = 3;
			this.menuItemRepeatCanada.Text = "Canada Monthly";
			this.menuItemRepeatCanada.Click += new System.EventHandler(this.menuItemRepeatCanada_Click);
			// 
			// menuItemRepeatWebSched
			// 
			this.menuItemRepeatWebSched.Index = 4;
			this.menuItemRepeatWebSched.Text = "WebSched Monthly";
			this.menuItemRepeatWebSched.Click += new System.EventHandler(this.menuItemRepeatWebSched_Click);
			// 
			// panelProgNotes
			// 
			this.panelProgNotes.Controls.Add(this.butShowNone);
			this.panelProgNotes.Controls.Add(this.butShowAll);
			this.panelProgNotes.Controls.Add(this.checkNotes);
			this.panelProgNotes.Controls.Add(this.groupBox7);
			this.panelProgNotes.Controls.Add(this.groupBox6);
			this.panelProgNotes.Controls.Add(this.gridProg);
			this.panelProgNotes.Location = new System.Drawing.Point(0, 461);
			this.panelProgNotes.Name = "panelProgNotes";
			this.panelProgNotes.Size = new System.Drawing.Size(749, 227);
			this.panelProgNotes.TabIndex = 211;
			// 
			// butShowNone
			// 
			this.butShowNone.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butShowNone.Autosize = true;
			this.butShowNone.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butShowNone.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butShowNone.CornerRadius = 4F;
			this.butShowNone.Location = new System.Drawing.Point(677, 207);
			this.butShowNone.Name = "butShowNone";
			this.butShowNone.Size = new System.Drawing.Size(58, 16);
			this.butShowNone.TabIndex = 216;
			this.butShowNone.Text = "None";
			this.butShowNone.Visible = false;
			this.butShowNone.Click += new System.EventHandler(this.butShowNone_Click);
			// 
			// butShowAll
			// 
			this.butShowAll.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butShowAll.Autosize = true;
			this.butShowAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butShowAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butShowAll.CornerRadius = 4F;
			this.butShowAll.Location = new System.Drawing.Point(614, 207);
			this.butShowAll.Name = "butShowAll";
			this.butShowAll.Size = new System.Drawing.Size(53, 16);
			this.butShowAll.TabIndex = 215;
			this.butShowAll.Text = "All";
			this.butShowAll.Visible = false;
			this.butShowAll.Click += new System.EventHandler(this.butShowAll_Click);
			// 
			// checkNotes
			// 
			this.checkNotes.AllowDrop = true;
			this.checkNotes.Checked = true;
			this.checkNotes.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkNotes.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkNotes.Location = new System.Drawing.Point(624, 191);
			this.checkNotes.Name = "checkNotes";
			this.checkNotes.Size = new System.Drawing.Size(102, 13);
			this.checkNotes.TabIndex = 214;
			this.checkNotes.Text = "Proc Notes";
			this.checkNotes.Visible = false;
			this.checkNotes.Click += new System.EventHandler(this.checkNotes_Click);
			// 
			// groupBox7
			// 
			this.groupBox7.Controls.Add(this.checkShowTeeth);
			this.groupBox7.Controls.Add(this.checkAudit);
			this.groupBox7.Controls.Add(this.checkExtraNotes);
			this.groupBox7.Controls.Add(this.checkAppt);
			this.groupBox7.Controls.Add(this.checkLabCase);
			this.groupBox7.Controls.Add(this.checkRx);
			this.groupBox7.Controls.Add(this.checkComm);
			this.groupBox7.Location = new System.Drawing.Point(614, 88);
			this.groupBox7.Name = "groupBox7";
			this.groupBox7.Size = new System.Drawing.Size(121, 101);
			this.groupBox7.TabIndex = 213;
			this.groupBox7.TabStop = false;
			this.groupBox7.Text = "Object Types";
			this.groupBox7.Visible = false;
			// 
			// checkShowTeeth
			// 
			this.checkShowTeeth.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowTeeth.Location = new System.Drawing.Point(44, 63);
			this.checkShowTeeth.Name = "checkShowTeeth";
			this.checkShowTeeth.Size = new System.Drawing.Size(104, 13);
			this.checkShowTeeth.TabIndex = 217;
			this.checkShowTeeth.Text = "Selected Teeth";
			this.checkShowTeeth.Visible = false;
			// 
			// checkAudit
			// 
			this.checkAudit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAudit.Location = new System.Drawing.Point(44, 79);
			this.checkAudit.Name = "checkAudit";
			this.checkAudit.Size = new System.Drawing.Size(73, 13);
			this.checkAudit.TabIndex = 218;
			this.checkAudit.Text = "Audit";
			this.checkAudit.Visible = false;
			// 
			// checkExtraNotes
			// 
			this.checkExtraNotes.AllowDrop = true;
			this.checkExtraNotes.Checked = true;
			this.checkExtraNotes.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkExtraNotes.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkExtraNotes.Location = new System.Drawing.Point(9, 82);
			this.checkExtraNotes.Name = "checkExtraNotes";
			this.checkExtraNotes.Size = new System.Drawing.Size(102, 13);
			this.checkExtraNotes.TabIndex = 215;
			this.checkExtraNotes.Text = "Extra Notes";
			this.checkExtraNotes.Visible = false;
			this.checkExtraNotes.Click += new System.EventHandler(this.checkExtraNotes_Click);
			// 
			// checkAppt
			// 
			this.checkAppt.Checked = true;
			this.checkAppt.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkAppt.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAppt.Location = new System.Drawing.Point(10, 17);
			this.checkAppt.Name = "checkAppt";
			this.checkAppt.Size = new System.Drawing.Size(102, 13);
			this.checkAppt.TabIndex = 20;
			this.checkAppt.Text = "Appointments";
			this.checkAppt.Click += new System.EventHandler(this.checkAppt_Click);
			// 
			// checkLabCase
			// 
			this.checkLabCase.Checked = true;
			this.checkLabCase.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkLabCase.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkLabCase.Location = new System.Drawing.Point(10, 49);
			this.checkLabCase.Name = "checkLabCase";
			this.checkLabCase.Size = new System.Drawing.Size(102, 13);
			this.checkLabCase.TabIndex = 17;
			this.checkLabCase.Text = "Lab Cases";
			this.checkLabCase.Click += new System.EventHandler(this.checkLabCase_Click);
			// 
			// checkRx
			// 
			this.checkRx.Checked = true;
			this.checkRx.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkRx.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkRx.Location = new System.Drawing.Point(10, 65);
			this.checkRx.Name = "checkRx";
			this.checkRx.Size = new System.Drawing.Size(102, 13);
			this.checkRx.TabIndex = 8;
			this.checkRx.Text = "Rx";
			this.checkRx.Click += new System.EventHandler(this.checkRx_Click);
			// 
			// checkComm
			// 
			this.checkComm.Checked = true;
			this.checkComm.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkComm.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkComm.Location = new System.Drawing.Point(10, 33);
			this.checkComm.Name = "checkComm";
			this.checkComm.Size = new System.Drawing.Size(102, 13);
			this.checkComm.TabIndex = 16;
			this.checkComm.Text = "Comm Log";
			this.checkComm.Click += new System.EventHandler(this.checkComm_Click);
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.Add(this.checkShowE);
			this.groupBox6.Controls.Add(this.checkShowR);
			this.groupBox6.Controls.Add(this.checkShowC);
			this.groupBox6.Controls.Add(this.checkShowTP);
			this.groupBox6.Location = new System.Drawing.Point(614, 1);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(121, 85);
			this.groupBox6.TabIndex = 212;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Procedures";
			this.groupBox6.Visible = false;
			// 
			// checkShowE
			// 
			this.checkShowE.Checked = true;
			this.checkShowE.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkShowE.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowE.Location = new System.Drawing.Point(9, 49);
			this.checkShowE.Name = "checkShowE";
			this.checkShowE.Size = new System.Drawing.Size(101, 13);
			this.checkShowE.TabIndex = 10;
			this.checkShowE.Text = "Existing";
			this.checkShowE.Click += new System.EventHandler(this.checkShowE_Click);
			// 
			// checkShowR
			// 
			this.checkShowR.Checked = true;
			this.checkShowR.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkShowR.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowR.Location = new System.Drawing.Point(9, 65);
			this.checkShowR.Name = "checkShowR";
			this.checkShowR.Size = new System.Drawing.Size(101, 13);
			this.checkShowR.TabIndex = 14;
			this.checkShowR.Text = "Referred";
			this.checkShowR.Click += new System.EventHandler(this.checkShowR_Click);
			// 
			// checkShowC
			// 
			this.checkShowC.Checked = true;
			this.checkShowC.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkShowC.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowC.Location = new System.Drawing.Point(9, 33);
			this.checkShowC.Name = "checkShowC";
			this.checkShowC.Size = new System.Drawing.Size(101, 13);
			this.checkShowC.TabIndex = 9;
			this.checkShowC.Text = "Completed";
			this.checkShowC.Click += new System.EventHandler(this.checkShowC_Click);
			// 
			// checkShowTP
			// 
			this.checkShowTP.Checked = true;
			this.checkShowTP.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkShowTP.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowTP.Location = new System.Drawing.Point(9, 17);
			this.checkShowTP.Name = "checkShowTP";
			this.checkShowTP.Size = new System.Drawing.Size(101, 13);
			this.checkShowTP.TabIndex = 8;
			this.checkShowTP.Text = "Treat Plan";
			this.checkShowTP.Click += new System.EventHandler(this.checkShowTP_Click);
			// 
			// gridProg
			// 
			this.gridProg.HasMultilineHeaders = false;
			this.gridProg.HScrollVisible = true;
			this.gridProg.Location = new System.Drawing.Point(3, 0);
			this.gridProg.Name = "gridProg";
			this.gridProg.ScrollValue = 0;
			this.gridProg.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridProg.Size = new System.Drawing.Size(603, 230);
			this.gridProg.TabIndex = 211;
			this.gridProg.Title = "Progress Notes";
			this.gridProg.TranslationName = "TableProg";
			this.gridProg.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridProg_CellDoubleClick);
			// 
			// panelAging
			// 
			this.panelAging.Controls.Add(this.labelInsRem);
			this.panelAging.Controls.Add(this.labelUnearnedAmt);
			this.panelAging.Controls.Add(this.labelUnearned);
			this.panelAging.Controls.Add(this.labelBalanceAmt);
			this.panelAging.Controls.Add(this.labelTotalAmt);
			this.panelAging.Controls.Add(this.panelTotalOwes);
			this.panelAging.Controls.Add(this.labelPatEstBalAmt);
			this.panelAging.Controls.Add(this.labelPatEstBal);
			this.panelAging.Controls.Add(this.panel2);
			this.panelAging.Controls.Add(this.panel1);
			this.panelAging.Controls.Add(this.panelAgeLine);
			this.panelAging.Controls.Add(this.labelInsEstAmt);
			this.panelAging.Controls.Add(this.labelBalance);
			this.panelAging.Controls.Add(this.labelInsEst);
			this.panelAging.Controls.Add(this.labelTotal);
			this.panelAging.Controls.Add(this.label7);
			this.panelAging.Controls.Add(this.text0_30);
			this.panelAging.Controls.Add(this.label6);
			this.panelAging.Controls.Add(this.text31_60);
			this.panelAging.Controls.Add(this.label5);
			this.panelAging.Controls.Add(this.text61_90);
			this.panelAging.Controls.Add(this.label3);
			this.panelAging.Controls.Add(this.textOver90);
			this.panelAging.Controls.Add(this.label2);
			this.panelAging.Location = new System.Drawing.Point(0, 25);
			this.panelAging.Name = "panelAging";
			this.panelAging.Size = new System.Drawing.Size(749, 37);
			this.panelAging.TabIndex = 213;
			// 
			// labelInsRem
			// 
			this.labelInsRem.BackColor = System.Drawing.Color.LightGray;
			this.labelInsRem.Location = new System.Drawing.Point(700, 4);
			this.labelInsRem.Name = "labelInsRem";
			this.labelInsRem.Size = new System.Drawing.Size(45, 29);
			this.labelInsRem.TabIndex = 0;
			this.labelInsRem.Text = "Ins\r\nRem";
			this.labelInsRem.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.labelInsRem.Click += new System.EventHandler(this.labelInsRem_Click);
			this.labelInsRem.MouseEnter += new System.EventHandler(this.labelInsRem_MouseEnter);
			this.labelInsRem.MouseLeave += new System.EventHandler(this.labelInsRem_MouseLeave);
			// 
			// labelUnearnedAmt
			// 
			this.labelUnearnedAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelUnearnedAmt.ForeColor = System.Drawing.Color.Firebrick;
			this.labelUnearnedAmt.Location = new System.Drawing.Point(636, 18);
			this.labelUnearnedAmt.Name = "labelUnearnedAmt";
			this.labelUnearnedAmt.Size = new System.Drawing.Size(60, 13);
			this.labelUnearnedAmt.TabIndex = 228;
			this.labelUnearnedAmt.Text = "25000.00";
			this.labelUnearnedAmt.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// labelUnearned
			// 
			this.labelUnearned.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelUnearned.ForeColor = System.Drawing.SystemColors.ControlText;
			this.labelUnearned.Location = new System.Drawing.Point(632, 2);
			this.labelUnearned.Name = "labelUnearned";
			this.labelUnearned.Size = new System.Drawing.Size(68, 13);
			this.labelUnearned.TabIndex = 227;
			this.labelUnearned.Text = "Unearned";
			this.labelUnearned.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// labelBalanceAmt
			// 
			this.labelBalanceAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelBalanceAmt.ForeColor = System.Drawing.Color.Firebrick;
			this.labelBalanceAmt.Location = new System.Drawing.Point(456, 16);
			this.labelBalanceAmt.Name = "labelBalanceAmt";
			this.labelBalanceAmt.Size = new System.Drawing.Size(80, 19);
			this.labelBalanceAmt.TabIndex = 60;
			this.labelBalanceAmt.Text = "25000.00";
			this.labelBalanceAmt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelTotalAmt
			// 
			this.labelTotalAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTotalAmt.ForeColor = System.Drawing.Color.Firebrick;
			this.labelTotalAmt.Location = new System.Drawing.Point(294, 16);
			this.labelTotalAmt.Name = "labelTotalAmt";
			this.labelTotalAmt.Size = new System.Drawing.Size(80, 19);
			this.labelTotalAmt.TabIndex = 61;
			this.labelTotalAmt.Text = "25000.00";
			this.labelTotalAmt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panelTotalOwes
			// 
			this.panelTotalOwes.Controls.Add(this.label21);
			this.panelTotalOwes.Controls.Add(this.labelTotalPtOwes);
			this.panelTotalOwes.Location = new System.Drawing.Point(560, -38);
			this.panelTotalOwes.Name = "panelTotalOwes";
			this.panelTotalOwes.Size = new System.Drawing.Size(126, 37);
			this.panelTotalOwes.TabIndex = 226;
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(3, 0);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(123, 12);
			this.label21.TabIndex = 223;
			this.label21.Text = "TOTAL  Owed w/ Plan:";
			this.label21.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.toolTip1.SetToolTip(this.label21, "Total balance owed on all payment plans ");
			// 
			// labelTotalPtOwes
			// 
			this.labelTotalPtOwes.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTotalPtOwes.ForeColor = System.Drawing.Color.Firebrick;
			this.labelTotalPtOwes.Location = new System.Drawing.Point(6, 12);
			this.labelTotalPtOwes.Name = "labelTotalPtOwes";
			this.labelTotalPtOwes.Size = new System.Drawing.Size(112, 23);
			this.labelTotalPtOwes.TabIndex = 222;
			this.labelTotalPtOwes.Text = "2500.00";
			this.labelTotalPtOwes.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// labelPatEstBalAmt
			// 
			this.labelPatEstBalAmt.Location = new System.Drawing.Point(550, 19);
			this.labelPatEstBalAmt.Name = "labelPatEstBalAmt";
			this.labelPatEstBalAmt.Size = new System.Drawing.Size(65, 13);
			this.labelPatEstBalAmt.TabIndex = 89;
			this.labelPatEstBalAmt.Text = "25000.00";
			this.labelPatEstBalAmt.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// labelPatEstBal
			// 
			this.labelPatEstBal.Location = new System.Drawing.Point(550, 3);
			this.labelPatEstBal.Name = "labelPatEstBal";
			this.labelPatEstBal.Size = new System.Drawing.Size(65, 13);
			this.labelPatEstBal.TabIndex = 88;
			this.labelPatEstBal.Text = "Pat Est Bal";
			this.labelPatEstBal.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.SystemColors.ControlDark;
			this.panel2.Location = new System.Drawing.Point(624, 3);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(2, 32);
			this.panel2.TabIndex = 87;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
			this.panel1.Location = new System.Drawing.Point(541, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(2, 32);
			this.panel1.TabIndex = 86;
			// 
			// panelAgeLine
			// 
			this.panelAgeLine.BackColor = System.Drawing.SystemColors.ControlDark;
			this.panelAgeLine.Location = new System.Drawing.Point(379, 2);
			this.panelAgeLine.Name = "panelAgeLine";
			this.panelAgeLine.Size = new System.Drawing.Size(2, 32);
			this.panelAgeLine.TabIndex = 63;
			// 
			// labelInsEstAmt
			// 
			this.labelInsEstAmt.Location = new System.Drawing.Point(387, 18);
			this.labelInsEstAmt.Name = "labelInsEstAmt";
			this.labelInsEstAmt.Size = new System.Drawing.Size(65, 13);
			this.labelInsEstAmt.TabIndex = 62;
			this.labelInsEstAmt.Text = "2500.00";
			this.labelInsEstAmt.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// labelBalance
			// 
			this.labelBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelBalance.Location = new System.Drawing.Point(454, 2);
			this.labelBalance.Name = "labelBalance";
			this.labelBalance.Size = new System.Drawing.Size(80, 13);
			this.labelBalance.TabIndex = 59;
			this.labelBalance.Text = "= Balance";
			this.labelBalance.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// labelInsEst
			// 
			this.labelInsEst.Location = new System.Drawing.Point(387, 2);
			this.labelInsEst.Name = "labelInsEst";
			this.labelInsEst.Size = new System.Drawing.Size(65, 13);
			this.labelInsEst.TabIndex = 57;
			this.labelInsEst.Text = "- InsEst";
			this.labelInsEst.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// labelTotal
			// 
			this.labelTotal.Location = new System.Drawing.Point(294, 2);
			this.labelTotal.Name = "labelTotal";
			this.labelTotal.Size = new System.Drawing.Size(80, 13);
			this.labelTotal.TabIndex = 55;
			this.labelTotal.Text = "Total";
			this.labelTotal.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(69, 2);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(55, 13);
			this.label7.TabIndex = 53;
			this.label7.Text = "0-30";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// text0_30
			// 
			this.text0_30.Location = new System.Drawing.Point(67, 15);
			this.text0_30.Name = "text0_30";
			this.text0_30.ReadOnly = true;
			this.text0_30.Size = new System.Drawing.Size(55, 20);
			this.text0_30.TabIndex = 52;
			this.text0_30.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(122, 2);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(55, 13);
			this.label6.TabIndex = 51;
			this.label6.Text = "31-60";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// text31_60
			// 
			this.text31_60.Location = new System.Drawing.Point(122, 15);
			this.text31_60.Name = "text31_60";
			this.text31_60.ReadOnly = true;
			this.text31_60.Size = new System.Drawing.Size(55, 20);
			this.text31_60.TabIndex = 50;
			this.text31_60.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(177, 2);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(55, 13);
			this.label5.TabIndex = 49;
			this.label5.Text = "61-90";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// text61_90
			// 
			this.text61_90.Location = new System.Drawing.Point(177, 15);
			this.text61_90.Name = "text61_90";
			this.text61_90.ReadOnly = true;
			this.text61_90.Size = new System.Drawing.Size(55, 20);
			this.text61_90.TabIndex = 48;
			this.text61_90.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(232, 2);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(55, 13);
			this.label3.TabIndex = 47;
			this.label3.Text = "over 90";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// textOver90
			// 
			this.textOver90.Location = new System.Drawing.Point(232, 15);
			this.textOver90.Name = "textOver90";
			this.textOver90.ReadOnly = true;
			this.textOver90.Size = new System.Drawing.Size(55, 20);
			this.textOver90.TabIndex = 46;
			this.textOver90.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(14, 2);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(50, 33);
			this.label2.TabIndex = 45;
			this.label2.Text = "Family\r\nAging";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// tabControlShow
			// 
			this.tabControlShow.Controls.Add(this.tabMain);
			this.tabControlShow.Controls.Add(this.tabShow);
			this.tabControlShow.Location = new System.Drawing.Point(749, 27);
			this.tabControlShow.Name = "tabControlShow";
			this.tabControlShow.SelectedIndex = 0;
			this.tabControlShow.Size = new System.Drawing.Size(186, 497);
			this.tabControlShow.TabIndex = 216;
			// 
			// tabMain
			// 
			this.tabMain.BackColor = System.Drawing.Color.Transparent;
			this.tabMain.Controls.Add(this.butCreditCard);
			this.tabMain.Controls.Add(this.labelUrgFinNote);
			this.tabMain.Controls.Add(this.labelFamFinancial);
			this.tabMain.Controls.Add(this.textUrgFinNote);
			this.tabMain.Controls.Add(this.gridAcctPat);
			this.tabMain.Controls.Add(this.textFinNotes);
			this.tabMain.Location = new System.Drawing.Point(4, 22);
			this.tabMain.Name = "tabMain";
			this.tabMain.Padding = new System.Windows.Forms.Padding(3);
			this.tabMain.Size = new System.Drawing.Size(178, 471);
			this.tabMain.TabIndex = 0;
			this.tabMain.Text = "Main";
			this.tabMain.UseVisualStyleBackColor = true;
			// 
			// butCreditCard
			// 
			this.butCreditCard.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCreditCard.Autosize = true;
			this.butCreditCard.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCreditCard.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCreditCard.CornerRadius = 4F;
			this.butCreditCard.Location = new System.Drawing.Point(22, 103);
			this.butCreditCard.Name = "butCreditCard";
			this.butCreditCard.Size = new System.Drawing.Size(137, 26);
			this.butCreditCard.TabIndex = 216;
			this.butCreditCard.Text = "Credit Card Manage";
			this.butCreditCard.UseVisualStyleBackColor = true;
			this.butCreditCard.Click += new System.EventHandler(this.butCreditCard_Click);
			// 
			// textUrgFinNote
			// 
			this.textUrgFinNote.AcceptsTab = true;
			this.textUrgFinNote.BackColor = System.Drawing.Color.White;
			this.textUrgFinNote.DetectUrls = false;
			this.textUrgFinNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textUrgFinNote.ForeColor = System.Drawing.Color.Red;
			this.textUrgFinNote.Location = new System.Drawing.Point(0, 20);
			this.textUrgFinNote.Name = "textUrgFinNote";
			this.textUrgFinNote.QuickPasteType = OpenDentBusiness.QuickPasteType.FinancialNotes;
			this.textUrgFinNote.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textUrgFinNote.Size = new System.Drawing.Size(178, 77);
			this.textUrgFinNote.TabIndex = 11;
			this.textUrgFinNote.Text = "";
			this.textUrgFinNote.TextChanged += new System.EventHandler(this.textUrgFinNote_TextChanged);
			this.textUrgFinNote.Leave += new System.EventHandler(this.textUrgFinNote_Leave);
			// 
			// gridAcctPat
			// 
			this.gridAcctPat.HasMultilineHeaders = false;
			this.gridAcctPat.HScrollVisible = false;
			this.gridAcctPat.Location = new System.Drawing.Point(0, 135);
			this.gridAcctPat.Name = "gridAcctPat";
			this.gridAcctPat.ScrollValue = 0;
			this.gridAcctPat.SelectedRowColor = System.Drawing.Color.DarkSalmon;
			this.gridAcctPat.Size = new System.Drawing.Size(178, 180);
			this.gridAcctPat.TabIndex = 72;
			this.gridAcctPat.Title = "Select Patient";
			this.gridAcctPat.TranslationName = "TableAccountPat";
			this.gridAcctPat.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridAcctPat_CellClick);
			// 
			// textFinNotes
			// 
			this.textFinNotes.AcceptsTab = true;
			this.textFinNotes.DetectUrls = false;
			this.textFinNotes.Location = new System.Drawing.Point(0, 337);
			this.textFinNotes.Name = "textFinNotes";
			this.textFinNotes.QuickPasteType = OpenDentBusiness.QuickPasteType.FinancialNotes;
			this.textFinNotes.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textFinNotes.Size = new System.Drawing.Size(178, 134);
			this.textFinNotes.TabIndex = 70;
			this.textFinNotes.Text = "";
			this.textFinNotes.TextChanged += new System.EventHandler(this.textFinNotes_TextChanged);
			this.textFinNotes.Leave += new System.EventHandler(this.textFinNotes_Leave);
			// 
			// tabShow
			// 
			this.tabShow.BackColor = System.Drawing.Color.Transparent;
			this.tabShow.Controls.Add(this.checkShowFamilyComm);
			this.tabShow.Controls.Add(this.butToday);
			this.tabShow.Controls.Add(this.checkShowDetail);
			this.tabShow.Controls.Add(this.butDatesAll);
			this.tabShow.Controls.Add(this.but90days);
			this.tabShow.Controls.Add(this.but45days);
			this.tabShow.Controls.Add(this.butRefresh);
			this.tabShow.Controls.Add(this.labelEndDate);
			this.tabShow.Controls.Add(this.labelStartDate);
			this.tabShow.Controls.Add(this.textDateEnd);
			this.tabShow.Controls.Add(this.textDateStart);
			this.tabShow.Location = new System.Drawing.Point(4, 22);
			this.tabShow.Name = "tabShow";
			this.tabShow.Padding = new System.Windows.Forms.Padding(3);
			this.tabShow.Size = new System.Drawing.Size(178, 471);
			this.tabShow.TabIndex = 1;
			this.tabShow.Text = "Show";
			this.tabShow.UseVisualStyleBackColor = true;
			// 
			// checkShowFamilyComm
			// 
			this.checkShowFamilyComm.AutoSize = true;
			this.checkShowFamilyComm.Location = new System.Drawing.Point(8, 220);
			this.checkShowFamilyComm.Name = "checkShowFamilyComm";
			this.checkShowFamilyComm.Size = new System.Drawing.Size(152, 17);
			this.checkShowFamilyComm.TabIndex = 221;
			this.checkShowFamilyComm.Text = "Show Family Comm Entries";
			this.checkShowFamilyComm.UseVisualStyleBackColor = true;
			this.checkShowFamilyComm.Click += new System.EventHandler(this.checkShowFamilyComm_Click);
			// 
			// butToday
			// 
			this.butToday.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butToday.Autosize = true;
			this.butToday.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butToday.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butToday.CornerRadius = 4F;
			this.butToday.Location = new System.Drawing.Point(95, 54);
			this.butToday.Name = "butToday";
			this.butToday.Size = new System.Drawing.Size(77, 24);
			this.butToday.TabIndex = 220;
			this.butToday.Text = "Today";
			this.butToday.Click += new System.EventHandler(this.butToday_Click);
			// 
			// checkShowDetail
			// 
			this.checkShowDetail.Checked = true;
			this.checkShowDetail.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkShowDetail.Location = new System.Drawing.Point(8, 196);
			this.checkShowDetail.Name = "checkShowDetail";
			this.checkShowDetail.Size = new System.Drawing.Size(164, 18);
			this.checkShowDetail.TabIndex = 219;
			this.checkShowDetail.Text = "Show Proc Breakdowns";
			this.checkShowDetail.UseVisualStyleBackColor = true;
			this.checkShowDetail.Click += new System.EventHandler(this.checkShowDetail_Click);
			// 
			// butDatesAll
			// 
			this.butDatesAll.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDatesAll.Autosize = true;
			this.butDatesAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDatesAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDatesAll.CornerRadius = 4F;
			this.butDatesAll.Location = new System.Drawing.Point(95, 132);
			this.butDatesAll.Name = "butDatesAll";
			this.butDatesAll.Size = new System.Drawing.Size(77, 24);
			this.butDatesAll.TabIndex = 218;
			this.butDatesAll.Text = "All Dates";
			this.butDatesAll.Click += new System.EventHandler(this.butDatesAll_Click);
			// 
			// but90days
			// 
			this.but90days.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.but90days.Autosize = true;
			this.but90days.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.but90days.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.but90days.CornerRadius = 4F;
			this.but90days.Location = new System.Drawing.Point(95, 106);
			this.but90days.Name = "but90days";
			this.but90days.Size = new System.Drawing.Size(77, 24);
			this.but90days.TabIndex = 217;
			this.but90days.Text = "Last 90 Days";
			this.but90days.Click += new System.EventHandler(this.but90days_Click);
			// 
			// but45days
			// 
			this.but45days.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.but45days.Autosize = true;
			this.but45days.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.but45days.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.but45days.CornerRadius = 4F;
			this.but45days.Location = new System.Drawing.Point(95, 80);
			this.but45days.Name = "but45days";
			this.but45days.Size = new System.Drawing.Size(77, 24);
			this.but45days.TabIndex = 216;
			this.but45days.Text = "Last 45 Days";
			this.but45days.Click += new System.EventHandler(this.but45days_Click);
			// 
			// butRefresh
			// 
			this.butRefresh.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butRefresh.Autosize = true;
			this.butRefresh.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRefresh.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRefresh.CornerRadius = 4F;
			this.butRefresh.Location = new System.Drawing.Point(95, 158);
			this.butRefresh.Name = "butRefresh";
			this.butRefresh.Size = new System.Drawing.Size(77, 24);
			this.butRefresh.TabIndex = 214;
			this.butRefresh.Text = "Refresh";
			this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
			// 
			// labelEndDate
			// 
			this.labelEndDate.Location = new System.Drawing.Point(2, 34);
			this.labelEndDate.Name = "labelEndDate";
			this.labelEndDate.Size = new System.Drawing.Size(91, 14);
			this.labelEndDate.TabIndex = 211;
			this.labelEndDate.Text = "End Date";
			this.labelEndDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelStartDate
			// 
			this.labelStartDate.Location = new System.Drawing.Point(8, 11);
			this.labelStartDate.Name = "labelStartDate";
			this.labelStartDate.Size = new System.Drawing.Size(84, 14);
			this.labelStartDate.TabIndex = 210;
			this.labelStartDate.Text = "Start Date";
			this.labelStartDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textDateEnd
			// 
			this.textDateEnd.Location = new System.Drawing.Point(95, 31);
			this.textDateEnd.Name = "textDateEnd";
			this.textDateEnd.Size = new System.Drawing.Size(77, 20);
			this.textDateEnd.TabIndex = 213;
			// 
			// textDateStart
			// 
			this.textDateStart.BackColor = System.Drawing.SystemColors.Window;
			this.textDateStart.ForeColor = System.Drawing.SystemColors.WindowText;
			this.textDateStart.Location = new System.Drawing.Point(95, 8);
			this.textDateStart.Name = "textDateStart";
			this.textDateStart.Size = new System.Drawing.Size(77, 20);
			this.textDateStart.TabIndex = 212;
			// 
			// groupBoxIndIns
			// 
			this.groupBoxIndIns.Controls.Add(this.textPriDed);
			this.groupBoxIndIns.Controls.Add(this.textPriUsed);
			this.groupBoxIndIns.Controls.Add(this.textPriDedRem);
			this.groupBoxIndIns.Controls.Add(this.textPriPend);
			this.groupBoxIndIns.Controls.Add(this.textPriRem);
			this.groupBoxIndIns.Controls.Add(this.textPriMax);
			this.groupBoxIndIns.Controls.Add(this.textSecRem);
			this.groupBoxIndIns.Controls.Add(this.label10);
			this.groupBoxIndIns.Controls.Add(this.textSecPend);
			this.groupBoxIndIns.Controls.Add(this.label11);
			this.groupBoxIndIns.Controls.Add(this.label18);
			this.groupBoxIndIns.Controls.Add(this.label12);
			this.groupBoxIndIns.Controls.Add(this.label13);
			this.groupBoxIndIns.Controls.Add(this.textSecDedRem);
			this.groupBoxIndIns.Controls.Add(this.label14);
			this.groupBoxIndIns.Controls.Add(this.label15);
			this.groupBoxIndIns.Controls.Add(this.textSecMax);
			this.groupBoxIndIns.Controls.Add(this.label16);
			this.groupBoxIndIns.Controls.Add(this.textSecDed);
			this.groupBoxIndIns.Controls.Add(this.textSecUsed);
			this.groupBoxIndIns.Location = new System.Drawing.Point(556, 144);
			this.groupBoxIndIns.Name = "groupBoxIndIns";
			this.groupBoxIndIns.Size = new System.Drawing.Size(193, 160);
			this.groupBoxIndIns.TabIndex = 219;
			this.groupBoxIndIns.TabStop = false;
			this.groupBoxIndIns.Text = "Individual Insurance";
			this.groupBoxIndIns.Visible = false;
			// 
			// textPriDed
			// 
			this.textPriDed.BackColor = System.Drawing.Color.White;
			this.textPriDed.Location = new System.Drawing.Point(71, 55);
			this.textPriDed.Name = "textPriDed";
			this.textPriDed.ReadOnly = true;
			this.textPriDed.Size = new System.Drawing.Size(60, 20);
			this.textPriDed.TabIndex = 45;
			this.textPriDed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textPriUsed
			// 
			this.textPriUsed.BackColor = System.Drawing.Color.White;
			this.textPriUsed.Location = new System.Drawing.Point(71, 95);
			this.textPriUsed.Name = "textPriUsed";
			this.textPriUsed.ReadOnly = true;
			this.textPriUsed.Size = new System.Drawing.Size(60, 20);
			this.textPriUsed.TabIndex = 44;
			this.textPriUsed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textPriDedRem
			// 
			this.textPriDedRem.BackColor = System.Drawing.Color.White;
			this.textPriDedRem.Location = new System.Drawing.Point(71, 75);
			this.textPriDedRem.Name = "textPriDedRem";
			this.textPriDedRem.ReadOnly = true;
			this.textPriDedRem.Size = new System.Drawing.Size(60, 20);
			this.textPriDedRem.TabIndex = 51;
			this.textPriDedRem.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textPriPend
			// 
			this.textPriPend.BackColor = System.Drawing.Color.White;
			this.textPriPend.Location = new System.Drawing.Point(71, 115);
			this.textPriPend.Name = "textPriPend";
			this.textPriPend.ReadOnly = true;
			this.textPriPend.Size = new System.Drawing.Size(60, 20);
			this.textPriPend.TabIndex = 43;
			this.textPriPend.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textPriRem
			// 
			this.textPriRem.BackColor = System.Drawing.Color.White;
			this.textPriRem.Location = new System.Drawing.Point(71, 135);
			this.textPriRem.Name = "textPriRem";
			this.textPriRem.ReadOnly = true;
			this.textPriRem.Size = new System.Drawing.Size(60, 20);
			this.textPriRem.TabIndex = 42;
			this.textPriRem.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textPriMax
			// 
			this.textPriMax.BackColor = System.Drawing.Color.White;
			this.textPriMax.Location = new System.Drawing.Point(71, 35);
			this.textPriMax.Name = "textPriMax";
			this.textPriMax.ReadOnly = true;
			this.textPriMax.Size = new System.Drawing.Size(60, 20);
			this.textPriMax.TabIndex = 38;
			this.textPriMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textSecRem
			// 
			this.textSecRem.BackColor = System.Drawing.Color.White;
			this.textSecRem.Location = new System.Drawing.Point(130, 135);
			this.textSecRem.Name = "textSecRem";
			this.textSecRem.ReadOnly = true;
			this.textSecRem.Size = new System.Drawing.Size(60, 20);
			this.textSecRem.TabIndex = 46;
			this.textSecRem.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(73, 16);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(60, 15);
			this.label10.TabIndex = 31;
			this.label10.Text = "Primary";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// textSecPend
			// 
			this.textSecPend.BackColor = System.Drawing.Color.White;
			this.textSecPend.Location = new System.Drawing.Point(130, 115);
			this.textSecPend.Name = "textSecPend";
			this.textSecPend.ReadOnly = true;
			this.textSecPend.Size = new System.Drawing.Size(60, 20);
			this.textSecPend.TabIndex = 47;
			this.textSecPend.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(4, 37);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(66, 15);
			this.label11.TabIndex = 32;
			this.label11.Text = "Annual Max";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(2, 77);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(69, 15);
			this.label18.TabIndex = 50;
			this.label18.Text = "Ded Remain";
			this.label18.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(4, 57);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(66, 15);
			this.label12.TabIndex = 33;
			this.label12.Text = "Deductible";
			this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(4, 97);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(66, 15);
			this.label13.TabIndex = 34;
			this.label13.Text = "Ins Used";
			this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textSecDedRem
			// 
			this.textSecDedRem.BackColor = System.Drawing.Color.White;
			this.textSecDedRem.Location = new System.Drawing.Point(130, 75);
			this.textSecDedRem.Name = "textSecDedRem";
			this.textSecDedRem.ReadOnly = true;
			this.textSecDedRem.Size = new System.Drawing.Size(60, 20);
			this.textSecDedRem.TabIndex = 52;
			this.textSecDedRem.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(4, 137);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(66, 15);
			this.label14.TabIndex = 35;
			this.label14.Text = "Remaining";
			this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(4, 117);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(66, 15);
			this.label15.TabIndex = 36;
			this.label15.Text = "Pending";
			this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textSecMax
			// 
			this.textSecMax.BackColor = System.Drawing.Color.White;
			this.textSecMax.Location = new System.Drawing.Point(130, 35);
			this.textSecMax.Name = "textSecMax";
			this.textSecMax.ReadOnly = true;
			this.textSecMax.Size = new System.Drawing.Size(60, 20);
			this.textSecMax.TabIndex = 41;
			this.textSecMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(130, 16);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(60, 14);
			this.label16.TabIndex = 37;
			this.label16.Text = "Secondary";
			this.label16.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// textSecDed
			// 
			this.textSecDed.BackColor = System.Drawing.Color.White;
			this.textSecDed.Location = new System.Drawing.Point(130, 55);
			this.textSecDed.Name = "textSecDed";
			this.textSecDed.ReadOnly = true;
			this.textSecDed.Size = new System.Drawing.Size(60, 20);
			this.textSecDed.TabIndex = 40;
			this.textSecDed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textSecUsed
			// 
			this.textSecUsed.BackColor = System.Drawing.Color.White;
			this.textSecUsed.Location = new System.Drawing.Point(130, 95);
			this.textSecUsed.Name = "textSecUsed";
			this.textSecUsed.ReadOnly = true;
			this.textSecUsed.Size = new System.Drawing.Size(60, 20);
			this.textSecUsed.TabIndex = 39;
			this.textSecUsed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// groupBoxFamilyIns
			// 
			this.groupBoxFamilyIns.Controls.Add(this.textFamPriMax);
			this.groupBoxFamilyIns.Controls.Add(this.textFamPriDed);
			this.groupBoxFamilyIns.Controls.Add(this.label4);
			this.groupBoxFamilyIns.Controls.Add(this.label8);
			this.groupBoxFamilyIns.Controls.Add(this.textFamSecMax);
			this.groupBoxFamilyIns.Controls.Add(this.label9);
			this.groupBoxFamilyIns.Controls.Add(this.textFamSecDed);
			this.groupBoxFamilyIns.Controls.Add(this.label17);
			this.groupBoxFamilyIns.Location = new System.Drawing.Point(556, 64);
			this.groupBoxFamilyIns.Name = "groupBoxFamilyIns";
			this.groupBoxFamilyIns.Size = new System.Drawing.Size(193, 80);
			this.groupBoxFamilyIns.TabIndex = 218;
			this.groupBoxFamilyIns.TabStop = false;
			this.groupBoxFamilyIns.Text = "Family Insurance";
			this.groupBoxFamilyIns.Visible = false;
			// 
			// textFamPriMax
			// 
			this.textFamPriMax.BackColor = System.Drawing.Color.White;
			this.textFamPriMax.Location = new System.Drawing.Point(72, 35);
			this.textFamPriMax.Name = "textFamPriMax";
			this.textFamPriMax.ReadOnly = true;
			this.textFamPriMax.Size = new System.Drawing.Size(60, 20);
			this.textFamPriMax.TabIndex = 69;
			this.textFamPriMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textFamPriDed
			// 
			this.textFamPriDed.BackColor = System.Drawing.Color.White;
			this.textFamPriDed.Location = new System.Drawing.Point(72, 55);
			this.textFamPriDed.Name = "textFamPriDed";
			this.textFamPriDed.ReadOnly = true;
			this.textFamPriDed.Size = new System.Drawing.Size(60, 20);
			this.textFamPriDed.TabIndex = 65;
			this.textFamPriDed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(74, 16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(60, 15);
			this.label4.TabIndex = 66;
			this.label4.Text = "Primary";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(4, 37);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(66, 15);
			this.label8.TabIndex = 67;
			this.label8.Text = "Annual Max";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textFamSecMax
			// 
			this.textFamSecMax.BackColor = System.Drawing.Color.White;
			this.textFamSecMax.Location = new System.Drawing.Point(131, 35);
			this.textFamSecMax.Name = "textFamSecMax";
			this.textFamSecMax.ReadOnly = true;
			this.textFamSecMax.Size = new System.Drawing.Size(60, 20);
			this.textFamSecMax.TabIndex = 70;
			this.textFamSecMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(131, 16);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(60, 14);
			this.label9.TabIndex = 68;
			this.label9.Text = "Secondary";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// textFamSecDed
			// 
			this.textFamSecDed.BackColor = System.Drawing.Color.White;
			this.textFamSecDed.Location = new System.Drawing.Point(131, 55);
			this.textFamSecDed.Name = "textFamSecDed";
			this.textFamSecDed.ReadOnly = true;
			this.textFamSecDed.Size = new System.Drawing.Size(60, 20);
			this.textFamSecDed.TabIndex = 64;
			this.textFamSecDed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(4, 57);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(66, 15);
			this.label17.TabIndex = 63;
			this.label17.Text = "Fam Ded";
			this.label17.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// gridPayPlan
			// 
			this.gridPayPlan.HasMultilineHeaders = false;
			this.gridPayPlan.HScrollVisible = false;
			this.gridPayPlan.Location = new System.Drawing.Point(0, 144);
			this.gridPayPlan.Name = "gridPayPlan";
			this.gridPayPlan.ScrollValue = 0;
			this.gridPayPlan.Size = new System.Drawing.Size(749, 93);
			this.gridPayPlan.TabIndex = 217;
			this.gridPayPlan.Title = "Payment Plans";
			this.gridPayPlan.TranslationName = "TablePaymentPlans";
			this.gridPayPlan.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridPayPlan_CellDoubleClick);
			// 
			// gridRepeat
			// 
			this.gridRepeat.HasMultilineHeaders = false;
			this.gridRepeat.HScrollVisible = false;
			this.gridRepeat.Location = new System.Drawing.Point(0, 63);
			this.gridRepeat.Name = "gridRepeat";
			this.gridRepeat.ScrollValue = 0;
			this.gridRepeat.Size = new System.Drawing.Size(749, 75);
			this.gridRepeat.TabIndex = 74;
			this.gridRepeat.Title = "Repeating Charges";
			this.gridRepeat.TranslationName = "TableRepeatCharges";
			this.gridRepeat.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridRepeat_CellDoubleClick);
			// 
			// gridAccount
			// 
			this.gridAccount.HasMultilineHeaders = false;
			this.gridAccount.HScrollVisible = true;
			this.gridAccount.Location = new System.Drawing.Point(0, 243);
			this.gridAccount.Name = "gridAccount";
			this.gridAccount.ScrollValue = 0;
			this.gridAccount.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridAccount.Size = new System.Drawing.Size(749, 179);
			this.gridAccount.TabIndex = 73;
			this.gridAccount.Title = "Patient Account";
			this.gridAccount.TranslationName = "TableAccount";
			this.gridAccount.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridAccount_CellDoubleClick);
			this.gridAccount.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridAccount_CellClick);
			// 
			// gridComm
			// 
			this.gridComm.HasMultilineHeaders = false;
			this.gridComm.HScrollVisible = false;
			this.gridComm.Location = new System.Drawing.Point(0, 440);
			this.gridComm.Name = "gridComm";
			this.gridComm.ScrollValue = 0;
			this.gridComm.Size = new System.Drawing.Size(749, 226);
			this.gridComm.TabIndex = 71;
			this.gridComm.Title = "Communications Log";
			this.gridComm.TranslationName = "TableCommLogAccount";
			this.gridComm.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridComm_CellDoubleClick);
			// 
			// gridPatInfo
			// 
			this.gridPatInfo.HasMultilineHeaders = false;
			this.gridPatInfo.HScrollVisible = false;
			this.gridPatInfo.Location = new System.Drawing.Point(751, 526);
			this.gridPatInfo.Name = "gridPatInfo";
			this.gridPatInfo.ScrollValue = 0;
			this.gridPatInfo.SelectionMode = OpenDental.UI.GridSelectionMode.None;
			this.gridPatInfo.Size = new System.Drawing.Size(182, 136);
			this.gridPatInfo.TabIndex = 217;
			this.gridPatInfo.Title = "Patient Information";
			this.gridPatInfo.TranslationName = "TableAccountPat";
			this.gridPatInfo.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridPatInfo_CellDoubleClick);
			// 
			// ToolBarMain
			// 
			this.ToolBarMain.Dock = System.Windows.Forms.DockStyle.Top;
			this.ToolBarMain.ImageList = this.imageListMain;
			this.ToolBarMain.Location = new System.Drawing.Point(0, 0);
			this.ToolBarMain.Name = "ToolBarMain";
			this.ToolBarMain.Size = new System.Drawing.Size(939, 25);
			this.ToolBarMain.TabIndex = 47;
			this.ToolBarMain.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.ToolBarMain_ButtonClick);
			// 
			// textQuickCharge
			// 
			this.textQuickProcs.Location = new System.Drawing.Point(17, 3);
			this.textQuickProcs.Name = "textQuickCharge";
			this.textQuickProcs.Size = new System.Drawing.Size(100, 20);
			this.textQuickProcs.TabIndex = 220;
			this.textQuickProcs.Visible = false;
			// 
			// ContrAccount
			// 
			this.Controls.Add(this.textQuickProcs);
			this.Controls.Add(this.gridPatInfo);
			this.Controls.Add(this.groupBoxIndIns);
			this.Controls.Add(this.groupBoxFamilyIns);
			this.Controls.Add(this.gridPayPlan);
			this.Controls.Add(this.tabControlShow);
			this.Controls.Add(this.panelAging);
			this.Controls.Add(this.panelProgNotes);
			this.Controls.Add(this.gridRepeat);
			this.Controls.Add(this.gridAccount);
			this.Controls.Add(this.gridComm);
			this.Controls.Add(this.panelSplitter);
			this.Controls.Add(this.ToolBarMain);
			this.Name = "ContrAccount";
			this.Size = new System.Drawing.Size(939, 732);
			this.Load += new System.EventHandler(this.ContrAccount_Load);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.ContrAccount_Layout);
			this.Resize += new System.EventHandler(this.ContrAccount_Resize);
			this.panelProgNotes.ResumeLayout(false);
			this.groupBox7.ResumeLayout(false);
			this.groupBox6.ResumeLayout(false);
			this.panelAging.ResumeLayout(false);
			this.panelAging.PerformLayout();
			this.panelTotalOwes.ResumeLayout(false);
			this.tabControlShow.ResumeLayout(false);
			this.tabMain.ResumeLayout(false);
			this.tabShow.ResumeLayout(false);
			this.tabShow.PerformLayout();
			this.groupBoxIndIns.ResumeLayout(false);
			this.groupBoxIndIns.PerformLayout();
			this.groupBoxFamilyIns.ResumeLayout(false);
			this.groupBoxFamilyIns.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		///<summary></summary>
		public void InitializeOnStartup() {
			if(InitializedOnStartup && !ViewingInRecall) {
				return;
			}
			InitializedOnStartup=true;
			//can't use Lan.F(this);
			Lan.C(this,new Control[]
				{
          labelStartDate,
					labelEndDate,
					label2,
					label7,
					label6,
					label5,
					label3,
					labelUrgFinNote,
					labelFamFinancial,
					gridAccount,
					gridAcctPat,
					gridComm,
					gridPatInfo,
					gridPayPlan,
					gridProg,
					gridRepeat,
					labelInsEst,
					labelBalance,
					labelPatEstBal,
					labelUnearned,
					labelInsRem,
					tabMain,
					tabShow,
					butToday,
					but45days,
					but90days,
					butDatesAll,
					butRefresh
				});
			Lan.C(this,contextMenuIns,contextMenuStatement);
			LayoutToolBar();
			textQuickProcs.AcceptsTab=true;
			textQuickProcs.KeyDown+=textQuickCharge_KeyDown;
			textQuickProcs.MouseDown+=textQuickCharge_MouseClick;
			textQuickProcs.MouseCaptureChanged+=textQuickCharge_CaptureChange;
			textQuickProcs.LostFocus+=textQuickCharge_FocusLost;
			ToolBarMain.Controls.Add(textQuickProcs);
			if(ViewingInRecall) {
				panelSplitter.Top=300;//start the splitter higher for recall window.
			}
			//This just makes the patient information grid show up or not.
			_patInfoDisplayFields=DisplayFields.GetForCategory(DisplayFieldCategory.AccountPatientInformation);
			LayoutPanels();
			checkShowFamilyComm.Checked=PrefC.GetBoolSilent(PrefName.ShowAccountFamilyCommEntries,true);
			Plugins.HookAddCode(this,"ContrAccount.InitializeOnStartup_end");
		}

		private void textQuickCharge_MouseClick(object sender,MouseEventArgs e) {
			if(e.X<0 || e.X>textQuickProcs.Width ||e.Y<0 || e.Y>textQuickProcs.Height) {
				textQuickProcs.Text="";
				textQuickProcs.Visible=false;
				textQuickProcs.Capture=false;
			}
		}

		private void textQuickCharge_CaptureChange(object sender,EventArgs e) {
			if(textQuickProcs.Visible==true) {
				textQuickProcs.Capture=true;
			}
		}

		private void ContrAccount_Load(object sender,System.EventArgs e) {
			this.Parent.MouseWheel+=new MouseEventHandler(Parent_MouseWheel);
		}

		///<summary>Causes the toolbar to be laid out again.</summary>
		public void LayoutToolBar() {
			ToolBarMain.Buttons.Clear();
			ODToolBarButton button;
			button=new ODToolBarButton(Lan.g(this,"Payment"),1,"","Payment");
			//button.Style=ODToolBarButtonStyle.DropDownButton;
			//button.DropDownMenu=contextMenuPayment;
			ToolBarMain.Buttons.Add(button);
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Adjustment"),2,"","Adjustment"));
			button=new ODToolBarButton(Lan.g(this,"New Claim"),3,"","Insurance");
			button.Style=ODToolBarButtonStyle.DropDownButton;
			button.DropDownMenu=contextMenuIns;
			ToolBarMain.Buttons.Add(button);
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Payment Plan"),-1,"","PayPlan"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Installment Plan"),-1,"","InstallPlan"));
			if(Security.IsAuthorized(Permissions.AccountProcsQuickAdd,true)) {
				//If the user doesn't have permission to use the quick charge button don't add it to the toolbar.
				ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
				_butQuickProcs=new ODToolBarButton(Lan.g(this,"Quick Procs"),-1,"","QuickProcs");
				_butQuickProcs.Style=ODToolBarButtonStyle.DropDownButton;
				_butQuickProcs.DropDownMenu=contextMenuQuickProcs;
				contextMenuQuickProcs.Popup+=new EventHandler(contextMenuQuickProcs_Popup);
				ToolBarMain.Buttons.Add(_butQuickProcs);
			}
			if(!PrefC.GetBool(PrefName.EasyHideRepeatCharges)) {
				button=new ODToolBarButton(Lan.g(this,"Repeating Charge"),-1,"","RepeatCharge");
				button.Style=ODToolBarButtonStyle.PushButton;

				if(PrefC.GetBool(PrefName.DockPhonePanelShow)) {//contextMenuRepeat items only get initialized when at HQ.
					button.Style=ODToolBarButtonStyle.DropDownButton;
					button.DropDownMenu=contextMenuRepeat;
				}
				ToolBarMain.Buttons.Add(button);
			}
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			button=new ODToolBarButton(Lan.g(this,"Statement"),4,"","Statement");
			button.Style=ODToolBarButtonStyle.DropDownButton;
			button.DropDownMenu=contextMenuStatement;
			ToolBarMain.Buttons.Add(button);
			if(PrefC.GetBool(PrefName.AccountShowQuestionnaire)) {
				ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
				ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Questionnaire"),-1,"","Questionnaire"));
			}
			if(PrefC.GetBool(PrefName.AccountShowTrojanExpressCollect)) {
				ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
				ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"TrojanCollect"),-1,"","TrojanCollect"));
			}
			ProgramL.LoadToolbar(ToolBarMain,ToolBarsAvail.AccountModule);
			ToolBarMain.Invalidate();
			Plugins.HookAddCode(this,"ContrAccount.LayoutToolBar_end",PatCur);
		}

		///<summary>This gets run just prior to the contextMenuQuickCharge menu displaying to the user.</summary>
		private void contextMenuQuickProcs_Popup(object sender,EventArgs e) {
			//Dynamically fill contextMenuQuickCharge's menu items because the definitions may have changed since last time it was filled.
			_acctProcQuickAddDefs=DefC.GetList(DefCat.AccountQuickCharge);
			contextMenuQuickProcs.MenuItems.Clear();
			for(int i=0;i<_acctProcQuickAddDefs.Length;i++) {
				contextMenuQuickProcs.MenuItems.Add(new MenuItem(_acctProcQuickAddDefs[i].ItemName,menuItemQuickProcs_Click));
			}
			if(_acctProcQuickAddDefs.Length==0) {
				contextMenuQuickProcs.MenuItems.Add(new MenuItem(Lan.g(this,"No quick charge procedures defined. Go to Setup | Definitions to add."),(x,y) => { }));//"null" event handler.
			}
		}

		private void ContrAccount_Layout(object sender,System.Windows.Forms.LayoutEventArgs e) {
			//see LayoutPanels()
		}

		private void ContrAccount_Resize(object sender,EventArgs e) {
			if(PrefC.HListIsNull()){
				return;//helps on startup.
			}
			LayoutPanels();
		}

		///<summary>This used to be a layout event, but that was making it get called far too frequently.  Now, this must explicitly and intelligently be called.</summary>
		private void LayoutPanels(){
			gridAccount.Height=panelSplitter.Top-gridAccount.Location.Y+1;
			gridAccount.Invalidate();
			gridComm.Top=panelSplitter.Bottom-1;
			gridComm.Height=Height-gridComm.Top;
			gridComm.Invalidate();
			if(_patInfoDisplayFields!=null && _patInfoDisplayFields.Count>0) {
				gridPatInfo.Height=Height-gridPatInfo.Top;
				gridPatInfo.Invalidate();
				gridPatInfo.Visible=true;
			}
			else {
				gridPatInfo.Visible=false;
			}
			//panelCommButs.Top=Height-63;//panelSplitter.Bottom-1;
			panelProgNotes.Top=panelSplitter.Bottom-1;
			panelProgNotes.Height=Height-panelProgNotes.Top;
			gridProg.Top=0;
			gridProg.Height=panelProgNotes.Height;
			/*
			panelBoldBalance.Left=329;
			panelBoldBalance.Top=29;
			panelInsInfoDetail.Top = panelBoldBalance.Top + panelBoldBalance.Height;
			panelInsInfoDetail.Left = panelBoldBalance.Left + panelBoldBalance.Width - panelInsInfoDetail.Width;*/
			int left=textUrgFinNote.Left;//769;
			labelFamFinancial.Location=new Point(left,gridAcctPat.Bottom);
			textFinNotes.Location=new Point(left,labelFamFinancial.Bottom);
			//tabControlShow.Height=panelCommButs.Top-tabControlShow.Top;
			textFinNotes.Height=tabMain.Height-textFinNotes.Top;
		}

		///<summary></summary>
		public void ModuleSelected(long patNum) {
			ModuleSelected(patNum,false);
		}

		///<summary></summary>
		public void ModuleSelected(long patNum,bool isSelectingFamily) {
			RefreshModuleData(patNum,isSelectingFamily);
			RefreshModuleScreen(isSelectingFamily);
			Plugins.HookAddCode(this,"ContrAccount.ModuleSelected_end",patNum,isSelectingFamily);
		}

		///<summary>Used when jumping to this module and directly to a claim.</summary>
		public void ModuleSelected(long patNum,long claimNum) {
			ModuleSelected(patNum);
			DataTable table=DataSetMain.Tables["account"];
			for(int i=0;i<table.Rows.Count;i++){
				if(table.Rows[i]["ClaimPaymentNum"].ToString()!="0") {//claimpayment
					continue;
				}
				if(table.Rows[i]["ClaimNum"].ToString()=="0") {//not a claim or claimpayment
					continue;
				}
				long claimNumRow=PIn.Long(table.Rows[i]["ClaimNum"].ToString());
				if(claimNumRow!=claimNum){
					continue;
				}
				gridAccount.SetSelected(i,true);
			}
		}

		///<summary></summary>
		public void ModuleUnselected() {
			if(FamCur==null)
				return;
			if(UrgFinNoteChanged) {
				//Patient tempPat=Patients.Cur;
				Patient patOld=FamCur.ListPats[0].Copy();
				//Patients.CurOld=Patients.Cur.Clone();//important
				FamCur.ListPats[0].FamFinUrgNote=textUrgFinNote.Text;
				Patients.Update(FamCur.ListPats[0],patOld);
				//Patients.GetFamily(tempPat.PatNum);
				UrgFinNoteChanged=false;
			}
			if(FinNoteChanged) {
				PatientNoteCur.FamFinancial=textFinNotes.Text;
				PatientNotes.Update(PatientNoteCur, PatCur.Guarantor);
				FinNoteChanged=false;
			}
			//if(CCChanged){
			//  CCSave();
			//  CCChanged=false;
			//}
			FamCur=null;
			RepeatChargeList=null;
			_patNumLast=0;//Clear out the last pat num so that a security log gets entered that the module was "visited" or "refreshed".
			Plugins.HookAddCode(this,"ContrAccount.ModuleUnselected_end");
		}

		///<summary></summary>
		private void RefreshModuleData(long patNum,bool isSelectingFamily) {
			if (patNum == 0){
				PatCur=null;
				FamCur=null;
				DataSetMain=null;
				Plugins.HookAddCode(this,"ContrAccount.RefreshModuleData_null");
				return;
			}
			DateTime fromDate=DateTime.MinValue;
			DateTime toDate=DateTime.MaxValue;
			if(textDateStart.errorProvider1.GetError(textDateStart)==""
				&& textDateEnd.errorProvider1.GetError(textDateEnd)=="") {
				if(textDateStart.Text!="") {
					fromDate=PIn.Date(textDateStart.Text);
				}
				if(textDateEnd.Text!="") {
					toDate=PIn.Date(textDateEnd.Text);
				}
			}
			bool viewingInRecall=ViewingInRecall;
			if(PrefC.GetBool(PrefName.FuchsOptionsOn)) {
				panelTotalOwes.Top=-38;
				viewingInRecall=true;
			}
			DataSetMain=AccountModules.GetAll(patNum,viewingInRecall,fromDate,toDate,isSelectingFamily,checkShowDetail.Checked,true,true);
			FamCur=Patients.GetFamily(patNum);//for now, have to get family after dataset due to aging calc.
			PatCur=FamCur.GetPatient(patNum);
			PatientNoteCur=PatientNotes.Refresh(PatCur.PatNum,PatCur.Guarantor);
			_patFieldList=PatFields.Refresh(patNum);
			FillSummary();
			if(_patNumLast!=patNum) {
				SecurityLogs.MakeLogEntry(Permissions.AccountModule,patNum,"");
				_patNumLast=patNum;
			}
			Plugins.HookAddCode(this,"ContrAccount.RefreshModuleData_end",FamCur,PatCur,DataSetMain,PPBalanceTotal,isSelectingFamily);
		}

		private void RefreshModuleScreen(bool isSelectingFamily) {
			if(PatCur==null) {
				gridAccount.Enabled=false;
				ToolBarMain.Buttons["Payment"].Enabled=false;
				ToolBarMain.Buttons["Adjustment"].Enabled=false;
				ToolBarMain.Buttons["Insurance"].Enabled=false;
				ToolBarMain.Buttons["PayPlan"].Enabled=false;
				ToolBarMain.Buttons["InstallPlan"].Enabled=false;
				if(ToolBarMain.Buttons["QuickProcs"]!=null) {
					ToolBarMain.Buttons["QuickProcs"].Enabled=false;
				}
				if(ToolBarMain.Buttons["RepeatCharge"]!=null) {
					ToolBarMain.Buttons["RepeatCharge"].Enabled=false;
				}
				ToolBarMain.Buttons["Statement"].Enabled=false;
				if(ToolBarMain.Buttons["Questionnaire"]!=null && PrefC.GetBool(PrefName.AccountShowQuestionnaire)) {
					ToolBarMain.Buttons["Questionnaire"].Enabled=false;
				}
				if(ToolBarMain.Buttons["TrojanCollect"]!=null && PrefC.GetBool(PrefName.AccountShowTrojanExpressCollect)) {
					ToolBarMain.Buttons["TrojanCollect"].Enabled=false;
				}
				ToolBarMain.Invalidate();
				textUrgFinNote.Enabled=false;
				textFinNotes.Enabled=false;
				//butComm.Enabled=false;
				tabControlShow.Enabled=false;
				Plugins.HookAddCode(this,"ContrAccount.RefreshModuleScreen_null");
			}
			else{
				gridAccount.Enabled=true;
				ToolBarMain.Buttons["Payment"].Enabled=true;
				ToolBarMain.Buttons["Adjustment"].Enabled=true;
				ToolBarMain.Buttons["Insurance"].Enabled=true;
				ToolBarMain.Buttons["PayPlan"].Enabled=true;
				ToolBarMain.Buttons["InstallPlan"].Enabled=true;
				if(ToolBarMain.Buttons["QuickProcs"]!=null) {
					ToolBarMain.Buttons["QuickProcs"].Enabled=true;
				}
				if(ToolBarMain.Buttons["RepeatCharge"]!=null) {
					ToolBarMain.Buttons["RepeatCharge"].Enabled=true;
				} 
				ToolBarMain.Buttons["Statement"].Enabled=true;
				if(ToolBarMain.Buttons["Questionnaire"]!=null && PrefC.GetBool(PrefName.AccountShowQuestionnaire)) {
					ToolBarMain.Buttons["Questionnaire"].Enabled=true;
				}
				if(ToolBarMain.Buttons["TrojanCollect"]!=null && PrefC.GetBool(PrefName.AccountShowTrojanExpressCollect)) {
					ToolBarMain.Buttons["TrojanCollect"].Enabled=true;
				}
				ToolBarMain.Invalidate();
				textUrgFinNote.Enabled=true;
				textFinNotes.Enabled=true;
				//butComm.Enabled=true;
				tabControlShow.Enabled=true;
			}
			FillPats(isSelectingFamily);
			FillMisc();
			FillAging(isSelectingFamily);
			FillRepeatCharges();//must be in this order. 1.
			FillPaymentPlans();//2.
			FillMain();//3.
			FillPatInfo();
			LayoutPanels();
			if(ViewingInRecall || PrefC.GetBoolSilent(PrefName.FuchsOptionsOn,false)) {
				panelProgNotes.Visible = true;
				FillProgNotes();
				if(PrefC.GetBool(PrefName.FuchsOptionsOn) && PatCur!=null){//show prog note options
					groupBox6.Visible = true;
					groupBox7.Visible = true;
					butShowAll.Visible = true;
					butShowNone.Visible = true;
					//FillInsInfo();
				}
			}
			else{
				panelProgNotes.Visible = false;
				FillComm();
			}
			Plugins.HookAddCode(this,"ContrAccount.RefreshModuleScreen_end",FamCur,PatCur,DataSetMain,PPBalanceTotal,isSelectingFamily);
		}

		///<summary>Call this before inserting new repeat charge to update patient.BillingCycleDay if no other repeat charges exist.
		///Changes the patient's BillingCycleDay to today if no other active repeat charges are on the patient's account</summary>
		private void UpdatePatientBillingDay(long patNum) {
			if(RepeatCharges.ActiveRepeatChargeExists(patNum)) {
				return;
			}
			Patient patOld=Patients.GetPat(patNum);
			if(patOld.BillingCycleDay==DateTimeOD.Today.Day) {
				return;
			}
			Patient patNew=patOld.Copy();
			patNew.BillingCycleDay=DateTimeOD.Today.Day;
			Patients.Update(patNew,patOld);
		}

		//private void FillPatientButton() {
		//	Patients.AddPatsToMenu(menuPatient,new EventHandler(menuPatient_Click),PatCur,FamCur);
		//}

		private void FillPats(bool isSelectingFamily) {
			if(PatCur==null) {
				gridAcctPat.BeginUpdate();
				gridAcctPat.Rows.Clear();
				gridAcctPat.EndUpdate();
				return;
			}
			gridAcctPat.BeginUpdate();
			gridAcctPat.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableAccountPat","Patient"),105);
			gridAcctPat.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableAccountPat","Bal"),49,HorizontalAlignment.Right);
			gridAcctPat.Columns.Add(col);
			gridAcctPat.Rows.Clear();
			ODGridRow row;
			DataTable table=DataSetMain.Tables["patient"];
			decimal bal=0;
			for(int i=0;i<table.Rows.Count;i++) {
				bal+=(decimal)table.Rows[i]["balanceDouble"];
				row = new ODGridRow();
				row.Cells.Add(table.Rows[i]["name"].ToString());
				row.Cells.Add(table.Rows[i]["balance"].ToString());
				if(i==0 || i==table.Rows.Count-1) {
					row.Bold=true;
				}
				gridAcctPat.Rows.Add(row);
			}
			gridAcctPat.EndUpdate();
			if(isSelectingFamily){
				gridAcctPat.SetSelected(FamCur.ListPats.Length,true);
			}
			else{
				for(int i=0;i<FamCur.ListPats.Length;i++) {
					if(FamCur.ListPats[i].PatNum==PatCur.PatNum) {
						gridAcctPat.SetSelected(i,true);
					}
				}
			}
			if(isSelectingFamily){
				ToolBarMain.Buttons["Insurance"].Enabled=false;
			}
			else{
				ToolBarMain.Buttons["Insurance"].Enabled=true;
			}
		}

		private void FillMisc() {
			//textCC.Text="";
			//textCCexp.Text="";
			if(PatCur==null) {
				textUrgFinNote.Text="";
				textFinNotes.Text="";
			}
			else{
				textUrgFinNote.Text=FamCur.ListPats[0].FamFinUrgNote;
				textFinNotes.Text=PatientNoteCur.FamFinancial;
				if(!textFinNotes.Focused) {
					textFinNotes.SelectionStart=textFinNotes.Text.Length;
					//This will cause a crash if the richTextBox currently has focus. We don't know why.
					//Only happens if you call this during a Leave event, and only when moving between two ODtextBoxes.
					//Tested with two ordinary richTextBoxes, and the problem does not exist.
					//We may pursue fixing the root problem some day, but this workaround will do for now.
					textFinNotes.ScrollToCaret();
				}
				if(!textUrgFinNote.Focused) {
					textUrgFinNote.SelectionStart=0;
					textUrgFinNote.ScrollToCaret();
				}
				//if(PrefC.GetBool(PrefName.StoreCCnumbers)) {
					//string cc=PatientNoteCur.CCNumber;
					//if(Regex.IsMatch(cc,@"^\d{16}$")){
					//  textCC.Text=cc.Substring(0,4)+"-"+cc.Substring(4,4)+"-"+cc.Substring(8,4)+"-"+cc.Substring(12,4);
					//}
					//else{
					//  textCC.Text=cc;
					//}
					//if(PatientNoteCur.CCExpiration.Year>2000){
					//  textCCexp.Text=PatientNoteCur.CCExpiration.ToString("MM/yy");
					//}
					//else{
					//  textCCexp.Text="";
					//}
				//}
			}
			UrgFinNoteChanged=false;
			FinNoteChanged=false;
			CCChanged=false;
			if(ViewingInRecall) {
				textUrgFinNote.ReadOnly=true;
				textFinNotes.ReadOnly=true;
			}
			else {
				textUrgFinNote.ReadOnly=false;
				textFinNotes.ReadOnly=false;
			}
		}

		private void FillAging(bool isSelectingFamily) {
			if(Plugins.HookMethod(this,"ContrAccount.FillAging",FamCur,PatCur,DataSetMain,isSelectingFamily)) {
				return;
			}
			if(PatCur!=null) {
				textOver90.Text=FamCur.ListPats[0].BalOver90.ToString("F");
				text61_90.Text=FamCur.ListPats[0].Bal_61_90.ToString("F");
				text31_60.Text=FamCur.ListPats[0].Bal_31_60.ToString("F");
				text0_30.Text=FamCur.ListPats[0].Bal_0_30.ToString("F");
				decimal total=(decimal)FamCur.ListPats[0].BalTotal;
				labelTotalAmt.Text=total.ToString("F");
				labelInsEstAmt.Text=FamCur.ListPats[0].InsEst.ToString("F");
				labelBalanceAmt.Text = (total - (decimal)FamCur.ListPats[0].InsEst).ToString("F");
				labelPatEstBalAmt.Text="";
				DataTable tableMisc=DataSetMain.Tables["misc"];
				if(!isSelectingFamily){
					for(int i=0;i<tableMisc.Rows.Count;i++){
						if(tableMisc.Rows[i]["descript"].ToString()=="patInsEst"){
							decimal estBal=(decimal)PatCur.EstBalance-PIn.Decimal(tableMisc.Rows[i]["value"].ToString());
							labelPatEstBalAmt.Text=estBal.ToString("F");
						}
					}
				}
				labelUnearnedAmt.Text="";
				for(int i=0;i<tableMisc.Rows.Count;i++){
					if(tableMisc.Rows[i]["descript"].ToString()=="unearnedIncome") {
						decimal unearned=PIn.Decimal(tableMisc.Rows[i]["value"].ToString());
						if(unearned!=0) {
							labelUnearnedAmt.Text=unearned.ToString("F");
						}
					}
				}
				//labelInsLeft.Text=Lan.g(this,"Ins Left");
				//labelInsLeftAmt.Text="";//etc. Will be same for everyone
				Font fontBold=new Font(FontFamily.GenericSansSerif,11,FontStyle.Bold);
				//In the new way of doing it, they are all visible and calculated identically,
				//but the emphasis simply changes by slight renaming of labels
				//and by font size changes.
				if(PrefC.GetBool(PrefName.BalancesDontSubtractIns)){
					labelTotal.Text=Lan.g(this,"Balance");
					labelTotalAmt.Font=fontBold;
					labelTotalAmt.ForeColor=Color.Firebrick;
					panelAgeLine.Visible=true;//verical line
					labelInsEst.Text=Lan.g(this,"Ins Pending");
					labelBalance.Text=Lan.g(this,"After Ins");
					labelBalanceAmt.Font=this.Font;
					labelBalanceAmt.ForeColor=Color.Black;
				}
				else{//this is more common
					labelTotal.Text=Lan.g(this,"Total");
					labelTotalAmt.Font=this.Font;
					labelTotalAmt.ForeColor = Color.Black;
					panelAgeLine.Visible=false;
					labelInsEst.Text=Lan.g(this,"-InsEst");
					labelBalance.Text=Lan.g(this,"=Est Bal");
					labelBalanceAmt.Font=fontBold;
					labelBalanceAmt.ForeColor=Color.Firebrick;
					if(PrefC.GetBool(PrefName.FuchsOptionsOn)){
						labelTotal.Text=Lan.g(this,"Balance");
						labelBalance.Text=Lan.g(this,"=Owed Now");
						labelTotalAmt.Font = fontBold;
					}
				}
			}
			else {
				textOver90.Text="";
				text61_90.Text="";
				text31_60.Text="";
				text0_30.Text="";
				labelTotalAmt.Text="";
				labelInsEstAmt.Text="";
				labelBalanceAmt.Text="";
				labelPatEstBalAmt.Text="";
				labelUnearnedAmt.Text="";
				//labelInsLeftAmt.Text="";
			}
		}

		///<summary></summary>
		private void FillRepeatCharges() {
			if(PatCur==null) {
				gridRepeat.Visible=false;
				return;
			}
			RepeatChargeList=RepeatCharges.Refresh(PatCur.PatNum);
			if(RepeatChargeList.Length==0) {
				gridRepeat.Visible=false;
				return;
			}
			if(PrefC.GetBool(PrefName.BillingUseBillingCycleDay)) {
				gridRepeat.Title=Lan.g(gridRepeat,"Repeat Charges")+" - Billing Day "+PatCur.BillingCycleDay;
			}
			else {
				gridRepeat.Title=Lan.g(gridRepeat,"Repeat Charges");
			}
			gridRepeat.Visible=true;
			gridRepeat.Height=92;//=140;
			gridRepeat.BeginUpdate();
			gridRepeat.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableRepeatCharges","Description"),150);
			gridRepeat.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRepeatCharges","Amount"),60,HorizontalAlignment.Right);
			gridRepeat.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRepeatCharges","Start Date"),70,HorizontalAlignment.Center);
			gridRepeat.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRepeatCharges","Stop Date"),70,HorizontalAlignment.Center);
			gridRepeat.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRepeatCharges","Enabled"),55,HorizontalAlignment.Center);
			gridRepeat.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRepeatCharges","Note"),355);
			gridRepeat.Columns.Add(col);
			gridRepeat.Rows.Clear();
			UI.ODGridRow row;
			ProcedureCode procCode;
			for(int i=0;i<RepeatChargeList.Length;i++) {
				row=new ODGridRow();
				procCode=ProcedureCodes.GetProcCode(RepeatChargeList[i].ProcCode);
				row.Cells.Add(procCode.Descript);
				row.Cells.Add(RepeatChargeList[i].ChargeAmt.ToString("F"));
				if(RepeatChargeList[i].DateStart.Year>1880) {
					row.Cells.Add(RepeatChargeList[i].DateStart.ToShortDateString());
				}
				else {
					row.Cells.Add("");
				}
				if(RepeatChargeList[i].DateStop.Year>1880) {
					row.Cells.Add(RepeatChargeList[i].DateStop.ToShortDateString());
				}
				else {
					row.Cells.Add("");
				}
				if(RepeatChargeList[i].IsEnabled) {
					row.Cells.Add("X");
				}
				else {
					row.Cells.Add("");
				}
				row.Cells.Add(RepeatChargeList[i].Note);
				gridRepeat.Rows.Add(row);
			}
			gridRepeat.EndUpdate();
		}

		private void FillPaymentPlans(){
			PPBalanceTotal=0;
			if(PatCur==null) {
				gridPayPlan.Visible=false;
				return;
			}
			DataTable table=DataSetMain.Tables["payplan"];
			if (table.Rows.Count == 0) {
				gridPayPlan.Visible=false;
				return;
			}
			if(gridRepeat.Visible){
				gridPayPlan.Location=new Point(0,gridRepeat.Bottom+3);
			}
			else{
				gridPayPlan.Location=gridRepeat.Location;
			}
			gridPayPlan.Visible = true;
			gridPayPlan.Height=100;
			gridPayPlan.BeginUpdate();
			gridPayPlan.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TablePaymentPlans","Date"),65);
			gridPayPlan.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaymentPlans","Guarantor"),100);
			gridPayPlan.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaymentPlans","Patient"),100);
			gridPayPlan.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaymentPlans","Type"),30,HorizontalAlignment.Center);
			gridPayPlan.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaymentPlans","Principal"),70,HorizontalAlignment.Right);
			gridPayPlan.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaymentPlans","Total Cost"),70,HorizontalAlignment.Right);
			gridPayPlan.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaymentPlans","Paid"),70,HorizontalAlignment.Right);
			gridPayPlan.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaymentPlans","PrincPaid"),70,HorizontalAlignment.Right);
			gridPayPlan.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaymentPlans","Balance"),70,HorizontalAlignment.Right);
			gridPayPlan.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaymentPlans","Due Now"),70,HorizontalAlignment.Right);
			gridPayPlan.Columns.Add(col);
			col=new ODGridColumn("",70);//filler
			gridPayPlan.Columns.Add(col);
			gridPayPlan.Rows.Clear();
			UI.ODGridRow row;
			UI.ODGridCell cell;
			for(int i=0;i<table.Rows.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(table.Rows[i]["date"].ToString());
				if(table.Rows[i]["InstallmentPlanNum"].ToString()!="0" && table.Rows[i]["PatNum"].ToString()!=PatCur.Guarantor.ToString()) {//Installment plan and not on guar
					cell=new ODGridCell(((string)"Invalid Guarantor"));
					cell.Bold=YN.Yes;
					cell.ColorText=Color.Red;
				}
				else {
					cell=new ODGridCell(table.Rows[i]["guarantor"].ToString());
				}
				row.Cells.Add(cell);
				row.Cells.Add(table.Rows[i]["patient"].ToString());
				row.Cells.Add(table.Rows[i]["type"].ToString());
				row.Cells.Add(table.Rows[i]["principal"].ToString());
				row.Cells.Add(table.Rows[i]["totalCost"].ToString());
				row.Cells.Add(table.Rows[i]["paid"].ToString());
				row.Cells.Add(table.Rows[i]["princPaid"].ToString());
				row.Cells.Add(table.Rows[i]["balance"].ToString());
				cell=new ODGridCell(table.Rows[i]["due"].ToString());
				if(table.Rows[i]["type"].ToString()!="Ins"){
					cell.Bold=YN.Yes;
					cell.ColorText=Color.Red;
				}
				row.Cells.Add(cell);
				row.Cells.Add("");
				gridPayPlan.Rows.Add(row);
				PPBalanceTotal += (Convert.ToDecimal(PIn.Double(table.Rows[i]["balance"].ToString())));
			}
			gridPayPlan.EndUpdate();
			if(PrefC.GetBool(PrefName.FuchsOptionsOn)) {
				panelTotalOwes.Top=1;
				labelTotalPtOwes.Text=(PPBalanceTotal + (decimal)FamCur.ListPats[0].BalTotal - (decimal)FamCur.ListPats[0].InsEst).ToString("F");
			}
		}

		/// <summary>Fills the commlog grid on this form.  It does not refresh the data from the database.</summary>
		private void FillComm() {
			if (DataSetMain == null) {
				gridComm.BeginUpdate();
				gridComm.Rows.Clear();
				gridComm.EndUpdate();
				return;
			}
			bool isSelectingFamily=gridAcctPat.GetSelectedIndex()==this.DataSetMain.Tables["patient"].Rows.Count-1;
			gridComm.BeginUpdate();
			gridComm.Columns.Clear();
			ODGridColumn col = new ODGridColumn(Lan.g("TableCommLogAccount", "Date"), 70);
			gridComm.Columns.Add(col);
			col = new ODGridColumn(Lan.g("TableCommLogAccount","Time"),42);//,HorizontalAlignment.Right);
			gridComm.Columns.Add(col);
			col = new ODGridColumn(Lan.g("TableCommLogAccount","Name"),80);
			gridComm.Columns.Add(col);
			col = new ODGridColumn(Lan.g("TableCommLogAccount", "Type"), 80);
			gridComm.Columns.Add(col);
			col = new ODGridColumn(Lan.g("TableCommLogAccount", "Mode"), 55);
			gridComm.Columns.Add(col);
			//col = new ODGridColumn(Lan.g("TableCommLogAccount", "Sent/Recd"), 75);
			//gridComm.Columns.Add(col);
			col = new ODGridColumn(Lan.g("TableCommLogAccount", "Note"), 455);
			gridComm.Columns.Add(col);
			gridComm.Rows.Clear();
			OpenDental.UI.ODGridRow row;
			DataTable table = DataSetMain.Tables["Commlog"];
			for(int i=0;i<table.Rows.Count;i++) {
				//Skip commlog entries which belong to other family members per user option.
				if(!this.checkShowFamilyComm.Checked										//show family not checked
					&& !isSelectingFamily																	//family not selected
					&& table.Rows[i]["PatNum"].ToString()!=PatCur.PatNum.ToString()	//not this patient
					&& table.Rows[i]["FormPatNum"].ToString()=="0")				//not a questionnaire (FormPat)
				{
					continue;
				}
				row = new ODGridRow();
				row.Cells.Add(table.Rows[i]["commDate"].ToString());
				row.Cells.Add(table.Rows[i]["commTime"].ToString());
				if(isSelectingFamily) {
					row.Cells.Add(table.Rows[i]["patName"].ToString());
				}
				else {//one patient
					if(table.Rows[i]["PatNum"].ToString()==PatCur.PatNum.ToString()) {//if this patient
						row.Cells.Add("");
					}
					else {//other patient
						row.Cells.Add(table.Rows[i]["patName"].ToString());
					}
				}
				row.Cells.Add(table.Rows[i]["commType"].ToString());
				row.Cells.Add(table.Rows[i]["mode"].ToString());
				//row.Cells.Add(table.Rows[i]["sentOrReceived"].ToString());
				row.Cells.Add(table.Rows[i]["Note"].ToString());
				row.Tag=i;
				gridComm.Rows.Add(row);
			}
			gridComm.EndUpdate();
			gridComm.ScrollToEnd();
		}

		private void FillMain(){
			if(gridPayPlan.Visible){
				gridAccount.Location=new Point(0,gridPayPlan.Bottom+3);
			}
			else if(gridRepeat.Visible){
				gridAccount.Location=new Point(0,gridRepeat.Bottom+3);
			}
			else{
				gridAccount.Location=gridRepeat.Location;
			}
			gridAccount.BeginUpdate();
			gridAccount.Columns.Clear();
			ODGridColumn col;
			fieldsForMainGrid=DisplayFields.GetForCategory(DisplayFieldCategory.AccountModule);
			HorizontalAlignment align;
			for(int i=0;i<fieldsForMainGrid.Count;i++) {
				align=HorizontalAlignment.Left;
				if(fieldsForMainGrid[i].InternalName=="Charges"
					|| fieldsForMainGrid[i].InternalName=="Credits"
					|| fieldsForMainGrid[i].InternalName=="Balance") 
				{
					align=HorizontalAlignment.Right;
				}
				if(fieldsForMainGrid[i].InternalName=="Signed") {
					align=HorizontalAlignment.Center;
				}
				if(fieldsForMainGrid[i].Description=="") {
					col=new ODGridColumn(fieldsForMainGrid[i].InternalName,fieldsForMainGrid[i].ColumnWidth,align);
				}
				else {
					col=new ODGridColumn(fieldsForMainGrid[i].Description,fieldsForMainGrid[i].ColumnWidth,align);
				}
				gridAccount.Columns.Add(col);
			}
			gridAccount.Rows.Clear();
			ODGridRow row;
			DataTable table=null;
			if(PatCur==null){
				table=new DataTable();
			}
			else{
				table=DataSetMain.Tables["account"];
			}
			for(int i=0;i<table.Rows.Count;i++) {
				row=new ODGridRow();
				for(int f=0;f<fieldsForMainGrid.Count;f++) {
					switch(fieldsForMainGrid[f].InternalName) {
						case "Date":
							row.Cells.Add(table.Rows[i]["date"].ToString());
							break;
						case "Patient":
							row.Cells.Add(table.Rows[i]["patient"].ToString());
							break;
						case "Prov":
							row.Cells.Add(table.Rows[i]["prov"].ToString());
							break;
						case "Clinic":
							row.Cells.Add(table.Rows[i]["clinic"].ToString());
							break;
						case "Code":
							row.Cells.Add(table.Rows[i]["ProcCode"].ToString());
							break;
						case "Tth":
							row.Cells.Add(table.Rows[i]["tth"].ToString());
							break;
						case "Description":
							row.Cells.Add(table.Rows[i]["description"].ToString());
							break;
						case "Charges":
							row.Cells.Add(table.Rows[i]["charges"].ToString());
							break;
						case "Credits":
							row.Cells.Add(table.Rows[i]["credits"].ToString());
							break;
						case "Balance":
							row.Cells.Add(table.Rows[i]["balance"].ToString());
							break;
						case "Signed":
							Procedure proc=Procedures.GetOneProc(PIn.Long(table.Rows[i]["ProcNum"].ToString()),true);
							if(!proc.IsNew && !String.IsNullOrWhiteSpace(proc.Signature)) {
								row.Cells.Add("Signed");
							}
							else {
								row.Cells.Add("");
							}
							break;
						case "Abbr":
							if(!String.IsNullOrEmpty(table.Rows[i]["AbbrDesc"].ToString())) {
								row.Cells.Add(table.Rows[i]["AbbrDesc"].ToString());
							}
							else {
								row.Cells.Add("");
							}
							break;
					}
				}
				row.ColorText=Color.FromArgb(PIn.Int(table.Rows[i]["colorText"].ToString()));
				if(i==table.Rows.Count-1//last row
					|| (DateTime)table.Rows[i]["DateTime"]!=(DateTime)table.Rows[i+1]["DateTime"])
				{
					row.ColorLborder=Color.Black;
				}
				gridAccount.Rows.Add(row);
			}
			gridAccount.EndUpdate();
			if(Actscrollval==0) {
				gridAccount.ScrollToEnd();
			}
			else {
				gridAccount.ScrollValue=Actscrollval;
				Actscrollval=0;
			}
		}

		private void FillSummary() {
			textFamPriMax.Text="";
			textFamPriDed.Text="";
			textFamSecMax.Text="";
			textFamSecDed.Text="";
			textPriMax.Text="";
			textPriDed.Text="";
			textPriDedRem.Text="";
			textPriUsed.Text="";
			textPriPend.Text="";
			textPriRem.Text="";
			textSecMax.Text="";
			textSecDed.Text="";
			textSecDedRem.Text="";
			textSecUsed.Text="";
			textSecPend.Text="";
			textSecRem.Text="";
			if(PatCur==null) {
				return;
			}
			double maxFam=0;
			double maxInd=0;
			double ded=0;
			double dedFam=0;
			double dedRem=0;
			double remain=0;
			double pend=0;
			double used=0;
			InsPlan PlanCur;
			InsSub SubCur;
			List<InsSub> subList=InsSubs.RefreshForFam(FamCur);
			List<InsPlan> InsPlanList=InsPlans.RefreshForSubList(subList);
			List<PatPlan> PatPlanList=PatPlans.Refresh(PatCur.PatNum);
			List<Benefit> BenefitList=Benefits.Refresh(PatPlanList,subList);
			List<Claim> ClaimList=Claims.Refresh(PatCur.PatNum);
			List<ClaimProcHist> HistList=ClaimProcs.GetHistList(PatCur.PatNum,BenefitList,PatPlanList,InsPlanList,DateTimeOD.Today,subList);
			if(PatPlanList.Count>0) {
				SubCur=InsSubs.GetSub(PatPlanList[0].InsSubNum,subList);
				PlanCur=InsPlans.GetPlan(SubCur.PlanNum,InsPlanList);
				pend=InsPlans.GetPendingDisplay(HistList,DateTime.Today,PlanCur,PatPlanList[0].PatPlanNum,-1,PatCur.PatNum,PatPlanList[0].InsSubNum,BenefitList);
				used=InsPlans.GetInsUsedDisplay(HistList,DateTime.Today,PlanCur.PlanNum,PatPlanList[0].PatPlanNum,-1,InsPlanList,BenefitList,PatCur.PatNum,PatPlanList[0].InsSubNum);
				textPriPend.Text=pend.ToString("F");
				textPriUsed.Text=used.ToString("F");
				maxFam=Benefits.GetAnnualMaxDisplay(BenefitList,PlanCur.PlanNum,PatPlanList[0].PatPlanNum,true);
				maxInd=Benefits.GetAnnualMaxDisplay(BenefitList,PlanCur.PlanNum,PatPlanList[0].PatPlanNum,false);
				if(maxFam==-1) {
					textFamPriMax.Text="";
				}
				else {
					textFamPriMax.Text=maxFam.ToString("F");
				}
				if(maxInd==-1) {//if annual max is blank
					textPriMax.Text="";
					textPriRem.Text="";
				}
				else {
					remain=maxInd-used-pend;
					if(remain<0) {
						remain=0;
					}
					//textFamPriMax.Text=max.ToString("F");
					textPriMax.Text=maxInd.ToString("F");
					textPriRem.Text=remain.ToString("F");
				}
				//deductible:
				ded=Benefits.GetDeductGeneralDisplay(BenefitList,PlanCur.PlanNum,PatPlanList[0].PatPlanNum,BenefitCoverageLevel.Individual);
				dedFam=Benefits.GetDeductGeneralDisplay(BenefitList,PlanCur.PlanNum,PatPlanList[0].PatPlanNum,BenefitCoverageLevel.Family);
				if(ded!=-1) {
					textPriDed.Text=ded.ToString("F");
					dedRem=InsPlans.GetDedRemainDisplay(HistList,DateTime.Today,PlanCur.PlanNum,PatPlanList[0].PatPlanNum,-1,InsPlanList,PatCur.PatNum,ded,dedFam);
					textPriDedRem.Text=dedRem.ToString("F");
				}
				if(dedFam!=-1) {
					textFamPriDed.Text=dedFam.ToString("F");
				}
			}
			if(PatPlanList.Count>1) {
				SubCur=InsSubs.GetSub(PatPlanList[1].InsSubNum,subList);
				PlanCur=InsPlans.GetPlan(SubCur.PlanNum,InsPlanList);
				pend=InsPlans.GetPendingDisplay(HistList,DateTime.Today,PlanCur,PatPlanList[1].PatPlanNum,-1,PatCur.PatNum,PatPlanList[1].InsSubNum,BenefitList);
				textSecPend.Text=pend.ToString("F");
				used=InsPlans.GetInsUsedDisplay(HistList,DateTime.Today,PlanCur.PlanNum,PatPlanList[1].PatPlanNum,-1,InsPlanList,BenefitList,PatCur.PatNum,PatPlanList[1].InsSubNum);
				textSecUsed.Text=used.ToString("F");
				//max=Benefits.GetAnnualMaxDisplay(BenefitList,PlanCur.PlanNum,PatPlanList[1].PatPlanNum);
				maxFam=Benefits.GetAnnualMaxDisplay(BenefitList,PlanCur.PlanNum,PatPlanList[1].PatPlanNum,true);
				maxInd=Benefits.GetAnnualMaxDisplay(BenefitList,PlanCur.PlanNum,PatPlanList[1].PatPlanNum,false);
				if(maxFam==-1) {
					textFamSecMax.Text="";
				}
				else {
					textFamSecMax.Text=maxFam.ToString("F");
				}
				if(maxInd==-1) {//if annual max is blank
					textSecMax.Text="";
					textSecRem.Text="";
				}
				else {
					remain=maxInd-used-pend;
					if(remain<0) {
						remain=0;
					}
					//textFamSecMax.Text=max.ToString("F");
					textSecMax.Text=maxInd.ToString("F");
					textSecRem.Text=remain.ToString("F");
				}
				//deductible:
				ded=Benefits.GetDeductGeneralDisplay(BenefitList,PlanCur.PlanNum,PatPlanList[1].PatPlanNum,BenefitCoverageLevel.Individual);
				dedFam=Benefits.GetDeductGeneralDisplay(BenefitList,PlanCur.PlanNum,PatPlanList[1].PatPlanNum,BenefitCoverageLevel.Family);
				if(ded!=-1) {
					textSecDed.Text=ded.ToString("F");
					dedRem=InsPlans.GetDedRemainDisplay(HistList,DateTime.Today,PlanCur.PlanNum,PatPlanList[1].PatPlanNum,-1,InsPlanList,PatCur.PatNum,ded,dedFam);
					textSecDedRem.Text=dedRem.ToString("F");
				}
				if(dedFam!=-1) {
					textFamSecDed.Text=dedFam.ToString("F");
				}
			}
		}

		private void FillPatInfo() {
			if(PatCur==null) {
				gridPatInfo.BeginUpdate();
				gridPatInfo.Rows.Clear();
				gridPatInfo.Columns.Clear();
				gridPatInfo.EndUpdate();
				return;
			}
			gridPatInfo.BeginUpdate();
			gridPatInfo.Columns.Clear();
			ODGridColumn col=new ODGridColumn("",80);
			gridPatInfo.Columns.Add(col);
			col=new ODGridColumn("",150);
			gridPatInfo.Columns.Add(col);
			gridPatInfo.Rows.Clear();
			ODGridRow row;
			_patInfoDisplayFields=DisplayFields.GetForCategory(DisplayFieldCategory.AccountPatientInformation);
			for(int f=0;f<_patInfoDisplayFields.Count;f++) {
				row=new ODGridRow();
				if(_patInfoDisplayFields[f].Description=="") {
					if(_patInfoDisplayFields[f].InternalName=="PatFields") {
						//don't add a cell
					}
					else {
						row.Cells.Add(_patInfoDisplayFields[f].InternalName);
					}
				}
				else {
					if(_patInfoDisplayFields[f].InternalName=="PatFields") {
						//don't add a cell
					}
					else {
						row.Cells.Add(_patInfoDisplayFields[f].Description);
					}
				}
				switch(_patInfoDisplayFields[f].InternalName) {
					case "Billing Type":
						row.Cells.Add(DefC.GetName(DefCat.BillingTypes,PatCur.BillingType));
						break;
					case "PatFields":
						PatField field;
						for(int i=0;i<PatFieldDefs.ListShort.Count;i++) {
							if(i>0) {
								row=new ODGridRow();
							}
							row.Cells.Add(PatFieldDefs.ListShort[i].FieldName);
							field=PatFields.GetByName(PatFieldDefs.ListShort[i].FieldName,_patFieldList);
							if(field==null) {
								row.Cells.Add("");
							}
							else {
								if(PatFieldDefs.ListShort[i].FieldType==PatFieldType.Checkbox) {
									row.Cells.Add("X");
								}
								else if(PatFieldDefs.ListShort[i].FieldType==PatFieldType.Currency) {
									row.Cells.Add(PIn.Double(field.FieldValue).ToString("c"));
								}
								else {
									row.Cells.Add(field.FieldValue);
								}
							}
							row.Tag="PatField"+i.ToString();
							gridPatInfo.Rows.Add(row);
						}
						break;
				}
				if(_patInfoDisplayFields[f].InternalName=="PatFields") {
					//don't add the row here
				}
				else {
					gridPatInfo.Rows.Add(row);
				}
			}
			gridPatInfo.EndUpdate();
		}

		private void gridAccount_CellClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			DataTable table=DataSetMain.Tables["account"];
			//this seems to fire after a doubleclick, so this prevents error:
			if(e.Row>=table.Rows.Count){
				return;
			}
			if(ViewingInRecall) return;
			if(table.Rows[e.Row]["ClaimNum"].ToString()!="0"){//claims and claimpayments
				//Claim ClaimCur=Claims.GetClaim(
				//	arrayClaim[AcctLineList[e.Row].Index];
				string[] procsOnClaim=table.Rows[e.Row]["procsOnObj"].ToString().Split(',');
				for(int i=0;i<table.Rows.Count;i++){//loop through all rows
					if(table.Rows[i]["ClaimNum"].ToString()==table.Rows[e.Row]["ClaimNum"].ToString()){
						gridAccount.SetSelected(i,true);//for the claim payments
					}
					else if(table.Rows[i]["ProcNum"].ToString()=="0"){//if not a procedure, then skip
						continue;
					}
					for(int j=0;j<procsOnClaim.Length;j++){
						if(table.Rows[i]["ProcNum"].ToString()==procsOnClaim[j]){
							gridAccount.SetSelected(i,true);
						}
					}
				}
			}
			if(table.Rows[e.Row]["PayNum"].ToString()!="0"){
				string[] procsOnPayment=table.Rows[e.Row]["procsOnObj"].ToString().Split(',');
				for(int i=0;i<table.Rows.Count;i++){//loop through all rows
					if(table.Rows[i]["PayNum"].ToString()==table.Rows[e.Row]["PayNum"].ToString()){
						gridAccount.SetSelected(i,true);//for other splits in family view
					}
					if(table.Rows[i]["ProcNum"].ToString()=="0"){//if not a procedure, then skip
						continue;
					}
					for(int j=0;j<procsOnPayment.Length;j++){
						if(table.Rows[i]["ProcNum"].ToString()==procsOnPayment[j]){
							gridAccount.SetSelected(i,true);
						}
					}
				}
			}
		}

		private void gridAccount_CellDoubleClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			if(ViewingInRecall) return;
			Actscrollval=gridAccount.ScrollValue;
			DataTable table=DataSetMain.Tables["account"];
			if(table.Rows[e.Row]["ProcNum"].ToString()!="0"){
				Procedure proc=Procedures.GetOneProc(PIn.Long(table.Rows[e.Row]["ProcNum"].ToString()),true);
				Patient pat=FamCur.GetPatient(proc.PatNum);
				FormProcEdit FormPE=new FormProcEdit(proc,pat,FamCur);
				FormPE.ShowDialog();
			}
			else if(table.Rows[e.Row]["AdjNum"].ToString()!="0"){
				Adjustment adj=Adjustments.GetOne(PIn.Long(table.Rows[e.Row]["AdjNum"].ToString()));
				FormAdjust FormAdj=new FormAdjust(PatCur,adj);
				FormAdj.ShowDialog();
			}
			else if(table.Rows[e.Row]["PayNum"].ToString()!="0"){
				Payment PaymentCur=Payments.GetPayment(PIn.Long(table.Rows[e.Row]["PayNum"].ToString()));
				/*
				if(PaymentCur.PayType==0){//provider income transfer
					FormProviderIncTrans FormPIT=new FormProviderIncTrans();
					FormPIT.PatNum=PatCur.PatNum;
					FormPIT.PaymentCur=PaymentCur;
					FormPIT.IsNew=false;
					FormPIT.ShowDialog();
				}
				else{*/
				FormPayment FormPayment2=new FormPayment(PatCur,FamCur,PaymentCur);
				FormPayment2.IsNew=false;
				FormPayment2.ShowDialog();
				//}
			}
			else if(table.Rows[e.Row]["ClaimNum"].ToString()!="0"){//claims and claimpayments
				Claim claim=Claims.GetClaim(PIn.Long(table.Rows[e.Row]["ClaimNum"].ToString()));
				Patient pat=FamCur.GetPatient(claim.PatNum);
				FormClaimEdit FormClaimEdit2=new FormClaimEdit(claim,pat,FamCur);
				FormClaimEdit2.IsNew=false;
				FormClaimEdit2.ShowDialog();
			}
			else if(table.Rows[e.Row]["StatementNum"].ToString()!="0"){
				Statement stmt=Statements.CreateObject(PIn.Long(table.Rows[e.Row]["StatementNum"].ToString()));
				FormStatementOptions FormS=new FormStatementOptions();
				FormS.StmtCur=stmt;
				FormS.ShowDialog();
			}
			else if(table.Rows[e.Row]["PayPlanNum"].ToString()!="0"){
				PayPlan payplan=PayPlans.GetOne(PIn.Long(table.Rows[e.Row]["PayPlanNum"].ToString()));
				FormPayPlan2=new FormPayPlan(PatCur,payplan);
				FormPayPlan2.ShowDialog();
				if(FormPayPlan2.GotoPatNum!=0){
					OnPatientSelected(Patients.GetPat(FormPayPlan2.GotoPatNum));
					ModuleSelected(FormPayPlan2.GotoPatNum,false);
					return;
				}
			}
			bool isSelectingFamily=gridAcctPat.GetSelectedIndex()==this.DataSetMain.Tables["patient"].Rows.Count-1;
			ModuleSelected(PatCur.PatNum,isSelectingFamily);
		}

		private void gridPayPlan_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			DataTable table=DataSetMain.Tables["payplan"];
			if(table.Rows[e.Row]["PayPlanNum"].ToString()!="0") {//Payment plan
				PayPlan payplan=PayPlans.GetOne(PIn.Long(table.Rows[e.Row]["PayPlanNum"].ToString()));
				FormPayPlan2=new FormPayPlan(PatCur,payplan);
				FormPayPlan2.ShowDialog();
				if(FormPayPlan2.GotoPatNum!=0) {
					OnPatientSelected(Patients.GetPat(FormPayPlan2.GotoPatNum));
					ModuleSelected(FormPayPlan2.GotoPatNum,false);
					return;
				}
				bool isSelectingFamily=gridAcctPat.GetSelectedIndex()==this.DataSetMain.Tables["patient"].Rows.Count-1;
				ModuleSelected(PatCur.PatNum,isSelectingFamily);
			}
			else {//Installment Plan
				FormInstallmentPlanEdit FormIPE= new FormInstallmentPlanEdit();
				FormIPE.InstallmentPlanCur = InstallmentPlans.GetOne(PIn.Long(table.Rows[e.Row]["InstallmentPlanNum"].ToString()));
				FormIPE.IsNew=false;
				FormIPE.ShowDialog();
				ModuleSelected(PatCur.PatNum);
			}
		}

		private void gridAcctPat_CellClick(object sender,ODGridClickEventArgs e) {
			if(ViewingInRecall){
				return;
			}
			if(e.Row==FamCur.ListPats.Length){//last row
				OnPatientSelected(FamCur.ListPats[0]);
				ModuleSelected(FamCur.ListPats[0].PatNum,true);
			}
			else{
				OnPatientSelected(FamCur.ListPats[e.Row]);
				ModuleSelected(FamCur.ListPats[e.Row].PatNum);
			}
		}

		private delegate void ToolBarClick();

		private void ToolBarMain_ButtonClick(object sender, OpenDental.UI.ODToolBarButtonClickEventArgs e) {
			if(e.Button.Tag.GetType()==typeof(string)){
				//standard predefined button
				switch(e.Button.Tag.ToString()){
					//case "Patient":
					//	OnPat_Click();
					//	break;
					case "Payment":
						toolBarButPay_Click();
						break;
					case "Adjustment":
						toolBarButAdj_Click();
						break;
					case "Insurance":
						toolBarButIns_Click();
						break;
					case "PayPlan":
						toolBarButPayPlan_Click();
						break;
					case "InstallPlan":
						toolBarButInstallPlan_Click();
						break;
					case "RepeatCharge":
						toolBarButRepeatCharge_Click();
						break;
					case "Statement":
						//The reason we are using a delegate and BeginInvoke() is because of a Microsoft bug that causes the Print Dialog window to not be in focus			
						//when it comes from a toolbar click.
						//https://social.msdn.microsoft.com/Forums/windows/en-US/681a50b4-4ae3-407a-a747-87fb3eb427fd/first-mouse-click-after-showdialog-hits-the-parent-form?forum=winforms
						ToolBarClick toolClick=toolBarButStatement_Click;
						this.BeginInvoke(toolClick);
						break;
					case "Questionnaire":
						toolBarButComm_Click();
						break;
					case "TrojanCollect":
						toolBarButTrojan_Click();
						break;
					case "QuickProcs":
						toolBarButQuickProcs_Click();
						break;
				}
			}
			else if(e.Button.Tag.GetType()==typeof(Program)) {
				ProgramL.Execute(((Program)e.Button.Tag).ProgramNum,PatCur);
			}
		}

		///<summary></summary>
		private void OnPatientSelected(Patient pat) {
			PatientSelectedEventArgs eArgs=new OpenDental.PatientSelectedEventArgs(pat);
			if(PatientSelected!=null){
				PatientSelected(this,eArgs);
			}
		}

		private void toolBarButPay_Click() {
			Payment PaymentCur=new Payment();
			PaymentCur.PayDate=DateTimeOD.Today;
			PaymentCur.PatNum=PatCur.PatNum;
			//Explicitly set ClinicNum=0, since a pat's ClinicNum will remain set if the user enabled clinics, assigned patients to clinics, and then
			//disabled clinics because we use the ClinicNum to determine which PayConnect or XCharge/XWeb credentials to use for payments.
			PaymentCur.ClinicNum=0;
			if(PrefC.HasClinicsEnabled) {//if clinics aren't enabled default to 0
				PaymentCur.ClinicNum=PatCur.ClinicNum;
			}
			PaymentCur.DateEntry=DateTimeOD.Today;//So that it will show properly in the new window.
			if(DefC.Short[(int)DefCat.PaymentTypes].Length>0){
				PaymentCur.PayType=DefC.Short[(int)DefCat.PaymentTypes][0].DefNum;
			}
			Payments.Insert(PaymentCur);
			FormPayment FormPayment2=new FormPayment(PatCur,FamCur,PaymentCur);
			FormPayment2.IsNew=true;
			FormPayment2.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}

		private void toolBarButAdj_Click() {
			Adjustment AdjustmentCur=new Adjustment();
			AdjustmentCur.DateEntry=DateTime.Today;//cannot be changed. Handled automatically
			AdjustmentCur.AdjDate=DateTime.Today;
			AdjustmentCur.ProcDate=DateTime.Today;
			AdjustmentCur.ProvNum=PatCur.PriProv;
			AdjustmentCur.PatNum=PatCur.PatNum;
			AdjustmentCur.ClinicNum=PatCur.ClinicNum;
			FormAdjust FormAdjust2=new FormAdjust(PatCur,AdjustmentCur);
			FormAdjust2.IsNew=true;
			FormAdjust2.ShowDialog();
			//Shared.ComputeBalances();
			ModuleSelected(PatCur.PatNum);
		}

		private bool CheckClearinghouseDefaults() {
			if(PrefC.GetLong(PrefName.ClearinghouseDefaultDent)==0) {
				MsgBox.Show(this,"No default dental clearinghouse defined.");
				return false;
			}
			if(PrefC.GetBool(PrefName.ShowFeatureMedicalInsurance) && PrefC.GetLong(PrefName.ClearinghouseDefaultMed)==0) {
				MsgBox.Show(this,"No default medical clearinghouse defined.");
				return false;
			}
			return true;
		}

		private void toolBarButIns_Click() {
			if(!CheckClearinghouseDefaults()) {
				return;
			}
			List <PatPlan> PatPlanList=PatPlans.Refresh(PatCur.PatNum);
			List<InsSub> SubList=InsSubs.RefreshForFam(FamCur);
			List<InsPlan> InsPlanList=InsPlans.RefreshForSubList(SubList);
			List<ClaimProc> ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			List <Benefit> BenefitList=Benefits.Refresh(PatPlanList,SubList);
			List<Procedure> procsForPat=Procedures.Refresh(PatCur.PatNum);
			if(PatPlanList.Count==0){
				MsgBox.Show(this,"Patient does not have insurance.");
				return;
			}
			int countSelected=0;
			DataTable table=DataSetMain.Tables["account"];
			InsPlan plan;
			InsSub sub;
			if(gridAccount.SelectedIndices.Length==0){
				//autoselect procedures
				for(int i=0;i<table.Rows.Count;i++){//loop through every line showing on screen
					if(table.Rows[i]["ProcNum"].ToString()=="0"){
						continue;//ignore non-procedures
					}
					if((decimal)table.Rows[i]["chargesDouble"]==0){
						continue;//ignore zero fee procedures, but user can explicitly select them
					}
					//payment rows skipped
					Procedure proc=Procedures.GetProcFromList(procsForPat,PIn.Long(table.Rows[i]["ProcNum"].ToString()));
					ProcedureCode procCode=ProcedureCodes.GetProcCodeFromDb(proc.CodeNum);
					if(procCode.IsCanadianLab) {
						continue;
					}
					int ordinal=PatPlans.GetOrdinal(PriSecMed.Primary,PatPlanList,InsPlanList,SubList);
					if(ordinal==0) { //No primary dental plan. Must be a medical plan.  Use the first medical plan instead.
						ordinal=1;
					}
					sub=InsSubs.GetSub(PatPlans.GetInsSubNum(PatPlanList,ordinal),SubList);
					if(Procedures.NeedsSent(proc.ProcNum,sub.InsSubNum,ClaimProcList)){
						if(CultureInfo.CurrentCulture.Name.EndsWith("CA") && countSelected==7) {//Canadian. en-CA or fr-CA
							MsgBox.Show(this,"Only the first 7 procedures will be automatically selected.  You will need to create another claim for the remaining procedures.");
							continue;//only send 7.  
						}
						countSelected++;
						gridAccount.SetSelected(i,true);
					}
				}
				if(gridAccount.SelectedIndices.Length==0){//if still none selected
					MessageBox.Show(Lan.g(this,"Please select procedures first."));
					return;
				}
			}
			bool allAreProcedures=true;//In Canada, this will also be true if the user selected labs.
			for(int i=0;i<gridAccount.SelectedIndices.Length;i++){
				if(table.Rows[gridAccount.SelectedIndices[i]]["ProcNum"].ToString()=="0"){
					allAreProcedures=false;
				}
			}
			if(!allAreProcedures){
				MsgBox.Show(this,"You can only select procedures.");
				return;
			}
			//At this point, all selected items are procedures.  In Canada, the selections may also include labs.
			InsCanadaValidateProcs(procsForPat,table);
			if(gridAccount.SelectedIndices.Length<1){
				return;
			}
			string claimType="P";
			//If they have medical insurance and no dental, make the claim type Medical.  This is to avoid the scenario of multiple med ins and no dental.
			if(PatPlans.GetOrdinal(PriSecMed.Medical,PatPlanList,InsPlanList,SubList)>0
				&& PatPlans.GetOrdinal(PriSecMed.Primary,PatPlanList,InsPlanList,SubList)==0
				&& PatPlans.GetOrdinal(PriSecMed.Secondary,PatPlanList,InsPlanList,SubList)==0)
			{
				claimType="Med";
			}
			Claim ClaimCur=CreateClaim(claimType,PatPlanList,InsPlanList,ClaimProcList,procsForPat,SubList);
			ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			if(ClaimCur.ClaimNum==0){
				ModuleSelected(PatCur.PatNum);
				return;
			}
			ClaimCur.ClaimStatus="W";
			ClaimCur.DateSent=DateTimeOD.Today;
			ClaimL.CalculateAndUpdate(procsForPat,InsPlanList,ClaimCur,PatPlanList,BenefitList,PatCur.Age,SubList);
			FormClaimEdit FormCE=new FormClaimEdit(ClaimCur,PatCur,FamCur);
			FormCE.IsNew=true;//this causes it to delete the claim if cancelling.
			FormCE.ShowDialog();
			if(FormCE.DialogResult!=DialogResult.OK){
				ModuleSelected(PatCur.PatNum);
				return;//will have already been deleted
			}
			if(PatPlans.GetOrdinal(PriSecMed.Secondary,PatPlanList,InsPlanList,SubList)>0 //if there exists a secondary plan
				&& !CultureInfo.CurrentCulture.Name.EndsWith("CA"))//And not Canada (don't create secondary claim for Canada)
			{
				sub=InsSubs.GetSub(PatPlans.GetInsSubNum(PatPlanList,PatPlans.GetOrdinal(PriSecMed.Secondary,PatPlanList,InsPlanList,SubList)),SubList);
				plan=InsPlans.GetPlan(sub.PlanNum,InsPlanList);
				ClaimCur=CreateClaim("S",PatPlanList,InsPlanList,ClaimProcList,procsForPat,SubList);
				if(ClaimCur.ClaimNum==0){
					ModuleSelected(PatCur.PatNum);
					return;
				}
				ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
				ClaimCur.ClaimStatus="H";
				ClaimL.CalculateAndUpdate(procsForPat,InsPlanList,ClaimCur,PatPlanList,BenefitList,PatCur.Age,SubList);
			}
			ModuleSelected(PatCur.PatNum);
		}

		///<summary>The procsForPat variable is all of the current procedures for the current patient. The tableAccount variable is the table from the DataSetMain object containing the information for the account grid.</summary>
		private void InsCanadaValidateProcs(List <Procedure> procsForPat,DataTable tableAccount) {
			int labProcsUnselected=0;
			List<int> selectedIndicies=new List<int>(gridAccount.SelectedIndices);
			for(int i=0;i<selectedIndicies.Count;i++) {
				Procedure proc=Procedures.GetProcFromList(procsForPat,PIn.Long(tableAccount.Rows[selectedIndicies[i]]["ProcNum"].ToString()));
				ProcedureCode procCode=ProcedureCodes.GetProcCodeFromDb(proc.CodeNum);
				if(procCode.IsCanadianLab) {
					gridAccount.SetSelected(selectedIndicies[i],false);
					labProcsUnselected++;
				}
			}
			if(labProcsUnselected>0) {
				MessageBox.Show(Lan.g(this,"Number of lab fee procedures automatically unselected")+": "+labProcsUnselected.ToString());
			}
			if(CultureInfo.CurrentCulture.Name.EndsWith("CA") && gridAccount.SelectedIndices.Length>7) {//Canadian. en-CA or fr-CA
				selectedIndicies=new List<int>(gridAccount.SelectedIndices);
				selectedIndicies.Sort();
				for(int i=0;i<selectedIndicies.Count;i++) { //Unselect all but the first 7 procedures with the smallest index numbers.
					gridAccount.SetSelected(selectedIndicies[i],(i<7));
				}
				MsgBox.Show(this,"Only the first 7 procedures will be selected.  You will need to create another claim for the remaining procedures.");
			}
		}

		///<summary>The only validation that's been done is just to make sure that only procedures are selected.  
		///All validation on the procedures selected is done here.  Creates and saves claim initially, attaching all selected procedures.  
		///But it does not refresh any data. Does not do a final update of the new claim.  Does not enter fee amounts.
		///claimType=P,S,Med,or Other
		///Returns a 'new' claim object (ClaimNum=0) to indicate that the user does not want to create the claim or there are validation issues.</summary>
		private Claim CreateClaim(string claimType,List <PatPlan> PatPlanList,List <InsPlan> planList,List<ClaimProc> ClaimProcList,List<Procedure> procsForPat,List<InsSub> subList){
			long claimFormNum = 0;
			InsPlan PlanCur=new InsPlan();
			InsSub SubCur=new InsSub();
			Relat relatOther=Relat.Self;
			switch(claimType){
				case "P":
					SubCur=InsSubs.GetSub(PatPlans.GetInsSubNum(PatPlanList,PatPlans.GetOrdinal(PriSecMed.Primary,PatPlanList,planList,subList)),subList);
					PlanCur=InsPlans.GetPlan(SubCur.PlanNum,planList);
					break;
				case "S":
					SubCur=InsSubs.GetSub(PatPlans.GetInsSubNum(PatPlanList,PatPlans.GetOrdinal(PriSecMed.Secondary,PatPlanList,planList,subList)),subList);
					PlanCur=InsPlans.GetPlan(SubCur.PlanNum,planList);
					break;
				case "Med":
					//It's already been verified that a med plan exists
					SubCur=InsSubs.GetSub(PatPlans.GetInsSubNum(PatPlanList,PatPlans.GetOrdinal(PriSecMed.Medical,PatPlanList,planList,subList)),subList);
					PlanCur=InsPlans.GetPlan(SubCur.PlanNum,planList);
					break;
				case "Other":
					FormClaimCreate FormCC=new FormClaimCreate(PatCur.PatNum);
					FormCC.ShowDialog();
					if(FormCC.DialogResult!=DialogResult.OK){
						return new Claim();
					}
					PlanCur=FormCC.SelectedPlan;
					SubCur=FormCC.SelectedSub;
					relatOther=FormCC.PatRelat;
					break;
			}
			DataTable table=DataSetMain.Tables["account"];
			Procedure proc;
			//Order the selected procedures in the same order that they show in the TP module for the following reasons:
			//1) Maintains the same procedure order on preauths and claims (required for Denti-Cal).
			//2) Estimates calculations are affected by procedure order, especially when the patient is near their annual max.
			//	Maintaining the same procedure order on the claim as in the TP module ensures that estimates are calculated the same way in both places.
			//To mimic the TP module sorting, we must first get the selected proceures in the order which they are found in the database, then sort
			//by the relevant columns.
			List<Procedure> listSelectedProcs=new List<Procedure>();
			for(int i=0;i<procsForPat.Count;i++) {
				proc=procsForPat[i];
				for(int j=0;j<gridAccount.SelectedIndices.Length;j++) {
					long procNum=PIn.Long(table.Rows[gridAccount.SelectedIndices[j]]["ProcNum"].ToString());
					if(proc.ProcNum==procNum) {
						listSelectedProcs.Add(proc);
						break;
					}
				}
			}
			//This sorting algorithm mimics the sorting found in ContrTreat.LoadActiveTP().
			List<Procedure> listProcs=listSelectedProcs
				.OrderBy(x => x.PriorityOrder<0)
				.ThenBy(x => x.PriorityOrder)
				.ThenBy(x => Tooth.ToInt(x.ToothNum))
				.ThenBy(x=>x.ProcDate).ToList();
			if((claimType=="P" || claimType=="S") && Procedures.GetUniqueDiagnosticCodes(listProcs,false).Count>4) {
				MsgBox.Show(this,"Claim has more than 4 unique diagnosis codes.  Create multiple claims instead.");
				return new Claim();
			}
			if(Procedures.GetUniqueDiagnosticCodes(listProcs,true).Count>12) {
				MsgBox.Show(this,"Claim has more than 12 unique diagnosis codes.  Create multiple claims instead.");
				return new Claim();
			}
			for(int i=0;i<listProcs.Count;i++){
				proc=listProcs[i];
				if(Procedures.NoBillIns(proc,ClaimProcList,PlanCur.PlanNum)){
					MsgBox.Show(this,"Not allowed to send procedures to insurance that are marked 'Do not bill to ins'.");
					return new Claim();
				}
			}
			for(int i=0;i<listProcs.Count;i++){
				proc=listProcs[i];
				if(Procedures.IsAlreadyAttachedToClaim(proc,ClaimProcList,SubCur.InsSubNum)){
					MsgBox.Show(this,"Not allowed to send a procedure to the same insurance company twice.");
					return new Claim();
				}
			}
			proc=listProcs[0];
			long clinicNum=proc.ClinicNum;
			PlaceOfService placeService=proc.PlaceService;
			for(int i=1;i<listProcs.Count;i++){//skips 0
				proc=listProcs[i];
				if(clinicNum!=proc.ClinicNum){
					MsgBox.Show(this,"All procedures do not have the same clinic.");
					return new Claim();
				}
				if(proc.PlaceService!=placeService) {
					MsgBox.Show(this,"All procedures do not have the same place of service.");
					return new Claim();
				}
			}
			ClaimProc[] claimProcs=new ClaimProc[listProcs.Count];//1:1 with listProcs
			for(int i=0;i<listProcs.Count;i++){//loop through selected procs
				//and try to find an estimate that can be used
				claimProcs[i]=Procedures.GetClaimProcEstimate(listProcs[i].ProcNum,ClaimProcList,PlanCur,SubCur.InsSubNum);
			}
			for(int i=0;i<claimProcs.Length;i++){//loop through each claimProc
				//and create any missing estimates. This handles claims to 3rd and 4th ins co's.
				if(claimProcs[i]==null){
					claimProcs[i]=new ClaimProc();
					proc=listProcs[i];//1:1
					ClaimProcs.CreateEst(claimProcs[i],proc,PlanCur,SubCur);
				}
			}
			Claim ClaimCur=new Claim();
			Claims.Insert(ClaimCur);//to retreive a key for new Claim.ClaimNum
			//now, all claimProcs have a valid value
			//for any CapComplete, need to make a copy so that original doesn't get attached.
			for(int i=0;i<claimProcs.Length;i++){
				if(claimProcs[i].Status==ClaimProcStatus.CapComplete){
					claimProcs[i].ClaimNum=ClaimCur.ClaimNum;
					claimProcs[i]=claimProcs[i].Copy();
					claimProcs[i].WriteOff=0;
					claimProcs[i].CopayAmt=-1;
					claimProcs[i].CopayOverride=-1;
					//status will get changed down below
					ClaimProcs.Insert(claimProcs[i]);//this makes a duplicate in db with different claimProcNum
				}
			}
			ClaimCur.PatNum=PatCur.PatNum;
			ClaimCur.DateService=claimProcs[claimProcs.Length-1].ProcDate;
			ClaimCur.ClinicNum=clinicNum;
			ClaimCur.PlaceService=proc.PlaceService;
			//datesent
			ClaimCur.ClaimStatus="U";
			//datereceived
			InsSub sub;
			ClaimCur.PlanNum=PlanCur.PlanNum;
			ClaimCur.InsSubNum=SubCur.InsSubNum;
			switch(claimType){
				case "P":
					ClaimCur.PatRelat=PatPlans.GetRelat(PatPlanList,PatPlans.GetOrdinal(PriSecMed.Primary,PatPlanList,planList,subList));
					ClaimCur.ClaimType="P";
					ClaimCur.InsSubNum2=PatPlans.GetInsSubNum(PatPlanList,PatPlans.GetOrdinal(PriSecMed.Secondary,PatPlanList,planList,subList));
					sub=InsSubs.GetSub(ClaimCur.InsSubNum2,subList);
					if(sub.PlanNum>0 && InsPlans.RefreshOne(sub.PlanNum).IsMedical) {
						ClaimCur.PlanNum2=0;//no sec ins
						ClaimCur.PatRelat2=Relat.Self;
					}
					else {
						ClaimCur.PlanNum2=sub.PlanNum;//might be 0 if no sec ins
						ClaimCur.PatRelat2=PatPlans.GetRelat(PatPlanList,PatPlans.GetOrdinal(PriSecMed.Secondary,PatPlanList,planList,subList));
					}
					break;
				case "S":
					ClaimCur.PatRelat=PatPlans.GetRelat(PatPlanList,PatPlans.GetOrdinal(PriSecMed.Secondary,PatPlanList,planList,subList));
					ClaimCur.ClaimType="S";
					ClaimCur.InsSubNum2=PatPlans.GetInsSubNum(PatPlanList,PatPlans.GetOrdinal(PriSecMed.Primary,PatPlanList,planList,subList));
					sub=InsSubs.GetSub(ClaimCur.InsSubNum2,subList);
					ClaimCur.PlanNum2=sub.PlanNum;
					ClaimCur.PatRelat2=PatPlans.GetRelat(PatPlanList,PatPlans.GetOrdinal(PriSecMed.Primary,PatPlanList,planList,subList));
					break;
				case "Med":
					ClaimCur.PatRelat=PatPlans.GetFromList(PatPlanList,SubCur.InsSubNum).Relationship;
					ClaimCur.ClaimType="Other";
					if(PrefC.GetBool(PrefName.ClaimMedTypeIsInstWhenInsPlanIsMedical)){
						ClaimCur.MedType=EnumClaimMedType.Institutional;
					}
					else{
						ClaimCur.MedType=EnumClaimMedType.Medical;
					}
					break;
				case "Other":
					ClaimCur.PatRelat=relatOther;
					ClaimCur.ClaimType="Other";
					//plannum2 is not automatically filled in.
					ClaimCur.ClaimForm=claimFormNum;
					if(PlanCur.IsMedical){
						if(PrefC.GetBool(PrefName.ClaimMedTypeIsInstWhenInsPlanIsMedical)){
							ClaimCur.MedType=EnumClaimMedType.Institutional;
						}
						else{
							ClaimCur.MedType=EnumClaimMedType.Medical;
						}
					}
					break;
			}
			if(PlanCur.PlanType=="c"){//if capitation
				ClaimCur.ClaimType="Cap";
			}
			ClaimCur.ProvTreat=listProcs[0].ProvNum;
			for(int i=0;i<listProcs.Count;i++){
				proc=listProcs[i];
				if(!Providers.GetIsSec(proc.ProvNum)){//if not a hygienist
					ClaimCur.ProvTreat=proc.ProvNum;
				}
			}
			if(Providers.GetIsSec(ClaimCur.ProvTreat)){
				ClaimCur.ProvTreat=PatCur.PriProv;
				//OK if 0, because auto select first in list when open claim
			}
			//claimfee calcs in ClaimEdit
			//inspayest ''
			//inspayamt
			//ClaimCur.DedApplied=0;//calcs in ClaimEdit.
			//preauthstring, etc, etc
			ClaimCur.IsProsthesis="N";
			//int clinicInsBillingProv=0;
			//bool useClinic=false;
			//if(ClaimCur.ClinicNum>0){
			//	useClinic=true;
			//	clinicInsBillingProv=Clinics.GetClinic(ClaimCur.ClinicNum).InsBillingProv;
			//}
			ClaimCur.ProvBill=Providers.GetBillingProvNum(ClaimCur.ProvTreat,ClaimCur.ClinicNum);//,useClinic,clinicInsBillingProv);//OK if zero, because it will get fixed in claim
			Provider prov=Providers.GetProv(ClaimCur.ProvTreat);
			if(prov.ProvNumBillingOverride!=0) {
				ClaimCur.ProvBill=prov.ProvNumBillingOverride;
			}
			ClaimCur.EmployRelated=YN.No;
			ClaimCur.ClaimForm=PlanCur.ClaimFormNum;
			//attach procedures
			//for(int i=0;i<tbAccount.SelectedIndices.Length;i++){
			for(int i=0;i<claimProcs.Length;i++){
				proc=listProcs[i];//1:1
				//ClaimProc ClaimProcCur=new ClaimProc();
				//ClaimProcCur.ProcNum=ProcCur.ProcNum;
				claimProcs[i].ClaimNum=ClaimCur.ClaimNum;
				//ClaimProcCur.PatNum=Patients.Cur.PatNum;
				//ClaimProcCur.ProvNum=ProcCur.ProvNum;
				//ClaimProcs.Cur.FeeBilled=;//handle in call to ClaimL.CalculateAndUpdate()
				//inspayest ''
				//dedapplied ''
				if(PlanCur.PlanType=="c")//if capitation
					claimProcs[i].Status=ClaimProcStatus.CapClaim;
				else
					claimProcs[i].Status=ClaimProcStatus.NotReceived;
				//inspayamt=0
				//remarks
				//claimpaymentnum=0
				//ClaimProcCur.PlanNum=Claims.Cur.PlanNum;
				//ClaimProcCur.DateCP=ProcCur.ProcDate;
				//writeoff handled in ClaimL.CalculateAndUpdate()
				if(PlanCur.UseAltCode && (ProcedureCodes.GetProcCode(proc.CodeNum).AlternateCode1!="")){
					claimProcs[i].CodeSent=ProcedureCodes.GetProcCode(proc.CodeNum).AlternateCode1;
				}
				else if(PlanCur.IsMedical && proc.MedicalCode!=""){
					claimProcs[i].CodeSent=proc.MedicalCode;
				}
				else{
					claimProcs[i].CodeSent=ProcedureCodes.GetProcCode(proc.CodeNum).ProcCode;
					if(claimProcs[i].CodeSent.Length>5 && claimProcs[i].CodeSent.Substring(0,1)=="D"){
						claimProcs[i].CodeSent=claimProcs[i].CodeSent.Substring(0,5);
					}
					if(CultureInfo.CurrentCulture.Name.EndsWith("CA")) {//Canadian. en-CA or fr-CA
						if(claimProcs[i].CodeSent.Length>5) { //In Canadian electronic claims, codes can contain letters or numbers and cannot be longer than 5 characters.
							claimProcs[i].CodeSent=claimProcs[i].CodeSent.Substring(0,5);
						}
					}
				}
			}//for claimProc
			List <ClaimProc> listClaimProcs=new List<ClaimProc>(claimProcs);
			for(int i=0;i<listClaimProcs.Count;i++) {
				listClaimProcs[i].LineNumber=(byte)(i+1);
				ClaimProcs.Update(listClaimProcs[i]);
			}
			//Insert claim snapshots for historical reporting purposes.
			CreateClaimSnapshot(claimType,listClaimProcs,proc.ProcFee);//,procsForPat
			return ClaimCur;
			//return null;
		}

		///<summary>Creates a snapshot for the claimprocs passed in.  Used for reporting purposes.  claimType=P,S
		///Only creates snapshots if the feature is enabled and if the claimproc is of certain statuses.</summary>
		private void CreateClaimSnapshot(string claimType,List<ClaimProc> listClaimProcs,double procFee) {//,List<Procedure> listPatProcs
			if(!PrefC.GetBool(PrefName.ClaimSnapshotEnabled) || (claimType!="P" && claimType!="S")) {
				return;
			}
			//Loop through all the claimprocs and create a claimsnapshot entry for each.
			for(int i=0;i<listClaimProcs.Count;i++) {
				if(listClaimProcs[i].Status==ClaimProcStatus.CapClaim
					|| listClaimProcs[i].Status==ClaimProcStatus.CapComplete
					|| listClaimProcs[i].Status==ClaimProcStatus.CapEstimate
					|| listClaimProcs[i].Status==ClaimProcStatus.Preauth) 
				{
					continue;
				}
				ClaimSnapshot snapshot=new ClaimSnapshot();
				snapshot.ProcNum=listClaimProcs[i].ProcNum;
				snapshot.Writeoff=listClaimProcs[i].WriteOffEst;
				snapshot.InsPayEst=listClaimProcs[i].InsEstTotal;
				snapshot.Fee=procFee;
				snapshot.ClaimType=claimType;				
				ClaimSnapshots.Insert(snapshot);
			}
		}

		private void menuInsPri_Click(object sender, System.EventArgs e) {
			if(!CheckClearinghouseDefaults()) {
				return;
			}
			List <PatPlan> PatPlanList=PatPlans.Refresh(PatCur.PatNum);
			List<InsSub> SubList=InsSubs.RefreshForFam(FamCur);
			List<InsPlan> InsPlanList=InsPlans.RefreshForSubList(SubList);
			List<ClaimProc> ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			List <Benefit> BenefitList=Benefits.Refresh(PatPlanList,SubList);
			List<Procedure> procsForPat=Procedures.Refresh(PatCur.PatNum);
			if(PatPlanList.Count==0){
				MessageBox.Show(Lan.g(this,"Patient does not have insurance."));
				return;
			}
			if(PatPlans.GetOrdinal(PriSecMed.Primary,PatPlanList,InsPlanList,SubList)==0) {
				MsgBox.Show(this,"The patient does not have any dental insurance plans.");
				return;
			}
			if(gridAccount.SelectedIndices.Length==0){
				MessageBox.Show(Lan.g(this,"Please select procedures first."));
				return;
			}
			DataTable table=DataSetMain.Tables["account"];
			bool allAreProcedures=true;
			for(int i=0;i<gridAccount.SelectedIndices.Length;i++){
				if(table.Rows[gridAccount.SelectedIndices[i]]["ProcNum"].ToString()=="0"){
					allAreProcedures=false;
				}
			}
			if(!allAreProcedures){
				MessageBox.Show(Lan.g(this,"You can only select procedures."));
				return;
			}
			//At this point, all selected items are procedures.
			InsCanadaValidateProcs(procsForPat,table);
			Claim ClaimCur=CreateClaim("P",PatPlanList,InsPlanList,ClaimProcList,procsForPat,SubList);
			if(ClaimCur.ClaimNum==0){
				ModuleSelected(PatCur.PatNum);
				return;
			}
			ClaimCur.ClaimStatus="W";
			ClaimCur.DateSent=DateTime.Today;
			//still have not saved some changes to the claim at this point
			FormClaimEdit FormCE=new FormClaimEdit(ClaimCur,PatCur,FamCur);
			ClaimL.CalculateAndUpdate(procsForPat,InsPlanList,ClaimCur,PatPlanList,BenefitList,PatCur.Age,SubList);
			FormCE.IsNew=true;//this causes it to delete the claim if cancelling.
			FormCE.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}

		private void menuInsSec_Click(object sender, System.EventArgs e) {
			if(!CheckClearinghouseDefaults()) {
				return;
			}
			List <PatPlan> PatPlanList=PatPlans.Refresh(PatCur.PatNum);
			List<InsSub> SubList=InsSubs.RefreshForFam(FamCur);
			List<InsPlan> InsPlanList=InsPlans.RefreshForSubList(SubList);
			List<ClaimProc> ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			List <Benefit> BenefitList=Benefits.Refresh(PatPlanList,SubList);
			List<Procedure> procsForPat=Procedures.Refresh(PatCur.PatNum);
			if(PatPlanList.Count<2){
				MessageBox.Show(Lan.g(this,"Patient does not have secondary insurance."));
				return;
			}
			if(PatPlans.GetOrdinal(PriSecMed.Secondary,PatPlanList,InsPlanList,SubList)==0) {
				MsgBox.Show(this,"Patient does not have secondary insurance.");
				return;
			}
			if(gridAccount.SelectedIndices.Length==0){
				MessageBox.Show(Lan.g(this,"Please select procedures first."));
				return;
			}
			DataTable table=DataSetMain.Tables["account"];
			bool allAreProcedures=true;
			for(int i=0;i<gridAccount.SelectedIndices.Length;i++){
				if(table.Rows[gridAccount.SelectedIndices[i]]["ProcNum"].ToString()=="0"){
					allAreProcedures=false;
				}
			}
			if(!allAreProcedures){
				MessageBox.Show(Lan.g(this,"You can only select procedures."));
				return;
			}
			//At this point, all selected items are procedures.
			InsCanadaValidateProcs(procsForPat,table);
			Claim ClaimCur=CreateClaim("S",PatPlanList,InsPlanList,ClaimProcList,procsForPat,SubList);
			if(ClaimCur.ClaimNum==0){
				ModuleSelected(PatCur.PatNum);
				return;
			}
			ClaimCur.ClaimStatus="W";
			ClaimCur.DateSent=DateTimeOD.Today;
			ClaimL.CalculateAndUpdate(procsForPat,InsPlanList,ClaimCur,PatPlanList,BenefitList,PatCur.Age,SubList);
			FormClaimEdit FormCE=new FormClaimEdit(ClaimCur,PatCur,FamCur);
			FormCE.IsNew=true;//this causes it to delete the claim if cancelling.
			FormCE.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}

		private void menuInsMedical_Click(object sender, System.EventArgs e) {
			if(!CheckClearinghouseDefaults()) {
				return;
			}
			List <PatPlan> PatPlanList=PatPlans.Refresh(PatCur.PatNum);
			List<InsSub> SubList=InsSubs.RefreshForFam(FamCur);
			List<InsPlan> InsPlanList=InsPlans.RefreshForSubList(SubList);
			List<ClaimProc> ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			List <Benefit> BenefitList=Benefits.Refresh(PatPlanList,SubList);
			List<Procedure> procsForPat=Procedures.Refresh(PatCur.PatNum);
			long medSubNum=0;
			for(int i=0;i<PatPlanList.Count;i++){
				InsSub sub=InsSubs.GetSub(PatPlanList[i].InsSubNum,SubList);
				if(InsPlans.GetPlan(sub.PlanNum,InsPlanList).IsMedical){
					medSubNum=sub.InsSubNum;
					break;
				}
			}
			if(medSubNum==0){
				MsgBox.Show(this,"Patient does not have medical insurance.");
				return;
			}
			DataTable table=DataSetMain.Tables["account"];
			Procedure proc;
			if(gridAccount.SelectedIndices.Length==0){
				//autoselect procedures
				for(int i=0;i<table.Rows.Count;i++){//loop through every line showing on screen
					if(table.Rows[i]["ProcNum"].ToString()=="0"){
						continue;//ignore non-procedures
					}
					proc=Procedures.GetProcFromList(procsForPat,PIn.Long(table.Rows[i]["ProcNum"].ToString()));
					if(proc.ProcFee==0){
						continue;//ignore zero fee procedures, but user can explicitly select them
					}
					if(proc.MedicalCode==""){
						continue;//ignore non-medical procedures
					}
					if(Procedures.NeedsSent(proc.ProcNum,medSubNum,ClaimProcList)) {
						gridAccount.SetSelected(i,true);
					}
				}
				if(gridAccount.SelectedIndices.Length==0){//if still none selected
					MsgBox.Show(this,"Please select procedures first.");
					return;
				}
			}
			bool allAreProcedures=true;
			for(int i=0;i<gridAccount.SelectedIndices.Length;i++){
				if(table.Rows[gridAccount.SelectedIndices[i]]["ProcNum"].ToString()=="0"){
					allAreProcedures=false;
				}
			}
			if(!allAreProcedures){
				MsgBox.Show(this,"You can only select procedures.");
				return;
			}
			Claim ClaimCur=CreateClaim("Med",PatPlanList,InsPlanList,ClaimProcList,procsForPat,SubList);
			if(ClaimCur.ClaimNum==0){
				ModuleSelected(PatCur.PatNum);
				return;
			}
			ClaimCur.ClaimStatus="W";
			ClaimCur.DateSent=DateTimeOD.Today;
			ClaimL.CalculateAndUpdate(procsForPat,InsPlanList,ClaimCur,PatPlanList,BenefitList,PatCur.Age,SubList);
			//still have not saved some changes to the claim at this point
			FormClaimEdit FormCE=new FormClaimEdit(ClaimCur,PatCur,FamCur);
			FormCE.IsNew=true;//this causes it to delete the claim if cancelling.
			FormCE.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}

		private void menuInsOther_Click(object sender, System.EventArgs e) {
			if(!CheckClearinghouseDefaults()) {
				return;
			}
			List <PatPlan> PatPlanList=PatPlans.Refresh(PatCur.PatNum);
			List<InsSub> SubList=InsSubs.RefreshForFam(FamCur);
			List<InsPlan> InsPlanList=InsPlans.RefreshForSubList(SubList);
			List<ClaimProc> ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			List <Benefit> BenefitList=Benefits.Refresh(PatPlanList,SubList);
			List<Procedure> procsForPat=Procedures.Refresh(PatCur.PatNum);
			if(gridAccount.SelectedIndices.Length==0){
				MessageBox.Show(Lan.g(this,"Please select procedures first."));
				return;
			}
			DataTable table=DataSetMain.Tables["account"];
			bool allAreProcedures=true;
			for(int i=0;i<gridAccount.SelectedIndices.Length;i++){
				if(table.Rows[gridAccount.SelectedIndices[i]]["ProcNum"].ToString()=="0"){
					allAreProcedures=false;
				}
			}
			if(!allAreProcedures){
				MessageBox.Show(Lan.g(this,"You can only select procedures."));
				return;
			}
			Claim ClaimCur=CreateClaim("Other",PatPlanList,InsPlanList,ClaimProcList,procsForPat,SubList);
			if(ClaimCur.ClaimNum==0){
				ModuleSelected(PatCur.PatNum);
				return;
			}
			ClaimL.CalculateAndUpdate(procsForPat,InsPlanList,ClaimCur,PatPlanList,BenefitList,PatCur.Age,SubList);
			//still have not saved some changes to the claim at this point
			FormClaimEdit FormCE=new FormClaimEdit(ClaimCur,PatCur,FamCur);
			FormCE.IsNew=true;//this causes it to delete the claim if cancelling.
			FormCE.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}

		private void toolBarButPayPlan_Click() {
			PayPlan payPlan=new PayPlan();
			payPlan.PatNum=PatCur.PatNum;
			payPlan.Guarantor=PatCur.Guarantor;
			payPlan.PayPlanDate=DateTimeOD.Today;
			payPlan.CompletedAmt=PatCur.EstBalance;
			PayPlans.Insert(payPlan);
			FormPayPlan FormPP=new FormPayPlan(PatCur,payPlan);
			FormPP.TotalAmt=PatCur.EstBalance;
			FormPP.IsNew=true;
			FormPP.ShowDialog();
			if(FormPP.GotoPatNum!=0) {
				OnPatientSelected(Patients.GetPat(FormPayPlan2.GotoPatNum));
				ModuleSelected(FormPP.GotoPatNum);//switches to other patient.
			}
			else{
				ModuleSelected(PatCur.PatNum);
			}
		}
		
		private void toolBarButInstallPlan_Click() {
			if(InstallmentPlans.GetOneForFam(PatCur.Guarantor)!=null) {
				MsgBox.Show(this,"Family already has an installment plan.");
				return;
			}
			InstallmentPlan installPlan=new InstallmentPlan();
			installPlan.PatNum=PatCur.Guarantor;
			installPlan.DateAgreement=DateTime.Today;
			installPlan.DateFirstPayment=DateTime.Today;
			//InstallmentPlans.Insert(installPlan);
			FormInstallmentPlanEdit FormIPE=new FormInstallmentPlanEdit();
			FormIPE.InstallmentPlanCur=installPlan;
			FormIPE.IsNew=true;
			FormIPE.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}

		private void toolBarButRepeatCharge_Click(){
			RepeatCharge repeat=new RepeatCharge();
			repeat.PatNum=PatCur.PatNum;
			repeat.DateStart=DateTime.Today;
			FormRepeatChargeEdit FormR=new FormRepeatChargeEdit(repeat);
			FormR.IsNew=true;
			FormR.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}

		private void MenuItemRepeatStand_Click(object sender,System.EventArgs e) {
			if(!ProcedureCodeC.HList.ContainsKey("001")) {
				return;
			}
			UpdatePatientBillingDay(PatCur.PatNum);
			RepeatCharge repeat=new RepeatCharge();
			repeat.PatNum=PatCur.PatNum;
			repeat.ProcCode="001";
			repeat.ChargeAmt=159;
			repeat.DateStart=DateTimeOD.Today;
			repeat.DateStop=DateTimeOD.Today.AddMonths(11);
			repeat.IsEnabled=true;
			RepeatCharges.Insert(repeat);
			repeat=new RepeatCharge();
			repeat.PatNum=PatCur.PatNum;
			repeat.ProcCode="001";
			repeat.ChargeAmt=99;
			repeat.DateStart=DateTimeOD.Today.AddYears(1);
			repeat.IsEnabled=true;
			RepeatCharges.Insert(repeat);
			ModuleSelected(PatCur.PatNum);
		}

		private void MenuItemRepeatEmail_Click(object sender,System.EventArgs e) {
			if(!ProcedureCodeC.HList.ContainsKey("008")) {
				return;
			}
			UpdatePatientBillingDay(PatCur.PatNum);
			RepeatCharge repeat=new RepeatCharge();
			repeat.PatNum=PatCur.PatNum;
			repeat.ProcCode="008";
			repeat.ChargeAmt=89;
			repeat.DateStart=DateTimeOD.Today;
			repeat.IsEnabled=true;
			RepeatCharges.Insert(repeat);
			ModuleSelected(PatCur.PatNum);
		}

		private void menuItemRepeatMobile_Click(object sender,EventArgs e) {
			if(!ProcedureCodeC.HList.ContainsKey("027")) {
				return;
			}
			UpdatePatientBillingDay(PatCur.PatNum);
			RepeatCharge repeat=new RepeatCharge();
			repeat.PatNum=PatCur.PatNum;
			repeat.ProcCode="027";
			repeat.ChargeAmt=15;
			repeat.DateStart=DateTimeOD.Today;
			repeat.IsEnabled=true;
			RepeatCharges.Insert(repeat);
			ModuleSelected(PatCur.PatNum);
		}

		private void menuItemRepeatCanada_Click(object sender,EventArgs e) {
			if(!ProcedureCodeC.HList.ContainsKey("001")) {
				return;
			}
			UpdatePatientBillingDay(PatCur.PatNum);
			RepeatCharge repeat=new RepeatCharge();
			repeat.PatNum=PatCur.PatNum;
			repeat.ProcCode="001";
			repeat.ChargeAmt=129;
			repeat.DateStart=DateTimeOD.Today;
			repeat.DateStop=DateTimeOD.Today.AddMonths(11);
			repeat.IsEnabled=true;
			RepeatCharges.Insert(repeat);
			repeat=new RepeatCharge();
			repeat.PatNum=PatCur.PatNum;
			repeat.ProcCode="001";
			repeat.ChargeAmt=99;
			repeat.DateStart=DateTimeOD.Today.AddYears(1);
			repeat.IsEnabled=true;
			RepeatCharges.Insert(repeat);
			ModuleSelected(PatCur.PatNum);
		}

		private void menuItemRepeatWebSched_Click(object sender,EventArgs e) {
			if(!ProcedureCodeC.HList.ContainsKey("037")) {
				return;
			}
			UpdatePatientBillingDay(PatCur.PatNum);
			RepeatCharge repeat=new RepeatCharge();
			repeat.PatNum=PatCur.PatNum;
			repeat.ProcCode="037";
			repeat.ChargeAmt=75;
			repeat.DateStart=DateTimeOD.Today;
			repeat.IsEnabled=true;
			RepeatCharges.Insert(repeat);
			ModuleSelected(PatCur.PatNum);
		}

		private void toolBarButStatement_Click() {
			Statement stmt=new Statement();
			stmt.PatNum=PatCur.Guarantor;
			stmt.DateSent=DateTimeOD.Today;
			stmt.IsSent=true;
			stmt.Mode_=StatementMode.InPerson;
			stmt.HidePayment=false;
			stmt.SinglePatient=false;
			stmt.Intermingled=false;
			stmt.DateRangeFrom=DateTime.MinValue;
			if (PrefC.GetBool(PrefName.IntermingleFamilyDefault)){
				stmt.Intermingled = true;
			}
			if (PrefC.GetBool(PrefName.FuchsOptionsOn)){
				stmt.DateRangeFrom = PIn.Date(DateTime.Today.AddDays(-45).ToShortDateString());
				stmt.DateRangeTo = PIn.Date(DateTime.Today.ToShortDateString());
			} 
			else{
				if (textDateStart.errorProvider1.GetError(textDateStart) == "") {
					if (textDateStart.Text != "") {
						stmt.DateRangeFrom = PIn.Date(textDateStart.Text);
					}
				}
			}
			stmt.DateRangeTo = DateTimeOD.Today;//This is needed for payment plan accuracy.//new DateTime(2200,1,1);
			if (textDateEnd.errorProvider1.GetError(textDateEnd) == "") {
				if (textDateEnd.Text != "") {
					stmt.DateRangeTo = PIn.Date(textDateEnd.Text);
				}
			}
			stmt.Note = "";
			stmt.NoteBold = "";
			PrintStatement(stmt);
			ModuleSelected(PatCur.PatNum);
		}
		
		private void menuItemStatementWalkout_Click(object sender, System.EventArgs e) {
			Statement stmt=new Statement();
			stmt.PatNum=PatCur.PatNum;
			stmt.DateSent=DateTimeOD.Today;
			stmt.IsSent=true;
			stmt.Mode_=StatementMode.InPerson;
			stmt.HidePayment=true;
			stmt.SinglePatient=true;
			stmt.Intermingled=false;
			stmt.IsReceipt=false;
			if(PrefC.GetBool(PrefName.IntermingleFamilyDefault)) {
				stmt.Intermingled = true;
				stmt.SinglePatient=false;
			}
			stmt.DateRangeFrom=DateTimeOD.Today;
			stmt.DateRangeTo=DateTimeOD.Today;
			stmt.Note="";
			stmt.NoteBold="";
			PrintStatement(stmt);
			ModuleSelected(PatCur.PatNum);
		}

		private void menuItemStatementEmail_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.EmailSend)) {
				Cursor=Cursors.Default;
				return;
			}
			Statement stmt=new Statement();
			stmt.PatNum=PatCur.Guarantor;
			stmt.DateSent=DateTimeOD.Today;
			stmt.IsSent=true;
			stmt.Mode_=StatementMode.Email;
			stmt.HidePayment=false;
			stmt.SinglePatient=false;
			stmt.Intermingled=false;
			stmt.IsReceipt=false;
			if(PrefC.GetBool(PrefName.IntermingleFamilyDefault)){
				stmt.Intermingled=true;
			}
			stmt.DateRangeFrom=DateTime.MinValue;
			if(textDateStart.errorProvider1.GetError(textDateStart)==""){
				if(textDateStart.Text!=""){
					stmt.DateRangeFrom=PIn.Date(textDateStart.Text);
				}
			}
			stmt.DateRangeTo=DateTimeOD.Today;//Needed for payplan accuracy.  Used to be setting to new DateTime(2200,1,1);
			if(textDateEnd.errorProvider1.GetError(textDateEnd)==""){
				if(textDateEnd.Text!=""){
					stmt.DateRangeTo=PIn.Date(textDateEnd.Text);
				}
			}
			stmt.Note="";
			stmt.NoteBold="";
			//It's pointless to give the user the window to select statement options, because they could just as easily have hit the More Options dropdown, then Email from there.
			PrintStatement(stmt);
			ModuleSelected(PatCur.PatNum);
		}

		private void menuItemReceipt_Click(object sender,EventArgs e) {
			Statement stmt=new Statement();
			stmt.PatNum=PatCur.PatNum;
			stmt.DateSent=DateTimeOD.Today;
			stmt.IsSent=true;
			stmt.Mode_=StatementMode.InPerson;
			stmt.HidePayment=true;
			stmt.SinglePatient=true;
			stmt.Intermingled=false;
			stmt.IsReceipt=true;
			if(PrefC.GetBool(PrefName.IntermingleFamilyDefault)) {
				stmt.Intermingled = true;
				stmt.SinglePatient=false;
			}
			stmt.DateRangeFrom=DateTimeOD.Today;
			stmt.DateRangeTo=DateTimeOD.Today;
			stmt.Note="";
			stmt.NoteBold="";
			PrintStatement(stmt);
			ModuleSelected(PatCur.PatNum);
		}

		private void menuItemInvoice_Click(object sender,EventArgs e) {
			DataTable table=DataSetMain.Tables["account"];
			if(gridAccount.SelectedIndices.Length==0) {
				//autoselect procedures and adjustments
				for(int i=0;i<table.Rows.Count;i++) {//loop through every line showing on screen
					if(table.Rows[i]["ProcNum"].ToString()=="0" && table.Rows[i]["AdjNum"].ToString()=="0") {
						continue;//ignore items that aren't procs or adjustments
					}
					if(PIn.Date(table.Rows[i]["date"].ToString())!=DateTime.Today) {
						continue;
					}
					if(table.Rows[i]["ProcNum"].ToString()!="0") {//if selected item is a procedure
						Procedure proc=Procedures.GetOneProc(PIn.Long(table.Rows[i]["ProcNum"].ToString()),false);
						if(proc.StatementNum!=0) {//already attached so don't autoselect
							continue;
						}
						if(proc.PatNum!=PatCur.PatNum) {
							continue;
						}
					}
					else {//item guaranteed to be a proc or adjustment, so must be adjustment
						Adjustment adj=Adjustments.GetOne(PIn.Long(table.Rows[i]["AdjNum"].ToString()));
						if(adj.StatementNum!=0) {//already attached so don't autoselect
							continue;
						}
						if(adj.PatNum!=PatCur.PatNum) {
							continue;
						}
					}
					gridAccount.SetSelected(i,true);
				}
				if(gridAccount.SelectedIndices.Length==0) {//if still none selected
					MsgBox.Show(this,"Please select procedures or adjustments first.");
					return;
				}
			}
			for(int i=0;i<gridAccount.SelectedIndices.Length;i++) {
				if(table.Rows[gridAccount.SelectedIndices[i]]["ProcNum"].ToString()=="0" 
					&& table.Rows[gridAccount.SelectedIndices[i]]["AdjNum"].ToString()=="0") //the selected item is neither a procedure nor an adjustment
				{
					MsgBox.Show(this,"You can only select procedures or adjustments.");
					gridAccount.SetSelected(false);
					return;
				}
				if(table.Rows[gridAccount.SelectedIndices[i]]["ProcNum"].ToString()!="0") {//the selected item is a proc
					Procedure proc=Procedures.GetOneProc(PIn.Long(table.Rows[gridAccount.SelectedIndices[i]]["ProcNum"].ToString()),false);
					if(proc.PatNum!=PatCur.PatNum) {
						MsgBox.Show(this,"You can only select procedures or adjustments for the current patient on an invoice.");
						gridAccount.SetSelected(false);
						return;
					}
					if(proc.StatementNum!=0) {
						MsgBox.Show(this,"Selected procedure(s) are already attached to an invoice.");
						gridAccount.SetSelected(false);
						return;
					}
				}
				else {//the selected item must be an adjustment
					Adjustment adj=Adjustments.GetOne(PIn.Long(table.Rows[gridAccount.SelectedIndices[i]]["AdjNum"].ToString()));
					if(adj.PatNum!=PatCur.PatNum) {
						MsgBox.Show(this,"You can only select procedures or adjustments for a single patient on an invoice.");
						gridAccount.SetSelected(false);
						return;
					}
					if(adj.StatementNum!=0) {
						MsgBox.Show(this,"Selected adjustment(s) are already attached to an invoice.");
						gridAccount.SetSelected(false);
						return;
					}
				}
			}
			//At this point, all selected items are procedures or adjustments, and are not already attached, and are for a single patient.
			Statement stmt=new Statement();
			stmt.PatNum=PatCur.PatNum;
			stmt.DateSent=DateTimeOD.Today;
			stmt.IsSent=false;
			stmt.Mode_=StatementMode.InPerson;
			stmt.HidePayment=true;
			stmt.SinglePatient=true;
			stmt.Intermingled=false;
			stmt.IsReceipt=false;
			stmt.IsInvoice=true;
			stmt.DateRangeFrom=DateTime.MinValue;
			stmt.DateRangeTo=DateTimeOD.Today;
			stmt.Note=PrefC.GetString(PrefName.BillingDefaultsInvoiceNote);
			stmt.NoteBold="";
			Statements.Insert(stmt);
			stmt.IsNew=true;
			List<Procedure> procsForPat=Procedures.Refresh(PatCur.PatNum);
			for(int i=0;i<gridAccount.SelectedIndices.Length;i++) {
				if(table.Rows[gridAccount.SelectedIndices[i]]["ProcNum"].ToString()!="0") {//if selected item is a procedure
					Procedure proc=Procedures.GetProcFromList(procsForPat,PIn.Long(table.Rows[gridAccount.SelectedIndices[i]]["ProcNum"].ToString()));
					Procedure oldProc=proc.Copy();
					proc.StatementNum=stmt.StatementNum;
					Procedures.Update(proc,oldProc);
				}
				else {//every selected item guaranteed to be a proc or adjustment, so must be adjustment
					Adjustment adj=Adjustments.GetOne(PIn.Long(table.Rows[gridAccount.SelectedIndices[i]]["AdjNum"].ToString()));
					adj.StatementNum=stmt.StatementNum;
					Adjustments.Update(adj);
				}
			}
			//All printing and emailing will be done from within the form:
			FormStatementOptions FormSO=new FormStatementOptions();
			FormSO.StmtCur=stmt;
			FormSO.ShowDialog();
			if(FormSO.DialogResult!=DialogResult.OK) {
				Procedures.DetachFromInvoice(stmt.StatementNum);
				Adjustments.DetachFromInvoice(stmt.StatementNum);
				Statements.Delete(stmt.StatementNum);
			}
			ModuleSelected(PatCur.PatNum);
		}

		private void menuItemStatementMore_Click(object sender, System.EventArgs e) {
			Statement stmt=new Statement();
			stmt.PatNum=PatCur.PatNum;
			stmt.DateSent=DateTime.Today;
			stmt.IsSent=false;
			stmt.Mode_=StatementMode.InPerson;
			stmt.HidePayment=false;
			stmt.SinglePatient=false;
			stmt.Intermingled=false;
			stmt.IsReceipt=false;
			if(PrefC.GetBool(PrefName.IntermingleFamilyDefault)) {
				stmt.Intermingled=true;
			}
			else {
				stmt.Intermingled=false;
			} 
			stmt.DateRangeFrom=DateTime.MinValue;
			stmt.DateRangeFrom=DateTime.MinValue;
			if(textDateStart.errorProvider1.GetError(textDateStart)==""){
				if(textDateStart.Text!=""){
					stmt.DateRangeFrom=PIn.Date(textDateStart.Text);
				}
			}
			if(PrefC.GetBool(PrefName.FuchsOptionsOn)) {
				stmt.DateRangeFrom=DateTime.Today.AddDays(-90);
			}
			stmt.DateRangeTo=DateTime.Today;//Needed for payplan accuracy.//new DateTime(2200,1,1);
			if(textDateEnd.errorProvider1.GetError(textDateEnd)==""){
				if(textDateEnd.Text!=""){
					stmt.DateRangeTo=PIn.Date(textDateEnd.Text);
				}
			}
			stmt.Note="";
			stmt.NoteBold="";
			//All printing and emailing will be done from within the form:
			FormStatementOptions FormSO=new FormStatementOptions();
			stmt.IsNew=true;
			FormSO.StmtCur=stmt;
			FormSO.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}

		/// <summary>Saves the statement.  Attaches a pdf to it by creating a doc object.  Prints it or emails it.  </summary>
		private void PrintStatement(Statement stmt) {
			if(PrefC.GetBool(PrefName.StatementsUseSheets)) {
				PrintStatementSheets(stmt);
			}
			else {
				PrintStatementClassic(stmt);
			}
		}

		private void PrintStatementClassic(Statement stmt) {
			Cursor=Cursors.WaitCursor;
			Statements.Insert(stmt);
			FormRpStatement FormST=new FormRpStatement();
			DataSet dataSet=AccountModules.GetStatementDataSet(stmt);
			FormST.CreateStatementPdfClassic(stmt,PatCur,FamCur,dataSet);
			//if(ImageStore.UpdatePatient == null){
			//	ImageStore.UpdatePatient = new FileStore.UpdatePatientDelegate(Patients.Update);
			//}
			Patient guar=Patients.GetPat(stmt.PatNum);
			string guarFolder=ImageStore.GetPatientFolder(guar,ImageStore.GetPreferredAtoZpath());
			//OpenDental.Imaging.ImageStoreBase imageStore = OpenDental.Imaging.ImageStore.GetImageStore(guar);
			if(stmt.Mode_==StatementMode.Email) {
				if(!Security.IsAuthorized(Permissions.EmailSend)) {
					Cursor=Cursors.Default;
					return;
				}
				string attachPath=EmailAttaches.GetAttachPath();
				Random rnd=new Random();
				string fileName=DateTime.Now.ToString("yyyyMMdd")+"_"+DateTime.Now.TimeOfDay.Ticks.ToString()+rnd.Next(1000).ToString()+".pdf";
				string filePathAndName=ODFileUtils.CombinePaths(attachPath,fileName);
				File.Copy(ImageStore.GetFilePath(Documents.GetByNum(stmt.DocNum),guarFolder),filePathAndName);
				//Process.Start(filePathAndName);
				EmailMessage message=Statements.GetEmailMessageForStatement(stmt,guar);
				EmailAttach attach=new EmailAttach();
				attach.DisplayedFileName="Statement.pdf";
				attach.ActualFileName=fileName;
				message.Attachments.Add(attach);
				FormEmailMessageEdit FormE=new FormEmailMessageEdit(message);
				FormE.IsNew=true;
				FormE.ShowDialog();
				//If user clicked delete or cancel, delete pdf and statement
				if(FormE.DialogResult==DialogResult.Cancel) {
					Patient pat;
					string patFolder;
					if(stmt.DocNum!=0) {
						//delete the pdf
						pat=Patients.GetPat(stmt.PatNum);
						patFolder=ImageStore.GetPatientFolder(pat,ImageStore.GetPreferredAtoZpath());
						List<Document> listdocs=new List<Document>();
						listdocs.Add(Documents.GetByNum(stmt.DocNum));
						try {
							ImageStore.DeleteDocuments(listdocs,patFolder);
						}
						catch {  //Image could not be deleted, in use.
							//This should never get hit because the file was created by this user within this method.  
							//If the doc cannot be deleted, then we will not stop them, they will have to manually delete it from the images module.
						}
					}
					//delete statement
					Statements.Delete(stmt);
				}
			}
			else {//not email
#if DEBUG
				//don't bother to check valid path because it's just debug.
				string imgPath=ImageStore.GetFilePath(Documents.GetByNum(stmt.DocNum),guarFolder);
				DateTime now=DateTime.Now;
				while(DateTime.Now<now.AddSeconds(5) && !File.Exists(imgPath)) {//wait up to 5 seconds.
					Application.DoEvents();
				}
				Process.Start(imgPath);
#else
					FormST.PrintStatement(stmt,false,dataSet,FamCur,PatCur);
#endif
			}
			Cursor=Cursors.Default;

		}

		private void PrintStatementSheets(Statement stmt) {
			Cursor=Cursors.WaitCursor;
			Statements.Insert(stmt);
			SheetDef sheetDef=SheetUtil.GetStatementSheetDef();
			Sheet sheet=SheetUtil.CreateSheet(sheetDef,stmt.PatNum,stmt.HidePayment);
			DataSet dataSet=AccountModules.GetAccount(stmt.PatNum,stmt.DateRangeFrom,stmt.DateRangeTo,stmt.Intermingled,stmt.SinglePatient
					,stmt.StatementNum,PrefC.GetBool(PrefName.StatementShowProcBreakdown),PrefC.GetBool(PrefName.StatementShowNotes)
					,stmt.IsInvoice,PrefC.GetBool(PrefName.StatementShowAdjNotes),true);
			SheetFiller.FillFields(sheet,dataSet,stmt,null);
			SheetUtil.CalculateHeights(sheet,Graphics.FromImage(new Bitmap(sheet.HeightPage,sheet.WidthPage)),dataSet,stmt);
			string tempPath=CodeBase.ODFileUtils.CombinePaths(PrefL.GetTempFolderPath(),stmt.PatNum.ToString()+".pdf");
			SheetPrinting.CreatePdf(sheet,tempPath,stmt,dataSet,null);
			long category=0;
			for(int i=0;i<DefC.Short[(int)DefCat.ImageCats].Length;i++) {
				if(Regex.IsMatch(DefC.Short[(int)DefCat.ImageCats][i].ItemValue,@"S")) {
					category=DefC.Short[(int)DefCat.ImageCats][i].DefNum;
					break;
				}
			}
			if(category==0) {
				category=DefC.Short[(int)DefCat.ImageCats][0].DefNum;//put it in the first category.
			}
			//create doc--------------------------------------------------------------------------------------
			OpenDentBusiness.Document docc=null;
			try {
				docc=ImageStore.Import(tempPath,category,Patients.GetPat(stmt.PatNum));
			}
			catch {
				MsgBox.Show(this,"Error saving document.");
				//this.Cursor=Cursors.Default;
				return;
			}
			docc.ImgType=ImageType.Document;
			docc.DateCreated=stmt.DateSent;
			Documents.Update(docc);
			stmt.DocNum=docc.DocNum;//this signals the calling class that the pdf was created successfully.
			Statements.AttachDoc(stmt.StatementNum,docc.DocNum);
			//if(ImageStore.UpdatePatient == null){
			//	ImageStore.UpdatePatient = new FileStore.UpdatePatientDelegate(Patients.Update);
			//}
			Patient guar=Patients.GetPat(stmt.PatNum);
			string guarFolder=ImageStore.GetPatientFolder(guar,ImageStore.GetPreferredAtoZpath());
			//OpenDental.Imaging.ImageStoreBase imageStore = OpenDental.Imaging.ImageStore.GetImageStore(guar);
			if(stmt.Mode_==StatementMode.Email) {
				if(!Security.IsAuthorized(Permissions.EmailSend)) {
					Cursor=Cursors.Default;
					return;
				}
				string attachPath=EmailAttaches.GetAttachPath();
				Random rnd=new Random();
				string fileName=DateTime.Now.ToString("yyyyMMdd")+"_"+DateTime.Now.TimeOfDay.Ticks.ToString()+rnd.Next(1000).ToString()+".pdf";
				string filePathAndName=ODFileUtils.CombinePaths(attachPath,fileName);
				File.Copy(ImageStore.GetFilePath(Documents.GetByNum(stmt.DocNum),guarFolder),filePathAndName);
				//Process.Start(filePathAndName);
				EmailMessage message=Statements.GetEmailMessageForStatement(stmt,guar);
				EmailAttach attach=new EmailAttach();
				attach.DisplayedFileName="Statement.pdf";
				attach.ActualFileName=fileName;
				message.Attachments.Add(attach);
				FormEmailMessageEdit FormE=new FormEmailMessageEdit(message);
				FormE.IsNew=true;
				FormE.ShowDialog();
				//If user clicked delete or cancel, delete pdf and statement
				if(FormE.DialogResult==DialogResult.Cancel) {
					Patient pat;
					string patFolder;
					if(stmt.DocNum!=0) {
						//delete the pdf
						pat=Patients.GetPat(stmt.PatNum);
						patFolder=ImageStore.GetPatientFolder(pat,ImageStore.GetPreferredAtoZpath());
						List<Document> listdocs=new List<Document>();
						listdocs.Add(Documents.GetByNum(stmt.DocNum));
						try {
							ImageStore.DeleteDocuments(listdocs,patFolder);
						}
						catch {  //Image could not be deleted, in use.
							//This should never get hit because the file was created by this user within this method.  
							//If the doc cannot be deleted, then we will not stop them, they will have to manually delete it from the images module.
						}
					}
					//delete statement
					Statements.Delete(stmt);
				}
			}
			else {//not email
#if DEBUG
				//don't bother to check valid path because it's just debug.
				string imgPath=ImageStore.GetFilePath(Documents.GetByNum(stmt.DocNum),guarFolder);
				DateTime now=DateTime.Now;
				while(DateTime.Now<now.AddSeconds(5) && !File.Exists(imgPath)) {//wait up to 5 seconds.
					Application.DoEvents();
				}
				Process.Start(imgPath);
#else
				//Thread thread=new Thread(new ParameterizedThreadStart(SheetPrinting.PrintStatement));
				//thread.Start(new List<object> { sheetDef,stmt,tempPath });
				//NOTE: This is printing a "fresh" GDI+ version of the statment which is ever so slightly different than the PDFSharp statment that was saved to disk.
				sheet=SheetUtil.CreateSheet(sheetDef,stmt.PatNum,stmt.HidePayment);
				SheetFiller.FillFields(sheet,stmt);
				SheetUtil.CalculateHeights(sheet,Graphics.FromImage(new Bitmap(sheet.HeightPage,sheet.WidthPage)),stmt);
				SheetPrinting.Print(sheet,1,false,stmt);//use GDI+ printing, which is slightly different than the pdf.
#endif
			}
			Cursor=Cursors.Default;

		}

		private void textUrgFinNote_TextChanged(object sender, System.EventArgs e) {
			UrgFinNoteChanged=true;
		}

		private void textFinNotes_TextChanged(object sender, System.EventArgs e) {
			FinNoteChanged=true;
		}

		//private void textCC_TextChanged(object sender,EventArgs e) {
		//  CCChanged=true;
		//  if(Regex.IsMatch(textCC.Text,@"^\d{4}$")
		//    || Regex.IsMatch(textCC.Text,@"^\d{4}-\d{4}$")
		//    || Regex.IsMatch(textCC.Text,@"^\d{4}-\d{4}-\d{4}$")) 
		//  {
		//    textCC.Text=textCC.Text+"-";
		//    textCC.Select(textCC.Text.Length,0);
		//  }
		//}

		//private void textCCexp_TextChanged(object sender,EventArgs e) {
		//  CCChanged=true;
		//}

		private void textUrgFinNote_Leave(object sender, System.EventArgs e) {
			//need to skip this if selecting another module. Handled in ModuleUnselected due to click event
			if(FamCur==null)
				return;
			if(UrgFinNoteChanged){
				Patient PatOld=FamCur.ListPats[0].Copy();
				FamCur.ListPats[0].FamFinUrgNote=textUrgFinNote.Text;
				Patients.Update(FamCur.ListPats[0],PatOld);
				UrgFinNoteChanged=false;
			}
			ModuleSelected(PatCur.PatNum);
		}

		private void textFinNotes_Leave(object sender, System.EventArgs e) {
			if(FamCur==null)
				return;
			if(FinNoteChanged){
				PatientNoteCur.FamFinancial=textFinNotes.Text;
				PatientNotes.Update(PatientNoteCur,PatCur.Guarantor);
				FinNoteChanged=false;
				ModuleSelected(PatCur.PatNum);
			}
		}

		//private void textCC_Leave(object sender,EventArgs e) {
		//  if(FamCur==null)
		//    return;
		//  if(CCChanged) {
		//    CCSave();
		//    CCChanged=false;
		//    ModuleSelected(PatCur.PatNum);
		//  }
		//}

		//private void textCCexp_Leave(object sender,EventArgs e) {
		//  if(FamCur==null)
		//    return;
		//  if(CCChanged){
		//    CCSave();
		//    CCChanged=false;
		//    ModuleSelected(PatCur.PatNum);
		//  }
		//}

		//private void CCSave(){
		//  string cc=textCC.Text;
		//  if(Regex.IsMatch(cc,@"^\d{4}-\d{4}-\d{4}-\d{4}$")){
		//    PatientNoteCur.CCNumber=cc.Substring(0,4)+cc.Substring(5,4)+cc.Substring(10,4)+cc.Substring(15,4);
		//  }
		//  else{
		//    PatientNoteCur.CCNumber=cc;
		//  }
		//  string exp=textCCexp.Text;
		//  if(Regex.IsMatch(exp,@"^\d\d[/\- ]\d\d$")){//08/07 or 08-07 or 08 07
		//    PatientNoteCur.CCExpiration=new DateTime(Convert.ToInt32("20"+exp.Substring(3,2)),Convert.ToInt32(exp.Substring(0,2)),1);
		//  }
		//  else if(Regex.IsMatch(exp,@"^\d{4}$")){//0807
		//    PatientNoteCur.CCExpiration=new DateTime(Convert.ToInt32("20"+exp.Substring(2,2)),Convert.ToInt32(exp.Substring(0,2)),1);
		//  } 
		//  else if(exp=="") {
		//    PatientNoteCur.CCExpiration=new DateTime();//Allow the experation date to be deleted.
		//  } 
		//  else {
		//    MsgBox.Show(this,"Expiration format invalid.");
		//  }
		//  PatientNotes.Update(PatientNoteCur,PatCur.Guarantor);
		//}

		private void butToday_Click(object sender,EventArgs e) {
			textDateStart.Text=DateTime.Today.ToShortDateString();
			textDateEnd.Text=DateTime.Today.ToShortDateString();
			ModuleSelected(PatCur.PatNum);
		}

		private void but45days_Click(object sender,EventArgs e) {
			textDateStart.Text=DateTime.Today.AddDays(-45).ToShortDateString();
			textDateEnd.Text="";
			ModuleSelected(PatCur.PatNum);
		}

		private void but90days_Click(object sender,EventArgs e) {
			textDateStart.Text=DateTime.Today.AddDays(-90).ToShortDateString();
			textDateEnd.Text="";
			ModuleSelected(PatCur.PatNum);
		}

		private void butDatesAll_Click(object sender,EventArgs e) {
			textDateStart.Text="";
			textDateEnd.Text="";
			ModuleSelected(PatCur.PatNum);
		}

		private void butRefresh_Click(object sender,EventArgs e) {
			if(PatCur==null){
				return;
			}
			ModuleSelected(PatCur.PatNum);
		}

		private void checkShowDetail_Click(object sender,EventArgs e) {
			if(PatCur==null){
				return;
			}
			ModuleSelected(PatCur.PatNum);
		}

		//private void checkShowNotes_Click(object sender,EventArgs e) {
			//checkShowNotes.Tag="JustClicked";		
			//RefreshModuleScreen();
			//checkShowNotes.Tag = "";		
		//	ModuleSelected(PatCur.PatNum);
		//}

		private void panelSplitter_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			MouseIsDownOnSplitter=true;
			SplitterOriginalY=panelSplitter.Top;
			OriginalMousePos=panelSplitter.Top+e.Y;
		}

		private void panelSplitter_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(!MouseIsDownOnSplitter)
				return;
			int splitterNewLoc=SplitterOriginalY+(panelSplitter.Top+e.Y)-OriginalMousePos;
			if(splitterNewLoc<gridAcctPat.Bottom)
				splitterNewLoc=gridAcctPat.Bottom;//keeps it from going too high
			if(splitterNewLoc>Height)
				splitterNewLoc=Height-panelSplitter.Height;//keeps it from going off the bottom edge
			panelSplitter.Top=splitterNewLoc;
			LayoutPanels();
		}

		private void panelSplitter_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			MouseIsDownOnSplitter=false;
			//tbAccount.LayoutTables();
		}

		private void toolBarButComm_Click() {
			FormPat form=new FormPat();
			form.PatNum=PatCur.PatNum;
			form.FormDateTime=DateTime.Now;
			FormFormPatEdit FormP=new FormFormPatEdit();
			FormP.FormPatCur=form;
			FormP.IsNew=true;
			FormP.ShowDialog();
			if(FormP.DialogResult==DialogResult.OK) {
				ModuleSelected(PatCur.PatNum);
			}
		}

		/*private void butTask_Click(object sender, System.EventArgs e) {
			//FormTaskListSelect FormT=new FormTaskListSelect(TaskObjectType.Patient,PatCur.PatNum);
			//FormT.ShowDialog();
		}*/

		private void butCreditCard_Click(object sender,EventArgs e) {
			FormCreditCardManage FormCCM=new FormCreditCardManage(PatCur);
			FormCCM.ShowDialog();
		}

		private void toolBarButTrojan_Click() {
			FormTrojanCollect FormT=new FormTrojanCollect();
			FormT.PatNum=PatCur.PatNum;
			FormT.ShowDialog();
		}

		private void toolBarButQuickProcs_Click() {
			if(!Security.IsAuthorized(Permissions.AccountProcsQuickAdd,true)) {
				//only happens if permissions are changed after the program is opened. (Very Rare)
				MsgBox.Show(this,"Not authorized for Quick Procs.");
				LayoutToolBar();
				return;
			}
			//Main QuickCharge button was clicked.  Create a textbox that can be entered so users can insert manually entered proc codes.
			if(!Security.IsAuthorized(Permissions.ProcComplCreate,true)) {//Button doesn't show up unless they have AccountQuickCharge permission. 
				//user can still use dropdown, just not type in codes.
				contextMenuQuickProcs.Show(this,new Point(_butQuickProcs.Bounds.X,_butQuickProcs.Bounds.Y+_butQuickProcs.Bounds.Height));
				return; 
			}
			textQuickProcs.SetBounds(_butQuickProcs.Bounds.X+1,_butQuickProcs.Bounds.Y+2,_butQuickProcs.Bounds.Width-17,_butQuickProcs.Bounds.Height-2);
			textQuickProcs.Visible=true;
			textQuickProcs.BringToFront();
			textQuickProcs.Focus();
			textQuickProcs.Capture=true;
		}

		private void textQuickCharge_FocusLost(object sender,EventArgs e) {
			textQuickProcs.Text="";
			textQuickProcs.Visible=false;
			textQuickProcs.Capture=false;
		}

		private void textQuickCharge_KeyDown(object sender,KeyEventArgs e) {
			//This is only the KeyDown event, user can still type if we return here.
			if(e.KeyCode!=Keys.Enter) {
				return;
			}
			textQuickProcs.Visible=false;
			textQuickProcs.Capture=false;
			e.Handled=true;//Suppress the "ding" in windows when pressing enter.
			e.SuppressKeyPress=true;//Suppress the "ding" in windows when pressing enter.
			if(textQuickProcs.Text=="") {
				return;
			}
			Provider patProv=Providers.GetProv(PatCur.PriProv);
			FeeSched provFeeSched=FeeScheds.GetOne(patProv.FeeSched,FeeSchedC.GetListShort());
			if(AddProcAndValidate(textQuickProcs.Text,provFeeSched,patProv)) {
				SecurityLogs.MakeLogEntry(Permissions.AccountProcsQuickAdd,PatCur.PatNum
					,Lan.g(this,"The following procedures were added via the Quick Charge button from the Account module")
						+": "+string.Join(",",textQuickProcs.Text));
				ModuleSelected(PatCur.PatNum);
			}
			textQuickProcs.Text="";
		}

		private void menuItemQuickProcs_Click(object sender,EventArgs e) {
			//Quick Charge button won't be present unless they have AccountQuickCharge permission, no need to check it.
			//One of the QuickCharge menu items was clicked.
			if(sender.GetType()!=typeof(MenuItem)) {
				return;
			}
			Def quickChargeDef=_acctProcQuickAddDefs[contextMenuQuickProcs.MenuItems.IndexOf((MenuItem)sender)];
			string[] procCodes=quickChargeDef.ItemValue.Split(',');
			if(procCodes.Length==0) {
				//No items entered into the definition category.  Notify the user.
				MsgBox.Show(this,"There are no Quick Charge items in Setup | Definitions.  There must be at least one in order to use the Quick Charge drop down menu.");
			}
			List<string> procCodesAdded=new List<string>();
			Provider patProv=Providers.GetProv(PatCur.PriProv);
			FeeSched provFeeSched=FeeScheds.GetOne(patProv.FeeSched,FeeSchedC.GetListShort());
			for(int i=0;i<procCodes.Length;i++) {
				if(AddProcAndValidate(procCodes[i],provFeeSched,patProv)) {
					procCodesAdded.Add(procCodes[i]);
				}
			}
			if(procCodesAdded.Count > 0) {
				SecurityLogs.MakeLogEntry(Permissions.AccountProcsQuickAdd,PatCur.PatNum
					,Lan.g(this,"The following procedures were added via the Quick Charge button from the Account module")
						+": "+string.Join(",",procCodesAdded));
				ModuleSelected(PatCur.PatNum);
			}
		}

		///<summary>Validated the procedure code using FormProcEdit and prompts user for input if required.</summary>
		private bool AddProcAndValidate(string procString,FeeSched provFeeSched,Provider patProv) {
			ProcedureCode procCode=ProcedureCodes.GetProcCode(procString.Trim());
			if(procCode.CodeNum==0) {
				MsgBox.Show(this,"Invalid Procedure Code: "+procString.Trim());
				return false; //Invalid ProcCode string manually entered.
			}
			Fee fee=Fees.GetFee(procCode.CodeNum,provFeeSched.FeeSchedNum);
			Procedure proc=new Procedure();
			proc.ProcStatus=ProcStat.C;
			proc.ClinicNum=FormOpenDental.ClinicNum;
			proc.CodeNum=procCode.CodeNum;
			proc.DateEntryC=DateTime.Now;
			proc.DateTP=DateTime.Now;
			proc.PatNum=PatCur.PatNum;
			proc.ProcDate=DateTime.Now;
			if(!PrefC.GetBool(PrefName.EasyHidePublicHealth)) {
				proc.SiteNum=PatCur.SiteNum;
			}
			if(fee==null) {
				proc.ProcFee=0;
			}
			else {
				proc.ProcFee=fee.Amount;
			}
			proc.ProvNum=patProv.ProvNum;
			proc.UnitQty=1;
			Procedures.Insert(proc);
			//launch form silently to validate code. If entry errors occur the form will be shown to user, otherwise it will close imidiately.
			FormProcEdit FormPE=new FormProcEdit(proc,PatCur,FamCur,true);
			FormPE.IsNew=true;
			FormPE.ShowDialog();
			if(FormPE.DialogResult!=DialogResult.OK) {
				Procedures.Delete(proc.ProcNum);
				return false;
			}
			return true;
		}

		private void gridComm_CellDoubleClick(object sender,OpenDental.UI.ODGridClickEventArgs e) {
			int row=(int)gridComm.Rows[e.Row].Tag;
			if(DataSetMain.Tables["Commlog"].Rows[row]["CommlogNum"].ToString()!="0") {
				Commlog CommlogCur=
					Commlogs.GetOne(PIn.Long(DataSetMain.Tables["Commlog"].Rows[row]["CommlogNum"].ToString()));
				FormCommItem FormCI=new FormCommItem(CommlogCur);
				FormCI.ShowDialog();
				if(FormCI.DialogResult==DialogResult.OK) {
					ModuleSelected(PatCur.PatNum);
				}
			}
			else if(DataSetMain.Tables["Commlog"].Rows[row]["EmailMessageNum"].ToString()!="0") {
				EmailMessage email=
					EmailMessages.GetOne(PIn.Long(DataSetMain.Tables["Commlog"].Rows[row]["EmailMessageNum"].ToString()));
				if(email.SentOrReceived==EmailSentOrReceived.WebMailReceived
					|| email.SentOrReceived==EmailSentOrReceived.WebMailRecdRead
					|| email.SentOrReceived==EmailSentOrReceived.WebMailSent
					|| email.SentOrReceived==EmailSentOrReceived.WebMailSentRead) 
				{
					//web mail uses special secure messaging portal
					FormWebMailMessageEdit FormWMME=new FormWebMailMessageEdit(PatCur.PatNum,email.EmailMessageNum);
					if(FormWMME.ShowDialog()==DialogResult.OK) {
						ModuleSelected(PatCur.PatNum);
					}
				}
				else {
					FormEmailMessageEdit FormE=new FormEmailMessageEdit(email);
					FormE.ShowDialog();
					if(FormE.DialogResult==DialogResult.OK) {
						ModuleSelected(PatCur.PatNum);
					}
				}
			}
			else if(DataSetMain.Tables["Commlog"].Rows[row]["FormPatNum"].ToString()!="0") {
				FormPat form=FormPats.GetOne(PIn.Long(DataSetMain.Tables["Commlog"].Rows[row]["FormPatNum"].ToString()));
				FormFormPatEdit FormP=new FormFormPatEdit();
				FormP.FormPatCur=form;
				FormP.ShowDialog();
				if(FormP.DialogResult==DialogResult.OK) {
					ModuleSelected(PatCur.PatNum);
				}
			}
			else if(DataSetMain.Tables["Commlog"].Rows[row]["SheetNum"].ToString()!="0") {
				Sheet sheet=Sheets.GetSheet(PIn.Long(DataSetMain.Tables["Commlog"].Rows[row]["SheetNum"].ToString()));
				FormSheetFillEdit FormSFE=new FormSheetFillEdit(sheet);
				FormSFE.ShowDialog();
				if(FormSFE.DialogResult==DialogResult.OK) {
					ModuleSelected(PatCur.PatNum);
				}
			}
		}

		private void Parent_MouseWheel(Object sender,MouseEventArgs e){
			if(Visible){
				this.OnMouseWheel(e);
			}
		}

		private void gridRepeat_CellDoubleClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			FormRepeatChargeEdit FormR=new FormRepeatChargeEdit(RepeatChargeList[e.Row]);
			FormR.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}

		private void gridPatInfo_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			if(TerminalActives.PatIsInUse(PatCur.PatNum)) {
				MsgBox.Show(this,"Patient is currently entering info at a reception terminal.  Please try again later.");
				return;
			}
			if(gridPatInfo.Rows[e.Row].Tag!=null) {
				//patfield
				string tag=gridPatInfo.Rows[e.Row].Tag.ToString();
				tag=tag.Substring(8);//strips off all but the number: PatField1
				int index=PIn.Int(tag);
				PatField field=PatFields.GetByName(PatFieldDefs.ListShort[index].FieldName,_patFieldList);
				if(field==null) {
					field=new PatField();
					field.PatNum=PatCur.PatNum;
					field.FieldName=PatFieldDefs.ListShort[index].FieldName;
					if(PatFieldDefs.ListShort[index].FieldType==PatFieldType.Text) {
						FormPatFieldEdit FormPF=new FormPatFieldEdit(field);
						FormPF.IsNew=true;
						FormPF.ShowDialog();
					}
					if(PatFieldDefs.ListShort[index].FieldType==PatFieldType.PickList) {
						FormPatFieldPickEdit FormPF=new FormPatFieldPickEdit(field);
						FormPF.IsNew=true;
						FormPF.ShowDialog();
					}
					if(PatFieldDefs.ListShort[index].FieldType==PatFieldType.Date) {
						FormPatFieldDateEdit FormPF=new FormPatFieldDateEdit(field);
						FormPF.IsNew=true;
						FormPF.ShowDialog();
					}
					if(PatFieldDefs.ListShort[index].FieldType==PatFieldType.Checkbox) {
						FormPatFieldCheckEdit FormPF=new FormPatFieldCheckEdit(field);
						FormPF.IsNew=true;
						FormPF.ShowDialog();
					}
					if(PatFieldDefs.ListShort[index].FieldType==PatFieldType.Currency) {
						FormPatFieldCurrencyEdit FormPF=new FormPatFieldCurrencyEdit(field);
						FormPF.IsNew=true;
						FormPF.ShowDialog();
					}
				}
				else {
					if(PatFieldDefs.ListShort[index].FieldType==PatFieldType.Text) {
						FormPatFieldEdit FormPF=new FormPatFieldEdit(field);
						FormPF.ShowDialog();
					}
					if(PatFieldDefs.ListShort[index].FieldType==PatFieldType.PickList) {
						FormPatFieldPickEdit FormPF=new FormPatFieldPickEdit(field);
						FormPF.ShowDialog();
					}
					if(PatFieldDefs.ListShort[index].FieldType==PatFieldType.Date) {
						FormPatFieldDateEdit FormPF=new FormPatFieldDateEdit(field);
						FormPF.ShowDialog();
					}
					if(PatFieldDefs.ListShort[index].FieldType==PatFieldType.Checkbox) {
						FormPatFieldCheckEdit FormPF=new FormPatFieldCheckEdit(field);
						FormPF.ShowDialog();
					}
					if(PatFieldDefs.ListShort[index].FieldType==PatFieldType.Currency) {
						FormPatFieldCurrencyEdit FormPF=new FormPatFieldCurrencyEdit(field);
						FormPF.ShowDialog();
					}
				}
			}
			else {
				FormPatientEdit FormP=new FormPatientEdit(PatCur,FamCur);
				FormP.IsNew=false;
				FormP.ShowDialog();
				if(FormP.DialogResult==DialogResult.OK) {
					OnPatientSelected(PatCur);
				}
			}
			ModuleSelected(PatCur.PatNum);
		}

		#region ProgressNotes
		///<summary>The supplied procedure row must include these columns: ProcDate,ProcStatus,ProcCode,Surf,ToothNum, and ToothRange, all in raw database format.</summary>
		private bool ShouldDisplayProc(DataRow row) {
			switch ((ProcStat)PIn.Long(row["ProcStatus"].ToString())) {
				case ProcStat.TP:
					if (checkShowTP.Checked) {
						return true;
					}
					break;
				case ProcStat.C:
					if (checkShowC.Checked) {
						return true;
					}
					break;
				case ProcStat.EC:
					if (checkShowE.Checked) {
						return true;
					}
					break;
				case ProcStat.EO:
					if (checkShowE.Checked) {
						return true;
					}
					break;
				case ProcStat.R:
					if (checkShowR.Checked) {
						return true;
					}
					break;
				case ProcStat.D:
					if (checkAudit.Checked) {
						return true;
					}
					break;
			}
			return false;
		}

		private void FillProgNotes() {
			ArrayList selectedTeeth = new ArrayList();//integers 1-32
			for(int i = 0;i < 32;i++) {
				selectedTeeth.Add(i);
			}
			gridProg.BeginUpdate();
			gridProg.Columns.Clear();
			ODGridColumn col = new ODGridColumn(Lan.g("TableProg", "Date"), 67);
			gridProg.Columns.Add(col);
			if(!Clinics.IsMedicalPracticeOrClinic(FormOpenDental.ClinicNum)) {
				col = new ODGridColumn(Lan.g("TableProg","Th"),27);
				gridProg.Columns.Add(col);
				col = new ODGridColumn(Lan.g("TableProg","Surf"),40);
				gridProg.Columns.Add(col);
			}
			col = new ODGridColumn(Lan.g("TableProg", "Dx"), 28);
			gridProg.Columns.Add(col);
			col = new ODGridColumn(Lan.g("TableProg", "Description"), 218);
			gridProg.Columns.Add(col);
			col = new ODGridColumn(Lan.g("TableProg", "Stat"), 25);
			gridProg.Columns.Add(col);
			col = new ODGridColumn(Lan.g("TableProg", "Prov"), 42);
			gridProg.Columns.Add(col);
			col = new ODGridColumn(Lan.g("TableProg", "Amount"), 48, HorizontalAlignment.Right);
			gridProg.Columns.Add(col);
			col = new ODGridColumn(Lan.g("TableProg", "ADA Code"), 62, HorizontalAlignment.Center);
			gridProg.Columns.Add(col);
			col = new ODGridColumn(Lan.g("TableProg", "User"), 62, HorizontalAlignment.Center);
			gridProg.Columns.Add(col);
			col = new ODGridColumn(Lan.g("TableProg", "Signed"), 55, HorizontalAlignment.Center);
			gridProg.Columns.Add(col);
			gridProg.NoteSpanStart = 2;
			gridProg.NoteSpanStop = 7;
			gridProg.Rows.Clear();
			ODGridRow row;
			//Type type;
			if (DataSetMain == null) {
				gridProg.EndUpdate();
				return;
			}
			DataTable table = DataSetMain.Tables["ProgNotes"];
			//ProcList = new List<DataRow>();
			for (int i = 0; i < table.Rows.Count; i++) {
				if (table.Rows[i]["ProcNum"].ToString() != "0") {//if this is a procedure
					if (ShouldDisplayProc(table.Rows[i])) {
						//ProcList.Add(table.Rows[i]);//show it in the graphical tooth chart
						//and add it to the grid below.
					}
					else {
						continue;
					}
				}
				else if (table.Rows[i]["CommlogNum"].ToString() != "0") {//if this is a commlog
					if (!checkComm.Checked) {
						continue;
					}
				}
				else if (table.Rows[i]["RxNum"].ToString() != "0") {//if this is an Rx
					if (!checkRx.Checked) {
						continue;
					}
				}
				else if (table.Rows[i]["LabCaseNum"].ToString() != "0") {//if this is a LabCase
					if (!checkLabCase.Checked) {
						continue;
					}
				}
				else if (table.Rows[i]["AptNum"].ToString() != "0") {//if this is an Appointment
					if (!checkAppt.Checked) {
						continue;
					}
				}
				row = new ODGridRow();
				row.ColorLborder = Color.Black;
				//remember that columns that start with lowercase are already altered for display rather than being raw data.
				row.Cells.Add(table.Rows[i]["procDate"].ToString());
				if(!Clinics.IsMedicalPracticeOrClinic(FormOpenDental.ClinicNum)) {
					row.Cells.Add(table.Rows[i]["toothNum"].ToString());
					row.Cells.Add(table.Rows[i]["Surf"].ToString());
				}
				row.Cells.Add(table.Rows[i]["dx"].ToString());
				row.Cells.Add(table.Rows[i]["description"].ToString());
				row.Cells.Add(table.Rows[i]["procStatus"].ToString());
				row.Cells.Add(table.Rows[i]["prov"].ToString());
				row.Cells.Add(table.Rows[i]["procFee"].ToString());
				row.Cells.Add(table.Rows[i]["ProcCode"].ToString());
				row.Cells.Add(table.Rows[i]["user"].ToString());
				row.Cells.Add(table.Rows[i]["signature"].ToString());
				if (checkNotes.Checked) {
					row.Note = table.Rows[i]["note"].ToString();
				}
				row.ColorText = Color.FromArgb(PIn.Int(table.Rows[i]["colorText"].ToString()));
				row.ColorBackG = Color.FromArgb(PIn.Int(table.Rows[i]["colorBackG"].ToString()));
				row.Tag = table.Rows[i];
				gridProg.Rows.Add(row);
			
			}
			gridProg.EndUpdate();
			gridProg.ScrollToEnd();
		}

		private void gridProg_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			//Chartscrollval = gridProg.ScrollValue;
			DataRow row = (DataRow)gridProg.Rows[e.Row].Tag;
			if(row["ProcNum"].ToString() != "0") {
				if(checkAudit.Checked) {
					MsgBox.Show(this,"Not allowed to edit procedures when in audit mode.");
					return;
				}
				Procedure proc = Procedures.GetOneProc(PIn.Long(row["ProcNum"].ToString()),true);
				FormProcEdit FormP = new FormProcEdit(proc,PatCur,FamCur);
				FormP.ShowDialog();
				if(FormP.DialogResult != DialogResult.OK) {
					return;
				}
			}
			else if(row["CommlogNum"].ToString() != "0") {
				Commlog comm = Commlogs.GetOne(PIn.Long(row["CommlogNum"].ToString()));
				FormCommItem FormC = new FormCommItem(comm);
				FormC.ShowDialog();
				if(FormC.DialogResult != DialogResult.OK) {
					return;
				}
			}
			else if(row["RxNum"].ToString() != "0") {
				RxPat rx = RxPats.GetRx(PIn.Long(row["RxNum"].ToString()));
				FormRxEdit FormRxE = new FormRxEdit(PatCur,rx);
				FormRxE.ShowDialog();
				if(FormRxE.DialogResult != DialogResult.OK) {
					return;
				}
			}
			else if(row["LabCaseNum"].ToString() != "0") {
				LabCase lab = LabCases.GetOne(PIn.Long(row["LabCaseNum"].ToString()));
				FormLabCaseEdit FormL = new FormLabCaseEdit();
				FormL.CaseCur = lab;
				FormL.ShowDialog();
			}
			else if(row["TaskNum"].ToString() != "0") {
				Task task = Tasks.GetOne(PIn.Long(row["TaskNum"].ToString()));
				FormTaskEdit FormT = new FormTaskEdit(task,task.Copy());
				FormT.Closing+=new CancelEventHandler(TaskGoToEvent);
				FormT.Show();//non-modal
			}
			else if(row["AptNum"].ToString() != "0") {
				//Appointment apt=Appointments.GetOneApt(
				FormApptEdit FormA = new FormApptEdit(PIn.Long(row["AptNum"].ToString()));
				//PinIsVisible=false
				FormA.ShowDialog();
				if(FormA.DialogResult != DialogResult.OK) {
					return;
				}
			}
			else if(row["EmailMessageNum"].ToString() != "0") {
				EmailMessage msg = EmailMessages.GetOne(PIn.Long(row["EmailMessageNum"].ToString()));
				FormEmailMessageEdit FormE = new FormEmailMessageEdit(msg);
				FormE.ShowDialog();
				if(FormE.DialogResult != DialogResult.OK) {
					return;
				}
			}
			ModuleSelected(PatCur.PatNum);
		}

		public void TaskGoToEvent(object sender,CancelEventArgs e) {
			FormTaskEdit FormT=(FormTaskEdit)sender;
			TaskObjectType GotoType=FormT.GotoType;
			long keyNum=FormT.GotoKeyNum;
			if(GotoType==TaskObjectType.None) {
				return;
			}
			if(GotoType == TaskObjectType.Patient) {
				if(keyNum != 0) {
					Patient pat = Patients.GetPat(keyNum);
					OnPatientSelected(pat);
					ModuleSelected(pat.PatNum);
					return;
				}
			}
			if(GotoType == TaskObjectType.Appointment) {
				//There's nothing to do here, since we're not in the appt module.
				return;
			}
		}

		private void checkShowTP_Click(object sender,EventArgs e) {
			FillProgNotes();
		}

		private void checkShowC_Click(object sender,EventArgs e) {
			FillProgNotes();

		}

		private void checkShowE_Click(object sender,EventArgs e) {
			FillProgNotes();

		}

		private void checkShowR_Click(object sender,EventArgs e) {
			FillProgNotes();

		}

		private void checkAppt_Click(object sender,EventArgs e) {
			FillProgNotes();

		}

		private void checkComm_Click(object sender,EventArgs e) {
			FillProgNotes();

		}

		private void checkLabCase_Click(object sender,EventArgs e) {
			FillProgNotes();

		}

		private void checkRx_Click(object sender,EventArgs e) {
		if (checkRx.Checked)//since there is no double click event...this allows almost the same thing
            {
                checkShowTP.Checked=false;
                checkShowC.Checked=false;
                checkShowE.Checked=false;
                checkShowR.Checked=false;
                checkNotes.Checked=true;
                checkRx.Checked=true;
                checkComm.Checked=false;
                checkAppt.Checked=false;
				checkLabCase.Checked=false;
                checkExtraNotes.Checked=false;

            }

			FillProgNotes();

		}

		private void checkExtraNotes_Click(object sender,EventArgs e) {
			FillProgNotes();

		}

		private void checkNotes_Click(object sender,EventArgs e) {
			FillProgNotes();

		}

		private void butShowNone_Click(object sender,EventArgs e) {
			checkShowTP.Checked=false;
			checkShowC.Checked=false;
			checkShowE.Checked=false;
			checkShowR.Checked=false;
			checkAppt.Checked=false;
			checkComm.Checked=false;
			checkLabCase.Checked=false;
			checkRx.Checked=false;
			checkShowTeeth.Checked=false;

			FillProgNotes();

		}

		private void butShowAll_Click(object sender,EventArgs e) {
			checkShowTP.Checked=true;
			checkShowC.Checked=true;
			checkShowE.Checked=true;
			checkShowR.Checked=true;
			checkAppt.Checked=true;
			checkComm.Checked=true;
			checkLabCase.Checked=true;
			checkRx.Checked=true;
			checkShowTeeth.Checked=false;
			FillProgNotes();

		}

		private void gridProg_MouseUp(object sender,MouseEventArgs e) {

		}
		#endregion ProgressNotes

		private void checkShowFamilyComm_Click(object sender,EventArgs e) {
			FillComm();
		}

		private void labelInsRem_MouseEnter(object sender,EventArgs e) {
			groupBoxFamilyIns.Visible=true;
			groupBoxIndIns.Visible=true;
		}

		private void labelInsRem_MouseLeave(object sender,EventArgs e) {
			groupBoxFamilyIns.Visible=false;
			groupBoxIndIns.Visible=false;
		}

		private void labelInsRem_Click(object sender,EventArgs e) {
			if(!CultureInfo.CurrentCulture.Name.EndsWith("CA")) {//Canadian. en-CA or fr-CA
				//Since the bonus information in FormInsRemain is currently only helpful in Canada,
				//we have decided not to show the form for other countries at this time.
				return;
			}
			if(PatCur==null) {
				return;
			}
			FormInsRemain FormIR=new FormInsRemain(PatCur.PatNum);
			FormIR.ShowDialog();
		}

	}
}











