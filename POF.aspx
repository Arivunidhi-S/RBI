<%@ Page Language="C#" AutoEventWireup="true" CodeFile="POF.aspx.cs" Inherits="POF" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="shortcut icon" href="Images/BoilerIcon.png" />
    <title>POF Details</title>
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
                            <td align="center" colspan="2">
                                <div style="height: 20px;">
                                    <asp:Label class="labelstyle" ID="lblStatus" runat="server" Font-Bold="true" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btn_Thinning" runat="server" Text="Thinning" OnClick="btn_Thinning_Click"
                                    Font-Bold="true" class="box" />
                                &nbsp;
                                <asp:Button ID="btn_Lining" runat="server" Text="Component Linings" OnClick="btn_Lining_Click"
                                    Font-Bold="true" class="box" />
                                &nbsp;
                                <asp:Button ID="btn_External" runat="server" Text="External Damage" OnClick="btn_External_Click"
                                    Font-Bold="true" class="box" />
                                &nbsp;
                                <asp:Button ID="btn_SCC" runat="server" Text="Stress Corrosion" OnClick="btn_SCC_Click"
                                    Font-Bold="true" class="box" />
                                &nbsp;
                                <asp:Button ID="btn_HTHA" runat="server" Text="High Temperature" Font-Bold="true"
                                    OnClick="btn_HTHA_Click" class="box" />
                                &nbsp;
                                <asp:Button ID="btn_Mech_Fati" runat="server" Text="Mechanical Fatigue" Font-Bold="true"
                                    OnClick="btn_Mech_Fati_Click" class="box" />
                                &nbsp;
                                <asp:Button ID="btn_Brit_Frac" runat="server" Text="Brittle Facture" Font-Bold="true"
                                    OnClick="btn_Brit_Frac_Click" class="box" />
                                &nbsp;
                                <asp:Button ID="btn_POF_Total" runat="server" Text="POF Total" Font-Bold="true" OnClick="btn_POF_Total_Click"
                                    class="box" />
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
                            <%--  <td>
                                                            <asp:Button ID="Button2" runat="server" Text=" Submit " OnClick="Button2_Click"
                                                                Font-Bold="true" />
                                                        </td>--%>
                        </tr>
                        <tr>
                            <td align="center">
                                <%-- <div  style="font-size: xx-large; color: #f5f5f5;
                            font-family: Algerian; font-weight: lighter; text-shadow: 0px 15px 5px rgba(0,0,0,0.1),
                 10px 20px 5px rgba(0,0,0,0.05),
                 -10px 20px 5px rgba(0,0,0,0.05);">
                                    <br />
                                </div>--%>
                                <br />
                                <%-- Tab Thinning Damage Factor--%>
                                <ajaxToolkit:TabContainer runat="server" ID="tab_Thinning" Height="220px" ActiveTabIndex="0"
                                    Font-Bold="true" Width="70%" BackColor="#C0D7E8">
                                    <ajaxToolkit:TabPanel BackColor="#C0D7E8" runat="server" ID="TabPanel1" HeaderText="Thinning Damage Factor">
                                        <ContentTemplate>
                                            <div class="text" style="background: -webkit-linear-gradient(#e09432,#f8b117); height: 220px;
                                                overflow: auto;">
                                                <br />
                                                <br />
                                                <table width="90%" cellspacing="2">
                                                    <tr>
                                                        <td colspan="3">
                                                            <asp:Button ID="btnThinningSubmit" runat="server" Text=" Submit " OnClick="btnThinningSubmit_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                        <td align="right">
                                                            <asp:Button ID="btnThiningDelete" runat="server" Text=" Delete " OnClick="btnThiningDelete_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Clad Component:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cboclad" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="60px" Width="150px" AppendDataBoundItems="True" DataTextField="CompName">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Inspection Date:
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="cbo_Thin_InspDate" runat="server" Width="150px" DateInput-EmptyMessage="Select Inspection Date"
                                                                AutoPostBack="true" MinDate="01/01/1900" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy"
                                                                OnSelectedDateChanged="InspDate_OnSelectedDateChanged">
                                                                <Calendar ID="Calendar8" runat="server">
                                                                    <SpecialDays>
                                                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                                    </SpecialDays>
                                                                </Calendar>
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Inspection Effectiveness:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_inspecEffec" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="100px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Highly Effective" Value="A" />
                                                                    <telerik:RadComboBoxItem Text="Usually Effective" Value="B" />
                                                                    <telerik:RadComboBoxItem Text="Fairly Effective" Value="C" />
                                                                    <telerik:RadComboBoxItem Text="Poorly Effective" Value="D" />
                                                                    <telerik:RadComboBoxItem Text="In Effective" Value="E" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            No of Inspection:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_Thin_nofIns" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="120px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem runat="server" Owner="" Text="1" Value="1" />
                                                                    <telerik:RadComboBoxItem runat="server" Owner="" Text="2" Value="2" />
                                                                    <telerik:RadComboBoxItem runat="server" Owner="" Text="3" Value="3" />
                                                                    <telerik:RadComboBoxItem runat="server" Owner="" Text="4" Value="4" />
                                                                    <telerik:RadComboBoxItem runat="server" Owner="" Text="5" Value="5" />
                                                                    <telerik:RadComboBoxItem runat="server" Owner="" Text="6" Value="6" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Age:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Age" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Thinning Type:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_thin_type" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="40px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Local" Value="L" />
                                                                    <telerik:RadComboBoxItem Text="General" Value="G" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            A_rt:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_art" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Tdf:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Tdf" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="1">
                                                            <asp:Button ID="btn_Calculate" runat="server" Text=" Calculate " OnClick="Calculate_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:Button ID="btn_ThinningSave" runat="server" Text="  Save   " OnClick="btn_ThinningSave_Click"
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
                                <%-- TabContainer Lining Damage Factor--%>
                                <ajaxToolkit:TabContainer runat="server" ID="tab_Lining" Height="335px" ActiveTabIndex="0"
                                    Width="50%" BackColor="#C0D7E8" Font-Bold="true">
                                    <ajaxToolkit:TabPanel BackColor="#C0D7E8" runat="server" ID="TabPanel2" HeaderText="Component Lining Damage Factor">
                                        <ContentTemplate>
                                            <div class="text" style="background: -webkit-linear-gradient(#e09432,#f8b117); height: 335px;
                                                overflow: auto;">
                                                <table width="60%" cellspacing="8">
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Button ID="btnLiningSubmit" runat="server" Text=" Submit " OnClick="btnLiningSubmit_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Type of Lining:
                                                        </td>
                                                        <td colspan="2">
                                                            <telerik:RadComboBox ID="cbo_Typelining" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="40px" Width="150px" AppendDataBoundItems="True" DataTextField="CompName"
                                                                EmptyMessage="Select" AutoPostBack="True" OnSelectedIndexChanged="OnSelectedIndexChanged_cbo_Typelining">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Organic" Value="Organic" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Inorganic" Value="Inorganic" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lbl_Lineage" Text="Line Age" runat="server"></asp:Label>
                                                        </td>
                                                        <td colspan="2">
                                                            <telerik:RadComboBox ID="cbo_AgeLining" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="80px" Width="150px" AppendDataBoundItems="True">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem runat="server" Owner="" Text="1" Value="1" />
                                                                    <telerik:RadComboBoxItem runat="server" Owner="" Text="2" Value="2" />
                                                                    <telerik:RadComboBoxItem runat="server" Owner="" Text="3" Value="3" />
                                                                    <telerik:RadComboBoxItem runat="server" Owner="" Text="4" Value="4" />
                                                                    <telerik:RadComboBoxItem runat="server" Owner="" Text="5" Value="5" />
                                                                    <telerik:RadComboBoxItem runat="server" Owner="" Text="6" Value="6" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Dfb:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_Dfb" runat="server" Filter="StartsWith" DropDownWidth="250px"
                                                                Height="120px" Width="150px" AppendDataBoundItems="True" DataTextField="CompName"
                                                                AutoPostBack="True">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Lining Condition:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_liningcondition" runat="server" Filter="StartsWith"
                                                                DropDownWidth="150px" Height="60px" Width="150px" AppendDataBoundItems="True"
                                                                DataTextField="CompName" EmptyMessage="Select" AutoPostBack="True" OnSelectedIndexChanged="OnSelectedIndexChanged_LiningCondition">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Poor" Value="10" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Average" Value="2" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Good" Value="1" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td rowspan="2" valign="top">
                                                            <asp:Label ID="lbl_Desc" runat="server" Text="Description:" ForeColor="Red"></asp:Label>
                                                            &nbsp;&nbsp;&nbsp;
                                                            <asp:Label ID="lbl_LineConDescription" runat="server" Height="50px" Width="300px"
                                                                Wrap="true" ForeColor="Blue"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Online Monitoring:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_OnlineMonitoring" runat="server" Filter="StartsWith"
                                                                DropDownWidth="150px" Height="40px" Width="150px" AppendDataBoundItems="True"
                                                                DataTextField="CompName" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Yes" Value="0.1" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="No" Value="1.0" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Df Liner:
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:TextBox ID="txt_dfliner" runat="server" Width="150px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <asp:Button ID="btn_CalcLine" Font-Bold="true" runat="server" Text=" Calculate "
                                                                OnClick="btn_CalcLine_Click" />
                                                            &nbsp; &nbsp;&nbsp; &nbsp;
                                                            <asp:Button ID="btn_LineSave" Font-Bold="true" runat="server" Text="  Save   " OnClick="btn_LineSave_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                            </div>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                </ajaxToolkit:TabContainer>
                                <%-- Tab External Corrosion Damage Factor--%>
                                <ajaxToolkit:TabContainer runat="server" ID="tab_ExternalDamage" Height="400px" ActiveTabIndex="1"
                                    Font-Bold="true" Width="50%" BackColor="#C0D7E8">
                                    <%-- Tab E C D Factor--%>
                                    <ajaxToolkit:TabPanel BackColor="#C0D7E8" runat="server" ID="tab_ExternalDamage_tB"
                                        HeaderText="External Corrosion Damage Factor">
                                        <ContentTemplate>
                                            <div class="text" style="background: -webkit-linear-gradient(#e09432,#f8b117); height: 400px;
                                                overflow: auto;">
                                                <table width="60%" cellspacing="8">
                                                    <tr>
                                                        <td colspan="3">
                                                            <asp:Button ID="btnECDSubmit" runat="server" Text=" Submit " OnClick="btnECDSubmit_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                        <td align="right">
                                                            <asp:Button ID="btnECDDelete" runat="server" Text=" Delete " OnClick="btnECDDelete_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Inspection Date:
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="dt_ECD_InspectDate" runat="server" Width="150px" DateInput-EmptyMessage="Select Inspection Date"
                                                                AutoPostBack="true" MinDate="01/01/1000" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy"
                                                                OnSelectedDateChanged="dt_ECD_InspectDate_OnSelectedDateChanged">
                                                                <Calendar ID="Calendar9" runat="server">
                                                                    <SpecialDays>
                                                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                                    </SpecialDays>
                                                                </Calendar>
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                        <td>
                                                            Inspection Effectiveness:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_ECD_InsEff" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="100px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Highly Effective" Value="A" />
                                                                    <telerik:RadComboBoxItem Text="Usually Effective" Value="B" />
                                                                    <telerik:RadComboBoxItem Text="Fairly Effective" Value="C" />
                                                                    <telerik:RadComboBoxItem Text="Poorly Effective" Value="D" />
                                                                    <telerik:RadComboBoxItem Text="In Effective" Value="E" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Comp/Coating Instal Date:
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="dt_ECD_CmpInstal" runat="server" Width="150px" DateInput-EmptyMessage="Select Comp/Coating Date"
                                                                MinDate="01/01/1000" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy">
                                                                <Calendar ID="dt_CmpInstal" runat="server">
                                                                    <SpecialDays>
                                                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                                    </SpecialDays>
                                                                </Calendar>
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                        <td>
                                                            Calculation Date:
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="dt_ECD_CalcDate" runat="server" Width="150px" DateInput-EmptyMessage="Select Calculation Date"
                                                                MinDate="01/01/1000" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy">
                                                                <Calendar ID="Calendar1" runat="server">
                                                                    <SpecialDays>
                                                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                                    </SpecialDays>
                                                                </Calendar>
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            No of Inspection:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_ECD_nofIns" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="120px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="1" Value="1" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="2" Value="2" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="3" Value="3" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="4" Value="4" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="5" Value="5" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="6" Value="6" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Age-tk:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_ECD_Agetk" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Coating Quality:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_ECD_CoatQual" runat="server" Filter="StartsWith" DropDownWidth="180px"
                                                                Height="60px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select"
                                                                AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_ECD_CoatQual">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="No Coating or Poor Quality" Value="1" runat="server"
                                                                        Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Medium Quality" Value="2" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="High Quality" Value="3" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Age_Coat:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_ECD_AgeCoat" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Age:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_ECD_Age" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Fps:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_ECD_Fps" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="40px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="1" Value="1" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="2" Value="2" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Fip:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_ECD_Fip" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="40px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="1" Value="1" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="2" Value="2" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Cr Driver:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_ECD_crdriver" runat="server" Filter="StartsWith" DropDownWidth="200px"
                                                                AutoPostBack="true" Height="80px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select"
                                                                OnSelectedIndexChanged="OnSelectedIndexChanged_ECD_crb">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Marine/Cooling Tower Drift Area" Value="marine" runat="server"
                                                                        Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Temperature" Value="temp" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Arid/Dry" Value="arid" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Severe" Value="severe" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Cr:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_ECD_cr" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Art:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_ECD_art" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Df-ext:
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txt_ECD_df" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="1">
                                                            <asp:Button ID="btn_ECD_Calc" runat="server" Text=" Calculate " OnClick="btn_ECD_Calc_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:Button ID="btn_ECD_Save" runat="server" Text="  Save   " OnClick="btn_ECD_Save_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                    <%--<tr>
                                                        <td colspan="4">
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Age-tk Field not support Characters please enter numbers only"
                                                                ControlToValidate="txt_ECD_Agetk" ForeColor="Red" ValidationExpression="^[1-9]\d*(\.\d+)?$"></asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>--%>
                                                </table>
                                                <br />
                                            </div>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <%-- Tab CUI Factor--%>
                                    <ajaxToolkit:TabPanel BackColor="#C0D7E8" runat="server" ID="tab_CUI" HeaderText="CUI Damage Factor">
                                        <ContentTemplate>
                                            <div class="text" style="background: -webkit-linear-gradient(#e09432,#f8b117); height: 400px;
                                                overflow: auto;">
                                                <table width="60%" cellspacing="8">
                                                    <tr>
                                                        <td colspan="3">
                                                            <asp:Button ID="btnCUISubmit" runat="server" Text=" Submit " OnClick="btnCUISubmit_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                        <td align="right">
                                                            <asp:Button ID="btnCUIDelete" runat="server" Text=" Delete " OnClick="btnCUIDelete_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Inspection Date:
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="dt_CUI_InspectDate" runat="server" Width="150px" DateInput-EmptyMessage="Select Inspection Date"
                                                                AutoPostBack="true" MinDate="01/01/1900" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy"
                                                                OnSelectedDateChanged="dt_CUI_InspectDate_OnSelectedDateChanged">
                                                                <Calendar ID="Calendar10" runat="server">
                                                                    <SpecialDays>
                                                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                                    </SpecialDays>
                                                                </Calendar>
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                        <td>
                                                            Inspection Effectiveness:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_CUI_InsEff" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="100px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Highly Effective" Value="A" />
                                                                    <telerik:RadComboBoxItem Text="Usually Effective" Value="B" />
                                                                    <telerik:RadComboBoxItem Text="Fairly Effective" Value="C" />
                                                                    <telerik:RadComboBoxItem Text="Poorly Effective" Value="D" />
                                                                    <telerik:RadComboBoxItem Text="In Effective" Value="E" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Comp/Coating Instal Date:
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="dt_CUI_CmpInstal" runat="server" Width="150px" DateInput-EmptyMessage="Select Comp/Coating Date"
                                                                MinDate="01/01/1000" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy">
                                                                <Calendar ID="Calendar2" runat="server">
                                                                    <SpecialDays>
                                                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                                    </SpecialDays>
                                                                </Calendar>
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                        <td>
                                                            Calculation Date:
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="dt_CUI_CalcDate" runat="server" Width="150px" DateInput-EmptyMessage="Select Calculation Date"
                                                                MinDate="01/01/1000" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy">
                                                                <Calendar ID="Calendar3" runat="server">
                                                                    <SpecialDays>
                                                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                                    </SpecialDays>
                                                                </Calendar>
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            No of Inspection:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_CUI_nofIns" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="120px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="1" Value="1" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="2" Value="2" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="3" Value="3" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="4" Value="4" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="5" Value="5" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="6" Value="6" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Age-tk:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_CUI_Agetk" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Coating Quality:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_CUI_coatqual" runat="server" Filter="StartsWith" DropDownWidth="180px"
                                                                Height="60px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select"
                                                                AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_CUI_CoatQual">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="No Coating or Poor Quality" Value="1" runat="server"
                                                                        Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Medium Quality" Value="2" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="High Quality" Value="3" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Age_Coat:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_CUI_Agecoat" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Age:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_CUI_Age" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Fins:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_CUI_Fins" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="140px" Width="150px" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="None" Value="1" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Foamglass" Value="0.75" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Pearlite" Value="1.0" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Fiberglass" Value="1.25" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="MineralWool" Value="1.250" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="CalciumSilicate" Value="1.2500" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Asbestos" Value="1.25000" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Fcm:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_CUI_Fcm" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="60px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Below Average" Value="0.75" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Average" Value="1.0" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Above Average" Value="1.25" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Fic:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_CUI_Fic" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="60px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Below Average" Value="1.25" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Average" Value="1.0" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Above Average" Value="0.75" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Fps:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_CUI_Fps" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="40px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="1" Value="1" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="2" Value="2" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Fip:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_CUI_Fip" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="40px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="1" Value="1" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="2" Value="2" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Cr Driver:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_CUI_crdriver" runat="server" Filter="StartsWith" DropDownWidth="200px"
                                                                AutoPostBack="true" Height="80px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select"
                                                                OnSelectedIndexChanged="OnSelectedIndexChanged_CUI_crb">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Marine/Cooling Tower Drift Area" Value="marine" runat="server"
                                                                        Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Temperature" Value="temp" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Arid/Dry" Value="arid" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Severe" Value="severe" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Cr:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_CUI_cr" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Art:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_CUI_art" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Df-CUIF:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_CUI_Df" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="1">
                                                            <asp:Button ID="btn_CUI_Calc" runat="server" Text=" Calculate " OnClick="btn_CUI_Calc_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:Button ID="btn_CUI_Save" runat="server" Text="  Save   " OnClick="btn_CUI_Save_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                    <%-- <tr>
                                                        <td colspan="4">
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Age-tk Field not support Characters please enter numbers only"
                                                                ControlToValidate="txt_CUI_Agetk" ForeColor="Red" ValidationExpression="^[1-9]\d*(\.\d+)?$"></asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>--%>
                                                </table>
                                            </div>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <%-- Tab  External CLSCC Factor--%>
                                    <ajaxToolkit:TabPanel BackColor="#C0D7E8" runat="server" ID="TabPanel10" HeaderText="External CLSCC">
                                        <ContentTemplate>
                                            <div class="text" style="background: -webkit-linear-gradient(#e09432,#f8b117); height: 400px;
                                                overflow: auto;">
                                                <br />
                                                <table width="60%" cellspacing="8">
                                                    <tr>
                                                        <td colspan="3">
                                                            <asp:Button ID="btnExCLSSubmit" runat="server" Text=" Submit " OnClick="btnExCLSSubmit_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                        <td align="right">
                                                            <asp:Button ID="btnExCLSSDelete" runat="server" Text=" Delete " OnClick="btnExCLSSDelete_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Inspection Date:
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="dt_ExCLS_InspectDate" runat="server" Width="150px" DateInput-EmptyMessage="Select Inspection Date"
                                                                AutoPostBack="true" MinDate="01/01/1000" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy"
                                                                OnSelectedDateChanged="dt_ExCLS_InspectDate_OnSelectedDateChanged">
                                                                <Calendar ID="Calendar11" runat="server">
                                                                    <SpecialDays>
                                                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                                    </SpecialDays>
                                                                </Calendar>
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                        <td>
                                                            Inspection Effectiveness:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_ExCLS_InsEff" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="80px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Highly Effective" Value="A" />
                                                                    <telerik:RadComboBoxItem Text="Usually Effective" Value="B" />
                                                                    <telerik:RadComboBoxItem Text="Fairly Effective" Value="C" />
                                                                    <telerik:RadComboBoxItem Text="Poorly Effective" Value="D" />
                                                                    <telerik:RadComboBoxItem Text="In Effective" Value="E" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Comp/Coating Instal Date:
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="dt_ExCLS_CmpInstal" runat="server" Width="150px" DateInput-EmptyMessage="Select Comp/Coating Date"
                                                                MinDate="01/01/1000" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy">
                                                                <Calendar ID="Calendar4" runat="server">
                                                                    <SpecialDays>
                                                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                                    </SpecialDays>
                                                                </Calendar>
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                        <td>
                                                            Calculation Date:
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="dt_ExCLS_CalcDate" runat="server" Width="150px" DateInput-EmptyMessage="Select Calculation Date"
                                                                MinDate="01/01/1000" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy">
                                                                <Calendar ID="Calendar5" runat="server">
                                                                    <SpecialDays>
                                                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                                    </SpecialDays>
                                                                </Calendar>
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            No of Inspection:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_ExCLS_nofIns" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="80px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="1" Value="1" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="2" Value="2" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="3" Value="3" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="4" Value="4" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="5" Value="5" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="6" Value="6" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Age-tk:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_ExCLS_Agetk" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Coating Quality:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_ExCLS_CoatQual" runat="server" Filter="StartsWith" DropDownWidth="180px"
                                                                Height="60px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select"
                                                                AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_ExCLS_CoatQual">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="No Coating or Poor Quality" Value="1" runat="server"
                                                                        Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Medium Quality" Value="2" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="High Quality" Value="3" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Age
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_ExCLS_Age" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Area:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_ExCLS_crdriver" runat="server" Filter="StartsWith" DropDownWidth="200px"
                                                                AutoPostBack="true" Height="80px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select"
                                                                OnSelectedIndexChanged="OnSelectedIndexChanged_ExCLS_crb">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Marine/Cooling Tower Drift Area" Value="marine" runat="server"
                                                                        Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Temperature" Value="temp" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Arid/Dry" Value="arid" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Severe" Value="severe" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Svi:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_ExCLS_Svi" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Df-ext-CLSCC:
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txt_ExCLS_Df" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="1">
                                                            <asp:Button ID="btn_ExCLS_Calc" runat="server" Text=" Calculate " OnClick="btn_ExCLS_Calc_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:Button ID="btn_ExCLS_Save" runat="server" Text="  Save   " OnClick="btn_ExCLS_Save_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                    <%-- <tr>
                                                        <td colspan="4">
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Age-tk Field not support Characters please enter numbers only"
                                                                ControlToValidate="txt_ExCLS_Agetk" ForeColor="Red" ValidationExpression="^[1-9]\d*(\.\d+)?$"></asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>--%>
                                                </table>
                                                <br />
                                            </div>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <%-- Tab  External CUI CLSCC Factor--%>
                                    <ajaxToolkit:TabPanel BackColor="#C0D7E8" runat="server" ID="TabPanel11" HeaderText="External CUI CLSCC">
                                        <ContentTemplate>
                                            <div class="text" style="background: -webkit-linear-gradient(#e09432,#f8b117); height: 400px;
                                                overflow: auto;">
                                                <br />
                                                <table width="60%" cellspacing="8">
                                                    <tr>
                                                        <td colspan="3">
                                                            <asp:Button ID="btnExCUISubmit" runat="server" Text=" Submit " OnClick="btnExCUISubmit_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                        <td align="right">
                                                            <asp:Button ID="btnExCUIDelete" runat="server" Text=" Delete " OnClick="btnExCUIDelete_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Inspection Date:
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="dt_ExCUI_InspectDate" runat="server" Width="150px" DateInput-EmptyMessage="Select Inspection Date"
                                                                AutoPostBack="true" MinDate="01/01/1000" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy"
                                                                OnSelectedDateChanged="dt_ExCUI_InspectDate_OnSelectedDateChanged">
                                                                <Calendar ID="Calendar12" runat="server">
                                                                    <SpecialDays>
                                                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                                    </SpecialDays>
                                                                </Calendar>
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                        <td>
                                                            Inspection Effectiveness:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_ExCUI_InsEff" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="100px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Highly Effective" Value="A" />
                                                                    <telerik:RadComboBoxItem Text="Usually Effective" Value="B" />
                                                                    <telerik:RadComboBoxItem Text="Fairly Effective" Value="C" />
                                                                    <telerik:RadComboBoxItem Text="Poorly Effective" Value="D" />
                                                                    <telerik:RadComboBoxItem Text="In Effective" Value="E" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Comp/Coating Instal Date:
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="dt_ExCUI_CmpInstal" runat="server" Width="150px" DateInput-EmptyMessage="Select Comp/Coating Date"
                                                                MinDate="01/01/1000" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy">
                                                                <Calendar ID="Calendar6" runat="server">
                                                                    <SpecialDays>
                                                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                                    </SpecialDays>
                                                                </Calendar>
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                        <td>
                                                            Calculation Date:
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="dt_ExCUI_CalcDate" runat="server" Width="150px" DateInput-EmptyMessage="Select Calculation Date"
                                                                MinDate="01/01/1000" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy">
                                                                <Calendar ID="Calendar7" runat="server">
                                                                    <SpecialDays>
                                                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                                    </SpecialDays>
                                                                </Calendar>
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            No of Inspection:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_ExCUI_nofIns" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="120px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="1" Value="1" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="2" Value="2" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="3" Value="3" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="4" Value="4" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="5" Value="5" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="6" Value="6" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Age-tk:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_ExCUI_Agetk" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Coating Quality:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_ExCUI_CoatQual" runat="server" Filter="StartsWith" DropDownWidth="180px"
                                                                Height="60px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select"
                                                                AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_ExCUI_CoatQual">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="No Coating or Poor Quality" Value="1" runat="server"
                                                                        Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Medium Quality" Value="2" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="High Quality" Value="3" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Age
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_ExCUI_Age" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Area:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_ExCUI_Area" runat="server" Filter="StartsWith" DropDownWidth="200px"
                                                                AutoPostBack="true" Height="80px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select"
                                                                OnSelectedIndexChanged="OnSelectedIndexChanged_ExCUI_crb">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Marine/Cooling Tower Drift Area" Value="marine" runat="server"
                                                                        Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Temperature" Value="temp" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Arid/Dry" Value="arid" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Severe" Value="severe" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Piping Complexity:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_ExCUI_Pip" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="60px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select"
                                                                AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_ExCUI_Pip">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Below Average" Value="1" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Average" Value="2" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Above Average" Value="3" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Insulation Condition:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_ExCUI_InsCon" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="60px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select"
                                                                AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_ExCUI_insCon">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Below Average" Value="1" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Average" Value="2" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Above Average" Value="3" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Chloride Free Insulation:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_ExCUI_ChlrFree" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="40px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select"
                                                                AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_ExCUI_ChlrFree">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Contain Chloride" Value="2" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Chloride Free" Value="1" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Svi:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_ExCUI_Svi" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Df-ext-CUI:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_ExCUI_Df" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="1">
                                                            <asp:Button ID="btn_ExCUI_Calc" runat="server" Text=" Calculate " OnClick="btn_ExCUI_Calc_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:Button ID="btn_ExCUI_Save" runat="server" Text="  Save   " OnClick="btn_ExCUI_Save_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                    <%-- <tr>
                                                        <td colspan="4">
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Age-tk Field not support Characters please enter numbers only"
                                                                ControlToValidate="txt_ExCUI_Agetk" ForeColor="Red" ValidationExpression="^[1-9]\d*(\.\d+)?$"></asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>--%>
                                                </table>
                                                <br />
                                            </div>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                </ajaxToolkit:TabContainer>
                                <%--TabContainer Stress Corrosion cracking Damage Factor--%>
                                <ajaxToolkit:TabContainer runat="server" ID="tab_SCCDF" Height="340px" ActiveTabIndex="0"
                                    Width="60%" BackColor="#C0D7E8" Font-Bold="true">
                                    <%--Panel SCCDF-Caustic Cracking--%>
                                    <ajaxToolkit:TabPanel BackColor="#C0D7E8" runat="server" ID="TabPanel3" HeaderText="Caustic">
                                        <ContentTemplate>
                                            <div class="text" style="background: -webkit-linear-gradient(#e09432,#f8b117); height: 340px;
                                                overflow: auto;">
                                                <br />
                                                <table width="60%" cellspacing="8">
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Button ID="btnCausticSubmit" runat="server" Text=" Submit " OnClick="btnCausticSubmit_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label1" Text="Age" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_CausticAge" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Inspection Effectiveness:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_Caustic_InsEff" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="100px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Highly Effective" Value="A" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Usually Effective" Value="B" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Fairly Effective" Value="C" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Poorly Effective" Value="D" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="In Effective" Value="E" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Text="No of Inspection"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_noCausInspect" runat="server" AppendDataBoundItems="True"
                                                                DropDownWidth="150px" EmptyMessage="Select" Filter="StartsWith" Height="120px"
                                                                Width="150px">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem runat="server" Owner="" Text="1" Value="1" />
                                                                    <telerik:RadComboBoxItem runat="server" Owner="" Text="2" Value="2" />
                                                                    <telerik:RadComboBoxItem runat="server" Owner="" Text="3" Value="3" />
                                                                    <telerik:RadComboBoxItem runat="server" Owner="" Text="4" Value="4" />
                                                                    <telerik:RadComboBoxItem runat="server" Owner="" Text="5" Value="5" />
                                                                    <telerik:RadComboBoxItem runat="server" Owner="" Text="6" Value="6" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Inspection Date:
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="dt_Caustic_InspectDate" runat="server" Width="150px" DateInput-EmptyMessage="Select Inspection Date"
                                                                MinDate="01/01/1000" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy">
                                                                <Calendar ID="Calendar13" runat="server">
                                                                    <SpecialDays>
                                                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                                    </SpecialDays>
                                                                </Calendar>
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Susceptibility & Svi:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_Caustic_Svi" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="80px" Width="150px" AppendDataBoundItems="True" DataTextField="CompName"
                                                                EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="High" Value="5000" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Medium" Value="500" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Low" Value="50" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="None" Value="1" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Df-Caustic:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Caustic_Df" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Button ID="btn_CausticCalc" Font-Bold="true" runat="server" Text=" Calculate "
                                                                OnClick="btn_CausticCalc_Click" />
                                                            &nbsp; &nbsp;&nbsp; &nbsp;
                                                            <asp:Button ID="btn_Caustic_Save" Font-Bold="true" runat="server" Text="  Save   "
                                                                OnClick="btn_Caustic_Save_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                            </div>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <%--Panel SCCDF-Amine Cracking--%>
                                    <ajaxToolkit:TabPanel BackColor="#C0D7E8" runat="server" ID="TabPanel4" HeaderText="Amine">
                                        <ContentTemplate>
                                            <div class="text" style="background: -webkit-linear-gradient(#e09432,#f8b117); height: 340px;
                                                overflow: auto;">
                                                <br />
                                                <table width="60%" cellspacing="8">
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Button ID="btnAmineSubmit" runat="server" Text=" Submit " OnClick="btnAmineSubmit_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label2" Text="Age" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Amn_Age" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Inspection Effectiveness:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_Amn_InsEff" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="80px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Highly Effective" Value="A" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Usually Effective" Value="B" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Fairly Effective" Value="C" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Poorly Effective" Value="D" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="In Effective" Value="E" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label5" Text="No of Inspection" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadComboBox ID="cbo_Amn_noIns" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                    Height="80px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="1" Value="1" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="2" Value="2" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="3" Value="3" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="4" Value="4" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="5" Value="5" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="6" Value="6" runat="server" Owner="" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Inspection Date:
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="dt_Amn_InspectDate" runat="server" Width="150px" DateInput-EmptyMessage="Select Inspection Date"
                                                                MinDate="01/01/1000" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy">
                                                                <Calendar ID="Calendar14" runat="server">
                                                                    <SpecialDays>
                                                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                                    </SpecialDays>
                                                                </Calendar>
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Susceptibility & Svi:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_Amn_Svi" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="80px" Width="150px" AppendDataBoundItems="True" DataTextField="CompName"
                                                                EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="High" Value="1000" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Medium" Value="100" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Low" Value="10" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="None" Value="1" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Df-Amine:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Amn_Df" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Button ID="btn_Amn_Calc" Font-Bold="true" runat="server" Text=" Calculate "
                                                                OnClick="btn_Amn_Calc_Click" />
                                                            &nbsp; &nbsp;&nbsp; &nbsp;
                                                            <asp:Button ID="btn_Amn_Save" Font-Bold="true" runat="server" Text="  Save   " OnClick="btn_Amn_Save_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                            </div>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <%--Panel SCCDF-Sulfide Stress Cracking--%>
                                    <ajaxToolkit:TabPanel BackColor="#C0D7E8" runat="server" ID="TabPanel5" HeaderText="Sulfide">
                                        <ContentTemplate>
                                            <div class="text" style="background: -webkit-linear-gradient(#e09432,#f8b117); height: 340px;
                                                overflow: auto;">
                                                <br />
                                                <table width="60%" cellspacing="8">
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:Button ID="btnSulSubmit" runat="server" Text=" Submit " OnClick="btnSulSubmit_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label3" Text="Age" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Sul_Age" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Inspection Effectiveness:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_Sul_InsEff" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="100px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Highly Effective" Value="A" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Usually Effective" Value="B" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Fairly Effective" Value="C" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Poorly Effective" Value="D" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="In Effective" Value="E" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label6" Text="No of Inspection" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_Sul_nofIns" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="120px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="1" Value="1" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="2" Value="2" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="3" Value="3" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="4" Value="4" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="5" Value="5" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="6" Value="6" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Inspection Date:
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="dt_Sul_InspectDate" runat="server" Width="150px" DateInput-EmptyMessage="Select Inspection Date"
                                                                MinDate="01/01/1000" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy">
                                                                <Calendar ID="Calendar15" runat="server">
                                                                    <SpecialDays>
                                                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                                    </SpecialDays>
                                                                </Calendar>
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            pH of Water:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_Sul_pH" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="100px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="< 5.5" Value="5.5" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="5.5 to 7.5" Value="7.5" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="7.6 to 8.3" Value="7.6" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="8.4 to 8.9" Value="8.4" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="> 9.0" Value="9.0" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            H2S:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_sul_H2s" runat="server" AppendDataBoundItems="True"
                                                                AutoPostBack="True" DropDownWidth="150px" EmptyMessage="Select" Filter="StartsWith"
                                                                Height="80px" OnSelectedIndexChanged="OnSelectedIndexChanged_H2S" Width="150px">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem runat="server" Owner="" Text="&lt; 55 ppm" Value="Fiftyppm" />
                                                                    <telerik:RadComboBoxItem runat="server" Owner="" Text="50 to 1000 ppm" Value="Thosandppm" />
                                                                    <telerik:RadComboBoxItem runat="server" Owner="" Text="1000 to 10000 ppm" Value="Tenppm" />
                                                                    <telerik:RadComboBoxItem runat="server" Owner="" Text="&gt; 10000 ppm" Value="GtTenppm" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Environmental Severity:
                                                            <%--Susceptibility &amp; Svi:--%>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Sul_Env" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label15" Text="Heat Treatment" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_Sul_Heat" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="40px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="As-Welded" Value="weld" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="PWHT" Value="pwht" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Max Brinnell:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_Sul_Brin" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="60px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select"
                                                                AutoPostBack="True" OnSelectedIndexChanged="OnSelectedIndexChanged_EnviSul">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="< 200" Value="two" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="200 - 237" Value="twothree" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="> 237" Value="gttwothree" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Susceptibility &amp; Svi:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Sul_Svi" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            is there is a known cracking:
                                                            <asp:CheckBox ID="chk_known_Crack" runat="server" AutoPostBack="True" OnCheckedChanged="chk_known_Crack_CheckedChanged" />
                                                        </td>
                                                        <td>
                                                            Df-Scc:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Sul_Df" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:Button ID="btn_Sul_Calc" Font-Bold="true" runat="server" OnClick="btn_Sul_Calc_Click"
                                                                Text=" Calculate " />
                                                            &nbsp; &nbsp;&nbsp; &nbsp;
                                                            <asp:Button ID="btn_Sul_Save" Font-Bold="true" runat="server" OnClick="btn_Sul_Save_Click"
                                                                Text="  Save   " />
                                                        </td>
                                                    </tr>
                                                    </tr>
                                                </table>
                                                <br />
                                            </div>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <%--Panel SCCDF-HIC/SOHIC-H2S Cracking--%>
                                    <ajaxToolkit:TabPanel BackColor="#C0D7E8" runat="server" ID="tab_SOHIC" HeaderText="H2S">
                                        <ContentTemplate>
                                            <div class="text" style="background: -webkit-linear-gradient(#e09432,#f8b117); height: 340px;
                                                overflow: auto;">
                                                <br />
                                                <table width="60%" cellspacing="8">
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:Button ID="btnHICSubmit" runat="server" Text=" Submit " OnClick="btnHICSubmit_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label7" Text="Age" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_HIC_Age" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Inspection Effectiveness:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_HIC_InsEff" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="100px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Highly Effective" Value="A" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Usually Effective" Value="B" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Fairly Effective" Value="C" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Poorly Effective" Value="D" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="In Effective" Value="E" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label8" Text="No of Inspection" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_HIC_nofIns" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="120px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="1" Value="1" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="2" Value="2" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="3" Value="3" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="4" Value="4" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="5" Value="5" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="6" Value="6" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Inspection Date:
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="dt_HIC_InspectDate" runat="server" Width="150px" DateInput-EmptyMessage="Select Inspection Date"
                                                                MinDate="01/01/1000" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy">
                                                                <Calendar ID="Calendar16" runat="server">
                                                                    <SpecialDays>
                                                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                                    </SpecialDays>
                                                                </Calendar>
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                        <tr>
                                                            <td>
                                                                pH of Water:
                                                            </td>
                                                            <td>
                                                                <telerik:RadComboBox ID="cbo_HIC_pH" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                    Height="100px" Width="150px" AppendDataBoundItems="True" DataTextField="CompName"
                                                                    EmptyMessage="Select" AutoPostBack="True">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="< 5.5" Value="5.5" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="5.5 to 7.5" Value="7.5" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="7.6 to 8.3" Value="7.6" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="8.4 to 8.9" Value="8.4" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="> 9.0" Value="9.0" runat="server" Owner="" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </td>
                                                            <td>
                                                                SteelSulfar:
                                                            </td>
                                                            <td>
                                                                <telerik:RadComboBox ID="cbo_HIC_StSulf" runat="server" AppendDataBoundItems="True"
                                                                    AutoPostBack="True" DropDownWidth="150px" EmptyMessage="Select" Filter="StartsWith"
                                                                    Height="80px" OnSelectedIndexChanged="OnSelectedIndexChanged_HIC" Width="150px">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem runat="server" Owner="" Text="&lt; 55 ppm" Value="Fiftyppm" />
                                                                        <telerik:RadComboBoxItem runat="server" Owner="" Text="50 to 1000 ppm" Value="Thosandppm" />
                                                                        <telerik:RadComboBoxItem runat="server" Owner="" Text="1000 to 10000 ppm" Value="Tenppm" />
                                                                        <telerik:RadComboBoxItem runat="server" Owner="" Text="&gt; 10000 ppm" Value="GtTenppm" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Environmental Severity:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_HIC_Env" runat="server" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label16" Text="Heat Treatment" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadComboBox ID="cbo_HIC_heat" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                    Height="40px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="As-Welded" Value="weld" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="PWHT" Value="pwht" runat="server" Owner="" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Steel Sulfur:
                                                            </td>
                                                            <td>
                                                                <telerik:RadComboBox ID="cbo_HIC_Steel" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                    Height="60px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select"
                                                                    AutoPostBack="True" OnSelectedIndexChanged="OnSelectedIndexChanged_SteelSul">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="High Sulfar Steel" Value="1" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="Low Sulfar Steel" Value="2" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="Ultra Sulfar Steel" Value="3" runat="server" Owner="" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </td>
                                                            <td>
                                                                Susceptibility &amp; Svi:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_HIC_Svi" runat="server" Enabled="false"></asp:TextBox>
                                                                <%-- <telerik:RadComboBox ID="cbo_HIC_Svi" runat="server" AppendDataBoundItems="True"
                                                                    AutoPostBack="True" DataTextField="CompName" DropDownWidth="150px" EmptyMessage="Select"
                                                                    Filter="StartsWith" Height="60px" Width="150px">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem runat="server" Owner="" Text="High" Value="100" />
                                                                        <telerik:RadComboBoxItem runat="server" Owner="" Text="Medium" Value="10" />
                                                                        <telerik:RadComboBoxItem runat="server" Owner="" Text="Low" Value="1" />
                                                                        <telerik:RadComboBoxItem runat="server" Owner="" Text="None" Value="1" />
                                                                    </Items>
                                                                </telerik:RadComboBox>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                Is there is a known cracking:
                                                                <asp:CheckBox ID="chk_HIC_known" runat="server" AutoPostBack="True" OnCheckedChanged="chk_HIC_known_CheckedChanged" />
                                                            </td>
                                                            <td>
                                                                Df-SOHIC:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_HIC_DfSOHIC" runat="server" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                                <asp:Button ID="btn_HIC_Calc" Font-Bold="true" runat="server" Text=" Calculate "
                                                                    OnClick="btn_HIC_Calc_Click" />
                                                                &nbsp; &nbsp;&nbsp; &nbsp;
                                                                <asp:Button ID="btn_HIC_Save" Font-Bold="true" runat="server" Text="  Save   " OnClick="btn_HIC_Save_Click" />
                                                            </td>
                                                        </tr>
                                                    </tr>
                                                </table>
                                                <br />
                                            </div>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <%--Panel SCCDF Carbonate Cracking--%>
                                    <ajaxToolkit:TabPanel BackColor="#C0D7E8" runat="server" ID="tab_Carbonate" HeaderText="Carbonate">
                                        <ContentTemplate>
                                            <div class="text" style="background: -webkit-linear-gradient(#e09432,#f8b117); height: 340px;
                                                overflow: auto;">
                                                <br />
                                                <table width="60%" cellspacing="8">
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:Button ID="btnCrbntSubmit" runat="server" Text=" Submit " OnClick="btnCrbntSubmit_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label9" Text="Age" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Crbnt_Age" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Inspection Effectiveness:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_Crbnt_InsEff" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="100px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Highly Effective" Value="A" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Usually Effective" Value="B" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Fairly Effective" Value="C" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Poorly Effective" Value="D" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="In Effective" Value="E" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label10" Text="No of Inspection" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_Crbnt_nofIns" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="120px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="1" Value="1" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="2" Value="2" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="3" Value="3" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="4" Value="4" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="5" Value="5" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="6" Value="6" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Inspection Date:
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="dt_Crbnt_InspectDate" runat="server" Width="150px" DateInput-EmptyMessage="Select Inspection Date"
                                                                MinDate="01/01/1000" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy">
                                                                <Calendar ID="Calendar17" runat="server">
                                                                    <SpecialDays>
                                                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                                    </SpecialDays>
                                                                </Calendar>
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                        <tr>
                                                            <td>
                                                                pH of Water:
                                                            </td>
                                                            <td>
                                                                <telerik:RadComboBox ID="cbo_Crbnt_pH" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                    Height="60px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="7.6 to 8.3" Value="7.6" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="> 8.3 to < 9.0" Value="8.3" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="=> 9.0" Value="9.0" runat="server" Owner="" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </td>
                                                            <td>
                                                                CO3:
                                                            </td>
                                                            <td>
                                                                <telerik:RadComboBox ID="cbo_Crbnt_CO3" runat="server" AppendDataBoundItems="True"
                                                                    AutoPostBack="True" DropDownWidth="150px" EmptyMessage="Select" Filter="StartsWith"
                                                                    Height="80px" OnSelectedIndexChanged="OnSelectedIndexChanged_CO3" Width="150px">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem runat="server" Owner="" Text="&lt; 100 ppm" Value="Hundredppm" />
                                                                        <telerik:RadComboBoxItem runat="server" Owner="" Text="100 to 500 ppm" Value="Five100ppm" />
                                                                        <telerik:RadComboBoxItem runat="server" Owner="" Text="500 to 1000 ppm" Value="Thousandppm" />
                                                                        <telerik:RadComboBoxItem runat="server" Owner="" Text="&gt; 1000 ppm" Value="GtThousandppm" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Susceptibility &amp; Svi:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_Crbnt_Svi" runat="server" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td colspan="2">
                                                                Is there is a known cracking:
                                                                <asp:CheckBox ID="chk_Crbnt_known" runat="server" AutoPostBack="True" OnCheckedChanged="chk_Crbnt_known_CheckedChanged" />
                                                            </td>
                                                            <%-- <td>
                                                                Susceptibility &amp; Svi:
                                                            </td>
                                                            <td>
                                                                <telerik:RadComboBox ID="cbo_Crbnt_Svi" runat="server" AppendDataBoundItems="True"
                                                                    AutoPostBack="True" DataTextField="CompName" DropDownWidth="150px" EmptyMessage="Select"
                                                                    Filter="StartsWith" Height="60px" Width="150px">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem runat="server" Owner="" Text="High" Value="1000" />
                                                                        <telerik:RadComboBoxItem runat="server" Owner="" Text="Medium" Value="100" />
                                                                        <telerik:RadComboBoxItem runat="server" Owner="" Text="Low" Value="10" />
                                                                        <telerik:RadComboBoxItem runat="server" Owner="" Text="None" Value="1" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </td>--%>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Df-Carbonate:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_Crbnt_Df" runat="server" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                                <asp:Button ID="btn_Crbnt_Calc" Font-Bold="true" runat="server" Text=" Calculate "
                                                                    OnClick="btn_Crbnt_Calc_Click" />
                                                                &nbsp; &nbsp;&nbsp; &nbsp;
                                                                <asp:Button ID="btn_Crbnt_Save" Font-Bold="true" runat="server" Text="  Save   "
                                                                    OnClick="btn_Crbnt_Save_Click" />
                                                            </td>
                                                        </tr>
                                                    </tr>
                                                </table>
                                                <br />
                                            </div>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <%--Panel SCCDF-PTA Cracking--%>
                                    <ajaxToolkit:TabPanel BackColor="#C0D7E8" runat="server" ID="TabPanel6" HeaderText="PTA">
                                        <ContentTemplate>
                                            <div class="text" style="background: -webkit-linear-gradient(#e09432,#f8b117); height: 340px;
                                                overflow: auto;">
                                                <br />
                                                <table width="60%" cellspacing="8">
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:Button ID="btnPTASubmit" runat="server" Text=" Submit " OnClick="btnPTASubmit_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label11" Text="Age" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_PTA_Age" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                Inspection Effectiveness:
                                                            </td>
                                                            <td>
                                                                <telerik:RadComboBox ID="cbo_PTA_InsEff" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                    Height="100px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="Highly Effective" Value="A" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="Usually Effective" Value="B" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="Fairly Effective" Value="C" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="Poorly Effective" Value="D" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="In Effective" Value="E" runat="server" Owner="" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label12" Text="No of Inspection" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadComboBox ID="cbo_PTA_nofIns" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                    Height="120px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="1" Value="1" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="2" Value="2" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="3" Value="3" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="4" Value="4" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="5" Value="5" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="6" Value="6" runat="server" Owner="" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </td>
                                                            <td>
                                                                Inspection Date:
                                                            </td>
                                                            <td>
                                                                <telerik:RadDatePicker ID="dt_PTA_InspectDate" runat="server" Width="150px" DateInput-EmptyMessage="Select Inspection Date"
                                                                    MinDate="01/01/1000" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy">
                                                                    <Calendar ID="Calendar18" runat="server">
                                                                        <SpecialDays>
                                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                                        </SpecialDays>
                                                                    </Calendar>
                                                                </telerik:RadDatePicker>
                                                            </td>
                                                            <tr>
                                                                <td>
                                                                    Material:
                                                                </td>
                                                                <td>
                                                                    <telerik:RadComboBox ID="cbo_PTA_Material" runat="server" Filter="StartsWith" DropDownWidth="300px"
                                                                        Height="100px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                        <Items>
                                                                            <telerik:RadComboBoxItem Text="All Regular 300Series SS & Alloy 600 & 800" Value="Alloy600"
                                                                                runat="server" Owner="" />
                                                                            <telerik:RadComboBoxItem Text="H Grade 300series SS" Value="HGrade" runat="server"
                                                                                Owner="" />
                                                                            <telerik:RadComboBoxItem Text="L Grade 300series SS" Value="LGrade" runat="server"
                                                                                Owner="" />
                                                                            <telerik:RadComboBoxItem Text="321 Stainless Steel " Value="321SS" runat="server"
                                                                                Owner="" />
                                                                            <telerik:RadComboBoxItem Text="347 SS Alloy 20,Alloy 625,All austenitic weld overlay"
                                                                                Value="347SS" runat="server" Owner="" />
                                                                        </Items>
                                                                    </telerik:RadComboBox>
                                                                </td>
                                                                <td>
                                                                    Heat Treatment:
                                                                </td>
                                                                <td>
                                                                    <telerik:RadComboBox ID="cbo_PTA_Heat" runat="server" AppendDataBoundItems="True"
                                                                        AutoPostBack="True" DropDownWidth="150px" EmptyMessage="Select" Filter="StartsWith"
                                                                        Height="60px" OnSelectedIndexChanged="OnSelectedIndexChanged_PTA" Width="150px">
                                                                        <Items>
                                                                            <telerik:RadComboBoxItem runat="server" Owner="" Text="Solution Annealed" Value="SolAnn" />
                                                                            <telerik:RadComboBoxItem runat="server" Owner="" Text="Stablized Before Welding"
                                                                                Value="BeforeWeld" />
                                                                            <telerik:RadComboBoxItem runat="server" Owner="" Text="Stablized After Welding" Value="AfterWeld" />
                                                                        </Items>
                                                                    </telerik:RadComboBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Susceptibility &amp; Svi:
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_PTA_Svi" runat="server" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td colspan="2">
                                                                    Is there is a known cracking:
                                                                    <asp:CheckBox ID="chk_PTA_known" runat="server" AutoPostBack="True" OnCheckedChanged="chk_PTA_known_CheckedChanged" />
                                                                </td>
                                                                <%-- <td>
                                                                Susceptibility &amp; Svi:
                                                            </td>
                                                            <td>
                                                                <telerik:RadComboBox ID="cbo_PTA_Svi" runat="server" AppendDataBoundItems="True"
                                                                    AutoPostBack="True" DataTextField="CompName" DropDownWidth="150px" EmptyMessage="Select"
                                                                    Filter="StartsWith" Height="60px" Width="150px">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem runat="server" Owner="" Text="High" Value="5000" />
                                                                        <telerik:RadComboBoxItem runat="server" Owner="" Text="Medium" Value="500" />
                                                                        <telerik:RadComboBoxItem runat="server" Owner="" Text="Low" Value="50" />
                                                                        <telerik:RadComboBoxItem runat="server" Owner="" Text="None" Value="1" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </td>--%>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Df-PTA:
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_PTA_Df" runat="server" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <asp:Button ID="btn_PTA_Calc" Font-Bold="true" runat="server" Text=" Calculate "
                                                                        OnClick="btn_PTA_Calc_Click" />
                                                                    &nbsp; &nbsp;&nbsp; &nbsp;
                                                                    <asp:Button ID="btn_PTA_Save" Font-Bold="true" runat="server" Text="  Save   " OnClick="btn_PTA_Save_Click" />
                                                                </td>
                                                            </tr>
                                                        </tr>
                                                </table>
                                                <br />
                                            </div>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <%--Panel CLSCC Cracking--%>
                                    <ajaxToolkit:TabPanel BackColor="#C0D7E8" runat="server" ID="TabPanel7" HeaderText="CLSCC">
                                        <ContentTemplate>
                                            <div class="text" style="background: -webkit-linear-gradient(#e09432,#f8b117); height: 340px;
                                                overflow: auto;">
                                                <br />
                                                <table width="60%" cellspacing="8">
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:Button ID="btnCLSSubmit" runat="server" Text=" Submit " OnClick="btnCLSSubmit_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label13" Text="Age" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_ClS_Age" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Inspection Effectiveness:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_CLS_InsEff" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="100px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Highly Effective" Value="A" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Usually Effective" Value="B" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Fairly Effective" Value="C" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Poorly Effective" Value="D" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="In Effective" Value="E" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label14" Text="No of Inspection" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_CLS_nofIns" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="120px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="1" Value="1" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="2" Value="2" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="3" Value="3" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="4" Value="4" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="5" Value="5" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="6" Value="6" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Inspection Date:
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="dt_CLS_InspectDate" runat="server" Width="150px" DateInput-EmptyMessage="Select Inspection Date"
                                                                MinDate="01/01/1000" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy">
                                                                <Calendar ID="Calendar19" runat="server">
                                                                    <SpecialDays>
                                                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                                    </SpecialDays>
                                                                </Calendar>
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                        <tr>
                                                            <td>
                                                                pH:
                                                            </td>
                                                            <td>
                                                                <telerik:RadComboBox ID="cbo_CLS_pH" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                    Height="40px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="pH <= 10" Value="LT" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="pH > 10" Value="GT" runat="server" Owner="" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </td>
                                                            <td>
                                                                Chloride(ppm):
                                                            </td>
                                                            <td>
                                                                <telerik:RadComboBox ID="cbo_CLS_Chl" runat="server" AppendDataBoundItems="True"
                                                                    AutoPostBack="True" DropDownWidth="150px" EmptyMessage="Select" Filter="StartsWith"
                                                                    Height="80px" Width="150px" OnSelectedIndexChanged="OnSelectedIndexChanged_CLS">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem runat="server" Owner="" Text="1-10" Value="oneten" />
                                                                        <telerik:RadComboBoxItem runat="server" Owner="" Text="11-100" Value="elhund" />
                                                                        <telerik:RadComboBoxItem runat="server" Owner="" Text="101-1000" Value="hundthousand" />
                                                                        <telerik:RadComboBoxItem runat="server" Owner="" Text=">1000" Value="gtthousand" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Susceptibility &amp; Svi:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_CLS_Svi" runat="server" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td colspan="2">
                                                                Is there is a known cracking:
                                                                <asp:CheckBox ID="chk_CLS_known" runat="server" AutoPostBack="True" OnCheckedChanged="chk_CLS_known_CheckedChanged" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Df-CLSCC:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_CLS_Df" runat="server" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                                <asp:Button ID="btn_CLS_Calc" Font-Bold="true" runat="server" Text=" Calculate "
                                                                    OnClick="btn_CLS_Calc_Click" />
                                                                &nbsp; &nbsp;&nbsp; &nbsp;
                                                                <asp:Button ID="btn_CLS_Save" Font-Bold="true" runat="server" Text="  Save   " OnClick="btn_CLS_Save_Click" />
                                                            </td>
                                                        </tr>
                                                    </tr>
                                                </table>
                                                <br />
                                            </div>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <%--Panel HSC Cracking--%>
                                    <ajaxToolkit:TabPanel BackColor="#C0D7E8" runat="server" ID="TabPanel8" HeaderText="HSC-HF">
                                        <ContentTemplate>
                                            <div class="text" style="background: -webkit-linear-gradient(#e09432,#f8b117); height: 340px;
                                                overflow: auto;">
                                                <br />
                                                <table width="60%" cellspacing="8">
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:Button ID="btnHSCSubmit" runat="server" Text=" Submit " OnClick="btnHSCSubmit_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label17" Text="Age" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_HSC_Age" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Inspection Effectiveness:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_HSC_InsEff" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="100px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Highly Effective" Value="A" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Usually Effective" Value="B" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Fairly Effective" Value="C" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Poorly Effective" Value="D" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="In Effective" Value="E" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label18" Text="No of Inspection" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_HSC_nofIns" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="120px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="1" Value="1" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="2" Value="2" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="3" Value="3" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="4" Value="4" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="5" Value="5" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="6" Value="6" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Inspection Date:
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="dt_HSC_InspectDate" runat="server" Width="150px" DateInput-EmptyMessage="Select Inspection Date"
                                                                MinDate="01/01/1000" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy">
                                                                <Calendar ID="Calendar20" runat="server">
                                                                    <SpecialDays>
                                                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                                    </SpecialDays>
                                                                </Calendar>
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                        <tr>
                                                            <td>
                                                                PWHT:
                                                            </td>
                                                            <td>
                                                                <telerik:RadComboBox ID="cbo_HSC_PWHT" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                    Height="40px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="Yes" Value="PWHT" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="No" Value="Weld" runat="server" Owner="" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </td>
                                                            <td>
                                                                Brinnel Hardness:
                                                            </td>
                                                            <td>
                                                                <telerik:RadComboBox ID="cbo_HSC_Brinnel" runat="server" AppendDataBoundItems="True"
                                                                    AutoPostBack="True" DropDownWidth="150px" EmptyMessage="Select" Filter="StartsWith"
                                                                    Height="60px" Width="150px" OnSelectedIndexChanged="OnSelectedIndexChanged_HSC">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="< 200" Value="two" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="200 - 237" Value="twothree" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="> 237" Value="gttwothree" runat="server" Owner="" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Susceptibility &amp; Svi:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_HSC_Svi" runat="server" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td colspan="2">
                                                                Is there is a known cracking:
                                                                <asp:CheckBox ID="chk_HSC_known" runat="server" AutoPostBack="True" OnCheckedChanged="chk_HSC_known_CheckedChanged" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Df-HSC-HF:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_HSC_Df" runat="server" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                                <asp:Button ID="btn_HSC_Calc" Font-Bold="true" runat="server" Text=" Calculate "
                                                                    OnClick="btn_HSC_Calc_Click" />
                                                                &nbsp; &nbsp;&nbsp; &nbsp;
                                                                <asp:Button ID="btn_HSC_Save" Font-Bold="true" runat="server" Text="  Save   " OnClick="btn_HSC_Save_Click" />
                                                            </td>
                                                        </tr>
                                                    </tr>
                                                </table>
                                                <br />
                                            </div>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <%--Panel HF Cracking--%>
                                    <ajaxToolkit:TabPanel BackColor="#C0D7E8" runat="server" ID="TabPanel9" HeaderText="HIC/SOHIC-HF">
                                        <ContentTemplate>
                                            <div class="text" style="background: -webkit-linear-gradient(#e09432,#f8b117); height: 340px;
                                                overflow: auto;">
                                                <br />
                                                <table width="60%" cellspacing="8">
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:Button ID="btnHFSubmit" runat="server" Text=" Submit " OnClick="btnHFSubmit_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label19" Text="Age" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_HF_Age" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Inspection Effectiveness:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_HF_InsEff" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="100px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Highly Effective" Value="A" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Usually Effective" Value="B" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Fairly Effective" Value="C" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="Poorly Effective" Value="D" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="In Effective" Value="E" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label20" Text="No of Inspection" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_HF_nofIns" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="120px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="1" Value="1" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="2" Value="2" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="3" Value="3" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="4" Value="4" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="5" Value="5" runat="server" Owner="" />
                                                                    <telerik:RadComboBoxItem Text="6" Value="6" runat="server" Owner="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Inspection Date:
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="dt_HF_InspectDate" runat="server" Width="150px" DateInput-EmptyMessage="Select Inspection Date"
                                                                MinDate="01/01/1000" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy">
                                                                <Calendar ID="Calendar21" runat="server">
                                                                    <SpecialDays>
                                                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                                    </SpecialDays>
                                                                </Calendar>
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                        <tr>
                                                            <td>
                                                                PWHT:
                                                            </td>
                                                            <td>
                                                                <telerik:RadComboBox ID="cbo_HF_pwht" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                    Height="40px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="Yes" Value="PWHT" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="No" Value="Weld" runat="server" Owner="" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </td>
                                                            <td>
                                                                Construction Material:
                                                            </td>
                                                            <td>
                                                                <telerik:RadComboBox ID="cbo_HF_Mat" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                                                    DropDownWidth="150px" EmptyMessage="Select" Filter="StartsWith" Height="60px"
                                                                    Width="150px" OnSelectedIndexChanged="OnSelectedIndexChanged_HF">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="High Sulfar Steel" Value="1" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="Low Sulfar Steel" Value="2" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="Ultra Sulfar Steel" Value="3" runat="server" Owner="" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Susceptibility &amp; Svi:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_HF_Svi" runat="server" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td colspan="2">
                                                                Is there is a known cracking:
                                                                <asp:CheckBox ID="chk_HF_known" runat="server" AutoPostBack="True" OnCheckedChanged="chk_HF_known_CheckedChanged" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Df-SOHIC:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_HF_Df" runat="server" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                                <asp:Button ID="btn_HF_Calc" Font-Bold="true" runat="server" Text=" Calculate " OnClick="btn_HF_Calc_Click" />
                                                                &nbsp; &nbsp;&nbsp; &nbsp;
                                                                <asp:Button ID="btn_HF_Save" Font-Bold="true" runat="server" Text="  Save   " OnClick="btn_HF_Save_Click" />
                                                            </td>
                                                        </tr>
                                                    </tr>
                                                </table>
                                                <br />
                                            </div>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                </ajaxToolkit:TabContainer>
                                <%-- TabContainer HTHA Damage Factor--%>
                                <ajaxToolkit:TabContainer runat="server" ID="tab_HTHA" Height="350px" ActiveTabIndex="0"
                                    Font-Bold="true" Width="50%" BackColor="#C0D7E8">
                                    <ajaxToolkit:TabPanel BackColor="#C0D7E8" runat="server" ID="TabPanel12" HeaderText="HTHA Damage Factor">
                                        <ContentTemplate>
                                            <div class="text" style="background: -webkit-linear-gradient(#e09432,#f8b117); height: 350px;
                                                overflow: auto;">
                                                <table width="60%" cellspacing="8">
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:Button ID="btnHTHASubmit" runat="server" Text=" Submit " OnClick="btnHTHASubmit_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Inspection Effectiveness:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_HTHA_insEff" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="60px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Usually Effective" Value="B" />
                                                                    <telerik:RadComboBoxItem Text="Fairly Effective" Value="C" />
                                                                    <telerik:RadComboBoxItem Text="Poorly Effective" Value="D" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            No of Inspection:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_HTHA_nofIns" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="80px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem runat="server" Text="1" Value="1" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="2" Value="2" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="3" Value="3" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="4" Value="4" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="5" Value="5" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="6" Value="6" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Inspection Date:
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="dt_HTHA_InspectDate" runat="server" Width="150px" DateInput-EmptyMessage="Select Inspection Date"
                                                                MinDate="01/01/1900" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy">
                                                                <Calendar ID="Calendar22" runat="server">
                                                                    <SpecialDays>
                                                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                                    </SpecialDays>
                                                                </Calendar>
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                        <td>
                                                            Age(Exposure time):
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_HTHA_Age" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Exposure Temperature:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_HTHA_Extemp" runat="server" Width="100px"></asp:TextBox>
                                                            °C
                                                        </td>
                                                        <td>
                                                            Heat Treatment:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_HTHA_Heat" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="60px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Annealed" Value="B" />
                                                                    <telerik:RadComboBoxItem Text="Normalized" Value="C" />
                                                                    <telerik:RadComboBoxItem Text="None" Value="D" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <strong>P</strong>H2:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_HTHA_ph2" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <strong>P</strong>v:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_HTHA_pv" runat="server" Width="110px" Enabled="false"></asp:TextBox><asp:Button
                                                                ID="btn_HTHA_Pv" runat="server" Text="Pv" Width="30px" OnClick="btn_HTHA_Pv_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Construction Material:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_HTHA_mat" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="80px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select"
                                                                AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_HTHA_mat">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem runat="server" Owner="" Text="Carbon Steel" Value="CS" />
                                                                    <telerik:RadComboBoxItem runat="server" Owner="" Text="C-0.5Mo-A" Value="Mo" />
                                                                    <telerik:RadComboBoxItem runat="server" Owner="" Text="C-0.5M-N" Value="MN" />
                                                                    <telerik:RadComboBoxItem runat="server" Owner="" Text="1 Cr-0.5Mo" Value="1cr" />
                                                                    <telerik:RadComboBoxItem runat="server" Owner="" Text="1.25 Cr-0.5Mo" Value="125cr" />
                                                                    <telerik:RadComboBoxItem runat="server" Owner="" Text="2.25 Cr-1Mo" Value="225cr" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Susceptibility:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_HTHA_svi" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            Damage Observed:
                                                            <asp:CheckBox ID="chk_HTHA_Damage" runat="server" AutoPostBack="true" OnCheckedChanged="chk_HTHA_Damage_CheckedChanged" />
                                                        </td>
                                                        <td>
                                                            Inspection:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_HTHA_1st2ndIns" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="40px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="First" Value="1" />
                                                                    <telerik:RadComboBoxItem Text="Second" Value="2" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Df-HTHA:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_HTHA_Df" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="1">
                                                            <asp:Button ID="btn_HTHA_Calc" runat="server" Text=" Calculate " OnClick="btn_HTHA_Calc_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:Button ID="btn_HTHA_Save" runat="server" Text="  Save   " OnClick="btn_HTHA_Save_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                            </div>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                </ajaxToolkit:TabContainer>
                                <%-- TabContainer Mechanical Fatigue Damage Factor--%>
                                <ajaxToolkit:TabContainer runat="server" ID="tab_Mech_Fati" Height="350px" ActiveTabIndex="0"
                                    Font-Bold="true" Width="90%" BackColor="#C0D7E8">
                                    <ajaxToolkit:TabPanel BackColor="#C0D7E8" runat="server" ID="TabPanel17" HeaderText="Mechanical Fatigue Damage Factor">
                                        <ContentTemplate>
                                            <div class="text" style="background: -webkit-linear-gradient(#e09432,#f8b117); height: 350px;
                                                overflow: auto;">
                                                <br />
                                                <br />
                                                <table width="90%" cellspacing="8">
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:Button ID="btnMechSubmit" runat="server" Text=" Submit " OnClick="btnMechSubmit_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                Number of previous failure have occured Dfb-PF:
                                                            </td>
                                                            <td>
                                                                <telerik:RadComboBox ID="cbo_Mech_DFBPF" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                    Height="60px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="None" Value="1" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="One" Value="50" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="Greater Than One" Value="500" runat="server" Owner="" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </td>
                                                            <td>
                                                                Amount of Visible/Audible shaking Dfb-AS:
                                                            </td>
                                                            <td>
                                                                <telerik:RadComboBox ID="cbo_Mech_DFBAS" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                    Height="60px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="Minor" Value="1" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="Moderate" Value="50" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="Severe" Value="500" runat="server" Owner="" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                Amount of Visible/Audible shaking for year Ffb-AS:
                                                            </td>
                                                            <td>
                                                                <telerik:RadComboBox ID="cbo_Mech_FFBAS" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                    Height="60px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="Shaking < 2 Weeks" Value="1.0" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="Shaking 2-13 Weeks" Value="0.2" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="Shaking 13-52 Weeks" Value="0.02" runat="server" Owner="" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </td>
                                                            <td>
                                                                Type of Cyclic loading connected Directly or Indirectly Dfb-CF:
                                                            </td>
                                                            <td>
                                                                <telerik:RadComboBox ID="cbo_Mech_DFBCF" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                    Height="80px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select"
                                                                    AutoPostBack="True" OnSelectedIndexChanged="OnSelectedIndexChanged_Mech_Dfb">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="Reciprocating Machinery" Value="50" runat="server"
                                                                            Owner="" />
                                                                        <telerik:RadComboBoxItem Text="PRV Chatter" Value="25" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="Valve high drop press" Value="10" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="None" Value="1" runat="server" Owner="" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                Dfb-mfat:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_Mech_Dfb" runat="server" Enabled="false" Width="150px"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                Adjustment for Corrective Action F-CA:
                                                            </td>
                                                            <td>
                                                                <telerik:RadComboBox ID="cbo_Mech_FCA" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                    Height="60px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="Mod Eng Analysis" Value="0.002" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="Mod Experience" Value="0.2" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="No Modification" Value="2.0" runat="server" Owner="" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                Adjustment for Pipe complexity F-PC:
                                                            </td>
                                                            <td>
                                                                <telerik:RadComboBox ID="cbo_Mech_FPC" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                    Height="60px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="0-5" Value="0.5" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="6-10" Value="1.0" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text=">10" Value="2.0" runat="server" Owner="" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </td>
                                                            <td>
                                                                Adjustment for Condition of pipe F-CP:
                                                            </td>
                                                            <td>
                                                                <telerik:RadComboBox ID="cbo_Mech_FCP" runat="server" Filter="StartsWith" DropDownWidth="250px"
                                                                    Height="60px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="Missing Damage Improper Support" Value="2.0" runat="server"
                                                                            Owner="" />
                                                                        <telerik:RadComboBoxItem Text="Gusset Welded to Pipe" Value="2" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="Good Condition" Value="1" runat="server" Owner="" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                Adjustment for joint type of Branch Design F-JB:
                                                            </td>
                                                            <td>
                                                                <telerik:RadComboBox ID="cbo_Mech_FJP" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                    Height="60px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="Fitting Saddled" Value="1.0" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="Tee/Weldolet" Value="0.2" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text="Sweepolets" Value="0.02" runat="server" Owner="" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </td>
                                                            <td>
                                                                Adjustment for Branch Diameter F-BD:
                                                            </td>
                                                            <td>
                                                                <telerik:RadComboBox ID="cbo_Mech_FBD" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                    Height="40px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="<2NPS" Value="1.0" runat="server" Owner="" />
                                                                        <telerik:RadComboBoxItem Text=">2NPS" Value="0.02" runat="server" Owner="" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                Df-mfat:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_Mech_Df" runat="server" Enabled="false" Width="150px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="1">
                                                                <asp:Button ID="btn_Mech_Calc" runat="server" Text=" Calculate " OnClick="btn_Mech_Calc_Click"
                                                                    Font-Bold="True" />
                                                            </td>
                                                            <td colspan="2">
                                                                <asp:Button ID="btn_Mech_Save" runat="server" Text="  Save   " OnClick="btn_Mech_Save_Click"
                                                                    Font-Bold="True" />
                                                            </td>
                                                        </tr>
                                                </table>
                                                <br />
                                            </div>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                </ajaxToolkit:TabContainer>
                                <%-- TabContainer Brittle Facture Damage Factor--%>
                                <ajaxToolkit:TabContainer runat="server" ID="tab_Brit_Fract" Height="360px" ActiveTabIndex="0"
                                    Font-Bold="true" Width="50%" BackColor="#C0D7E8">
                                    <%--Panel Brittle Facture--%>
                                    <ajaxToolkit:TabPanel BackColor="#C0D7E8" runat="server" ID="TabPanel13" HeaderText="Brittle Facture">
                                        <ContentTemplate>
                                            <div class="text" style="background: -webkit-linear-gradient(#e09432,#f8b117); height: 360px;
                                                overflow: auto;">
                                                <br />
                                                <table width="60%" cellspacing="8">
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:Button ID="btnBrittSubmit" runat="server" Text=" Submit " OnClick="btnBrittleSubmit_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            Process control exist is fully pressurized:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_Brittle_fulpressure" runat="server" Filter="StartsWith"
                                                                DropDownWidth="150px" Height="40px" Width="150px" AppendDataBoundItems="True"
                                                                EmptyMessage="Select" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_Brittle_fulpressure">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Yes" Value="1" />
                                                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Operating Temp:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Britt_OpTemp" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbl_Brittle_Mintemp" runat="server" Text=" Min Temp:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Britt_Dsgn" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            ATM_Liquid Boiling_Point:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Britt_ATM" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Tmin:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Britt_Tmin" runat="server" Width="110px" Enabled="false"></asp:TextBox><asp:Button
                                                                ID="btn_Brittle_Tmin" runat="server" Text="Tmin" Width="40px" OnClick="btn_Brittle_Tmin_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Tref:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Britt_Tref" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Componet Thickness:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_Britt_CmpThick" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="180px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="6.4" Value="sixfour" />
                                                                    <telerik:RadComboBoxItem Text="12.7" Value="onetwo" />
                                                                    <telerik:RadComboBoxItem Text="25.4" Value="twofive" />
                                                                    <telerik:RadComboBoxItem Text="38.1" Value="threeeight" />
                                                                    <telerik:RadComboBoxItem Text="50.8" Value="fivezero" />
                                                                    <telerik:RadComboBoxItem Text="63.5" Value="sixthree" />
                                                                    <telerik:RadComboBoxItem Text="76.2" Value="sevensix" />
                                                                    <telerik:RadComboBoxItem Text="88.9" Value="eighteight" />
                                                                    <telerik:RadComboBoxItem Text="101.6" Value="onezero" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            PWHT:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_Britt_PWHT" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="40px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select"
                                                                AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_cbo_Britt_PWHT">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Yes" Value="Ref_PWHTyes" />
                                                                    <telerik:RadComboBoxItem Text="No" Value="Ref_PWHTno" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Dfb-Brittle Fracture:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Britt_Dfb" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            FSE:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_Britt_FSE" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="40px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="0.01" Value="0.01" />
                                                                    <telerik:RadComboBoxItem Text="1.0" Value="1.0" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Df-britfract:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Britt_Df" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="1">
                                                            <asp:Button ID="btn_Britt_Calc" runat="server" Text=" Calculate " OnClick="btn_Britt_Calc_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:Button ID="btn_Britt_Save" runat="server" Text="  Save   " OnClick="btn_Britt_Save_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                            </div>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <%--Panel Temper Embrittlement--%>
                                    <ajaxToolkit:TabPanel BackColor="#C0D7E8" runat="server" ID="TabPanel14" HeaderText="Temper Embrittlement">
                                        <ContentTemplate>
                                            <div class="text" style="background: -webkit-linear-gradient(#e09432,#f8b117); height: 360px;
                                                overflow: auto;">
                                                <br />
                                                <table width="60%" cellspacing="8">
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:Button ID="btnTemperSubmit" runat="server" Text=" Submit " OnClick="btnTemperSubmit_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            Process control exist is fully pressurized:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_Temper_Fullpressure" runat="server" Filter="StartsWith"
                                                                DropDownWidth="150px" Height="40px" Width="150px" AppendDataBoundItems="True"
                                                                EmptyMessage="Select" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_Temper_Fullpressure">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Yes" Value="1" />
                                                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Operating Temp:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Temper_optemp" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbl_Temper_Min" runat="server" Text=" Min Temp:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Temper_MinDsgn" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Tmin:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Temper_Tmin" runat="server" Width="110px" Enabled="false"></asp:TextBox><asp:Button
                                                                ID="Button1" runat="server" Text="Tmin" Width="40px" OnClick="btn_Temper_Tmin_Click" />
                                                        </td>
                                                        <td>
                                                            Tref:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Temper_Tref" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                Did FATT Value is known:
                                                            </td>
                                                            <td>
                                                                <telerik:RadComboBox ID="cbo_Temper_FATT" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                    Height="40px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select"
                                                                    AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_Temper_FATT">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="Yes" Value="1" />
                                                                        <telerik:RadComboBoxItem Text="No" Value="2" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                        </tr>
                                                        <td>
                                                            FATT:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Temper_Fattval" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Componet Thickness:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_Temper_cmpThick" runat="server" Filter="StartsWith"
                                                                DropDownWidth="150px" Height="180px" Width="150px" AppendDataBoundItems="True"
                                                                EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="6.4" Value="sixfour" />
                                                                    <telerik:RadComboBoxItem Text="12.7" Value="onetwo" />
                                                                    <telerik:RadComboBoxItem Text="25.4" Value="twofive" />
                                                                    <telerik:RadComboBoxItem Text="38.1" Value="threeeight" />
                                                                    <telerik:RadComboBoxItem Text="50.8" Value="fivezero" />
                                                                    <telerik:RadComboBoxItem Text="63.5" Value="sixthree" />
                                                                    <telerik:RadComboBoxItem Text="76.2" Value="sevensix" />
                                                                    <telerik:RadComboBoxItem Text="88.9" Value="eighteight" />
                                                                    <telerik:RadComboBoxItem Text="101.6" Value="onezero" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            PWHT:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_Temper_PWHT" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="40px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select"
                                                                AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_Temper_PWHT">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Yes" Value="Ref_PWHTyes" />
                                                                    <telerik:RadComboBoxItem Text="No" Value="Ref_PWHTno" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Dfc-temp:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Temper_Dfb" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            FSE:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_Temper_FSE" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="40px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="0.01" Value="0.01" />
                                                                    <telerik:RadComboBoxItem Text="1.0" Value="1.0" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Df - tempe:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Temper_Df" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="1">
                                                            <asp:Button ID="btn_Temper_Calc" runat="server" Text=" Calculate " OnClick="btn_Temper_Calc_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:Button ID="btn_Temper_Save" runat="server" Text="  Save   " OnClick="btn_Temper_Save_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                            </div>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <%--Panel 885 Embrittlement--%>
                                    <ajaxToolkit:TabPanel BackColor="#C0D7E8" runat="server" ID="TabPanel15" HeaderText="885 Embrittlement">
                                        <ContentTemplate>
                                            <div class="text" style="background: -webkit-linear-gradient(#e09432,#f8b117); height: 360px;
                                                overflow: auto;">
                                                <br />
                                                <table width="60%" cellspacing="8">
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:Button ID="btn885Submit" runat="server" Text=" Submit " OnClick="btn885Submit_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            Process control exist is fully pressurized:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_885_Fullpressure" runat="server" Filter="StartsWith"
                                                                DropDownWidth="150px" Height="40px" Width="150px" AppendDataBoundItems="True"
                                                                EmptyMessage="Select" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_885_Fullpressure">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Yes" Value="1" />
                                                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Operating Temp:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_885_Optemp" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbl_885_MinDsgn" runat="server" Text=" Min Temp:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_885_MinDsgn" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Tmin:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_885_Tmin" runat="server" Width="110px" Enabled="false"></asp:TextBox><asp:Button
                                                                ID="btn_885_Tmin" runat="server" Text="Tmin" Width="40px" OnClick="btn_885_Tmin_Click" />
                                                        </td>
                                                        <td>
                                                            Componet Thickness:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_885_CmpThick" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="180px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="6.4" Value="sixfour" />
                                                                    <telerik:RadComboBoxItem Text="12.7" Value="onetwo" />
                                                                    <telerik:RadComboBoxItem Text="25.4" Value="twofive" />
                                                                    <telerik:RadComboBoxItem Text="38.1" Value="threeeight" />
                                                                    <telerik:RadComboBoxItem Text="50.8" Value="fivezero" />
                                                                    <telerik:RadComboBoxItem Text="63.5" Value="sixthree" />
                                                                    <telerik:RadComboBoxItem Text="76.2" Value="sevensix" />
                                                                    <telerik:RadComboBoxItem Text="88.9" Value="eighteight" />
                                                                    <telerik:RadComboBoxItem Text="101.6" Value="onezero" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <tr>
                                                        </tr>
                                                        <td colspan="3">
                                                            Did Brittle Transition Temperature is known:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_885_Trefknown" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="40px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select"
                                                                AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_885_FATT">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Yes" Value="1" />
                                                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Tref:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_885_Tref" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Df - 885:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_885_Df" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="1">
                                                            <asp:Button ID="btn_885_Calc" runat="server" Text=" Calculate " OnClick="btn_885_Calc_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:Button ID="btn_885_Save" runat="server" Text="  Save   " OnClick="btn_885_Save_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                            </div>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <%--Panel Sigma Phase Embrittlement--%>
                                    <ajaxToolkit:TabPanel BackColor="#C0D7E8" runat="server" ID="TabPanel16" HeaderText="Sigma Phase Embrittlement">
                                        <ContentTemplate>
                                            <div class="text" style="background: -webkit-linear-gradient(#e09432,#f8b117); height: 360px;
                                                overflow: auto;">
                                                <br />
                                                <br />
                                                <table width="60%" cellspacing="8">
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:Button ID="btnSigmaSubmit" runat="server" Text=" Submit " OnClick="btnSigmaSubmit_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Tmin:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_Sigma_Tmin" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="220px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="649" Value="649" />
                                                                    <telerik:RadComboBoxItem Text="538" Value="538" />
                                                                    <telerik:RadComboBoxItem Text="427" Value="427" />
                                                                    <telerik:RadComboBoxItem Text="316" Value="316" />
                                                                    <telerik:RadComboBoxItem Text="204" Value="204" />
                                                                    <telerik:RadComboBoxItem Text="93" Value="93" />
                                                                    <telerik:RadComboBoxItem Text="66" Value="66" />
                                                                    <telerik:RadComboBoxItem Text="38" Value="38" />
                                                                    <telerik:RadComboBoxItem Text="10" Value="10" />
                                                                    <telerik:RadComboBoxItem Text="-18" Value="-18" />
                                                                    <telerik:RadComboBoxItem Text="-46" Value="-46" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Sigma Function:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_Sigma_Function" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="60px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Low Sigma" Value="Low" />
                                                                    <telerik:RadComboBoxItem Text="Medium Sigma" Value="Medium" />
                                                                    <telerik:RadComboBoxItem Text="High Sigma" Value="High" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Df - Sigma:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Sigma_Df" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="1">
                                                            <asp:Button ID="btn_Sigma_Calc" runat="server" Text=" Calculate " OnClick="btn_Sigma_Calc_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:Button ID="btn_Sigma_Save" runat="server" Text="  Save   " OnClick="btn_Sigma_Save_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                            </div>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                </ajaxToolkit:TabContainer>
                                <%-- Tab POF Total --%>
                                <ajaxToolkit:TabContainer runat="server" ID="tab_POF_Total" Height="220px" ActiveTabIndex="0"
                                    Font-Bold="true" Width="50%" BackColor="#C0D7E8">
                                    <ajaxToolkit:TabPanel BackColor="#C0D7E8" runat="server" ID="tabpanel_POF_Total"
                                        HeaderText="POF Total">
                                        <ContentTemplate>
                                            <div class="text" style="background: -webkit-linear-gradient(#e09432,#f8b117); height: 220px;
                                                overflow: auto;">
                                                <br />
                                                <br />
                                                <table width="90%" cellspacing="2">
                                                    <tr>
                                                        <td colspan="3">
                                                            <asp:Button ID="btnFinalSubmit" runat="server" Text=" Submit " OnClick="btnFinalSubmit_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnPOFTotalDelete" runat="server" Text=" Delete " OnClick="btnPOFTotalDelete_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Df-Thinning gov:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Df_Thinninggov" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Df-SCC gov:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Df_SCCgov" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Df-extd gov:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Df_extdgov" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Df-brit gov:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Df_britgov" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Df-total:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Df_totalgov" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Category:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Category" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="1">
                                                            <asp:Button ID="btn_Final_Calc" runat="server" Text=" Calculate " OnClick="btn_Final_Calc_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:Button ID="btn_Final_Save" runat="server" Text="  Save   " OnClick="btn_Final_Save_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                    </tr>
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
