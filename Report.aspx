<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Stimulsoft.Report.Web, Version=2011.2.1100.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a"
    Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="shortcut icon" href="Images/BoilerIcon.png" />
    <title>Reports</title>
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
                                    REPORTS DETAILS
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <br />
                                <table>
                                    <tr>
                                        <td style="font-weight: bold">
                                            Reports:&nbsp;&nbsp;
                                        </td>
                                        <td align="center">
                                            <div class="box">
                                                <telerik:RadComboBox ID="cbo_Report_Select" runat="server" Filter="StartsWith" DropDownWidth="190px" AutoPostBack="true"
                                                    Width="150px" EmptyMessage="Select" OnSelectedIndexChanged="OnSelectedIndexChanged_cbo_Report_Select">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="COF_Flammable" Value="A" />
                                                        <telerik:RadComboBoxItem Text="COF_NonFlammable" Value="B" />
                                                        <telerik:RadComboBoxItem Text="Inspection Details" Value="E" />
                                                        <telerik:RadComboBoxItem Text="Inspection Chart" Value="F" />
                                                         <telerik:RadComboBoxItem Text="Risk Ranking Chart" Value="D" />
                                                        <telerik:RadComboBoxItem Text="POF Summary" Value="C" />
                                                        <telerik:RadComboBoxItem Text="Equipment Details" Value="G" />
                                                        <telerik:RadComboBoxItem Text="Inspection Summary" Value="H" />
                                                        <telerik:RadComboBoxItem Text="Inspection Plan" Value="I" />
                                                         <telerik:RadComboBoxItem Text="Equipment list" Value="J" />
                                                          <telerik:RadComboBoxItem Text="Dosh list Summary" Value="K" />
                                                        
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </div>
                                        </td>
                                        <td height="50%" style="font-weight: bold">
                                            &nbsp;&nbsp;<asp:Label class="labelstyle" ID="lbl_ProcessArea" runat="server" Text="Process Area:" Font-Bold="true" />  &nbsp;&nbsp;
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
                                            &nbsp;&nbsp; <asp:Label class="labelstyle" ID="lbl_Equipment" runat="server" Text="Equipment:" Font-Bold="true" /> &nbsp;&nbsp;
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
                                            &nbsp;&nbsp; <asp:Label class="labelstyle" ID="lbl_Component" runat="server" Text="Component:" Font-Bold="true" />&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <div class="box">
                                                <telerik:RadComboBox ID="cboComponent" runat="server" DropDownWidth="300px" Height="300px"
                                                    DataValueField="CompNo" Width="150px" AppendDataBoundItems="True" EmptyMessage="Select" 
                                                    DataTextField="CompName" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_cboComponent">
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
                                        <td colspan="9" align="center">
                                            &nbsp;&nbsp;
                                            <asp:Button ID="btn_Report_Submit" runat="server" Text=" Submit " class="box" OnClick="btn_Report_Submit_Click"
                                                Font-Bold="true" />
                                           
                                        </td>
                                        <td>&nbsp;&nbsp;
                                            <asp:Button ID="btnlastinspection" runat="server" Text="  Last Inspection Value  "  OnClick="Onclick_btnlastinspection" class="box"/>
                                            </td>
                                        <td colspan="9" align="center">
                                            <asp:Button ID="btn_Report_Print" runat="server" Text=" Print " class="box" OnClick="btn_Report_Print_Click" Visible="false"
                                                Font-Bold="true" />
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
                            
                                <div id="divrepo" class="text" style="background: -webkit-linear-gradient(#e09432,#f8b117); 
                                    width: 1300px; overflow: auto; border: 4px solid #ffffff; box-shadow: 3px 5px 6px rgba(0,0,0,0.5);">
                                    <br />
                                     <cc1:StiWebViewer ID="StiWebViewer1" runat="server" RenderMode="UseCache" ScrollBarsMode="true"
                                                    Width="1250px" Height="900px" />
                                    <br />
                                    </div>
                          <br />
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
