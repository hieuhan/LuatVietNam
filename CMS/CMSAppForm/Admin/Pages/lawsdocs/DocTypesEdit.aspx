<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="DocTypesEdit.aspx.cs" Inherits="Admin_DocTypesEdit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td style="width:80px">
                <asp:Label ID="lblLanguage" runat="server" meta:resourcekey="lblLanguage" 
                    Text="Ngôn ngữ:"></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlLanguage" runat="server" CssClass="userselect"
                    DataTextField="LanguageDesc" DataValueField="LanguageId" Width="250px">
                </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblDocTypeName" runat="server" Text="Tên:" meta:resourcekey="lblDocTypeName"></asp:Label>
            <asp:RequiredFieldValidator ID="rfvDocTypeName" runat="server" Display="Dynamic" 
                ErrorMessage="(*)" ValidationGroup="G1" 
                ControlToValidate="txtDocTypeName" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            <td>
                <asp:TextBox ID="txtDocTypeName" runat="server" CssClass="tukhoatimekiem" Width="240px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblDocTypeDesc" runat="server" 
                    Text="Mô tả:" meta:resourcekey="lblDocTypeDesc"></asp:Label>
                    <asp:RequiredFieldValidator ID="rfvDocTypeDesc" runat="server" Display="Dynamic" 
                ErrorMessage="(*)" ValidationGroup="G1" 
                ControlToValidate="txtDocTypeDesc" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            <td>
                <asp:TextBox ID="txtDocTypeDesc" runat="server" CssClass="tukhoatimekiem" 
                    Width="240px" Height="100px" TextMode="MultiLine"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblDisplayOrder" runat="server" 
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
                <asp:Label ID="lblReviewStatus" runat="server" Text="Trạng thái:" 
                    meta:resourcekey="lblReviewStatus"></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlReviewStatus" runat="server" 
                    DataTextField="ReviewStatusDesc" DataValueField="ReviewStatusId" Width="250px" 
                    CssClass="userselect">
                </asp:DropDownList>
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

