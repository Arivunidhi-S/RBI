<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportsCOFNonFlame.aspx.cs" Inherits="ReportsCOFNonFlame" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Stimulsoft.Report.Web, Version=2011.2.1100.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a"
    Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
   
   <link rel="shortcut icon" href="Images/BoilerIcon.png" />
    <title>Report COF Flammable</title>
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
    <link rel="stylesheet" href="css/menu_core.css" type="text/css" />
    <link rel="stylesheet" href="css/modal.css" type="text/css" />
    <%--Menu Script--%>
    <link type="text/css" rel="stylesheet" href="css/Upper.css" />
    <script type="text/javascript" src="js/Upperjs.js"></script>
    <style type="text/css">
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
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <table border="0" cellpadding="2" cellspacing="2" width="100%">
            <tr>
                <td style="border-right: blue thin solid; border-top: blue thin solid; border-left: blue thin solid;
                    border-bottom: blue thin solid; border-width: 0px" align="center">
                    <table border="0" width="100%">
                        <tr>
                            <td align="center" style="width: 80%;">
                                <div>
                                    <asp:Label class="labelstyle" ID="lblStatus" runat="server" ForeColor="Red" Font-Bold="true" />
                                </div>
                            </td>
                            <td align="center" style="width: 20%;">
                                <div style="height: 20px;">
                                    <asp:Label class="labelstyle" ID="lblUserInfo" Visible="false" Font-Size="Small"
                                        runat="server" ForeColor="#e4cd87" Font-Bold="true" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%;">
                                <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
                                <div id="Div11" runat="server" style="width: 100%; background-color: transparent;
                                    text-align: center;">
                                   <%-- <cc1:StiWebViewer ID="StiWebViewer1" ToolbarAlignment="Center" runat="server" RenderMode="Standard"
                                        Width="1000px" Height="600px" ScrollBarsMode="true" BackColor="#aeb6f7" Visible="true" />--%>
                                            <cc1:StiWebViewer ID="StiWebViewer1" runat="server" RenderMode="UseCache" ScrollBarsMode="true"
                                                    Width="900px" Height="700px" />
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
