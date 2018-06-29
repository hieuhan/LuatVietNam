<%@ Page Language="C#" MasterPageFile="~/admin/MasterPageAdmin.master" AutoEventWireup="true"
    CodeFile="changePassword.aspx.cs" Inherits="ICSoft.AppForm.Admin.PageChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="m_contentBody" runat="Server">
    <table border="0" id="table1" align="center" cellpadding="1" cellspacing="1">
        <tr>
            <td class="td-edit-4">
            </td>
            <td>
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td-edit-4">
                <asp:Label ID="lblOldPass" runat="server" Text="Mật khẩu cũ:" meta:resourcekey="lblOldPass"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtOldPass" runat="server" MaxLength="50" TextMode="Password" CssClass="tbSearch"> </asp:TextBox>
                <asp:RequiredFieldValidator ID="reqOldPass" ControlToValidate="txtOldPass" Text="Gõ mật khẩu cũ"
                    runat="server"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="td-edit-4">
                <asp:Label ID="lblPass" runat="server" Text="Mật khẩu mới:" meta:resourcekey="lblPass"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" MaxLength="50" CssClass="tbSearch"
                    EnableViewState="true"></asp:TextBox><i>(<asp:Label ID="lblMinPassLen" runat="server" Text="Mật khẩu ít nhất 6 ký tự"  meta:resourcekey="lblMinPassLen"></asp:Label>)</i>
                <asp:RequiredFieldValidator ID="reqPassValid" ControlToValidate="txtPassword" Text="Type password"
                    runat="server"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="td-edit-4">
                <asp:Label ID="lblPassConfirm" runat="server" Text="Gõ lại mật khẩu:" meta:resourcekey="lblPassConfirm"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtPasswordConfirm" runat="server" TextMode="Password" MaxLength="50"
                    CssClass="tbSearch"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqPassCofirm" ControlToValidate="txtPasswordConfirm"
                    Text="Retype new password" runat="server"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cmpPass" runat="server" ControlToValidate="txtPasswordConfirm"
                    ControlToCompare="txtPassword" Operator="Equal" ErrorMessage="Mật khẩu mới và mật khẩu gõ lại không giống nhau" meta:resourcekey="cmpPass" />
            </td>
        </tr>
        <tr>
            <td  height="15px">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="td-edit-4">
            </td>
            <td>
                <asp:Button ID="btnChangePass" runat="server" Text="Lưu" CssClass="btSearch button"
                    OnClick="changePass_ChangingPassword" meta:resourcekey="btnChangePass" />
            </td>
        </tr>
    </table>
    <br />
    <p align="center">
        <asp:HiddenField ID="userId" runat="server" />
        
    </p>
</asp:Content>
