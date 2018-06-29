<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="ServiceRolesEdit.aspx.cs" Inherits="Admin_ServiceRolesEdit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td width="110px" ><asp:Label ID="lblRoles" runat="server" Text="Chọn quyền:" meta:resourcekey="lblRoles"></asp:Label>            
              <span style="color:Red;">(*)</span></td>
            <td>
            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" ForeColor="Red" Text=""></asp:Label>
                <asp:CheckBoxList ID="chkRoles" runat="server" DataTextField="RoleDesc" 
                    DataValueField="RoleId" RepeatColumns="3" 
                    RepeatDirection="Horizontal">
                </asp:CheckBoxList>
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

