<%@ Page Language="C#" AutoEventWireup="true" CodeFile="User.aspx.cs" Inherits="User" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="shortcut icon" href="Images/BoilerIcon.png" />
    <title>Master User</title>
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
    <link rel="stylesheet" href="css/menu_core.css" type="text/css" />
    <link rel="stylesheet" href="css/modal.css" type="text/css" />
    <link rel="stylesheet" href="css/styles_green.css" type="text/css" />
    <link rel="stylesheet" href="css/skins/menu_simpleAnimated.css" type="text/css" />
    <%--Menu Script--%>
    <link type="text/css" rel="stylesheet" href="css/Upper.css" />
    <script type="text/javascript" src="js/Upperjs.js"></script>
    <script language="javascript">
        history.go(1); /* undo user navigation (ex: IE Back Button) */
    </script>
    <style type="text/css">
        .box
        {
            box-shadow: 0.2px 0.2px 8px 0.2px;
        }
        .mycss
        {
            text-shadow: 1px 1px 3px rgba(48,47,46,1);
            font-weight: bold;
            color: #291517;
            letter-spacing: 1pt;
            word-spacing: 0pt;
            font-size: 25px;
            text-align: center;
            font-family: times new roman, times, serif;
        }
        body
        {
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
<body>
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
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 100%;">
                            <div align="center" style="margin-top: -31px;">
                                <table border="0" cellpadding="2" cellspacing="2" width="1000px">
                                    <tr>
                                        <td style="border-right: blue thin solid; border-top: blue thin solid; border-left: blue thin solid;
                                            border-bottom: blue thin solid; border-width: 0px" align="center">
                                            <hr style="visibility: hidden;" />
                                            <hr style="visibility: hidden;" />
                                            <table border="0" width="100%">
                                                <tr>
                                                    <td id="Td2" align="left" runat="server" colspan="2">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="2">
                                                        <div style="height: 20px;">
                                                            <asp:Label class="labelstyle" ID="Label1" runat="server" ForeColor="Red" Font-Bold="true" />
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" style="width: 80%;">
                                                        <telerik:RadScriptManager ID="ScriptManager" runat="server">
                                                        </telerik:RadScriptManager>
                                                        <div class="mycss">
                                                            USER MODULE
                                                        </div>
                                                        <br />
                                                        <asp:Label class="labelstyle" ID="lblStatus" runat="server" ForeColor="Red" Font-Bold="true" />
                                                        <telerik:RadAjaxManager ID="RadAjaxManager2" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
                                                            <AjaxSettings>
                                                                <telerik:AjaxSetting AjaxControlID="RadGrid1">
                                                                    <UpdatedControls>
                                                                        <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                                                                        <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                                                    </UpdatedControls>
                                                                </telerik:AjaxSetting>
                                                            </AjaxSettings>
                                                        </telerik:RadAjaxManager>
                                                        <telerik:RadInputManager ID="RadInputManager1" runat="server">
                                                            <telerik:TextBoxSetting BehaviorID="TextBoxBehavior1" InitializeOnClient="false"
                                                                Validation-IsRequired="true" ErrorMessage="Mandatory Fields">
                                                                <TargetControls>
                                                                    <telerik:TargetInput ControlID="cboCompanyName" />
                                                                    <%--<telerik:TargetInput ControlID="txtequpID" />--%>
                                                                </TargetControls>
                                                            </telerik:TextBoxSetting>
                                                        </telerik:RadInputManager>
                                                        <telerik:RadInputManager ID="RadInputManager2" runat="server">
                                                            <telerik:TextBoxSetting BehaviorID="TextBoxBehavior1" InitializeOnClient="false"
                                                                Validation-IsRequired="true">
                                                            </telerik:TextBoxSetting>
                                                        </telerik:RadInputManager>
                                                        <telerik:RadGrid ID="RadGrid1" runat="server" AllowMultiRowEdit="false" OnNeedDataSource="RadGrid1_NeedDataSource"
                                                            GridLines="Vertical" AllowPaging="True" PagerStyle-AlwaysVisible="true" PagerStyle-Position="Bottom"
                                                            OnDeleteCommand="RadGrid1_DeleteCommand" Skin="Telerik" OnItemCreated="RadGrid1_ItemCreated" OnItemDataBound="RadGrid1_ItemDataBound"
                                                            AllowAutomaticUpdates="true" AllowAutomaticInserts="true" PagerStyle-Mode="NextPrevNumericAndAdvanced"
                                                            AllowAutomaticDeletes="true" OnInsertCommand="RadGrid1_InsertCommand" AllowSorting="true"
                                                            AllowFilteringByColumn="true" OnUpdateCommand="RadGrid1_UpdateCommand">
                                                            <MasterTableView AutoGenerateColumns="false" DataKeyNames="ID" CommandItemDisplay="Top"
                                                                ClientDataKeyNames="ID" CommandItemSettings-AddNewRecordText="Add New User Details"
                                                                InsertItemPageIndexAction="ShowItemOnFirstPage">
                                                                <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                                                <Columns>
                                                                    <telerik:GridEditCommandColumn ButtonType="ImageButton" />
                                                                    <telerik:GridTemplateColumn DataField="ID" DataType="System.Int32" HeaderText="ID"
                                                                        SortExpression="ID" UniqueName="ID" Resizable="true" Visible="false">
                                                                        <ItemTemplate>
                                                                            <%-- <asp:Label runat="server" Visible="true" ID="lbluserid" Text='<%# Eval("ID") %>' />--%>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="7%" />
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridBoundColumn DataField="StaffName" DataType="System.String" HeaderText="Staff Name"
                                                                        ReadOnly="True" SortExpression="StaffName" UniqueName="StaffName" Visible="true">
                                                                        <HeaderStyle Width="25%" />
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Position" DataType="System.String" HeaderText="Designation"
                                                                        ReadOnly="True" SortExpression="Position" UniqueName="Position" AllowSorting="true"
                                                                        AllowFiltering="true" Visible="true">
                                                                        <HeaderStyle Width="25%" />
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="UserID" DataType="System.Int64" HeaderText="User Id"
                                                                        ReadOnly="True" SortExpression="UserID" UniqueName="UserID" AllowSorting="true"
                                                                        Visible="true">
                                                                        <HeaderStyle Width="25%" />
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Password" DataType="System.String" HeaderText="Password"
                                                                        ReadOnly="True" SortExpression="Password" UniqueName="Password" AllowSorting="true"
                                                                        AllowFiltering="true" Visible="true">
                                                                        <HeaderStyle Width="25%" />
                                                                    </telerik:GridBoundColumn>
                                                                     <telerik:GridBoundColumn DataField="CompanyName" DataType="System.String" HeaderText="Company Name"
                                                                        ReadOnly="True" SortExpression="CompanyName" UniqueName="CompanyName" AllowSorting="true"
                                                                        AllowFiltering="true" Visible="true">
                                                                        <HeaderStyle Width="25%" />
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridButtonColumn CommandName="Delete" UniqueName="DeleteColumn" ButtonType="ImageButton"
                                                                        ConfirmText="Are you sure want to delete?" />
                                                                </Columns>
                                                                <EditFormSettings EditFormType="Template">
                                                                    <EditColumn UniqueName="EditCommandColumn1">
                                                                    </EditColumn>
                                                                    <FormTemplate>
                                                                        <table cellspacing="2" cellpadding="1" width="100%" border="0">
                                                                            <tr>
                                                                                <td colspan="4" align="left">
                                                                                    <b>
                                                                                        <%--ID:--%>
                                                                                        <asp:Label ID="lblID" Visible="true" runat="server" Width="20px" Text='<%# Bind("ID")%>' />
                                                                                    </b>
                                                                                </td>
                                                                            </tr>
                                                                            <br />
                                                                            <tr>
                                                                                <td align="left">
                                                                                    StaffName:
                                                                                </td>
                                                                                <td align="left">
                                                                                    <telerik:RadTextBox Width="200px" ID="txtStaffName" MaxLength="50" ToolTip="Maximum Length: 50"
                                                                                        runat="server" Text='<%# Bind("StaffName") %>'>
                                                                                    </telerik:RadTextBox>
                                                                                </td>
                                                                                <td align="left">
                                                                                    Designation:
                                                                                </td>
                                                                                <td align="left">
                                                                                    <telerik:RadTextBox Width="200px" ID="txtDesignation" MaxLength="50" ToolTip="Maximum Length: 50"
                                                                                        runat="server" Text='<%# Bind("Position") %>'>
                                                                                    </telerik:RadTextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left">
                                                                                    UserID:
                                                                                </td>
                                                                                <td align="left">
                                                                                    <telerik:RadTextBox Width="200px" ID="txtUserID" MaxLength="30" ToolTip="Maximum Length: 30"
                                                                                        runat="server" Text='<%# Bind("UserID") %>'>
                                                                                    </telerik:RadTextBox>
                                                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtUserID"
                                                                                        ErrorMessage="Name is required" ForeColor="Red" Text="*"></asp:RequiredFieldValidator>
                                                                                </td>
                                                                                <td align="left">
                                                                                    Password:
                                                                                </td>
                                                                                <td align="left">
                                                                                    <telerik:RadTextBox Width="200px" ID="txtPassword" MaxLength="30" ToolTip="Maximum Length: 30"
                                                                                        runat="server" Text='<%# Bind("Password") %>'>
                                                                                    </telerik:RadTextBox>
                                                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtPassword"
                                                                                        ErrorMessage="Password is required" ForeColor="Red" Text="*"></asp:RequiredFieldValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Company :
                                                                                </td>
                                                                                 <%--DataSourceID="SqlDataSourcePA" DataTextField="CompanyName" DataValueField="CompanyId"--%>
                                                                                <td><telerik:RadComboBox ID="cboCompanyName" runat="server" Height="60px" Width="200px"  DropDownWidth="200px" EmptyMessage="Select" AppendDataBoundItems="True"
                                                                                Text='<%# Bind("Password") %>'>
                                                                                       <Items>
                                                                                        </Items>
                                                                                    </telerik:RadComboBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="4" align="left">
                                                                                    <asp:Button ID="Button1" runat="server" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                                                        CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                                                                    </asp:Button>
                                                                                    <asp:Button ID="Button2" runat="server" Text="Cancel" CausesValidation="false" CommandName="Cancel">
                                                                                    </asp:Button>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <br />
                                                                    </FormTemplate>
                                                                </EditFormSettings>
                                                            </MasterTableView>
                                                            <PagerStyle Mode="NumericPages"></PagerStyle>
                                                        </telerik:RadGrid>
                                                       <%-- <asp:Button runat="server" ID="btnDelivered" CssClass="buttonstyle" Text="Update"
                                                            Visible="false" OnClick="btnDelivered_Click" />--%>
                                                        <asp:SqlDataSource ID="SqlDataSourceCA" runat="server" ConnectionString="<%$ ConnectionStrings:connString %>"
                                                            SelectCommand="SELECT  [CompanyId], [CompanyName] FROM [RBI].[dbo].[Company] where deleted=0 ORDER BY [CompanyId]">
                                                        </asp:SqlDataSource>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
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
