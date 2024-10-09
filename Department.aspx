<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Department.aspx.cs" Inherits="Department" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<link rel="shortcut icon" href="Images/BoilerIcon.png" />
    <title>Master Department</title>
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
    <link rel="stylesheet" href="css/menu_core.css" type="text/css" />
    <link rel="stylesheet" href="css/modal.css" type="text/css" />
    <link rel="stylesheet" href="css/styles_green.css" type="text/css" />
    <link rel="stylesheet" href="css/skins/menu_simpleAnimated.css" type="text/css" />
     <%--Menu Script--%>
    <link type="text/css" rel="stylesheet" href="css/Upper.css" />
    <script type="text/javascript" src="js/Upperjs.js"></script>
    <script type="text/javascript">
        history.go(1); /* undo user navigation (ex: IE Back Button) */
    </script>
    <style type="text/css">
        .mycss
{
text-shadow:1px 1px 3px rgba(48,47,46,1);
font-weight:bold;
color:#291517;
letter-spacing:1pt;
word-spacing:0pt;
font-size:25px;
text-align:center;
font-family:times new roman, times, serif;
}
    body {
background-image: url(images/Bg2.jpg); /*You will specify your image path here.*/

-moz-background-size: cover;
-webkit-background-size: cover;
background-size: cover;
background-position: top center !important;
background-repeat: no-repeat !important;
background-attachment: fixed;
}
    </style>
