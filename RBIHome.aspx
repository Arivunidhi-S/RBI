﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RBIHome.aspx.cs" Inherits="RBIHome" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link rel="shortcut icon" href="Images/BoilerIcon.png" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>RBI Home</title>
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
                <a href="RBIMaster.aspx" title="Master">
                    <img class="Round" src="Images/Thumbnail/master.jpg" alt="Master" /></a> &nbsp;
                <a href="RBITransaction.aspx" title="Transaction">
                    <img class="Round" src="Images/Thumbnail/trans.jpg" alt="Transaction" /></a>
                &nbsp; <a href="POF.aspx" title="POF">
                    <img class="Round" src="Images/Thumbnail/pof.jpg" alt="POF" /></a>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                <a href="COF.aspx" title="COF">
                    <img class="Round" src="Images/Thumbnail/cof.jpg" alt="COF" /></a> &nbsp; <a href="Report.aspx"
                        title="Report">
                        <img class="Round" src="Images/Thumbnail/rep.jpg" alt="Report" /></a> &nbsp;
                <a href="Login.aspx" title="Logout">
                    <img class="Round" src="Images/Thumbnail/log.jpg" alt="Report" /></a>
                <br />
                <br />
                <br />
            </td>
        </tr>
    </table>
</body>
</html>