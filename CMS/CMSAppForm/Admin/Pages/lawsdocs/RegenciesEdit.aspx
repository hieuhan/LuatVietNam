<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="RegenciesEdit.aspx.cs" Inherits="Admin_RegenciesEdit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td width="90px" ><asp:Label ID="lblRegencyName" runat="server" Text="Chức vụ:" meta:resourcekey="lblRegencyName"></asp:Label>
            <asp:RequiredFieldValidator ID="rfvRegencyName" runat="server" Display="Dynamic" 
                ErrorMessage="(*)" ValidationGroup="G1" 
                ControlToValidate="txtRegencyName" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            <td>
                <asp:TextBox ID="txtRegencyName" runat="server" CssClass="tukhoatimekiem" Width="250px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td >
                <asp:Label ID="lblRegencyDesc" runat="server" Text="Mô tả:" 
                    meta:resourcekey="lblRegencyDesc"></asp:Label>
                    <asp:RequiredFieldValidator ID="rfvRegencyDesc" runat="server" Display="Dynamic" 
                ErrorMessage="(*)" ValidationGroup="G1" 
                ControlToValidate="txtRegencyDesc" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            <td>
                <asp:TextBox ID="txtRegencyDesc" runat="server" CssClass="tukhoatimekiem" 
                    Width="250px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td >
                <asp:Label ID="lblDisplayOrder" runat="server" Text="Thứ tự:" 
                    meta:resourcekey="lblDisplayOrder"></asp:Label>
                    <asp:RequiredFieldValidator ID="rfvDisplayOrder" runat="server" Display="Dynamic" 
                ErrorMessage="(*)" ValidationGroup="G1" 
                ControlToValidate="txtDisplayOrder" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            <td>
                <asp:TextBox ID="txtDisplayOrder" runat="server" CssClass="tukhoatimekiem" 
                    Width="250px"></asp:TextBox>
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

