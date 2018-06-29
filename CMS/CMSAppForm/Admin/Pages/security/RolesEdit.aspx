<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="RolesEdit.aspx.cs" Inherits="Admin_RolesEdit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td style="width:120px">
                </td>
            <td></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblName" runat="server" Text="Tên nhóm:" meta:resourcekey="lblName"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtRoleName" runat="server" Width="350px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblDesc" runat="server" Text="Mô tả:" meta:resourcekey="lblDesc"></asp:Label>
                </td>
            <td><asp:TextBox ID="txtRoleDesc" runat="server" Width="350px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblBuildIn" runat="server" Text="Hiển thị:" meta:resourcekey="lblBuildIn"></asp:Label>
                </td>
            <td>
                <asp:CheckBox ID="chkBuildIn" runat="server" />
                </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <div style="text-align:center"><asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom" 
                    Text="Lưu thông tin" meta:resourcekey="btnSave" 
            onclick="btnSave_Click">
                        </asp:LinkButton></div>
</asp:Content>

