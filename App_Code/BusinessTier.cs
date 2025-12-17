using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;
using System.Net.Mail;
using System.IO;
using System.Net;

using System.Globalization;
using System.Collections;
using System.Data.OleDb;
using System.Drawing;

/// <summary>
/// Summary description for Class1
/// </summary>
public class BusinessTier
{
    public BusinessTier()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static DataTable g_ErrorMessagesDataTable;

    public static SqlConnection getConnection()
    {
        string conString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        SqlConnection conn = new SqlConnection(conString);
        return conn;
    }

    public static string getConnection1()
    {
        string conString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        return conString;
    }

    public static void DisposeConnection(SqlConnection conn)
    {
        conn.Close();
        conn.Dispose();
    }

    public static void DisposeReader(SqlDataReader reader)
    {
        reader.Close();
        reader.Dispose();
    }

    public static void DisposeAdapter(SqlDataAdapter adapter)
    {
        adapter.Dispose();
    }

    public static SqlDataReader VaildateUserLogin(SqlConnection connec, string Logind, string Password)
    {
        SqlCommand cmd = new SqlCommand("sp_Validate_UserLogin", connec);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Useridp", Logind);
        cmd.Parameters.AddWithValue("@Passp", Password); ;
        SqlDataReader reader1 = cmd.ExecuteReader();
        return reader1;
    }


    //---------------------------------------------------------------------------------------------
    //--------------------------< Methods For Master Module >--------------------------------------

