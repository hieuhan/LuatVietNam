<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" ValidateRequest="false"
AutoEventWireup="true" CodeFile="ArticleCrawlersEdit.aspx.cs" Inherits="Admin_ArticleCrawlersEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <asp:Label ID="lblTitle" runat="server" Font-Bold="true" Font-Size="X-Large"></asp:Label><br /><br />
    <asp:Label ID="lblSummary" runat="server"  Font-Bold="true"></asp:Label><br />
    <asp:Label ID="lblContent" runat="server" Text=""></asp:Label>
</asp:Content>

