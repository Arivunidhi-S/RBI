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
using System.ComponentModel;
using System.Globalization;
using System.Collections;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.OleDb;
using System.Drawing;
using Telerik.Web.UI.Calendar;
using System.Web.SessionState;
using System.Runtime.Remoting.Contexts;
using System.Web.Script.Serialization;
using System.Net.Mail;


public partial class PPEIssue : System.Web.UI.Page
{
   // protected BunnyBear.msgBox MsgBox1;// = new BunnyBear.msgBox();

    public DataTable dtMenuItems = new DataTable();

    public DataTable dtSubMenuItems = new DataTable();

    public static string stocklevel = "High";

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            //Session["sesUserID"] = "1";
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
            if (IsPostBack)
            {
                if (Request.Form["hid_f"] == "1")
                {
                    Request.Form["hid_f"].Replace("1", "0");
                }

            }

            Session["Incdate"] = 0;
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
    //=====================================================================================================

    protected void RadGrid1_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditFormItem editedItem = e.Item as GridEditFormItem;
                RadComboBox cboProcessArea = (RadComboBox)editedItem.FindControl("cboProcessArea");
                RadComboBox cboEquipmentID = (RadComboBox)editedItem.FindControl("cboEquipmentID");
                Label lblProcessArea = (Label)editedItem.FindControl("lblProcessArea");
                Label lblEquipmentID = (Label)editedItem.FindControl("lblEquipmentID");
                Label lblCompNo = (Label)editedItem.FindControl("lblCompNo");
                RadTextBox txtCompNo = (RadTextBox)editedItem.FindControl("txtCompNo");
                RadTextBox txtCompName = (RadTextBox)editedItem.FindControl("txtCompName");
                RadComboBox cboinsulated = (RadComboBox)editedItem.FindControl("cboinsulated");
                RadComboBox cbopainting = (RadComboBox)editedItem.FindControl("cbopainting");
                RadTextBox txtMaterialtype = (RadTextBox)editedItem.FindControl("txtMaterialtype");
                RadTextBox txtmaterialspecification = (RadTextBox)editedItem.FindControl("txtmaterialspecification");
                RadTextBox txtNormalThickness = (RadTextBox)editedItem.FindControl("txtNormalThickness");
                RadTextBox txtConstThickness = (RadTextBox)editedItem.FindControl("txtConstThickness");
                RadNumericTextBox txtmrt = (RadNumericTextBox)editedItem.FindControl("txtmrt");
                RadTextBox txtDesignpressure = (RadTextBox)editedItem.FindControl("txtDesignpressure");
                RadTextBox txtdesigntemp = (RadTextBox)editedItem.FindControl("txtdesigntemp");
                RadTextBox txtoppressure = (RadTextBox)editedItem.FindControl("txtoppressure");
                RadTextBox txtOPtemp = (RadTextBox)editedItem.FindControl("txtOPtemp");
                RadTextBox txtDefaultValue = (RadTextBox)editedItem.FindControl("txtDefaultValue");
                RadComboBox cboMaterialtype = (RadComboBox)editedItem.FindControl("cboMaterialtype");
                TextBox txtinspection = (TextBox)editedItem.FindControl("txtinspection");
                RadNumericTextBox txtexpeCR = (RadNumericTextBox)editedItem.FindControl("txtexpeCR");
                RadComboBox cboinspecEffec = (RadComboBox)editedItem.FindControl("cboinspecEffec");
                RadTextBox txtcorrAllow = (RadTextBox)editedItem.FindControl("txtcorrAllow");
                RadComboBox cboclad = (RadComboBox)editedItem.FindControl("cboclad");

            }
        }
        catch (Exception ex)
        {
            ShowMessage(8);
            e.Canceled = true;
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "ppeissue", "RadGrid1_ItemCreated", ex.ToString(), "Audit");
        }

    }

    //--------------------------------------------------------------------------------------------

    protected void RadGrid1_ItemDataBound(object source, Telerik.Web.UI.GridItemEventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditFormItem editedItem = e.Item as GridEditFormItem;
                Label lblID = (Label)editedItem.FindControl("lblID");
                Label lblProcessArea = (Label)editedItem.FindControl("lblProcessArea");
                Label lblEquipmentID = (Label)editedItem.FindControl("lblEquipmentID");
                Label lblCompNo = (Label)editedItem.FindControl("lblCompNo");
                RadComboBox cboProcessArea = (RadComboBox)editedItem.FindControl("cboProcessArea");
                RadComboBox cboEquipmentID = (RadComboBox)editedItem.FindControl("cboEquipmentID");
                RadTextBox txtCompNo = (RadTextBox)editedItem.FindControl("txtCompNo");
                RadTextBox txtCompName = (RadTextBox)editedItem.FindControl("txtCompName");
                RadComboBox cboinsulated = (RadComboBox)editedItem.FindControl("cboinsulated");
                RadComboBox cbopainting = (RadComboBox)editedItem.FindControl("cbopainting");
                RadTextBox txtMaterialtype = (RadTextBox)editedItem.FindControl("txtMaterialtype");
                RadTextBox txtmaterialspecification = (RadTextBox)editedItem.FindControl("txtmaterialspecification");
                RadTextBox txtNormalThickness = (RadTextBox)editedItem.FindControl("txtNormalThickness");
                RadTextBox txtConstThickness = (RadTextBox)editedItem.FindControl("txtConstThickness");
                RadNumericTextBox txtmrt = (RadNumericTextBox)editedItem.FindControl("txtmrt");
                RadTextBox txtDesignpressure = (RadTextBox)editedItem.FindControl("txtDesignpressure");
                RadTextBox txtdesigntemp = (RadTextBox)editedItem.FindControl("txtdesigntemp");
                RadTextBox txtoppressure = (RadTextBox)editedItem.FindControl("txtoppressure");
                RadTextBox txtOPtemp = (RadTextBox)editedItem.FindControl("txtOPtemp");
                cboProcessArea.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(OnSelectedIndexChangedEquipment);

                RadComboBox cboMaterialtype = (RadComboBox)editedItem.FindControl("cboMaterialtype");
                cboMaterialtype.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(OnSelectedIndexChangedMaterialtype);
                //RadComboBox cboinspection = (RadComboBox)editedItem.FindControl("cboinspection");
                TextBox txtinspection = (TextBox)editedItem.FindControl("txtinspection");
                RadNumericTextBox txtexpeCR = (RadNumericTextBox)editedItem.FindControl("txtexpeCR");
                RadComboBox cboinspecEffec = (RadComboBox)editedItem.FindControl("cboinspecEffec");
                RadTextBox txtcorrAllow = (RadTextBox)editedItem.FindControl("txtcorrAllow");
                RadComboBox cboclad = (RadComboBox)editedItem.FindControl("cboclad");
                RadTextBox txtDefaultValue = (RadTextBox)editedItem.FindControl("txtDefaultValue");

                string sql1 = " SELECT [ProcessAreaID],[processarea] FROM [Tbl_ProcessArea] where deleted=0  and companyid= '" + Convert.ToInt32(Session["sesCompanyID"].ToString()) + "' ORDER BY [processareaid]";
                SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
                // adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
                DataTable dataTable1 = new DataTable();
                adapter1.Fill(dataTable1);
                // RadComboBox comboBox = (RadComboBox)sender;
                cboProcessArea.Items.Clear();

                foreach (DataRow row in dataTable1.Rows)
                {
                    RadComboBoxItem item = new RadComboBoxItem();

                    item.Text = row["processarea"].ToString();
                    item.Value = row["ProcessAreaID"].ToString();
                    cboProcessArea.Items.Add(item);
                    item.DataBind();
                }
                adapter1.Dispose();
                //cboProcessArea.Text = "<---Select--->";


                if (!(string.IsNullOrEmpty(lblID.Text.ToString())))
                {
                    //SqlConnection conn = BusinessTier.getConnection();
                   // conn.Open();
                    string strqry1 = "SELECT * from VW_Componentview WHERE  compautoID = '" + lblID.Text.ToString() + "' ";
                    SqlCommand cmd11 = new SqlCommand(strqry1, conn);
                    SqlDataReader rdr11 = cmd11.ExecuteReader();
                    if (rdr11.Read())
                    {
                        cboProcessArea.Text = rdr11["ProcessArea"].ToString().Trim();
                        cboProcessArea.SelectedValue = rdr11["ProcessAreaID"].ToString().Trim();
                        cboEquipmentID.Text = rdr11["EquipID"].ToString().Trim();
                        cboEquipmentID.SelectedValue = rdr11["EqupID"].ToString().Trim();
                        cboMaterialtype.SelectedItem.Text = rdr11["Materialtype"].ToString().Trim();
                        txtinspection.Text = rdr11["NoofInspection"].ToString().Trim();
                        cboinspecEffec.SelectedItem.Text = rdr11["InspectionEffective"].ToString().Trim();
                        cboclad.SelectedItem.Text = rdr11["Clad"].ToString().Trim();

                        cboinsulated.SelectedValue = rdr11["insulated"].ToString().Trim();
                        cbopainting.SelectedValue = rdr11["painting"].ToString().Trim();
                    }

                    BusinessTier.DisposeReader(rdr11);
                    BusinessTier.DisposeConnection(conn);
                }
                else
                {

                    BusinessTier.DisposeConnection(conn);
                    txtinspection.Text = "0";
                }
            }

        }
        catch (Exception ex)
        {
            ShowMessage(8);
            e.Canceled = true;
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "component", "RadGrid1_ItemDataBound", ex.ToString(), "Audit");
        }

        finally
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    //============================================================================================================

    protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            RadGrid1.DataSource = DataSourceHelper();
        }
        catch (Exception ex)
        {
            ShowMessage(9);
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "ppeissue", "NeedDataSource", ex.ToString(), "Audit");
        }
    }

    public DataTable DataSourceHelper()
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Close();
        string sql = " SELECT * from VW_Componentview where deleted=0 and companyid= '" + Convert.ToInt32(Session["sesCompanyID"].ToString()) + "' order by compAutoid asc";
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
        try
        {
            int flg = 0;
            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CompAutoID"].ToString().Trim();
            string dt = Convert.ToString(System.DateTime.Now);
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            //flg = BusinessTier.Component(conn, 0, 0, "", "", "", "", "", "", "", "", "", "", "", "", "", "","","", Convert.ToInt32(Session["sesUserID"].ToString()), "D", Convert.ToInt32(ID.ToString().Trim()));
            flg = BusinessTier.Componentdel(conn, 0, 0, "", "", "D", Convert.ToInt32(ID.ToString().Trim()));
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(61);
            }
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "component", "Delete", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(6);
            e.Canceled = true;
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "component", "Delete", ex.ToString(), "Audit");
        }
    }

    //-----------------------------------------------------------------------------------------------

    protected void RadGrid1_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            //Label lblID = (Label)editedItem.FindControl("lblID");
            Label lblProcessArea = (Label)editedItem.FindControl("lblProcessArea");
            Label lblEquipmentID = (Label)editedItem.FindControl("lblEquipmentID");
            Label lblCompNo = (Label)editedItem.FindControl("lblCompNo");
            RadComboBox cboProcessArea = (RadComboBox)editedItem.FindControl("cboProcessArea");
            RadComboBox cboEquipmentID = (RadComboBox)editedItem.FindControl("cboEquipmentID");
            RadTextBox txtCompNo = (RadTextBox)editedItem.FindControl("txtCompNo");
            RadTextBox txtCompName = (RadTextBox)editedItem.FindControl("txtCompName");
            RadComboBox cboinsulated = (RadComboBox)editedItem.FindControl("cboinsulated");
            RadComboBox cbopainting = (RadComboBox)editedItem.FindControl("cbopainting");
            RadTextBox txtMaterialtype = (RadTextBox)editedItem.FindControl("txtMaterialtype");
            RadTextBox txtmaterialspecification = (RadTextBox)editedItem.FindControl("txtmaterialspecification");
            RadTextBox txtNormalThickness = (RadTextBox)editedItem.FindControl("txtNormalThickness");
            RadTextBox txtConstThickness = (RadTextBox)editedItem.FindControl("txtConstThickness");
            RadNumericTextBox txtmrt = (RadNumericTextBox)editedItem.FindControl("txtmrt");
            RadTextBox txtDesignpressure = (RadTextBox)editedItem.FindControl("txtDesignpressure");
            RadTextBox txtdesigntemp = (RadTextBox)editedItem.FindControl("txtdesigntemp");
            RadTextBox txtoppressure = (RadTextBox)editedItem.FindControl("txtoppressure");
            RadTextBox txtOPtemp = (RadTextBox)editedItem.FindControl("txtOPtemp");
            RadTextBox txtDefaultValue = (RadTextBox)editedItem.FindControl("txtDefaultValue");

            RadComboBox cboMaterialtype = (RadComboBox)editedItem.FindControl("cboMaterialtype");
            //RadComboBox cboinspection = (RadComboBox)editedItem.FindControl("cboinspection");
            TextBox txtinspection = (TextBox)editedItem.FindControl("txtinspection");
            RadNumericTextBox txtexpeCR = (RadNumericTextBox)editedItem.FindControl("txtexpeCR");
            RadComboBox cboinspecEffec = (RadComboBox)editedItem.FindControl("cboinspecEffec");
            RadTextBox txtcorrAllow = (RadTextBox)editedItem.FindControl("txtcorrAllow");
            RadComboBox cboclad = (RadComboBox)editedItem.FindControl("cboclad");
           
            //--------------------------------------------------------
            if (cboProcessArea.SelectedValue.ToString() == "--Select--")
            {
                ShowMessage(35);
                e.Canceled = true;
                return;
            }
            if (string.IsNullOrEmpty(cboEquipmentID.SelectedValue.ToString()))
            {
                ShowMessage(36);
                e.Canceled = true;
                return;
            }
            if (string.IsNullOrEmpty(txtCompNo.Text.ToString()))
            {
                ShowMessage(37);
                e.Canceled = true;
                return;
            }
            if (string.IsNullOrEmpty(txtCompName.Text.ToString()))
            {
                ShowMessage(38);
                e.Canceled = true;
                return;
            }
            if (cboMaterialtype.SelectedValue.ToString() == "None")
            {
                ShowMessage(87);
                e.Canceled = true;
                return;
            }
            if (string.IsNullOrEmpty(txtmrt.Text.ToString()))
            {
                ShowMessage(39);
                e.Canceled = true;
                return;
            }
            if (string.IsNullOrEmpty(txtcorrAllow.Text.ToString()))
            {
                ShowMessage(92);
                e.Canceled = true;
                return;
            }
            
            if (txtinspection.Text.ToString() == "")
            {
                ShowMessage(88);
                e.Canceled = true;
                return;
            }

            if (cboclad.SelectedValue.ToString() == "None")
            {
                ShowMessage(90);
                e.Canceled = true;
                return;
            }
            if (cboinspecEffec.SelectedValue.ToString() == "None")
            {
                ShowMessage(91);
                e.Canceled = true;
                return;
            }
            if (txtNormalThickness.Text.ToString() == "")
            {
                lblStatus.Text = "Enter Normal Thickness:";
                return;
            }
            if (txtDefaultValue.Text.ToString() == "")
            {
                lblStatus.Text = "Enter Default Value";
                return;
            }


            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string compid = "0";
            string strqry = "select CompAutoID,ProcessareaID,EqupID,CompNo FROM Tbl_EquipmentComponentDetails where ProcessareaID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' and EqupID ='" + cboEquipmentID.SelectedValue.ToString().Trim() + "' and CompNo ='" + txtCompNo.Text.ToString().Trim() + "'  and Deleted=0";
            SqlCommand cmd = new SqlCommand(strqry, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                compid = (reader["CompAutoID"].ToString().Trim());
            }
            BusinessTier.DisposeReader(reader);
            if (compid == "0")
            {
                int flg = BusinessTier.Component(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString().Trim()), Convert.ToInt32(cboEquipmentID.SelectedValue.ToString().Trim()), txtCompNo.Text.ToString(), txtCompName.Text.ToString().Trim(), cboinsulated.SelectedItem.Text.ToString(), cbopainting.SelectedItem.Text.ToString(), cboMaterialtype.SelectedItem.Text.ToString(), txtmaterialspecification.Text.ToString(), txtNormalThickness.Text.ToString(), txtConstThickness.Text.ToString(), txtmrt.Text.ToString(), txtDesignpressure.Text.ToString(), txtdesigntemp.Text.ToString(), txtoppressure.Text.ToString(), txtOPtemp.Text.ToString(), txtcorrAllow.Text.ToString(), cboinspecEffec.SelectedValue.ToString(), txtexpeCR.Text.ToString(), Convert.ToInt32(txtinspection.Text.ToString()), cboclad.SelectedValue.ToString(), Convert.ToDouble(txtDefaultValue.Text.ToString()), Convert.ToInt32(Session["sesCompanyID"].ToString()), Convert.ToInt32(Session["sesUserID"].ToString()), "N", 0);
                BusinessTier.DisposeConnection(conn);
                if (flg >= 1)
                {
                    ShowMessage(59);
                    return;
                }
            }
            else
            {
                if (compid != "0")
                {
                    ShowMessage(71);
                    e.Canceled = true;
                    return;
                }
            }
            //InsertLogAuditTrail(Session["sesUserID"].ToString(), "component", "Insert", "Success", "Log");
        }
        catch (Exception ex)
        {

            ShowMessage(4);
          //  e.Canceled = true;
            // InsertLogAuditTrail(Session["sesUserID"].ToString(), "component", "Insert", ex.ToString(), "Audit");
        }
        RadGrid1.Rebind();
    }

    //---------------------------------------------------------------------------------------------------------------------
    [System.Web.Services.WebMethod(EnableSession = false)]

    protected void RadGrid1_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            Label lblID = (Label)editedItem.FindControl("lblID");
            Label lblProcessArea = (Label)editedItem.FindControl("lblProcessArea");
            Label lblEquipmentID = (Label)editedItem.FindControl("lblEquipmentID");
            Label lblCompNo = (Label)editedItem.FindControl("lblCompNo");
            RadComboBox cboProcessArea = (RadComboBox)editedItem.FindControl("cboProcessArea");
            RadComboBox cboEquipmentID = (RadComboBox)editedItem.FindControl("cboEquipmentID");
            RadTextBox txtCompNo = (RadTextBox)editedItem.FindControl("txtCompNo");
            RadTextBox txtCompName = (RadTextBox)editedItem.FindControl("txtCompName");
            RadComboBox cboinsulated = (RadComboBox)editedItem.FindControl("cboinsulated");
            RadComboBox cbopainting = (RadComboBox)editedItem.FindControl("cbopainting");
            RadTextBox txtMaterialtype = (RadTextBox)editedItem.FindControl("txtMaterialtype");
            RadTextBox txtmaterialspecification = (RadTextBox)editedItem.FindControl("txtmaterialspecification");
            RadTextBox txtNormalThickness = (RadTextBox)editedItem.FindControl("txtNormalThickness");
            RadTextBox txtConstThickness = (RadTextBox)editedItem.FindControl("txtConstThickness");
            RadNumericTextBox txtmrt = (RadNumericTextBox)editedItem.FindControl("txtmrt");
            RadTextBox txtDesignpressure = (RadTextBox)editedItem.FindControl("txtDesignpressure");
            RadTextBox txtdesigntemp = (RadTextBox)editedItem.FindControl("txtdesigntemp");
            RadTextBox txtoppressure = (RadTextBox)editedItem.FindControl("txtoppressure");
            RadTextBox txtOPtemp = (RadTextBox)editedItem.FindControl("txtOPtemp");
            RadTextBox txtDefaultValue = (RadTextBox)editedItem.FindControl("txtDefaultValue");
            RadComboBox cboMaterialtype = (RadComboBox)editedItem.FindControl("cboMaterialtype");
            TextBox txtinspection = (TextBox)editedItem.FindControl("txtinspection");
           // RadComboBox cboinspection = (RadComboBox)editedItem.FindControl("cboinspection");
            RadNumericTextBox txtexpeCR = (RadNumericTextBox)editedItem.FindControl("txtexpeCR");
            RadComboBox cboinspecEffec = (RadComboBox)editedItem.FindControl("cboinspecEffec");
            RadTextBox txtcorrAllow = (RadTextBox)editedItem.FindControl("txtcorrAllow");
            RadComboBox cboclad = (RadComboBox)editedItem.FindControl("cboclad");
            if (string.IsNullOrEmpty(txtmrt.Text.ToString()))
            {
                ShowMessage(39);
                e.Canceled = true;
                return;
            }
            if (txtNormalThickness.Text.ToString() == "")
            {
                lblStatus.Text = "Enter Normal Thickness:";
                return;
            }
            if (txtDefaultValue.Text.ToString() == "")
            {
                lblStatus.Text = "Enter Default Value";
                return;
            }

            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            ////(((((((((((((((((((((((((cboProcessArea))))))))))))))))))))))))
            int prodid = 0;
            string strqry = " SELECT Processareaid from Tbl_ProcessArea  where processarea='" + cboProcessArea.Text.ToString() + "'";
            SqlCommand cmd = new SqlCommand(strqry, conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                prodid = Convert.ToInt32(rdr["Processareaid"].ToString().Trim());
            }
            BusinessTier.DisposeReader(rdr);
            ////(((((((((((((((((((((((((EquipmentID))))))))))))))))))))))))
            string equpid = "0";
            string strqrye = " SELECT Equautoid from Tbl_EquipmentAsset  where EquPID='" + cboEquipmentID.Text.ToString() + "'";
            SqlCommand cmde = new SqlCommand(strqrye, conn);
            SqlDataReader rdre = cmde.ExecuteReader();
            if (rdre.Read())
            {
                equpid = rdre["Equautoid"].ToString().Trim();
            }
            BusinessTier.DisposeReader(rdre);

            int flg = BusinessTier.Component(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString().Trim()), Convert.ToInt32(cboEquipmentID.SelectedValue.ToString().Trim()), txtCompNo.Text.ToString(), txtCompName.Text.ToString().Trim(), cboinsulated.SelectedItem.Text.ToString(), cbopainting.SelectedItem.Text.ToString(), cboMaterialtype.SelectedItem.Text.ToString(), txtmaterialspecification.Text.ToString(), txtNormalThickness.Text.ToString(), txtConstThickness.Text.ToString(), txtmrt.Text.ToString(), txtDesignpressure.Text.ToString(), txtdesigntemp.Text.ToString(), txtoppressure.Text.ToString(), txtOPtemp.Text.ToString(), txtcorrAllow.Text.ToString(), cboinspecEffec.Text.ToString(), txtexpeCR.Text.ToString(), Convert.ToInt32(txtinspection.Text.ToString()), cboclad.SelectedValue.ToString(), Convert.ToDouble(txtDefaultValue.Text.ToString()), Convert.ToInt32(Session["sesCompanyID"].ToString()), Convert.ToInt32(Session["sesUserID"].ToString()), "U", Convert.ToInt32(lblID.Text.ToString())); //, txtcorrAllow.Text.ToString(), cboinspecEffec.SelectedValue.ToString(), txtexpeCR.Text.ToString(), Convert.ToInt32(cboinspection.SelectedValue.ToString()), cboclad.SelectedValue.ToString(),
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(60);
            }
            else
            {
                ShowMessage(58);
                e.Canceled = true;
                return;
            }

            RadGrid1.Rebind();
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Component", "Update", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(5);
            e.Canceled = true;
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "MasterStaff", "Update", ex.ToString(), "Audit");
        }

    }

    //====================================================================================================

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

    //==========================================Staff On ITEM REQ======================================

    protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
    {
        RadToolTipManager1.TargetControls.Clear();
    }

    protected void OnSelectedIndexChangedMaterialtype(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        RadComboBox combobox = (RadComboBox)sender;
        GridEditFormItem editedItem = (GridEditFormItem)combobox.NamingContainer;
        RadComboBox cboMaterialtype = (RadComboBox)editedItem.FindControl("cboMaterialtype");
        RadNumericTextBox txtexpeCR = (RadNumericTextBox)editedItem.FindControl("txtexpeCR");
        try
        {
            if (cboMaterialtype.SelectedItem.Value == "1")
            {
                txtexpeCR.Text = "0.025";
                txtexpeCR.Enabled = false;
            }
            else if (cboMaterialtype.SelectedItem.Value == "2")
            {
                txtexpeCR.Text = "0.01";
                txtexpeCR.Enabled = false;
            }
            else if (cboMaterialtype.SelectedItem.Value == "3")
            {
                txtexpeCR.Text = "";
                txtexpeCR.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            lblStatus.Text = "Err:Select correct Product Item" + ex.Message.ToString();
            return;
        }
    }

    protected void OnSelectedIndexChangedEquipment(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        RadComboBox combobox = (RadComboBox)sender;
        GridEditFormItem editedItem = (GridEditFormItem)combobox.NamingContainer;
        Label lblID = (Label)editedItem.FindControl("lblID");
        RadComboBox cboProcessArea = (RadComboBox)editedItem.FindControl("cboProcessArea");
        RadComboBox cboEquipmentID = (RadComboBox)editedItem.FindControl("cboEquipmentID");
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            string sql1 = "select EquAutoID,EqupType,EqupID from Tbl_EquipmentAsset where ProcessAreaID='" + cboProcessArea.SelectedValue.ToString() + "' and deleted=0";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            cboEquipmentID.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();

                item.Text = row["EqupType"].ToString();
                item.Text = row["EqupID"].ToString();
                item.Value = row["EquAutoID"].ToString();
                string balqty = row["EquAutoID"].ToString();
                if (balqty != "")
                {
                    item.Attributes.Add("EquAutoID", row["EquAutoID"].ToString());
                    item.Attributes.Add("EqupType", row["EqupType"].ToString());
                    item.Attributes.Add("EqupID", row["EqupID"].ToString());
                    // Lbldupdate.Text=(row["IncomingDate"].ToString());

                    cboEquipmentID.Items.Add(item);
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

    //-----------------------------------------------------------------------------

   
}
