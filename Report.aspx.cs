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
//using Excel = Microsoft.Office.Interop.Excel;
using ClosedXML.Excel;

public partial class Report : System.Web.UI.Page
{
    public DataTable dtMenuItems = new DataTable();

    public DataTable dtSubMenuItems = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
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
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }


    }

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
            lblName.Text = "Hi, " + Session["sesUserName"].ToString();
            
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
        StiWebViewer1.Visible = false;
        btnlastinspection.Visible = false;
    }

    protected void Onclick_btnlastinspection(object sender, EventArgs e)
    {
       
        try
        {
            string con = BusinessTier.getConnection1();
            StiWebViewer1.Visible = true;
            string sqldatasource1 = string.Empty, sql1 = string.Empty, path = string.Empty;
            DataSet ds1 = new DataSet();
             sqldatasource1 = "VW_Inspection";
             sql1 = "select *,CONVERT(VARCHAR(10), Initialdate, 103) AS [Indate],Initialvalue,CONVERT(VARCHAR(10), InspecDate, 103) AS [Insdate],CONVERT(VARCHAR(10), Previousdate, 103) AS [Predate] from VW_Inspection where EquAutoID='" + cboEquipment.SelectedValue.ToString().Trim() + "' and CompAutoID='" + cboComponent.SelectedValue.ToString().Trim() + "' and   Deleted=0 and LongCRrate is not null order by EquAutoID,InspectionPointNo,InspecDate";
             SqlDataAdapter ad1 = new SqlDataAdapter(sql1, con);

             ds1.DataSetName = "DynamicDataSource1";
             ds1.Tables.Add(sqldatasource1);
             ad1.Fill(ds1, sqldatasource1);
       
                path = "C:\\inetpub\\wwwroot\\RBI\\Reports\\Inspection.mrt";
                Stimulsoft.Report.StiReport stiReport1;
                stiReport1 = new StiReport();
                stiReport1.Dictionary.DataStore.Clear();
                stiReport1.ClearAllStates();
                stiReport1.Load(path);
                stiReport1.Dictionary.Databases.Clear();
                stiReport1.Dictionary.Databases.Add(new StiSqlDatabase("Connection", con));
                stiReport1.Dictionary.DataSources.Clear();

                stiReport1.RegData(sqldatasource1, ds1);
                stiReport1.Dictionary.Synchronize();
                stiReport1.Compile();
                // stiReport1.Render();
                StiWebViewer1.Report = stiReport1;
                StiWebViewer1.ViewMode = StiWebViewMode.WholeReport;
                stiReport1.Dispose();

        }
        catch (Exception ex)
        {
            
            //  lblStatus.Text = "Err:Select correct Product Item" + ex.Message.ToString();
            return;
        }
           
       
    }

    protected void cboProcessArea_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {

        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {

            string sql1 = " SELECT [ProcessAreaID],[processarea] FROM [Tbl_ProcessArea] where deleted=0  and companyid= '" + Convert.ToInt32(Session["sesCompanyID"].ToString()) + "' ORDER BY [processareaid]";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["processarea"].ToString();
                item.Value = row["ProcessAreaID"].ToString();
                comboBox.Items.Add(item);
                item.DataBind();
            }
            adapter1.Dispose();
            BusinessTier.DisposeConnection(conn);

        }


        catch (Exception ex)
        {


        }

    }

    protected void OnSelectedIndexChanged_cboProcess(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        btnlastinspection.Enabled = false;
        lblStatus.Text = string.Empty;
        StiWebViewer1.Visible = false;
        RadComboBox combobox = (RadComboBox)sender;
        cboEquipment.Items.Clear();
        cboComponent.Items.Clear();
        cboEquipment.Text = string.Empty;
        cboComponent.Text = string.Empty;
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            string sql1 = "select EquAutoID,EqupType,EqupID from Tbl_EquipmentAsset where ProcessAreaID='" + cboProcessArea.SelectedValue.ToString() + "' and deleted=0 order by EqupID";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            cboEquipment.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["EqupID"].ToString();
                item.Value = row["EquAutoID"].ToString();
                string balqty = row["EquAutoID"].ToString();
                if (balqty != "")
                {
                    item.Attributes.Add("EqupType", row["EqupType"].ToString());
                    item.Attributes.Add("EqupID", row["EqupID"].ToString());
                    cboEquipment.Items.Add(item);
                }
                item.DataBind();
            }
        }
        catch (Exception ex)
        {
            BusinessTier.DisposeConnection(conn);
          //  lblStatus.Text = "Err:Select correct Product Item" + ex.Message.ToString();
            return;
        }
    }

    protected void OnSelectedIndexChanged_cboEquipment(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        btnlastinspection.Enabled = false;
        lblStatus.Text = string.Empty;
        StiWebViewer1.Visible = false;
        cboComponent.Items.Clear();
        cboComponent.Text = string.Empty;
        RadComboBox combobox = (RadComboBox)sender;
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            string sql1 = "select compautoid,CompNo,compname,InspectionEffective,NoofInspection,OPTemp,Clad,MRT,CorrosionAllownce from Tbl_EquipmentComponentDetails where EqupID='" + cboEquipment.SelectedValue.ToString() + "' and deleted=0";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            cboComponent.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["compname"].ToString();
                item.Value = row["compautoid"].ToString();
                string balqty = row["compautoid"].ToString();
                if (balqty != "")
                {
                    item.Attributes.Add("CompName", row["CompName"].ToString());
                    item.Attributes.Add("CompNo", row["CompNo"].ToString());
                    item.Attributes.Add("OPTemp", row["OPTemp"].ToString());
                    item.Attributes.Add("Clad", row["Clad"].ToString());
                    item.Attributes.Add("InspectionEffective", row["InspectionEffective"].ToString());
                    item.Attributes.Add("NoofInspection", row["NoofInspection"].ToString());
                    item.Attributes.Add("MRT", row["MRT"].ToString());
                    item.Attributes.Add("CorrosionAllownce", row["CorrosionAllownce"].ToString());
                    cboComponent.Items.Add(item);
                }
                item.DataBind();
            }
            string EquipType = cboEquipment.SelectedValue.ToString();

            BusinessTier.DisposeAdapter(adapter1);
            BusinessTier.DisposeConnection(conn);
        }
        catch (Exception ex)
        {
            BusinessTier.DisposeConnection(conn);
            //lblStatus.Text = "Err:Select correct Product Item" + ex.Message.ToString();
            return;
        }
    }

    protected void OnSelectedIndexChanged_cboComponent(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        lblStatus.Text = string.Empty;
        StiWebViewer1.Visible = false;
        btnlastinspection.Enabled = true;

    }

    protected void OnSelectedIndexChanged_cbo_Report_Select(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        lblStatus.Text = string.Empty;
        StiWebViewer1.Visible = false;
        cboProcessArea.ClearSelection();
        cboEquipment.Items.Clear();
        cboComponent.Items.Clear();
        //cboProcessArea.EmptyMessage = "Select";
        cboEquipment.Text = string.Empty;
        cboComponent.Text = string.Empty;
       // Response.Redirect(Request.Url.PathAndQuery, true);
        if (cbo_Report_Select.SelectedItem.Text == "Inspection Summary" || cbo_Report_Select.SelectedItem.Text == "POF Summary" || cbo_Report_Select.SelectedItem.Text == "Inspection Plan" || cbo_Report_Select.SelectedItem.Text == "Dosh list Summary")
        {
            btnlastinspection.Visible = false;
            btnlastinspection.Enabled = false;
            lbl_Component.Visible = false;
            lbl_Equipment.Visible = false;
            lbl_ProcessArea.Visible = false;
            cboEquipment.Visible = false;
            cboComponent.Visible = false;
            cboProcessArea.Visible = false;

        }
        else if (cbo_Report_Select.SelectedItem.Text == "Inspection Details")
        {
            btnlastinspection.Visible = true;
            btnlastinspection.Enabled = false;
            lbl_Component.Visible = true;
            lbl_Equipment.Visible = true;
            lbl_ProcessArea.Visible = true;
            cboEquipment.Visible = true;
            cboComponent.Visible = true;
            cboProcessArea.Visible = true;

        }
        else if (cbo_Report_Select.SelectedItem.Text == "Risk Ranking Chart" || cbo_Report_Select.SelectedItem.Text == "Equipment list")
        {
            btnlastinspection.Visible = false;
            btnlastinspection.Enabled = false;
            lbl_Component.Visible = false;
            lbl_Equipment.Visible = false;
            lbl_ProcessArea.Visible = true;
            cboEquipment.Visible = false;
            cboComponent.Visible = false;
            cboProcessArea.Visible = true;

        }
        else 
        {
            btnlastinspection.Visible = false;
            btnlastinspection.Enabled = false;
            lbl_Component.Visible = true;
            lbl_Equipment.Visible = true;
            lbl_ProcessArea.Visible = true;
            cboEquipment.Visible = true;
            cboComponent.Visible = true;
            cboProcessArea.Visible = true;
        }
    }

    protected void btn_Report_Submit_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        if ((string.IsNullOrEmpty(cbo_Report_Select.Text.ToString())))
        {
            lblStatus.Text = "Select Reports";
            return;
        }
        if ((string.IsNullOrEmpty(cboProcessArea.Text.ToString())) && cbo_Report_Select.Text != "Inspection Summary" && cbo_Report_Select.Text != "POF Summary" && cbo_Report_Select.Text != "Inspection Plan" && cbo_Report_Select.Text != "Dosh list Summary")
        {
            lblStatus.Text = "Select Process Area";
            return;
        }

        string con = BusinessTier.getConnection1();
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            string path = string.Empty;
            string sql = string.Empty;
            string sqldatasource = string.Empty;
            //------------COF_Flammable-----------------------------
            if (cbo_Report_Select.SelectedItem.Text == "COF_Flammable")
            {
                StiWebViewer1.Visible = true;
                if ((string.IsNullOrEmpty(cboEquipment.Text.ToString())))
                {
                    lblStatus.Text = "Select Equipment";
                    return;
                }
                if ((string.IsNullOrEmpty(cboComponent.Text.ToString())))
                {
                    lblStatus.Text = "Select Component";
                    return;
                }
                sqldatasource = "COF_Flammable";
                sql = "Select  *  from VW_COF_Flammable  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' and  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' and CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
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
            //------------COF_NonFlammable-----------------------------
            if (cbo_Report_Select.SelectedItem.Text == "COF_NonFlammable")
            {
                StiWebViewer1.Visible = true;
                if ((string.IsNullOrEmpty(cboEquipment.Text.ToString())))
                {
                    lblStatus.Text = "Select Equipment";
                    return;
                }
                if ((string.IsNullOrEmpty(cboComponent.Text.ToString())))
                {
                    lblStatus.Text = "Select Component";
                    return;
                }
                sqldatasource = "COF_NonFlammable";
                sql = "Select  *  from VW_COF_NonFlammable  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' and  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' and CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader rd = cmd.ExecuteReader();

                if (rd.Read())
                {
                    double ptrans = Convert.ToDouble(rd["ptrans"].ToString());
                    double oppress = Convert.ToDouble(rd["OpPres"].ToString());
                    if (rd["Fluid"].ToString() == "Liquid")
                    {
                        path = "C:\\inetpub\\wwwroot\\RBI\\Reports\\COF_Non_Flammable_Liquid.mrt";
                    }
                    else
                    {
                        if (oppress < ptrans)
                        {
                            path = "c:\\inetpub\\wwwroot\\RBI\\Reports\\COF_Non_Flammable_Vapour_Sonic.mrt";
                        }
                        else
                        {
                            path = "c:\\inetpub\\wwwroot\\RBI\\Reports\\COF_Non_Flammable_Vapour_SubSonic.mrt";
                        }
                    }
                }
                else
                {
                    lblStatus.Text = "Data does not exist on selected Equipment and Component.";
                    lblStatus.ForeColor = Color.Green;
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

            //--------------------------------------POF------------------------
            //if (cbo_Report_Select.SelectedItem.Text == "POF ExcelExport")
            //{
            //    StiWebViewer1.Visible = true;
            //    if ((string.IsNullOrEmpty(cboEquipment.Text.ToString())))
            //    {
            //        lblStatus.Text = "Select Equipment";
            //        return;
            //    }
            //    if ((string.IsNullOrEmpty(cboComponent.Text.ToString())))
            //    {
            //        lblStatus.Text = "Select Component";
            //        return;
            //    }
            //    Excel.Application xlApp;
            //    Excel.Workbook xlWorkBook;
            //    Excel.Worksheet xlWorkSheet;
            //    object misValue = System.Reflection.Missing.Value;
            //    xlApp = new Excel.ApplicationClass();
            //    xlWorkBook = xlApp.Workbooks.Add(misValue);
            //    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            //    Excel.Range chartRange;


            //    string sql1 = "Select  EquipType,CompName from RiskRanking  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd = new SqlDataAdapter(sql1, conn);
            //    DataSet ds = new DataSet();
            //    dscmd.Fill(ds);

            //    xlWorkSheet.Cells[1, 1] = "Equipment Type";
            //    xlWorkSheet.Cells[1, 2] = "Component Name";
               

            //    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            //    {
            //        for (int j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)
            //        {
            //            string data = ds.Tables[0].Rows[i].ItemArray[j].ToString();
            //            xlWorkSheet.Cells[i + 2, j + 1] = data;
            //        }
            //    }

            //    string sql17 = "Select  Tdf  from ThinningDamage  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd17 = new SqlDataAdapter(sql17, conn);
            //    DataSet ds17 = new DataSet();
            //    dscmd17.Fill(ds17);
            //    xlWorkSheet.Cells[1,  3] = "ThinningDamage";

            //    for (int i = 0; i <= ds17.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds17.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 3] = data;
            //    }

            //    string sql18 = "Select  DfLine  from LiningDamage  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd18 = new SqlDataAdapter(sql18, conn);
            //    DataSet ds18 = new DataSet();
            //    dscmd18.Fill(ds18);
            //    xlWorkSheet.Cells[1, 4] = "LiningDamage";

            //    for (int i = 0; i <= ds18.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds18.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 4] = data;
            //    }

            //    string sql9 = "Select  ECDDf  from ECD  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd9 = new SqlDataAdapter(sql9, conn);
            //    DataSet ds9 = new DataSet();
            //    dscmd9.Fill(ds9);
            //    xlWorkSheet.Cells[1, 5] = "ExternalCorrosion";

            //    for (int i = 0; i <= ds9.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds9.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 5] = data;
            //    }

            //    string sql8 = "Select  CUIDf  from CUI  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd8 = new SqlDataAdapter(sql8, conn);
            //    DataSet ds8 = new DataSet();
            //    dscmd8.Fill(ds8);
            //    xlWorkSheet.Cells[1, 6] = "CUI";

            //    for (int i = 0; i <= ds8.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds8.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 6] = data;
            //    }


            //    string sql10 = "Select  ExCLSDf  from ExCLS  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd10 = new SqlDataAdapter(sql10, conn);
            //    DataSet ds10 = new DataSet();
            //    dscmd10.Fill(ds10);
            //    xlWorkSheet.Cells[1, 7] = "ExternalCLSCC";

            //    for (int i = 0; i <= ds10.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds10.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 7] = data;
            //    }

            //    string sql11 = "Select  ExCUIDf  from ExCUI  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd11 = new SqlDataAdapter(sql11, conn);
            //    DataSet ds11 = new DataSet();
            //    dscmd11.Fill(ds11);
            //    xlWorkSheet.Cells[1, 8] = "ExternalCUI";

            //    for (int i = 0; i <= ds11.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds11.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 8] = data;
            //    }

            //    string sql7 = "Select  CSDf  from CausticCracking  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd7 = new SqlDataAdapter(sql7, conn);
            //    DataSet ds7 = new DataSet();
            //    dscmd7.Fill(ds7);
            //    xlWorkSheet.Cells[1, 9] = "CausticCracking";

            //    for (int i = 0; i <= ds7.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds7.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 9] = data;
            //    }


            //    string sql3 = "Select  AmDf  from AmineCracking  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd3 = new SqlDataAdapter(sql3, conn);
            //    DataSet ds3 = new DataSet();
            //    dscmd3.Fill(ds3);
            //    xlWorkSheet.Cells[1, 10] = "AmineCracking";

            //    for (int i = 0; i <= ds3.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds3.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 10] = data;
            //    }

            //    string sql22 = "Select  SulDf  from SulfideCracking  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd22 = new SqlDataAdapter(sql22, conn);
            //    DataSet ds22 = new DataSet();
            //    dscmd22.Fill(ds22);
            //    xlWorkSheet.Cells[1, 11] = "SulfideCracking";

            //    for (int i = 0; i <= ds22.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds22.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 11] = data;
            //    }

            //    string sql14 = "Select  HICDf  from HIC  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd14 = new SqlDataAdapter(sql14, conn);
            //    DataSet ds14 = new DataSet();
            //    dscmd14.Fill(ds14);
            //    xlWorkSheet.Cells[1, 12] = "Df-HIC";

            //    for (int i = 0; i <= ds14.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds14.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 12] = data;
            //    }

            //    string sql5 = "Select  CO3Df  from CarbonateCracking  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd5 = new SqlDataAdapter(sql5, conn);
            //    DataSet ds5 = new DataSet();
            //    dscmd5.Fill(ds5);
            //    xlWorkSheet.Cells[1, 13] = "CarbonateCracking";

            //    for (int i = 0; i <= ds5.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds5.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 13] = data;
            //    }

            //    string sql20 = "Select  PTADf  from PTA  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd20 = new SqlDataAdapter(sql20, conn);
            //    DataSet ds20 = new DataSet();
            //    dscmd20.Fill(ds20);
            //    xlWorkSheet.Cells[1, 14] = "PTA";

            //    for (int i = 0; i <= ds20.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds20.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 14] = data;
            //    }

            //    string sql6 = "Select  CLSDf  from CLSCC  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd6 = new SqlDataAdapter(sql6, conn);
            //    DataSet ds6 = new DataSet();
            //    dscmd6.Fill(ds6);
            //    xlWorkSheet.Cells[1, 15] = "CLSCC";

            //    for (int i = 0; i <= ds6.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds6.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 15] = data;
            //    }

            //    string sql15 = "Select  HSCDf  from HSC  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd15 = new SqlDataAdapter(sql15, conn);
            //    DataSet ds15 = new DataSet();
            //    dscmd15.Fill(ds15);
            //    xlWorkSheet.Cells[1, 16] = "Df-HSC";

            //    for (int i = 0; i <= ds15.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds15.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 16] = data;
            //    }

            //    string sql13 = "Select  HFDf  from HF  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd13 = new SqlDataAdapter(sql13, conn);
            //    DataSet ds13 = new DataSet();
            //    dscmd13.Fill(ds13);
            //    xlWorkSheet.Cells[1, 17] = "Df-HF";

            //    for (int i = 0; i <= ds13.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds13.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 17] = data;
            //    }

            //    string sql16 = "Select  HTHADf  from HTHA  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd16 = new SqlDataAdapter(sql16, conn);
            //    DataSet ds16 = new DataSet();
            //    dscmd16.Fill(ds16);
            //    xlWorkSheet.Cells[1, 18] = "Df-HTHA";

            //    for (int i = 0; i <= ds16.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds16.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 18] = data;
            //    }

            //    string sql4 = "Select  Dfb  from Brittle_Facture  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd4 = new SqlDataAdapter(sql4, conn);
            //    DataSet ds4 = new DataSet();
            //    dscmd4.Fill(ds4);
            //    xlWorkSheet.Cells[1, 19] = "Brittle_Facture";

            //    for (int i = 0; i <= ds4.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds4.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 19] = data;
            //    }

            //    string sql23 = "Select  DfTemp  from Temper_Embrit  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd23 = new SqlDataAdapter(sql23, conn);
            //    DataSet ds23 = new DataSet();
            //    dscmd23.Fill(ds23);
            //    xlWorkSheet.Cells[1, 20] = "Temper_Embrit";

            //    for (int i = 0; i <= ds23.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds23.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 20] = data;
            //    }
                
            //    string sql12 = "Select  Df885  from EightEightFive  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd12 = new SqlDataAdapter(sql12, conn);
            //    DataSet ds12 = new DataSet();
            //    dscmd12.Fill(ds12);
            //    xlWorkSheet.Cells[1, 21] = "Df-885";

            //    for (int i = 0; i <= ds12.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds12.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 21] = data;
            //    }

            //    ///-----------------------------------------------------------

            //    string sql21 = "Select  DfSigma  from Sigma_Phase  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd21 = new SqlDataAdapter(sql21, conn);
            //    DataSet ds21 = new DataSet();
            //    dscmd21.Fill(ds21);
            //    xlWorkSheet.Cells[1, 22] = "Sigma_Phase";

            //    for (int i = 0; i <= ds21.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds21.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 22] = data;
            //    }
              
            //    string sql19 = "Select  DfMech from Mechanical_Fatigue  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd19 = new SqlDataAdapter(sql19, conn);
            //    DataSet ds19 = new DataSet();
            //    dscmd19.Fill(ds19);
            //    xlWorkSheet.Cells[1, 23] = "Mechanical_Fatigue";

            //    for (int i = 0; i <= ds19.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds19.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 23] = data;
            //    }

               
            //    string sql24 = "Select POFval,POFCate  from RiskRanking  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd24 = new SqlDataAdapter(sql24, conn);
            //    DataSet ds24 = new DataSet();
            //    dscmd24.Fill(ds24);
            //    xlWorkSheet.Cells[1, 24] = "POFTotal";
            //    xlWorkSheet.Cells[1, 25] = "POFCategory";

            //    //for (int i = 0; i <= ds24.Tables[0].Rows.Count - 1; i++)
            //    //{
            //    //    string data = ds24.Tables[0].Rows[i].ItemArray[0].ToString();
            //    //    xlWorkSheet.Cells[i + 2, 24] = data;

            //    //}

            //    for (int i = 0; i <= ds24.Tables[0].Rows.Count - 1; i++)
            //    {
            //        for (int j = 0; j <= ds24.Tables[0].Columns.Count - 1; j++)
            //        {
            //            string data = ds24.Tables[0].Rows[i].ItemArray[j].ToString();
            //            xlWorkSheet.Cells[i + 2, j + 24] = data;
            //        }
            //    }


            //    //for (int i = 0; i <= ds2.Tables[0].Rows.Count - 1; i++)
            //    //{
            //    //    for (int j = 0; j <= ds2.Tables[0].Columns.Count - 1; j++)
            //    //    {
            //    //        string data = ds2.Tables[0].Rows[i].ItemArray[j].ToString();
            //    //        xlWorkSheet.Cells[i + 2, j + 16] = data;
            //    //    }
            //    //}




            //    chartRange = xlWorkSheet.get_Range("a2", "y50");
            //    chartRange.Font.Italic = true;
            //    chartRange.Font.Bold = true;
            //    chartRange.Cells.Interior.Color = System.Drawing.Color.FromKnownColor(KnownColor.Yellow);
            //    chartRange.Cells.Font.Color = System.Drawing.Color.FromKnownColor(KnownColor.Red);
            //    chartRange.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            //    chartRange.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

            //    chartRange = xlWorkSheet.get_Range("a1", "y1");
            //    chartRange.Font.Bold = true;
            //    chartRange.Cells.Interior.Color = System.Drawing.Color.FromKnownColor(KnownColor.GreenYellow);
            //    chartRange.Cells.EntireColumn.AutoFit();
            //    chartRange.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlDouble;

            //    xlWorkBook.SaveAs(Server.MapPath("Files/" + "POF.xls"), Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            //    xlWorkBook.Close(true, misValue, misValue);
            //    xlApp.Quit();

            //    releaseObject(xlWorkSheet);
            //    releaseObject(xlWorkBook);
            //    releaseObject(xlApp);

            //    Response.ContentType = "xls";
            //    Response.AppendHeader("Content-Disposition", "attachment; filename=POF.xls");
            //    Response.TransmitFile(Server.MapPath("~/Files/POF.xls"));
            //    Response.End();
                
            //}
            //--------------------------------------Risk Ranking------------------------
            if (cbo_Report_Select.SelectedItem.Text == "Risk Ranking Chart")
            {
                StiWebViewer1.Visible = true;
                string sqldatasource1 = "DataSource1";
                string sql1 = "select count(ChooseRisk)as High,processarea from VW_RiskRanking_Save where ChooseRisk='High'and ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' and  Deleted=0  group by processarea ";
                SqlDataAdapter ad1 = new SqlDataAdapter(sql1, con);
                DataSet ds1 = new DataSet();
                ds1.DataSetName = "DynamicDataSource1";
                ds1.Tables.Add(sqldatasource1);
                ad1.Fill(ds1, sqldatasource1);

                string sqldatasource2 = "DataSource2";
                string sql2 = "select count(ChooseRisk)as MHigh from RiskRanking where ChooseRisk='Medium High' and ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' and  Deleted=0";
                SqlDataAdapter ad2 = new SqlDataAdapter(sql2, con);
                DataSet ds2 = new DataSet();
                ds2.DataSetName = "DynamicDataSource1";
                ds2.Tables.Add(sqldatasource2);
                ad2.Fill(ds2, sqldatasource2);

                string sqldatasource3 = "DataSource3";
                string sql3 = "select count(ChooseRisk)as Medium from RiskRanking where ChooseRisk='Medium' and ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' and  Deleted=0";
                SqlDataAdapter ad3 = new SqlDataAdapter(sql3, con);
                DataSet ds3 = new DataSet();
                ds3.DataSetName = "DynamicDataSource1";
                ds3.Tables.Add(sqldatasource3);
                ad3.Fill(ds3, sqldatasource3);

                string sqldatasource4 = "DataSource4";
                string sql4 = "select count(ChooseRisk)as Low from RiskRanking where ChooseRisk='Low' and ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' and  Deleted=0";
                SqlDataAdapter ad4 = new SqlDataAdapter(sql4, con);
                DataSet ds4 = new DataSet();
                ds4.DataSetName = "DynamicDataSource1";
                ds4.Tables.Add(sqldatasource4);
                ad4.Fill(ds4, sqldatasource4);

                string sqldatasource5 = "DataSource5";
                string sql5 = "select processarea from VW_RiskRanking_Save where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' and  Deleted=0";
                SqlDataAdapter ad5 = new SqlDataAdapter(sql5, con);
                DataSet ds5 = new DataSet();
                ds5.DataSetName = "DynamicDataSource1";
                ds5.Tables.Add(sqldatasource5);
                ad5.Fill(ds1, sqldatasource5);

                path = "C:\\inetpub\\wwwroot\\RBI\\Reports\\Chart_RiskRanking.mrt";
                Stimulsoft.Report.StiReport stiReport1;
                stiReport1 = new StiReport();
                stiReport1.Dictionary.DataStore.Clear();
                stiReport1.ClearAllStates();
                stiReport1.Load(path);
                stiReport1.Dictionary.Databases.Clear();
                stiReport1.Dictionary.Databases.Add(new StiSqlDatabase("Connection", con));
                stiReport1.Dictionary.DataSources.Clear();

                stiReport1.RegData(sqldatasource1, ds1);
                stiReport1.RegData(sqldatasource2, ds2);
                stiReport1.RegData(sqldatasource3, ds3);
                stiReport1.RegData(sqldatasource4, ds4);
                stiReport1.RegData(sqldatasource5, ds5);

                stiReport1.Dictionary.Synchronize();
                stiReport1.Compile();
                // stiReport1.Render();
                StiWebViewer1.Report = stiReport1;
                StiWebViewer1.ViewMode = StiWebViewMode.WholeReport;
                stiReport1.Dispose();
            }
            //--------------------------------------Inspection Details------------------------
            if (cbo_Report_Select.SelectedItem.Text == "Inspection Details")
            {
                StiWebViewer1.Visible = true;
                string sqldatasource1 = string.Empty, sql1 = string.Empty;
                DataSet ds1 = new DataSet();
                if (string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()))
                {
                    sqldatasource1 = "VW_Inspection";
                    sql1 = "select *,CONVERT(VARCHAR(10), Initialdate, 103) AS [Indate],Initialvalue,CONVERT(VARCHAR(10), InspecDate, 103) AS [Insdate],CONVERT(VARCHAR(10), Previousdate, 103) AS [Predate] from VW_Inspection where Deleted=0 order by EquAutoID,InspectionPointNo,InspecDate";
                    SqlDataAdapter ad1 = new SqlDataAdapter(sql1, con);

                    ds1.DataSetName = "DynamicDataSource1";
                    ds1.Tables.Add(sqldatasource1);
                    ad1.Fill(ds1, sqldatasource1);
                }
                else if (string.IsNullOrEmpty(cboComponent.Text.ToString()))
                {
                    sqldatasource1 = "VW_Inspection";
                    sql1 = "select *,CONVERT(VARCHAR(10), Initialdate, 103) AS [Indate],Initialvalue,CONVERT(VARCHAR(10), InspecDate, 103) AS [Insdate],CONVERT(VARCHAR(10), Previousdate, 103) AS [Predate] from VW_Inspection where EquAutoID='" + cboEquipment.SelectedValue.ToString().Trim() + "' and  Deleted=0 order by EquAutoID,InspectionPointNo,InspecDate";
                    SqlDataAdapter ad1 = new SqlDataAdapter(sql1, con);

                    ds1.DataSetName = "DynamicDataSource1";
                    ds1.Tables.Add(sqldatasource1);
                    ad1.Fill(ds1, sqldatasource1);
                }
                else
                {
                    sqldatasource1 = "VW_Inspection";
                    sql1 = "select *,CONVERT(VARCHAR(10), Initialdate, 103) AS [Indate],Initialvalue,CONVERT(VARCHAR(10), InspecDate, 103) AS [Insdate],CONVERT(VARCHAR(10), Previousdate, 103) AS [Predate] from VW_Inspection where EquAutoID='" + cboEquipment.SelectedValue.ToString().Trim() + "' and CompAutoID='" + cboComponent.SelectedValue.ToString().Trim() + "' and   Deleted=0 order by EquAutoID,InspectionPointNo,InspecDate";
                    SqlDataAdapter ad1 = new SqlDataAdapter(sql1, con);

                    ds1.DataSetName = "DynamicDataSource1";
                    ds1.Tables.Add(sqldatasource1);
                    ad1.Fill(ds1, sqldatasource1);
                }
                path = "C:\\inetpub\\wwwroot\\RBI\\Reports\\Inspection.mrt";
                Stimulsoft.Report.StiReport stiReport1;
                stiReport1 = new StiReport();
                stiReport1.Dictionary.DataStore.Clear();
                stiReport1.ClearAllStates();
                stiReport1.Load(path);
                stiReport1.Dictionary.Databases.Clear();
                stiReport1.Dictionary.Databases.Add(new StiSqlDatabase("Connection", con));
                stiReport1.Dictionary.DataSources.Clear();

                stiReport1.RegData(sqldatasource1, ds1);
                stiReport1.Dictionary.Synchronize();
                stiReport1.Compile();
                // stiReport1.Render();
                StiWebViewer1.Report = stiReport1;
                StiWebViewer1.ViewMode = StiWebViewMode.WholeReport;
                stiReport1.Dispose();
            }

            //--------------------------------------Inspection Summary------------------------
            if (cbo_Report_Select.SelectedItem.Text == "Inspection Summary")
            {
                StiWebViewer1.Visible = true;
                string sqldatasource1 = string.Empty, sql1 = string.Empty;
                DataSet ds1 = new DataSet();
               
                    sqldatasource1 = "VW_Inspection";
                    sql1 = "select * from (select *,row_number() over (partition by compautoid order by processareaid) as row_number from VW_InspectionSummaryExtend ) as rows where row_number = 1 and companyid='" + Convert.ToInt32(Session["sesCompanyID"].ToString()) + "' order by EquipID";
                    SqlDataAdapter ad1 = new SqlDataAdapter(sql1, con);

                    ds1.DataSetName = "DynamicDataSource1";
                    ds1.Tables.Add(sqldatasource1);
                    ad1.Fill(ds1, sqldatasource1);

                    path = "C:\\inetpub\\wwwroot\\RBI\\Reports\\InspectionSummaryExtend.mrt";
                Stimulsoft.Report.StiReport stiReport1;
                stiReport1 = new StiReport();
                stiReport1.Dictionary.DataStore.Clear();
                stiReport1.ClearAllStates();
                stiReport1.Load(path);
                stiReport1.Dictionary.Databases.Clear();
                stiReport1.Dictionary.Databases.Add(new StiSqlDatabase("Connection", con));
                stiReport1.Dictionary.DataSources.Clear();

                stiReport1.RegData(sqldatasource1, ds1);
                stiReport1.Dictionary.Synchronize();
                stiReport1.Compile();
                // stiReport1.Render();
                StiWebViewer1.Report = stiReport1;
                StiWebViewer1.ViewMode = StiWebViewMode.WholeReport;
                stiReport1.Dispose();
            }
            //--------------------------------------POF Summary------------------------
            if (cbo_Report_Select.SelectedItem.Text == "POF Summary")
            {
                StiWebViewer1.Visible = true;
                string sqldatasource1 = string.Empty, sql1 = string.Empty;
                DataSet ds1 = new DataSet();

                sqldatasource1 = "VW_POF";
                sql1 = "select * from VW_POF where companyid='" + Convert.ToInt32(Session["sesCompanyID"].ToString()) + "'";
                SqlDataAdapter ad1 = new SqlDataAdapter(sql1, con);

                ds1.DataSetName = "DynamicDataSource1";
                ds1.Tables.Add(sqldatasource1);
                ad1.Fill(ds1, sqldatasource1);

                path = "C:\\inetpub\\wwwroot\\RBI\\Reports\\POFList.mrt";
                Stimulsoft.Report.StiReport stiReport1;
                stiReport1 = new StiReport();
                stiReport1.Dictionary.DataStore.Clear();
                stiReport1.ClearAllStates();
                stiReport1.Load(path);
                stiReport1.Dictionary.Databases.Clear();
                stiReport1.Dictionary.Databases.Add(new StiSqlDatabase("Connection", con));
                stiReport1.Dictionary.DataSources.Clear();

                stiReport1.RegData(sqldatasource1, ds1);
                stiReport1.Dictionary.Synchronize();
                stiReport1.Compile();
                // stiReport1.Render();
                StiWebViewer1.Report = stiReport1;
                StiWebViewer1.ViewMode = StiWebViewMode.WholeReport;
                stiReport1.Dispose();
            }

            //--------------------------------------Equipment list------------------------
            if (cbo_Report_Select.SelectedItem.Text == "Equipment list")
            {
                StiWebViewer1.Visible = true;
                string sqldatasource1 = string.Empty, sql1 = string.Empty;
                DataSet ds1 = new DataSet();

                sqldatasource1 = "VW__EquipmentAsset";
                sql1 = "SELECT ROW_NUMBER() OVER(ORDER BY [DoshNo] DESC) as No,[ProcessArea],[DoshNo],[EqupID],[EqupDescription] FROM [RBI].[dbo].[VW__EquipmentAsset] where companyid='" + Convert.ToInt32(Session["sesCompanyID"].ToString()) + "' and ProcessAreaID='" + cboProcessArea.SelectedValue.ToString().Trim() + "'";
                SqlDataAdapter ad1 = new SqlDataAdapter(sql1, con);

                ds1.DataSetName = "DynamicDataSource1";
                ds1.Tables.Add(sqldatasource1);
                ad1.Fill(ds1, sqldatasource1);

                path = "C:\\inetpub\\wwwroot\\RBI\\Reports\\EquipmentList.mrt";
                Stimulsoft.Report.StiReport stiReport1;
                stiReport1 = new StiReport();
                stiReport1.Dictionary.DataStore.Clear();
                stiReport1.ClearAllStates();
                stiReport1.Load(path);
                stiReport1.Dictionary.Databases.Clear();
                stiReport1.Dictionary.Databases.Add(new StiSqlDatabase("Connection", con));
                stiReport1.Dictionary.DataSources.Clear();

                stiReport1.RegData(sqldatasource1, ds1);
                stiReport1.Dictionary.Synchronize();
                stiReport1.Compile();
                // stiReport1.Render();
                StiWebViewer1.Report = stiReport1;
                StiWebViewer1.ViewMode = StiWebViewMode.WholeReport;
                stiReport1.Dispose();
            }

            //--------------------------------------Dosh list Summary------------------------
            if (cbo_Report_Select.SelectedItem.Text == "Dosh list Summary")
            {
        //       string constr = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        //using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("SELECT ROW_NUMBER() OVER(ORDER BY DamageFact,Doshno) as No, [DoshNo],[EquipID],[ProcessArea],[EqupDescription],[CompName],[InspecDate],[DesignCode],[materialspecification],[DesignTemp],[Designpressure],[NormalThickness] as NorminalThick,[Insulated],[fins] as InsulationMaterial,[COF_NonFlammableRepresentativeFluid] as RepresentativeFluid_NonFlame,[Repfluid] as RepresentativeFluid_Flame,[COF_NonFlammableFluid] as Fluid_NonFlame,[Fluid]as Fluid_Flame,[YearInstalled],[OPPressure],[OPTemp],DamageFact as DamageFactor,IntrInsp as Intrusive,NonIntrInsp as NonIntrusive,NextInspectionDate,[FinCOFCate] as FinancialCOFRanking,[COFCate] as COFRanking,[(Choose)Use COF],[Category] as POFCategory,[ChooseRisk] as RiskRanking,[ShortCRrate],[LongCRrate],[uCR],[MRT],[Initialdate],[Initialvalue] as InitialThickness,[Previousdate],[Previousvalue] as PreviousThickness,[InspecDate] as Currentdate,[ReadVal] as CurrentThickness,DATEDIFF(year,[YearInstalled],[InspecDate]) as AgeInService,[RemainingLife],[historydescription] as EquipmentHistory,[DOSHobservation] FROM [RBI].[dbo].[VW_InsSummary_Dosh] where companyid='" + Convert.ToInt32(Session["sesCompanyID"].ToString()) + "' order by DamageFact,Doshno"))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = conn;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            wb.Worksheets.Add(dt, "Customers");

                            Response.Clear();
                            Response.Buffer = true;
                            Response.Charset = "";
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.AddHeader("content-disposition", "attachment;filename=SqlExport.xlsx");
                            using (MemoryStream MyMemoryStream = new MemoryStream())
                            {
                                wb.SaveAs(MyMemoryStream);
                                MyMemoryStream.WriteTo(Response.OutputStream);
                                Response.Flush();
                                Response.End();
                            }
                        }
                    }
                }
            }
        }
    }
            
            //--------------------------------------Equipment Details------------------------
            if (cbo_Report_Select.SelectedItem.Text == "Equipment Details")
            {
                StiWebViewer1.Visible = true;
                string sqldatasource1 = string.Empty, sqldatasource2 = string.Empty, sql1 = string.Empty, sql2 = string.Empty;
                DataSet ds1 = new DataSet();

                sqldatasource1 = "VW_EquipmentDetails"; sqldatasource2 = "VW_InspectionPlan";
                sql1 = "select * from VW_EquipmentDetails where EqupID='" + cboEquipment.SelectedValue.ToString().Trim() + "' and CompAutoID='" + cboComponent.SelectedValue.ToString().Trim() + "'";
                sql2 = "select * from VW_InspectionPlanForEquipmentDetails where  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' and CompID='" + cboComponent.SelectedValue.ToString().Trim() + "'";
                
                SqlDataAdapter ad1 = new SqlDataAdapter(sql1, con);
                ds1.DataSetName = "DynamicDataSource1";
                ds1.Tables.Add(sqldatasource1);
                ad1.Fill(ds1, sqldatasource1);

                SqlDataAdapter ad = new SqlDataAdapter(sql2, con);
                DataSet ds = new DataSet();
                ds.DataSetName = "DynamicDataSource";
                ds.Tables.Add(sqldatasource2);
                ad.Fill(ds, sqldatasource2);

                path = "C:\\inetpub\\wwwroot\\RBI\\Reports\\EquipmentDetails.mrt";
                Stimulsoft.Report.StiReport stiReport1;
                stiReport1 = new StiReport();
                stiReport1.Dictionary.DataStore.Clear();
                stiReport1.ClearAllStates();
                stiReport1.Load(path);
                stiReport1.Dictionary.Databases.Clear();
                stiReport1.Dictionary.Databases.Add(new StiSqlDatabase("Connection", con));
                stiReport1.Dictionary.DataSources.Clear();

                stiReport1.RegData(sqldatasource1, ds1);
                stiReport1.RegData(sqldatasource2, ds);
                stiReport1.Dictionary.Synchronize();
                stiReport1.Compile();
                // stiReport1.Render();
                StiWebViewer1.Report = stiReport1;
                StiWebViewer1.ViewMode = StiWebViewMode.WholeReport;
                stiReport1.Dispose();
            }

            //--------------------------------------Inspection Plan-----------------------
            if (cbo_Report_Select.SelectedItem.Text == "Inspection Plan")
            {
                StiWebViewer1.Visible = true;
                string sqldatasource1 = string.Empty, sql1 = string.Empty;
                DataSet ds1 = new DataSet();

                sqldatasource1 = "VW_InspectionPlan";
                sql1 = "select * from VW_InspectionPlan_New where companyid='" + Convert.ToInt32(Session["sesCompanyID"].ToString()) + "'";
                SqlDataAdapter ad1 = new SqlDataAdapter(sql1, con);

                ds1.DataSetName = "DynamicDataSource1";
                ds1.Tables.Add(sqldatasource1);
                ad1.Fill(ds1, sqldatasource1);

                path = "C:\\inetpub\\wwwroot\\RBI\\Reports\\Inspection Plan.mrt";
                Stimulsoft.Report.StiReport stiReport1;
                stiReport1 = new StiReport();
                stiReport1.Dictionary.DataStore.Clear();
                stiReport1.ClearAllStates();
                stiReport1.Load(path);
                stiReport1.Dictionary.Databases.Clear();
                stiReport1.Dictionary.Databases.Add(new StiSqlDatabase("Connection", con));
                stiReport1.Dictionary.DataSources.Clear();

                stiReport1.RegData(sqldatasource1, ds1);
                stiReport1.Dictionary.Synchronize();
                stiReport1.Compile();
                // stiReport1.Render();
                StiWebViewer1.Report = stiReport1;
                StiWebViewer1.ViewMode = StiWebViewMode.WholeReport;
                stiReport1.Dispose();
            }
            //--------------------------------------Inspection Chart------------------------
            if (cbo_Report_Select.SelectedItem.Text == "Inspection Chart")
            {
                 StiWebViewer1.Visible = true;
                string sqldatasource1 = string.Empty, sql1 = string.Empty;
                DataSet ds1 = new DataSet();
                sqldatasource1 = "VW_InspectionChart";
                //sql1 = "select MRT,CONVERT(VARCHAR(10), InspecDate, 103) AS InsDate,InspecDate,ReadingValue,NormalThickness from VW_InspectionChart where  EqupID='" + cboEquipment.SelectedValue.ToString().Trim() + "' and ComponentNo ='" + cboComponent.SelectedValue.ToString().Trim() + "' and mrt is not null  and LongCRrate is not null order by InspecDate";
                sql1 = "select MRT,CONVERT(VARCHAR(10), InspecDate, 103) AS InsDate,InspecDate,ReadingValue,NormalThickness,[Equipment ID],DoshNo,CompName,uCR,RemainingLife from VW_InspectionChart where EqupID='" + cboEquipment.SelectedValue.ToString().Trim() + "' and CompAutoID='" + cboComponent.SelectedValue.ToString().Trim() + "' and   Deleted=0 and LongCRrate is not null order by EqupID,InspecDate";
                SqlDataAdapter ad1 = new SqlDataAdapter(sql1, con);

                ds1.DataSetName = "DynamicDataSource1";
                ds1.Tables.Add(sqldatasource1);
                ad1.Fill(ds1, sqldatasource1);
                path = "C:\\inetpub\\wwwroot\\RBI\\Reports\\Chart_Inspection.mrt";
                Stimulsoft.Report.StiReport stiReport1;
                stiReport1 = new StiReport();
                stiReport1.Dictionary.DataStore.Clear();
                stiReport1.ClearAllStates();
                stiReport1.Load(path);
                stiReport1.Dictionary.Databases.Clear();
                stiReport1.Dictionary.Databases.Add(new StiSqlDatabase("Connection", con));
                stiReport1.Dictionary.DataSources.Clear();

                stiReport1.RegData(sqldatasource1, ds1);
                stiReport1.Dictionary.Synchronize();
                stiReport1.Compile();
                // stiReport1.Render();
                StiWebViewer1.Report = stiReport1;
                StiWebViewer1.ViewMode = StiWebViewMode.WholeReport;
                stiReport1.Dispose();

            }
            //--------------------------------------RBI Data Sheet------------------------
            //if (cbo_Report_Select.SelectedItem.Text == "RBIDataSheet ExcelExport")
            //{
            //    StiWebViewer1.Visible = false;
            //    if ((string.IsNullOrEmpty(cboEquipment.Text.ToString())))
            //    {
            //        lblStatus.Text = "Select Equipment";
            //        return;
            //    }
            //    if ((string.IsNullOrEmpty(cboComponent.Text.ToString())))
            //    {
            //        lblStatus.Text = "Select Component";
            //        return;
            //    }

            //    Excel.Application xlApp;
            //    Excel.Workbook xlWorkBook;
            //    Excel.Worksheet xlWorkSheet;
            //    object misValue = System.Reflection.Missing.Value;
            //    xlApp = new Excel.ApplicationClass();
            //    xlWorkBook = xlApp.Workbooks.Add(misValue);
            //    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            //    Excel.Range chartRange;


            //    string sqlrbi = "select equpid,doshno,equpdescription,designcode from tbl_equipmentasset where ProcessareaID=" + Convert.ToInt32(cboProcessArea.SelectedValue.ToString()) + " or equautoid=" + Convert.ToInt32(cboEquipment.SelectedValue.ToString()) + " and  Deleted=0";
            //    SqlDataAdapter dscmd = new SqlDataAdapter(sqlrbi, conn);
            //    DataSet ds = new DataSet();
            //    dscmd.Fill(ds);
            //    xlWorkSheet.Cells[1, 1] = "Equipment No";
            //    xlWorkSheet.Cells[1, 2] = "Dosh No";
            //    xlWorkSheet.Cells[1, 3] = "Equipment Description";
            //    xlWorkSheet.Cells[1, 4] = "Design Code";

            //    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            //    {
            //        for (int j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)
            //        {
            //            string data = ds.Tables[0].Rows[i].ItemArray[j].ToString();
            //            xlWorkSheet.Cells[i + 2, j + 1] = data;
            //        }
            //    }

            //    string sqlrbi1 = "select materialtype,materialspecification,designtemp,designpressure,oppressure,optemp,compname,mrt,normalthickness,mrt, corrosionallownce from tbl_equipmentcomponentdetails where ProcessareaID=" + Convert.ToInt32(cboProcessArea.SelectedValue.ToString()) + " or equpid=" + Convert.ToInt32(cboEquipment.SelectedValue.ToString()) + " and  Deleted=0";
            //    SqlDataAdapter dscmd1 = new SqlDataAdapter(sqlrbi1, conn);
            //    DataSet ds1 = new DataSet();
            //    dscmd1.Fill(ds1);

            //    xlWorkSheet.Cells[1, 5] = "Type";
            //    xlWorkSheet.Cells[1, 6] = "Specification";
            //    xlWorkSheet.Cells[1, 7] = "DesignTemp-C";
            //    xlWorkSheet.Cells[1, 8] = "DesignPressure(kPa)";
            //    xlWorkSheet.Cells[1, 9] = "OPerating Pressure";
            //    xlWorkSheet.Cells[1, 10] = "OPerating Temp-C";
            //    xlWorkSheet.Cells[1, 11] = "Inspection Location";
            //    xlWorkSheet.Cells[1, 12] = "Design";
            //    xlWorkSheet.Cells[1, 13] = "NormalThickness";
            //    xlWorkSheet.Cells[1, 14] = "CA";
            //    xlWorkSheet.Cells[1, 15] = "Required";
            //    chartRange = xlWorkSheet.get_Range("a1", "au1");
            //    chartRange.Font.Bold = true;
            //    chartRange.Cells.Interior.Color = System.Drawing.Color.GreenYellow.ToArgb();
            //    chartRange.Cells.EntireColumn.AutoFit();
            //    chartRange.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlDouble;

            //   for (int i = 0; i <= ds1.Tables[0].Rows.Count - 1; i++)
            //    {
            //        for (int j = 0; j <= ds1.Tables[0].Columns.Count - 1; j++)
            //        {
            //            string data = ds1.Tables[0].Rows[i].ItemArray[j].ToString();
            //            xlWorkSheet.Cells[i + 2, j + 5] = data;
            //        }
            //    }

            //    string sqlrbi2 = "select initialvalue,CONVERT(VARCHAR, initialdate,103)as intdate,previousvalue,CONVERT(VARCHAR, previousdate,103)as prevdate ,readingvalue,CONVERT(VARCHAR, inspecdate,103)as insdate ,shortcrrate,longcrrate,case when isnull(shortcrrate,0) >= isnull(longcrrate,0) then shortcrrate else longcrrate end as required,  remaininglife from vw_inspection where  equautoid=" + Convert.ToInt32(cboEquipment.SelectedValue.ToString()) + "  or compautoID=" + Convert.ToInt32(cboComponent.SelectedValue.ToString()) + " and  Deleted=0";
            //    SqlDataAdapter dscmd2 = new SqlDataAdapter(sqlrbi2, conn);
            //    DataSet ds2 = new DataSet();
            //    dscmd2.Fill(ds2);

            //    xlWorkSheet.Cells[1, 16] = "Initialvalue";
            //    xlWorkSheet.Cells[1, 17] = "Initialdate";
            //    xlWorkSheet.Cells[1, 18] = "Previousvalue";
            //    xlWorkSheet.Cells[1, 19] = "Previousdate";
            //    xlWorkSheet.Cells[1, 20] = "Inspectionvalue";
            //    xlWorkSheet.Cells[1, 21] = "Inspectiondate";
            //    xlWorkSheet.Cells[1, 22] = "ShortTerm";
            //    xlWorkSheet.Cells[1, 23] = "LongTerm";
            //    xlWorkSheet.Cells[1, 25] = "Remaininglife";
            //    xlWorkSheet.Cells[1, 24] = "Required";

            //    for (int i = 0; i <= ds2.Tables[0].Rows.Count - 1; i++)
            //    {
            //        for (int j = 0; j <= ds2.Tables[0].Columns.Count - 1; j++)
            //        {
            //            string data = ds2.Tables[0].Rows[i].ItemArray[j].ToString();
            //            xlWorkSheet.Cells[i + 2, j + 16] = data;
            //        }
            //    }


                //----------------------------------------------------POF------------------------------
                
            //    string sql3 = "Select  AmDf  from AmineCracking  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd3 = new SqlDataAdapter(sql3, conn);
            //    DataSet ds3 = new DataSet();
            //    dscmd3.Fill(ds3);
            //    xlWorkSheet.Cells[1, 26] = "AmineCracking";

            //    for (int i = 0; i <= ds3.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds3.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 26] = data;
            //    }


            //    string sql4 = "Select  Dfb  from Brittle_Facture  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd4 = new SqlDataAdapter(sql4, conn);
            //    DataSet ds4 = new DataSet();
            //    dscmd4.Fill(ds4);
            //    xlWorkSheet.Cells[1, 27] = "Brittle_Facture";

            //    for (int i = 0; i <= ds4.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds4.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 27] = data;
            //    }


            //    string sql5 = "Select  CO3Df  from CarbonateCracking  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd5 = new SqlDataAdapter(sql5, conn);
            //    DataSet ds5 = new DataSet();
            //    dscmd5.Fill(ds5);
            //    xlWorkSheet.Cells[1, 28] = "CarbonateCracking";

            //    for (int i = 0; i <= ds5.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds5.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 28] = data;
            //    }

            //    string sql6 = "Select  CLSDf  from CLSCC  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd6 = new SqlDataAdapter(sql6, conn);
            //    DataSet ds6 = new DataSet();
            //    dscmd6.Fill(ds6);
            //    xlWorkSheet.Cells[1, 29] = "CLSCC";

            //    for (int i = 0; i <= ds6.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds6.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 29] = data;
            //    }

            //    string sql7 = "Select  CSDf  from CausticCracking  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd7 = new SqlDataAdapter(sql7, conn);
            //    DataSet ds7 = new DataSet();
            //    dscmd7.Fill(ds7);
            //    xlWorkSheet.Cells[1, 30] = "CausticCracking";

            //    for (int i = 0; i <= ds7.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds7.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 30] = data;
            //    }

            //    string sql8 = "Select  CUIDf  from CUI  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd8 = new SqlDataAdapter(sql8, conn);
            //    DataSet ds8 = new DataSet();
            //    dscmd8.Fill(ds8);
            //    xlWorkSheet.Cells[1, 31] = "CUI";

            //    for (int i = 0; i <= ds8.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds8.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 31] = data;
            //    }

            //    string sql9 = "Select  ECDDf  from ECD  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd9 = new SqlDataAdapter(sql9, conn);
            //    DataSet ds9 = new DataSet();
            //    dscmd9.Fill(ds9);
            //    xlWorkSheet.Cells[1, 32] = "ExternalCorrosion";

            //    for (int i = 0; i <= ds9.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds9.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 32] = data;
            //    }


            //    string sql10 = "Select  ExCLSDf  from ExCLS  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd10 = new SqlDataAdapter(sql10, conn);
            //    DataSet ds10 = new DataSet();
            //    dscmd10.Fill(ds10);
            //    xlWorkSheet.Cells[1, 33] = "ExternalCLSCC";

            //    for (int i = 0; i <= ds10.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds10.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 33] = data;
            //    }

            //    string sql11 = "Select  ExCUIDf  from ExCUI  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd11 = new SqlDataAdapter(sql11, conn);
            //    DataSet ds11 = new DataSet();
            //    dscmd11.Fill(ds11);
            //    xlWorkSheet.Cells[1, 34] = "ExternalCUI";

            //    for (int i = 0; i <= ds11.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds11.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 34] = data;
            //    }


            //    string sql12 = "Select  Df885  from EightEightFive  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd12 = new SqlDataAdapter(sql12, conn);
            //    DataSet ds12 = new DataSet();
            //    dscmd12.Fill(ds12);
            //    xlWorkSheet.Cells[1, 35] = "Df-885";

            //    for (int i = 0; i <= ds12.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds12.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 35] = data;
            //    }

            //    ///-----------------------------------------------------------


            //    string sql13 = "Select  HFDf  from HF  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd13 = new SqlDataAdapter(sql13, conn);
            //    DataSet ds13 = new DataSet();
            //    dscmd13.Fill(ds13);
            //    xlWorkSheet.Cells[1, 36] = "Df-HF";

            //    for (int i = 0; i <= ds13.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds13.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 36] = data;
            //    }


            //    string sql14 = "Select  HICDf  from HIC  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd14 = new SqlDataAdapter(sql14, conn);
            //    DataSet ds14 = new DataSet();
            //    dscmd14.Fill(ds14);
            //    xlWorkSheet.Cells[1, 37] = "Df-HIC";

            //    for (int i = 0; i <= ds14.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds14.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 37] = data;
            //    }


            //    string sql15 = "Select  HSCDf  from HSC  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd15 = new SqlDataAdapter(sql15, conn);
            //    DataSet ds15 = new DataSet();
            //    dscmd15.Fill(ds15);
            //    xlWorkSheet.Cells[1, 38] = "Df-HSC";

            //    for (int i = 0; i <= ds15.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds15.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 38] = data;
            //    }

            //    string sql16 = "Select  HTHADf  from HTHA  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd16 = new SqlDataAdapter(sql16, conn);
            //    DataSet ds16 = new DataSet();
            //    dscmd16.Fill(ds16);
            //    xlWorkSheet.Cells[1, 39] = "Df-HTHA";

            //    for (int i = 0; i <= ds16.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds16.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 39] = data;
            //    }


            //    string sql17 = "Select  Tdf  from ThinningDamage  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd17 = new SqlDataAdapter(sql17, conn);
            //    DataSet ds17 = new DataSet();
            //    dscmd17.Fill(ds17);
            //    xlWorkSheet.Cells[1, 40] = "ThinningDamage";

            //    for (int i = 0; i <= ds17.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds17.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 40] = data;
            //    }

            //    string sql18 = "Select  DfLine  from LiningDamage  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd18 = new SqlDataAdapter(sql18, conn);
            //    DataSet ds18 = new DataSet();
            //    dscmd18.Fill(ds18);
            //    xlWorkSheet.Cells[1, 41] = "LiningDamage";

            //    for (int i = 0; i <= ds18.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds18.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 41] = data;
            //    }


            //    string sql19 = "Select  DfMech from Mechanical_Fatigue  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd19 = new SqlDataAdapter(sql19, conn);
            //    DataSet ds19 = new DataSet();
            //    dscmd19.Fill(ds19);
            //    xlWorkSheet.Cells[1, 42] = "Mechanical_Fatigue";

            //    for (int i = 0; i <= ds19.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds19.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 42] = data;
            //    }

            //    string sql20 = "Select  PTADf  from PTA  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd20 = new SqlDataAdapter(sql20, conn);
            //    DataSet ds20 = new DataSet();
            //    dscmd20.Fill(ds20);
            //    xlWorkSheet.Cells[1, 43] = "PTA";

            //    for (int i = 0; i <= ds20.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds20.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 43] = data;
            //    }

            //    string sql21 = "Select  DfSigma  from Sigma_Phase  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd21 = new SqlDataAdapter(sql21, conn);
            //    DataSet ds21 = new DataSet();
            //    dscmd21.Fill(ds21);
            //    xlWorkSheet.Cells[1, 44] = "Sigma_Phase";

            //    for (int i = 0; i <= ds21.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds21.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 44] = data;
            //    }

            //    //---------------------------------------------//

            //    string sql22 = "Select  SulDf  from SulfideCracking  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd22 = new SqlDataAdapter(sql22, conn);
            //    DataSet ds22 = new DataSet();
            //    dscmd22.Fill(ds22);
            //    xlWorkSheet.Cells[1, 45] = "SulfideCracking";

            //    for (int i = 0; i <= ds22.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds22.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 45] = data;
            //    }


            //    string sql23 = "Select  DfTemp  from Temper_Embrit  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd23 = new SqlDataAdapter(sql23, conn);
            //    DataSet ds23 = new DataSet();
            //    dscmd23.Fill(ds23);
            //    xlWorkSheet.Cells[1, 46] = "Temper_Embrit";

            //    for (int i = 0; i <= ds23.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds23.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 46] = data;
            //    }

            //    string sql24 = "Select chooserisk  from RiskRanking  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' or  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' or CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            //    SqlDataAdapter dscmd24 = new SqlDataAdapter(sql24, conn);
            //    DataSet ds24 = new DataSet();
            //    dscmd24.Fill(ds24);
            //    xlWorkSheet.Cells[1, 47] = "CriticalityRating";

            //    for (int i = 0; i <= ds24.Tables[0].Rows.Count - 1; i++)
            //    {
            //        string data = ds24.Tables[0].Rows[i].ItemArray[0].ToString();
            //        xlWorkSheet.Cells[i + 2, 47] = data;
                    
            //    }
            //    chartRange = xlWorkSheet.get_Range("a2", "au50");
            //    chartRange.Font.Italic = true;
            //    chartRange.Font.Bold = true;
            //    chartRange.Cells.Interior.Color = System.Drawing.Color.FromKnownColor(KnownColor.Yellow);
            //    chartRange.Cells.Font.Color = System.Drawing.Color.FromKnownColor(KnownColor.Red);
            //    chartRange.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            //    chartRange.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

            //    chartRange = xlWorkSheet.get_Range("a1", "au1");
            //    chartRange.Font.Bold = true;
            //    chartRange.Cells.Interior.Color = System.Drawing.Color.FromKnownColor(KnownColor.GreenYellow);
            //    chartRange.Cells.EntireColumn.AutoFit();
            //    chartRange.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlDouble;
                             
            //    xlWorkBook.SaveAs(Server.MapPath("Files/"+"RBIDataSheet.xls"), Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            //    xlWorkBook.Close(true, misValue, misValue);
            //    xlApp.Quit();

            //    releaseObject(xlWorkSheet);
            //    releaseObject(xlWorkBook);
            //    releaseObject(xlApp);
                
            //    Response.ContentType = "xls";
            //    Response.AppendHeader("Content-Disposition", "attachment; filename=RBIDataSheet.xls");
            //    Response.TransmitFile(Server.MapPath("~/Files/RBIDataSheet.xls"));
            //    Response.End();
                
            //}
        }

        catch (Exception ex)
        {
            BusinessTier.DisposeConnection(conn);
            StiWebViewer1.Visible = false;
           // lblStatus.Text = "An error occured while trying to generate report, Please try again/Contact your administrator";
            lblStatus.Text = ex.Message.ToString();
            return;
        }

    }

    private void releaseObject(object obj)
    {

        try
        {

            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
            obj = null;
        }
        catch (Exception ex)
        {
            obj = null;
            // MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
        }

        finally
        {
            GC.Collect();
        }

    }

    protected void btn_Report_Print_Click(object sender, EventArgs e)
    {
    }

    private static void OnPrintingDocument(object sender, EventArgs e)
    {
        StiReport report = sender as StiReport;
        report.Print();
    }


}