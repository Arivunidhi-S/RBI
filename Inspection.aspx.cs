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

public partial class Inspection : System.Web.UI.Page
{
    public DataTable dtMenuItems = new DataTable();

    public DataTable dtSubMenuItems = new DataTable();

    string flag = string.Empty;

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
        btnlastinspection.Enabled = false;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            details();
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

    protected void OnSelectedIndexChanged_cboProcess(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {

        RadComboBox combobox = (RadComboBox)sender;

        cboEquipment.Items.Clear();
        cboComponent.Items.Clear();
        cboEquipment.Text = string.Empty;
        cboComponent.Text = string.Empty;
        lblsCR.Text = string.Empty;
        lbllCR.Text = string.Empty;
        lbluCR.Text = string.Empty;
        lblCurThick.Text = string.Empty;
        lblDefaultcr.Text = string.Empty;
        btnlastinspection.Enabled = false;

        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            if (cboProcessArea.Text.ToString() != "")
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
                string ProType = cboProcessArea.SelectedValue.ToString();
                RadGrid1.DataSource = DataSourceHelper(ProType, "0", "0");
                RadGrid1.Rebind();
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
        lblsCR.Text = string.Empty;
        lbllCR.Text = string.Empty;
        lbluCR.Text = string.Empty;
        lblCurThick.Text = string.Empty;
        lblDefaultcr.Text = string.Empty;
        btnlastinspection.Enabled = false;

        RadComboBox combobox = (RadComboBox)sender;
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            if (cboEquipment.Text.ToString() != "")
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
                string ProType = cboProcessArea.SelectedValue.ToString();
                string EquipType = cboEquipment.SelectedValue.ToString();
                RadGrid1.DataSource = DataSourceHelper(ProType, EquipType, "0");
                RadGrid1.Rebind();
                BusinessTier.DisposeAdapter(adapter1);
                BusinessTier.DisposeConnection(conn);
            }

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
        lblsCR.Text = string.Empty;
        lbllCR.Text = string.Empty;
        lbluCR.Text = string.Empty;
        lblCurThick.Text = string.Empty;
        lblDefaultcr.Text = string.Empty;
        btnlastinspection.Enabled = true;
        btnlastinspection.Text = "  Last Inspection Value  ";
        if (string.IsNullOrEmpty(cboEquipment.Text.ToString().Trim()))
        {
            RadGrid1.DataSource = DataSourceHelper("0", "0", "0");
            RadGrid1.Rebind();
        }
        else
        {
            string ProType = cboProcessArea.SelectedValue.ToString();
            string EquipType = cboEquipment.SelectedValue.ToString();
            string CompNo = cboComponent.SelectedValue.ToString();
            RadGrid1.DataSource = DataSourceHelper(ProType, EquipType, CompNo);
            RadGrid1.Rebind();
        }

        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            //double dfv = 0.000;
            string strqry1 = "SELECT ShortCRrate,LongCRrate,uCR,Defaultvalue,Remaininglife from Tbl_EquipmentComponentDetails WHERE ProcessAreaID='" + cboProcessArea.SelectedValue.ToString() + "' and EqupID='" + cboEquipment.SelectedValue.ToString() + "' and CompAutoID='" + cboComponent.SelectedValue.ToString() + "' and deleted=0";
            SqlCommand cmd1 = new SqlCommand(strqry1, conn);
            SqlDataReader rdr1 = cmd1.ExecuteReader();
            if (rdr1.Read())
            {
                lblsCR.Text = rdr1["ShortCRrate"].ToString();
                lbllCR.Text = rdr1["LongCRrate"].ToString();
                lbluCR.Text = rdr1["uCR"].ToString();
                lblDefaultcr.Text = rdr1["Defaultvalue"].ToString();
                lblRemainLife.Text = rdr1["Remaininglife"].ToString();
            }
            if (string.IsNullOrEmpty(lblsCR.Text))
            {
                lblsCR.Text = "0";
            }
            if (string.IsNullOrEmpty(lbllCR.Text))
            {
                lbllCR.Text = "0";
            }
            rdr1.Close();
            string strqry = "SELECT min(ReadingValue) as readval from Tbl_InspectionDataDetails WHERE EqupID='" + cboEquipment.SelectedValue.ToString() + "' and ComponentNo='" + cboComponent.SelectedValue.ToString() + "' and DefaultsCR='" + lblsCR.Text.ToString() + "' and DefaultlCR='" + lbllCR.Text.ToString() + "'  and deleted=0";
            SqlCommand cmd = new SqlCommand(strqry, conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {

                if (string.IsNullOrEmpty(rdr["readval"].ToString()))
                {
                    rdr.Close();
                    string strqry11 = "SELECT min(ReadingValue) as readval11 from Tbl_InspectionDataDetails WHERE EqupID='" + cboEquipment.SelectedValue.ToString() + "' and ComponentNo='" + cboComponent.SelectedValue.ToString() + "' and uCR='" + lbluCR.Text.ToString() + "' and deleted=0";
                    SqlCommand cmd11 = new SqlCommand(strqry11, conn);
                    SqlDataReader rdr11 = cmd11.ExecuteReader();
                    if (rdr11.Read())
                    {
                        lblCurThick.Text = rdr11["readval11"].ToString();
                        rdr11.Close();
                    }

                }
                else
                {
                    lblCurThick.Text = rdr["readval"].ToString();
                    rdr.Close();

                }
            }
            else
            {
                rdr.Close();
            }
            //if (rdr1.Read())
            //{
            //    lblsCR.Text = rdr1["ShortCRrate"].ToString();
            //    lbllCR.Text = rdr1["LongCRrate"].ToString();
            //    lbluCR.Text = rdr1["uCR"].ToString();
            //    lblDefaultcr.Text = rdr1["Defaultvalue"].ToString();
            //    if (string.IsNullOrEmpty(rdr1["Defaultvalue"].ToString()))
            //    {
            //        dfv = 0.000;
            //    }
            //    else
            //    {
            //        dfv = Convert.ToDouble(rdr1["Defaultvalue"].ToString());
            //    }
            //}

            //rdr1.Close();

            //if (string.IsNullOrEmpty(lblsCR.Text))
            //{
            //    lbllCR.Text = "0";
            //}
            //if (dfv == Convert.ToDouble(lbllCR.Text.ToString()))
            //{
            //    string strqry = "SELECT ReadingValue from Tbl_InspectionDataDetails WHERE LongCRrate=(SELECT max(LongCRrate) from Tbl_InspectionDataDetails WHERE EqupID='" + cboEquipment.SelectedValue.ToString() + "' and ComponentNo='" + cboComponent.SelectedValue.ToString() + "' and DefaultlCR='" + lbllCR.Text.ToString() + "'  and deleted=0)  and deleted=0 and  EqupID='" + cboEquipment.SelectedValue.ToString() + "' and ComponentNo='" + cboComponent.SelectedValue.ToString() + "'";
            //    SqlCommand cmd = new SqlCommand(strqry, conn);
            //    SqlDataReader rdr = cmd.ExecuteReader();
            //    if (rdr.Read())
            //    {

            //        lblCurThick.Text = rdr["ReadingValue"].ToString();
            //    }

            //    rdr.Close();

            //}
            //else
            //{

            //    string strqry = "SELECT min(ReadingValue) as readval from Tbl_InspectionDataDetails WHERE EqupID='" + cboEquipment.SelectedValue.ToString() + "' and ComponentNo='" + cboComponent.SelectedValue.ToString() + "' and DefaultlCR='" + lbllCR.Text.ToString() + "'  and deleted=0";
            //    SqlCommand cmd = new SqlCommand(strqry, conn);
            //    SqlDataReader rdr = cmd.ExecuteReader();
            //    if (rdr.Read())
            //    {
            //        lblCurThick.Text = rdr["readval"].ToString();
            //    }

            //    rdr.Close();
            //}
        }

        catch (Exception ex)
        {
            BusinessTier.DisposeConnection(conn);
            // lblStatus.Text = "Err:Select correct Product Item" + ex.Message.ToString();
            return;
        }



    }

    protected void Onclick_btnRefresh(object sender, EventArgs e)
    {
        lblsCR.Text = string.Empty;
        lbllCR.Text = string.Empty;
        lbluCR.Text = string.Empty;
        lblCurThick.Text = string.Empty;
        lblDefaultcr.Text = string.Empty;
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            //double dfv = 0.000;
            string strqry1 = "SELECT ShortCRrate,LongCRrate,uCR,Defaultvalue,Remaininglife from Tbl_EquipmentComponentDetails WHERE ProcessAreaID='" + cboProcessArea.SelectedValue.ToString() + "' and EqupID='" + cboEquipment.SelectedValue.ToString() + "' and CompAutoID='" + cboComponent.SelectedValue.ToString() + "' and deleted=0";
            SqlCommand cmd1 = new SqlCommand(strqry1, conn);
            SqlDataReader rdr1 = cmd1.ExecuteReader();
            if (rdr1.Read())
            {
                lblsCR.Text = rdr1["ShortCRrate"].ToString();
                lbllCR.Text = rdr1["LongCRrate"].ToString();
                lbluCR.Text = rdr1["uCR"].ToString();
                lblDefaultcr.Text = rdr1["Defaultvalue"].ToString();
                lblRemainLife.Text = rdr1["Remaininglife"].ToString();
            }
            if (string.IsNullOrEmpty(lblsCR.Text))
            {
                lblsCR.Text = "0";
            }
            if (string.IsNullOrEmpty(lbllCR.Text))
            {
                lbllCR.Text = "0";
            }
            rdr1.Close();
            string strqry = "SELECT min(ReadingValue) as readval from Tbl_InspectionDataDetails WHERE EqupID='" + cboEquipment.SelectedValue.ToString() + "' and ComponentNo='" + cboComponent.SelectedValue.ToString() + "' and DefaultsCR='" + lblsCR.Text.ToString() + "' and DefaultlCR='" + lbllCR.Text.ToString() + "'  and deleted=0";
            SqlCommand cmd = new SqlCommand(strqry, conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {

                if (string.IsNullOrEmpty(rdr["readval"].ToString()))
                {
                    rdr.Close();
                    string strqry11 = "SELECT min(ReadingValue) as readval11 from Tbl_InspectionDataDetails WHERE EqupID='" + cboEquipment.SelectedValue.ToString() + "' and ComponentNo='" + cboComponent.SelectedValue.ToString() + "' and uCR='" + lbluCR.Text.ToString() + "' and deleted=0";
                    SqlCommand cmd11 = new SqlCommand(strqry11, conn);
                    SqlDataReader rdr11 = cmd11.ExecuteReader();
                    if (rdr11.Read())
                    {
                        lblCurThick.Text = rdr11["readval11"].ToString();
                        rdr11.Close();
                    }

                }
                else
                {
                    lblCurThick.Text = rdr["readval"].ToString();
                    rdr.Close();

                }
            }
            else
            {
                rdr.Close();
            }
            //if (rdr1.Read())
            //{
            //    lblsCR.Text = rdr1["ShortCRrate"].ToString();
            //    lbllCR.Text = rdr1["LongCRrate"].ToString();
            //    lbluCR.Text = rdr1["uCR"].ToString();
            //    lblDefaultcr.Text = rdr1["Defaultvalue"].ToString();
            //    if (string.IsNullOrEmpty(rdr1["Defaultvalue"].ToString()))
            //    {
            //        dfv = 0.000;
            //    }
            //    else
            //    {
            //        dfv =Convert.ToDouble( rdr1["Defaultvalue"].ToString());
            //    }
            //}

            //rdr1.Close();

            //if (string.IsNullOrEmpty(lblsCR.Text))
            //{
            //    lbllCR.Text = "0";
            //}
            //if (dfv == Convert.ToDouble(lbllCR.Text.ToString()))
            //{
            //    string strqry = "SELECT ReadingValue from Tbl_InspectionDataDetails WHERE LongCRrate=(SELECT max(LongCRrate) from Tbl_InspectionDataDetails WHERE EqupID='" + cboEquipment.SelectedValue.ToString() + "' and ComponentNo='" + cboComponent.SelectedValue.ToString() + "' and DefaultlCR='" + lbllCR.Text.ToString() + "'  and deleted=0) and deleted=0 and  EqupID='" + cboEquipment.SelectedValue.ToString() + "' and ComponentNo='" + cboComponent.SelectedValue.ToString() + "'";
            //    SqlCommand cmd = new SqlCommand(strqry, conn);
            //    SqlDataReader rdr = cmd.ExecuteReader();
            //    if (rdr.Read())
            //    {

            //        lblCurThick.Text = rdr["ReadingValue"].ToString();
            //    }

            //    rdr.Close();

            //}
            //else
            //{

            //    string strqry = "SELECT min(ReadingValue) as readval from Tbl_InspectionDataDetails WHERE EqupID='" + cboEquipment.SelectedValue.ToString() + "' and ComponentNo='" + cboComponent.SelectedValue.ToString() + "' and DefaultlCR='" + lbllCR.Text.ToString() + "'  and deleted=0";
            //    SqlCommand cmd = new SqlCommand(strqry, conn);
            //    SqlDataReader rdr = cmd.ExecuteReader();
            //    if (rdr.Read())
            //    {
            //        lblCurThick.Text = rdr["readval"].ToString();
            //    }

            //    rdr.Close();
            //}
        }

        catch (Exception ex)
        {
            BusinessTier.DisposeConnection(conn);
            // lblStatus.Text = "Err:Select correct Product Item" + ex.Message.ToString();
            return;
        }

    }