</head>
<body >
    <form id="form1" runat="server">
    <table border="0" cellpadding="2" cellspacing="2" width="100%">
        <tr>
            <td style="border-right: blue thin solid; border-top: blue thin solid; border-left: blue thin solid;
                border-bottom: blue thin solid; border-width: 0px" align="center">
                <table border="0" width="100%">
                     <tr>
                        <td id="Td1" align="center" runat="server" colspan="2">
                            <div id="sse1">
                                    <div id="sses1">
                                        <ul>
                                            <li><a href="RBIHome.aspx">HOME</a></li>
                                            <li><a href="RBIMaster.aspx">MASTER</a></li>
                                            <li><a href="RBITransaction.aspx">TRANSACTION</a></li>
                                            <li><a href="login.aspx">LOGOUT</a></li>
                                        </ul>
                                    </div>
                                </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <div style="height: 20px;">
                                <asp:Label class="labelstyle" ID="lblStatus" runat="server" BackColor="Gray" Font-Bold="true" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 100%;">
                            <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
                            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
                                <AjaxSettings>
                                    <telerik:AjaxSetting AjaxControlID="RadGrid1">
                                        <UpdatedControls>
                                            <telerik:AjaxUpdatedControl ControlID="RadAjaxPanel1" />
                                            <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                                            <telerik:AjaxUpdatedControl ControlID="RadInputManager1" />
                                            <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                        </UpdatedControls>
                                    </telerik:AjaxSetting>
                                </AjaxSettings>
                            </telerik:RadAjaxManager>
                            <telerik:RadInputManager ID="RadInputManager1" runat="server">
                                <telerik:TextBoxSetting BehaviorID="TextBoxBehavior1" InitializeOnClient="false"
                                    Validation-IsRequired="true" ErrorMessage="Mandatory Fields">
                                    <TargetControls>
                                        <%-- <telerik:TargetInput ControlID="txtDeptCode" />
                                        <telerik:TargetInput ControlID="txtDeptName" />--%>
                                    </TargetControls>
                                </telerik:TextBoxSetting>
                            </telerik:RadInputManager>
                            <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" />
                            <div class="mycss">
                                DEPARTMENT MASTER DETAILS
                            </div>
                            <br />
                            <div id="Div10" runat="server" style="width: 1250px; height: 400px; background-color: transparent;
                                overflow: auto;" align="center">
                                <telerik:RadGrid ID="RadGrid1" runat="server" AllowMultiRowEdit="false" Skin="Telerik"
                                    OnNeedDataSource="RadGrid1_NeedDataSource" GridLines="Vertical" AllowPaging="True"
                                    PageSize="10" OnItemDataBound="RadGrid1_ItemDataBound" OnItemCreated="RadGrid1_ItemCreated"
                                    PagerStyle-AlwaysVisible="true" PagerStyle-Position="Bottom" OnDeleteCommand="RadGrid1_DeleteCommand"
                                    AllowAutomaticUpdates="false" AllowAutomaticInserts="true" PagerStyle-Mode="NextPrevNumericAndAdvanced"
                                    AllowAutomaticDeletes="false" OnInsertCommand="RadGrid1_InsertCommand" AllowSorting="false"
                                    AllowFilteringByColumn="true" OnUpdateCommand="RadGrid1_UpdateCommand">

                                    <ClientSettings AllowDragToGroup="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true"
                                        EnableRowHoverStyle="true" EnablePostBackOnRowClick="false">
                                        <Resizing AllowColumnResize="true" />
                                        <Selecting AllowRowSelect="true" />
                                    </ClientSettings>
                                    <MasterTableView AutoGenerateColumns="false" DataKeyNames="DeptId" InsertItemPageIndexAction="ShowItemOnFirstPage"
                                        CommandItemDisplay="Top" CommandItemSettings-AddNewRecordText="Add New Department Details">
                                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                        <Columns>
                                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                                <HeaderStyle Width="5%" />
                                            </telerik:GridEditCommandColumn>
                                            <telerik:GridBoundColumn DataField="DeptId" DataType="System.Int64" HeaderText="ID"
                                                ReadOnly="True" SortExpression="DeptId" UniqueName="DeptId" AllowFiltering="false"
                                                AllowSorting="false" Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CompanyName" DataType="System.String" HeaderText="CompanyName"
                                                ReadOnly="True" SortExpression="CompanyName" UniqueName="CompanyName" AllowFiltering="true"
                                                AllowSorting="false" Visible="true" FilterControlWidth="150">
                                                <HeaderStyle Width="40%" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="DeptCode" DataType="System.String" HeaderText="Department Code"
                                                SortExpression="DeptCode" UniqueName="DeptCode">
                                                <HeaderStyle Width="10%" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="DeptName" DataType="System.String" HeaderText="Department Name"
                                                SortExpression="DeptName" UniqueName="DeptName">
                                                <HeaderStyle Width="40%" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridButtonColumn CommandName="Delete" UniqueName="DeleteColumn" ButtonType="ImageButton"
                                                ConfirmText="Are you sure want to delete?">
                                                <HeaderStyle Width="5%" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </telerik:GridButtonColumn>
                                        </Columns>
                                        <EditFormSettings EditFormType="Template">
                                            <EditColumn UniqueName="EditCommandColumn1">
                                            </EditColumn>
                                            <FormTemplate>
                                                <table cellspacing="5" cellpadding="5" width="100%" border="0">
                                                    <tr>
                                                        <td colspan="2" align="left">
                                                            <b>ID:
                                                                <asp:Label ID="lblID" runat="server" Text='<%# Bind("DeptId")%>' />
                                                            </b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 15%" align="left">
                                                            Company Name:
                                                            <asp:Label ID="Label2" Text="*" runat="server" Style="color: Red; font-size: smaller;
                                                                width: 1px;" />
                                                        </td>
                                                        <td align="left">
                                                            <telerik:RadComboBox ID="cbocompany" runat="server" Height="60px" Width="200px" DataSourceID="SqlDatacompany"
                                                                DataTextField="companyname" DataValueField="companyid" DropDownWidth="300px"
                                                                AppendDataBoundItems="True" Text='<%# Bind("companyName") %>'>
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="--Select--" Value="0" ForeColor="Silver" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                            <asp:RequiredFieldValidator ID="RFcpy" runat="server" ControlToValidate="cbocompany"
                                                                ErrorMessage="Please select Company" SetFocusOnError="True"> 
                                                            </asp:RequiredFieldValidator>
                                                            <asp:Label ID="Lblcmpydummy" Visible="false" runat="server" Text='<%# Bind("companyName")%>' />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 15%" align="left">
                                                            Department Code:
                                                            <asp:Label ID="Label11" Text="*" runat="server" Style="color: Red; font-size: smaller;
                                                                width: 1px;" />
                                                        </td>
                                                        <td style="width: 85%" align="left">
                                                            <asp:TextBox Width="200px" ID="txtDeptCode" MaxLength="20" ToolTip="Maximum Length: 20"
                                                                runat="server" Text='<%# Bind("DeptCode") %>' />
                                                            <asp:Label ID="Lbldeptcode" Visible="false" runat="server" Text='<%# Bind("DeptCode")%>' />
                                                            <asp:RequiredFieldValidator ID="RFdcode" runat="server" ForeColor="Red" ControlToValidate="txtDeptCode"
                                                                ErrorMessage="Mandatory" SetFocusOnError="True" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            Department Name:
                                                            <asp:Label ID="Label1" Text="*" runat="server" Style="color: Red; font-size: smaller;
                                                                width: 1px;" />
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox Width="200px" ID="txtDeptName" MaxLength="150" ToolTip="Maximum Length: 150"
                                                                runat="server" Text='<%# Bind("DeptName") %>' />
                                                            <asp:Label ID="Lbldeptname" Visible="false" runat="server" Text='<%# Bind("DeptName")%>' />
                                                            <asp:RequiredFieldValidator ID="RFdname" runat="server" ForeColor="Red" ControlToValidate="txtDeptName"
                                                                ErrorMessage="Mandatory" SetFocusOnError="True" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="left">
                                                            <asp:Button ID="Button1" runat="server" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                                CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                                            </asp:Button>
                                                            <asp:Button ID="Button2" runat="server" Text="Cancel" CausesValidation="false" CommandName="Cancel">
                                                            </asp:Button>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </FormTemplate>
                                        </EditFormSettings>
                                    </MasterTableView>
                                    <PagerStyle Mode="NumericPages"></PagerStyle>
                                </telerik:RadGrid>
                                <asp:SqlDataSource ID="SqlDatacompany" runat="server" ConnectionString="<%$ ConnectionStrings:connString %>"
                                    SelectCommand="SELECT  [companyid], [companyname] FROM [company] where deleted=0 ORDER BY [companyid]">
                                </asp:SqlDataSource>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
