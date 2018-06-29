<%@ Page Title="" Language="C#"  ValidateRequest="false"
AutoEventWireup="true" CodeFile="ArticlesPreview.aspx.cs" Inherits="Admin_ArticlesPreview" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src="<%= ICSoft.CMSLib.CmsConstants.ROOT_PATH %>Integrated/ckfinder/ckfinder.js"></script>
    
    <script type="text/javascript">
        var popup;
        function SelectName() {
            popup = window.open("MediasSelect.aspx?txtDemo=m_contentBody_txtIcon&ImgDemo=m_contentBody_imgDemo", "Popup", "width=650,height=550,scrollbars=1");
            popup.focus();
            return false
        }
        function SelectName1() {
            popup = window.open("MediasSelect.aspx?txtDemo=m_contentBody_txtIcon1&ImgDemo=m_contentBody_imgDemo1", "Popup", "width=650,height=550,scrollbars=1");
            popup.focus();
            return false
        }
        $(document).ready(function () {
            
           
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);
            $(".uiselect").chosen({ width: "100%", no_results_text: "Không có dữ liệu " });
        });
        function InitializeRequest(sender, args) {

        }
        function EndRequest(sender, args) {
           
            $(".uiselect").chosen({ width: "100%", no_results_text: "Không có dữ liệu " });
        }
    </script>
    <title>Xem trước tin bài</title>
</head>
  <body>  
      <form id="form1" runat="server">    
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">               
        <tr>
            <td colspan="2">
                <h1><asp:Label ID="txtTitle" runat="server"></asp:Label></h1>
            </td>
        </tr>
        <tr id="trSummary" runat="server">
            <td>
                <img alt="txtIcon" id="imgDemo" runat="server" class="ImageSelectPath1" src="../../../Images/transparent.png" style="width: 60px;height: 40px;border: 1px solid black;" /></a>
            </td>
            <td>
               <h3> <asp:Label ID="txtSummaryPlain" runat="server"  ></asp:Label></h3>
                
            </td>
        </tr>
    </table>
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter; padding-top: 20px;">       
        <tr>           
            <td>
                <asp:Label ID="txtContent"  runat="server"  ></asp:Label>            
            </td>
        </tr>
    </table>
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">       
        <tr id="trTag" runat="server">
            <td style="width: 105px;">
                Tag:</td>
            <td>
                <asp:Label ID="txtTag" runat="server" Width="98%"></asp:Label></td>
        </tr>
        <tr>
            <td>
                Tiêu đề SEO:</td>
            <td>
                <asp:Label ID="txtSEOTitle" runat="server" Width="98%"></asp:Label></td>
        </tr>
        <tr>
            <td>
                Mô tả SEO:</td>
            <td>
                <asp:Label ID="txtSEODesc" runat="server" Width="98%"></asp:Label></td>
        </tr>
        <tr>
            <td>
                Từ khóa SEO:</td>
            <td>
                <asp:Label ID="txtSEOKeyword" runat="server" Width="98%"></asp:Label></td>
        </tr>
        
    </table>
    </form>
</body>
