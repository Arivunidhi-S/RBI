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

public partial class Product : System.Web.UI.Page
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

    //=====================================

    //Data Source  =========================================================================

    protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            RadGridProcessArea.DataSource = DataSourceHelper();
        }
        catch (Exception ex)
        {
            ShowMessage(9);
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "productmaster", "NeedDataSource", ex.ToString(), "Audit");
        }
    }

    public DataTable DataSourceHelper()
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Close();
        string sql = "SELECT * from Tbl_ProcessArea where deleted=0  and companyid= '" + Convert.ToInt32(Session["sesCompanyID"].ToString()) + "' order by Processareaid Asc";
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, conn);
        DataTable g_datatable = new DataTable();
        sqlDataAdapter.Fill(g_datatable);
        BusinessTier.DisposeAdapter(sqlDataAdapter);
        BusinessTier.DisposeConnection(conn);
        return g_datatable;
    }

    //==========================================================

    protected void RadGrid1_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditFormItem editedItem = e.Item as GridEditFormItem;
                TextBox txtProcessarea = editedItem.FindControl("txtProcessarea") as TextBox;
                TextBox txtdescription = editedItem.FindControl("txtdescription") as TextBox;
                TextBox txtunit = editedItem.FindControl("txtunit") as TextBox;
            }
        }
        catch (Exception ex)
        {
            ShowMessage(8);
            e.Canceled = true;
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "ProductMaster", "RadGrid1_ItemCreated", ex.ToString(), "Audit");
        }
    }

    protected void RadGrid1_ItemDataBound(object source, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditFormItem editedItem = e.Item as GridEditFormItem;
                Label lblID = (Label)editedItem.FindControl("lblId");
                TextBox txtProcessarea = (TextBox)editedItem.FindControl("txtProcessarea");
                TextBox txtdescription = (TextBox)editedItem.FindControl("txtdescription");
                TextBox txtunit = (TextBox)editedItem.FindControl("txtunit");


                //if (!(string.IsNullOrEmpty(lblID.Text.ToString())))
                //{
                //    SqlConnection conn = BusinessTier.getConnection();
                //    conn.Open();
                //    SqlCommand command = new SqlCommand("select id,durationperiod from products where id= '" + lblID.Text.ToString() + "'", conn);
                //    SqlDataReader reader = command.ExecuteReader();
                //    if (reader.Read())
                //    {
                //        cboduration.SelectedItem.Text = reader["DurationPeriod"].ToString();
                //    }
                //    BusinessTier.DisposeReader(reader);

                //    SqlCommand cmd = new SqlCommand("SELECT dbo.Products.ID, dbo.Warehouse.WarehouseName, dbo.Products.Deleted FROM dbo.Products INNER JOIN dbo.Warehouse ON dbo.Products.WarehouseId = dbo.Warehouse.WarehouseId where dbo.Products.Deleted=0 and dbo.Products.ID= '" + lblID.Text.ToString() + "'", conn);
                //    SqlDataReader reader1 = cmd.ExecuteReader();
                //    if (reader1.Read())
                //    {
                //        cbowarehouse.SelectedItem.Text = reader1["WarehouseName"].ToString();
                //    }
                //    BusinessTier.DisposeReader(reader1);
                //    BusinessTier.DisposeConnection(conn);
                //}
            }
        }
        catch (Exception ex)
        {
            ShowMessage(9);
            e.Canceled = true;
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Product Master", "RadGrid1_ItemDataBound", ex.ToString(), "Audit");
        }
    }

    //==================================================================================

    protected void RadGrid1_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            TextBox txtProcessarea = (TextBox)editedItem.FindControl("txtProcessarea");
            TextBox txtdescription = (TextBox)editedItem.FindControl("txtdescription");
            TextBox txtunit = (TextBox)editedItem.FindControl("txtunit");
            if (txtProcessarea.Text.ToString() == "0")
            {
                ShowMessage(65);
                e.Canceled = true;
                return;
            }
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string processid = "0";
            string processname = "0";
            string strqry = "select ProcessAreaID,processarea FROM Tbl_ProcessArea where processarea='" + txtProcessarea.Text.ToString().Trim() + "' and Deleted=0";
            SqlCommand cmd = new SqlCommand(strqry, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                processid = (reader["ProcessAreaID"].ToString().Trim());
                processname = (reader["processname"].ToString().Trim());
            }
            BusinessTier.DisposeReader(reader);
            if (processid == "0")
            {
                int flg = BusinessTier.SaveProcessarea(conn, txtProcessarea.Text.ToString().Trim(), txtdescription.Text.ToString().Trim(), txtunit.Text.ToString(), Convert.ToInt32(Session["sesCompanyID"].ToString()), Convert.ToInt32(Session["sesUserID"].ToString()), "prodnew", 0);
                BusinessTier.DisposeConnection(conn);
                if (flg >= 1)
                {
                    ShowMessage(53);
                }
            }
            else
            {
                if (processid != "0")
                {
                    ShowMessage(56);
                    e.Canceled = true;
                    return;
                }
                else if (processname != "0")
                {
                    ShowMessage(57);
                    e.Canceled = true;
                    return;
                }
            }
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "ProcessArea Master", "Insert", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(4);
            e.Canceled = true;
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "ProcessArea Master", "Insert", ex.ToString(), "Audit");
        }
        RadGridProcessArea.Rebind();
    }

    //------------------------------------------------------------------------------------

    protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            int flg = 0;
            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ProcessAreaID"].ToString().Trim();
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            flg = BusinessTier.SaveProcessarea(conn, "", "", "", Convert.ToInt32(Session["sesCompanyID"].ToString()), Convert.ToInt32(Session["sesUserID"].ToString()), "ProdDel", Convert.ToInt32(ID.ToString().Trim()));
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(55);
            }
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "ProcessArea Master", "Delete", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(6);
            e.Canceled = true;
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "ProcessArea Master", "Delete", ex.ToString(), "Audit");
        }
    }

    //------------------------------------------------------------------------------------------------------------

    protected void RadGrid1_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            Label lblID = (Label)editedItem.FindControl("lblID");
            TextBox txtProcessarea = editedItem.FindControl("txtProcessarea") as TextBox;
            Label lblprocessarea = (Label)editedItem.FindControl("lblprocessarea");
            TextBox txtdescription = editedItem.FindControl("txtdescription") as TextBox;
            TextBox txtunit = editedItem.FindControl("txtunit") as TextBox;
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string prodname = "0";
            if (txtProcessarea.Text.ToString() == "0")
            {
                ShowMessage(65);
                e.Canceled = true;
                return;
            }
            if (txtProcessarea.Text.ToString() != lblprocessarea.Text.ToString())
            {
                string strqry1 = "select ProcessAreaID FROM Tbl_ProcessArea where ProcessArea='" + txtProcessarea.Text.ToString().Trim() + "'  and Deleted=0";
                SqlCommand cmd1 = new SqlCommand(strqry1, conn);
                SqlDataReader reader1 = cmd1.ExecuteReader();
                while (reader1.Read())
                {
                    prodname = (reader1["ProcessAreaID"].ToString().Trim());
                }
                BusinessTier.DisposeReader(reader1);
                if (prodname != "0")
                {
                    ShowMessage(57);
                    e.Canceled = true;
                    return;
                }
            }
            int flg = BusinessTier.SaveProcessarea(conn, txtProcessarea.Text.ToString().Trim(), txtdescription.Text.ToString().Trim(), txtunit.Text.ToString(), Convert.ToInt32(Session["sesCompanyID"].ToString()), Convert.ToInt32(Session["sesUserID"].ToString()), "prodUpd", Convert.ToInt32(lblID.Text.ToString().Trim()));
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(54);
            }
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "ProcessArea Master", "Update", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(5);
            e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Product Master", "Update", ex.ToString(), "Audit");
        }
        RadGridProcessArea.Rebind();
    }

    //============================================================================================

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