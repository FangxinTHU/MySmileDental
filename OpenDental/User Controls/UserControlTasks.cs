using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;
using System.Linq;

namespace OpenDental {
	public partial class UserControlTasks:UserControl {
		///<summary>List of all TastLists that are to be displayed in the main window. Combine with TasksList.</summary>
		private List<TaskList> TaskListsList;
		///<summary>List of all Tasks that are to be displayed in the main window.  Combine with TaskListsList.</summary>
		private List<Task> TasksList;
		//<Summary>Only used if viewing user tab.  This is a list of all task lists in the general tab.  It is used to generate full path heirarchy info for each task list in the user tab.</Summary>
		//private List<TaskList> TaskListsAllGeneral;
		///<summary>An arraylist of TaskLists beginning from the trunk and adding on branches.  If the count is 0, then we are in the trunk of one of the five categories.  The last TaskList in the TreeHistory is the one that is open in the main window.</summary>
		private List<TaskList> TreeHistory;
		///<summary>A TaskList that is on the 'clipboard' waiting to be pasted.  Will be null if nothing has been copied yet.</summary>
		private TaskList ClipTaskList;
		///<summary>A Task that is on the 'clipboard' waiting to be pasted.  Will be null if nothing has been copied yet.</summary>
		private Task ClipTask;
		///<summary>If there is an item on our 'clipboard', this tracks whether it was cut.</summary>
		private bool WasCut;
		///<summary>The index of the last clicked item in the main list.</summary>
		private int clickedI;
		///<summary>After closing, if this is not zero, then it will jump to the object specified in GotoKeyNum.</summary>
		public TaskObjectType GotoType;
		///<summary>After closing, if this is not zero, then it will jump to the specified patient.</summary>
		public long GotoKeyNum;
		///<summary></summary>
		[Category("Property Changed"),Description("Event raised when user wants to go to a patient or related object.")]
		public event EventHandler GoToChanged=null;
		///<summary>All notes for the showing tasks, ordered by date time.</summary>
		private List<TaskNote> TaskNoteList;
		private const int _TriageListNum=1697;

		public UserControlTasks() {
			InitializeComponent();
			//this.listMain.ContextMenu = this.menuEdit;
			//Lan.F(this);
			Lan.C(this,new Control[] {
				checkShowFinished,
				gridMain,
				labelStartDate,
			});
			Lan.C(this,menuEdit);
			this.gridMain.ContextMenu=this.menuEdit;
		}

		///<summary>The parent might call this if it gets a signal that a relevant task was added from another workstation.  The parent should only call this if it has been verified that there is a change to tasks.</summary>
		public void RefreshTasks(){
			FillGrid();
		}

		protected void OnGoToChanged() {
			if(GoToChanged!=null) {
				GoToChanged(this,new EventArgs());
			}
		}

		///<summary>And resets the tabs if the user changes.</summary>
		public void InitializeOnStartup(){
			if(Security.CurUser==null) {
				return;
			}
			tabUser.Text=Lan.g(this,"for ")+Security.CurUser.UserName;
			tabNew.Text=Lan.g(this,"New for ")+Security.CurUser.UserName;
			if(PrefC.GetBool(PrefName.TasksShowOpenTickets)) {
				if(!tabContr.TabPages.Contains(tabOpenTickets)) {
					tabContr.TabPages.Insert(2,tabOpenTickets);
				}
			}
			else{
				if(tabContr.TabPages.Contains(tabOpenTickets)) {
					tabContr.TabPages.Remove(tabOpenTickets);
				}
			}
			LayoutToolBar();
			if(Tasks.LastOpenList==null) {//first time openning
				TreeHistory=new List<TaskList>();
				cal.SelectionStart=DateTimeOD.Today;
			}
			else {//reopening
				tabContr.SelectedIndex=Tasks.LastOpenGroup;
				TreeHistory=new List<TaskList>();
				//for(int i=0;i<Tasks.LastOpenList.Count;i++) {
				//	TreeHistory.Add(((TaskList)Tasks.LastOpenList[i]).Copy());
				//}
				cal.SelectionStart=Tasks.LastOpenDate;
			}
			FillTree();
			FillGrid();
			if(tabContr.SelectedTab!=tabOpenTickets) {//because it will have alread been set
				SetOpenTicketTab(-1);
			}
			SetMenusEnabled();
		}

		///<summary>Called whenever OpenTicket tab is refreshed to set the count at the top.  Also called from InitializeOnStartup.  In that case, we don't know what the count should be, so we pass in a -1.</summary>
		private void SetOpenTicketTab(int countSet) {
			if(!tabContr.TabPages.Contains(tabOpenTickets)) {
				return;
			}
			if(countSet==-1) {
				countSet=Tasks.GetCountOpenTickets(Security.CurUser.UserNum);
			}
			tabOpenTickets.Text=Lan.g(this,"Open Tickets")+" ("+countSet.ToString()+")";
		}

		public void ClearLogOff() {
			tabUser.Text="for";
			tabNew.Text="New for";
			TreeHistory=new List<TaskList>();
			FillTree();
			gridMain.Rows.Clear();
			gridMain.Invalidate();
		}

		///<summary>Used by OD HQ.</summary>
		public void FillGridWithTriageList() {
			TaskList tlOne=TaskLists.GetOne(_TriageListNum);
			tabContr.SelectedTab=tabMain;
			if(TreeHistory==null) {
				TreeHistory=new List<TaskList>();
			}
			TreeHistory.Clear();
			TreeHistory.Add(tlOne);
			if(TaskListsList==null) {
				TaskListsList=new List<TaskList>();
			}
			TaskListsList.Clear();
			if(TasksList==null) {
				TasksList=new List<Task>();
			}
			TasksList.Clear();
			TasksList=Tasks.RefreshChildren(_TriageListNum,false,cal.SelectionStart,Security.CurUser.UserNum,0);
			FillTree();
			FillGrid();
		}

		private void UserControlTasks_Load(object sender,System.EventArgs e) {
			if(this.DesignMode){
				return;
			}
			if(!PrefC.GetBool(PrefName.TaskAncestorsAllSetInVersion55)) {
				if(!MsgBox.Show(this,true,"A one-time routine needs to be run.  It will take a few minutes.  Do you have time right now?")){
					return;
				}
				Cursor=Cursors.WaitCursor;
				TaskAncestors.SynchAll();
				Prefs.UpdateBool(PrefName.TaskAncestorsAllSetInVersion55,true);
				DataValid.SetInvalid(InvalidType.Prefs);
				Cursor=Cursors.Default;
			}
		}

