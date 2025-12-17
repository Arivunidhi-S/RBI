<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProcessArea.aspx.cs" Inherits="Product" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link rel="shortcut icon" href="Images/BoilerIcon.png" />
    <title>Process Area Master</title>
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
    <style>
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
<body>
    <form id="form1" runat="server">
    <div>
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
                                    <asp:Label class="labelstyle" ID="lblName" runat="server" Font-Size="Large" ForeColor="White" Bold="true" style="text-align: right; vertical-align: middle; font-family: 'Aclonica', serif;
                                        color: #fff; text-shadow: 0px 1px 0px #999, 0px 2px 0px #888, 0px 3px 0px #777, 0px 4px 0px #666, 0px 5px 0px #555, 0px 6px 0px #444, 0px 7px 0px #333, 0px 8px 7px #001135;"/>
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
                                                <telerik:AjaxUpdatedControl ControlID="RadGridProcessArea" />
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
                                            <telerik:TargetInput ControlID="txtProcessarea" />
                                          <%--  <telerik:TargetInput ControlID="txtdescription" />
                                            <telerik:TargetInput ControlID="txtunit" />--%>
                                        </TargetControls>
                                    </telerik:TextBoxSetting>
                                </telerik:RadInputManager>
                                <div class="mycss">
                                    PROCESS AREA MASTER DETAILS
                                </div>
                                <br />
                                <div id="Div10" runat="server" style="width: 1250px; height: 400px; background-color: transparent;
                                    overflow: auto;" align="center">
                                    <telerik:RadGrid ID="RadGridProcessArea" runat="server" Width="90%" AllowMultiRowEdit="false"
                                        Skin="Telerik" OnNeedDataSource="RadGrid1_NeedDataSource" GridLines="Vertical"
                                        AllowPaging="True" OnItemDataBound="RadGrid1_ItemDataBound" OnItemCreated="RadGrid1_ItemCreated"
                                        PageSize="8" PagerStyle-AlwaysVisible="true" PagerStyle-Position="Bottom" OnDeleteCommand="RadGrid1_DeleteCommand"
                                        AllowAutomaticUpdates="false" AllowAutomaticInserts="false" PagerStyle-Mode="NextPrevNumericAndAdvanced"
                                        AllowAutomaticDeletes="false" OnInsertCommand="RadGrid1_InsertCommand" AllowSorting="false"
                                        AllowFilteringByColumn="true" OnUpdateCommand="RadGrid1_UpdateCommand">
                                        <ClientSettings AllowDragToGroup="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true"
                                            EnableRowHoverStyle="true" EnablePostBackOnRowClick="false">
                                            <Resizing AllowColumnResize="true" />
                                            <Selecting AllowRowSelect="true" />
                                        </ClientSettings>
                                        <MasterTableView AutoGenerateColumns="false" DataKeyNames="ProcessAreaID" CommandItemDisplay="Top"
                                            CommandItemSettings-AddNewRecordText="Add New ProcessArea Details" InsertItemPageIndexAction="ShowItemOnFirstPage">
                                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                            <Columns>
                                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                                    <HeaderStyle Width="5%" />
                                                </telerik:GridEditCommandColumn>
                                                <telerik:GridBoundColumn DataField="ProcessAreaID" DataType="System.Int64" HeaderText="ProcessAreaID"
                                                    ReadOnly="True" SortExpression="ProcessAreaID" UniqueName="ProcessAreaID" AllowFiltering="false"
                                                    AllowSorting="false" Visible="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ProcessArea" DataType="System.String" HeaderText="Process Area">
                                                    <HeaderStyle Width="20%" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Description" DataType="System.String" HeaderText="Description"
                                                    SortExpression="Description" UniqueName="Description">
                                                    <HeaderStyle Width="20%" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ProcessUnit" DataType="System.String" HeaderText="Process Unit"
                                                    SortExpression="ProcessUnit" UniqueName="ProcessUnit">
                                                    <HeaderStyle Width="30%" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
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
                                                                <b>
                                                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("ProcessAreaID")%>' />
                                                                </b>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 15%" align="left">
                                                                Process Area :
                                                                <asp:Label ID="Label11" Text="*" runat="server" Style="color: Red; font-size: smaller;
                                                                    width: 1px;" />
                                                            </td>
                                                            <td style="width: 85%" align="left">
                                                                <asp:TextBox Width="200px" ID="txtProcessarea" MaxLength="30" ToolTip="Maximum Length: 30"
                                                                    runat="server" Text='<%# Bind("ProcessArea") %>' />
                                                                <asp:Label ID="lblprocessarea" Text='<%# Bind("ProcessArea") %>' runat="server" Visible="false" />
                                                                <%--<asp:RequiredFieldValidator ID="RFpcode" runat="server" ForeColor="Red" ControlToValidate="txtProductCode"
                                                                    ErrorMessage="Mandatory" SetFocusOnError="True" />--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                Description:
                                                                <asp:Label ID="Label1" Text="*" runat="server" Style="color: Red; font-size: smaller;
                                                                    width: 1px;" />
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox Width="200px" ID="txtdescription" MaxLength="50" ToolTip="Maximum Length: 50"
                                                                    runat="server" Text='<%# Bind("Description") %>' />
                                                                <%-- <asp:RequiredFieldValidator ID="RFPname" runat="server" ForeColor="Red" ControlToValidate="txtProductName"
                                                                    ErrorMessage="Mandatory" SetFocusOnError="True" />--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                Process Unit :
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox Width="200px" ID="txtunit" MaxLength="10" ToolTip="Maximum Length: 10"
                                                                    runat="server" Text='<%# Bind("ProcessUnit") %>' />
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
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
