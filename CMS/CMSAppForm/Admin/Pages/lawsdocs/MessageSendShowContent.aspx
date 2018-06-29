<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="MessageSendShowContent.aspx.cs" Inherits="Admin_MessageSendShowContent" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <script type="text/javascript">
    function InitializeRequest(sender, args) {
    }

    function EndRequest(sender, args) {
       
    }
    </script>
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td >
                <asp:Label ID="lblTitle" runat="server" 
                    Text="Tiêu đề:" meta:resourcekey="lblTitle"></asp:Label>
                </td>
            <td>
            <asp:Label ID="lblTitleEdit" Font-Bold="true" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="lbResend" runat="server" OnClick="lbResend_Click">Gửi lại email</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td >
             <asp:Label ID="lblContent" runat="server" Text="Nội dung:" 
                    meta:resourcekey="lblContent"></asp:Label>
            </td>
            <td>
                <CKEditor:CKEditorControl ID="txtContent" runat="server" 
                    BasePath="~/Integrated/ckeditor/" Height="330px" Toolbar="Basic" Width="98%"></CKEditor:CKEditorControl>
            </td>
        </tr>
        </table>
    <br />
    <div style="text-align:center"></div>
</asp:Content>

