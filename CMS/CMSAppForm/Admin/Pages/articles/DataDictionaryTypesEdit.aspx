<%@ Page Language="C#" MasterPageFile="~/admin/MasterPageAdminEdit.master" AutoEventWireup="true"
    CodeFile="DataDictionaryTypesEdit.aspx.cs" Inherits="admin_pages_DataDictionaryTypesEdit"
    Title="" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" runat="Server">
    <table id="tblEdit" runat="server" cellspacing="0" cellpadding="3" width="100%" border="0">
        <tr>
            <td class="td-edit-2">
                &nbsp;
            </td>
            <td>
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td-edit-2">
                <asp:Label ID="lblDataDictionaryTypeName" runat="server" Text="Tên:" meta:resourcekey="lblDataDictionaryTypeName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDataDictionaryTypeName" runat="server" CssClass="tukhoatimekiem"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td-edit-2">
                <asp:Label ID="lblDataDictionaryTypeDesc" runat="server" Text="Mô tả:" meta:resourcekey="lblDataDictionaryTypeDesc"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDataDictionaryTypeDesc" runat="server" CssClass="tukhoatimekiem"
                    Width="400px" TextMode="MultiLine" Height="81px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td-edit-2" colspan="2">
                <br />
                <div style="text-align: center">
                    <asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom" Text="Lưu thông tin"
                        meta:resourcekey="btnSave" OnClick="btnSave_Click">
                    </asp:LinkButton></div>
            </td>
        </tr>
    </table>
</asp:Content>
