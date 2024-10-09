﻿using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.OleDb;
using System.Drawing;

public partial class RBILogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtLogin.Focus();
        Session["sesUserID"] = 0;
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        Session.Contents.Clear();
        txtLogin.Text = "admin";
        txtPassword.Text = "admin";
    }
    protected void LoginBtn_Click(object sender, EventArgs e)
    {
       
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            BusinessTier.BindErrorMessageDetails(conn);
            BusinessTier.DisposeConnection(conn);
            DateTime logintime = System.DateTime.Now;
            int flag = 0;
            int intValidation = 0;
            SqlConnection con = BusinessTier.getConnection();
            con.Open();
            SqlDataReader reader1 = BusinessTier.VaildateUserLogin(con, txtLogin.Text.ToString(), txtPassword.Text.ToString());
            if (reader1.Read())
            {
                if (!(string.IsNullOrEmpty(reader1["ID"].ToString())))
                {
                    flag = 1;
                    Session["sesUserID"] = (reader1["ID"].ToString().Trim());
                    Session["sesUserName"] = (reader1["StaffName"].ToString().Trim());
                    Session["sesCompanyID"] = (reader1["Company"].ToString().Trim());
                    Session["sesCompanyName"] = (reader1["CompanyName"].ToString().Trim());
                    //string a, b, c, d;
                    //a = Session["sesUserID"].ToString().Trim();
                    //b = Session["sesUserName"].ToString().Trim();
                    //c = Session["sesCompanyID"].ToString().Trim();
                    //d = Session["sesCompanyName"].ToString().Trim();
                }
                else
                {
                    intValidation = 1;
                }
            }
            BusinessTier.DisposeReader(reader1);

            if (flag >= 1)
            {
               
                Response.Redirect("RBIHome.aspx", false);
            }
            else
            {
                ShowMessage(7);
                return;
            }
            //InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "MasterLogin", "SignIn", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(7);
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "MasterLogin", "SignIn", ex.ToString(), "Audit");
        }

    }
    private void ShowMessage(int errorNo)
    {
        lblStatus.Text = BusinessTier.g_ErrorMessagesDataTable.Rows[errorNo - 1]["Message"].ToString();
        System.Drawing.ColorConverter colConvert = new ColorConverter();
        string strColor = BusinessTier.g_ErrorMessagesDataTable.Rows[errorNo - 1]["Color"].ToString();
        lblStatus.ForeColor = (System.Drawing.Color)colConvert.ConvertFromString(strColor);
    }

    private void InsertLogAuditTrail(string userid, string module, string activity, string result, string flag)
    {
        SqlConnection connLog = BusinessTier.getConnection();
        connLog.Open();
        BusinessTier.InsertLogAuditTrial(connLog, userid, module, activity, result, flag);
        BusinessTier.DisposeConnection(connLog);
    }
}