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
using System.IO;

using System.Globalization;
using System.Collections;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.OleDb;
using System.Drawing;
using Telerik.Web.UI.Calendar;

public partial class POF : System.Web.UI.Page
{
    public DataTable dtMenuItems = new DataTable();

    public DataTable dtSubMenuItems = new DataTable();

    protected void Page_Init(object sender, EventArgs e)
    {
        //------------Visible Tab Containers
        tab_SCCDF.Visible = false;
        tab_Lining.Visible = false;
        tab_Thinning.Visible = false;
        tab_ExternalDamage.Visible = false;
        tab_HTHA.Visible = false;
        tab_Brit_Fract.Visible = false;
        tab_Mech_Fati.Visible = false;
        tab_POF_Total.Visible = false;

        //------------Enabled Text Boxes
        cbo_ExCUI_InsCon.Enabled = false;
        cbo_ExCUI_ChlrFree.Enabled = false;
        txt_Temper_Fattval.Enabled = false;
        txt_885_Tref.Enabled = false;
        cboclad.Enabled = false;
        txt_art.Enabled = false;
        txt_Tdf.Enabled = false;
        txt_dfliner.Enabled = false;
        txt_ECD_AgeCoat.Enabled = false;
        txt_ECD_Age.Enabled = false;
        txt_ECD_cr.Enabled = false;
        txt_ECD_art.Enabled = false;
        txt_ECD_df.Enabled = false;
        txt_CUI_Agecoat.Enabled = false;
        txt_CUI_Age.Enabled = false;
        txt_CUI_cr.Enabled = false;
        txt_CUI_art.Enabled = false;
        txt_CUI_Df.Enabled = false;

        //Button//

        btn_ThinningSave.Enabled = false;
        btn_ECD_Save.Enabled = false;
        btn_LineSave.Enabled = false;

        btn_CUI_Save.Enabled = false;
        btn_ExCLS_Save.Enabled = false;
        btn_ExCUI_Save.Enabled = false;
        btn_Caustic_Save.Enabled = false;

        btn_Amn_Save.Enabled = false;
        btn_Sul_Save.Enabled = false;
        btn_HIC_Save.Enabled = false;
        btn_Crbnt_Save.Enabled = false;
        btn_PTA_Save.Enabled = false;
        btn_CLS_Save.Enabled = false;
        btn_HSC_Save.Enabled = false;
        btn_HF_Save.Enabled = false;

        btn_HTHA_Save.Enabled = false;
        btn_Mech_Save.Enabled = false;

        btn_Britt_Save.Enabled = false;
        btn_Temper_Save.Enabled = false;
        btn_885_Save.Enabled = false;
        btn_Sigma_Save.Enabled = false;

        btn_Final_Save.Enabled = false;

        cboclad.EmptyMessage = "Select";
        lbl_Desc.Visible = false;

        // Delete

        btnExCUIDelete.Enabled = false;
        btnExCLSSDelete.Enabled = false;
        btnCUIDelete.Enabled = false;
        btnECDDelete.Enabled = false;
        btnThiningDelete.Enabled = false;
        btnPOFTotalDelete.Enabled = false;
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
                //item.Text = row["EqupType"].ToString();
                item.Text = row["EqupID"].ToString();
                item.Value = row["EquAutoID"].ToString();
                string balqty = row["EquAutoID"].ToString();
                if (balqty != "")
                {
                    //item.Attributes.Add("EquAutoID", row["EquAutoID"].ToString());
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
                //item.Text = row["CompName"].ToString();
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
                    // item.Attributes.Add("compautoid", row["compautoid"].ToString());
                    // Lbldupdate.Text=(row["IncomingDate"].ToString());
                    cboComponent.Items.Add(item);
                }
                item.DataBind();
            }
            //lblStatus.Text = cboEquipment.SelectedValue.ToString();
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
        string strcladdvalue = cboComponent.SelectedItem.Attributes["Clad"].ToString();
        if (strcladdvalue == "Yes")
        {
            cboclad.Enabled = true;

            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            try
            {
                string sql1 = "SELECT [CompAutoID],[CompName] FROM [RBI].[dbo].[Tbl_EquipmentComponentDetails] where [Clad] = 'Clad' and deleted=0";
                SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
                DataTable dataTable1 = new DataTable();
                adapter1.Fill(dataTable1);
                cboclad.Items.Clear();
                foreach (DataRow row in dataTable1.Rows)
                {
                    RadComboBoxItem item = new RadComboBoxItem();
                    item.Text = row["CompName"].ToString();
                    item.Value = row["CompAutoID"].ToString();
                    cboclad.Items.Add(item);
                    item.DataBind();
                }

                BusinessTier.DisposeAdapter(adapter1);
                BusinessTier.DisposeConnection(conn);
            }
            catch (Exception ex)
            {
                BusinessTier.DisposeConnection(conn);
                //lblStatus.Text = ex.Message.ToString();
                return;
            }
        }
        else
        {
            //cboclad.SelectedItem.Text = string.Empty;
            cboclad.Text = "None";
            cboclad.Enabled = false;

        }

    }

    //<-------------------Button Clicks------------------------>
    //protected void Button2_Click(object sender, EventArgs e)
    //{
    //    SqlConnection conn = BusinessTier.getConnection();
    //    conn.Open();
    //    string sql1 = "Select  [ProcessareaID],Plantcode,[TagName] from [Tbl_EquipmentComponentDetails]  where CompAutoID  > 72 and  Deleted=0";
    //    SqlDataAdapter adp = new SqlDataAdapter(sql1, conn);
    //    DataTable dataTable1 = new DataTable();
    //    adp.Fill(dataTable1);
    //    int inspec = Convert.ToInt32(dataTable1.Rows.Count);
    //    //string ProcessareaID = dataTable1.Rows[0]["ProcessareaID"].ToString();
    //    //string TagName = dataTable1.Rows[0]["TagName"].ToString();
    //    int i = 0;
    //    foreach (DataRow row in dataTable1.Rows)
    //    {

    //        string ProcessareaID = dataTable1.Rows[i]["ProcessareaID"].ToString();
    //        string TagName = dataTable1.Rows[i]["TagName"].ToString();
    //        string Plantcode = dataTable1.Rows[i]["Plantcode"].ToString();
    //        string EquAutoID = "";
    //        string sql = "Select  [EquAutoID] from [Tbl_EquipmentAsset]  where EqupID='" + TagName.ToString().Trim() + "' and [ProcessAreaID] ='" + ProcessareaID.ToString().Trim() + "' and  Deleted=0";
    //        SqlCommand cmd = new SqlCommand(sql, conn);
    //        SqlDataReader rd = cmd.ExecuteReader();
    //        if (rd.Read())
    //        {
    //            EquAutoID = rd["EquAutoID"].ToString();
    //        }
    //        rd.Close();
    //        string sql7 = "update [RBI].[dbo].[Tbl_EquipmentComponentDetails] set [EqupID]='" + EquAutoID.ToString().Trim() + "' where [Plantcode]='" + Plantcode.ToString().Trim() + "'  and  [TagName]='" + TagName.ToString().Trim() + "'  and  Deleted=0";
    //        SqlCommand cmd7 = new SqlCommand(sql7, conn);
    //        cmd7.ExecuteNonQuery();
    //        i = i + 1;
    //    }
    //    BusinessTier.DisposeConnection(conn);
    //}

    protected void btn_External_Click(object sender, EventArgs e)
    {
        btn_Thinning.ForeColor = Color.Black;
        btn_External.ForeColor = Color.Red;
        btn_Lining.ForeColor = Color.Black;
        btn_SCC.ForeColor = Color.Black;
        btn_HTHA.ForeColor = Color.Black;
        btn_Mech_Fati.ForeColor = Color.Black;
        btn_Brit_Frac.ForeColor = Color.Black;
        btn_POF_Total.ForeColor = Color.Black;

        tab_SCCDF.Visible = false;
        tab_Lining.Visible = false;
        tab_Thinning.Visible = false;
        tab_HTHA.Visible = false;
        tab_Brit_Fract.Visible = false;
        tab_Mech_Fati.Visible = false;
        tab_ExternalDamage.Visible = true;
        tab_POF_Total.Visible = false;
    }

    protected void btn_Lining_Click(object sender, EventArgs e)
    {
        btn_Thinning.ForeColor = Color.Black;
        btn_External.ForeColor = Color.Black;
        btn_Lining.ForeColor = Color.Red;
        btn_SCC.ForeColor = Color.Black;
        btn_HTHA.ForeColor = Color.Black;
        btn_Mech_Fati.ForeColor = Color.Black;
        btn_Brit_Frac.ForeColor = Color.Black;
        btn_POF_Total.ForeColor = Color.Black;

        tab_SCCDF.Visible = false;
        tab_Thinning.Visible = false;
        tab_ExternalDamage.Visible = false;
        tab_HTHA.Visible = false;
        tab_Brit_Fract.Visible = false;
        tab_Mech_Fati.Visible = false;
        tab_Lining.Visible = true;
        tab_POF_Total.Visible = false;
    }

    protected void btn_SCC_Click(object sender, EventArgs e)
    {
        btn_Thinning.ForeColor = Color.Black;
        btn_External.ForeColor = Color.Black;
        btn_Lining.ForeColor = Color.Black;
        btn_SCC.ForeColor = Color.Red;
        btn_HTHA.ForeColor = Color.Black;
        btn_Mech_Fati.ForeColor = Color.Black;
        btn_Brit_Frac.ForeColor = Color.Black;
        btn_POF_Total.ForeColor = Color.Black;

        tab_Lining.Visible = false;
        tab_Thinning.Visible = false;
        tab_ExternalDamage.Visible = false;
        tab_HTHA.Visible = false;
        tab_Brit_Fract.Visible = false;
        tab_Mech_Fati.Visible = false;
        tab_SCCDF.Visible = true;
        tab_POF_Total.Visible = false;

    }

    protected void btn_Thinning_Click(object sender, EventArgs e)
    {
        btn_Thinning.ForeColor = Color.Red;
        btn_External.ForeColor = Color.Black;
        btn_Lining.ForeColor = Color.Black;
        btn_SCC.ForeColor = Color.Black;
        btn_HTHA.ForeColor = Color.Black;
        btn_Mech_Fati.ForeColor = Color.Black;
        btn_Brit_Frac.ForeColor = Color.Black;
        btn_POF_Total.ForeColor = Color.Black;

        tab_SCCDF.Visible = false;
        tab_Lining.Visible = false;
        tab_ExternalDamage.Visible = false;
        tab_HTHA.Visible = false;
        tab_Brit_Fract.Visible = false;
        tab_Mech_Fati.Visible = false;
        tab_Thinning.Visible = true;
        tab_POF_Total.Visible = false;
    }

    protected void btn_HTHA_Click(object sender, EventArgs e)
    {
        btn_Thinning.ForeColor = Color.Black;
        btn_External.ForeColor = Color.Black;
        btn_Lining.ForeColor = Color.Black;
        btn_SCC.ForeColor = Color.Black;
        btn_HTHA.ForeColor = Color.Red;
        btn_Mech_Fati.ForeColor = Color.Black;
        btn_Brit_Frac.ForeColor = Color.Black;
        btn_POF_Total.ForeColor = Color.Black;

        tab_SCCDF.Visible = false;
        tab_Lining.Visible = false;
        tab_ExternalDamage.Visible = false;
        tab_Thinning.Visible = false;
        tab_Brit_Fract.Visible = false;
        tab_Mech_Fati.Visible = false;
        tab_HTHA.Visible = true;
        tab_POF_Total.Visible = false;
    }

    protected void btn_Mech_Fati_Click(object sender, EventArgs e)
    {
        btn_Thinning.ForeColor = Color.Black;
        btn_External.ForeColor = Color.Black;
        btn_Lining.ForeColor = Color.Black;
        btn_SCC.ForeColor = Color.Black;
        btn_HTHA.ForeColor = Color.Black;
        btn_Mech_Fati.ForeColor = Color.Red;
        btn_Brit_Frac.ForeColor = Color.Black;
        btn_POF_Total.ForeColor = Color.Black;

        tab_SCCDF.Visible = false;
        tab_Lining.Visible = false;
        tab_ExternalDamage.Visible = false;
        tab_Thinning.Visible = false;
        tab_HTHA.Visible = false;
        tab_Brit_Fract.Visible = false;
        tab_Mech_Fati.Visible = true;
        tab_POF_Total.Visible = false;
    }

    protected void btn_Brit_Frac_Click(object sender, EventArgs e)
    {
        btn_Thinning.ForeColor = Color.Black;
        btn_External.ForeColor = Color.Black;
        btn_Lining.ForeColor = Color.Black;
        btn_SCC.ForeColor = Color.Black;
        btn_HTHA.ForeColor = Color.Black;
        btn_Mech_Fati.ForeColor = Color.Black;
        btn_Brit_Frac.ForeColor = Color.Red;
        btn_POF_Total.ForeColor = Color.Black;

        tab_SCCDF.Visible = false;
        tab_Lining.Visible = false;
        tab_ExternalDamage.Visible = false;
        tab_Thinning.Visible = false;
        tab_HTHA.Visible = false;
        tab_Mech_Fati.Visible = false;
        tab_Brit_Fract.Visible = true;
        tab_POF_Total.Visible = false;
    }

    protected void btn_POF_Total_Click(object sender, EventArgs e)
    {
        btn_Thinning.ForeColor = Color.Black;
        btn_External.ForeColor = Color.Black;
        btn_Lining.ForeColor = Color.Black;
        btn_SCC.ForeColor = Color.Black;
        btn_HTHA.ForeColor = Color.Black;
        btn_Mech_Fati.ForeColor = Color.Black;
        btn_Brit_Frac.ForeColor = Color.Black;
        btn_POF_Total.ForeColor = Color.Red;

        tab_SCCDF.Visible = false;
        tab_Lining.Visible = false;
        tab_ExternalDamage.Visible = false;
        tab_Thinning.Visible = false;
        tab_HTHA.Visible = false;
        tab_Mech_Fati.Visible = false;
        tab_Brit_Fract.Visible = false;
        tab_POF_Total.Visible = true;
    }

    //<-------------------Thinning Cracking------------------------>

    protected void Calculate_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        if (txt_Age.Text == "")
        {
            lblStatus.Text = "Please Enter Age Value";
            lblStatus.ForeColor = Color.Red;
        }
        else
        {
            try
            {
                string strqry = "SELECT [MRT],[ReadVal],[CorrosionAllownce],[Clad],[uCR] FROM [RBI].[dbo].[Tbl_EquipmentComponentDetails] where [ProcessareaID]='" + cboProcessArea.SelectedValue.ToString() + "' and EqupID='" + cboEquipment.SelectedValue.ToString() + "' and [CompAutoID]='" + cboComponent.SelectedValue.ToString() + "' and deleted=0";
                SqlCommand cmd = new SqlCommand(strqry, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                double trd = 0, t = 0, crcm = 0, CA = 0, tmin = 0, Art = 0, agerc = 0, age = 0;
                string clad = "";
                if (rd.Read())
                {
                    trd = Convert.ToDouble(rd["ReadVal"].ToString().Trim());
                    tmin = Convert.ToDouble(rd["MRT"].ToString().Trim());
                    crcm = Convert.ToDouble(rd["uCR"].ToString().Trim());
                    CA = Convert.ToDouble(rd["CorrosionAllownce"].ToString().Trim());
                    clad = rd["Clad"].ToString().Trim();
                }
                else
                {
                    return;
                }
                rd.Close();
                age = Convert.ToDouble(txt_Age.Text.ToString());
               
                if (clad == "Clad" || clad == "Yes")
                {
                    double cal1 = 0, cal2 = 0;
                    cal1 = (trd - t) / crcm;
                    agerc = Math.Max(cal1, 0);

                    cal2 = 1 - (trd - (crcm * agerc) - (crcm * (age - agerc)) / (tmin + CA));
                    //cal2 = 1 - ((trd - crcm) * (agerc - crcm) * (age - agerc) / (tmin + CA));
                    Art = Math.Max(cal2, 0);
                }
                else                   
                {
                    double cal = 0;
                    cal = 1 - (trd - (crcm * age) / (tmin + CA));
                    Art = Math.Max(cal, 0);
                }
                txt_art.Text = Math.Round(Art, 2).ToString();

                int noinspecval = Convert.ToInt32(cbo_Thin_nofIns.SelectedValue.ToString().Trim());
                string ins = cbo_inspecEffec.SelectedValue.ToString().Trim();
                int tdf = 0;
                double artval = Convert.ToDouble(txt_art.Text.ToString());
                double finalart = 0;
                if (artval >= 0.00 && artval < 0.04)
                {
                    tdf = Interpolition(ins, 0.02, 0.04, artval, noinspecval);
                }
                else if (artval >= 0.04 && artval < 0.06)
                {
                    tdf = Interpolition(ins, 0.04, 0.06, artval, noinspecval);
                }
                else if (artval >= 0.06 && artval < 0.08)
                {
                    tdf = Interpolition(ins, 0.06, 0.08, artval, noinspecval);
                }
                else if (artval >= 0.08 && artval < 0.10)
                {
                    tdf = Interpolition(ins, 0.08, 0.10, artval, noinspecval);
                }
                else if (artval >= 0.10 && artval < 0.12)
                {
                    tdf = Interpolition(ins, 0.10, 0.12, artval, noinspecval);
                }
                else if (artval >= 0.12 && artval < 0.14)
                {
                    tdf = Interpolition(ins, 0.12, 0.14, artval, noinspecval);
                }
                else if (artval >= 0.14 && artval < 0.16)
                {
                    tdf = Interpolition(ins, 0.14, 0.16, artval, noinspecval);
                }
                else if (artval >= 0.16 && artval < 0.18)
                {
                    tdf = Interpolition(ins, 0.16, 0.18, artval, noinspecval);
                }
                else if (artval >= 0.18 && artval < 0.20)
                {
                    tdf = Interpolition(ins, 0.18, 0.20, artval, noinspecval);
                }
                else if (artval >= 0.20 && artval < 0.25)
                {
                    tdf = Interpolition(ins, 0.20, 0.25, artval, noinspecval);
                }
                else if (artval >= 0.25 && artval < 0.30)
                {
                    tdf = Interpolition(ins, 0.25, 0.30, artval, noinspecval);
                }
                else if (artval >= 0.30 && artval < 0.35)
                {
                    tdf = Interpolition(ins, 0.30, 0.35, artval, noinspecval);
                }
                else if (artval >= 0.35 && artval < 0.40)
                {
                    tdf = Interpolition(ins, 0.35, 0.40, artval, noinspecval);
                }
                else if (artval >= 0.40 && artval < 0.45)
                {
                    tdf = Interpolition(ins, 0.40, 0.45, artval, noinspecval);
                }
                else if (artval >= 0.45 && artval < 0.50)
                {
                    tdf = Interpolition(ins, 0.45, 0.50, artval, noinspecval);
                }
                else if (artval >= 0.50 && artval < 0.55)
                {
                    tdf = Interpolition(ins, 0.50, 0.55, artval, noinspecval);
                }
                else if (artval >= 0.55 && artval < 0.60)
                {
                    tdf = Interpolition(ins, 0.55, 0.60, artval, noinspecval);
                }
                else if (artval >= 0.60 && artval <= 0.65)
                {
                    tdf = Interpolition(ins, 0.60, 0.65, artval, noinspecval);
                }
                //else if (artval >= 0.65)
                //{
                //    finalart = 0.65;
                //}
                else
                {

                    double min = 0;
                    string strqry2 = "select " + ins + " as inspect from Tbl_InspectionEffective where art= " + 0.65 + " and inspection=" + noinspecval + " ";
                    SqlCommand cmd1 = new SqlCommand(strqry2, conn);
                    SqlDataReader rdr1 = cmd1.ExecuteReader();
                    if (rdr1.Read())
                        min = Convert.ToDouble(rdr1["inspect"].ToString());
                    rdr1.Close();
                    tdf = Convert.ToInt32(min.ToString());
                }



                //string strqry2 = "select " + ins + " as inspect from Tbl_InspectionEffective where art= " + finalart + " and inspection=" + noinspecval + " ";
                //SqlCommand cmd1 = new SqlCommand(strqry2, conn);
                //SqlDataReader rdr1 = cmd1.ExecuteReader();
                //rdr1.Read();
                txt_Tdf.Text = tdf.ToString().Trim();
                BusinessTier.DisposeConnection(conn);

            }
            catch (Exception ex)
            {
                // lblStatus.Text = "Err:" + ex.Message.ToString();
                InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "Calculate_Click", ex.ToString(), "Audit");
            }
        }


    }

    public static int Interpolition(string ins, double minart, double maxart, double art, int noins)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();

        double min = 0, max = 0, Tdf = 0, cal = 0;
        string strqry2 = "select " + ins + " as inspect from Tbl_InspectionEffective where art= " + minart + " and inspection=" + noins + " ";
        SqlCommand cmd1 = new SqlCommand(strqry2, conn);
        SqlDataReader rdr1 = cmd1.ExecuteReader();
        if (rdr1.Read())
            min = Convert.ToDouble(rdr1["inspect"].ToString());
        rdr1.Close();

        string strqry3 = "select " + ins + " as inspect from Tbl_InspectionEffective where art= " + maxart + " and inspection=" + noins + " ";
        SqlCommand cmd3 = new SqlCommand(strqry3, conn);
        SqlDataReader rdr3 = cmd3.ExecuteReader();
        if (rdr3.Read())
            max = Convert.ToDouble(rdr3["inspect"].ToString());
        rdr3.Close();

        cal = (art - minart) * (max - min) / (maxart - minart);
        Tdf = cal + min;

        BusinessTier.DisposeConnection(conn);
        return Convert.ToInt32(Tdf);

    }

    protected void btn_ThinningSave_Click(object sender, EventArgs e)
    {

        lblStatus.Text = string.Empty;
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_art.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_Tdf.Text.ToString().Trim()))
        {
            lblStatus.Text = "Please check all the  fields";
            lblStatus.ForeColor = Color.Red;
        }
        else
        {
            try
            {
                string clad = string.Empty, damagefactor = string.Empty;
                if (cboclad.Enabled == true)
                {
                    if (cboclad.SelectedItem.Text == "")
                    {
                        lblStatus.Text = "Please Select Clad Component";
                    }
                    else
                    {
                        clad = cboclad.SelectedItem.Text;
                    }
                }
                else
                {
                    clad = "";
                }

                if (cbo_thin_type.Text == "General")
                {
                    damagefactor = "Thinning";
                }
                else
                {
                    damagefactor = "ThinningL";
                }

                if (btnThinningSubmit.ToolTip == "Save")
                {
                    InspectionPlan(Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), damagefactor.ToString(), cbo_inspecEffec.SelectedValue.ToString(), Convert.ToDateTime(cbo_Thin_InspDate.SelectedDate.ToString().Trim()));
                    int intFlag = BusinessTier.ThinningSave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), clad.ToString().Trim(), Convert.ToInt32(txt_Age.Text.ToString().Trim()), Convert.ToDecimal(txt_art.Text.ToString().Trim()), Convert.ToDecimal(txt_Tdf.Text.ToString().Trim()), cbo_inspecEffec.SelectedValue.ToString(), Convert.ToDateTime(cbo_Thin_InspDate.SelectedDate.ToString()), Convert.ToInt32(cbo_Thin_nofIns.Text.ToString()), cbo_thin_type.SelectedItem.Text.ToString(), Convert.ToInt32(Session["sesCompanyID"].ToString()), Convert.ToInt32(Session["sesUserID"].ToString()), "N", 0);
                    lblStatus.Text = "Successfully Thinning Value Inserted";
                    lblStatus.ForeColor = Color.Green;
                }
                else if (btnThinningSubmit.ToolTip == "Update")
                {
                    InspectionPlan(Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), damagefactor.ToString(), cbo_inspecEffec.SelectedValue.ToString(), Convert.ToDateTime(cbo_Thin_InspDate.SelectedDate.ToString().Trim()));
                    int intFlag = BusinessTier.ThinningSave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), clad.ToString().Trim(), Convert.ToInt32(txt_Age.Text.ToString().Trim()), Convert.ToDecimal(txt_art.Text.ToString().Trim()), Convert.ToDecimal(txt_Tdf.Text.ToString().Trim()), cbo_inspecEffec.SelectedValue.ToString(), Convert.ToDateTime(cbo_Thin_InspDate.SelectedDate.ToString()), Convert.ToInt32(cbo_Thin_nofIns.Text.ToString()), cbo_thin_type.SelectedItem.Text.ToString(), Convert.ToInt32(Session["sesCompanyID"].ToString()), Convert.ToInt32(Session["sesUserID"].ToString()), "U", Convert.ToInt32(btn_ThinningSave.ToolTip));
                    lblStatus.Text = "Successfully Thinning Value Updated";
                    lblStatus.ForeColor = Color.Green;
                }
                BusinessTier.DisposeConnection(conn);
                btn_ThinningSave.Enabled = false;
                ThinningClear();
            }
            catch (Exception ex)
            {
                // lblStatus.Text = "Err:" + ex.Message.ToString();
                InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn_ThinningSave_Click", ex.ToString(), "Audit");
            }
            finally
            {
                BusinessTier.DisposeConnection(conn);
            }
        }


    }

    protected void InspDate_OnSelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
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

        int maxdate = 0;
        maxdate = Convert.ToInt32(cbo_Thin_InspDate.SelectedDate.Value.Year);

        txt_Age.Text = Agecal(maxdate, cboProcessArea.SelectedValue.ToString(), cboEquipment.SelectedValue.ToString(), cboComponent.SelectedValue.ToString(), Session["sesCompanyID"].ToString()).ToString();

        //string str2 = "SELECT yearinstalled as mindate FROM [Tbl_EquipmentAsset] where deleted=0 and [ProcessareaID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquAutoID]='" + cboEquipment.SelectedValue.ToString() + "'";
        //SqlCommand cmd2 = new SqlCommand(str2, conn);
        //SqlDataReader rdr2 = cmd2.ExecuteReader();
        //int mindate = 0, caldate = 0;
        //if (rdr2.Read())
        //{
        //    mindate = Convert.ToInt32(rdr2["mindate"].ToString());
        //}
        //rdr2.Close();
        //caldate = maxdate - mindate;
        //txt_Age.Text = caldate.ToString();
        //BusinessTier.DisposeConnection(conn);
    }

    //<-------------------Lining Cracking------------------------>

    protected void OnSelectedIndexChanged_cbo_Typelining(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        lblStatus.Text = string.Empty;

        if (cbo_Typelining.SelectedValue == "Organic")
        {
            lbl_Lineage.Text = "Years in Service";
            cbo_Dfb.Items.Clear();
            RadComboBoxItem item = new RadComboBoxItem();
            RadComboBoxItem item1 = new RadComboBoxItem();
            RadComboBoxItem item2 = new RadComboBoxItem();
            item.Text = "Inspected More Than 6 Years Ago";
            item.Value = "agosix";
            item1.Text = "Inspected within Last 6 Years";
            item1.Value = "sixyear";
            item2.Text = "Inspected within Last 3 Years";
            item2.Value = "threeyear";
            cbo_Dfb.Items.Add(item);
            cbo_Dfb.Items.Add(item1);
            cbo_Dfb.Items.Add(item2);
            cbo_AgeLining.Items.Clear();
            for (int i = 1; i <= 25; i++)
            {

                RadComboBoxItem AgeLining = new RadComboBoxItem();
                AgeLining.Text = i.ToString();
                AgeLining.Value = i.ToString();
                cbo_AgeLining.Items.Add(AgeLining);
            }

        }
        else if (cbo_Typelining.SelectedValue == "Inorganic")
        {
            lbl_Lineage.Text = "Years Last Inspection";
            cbo_Dfb.Items.Clear();
            RadComboBoxItem item = new RadComboBoxItem();
            RadComboBoxItem item1 = new RadComboBoxItem();
            RadComboBoxItem item2 = new RadComboBoxItem();
            RadComboBoxItem item3 = new RadComboBoxItem();
            RadComboBoxItem item4 = new RadComboBoxItem();
            RadComboBoxItem item5 = new RadComboBoxItem();
            item.Text = "Strip Lined Alloy(Resistant)";
            item.Value = "Alloy";
            item1.Text = "Castable Refractory";
            item1.Value = "Castable";
            item2.Text = "Castable Refractory Severe Conditions";
            item2.Value = "Severe";
            item3.Text = "Glass Lined";
            item3.Value = "Glass";
            item4.Text = "Acid Brick";
            item4.Value = "Acid";
            item5.Text = "Fiberglass";
            item5.Value = "Fiber";
            cbo_Dfb.Items.Add(item);
            cbo_Dfb.Items.Add(item1);
            cbo_Dfb.Items.Add(item2);
            cbo_Dfb.Items.Add(item3);
            cbo_Dfb.Items.Add(item4);
            cbo_Dfb.Items.Add(item5);
            cbo_AgeLining.Items.Clear();
            for (int i = 1; i <= 25; i++)
            {

                RadComboBoxItem AgeLining = new RadComboBoxItem();
                AgeLining.Text = i.ToString();
                AgeLining.Value = i.ToString();
                cbo_AgeLining.Items.Add(AgeLining);
            }

        }


    }

    protected void OnSelectedIndexChanged_LiningCondition(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        lbl_Desc.Visible = true;
        if (cbo_liningcondition.SelectedItem.Text == "Poor")
        {
            lbl_LineConDescription.Text = "The lining has either had previous failures or exhibits conditions that may lead to failure in the near future. Repairs to previous failures are not successful or are of poor quality.";
        }
        else if (cbo_liningcondition.SelectedItem.Text == "Average")
        {
            lbl_LineConDescription.Text = "The lining is not showing signs of excessive attack by any damage mechanisms. local repairs may have been performed, but they are of good quality and have succesfully corrected the lining condition.";
        }
        else if (cbo_liningcondition.SelectedItem.Text == "Good")
        {
            lbl_LineConDescription.Text = "The lining is in 'like new' conditon with no signs of attack by any damage mechanisms. There has been no need for any repairs to the lining.";
        }

    }

    protected void btn_CalcLine_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_AgeLining.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Dfb.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_liningcondition.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_OnlineMonitoring.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Typelining.Text.ToString().Trim()))
        {
            lblStatus.Text = "Please check all the  fields";
            lblStatus.ForeColor = Color.Red;
        }
        else
        {
            try
            {
                lblStatus.Text = string.Empty;
                SqlConnection conn = BusinessTier.getConnection();
                conn.Open();
                // int intFlag = BusinessTier.LiningFormula(conn, cbo_Typelining.SelectedItem.Text.ToString().Trim(), Convert.ToInt32(cbo_AgeLining.SelectedValue.ToString().Trim()), cbo_Dfb.SelectedValue.ToString().Trim(), Convert.ToInt32(cbo_liningcondition.SelectedValue.ToString().Trim()), Convert.ToInt32(cbo_OnlineMonitoring.SelectedValue.ToString().Trim()));


                string strqryln = "select " + cbo_Dfb.SelectedValue.ToString().Trim() + " as df from " + cbo_Typelining.SelectedItem.Text.ToString().Trim() + "   where siyear=" + cbo_AgeLining.SelectedItem.Text.ToString().Trim() + "";
                SqlCommand cmd11 = new SqlCommand(strqryln, conn);
                SqlDataReader rdrln = cmd11.ExecuteReader();
                rdrln.Read();
                decimal df, Dfliner;

                df = Convert.ToDecimal(rdrln["df"].ToString().Trim());
                Dfliner = df * Convert.ToDecimal(cbo_liningcondition.SelectedValue.ToString().Trim()) * Convert.ToDecimal(cbo_OnlineMonitoring.SelectedValue.ToString().Trim());
                txt_dfliner.Text = Dfliner.ToString();
                rdrln.Close();
                BusinessTier.DisposeConnection(conn);

            }
            catch (Exception ex)
            {
                // lblStatus.Text = "Err:" + ex.Message.ToString();
                InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn_CalcLine_Click", ex.ToString(), "Audit");
            }
        }

    }

    protected void btn_LineSave_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_AgeLining.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Dfb.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_liningcondition.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_OnlineMonitoring.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Typelining.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_dfliner.Text.ToString().Trim()))
        {
            lblStatus.Text = "Please check all the  fields";
            lblStatus.ForeColor = Color.Red;
        }
        else
        {
            try
            {

                if (btnLiningSubmit.ToolTip == "Save")
                {
                    int intFlag = BusinessTier.LiningSave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), cbo_Typelining.SelectedItem.Text.ToString().Trim(), Convert.ToInt32(cbo_AgeLining.SelectedValue.ToString().Trim()), cbo_Dfb.SelectedItem.Text.ToString().Trim(), cbo_liningcondition.SelectedItem.Text.ToString().Trim(), Convert.ToInt32(cbo_liningcondition.SelectedValue.ToString().Trim()), cbo_OnlineMonitoring.SelectedItem.Text.ToString().Trim(), Convert.ToDecimal(cbo_OnlineMonitoring.SelectedValue.ToString().Trim()), Convert.ToDecimal(txt_dfliner.Text.ToString().Trim()), Convert.ToInt32(Session["sesCompanyID"].ToString()), Convert.ToInt32(Session["sesUserID"].ToString()), "N", 0);
                    lblStatus.Text = "Successfully Lining Value Inserted";
                    lblStatus.ForeColor = Color.Green;
                }
                else if (btnLiningSubmit.ToolTip == "Update")
                {
                    int intFlag = BusinessTier.LiningSave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), cbo_Typelining.SelectedItem.Text.ToString().Trim(), Convert.ToInt32(cbo_AgeLining.SelectedValue.ToString().Trim()), cbo_Dfb.SelectedItem.Text.ToString().Trim(), cbo_liningcondition.SelectedItem.Text.ToString().Trim(), Convert.ToInt32(cbo_liningcondition.SelectedValue.ToString().Trim()), cbo_OnlineMonitoring.SelectedItem.Text.ToString().Trim(), Convert.ToDecimal(cbo_OnlineMonitoring.SelectedValue.ToString().Trim()), Convert.ToDecimal(txt_dfliner.Text.ToString().Trim()), Convert.ToInt32(Session["sesCompanyID"].ToString()), Convert.ToInt32(Session["sesUserID"].ToString()), "U", Convert.ToInt32(btn_LineSave.ToolTip));
                    lblStatus.Text = "Successfully Lining Value Updated";
                    lblStatus.ForeColor = Color.Green;
                }
                BusinessTier.DisposeConnection(conn);
                btn_ThinningSave.Enabled = false;
                LiningClear();
            }
            catch (Exception ex)
            {
                InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn_LineSave_Click", ex.ToString(), "Audit");
                // lblStatus.Text = "Err:" + ex.Message.ToString();
            }
            finally
            {
                BusinessTier.DisposeConnection(conn);
            }
        }

    }

    //<-------------------Caustic Cracking------------------------>

    protected void btn_CausticCalc_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {

            if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Caustic_InsEff.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_CausticAge.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Caustic_Svi.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_noCausInspect.Text.ToString().Trim()))
            {
                lblStatus.Text = "Please check all the  fields";
                lblStatus.ForeColor = Color.Red;
            }
            else
            {
                string strqrycs = "select " + cbo_Caustic_InsEff.SelectedValue.ToString().Trim() + " as dfcs from Ref_SCC where Inspection=" + cbo_noCausInspect.SelectedValue.ToString().Trim() + " and Svi=" + cbo_Caustic_Svi.SelectedValue.ToString().Trim() + "";
                SqlCommand cmdcs = new SqlCommand(strqrycs, conn);
                SqlDataReader rdcs = cmdcs.ExecuteReader();
                rdcs.Read();
                double dfcs, dfb_cs;
                dfcs = Convert.ToInt32(rdcs["dfcs"].ToString().Trim());
                dfb_cs = dfcs * (Math.Pow(Convert.ToInt32(txt_CausticAge.Text.ToString().Trim()), 1.1));
                txt_Caustic_Df.Text = Math.Round(dfb_cs, 3).ToString();
                rdcs.Close();
                BusinessTier.DisposeConnection(conn);
            }

        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn_CausticCalc_Click", ex.ToString(), "Audit");
            // lblStatus.Text = "Err:" + ex.Message.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    protected void btn_Caustic_Save_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {

            if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Caustic_InsEff.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_CausticAge.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Caustic_Svi.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_noCausInspect.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_Caustic_Df.Text.ToString().Trim()))
            {
                lblStatus.Text = "Please check all the fields";
                lblStatus.ForeColor = Color.Red;
            }
            else
            {
                if (btnCausticSubmit.ToolTip == "Save")
                {
                    int intFlag = BusinessTier.CausticAmineSave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), Convert.ToInt32(txt_CausticAge.Text.ToString().Trim()), cbo_Caustic_InsEff.SelectedValue.ToString().Trim(), Convert.ToInt32(cbo_noCausInspect.SelectedItem.Text.ToString().Trim()), Convert.ToDateTime(dt_Caustic_InspectDate.SelectedDate.ToString()), cbo_Caustic_Svi.SelectedItem.Text.ToString().Trim(), Convert.ToInt32(cbo_Caustic_Svi.SelectedValue.ToString().Trim()), Convert.ToDouble(txt_Caustic_Df.Text.ToString().Trim()), Convert.ToInt32(Session["sesUserID"].ToString()), "N", 0);
                    InspectionPlan(Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "Caustic", cbo_CUI_InsEff.SelectedValue.ToString(), Convert.ToDateTime(dt_CUI_InspectDate.SelectedDate.ToString().Trim()));
                    lblStatus.Text = "Successfully SCCDF-Caustic Cracking Value Inserted";
                    lblStatus.ForeColor = Color.Green;
                }
                else if (btnCausticSubmit.ToolTip == "Update")
                {
                    int intFlag = BusinessTier.CausticAmineSave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), Convert.ToInt32(txt_CausticAge.Text.ToString().Trim()), cbo_Caustic_InsEff.SelectedValue.ToString().Trim(), Convert.ToInt32(cbo_noCausInspect.SelectedItem.Text.ToString().Trim()), Convert.ToDateTime(dt_Caustic_InspectDate.SelectedDate.ToString()), cbo_Caustic_Svi.SelectedItem.Text.ToString().Trim(), Convert.ToInt32(cbo_Caustic_Svi.SelectedValue.ToString().Trim()), Convert.ToDouble(txt_Caustic_Df.Text.ToString().Trim()), Convert.ToInt32(Session["sesUserID"].ToString()), "U", Convert.ToInt32(btn_Caustic_Save.ToolTip));
                    InspectionPlan(Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "Caustic", cbo_CUI_InsEff.SelectedValue.ToString(), Convert.ToDateTime(dt_CUI_InspectDate.SelectedDate.ToString().Trim()));
                    lblStatus.Text = "Successfully SCCDF-Caustic Cracking Value Updated";
                    lblStatus.ForeColor = Color.Green;
                }
                BusinessTier.DisposeConnection(conn);
                btn_Caustic_Save.Enabled = false;
                CausticClear();


            }
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn_CausticCalc_Click", ex.ToString(), "Audit");
            // lblStatus.Text = "Err:" + ex.Message.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }

    }

    //<-------------------Amine Cracking------------------------>

    protected void btn_Amn_Save_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {

            if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Amn_InsEff.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_Amn_Age.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Amn_Svi.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Amn_noIns.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_Amn_Df.Text.ToString().Trim()))
            {
                lblStatus.Text = "Please check all the  fields";
                lblStatus.ForeColor = Color.Red;
            }
            else
            {
                if (btnAmineSubmit.ToolTip == "Save")
                {
                    int intFlag = BusinessTier.CausticAmineSave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), Convert.ToInt32(txt_Amn_Age.Text.ToString().Trim()), cbo_Amn_InsEff.SelectedValue.ToString().Trim(), Convert.ToInt32(cbo_Amn_noIns.SelectedItem.Text.ToString().Trim()), Convert.ToDateTime(dt_Amn_InspectDate.SelectedDate.ToString()), cbo_Amn_Svi.SelectedItem.Text.ToString().Trim(), Convert.ToInt32(cbo_Amn_Svi.SelectedValue.ToString().Trim()), Convert.ToDouble(txt_Amn_Df.Text.ToString().Trim()), Convert.ToInt32(Session["sesUserID"].ToString()), "N1", 0);
                    InspectionPlan(Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "Amine", cbo_CUI_InsEff.SelectedValue.ToString(), Convert.ToDateTime(dt_CUI_InspectDate.SelectedDate.ToString().Trim()));
                    lblStatus.Text = "Successfully SCCDF-Amine Cracking Value Inserted";
                    lblStatus.ForeColor = Color.Green;
                }
                else if (btnAmineSubmit.ToolTip == "Update")
                {
                    int intFlag = BusinessTier.CausticAmineSave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), Convert.ToInt32(txt_Amn_Age.Text.ToString().Trim()), cbo_Amn_InsEff.SelectedValue.ToString().Trim(), Convert.ToInt32(cbo_Amn_noIns.SelectedItem.Text.ToString().Trim()), Convert.ToDateTime(dt_Amn_InspectDate.SelectedDate.ToString()), cbo_Amn_Svi.SelectedItem.Text.ToString().Trim(), Convert.ToInt32(cbo_Amn_Svi.SelectedValue.ToString().Trim()), Convert.ToDouble(txt_Amn_Df.Text.ToString().Trim()), Convert.ToInt32(Session["sesUserID"].ToString()), "U1", Convert.ToInt32(btn_Amn_Save.ToolTip));
                    InspectionPlan(Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "Amine", cbo_CUI_InsEff.SelectedValue.ToString(), Convert.ToDateTime(dt_CUI_InspectDate.SelectedDate.ToString().Trim()));
                    lblStatus.Text = "Successfully SCCDF-Amine Cracking Value Updated";
                    lblStatus.ForeColor = Color.Green;
                }
                BusinessTier.DisposeConnection(conn);
                btn_Amn_Save.Enabled = false;
                AmnClear();
            }
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn_LineSave_Click", ex.ToString(), "Audit");
            // lblStatus.Text = "Err:" + ex.Message.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }

    }

    protected void btn_Amn_Calc_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {

            if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Amn_InsEff.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_Amn_Age.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Amn_Svi.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Amn_noIns.Text.ToString().Trim()))
            {
                lblStatus.Text = "Please check all the  fields";
                lblStatus.ForeColor = Color.Red;
            }
            else
            {
                string strqryAmn = "select " + cbo_Amn_InsEff.SelectedValue.ToString().Trim() + " as dfAmn from Ref_SCC where Inspection=" + cbo_Amn_noIns.SelectedValue.ToString().Trim() + " and Svi=" + cbo_Amn_Svi.SelectedValue.ToString().Trim() + "";
                SqlCommand cmdAmn = new SqlCommand(strqryAmn, conn);
                SqlDataReader rdAmn = cmdAmn.ExecuteReader();
                rdAmn.Read();
                double dfAmn, dfb_Amn;
                dfAmn = Convert.ToInt32(rdAmn["dfAmn"].ToString().Trim());
                dfb_Amn = dfAmn * (Math.Pow(Convert.ToInt32(txt_Amn_Age.Text.ToString().Trim()), 1.1));
                txt_Amn_Df.Text = Math.Round(dfb_Amn, 3).ToString();
                rdAmn.Close();
                BusinessTier.DisposeConnection(conn);
            }
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn_Amn_Calc_Click", ex.ToString(), "Audit");
            // lblStatus.Text = "Err:" + ex.Message.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }


    }

    //<-------------------Sulfide Cracking------------------------>

    protected void OnSelectedIndexChanged_H2S(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            lblStatus.Text = string.Empty;

            string strqryH2S = "select " + cbo_sul_H2s.SelectedValue.ToString().Trim() + " as dfH2S from Ref_Envi_Sev where pHofWater=" + cbo_Sul_pH.SelectedValue.ToString().Trim() + "";
            SqlCommand cmdH2S = new SqlCommand(strqryH2S, conn);
            SqlDataReader rdH2S = cmdH2S.ExecuteReader();
            rdH2S.Read();
            txt_Sul_Env.Text = rdH2S["dfH2S"].ToString().Trim();
            //RadComboBoxItem item = new RadComboBoxItem();
            //item.Attributes.Add("dfH2S", hs2);
            //cbo_Sul_Svi.Items.Add(item);
            //cbo_Sul_Svi.SelectdItem.Attributes.Add(rdH2S["dfH2S"].ToString().Trim());
            rdH2S.Close();
            BusinessTier.DisposeConnection(conn);

        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "OnSelectedIndexChanged_EnviSul", ex.ToString(), "Audit");
            // lblStatus.Text = "Err:" + ex.Message.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    protected void OnSelectedIndexChanged_EnviSul(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        chk_known_Crack.Checked = false;
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {

            string sql1 = "select " + cbo_Sul_Brin.SelectedValue.ToString().Trim() + " as dfEnviSul from Ref_EnviSul where Heat='" + cbo_Sul_Heat.SelectedValue.ToString().Trim() + "' and Envi='" + txt_Sul_Env.Text.ToString().Trim() + "'";
            SqlCommand cmd = new SqlCommand(sql1, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            rd.Read();
            txt_Sul_Svi.Text = rd["dfEnviSul"].ToString().Trim();
            rd.Close();
            BusinessTier.DisposeConnection(conn);
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "OnSelectedIndexChanged_EnviSul", ex.ToString(), "Audit");
            // lblStatus.Text = "Err:" + ex.Message.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }

    }

    protected void chk_known_Crack_CheckedChanged(object sender, EventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            if (chk_known_Crack.Checked)
            {
                txt_Sul_Svi.Text = "High";
                txt_Sul_Df.Text = string.Empty;
            }
            else
            {


                string sql1 = "select " + cbo_Sul_Brin.SelectedValue.ToString().Trim() + " as dfEnviSul from Ref_EnviSul where Heat='" + cbo_Sul_Heat.SelectedValue.ToString().Trim() + "' and Envi='" + txt_Sul_Env.Text.ToString().Trim() + "'";
                SqlCommand cmd = new SqlCommand(sql1, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                rd.Read();
                txt_Sul_Svi.Text = rd["dfEnviSul"].ToString().Trim();
                txt_Sul_Df.Text = string.Empty;
                rd.Close();
                BusinessTier.DisposeConnection(conn);
            }
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "chk_known_Crack_CheckedChanged", ex.ToString(), "Audit");
            // lblStatus.Text = "Err:" + ex.Message.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }

    }

    protected void btn_Sul_Calc_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Sul_InsEff.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_Sul_Age.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_Sul_Svi.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Sul_nofIns.Text.ToString().Trim()))
            {
                lblStatus.Text = "Please check all the  fields";
                lblStatus.ForeColor = Color.Red;
            }
            else
            {


                int svival = 0;
                if (txt_Sul_Svi.Text.ToString().Trim() == "High")
                {
                    svival = 100;
                }
                else if (txt_Sul_Svi.Text.ToString().Trim() == "Medium")
                {
                    svival = 10;
                }
                else if (txt_Sul_Svi.Text.ToString().Trim() == "Low")
                {
                    svival = 1;
                }
                else if (txt_Sul_Svi.Text.ToString().Trim() == "None")
                {
                    svival = 1;
                }

                string strqrySul = "select " + cbo_Sul_InsEff.SelectedValue.ToString().Trim() + " as dfSul from Ref_SCC where Inspection=" + cbo_Sul_nofIns.SelectedValue.ToString().Trim() + " and Svi=" + svival + "";
                SqlCommand cmdSul = new SqlCommand(strqrySul, conn);
                SqlDataReader rdSul = cmdSul.ExecuteReader();
                rdSul.Read();
                double dfSul, dfb_Sul;
                dfSul = Convert.ToInt32(rdSul["dfSul"].ToString().Trim());
                dfb_Sul = dfSul * (Math.Pow(Convert.ToDouble(txt_Sul_Age.Text.ToString().Trim()), 1.1));
                txt_Sul_Df.Text = Math.Round(dfb_Sul, 3).ToString();
                rdSul.Close();
                BusinessTier.DisposeConnection(conn);
            }
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn_Sul_Calc_Click", ex.ToString(), "Audit");
            // lblStatus.Text = "Err:" + ex.Message.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }

    }

    protected void btn_Sul_Save_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Sul_InsEff.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_Sul_Age.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_Sul_Svi.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Sul_nofIns.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_Sul_Df.Text.ToString().Trim()))
            {
                lblStatus.Text = "Please check all the  fields";
                lblStatus.ForeColor = Color.Red;
            }
            else
            {
                int svival = 0;
                if (txt_Sul_Svi.Text.ToString().Trim() == "High")
                {
                    svival = 100;
                }
                else if (txt_Sul_Svi.Text.ToString().Trim() == "Medium")
                {
                    svival = 10;
                }
                else if (txt_Sul_Svi.Text.ToString().Trim() == "Low")
                {
                    svival = 1;
                }
                else if (txt_Sul_Svi.Text.ToString().Trim() == "None")
                {
                    svival = 1;
                }

                if (btnSulSubmit.ToolTip == "Save")
                {
                    int intFlag = BusinessTier.SulfideCrackingSave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), Convert.ToInt32(txt_Sul_Age.Text.ToString().Trim()), cbo_Sul_InsEff.SelectedValue.ToString().Trim(), Convert.ToInt32(cbo_Sul_nofIns.SelectedItem.Text.ToString().Trim()), Convert.ToDateTime(dt_Sul_InspectDate.SelectedDate.ToString()), txt_Sul_Svi.Text.ToString().Trim(), Convert.ToInt32(svival.ToString().Trim()), Convert.ToDouble(txt_Sul_Df.Text.ToString().Trim()), cbo_Sul_pH.SelectedItem.Text.ToString().Trim(), cbo_sul_H2s.SelectedItem.Text.ToString().Trim(), txt_Sul_Env.Text.ToString().Trim(), cbo_Sul_Heat.SelectedItem.Text.ToString().Trim(), cbo_Sul_Brin.SelectedItem.Text.ToString().Trim(), Convert.ToInt32(Session["sesUserID"].ToString()), "N", 0);
                    InspectionPlan(Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "Sulfide", cbo_CUI_InsEff.SelectedValue.ToString(), Convert.ToDateTime(dt_CUI_InspectDate.SelectedDate.ToString().Trim()));
                    lblStatus.Text = "Successfully SCCDF-Sulfide Stress Cracking Value Inserted";
                    lblStatus.ForeColor = Color.Green;
                }
                else if (btnSulSubmit.ToolTip == "Update")
                {
                    int intFlag = BusinessTier.SulfideCrackingSave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), Convert.ToInt32(txt_Sul_Age.Text.ToString().Trim()), cbo_Sul_InsEff.SelectedValue.ToString().Trim(), Convert.ToInt32(cbo_Sul_nofIns.SelectedItem.Text.ToString().Trim()), Convert.ToDateTime(dt_Sul_InspectDate.SelectedDate.ToString()), txt_Sul_Svi.Text.ToString().Trim(), Convert.ToInt32(svival.ToString().Trim()), Convert.ToDouble(txt_Sul_Df.Text.ToString().Trim()), cbo_Sul_pH.SelectedItem.Text.ToString().Trim(), cbo_sul_H2s.SelectedItem.Text.ToString().Trim(), txt_Sul_Env.Text.ToString().Trim(), cbo_Sul_Heat.SelectedItem.Text.ToString().Trim(), cbo_Sul_Brin.SelectedItem.Text.ToString().Trim(), Convert.ToInt32(Session["sesUserID"].ToString()), "U", Convert.ToInt32(btn_Sul_Save.ToolTip));
                    InspectionPlan(Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "Sulfide", cbo_CUI_InsEff.SelectedValue.ToString(), Convert.ToDateTime(dt_CUI_InspectDate.SelectedDate.ToString().Trim()));
                    lblStatus.Text = "Successfully SCCDF-Sulfide Stress Cracking Value Updated";
                    lblStatus.ForeColor = Color.Green;
                }
                BusinessTier.DisposeConnection(conn);
                btn_Sul_Save.Enabled = false;
                SulClear();
            }
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn_Sul_Save_Click", ex.ToString(), "Audit");
            // lblStatus.Text = "Err:" + ex.Message.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }


    }

    //<-------------------Hydrogen Induced------------------------>

    protected void OnSelectedIndexChanged_HIC(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            lblStatus.Text = string.Empty;


            string strqryH2S = "select " + cbo_HIC_StSulf.SelectedValue.ToString().Trim() + " as dfH2S from Ref_Envi_Sev where pHofWater=" + cbo_HIC_pH.SelectedValue.ToString().Trim() + "";
            SqlCommand cmdH2S = new SqlCommand(strqryH2S, conn);
            SqlDataReader rdH2S = cmdH2S.ExecuteReader();
            rdH2S.Read();
            txt_HIC_Env.Text = rdH2S["dfH2S"].ToString().Trim();
            rdH2S.Close();
            BusinessTier.DisposeConnection(conn);

        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "OnSelectedIndexChanged_HIC", ex.ToString(), "Audit");
            //   lblStatus.Text = "Err:" + ex.Message.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    protected void OnSelectedIndexChanged_SteelSul(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        chk_HIC_known.Checked = false;
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {

            string sql1 = "select " + cbo_HIC_heat.SelectedValue.ToString().Trim() + " as dfHIC from Ref_HIC where stlsul='" + cbo_HIC_Steel.SelectedValue.ToString().Trim() + "' and Envi='" + txt_HIC_Env.Text.ToString().Trim() + "'";
            SqlCommand cmd = new SqlCommand(sql1, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            rd.Read();
            txt_HIC_Svi.Text = rd["dfHIC"].ToString().Trim();
            rd.Close();
            BusinessTier.DisposeConnection(conn);
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "OnSelectedIndexChanged_SteelSul", ex.ToString(), "Audit");
            //  lblStatus.Text = "Err:" + ex.Message.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }

    }

    protected void chk_HIC_known_CheckedChanged(object sender, EventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            if (chk_HIC_known.Checked)
            {
                txt_HIC_Svi.Text = "High";
                txt_HIC_DfSOHIC.Text = string.Empty;
            }
            else
            {
                string sql1 = "select " + cbo_HIC_heat.SelectedValue.ToString().Trim() + " as dfHIC from Ref_HIC where stlsul='" + cbo_HIC_Steel.SelectedValue.ToString().Trim() + "' and Envi='" + txt_HIC_Env.Text.ToString().Trim() + "'";
                SqlCommand cmd = new SqlCommand(sql1, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                rd.Read();
                txt_HIC_Svi.Text = rd["dfHIC"].ToString().Trim();
                txt_HIC_DfSOHIC.Text = string.Empty;
                rd.Close();
                BusinessTier.DisposeConnection(conn);
            }
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "chk_HIC_known_CheckedChanged", ex.ToString(), "Audit");
            //  lblStatus.Text = "Err:" + ex.Message.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }

    }

    protected void btn_HIC_Calc_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_HIC_InsEff.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_HIC_Age.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_HIC_Svi.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_HIC_nofIns.Text.ToString().Trim()))
            {
                lblStatus.Text = "Please check all the  fields";
                lblStatus.ForeColor = Color.Red;
            }
            else
            {


                int svival = 0;
                if (txt_HIC_Svi.Text.ToString().Trim() == "High")
                {
                    svival = 100;
                }
                else if (txt_HIC_Svi.Text.ToString().Trim() == "Medium")
                {
                    svival = 10;
                }
                else if (txt_HIC_Svi.Text.ToString().Trim() == "Low")
                {
                    svival = 1;
                }
                else if (txt_HIC_Svi.Text.ToString().Trim() == "None")
                {
                    svival = 1;
                }
                string strqryHIC = "select " + cbo_HIC_InsEff.SelectedValue.ToString().Trim() + " as dfHIC from Ref_SCC where Inspection=" + cbo_HIC_nofIns.SelectedValue.ToString().Trim() + " and Svi=" + svival.ToString().Trim() + "";
                SqlCommand cmdHIC = new SqlCommand(strqryHIC, conn);
                SqlDataReader rdHIC = cmdHIC.ExecuteReader();
                rdHIC.Read();
                double dfHIC, dfb_SOHIC;
                dfHIC = Convert.ToInt32(rdHIC["dfHIC"].ToString().Trim());
                dfb_SOHIC = dfHIC * (Math.Pow(Convert.ToDouble(txt_HIC_Age.Text.ToString().Trim()), 1.1));
                txt_HIC_DfSOHIC.Text = Math.Round(dfb_SOHIC, 3).ToString();
                rdHIC.Close();
                BusinessTier.DisposeConnection(conn);
            }
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn_HIC_Calc_Click", ex.ToString(), "Audit");
            //  lblStatus.Text = "Err:" + ex.Message.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }

    }

    protected void btn_HIC_Save_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {

            if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_HIC_InsEff.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_HIC_Age.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_HIC_Svi.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_HIC_nofIns.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_HIC_DfSOHIC.Text.ToString().Trim()))
            {
                lblStatus.Text = "Please check all the  fields";
                lblStatus.ForeColor = Color.Red;
            }
            else
            {
                int svival = 0;
                if (txt_HIC_Svi.Text.ToString().Trim() == "High")
                {
                    svival = 100;
                }
                else if (txt_HIC_Svi.Text.ToString().Trim() == "Medium")
                {
                    svival = 10;
                }
                else if (txt_HIC_Svi.Text.ToString().Trim() == "Low")
                {
                    svival = 1;
                }
                else if (txt_HIC_Svi.Text.ToString().Trim() == "None")
                {
                    svival = 1;
                }

                if (btnHICSubmit.ToolTip == "Save")
                {
                    int intFlag = BusinessTier.SulfideCrackingSave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), Convert.ToInt32(txt_HIC_Age.Text.ToString().Trim()), cbo_HIC_InsEff.SelectedValue.ToString().Trim(), Convert.ToInt32(cbo_HIC_nofIns.SelectedItem.Text.ToString().Trim()), Convert.ToDateTime(dt_HIC_InspectDate.SelectedDate.ToString()), txt_HIC_Svi.Text.ToString().Trim(), Convert.ToInt32(svival.ToString().Trim()), Convert.ToDouble(txt_HIC_DfSOHIC.Text.ToString().Trim()), cbo_HIC_pH.SelectedItem.Text.ToString().Trim(), cbo_HIC_StSulf.SelectedItem.Text.ToString().Trim(), txt_HIC_Env.Text.ToString().Trim(), cbo_HIC_heat.SelectedItem.Text.ToString().Trim(), cbo_HIC_Steel.Text.ToString().Trim(), Convert.ToInt32(Session["sesUserID"].ToString()), "N1", 0);
                    InspectionPlan(Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "H2S", cbo_CUI_InsEff.SelectedValue.ToString(), Convert.ToDateTime(dt_CUI_InspectDate.SelectedDate.ToString().Trim()));
                    lblStatus.Text = "Successfully SCCDF-HIC/SOHIC-H2S Cracking Value Inserted";
                    lblStatus.ForeColor = Color.Green;
                }
                else if (btnHICSubmit.ToolTip == "Update")
                {
                    int intFlag = BusinessTier.SulfideCrackingSave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), Convert.ToInt32(txt_HIC_Age.Text.ToString().Trim()), cbo_HIC_InsEff.SelectedValue.ToString().Trim(), Convert.ToInt32(cbo_HIC_nofIns.SelectedItem.Text.ToString().Trim()), Convert.ToDateTime(dt_HIC_InspectDate.SelectedDate.ToString()), txt_HIC_Svi.Text.ToString().Trim(), Convert.ToInt32(svival.ToString().Trim()), Convert.ToDouble(txt_HIC_DfSOHIC.Text.ToString().Trim()), cbo_HIC_pH.SelectedItem.Text.ToString().Trim(), cbo_HIC_StSulf.SelectedItem.Text.ToString().Trim(), txt_HIC_Env.Text.ToString().Trim(), cbo_HIC_heat.SelectedItem.Text.ToString().Trim(), cbo_HIC_Steel.Text.ToString().Trim(), Convert.ToInt32(Session["sesUserID"].ToString()), "U1", Convert.ToInt32(btn_HIC_Save.ToolTip));
                    InspectionPlan(Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "H2S", cbo_CUI_InsEff.SelectedValue.ToString(), Convert.ToDateTime(dt_CUI_InspectDate.SelectedDate.ToString().Trim()));
                    lblStatus.Text = "Successfully SCCDF-HIC/SOHIC-H2S Cracking Value Updated";
                    lblStatus.ForeColor = Color.Green;
                }
                BusinessTier.DisposeConnection(conn);
                btn_HIC_Save.Enabled = false;
                HICClear();
            }
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn_HIC_Calc_Click", ex.ToString(), "Audit");
            // lblStatus.Text = "Err:" + ex.Message.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }

    }

    //<-------------------Corbanate------------------------>

    protected void OnSelectedIndexChanged_CO3(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        lblStatus.Text = string.Empty;
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();

        try
        {
            chk_Crbnt_known.Checked = false;

            string strqryH2S = "select " + cbo_Crbnt_CO3.SelectedValue.ToString().Trim() + " as dfH2S from Ref_CO3 where pHofWater=" + cbo_Crbnt_pH.SelectedValue.ToString().Trim() + "";
            SqlCommand cmdH2S = new SqlCommand(strqryH2S, conn);
            SqlDataReader rdH2S = cmdH2S.ExecuteReader();
            rdH2S.Read();

            txt_Crbnt_Svi.Text = rdH2S["dfH2S"].ToString().Trim();
            rdH2S.Close();
            BusinessTier.DisposeConnection(conn);
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "OnSelectedIndexChanged_CO3", ex.ToString(), "Audit");
            // lblStatus.Text = "Err:" + ex.Message.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    protected void chk_Crbnt_known_CheckedChanged(object sender, EventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {

            if (chk_Crbnt_known.Checked)
            {
                txt_Crbnt_Svi.Text = "High";
                txt_Crbnt_Df.Text = string.Empty;
            }
            else
            {

                string sql1 = "select " + cbo_Crbnt_CO3.SelectedValue.ToString().Trim() + " as dfH2S from Ref_CO3 where pHofWater=" + cbo_Crbnt_pH.SelectedValue.ToString().Trim() + "";
                SqlCommand cmd = new SqlCommand(sql1, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                rd.Read();
                txt_Crbnt_Svi.Text = rd["dfH2S"].ToString().Trim();
                txt_Crbnt_Df.Text = string.Empty;
                rd.Close();
                BusinessTier.DisposeConnection(conn);
            }
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "chk_Crbnt_known_CheckedChanged", ex.ToString(), "Audit");
            // lblStatus.Text = "Err:" + ex.Message.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }

    }

    protected void btn_Crbnt_Calc_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {

            if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Crbnt_InsEff.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_Crbnt_Age.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_Crbnt_Svi.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Crbnt_nofIns.Text.ToString().Trim()))
            {
                lblStatus.Text = "Please check all the  fields";
                lblStatus.ForeColor = Color.Red;
            }
            else
            {

                int svival = 0;
                if (txt_Crbnt_Svi.Text.ToString().Trim() == "High")
                {
                    svival = 1000;
                }
                else if (txt_Crbnt_Svi.Text.ToString().Trim() == "Medium")
                {
                    svival = 100;
                }
                else if (txt_Crbnt_Svi.Text.ToString().Trim() == "Low")
                {
                    svival = 10;
                }
                else if (txt_Crbnt_Svi.Text.ToString().Trim() == "None")
                {
                    svival = 1;
                }
                string strqry = "select " + cbo_Crbnt_InsEff.SelectedValue.ToString().Trim() + " as dfcrbnt from Ref_SCC where Inspection=" + cbo_Crbnt_nofIns.SelectedValue.ToString().Trim() + " and Svi=" + svival.ToString().Trim() + "";
                SqlCommand cmd = new SqlCommand(strqry, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                rd.Read();
                double df, dfb_Crbnt;
                df = Convert.ToInt32(rd["dfcrbnt"].ToString().Trim());
                dfb_Crbnt = df * (Math.Pow(Convert.ToDouble(txt_Crbnt_Age.Text.ToString().Trim()), 1.1));
                txt_Crbnt_Df.Text = Math.Round(dfb_Crbnt, 3).ToString();
                rd.Close();
                BusinessTier.DisposeConnection(conn);
            }
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn_Crbnt_Calc_Click", ex.ToString(), "Audit");
            //lblStatus.Text = "Err:" + ex.Message.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }


    }

    protected void btn_Crbnt_Save_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Crbnt_InsEff.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_Crbnt_Age.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_Crbnt_Svi.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Crbnt_nofIns.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_Crbnt_Df.Text.ToString().Trim()))
            {
                lblStatus.Text = "Please check all the  fields";
                lblStatus.ForeColor = Color.Red;
            }
            else
            {

                int svival = 0;
                if (txt_Crbnt_Svi.Text.ToString().Trim() == "High")
                {
                    svival = 1000;
                }
                else if (txt_Crbnt_Svi.Text.ToString().Trim() == "Medium")
                {
                    svival = 100;
                }
                else if (txt_Crbnt_Svi.Text.ToString().Trim() == "Low")
                {
                    svival = 10;
                }
                else if (txt_Crbnt_Svi.Text.ToString().Trim() == "None")
                {
                    svival = 1;
                }

                if (btnCrbntSubmit.ToolTip == "Save")
                {
                    int intFlag = BusinessTier.SulfideSave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), Convert.ToInt32(txt_Crbnt_Age.Text.ToString().Trim()), cbo_Crbnt_InsEff.SelectedValue.ToString().Trim(), Convert.ToInt32(cbo_Crbnt_nofIns.SelectedItem.Text.ToString().Trim()), Convert.ToDateTime(dt_Crbnt_InspectDate.SelectedDate.ToString()), txt_Crbnt_Svi.Text.ToString().Trim(), Convert.ToInt32(svival.ToString().Trim()), Convert.ToDouble(txt_Crbnt_Df.Text.ToString().Trim()), cbo_Crbnt_pH.SelectedItem.Text.ToString().Trim(), cbo_Crbnt_CO3.SelectedItem.Text.ToString().Trim(), Convert.ToInt32(Session["sesUserID"].ToString()), "N", 0);
                    InspectionPlan(Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "Carbonate", cbo_CUI_InsEff.SelectedValue.ToString(), Convert.ToDateTime(dt_CUI_InspectDate.SelectedDate.ToString().Trim()));
                    lblStatus.Text = "Successfully SCCDF Carbonate Cracking Value Inserted";
                    lblStatus.ForeColor = Color.Green;
                }
                else if (btnCrbntSubmit.ToolTip == "Update")
                {
                    int intFlag = BusinessTier.SulfideSave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), Convert.ToInt32(txt_Crbnt_Age.Text.ToString().Trim()), cbo_Crbnt_InsEff.SelectedValue.ToString().Trim(), Convert.ToInt32(cbo_Crbnt_nofIns.SelectedItem.Text.ToString().Trim()), Convert.ToDateTime(dt_Crbnt_InspectDate.SelectedDate.ToString()), txt_Crbnt_Svi.Text.ToString().Trim(), Convert.ToInt32(svival.ToString().Trim()), Convert.ToDouble(txt_Crbnt_Df.Text.ToString().Trim()), cbo_Crbnt_pH.SelectedItem.Text.ToString().Trim(), cbo_Crbnt_CO3.SelectedItem.Text.ToString().Trim(), Convert.ToInt32(Session["sesUserID"].ToString()), "U", Convert.ToInt32(btn_Crbnt_Save.ToolTip));
                    InspectionPlan(Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "Carbonate", cbo_CUI_InsEff.SelectedValue.ToString(), Convert.ToDateTime(dt_CUI_InspectDate.SelectedDate.ToString().Trim()));
                    lblStatus.Text = "Successfully SCCDF Carbonate Cracking Value Updated";
                    lblStatus.ForeColor = Color.Green;
                }
                BusinessTier.DisposeConnection(conn);
                btn_Crbnt_Save.Enabled = false;
                CrbntClear();
            }
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn_Crbnt_Save_Click", ex.ToString(), "Audit");
            // lblStatus.Text = "Err:" + ex.Message.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }

    }

    //<-------------------PTA------------------------>

    protected void OnSelectedIndexChanged_PTA(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        lblStatus.Text = string.Empty;
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()))
            {
                lblStatus.Text = "Please Select Component";
            }
            else
            {

                chk_PTA_known.Checked = false;
                int optemp = Convert.ToInt32(cboComponent.SelectedItem.Attributes["OPTemp"].ToString());
                string GLT = string.Empty;
                if (optemp < 427)
                {
                    GLT = "LT";
                }
                else if (optemp >= 427)
                {
                    GLT = "GT";
                }

                string strqryH2S = "select " + cbo_PTA_Heat.SelectedValue.ToString().Trim() + " as dfPTA from Ref_PTA where Material='" + cbo_PTA_Material.SelectedValue.ToString().Trim() + "' and temp = '" + GLT + "'";
                SqlCommand cmdH2S = new SqlCommand(strqryH2S, conn);
                SqlDataReader rdH2S = cmdH2S.ExecuteReader();
                rdH2S.Read();
                txt_PTA_Svi.Text = rdH2S["dfPTA"].ToString().Trim();
                rdH2S.Close();
                BusinessTier.DisposeConnection(conn);
            }
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "OnSelectedIndexChanged_PTA", ex.ToString(), "Audit");
            // lblStatus.Text = "Err:" + ex.Message.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }

    }

    protected void chk_PTA_known_CheckedChanged(object sender, EventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            if (chk_PTA_known.Checked)
            {
                txt_PTA_Svi.Text = "High";
                txt_PTA_Df.Text = string.Empty;
            }
            else
            {

                int optemp = Convert.ToInt32(cboComponent.SelectedItem.Attributes["OPTemp"].ToString());
                string GLT = string.Empty;
                if (optemp < 427)
                {
                    GLT = "LT";
                }
                else if (optemp >= 427)
                {
                    GLT = "GT";
                }

                string sql1 = "select " + cbo_PTA_Heat.SelectedValue.ToString().Trim() + " as dfPTA from Ref_PTA where Material='" + cbo_PTA_Material.SelectedValue.ToString().Trim() + "' and temp = '" + GLT + "'";
                SqlCommand cmd = new SqlCommand(sql1, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                rd.Read();
                txt_PTA_Svi.Text = rd["dfPTA"].ToString().Trim();
                txt_PTA_Df.Text = string.Empty;
                rd.Close();
                BusinessTier.DisposeConnection(conn);
            }
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "chk_PTA_known_CheckedChanged", ex.ToString(), "Audit");
            // lblStatus.Text = "Err:" + ex.Message.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }


    }

    protected void btn_PTA_Calc_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {

            if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_PTA_InsEff.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_PTA_Age.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_PTA_Svi.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_PTA_nofIns.Text.ToString().Trim()))
            {
                lblStatus.Text = "Please check all the  fields";
                lblStatus.ForeColor = Color.Red;
            }
            else
            {

                int svival = 0;
                if (txt_PTA_Svi.Text.ToString().Trim() == "High")
                {
                    svival = 5000;
                }
                else if (txt_PTA_Svi.Text.ToString().Trim() == "Medium")
                {
                    svival = 500;
                }
                else if (txt_PTA_Svi.Text.ToString().Trim() == "Low")
                {
                    svival = 50;
                }
                else if (txt_PTA_Svi.Text.ToString().Trim() == "None")
                {
                    svival = 1;
                }
                string strqry = "select " + cbo_PTA_InsEff.SelectedValue.ToString().Trim() + " as dfPTA from Ref_SCC where Inspection=" + cbo_PTA_nofIns.SelectedValue.ToString().Trim() + " and Svi=" + svival.ToString().Trim() + "";
                SqlCommand cmd = new SqlCommand(strqry, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                rd.Read();
                double df, dfb_PTA;
                df = Convert.ToInt32(rd["dfPTA"].ToString().Trim());
                dfb_PTA = df * (Math.Pow(Convert.ToDouble(txt_PTA_Age.Text.ToString().Trim()), 1.1));
                txt_PTA_Df.Text = Math.Round(dfb_PTA, 3).ToString();
                rd.Close();
                BusinessTier.DisposeConnection(conn);
            }
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn_PTA_Calc_Click", ex.ToString(), "Audit");
            // lblStatus.Text = "Err:" + ex.Message.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }

    }

    protected void btn_PTA_Save_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_PTA_InsEff.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_PTA_Age.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_PTA_Svi.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_PTA_nofIns.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_PTA_Df.Text.ToString().Trim()))
            {
                lblStatus.Text = "Please check all the  fields";
                lblStatus.ForeColor = Color.Red;
            }
            else
            {

                int svival = 0;
                if (txt_PTA_Svi.Text.ToString().Trim() == "High")
                {
                    svival = 5000;
                }
                else if (txt_PTA_Svi.Text.ToString().Trim() == "Medium")
                {
                    svival = 500;
                }
                else if (txt_PTA_Svi.Text.ToString().Trim() == "Low")
                {
                    svival = 50;
                }
                else if (txt_PTA_Svi.Text.ToString().Trim() == "None")
                {
                    svival = 1;
                }

                if (btnPTASubmit.ToolTip == "Save")
                {
                    int intFlag = BusinessTier.SulfideSave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), Convert.ToInt32(txt_PTA_Age.Text.ToString().Trim()), cbo_PTA_InsEff.SelectedValue.ToString().Trim(), Convert.ToInt32(cbo_PTA_nofIns.SelectedItem.Text.ToString().Trim()), Convert.ToDateTime(dt_PTA_InspectDate.SelectedDate.ToString()), txt_PTA_Svi.Text.ToString().Trim(), Convert.ToInt32(svival.ToString().Trim()), Convert.ToDouble(txt_PTA_Df.Text.ToString().Trim()), cbo_PTA_Material.SelectedItem.Text.ToString().Trim(), cbo_PTA_Heat.SelectedItem.Text.ToString().Trim(), Convert.ToInt32(Session["sesUserID"].ToString()), "N3", 0);
                    InspectionPlan(Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "PTA", cbo_CUI_InsEff.SelectedValue.ToString(), Convert.ToDateTime(dt_CUI_InspectDate.SelectedDate.ToString().Trim()));
                    lblStatus.Text = "Successfully SCCDF-PTA Cracking Value Inserted";
                    lblStatus.ForeColor = Color.Green;
                }
                else if (btnPTASubmit.ToolTip == "Update")
                {
                    int intFlag = BusinessTier.SulfideSave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), Convert.ToInt32(txt_PTA_Age.Text.ToString().Trim()), cbo_PTA_InsEff.SelectedValue.ToString().Trim(), Convert.ToInt32(cbo_PTA_nofIns.SelectedItem.Text.ToString().Trim()), Convert.ToDateTime(dt_PTA_InspectDate.SelectedDate.ToString()), txt_PTA_Svi.Text.ToString().Trim(), Convert.ToInt32(svival.ToString().Trim()), Convert.ToDouble(txt_PTA_Df.Text.ToString().Trim()), cbo_PTA_Material.SelectedItem.Text.ToString().Trim(), cbo_PTA_Heat.SelectedItem.Text.ToString().Trim(), Convert.ToInt32(Session["sesUserID"].ToString()), "U3", Convert.ToInt32(btn_PTA_Save.ToolTip));
                    InspectionPlan(Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "PTA", cbo_CUI_InsEff.SelectedValue.ToString(), Convert.ToDateTime(dt_CUI_InspectDate.SelectedDate.ToString().Trim()));
                    lblStatus.Text = "Successfully SCCDF-PTA Cracking Value Updated";
                    lblStatus.ForeColor = Color.Green;
                }
                BusinessTier.DisposeConnection(conn);
                btn_PTA_Save.Enabled = false;
                PTAClear();
            }
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn_PTA_Save_Click", ex.ToString(), "Audit");
            //lblStatus.Text = "Err:" + ex.Message.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }

    }

    //<-------------------CLSCC------------------------>

    protected void OnSelectedIndexChanged_CLS(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()))
            {
                lblStatus.Text = "Please Select Component";
            }
            else
            {


                lblStatus.Text = string.Empty;
                int optemp = Convert.ToInt32(cboComponent.SelectedItem.Attributes["OPTemp"].ToString());

                string GLT = cbo_CLS_pH.SelectedValue.ToString().Trim();

                int temp = 0;
                if (GLT == "LT")
                {
                    if (optemp > 38 && optemp < 66)
                    {
                        temp = 38;
                    }
                    else if (optemp >= 66 && optemp < 93)
                    {
                        temp = 66;
                    }
                    else if (optemp >= 93 && optemp < 149)
                    {
                        temp = 93;
                    }
                    else if (optemp < 38)
                    {
                        temp = 38;
                        //lblStatus.Text = "Temperature Out Of Range Select Correct Component";
                        //return;
                    }
                    else
                    {
                        temp = 93;
                        //lblStatus.Text = "Temperature Out Of Range Select Correct Component";
                        //return;
                    }

                }

                else if (GLT == "GT")
                {
                    if (optemp < 93)
                    {
                        temp = 93;
                    }
                    else if (optemp >= 93 && optemp < 149)
                    {
                        temp = 149;
                    }
                    else
                    {
                        temp = 149;
                        //lblStatus.Text = "Temperature Out Of Range Select Correct Component";
                        //return;
                    }

                }


                chk_CLS_known.Checked = false;
                string strqryH2S = "select " + cbo_CLS_Chl.SelectedValue.ToString().Trim() + " as dfCLS from Ref_CLSCC where temp=" + temp + " and pH = '" + GLT + "'";
                SqlCommand cmdH2S = new SqlCommand(strqryH2S, conn);
                SqlDataReader rdH2S = cmdH2S.ExecuteReader();
                rdH2S.Read();
                txt_CLS_Svi.Text = rdH2S["dfCLS"].ToString().Trim();
                rdH2S.Close();
                BusinessTier.DisposeConnection(conn);
            }
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "OnSelectedIndexChanged_CLS", ex.ToString(), "Audit");
            //lblStatus.Text = "Err:" + ex.Message.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);

        }
    }

    protected void chk_CLS_known_CheckedChanged(object sender, EventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            if (chk_CLS_known.Checked)
            {
                txt_CLS_Svi.Text = "High";
                txt_CLS_Df.Text = string.Empty;
            }
            else
            {

                int optemp = Convert.ToInt32(cboComponent.SelectedItem.Attributes["OPTemp"].ToString());

                string GLT = cbo_CLS_pH.SelectedValue.ToString().Trim();

                int temp = 0;
                if (GLT == "LT")
                {
                    if (optemp > 38 && optemp < 66)
                    {
                        temp = 38;
                    }
                    else if (optemp >= 66 && optemp < 93)
                    {
                        temp = 66;
                    }
                    else if (optemp >= 93 && optemp < 149)
                    {
                        temp = 93;
                    }
                    else if (optemp < 38)
                    {
                        temp = 38;
                        //lblStatus.Text = "Temperature Out Of Range Select Correct Component";
                        //return;
                    }
                    else
                    {
                        temp = 93;
                        //lblStatus.Text = "Temperature Out Of Range Select Correct Component";
                        //return;
                    }

                }

                else if (GLT == "GT")
                {
                    if (optemp < 93)
                    {
                        temp = 93;
                    }
                    else if (optemp >= 93 && optemp < 149)
                    {
                        temp = 149;
                    }
                    else
                    {
                        temp = 149;
                        //lblStatus.Text = "Temperature Out Of Range Select Correct Component";
                        //return;
                    }

                }



                string sql1 = "select " + cbo_CLS_Chl.SelectedValue.ToString().Trim() + " as dfCLS from Ref_CLSCC where temp=" + temp + " and pH = '" + GLT + "'";
                SqlCommand cmd = new SqlCommand(sql1, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                rd.Read();
                txt_CLS_Svi.Text = rd["dfCLS"].ToString().Trim();
                txt_CLS_Df.Text = string.Empty;
                rd.Close();
                BusinessTier.DisposeConnection(conn);
            }
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "chk_CLS_known_CheckedChanged", ex.ToString(), "Audit");
            //lblStatus.Text = "Err:" + ex.Message.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);

        }

    }

    protected void btn_CLS_Calc_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {

            if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_CLS_InsEff.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_ClS_Age.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_CLS_Svi.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_CLS_nofIns.Text.ToString().Trim()))
            {
                lblStatus.Text = "Please check all the  fields";
                lblStatus.ForeColor = Color.Red;
            }
            else
            {

                int svival = 0;
                if (txt_CLS_Svi.Text.ToString().Trim() == "High")
                {
                    svival = 5000;
                }
                else if (txt_CLS_Svi.Text.ToString().Trim() == "Medium")
                {
                    svival = 500;
                }
                else if (txt_CLS_Svi.Text.ToString().Trim() == "Low")
                {
                    svival = 50;
                }
                else if (txt_CLS_Svi.Text.ToString().Trim() == "None")
                {
                    svival = 1;
                }
                string strqry = "select " + cbo_CLS_InsEff.SelectedValue.ToString().Trim() + " as dfCLS from Ref_SCC where Inspection=" + cbo_CLS_nofIns.SelectedValue.ToString().Trim() + " and Svi=" + svival.ToString().Trim() + "";
                SqlCommand cmd = new SqlCommand(strqry, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                rd.Read();
                double df, dfb_CLS;
                df = Convert.ToInt32(rd["dfCLS"].ToString().Trim());
                dfb_CLS = df * (Math.Pow(Convert.ToDouble(txt_ClS_Age.Text.ToString().Trim()), 1.1));
                txt_CLS_Df.Text = Math.Round(dfb_CLS, 3).ToString();
                rd.Close();
                BusinessTier.DisposeConnection(conn);
            }
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn_CLS_Calc_Click", ex.ToString(), "Audit");
            // lblStatus.Text = "Err:" + ex.Message.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);

        }

    }

    protected void btn_CLS_Save_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {

            if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_CLS_InsEff.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_ClS_Age.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_CLS_Svi.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_CLS_nofIns.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_CLS_Df.Text.ToString().Trim()))
            {
                lblStatus.Text = "Please check all the  fields";
                lblStatus.ForeColor = Color.Red;
            }
            else
            {

                int svival = 0;
                if (txt_CLS_Svi.Text.ToString().Trim() == "High")
                {
                    svival = 5000;
                }
                else if (txt_CLS_Svi.Text.ToString().Trim() == "Medium")
                {
                    svival = 500;
                }
                else if (txt_CLS_Svi.Text.ToString().Trim() == "Low")
                {
                    svival = 50;
                }
                else if (txt_CLS_Svi.Text.ToString().Trim() == "None")
                {
                    svival = 1;
                }

                if (btnCLSSubmit.ToolTip == "Save")
                {
                    int intFlag = BusinessTier.SulfideSave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), Convert.ToInt32(txt_ClS_Age.Text.ToString().Trim()), cbo_CLS_InsEff.SelectedValue.ToString().Trim(), Convert.ToInt32(cbo_CLS_nofIns.SelectedItem.Text.ToString().Trim()), Convert.ToDateTime(dt_CLS_InspectDate.SelectedDate.ToString()), txt_CLS_Svi.Text.ToString().Trim(), Convert.ToInt32(svival.ToString().Trim()), Convert.ToDouble(txt_CLS_Df.Text.ToString().Trim()), cbo_CLS_pH.SelectedItem.Text.ToString().Trim(), cbo_CLS_Chl.SelectedItem.Text.ToString().Trim(), Convert.ToInt32(Session["sesUserID"].ToString()), "N4", 0);
                    InspectionPlan(Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "CLSCC", cbo_CUI_InsEff.SelectedValue.ToString(), Convert.ToDateTime(dt_CUI_InspectDate.SelectedDate.ToString().Trim()));
                    lblStatus.Text = "Successfully CLSCC CrackingValue Inserted";
                    lblStatus.ForeColor = Color.Green;
                }
                else if (btnCLSSubmit.ToolTip == "Update")
                {
                    int intFlag = BusinessTier.SulfideSave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), Convert.ToInt32(txt_ClS_Age.Text.ToString().Trim()), cbo_CLS_InsEff.SelectedValue.ToString().Trim(), Convert.ToInt32(cbo_CLS_nofIns.SelectedItem.Text.ToString().Trim()), Convert.ToDateTime(dt_CLS_InspectDate.SelectedDate.ToString()), txt_CLS_Svi.Text.ToString().Trim(), Convert.ToInt32(svival.ToString().Trim()), Convert.ToDouble(txt_CLS_Df.Text.ToString().Trim()), cbo_CLS_pH.SelectedItem.Text.ToString().Trim(), cbo_CLS_Chl.SelectedItem.Text.ToString().Trim(), Convert.ToInt32(Session["sesUserID"].ToString()), "U4", Convert.ToInt32(btn_CLS_Save.ToolTip));
                    InspectionPlan(Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "CLSCC", cbo_CUI_InsEff.SelectedValue.ToString(), Convert.ToDateTime(dt_CUI_InspectDate.SelectedDate.ToString().Trim()));
                    lblStatus.Text = "Successfully CLSCC Cracking Value Updated";
                    lblStatus.ForeColor = Color.Green;
                }
                BusinessTier.DisposeConnection(conn);
                btn_CLS_Save.Enabled = false;
                CLSClear();
            }
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn_CLS_Calc_Click", ex.ToString(), "Audit");
            //lblStatus.Text = "Err:" + ex.Message.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);

        }

    }

    //<-------------------HSC-HF------------------------>

    protected void OnSelectedIndexChanged_HSC(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        chk_HSC_known.Checked = false;
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {

            string sql1 = "select " + cbo_HSC_Brinnel.SelectedValue.ToString().Trim() + " as dfHSC from Ref_HSC where Heat='" + cbo_HSC_PWHT.SelectedValue.ToString().Trim() + "'";
            SqlCommand cmd = new SqlCommand(sql1, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            rd.Read();
            txt_HSC_Svi.Text = rd["dfHSC"].ToString().Trim();
            rd.Close();
            BusinessTier.DisposeConnection(conn);
            //string BrinHard=string.Empty;
            //string pwht = cbo_HSC_PWHT.SelectedValue.ToString().Trim();
            //string brinval = cbo_HSC_Brinnel.SelectedValue.ToString().Trim();
            //if (pwht == "As-Welded")
            //{
            //    if (brinval == "two")
            //    {
            //        BrinHard = "Low";
            //    }
            //    else if (brinval == "twothree")
            //    {
            //        BrinHard = "Medium";
            //    }
            //    else if (brinval == "gttwothree")
            //    {
            //        BrinHard = "High";
            //    }
            //}
            //else if (pwht == "PWHT")
            //{
            //    if (brinval == "two")
            //    {
            //        BrinHard = "None";
            //    }
            //    else if (brinval == "twothree")
            //    {
            //        BrinHard = "Low";
            //    }
            //    else if (brinval == "gttwothree")
            //    {
            //        BrinHard = "High";
            //    }
            //}

            //txt_HSC_Svi.Text = BrinHard;

        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "OnSelectedIndexChanged_HSC", ex.ToString(), "Audit");
            //lblStatus.Text = "Err:" + ex.Message.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);

        }

    }

    protected void chk_HSC_known_CheckedChanged(object sender, EventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {

            if (chk_HSC_known.Checked)
            {
                txt_HSC_Svi.Text = "High";
                txt_HSC_Df.Text = string.Empty;
            }
            else
            {

                string sql1 = "select " + cbo_HSC_Brinnel.SelectedValue.ToString().Trim() + " as dfHSC from Ref_HSC where Heat='" + cbo_HSC_PWHT.SelectedValue.ToString().Trim() + "'";
                SqlCommand cmd = new SqlCommand(sql1, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                rd.Read();
                txt_HSC_Svi.Text = rd["dfHSC"].ToString().Trim();
                txt_HSC_Df.Text = string.Empty;
                rd.Close();
                BusinessTier.DisposeConnection(conn);
            }
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "chk_HSC_known_CheckedChanged", ex.ToString(), "Audit");
            //lblStatus.Text = "Err:" + ex.Message.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }

    }

    protected void btn_HSC_Calc_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {

            if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_HSC_InsEff.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_HSC_Age.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_HSC_Svi.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_HSC_nofIns.Text.ToString().Trim()))
            {
                lblStatus.Text = "Please check all the  fields";
                lblStatus.ForeColor = Color.Red;
            }
            else
            {

                int svival = 0;
                if (txt_HSC_Svi.Text.ToString().Trim() == "High")
                {
                    svival = 100;
                }
                else if (txt_HSC_Svi.Text.ToString().Trim() == "Medium")
                {
                    svival = 10;
                }
                else if (txt_HSC_Svi.Text.ToString().Trim() == "Low")
                {
                    svival = 1;
                }
                else if (txt_HSC_Svi.Text.ToString().Trim() == "None")
                {
                    svival = 1;
                }
                string strqry = "select " + cbo_HSC_InsEff.SelectedValue.ToString().Trim() + " as dfHSC from Ref_SCC where Inspection=" + cbo_HSC_nofIns.SelectedValue.ToString().Trim() + " and Svi=" + svival.ToString().Trim() + "";
                SqlCommand cmd = new SqlCommand(strqry, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                rd.Read();
                double df, dfb_HSC;
                df = Convert.ToInt32(rd["dfHSC"].ToString().Trim());
                dfb_HSC = df * (Math.Pow(Convert.ToDouble(txt_HSC_Age.Text.ToString().Trim()), 1.1));
                txt_HSC_Df.Text = Math.Round(dfb_HSC, 3).ToString();
                rd.Close();
                BusinessTier.DisposeConnection(conn);
            }
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn_HSC_Calc_Click", ex.ToString(), "Audit");
            //lblStatus.Text = "Err:" + ex.Message.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }

    }

    protected void btn_HSC_Save_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {

            if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_HSC_InsEff.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_HSC_Age.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_HSC_Svi.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_HSC_nofIns.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_HSC_Df.Text.ToString().Trim()))
            {
                lblStatus.Text = "Please check all the  fields";
                lblStatus.ForeColor = Color.Red;
            }
            else
            {

                int svival = 0;
                if (txt_HSC_Svi.Text.ToString().Trim() == "High")
                {
                    svival = 100;
                }
                else if (txt_HSC_Svi.Text.ToString().Trim() == "Medium")
                {
                    svival = 10;
                }
                else if (txt_HSC_Svi.Text.ToString().Trim() == "Low")
                {
                    svival = 1;
                }
                else if (txt_HSC_Svi.Text.ToString().Trim() == "None")
                {
                    svival = 1;
                }

                if (btnHSCSubmit.ToolTip == "Save")
                {
                    int intFlag = BusinessTier.SulfideSave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), Convert.ToInt32(txt_HSC_Age.Text.ToString().Trim()), cbo_HSC_InsEff.SelectedValue.ToString().Trim(), Convert.ToInt32(cbo_HSC_nofIns.SelectedItem.Text.ToString().Trim()), Convert.ToDateTime(dt_HSC_InspectDate.SelectedDate.ToString()), txt_HSC_Svi.Text.ToString().Trim(), Convert.ToInt32(svival.ToString().Trim()), Convert.ToDouble(txt_HSC_Df.Text.ToString().Trim()), cbo_HSC_PWHT.SelectedItem.Text.ToString().Trim(), cbo_HSC_Brinnel.SelectedItem.Text.ToString().Trim(), Convert.ToInt32(Session["sesUserID"].ToString()), "N5", 0);
                    InspectionPlan(Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "HSC-HF", cbo_CUI_InsEff.SelectedValue.ToString(), Convert.ToDateTime(dt_CUI_InspectDate.SelectedDate.ToString().Trim()));
                    lblStatus.Text = "Successfully HSC-HF CrackingValue Inserted";
                    lblStatus.ForeColor = Color.Green;
                }
                else if (btnHSCSubmit.ToolTip == "Update")
                {
                    int intFlag = BusinessTier.SulfideSave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), Convert.ToInt32(txt_HSC_Age.Text.ToString().Trim()), cbo_HSC_InsEff.SelectedValue.ToString().Trim(), Convert.ToInt32(cbo_HSC_nofIns.SelectedItem.Text.ToString().Trim()), Convert.ToDateTime(dt_HSC_InspectDate.SelectedDate.ToString()), txt_HSC_Svi.Text.ToString().Trim(), Convert.ToInt32(svival.ToString().Trim()), Convert.ToDouble(txt_HSC_Df.Text.ToString().Trim()), cbo_HSC_PWHT.SelectedItem.Text.ToString().Trim(), cbo_HSC_Brinnel.SelectedItem.Text.ToString().Trim(), Convert.ToInt32(Session["sesUserID"].ToString()), "U5", Convert.ToInt32(btn_HSC_Save.ToolTip));
                    InspectionPlan(Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "HSC-HF", cbo_CUI_InsEff.SelectedValue.ToString(), Convert.ToDateTime(dt_CUI_InspectDate.SelectedDate.ToString().Trim()));
                    lblStatus.Text = "Successfully HSC-HF Cracking Value Updated";
                    lblStatus.ForeColor = Color.Green;
                }
                BusinessTier.DisposeConnection(conn);
                btn_HSC_Save.Enabled = false;
                HSCClear();
            }
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn_HSC_Calc_Click", ex.ToString(), "Audit");
            //lblStatus.Text = "Err:" + ex.Message.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }

    }

    //<-------------------HIC/SOHIC-HF------------------------>

    protected void OnSelectedIndexChanged_HF(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        chk_HF_known.Checked = false;
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {

            string sql1 = "select " + cbo_HF_pwht.SelectedValue.ToString().Trim() + " as dfHF from Ref_HIC where stlsul='" + cbo_HF_Mat.SelectedValue.ToString().Trim() + "' and Envi='High'";
            SqlCommand cmd = new SqlCommand(sql1, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            rd.Read();
            txt_HF_Svi.Text = rd["dfHF"].ToString().Trim();
            rd.Close();
            BusinessTier.DisposeConnection(conn);
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "OnSelectedIndexChanged_HF", ex.ToString(), "Audit");
            //lblStatus.Text = "Err:" + ex.Message.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }

    }

    protected void chk_HF_known_CheckedChanged(object sender, EventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            if (chk_HF_known.Checked)
            {
                txt_HF_Svi.Text = "High";
                txt_HF_Df.Text = string.Empty;
            }
            else
            {


                string sql1 = "select " + cbo_HF_pwht.SelectedValue.ToString().Trim() + " as dfHF from Ref_HIC where stlsul='" + cbo_HF_Mat.SelectedValue.ToString().Trim() + "' and Envi='High'";
                SqlCommand cmd = new SqlCommand(sql1, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                rd.Read();
                txt_HF_Svi.Text = rd["dfHF"].ToString().Trim();
                txt_HF_Df.Text = string.Empty;
                rd.Close();
                BusinessTier.DisposeConnection(conn);
            }
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "OnSelectedIndexChanged_HF", ex.ToString(), "Audit");
            //lblStatus.Text = "Err:" + ex.Message.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }

    }

    protected void btn_HF_Calc_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {

            if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_HF_InsEff.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_HF_Age.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_HF_Svi.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_HF_nofIns.Text.ToString().Trim()))
            {
                lblStatus.Text = "Please check all the  fields";
                lblStatus.ForeColor = Color.Red;
            }
            else
            {

                int svival = 0;
                if (txt_HF_Svi.Text.ToString().Trim() == "High")
                {
                    svival = 100;
                }
                else if (txt_HF_Svi.Text.ToString().Trim() == "Medium")
                {
                    svival = 10;
                }
                else if (txt_HF_Svi.Text.ToString().Trim() == "Low")
                {
                    svival = 1;
                }
                else if (txt_HF_Svi.Text.ToString().Trim() == "None")
                {
                    svival = 1;
                }
                string strqry = "select " + cbo_HF_InsEff.SelectedValue.ToString().Trim() + " as dfHF from Ref_SCC where Inspection=" + cbo_HF_nofIns.SelectedValue.ToString().Trim() + " and Svi=" + svival.ToString().Trim() + "";
                SqlCommand cmd = new SqlCommand(strqry, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                rd.Read();
                double df, dfb_HF;
                df = Convert.ToInt32(rd["dfHF"].ToString().Trim());
                dfb_HF = df * (Math.Pow(Convert.ToDouble(txt_HF_Age.Text.ToString().Trim()), 1.1));
                txt_HF_Df.Text = Math.Round(dfb_HF, 3).ToString();
                rd.Close();
                BusinessTier.DisposeConnection(conn);
            }
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn_HF_Calc_Click", ex.ToString(), "Audit");
            //lblStatus.Text = "Err:" + ex.Message.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }

    }

    protected void btn_HF_Save_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {

            if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_HF_InsEff.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_HF_Age.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_HF_Svi.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_HF_nofIns.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_HF_Df.Text.ToString().Trim()))
            {
                lblStatus.Text = "Please check all the  fields";
                lblStatus.ForeColor = Color.Red;
            }
            else
            {

                int svival = 0;
                if (txt_HF_Svi.Text.ToString().Trim() == "High")
                {
                    svival = 100;
                }
                else if (txt_HF_Svi.Text.ToString().Trim() == "Medium")
                {
                    svival = 10;
                }
                else if (txt_HF_Svi.Text.ToString().Trim() == "Low")
                {
                    svival = 1;
                }
                else if (txt_HF_Svi.Text.ToString().Trim() == "None")
                {
                    svival = 1;
                }
                if (btnHFSubmit.ToolTip == "Save")
                {
                    int intFlag = BusinessTier.SulfideSave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), Convert.ToInt32(txt_HF_Age.Text.ToString().Trim()), cbo_HF_InsEff.SelectedValue.ToString().Trim(), Convert.ToInt32(cbo_HF_nofIns.SelectedItem.Text.ToString().Trim()), Convert.ToDateTime(dt_HF_InspectDate.SelectedDate.ToString()), txt_HF_Svi.Text.ToString().Trim(), Convert.ToInt32(svival.ToString().Trim()), Convert.ToDouble(txt_HF_Df.Text.ToString().Trim()), cbo_HF_pwht.SelectedItem.Text.ToString().Trim(), cbo_HF_Mat.SelectedItem.Text.ToString().Trim(), Convert.ToInt32(Session["sesUserID"].ToString()), "N6", 0);
                    InspectionPlan(Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "HIC/SOHIC-HF", cbo_CUI_InsEff.SelectedValue.ToString(), Convert.ToDateTime(dt_CUI_InspectDate.SelectedDate.ToString().Trim()));
                    lblStatus.Text = "Successfully HSC-HF CrackingValue Inserted";
                    lblStatus.ForeColor = Color.Green;
                }
                else if (btnHFSubmit.ToolTip == "Update")
                {
                    int intFlag = BusinessTier.SulfideSave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), Convert.ToInt32(txt_HF_Age.Text.ToString().Trim()), cbo_HF_InsEff.SelectedValue.ToString().Trim(), Convert.ToInt32(cbo_HF_nofIns.SelectedItem.Text.ToString().Trim()), Convert.ToDateTime(dt_HF_InspectDate.SelectedDate.ToString()), txt_HF_Svi.Text.ToString().Trim(), Convert.ToInt32(svival.ToString().Trim()), Convert.ToDouble(txt_HF_Df.Text.ToString().Trim()), cbo_HF_pwht.SelectedItem.Text.ToString().Trim(), cbo_HF_Mat.SelectedItem.Text.ToString().Trim(), Convert.ToInt32(Session["sesUserID"].ToString()), "U6", Convert.ToInt32(btn_HF_Save.ToolTip));
                    InspectionPlan(Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "HIC/SOHIC-HF", cbo_CUI_InsEff.SelectedValue.ToString(), Convert.ToDateTime(dt_CUI_InspectDate.SelectedDate.ToString().Trim()));
                    lblStatus.Text = "Successfully HSC-HF Cracking Value Updated";
                    lblStatus.ForeColor = Color.Green;
                }
                BusinessTier.DisposeConnection(conn);
                btn_HF_Save.Enabled = false;
                HFClear();
            }
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn_HF_Save_Click", ex.ToString(), "Audit");
            lblStatus.Text = "Err:" + ex.Message.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }


    }

    //<-------------------External Corrosion------------------------>

    protected void OnSelectedIndexChanged_ECD_CoatQual(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_ECD_InsEff.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_ECD_Agetk.Text.ToString().Trim()) || string.IsNullOrEmpty(dt_ECD_CalcDate.SelectedDate.ToString().Trim()) || string.IsNullOrEmpty(dt_ECD_CmpInstal.SelectedDate.ToString().Trim()))
        {
            lblStatus.Text = "Please check all the  fields";
            lblStatus.ForeColor = Color.Red;
        }
        else
        {
            try
            {
                lblStatus.Text = string.Empty;
                DateTime CalcDt, Dt = DateTime.Today;
                TimeSpan SubDt;
                int agecoat = 0, age = 0, calagecoat = 0;
                if (cbo_ECD_CoatQual.SelectedValue.ToString().Trim() == "1")
                {
                    Dt = Convert.ToDateTime(dt_ECD_CmpInstal.SelectedDate.Value.ToString());
                }
                else if (cbo_ECD_CoatQual.SelectedValue.ToString().Trim() == "2")
                {
                    Dt = Convert.ToDateTime(dt_ECD_CmpInstal.SelectedDate.Value.AddYears(5));
                }
                else if (cbo_ECD_CoatQual.SelectedValue.ToString().Trim() == "3")
                {
                    Dt = Convert.ToDateTime(dt_ECD_CmpInstal.SelectedDate.Value.AddYears(15));
                }
                CalcDt = dt_ECD_CalcDate.SelectedDate.Value;
                SubDt = CalcDt.Subtract(Dt);
                calagecoat = Convert.ToInt32(SubDt.TotalDays.ToString()) / 365;
                agecoat = Math.Max(0, calagecoat);
                age = Math.Min(Convert.ToInt32(txt_ECD_Agetk.Text.ToString().Trim()), agecoat);

                txt_ECD_AgeCoat.Text = agecoat.ToString();
                txt_ECD_Age.Text = age.ToString();
            }
            catch (Exception ex)
            {
                InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "OnSelectedIndexChanged_ECD_CoatQual", ex.ToString(), "Audit");
            }
        }
    }

    protected void OnSelectedIndexChanged_ECD_crb(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_ECD_InsEff.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_ECD_Agetk.Text.ToString().Trim()) || string.IsNullOrEmpty(dt_ECD_CalcDate.SelectedDate.ToString().Trim()) || string.IsNullOrEmpty(dt_ECD_CmpInstal.SelectedDate.ToString().Trim()) || string.IsNullOrEmpty(cbo_ECD_Fip.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_ECD_Fps.Text.ToString().Trim()))
        {
            lblStatus.Text = "Please check all the  fields";
            lblStatus.ForeColor = Color.Red;
        }
        else
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            try
            {
                double optemp = Convert.ToDouble(cboComponent.SelectedItem.Attributes["OPTemp"].ToString());

                if (optemp > -12 && optemp < -8)
                {
                    optemp = -12;
                }
                else if (optemp >= -8 && optemp < 6)
                {
                    optemp = -8;
                }
                else if (optemp >= 6 && optemp < 32)
                {
                    optemp = 6;
                }
                else if (optemp >= 32 && optemp < 71)
                {
                    optemp = 32;
                }
                else if (optemp >= 71 && optemp < 107)
                {
                    optemp = 71;
                }
                else if (optemp >= 107 && optemp < 121)
                {
                    optemp = 107;
                }
                else if (optemp >= 121)
                {
                    optemp = 121;
                }
                else
                {
                    optemp = -12;
                }

                lblStatus.Text = string.Empty;
                string sql1 = "select " + cbo_ECD_crdriver.SelectedValue.ToString().Trim() + " as crbECD from Ref_ECD where opTemp='" + optemp.ToString().Trim() + "'";
                SqlCommand cmd = new SqlCommand(sql1, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                rd.Read();
                double cr = 0.0;
                cr = Convert.ToDouble(rd["crbECD"].ToString().Trim()) * Math.Max(Convert.ToDouble(cbo_ECD_Fps.SelectedItem.Text.ToString().Trim()), Convert.ToDouble(cbo_ECD_Fip.SelectedItem.Text.ToString().Trim()));
                txt_ECD_cr.Text = cr.ToString().Trim();
                rd.Close();
                BusinessTier.DisposeConnection(conn);
            }
            catch (Exception ex)
            {
                BusinessTier.DisposeConnection(conn);
                InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "OnSelectedIndexChanged_ECD_crb", ex.ToString(), "Audit");
                //  lblStatus.Text = ex.Message.ToString();
                return;

            }

        }
    }

    protected void btn_ECD_Calc_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_ECD_InsEff.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_ECD_Agetk.Text.ToString().Trim()) || string.IsNullOrEmpty(dt_ECD_CalcDate.SelectedDate.ToString().Trim()) || string.IsNullOrEmpty(dt_ECD_CmpInstal.SelectedDate.ToString().Trim()))
        {
            lblStatus.Text = "Please check all the  fields";
            lblStatus.ForeColor = Color.Red;
        }
        else
        {
            lblStatus.Text = string.Empty;
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            try
            {
                double tmin = Convert.ToDouble(cboComponent.SelectedItem.Attributes["MRT"].ToString());
                double CA = Convert.ToDouble(cboComponent.SelectedItem.Attributes["CorrosionAllownce"].ToString());
                string sql1 = "SELECT [ReadVal] as trd FROM [RBI].[dbo].[Tbl_EquipmentComponentDetails] where [CompAutoID]=" + cboComponent.SelectedValue.ToString().Trim() + " and Deleted=0";
                SqlCommand cmd = new SqlCommand(sql1, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                rd.Read();
                double trd = 0.0, ar = 0.0, artval = 0.0;
                trd = Convert.ToDouble(rd["trd"].ToString().Trim());
                ar = 1 - (trd - Convert.ToDouble(txt_ECD_cr.Text.ToString()) * Convert.ToDouble(txt_ECD_Age.Text.ToString()) / (tmin + CA));
                artval = Math.Max(ar, 0.0);
                txt_ECD_art.Text = artval.ToString().Trim();
                BusinessTier.DisposeReader(rd);

                double finalart = 0;
                if (artval >= 0.02 && artval < 0.04)
                {
                    finalart = 0.02;
                }
                else if (artval >= 0.04 && artval < 0.06)
                {
                    finalart = 0.04;
                }
                else if (artval >= 0.06 && artval < 0.08)
                {
                    finalart = 0.06;
                }
                else if (artval >= 0.08 && artval < 0.10)
                {
                    finalart = 0.08;
                }
                else if (artval >= 0.10 && artval < 0.12)
                {
                    finalart = 0.10;
                }
                else if (artval < 0.12 && artval < 0.14)
                {
                    finalart = 0.12;
                }
                else if (artval >= 0.14 && artval < 0.16)
                {
                    finalart = 0.14;
                }
                else if (artval >= 0.16 && artval < 0.18)
                {
                    finalart = 0.16;
                }
                else if (artval >= 0.18 && artval < 0.20)
                {
                    finalart = 0.18;
                }
                else if (artval >= 0.20 && artval < 0.25)
                {
                    finalart = 0.20;
                }
                else if (artval >= 0.25 && artval < 0.30)
                {
                    finalart = 0.25;
                }
                else if (artval >= 0.30 && artval < 0.35)
                {
                    finalart = 0.30;
                }
                else if (artval >= 0.35 && artval < 0.40)
                {
                    finalart = 0.40;
                }
                else if (artval >= 0.40 && artval < 0.45)
                {
                    finalart = 0.40;
                }
                else if (artval >= 0.45 && artval < 0.50)
                {
                    finalart = 0.45;
                }
                else if (artval >= 0.50 && artval < 0.55)
                {
                    finalart = 0.50;
                }
                else if (artval >= 0.55 && artval < 0.60)
                {
                    finalart = 0.55;
                }
                else if (artval >= 0.60 && artval < 0.65)
                {
                    finalart = 0.60;
                }
                else if (artval >= 0.65)
                {
                    finalart = 0.65;
                }
                else
                {
                    finalart = 0.02;
                }


                string strqry2 = "select " + cbo_ECD_InsEff.SelectedValue.ToString().Trim() + " as ECD_inspect from Tbl_InspectionEffective where art= " + finalart + " and inspection=" + cbo_ECD_nofIns.SelectedValue.ToString().Trim() + " ";
                SqlCommand cmd1 = new SqlCommand(strqry2, conn);
                SqlDataReader rdr1 = cmd1.ExecuteReader();
                rdr1.Read();
                txt_ECD_df.Text = rdr1["ECD_inspect"].ToString().Trim();
                BusinessTier.DisposeReader(rdr1);

                BusinessTier.DisposeConnection(conn);
            }
            catch (Exception ex)
            {
                BusinessTier.DisposeConnection(conn);
                InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn_ECD_Calc_Click", ex.ToString(), "Audit");
                // lblStatus.Text = ex.Message.ToString();
                return;

            }

        }
    }

    protected void btn_ECD_Save_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_ECD_InsEff.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_ECD_Agetk.Text.ToString().Trim()) || string.IsNullOrEmpty(dt_ECD_CalcDate.SelectedDate.ToString().Trim()) || string.IsNullOrEmpty(dt_ECD_CmpInstal.SelectedDate.ToString().Trim()) || string.IsNullOrEmpty(txt_ECD_art.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_ECD_df.Text.ToString().Trim()))
        {
            lblStatus.Text = "Err : Please check all the fields";
            lblStatus.ForeColor = Color.Red;
        }
        else
        {

            try
            {
                if (btnECDSubmit.ToolTip == "Save")
                {
                    int intFlag = BusinessTier.ECDSave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), Convert.ToInt32(txt_ECD_Agetk.Text.ToString().Trim()), cbo_ECD_InsEff.SelectedValue.ToString().Trim(), Convert.ToDateTime(dt_ECD_CmpInstal.SelectedDate.ToString().Trim()), Convert.ToDateTime(dt_ECD_CalcDate.SelectedDate.ToString().Trim()), Convert.ToInt32(cbo_ECD_nofIns.SelectedItem.Text.ToString().Trim()), Convert.ToDateTime(dt_ECD_InspectDate.SelectedDate.ToString().Trim()), cbo_ECD_CoatQual.SelectedItem.Text.ToString().Trim(), Convert.ToDecimal(txt_ECD_AgeCoat.Text.ToString().Trim()), Convert.ToDecimal(txt_ECD_Age.Text.ToString().Trim()), Convert.ToInt32(cbo_ECD_Fps.SelectedValue.ToString().Trim()), Convert.ToInt32(cbo_ECD_Fip.SelectedValue.ToString().Trim()), cbo_ECD_crdriver.SelectedItem.Text.ToString().Trim(), Convert.ToDecimal(txt_ECD_cr.Text.ToString().Trim()), Convert.ToDecimal(txt_ECD_art.Text.ToString().Trim()), Convert.ToDecimal(txt_ECD_df.Text.ToString().Trim()), Convert.ToInt32(Session["sesCompanyID"].ToString()), Convert.ToInt32(Session["sesUserID"].ToString()), "N", 0);
                    InspectionPlan(Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "ExternalCorrosion", cbo_ECD_InsEff.SelectedValue.ToString(), Convert.ToDateTime(dt_ECD_InspectDate.SelectedDate.ToString().Trim()));
                    lblStatus.Text = "Successfully ExternalCorrosion Value Inserted";
                    lblStatus.ForeColor = Color.Green;
                }
                else if (btnECDSubmit.ToolTip == "Update")
                {
                    int intFlag = BusinessTier.ECDSave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), Convert.ToInt32(txt_ECD_Agetk.Text.ToString().Trim()), cbo_ECD_InsEff.SelectedValue.ToString().Trim(), Convert.ToDateTime(dt_ECD_CmpInstal.SelectedDate.ToString().Trim()), Convert.ToDateTime(dt_ECD_CalcDate.SelectedDate.ToString().Trim()), Convert.ToInt32(cbo_ECD_nofIns.SelectedItem.Text.ToString().Trim()), Convert.ToDateTime(dt_ECD_InspectDate.SelectedDate.ToString().Trim()), cbo_ECD_CoatQual.SelectedItem.Text.ToString().Trim(), Convert.ToDecimal(txt_ECD_AgeCoat.Text.ToString().Trim()), Convert.ToDecimal(txt_ECD_Age.Text.ToString().Trim()), Convert.ToInt32(cbo_ECD_Fps.SelectedValue.ToString().Trim()), Convert.ToInt32(cbo_ECD_Fip.SelectedValue.ToString().Trim()), cbo_ECD_crdriver.SelectedItem.Text.ToString().Trim(), Convert.ToDecimal(txt_ECD_cr.Text.ToString().Trim()), Convert.ToDecimal(txt_ECD_art.Text.ToString().Trim()), Convert.ToDecimal(txt_ECD_df.Text.ToString().Trim()), Convert.ToInt32(Session["sesCompanyID"].ToString()), Convert.ToInt32(Session["sesUserID"].ToString()), "U", Convert.ToInt32(btn_ECD_Save.ToolTip));
                    InspectionPlan(Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "ExternalCorrosion", cbo_ECD_InsEff.SelectedValue.ToString(), Convert.ToDateTime(dt_ECD_InspectDate.SelectedDate.ToString().Trim()));
                    lblStatus.Text = "Successfully ExternalCorrosion Value Updated";
                    lblStatus.ForeColor = Color.Green;
                }

                BusinessTier.DisposeConnection(conn);
                ECDClear();
                btn_ECD_Save.Enabled = false;
            }
            catch (Exception ex)
            {
                // lblStatus.Text = ex.Message.ToString();
                InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn_ECD_Save_Click", ex.ToString(), "Audit");
            }
            finally
            {
                BusinessTier.DisposeConnection(conn);
            }
        }
    }

    protected void dt_ECD_InspectDate_OnSelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
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

        int maxdate = 0;
        maxdate = Convert.ToInt32(dt_ECD_InspectDate.SelectedDate.Value.Year);

        txt_ECD_Agetk.Text = Agecal(maxdate, cboProcessArea.SelectedValue.ToString(), cboEquipment.SelectedValue.ToString(), cboComponent.SelectedValue.ToString(), Session["sesCompanyID"].ToString()).ToString();

        //string str2 = "SELECT yearinstalled as mindate FROM [Tbl_EquipmentAsset] where deleted=0 and [ProcessareaID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquAutoID]='" + cboEquipment.SelectedValue.ToString() + "'";
        //SqlCommand cmd2 = new SqlCommand(str2, conn);
        //SqlDataReader rdr2 = cmd2.ExecuteReader();
        //int mindate = 0, caldate = 0;
        //if (rdr2.Read())
        //{
        //    mindate = Convert.ToInt32(rdr2["mindate"].ToString());
        //}
        //rdr2.Close();
        //caldate = maxdate - mindate;
        //txt_Age.Text = caldate.ToString();
        //BusinessTier.DisposeConnection(conn);
    }

    //<-------------------CUI----------------------- using double reader here--------->

    protected void OnSelectedIndexChanged_CUI_CoatQual(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_CUI_InsEff.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_CUI_Agetk.Text.ToString().Trim()) || string.IsNullOrEmpty(dt_CUI_CalcDate.SelectedDate.ToString().Trim()) || string.IsNullOrEmpty(dt_CUI_CmpInstal.SelectedDate.ToString().Trim()))
        {
            lblStatus.Text = "Please check all the  fields";
            lblStatus.ForeColor = Color.Red;
        }
        else
        {
            try
            {
                lblStatus.Text = string.Empty;
                DateTime CalcDt, Dt = DateTime.Today;
                TimeSpan SubDt;
                int agecoat = 0, age = 0, calagecoat = 0;
                if (cbo_CUI_coatqual.SelectedValue.ToString().Trim() == "1")
                {
                    Dt = Convert.ToDateTime(dt_CUI_CmpInstal.SelectedDate.Value.ToString());
                }
                else if (cbo_CUI_coatqual.SelectedValue.ToString().Trim() == "2")
                {
                    Dt = Convert.ToDateTime(dt_CUI_CmpInstal.SelectedDate.Value.AddYears(5));
                }
                else if (cbo_CUI_coatqual.SelectedValue.ToString().Trim() == "3")
                {
                    Dt = Convert.ToDateTime(dt_CUI_CmpInstal.SelectedDate.Value.AddYears(15));
                }
                CalcDt = dt_CUI_CalcDate.SelectedDate.Value;
                SubDt = CalcDt.Subtract(Dt);
                calagecoat = Convert.ToInt32(SubDt.TotalDays.ToString()) / 365;
                agecoat = Math.Max(0, calagecoat);
                age = Math.Min(Convert.ToInt32(txt_CUI_Agetk.Text.ToString().Trim()), agecoat);
                txt_CUI_Agecoat.Text = agecoat.ToString();
                txt_CUI_Age.Text = age.ToString();
            }
            catch (Exception ex)
            {
                InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "OnSelectedIndexChanged_CUI_CoatQual", ex.ToString(), "Audit");
            }
        }
    }

    protected void OnSelectedIndexChanged_CUI_crb(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_CUI_InsEff.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_CUI_Agetk.Text.ToString().Trim()) || string.IsNullOrEmpty(dt_CUI_CalcDate.SelectedDate.ToString().Trim()) || string.IsNullOrEmpty(dt_CUI_CmpInstal.SelectedDate.ToString().Trim()) || string.IsNullOrEmpty(cbo_CUI_Fcm.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_CUI_Fic.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_CUI_Fins.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_CUI_Fip.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_CUI_Fps.Text.ToString().Trim()))
        {
            lblStatus.Text = "Please check all the  fields";
            lblStatus.ForeColor = Color.Red;
        }
        else
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            try
            {
                lblStatus.Text = string.Empty;
              //  Response.Write(cboComponent.SelectedItem.Attributes["OPTemp"].ToString());
                decimal optemp = Convert.ToDecimal(cboComponent.SelectedItem.Attributes["OPTemp"].ToString());
               

                if (optemp > -12 && optemp < -8)
                {
                    optemp = -12;
                }
                else if (optemp >= -8 && optemp < 6)
                {
                    optemp = -8;
                }
                else if (optemp >= 6 && optemp < 32)
                {
                    optemp = 6;
                }
                else if (optemp >= 32 && optemp < 71)
                {
                    optemp = 32;
                }
                else if (optemp >= 71 && optemp < 107)
                {
                    optemp = 71;
                }
                else if (optemp >= 107 && optemp < 135)
                {
                    optemp = 107;
                }
                else if (optemp >= 135 && optemp < 162)
                {
                    optemp = 135;
                }
                else if (optemp >= 162 && optemp < 176)
                {
                    optemp = 162;
                }
                else if (optemp >= 176)
                {
                    optemp = 176;
                }
                else
                {
                    optemp = -12;
                }

                string sql1 = "select " + cbo_CUI_crdriver.SelectedValue.ToString().Trim() + " as crbCUI from Ref_CUI where opTemp='" + optemp.ToString().Trim() + "'";
                SqlCommand cmd = new SqlCommand(sql1, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                rd.Read();
                double cr = 0.0;
                cr = Convert.ToDouble(rd["crbCUI"].ToString().Trim()) * Convert.ToDouble(cbo_CUI_Fins.SelectedValue.ToString().Trim()) * Convert.ToDouble(cbo_CUI_Fcm.SelectedValue.ToString().Trim()) * Convert.ToDouble(cbo_CUI_Fic.SelectedValue.ToString().Trim()) * Math.Max(Convert.ToDouble(cbo_CUI_Fps.SelectedItem.Text.ToString().Trim()), Convert.ToDouble(cbo_CUI_Fip.SelectedItem.Text.ToString().Trim()));
                txt_CUI_cr.Text = cr.ToString().Trim();
                rd.Close();
                BusinessTier.DisposeConnection(conn);
            }
            catch (Exception ex)
            {
                BusinessTier.DisposeConnection(conn);
                InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "OnSelectedIndexChanged_CUI_crb", ex.ToString(), "Audit");
                //lblStatus.Text = ex.Message.ToString();
                return;

            }

        }
    }

    protected void btn_CUI_Calc_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_CUI_InsEff.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_CUI_Agetk.Text.ToString().Trim()) || string.IsNullOrEmpty(dt_CUI_CalcDate.SelectedDate.ToString().Trim()) || string.IsNullOrEmpty(dt_CUI_CmpInstal.SelectedDate.ToString().Trim()) || string.IsNullOrEmpty(cbo_CUI_Fcm.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_CUI_Fic.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_CUI_Fins.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_CUI_Fip.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_CUI_Fps.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_CUI_Age.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_CUI_cr.Text.ToString().Trim()))
        {
            lblStatus.Text = "Please check all the  fields";
            lblStatus.ForeColor = Color.Red;
        }
        else
        {
            lblStatus.Text = string.Empty;
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            try
            {
                double tmin = Convert.ToDouble(cboComponent.SelectedItem.Attributes["MRT"].ToString());
                double CA = Convert.ToDouble(cboComponent.SelectedItem.Attributes["CorrosionAllownce"].ToString());
                string sql1 = "SELECT [ReadVal] as trd FROM [RBI].[dbo].[Tbl_EquipmentComponentDetails] where [CompAutoID]=" + cboComponent.SelectedValue.ToString().Trim() + " and Deleted=0";
                SqlCommand cmd = new SqlCommand(sql1, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                rd.Read();
                double trd = 0.0, ar = 0.0, artval = 0.0;
                trd = Convert.ToDouble(rd["trd"].ToString().Trim());
                ar = 1 - (trd - Convert.ToDouble(txt_CUI_cr.Text.ToString()) * Convert.ToDouble(txt_CUI_Age.Text.ToString()) / (tmin + CA));
                artval = Math.Max(ar, 0.0);
                txt_CUI_art.Text = artval.ToString().Trim();
                BusinessTier.DisposeReader(rd);

                double finalart = 0;
                if (artval >= 0.02 && artval < 0.04)
                {
                    finalart = 0.02;
                }
                else if (artval >= 0.04 && artval < 0.06)
                {
                    finalart = 0.04;
                }
                else if (artval >= 0.06 && artval < 0.08)
                {
                    finalart = 0.06;
                }
                else if (artval >= 0.08 && artval < 0.10)
                {
                    finalart = 0.08;
                }
                else if (artval >= 0.10 && artval < 0.12)
                {
                    finalart = 0.10;
                }
                else if (artval >= 0.12 && artval < 0.14)
                {
                    finalart = 0.12;
                }
                else if (artval >= 0.14 && artval < 0.16)
                {
                    finalart = 0.14;
                }
                else if (artval >= 0.16 && artval < 0.18)
                {
                    finalart = 0.16;
                }
                else if (artval >= 0.18 && artval < 0.20)
                {
                    finalart = 0.18;
                }
                else if (artval >= 0.20 && artval < 0.25)
                {
                    finalart = 0.20;
                }
                else if (artval >= 0.25 && artval < 0.30)
                {
                    finalart = 0.25;
                }
                else if (artval >= 0.30 && artval < 0.35)
                {
                    finalart = 0.30;
                }
                else if (artval >= 0.35 && artval < 0.40)
                {
                    finalart = 0.40;
                }
                else if (artval >= 0.40 && artval < 0.45)
                {
                    finalart = 0.40;
                }
                else if (artval >= 0.45 && artval < 0.50)
                {
                    finalart = 0.45;
                }
                else if (artval >= 0.50 && artval < 0.55)
                {
                    finalart = 0.50;
                }
                else if (artval >= 0.55 && artval < 0.60)
                {
                    finalart = 0.55;
                }
                else if (artval >= 0.60 && artval < 0.65)
                {
                    finalart = 0.60;
                }
                else if (artval >= 0.65)
                {
                    finalart = 0.65;
                }
                else
                {
                    finalart = 0.02;
                }


                string strqry2 = "select " + cbo_CUI_InsEff.SelectedValue.ToString().Trim() + " as CUI_inspect from Tbl_InspectionEffective where art= " + finalart + " and inspection=" + cbo_CUI_nofIns.SelectedValue.ToString().Trim() + " ";
                SqlCommand cmd1 = new SqlCommand(strqry2, conn);
                SqlDataReader rdr1 = cmd1.ExecuteReader();
                rdr1.Read();
                txt_CUI_Df.Text = rdr1["CUI_inspect"].ToString().Trim();
                BusinessTier.DisposeReader(rdr1);

                BusinessTier.DisposeConnection(conn);
            }
            catch (Exception ex)
            {
                BusinessTier.DisposeConnection(conn);
                // lblStatus.Text = ex.Message.ToString();
                InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn_CUI_Calc_Click", ex.ToString(), "Audit");
                return;

            }

        }
    }

    protected void btn_CUI_Save_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_CUI_InsEff.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_CUI_Agetk.Text.ToString().Trim()) || string.IsNullOrEmpty(dt_CUI_CalcDate.SelectedDate.ToString().Trim()) || string.IsNullOrEmpty(dt_CUI_CmpInstal.SelectedDate.ToString().Trim()) || string.IsNullOrEmpty(cbo_CUI_Fcm.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_CUI_Fic.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_CUI_Fins.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_CUI_Fip.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_CUI_Fps.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_CUI_art.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_CUI_Df.Text.ToString().Trim()))
        {
            lblStatus.Text = "Err : Please check all the fields";
            lblStatus.ForeColor = Color.Red;
        }
        else
        {

            try
            {
                if (btnCUISubmit.ToolTip == "Save")
                {
                    int intFlag = BusinessTier.CUISave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), Convert.ToInt32(txt_CUI_Agetk.Text.ToString().Trim()), cbo_CUI_InsEff.SelectedValue.ToString().Trim(), Convert.ToDateTime(dt_CUI_CmpInstal.SelectedDate.ToString().Trim()), Convert.ToDateTime(dt_CUI_CalcDate.SelectedDate.ToString().Trim()), Convert.ToInt32(cbo_CUI_nofIns.SelectedItem.Text.ToString().Trim()), Convert.ToDateTime(dt_CUI_InspectDate.SelectedDate.ToString().Trim()), cbo_CUI_coatqual.SelectedItem.Text.ToString().Trim(), Convert.ToDecimal(txt_CUI_Agecoat.Text.ToString().Trim()), Convert.ToDecimal(txt_CUI_Age.Text.ToString().Trim()), Convert.ToInt32(cbo_CUI_Fps.SelectedValue.ToString().Trim()), Convert.ToInt32(cbo_CUI_Fip.SelectedValue.ToString().Trim()), cbo_CUI_crdriver.SelectedItem.Text.ToString().Trim(), Convert.ToDecimal(txt_CUI_cr.Text.ToString().Trim()), Convert.ToDecimal(txt_CUI_art.Text.ToString().Trim()), Convert.ToDecimal(txt_CUI_Df.Text.ToString().Trim()), cbo_CUI_Fins.SelectedItem.Text.ToString().Trim(), cbo_CUI_Fcm.SelectedItem.Text.ToString().Trim(), cbo_CUI_Fic.SelectedItem.Text.ToString().Trim(), Convert.ToInt32(Session["sesCompanyID"].ToString()), Convert.ToInt32(Session["sesUserID"].ToString()), "N", 0);
                    InspectionPlan(Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "CUIDamage", cbo_CUI_InsEff.SelectedValue.ToString(), Convert.ToDateTime(dt_CUI_InspectDate.SelectedDate.ToString().Trim()));
                    lblStatus.Text = "Successfully CUIDamage Value Inserted";
                    lblStatus.ForeColor = Color.Green;
                }
                else if (btnCUISubmit.ToolTip == "Update")
                {
                    int intFlag = BusinessTier.CUISave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), Convert.ToInt32(txt_CUI_Agetk.Text.ToString().Trim()), cbo_CUI_InsEff.SelectedValue.ToString().Trim(), Convert.ToDateTime(dt_CUI_CmpInstal.SelectedDate.ToString().Trim()), Convert.ToDateTime(dt_CUI_CalcDate.SelectedDate.ToString().Trim()), Convert.ToInt32(cbo_CUI_nofIns.SelectedItem.Text.ToString().Trim()), Convert.ToDateTime(dt_CUI_InspectDate.SelectedDate.ToString().Trim()), cbo_CUI_coatqual.SelectedItem.Text.ToString().Trim(), Convert.ToDecimal(txt_CUI_Agecoat.Text.ToString().Trim()), Convert.ToDecimal(txt_CUI_Age.Text.ToString().Trim()), Convert.ToInt32(cbo_CUI_Fps.SelectedValue.ToString().Trim()), Convert.ToInt32(cbo_CUI_Fip.SelectedValue.ToString().Trim()), cbo_CUI_crdriver.SelectedItem.Text.ToString().Trim(), Convert.ToDecimal(txt_CUI_cr.Text.ToString().Trim()), Convert.ToDecimal(txt_CUI_art.Text.ToString().Trim()), Convert.ToDecimal(txt_CUI_Df.Text.ToString().Trim()), cbo_CUI_Fins.SelectedItem.Text.ToString().Trim(), cbo_CUI_Fcm.SelectedItem.Text.ToString().Trim(), cbo_CUI_Fic.SelectedItem.Text.ToString().Trim(), Convert.ToInt32(Session["sesCompanyID"].ToString()), Convert.ToInt32(Session["sesUserID"].ToString()), "U", Convert.ToInt32(btn_CUI_Save.ToolTip));
                    InspectionPlan(Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "CUIDamage", cbo_CUI_InsEff.SelectedValue.ToString(), Convert.ToDateTime(dt_CUI_InspectDate.SelectedDate.ToString().Trim()));
                    lblStatus.Text = "Successfully CUIDamage Value Updated";
                    lblStatus.ForeColor = Color.Green;
                }

                BusinessTier.DisposeConnection(conn);
                CUIClear();
                btn_CUI_Save.Enabled = false;
            }
            catch (Exception ex)
            {
                InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn_CUI_Save_Click", ex.ToString(), "Audit");
                // lblStatus.Text = ex.Message.ToString();
                return;
            }
            finally
            {
                BusinessTier.DisposeConnection(conn);
            }
        }
    }

    protected void dt_CUI_InspectDate_OnSelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
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

        int maxdate = 0;
        maxdate = Convert.ToInt32(dt_CUI_InspectDate.SelectedDate.Value.Year);

        txt_CUI_Agetk.Text = Agecal(maxdate, cboProcessArea.SelectedValue.ToString(), cboEquipment.SelectedValue.ToString(), cboComponent.SelectedValue.ToString(), Session["sesCompanyID"].ToString()).ToString();

    }

    //<-------------------External CLSCC------------------------>

    protected void OnSelectedIndexChanged_ExCLS_CoatQual(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_ExCLS_InsEff.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_ExCLS_Agetk.Text.ToString().Trim()) || string.IsNullOrEmpty(dt_ExCLS_CalcDate.SelectedDate.ToString().Trim()) || string.IsNullOrEmpty(dt_ExCLS_CmpInstal.SelectedDate.ToString().Trim()))
        {
            lblStatus.Text = "Please check all the  fields";
            lblStatus.ForeColor = Color.Red;
        }
        else
        {
            lblStatus.Text = string.Empty;
            DateTime CalcDt, Dt = DateTime.Today;
            TimeSpan SubDt;
            int agecoat = 0, calagecoat = 0;
            if (cbo_ExCLS_CoatQual.SelectedValue.ToString().Trim() == "1")
            {
                Dt = Convert.ToDateTime(dt_ExCLS_CmpInstal.SelectedDate.Value.ToString());
            }
            else if (cbo_ExCLS_CoatQual.SelectedValue.ToString().Trim() == "2")
            {
                Dt = Convert.ToDateTime(dt_ExCLS_CmpInstal.SelectedDate.Value.AddYears(5));
            }
            else if (cbo_ExCLS_CoatQual.SelectedValue.ToString().Trim() == "3")
            {
                Dt = Convert.ToDateTime(dt_ExCLS_CmpInstal.SelectedDate.Value.AddYears(15));
            }
            CalcDt = dt_ExCLS_CalcDate.SelectedDate.Value;
            SubDt = CalcDt.Subtract(Dt);
            calagecoat = Convert.ToInt32(SubDt.TotalDays.ToString()) / 365;
            agecoat = Math.Max(0, calagecoat);
            txt_ExCLS_Age.Text = agecoat.ToString();
        }
    }

    protected void OnSelectedIndexChanged_ExCLS_crb(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_ExCLS_InsEff.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_ExCLS_Agetk.Text.ToString().Trim()) || string.IsNullOrEmpty(dt_ExCLS_CalcDate.SelectedDate.ToString().Trim()) || string.IsNullOrEmpty(dt_ExCLS_CmpInstal.SelectedDate.ToString().Trim()))
        {
            lblStatus.Text = "Please check all the  fields";
            lblStatus.ForeColor = Color.Red;
        }
        else
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            try
            {
                lblStatus.Text = string.Empty;
                double optemp = Convert.ToDouble(cboComponent.SelectedItem.Attributes["OPTemp"].ToString());

                if (optemp < 49)
                {
                    optemp = 49;
                }
                else if (optemp >= 49 && optemp < 93)
                {
                    optemp = 93;
                }
                else if (optemp >= 93 && optemp < 149)
                {
                    optemp = 149;
                }
                else if (optemp >= 149)
                {
                    optemp = 150;
                }


                string sql1 = "select " + cbo_ExCLS_crdriver.SelectedValue.ToString().Trim() + " as crbExCLS from Ref_ExCLSCC where opTemp='" + optemp.ToString().Trim() + "'";
                SqlCommand cmd = new SqlCommand(sql1, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                rd.Read();
                txt_ExCLS_Svi.Text = rd["crbExCLS"].ToString().Trim();
                rd.Close();
                BusinessTier.DisposeConnection(conn);
            }
            catch (Exception ex)
            {
                BusinessTier.DisposeConnection(conn);
                //lblStatus.Text = ex.Message.ToString();
                return;

            }

        }
    }

    protected void btn_ExCLS_Calc_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_ExCLS_InsEff.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_ExCLS_Age.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_ExCLS_Svi.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_ExCLS_nofIns.Text.ToString().Trim()))
        {
            lblStatus.Text = "Please check all the  fields";
            lblStatus.ForeColor = Color.Red;
        }
        else
        {
            try
            {
                lblStatus.Text = string.Empty;
                SqlConnection conn = BusinessTier.getConnection();
                conn.Open();
                int svival = 0;
                if (txt_ExCLS_Svi.Text.ToString().Trim() == "High")
                {
                    svival = 50;
                }
                else if (txt_ExCLS_Svi.Text.ToString().Trim() == "Medium")
                {
                    svival = 10;
                }
                else if (txt_ExCLS_Svi.Text.ToString().Trim() == "Low")
                {
                    svival = 1;
                }
                else if (txt_ExCLS_Svi.Text.ToString().Trim() == "None")
                {
                    svival = 1;
                }
                string strqry = "select " + cbo_ExCLS_InsEff.SelectedValue.ToString().Trim() + " as dfExCLS from Ref_SCC where Inspection=" + cbo_ExCLS_nofIns.SelectedValue.ToString().Trim() + " and Svi=" + svival.ToString().Trim() + "";
                SqlCommand cmd = new SqlCommand(strqry, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                rd.Read();
                double df, dfb_ExCLS;
                df = Convert.ToInt32(rd["dfExCLS"].ToString().Trim());
                dfb_ExCLS = df * (Math.Pow(Convert.ToDouble(txt_ExCLS_Age.Text.ToString().Trim()), 1.1));
                txt_ExCLS_Df.Text = Math.Round(dfb_ExCLS, 3).ToString();
                rd.Close();
                BusinessTier.DisposeConnection(conn);

            }
            catch (Exception ex)
            {
                InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn_ExCLS_Calc_Click", ex.ToString(), "Audit");
                // lblStatus.Text = "Err: " + ex.Message.ToString();
            }
        }
    }

    protected void btn_ExCLS_Save_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_ExCLS_InsEff.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_ExCLS_Agetk.Text.ToString().Trim()) || string.IsNullOrEmpty(dt_ExCLS_CalcDate.SelectedDate.ToString().Trim()) || string.IsNullOrEmpty(dt_ExCLS_CmpInstal.SelectedDate.ToString().Trim()) || string.IsNullOrEmpty(txt_ExCLS_Svi.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_ExCLS_Df.Text.ToString().Trim()))
        {
            lblStatus.Text = "Err : Please check all the fields";
            lblStatus.ForeColor = Color.Red;
        }
        else
        {
            lblStatus.Text = string.Empty;
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            try
            {

                if (btnExCLSSubmit.ToolTip == "Save")
                {
                    int intFlag = BusinessTier.ExCLSSave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), Convert.ToInt32(txt_ExCLS_Agetk.Text.ToString().Trim()), cbo_ExCLS_InsEff.SelectedValue.ToString().Trim(), Convert.ToDateTime(dt_ExCLS_CmpInstal.SelectedDate.ToString().Trim()), Convert.ToDateTime(dt_ExCLS_CalcDate.SelectedDate.ToString().Trim()), Convert.ToInt32(cbo_ExCLS_nofIns.SelectedItem.Text.ToString().Trim()), Convert.ToDateTime(dt_ExCLS_InspectDate.SelectedDate.ToString().Trim()), cbo_ExCLS_CoatQual.Text.ToString().Trim(), Convert.ToDecimal(txt_ExCLS_Age.Text.ToString().Trim()), cbo_ExCLS_crdriver.Text.ToString().Trim(), txt_ExCLS_Svi.Text.ToString().Trim(), Convert.ToDecimal(txt_ExCLS_Df.Text.ToString().Trim()), Convert.ToInt32(Session["sesCompanyID"].ToString()), Convert.ToInt32(Session["sesUserID"].ToString()), "N", 0);
                    InspectionPlan(Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "ExternalCLSCC", cbo_ExCLS_InsEff.SelectedValue.ToString(), Convert.ToDateTime(dt_ExCLS_InspectDate.SelectedDate.ToString().Trim()));

                    lblStatus.Text = "Successfully ExternalCLSCC Value Inserted";
                    lblStatus.ForeColor = Color.Green;
                }
                else if (btnExCLSSubmit.ToolTip == "Update")
                {
                    //string a = Session["sesCompanyID"].ToString();
                    //string b = Session["sesUserID"].ToString();
                    int intFlag = BusinessTier.ExCLSSave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), Convert.ToInt32(txt_ExCLS_Agetk.Text.ToString().Trim()), cbo_ExCLS_InsEff.SelectedValue.ToString().Trim(), Convert.ToDateTime(dt_ExCLS_CmpInstal.SelectedDate.ToString().Trim()), Convert.ToDateTime(dt_ExCLS_CalcDate.SelectedDate.ToString().Trim()), Convert.ToInt32(cbo_ExCLS_nofIns.SelectedItem.Text.ToString().Trim()), Convert.ToDateTime(dt_ExCLS_InspectDate.SelectedDate.ToString().Trim()), cbo_ExCLS_CoatQual.Text.ToString().Trim(), Convert.ToDecimal(txt_ExCLS_Age.Text.ToString().Trim()), cbo_ExCLS_crdriver.Text.ToString().Trim(), txt_ExCLS_Svi.Text.ToString().Trim(), Convert.ToDecimal(txt_ExCLS_Df.Text.ToString().Trim()), Convert.ToInt32(Session["sesCompanyID"].ToString()), Convert.ToInt32(Session["sesUserID"].ToString()), "U", Convert.ToInt32(btn_ExCLS_Save.ToolTip));
                    InspectionPlan(Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "ExternalCLSCC", cbo_ExCLS_InsEff.SelectedValue.ToString(), Convert.ToDateTime(dt_ExCLS_InspectDate.SelectedDate.ToString().Trim()));
                    lblStatus.Text = "Successfully ExternalCLSCC Value Updated";
                    lblStatus.ForeColor = Color.Green;
                }

                BusinessTier.DisposeConnection(conn);
                ExCLSClear();
                btn_ExCLS_Save.Enabled = false;

            }
            catch (Exception ex)
            {
                BusinessTier.DisposeConnection(conn);
                InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn_ExCLS_Save_Click", ex.ToString(), "Audit");
                //lblStatus.Text = ex.Message.ToString();
                return;

            }
        }
    }

    protected void dt_ExCLS_InspectDate_OnSelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
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

        int maxdate = 0;
        maxdate = Convert.ToInt32(dt_ExCLS_InspectDate.SelectedDate.Value.Year);

        txt_ExCLS_Agetk.Text = Agecal(maxdate, cboProcessArea.SelectedValue.ToString(), cboEquipment.SelectedValue.ToString(), cboComponent.SelectedValue.ToString(), Session["sesCompanyID"].ToString()).ToString();

        //string str2 = "SELECT yearinstalled as mindate FROM [Tbl_EquipmentAsset] where deleted=0 and [ProcessareaID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquAutoID]='" + cboEquipment.SelectedValue.ToString() + "'";
        //SqlCommand cmd2 = new SqlCommand(str2, conn);
        //SqlDataReader rdr2 = cmd2.ExecuteReader();
        //int mindate = 0, caldate = 0;
        //if (rdr2.Read())
        //{
        //    mindate = Convert.ToInt32(rdr2["mindate"].ToString());
        //}
        //rdr2.Close();
        //caldate = maxdate - mindate;
        //txt_Age.Text = caldate.ToString();
        //BusinessTier.DisposeConnection(conn);
    }



    //<-------------------External CUI------------------------>

    protected void OnSelectedIndexChanged_ExCUI_CoatQual(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_ExCUI_InsEff.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_ExCUI_Agetk.Text.ToString().Trim()) || string.IsNullOrEmpty(dt_ExCUI_CalcDate.SelectedDate.ToString().Trim()) || string.IsNullOrEmpty(dt_ExCUI_CmpInstal.SelectedDate.ToString().Trim()))
            {
                lblStatus.Text = "Please check all the  fields";
                lblStatus.ForeColor = Color.Red;
            }
            else
            {
                lblStatus.Text = string.Empty;
                DateTime CalcDt, Dt = DateTime.Today;
                TimeSpan SubDt;
                int agecoat = 0, calagecoat = 0;
                if (cbo_ExCUI_CoatQual.SelectedValue.ToString().Trim() == "1")
                {
                    Dt = Convert.ToDateTime(dt_ExCUI_CmpInstal.SelectedDate.Value.ToString());
                }
                else if (cbo_ExCUI_CoatQual.SelectedValue.ToString().Trim() == "2")
                {
                    Dt = Convert.ToDateTime(dt_ExCUI_CmpInstal.SelectedDate.Value.AddYears(5));
                }
                else if (cbo_ExCUI_CoatQual.SelectedValue.ToString().Trim() == "3")
                {
                    Dt = Convert.ToDateTime(dt_ExCUI_CmpInstal.SelectedDate.Value.AddYears(15));
                }
                CalcDt = dt_ExCUI_CalcDate.SelectedDate.Value;
                SubDt = CalcDt.Subtract(Dt);
                calagecoat = Convert.ToInt32(SubDt.TotalDays.ToString()) / 365;
                agecoat = Math.Max(0, calagecoat);
                txt_ExCUI_Age.Text = agecoat.ToString();
            }
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "OnSelectedIndexChanged_ExCUI_CoatQual", ex.ToString(), "Audit");
        }
    }

    protected void OnSelectedIndexChanged_ExCUI_crb(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_ExCUI_InsEff.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_ExCUI_Agetk.Text.ToString().Trim()) || string.IsNullOrEmpty(dt_ExCUI_CalcDate.SelectedDate.ToString().Trim()) || string.IsNullOrEmpty(dt_ExCUI_CmpInstal.SelectedDate.ToString().Trim()))
        {
            lblStatus.Text = "Please check all the  fields";
            lblStatus.ForeColor = Color.Red;
        }
        else
        {
            sviload();

        }
    }

    protected void sviload()
    {

        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            double optemp = Convert.ToDouble(cboComponent.SelectedItem.Attributes["OPTemp"].ToString());

            if (optemp < 49)
            {
                optemp = 49;
            }
            else if (optemp >= 49 && optemp < 93)
            {
                optemp = 93;
            }
            else if (optemp >= 93 && optemp < 149)
            {
                optemp = 149;
            }
            else if (optemp >= 149)
            {
                optemp = 150;
            }

            lblStatus.Text = string.Empty;
            string sql1 = "select " + cbo_ExCUI_Area.SelectedValue.ToString().Trim() + " as AreaExCUI from Ref_ExCUI where opTemp='" + optemp.ToString().Trim() + "'";
            SqlCommand cmd = new SqlCommand(sql1, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            rd.Read();
            txt_ExCUI_Svi.Text = rd["AreaExCUI"].ToString().Trim();
            rd.Close();
            BusinessTier.DisposeConnection(conn);
        }
        catch (Exception ex)
        {
            BusinessTier.DisposeConnection(conn);
            // lblStatus.Text = ex.Message.ToString();
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "sviload", ex.ToString(), "Audit");
            return;

        }
    }

    protected void OnSelectedIndexChanged_ExCUI_Pip(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        try
        {
            string svival = txt_ExCUI_Svi.Text.ToString().Trim();
            cbo_ExCUI_InsCon.Enabled = true;
            if (Convert.ToInt32(cbo_ExCUI_Pip.SelectedValue.ToString()) == 1)
            {
                if (svival == "High")
                {
                    txt_ExCUI_Svi.Text = "Medium";

                }
                else if (svival == "Medium")
                {
                    txt_ExCUI_Svi.Text = "Low";

                }
                else if (svival == "Low")
                {
                    txt_ExCUI_Svi.Text = "None";

                }
                else if (svival == "None")
                {
                    txt_ExCUI_Svi.Text = "None";

                }
            }
            else if (Convert.ToInt32(cbo_ExCUI_Pip.SelectedValue.ToString()) == 2)
            {

                txt_ExCUI_Svi.Text = svival;

            }
            else if (Convert.ToInt32(cbo_ExCUI_Pip.SelectedValue.ToString()) == 3)
            {
                if (svival == "High")
                {
                    txt_ExCUI_Svi.Text = "High";

                }
                else if (svival == "Medium")
                {
                    txt_ExCUI_Svi.Text = "High";

                }
                else if (svival == "Low")
                {
                    txt_ExCUI_Svi.Text = "Medium";

                }
                else if (svival == "None")
                {
                    txt_ExCUI_Svi.Text = "Low";

                }
            }
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "OnSelectedIndexChanged_ExCUI_Pip", ex.ToString(), "Audit");
        }
    }

    protected void OnSelectedIndexChanged_ExCUI_insCon(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        try
        {
            cbo_ExCUI_ChlrFree.Enabled = true;
            string svival1 = txt_ExCUI_Svi.Text.ToString().Trim();
            if (Convert.ToInt32(cbo_ExCUI_InsCon.SelectedValue.ToString()) == 1)
            {
                if (svival1 == "High")
                {
                    txt_ExCUI_Svi.Text = "Medium";

                }
                else if (svival1 == "Medium")
                {
                    txt_ExCUI_Svi.Text = "Low";

                }
                else if (svival1 == "Low")
                {
                    txt_ExCUI_Svi.Text = "None";

                }
                else if (svival1 == "None")
                {
                    txt_ExCUI_Svi.Text = "None";

                }
            }
            else if (Convert.ToInt32(cbo_ExCUI_InsCon.SelectedValue.ToString()) == 2)
            {

                txt_ExCUI_Svi.Text = svival1;

            }
            else if (Convert.ToInt32(cbo_ExCUI_InsCon.SelectedValue.ToString()) == 3)
            {
                if (svival1 == "High")
                {
                    txt_ExCUI_Svi.Text = "High";

                }
                else if (svival1 == "Medium")
                {
                    txt_ExCUI_Svi.Text = "High";

                }
                else if (svival1 == "Low")
                {
                    txt_ExCUI_Svi.Text = "Medium";

                }
                else if (svival1 == "None")
                {
                    txt_ExCUI_Svi.Text = "Low";

                }
            }
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "OnSelectedIndexChanged_ExCUI_insCon", ex.ToString(), "Audit");
        }
    }

    protected void OnSelectedIndexChanged_ExCUI_ChlrFree(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        string svival1 = txt_ExCUI_Svi.Text.ToString().Trim();
        if (Convert.ToInt32(cbo_ExCUI_ChlrFree.SelectedValue.ToString()) == 1)
        {
            if (svival1 == "High")
            {
                txt_ExCUI_Svi.Text = "Medium";

            }
            else if (svival1 == "Medium")
            {
                txt_ExCUI_Svi.Text = "Low";

            }
            else if (svival1 == "Low")
            {
                txt_ExCUI_Svi.Text = "None";

            }
            else if (svival1 == "None")
            {
                txt_ExCUI_Svi.Text = "None";

            }
        }
        else if (Convert.ToInt32(cbo_ExCUI_ChlrFree.SelectedValue.ToString()) == 2)
        {

            txt_ExCUI_Svi.Text = svival1;

        }
    }

    protected void btn_ExCUI_Calc_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_ExCUI_InsEff.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_ExCUI_Age.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_ExCUI_Svi.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_ExCUI_nofIns.Text.ToString().Trim()))
        {
            lblStatus.Text = "Please check all the  fields";
            lblStatus.ForeColor = Color.Red;
        }
        else
        {
            try
            {
                lblStatus.Text = string.Empty;
                SqlConnection conn = BusinessTier.getConnection();
                conn.Open();
                int svival = 0;
                if (txt_ExCUI_Svi.Text.ToString().Trim() == "High")
                {
                    svival = 50;
                }
                else if (txt_ExCUI_Svi.Text.ToString().Trim() == "Medium")
                {
                    svival = 10;
                }
                else if (txt_ExCUI_Svi.Text.ToString().Trim() == "Low")
                {
                    svival = 1;
                }
                else if (txt_ExCUI_Svi.Text.ToString().Trim() == "None")
                {
                    svival = 1;
                }
                string strqry = "select " + cbo_ExCUI_InsEff.SelectedValue.ToString().Trim() + " as dfExCUI from Ref_SCC where Inspection=" + cbo_ExCUI_nofIns.SelectedValue.ToString().Trim() + " and Svi=" + svival.ToString().Trim() + "";
                SqlCommand cmd = new SqlCommand(strqry, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                rd.Read();
                double df, dfb_ExCUI;
                df = Convert.ToInt32(rd["dfExCUI"].ToString().Trim());
                dfb_ExCUI = df * (Math.Pow(Convert.ToDouble(txt_ExCUI_Age.Text.ToString().Trim()), 1.1));
                txt_ExCUI_Df.Text = Math.Round(dfb_ExCUI, 3).ToString();
                rd.Close();
                BusinessTier.DisposeConnection(conn);

            }
            catch (Exception ex)
            {
                InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn_ExCUI_Calc_Click", ex.ToString(), "Audit");
                // lblStatus.Text = "Err: " + ex.Message.ToString();
            }
        }
    }

    protected void btn_ExCUI_Save_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_ExCUI_InsEff.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_ExCUI_Age.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_ExCUI_Svi.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_ExCUI_nofIns.Text.ToString().Trim()))
        {
            lblStatus.Text = "Err : Please check all the fields";
            lblStatus.ForeColor = Color.Red;
        }
        else
        {
            lblStatus.Text = string.Empty;
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            try
            {

                if (btnExCUISubmit.ToolTip == "Save")
                {
                    int intFlag = BusinessTier.ExCUISave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), Convert.ToInt32(txt_ExCUI_Agetk.Text.ToString().Trim()), cbo_ExCUI_InsEff.SelectedValue.ToString().Trim(), Convert.ToDateTime(dt_ExCUI_CmpInstal.SelectedDate.ToString().Trim()), Convert.ToDateTime(dt_ExCUI_CalcDate.SelectedDate.ToString().Trim()), Convert.ToInt32(cbo_ExCUI_nofIns.SelectedItem.Text.ToString().Trim()), Convert.ToDateTime(dt_ExCUI_InspectDate.SelectedDate.ToString().Trim()), cbo_ExCUI_CoatQual.Text.ToString().Trim(), Convert.ToDecimal(txt_ExCUI_Age.Text.ToString().Trim()), cbo_ExCUI_Area.Text.ToString().Trim(), cbo_ExCUI_Pip.Text.ToString().Trim(), cbo_ExCUI_InsCon.SelectedItem.Text.ToString().Trim(), cbo_ExCUI_ChlrFree.Text.ToString().Trim(), txt_ExCUI_Svi.Text.ToString().Trim(), Convert.ToDecimal(txt_ExCUI_Df.Text.ToString().Trim()), Convert.ToInt32(Session["sesCompanyID"].ToString()), Convert.ToInt32(Session["sesUserID"].ToString()), "N", 0);
                    InspectionPlan(Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "External CUI CLSCC", cbo_ExCUI_InsEff.SelectedValue.ToString(), Convert.ToDateTime(dt_ExCUI_InspectDate.SelectedDate.ToString().Trim()));
                    lblStatus.Text = "Successfully External CUI CLSCC Value Inserted";
                    lblStatus.ForeColor = Color.Green;
                }
                else if (btnExCUISubmit.ToolTip == "Update")
                {
                    int intFlag = BusinessTier.ExCUISave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), Convert.ToInt32(txt_ExCUI_Agetk.Text.ToString().Trim()), cbo_ExCUI_InsEff.SelectedValue.ToString().Trim(), Convert.ToDateTime(dt_ExCUI_CmpInstal.SelectedDate.ToString().Trim()), Convert.ToDateTime(dt_ExCUI_CalcDate.SelectedDate.ToString().Trim()), Convert.ToInt32(cbo_ExCUI_nofIns.SelectedItem.Text.ToString().Trim()), Convert.ToDateTime(dt_ExCUI_InspectDate.SelectedDate.ToString().Trim()), cbo_ExCUI_CoatQual.Text.ToString().Trim(), Convert.ToDecimal(txt_ExCUI_Age.Text.ToString().Trim()), cbo_ExCUI_Area.Text.ToString().Trim(), cbo_ExCUI_Pip.Text.ToString().Trim(), cbo_ExCUI_InsCon.SelectedItem.Text.ToString().Trim(), cbo_ExCUI_ChlrFree.Text.ToString().Trim(), txt_ExCUI_Svi.Text.ToString().Trim(), Convert.ToDecimal(txt_ExCUI_Df.Text.ToString().Trim()), Convert.ToInt32(Session["sesCompanyID"].ToString()), Convert.ToInt32(Session["sesUserID"].ToString()), "U", Convert.ToInt32(btn_ExCUI_Save.ToolTip));
                    InspectionPlan(Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "External CUI CLSCC", cbo_ExCUI_InsEff.SelectedValue.ToString(), Convert.ToDateTime(dt_ExCUI_InspectDate.SelectedDate.ToString().Trim()));
                    lblStatus.Text = "Successfully External CUI CLSCC Value Updated";
                    lblStatus.ForeColor = Color.Green;
                }

                BusinessTier.DisposeConnection(conn);
                ExCUIClear();
                btn_ExCUI_Save.Enabled = false;

            }
            catch (Exception ex)
            {
                InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn_ExCUI_Save_Click", ex.ToString(), "Audit");
                // lblStatus.Text = ex.Message.ToString();
                return;

            }
            finally
            {
                BusinessTier.DisposeConnection(conn);
            }
        }
    }

    protected void dt_ExCUI_InspectDate_OnSelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
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

        int maxdate = 0;
        maxdate = Convert.ToInt32(dt_ExCUI_InspectDate.SelectedDate.Value.Year);

        txt_ExCUI_Agetk.Text = Agecal(maxdate, cboProcessArea.SelectedValue.ToString(), cboEquipment.SelectedValue.ToString(), cboComponent.SelectedValue.ToString(), Session["sesCompanyID"].ToString()).ToString();

        //string str2 = "SELECT yearinstalled as mindate FROM [Tbl_EquipmentAsset] where deleted=0 and [ProcessareaID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquAutoID]='" + cboEquipment.SelectedValue.ToString() + "'";
        //SqlCommand cmd2 = new SqlCommand(str2, conn);
        //SqlDataReader rdr2 = cmd2.ExecuteReader();
        //int mindate = 0, caldate = 0;
        //if (rdr2.Read())
        //{
        //    mindate = Convert.ToInt32(rdr2["mindate"].ToString());
        //}
        //rdr2.Close();
        //caldate = maxdate - mindate;
        //txt_Age.Text = caldate.ToString();
        //BusinessTier.DisposeConnection(conn);
    }

    //<-------------------HTHA-DF------------------------>

    protected void btn_HTHA_Pv_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_HTHA_Age.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_HTHA_Extemp.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_HTHA_ph2.Text.ToString().Trim()))
        {
            lblStatus.Text = "Please check all the  fields";
            lblStatus.ForeColor = Color.Red;
        }
        else
        {
            try
            {
                lblStatus.Text = string.Empty;
                double Pv, Val, Val1, Val2, Val3;
                Val = Math.Log(Convert.ToDouble(txt_HTHA_ph2.Text.ToString().Trim()) / 0.0979);
                Val1 = 3.09 * Math.Pow(10, -4);
                Val2 = Convert.ToDouble(txt_HTHA_Extemp.Text.ToString().Trim()) + 273;
                Val3 = Math.Log(Convert.ToDouble(txt_HTHA_Age.Text.ToString().Trim())) + 14;
                Pv = (Val + Val2) * Val2 * Val3;
                txt_HTHA_pv.Text = Pv.ToString().Trim();
            }
            catch (Exception ex)
            {
                // lblStatus.Text = "Err: " + ex.Message.ToString();
            }
        }
    }

    protected void OnSelectedIndexChanged_HTHA_mat(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_HTHA_Age.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_HTHA_Extemp.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_HTHA_ph2.Text.ToString().Trim()))
        {
            lblStatus.Text = "Please check all the  fields";
            lblStatus.ForeColor = Color.Red;
        }
        else
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            try
            {
                double pval = 0.0;
                if (cbo_HTHA_mat.SelectedValue.ToString().Trim() == "CS")
                {
                    if (Convert.ToDouble(txt_HTHA_pv.Text.ToString().Trim()) > 4.70)
                    {
                        pval = 4.70;
                    }
                    else if (Convert.ToDouble(txt_HTHA_pv.Text.ToString().Trim()) >= 4.70 && Convert.ToDouble(txt_HTHA_pv.Text.ToString().Trim()) > 4.61)
                    {
                        pval = 4.61;
                    }
                    else if (Convert.ToDouble(txt_HTHA_pv.Text.ToString().Trim()) >= 4.61 && Convert.ToDouble(txt_HTHA_pv.Text.ToString().Trim()) > 4.53)
                    {
                        pval = 4.53;
                    }
                    else if (Convert.ToDouble(txt_HTHA_pv.Text.ToString().Trim()) <= 4.53)
                    {
                        pval = 4.54;
                    }
                }
                else if (cbo_HTHA_mat.SelectedValue.ToString().Trim() == "Mo")
                {
                    if (Convert.ToDouble(txt_HTHA_pv.Text.ToString().Trim()) > 4.95)
                    {
                        pval = 4.95;
                    }
                    else if (Convert.ToDouble(txt_HTHA_pv.Text.ToString().Trim()) >= 4.95 && Convert.ToDouble(txt_HTHA_pv.Text.ToString().Trim()) > 4.87)
                    {
                        pval = 4.87;
                    }
                    else if (Convert.ToDouble(txt_HTHA_pv.Text.ToString().Trim()) >= 4.87 && Convert.ToDouble(txt_HTHA_pv.Text.ToString().Trim()) > 4.78)
                    {
                        pval = 4.78;
                    }
                    else if (Convert.ToDouble(txt_HTHA_pv.Text.ToString().Trim()) <= 4.78)
                    {
                        pval = 4.79;
                    }
                }
                else if (cbo_HTHA_mat.SelectedValue.ToString().Trim() == "MN")
                {
                    if (Convert.ToDouble(txt_HTHA_pv.Text.ToString().Trim()) > 5.60)
                    {
                        pval = 5.60;
                    }
                    else if (Convert.ToDouble(txt_HTHA_pv.Text.ToString().Trim()) >= 5.60 && Convert.ToDouble(txt_HTHA_pv.Text.ToString().Trim()) > 5.51)
                    {
                        pval = 5.51;
                    }
                    else if (Convert.ToDouble(txt_HTHA_pv.Text.ToString().Trim()) >= 5.51 && Convert.ToDouble(txt_HTHA_pv.Text.ToString().Trim()) > 5.43)
                    {
                        pval = 5.43;
                    }
                    else if (Convert.ToDouble(txt_HTHA_pv.Text.ToString().Trim()) <= 5.43)
                    {
                        pval = 5.44;
                    }
                }
                else if (cbo_HTHA_mat.SelectedValue.ToString().Trim() == "1cr")
                {
                    if (Convert.ToDouble(txt_HTHA_pv.Text.ToString().Trim()) > 5.80)
                    {
                        pval = 5.80;
                    }
                    else if (Convert.ToDouble(txt_HTHA_pv.Text.ToString().Trim()) >= 5.80 && Convert.ToDouble(txt_HTHA_pv.Text.ToString().Trim()) > 5.71)
                    {
                        pval = 5.71;
                    }
                    else if (Convert.ToDouble(txt_HTHA_pv.Text.ToString().Trim()) >= 5.71 && Convert.ToDouble(txt_HTHA_pv.Text.ToString().Trim()) > 5.63)
                    {
                        pval = 5.43;
                    }
                    else if (Convert.ToDouble(txt_HTHA_pv.Text.ToString().Trim()) <= 5.63)
                    {
                        pval = 5.64;
                    }
                }
                else if (cbo_HTHA_mat.SelectedValue.ToString().Trim() == "125cr")
                {
                    if (Convert.ToDouble(txt_HTHA_pv.Text.ToString().Trim()) > 6.00)
                    {
                        pval = 6.00;
                    }
                    else if (Convert.ToDouble(txt_HTHA_pv.Text.ToString().Trim()) >= 6.00 && Convert.ToDouble(txt_HTHA_pv.Text.ToString().Trim()) > 5.92)
                    {
                        pval = 5.92;
                    }
                    else if (Convert.ToDouble(txt_HTHA_pv.Text.ToString().Trim()) >= 5.92 && Convert.ToDouble(txt_HTHA_pv.Text.ToString().Trim()) > 5.83)
                    {
                        pval = 5.43;
                    }
                    else if (Convert.ToDouble(txt_HTHA_pv.Text.ToString().Trim()) <= 5.83)
                    {
                        pval = 5.84;
                    }
                }
                else if (cbo_HTHA_mat.SelectedValue.ToString().Trim() == "225cr")
                {
                    if (Convert.ToDouble(txt_HTHA_pv.Text.ToString().Trim()) > 6.53)
                    {
                        pval = 6.53;
                    }
                    else if (Convert.ToDouble(txt_HTHA_pv.Text.ToString().Trim()) >= 6.53 && Convert.ToDouble(txt_HTHA_pv.Text.ToString().Trim()) > 6.45)
                    {
                        pval = 6.45;
                    }
                    else if (Convert.ToDouble(txt_HTHA_pv.Text.ToString().Trim()) >= 6.45 && Convert.ToDouble(txt_HTHA_pv.Text.ToString().Trim()) > 6.36)
                    {
                        pval = 5.43;
                    }
                    else if (Convert.ToDouble(txt_HTHA_pv.Text.ToString().Trim()) <= 6.36)
                    {
                        pval = 6.37;
                    }
                }
                lblStatus.Text = string.Empty;
                string sql1 = "select Susceptibility as Sus from Ref_HTHA where Pv=" + pval.ToString().Trim() + " and Material='" + cbo_HTHA_mat.SelectedValue.ToString().Trim() + "'";
                SqlCommand cmd = new SqlCommand(sql1, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                rd.Read();
                txt_HTHA_svi.Text = rd["Sus"].ToString().Trim();
                rd.Close();
                BusinessTier.DisposeConnection(conn);


            }
            catch (Exception ex)
            {
                BusinessTier.DisposeConnection(conn);
                // lblStatus.Text = ex.Message.ToString();
                return;
            }
        }
    }

    protected void chk_HTHA_Damage_CheckedChanged(object sender, EventArgs e)
    {
        txt_HTHA_Df.Text = string.Empty;
    }

    protected void btn_HTHA_Calc_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_HTHA_Age.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_HTHA_Extemp.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_HTHA_ph2.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_HTHA_svi.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_HTHA_1st2ndIns.Text.ToString().Trim()))
        {
            lblStatus.Text = "Please check all the  fields";
            lblStatus.ForeColor = Color.Red;
        }
        else
        {
            lblStatus.Text = string.Empty;
            try
            {
                if (chk_HTHA_Damage.Checked)
                {

                    txt_HTHA_Df.Text = "2000";
                }
                else
                {

                    SqlConnection conn = BusinessTier.getConnection();
                    conn.Open();
                    string strqry = "select " + cbo_HTHA_insEff.SelectedValue.ToString().Trim() + " as dfHTHA from Ref_HTHA_Df where Inspection=" + cbo_HTHA_1st2ndIns.SelectedValue.ToString().Trim() + " and Susceptibility='" + txt_HTHA_svi.Text.ToString().Trim() + "'";
                    SqlCommand cmd = new SqlCommand(strqry, conn);
                    SqlDataReader rd = cmd.ExecuteReader();
                    rd.Read();
                    txt_HTHA_Df.Text = rd["dfHTHA"].ToString().Trim();
                    rd.Close();
                    BusinessTier.DisposeConnection(conn);
                }
            }
            catch (Exception ex)
            {
                // lblStatus.Text = "Err: " + ex.Message.ToString();
            }
        }
    }

    protected void btn_HTHA_Save_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_HTHA_Age.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_HTHA_Extemp.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_HTHA_ph2.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_HTHA_svi.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_HTHA_1st2ndIns.Text.ToString().Trim()))
        {
            lblStatus.Text = "Err : Please check all the fields";
            lblStatus.ForeColor = Color.Red;
        }
        else
        {
            lblStatus.Text = string.Empty;
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            try
            {


                if (btnHTHASubmit.ToolTip == "Save")
                {
                    int intFlag = BusinessTier.HTHASave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), cbo_HTHA_insEff.SelectedValue.ToString().Trim(), Convert.ToInt32(cbo_HTHA_nofIns.SelectedValue.ToString().Trim()), Convert.ToDateTime(dt_HTHA_InspectDate.SelectedDate.ToString().Trim()), Convert.ToDouble(txt_HTHA_Age.Text.ToString().Trim()), Convert.ToDouble(txt_HTHA_Extemp.Text.ToString().Trim()), cbo_HTHA_Heat.SelectedItem.Text.ToString().Trim(), Convert.ToDouble(txt_HTHA_ph2.Text.ToString().Trim()), Convert.ToDouble(txt_HTHA_pv.Text.ToString().Trim()), cbo_HTHA_mat.SelectedItem.Text.ToString().Trim(), txt_HTHA_svi.Text.ToString().Trim(), cbo_HTHA_1st2ndIns.SelectedItem.Text.ToString().Trim(), Convert.ToInt32(txt_HTHA_Df.Text.ToString().Trim()), Convert.ToInt32(Session["sesUserID"].ToString()), "N", 0);
                    InspectionPlan(Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "HTHA", cbo_CUI_InsEff.SelectedValue.ToString(), Convert.ToDateTime(dt_CUI_InspectDate.SelectedDate.ToString().Trim()));
                    lblStatus.Text = "Successfully HTHA Damage Factor Value Inserted";
                    lblStatus.ForeColor = Color.Green;
                }
                else if (btnHTHASubmit.ToolTip == "Update")
                {
                    int intFlag = BusinessTier.HTHASave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), cbo_HTHA_insEff.SelectedValue.ToString().Trim(), Convert.ToInt32(cbo_HTHA_nofIns.SelectedValue.ToString().Trim()), Convert.ToDateTime(dt_HTHA_InspectDate.SelectedDate.ToString().Trim()), Convert.ToDouble(txt_HTHA_Age.Text.ToString().Trim()), Convert.ToDouble(txt_HTHA_Extemp.Text.ToString().Trim()), cbo_HTHA_Heat.SelectedItem.Text.ToString().Trim(), Convert.ToDouble(txt_HTHA_ph2.Text.ToString().Trim()), Convert.ToDouble(txt_HTHA_pv.Text.ToString().Trim()), cbo_HTHA_mat.SelectedItem.Text.ToString().Trim(), txt_HTHA_svi.Text.ToString().Trim(), cbo_HTHA_1st2ndIns.SelectedItem.Text.ToString().Trim(), Convert.ToInt32(txt_HTHA_Df.Text.ToString().Trim()), Convert.ToInt32(Session["sesUserID"].ToString()), "U", Convert.ToInt32(btn_HTHA_Save.ToolTip));
                    InspectionPlan(Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), "HTHA", cbo_CUI_InsEff.SelectedValue.ToString(), Convert.ToDateTime(dt_CUI_InspectDate.SelectedDate.ToString().Trim()));
                    lblStatus.Text = "Successfully HTHA Damage Factor Value Updated";
                    lblStatus.ForeColor = Color.Green;
                }

                BusinessTier.DisposeConnection(conn);
                HTHAClear();
                btn_HTHA_Save.Enabled = false;

            }
            catch (Exception ex)
            {
                InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn_HTHA_Save_Click", ex.ToString(), "Audit");
                lblStatus.Text = "Err:" + ex.Message.ToString();
            }
            finally
            {
                BusinessTier.DisposeConnection(conn);
            }
        }
    }

    //<-------------------Brittle Fracture------------------------>

    protected void OnSelectedIndexChanged_Brittle_fulpressure(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()))
            {
                lblStatus.Text = "Err : Please Select Process,Component & Equipment fields";
                lblStatus.ForeColor = Color.Red;
            }
            else
            {
                lblStatus.Text = string.Empty;
                txt_Britt_OpTemp.Text = cboComponent.SelectedItem.Attributes["OPTemp"].ToString();
                if (cbo_Brittle_fulpressure.SelectedItem.Text.ToString() == "Yes")
                {
                    lbl_Brittle_Mintemp.Text = "Minimum Temp:";

                }
                else
                {
                    lbl_Brittle_Mintemp.Text = "Min_Design Temp:";
                }
            }
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "OnSelectedIndexChanged_Brittle_fulpressure", ex.ToString(), "Audit");
            lblStatus.Text = "Err:" + ex.Message.ToString();
        }

    }

    protected void OnSelectedIndexChanged_cbo_Britt_PWHT(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            lblStatus.Text = string.Empty;
            if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_Britt_Tmin.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_Britt_Tref.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Britt_CmpThick.SelectedItem.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Britt_PWHT.SelectedItem.Text.ToString().Trim()))
            {
                lblStatus.Text = "Err : Please check all the fields";
                lblStatus.ForeColor = Color.Red;
            }
            else
            {

                int c = Convert.ToInt32(txt_Britt_Tmin.Text.ToString().Trim()) - Convert.ToInt32(txt_Britt_Tref.Text.ToString().Trim());

                if (c <= 38 && c > 27)
                {
                    c = 38;
                }
                else if (c <= 27 && c > 16)
                {
                    c = 27;
                }
                else if (c <= 16 && c > 4)
                {
                    c = 16;
                }
                else if (c <= 4 && c > -7)
                {
                    c = 4;
                }
                else if (c <= -7 && c > -18)
                {
                    c = -7;
                }
                else if (c <= -18 && c > -29)
                {
                    c = -18;
                }
                else if (c <= -29 && c > -40)
                {
                    c = -29;
                }
                else if (c <= -40 && c > -51)
                {
                    c = -40;
                }
                else if (c <= -51 && c > -62)
                {
                    c = -51;
                }
                else if (c <= -62 && c > -73)
                {
                    c = -62;
                }
                else if (c <= -73)
                {
                    c = -73;
                }
                else
                {
                    c = 38;
                }

                string strqryH2S = "select " + cbo_Britt_CmpThick.SelectedValue.ToString().Trim() + " as dfbBrit from " + cbo_Britt_PWHT.SelectedValue.ToString().Trim() + " where C=" + c + "";
                SqlCommand cmdH2S = new SqlCommand(strqryH2S, conn);
                SqlDataReader rdH2S = cmdH2S.ExecuteReader();
                rdH2S.Read();
                txt_Britt_Dfb.Text = rdH2S["dfbBrit"].ToString().Trim();
                rdH2S.Close();
                BusinessTier.DisposeConnection(conn);
            }
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "OnSelectedIndexChanged_cbo_Britt_PWHT", ex.ToString(), "Audit");
            lblStatus.Text = "Err:" + ex.Message.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }

    }

    protected void btn_Brittle_Tmin_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_Britt_Dsgn.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_Britt_ATM.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_Britt_OpTemp.Text.ToString().Trim()))
            {
                lblStatus.Text = "Err : Please Check all the fields";
                lblStatus.ForeColor = Color.Red;
            }
            else
            {
                lblStatus.Text = string.Empty;
                int t = Math.Min(Convert.ToInt32(txt_Britt_OpTemp.Text.ToString()), Convert.ToInt32(txt_Britt_Dsgn.Text.ToString()));
                int tmin = Math.Min(t, Convert.ToInt32(txt_Britt_ATM.Text.ToString()));
                txt_Britt_Tmin.Text = tmin.ToString();
            }
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn_Brittle_Tmin_Click", ex.ToString(), "Audit");
            lblStatus.Text = "Err:" + ex.Message.ToString();
        }
    }

    protected void btn_Britt_Calc_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txt_Britt_Dfb.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Britt_FSE.SelectedItem.Text.ToString().Trim()))
            {
            }
            else
            {
                decimal Dfb = Convert.ToDecimal(txt_Britt_Dfb.Text.ToString().Trim()) * Convert.ToDecimal(cbo_Britt_FSE.SelectedValue.ToString().Trim());
                txt_Britt_Df.Text = Dfb.ToString().Trim();
            }
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn_Britt_Calc_Click", ex.ToString(), "Audit");
            lblStatus.Text = "Err:" + ex.Message.ToString();
        }

    }

    protected void btn_Britt_Save_Click(object sender, EventArgs e)
    {

        lblStatus.Text = string.Empty;
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_Britt_Tmin.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_Britt_Tref.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Britt_CmpThick.SelectedItem.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Britt_PWHT.SelectedItem.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_Britt_Dfb.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Britt_FSE.SelectedItem.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_Britt_Df.Text.ToString().Trim()))
            {
                lblStatus.Text = "Err : Please check all the fields";
                lblStatus.ForeColor = Color.Red;
            }
            else
            {
                if (btnBrittSubmit.ToolTip == "Save")
                {
                    int intFlag = BusinessTier.BrittleSave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), cbo_Brittle_fulpressure.SelectedItem.Text.ToString().Trim(), Convert.ToInt32(txt_Britt_Dsgn.Text.ToString().Trim()), Convert.ToInt32(txt_Britt_ATM.Text.ToString().Trim()), Convert.ToInt32(txt_Britt_Tmin.Text.ToString().Trim()), Convert.ToInt32(txt_Britt_Tref.Text.ToString().Trim()), Convert.ToDecimal(cbo_Britt_CmpThick.SelectedItem.Text.ToString().Trim()), cbo_Britt_PWHT.SelectedItem.Text.ToString().Trim(), Convert.ToDecimal(txt_Britt_Dfb.Text.ToString().Trim()), Convert.ToInt32(txt_Britt_OpTemp.Text.ToString().Trim()), Convert.ToDecimal(cbo_Britt_FSE.SelectedItem.Text.ToString().Trim()), Convert.ToDecimal(txt_Britt_Df.Text.ToString().Trim()), Convert.ToInt32(Session["sesUserID"].ToString()), "N", 0);
                    lblStatus.Text = "Successfully Brittle Facture Value Inserted";
                    lblStatus.ForeColor = Color.Green;
                }
                else if (btnBrittSubmit.ToolTip == "Update")
                {
                    int intFlag = BusinessTier.BrittleSave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), cbo_Brittle_fulpressure.SelectedItem.Text.ToString().Trim(), Convert.ToInt32(txt_Britt_Dsgn.Text.ToString().Trim()), Convert.ToInt32(txt_Britt_ATM.Text.ToString().Trim()), Convert.ToInt32(txt_Britt_Tmin.Text.ToString().Trim()), Convert.ToInt32(txt_Britt_Tref.Text.ToString().Trim()), Convert.ToDecimal(cbo_Britt_CmpThick.SelectedItem.Text.ToString().Trim()), cbo_Britt_PWHT.SelectedItem.Text.ToString().Trim(), Convert.ToDecimal(txt_Britt_Dfb.Text.ToString().Trim()), Convert.ToInt32(txt_Britt_OpTemp.Text.ToString().Trim()), Convert.ToDecimal(cbo_Britt_FSE.SelectedItem.Text.ToString().Trim()), Convert.ToDecimal(txt_Britt_Df.Text.ToString().Trim()), Convert.ToInt32(Session["sesUserID"].ToString()), "U", Convert.ToInt32(btn_Britt_Save.ToolTip));
                    lblStatus.Text = "Successfully Brittle Facture Value Updated";
                    lblStatus.ForeColor = Color.Green;
                }

                BusinessTier.DisposeConnection(conn);
                BrittClear();
                btn_Britt_Save.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn_Britt_Save_Click", ex.ToString(), "Audit");
            //lblStatus.Text = "Err:" + ex.Message.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }

    }

    //<-------------------Temper Cracking------------------------>

    protected void OnSelectedIndexChanged_Temper_Fullpressure(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()))
            {
                lblStatus.Text = "Err : Please Select Process,Component & Equipment fields";
                lblStatus.ForeColor = Color.Red;
            }
            else
            {
                lblStatus.Text = string.Empty;
                txt_Temper_optemp.Text = cboComponent.SelectedItem.Attributes["OPTemp"].ToString();
                if (cbo_Temper_Fullpressure.SelectedItem.Text.ToString() == "Yes")
                {
                    lbl_Temper_Min.Text = "Minimum Temp:";

                }
                else
                {
                    lbl_Temper_Min.Text = "Min_Design Temp:";
                }
            }
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "OnSelectedIndexChanged_Temper_Fullpressure", ex.ToString(), "Audit");
            lblStatus.Text = "Err:" + ex.Message.ToString();
        }
    }

    protected void OnSelectedIndexChanged_Temper_FATT(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (cbo_Temper_FATT.SelectedItem.Text.ToString() == "Yes")
        {
            txt_Temper_Fattval.Enabled = true;
            txt_Temper_Fattval.Text = string.Empty;

        }
        else
        {
            txt_Temper_Fattval.Enabled = false;
            txt_Temper_Fattval.Text = "66";
        }
    }

    protected void OnSelectedIndexChanged_Temper_PWHT(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_Temper_Fattval.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_Temper_Tmin.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_Temper_Tref.ToString().Trim()) || string.IsNullOrEmpty(cbo_Temper_cmpThick.SelectedItem.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Temper_PWHT.SelectedItem.Text.ToString().Trim()))
            {
                lblStatus.Text = "Err : Please check all the fields";
                lblStatus.ForeColor = Color.Red;
            }
            else
            {
                lblStatus.Text = string.Empty;

                int c = Convert.ToInt32(txt_Temper_Tmin.Text.ToString().Trim()) - (Convert.ToInt32(txt_Temper_Tref.Text.ToString().Trim()) + Convert.ToInt32(txt_Temper_Fattval.Text.ToString().Trim()));

                if (c <= 38 && c > 27)
                {
                    c = 38;
                }
                else if (c <= 27 && c > 16)
                {
                    c = 27;
                }
                else if (c <= 16 && c > 4)
                {
                    c = 16;
                }
                else if (c <= 4 && c > -7)
                {
                    c = 4;
                }
                else if (c <= -7 && c > -18)
                {
                    c = -7;
                }
                else if (c <= -18 && c > -29)
                {
                    c = -18;
                }
                else if (c <= -29 && c > -40)
                {
                    c = -29;
                }
                else if (c <= -40 && c > -51)
                {
                    c = -40;
                }
                else if (c <= -51 && c > -62)
                {
                    c = -51;
                }
                else if (c <= -62 && c > -73)
                {
                    c = -62;
                }
                else if (c <= -73)
                {
                    c = -73;
                }
                else
                {
                    c = 38;
                }
                string strqryH2S = "select " + cbo_Temper_cmpThick.SelectedValue.ToString().Trim() + " as dfbtemper from " + cbo_Temper_PWHT.SelectedValue.ToString().Trim() + " where C=" + c + "";
                SqlCommand cmdH2S = new SqlCommand(strqryH2S, conn);
                SqlDataReader rdH2S = cmdH2S.ExecuteReader();
                rdH2S.Read();
                txt_Temper_Dfb.Text = rdH2S["dfbtemper"].ToString().Trim();
                rdH2S.Close();
                BusinessTier.DisposeConnection(conn);
            }
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "OnSelectedIndexChanged_Temper_PWHT", ex.ToString(), "Audit");
            lblStatus.Text = "Err:" + ex.Message.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    protected void btn_Temper_Tmin_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_Temper_MinDsgn.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_Temper_optemp.Text.ToString().Trim()))
        {
            lblStatus.Text = "Err : Please Check all the fields";
            lblStatus.ForeColor = Color.Red;
        }
        else
        {
            lblStatus.Text = string.Empty;
            int tmin = Math.Min(Convert.ToInt32(txt_Temper_optemp.Text.ToString()), Convert.ToInt32(txt_Temper_MinDsgn.Text.ToString()));

            txt_Temper_Tmin.Text = tmin.ToString();
        }
    }

    protected void btn_Temper_Calc_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txt_Temper_Dfb.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Temper_FSE.SelectedItem.Text.ToString().Trim()))
        {
        }
        else
        {
            decimal Dfb = Convert.ToDecimal(txt_Temper_Dfb.Text.ToString().Trim()) * Convert.ToDecimal(cbo_Temper_FSE.SelectedValue.ToString().Trim());
            txt_Temper_Df.Text = Dfb.ToString().Trim();
        }
    }

    protected void btn_Temper_Save_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_Temper_Tmin.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_Temper_Tref.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Temper_cmpThick.SelectedItem.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Temper_PWHT.SelectedItem.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_Temper_Dfb.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Temper_FSE.SelectedItem.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_Temper_Df.Text.ToString().Trim()))
        {
            lblStatus.Text = "Err : Please check all the fields";
            lblStatus.ForeColor = Color.Red;
        }
        else
        {
            lblStatus.Text = string.Empty;
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            try
            {

                if (btnTemperSubmit.ToolTip == "Save")
                {
                    int intFlag = BusinessTier.TemperSave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), cbo_Temper_Fullpressure.SelectedItem.Text.ToString().Trim(), Convert.ToInt32(txt_Temper_MinDsgn.Text.ToString().Trim()), Convert.ToInt32(txt_Temper_Tmin.Text.ToString().Trim()), Convert.ToInt32(txt_Temper_Tref.Text.ToString().Trim()), cbo_Temper_FATT.SelectedItem.Text.ToString().Trim(), Convert.ToInt32(txt_Temper_Fattval.Text.ToString().Trim()), Convert.ToDecimal(cbo_Temper_cmpThick.SelectedItem.Text.ToString().Trim()), cbo_Temper_PWHT.SelectedItem.Text.ToString().Trim(), Convert.ToDecimal(txt_Temper_Dfb.Text.ToString().Trim()), Convert.ToInt32(txt_Temper_optemp.Text.ToString().Trim()), Convert.ToDecimal(cbo_Temper_FSE.SelectedItem.Text.ToString().Trim()), Convert.ToDecimal(txt_Temper_Df.Text.ToString().Trim()), Convert.ToInt32(Session["sesUserID"].ToString()), "N", 0);
                    lblStatus.Text = "Successfully Temper Embrittlement Value Inserted";
                    lblStatus.ForeColor = Color.Green;
                }
                else if (btnTemperSubmit.ToolTip == "Update")
                {
                    int intFlag = BusinessTier.TemperSave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), cbo_Temper_Fullpressure.SelectedItem.Text.ToString().Trim(), Convert.ToInt32(txt_Temper_MinDsgn.Text.ToString().Trim()), Convert.ToInt32(txt_Temper_Tmin.Text.ToString().Trim()), Convert.ToInt32(txt_Temper_Tref.Text.ToString().Trim()), cbo_Temper_FATT.SelectedItem.Text.ToString().Trim(), Convert.ToInt32(txt_Temper_Fattval.Text.ToString().Trim()), Convert.ToDecimal(cbo_Temper_cmpThick.SelectedItem.Text.ToString().Trim()), cbo_Temper_PWHT.SelectedItem.Text.ToString().Trim(), Convert.ToDecimal(txt_Temper_Dfb.Text.ToString().Trim()), Convert.ToInt32(txt_Temper_optemp.Text.ToString().Trim()), Convert.ToDecimal(cbo_Temper_FSE.SelectedItem.Text.ToString().Trim()), Convert.ToDecimal(txt_Temper_Df.Text.ToString().Trim()), Convert.ToInt32(Session["sesUserID"].ToString()), "U", Convert.ToInt32(btn_Temper_Save.ToolTip));
                    lblStatus.Text = "Successfully Temper Embrittlement Value Updated";
                    lblStatus.ForeColor = Color.Green;
                }

                BusinessTier.DisposeConnection(conn);
                TemperClear();
                btn_Temper_Save.Enabled = false;

            }
            catch (Exception ex)
            {
                InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn_Temper_Save_Click", ex.ToString(), "Audit");
                //lblStatus.Text = "Err:" + ex.Message.ToString();
            }
            finally
            {
                BusinessTier.DisposeConnection(conn);
            }
        }
    }

    //<-------------------885 Cracking------------------------>

    protected void OnSelectedIndexChanged_885_Fullpressure(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()))
        {
            lblStatus.Text = "Err : Please Select Process,Component & Equipment fields";
            lblStatus.ForeColor = Color.Red;
        }
        else
        {
            lblStatus.Text = string.Empty;
            txt_885_Optemp.Text = cboComponent.SelectedItem.Attributes["OPTemp"].ToString();
            if (cbo_885_Fullpressure.SelectedItem.Text.ToString() == "Yes")
            {
                lbl_885_MinDsgn.Text = "Minimum Temp:";

            }
            else
            {
                lbl_885_MinDsgn.Text = "Min_Design Temp:";
            }
        }
    }

    protected void OnSelectedIndexChanged_885_FATT(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (cbo_885_Trefknown.SelectedItem.Text.ToString() == "Yes")
        {
            txt_885_Tref.Enabled = true;
            txt_885_Tref.Text = string.Empty;

        }
        else
        {
            txt_885_Tref.Enabled = false;
            txt_885_Tref.Text = "28";

        }
    }

    protected void btn_885_Tmin_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_885_MinDsgn.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_885_Optemp.Text.ToString().Trim()))
        {
            lblStatus.Text = "Err : Please Check all the fields";
            lblStatus.ForeColor = Color.Red;
        }
        else
        {
            lblStatus.Text = string.Empty;
            int tmin = Math.Min(Convert.ToInt32(txt_885_Optemp.Text.ToString()), Convert.ToInt32(txt_885_MinDsgn.Text.ToString()));

            txt_885_Tmin.Text = tmin.ToString();
        }
    }

    protected void btn_885_Calc_Click(object sender, EventArgs e)
    {
        try
        {
            lblStatus.Text = string.Empty;

            if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_885_Tmin.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_885_Tref.ToString().Trim()) || string.IsNullOrEmpty(cbo_885_CmpThick.SelectedItem.Text.ToString().Trim()))
            {
                lblStatus.Text = "Err : Please check all the fields";
                lblStatus.ForeColor = Color.Red;
            }
            else
            {
                int c = Convert.ToInt32(txt_885_Tmin.Text.ToString().Trim()) - Convert.ToInt32(txt_885_Tref.Text.ToString().Trim());

                if (c <= 38 && c > 27)
                {
                    c = 2;
                }
                else if (c <= 27 && c > 16)
                {
                    c = 8;
                }
                else if (c <= 16 && c > 4)
                {
                    c = 30;
                }
                else if (c <= 4 && c > -7)
                {
                    c = 87;
                }
                else if (c <= -7 && c > -18)
                {
                    c = 200;
                }
                else if (c <= -18 && c > -29)
                {
                    c = 371;
                }
                else if (c <= -29 && c > -40)
                {
                    c = 581;
                }
                else if (c <= -40 && c > -51)
                {
                    c = 806;
                }
                else if (c <= -51 && c > -62)
                {
                    c = 1022;
                }
                else if (c <= -62 && c > -73)
                {
                    c = 1216;
                }
                else if (c <= -73)
                {
                    c = 1381;
                }
                else
                {
                    c = 2;
                }

                txt_885_Df.Text = c.ToString().Trim();
            }
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn_885_Calc_Click", ex.ToString(), "Audit");
            // lblStatus.Text = "Err:" + ex.Message.ToString();
        }

    }

    protected void btn_885_Save_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_885_Tmin.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_885_Tref.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_885_CmpThick.SelectedItem.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_885_Trefknown.SelectedItem.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_885_Df.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_885_Optemp.Text.ToString().Trim()))
        {
            lblStatus.Text = "Err : Please check all the fields";
            lblStatus.ForeColor = Color.Red;
        }
        else
        {
            lblStatus.Text = string.Empty;
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            try
            {
                if (btn885Submit.ToolTip == "Save")
                {
                    int intFlag = BusinessTier.EightEightFiveSave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), cbo_885_Fullpressure.SelectedItem.Text.ToString().Trim(), Convert.ToInt32(txt_885_Optemp.Text.ToString().Trim()), Convert.ToInt32(txt_885_MinDsgn.Text.ToString().Trim()), Convert.ToInt32(txt_885_Tmin.Text.ToString().Trim()), Convert.ToDecimal(cbo_885_CmpThick.SelectedItem.Text.ToString().Trim()), cbo_885_Trefknown.SelectedItem.Text.ToString().Trim(), Convert.ToInt32(txt_885_Tref.Text.ToString().Trim()), Convert.ToDecimal(txt_885_Df.Text.ToString().Trim()), Convert.ToInt32(Session["sesUserID"].ToString()), "N", 0);
                    lblStatus.Text = "Successfully 885 Embrittlement Value Inserted";
                    lblStatus.ForeColor = Color.Green;
                }
                else if (btn885Submit.ToolTip == "Update")
                {
                    int intFlag = BusinessTier.EightEightFiveSave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), cbo_885_Fullpressure.SelectedItem.Text.ToString().Trim(), Convert.ToInt32(txt_885_Optemp.Text.ToString().Trim()), Convert.ToInt32(txt_885_MinDsgn.Text.ToString().Trim()), Convert.ToInt32(txt_885_Tmin.Text.ToString().Trim()), Convert.ToDecimal(cbo_885_CmpThick.SelectedItem.Text.ToString().Trim()), cbo_885_Trefknown.SelectedItem.Text.ToString().Trim(), Convert.ToInt32(txt_885_Tref.Text.ToString().Trim()), Convert.ToDecimal(txt_885_Df.Text.ToString().Trim()), Convert.ToInt32(Session["sesUserID"].ToString()), "U", Convert.ToInt32(btn_885_Save.ToolTip));
                    lblStatus.Text = "Successfully 885 Embrittlement Value Updated";
                    lblStatus.ForeColor = Color.Green;
                }

                BusinessTier.DisposeConnection(conn);
                Clear885();
                btn_885_Save.Enabled = false;

            }
            catch (Exception ex)
            {
                InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn_885_Save_Click", ex.ToString(), "Audit");
                //lblStatus.Text = "Err:" + ex.Message.ToString();
            }
            finally
            {
                BusinessTier.DisposeConnection(conn);
            }
        }
    }

    //<-------------------Sigma Function------------------------>

    protected void btn_Sigma_Calc_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Sigma_Tmin.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Sigma_Function.Text.ToString().Trim()))
        {
            lblStatus.Text = "Err : Please check all the fields";
            lblStatus.ForeColor = Color.Red;
        }
        else
        {
            lblStatus.Text = string.Empty;
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            try
            {

                string strqryH2S = "select " + cbo_Sigma_Function.SelectedValue.ToString().Trim() + " as dfsigma from Ref_Sigma where Tmin=" + Convert.ToInt32(cbo_Sigma_Tmin.SelectedItem.Text.ToString().Trim()) + "";
                SqlCommand cmdH2S = new SqlCommand(strqryH2S, conn);
                SqlDataReader rdH2S = cmdH2S.ExecuteReader();
                rdH2S.Read();
                txt_Sigma_Df.Text = rdH2S["dfsigma"].ToString().Trim();
                rdH2S.Close();
                BusinessTier.DisposeConnection(conn);

            }
            catch (Exception ex)
            {
                InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn_Sigma_Calc_Click", ex.ToString(), "Audit");
                //lblStatus.Text = "Err:" + ex.Message.ToString();
            }
            finally
            {
                BusinessTier.DisposeConnection(conn);
            }
        }
    }

    protected void btn_Sigma_Save_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Sigma_Tmin.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Sigma_Function.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_Sigma_Df.Text.ToString().Trim()))
        {
            lblStatus.Text = "Err : Please check all the fields";
            lblStatus.ForeColor = Color.Red;
        }
        else
        {
            lblStatus.Text = string.Empty;
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            try
            {
                if (btnSigmaSubmit.ToolTip == "Save")
                {
                    int intFlag = BusinessTier.SigmaSave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), Convert.ToInt32(cbo_Sigma_Tmin.Text.ToString().Trim()), cbo_Sigma_Function.SelectedItem.Text.ToString().Trim(), Convert.ToDecimal(txt_Sigma_Df.Text.ToString().Trim()), Convert.ToInt32(Session["sesUserID"].ToString()), "N", 0);
                    lblStatus.Text = "Successfully Sigma Phase Embrittlement Value Inserted";
                    lblStatus.ForeColor = Color.Green;
                }
                else if (btnSigmaSubmit.ToolTip == "Update")
                {
                    int intFlag = BusinessTier.SigmaSave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), Convert.ToInt32(cbo_Sigma_Tmin.Text.ToString().Trim()), cbo_Sigma_Function.SelectedItem.Text.ToString().Trim(), Convert.ToDecimal(txt_Sigma_Df.Text.ToString().Trim()), Convert.ToInt32(Session["sesUserID"].ToString()), "U", Convert.ToInt32(btn_Sigma_Save.ToolTip));
                    lblStatus.Text = "Successfully Sigma Phase Embrittlement Value Updated";
                    lblStatus.ForeColor = Color.Green;
                }

                BusinessTier.DisposeConnection(conn);
                SigmaClear();
                btn_Sigma_Save.Enabled = false;
            }
            catch (Exception ex)
            {
                InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn_Sigma_Save_Click", ex.ToString(), "Audit");
                //lblStatus.Text = "Err:" + ex.Message.ToString();
            }
            finally
            {
                BusinessTier.DisposeConnection(conn);
            }
        }
    }

    //<-------------------Mechanical Fatigue Function------------------------>

    protected void OnSelectedIndexChanged_Mech_Dfb(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Mech_DFBPF.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Mech_DFBAS.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Mech_FFBAS.Text.ToString().Trim()))
        {
            lblStatus.Text = "Err : Please Check all the fields";
            lblStatus.ForeColor = Color.Red;
        }
        else
        {
            lblStatus.Text = string.Empty;
            decimal d = Math.Max(Convert.ToDecimal(cbo_Mech_DFBPF.SelectedValue.ToString()), (Convert.ToDecimal(cbo_Mech_DFBAS.SelectedValue.ToString()) * Convert.ToDecimal(cbo_Mech_FFBAS.SelectedValue.ToString())));
            decimal dfb = Math.Max(d, Convert.ToDecimal(cbo_Mech_DFBCF.SelectedValue.ToString()));
            txt_Mech_Dfb.Text = dfb.ToString();
        }
    }

    protected void btn_Mech_Calc_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Mech_DFBPF.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Mech_DFBAS.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Mech_FFBAS.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Mech_DFBCF.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Mech_FCA.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Mech_FPC.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Mech_FCP.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Mech_FJP.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Mech_FBD.Text.ToString().Trim()))
        {
            lblStatus.Text = "Err : Please Check all the fields";
            lblStatus.ForeColor = Color.Red;
        }
        else
        {
            lblStatus.Text = string.Empty;
            decimal df = Convert.ToDecimal(txt_Mech_Dfb.Text.ToString()) * Convert.ToDecimal(cbo_Mech_FCA.SelectedValue.ToString()) * Convert.ToDecimal(cbo_Mech_FPC.SelectedValue.ToString()) * Convert.ToDecimal(cbo_Mech_FCP.SelectedValue.ToString()) * Convert.ToDecimal(cbo_Mech_FJP.SelectedValue.ToString()) * Convert.ToDecimal(cbo_Mech_FBD.SelectedValue.ToString());
            txt_Mech_Df.Text = Math.Round(df, 3).ToString();
        }
    }

    protected void btn_Mech_Save_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Mech_DFBPF.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Mech_DFBAS.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Mech_FFBAS.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Mech_DFBCF.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Mech_FCA.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Mech_FPC.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Mech_FCP.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Mech_FJP.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_Mech_FBD.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_Mech_Df.Text.ToString().Trim()))
        {
            lblStatus.Text = "Err : Please check all the fields";
            lblStatus.ForeColor = Color.Red;
        }
        else
        {
            lblStatus.Text = string.Empty;
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            try
            {

                if (btnMechSubmit.ToolTip == "Save")
                {
                    int intFlag = BusinessTier.MechSave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), Convert.ToInt32(cbo_Mech_DFBPF.SelectedValue.ToString().Trim()), Convert.ToInt32(cbo_Mech_DFBAS.SelectedValue.ToString().Trim()), Convert.ToDecimal(cbo_Mech_FFBAS.SelectedValue.ToString().Trim()), Convert.ToInt32(cbo_Mech_DFBCF.SelectedValue.ToString().Trim()), Convert.ToDecimal(txt_Mech_Dfb.Text.ToString().Trim()), Convert.ToDecimal(cbo_Mech_FCA.SelectedValue.ToString().Trim()), Convert.ToDecimal(cbo_Mech_FPC.SelectedValue.ToString().Trim()), cbo_Mech_FCP.SelectedValue.ToString().Trim(), Convert.ToDecimal(cbo_Mech_FJP.SelectedValue.ToString().Trim()), Convert.ToDecimal(cbo_Mech_FBD.SelectedValue.ToString().Trim()), Convert.ToDecimal(txt_Mech_Df.Text.ToString().Trim()), Convert.ToInt32(Session["sesUserID"].ToString()), "N", 0);
                    lblStatus.Text = "Successfully Mechanical Fatigue Damage Factor Value Inserted";
                    lblStatus.ForeColor = Color.Green;
                }
                else if (btnMechSubmit.ToolTip == "Update")
                {
                    int intFlag = BusinessTier.MechSave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), Convert.ToInt32(cbo_Mech_DFBPF.SelectedValue.ToString().Trim()), Convert.ToInt32(cbo_Mech_DFBAS.SelectedValue.ToString().Trim()), Convert.ToDecimal(cbo_Mech_FFBAS.SelectedValue.ToString().Trim()), Convert.ToInt32(cbo_Mech_DFBCF.SelectedValue.ToString().Trim()), Convert.ToDecimal(txt_Mech_Dfb.Text.ToString().Trim()), Convert.ToDecimal(cbo_Mech_FCA.SelectedValue.ToString().Trim()), Convert.ToDecimal(cbo_Mech_FPC.SelectedValue.ToString().Trim()), cbo_Mech_FCP.SelectedValue.ToString().Trim(), Convert.ToDecimal(cbo_Mech_FJP.SelectedValue.ToString().Trim()), Convert.ToDecimal(cbo_Mech_FBD.SelectedValue.ToString().Trim()), Convert.ToDecimal(txt_Mech_Df.Text.ToString().Trim()), Convert.ToInt32(Session["sesUserID"].ToString()), "U", Convert.ToInt32(btn_Mech_Save.ToolTip));
                    lblStatus.Text = "Successfully Mechanical Fatigue Damage Factor Value Updated";
                    lblStatus.ForeColor = Color.Green;
                }

                BusinessTier.DisposeConnection(conn);
                MechClear();
                btn_Mech_Save.Enabled = false;
            }
            catch (Exception ex)
            {
                InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn_Mech_Save_Click", ex.ToString(), "Audit");
                //lblStatus.Text = "Err:" + ex.Message.ToString();
            }
            finally
            {
                BusinessTier.DisposeConnection(conn);
            }
        }
    }

    //<-------------------Final Calculation------------------------>

    protected void btn_Final_Calc_Click(object sender, EventArgs e)
    {
        double Df_total = Convert.ToDouble(txt_Df_totalgov.Text);

        string CmdCate = string.Empty;

        if (Df_total <= 2)
        {
            CmdCate = "1";
        }
        else if (Df_total > 2 && Df_total <= 20)
        {
            CmdCate = "2";
        }
        else if (Df_total > 20 && Df_total <= 100)
        {
            CmdCate = "3";
        }
        else if (Df_total > 100 && Df_total <= 1000)
        {
            CmdCate = "4";
        }
        else if (Df_total > 1000)
        {
            CmdCate = "5";
        }

        txt_Category.Text = CmdCate.ToString();
    }

    protected void btn_Final_Save_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_Df_Thinninggov.Text.ToString().Trim()))
        {
            lblStatus.Text = "Err : Please check all the fields";
            lblStatus.ForeColor = Color.Red;
        }
        else
        {
            lblStatus.Text = string.Empty;
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            try
            {
                string flag = string.Empty;
                string strqrydup = "Select  *  from pof_total  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' and  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' and CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
                SqlCommand cmddup = new SqlCommand(strqrydup, conn);
                SqlDataReader readerdup = cmddup.ExecuteReader();
                if (readerdup.Read())
                {
                    readerdup.Close();
                    flag = "U";
                    lblStatus.Text = "Successfully POF Total Updated";
                }
                else
                {
                    readerdup.Close();
                    flag = "N";
                    lblStatus.Text = "Successfully POF Total Inserted";
                }
                lblStatus.ForeColor = Color.Green;
                int intFlag = BusinessTier.POFtotalSave(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), txt_Df_Thinninggov.Text.ToString().Trim(), txt_Df_SCCgov.Text.ToString().Trim(), txt_Df_extdgov.Text.ToString().Trim(), txt_Df_britgov.Text.ToString().Trim(), txt_Df_totalgov.Text.ToString().Trim(), txt_Category.Text.ToString().Trim(), Convert.ToInt32(Session["sesUserID"].ToString()), flag, 0);
                BusinessTier.DisposeConnection(conn);

                FinalClear();
                btn_Final_Save.Enabled = false;
            }
            catch (Exception ex)
            {
                InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn_Final_Save_Click", ex.ToString(), "Audit");
                return;

            }
            finally
            {
                BusinessTier.DisposeConnection(conn);
            }
        }
    }

    //<-------------------Submit Buttons------------------------>

    protected void btnThinningSubmit_Click(object sender, EventArgs e)
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
            //string strqry = "SELECT InspectionEffective,NoofInspection FROM [Tbl_EquipmentComponentDetails] where deleted=0 and [ProcessareaID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EqupID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompAutoID]='" + cboComponent.SelectedValue.ToString() + "' and [CompanyID]='" + Convert.ToInt32(Session["sesCompanyID"].ToString()) + "'";
            //SqlCommand cmd = new SqlCommand(strqry, conn);
            //SqlDataReader rdr = cmd.ExecuteReader();

            //if (rdr.Read())
            //{
            //    //cbo_inspecEffec.Text = rdr["InspectionEffective"].ToString();
            //   // cbo_inspecEffec.SelectedValue = Inspectcategory(rdr["InspectionEffective"].ToString()).ToString();
            //   // cbo_Thin_nofIns.Text = rdr["NoofInspection"].ToString();
            //}
            //rdr.Close();

            // txt_Age.Text = Agecal(cboProcessArea.SelectedValue.ToString(), cboEquipment.SelectedValue.ToString(), cboComponent.SelectedValue.ToString(), Session["sesCompanyID"].ToString()).ToString();

            string strqry1 = "SELECT * FROM [ThinningDamage] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "' and [CompanyID]='" + Convert.ToInt32(Session["sesCompanyID"].ToString()) + "'";
            SqlCommand cmd11 = new SqlCommand(strqry1, conn);
            SqlDataReader rdr11 = cmd11.ExecuteReader();

            if (rdr11.Read())
            {
                btnThinningSubmit.ToolTip = "Update";
                btn_ThinningSave.ToolTip = rdr11["TDFId"].ToString();
                //cboclad.Text = rdr11["Clad"].ToString();
                txt_Age.Text = rdr11["age"].ToString();
                cbo_inspecEffec.Text = Inspectcategory(rdr11["InspectCate"].ToString()).ToString();
                cbo_inspecEffec.SelectedValue = rdr11["InspectCate"].ToString();
                cbo_Thin_nofIns.Text = rdr11["nofins"].ToString();
                cbo_Thin_InspDate.SelectedDate = Convert.ToDateTime(rdr11["InspectDate"].ToString());
                cbo_thin_type.Text = rdr11["ThinType"].ToString();
                txt_art.Text = rdr11["Art"].ToString();
                txt_Tdf.Text = rdr11["Tdf"].ToString();
                BusinessTier.DisposeReader(rdr11);
                btnThiningDelete.Enabled = true;
            }
            else
            {
                BusinessTier.DisposeReader(rdr11);


                ThinningClear();
                lblStatus.Text = "This Component dont have the value now you can enter the new data";
                lblStatus.ForeColor = Color.Blue;
            }


            BusinessTier.DisposeConnection(conn);
            btn_ThinningSave.Enabled = true;
        }
        catch (Exception ex)
        {
            //lblStatus.Text = ex.ToString();
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btnThinningSubmit_Click", ex.ToString(), "Audit");

        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    public static int Agecal(int maxdate, string ProcessareaID, string EqupID, string ComponentNo, string sesCompanyID)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        //string str1 = "SELECT Max(DATEPART(yyyy,InspecDate)) as maxdate FROM [Tbl_InspectionDataDetails] where deleted=0 and [EqupID]='" + EqupID.ToString() + "' and [ComponentNo]='" + ComponentNo.ToString() + "' and [CompanyID]='" + sesCompanyID.ToString() + "'";
        //SqlCommand cmd1 = new SqlCommand(str1, conn);
        //SqlDataReader rdr1 = cmd1.ExecuteReader();
        //int maxdate = 0;
        //if (rdr1.Read())
        //{
        //    if (string.IsNullOrEmpty(rdr1["maxdate"].ToString()))
        //    {
        //        maxdate = 0;
        //    }
        //    else
        //    {
        //        maxdate = Convert.ToInt32(rdr1["maxdate"].ToString());
        //    }
        //}

        //rdr1.Close();
        //int maxdate = 0;
        // maxdate= Convert.ToInt32(cbo_Thin_InspDate.

        string str2 = "SELECT yearinstalled as mindate FROM [Tbl_EquipmentAsset] where deleted=0 and [ProcessareaID]='" + ProcessareaID.ToString() + "' and [EquAutoID]='" + EqupID.ToString() + "'  and [CompanyID]='" + sesCompanyID.ToString() + "'";
        SqlCommand cmd2 = new SqlCommand(str2, conn);
        SqlDataReader rdr2 = cmd2.ExecuteReader();
        int mindate = 0, caldate = 0;
        if (rdr2.Read())
        {
            mindate = Convert.ToInt32(rdr2["mindate"].ToString());
        }
        rdr2.Close();
        BusinessTier.DisposeConnection(conn);
        caldate = maxdate - mindate;
        return caldate;
    }

    protected void btnLiningSubmit_Click(object sender, EventArgs e)
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

            string strqry1 = "SELECT * FROM [LiningDamage] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "' and [CompanyID]='" + Convert.ToInt32(Session["sesCompanyID"].ToString()) + "'";
            SqlCommand cmd11 = new SqlCommand(strqry1, conn);
            SqlDataReader rdr11 = cmd11.ExecuteReader();

            if (rdr11.Read())
            {
                btnLiningSubmit.ToolTip = "Update";
                btn_LineSave.ToolTip = rdr11["LnfID"].ToString();
                cbo_Typelining.Text = rdr11["Lntype"].ToString();
                cbo_AgeLining.Text = rdr11["SIyear"].ToString();
                cbo_Dfb.Text = rdr11["Dfb"].ToString();
                cbo_liningcondition.Text = rdr11["LnCond"].ToString();
                cbo_liningcondition.SelectedValue = rdr11["LnCondVal"].ToString();
                cbo_OnlineMonitoring.Text = rdr11["OnMoni"].ToString();
                cbo_OnlineMonitoring.SelectedValue = rdr11["OnMoniVal"].ToString();
                txt_dfliner.Text = rdr11["DfLine"].ToString();

            }
            else
            {
                btnLiningSubmit.ToolTip = "Save";
                LiningClear();

                lblStatus.Text = "This Component dont have the value now you can enter the new data";
                lblStatus.ForeColor = Color.Blue;
            }

            BusinessTier.DisposeReader(rdr11);
            BusinessTier.DisposeConnection(conn);
            btn_LineSave.Enabled = true;
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btnLiningSubmit_Click", ex.ToString(), "Audit");
            //lblStatus.Text = ex.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    protected void btnECDSubmit_Click(object sender, EventArgs e)
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

            string strqry1 = "SELECT * FROM [ECD] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "'and [CompanyID]='" + Convert.ToInt32(Session["sesCompanyID"].ToString()) + "'";
            SqlCommand cmd11 = new SqlCommand(strqry1, conn);
            SqlDataReader rdr11 = cmd11.ExecuteReader();

            if (rdr11.Read())
            {
                btnECDSubmit.ToolTip = "Update";
                btn_ECD_Save.ToolTip = rdr11["ECDID"].ToString();
                txt_ECD_Agetk.Text = rdr11["Agtk"].ToString();
                cbo_ECD_InsEff.Text = Inspectcategory(rdr11["InsEff"].ToString()).ToString();
                cbo_ECD_InsEff.SelectedValue = rdr11["InsEff"].ToString();
                dt_ECD_CmpInstal.SelectedDate = Convert.ToDateTime(rdr11["cmpdt"].ToString());
                dt_ECD_CalcDate.SelectedDate = Convert.ToDateTime(rdr11["caldt"].ToString());
                cbo_ECD_nofIns.Text = rdr11["nofins"].ToString();
                dt_ECD_InspectDate.SelectedDate = Convert.ToDateTime(rdr11["InspectDate"].ToString());
                cbo_ECD_CoatQual.Text = rdr11["coatqual"].ToString();
                txt_ECD_AgeCoat.Text = rdr11["ECDagcoat"].ToString();
                txt_ECD_Age.Text = rdr11["ECDage"].ToString();

                cbo_ECD_Fps.Text = rdr11["fps"].ToString();
                cbo_ECD_Fip.Text = rdr11["fip"].ToString();
                cbo_ECD_crdriver.Text = rdr11["crdriver"].ToString();
                txt_ECD_cr.Text = rdr11["cr"].ToString();
                txt_ECD_art.Text = rdr11["ECDart"].ToString();
                txt_ECD_df.Text = rdr11["ECDDf"].ToString();
                btnECDDelete.Enabled = true;

            }
            else
            {
                btnECDSubmit.ToolTip = "Save";
                ECDClear();
                lblStatus.Text = "This Component dont have the value now you can enter the new data";
                lblStatus.ForeColor = Color.Blue;
            }

            BusinessTier.DisposeReader(rdr11);
            BusinessTier.DisposeConnection(conn);
            btn_ECD_Save.Enabled = true;

        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btnECDSubmit_Click", ex.ToString(), "Audit");
            //lblStatus.Text = ex.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    protected void btnCUISubmit_Click(object sender, EventArgs e)
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

            string strqry1 = "SELECT * FROM [CUI] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "' and [CompanyID]='" + Convert.ToInt32(Session["sesCompanyID"].ToString()) + "'";
            SqlCommand cmd11 = new SqlCommand(strqry1, conn);
            SqlDataReader rdr11 = cmd11.ExecuteReader();

            if (rdr11.Read())
            {
                btnCUISubmit.ToolTip = "Update";
                btn_CUI_Save.ToolTip = rdr11["CUIID"].ToString();
                txt_CUI_Agetk.Text = rdr11["Agtk"].ToString();
                cbo_CUI_InsEff.Text = Inspectcategory(rdr11["InsEff"].ToString()).ToString();
                cbo_CUI_InsEff.SelectedValue = rdr11["InsEff"].ToString();
                dt_CUI_CmpInstal.SelectedDate = Convert.ToDateTime(rdr11["cmpdt"].ToString());
                dt_CUI_CalcDate.SelectedDate = Convert.ToDateTime(rdr11["caldt"].ToString());
                cbo_CUI_nofIns.Text = rdr11["nofins"].ToString();
                dt_CUI_InspectDate.SelectedDate = Convert.ToDateTime(rdr11["InspectDate"].ToString());
                cbo_CUI_coatqual.Text = rdr11["coatqual"].ToString();
                txt_CUI_Agecoat.Text = rdr11["CUIagcoat"].ToString();
                txt_CUI_Age.Text = rdr11["CUIage"].ToString();

                cbo_CUI_Fps.Text = rdr11["fps"].ToString();
                cbo_CUI_Fip.Text = rdr11["fip"].ToString();
                cbo_CUI_crdriver.Text = rdr11["crdriver"].ToString();
                txt_CUI_cr.Text = rdr11["cr"].ToString();
                txt_CUI_art.Text = rdr11["CUIart"].ToString();
                txt_CUI_Df.Text = rdr11["CUIDf"].ToString();

                cbo_CUI_Fins.Text = rdr11["fins"].ToString();
                cbo_CUI_Fcm.Text = rdr11["fcm"].ToString();
                cbo_CUI_Fic.Text = rdr11["fic"].ToString();
                btnCUIDelete.Enabled = true;
            }
            else
            {
                btnCUISubmit.ToolTip = "Save";
                CUIClear();
                lblStatus.Text = "This Component dont have the value now you can enter the new data";
                lblStatus.ForeColor = Color.Blue;
            }

            BusinessTier.DisposeReader(rdr11);
            BusinessTier.DisposeConnection(conn);
            btn_CUI_Save.Enabled = true;

        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btnCUISubmit_Click", ex.ToString(), "Audit");
            lblStatus.Text = ex.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    protected void btnExCLSSubmit_Click(object sender, EventArgs e)
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

            string strqry1 = "SELECT * FROM [ExCLS] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "' and [CompanyID]='" + Convert.ToInt32(Session["sesCompanyID"].ToString()) + "'";
            SqlCommand cmd11 = new SqlCommand(strqry1, conn);
            SqlDataReader rdr11 = cmd11.ExecuteReader();

            if (rdr11.Read())
            {
                btnExCLSSubmit.ToolTip = "Update";
                btn_ExCLS_Save.ToolTip = rdr11["ExCLSID"].ToString();
                txt_ExCLS_Agetk.Text = rdr11["Agtk"].ToString();
                cbo_ExCLS_InsEff.Text = Inspectcategory(rdr11["InsEff"].ToString()).ToString();
                cbo_ExCLS_InsEff.SelectedValue = rdr11["InsEff"].ToString();
                dt_ExCLS_CmpInstal.SelectedDate = Convert.ToDateTime(rdr11["cmpdt"].ToString());
                dt_ExCLS_CalcDate.SelectedDate = Convert.ToDateTime(rdr11["caldt"].ToString());
                cbo_ExCLS_nofIns.Text = rdr11["nofins"].ToString();
                dt_ExCLS_InspectDate.SelectedDate = Convert.ToDateTime(rdr11["InspectDate"].ToString());
                cbo_ExCLS_CoatQual.Text = rdr11["coatqual"].ToString();
                txt_ExCLS_Age.Text = rdr11["age"].ToString();
                cbo_ExCLS_crdriver.Text = rdr11["crdriver"].ToString();

                txt_ExCLS_Svi.Text = rdr11["Svi"].ToString();
                txt_ExCLS_Df.Text = rdr11["ExCLSDf"].ToString();

                btnExCLSSDelete.Enabled = true;
            }
            else
            {
                btnExCLSSubmit.ToolTip = "Save";
                ExCLSClear();
                lblStatus.Text = "This Component dont have the value now you can enter the new data";
                lblStatus.ForeColor = Color.Blue;
            }

            BusinessTier.DisposeReader(rdr11);
            BusinessTier.DisposeConnection(conn);
            btn_ExCLS_Save.Enabled = true;

        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btnExCLSSubmit_Click", ex.ToString(), "Audit");
            lblStatus.Text = ex.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    protected void btnExCUISubmit_Click(object sender, EventArgs e)
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

            string strqry1 = "SELECT * FROM [ExCUI] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "' and [CompanyID]='" + Convert.ToInt32(Session["sesCompanyID"].ToString()) + "'";
            SqlCommand cmd11 = new SqlCommand(strqry1, conn);
            SqlDataReader rdr11 = cmd11.ExecuteReader();

            if (rdr11.Read())
            {
                btnExCUISubmit.ToolTip = "Update";
                btn_ExCUI_Save.ToolTip = rdr11["ExCUIID"].ToString();
                txt_ExCUI_Agetk.Text = rdr11["Agtk"].ToString();
                cbo_ExCUI_InsEff.Text = Inspectcategory(rdr11["InsEff"].ToString()).ToString();
                cbo_ExCUI_InsEff.SelectedValue = rdr11["InsEff"].ToString();
                dt_ExCUI_CmpInstal.SelectedDate = Convert.ToDateTime(rdr11["cmpdt"].ToString());
                dt_ExCUI_CalcDate.SelectedDate = Convert.ToDateTime(rdr11["caldt"].ToString());
                cbo_ExCUI_nofIns.Text = rdr11["nofins"].ToString();
                dt_ExCUI_InspectDate.SelectedDate = Convert.ToDateTime(rdr11["InspectDate"].ToString());
                cbo_ExCUI_CoatQual.Text = rdr11["coatqual"].ToString();
                txt_ExCUI_Age.Text = rdr11["age"].ToString();
                cbo_ExCUI_Area.Text = rdr11["crdriver"].ToString();

                cbo_ExCUI_Pip.Text = rdr11["pip"].ToString();
                cbo_ExCUI_InsCon.Text = rdr11["inscon"].ToString();
                cbo_ExCUI_ChlrFree.Text = rdr11["chlrfree"].ToString();

                txt_ExCUI_Svi.Text = rdr11["Svi"].ToString();
                txt_ExCUI_Df.Text = rdr11["ExCUIDf"].ToString();

                btnExCUIDelete.Enabled = true;
            }
            else
            {
                btnExCUISubmit.ToolTip = "Save";
                ExCUIClear();
                lblStatus.Text = "This Component dont have the value now you can enter the new data";
                lblStatus.ForeColor = Color.Blue;
            }

            BusinessTier.DisposeReader(rdr11);
            BusinessTier.DisposeConnection(conn);
            btn_ExCUI_Save.Enabled = true;


        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btnExCLSSubmit_Click", ex.ToString(), "Audit");
            lblStatus.Text = ex.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    //<-------------------Stress Corrosion cracking Damage Factor Submit Buttons------------------------> 

    protected void btnCausticSubmit_Click(object sender, EventArgs e)
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

            string strqry1 = "SELECT * FROM [CausticCracking] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "'";
            SqlCommand cmd11 = new SqlCommand(strqry1, conn);
            SqlDataReader rdr11 = cmd11.ExecuteReader();

            if (rdr11.Read())
            {
                btnCausticSubmit.ToolTip = "Update";
                btn_Caustic_Save.ToolTip = rdr11["CauID"].ToString();
                txt_CausticAge.Text = rdr11["CSAge"].ToString();
                cbo_Caustic_InsEff.Text = Inspectcategory(rdr11["CSInsEff"].ToString()).ToString();
                cbo_Caustic_InsEff.SelectedValue = rdr11["CSInsEff"].ToString();

                cbo_noCausInspect.Text = rdr11["CSnofIns"].ToString();
                dt_Caustic_InspectDate.SelectedDate = Convert.ToDateTime(rdr11["InspectDate"].ToString());

                cbo_Caustic_Svi.Text = rdr11["CSSvi"].ToString();
                cbo_Caustic_Svi.SelectedValue = rdr11["CSSviVal"].ToString();
                txt_Caustic_Df.Text = rdr11["CSDf"].ToString();
            }
            else
            {
                btnCausticSubmit.ToolTip = "Save";
                CausticClear();
                lblStatus.Text = "This Component dont have the value now you can enter the new data";
                lblStatus.ForeColor = Color.Blue;
            }

            BusinessTier.DisposeReader(rdr11);
            BusinessTier.DisposeConnection(conn);
            btn_Caustic_Save.Enabled = true;
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btnCausticSubmit_Click", ex.ToString(), "Audit");
            //lblStatus.Text = ex.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    protected void btnAmineSubmit_Click(object sender, EventArgs e)
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

            string strqry1 = "SELECT * FROM [AmineCracking] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "'";
            SqlCommand cmd11 = new SqlCommand(strqry1, conn);
            SqlDataReader rdr11 = cmd11.ExecuteReader();

            if (rdr11.Read())
            {
                btnAmineSubmit.ToolTip = "Update";
                btn_Amn_Save.ToolTip = rdr11["AmnID"].ToString();
                txt_Amn_Age.Text = rdr11["AmAge"].ToString();
                cbo_Amn_InsEff.Text = Inspectcategory(rdr11["AmInsEff"].ToString()).ToString();
                cbo_Amn_InsEff.SelectedValue = rdr11["AmInsEff"].ToString();

                cbo_Amn_noIns.Text = rdr11["AmnofIns"].ToString();
                dt_Amn_InspectDate.SelectedDate = Convert.ToDateTime(rdr11["InspectDate"].ToString());

                cbo_Amn_Svi.Text = rdr11["AmSvi"].ToString();
                cbo_Amn_Svi.SelectedValue = rdr11["AmSviVal"].ToString();
                txt_Amn_Df.Text = rdr11["AmDf"].ToString();
            }
            else
            {
                btnAmineSubmit.ToolTip = "Save";
                AmnClear();
                lblStatus.Text = "This Component dont have the value now you can enter the new data";
                lblStatus.ForeColor = Color.Blue;
            }

            BusinessTier.DisposeReader(rdr11);
            BusinessTier.DisposeConnection(conn);
            btn_Amn_Save.Enabled = true;
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btnAmineSubmit_Click", ex.ToString(), "Audit");
            lblStatus.Text = ex.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    protected void btnSulSubmit_Click(object sender, EventArgs e)
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

            string strqry1 = "SELECT * FROM [SulfideCracking] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "'";
            SqlCommand cmd11 = new SqlCommand(strqry1, conn);
            SqlDataReader rdr11 = cmd11.ExecuteReader();

            if (rdr11.Read())
            {
                btnSulSubmit.ToolTip = "Update";
                btn_Sul_Save.ToolTip = rdr11["SulID"].ToString();

                txt_Sul_Age.Text = rdr11["SulAge"].ToString();
                cbo_Sul_InsEff.Text = Inspectcategory(rdr11["SulInsEff"].ToString()).ToString();
                cbo_Sul_InsEff.SelectedValue = rdr11["SulInsEff"].ToString();
                cbo_Sul_nofIns.Text = rdr11["SulnofIns"].ToString();
                dt_Sul_InspectDate.SelectedDate = Convert.ToDateTime(rdr11["InspectDate"].ToString());
                cbo_Sul_pH.Text = rdr11["pHwater"].ToString();
                cbo_sul_H2s.Text = rdr11["H2S"].ToString();

                txt_Sul_Env.Text = rdr11["Severity"].ToString();
                cbo_Sul_Heat.Text = rdr11["Heat"].ToString();
                cbo_Sul_Brin.Text = rdr11["Brinnell"].ToString();

                txt_Sul_Svi.Text = rdr11["SulSvi"].ToString();

                txt_Sul_Df.Text = rdr11["SulDf"].ToString();
            }
            else
            {
                btnSulSubmit.ToolTip = "Save";
                SulClear();
                lblStatus.Text = "This Component dont have the value now you can enter the new data";
                lblStatus.ForeColor = Color.Blue;
            }

            BusinessTier.DisposeReader(rdr11);
            BusinessTier.DisposeConnection(conn);
            btn_Sul_Save.Enabled = true;
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btnSulSubmit_Click", ex.ToString(), "Audit");
            lblStatus.Text = ex.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    protected void btnHICSubmit_Click(object sender, EventArgs e)
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

            string strqry1 = "SELECT * FROM [HIC] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "'";
            SqlCommand cmd11 = new SqlCommand(strqry1, conn);
            SqlDataReader rdr11 = cmd11.ExecuteReader();

            if (rdr11.Read())
            {
                btnHICSubmit.ToolTip = "Update";
                btn_HIC_Save.ToolTip = rdr11["HICID"].ToString();

                txt_HIC_Age.Text = rdr11["HICAge"].ToString();
                cbo_HIC_InsEff.Text = Inspectcategory(rdr11["HICInsEff"].ToString()).ToString();
                cbo_HIC_InsEff.SelectedValue = rdr11["HICInsEff"].ToString();
                cbo_HIC_nofIns.Text = rdr11["HICnofIns"].ToString();
                dt_HIC_InspectDate.SelectedDate = Convert.ToDateTime(rdr11["InspectDate"].ToString());
                cbo_HIC_pH.Text = rdr11["pHwater"].ToString();

                cbo_HIC_StSulf.Text = rdr11["SteelSul"].ToString();

                txt_HIC_Env.Text = rdr11["Severity"].ToString();
                cbo_HIC_heat.Text = rdr11["Heat"].ToString();
                cbo_HIC_Steel.Text = rdr11["steelsulf"].ToString();

                txt_HIC_Svi.Text = rdr11["HICSvi"].ToString();

                txt_HIC_DfSOHIC.Text = rdr11["HICDf"].ToString();
            }
            else
            {
                btnHICSubmit.ToolTip = "Save";
                HICClear();
                lblStatus.Text = "This Component dont have the value now you can enter the new data";
                lblStatus.ForeColor = Color.Blue;
            }

            BusinessTier.DisposeReader(rdr11);
            BusinessTier.DisposeConnection(conn);
            btn_HIC_Save.Enabled = true;
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btnHICSubmit_Click", ex.ToString(), "Audit");
            lblStatus.Text = ex.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    protected void btnCrbntSubmit_Click(object sender, EventArgs e)
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

            string strqry1 = "SELECT * FROM [CarbonateCracking] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "'";
            SqlCommand cmd11 = new SqlCommand(strqry1, conn);
            SqlDataReader rdr11 = cmd11.ExecuteReader();

            if (rdr11.Read())
            {
                btnCrbntSubmit.ToolTip = "Update";
                btn_Crbnt_Save.ToolTip = rdr11["CO3ID"].ToString();

                txt_Crbnt_Age.Text = rdr11["CO3Age"].ToString();
                cbo_Crbnt_InsEff.Text = Inspectcategory(rdr11["CO3InsEff"].ToString()).ToString();
                cbo_Crbnt_InsEff.SelectedValue = rdr11["CO3InsEff"].ToString();
                cbo_Crbnt_nofIns.Text = rdr11["CO3nofIns"].ToString();
                dt_Crbnt_InspectDate.SelectedDate = Convert.ToDateTime(rdr11["InspectDate"].ToString());
                cbo_Crbnt_pH.Text = rdr11["pHwater"].ToString();

                cbo_Crbnt_CO3.Text = rdr11["CO3"].ToString();

                txt_Crbnt_Svi.Text = rdr11["CO3Svi"].ToString();

                txt_Crbnt_Df.Text = rdr11["CO3Df"].ToString();
            }
            else
            {
                btnCrbntSubmit.ToolTip = "Save";
                CrbntClear();
                lblStatus.Text = "This Component dont have the value now you can enter the new data";
                lblStatus.ForeColor = Color.Blue;
            }

            BusinessTier.DisposeReader(rdr11);
            BusinessTier.DisposeConnection(conn);
            btn_Crbnt_Save.Enabled = true;
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btnCrbntSubmit_Click", ex.ToString(), "Audit");
            // lblStatus.Text = ex.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    protected void btnPTASubmit_Click(object sender, EventArgs e)
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

            string strqry1 = "SELECT * FROM [PTA] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "'";
            SqlCommand cmd11 = new SqlCommand(strqry1, conn);
            SqlDataReader rdr11 = cmd11.ExecuteReader();

            if (rdr11.Read())
            {
                btnPTASubmit.ToolTip = "Update";
                btn_PTA_Save.ToolTip = rdr11["PTAID"].ToString();

                txt_PTA_Age.Text = rdr11["PTAAge"].ToString();
                cbo_PTA_InsEff.Text = Inspectcategory(rdr11["PTAInsEff"].ToString()).ToString();
                cbo_PTA_InsEff.SelectedValue = rdr11["PTAInsEff"].ToString();
                cbo_PTA_nofIns.Text = rdr11["PTAnofIns"].ToString();
                dt_PTA_InspectDate.SelectedDate = Convert.ToDateTime(rdr11["InspectDate"].ToString());
                cbo_PTA_Material.Text = rdr11["Material"].ToString();

                cbo_PTA_Heat.Text = rdr11["Heat"].ToString();

                txt_PTA_Svi.Text = rdr11["PTASvi"].ToString();

                txt_PTA_Df.Text = rdr11["PTADf"].ToString();
            }
            else
            {
                btnPTASubmit.ToolTip = "Save";
                PTAClear();
                lblStatus.Text = "This Component dont have the value now you can enter the new data";
                lblStatus.ForeColor = Color.Blue;
            }

            BusinessTier.DisposeReader(rdr11);
            BusinessTier.DisposeConnection(conn);
            btn_PTA_Save.Enabled = true;
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btnPTASubmit_Click", ex.ToString(), "Audit");
            // lblStatus.Text = ex.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    protected void btnCLSSubmit_Click(object sender, EventArgs e)
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

            string strqry1 = "SELECT * FROM [CLSCC] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "'";
            SqlCommand cmd11 = new SqlCommand(strqry1, conn);
            SqlDataReader rdr11 = cmd11.ExecuteReader();

            if (rdr11.Read())
            {
                btnCLSSubmit.ToolTip = "Update";
                btn_CLS_Save.ToolTip = rdr11["CLSID"].ToString();

                txt_ClS_Age.Text = rdr11["CLSAge"].ToString();
                cbo_CLS_InsEff.Text = Inspectcategory(rdr11["CLSInsEff"].ToString()).ToString();
                cbo_CLS_InsEff.SelectedValue = rdr11["CLSInsEff"].ToString();
                cbo_CLS_nofIns.Text = rdr11["CLSnofIns"].ToString();
                dt_CLS_InspectDate.SelectedDate = Convert.ToDateTime(rdr11["InspectDate"].ToString());


                cbo_CLS_pH.Text = rdr11["pH"].ToString();
                cbo_CLS_Chl.Text = rdr11["Chldppm"].ToString();
                txt_CLS_Svi.Text = rdr11["CLSSvi"].ToString();

                txt_CLS_Df.Text = rdr11["CLSDf"].ToString();
            }
            else
            {
                btnCLSSubmit.ToolTip = "Save";
                CLSClear();
                lblStatus.Text = "This Component dont have the value now you can enter the new data";
                lblStatus.ForeColor = Color.Blue;
            }

            BusinessTier.DisposeReader(rdr11);
            BusinessTier.DisposeConnection(conn);
            btn_CLS_Save.Enabled = true;
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btnCLSSubmit_Click", ex.ToString(), "Audit");
            // lblStatus.Text = ex.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    protected void btnHSCSubmit_Click(object sender, EventArgs e)
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

            string strqry1 = "SELECT * FROM [HSC] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "'";
            SqlCommand cmd11 = new SqlCommand(strqry1, conn);
            SqlDataReader rdr11 = cmd11.ExecuteReader();

            if (rdr11.Read())
            {
                btnHSCSubmit.ToolTip = "Update";
                btn_HSC_Save.ToolTip = rdr11["HSCID"].ToString();

                txt_HSC_Age.Text = rdr11["HSCAge"].ToString();
                cbo_HSC_InsEff.Text = Inspectcategory(rdr11["HSCInsEff"].ToString()).ToString();
                cbo_HSC_InsEff.SelectedValue = rdr11["HSCInsEff"].ToString();
                cbo_HSC_nofIns.Text = rdr11["HSCnofIns"].ToString();
                dt_HSC_InspectDate.SelectedDate = Convert.ToDateTime(rdr11["InspectDate"].ToString());


                cbo_HSC_PWHT.Text = rdr11["pwht"].ToString();
                cbo_HSC_Brinnel.Text = rdr11["Brinnel"].ToString();
                txt_HSC_Svi.Text = rdr11["HSCSvi"].ToString();

                txt_HSC_Df.Text = rdr11["HSCDf"].ToString();
            }
            else
            {
                btnHSCSubmit.ToolTip = "Save";
                HSCClear();
                lblStatus.Text = "This Component dont have the value now you can enter the new data";
                lblStatus.ForeColor = Color.Blue;
            }

            BusinessTier.DisposeReader(rdr11);
            BusinessTier.DisposeConnection(conn);
            btn_HSC_Save.Enabled = true;
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btnHSCSubmit_Click", ex.ToString(), "Audit");
            // lblStatus.Text = ex.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    protected void btnHFSubmit_Click(object sender, EventArgs e)
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

            string strqry1 = "SELECT * FROM [HF] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "'";
            SqlCommand cmd11 = new SqlCommand(strqry1, conn);
            SqlDataReader rdr11 = cmd11.ExecuteReader();

            if (rdr11.Read())
            {
                btnHFSubmit.ToolTip = "Update";
                btn_HF_Save.ToolTip = rdr11["HFID"].ToString();

                txt_HF_Age.Text = rdr11["HFAge"].ToString();
                cbo_HF_InsEff.Text = Inspectcategory(rdr11["HFInsEff"].ToString()).ToString();
                cbo_HF_InsEff.SelectedValue = rdr11["HFInsEff"].ToString();
                cbo_HF_nofIns.Text = rdr11["HFnofIns"].ToString();
                dt_HF_InspectDate.SelectedDate = Convert.ToDateTime(rdr11["InspectDate"].ToString());


                cbo_HF_pwht.Text = rdr11["pwht"].ToString();
                cbo_HF_Mat.Text = rdr11["Material"].ToString();
                txt_HF_Svi.Text = rdr11["HFSvi"].ToString();

                txt_HF_Df.Text = rdr11["HFDf"].ToString();
            }
            else
            {
                btnHFSubmit.ToolTip = "Save";
                HFClear();
                lblStatus.Text = "This Component dont have the value now you can enter the new data";
                lblStatus.ForeColor = Color.Blue;
            }

            BusinessTier.DisposeReader(rdr11);
            BusinessTier.DisposeConnection(conn);
            btn_HF_Save.Enabled = true;
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btnHFSubmit_Click", ex.ToString(), "Audit");
            lblStatus.Text = ex.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    //<-------------------HTHA Damage Factor Submit Buttons------------------------> 

    protected void btnHTHASubmit_Click(object sender, EventArgs e)
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

            string strqry1 = "SELECT * FROM [HTHA] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "'";
            SqlCommand cmd11 = new SqlCommand(strqry1, conn);
            SqlDataReader rdr11 = cmd11.ExecuteReader();

            if (rdr11.Read())
            {
                btnHTHASubmit.ToolTip = "Update";
                btn_HTHA_Save.ToolTip = rdr11["HTHAID"].ToString();
                cbo_HTHA_insEff.Text = Inspectcategory(rdr11["InsEff"].ToString()).ToString();
                cbo_HTHA_insEff.SelectedValue = rdr11["InsEff"].ToString();
                cbo_HTHA_nofIns.Text = rdr11["nofins"].ToString();
                dt_HTHA_InspectDate.SelectedDate = Convert.ToDateTime(rdr11["InspectDate"].ToString());
                txt_HTHA_Age.Text = rdr11["age"].ToString();
                txt_HTHA_Extemp.Text = rdr11["ExTemp"].ToString();
                cbo_HTHA_Heat.Text = rdr11["Heat"].ToString();
                txt_HTHA_ph2.Text = rdr11["PH2"].ToString();
                txt_HTHA_pv.Text = rdr11["Pv"].ToString();
                cbo_HTHA_mat.Text = rdr11["Mat"].ToString();
                txt_HTHA_svi.Text = rdr11["Svi"].ToString();
                cbo_HTHA_1st2ndIns.Text = rdr11["inspection"].ToString();
                txt_HTHA_Df.Text = rdr11["HTHADf"].ToString();
            }
            else
            {
                btnHTHASubmit.ToolTip = "Save";
                HTHAClear();
                lblStatus.Text = "This Component dont have the value now you can enter the new data";
                lblStatus.ForeColor = Color.Blue;
            }

            BusinessTier.DisposeReader(rdr11);
            BusinessTier.DisposeConnection(conn);
            btn_HTHA_Save.Enabled = true;
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btnHTHASubmit_Click", ex.ToString(), "Audit");
            // lblStatus.Text = ex.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    protected void btnMechSubmit_Click(object sender, EventArgs e)
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

            string strqry1 = "SELECT * FROM [Mechanical_Fatigue] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "'";
            SqlCommand cmd11 = new SqlCommand(strqry1, conn);
            SqlDataReader rdr11 = cmd11.ExecuteReader();

            if (rdr11.Read())
            {
                btnMechSubmit.ToolTip = "Update";
                btn_Mech_Save.ToolTip = rdr11["MechID"].ToString();

                //  cbo_Mech_DFBPF.Text = rdr11["DfbPF"].ToString();

                if (rdr11["DfbPF"].ToString() == "1")
                {
                    cbo_Mech_DFBPF.Text = "None";
                    cbo_Mech_DFBPF.SelectedValue = rdr11["DfbPF"].ToString();
                }
                if (rdr11["DfbPF"].ToString() == "50")
                {
                    cbo_Mech_DFBPF.Text = "One";
                    cbo_Mech_DFBPF.SelectedValue = rdr11["DfbPF"].ToString();
                }
                if (rdr11["DfbPF"].ToString() == "500")
                {
                    cbo_Mech_DFBPF.Text = "Greater Than One";
                    cbo_Mech_DFBPF.SelectedValue = rdr11["DfbPF"].ToString();
                }


                //  cbo_Mech_DFBAS.Text = rdr11["DfbAS"].ToString();

                if (rdr11["DfbAS"].ToString() == "1")
                {
                    cbo_Mech_DFBAS.Text = "Minor";
                    cbo_Mech_DFBAS.SelectedValue = rdr11["DfbAS"].ToString();
                }
                if (rdr11["DfbAS"].ToString() == "50")
                {
                    cbo_Mech_DFBAS.Text = "Moderate";
                    cbo_Mech_DFBAS.SelectedValue = rdr11["DfbAS"].ToString();
                }
                if (rdr11["DfbAS"].ToString() == "500")
                {
                    cbo_Mech_DFBAS.Text = "Severe";
                    cbo_Mech_DFBAS.SelectedValue = rdr11["DfbAS"].ToString();
                }

                //  cbo_Mech_FFBAS.Text = rdr11["FfbAS"].ToString();

                if (Convert.ToDouble(rdr11["FfbAS"].ToString()) == 1.0)
                {
                    cbo_Mech_FFBAS.Text = "Shaking < 2 Weeks";
                    cbo_Mech_FFBAS.SelectedValue = rdr11["FfbAS"].ToString();
                }
                if (Convert.ToDouble(rdr11["FfbAS"].ToString()) == 0.2)
                {
                    cbo_Mech_FFBAS.Text = "Shaking 2-13 Weeks";
                    cbo_Mech_FFBAS.SelectedValue = rdr11["FfbAS"].ToString();
                }
                if (Convert.ToDouble(rdr11["FfbAS"].ToString()) == 0.02)
                {
                    cbo_Mech_FFBAS.Text = "Shaking 13-52 Weeks";
                    cbo_Mech_FFBAS.SelectedValue = rdr11["FfbAS"].ToString();
                }



                //cbo_Mech_DFBCF.Text = rdr11["DfbCF"].ToString();

                if (rdr11["DfbCF"].ToString() == "50")
                {
                    cbo_Mech_DFBCF.Text = "Reciprocating Machinery";
                    cbo_Mech_DFBCF.SelectedValue = rdr11["DfbCF"].ToString();
                }
                if (rdr11["DfbCF"].ToString() == "25")
                {
                    cbo_Mech_DFBCF.Text = "PRV Chatter";
                    cbo_Mech_DFBCF.SelectedValue = rdr11["DfbCF"].ToString();
                }
                if (rdr11["DfbCF"].ToString() == "10")
                {
                    cbo_Mech_DFBCF.Text = "Valve high drop press";
                    cbo_Mech_DFBCF.SelectedValue = rdr11["DfbCF"].ToString();
                }
                if (rdr11["DfbCF"].ToString() == "1")
                {
                    cbo_Mech_DFBCF.Text = "None";
                    cbo_Mech_DFBCF.SelectedValue = rdr11["DfbCF"].ToString();
                }



                txt_Mech_Dfb.Text = rdr11["Dfb"].ToString();
                // cbo_Mech_FCA.Text = rdr11["FCA"].ToString();

                if (Convert.ToDouble(rdr11["FCA"].ToString()) == 0.002)
                {
                    cbo_Mech_FCA.Text = "Mod Eng Analysis";
                    cbo_Mech_FCA.SelectedValue = rdr11["FCA"].ToString();
                }
                if (Convert.ToDouble(rdr11["FCA"].ToString()) == 0.2)
                {
                    cbo_Mech_FCA.Text = "Mod Experience";
                    cbo_Mech_FCA.SelectedValue = rdr11["FCA"].ToString();
                }
                if (Convert.ToDouble(rdr11["FCA"].ToString()) == 2.0)
                {
                    cbo_Mech_FCA.Text = "No Modification";
                    cbo_Mech_FCA.SelectedValue = rdr11["FCA"].ToString();
                }

                //cbo_Mech_FPC.Text = rdr11["FPC"].ToString();

                if (Convert.ToDouble(rdr11["FPC"].ToString()) == 0.5)
                {
                    cbo_Mech_FPC.Text = "0-5";
                    cbo_Mech_FPC.SelectedValue = rdr11["FPC"].ToString();
                }
                if (Convert.ToDouble(rdr11["FPC"].ToString()) == 1.0)
                {
                    cbo_Mech_FPC.Text = "6-10";
                    cbo_Mech_FPC.SelectedValue = rdr11["FPC"].ToString();
                }
                if (Convert.ToDouble(rdr11["FPC"].ToString()) == 2.0)
                {
                    cbo_Mech_FPC.Text = ">10";
                    cbo_Mech_FPC.SelectedValue = rdr11["FPC"].ToString();
                }

                //  cbo_Mech_FCP.Text = rdr11["FCP"].ToString();

                if (rdr11["FCP"].ToString() == "2.0")
                {
                    cbo_Mech_FCP.Text = "Missing Damage Improper Support";
                    cbo_Mech_FCP.SelectedValue = rdr11["FCP"].ToString();
                }
                if (rdr11["FCP"].ToString() == "2")
                {
                    cbo_Mech_FCP.Text = "Gusset Welded to Pipe";
                    cbo_Mech_FCP.SelectedValue = rdr11["FCP"].ToString();
                }
                if (rdr11["FCP"].ToString() == "1")
                {
                    cbo_Mech_FCP.Text = "Good Condition";
                    cbo_Mech_FCP.SelectedValue = rdr11["FCP"].ToString();
                }



                //cbo_Mech_FJP.Text = rdr11["FJB"].ToString();

                if (Convert.ToDouble(rdr11["FJB"].ToString()) == 1.0)
                {
                    cbo_Mech_FJP.Text = "Fitting Saddled";
                    cbo_Mech_FJP.SelectedValue = rdr11["FJB"].ToString();
                }
                if (Convert.ToDouble(rdr11["FJB"].ToString()) == 0.2)
                {
                    cbo_Mech_FJP.Text = "Tee/Weldolet";
                    cbo_Mech_FJP.SelectedValue = rdr11["FJB"].ToString();
                }
                if (Convert.ToDouble(rdr11["FJB"].ToString()) == 0.02)
                {
                    cbo_Mech_FJP.Text = "Sweepolets";
                    cbo_Mech_FJP.SelectedValue = rdr11["FJB"].ToString();
                }

                // cbo_Mech_FBD.Text = rdr11["FBD"].ToString();

                if (Convert.ToDouble(rdr11["FBD"].ToString()) == 1.0)
                {
                    cbo_Mech_FBD.Text = "<2NPS";
                    cbo_Mech_FBD.SelectedValue = rdr11["FBD"].ToString();
                }
                if (Convert.ToDouble(rdr11["FBD"].ToString()) == 0.02)
                {
                    cbo_Mech_FBD.Text = ">2NPS";
                    cbo_Mech_FBD.SelectedValue = rdr11["FBD"].ToString();
                }

                txt_Mech_Df.Text = rdr11["DfMech"].ToString();
            }
            else
            {
                btnMechSubmit.ToolTip = "Save";
                MechClear();
                lblStatus.Text = "This Component dont have the value now you can enter the new data";
                lblStatus.ForeColor = Color.Blue;
            }

            BusinessTier.DisposeReader(rdr11);
            BusinessTier.DisposeConnection(conn);
            btn_Mech_Save.Enabled = true;
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btnMechSubmit_Click", ex.ToString(), "Audit");
            // lblStatus.Text = ex.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    //<-------------------Brittle Facture Damage Factor Submit Buttons------------------------> 

    protected void btnBrittleSubmit_Click(object sender, EventArgs e)
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

            string strqry1 = "SELECT * FROM [Brittle_Facture] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "'";
            SqlCommand cmd11 = new SqlCommand(strqry1, conn);
            SqlDataReader rdr11 = cmd11.ExecuteReader();

            if (rdr11.Read())
            {
                btnBrittSubmit.ToolTip = "Update";
                btn_Britt_Save.ToolTip = rdr11["BritID"].ToString();

                cbo_Brittle_fulpressure.Text = rdr11["Fullpress"].ToString();
                txt_Britt_OpTemp.Text = rdr11["OpTemp"].ToString();

                txt_Britt_Dsgn.Text = rdr11["MinDsgn"].ToString();
                txt_Britt_ATM.Text = rdr11["ATM"].ToString();
                txt_Britt_Tmin.Text = rdr11["Tmin"].ToString();
                txt_Britt_Tref.Text = rdr11["Tref"].ToString();

                cbo_Britt_CmpThick.Text = rdr11["CmpThick"].ToString();
                cbo_Britt_PWHT.Text = rdr11["PWHT"].ToString();
                txt_Britt_Dfb.Text = rdr11["Dfb"].ToString();
                cbo_Britt_FSE.Text = rdr11["FSE"].ToString();
                txt_Britt_Df.Text = rdr11["Df"].ToString();

            }
            else
            {
                btnBrittSubmit.ToolTip = "Save";
                BrittClear();
                lblStatus.Text = "This Component dont have the value now you can enter the new data";
                lblStatus.ForeColor = Color.Blue;
            }

            BusinessTier.DisposeReader(rdr11);
            BusinessTier.DisposeConnection(conn);
            btn_Britt_Save.Enabled = true;
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btnBrittleSubmit_Click", ex.ToString(), "Audit");
            // lblStatus.Text = ex.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    protected void btnTemperSubmit_Click(object sender, EventArgs e)
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

            string strqry1 = "SELECT * FROM [Temper_Embrit] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "'";
            SqlCommand cmd11 = new SqlCommand(strqry1, conn);
            SqlDataReader rdr11 = cmd11.ExecuteReader();

            if (rdr11.Read())
            {
                btnTemperSubmit.ToolTip = "Update";
                btn_Temper_Save.ToolTip = rdr11["TemperID"].ToString();

                cbo_Temper_Fullpressure.Text = rdr11["Fullpress"].ToString();
                txt_Temper_optemp.Text = rdr11["OpTemp"].ToString();

                txt_Temper_MinDsgn.Text = rdr11["MinDsgn"].ToString();

                txt_Temper_Tmin.Text = rdr11["Tmin"].ToString();
                txt_Temper_Tref.Text = rdr11["Tref"].ToString();
                cbo_Temper_FATT.Text = rdr11["FATT"].ToString();
                txt_Temper_Fattval.Text = rdr11["fatval"].ToString();

                cbo_Temper_cmpThick.Text = rdr11["CmpThick"].ToString();
                cbo_Temper_PWHT.Text = rdr11["PWHT"].ToString();
                txt_Temper_Dfb.Text = rdr11["Dfb"].ToString();
                cbo_Temper_FSE.Text = rdr11["FSE"].ToString();
                txt_Temper_Df.Text = rdr11["DfTemp"].ToString();

            }
            else
            {
                btnTemperSubmit.ToolTip = "Save";
                TemperClear();
                lblStatus.Text = "This Component dont have the value now you can enter the new data";
                lblStatus.ForeColor = Color.Blue;
            }

            BusinessTier.DisposeReader(rdr11);
            BusinessTier.DisposeConnection(conn);
            btn_Temper_Save.Enabled = true;
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btnTemperSubmit_Click", ex.ToString(), "Audit");
            lblStatus.Text = ex.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    protected void btn885Submit_Click(object sender, EventArgs e)
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

            string strqry1 = "SELECT * FROM [EightEightFive] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "'";
            SqlCommand cmd11 = new SqlCommand(strqry1, conn);
            SqlDataReader rdr11 = cmd11.ExecuteReader();

            if (rdr11.Read())
            {
                btn885Submit.ToolTip = "Update";
                btn_885_Save.ToolTip = rdr11["ID885"].ToString();
                cbo_885_Fullpressure.Text = rdr11["Fullpress"].ToString();
                txt_885_Optemp.Text = rdr11["OpTemp"].ToString();
                txt_885_MinDsgn.Text = rdr11["MinDsgn"].ToString();
                txt_885_Tmin.Text = rdr11["Tmin"].ToString();
                txt_885_Tref.Text = rdr11["Tref"].ToString();
                cbo_885_Trefknown.Text = rdr11["BritTrans"].ToString();
                cbo_885_CmpThick.Text = rdr11["CmpThick"].ToString();
                txt_885_Df.Text = rdr11["Df885"].ToString();

            }
            else
            {
                btn885Submit.ToolTip = "Save";
                Clear885();
                lblStatus.Text = "This Component dont have the value now you can enter the new data";
                lblStatus.ForeColor = Color.Blue;
            }

            BusinessTier.DisposeReader(rdr11);
            BusinessTier.DisposeConnection(conn);
            btn_885_Save.Enabled = true;
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btn885Submit_Click", ex.ToString(), "Audit");
            lblStatus.Text = ex.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    protected void btnSigmaSubmit_Click(object sender, EventArgs e)
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

            string strqry1 = "SELECT * FROM [Sigma_Phase] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "'";
            SqlCommand cmd11 = new SqlCommand(strqry1, conn);
            SqlDataReader rdr11 = cmd11.ExecuteReader();

            if (rdr11.Read())
            {
                btnSigmaSubmit.ToolTip = "Update";
                btn_Sigma_Save.ToolTip = rdr11["SigmaID"].ToString();

                cbo_Sigma_Tmin.Text = rdr11["Tmin"].ToString();
                cbo_Sigma_Function.Text = rdr11["Sigma"].ToString();

                txt_Sigma_Df.Text = rdr11["DfSigma"].ToString();
            }
            else
            {
                btnSigmaSubmit.ToolTip = "Save";
                SigmaClear();
                lblStatus.Text = "This Component dont have the value now you can enter the new data";
                lblStatus.ForeColor = Color.Blue;
            }

            BusinessTier.DisposeReader(rdr11);
            BusinessTier.DisposeConnection(conn);
            btn_Sigma_Save.Enabled = true;
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btnSigmaSubmit_Click", ex.ToString(), "Audit");
            lblStatus.Text = ex.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    //<-------------------POF Total Submit Buttons------------------------> 

    protected void btnFinalSubmit_Click(object sender, EventArgs e)
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
           btnPOFTotalDelete .Enabled = true;
            //-----------------Df_britfract---------------------------
            double Df_sigma = 0.0;
            double Df_885 = 0.0;
            double Df_tempe = 0.0;
            double Df_britfract = 0.0;

            string strqry1 = "SELECT * FROM [Brittle_Facture] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "'";
            SqlCommand cmd1 = new SqlCommand(strqry1, conn);
            SqlDataReader rdr1 = cmd1.ExecuteReader();
            if (rdr1.Read())
            {
                Df_britfract = Convert.ToDouble(rdr1["Df"].ToString());
            }
            BusinessTier.DisposeReader(rdr1);

            string strqry2 = "SELECT * FROM [Temper_Embrit] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "'";
            SqlCommand cmd2 = new SqlCommand(strqry2, conn);
            SqlDataReader rdr2 = cmd2.ExecuteReader();
            if (rdr2.Read())
            {
                Df_tempe = Convert.ToDouble(rdr2["DfTemp"].ToString());
            }
            BusinessTier.DisposeReader(rdr2);

            string strqry3 = "SELECT * FROM [EightEightFive] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "'";
            SqlCommand cmd3 = new SqlCommand(strqry3, conn);
            SqlDataReader rdr3 = cmd3.ExecuteReader();
            if (rdr3.Read())
            {
                Df_885 = Convert.ToDouble(rdr3["Df885"].ToString());
            }
            BusinessTier.DisposeReader(rdr3);

            string strqry4 = "SELECT * FROM [Sigma_Phase] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "'";
            SqlCommand cmd4 = new SqlCommand(strqry4, conn);
            SqlDataReader rdr4 = cmd4.ExecuteReader();
            if (rdr4.Read())
            {
                Df_sigma = Convert.ToDouble(rdr4["DfSigma"].ToString());
            }
            BusinessTier.DisposeReader(rdr4);


            double Df_britgov = Math.Max(Df_britfract + Df_tempe, Math.Max(Df_sigma, Df_885));
            txt_Df_britgov.Text = Df_britgov.ToString("#.##");

            //-----------------Df_External---------------------------

            double Df_CUI_CLSCC = 0.0;
            double Df_ext_CLSCC = 0.0;
            double Df_CUIF = 0.0;
            double Df_extcor = 0.0;


            string strqry11 = "SELECT * FROM [ExCUI] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "'";
            SqlCommand cmd11 = new SqlCommand(strqry11, conn);
            SqlDataReader rdr11 = cmd11.ExecuteReader();
            if (rdr11.Read())
            {
                Df_CUI_CLSCC = Convert.ToDouble(rdr11["ExCUIDf"].ToString());
            }
            BusinessTier.DisposeReader(rdr11);

            string strqry21 = "SELECT * FROM [ExCLS] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "'";
            SqlCommand cmd21 = new SqlCommand(strqry21, conn);
            SqlDataReader rdr21 = cmd21.ExecuteReader();
            if (rdr21.Read())
            {
                Df_ext_CLSCC = Convert.ToDouble(rdr21["ExCLSDf"].ToString());
            }
            BusinessTier.DisposeReader(rdr21);

            string strqry31 = "SELECT * FROM [CUI] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "'";
            SqlCommand cmd31 = new SqlCommand(strqry31, conn);
            SqlDataReader rdr31 = cmd31.ExecuteReader();
            if (rdr31.Read())
            {
                Df_CUIF = Convert.ToDouble(rdr31["CUIDf"].ToString());
            }
            BusinessTier.DisposeReader(rdr31);

            string strqry41 = "SELECT * FROM [ECD] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "'";
            SqlCommand cmd41 = new SqlCommand(strqry41, conn);
            SqlDataReader rdr41 = cmd41.ExecuteReader();
            if (rdr41.Read())
            {
                Df_extcor = Convert.ToDouble(rdr41["ECDDf"].ToString());
            }
            BusinessTier.DisposeReader(rdr41);

            double Df_extdgov = Math.Max(Math.Max(Df_CUI_CLSCC, Df_ext_CLSCC), Math.Max(Df_CUIF, Df_extcor));
            txt_Df_extdgov.Text = Df_extdgov.ToString("#.##");

            //-----------------Df_Stress Corrosion---------------------------

            double Df_SOHIC_HF = 0.0;
            double Df_HSC_HF = 0.0;
            double Df_CLSCC = 0.0;
            double Df_PTA = 0.0;

            double Df_carbonate = 0.0;
            double Df_SOHIC_H2S = 0.0;
            double Df_ssc = 0.0;
            double Df_amine = 0.0;

            double Df_caustic = 0.0;

            string strqry12 = "SELECT * FROM [HF] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "'";
            SqlCommand cmd12 = new SqlCommand(strqry12, conn);
            SqlDataReader rdr12 = cmd12.ExecuteReader();
            if (rdr12.Read())
            {
                Df_SOHIC_HF = Convert.ToDouble(rdr12["HFDf"].ToString());
            }
            BusinessTier.DisposeReader(rdr12);

            string strqry22 = "SELECT * FROM [HSC] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "'";
            SqlCommand cmd22 = new SqlCommand(strqry22, conn);
            SqlDataReader rdr22 = cmd22.ExecuteReader();
            if (rdr22.Read())
            {
                Df_HSC_HF = Convert.ToDouble(rdr22["HSCDf"].ToString());
            }
            BusinessTier.DisposeReader(rdr22);

            string strqry32 = "SELECT * FROM [CLSCC] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "'";
            SqlCommand cmd32 = new SqlCommand(strqry32, conn);
            SqlDataReader rdr32 = cmd32.ExecuteReader();
            if (rdr32.Read())
            {
                Df_CLSCC = Convert.ToDouble(rdr32["CLSDf"].ToString());
            }
            BusinessTier.DisposeReader(rdr32);

            string strqry42 = "SELECT * FROM [PTA] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "'";
            SqlCommand cmd42 = new SqlCommand(strqry42, conn);
            SqlDataReader rdr42 = cmd42.ExecuteReader();
            if (rdr42.Read())
            {
                Df_PTA = Convert.ToDouble(rdr42["PTADf"].ToString());
            }
            BusinessTier.DisposeReader(rdr42);




            string strqry13 = "SELECT * FROM [CarbonateCracking] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "'";
            SqlCommand cmd13 = new SqlCommand(strqry13, conn);
            SqlDataReader rdr13 = cmd13.ExecuteReader();
            if (rdr13.Read())
            {
                Df_carbonate = Convert.ToDouble(rdr13["CO3Df"].ToString());
            }
            BusinessTier.DisposeReader(rdr13);

            string strqry23 = "SELECT * FROM [HIC] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "'";
            SqlCommand cmd23 = new SqlCommand(strqry23, conn);
            SqlDataReader rdr23 = cmd23.ExecuteReader();
            if (rdr23.Read())
            {
                Df_SOHIC_H2S = Convert.ToDouble(rdr23["HICDf"].ToString());
            }
            BusinessTier.DisposeReader(rdr23);

            string strqry33 = "SELECT * FROM [SulfideCracking] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "'";
            SqlCommand cmd33 = new SqlCommand(strqry33, conn);
            SqlDataReader rdr33 = cmd33.ExecuteReader();
            if (rdr33.Read())
            {
                Df_ssc = Convert.ToDouble(rdr33["SulDf"].ToString());
            }
            BusinessTier.DisposeReader(rdr33);

            string strqry43 = "SELECT * FROM [AmineCracking] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "'";
            SqlCommand cmd43 = new SqlCommand(strqry43, conn);
            SqlDataReader rdr43 = cmd43.ExecuteReader();
            if (rdr43.Read())
            {
                Df_amine = Convert.ToDouble(rdr43["AmDf"].ToString());
            }
            BusinessTier.DisposeReader(rdr43);

            string strqry15 = "SELECT * FROM [CausticCracking] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "'";
            SqlCommand cmd15 = new SqlCommand(strqry15, conn);
            SqlDataReader rdr15 = cmd15.ExecuteReader();
            if (rdr15.Read())
            {
                Df_caustic = Convert.ToDouble(rdr15["CSDf"].ToString());
            }
            BusinessTier.DisposeReader(rdr15);



            double Df_sccgov1 = Math.Max(Math.Max(Df_SOHIC_HF, Df_HSC_HF), Math.Max(Df_CLSCC, Df_PTA));
            double Df_sccgov2 = Math.Max(Math.Max(Df_carbonate, Df_SOHIC_H2S), Math.Max(Df_ssc, Df_amine));
            double Df_sccgov = Math.Max(Math.Max(Df_sccgov1, Df_sccgov2), Df_caustic);
            txt_Df_SCCgov.Text = Df_sccgov.ToString("#.##");

            //-----------------Df_Thinning---------------------------

            double Df_thin = 0.0;
            string Df_Thin_Type = string.Empty;
            double Df_elin = 0.0;
            double Df_thingov = 0.0;

            string strqry25 = "SELECT * FROM [ThinningDamage] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "'";
            SqlCommand cmd25 = new SqlCommand(strqry25, conn);
            SqlDataReader rdr25 = cmd25.ExecuteReader();
            if (rdr25.Read())
            {
                Df_thin = Convert.ToDouble(rdr25["Tdf"].ToString());
                Df_Thin_Type = rdr25["ThinType"].ToString();
            }
            BusinessTier.DisposeReader(rdr25);

            string strqry35 = "SELECT * FROM [LiningDamage] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "'";
            SqlCommand cmd35 = new SqlCommand(strqry35, conn);
            SqlDataReader rdr35 = cmd35.ExecuteReader();
            if (rdr35.Read())
            {
                Df_elin = Convert.ToDouble(rdr35["DfLine"].ToString());
            }
            BusinessTier.DisposeReader(rdr35);




            if (txt_Tdf.Text != string.Empty)
            {
                // Df_thin = Convert.ToDouble(txt_Tdf.Text.ToString());
            }
            if (txt_dfliner.Text != string.Empty)
            {
                //Df_elin = Convert.ToDouble(txt_dfliner.Text.ToString());
            }
            if (Df_elin.ToString() == string.Empty || Convert.ToDouble(Df_elin.ToString()) == 0.00000000)
            {
                Df_thingov = Df_thin;
            }
            else
            {
                Df_thingov = Math.Min(Df_thin, Df_elin);
            }
            txt_Df_Thinninggov.Text = Df_thingov.ToString("#.##");

            //------------------------Calculation----------------------

            double Df_htha = 0.0;
            double Df_mfat = 0.0;
            double Df_total = 0.0;

            string strqry36 = "SELECT * FROM [HTHA] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "'";
            SqlCommand cmd36 = new SqlCommand(strqry36, conn);
            SqlDataReader rdr36 = cmd36.ExecuteReader();
            if (rdr36.Read())
            {
                Df_htha = Convert.ToDouble(rdr36["HTHADf"].ToString());
            }

            BusinessTier.DisposeReader(rdr36);

            string strqry37 = "SELECT * FROM [Mechanical_Fatigue] where deleted=0 and [ProcID]='" + cboProcessArea.SelectedValue.ToString() + "' and [EquID]='" + cboEquipment.SelectedValue.ToString() + "' and [CompID]='" + cboComponent.SelectedValue.ToString() + "'";
            SqlCommand cmd37 = new SqlCommand(strqry37, conn);
            SqlDataReader rdr37 = cmd37.ExecuteReader();
            if (rdr37.Read())
            {
                Df_mfat = Convert.ToDouble(rdr37["DfMech"].ToString());
            }

            BusinessTier.DisposeReader(rdr37);


            //if (txt_HTHA_Df.Text != string.Empty)
            //{
            //    Df_htha = Convert.ToDouble(txt_HTHA_Df.Text.ToString());
            //}

            //if (txt_Mech_Df.Text != string.Empty)
            //{
            //    Df_mfat = Convert.ToDouble(txt_Mech_Df.Text.ToString());
            //}

            if (Df_Thin_Type.ToString() == "Local")
            {
                Df_total = Math.Max(Df_thingov, Df_extdgov) + Df_sccgov + Df_htha + Df_britgov + Df_mfat;
            }
            if (Df_Thin_Type.ToString() == "General")
            {
                Df_total = Df_thingov + Df_extdgov + Df_sccgov + Df_htha + Df_britgov + Df_mfat;
            }
            //if (cbo_thin_type.Text == string.Empty)
            //{
            //    lblStatus.Text = "Please Select Thinning Type";
            //}
            txt_Df_totalgov.Text = Df_total.ToString("#.##");

            BusinessTier.DisposeConnection(conn);
            btn_Final_Save.Enabled = true;

        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btnBrittleSubmit_Click", ex.ToString(), "Audit");
            // lblStatus.Text = ex.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    // Clear Funtion///

    public void ThinningClear()
    {
        btnThinningSubmit.ToolTip = "Save";
        cboclad.Text = "";
        //txt_Age.Text = "";
        // cbo_inspecEffec.Text = "";
        //cbo_inspecEffec.SelectedValue = "";
        //cbo_Thin_nofIns.ClearSelection();
        cbo_Thin_InspDate.SelectedDate = DateTime.Now;
        cbo_thin_type.Text = "";
        txt_art.Text = "";
        txt_Tdf.Text = "";
    }

    public void LiningClear()
    {

        cbo_Typelining.ClearSelection();
        cbo_AgeLining.ClearSelection();//  = "";
        cbo_Dfb.ClearSelection();
        cbo_Dfb.Items.Clear();
        cbo_liningcondition.ClearSelection();
        cbo_OnlineMonitoring.ClearSelection();
        txt_dfliner.Text = "";
    }

    public void ECDClear()
    {
        txt_ECD_Agetk.Text = "";
        cbo_ECD_InsEff.ClearSelection();

        dt_ECD_CmpInstal.Clear();
        dt_ECD_CalcDate.Clear();
        cbo_ECD_nofIns.ClearSelection();
        dt_ECD_InspectDate.Clear();
        cbo_ECD_CoatQual.ClearSelection();
        txt_ECD_AgeCoat.Text = "";
        txt_ECD_Age.Text = "";

        cbo_ECD_Fps.ClearSelection();
        cbo_ECD_Fip.ClearSelection();
        cbo_ECD_crdriver.ClearSelection();
        txt_ECD_cr.Text = "";
        txt_ECD_art.Text = "";
        txt_ECD_df.Text = "";
    }

    public void CUIClear()
    {
        txt_CUI_Agetk.Text = "";
        cbo_CUI_InsEff.ClearSelection();

        dt_CUI_CmpInstal.Clear();
        dt_CUI_CalcDate.Clear();
        cbo_CUI_nofIns.ClearSelection();
        dt_CUI_InspectDate.Clear();
        cbo_CUI_coatqual.ClearSelection();
        txt_CUI_Agecoat.Text = "";
        txt_CUI_Age.Text = "";

        cbo_CUI_Fps.ClearSelection();
        cbo_CUI_Fip.ClearSelection();
        cbo_CUI_crdriver.ClearSelection();
        txt_CUI_cr.Text = "";
        txt_CUI_art.Text = "";
        txt_CUI_Df.Text = "";

        cbo_CUI_Fins.ClearSelection();
        cbo_CUI_Fcm.ClearSelection();
        cbo_CUI_Fic.ClearSelection();
    }

    public void ExCLSClear()
    {

        txt_ExCLS_Agetk.Text = "";
        cbo_ExCLS_InsEff.ClearSelection();
        cbo_ExCLS_InsEff.ClearSelection();
        dt_ExCLS_CmpInstal.Clear();
        dt_ExCLS_CalcDate.Clear();
        cbo_ExCLS_nofIns.ClearSelection();
        dt_ExCLS_InspectDate.Clear();
        cbo_ExCLS_CoatQual.ClearSelection();
        txt_ExCLS_Age.Text = "";
        cbo_ExCLS_crdriver.ClearSelection();

        txt_ExCLS_Svi.Text = "";
        txt_ExCLS_Df.Text = "";

    }

    public void ExCUIClear()
    {

        txt_ExCUI_Agetk.Text = "";
        cbo_ExCUI_InsEff.ClearSelection();
        cbo_ExCUI_InsEff.ClearSelection();
        dt_ExCUI_CmpInstal.Clear();
        dt_ExCUI_CalcDate.Clear();
        cbo_ExCUI_nofIns.ClearSelection();
        dt_ExCUI_InspectDate.Clear();
        cbo_ExCUI_CoatQual.ClearSelection();
        cbo_ExCUI_CoatQual.Text = "";
        txt_ExCUI_Age.Text = "";
        cbo_ExCUI_Area.ClearSelection();

        cbo_ExCUI_Pip.ClearSelection();
        cbo_ExCUI_InsCon.ClearSelection();
        cbo_ExCUI_ChlrFree.ClearSelection();

        txt_ExCUI_Svi.Text = "";
        txt_ExCUI_Df.Text = "";

    }

    //<-------------------Stress Corrosion cracking Damage Factor Clear Funtion------------------------> 

    public void CausticClear()
    {

        txt_CausticAge.Text = "";
        cbo_Caustic_InsEff.ClearSelection();
        cbo_Caustic_InsEff.ClearSelection();

        cbo_noCausInspect.ClearSelection();
        dt_Caustic_InspectDate.Clear();

        cbo_Caustic_Svi.ClearSelection();
        txt_Caustic_Df.Text = "";

    }

    public void AmnClear()
    {

        txt_Amn_Age.Text = "";
        cbo_Amn_InsEff.ClearSelection();
        cbo_Amn_InsEff.ClearSelection();

        cbo_Amn_noIns.ClearSelection();
        dt_Amn_InspectDate.Clear();

        cbo_Amn_Svi.ClearSelection();
        txt_Amn_Df.Text = "";

    }

    public void SulClear()
    {
        txt_Sul_Age.Text = "";
        cbo_Sul_InsEff.Text = "";
        cbo_Sul_InsEff.ClearSelection();
        cbo_Sul_nofIns.ClearSelection();
        dt_Sul_InspectDate.Clear();
        cbo_Sul_pH.ClearSelection();
        cbo_sul_H2s.ClearSelection();

        txt_Sul_Env.Text = "";
        cbo_Sul_Heat.ClearSelection();
        cbo_Sul_Brin.ClearSelection();

        txt_Sul_Svi.Text = "";

        txt_Sul_Df.Text = "";

    }

    public void HICClear()
    {
        txt_HIC_Age.Text = "";
        cbo_HIC_InsEff.ClearSelection();
        cbo_HIC_nofIns.ClearSelection();
        dt_HIC_InspectDate.Clear();
        cbo_HIC_pH.ClearSelection();
        cbo_HIC_StSulf.ClearSelection();
        txt_HIC_Env.Text = "";
        cbo_HIC_heat.ClearSelection();
        cbo_HIC_Steel.ClearSelection();
        txt_HIC_Svi.Text = "";
        txt_HIC_DfSOHIC.Text = "";
    }

    public void CrbntClear()
    {
        txt_Crbnt_Age.Text = "";
        cbo_Crbnt_InsEff.ClearSelection();
        cbo_Crbnt_nofIns.ClearSelection();
        dt_Crbnt_InspectDate.Clear();
        cbo_Crbnt_pH.ClearSelection();
        cbo_Crbnt_CO3.ClearSelection();

        txt_Crbnt_Svi.Text = "";
        txt_Crbnt_Df.Text = "";
    }

    public void PTAClear()
    {
        txt_PTA_Age.Text = "";
        cbo_PTA_InsEff.ClearSelection();
        cbo_PTA_nofIns.ClearSelection();
        dt_PTA_InspectDate.Clear();
        cbo_PTA_Material.ClearSelection();
        cbo_PTA_Heat.ClearSelection();

        txt_PTA_Svi.Text = "";
        txt_PTA_Df.Text = "";
    }

    public void CLSClear()
    {
        txt_ClS_Age.Text = "";
        cbo_CLS_InsEff.ClearSelection();
        cbo_CLS_nofIns.ClearSelection();
        dt_CLS_InspectDate.Clear();
        cbo_CLS_pH.ClearSelection();
        cbo_CLS_Chl.ClearSelection();

        txt_CLS_Svi.Text = "";
        txt_CLS_Df.Text = "";
    }

    public void HSCClear()
    {
        txt_HSC_Age.Text = "";
        cbo_HSC_InsEff.ClearSelection();
        cbo_HSC_nofIns.ClearSelection();
        dt_HSC_InspectDate.Clear();
        cbo_HSC_PWHT.ClearSelection();
        cbo_HSC_Brinnel.ClearSelection();

        txt_HSC_Svi.Text = "";
        txt_HSC_Df.Text = "";
    }

    public void HFClear()
    {
        txt_HF_Age.Text = "";
        cbo_HF_InsEff.ClearSelection();
        cbo_HF_nofIns.ClearSelection();
        dt_HF_InspectDate.Clear();
        cbo_HF_pwht.ClearSelection();
        cbo_HF_Mat.ClearSelection();

        txt_HF_Svi.Text = "";
        txt_HF_Df.Text = "";
    }

    //<-------------------HTHA Damage Factor Clear Funtion------------------------> 

    public void HTHAClear()
    {

        cbo_HTHA_insEff.ClearSelection();
        cbo_HTHA_nofIns.ClearSelection();
        dt_HTHA_InspectDate.Clear();
        txt_HTHA_Age.Text = "";
        txt_HTHA_Extemp.Text = "";
        cbo_HTHA_Heat.ClearSelection();
        txt_HTHA_ph2.Text = "";
        cbo_HTHA_mat.ClearSelection();
        txt_HTHA_svi.Text = "";
        cbo_HTHA_1st2ndIns.ClearSelection();
        txt_HTHA_Df.Text = "";
        txt_HTHA_pv.Text = "";
    }

    public void MechClear()
    {

        cbo_Mech_DFBPF.ClearSelection();
        cbo_Mech_DFBAS.ClearSelection();

        cbo_Mech_FFBAS.ClearSelection();
        cbo_Mech_DFBCF.ClearSelection();
        txt_Mech_Dfb.Text = "";
        cbo_Mech_FCA.ClearSelection();
        cbo_Mech_FPC.ClearSelection();
        cbo_Mech_FCP.ClearSelection();
        cbo_Mech_FJP.ClearSelection();
        cbo_Mech_FBD.ClearSelection();
        txt_Mech_Df.Text = "";
    }

    //<-------------------Brittle Facture Damage Factor Clear Funtion------------------------> 

    public void BrittClear()
    {
        cbo_Brittle_fulpressure.ClearSelection();
        txt_Britt_OpTemp.Text = "";

        txt_Britt_Dsgn.Text = "";
        txt_Britt_ATM.Text = "";
        txt_Britt_Tmin.Text = "";
        txt_Britt_Tref.Text = "";

        cbo_Britt_CmpThick.ClearSelection();
        cbo_Britt_PWHT.ClearSelection();
        txt_Britt_Dfb.Text = "";
        cbo_Britt_FSE.ClearSelection();
        txt_Britt_Df.Text = "";
    }

    public void TemperClear()
    {
        cbo_Temper_Fullpressure.ClearSelection();
        txt_Temper_optemp.Text = "";

        txt_Temper_MinDsgn.Text = "";

        txt_Temper_Tmin.Text = "";
        txt_Temper_Tref.Text = "";
        cbo_Temper_FATT.ClearSelection();
        txt_Temper_Fattval.Text = "";

        cbo_Temper_cmpThick.ClearSelection();
        cbo_Temper_PWHT.ClearSelection();
        txt_Temper_Dfb.Text = "";
        cbo_Temper_FSE.ClearSelection();
        txt_Temper_Df.Text = "";

    }

    public void Clear885()
    {
        cbo_885_Fullpressure.ClearSelection();
        txt_885_Optemp.Text = "";
        txt_885_MinDsgn.Text = "";
        txt_885_Tmin.Text = "";
        txt_885_Tref.Text = "";
        cbo_885_Trefknown.ClearSelection();
        cbo_885_CmpThick.ClearSelection();
        txt_885_Df.Text = "";

    }

    public void SigmaClear()
    {
        cbo_Sigma_Tmin.ClearSelection();
        cbo_Sigma_Function.ClearSelection();

        txt_Sigma_Df.Text = "";
    }

    //<-------------------Final Clear Funtion------------------------> 

    public void FinalClear()
    {

        txt_Df_Thinninggov.Text = "";
        txt_Df_SCCgov.Text = "";
        txt_Df_extdgov.Text = "";
        txt_Df_britgov.Text = "";

        txt_Df_totalgov.Text = "";
        txt_Category.Text = "";

    }

    public static string Inspectcategory(string Val)
    {
        string ret = "";

        if (Val == "A")
            ret = "Highly Effective";
        else if (Val == "B")
            ret = "Usually Effective";
        else if (Val == "C")
            ret = "Fairly Effective";
        else if (Val == "D")
            ret = "Poorly Effective";
        else
            ret = "In Effective";

        return ret;
    }

    private void InsertLogAuditTrail(string userid, string module, string activity, string result, string flag)
    {
        SqlConnection connLog = BusinessTier.getConnection();
        connLog.Open();
        BusinessTier.InsertLogAuditTrial(connLog, userid, module, activity, result, flag);
        BusinessTier.DisposeConnection(connLog);
    }

    //<-------------------Delete------------------------> 

    protected void btnThiningDelete_Click(object sender, EventArgs e)
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
            string damagefactor = string.Empty;
            if (cbo_thin_type.Text == "General")
            {
                damagefactor = "Thinning";
            }
            else
            {
                damagefactor = "ThinningL";
            }
            //string strqrydup = "update COF_Flammable set Deleted=1 where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' and  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' and CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and Deleted=0";
            string strqrydup = "delete from ThinningDamage where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' and  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' and CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and Deleted=0";

            SqlCommand cmddup = new SqlCommand(strqrydup, conn);
            cmddup.ExecuteNonQuery();

            string strqrydel = "delete from InspectionPlan where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' and  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' and CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and DamageFact='" + damagefactor.ToString().Trim() + "'";

            SqlCommand cmddel = new SqlCommand(strqrydel, conn);
            cmddel.ExecuteNonQuery();

            BusinessTier.DisposeConnection(conn);

            lblStatus.Text = "Thinning Value Successfully Deleted";
            lblStatus.ForeColor = Color.Maroon;
            btnThiningDelete.Enabled = false;
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btnThiningDelete_Click", ex.ToString(), "Audit");
            lblStatus.Text = ex.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    protected void btnECDDelete_Click(object sender, EventArgs e)
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
            string strqrydup = "delete from ECD where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' and  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' and CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and Deleted=0";

            SqlCommand cmddup = new SqlCommand(strqrydup, conn);
            cmddup.ExecuteNonQuery();

            string strqrydel = "delete from InspectionPlan where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' and  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' and CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and DamageFact='ExternalCorrosion'";

            SqlCommand cmddel = new SqlCommand(strqrydel, conn);
            cmddel.ExecuteNonQuery();

            BusinessTier.DisposeConnection(conn);

            lblStatus.Text = "ECD Value Successfully Deleted";
            lblStatus.ForeColor = Color.Maroon;
            btnECDDelete.Enabled = false;
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btnECDDelete_Click", ex.ToString(), "Audit");
            lblStatus.Text = ex.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    protected void btnCUIDelete_Click(object sender, EventArgs e)
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
            string strqrydup = "delete from CUI where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' and  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' and CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and Deleted=0";

            SqlCommand cmddup = new SqlCommand(strqrydup, conn);
            cmddup.ExecuteNonQuery();

            string strqrydel = "delete from InspectionPlan where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' and  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' and CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and DamageFact='CUIDamage'";

            SqlCommand cmddel = new SqlCommand(strqrydel, conn);
            cmddel.ExecuteNonQuery();

            lblStatus.Text = "ExCUI Value Successfully Deleted";
            lblStatus.ForeColor = Color.Maroon;
            btnCUIDelete.Enabled = false;
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btnCUIDelete_Click", ex.ToString(), "Audit");
            lblStatus.Text = ex.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }
   
    protected void btnExCLSSDelete_Click(object sender, EventArgs e)
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
            string strqrydup = "delete from ExCLS where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' and  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' and CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and Deleted=0";

            SqlCommand cmddup = new SqlCommand(strqrydup, conn);
            cmddup.ExecuteNonQuery();

            string strqrydel = "delete from InspectionPlan where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' and  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' and CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and DamageFact='ExternalCLSCC'";

            SqlCommand cmddel = new SqlCommand(strqrydel, conn);
            cmddel.ExecuteNonQuery();

            BusinessTier.DisposeConnection(conn);


            lblStatus.Text = "ExCLS Value Successfully Deleted";
            lblStatus.ForeColor = Color.Maroon;
            btnExCLSSDelete.Enabled = false;
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btnExCLSSDelete_Click", ex.ToString(), "Audit");
            lblStatus.Text = ex.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    protected void btnExCUIDelete_Click(object sender, EventArgs e)
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
            string strqrydup = "delete from ExCUI where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' and  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' and CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and Deleted=0";

            SqlCommand cmddup = new SqlCommand(strqrydup, conn);
            cmddup.ExecuteNonQuery();

            string strqrydel = "delete from InspectionPlan where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' and  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' and CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and DamageFact='External CUI CLSCC'";

            SqlCommand cmddel = new SqlCommand(strqrydel, conn);
            cmddel.ExecuteNonQuery();

            lblStatus.Text = "ExCUI CLSCC Value Successfully Deleted";
            lblStatus.ForeColor = Color.Maroon;
            btnExCUIDelete.Enabled = false;
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btnExCUIDelete_Click", ex.ToString(), "Audit");
            lblStatus.Text = ex.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    protected void btnPOFTotalDelete_Click(object sender, EventArgs e)
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
            string strqrydup = "delete from POF_Total where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' and  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' and CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and Deleted=0";

            SqlCommand cmddup = new SqlCommand(strqrydup, conn);
            cmddup.ExecuteNonQuery();

            BusinessTier.DisposeConnection(conn);
            lblStatus.Text = "POF Total Value Successfully Deleted";
            lblStatus.ForeColor = Color.Maroon;
            btnPOFTotalDelete.Enabled = false;
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "POF", "btnPOFTotalDelete_Click", ex.ToString(), "Audit");
            lblStatus.Text = ex.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    //--------------------------function for inspectionplan ------------------------>

    public static int InspectionPlan(int ProcessArea, int Equipment, int Component, string damagefactor, string inspecEffec, DateTime InspDate)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();

        string strqry1 = "SELECT * FROM [InspectionPlan] where [ProcID]='" + ProcessArea.ToString() + "' and [EquID]='" + Equipment.ToString() + "' and [CompID]='" + Component.ToString() + "' and DamageFact='" + damagefactor.ToString() + "'";
        SqlCommand cmd11 = new SqlCommand(strqry1, conn);
        SqlDataReader rdr11 = cmd11.ExecuteReader();

        if (rdr11.Read())
        {
            rdr11.Close();
            int intFlag1 = BusinessTier.InspectionPlan(conn, ProcessArea, Equipment, Component, damagefactor, inspecEffec, InspDate, "U");
        }
        else
        {

            rdr11.Close();
            int intFlag1 = BusinessTier.InspectionPlan(conn, ProcessArea, Equipment, Component, damagefactor, inspecEffec, InspDate, "N");
        }

        return 1;

    }

}