    protected void Onclick_btnlastinspection(object sender, EventArgs e)
    {
        if (btnlastinspection.Text == "  Last Inspection Value  ")
        {
            flag = "A";
            btnlastinspection.Text = "  View All Inspection  ";
            RadGrid1.DataSource = DataSourceHelper("A", "A", "A");
            RadGrid1.Rebind();
        }
        else
        {
            flag = "B";
            btnlastinspection.Text = "  Last Inspection Value  ";
            RadGrid1.DataSource = DataSourceHelper("A", "A", "B");
            RadGrid1.Rebind();
        }

    }

    protected void Onclick_btnupdateall(object sender, EventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();

        string sqlqry = "Select  EqupID,componentno,InspectionPointNo from Tbl_InspectionDataDetails where Deleted=0 group by EqupID,componentno,InspectionPointNo";
        SqlDataAdapter sqladp = new SqlDataAdapter(sqlqry, conn);
        DataTable dtt = new DataTable();
        sqladp.Fill(dtt);
        int j = 0,f=0;
        f = dtt.Rows.Count;
        foreach (DataRow row1 in dtt.Rows)
        {
            int EqupID = Convert.ToInt32(dtt.Rows[j]["EqupID"].ToString());
            int componentno = Convert.ToInt32(dtt.Rows[j]["componentno"].ToString());
            string InspectionPointNo = dtt.Rows[j]["InspectionPointNo"].ToString();

            double NT = 0.000, MRT = 0.000, Dfvalue = 0.000;

            string sql = "Select  normalthickness,MRT,defaultvalue from VW_Componentview  where EqupID='" + EqupID + "' and CompAutoid ='" + componentno + "' and  Deleted=0";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                NT = Convert.ToDouble(rd["normalthickness"].ToString());
                MRT = Convert.ToDouble(rd["MRT"].ToString());
                if (string.IsNullOrEmpty(rd["defaultvalue"].ToString()))
                {
                    Dfvalue = 0.010;
                }
                else
                {
                    Dfvalue = Convert.ToDouble(rd["defaultvalue"].ToString());
                }
            }
            else
            {
                lblStatus.Text = "Please Check the Normal Thickness,MRT,Default CR Rate Values in Component Form";
            }
            BusinessTier.DisposeReader(rd);

            string sql1 = "Select  [InspecDate],[ReadingValue],[Previousdate],[Previousvalue],[Initialdate],[Initialvalue] from Tbl_InspectionDataDetails  where EqupID='" + EqupID + "' and componentno ='" + componentno + "' and InspectionPointNo ='" + InspectionPointNo.ToString() + "'  and  Deleted=0 order by InspecDate";
            SqlDataAdapter adp = new SqlDataAdapter(sql1, conn);
            DataTable dataTable1 = new DataTable();
            adp.Fill(dataTable1);
            // int inspec = Convert.ToInt32(dataTable1.Rows.Count);
            double Initvalue = Convert.ToDouble(dataTable1.Rows[0]["ReadingValue"].ToString());
            DateTime Initialdt = Convert.ToDateTime(dataTable1.Rows[0]["InspecDate"].ToString());
            DateTime Previousdt = new DateTime();
            double Prevalue = 0.000;
            int i = 0;
            foreach (DataRow row in dataTable1.Rows)
            {

                double TInitvalue = Convert.ToDouble(dataTable1.Rows[i]["ReadingValue"].ToString());
                if (Previousdt.ToString() != "01/01/0001 00:00:00")
                {
                    String prdt1 = Previousdt.ToString();
                    DateTime pdt1 = DateTime.Parse(prdt1);
                    prdt1 = pdt1.Month + "/" + pdt1.Day + "/" + pdt1.Year + " 00:00:00";
                    string sql7 = "Update Tbl_InspectionDataDetails set ShortCRrate=null,LongCRrate=null,DefaultsCR=null,DefaultlCR=null,uCR=null where EqupID='" + EqupID + "' and componentno ='" + componentno + "' and InspectionPointNo ='" + InspectionPointNo.ToString() + "' and InspecDate='" + prdt1.ToString() + "' and  Deleted=0";
                    SqlCommand cmd7 = new SqlCommand(sql7, conn);
                    cmd7.ExecuteNonQuery();
                }

                DateTime inspectdt = Convert.ToDateTime(row["InspecDate"].ToString());


                //Previousdt.datet
                double inspectvalue = Convert.ToDouble(row["ReadingValue"].ToString());

                // Short Corrosion Rate
                double sCR = 0.000, dsCR = 0.000;
                if (inspectdt == Initialdt)
                {
                    sCR = 0;
                }
                else
                {
                    double dt1 = inspectdt.Subtract(Previousdt).Days;
                    double dt = dt1 / 365;
                    if (dt == 0)
                        sCR = 0;
                    else
                        sCR = Math.Round((Prevalue - inspectvalue) / dt, 3);

                    if (sCR <= 0)
                        dsCR = Dfvalue;
                    else
                        dsCR = sCR;
                }
                // Long Corrosion Rate
                double lCR = 0.000, dlCR = 0.000;
                if (inspectdt == Initialdt)
                {
                    lCR = 0;
                }
                else
                {
                    double dt1 = inspectdt.Subtract(Initialdt).Days;
                    double dt = dt1 / 365;
                    if (dt == 0)
                        lCR = 0;
                    else
                        lCR = Math.Round((Initvalue - inspectvalue) / dt, 3);

                    if (lCR <= 0)
                        dlCR = Dfvalue;
                    else
                        dlCR = lCR;
                }


                // Remaing Life
                double RemainLife = 0.000, maxdCR = 0.000;
                maxdCR = Math.Max(dsCR, dlCR);


                RemainLife = Math.Round((TInitvalue - MRT) / maxdCR, 0);

                String prdt = Previousdt.ToString();
                DateTime pdt = DateTime.Parse(prdt);
                prdt = pdt.Month + "/" + pdt.Day + "/" + pdt.Year + " 00:00:00";

                String Initdt = Initialdt.ToString();
                DateTime idt = DateTime.Parse(Initdt);
                Initdt = idt.Month + "/" + idt.Day + "/" + idt.Year + " 00:00:00";

                String insdt = inspectdt.ToString();
                DateTime isdt = DateTime.Parse(insdt);
                insdt = isdt.Month + "/" + isdt.Day + "/" + isdt.Year + " 00:00:00";
                string sql5 = string.Empty;
                if (inspectdt == Initialdt)
                {
                    sql5 = "Update Tbl_InspectionDataDetails set [Previousdate]=null,[Previousvalue]=null,[Initialdate]=null,[Initialvalue]=null,ShortCRrate=null,LongCRrate=null,DefaultsCR=null,DefaultlCR=null,uCR=null,SCR=null,LCR=null,DSCR=null,DLCR=null where EqupID='" + EqupID + "' and componentno ='" + componentno + "' and InspectionPointNo ='" + InspectionPointNo.ToString() + "' and InspecDate='" + insdt.ToString() + "' and  Deleted=0";
                }
                else
                {
                    sql5 = "Update Tbl_InspectionDataDetails set [Previousdate]='" + prdt.ToString() + "',[Previousvalue]='" + Prevalue + "',[Initialdate]='" + Initdt.ToString() + "',[Initialvalue]='" + Initvalue + "'  ,ShortCRrate='" + sCR + "',LongCRrate='" + lCR + "',RemainingLife='" + RemainLife + "',DefaultsCR='" + dsCR + "',DefaultlCR='" + dlCR + "',SCR='" + sCR + "',LCR='" + lCR + "',DSCR='" + dsCR + "',DLCR='" + dlCR + "',uCR='" + maxdCR + "' where EqupID='" + EqupID + "' and componentno ='" + componentno + "' and InspectionPointNo ='" + InspectionPointNo.ToString() + "' and InspecDate='" + insdt.ToString() + "' and  Deleted=0";
                }

                SqlCommand cmd5 = new SqlCommand(sql5, conn);
                cmd5.ExecuteNonQuery();
                Previousdt = inspectdt;
                Prevalue = inspectvalue;
                i = i + 1;
            }


            double maxsCR = 0.000, maxlCR = 0.000;
            string sql4 = "Select max(DefaultsCR) as sc,max(DefaultlCR) as lc from Tbl_InspectionDataDetails where EqupID='" + EqupID + "' and componentno ='" + componentno + "' and  Deleted=0";
            SqlCommand cmd4 = new SqlCommand(sql4, conn);
            SqlDataReader rd4 = cmd4.ExecuteReader();
            if (rd4.Read())
            {
                if (string.IsNullOrEmpty(rd4["sc"].ToString()))
                {
                    BusinessTier.DisposeReader(rd4);
                    BusinessTier.DisposeConnection(conn);
                    ShowMessage(84);
                    return;
                }
                maxsCR = Convert.ToDouble(rd4["sc"].ToString());
                maxlCR = Convert.ToDouble(rd4["lc"].ToString());
            }


            BusinessTier.DisposeReader(rd4);

            double maxuCR = 0.000, RemainingLife = 0.000, redval = 0.000;
            maxuCR = Math.Max(maxsCR, maxlCR);

            string strqry = "SELECT min(ReadingValue) as readval from Tbl_InspectionDataDetails WHERE EqupID='" + EqupID + "' and ComponentNo='" + componentno + "' and DefaultsCR='" + maxsCR.ToString() + "' and DefaultlCR='" + maxlCR.ToString() + "'  and deleted=0";
            SqlCommand cmdrd = new SqlCommand(strqry, conn);
            SqlDataReader rdr = cmdrd.ExecuteReader();
            if (rdr.Read())
            {

                if (string.IsNullOrEmpty(rdr["readval"].ToString()))
                {
                    rdr.Close();
                    string strqry11 = "SELECT min(ReadingValue) as readval11 from Tbl_InspectionDataDetails WHERE EqupID='" + EqupID + "' and ComponentNo='" + componentno + "' and uCR='" + maxuCR.ToString() + "' and deleted=0";
                    SqlCommand cmd11 = new SqlCommand(strqry11, conn);
                    SqlDataReader rdr11 = cmd11.ExecuteReader();
                    if (rdr11.Read())
                    {
                        redval = Convert.ToDouble(rdr11["readval11"].ToString());
                        rdr11.Close();
                    }

                }
                else
                {
                    redval = Convert.ToDouble(rdr["readval"].ToString());
                    rdr.Close();

                }
            }
            else
            {
                rdr.Close();
            }


            RemainingLife = Math.Round((redval - MRT) / maxuCR, 0);

            int inspec = 0;
            string sql6 = "Select count(*) as Countvalue from Tbl_InspectionDataDetails  where EqupID='" + EqupID + "' and componentno ='" + componentno + "' and InspectionPointNo='" + InspectionPointNo.ToString() + "'  and  Deleted=0";
            SqlCommand cmd6 = new SqlCommand(sql6, conn);
            SqlDataReader rd6 = cmd6.ExecuteReader();
            if (rd6.Read())
            {
                inspec = Convert.ToInt32(rd6["Countvalue"].ToString());
            }
            BusinessTier.DisposeReader(rd6);

            string sql3 = " update Tbl_EquipmentComponentDetails set ShortCRrate='" + maxsCR + "',LongCRrate='" + maxlCR + "',RemainingLife='" + RemainingLife + "',uCR='" + maxuCR + "', NoofInspection='" + inspec + "',ReadVal='" + redval + "' where EqupID = '" + EqupID + "' and Compautoid='" + componentno + "' and Deleted=0";
            SqlCommand cmd3 = new SqlCommand(sql3, conn);
            cmd3.ExecuteNonQuery();
            j = j + 1;
        }
        ShowMessage(84);
        BusinessTier.DisposeConnection(conn);
    }

    public void details()
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        string strdet = "SELECT Equptype,compname,ShortCRrate,LongCRrate,RemainingLife from VW_Componentview where deleted=0"; // and equpid = '" + cboeqid.SelectedValue.ToString() + "' and compautoid='" + cbocompID.SelectedValue.ToString() + "' ";

        SqlCommand cmddet = new SqlCommand(strdet, conn);
        SqlDataReader rdrdet = cmddet.ExecuteReader();
        while (rdrdet.Read())
        {

        }
        BusinessTier.DisposeReader(rdrdet);
        BusinessTier.DisposeConnection(conn);

    }

    //============================================================================================================

    protected void RadGrid1_ItemDataBound(object source, Telerik.Web.UI.GridItemEventArgs e)
    {

        try
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditFormItem editedItem = e.Item as GridEditFormItem;
                Label lblID = (Label)editedItem.FindControl("lblID");
                RadComboBox cboeqid = (RadComboBox)editedItem.FindControl("cboeqid");
                RadComboBox cbocompID = (RadComboBox)editedItem.FindControl("cbocompID");
                Label lbleqid = (Label)editedItem.FindControl("lbleqid");
                Label lblcompid = (Label)editedItem.FindControl("lblcompid");
                Label lblInspdate = (Label)editedItem.FindControl("lblInspdate");

                RadDatePicker txtInspdate = (RadDatePicker)editedItem.FindControl("txtInspdate");
                RadTextBox txtInspectionPointNo = (RadTextBox)editedItem.FindControl("txtInspectionPointNo");
                RadNumericTextBox txtReadingValue = (RadNumericTextBox)editedItem.FindControl("txtReadingValue");
                //cboeqid.AutoPostBack = true;
                cboeqid.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(OnSelectedIndexChangedInspection);
                //txtInspdate.SelectedDateChanged += new RadDateTimePickerse(OnSelectedIndexChangedInspection);
                btnlastinspection.Text = "  Last Inspection Value  ";
                if (!(string.IsNullOrEmpty(lblID.Text.ToString())))
                {
                    //cboeqid.Enabled = false;
                    //cbocompID.Enabled = false;
                    //txtInspdate.Enabled = false;
                    //txtInspectionPointNo.Enabled = false;
                    SqlConnection conn = BusinessTier.getConnection();
                    conn.Open();
                    string strqry1 = "SELECT EquautoID,CompautoID, EqupID,compno,inspecdate  from VW_Inspection WHERE  inspecautoID = '" + lblID.Text.ToString() + "' and deleted=0";
                    SqlCommand cmd11 = new SqlCommand(strqry1, conn);
                    SqlDataReader rdr11 = cmd11.ExecuteReader();
                    if (rdr11.Read())
                    {
                        cboeqid.SelectedItem.Text = Convert.ToString(rdr11["EqupID"].ToString().Trim());
                        cboeqid.SelectedValue = Convert.ToString(rdr11["EquautoID"].ToString().Trim());
                        cbocompID.SelectedValue = Convert.ToString(rdr11["CompautoID"].ToString().Trim());
                        cboeqid.Enabled = false;
                        cbocompID.Enabled = false;
                        //txtInspdate.Enabled = false;
                        txtInspectionPointNo.Enabled = false;
                        //cbocompID.Text = rdr11["compno"].ToString().Trim();
                        //txtInspdate.SelectedDate = Convert.ToDateTime(rdr11["inspecdate"].ToString().Trim());
                    }
                    BusinessTier.DisposeReader(rdr11);
                    BusinessTier.DisposeConnection(conn);
                }
                else
                {
                    if (cboEquipment.Text != "")
                    {
                        cboeqid.Text = cboEquipment.Text.ToString();
                        cboeqid.SelectedValue = cboEquipment.SelectedValue.ToString();

                    }
                    if (cboComponent.Text != "")
                    {
                        cbocompID.Text = cboComponent.SelectedItem.Attributes["CompName"].ToString();
                        cbocompID.SelectedValue = cboComponent.SelectedValue.ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(8);
            e.Canceled = true;
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "ppeissue", "RadGrid1_ItemDataBound", ex.ToString(), "Audit");
        }
    }

    protected void RadGrid1_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditFormItem editedItem = e.Item as GridEditFormItem;
                RadComboBox cboeqid = (RadComboBox)editedItem.FindControl("cboeqid");
                RadComboBox cbocompID = (RadComboBox)editedItem.FindControl("cbocompID");
                Label lbleqid = (Label)editedItem.FindControl("lbleqid");
                Label lblcompid = (Label)editedItem.FindControl("lblcompid");
                Label lblInspdate = (Label)editedItem.FindControl("lblInspdate");
                RadDatePicker txtInspdate = (RadDatePicker)editedItem.FindControl("txtInspdate");
                RadTextBox txtInspectionPointNo = (RadTextBox)editedItem.FindControl("txtInspectionPointNo");
                RadNumericTextBox txtReadingValue = (RadNumericTextBox)editedItem.FindControl("txtReadingValue");
            }
        }
        catch (Exception ex)
        {
            ShowMessage(8);
            e.Canceled = true;
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Inspection", "RadGrid1_ItemCreated", ex.ToString(), "Audit");
        }

    }

    protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            if (!(string.IsNullOrEmpty(cboEquipment.Text.ToString())) && !(string.IsNullOrEmpty(cboComponent.Text.ToString())) && !(string.IsNullOrEmpty(cboProcessArea.Text.ToString())))
                if (btnlastinspection.Text == "  View All Inspection  ")
                    RadGrid1.DataSource = DataSourceHelper("A", "A", "A");
                else if (btnlastinspection.Text == "  Last Inspection Value  ")
                    RadGrid1.DataSource = DataSourceHelper("A", "A", "B");
                else
                    RadGrid1.DataSource = DataSourceHelper(cboProcessArea.SelectedValue.ToString().Trim(), cboEquipment.SelectedValue.ToString().Trim(), cboComponent.SelectedValue.ToString().Trim());
            else if (!(string.IsNullOrEmpty(cboEquipment.Text.ToString())) && !(string.IsNullOrEmpty(cboComponent.Text.ToString())))
                RadGrid1.DataSource = DataSourceHelper(cboProcessArea.SelectedValue.ToString().Trim(), "0", "0");
            else if (!(string.IsNullOrEmpty(cboComponent.Text.ToString())))
                RadGrid1.DataSource = DataSourceHelper(cboProcessArea.SelectedValue.ToString().Trim(), cboEquipment.SelectedValue.ToString().Trim(), "0");
            else
                RadGrid1.DataSource = DataSourceHelper("0", "0", "0");
            //RadGrid1.DataSource = DataSourceHelper();
        }
        catch (Exception ex)
        {
            ShowMessage(9);
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "ppeissue", "NeedDataSource", ex.ToString(), "Audit");
        }
    }

    public DataTable DataSourceHelper(string ProType, string Equivalue, string Compvalue)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Close();
        string sql = "";
        if (ProType.ToString().Trim() == "0")
            sql = "SELECT *,case when isnull(shortcrrate,0) >= isnull(longcrrate,0) then shortcrrate else longcrrate end as uCr from VW_Inspection where deleted=0 and companyid= '" + Convert.ToInt32(Session["sesCompanyID"].ToString()) + "' order by EquAutoID,InspectionPointNo,InspecDate";
        else if (Equivalue.ToString().Trim() == "0")
            sql = "SELECT *,case when isnull(shortcrrate,0) >= isnull(longcrrate,0) then shortcrrate else longcrrate end as uCr from VW_Inspection where deleted=0 and ProcessareaID='" + Convert.ToInt32(ProType.ToString().Trim()) + "' order by EquAutoID,InspectionPointNo,InspecDate";
        else if (Compvalue.ToString().Trim() == "0")
            sql = "SELECT *,case when isnull(shortcrrate,0) >= isnull(longcrrate,0) then shortcrrate else longcrrate end as uCr from VW_Inspection where deleted=0 and ProcessareaID='" + Convert.ToInt32(ProType.ToString().Trim()) + "' and EquAutoID = '" + Convert.ToInt32(Equivalue.ToString().Trim()) + "' order by EquAutoID,InspectionPointNo,InspecDate";
        else if (Compvalue.ToString().Trim() == "A")
            sql = "SELECT * from VW_Inspection where deleted=0 and ProcessareaID='" + Convert.ToInt32(cboProcessArea.SelectedValue.ToString().Trim()) + "' and EquAutoID = '" + Convert.ToInt32(cboEquipment.SelectedValue.ToString().Trim()) + "' and Compautoid = '" + Convert.ToInt32(cboComponent.SelectedValue.ToString().Trim()) + "' and LongCRrate is not null order by EquAutoID,InspectionPointNo,InspecDate";
        else if (Compvalue.ToString().Trim() == "B")
            sql = "SELECT * from VW_Inspection where deleted=0 and ProcessareaID='" + Convert.ToInt32(cboProcessArea.SelectedValue.ToString().Trim()) + "' and EquAutoID = '" + Convert.ToInt32(cboEquipment.SelectedValue.ToString().Trim()) + "' and Compautoid = '" + Convert.ToInt32(cboComponent.SelectedValue.ToString().Trim()) + "' order by EquAutoID,InspectionPointNo,InspecDate";

        else
            sql = "SELECT *,case when isnull(shortcrrate,0) >= isnull(longcrrate,0) then shortcrrate else longcrrate end as uCr from VW_Inspection where deleted=0 and ProcessareaID='" + Convert.ToInt32(ProType.ToString().Trim()) + "' and EquAutoID = '" + Convert.ToInt32(Equivalue.ToString().Trim()) + "' and Compautoid = '" + Convert.ToInt32(Compvalue.ToString().Trim()) + "' order by EquAutoID,InspectionPointNo,InspecDate";
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, conn);
        DataTable g_datatable = new DataTable();
        sqlDataAdapter.Fill(g_datatable);
        BusinessTier.DisposeAdapter(sqlDataAdapter);
        BusinessTier.DisposeConnection(conn);
        return g_datatable;

    }

    //====================================================================================================

    protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        btnlastinspection.Text = "  Last Inspection Value  ";
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["InspecAutoID"].ToString().Trim();
            string eqID = string.Empty, cmpID = string.Empty, Insno = string.Empty;
            string strqry1 = "SELECT EquAutoID,CompAutoID,InspectionPointNo from VW_Inspection WHERE  inspecautoID = '" + ID.ToString() + "' and deleted=0";
            SqlCommand cmd11 = new SqlCommand(strqry1, conn);
            SqlDataReader rdr11 = cmd11.ExecuteReader();
            if (rdr11.Read())
            {
                eqID = rdr11["EquAutoID"].ToString().Trim();
                cmpID = rdr11["CompAutoID"].ToString().Trim();
                Insno = rdr11["InspectionPointNo"].ToString().Trim();
            }
            BusinessTier.DisposeReader(rdr11);
            int flg = BusinessTier.Del(conn, "D", Convert.ToInt32(ID.ToString().Trim()), eqID.ToString().Trim(), cmpID.ToString().Trim());


            double NT1 = 0.000, MRT1 = 0.000;
            string strqry12 = "SELECT * from Tbl_InspectionDataDetails  where EqupID='" + eqID.ToString().Trim() + "' and componentno ='" + cmpID.ToString().Trim() + "' and InspectionPointNo ='" + Insno.ToString().Trim() + "'  and  Deleted=0";
            SqlCommand cmd12 = new SqlCommand(strqry12, conn);
            SqlDataReader rdr12 = cmd12.ExecuteReader();
            if (rdr12.Read())
            {

                BusinessTier.DisposeReader(rdr12);

                double NT = 0.000, MRT = 0.000, Dfvalue = 0.000;

                string sql = "Select  normalthickness,MRT,defaultvalue from VW_Componentview  where EqupID='" + eqID.ToString().Trim() + "' and CompAutoid ='" + cmpID.ToString().Trim() + "' and  Deleted=0";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    NT = Convert.ToDouble(rd["normalthickness"].ToString());
                    MRT = Convert.ToDouble(rd["MRT"].ToString());
                    if (string.IsNullOrEmpty(rd["defaultvalue"].ToString()))
                    {
                        Dfvalue = 0.010;
                    }
                    else
                    {
                        Dfvalue = Convert.ToDouble(rd["defaultvalue"].ToString());
                    }
                    NT1 = NT;
                    MRT1 = MRT;
                }
                else
                {
                    lblStatus.Text = "Please Check the Normal Thickness,MRT,Default CR Rate Values in Component Form";
                }
                BusinessTier.DisposeReader(rd);

                string sql1 = "Select  [InspecDate],[ReadingValue],[Previousdate],[Previousvalue],[Initialdate],[Initialvalue] from Tbl_InspectionDataDetails  where EqupID='" + eqID.ToString().Trim() + "' and componentno ='" + cmpID.ToString().Trim() + "' and InspectionPointNo ='" + Insno.ToString().Trim() + "'  and  Deleted=0 order by InspecDate";
                SqlDataAdapter adp = new SqlDataAdapter(sql1, conn);
                DataTable dataTable1 = new DataTable();
                adp.Fill(dataTable1);
                // int inspec = Convert.ToInt32(dataTable1.Rows.Count);
                double Initvalue = Convert.ToDouble(dataTable1.Rows[0]["ReadingValue"].ToString());
                DateTime Initialdt = Convert.ToDateTime(dataTable1.Rows[0]["InspecDate"].ToString());
                DateTime Previousdt = new DateTime();
                double Prevalue = 0.000;
                foreach (DataRow row in dataTable1.Rows)
                {
                    if (Previousdt.ToString() != "01/01/0001 00:00:00")
                    {
                        String prdt1 = Previousdt.ToString();
                        DateTime pdt1 = DateTime.Parse(prdt1);
                        prdt1 = pdt1.Month + "/" + pdt1.Day + "/" + pdt1.Year + " 00:00:00";
                        string sql7 = "Update Tbl_InspectionDataDetails set ShortCRrate=null,LongCRrate=null,DefaultsCR=null,DefaultlCR=null,uCR=null where EqupID='" + eqID.ToString().Trim() + "' and componentno ='" + cmpID.ToString().Trim() + "' and InspectionPointNo ='" + Insno.ToString().Trim() + "' and InspecDate='" + prdt1.ToString() + "' and  Deleted=0";
                        SqlCommand cmd7 = new SqlCommand(sql7, conn);
                        cmd7.ExecuteNonQuery();
                    }
                    DateTime inspectdt = Convert.ToDateTime(row["InspecDate"].ToString());


                    //Previousdt.datet
                    double inspectvalue = Convert.ToDouble(row["ReadingValue"].ToString());

                    // Short Corrosion Rate
                    double sCR = 0.000, dsCR = 0.000;
                    if (inspectdt == Initialdt)
                    {
                        sCR = 0;
                    }
                    else
                    {
                        double dt1 = inspectdt.Subtract(Previousdt).Days;
                        double dt = dt1 / 365;
                        if (dt == 0)
                            sCR = 0;
                        else
                            sCR = Math.Round((Prevalue - inspectvalue) / dt, 3);

                        if (sCR <= 0)
                            dsCR = Dfvalue;
                        else
                            dsCR = sCR;
                    }
                    // Long Corrosion Rate
                    double lCR = 0.000, dlCR = 0.000;
                    if (inspectdt == Initialdt)
                    {
                        lCR = 0;
                    }
                    else
                    {
                        double dt1 = inspectdt.Subtract(Initialdt).Days;
                        double dt = dt1 / 365;
                        if (dt == 0)
                            lCR = 0;
                        else
                            lCR = Math.Round((Initvalue - inspectvalue) / dt, 3);

                        if (lCR <= 0)
                            dlCR = Dfvalue;
                        else
                            dlCR = lCR;
                    }

                    // Remaing Life
                    double maxCR = 0.000, RemainLife = 0.000, maxdCR = 0.000;
                    maxCR = Math.Max(sCR, lCR);
                    RemainLife = (NT - MRT) / maxCR;
                    maxdCR = Math.Max(dsCR, dlCR);


                    String prdt = Previousdt.ToString();
                    DateTime pdt = DateTime.Parse(prdt);
                    prdt = pdt.Month + "/" + pdt.Day + "/" + pdt.Year + " 00:00:00";

                    String Initdt = Initialdt.ToString();
                    DateTime idt = DateTime.Parse(Initdt);
                    Initdt = idt.Month + "/" + idt.Day + "/" + idt.Year + " 00:00:00";

                    String insdt = inspectdt.ToString();
                    DateTime isdt = DateTime.Parse(insdt);
                    insdt = isdt.Month + "/" + isdt.Day + "/" + isdt.Year + " 00:00:00";
                    string sql5 = string.Empty;
                    if (inspectdt == Initialdt)
                    {
                        sql5 = "Update Tbl_InspectionDataDetails set [Previousdate]=null,[Previousvalue]=null,[Initialdate]=null,[Initialvalue]=null,ShortCRrate=null,LongCRrate=null,DefaultsCR=null,DefaultlCR=null,uCR=null,SCR=null,LCR=null,DSCR=null,DLCR=null where EqupID='" + eqID.ToString().Trim() + "' and componentno ='" + cmpID.ToString().Trim() + "' and InspectionPointNo ='" + Insno.ToString().Trim() + "' and InspecDate='" + insdt.ToString() + "' and  Deleted=0";
                    }
                    else
                    {
                        sql5 = "Update Tbl_InspectionDataDetails set [Previousdate]='" + prdt.ToString() + "',[Previousvalue]='" + Prevalue + "',[Initialdate]='" + Initdt.ToString() + "',[Initialvalue]='" + Initvalue + "'  ,ShortCRrate='" + sCR + "',LongCRrate='" + lCR + "',RemainingLife='" + RemainLife + "',DefaultsCR='" + dsCR + "',DefaultlCR='" + dlCR + "',SCR='" + sCR + "',LCR='" + lCR + "',DSCR='" + dsCR + "',DLCR='" + dlCR + "',uCR='" + maxdCR + "' where  EqupID='" + eqID.ToString().Trim() + "' and componentno ='" + cmpID.ToString().Trim() + "' and InspectionPointNo ='" + Insno.ToString().Trim() + "' and InspecDate='" + insdt.ToString() + "' and  Deleted=0";
                    }



                    SqlCommand cmd5 = new SqlCommand(sql5, conn);
                    cmd5.ExecuteNonQuery();
                    Previousdt = inspectdt;
                    Prevalue = inspectvalue;
                }
                double maxsCR = 0.000, maxlCR = 0.000;
                string sql4 = "Select max(DefaultsCR) as sc,max(DefaultlCR) as lc from Tbl_InspectionDataDetails  where EqupID='" + eqID.ToString().Trim() + "' and componentno ='" + cmpID.ToString().Trim() + "' and  Deleted=0";
                SqlCommand cmd4 = new SqlCommand(sql4, conn);
                SqlDataReader rd4 = cmd4.ExecuteReader();
                if (rd4.Read())
                {
                    maxsCR = Convert.ToDouble(rd4["sc"].ToString());
                    maxlCR = Convert.ToDouble(rd4["lc"].ToString());
                }

                double maxuCR = 0.000, RemainingLife = 0.000;
                maxuCR = Math.Max(maxsCR, maxlCR);
                RemainingLife = (NT - MRT) / maxuCR;

                BusinessTier.DisposeReader(rd4);

                int inspec = 0;
                string sql6 = "Select count(*) as Countvalue from Tbl_InspectionDataDetails  where EqupID='" + eqID.ToString().Trim() + "' and componentno ='" + cmpID.ToString().Trim() + "' and  Deleted=0";
                SqlCommand cmd6 = new SqlCommand(sql6, conn);
                SqlDataReader rd6 = cmd6.ExecuteReader();
                if (rd6.Read())
                {
                    inspec = Convert.ToInt32(rd6["Countvalue"].ToString());
                }
                BusinessTier.DisposeReader(rd6);

                string sql3 = " update Tbl_EquipmentComponentDetails set ShortCRrate='" + maxsCR + "',LongCRrate='" + maxlCR + "',RemainingLife='" + RemainingLife + "',uCR='" + maxuCR + "', NoofInspection='" + inspec + "' where EqupID = '" + eqID.ToString().Trim() + "' and Compautoid='" + cmpID.ToString().Trim() + "' and Deleted=0";
                SqlCommand cmd3 = new SqlCommand(sql3, conn);
                cmd3.ExecuteNonQuery();
            }
            else
            {
                BusinessTier.DisposeReader(rdr12);
                double maxsCR = 0.000, maxlCR = 0.000;
                string sql4 = "Select max(ShortCRrate) as sc,max(LongCRrate) as lc from Tbl_InspectionDataDetails  where EqupID='" + eqID.ToString().Trim() + "' and componentno ='" + cmpID.ToString().Trim() + "' and  Deleted=0";
                SqlCommand cmd4 = new SqlCommand(sql4, conn);
                SqlDataReader rd4 = cmd4.ExecuteReader();
                if (rd4.Read())
                {
                    if (string.IsNullOrEmpty(rd4["sc"].ToString()))
                    {
                        BusinessTier.DisposeReader(rd4);
                        string sql13 = "update Tbl_EquipmentComponentDetails set ShortCRrate=null,LongCRrate=null,RemainingLife=null,uCR=null, NoofInspection=0 where EqupID = '" + eqID.ToString().Trim() + "' and Compautoid='" + cmpID.ToString().Trim() + "' and Deleted=0";
                        SqlCommand cmd13 = new SqlCommand(sql13, conn);
                        cmd13.ExecuteNonQuery();
                        BusinessTier.DisposeConnection(conn);
                        return;
                    }
                    maxsCR = Convert.ToDouble(rd4["sc"].ToString());
                    maxlCR = Convert.ToDouble(rd4["lc"].ToString());
                }

                double maxuCR = 0.000, RemainingLife = 0.000;
                maxuCR = Math.Max(maxsCR, maxlCR);
                RemainingLife = (NT1 - MRT1) / maxuCR;

                BusinessTier.DisposeReader(rd4);

                int inspec = 0;
                string sql6 = "Select count(*) as Countvalue from Tbl_InspectionDataDetails  where EqupID='" + eqID.ToString().Trim() + "' and componentno ='" + cmpID.ToString().Trim() + "' and  Deleted=0";
                SqlCommand cmd6 = new SqlCommand(sql6, conn);
                SqlDataReader rd6 = cmd6.ExecuteReader();
                if (rd6.Read())
                {
                    inspec = Convert.ToInt32(rd6["Countvalue"].ToString());
                    BusinessTier.DisposeReader(rd6);

                    string sql3 = "update Tbl_EquipmentComponentDetails set ShortCRrate='" + maxsCR + "',LongCRrate='" + maxlCR + "',RemainingLife='" + RemainingLife + "',uCR='" + maxuCR + "', NoofInspection='" + inspec + "' where EqupID = '" + eqID.ToString().Trim() + "' and Compautoid='" + cmpID.ToString().Trim() + "' and Deleted=0";
                    SqlCommand cmd3 = new SqlCommand(sql3, conn);
                    cmd3.ExecuteNonQuery();
                }
                else
                {

                    BusinessTier.DisposeReader(rd6);
                }


            }
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(85);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(6);
            e.Canceled = true;
            BusinessTier.DisposeConnection(conn);
        }
    }

    protected void RadGrid1_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        btnlastinspection.Text = "  Last Inspection Value  ";
        // double cal = 00.000;
        // DateTime lat = new DateTime(2011, 04, 23);
        // DateTime intz = new DateTime(2009, 03, 07);
        // TimeSpan dtdiff = lat - intz;
        //// string dt = dtdiff.Days.ToString();
        //// double mn = Convert.ToDouble(dt)/30;
        // double dt1 = lat.Subtract(intz).Days / 30;
        // double dt = dt1 / 12;
        // cal = (5.54 - 5.40) / dt;
        //// cal = (5.54 - 5.40) / (mn/12);
        try
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            RadComboBox cboeqid = (RadComboBox)editedItem.FindControl("cboeqid");
            RadComboBox cbocompID = (RadComboBox)editedItem.FindControl("cbocompID");
            RadDatePicker txtInspdate = (RadDatePicker)editedItem.FindControl("txtInspdate");
            RadTextBox txtInspectionPointNo = (RadTextBox)editedItem.FindControl("txtInspectionPointNo");
            RadNumericTextBox txtReadingValue = (RadNumericTextBox)editedItem.FindControl("txtReadingValue");
            Label lbleqid = (Label)editedItem.FindControl("lbleqid");
            Label lblcompid = (Label)editedItem.FindControl("lblcompid");
            Label lblInspdate = (Label)editedItem.FindControl("lblInspdate");

            //--------------------------------------------------------
            if (cboeqid.SelectedValue.ToString() == "--Select--")
            {
                ShowMessage(36);
                e.Canceled = true;
                return;
            }
            if (string.IsNullOrEmpty(cbocompID.SelectedValue.ToString()))
            {
                ShowMessage(37);
                e.Canceled = true;
                return;
            }
            if (string.IsNullOrEmpty(txtInspdate.SelectedDate.ToString()))
            {
                ShowMessage(79);
                e.Canceled = true;
                return;
            }
            if (string.IsNullOrEmpty(txtInspectionPointNo.Text.ToString()))
            {
                ShowMessage(80);
                e.Canceled = true;
                return;
            }
            if (string.IsNullOrEmpty(txtReadingValue.Text.ToString()))
            {
                ShowMessage(81);
                e.Canceled = true;
                return;
            }
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            String Insdate = txtInspdate.SelectedDate.ToString();
            DateTime dtinsDate = DateTime.Parse(Insdate);
            Insdate = dtinsDate.Month + "/" + dtinsDate.Day + "/" + dtinsDate.Year + " 00:00:00";

            double NT = 0.000, MRT = 0.000, Dfvalue = 0.000;

            string sql = "Select  normalthickness,MRT,defaultvalue from VW_Componentview  where EqupID='" + cboeqid.SelectedValue.ToString().Trim() + "' and CompAutoid ='" + cbocompID.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                NT = Convert.ToDouble(rd["normalthickness"].ToString());
                MRT = Convert.ToDouble(rd["MRT"].ToString());
                if (string.IsNullOrEmpty(rd["defaultvalue"].ToString()))
                {
                    Dfvalue = 0.010;
                }
                else
                {
                    Dfvalue = Convert.ToDouble(rd["defaultvalue"].ToString());
                }
            }
            else
            {
                lblStatus.Text = "Please Check the Normal Thickness,MRT,Default CR Rate Values in Component Form";
            }
            BusinessTier.DisposeReader(rd);

            string sql2 = "insert into Tbl_InspectionDataDetails (EqupID,ComponentNo,InspecDate,InspectionPointNo,ReadingValue,ShortCRrate,LongCRrate,RemainingLife,companyid,createdby) values ('" + cboeqid.SelectedValue.ToString().Trim() + "', '" + cbocompID.SelectedValue.ToString().Trim() + "','" + Insdate.ToString() + "','" + txtInspectionPointNo.Text.ToString().Trim() + "','" + txtReadingValue.Text.ToString() + "' ,0,0,0, '" + Convert.ToInt32(Session["sesCompanyID"].ToString()) + "','" + Convert.ToInt32(Session["sesUserID"].ToString()) + "')";
            SqlCommand cmd2 = new SqlCommand(sql2, conn);
            cmd2.ExecuteNonQuery();

            string sql1 = "Select  [InspecDate],[ReadingValue],[Previousdate],[Previousvalue],[Initialdate],[Initialvalue] from Tbl_InspectionDataDetails  where EqupID='" + cboeqid.SelectedValue.ToString().Trim() + "' and componentno ='" + cbocompID.SelectedValue.ToString().Trim() + "' and InspectionPointNo ='" + txtInspectionPointNo.Text.ToString().Trim() + "'  and  Deleted=0 order by InspecDate";
            SqlDataAdapter adp = new SqlDataAdapter(sql1, conn);
            DataTable dataTable1 = new DataTable();
            adp.Fill(dataTable1);
            // int inspec = Convert.ToInt32(dataTable1.Rows.Count);
            double Initvalue = Convert.ToDouble(dataTable1.Rows[0]["ReadingValue"].ToString());
            DateTime Initialdt = Convert.ToDateTime(dataTable1.Rows[0]["InspecDate"].ToString());
            DateTime Previousdt = new DateTime();
            double Prevalue = 0.000;
            int i = 0;
            foreach (DataRow row in dataTable1.Rows)
            {
                double TInitvalue = Convert.ToDouble(dataTable1.Rows[i]["ReadingValue"].ToString());
                if (Previousdt.ToString() != "01/01/0001 00:00:00")
                {
                    String prdt1 = Previousdt.ToString();
                    DateTime pdt1 = DateTime.Parse(prdt1);
                    prdt1 = pdt1.Month + "/" + pdt1.Day + "/" + pdt1.Year + " 00:00:00";
                    string sql7 = "Update Tbl_InspectionDataDetails set ShortCRrate=null,LongCRrate=null,DefaultsCR=null,DefaultlCR=null,uCR=null where EqupID='" + cboeqid.SelectedValue.ToString().Trim() + "' and componentno ='" + cbocompID.SelectedValue.ToString().Trim() + "' and InspectionPointNo ='" + txtInspectionPointNo.Text.ToString().Trim() + "' and InspecDate='" + prdt1.ToString() + "' and  Deleted=0";
                    SqlCommand cmd7 = new SqlCommand(sql7, conn);
                    cmd7.ExecuteNonQuery();
                }

                DateTime inspectdt = Convert.ToDateTime(row["InspecDate"].ToString());


                //Previousdt.datet
                double inspectvalue = Convert.ToDouble(row["ReadingValue"].ToString());

                // Short Corrosion Rate
                double sCR = 0.000, dsCR = 0.000;
                if (inspectdt == Initialdt)
                {
                    sCR = 0;
                }
                else
                {
                    double dt1 = inspectdt.Subtract(Previousdt).Days;
                    double dt = dt1 / 365;
                    if (dt == 0)
                        sCR = 0;
                    else
                        sCR = Math.Round((Prevalue - inspectvalue) / dt, 3);

                    if (sCR <= 0)
                        dsCR = Dfvalue;
                    else
                        dsCR = sCR;
                }
                // Long Corrosion Rate
                double lCR = 0.000, dlCR = 0.000;
                if (inspectdt == Initialdt)
                {
                    lCR = 0;
                }
                else
                {
                    double dt1 = inspectdt.Subtract(Initialdt).Days;
                    double dt = dt1 / 365;
                    if (dt == 0)
                        lCR = 0;
                    else
                        lCR = Math.Round((Initvalue - inspectvalue) / dt, 3);

                    if (lCR <= 0)
                        dlCR = Dfvalue;
                    else
                        dlCR = lCR;
                }

                // Remaing Life
                double RemainLife = 0.000, maxdCR = 0.000;
                maxdCR = Math.Max(dsCR, dlCR);

                //if (sCR >= 0)
                //    ChksCR = sCR;
                //else
                //    ChksCR = Dfvalue;

                //if (lCR >= 0)
                //    ChklCR = lCR;
                //else
                //    ChklCR = Dfvalue;

                //maxCR = Math.Max(ChksCR, ChklCR);

                // RemainLife = (TInitvalue - MRT) / maxCR;
                RemainLife = Math.Round((TInitvalue - MRT) / maxdCR, 0);

                String prdt = Previousdt.ToString();
                DateTime pdt = DateTime.Parse(prdt);
                prdt = pdt.Month + "/" + pdt.Day + "/" + pdt.Year + " 00:00:00";

                String Initdt = Initialdt.ToString();
                DateTime idt = DateTime.Parse(Initdt);
                Initdt = idt.Month + "/" + idt.Day + "/" + idt.Year + " 00:00:00";

                String insdt = inspectdt.ToString();
                DateTime isdt = DateTime.Parse(insdt);
                insdt = isdt.Month + "/" + isdt.Day + "/" + isdt.Year + " 00:00:00";
                string sql5 = string.Empty;
                if (inspectdt == Initialdt)
                {
                    sql5 = "Update Tbl_InspectionDataDetails set [Previousdate]=null,[Previousvalue]=null,[Initialdate]=null,[Initialvalue]=null,ShortCRrate=null,LongCRrate=null,DefaultsCR=null,DefaultlCR=null,uCR=null,SCR=null,LCR=null,DSCR=null,DLCR=null where EqupID='" + cboeqid.SelectedValue.ToString().Trim() + "' and componentno ='" + cbocompID.SelectedValue.ToString().Trim() + "' and InspectionPointNo ='" + txtInspectionPointNo.Text.ToString().Trim() + "' and InspecDate='" + insdt.ToString() + "' and  Deleted=0";
                }
                else
                {
                    sql5 = "Update Tbl_InspectionDataDetails set [Previousdate]='" + prdt.ToString() + "',[Previousvalue]='" + Prevalue + "',[Initialdate]='" + Initdt.ToString() + "',[Initialvalue]='" + Initvalue + "'  ,ShortCRrate='" + sCR + "',LongCRrate='" + lCR + "',RemainingLife='" + RemainLife + "',DefaultsCR='" + dsCR + "',DefaultlCR='" + dlCR + "',SCR='" + sCR + "',LCR='" + lCR + "',DSCR='" + dsCR + "',DLCR='" + dlCR + "',uCR='" + maxdCR + "' where EqupID='" + cboeqid.SelectedValue.ToString().Trim() + "' and componentno ='" + cbocompID.SelectedValue.ToString().Trim() + "' and InspectionPointNo ='" + txtInspectionPointNo.Text.ToString().Trim() + "' and InspecDate='" + insdt.ToString() + "' and  Deleted=0";
                }

                SqlCommand cmd5 = new SqlCommand(sql5, conn);
                cmd5.ExecuteNonQuery();
                Previousdt = inspectdt;
                Prevalue = inspectvalue;
                i = i + 1;
            }

            double maxsCR = 0.000, maxlCR = 0.000;
            string sql4 = "Select max(DefaultsCR) as sc,max(DefaultlCR) as lc from Tbl_InspectionDataDetails  where EqupID='" + cboeqid.SelectedValue.ToString().Trim() + "' and componentno ='" + cbocompID.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            SqlCommand cmd4 = new SqlCommand(sql4, conn);
            SqlDataReader rd4 = cmd4.ExecuteReader();
            if (rd4.Read())
            {
                if (string.IsNullOrEmpty(rd4["sc"].ToString()))
                {
                    BusinessTier.DisposeReader(rd4);
                    BusinessTier.DisposeConnection(conn);
                    ShowMessage(83);
                    return;
                }

                maxsCR = Convert.ToDouble(rd4["sc"].ToString());
                maxlCR = Convert.ToDouble(rd4["lc"].ToString());
            }


            BusinessTier.DisposeReader(rd4);

            double maxuCR = 0.000, RemainingLife = 0.000, redval = 0.000;

            maxuCR = Math.Max(maxsCR, maxlCR);

            string strqry = "SELECT min(ReadingValue) as readval from Tbl_InspectionDataDetails WHERE EqupID='" + cboEquipment.SelectedValue.ToString() + "' and ComponentNo='" + cboComponent.SelectedValue.ToString() + "' and DefaultsCR='" + maxsCR.ToString() + "' and DefaultlCR='" + maxlCR.ToString() + "'  and deleted=0";
            SqlCommand cmdrd = new SqlCommand(strqry, conn);
            SqlDataReader rdr = cmdrd.ExecuteReader();
            if (rdr.Read())
            {

                if (string.IsNullOrEmpty(rdr["readval"].ToString()))
                {
                    rdr.Close();
                    string strqry11 = "SELECT min(ReadingValue) as readval11 from Tbl_InspectionDataDetails WHERE EqupID='" + cboEquipment.SelectedValue.ToString() + "' and ComponentNo='" + cboComponent.SelectedValue.ToString() + "' and uCR='" + maxuCR.ToString() + "' and deleted=0";
                    SqlCommand cmd11 = new SqlCommand(strqry11, conn);
                    SqlDataReader rdr11 = cmd11.ExecuteReader();
                    if (rdr11.Read())
                    {
                        redval = Convert.ToDouble(rdr11["readval11"].ToString());
                        rdr11.Close();
                    }

                }
                else
                {
                    redval = Convert.ToDouble(rdr["readval"].ToString());
                    rdr.Close();

                }
            }
            else
            {
                rdr.Close();
            }


            //string sql60 = "Select Readingvalue from Tbl_InspectionDataDetails  where EqupID='" + cboeqid.SelectedValue.ToString().Trim() + "' and componentno ='" + cbocompID.SelectedValue.ToString().Trim() + "' and DefaultsCR='" + maxsCR.ToString().Trim() + "' and DefaultlCR='" + maxlCR.ToString().Trim() + "' and  Deleted=0";
            //SqlCommand cmd60 = new SqlCommand(sql60, conn);
            //SqlDataReader rd60 = cmd60.ExecuteReader();
            //if (rd60.Read())
            //{
            //    redval = Convert.ToDouble(rd60["Readingvalue"].ToString());
            //}
            //BusinessTier.DisposeReader(rd60);

            RemainingLife = Math.Round((redval - MRT) / maxuCR, 0);

            int inspec = 0;
            string sql6 = "Select count(*) as Countvalue from Tbl_InspectionDataDetails  where EqupID='" + cboeqid.SelectedValue.ToString().Trim() + "' and componentno ='" + cbocompID.SelectedValue.ToString().Trim() + "' and InspectionPointNo='" + txtInspectionPointNo.Text.ToString().Trim() + "' and  Deleted=0";
            SqlCommand cmd6 = new SqlCommand(sql6, conn);
            SqlDataReader rd6 = cmd6.ExecuteReader();
            if (rd6.Read())
            {
                inspec = Convert.ToInt32(rd6["Countvalue"].ToString());
            }
            BusinessTier.DisposeReader(rd6);

            //double finscr = 0, finlcr = 0;
            //string sqlscr = "Select DefaultsCR,DefaultlCR from Tbl_InspectionDataDetails  where ReadingValue='" + redval.ToString().Trim() + "' and uCR='" + maxuCR + "' and Deleted=0";
            //SqlCommand cmdscr = new SqlCommand(sqlscr, conn);
            //SqlDataReader rdscr = cmdscr.ExecuteReader();
            //if (rdscr.Read())
            //{
            //    finscr = Convert.ToDouble(rdscr["DefaultsCR"].ToString());
            //    finlcr = Convert.ToDouble(rdscr["DefaultlCR"].ToString());
            //}
            //BusinessTier.DisposeReader(rdscr);

            string sql3 = " update Tbl_EquipmentComponentDetails set ShortCRrate='" + maxsCR + "',LongCRrate='" + maxlCR + "',RemainingLife='" + RemainingLife + "',uCR='" + maxuCR + "', NoofInspection='" + inspec + "',ReadVal='" + redval + "' where EqupID = '" + cboeqid.SelectedValue.ToString().Trim() + "' and Compautoid='" + cbocompID.SelectedValue.ToString().Trim() + "' and Deleted=0";
            SqlCommand cmd3 = new SqlCommand(sql3, conn);
            cmd3.ExecuteNonQuery();
            ShowMessage(83);
            //lblStatus.Text = "Inspection Values Successfully Inserted";
            //lblStatus.ForeColor = Color.Blue;


            //else
            //{
            //    int inspecautoiddup = 0;
            //    string strqrydup = "Select  inspecautoid  from Tbl_InspectionDataDetails  where EqupID='" + cboeqid.SelectedValue.ToString().Trim() + "' and componentno ='" + cbocompID.SelectedValue.ToString().Trim() + "' and InspectionPointNo ='" + txtInspectionPointNo.Text.ToString().Trim() + "'  and inspecdate='" + Insdate.ToString().Trim() + "' and  Deleted=0";  //inspecdate ='" + txtInspdate.SelectedDate.ToString().Trim() + "'
            //    SqlCommand cmddup = new SqlCommand(strqrydup, conn);
            //    SqlDataReader readerdup = cmddup.ExecuteReader();
            //    while (readerdup.Read())
            //    {
            //        inspecautoiddup = Convert.ToInt32((readerdup["inspecautoid"].ToString().Trim()));
            //    }
            //    BusinessTier.DisposeReader(readerdup);
            //    if (inspecautoiddup.ToString() != "0")
            //    {
            //        // ShowMessage(83);
            //        lblStatus.Text = "Same data already exist";
            //        e.Canceled = true;
            //        return;
            //    }
            //    string indt = Convert.ToString(txtInspdate.SelectedDate.ToString()).ToString();
            //    int flg = BusinessTier.Inspection(conn, cboeqid.SelectedValue.ToString().Trim(), cbocompID.SelectedValue.ToString().Trim(), Convert.ToDateTime(Insdate.ToString()), txtInspectionPointNo.Text.ToString().Trim(), txtReadingValue.Text.ToString(), Convert.ToInt32(Session["sesUserID"].ToString()), "N", 0);
            //    BusinessTier.DisposeConnection(conn);
            //    if (flg >= 1)
            //    {
            //        ShowMessage(83);
            //        return;
            //    }
            //}
        }

        catch (Exception ex)
        {
            lblStatus.Text = ex.Message;
            //ShowMessage(4);
            // e.Canceled = true;
        }
        RadGrid1.Rebind();
    }

    protected void RadGrid1_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        btnlastinspection.Text = "  Last Inspection Value  ";
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            Label lblID = (Label)editedItem.FindControl("lblID");
            RadComboBox cboeqid = (RadComboBox)editedItem.FindControl("cboeqid");
            RadComboBox cbocompID = (RadComboBox)editedItem.FindControl("cbocompID");
            RadDatePicker txtInspdate = (RadDatePicker)editedItem.FindControl("txtInspdate");
            RadTextBox txtInspectionPointNo = (RadTextBox)editedItem.FindControl("txtInspectionPointNo");
            RadNumericTextBox txtReadingValue = (RadNumericTextBox)editedItem.FindControl("txtReadingValue");
            Label lbleqid = (Label)editedItem.FindControl("lbleqid");
            Label lblcompid = (Label)editedItem.FindControl("lblcompid");
            Label lblInspdate = (Label)editedItem.FindControl("lblInspdate");

            if (string.IsNullOrEmpty(txtInspdate.SelectedDate.ToString()))
            {
                ShowMessage(79);
                e.Canceled = true;
                return;
            }

            if (string.IsNullOrEmpty(txtReadingValue.Text.ToString()))
            {
                ShowMessage(81);
                e.Canceled = true;
                return;
            }

            String Insdate = txtInspdate.SelectedDate.ToString();
            DateTime dtinsDate = DateTime.Parse(Insdate);
            Insdate = dtinsDate.Month + "/" + dtinsDate.Day + "/" + dtinsDate.Year + " 00:00:00";

            double NT = 0.000, MRT = 0.000, Dfvalue = 0.000;

            string sql = "Select  normalthickness,MRT,defaultvalue from VW_Componentview  where EqupID='" + cboeqid.SelectedValue.ToString().Trim() + "' and CompAutoid ='" + cbocompID.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                NT = Convert.ToDouble(rd["normalthickness"].ToString());
                MRT = Convert.ToDouble(rd["MRT"].ToString());
                if (string.IsNullOrEmpty(rd["defaultvalue"].ToString()))
                {
                    Dfvalue = 0.010;
                }
                else
                {
                    Dfvalue = Convert.ToDouble(rd["defaultvalue"].ToString());
                }
            }
            else
            {
                lblStatus.Text = "Please Check the Normal Thickness,MRT,Default CR Rate Values in Component Form";
            }
            BusinessTier.DisposeReader(rd);

            string sql2 = "update Tbl_InspectionDataDetails set InspecDate='" + Insdate.ToString() + "', ReadingValue ='" + txtReadingValue.Text.ToString().Trim() + "' where InspecautoID ='" + lblID.Text.ToString() + "'";
            SqlCommand cmd2 = new SqlCommand(sql2, conn);
            cmd2.ExecuteNonQuery();

            string sql1 = "Select  [InspecDate],[ReadingValue],[Previousdate],[Previousvalue],[Initialdate],[Initialvalue] from Tbl_InspectionDataDetails  where EqupID='" + cboeqid.SelectedValue.ToString().Trim() + "' and componentno ='" + cbocompID.SelectedValue.ToString().Trim() + "' and InspectionPointNo ='" + txtInspectionPointNo.Text.ToString().Trim() + "'  and  Deleted=0 order by InspecDate";
            SqlDataAdapter adp = new SqlDataAdapter(sql1, conn);
            DataTable dataTable1 = new DataTable();
            adp.Fill(dataTable1);
            // int inspec = Convert.ToInt32(dataTable1.Rows.Count);
            double Initvalue = Convert.ToDouble(dataTable1.Rows[0]["ReadingValue"].ToString());
            DateTime Initialdt = Convert.ToDateTime(dataTable1.Rows[0]["InspecDate"].ToString());
            DateTime Previousdt = new DateTime();
            double Prevalue = 0.000;
            int i = 0;
            foreach (DataRow row in dataTable1.Rows)
            {

                double TInitvalue = Convert.ToDouble(dataTable1.Rows[i]["ReadingValue"].ToString());
                if (Previousdt.ToString() != "01/01/0001 00:00:00")
                {
                    String prdt1 = Previousdt.ToString();
                    DateTime pdt1 = DateTime.Parse(prdt1);
                    prdt1 = pdt1.Month + "/" + pdt1.Day + "/" + pdt1.Year + " 00:00:00";
                    string sql7 = "Update Tbl_InspectionDataDetails set ShortCRrate=null,LongCRrate=null,DefaultsCR=null,DefaultlCR=null,uCR=null where EqupID='" + cboeqid.SelectedValue.ToString().Trim() + "' and componentno ='" + cbocompID.SelectedValue.ToString().Trim() + "' and InspectionPointNo ='" + txtInspectionPointNo.Text.ToString().Trim() + "' and InspecDate='" + prdt1.ToString() + "' and  Deleted=0";
                    SqlCommand cmd7 = new SqlCommand(sql7, conn);
                    cmd7.ExecuteNonQuery();
                }

                DateTime inspectdt = Convert.ToDateTime(row["InspecDate"].ToString());


                //Previousdt.datet
                double inspectvalue = Convert.ToDouble(row["ReadingValue"].ToString());

                // Short Corrosion Rate
                double sCR = 0.000, dsCR = 0.000;
                if (inspectdt == Initialdt)
                {
                    sCR = 0;
                }
                else
                {
                    double dt1 = inspectdt.Subtract(Previousdt).Days;
                    double dt = dt1 / 365;
                    if (dt == 0)
                        sCR = 0;
                    else
                        sCR = Math.Round((Prevalue - inspectvalue) / dt, 3);

                    if (sCR <= 0)
                        dsCR = Dfvalue;
                    else
                        dsCR = sCR;
                }
                // Long Corrosion Rate
                double lCR = 0.000, dlCR = 0.000;
                if (inspectdt == Initialdt)
                {
                    lCR = 0;
                }
                else
                {
                    double dt1 = inspectdt.Subtract(Initialdt).Days;
                    double dt = dt1 / 365;
                    if (dt == 0)
                        lCR = 0;
                    else
                        lCR = Math.Round((Initvalue - inspectvalue) / dt, 3);

                    if (lCR <= 0)
                        dlCR = Dfvalue;
                    else
                        dlCR = lCR;
                }


                // Remaing Life
                double RemainLife = 0.000, maxdCR = 0.000;
                maxdCR = Math.Max(dsCR, dlCR);

                //if (sCR >= 0)
                //    ChksCR = sCR;
                //else
                //    ChksCR = Dfvalue;

                //if (lCR >= 0)
                //    ChklCR = lCR;
                //else
                //    ChklCR = Dfvalue;

                //maxCR = Math.Max(ChksCR, ChklCR);

                // RemainLife = (TInitvalue - MRT) / maxCR;
                RemainLife = Math.Round((TInitvalue - MRT) / maxdCR, 0);

                String prdt = Previousdt.ToString();
                DateTime pdt = DateTime.Parse(prdt);
                prdt = pdt.Month + "/" + pdt.Day + "/" + pdt.Year + " 00:00:00";

                String Initdt = Initialdt.ToString();
                DateTime idt = DateTime.Parse(Initdt);
                Initdt = idt.Month + "/" + idt.Day + "/" + idt.Year + " 00:00:00";

                String insdt = inspectdt.ToString();
                DateTime isdt = DateTime.Parse(insdt);
                insdt = isdt.Month + "/" + isdt.Day + "/" + isdt.Year + " 00:00:00";
                string sql5 = string.Empty;
                if (inspectdt == Initialdt)
                {
                    sql5 = "Update Tbl_InspectionDataDetails set [Previousdate]=null,[Previousvalue]=null,[Initialdate]=null,[Initialvalue]=null,ShortCRrate=null,LongCRrate=null,DefaultsCR=null,DefaultlCR=null,uCR=null,SCR=null,LCR=null,DSCR=null,DLCR=null where EqupID='" + cboeqid.SelectedValue.ToString().Trim() + "' and componentno ='" + cbocompID.SelectedValue.ToString().Trim() + "' and InspectionPointNo ='" + txtInspectionPointNo.Text.ToString().Trim() + "' and InspecDate='" + insdt.ToString() + "' and  Deleted=0";
                }
                else
                {
                    sql5 = "Update Tbl_InspectionDataDetails set [Previousdate]='" + prdt.ToString() + "',[Previousvalue]='" + Prevalue + "',[Initialdate]='" + Initdt.ToString() + "',[Initialvalue]='" + Initvalue + "'  ,ShortCRrate='" + sCR + "',LongCRrate='" + lCR + "',RemainingLife='" + RemainLife + "',DefaultsCR='" + dsCR + "',DefaultlCR='" + dlCR + "',SCR='" + sCR + "',LCR='" + lCR + "',DSCR='" + dsCR + "',DLCR='" + dlCR + "',uCR='" + maxdCR + "' where EqupID='" + cboeqid.SelectedValue.ToString().Trim() + "' and componentno ='" + cbocompID.SelectedValue.ToString().Trim() + "' and InspectionPointNo ='" + txtInspectionPointNo.Text.ToString().Trim() + "' and InspecDate='" + insdt.ToString() + "' and  Deleted=0";
                }

                SqlCommand cmd5 = new SqlCommand(sql5, conn);
                cmd5.ExecuteNonQuery();
                Previousdt = inspectdt;
                Prevalue = inspectvalue;
                i = i + 1;
            }


            //string equipmentID = "0";
            //string strqryEq = "SELECT equautoid from Tbl_EquipmentAsset where equpid='" + cboeqid.SelectedItem.Text.ToString().Trim() + "' and deleted=0";
            //SqlCommand cmdautoid = new SqlCommand(strqryEq, conn);
            //SqlDataReader readerequip = cmdautoid.ExecuteReader();
            //while (readerequip.Read())
            //{
            //    equipmentID = (readerequip["equautoid"].ToString().Trim());
            //}
            //BusinessTier.DisposeReader(readerequip);

            //string ComponenttID = "0";
            //string strqrycom = "SELECT compautoid from Tbl_EquipmentComponentDetails where compno='" + cbocompID.Text.ToString().Trim() + "' and deleted=0";
            //SqlCommand cmdcomp = new SqlCommand(strqrycom, conn);
            //SqlDataReader readercomp = cmdcomp.ExecuteReader();
            //while (readercomp.Read())
            //{
            //    ComponenttID = (readercomp["compautoid"].ToString().Trim());
            //}
            //BusinessTier.DisposeReader(readercomp);

            //int flg = BusinessTier.Inspection(conn, equipmentID.ToString().Trim(), ComponenttID.ToString().Trim(),txtInspdate.SelectedDate.Value.Date, txtInspectionPointNo.Text.ToString().Trim(),txtReadingValue.Text.ToString(), Convert.ToInt32(Session["sesUserID"].ToString()), "U", Convert.ToInt32(lblID.Text.ToString()));
            //BusinessTier.DisposeConnection(conn);
            //if (flg >= 1)
            //{
            //    ShowMessage(60);
            //}
            //else
            //{
            //    ShowMessage(58);
            //    e.Canceled = true;
            //    return;
            //}
            double maxsCR = 0.000, maxlCR = 0.000;
            string sql4 = "Select max(DefaultsCR) as sc,max(DefaultlCR) as lc from Tbl_InspectionDataDetails where EqupID='" + cboeqid.SelectedValue.ToString().Trim() + "' and componentno ='" + cbocompID.SelectedValue.ToString().Trim() + "' and  Deleted=0";
            SqlCommand cmd4 = new SqlCommand(sql4, conn);
            SqlDataReader rd4 = cmd4.ExecuteReader();
            if (rd4.Read())
            {
                if (string.IsNullOrEmpty(rd4["sc"].ToString()))
                {
                    BusinessTier.DisposeReader(rd4);
                    BusinessTier.DisposeConnection(conn);
                    ShowMessage(84);
                    return;
                }
                maxsCR = Convert.ToDouble(rd4["sc"].ToString());
                maxlCR = Convert.ToDouble(rd4["lc"].ToString());
            }


            BusinessTier.DisposeReader(rd4);

            double maxuCR = 0.000, RemainingLife = 0.000, redval = 0.000;
            maxuCR = Math.Max(maxsCR, maxlCR);

            string strqry = "SELECT min(ReadingValue) as readval from Tbl_InspectionDataDetails WHERE EqupID='" + cboEquipment.SelectedValue.ToString() + "' and ComponentNo='" + cboComponent.SelectedValue.ToString() + "' and DefaultsCR='" + maxsCR.ToString() + "' and DefaultlCR='" + maxlCR.ToString() + "'  and deleted=0";
            SqlCommand cmdrd = new SqlCommand(strqry, conn);
            SqlDataReader rdr = cmdrd.ExecuteReader();
            if (rdr.Read())
            {

                if (string.IsNullOrEmpty(rdr["readval"].ToString()))
                {
                    rdr.Close();
                    string strqry11 = "SELECT min(ReadingValue) as readval11 from Tbl_InspectionDataDetails WHERE EqupID='" + cboEquipment.SelectedValue.ToString() + "' and ComponentNo='" + cboComponent.SelectedValue.ToString() + "' and uCR='" + maxuCR.ToString() + "' and deleted=0";
                    SqlCommand cmd11 = new SqlCommand(strqry11, conn);
                    SqlDataReader rdr11 = cmd11.ExecuteReader();
                    if (rdr11.Read())
                    {
                        redval = Convert.ToDouble(rdr11["readval11"].ToString());
                        rdr11.Close();
                    }

                }
                else
                {
                    redval = Convert.ToDouble(rdr["readval"].ToString());
                    rdr.Close();

                }
            }
            else
            {
                rdr.Close();
            }

            //string sql60 = "Select Readingvalue from Tbl_InspectionDataDetails  where EqupID='" + cboeqid.SelectedValue.ToString().Trim() + "' and componentno ='" + cbocompID.SelectedValue.ToString().Trim() + "' and DefaultsCR='" + maxsCR.ToString().Trim() + "' and DefaultlCR='" + maxlCR.ToString().Trim() + "' and  Deleted=0";
            //SqlCommand cmd60 = new SqlCommand(sql60, conn);
            //SqlDataReader rd60 = cmd60.ExecuteReader();
            //if (rd60.Read())
            //{
            //    redval = Convert.ToDouble(rd60["Readingvalue"].ToString());
            //}
            //BusinessTier.DisposeReader(rd60);




            RemainingLife = Math.Round((redval - MRT) / maxuCR, 0);


            int inspec = 0;
            string sql6 = "Select count(*) as Countvalue from Tbl_InspectionDataDetails  where EqupID='" + cboeqid.SelectedValue.ToString().Trim() + "' and componentno ='" + cbocompID.SelectedValue.ToString().Trim() + "' and InspectionPointNo='" + txtInspectionPointNo.Text.ToString().Trim() + "'  and  Deleted=0";
            SqlCommand cmd6 = new SqlCommand(sql6, conn);
            SqlDataReader rd6 = cmd6.ExecuteReader();
            if (rd6.Read())
            {
                inspec = Convert.ToInt32(rd6["Countvalue"].ToString());
            }
            BusinessTier.DisposeReader(rd6);

            //double finscr = 0,finlcr=0;
            //string sqlscr = "Select DefaultsCR,DefaultlCR from Tbl_InspectionDataDetails  where ReadingValue='" + redval.ToString().Trim() + "' and  Deleted=0";
            //SqlCommand cmdscr = new SqlCommand(sqlscr, conn);
            //SqlDataReader rdscr = cmdscr.ExecuteReader();
            //if (rdscr.Read())
            //{
            //    finscr = Convert.ToDouble(rdscr["DefaultsCR"].ToString());
            //    finlcr = Convert.ToDouble(rdscr["DefaultlCR"].ToString());
            //}
            //BusinessTier.DisposeReader(rdscr);


            string sql3 = " update Tbl_EquipmentComponentDetails set ShortCRrate='" + maxsCR + "',LongCRrate='" + maxlCR + "',RemainingLife='" + RemainingLife + "',uCR='" + maxuCR + "', NoofInspection='" + inspec + "',ReadVal='" + redval + "' where EqupID = '" + cboeqid.SelectedValue.ToString().Trim() + "' and Compautoid='" + cbocompID.SelectedValue.ToString().Trim() + "' and Deleted=0";
            SqlCommand cmd3 = new SqlCommand(sql3, conn);
            cmd3.ExecuteNonQuery();
            ShowMessage(84);
            BusinessTier.DisposeConnection(conn);
        }
        catch (Exception ex)
        {
            BusinessTier.DisposeConnection(conn);
            //ShowMessage(5);
            //e.Canceled = true;

        }
        RadGrid1.Rebind();
    }

    // ====================================================================================================

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

    protected void OnSelectedIndexChangedInspection(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        RadComboBox combobox = (RadComboBox)sender;
        GridEditFormItem editedItem = (GridEditFormItem)combobox.NamingContainer;
        Label lblID = (Label)editedItem.FindControl("lblID");
        RadComboBox cboeqid = (RadComboBox)editedItem.FindControl("cboeqid");
        RadComboBox cbocompID = (RadComboBox)editedItem.FindControl("cbocompID");
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            string sql1 = "select CompName,compautoID,compno from Tbl_EquipmentComponentDetails where EqupID='" + cboeqid.SelectedValue.ToString() + "' and deleted=0";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            cbocompID.Text = string.Empty;
            cbocompID.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();

                item.Text = row["CompName"].ToString();
                //item.Text = row["compno"].ToString();
                item.Value = row["compautoID"].ToString();
                string balqty = row["compautoID"].ToString();
                if (balqty != "")
                {
                    item.Attributes.Add("CompName", row["CompName"].ToString());
                    item.Attributes.Add("compno", row["compno"].ToString());
                    // Lbldupdate.Text=(row["IncomingDate"].ToString());

                    cbocompID.Items.Add(item);
                }
                item.DataBind();
            }
            BusinessTier.DisposeAdapter(adapter1);
            BusinessTier.DisposeConnection(conn);
        }
        catch (Exception ex)
        {
            BusinessTier.DisposeConnection(conn);
            lblStatus.Text = "Err:Select correct Product Item" + ex.Message.ToString();
            return;
        }
    }

    protected void OnSelectedDateChanged_InpectionDate(object sender, EventArgs e)
    {
        RadDatePicker inpectdate = (RadDatePicker)sender;
        GridEditFormItem editedItem = (GridEditFormItem)inpectdate.NamingContainer;
        RadComboBox cboeqid = (RadComboBox)editedItem.FindControl("cboeqid");
        RadComboBox cbocompID = (RadComboBox)editedItem.FindControl("cbocompID");
        RadTextBox txtInspectionPointNo = (RadTextBox)editedItem.FindControl("txtInspectionPointNo");
        RadDatePicker txtInspdate = (RadDatePicker)editedItem.FindControl("txtInspdate");
        Label lblID = (Label)editedItem.FindControl("lblID");
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            if (cboeqid.Text.ToString() == "--Select--")
            {
                ShowMessage(36);

                return;
            }
            if (string.IsNullOrEmpty(cbocompID.Text.ToString()))
            {
                ShowMessage(37);

                return;
            }
            if (string.IsNullOrEmpty(txtInspectionPointNo.Text.ToString()))
            {
                ShowMessage(80);

                return;
            }
            if (lblID.Text == "")
            {
                string sql1 = "select max(inspecdate) as inspectdt  from Tbl_InspectionDataDetails where EqupID='" + cboeqid.SelectedValue.ToString() + "' and ComponentNo= '" + cbocompID.SelectedValue.ToString().Trim() + "' and inspectionpointno='" + txtInspectionPointNo.Text.ToString().Trim() + "' and deleted=0";
                SqlCommand cmd11 = new SqlCommand(sql1, conn);
                SqlDataReader rdr11 = cmd11.ExecuteReader();
                if (rdr11.Read())
                {
                    if (!(string.IsNullOrEmpty(rdr11["inspectdt"].ToString().Trim())))
                    {
                        DateTime endDate = Convert.ToDateTime(txtInspdate.SelectedDate.ToString());
                        DateTime startDate = Convert.ToDateTime(rdr11["inspectdt"].ToString().Trim());
                        TimeSpan diff = startDate.Subtract(endDate);
                        if (diff.Days > 0)
                        {


                            // ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "confirm('Given Date is Lower then InspectionDate do u want to continue ?');", true);
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            sb.Append(@"<script language='javascript'>");
                            sb.Append(@"Confirm();");
                            sb.Append(@"</script>");
                            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "JCall1", sb.ToString(), false);
                            return;

                        }
                    }
                }
                BusinessTier.DisposeReader(rdr11);
            }

            BusinessTier.DisposeConnection(conn);
        }
        catch (Exception ex)
        {
            BusinessTier.DisposeConnection(conn);
            lblStatus.Text = "Err:Select correct Product Item" + ex.Message.ToString();
            return;
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