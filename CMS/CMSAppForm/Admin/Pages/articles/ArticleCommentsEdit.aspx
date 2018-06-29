<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master"
    AutoEventWireup="true" CodeFile="ArticleCommentsEdit.aspx.cs" Inherits="Admin_Pages_articles_ArticleCommentsEdit" %>

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
                <asp:Label ID="lblDataType" runat="server" Text="Kiểu dữ liệu:" meta:resourcekey="lblDataType"></asp:Label>
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
                <asp:Label ID="lblLanguage" runat="server" Text="Ngôn ngữ:" meta:resourcekey="lblLanguage"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlLanguage" runat="server" DataTextField="LanguageDesc" DataValueField="LanguageId"
                    Width="355px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 120px">
                <asp:Label ID="lblAppType" runat="server" meta:resourcekey="lblAppType" Text="Loại ứng dụng:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlAppType" runat="server" DataTextField="ApplicationTypeDesc"
                    DataValueField="ApplicationTypeId" Width="355px">
                </asp:DropDownList>
            </td>
        </tr>
        
        <tr>
            <td style="width: 120px">
                <asp:Label ID="lblFullName" runat="server" Text="Tên:" meta:resourcekey="lblFullName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFullName" runat="server"  Width="355px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 120px">
                <asp:Label ID="lblEmail" runat="server" Text="Mail:" meta:resourcekey="lblEmail"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" Width="355px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 120px">
                <asp:Label ID="lblPhoneNumber" runat="server" Text="PhoneNumber:" meta:resourcekey="lblPhoneNumber"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtPhoneNumber" runat="server" Width="355px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 120px">
                <asp:Label ID="lblComment" runat="server" Text="Comment:" meta:resourcekey="lblComment"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtComment" TextMode="MultiLine" runat="server" Width="355px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 120px">
                <asp:Label ID="lblFromIP" runat="server" Text="FromIP:" meta:resourcekey="lblFormIP"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFromIP" runat="server" Width="355px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 120px">
                <asp:Label ID="lblUserAgent" runat="server" Text="UserAgent:" meta:resourcekey="lblUserAgent"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtUserAgent" runat="server" Width="355px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 120px">
                <asp:Label ID="lblCommentLevel" runat="server" Text="Cấp Bình Luận:" meta:resourcekey="lblCommentLevel"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtCommentLevel" runat="server"  Width="355px"></asp:TextBox>
            </td>
        </tr>


        <tr>
            <td style="width: 120px">
                <asp:Label ID="lblReviewStatus" runat="server" meta:resourcekey="lblReviewStatus"
                    Text="Trạng thái:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlReviewStatus" runat="server" DataTextField="ReviewStatusDesc"
                    DataValueField="ReviewStatusId" Width="150px">
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
