<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="CustomersChangeAvatar.aspx.cs" Inherits="Admin_CustomersChangeAvatar" %>
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
             <asp:Label ID="lblCustomerName" runat="server" Text="Tên truy cập:" 
                    meta:resourcekey="lblCustomerName"></asp:Label>
             </td>
            <td>               
                <asp:Label ID="lblCustomerNames" Font-Bold="true" ForeColor="Maroon" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Chọn file:
                </td>
            <td align="left">
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </td>
        </tr>
        </table>
    <div style="text-align:center">
    <p>
        <asp:LinkButton ID="btnSave" runat="server" CssClass="luuvaquaylai" 
            Text="Lưu thông tin" meta:resourcekey="btnSave"  onclick="btnSave_Click">
        </asp:LinkButton>
        </p>
    </div>
</asp:Content>

