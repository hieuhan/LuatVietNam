<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenuSub.ascx.cs" Inherits="ICSoft.AppForm.Admin.MenuSub" %>
<div class="menudieuhuong">
	<div class="menudieuhuong_conen">      
        <a class="dieuhuong123" title="<%= ICSoft.HelperLib.Themes.GetItemAdmin("UC", "HOME", LanguageName, "Trang chủ")%>" href="<%= ICSoft.CMSLib.CmsConstants.PRJ_ROOT %>">
            <%= ICSoft.HelperLib.Themes.GetItemAdmin("UC", "HOME", LanguageName, "Trang chủ")%>
        </a> 
        <asp:Repeater ID="rptMenu" runat="server">
            <ItemTemplate>
                <img src="<%# ICSoft.CMSLib.CmsConstants.PRJ_ROOT %>images/muitendieuhuong.png" alt="next" />
                <a href='<%# GetActionUrl(Eval("Url").ToString()) %>' class="<%# GetClass(Eval("Url").ToString(),"dieuhuong123_showtieude","dieuhuong123") %>" >
                    <%# Eval("ActionName").ToString()%>
                </a>
            </ItemTemplate>
        </asp:Repeater>
	</div>
</div>


