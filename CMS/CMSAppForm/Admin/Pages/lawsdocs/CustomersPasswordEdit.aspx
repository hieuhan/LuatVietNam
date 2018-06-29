<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="CustomersPasswordEdit.aspx.cs" Inherits="Admin_CustomersPasswordEdit" %>
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
                <asp:Label ID="lblCustomerNames"  Font-Bold="true" ForeColor="Maroon" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblCustomerPass" runat="server" Text="Mật khẩu:" 
                    meta:resourcekey="lblCustomerPass"></asp:Label>
                <asp:RequiredFieldValidator ID="rfvCustomerPassword" runat="server" Display="Dynamic" 
                ErrorMessage="(*)" ValidationGroup="G1" 
                ControlToValidate="txtCustomerPass" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            <td align="left">
                <asp:TextBox ID="txtCustomerPass" runat="server" Width="260px" CssClass="tukhoatimekiem" TextMode="Password" ></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="rfvtxtCustomerPass" runat="server" ErrorMessage="Vui lòng nhập Mật khẩu mới" ForeColor="Red" ControlToValidate="txtCustomerPass" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
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

