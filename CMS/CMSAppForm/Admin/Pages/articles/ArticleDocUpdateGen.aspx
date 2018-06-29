<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="ArticleDocUpdateGen.aspx.cs" Inherits="Admin_ArticleDocUpdateGen" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <div style="text-align:center; padding:5px;"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
    <div style="width:auto; height:510px; overflow:auto;">
        <asp:Label ID="lblGen" runat="server" Text=""></asp:Label>
    </div>
    <br />

    <div style="text-align:center">
        <a href="ArticleDocUpdate.aspx?LanguageId=<%=LanguageId %>&ArticleId=<%=ArticleId %>&GenType=<%=GenType %>" class="quaylai">Quay lại</a>
        <asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom" 
                    Text="Tạo bản tin" meta:resourcekey="btnSave" onclick="btnSave_Click">
        </asp:LinkButton>
    </div>
</asp:Content>

