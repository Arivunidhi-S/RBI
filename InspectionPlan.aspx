<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InspectionPlan.aspx.cs" Inherits="InspectionPlan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="shortcut icon" href="Images/BoilerIcon.png" />
    <title>InspectionPlan Details</title>
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
                                   INSPECTION PLAN DETAILS
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
                              
                                <div class="text" style="background: -webkit-linear-gradient(#e09432,#f8b117); 
                                    width: 100%; overflow: auto; border: 4px solid #ffffff; box-shadow: 3px 5px 6px rgba(0,0,0,0.5);">
                                    <br />
                                    <table width="100%" rules="all" border="1">
                                        <tr style="font-weight: bold; font-size: medium">
                                            <td style="width:10%; color:#00008B" align="center">
                                                Inspection Date
                                            </td>
                                            <td style="width:10%; color:#00008B" align="center">
                                                DamageFactor
                                            </td>
                                            <td style="width:10%; color:#00008B" align="center">
                                                InspectionEffect
                                            </td>
                                            <td style="width:25%; color:#00008B" align="center">
                                                Intrusive Inspection
                                            </td>
                                            <td style="width:25%; color:#00008B" align="center">
                                                Non-Intrusive Inspection
                                            </td>
                                            <td style="width:10%; color:#00008B" align="center">
                                                Next Inspection Date
                                            </td>
                                            <td style="width:10%; color:#00008B" align="center">
                                                InspectionEffect
                                            </td>
                                        </tr>
                                        <%--------------1--------Thinning------------------------%>
                                        <tr style="font-weight: bold; font-size: medium">
                                            <td align="center">
                                                <asp:Label Text="" runat="server" ID="lbl_Thin_Date" ForeColor="#006400"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label Text="Thinning" runat="server" ID="lbl_Thin_Damage" ForeColor="DarkRed"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label Text="" runat="server" ID="lbl_Thin_InsEff"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label Text="" runat="server" ID="lbl_Thin_IntruIns"  ForeColor="Maroon"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label Text="" runat="server" ID="lbl_Thin_NonIntruIns" ForeColor="Maroon"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="dt_Thin_InsDt" runat="server" Width="150px" DateInput-EmptyMessage="Select Inspection Date"
                                                    MinDate="01/01/1000" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy">
                                                    <Calendar ID="Calendar8" runat="server">
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                        </SpecialDays>
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cbo_Thin_inspecEffec" runat="server" Filter="StartsWith"
                                                    DropDownWidth="150px" Height="80px" Width="150px" AppendDataBoundItems="True"
                                                    EmptyMessage="Select">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="Highly Effective" Value="A" />
                                                        <telerik:RadComboBoxItem Text="Usually Effective" Value="B" />
                                                        <telerik:RadComboBoxItem Text="Fairly Effective" Value="C" />
                                                        <telerik:RadComboBoxItem Text="Poorly Effective" Value="D" />
                                                        <telerik:RadComboBoxItem Text="In Effective" Value="E" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                           <%-- <td>
                                            <asp:Button ID="btn_Thin_Save" runat="server" Text=" Save " 
                                                                Font-Bold="true" />
                                            </td>--%>
                                        </tr>
                                        <%--------------2--------ExternalCorrosion------------------------%>
                                        <tr style="font-weight: bold; font-size: medium">
                                            <td align="center">
                                                <asp:Label Text="" runat="server" ID="lbl_ExeCor_Date" ForeColor="#006400"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label Text="ExternalCorrosion" runat="server" ID="lbl_ExeCor_Damage" ForeColor="DarkRed"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label Text="" runat="server" ID="lbl_ExeCor_InsEff"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label Text="" runat="server" ID="lbl_ExeCor_IntruIns" ForeColor="Maroon"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label Text="" runat="server" ID="lbl_ExeCor_NonIntruIns" ForeColor="Maroon"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="dt_ExeCor_InsDt" runat="server" Width="150px" DateInput-EmptyMessage="Select Inspection Date"
                                                    MinDate="01/01/1000" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy">
                                                    <Calendar ID="Calendar1" runat="server">
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                        </SpecialDays>
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cbo_ExeCor_inspecEffec" runat="server" Filter="StartsWith"
                                                    DropDownWidth="150px" Height="80px" Width="150px" AppendDataBoundItems="True"
                                                    EmptyMessage="Select">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="Highly Effective" Value="A" />
                                                        <telerik:RadComboBoxItem Text="Usually Effective" Value="B" />
                                                        <telerik:RadComboBoxItem Text="Fairly Effective" Value="C" />
                                                        <telerik:RadComboBoxItem Text="Poorly Effective" Value="D" />
                                                        <telerik:RadComboBoxItem Text="In Effective" Value="E" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                           <%-- <td>
                                            <asp:Button ID="btn_ExeCor_Save" runat="server" Text=" Save "
                                                                Font-Bold="true" />
                                            </td>--%>
                                        </tr>
                                        <%--------------3--------CUI Damage------------------------%>
                                        <tr style="font-weight: bold; font-size: medium">
                                            <td align="center">
                                                <asp:Label Text="" runat="server" ID="lbl_CUIDamage_Date" ForeColor="#006400"></asp:Label>
                                            </td >
                                            <td align="center">
                                                <asp:Label Text="CUI Damage" runat="server" ID="lbl_CUIDamage_Damage" ForeColor="DarkRed"></asp:Label>
                                            </td>
                                            <td align="center"> 
                                                <asp:Label Text="" runat="server" ID="lbl_CUIDamage_InsEff"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label Text="" runat="server" ID="lbl_CUIDamage_IntruIns" ForeColor="Maroon"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label Text="" runat="server" ID="lbl_CUIDamage_NonIntruIns" ForeColor="Maroon"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="dt_CUIDamage_InsDt" runat="server" Width="150px" DateInput-EmptyMessage="Select Inspection Date"
                                                    MinDate="01/01/1000" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy">
                                                    <Calendar ID="CalendarCUIDamage" runat="server">
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                        </SpecialDays>
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cbo_CUIDamage_inspecEffec" runat="server" Filter="StartsWith"
                                                    DropDownWidth="150px" Height="80px" Width="150px" AppendDataBoundItems="True"
                                                    EmptyMessage="Select">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="Highly Effective" Value="A" />
                                                        <telerik:RadComboBoxItem Text="Usually Effective" Value="B" />
                                                        <telerik:RadComboBoxItem Text="Fairly Effective" Value="C" />
                                                        <telerik:RadComboBoxItem Text="Poorly Effective" Value="D" />
                                                        <telerik:RadComboBoxItem Text="In Effective" Value="E" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                            <%-- <td>
                                            <asp:Button ID="btn_CUIDamage_Save" runat="server" Text=" Save " 
                                                                Font-Bold="true" />
                                            </td>--%>
                                        </tr>
                                         <%-------------4---------External CLSCC------------------------%>
                                        <tr style="font-weight: bold; font-size: medium">
                                            <td align="center">
                                                <asp:Label Text="" runat="server" ID="lbl_ExtCLSCC_Date"  ForeColor="#006400"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label Text="External CLSCC" runat="server" ID="lbl_ExtCLSCC_Damage" ForeColor="DarkRed"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label Text="" runat="server" ID="lbl_ExtCLSCC_InsEff"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label Text="" runat="server" ID="lbl_ExtCLSCC_IntruIns" ForeColor="Maroon"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label Text="" runat="server" ID="lbl_ExtCLSCC_NonIntruIns" ForeColor="Maroon"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="dt_ExtCLSCC_InsDt" runat="server" Width="150px" DateInput-EmptyMessage="Select Inspection Date"
                                                    MinDate="01/01/1000" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy">
                                                    <Calendar ID="CalendarExtCLSCC" runat="server">
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                        </SpecialDays>
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cbo_ExtCLSCC_inspecEffec" runat="server" Filter="StartsWith"
                                                    DropDownWidth="150px" Height="80px" Width="150px" AppendDataBoundItems="True"
                                                    EmptyMessage="Select">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="Highly Effective" Value="A" />
                                                        <telerik:RadComboBoxItem Text="Usually Effective" Value="B" />
                                                        <telerik:RadComboBoxItem Text="Fairly Effective" Value="C" />
                                                        <telerik:RadComboBoxItem Text="Poorly Effective" Value="D" />
                                                        <telerik:RadComboBoxItem Text="In Effective" Value="E" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                             <%-- <td>
                                            <asp:Button ID="btn_ExtCLSCC_Save" runat="server" Text=" Save " 
                                                                Font-Bold="true" />
                                            </td>--%>
                                        </tr>
                                        <%--------------5--------External CUI CLSCC------------------------%>
                                        <tr style="font-weight: bold; font-size: medium">
                                            <td align="center">
                                                <asp:Label Text="" runat="server" ID="lbl_ExtCUI_Date" ForeColor="#006400"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label Text="External CUI CLSCC" runat="server" ID="lbl_ExtCUI_Damage" ForeColor="DarkRed"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label Text="" runat="server" ID="lbl_ExtCUI_InsEff"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label Text="" runat="server" ID="lbl_ExtCUI_IntruIns" ForeColor="Maroon"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label Text="" runat="server" ID="lbl_ExtCUI_NonIntruIns" ForeColor="Maroon"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="dt_ExtCUI_InsDt" runat="server" Width="150px" DateInput-EmptyMessage="Select Inspection Date"
                                                    MinDate="01/01/1000" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy">
                                                    <Calendar ID="CalendarExtCUI" runat="server">
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                        </SpecialDays>
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cbo_ExtCUI_inspecEffec" runat="server" Filter="StartsWith"
                                                    DropDownWidth="150px" Height="80px" Width="150px" AppendDataBoundItems="True"
                                                    EmptyMessage="Select">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="Highly Effective" Value="A" />
                                                        <telerik:RadComboBoxItem Text="Usually Effective" Value="B" />
                                                        <telerik:RadComboBoxItem Text="Fairly Effective" Value="C" />
                                                        <telerik:RadComboBoxItem Text="Poorly Effective" Value="D" />
                                                        <telerik:RadComboBoxItem Text="In Effective" Value="E" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                             <%--<td>
                                            <asp:Button ID="btn_ExtCUI_Save" runat="server" Text=" Save " 
                                                                Font-Bold="true" />
                                            </td>--%>
                                        </tr>
                                          <%------------6----------Caustic------------------------%>
                                        <tr style="font-weight: bold; font-size: medium">
                                            <td  align="center">
                                                <asp:Label Text="" runat="server" ID="lbl_Caustic_Date" ForeColor="#006400"></asp:Label>
                                            </td>
                                            <td  align="center">
                                                <asp:Label Text="Caustic" runat="server" ID="lbl_Caustic_Damage" ForeColor="DarkRed"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label Text="" runat="server" ID="lbl_Caustic_InsEff"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label Text="" runat="server" ID="lbl_Caustic_IntruIns" ForeColor="Maroon"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label Text="" runat="server" ID="lbl_Caustic_NonIntruIns" ForeColor="Maroon"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="dt_Caustic_InsDt" runat="server" Width="150px" DateInput-EmptyMessage="Select Inspection Date"
                                                    MinDate="01/01/1000" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy">
                                                    <Calendar ID="CalendarCaustic" runat="server">
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                        </SpecialDays>
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cbo_Caustic_inspecEffec" runat="server" Filter="StartsWith"
                                                    DropDownWidth="150px" Height="80px" Width="150px" AppendDataBoundItems="True"
                                                    EmptyMessage="Select">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="Highly Effective" Value="A" />
                                                        <telerik:RadComboBoxItem Text="Usually Effective" Value="B" />
                                                        <telerik:RadComboBoxItem Text="Fairly Effective" Value="C" />
                                                        <telerik:RadComboBoxItem Text="Poorly Effective" Value="D" />
                                                        <telerik:RadComboBoxItem Text="In Effective" Value="E" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                            <%-- <td>
                                            <asp:Button ID="btn_Caustic_Save" runat="server" Text=" Save " 
                                                                Font-Bold="true" />
                                            </td>--%>
                                        </tr>
                                        <%--------------7--------Amine------------------------%>
                                        <tr style="font-weight: bold; font-size: medium">
                                            <td align="center">
                                                <asp:Label Text="" runat="server" ID="lbl_Amine_Date" ForeColor="#006400"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label Text="Amine" runat="server" ID="lbl_Amine_Damage" ForeColor="DarkRed"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label Text="" runat="server" ID="lbl_Amine_InsEff"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label Text="" runat="server" ID="lbl_Amine_IntruIns" ForeColor="Maroon"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label Text="" runat="server" ID="lbl_Amine_NonIntruIns" ForeColor="Maroon"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="dt_Amine_InsDt" runat="server" Width="150px" DateInput-EmptyMessage="Select Inspection Date"
                                                    MinDate="01/01/1000" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy">
                                                    <Calendar ID="CalendarAmine" runat="server">
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                        </SpecialDays>
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cbo_Amine_inspecEffec" runat="server" Filter="StartsWith"
                                                    DropDownWidth="150px" Height="80px" Width="150px" AppendDataBoundItems="True"
                                                    EmptyMessage="Select">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="Highly Effective" Value="A" />
                                                        <telerik:RadComboBoxItem Text="Usually Effective" Value="B" />
                                                        <telerik:RadComboBoxItem Text="Fairly Effective" Value="C" />
                                                        <telerik:RadComboBoxItem Text="Poorly Effective" Value="D" />
                                                        <telerik:RadComboBoxItem Text="In Effective" Value="E" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                            <%-- <td>
                                            <asp:Button ID="btn_Amine_Save" runat="server" Text=" Save " 
                                                                Font-Bold="true" />
                                            </td>--%>
                                        </tr>
                                        <%--------------8--------Sulfide------------------------%>
                                        <tr style="font-weight: bold; font-size: medium">
                                            <td align="center">
                                                <asp:Label Text="" runat="server" ID="lbl_Sulfide_Date" ForeColor="#006400"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label Text="Sulfide" runat="server" ID="lbl_Sulfide_Damage" ForeColor="DarkRed"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label Text="" runat="server" ID="lbl_Sulfide_InsEff"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label Text="" runat="server" ID="lbl_Sulfide_IntruIns" ForeColor="Maroon"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label Text="" runat="server" ID="lbl_Sulfide_NonIntruIns" ForeColor="Maroon"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="dt_Sulfide_InsDt" runat="server" Width="150px" DateInput-EmptyMessage="Select Inspection Date"
                                                    MinDate="01/01/1000" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy">
                                                    <Calendar ID="CalendarSulfide" runat="server">
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                        </SpecialDays>
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cbo_Sulfide_inspecEffec" runat="server" Filter="StartsWith"
                                                    DropDownWidth="150px" Height="80px" Width="150px" AppendDataBoundItems="True"
                                                    EmptyMessage="Select">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="Highly Effective" Value="A" />
                                                        <telerik:RadComboBoxItem Text="Usually Effective" Value="B" />
                                                        <telerik:RadComboBoxItem Text="Fairly Effective" Value="C" />
                                                        <telerik:RadComboBoxItem Text="Poorly Effective" Value="D" />
                                                        <telerik:RadComboBoxItem Text="In Effective" Value="E" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                            <%-- <td>
                                            <asp:Button ID="btn_Sulfide_Save" runat="server" Text=" Save " 
                                                                Font-Bold="true" />
                                            </td>--%>
                                        </tr>
                                         <%-------------9---------H2S------------------------%>
                                        <tr style="font-weight: bold; font-size: medium">
                                            <td align="center">
                                                <asp:Label Text="" runat="server" ID="lbl_H2S_Date" ForeColor="#006400"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label Text="H2S" runat="server" ID="lbl_H2S_Damage" ForeColor="DarkRed"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label Text="" runat="server" ID="lbl_H2S_InsEff"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label Text="" runat="server" ID="lbl_H2S_IntruIns" ForeColor="Maroon"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label Text="" runat="server" ID="lbl_H2S_NonIntruIns" ForeColor="Maroon"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="dt_H2S_InsDt" runat="server" Width="150px" DateInput-EmptyMessage="Select Inspection Date"
                                                    MinDate="01/01/1000" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy">
                                                    <Calendar ID="CalendarH2S" runat="server">
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                        </SpecialDays>
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cbo_H2S_inspecEffec" runat="server" Filter="StartsWith"
                                                    DropDownWidth="150px" Height="80px" Width="150px" AppendDataBoundItems="True"
                                                    EmptyMessage="Select">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="Highly Effective" Value="A" />
                                                        <telerik:RadComboBoxItem Text="Usually Effective" Value="B" />
                                                        <telerik:RadComboBoxItem Text="Fairly Effective" Value="C" />
                                                        <telerik:RadComboBoxItem Text="Poorly Effective" Value="D" />
                                                        <telerik:RadComboBoxItem Text="In Effective" Value="E" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                             <%--<td>
                                            <asp:Button ID="btn_H2S_Save" runat="server" Text=" Save " 
                                                                Font-Bold="true" />
                                            </td>--%>
                                        </tr>
                                          <%------------10----------Carbonate------------------------%>
                                        <tr style="font-weight: bold; font-size: medium">
                                            <td align="center">
                                                <asp:Label Text="" runat="server" ID="lbl_Carbonate_Date" ForeColor="#006400"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label Text="Carbonate" runat="server" ID="lbl_Carbonate_Damage" ForeColor="DarkRed"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label Text="" runat="server" ID="lbl_Carbonate_InsEff"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label Text="" runat="server" ID="lbl_Carbonate_IntruIns" ForeColor="Maroon"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label Text="" runat="server" ID="lbl_Carbonate_NonIntruIns" ForeColor="Maroon"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="dt_Carbonate_InsDt" runat="server" Width="150px" DateInput-EmptyMessage="Select Inspection Date"
                                                    MinDate="01/01/1000" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy">
                                                    <Calendar ID="CalendarCarbonate" runat="server">
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                        </SpecialDays>
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cbo_Carbonate_inspecEffec" runat="server" Filter="StartsWith"
                                                    DropDownWidth="150px" Height="80px" Width="150px" AppendDataBoundItems="True"
                                                    EmptyMessage="Select">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="Highly Effective" Value="A" />
                                                        <telerik:RadComboBoxItem Text="Usually Effective" Value="B" />
                                                        <telerik:RadComboBoxItem Text="Fairly Effective" Value="C" />
                                                        <telerik:RadComboBoxItem Text="Poorly Effective" Value="D" />
                                                        <telerik:RadComboBoxItem Text="In Effective" Value="E" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                             <%--<td>
                                            <asp:Button ID="btn_Carbonate_Save" runat="server" Text=" Save " 
                                                                Font-Bold="true" />
                                            </td>--%>
                                        </tr>
                                            <%----------11------------PTA------------------------%>
                                        <tr style="font-weight: bold; font-size: medium">
                                            <td align="center">
                                                <asp:Label Text="" runat="server" ID="lbl_PTA_Date" ForeColor="#006400"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label Text="PTA" runat="server" ID="lbl_PTA_Damage" ForeColor="DarkRed"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label Text="" runat="server" ID="lbl_PTA_InsEff"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label Text="" runat="server" ID="lbl_PTA_IntruIns" ForeColor="Maroon"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label Text="" runat="server" ID="lbl_PTA_NonIntruIns" ForeColor="Maroon"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="dt_PTA_InsDt" runat="server" Width="150px" DateInput-EmptyMessage="Select Inspection Date"
                                                    MinDate="01/01/1000" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy">
                                                    <Calendar ID="CalendarPTA" runat="server">
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                        </SpecialDays>
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cbo_PTA_inspecEffec" runat="server" Filter="StartsWith"
                                                    DropDownWidth="150px" Height="80px" Width="150px" AppendDataBoundItems="True"
                                                    EmptyMessage="Select">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="Highly Effective" Value="A" />
                                                        <telerik:RadComboBoxItem Text="Usually Effective" Value="B" />
                                                        <telerik:RadComboBoxItem Text="Fairly Effective" Value="C" />
                                                        <telerik:RadComboBoxItem Text="Poorly Effective" Value="D" />
                                                        <telerik:RadComboBoxItem Text="In Effective" Value="E" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                            <%-- <td>
                                            <asp:Button ID="btn_PTA_Save" runat="server" Text=" Save " 
                                                                Font-Bold="true" />
                                            </td>--%>
                                        </tr>
                                        <%--------------12--------CLSCC------------------------%>
                                        <tr style="font-weight: bold; font-size: medium">
                                            <td align="center">
                                                <asp:Label Text="" runat="server" ID="lbl_CLSCC_Date" ForeColor="#006400"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label Text="CLSCC" runat="server" ID="lbl_CLSCC_Damage" ForeColor="DarkRed"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label Text="" runat="server" ID="lbl_CLSCC_InsEff"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label Text="" runat="server" ID="lbl_CLSCC_IntruIns" ForeColor="Maroon"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label Text="" runat="server" ID="lbl_CLSCC_NonIntruIns" ForeColor="Maroon"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="dt_CLSCC_InsDt" runat="server" Width="150px" DateInput-EmptyMessage="Select Inspection Date"
                                                    MinDate="01/01/1000" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy">
                                                    <Calendar ID="CalendarCLSCC" runat="server">
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                        </SpecialDays>
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cbo_CLSCC_inspecEffec" runat="server" Filter="StartsWith"
                                                    DropDownWidth="150px" Height="80px" Width="150px" AppendDataBoundItems="True"
                                                    EmptyMessage="Select">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="Highly Effective" Value="A" />
                                                        <telerik:RadComboBoxItem Text="Usually Effective" Value="B" />
                                                        <telerik:RadComboBoxItem Text="Fairly Effective" Value="C" />
                                                        <telerik:RadComboBoxItem Text="Poorly Effective" Value="D" />
                                                        <telerik:RadComboBoxItem Text="In Effective" Value="E" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                            <%-- <td>
                                            <asp:Button ID="btn_CLSCC_Save" runat="server" Text=" Save " 
                                                                Font-Bold="true" />
                                            </td>--%>
                                        </tr>
                                            <%----------13------------HSC-HF------------------------%>
                                        <tr style="font-weight: bold; font-size: medium">
                                            <td align="center">
                                                <asp:Label Text="" runat="server" ID="lbl_HSCHF_Date" ForeColor="#006400"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label Text="HSC-HF" runat="server" ID="lbl_HSCHF_Damage" ForeColor="DarkRed"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label Text="" runat="server" ID="lbl_HSCHF_InsEff"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label Text="" runat="server" ID="lbl_HSCHF_IntruIns" ForeColor="Maroon"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label Text="" runat="server" ID="lbl_HSCHF_NonIntruIns" ForeColor="Maroon"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="dt_HSCHF_InsDt" runat="server" Width="150px" DateInput-EmptyMessage="Select Inspection Date"
                                                    MinDate="01/01/1000" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy">
                                                    <Calendar ID="CalendarHSCHF" runat="server">
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                        </SpecialDays>
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cbo_HSCHF_inspecEffec" runat="server" Filter="StartsWith"
                                                    DropDownWidth="150px" Height="80px" Width="150px" AppendDataBoundItems="True"
                                                    EmptyMessage="Select">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="Highly Effective" Value="A" />
                                                        <telerik:RadComboBoxItem Text="Usually Effective" Value="B" />
                                                        <telerik:RadComboBoxItem Text="Fairly Effective" Value="C" />
                                                        <telerik:RadComboBoxItem Text="Poorly Effective" Value="D" />
                                                        <telerik:RadComboBoxItem Text="In Effective" Value="E" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                            <%-- <td>
                                            <asp:Button ID="btn_HSCHF_Save" runat="server" Text=" Save " 
                                                                Font-Bold="true" />
                                            </td>--%>
                                        </tr>
                                          <%------------14----------HIC/SOHIC-HF------------------------%>
                                        <tr style="font-weight: bold; font-size: medium">
                                            <td align="center">
                                                <asp:Label Text="" runat="server" ID="lbl_SOHIC_Date" ForeColor="#006400"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label Text="HIC/SOHIC-HF" runat="server" ID="lbl_SOHIC_Damage" ForeColor="DarkRed"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label Text="" runat="server" ID="lbl_SOHIC_InsEff"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label Text="" runat="server" ID="lbl_SOHIC_IntruIns" ForeColor="Maroon"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label Text="" runat="server" ID="lbl_SOHIC_NonIntruIns" ForeColor="Maroon"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="dt_SOHIC_InsDt" runat="server" Width="150px" DateInput-EmptyMessage="Select Inspection Date"
                                                    MinDate="01/01/1000" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy">
                                                    <Calendar ID="CalendarSOHIC" runat="server">
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                        </SpecialDays>
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cbo_SOHIC_inspecEffec" runat="server" Filter="StartsWith"
                                                    DropDownWidth="150px" Height="80px" Width="150px" AppendDataBoundItems="True"
                                                    EmptyMessage="Select">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="Highly Effective" Value="A" />
                                                        <telerik:RadComboBoxItem Text="Usually Effective" Value="B" />
                                                        <telerik:RadComboBoxItem Text="Fairly Effective" Value="C" />
                                                        <telerik:RadComboBoxItem Text="Poorly Effective" Value="D" />
                                                        <telerik:RadComboBoxItem Text="In Effective" Value="E" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                            <%-- <td>
                                            <asp:Button ID="btn_SOHIC_Save" runat="server" Text=" Save " 
                                                                Font-Bold="true" />
                                            </td>--%>
                                        </tr>
                                          <%------------15----------HTHA------------------------%>
                                        <tr style="font-weight: bold; font-size: medium">
                                            <td align="center">
                                                <asp:Label Text="" runat="server" ID="lbl_HTHA_Date" ForeColor="#006400"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label Text="HTHA" runat="server" ID="lbl_HTHA_Damage" ForeColor="DarkRed"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label Text="" runat="server" ID="lbl_HTHA_InsEff"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label Text="" runat="server" ID="lbl_HTHA_IntruIns" ForeColor="Maroon"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label Text="" runat="server" ID="lbl_HTHA_NonIntruIns" ForeColor="Maroon"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="dt_HTHA_InsDt" runat="server" Width="150px" DateInput-EmptyMessage="Select Inspection Date"
                                                    MinDate="01/01/1000" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy">
                                                    <Calendar ID="CalendarHTHA" runat="server">
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                        </SpecialDays>
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cbo_HTHA_inspecEffec" runat="server" Filter="StartsWith"
                                                    DropDownWidth="150px" Height="80px" Width="150px" AppendDataBoundItems="True"
                                                    EmptyMessage="Select">
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
                                             <td colspan="7" align="center">
                                            <asp:Button ID="btn_inspectionPlan_Save" runat="server" Text=" Save " OnClick="btn_inspectionPlan_Save_Click" class="box"
                                                                Font-Bold="true" />
                                            </td>
                                        </tr>

                                    </table>
                                    <br />
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
