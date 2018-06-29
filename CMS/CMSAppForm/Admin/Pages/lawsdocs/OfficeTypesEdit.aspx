<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="OfficeTypesEdit.aspx.cs" Inherits="Admin_OfficeTypesEdit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td style="width:80px"><asp:Label ID="lblOfficeTypeName" runat="server" Text="Tên:" meta:resourcekey="lblOfficeTypeName"></asp:Label>
            <asp:RequiredFieldValidator ID="rfvOfficeTypeName" runat="server" Display="Dynamic" 
                ErrorMessage="(*)" ValidationGroup="G1" 
                ControlToValidate="txtOfficeTypeName" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            <td>
                <asp:TextBox ID="txtOfficeTypeName" runat="server" CssClass="tukhoatimekiem" Width="240px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblOfficeTypeDesc" runat="server" 
                    Text="Mô tả:" meta:resourcekey="lblOfficeTypeDesc"></asp:Label>
                     <asp:RequiredFieldValidator ID="rfvOfficeTypeDesc" runat="server" Display="Dynamic" 
                ErrorMessage="(*)" ValidationGroup="G1" 
                ControlToValidate="txtOfficeTypeDesc" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            <td>
                <asp:TextBox ID="txtOfficeTypeDesc" runat="server" CssClass="tukhoatimekiem" 
                    Width="240px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblDisplayOrder" runat="server" meta:resourcekey="lblDisplayOrder" 
                    Text="Thứ tự:"></asp:Label>
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

