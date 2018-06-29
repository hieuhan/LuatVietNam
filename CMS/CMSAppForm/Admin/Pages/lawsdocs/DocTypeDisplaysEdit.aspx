<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="DocTypeDisplaysEdit.aspx.cs" Inherits="Admin_DocTypeDisplaysEdit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td style="width:120px">&nbsp;</td>
            <td>
                <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label>
                </td>
        </tr>
        <tr>
            <td style="width:120px">
                <asp:Label ID="lblLanguage" runat="server" Text="Ngôn ngữ:" 
                    meta:resourcekey="lblLanguage"></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlLanguage" runat="server" DataTextField="LanguageDesc" Enabled="false"
                    DataValueField="LanguageId" Width="250px" CssClass="userselect">
                </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td style="width:120px"><asp:Label ID="lblDisplayTypes" runat="server" 
                    Text="Loại hiển thị:" meta:resourcekey="lblDisplayTypes"></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlDisplayTypes" runat="server" Enabled="false" 
                    CssClass="userselect" DataTextField="DisplayTypeDesc" 
                    DataValueField="DisplayTypeId" Width="250px">
                </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td style="width:120px">
                <asp:Label ID="lblDocTypes" runat="server" 
                    Text="Loại văn bản:" meta:resourcekey="lblDocTypes"></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlDocTypes" runat="server" Enabled="false"
                    CssClass="userselect" DataTextField="DocTypeDesc" 
                    DataValueField="DocTypeId" Width="250px">
                </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td style="width:120px"><asp:Label ID="lblDisplayOrder" runat="server" 
                    Text="Thứ tự:" meta:resourcekey="lblDisplayOrder"></asp:Label>
                     <asp:RequiredFieldValidator ID="rfvDisplayOrder" runat="server" Display="Dynamic" 
                ErrorMessage="(*)" ValidationGroup="G1" 
                ControlToValidate="txtDisplayOrder" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            <td>
                <asp:TextBox ID="txtDisplayOrder" runat="server" CssClass="tukhoatimekiem" 
                    Width="240px"></asp:TextBox>
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

