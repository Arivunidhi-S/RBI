using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;

using System.Globalization;
using System.Collections;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.OleDb;
using System.Drawing;

public partial class RiskRanking : System.Web.UI.Page
{
    public DataTable dtMenuItems = new DataTable();

    public DataTable dtSubMenuItems = new DataTable();

    private void RR_Visible(Boolean tf)
    {
        lbl_RR_POFVal.Visible = tf;
        lbl_RR_POFCateVisible.Visible = tf;
        lbl_RR_COFVal.Visible = tf;
        lbl_RR_COFCate.Visible = tf;
        lbl_RR_FinCOFVal.Visible = tf;
        lbl_RR_FinCOFCate.Visible = tf;
        cbo_RR_ChooseCOF.Visible = tf;
        lbl_RR_COFRisk.Visible = tf;
        lbl_RR_FinCOFRisk.Visible = tf;
        btn_RiskRanking_Save.Visible = tf;
        //btnDelete.Visible = tf;
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
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
        RR_Visible(false);
        cbo_RiskRanking_Choose.Enabled = false;
        btn_RiskRanking_Select.Enabled = false;
       btnDelete.Enabled = false;
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
        RadComboBox combobox = (RadComboBox)sender;
        cboEquipment.Items.Clear();
        cboComponent.Items.Clear();
        cboEquipment.Text = string.Empty;
        cboComponent.Text = string.Empty;
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            string sql1 = "select EquAutoID,EqupType,EqupID from Tbl_EquipmentAsset where ProcessAreaID='" + cboProcessArea.SelectedValue.ToString() + "' and deleted=0";
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
            // lblStatus.Text = "Err:Select correct Product Item" + ex.Message.ToString();
            return;
        }
    }

    protected void OnSelectedIndexChanged_cboEquipment(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
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
            RadGrid1.DataSource = DataSourceHelper(EquipType, "0");
            RadGrid1.Rebind();
            BusinessTier.DisposeAdapter(adapter1);
            BusinessTier.DisposeConnection(conn);
        }
        catch (Exception ex)
        {
            BusinessTier.DisposeConnection(conn);
            // lblStatus.Text = "Err:Select correct Product Item" + ex.Message.ToString();
            return;
        }
    }

    protected void OnSelectedIndexChanged_cboComponent(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()))
        {
            RadGrid1.DataSource = DataSourceHelper("0", "0");
            RadGrid1.Rebind();
        }
        else
        {
            string EquipType = cboEquipment.SelectedValue.ToString();
            string CompNo = cboComponent.SelectedValue.ToString();
            RadGrid1.DataSource = DataSourceHelper(EquipType, CompNo);
            RadGrid1.Rebind();
        }
       
        RR_Visible(true);
        btnDelete.Enabled = true;
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            string sql = "select Dftotal,Category,maxval,maxcate,CAInjTotal,CAinjCate,SUMPFC,FinCate from VW_RiskRanking where ProcID=" + Convert.ToInt32(cboProcessArea.SelectedValue.ToString()) + " and EquID=" + Convert.ToInt32(cboEquipment.SelectedValue.ToString()) + "  and CompID=" + Convert.ToInt32(cboComponent.SelectedValue.ToString()) + "";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                lbl_RR_POFVal.Text = rd["Dftotal"].ToString();
                lbl_RR_POFCateVisible.Text = rd["Category"].ToString();

                // Display POF Number

                if (lbl_RR_POFCateVisible.Text == "1")
                {
                    lbl_RR_POFCate.Text = "A";
                }
                else if (lbl_RR_POFCateVisible.Text == "2")
                {
                    lbl_RR_POFCate.Text = "B";
                }
                else if (lbl_RR_POFCateVisible.Text == "3")
                {
                    lbl_RR_POFCate.Text = "C";
                }
                else if (lbl_RR_POFCateVisible.Text == "4")
                {
                    lbl_RR_POFCate.Text = "D";
                }
                else if (lbl_RR_POFCateVisible.Text == "5")
                {
                    lbl_RR_POFCate.Text = "E";
                }

                double flam = 0, nonflame = 0;
                if (string.IsNullOrEmpty(rd["maxval"].ToString()))
                {
                    if (string.IsNullOrEmpty(rd["CAInjTotal"].ToString()))
                    {
                        flam = 0;
                    }
                    else
                    {
                        flam = Convert.ToDouble(rd["CAInjTotal"].ToString());
                    }
                    //lbl_RR_COFVal.Text = rd["CAInjTotal"].ToString();
                    //lbl_RR_COFCate.Text = rd["CAinjCate"].ToString();
                }
                if (string.IsNullOrEmpty(rd["CAInjTotal"].ToString()))
                {
                    if (string.IsNullOrEmpty(rd["maxval"].ToString()))
                    {
                        nonflame = 0;
                    }
                    else
                    {
                        nonflame = Convert.ToDouble(rd["maxval"].ToString());
                        //lbl_RR_COFCate.Text = rd["maxcate"].ToString();
                    }
                }
                if (nonflame > flam)
                {
                    //lbl_RR_COFVal.Text = rd["maxval"].ToString();
                    lbl_RR_COFVal.Text = nonflame.ToString();
                    lbl_RR_COFCate.Text = rd["maxcate"].ToString();
                }
                else
                {
                    //lbl_RR_COFVal.Text = rd["CAInjTotal"].ToString();
                    lbl_RR_COFVal.Text = flam.ToString();
                    lbl_RR_COFCate.Text = rd["CAinjCate"].ToString();
                }

                if (string.IsNullOrEmpty(rd["SUMPFC"].ToString()))
                {
                    lbl_RR_FinCOFVal.Text = "None";
                    lbl_RR_FinCOFCate.Text = "None";
                }
                else
                {
                    lbl_RR_FinCOFVal.Text = rd["SUMPFC"].ToString();
                    lbl_RR_FinCOFCate.Text = rd["FinCate"].ToString();
                }
            }
            else
            {
                RR_Visible(false);
            }
            rd.Close();
            string sql1 = "select " + lbl_RR_COFCate.Text.ToString() + " as Cate from Ref_RiskRanking where Category='" + lbl_RR_POFCate.Text.ToString() + "'";
            SqlCommand cmd1 = new SqlCommand(sql1, conn);
            SqlDataReader rd1 = cmd1.ExecuteReader();
            if (rd1.Read())
            {
                lbl_RR_COFRisk.BackColor = Color.FromName(rd1["Cate"].ToString());

                if (rd1["Cate"].ToString() == "Red")
                {
                    lbl_RR_COFRisk.Text = "High";
                }
                else if (rd1["Cate"].ToString() == "Orange")
                {
                    lbl_RR_COFRisk.Text = "Medium High";
                }
                else if (rd1["Cate"].ToString() == "Yellow")
                {
                    lbl_RR_COFRisk.Text = "Medium";
                }
                else if (rd1["Cate"].ToString() == "Lime")
                {
                    lbl_RR_COFRisk.Text = "Low";
                }

            }
            rd1.Close();

            string sql2 = "select " + lbl_RR_FinCOFCate.Text.ToString() + " as Cate from Ref_RiskRanking where Category='" + lbl_RR_POFCate.Text.ToString() + "'";
            SqlCommand cmd2 = new SqlCommand(sql2, conn);
            SqlDataReader rd2 = cmd2.ExecuteReader();
            if (rd2.Read())
            {
                lbl_RR_FinCOFRisk.BackColor = Color.FromName(rd2["Cate"].ToString());

                if (rd2["Cate"].ToString() == "Red")
                {
                    lbl_RR_FinCOFRisk.Text = "High";
                }
                else if (rd2["Cate"].ToString() == "Orange")
                {
                    lbl_RR_FinCOFRisk.Text = "Medium High";
                }
                else if (rd2["Cate"].ToString() == "Yellow")
                {
                    lbl_RR_FinCOFRisk.Text = "Medium";
                }
                else if (rd2["Cate"].ToString() == "Lime")
                {
                    lbl_RR_FinCOFRisk.Text = "Low";
                }
            }
            rd2.Close();
            
        }
        catch (Exception ex)
        {
            BusinessTier.DisposeConnection(conn);
            // lblStatus.Text = ex.Message.ToString();
            return;
        }

    }

    //===============================================================================================
    protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            if (!(string.IsNullOrEmpty(cboEquipment.Text.ToString())) && !(string.IsNullOrEmpty(cboComponent.Text.ToString())))
                RadGrid1.DataSource = DataSourceHelper(cboEquipment.SelectedValue.ToString().Trim(), cboComponent.SelectedValue.ToString().Trim());
            else if (!(string.IsNullOrEmpty(cboComponent.Text.ToString())))
                RadGrid1.DataSource = DataSourceHelper(cboEquipment.SelectedValue.ToString().Trim(), "0");
            else
                RadGrid1.DataSource = DataSourceHelper("0", "0");
            //RadGrid1.DataSource = DataSourceHelper();
        }
        catch (Exception ex)
        {
            //ShowMessage(9);
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            //InsertLogAuditTrail(Session["sesUserID"].ToString(), "ppeissue", "NeedDataSource", ex.ToString(), "Audit");
        }
    }

    public DataTable DataSourceHelper(string cboEquipment, string cboComponent)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Close();
        string sql = "";
        if (cboEquipment.ToString().Trim() == "0")
            sql = "SELECT * from VW_RiskRanking_Save where deleted=0 and CreatedBy='" + Session["sesUserID"].ToString() + "'";
        else if (cboComponent.ToString().Trim() == "0")
            sql = "SELECT * from VW_RiskRanking_Save where EquID = '" + cboEquipment.ToString().Trim() + "' and deleted=0 and CreatedBy='" + Session["sesUserID"].ToString() + "'";
        else
            sql = "SELECT * from VW_RiskRanking_Save where EquID = '" + cboEquipment.ToString().Trim() + "' and CompID = '" + cboComponent.ToString().Trim() + "' and deleted=0 and CreatedBy='" + Session["sesUserID"].ToString() + "'";
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, conn);
        DataTable g_datatable = new DataTable();
        DataTable g_datatable1 = new DataTable();
        foreach (DataRow drow in g_datatable.Rows)
        {

        }
        sqlDataAdapter.Fill(g_datatable);
        BusinessTier.DisposeAdapter(sqlDataAdapter);
        BusinessTier.DisposeConnection(conn);
        return g_datatable;

    }

    protected void btn_RiskRanking_Save_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cbo_RR_ChooseCOF.Text.ToString()))
        {
            lblStatus.Text = "Please Select ChooseCOF";
            lblStatus.ForeColor = Color.Red;
        }
        else
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            try
            {
                string flag = string.Empty;
                string strqrydup = "Select  *  from RiskRanking  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' and  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' and CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
                SqlCommand cmddup = new SqlCommand(strqrydup, conn);
                SqlDataReader readerdup = cmddup.ExecuteReader();
                if (readerdup.Read())
                {
                    readerdup.Close();
                    flag = "U";
                    lblStatus.Text = "Successfully Value Updated";
                    lblStatus.ForeColor = Color.Blue;
                }
                else
                {
                    readerdup.Close();
                    flag = "N";
                    lblStatus.Text = "Successfully Value Inserted";
                    lblStatus.ForeColor = Color.Blue;
                }
                string choosecate = string.Empty;
                if (cbo_RR_ChooseCOF.SelectedValue.ToString() == "A")
                {
                    choosecate = lbl_RR_COFRisk.Text.ToString();
                }
                else
                {
                    choosecate = lbl_RR_FinCOFRisk.Text.ToString();
                }
                int intFlag = BusinessTier.RiskRankingSave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), cboEquipment.SelectedItem.Attributes["EqupType"].ToString(), cboComponent.SelectedItem.Attributes["CompName"].ToString(), lbl_RR_POFVal.Text.ToString(), lbl_RR_POFCateVisible.Text.ToString(), lbl_RR_COFVal.Text.ToString(), lbl_RR_COFCate.Text.ToString(), lbl_RR_FinCOFVal.Text.ToString(), lbl_RR_FinCOFCate.Text.ToString(), lbl_RR_FinCOFVal.Text.ToString(), lbl_RR_FinCOFCate.Text.ToString(), lbl_RR_COFRisk.Text.ToString(), lbl_RR_FinCOFRisk.Text.ToString(), choosecate.ToString(), cbo_RR_ChooseCOF.SelectedItem.Text.ToString(), Convert.ToInt32(Session["sesUserID"].ToString()), flag);

                BusinessTier.DisposeConnection(conn);
                RadGrid1.DataSource = DataSourceHelper("0", "0");
                RadGrid1.Rebind();
            }
            catch (Exception ex)
            {
                BusinessTier.DisposeConnection(conn);
                // lblStatus.Text = ex.Message.ToString();
                lblStatus.ForeColor = Color.Red;
                return;
            }
        }
    }

    protected void btn_RiskRanking_ViewAll_Click(object sender, EventArgs e)
    {
        cbo_RiskRanking_Choose.Enabled = true;
        btn_RiskRanking_Select.Enabled = true;
        RadGrid1.DataSource = DataSourceHelper("0", "0");
        RadGrid1.Rebind();
    }

    protected void btn_RiskRanking_Select_Click(object sender, EventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        SqlConnection conn1 = BusinessTier.getConnection();
        conn.Open();
        conn1.Open();
        try
        {
            string concate = string.Empty;
            string choosecate = string.Empty;
            string strqrydup = "Select ProcID,EquID,CompID,POFCate,MaxVal,FinCOFCate from RiskRanking";
            SqlCommand cmddup = new SqlCommand(strqrydup, conn);
            SqlDataReader readerdup = cmddup.ExecuteReader();
            while (readerdup.Read())
            {

                if (cbo_RiskRanking_Choose.SelectedValue.ToString() == "A")
                {
                    concate = readerdup["MaxVal"].ToString().Trim() + readerdup["POFCate"].ToString().Trim();
                }
                else
                {
                    concate = readerdup["FinCOFCate"].ToString().Trim() + readerdup["POFCate"].ToString().Trim();
                }

                if (concate.ToString().Trim() == "EA" || concate.ToString().Trim() == "EB" || concate.ToString().Trim() == "EC" || concate.ToString().Trim() == "DC" || concate.ToString().Trim() == "DD" || concate.ToString().Trim() == "CD" || concate.ToString().Trim() == "BE" || concate.ToString().Trim() == "AE")
                {
                    choosecate = "Medium High";
                }
                else if (concate.ToString().Trim() == "DA" || concate.ToString().Trim() == "DB" || concate.ToString().Trim() == "CC" || concate.ToString().Trim() == "BC" || concate.ToString().Trim() == "AC" || concate.ToString().Trim() == "BD" || concate.ToString().Trim() == "AD")
                {
                    choosecate = "Medium";
                }
                else if (concate.ToString().Trim() == "CD" || concate.ToString().Trim() == "EE" || concate.ToString().Trim() == "DE" || concate.ToString().Trim() == "CE")
                {
                    choosecate = "High";
                }
                else if (concate.ToString().Trim() == "CA" || concate.ToString().Trim() == "BA" || concate.ToString().Trim() == "AA" || concate.ToString().Trim() == "CB" || concate.ToString().Trim() == "BB" || concate.ToString().Trim() == "AB")
                {
                    choosecate = "Low";
                }

                int intFlag = BusinessTier.RiskRankingloop(conn1, Convert.ToInt32(readerdup["ProcID"].ToString().Trim()), Convert.ToInt32(readerdup["EquID"].ToString().Trim()), Convert.ToInt32(readerdup["CompID"].ToString().Trim()), choosecate.ToString(), cbo_RiskRanking_Choose.SelectedItem.Text.ToString(), Convert.ToInt32(Session["sesUserID"].ToString()), "U1");
            }
            readerdup.Close();

            BusinessTier.DisposeConnection(conn);
            BusinessTier.DisposeConnection(conn1);
        }
        catch (Exception ex)
        {
            BusinessTier.DisposeConnection(conn);
            BusinessTier.DisposeConnection(conn1);
            //lblStatus.Text = ex.Message.ToString();
            lblStatus.ForeColor = Color.Red;
            return;
        }
        RadGrid1.DataSource = DataSourceHelper("0", "0");
        RadGrid1.Rebind();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        if (cboProcessArea.Text == "")
        {
            lblStatus.Text = "Please Select ProcessArea";
            lblStatus.ForeColor = Color.Maroon;
            return;
        }
        if (cboEquipment.Text == "")
        {
            lblStatus.Text = "Please Select Equipment";
            lblStatus.ForeColor = Color.Maroon;
            return;
        }
        if (cboComponent.Text == "")
        {
            lblStatus.Text = "Please Select Component";
            lblStatus.ForeColor = Color.Maroon;
            return;
        }
        try
        {
            //string strqrydup = "update COF_Flammable set Deleted=1 where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' and  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' and CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and Deleted=0";
            string strqrydup = "delete from RiskRanking where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' and  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' and CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and Deleted=0";

            SqlCommand cmddup = new SqlCommand(strqrydup, conn);
            cmddup.ExecuteNonQuery();

            BusinessTier.DisposeConnection(conn);
            lblStatus.Text = "POF Total Value Successfully Deleted";
            lblStatus.ForeColor = Color.Maroon;
            btnDelete.Enabled = false;
            string EquipType = cboEquipment.SelectedValue.ToString();
            string CompNo = cboComponent.SelectedValue.ToString();
            RadGrid1.DataSource = DataSourceHelper(EquipType, CompNo);
            RadGrid1.Rebind();
        }
        catch (Exception ex)
        {
            //InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btnPOFTotalDelete_Click", ex.ToString(), "Audit");
            lblStatus.Text = ex.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }
}