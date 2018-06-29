<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true"
    CodeFile="MediaGroups.aspx.cs" Inherits="Admin_Pages_articles_MediaGroups" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/admin/UserControls/CustomPaging.ascx" TagName="CustomPaging"
    TagPrefix="uc1" %>
<%@ Import Namespace="ICSoft.CMSLib" %>
<%@ Import Namespace="ICSoft.HelperLib" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);


            $('a#popup').live('click', function (e) {
                var page = $(this).attr("href")
                var cdialog = $('<div id="divEdit"></div>')
                .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="98%" height="98%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 400,
                    width: 550,
                    title: $(this).attr("title"),
                    close: function (event, ui) {
                        $(this).remove();
                        window.location = $('#<%= btnSearch.ClientID %>').attr("href");
                        //$('#<%= btnSearch.ClientID %>').click();
                    }
                });
                cdialog.dialog('open');
                e.preventDefault();
            });
        });
        function InitializeRequest(sender, args) {
        }

        function EndRequest(sender, args) {

        }
    </script>
    <asp:UpdatePanel ID="upn_Grid" runat="server">
        <ContentTemplate>
            <div style="text-align: center">
                <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
            <table cellpadding="4" cellspacing="0" style="width: 100%">
                <tr>
                    <td style="width: 60px; white-space: nowrap;">
                        <asp:Label ID="lblSite" runat="server" Text="Site:" meta:resourcekey="lblSite"></asp:Label>
                    </td>
                    <td style="width: 260px">
                        <asp:DropDownList ID="ddlSite" runat="server" DataTextField="SiteDesc" DataValueField="SiteId"
                            Width="250px" CssClass="userselect" AutoPostBack="True" OnSelectedIndexChanged="ddlSite_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:LinkButton ID="btnSearch" runat="server" CssClass="timkiembutom" meta:resourcekey="btnSearch"
                            OnClick="btnSearch_Click" Text="Tìm kiếm">
                        </asp:LinkButton>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            <div class="clear5px">
            </div>
            <div class="vien">
            </div>
            <div class="clear5px">
            </div>
            <div class="khungchucnang">
                <div class="chucnangleft">
                    <span class="tieudetongcong" style="display: none">
                        <asp:Label ID="lblTotalText" runat="server" Text="Tổng cộng:" meta:resourcekey="lblTotalText"></asp:Label>
                    </span><span style="display: none">
                        <asp:Label ID="lblTong" runat="server" Text="" CssClass="tieudetongcong2"></asp:Label>
                    </span>
                </div>
                <div class="chucnangright">
                    <a id="popup" href="MediaGroupsEdit.aspx" title="<%=GetLocalResourceObject("lnkAddNew.title").ToString() %>"
                        class="themmoi">
                        <asp:Label ID="lblAddNew" runat="server" Text="Thêm mới" meta:resourcekey="lblAddNew"></asp:Label>
                    </a>
                    <asp:LinkButton ID="lbDelete" runat="server" CssClass="xoatin" Text="Xóa" meta:resourcekey="lbDelete"
                        OnClick="lbDelete_Click">
                    </asp:LinkButton>
                </div>
                <div style="text-align: left; width: 200px; float: right">
                    <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                        <ProgressTemplate>
                            <img style="text-align: center; vertical-align: middle" alt="loading..." src="../../Icons/loading.gif" />
                            Loading...</ProgressTemplate>
                    </asp:UpdateProgress>
                </div>
                <div class="clear5px">
                </div>
                <div class="contenbangdulieu">
                    <asp:GridView ID="m_grid" DataKeyNames="MediaGroupId" runat="server" ShowHeaderWhenEmpty="True"
                        AutoGenerateColumns="False" CssClass="filter-table" OnRowDeleting="m_grid_RowDeleting"
                        OnRowDataBound="m_grid_OnRowDataBound" Width="100%" CellPadding="0" CellSpacing="0"
                        BorderWidth="0" PageSize="50">
                        <HeaderStyle CssClass="trbangdulieutieude" />
                        <RowStyle CssClass="trbangdulieutieudenoidung" />
                        <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                        <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    #
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbNo" runat="server" Text='<%# Container.DataItemIndex + (CustomPaging.PageIndex - 1) * m_grid.PageSize + 1%>'
                                        ToolTip='<%# Eval("MediaGroupId").ToString() %>'> </asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="3%" />
                                <HeaderStyle Width="3%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnName" runat="server" Text="Tên" meta:resourcekey="lblGridColumnName"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Literal ID="ltMediaGroupName" runat="server" EnableViewState="false" Text='<%# Eval("MediaGroupName") %>'></asp:Literal>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="25%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnDataDesc" runat="server" Text="Mô tả" meta:resourcekey="lblGridColumnDataDesc"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Literal ID="ltMediaGroupDesc" runat="server" EnableViewState="false" Text='<%# Eval("MediaGroupDesc") %>'></asp:Literal>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="25%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnTime" runat="server" Text="Thời gian" meta:resourcekey="lblGridColumnTime"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <span class="ngaythang">
                                        <%# DateTimeHelpers.GetDateAndTime(Eval("CrDateTime"))%></span>
                                </ItemTemplate>
                                <ItemStyle Width="10%" HorizontalAlign="center" Wrap="false" />
                                <HeaderStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnUser" runat="server" Text="Tạo bởi" meta:resourcekey="lblGridColumnUser"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <span class="ngaythang">
                                        <%# UserHelpers.Static_Get(int.Parse(Eval("CrUserId").ToString()), l_Users, "").Username %>
                                    </span>
                                </ItemTemplate>
                                <ItemStyle Width="15%" HorizontalAlign="center" Wrap="false" />
                                <HeaderStyle Width="15%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnOperation" runat="server" Text="Thao tác" meta:resourcekey="lblGridColumnOperation"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <a id="popup" onmouseover="ShowMenu('-1-<%# Eval("MediaGroupId") %>')" href='MediaGroupsEdit.aspx?MediaGroupId=<%# Eval("MediaGroupId") %>'
                                        class="iconadmsua" title="<%# GetLocalResourceObject("lnkGridColumnEdit.title").ToString() %>"
                                        meta:resourcekey="lnkGridColumnEdit"></a>
                                    <asp:LinkButton ID="lbDelete" runat="server" CssClass="iconadmxoa" CommandName="Delete"
                                        ToolTip="Xóa" meta:resourcekey="lnkGridColumnDel"></asp:LinkButton>
                                    <br />
                                    <a class="dropdown-menu-hover-1-<%# Eval("MediaGroupId") %>" href="#" data-dropdown="#dropdown-1-<%# Eval("MediaGroupId") %>">
                                    </a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Width="20px" Wrap="false" />
                                <HeaderStyle Width="20px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                                <HeaderStyle Width="40px" />
                                <HeaderTemplate>
                                    <input type="checkbox" name="SelectAllCheckBox" onclick="SelectAll(this)">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkAction" runat="server"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="clear5px">
                </div>
                <div class="clear5px">
                </div>
            </div>
            <div class="clear5px">
            </div>
            <span style="display: none">
                <uc1:CustomPaging ID="CustomPaging" runat="server" />
            </span>
            <div class="clear5px">
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
