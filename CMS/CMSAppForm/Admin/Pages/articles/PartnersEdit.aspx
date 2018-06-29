<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="PartnersEdit.aspx.cs" Inherits="Admin_PartnersEdit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td>&nbsp;</td>
            <td>
                            <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblPartnerName" runat="server" Text="Tên:" meta:resourcekey="lblPartnerName"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtPartnerName" runat="server" CssClass="tukhoatimekiem" Width="350px"></asp:TextBox>
                </td>
        </tr>
         <tr>
            <td><asp:Label ID="lblPartnerDesc" runat="server" Text="Mô tả:" meta:resourcekey="lblPartnerDesc"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtPartnerDesc" runat="server" CssClass="tukhoatimekiem" TextMode="MultiLine" Width="350px" Height="100px"></asp:TextBox>
                </td>
        </tr>
         <tr>
            <td><asp:Label ID="lblNotes" runat="server" Text="Ghi chú:" 
                    meta:resourcekey="lblNotes"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtNotes" runat="server" CssClass="tukhoatimekiem" Width="350px" TextMode="MultiLine" Height="100px"></asp:TextBox>
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