		///<summary></summary>
		public void LayoutToolBar() {
			ToolBarMain.Buttons.Clear();
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Setup"),-1,"","Setup"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Add TaskList"),0,"","AddList"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Add Task"),1,"","AddTask"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Search"),-1,"","Search"));
			ODToolBarButton button=new ODToolBarButton();
			button.Style=ODToolBarButtonStyle.ToggleButton;
			button.Text=Lan.g(this,"BlockSubsc");
			button.ToolTipText=Lan.g(this,"Popups will be blocked for tasks sent to subscribed task lists.");
			button.Tag="BlockSubsc";
			button.Pushed=Security.CurUser.DefaultHidePopups;
			ToolBarMain.Buttons.Add(button);
			ODToolBarButton InboxButton=new ODToolBarButton();
			InboxButton.Style=ODToolBarButtonStyle.ToggleButton;
			InboxButton.Text=Lan.g(this,"BlockInbox");
			InboxButton.ToolTipText=Lan.g(this,"Popups will be blocked for tasks sent to user's personal inbox.");
			InboxButton.Tag="BlockInbox";
			InboxButton.Pushed=Security.CurUser.InboxHidePopups;
			ToolBarMain.Buttons.Add(InboxButton);
			ToolBarMain.Invalidate();
		}

		private void FillTree() {
			tree.Nodes.Clear();
			TreeNode node;
			//TreeNode lastNode=null;
			string nodedesc;
			for(int i=0;i<TreeHistory.Count;i++) {
				nodedesc=TreeHistory[i].Descript;
				if(tabContr.SelectedTab==tabUser) {
					nodedesc=TreeHistory[i].ParentDesc+nodedesc;
				}
				node=new TreeNode(nodedesc);
				node.Tag=TreeHistory[i].TaskListNum;
				if(tree.SelectedNode==null) {
					tree.Nodes.Add(node);
				}
				else {
					tree.SelectedNode.Nodes.Add(node);
				}
				tree.SelectedNode=node;
			}
			//remember this position for the next time we open tasks
			Tasks.LastOpenList=new ArrayList();
			for(int i=0;i<TreeHistory.Count;i++) {
				Tasks.LastOpenList.Add(TreeHistory[i].Copy());
			}
			Tasks.LastOpenGroup=tabContr.SelectedIndex;
			Tasks.LastOpenDate=cal.SelectionStart;
			//layout
			if(tabContr.SelectedTab==tabUser) {
				tree.Top=tabContr.Bottom;
			}
			else if(tabContr.SelectedTab==tabNew) {
				tree.Top=tabContr.Bottom;
			}
			else if(tabContr.SelectedTab==tabMain) {
				tree.Top=tabContr.Bottom;
			}
			else if(tabContr.SelectedTab==tabRepeating) {
				tree.Top=tabContr.Bottom;
			}
			else if(tabContr.SelectedTab==tabDate || tabContr.SelectedTab==tabWeek || tabContr.SelectedTab==tabMonth) {
				tree.Top=cal.Bottom+1;
			}
			tree.Height=TreeHistory.Count*tree.ItemHeight+8;
			tree.Refresh();
			gridMain.Top=tree.Bottom;
			checkShowFinished.Top=gridMain.Top+1;
			textStartDate.Top=gridMain.Top;
			labelStartDate.Top=gridMain.Top+1;
		}

		private void textStartDate_KeyPress(object sender,KeyPressEventArgs e) {
			//FillGrid();
		}

		private void textStartDate_TextChanged(object sender,EventArgs e) {
			//Prevent FillGrid() from updating too frequently.
			timerDoneTaskListRefresh.Stop();
			timerDoneTaskListRefresh.Start();
		}

		private void FillGrid(){
			if(Security.CurUser==null) {
				gridMain.BeginUpdate();
				gridMain.Rows.Clear();
				gridMain.EndUpdate();
				return;
			}
			long parent;
			DateTime date;
			if(TreeHistory==null){
				return;
			}
			Cursor=Cursors.WaitCursor;
			if(TreeHistory.Count>0) {//not on main trunk
				parent=TreeHistory[TreeHistory.Count-1].TaskListNum;
				date=DateTime.MinValue;
			}
			else {//one of the main trunks
				parent=0;
				date=cal.SelectionStart;
			}
			gridMain.Height=this.ClientSize.Height-gridMain.Top-3;
			RefreshMainLists(parent,date);
			#region dated trunk automation
			//dated trunk automation-----------------------------------------------------------------------------
			if(TreeHistory.Count==0//main trunk
				&& (tabContr.SelectedTab==tabDate || tabContr.SelectedTab==tabWeek || tabContr.SelectedTab==tabMonth))
			{
				//clear any lists which are derived from a repeating list and which do not have any itmes checked off
				bool changeMade=false;
				for(int i=0;i<TaskListsList.Count;i++) {
					if(TaskListsList[i].FromNum==0) {//ignore because not derived from a repeating list
						continue;
					}
					if(!AnyAreMarkedComplete(TaskListsList[i])) {
						DeleteEntireList(TaskListsList[i]);
						changeMade=true;
					}
				}
				//clear any tasks which are derived from a repeating task 
				//and which are still new (not marked viewed or done)
				for(int i=0;i<TasksList.Count;i++) {
					if(TasksList[i].FromNum==0) {
						continue;
					}
					if(TasksList[i].TaskStatus==TaskStatusEnum.New) {
						Tasks.Delete(TasksList[i].TaskNum);
						changeMade=true;
					}
				}
				if(changeMade) {
					RefreshMainLists(parent,date);
				}
				//now add back any repeating lists and tasks that meet the criteria
				//Get lists of all repeating lists and tasks of one type.  We will pick items from these two lists.
				List<TaskList> repeatingLists=new List<TaskList>();
				List<Task> repeatingTasks=new List<Task>();
				if(tabContr.SelectedTab==tabDate){
					repeatingLists=TaskLists.RefreshRepeating(TaskDateType.Day);
					repeatingTasks=Tasks.RefreshRepeating(TaskDateType.Day,Security.CurUser.UserNum);
				}
				if(tabContr.SelectedTab==tabWeek){
					repeatingLists=TaskLists.RefreshRepeating(TaskDateType.Week);
					repeatingTasks=Tasks.RefreshRepeating(TaskDateType.Week,Security.CurUser.UserNum);
				}
				if(tabContr.SelectedTab==tabMonth) {
					repeatingLists=TaskLists.RefreshRepeating(TaskDateType.Month);
					repeatingTasks=Tasks.RefreshRepeating(TaskDateType.Month,Security.CurUser.UserNum);
				}
				//loop through list and add back any that meet criteria.
				changeMade=false;
				bool alreadyExists;
				for(int i=0;i<repeatingLists.Count;i++) {
					//if already exists, skip
					alreadyExists=false;
					for(int j=0;j<TaskListsList.Count;j++) {//loop through Main list
						if(TaskListsList[j].FromNum==repeatingLists[i].TaskListNum) {
							alreadyExists=true;
							break;
						}
					}
					if(alreadyExists) {
						continue;
					}
					//otherwise, duplicate the list
					repeatingLists[i].DateTL=date;
					repeatingLists[i].FromNum=repeatingLists[i].TaskListNum;
					repeatingLists[i].IsRepeating=false;
					repeatingLists[i].Parent=0;
					repeatingLists[i].ObjectType=0;//user will have to set explicitly
					DuplicateExistingList(repeatingLists[i],null,true);//repeating lists cannot be subscribed to, so send null in as old list, will not attempt to move subscriptions
					changeMade=true;
				}
				for(int i=0;i<repeatingTasks.Count;i++) {
					//if already exists, skip
					alreadyExists=false;
					for(int j=0;j<TasksList.Count;j++) {//loop through Main list
						if(TasksList[j].FromNum==repeatingTasks[i].TaskNum) {
							alreadyExists=true;
							break;
						}
					}
					if(alreadyExists) {
						continue;
					}
					//otherwise, duplicate the task
					repeatingTasks[i].DateTask=date;
					repeatingTasks[i].FromNum=repeatingTasks[i].TaskNum;
					repeatingTasks[i].IsRepeating=false;
					repeatingTasks[i].TaskListNum=0;
					//repeatingTasks[i].UserNum//repeating tasks shouldn't get a usernum
					Tasks.Insert(repeatingTasks[i]);
					changeMade=true;
				}
				if(changeMade) {
					RefreshMainLists(parent,date);
				}
			}//End of dated trunk automation--------------------------------------------------------------------------
			#endregion dated trunk automation
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn("",17);
			col.ImageList=imageListTree;
			gridMain.Columns.Add(col);
			if(tabContr.SelectedTab==tabNew && !PrefC.GetBool(PrefName.TasksNewTrackedByUser)) {//The old way
				col=new ODGridColumn(Lan.g("TableTasks","Read"),35,HorizontalAlignment.Center);
				//col.ImageList=imageListTree;
				gridMain.Columns.Add(col);
			}
			if(tabContr.SelectedTab==tabNew || tabContr.SelectedTab==tabOpenTickets) {
				col=new ODGridColumn(Lan.g("TableTasks","Task List"),90);
				gridMain.Columns.Add(col);
			}
			col=new ODGridColumn(Lan.g("TableTasks","Description"),200);//any width
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			string dateStr="";
			string objDesc="";
			string tasklistdescript="";
			string notes="";
			int imageindex;
			for(int i=0;i<TaskListsList.Count;i++) {
				dateStr="";
				if(TaskListsList[i].DateTL.Year>1880
					&& (tabContr.SelectedTab==tabUser || tabContr.SelectedTab==tabMain))
				{
					if(TaskListsList[i].DateType==TaskDateType.Day) {
						dateStr=TaskListsList[i].DateTL.ToShortDateString()+" - ";
					}
					else if(TaskListsList[i].DateType==TaskDateType.Week) {
						dateStr=Lan.g(this,"Week of")+" "+TaskListsList[i].DateTL.ToShortDateString()+" - ";
					}
					else if(TaskListsList[i].DateType==TaskDateType.Month) {
						dateStr=TaskListsList[i].DateTL.ToString("MMMM")+" - ";
					}
				}
				objDesc="";
				if(tabContr.SelectedTab==tabUser){
					objDesc=TaskListsList[i].ParentDesc;
				}
				tasklistdescript=TaskListsList[i].Descript;
				imageindex=0;
				if(TaskListsList[i].NewTaskCount>0){
					imageindex=3;//orange
					tasklistdescript=tasklistdescript+"("+TaskListsList[i].NewTaskCount.ToString()+")";
				}
				row=new ODGridRow();
				row.Cells.Add(imageindex.ToString());
				row.Cells.Add(dateStr+objDesc+tasklistdescript);
				gridMain.Rows.Add(row);
			}
			for(int i=0;i<TasksList.Count;i++) {
				dateStr="";
				if(tabContr.SelectedTab==tabUser || tabContr.SelectedTab==tabNew
					|| tabContr.SelectedTab==tabOpenTickets || tabContr.SelectedTab==tabMain) 
				{
					if(TasksList[i].DateTask.Year>1880) {
						if(TasksList[i].DateType==TaskDateType.Day) {
							dateStr+=TasksList[i].DateTask.ToShortDateString()+" - ";
						}
						else if(TasksList[i].DateType==TaskDateType.Week) {
							dateStr+=Lan.g(this,"Week of")+" "+TasksList[i].DateTask.ToShortDateString()+" - ";
						}
						else if(TasksList[i].DateType==TaskDateType.Month) {
							dateStr+=TasksList[i].DateTask.ToString("MMMM")+" - ";
						}
					}
					else if(TasksList[i].DateTimeEntry.Year>1880) {
						dateStr+=TasksList[i].DateTimeEntry.ToShortDateString()+" "+TasksList[i].DateTimeEntry.ToShortTimeString()+" - ";
					}
				}
				objDesc="";
				if(TasksList[i].TaskStatus==TaskStatusEnum.Done){
					objDesc=Lan.g(this,"Done:")+TasksList[i].DateTimeFinished.ToShortDateString()+" - ";
				}
				if(TasksList[i].ObjectType==TaskObjectType.Patient) {
					if(TasksList[i].KeyNum!=0) {
						objDesc+=TasksList[i].PatientName+" - ";
					}
				}
				else if(TasksList[i].ObjectType==TaskObjectType.Appointment) {
					if(TasksList[i].KeyNum!=0) {
						Appointment AptCur=Appointments.GetOneApt(TasksList[i].KeyNum);
						if(AptCur!=null) {
							objDesc=Patients.GetPat(AptCur.PatNum).GetNameLF()//this is going to stay. Still not optimized, but here at HQ, we don't use it.
								+"  "+AptCur.AptDateTime.ToString()
								+"  "+AptCur.ProcDescript
								+"  "+AptCur.Note
								+" - ";
						}
					}
				}
				if(!TasksList[i].Descript.StartsWith("==") && TasksList[i].UserNum!=0) {
					objDesc+=Userods.GetName(TasksList[i].UserNum)+" - ";
				}
				notes="";
				bool hasNotes=false;
				for(int n=0;n<TaskNoteList.Count;n++) {
					if(TaskNoteList[n].TaskNum!=TasksList[i].TaskNum) {
						continue;
					}
					notes+="\r\n"//even on the first loop
						+"=="+Userods.GetName(TaskNoteList[n].UserNum)+" - "
						+TaskNoteList[n].DateTimeNote.ToShortDateString()+" "
						+TaskNoteList[n].DateTimeNote.ToShortTimeString()
						+" - "+TaskNoteList[n].Note;
					hasNotes=true;
				}
				row=new ODGridRow();
				if(PrefC.GetBool(PrefName.TasksNewTrackedByUser)) {//The new way
					if(TasksList[i].TaskStatus==TaskStatusEnum.Done) {
						row.Cells.Add("1");
					}
					else {
						if(TasksList[i].IsUnread) {
							row.Cells.Add("4");
						}
						else{
							row.Cells.Add("2");
						}
					}
				}
				else {
					switch(TasksList[i].TaskStatus) {
						case TaskStatusEnum.New:
							row.Cells.Add("4");
							break;
						case TaskStatusEnum.Viewed:
							row.Cells.Add("2");
							break;
						case TaskStatusEnum.Done:
							row.Cells.Add("1");
							break;
					}
					if(tabContr.SelectedTab==tabNew) {//In this mode, there's a extra column in this tab
						row.Cells.Add("read");
					}
				}
				if(tabContr.SelectedTab==tabNew || tabContr.SelectedTab==tabOpenTickets) {
					row.Cells.Add(TasksList[i].ParentDesc);
				}
				row.Cells.Add(dateStr+objDesc+TasksList[i].Descript+notes);
				row.ColorBackG=DefC.GetColor(DefCat.TaskPriorities,TasksList[i].PriorityDefNum);//No need to do any text detection for triage priorities, we'll just use the task priority colors.
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			gridMain.ScrollValue=gridMain.ScrollValue;//this forces scroll value to reset if it's > allowed max.
			if(tabContr.SelectedTab==tabOpenTickets) {
				SetOpenTicketTab(gridMain.Rows.Count);
			}
			Cursor=Cursors.Default;
		}

		private void checkShowFinished_Click(object sender,EventArgs e) {
			if(checkShowFinished.Checked){
				labelStartDate.Visible=true;
				textStartDate.Visible=true;
				textStartDate.Text=DateTime.Now.AddDays(-7).ToShortDateString();
			}
			else{
				labelStartDate.Visible=false;
				textStartDate.Visible=false;
			}
			FillGrid();
		}

		///<summary>A recursive function that checks every child in a list IsFromRepeating.  If any are marked complete, then it returns true, signifying that this list should be immune from being deleted since it's already in use.</summary>
		private bool AnyAreMarkedComplete(TaskList list) {
			//get all children:
			List<TaskList> childLists=TaskLists.RefreshChildren(list.TaskListNum,Security.CurUser.UserNum,0);
			List<Task> childTasks=Tasks.RefreshChildren(list.TaskListNum,true,DateTime.MinValue,Security.CurUser.UserNum,0);
			for(int i=0;i<childLists.Count;i++) {
				if(AnyAreMarkedComplete(childLists[i])) {
					return true;
				}
			}
			for(int i=0;i<childTasks.Count;i++) {
				if(childTasks[i].TaskStatus==TaskStatusEnum.Done) {
					return true;
				}
			}
			return false;
		}

		///<summary>If parent=0, then this is a trunk.</summary>
		private void RefreshMainLists(long parent,DateTime date) {
			DateTime startDate=DateTime.MinValue;
			if(textStartDate.Visible && textStartDate.Text!=""){
				if(textStartDate.errorProvider1.GetError(textStartDate)==""){
					startDate=PIn.Date(textStartDate.Text);
				}
				else{//invalid date
					startDate=DateTimeOD.Today.AddDays(-7);
				}
			}
			if(this.DesignMode){
				TaskListsList=new List<TaskList>();
				TasksList=new List<Task>();
				TaskNoteList=new List<TaskNote>();
				return;
			}
			if(parent!=0){//not a trunk
				//if(TreeHistory.Count>0//we already know this is true
				long userNumInbox=TaskLists.GetMailboxUserNum(TreeHistory[0].TaskListNum);
				TaskListsList=TaskLists.RefreshChildren(parent,Security.CurUser.UserNum,userNumInbox);
				TasksList=Tasks.RefreshChildren(parent,checkShowFinished.Checked,startDate,Security.CurUser.UserNum,userNumInbox);
			}
			else if(tabContr.SelectedTab==tabUser) {
				TaskListsList=TaskLists.RefreshUserTrunk(Security.CurUser.UserNum);
				TasksList=new List<Task>();//no tasks in the user trunk
			}
			else if(tabContr.SelectedTab==tabNew) {
				TaskListsList=new List<TaskList>();//no task lists in new tab
				TasksList=Tasks.RefreshUserNew(Security.CurUser.UserNum);
			}
			else if(tabContr.SelectedTab==tabOpenTickets) {
				TaskListsList=new List<TaskList>();//no task lists in new tab
				TasksList=Tasks.RefreshOpenTickets(Security.CurUser.UserNum);
			}
			else if(tabContr.SelectedTab==tabMain) {
				TaskListsList=TaskLists.RefreshMainTrunk(Security.CurUser.UserNum);
				TasksList=Tasks.RefreshMainTrunk(checkShowFinished.Checked,startDate,Security.CurUser.UserNum);
			}
			else if(tabContr.SelectedTab==tabRepeating) {
				TaskListsList=TaskLists.RefreshRepeatingTrunk();
				TasksList=Tasks.RefreshRepeatingTrunk();
			}
			else if(tabContr.SelectedTab==tabDate) {
				TaskListsList=TaskLists.RefreshDatedTrunk(date,TaskDateType.Day);
				TasksList=Tasks.RefreshDatedTrunk(date,TaskDateType.Day,checkShowFinished.Checked,startDate,Security.CurUser.UserNum);
			}
			else if(tabContr.SelectedTab==tabWeek) {
				TaskListsList=TaskLists.RefreshDatedTrunk(date,TaskDateType.Week);
				TasksList=Tasks.RefreshDatedTrunk(date,TaskDateType.Week,checkShowFinished.Checked,startDate,Security.CurUser.UserNum);
			}
			else if(tabContr.SelectedTab==tabMonth) {
				TaskListsList=TaskLists.RefreshDatedTrunk(date,TaskDateType.Month);
				TasksList=Tasks.RefreshDatedTrunk(date,TaskDateType.Month,checkShowFinished.Checked,startDate,Security.CurUser.UserNum);
			}
			//notes
			List<long> taskNums=new List<long>();
			for(int i=0;i<TasksList.Count;i++) {
				taskNums.Add(TasksList[i].TaskNum);
			}
			TaskNoteList=TaskNotes.RefreshForTasks(taskNums);
		}

		private void tabContr_MouseDown(object sender,MouseEventArgs e) {
			TreeHistory=new List<TaskList>();//clear the tree no matter which tab clicked.
			FillTree();
			FillGrid();
		}

		private void cal_DateSelected(object sender,System.Windows.Forms.DateRangeEventArgs e) {
			TreeHistory=new List<TaskList>();//clear the tree
			FillTree();
			FillGrid();
		}

		private void ToolBarMain_ButtonClick(object sender,OpenDental.UI.ODToolBarButtonClickEventArgs e) {
			//if(e.Button.Tag.GetType()==typeof(string)){
			//standard predefined button
			switch(e.Button.Tag.ToString()) {
				case "Setup":
					Setup_Clicked();
					break;
				case "AddList":
					AddList_Clicked();
					break;
				case "AddTask":
					AddTask_Clicked();
					break;
				case "Search":
					Search_Clicked();
					break;
				case "BlockSubsc":
					BlockSubsc_Clicked();
					break;
				case "BlockInbox":
					BlockInbox_Clicked();
					break;
			}
		}

		private void Setup_Clicked() {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormTaskSetup FormT=new FormTaskSetup();
			FormT.ShowDialog();
		}

		private void AddList_Clicked() {
			if(tabContr.SelectedTab==tabUser && TreeHistory.Count==0) {//trunk of user tab
				MsgBox.Show(this,"Not allowed to add a task list to the trunk of the user tab.  Either use the subscription feature, or add it to a child list.");
				return;
			}
			if(tabContr.SelectedTab==tabNew) {//new tab
				MsgBox.Show(this,"Not allowed to add items to the 'New' tab.");
				return;
			}
			TaskList cur=new TaskList();
			//if this is a child of any other taskList
			if(TreeHistory.Count>0) {
				cur.Parent=TreeHistory[TreeHistory.Count-1].TaskListNum;
			}
			else {
				cur.Parent=0;
				if(tabContr.SelectedTab==tabDate) {
					cur.DateTL=cal.SelectionStart;
					cur.DateType=TaskDateType.Day;
				}
				else if(tabContr.SelectedTab==tabWeek) {
					cur.DateTL=cal.SelectionStart;
					cur.DateType=TaskDateType.Week;
				}
				else if(tabContr.SelectedTab==tabMonth) {
					cur.DateTL=cal.SelectionStart;
					cur.DateType=TaskDateType.Month;
				}
			}
			if(tabContr.SelectedTab==tabRepeating) {
				cur.IsRepeating=true;
			}
			FormTaskListEdit FormT=new FormTaskListEdit(cur);
			FormT.IsNew=true;
			FormT.ShowDialog();
			FillGrid();
		}

		private void AddTask_Clicked() {
			if(Plugins.HookMethod(this,"UserControlTasks.AddTask_Clicked")) {
				return;
			}
			//if(tabContr.SelectedTab==tabUser && TreeHistory.Count==0) {//trunk of user tab
			//	MsgBox.Show(this,"Not allowed to add a task to the trunk of the user tab.  Add it to a child list instead.");
			//	return;
			//}
			//if(tabContr.SelectedTab==tabNew) {//new tab
			//	MsgBox.Show(this,"Not allowed to add items to the 'New' tab.");
			//	return;
			//}
			Task task=new Task();
			task.TaskListNum=-1;//don't show it in any list yet.
			Tasks.Insert(task);
			Task taskOld=task.Copy();
			//if this is a child of any taskList
			if(TreeHistory.Count>0) {
				task.TaskListNum=TreeHistory[TreeHistory.Count-1].TaskListNum;
			}
			else if(tabContr.SelectedTab==tabNew) {//new tab
				task.TaskListNum=-1;
			}
			else if(tabContr.SelectedTab==tabUser && TreeHistory.Count==0) {//trunk of user tab
				task.TaskListNum=-1;
			}
			else {
				task.TaskListNum=0;
				if(tabContr.SelectedTab==tabDate) {
					task.DateTask=cal.SelectionStart;
					task.DateType=TaskDateType.Day;
				}
				else if(tabContr.SelectedTab==tabWeek) {
					task.DateTask=cal.SelectionStart;
					task.DateType=TaskDateType.Week;
				}
				else if(tabContr.SelectedTab==tabMonth) {
					task.DateTask=cal.SelectionStart;
					task.DateType=TaskDateType.Month;
				}
			}
			if(tabContr.SelectedTab==tabRepeating) {
				task.IsRepeating=true;
			}
			task.UserNum=Security.CurUser.UserNum;
			FormTaskEdit FormT=new FormTaskEdit(task,taskOld);
			FormT.IsNew=true;
			FormT.Closing+=new CancelEventHandler(TaskGoToEvent);
			FormT.Show();//non-modal
		}

		public void Search_Clicked() {
			FormTaskSearch FormTS=new FormTaskSearch();
			FormTS.TaskGoToEvent+=TaskGoToEvent;
			FormTS.Show();
		}

		public void TaskGoToEvent(object sender,CancelEventArgs e) {
			FormTaskEdit FormT=(FormTaskEdit)sender;
			if(FormT.GotoType!=TaskObjectType.None) {
				GotoType=FormT.GotoType;
				GotoKeyNum=FormT.GotoKeyNum;
				OnGoToChanged();
			}
			if(!this.IsDisposed) {
				FillGrid();
			}
		}

		private void BlockSubsc_Clicked() {
			if(ToolBarMain.Buttons["BlockSubsc"].Pushed) {
				Security.CurUser.DefaultHidePopups=true;
			}
			else {
				Security.CurUser.DefaultHidePopups=false;
			}
			try {
				Userods.Update(Security.CurUser);
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
				return;
			}
			DataValid.SetInvalid(InvalidType.Security);
		}

		private void BlockInbox_Clicked() {
			if(ToolBarMain.Buttons["BlockInbox"].Pushed) {
				Security.CurUser.InboxHidePopups=true;
			}
			else {
				Security.CurUser.InboxHidePopups=false;
			}
			try {
				Userods.Update(Security.CurUser);
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
				return;
			}
			DataValid.SetInvalid(InvalidType.Security);
		}

		private void Done_Clicked() {
			//already blocked if list
			Task task=TasksList[clickedI-TaskListsList.Count];
			Task oldTask=task.Copy();
			task.TaskStatus=TaskStatusEnum.Done;
			if(task.DateTimeFinished.Year<1880) {
				task.DateTimeFinished=DateTime.Now;
			}
			try {
				Tasks.Update(task,oldTask);
				TaskUnreads.DeleteForTask(task.TaskNum);
				TaskHist taskHist=new TaskHist(oldTask);
				taskHist.UserNumHist=Security.CurUser.UserNum;
				TaskHists.Insert(taskHist);
				DataValid.SetInvalidTask(task.TaskNum,false);//this causes an immediate local refresh of the grid
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
				return;
			}
		}

		private void Edit_Clicked() {
			if(clickedI < TaskListsList.Count) {//is list
				FormTaskListEdit FormT=new FormTaskListEdit(TaskListsList[clickedI]);
				FormT.ShowDialog();
				FillGrid();
			}
			else {//task
				FormTaskEdit FormT=new FormTaskEdit(TasksList[clickedI-TaskListsList.Count],TasksList[clickedI-TaskListsList.Count].Copy());
				FormT.Closing+=new CancelEventHandler(TaskGoToEvent);
				FormT.Show();//non-modal
			}
		}

		private void Cut_Clicked() {
			if(clickedI < TaskListsList.Count) {//is list
				ClipTaskList=TaskListsList[clickedI].Copy();
				ClipTask=null;
			}
			else {//task
				ClipTaskList=null;
				ClipTask=TasksList[clickedI-TaskListsList.Count].Copy();
			}
			WasCut=true;
		}

		private void Copy_Clicked() {
			if(clickedI < TaskListsList.Count) {//is list
				ClipTaskList=TaskListsList[clickedI].Copy();
				ClipTask=null;
			}
			else {//task
				ClipTaskList=null;
				ClipTask=TasksList[clickedI-TaskListsList.Count].Copy();
			}
			WasCut=false;
		}

		///<summary>When cutting and pasting, Task hist will be lost because the pasted task has a new TaskNum.</summary>
		private void Paste_Clicked() {
			if(ClipTaskList!=null) {//a taskList is on the clipboard
				TaskList newTL=ClipTaskList.Copy();
				if(TreeHistory.Count>0) {//not on main trunk
					newTL.Parent=TreeHistory[TreeHistory.Count-1].TaskListNum;
					if(tabContr.SelectedTab==tabUser){
						//treat pasting just like it's the main tab, because not on the trunk.
					}
					else if(tabContr.SelectedTab==tabMain){
						//even though usually only trunks are dated, we will leave the date alone in main
						//category since user may wish to preserve it. All other children get date cleared.
					}
					else if(tabContr.SelectedTab==tabRepeating){
						newTL.DateTL=DateTime.MinValue;//never a date
						//leave dateType alone, since that affects how it repeats
					}
					else if(tabContr.SelectedTab==tabDate
						|| tabContr.SelectedTab==tabWeek
						|| tabContr.SelectedTab==tabMonth) 
					{
						newTL.DateTL=DateTime.MinValue;//children do not get dated
						newTL.DateType=TaskDateType.None;//this doesn't matter either for children
					}
				}
				else {//one of the main trunks
					newTL.Parent=0;
					if(tabContr.SelectedTab==tabUser) {
						//maybe we should treat this like a subscription rather than a paste.  Implement later.  For now:
						MsgBox.Show(this,"Not allowed to paste directly to the trunk of this tab.  Try using the subscription feature instead.");
						return;
					}
					else if(tabContr.SelectedTab==tabMain) {
						newTL.DateTL=DateTime.MinValue;
						newTL.DateType=TaskDateType.None;
					}
					else if(tabContr.SelectedTab==tabRepeating) {
						newTL.DateTL=DateTime.MinValue;//never a date
						//newTL.DateType=TaskDateType.None;//leave alone
					}
					else if(tabContr.SelectedTab==tabDate){
						newTL.DateTL=cal.SelectionStart;
						newTL.DateType=TaskDateType.Day;
					}
					else if(tabContr.SelectedTab==tabWeek) {
						newTL.DateTL=cal.SelectionStart;
						newTL.DateType=TaskDateType.Week;
					}
					else if(tabContr.SelectedTab==tabMonth) {
						newTL.DateTL=cal.SelectionStart;
						newTL.DateType=TaskDateType.Month;
					}
				}
				if(tabContr.SelectedTab==tabRepeating) {
					newTL.IsRepeating=true;
				}
				else {
					newTL.IsRepeating=false;
				}
				newTL.FromNum=0;//always
				//oldList used to move subscriptions to task list if Cut/Paste. If null, subscriptions will be deleted.
				TaskList oldList=null;
				if(WasCut) {
					oldList=ClipTaskList.Copy();
				}				
				if(tabContr.SelectedTab==tabUser || tabContr.SelectedTab==tabMain) {
					DuplicateExistingList(newTL,oldList,true);
				}
				else {
					DuplicateExistingList(newTL,oldList,false);
				}
				DataValid.SetInvalid(InvalidType.Task);
			}
			if(ClipTask!=null) {//a task is on the clipboard
				Task newT=ClipTask.Copy();
				if(TreeHistory.Count>0) {//not on main trunk
					newT.TaskListNum=TreeHistory[TreeHistory.Count-1].TaskListNum;
					if(tabContr.SelectedTab==tabUser) {
						//treat pasting just like it's the main tab, because not on the trunk.
					}
					else if(tabContr.SelectedTab==tabMain) {
						//even though usually only trunks are dated, we will leave the date alone in main
						//category since user may wish to preserve it. All other children get date cleared.
					}
					else if(tabContr.SelectedTab==tabRepeating) {
						newT.DateTask=DateTime.MinValue;//never a date
						//leave dateType alone, since that affects how it repeats
					}
					else if(tabContr.SelectedTab==tabDate
						|| tabContr.SelectedTab==tabWeek
						|| tabContr.SelectedTab==tabMonth) 
					{
						newT.DateTask=DateTime.MinValue;//children do not get dated
						newT.DateType=TaskDateType.None;//this doesn't matter either for children
					}
				}
				else {//one of the main trunks
					newT.TaskListNum=0;
					if(tabContr.SelectedTab==tabUser) {
						//never allowed to have a task on the user trunk.
						MsgBox.Show(this,"Tasks may not be pasted directly to the trunk of this tab.  Try pasting within a list instead.");
						return;
					}
					else if(tabContr.SelectedTab==tabMain) {
						newT.DateTask=DateTime.MinValue;
						newT.DateType=TaskDateType.None;
					}
					else if(tabContr.SelectedTab==tabRepeating) {
						newT.DateTask=DateTime.MinValue;//never a date
						//newTL.DateType=TaskDateType.None;//leave alone
					}
					else if(tabContr.SelectedTab==tabDate) {
						newT.DateTask=cal.SelectionStart;
						newT.DateType=TaskDateType.Day;
					}
					else if(tabContr.SelectedTab==tabWeek) {
						newT.DateTask=cal.SelectionStart;
						newT.DateType=TaskDateType.Week;
					}
					else if(tabContr.SelectedTab==tabMonth) {
						newT.DateTask=cal.SelectionStart;
						newT.DateType=TaskDateType.Month;
					}
				}
				if(tabContr.SelectedTab==tabRepeating) {
					newT.IsRepeating=true;
				}
				else {
					newT.IsRepeating=false;
				}
				newT.FromNum=0;//always
				if(WasCut && Tasks.WasTaskAltered(ClipTask)){
					MsgBox.Show("Tasks","Not allowed to move because the task has been altered by someone else.");
					FillGrid();
					return;
				}
				List<TaskNote> noteList=TaskNotes.GetForTask(newT.TaskNum);
				long newTaskNum=Tasks.Insert(newT);
				TaskHist hist=new TaskHist(newT);
				if(WasCut) {
					//Reassign old taskHist.TaskNums
					TaskHists.UpdateTaskNums(ClipTask,newT);
					hist.Descript="This task was cut from task list "+TaskLists.GetFullPath(ClipTask.TaskListNum);
				}
				else { 
					hist.Descript="This task was copied from task "+ClipTask.TaskNum+" in task list "+TaskLists.GetFullPath(ClipTask.TaskListNum);
				}
				TaskHists.Insert(hist);
				for(int t=0;t<noteList.Count;t++) {
					noteList[t].TaskNum=newTaskNum;
					TaskNotes.Insert(noteList[t]);//Creates the new note with the current datetime stamp.
					TaskNotes.Update(noteList[t]);//Restores the historical datetime for the note.
				}
				DataValid.SetInvalidTask(newT.TaskNum,true);
			}
			if(WasCut) {
				if(ClipTaskList!=null) {
					DeleteEntireList(ClipTaskList);
				}
				else if(ClipTask!=null) {
					Tasks.Delete(ClipTask.TaskNum);
					DataValid.SetInvalidTask(ClipTask.TaskNum,false);//this causes an immediate local refresh of the grid
				}
			}
		}

		private void SendToMe_Clicked() {
			if(Security.CurUser.TaskListInBox==0) {
				MsgBox.Show(this,"You do not have an inbox.");
				return;
			}
			Task oldTask;
			Task task;
			try {
				//If a user right clicks on a task and then the task list is updated soon after, it is possible that clickedI-TaskListsList.Count will be an index out bounds.
				//This try catch simply solves the one scenario where the user right clicked on the last task in the list, the list auto-refreshed and the task was moved, then send to me was clicked.
				//This fix does not fix the root of the problem because a user could right click on a task in the middle of the list which will cause a different task right to be sent.
				//Because this is such a rare bug and no customers are complaining about it (yet), Jordan has decided to only fix the potential UE because the correct fix is very time consuming.
				oldTask=TasksList[clickedI-TaskListsList.Count];
				task=oldTask.Copy();
				task.TaskListNum=Security.CurUser.TaskListInBox;
			}
			catch {
				MsgBox.Show(this,"Not allowed to save changes because the task has been altered by someone else.");
				return;
			}
			try {
				Tasks.Update(task,oldTask);
				//At HQ the refresh interval wasn't quick enough for the task to pop up.
				//We will immediately show the task instead of waiting for the refresh interval.
				TaskHist taskHist=new TaskHist(oldTask);
				taskHist.UserNumHist=Security.CurUser.UserNum;
				TaskHists.Insert(taskHist);
				DataValid.SetInvalidTask(task.TaskNum,false);
				FormTaskEdit FormT=new FormTaskEdit(task,task.Copy());
				FormT.IsPopup=true;
				FormT.Closing+=new CancelEventHandler(TaskGoToEvent);
				FormT.Show();//non-modal
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
				return;
			}
		}

		private void Goto_Clicked() {
			//not even allowed to get to this point unless a valid task
			Task task=TasksList[clickedI-TaskListsList.Count];
			GotoType=task.ObjectType;
			GotoKeyNum=task.KeyNum;
			OnGoToChanged();
		}

		///<summary>A recursive function that duplicates an entire existing TaskList.  For the initial loop, make changes to the original taskList before passing it in.  That way, Date and type are only set in initial loop.  All children preserve original dates and types.  The isRepeating value will be applied in all loops.  Also, make sure to change the parent num to the new one before calling this function.  The taskListNum will always change, because we are inserting new record into database.  Sending null in for oldList will cause all existing subscriptions to the old list to be deleted.  In order for the new list to have the same subscriptions as the old list (as in a Cut/Paste), send in the old task list.</summary>
		private void DuplicateExistingList(TaskList newList,TaskList oldList,bool isInMainOrUser) {
			//get all children:
			List<TaskList> childLists=TaskLists.RefreshChildren(newList.TaskListNum,Security.CurUser.UserNum,0);
			List<Task> childTasks=Tasks.RefreshChildren(newList.TaskListNum,true,DateTime.MinValue,Security.CurUser.UserNum,0);
			TaskLists.Insert(newList);
			if(oldList!=null) {
				TaskSubscriptions.UpdateAllSubs(oldList.TaskListNum,newList.TaskListNum);
			}
			//now we have a new taskListNum to work with
			for(int i=0;i<childLists.Count;i++) {
				childLists[i].Parent=newList.TaskListNum;
				if(newList.IsRepeating) {
					childLists[i].IsRepeating=true;
					childLists[i].DateTL=DateTime.MinValue;//never a date
				}
				else {
					childLists[i].IsRepeating=false;
				}
				childLists[i].FromNum=0;
				if(!isInMainOrUser) {
					childLists[i].DateTL=DateTime.MinValue;
					childLists[i].DateType=TaskDateType.None;
				}
				if(oldList==null) {
					DuplicateExistingList(childLists[i],null,isInMainOrUser);//delete any existing subscriptions
				}
				else {
					DuplicateExistingList(childLists[i],childLists[i],isInMainOrUser);
				}
			}
			for(int i=0;i<childTasks.Count;i++) {
				childTasks[i].TaskListNum=newList.TaskListNum;
				if(newList.IsRepeating) {
					childTasks[i].IsRepeating=true;
					childTasks[i].DateTask=DateTime.MinValue;//never a date
				}
				else {
					childTasks[i].IsRepeating=false;
				}
				childTasks[i].FromNum=0;
				if(!isInMainOrUser) {
					childTasks[i].DateTask=DateTime.MinValue;
					childTasks[i].DateType=TaskDateType.None;
				}
				List<TaskNote> noteList=TaskNotes.GetForTask(childTasks[i].TaskNum);
				long newTaskNum=Tasks.Insert(childTasks[i]);
				for(int t=0;t<noteList.Count;t++) {
					noteList[t].TaskNum=newTaskNum;
					TaskNotes.Insert(noteList[t]);//Creates the new note with the current datetime stamp.
					TaskNotes.Update(noteList[t]);//Restores the historical datetime for the note.
				}
			}
		}

		private void Delete_Clicked() {
			if(clickedI < TaskListsList.Count) {//is list
				//check to make sure the list is empty.
				List<Task> tsks=Tasks.RefreshChildren(TaskListsList[clickedI].TaskListNum,true,DateTime.MinValue,Security.CurUser.UserNum,0);
				List<TaskList> tsklsts=TaskLists.RefreshChildren(TaskListsList[clickedI].TaskListNum,Security.CurUser.UserNum,0);
				if(tsks.Count>0 || tsklsts.Count>0){
					MsgBox.Show(this,"Not allowed to delete a list unless it's empty.");
					return;
				}
				if(TaskLists.GetMailboxUserNum(TaskListsList[clickedI].TaskListNum)!=0) {
					MsgBox.Show(this,"Not allowed to delete task list because it is attached to a user inbox.");
					return;
				}
				if(!MsgBox.Show(this,true,"Delete this empty list?")) {
					return;
				}
				TaskSubscriptions.UpdateAllSubs(TaskListsList[clickedI].TaskListNum,0);
				TaskLists.Delete(TaskListsList[clickedI]);
				//DeleteEntireList(TaskListsList[clickedI]);
				DataValid.SetInvalid(InvalidType.Task);
			}
			else {//Is task
				Task task=TasksList[clickedI-TaskListsList.Count];
				bool isTaskForCurUser=true;
				if(task.UserNum!=Security.CurUser.UserNum) {//current user didn't write this task, so block them.
					isTaskForCurUser=false;//Delete will only be allowed if the user has the TaskEdit and TaskNoteEdit permissions.
				}
				else if(task.TaskListNum!=Security.CurUser.TaskListInBox) {//the task is not in the logged-in user's inbox
					isTaskForCurUser=false;
				}
				if(isTaskForCurUser) {//this just allows getting the Task Notes less often
					//Check to see if other users have added notes
					isTaskForCurUser=TaskNotes.GetForTask(task.TaskNum).All(x => x.UserNum==Security.CurUser.UserNum);
				}
				if(!isTaskForCurUser && (!Security.IsAuthorized(Permissions.TaskEdit) || !Security.IsAuthorized(Permissions.TaskNoteEdit))) {
					return;
				}
				if(!MsgBox.Show(this,true,"Delete?")) {
					return;
				}
				Tasks.Delete(task.TaskNum);
				DataValid.SetInvalidTask(task.TaskNum,false);
			}
			//FillGrid();
		}

		///<summary>A recursive function that deletes the specified list and all children.</summary>
		private void DeleteEntireList(TaskList list) {
			//get all children:
			List<TaskList> childLists=TaskLists.RefreshChildren(list.TaskListNum,Security.CurUser.UserNum,0);
			List<Task> childTasks=Tasks.RefreshChildren(list.TaskListNum,true,DateTime.MinValue,Security.CurUser.UserNum,0);
			for(int i=0;i<childLists.Count;i++) {
				DeleteEntireList(childLists[i]);
			}
			for(int i=0;i<childTasks.Count;i++) {
				Tasks.Delete(childTasks[i].TaskNum);
			}
			try {
				TaskLists.Delete(list);
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
			}
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			if(e.Col==0){
				//no longer allow double click on checkbox, because it's annoying.
				return;
			}
			if(e.Row >= TaskListsList.Count) {//is task
				//It's important to grab the task directly from the db because the status in this list is fake, being the "unread" status instead.
				Task task=Tasks.GetOne(TasksList[e.Row-TaskListsList.Count].TaskNum);
				FormTaskEdit FormT=new FormTaskEdit(task,task.Copy());
				FormT.Closing+=new CancelEventHandler(TaskGoToEvent);
				FormT.Show();//non-modal
			}
		}

		private void gridMain_MouseDown(object sender,MouseEventArgs e) {
			clickedI=gridMain.PointToRow(e.Y);//e.Row;
			int clickedCol=gridMain.PointToCol(e.X);
			if(clickedI==-1){
				return;
			}
			gridMain.SetSelected(clickedI,true);//if right click.
			if(e.Button!=MouseButtons.Left) {
				return;
			}
			if(clickedI < TaskListsList.Count) {//is list
				//If the list is someone else's inbox, block
				//long mailboxUserNum=TaskLists.GetMailboxUserNum(TaskListsList[clickedI].TaskListNum);
				//This is too restrictive.  Need to work into security permissions:
				//if(mailboxUserNum != 0 && mailboxUserNum != Security.CurUser.UserNum) {
				//	MsgBox.Show(this,"Inboxes are private.");
				//	return;
				//}
				TreeHistory.Add(TaskListsList[clickedI]);
				FillTree();
				FillGrid();
				return;
			}
			if(tabContr.SelectedTab==tabNew && !PrefC.GetBool(PrefName.TasksNewTrackedByUser)){//There's an extra column
				if(clickedCol==1) {
					TaskUnreads.SetRead(Security.CurUser.UserNum,TasksList[clickedI-TaskListsList.Count].TaskNum);
					FillGrid();
				}
				return;//but ignore column 0 for now.  We would need to add that as a new feature.
			}
			if(clickedCol==0){//check tasks off
				if(PrefC.GetBool(PrefName.TasksNewTrackedByUser)) {
					if(tabContr.SelectedTab==tabNew){
						//these are never in someone else's inbox, so don't block. 
					}
					else{
						long userNumInbox=0;
						if(tabContr.SelectedTab==tabOpenTickets) {
							userNumInbox=TaskLists.GetMailboxUserNumByAncestor(TasksList[clickedI-TaskListsList.Count].TaskNum);
						}
						else {
							userNumInbox=TaskLists.GetMailboxUserNum(TreeHistory[0].TaskListNum);
						}
						if(userNumInbox != 0 && userNumInbox != Security.CurUser.UserNum) {
							MsgBox.Show(this,"Not allowed to mark off tasks in someone else's inbox.");
							return;
						}
					}
					//might not need to go to db to get this info 
					//might be able to check this:
					//if(task.IsUnread) {
					//But seems safer to go to db.
					if(TaskUnreads.IsUnread(Security.CurUser.UserNum,TasksList[clickedI-TaskListsList.Count].TaskNum)) {
						TaskUnreads.SetRead(Security.CurUser.UserNum,TasksList[clickedI-TaskListsList.Count].TaskNum);
					}
					DataValid.SetInvalidTask(TasksList[clickedI-TaskListsList.Count].TaskNum,false);
					//if already read, nothing else to do.  If done, nothing to do
				}
				else {
					if(TasksList[clickedI-TaskListsList.Count].TaskStatus==TaskStatusEnum.New) {
						Task task=TasksList[clickedI-TaskListsList.Count].Copy();
						Task taskOld=task.Copy();
						task.TaskStatus=TaskStatusEnum.Viewed;
						try {
							Tasks.Update(task,taskOld);
							DataValid.SetInvalidTask(task.TaskNum,false);
						}
						catch(Exception ex) {
							MessageBox.Show(ex.Message);
							return;
						}
					}
					//no longer allowed to mark done from here
				}
				//FillGrid();
			}
		}

		private void menuEdit_Popup(object sender,System.EventArgs e) {
			SetMenusEnabled();
		}

		private void SetMenusEnabled() {
			//Done----------------------------------
			if(gridMain.SelectedIndices.Length==0 || clickedI < TaskListsList.Count) {//or a tasklist selected
				menuItemDone.Enabled=false;
			}
			else {
				menuItemDone.Enabled=true;
			}
			//Edit,Cut,Copy,Delete-------------------------
			if(gridMain.SelectedIndices.Length==0) {
				menuItemEdit.Enabled=false;
				menuItemCut.Enabled=false;
				menuItemCopy.Enabled=false;
				menuItemDelete.Enabled=false;
			}
			else {
				menuItemEdit.Enabled=true;
				menuItemCut.Enabled=true;
				menuItemCopy.Enabled=true;
				menuItemDelete.Enabled=true;
			}
			//Paste----------------------------------------
			if(tabContr.SelectedTab==tabUser && TreeHistory.Count==0) {//not allowed to paste into the trunk of a user tab
				menuItemPaste.Enabled=false;
			}
			else if(ClipTaskList==null && ClipTask==null) {
				menuItemPaste.Enabled=false;
			}
			else {//there is an item on our clipboard
				menuItemPaste.Enabled=true;
			}
			//(overrides)
			if(tabContr.SelectedTab==tabNew || tabContr.SelectedTab==tabOpenTickets) {
				menuItemCut.Enabled=false;
				menuItemDelete.Enabled=false;
				menuItemPaste.Enabled=false;
			}
			//Subscriptions----------------------------------------------------------
			if(gridMain.SelectedIndices.Length==0) {
				menuItemSubscribe.Enabled=false;
				menuItemUnsubscribe.Enabled=false;
			}
			else if(tabContr.SelectedTab==tabUser && clickedI<TaskListsList.Count) {//user tab and is a list
				menuItemSubscribe.Enabled=false;
				menuItemUnsubscribe.Enabled=true;
			}
			else if(tabContr.SelectedTab==tabMain && clickedI < TaskListsList.Count) {//main and tasklist
				menuItemSubscribe.Enabled=true;
				menuItemUnsubscribe.Enabled=false;
			}
			else{//either any other tab, or a task on the main list
				menuItemSubscribe.Enabled=false;
				menuItemUnsubscribe.Enabled=false;
			}
			//SendToMe/GoTo---------------------------------------------------------------
			if(gridMain.SelectedIndices.Length>0 && clickedI >= TaskListsList.Count){//is task
				Task task=TasksList[clickedI-TaskListsList.Count];
				if(task.ObjectType==TaskObjectType.None) {
					menuItemGoto.Enabled=false;
				}
				else {
					menuItemGoto.Enabled=true;
				}
				menuItemSendToMe.Enabled=true;
			}
			else {
				menuItemGoto.Enabled=false;//not a task
				menuItemSendToMe.Enabled=false;
			}
		}

		private void OnSubscribe_Click(){
			//won't even get to this point unless it is a list
			try{
				TaskSubscriptions.SubscList(TaskListsList[clickedI].TaskListNum,Security.CurUser.UserNum);
			}
			catch(ApplicationException ex){//for example, if already subscribed.
				MessageBox.Show(ex.Message);
				return;
			}
			MsgBox.Show(this,"Done");
		}

		private void OnUnsubscribe_Click() {
			TaskSubscriptions.UnsubscList(TaskListsList[clickedI].TaskListNum,Security.CurUser.UserNum);
			//FillMain();
			FillGrid();
		}

		private void menuItemDone_Click(object sender,EventArgs e) {
			Done_Clicked();
		}

		private void menuItemEdit_Click(object sender,System.EventArgs e) {
			Edit_Clicked();
		}

		private void menuItemCut_Click(object sender,System.EventArgs e) {
			Cut_Clicked();
		}

		private void menuItemCopy_Click(object sender,System.EventArgs e) {
			Copy_Clicked();
		}

		private void menuItemPaste_Click(object sender,System.EventArgs e) {
			Paste_Clicked();
		}

		private void menuItemDelete_Click(object sender,System.EventArgs e) {
			Delete_Clicked();
		}

		private void menuItemSubscribe_Click(object sender,EventArgs e) {
			OnSubscribe_Click();
		}

		private void menuItemUnsubscribe_Click(object sender,EventArgs e) {
			OnUnsubscribe_Click();
		}

		private void menuItemSendToMe_Click(object sender,EventArgs e) {
			SendToMe_Clicked();
		}

		private void menuItemGoto_Click(object sender,System.EventArgs e) {
			Goto_Clicked();
		}

		//private void listMain_SelectedIndexChanged(object sender,System.EventArgs e) {
		//	SetMenusEnabled();
		//}

		private void tree_MouseDown(object sender,System.Windows.Forms.MouseEventArgs e) {
			for(int i=TreeHistory.Count-1;i>0;i--) {
				try {
					if(TreeHistory[i].TaskListNum==(long)tree.GetNodeAt(e.X,e.Y).Tag) {
						break;//don't remove the node click on or any higher node
					}
					TreeHistory.RemoveAt(i);
				}
				catch {//Harmless to return here because the user could have clicked near the node
					return;
				}
			}
			FillTree();
			//FillMain();
			FillGrid();
		}

		private void timerDoneTaskListRefresh_Tick(object sender,EventArgs e) {
			//This timer was set by textStartDate_TextChanged in order to prevent refreshing too frequently.
			timerDoneTaskListRefresh.Stop();
			FillGrid();
		}

		

		

		

		

	

		

		

		




	}
}
