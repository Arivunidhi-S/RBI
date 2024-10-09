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

public partial class User : System.Web.UI.Page
{
    String a = "", b = "";

    public DataTable dtMenuItems = new DataTable();

    public DataTable dtSubMenuItems = new DataTable();

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

    protected void RadGrid1_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditFormItem editedItem = e.Item as GridEditFormItem;
                RadTextBox txtStaffName = (RadTextBox)editedItem.FindControl("txtStaffName");
                RadTextBox txtDesignation = (RadTextBox)editedItem.FindControl("txtDesignation");
                Label lblID = (Label)editedItem.FindControl("lblID");
                RadTextBox txtUserID = (RadTextBox)editedItem.FindControl("txtUserID");
                RadTextBox txtPassword = (RadTextBox)editedItem.FindControl("txtPassword");
                RadComboBox cboCompanyName = (RadComboBox)editedItem.FindControl("cboCompanyName");

            }
        }
        catch (Exception ex)
        {

            InsertLogAuditTrail(Session["sesUserID"].ToString(), "user", "RadGrid1_ItemCreated", ex.ToString(), "Audit");
        }

    }

    protected void RadGrid1_ItemDataBound(object source, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditFormItem editedItem = e.Item as GridEditFormItem;

                RadComboBox cboCompanyName = (RadComboBox)editedItem.FindControl("cboCompanyName");
                Label lblID = (Label)editedItem.FindControl("lblID");

                SqlConnection conn = BusinessTier.getConnection();
                conn.Open();
                string strqry1 = "SELECT [CompanyId], [CompanyName] FROM [RBI].[dbo].[Company] where deleted=0 ORDER BY [CompanyId]";
                SqlCommand cmd11 = new SqlCommand(strqry1, conn);
                SqlDataReader rdr11 = cmd11.ExecuteReader();
                while (rdr11.Read())
                {
                    RadComboBoxItem item = new RadComboBoxItem();
                    item.Text = rdr11["CompanyName"].ToString();
                    item.Value = rdr11["CompanyId"].ToString();
                    cboCompanyName.Items.Add(item);
                    item.DataBind();
                }
                BusinessTier.DisposeReader(rdr11);

                if (!(string.IsNullOrEmpty(lblID.Text.ToString())))
                {
                    string strqry = "SELECT [Company], [CompanyName] FROM [RBI].[dbo].[UserInfo] where deleted=0 and ID = '" + lblID.Text.ToString() + "' ";
                    SqlCommand cmd1 = new SqlCommand(strqry, conn);
                    SqlDataReader rdr1 = cmd1.ExecuteReader();
                    if (rdr1.Read())
                    {
                        cboCompanyName.Text = rdr1["CompanyName"].ToString().Trim();
                        cboCompanyName.SelectedValue = rdr1["Company"].ToString().Trim();
                    }
                    BusinessTier.DisposeReader(rdr1);
                }

                BusinessTier.DisposeConnection(conn);
            }

        }
        catch (Exception ex)
        {

        }
    }

    protected void RadGrid1_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;

            RadTextBox txtStaffName = (RadTextBox)editedItem.FindControl("txtStaffName");
            RadTextBox txtDesignation = (RadTextBox)editedItem.FindControl("txtDesignation");

            RadTextBox txtUserID = (RadTextBox)editedItem.FindControl("txtUserID");
            RadTextBox txtPassword = (RadTextBox)editedItem.FindControl("txtPassword");

            RadComboBox cboCompanyName = (RadComboBox)editedItem.FindControl("cboCompanyName");

            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            //int flg = BusinessTier.SaveUserDetails(conn, "0", txtName.Text.ToString(), txtPass.Text.ToString(), cboGroup.SelectedItem.Text.ToString(), txtEmail.Text.ToString(), "", "Insert", cboStatus.SelectedItem.Text.ToString(), cboContact.SelectedItem.Text.ToString(), contact_name.Text.ToString(), contact_no.Text.ToString(), designation.Text.ToString(), department.Text.ToString());
            int flg = BusinessTier.SaveUserDetails(conn, txtStaffName.Text.ToString(), txtDesignation.Text.ToString(), txtUserID.Text.ToString(), txtPassword.Text.ToString(), Convert.ToInt32(cboCompanyName.SelectedValue.ToString().Trim()), cboCompanyName.Text.ToString().Trim(), Session["sesUserID"].ToString(), "Insert", "0");
            BusinessTier.DisposeConnection(conn);

            ShowMessage("User Info Inserted Successfully", "Blue");
            System.Drawing.ColorConverter colConvert = new ColorConverter();
            lblStatus.ForeColor = (System.Drawing.Color)colConvert.ConvertFromString("Blue");
        }
        catch (Exception ex)
        {
            ShowMessage("Can't Perform Insertion", "Red");
            System.Drawing.ColorConverter colConvert = new ColorConverter();
            lblStatus.ForeColor = (System.Drawing.Color)colConvert.ConvertFromString("Red");
            e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Users_tbl", "Insert", ex.ToString(), "Audit");
        }
        RadGrid1.Rebind();
    }

    protected void RadGrid1_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {

            GridEditFormItem editedItem = e.Item as GridEditFormItem;
            RadTextBox txtStaffName = (RadTextBox)editedItem.FindControl("txtStaffName");
            RadTextBox txtDesignation = (RadTextBox)editedItem.FindControl("txtDesignation");
            Label lblID = (Label)editedItem.FindControl("lblID");
            RadTextBox txtUserID = (RadTextBox)editedItem.FindControl("txtUserID");
            RadTextBox txtPassword = (RadTextBox)editedItem.FindControl("txtPassword");
            RadComboBox cboCompanyName = (RadComboBox)editedItem.FindControl("cboCompanyName");
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            int flg = BusinessTier.SaveUserDetails(conn, txtStaffName.Text.ToString(), txtDesignation.Text.ToString(), txtUserID.Text.ToString(), txtPassword.Text.ToString(), Convert.ToInt32(cboCompanyName.SelectedValue.ToString().Trim()), cboCompanyName.Text.ToString().Trim(), Session["sesUserID"].ToString(), "Update",lblID.Text.ToString());
            BusinessTier.DisposeConnection(conn);

            ShowMessage("User Info Updated Successfully", "Blue");
            System.Drawing.ColorConverter colConvert = new ColorConverter();
            lblStatus.ForeColor = (System.Drawing.Color)colConvert.ConvertFromString("Blue");
        }
        catch (Exception ex)
        {

            ShowMessage("Can't Perform Updation", "Red");
            System.Drawing.ColorConverter colConvert = new ColorConverter();
            lblStatus.ForeColor = (System.Drawing.Color)colConvert.ConvertFromString("Red");
            e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Users_tbl", "Update", ex.ToString(), "Audit");
        }

        RadGrid1.Rebind();
    }

    protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString();
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            int flg = BusinessTier.SaveUserDetails(conn, "", "", "", "", 0, "", "", "Delete", ID.ToString());
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                //lblStatus.Text = "User Data deleted successfully";
                ShowMessage("User Info Deleted Successfully", "Blue");
                System.Drawing.ColorConverter colConvert = new ColorConverter();
                lblStatus.ForeColor = (System.Drawing.Color)colConvert.ConvertFromString("Blue");
            }
        }
        catch (Exception ex)
        {
            ShowMessage("Can't Perform Deletion", "Red");
            System.Drawing.ColorConverter colConvert = new ColorConverter();
            lblStatus.ForeColor = (System.Drawing.Color)colConvert.ConvertFromString("Red");
            e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail("", "Users_tbl", "Delete", ex.ToString(), "Audit");
        }
    }

    public DataTable DataSourceHelper()
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        // SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select user_aid,user_name,user_password,group_aid,user_email,user_active,contact,contact_name,contact_no,designation,department FROM users_tbl WHERE user_active='TRUE' and Deleted=0 order by user_name", conn);
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from UserInfo where deleted=0", conn);


        DataTable g_datatable = new DataTable();
        sqlDataAdapter.Fill(g_datatable);
        BusinessTier.DisposeAdapter(sqlDataAdapter);
        BusinessTier.DisposeConnection(conn);
        return g_datatable;
    }

    protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        try
        {
            RadGrid1.DataSource = DataSourceHelper();
        }
        catch (Exception ex)
        {
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Users_tbl", "NeedDataSource", ex.ToString(), "Audit");
        }
    }

    private void ShowMessage(string message, string color)
    {
        lblStatus.Text = message.ToString();
        System.Drawing.ColorConverter colConvert = new ColorConverter();
        string strColor = color.ToString();
        lblStatus.ForeColor = (System.Drawing.Color)colConvert.ConvertFromString(strColor);
    }

    private void InsertLogAuditTrail(string userid, string module, string activity, string result, string flag)
    {
        SqlConnection connLog = BusinessTier.getConnection();
        connLog.Open();
        BusinessTier.InsertLogAuditTrial(connLog, userid, module, activity, result, flag);
        BusinessTier.DisposeConnection(connLog);
    }

    protected void btnDelivered_Click(object sender, EventArgs e)
    {
        try
        {


            foreach (GridDataItem Item in RadGrid1.Items)
            {
                CheckBox ChkSelect = (CheckBox)Item.FindControl("ChkSelect");

                if (ChkSelect.Checked)
                {
                    // strSectorDKey = item.GetDataKeyValue("Sector_ID").ToString();
                    Label lbluserid = Item.FindControl("lbluserid") as Label;
                    string struserid = lbluserid.Text.ToString().Trim();

                    SqlConnection conn = BusinessTier.getConnection();
                    conn.Open();

                    string strupdate = "UPDATE [Users_tbl] SET [Rpt_flag]=1 WHERE [user_aid] = '" + struserid + "'";
                    SqlCommand cmd = new SqlCommand(strupdate, conn);
                    cmd.ExecuteNonQuery();
                    BusinessTier.DisposeConnection(conn);
                    ShowMessage("Updated Successfully", "Yellow");
                    // RadGrid1.Rebind();
                }
                else
                {
                    Label lbluserid = Item.FindControl("lbluserid") as Label;
                    string struserid = lbluserid.Text.ToString().Trim();

                    SqlConnection conn = BusinessTier.getConnection();
                    conn.Open();

                    string strupdate = "UPDATE [Users_tbl] SET [Rpt_flag]=0 WHERE [user_aid] = '" + struserid + "'";
                    SqlCommand cmd = new SqlCommand(strupdate, conn);
                    cmd.ExecuteNonQuery();
                    BusinessTier.DisposeConnection(conn);
                    ShowMessage("Updated Successfully", "Yellow");
                    // RadGrid1.Rebind();
                    // lblStatus.Text = "Please Select the CheckBox";
                }
            }

            RadGrid1.Rebind();
        }

        catch (Exception ex)
        {
            //InsertLogAuditTrail(Session["sesUserID"].ToString(), "ScheWaste_Approve", "btnSchWasteApprove_Click", ex.ToString(), "Audit");
            // ShowMessage(8);
        }
    }
}