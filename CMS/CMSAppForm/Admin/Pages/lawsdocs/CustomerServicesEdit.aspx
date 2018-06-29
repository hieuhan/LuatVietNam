<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="CustomerServicesEdit.aspx.cs" Inherits="Admin_CustomerServicesEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Import Namespace="ICSoft.CMSLib" %>
<%@ Import Namespace="ICSoft.LawDocsLib" %>
<%@ Import Namespace="ICSoft.HelperLib" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
<script type="text/javascript">
    $(document).ready(function () {
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(InitializeRequest);
        prm.add_endRequest(EndRequest);
        
    });
    function InitializeRequest(sender, args) {
    }

    function EndRequest(sender, args) {
        $(function () {
        
        });
    }
    </script>
    <div style="text-align:center"><asp:Label ID="lblMessage" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label><input type="password" style="visibility:hidden" /></div>
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td style="width:80px">
             Khách hàng:
             </td>
            <td>               
                <asp:Label ID="lblCustomerNames" Font-Bold="true" ForeColor="Maroon" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Ngày hết hạn</td>
            <td align="left">
                <asp:TextBox ID="txtEndTime" runat="server" Width="260px" CssClass="tukhoatimekiem"></asp:TextBox>
            </td>
        </tr>
        </table>
    <div style="text-align:center">
    <p>
        <asp:LinkButton ID="btnSave" runat="server" CssClass="luuvaquaylai" 
            Text="Lưu thông tin" meta:resourcekey="btnSave" ValidationGroup="G1"  onclick="btnSave_Click">
        </asp:LinkButton>
        </p>
    </div>
</asp:Content>

