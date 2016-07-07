using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MigraDoc.DocumentObjectModel;
using OpenDental.Bridges;
using OpenDental.UI;
using OpenDentBusiness;
using OpenDentBusiness.HL7;
using OpenDentBusiness.UI;
using PdfSharp.Pdf;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormApptEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.ODGrid gridPatient;
		private OpenDental.UI.ODGrid gridComm;
		private IContainer components;
		private ComboBox comboConfirmed;
		private ComboBox comboUnschedStatus;
		private Label label4;
		private ComboBox comboStatus;
		private Label label5;
		private Label labelStatus;
		private OpenDental.UI.Button butAudit;
		private OpenDental.UI.Button butTask;
		private OpenDental.UI.Button butDelete;
		private OpenDental.UI.Button butPin;
		private Label label24;
		private CheckBox checkIsHygiene;
		private ComboBox comboClinic;
		private Label labelClinic;
		private ComboBox comboAssistant;
		private ComboBox comboProvHyg;
		private ComboBox comboProvNum;
		private Label label12;
		private CheckBox checkIsNewPatient;
		private Label label3;
		private Label label2;
		private OpenDental.UI.ODGrid gridProc;
		private System.Windows.Forms.Button butSlider;
		private TableTimeBar tbTime;
		private Label label6;
		private TextBox textTime;
		private ODtextBox textNote;
		private Label labelApptNote;
		private OpenDental.UI.Button butAddComm;
		public bool PinIsVisible;
		public bool PinClicked;
		public bool IsNew;
		private Appointment AptCur;
		private Appointment AptOld;
		///<summary>The string time pattern in the current increment. Not in the 5 minute increment.</summary>
		private StringBuilder strBTime;
		private bool mouseIsDown;
		private Point mouseOrigin;
		private Point sliderOrigin;
		private List <InsPlan> PlanList;
		private List<InsSub> SubList;
		private Patient pat;
		private Family fam;
		private ToolTip toolTip1;
		private ContextMenu contextMenuTimeArrived;
		private MenuItem menuItemArrivedNow;
		private ContextMenu contextMenuTimeSeated;
		private MenuItem menuItemSeatedNow;
		private ContextMenu contextMenuTimeDismissed;
		private MenuItem menuItemDismissedNow;
		private OpenDental.UI.Button butAdd;
		private OpenDental.UI.Button butDeleteProc;
		private OpenDental.UI.Button butComplete;
		private CheckBox checkTimeLocked;
		private OpenDental.UI.Button butPickHyg;
		private OpenDental.UI.Button butPickDentist;
		private ODGrid gridFields;
		private TextBox textTimeAskedToArrive;
		private Label label8;
		private OpenDental.UI.Button butPDF;
		///<summary>This is the way to pass a "signal" up to the parent form that OD is to close.</summary>
		public bool CloseOD;
		private ListBox listQuickAdd;
		private Panel panel1;
		private TextBox textRequirement;
		private UI.Button butRequirement;
		private UI.Button butInsPlan2;
		private UI.Button butInsPlan1;
		private TextBox textInsPlan2;
		private Label labelInsPlan2;
		private TextBox textInsPlan1;
		private Label labelInsPlan1;
		private TextBox textTimeDismissed;
		private Label label7;
		private TextBox textTimeSeated;
		private Label label1;
		private TextBox textTimeArrived;
		private Label labelTimeArrived;
		private TextBox textLabCase;
		private UI.Button butLab;
		private Label label9;
		private UI.Button butColorClear;
		private System.Windows.Forms.Button butColor;
		private UI.Button butText;
		private Label labelQuickAdd;
		private UI.Button butSyndromicObservations;
		private Label labelSyndromicObservations;
		///<summary>True if appt was double clicked on from the chart module gridProg.  Currently only used to trigger an appointment overlap check.</summary>
		public bool IsInChartModule;
		private ComboBox comboApptType;
		private Label label10;
		///<summary>True if appt was double clicked on from the ApptsOther form.  Currently only used to trigger an appointment overlap check.</summary>
		public bool IsInViewPatAppts;
		///<summary>Matches list of appointments in comboAppointmentType. Does not include hidden types unless current appointment is of that type.</summary>
		private List<AppointmentType> _listAppointmentType;
		///<summary>Procedure were attached/detached from appt and the user clicked cancel or closed the form.
		///Used in ApptModule to tell if we need to refresh.</summary>
		public bool HasProcsChangedAndCancel;
		///<summary>A list of all procs for this patient.  Enables the user to edit and attach procs from other appointments.</summary>
		private List<Procedure> _listProcs;
		///<summary>A list of all procs for this patient from the database.  Used for syncing and recovering information if appointment cancelled/deleted.</summary>
		private List<Procedure> _listProcsFromDB;
		///<summary>Lab for the current appointment.  It may be null if there is no lab.</summary>
		private LabCase _labCur;
		///<summary>A list of appointments for this patient that are either scheduled or planned.</summary>
		private List<Appointment> _listAppointments;
		///<summary>Stale deep copy of _listAppointments to use with sync.</summary>
		private List<Appointment> _listAppointmentsOld;
		private bool _isPlanned;
		private DataTable _tableFields;
		private DataTable _tableComms;
		private List<ProcedureCode> _listProcCodes;
		///<summary>A short list of procedures which were moved from another appointment to this appointment.</summary>
		private List<Procedure> _listProcsMoved;
		private Label labelPlannedComplete;
		private List<Provider> _listProviders;

		///<summary></summary>
		public FormApptEdit(long aptNum)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
			AptCur=Appointments.GetOneApt(aptNum);//We need this query to get the PatNum for the appointment.
			_listProcsMoved=new List<Procedure>();
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormApptEdit));
			this.comboConfirmed = new System.Windows.Forms.ComboBox();
			this.comboUnschedStatus = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.comboStatus = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.labelStatus = new System.Windows.Forms.Label();
			this.label24 = new System.Windows.Forms.Label();
			this.checkIsHygiene = new System.Windows.Forms.CheckBox();
			this.comboClinic = new System.Windows.Forms.ComboBox();
			this.labelClinic = new System.Windows.Forms.Label();
			this.comboAssistant = new System.Windows.Forms.ComboBox();
			this.comboProvHyg = new System.Windows.Forms.ComboBox();
			this.comboProvNum = new System.Windows.Forms.ComboBox();
			this.label12 = new System.Windows.Forms.Label();
			this.checkIsNewPatient = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.labelApptNote = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.textTime = new System.Windows.Forms.TextBox();
			this.butSlider = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.checkTimeLocked = new System.Windows.Forms.CheckBox();
			this.contextMenuTimeArrived = new System.Windows.Forms.ContextMenu();
			this.menuItemArrivedNow = new System.Windows.Forms.MenuItem();
			this.contextMenuTimeSeated = new System.Windows.Forms.ContextMenu();
			this.menuItemSeatedNow = new System.Windows.Forms.MenuItem();
			this.contextMenuTimeDismissed = new System.Windows.Forms.ContextMenu();
			this.menuItemDismissedNow = new System.Windows.Forms.MenuItem();
			this.textTimeAskedToArrive = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.listQuickAdd = new System.Windows.Forms.ListBox();
			this.labelQuickAdd = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.comboApptType = new System.Windows.Forms.ComboBox();
			this.label10 = new System.Windows.Forms.Label();
			this.labelSyndromicObservations = new System.Windows.Forms.Label();
			this.butSyndromicObservations = new OpenDental.UI.Button();
			this.label9 = new System.Windows.Forms.Label();
			this.butColorClear = new OpenDental.UI.Button();
			this.butColor = new System.Windows.Forms.Button();
			this.textRequirement = new System.Windows.Forms.TextBox();
			this.butRequirement = new OpenDental.UI.Button();
			this.butInsPlan2 = new OpenDental.UI.Button();
			this.butInsPlan1 = new OpenDental.UI.Button();
			this.textInsPlan2 = new System.Windows.Forms.TextBox();
			this.labelInsPlan2 = new System.Windows.Forms.Label();
			this.textInsPlan1 = new System.Windows.Forms.TextBox();
			this.labelInsPlan1 = new System.Windows.Forms.Label();
			this.textTimeDismissed = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textTimeSeated = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textTimeArrived = new System.Windows.Forms.TextBox();
			this.labelTimeArrived = new System.Windows.Forms.Label();
			this.textLabCase = new System.Windows.Forms.TextBox();
			this.butLab = new OpenDental.UI.Button();
			this.butPickHyg = new OpenDental.UI.Button();
			this.butPickDentist = new OpenDental.UI.Button();
			this.gridFields = new OpenDental.UI.ODGrid();
			this.gridPatient = new OpenDental.UI.ODGrid();
			this.gridComm = new OpenDental.UI.ODGrid();
			this.gridProc = new OpenDental.UI.ODGrid();
			this.butPDF = new OpenDental.UI.Button();
			this.butComplete = new OpenDental.UI.Button();
			this.butDeleteProc = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.textNote = new OpenDental.ODtextBox();
			this.butAddComm = new OpenDental.UI.Button();
			this.tbTime = new OpenDental.TableTimeBar();
			this.butAudit = new OpenDental.UI.Button();
			this.butTask = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.butPin = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butText = new OpenDental.UI.Button();
			this.labelPlannedComplete = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// comboConfirmed
			// 
			this.comboConfirmed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboConfirmed.Location = new System.Drawing.Point(114, 42);
			this.comboConfirmed.MaxDropDownItems = 30;
			this.comboConfirmed.Name = "comboConfirmed";
			this.comboConfirmed.Size = new System.Drawing.Size(126, 21);
			this.comboConfirmed.TabIndex = 84;
			this.comboConfirmed.SelectionChangeCommitted += new System.EventHandler(this.comboConfirmed_SelectionChangeCommitted);
			// 
			// comboUnschedStatus
			// 
			this.comboUnschedStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboUnschedStatus.Location = new System.Drawing.Point(114, 21);
			this.comboUnschedStatus.MaxDropDownItems = 30;
			this.comboUnschedStatus.Name = "comboUnschedStatus";
			this.comboUnschedStatus.Size = new System.Drawing.Size(126, 21);
			this.comboUnschedStatus.TabIndex = 83;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(1, 24);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(111, 15);
			this.label4.TabIndex = 82;
			this.label4.Text = "Unscheduled Status";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboStatus
			// 
			this.comboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboStatus.Location = new System.Drawing.Point(114, 0);
			this.comboStatus.MaxDropDownItems = 30;
			this.comboStatus.Name = "comboStatus";
			this.comboStatus.Size = new System.Drawing.Size(126, 21);
			this.comboStatus.TabIndex = 81;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(1, 44);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(111, 16);
			this.label5.TabIndex = 80;
			this.label5.Text = "Confirmed";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelStatus
			// 
			this.labelStatus.Location = new System.Drawing.Point(1, 3);
			this.labelStatus.Name = "labelStatus";
			this.labelStatus.Size = new System.Drawing.Size(111, 15);
			this.labelStatus.TabIndex = 79;
			this.labelStatus.Text = "Status";
			this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label24
			// 
			this.label24.Location = new System.Drawing.Point(128, 148);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(113, 16);
			this.label24.TabIndex = 138;
			this.label24.Text = "(use hyg color)";
			// 
			// checkIsHygiene
			// 
			this.checkIsHygiene.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsHygiene.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIsHygiene.Location = new System.Drawing.Point(23, 148);
			this.checkIsHygiene.Name = "checkIsHygiene";
			this.checkIsHygiene.Size = new System.Drawing.Size(104, 16);
			this.checkIsHygiene.TabIndex = 137;
			this.checkIsHygiene.Text = "Is Hygiene";
			this.checkIsHygiene.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboClinic
			// 
			this.comboClinic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboClinic.Location = new System.Drawing.Point(114, 83);
			this.comboClinic.MaxDropDownItems = 30;
			this.comboClinic.Name = "comboClinic";
			this.comboClinic.Size = new System.Drawing.Size(126, 21);
			this.comboClinic.TabIndex = 136;
			// 
			// labelClinic
			// 
			this.labelClinic.Location = new System.Drawing.Point(13, 86);
			this.labelClinic.Name = "labelClinic";
			this.labelClinic.Size = new System.Drawing.Size(98, 16);
			this.labelClinic.TabIndex = 135;
			this.labelClinic.Text = "Clinic";
			this.labelClinic.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboAssistant
			// 
			this.comboAssistant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboAssistant.Location = new System.Drawing.Point(114, 164);
			this.comboAssistant.MaxDropDownItems = 30;
			this.comboAssistant.Name = "comboAssistant";
			this.comboAssistant.Size = new System.Drawing.Size(126, 21);
			this.comboAssistant.TabIndex = 133;
			// 
			// comboProvHyg
			// 
			this.comboProvHyg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboProvHyg.Location = new System.Drawing.Point(114, 126);
			this.comboProvHyg.MaxDropDownItems = 30;
			this.comboProvHyg.Name = "comboProvHyg";
			this.comboProvHyg.Size = new System.Drawing.Size(107, 21);
			this.comboProvHyg.TabIndex = 132;
			// 
			// comboProvNum
			// 
			this.comboProvNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboProvNum.Location = new System.Drawing.Point(114, 104);
			this.comboProvNum.MaxDropDownItems = 30;
			this.comboProvNum.Name = "comboProvNum";
			this.comboProvNum.Size = new System.Drawing.Size(107, 21);
			this.comboProvNum.TabIndex = 131;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(13, 167);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(98, 16);
			this.label12.TabIndex = 129;
			this.label12.Text = "Assistant";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkIsNewPatient
			// 
			this.checkIsNewPatient.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsNewPatient.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIsNewPatient.Location = new System.Drawing.Point(17, 64);
			this.checkIsNewPatient.Name = "checkIsNewPatient";
			this.checkIsNewPatient.Size = new System.Drawing.Size(110, 17);
			this.checkIsNewPatient.TabIndex = 128;
			this.checkIsNewPatient.Text = "New Patient";
			this.checkIsNewPatient.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(15, 128);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(98, 16);
			this.label3.TabIndex = 127;
			this.label3.Text = "Hygienist";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(14, 107);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(98, 16);
			this.label2.TabIndex = 126;
			this.label2.Text = "Provider";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelApptNote
			// 
			this.labelApptNote.Location = new System.Drawing.Point(20, 451);
			this.labelApptNote.Name = "labelApptNote";
			this.labelApptNote.Size = new System.Drawing.Size(197, 16);
			this.labelApptNote.TabIndex = 141;
			this.labelApptNote.Text = "Appointment Note";
			this.labelApptNote.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(-1, 190);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(113, 14);
			this.label6.TabIndex = 65;
			this.label6.Text = "Time Length";
			this.label6.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// textTime
			// 
			this.textTime.Location = new System.Drawing.Point(114, 187);
			this.textTime.Name = "textTime";
			this.textTime.ReadOnly = true;
			this.textTime.Size = new System.Drawing.Size(66, 20);
			this.textTime.TabIndex = 62;
			// 
			// butSlider
			// 
			this.butSlider.BackColor = System.Drawing.SystemColors.ControlDark;
			this.butSlider.Location = new System.Drawing.Point(6, 90);
			this.butSlider.Name = "butSlider";
			this.butSlider.Size = new System.Drawing.Size(12, 15);
			this.butSlider.TabIndex = 60;
			this.butSlider.UseVisualStyleBackColor = false;
			this.butSlider.MouseDown += new System.Windows.Forms.MouseEventHandler(this.butSlider_MouseDown);
			this.butSlider.MouseMove += new System.Windows.Forms.MouseEventHandler(this.butSlider_MouseMove);
			this.butSlider.MouseUp += new System.Windows.Forms.MouseEventHandler(this.butSlider_MouseUp);
			// 
			// checkTimeLocked
			// 
			this.checkTimeLocked.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkTimeLocked.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkTimeLocked.Location = new System.Drawing.Point(-1, 210);
			this.checkTimeLocked.Name = "checkTimeLocked";
			this.checkTimeLocked.Size = new System.Drawing.Size(128, 16);
			this.checkTimeLocked.TabIndex = 148;
			this.checkTimeLocked.Text = "Time Locked";
			this.checkTimeLocked.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkTimeLocked.Click += new System.EventHandler(this.checkTimeLocked_Click);
			// 
			// contextMenuTimeArrived
			// 
			this.contextMenuTimeArrived.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemArrivedNow});
			// 
			// menuItemArrivedNow
			// 
			this.menuItemArrivedNow.Index = 0;
			this.menuItemArrivedNow.Text = "Now";
			this.menuItemArrivedNow.Click += new System.EventHandler(this.menuItemArrivedNow_Click);
			// 
			// contextMenuTimeSeated
			// 
			this.contextMenuTimeSeated.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemSeatedNow});
			// 
			// menuItemSeatedNow
			// 
			this.menuItemSeatedNow.Index = 0;
			this.menuItemSeatedNow.Text = "Now";
			this.menuItemSeatedNow.Click += new System.EventHandler(this.menuItemSeatedNow_Click);
			// 
			// contextMenuTimeDismissed
			// 
			this.contextMenuTimeDismissed.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemDismissedNow});
			// 
			// menuItemDismissedNow
			// 
			this.menuItemDismissedNow.Index = 0;
			this.menuItemDismissedNow.Text = "Now";
			this.menuItemDismissedNow.Click += new System.EventHandler(this.menuItemDismissedNow_Click);
			// 
			// textTimeAskedToArrive
			// 
			this.textTimeAskedToArrive.Location = new System.Drawing.Point(114, 269);
			this.textTimeAskedToArrive.Name = "textTimeAskedToArrive";
			this.textTimeAskedToArrive.Size = new System.Drawing.Size(126, 20);
			this.textTimeAskedToArrive.TabIndex = 146;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(-1, 271);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(113, 18);
			this.label8.TabIndex = 160;
			this.label8.Text = "Time Ask To Arrive";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// listQuickAdd
			// 
			this.listQuickAdd.IntegralHeight = false;
			this.listQuickAdd.Location = new System.Drawing.Point(282, 48);
			this.listQuickAdd.Name = "listQuickAdd";
			this.listQuickAdd.Size = new System.Drawing.Size(150, 355);
			this.listQuickAdd.TabIndex = 163;
			this.listQuickAdd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listQuickAdd_MouseDown);
			// 
			// labelQuickAdd
			// 
			this.labelQuickAdd.Location = new System.Drawing.Point(282, 7);
			this.labelQuickAdd.Name = "labelQuickAdd";
			this.labelQuickAdd.Size = new System.Drawing.Size(143, 39);
			this.labelQuickAdd.TabIndex = 162;
			this.labelQuickAdd.Text = "Single click on items in the list below to add them to this appointment.";
			// 
			// panel1
			// 
			this.panel1.AutoScroll = true;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.comboApptType);
			this.panel1.Controls.Add(this.label10);
			this.panel1.Controls.Add(this.labelSyndromicObservations);
			this.panel1.Controls.Add(this.butSyndromicObservations);
			this.panel1.Controls.Add(this.label9);
			this.panel1.Controls.Add(this.butColorClear);
			this.panel1.Controls.Add(this.butColor);
			this.panel1.Controls.Add(this.textRequirement);
			this.panel1.Controls.Add(this.butRequirement);
			this.panel1.Controls.Add(this.butInsPlan2);
			this.panel1.Controls.Add(this.butInsPlan1);
			this.panel1.Controls.Add(this.textInsPlan2);
			this.panel1.Controls.Add(this.labelInsPlan2);
			this.panel1.Controls.Add(this.textInsPlan1);
			this.panel1.Controls.Add(this.labelInsPlan1);
			this.panel1.Controls.Add(this.textTimeDismissed);
			this.panel1.Controls.Add(this.label7);
			this.panel1.Controls.Add(this.textTimeSeated);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.textTimeArrived);
			this.panel1.Controls.Add(this.labelTimeArrived);
			this.panel1.Controls.Add(this.textLabCase);
			this.panel1.Controls.Add(this.butLab);
			this.panel1.Controls.Add(this.comboStatus);
			this.panel1.Controls.Add(this.checkIsNewPatient);
			this.panel1.Controls.Add(this.comboConfirmed);
			this.panel1.Controls.Add(this.label24);
			this.panel1.Controls.Add(this.textTimeAskedToArrive);
			this.panel1.Controls.Add(this.comboUnschedStatus);
			this.panel1.Controls.Add(this.label8);
			this.panel1.Controls.Add(this.checkIsHygiene);
			this.panel1.Controls.Add(this.comboClinic);
			this.panel1.Controls.Add(this.labelClinic);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.comboAssistant);
			this.panel1.Controls.Add(this.butPickHyg);
			this.panel1.Controls.Add(this.comboProvHyg);
			this.panel1.Controls.Add(this.comboProvNum);
			this.panel1.Controls.Add(this.butPickDentist);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.label12);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.labelStatus);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.textTime);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.checkTimeLocked);
			this.panel1.Location = new System.Drawing.Point(21, 2);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(260, 447);
			this.panel1.TabIndex = 164;
			// 
			// comboApptType
			// 
			this.comboApptType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboApptType.Location = new System.Drawing.Point(114, 246);
			this.comboApptType.MaxDropDownItems = 30;
			this.comboApptType.Name = "comboApptType";
			this.comboApptType.Size = new System.Drawing.Size(126, 21);
			this.comboApptType.TabIndex = 183;
			this.comboApptType.SelectionChangeCommitted += new System.EventHandler(this.comboApptType_SelectionChangeCommitted);
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(13, 249);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(98, 16);
			this.label10.TabIndex = 182;
			this.label10.Text = "Appointment Type";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelSyndromicObservations
			// 
			this.labelSyndromicObservations.Location = new System.Drawing.Point(63, 486);
			this.labelSyndromicObservations.Name = "labelSyndromicObservations";
			this.labelSyndromicObservations.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.labelSyndromicObservations.Size = new System.Drawing.Size(174, 16);
			this.labelSyndromicObservations.TabIndex = 181;
			this.labelSyndromicObservations.Text = "(Syndromic Observations)";
			this.labelSyndromicObservations.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.labelSyndromicObservations.Visible = false;
			// 
			// butSyndromicObservations
			// 
			this.butSyndromicObservations.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSyndromicObservations.Autosize = true;
			this.butSyndromicObservations.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSyndromicObservations.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSyndromicObservations.CornerRadius = 4F;
			this.butSyndromicObservations.Location = new System.Drawing.Point(15, 484);
			this.butSyndromicObservations.Name = "butSyndromicObservations";
			this.butSyndromicObservations.Size = new System.Drawing.Size(46, 20);
			this.butSyndromicObservations.TabIndex = 180;
			this.butSyndromicObservations.Text = "Obs";
			this.butSyndromicObservations.Visible = false;
			this.butSyndromicObservations.Click += new System.EventHandler(this.butSyndromicObservations_Click);
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(-1, 228);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(113, 14);
			this.label9.TabIndex = 179;
			this.label9.Text = "Color";
			this.label9.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// butColorClear
			// 
			this.butColorClear.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butColorClear.Autosize = true;
			this.butColorClear.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butColorClear.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butColorClear.CornerRadius = 4F;
			this.butColorClear.Location = new System.Drawing.Point(137, 224);
			this.butColorClear.Name = "butColorClear";
			this.butColorClear.Size = new System.Drawing.Size(39, 20);
			this.butColorClear.TabIndex = 178;
			this.butColorClear.Text = "none";
			this.butColorClear.Click += new System.EventHandler(this.butColorClear_Click);
			// 
			// butColor
			// 
			this.butColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.butColor.Location = new System.Drawing.Point(114, 225);
			this.butColor.Name = "butColor";
			this.butColor.Size = new System.Drawing.Size(21, 19);
			this.butColor.TabIndex = 177;
			this.butColor.Click += new System.EventHandler(this.butColor_Click);
			// 
			// textRequirement
			// 
			this.textRequirement.Location = new System.Drawing.Point(63, 432);
			this.textRequirement.Multiline = true;
			this.textRequirement.Name = "textRequirement";
			this.textRequirement.ReadOnly = true;
			this.textRequirement.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textRequirement.Size = new System.Drawing.Size(177, 53);
			this.textRequirement.TabIndex = 164;
			// 
			// butRequirement
			// 
			this.butRequirement.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butRequirement.Autosize = true;
			this.butRequirement.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRequirement.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRequirement.CornerRadius = 4F;
			this.butRequirement.Location = new System.Drawing.Point(15, 432);
			this.butRequirement.Name = "butRequirement";
			this.butRequirement.Size = new System.Drawing.Size(46, 20);
			this.butRequirement.TabIndex = 163;
			this.butRequirement.Text = "Req";
			this.butRequirement.Click += new System.EventHandler(this.butRequirement_Click);
			// 
			// butInsPlan2
			// 
			this.butInsPlan2.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butInsPlan2.Autosize = false;
			this.butInsPlan2.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butInsPlan2.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butInsPlan2.CornerRadius = 2F;
			this.butInsPlan2.Location = new System.Drawing.Point(222, 411);
			this.butInsPlan2.Name = "butInsPlan2";
			this.butInsPlan2.Size = new System.Drawing.Size(18, 20);
			this.butInsPlan2.TabIndex = 176;
			this.butInsPlan2.Text = "...";
			this.butInsPlan2.Click += new System.EventHandler(this.butInsPlan2_Click);
			// 
			// butInsPlan1
			// 
			this.butInsPlan1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butInsPlan1.Autosize = false;
			this.butInsPlan1.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butInsPlan1.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butInsPlan1.CornerRadius = 2F;
			this.butInsPlan1.Location = new System.Drawing.Point(222, 390);
			this.butInsPlan1.Name = "butInsPlan1";
			this.butInsPlan1.Size = new System.Drawing.Size(18, 20);
			this.butInsPlan1.TabIndex = 175;
			this.butInsPlan1.Text = "...";
			this.butInsPlan1.Click += new System.EventHandler(this.butInsPlan1_Click);
			// 
			// textInsPlan2
			// 
			this.textInsPlan2.Location = new System.Drawing.Point(63, 411);
			this.textInsPlan2.Name = "textInsPlan2";
			this.textInsPlan2.ReadOnly = true;
			this.textInsPlan2.Size = new System.Drawing.Size(158, 20);
			this.textInsPlan2.TabIndex = 174;
			// 
			// labelInsPlan2
			// 
			this.labelInsPlan2.Location = new System.Drawing.Point(2, 413);
			this.labelInsPlan2.Name = "labelInsPlan2";
			this.labelInsPlan2.Size = new System.Drawing.Size(59, 16);
			this.labelInsPlan2.TabIndex = 173;
			this.labelInsPlan2.Text = "InsPlan 2";
			this.labelInsPlan2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textInsPlan1
			// 
			this.textInsPlan1.Location = new System.Drawing.Point(63, 390);
			this.textInsPlan1.Name = "textInsPlan1";
			this.textInsPlan1.ReadOnly = true;
			this.textInsPlan1.Size = new System.Drawing.Size(158, 20);
			this.textInsPlan1.TabIndex = 172;
			// 
			// labelInsPlan1
			// 
			this.labelInsPlan1.Location = new System.Drawing.Point(2, 392);
			this.labelInsPlan1.Name = "labelInsPlan1";
			this.labelInsPlan1.Size = new System.Drawing.Size(59, 16);
			this.labelInsPlan1.TabIndex = 171;
			this.labelInsPlan1.Text = "InsPlan 1";
			this.labelInsPlan1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textTimeDismissed
			// 
			this.textTimeDismissed.Location = new System.Drawing.Point(114, 329);
			this.textTimeDismissed.Name = "textTimeDismissed";
			this.textTimeDismissed.Size = new System.Drawing.Size(126, 20);
			this.textTimeDismissed.TabIndex = 170;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(-1, 331);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(113, 16);
			this.label7.TabIndex = 169;
			this.label7.Text = "Time Dismissed";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textTimeSeated
			// 
			this.textTimeSeated.Location = new System.Drawing.Point(114, 309);
			this.textTimeSeated.Name = "textTimeSeated";
			this.textTimeSeated.Size = new System.Drawing.Size(126, 20);
			this.textTimeSeated.TabIndex = 168;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(-1, 311);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(113, 16);
			this.label1.TabIndex = 166;
			this.label1.Text = "Time Seated";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textTimeArrived
			// 
			this.textTimeArrived.Location = new System.Drawing.Point(114, 289);
			this.textTimeArrived.Name = "textTimeArrived";
			this.textTimeArrived.Size = new System.Drawing.Size(126, 20);
			this.textTimeArrived.TabIndex = 167;
			// 
			// labelTimeArrived
			// 
			this.labelTimeArrived.Location = new System.Drawing.Point(-1, 291);
			this.labelTimeArrived.Name = "labelTimeArrived";
			this.labelTimeArrived.Size = new System.Drawing.Size(113, 16);
			this.labelTimeArrived.TabIndex = 165;
			this.labelTimeArrived.Text = "Time Arrived";
			this.labelTimeArrived.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textLabCase
			// 
			this.textLabCase.AcceptsReturn = true;
			this.textLabCase.Location = new System.Drawing.Point(63, 354);
			this.textLabCase.Multiline = true;
			this.textLabCase.Name = "textLabCase";
			this.textLabCase.ReadOnly = true;
			this.textLabCase.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textLabCase.Size = new System.Drawing.Size(177, 34);
			this.textLabCase.TabIndex = 162;
			// 
			// butLab
			// 
			this.butLab.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butLab.Autosize = true;
			this.butLab.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLab.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLab.CornerRadius = 4F;
			this.butLab.Location = new System.Drawing.Point(15, 354);
			this.butLab.Name = "butLab";
			this.butLab.Size = new System.Drawing.Size(46, 20);
			this.butLab.TabIndex = 161;
			this.butLab.Text = "Lab";
			this.butLab.Click += new System.EventHandler(this.butLab_Click);
			// 
			// butPickHyg
			// 
			this.butPickHyg.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPickHyg.Autosize = false;
			this.butPickHyg.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPickHyg.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPickHyg.CornerRadius = 2F;
			this.butPickHyg.Location = new System.Drawing.Point(222, 127);
			this.butPickHyg.Name = "butPickHyg";
			this.butPickHyg.Size = new System.Drawing.Size(18, 20);
			this.butPickHyg.TabIndex = 158;
			this.butPickHyg.Text = "...";
			this.butPickHyg.Click += new System.EventHandler(this.butPickHyg_Click);
			// 
			// butPickDentist
			// 
			this.butPickDentist.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPickDentist.Autosize = false;
			this.butPickDentist.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPickDentist.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPickDentist.CornerRadius = 2F;
			this.butPickDentist.Location = new System.Drawing.Point(222, 105);
			this.butPickDentist.Name = "butPickDentist";
			this.butPickDentist.Size = new System.Drawing.Size(18, 20);
			this.butPickDentist.TabIndex = 157;
			this.butPickDentist.Text = "...";
			this.butPickDentist.Click += new System.EventHandler(this.butPickDentist_Click);
			// 
			// gridFields
			// 
			this.gridFields.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gridFields.HasAddButton = false;
			this.gridFields.HasMultilineHeaders = false;
			this.gridFields.HScrollVisible = false;
			this.gridFields.Location = new System.Drawing.Point(21, 578);
			this.gridFields.Name = "gridFields";
			this.gridFields.ScrollValue = 0;
			this.gridFields.Size = new System.Drawing.Size(259, 118);
			this.gridFields.TabIndex = 159;
			this.gridFields.Title = "Appt Fields";
			this.gridFields.TranslationName = "FormApptEdit";
			this.gridFields.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridFields_CellDoubleClick);
			// 
			// gridPatient
			// 
			this.gridPatient.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gridPatient.HasAddButton = false;
			this.gridPatient.HasMultilineHeaders = false;
			this.gridPatient.HScrollVisible = false;
			this.gridPatient.Location = new System.Drawing.Point(282, 405);
			this.gridPatient.Name = "gridPatient";
			this.gridPatient.ScrollValue = 0;
			this.gridPatient.Size = new System.Drawing.Size(258, 291);
			this.gridPatient.TabIndex = 0;
			this.gridPatient.Title = "Patient Info";
			this.gridPatient.TranslationName = "TableApptPtInfo";
			this.gridPatient.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridPatient_CellClick);
			this.gridPatient.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gridPatient_MouseMove);
			// 
			// gridComm
			// 
			this.gridComm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridComm.HasAddButton = false;
			this.gridComm.HasMultilineHeaders = false;
			this.gridComm.HScrollVisible = false;
			this.gridComm.Location = new System.Drawing.Point(542, 405);
			this.gridComm.Name = "gridComm";
			this.gridComm.ScrollValue = 0;
			this.gridComm.Size = new System.Drawing.Size(335, 291);
			this.gridComm.TabIndex = 1;
			this.gridComm.Title = "Communications Log - Appointment Scheduling";
			this.gridComm.TranslationName = "TableCommLog";
			this.gridComm.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridComm_CellDoubleClick);
			this.gridComm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gridComm_MouseMove);
			// 
			// gridProc
			// 
			this.gridProc.AllowSelection = false;
			this.gridProc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridProc.HasAddButton = false;
			this.gridProc.HasMultilineHeaders = false;
			this.gridProc.HScrollVisible = false;
			this.gridProc.Location = new System.Drawing.Point(434, 28);
			this.gridProc.Name = "gridProc";
			this.gridProc.ScrollValue = 0;
			this.gridProc.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridProc.Size = new System.Drawing.Size(538, 375);
			this.gridProc.TabIndex = 139;
			this.gridProc.Title = "Procedures on this Appointment";
			this.gridProc.TranslationName = "TableApptProcs";
			this.gridProc.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridProc_CellDoubleClick);
			this.gridProc.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridProc_CellClick);
			// 
			// butPDF
			// 
			this.butPDF.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPDF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butPDF.Autosize = true;
			this.butPDF.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPDF.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPDF.CornerRadius = 4F;
			this.butPDF.Location = new System.Drawing.Point(880, 457);
			this.butPDF.Name = "butPDF";
			this.butPDF.Size = new System.Drawing.Size(92, 24);
			this.butPDF.TabIndex = 161;
			this.butPDF.Text = "Notes PDF";
			this.butPDF.Visible = false;
			this.butPDF.Click += new System.EventHandler(this.butPDF_Click);
			// 
			// butComplete
			// 
			this.butComplete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butComplete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butComplete.Autosize = true;
			this.butComplete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butComplete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butComplete.CornerRadius = 4F;
			this.butComplete.Location = new System.Drawing.Point(880, 483);
			this.butComplete.Name = "butComplete";
			this.butComplete.Size = new System.Drawing.Size(92, 24);
			this.butComplete.TabIndex = 155;
			this.butComplete.Text = "Finish && Send";
			this.butComplete.Visible = false;
			this.butComplete.Click += new System.EventHandler(this.butComplete_Click);
			// 
			// butDeleteProc
			// 
			this.butDeleteProc.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDeleteProc.Autosize = true;
			this.butDeleteProc.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDeleteProc.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDeleteProc.CornerRadius = 4F;
			this.butDeleteProc.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDeleteProc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDeleteProc.Location = new System.Drawing.Point(434, 2);
			this.butDeleteProc.Name = "butDeleteProc";
			this.butDeleteProc.Size = new System.Drawing.Size(75, 24);
			this.butDeleteProc.TabIndex = 154;
			this.butDeleteProc.Text = "Delete";
			this.butDeleteProc.Click += new System.EventHandler(this.butDeleteProc_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(510, 2);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(75, 24);
			this.butAdd.TabIndex = 152;
			this.butAdd.Text = "Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// textNote
			// 
			this.textNote.AcceptsTab = true;
			this.textNote.BackColor = System.Drawing.SystemColors.Window;
			this.textNote.DetectUrls = false;
			this.textNote.Location = new System.Drawing.Point(21, 469);
			this.textNote.Name = "textNote";
			this.textNote.QuickPasteType = OpenDentBusiness.QuickPasteType.Appointment;
			this.textNote.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textNote.Size = new System.Drawing.Size(260, 106);
			this.textNote.TabIndex = 142;
			this.textNote.Text = "";
			// 
			// butAddComm
			// 
			this.butAddComm.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAddComm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butAddComm.Autosize = true;
			this.butAddComm.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddComm.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddComm.CornerRadius = 4F;
			this.butAddComm.Image = global::OpenDental.Properties.Resources.commlog;
			this.butAddComm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddComm.Location = new System.Drawing.Point(880, 405);
			this.butAddComm.Name = "butAddComm";
			this.butAddComm.Size = new System.Drawing.Size(92, 24);
			this.butAddComm.TabIndex = 143;
			this.butAddComm.Text = "Co&mm";
			this.butAddComm.Click += new System.EventHandler(this.butAddComm_Click);
			// 
			// tbTime
			// 
			this.tbTime.BackColor = System.Drawing.SystemColors.Window;
			this.tbTime.Location = new System.Drawing.Point(4, 6);
			this.tbTime.Name = "tbTime";
			this.tbTime.ScrollValue = 150;
			this.tbTime.SelectedIndices = new int[0];
			this.tbTime.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbTime.Size = new System.Drawing.Size(15, 561);
			this.tbTime.TabIndex = 59;
			// 
			// butAudit
			// 
			this.butAudit.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAudit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butAudit.Autosize = true;
			this.butAudit.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAudit.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAudit.CornerRadius = 4F;
			this.butAudit.Location = new System.Drawing.Point(880, 509);
			this.butAudit.Name = "butAudit";
			this.butAudit.Size = new System.Drawing.Size(92, 24);
			this.butAudit.TabIndex = 125;
			this.butAudit.Text = "Audit Trail";
			this.butAudit.Click += new System.EventHandler(this.butAudit_Click);
			// 
			// butTask
			// 
			this.butTask.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butTask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butTask.Autosize = true;
			this.butTask.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butTask.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butTask.CornerRadius = 4F;
			this.butTask.Location = new System.Drawing.Point(880, 535);
			this.butTask.Name = "butTask";
			this.butTask.Size = new System.Drawing.Size(92, 24);
			this.butTask.TabIndex = 124;
			this.butTask.Text = "To Task List";
			this.butTask.Click += new System.EventHandler(this.butTask_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(880, 587);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(92, 24);
			this.butDelete.TabIndex = 123;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butPin
			// 
			this.butPin.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butPin.Autosize = true;
			this.butPin.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPin.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPin.CornerRadius = 4F;
			this.butPin.Image = ((System.Drawing.Image)(resources.GetObject("butPin.Image")));
			this.butPin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPin.Location = new System.Drawing.Point(880, 561);
			this.butPin.Name = "butPin";
			this.butPin.Size = new System.Drawing.Size(92, 24);
			this.butPin.TabIndex = 122;
			this.butPin.Text = "&Pinboard";
			this.butPin.Click += new System.EventHandler(this.butPin_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(880, 640);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(92, 24);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(880, 666);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(92, 24);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butText
			// 
			this.butText.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butText.Autosize = true;
			this.butText.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butText.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butText.CornerRadius = 4F;
			this.butText.Image = global::OpenDental.Properties.Resources.Text;
			this.butText.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butText.Location = new System.Drawing.Point(880, 431);
			this.butText.Name = "butText";
			this.butText.Size = new System.Drawing.Size(92, 24);
			this.butText.TabIndex = 143;
			this.butText.Text = "Text";
			this.butText.Click += new System.EventHandler(this.butText_Click);
			// 
			// labelPlannedComplete
			// 
			this.labelPlannedComplete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelPlannedComplete.Location = new System.Drawing.Point(633, 1);
			this.labelPlannedComplete.Name = "labelPlannedComplete";
			this.labelPlannedComplete.Size = new System.Drawing.Size(305, 26);
			this.labelPlannedComplete.TabIndex = 185;
			this.labelPlannedComplete.Text = "This planned appointment is attached\r\nto a completed appointment.";
			this.labelPlannedComplete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.labelPlannedComplete.Visible = false;
			// 
			// FormApptEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(974, 698);
			this.Controls.Add(this.labelPlannedComplete);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.listQuickAdd);
			this.Controls.Add(this.labelQuickAdd);
			this.Controls.Add(this.butPDF);
			this.Controls.Add(this.gridFields);
			this.Controls.Add(this.butComplete);
			this.Controls.Add(this.butDeleteProc);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.labelApptNote);
			this.Controls.Add(this.butText);
			this.Controls.Add(this.butAddComm);
			this.Controls.Add(this.butSlider);
			this.Controls.Add(this.tbTime);
			this.Controls.Add(this.gridPatient);
			this.Controls.Add(this.gridComm);
			this.Controls.Add(this.gridProc);
			this.Controls.Add(this.butAudit);
			this.Controls.Add(this.butTask);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butPin);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormApptEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Appointment";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormApptEdit_FormClosing);
			this.Load += new System.EventHandler(this.FormApptEdit_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion

		private void FormApptEdit_Load(object sender, System.EventArgs e){
			tbTime.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbTime_CellClicked);
			_listAppointments=Appointments.GetListForPat(AptCur.PatNum);
			_listAppointmentsOld=_listAppointments.Select(x => x.Clone()).ToList();
			for(int i=0;i<_listAppointments.Count;i++) {
				if(_listAppointments[i].AptNum==AptCur.AptNum) {
					AptCur=_listAppointments[i];//Changing the variable pointer so all changes are done on the element in the list.
				}
			}
			AptOld=AptCur.Clone();
			if(IsNew){
				if(!Security.IsAuthorized(Permissions.AppointmentCreate)) { //Should have been checked before appointment was inserted into DB and this form was loaded.  Left here just in case.
					DialogResult=DialogResult.Cancel;
					return;
				}
			}
			else {
				//The order of the conditional matters; C# will not evaluate the second part of the conditional if it is not needed. 
				//Changing the order will cause unneeded Security MsgBoxes to pop up.
				if (AptCur.AptStatus!=ApptStatus.Complete && !Security.IsAuthorized(Permissions.AppointmentEdit)
					|| (AptCur.AptStatus==ApptStatus.Complete && !Security.IsAuthorized(Permissions.AppointmentCompleteEdit))) 
				{//completed apts have their own perm.
					butOK.Enabled=false;
					butDelete.Enabled=false;
					butPin.Enabled=false;
					butTask.Enabled=false;
					gridProc.Enabled=false;
					listQuickAdd.Enabled=false;
					butAdd.Enabled=false;
					butDeleteProc.Enabled=false;
					butInsPlan1.Enabled=false;
					butInsPlan2.Enabled=false;
					butComplete.Enabled=false;
				}
			}
			//The four objects below are needed when adding procs to this appt.
			_listProcCodes=ProcedureCodeC.GetListLong();
			fam=Patients.GetFamily(AptCur.PatNum);
			pat=fam.GetPatient(AptCur.PatNum);
			SubList=InsSubs.RefreshForFam(fam);
			PlanList=InsPlans.RefreshForSubList(SubList);
			_tableFields=Appointments.GetApptFields(AptCur.AptNum);
			_tableComms=Appointments.GetCommTable(AptCur.PatNum.ToString());
			_listProcs=Procedures.GetProcsForApptEdit(AptCur);
			_listProviders=ProviderC.ListLong;
			_listProcsFromDB=new List<Procedure>();
			for(int i=0;i<_listProcs.Count;i++) {
				_listProcsFromDB.Add(_listProcs[i].Copy());
			}
			_isPlanned=false;
			if(AptCur.AptStatus==ApptStatus.Planned) {
				_isPlanned=true;
			}
			_labCur=LabCases.GetForApt(AptCur);
			if(PrefC.GetBool(PrefName.EasyHideDentalSchools)) {
				butRequirement.Visible=false;
				textRequirement.Visible=false;
			}
			if(PrefC.GetBool(PrefName.ShowFeatureEhr)) {
				butSyndromicObservations.Visible=true;
				labelSyndromicObservations.Visible=true;
			}
			if(PrefC.GetBool(PrefName.EasyNoClinics)) {
				labelClinic.Visible=false;
				comboClinic.Visible=false;
			}
			if(!PinIsVisible){
				butPin.Visible=false;
			}
			if(_isPlanned) {
				Text=Lan.g(this,"Edit Planned Appointment")+" - "+pat.GetNameFL(); 
				labelStatus.Visible=false;
				comboStatus.Visible=false;
				butDelete.Visible=false;
				if(_listAppointments.FindAll(x => x.NextAptNum==AptCur.AptNum)//This planned appt is attached to a completed appt.
					.Exists(x => x.AptStatus==ApptStatus.Complete)) 
				{
					labelPlannedComplete.Visible=true;
				}
			}
			else if(AptCur.AptStatus==ApptStatus.PtNote) {
				labelApptNote.Text="Patient NOTE:";
				Text=Lan.g(this,"Edit Patient Note")+" - "+pat.GetNameFL()+" on "+AptCur.AptDateTime.DayOfWeek+", "+AptCur.AptDateTime;
				comboStatus.Items.Add(Lan.g("enumApptStatus","Patient Note"));
				comboStatus.Items.Add(Lan.g("enumApptStatus","Completed Pt. Note"));
				comboStatus.SelectedIndex=(int)AptCur.AptStatus-7;
				labelQuickAdd.Visible=false;
				labelStatus.Visible=false;
				gridProc.Visible=false;
				listQuickAdd.Visible=false;
				butAdd.Visible=false;
				butDeleteProc.Visible=false;
				//textNote.Width = 400;
			}
			else if(AptCur.AptStatus==ApptStatus.PtNoteCompleted) {
				labelApptNote.Text="Completed Patient NOTE:";
				Text=Lan.g(this,"Edit Completed Patient Note")+" - "+pat.GetNameFL()+" on "+AptCur.AptDateTime.DayOfWeek+", "+AptCur.AptDateTime;
				comboStatus.Items.Add(Lan.g("enumApptStatus","Patient Note"));
				comboStatus.Items.Add(Lan.g("enumApptStatus","Completed Pt. Note"));
				comboStatus.SelectedIndex=(int)AptCur.AptStatus-7;
				labelQuickAdd.Visible=false;
				labelStatus.Visible=false;
				gridProc.Visible=false;
				listQuickAdd.Visible=false;
				butAdd.Visible=false;
				butDeleteProc.Visible=false;
				//textNote.Width = 400;
			}
			else {
				Text=Lan.g(this, "Edit Appointment")+" - "+pat.GetNameFL()+" on "+AptCur.AptDateTime.DayOfWeek+", "+AptCur.AptDateTime;
				comboStatus.Items.Add(Lan.g("enumApptStatus","Scheduled"));
				comboStatus.Items.Add(Lan.g("enumApptStatus","Complete"));
				comboStatus.Items.Add(Lan.g("enumApptStatus","UnschedList"));
				comboStatus.Items.Add(Lan.g("enumApptStatus","ASAP"));
				comboStatus.Items.Add(Lan.g("enumApptStatus","Broken"));
				comboStatus.SelectedIndex=(int)AptCur.AptStatus-1;
			}
			if(AptCur.AptStatus==ApptStatus.UnschedList) {
				if(Programs.UsingEcwTightOrFullMode()) {
					comboStatus.Enabled=true;
				}
				else if(HL7Defs.GetOneDeepEnabled()!=null && !HL7Defs.GetOneDeepEnabled().ShowAppts) {
					comboStatus.Enabled=true;
				}
				else {
					comboStatus.Enabled=false;
				}
			}
			//convert time pattern from 5 to current increment.
			strBTime=new StringBuilder();
			for(int i=0;i<AptCur.Pattern.Length;i++) {
				strBTime.Append(AptCur.Pattern.Substring(i,1));
				if(PrefC.GetLong(PrefName.AppointmentTimeIncrement)==10) {
					i++;
				}
				if(PrefC.GetLong(PrefName.AppointmentTimeIncrement)==15) {
					i++;
					i++;
				}
			}
			comboUnschedStatus.Items.Add(Lan.g(this,"none"));
			comboUnschedStatus.SelectedIndex=0;
			//Consider making a local copy of DefC.Short[(int)DefCat.RecallUnschedStatus] because each call creates a deep copy of the cache . 
			//This is due to the new thread safe cache pattern implemented in 15.1
			for(int i=0;i<DefC.Short[(int)DefCat.RecallUnschedStatus].Length;i++) {
				comboUnschedStatus.Items.Add(DefC.Short[(int)DefCat.RecallUnschedStatus][i].ItemName);
				if(DefC.Short[(int)DefCat.RecallUnschedStatus][i].DefNum==AptCur.UnschedStatus)
					comboUnschedStatus.SelectedIndex=i+1;
			}
			for(int i=0;i<DefC.Short[(int)DefCat.ApptConfirmed].Length;i++) {
				comboConfirmed.Items.Add(DefC.Short[(int)DefCat.ApptConfirmed][i].ItemName);
				if(DefC.Short[(int)DefCat.ApptConfirmed][i].DefNum==AptCur.Confirmed) {
					comboConfirmed.SelectedIndex=i;
				}
			}
			checkTimeLocked.Checked=AptCur.TimeLocked;
			textNote.Text=AptCur.Note;
			for(int i=0;i<DefC.Short[(int)DefCat.ApptProcsQuickAdd].Length;i++) {
				listQuickAdd.Items.Add(DefC.Short[(int)DefCat.ApptProcsQuickAdd][i].ItemName);
			}
			comboClinic.Items.Add(Lan.g(this,"none"));
			comboClinic.SelectedIndex=0;
			for(int i=0;i<Clinics.List.Length;i++) {
				comboClinic.Items.Add(Clinics.List[i].Description);
				if(Clinics.List[i].ClinicNum==AptCur.ClinicNum)
					comboClinic.SelectedIndex=i+1;
			}
			if(IsNew) {
				//Try to auto-select a provider when in Orion mode. Only for new appointments so we don't change historical data.
				AptCur.ProvNum=Providers.GetOrionProvNum(AptCur.ProvNum);
			}
			for(int i=0;i<ProviderC.ListShort.Count;i++) {
				comboProvNum.Items.Add(ProviderC.ListShort[i].Abbr);
				if(ProviderC.ListShort[i].ProvNum==AptCur.ProvNum)
					comboProvNum.SelectedIndex=i;
			}
			comboProvHyg.Items.Add(Lan.g(this,"none"));
			comboProvHyg.SelectedIndex=0;
			for(int i=0;i<ProviderC.ListShort.Count;i++) {
				comboProvHyg.Items.Add(ProviderC.ListShort[i].Abbr);
				if(ProviderC.ListShort[i].ProvNum==AptCur.ProvHyg)
					comboProvHyg.SelectedIndex=i+1;
			}
			checkIsHygiene.Checked=AptCur.IsHygiene;
			comboAssistant.Items.Add(Lan.g(this,"none"));
			comboAssistant.SelectedIndex=0;
			for(int i=0;i<Employees.ListShort.Length;i++) {
				comboAssistant.Items.Add(Employees.ListShort[i].FName);
				if(Employees.ListShort[i].EmployeeNum==AptCur.Assistant)
					comboAssistant.SelectedIndex=i+1;
			}
			textLabCase.Text=GetLabCaseDescript();
			textTimeArrived.ContextMenu=contextMenuTimeArrived;
			textTimeSeated.ContextMenu=contextMenuTimeSeated;
			textTimeDismissed.ContextMenu=contextMenuTimeDismissed;
			if(AptCur.DateTimeAskedToArrive.TimeOfDay>TimeSpan.FromHours(0)) {
				textTimeAskedToArrive.Text=AptCur.DateTimeAskedToArrive.ToShortTimeString();
			}
			if(AptCur.DateTimeArrived.TimeOfDay>TimeSpan.FromHours(0)){
				textTimeArrived.Text=AptCur.DateTimeArrived.ToShortTimeString();
			}
			if(AptCur.DateTimeSeated.TimeOfDay>TimeSpan.FromHours(0)){
				textTimeSeated.Text=AptCur.DateTimeSeated.ToShortTimeString();
			}
			if(AptCur.DateTimeDismissed.TimeOfDay>TimeSpan.FromHours(0)){
				textTimeDismissed.Text=AptCur.DateTimeDismissed.ToShortTimeString();
			}
			if(AptCur.AptStatus==ApptStatus.Complete
				|| AptCur.AptStatus==ApptStatus.Broken
				|| AptCur.AptStatus==ApptStatus.PtNote
				|| AptCur.AptStatus==ApptStatus.PtNoteCompleted) 
			{
				textInsPlan1.Text=InsPlans.GetCarrierName(AptCur.InsPlan1,PlanList);
				textInsPlan2.Text=InsPlans.GetCarrierName(AptCur.InsPlan2,PlanList);
			}
			else {//Get the current ins plans for the patient.
				butInsPlan1.Enabled=false;
				butInsPlan2.Enabled=false;
				List<PatPlan> listPatPlans=PatPlans.Refresh(pat.PatNum);
				InsSub sub1=InsSubs.GetSub(PatPlans.GetInsSubNum(listPatPlans,PatPlans.GetOrdinal(PriSecMed.Primary,listPatPlans,PlanList,SubList)),SubList);
				InsSub sub2=InsSubs.GetSub(PatPlans.GetInsSubNum(listPatPlans,PatPlans.GetOrdinal(PriSecMed.Secondary,listPatPlans,PlanList,SubList)),SubList);
				AptCur.InsPlan1=sub1.PlanNum;
				AptCur.InsPlan2=sub2.PlanNum;
				textInsPlan1.Text=InsPlans.GetCarrierName(AptCur.InsPlan1,PlanList);
				textInsPlan2.Text=InsPlans.GetCarrierName(AptCur.InsPlan2,PlanList);
			}
			if(!PrefC.GetBool(PrefName.EasyHideDentalSchools)) {
				List<ReqStudent> listStudents=ReqStudents.GetForAppt(AptCur.AptNum);
				string requirements="";
				for(int i=0;i<listStudents.Count;i++) {
					if(i > 0) {
						requirements+="\r\n";
					}
					Provider student=Providers.GetProv(listStudents[i].ProvNum,_listProviders);
					requirements+=student.LName+", "+student.FName+": "+listStudents[i].Descript;
				}
				textRequirement.Text=requirements;
			}
			//IsNewPatient is set well before opening this form.
			checkIsNewPatient.Checked=AptCur.IsNewPatient;
			butColor.BackColor=AptCur.ColorOverride;
			if(ApptDrawing.MinPerIncr==5) {
				tbTime.TopBorder[0,12]=System.Drawing.Color.Black;
				tbTime.TopBorder[0,24]=System.Drawing.Color.Black;
				tbTime.TopBorder[0,36]=System.Drawing.Color.Black;
			}
			else if(ApptDrawing.MinPerIncr==10) {
				tbTime.TopBorder[0,6]=System.Drawing.Color.Black;
				tbTime.TopBorder[0,12]=System.Drawing.Color.Black;
				tbTime.TopBorder[0,18]=System.Drawing.Color.Black;
				tbTime.TopBorder[0,24]=System.Drawing.Color.Black;
				tbTime.TopBorder[0,30]=System.Drawing.Color.Black;
				tbTime.TopBorder[0,36]=System.Drawing.Color.Black;
			}
			else if(ApptDrawing.MinPerIncr==15){
				tbTime.TopBorder[0,4]=System.Drawing.Color.Black;
				tbTime.TopBorder[0,8]=System.Drawing.Color.Black;
				tbTime.TopBorder[0,12]=System.Drawing.Color.Black;
				tbTime.TopBorder[0,16]=System.Drawing.Color.Black;
				tbTime.TopBorder[0,20]=System.Drawing.Color.Black;
				tbTime.TopBorder[0,24]=System.Drawing.Color.Black;
				tbTime.TopBorder[0,28]=System.Drawing.Color.Black;
				tbTime.TopBorder[0,32]=System.Drawing.Color.Black;
				tbTime.TopBorder[0,36]=System.Drawing.Color.Black;
			}
			if(Programs.UsingEcwTightOrFullMode()) {
				//These buttons are ONLY for eCW, not any other HL7 interface.
				butComplete.Visible=true;
				butPDF.Visible=true;
				//for eCW, we need to hide some things--------------------
				if(Bridges.ECW.AptNum==AptCur.AptNum) {
					butDelete.Visible=false;
				}
				butPin.Visible=false;
				butTask.Visible=false;
				butAddComm.Visible=false;
				if(HL7Msgs.MessageWasSent(AptCur.AptNum)) {
					butComplete.Text="Revise";
					//if(!Security.IsAuthorized(Permissions.Setup,true)) {
					//	butComplete.Enabled=false;
					//	butPDF.Enabled=false;
					//}
					butOK.Enabled=false;
					gridProc.Enabled=false;
					listQuickAdd.Enabled=false;
					butAdd.Enabled=false;
					butDeleteProc.Enabled=false;
				}
				else {//hl7 was not sent for this appt
					butComplete.Text="Finish && Send";
					if(Bridges.ECW.AptNum != AptCur.AptNum) {
						butComplete.Enabled=false;
					}
					butPDF.Enabled=false;
				}
			}
			else {
				butComplete.Visible=false;
				butPDF.Visible=false;
			}
			//Hide text message button sometimes
			if(pat.WirelessPhone=="" || (!Programs.IsEnabled(ProgramName.CallFire) && !SmsPhones.IsIntegratedTextingEnabled())) {
				butText.Enabled=false;
			}
			else {//Pat has a wireless phone number and CallFire is enabled
				butText.Enabled=true;//TxtMsgOk checking performed on button click.
			}
			//AppointmentType
			_listAppointmentType=new List<AppointmentType>();
			comboApptType.Items.Add("none");
			comboApptType.SelectedIndex=0;
			for(int i=0;i<AppointmentTypes.Listt.Count;i++) {
				if(AppointmentTypes.Listt[i].IsHidden
					&& AppointmentTypes.Listt[i].AppointmentTypeNum!=AptCur.AppointmentTypeNum){
					continue;
				}
				_listAppointmentType.Add(AppointmentTypes.Listt[i]);
				comboApptType.Items.Add(AppointmentTypes.Listt[i].AppointmentTypeName);
				if(AppointmentTypes.Listt[i].AppointmentTypeNum==AptCur.AppointmentTypeNum) {
					comboApptType.SelectedIndex=_listAppointmentType.Count;//-1 for 0 index, +1 for adding none to list.
				}
			}
			HasProcsChangedAndCancel=false;
			FillProcedures();
			SetProceduresForECW();
			FillPatient();//Must be after FillProcedures(), so that the initial amount for the appointment can be calculated.
			FillTime();
			FillComm();
			FillFields();
			textNote.Focus();
			textNote.SelectionStart = 0;
			#if DEBUG
				Text="AptNum"+AptCur.AptNum;
			#endif
			Plugins.HookAddCode(this,"FormApptEdit.Load_End",pat,butText);
			Plugins.HookAddCode(this,"FormApptEdit.Load_end2",AptCur);//Lower casing the code area (_end) is the newer pattern for this.
		}

		///<summary>If an eCW program link is turned on, then this attaches completed procs with the same date as the appt.</summary>
		private void SetProceduresForECW() {
			if(!Programs.UsingEcwTightOrFullMode()) {
			  return;
			}
			List<Procedure> listProcs=new List<Procedure>();
			//this is a method that attaches very specific kinds of procedures to appt
			for(int i=0;i<_listProcs.Count;i++) {//loop through procs
				if(_listProcs[i].ProcStatus != ProcStat.C) {//must be complete proc
					continue;
				}
				if(_listProcs[i].ProcDate.Date != AptCur.AptDateTime.Date) {//must have same date as appt
					continue;
				}
				gridProc.SetSelected(i,true);//harmless if already selected.
				listProcs.Add(_listProcs[i]);
			}
			//Now attach the procedures to the appt in the database.
			for(int i=0;i<listProcs.Count;i++) {
				Procedure proc=listProcs[i];
				UpdateOtherApptDesc(proc);
			}
		}

		private void butPickDentist_Click(object sender,EventArgs e) {
			FormProviderPick formp=new FormProviderPick();
			if(comboProvNum.SelectedIndex>-1) {
				formp.SelectedProvNum=ProviderC.ListShort[comboProvNum.SelectedIndex].ProvNum;
			}
			formp.ShowDialog();
			if(formp.DialogResult!=DialogResult.OK) {
				return;
			}
			comboProvNum.SelectedIndex=Providers.GetIndex(formp.SelectedProvNum);
		}

		private void butPickHyg_Click(object sender,EventArgs e) {
			FormProviderPick formp=new FormProviderPick();
			if(comboProvHyg.SelectedIndex>0) {
				formp.SelectedProvNum=ProviderC.ListShort[comboProvHyg.SelectedIndex-1].ProvNum;
			}
			formp.ShowDialog();
			if(formp.DialogResult!=DialogResult.OK) {
				return;
			}
			comboProvHyg.SelectedIndex=Providers.GetIndex(formp.SelectedProvNum)+1;
		}

		private void butColor_Click(object sender,EventArgs e) {
			ColorDialog colorDialog1=new ColorDialog();
			colorDialog1.Color=butColor.BackColor;
			colorDialog1.ShowDialog();
			butColor.BackColor=colorDialog1.Color;
		}

		private void butColorClear_Click(object sender,EventArgs e) {
			butColor.BackColor=System.Drawing.Color.FromArgb(0);
		}

		private void FillPatient(){
			DataTable table=Appointments.GetPatTable(AptCur.PatNum.ToString());
			gridPatient.BeginUpdate();
			gridPatient.Columns.Clear();
			ODGridColumn col=new ODGridColumn("",120);//Add 2 blank columns
			gridPatient.Columns.Add(col);
			col=new ODGridColumn("",120);
			gridPatient.Columns.Add(col);
			gridPatient.Rows.Clear();
			ODGridRow row;
			for(int i=1;i<table.Rows.Count;i++) {//starts with 1 to skip name
				row=new ODGridRow();
				row.Cells.Add(table.Rows[i]["field"].ToString());
				row.Cells.Add(table.Rows[i]["value"].ToString());
				if(table.Rows[i]["field"].ToString().EndsWith("Phone")  && Programs.GetCur(ProgramName.DentalTekSmartOfficePhone).Enabled) {
					row.Cells[row.Cells.Count-1].ColorText=System.Drawing.Color.Blue;
					row.Cells[row.Cells.Count-1].Underline=YN.Yes;
				}
				gridPatient.Rows.Add(row);
			}
			//Add a UI managed row to display the total fee for the selected procedures in this appointment.
			row=new ODGridRow();
			row.Cells.Add(Lan.g(this,"Fee This Appt"));
			row.Cells.Add("");//Calculated below
			gridPatient.Rows.Add(row);
			CalcPatientFeeThisAppt();
			gridPatient.EndUpdate();
			gridPatient.ScrollToEnd();
		}

		///<summary>Calculates the fee for this appointment using the highlighted procedures in the procedure list.</summary>
		private void CalcPatientFeeThisAppt() {
			double feeThisAppt=0;
			for(int i=0;i<gridProc.SelectedIndices.Length;i++) {
				if(Clinics.IsMedicalPracticeOrClinic(FormOpenDental.ClinicNum)) {
					feeThisAppt+=PIn.Double(gridProc.Rows[gridProc.SelectedIndices[i]].Cells[4].Text);
				}
				else {
					feeThisAppt+=PIn.Double(gridProc.Rows[gridProc.SelectedIndices[i]].Cells[6].Text);
				}
			}
				
			gridPatient.Rows[gridPatient.Rows.Count-1].Cells[1].Text=POut.Double(feeThisAppt);
			gridPatient.Invalidate();
		}

		private void FillFields() {
			gridFields.BeginUpdate();
			gridFields.Columns.Clear();
			ODGridColumn col=new ODGridColumn("",100);
			gridFields.Columns.Add(col);
			col=new ODGridColumn("",100);
			gridFields.Columns.Add(col);
			gridFields.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<_tableFields.Rows.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(_tableFields.Rows[i]["FieldName"].ToString());
				row.Cells.Add(_tableFields.Rows[i]["FieldValue"].ToString());
				gridFields.Rows.Add(row);
			}
			gridFields.EndUpdate();
		}

		private void FillComm(){
			gridComm.BeginUpdate();
			gridComm.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableCommLog","DateTime"),80);
			gridComm.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableCommLog","Description"),80);
			gridComm.Columns.Add(col);
			gridComm.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<_tableComms.Rows.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(_tableComms.Rows[i]["commDateTime"].ToString());
				row.Cells.Add(_tableComms.Rows[i]["Note"].ToString());
				if(_tableComms.Rows[i]["CommType"].ToString()==Commlogs.GetTypeAuto(CommItemTypeAuto.APPT).ToString()){
					row.ColorBackG=DefC.Long[(int)DefCat.MiscColors][7].ItemColor;
				}
				gridComm.Rows.Add(row);
			}
			gridComm.EndUpdate();
			gridComm.ScrollToEnd();
		}

		private void gridComm_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			Commlog item=Commlogs.GetOne(PIn.Long(_tableComms.Rows[e.Row]["CommlogNum"].ToString()));
			FormCommItem FormCI=new FormCommItem(item);
			FormCI.ShowDialog();
			_tableComms=Appointments.GetCommTable(AptCur.PatNum.ToString());
			FillComm();
		}

		private void FillProcedures(){
			gridProc.BeginUpdate();
			gridProc.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableApptProcs","Stat"),35);
			gridProc.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableApptProcs","Priority"),45);
			gridProc.Columns.Add(col);
			if(Clinics.IsMedicalPracticeOrClinic(FormOpenDental.ClinicNum)) {
				col=new ODGridColumn(Lan.g("TableApptProcs","Code"),125);
				gridProc.Columns.Add(col);
			}
			else {
				col=new ODGridColumn(Lan.g("TableApptProcs","Tth"),25);
				gridProc.Columns.Add(col);
				col=new ODGridColumn(Lan.g("TableApptProcs","Surf"),50);
				gridProc.Columns.Add(col);
				col=new ODGridColumn(Lan.g("TableApptProcs","Code"),50);
				gridProc.Columns.Add(col);
			}
			col=new ODGridColumn(Lan.g("TableApptProcs","Description"),275);
			gridProc.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableApptProcs","Fee"),60,HorizontalAlignment.Right);
			gridProc.Columns.Add(col);
			gridProc.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<_listProcs.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(_listProcs[i].ProcStatus.ToString());
				row.Cells.Add(DefC.GetName(DefCat.TxPriorities,_listProcs[i].Priority));
				if(!Clinics.IsMedicalPracticeOrClinic(FormOpenDental.ClinicNum)) {
					row.Cells.Add(Tooth.GetToothLabel(_listProcs[i].ToothNum));
					row.Cells.Add(_listProcs[i].Surf);
				}
				ProcedureCode procCode=ProcedureCodes.GetProcCode(_listProcCodes,_listProcs[i].CodeNum);
				row.Cells.Add(procCode.ProcCode);
				string descript="";
				//This descript is gotten the same way it was in Appointments.GetProcTable()
				if(_isPlanned && _listProcs[i].PlannedAptNum!=0 && _listProcs[i].PlannedAptNum!=AptCur.AptNum) {
					descript+=Lan.g(this,"(other appt) ");
				}
				else if(!_isPlanned && _listProcs[i].AptNum!=0 && _listProcs[i].AptNum!=AptCur.AptNum){
					descript+=Lan.g(this,"(other appt) ");
				}
				if(procCode.LaymanTerm=="") {
					descript+=procCode.Descript;
				}
				else {
					descript+=procCode.LaymanTerm;
				}
				if(_listProcs[i].ToothRange!="") {
					descript+=" #"+Tooth.FormatRangeForDisplay(_listProcs[i].ToothRange);
				}
				row.Cells.Add(descript);
				row.Cells.Add(_listProcs[i].ProcFee.ToString("F"));
				row.Tag=_listProcs[i];
				gridProc.Rows.Add(row);
			}
			gridProc.EndUpdate();
			for(int i=0;i<_listProcs.Count;i++){
				if(_isPlanned && _listProcs[i].PlannedAptNum==AptCur.AptNum) {
					gridProc.SetSelected(i,true);
				}
				else if(!_isPlanned && _listProcs[i].AptNum==AptCur.AptNum) {
					gridProc.SetSelected(i,true);
				}
			}
		}

		private string GetLabCaseDescript() {
			string descript="";
			if(_labCur!=null) {
				descript=Laboratories.GetOne(_labCur.LaboratoryNum).Description;
				if(_labCur.DateTimeChecked.Year>1880) {//Logic from Appointments.cs lines 1818 to 1840
					descript+=", "+Lan.g(this,"Quality Checked");
				}
				else {
					if(_labCur.DateTimeRecd.Year>1880) {
						descript+=", "+Lan.g(this,"Received");
					}
					else {
						if(_labCur.DateTimeSent.Year>1880) {
							descript+=", "+Lan.g(this,"Sent");
						}
						else {
							descript+=", "+Lan.g(this,"Not Sent");
						}
						if(_labCur.DateTimeDue.Year>1880) {
							descript+=", "+Lan.g(this,"Due: ")+_labCur.DateTimeDue.ToString("ddd")+" "
								+_labCur.DateTimeDue.ToShortDateString()+" "
								+_labCur.DateTimeDue.ToShortTimeString();
						}
					}
				}
			}
			return descript;
		}

		private void butAddComm_Click(object sender,EventArgs e) {
			Commlog CommlogCur=new Commlog();
			CommlogCur.PatNum=AptCur.PatNum;
			CommlogCur.CommDateTime=DateTime.Now;
			CommlogCur.CommType=Commlogs.GetTypeAuto(CommItemTypeAuto.APPT);
			CommlogCur.UserNum=Security.CurUser.UserNum;
			FormCommItem FormCI=new FormCommItem(CommlogCur);
			FormCI.IsNew=true;
			FormCI.ShowDialog();
			_tableComms=Appointments.GetCommTable(AptCur.PatNum.ToString());	
			FillComm();
		}

		private void butText_Click(object sender,EventArgs e) {
			if(Plugins.HookMethod(this,"FormApptEdit.butText_Click_start",pat,AptCur,this)) {
				return;
			}
			bool updateTextYN=false;
			if(pat.TxtMsgOk==YN.No) {
				if(MsgBox.Show(this,MsgBoxButtons.YesNo,"This patient is marked to not receive text messages. "
					+"Would you like to mark this patient as okay to receive text messages?")) {
					updateTextYN=true;
				}
				else {
					return;
				}
			}
			if(pat.TxtMsgOk==YN.Unknown && PrefC.GetBool(PrefName.TextMsgOkStatusTreatAsNo)) {
				if(MsgBox.Show(this,MsgBoxButtons.YesNo,"This patient might not want to receive text messages. "
					+"Would you like to mark this patient as okay to receive text messages?")) {
					updateTextYN=true;
				}
				else {
					return;
				}
			}
			if(updateTextYN) {
				Patient patOld=pat.Copy();
				pat.TxtMsgOk=YN.Yes;
				Patients.Update(pat,patOld);
			}
			string message;
			message=PrefC.GetString(PrefName.ConfirmTextMessage);
			message=message.Replace("[NameF]",pat.GetNameFirst());
			message=message.Replace("[NameFL]",pat.GetNameFL());
			message=message.Replace("[date]",AptCur.AptDateTime.ToShortDateString());
			message=message.Replace("[time]",AptCur.AptDateTime.ToShortTimeString());
			FormTxtMsgEdit FormTME=new FormTxtMsgEdit();
			FormTME.PatNum=pat.PatNum;
			FormTME.WirelessPhone=pat.WirelessPhone;
			FormTME.Message=message;
			FormTME.TxtMsgOk=pat.TxtMsgOk;
			FormTME.ShowDialog();
		}

		private void gridProc_CellClick(object sender,ODGridClickEventArgs e) {
			bool isSelected=false;
			for(int i=0;i<gridProc.SelectedIndices.Length;i++){
				if(gridProc.SelectedIndices[i]==e.Row){
					isSelected=true;
				}
			}
			Procedure proc=_listProcs[e.Row];
			if(isSelected && _isPlanned) {//Detatching from this planned appointment.
				proc.PlannedAptNum=0;
			}
			else if(isSelected && !_isPlanned) {//Detatching from this appointment.
				proc.AptNum=0;
			}
			else if(!isSelected && _isPlanned) {//Attaching to this planned appointment.
				if(proc.PlannedAptNum != 0 && proc.PlannedAptNum != AptCur.AptNum) {
					UpdateOtherApptDesc(proc);
				}
				else {
					proc.PlannedAptNum=AptCur.AptNum;
				}
			}
			else if(!isSelected && !_isPlanned) {//Attaching to this appointment.
				if(proc.AptNum != 0 && proc.AptNum != AptCur.AptNum) {
					UpdateOtherApptDesc(proc);	
				}
				else {//Proc's AptNum==0, we already know it's not on this appointment here.
					proc.AptNum=AptCur.AptNum;
				}
			}
			FillProcedures();
			CalculateTime();
			FillTime();
			CalcPatientFeeThisAppt();
		}

		private void gridProc_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			Procedure proc=_listProcs[e.Row].Copy();//Make a copy so if the user cancels we don't reflect changes.
			FormProcEdit FormP=new FormProcEdit(proc,pat,fam);
			FormP.ShowDialog();
			if(FormP.DialogResult!=DialogResult.OK){
				return;
			}
			//we can go to the db for this proc because FormProcEdit saves changes before returning to this form
			if(Procedures.GetOneProc(proc.ProcNum,true).ProcStatus==ProcStat.D) {//User deleted the procedure.
				_listProcsMoved.Remove(_listProcs[e.Row]);
				_listProcs.RemoveAt(e.Row);
			}
			else {//User did not delete the procedure.
				_listProcs[e.Row]=proc;//Replace the proc in the mem list with the edited proc.
			}
			_listProcs.Sort(ProcedureLogic.CompareProcedures);
			FillProcedures();
			CalculateTime();
			FillTime();
		}

		///<summary>Updates the proc description of the both the old appointment this appointment.</summary>
		private void UpdateOtherApptDesc(Procedure proc) {
			string procCodeDescript=ProcedureCodes.GetProcCode(_listProcCodes,proc.CodeNum).AbbrDesc;
			Appointment appt;
			if(_isPlanned) {
				appt=_listAppointments.FirstOrDefault(x => x.AptNum==proc.PlannedAptNum);
				proc.PlannedAptNum=AptCur.AptNum;
			}
			else {
				appt=_listAppointments.FirstOrDefault(x => x.AptNum==proc.AptNum);
				proc.AptNum=AptCur.AptNum;
			}
			if(appt!=null) {
				SetProcDescript(appt);
				_listProcsMoved.Add(proc);
			}
		}

		private void butDeleteProc_Click(object sender,EventArgs e) {
			//this button will not be enabled if user does not have permission for AppointmentEdit
			if(gridProc.SelectedIndices.Length==0){
				MsgBox.Show(this,"Please select one or more procedures first.");
				return;
			}
			if(!MsgBox.Show(this,true,"Permanently delete all selected procedure(s)?")){
				return;
			}
			int skipped=0;
			int skippedSecurity=0;
			try{
				for(int i=gridProc.SelectedIndices.Length-1;i>=0;i--) {
					Procedure proc=_listProcs[gridProc.SelectedIndices[i]];
					if(!Security.IsAuthorized(Permissions.ProcComplEdit,proc.DateEntryC,true)) {
						if(proc.ProcStatus==ProcStat.C) {
							skipped++;
							continue;
						}
					}
					if(proc.ProcStatus!=ProcStat.C && !Security.IsAuthorized(Permissions.ProcDelete,proc.DateEntryC,true)) {
						skippedSecurity++;
						continue;
					}
					if(proc.ProcStatus==ProcStat.C && !Security.IsAuthorized(Permissions.ProcComplEdit,proc.DateEntryC,true)) {
						skippedSecurity++;
						continue;
					}
					Procedures.ValidateDelete(proc.ProcNum);
					_listProcsMoved.Remove(proc);
					_listProcs.Remove(proc);//Actual deletion will be done in the Sync at close.
					if(proc.ProcStatus==ProcStat.C) {
						SecurityLogs.MakeLogEntry(Permissions.ProcComplEdit,AptCur.PatNum,ProcedureCodes.GetProcCode(_listProcCodes,proc.CodeNum)
							+", "+proc.ProcFee.ToString("c")+", Deleted");
					}
					else {
						SecurityLogs.MakeLogEntry(Permissions.ProcDelete,AptCur.PatNum,ProcedureCodes.GetProcCode(_listProcCodes,proc.CodeNum)
							+", "+proc.ProcFee.ToString("c"));
					}
				}
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);
			}
			FillProcedures();
			CalculateTime();
			FillTime();
			if(skipped>0) {
				MessageBox.Show(Lan.g(this,"Procedures skipped due to lack of permission to edit completed procedures: ")+skipped.ToString());
			}
			if(skippedSecurity>0) {
				MessageBox.Show(Lan.g(this,"Procedures skipped due to lack of permission to delete procedures: ")+skippedSecurity.ToString());
			}
		}

		private void butAdd_Click(object sender,EventArgs e) {
			if(comboProvNum.SelectedIndex==-1) {
				MsgBox.Show(this,"Please select a provider.");
				return;
			}
			FormProcCodes FormP=new FormProcCodes();
			FormP.IsSelectionMode=true;
			FormP.ShowDialog();
			if(FormP.DialogResult!=DialogResult.OK) {
				return;
			}
			Procedure proc;
			proc=new Procedure();//going to be an insert, so no need to set Procedures.CurOld
			proc.CodeNum=FormP.SelectedCodeNum;
			proc.PatNum=AptCur.PatNum;
			proc.ProcDate=DateTimeOD.Today;
			proc.DateTP=proc.ProcDate;
			proc.ToothRange="";
			InsPlan primaryPlan=null;
			InsSub primarySub=null;
			List<PatPlan> listPatPlans=PatPlans.Refresh(pat.PatNum);
			if(listPatPlans.Count>0) {
				primarySub=InsSubs.GetSub(listPatPlans[0].InsSubNum,SubList);
				primaryPlan=InsPlans.GetPlan(primarySub.PlanNum,PlanList);
			}
			//Check if it's a medical procedure.
			double insfee;
			bool isMed=false;
			proc.MedicalCode=ProcedureCodes.GetProcCode(_listProcCodes,proc.CodeNum).MedicalCode;
			if(proc.MedicalCode!=null && proc.MedicalCode!="") {
				isMed=true;
			}
			//Get fee schedule for medical or dental.
			long feeSch;
			if(isMed) {
				feeSch=Fees.GetMedFeeSched(pat,PlanList,listPatPlans,SubList);
			}
			else {
				feeSch=Fees.GetFeeSched(pat,PlanList,listPatPlans,SubList);
			}
			//surf
			proc.Priority=0;
			proc.ProcStatus=ProcStat.TP;
			long aptProvNum=ProviderC.ListShort[0].ProvNum;
			if(comboProvNum.SelectedIndex!=-1) {
				aptProvNum=ProviderC.ListShort[comboProvNum.SelectedIndex].ProvNum;
			}
			long aptProvHyg=0;
			if(comboProvHyg.SelectedIndex>0) {
				aptProvHyg=ProviderC.ListShort[comboProvHyg.SelectedIndex-1].ProvNum;
			}
			if(ProcedureCodes.GetProcCode(_listProcCodes,proc.CodeNum).IsHygiene && aptProvHyg!=0) {
				proc.ProvNum=aptProvHyg;
			}
			else {
				proc.ProvNum=aptProvNum;
			}
			if(ProcedureCodes.GetProcCode(_listProcCodes,proc.CodeNum).ProvNumDefault!=0) {//Override provider for procedures with a default provider
				proc.ProvNum=ProcedureCodes.GetProcCode(_listProcCodes,proc.CodeNum).ProvNumDefault;
			}
			proc.ClinicNum=AptCur.ClinicNum;
			//Get the fee amount for medical or dental.
			if(PrefC.GetBool(PrefName.MedicalFeeUsedForNewProcs) && isMed) {
				insfee=Fees.GetAmount0(ProcedureCodes.GetProcCode(_listProcCodes,proc.CodeNum).CodeNum,feeSch,proc.ClinicNum,proc.ProvNum);
			}
			else {
				insfee=Fees.GetAmount0(proc.CodeNum,feeSch,proc.ClinicNum,proc.ProvNum);
			}
			if(primaryPlan!=null && primaryPlan.PlanType=="p") {//PPO
				double standardfee=Fees.GetAmount0(proc.CodeNum,Providers.GetProv(Patients.GetProvNum(pat),_listProviders).FeeSched,proc.ClinicNum,proc.ProvNum);
				if(standardfee>insfee) {
					proc.ProcFee=standardfee;
				}
				else {
					proc.ProcFee=insfee;
				}
			}
			else {
				proc.ProcFee=insfee;
			}
			proc.Note="";
			//dx
			//nextaptnum
			proc.DateEntryC=DateTime.Now;
			proc.BaseUnits=ProcedureCodes.GetProcCode(_listProcCodes,proc.CodeNum).BaseUnits;
			proc.SiteNum=pat.SiteNum;
			proc.RevCode=ProcedureCodes.GetProcCode(_listProcCodes,proc.CodeNum).RevenueCodeDefault;
			proc.DiagnosticCode=PrefC.GetString(PrefName.ICD9DefaultForNewProcs);
			if(Userods.IsUserCpoe(Security.CurUser)) {
				//This procedure is considered CPOE because the provider is the one that has added it.
				proc.IsCpoe=true;
			}
			Procedures.Insert(proc);
			List<Benefit> benefitList=Benefits.Refresh(listPatPlans,SubList);
			Procedures.ComputeEstimates(proc,pat.PatNum,new List<ClaimProc>(),true,PlanList,listPatPlans,benefitList,pat.Age,SubList);
			FormProcEdit FormPE=new FormProcEdit(proc,pat.Copy(),fam);
			FormPE.IsNew=true;
			if(Programs.UsingOrion) {
				FormPE.OrionProvNum=ProviderC.ListShort[comboProvNum.SelectedIndex].ProvNum;
				FormPE.OrionDentist=true;
			}
			FormPE.ShowDialog();
			if(FormPE.DialogResult==DialogResult.Cancel) {
				//any created claimprocs are automatically deleted from within procEdit window.
				try {
					Procedures.Delete(proc.ProcNum);//also deletes the claimprocs
				}
				catch(Exception ex) {
					MessageBox.Show(ex.Message);
				}
				return;
			}
			_listProcs.Add(proc);//In Appointments.cs there is no ORDER BY clause for getting the appointments...  We may want to think about ordering the list.
			if(_isPlanned) {
				proc.PlannedAptNum=AptCur.AptNum;
			}
			else {
				proc.AptNum=AptCur.AptNum;
			}
			_listProcs.Sort(ProcedureLogic.CompareProcedures);
			FillProcedures();
			CalculateTime();
			FillTime();
		}

		private void butQuickAdd_Click(object sender,EventArgs e) {
			/*
			if(AptCur.AptStatus==ApptStatus.Complete) {
				//added procedures would be marked complete when form closes. We'll just stop it here.
				if(!Security.IsAuthorized(Permissions.ProcComplCreate)) {
					return;
				}
			}
			FormApptQuickAdd formAq=new FormApptQuickAdd();
			formAq.ParentFormLocation=this.Location;
			formAq.ShowDialog();
			if(formAq.DialogResult!=DialogResult.OK) {
				return;
			}
			Procedures.SetDateFirstVisit(AptCur.AptDateTime.Date,1,pat);
			List<PatPlan> PatPlanList=PatPlans.Refresh(AptCur.PatNum);
			List<Benefit> benefitList=Benefits.Refresh(PatPlanList);
			List<ClaimProc> ClaimProcList=ClaimProcs.Refresh(AptCur.PatNum);
			List<long> selectedProcNums=new List<long>();//start with the originally selected list, then add the new ones.
			for(int i=0;i<gridProc.SelectedIndices.Length;i++) {
				selectedProcNums.Add(PIn.Long(DS.Tables["Procedure"].Rows[gridProc.SelectedIndices[i]]["ProcNum"].ToString()));
			}
			for(int i=0;i<formAq.SelectedCodeNums.Count;i++) {
				Procedure ProcCur=new Procedure();
				ProcCur.PatNum=AptCur.PatNum;
				if(AptCur.AptStatus!=ApptStatus.Planned) {
					ProcCur.AptNum=AptCur.AptNum;
				}
				ProcCur.CodeNum=formAq.SelectedCodeNums[i];
				ProcCur.ProcDate=AptCur.AptDateTime.Date;
				ProcCur.DateTP=AptCur.AptDateTime.Date;
				InsPlan priplan=null;
				if(PatPlanList.Count>0) {
					priplan=InsPlans.GetPlan(PatPlanList[0].PlanNum,PlanList);
				}
				double insfee=Fees.GetAmount0(ProcCur.CodeNum,Fees.GetFeeSched(pat,PlanList,PatPlanList));
				if(priplan!=null && priplan.PlanType=="p") {//PPO
					double standardfee=Fees.GetAmount0(ProcCur.CodeNum,Providers.GetProv(Patients.GetProvNum(pat)).FeeSched);
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
				//surf
				//toothnum
				//toothrange
				//priority
				ProcCur.ProcStatus=ProcStat.TP;
				//procnote
				ProcCur.ProvNum=AptCur.ProvNum;
				//Dx
				ProcCur.ClinicNum=AptCur.ClinicNum;
				ProcCur.SiteNum=pat.SiteNum;
				if(AptCur.AptStatus==ApptStatus.Planned) {
					ProcCur.PlannedAptNum=AptCur.AptNum;
				}
				ProcCur.MedicalCode=ProcedureCodes.GetProcCode(ProcCur.CodeNum).MedicalCode;
				ProcCur.BaseUnits=ProcedureCodes.GetProcCode(ProcCur.CodeNum).BaseUnits;
				Procedures.Insert(ProcCur);//recall synch not required
				selectedProcNums.Add(ProcCur.ProcNum);
				Procedures.ComputeEstimates(ProcCur,pat.PatNum,ClaimProcList,false,PlanList,PatPlanList,benefitList,pat.Age);
			}
			//listQuickAdd.SelectedIndex=-1;
			DS.Tables.Remove("Procedure");
			DS.Tables.Add(Appointments.GetApptEdit(AptCur.AptNum).Tables["Procedure"].Copy());
			FillProcedures();
			for(int i=0;i<gridProc.Rows.Count;i++) {
				for(int j=0;j<selectedProcNums.Count;j++) {
					if(selectedProcNums[j].ToString()==DS.Tables["Procedure"].Rows[i]["ProcNum"].ToString()) {
						gridProc.SetSelected(i,true);
					}
				}
			}
			CalculateTime();
			FillTime();
			CalcPatientFeeThisAppt();*/
		}

		private void FillTime() {
			System.Drawing.Color provColor=System.Drawing.Color.Gray;
			if(comboProvNum.SelectedIndex!=-1) {
				provColor=ProviderC.ListShort[comboProvNum.SelectedIndex].ProvColor;
			}
			if(strBTime.Length > tbTime.MaxRows) {
				strBTime.Remove(tbTime.MaxRows-1,strBTime.Length-tbTime.MaxRows+1);//example: Remove(40-1,78-40+1), start at 39, remove 39.
				MsgBox.Show(this,"Appointment time shortened.  10 and 15 minute increments allow longer appointments than 5 minute increments.");
			}
			for(int i=0;i<strBTime.Length;i++) {
				if(strBTime.ToString(i,1)=="X") {
					tbTime.BackGColor[0,i]=provColor;
					//.Cell[0,i]=strBTime.ToString(i,1);
				}
				else {
					tbTime.BackGColor[0,i]=System.Drawing.Color.White;
				}
			}
			for(int i=strBTime.Length;i<tbTime.MaxRows;i++) {
				//tbTime.Cell[0,i]="";
				tbTime.BackGColor[0,i]=System.Drawing.Color.FromName("Control");
			}
			tbTime.Refresh();
			butSlider.Location=new Point(tbTime.Location.X+2,(tbTime.Location.Y+strBTime.Length*14+1));
			textTime.Text=(strBTime.Length*ApptDrawing.MinPerIncr).ToString();
		}

		private void CalculateTime() {
			if(checkTimeLocked.Checked){
				return;
			}
			//We are using the providers selected for the appt rather than the providers for the procs.
			//Providers for the procs get reset when closing this form.
			long provDent=Patients.GetProvNum(pat);
			long provHyg=Patients.GetProvNum(pat);
			if(comboProvNum.SelectedIndex!=-1){
				provDent=ProviderC.ListShort[comboProvNum.SelectedIndex].ProvNum;
				provHyg=ProviderC.ListShort[comboProvNum.SelectedIndex].ProvNum;
			}
			if(comboProvHyg.SelectedIndex!=0) {
				provHyg=ProviderC.ListShort[comboProvHyg.SelectedIndex-1].ProvNum;
			}
			List<long> codeNums=new List<long>();
			for(int i=0;i<gridProc.SelectedIndices.Length;i++) {
				codeNums.Add(_listProcs[gridProc.SelectedIndices[i]].CodeNum);
			}
			strBTime=new StringBuilder(Appointments.CalculatePattern(provDent,provHyg,codeNums,false));
			//Plugins.HookAddCode(this,"FormApptEdit.CalculateTime_end",strBTime,provDent,provHyg,codeNums);//set strBTime, but without using the 'new' keyword.--Hook removed.
		}

		private void checkTimeLocked_Click(object sender,EventArgs e) {
			CalculateTime();
			FillTime();
		}

		private void tbTime_CellClicked(object sender,CellEventArgs e) {
			if(e.Row<strBTime.Length) {
				if(strBTime[e.Row]=='/') {
					strBTime.Replace('/','X',e.Row,1);
				}
				else {
					strBTime.Replace(strBTime[e.Row],'/',e.Row,1);
				}
			}
			FillTime();
		}

		private void butSlider_MouseDown(object sender,System.Windows.Forms.MouseEventArgs e) {
			mouseIsDown=true;
			mouseOrigin=new Point(e.X+butSlider.Location.X,e.Y+butSlider.Location.Y);
			sliderOrigin=butSlider.Location;
		}

		private void butSlider_MouseMove(object sender,System.Windows.Forms.MouseEventArgs e) {
			if(!mouseIsDown){
				return;
			}
			//tempPoint represents the new location of button of smooth dragging.
			Point tempPoint=new Point(sliderOrigin.X,sliderOrigin.Y+(e.Y+butSlider.Location.Y)-mouseOrigin.Y);
			int step=(int)(Math.Round((Decimal)(tempPoint.Y-tbTime.Location.Y)/14));
			if(step==strBTime.Length){
				return;
			}
			if(step<1){
				return;
			}
			if(step>tbTime.MaxRows-1){
				return;
			}
			if(step>strBTime.Length) {
				strBTime.Append('/');
			}
			if(step<strBTime.Length) {
				strBTime.Remove(step,1);
			}
			checkTimeLocked.Checked=true;
			FillTime();
		}

		private void butSlider_MouseUp(object sender,System.Windows.Forms.MouseEventArgs e) {
			mouseIsDown=false;
		}
		
		private void gridComm_MouseMove(object sender,MouseEventArgs e) {
			
		}

		private void gridPatient_MouseMove(object sender,MouseEventArgs e) {
			
		}

		private void gridPatient_CellClick(object sender,ODGridClickEventArgs e) {
			ODGridCell gridCellCur=gridPatient.Rows[e.Row].Cells[e.Col];
			//Only grid cells with phone numbers are blue and underlined.
			if(gridCellCur.ColorText==System.Drawing.Color.Blue && gridCellCur.Underline==YN.Yes && Programs.GetCur(ProgramName.DentalTekSmartOfficePhone).Enabled) {
				DentalTek.PlaceCall(gridCellCur.Text);
			}
		}

		private void listQuickAdd_MouseDown(object sender,System.Windows.Forms.MouseEventArgs e) {
			if(comboProvNum.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a provider.");
				return;
			}
			if(listQuickAdd.IndexFromPoint(e.X,e.Y)==-1) {
				return;
			}
			if(AptCur.AptStatus==ApptStatus.Complete) {
				//added procedures would be marked complete when form closes. We'll just stop it here.
				if(!Security.IsAuthorized(Permissions.ProcComplCreate)) {
					return;
				}
			}
			Procedures.SetDateFirstVisit(AptCur.AptDateTime.Date,1,pat);
			List<PatPlan> PatPlanList=PatPlans.Refresh(AptCur.PatNum);
			List<Benefit> benefitList=Benefits.Refresh(PatPlanList,SubList);
			List<ClaimProc> ClaimProcList=ClaimProcs.Refresh(AptCur.PatNum);
			string[] codes=DefC.Short[(int)DefCat.ApptProcsQuickAdd][listQuickAdd.IndexFromPoint(e.X,e.Y)].ItemValue.Split(',');
			for(int i=0;i<codes.Length;i++) {
				if(!ProcedureCodeC.HList.ContainsKey(codes[i])) {
					MsgBox.Show(this,"Definition contains invalid code.");
					return;
				}
			}
			List<Procedure> listAddedProcs=new List<Procedure>();
			for(int i=0;i<codes.Length;i++) {
				Procedure proc=new Procedure();
				proc.PatNum=AptCur.PatNum;
				if(AptCur.AptStatus!=ApptStatus.Planned) {
					proc.AptNum=AptCur.AptNum;
				}
				proc.CodeNum=ProcedureCodes.GetProcCode(codes[i]).CodeNum;
				proc.ProcDate=AptCur.AptDateTime.Date;
				proc.DateTP=DateTimeOD.Today;
				InsPlan priplan=null;
				InsSub prisub=null;
				if(PatPlanList.Count>0) {
					prisub=InsSubs.GetSub(PatPlanList[0].InsSubNum,SubList);
					priplan=InsPlans.GetPlan(prisub.PlanNum,PlanList);
				}
				//Check if it's a medical procedure.
				double insfee;
				bool isMed = false;
				proc.MedicalCode=ProcedureCodes.GetProcCode(_listProcCodes,proc.CodeNum).MedicalCode;
				if(proc.MedicalCode != null && proc.MedicalCode != "") {
					isMed = true;
				}
				//Get fee schedule for medical or dental.
				long feeSch;
				if(isMed) {
					feeSch=Fees.GetMedFeeSched(pat,PlanList,PatPlanList,SubList);
				}
				else {
					feeSch=Fees.GetFeeSched(pat,PlanList,PatPlanList,SubList);
				}
				if(ProcedureCodes.GetProcCode(_listProcCodes,proc.CodeNum).ProvNumDefault==0) {//Override ProvNum if there is a default provider for this proc
					proc.ProvNum=ProviderC.ListShort[comboProvNum.SelectedIndex].ProvNum;//Normal behavior
				}
				else {
					proc.ProvNum=ProcedureCodes.GetProcCode(_listProcCodes,proc.CodeNum).ProvNumDefault;//New behavior for procs with default provider
				}
				proc.ClinicNum=AptCur.ClinicNum;
				//Get the fee amount for medical or dental.
				if(PrefC.GetBool(PrefName.MedicalFeeUsedForNewProcs) && isMed) {
					insfee=Fees.GetAmount0(ProcedureCodes.GetProcCode(proc.MedicalCode).CodeNum,feeSch,proc.ClinicNum,proc.ProvNum);
				}
				else {
					insfee=Fees.GetAmount0(proc.CodeNum,feeSch,proc.ClinicNum,proc.ProvNum);
				}
				if(priplan!=null && priplan.PlanType=="p") {//PPO
					double standardfee=Fees.GetAmount0(proc.CodeNum,Providers.GetProv(Patients.GetProvNum(pat),_listProviders).FeeSched,proc.ClinicNum,proc.ProvNum);
					if(standardfee>insfee) {
						proc.ProcFee=standardfee;
					}
					else {
						proc.ProcFee=insfee;
					}
				}
				else {
					proc.ProcFee=insfee;
				}
				//surf
				//toothnum
				//toothrange
				//priority
				proc.ProcStatus=ProcStat.TP;
				//procnote
				//Dx
				proc.SiteNum=pat.SiteNum;
				proc.RevCode=ProcedureCodes.GetProcCode(_listProcCodes,proc.CodeNum).RevenueCodeDefault;
				if(_isPlanned) {
					proc.PlannedAptNum=AptCur.AptNum;
				}
				proc.BaseUnits=ProcedureCodes.GetProcCode(_listProcCodes,proc.CodeNum).BaseUnits;
				proc.DiagnosticCode=PrefC.GetString(PrefName.ICD9DefaultForNewProcs);
				if(Userods.IsUserCpoe(Security.CurUser)) {
					//This procedure is considered CPOE because the provider is the one that has added it.
					proc.IsCpoe=true;
				}
				Procedures.Insert(proc);//recall synch not required
				if(Programs.UsingOrion){//Orion requires a DPC for every procedure. Force proc edit window open.
					FormProcEdit FormP=new FormProcEdit(proc,pat.Copy(),fam);
					FormP.IsNew=true;
					FormP.OrionDentist=true;
					FormP.ShowDialog();
					if(FormP.DialogResult==DialogResult.Cancel) {
						try {
							Procedures.Delete(proc.ProcNum);//also deletes the claimprocs
						}
						catch(Exception ex) {
							MessageBox.Show(ex.Message);
						}
					}
				}
				Procedures.ComputeEstimates(proc,pat.PatNum,ClaimProcList,false,PlanList,PatPlanList,benefitList,pat.Age,SubList);
				listAddedProcs.Add(proc);
			}
			//Get from db to remove nulls. Consider initializing dates and strings instead.  Safe to get from DB since they were just inserted above.
			_listProcs.AddRange(Procedures.GetManyProc(listAddedProcs.Select(x => x.ProcNum).ToList(),false));
			listQuickAdd.SelectedIndex=-1;
			_listProcs.Sort(ProcedureLogic.CompareProcedures);
			FillProcedures();
			for(int i=0;i<gridProc.Rows.Count;i++) {
				if(listAddedProcs.Contains((Procedure)gridProc.Rows[i].Tag)) {
					gridProc.SetSelected(i,true);//Select those that were just added.
				}
			}
			CalculateTime();
			FillTime();
			CalcPatientFeeThisAppt();
		}

		private void butLab_Click(object sender,EventArgs e) {
			if(_labCur==null) {//no labcase
				//so let user pick one to add
				FormLabCaseSelect FormL=new FormLabCaseSelect();
				FormL.PatNum=AptCur.PatNum;
				FormL.IsPlanned=_isPlanned;
				FormL.ShowDialog();
				if(FormL.DialogResult!=DialogResult.OK){
					return;
				}
				if(_isPlanned) {
					LabCases.AttachToPlannedAppt(FormL.SelectedLabCaseNum,AptCur.AptNum);
				}
				else{
					LabCases.AttachToAppt(FormL.SelectedLabCaseNum,AptCur.AptNum);
				}
			}
			else{//already a labcase attached
				FormLabCaseEdit FormLCE=new FormLabCaseEdit();
				FormLCE.CaseCur=_labCur;
				FormLCE.ShowDialog();
				if(FormLCE.DialogResult!=DialogResult.OK){
					return;
				}
				//Deleting or detaching labcase would have been done from in that window
			}
			_labCur=LabCases.GetForApt(AptCur);
			textLabCase.Text=GetLabCaseDescript();
		}

		private void butInsPlan1_Click(object sender,EventArgs e) {
			FormInsPlanSelect FormIPS=new FormInsPlanSelect(AptCur.PatNum);
			FormIPS.ShowNoneButton=true;
			FormIPS.ViewRelat=false;
			FormIPS.ShowDialog();
			if(FormIPS.DialogResult!=DialogResult.OK) {
				return;
			}
			if(FormIPS.SelectedPlan==null) {
				AptCur.InsPlan1=0;
				textInsPlan1.Text="";
				return;
			}
			AptCur.InsPlan1=FormIPS.SelectedPlan.PlanNum;
			textInsPlan1.Text=InsPlans.GetCarrierName(AptCur.InsPlan1,PlanList);
		}

		private void butInsPlan2_Click(object sender,EventArgs e) {
			FormInsPlanSelect FormIPS=new FormInsPlanSelect(AptCur.PatNum);
			FormIPS.ShowNoneButton=true;
			FormIPS.ViewRelat=false;
			FormIPS.ShowDialog();
			if(FormIPS.DialogResult!=DialogResult.OK) {
				return;
			}
			if(FormIPS.SelectedPlan==null) {
				AptCur.InsPlan2=0;
				textInsPlan2.Text="";
				return;
			}
			AptCur.InsPlan2=FormIPS.SelectedPlan.PlanNum;
			textInsPlan2.Text=InsPlans.GetCarrierName(AptCur.InsPlan2,PlanList);
		}

		private void butRequirement_Click(object sender,EventArgs e) {
			FormReqAppt FormR=new FormReqAppt();
			FormR.AptNum=AptCur.AptNum;
			FormR.PatNum=AptCur.PatNum;
			FormR.ShowDialog();
			if(FormR.DialogResult!=DialogResult.OK){
				return;
			}
			List<ReqStudent> listStudents=ReqStudents.GetForAppt(AptCur.AptNum);
			string requirements="";
			for(int i=0;i<listStudents.Count;i++) {
				if(i!=0) {
					requirements+="\r\n";
				}
				Provider student=Providers.GetProv(listStudents[i].ProvNum,_listProviders);
				requirements+=student.LName+", "+student.FName+": "+listStudents[i].Descript;
			}
			textRequirement.Text=requirements;
		}

		private void butSyndromicObservations_Click(object sender,EventArgs e) {
			FormEhrAptObses formE=new FormEhrAptObses(AptCur);
			formE.ShowDialog();
		}

		private void menuItemArrivedNow_Click(object sender,EventArgs e) {
			textTimeArrived.Text=DateTime.Now.ToShortTimeString();
		}

		private void menuItemSeatedNow_Click(object sender,EventArgs e) {
			textTimeSeated.Text=DateTime.Now.ToShortTimeString();
		}

		private void menuItemDismissedNow_Click(object sender,EventArgs e) {
			textTimeDismissed.Text=DateTime.Now.ToShortTimeString();
		}

		private void gridFields_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			if(ApptFieldDefs.HasDuplicateFieldNames()) {//Check for duplicate field names.
				MsgBox.Show(this,"There are duplicate appointment field defs, go rename or delete the duplicates.");
				return;
			}
			ApptField field=ApptFields.GetOne(PIn.Long(_tableFields.Rows[e.Row]["ApptFieldNum"].ToString()));
			if(field==null) {
				field=new ApptField();
				field.AptNum=AptCur.AptNum;
				field.FieldName=_tableFields.Rows[e.Row]["FieldName"].ToString();
				if(ApptFieldDefs.Listt[e.Row].FieldType==ApptFieldType.Text) {
					FormApptFieldEdit formAF=new FormApptFieldEdit(field);
					formAF.IsNew=true;
					formAF.ShowDialog();
				}
				else if(ApptFieldDefs.Listt[e.Row].FieldType==ApptFieldType.PickList) {
					FormApptFieldPickEdit formAF=new FormApptFieldPickEdit(field);
					formAF.IsNew=true;
					formAF.ShowDialog();
				}
			}
			else {
				if(ApptFieldDefs.Listt[e.Row].FieldType==ApptFieldType.Text) {
					FormApptFieldEdit formAF=new FormApptFieldEdit(field);
					formAF.ShowDialog();
				}
				else if(ApptFieldDefs.Listt[e.Row].FieldType==ApptFieldType.PickList) {
					FormApptFieldPickEdit formAF=new FormApptFieldPickEdit(field);
					formAF.ShowDialog();
				}
			}
			_tableFields=Appointments.GetApptFields(AptCur.AptNum);
			FillFields();
		}

		///<summary>Called from butOK_Click and butPin_Click. Only saves appointment infomration to DB. Procedure information is updated to _listProcs for
		///use with syncing later.</summary>
		private bool UpdateListAndDB(){
			DateTime dateTimeAskedToArrive=DateTime.MinValue;
			if((AptOld.AptStatus==ApptStatus.Complete && comboStatus.SelectedIndex!=1)
				|| (AptOld.AptStatus==ApptStatus.Broken && comboStatus.SelectedIndex!=4)) //Un-completing or un-breaking the appt.  We must use selectedindex due to AptCur gets updated later UpdateDB()
			{
				//If the insurance plans have changed since this appt was completed, warn the user that the historical data will be neutralized.
				List<PatPlan> listPatPlans=PatPlans.Refresh(pat.PatNum);
				InsSub sub1=InsSubs.GetSub(PatPlans.GetInsSubNum(listPatPlans,PatPlans.GetOrdinal(PriSecMed.Primary,listPatPlans,PlanList,SubList)),SubList);
				InsSub sub2=InsSubs.GetSub(PatPlans.GetInsSubNum(listPatPlans,PatPlans.GetOrdinal(PriSecMed.Secondary,listPatPlans,PlanList,SubList)),SubList);
				if(sub1.PlanNum!=AptCur.InsPlan1 || sub2.PlanNum!=AptCur.InsPlan2) {
					if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"The current insurance plans for this patient are different than the plans associated to this appointment.  They will be updated to the patient's current insurance plans.  Continue?")) {
						return false;
					}
					//Update the ins plans associated to this appointment so that they're the most accurate at this time.
					AptCur.InsPlan1=sub1.PlanNum;
					AptCur.InsPlan2=sub2.PlanNum;
				}
			}
			if(textTimeAskedToArrive.Text!=""){
				try{
					dateTimeAskedToArrive=AptCur.AptDateTime.Date+DateTime.Parse(textTimeAskedToArrive.Text).TimeOfDay;
				}
				catch{
					MsgBox.Show(this,"Time Asked To Arrive invalid.");
					return false;
				}
			}
			DateTime dateTimeArrived=AptCur.AptDateTime.Date;
			if(textTimeArrived.Text!=""){
				try{
					dateTimeArrived=AptCur.AptDateTime.Date+DateTime.Parse(textTimeArrived.Text).TimeOfDay;
				}
				catch{
					MsgBox.Show(this,"Time Arrived invalid.");
					return false;
				}
			}
			DateTime dateTimeSeated=AptCur.AptDateTime.Date;
			if(textTimeSeated.Text!=""){
				try{
					dateTimeSeated=AptCur.AptDateTime.Date+DateTime.Parse(textTimeSeated.Text).TimeOfDay;
				}
				catch{
					MsgBox.Show(this,"Time Seated invalid.");
					return false;
				}
			}
			DateTime dateTimeDismissed=AptCur.AptDateTime.Date;
			if(textTimeDismissed.Text!=""){
				try{
					dateTimeDismissed=AptCur.AptDateTime.Date+DateTime.Parse(textTimeDismissed.Text).TimeOfDay;
				}
				catch{
					MsgBox.Show(this,"Time Dismissed invalid.");
					return false;
				}
			}
			//This change was just slightly too risky to make to 6.9, so 7.0 only
			if(AptCur.AptStatus!=ApptStatus.Complete//was not originally complete
				&& AptCur.AptStatus!=ApptStatus.PtNote
				&& AptCur.AptStatus!=ApptStatus.PtNoteCompleted
				&& comboStatus.SelectedIndex==1 //making it complete
				&& AptCur.AptDateTime.Date > DateTime.Today)//and future appt
			{
				MsgBox.Show(this,"Not allowed to set complete future appointments.");
				return false;
			}
			string aptPattern=Appointments.ConvertPatternTo5(strBTime.ToString());
			//Only run appt overlap check if editing an appt not in unscheduled list and in chart module and eCW program link not enabled.
			//Also need to see if there is a generic HL7 def enabled where Open Dental is not the filler application.
			//Open Dental is the filler application if appointments, schedules, and operatories are maintained by Open Dental and messages are sent out
			//to inform another software of any changes made.  If Open Dental is an auxiliary application, appointments are created from inbound SIU
			//messages and Open Dental no longer has control over whether the appointments overlap or which operatory/provider's schedule the appointment
			//belongs to.  In this case, we do not want to check for overlapping appointments and the appointment module should be hidden.
			HL7Def hl7DefEnabled=HL7Defs.GetOneDeepEnabled();//the ShowAppts check box is hidden for MedLab HL7 interfaces, so only need to check the others
			bool isAuxiliaryRole=false;
			if(hl7DefEnabled!=null && !hl7DefEnabled.ShowAppts) {//if the appts module is hidden
				for(int i=0;i<hl7DefEnabled.hl7DefMessages.Count;i++) {
					HL7DefMessage msgDefCur=hl7DefEnabled.hl7DefMessages[i];
					//if there is also an inbound SIU defined
					if(msgDefCur.MessageType==MessageTypeHL7.SIU && msgDefCur.InOrOut==InOutHL7.Incoming) {
						//OD is considered an auxiliary application which neither exerts control over nor requests changes to a schedule
						isAuxiliaryRole=true;
						break;
					}
				}
			}
			if((IsInChartModule || IsInViewPatAppts)
				&& !Programs.UsingEcwTightOrFullMode()//if eCW Tight or Full mode, appts created from inbound SIU messages and appt module always hidden
				&& AptCur.AptStatus!=ApptStatus.UnschedList
				&& !isAuxiliaryRole)//generic HL7 def enabled, appt module hidden and an inbound SIU msg defined, appts created from msgs so no overlap check
			{
				//==Travis 04/06/2015:  This call was added on 04/23/2014 and backported to 14.1.  It is not storing the return value and does not look to be
				//		doing anything so it has been commented out.
				//Appointments.RefreshPeriod(AptCur.AptDateTime,AptCur.AptDateTime);
				List<Appointment> apptList=Appointments.GetForPeriodList(AptCur.AptDateTime,AptCur.AptDateTime);
				if(DoesOverlap(aptPattern,apptList)) {
					MsgBox.Show(this,"Appointment is too long and would overlap another appointment.  Automatically shortened to fit.");
					do {
						aptPattern=aptPattern.Substring(0,aptPattern.Length-1);
						if(aptPattern.Length==1) {
							break;
						}
					} while(DoesOverlap(aptPattern,apptList));
				}
			}
			if(AptCur.AptStatus == ApptStatus.Planned) {
				;
			}
			else if(comboStatus.SelectedIndex==-1) {
				AptCur.AptStatus=ApptStatus.Scheduled;
			}
			else if(AptCur.AptStatus == ApptStatus.PtNote | AptCur.AptStatus == ApptStatus.PtNoteCompleted){
				AptCur.AptStatus = (ApptStatus)comboStatus.SelectedIndex + 7;
			}
			else {
				AptCur.AptStatus=(ApptStatus)comboStatus.SelectedIndex+1;//The only place in the entire form where the AptStatus gets updated.
			}
			//set procs complete was moved further down
			//convert from current increment into 5 minute increment
			//MessageBox.Show(strBTime.ToString());
			AptCur.Pattern=aptPattern;
			if(comboUnschedStatus.SelectedIndex==0){//none
				AptCur.UnschedStatus=0;
			}
			else{
				AptCur.UnschedStatus=DefC.Short[(int)DefCat.RecallUnschedStatus][comboUnschedStatus.SelectedIndex-1].DefNum;
			}
			if(comboConfirmed.SelectedIndex!=-1){
				AptCur.Confirmed=DefC.Short[(int)DefCat.ApptConfirmed][comboConfirmed.SelectedIndex].DefNum;
			}
			AptCur.TimeLocked=checkTimeLocked.Checked;
			AptCur.ColorOverride=butColor.BackColor;
			AptCur.Note=textNote.Text;
			if(comboClinic.SelectedIndex==0) {//none
				AptCur.ClinicNum=0;
			}
			else {
				AptCur.ClinicNum=Clinics.List[comboClinic.SelectedIndex-1].ClinicNum;
			}
			//there should always be a non-hidden primary provider for an appt.
			if(comboProvNum.SelectedIndex==-1) {
				AptCur.ProvNum=ProviderC.ListShort[0].ProvNum;
			}
			else {
				AptCur.ProvNum=ProviderC.ListShort[comboProvNum.SelectedIndex].ProvNum;
			}
			if(comboProvHyg.SelectedIndex==0) {//none
				AptCur.ProvHyg=0;
			}
			else {
				AptCur.ProvHyg=ProviderC.ListShort[comboProvHyg.SelectedIndex-1].ProvNum;
			}
			AptCur.IsHygiene=checkIsHygiene.Checked;
			if(comboAssistant.SelectedIndex==0) {//none
				AptCur.Assistant=0;
			}
			else {
				AptCur.Assistant=Employees.ListShort[comboAssistant.SelectedIndex-1].EmployeeNum;
			}
			AptCur.IsNewPatient=checkIsNewPatient.Checked;
			AptCur.DateTimeAskedToArrive=dateTimeAskedToArrive;
			AptCur.DateTimeArrived=dateTimeArrived;
			AptCur.DateTimeSeated=dateTimeSeated;
			AptCur.DateTimeDismissed=dateTimeDismissed;
			//AptCur.InsPlan1 and InsPlan2 already handled 
			SetProcDescript(AptCur);
			if(comboApptType.SelectedIndex==0) {//0 index = none.
				AptCur.AppointmentTypeNum=0;
			}
			else {
				AptCur.AppointmentTypeNum=_listAppointmentType[comboApptType.SelectedIndex-1].AppointmentTypeNum;
			}
			try {
				Appointments.Update(AptCur,AptOld);
				//Appointments.UpdateAttached(AptCur.AptNum,procNums,isPlanned);
			}
			catch(ApplicationException ex) {
				MessageBox.Show(ex.Message);
				return false;
			}
			//if appointment is marked complete and any procedures are not,
			//then set the remaining procedures complete
			List<Procedure> listProcsInAppt=new List<Procedure>();
			//don't use _listProcs[x].Copy() so changes made to procs in listProcsInAppt will be reflected in _listProcs
			gridProc.SelectedIndices.ToList().ForEach(x => listProcsInAppt.Add(_listProcs[x]));
			if(AptCur.AptStatus==ApptStatus.Complete) {
				if(listProcsInAppt.Any(x => x.ProcStatus!=ProcStat.C)) {
					if(!Security.IsAuthorized(Permissions.ProcComplCreate,AptCur.AptDateTime)) {
						return false;
					}
					List<PatPlan> PatPlanList=PatPlans.Refresh(AptCur.PatNum);
					//changes made to listProcsInAppt will be reflected in _listProcs
					ProcedureL.SetCompleteInAppt(AptCur,PlanList,PatPlanList,pat.SiteNum,pat.Age,SubList,listProcsInAppt);
					if(AptOld.AptStatus!=ApptStatus.Complete) { //seperate log entry for completed appointments
						SecurityLogs.MakeLogEntry(Permissions.AppointmentEdit,pat.PatNum,AptCur.AptDateTime.ToShortDateString()
						+", "+AptCur.ProcDescript+", Procedures automatically set complete due to appt being set complete",AptCur.AptNum);
					}
					else {
						SecurityLogs.MakeLogEntry(Permissions.AppointmentCompleteEdit,pat.PatNum,AptCur.AptDateTime.ToShortDateString()
						+", "+AptCur.ProcDescript+", Procedures automatically set complete due to appt being set complete",AptCur.AptNum);
					}
				}
			}
			else {
				Procedures.SetProvidersInAppointment(AptCur,listProcsInAppt);//changes made to listProcsInAppt will be reflected in _listProcs
			}
			//Save changes from local listProcsInAppt to classwide _listProcs
			foreach(Procedure proc in listProcsInAppt) {
				int procIndex=_listProcs.FindIndex(x => x.ProcNum==proc.ProcNum);
				if(procIndex<0) {
					continue;
				}
				_listProcs[procIndex]=proc.Copy();
			}
			//Do the appointment "break" automation for appointments that were just broken.
			if(AptCur.AptStatus==ApptStatus.Broken && AptOld.AptStatus!=ApptStatus.Broken) {
				if(AptOld.AptStatus!=ApptStatus.Complete) { //seperate log entry for completed appointments
					SecurityLogs.MakeLogEntry(Permissions.AppointmentEdit,pat.PatNum,AptCur.ProcDescript+", "+AptCur.AptDateTime.ToString()
					+", Broken by changing the Status in the Edit Appointment window.",AptCur.AptNum);
				}
				else {
					SecurityLogs.MakeLogEntry(Permissions.AppointmentCompleteEdit,pat.PatNum,AptCur.ProcDescript+", "+AptCur.AptDateTime.ToString()
					+", Broken by changing the Status in the Edit Appointment window.",AptCur.AptNum);
				}
				//If there is an existing HL7 def enabled, send a SIU message if there is an outbound SIU message defined
				if(HL7Defs.IsExistingHL7Enabled()) {
					//S15 - Appt Cancellation event
					MessageHL7 messageHL7=MessageConstructor.GenerateSIU(pat,fam.GetPatient(pat.Guarantor),EventTypeHL7.S15,AptCur);
					//Will be null if there is no outbound SIU message defined, so do nothing
					if(messageHL7!=null) {
						HL7Msg hl7Msg=new HL7Msg();
						hl7Msg.AptNum=AptCur.AptNum;
						hl7Msg.HL7Status=HL7MessageStatus.OutPending;//it will be marked outSent by the HL7 service.
						hl7Msg.MsgText=messageHL7.ToString();
						hl7Msg.PatNum=pat.PatNum;
						HL7Msgs.Insert(hl7Msg);
#if DEBUG
						MessageBox.Show(this,messageHL7.ToString());
#endif
					}
				}
				if(ProcedureCodes.HasBrokenApptCode()) {//ADA proc code D9986 exists
					ProcedureCode procCodeBrokenApt=ProcedureCodes.GetProcCode("D9986");
					Procedure procedureCur=new Procedure();
					procedureCur.PatNum=pat.PatNum;
					procedureCur.ProvNum=AptCur.ProvNum;
					procedureCur.CodeNum=procCodeBrokenApt.CodeNum;
					procedureCur.ProcDate=DateTime.Today;
					procedureCur.DateEntryC=DateTime.Now;
					procedureCur.ProcStatus=ProcStat.C;
					procedureCur.ClinicNum=AptCur.ClinicNum;
					procedureCur.UserNum=Security.CurUser.UserNum;
					procedureCur.Note=Lan.g(this,"Appt BROKEN for ")+AptCur.ProcDescript+"  "+AptCur.AptDateTime.ToString();
					List<InsSub> listInsSubs=InsSubs.RefreshForFam(Patients.GetFamily(pat.PatNum));
					List<InsPlan> listInsPlans=InsPlans.RefreshForSubList(listInsSubs);
					List<PatPlan> listPatPlans=PatPlans.Refresh(pat.PatNum);
					InsPlan insPlanPrimary=null;
					InsSub insSubPrimary=null;
					if(listPatPlans.Count>0) {
						insSubPrimary=InsSubs.GetSub(listPatPlans[0].InsSubNum,listInsSubs);
						insPlanPrimary=InsPlans.GetPlan(insSubPrimary.PlanNum,listInsPlans);
					}
					double procFee;
					long feeSch;
					if(insPlanPrimary==null || procCodeBrokenApt.NoBillIns) {
						feeSch=Fees.GetFeeSched(0,pat.FeeSched,procedureCur.ProvNum);
					}
					else {//Only take into account the patient's insurance fee schedule if the D9986 procedure is not marked as NoBillIns
						feeSch=Fees.GetFeeSched(insPlanPrimary.FeeSched,pat.FeeSched,procedureCur.ProvNum);
					}
					procFee=Fees.GetAmount0(procedureCur.CodeNum,feeSch,procedureCur.ClinicNum,procedureCur.ProvNum);
					if(insPlanPrimary!=null && insPlanPrimary.PlanType=="p" && !insPlanPrimary.IsMedical) {//PPO
						double provFee=Fees.GetAmount0(procedureCur.CodeNum,Providers.GetProv(procedureCur.ProvNum,_listProviders).FeeSched,procedureCur.ClinicNum,
							procedureCur.ProvNum);
						procedureCur.ProcFee=Math.Max(provFee,procFee);
					}
					else {
						procedureCur.ProcFee=procFee;
					}
					if(!PrefC.GetBool(PrefName.EasyHidePublicHealth)) {
						procedureCur.SiteNum=pat.SiteNum;
					}
					_listProcs.Add(procedureCur);
					Procedures.Insert(procedureCur);
					//Now make a claimproc if the patient has insurance.  We do this now for consistency because a claimproc could get created in the future.
					List<Benefit> listBenefits=Benefits.Refresh(listPatPlans,listInsSubs);
					List<ClaimProc> listClaimProcsForProc=ClaimProcs.RefreshForProc(procedureCur.ProcNum);
					Procedures.ComputeEstimates(procedureCur,pat.PatNum,listClaimProcsForProc,false,listInsPlans,listPatPlans,listBenefits,pat.Age,listInsSubs);
					FormProcBroken FormPB=new FormProcBroken(procedureCur);
					FormPB.IsNew=true;
					FormPB.ShowDialog();
					if(PrefC.GetBool(PrefName.BrokenApptAdjustmentWithProcedure)) {
						Adjustment AdjustmentCur=new Adjustment();
						AdjustmentCur.DateEntry=DateTime.Today;
						AdjustmentCur.AdjDate=DateTime.Today;
						AdjustmentCur.ProcDate=DateTime.Today;
						AdjustmentCur.ProvNum=AptCur.ProvNum;
						AdjustmentCur.PatNum=pat.PatNum;
						AdjustmentCur.AdjType=PrefC.GetLong(PrefName.BrokenAppointmentAdjustmentType);
						AdjustmentCur.ClinicNum=AptCur.ClinicNum;
						FormAdjust FormA=new FormAdjust(pat,AdjustmentCur);
						FormA.IsNew=true;
						FormA.ShowDialog();
					}
					if(PrefC.GetBool(PrefName.BrokenApptCommLogWithProcedure)) {
						Commlog CommlogCur=new Commlog();
						CommlogCur.PatNum=pat.PatNum;
						CommlogCur.CommDateTime=DateTime.Now;
						CommlogCur.CommType=Commlogs.GetTypeAuto(CommItemTypeAuto.APPT);
						CommlogCur.Note=Lan.g(this,"Appt BROKEN for ")+AptCur.ProcDescript+"  "+AptCur.AptDateTime.ToString();
						CommlogCur.Mode_=CommItemMode.None;
						CommlogCur.UserNum=Security.CurUser.UserNum;
						FormCommItem FormCI=new FormCommItem(CommlogCur);
						FormCI.IsNew=true;
						FormCI.ShowDialog();
					}
				}
				else {//No D9986 present
					if(PrefC.GetBool(PrefName.BrokenApptCommLogNotAdjustment)) {
						Commlog CommlogCur=new Commlog();
						CommlogCur.PatNum=pat.PatNum;
						CommlogCur.CommDateTime=DateTime.Now;
						CommlogCur.CommType=Commlogs.GetTypeAuto(CommItemTypeAuto.APPT);
						CommlogCur.Note=Lan.g(this,"Appt BROKEN for ")+AptCur.ProcDescript+"  "+AptCur.AptDateTime.ToString();
						CommlogCur.Mode_=CommItemMode.None;
						CommlogCur.UserNum=Security.CurUser.UserNum;
						FormCommItem FormCI=new FormCommItem(CommlogCur);
						FormCI.IsNew=true;
						FormCI.ShowDialog();
					}
					else {
						Adjustment AdjustmentCur=new Adjustment();
						AdjustmentCur.DateEntry=DateTime.Today;
						AdjustmentCur.AdjDate=DateTime.Today;
						AdjustmentCur.ProcDate=DateTime.Today;
						AdjustmentCur.ProvNum=AptCur.ProvNum;
						AdjustmentCur.PatNum=pat.PatNum;
						AdjustmentCur.AdjType=PrefC.GetLong(PrefName.BrokenAppointmentAdjustmentType);
						AdjustmentCur.ClinicNum=AptCur.ClinicNum;
						FormAdjust FormA=new FormAdjust(pat,AdjustmentCur);
						FormA.IsNew=true;
						FormA.ShowDialog();
					}
				}
				AutomationL.Trigger(AutomationTrigger.BreakAppointment,null,pat.PatNum);
			}
			return true;
		}

		///<summary>This code is also in FormProcEdit.SaveAndClose() and FormDatabaseMaintenance.butApptProcs_Click().  Make any changes there as well.
		///Consider moving all of this logic into Appointments.cs at some point, so we do not have to keep editing in multiple places.</summary>
		private void SetProcDescript(Appointment apt) {
			apt.ProcDescript="";
			apt.ProcsColored="";
			int numAptProcs=0;
			for(int i=0;i<_listProcs.Count;i++) {
				Procedure proc=_listProcs[i];
				if(apt.AptStatus==ApptStatus.Planned && apt.AptNum != proc.PlannedAptNum) {
					continue;
				}
				if(apt.AptStatus != ApptStatus.Planned && apt.AptNum != proc.AptNum) {
					continue;
				}
				string procDescOne="";
				ProcedureCode procCode=ProcedureCodes.GetProcCode(_listProcCodes,proc.CodeNum);
				if(numAptProcs > 0) {
					apt.ProcDescript+=", ";
				}
				switch(procCode.TreatArea) {
					case TreatmentArea.Surf:
						procDescOne+="#"+Tooth.GetToothLabel(proc.ToothNum)+"-"
				      +proc.Surf+"-";//""#12-MOD-"
						break;
					case TreatmentArea.Tooth:
						procDescOne+="#"+Tooth.GetToothLabel(proc.ToothNum)+"-";//"#12-"
						break;
					default://area 3 or 0 (mouth)
						break;
					case TreatmentArea.Quad:
						procDescOne+=proc.Surf+"-";//"UL-"
						break;
					case TreatmentArea.Sextant:
						procDescOne+="S"+proc.Surf+"-";//"S2-"
						break;
					case TreatmentArea.Arch:
						procDescOne+=proc.Surf+"-";//"U-"
						break;
					case TreatmentArea.ToothRange:
						//strLine+=table.Rows[j][13].ToString()+" ";//don't show range
						break;
				}
				procDescOne+=procCode.AbbrDesc;
				apt.ProcDescript+=procDescOne;
				//Color and previous date are determined by ProcApptColor object
				ProcApptColor pac=ProcApptColors.GetMatch(procCode.ProcCode);
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
				numAptProcs++;
			}
		}

		///<summary>Tests all appts for the day, even not visible, to make sure AptCur doesn't overlap others. Pass in the pattern for the appt being edited and the list of appts to test against.</summary>
		private bool DoesOverlap(string pattern,List<Appointment> apptList) {
			DateTime aptDateTime;
			for(int i=0;i<apptList.Count;i++) {
				if(apptList[i].AptNum==AptCur.AptNum) {
					continue;
				}
				if(apptList[i].Op!=AptCur.Op) {
					continue;
				}
				aptDateTime=apptList[i].AptDateTime;
				if(aptDateTime.Date!=AptCur.AptDateTime.Date) {
					continue;
				}
				//tests start time
				if(AptCur.AptDateTime.TimeOfDay >= aptDateTime.TimeOfDay
					&& AptCur.AptDateTime.TimeOfDay < aptDateTime.TimeOfDay.Add(TimeSpan.FromMinutes(
					apptList[i].Pattern.Length*5))) {
					return true;
				}
				//tests stop time
				if(AptCur.AptDateTime.TimeOfDay.Add(TimeSpan.FromMinutes(pattern.Length*5)) > aptDateTime.TimeOfDay
					&& AptCur.AptDateTime.TimeOfDay.Add(TimeSpan.FromMinutes(pattern.Length*5))
					<= aptDateTime.TimeOfDay.Add(TimeSpan.FromMinutes(apptList[i].Pattern.Length*5))) {
					return true;
				}
				//tests engulf
				if(AptCur.AptDateTime.TimeOfDay <= aptDateTime.TimeOfDay
					&& AptCur.AptDateTime.TimeOfDay.Add(TimeSpan.FromMinutes(pattern.Length*5))
					>= aptDateTime.TimeOfDay.Add(TimeSpan.FromMinutes(apptList[i].Pattern.Length*5))) {
					return true;
				}
			}
			return false;
		}

		private void butPDF_Click(object sender,EventArgs e) {
			//this will only happen for eCW HL7 interface users.
			List<Procedure> listProcsForAppt=_listProcs.FindAll(x => x.AptNum==AptCur.AptNum);
			string duplicateProcs=ProcedureL.ProcsContainDuplicates(listProcsForAppt);
			if(duplicateProcs!="") {
				MessageBox.Show(duplicateProcs);
				return;
			}
			//Send DFT to eCW containing a dummy procedure with this appointment in a .pdf file.	
			//no security
			string pdfDataStr=GenerateProceduresIntoPdf();
			if(HL7Defs.IsExistingHL7Enabled()) {
				//PDF messages do not contain FT1 segments, so proc list can be empty
				//MessageHL7 messageHL7=MessageConstructor.GenerateDFT(procs,EventTypeHL7.P03,pat,Patients.GetPat(pat.Guarantor),AptCur.AptNum,"progressnotes",pdfDataStr);
				MessageHL7 messageHL7=MessageConstructor.GenerateDFT(new List<Procedure>(),EventTypeHL7.P03,pat,Patients.GetPat(pat.Guarantor),AptCur.AptNum,"progressnotes",pdfDataStr);
				if(messageHL7==null) {
					MsgBox.Show(this,"There is no DFT message type defined for the enabled HL7 definition.");
					return;
				}
				HL7Msg hl7Msg=new HL7Msg();
				//hl7Msg.AptNum=AptCur.AptNum;
				hl7Msg.AptNum=0;//Prevents the appt complete button from changing to the "Revise" button prematurely.
				hl7Msg.HL7Status=HL7MessageStatus.OutPending;//it will be marked outSent by the HL7 service.
				hl7Msg.MsgText=messageHL7.ToString();
				hl7Msg.PatNum=pat.PatNum;
				HL7Msgs.Insert(hl7Msg);
#if DEBUG
				MessageBox.Show(this,messageHL7.ToString());
#endif
			}
			else {
				Bridges.ECW.SendHL7(AptCur.AptNum,AptCur.ProvNum,pat,pdfDataStr,"progressnotes",true,null);//just pdf, passing null proc list
			}
			MsgBox.Show(this,"Notes PDF sent.");
		}

		///<summary>Creates a new .pdf file containing all of the procedures attached to this appointment and 
		///returns the contents of the .pdf file as a base64 encoded string.</summary>
		private string GenerateProceduresIntoPdf(){
			MigraDoc.DocumentObjectModel.Document doc=new MigraDoc.DocumentObjectModel.Document();
			doc.DefaultPageSetup.PageWidth=Unit.FromInch(8.5);
			doc.DefaultPageSetup.PageHeight=Unit.FromInch(11);
			doc.DefaultPageSetup.TopMargin=Unit.FromInch(.5);
			doc.DefaultPageSetup.LeftMargin=Unit.FromInch(.5);
			doc.DefaultPageSetup.RightMargin=Unit.FromInch(.5);
			MigraDoc.DocumentObjectModel.Section section=doc.AddSection();
			MigraDoc.DocumentObjectModel.Font headingFont=MigraDocHelper.CreateFont(13,true);
			MigraDoc.DocumentObjectModel.Font bodyFontx=MigraDocHelper.CreateFont(9,false);
			string text;
			//Heading---------------------------------------------------------------------------------------------------------------
			#region printHeading
			Paragraph par=section.AddParagraph();
			ParagraphFormat parformat=new ParagraphFormat();
			parformat.Alignment=ParagraphAlignment.Center;
			parformat.Font=MigraDocHelper.CreateFont(10,true);
			par.Format=parformat;
			text=Lan.g(this,"procedures").ToUpper();
			par.AddFormattedText(text,headingFont);
			par.AddLineBreak();
			text=pat.GetNameFLFormal();
			par.AddFormattedText(text,headingFont);
			par.AddLineBreak();
			text=DateTime.Now.ToShortDateString();
			par.AddFormattedText(text,headingFont);
			par.AddLineBreak();
			par.AddLineBreak();
			#endregion
			//Procedure List--------------------------------------------------------------------------------------------------------
			#region Procedure List
			ODGrid gridProg=new ODGrid();
			this.Controls.Add(gridProg);//Only added temporarily so that printing will work. Removed at end with Dispose().
			gridProg.BeginUpdate();
			gridProg.Columns.Clear();
			ODGridColumn col;
			List<DisplayField> fields=DisplayFields.GetDefaultList(DisplayFieldCategory.None);
			for(int i=0;i<fields.Count;i++){
				if(fields[i].InternalName=="User" || fields[i].InternalName=="Signed"){
					continue;
				}
				if(fields[i].Description==""){
					col=new ODGridColumn(fields[i].InternalName,fields[i].ColumnWidth);
				}
				else{
					col=new ODGridColumn(fields[i].Description,fields[i].ColumnWidth);
				}
				if(fields[i].InternalName=="Amount"){
					col.TextAlign=HorizontalAlignment.Right;
				}
				if(fields[i].InternalName=="ADA Code")
				{
					col.TextAlign=HorizontalAlignment.Center;
				}
				gridProg.Columns.Add(col);
			}
			gridProg.NoteSpanStart=2;
			gridProg.NoteSpanStop=7;
			gridProg.Rows.Clear();
			List<Procedure> procsForDay=_listProcs.FindAll(x => x.ProcDate.Date==AptCur.AptDateTime.Date || x.DateEntryC.Date==AptCur.AptDateTime.Date);
			for(int i=0;i<procsForDay.Count;i++){
				Procedure proc=procsForDay[i];
				ProcedureCode procCode=ProcedureCodes.GetProcCode(_listProcCodes,proc.CodeNum);
				Provider prov=Providers.GetProv(proc.ProvNum,_listProviders);
				Userod usr=Userods.GetUser(proc.UserNum);
				ODGridRow row=new ODGridRow();
				row.ColorLborder=System.Drawing.Color.Black;
				for(int f=0;f<fields.Count;f++) {
					switch(fields[f].InternalName){
						case "Date":
							row.Cells.Add(proc.ProcDate.Date.ToShortDateString());
							break;
						case "Time":
							row.Cells.Add(proc.ProcDate.ToString("h:mm")+proc.ProcDate.ToString("%t").ToLower());
							break;
						case "Th":
							row.Cells.Add(proc.ToothNum);
							break;
						case "Surf":
							row.Cells.Add(proc.Surf);
							break;
						case "Dx":
							row.Cells.Add(proc.Dx.ToString());
							break;
						case "Description":
							row.Cells.Add((procCode.LaymanTerm!="")?procCode.LaymanTerm:procCode.Descript);
							break;
						case "Stat":
							row.Cells.Add(Lans.g("enumProcStat",proc.ProcStatus.ToString()));
							break;
						case "Prov":
							if(prov.Abbr.Length>5){
								row.Cells.Add(prov.Abbr.Substring(0,5));
							}
							else{
								row.Cells.Add(prov.Abbr);
							}
							break;
						case "Amount":
							row.Cells.Add(proc.ProcFee.ToString("F"));
							break;
						case "ADA Code":
							if(procCode.ProcCode.Length>5 && procCode.ProcCode.StartsWith("D")) {
								row.Cells.Add(procCode.ProcCode.Substring(0,5));//Remove suffix from all D codes.
							}
							else {
								row.Cells.Add(procCode.ProcCode);
							}
							break;
						case "User":
							row.Cells.Add(usr!=null?usr.UserName:"");
						  break;
					}
				}
				row.Note=proc.Note;
				//Row text color.
				switch(proc.ProcStatus) {
					case ProcStat.TP:
						row.ColorText=DefC.Long[(int)DefCat.ProgNoteColors][0].ItemColor;
						break;
					case ProcStat.C:
						row.ColorText=DefC.Long[(int)DefCat.ProgNoteColors][1].ItemColor;
						break;
					case ProcStat.EC:
						row.ColorText=DefC.Long[(int)DefCat.ProgNoteColors][2].ItemColor;
						break;
					case ProcStat.EO:
						row.ColorText=DefC.Long[(int)DefCat.ProgNoteColors][3].ItemColor;
						break;
					case ProcStat.R:
						row.ColorText=DefC.Long[(int)DefCat.ProgNoteColors][4].ItemColor;
						break;
					case ProcStat.D:
						row.ColorText=System.Drawing.Color.Black;
						break;
					case ProcStat.Cn:
						row.ColorText=DefC.Long[(int)DefCat.ProgNoteColors][22].ItemColor;
						break;
				}
				row.ColorBackG=System.Drawing.Color.White;
				if(proc.ProcDate.Date==DateTime.Today) {
					row.ColorBackG=DefC.Long[(int)DefCat.MiscColors][6].ItemColor;
				}				
				gridProg.Rows.Add(row);
			}
			MigraDocHelper.DrawGrid(section,gridProg);
			#endregion		
			MigraDoc.Rendering.PdfDocumentRenderer pdfRenderer=new MigraDoc.Rendering.PdfDocumentRenderer(true,PdfFontEmbedding.Always);
			pdfRenderer.Document=doc;
			pdfRenderer.RenderDocument();
			MemoryStream ms=new MemoryStream();
			pdfRenderer.PdfDocument.Save(ms);
			byte[] pdfBytes=ms.GetBuffer();
			//#region Remove when testing is complete.
			//string tempFilePath=Path.GetTempFileName();
			//File.WriteAllBytes(tempFilePath,pdfBytes);
			//#endregion
			string pdfDataStr=Convert.ToBase64String(pdfBytes);
			ms.Dispose();
			return pdfDataStr;
		}

		private void butComplete_Click(object sender,EventArgs e) {
			//This is only used with eCW HL7 interface.
			if(butComplete.Text=="Finish && Send") {
				List<Procedure> listProcsForAppt=_listProcs.FindAll(x => x.AptNum==AptCur.AptNum);
				string duplicateProcs=ProcedureL.ProcsContainDuplicates(listProcsForAppt);
				if(duplicateProcs!="") {
					MessageBox.Show(duplicateProcs);
					return;
				}
				if(ProgramProperties.GetPropVal(ProgramName.eClinicalWorks,"ProcNotesNoIncomplete")=="1") {
					if(listProcsForAppt.Any(x => x.Note!=null && x.Note.Contains("\"\""))) {
						MsgBox.Show(this,"This appointment cannot be sent because there are incomplete procedure notes.");
						return;
					}
				}
				if(ProgramProperties.GetPropVal(ProgramName.eClinicalWorks,"ProcRequireSignature")=="1") {
					if(listProcsForAppt.Any(x => !string.IsNullOrEmpty(x.Note) && string.IsNullOrEmpty(x.Signature))) {
						MsgBox.Show(this,"This appointment cannot be sent because there are unsigned procedure notes.");
						return;
					}
				}
				////check to make sure that the appointment and all attached procedures are marked complete as required.
				//bool procsAreComplete=true;
				//for(int i=0;i<gridProc.SelectedIndices.Length;i++) {
				//  string procStat=gridProc.Rows[gridProc.SelectedIndices[i]].Cells[0].Text;
				//  if(procStat!="C") {
				//    procsAreComplete=false;
				//    break;
				//  }
				//}
				//if(!procsAreComplete) {
				//  MsgBox.Show(this,"The procedures must all have statuses set to complete first.");
				//  return;
				//}
				//user can only get this far if aptNum matches visit num previously passed in by eCW.
				if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Send attached procedures to eClinicalWorks and exit?")) {
					return;
				}
				comboStatus.SelectedIndex=1;//Set the appointment status to complete. This will trigger the procedures to be completed in UpdateToDB() as well.
				if(!UpdateListAndDB()) {//procedure changes saved to db in sync call in form closing
					return;
				}
				listProcsForAppt=_listProcs.FindAll(x => x.AptNum==AptCur.AptNum);//may not be necessary
				//Send DFT to eCW containing the attached procedures for this appointment in a .pdf file.				
				string pdfDataStr=GenerateProceduresIntoPdf();
				if(HL7Defs.IsExistingHL7Enabled()) {
					//MessageConstructor.GenerateDFT(procs,EventTypeHL7.P03,pat,Patients.GetPat(pat.Guarantor),AptCur.AptNum,"progressnotes",pdfDataStr);
					MessageHL7 messageHL7=MessageConstructor.GenerateDFT(listProcsForAppt,EventTypeHL7.P03,pat,Patients.GetPat(pat.Guarantor),AptCur.AptNum,
						"progressnotes",pdfDataStr);
					if(messageHL7==null) {
						MsgBox.Show(this,"There is no DFT message type defined for the enabled HL7 definition.");
						return;
					}
					HL7Msg hl7Msg=new HL7Msg();
					hl7Msg.AptNum=AptCur.AptNum;
					hl7Msg.HL7Status=HL7MessageStatus.OutPending;//it will be marked outSent by the HL7 service.
					hl7Msg.MsgText=messageHL7.ToString();
					hl7Msg.PatNum=pat.PatNum;
					HL7Msgs.Insert(hl7Msg);
				}
				else {
					Bridges.ECW.SendHL7(AptCur.AptNum,AptCur.ProvNum,pat,pdfDataStr,"progressnotes",false,listProcsForAppt);
				}
				CloseOD=true;
				if(IsNew) {
					SecurityLogs.MakeLogEntry(Permissions.AppointmentCreate,pat.PatNum,
					AptCur.AptDateTime.ToString()+", "+AptCur.ProcDescript,
					AptCur.AptNum);
				}
				DialogResult=DialogResult.OK;
			}
			else if(butComplete.Text=="Revise") {
				if(!Security.IsAuthorized(Permissions.EcwAppointmentRevise)) {
					return;
				}
				MsgBox.Show(this,"Any changes that you make will not be sent to eCW.  You will also have to make the same changes in eCW.");
				//revise is only clickable if user has permission
				butOK.Enabled=true;
				gridProc.Enabled=true;
				listQuickAdd.Enabled=true;
				butAdd.Enabled=true;
				butDeleteProc.Enabled=true;
			}
		}

		private void butAudit_Click(object sender,EventArgs e) {
			List<Permissions> perms=new List<Permissions>();
			perms.Add(Permissions.AppointmentCreate);
			perms.Add(Permissions.AppointmentEdit);
			perms.Add(Permissions.AppointmentMove);
			perms.Add(Permissions.AppointmentCompleteEdit);
			FormAuditOneType FormA=new FormAuditOneType(pat.PatNum,perms,Lan.g(this,"Audit Trail for Appointment"),AptCur.AptNum);
			FormA.ShowDialog();
		}

		private void butTask_Click(object sender,EventArgs e) {
			if(!UpdateListAndDB()) {//procedure changes saved to db in sync call in form closing
				return;
			}
			FormTaskListSelect FormT=new FormTaskListSelect(TaskObjectType.Appointment);//,AptCur.AptNum);
			FormT.ShowDialog();
			if(FormT.DialogResult!=DialogResult.OK) {
				return;
			}
			Task task=new Task();
			task.TaskListNum=-1;//don't show it in any list yet.
			Tasks.Insert(task);
			Task taskOld=task.Copy();
			task.KeyNum=AptCur.AptNum;
			task.ObjectType=TaskObjectType.Appointment;
			task.TaskListNum=FormT.SelectedTaskListNum;
			task.UserNum=Security.CurUser.UserNum;
			FormTaskEdit FormTE=new FormTaskEdit(task,taskOld);
			FormTE.IsNew=true;
			FormTE.ShowDialog();
		}

		private void butPin_Click(object sender,System.EventArgs e) {
			if(!UpdateListAndDB()) {//procedure changes saved to db in sync call in form closing
				return;
			}
			PinClicked=true;
			DialogResult=DialogResult.OK;
		}

		///<summary>Only catches user changes, not programatic changes. For instance this does not fire when loading the form.</summary>
		private void comboApptType_SelectionChangeCommitted(object sender,EventArgs e) {
			if(comboApptType.SelectedIndex==0){
				//We will leave the color override alone, because we do not want to remove an override that the user has already chosen
				//and they can always use the none button to clear the color anyway.
			}
			else{
				butColor.BackColor=_listAppointmentType[comboApptType.SelectedIndex-1].AppointmentTypeColor;
			}
		}

		private void comboConfirmed_SelectionChangeCommitted(object sender,EventArgs e) {
			if(PrefC.GetLong(PrefName.AppointmentTimeArrivedTrigger)!=0 //Using appointmentTimeArrivedTrigger preference
				&& DefC.Short[(int)DefCat.ApptConfirmed][comboConfirmed.SelectedIndex].DefNum==PrefC.GetLong(PrefName.AppointmentTimeArrivedTrigger) //selected index matches pref
				&& String.IsNullOrWhiteSpace(textTimeArrived.Text))//time not already set 
			{
				textTimeArrived.Text=DateTime.Now.ToShortTimeString();
			}
			if(PrefC.GetLong(PrefName.AppointmentTimeSeatedTrigger)!=0 //Using AppointmentTimeSeatedTrigger preference
				&& DefC.Short[(int)DefCat.ApptConfirmed][comboConfirmed.SelectedIndex].DefNum==PrefC.GetLong(PrefName.AppointmentTimeSeatedTrigger) //selected index matches pref
				&& String.IsNullOrWhiteSpace(textTimeSeated.Text))//time not already set 
			{
				textTimeSeated.Text=DateTime.Now.ToShortTimeString();
			}
			if(PrefC.GetLong(PrefName.AppointmentTimeDismissedTrigger)!=0 //Using AppointmentTimeDismissedTrigger preference
				&& DefC.Short[(int)DefCat.ApptConfirmed][comboConfirmed.SelectedIndex].DefNum==PrefC.GetLong(PrefName.AppointmentTimeDismissedTrigger) //selected index matches pref
				&& String.IsNullOrWhiteSpace(textTimeDismissed.Text))//time not already set 
			{
				textTimeDismissed.Text=DateTime.Now.ToShortTimeString();
			}
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if (AptCur.AptStatus == ApptStatus.PtNote || AptCur.AptStatus == ApptStatus.PtNoteCompleted) {
				if (!MsgBox.Show(this, true, "Delete Patient Note?")) {
					return;
				}
				if(textNote.Text != "") {
					if(MessageBox.Show(Commlogs.GetDeleteApptCommlogMessage(textNote.Text,AptCur.AptStatus),"Question...",MessageBoxButtons.YesNo) == DialogResult.Yes) {
						Commlog CommlogCur = new Commlog();
						CommlogCur.PatNum = AptCur.PatNum;
						CommlogCur.CommDateTime = DateTime.Now;
						CommlogCur.CommType = Commlogs.GetTypeAuto(CommItemTypeAuto.APPT);
						CommlogCur.Note = "Deleted Pt NOTE from schedule, saved copy: ";
						CommlogCur.Note += textNote.Text;
						CommlogCur.UserNum=Security.CurUser.UserNum;
						//there is no dialog here because it is just a simple entry
						Commlogs.Insert(CommlogCur);
					}
				}
			}
			else {//ordinary appointment
				if (MessageBox.Show(Lan.g(this, "Delete appointment?"), "", MessageBoxButtons.OKCancel) != DialogResult.OK) {
					return;
				}
				if(textNote.Text != "") {
					if(MessageBox.Show(Commlogs.GetDeleteApptCommlogMessage(textNote.Text,AptCur.AptStatus),"Question...",MessageBoxButtons.YesNo) == DialogResult.Yes) {
						Commlog CommlogCur = new Commlog();
						CommlogCur.PatNum = AptCur.PatNum;
						CommlogCur.CommDateTime = DateTime.Now;
						CommlogCur.CommType = Commlogs.GetTypeAuto(CommItemTypeAuto.APPT);
						CommlogCur.Note = "Deleted Appt. & saved note: ";
						if(AptCur.ProcDescript != "") {
							CommlogCur.Note += AptCur.ProcDescript + ": ";
						}
						CommlogCur.Note += textNote.Text;
						CommlogCur.UserNum=Security.CurUser.UserNum;
						//there is no dialog here because it is just a simple entry
						Commlogs.Insert(CommlogCur);
					}
				}
				//If there is an existing HL7 def enabled, send a SIU message if there is an outbound SIU message defined
				if(HL7Defs.IsExistingHL7Enabled()) {
					//S17 - Appt Deletion event
					MessageHL7 messageHL7=MessageConstructor.GenerateSIU(pat,fam.GetPatient(pat.Guarantor),EventTypeHL7.S17,AptCur);
					//Will be null if there is no outbound SIU message defined, so do nothing
					if(messageHL7!=null) {
						HL7Msg hl7Msg=new HL7Msg();
						hl7Msg.AptNum=AptCur.AptNum;
						hl7Msg.HL7Status=HL7MessageStatus.OutPending;//it will be marked outSent by the HL7 service.
						hl7Msg.MsgText=messageHL7.ToString();
						hl7Msg.PatNum=pat.PatNum;
						HL7Msgs.Insert(hl7Msg);
#if DEBUG
						MessageBox.Show(this,messageHL7.ToString());
#endif
					}
				}
			}
			for(int i=0;i<_listAppointments.Count;i++) {
				if(_listAppointments[i].AptNum==AptCur.AptNum) {
					_listAppointments.RemoveAt(i);
				}
			}
			if(AptOld.AptStatus!=ApptStatus.Complete) { //seperate log entry for completed appointments
				SecurityLogs.MakeLogEntry(Permissions.AppointmentEdit,pat.PatNum,
					"Delete for date/time: "+AptCur.AptDateTime.ToString(),
					AptCur.AptNum);
			}
			else {
				SecurityLogs.MakeLogEntry(Permissions.AppointmentCompleteEdit,pat.PatNum,
					"Delete for date/time: "+AptCur.AptDateTime.ToString(),
					AptCur.AptNum);
			}
			if(IsNew) {
				Procedures.Sync(_listProcs,AptCur);//Deleted procedures were not getting synced in new deleted appointments
				DialogResult=DialogResult.Cancel;
			}
			else {
				DialogResult=DialogResult.OK;
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(comboProvNum.SelectedIndex==-1) {
				MsgBox.Show(this,"Please select a provider.");
				return;
			}
			if(_listProcsMoved.Count > 0) {
				if(!MsgBox.Show(this,true,"Some procedures were detached from a different appointment.  Continue with these changes?")) {
					return;
				}
				for(int i=0;i<_listProcsMoved.Count;i++) {
					Procedure proc=_listProcsMoved[i];
					Procedure procDB=_listProcsFromDB.FirstOrDefault(x => x.ProcNum==proc.ProcNum);
					if(_isPlanned) {
						Appointment apptOldPlanned=_listAppointments.FirstOrDefault(x => x.AptNum==procDB.PlannedAptNum && x.AptStatus==ApptStatus.Planned);
						SecurityLogs.MakeLogEntry(Permissions.AppointmentEdit,AptCur.PatNum,Lan.g(this,"Procedure")+" "
							+ProcedureCodes.GetProcCode(_listProcCodes,proc.CodeNum).AbbrDesc+" "+Lan.g(this,"moved from planned appointment created on")+" "
							+apptOldPlanned.AptDateTime.ToShortDateString()+" "+Lan.g(this,"to planned appointment created on")+" "
							+AptCur.AptDateTime.ToShortDateString());
					}
					else {
						Appointment apptOld=_listAppointments.FirstOrDefault(x => x.AptNum==procDB.AptNum);
						SecurityLogs.MakeLogEntry(Permissions.AppointmentEdit,AptCur.PatNum,Lan.g(this,"Procedure")+" "
							+ProcedureCodes.GetProcCode(_listProcCodes,proc.CodeNum).AbbrDesc+" "+Lan.g(this,"moved from appointment on")+" "+apptOld.AptDateTime
							+" "+Lan.g(this,"to appointment on")+" "+AptCur.AptDateTime);
					}
				}
			}
			if(!UpdateListAndDB()) {//procedure changes saved to db in sync call in form closing
				return;
			}
			bool isCreateAppt=false;
			bool sendHL7=false;
			if(IsNew) {
				if(AptCur.AptStatus==ApptStatus.UnschedList && AptCur.AptDateTime==DateTime.MinValue) { //If new appt is being added directly to pinboard
					//Do nothing.  Log will be created when appointment is dragged off the pinboard.
				}
				else {
					SecurityLogs.MakeLogEntry(Permissions.AppointmentCreate,pat.PatNum,
						AptCur.AptDateTime.ToString()+", "+AptCur.ProcDescript,
						AptCur.AptNum);
					sendHL7=true;
					isCreateAppt=true;
				}
			}
			else {
				string logEntryMessage="";
				if(AptCur.AptStatus==ApptStatus.Complete) {
					string newCarrierName1=InsPlans.GetCarrierName(AptCur.InsPlan1,PlanList);
					string newCarrierName2=InsPlans.GetCarrierName(AptCur.InsPlan2,PlanList);
					string oldCarrierName1=InsPlans.GetCarrierName(AptOld.InsPlan1,PlanList);
					string oldCarrierName2=InsPlans.GetCarrierName(AptOld.InsPlan2,PlanList);
					if(AptOld.InsPlan1!=AptCur.InsPlan1) {
						if(AptCur.InsPlan1==0) {
							logEntryMessage+="\r\nRemoved "+oldCarrierName1+" for InsPlan1";
						}
						else if(AptOld.InsPlan1==0) {
							logEntryMessage+="\r\nAdded "+newCarrierName1+" for InsPlan1";
						}
						else {
							logEntryMessage+="\r\nChanged "+oldCarrierName1+" to "+newCarrierName1+" for InsPlan1";
						}
					}
					if(AptOld.InsPlan2!=AptCur.InsPlan2) {
						if(AptCur.InsPlan2==0) {
							logEntryMessage+="\r\nRemoved "+oldCarrierName2+" for InsPlan2";
						}
						else if(AptOld.InsPlan2==0) {
							logEntryMessage+="\r\nAdded "+newCarrierName2+" for InsPlan2";
						}
						else {
							logEntryMessage+="\r\nChanged "+oldCarrierName2+" to "+newCarrierName2+" for InsPlan2";
						}
					}
				}
				if(AptOld.AptStatus!=ApptStatus.Complete) { //seperate log entry for completed appointments
					SecurityLogs.MakeLogEntry(Permissions.AppointmentEdit,pat.PatNum,
					AptCur.AptDateTime.ToShortDateString()+", "+AptCur.ProcDescript+logEntryMessage,AptCur.AptNum);
				}
				else {
					SecurityLogs.MakeLogEntry(Permissions.AppointmentCompleteEdit,pat.PatNum,
					AptCur.AptDateTime.ToShortDateString()+", "+AptCur.ProcDescript+logEntryMessage,AptCur.AptNum);
				}
				sendHL7=true;
			}
			//If there is an existing HL7 def enabled, send a SIU message if there is an outbound SIU message defined
			if(sendHL7 && HL7Defs.IsExistingHL7Enabled()) {
				//S14 - Appt Modification event, S12 - New Appt Booking event
				MessageHL7 messageHL7=null;
				if(isCreateAppt) {
					messageHL7=MessageConstructor.GenerateSIU(pat,fam.GetPatient(pat.Guarantor),EventTypeHL7.S12,AptCur);
				}
				else {
					messageHL7=MessageConstructor.GenerateSIU(pat,fam.GetPatient(pat.Guarantor),EventTypeHL7.S14,AptCur);
				}
				//Will be null if there is no outbound SIU message defined, so do nothing
				if(messageHL7!=null) {
					HL7Msg hl7Msg=new HL7Msg();
					hl7Msg.AptNum=AptCur.AptNum;
					hl7Msg.HL7Status=HL7MessageStatus.OutPending;//it will be marked outSent by the HL7 service.
					hl7Msg.MsgText=messageHL7.ToString();
					hl7Msg.PatNum=pat.PatNum;
					HL7Msgs.Insert(hl7Msg);
#if DEBUG
					MessageBox.Show(this,messageHL7.ToString());
#endif
				}
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormApptEdit_FormClosing(object sender,FormClosingEventArgs e) {
			//Do not use pat.PatNum here.  Use AptCur.PatNum instead.  Pat will be null in the case that the user does not have the appt create permission.
			if(DialogResult!=DialogResult.OK) {
				if(AptCur.AptStatus==ApptStatus.Complete) {
					for(int i=0;i<_listProcs.Count;i++) {
						bool attached=false;
						if(AptCur.AptStatus==ApptStatus.Planned && _listProcs[i].PlannedAptNum==AptCur.AptNum) {
							attached=true;
						}
						else if(_listProcs[i].AptNum==AptCur.AptNum) {
							attached=true;
						}
						if(_listProcs[i].ProcStatus!=ProcStat.TP || !attached) {
							continue;
						}
						if(!Security.IsAuthorized(Permissions.AppointmentCompleteEdit,true)) {
							continue;
						}
						MsgBox.Show(this,"Detach treatment planned procedures or click OK in the appointment edit window to set them complete.");
						e.Cancel=true;
						return;
					}
				}
				if(IsNew) {
					SecurityLogs.MakeLogEntry(Permissions.AppointmentEdit,AptCur.PatNum,
						"Create cancel for date/time: "+AptCur.AptDateTime.ToString(),
						AptCur.AptNum);
					//If cancel was pressed we want to un-do any changes to other appointments that were done.
					_listAppointments=Appointments.GetListForPat(AptCur.PatNum);
					//Now we also have to remove the appointment that was pre-inserted and is in this list as well so it is deleted on sync.
					for(int i=0;i<_listAppointments.Count;i++) {
						if(_listAppointments[i].AptNum==AptCur.AptNum) {
							_listAppointments.RemoveAt(i);
							break;
						}
					}
				}
				else {  //User clicked cancel on an existing appt
					AptCur=AptOld.Clone();  //We do not want to save any other changes made in this form.
				}
			}
			else {//DialogResult==DialogResult.OK (User clicked OK or Delete)
				Procedures.Sync(_listProcs,AptCur);
			}
			//Sync detaches any attached procedures within Appointments.Delete() but doesn't create any ApptComm items.
			Appointments.Sync(_listAppointments,_listAppointmentsOld,AptCur.PatNum);
			//Synch the recalls for this patient.  This is necessary in case the date of the appointment has change or has been deleted entirely.
			Recalls.Synch(AptCur.PatNum);
			Recalls.SynchScheduledApptFull(AptCur.PatNum);
		}
		

		

		

		

		

		

		

		

	



	}
}








