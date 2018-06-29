<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master"
    AutoEventWireup="true" CodeFile="CrawlerRulesEdit.aspx.cs" Inherits="Admin_CrawlerRulesEdit" %>

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
                Tag name:
            </td>
            <td>
                <asp:TextBox ID="txtTagName" runat="server"  Width="355px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 120px">
                Tag class:
            </td>
            <td>
                <asp:TextBox ID="txtTagClass" runat="server"  Width="355px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 120px">
                Tag id:
            </td>
            <td>
                <asp:TextBox ID="txtTagId" runat="server"  Width="355px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 120px">
                Vị trí:
            </td>
            <td>
                <asp:TextBox ID="txtPosition" runat="server"  Width="355px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 120px">
                Nội dung phân tích:
            </td>
            <td>
                <asp:DropDownList ID="ddlContentType" runat="server" Width="355px" DataTextField="CrawlerContentTypeDesc" DataValueField="CrawlerContentTypeId">
                </asp:DropDownList>
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
