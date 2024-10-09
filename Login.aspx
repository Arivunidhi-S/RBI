<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="RBILogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>RBI Login</title>
    <link rel="shortcut icon" href="Images/BoilerIcon.png" />
<script type="text/javascript" src="js/reflex.js"></script>
<style type="text/css">
		.demo { float: left; padding: 10px; text-align: center;}
		.logo { float: left; margin: 0 1em 1em 0; position: relative; width: 435px; height: 120px; }
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
.box{box-shadow: 1px 1px 1px 1px; }
.Para
{
text-shadow:0px 0px 1px rgba(48,47,46,1);
font-weight:bold;
color:#291517;
letter-spacing:1pt;
word-spacing:0pt;
font-size:14px;
text-align:right;
font-family:times new roman, times, serif;
<!--line-height:4;-->
}
  
    body {
background-image: url(images/Bg3.jpg); /*You will specify your image path here.*/

-moz-background-size: cover;
-webkit-background-size: cover;
background-size: cover;
background-position: top center !important;
background-repeat: no-repeat !important;
background-attachment: fixed;
}
   
	</style>

</head>

<body bgcolor="#DDDDDD">
<form id="form1" runat="server">
<table width="660" border="0" align="center" style=" -moz-box-shadow: 0 0 7px 2px #000;
            -webkit-box-shadow: 0 0 7px 2px #000;
            box-shadow: 0 0 7px 2px #000; position: fixed; top: 15%; left: 24%; background: -webkit-linear-gradient(#e09432,#f8b117)">
  <tr>
  
    <td align="center">
      <h1 style=" text-shadow: 1px 3px 5px rgba(0,0,0,0.3);font-size: 45px;
  background: -webkit-linear-gradient(#000, #999);
  -webkit-background-clip: text;
  -webkit-text-fill-color: transparent; text-outline: 2px 2px #ff0000;"><strong> Risk Based Inspection (RBI)</strong></h1></td>
    
  </tr>
  <tr>
    <td>
    
<div id="demo" class="inlet">
<div class="demo"><img src="images/left.jpg" width="200" class="reflex iopacity75 iborder4 iheight24" alt="" /></div>
<div class="demo"><img src="images/none.jpg" width="200" class="reflex iopacity75 iborder4 iheight24" alt="" /><br><br><tt><font class="mycss"> Login </font></tt></div>
<div class="demo"><img src="images/right.jpg" width="200" height="130" class="reflex iopacity75 iborder4 iheight24" alt="" /></div>
</div>
</td>
  </tr>
  <tr>
    <td><table width="660" border="0"">
    <!--<tr><td colspan="5" class="mycss" align="center"><font> Login </font></td></tr>-->
  <tr>
    <td class="Para" align="center">User : </td>
    <td><asp:TextBox ID="txtLogin" runat="server" class="box"/></td>
    <td class="Para" align="center">Password : </td>
    <td><asp:TextBox  ID="txtPassword" runat="server" TextMode="Password" class="box"/></td>
    <td align="left"><asp:Button ID="LoginBtn" Text="Log In" runat="server" OnClick="LoginBtn_Click" class="box"/></td>
     </tr>
     <tr>
     <td align="center" colspan="5"><br /><asp:Label ID="lblStatus" runat="server"></asp:Label>
     </td></tr>
</table>
<br /><br />
</td>
  </tr>
</table>
</form>
<p style="clear: left;"></p>
<!--<div class="demo"><img src="images/example.jpg" width="200" class="reflex idistance16 iborder2 iheight24" alt="" /></div>
<div class="demo"><img src="images/example.jpg" width="200" class="reflex idistance0 iborder8 iheight40 icolorffffff" alt="" /></div>
<div class="demo"><img src="images/example.jpg" width="200" class="reflex idistance8 iborder1 iheight32 icolor0070a4" alt="" /></div>-->

</body>

</html>
