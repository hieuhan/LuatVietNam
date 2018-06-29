<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="UserCategoriesEdit.aspx.cs" Inherits="Admin_UserCategoriesEdit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td width="110px" ><asp:Label ID="lblUserName" runat="server" Text="Tên truy cập:" 
                    meta:resourcekey="lblUserName"></asp:Label>            
              </td>
            <td>
                <asp:Label ID="lblUserNameShow" runat="server" Font-Bold="true" ForeColor="Maroon" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="120px" valign="top" ><asp:Label ID="lblCategories" runat="server" Text="Chọn chuyên mục:" meta:resourcekey="lblCategories"></asp:Label>            
              <span style="color:Red;">(*)</span></td>
            <td>
            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" ForeColor="Red" Text=""></asp:Label>
                <asp:CheckBoxList ID="chkCategories" runat="server" DataTextField="CategoryDesc" 
                    DataValueField="CategoryId" RepeatColumns="2" 
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

