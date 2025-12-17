using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using System.Net;

using System.Globalization;
using System.Collections;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.OleDb;
using System.Drawing;
using Stimulsoft.Report;
using Stimulsoft.Report.Web;
using Stimulsoft.Report.Viewer;
using Stimulsoft.Report.SaveLoad;
using Stimulsoft.Report.Export;
using Stimulsoft.Report.Print;
using Stimulsoft.Base;
using Stimulsoft.Controls;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report.Controls;
using System.IO;
using System.Web.SessionState;
using System.Runtime;
using Excel = Microsoft.Office.Interop.Excel; 


public partial class ReportsCOFFlame : System.Web.UI.Page
{
    public DataTable dtMenuItems = new DataTable();

    public DataTable dtSubMenuItems = new DataTable();

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            if (!(string.IsNullOrEmpty(Session["sesUserID"].ToString())))
            {
                SqlConnection connMenu = BusinessTier.getConnection();
                connMenu.Open();
                SqlDataReader readerMenu = BusinessTier.getMenuList(connMenu, Session["sesUserID"].ToString());
                dtMenuItems.Load(readerMenu);
                BusinessTier.DisposeReader(readerMenu);
                BusinessTier.DisposeConnection(connMenu);

            }
            else
            {
                Response.Redirect("Login.aspx");
            }
            string appPath = Request.PhysicalApplicationPath;
            string srtParamval1 = "", srtParamval2 = "",srtParamval3 = "" ;
            srtParamval1 = Request.QueryString.Get("param1").ToString();
            srtParamval2 = Request.QueryString.Get("param2").ToString();
            srtParamval3 = Request.QueryString.Get("param3").ToString();
           // string strAttachmentpath = ConfigurationManager.AppSettings["WC_AttachementPath"].ToString();
            //string strMessage = "";
            //string strSubject = "";

            string con = BusinessTier.getConnection1();
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string path = string.Empty;
            string sqldatasource = "COF_Flammable";
            string sql = "Select  *  from VW_COF_Flammable  where ProcID='" + srtParamval1.ToString() + "' and  EquID='" + srtParamval2.ToString().Trim() + "' and CompID ='" + srtParamval3.ToString().Trim() + "' and  Deleted=0";
            SqlCommand cmddup = new SqlCommand(sql, conn);
            SqlDataReader readerdup = cmddup.ExecuteReader();

            if (readerdup.Read())
            {
                double ptrans = Convert.ToDouble(readerdup["ptrans"].ToString());
                double oppress = Convert.ToDouble(readerdup["OpPres"].ToString());
                if (readerdup["Fluid"].ToString() == "Liquid")
                {
                    path = "C:\\inetpub\\wwwroot\\RBI\\Reports\\COF_Flammable_Liquid.mrt";
                }
                else
                {
                    if (oppress < ptrans)
                    {
                        path = "c:\\inetpub\\wwwroot\\RBI\\Reports\\COF_Flammable_Vapour_Sonic.mrt";
                    }
                    else
                    {
                        path = "c:\\inetpub\\wwwroot\\RBI\\Reports\\COF_Flammable_Vapour_SubSonic.mrt";
                    }
                }
            }
            else
            {
                lblStatus.Text = "Data does not exist on selected Equipment and Component.";
                lblStatus.ForeColor = Color.Blue;
                return;
            }


            SqlDataAdapter ad1 = new SqlDataAdapter(sql, con);

            DataSet ds1 = new DataSet();
            ds1.DataSetName = "DynamicDataSource1";
            ds1.Tables.Add(sqldatasource);
            ad1.Fill(ds1, sqldatasource);
            Stimulsoft.Report.StiReport stiReport1;
            stiReport1 = new StiReport();
            stiReport1.Dictionary.DataStore.Clear();
            stiReport1.ClearAllStates();
            stiReport1.Load(path);
            stiReport1.Dictionary.Databases.Clear();
            stiReport1.Dictionary.Databases.Add(new StiSqlDatabase("Connection", con));
            stiReport1.Dictionary.DataSources.Clear();
            stiReport1.RegData(sqldatasource, ds1);
            stiReport1.Dictionary.Synchronize();
            stiReport1.Compile();
            stiReport1.Render();
            StiWebViewer1.Report = stiReport1;
            StiWebViewer1.ViewMode = StiWebViewMode.WholeReport;
            stiReport1.Dispose();
        }
        catch (Exception Ex)
        {
            lblStatus.Text = Ex.Message.ToString();
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "PageInit", "ReportsCOFFlame", Ex.ToString(), "Audit");
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(Session["sesUserID"].ToString()))
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                lblStatus.Text = "";

            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
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