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
using Telerik.Web.UI.Upload;

public partial class EquipmentMaster : System.Web.UI.Page
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
                SqlConnection connSave = BusinessTier.getConnection();
                connSave.Open();
                int iflg = BusinessTier.InsertLogTable(connSave, Session["sesUserID"].ToString(), "UpdateVal");
                connSave.Close();
                Response.Redirect("Login.aspx");
            }
            lblName.Text = "Hi, " + Session["sesUserName"].ToString();
        }
        catch (Exception ex)
        {
            SqlConnection connSave = BusinessTier.getConnection();
            connSave.Open();
            int iflg = BusinessTier.InsertLogTable(connSave, Session["sesUserID"].ToString(), "UpdateVal");
            connSave.Close();
            Response.Redirect("Login.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(Session["sesUserID"].ToString()))
            {
                SqlConnection connSave = BusinessTier.getConnection();
                connSave.Open();
                int iflg = BusinessTier.InsertLogTable(connSave, Session["sesUserID"].ToString(), "UpdateVal");
                connSave.Close();
                Response.Redirect("Login.aspx");
            }
            else
            {
                lblStatus.Text = "";
            }
        }
        catch (Exception ex)
        {
            SqlConnection connSave = BusinessTier.getConnection();
            connSave.Open();
            int iflg = BusinessTier.InsertLogTable(connSave, Session["sesUserID"].ToString(), "UpdateVal");
            connSave.Close();
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
                RadComboBox cboProcessArea = (RadComboBox)editedItem.FindControl("cboProcessArea");
                TextBox txtequpID = editedItem.FindControl("txtequpID") as TextBox;
                Label lblequpid = editedItem.FindControl("lblequpid") as Label;
                TextBox txtequipmenttype = editedItem.FindControl("txtequipmenttype") as TextBox;
                TextBox txtdescription = editedItem.FindControl("txtdescription") as TextBox;
                TextBox txtdoshno = editedItem.FindControl("txtdoshno") as TextBox;
                TextBox txtPID = editedItem.FindControl("txtPID") as TextBox;
                RadComboBox cboIPLAYER = (RadComboBox)editedItem.FindControl("cboIPLAYER");
                RadComboBox cboIntrusive = (RadComboBox)editedItem.FindControl("cboIntrusive");
                CheckBox chkWTM = (CheckBox)editedItem.FindControl("chkWTM");
                RadComboBox cboyearinatall = (RadComboBox)editedItem.FindControl("cboyearinatall");
                TextBox txthistdescrip = editedItem.FindControl("txthistdescrip") as TextBox;
                TextBox txtinspectech = editedItem.FindControl("txtinspectech") as TextBox;
                TextBox txtinspecscope = editedItem.FindControl("txtinspecscope") as TextBox;
                TextBox txtRBIObserv = editedItem.FindControl("txtRBIObserv") as TextBox;
                TextBox txtDOSHobserv = editedItem.FindControl("txtDOSHobserv") as TextBox;

                //FileUpload EquipUpload = e.Item.FindControl("EquipUpload") as FileUpload;
                ////RadUpload EquipUpload = e.Item.FindControl("EquipUpload") as RadUpload;

                //strfilename = System.IO.Path.GetFileName(EquipUpload.PostedFile.FileName); // file name 
                //if(string.IsNullOrEmpty(strfilename.ToString()))
                //{
                //}
                ////UploadedFile attachment = EquipUpload.UploadedFiles[0];

                ////// create byte array
                ////byte[] attachmentBytes = new byte[attachment.InputStream.Length];

                ////// read attachment into byte array 
                ////attachment.InputStream.Read(attachmentBytes, 0, attachmentBytes.Length);
                //path = EquipUpload.FileBytes;// path

            }
        }
        catch (Exception ex)
        {
            ShowMessage(8);
            e.Canceled = true;
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Equipment Master", "RadGrid1_ItemCreated", ex.ToString(), "Audit");
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
                RadComboBox cboProcessArea = (RadComboBox)editedItem.FindControl("cboProcessArea");
                RadTextBox txtequpID = editedItem.FindControl("txtequpID") as RadTextBox;
                Label lblequpid = editedItem.FindControl("lblequpid") as Label;
                RadTextBox txtequipmenttype = editedItem.FindControl("txtequipmenttype") as RadTextBox;
                RadTextBox txtdescription = editedItem.FindControl("txtdescription") as RadTextBox;
                RadTextBox txtdoshno = editedItem.FindControl("txtdoshno") as RadTextBox;
                RadTextBox txtPID = editedItem.FindControl("txtPID") as RadTextBox;
                RadComboBox cboIPLAYER = (RadComboBox)editedItem.FindControl("cboIPLAYER");
                RadComboBox cboIntrusive = (RadComboBox)editedItem.FindControl("cboIntrusive");
                CheckBox chkWTM = (CheckBox)editedItem.FindControl("chkWTM");
                RadComboBox cboyearinatall = (RadComboBox)editedItem.FindControl("cboyearinatall");
                RadComboBoxItem preselectedItem = new RadComboBoxItem();
                TextBox txthistdescrip = editedItem.FindControl("txthistdescrip") as TextBox;
                TextBox txtinspectech = editedItem.FindControl("txtinspectech") as TextBox;
                TextBox txtinspecscope = editedItem.FindControl("txtinspecscope") as TextBox;
                TextBox txtRBIObserv = editedItem.FindControl("txtRBIObserv") as TextBox;
                TextBox txtDOSHobserv = editedItem.FindControl("txtDOSHobserv") as TextBox;

                ////RadUpload EquipUpload = e.Item.FindControl("EquipUpload") as RadUpload;
                //FileUpload EquipUpload = e.Item.FindControl("EquipUpload") as FileUpload;

                //strfilename = EquipUpload.FileName; // file name 
                ////UploadedFile attachment = EquipUpload.UploadedFiles[0];

                ////// create byte array
                ////byte[] attachmentBytes = new byte[attachment.InputStream.Length];

                ////// read attachment into byte array 
                ////attachment.InputStream.Read(attachmentBytes, 0, attachmentBytes.Length);
                //path = EquipUpload.FileBytes;// path


                for (int i = 1970; i <= 2050; i++)
                {
                    RadComboBoxItem item1 = new RadComboBoxItem();
                    item1.Text = i.ToString();
                    item1.Value = i.ToString();
                    cboyearinatall.Items.Add(item1);
                }
                if (!(string.IsNullOrEmpty(lblID.Text.ToString())))
                {
                    SqlConnection conn = BusinessTier.getConnection();
                    conn.Open();
                    SqlCommand command = new SqlCommand("SELECT * from VW__EquipmentAsset where  EquAutoID = '" + lblID.Text.ToString() + "' ", conn);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        cboIntrusive.SelectedItem.Text = reader["Instructive"].ToString();
                        cboIPLAYER.SelectedItem.Text = reader["IPLayer"].ToString();
                        string wmt = reader["WTM"].ToString();
                        if (wmt == "True")
                        {
                            chkWTM.Checked = true;
                        }
                        //while (reader.Read())
                        //{
                        //    cboProcessArea.SelectedItem.Text = reader["ProcessArea"].ToString();
                        //    cboProcessArea.SelectedItem.Value = reader["ProcessAreaID"].ToString();
                        //    cboIntrusive.SelectedItem.Text = reader["Instructive"].ToString();
                        //    cboIPLAYER.SelectedItem.Text = reader["IPLayer"].ToString();
                        //    cboyearinatall.SelectedItem.Text = reader["YearInstalled"].ToString();
                           
                        //}
                    }
                    BusinessTier.DisposeReader(reader);
                    BusinessTier.DisposeConnection(conn);
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(9);
            e.Canceled = true;
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "EquipmentMaster", "RadGrid1_ItemDataBound", ex.ToString(), "Audit");
        }
    }
    //=================================================================================================
    protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            RadGrid1.DataSource = DataSourceHelper();
        }
        catch (Exception ex)
        {
            ShowMessage(9);
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Incoming", "NeedDataSource", ex.ToString(), "Audit");
        }
    }

    public DataTable DataSourceHelper()
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Close();
        //string sql = "select * from Tbl_EquipmentAsset where deleted=0 order by equautoid asc";
        string sql = "select * from VW__EquipmentAsset where deleted=0 and companyid= '" + Convert.ToInt32(Session["sesCompanyID"].ToString()) + "' order by equautoid asc";
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, conn);
        DataTable g_datatable = new DataTable();
        sqlDataAdapter.Fill(g_datatable);
        BusinessTier.DisposeAdapter(sqlDataAdapter);
        BusinessTier.DisposeConnection(conn);
        return g_datatable;
    }
    //=================================================================================
    protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            int flg = 0;
            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["EquAutoID"].ToString().Trim();
            string dt = Convert.ToString(System.DateTime.Now);
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            flg = BusinessTier.EquipmentMaster(conn, 0, "", "", "", "", "", "", "", "", 0, "", "", "", "", "", 0, "", Convert.ToInt32(Session["sesCompanyID"].ToString()), Convert.ToInt32(Session["sesUserID"].ToString()), "D", Convert.ToInt32(ID.ToString().Trim()));

            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(28);
            }
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "EquipmentMaster", "Delete", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(6);
            e.Canceled = true;
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "EquipmentMaster", "Delete", ex.ToString(), "Audit");
        }
    }
    //*******************************INSERT**********************************************************************************************
    protected void RadGrid1_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            int WTM = 0;
            GridEditableItem editedItem = e.Item as GridEditableItem;
            RadComboBox cboProcessArea = (RadComboBox)editedItem.FindControl("cboProcessArea");
            RadTextBox txtequpID = editedItem.FindControl("txtequpID") as RadTextBox;
            Label lblequpid = editedItem.FindControl("lblequpid") as Label;
            RadTextBox txtequipmenttype = editedItem.FindControl("txtequipmenttype") as RadTextBox;
            RadTextBox txtdescription = editedItem.FindControl("txtdescription") as RadTextBox;
            RadTextBox txtdoshno = editedItem.FindControl("txtdoshno") as RadTextBox;
            RadTextBox txtPID = editedItem.FindControl("txtPID") as RadTextBox;
            RadComboBox cboIPLAYER = (RadComboBox)editedItem.FindControl("cboIPLAYER");
            RadComboBox cboIntrusive = (RadComboBox)editedItem.FindControl("cboIntrusive");
            CheckBox chkWTM = (CheckBox)editedItem.FindControl("chkWTM");
            RadComboBox cboyearinatall = (RadComboBox)editedItem.FindControl("cboyearinatall");
            TextBox txthistdescrip = editedItem.FindControl("txthistdescrip") as TextBox;
            TextBox txtinspectech = editedItem.FindControl("txtinspectech") as TextBox;
            TextBox txtinspecscope = editedItem.FindControl("txtinspecscope") as TextBox;
            TextBox txtRBIObserv = editedItem.FindControl("txtRBIObserv") as TextBox;
            TextBox txtDOSHobserv = editedItem.FindControl("txtDOSHobserv") as TextBox;
            FileUpload EquipUpload = editedItem.FindControl("EquipUpload") as FileUpload;
            TextBox txtDesignCode = editedItem.FindControl("txtDesignCode") as TextBox;

            DateTime IncomingDate;
            IncomingDate = DateTime.Now;
            if (cboProcessArea.SelectedValue.ToString() == "")
            {
                ShowMessage(31);
                e.Canceled = true;
                return;
            }
            if (txtequpID.Text.ToString() == "")
            {
                ShowMessage(70);
                e.Canceled = true;
                return;
            }
            if (txtequipmenttype.Text.ToString() == "")
            {
                ShowMessage(76);
                e.Canceled = true;
                return;
            }
            if (cboyearinatall.SelectedValue.ToString() == "--Select--")
            {
                ShowMessage(77);
                e.Canceled = true;
                return;
            }
            if (chkWTM.Checked == true)
            {
               
                WTM = 1;
            }

            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string processid = "0";
            string EqipID = "0";
            string strqry = "select EquAutoID FROM Tbl_EquipmentAsset where ProcessAreaID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' and equpID='" + txtequpID.Text.ToString().Trim() + "' and   equpType='" + txtequipmenttype.Text.ToString().Trim() + "' and Deleted=0";
            SqlCommand cmd = new SqlCommand(strqry, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                processid = (reader["EquAutoID"].ToString().Trim());
            }
            BusinessTier.DisposeReader(reader);

            //string fileBasePath = Server.MapPath("Files/");
            //string fileName = System.IO.Path.GetFileName(EquipUpload.FileName);
            //EquipUpload.SaveAs(Server.MapPath("Files/" + fileName));
            //string path = fileBasePath + fileName;
            //FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            //BinaryReader br = new BinaryReader(fs);
            //FileInfo fi = new FileInfo(path);
            //byte[] imagedata = br.ReadBytes((int)fi.Length);
            byte imagedata = 0;

            if (processid == "0")
            {
                int flg = BusinessTier.EquipmentMaster(conn, Convert.ToInt32(cboProcessArea.SelectedValue.ToString().Trim()), txtequpID.Text.ToString().Trim(), txtequipmenttype.Text.ToString().Trim(), txtdescription.Text.ToString().Trim(), txtdoshno.Text.ToString().Trim(), txtPID.Text.ToString().Trim(), cboIPLAYER.SelectedItem.Text, cboIntrusive.SelectedItem.Text, cboyearinatall.SelectedItem.Text, Convert.ToInt32(WTM), txthistdescrip.Text.ToString().Trim(), txtinspectech.Text.ToString().Trim(), txtinspecscope.Text.ToString().Trim(), txtRBIObserv.Text.ToString().Trim(), txtDOSHobserv.Text.ToString().Trim(), Convert.ToByte(imagedata.ToString()), txtDesignCode.Text.ToString(), Convert.ToInt32(Session["sesCompanyID"].ToString()), Convert.ToInt32(Session["sesUserID"].ToString()), "N", 0);
                BusinessTier.DisposeConnection(conn);
                if (flg >= 1)
                {
                    ShowMessage(26);
                }
            }
            else
            {
                if (processid != "")
                {
                    ShowMessage(50);
                    e.Canceled = true;
                    return;
                }

            }
            // InsertLogAuditTrail(Session["sesUserID"].ToString(), "EquipmentMaster", "Insert", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(4);
            e.Canceled = true;
            // InsertLogAuditTrail(Session["sesUserID"].ToString(), "EquipmentMaster", "Insert", ex.ToString(), "Audit");
        }
        RadGrid1.Rebind();
    }
    
    //string strfilename;
    //byte[] path;

    //protected void btnPreview_Click(object source, EventArgs e)
    //{
    //    strfilename = EquipUpload.FileName; // file name 
    //    path = EquipUpload.FileBytes; // path

    //}
    //protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    //{
       
    //   if (e.CommandName == "custom")
    //    {
    //       // System.Web.UI.WebControls.FileUpload EquipUpload = e.Item.FindControl("EquipUpload") as System.Web.UI.WebControls.FileUpload;
    //        //RadUpload EquipUpload = e.Item.FindControl("EquipUpload") as RadUpload;
    //        ////btn_Preview();
    //        //UploadedFile attachment = EquipUpload.UploadedFiles[0];

    //        //// create byte array
    //        //byte[] attachmentBytes = new byte[attachment.InputStream.Length];

    //        //// read attachment into byte array 
    //        //attachment.InputStream.Read(attachmentBytes, 0, attachmentBytes.Length);

    //        //strfilename = EquipUpload.UploadedFiles[0].FileName; // file name 
    //        //path = attachmentBytes;// path
    //        //System.Web.UI.WebControls.Image ImagePreview = e.Item.FindControl("ImagePreview") as System.Web.UI.WebControls.Image;
    //        //Session["ImageBytes"] = path;
    //        //ImagePreview.ImageUrl = "~/ImageHandler.ashx";
    //    }
       
    //} 

    private void MessageBox(string msg)
    {
        Label lbl = new Label();
        lbl.Text = "<script language='javascript'>" + Environment.NewLine + "window.alert('" + msg + "')</script>";
        Page.Controls.Add(lbl);
    }
    //****************************UPDATE*********************************************************************************
    protected void RadGrid1_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            int WTM = 0;
            GridEditableItem editedItem = e.Item as GridEditableItem;
            Label lblID = (Label)editedItem.FindControl("lblID");

            Label LblProcessArea = (Label)editedItem.FindControl("LblProcessArea");
            Label lblequpid = (Label)editedItem.FindControl("lblequpid");
            Label lblequipmenttype = (Label)editedItem.FindControl("lblequipmenttype");

            RadComboBox cboProcessArea = (RadComboBox)editedItem.FindControl("cboProcessArea");
            RadTextBox txtequpID = editedItem.FindControl("txtequpID") as RadTextBox;
            RadTextBox txtequipmenttype = editedItem.FindControl("txtequipmenttype") as RadTextBox;
            RadTextBox txtdescription = editedItem.FindControl("txtdescription") as RadTextBox;
            RadTextBox txtdoshno = editedItem.FindControl("txtdoshno") as RadTextBox;
            RadTextBox txtPID = editedItem.FindControl("txtPID") as RadTextBox;
            RadComboBox cboIPLAYER = (RadComboBox)editedItem.FindControl("cboIPLAYER");
            RadComboBox cboIntrusive = (RadComboBox)editedItem.FindControl("cboIntrusive");
            CheckBox chkWTM = (CheckBox)editedItem.FindControl("chkWTM");
            RadComboBox cboyearinatall = (RadComboBox)editedItem.FindControl("cboyearinatall");
            TextBox txthistdescrip = editedItem.FindControl("txthistdescrip") as TextBox;
            TextBox txtinspectech = editedItem.FindControl("txtinspectech") as TextBox;
            TextBox txtinspecscope = editedItem.FindControl("txtinspecscope") as TextBox;
            TextBox txtRBIObserv = editedItem.FindControl("txtRBIObserv") as TextBox;
            TextBox txtDOSHobserv = editedItem.FindControl("txtDOSHobserv") as TextBox;

            FileUpload EquipUpload = editedItem.FindControl("EquipUpload") as FileUpload;
            TextBox txtDesignCode = editedItem.FindControl("txtDesignCode") as TextBox;
            //RadAsyncUpload radAsyncUpload = editedItem["Upload"].FindControl("AsyncUpload1") as RadAsyncUpload;

            if (cboProcessArea.SelectedValue.ToString() == "")
            {
                ShowMessage(31);
                e.Canceled = true;
                return;
            }
            if (txtequpID.Text.ToString() == "")
            {
                ShowMessage(70);
                e.Canceled = true;
                return;
            }
            if (txtequipmenttype.Text.ToString() == "")
            {
                ShowMessage(76);
                e.Canceled = true;
                return;
            }
            if (cboyearinatall.SelectedValue.ToString() == "")
            {
                ShowMessage(77);
                e.Canceled = true;
                return;
            }
            if (chkWTM.Checked == true)
            {
                WTM = 1;
            }


            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();

            int processarea = 0;
            string eqid = "";
            string strqry11 = "SELECT ProcessAreaID from Tbl_ProcessArea where ProcessArea='" + cboProcessArea.Text.ToString() + "'";
            SqlCommand cmd11 = new SqlCommand(strqry11, conn);
            SqlDataReader reader11 = cmd11.ExecuteReader();
            if (reader11.Read())
            {
                processarea = Convert.ToInt32(reader11["ProcessAreaID"].ToString().Trim());
                // balqty = Convert.ToInt32(reader11["balqty"].ToString().Trim());
            }
            BusinessTier.DisposeReader(reader11);

            //string fileBasePath = Server.MapPath("Files/");
            //string fileName = System.IO.Path.GetFileName(EquipUpload.FileName.ToString());
            //EquipUpload.SaveAs(Server.MapPath("Files/" + fileName));
            //string path = fileBasePath + fileName;
            //FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            //BinaryReader br = new BinaryReader(fs);
            //FileInfo fi = new FileInfo(path);
            //byte[] imagedata = br.ReadBytes((int)fi.Length);
            byte imagedata=0;
            //UploadedFile file = radAsyncUpload.UploadedFiles[0];
            //byte[] fileData = new byte[file.InputStream.Length];
            //file.InputStream.Read(fileData, 0, (int)file.InputStream.Length);

            if (cboProcessArea.Text.ToString() != LblProcessArea.Text.ToString() && txtequpID.Text.ToString() != lblequpid.Text.ToString() && txtequipmenttype.Text.ToString() != lblequipmenttype.Text.ToString())
            {
                string strqry = "select EquAutoID FROM Tbl_EquipmentAsset where ProcessAreaID='" + cboProcessArea.SelectedValue.ToString().Trim() + "' and equpID='" + txtequpID.Text.ToString().Trim() + "' and   equpType='" + txtequipmenttype.Text.ToString().Trim() + "' and Deleted=0";
                SqlCommand cmd1 = new SqlCommand(strqry, conn);
                SqlDataReader reader1 = cmd1.ExecuteReader();
                while (reader1.Read())
                {
                    eqid = (reader1["EquAutoID"].ToString().Trim());
                }
                BusinessTier.DisposeReader(reader1);
                if (eqid != "0")
                {
                    ShowMessage(78);
                    e.Canceled = true;
                    return;
                }
            }
            int flg = BusinessTier.EquipmentMaster(conn, Convert.ToInt32(processarea), txtequpID.Text.ToString().Trim(), txtequipmenttype.Text.ToString().Trim(), txtdescription.Text.ToString().Trim(), txtdoshno.Text.ToString().Trim(), txtPID.Text.ToString().Trim(), cboIPLAYER.SelectedItem.Text, cboIntrusive.SelectedItem.Text, cboyearinatall.SelectedItem.Text, Convert.ToInt32(WTM), txthistdescrip.Text.ToString().Trim(), txtinspectech.Text.ToString().Trim(), txtinspecscope.Text.ToString().Trim(), txtRBIObserv.Text.ToString().Trim(), txtDOSHobserv.Text.ToString().Trim(), Convert.ToByte(imagedata.ToString()), txtDesignCode.Text.ToString(), Convert.ToInt32(Session["sesCompanyID"].ToString()), Convert.ToInt32(Session["sesUserID"].ToString()), "U", Convert.ToInt32(lblID.Text.ToString().Trim()));
            if (flg >= 1)
            {
                ShowMessage(27);
            }

            BusinessTier.DisposeConnection(conn);
            //InsertLogAuditTrial is used to insert the action into MasterLog table
            // InsertLogAuditTrail(Session["sesUserID"].ToString(), "Incoming", "Update", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(5);
            e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            // InsertLogAuditTrail(Session["sesUserID"].ToString(), "Incoming", "Update", ex.ToString(), "Audit");
        }
        RadGrid1.Rebind();
    }

    //protected void btnPreview_Click(object sender, EventArgs e)
    //{
    //   string str = EquipUpload.FileName; // file name 
    //   byte[] path = EquipUpload.FileBytes;
    //    Session["ImageBytes"] = path;
    //    ImagePreview.ImageUrl = "~/ImageHandler.ashx";
    //}

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


