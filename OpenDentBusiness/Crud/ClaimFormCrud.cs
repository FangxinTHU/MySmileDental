//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Crud{
	public class ClaimFormCrud {
		///<summary>Gets one ClaimForm object from the database using the primary key.  Returns null if not found.</summary>
		public static ClaimForm SelectOne(long claimFormNum){
			string command="SELECT * FROM claimform "
				+"WHERE ClaimFormNum = "+POut.Long(claimFormNum);
			List<ClaimForm> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one ClaimForm object from the database using a query.</summary>
		public static ClaimForm SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<ClaimForm> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of ClaimForm objects from the database using a query.</summary>
		public static List<ClaimForm> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<ClaimForm> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<ClaimForm> TableToList(DataTable table){
			List<ClaimForm> retVal=new List<ClaimForm>();
			ClaimForm claimForm;
			foreach(DataRow row in table.Rows) {
				claimForm=new ClaimForm();
				claimForm.ClaimFormNum= PIn.Long  (row["ClaimFormNum"].ToString());
				claimForm.Description = PIn.String(row["Description"].ToString());
				claimForm.IsHidden    = PIn.Bool  (row["IsHidden"].ToString());
				claimForm.FontName    = PIn.String(row["FontName"].ToString());
				claimForm.FontSize    = PIn.Float (row["FontSize"].ToString());
				claimForm.UniqueID    = PIn.String(row["UniqueID"].ToString());
				claimForm.PrintImages = PIn.Bool  (row["PrintImages"].ToString());
				claimForm.OffsetX     = PIn.Int   (row["OffsetX"].ToString());
				claimForm.OffsetY     = PIn.Int   (row["OffsetY"].ToString());
				retVal.Add(claimForm);
			}
			return retVal;
		}

		///<summary>Inserts one ClaimForm into the database.  Returns the new priKey.</summary>
		public static long Insert(ClaimForm claimForm){
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				claimForm.ClaimFormNum=DbHelper.GetNextOracleKey("claimform","ClaimFormNum");
				int loopcount=0;
				while(loopcount<100){
					try {
						return Insert(claimForm,true);
					}
					catch(Oracle.DataAccess.Client.OracleException ex){
						if(ex.Number==1 && ex.Message.ToLower().Contains("unique constraint") && ex.Message.ToLower().Contains("violated")){
							claimForm.ClaimFormNum++;
							loopcount++;
						}
						else{
							throw ex;
						}
					}
				}
				throw new ApplicationException("Insert failed.  Could not generate primary key.");
			}
			else {
				return Insert(claimForm,false);
			}
		}

		///<summary>Inserts one ClaimForm into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(ClaimForm claimForm,bool useExistingPK){
			if(!useExistingPK && PrefC.RandomKeys) {
				claimForm.ClaimFormNum=ReplicationServers.GetKey("claimform","ClaimFormNum");
			}
			string command="INSERT INTO claimform (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="ClaimFormNum,";
			}
			command+="Description,IsHidden,FontName,FontSize,UniqueID,PrintImages,OffsetX,OffsetY) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(claimForm.ClaimFormNum)+",";
			}
			command+=
				 "'"+POut.String(claimForm.Description)+"',"
				+    POut.Bool  (claimForm.IsHidden)+","
				+"'"+POut.String(claimForm.FontName)+"',"
				+    POut.Float (claimForm.FontSize)+","
				+"'"+POut.String(claimForm.UniqueID)+"',"
				+    POut.Bool  (claimForm.PrintImages)+","
				+    POut.Int   (claimForm.OffsetX)+","
				+    POut.Int   (claimForm.OffsetY)+")";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				claimForm.ClaimFormNum=Db.NonQ(command,true);
			}
			return claimForm.ClaimFormNum;
		}

		///<summary>Inserts one ClaimForm into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(ClaimForm claimForm){
			if(DataConnection.DBtype==DatabaseType.MySql) {
				return InsertNoCache(claimForm,false);
			}
			else {
				if(DataConnection.DBtype==DatabaseType.Oracle) {
					claimForm.ClaimFormNum=DbHelper.GetNextOracleKey("claimform","ClaimFormNum"); //Cacheless method
				}
				return InsertNoCache(claimForm,true);
			}
		}

		///<summary>Inserts one ClaimForm into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(ClaimForm claimForm,bool useExistingPK){
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO claimform (";
			if(!useExistingPK && isRandomKeys) {
				claimForm.ClaimFormNum=ReplicationServers.GetKeyNoCache("claimform","ClaimFormNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="ClaimFormNum,";
			}
			command+="Description,IsHidden,FontName,FontSize,UniqueID,PrintImages,OffsetX,OffsetY) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(claimForm.ClaimFormNum)+",";
			}
			command+=
				 "'"+POut.String(claimForm.Description)+"',"
				+    POut.Bool  (claimForm.IsHidden)+","
				+"'"+POut.String(claimForm.FontName)+"',"
				+    POut.Float (claimForm.FontSize)+","
				+"'"+POut.String(claimForm.UniqueID)+"',"
				+    POut.Bool  (claimForm.PrintImages)+","
				+    POut.Int   (claimForm.OffsetX)+","
				+    POut.Int   (claimForm.OffsetY)+")";
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command);
			}
			else {
				claimForm.ClaimFormNum=Db.NonQ(command,true);
			}
			return claimForm.ClaimFormNum;
		}

		///<summary>Updates one ClaimForm in the database.</summary>
		public static void Update(ClaimForm claimForm){
			string command="UPDATE claimform SET "
				+"Description = '"+POut.String(claimForm.Description)+"', "
				+"IsHidden    =  "+POut.Bool  (claimForm.IsHidden)+", "
				+"FontName    = '"+POut.String(claimForm.FontName)+"', "
				+"FontSize    =  "+POut.Float (claimForm.FontSize)+", "
				+"UniqueID    = '"+POut.String(claimForm.UniqueID)+"', "
				+"PrintImages =  "+POut.Bool  (claimForm.PrintImages)+", "
				+"OffsetX     =  "+POut.Int   (claimForm.OffsetX)+", "
				+"OffsetY     =  "+POut.Int   (claimForm.OffsetY)+" "
				+"WHERE ClaimFormNum = "+POut.Long(claimForm.ClaimFormNum);
			Db.NonQ(command);
		}

		///<summary>Updates one ClaimForm in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(ClaimForm claimForm,ClaimForm oldClaimForm){
			string command="";
			if(claimForm.Description != oldClaimForm.Description) {
				if(command!=""){ command+=",";}
				command+="Description = '"+POut.String(claimForm.Description)+"'";
			}
			if(claimForm.IsHidden != oldClaimForm.IsHidden) {
				if(command!=""){ command+=",";}
				command+="IsHidden = "+POut.Bool(claimForm.IsHidden)+"";
			}
			if(claimForm.FontName != oldClaimForm.FontName) {
				if(command!=""){ command+=",";}
				command+="FontName = '"+POut.String(claimForm.FontName)+"'";
			}
			if(claimForm.FontSize != oldClaimForm.FontSize) {
				if(command!=""){ command+=",";}
				command+="FontSize = "+POut.Float(claimForm.FontSize)+"";
			}
			if(claimForm.UniqueID != oldClaimForm.UniqueID) {
				if(command!=""){ command+=",";}
				command+="UniqueID = '"+POut.String(claimForm.UniqueID)+"'";
			}
			if(claimForm.PrintImages != oldClaimForm.PrintImages) {
				if(command!=""){ command+=",";}
				command+="PrintImages = "+POut.Bool(claimForm.PrintImages)+"";
			}
			if(claimForm.OffsetX != oldClaimForm.OffsetX) {
				if(command!=""){ command+=",";}
				command+="OffsetX = "+POut.Int(claimForm.OffsetX)+"";
			}
			if(claimForm.OffsetY != oldClaimForm.OffsetY) {
				if(command!=""){ command+=",";}
				command+="OffsetY = "+POut.Int(claimForm.OffsetY)+"";
			}
			if(command==""){
				return false;
			}
			command="UPDATE claimform SET "+command
				+" WHERE ClaimFormNum = "+POut.Long(claimForm.ClaimFormNum);
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if Update(ClaimForm,ClaimForm) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(ClaimForm claimForm,ClaimForm oldClaimForm) {
			if(claimForm.Description != oldClaimForm.Description) {
				return true;
			}
			if(claimForm.IsHidden != oldClaimForm.IsHidden) {
				return true;
			}
			if(claimForm.FontName != oldClaimForm.FontName) {
				return true;
			}
			if(claimForm.FontSize != oldClaimForm.FontSize) {
				return true;
			}
			if(claimForm.UniqueID != oldClaimForm.UniqueID) {
				return true;
			}
			if(claimForm.PrintImages != oldClaimForm.PrintImages) {
				return true;
			}
			if(claimForm.OffsetX != oldClaimForm.OffsetX) {
				return true;
			}
			if(claimForm.OffsetY != oldClaimForm.OffsetY) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one ClaimForm from the database.</summary>
		public static void Delete(long claimFormNum){
			string command="DELETE FROM claimform "
				+"WHERE ClaimFormNum = "+POut.Long(claimFormNum);
			Db.NonQ(command);
		}

	}
}