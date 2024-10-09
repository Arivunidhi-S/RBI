<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Component.aspx.cs" Inherits="PPEIssue" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="shortcut icon" href="Images/BoilerIcon.png" />
    <title>Equipment Component Details</title>
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
    <link rel="stylesheet" href="css/menu_core.css" type="text/css" />
    <link rel="stylesheet" href="css/modal.css" type="text/css" />
    <link rel="stylesheet" href="css/styles_green.css" type="text/css" />
    <link rel="stylesheet" href="css/skins/menu_simpleAnimated.css" type="text/css" />
    <%--Menu Script--%>
    <link type="text/css" rel="stylesheet" href="css/Upper.css" />
    <script type="text/javascript" src="js/Upperjs.js"></script>
    <script type="text/javascript">
        function Confirm() {

            //var confirm_value = document.forms[0].getElementByName("confirm_value");
            //var confirm_value = document.createElement("INPUT");
            //            confirm_value.type = "hidden";
            //            confirm_value.name = "confirm_value";

            //if (sl == "low") {
            if (confirm("Do u want to Proceed?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            //}

            //document.forms[0].appendChild(confirm_value);
        }
    </script>
    <style>
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
    <form id="form1" method="post" runat="server">
    <input type="hidden" id="confirm_value" name="confirm_value" value="No" />
    <%--<cc1:msgBox ID="MsgBox1" runat="server"></cc1:msgBox>--%>
    <div>
        <table border="0" cellpadding="2" cellspacing="2" width="100%">
            <tr>
                <td style="border-right: blue thin solid; border-top: blue thin solid; border-left: blue thin solid;
                    border-bottom: blue thin solid; border-width: 0px" align="center">
                    <table border="0" width="100%" style="background-color: transparent">
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
                                    <asp:Label class="labelstyle" ID="lblName" runat="server" Font-Size="Large" ForeColor="White"
                                        Bold="true" Style="text-align: right; vertical-align: middle; font-family: 'Aclonica', serif;
                                        color: #fff; text-shadow: 0px 1px 0px #999, 0px 2px 0px #888, 0px 3px 0px #777, 0px 4px 0px #666, 0px 5px 0px #555, 0px 6px 0px #444, 0px 7px 0px #333, 0px 8px 7px #001135;" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <div style="height: 20px;">
                                    <asp:Label class="labelstyle" ID="lblStatus" runat="server" Font-Bold="true" />
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
                                                <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                                                <telerik:AjaxUpdatedControl ControlID="RadInputManager1" />
                                                <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                                <telerik:AjaxUpdatedControl ControlID="RadToolTipManager1" />
                                                <telerik:AjaxUpdatedControl ControlID="cboProcessArea" />
                                            </UpdatedControls>
                                        </telerik:AjaxSetting>
                                        <telerik:AjaxSetting AjaxControlID="cboProcessArea">
                                            <UpdatedControls>
                                                <telerik:AjaxUpdatedControl ControlID="cboProcessArea" />
                                            </UpdatedControls>
                                        </telerik:AjaxSetting>
                                    </AjaxSettings>
                                </telerik:RadAjaxManager>
                                <telerik:RadToolTipManager ID="RadToolTipManager1" OffsetY="-1" HideEvent="ManualClose"
                                    Width="300" Height="305" runat="server" RelativeTo="Element" Position="MiddleRight">
                                </telerik:RadToolTipManager>
                                <telerik:RadInputManager ID="RadInputManager1" runat="server">
                                    <telerik:TextBoxSetting BehaviorID="TextBoxBehavior1" InitializeOnClient="false"
                                        Validation-IsRequired="true" ErrorMessage="Mandatory Fields">
                                        <TargetControls>
                                            <telerik:TargetInput ControlID="cboProcessArea" />
                                            <%--  <telerik:TargetInput ControlID="txtdescription" />
                                            <telerik:TargetInput ControlID="txtunit" />--%>
                                        </TargetControls>
                                    </telerik:TextBoxSetting>
                                </telerik:RadInputManager>
                                <div class="mycss">
                                    EQUIPMENT COMPONENT DETAILS
                                </div>
                                <br />
                                <div id="Div10" runat="server" style="width: 1350px; overflow: auto;" align="center">
                                    <telerik:RadGrid ID="RadGrid1" runat="server" AllowMultiRowEdit="false" OnNeedDataSource="RadGrid1_NeedDataSource"
                                        GridLines="Vertical" AllowPaging="True" OnItemCreated="RadGrid1_ItemCreated"
                                        Skin="Telerik" PagerStyle-AlwaysVisible="true" PagerStyle-Position="Bottom" OnDeleteCommand="RadGrid1_DeleteCommand"
                                        OnItemCommand="RadGrid1_ItemCommand" PageSize="15" AllowAutomaticUpdates="false"
                                        AllowAutomaticInserts="false" OnItemDataBound="RadGrid1_ItemDataBound" PagerStyle-Mode="NextPrevNumericAndAdvanced"
                                        AllowAutomaticDeletes="false" OnInsertCommand="RadGrid1_InsertCommand" AllowSorting="false"
                                        AllowFilteringByColumn="true" Width="98%" OnUpdateCommand="RadGrid1_UpdateCommand">
                                        <ClientSettings AllowDragToGroup="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true"
                                            EnableRowHoverStyle="true" EnablePostBackOnRowClick="false">
                                            <Resizing AllowColumnResize="true" />
                                            <Selecting AllowRowSelect="true" />
                                        </ClientSettings>
                                        <MasterTableView AutoGenerateColumns="false" DataKeyNames="CompAutoID" CommandItemDisplay="Top"
                                            CommandItemSettings-AddNewRecordText="Add New Component Details" InsertItemPageIndexAction="ShowItemOnFirstPage">
                                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                            <Columns>
                                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                                    <HeaderStyle Width="5%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridEditCommandColumn>
                                                <telerik:GridBoundColumn DataField="id" DataType="System.Int64" HeaderText="CompAutoID"
                                                    ReadOnly="True" SortExpression="CompAutoID" UniqueName="CompAutoID" AllowFiltering="false"
                                                    AllowSorting="false" Visible="false">
                                                </telerik:GridBoundColumn>
                                                <%-- <telerik:GridBoundColumn DataField="ProcessareaID" DataType="System.String" HeaderText="Process Area"
                                                    SortExpression="ProcessareaID" UniqueName="ProcessareaID">
                                                    <HeaderStyle Width="10%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>--%>
                                                <telerik:GridBoundColumn DataField="EquipID" DataType="System.String" HeaderText="EquipmentID"
                                                    SortExpression="EquipID" UniqueName="EquipID">
                                                    <HeaderStyle Width="15%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CompName" DataType="System.String" HeaderText="Component Name"
                                                    SortExpression="CompName" UniqueName="CompName">
                                                    <HeaderStyle Width="15%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Insulated" DataType="System.String" HeaderText="Insulated"
                                                    SortExpression="Insulated" UniqueName="Insulated" AllowFiltering="false">
                                                    <HeaderStyle Width="10%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <%--<telerik:GridBoundColumn DataField="Painting" DataType="System.String" HeaderText="Painting"
                                                    SortExpression="Painting" UniqueName="Painting">
                                                    <HeaderStyle Width="10%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>--%>
                                                <telerik:GridBoundColumn DataField="Materialtype" DataType="System.String" HeaderText="Materialtype"
                                                    SortExpression="Materialtype" UniqueName="Materialtype">
                                                    <HeaderStyle Width="12%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="materialspecification" DataType="System.String"
                                                    HeaderText="MaterialSpecification" SortExpression="materialspecification" UniqueName="materialspecification">
                                                    <HeaderStyle Width="13%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NormalThickness" DataType="System.String" HeaderText="NormalThickness"
                                                    SortExpression="NormalThickness" UniqueName="NormalThickness" AllowFiltering="false">
                                                    <HeaderStyle Width="11%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ConstThickness" DataType="System.String" HeaderText="ConstThickness"
                                                    SortExpression="ConstThickness" UniqueName="ConstThickness" AllowFiltering="false">
                                                    <HeaderStyle Width="10%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="MRT" DataType="System.String" HeaderText="MRT"
                                                    SortExpression="MRT" UniqueName="MRT" AllowFiltering="false">
                                                    <HeaderStyle Width="10%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ShortCRrate" DataType="System.String" HeaderText="ShortCRrate"
                                                    SortExpression="ShortCRrate" UniqueName="ShortCRrate" AllowFiltering="false">
                                                    <HeaderStyle Width="10%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="LongCRrate" DataType="System.String" HeaderText="LongCRrate"
                                                    SortExpression="LongCRrate" UniqueName="LongCRrate" AllowFiltering="false">
                                                    <HeaderStyle Width="10%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="RemainingLife" DataType="System.String" HeaderText="RemainingLife"
                                                    SortExpression="RemainingLife" UniqueName="RemainingLife" AllowFiltering="false">
                                                    <HeaderStyle Width="10%" />
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
                                                    <table cellspacing="2" cellpadding="2" width="100%" border="0" style="height: 350px;
                                                        background-color: transparent">
                                                        <tr>
                                                            <td colspan="2" align="left">
                                                                <b>ID:
                                                                    <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Bind("CompAutoID")%>' />
                                                                </b>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <b>Process Area:
                                                                    <asp:Label ID="Label11" Text="*" runat="server" Style="color: Red; font-size: smaller;
                                                                        width: 1px;" /></b>
                                                            </td>
                                                            <td align="left">
                                                                <%--DataSourceID="SqlDataSourcePA"--%>
                                                                <%--DataValueField="ProcessAreaID"--%>
                                                                <%--DataTextField="ProcessArea"--%>
                                                                <telerik:RadComboBox ID="cboProcessArea" runat="server" Height="200px" Width="200px" EnableLoadOnDemand="true" EmptyMessage="Select ProcessArea"
                                                                    Filter="StartsWith" OnItemsRequested="cboProcessArea_OnItemsRequested" OnSelectedIndexChanged="OnSelectedIndexChangedEquipment"
                                                                    AutoPostBack="true" AppendDataBoundItems="false" Text='<%# Bind("ProcessareaID") %>'>
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="--Select--" Value="--Select--" ForeColor="Silver" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                                <asp:Label ID="lblProcessArea" Text='<%# Bind("ProcessareaID") %>' runat="server"
                                                                    Visible="false" />
                                                            </td>
                                                            <td align="left">
                                                                <b>Equipment ID:<asp:Label ID="Label7" Text="*" runat="server" Style="color: Red;
                                                                    font-size: smaller; width: 1px;" /></b>
                                                            </td>
                                                            <td align="left">
                                                                <telerik:RadComboBox ID="cboEquipmentID" MarkFirstMatch="true" Filter="StartsWith" 
                                                                    runat="server" Height="300px" Width="200px" DataValueField="EqupID" DataTextField="EqupType"
                                                                    Text='<%# Bind("EqupID") %>' DropDownWidth="310px" EnableLoadOnDemand="true"
                                                                    AppendDataBoundItems="True" EmptyMessage="Select EquipmentID">
                                                                    <HeaderTemplate>
                                                                        <table style="width: 300px; font-size: small" cellspacing="0" cellpadding="0">
                                                                            <tr>
                                                                                <td style="width: 150px;">
                                                                                    EquipID
                                                                                </td>
                                                                                <td style="width: 150px;">
                                                                                    EquipType
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <table style="width: 300px; font-size: small" cellspacing="0" cellpadding="0">
                                                                            <tr>
                                                                                <td style="width: 150px;" align="left">
                                                                                    <%# DataBinder.Eval(Container, "Attributes['EqupID']")%>
                                                                                </td>
                                                                                <td style="width: 150px;" align="left">
                                                                                    <%# DataBinder.Eval(Container, "Attributes['EqupType']")%>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </ItemTemplate>
                                                                </telerik:RadComboBox>
                                                                <asp:Label ID="lblEquipmentID" Text='<%# Bind("EqupID") %>' runat="server" Visible="false" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <b>Component No :
                                                                    <asp:Label ID="Label12" Text="*" runat="server" Style="color: Red; font-size: smaller;
                                                                        width: 1px;" /></b>
                                                            </td>
                                                            <td align="left">
                                                                <telerik:RadTextBox Width="200px" ID="txtCompNo" Enabled="true" MaxLength="100" ToolTip="Maximum Length: 100"
                                                                    runat="server" Text='<%# Bind("CompNo") %>'>
                                                                </telerik:RadTextBox>
                                                                <asp:Label ID="lblCompNo" Text='<%# Bind("CompNo") %>' runat="server" Visible="false" />
                                                            </td>
                                                            <td align="left">
                                                                <b>Component Name :<asp:Label ID="Label8" Text="*" runat="server" Style="color: Red;
                                                                    font-size: smaller; width: 1px;" /></b>
                                                            </td>
                                                            <td align="left">
                                                                <telerik:RadTextBox Width="200px" ID="txtCompName" Enabled="true" MaxLength="50"
                                                                    ToolTip="Maximum Length: 50" runat="server" Text='<%# Bind("CompName") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <b>Material Type:
                                                                    <asp:Label ID="Label2" Text="*" runat="server" Style="color: Red; font-size: smaller;
                                                                        width: 1px;" /></b>
                                                            </td>
                                                            <td align="left">
                                                                <telerik:RadComboBox ID="cboMaterialtype" runat="server" AutoPostBack="true" Filter="StartsWith"
                                                                    OnSelectedIndexChanged="OnSelectedIndexChangedMaterialtype" DropDownWidth="200px"
                                                                    Text='<%# Bind("Materialtype") %>' Height="80px" Width="200px" AppendDataBoundItems="True">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="None" Value="None" />
                                                                        <telerik:RadComboBoxItem Text="Carbon" Value="1" />
                                                                        <telerik:RadComboBoxItem Text="Stainless" Value="2" />
                                                                        <telerik:RadComboBoxItem Text="Others" Value="3" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                                <%--  <telerik:RadTextBox Width="200px" ID="txtMaterialtype" Enabled="true" MaxLength="50"
                                                                    ToolTip="Maximum Length: 50" runat="server" Text='<%# Bind("Materialtype") %>' />--%>
                                                            </td>
                                                            <td align="left">
                                                                <b>Material Specification:</b>
                                                            </td>
                                                            <td align="left">
                                                                <telerik:RadTextBox Width="200px" ID="txtmaterialspecification" Enabled="true" MaxLength="50"
                                                                    ToolTip="Maximum Length: 50" runat="server" Text='<%# Bind("materialspecification") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <b>MRT (mm):
                                                                    <asp:Label ID="Label1" Text="*" runat="server" Style="color: Red; font-size: smaller;
                                                                        width: 1px;" /></b>
                                                            </td>
                                                            <td align="left">
                                                                <telerik:RadNumericTextBox Width="200px" ID="txtmrt" Enabled="true" MaxLength="50"
                                                                    ToolTip="Maximum Length: 50" runat="server" Text='<%# Bind("MRT") %>' MinValue="0"
                                                                    MaxValue="9999999999999999999999999">
                                                                    <NumberFormat GroupSeparator="" DecimalDigits="2" />
                                                                </telerik:RadNumericTextBox>
                                                            </td>
                                                            <td align="left">
                                                                <b>No of Inspection :
                                                                    <asp:Label ID="Label3" Text="*" runat="server" Style="color: Red; font-size: smaller;
                                                                        width: 1px;" /></b>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox Width="200px" ID="txtinspection" Enabled="false" MaxLength="50" ToolTip="Maximum Length: 50"
                                                                    runat="server" Text='<%# Bind("NoofInspection") %>' />
                                                                <%-- <telerik:RadComboBox ID="cboinspection" runat="server" Filter="StartsWith" DropDownWidth="300px"
                                                                    Height="60px" Width="200px"  AppendDataBoundItems="True" Text='<%# Bind("NoofInspection") %>'>
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="None" Value="None" />
                                                                        <telerik:RadComboBoxItem Text="1" Value="1" />
                                                                        <telerik:RadComboBoxItem Text="2" Value="2" />
                                                                        <telerik:RadComboBoxItem Text="3" Value="3" />
                                                                        <telerik:RadComboBoxItem Text="4" Value="4" />
                                                                        <telerik:RadComboBoxItem Text="5" Value="5" />
                                                                        <telerik:RadComboBoxItem Text="6" Value="6" />
                                                                    </Items>
                                                                </telerik:RadComboBox>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <b>Corrosion Allownce :
                                                                    <asp:Label ID="Label5" Text="*" runat="server" Style="color: Red; font-size: smaller;
                                                                        width: 1px;" /></b>
                                                            </td>
                                                            <td align="left">
                                                                <telerik:RadTextBox Width="200px" ID="txtcorrAllow" Enabled="true" MaxLength="50"
                                                                    ToolTip="Maximum Length: 50" runat="server" Text='<%# Bind("CorrosionAllownce") %>' />
                                                            </td>
                                                            <td align="left">
                                                                <b>Clad :
                                                                    <asp:Label ID="Label6" Text="*" runat="server" Style="color: Red; font-size: smaller;
                                                                        width: 1px;" /></b>
                                                            </td>
                                                            <td align="left">
                                                                <telerik:RadComboBox ID="cboclad" runat="server" Filter="StartsWith" DropDownWidth="200px"
                                                                    Height="60px" Width="200px" AppendDataBoundItems="True" Text='<%# Bind("clad") %>'>
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="Clad" Value="Clad" />
                                                                        <telerik:RadComboBoxItem Text="Yes" Value="Yes" />
                                                                        <telerik:RadComboBoxItem Text="No" Value="No" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <b>Expected Rate : </b>
                                                            </td>
                                                            <td align="left">
                                                                <telerik:RadNumericTextBox Width="200px" ID="txtexpeCR" Enabled="true" MaxLength="50"
                                                                    runat="server" Text='<%# Bind("ExpectedRate") %>' MinValue="0" MaxValue="9999999999999999999999999">
                                                                    <NumberFormat GroupSeparator="" DecimalDigits="2" />
                                                                </telerik:RadNumericTextBox>
                                                            </td>
                                                            <td align="left">
                                                                <b>Inspection Effectiveness:
                                                                    <asp:Label ID="Label4" Text="*" runat="server" Style="color: Red; font-size: smaller;
                                                                        width: 1px;" />
                                                                </b>
                                                            </td>
                                                            <td align="left">
                                                                <telerik:RadComboBox ID="cboinspecEffec" runat="server" Filter="StartsWith" DropDownWidth="200px"
                                                                    Height="120px" Width="200px" AppendDataBoundItems="True" Text='<%# Bind("InspectionEffective") %>'>
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="None" Value="None" />
                                                                        <telerik:RadComboBoxItem Text="Highly Effective" Value="Highly Effective" />
                                                                        <telerik:RadComboBoxItem Text="Usually Effective" Value="Usually Effective" />
                                                                        <telerik:RadComboBoxItem Text="Fairly Effective" Value="Fairly Effective" />
                                                                        <telerik:RadComboBoxItem Text="Poorly Effective" Value="Poorly Effective" />
                                                                        <telerik:RadComboBoxItem Text="In Effective" Value="In Effective" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <b>OP Temperature (C): </b>
                                                            </td>
                                                            <td align="left">
                                                                <telerik:RadTextBox Width="200px" ID="txtOPtemp" Enabled="true" MaxLength="50" ToolTip="Maximum Length: 50"
                                                                    runat="server" Text='<%# Bind("OPTemp") %>' />
                                                            </td>
                                                            <td align="left">
                                                                <b>Design Pressure (psi): </b>
                                                            </td>
                                                            <td align="left">
                                                                <telerik:RadTextBox Width="200px" ID="txtDesignpressure" Enabled="true" MaxLength="50"
                                                                    ToolTip="Maximum Length: 50" runat="server" Text='<%# Bind("Designpressure") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <b>Insulated :</b>
                                                            </td>
                                                            <td align="left">
                                                                <telerik:RadComboBox ID="cboinsulated" runat="server" Filter="StartsWith" DropDownWidth="200px"
                                                                    Height="80px" Width="200px" AppendDataBoundItems="True" Text='<%# Bind("Insulated") %>'>
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="None" Value="None" />
                                                                        <telerik:RadComboBoxItem Text="Hot Insulation" Value="Hot Insulation" />
                                                                        <telerik:RadComboBoxItem Text="Cool Insulation" Value="Cool Insulation" />
                                                                        <telerik:RadComboBoxItem Text="Protective Insulation" Value="Protective Insulation" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </td>
                                                            <td align="left">
                                                                <b>Painting :</b>
                                                            </td>
                                                            <td align="left">
                                                                <telerik:RadComboBox ID="cbopainting" runat="server" DropDownWidth="200px" Height="60px"
                                                                    Width="200px" AppendDataBoundItems="True" Text='<%# Bind("Painting") %>'>
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="None" Value="None" />
                                                                        <telerik:RadComboBoxItem Text="Yes" Value="Yes" />
                                                                        <telerik:RadComboBoxItem Text="No" Value="No" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <b>Normal Thickness (mm): </b>
                                                            </td>
                                                            <td align="left">
                                                                <telerik:RadTextBox Width="200px" ID="txtNormalThickness" Enabled="true" MaxLength="50"
                                                                    ToolTip="Maximum Length: 50" runat="server" Text='<%# Bind("NormalThickness") %>' />
                                                            </td>
                                                            <td align="left">
                                                                <b>Const Thickness (mm): </b>
                                                            </td>
                                                            <td align="left">
                                                                <telerik:RadTextBox Width="200px" ID="txtConstThickness" Enabled="true" MaxLength="50"
                                                                    ToolTip="Maximum Length: 50" runat="server" Text='<%# Bind("ConstThickness") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <b>Design Temperature (C): </b>
                                                            </td>
                                                            <td align="left">
                                                                <telerik:RadTextBox Width="200px" ID="txtdesigntemp" Enabled="true" MaxLength="50"
                                                                    ToolTip="Maximum Length: 50" runat="server" Text='<%# Bind("DesignTemp") %>' />
                                                            </td>
                                                            <td align="left">
                                                                <b>OP Pressure (psi): </b>
                                                            </td>
                                                            <td align="left">
                                                                <telerik:RadTextBox Width="200px" ID="txtoppressure" Enabled="true" MaxLength="50"
                                                                    ToolTip="Maximum Length: 50" runat="server" Text='<%# Bind("OPPressure") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <b>Default CR Rate (mm/y): </b>
                                                            </td>
                                                            <td align="left">
                                                                <telerik:RadTextBox Width="200px" ID="txtDefaultValue" Enabled="true" MaxLength="50"
                                                                    ToolTip="Maximum Length: 50" runat="server" Text='<%# Bind("DefaultValue") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4" align="left">
                                                                <asp:Button ID="Button3" runat="server" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
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
                                    <%--<asp:SqlDataSource ID="SqlDataSourcePA" runat="server" ConnectionString="<%$ ConnectionStrings:connString %>"
                                        SelectCommand="SELECT  [processareaid], [processarea] FROM [Tbl_ProcessArea] where deleted=0 ORDER BY [processareaid]">
                                    </asp:SqlDataSource>--%>
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
