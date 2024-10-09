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

public partial class InspectionPlan : System.Web.UI.Page
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
            lblName.Text = "Hi, " + Session["sesUserName"].ToString();
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
        btn_inspectionPlan_Save.Visible = false;
        Thin_Visible(false);
        ExeCor_Visible(false);
        CUIDamage_Visible(false);
        ExtCLSCC_Visible(false);
        ExtCUI_Visible(false);
        Caustic_Visible(false);
        Amine_Visible(false);
        Sulfide_Visible(false);
        H2S_Visible(false);
        Carbonate_Visible(false);
        PTA_Visible(false);
        CLSCC_Visible(false);
        HSCHF_Visible(false);
        SOHIC_Visible(false);
        HTHA_Visible(false);
    }

    //1---------------Thinning Visible true or false
    private void Thin_Visible(Boolean tf)
    {
        lbl_Thin_Date.Visible = tf;
        lbl_Thin_Damage.Visible = tf;
        lbl_Thin_InsEff.Visible = tf;
        lbl_Thin_IntruIns.Visible = tf;
        lbl_Thin_NonIntruIns.Visible = tf;
        dt_Thin_InsDt.Visible = tf;
        cbo_Thin_inspecEffec.Visible = tf;
    }
    //2---------------ExternalCorrosion Visible true or false
    private void ExeCor_Visible(Boolean tf)
    {
        lbl_ExeCor_Date.Visible = tf;
        lbl_ExeCor_Damage.Visible = tf;
        lbl_ExeCor_InsEff.Visible = tf;
        lbl_ExeCor_IntruIns.Visible = tf;
        lbl_ExeCor_NonIntruIns.Visible = tf;
        dt_ExeCor_InsDt.Visible = tf;
        cbo_ExeCor_inspecEffec.Visible = tf;
    }
    //3---------------CUI Damage Visible true or false
    private void CUIDamage_Visible(Boolean tf)
    {
        lbl_CUIDamage_Date.Visible = tf;
        lbl_CUIDamage_Damage.Visible = tf;
        lbl_CUIDamage_InsEff.Visible = tf;
        lbl_CUIDamage_IntruIns.Visible = tf;
        lbl_CUIDamage_NonIntruIns.Visible = tf;
        dt_CUIDamage_InsDt.Visible = tf;
        cbo_CUIDamage_inspecEffec.Visible = tf;
    }
    //4---------------External CLSCC Visible true or false
    private void ExtCLSCC_Visible(Boolean tf)
    {
        lbl_ExtCLSCC_Date.Visible = tf;
        lbl_ExtCLSCC_Damage.Visible = tf;
        lbl_ExtCLSCC_InsEff.Visible = tf;
        lbl_ExtCLSCC_IntruIns.Visible = tf;
        lbl_ExtCLSCC_NonIntruIns.Visible = tf;
        dt_ExtCLSCC_InsDt.Visible = tf;
        cbo_ExtCLSCC_inspecEffec.Visible = tf;
    }
    //5---------------External CUI CLSCC Visible true or false
    private void ExtCUI_Visible(Boolean tf)
    {
        lbl_ExtCUI_Date.Visible = tf;
        lbl_ExtCUI_Damage.Visible = tf;
        lbl_ExtCUI_InsEff.Visible = tf;
        lbl_ExtCUI_IntruIns.Visible = tf;
        lbl_ExtCUI_NonIntruIns.Visible = tf;
        dt_ExtCUI_InsDt.Visible = tf;
        cbo_ExtCUI_inspecEffec.Visible = tf;
    }
    //6---------------Caustic Visible true or false
    private void Caustic_Visible(Boolean tf)
    {
        lbl_Caustic_Date.Visible = tf;
        lbl_Caustic_Damage.Visible = tf;
        lbl_Caustic_InsEff.Visible = tf;
        lbl_Caustic_IntruIns.Visible = tf;
        lbl_Caustic_NonIntruIns.Visible = tf;
        dt_Caustic_InsDt.Visible = tf;
        cbo_Caustic_inspecEffec.Visible = tf;
    }
    //7---------------Amine Visible true or false
    private void Amine_Visible(Boolean tf)
    {
        lbl_Amine_Date.Visible = tf;
        lbl_Amine_Damage.Visible = tf;
        lbl_Amine_InsEff.Visible = tf;
        lbl_Amine_IntruIns.Visible = tf;
        lbl_Amine_NonIntruIns.Visible = tf;
        dt_Amine_InsDt.Visible = tf;
        cbo_Amine_inspecEffec.Visible = tf;
    }
    //8---------------Sulfide Visible true or false
    private void Sulfide_Visible(Boolean tf)
    {
        lbl_Sulfide_Date.Visible = tf;
        lbl_Sulfide_Damage.Visible = tf;
        lbl_Sulfide_InsEff.Visible = tf;
        lbl_Sulfide_IntruIns.Visible = tf;
        lbl_Sulfide_NonIntruIns.Visible = tf;
        dt_Sulfide_InsDt.Visible = tf;
        cbo_Sulfide_inspecEffec.Visible = tf;
    }
    //9---------------H2S Visible true or false
    private void H2S_Visible(Boolean tf)
    {
        lbl_H2S_Date.Visible = tf;
        lbl_H2S_Damage.Visible = tf;
        lbl_H2S_InsEff.Visible = tf;
        lbl_H2S_IntruIns.Visible = tf;
        lbl_H2S_NonIntruIns.Visible = tf;
        dt_H2S_InsDt.Visible = tf;
        cbo_H2S_inspecEffec.Visible = tf;
    }
    //10---------------Carbonate Visible true or false
    private void Carbonate_Visible(Boolean tf)
    {
        lbl_Carbonate_Date.Visible = tf;
        lbl_Carbonate_Damage.Visible = tf;
        lbl_Carbonate_InsEff.Visible = tf;
        lbl_Carbonate_IntruIns.Visible = tf;
        lbl_Carbonate_NonIntruIns.Visible = tf;
        dt_Carbonate_InsDt.Visible = tf;
        cbo_Carbonate_inspecEffec.Visible = tf;
    }
    //11---------------PTA Visible true or false
    private void PTA_Visible(Boolean tf)
    {
        lbl_PTA_Date.Visible = tf;
        lbl_PTA_Damage.Visible = tf;
        lbl_PTA_InsEff.Visible = tf;
        lbl_PTA_IntruIns.Visible = tf;
        lbl_PTA_NonIntruIns.Visible = tf;
        dt_PTA_InsDt.Visible = tf;
        cbo_PTA_inspecEffec.Visible = tf;
    }
    //12---------------CLSCC Visible true or false
    private void CLSCC_Visible(Boolean tf)
    {
        lbl_CLSCC_Date.Visible = tf;
        lbl_CLSCC_Damage.Visible = tf;
        lbl_CLSCC_InsEff.Visible = tf;
        lbl_CLSCC_IntruIns.Visible = tf;
        lbl_CLSCC_NonIntruIns.Visible = tf;
        dt_CLSCC_InsDt.Visible = tf;
        cbo_CLSCC_inspecEffec.Visible = tf;
    }
    //13---------------HSC-HF Visible true or false
    private void HSCHF_Visible(Boolean tf)
    {
        lbl_HSCHF_Date.Visible = tf;
        lbl_HSCHF_Damage.Visible = tf;
        lbl_HSCHF_InsEff.Visible = tf;
        lbl_HSCHF_IntruIns.Visible = tf;
        lbl_HSCHF_NonIntruIns.Visible = tf;
        dt_HSCHF_InsDt.Visible = tf;
        cbo_HSCHF_inspecEffec.Visible = tf;
    }
    //14---------------HIC/SOHIC-HF Visible true or false
    private void SOHIC_Visible(Boolean tf)
    {
        lbl_SOHIC_Date.Visible = tf;
        lbl_SOHIC_Damage.Visible = tf;
        lbl_SOHIC_InsEff.Visible = tf;
        lbl_SOHIC_IntruIns.Visible = tf;
        lbl_SOHIC_NonIntruIns.Visible = tf;
        dt_SOHIC_InsDt.Visible = tf;
        cbo_SOHIC_inspecEffec.Visible = tf;
    }
    //15---------------HTHA Visible true or false
    private void HTHA_Visible(Boolean tf)
    {
        lbl_HTHA_Date.Visible = tf;
        lbl_HTHA_Damage.Visible = tf;
        lbl_HTHA_InsEff.Visible = tf;
        lbl_HTHA_IntruIns.Visible = tf;
        lbl_HTHA_NonIntruIns.Visible = tf;
        dt_HTHA_InsDt.Visible = tf;
        cbo_HTHA_inspecEffec.Visible = tf;
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
            //lblStatus.Text = "Err:Select correct Product Item" + ex.Message.ToString();
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
                item.Text = row["CompName"].ToString();
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
            BusinessTier.DisposeAdapter(adapter1);
            BusinessTier.DisposeConnection(conn);
        }
        catch (Exception ex)
        {
            BusinessTier.DisposeConnection(conn);
          //  lblStatus.Text = "Err:Select correct Product Item" + ex.Message.ToString();
            return;
        }
    }

    protected void OnSelectedIndexChanged_cboComponent(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            //  <%--------------1--------Thinning------------------------%>
            string sql = "select InspectCate,InspectDate from InspectionPlan where ProcID=" + Convert.ToInt32(cboProcessArea.SelectedValue.ToString()) + " and EquID=" + Convert.ToInt32(cboEquipment.SelectedValue.ToString()) + "  and CompID=" + Convert.ToInt32(cboComponent.SelectedValue.ToString()) + " and DamageFact='Thinning'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                Thin_Visible(true);
                lbl_Thin_Date.Text = Convert.ToDateTime(rd["InspectDate"]).ToString("dd-MMM-yyyy");

                string sql1 = "select InsEffCate,IntrInsp,NonIntrInsp from InspectEffect where DamageFactor='Thinning' and InsCate='" + rd["InspectCate"].ToString() + "'";
                rd.Close();
                SqlCommand cmd1 = new SqlCommand(sql1, conn);
                SqlDataReader rd1 = cmd1.ExecuteReader();
                rd1.Read();
                lbl_Thin_InsEff.Text = rd1["InsEffCate"].ToString();
                lbl_Thin_IntruIns.Text = rd1["IntrInsp"].ToString();
                lbl_Thin_NonIntruIns.Text = rd1["NonIntrInsp"].ToString();
                rd1.Close();
            }
            else
            {
                rd.Close();
                Thin_Visible(false);
            }

            //<%--------------2--------ExternalCorrosion------------------------%>
            string sql2 = "select InspectCate,InspectDate from InspectionPlan where ProcID=" + Convert.ToInt32(cboProcessArea.SelectedValue.ToString()) + " and EquID=" + Convert.ToInt32(cboEquipment.SelectedValue.ToString()) + "  and CompID=" + Convert.ToInt32(cboComponent.SelectedValue.ToString()) + "  and DamageFact='ExternalCorrosion' ";
            SqlCommand cmd2 = new SqlCommand(sql2, conn);
            SqlDataReader rd2 = cmd2.ExecuteReader();
            if (rd2.Read())
            {
                ExeCor_Visible(true);
                lbl_ExeCor_Date.Text = Convert.ToDateTime(rd2["InspectDate"]).ToString("dd-MMM-yyyy");

                string sql3 = "select InsEffCate,IntrInsp,NonIntrInsp from InspectEffect where DamageFactor='ExternalCorrosion' and InsCate='" + rd2["InspectCate"].ToString() + "'";
                rd2.Close();
                SqlCommand cmd3 = new SqlCommand(sql3, conn);
                SqlDataReader rd3 = cmd3.ExecuteReader();
                rd3.Read();
                lbl_ExeCor_InsEff.Text = rd3["InsEffCate"].ToString();
                lbl_ExeCor_IntruIns.Text = rd3["IntrInsp"].ToString();
                lbl_ExeCor_NonIntruIns.Text = rd3["NonIntrInsp"].ToString();
                rd3.Close();
            }
            else
            {
                rd2.Close();
                ExeCor_Visible(false);
            }
            // <%--------------3--------CUI Damage------------------------%>
            string sql4 = "select InspectCate,InspectDate from InspectionPlan where ProcID=" + Convert.ToInt32(cboProcessArea.SelectedValue.ToString()) + " and EquID=" + Convert.ToInt32(cboEquipment.SelectedValue.ToString()) + "  and CompID=" + Convert.ToInt32(cboComponent.SelectedValue.ToString()) + "  and DamageFact='CUIDamage' ";
            SqlCommand cmd4 = new SqlCommand(sql4, conn);
            SqlDataReader rd4 = cmd4.ExecuteReader();
            if (rd4.Read())
            {
                CUIDamage_Visible(true);
                lbl_CUIDamage_Date.Text =Convert.ToDateTime( rd4["InspectDate"]).ToString("dd-MMM-yyyy");

                string sql5 = "select InsEffCate,IntrInsp,NonIntrInsp from InspectEffect where DamageFactor='CUIDamage' and InsCate='" + rd4["InspectCate"].ToString() + "'";
                rd4.Close();
                SqlCommand cmd5 = new SqlCommand(sql5, conn);
                SqlDataReader rd5 = cmd5.ExecuteReader();
                rd5.Read();
                lbl_CUIDamage_InsEff.Text = rd5["InsEffCate"].ToString();
                lbl_CUIDamage_IntruIns.Text = rd5["IntrInsp"].ToString();
                lbl_CUIDamage_NonIntruIns.Text = rd5["NonIntrInsp"].ToString();
                rd5.Close();
            }
            else
            {
                rd4.Close();
                CUIDamage_Visible(false);
            }

            //   <%-------------4---------External CLSCC------------------------%>
            string sql6 = "select InspectCate,InspectDate from InspectionPlan where ProcID=" + Convert.ToInt32(cboProcessArea.SelectedValue.ToString()) + " and EquID=" + Convert.ToInt32(cboEquipment.SelectedValue.ToString()) + "  and CompID=" + Convert.ToInt32(cboComponent.SelectedValue.ToString()) + "  and DamageFact='ExternalCLSCC'";
            SqlCommand cmd6 = new SqlCommand(sql6, conn);
            SqlDataReader rd6 = cmd6.ExecuteReader();
            if (rd6.Read())
            {
                ExtCLSCC_Visible(true);
                lbl_ExtCLSCC_Date.Text = Convert.ToDateTime(rd6["InspectDate"]).ToString("dd-MMM-yyyy");

                string sql7 = "select InsEffCate,IntrInsp,NonIntrInsp from InspectEffect where DamageFactor='ExternalCLSCC' and InsCate='" + rd6["InspectCate"].ToString() + "'";
                rd6.Close();
                SqlCommand cmd7 = new SqlCommand(sql7, conn);
                SqlDataReader rd7 = cmd7.ExecuteReader();
                rd7.Read();
                lbl_ExtCLSCC_InsEff.Text = rd7["InsEffCate"].ToString();
                lbl_ExtCLSCC_IntruIns.Text = rd7["IntrInsp"].ToString();
                lbl_ExtCLSCC_NonIntruIns.Text = rd7["NonIntrInsp"].ToString();
                rd7.Close();
            }
            else
            {
                rd6.Close();
                ExtCLSCC_Visible(false);
            }

            //     <%--------------5--------External CUI CLSCC------------------------%>
            string sql8 = "select InspectCate,InspectDate from InspectionPlan where ProcID=" + Convert.ToInt32(cboProcessArea.SelectedValue.ToString()) + " and EquID=" + Convert.ToInt32(cboEquipment.SelectedValue.ToString()) + "  and CompID=" + Convert.ToInt32(cboComponent.SelectedValue.ToString()) + "  and DamageFact='External CUI CLSCC'";
            SqlCommand cmd8 = new SqlCommand(sql8, conn);
            SqlDataReader rd8 = cmd8.ExecuteReader();
            if (rd8.Read())
            {
                ExtCUI_Visible(true);
                lbl_ExtCUI_Date.Text = Convert.ToDateTime(rd8["InspectDate"]).ToString("dd-MMM-yyyy");

                string sql9 = "select InsEffCate,IntrInsp,NonIntrInsp from InspectEffect where DamageFactor='External CUI CLSCC' and InsCate='" + rd8["InspectCate"].ToString() + "'";
                rd8.Close();
                SqlCommand cmd9 = new SqlCommand(sql9, conn);
                SqlDataReader rd9 = cmd9.ExecuteReader();
                rd9.Read();
                lbl_ExtCUI_InsEff.Text = rd9["InsEffCate"].ToString();
                lbl_ExtCUI_IntruIns.Text = rd9["IntrInsp"].ToString();
                lbl_ExtCUI_NonIntruIns.Text = rd9["NonIntrInsp"].ToString();
                rd9.Close();
            }
            else
            {
                rd8.Close();
                ExtCUI_Visible(false);
            }
            //    <%------------6----------Caustic------------------------%>
            string sql10 = "select InspectCate,InspectDate from InspectionPlan where ProcID=" + Convert.ToInt32(cboProcessArea.SelectedValue.ToString()) + " and EquID=" + Convert.ToInt32(cboEquipment.SelectedValue.ToString()) + "  and CompID=" + Convert.ToInt32(cboComponent.SelectedValue.ToString()) + "  and DamageFact='Caustic'";
            SqlCommand cmd10 = new SqlCommand(sql10, conn);
            SqlDataReader rd10 = cmd10.ExecuteReader();
            if (rd10.Read())
            {
                Caustic_Visible(true);
                lbl_Caustic_Date.Text = Convert.ToDateTime(rd10["InspectDate"]).ToString("dd-MMM-yyyy");

                string sql11 = "select InsEffCate,IntrInsp,NonIntrInsp from InspectEffect where DamageFactor='Caustic' and InsCate='" + rd10["InspectCate"].ToString() + "'";
                rd10.Close();
                SqlCommand cmd11 = new SqlCommand(sql11, conn);
                SqlDataReader rd11 = cmd11.ExecuteReader();
                rd11.Read();
                lbl_Caustic_InsEff.Text = rd11["InsEffCate"].ToString();
                lbl_Caustic_IntruIns.Text = rd11["IntrInsp"].ToString();
                lbl_Caustic_NonIntruIns.Text = rd11["NonIntrInsp"].ToString();
                rd11.Close();
            }
            else
            {
                rd10.Close();
                Caustic_Visible(false);
            }
            //     <%--------------7--------Amine------------------------%>
            string sql12 = "select InspectCate,InspectDate from InspectionPlan where ProcID=" + Convert.ToInt32(cboProcessArea.SelectedValue.ToString()) + " and EquID=" + Convert.ToInt32(cboEquipment.SelectedValue.ToString()) + "  and CompID=" + Convert.ToInt32(cboComponent.SelectedValue.ToString()) + "  and DamageFact='Amine'";
            SqlCommand cmd12 = new SqlCommand(sql12, conn);
            SqlDataReader rd12 = cmd12.ExecuteReader();
            if (rd12.Read())
            {
                Amine_Visible(true);
                lbl_Amine_Date.Text = Convert.ToDateTime(rd12["InspectDate"]).ToString("dd-MMM-yyyy");

                string sql11 = "select InsEffCate,IntrInsp,NonIntrInsp from InspectEffect where DamageFactor='Amine' and InsCate='" + rd12["InspectCate"].ToString() + "'";
                rd12.Close();
                SqlCommand cmd11 = new SqlCommand(sql11, conn);
                SqlDataReader rd11 = cmd11.ExecuteReader();
                rd11.Read();
                lbl_Amine_InsEff.Text = rd11["InsEffCate"].ToString();
                lbl_Amine_IntruIns.Text = rd11["IntrInsp"].ToString();
                lbl_Amine_NonIntruIns.Text = rd11["NonIntrInsp"].ToString();
                rd11.Close();
            }
            else
            {
                rd12.Close();
                Amine_Visible(false);
            }
            //     <%--------------8--------Sulfide------------------------%>
            string sql14 = "select InspectCate,InspectDate from InspectionPlan where ProcID=" + Convert.ToInt32(cboProcessArea.SelectedValue.ToString()) + " and EquID=" + Convert.ToInt32(cboEquipment.SelectedValue.ToString()) + "  and CompID=" + Convert.ToInt32(cboComponent.SelectedValue.ToString()) + "  and DamageFact='Sulfide'";
            SqlCommand cmd14 = new SqlCommand(sql14, conn);
            SqlDataReader rd14 = cmd14.ExecuteReader();
            if (rd14.Read())
            {
                Sulfide_Visible(true);
                lbl_Sulfide_Date.Text = Convert.ToDateTime(rd14["InspectDate"]).ToString("dd-MMM-yyyy");

                string sql11 = "select InsEffCate,IntrInsp,NonIntrInsp from InspectEffect where DamageFactor='Sulfide' and InsCate='" + rd14["InspectCate"].ToString() + "'";
                rd14.Close();
                SqlCommand cmd11 = new SqlCommand(sql11, conn);
                SqlDataReader rd11 = cmd11.ExecuteReader();
                rd11.Read();
                lbl_Sulfide_InsEff.Text = rd11["InsEffCate"].ToString();
                lbl_Sulfide_IntruIns.Text = rd11["IntrInsp"].ToString();
                lbl_Sulfide_NonIntruIns.Text = rd11["NonIntrInsp"].ToString();
                rd11.Close();
            }
            else
            {
                rd14.Close();
                Sulfide_Visible(false);
            }
            //         <%-------------9---------H2S------------------------%>
            string sql16 = "select InspectCate,InspectDate from InspectionPlan where ProcID=" + Convert.ToInt32(cboProcessArea.SelectedValue.ToString()) + " and EquID=" + Convert.ToInt32(cboEquipment.SelectedValue.ToString()) + "  and CompID=" + Convert.ToInt32(cboComponent.SelectedValue.ToString()) + "  and DamageFact='H2S'";
            SqlCommand cmd16 = new SqlCommand(sql16, conn);
            SqlDataReader rd16 = cmd16.ExecuteReader();
            if (rd16.Read())
            {
                H2S_Visible(true);
                lbl_H2S_Date.Text = Convert.ToDateTime(rd16["InspectDate"]).ToString("dd-MMM-yyyy");

                string sql11 = "select InsEffCate,IntrInsp,NonIntrInsp from InspectEffect where DamageFactor='H2S' and InsCate='" + rd16["InspectCate"].ToString() + "'";
                rd16.Close();
                SqlCommand cmd11 = new SqlCommand(sql11, conn);
                SqlDataReader rd11 = cmd11.ExecuteReader();
                rd11.Read();
                lbl_H2S_InsEff.Text = rd11["InsEffCate"].ToString();
                lbl_H2S_IntruIns.Text = rd11["IntrInsp"].ToString();
                lbl_H2S_NonIntruIns.Text = rd11["NonIntrInsp"].ToString();
                rd11.Close();
            }
            else
            {
                rd16.Close();
                H2S_Visible(false);
            }
            //          <%------------10----------Carbonate------------------------%>
            string sql18 = "select InspectCate,InspectDate from InspectionPlan where ProcID=" + Convert.ToInt32(cboProcessArea.SelectedValue.ToString()) + " and EquID=" + Convert.ToInt32(cboEquipment.SelectedValue.ToString()) + "  and CompID=" + Convert.ToInt32(cboComponent.SelectedValue.ToString()) + "  and DamageFact='Carbonate'";
            SqlCommand cmd18 = new SqlCommand(sql18, conn);
            SqlDataReader rd18 = cmd18.ExecuteReader();
            if (rd18.Read())
            {
                Carbonate_Visible(true);
                lbl_Carbonate_Date.Text = Convert.ToDateTime(rd18["InspectDate"]).ToString("dd-MMM-yyyy");

                string sql11 = "select InsEffCate,IntrInsp,NonIntrInsp from InspectEffect where DamageFactor='Carbonate' and InsCate='" + rd18["InspectCate"].ToString() + "'";
                rd18.Close();
                SqlCommand cmd11 = new SqlCommand(sql11, conn);
                SqlDataReader rd11 = cmd11.ExecuteReader();
                rd11.Read();
                lbl_Carbonate_InsEff.Text = rd11["InsEffCate"].ToString();
                lbl_Carbonate_IntruIns.Text = rd11["IntrInsp"].ToString();
                lbl_Carbonate_NonIntruIns.Text = rd11["NonIntrInsp"].ToString();
                rd11.Close();
            }
            else
            {
                rd18.Close();
                Carbonate_Visible(false);
            }
            //             <%----------11------------PTA------------------------%>
            string sql20 = "select InspectCate,InspectDate from InspectionPlan where ProcID=" + Convert.ToInt32(cboProcessArea.SelectedValue.ToString()) + " and EquID=" + Convert.ToInt32(cboEquipment.SelectedValue.ToString()) + "  and CompID=" + Convert.ToInt32(cboComponent.SelectedValue.ToString()) + "  and DamageFact='PTA'";
            SqlCommand cmd20 = new SqlCommand(sql20, conn);
            SqlDataReader rd20 = cmd20.ExecuteReader();
            if (rd20.Read())
            {
                PTA_Visible(true);
                lbl_PTA_Date.Text =Convert.ToDateTime(rd20["InspectDate"]).ToString("dd-MMM-yyyy");

                string sql11 = "select InsEffCate,IntrInsp,NonIntrInsp from InspectEffect where DamageFactor='PTA' and InsCate='" + rd20["InspectCate"].ToString() + "'";
                rd20.Close();
                SqlCommand cmd11 = new SqlCommand(sql11, conn);
                SqlDataReader rd11 = cmd11.ExecuteReader();
                rd11.Read();
                lbl_PTA_InsEff.Text = rd11["InsEffCate"].ToString();
                lbl_PTA_IntruIns.Text = rd11["IntrInsp"].ToString();
                lbl_PTA_NonIntruIns.Text = rd11["NonIntrInsp"].ToString();
                rd11.Close();
            }
            else
            {
                rd20.Close();
                PTA_Visible(false);
            }
            //             <%--------------12--------CLSCC------------------------%>
            string sql22 = "select InspectCate,InspectDate from InspectionPlan where ProcID=" + Convert.ToInt32(cboProcessArea.SelectedValue.ToString()) + " and EquID=" + Convert.ToInt32(cboEquipment.SelectedValue.ToString()) + "  and CompID=" + Convert.ToInt32(cboComponent.SelectedValue.ToString()) + "  and DamageFact='CLSCC'";
            SqlCommand cmd22 = new SqlCommand(sql22, conn);
            SqlDataReader rd22 = cmd22.ExecuteReader();
            if (rd22.Read())
            {
                CLSCC_Visible(true);
                lbl_CLSCC_Date.Text = Convert.ToDateTime(rd22["InspectDate"]).ToString("dd-MMM-yyyy");

                string sql11 = "select InsEffCate,IntrInsp,NonIntrInsp from InspectEffect where DamageFactor='CLSCC' and InsCate='" + rd22["InspectCate"].ToString() + "'";
                rd22.Close();
                SqlCommand cmd11 = new SqlCommand(sql11, conn);
                SqlDataReader rd11 = cmd11.ExecuteReader();
                rd11.Read();
                lbl_CLSCC_InsEff.Text = rd11["InsEffCate"].ToString();
                lbl_CLSCC_IntruIns.Text = rd11["IntrInsp"].ToString();
                lbl_CLSCC_NonIntruIns.Text = rd11["NonIntrInsp"].ToString();
                rd11.Close();
            }
            else
            {
                rd22.Close();
                CLSCC_Visible(false);
            }
            //               <%----------13------------HSC-HF------------------------%>
            string sql24 = "select InspectCate,InspectDate from InspectionPlan where ProcID=" + Convert.ToInt32(cboProcessArea.SelectedValue.ToString()) + " and EquID=" + Convert.ToInt32(cboEquipment.SelectedValue.ToString()) + "  and CompID=" + Convert.ToInt32(cboComponent.SelectedValue.ToString()) + "  and DamageFact='HSC-HF'";
            SqlCommand cmd24 = new SqlCommand(sql24, conn);
            SqlDataReader rd24 = cmd24.ExecuteReader();
            if (rd24.Read())
            {
                HSCHF_Visible(true);
                lbl_HSCHF_Date.Text = Convert.ToDateTime(rd24["InspectDate"]).ToString("dd-MMM-yyyy");

                string sql11 = "select InsEffCate,IntrInsp,NonIntrInsp from InspectEffect where DamageFactor='HSC-HF' and InsCate='" + rd24["InspectCate"].ToString() + "'";
                rd24.Close();
                SqlCommand cmd11 = new SqlCommand(sql11, conn);
                SqlDataReader rd11 = cmd11.ExecuteReader();
                rd11.Read();
                lbl_HSCHF_InsEff.Text = rd11["InsEffCate"].ToString();
                lbl_HSCHF_IntruIns.Text = rd11["IntrInsp"].ToString();
                lbl_HSCHF_NonIntruIns.Text = rd11["NonIntrInsp"].ToString();
                rd11.Close();
            }
            else
            {
                rd24.Close();
                HSCHF_Visible(false);
            }
            //               <%------------14----------HIC/SOHIC-HF------------------------%>
            string sql26 = "select InspectCate,InspectDate from InspectionPlan where ProcID=" + Convert.ToInt32(cboProcessArea.SelectedValue.ToString()) + " and EquID=" + Convert.ToInt32(cboEquipment.SelectedValue.ToString()) + "  and CompID=" + Convert.ToInt32(cboComponent.SelectedValue.ToString()) + "  and DamageFact='HIC/SOHIC-HF'";
            SqlCommand cmd26 = new SqlCommand(sql26, conn);
            SqlDataReader rd26 = cmd26.ExecuteReader();
            if (rd26.Read())
            {
                SOHIC_Visible(true);
                lbl_SOHIC_Date.Text = Convert.ToDateTime(rd26["InspectDate"]).ToString("dd-MMM-yyyy");

                string sql11 = "select InsEffCate,IntrInsp,NonIntrInsp from InspectEffect where DamageFactor='HIC/SOHIC-HF' and InsCate='" + rd26["InspectCate"].ToString() + "'";
                rd26.Close();
                SqlCommand cmd11 = new SqlCommand(sql11, conn);
                SqlDataReader rd11 = cmd11.ExecuteReader();
                rd11.Read();
                lbl_SOHIC_InsEff.Text = rd11["InsEffCate"].ToString();
                lbl_SOHIC_IntruIns.Text = rd11["IntrInsp"].ToString();
                lbl_SOHIC_NonIntruIns.Text = rd11["NonIntrInsp"].ToString();
                rd11.Close();
            }
            else
            { 
                rd26.Close();
                SOHIC_Visible(false);
            }
            //              <%------------15----------HTHA------------------------%>
            string sql28 = "select InspectCate,InspectDate from InspectionPlan where ProcID=" + Convert.ToInt32(cboProcessArea.SelectedValue.ToString()) + " and EquID=" + Convert.ToInt32(cboEquipment.SelectedValue.ToString()) + "  and CompID=" + Convert.ToInt32(cboComponent.SelectedValue.ToString()) + "  and DamageFact='HTHA'";
            SqlCommand cmd28 = new SqlCommand(sql28, conn);
            SqlDataReader rd28 = cmd28.ExecuteReader();
            if (rd28.Read())
            {
                HTHA_Visible(true);
                lbl_HTHA_Date.Text =Convert.ToDateTime(rd28["InspectDate"]).ToString("dd-MMM-yyyy");

                string sql11 = "select InsEffCate,IntrInsp,NonIntrInsp from InspectEffect where DamageFactor='HTHA' and InsCate='" + rd28["InspectCate"].ToString() + "'";
                rd28.Close();
                SqlCommand cmd11 = new SqlCommand(sql11, conn);
                SqlDataReader rd11 = cmd11.ExecuteReader();
                rd11.Read();
                lbl_HTHA_InsEff.Text = rd11["InsEffCate"].ToString();
                lbl_HTHA_IntruIns.Text = rd11["IntrInsp"].ToString();
                lbl_HTHA_NonIntruIns.Text = rd11["NonIntrInsp"].ToString();
                rd11.Close();
            }
            else
            {
                rd28.Close();
                HTHA_Visible(false);
            }
            BusinessTier.DisposeConnection(conn);
            btn_inspectionPlan_Save.Visible = true;
        }
        catch (Exception ex)
        {
            BusinessTier.DisposeConnection(conn);
           // lblStatus.Text = ex.Message.ToString();
            return;
        }
    }

    protected void btn_inspectionPlan_Save_Click(object sender, EventArgs e)
    {
        try
        {
            // lblStatus.Text = string.Empty;
            // SqlConnection conn = BusinessTier.getConnection();
            // conn.Open();

            //// <%--------------1--------Thinning------------------------%>

            // if (lbl_Thin_Date.Visible == true && cbo_Thin_inspecEffec.SelectedValue.ToString() != "")
            // {
            //     int intFlag1 = BusinessTier.InspectionPlan(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "Thinning", cbo_Thin_inspecEffec.SelectedValue.ToString(), Convert.ToDateTime(dt_Thin_InsDt.SelectedDate.ToString().Trim()), "U");
            //     lblStatus.Text = "Successfully Value Inserted";
            //     cbo_Thin_inspecEffec.EmptyMessage = "Select";
            // }

            //    //<%--------------2--------ExternalCorrosion------------------------%>

            // if (lbl_ExeCor_Date.Visible == true && cbo_ExeCor_inspecEffec.SelectedValue.ToString() != "")
            // {
            //     int intFlag1 = BusinessTier.InspectionPlan(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "ExternalCorrosion", cbo_ExeCor_inspecEffec.SelectedValue.ToString(), Convert.ToDateTime(dt_ExeCor_InsDt.SelectedDate.ToString().Trim()), "U");
            //     lblStatus.Text = "Successfully Value Inserted";
            //     cbo_ExeCor_inspecEffec.EmptyMessage = "Select";
            // }

            // //   <%--------------3--------CUI Damage------------------------%>

            // if (lbl_CUIDamage_Date.Visible == true && cbo_CUIDamage_inspecEffec.SelectedValue.ToString() != "")
            //  {
            //     int intFlag1 = BusinessTier.InspectionPlan(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "CUIDamage", cbo_CUIDamage_inspecEffec.SelectedValue.ToString(), Convert.ToDateTime(dt_CUIDamage_InsDt.SelectedDate.ToString().Trim()), "U");
            //     lblStatus.Text = "Successfully Value Inserted";
            //     cbo_CUIDamage_inspecEffec.EmptyMessage = "Select";
            // }

            // // <%-------------4---------External CLSCC------------------------%>

            // if (lbl_ExtCLSCC_Date.Visible == true && cbo_ExtCLSCC_inspecEffec.SelectedValue.ToString() != "")
            // {
            //     int intFlag1 = BusinessTier.InspectionPlan(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "ExternalCLSCC", cbo_ExtCLSCC_inspecEffec.SelectedValue.ToString(), Convert.ToDateTime(dt_ExtCLSCC_InsDt.SelectedDate.ToString().Trim()), "U");
            //     lblStatus.Text = "Successfully Value Inserted";
            //     cbo_ExtCLSCC_inspecEffec.EmptyMessage = "Select";
            // }
            //// <%--------------5--------External CUI CLSCC------------------------%>

            // if (lbl_ExtCUI_Date.Visible == true && cbo_ExtCUI_inspecEffec.SelectedValue.ToString() != "")
            // {
            //     int intFlag1 = BusinessTier.InspectionPlan(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "External CUI CLSCC", cbo_ExtCUI_inspecEffec.SelectedValue.ToString(), Convert.ToDateTime(dt_ExtCUI_InsDt.SelectedDate.ToString().Trim()), "U");
            //     lblStatus.Text = "Successfully Value Inserted";
            //     cbo_ExtCUI_inspecEffec.EmptyMessage = "Select";
            // }

            // //  <%------------6----------Caustic------------------------%>

            // if (lbl_Caustic_Date.Visible == true && cbo_Caustic_inspecEffec.SelectedValue.ToString() != "")
            // {
            //     int intFlag1 = BusinessTier.InspectionPlan(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "Caustic", cbo_Caustic_inspecEffec.SelectedValue.ToString(), Convert.ToDateTime(dt_Caustic_InsDt.SelectedDate.ToString().Trim()), "U");
            //     lblStatus.Text = "Successfully Value Inserted";
            //     cbo_Caustic_inspecEffec.EmptyMessage = "Select";
            // }

            // //  <%--------------7--------Amine------------------------%>

            // if (lbl_Amine_Date.Visible == true && cbo_Amine_inspecEffec.SelectedValue.ToString() != "")
            // {
            //     int intFlag1 = BusinessTier.InspectionPlan(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "Amine", cbo_Amine_inspecEffec.SelectedValue.ToString(), Convert.ToDateTime(dt_Amine_InsDt.SelectedDate.ToString().Trim()), "U");
            //     lblStatus.Text = "Successfully Value Inserted";
            //     cbo_Amine_inspecEffec.EmptyMessage = "Select";
            // }

            // //  <%--------------8--------Sulfide------------------------%>

            // if (lbl_Sulfide_Date.Visible == true && cbo_Sulfide_inspecEffec.SelectedValue.ToString() != "")
            // {
            //     int intFlag1 = BusinessTier.InspectionPlan(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "Sulfide", cbo_Sulfide_inspecEffec.SelectedValue.ToString(), Convert.ToDateTime(dt_Sulfide_InsDt.SelectedDate.ToString().Trim()), "U");
            //     lblStatus.Text = "Successfully Value Inserted";
            //     cbo_Sulfide_inspecEffec.EmptyMessage = "Select";
            // }

            // // <%-------------9---------H2S------------------------%>

            // if (lbl_H2S_Date.Visible == true && cbo_H2S_inspecEffec.SelectedValue.ToString() != "")
            // {
            //     int intFlag1 = BusinessTier.InspectionPlan(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "H2S", cbo_H2S_inspecEffec.SelectedValue.ToString(), Convert.ToDateTime(dt_H2S_InsDt.SelectedDate.ToString().Trim()), "U");
            //     lblStatus.Text = "Successfully Value Inserted";
            //     cbo_H2S_inspecEffec.Text = String.Empty;
            //     cbo_H2S_inspecEffec.EmptyMessage = "Select";
            // }

            //  // <%------------10----------Carbonate------------------------%>

            // if (lbl_Carbonate_Date.Visible == true && cbo_Carbonate_inspecEffec.SelectedValue.ToString() != "")
            // {
            //     int intFlag1 = BusinessTier.InspectionPlan(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "Carbonate", cbo_Carbonate_inspecEffec.SelectedValue.ToString(), Convert.ToDateTime(dt_Carbonate_InsDt.SelectedDate.ToString().Trim()), "U");
            //     lblStatus.Text = "Successfully Value Inserted";
            //     cbo_Carbonate_inspecEffec.EmptyMessage = "Select";
            // }

            //   // <%----------11------------PTA------------------------%>

            // if (lbl_PTA_Date.Visible == true && cbo_PTA_inspecEffec.SelectedValue.ToString() != "")
            // {
            //     int intFlag1 = BusinessTier.InspectionPlan(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "PTA", cbo_PTA_inspecEffec.SelectedValue.ToString(), Convert.ToDateTime(dt_PTA_InsDt.SelectedDate.ToString().Trim()), "U");
            //     lblStatus.Text = "Successfully Value Inserted";
            //     cbo_PTA_inspecEffec.EmptyMessage = "Select";
            // }

            // //  <%--------------12--------CLSCC------------------------%>

            // if (lbl_CLSCC_Date.Visible == true && cbo_CLSCC_inspecEffec.SelectedValue.ToString() != "")
            // {
            //     int intFlag1 = BusinessTier.InspectionPlan(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "CLSCC", cbo_CLSCC_inspecEffec.SelectedValue.ToString(), Convert.ToDateTime(dt_CLSCC_InsDt.SelectedDate.ToString().Trim()), "U");
            //     lblStatus.Text = "Successfully Value Inserted";
            //     cbo_CLSCC_inspecEffec.EmptyMessage = "Select";
            // }

            // // <%----------13------------HSC-HF------------------------%>

            // if (lbl_HSCHF_Date.Visible == true && cbo_HSCHF_inspecEffec.SelectedValue.ToString() != "")
            // {
            //     int intFlag1 = BusinessTier.InspectionPlan(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "HSC-HF", cbo_HSCHF_inspecEffec.SelectedValue.ToString(), Convert.ToDateTime(dt_HSCHF_InsDt.SelectedDate.ToString().Trim()), "U");
            //     lblStatus.Text = "Successfully Value Inserted";
            // }

            // // <%------------14----------HIC/SOHIC-HF------------------------%>

            // if (lbl_SOHIC_Date.Visible == true && cbo_SOHIC_inspecEffec.SelectedValue.ToString() != "")
            // {
            //     int intFlag1 = BusinessTier.InspectionPlan(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "HIC/SOHIC-HF", cbo_SOHIC_inspecEffec.SelectedValue.ToString(), Convert.ToDateTime(dt_SOHIC_InsDt.SelectedDate.ToString().Trim()), "U");
            //     lblStatus.Text = "Successfully Value Updated";
            //     cbo_SOHIC_inspecEffec.EmptyMessage = "Select";
            // }

            // //  <%------------15----------HTHA------------------------%>

            // if (lbl_HTHA_Date.Visible == true && cbo_HTHA_inspecEffec.SelectedValue.ToString() != "")
            // {
            //     int intFlag1 = BusinessTier.InspectionPlan(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "HTHA", cbo_HTHA_inspecEffec.SelectedValue.ToString(), Convert.ToDateTime(dt_HTHA_InsDt.SelectedDate.ToString().Trim()), "U");
            //     lblStatus.Text = "Successfully Value Inserted";
            //    // dt_HTHA_InsDt.PreRender();
            //     cbo_HTHA_inspecEffec.EmptyMessage = "Select";
            // }

            // BusinessTier.DisposeConnection(conn);
            lblStatus.Text = "Successfully Value Inserted";
            lblStatus.ForeColor = Color.Blue;
        }
        catch (Exception ex)
        {
           // SqlConnection conn = BusinessTier.getConnection();
           // lblStatus.Text = "Err:" + ex.Message.ToString();
        }
    }

}