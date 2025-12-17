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


public partial class COF : System.Web.UI.Page
{
    public DataTable dtMenuItems = new DataTable();

    public DataTable dtSubMenuItems = new DataTable();

    protected void Page_Init(object sender, EventArgs e)
    {
        txt_COF_P1.Enabled = false;
        tab_Flammable.Visible = false;
        tab_NonFlammable.Visible = false;
        btnCOFFlameDelete.Enabled = false;
        btnCOFNonFlameDelete.Enabled = false;
        //tab_SCCDF.Visible = false;
        //tab_Lining.Visible = false;
        //tab_ExternalDamage.Visible = false;
        //tab_HTHA.Visible = false;
        //tab_Brit_Fract.Visible = false;
        //tab_Mech_Fati.Visible = false;
        //cbo_ExCUI_InsCon.Enabled = false;
        //cbo_ExCUI_ChlrFree.Enabled = false;
        //txt_Temper_Fattval.Enabled = false;
        //txt_885_Tref.Enabled = false;
        //cboclad.Enabled = false;
        //cboclad.EmptyMessage = "Select";
        //lbl_Desc.Visible = false;
        // btn_Calculate.Enabled = false;
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

    //<-------------------Button Clicks------------------------>

    protected void btn_Flammable_Click(object sender, EventArgs e)
    {
        btn_Flammable.ForeColor = Color.Red;
        btn_NonFlammable.ForeColor = Color.Black;
        tab_NonFlammable.Visible = false;
        tab_Flammable.Visible = true;
    }

    protected void btn_NonFlammable_Click(object sender, EventArgs e)
    {
        btn_Flammable.ForeColor = Color.Black;
        btn_NonFlammable.ForeColor = Color.Red;
        tab_NonFlammable.Visible = true;
        tab_Flammable.Visible = false;
    }

    //<-------------------Flammable------------------------>

    protected void OnSelectedIndexChanged_FluidStored(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        RadComboBox combobox = (RadComboBox)sender;
        if (cbo_FluidStored.SelectedItem.Text.ToString() == "Liquid")
        {
            txt_COF_P1.Enabled = true;
        }
        else
        {
            txt_COF_P1.Enabled = false;
            txt_COF_P1.Text = string.Empty;
        }
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            string sql1 = "select Fluid from Ref_COF_Lvl_1";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            cbo_RepFluid.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["Fluid"].ToString();
                cbo_RepFluid.Items.Add(item);
                item.DataBind();
            }

        }
        catch (Exception ex)
        {
            BusinessTier.DisposeConnection(conn);
          //  lblStatus.Text = ex.Message.ToString();
            return;
        }
    }

    protected void OnSelectedIndexChanged_RepFluid(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            lblStatus.Text = string.Empty;
            string sql1 = "select Fluid_Type from Ref_COF_Rep_Fluids where Rep_Fluid='" + cbo_RepFluid.SelectedItem.Text.ToString().Trim() + "'";
            SqlCommand cmd = new SqlCommand(sql1, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            rd.Read();
            txt_FluidType.Text = rd["Fluid_Type"].ToString().Trim();
            rd.Close();
            BusinessTier.DisposeConnection(conn);
        }
        catch (Exception ex)
        {
            BusinessTier.DisposeConnection(conn);
         //   lblStatus.Text = ex.Message.ToString();
            return;

        }

    }

    protected void OnSelectedIndexChanged_Detection(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {

        if (cbo_COF_Detection.SelectedItem.Text == "A")
        {
            lbl_COF_Detection.Text = "Instrumentation designed specially to detect material losses by chenges in operating conditions (i.e., loss of pressure or flow) in the system.";
        }
        else if (cbo_COF_Detection.SelectedItem.Text == "B")
        {
            lbl_COF_Detection.Text = "Suitably locate detectors to determine when the matrial is present outside the pressure-containing envelope.";
        }
        else if (cbo_COF_Detection.SelectedItem.Text == "C")
        {
            lbl_COF_Detection.Text = "Visual detection,cameras,or detectors with marginal coverage.";
        }

    }

    protected void OnSelectedIndexChanged_Isolation(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {

        if (cbo_COF_Isolation.SelectedItem.Text == "A")
        {
            lbl_COF_Isolation.Text = "Isolation shutdown systems activated directly from process instrumentation or detectors, with no operator intervention.";
        }
        else if (cbo_COF_Isolation.SelectedItem.Text == "B")
        {
            lbl_COF_Isolation.Text = "Isolation shutdown systems activated by operators in the control room or other suitable locations remote from the leak.";
        }
        else if (cbo_COF_Isolation.SelectedItem.Text == "C")
        {
            lbl_COF_Isolation.Text = "Isolation dependent on manually-operated valves.";
        }

    }

    protected void btn_COF_Calc_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_FluidStored.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_RepFluid.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_FinalPhase.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_OpPressure.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_OpTemp.Text.ToString().Trim()))
        {
            lblStatus.Text = "Err : Please check all the  fields";
            lblStatus.ForeColor = Color.Red;
        }
        else
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            try
            {
                lblStatus.Text = string.Empty;
                string sql1 = "select MW,LqdDensity,NBF,Ambient,IdealGas,A,B,C,D,E,IgnTemp from Ref_COF_Lvl_1 where Fluid='" + cbo_RepFluid.SelectedItem.Text.ToString().Trim() + "'";
                SqlCommand cmd = new SqlCommand(sql1, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                rd.Read();
                string MW = rd["MW"].ToString().Trim();
                string LqdDensity = rd["LqdDensity"].ToString().Trim();
                string NBF = rd["NBF"].ToString().Trim();
                string Ambient = rd["Ambient"].ToString().Trim();
                string IdealGas = rd["IdealGas"].ToString().Trim();
                string A = rd["A"].ToString().Trim();
                string B = rd["B"].ToString().Trim();
                string C = rd["C"].ToString().Trim();
                string D = rd["D"].ToString().Trim();
                string E = rd["E"].ToString().Trim();
                string IgnTemp = rd["IgnTemp"].ToString().Trim();
                double T = Convert.ToDouble(txt_OpTemp.Text.ToString().Trim()) + 273;
                double A1 = 0.049;
                double A2 = 0.785;
                double A3 = 12.56;
                double A4 = 200.96;

                double w1 = 0.0000000000;
                double w2 = 0.0000000000;
                double w3 = 0.0000000000;
                double w4 = 0.0000000000;

                double ptrans = 0.0000000000;
                BusinessTier.DisposeReader(rd);
                if (cbo_FluidStored.SelectedValue.ToString() == "Liquid")
                {
                    double NBPV = Convert.ToDouble(NBF.ToString());
                    double LqdDensityV = Convert.ToDouble(LqdDensity.ToString());
                    double IgnTempV = Convert.ToDouble(IgnTemp.ToString());
                    //--------------------------Release Rate Calculation---------------------------------
                    double Kvn = 1.0;
                    double ps = Convert.ToDouble(txt_OpPressure.Text.ToString());
                    double patm = 14.7;
                    double p = Convert.ToDouble(txt_COF_P1.Text.ToString().Trim());
                    double gc = 32.2;
                    double c1 = 12.0;
                    double cd = 0.61;
                    w1 = cd * Kvn * p * (A1 / c1) * Math.Sqrt(2 * gc * (ps - patm) / p);
                    w2 = cd * Kvn * p * (A2 / c1) * Math.Sqrt(2 * gc * (ps - patm) / p);
                    w3 = cd * Kvn * p * (A3 / c1) * Math.Sqrt(2 * gc * (ps - patm) / p);
                    w4 = cd * Kvn * p * (A4 / c1) * Math.Sqrt(2 * gc * (ps - patm) / p);
                }
                else if (cbo_FluidStored.SelectedValue.ToString() == "Gas")
                {
                    double cp = 0.00000000000;
                    if (IdealGas.ToString() == "Note 1")
                    {
                        cp = Convert.ToDouble(A.ToString()) + (Convert.ToDouble(B.ToString()) * Convert.ToDouble(T.ToString())) + (Convert.ToDouble(C.ToString()) * Math.Pow(Convert.ToDouble(T.ToString()), 2)) + (Convert.ToDouble(D.ToString()) * Math.Pow(Convert.ToDouble(T.ToString()), 3));
                    }
                    else if (IdealGas.ToString() == "Note 2")
                    {
                        double ct = (Convert.ToDouble(C.ToString()) / Convert.ToDouble(T.ToString())) / Math.Sinh(Convert.ToDouble(C.ToString()) / Convert.ToDouble(T.ToString()));
                        double et = (Convert.ToDouble(E.ToString()) / Convert.ToDouble(T.ToString())) / Math.Cosh(Convert.ToDouble(E.ToString()) / Convert.ToDouble(T.ToString()));
                        cp = Convert.ToDouble(A.ToString()) + (Convert.ToDouble(B.ToString()) * Math.Pow(ct, 2)) + (Convert.ToDouble(D.ToString()) * Math.Pow(et, 2));
                    }
                    else if (IdealGas.ToString() == "Note 3")
                    {
                        cp = Convert.ToDouble(A.ToString()) + (Convert.ToDouble(B.ToString()) * Convert.ToDouble(T.ToString())) + (Convert.ToDouble(C.ToString()) * Math.Pow(Convert.ToDouble(T.ToString()), 2)) + (Convert.ToDouble(D.ToString()) * Math.Pow(Convert.ToDouble(T.ToString()), 3)) + (Convert.ToDouble(E.ToString()) * Math.Pow(Convert.ToDouble(T.ToString()), 4));
                    }
                    double R = 8.314;
                    double K = cp / (cp - R);
                    //--------------------------Release Rate Calculation---------------------------------
                    double ps = Convert.ToDouble(txt_OpPressure.Text.ToString());
                    double patm = 14.7;
                    double gc = 32.2;
                    double c2 = 1.0;
                    double cd = 0.90;
                    double mw = Convert.ToDouble(MW.ToString());
                    double Ts = (Convert.ToDouble(txt_OpTemp.Text.ToString()) * 1.8) + 491.67;
                    double R1 = 1545;
                    ptrans = patm * Math.Pow(((K + 1) / 2), K / (K - 1));
                    if (ps > ptrans)//-----------------------------------Sonic---------------------------
                    {
                        double kmw = (K * mw * gc) / (R1 * Ts);
                        double power = (K + 1) / (K - 1);
                        double kplus = Math.Pow((2 / (K + 1)), power);
                        w1 = (cd / c2) * A1 * ps * Math.Sqrt(kmw * kplus);
                        w2 = (cd / c2) * A2 * ps * Math.Sqrt(kmw * kplus);
                        w3 = (cd / c2) * A3 * ps * Math.Sqrt(kmw * kplus);
                        w4 = (cd / c2) * A4 * ps * Math.Sqrt(kmw * kplus);
                    }
                    else //--------------------------------SubSonic-----------------------if (ps < ptrans)
                    {
                        double gmw = (mw * gc) / (R1 * Ts);
                        double k2 = (2 * K) / (K - 1);
                        double patmps = Math.Pow(patm / ps, 2 / K);
                        double patmps1 = 1 - (Math.Pow(patm / ps, (K - 1) / K));
                        w1 = (cd / c2) * A1 * ps * Math.Sqrt(gmw * k2 * patmps * patmps1);
                        w2 = (cd / c2) * A2 * ps * Math.Sqrt(gmw * k2 * patmps * patmps1);
                        w3 = (cd / c2) * A3 * ps * Math.Sqrt(gmw * k2 * patmps * patmps1);
                        w4 = (cd / c2) * A4 * ps * Math.Sqrt(gmw * k2 * patmps * patmps1);
                    }

                }
                double c3 = 0.00000000000;
                if (Convert.ToDouble(txt_COF_Masslbs.Text.ToString().Trim()) >= 10000.00)
                {
                    c3 = 10000;
                }
                else
                {
                    c3 = Convert.ToDouble(txt_COF_Masslbs.Text.ToString());
                }
                double t1 = c3 / w1;
                double t2 = c3 / w2;
                double t3 = c3 / w3;
                double t4 = c3 / w4;
                string Timet1 = string.Empty;
                string Timet2 = string.Empty;
                string Timet3 = string.Empty;
                string Timet4 = string.Empty;
                //-----------------------------T1
                //if (t1 <= 180)
                //{
                //    Timet1 = "Instantaneous";
                //}
                //else
                //{
                    Timet1 = "Continuous";
                //}
                //-----------------------------T2
                if (t2 <= 180)
                {
                    Timet2 = "Instantaneous";
                }
                else
                {
                    Timet2 = "Continuous";
                }
                //-----------------------------T3
                if (t3 <= 180)
                {
                    Timet3 = "Instantaneous";
                }
                else
                {
                    Timet3 = "Continuous";
                }
                //-----------------------------T4
                if (t4 <= 180)
                {
                    Timet4 = "Instantaneous";
                }
                else
                {
                    Timet4 = "Continuous";
                }


                //---------------------------------Detection & Isolation-------------------------
                string sql2 = "select Reduction from Ref_COF_Adj_Det_Iso where Detection='" + cbo_COF_Detection.SelectedItem.Text.ToString().Trim() + "' and  Isolat='" + cbo_COF_Isolation.SelectedItem.Text.ToString().Trim() + "'";
                SqlCommand cmd1 = new SqlCommand(sql2, conn);
                SqlDataReader rd1 = cmd1.ExecuteReader();
                rd1.Read();
                double factdi = Convert.ToDouble(rd1["Reduction"].ToString().Trim());
                BusinessTier.DisposeReader(rd1);

                string sql3 = "select Inch25,Inch1,Inch4 from Ref_COF_Leak_Det_Iso where Detection='" + cbo_COF_Detection.SelectedItem.Text.ToString().Trim() + "' and  Isolat='" + cbo_COF_Isolation.SelectedItem.Text.ToString().Trim() + "'";
                SqlCommand cmd3 = new SqlCommand(sql3, conn);
                SqlDataReader rd3 = cmd3.ExecuteReader();
                rd3.Read();
                double ld1 = Convert.ToDouble(rd3["Inch25"].ToString().Trim());
                double ld2 = Convert.ToDouble(rd3["Inch1"].ToString().Trim());
                double ld3 = Convert.ToDouble(rd3["Inch4"].ToString().Trim());
                BusinessTier.DisposeReader(rd3);

                double rate1 = w1 * (1 - factdi);
                double rate2 = w2 * (1 - factdi);
                double rate3 = w3 * (1 - factdi);
                double rate4 = w4 * (1 - factdi);

                double usld1 = Math.Min(Convert.ToDouble(txt_COF_Masslbs.Text.ToString().Trim()) / rate1 / 60, ld1);
                double usld2 = Math.Min(Convert.ToDouble(txt_COF_Masslbs.Text.ToString().Trim()) / rate2 / 60, ld2);
                double usld3 = Math.Min(Convert.ToDouble(txt_COF_Masslbs.Text.ToString().Trim()) / rate3 / 60, ld3);
                double usld4 = Convert.ToDouble(txt_COF_Masslbs.Text.ToString().Trim()) / rate4 / 60;

                string useld1 = string.Empty;
                string useld2 = string.Empty;
                string useld3 = string.Empty;
                string useld4 = string.Empty;

                double mass1 = 0.00000000000;
                double mass2 = 0.00000000000;
                double mass3 = 0.00000000000;
                double mass4 = 0.00000000000;
                //-----------------------------useld1 & Mass1
                if (Timet1 == "Continuous")
                {
                    useld1 = usld1.ToString("#.##");
                    mass1 = rate1 * usld1 * 60;
                }
                else
                {
                    useld1 = "Instantaneous";
                    mass1 = Convert.ToDouble(txt_COF_Masslbs.Text.ToString().Trim()) * 1;
                }
                //-----------------------------useld2 & Mass2
                if (Timet2 == "Continuous")
                {
                    useld2 = usld2.ToString("#.##");
                    mass2 = rate2 * usld2 * 60;
                }
                else
                {
                    useld2 = "Instantaneous";
                    mass2 = Convert.ToDouble(txt_COF_Masslbs.Text.ToString().Trim()) * 1;

                }
                //-----------------------------useld3 & Mass3
                if (Timet3 == "Continuous")
                {
                    useld3 = usld3.ToString("#.##");
                    mass3 = rate3 * usld3 * 60;
                }
                else
                {
                    useld3 = "Instantaneous";
                    mass3 = Convert.ToDouble(txt_COF_Masslbs.Text.ToString().Trim()) * 1;
                }
                //-----------------------------useld4 & Mass3
                if (Timet4 == "Continuous")
                {
                    useld4 = usld4.ToString("#.##");
                    mass4 = rate4 * usld4 * 60;
                }
                else
                {
                    useld4 = "Instantaneous";
                    mass4 = Convert.ToDouble(txt_COF_Masslbs.Text.ToString().Trim()) * 1;
                }
                //-----------------------------Mitigation Value
                double factmit = Convert.ToDouble(cbo_COF_Mitigation.SelectedValue.ToString().Trim());
                //-----------------------------efficiency correction
                double eneffn = 1.0000000000; // ---------------------- for Display
                double eneffn1 = 0.0000000000;
                double eneffn2 = 0.0000000000;
                double eneffn3 = 0.0000000000;
                double eneffn4 = 0.0000000000;
                double c4 = 1.0;
                if (Convert.ToDouble(txt_COF_Masslbs.Text.ToString().Trim()) > 10000 && Timet1 == "Instantaneous")
                {
                    eneffn1 = 4 * Math.Log10(c4 * Convert.ToDouble(txt_COF_Masslbs.Text.ToString().Trim())) - 15;
                    eneffn = eneffn1;
                }
                else
                {
                    eneffn1 = 1.0;
                }


                if (Convert.ToDouble(txt_COF_Masslbs.Text.ToString().Trim()) > 10000  && Timet2 == "Instantaneous")
                {
                    eneffn2 = 4 * Math.Log10(c4 * Convert.ToDouble(txt_COF_Masslbs.Text.ToString().Trim())) - 15;
                    eneffn = eneffn2;

                }
                else
                {
                    eneffn2 = 1.0;
                }



                if (Convert.ToDouble(txt_COF_Masslbs.Text.ToString().Trim()) > 10000 && Timet3 == "Instantaneous")
                {
                    eneffn3 = 4 * Math.Log10(c4 * Convert.ToDouble(txt_COF_Masslbs.Text.ToString().Trim())) - 15; 
                    eneffn = eneffn3;

                }
                else
                {
                    eneffn3 = 1.0;
                }


                if (Convert.ToDouble(txt_COF_Masslbs.Text.ToString().Trim()) > 10000 && Timet4 == "Instantaneous")
                {
                    eneffn4 = 4 * Math.Log10(c4 * Convert.ToDouble(txt_COF_Masslbs.Text.ToString().Trim())) - 15;
                    eneffn = eneffn4;
                }
                else
                {
                    eneffn4 = 1.0;
                }

               

                string ConInstA = "", ConInstB = "";
                double C7 = 10.763;
                double C4 = 1.0;
                double C8 = 1.0;

                double CAc1 = 0.0000000000;
                double CAc2 = 0.0000000000;
                double CAc3 = 0.0000000000;
                double CAc4 = 0.0000000000;

                double CAInst1 = 0.0000000000;
                double CAInst2 = 0.0000000000;
                double CAInst3 = 0.0000000000;
                double CAInst4 = 0.0000000000;

                double effrate1 = 0.0000000000;
                double effrate2 = 0.0000000000;
                double effrate3 = 0.0000000000;
                double effrate4 = 0.0000000000;

                double effmass1 = 0.0000000000;
                double effmass2 = 0.0000000000;
                double effmass3 = 0.0000000000;
                double effmass4 = 0.0000000000;

                double CONTa = 0.0000000000;
                double CONTb = 0.0000000000;
                double INSTa = 0.0000000000;
                double INSTb = 0.0000000000;
                //--------------Compute Component damage consequence area for Auto ignation not likely

                //---------------For Coninuous Release CAc1
                if (Timet1 == "Continuous")
                {
                    if (cbo_FluidStored.SelectedValue.ToString() == "Gas")
                    {
                        ConInstA = "ConGasA";
                        ConInstB = "ConGasB";
                    }
                    if (cbo_FluidStored.SelectedValue.ToString() == "Liquid")
                    {
                        ConInstA = "ConLiquidA";
                        ConInstB = "ConLiquidB";
                    }
                    string sql4 = "select " + ConInstA.ToString().Trim() + " as a, " + ConInstB.ToString().Trim() + " as b from Ref_COF_Damage where Fluid='" + cbo_RepFluid.SelectedItem.Text.ToString().Trim() + "'";
                    SqlCommand cmd4 = new SqlCommand(sql4, conn);
                    SqlDataReader rd4 = cmd4.ExecuteReader();
                    rd4.Read();
                    CONTa = Convert.ToDouble(rd4["a"].ToString().Trim());
                    CONTb = Convert.ToDouble(rd4["b"].ToString().Trim());
                    BusinessTier.DisposeReader(rd4);
                    if (txt_FluidType.Text.ToString().Trim() == "TYPE 0" && cbo_FluidStored.SelectedValue.ToString() == "Liquid")
                    {
                        CAc1 = Math.Min(CONTa * Math.Pow(rate1, CONTb), C7) * (1 - factmit);
                        effrate1 = (1 / C4) * Math.Exp(Math.Log10(CAc1 / (C8 * CONTa)) * Math.Pow(CONTb, -1));
                    }
                    else
                    {
                        CAc1 = CONTa * Math.Pow(rate1, CONTb) * (1 - factmit);
                        effrate1 = rate1;
                    }
                }
                //---------------For Instantaneous Release CAInst1
                else if (Timet1 == "Instantaneous")
                {
                    if (cbo_FluidStored.SelectedValue.ToString() == "Gas")
                    {
                        ConInstA = "InstGasA";
                        ConInstB = "InstGasB";
                    }
                    if (cbo_FluidStored.SelectedValue.ToString() == "Liquid")
                    {
                        ConInstA = "InstLiquidA";
                        ConInstB = "InstLiquidB";
                    }
                    string sql4 = "select " + ConInstA.ToString().Trim() + " as a, " + ConInstB.ToString().Trim() + " as b from Ref_COF_Damage where Fluid='" + cbo_RepFluid.SelectedItem.Text.ToString().Trim() + "'";
                    SqlCommand cmd4 = new SqlCommand(sql4, conn);
                    SqlDataReader rd4 = cmd4.ExecuteReader();
                    rd4.Read();
                    INSTa = Convert.ToDouble(rd4["a"].ToString().Trim());
                    INSTb = Convert.ToDouble(rd4["b"].ToString().Trim());
                    BusinessTier.DisposeReader(rd4);
                    if (txt_FluidType.Text.ToString().Trim() == "TYPE 0" && cbo_FluidStored.SelectedValue.ToString() == "Liquid")
                    {
                        CAInst1 = Math.Min(INSTa * Math.Pow(mass1, INSTb), C7) * (1 - factmit) / eneffn1;
                        effmass1 = (1 / C4) * Math.Exp(Math.Log10(CAInst1 / (C8 * INSTa)) * Math.Pow(INSTb, -1));
                    }
                    else
                    {
                        CAInst1 = INSTa * Math.Pow(mass1, INSTb) * (1 - factmit) / eneffn1;
                        effmass1 = mass1;
                    }
                }

                //---------------For Coninuous Release CAc2
                if (Timet2 == "Continuous")
                {
                    if (cbo_FluidStored.SelectedValue.ToString() == "Gas")
                    {
                        ConInstA = "ConGasA";
                        ConInstB = "ConGasB";
                    }
                    if (cbo_FluidStored.SelectedValue.ToString() == "Liquid")
                    {
                        ConInstA = "ConLiquidA";
                        ConInstB = "ConLiquidB";
                    }
                    string sql4 = "select " + ConInstA.ToString().Trim() + " as a, " + ConInstB.ToString().Trim() + " as b from Ref_COF_Damage where Fluid='" + cbo_RepFluid.SelectedItem.Text.ToString().Trim() + "'";
                    SqlCommand cmd4 = new SqlCommand(sql4, conn);
                    SqlDataReader rd4 = cmd4.ExecuteReader();
                    rd4.Read();
                    CONTa = Convert.ToDouble(rd4["a"].ToString().Trim());
                    CONTb = Convert.ToDouble(rd4["b"].ToString().Trim());
                    BusinessTier.DisposeReader(rd4);
                    if (txt_FluidType.Text.ToString().Trim() == "TYPE 0" && cbo_FluidStored.SelectedValue.ToString() == "Liquid")
                    {
                        CAc2 = Math.Min(CONTa * Math.Pow(rate2, CONTb), C7) * (1 - factmit);
                        effrate2 = (1 / C4) * Math.Exp(Math.Log10(CAc2 / (C8 * CONTa)) * Math.Pow(CONTb, -1));
                    }
                    else
                    {
                        CAc2 = CONTa * Math.Pow(rate2, CONTb) * (1 - factmit);
                        effrate2 = rate2;
                    }
                }
                //---------------For Instantaneous Release CAInst2
                else if (Timet2 == "Instantaneous")
                {
                    if (cbo_FluidStored.SelectedValue.ToString() == "Gas")
                    {
                        ConInstA = "InstGasA";
                        ConInstB = "InstGasB";
                    }
                    if (cbo_FluidStored.SelectedValue.ToString() == "Liquid")
                    {
                        ConInstA = "InstLiquidA";
                        ConInstB = "InstLiquidB";
                    }
                    string sql4 = "select " + ConInstA.ToString().Trim() + " as a, " + ConInstB.ToString().Trim() + " as b from Ref_COF_Damage where Fluid='" + cbo_RepFluid.SelectedItem.Text.ToString().Trim() + "'";
                    SqlCommand cmd4 = new SqlCommand(sql4, conn);
                    SqlDataReader rd4 = cmd4.ExecuteReader();
                    rd4.Read();
                    INSTa = Convert.ToDouble(rd4["a"].ToString().Trim());
                    INSTb = Convert.ToDouble(rd4["b"].ToString().Trim());
                    BusinessTier.DisposeReader(rd4);
                    if (txt_FluidType.Text.ToString().Trim() == "TYPE 0" && cbo_FluidStored.SelectedValue.ToString() == "Liquid")
                    {
                        CAInst2 = Math.Min(INSTa * Math.Pow(mass2, INSTb), C7) * (1 - factmit) / eneffn2;
                        effmass2 = (1 / C4) * Math.Exp(Math.Log10(CAInst2 / (C8 * INSTa)) * Math.Pow(INSTb, -1));
                    }
                    else
                    {
                        CAInst2 = INSTa * Math.Pow(mass2, INSTb) * (1 - factmit) / eneffn2;
                        effmass2 = mass2;
                    }
                }

                //---------------For Coninuous Release CAc3
                if (Timet3 == "Continuous")
                {
                    if (cbo_FluidStored.SelectedValue.ToString() == "Gas")
                    {
                        ConInstA = "ConGasA";
                        ConInstB = "ConGasB";
                    }
                    if (cbo_FluidStored.SelectedValue.ToString() == "Liquid")
                    {
                        ConInstA = "ConLiquidA";
                        ConInstB = "ConLiquidB";
                    }
                    string sql4 = "select " + ConInstA.ToString().Trim() + " as a, " + ConInstB.ToString().Trim() + " as b from Ref_COF_Damage where Fluid='" + cbo_RepFluid.SelectedItem.Text.ToString().Trim() + "'";
                    SqlCommand cmd4 = new SqlCommand(sql4, conn);
                    SqlDataReader rd4 = cmd4.ExecuteReader();
                    rd4.Read();
                    CONTa = Convert.ToDouble(rd4["a"].ToString().Trim());
                    CONTb = Convert.ToDouble(rd4["b"].ToString().Trim());
                    BusinessTier.DisposeReader(rd4);
                    if (txt_FluidType.Text.ToString().Trim() == "TYPE 0" && cbo_FluidStored.SelectedValue.ToString() == "Liquid")
                    {
                        CAc3 = Math.Min(CONTa * Math.Pow(rate3, CONTb), C7) * (1 - factmit);
                        effrate3 = (1 / C4) * Math.Exp(Math.Log10(CAc3 / (C8 * CONTa)) * Math.Pow(CONTb, -1));
                    }
                    else
                    {
                        CAc3 = CONTa * Math.Pow(rate3, CONTb) * (1 - factmit);
                        effrate3 = rate3;
                    }
                }
                //---------------For Instantaneous Release CAInst3
                else if (Timet3 == "Instantaneous")
                {
                    if (cbo_FluidStored.SelectedValue.ToString() == "Gas")
                    {
                        ConInstA = "InstGasA";
                        ConInstB = "InstGasB";
                    }
                    if (cbo_FluidStored.SelectedValue.ToString() == "Liquid")
                    {
                        ConInstA = "InstLiquidA";
                        ConInstB = "InstLiquidB";
                    }
                    string sql4 = "select " + ConInstA.ToString().Trim() + " as a, " + ConInstB.ToString().Trim() + " as b from Ref_COF_Damage where Fluid='" + cbo_RepFluid.SelectedItem.Text.ToString().Trim() + "'";
                    SqlCommand cmd4 = new SqlCommand(sql4, conn);
                    SqlDataReader rd4 = cmd4.ExecuteReader();
                    rd4.Read();
                    INSTa = Convert.ToDouble(rd4["a"].ToString().Trim());
                    INSTb = Convert.ToDouble(rd4["b"].ToString().Trim());
                    BusinessTier.DisposeReader(rd4);
                    if (txt_FluidType.Text.ToString().Trim() == "TYPE 0" && cbo_FluidStored.SelectedValue.ToString() == "Liquid")
                    {
                        CAInst3 = Math.Min(INSTa * Math.Pow(mass3, INSTb), C7) * (1 - factmit) / eneffn3;
                        effmass3 = (1 / C4) * Math.Exp(Math.Log10(CAInst3 / (C8 * INSTa)) * Math.Pow(INSTb, -1));
                    }
                    else
                    {
                        CAInst3 = INSTa * Math.Pow(mass3, INSTb) * (1 - factmit) / eneffn3;
                        effmass3 = mass3;
                    }
                }

                //---------------For Coninuous Release CAc4
                if (Timet4 == "Continuous")
                {
                    if (cbo_FluidStored.SelectedValue.ToString() == "Gas")
                    {
                        ConInstA = "ConGasA";
                        ConInstB = "ConGasB";
                    }
                    if (cbo_FluidStored.SelectedValue.ToString() == "Liquid")
                    {
                        ConInstA = "ConLiquidA";
                        ConInstB = "ConLiquidB";
                    }
                    string sql4 = "select " + ConInstA.ToString().Trim() + " as a, " + ConInstB.ToString().Trim() + " as b from Ref_COF_Damage where Fluid='" + cbo_RepFluid.SelectedItem.Text.ToString().Trim() + "'";
                    SqlCommand cmd4 = new SqlCommand(sql4, conn);
                    SqlDataReader rd4 = cmd4.ExecuteReader();
                    rd4.Read();
                    CONTa = Convert.ToDouble(rd4["a"].ToString().Trim());
                    CONTb = Convert.ToDouble(rd4["b"].ToString().Trim());
                    BusinessTier.DisposeReader(rd4);
                    if (txt_FluidType.Text.ToString().Trim() == "TYPE 0" && cbo_FluidStored.SelectedValue.ToString() == "Liquid")
                    {
                        CAc4 = Math.Min(CONTa * Math.Pow(rate4, CONTb), C7) * (1 - factmit);
                        effrate4 = (1 / C4) * Math.Exp(Math.Log10(CAc4 / (C8 * CONTa)) * Math.Pow(CONTb, -1));
                    }
                    else
                    {
                        CAc4 = CONTa * Math.Pow(rate4, CONTb) * (1 - factmit);
                        effrate4 = rate4;
                    }
                }
                //---------------For Instantaneous Release CAInst3
                else if (Timet4 == "Instantaneous")
                {
                    if (cbo_FluidStored.SelectedValue.ToString() == "Gas")
                    {
                        ConInstA = "InstGasA";
                        ConInstB = "InstGasB";
                    }
                    if (cbo_FluidStored.SelectedValue.ToString() == "Liquid")
                    {
                        ConInstA = "InstLiquidA";
                        ConInstB = "InstLiquidB";
                    }
                    string sql4 = "select " + ConInstA.ToString().Trim() + " as a, " + ConInstB.ToString().Trim() + " as b from Ref_COF_Damage where Fluid='" + cbo_RepFluid.SelectedItem.Text.ToString().Trim() + "'";
                    SqlCommand cmd4 = new SqlCommand(sql4, conn);
                    SqlDataReader rd4 = cmd4.ExecuteReader();
                    rd4.Read();
                    INSTa = Convert.ToDouble(rd4["a"].ToString().Trim());
                    INSTb = Convert.ToDouble(rd4["b"].ToString().Trim());
                    BusinessTier.DisposeReader(rd4);
                    if (txt_FluidType.Text.ToString().Trim() == "TYPE 0" && cbo_FluidStored.SelectedValue.ToString() == "Liquid")
                    {
                        CAInst4 = Math.Min(INSTa * Math.Pow(mass4, INSTb), C7) * (1 - factmit) / eneffn4;
                        effmass4 = (1 / C4) * Math.Exp(Math.Log10(CAInst4 / (C8 * INSTa)) * Math.Pow(INSTb, -1));
                    }
                    else
                    {
                        CAInst4 = INSTa * Math.Pow(mass4, INSTb) * (1 - factmit) / eneffn4;
                        effmass4 = mass4;
                    }
                }
                double CAinjCONT1 = 0.0000000000;
                double CAinjCONT2 = 0.0000000000;
                double CAinjCONT3 = 0.0000000000;
                double CAinjCONT4 = 0.0000000000;

                double CAinjINST1 = 0.0000000000;
                double CAinjINST2 = 0.0000000000;
                double CAinjINST3 = 0.0000000000;
                double CAinjINST4 = 0.0000000000;

                double InjCONTa = 0.0000000000;
                double InjCONTb = 0.0000000000;
                double InjINSTa = 0.0000000000;
                double InjINSTb = 0.0000000000;

                //--------------Compute personnel injury consequence area for Auto ignation not likely

                //---------------For Coninuous Release CAinjCONT1

                if (Timet1 == "Continuous")
                {
                    if (cbo_FluidStored.SelectedValue.ToString() == "Gas")
                    {
                        ConInstA = "ConGasA";
                        ConInstB = "ConGasB";
                    }
                    if (cbo_FluidStored.SelectedValue.ToString() == "Liquid")
                    {
                        ConInstA = "ConLiquidA";
                        ConInstB = "ConLiquidB";
                    }
                    string sql4 = "select " + ConInstA.ToString().Trim() + " as a, " + ConInstB.ToString().Trim() + " as b from Ref_COF_Injury where Fluid='" + cbo_RepFluid.SelectedItem.Text.ToString().Trim() + "'";
                    SqlCommand cmd4 = new SqlCommand(sql4, conn);
                    SqlDataReader rd4 = cmd4.ExecuteReader();
                    rd4.Read();
                    InjCONTa = Convert.ToDouble(rd4["a"].ToString().Trim());
                    InjCONTb = Convert.ToDouble(rd4["b"].ToString().Trim());
                    BusinessTier.DisposeReader(rd4);
                    CAinjCONT1 = InjCONTa * Math.Pow(effrate1, InjCONTb) * (1 - factmit);

                }
                //---------------For Instantaneous Release CAinjINST1
                else if (Timet1 == "Instantaneous")
                {
                    if (cbo_FluidStored.SelectedValue.ToString() == "Gas")
                    {
                        ConInstA = "InstGasA";
                        ConInstB = "InstGasB";
                    }
                    if (cbo_FluidStored.SelectedValue.ToString() == "Liquid")
                    {
                        ConInstA = "InstLiquidA";
                        ConInstB = "InstLiquidB";
                    }
                    string sql4 = "select " + ConInstA.ToString().Trim() + " as a, " + ConInstB.ToString().Trim() + " as b from Ref_COF_Injury where Fluid='" + cbo_RepFluid.SelectedItem.Text.ToString().Trim() + "'";
                    SqlCommand cmd4 = new SqlCommand(sql4, conn);
                    SqlDataReader rd4 = cmd4.ExecuteReader();
                    rd4.Read();
                    InjINSTa = Convert.ToDouble(rd4["a"].ToString().Trim());
                    InjINSTb = Convert.ToDouble(rd4["b"].ToString().Trim());
                    BusinessTier.DisposeReader(rd4);

                    if (cbo_FluidStored.SelectedValue.ToString() == "Liquid")
                    {
                        CAinjINST1 = InjINSTa * Math.Pow(effmass1, InjINSTb) * (1 - factmit) / eneffn1;
                    }
                    else
                    {
                        //CAinjINST1 = InjINSTa * Math.Pow(CAInst1, InjINSTb) * (1 - factmit) / eneffn;
                        CAinjINST1 = InjINSTa * Math.Pow(effmass1, InjINSTb) * (1 - factmit) / eneffn1;
                    }
                }

                //---------------For Coninuous Release CAinjCONT2

                if (Timet2 == "Continuous")
                {
                    if (cbo_FluidStored.SelectedValue.ToString() == "Gas")
                    {
                        ConInstA = "ConGasA";
                        ConInstB = "ConGasB";
                    }
                    if (cbo_FluidStored.SelectedValue.ToString() == "Liquid")
                    {
                        ConInstA = "ConLiquidA";
                        ConInstB = "ConLiquidB";
                    }
                    string sql4 = "select " + ConInstA.ToString().Trim() + " as a, " + ConInstB.ToString().Trim() + " as b from Ref_COF_Injury where Fluid='" + cbo_RepFluid.SelectedItem.Text.ToString().Trim() + "'";
                    SqlCommand cmd4 = new SqlCommand(sql4, conn);
                    SqlDataReader rd4 = cmd4.ExecuteReader();
                    rd4.Read();
                    InjCONTa = Convert.ToDouble(rd4["a"].ToString().Trim());
                    InjCONTb = Convert.ToDouble(rd4["b"].ToString().Trim());
                    BusinessTier.DisposeReader(rd4);
                    CAinjCONT2 = InjCONTa * Math.Pow(effrate2, InjCONTb) * (1 - factmit);
                }
                //---------------For Instantaneous Release CAinjINST2
                else if (Timet2 == "Instantaneous")
                {
                    if (cbo_FluidStored.SelectedValue.ToString() == "Gas")
                    {
                        ConInstA = "InstGasA";
                        ConInstB = "InstGasB";
                    }
                    if (cbo_FluidStored.SelectedValue.ToString() == "Liquid")
                    {
                        ConInstA = "InstLiquidA";
                        ConInstB = "InstLiquidB";
                    }
                    string sql4 = "select " + ConInstA.ToString().Trim() + " as a, " + ConInstB.ToString().Trim() + " as b from Ref_COF_Injury where Fluid='" + cbo_RepFluid.SelectedItem.Text.ToString().Trim() + "'";
                    SqlCommand cmd4 = new SqlCommand(sql4, conn);
                    SqlDataReader rd4 = cmd4.ExecuteReader();
                    rd4.Read();
                    InjINSTa = Convert.ToDouble(rd4["a"].ToString().Trim());
                    InjINSTb = Convert.ToDouble(rd4["b"].ToString().Trim());
                    BusinessTier.DisposeReader(rd4);
                    if (cbo_FluidStored.SelectedValue.ToString() == "Liquid")
                    {
                        CAinjINST2 = InjINSTa * Math.Pow(effmass2, InjINSTb) * (1 - factmit) / eneffn2;
                    }
                    else
                    {
                       // CAinjINST2 = InjINSTa * Math.Pow(CAInst2, InjINSTb) * (1 - factmit) / eneffn;
                        CAinjINST2 = InjINSTa * Math.Pow(effmass2, InjINSTb) * (1 - factmit) / eneffn2;
                    }

                }

                //---------------For Coninuous Release CAinjCONT3
                if (Timet3 == "Continuous")
                {
                    if (cbo_FluidStored.SelectedValue.ToString() == "Gas")
                    {
                        ConInstA = "ConGasA";
                        ConInstB = "ConGasB";
                    }
                    if (cbo_FluidStored.SelectedValue.ToString() == "Liquid")
                    {
                        ConInstA = "ConLiquidA";
                        ConInstB = "ConLiquidB";
                    }
                    string sql4 = "select " + ConInstA.ToString().Trim() + " as a, " + ConInstB.ToString().Trim() + " as b from Ref_COF_Injury where Fluid='" + cbo_RepFluid.SelectedItem.Text.ToString().Trim() + "'";
                    SqlCommand cmd4 = new SqlCommand(sql4, conn);
                    SqlDataReader rd4 = cmd4.ExecuteReader();
                    rd4.Read();
                    InjCONTa = Convert.ToDouble(rd4["a"].ToString().Trim());
                    InjCONTb = Convert.ToDouble(rd4["b"].ToString().Trim());
                    BusinessTier.DisposeReader(rd4);
                    CAinjCONT3 = InjCONTa * Math.Pow(effrate3, InjCONTb) * (1 - factmit);
                }
                //---------------For Instantaneous Release CAinjINST3
                else if (Timet3 == "Instantaneous")
                {
                    if (cbo_FluidStored.SelectedValue.ToString() == "Gas")
                    {
                        ConInstA = "InstGasA";
                        ConInstB = "InstGasB";
                    }
                    if (cbo_FluidStored.SelectedValue.ToString() == "Liquid")
                    {
                        ConInstA = "InstLiquidA";
                        ConInstB = "InstLiquidB";
                    }
                    string sql4 = "select " + ConInstA.ToString().Trim() + " as a, " + ConInstB.ToString().Trim() + " as b from Ref_COF_Injury where Fluid='" + cbo_RepFluid.SelectedItem.Text.ToString().Trim() + "'";
                    SqlCommand cmd4 = new SqlCommand(sql4, conn);
                    SqlDataReader rd4 = cmd4.ExecuteReader();
                    rd4.Read();
                    InjINSTa = Convert.ToDouble(rd4["a"].ToString().Trim());
                    InjINSTb = Convert.ToDouble(rd4["b"].ToString().Trim());
                    BusinessTier.DisposeReader(rd4);
                    if (cbo_FluidStored.SelectedValue.ToString() == "Liquid")
                    {
                        CAinjINST3 = InjINSTa * Math.Pow(effmass3, InjINSTb) * (1 - factmit) / eneffn3;
                    }
                    else
                    {
                        //CAinjINST3 = InjINSTa * Math.Pow(CAInst3, InjINSTb) * (1 - factmit) / eneffn;
                        CAinjINST3 = InjINSTa * Math.Pow(effmass3, InjINSTb) * (1 - factmit) / eneffn3;
                    }
                }

                //---------------For Coninuous Release CAinjCONT4
                if (Timet4 == "Continuous")
                {
                    if (cbo_FluidStored.SelectedValue.ToString() == "Gas")
                    {
                        ConInstA = "ConGasA";
                        ConInstB = "ConGasB";
                    }
                    if (cbo_FluidStored.SelectedValue.ToString() == "Liquid")
                    {
                        ConInstA = "ConLiquidA";
                        ConInstB = "ConLiquidB";
                    }
                    string sql4 = "select " + ConInstA.ToString().Trim() + " as a, " + ConInstB.ToString().Trim() + " as b from Ref_COF_Injury where Fluid='" + cbo_RepFluid.SelectedItem.Text.ToString().Trim() + "'";
                    SqlCommand cmd4 = new SqlCommand(sql4, conn);
                    SqlDataReader rd4 = cmd4.ExecuteReader();
                    rd4.Read();
                    InjCONTa = Convert.ToDouble(rd4["a"].ToString().Trim());
                    InjCONTb = Convert.ToDouble(rd4["b"].ToString().Trim());
                    BusinessTier.DisposeReader(rd4);
                    CAinjCONT4 = InjCONTa * Math.Pow(effrate4, InjCONTb) * (1 - factmit);
                }
                //---------------For Instantaneous Release CAinjINST4
                else if (Timet4 == "Instantaneous")
                {
                    if (cbo_FluidStored.SelectedValue.ToString() == "Gas")
                    {
                        ConInstA = "InstGasA";
                        ConInstB = "InstGasB";
                    }
                    if (cbo_FluidStored.SelectedValue.ToString() == "Liquid")
                    {
                        ConInstA = "InstLiquidA";
                        ConInstB = "InstLiquidB";
                    }
                    string sql4 = "select " + ConInstA.ToString().Trim() + " as a, " + ConInstB.ToString().Trim() + " as b from Ref_COF_Injury where Fluid='" + cbo_RepFluid.SelectedItem.Text.ToString().Trim() + "'";
                    SqlCommand cmd4 = new SqlCommand(sql4, conn);
                    SqlDataReader rd4 = cmd4.ExecuteReader();
                    rd4.Read();
                    InjINSTa = Convert.ToDouble(rd4["a"].ToString().Trim());
                    InjINSTb = Convert.ToDouble(rd4["b"].ToString().Trim());
                    rd.Close();
                    BusinessTier.DisposeReader(rd4);
                    if (cbo_FluidStored.SelectedValue.ToString() == "Liquid")
                    {
                        CAinjINST4 = InjINSTa * Math.Pow(effmass4, InjINSTb) * (1 - factmit) / eneffn4;
                    }
                    else
                    {
                       // CAinjINST4 = InjINSTa * Math.Pow(CAInst4, InjINSTb) * (1 - factmit) / eneffn;
                        CAinjINST4 = InjINSTa * Math.Pow(effmass4, InjINSTb) * (1 - factmit) / eneffn4;
                    }

                }
                //--------------Step  8.12 factic------------------------------------------------------------------

                double factic1 = 0.0000000000;
                double factic2 = 0.0000000000;
                double factic3 = 0.0000000000;
                double factic4 = 0.0000000000;
                double c5 = 55.6000000000;

                double cacmd1 = 0.0000000000;
                double cacmd2 = 0.0000000000;
                double cacmd3 = 0.0000000000;
                double cacmd4 = 0.0000000000;


                double cainj1 = 0.0000000000;
                double cainj2 = 0.0000000000;
                double cainj3 = 0.0000000000;
                double cainj4 = 0.0000000000;


                double instmax1 = 0.0000000000;
                double instmax2 = 0.0000000000;
                double instmax3 = 0.0000000000;

                if (txt_FluidType.Text.ToString().Trim() == "TYPE 0" && Convert.ToDouble(txt_COF_Masslbs.Text.ToString().Trim()) >= 10000.00)
                {
                    //-------------------------- Continuous/Instantaneous blending factor, fact-ic----------------------
                    if (Timet1 == "Continuous")
                    {
                        factic1 = rate1 / c5;
                    }
                    else
                    {
                        factic1 = 1.0;
                    }
                    if (Timet2 == "Continuous")
                    {
                        factic2 = rate2 / c5;
                    }
                    else
                    {
                        factic2 = 1.0;
                    }
                    if (Timet3 == "Continuous")
                    {
                        factic3 = rate3 / c5;
                    }
                    else
                    {
                        factic3 = 1.0;
                    }
                    if (Timet4 == "Continuous")
                    {
                        factic4 = rate4 / c5;
                    }
                    else
                    {
                        factic4 = 1.0;
                    }

                    instmax1 = Math.Max(CAInst1, CAInst2);
                    instmax2 = Math.Max(CAInst3, CAInst4);
                    instmax3 = Math.Max(instmax1, instmax2);

                    //------------------- Continuous/Instantaneous blended consequence area (component damage)----------------

                    if (Timet1 == "Continuous")
                    {
                        cacmd1 = CAc1 * factic1 + instmax3 * (1 - factic1);
                    }
                    else
                    {
                        if (CAInst1 == instmax3)
                        {
                            cacmd1 = instmax3;
                        }
                        else
                        {
                            cacmd1 = CAc1 * factic1 + CAInst1 * (1 - factic1);
                        }
                    }
                    if (Timet2 == "Continuous")
                    {
                        cacmd2 = CAc2 * factic2 + instmax3 * (1 - factic2);
                    }
                    else
                    {
                        if (CAInst2 == instmax3)
                        {
                            cacmd2 = instmax3;
                        }
                        else
                        {
                            cacmd2 = CAc2 * factic2 + CAInst2 * (1 - factic2);
                        }

                    }
                    if (Timet3 == "Continuous")
                    {
                        cacmd3 = CAc3 * factic3 + instmax3 * (1 - factic3);
                    }
                    else
                    {
                        if (CAInst3 == instmax3)
                        {
                            cacmd3 = instmax3;
                        }
                        else
                        {
                            cacmd3 = CAc3 * factic3 + CAInst3 * (1 - factic3);
                        }

                    }
                    if (Timet4 == "Continuous")
                    {
                        cacmd4 = CAc4 * factic4 + instmax3 * (1 - factic4);
                    }
                    else
                    {
                        if (CAInst4 == instmax3)
                        {
                            cacmd4 = instmax3;
                        }
                        else
                        {
                            cacmd4 = CAc4 * factic4 + CAInst4 * (1 - factic4);
                        }

                    }



                    double injmax1 = 0.0000000000;
                    double injmax2 = 0.0000000000;
                    double injmax3 = 0.0000000000;

                    injmax1 = Math.Max(CAinjINST1, CAinjINST2);
                    injmax2 = Math.Max(CAinjINST3, CAinjINST4);
                    injmax3 = Math.Max(injmax1, injmax2);

                    //if (injmax3 == 0)
                    //{
                    //    injmax3 = instmax3;
                    //}

                    //--------------------- Continuous/Instantaneous blended consequence (personnel injury)-----------------

                    if (Timet1 == "Continuous")
                    {
                        cainj1 = (CAinjCONT1 * factic1) + (injmax3 * (1 - factic1));
                    }
                    else
                    {
                        if (CAinjINST1 == injmax3)
                        {
                            cainj1 = injmax3;
                        }
                        else
                        {
                            cainj1 = (CAinjCONT1 * factic1) + CAinjINST1 * (1 - factic1);
                        }
                    }
                    if (Timet2 == "Continuous")
                    {
                        cainj2 = (CAinjCONT2 * factic2) + (injmax3 * (1 - factic2));
                    }
                    else
                    {
                        if (CAinjINST2 == injmax3)
                        {
                            cainj2 = injmax3;
                        }
                        else
                        {
                            cainj2 = (CAinjCONT2 * factic2) + (CAinjINST2 * (1 - factic2));
                        }
                    }
                    if (Timet3 == "Continuous")
                    {
                        cainj3 = (CAinjCONT3 * factic3) + (injmax3 * (1 - factic3));
                    }
                    else
                    {
                        if (CAinjINST3 == injmax3)
                        {
                            cainj3 = injmax3;
                        }
                        else
                        {
                            cainj3 = (CAinjCONT3 * factic3) + (CAinjINST3 * (1 - factic3));
                        }
                    }
                    if (Timet4 == "Continuous")
                    {
                        cainj4 = (CAinjCONT4 * factic4) + (injmax3 * (1 - factic4));
                    }
                    else
                    {
                        if (CAinjINST4 == injmax3)
                        {
                            cainj4 = injmax3;
                        }
                        else
                        {
                            cainj4 = (CAinjCONT4 * factic4) + (CAinjINST4 * (1 - factic4));
                        }
                    }

                }

                double gff1 = 0.261;
                double gff2 = 0.654;
                double gff3 = 0.065;
                double gff4 = 0.02;

                double CACMDFinal1 = 0.0000000000;
                double CACMDFinal2 = 0.0000000000;
                double CACMDFinal3 = 0.0000000000;
                double CACMDFinal4 = 0.0000000000;
                double CACMDFinaltot = 0.0000000000;

                double CAnfnt1 = 0.0000000000;
                double CAnfnt2 = 0.0000000000;
                double CAnfnt3 = 0.0000000000;
                double CAnfnt4 = 0.0000000000;
                double CAnfnttotal = 0.0000000000;

                if (cacmd1 != 0.0)
                {
                    CACMDFinal1 = cacmd1 * gff1;
                    CACMDFinal2 = cacmd2 * gff2;
                    CACMDFinal3 = cacmd3 * gff3;
                    CACMDFinal4 = cacmd4 * gff4;
                    CACMDFinaltot = CACMDFinal1 + CACMDFinal2 + CACMDFinal3 + CACMDFinal4;

                    CAnfnt1 = cainj1 * gff1;
                    CAnfnt2 = cainj2 * gff2;
                    CAnfnt3 = cainj3 * gff3;
                    CAnfnt4 = cainj4 * gff4;
                    CAnfnttotal = CAnfnt1 + CAnfnt2 + CAnfnt3 + CAnfnt4;

                }
                else
                {
                    if (Timet1 == "Continuous")
                    {
                        CACMDFinal1 = CAc1 * gff1;
                        CAnfnt1 = CAinjCONT1 * gff1;
                    }
                    else
                    {
                        CACMDFinal1 = CAInst1 * gff1;
                        CAnfnt1 = CAinjINST1 * gff1;
                    }
                    if (Timet2 == "Continuous")
                    {
                        CACMDFinal2 = CAc2 * gff2;
                        CAnfnt2 = CAinjCONT2 * gff2;
                    }
                    else
                    {
                        CACMDFinal2 = CAInst2 * gff2;
                        CAnfnt2 = CAinjINST2 * gff2;
                    }
                    if (Timet3 == "Continuous")
                    {
                        CACMDFinal3 = CAc3 * gff3;
                        CAnfnt3 = CAinjCONT3 * gff3;
                    }
                    else
                    {
                        CACMDFinal3 = CAInst3 * gff3;
                        CAnfnt3 = CAinjINST3 * gff3;
                    }
                    if (Timet4 == "Continuous")
                    {
                        CACMDFinal4 = CAc4 * gff4;
                        CAnfnt4 = CAinjCONT4 * gff4;
                    }
                    else
                    {
                        CACMDFinal4 = CAInst4 * gff4;
                        CAnfnt4 = CAinjINST4 * gff4;
                    }
                    CACMDFinaltot = CACMDFinal1 + CACMDFinal2 + CACMDFinal3 + CACMDFinal4;
                    CAnfnttotal = CAnfnt1 + CAnfnt2 + CAnfnt3 + CAnfnt4;

                  

                }
                string CmdCate = string.Empty;
                string CAinjCate = string.Empty;

                if (CACMDFinaltot <= 100)
                {
                    CmdCate = "A";
                }
                else if (CACMDFinaltot > 100 && CACMDFinaltot <= 1000)
                {
                    CmdCate = "B";
                }
                else if (CACMDFinaltot > 1000 && CACMDFinaltot <= 3000)
                {
                    CmdCate = "C";
                }
                else if (CACMDFinaltot > 3000 && CACMDFinaltot <= 10000)
                {
                    CmdCate = "D";
                }
                else if (CACMDFinaltot > 10000)
                {
                    CmdCate = "E";
                }


                if (CAnfnttotal <= 100)
                {
                    CAinjCate = "A";
                }
                else if (CAnfnttotal > 100 && CAnfnttotal <= 1000)
                {
                    CAinjCate = "B";
                }
                else if (CAnfnttotal > 1000 && CAnfnttotal <= 3000)
                {
                    CAinjCate = "C";
                }
                else if (CAnfnttotal > 3000 && CAnfnttotal <= 10000)
                {
                    CAinjCate = "D";
                }
                else if (CAnfnttotal > 10000)
                {
                    CAinjCate = "E";
                }

                txt_COF_CmpDmg.Text = CACMDFinaltot.ToString();
                txt_COF_PerInj.Text = CAnfnttotal.ToString();

                txt_COF_DmCategory.Text = CmdCate.ToString();
                txt_COF_InCategory.Text = CAinjCate.ToString();

                string maxcate = string.Empty;
                double maxval = 0.0;
                //maxval = Math.Min(CACMDFinaltot, CAnfnttotal);
                if (CACMDFinaltot > CAnfnttotal)
                {
                    maxval = CACMDFinaltot;
                    maxcate = CmdCate.ToString();
                }
                else 
                {
                    maxval = CAnfnttotal;
                    maxcate = CAinjCate.ToString();
                }


                if (btn_COF_Cal.Text == " Calculate ")
                {
                    btn_COF_Cal.Text = "  Save   ";
                    btn_COF_Cal.BackColor = Color.Maroon;
                    btn_COF_Cal.ForeColor = Color.White;
                }
                else
                {
                     btn_COF_Cal.Text = " Calculate ";
                    btn_COF_Cal.BackColor = Color.FromArgb(240, 240, 240);
                    btn_COF_Cal.ForeColor = Color.Black;

                    string strqrydup = "Select  *  from COF_Flammable  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' and  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' and CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0"; 
                    SqlCommand cmddup = new SqlCommand(strqrydup, conn);
                    SqlDataReader readerdup = cmddup.ExecuteReader();
                    if (readerdup.Read()) 
                    {
                        readerdup.Close();
                        string sql5 = "update COF_Flammable set Fluid='" + cbo_FluidStored.SelectedItem.Text.ToString() + "',Repfluid='" + cbo_RepFluid.SelectedItem.Text.ToString() + "',Type='" + txt_FluidType.Text.ToString() + "',OpPres='" + txt_OpPressure.Text.ToString() + "',Optemp='" + txt_OpTemp.Text.ToString() + "',P1='" + txt_COF_P1.Text.ToString() + "',Mass='" + txt_COF_Masslbs.Text.ToString() + "',Detect='" + cbo_COF_Detection.SelectedItem.Text.ToString() + "',Iso='" + cbo_COF_Isolation.SelectedItem.Text.ToString() + "',factMit='" + cbo_COF_Mitigation.SelectedValue.ToString() + "',w1='" + w1.ToString("#.##") + "',w2='" + w2.ToString("#.##") + "',w3='" + w3.ToString("#.##") + "',w4='" + w4.ToString("#.##") + "',Time1='" + Timet1.ToString() + "',Time2='" + Timet2.ToString() + "',Time3='" + Timet3.ToString() + "',Time4='" + Timet4.ToString() + "',T1='" + t1.ToString("#.##") + "',T2='" + t2.ToString("#.##") + "',T3='" + t3.ToString("#.##") + "',T4='" + t4.ToString("#.##") + "',rate1='" + rate1.ToString("#.##") + "',rate2='" + rate2.ToString("#.##") + "',rate3='" + rate3.ToString("#.##") + "',rate4='" + rate4.ToString("#.##") + "',id1='" + useld1.ToString() + "',id2='" + useld2.ToString() + "',id3='" + useld3.ToString() + "',id4='" + useld4.ToString() + "',mass1='" + mass1.ToString("#.##") + "',mass2='" + mass2.ToString("#.##") + "',mass3='" + mass3.ToString("#.##") + "',mass4='" + mass4.ToString("#.##") + "',eneff='" + eneffn.ToString("#.##") + "',CaA='" + CONTa.ToString() + "',CaB='" + CONTb.ToString() + "',CAc1='" + CAc1.ToString("#.##") + "',CAc2='" + CAc2.ToString("#.##") + "',CAc3='" + CAc3.ToString("#.##") + "',CAc4='" + CAc4.ToString("#.##") + "',CaInsA= '" + INSTa.ToString() + "',CaInsB= '" + INSTb.ToString() + "',CAIns1='" + CAInst1.ToString("#.##") + "',CAIns2='" + CAInst2.ToString("#.##") + "',CAIns3='" + CAInst3.ToString("#.##") + "',CAIns4='" + CAInst4.ToString("#.##") + "',Ainj= '" + InjCONTa.ToString() + "',Binj= '" + InjCONTb.ToString() + "',CAInj1='" + CAinjCONT1.ToString("#.##") + "',CAInj2='" + CAinjCONT2.ToString("#.##") + "',CAInj3='" + CAinjCONT3.ToString("#.##") + "',CAInj4='" + CAinjCONT4.ToString("#.##") + "',AInsInj='" + InjINSTa.ToString() + "',BInsInj='" + InjINSTb.ToString() + "',CAInsInj1='" + CAinjINST1.ToString("#.##") + "',CAInsInj2='" + CAinjINST2.ToString("#.##") + "',CAInsInj3='" + CAinjINST3.ToString("#.##") + "',CAInsInj4='" + CAinjINST4.ToString("#.##") + "',factic1='" + factic1.ToString("#.##") + "',factic2='" + factic2.ToString("#.##") + "',factic3='" + factic3.ToString("#.##") + "',factic4='" + factic4.ToString("#.##") + "',CAcmd1='" + cacmd1.ToString("#.##") + "',CAcmd2='" + cacmd2.ToString("#.##") + "',CAcmd3='" + cacmd3.ToString("#.##") + "',CAcmd4='" + cacmd4.ToString("#.##") + "',CAbleInj1='" + cainj1.ToString("#.##") + "',CAbleInj2='" + cainj2.ToString("#.##") + "',CAbleInj3='" + cainj3.ToString("#.##") + "',CAbleInj4='" + cainj4.ToString("#.##") + "',CAcmdFinal1= '" + CACMDFinal1.ToString("#.##") + "',CAcmdFinal2= '" + CACMDFinal2.ToString("#.##") + "',CAcmdFinal3= '" + CACMDFinal3.ToString("#.##") + "',CAcmdFinal4= '" + CACMDFinal4.ToString("#.##") + "',CAInjFinal1= '" + CAnfnt1.ToString("#.##") + "',CAInjFinal2= '" + CAnfnt2.ToString("#.##") + "',CAInjFinal3= '" + CAnfnt3.ToString("#.##") + "',CAInjFinal4= '" + CAnfnt4.ToString("#.##") + "',CAcmdTotal= '" + CACMDFinaltot.ToString("#.##") + "',CAInjTotal='" + CAnfnttotal.ToString("#.##") + "',CAcmdCate='" + CmdCate.ToString() + "',CAinjCate='" + CAinjCate.ToString() + "',maxval='" + maxval.ToString("#.##") + "',maxcate='" + maxcate.ToString() + "',CreatedBy=" + Convert.ToInt32(Session["sesUserID"].ToString()) + " ,ptrans='" + ptrans.ToString() + "',FluidStored='" + txt_FluidStored.Text.ToString() + "' where ProcID=" + cboProcessArea.SelectedValue.ToString() + " and EquID=" + cboEquipment.SelectedValue.ToString() + " and CompID=" + cboComponent.SelectedValue.ToString() + "";
                        SqlCommand cmd5 = new SqlCommand(sql5, conn);
                        cmd5.ExecuteNonQuery();
                        lblStatus.Text = "Successfully Value Updated";
                        lblStatus.ForeColor = Color.Blue;
                    }
                    else
                    {
                        readerdup.Close();
                        string sql5 = "insert into COF_Flammable (ProcID,EquID,CompID,Fluid,Repfluid,Type,OpPres,Optemp,P1,Mass,Detect,Iso,factMit,w1,w2,w3,w4,Time1,Time2,Time3,Time4,T1,T2,T3,T4,rate1,rate2,rate3,rate4,id1,id2,id3,id4,mass1,mass2,mass3,mass4,eneff,CaA,CaB,CAc1,CAc2,CAc3,CAc4,CaInsA,CaInsB,CAIns1,CAIns2,CAIns3,CAIns4,Ainj,Binj,CAInj1,CAInj2,CAInj3,CAInj4,AInsInj,BInsInj,CAInsInj1,CAInsInj2,CAInsInj3,CAInsInj4,factic1,factic2,factic3,factic4,CAcmd1,CAcmd2,CAcmd3,CAcmd4,CAbleInj1,CAbleInj2,CAbleInj3,CAbleInj4,CAcmdFinal1,CAcmdFinal2,CAcmdFinal3,CAcmdFinal4,CAInjFinal1,CAInjFinal2,CAInjFinal3,CAInjFinal4,CAcmdTotal,CAInjTotal,CAcmdCate,CAinjCate,maxval,maxcate,CreatedBy,ptrans,FluidStored)   values   (" + cboProcessArea.SelectedValue.ToString() + "," + cboEquipment.SelectedValue.ToString() + "," + cboComponent.SelectedValue.ToString() + ",'" + cbo_FluidStored.SelectedItem.Text.ToString() + "','" + cbo_RepFluid.SelectedItem.Text.ToString() + "','" + txt_FluidType.Text.ToString() + "','" + txt_OpPressure.Text.ToString() + "','" + txt_OpTemp.Text.ToString() + "','" + txt_COF_P1.Text.ToString() + "','" + txt_COF_Masslbs.Text.ToString() + "','" + cbo_COF_Detection.SelectedItem.Text.ToString() + "','" + cbo_COF_Isolation.SelectedItem.Text.ToString() + "','" + cbo_COF_Mitigation.SelectedValue.ToString() + "','" + w1.ToString("#.##") + "','" + w2.ToString("#.##") + "','" + w3.ToString("#.##") + "','" + w4.ToString("#.##") + "','" + Timet1.ToString() + "','" + Timet2.ToString() + "','" + Timet3.ToString() + "','" + Timet4.ToString() + "','" + t1.ToString("#.##") + "','" + t2.ToString("#.##") + "','" + t3.ToString("#.##") + "','" + t4.ToString("#.##") + "','" + rate1.ToString("#.##") + "','" + rate2.ToString("#.##") + "','" + rate3.ToString("#.##") + "','" + rate4.ToString("#.##") + "','" + useld1.ToString() + "','" + useld2.ToString() + "','" + useld3.ToString() + "','" + useld4.ToString() + "','" + mass1.ToString("#.##") + "','" + mass2.ToString("#.##") + "','" + mass3.ToString("#.##") + "','" + mass4.ToString("#.##") + "','" + eneffn.ToString("#.##") + "','" + CONTa.ToString() + "','" + CONTb.ToString() + "','" + CAc1.ToString("#.##") + "','" + CAc2.ToString("#.##") + "','" + CAc3.ToString("#.##") + "','" + CAc4.ToString("#.##") + "','" + INSTa.ToString() + "','" + INSTb.ToString() + "','" + CAInst1.ToString("#.##") + "','" + CAInst2.ToString("#.##") + "','" + CAInst3.ToString("#.##") + "','" + CAInst4.ToString("#.##") + "','" + InjCONTa.ToString() + "','" + InjCONTb.ToString() + "','" + CAinjCONT1.ToString("#.##") + "','" + CAinjCONT2.ToString("#.##") + "','" + CAinjCONT3.ToString("#.##") + "','" + CAinjCONT4.ToString("#.##") + "','" + InjINSTa.ToString() + "','" + InjINSTb.ToString("#.##") + "','" + CAinjINST1.ToString("#.##") + "','" + CAinjINST2.ToString("#.##") + "','" + CAinjINST3.ToString("#.##") + "','" + CAinjINST4.ToString("#.##") + "','" + factic1.ToString("#.##") + "','" + factic2.ToString("#.##") + "','" + factic3.ToString("#.##") + "','" + factic4.ToString("#.##") + "','" + cacmd1.ToString("#.##") + "','" + cacmd2.ToString("#.##") + "','" + cacmd3.ToString("#.##") + "','" + cacmd4.ToString("#.##") + "','" + cainj1.ToString("#.##") + "','" + cainj2.ToString("#.##") + "','" + cainj3.ToString("#.##") + "','" + cainj4.ToString("#.##") + "','" + CACMDFinal1.ToString("#.##") + "','" + CACMDFinal2.ToString("#.##") + "','" + CACMDFinal3.ToString("#.##") + "','" + CACMDFinal4.ToString("#.##") + "','" + CAnfnt1.ToString("#.##") + "','" + CAnfnt2.ToString("#.##") + "','" + CAnfnt3.ToString("#.##") + "','" + CAnfnt4.ToString("#.##") + "','" + CACMDFinaltot.ToString("#.##") + "','" + CAnfnttotal.ToString("#.##") + "','" + CmdCate.ToString() + "','" + CAinjCate.ToString() + "','" + maxval.ToString("#.##") + "','" + maxcate.ToString() + "'," + Convert.ToInt32(Session["sesUserID"].ToString()) + ",'" + ptrans.ToString() + "','" + txt_FluidStored.Text.ToString() + "')";

                        SqlCommand cmd5 = new SqlCommand(sql5, conn);
                        cmd5.ExecuteNonQuery();
                        lblStatus.Text = "Successfully Value Inserted";
                        lblStatus.ForeColor = Color.Blue;
                    }
                   
                }
                BusinessTier.DisposeConnection(conn);
            }

            catch (Exception ex)
            {
               // lblStatus.Text = "Err: " + ex.Message.ToString();
            }
        }
    }

    protected void btn_COF_Cancel_Click(object sender, EventArgs e)
    {
        btn_COF_Cal.Text = " Calculate ";
        btn_COF_Cal.BackColor = Color.FromArgb(240, 240, 240);
        btn_COF_Cal.ForeColor = Color.Black;
    }

    protected void btnCOFFlameSubmit_Click(object sender, EventArgs e)
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
              string strqrydup = "Select  *  from COF_Flammable  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' and  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' and CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0"; 
                    SqlCommand cmddup = new SqlCommand(strqrydup, conn);
                    SqlDataReader readerdup = cmddup.ExecuteReader();
                    if (readerdup.Read())
                    {

                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('ReportsCOFFlame.aspx?param1=" + cboProcessArea.SelectedValue.ToString().Trim() + "&param2=" + cboEquipment.SelectedValue.ToString().Trim() + "&param3=" + cboComponent.SelectedValue.ToString().Trim() + "');", true);
                        btnCOFFlameDelete.Enabled = true;
                       
                    }
                    else
                    {
                        lblStatus.Text = "This Component don't have value you can enter the new data";
                        lblStatus.ForeColor = Color.Blue;
                    }
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "COF", "btnCOFFlameSubmit_Click", ex.ToString(), "Audit");
            lblStatus.Text = ex.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    protected void btnCOFFlameDelete_Click(object sender, EventArgs e)
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
            string strqrydup = "delete from COF_Flammable where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' and  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' and CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and Deleted=0";
            
            SqlCommand cmddup = new SqlCommand(strqrydup, conn);
            cmddup.ExecuteNonQuery();
                lblStatus.Text = "COF Flammable Value Successfully Deleted";
                lblStatus.ForeColor = Color.Maroon;
           
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "COF", "btnCOFFlameSubmit_Click", ex.ToString(), "Audit");
            lblStatus.Text = ex.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    //<-------------------Non Flammable------------------------>

    protected void NonFlammable_OnSelectedIndexChanged_FluidStored(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        RadComboBox combobox = (RadComboBox)sender;
        if (cbo_COF_NonFluid.SelectedItem.Text.ToString() == "Liquid")
        {
            txt_COF_NonP1.Enabled = true;
        }
        else
        {
            txt_COF_NonP1.Text = string.Empty;
            txt_COF_NonP1.Enabled = false;
        }
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            string sql1 = "select Fluid from Ref_COF_Lvl_1 where fluid like 'steam' or fluid like 'water' or fluid like 'Acid'";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            cbo_COF_NonRefFluid.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["Fluid"].ToString();
                cbo_COF_NonRefFluid.Items.Add(item);
                item.DataBind();
            }

        }
        catch (Exception ex)
        {
            BusinessTier.DisposeConnection(conn);
          //  lblStatus.Text = ex.Message.ToString();
            return;
        }
    }

    protected void NonOnSelectedIndexChanged_Detection(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {

        if (cbo_COF_NonDetect.SelectedItem.Text == "A")
        {
            lbl_COF_NonDetect.Text = "Instrumentation designed specially to detect material losses by chenges in operating conditions (i.e., loss of pressure or flow) in the system.";
        }
        else if (cbo_COF_NonDetect.SelectedItem.Text == "B")
        {
            lbl_COF_NonDetect.Text = "Suitably locate detectors to determine when the matrial is present outside the pressure-containing envelope.";
        }
        else if (cbo_COF_NonDetect.SelectedItem.Text == "C")
        {
            lbl_COF_NonDetect.Text = "Visual detection,cameras,or detectors with marginal coverage.";
        }

    }

    protected void NonOnSelectedIndexChanged_Isolation(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {

        if (cbo_COF_NonIso.SelectedItem.Text == "A")
        {
            lbl_COF_NonIso.Text = "Isolation shutdown systems activated directly from process instrumentation or detectors, with no operator intervention.";
        }
        else if (cbo_COF_NonIso.SelectedItem.Text == "B")
        {
            lbl_COF_NonIso.Text = "Isolation shutdown systems activated by operators in the control room or other suitable locations remote from the leak.";
        }
        else if (cbo_COF_NonIso.SelectedItem.Text == "C")
        {
            lbl_COF_NonIso.Text = "Isolation dependent on manually-operated valves.";
        }

    }

    protected void NonOnSelectedIndexChanged_RepFluid(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            lblStatus.Text = string.Empty;
            string sql1 = "select Fluid_Type from Ref_COF_Rep_Fluids where Rep_Fluid='" + cbo_COF_NonRefFluid.SelectedItem.Text.ToString().Trim() + "'";
            SqlCommand cmd = new SqlCommand(sql1, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            rd.Read();
            txt_COF_Non_FluidType.Text = rd["Fluid_Type"].ToString().Trim();
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

    protected void btn_COF_NonCalc_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cboComponent.Text.ToString().Trim()) || string.IsNullOrEmpty(cboProcessArea.Text.ToString().Trim()) || string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_COF_NonFluid.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_COF_NonRefFluid.Text.ToString().Trim()) || string.IsNullOrEmpty(cbo_COF_NonFinal.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_COF_NonOpPressure.Text.ToString().Trim()) || string.IsNullOrEmpty(txt_COF_NonOpTemp.Text.ToString().Trim()))
        {
            lblStatus.Text = "Err : Please check all the  fields";
            lblStatus.ForeColor = Color.Red;
        }
        else
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            try
            {
                lblStatus.Text = string.Empty;
                string sql1 = "select MW,LqdDensity,NBF,Ambient,IdealGas,A,B,C,D,E,IgnTemp from Ref_COF_Lvl_1 where Fluid='" + cbo_COF_NonRefFluid.SelectedItem.Text.ToString().Trim() + "'";
                SqlCommand cmd = new SqlCommand(sql1, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                rd.Read();
                string MW = rd["MW"].ToString().Trim();
                string LqdDensity = rd["LqdDensity"].ToString().Trim();
                string NBF = rd["NBF"].ToString().Trim();
                string Ambient = rd["Ambient"].ToString().Trim();
                string IdealGas = rd["IdealGas"].ToString().Trim();
                string A = rd["A"].ToString().Trim();
                string B = rd["B"].ToString().Trim();
                string C = rd["C"].ToString().Trim();
                string D = rd["D"].ToString().Trim();
                string E = rd["E"].ToString().Trim();
                string IgnTemp = rd["IgnTemp"].ToString().Trim();
                double T = Convert.ToDouble(txt_COF_NonOpTemp.Text.ToString().Trim()) + 273;
                double A1 = 0.049;
                double A2 = 0.785;
                double A3 = 12.56;
                double A4 = 200.96;

                double w1 = 0.0000000000;
                double w2 = 0.0000000000;
                double w3 = 0.0000000000;
                double w4 = 0.0000000000;

                double ptrans = 0.0000000000;

                BusinessTier.DisposeReader(rd);
                if (cbo_COF_NonFluid.SelectedValue.ToString() == "Liquid")
                {
                    double NBPV = Convert.ToDouble(NBF.ToString());
                    double LqdDensityV = Convert.ToDouble(LqdDensity.ToString());
                    // double IgnTempV = Convert.ToDouble(IgnTemp.ToString());
                    //--------------------------Release Rate Calculation---------------------------------
                    double Kvn = 1.0;
                    double ps = Convert.ToDouble(txt_COF_NonOpPressure.Text.ToString());
                    double patm = 14.7;
                    double p = Convert.ToDouble(txt_COF_NonP1.Text.ToString().Trim());
                    double gc = 32.2;
                    double c1 = 12.0;
                    double cd = 0.61;
                    w1 = cd * Kvn * p * (A1 / c1) * Math.Sqrt(2 * gc * (ps - patm) / p);
                    w2 = cd * Kvn * p * (A2 / c1) * Math.Sqrt(2 * gc * (ps - patm) / p);
                    w3 = cd * Kvn * p * (A3 / c1) * Math.Sqrt(2 * gc * (ps - patm) / p);
                    w4 = cd * Kvn * p * (A4 / c1) * Math.Sqrt(2 * gc * (ps - patm) / p);
                }
                else if (cbo_COF_NonFluid.SelectedValue.ToString() == "Gas")
                {
                    double cp = 0.00000000000;
                    if (IdealGas.ToString() == "Note 1")
                    {
                        cp = Convert.ToDouble(A.ToString()) + (Convert.ToDouble(B.ToString()) * Convert.ToDouble(T.ToString())) + (Convert.ToDouble(C.ToString()) * Math.Pow(Convert.ToDouble(T.ToString()), 2)) + (Convert.ToDouble(D.ToString()) * Math.Pow(Convert.ToDouble(T.ToString()), 3));
                    }
                    else if (IdealGas.ToString() == "Note 2")
                    {
                        double ct = (Convert.ToDouble(C.ToString()) / Convert.ToDouble(T.ToString())) / Math.Sinh(Convert.ToDouble(C.ToString()) / Convert.ToDouble(T.ToString()));
                        double et = (Convert.ToDouble(E.ToString()) / Convert.ToDouble(T.ToString())) / Math.Cosh(Convert.ToDouble(E.ToString()) / Convert.ToDouble(T.ToString()));
                        cp = Convert.ToDouble(A.ToString()) + (Convert.ToDouble(B.ToString()) * Math.Pow(ct, 2)) + (Convert.ToDouble(D.ToString()) * Math.Pow(et, 2));
                    }
                    else if (IdealGas.ToString() == "Note 3")
                    {
                        cp = Convert.ToDouble(A.ToString()) + (Convert.ToDouble(B.ToString()) * Convert.ToDouble(T.ToString())) + (Convert.ToDouble(C.ToString()) * Math.Pow(Convert.ToDouble(T.ToString()), 2)) + (Convert.ToDouble(D.ToString()) * Math.Pow(Convert.ToDouble(T.ToString()), 3)) + (Convert.ToDouble(E.ToString()) * Math.Pow(Convert.ToDouble(T.ToString()), 4));
                    }
                    double R = 8.314;
                    double K = cp / (cp - R);
                    //--------------------------Release Rate Calculation---------------------------------
                    double ps = Convert.ToDouble(txt_COF_NonOpPressure.Text.ToString());
                    double patm = 14.7;
                    double gc = 32.2;
                    double c2 = 1.0;
                    double cd = 0.90;
                    double mw = Convert.ToDouble(MW.ToString());
                    double Ts = (Convert.ToDouble(txt_COF_NonOpTemp.Text.ToString()) * 1.8) + 491.67;
                    double R1 = 1545;
                    ptrans = patm * Math.Pow(((K + 1) / 2), K / (K - 1));
                    if (ps > ptrans)//-----------------------------------Sonic---------------------------
                    {
                        double kmw = (K * mw * gc) / (R1 * Ts);
                        double power = (K + 1) / (K - 1);
                        double kplus = Math.Pow((2 / (K + 1)), power);
                        w1 = (cd / c2) * A1 * ps * Math.Sqrt(kmw * kplus);
                        w2 = (cd / c2) * A2 * ps * Math.Sqrt(kmw * kplus);
                        w3 = (cd / c2) * A3 * ps * Math.Sqrt(kmw * kplus);
                        w4 = (cd / c2) * A4 * ps * Math.Sqrt(kmw * kplus);
                    }
                    else//--------------------------------SubSonic----------------------- if (ps < ptrans)
                    {
                        double gmw = (mw * gc) / (R1 * Ts);
                        double k2 = (2 * K) / (K - 1);
                        double patmps = Math.Pow(patm / ps, 2 / K);
                        double patmps1 = 1 - (Math.Pow(patm / ps, (K - 1) / K));
                        w1 = (cd / c2) * A1 * ps * Math.Sqrt(gmw * k2 * patmps * patmps1);
                        w2 = (cd / c2) * A2 * ps * Math.Sqrt(gmw * k2 * patmps * patmps1);
                        w3 = (cd / c2) * A3 * ps * Math.Sqrt(gmw * k2 * patmps * patmps1);
                        w4 = (cd / c2) * A4 * ps * Math.Sqrt(gmw * k2 * patmps * patmps1);
                    }

                }
                double c3 = 0.00000000000;
                if (Convert.ToDouble(txt_COF_NonMass.Text.ToString().Trim()) >= 10000.00)
                {
                    c3 = 10000;
                }
                else
                {
                    c3 = Convert.ToDouble(txt_COF_NonMass.Text.ToString());
                }
                double t1 = c3 / w1;
                double t2 = c3 / w2;
                double t3 = c3 / w3;
                double t4 = c3 / w4;
                string Timet1 = string.Empty;
                string Timet2 = string.Empty;
                string Timet3 = string.Empty;
                string Timet4 = string.Empty;
                //-----------------------------T1
                //if (t1 <= 180)
                //{
                //    Timet1 = "Instantaneous";
                //}
                //else
                //{
                    Timet1 = "Continuous";
                //}
                //-----------------------------T2
                if (t2 <= 180)
                {
                    Timet2 = "Instantaneous";
                }
                else
                {
                    Timet2 = "Continuous";
                }
                //-----------------------------T3
                if (t3 <= 180)
                {
                    Timet3 = "Instantaneous";
                }
                else
                {
                    Timet3 = "Continuous";
                }
                //-----------------------------T4
                if (t4 <= 180)
                {
                    Timet4 = "Instantaneous";
                }
                else
                {
                    Timet4 = "Continuous";
                }


                //---------------------------------Detection & Isolation-------------------------
                string sql2 = "select Reduction from Ref_COF_Adj_Det_Iso where Detection='" + cbo_COF_NonDetect.SelectedItem.Text.ToString().Trim() + "' and  Isolat='" + cbo_COF_NonIso.SelectedItem.Text.ToString().Trim() + "'";
                SqlCommand cmd1 = new SqlCommand(sql2, conn);
                SqlDataReader rd1 = cmd1.ExecuteReader();
                rd1.Read();
                double factdi = Convert.ToDouble(rd1["Reduction"].ToString().Trim());
                BusinessTier.DisposeReader(rd1);

                string sql3 = "select Inch25,Inch1,Inch4 from Ref_COF_Leak_Det_Iso where Detection='" + cbo_COF_NonDetect.SelectedItem.Text.ToString().Trim() + "' and  Isolat='" + cbo_COF_NonIso.SelectedItem.Text.ToString().Trim() + "'";
                SqlCommand cmd3 = new SqlCommand(sql3, conn);
                SqlDataReader rd3 = cmd3.ExecuteReader();
                rd3.Read();
                double ld1 = Convert.ToDouble(rd3["Inch25"].ToString().Trim());
                double ld2 = Convert.ToDouble(rd3["Inch1"].ToString().Trim());
                double ld3 = Convert.ToDouble(rd3["Inch4"].ToString().Trim());
                BusinessTier.DisposeReader(rd3);

                double rate1 = w1 * (1 - factdi);
                double rate2 = w2 * (1 - factdi);
                double rate3 = w3 * (1 - factdi);
                double rate4 = w4 * (1 - factdi);

                double usld1 = Math.Min(Convert.ToDouble(txt_COF_NonMass.Text.ToString().Trim()) / rate1 / 60, ld1);
                double usld2 = Math.Min(Convert.ToDouble(txt_COF_NonMass.Text.ToString().Trim()) / rate2 / 60, ld2);
                double usld3 = Math.Min(Convert.ToDouble(txt_COF_NonMass.Text.ToString().Trim()) / rate3 / 60, ld3);
                double usld4 = Convert.ToDouble(txt_COF_NonMass.Text.ToString().Trim()) / rate4 / 60;

                string useld1 = string.Empty;
                string useld2 = string.Empty;
                string useld3 = string.Empty;
                string useld4 = string.Empty;

                double mass1 = 0.00000000000;
                double mass2 = 0.00000000000;
                double mass3 = 0.00000000000;
                double mass4 = 0.00000000000;
               
               
                double C9 = 0.6;
                double C10 = 63.32;
                double C5 = 55.6;

                double CAc1 = 0.0000000000;
                double CAc2 = 0.0000000000;
                double CAc3 = 0.0000000000;
                double CAc4 = 0.0000000000;

                double effrate1 = 0.0000000000;
                double effrate2 = 0.0000000000;
                double effrate3 = 0.0000000000;
                double effrate4 = 0.0000000000;

                double effmass1 = 0.0000000000;
                double effmass2 = 0.0000000000;
                double effmass3 = 0.0000000000;
                double effmass4 = 0.0000000000;

                double factic1 = 0.0000000000;
                double factic2 = 0.0000000000;
                double factic3 = 0.0000000000;
                double factic4 = 0.0000000000;

                double CAInj1 = 0.0000000000;
                double CAInj2 = 0.0000000000;
                double CAInj3 = 0.0000000000;
                double CAInj4 = 0.0000000000;

                double CAInjInst1 = 0.0000000000;
                double CAInjInst2 = 0.0000000000;
                double CAInjInst3 = 0.0000000000;
                double CAInjInst4 = 0.0000000000;

                double CAnfnt1 = 0.0000000000;
                double CAnfnt2 = 0.0000000000;
                double CAnfnt3 = 0.0000000000;
                double CAnfnt4 = 0.0000000000;

                double CAInjTotal = 0.0000000000;

                string CmdCate = string.Empty;
                string CAinjCate = string.Empty;
                double eneffn = 0.0000000000;


                double g = 0.0000000000;
                double h = 0.0000000000;


                if (cbo_COF_NonRefFluid.Text.ToString() == "steam")
                {
                    //-----------------------------useld1 & Mass1
                    if (Timet1 == "Continuous")
                    {
                        useld1 = usld1.ToString("#.##");
                        mass1 = rate1 * usld1 * 60;
                    }
                    else
                    {
                        useld1 = "Instantaneous";
                        mass1 = Convert.ToDouble(txt_COF_NonMass.Text.ToString().Trim()) * 1;
                    }
                    //-----------------------------useld2 & Mass2
                    if (Timet2 == "Continuous")
                    {
                        useld2 = usld2.ToString("#.##");
                        mass2 = rate2 * usld2 * 60;
                    }
                    else
                    {
                        useld2 = "Instantaneous";
                        mass2 = Convert.ToDouble(txt_COF_NonMass.Text.ToString().Trim()) * 1;

                    }
                    //-----------------------------useld3 & Mass3
                    if (Timet3 == "Continuous")
                    {
                        useld3 = usld3.ToString("#.##");
                        mass3 = rate3 * usld3 * 60;
                    }
                    else
                    {
                        useld3 = "Instantaneous";
                        mass3 = Convert.ToDouble(txt_COF_NonMass.Text.ToString().Trim()) * 1;
                    }
                    //-----------------------------useld4 & Mass3
                    if (Timet4 == "Continuous")
                    {
                        useld4 = usld4.ToString("#.##");
                        mass4 = rate4 * usld4 * 60;
                    }
                    else
                    {
                        useld4 = "Instantaneous";
                        mass4 = Convert.ToDouble(txt_COF_NonMass.Text.ToString().Trim()) * 1;
                    }
                    //-----------------------------Mitigation Value
                    double factmit = Convert.ToDouble(cbo_COF_NonMiti.SelectedValue.ToString().Trim());
                    //-----------------------------efficiency correction
                  
                    double c4 = 1.0;
                    if (Convert.ToDouble(txt_COF_NonMass.Text.ToString().Trim()) > 10000 || Timet1 == "Instantaneous" && Timet2 == "Instantaneous" && Timet3 == "Instantaneous" && Timet4 == "Instantaneous")
                    {
                        eneffn = 4 * Math.Log10(c4 * Convert.ToDouble(txt_COF_NonMass.Text.ToString().Trim())) - 15;
                    }
                    else
                    {
                        eneffn = 1.0;
                    }
                    // ---------------with blending mode
                    if (txt_COF_Non_FluidType.Text.ToString().Trim() == "TYPE 0" && Convert.ToDouble(txt_COF_NonMass.Text.ToString().Trim()) >= 10000.00)
                    {
                        factic1 = Math.Min(rate1 / C5, 1.0);
                        factic2 = Math.Min(rate2 / C5, 1.0);
                        factic3 = Math.Min(rate3 / C5, 1.0);
                        factic4 = Math.Min(rate4 / C5, 1.0);

                        if (Timet1 == "Continuous")
                        {
                            CAInj1 = C9 * rate1;
                            effrate1 = rate1;
                        }

                        //---------------For Instantaneous Release CAInst4
                        else if (Timet1 == "Instantaneous")
                        {

                            CAInjInst1 = C10 * Math.Pow(mass1, 0.6384);
                            effmass1 = mass1;
                        }
                        //---------------For Coninuous Release CAc4
                        if (Timet2 == "Continuous")
                        {
                            CAInj2 = C9 * rate2;
                            effrate2 = rate2;
                        }

                       //---------------For Instantaneous Release CAInst4
                        else if (Timet2 == "Instantaneous")
                        {

                            CAInjInst2 = C10 * Math.Pow(mass2, 0.6384);
                            effmass2 = mass2;
                        }
                        //---------------For Coninuous Release CAc4
                        if (Timet3 == "Continuous")
                        {
                            CAInj3 = C9 * rate3;
                            effrate3 = rate3;
                        }

                       //---------------For Instantaneous Release CAInst4
                        else if (Timet3 == "Instantaneous")
                        {

                            CAInjInst3 = C10 * Math.Pow(mass3, 0.6384);
                            effmass3 = mass3;
                        }
                        //---------------For Coninuous Release CAc4
                        if (Timet4 == "Continuous")
                        {
                            CAInj4 = C9 * rate4;
                            effrate4 = rate4;
                        }

                       //---------------For Instantaneous Release CAInst4
                        else if (Timet4 == "Instantaneous")
                        {

                            CAInjInst4 = C10 * Math.Pow(mass4, 0.6384);
                            effmass4 = mass4;
                        }


                        //CAc1 = CAInj1 * factic1 + CAInjInst1 * (1 - factic1);
                        //CAc2 = CAInj2 * factic2 + CAInjInst2 * (1 - factic2);
                        //CAc3 = CAInj3 * factic3 + CAInjInst3 * (1 - factic3);
                        //CAc4 = CAInj4 * factic4 + CAInjInst4 * (1 - factic4);

                        CAc1 = CAInjInst1 * factic1 + CAInj1 * (1 - factic1);
                        CAc2 = CAInjInst2 * factic2 + CAInj2 * (1 - factic2);
                        CAc3 = CAInjInst3 * factic3 + CAInj3 * (1 - factic3);
                        CAc4 = CAInjInst4 * factic4 + CAInj4 * (1 - factic4);

                        CAnfnt1 = CAc1 * 0.261;
                        CAnfnt2 = CAc2 * 0.654;
                        CAnfnt3 = CAc3 * 0.065;
                        CAnfnt4 = CAc4 * 0.02;

                        CAInjTotal = CAnfnt1 + CAnfnt2 + CAnfnt3 + CAnfnt4;

                        if (CAInjTotal <= 100)
                        {
                            CmdCate = "A";
                        }
                        else if (CAInjTotal > 100 && CAInjTotal <= 1000)
                        {
                            CmdCate = "B";
                        }
                        else if (CAInjTotal > 1000 && CAInjTotal <= 3000)
                        {
                            CmdCate = "C";
                        }
                        else if (CAInjTotal > 3000 && CAInjTotal <= 10000)
                        {
                            CmdCate = "D";
                        }
                        else if (CAInjTotal > 10000)
                        {
                            CmdCate = "E";
                        }

                        txt_COF_NonPersonnel.Text = CAInjTotal.ToString();
                        txt_COF_NonCategory.Text = CmdCate.ToString();
                    }
                    else // ---------------without blending mode
                    {
                        if (Timet1 == "Continuous")
                        {
                            CAInj1 = C9 * rate1;
                            effrate1 = rate1;
                        }

                       //---------------For Instantaneous Release CAInst4
                        else if (Timet1 == "Instantaneous")
                        {

                            CAInjInst1 = C10 * Math.Pow(mass1, 0.6384);
                            effmass1 = mass1;
                        }
                        //---------------For Coninuous Release CAc4
                        if (Timet2 == "Continuous")
                        {
                            CAInj2 = C9 * rate2;
                            effrate2 = rate2;
                        }

                       //---------------For Instantaneous Release CAInst4
                        else if (Timet2 == "Instantaneous")
                        {

                            CAInjInst2 = C10 * Math.Pow(mass2, 0.6384);
                            effmass2 = mass2;
                        }
                        //---------------For Coninuous Release CAc4
                        if (Timet3 == "Continuous")
                        {
                            CAInj3 = C9 * rate3;
                            effrate3 = rate3;
                        }

                       //---------------For Instantaneous Release CAInst4
                        else if (Timet3 == "Instantaneous")
                        {

                            CAInjInst3 = C10 * Math.Pow(mass3, 0.6384);
                            effmass3 = mass3;
                        }
                        //---------------For Coninuous Release CAc4
                        if (Timet4 == "Continuous")
                        {
                            CAInj4 = C9 * rate4;
                            effrate4 = rate4;
                        }

                       //---------------For Instantaneous Release CAInst4
                        else if (Timet4 == "Instantaneous")
                        {

                            CAInjInst4 = C10 * Math.Pow(mass4, 0.6384);
                            effmass4 = mass4;
                        }

                        CAc1 = Math.Max(CAInj1, CAInjInst1);
                        CAc2 = Math.Max(CAInj2, CAInjInst2);
                        CAc3 = Math.Max(CAInj3, CAInjInst3);
                        CAc4 = Math.Max(CAInj4, CAInjInst4);

                        CAnfnt1 = CAc1 * 0.261;
                        CAnfnt2 = CAc2 * 0.654;
                        CAnfnt3 = CAc3 * 0.065;
                        CAnfnt4 = CAc4 * 0.02;

                        CAInjTotal = CAnfnt1 + CAnfnt2 + CAnfnt3 + CAnfnt4;

                        if (CAInjTotal <= 100)
                        {
                            CmdCate = "A";
                        }
                        else if (CAInjTotal > 100 && CAInjTotal <= 1000)
                        {
                            CmdCate = "B";
                        }
                        else if (CAInjTotal > 1000 && CAInjTotal <= 3000)
                        {
                            CmdCate = "C";
                        }
                        else if (CAInjTotal > 3000 && CAInjTotal <= 10000)
                        {
                            CmdCate = "D";
                        }
                        else if (CAInjTotal > 10000)
                        {
                            CmdCate = "E";
                        }

                        txt_COF_NonPersonnel.Text = CAInjTotal.ToString();
                        txt_COF_NonCategory.Text = CmdCate.ToString();
                    }
                }
                else    //----------------------------------Condition for checking Steam Or water
                {



                    double C8 = 1;
                    double C4 = 1;
                    double C11 = 1;

                    double ps = Convert.ToDouble(txt_COF_NonOpPressure.Text.ToString());
                    double patm = 14.7;
                    g = 2696.0 - 21.9 * C11 * (ps - patm) + 1.474 * Math.Pow(C11 * (ps - patm), 2);
                    h = 0.31 - 0.00032 * Math.Pow(C11 * (ps - patm) - 40, 2);

                    if (Timet1 == "Continuous")
                    {
                        CAc1 = 0.2 * C8 * g * Math.Pow((C4 * rate1), h);
                    }
                    else
                    {
                        CAc1 = 0.0;
                    }

                    if (Timet2 == "Continuous")
                    {
                        CAc2 = 0.2 * C8 * g * Math.Pow((C4 * rate2), h);
                    }
                    else
                    {
                        CAc2 = 0.0;
                    }

                    if (Timet3 == "Continuous")
                    {
                        CAc3 = 0.2 * C8 * g * Math.Pow((C4 * rate3), h);
                    }
                    else
                    {
                        CAc3 = 0.0;
                    }

                    if (Timet4 == "Continuous")
                    {
                        CAc4 = 0.2 * C8 * g * Math.Pow((C4 * rate4), h);
                    }
                    else
                    {
                        CAc4 = 0.0;
                    }

                    CAnfnt1 = CAc1 * 0.261;
                    CAnfnt2 = CAc2 * 0.654;
                    CAnfnt3 = CAc3 * 0.065;
                    CAnfnt4 = CAc4 * 0.02;

                    CAInjTotal = CAnfnt1 + CAnfnt2 + CAnfnt3 + CAnfnt4;

                    if (CAInjTotal <= 100)
                    {
                        CmdCate = "A";
                    }
                    else if (CAInjTotal > 100 && CAInjTotal <= 1000)
                    {
                        CmdCate = "B";
                    }
                    else if (CAInjTotal > 1000 && CAInjTotal <= 3000)
                    {
                        CmdCate = "C";
                    }
                    else if (CAInjTotal > 3000 && CAInjTotal <= 10000)
                    {
                        CmdCate = "D";
                    }
                    else if (CAInjTotal > 10000)
                    {
                        CmdCate = "E";
                    }

                    txt_COF_NonPersonnel.Text = CAInjTotal.ToString();
                    txt_COF_NonCategory.Text = CmdCate.ToString();


                }
                if (btn_COF_NonCalc.Text == " Calculate ")
                {
                    btn_COF_NonCalc.Text = "  Save   ";
                    btn_COF_NonCalc.BackColor = Color.Maroon;
                    btn_COF_NonCalc.ForeColor = Color.White;
                }
                else
                {
                    btn_COF_NonCalc.Text = " Calculate ";
                    btn_COF_NonCalc.BackColor = Color.FromArgb(240, 240, 240);
                    btn_COF_NonCalc.ForeColor = Color.Black;
                    string strqrydup = "Select  *  from COF_NonFlammable  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' and  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' and CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0"; 
                    SqlCommand cmddup = new SqlCommand(strqrydup, conn);
                    SqlDataReader readerdup = cmddup.ExecuteReader();
                    if (readerdup.Read())
                    {
                        readerdup.Close();
                        string sql5 = "update COF_NonFlammable set Fluid='" + cbo_COF_NonFluid.SelectedItem.Text.ToString() + "',Repfluid='" + cbo_COF_NonRefFluid.SelectedItem.Text.ToString() + "',Type='" + txt_COF_Non_FluidType.Text.ToString() + "',OpPres='" + txt_COF_NonOpPressure.Text.ToString() + "',Optemp='" + txt_COF_NonOpTemp.Text.ToString() + "',P1='" + txt_COF_NonP1.Text.ToString() + "',Mass='" + txt_COF_NonMass.Text.ToString() + "',Detect='" + cbo_COF_NonDetect.SelectedItem.Text.ToString() + "',Iso='" + cbo_COF_NonIso.SelectedItem.Text.ToString() + "',factMit='" + cbo_COF_NonMiti.SelectedValue.ToString() + "',w1='" + w1.ToString("#.##") + "',w2='" + w2.ToString("#.##") + "',w3='" + w3.ToString("#.##") + "',w4='" + w4.ToString("#.##") + "',Time1='" + Timet1.ToString() + "',Time2='" + Timet2.ToString() + "',Time3='" + Timet3.ToString() + "',Time4='" + Timet4.ToString() + "',T1= '" + t1.ToString("#.##") + "',T2= '" + t2.ToString("#.##") + "',T3= '" + t3.ToString("#.##") + "',T4= '" + t4.ToString("#.##") + "',rate1='" + rate1.ToString("#.##") + "',rate2='" + rate2.ToString("#.##") + "',rate3='" + rate3.ToString("#.##") + "',rate4='" + rate4.ToString("#.##") + "',id1='" + useld1.ToString() + "',id2='" + useld2.ToString() + "',id3='" + useld3.ToString() + "',id4='" + useld4.ToString() + "',mass1='" + mass1.ToString("#.##") + "',mass2='" + mass2.ToString("#.##") + "',mass3='" + mass3.ToString("#.##") + "',mass4='" + mass4.ToString("#.##") + "',eneff='" + eneffn.ToString("#.##") + "',CAInj1='" + CAInj1.ToString("#.##") + "',CAInj2='" + CAInj2.ToString("#.##") + "',CAInj3='" + CAInj3.ToString("#.##") + "',CAInj4='" + CAInj4.ToString("#.##") + "',CAInsInj1='" + CAInjInst1.ToString("#.##") + "',CAInsInj2='" + CAInjInst2.ToString("#.##") + "',CAInsInj3='" + CAInjInst3.ToString("#.##") + "',CAInsInj4='" + CAInjInst4.ToString("#.##") + "',factic1='" + factic1.ToString("#.##") + "',factic2='" + factic2.ToString("#.##") + "',factic3='" + factic3.ToString("#.##") + "',factic4='" + factic4.ToString("#.##") + "',CAbleInj1= '" + CAc1.ToString("#.##") + "',CAbleInj2= '" + CAc2.ToString("#.##") + "',CAbleInj3= '" + CAc3.ToString("#.##") + "',CAbleInj4= '" + CAc4.ToString("#.##") + "',CAInjFinal1= '" + CAnfnt1.ToString("#.##") + "',CAInjFinal2= '" + CAnfnt2.ToString("#.##") + "',CAInjFinal3= '" + CAnfnt3.ToString("#.##") + "',CAInjFinal4= '" + CAnfnt4.ToString("#.##") + "',CAInjTotal='" + CAInjTotal.ToString("#.##") + "',CAinjCate='" + CmdCate.ToString() + "',CreatedBy=" + Convert.ToInt32(Session["sesUserID"].ToString()) + ",ptrans='" + ptrans.ToString() + "',FluidStored='" + txt_Non_FluidStored.Text.ToString() + "',g='" + Convert.ToDouble(g.ToString()) + "',h='" + Convert.ToDouble(h.ToString()) + "' where ProcID=" + cboProcessArea.SelectedValue.ToString() + " and EquID=" + cboEquipment.SelectedValue.ToString() + " and CompID=" + cboComponent.SelectedValue.ToString() + "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       
                        SqlCommand cmd5 = new SqlCommand(sql5, conn);
                        cmd5.ExecuteNonQuery();
                        lblStatus.Text = "Successfully Value Updated";
                        lblStatus.ForeColor = Color.Blue;
                    }
                    else
                    {
                        readerdup.Close();

                        string sql6 = "insert into COF_NonFlammable(ProcID,EquID,CompID,Fluid,Repfluid,Type,OpPres,Optemp,P1,Mass,Detect,Iso,factMit,w1,w2,w3,w4,Time1,Time2,Time3,Time4,T1,T2,T3,T4,rate1,rate2,rate3,rate4,id1,id2,id3,id4,mass1,mass2,mass3,mass4,eneff,CAInj1,CAInj2,CAInj3,CAInj4,CAInsInj1,CAInsInj2,CAInsInj3,CAInsInj4,factic1,factic2,factic3,factic4,CAbleInj1,CAbleInj2,CAbleInj3,CAbleInj4,CAInjFinal1,CAInjFinal2,CAInjFinal3,CAInjFinal4,CAInjTotal,CAinjCate,CreatedBy,ptrans,FluidStored,g,h)   values   (" + cboProcessArea.SelectedValue.ToString() + "," + cboEquipment.SelectedValue.ToString() + "," + cboComponent.SelectedValue.ToString() + ",'" + cbo_COF_NonFluid.SelectedItem.Text.ToString() + "','" + cbo_COF_NonRefFluid.SelectedItem.Text.ToString() + "','" + txt_COF_Non_FluidType.Text.ToString() + "','" + txt_COF_NonOpPressure.Text.ToString() + "','" + txt_COF_NonOpTemp.Text.ToString() + "','" + txt_COF_NonP1.Text.ToString() + "','" + txt_COF_NonMass.Text.ToString() + "','" + cbo_COF_NonDetect.SelectedItem.Text.ToString() + "','" + cbo_COF_NonIso.SelectedItem.Text.ToString() + "','" + cbo_COF_NonMiti.SelectedValue.ToString() + "','" + w1.ToString("#.##") + "','" + w2.ToString("#.##") + "','" + w3.ToString("#.##") + "','" + w4.ToString("#.##") + "','" + Timet1.ToString() + "','" + Timet2.ToString() + "','" + Timet3.ToString() + "','" + Timet4.ToString() + "','" + t1.ToString("#.##") + "','" + t2.ToString("#.##") + "','" + t3.ToString("#.##") + "','" + t4.ToString("#.##") + "','" + rate1.ToString("#.##") + "','" + rate2.ToString("#.##") + "','" + rate3.ToString("#.##") + "','" + rate4.ToString("#.##") + "','" + useld1.ToString() + "','" + useld2.ToString() + "','" + useld3.ToString() + "','" + useld4.ToString() + "','" + mass1.ToString("#.##") + "','" + mass2.ToString("#.##") + "','" + mass3.ToString("#.##") + "','" + mass4.ToString("#.##") + "','" + eneffn.ToString("#.##") + "','" + CAInj1.ToString("#.##") + "','" + CAInj2.ToString("#.##") + "','" + CAInj3.ToString("#.##") + "','" + CAInj4.ToString("#.##") + "','" + CAInjInst1.ToString("#.##") + "','" + CAInjInst2.ToString("#.##") + "','" + CAInjInst3.ToString("#.##") + "','" + CAInjInst4.ToString("#.##") + "','" + factic1.ToString("#.##") + "','" + factic2.ToString("#.##") + "','" + factic3.ToString("#.##") + "','" + factic4.ToString("#.##") + "','" + CAc1.ToString("#.##") + "','" + CAc2.ToString("#.##") + "','" + CAc3.ToString("#.##") + "','" + CAc4.ToString("#.##") + "','" + CAnfnt1.ToString("#.##") + "','" + CAnfnt2.ToString("#.##") + "','" + CAnfnt3.ToString("#.##") + "','" + CAnfnt4.ToString("#.##") + "','" + CAInjTotal.ToString("#.##") + "','" + CmdCate.ToString() + "'," + Convert.ToInt32(Session["sesUserID"].ToString()) + ",'" + ptrans.ToString() + "','" + txt_Non_FluidStored.Text.ToString() + "','" + Convert.ToDouble(g.ToString()) + "','" + Convert.ToDouble(h.ToString()) + "')";
                        SqlCommand cmd6 = new SqlCommand(sql6, conn);
                        cmd6.ExecuteNonQuery();
                        lblStatus.Text = "Successfully Value Inserted";
                        lblStatus.ForeColor = Color.Blue;
                    }
                   
                }
                BusinessTier.DisposeConnection(conn);
            }
            catch (Exception ex)
            {
               // lblStatus.Text = "Err: " + ex.Message.ToString();
            }
        }
    }

    protected void btn_COF_NonCancel_Click(object sender, EventArgs e)
    {
        btn_COF_NonCalc.Text = " Calculate ";
        btn_COF_NonCalc.BackColor = Color.FromArgb(240, 240, 240);
        btn_COF_NonCalc.ForeColor = Color.Black;
    }

    protected void btnCOFNonFlameSubmit_Click(object sender, EventArgs e)
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
            string strqrydup = "Select  *  from COF_NonFlammable  where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' and  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' and CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            SqlCommand cmddup = new SqlCommand(strqrydup, conn);
            SqlDataReader readerdup = cmddup.ExecuteReader();
            if (readerdup.Read())
            {

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('ReportsCOFNonFlame.aspx?param1=" + cboProcessArea.SelectedValue.ToString().Trim() + "&param2=" + cboEquipment.SelectedValue.ToString().Trim() + "&param3=" + cboComponent.SelectedValue.ToString().Trim() + "');", true);
                btnCOFNonFlameDelete.Enabled = true;
            }
            else
            {
                lblStatus.Text = "This Component don't have value you can enter the new data";
                lblStatus.ForeColor = Color.Blue;
            }
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "COF", "btnCOFNonFlameSubmit_Click", ex.ToString(), "Audit");
            lblStatus.Text = ex.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    protected void btnCOFNonFlameDelete_Click(object sender, EventArgs e)
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
          //  string strqrydup = "update COF_NonFlammable set Deleted=1 where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' and  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' and CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and Deleted=0";
            string strqrydup = "delete from COF_NonFlammable where ProcID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' and  EquID='" + cboEquipment.SelectedValue.ToString().Trim() + "' and CompID ='" + cboComponent.SelectedValue.ToString().Trim() + "' and Deleted=0";
            SqlCommand cmddup = new SqlCommand(strqrydup, conn);
            cmddup.ExecuteNonQuery();
            lblStatus.Text = "COF NonFlammable Value Successfully Deleted";
            lblStatus.ForeColor = Color.Maroon;

        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "COF", "btnCOFFlameSubmit_Click", ex.ToString(), "Audit");
            lblStatus.Text = ex.ToString();
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
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


