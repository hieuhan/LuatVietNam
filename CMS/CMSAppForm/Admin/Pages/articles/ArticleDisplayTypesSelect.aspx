<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="ArticleDisplayTypesSelect.aspx.cs" Inherits="Admin_ArticleDisplayTypeSelect" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <br /><br />
    <div style="width:auto; height:350px; overflow:auto;">
        <asp:CheckBox ID="chkShowTop" runat="server" Text="Show Top" />&nbsp;
        <asp:CheckBox ID="chkShowBottom" runat="server" Text="Show Bottom" />&nbsp;
        <asp:CheckBox ID="chkShowWeb" runat="server" Text="Show Home web" />&nbsp;
        <asp:CheckBox ID="chkShowWap" runat="server" Text="Show Home wap" />&nbsp;
        <asp:CheckBox ID="chkShowApp" runat="server" Text="Show Home app" />&nbsp;<br /><br />
        <asp:CheckBoxList ID="chkDisplayType" DataTextField="DisplayTypeDesc" DataValueField="DisplayTypeId" runat="server">
        </asp:CheckBoxList>
    </div>
    <div style="text-align:center">
        <asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom" 
                    Text="Lưu thông tin" meta:resourcekey="btnSave" onclick="btnSave_Click">
        </asp:LinkButton>
    </div>
</asp:Content>

