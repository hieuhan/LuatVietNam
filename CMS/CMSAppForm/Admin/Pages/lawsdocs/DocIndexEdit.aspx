<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="DocIndexEdit.aspx.cs" Inherits="Admin_DocIndexEdit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
       <tr>
            <td style="width:150px">
                <asp:Label ID="lblLanguage" runat="server" meta:resourcekey="lblLanguage" 
                    Text="Ngôn ngữ:"></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlLanguage" runat="server" CssClass="userselect"
                    DataTextField="LanguageDesc" DataValueField="LanguageId" Width="90%">
                </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblTitle" runat="server" Text="Title:"></asp:Label>
                <asp:RequiredFieldValidator ID="rfvTitle" runat="server" Display="Dynamic" 
                ErrorMessage="(*)" ValidationGroup="G1" 
                ControlToValidate="txtTitle" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            <td>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="tukhoatimekiem" Width="90%"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblBookmark" runat="server" Text="Bookmark:"></asp:Label>
                <asp:RequiredFieldValidator ID="rfvBookmark" runat="server" Display="Dynamic" 
                ErrorMessage="(*)" ValidationGroup="G1" 
                ControlToValidate="txtBookmark" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            <td>
                <asp:TextBox ID="txtBookmark" runat="server" CssClass="tukhoatimekiem" Width="90%"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblLevel" runat="server" Text="Level:"></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlLevelId" runat="server" CssClass="userselect" Width="90%">
                    <asp:ListItem Value="1">Level 1</asp:ListItem>
                    <asp:ListItem Value="2">Level 2</asp:ListItem>
                    <asp:ListItem Value="3">Level 3</asp:ListItem>
                    <asp:ListItem Value="4">Level 4</asp:ListItem>
                    <asp:ListItem Value="5">Level 5</asp:ListItem>
                </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblDisplayOrder" runat="server" Text="DisplayOrder:"></asp:Label>
                <asp:RegularExpressionValidator ID="rglDisplayOrder" runat="server" ForeColor="Red" ErrorMessage="Bạn vui lòng nhập số"
                        Display="Dynamic" ValidationExpression="\d+" ControlToValidate="txtDisplayOrder"></asp:RegularExpressionValidator>
                </td>
            <td>
                <asp:TextBox ID="txtDisplayOrder" runat="server" CssClass="tukhoatimekiem" Width="90%"></asp:TextBox>
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
                    Text="Lưu thông tin" meta:resourcekey="btnSave"  ValidationGroup="G1"
            onclick="btnSave_Click">
                        </asp:LinkButton></div>
</asp:Content>

