using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormCreditCardManage:Form {
		private Patient PatCur;
		private List<CreditCard> creditCards;

		public FormCreditCardManage(Patient pat) {
			InitializeComponent();
			Lan.F(this);
			PatCur=pat;
		}
		
		private void FormCreditCardManage_Load(object sender,EventArgs e) {
			if(PrefC.GetBool(PrefName.StoreCCnumbers)
				&& (Programs.IsEnabled(ProgramName.Xcharge) || Programs.IsEnabled(ProgramName.PayConnect)))//tokens supported by Xcharge and PayConnect
			{
				labelStoreCCNumWarning.Visible=true;
			}
			RefreshCardList();
			if(creditCards.Count>0) {
				listCreditCards.SelectedIndex=0;
			}
		}

		private void RefreshCardList() {
			listCreditCards.Items.Clear();
			creditCards=CreditCards.Refresh(PatCur.PatNum);
			for(int i=0;i<creditCards.Count;i++) {
				listCreditCards.Items.Add(creditCards[i].CCNumberMasked);
			}
		}

		private void listCreditCards_MouseDoubleClick(object sender,MouseEventArgs e) {
			if(listCreditCards.SelectedIndex==-1) {
				return;
			}
			int prev=creditCards.Count;
			int placement=listCreditCards.SelectedIndex;
			FormCreditCardEdit FormCCE=new FormCreditCardEdit(PatCur);
			FormCCE.CreditCardCur=creditCards[placement];
			FormCCE.ShowDialog();
			RefreshCardList();
			if(creditCards.Count==prev) {
				listCreditCards.SelectedIndex=placement;
			}
			else if(creditCards.Count>0) {
				listCreditCards.SelectedIndex=0;
			}
		}

		private void butAdd_Click(object sender,EventArgs e) {
			List<string> listDefaultProcs;
			if(!PrefC.GetBool(PrefName.StoreCCnumbers)) {
				if(Programs.IsEnabled(ProgramName.Xcharge)) {
					Program prog=Programs.GetCur(ProgramName.Xcharge);
					string path=Programs.GetProgramPath(prog);
					string xUsername=ProgramProperties.GetPropVal(prog.ProgramNum,"Username",FormOpenDental.ClinicNum).Trim();
					string xPassword=ProgramProperties.GetPropVal(prog.ProgramNum,"Password",FormOpenDental.ClinicNum).Trim();
					//Force user to retry entering information until it's correct or they press cancel
					while(!File.Exists(path) || string.IsNullOrEmpty(xPassword) || string.IsNullOrEmpty(xUsername)) {
						MsgBox.Show(this,"The Path, Username, and/or Password for X-Charge have not been set or are invalid.");
						if(!Security.IsAuthorized(Permissions.Setup)) {
							return;
						}
						FormXchargeSetup FormX=new FormXchargeSetup();//refreshes program and program property caches on OK click
						FormX.ShowDialog();
						if(FormX.DialogResult!=DialogResult.OK) {//if user presses cancel, return
							return;
						}
						prog=Programs.GetCur(ProgramName.Xcharge);//refresh local variable prog to reflect any changes made in setup window
						path=Programs.GetProgramPath(prog);
						xUsername=ProgramProperties.GetPropVal(prog.ProgramNum,"Username",FormOpenDental.ClinicNum).Trim();
						xPassword=ProgramProperties.GetPropVal(prog.ProgramNum,"Password",FormOpenDental.ClinicNum).Trim();
					}
					xPassword=CodeBase.MiscUtils.Decrypt(xPassword);
					ProcessStartInfo info=new ProcessStartInfo(path);
					string resultfile=Path.Combine(Path.GetDirectoryName(path),"XResult.txt");
					try {
						File.Delete(resultfile);//delete the old result file.
					}
					catch {
						MsgBox.Show(this,"Could not delete XResult.txt file.  It may be in use by another program, flagged as read-only, or you might not have sufficient permissions.");
						return;
					}
					info.Arguments="";
					info.Arguments+="/TRANSACTIONTYPE:ArchiveVaultAdd /LOCKTRANTYPE ";
					info.Arguments+="/RESULTFILE:\""+resultfile+"\" ";
					info.Arguments+="/USERID:"+xUsername+" ";
					info.Arguments+="/PASSWORD:"+xPassword+" ";
					info.Arguments+="/VALIDATEARCHIVEVAULTACCOUNT ";
					info.Arguments+="/STAYONTOP ";
					info.Arguments+="/SMARTAUTOPROCESS ";
					info.Arguments+="/AUTOCLOSE ";
					info.Arguments+="/HIDEMAINWINDOW ";
					info.Arguments+="/SMALLWINDOW ";
					info.Arguments+="/NORESULTDIALOG ";
					info.Arguments+="/TOOLBAREXITBUTTON ";
					Cursor=Cursors.WaitCursor;
					Process process=new Process();
					process.StartInfo=info;
					process.EnableRaisingEvents=true;
					process.Start();
					while(!process.HasExited) {
						Application.DoEvents();
					}
					Thread.Sleep(200);//Wait 2/10 second to give time for file to be created.
					Cursor=Cursors.Default;
					string resulttext="";
					string line="";
					string xChargeToken="";
					string accountMasked="";
					string exp="";;
					bool insertCard=false;
					try {
						using(TextReader reader=new StreamReader(resultfile)) {
							line=reader.ReadLine();
							while(line!=null) {
								if(resulttext!="") {
									resulttext+="\r\n";
								}
								resulttext+=line;
								if(line.StartsWith("RESULT=")) {
									if(line!="RESULT=SUCCESS") {
										throw new Exception();
									}
									insertCard=true;
								}
								if(line.StartsWith("XCACCOUNTID=")) {
									xChargeToken=PIn.String(line.Substring(12));
								}
								if(line.StartsWith("ACCOUNT=")) {
									accountMasked=PIn.String(line.Substring(8));
								}
								if(line.StartsWith("EXPIRATION=")) {
									exp=PIn.String(line.Substring(11));
								}
								line=reader.ReadLine();
							}
							if(insertCard && xChargeToken!="") {//Might not be necessary but we've had successful charges with no tokens returned before.
								CreditCard creditCardCur=new CreditCard();
								List<CreditCard> itemOrderCount=CreditCards.Refresh(PatCur.PatNum);
								creditCardCur.PatNum=PatCur.PatNum;
								creditCardCur.ItemOrder=itemOrderCount.Count;
								creditCardCur.CCNumberMasked=accountMasked;
								creditCardCur.XChargeToken=xChargeToken;
								creditCardCur.CCExpiration=new DateTime(Convert.ToInt32("20"+PIn.String(exp.Substring(2,2))),Convert.ToInt32(PIn.String(exp.Substring(0,2))),1);
								//Add the default procedures to this card if those procedures are not attached to any other active card
								listDefaultProcs=PrefC.GetString(PrefName.DefaultCCProcs).Split(',').ToList();
								for(int i=listDefaultProcs.Count-1;i>=0;i--) {
									if(CreditCards.ProcLinkedToCard(PatCur.PatNum,listDefaultProcs[i],0)) {
										listDefaultProcs.RemoveAt(i);
									}
								}
								creditCardCur.Procedures=String.Join(",",listDefaultProcs);
								CreditCards.Insert(creditCardCur);
							}
						}
						RefreshCardList();
					}
					catch(Exception ex) {
						MsgBox.Show(this,Lan.g(this,"There was a problem adding the credit card.  Please try again."));
						return;
					}
					return;
				}
				else if(Programs.IsEnabled(ProgramName.PayConnect)) {
					MsgBox.Show(this,"Storing credit card numbers is not allowed.  To store a PayConnect token for this card, "
						+"enter the card information into the payment window and process a payment with the \"Save Token\" option selected.");
					return;
				}
				else {
					MsgBox.Show(this,"Not allowed to store credit cards.");
					return;
				}
			}
			bool remember=false;
			int placement=listCreditCards.SelectedIndex;
			if(placement!=-1) {
				remember=true;
			}
			FormCreditCardEdit FormCCE=new FormCreditCardEdit(PatCur);
			FormCCE.CreditCardCur=new CreditCard();
			FormCCE.CreditCardCur.IsNew=true;
			//Add the default procedures to this card if those procedures are not attached to any other active card
			listDefaultProcs=PrefC.GetString(PrefName.DefaultCCProcs).Split(',').ToList();
			for(int i=listDefaultProcs.Count-1;i>=0;i--) {
				if(CreditCards.ProcLinkedToCard(PatCur.PatNum,listDefaultProcs[i],0)) {
					listDefaultProcs.RemoveAt(i);
				}
			}
			FormCCE.CreditCardCur.Procedures=String.Join(",",listDefaultProcs);
			FormCCE.ShowDialog();
			RefreshCardList();
			if(remember) {//in case they canceled and had one selected
				listCreditCards.SelectedIndex=placement;
			}
			if(FormCCE.DialogResult==DialogResult.OK && creditCards.Count>0) {
				listCreditCards.SelectedIndex=0;
			}
		}

		private void butMoveTo_Click(object sender,EventArgs e) {
			if(listCreditCards.SelectedIndex==-1) {
				MsgBox.Show(this,"Please select a card first.");
				return;
			}
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Move this credit card information to a different patient account?")) {
				return;
			}
			FormPatientSelect form=new FormPatientSelect();
			if(form.ShowDialog()!=DialogResult.OK) {
				return;
			}
			int selected=listCreditCards.SelectedIndex;
			CreditCard creditCard=creditCards[selected];
			creditCard.PatNum=form.SelectedPatNum;
			CreditCards.Update(creditCard);
			RefreshCardList();
			MessageBox.Show("Credit card moved successfully");
		}

		private void butUp_Click(object sender,EventArgs e) {
			int placement=listCreditCards.SelectedIndex;
			if(placement==-1) {
				MsgBox.Show(this,"Please select a card first.");
				return;
			}
			if(placement==0) {
				return;//can't move up any more
			}
			int oldIdx;
			int newIdx;
			CreditCard oldItem;
			CreditCard newItem;
			oldIdx=creditCards[placement].ItemOrder;
			newIdx=oldIdx+1; 
			for(int i=0;i<creditCards.Count;i++) {
				if(creditCards[i].ItemOrder==oldIdx) {
					oldItem=creditCards[i];
					newItem=creditCards[i-1];
					oldItem.ItemOrder=newItem.ItemOrder;
					newItem.ItemOrder-=1;
					CreditCards.Update(oldItem);
					CreditCards.Update(newItem);
				}
			}
			RefreshCardList();
			listCreditCards.SetSelected(placement-1,true);
		}

		private void butDown_Click(object sender,EventArgs e) {
			int placement=listCreditCards.SelectedIndex;
			if(placement==-1) {
				MsgBox.Show(this,"Please select a card first.");
				return;
			}
			if(placement==creditCards.Count-1) {
				return;//can't move down any more
			}
			int oldIdx;
			int newIdx;
			CreditCard oldItem;
			CreditCard newItem;
			oldIdx=creditCards[placement].ItemOrder;
			newIdx=oldIdx-1;
			for(int i=0;i<creditCards.Count;i++) {
				if(creditCards[i].ItemOrder==newIdx) {
					newItem=creditCards[i];
					oldItem=creditCards[i-1];
					newItem.ItemOrder=oldItem.ItemOrder;
					oldItem.ItemOrder-=1;
					CreditCards.Update(oldItem);
					CreditCards.Update(newItem);
				}
			}
			RefreshCardList();
			listCreditCards.SetSelected(placement+1,true);
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}