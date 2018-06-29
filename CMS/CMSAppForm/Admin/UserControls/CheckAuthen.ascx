<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CheckAuthen.ascx.cs" Inherits="ICSoft.AppForm.Admin.MenuControl" %>
<div id="authenmessage" runat="server" visible="false">
    <div class="clear5px"></div>
    <div class="AuthMessage" >
        <%= ICSoft.HelperLib.Themes.GetItemAdmin("UC", "NOT_IN_ROLE", LanguageName, "Bạn chưa được cấp quyền sử dụng chức năng này")%>        
    </div>
    <div class="clear5px"></div>
</div>