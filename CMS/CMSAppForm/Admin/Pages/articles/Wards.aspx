﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true"
    CodeFile="Wards.aspx.cs" Inherits="Admin_Pages_articles_Wards" %>

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
                    height: 500,
                    width: 650,
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
            <table cellpadding="2" cellspacing="0" style="width: 100%">
                <tr>
                    <td style="width: 80px; white-space: nowrap;">
                        <asp:Label ID="lblCountries" runat="server" meta:resourcekey="lblCountries" Text="Countries:"></asp:Label>
                    </td>
                    <td style="width: 260px">
                        <asp:DropDownList ID="ddlCountries" runat="server" AutoPostBack="True" CssClass="userselect"
                            DataTextField="CountryDesc" DataValueField="CountryId" OnSelectedIndexChanged="ddlCountries_SelectedIndexChanged"
                            Width="250px">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 90px; white-space: nowrap;">
                        <asp:Label ID="lblProvinces" runat="server" meta:resourcekey="lblProvinces" Text="Provinces:"></asp:Label>
                    </td>
                    <td style="width: 260px">
                        <asp:DropDownList ID="ddlProvinces" runat="server" AutoPostBack="True" CssClass="userselect"
                            DataTextField="ProvinceDesc" DataValueField="ProvinceId" OnSelectedIndexChanged="ddlProvinces_SelectedIndexChanged"
                            Width="250px">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 90px; white-space: nowrap;">
                        <asp:Label ID="lblDistricts" runat="server" meta:resourcekey="lblDistrict" Text="District:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDistrict" runat="server" AutoPostBack="True" CssClass="userselect"
                            DataTextField="DistrictDesc" DataValueField="DistrictId" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                            Width="250px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblContentSearch" runat="server" meta:resourcekey="lblContentSearch"
                            Text="Từ khóa:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtContentSearch" runat="server" CssClass="tukhoatimekiem" Width="240px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblOrderBy" runat="server" meta:resourcekey="lblOrderBy" Text="Sắp xếp:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlOrderBy" runat="server" AutoPostBack="True" CssClass="userselect"
                            DataTextField="OrderByDesc" DataValueField="OrderBy" OnSelectedIndexChanged="ddlOrderBy_SelectedIndexChanged"
                            Width="250px">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                    <td style="width: 550px;">
                        <asp:LinkButton ID="btnSearch" runat="server" CssClass="timkiembutom" meta:resourcekey="btnSearch"
                            OnClick="btnSearch_Click" Text="Tìm kiếm">
                        </asp:LinkButton>
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
                    <span class="tieudetongcong">
                        <asp:Label ID="lblTotalText" runat="server" Text="Tổng cộng:" meta:resourcekey="lblTotalText"></asp:Label>
                    </span>
                    <asp:Label ID="lblTong" runat="server" Text="" CssClass="tieudetongcong2"></asp:Label>
                    <%-- <%=GetLocalResourceObject("Districts").ToString()%>--%>
                </div>
                <div class="chucnangright">
                    <a id="popup" href="WardsEdit.aspx?CountryId=<%=CountryId %>&ProvinceId=<%=ProvinceId %>&DistrictId=<%=DistrictId %>&LanguageId=<%=LanguageId %>&AppTypeId=<%=AppTypeId %>"
                        title="<%=GetLocalResourceObject("lnkAddNew.title").ToString() %>" class="themmoi">
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
                    <asp:GridView ID="m_grid" DataKeyNames="WardId" runat="server" ShowHeaderWhenEmpty="True"
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
                                        ToolTip='<%# Eval("WardId").ToString() %>'> </asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="3%" />
                                <HeaderStyle Width="3%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnVoteName" runat="server" Text="Tên" meta:resourcekey="lblGridColumnVoteName"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Literal ID="ltWardName" runat="server" EnableViewState="false" Text='<%# Eval("WardName") %>'></asp:Literal>
                                </ItemTemplate>
                                <ItemStyle Width="20%" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnDesc" runat="server" Text="Mô tả" meta:resourcekey="lblGridColumnDesc"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Literal ID="ltWardDesc" runat="server" EnableViewState="false" Text='<%# Eval("WardDesc") %>'></asp:Literal>
                                </ItemTemplate>
                                <ItemStyle Width="40%" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnDisplayOrder" runat="server" Text="Thứ tự hiển thị" meta:resourcekey="lblGridColumnDisplayOrder"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Literal ID="ltDisplayOrder" runat="server" EnableViewState="false" Text='<%# Eval("DisplayOrder") %>'></asp:Literal>
                                </ItemTemplate>
                                <ItemStyle Width="10%" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnTime" runat="server" Text="Thời gian" meta:resourcekey="lblGridColumnTime"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <span class="ngaythang">
                                        <%# DateTimeHelpers.GetDateAndTime(Eval("CrDateTime"))%><br />
                                    </span>
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
                                <ItemStyle Width="10%" HorizontalAlign="center" Wrap="false" />
                                <HeaderStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnOperation" runat="server" Text="Thao tác" meta:resourcekey="lblGridColumnOperation"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <a id="popup" href='WardsEdit.aspx?WardId=<%# Eval("WardId") %>'
                                        class="iconadmsua" title="<%# GetLocalResourceObject("lnkGridColumnEdit.title").ToString() %>"
                                        meta:resourcekey="lnkGridColumnEdit"></a>
                                    <asp:LinkButton ID="lbDelete" runat="server" CssClass="iconadmxoa" CommandName="Delete"
                                        ToolTip="Xóa" meta:resourcekey="lnkGridColumnDel"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Width="8%" Wrap="false" />
                                <HeaderStyle Width="8%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                <HeaderStyle Width="5%" />
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
                <uc1:CustomPaging ID="CustomPaging" runat="server" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
