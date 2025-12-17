<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FinancialCOF.aspx.cs" Inherits="FinancialCOF" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="shortcut icon" href="Images/BoilerIcon.png" />
    <title>Financial Consequence</title>
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
                    <telerik:AjaxUpdatedControl ControlID="RadToolTipManager1" />
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
                                  <asp:Label class="labelstyle" ID="lblName" runat="server" Font-Size="Large" ForeColor="White"
                                        Bold="true" Style="text-align: right; vertical-align: middle; font-family: 'Aclonica', serif;
                                        color: #fff; text-shadow: 0px 1px 0px #999, 0px 2px 0px #888, 0px 3px 0px #777, 0px 4px 0px #666, 0px 5px 0px #555, 0px 6px 0px #444, 0px 7px 0px #333, 0px 8px 7px #001135;" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                                <div class="mycss">
                                    FINANCIAL COF DETAILS
                                </div>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
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
                                                        EmptyMessage="Select" DataTextField="CompName">
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
                                <%-- Financial COF --%>
                                <ajaxToolkit:TabContainer runat="server" ID="tab_FinancialCOF" Height="300px" ActiveTabIndex="0"
                                    Font-Bold="true" Width="90%" BackColor="#C0D7E8">
                                    <ajaxToolkit:TabPanel BackColor="#C0D7E8" runat="server" ID="TabPanel1" HeaderText="Financial COF">
                                        <ContentTemplate>
                                            <div class="text" style="background: -webkit-linear-gradient(#e09432,#f8b117); height: 300px;
                                                overflow: auto;">
                                                <br />
                                                <br />
                                                <table width="90%" cellspacing="2">
                                                    <tr>
                                                        <td>
                                                            Select Component:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cboEqualComponent" runat="server" Width="150px" EnableLoadOnDemand="true"
                                                                AppendDataBoundItems="True" AutoPostBack="True" EmptyMessage="Select" OnItemsRequested="cboEqualComponent_OnItemsRequested"
                                                                Height="160px">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:Button ID="btnFinCOFSubmit" runat="server" Text=" Submit " OnClick="btnFinCOFSubmit_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            HCSmall:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Fin_HCSmall" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            HCMedium:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Fin_HCMedium" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            HCLarge:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Fin_HCLarge" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            HCRupture:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Fin_HCRupture" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            OutSmall:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Fin_OutSmall" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            OutMedium:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Fin_OutMedium" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            OutLarge:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Fin_OutLarge" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            OutRupture:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Fin_OutRupture" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Material:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_MatCost" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                AutoPostBack="true" BackColor="LightYellow" EmptyMessage="Select" Height="100px"
                                                                Width="150px" AppendDataBoundItems="True" DataTextField="CompName" OnSelectedIndexChanged="OnSelectedIndexChanged_cbo_MatCost">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Material Cost:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Fin_MatCost" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            FC cmd:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Fin_FCcmd" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            CA cmd:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Fin_CAcmd" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Outage cmd:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Fin_Outagecmd" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            CA Inj:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Fin_CAInj" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            EquipCost $:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Fin_ECostDollar" runat="server" BackColor="Yellow"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            EquipArea:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Fin_EquipArea" runat="server" BackColor="Yellow"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            EquipCost ft^2:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Fin_ECostft2" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            FC affa:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Fin_FCaffa" runat="server" Width="120px" Enabled="false"></asp:TextBox><asp:Button
                                                                ID="btn_Fin_FCaffa" runat="server" Text="FC" Width="30px" OnClick="btn_Fin_FCaffa_Click" />
                                                        </td>
                                                        <td>
                                                            Product Cost:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Fin_ProdCst" runat="server" BackColor="Yellow"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Outage affa:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Fin_Outageaffa" runat="server" Width="120px" Enabled="false"></asp:TextBox><asp:Button
                                                                ID="btn_Fin_Outageaffa" runat="server" Text="FC" Width="30px" OnClick="btn_Fin_Outageaffa_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            FC Prod:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Fin_FCProd" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            PopDensity:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Fin_PopDensity" runat="server" BackColor="Yellow"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Inj Cost:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Fin_injcost" runat="server" BackColor="Yellow"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            FC Env:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Fin_FCEnv" runat="server" BackColor="Yellow"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            FC Inj:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Fin_FCInj" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            SUMP FC:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Fin_SUMPFC" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Category:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Fin_Category" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btn_Fin_Calculate" runat="server" Text=" Calculate " OnClick="btn_Fin_Calculate_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:Button ID="btn_Fin_Save" runat="server" Text="  Save   " OnClick="btn_Fin_Save_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                    <%-- <tr>
                                                        <td colspan="4">
                                                            <asp:RegularExpressionValidator ID="Thinning_Age" runat="server" ErrorMessage="Age Field not support Characters please enter numbers only"
                                                                ControlToValidate="txt_Age" ForeColor="Red" ValidationExpression="^[1-9]\d*(\.\d+)?$" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>--%>
                                                </table>
                                                <br />
                                            </div>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                </ajaxToolkit:TabContainer>
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
