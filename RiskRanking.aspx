<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RiskRanking.aspx.cs" Inherits="RiskRanking" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="shortcut icon" href="Images/BoilerIcon.png" />
    <title>Risk Ranking</title>
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
    <link rel="stylesheet" href="css/menu_core.css" type="text/css" />
    <link rel="stylesheet" href="css/modal.css" type="text/css" />
    <%--Menu Script--%>
    <link type="text/css" rel="stylesheet" href="css/Upper.css" />
    <script type="text/javascript" src="js/Upperjs.js"></script>
    <style type="text/css">
        .box
        {
            box-shadow: 0.2px 0.2px 8px 0.2px;
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
    <%-- <input type="hidden" id="confirm_value" name="confirm_value" value="No" />--%>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadInputManager1" />
                    <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadToolTipManager ID="RadToolTipManager1" OffsetY="-1" HideEvent="ManualClose"
        Width="300" Height="305" runat="server" RelativeTo="Element" Position="MiddleRight">
    </telerik:RadToolTipManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
    <div>
        <table border="0" cellpadding="2" cellspacing="2" width="100%" style="background-color: transparent">
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
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                                <div class="mycss">
                                    RISK RANKING DETAILS
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <br />
                                <table>
                                    <tr>
                                        <td height="50%" style="font-weight: bold">
                                            Process Area: &nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <div class="box">
                                                <telerik:RadComboBox ID="cboProcessArea" runat="server" OnSelectedIndexChanged="OnSelectedIndexChanged_cboProcess"
                                                    Width="150px" EnableLoadOnDemand="true" AppendDataBoundItems="True" AutoPostBack="True"
                                                    EmptyMessage="Select" OnItemsRequested="cboProcessArea_OnItemsRequested" Height="160px">
                                                </telerik:RadComboBox>
                                            </div>
                                        </td>
                                        <td style="font-weight: bold">
                                            &nbsp;&nbsp; Equipment: &nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <div class="box">
                                                <telerik:RadComboBox ID="cboEquipment" runat="server" DropDownWidth="300px" OnSelectedIndexChanged="OnSelectedIndexChanged_cboEquipment"
                                                    DataTextField="EqupType" Width="150px" AppendDataBoundItems="True" Filter="StartsWith"
                                                    AutoPostBack="True" EmptyMessage="Select" Height="300px" MarkFirstMatch="True"
                                                    DataValueField="EquipID">
                                                    <HeaderTemplate>
                                                        <table style="width: 275px; font-size: small" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 150px;">
                                                                    EquipID &nbsp;&nbsp;
                                                                </td>
                                                                <td style="width: 150px;">
                                                                    EquipType
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table style="width: 275px; font-size: small" cellspacing="0" cellpadding="0">
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
                                            </div>
                                        </td>
                                        <td style="font-weight: bold">
                                            &nbsp;&nbsp; Component: &nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <div class="box">
                                                <telerik:RadComboBox ID="cboComponent" runat="server" DropDownWidth="300px" Height="300px"
                                                    DataValueField="CompNo" AutoPostBack="True" Width="150px" AppendDataBoundItems="True"
                                                    EmptyMessage="Select" DataTextField="CompName" OnSelectedIndexChanged="OnSelectedIndexChanged_cboComponent">
                                                    <HeaderTemplate>
                                                        <table style="width: 275px; font-size: small" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 150px;">
                                                                    CompNo &nbsp;&nbsp;
                                                                </td>
                                                                <td style="width: 150px;">
                                                                    CompName
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table style="width: 275px; font-size: small" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 150px;" align="left">
                                                                    <%# DataBinder.Eval(Container, "Attributes['CompNo']")%>
                                                                </td>
                                                                <td style="width: 150px;" align="left">
                                                                    <%# DataBinder.Eval(Container, "Attributes['CompName']")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </telerik:RadComboBox>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
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
                            <td align="center">
                                <div class="text" style="background: -webkit-linear-gradient(#e09432,#f8b117); width: 90%;
                                    overflow: auto; border: 4px solid #ffffff; box-shadow: 3px 5px 6px rgba(0,0,0,0.5);">
                                    <br />
                                    <table width="100%" rules="all" border="1">
                                        <tr style="font-weight: bold; font-size: medium">
                                            <td style="width: 25%; color: #00008B" align="center" colspan="2">
                                                POF
                                            </td>
                                            <td style="width: 25%; color: #00008B" align="center" colspan="2">
                                                COF
                                            </td>
                                            <td style="width: 25%; color: #00008B" align="center" colspan="2">
                                                Financial COF
                                            </td>
                                            <td style="width: 25%; color: #00008B" align="center" colspan="3">
                                                Risk Ranking
                                            </td>
                                        </tr>
                                        <tr style="font-weight: bold;">
                                            <td style="width: 15%; color: #006400" align="center">
                                                Value
                                            </td>
                                            <td style="color: #006400" align="center">
                                                Category
                                            </td>
                                            <td style="width: 15%; color: #006400" align="center">
                                                Value
                                            </td>
                                            <td style="color: #006400" align="center">
                                                Category
                                            </td>
                                            <td style="width: 15%; color: #006400" align="center">
                                                Value
                                            </td>
                                            <td style="color: #006400" align="center">
                                                Category
                                            </td>
                                            <td style="width: 8%; color: #006400" align="center">
                                                COF Risk
                                            </td>
                                            <td style="width: 9%; color: #006400" align="center">
                                                FinCOF Risk
                                            </td>
                                            <td style="color: #006400" align="center">
                                                Choose COF
                                            </td>
                                        </tr>
                                        <tr style="font-weight: bold;">
                                            <td style="width: 15%; color: Maroon" align="center">
                                                <asp:Label runat="server" ID="lbl_RR_POFVal"></asp:Label>
                                            </td>
                                            <td style="color: Maroon" align="center">
                                                <asp:Label runat="server" ID="lbl_RR_POFCate" Visible="false"></asp:Label>
                                                <asp:Label runat="server" ID="lbl_RR_POFCateVisible"></asp:Label>
                                            </td>
                                            <td style="width: 15%; color: Maroon" align="center">
                                                <asp:Label runat="server" ID="lbl_RR_COFVal"></asp:Label>
                                            </td>
                                            <td style="color: Maroon" align="center">
                                                <asp:Label runat="server" ID="lbl_RR_COFCate"></asp:Label>
                                            </td>
                                            <td style="width: 15%; color: Maroon" align="center">
                                                <asp:Label runat="server" ID="lbl_RR_FinCOFVal"></asp:Label>
                                            </td>
                                            <td style="color: Maroon" align="center">
                                                <asp:Label runat="server" ID="lbl_RR_FinCOFCate"></asp:Label>
                                            </td>
                                            <td style="color: Black" align="center">
                                                <asp:Label runat="server" ID="lbl_RR_COFRisk" BorderColor="Black" BorderStyle="Solid"></asp:Label>
                                            </td>
                                            <td style="color: Black" align="center">
                                                <asp:Label runat="server" ID="lbl_RR_FinCOFRisk" BorderColor="Black" BorderStyle="Solid"></asp:Label>
                                            </td>
                                            <td style="width: 15%; color: Maroon" align="center">
                                                <telerik:RadComboBox ID="cbo_RR_ChooseCOF" runat="server" Filter="StartsWith" AutoPostBack="true"
                                                    DropDownWidth="100px" Height="40px" Width="100px" EmptyMessage="Select">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="COF" Value="A" />
                                                        <telerik:RadComboBoxItem Text="Financial COF" Value="B" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                        <tr> 
                                        <td align="center">
                                                <asp:Button ID="btnDelete" runat="server" Text=" Delete " class="box" OnClick="btnDelete_Click"
                                                    Font-Bold="true" />
                                            </td>
                                            <td  colspan="8" align="center">
                                                <asp:Button ID="btn_RiskRanking_Save" runat="server" Text=" Save " class="box" OnClick="btn_RiskRanking_Save_Click"
                                                    Font-Bold="true" />
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                </div>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="font-weight: bold">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btn_RiskRanking_ViewAll" runat="server" Text=" ViewAll " class="box"
                                                OnClick="btn_RiskRanking_ViewAll_Click" Font-Bold="true" />&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            Choose COF:&nbsp;
                                        </td>
                                        <td>
                                            <div class="box">
                                                <telerik:RadComboBox ID="cbo_RiskRanking_Choose" runat="server" Filter="StartsWith"
                                                    DropDownWidth="100px" Height="40px" Width="100px" EmptyMessage="Select">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="COF" Value="A" />
                                                        <telerik:RadComboBoxItem Text="Financial COF" Value="B" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </div>
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;
                                            <asp:Button ID="btn_RiskRanking_Select" runat="server" Text=" Select " class="box"
                                                OnClick="btn_RiskRanking_Select_Click" Font-Bold="true" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <br />
                                <div id="Div10" runat="server" style="width: 90%; background-color: transparent;
                                    box-shadow: 3px 5px 6px rgba(0,0,0,0.5); border: 4px solid #ffffff; overflow: auto;"
                                    align="center">
                                    <telerik:RadGrid ID="RadGrid1" runat="server" AllowMultiRowEdit="false" Skin="Telerik"
                                        OnNeedDataSource="RadGrid1_NeedDataSource" GridLines="Vertical" AllowPaging="True"
                                        PagerStyle-AlwaysVisible="true" PagerStyle-Position="Bottom" PageSize="10" AllowAutomaticUpdates="false"
                                        AllowAutomaticInserts="false" PagerStyle-Mode="NextPrevNumericAndAdvanced" AllowAutomaticDeletes="false"
                                        AllowSorting="false" AllowFilteringByColumn="true">
                                        <ClientSettings AllowDragToGroup="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true"
                                            EnableRowHoverStyle="true" EnablePostBackOnRowClick="false">
                                            <Resizing AllowColumnResize="true" />
                                            <Selecting AllowRowSelect="true" />
                                        </ClientSettings>
                                        <MasterTableView AutoGenerateColumns="false" DataKeyNames="ProcID" InsertItemPageIndexAction="ShowItemOnFirstPage"
                                            Visible="true">
                                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                            <Columns>
                                                <%-- <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                                    <HeaderStyle Width="5%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridEditCommandColumn>--%>
                                                <telerik:GridBoundColumn DataField="ProcID" DataType="System.Int64" HeaderText="ProcessID"
                                                    ReadOnly="True" UniqueName="ProcID" AllowFiltering="false" AllowSorting="false"
                                                    Visible="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="EquID" DataType="System.Int64" HeaderText="EquipmentID"
                                                    UniqueName="EquID" Visible="false">
                                                    <HeaderStyle Width="12%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CompID" DataType="System.Int64" HeaderText="ComponentID"
                                                    UniqueName="CompID" Visible="false">
                                                    <HeaderStyle Width="12%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="EquPID" DataType="System.String" HeaderText="EquipNo"
                                                    UniqueName="EquPID" AllowSorting="false" AllowFiltering="false">
                                                    <HeaderStyle Width="10%" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="EquipType" DataType="System.String" HeaderText="EquipType"
                                                    UniqueName="EquipType" AllowSorting="false" AllowFiltering="false">
                                                    <HeaderStyle Width="10%" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CompName" DataType="System.String" HeaderText="CompName"
                                                    UniqueName="CompName" AllowFiltering="false">
                                                    <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="POFval" DataType="System.String" HeaderText="POFVal"
                                                    UniqueName="POFval" AllowSorting="false" AllowFiltering="false">
                                                    <HeaderStyle Width="12%" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="POFCate" DataType="System.String" HeaderText="POFCategory"
                                                    UniqueName="POFCate" AllowFiltering="false">
                                                    <HeaderStyle Width="8%" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="COFVal" DataType="System.String" HeaderText="COFVal"
                                                    UniqueName="COFVal" AllowFiltering="false">
                                                    <HeaderStyle Width="12%" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="COFCate" DataType="System.String" HeaderText="COFCategory"
                                                    UniqueName="COFCate" AllowFiltering="false">
                                                    <HeaderStyle Width="8%" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FinCOFval" DataType="System.String" HeaderText="FinCOFval"
                                                    UniqueName="FinCOFval" AllowFiltering="false">
                                                    <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FinCOFCate" DataType="System.String" HeaderText="FinCOFCategory"
                                                    UniqueName="FinCOFCate" AllowFiltering="false">
                                                    <HeaderStyle Width="8%" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="COFRisk" DataType="System.String" HeaderText="COFRisk"
                                                    UniqueName="COFRisk" AllowFiltering="false">
                                                    <HeaderStyle Width="8%" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FinCOFRisk" DataType="System.String" HeaderText="FinCOFRisk"
                                                    UniqueName="FinCOFRisk" AllowFiltering="false">
                                                    <HeaderStyle Width="8%" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="SelectRisk" DataType="System.String" HeaderText="SelectedRisk"
                                                    UniqueName="SelectRisk" AllowFiltering="false">
                                                    <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ChooseRisk" DataType="System.String" HeaderText="RiskRanking"
                                                    UniqueName="ChooseRisk" AllowFiltering="false">
                                                    <HeaderStyle Width="8%" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <%--<telerik:GridButtonColumn CommandName="Delete" UniqueName="DeleteColumn" ButtonType="ImageButton"
                                                    ConfirmText="Are you sure want to delete?">
                                                    <HeaderStyle Width="5%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridButtonColumn>--%>
                                            </Columns>
                                            <%--   <EditFormSettings EditFormType="Template">
                                                <EditColumn UniqueName="EditCommandColumn1">
                                                </EditColumn>
                                                <FormTemplate>
                                                    <table cellspacing="2" cellpadding="2" width="100%" border="0" style="height: 200px">
                                                        <tr>
                                                            <td colspan="2" align="left">
                                                                <b>ID:
                                                                    <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Bind("InspecAutoID")%>' />
                                                                </b>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <b>Equipment ID
                                                                    <asp:Label ID="Label11" Text="*" runat="server" Style="color: Red; font-size: smaller;
                                                                        width: 1px;" /></b>
                                                            </td>
                                                            <td align="left">
                                                                  <telerik:RadComboBox ID="cboeqid" runat="server" Height="60px" Width="200px" DataSourceID="SqlDataSourceEq" EmptyMessage="Select"
                                                                    DataTextField="Equptype" Filter="StartsWith" DropDownWidth="200px" OnSelectedIndexChanged="OnSelectedIndexChangedInspection"
                                                                    DataValueField="EquAutoID" AutoPostBack="true" AppendDataBoundItems="True" Text='<%# Bind("EqupID") %>'>
                                                                    <Items>
                                                                     <telerik:RadComboBoxItem Text="--Select--" Value="--Select--" ForeColor="Silver" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                                <asp:Label ID="lbl_EquipmentID" runat="server" />
                                                                <asp:Label ID="lbl_EquipmentID2" runat="server" Visible="false" />
                                                                <asp:Label ID="lbleqid" Text='<%# Bind("EquPID") %>' runat="server" Visible="false" />
                                                            </td>
                                                            <td align="left">
                                                                <b>Component Name :<asp:Label ID="Label7" Text="*" runat="server" Style="color: Red;
                                                                    font-size: smaller; width: 1px;" /></b>
                                                            </td>
                                                            <td align="left">
                                                             <telerik:RadComboBox ID="cbocompID" MarkFirstMatch="true" Filter="StartsWith" runat="server"
                                                                    Height="80px" Width="200px" DataValueField="compautoid" DataTextField="CompName"
                                                                    Text='<%# Bind("compno") %>' DropDownWidth="300px" EnableLoadOnDemand="true"
                                                                    AppendDataBoundItems="True" EmptyMessage="Select">
                                                                    <HeaderTemplate>
                                                                        <table style="width: 250px; font-size: small" cellspacing="0" cellpadding="0">
                                                                            <tr>
                                                                                <td>
                                                                                    ComponentNo 
                                                                                </td><td>&nbsp;&nbsp;</td>
                                                                                <td>
                                                                                    Component Name
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <table style="width: 250px; font-size: small" cellspacing="0" cellpadding="0">
                                                                            <tr>
                                                                                <td>
                                                                                    <%# DataBinder.Eval(Container, "Attributes['CompNo']")%> 
                                                                                </td><td>&nbsp;&nbsp;</td>
                                                                             
                                                                                </td>
                                                                                <td>
                                                                                    <%# DataBinder.Eval(Container, "Text")%>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </ItemTemplate>
                                                                </telerik:RadComboBox>
                                                               
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <b>Inspection Date:
                                                                    <asp:Label ID="Label12" Text="*" runat="server" Style="color: Red; font-size: smaller;
                                                                        width: 1px;" /></b>
                                                            </td>
                                                            <td align="left">
                                                                <telerik:RadDatePicker ID="txtInspdate" runat="server" Width="200px" DateInput-EmptyMessage="Select Inspection Date"
                                                                    MinDate="01/01/1000" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy"
                                                                    DbSelectedDate='<%# Bind("inspecdate") %>'>
                                                                    <Calendar ID="Calendar1" runat="server">
                                                                        <SpecialDays>
                                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                                        </SpecialDays>
                                                                    </Calendar>
                                                                </telerik:RadDatePicker>
                                                              
                                                            </td>
                                                            <td align="left">
                                                                <b>Inspection Point No :<asp:Label ID="Label8" Text="*" runat="server" Style="color: Red;
                                                                    font-size: smaller; width: 1px;" /></b>
                                                            </td>
                                                            <td align="left">
                                                                <telerik:RadNumericTextBox Width="200px" MinValue="0" MaxValue="9999999999999999999999999"
                                                                    DbValue='<%# Bind("InspectionPointNo") %>' MaxLength="10" ToolTip="Maximum Length: 20"
                                                                    ID="txtInspectionPointNo" runat="server">
                                                                    <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                                                </telerik:RadNumericTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <b>Reading Value :</b>
                                                            </td>
                                                            <td align="left">
                                                                <telerik:RadNumericTextBox Width="200px" MinValue="0" MaxValue="9999999999999999999999999"
                                                                    DbValue='<%# Bind("ReadingValue") %>' MaxLength="10" ToolTip="Maximum Length: 20"
                                                                    ID="txtReadingValue" runat="server">
                                                                    <NumberFormat GroupSeparator="" DecimalDigits="2" />
                                                                </telerik:RadNumericTextBox>
                                                             
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
                                            </EditFormSettings>--%>
                                        </MasterTableView>
                                        <PagerStyle Mode="NumericPages"></PagerStyle>
                                    </telerik:RadGrid>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:connString %>"
                                        SelectCommand="SELECT [ProcessAreaID],[processarea] FROM [Tbl_ProcessArea] where deleted=0 ORDER BY [processareaid]">
                                    </asp:SqlDataSource>
                                    <asp:SqlDataSource ID="SqlDataSourceEq" runat="server" ConnectionString="<%$ ConnectionStrings:connString %>"
                                        SelectCommand="SELECT [EquAutoID],[EquPID], [Equptype] FROM [Tbl_EquipmentAsset] where deleted=0 ORDER BY [Equautoid]">
                                    </asp:SqlDataSource>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="width: 100%;">
                                <div>
                                    <asp:SqlDataSource ID="SqlDataSourcePA" runat="server" ConnectionString="<%$ ConnectionStrings:connString %>"
                                        SelectCommand="SELECT [ProcessAreaID],[processarea] FROM [Tbl_ProcessArea] where deleted=0 ORDER BY [processareaid]">
                                    </asp:SqlDataSource>
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