    public static SqlDataReader getMasterModule(SqlConnection conn)
    {
        int delval = 0;
        string sql = "select * FROM MasterModule WHERE Deleted='" + delval + "' ORDER BY ModuleName";
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    public static SqlDataReader getMasterModuleById(SqlConnection connect, string strModuleId)
    {
        int delval = 0;
        string sql = "select * FROM MasterModule WHERE Deleted='" + delval + "' and ModuleId='" + strModuleId + "' ORDER BY ModuleName";
        SqlCommand cmd = new SqlCommand(sql, connect);
        SqlDataReader reader1 = cmd.ExecuteReader();
        return reader1;

    }

    public static int DeleteModuleGrid(SqlConnection conn, string id)
    {
        SqlCommand dCmd = new SqlCommand("sp_MasterModule_Delete", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@moduleidp", id);
        return dCmd.ExecuteNonQuery();
    }


    public static SqlDataReader checkModuleName(SqlConnection connCheck, string name)
    {
        SqlCommand cmd = new SqlCommand("sp_MasterModule_IsDuplicate", connCheck);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@modulenamep", name);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }


    public static int SaveModuleMaster(SqlConnection conn, string name, string desc, string appflag, string userid, string saveflag, string modid)
    {
        string sp_Name;
        string RowValue = "0";
        if (saveflag.ToString() == "N")
        {
            sp_Name = "[sp_MasterModule_Insert]";
        }
        else
        {
            sp_Name = "[sp_MasterModule_Update]";
        }
        SqlCommand dCmd = new SqlCommand(sp_Name, conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        if (saveflag.ToString() == "U")
        {
            dCmd.Parameters.AddWithValue("@idp", modid);
            dCmd.Parameters.AddWithValue("@Rowp", RowValue);
        }
        dCmd.Parameters.AddWithValue("@namep", name);
        dCmd.Parameters.AddWithValue("@descriptionp", desc);
        dCmd.Parameters.AddWithValue("@approvalflag", appflag);
        dCmd.Parameters.AddWithValue("@useridp", userid);
        return dCmd.ExecuteNonQuery();
    }

    //---------------------------------------------------------------------------------------------
    //--------------------------< Methods For Master User >--------------------------------------

    public static SqlDataReader getMasterUserInfo(SqlConnection conn)
    {
        int delval = 0;
        string sql = "select * FROM MasterUser WHERE Deleted='" + delval + "' ORDER BY UserName";
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    public static SqlDataReader getMasterUserByID(SqlConnection conn, string strID)
    {
        int delval = 0;
        string sql = "select * FROM MasterUser WHERE ID='" + strID + "' and  Deleted='" + delval + "' ORDER BY UserName";
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    public static SqlDataReader getUserNameByID(SqlConnection conn, string strID)
    {
        SqlCommand cmd = new SqlCommand("[sp_MasterUser_getUserName]", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@idp", strID);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    public static string getMasterUserIDByName(SqlConnection conn, string strName)
    {
        int delval = 0;
        string sql = "select ID FROM MasterUser WHERE UserName like '%" + strName + "%' and  Deleted='" + delval + "'";
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader reader = cmd.ExecuteReader();
        reader.Read();
        string ret = reader[0].ToString();
        BusinessTier.DisposeReader(reader);
        //BusinessTier.DisposeConnection(conn);
        return ret;
    }

    public static SqlDataReader getMasterUserByLoginId(SqlConnection conn, string strLoginId)
    {
        int delval = 0;
        string sql = "select * FROM MasterUser WHERE Deleted='" + delval + "' and LoginId='" + strLoginId + "' ORDER BY UserName";
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }


    public static void BindErrorMessageDetails(SqlConnection connError)
    {
        string sql = "select * FROM MasterErrorMessage order by OrderNo";
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, connError);
        g_ErrorMessagesDataTable = new DataTable();
        sqlDataAdapter.Fill(g_ErrorMessagesDataTable);
        BusinessTier.DisposeAdapter(sqlDataAdapter);
    }

    public static void InsertLogAuditTrial(SqlConnection connLog, string userid, string module, string activity, string result, string flag)
    {
        string sp_Name;
        if (flag.ToString() == "Log")
        {
            sp_Name = "[sp_Master_Insert_Log]";
        }
        else
        {
            sp_Name = "[sp_Master_Insert_AuditTrail]";
        }

        SqlCommand dCmd = new SqlCommand(sp_Name, connLog);

        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@useridp", userid);
        dCmd.Parameters.AddWithValue("@modulep", module);
        dCmd.Parameters.AddWithValue("@activityp", activity);
        dCmd.Parameters.AddWithValue("@resultp", result);
        dCmd.ExecuteNonQuery();
    }


    public static SqlDataReader getMenuList(SqlConnection conn, string Uid)
    {
        string sql = "SELECT dbo.MenuList.Category, dbo.MenuList.SeqCategory FROM dbo.UserInfo_Permission INNER JOIN dbo.MenuList ON dbo.UserInfo_Permission.MenuId = dbo.MenuList.Id WHERE dbo.UserInfo_Permission.UserId = '" + Uid.ToString().Trim() + "' GROUP BY dbo.MenuList.Category, dbo.MenuList.SeqCategory";
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    public static DataTable getSubMenuItems(string category, string uid)
    {

        DataTable ret = new DataTable();
        SqlConnection conn = getConnection();
        conn.Open();
        string sql = "SELECT dbo.MenuList.ModuleID, dbo.MenuList.Href, dbo.MenuList.ModuleName FROM dbo.UserInfo_Permission INNER JOIN dbo.MenuList ON dbo.UserInfo_Permission.MenuId = dbo.MenuList.Id WHERE dbo.MenuList.Category = '" + category + "' and dbo.UserInfo_Permission.UserId = '" + uid.ToString().Trim() + "'  order by SeqMenu";
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader reader = cmd.ExecuteReader();
        ret.Load(reader);
        BusinessTier.DisposeConnection(conn);
        return ret;
    }

    //--------------------------------------------------------------------------------------------
    //----------------------MISC------------------------------------------------------------------

    public static string getCCMailID(string strModule)
    {
        string strEmailFile = ConfigurationManager.AppSettings["Email_CC_FilePath"].ToString();
        string strMailCC = "Default@petronas.com.my";

        if (File.Exists(strEmailFile))
        {
            string strLine = "";
            string[] strLine1 = new string[1];
            int counter = 0;
            StreamReader reader = new StreamReader(strEmailFile);
            while ((strLine = reader.ReadLine()) != null)
            {
                if (counter == 0)
                {
                    strLine1 = strLine.Split(':');

                    if (strLine1[0].ToString().Trim() == strModule.ToString().Trim())
                    {
                        strMailCC = strLine1[1].ToString().Trim();
                        counter = 1;
                    }
                }
            }
            reader.Close();
            reader.Dispose();
        }
        return strMailCC.ToString().Trim();
    }

    public static void SendMail(string strSubject, string strBody, string strToAddress, string strApprovarMail, string strAttachmentFilename)
    {
        SmtpClient smtpClient = new SmtpClient();
        MailMessage message = new MailMessage();
        if (!(strAttachmentFilename.ToString().Trim() == "NoAttach"))
        {
            Attachment attachment = new Attachment(strAttachmentFilename.ToString().Trim());
            message.Attachments.Add(attachment);
        }
        MailAddress fromAddress = new MailAddress(ConfigurationManager.AppSettings["FromAddress"].ToString(), "Online Asset Tracking System");
        smtpClient.Host = ConfigurationManager.AppSettings["ExchangeServer"].ToString();
        smtpClient.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"].ToString());

        message.Priority = MailPriority.High;
        message.From = fromAddress;
        message.Subject = strSubject.ToString();
        message.To.Add(strToAddress.ToString());
        message.CC.Add(strApprovarMail.ToString());
        //message.CC.Add("bala@e-serbadk.com");
        message.Body = strBody;
        //smtpClient.EnableSsl = true;
        smtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["FromAddress"].ToString(), ConfigurationManager.AppSettings["Password"].ToString().Trim());
        // smtpClient.Send(message);
        message.Dispose();
        smtpClient.Dispose();
        File.Delete(strAttachmentFilename.ToString().Trim());
    }

    //---------------------------------------------------------------------------------------------
    //--------------------------< Methods For RFID Activate/Deactivate >------------------------------------------------
    public static void UpdateRFIDDB(string MaterialID, string flag, string rfid)
    {
        string conString = ConfigurationManager.ConnectionStrings["connStringRFID"].ConnectionString;

        string desc = "Unknown", name = "Unknown", expirydate = "";
        int typeID = 31, behaviourID = 23, interval = 1;
        bool isActive = false;
        if (flag == "A")
        {
            typeID = 8;
            isActive = true;

            //SqlConnection getIdConn = getConnection();
            //getIdConn.Open();
            //SqlDataReader getIdreader = getAllInfoByID_MRVDetail(getIdConn, MaterialID);
            //if (getIdreader.Read())
            //{
            //    name = (getIdreader["MaterialName"].ToString()).TrimEnd();
            //    desc = (getIdreader["MaterialNo"].ToString()).TrimEnd();
            //    expirydate = (getIdreader["ExpiryDate"].ToString()).TrimEnd();
            //}
            //BusinessTier.DisposeReader(getIdreader);
            //BusinessTier.DisposeConnection(getIdConn);
        }

        //Update fasb_Tags.... Insert into TagAlertRules......
        SqlConnection conn = new SqlConnection(conString);
        conn.Open();
        string sp_Name = "[sp_RFID_Update]";
        SqlCommand dCmd = new SqlCommand(sp_Name, conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@ID", rfid);
        dCmd.Parameters.AddWithValue("@TypeID", typeID);
        dCmd.Parameters.AddWithValue("@Description", desc);
        dCmd.Parameters.AddWithValue("@BehaviourID", behaviourID);
        dCmd.Parameters.AddWithValue("@Interval", interval);
        dCmd.Parameters.AddWithValue("@IsActive", isActive);
        dCmd.Parameters.AddWithValue("@Name", name);
        dCmd.Parameters.AddWithValue("@ExpiryDate", expirydate);
        dCmd.Parameters.AddWithValue("@Email", ConfigurationManager.AppSettings["RFID_Email"].ToString());
        dCmd.Parameters.AddWithValue("@Flag", flag);
        dCmd.ExecuteNonQuery();
        BusinessTier.DisposeConnection(conn);
    }

    public static void ExtendTagAlertRulesEndDate(string MRVMasterID, string MRVDetailID)
    {
        string conString = ConfigurationManager.ConnectionStrings["connStringRFID"].ConnectionString;
        string rfID = getRFID(MRVMasterID, MRVDetailID);
        string expiryDate = getMRVExtExpiryDate(MRVMasterID, MRVDetailID);
        SqlConnection conn = getConnection();
        conn.Open();
        string sp_Name = "[sp_RFID_Extend]";
        SqlCommand dCmd = new SqlCommand(sp_Name, conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@ID", rfID);
        dCmd.Parameters.AddWithValue("@ExpiryDate", expiryDate);
        dCmd.ExecuteNonQuery();
        BusinessTier.DisposeConnection(conn);
    }

    public static string getRFID(string mrvMasterID, string mrvDetID)
    {
        string ret = "";
        SqlConnection conn = getConnection();
        conn.Open();
        string sql = "select RFID from MRV_Received WHERE MRVMasterID = " + mrvMasterID + " and MRVDetailID=" + mrvDetID + " and Deleted='0'";
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
            ret = reader["RFID"].ToString();
        BusinessTier.DisposeConnection(conn);
        return ret;
    }
    public static string getMRVExtExpiryDate(string mrvMasterID, string mrvDetID)
    {
        string ret = "";
        SqlConnection conn = getConnection();
        conn.Open();
        string sql = "select ExpiryDate from MRV_Detail WHERE MRVMasterID = " + mrvMasterID + " and MRVDetailID=" + mrvDetID + " and Deleted='0'";
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
            ret = reader["ExpiryDate"].ToString();
        BusinessTier.DisposeConnection(conn);
        return ret;
    }

    public static int SaveCompany(SqlConnection conn, string name, string address1, string address2, string city, string state, string country, string postcode, string desc, string phone, string fax, string email, string website, string userid, string saveflag, string Compid)
    {
        SqlCommand dCmd = new SqlCommand("[sp_Company_Save]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@idp", Compid);
        dCmd.Parameters.AddWithValue("@namep", name);
        dCmd.Parameters.AddWithValue("@address1p", address1);
        dCmd.Parameters.AddWithValue("@address2p", address2);
        dCmd.Parameters.AddWithValue("@cityp", city);
        dCmd.Parameters.AddWithValue("@statep", state);
        dCmd.Parameters.AddWithValue("@countryp", country);
        dCmd.Parameters.AddWithValue("@descriptionp", desc);
        dCmd.Parameters.AddWithValue("@contactnop", phone);
        dCmd.Parameters.AddWithValue("@faxnop", fax);
        dCmd.Parameters.AddWithValue("@emailp", email);
        dCmd.Parameters.AddWithValue("@websitep", website);
        dCmd.Parameters.AddWithValue("@postcodep", postcode);
        dCmd.Parameters.AddWithValue("@useridp", userid);
        dCmd.Parameters.AddWithValue("@saveflag", saveflag);
        return dCmd.ExecuteNonQuery();
    }
    // -------------------------------------------POF--------------------------------------
    //-------------Thinning Damage Factor

    public static int Claddformula(SqlConnection conn, int Compid, int claddcompid, int age)
    {
        SqlCommand dCmd = new SqlCommand("[sp_Cladd_Formula]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@CompAutoIDp", Compid);
        dCmd.Parameters.AddWithValue("@claddcompidp", claddcompid);
        dCmd.Parameters.AddWithValue("@agep", age);
        return dCmd.ExecuteNonQuery();
    }

    public static int withoutCladdformula(SqlConnection conn, int Compid, int age)
    {
        SqlCommand dCmd = new SqlCommand("[sp_withoutCladd_Formula]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@CompAutoIDp", Compid);
        dCmd.Parameters.AddWithValue("@agep", age);
        return dCmd.ExecuteNonQuery();
    }

    public static int ThinningSave(SqlConnection conn, int ProcessareaID, int EqupID, int CompID, string Clad, int age, decimal art, decimal Tdf, string InspectCate, DateTime InspectDate, int NofInsp, string ThinType, int CompanyID,int userid, string saveflag, int intId)
    {
        SqlCommand dCmd = new SqlCommand("[sp_Thinning_Save]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@ProcessareaIDp", ProcessareaID);
        dCmd.Parameters.AddWithValue("@EqupIDp", EqupID);
        dCmd.Parameters.AddWithValue("@CompIDp", CompID);
        dCmd.Parameters.AddWithValue("@Cladp", Clad);
        dCmd.Parameters.AddWithValue("@agep", age);
        dCmd.Parameters.AddWithValue("@artp", art);
        dCmd.Parameters.AddWithValue("@Tdfp", Tdf);
        dCmd.Parameters.AddWithValue("@InspectCatep", InspectCate);
        dCmd.Parameters.AddWithValue("@InspectDate", InspectDate);
        dCmd.Parameters.AddWithValue("@NofInsp", NofInsp);
        dCmd.Parameters.AddWithValue("@ThinTypep", ThinType);
        dCmd.Parameters.AddWithValue("@CompanyID", CompanyID);
        dCmd.Parameters.AddWithValue("@useridp", userid);
        dCmd.Parameters.AddWithValue("@saveflagp", saveflag);
        dCmd.Parameters.AddWithValue("@idp", intId);
        return dCmd.ExecuteNonQuery();
    }

     

    

    //-------------Lining Damage Factor

    public static int LiningSave(SqlConnection conn, int ProcessareaID, int EqupID, int CompID, string liningtype, int ageline, string dfb, string lncondition, int lnconditionVal, string Onlinemonitor, decimal OnlinemonitorVal, decimal Dfline, int CompanyID, int userid, string saveflag, int intId)
    {
        SqlCommand dCmd = new SqlCommand("[sp_Lining_Df_Save]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@ProcessareaIDp", ProcessareaID);
        dCmd.Parameters.AddWithValue("@EqupIDp", EqupID);
        dCmd.Parameters.AddWithValue("@CompIDp", CompID);
        dCmd.Parameters.AddWithValue("@lingingtypep", liningtype);
        dCmd.Parameters.AddWithValue("@agelinep", ageline);
        dCmd.Parameters.AddWithValue("@dfbp", dfb);
        dCmd.Parameters.AddWithValue("@lineconditionp", lncondition);
        dCmd.Parameters.AddWithValue("@lineconditionValp", lnconditionVal);
        dCmd.Parameters.AddWithValue("@Onlinemonitorp", Onlinemonitor);
        dCmd.Parameters.AddWithValue("@OnlinemonitorValp", OnlinemonitorVal);
        dCmd.Parameters.AddWithValue("@Dfline", Dfline);
        dCmd.Parameters.AddWithValue("@CompanyID", CompanyID);
        dCmd.Parameters.AddWithValue("@useridp", userid);
        dCmd.Parameters.AddWithValue("@saveflagp", saveflag);
        dCmd.Parameters.AddWithValue("@idp", intId);
        return dCmd.ExecuteNonQuery();
    }

    //------------ECD Factor

    public static int ECDSave(SqlConnection conn, int ProcessareaID, int EqupID, int CompID, int agetkp, string InsEffp, DateTime compDtp, DateTime calDtp, int NofInsp, DateTime InspectDate, string coatqualp, decimal agecoatp, decimal agep, int fpsp, int fipp, string CrDriverp, decimal Crp, decimal artp, decimal Dfextp, int CompanyID, int userid, string saveflag, int intId)
    {
        SqlCommand dCmd = new SqlCommand("[sp_ECD_Save]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@ProcessareaIDp", ProcessareaID);
        dCmd.Parameters.AddWithValue("@EqupIDp", EqupID);
        dCmd.Parameters.AddWithValue("@CompIDp", CompID);
        dCmd.Parameters.AddWithValue("@agetkp", agetkp);
        dCmd.Parameters.AddWithValue("@InsEffp", InsEffp);
        dCmd.Parameters.AddWithValue("@compDtp", compDtp);
        dCmd.Parameters.AddWithValue("@calDtp", calDtp);
        dCmd.Parameters.AddWithValue("@NofInsp", NofInsp);
        dCmd.Parameters.AddWithValue("@InspectDate", InspectDate);
        dCmd.Parameters.AddWithValue("@coatqualp", coatqualp);
        dCmd.Parameters.AddWithValue("@agecoatp", agecoatp);
        dCmd.Parameters.AddWithValue("@agep", agep);
        dCmd.Parameters.AddWithValue("@fpsp", fpsp);
        dCmd.Parameters.AddWithValue("@fipp", fipp);
        dCmd.Parameters.AddWithValue("@CrDriverp", CrDriverp);
        dCmd.Parameters.AddWithValue("@Crp", Crp);
        dCmd.Parameters.AddWithValue("@artp", artp);
        dCmd.Parameters.AddWithValue("@Dfextp", Dfextp);
        dCmd.Parameters.AddWithValue("@CompanyID", CompanyID);
        dCmd.Parameters.AddWithValue("@useridp", userid);
        dCmd.Parameters.AddWithValue("@saveflagp", saveflag);
        dCmd.Parameters.AddWithValue("@idp", intId);
        return dCmd.ExecuteNonQuery();
    }

    //------------ExCLSCC Factor

    public static int ExCLSSave(SqlConnection conn, int ProcessareaID, int EqupID, int CompID, int agetkp, string InsEffp, DateTime compDtp, DateTime calDtp, int NofInsp, DateTime InspectDate, string coatqualp, decimal agep, string CrDriverp, string svip, decimal Dfexclsp, int CompanyID, int userid, string saveflag, int intId)
    {
        SqlCommand dCmd = new SqlCommand("[sp_ExCLS_Save]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@ProcessareaIDp", ProcessareaID);
        dCmd.Parameters.AddWithValue("@EqupIDp", EqupID);
        dCmd.Parameters.AddWithValue("@CompIDp", CompID);
        dCmd.Parameters.AddWithValue("@agetkp", agetkp);
        dCmd.Parameters.AddWithValue("@InsEffp", InsEffp);
        dCmd.Parameters.AddWithValue("@compDtp", compDtp);
        dCmd.Parameters.AddWithValue("@calDtp", calDtp);
        dCmd.Parameters.AddWithValue("@NofInsp", NofInsp);
        dCmd.Parameters.AddWithValue("@InspectDate", InspectDate);
        dCmd.Parameters.AddWithValue("@coatqualp", coatqualp);
        dCmd.Parameters.AddWithValue("@agep", agep);
        dCmd.Parameters.AddWithValue("@CrDriverp", CrDriverp);
        dCmd.Parameters.AddWithValue("@svip", svip);
        dCmd.Parameters.AddWithValue("@Dfexclsp", Dfexclsp);
        dCmd.Parameters.AddWithValue("@CompanyID", CompanyID);
        dCmd.Parameters.AddWithValue("@useridp", userid);
        dCmd.Parameters.AddWithValue("@saveflagp", saveflag);
        dCmd.Parameters.AddWithValue("@idp", intId);
        return dCmd.ExecuteNonQuery();
    }

    //------------ExCUI Factor

    public static int ExCUISave(SqlConnection conn, int ProcessareaID, int EqupID, int CompID, int agetkp, string InsEffp, DateTime compDtp, DateTime calDtp, int NofInsp, DateTime InspectDate, string coatqualp, decimal agep, string CrDriverp, string pipp, string insconp, string chlrfreep, string svip, decimal Dfexclsp, int CompanyID, int userid, string saveflag, int intId)
    {
        SqlCommand dCmd = new SqlCommand("[sp_ExCUI_Save]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@ProcessareaIDp", ProcessareaID);
        dCmd.Parameters.AddWithValue("@EqupIDp", EqupID);
        dCmd.Parameters.AddWithValue("@CompIDp", CompID);
        dCmd.Parameters.AddWithValue("@agetkp", agetkp);
        dCmd.Parameters.AddWithValue("@InsEffp", InsEffp);
        dCmd.Parameters.AddWithValue("@compDtp", compDtp);
        dCmd.Parameters.AddWithValue("@calDtp", calDtp);
        dCmd.Parameters.AddWithValue("@NofInsp", NofInsp);
        dCmd.Parameters.AddWithValue("@InspectDate", InspectDate);
        dCmd.Parameters.AddWithValue("@coatqualp", coatqualp);
        dCmd.Parameters.AddWithValue("@agep", agep);
        dCmd.Parameters.AddWithValue("@CrDriverp", CrDriverp);
        dCmd.Parameters.AddWithValue("@pipp", pipp);
        dCmd.Parameters.AddWithValue("@insconp", insconp);
        dCmd.Parameters.AddWithValue("@chlrfreep", chlrfreep);
        dCmd.Parameters.AddWithValue("@svip", svip);
        dCmd.Parameters.AddWithValue("@Dfexclsp", Dfexclsp);
        dCmd.Parameters.AddWithValue("@CompanyID", CompanyID);
        dCmd.Parameters.AddWithValue("@useridp", userid);
        dCmd.Parameters.AddWithValue("@saveflagp", saveflag);
        dCmd.Parameters.AddWithValue("@idp", intId);
        return dCmd.ExecuteNonQuery();
    }

    //------------HTHA Factor

    public static int HTHASave(SqlConnection conn, int ProcessareaID, int EqupID, int CompID, string InsEff, int nofins,DateTime InspectDate,double age, double ExTemp, string Heat, double PH2, double Pv, string Mat, string Svi, string inspection, int HTHADf, int userid, string saveflag, int intId)
    {
        SqlCommand dCmd = new SqlCommand("[sp_HTHA_Save]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@ProcessareaIDp", ProcessareaID);
        dCmd.Parameters.AddWithValue("@EqupIDp", EqupID);
        dCmd.Parameters.AddWithValue("@CompIDp", CompID);
        dCmd.Parameters.AddWithValue("@InsEffp", InsEff);
        dCmd.Parameters.AddWithValue("@NofInsp", nofins);
        dCmd.Parameters.AddWithValue("@InspectDate", InspectDate);
        dCmd.Parameters.AddWithValue("@agep", age);
        dCmd.Parameters.AddWithValue("@ExTempp", ExTemp);
        dCmd.Parameters.AddWithValue("@Heatp", Heat);
        dCmd.Parameters.AddWithValue("@PH2p", PH2);
        dCmd.Parameters.AddWithValue("@Pvp", Pv);
        dCmd.Parameters.AddWithValue("@Matp", Mat);
        dCmd.Parameters.AddWithValue("@svip", Svi);
        dCmd.Parameters.AddWithValue("@inspectionp", inspection);
        dCmd.Parameters.AddWithValue("@DfHTHAp", HTHADf);
        dCmd.Parameters.AddWithValue("@useridp", userid);
        dCmd.Parameters.AddWithValue("@saveflagp", saveflag);
        dCmd.Parameters.AddWithValue("@idp", intId);
        return dCmd.ExecuteNonQuery();
    }

    //------------Brittle Factor

    public static int BrittleSave(SqlConnection conn, int ProcessareaID, int EqupID, int CompID, string Fullpress, int MinDsgn, int ATM, int Tmin, int Tref, decimal CmpThick, string PWHT, decimal Dfb, int OpTemp, decimal FSE, decimal Df, int userid, string saveflag, int intId)
    {
        SqlCommand dCmd = new SqlCommand("[sp_Brittle_Save]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@ProcessareaIDp", ProcessareaID);
        dCmd.Parameters.AddWithValue("@EqupIDp", EqupID);
        dCmd.Parameters.AddWithValue("@CompIDp", CompID);
        dCmd.Parameters.AddWithValue("@Fullpressp", Fullpress);
        dCmd.Parameters.AddWithValue("@MinDsgnp", MinDsgn);
        dCmd.Parameters.AddWithValue("@ATMp", ATM);
        dCmd.Parameters.AddWithValue("@Tminp", Tmin);
        dCmd.Parameters.AddWithValue("@Trefp", Tref);
        dCmd.Parameters.AddWithValue("@CmpThickp", CmpThick);
        dCmd.Parameters.AddWithValue("@PWHTp", PWHT);
        dCmd.Parameters.AddWithValue("@Dfbp", Dfb);
        dCmd.Parameters.AddWithValue("@OpTempp", OpTemp);
        dCmd.Parameters.AddWithValue("@FSE", FSE);
        dCmd.Parameters.AddWithValue("@Dfp", Df);
        dCmd.Parameters.AddWithValue("@useridp", userid);
        dCmd.Parameters.AddWithValue("@saveflagp", saveflag);
        dCmd.Parameters.AddWithValue("@idp", intId);
        return dCmd.ExecuteNonQuery();
    }

    //------------Temper Factor

    public static int TemperSave(SqlConnection conn, int ProcessareaID, int EqupID, int CompID, string Fullpress, int MinDsgn, int Tmin, int Tref, string FATT, int fatval, decimal CmpThick, string PWHT, decimal Dfb, int OpTemp, decimal FSE, decimal Df, int userid, string saveflag, int intId)
    {
        SqlCommand dCmd = new SqlCommand("[sp_Temper_Save]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@ProcessareaIDp", ProcessareaID);
        dCmd.Parameters.AddWithValue("@EqupIDp", EqupID);
        dCmd.Parameters.AddWithValue("@CompIDp", CompID);
        dCmd.Parameters.AddWithValue("@Fullpressp", Fullpress);
        dCmd.Parameters.AddWithValue("@MinDsgnp", MinDsgn);
        dCmd.Parameters.AddWithValue("@Tminp", Tmin);
        dCmd.Parameters.AddWithValue("@Trefp", Tref);
        dCmd.Parameters.AddWithValue("@FATTp", FATT);
        dCmd.Parameters.AddWithValue("@fatvalp", fatval);
        dCmd.Parameters.AddWithValue("@CmpThickp", CmpThick);
        dCmd.Parameters.AddWithValue("@PWHTp", PWHT);
        dCmd.Parameters.AddWithValue("@Dfbp", Dfb);
        dCmd.Parameters.AddWithValue("@OpTempp", OpTemp);
        dCmd.Parameters.AddWithValue("@FSE", FSE);
        dCmd.Parameters.AddWithValue("@Dfp", Df);
        dCmd.Parameters.AddWithValue("@useridp", userid);
        dCmd.Parameters.AddWithValue("@saveflagp", saveflag);
        dCmd.Parameters.AddWithValue("@idp", intId);
        return dCmd.ExecuteNonQuery();
    }

    //------------EightEightFive Factor

    public static int EightEightFiveSave(SqlConnection conn, int ProcessareaID, int EqupID, int CompID, string Fullpress, int OpTemp, int MinDsgn, int Tmin, decimal CmpThick, string BritTrans, int Tref, decimal Df, int userid, string saveflag, int intId)
    {
        SqlCommand dCmd = new SqlCommand("[sp_EightEightFive_Save]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@ProcessareaIDp", ProcessareaID);
        dCmd.Parameters.AddWithValue("@EqupIDp", EqupID);
        dCmd.Parameters.AddWithValue("@CompIDp", CompID);
        dCmd.Parameters.AddWithValue("@Fullpressp", Fullpress);
        dCmd.Parameters.AddWithValue("@OpTempp", OpTemp);
        dCmd.Parameters.AddWithValue("@MinDsgnp", MinDsgn);
        dCmd.Parameters.AddWithValue("@Tminp", Tmin);
        dCmd.Parameters.AddWithValue("@CmpThickp", CmpThick);
        dCmd.Parameters.AddWithValue("@BritTransp", BritTrans);
        dCmd.Parameters.AddWithValue("@Trefp", Tref);
        dCmd.Parameters.AddWithValue("@Dfp", Df);
        dCmd.Parameters.AddWithValue("@useridp", userid);
        dCmd.Parameters.AddWithValue("@saveflagp", saveflag);
        dCmd.Parameters.AddWithValue("@idp", intId);
        return dCmd.ExecuteNonQuery();
    }

    //------------Sigma Factor

    public static int SigmaSave(SqlConnection conn, int ProcessareaID, int EqupID, int CompID, int Tmin, string Sigma, decimal Df, int userid, string saveflag, int intId)
    {
        SqlCommand dCmd = new SqlCommand("[sp_Sigma_Save]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@ProcessareaIDp", ProcessareaID);
        dCmd.Parameters.AddWithValue("@EqupIDp", EqupID);
        dCmd.Parameters.AddWithValue("@CompIDp", CompID);
        dCmd.Parameters.AddWithValue("@Tminp", Tmin);
        dCmd.Parameters.AddWithValue("@Sigmap", Sigma);
        dCmd.Parameters.AddWithValue("@Dfp", Df);
        dCmd.Parameters.AddWithValue("@useridp", userid);
        dCmd.Parameters.AddWithValue("@saveflagp", saveflag);
        dCmd.Parameters.AddWithValue("@idp", intId);
        return dCmd.ExecuteNonQuery();
    }

    //------------CUI Factor

    public static int CUISave(SqlConnection conn, int ProcessareaID, int EqupID, int CompID, int agetkp, string InsEffp, DateTime compDtp, DateTime calDtp, int NofInsp, DateTime InspectDate, string coatqualp, decimal agecoatp, decimal agep, int fpsp, int fipp, string CrDriverp, decimal Crp, decimal artp, decimal Dfextp, string Finsp, string Fcmp, string Ficp, int CompanyID, int userid, string saveflag, int intId)
    {
        SqlCommand dCmd = new SqlCommand("[sp_CUI_Save]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@ProcessareaIDp", ProcessareaID);
        dCmd.Parameters.AddWithValue("@EqupIDp", EqupID);
        dCmd.Parameters.AddWithValue("@CompIDp", CompID);
        dCmd.Parameters.AddWithValue("@agetkp", agetkp);
        dCmd.Parameters.AddWithValue("@InsEffp", InsEffp);
        dCmd.Parameters.AddWithValue("@compDtp", compDtp);
        dCmd.Parameters.AddWithValue("@calDtp", calDtp);
        dCmd.Parameters.AddWithValue("@NofInsp", NofInsp);
        dCmd.Parameters.AddWithValue("@InspectDate", InspectDate);
        dCmd.Parameters.AddWithValue("@coatqualp", coatqualp);
        dCmd.Parameters.AddWithValue("@agecoatp", agecoatp);
        dCmd.Parameters.AddWithValue("@agep", agep);
        dCmd.Parameters.AddWithValue("@fpsp", fpsp);
        dCmd.Parameters.AddWithValue("@fipp", fipp);
        dCmd.Parameters.AddWithValue("@CrDriverp", CrDriverp);
        dCmd.Parameters.AddWithValue("@Crp", Crp);
        dCmd.Parameters.AddWithValue("@artp", artp);
        dCmd.Parameters.AddWithValue("@Dfextp", Dfextp);
        dCmd.Parameters.AddWithValue("@Finsp", Finsp);
        dCmd.Parameters.AddWithValue("@Fcmp", Fcmp);
        dCmd.Parameters.AddWithValue("@Ficp", Ficp);
        dCmd.Parameters.AddWithValue("@CompanyID", CompanyID);
        dCmd.Parameters.AddWithValue("@useridp", userid);
        dCmd.Parameters.AddWithValue("@saveflagp", saveflag);
        dCmd.Parameters.AddWithValue("@idp", intId);
        return dCmd.ExecuteNonQuery();
    }

    //------------Mechanical Fatigue Factor

    public static int MechSave(SqlConnection conn, int ProcessareaID, int EqupID, int CompID, int DfbPF, int DfbAS, decimal FfbAS, int DfbCF, decimal Dfb, decimal FCA, decimal FPC, string FCP, decimal FJB, decimal FBD, decimal Df, int userid, string saveflag, int intId)
    {
        SqlCommand dCmd = new SqlCommand("[sp_Mech_Save]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@ProcessareaIDp", ProcessareaID);
        dCmd.Parameters.AddWithValue("@EqupIDp", EqupID);
        dCmd.Parameters.AddWithValue("@CompIDp", CompID);
        dCmd.Parameters.AddWithValue("@DfbPFp", DfbPF);
        dCmd.Parameters.AddWithValue("@DfbASp", DfbAS);
        dCmd.Parameters.AddWithValue("@FfbASp", FfbAS);
        dCmd.Parameters.AddWithValue("@DfbCFp", DfbCF);
        dCmd.Parameters.AddWithValue("@Dfbp", Dfb);
        dCmd.Parameters.AddWithValue("@FCAp", FCA);
        dCmd.Parameters.AddWithValue("@FPCp", FPC);
        dCmd.Parameters.AddWithValue("@FCPp", FCP);
        dCmd.Parameters.AddWithValue("@FJBp", FJB);
        dCmd.Parameters.AddWithValue("@FBDp", FBD);
        dCmd.Parameters.AddWithValue("@Dfp", Df);
        dCmd.Parameters.AddWithValue("@useridp", userid);
        dCmd.Parameters.AddWithValue("@saveflagp", saveflag);
        dCmd.Parameters.AddWithValue("@idp", intId);
        return dCmd.ExecuteNonQuery();
    }


    //-------------SCC Damage Factor
    public static int SulfideCrackingSave(SqlConnection conn, int ProcessareaID, int EqupID, int CompID, int ageline, string InsEffp, int NofInsp,DateTime InspectDate ,string Svi, int SviVal, double DfCausp, string pHWater, string H2S, string Severity, string Heat, string Brinnell, int userid, string saveflag, int intId)
    {
        SqlCommand dCmd = new SqlCommand("[sp_Sulfide_Save]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@ProcessareaIDp", ProcessareaID);
        dCmd.Parameters.AddWithValue("@EqupIDp", EqupID);
        dCmd.Parameters.AddWithValue("@CompIDp", CompID);
        dCmd.Parameters.AddWithValue("@agep", ageline);
        dCmd.Parameters.AddWithValue("@InsEffp", InsEffp);
        dCmd.Parameters.AddWithValue("@NofInsp", NofInsp);
        dCmd.Parameters.AddWithValue("@InspectDate", InspectDate);
        dCmd.Parameters.AddWithValue("@Svi", Svi);
        dCmd.Parameters.AddWithValue("@SviVal", SviVal);
        dCmd.Parameters.AddWithValue("@DfCausp", DfCausp);
        dCmd.Parameters.AddWithValue("@pHWaterp", pHWater);
        dCmd.Parameters.AddWithValue("@H2Sp", H2S);
        dCmd.Parameters.AddWithValue("@Heatp", Heat);
        dCmd.Parameters.AddWithValue("@Brinnellp", Brinnell);
        dCmd.Parameters.AddWithValue("@Severityp", Severity);
        dCmd.Parameters.AddWithValue("@useridp", userid);
        dCmd.Parameters.AddWithValue("@saveflagp", saveflag);
        dCmd.Parameters.AddWithValue("@idp", intId);
        return dCmd.ExecuteNonQuery();
    }
    public static int SulfideSave(SqlConnection conn, int ProcessareaID, int EqupID, int CompID, int ageline, string InsEffp, int NofInsp, DateTime InspectDate, string Svi, int SviVal, double DfCausp, string pHWater, string H2S, int userid, string saveflag, int intId)
    {
        SqlCommand dCmd = new SqlCommand("[sp_Carbanate_Save]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@ProcessareaIDp", ProcessareaID);
        dCmd.Parameters.AddWithValue("@EqupIDp", EqupID);
        dCmd.Parameters.AddWithValue("@CompIDp", CompID);
        dCmd.Parameters.AddWithValue("@agep", ageline);
        dCmd.Parameters.AddWithValue("@InsEffp", InsEffp);
        dCmd.Parameters.AddWithValue("@NofInsp", NofInsp);
        dCmd.Parameters.AddWithValue("@InspectDate", InspectDate);
        dCmd.Parameters.AddWithValue("@Svi", Svi);
        dCmd.Parameters.AddWithValue("@SviVal", SviVal);
        dCmd.Parameters.AddWithValue("@DfCausp", DfCausp);
        dCmd.Parameters.AddWithValue("@pHWaterp", pHWater);
        dCmd.Parameters.AddWithValue("@H2Sp", H2S);
        //dCmd.Parameters.AddWithValue("@Severityp", Severity);
        dCmd.Parameters.AddWithValue("@useridp", userid);
        dCmd.Parameters.AddWithValue("@saveflagp", saveflag);
        dCmd.Parameters.AddWithValue("@idp", intId);
        return dCmd.ExecuteNonQuery();
    }

    public static int CausticAmineSave(SqlConnection conn, int ProcessareaID, int EqupID, int CompID, int ageline, string InsEffp, int NofInsp,DateTime InspectDate,string Svi, int SviVal, double DfCausp, int userid, string saveflag, int intId)
    {
        SqlCommand dCmd = new SqlCommand("[sp_CausticAmine_Save]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@ProcessareaIDp", ProcessareaID);
        dCmd.Parameters.AddWithValue("@EqupIDp", EqupID);
        dCmd.Parameters.AddWithValue("@CompIDp", CompID);
        dCmd.Parameters.AddWithValue("@agep", ageline);
        dCmd.Parameters.AddWithValue("@InsEffp", InsEffp);
        dCmd.Parameters.AddWithValue("@NofInsp", NofInsp);
        dCmd.Parameters.AddWithValue("@InspectDate", InspectDate);
        dCmd.Parameters.AddWithValue("@Svi", Svi);
        dCmd.Parameters.AddWithValue("@SviVal", SviVal);
        dCmd.Parameters.AddWithValue("@DfCausp", DfCausp);
        dCmd.Parameters.AddWithValue("@useridp", userid);
        dCmd.Parameters.AddWithValue("@saveflagp", saveflag);
        dCmd.Parameters.AddWithValue("@idp", intId);
        return dCmd.ExecuteNonQuery();
    }

    public static int POFtotalSave(SqlConnection conn, int ProcessareaID, int EqupID, int CompID, string DfThin, string DfSCC, string Dfextd, string Dfbrit, string Dftotal, string Category, int userid, string saveflag, int intId)
    {
        SqlCommand dCmd = new SqlCommand("[sp_POF_Total_Save]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@ProcessareaIDp", ProcessareaID);
        dCmd.Parameters.AddWithValue("@EqupIDp", EqupID);
        dCmd.Parameters.AddWithValue("@CompIDp", CompID);
        dCmd.Parameters.AddWithValue("@DfThinp", DfThin);
        dCmd.Parameters.AddWithValue("@DfSCCp", DfSCC);
        dCmd.Parameters.AddWithValue("@Dfextdp", Dfextd);
        dCmd.Parameters.AddWithValue("@Dfbritp", Dfbrit);
        dCmd.Parameters.AddWithValue("@Dftotalp", Dftotal);
        dCmd.Parameters.AddWithValue("@Categoryp", Category);
        dCmd.Parameters.AddWithValue("@useridp", userid);
        dCmd.Parameters.AddWithValue("@saveflagp", saveflag);
        dCmd.Parameters.AddWithValue("@idp", intId);
        return dCmd.ExecuteNonQuery();
    }

    // -------------------------------------------POF End--------------------------------------

    // -------------------------------------------Financial COF--------------------------------------

    public static int Fin_COF_Save(SqlConnection conn, int ProcessareaID, int EqupID, int CompID,string EquipNO,string CompName,string HCSmall, string HCMedium, string HCLarge, string HCRupture, string OutSmall, string OutMedium, string OutLarge, string OutRupture, string MatCost, string FCcmd, string CAcmd, string Outcmd, string CAInj, string EqcostDollar, string Eqcostft2, string FCaffa, string ProCost, string Outaffa, string FCprod, string InjCost, string FCInj, string SUMPFC, string Category, int userid, string saveflag)
    {
        SqlCommand dCmd = new SqlCommand("[sp_FinCOF_Save]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@ProcessareaIDp", ProcessareaID);
        dCmd.Parameters.AddWithValue("@EqupIDp", EqupID);
        dCmd.Parameters.AddWithValue("@CompIDp", CompID);

        dCmd.Parameters.AddWithValue("@EquipNO", EquipNO);
        dCmd.Parameters.AddWithValue("@CompName", CompName);
        dCmd.Parameters.AddWithValue("@HCSmallp", HCSmall);
        dCmd.Parameters.AddWithValue("@HCMediump", HCMedium);
        dCmd.Parameters.AddWithValue("@HCLargep", HCLarge);
        dCmd.Parameters.AddWithValue("@HCRupturep", HCRupture);
        dCmd.Parameters.AddWithValue("@OutSmallp", OutSmall);
        dCmd.Parameters.AddWithValue("@OutMediump", OutMedium);
        dCmd.Parameters.AddWithValue("@OutLargep", OutLarge);
        dCmd.Parameters.AddWithValue("@OutRupturep", OutRupture);
        dCmd.Parameters.AddWithValue("@MatCostp", MatCost);
        dCmd.Parameters.AddWithValue("@FCcmdp", FCcmd);
        dCmd.Parameters.AddWithValue("@CAcmdp", CAcmd);
        dCmd.Parameters.AddWithValue("@Outcmdp", Outcmd);
        dCmd.Parameters.AddWithValue("@CAInjp", CAInj);
        dCmd.Parameters.AddWithValue("@EqcostDollarp", EqcostDollar);
        dCmd.Parameters.AddWithValue("@Eqcostft2p", Eqcostft2);
        dCmd.Parameters.AddWithValue("@FCaffap", FCaffa);
        dCmd.Parameters.AddWithValue("@ProCostp", ProCost);
        dCmd.Parameters.AddWithValue("@Outaffap", Outaffa);

        dCmd.Parameters.AddWithValue("@FCprodp", FCprod);
        dCmd.Parameters.AddWithValue("@InjCostp", InjCost);
        dCmd.Parameters.AddWithValue("@FCInjp", FCInj);
        dCmd.Parameters.AddWithValue("@SUMPFCp", SUMPFC);
        dCmd.Parameters.AddWithValue("@Categoryp", Category);

        dCmd.Parameters.AddWithValue("@useridp", userid);
        dCmd.Parameters.AddWithValue("@saveflagp", saveflag);

        return dCmd.ExecuteNonQuery();
    }
    // -------------------------------------------Financial COF End--------------------------------------
    // -------------------------------------------Inspection Plan--------------------------------------
    public static int InspectionPlan(SqlConnection conn, int ProcessareaID, int EqupID, int CompID, string DamageFact, string InspectCate, DateTime InspectDate, string saveflag)
    {
        SqlCommand dCmd = new SqlCommand("[sp_InspectEffectPlan_Save]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@ProcessareaIDp", ProcessareaID);
        dCmd.Parameters.AddWithValue("@EqupIDp", EqupID);
        dCmd.Parameters.AddWithValue("@CompIDp", CompID);
        dCmd.Parameters.AddWithValue("@DamageFactp", DamageFact);
        dCmd.Parameters.AddWithValue("@InspectCatep", InspectCate);
        dCmd.Parameters.AddWithValue("@InspectDatep", InspectDate);
        dCmd.Parameters.AddWithValue("@saveflagp", saveflag);
        return dCmd.ExecuteNonQuery();
    }
    // -------------------------------------------Inspection Plan End--------------------------------------

    //--------------------------------------------RiskRanking----------------------------------------------
    public static int RiskRankingSave(SqlConnection conn, int ProcessareaID, int EqupID, int CompID,string EquipType,string CompName, string POFval,string POFCate,string COFVal,string COFCate,string FinCOFval,string FinCOFCate,string MaxCOF,string MaxVal,string COFRisk,string FinCOFRisk,string ChooseRisk,string SelectRisk, int userid, string saveflag)
    {
        SqlCommand dCmd = new SqlCommand("[sp_RiskRanking_Save]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@ProcessareaIDp", ProcessareaID);
        dCmd.Parameters.AddWithValue("@EqupIDp", EqupID);
        dCmd.Parameters.AddWithValue("@CompIDp", CompID);
        dCmd.Parameters.AddWithValue("@EquipTypep", EquipType);
        dCmd.Parameters.AddWithValue("@CompNamep", CompName);
        dCmd.Parameters.AddWithValue("@POFvalp", POFval);
        dCmd.Parameters.AddWithValue("@POFCatep", POFCate);
        dCmd.Parameters.AddWithValue("@COFValp", COFVal);
        dCmd.Parameters.AddWithValue("@COFCatep", COFCate);
        dCmd.Parameters.AddWithValue("@FinCOFvalp", FinCOFval);
        dCmd.Parameters.AddWithValue("@FinCOFCatep", FinCOFCate);
        dCmd.Parameters.AddWithValue("@MaxCOF", MaxCOF);
        dCmd.Parameters.AddWithValue("@MaxVal", MaxVal);
        dCmd.Parameters.AddWithValue("@COFRiskp", COFRisk);
        dCmd.Parameters.AddWithValue("@FinCOFRiskp", FinCOFRisk);
        dCmd.Parameters.AddWithValue("@ChooseRiskp", ChooseRisk);
        dCmd.Parameters.AddWithValue("@SelectRiskp", SelectRisk);
        dCmd.Parameters.AddWithValue("@useridp", userid);
        dCmd.Parameters.AddWithValue("@saveflagp", saveflag);
       
        return dCmd.ExecuteNonQuery();
    }
    public static int RiskRankingloop(SqlConnection conn, int ProcessareaID, int EqupID, int CompID, string ChooseRisk, string SelectRisk, int userid, string saveflag)
    {
        SqlCommand dCmd = new SqlCommand("[sp_RiskRanking_loop]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@ProcessareaIDp", ProcessareaID);
        dCmd.Parameters.AddWithValue("@EqupIDp", EqupID);
        dCmd.Parameters.AddWithValue("@CompIDp", CompID);
        dCmd.Parameters.AddWithValue("@ChooseRiskp", ChooseRisk);
        dCmd.Parameters.AddWithValue("@SelectRiskp", SelectRisk);
        dCmd.Parameters.AddWithValue("@useridp", userid);
        dCmd.Parameters.AddWithValue("@saveflagp", saveflag);

        return dCmd.ExecuteNonQuery();
    }
    //-----------------------------------------------RiskRanking End-----------------------------------------

    public static int SaveSupplier(SqlConnection conn, string name, string address1, string address2, string city, string state, string country, string postcode, string desc, string phone, string fax, string email, string website, string userid, string saveflag, string suppid)
    {
        string sp_Name = "";
        string RowValue = "0";
        if (saveflag.ToString() == "N")
            sp_Name = "sp_Supplier_Ins";

        if (saveflag.ToString() == "U")
            sp_Name = "sp_Supplier_Up";

        if (saveflag.ToString() == "D")
            sp_Name = "sp_Supplier_Del";


        SqlCommand dCmd = new SqlCommand(sp_Name, conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        if (saveflag.ToString() == "U")
        {
            dCmd.Parameters.AddWithValue("@idp", suppid);
            dCmd.Parameters.AddWithValue("@namep", name);
            dCmd.Parameters.AddWithValue("@Rowp", RowValue);
        }
        if (saveflag.ToString() == "D")
            dCmd.Parameters.AddWithValue("@idp", suppid);

        if (saveflag.ToString() == "N")
            dCmd.Parameters.AddWithValue("@namep", name);

        if ((saveflag.ToString() == "N") || (saveflag.ToString() == "U"))
        {
            dCmd.Parameters.AddWithValue("@address1p", address1);
            dCmd.Parameters.AddWithValue("@address2p", address2);
            dCmd.Parameters.AddWithValue("@cityp", city);
            dCmd.Parameters.AddWithValue("@statep", state);
            dCmd.Parameters.AddWithValue("@countryp", country);
            dCmd.Parameters.AddWithValue("@descriptionp", desc);
            dCmd.Parameters.AddWithValue("@contactnop", phone);
            dCmd.Parameters.AddWithValue("@faxnop", fax);
            dCmd.Parameters.AddWithValue("@emailp", email);
            dCmd.Parameters.AddWithValue("@websitep", website);
            dCmd.Parameters.AddWithValue("@postcodep", postcode);
            dCmd.Parameters.AddWithValue("@useridp", userid);
        }
        return dCmd.ExecuteNonQuery();
    }

    public static SqlDataReader checkUserLoginId(SqlConnection connCheck, string strLoginId, int strstaffid)
    {
        SqlCommand cmd = new SqlCommand("[sp_MasterUser_IsDuplicate]", connCheck);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@loginidp", strLoginId);
        cmd.Parameters.AddWithValue("@staffidp", strstaffid);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    public static int SaveProcessarea(SqlConnection conn, string processarea, string description, string unit,int CompanyID, int userid, string saveflag, int intId)
    {
        SqlCommand dCmd = new SqlCommand("[sp_processarea_Save]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@idp", intId);
        dCmd.Parameters.AddWithValue("@processareap", processarea);
        dCmd.Parameters.AddWithValue("@descriptionp", description);
        dCmd.Parameters.AddWithValue("@unitp", unit);
        dCmd.Parameters.AddWithValue("@CompanyID", CompanyID);
        dCmd.Parameters.AddWithValue("@useridp", userid);
        dCmd.Parameters.AddWithValue("@saveflag", saveflag);
        return dCmd.ExecuteNonQuery();
    }

    public static int SaveDept(SqlConnection conn, string deptcode, string deptname, int companyid, string userid, string saveflag, int deptid)
    {
        SqlCommand dCmd = new SqlCommand("[sp_Department_Save]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@idp", deptid);
        dCmd.Parameters.AddWithValue("@companyidp", companyid);
        dCmd.Parameters.AddWithValue("@deptcodep", deptcode);
        dCmd.Parameters.AddWithValue("@deptnamep", deptname);
        dCmd.Parameters.AddWithValue("@useridp", userid);
        dCmd.Parameters.AddWithValue("@saveflag", saveflag);
        return dCmd.ExecuteNonQuery();
    }

    public static int SaveStaff(SqlConnection conn, string strCode, string strName, string strDesignation, string strLoc, int intDeptId, int intwarehseId, string strPhone, string strEmail, string strUserId, string strFlag, int intId)
    {
        SqlCommand dCmd = new SqlCommand("[sp_Staff_Save]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@idp", intId);
        dCmd.Parameters.AddWithValue("@staffnop", strCode);
        dCmd.Parameters.AddWithValue("@namep", strName);
        dCmd.Parameters.AddWithValue("@designationp", strDesignation);
        dCmd.Parameters.AddWithValue("@deptidp", intDeptId);

        dCmd.Parameters.AddWithValue("@warehseIdp", intwarehseId);
        dCmd.Parameters.AddWithValue("@locationp", strLoc);
        dCmd.Parameters.AddWithValue("@phonep", strPhone);
        dCmd.Parameters.AddWithValue("@emailp", strEmail);
        dCmd.Parameters.AddWithValue("@useridp", strUserId);
        dCmd.Parameters.AddWithValue("@saveflag", strFlag);
        return dCmd.ExecuteNonQuery();
    }

    public static int SaveUser(SqlConnection conn, string strloginid, string strpassword, int intStaffid, string strCategory, int strUserid, string saveflag, int id, int intComp, int intDept, int intProd, int intSupp, int intStaff, int intUser, int intIn, int intOut, int intReport, int intwarh)
    {
        SqlCommand dCmd = new SqlCommand("sp_User_Save", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@idp", id);
        dCmd.Parameters.AddWithValue("@loginidp", strloginid);
        dCmd.Parameters.AddWithValue("@passp", strpassword);
        dCmd.Parameters.AddWithValue("@staffidp", intStaffid);
        dCmd.Parameters.AddWithValue("@categoryp", strCategory);
        dCmd.Parameters.AddWithValue("@saveflag", saveflag);
        dCmd.Parameters.AddWithValue("@useridp", strUserid);

        dCmd.Parameters.AddWithValue("@companyp", intComp);
        dCmd.Parameters.AddWithValue("@deptp", intDept);
        dCmd.Parameters.AddWithValue("@prodp", intProd);
        dCmd.Parameters.AddWithValue("@supplierp", intSupp);
        dCmd.Parameters.AddWithValue("@staffp", intStaff);
        dCmd.Parameters.AddWithValue("@userp", intUser);
        dCmd.Parameters.AddWithValue("@incomep", intIn);
        dCmd.Parameters.AddWithValue("@outg", intOut);
        dCmd.Parameters.AddWithValue("@reportp", intReport);
        dCmd.Parameters.AddWithValue("@iwarhp", intwarh);
        return dCmd.ExecuteNonQuery();
    }

    public static int EquipmentMaster(SqlConnection conn, int processarea, string EquipID, string Equiptype, string description, string doshno, string PID, string IPLayer, string Intrusive, string yearinstall, int WTM, string histdescrip, string inspectech, string inspecscope, string RBIObserv, string DOSHobserv,byte EquipImage,string DesignCode,int CompanyID,int strUserId, string strFlag, int intId)
    {
        SqlCommand dCmd = new SqlCommand("[sp_EquipmentMaster_Save]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@idp", intId);
        dCmd.Parameters.AddWithValue("@processareap", processarea);
        dCmd.Parameters.AddWithValue("@EquipIDp", EquipID);
        dCmd.Parameters.AddWithValue("@Equiptypep", Equiptype);
        dCmd.Parameters.AddWithValue("@descriptionp", description);
        dCmd.Parameters.AddWithValue("@doshnop", doshno);
        dCmd.Parameters.AddWithValue("@PIDp", PID);
        dCmd.Parameters.AddWithValue("@IPLayerp", IPLayer);
        dCmd.Parameters.AddWithValue("@Intrusivep", Intrusive);
        dCmd.Parameters.AddWithValue("@yearinstallp", yearinstall);
        dCmd.Parameters.AddWithValue("@WTMp", WTM);
        dCmd.Parameters.AddWithValue("@histdescripp", histdescrip);
        dCmd.Parameters.AddWithValue("@inspectechp", inspectech);
        dCmd.Parameters.AddWithValue("@inspecscopep", inspecscope);
        dCmd.Parameters.AddWithValue("@RBIObservp", RBIObserv);
        dCmd.Parameters.AddWithValue("@DOSHobservp", DOSHobserv);
        dCmd.Parameters.AddWithValue("@EquipImagep", EquipImage);
        dCmd.Parameters.AddWithValue("@DesignCodep", DesignCode);
        dCmd.Parameters.AddWithValue("@CompanyID", CompanyID);
        dCmd.Parameters.AddWithValue("@useridp", strUserId);
        dCmd.Parameters.AddWithValue("@saveflag", strFlag);
        return dCmd.ExecuteNonQuery();
    }

    public static int Component(SqlConnection conn, int ProcessareaID, int EqupID, string CompNo, string CompName, string Insulated, string Painting, string Materialtype, string materialspecification, string NormalThickness, string ConstThickness, string MRT, string Designpressure, string DesignTemp, string OPPressure, string OPTemp, string CorrosionAllownce, string InspectionEffective, string ExpectedRate, int NoofInspection, string clad, double Defaultvalue, int CompanyID,int userid, string @saveflag, int intId) //  
    {
        SqlCommand dCmd = new SqlCommand("[sp_Component_Save]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@idp", intId);
        dCmd.Parameters.AddWithValue("@ProcessareaIDp", ProcessareaID);
        dCmd.Parameters.AddWithValue("@EqupIDp", EqupID);
        dCmd.Parameters.AddWithValue("@CompNop", CompNo);
        dCmd.Parameters.AddWithValue("@CompNamep", CompName);
        dCmd.Parameters.AddWithValue("@Insulatedp", Insulated);
        dCmd.Parameters.AddWithValue("@Paintingp", Painting);
        dCmd.Parameters.AddWithValue("@Materialtypeg", Materialtype);
        dCmd.Parameters.AddWithValue("@materialspecificationp", materialspecification);
        dCmd.Parameters.AddWithValue("@NormalThicknessp", NormalThickness);
        dCmd.Parameters.AddWithValue("@ConstThicknessp", ConstThickness);
        dCmd.Parameters.AddWithValue("@MRTp", MRT);
        dCmd.Parameters.AddWithValue("@Designpressurep", Designpressure);
        dCmd.Parameters.AddWithValue("@DesignTempp", DesignTemp);
        dCmd.Parameters.AddWithValue("@OPPressurep", OPPressure);
        dCmd.Parameters.AddWithValue("@OPTempp", OPTemp);
        dCmd.Parameters.AddWithValue("@CorrosionAllowncep", CorrosionAllownce);
        dCmd.Parameters.AddWithValue("@InspectionEffectivep", InspectionEffective);
        dCmd.Parameters.AddWithValue("@ExpectedRatep", ExpectedRate);
        dCmd.Parameters.AddWithValue("@NoofInspectionp", NoofInspection);
        dCmd.Parameters.AddWithValue("@cladp", clad);
        dCmd.Parameters.AddWithValue("@Defaultvalue", Defaultvalue);
        dCmd.Parameters.AddWithValue("@CompanyID", CompanyID);
        dCmd.Parameters.AddWithValue("@useridp", userid);
        dCmd.Parameters.AddWithValue("@saveflagp", @saveflag);
        return dCmd.ExecuteNonQuery();
    }
    public static int Componentdel(SqlConnection conn, int ProcessareaID, int EqupID, string CompNo, string CompName, string @saveflag, int intId) //   int NoofInspection, string clad,
    {
        SqlCommand dCmd = new SqlCommand("[sp_Component_Del]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@idp", intId);
        dCmd.Parameters.AddWithValue("@ProcessareaIDp", ProcessareaID);
        dCmd.Parameters.AddWithValue("@EqupIDp", EqupID);
        dCmd.Parameters.AddWithValue("@CompNop", CompNo);
        dCmd.Parameters.AddWithValue("@CompNamep", CompName);
        dCmd.Parameters.AddWithValue("@saveflagp", @saveflag);
        return dCmd.ExecuteNonQuery();
    }


    public static int Del(SqlConnection conn, string @saveflag, int intId, string EqupID, string CompNo)
    {
        SqlCommand dCmd = new SqlCommand("[sp_Del_Save]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@idp", intId);
        dCmd.Parameters.AddWithValue("@EqupIDp", EqupID);
        dCmd.Parameters.AddWithValue("@CompIDp", CompNo);
        dCmd.Parameters.AddWithValue("@saveflagp", @saveflag);
        return dCmd.ExecuteNonQuery();
    }
    public static int Inspection(SqlConnection conn, string EqupID, string CompID, DateTime inspecdate, string inspecpoint, string value, int userid, string @saveflag, int intId)
    {
        SqlCommand dCmd = new SqlCommand("[sp_Inspection_Save]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@idp", intId);
        dCmd.Parameters.AddWithValue("@CompIDp", CompID);
        dCmd.Parameters.AddWithValue("@EqupIDp", EqupID);
        dCmd.Parameters.AddWithValue("@inspecdatep", inspecdate);
        dCmd.Parameters.AddWithValue("@inspecpointp", inspecpoint);
        dCmd.Parameters.AddWithValue("@valuep", value);
        dCmd.Parameters.AddWithValue("@useridp", userid);
        dCmd.Parameters.AddWithValue("@saveflagp", @saveflag);
        return dCmd.ExecuteNonQuery();
    }
    public static int InsertLogTable(SqlConnection connSave, string userid, string param)
    {
        SqlCommand dCmd = new SqlCommand("[sp_Insert_LogTable]", connSave);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@useridp", userid);
        dCmd.Parameters.AddWithValue("@paramp", param);
        //'dCmd.Parameters.AddWithValue("@lgin", lgin);
        return dCmd.ExecuteNonQuery();
    }
    public static int SaveUserDetails(SqlConnection conn, string StaffName, string Position, string UserID, string Password, int Company, string CompanyName, string Createdby, string SaveFlag, string UpID)
    {
        string sp_Name = "";
        if (SaveFlag.ToString() == "Insert")
            sp_Name = "sp_user_tbl_insert";

        if (SaveFlag.ToString() == "Update")
            sp_Name = "sp_user_tbl_update";

        if (SaveFlag.ToString() == "Delete")
            sp_Name = "[sp_user_tbl_delete]";

        SqlCommand dCmd = new SqlCommand(sp_Name, conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        if ((SaveFlag.ToString() == "Update") || (SaveFlag.ToString() == "Delete"))
        {
            dCmd.Parameters.AddWithValue("@id", UpID);
        }
        if ((SaveFlag.ToString() == "Update") || (SaveFlag.ToString() == "Insert"))
        {
            dCmd.Parameters.AddWithValue("@StaffName", StaffName.ToString().Trim());
            dCmd.Parameters.AddWithValue("@Position", Position.ToString().Trim());
            dCmd.Parameters.AddWithValue("@UserID", UserID.ToString().Trim());
            dCmd.Parameters.AddWithValue("@Password", Password.ToString().Trim());
            dCmd.Parameters.AddWithValue("@Company", Company.ToString().Trim());
            dCmd.Parameters.AddWithValue("@CompanyName", CompanyName.ToString().Trim());
            dCmd.Parameters.AddWithValue("@Createdby", Createdby.ToString().Trim());
        }
        return dCmd.ExecuteNonQuery();
    }
}