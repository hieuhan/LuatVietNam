<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master"
    AutoEventWireup="true" CodeFile="CategoryCrawlersEdit.aspx.cs" Inherits="Admin_CategoryCrawlersEdit" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" runat="Server">
    <table id="tblEdit" runat="server" cellspacing="0" cellpadding="3" width="100%" border="0">
        <tr>
            <td colspan="2" class="td-edit-2">
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 120px">
                <asp:Label ID="lblSite" runat="server" Text="Site:" meta:resourcekey="lblSite"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlSite" runat="server" DataTextField="SiteDesc" DataValueField="SiteId"
                    Width="355px" AutoPostBack="True" 
                    onselectedindexchanged="ddlSite_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblDataType" runat="server" Text="Loại dữ liệu:" meta:resourcekey="lblDataType"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlDataType" runat="server" DataTextField="DataTypeDesc" DataValueField="DataTypeId"
                    Width="355px" AutoPostBack="True" 
                    onselectedindexchanged="ddlDataType_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 120px">
                Nguồn dữ liệu:
            </td>
            <td>
                <asp:DropDownList ID="ddlDataSource" runat="server" DataTextField="DataSourceDesc" DataValueField="DataSourceId"
                    Width="355px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 120px">
                <asp:Label ID="lblParentCategory" runat="server" meta:resourcekey="lblParentCategory"
                    Text="Chuyên mục:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlParentCategory" runat="server" DataTextField="CategoryDesc"
                    DataValueField="CategoryId" Width="355px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 120px">
                Url
            </td>
            <td>
                <asp:TextBox ID="txtUrl" runat="server"  Width="355px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 120px">
                Loại
            </td>
            <td>
                <asp:DropDownList ID="ddlLinkType" runat="server" Width="150px">
                    <asp:ListItem>RSS</asp:ListItem>
                    <asp:ListItem>WebPage</asp:ListItem>
                    <asp:ListItem>WebService</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 120px">
                Số mục tin:
            </td>
            <td>
                <asp:TextBox ID="txtMaxItem" runat="server"  Width="355px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 120px">
                <asp:Label ID="lblReviewStatus" runat="server" meta:resourcekey="lblReviewStatus"
                    Text="Trạng thái:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlReviewStatus" runat="server" DataTextField="StatusDesc"
                    DataValueField="StatusId" Width="150px">
                </asp:DropDownList>
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
