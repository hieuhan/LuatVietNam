<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenuControl.ascx.cs" Inherits="ICSoft.AppForm.Admin.MenuControl" %>
<div class="menu">
	<ul id="nav">
		<li class="site-name">
            <a title="<%= ICSoft.HelperLib.Themes.GetItemAdmin("UC", "HOME", LanguageName, "Trang chủ")%>" href="<%= ICSoft.CMSLib.CmsConstants.PRJ_ROOT %>">&nbsp;</a>
        </li>
        <asp:Repeater ID="rptItemLevel1" runat="server" OnItemDataBound="rptItemLevel1_ItemDataBound">
            <ItemTemplate>
                <li>
                    <asp:HiddenField ID="hdfActionId" runat="server" Value='<%# Eval("ActionId").ToString() %>'
                        Visible="false" />
                    <a href='<%# GetActionUrl(Eval("Url").ToString()) %>' >
                        <%# Eval("ActionName").ToString()%>
                    </a>
                    <ul>
                        <asp:Repeater ID="rptItemLevel2" runat="server" OnItemDataBound="rptItemLevel2_ItemDataBound">
                            <ItemTemplate>
                                <li>
                                <asp:HiddenField ID="hdfActionId" runat="server" Value='<%# Eval("ActionId").ToString() %>'
                                Visible="false" />
                                <a href='<%# GetActionUrl(Eval("Url").ToString()) %>' >
                                    <%# Eval("ActionName").ToString()%>
                                </a>
                                <ul>
                                <asp:Repeater ID="rptItemLevel3" runat="server">
                                    <ItemTemplate>
                                        <li>
                                        <a href='<%# GetActionUrl(Eval("Url").ToString()) %>' >
                                            <%# Eval("ActionName").ToString()%>
                                        </a>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                                </ul>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </li>
            </ItemTemplate>
        </asp:Repeater>
	</ul>
</div>

