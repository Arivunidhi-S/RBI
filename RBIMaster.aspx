<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RBIMaster.aspx.cs" Inherits="RBIMaster" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<link rel="shortcut icon" href="Images/BoilerIcon.png" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>RBI Master</title>
    <style type="text/css">
        .Round
        {
            width: 186px;
            height: 116px;
            -webkit-border-radius: 0px;
            -moz-border-radius: 0px;
            border-radius: 0px;
            border: 4px solid #541616;
            box-shadow: 3px 5px 6px rgba(0,0,0,0.5);
            
            -webkit-transition: all 0.5s ease;
            -moz-transition: all 0.5s ease;
            -o-transition: all 0.5s ease;
            -ms-transition: all 0.5s ease;
            transition: all 0.5s ease;
        }
        
        .Round:hover
        {
            -webkit-transform: rotate(-10deg);
            -moz-transform: rotate(-10deg);
            -o-transform: rotate(-10deg);
            -ms-transform: rotate(-10deg);
            transform: rotate(-10deg);
        }
        .Text
        {
            text-shadow: 1px 3px 5px rgba(0,0,0,0.3);
            font-size: 45px;
            background: -webkit-linear-gradient(#000, #999);
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
            text-outline: 2px 2px #ff0000;
        }
        .TbBg
        {
            -moz-box-shadow: 0 0 7px 2px #000;
            -webkit-box-shadow: 0 0 7px 2px #000;
            box-shadow: 0 0 7px 2px #000;
            position: fixed;
            top: 15%;
            left: 24%;
            background: linear-gradient(#e09432,#f8b117);
        }
        body
        {
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
    <table width="660" border="0" align="center" class="TbBg">
        <tr>
            <td align="center">
                <h1 class="Text">
                    <strong>Risk Based Inspection (RBI)</strong></h1>
            </td>
        </tr>
        <tr>
            <td align="center">
                <a href="Company.aspx" title="Company">
                    <img class="Round" src="Images/Thumbnail/Company.jpg" alt="Company" /></a> &nbsp;
                <a href="Department.aspx" title="Department">
                    <img class="Round" src="Images/Thumbnail/Department.jpg" alt="Department" /></a>
                &nbsp; <a href="User.aspx" title="User">
                    <img class="Round" src="Images/Thumbnail/User.jpg" alt="User" /></a>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                <a href="ProcessArea.aspx" title="Process">
                    <img class="Round" src="Images/Thumbnail/Process.jpg" alt="Process" /></a> &nbsp;
                <a href="EquipmentMaster.aspx" title="Equipment">
                    <img class="Round" src="Images/Thumbnail/Equipment.jpg" alt="Equipment" /></a>
                &nbsp; <a href="RBIHome.aspx" title="Home">
                    <img class="Round" src="Images/Thumbnail/Home.jpg" alt="Home" /></a>
                <br />
                <br />
                <br />
            </td>
        </tr>
    </table>
</body>
</html>
