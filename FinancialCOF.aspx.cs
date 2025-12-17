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


public partial class FinancialCOF : System.Web.UI.Page
{
    public DataTable dtMenuItems = new DataTable();

    public DataTable dtSubMenuItems = new DataTable();

    protected void Page_Init(object sender, EventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            string sql1 = "select Material from Ref_Finan_MaterialCost";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            cbo_MatCost.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["Material"].ToString();
                cbo_MatCost.Items.Add(item);
                item.DataBind();
            }
            lblName.Text = "Hi, " + Session["sesUserName"].ToString();
        }
        catch (Exception ex)
        {
            BusinessTier.DisposeConnection(conn);
            //lblStatus.Text = ex.Message.ToString();
            return;
        }
        //try
        //{
        //    if (!(string.IsNullOrEmpty(Session["sesUserID"].ToString())))
        //    {
        //        SqlConnection connMenu = BusinessTier.getConnection();
        //        connMenu.Open();
        //        SqlDataReader readerMenu = BusinessTier.getMenuList(connMenu, Session["sesUserID"].ToString());
        //        dtMenuItems.Load(readerMenu);
        //        BusinessTier.DisposeReader(readerMenu);
        //        BusinessTier.DisposeConnection(connMenu);
        //    }
        //    else
        //    {
        //        Response.Redirect("Login.aspx");
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Response.Redirect("Login.aspx");
        //}
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
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "FinancialCOF", "cboEqualComponent_OnItemsRequested", ex.ToString(), "Audit");
            lblStatus.Text = ex.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    protected void cboEqualComponent_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {

        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {

            string sql1 = "select * from Ref_Finan_DamageCost";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["ComponentType"].ToString();
               // item.Value = row["ProcessAreaID"].ToString();
                comboBox.Items.Add(item);
                item.DataBind();
            }
            adapter1.Dispose();
            BusinessTier.DisposeConnection(conn);

        }

        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "FinancialCOF", "cboEqualComponent_OnItemsRequested", ex.ToString(), "Audit");
            lblStatus.Text = ex.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
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
           // lblStatus.Text = "Err:Select correct Product Item" + ex.Message.ToString();
            return;
        }


    }

    protected void OnSelectedIndexChanged_CompCmb(object sender, RadComboBoxSelectedIndexChangedEventArgs e) 
    {
       
    }

    protected void OnSelectedIndexChanged_cbo_MatCost(object sender, RadComboBoxSelectedIndexChangedEventArgs e) 
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            //--------------For Material Cost
            string sql1 = "select Matcost from Ref_Finan_MaterialCost where Material='" + cbo_MatCost.SelectedItem.Text.ToString() + "'";
            SqlCommand cmd = new SqlCommand(sql1, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            rd.Read();
            txt_Fin_MatCost.Text = rd["MatCost"].ToString();
            rd.Close();
            //--------------For FC cmd
            double HCsmall = Convert.ToDouble(txt_Fin_HCSmall.Text.ToString()) * 0.261;
            double HCmedium = Convert.ToDouble(txt_Fin_HCMedium.Text.ToString()) * 0.654;
            double HClarge = Convert.ToDouble(txt_Fin_HCLarge.Text.ToString()) * 0.065;
            double HCrupture = Convert.ToDouble(txt_Fin_HCRupture.Text.ToString()) * 0.020;
            double Matcost = Convert.ToDouble(txt_Fin_MatCost.Text.ToString());

            double FCcmd = (HCsmall + HCmedium + HClarge + HCrupture) * Matcost;
            txt_Fin_FCcmd.Text = FCcmd.ToString();
            //--------------For CAcmd & CAInj
            //---------------Flammable
            string sql2 = "select CAcmdTotal,CAInjTotal from COF_Flammable where EquID=" + cboEquipment.SelectedValue.ToString() + " and CompID=" + cboComponent.SelectedValue.ToString() + "  and deleted=0";
            SqlCommand cmd1 = new SqlCommand(sql2, conn);
            SqlDataReader rd1 = cmd1.ExecuteReader();
            if (rd1.Read())
            {
                if (string.IsNullOrEmpty(rd1["CAcmdTotal"].ToString()))
                {
                    txt_Fin_CAcmd.Text = "1";
                }
                else 
                {
                    txt_Fin_CAcmd.Text = Convert.ToDouble(rd1["CAcmdTotal"].ToString()).ToString("#.##");
                   
                }
                if (string.IsNullOrEmpty(rd1["CAInjTotal"].ToString()))
                {
                    txt_Fin_CAInj.Text = "1";
                }
                else
                {

                    txt_Fin_CAInj.Text = Convert.ToDouble(rd1["CAInjTotal"].ToString()).ToString("#.##");
                }
            }
            rd1.Close();

            //----------------Non-Flammable

            string sql3 = "select CAInjTotal from COF_NonFlammable where EquID=" + cboEquipment.SelectedValue.ToString() + " and CompID=" + cboComponent.SelectedValue.ToString() + "  and deleted=0";
            SqlCommand cmd3 = new SqlCommand(sql3, conn);
            SqlDataReader rd2 = cmd3.ExecuteReader();
            if (rd2.Read())
            {
                txt_Fin_CAcmd.Text = "1";
                if (string.IsNullOrEmpty(rd2["CAInjTotal"].ToString()))
                {
                    txt_Fin_CAInj.Text = "1";
                }
                else
                {

                    txt_Fin_CAInj.Text = Convert.ToDouble(rd2["CAInjTotal"].ToString()).ToString("#.##");
                }
            }
            rd2.Close();

            //--------------For Outage cmd
            double Outsmall = Convert.ToDouble(txt_Fin_OutSmall.Text.ToString()) * 0.261;
            double Outmedium = Convert.ToDouble(txt_Fin_OutMedium.Text.ToString()) * 0.654;
            double Outlarge = Convert.ToDouble(txt_Fin_OutLarge.Text.ToString()) * 0.065;
            double Outrupture = Convert.ToDouble(txt_Fin_OutRupture.Text.ToString()) * 0.020;
            double OutMulti = 1;

            double Outagecmd = (Outsmall + Outmedium + Outlarge + Outrupture) * OutMulti;
            txt_Fin_Outagecmd.Text = Outagecmd.ToString();
            BusinessTier.DisposeConnection(conn);
        }
        catch (Exception ex)
        {
            BusinessTier.DisposeConnection(conn);
         //   lblStatus.Text = ex.Message.ToString();
            return;
        }
    }

    protected void btn_Fin_Calculate_Click(object sender, EventArgs e)
    {
        try
        {
            //--------------For FC Inj
            double PopDens = Convert.ToDouble(txt_Fin_PopDensity.Text.ToString());
            double InjCost = Convert.ToDouble(txt_Fin_injcost.Text.ToString());
            double CAInj = Convert.ToDouble(txt_Fin_CAInj.Text.ToString());
            double FCInj = CAInj * PopDens * InjCost;
            txt_Fin_FCInj.Text = FCInj.ToString("#.##");
            //--------------For SUMP FC
            double FCcmd = Convert.ToDouble(txt_Fin_FCcmd.Text.ToString());
            double FCaffa = Convert.ToDouble(txt_Fin_FCaffa.Text.ToString());
            double FCprod = Convert.ToDouble(txt_Fin_FCProd.Text.ToString());
            double FCenv = Convert.ToDouble(txt_Fin_FCEnv.Text.ToString());
            double SUMPFC = FCcmd + FCaffa + FCprod + FCInj + FCenv;
            txt_Fin_SUMPFC.Text = SUMPFC.ToString("#.##");
            //--------------For Category
            string cate = string.Empty; 
            if (SUMPFC < 10000)
            {
                cate = "A";
                }
            else if (SUMPFC >= 10000 && SUMPFC < 100000)
            {
                cate = "B";
            }
            else if (SUMPFC >= 100000 && SUMPFC < 1000000)
            {
                cate = "C";
            }
            else if (SUMPFC >= 1000000 && SUMPFC < 10000000)
            {
                cate = "D";
            }
            else if (SUMPFC > 10000000)
            {
                cate = "E";
            }
            txt_Fin_Category.Text = cate;
        }
        catch (Exception ex)
        {
         //   lblStatus.Text = ex.Message.ToString();
            return;
        }
    }

    protected void btn_Fin_Save_Click(object sender, EventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            string flag = string.Empty;
                    string strqrydup = "Select  *  from FinanceCOF  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' and  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' and CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0"; 
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

                    int intFlag = BusinessTier.Fin_COF_Save(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString()), Convert.ToInt32(cboEquipment.SelectedValue.ToString()), Convert.ToInt32(cboComponent.SelectedValue.ToString()), cboEquipment.SelectedItem.Attributes["EqupID"].ToString(), cboComponent.SelectedItem.Attributes["CompName"].ToString(), txt_Fin_HCSmall.Text.ToString(), txt_Fin_HCMedium.Text.ToString(), txt_Fin_HCLarge.Text.ToString(), txt_Fin_HCRupture.Text.ToString(), txt_Fin_OutSmall.Text.ToString(), txt_Fin_OutMedium.Text.ToString(), txt_Fin_OutLarge.Text.ToString(), txt_Fin_OutRupture.Text.ToString(), txt_Fin_MatCost.Text.ToString(), txt_Fin_FCcmd.Text.ToString(), txt_Fin_CAcmd.Text.ToString(), txt_Fin_Outagecmd.Text.ToString(), txt_Fin_CAInj.Text.ToString(), txt_Fin_ECostDollar.Text.ToString(), txt_Fin_ECostft2.Text.ToString(), txt_Fin_FCaffa.Text.ToString(), txt_Fin_ProdCst.Text.ToString(), txt_Fin_Outageaffa.Text.ToString(), txt_Fin_FCProd.Text.ToString(), txt_Fin_injcost.Text.ToString(), txt_Fin_FCInj.Text.ToString(), txt_Fin_SUMPFC.Text.ToString(), txt_Fin_Category.Text.ToString(), Convert.ToInt32(Session["sesUserID"].ToString()), flag);
           
            BusinessTier.DisposeConnection(conn);
        }
        catch (Exception ex)
        {
            BusinessTier.DisposeConnection(conn);
           // lblStatus.Text = ex.Message.ToString();
            lblStatus.ForeColor = Color.Blue;
            return;
        }
    }

    protected void btn_Fin_FCaffa_Click(object sender, EventArgs e)
    {
        try
        {
            double CAcmd = Convert.ToDouble(txt_Fin_CAcmd.Text.ToString());
            double EquiCostDollar = Convert.ToDouble(txt_Fin_ECostDollar.Text.ToString());
            double EquipArea = Convert.ToDouble(txt_Fin_EquipArea.Text.ToString());
            double EquipCost = EquiCostDollar / EquipArea;
            txt_Fin_ECostft2.Text = EquipCost.ToString();
            double EquiCostft2 = Convert.ToDouble(txt_Fin_ECostft2.Text.ToString());

            double FCaffa = CAcmd * EquiCostft2;
            txt_Fin_FCaffa.Text = FCaffa.ToString("#.##");
        }
        catch (Exception ex)
        {
         //   lblStatus.Text = ex.Message.ToString();
            return;
        }
    }

    protected void btn_Fin_Outageaffa_Click(object sender, EventArgs e)
    {
        // --------------For Outageaffa
        double a, b, c, d, Outageaffa;
        a = 1.242;
        b = 0.585;
        double FCaffa = Convert.ToDouble(txt_Fin_FCaffa.Text.ToString());
        c = Math.Log10(FCaffa * Math.Pow(10, -6));
        d = a + b * c;
        Outageaffa = Math.Pow(10, d);
        txt_Fin_Outageaffa.Text =Math.Round(Outageaffa,2).ToString();

        // --------------For FC Product
        double Outcmd = Convert.ToDouble(txt_Fin_Outagecmd.Text.ToString());
        double ProdCost = Convert.ToDouble(txt_Fin_ProdCst.Text.ToString());
        double FCProd = (Outcmd + Outageaffa) * ProdCost;
        txt_Fin_FCProd.Text = Math.Round(FCProd, 2).ToString();
    }

    protected void btnFinCOFSubmit_Click(object sender, EventArgs e) 
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            lblStatus.Text = string.Empty;
            string CompName = cboEqualComponent.Text.ToString();
            string sql1 = "select Small,Medium,Large,Rupture from Ref_Finan_DamageCost where ComponentType='" + CompName.ToString().Trim() + "'";
            SqlCommand cmd = new SqlCommand(sql1, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            rd.Read();
            txt_Fin_HCSmall.Text = rd["Small"].ToString();
            txt_Fin_HCMedium.Text = rd["Medium"].ToString();
            txt_Fin_HCLarge.Text = rd["Large"].ToString();
            txt_Fin_HCRupture.Text = rd["Rupture"].ToString();
            rd.Close();
            string sql2 = "select Small,Medium,Large,Rupture from Ref_Finan_Outage where ComponentType='" + CompName.ToString().Trim() + "'";
            SqlCommand cmd1 = new SqlCommand(sql2, conn);
            SqlDataReader rd1 = cmd1.ExecuteReader();
            rd1.Read();
            txt_Fin_OutSmall.Text = rd1["Small"].ToString();
            txt_Fin_OutMedium.Text = rd1["Medium"].ToString();
            txt_Fin_OutLarge.Text = rd1["Large"].ToString();
            txt_Fin_OutRupture.Text = rd1["Rupture"].ToString();
            rd1.Close();
            BusinessTier.DisposeConnection(conn);
        }
        catch (Exception ex)
        {
            BusinessTier.DisposeConnection(conn);
            // lblStatus.Text = ex.Message.ToString();
            return;
        }
    }

    private void InsertLogAuditTrail(string userid, string module, string activity, string result, string flag)
    {
        SqlConnection connLog = BusinessTier.getConnection();
        connLog.Open();
        BusinessTier.InsertLogAuditTrial(connLog, userid, module, activity, result, flag);
        BusinessTier.DisposeConnection(connLog);
    }
}