<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master"
    AutoEventWireup="true" CodeFile="MediaGroupsEdit.aspx.cs" Inherits="Admin_Pages_articles_MediaGroupsEdit" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Content ID="contentAccountType" ContentPlaceHolderID="m_contentBody" runat="Server">
    <table cellpadding="3" cellspacing="0" style="width: 100%; font-weight: lighter">
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
                <asp:Label ID="lblSite" runat="server" Text="Site:" meta:resourcekey="lblSite"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlSite" runat="server" DataTextField="SiteDesc" DataValueField="SiteId"
                    Width="400px" CssClass="tukhoatimekiem">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="td-edit-2">
                <asp:Label ID="lblParentGroup" runat="server" Text="Chuyên mục cha:" meta:resourcekey="lblParentGroup"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlParentGroup" runat="server" DataTextField="MediaGroupDesc" DataValueField="MediaGroupId"
                    Width="400px" CssClass="tukhoatimekiem">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="td-edit-2">
                <asp:Label ID="lblFeedBackGroupName" runat="server" Text="Tên:" meta:resourcekey="lblFeedBackGroupName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtMediaGroupName" runat="server" CssClass="tukhoatimekiem" Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td-edit-2">
                <asp:Label ID="lblFeedBackGroupDesc" runat="server" Text="Mô tả:" meta:resourcekey="lblFeedBackGroupDesc"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtMediaGroupDesc" TextMode="MultiLine" runat="server" CssClass="tukhoatimekiem"
                    Width="400px" Height="81px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td-edit-2" colspan="2">
                <br />
                <div style="text-align: center">
                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="savebutom" Text="Lưu thông tin"
                        meta:resourcekey="btnSave" OnClick="btnSave_Click">
                    </asp:LinkButton></div>
            </td>
        </tr>
    </table>
    <br />
</asp:Content>
