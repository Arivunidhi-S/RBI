<%@ Page Language="C#" AutoEventWireup="true" CodeFile="COF.aspx.cs" Inherits="COF" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<link rel="shortcut icon" href="Images/BoilerIcon.png" />
    <title>COF Details</title>
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
                                <asp:Button ID="btn_Flammable" runat="server" Text="Flammable" OnClick="btn_Flammable_Click"
                                    Font-Bold="true" class="box" />
                                &nbsp;
                                <asp:Button ID="btn_NonFlammable" runat="server" Text="NonFlammable" OnClick="btn_NonFlammable_Click"
                                    Font-Bold="true" class="box" />
                                &nbsp;
                                <%-- <asp:Button ID="btn_External" runat="server" Text="External Damage" OnClick="btn_External_Click"
                                    Font-Bold="true" class="box"/>
                                &nbsp;
                                <asp:Button ID="btn_SCC" runat="server" Text="Stress Corrosion" OnClick="btn_SCC_Click"
                                    Font-Bold="true" class="box"/>
                                &nbsp;
                                <asp:Button ID="btn_HTHA" runat="server" Text="High Temperature" Font-Bold="true"
                                    OnClick="btn_HTHA_Click" class="box"/>
                                &nbsp;
                                <asp:Button ID="btn_Mech_Fati" runat="server" Text="Mechanical Fatigue" Font-Bold="true"
                                    OnClick="btn_Mech_Fati_Click" class="box"/>
                                &nbsp;
                                <asp:Button ID="btn_Brit_Frac" runat="server" Text="Brittle Facture" Font-Bold="true"
                                    OnClick="btn_Brit_Frac_Click" class="box"/>
                                &nbsp;--%>
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
                                                        DataValueField="CompNo" Width="150px" AppendDataBoundItems="True"
                                                        EmptyMessage="Select" DataTextField="CompName" >
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
                            <td align="center">
                              <%--  <div style="font-size: xx-large; color: #f5f5f5; font-family: Algerian; font-weight: lighter;
                                    text-shadow: 0px 15px 5px rgba(0,0,0,0.1),
                 10px 20px 5px rgba(0,0,0,0.05),
                 -10px 20px 5px rgba(0,0,0,0.05);">
                                    <br />
                                </div>--%>
                                  <br />
                                <%-- Tab Flammable--%>
                                <ajaxToolkit:TabContainer runat="server" ID="tab_Flammable" Height="440px" ActiveTabIndex="0"
                                    Font-Bold="true" Width="50%" BackColor="#C0D7E8">
                                    <ajaxToolkit:TabPanel BackColor="#C0D7E8" runat="server" ID="TabPanel1" HeaderText="Flammable">
                                        <ContentTemplate>
                                            <div class="text" style="background: -webkit-linear-gradient(#e09432,#f8b117); height: 440px;
                                                overflow: auto;">
                                               <br />
                                                <table width="65%" cellspacing="2">
                                                 <tr> <td>
                                                            Fluid Stored:
                                                        </td>
                                                        <td>
                                                             <asp:TextBox ID="txt_FluidStored" runat="server" Width="150px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnCOFFlameSubmit" runat="server" Text=" Submit " OnClick="btnCOFFlameSubmit_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                         <td align="right">
                                                            <asp:Button ID="btnCOFFlameDelete" runat="server" Text=" Delete " OnClick="btnCOFFlameDelete_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Fluid Properties:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_FluidStored" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                AutoPostBack="true" Height="40px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select"
                                                                OnSelectedIndexChanged="OnSelectedIndexChanged_FluidStored">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Liquid" Value="Liquid" />
                                                                    <telerik:RadComboBoxItem Text="Gas or Vapour" Value="Gas" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Representative Fluid:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_RepFluid" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                AutoPostBack="true" Height="100px" Width="150px" AppendDataBoundItems="True"
                                                                EmptyMessage="Select" OnSelectedIndexChanged="OnSelectedIndexChanged_RepFluid">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Final Phase:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_FinalPhase" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="60px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Liquid" Value="A" />
                                                                    <telerik:RadComboBoxItem Text="Vapor" Value="B" />
                                                                    <telerik:RadComboBoxItem Text="Powder" Value="C" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Fluid Type:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_FluidType" runat="server" Width="150px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Operating Pressure:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_OpPressure" runat="server" Width="150px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Operating Temp:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_OpTemp" runat="server" Width="150px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            P1:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_COF_P1" runat="server" Width="150px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Mass,lbs:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_COF_Masslbs" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            Detection:
                                                        </td>
                                                        <td valign="top">
                                                            <telerik:RadComboBox ID="cbo_COF_Detection" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                AutoPostBack="true" Height="60px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select"
                                                                OnSelectedIndexChanged="OnSelectedIndexChanged_Detection">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="A" Value="A" />
                                                                    <telerik:RadComboBoxItem Text="B" Value="B" />
                                                                    <telerik:RadComboBoxItem Text="C" Value="C" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:Label ID="lbl_COF_Detection" runat="server" Height="60px" Width="300px" Wrap="true"
                                                                ForeColor="Maroon"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            Isolation:
                                                        </td>
                                                        <td valign="top">
                                                            <telerik:RadComboBox ID="cbo_COF_Isolation" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                AutoPostBack="true" Height="60px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select"
                                                                OnSelectedIndexChanged="OnSelectedIndexChanged_Isolation">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="A" Value="A" />
                                                                    <telerik:RadComboBoxItem Text="B" Value="B" />
                                                                    <telerik:RadComboBoxItem Text="C" Value="C" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:Label ID="lbl_COF_Isolation" runat="server" Height="50px" Width="300px" Wrap="true"
                                                                ForeColor="Blue"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            Mitigation System:
                                                        </td>
                                                        <td valign="top" colspan="2">
                                                            <telerik:RadComboBox ID="cbo_COF_Mitigation" runat="server" Filter="StartsWith" DropDownWidth="250px"
                                                                Width="250px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Inventory blondown,coupled with isolation system clasification B or higher"
                                                                        Value="0.25" />
                                                                    <telerik:RadComboBoxItem Text="Fire water deluge system and monitors" Value="0.20" />
                                                                    <telerik:RadComboBoxItem Text="Fire water monitors only" Value="0.05" />
                                                                    <telerik:RadComboBoxItem Text="Foam spray system" Value="0.15" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                           Component Damage:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_COF_CmpDmg" runat="server" Width="150px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                           Personnel Injury:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_COF_PerInj" runat="server" Width="150px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Damage Category:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_COF_DmCategory" runat="server" Width="150px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Injury Category:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_COF_InCategory" runat="server" Width="150px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="1">
                                                            <asp:Button ID="btn_COF_Cal" runat="server" Text=" Calculate " Font-Bold="true" OnClick="btn_COF_Calc_Click" />
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:Button ID="btn_COF_Save" runat="server" Text="  Cancel   " Font-Bold="true" OnClick="btn_COF_Cancel_Click" />
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
                                <%-- Tab NonFlammable--%>
                                <ajaxToolkit:TabContainer runat="server" ID="tab_NonFlammable" Height="420px" ActiveTabIndex="0"
                                    Font-Bold="true" Width="50%" BackColor="#C0D7E8">
                                    <ajaxToolkit:TabPanel BackColor="#C0D7E8" runat="server" ID="TabPanel2" HeaderText="NonFlammable">
                                        <ContentTemplate>
                                            <div class="text" style="background: -webkit-linear-gradient(#e09432,#f8b117); height: 420px;
                                                overflow: auto;">
                                                <br />
                                                <table width="60%" cellspacing="2">
                                                 <tr>
                                                  <td>
                                                            Fluid Stored:
                                                        </td>
                                                        <td>
                                                             <asp:TextBox ID="txt_Non_FluidStored" runat="server" Width="150px"></asp:TextBox>
                                                        </td>
                                                      
                                                        <td>
                                                            <asp:Button ID="btnCOFNonFlameSubmit" runat="server" Text=" Submit " OnClick="btnCOFNonFlameSubmit_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                         <td align="right">
                                                            <asp:Button ID="btnCOFNonFlameDelete" runat="server" Text=" Delete " OnClick="btnCOFNonFlameDelete_Click"
                                                                Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Fluid Properties:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_COF_NonFluid" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                AutoPostBack="true" Height="40px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select"
                                                                OnSelectedIndexChanged="NonFlammable_OnSelectedIndexChanged_FluidStored">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Liquid" Value="Liquid" />
                                                                    <telerik:RadComboBoxItem Text="Gas or Vapour" Value="Gas" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Representative Fluid:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_COF_NonRefFluid" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                AutoPostBack="true" Height="60px" Width="150px" AppendDataBoundItems="True"
                                                                EmptyMessage="Select" OnSelectedIndexChanged="NonOnSelectedIndexChanged_RepFluid">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Final Phase:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbo_COF_NonFinal" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                Height="60px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Liquid" Value="A" />
                                                                    <telerik:RadComboBoxItem Text="Vapor" Value="B" />
                                                                    <telerik:RadComboBoxItem Text="Powder" Value="C" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Fluid Type:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_COF_Non_FluidType" runat="server" Width="150px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Operating Pressure:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_COF_NonOpPressure" runat="server" Width="150px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Operating Temp:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_COF_NonOpTemp" runat="server" Width="150px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            P1:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_COF_NonP1" runat="server" Width="150px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Mass,lbs:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_COF_NonMass" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            Detection:
                                                        </td>
                                                        <td valign="top">
                                                            <telerik:RadComboBox ID="cbo_COF_NonDetect" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                AutoPostBack="true" Height="60px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select"
                                                                OnSelectedIndexChanged="NonOnSelectedIndexChanged_Detection">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="A" Value="A" />
                                                                    <telerik:RadComboBoxItem Text="B" Value="B" />
                                                                    <telerik:RadComboBoxItem Text="C" Value="C" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:Label ID="lbl_COF_NonDetect" runat="server" Height="60px" Width="300px" Wrap="true" ForeColor="Maroon"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            Isolation:
                                                        </td>
                                                        <td valign="top">
                                                            <telerik:RadComboBox ID="cbo_COF_NonIso" runat="server" Filter="StartsWith" DropDownWidth="150px"
                                                                AutoPostBack="true" Height="60px" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select"
                                                                OnSelectedIndexChanged="NonOnSelectedIndexChanged_Isolation">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="A" Value="A" />
                                                                    <telerik:RadComboBoxItem Text="B" Value="B" />
                                                                    <telerik:RadComboBoxItem Text="C" Value="C" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:Label ID="lbl_COF_NonIso" runat="server" Height="50px" Width="300px" Wrap="true" ForeColor="Blue"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            Mitigation System:
                                                        </td>
                                                        <td valign="top" colspan="2">
                                                            <telerik:RadComboBox ID="cbo_COF_NonMiti" runat="server" Filter="StartsWith" DropDownWidth="250px"
                                                                Width="250px" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Inventory blondown,coupled with isolation system clasification B or higher"
                                                                        Value="0.25" />
                                                                    <telerik:RadComboBoxItem Text="Fire water deluge system and monitors" Value="0.20" />
                                                                    <telerik:RadComboBoxItem Text="Fire water monitors only" Value="0.05" />
                                                                    <telerik:RadComboBoxItem Text="Foam spray system" Value="0.15" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <tr>
                                                            <td>
                                                                Personnel Injury:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_COF_NonPersonnel" runat="server" Width="150px"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                Injury Category:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_COF_NonCategory" runat="server" Width="150px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <td colspan="1">
                                                            <asp:Button ID="btn_COF_NonCalc" runat="server" Text=" Calculate " Font-Bold="true" OnClick="btn_COF_NonCalc_Click" />
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:Button ID="btn_COF_NonSave" runat="server" Text="  Cancel   " Font-Bold="true" OnClick="btn_COF_NonCancel_Click" />
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
