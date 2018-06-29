<%@ Page Language="C#" AutoEventWireup="true" CodeFile="docstips.aspx.cs" Inherits="Admin_Pages_lawsdocs_docstips" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />  
    <style type="text/css">
        .contenttooltip { text-align:center; font-family:Arial; font-size:12px;font-weight:bold;color:#990002; vertical-align:middle;}
        .ajaxtooltip { position: absolute; display: none; width: 450px; left: 0; top: 0; background:#F7F7CB; border: 1px solid gray; border-width: 1px 2px 2px 1px; padding: 5px; }        
        .VL_Tip_Property_C1 {text-align:left; font-family:Arial; font-size:12px; font-weight:bold; color:#000000; width:120px;}
        .VL_Tip_Property_C2 {text-align:left; font-family:Arial; font-size:12px; font-weight:normal; color:#000000; width:330px;}
      </style>
</head>
<body style="margin: 0px">
<div class="contenttooltip">
  <span id="Span1"> 
      <asp:Label ID="lbTip" Visible="true" runat="server"  EnableViewState="false"></asp:Label>
        </span> 
 </div> 
</body>
</html>
