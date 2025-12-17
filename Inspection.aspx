<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Inspection.aspx.cs" Inherits="Inspection" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="shortcut icon" href="Images/BoilerIcon.png" />
    <title>Inspection Details</title>
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
            if (alert("The Given Date is Lower than Inspection Date")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            //}

            document.forms[0].appendChild(confirm_value);
        }
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
    <form id="form1" method="post" runat="server">
    <input type="hidden" id="confirm_value" name="confirm_value" value="No" />
    <div>
        <table border="0" cellpadding="2" cellspacing="2" style="background-color: transparent"
            width="100%">
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
                                    <asp:Label class="labelstyle" ID="lblStatus" runat="server" Font-Bold="true" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="width: 100%;">
                                <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                                </telerik:RadScriptManager>
                                <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
                                    <AjaxSettings>
                                        <telerik:AjaxSetting AjaxControlID="RadGrid1">
                                            <UpdatedControls>
                                                <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                                                <telerik:AjaxUpdatedControl ControlID="RadInputManager1" />
                                                <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                                <telerik:AjaxUpdatedControl ControlID="cbostaffno" />
                                                <telerik:AjaxUpdatedControl ControlID="RadToolTipManager1" />
                                     <%--           <telerik:AjaxUpdatedControl ControlID="btnlastinspection" />--%>
                                            </UpdatedControls>
                                        </telerik:AjaxSetting>
                                    </AjaxSettings>
                                </telerik:RadAjaxManager>
                                <telerik:RadToolTipManager ID="RadToolTipManager1" OffsetY="-1" HideEvent="ManualClose"
                                    Width="300" Height="305" runat="server" RelativeTo="Element" Position="MiddleRight">
                                </telerik:RadToolTipManager>
                                <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
                                <telerik:RadInputManager ID="RadInputManager1" runat="server">
                                    <telerik:TextBoxSetting BehaviorID="TextBoxBehavior1" InitializeOnClient="false"
                                        Validation-IsRequired="true" ErrorMessage="Mandatory Fields">
                                        <TargetControls>
                                            <%--  <telerik:TargetInput ControlID="cboeqid" />--%>
                                        </TargetControls>
                                    </telerik:TextBoxSetting>
                                </telerik:RadInputManager>
                                <div class="mycss">
                                    INSPECTION COMPONENT DETAILS
                                </div>
                                <br />
                                <div>
                                    <table>
                                        <tr>
                                            <td height="50%" style="font-weight: bold">
                                                Process Area: &nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <div class="box">
                                                    <telerik:RadComboBox ID="cboProcessArea" runat="server" OnSelectedIndexChanged="OnSelectedIndexChanged_cboProcess"
                                                        Width="150px"  EnableLoadOnDemand="true" AppendDataBoundItems="True"
                                                        AutoPostBack="True" EmptyMessage="Select" OnItemsRequested="cboProcessArea_OnItemsRequested"
                                                        Height="160px">
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
                                            <td>&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnlastinspection" runat="server" Text="  Last Inspection Value  "  OnClick="Onclick_btnlastinspection" class="box"/>
                                            </td>
                                            <%-- <td>&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="Button1" runat="server" Text=" Update All "  OnClick="Onclick_btnupdateall" class="box"/>
                                            </td>--%>
                                        </tr>
                                    </table>
                                </div>
                                <br />
                                       <div class="text" style="background: -webkit-linear-gradient(#e09432,#f8b117); width: 1297px; border: 2px solid Black; font-weight:bold; height:25px;
                                                overflow: auto;">
                                    <table width="80%">
                                        <tr align="center">
                                        <td align="right">
                                                Default CR Rate:
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lblDefaultcr" runat="server" Font-Bold="true" ForeColor="DarkViolet" Font-Names="Arial" ></asp:Label>
                                            </td>
                                            <td align="right">
                                                Short CR:
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lblsCR" runat="server" Font-Bold="true" ForeColor="Blue" Font-Names="Arial" Font-Size="Large"></asp:Label>
                                            </td>
                                            <td align="right">
                                                Long CR:
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lbllCR" runat="server" Font-Bold="true" ForeColor="Green" Font-Names="Arial" Font-Size="Large"></asp:Label>
                                            </td>
                                             <td align="right">
                                                uCR:
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lbluCR" runat="server" Font-Bold="true" ForeColor="Crimson" Font-Names="Arial" Font-Size="Large"></asp:Label>
                                            </td>
                                             <td align="right">
                                                Remaining Life:
                                            </td>
                                             <td align="left">
                                                <asp:Label ID="lblRemainLife" runat="server" Font-Bold="true" ForeColor="Teal" Font-Names="Arial" Font-Size="Large"></asp:Label>
                                            </td>
                                             <td align="right">
                                                Current Thickness:
                                            </td>
                                             <td align="left">
                                                <asp:Label ID="lblCurThick" runat="server" Font-Bold="true" ForeColor="Maroon" Font-Names="Arial" Font-Size="Large"></asp:Label>
                                            </td>
                                            <td align="right">
                                               <asp:Button ID="btnRefresh" runat="server" Text="  Refresh  " OnClick="Onclick_btnRefresh"/>
                                            </td>
                                            
                                        </tr>
                                    </table>
                                </div>
                                <div id="Div10" runat="server" style="width: 1300px; background-color: transparent;
                                    overflow: auto;" align="center">
                                    <telerik:RadGrid ID="RadGrid1" runat="server" AllowMultiRowEdit="false" Skin="Telerik"
                                        OnNeedDataSource="RadGrid1_NeedDataSource" GridLines="Vertical" AllowPaging="True"
                                        OnItemCreated="RadGrid1_ItemCreated" PagerStyle-AlwaysVisible="true" PagerStyle-Position="Bottom"
                                        OnDeleteCommand="RadGrid1_DeleteCommand" PageSize="10" AllowAutomaticUpdates="false"
                                        AllowAutomaticInserts="false" OnItemDataBound="RadGrid1_ItemDataBound" PagerStyle-Mode="NextPrevNumericAndAdvanced"
                                        AllowAutomaticDeletes="false" OnInsertCommand="RadGrid1_InsertCommand" AllowSorting="false"
                                        AllowFilteringByColumn="true" OnUpdateCommand="RadGrid1_UpdateCommand">
                                        <ClientSettings AllowDragToGroup="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true"
                                            EnableRowHoverStyle="true" EnablePostBackOnRowClick="false">
                                            <Resizing AllowColumnResize="true" />
                                            <Selecting AllowRowSelect="true" />
                                        </ClientSettings>
                                        <MasterTableView AutoGenerateColumns="false" DataKeyNames="InspecAutoID" CommandItemDisplay="Top"
                                            CommandItemSettings-AddNewRecordText="Add New Inspection Details" InsertItemPageIndexAction="ShowItemOnFirstPage">
                                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                            <Columns>
                                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                                    <HeaderStyle Width="5%" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridEditCommandColumn>
                                                <telerik:GridBoundColumn DataField="InspecAutoID" DataType="System.Int64" HeaderText="InspecAutoID"
                                                    ReadOnly="True" SortExpression="InspecAutoID" UniqueName="InspecAutoID" AllowFiltering="false"
                                                    AllowSorting="false" Visible="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Equptype" DataType="System.String" HeaderText="Equipment"
                                                    SortExpression="Equptype" UniqueName="Equptype" AllowFiltering="false">
                                                    <HeaderStyle Width="14%" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CompName" DataType="System.String" HeaderText="Component Name"
                                                    SortExpression="CompName" UniqueName="CompName" AllowFiltering="false">
                                                    <HeaderStyle Width="12%" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="InspectionPointNo" DataType="System.String" HeaderText="InspecPoint"
                                                    SortExpression="InspectionPointNo" UniqueName="InspectionPointNo" FilterControlWidth="25">
                                                    <HeaderStyle Width="7%" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="InspecDate" DataType="System.DateTime" HeaderText="InspecDate"
                                                    SortExpression="InspecDate" UniqueName="InspecDate" DataFormatString="{0:dd/MMM/yyyy}"  FilterControlWidth="55">
                                                    <%--DataFormatString="{dd/MM/yyyy}"--%>
                                                    <HeaderStyle Width="9%" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ReadingValue" DataType="System.String" HeaderText="InspecValue" ItemStyle-BackColor="LightPink"
                                                    SortExpression="ReadingValue" UniqueName="ReadingValue" AllowFiltering="false" HeaderStyle-ForeColor="Firebrick">
                                                    <HeaderStyle Width="8%" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Previousdate" DataType="System.DateTime" HeaderText="Previous Date"
                                                    SortExpression="Previousdate" UniqueName="Previousdate" DataFormatString="{0:dd/MMM/yyyy}" AllowFiltering="false">
                                                    <HeaderStyle Width="9%" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Previousvalue" DataType="System.String" HeaderText="PreValue"
                                                    SortExpression="Previousvalue" UniqueName="Previousvalue" AllowFiltering="false">
                                                    <HeaderStyle Width="6%" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Initialdate" DataType="System.DateTime" HeaderText="Initial Date"
                                                    SortExpression="Initialdate" UniqueName="Initialdate" DataFormatString="{0:dd/MMM/yyyy}" FilterControlWidth="55">
                                                    <HeaderStyle Width="9%" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Initialvalue" DataType="System.String" HeaderText="Initialvalue"
                                                    SortExpression="Initialvalue" UniqueName="Initialvalue" AllowFiltering="false">
                                                    <HeaderStyle Width="7%" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>  <telerik:GridBoundColumn DataField="RemainingLife" DataType="System.String" HeaderText="RemainLife"
                                                    SortExpression="RemainingLife" UniqueName="RemainingLife" AllowFiltering="false">
                                                    <HeaderStyle Width="7%" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="SCR" DataType="System.String" HeaderText="OriginalSCR" ItemStyle-BackColor="LightGreen"
                                                    SortExpression="SCR" UniqueName="SCR" AllowFiltering="false" HeaderStyle-ForeColor="Blue">
                                                    <HeaderStyle Width="7%" />
                                                    <ItemStyle HorizontalAlign="Center"/>
                                                </telerik:GridBoundColumn>
                                                 <telerik:GridBoundColumn DataField="DSCR" DataType="System.String" HeaderText="DefaultSCR" ItemStyle-BackColor="Khaki"
                                                    SortExpression="DSCR" UniqueName="DSCR" AllowFiltering="false" HeaderStyle-ForeColor="Brown">
                                                    <HeaderStyle Width="7%" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="LCR" DataType="System.String" HeaderText="OriginalLCR" ItemStyle-BackColor="LightGreen"
                                                    SortExpression="LCR" UniqueName="LCR" AllowFiltering="false" HeaderStyle-ForeColor="Blue">
                                                    <HeaderStyle Width="7%" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                   <telerik:GridBoundColumn DataField="DLCR" DataType="System.String" HeaderText="DefaultLCR" ItemStyle-BackColor="Khaki"
                                                    SortExpression="DLCR" UniqueName="DLCR" AllowFiltering="false" HeaderStyle-ForeColor="Brown">
                                                    <HeaderStyle Width="7%" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                              <%--  <telerik:GridBoundColumn DataField="uCr" DataType="System.String" HeaderText="uCr"
                                                    SortExpression="uCr" UniqueName="uCr" AllowFiltering="false">
                                                    <HeaderStyle Width="7%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>--%>
                                                <%-- <telerik:GridBoundColumn DataField="RemainingLife" DataType="System.String" HeaderText="RemainingLife"
                                                    SortExpression="RemainingLife" UniqueName="RemainingLife">
                                                    <HeaderStyle Width="10%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>--%>
                                                <telerik:GridButtonColumn CommandName="Delete" UniqueName="DeleteColumn" ButtonType="ImageButton"
                                                    ConfirmText="Are you sure want to delete?">
                                                    <HeaderStyle Width="5%" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridButtonColumn>
                                            </Columns>
                                            <EditFormSettings EditFormType="Template">
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
                                                                <telerik:RadComboBox ID="cboeqid" runat="server" Height="110px" Width="200px" DataSourceID="SqlDataSourceEq"
                                                                    DataTextField="EquPID" Filter="StartsWith" DropDownWidth="200px" OnSelectedIndexChanged="OnSelectedIndexChangedInspection"
                                                                    DataValueField="EquAutoID" AutoPostBack="true" AppendDataBoundItems="True" Text='<%# Bind("EqupID") %>'>
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="--Select--" Value="--Select--" ForeColor="Silver" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                                <asp:Label ID="lbleqid" Text='<%# Bind("EquPID") %>' runat="server" Visible="false" />
                                                            </td>
                                                            <td align="left">
                                                                <b>Component Name :<asp:Label ID="Label7" Text="*" runat="server" Style="color: Red;
                                                                    font-size: smaller; width: 1px;" /></b>
                                                            </td>
                                                            <td align="left">
                                                                <telerik:RadComboBox ID="cbocompID" MarkFirstMatch="true" Filter="StartsWith" runat="server"
                                                                    Height="110px" Width="200px" DataValueField="compautoid" DataTextField="CompName"
                                                                    Text='<%# Bind("compno") %>' DropDownWidth="300px" EnableLoadOnDemand="true"
                                                                    AppendDataBoundItems="True" EmptyMessage="Select">
                                                                    <HeaderTemplate>
                                                                        <table style="width: 275px; font-size: small" cellspacing="0" cellpadding="0">
                                                                            <tr>
                                                                                <td style="width: 150px;">
                                                                                    ComponentNo &nbsp;&nbsp;
                                                                                </td>
                                                                                <td style="width: 150px;">
                                                                                    Component Name
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
                                                                                <%--  <td style="width: 70px;" align="left">
                                                                                    <%# DataBinder.Eval(Container, "Attributes['CompName']")%>
                                                                                </td>--%>
                                                                                <td style="width: 150px;" align="left">
                                                                                    <%# DataBinder.Eval(Container, "Text")%>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </ItemTemplate>
                                                                </telerik:RadComboBox>
                                                                <%--<asp:Label ID="lblcompid" Text='<%# Bind("ComponentNo") %>' runat="server" Visible="false" />--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <b>Inspection Point No :<asp:Label ID="Label8" Text="*" runat="server" Style="color: Red;
                                                                    font-size: smaller; width: 1px;" /></b>
                                                            </td>
                                                            <td align="left">
                                                                <telerik:RadTextBox Width="200px" Text='<%# Bind("InspectionPointNo") %>' MaxLength="10"
                                                                    ToolTip="Maximum Length: 20" ID="txtInspectionPointNo" runat="server" />
                                                                <%-- <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                                                </telerik:RadNumericTextBox>--%>
                                                            </td>
                                                            <td align="left">
                                                                <b>Inspection Date:
                                                                    <asp:Label ID="Label12" Text="*" runat="server" Style="color: Red; font-size: smaller;
                                                                        width: 1px;" /></b>
                                                            </td>
                                                            <td align="left">
                                                                <telerik:RadDatePicker ID="txtInspdate" runat="server" Width="200px" DateInput-EmptyMessage="Select Inspection Date"
                                                                    AutoPostBack="true" MinDate="01/01/1000" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy"
                                                                    OnSelectedDateChanged="OnSelectedDateChanged_InpectionDate" DbSelectedDate='<%# Bind("inspecdate") %>'>
                                                                    <Calendar ID="Calendar1" runat="server">
                                                                        <SpecialDays>
                                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                                        </SpecialDays>
                                                                    </Calendar>
                                                                </telerik:RadDatePicker>
                                                                <%--<asp:Label ID="lblInspdate" Text='<%# Bind("InspecDate") %>' runat="server" Visible="false" />--%>
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
                                                                <%--<telerik:RadTextBox Width="200px" ID="txtReadingValue" Enabled="true" MaxLength="50"
                                                                    ToolTip="Maximum Length: 50" runat="server" Text='<%# Bind("ReadingValue") %>' />--%>
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
                                  <%--  <asp:SqlDataSource ID="SqlDataSourcePA" runat="server" ConnectionString="<%$ ConnectionStrings:connString %>"--%>
<%--                                        SelectCommand="SELECT [ProcessAreaID],[processarea] FROM [Tbl_ProcessArea] where deleted=0  ORDER BY [processareaid]">--%> <%--and companyid = <%= Session("sesCompanyID").ToString() %>--%>
                                    <%--</asp:SqlDataSource>--%>
                                    <asp:SqlDataSource ID="SqlDataSourceEq" runat="server" ConnectionString="<%$ ConnectionStrings:connString %>"
                                        SelectCommand="SELECT EquPID, [EquAutoID], [Equptype] FROM [Tbl_EquipmentAsset] where deleted=0 ORDER BY [Equautoid]">
                                    </asp:SqlDataSource>
                                </div>
                         
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <%-- <div   align="center">
            <asp:GridView ID="grid1" runat="server" Width="80%" align="center" Visible="true">
            </asp:GridView>
        </div>--%>
    </div>
    </form>
</body>
</html>
