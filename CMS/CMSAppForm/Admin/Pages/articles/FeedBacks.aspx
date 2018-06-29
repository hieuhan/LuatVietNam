<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true"
    CodeFile="FeedBacks.aspx.cs" Inherits="Admin_FeedBacks" EnableEventValidation="false" %>

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
            $("#<%= txtDateFrom.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
            $("#<%= txtDateTo.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });

            $('a#popup').live('click', function (e) {
                var page = $(this).attr("href")
                var cdialog = $('<div id="divEdit"></div>')
                .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="98%" height="98%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 540,
                    width: 750,
                    title: $(this).attr("title"),
                    close: function (event, ui) {
                        $(this).remove();
                        //window.location = $('#<%= btnSearch.ClientID %>').attr("href");
                        $('#<%= btnSearch.ClientID %>').click();
                    }
                });
                cdialog.dialog('open');
                e.preventDefault();
            });
        });
        function InitializeRequest(sender, args) {
        }

        function EndRequest(sender, args) {
            $("#<%= txtDateFrom.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
            $("#<%= txtDateTo.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        }
        function submitButton(event) {
            if (event.which == 13) {
                $('#<%= btnSearch.ClientID %>').click();
            }
        }
    </script>
    <%--<asp:UpdatePanel ID="upn_Grid" runat="server">
        <ContentTemplate>--%>
            <div style="text-align: center">
                <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
            <table cellpadding="2" cellspacing="0" style="width: 100%">
                <tr>
                    <td style="width: 80px; white-space: nowrap;">
                        <asp:Label ID="lblSite" runat="server" meta:resourcekey="lblSite" Text="Site:"></asp:Label>
                    </td>
                    <td style="width: 260px">
                        <asp:DropDownList ID="ddlSite" runat="server" AutoPostBack="True" CssClass="userselect"
                            DataTextField="SiteDesc" DataValueField="SiteId" OnSelectedIndexChanged="ddlSite_SelectedIndexChanged"
                            Width="250px">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 90px; white-space: nowrap;">
                        <asp:Label ID="lblLanguage" runat="server" meta:resourcekey="lblLanguage" Text="Ngôn ngữ:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="True" CssClass="userselect"
                            DataTextField="LanguageDesc" DataValueField="LanguageId" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged"
                            Width="250px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 90px; white-space: nowrap;">
                        <asp:Label ID="lblAppType" runat="server" meta:resourcekey="lblAppType" Text="Loại ứng dụng:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAppType" runat="server" AutoPostBack="True" CssClass="userselect"
                            DataTextField="ApplicationTypeDesc" DataValueField="ApplicationTypeId" OnSelectedIndexChanged="ddlAppType_SelectedIndexChanged"
                            Width="250px">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 80px; white-space: nowrap;">
                        <asp:Label ID="lblFeedBackGroups" runat="server" meta:resourcekey="lblFeedBackGroups"
                            Text="Nhóm:"></asp:Label>
                    </td>
                    <td style="width: 260px">
                        <asp:DropDownList ID="ddlFeedBackGroups" runat="server" AutoPostBack="True" CssClass="userselect"
                            DataTextField="FeedBackGroupDesc" DataValueField="FeedBackGroupId" OnSelectedIndexChanged="ddlFeedBackGroups_SelectedIndexChanged"
                            Width="250px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 90px; white-space: nowrap;">
                        <asp:Label ID="lblReviewStatus" runat="server" meta:resourcekey="lblReviewStatus"
                            Text="Trạng thái:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlReviewStatus" runat="server" AutoPostBack="True" CssClass="userselect"
                            DataTextField="ReviewStatusDesc" DataValueField="ReviewStatusId" OnSelectedIndexChanged="ddlReviewStatus_SelectedIndexChanged"
                            Width="250px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblDateFrom" runat="server" meta:resourcekey="lblDateFrom" Text="Từ ngày:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDateFrom" runat="server" CssClass="tukhoatimekiem" Width="100px"></asp:TextBox>
                        <asp:Label ID="lblDateTo" runat="server" meta:resourcekey="lblDateTo" Text="đến:"></asp:Label>
                        <asp:TextBox ID="txtDateTo" runat="server" CssClass="tukhoatimekiem" Width="100px"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblOrderBy" runat="server" meta:resourcekey="lblOrderBy" Text="Sắp xếp:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlOrderBy" runat="server" AutoPostBack="True" CssClass="userselect"
                            DataTextField="OrderByDesc" DataValueField="OrderBy" OnSelectedIndexChanged="ddlOrderBy_SelectedIndexChanged"
                            Width="250px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" CssClass="timkiembutom" meta:resourcekey="btnSearch"
                            OnClick="btnSearch_Click" Text="Tìm kiếm">
                        </asp:Button>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
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
                    <%=GetLocalResourceObject("FeedBacks").ToString()%>

                    &nbsp;  &nbsp; <asp:Button ID="btnXuatExcel" runat="server" Text="Xuất Excel" width="90px" CssClass="timkiembutom" OnClick="btnXuatExcel_Click"/>
                </div>
                <div class="chucnangright">
                    <a id="popup" href="FeedBacksEdit.aspx?LanguageId=<%=LanguageId %>&AppTypeId=<%=AppTypeId %>"
                        title="<%=GetLocalResourceObject("lnkAddNew.title").ToString() %>" class="themmoi">
                        <asp:Label ID="lblAddNew" runat="server" Text="Thêm mới" meta:resourcekey="lblAddNew"></asp:Label>
                    </a>
                    <asp:LinkButton ID="lbReview" runat="server" CssClass="duyettin" Text="Duyệt" Visible="false"
                        meta:resourcekey="lbReview" OnClick="lbReview_Click"> 
                    </asp:LinkButton>
                    <asp:LinkButton ID="lbUnCheck" runat="server" CssClass="boduyet" Visible="false"
                        Text="Bỏ duyệt" meta:resourcekey="lbUnCheck" OnClick="lbUnCheck_Click"> 
                    </asp:LinkButton>
                    <asp:LinkButton ID="lbDelete" runat="server" CssClass="xoatin" Text="Xóa" meta:resourcekey="lbDelete"
                        OnClick="lbDelete_Click">
                    </asp:LinkButton>
                </div>
                <%--<div style="text-align: left; width: 200px; float: right">
                    <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                        <ProgressTemplate>
                            <img style="text-align: center; vertical-align: middle" alt="loading..." src="../../Icons/loading.gif" />
                            Loading...</ProgressTemplate>
                    </asp:UpdateProgress>
                </div>--%>
                <div class="clear5px">
                </div>
                <div class="contenbangdulieu">
                    <asp:GridView ID="m_grid" DataKeyNames="FeedBackId" runat="server" ShowHeaderWhenEmpty="True"
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
                                        ToolTip='<%# Eval("FeedBackId").ToString() %>'> </asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="3%" />
                                <HeaderStyle Width="3%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnComment" runat="server" Text="Nội dung" meta:resourcekey="lblGridColumnComment"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <b><asp:Literal ID="ltTitle" runat="server" EnableViewState="false" Text='<%# Eval("Title") %>'></asp:Literal></b>
                                    <div style="width: 600px; max-height: 80px; overflow: auto">
                                        <asp:Literal ID="ltComment" runat="server" EnableViewState="false" Text='<%# Eval("Comment") %>'></asp:Literal>
                                    </div>
                                </ItemTemplate>
                                <ItemStyle Width="15%" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnFullName" runat="server" Text="Người gửi" meta:resourcekey="lblGridColumnFullName"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Literal ID="ltFullName" runat="server" EnableViewState="false" Text='<%# Eval("FullName") %>'></asp:Literal>
                                    <asp:Label ID="lblFullName" runat="server" Text='<%# Eval("FullName")%>' Visible="false"></asp:Label>
                                    <br />
                                    <asp:Literal ID="ltPhoneNumber" runat="server" EnableViewState="false" Text='<%# Eval("PhoneNumber") %>'></asp:Literal><br />
                                    <asp:Literal ID="ltEmail" runat="server" EnableViewState="false" Text='<%# Eval("Email") %>'></asp:Literal>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnOrganName" runat="server" Text="Tên cơ quan" meta:resourcekey="lblGridColumnOrganName"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Literal ID="ltOrganName" runat="server" EnableViewState="false" Text='<%# Eval("OrganName") %>'></asp:Literal>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnStatus" runat="server" Text="Trạng thái" meta:resourcekey="lblGridColumnStatus"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <span class="xuatban<%# Eval("ReviewStatusId") %>">
                                        <%# LanguageHelpers.IsCultureVietnamese() ? ReviewStatus.Static_Get(byte.Parse(Eval("ReviewStatusId").ToString()), l_ReviewStatus).ReviewStatusDesc : ReviewStatus.Static_Get(byte.Parse(Eval("ReviewStatusId").ToString()), l_ReviewStatus).ReviewStatusName %></span>
                                </ItemTemplate>
                                <ItemStyle Width="10%" HorizontalAlign="Left" Wrap="false" />
                                <HeaderStyle Width="10%" />
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
                                    <asp:Label ID="lblGridColumnOperation" runat="server" Text="Thao tác" meta:resourcekey="lblGridColumnOperation"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <a id="popup" onmouseover="ShowMenu('-1-<%# Eval("FeedBackId") %>')" href='FeedBacksEdit.aspx?FeedBackId=<%# Eval("FeedBackId") %>&LanguageId=<%# Eval("LanguageId") %>&AppTypeId=<%# Eval("ApplicationTypeId").ToString() %>'
                                        class="iconadmsua" title="<%# GetLocalResourceObject("lnkGridColumnEdit.title").ToString() %>"
                                        meta:resourcekey="lnkGridColumnEdit"></a>
                                    <asp:LinkButton ID="lbDelete" runat="server" CssClass="iconadmxoa" CommandName="Delete"
                                        ToolTip="Xóa" meta:resourcekey="lnkGridColumnDel"></asp:LinkButton>
                                    <asp:Label ID="lblLanguageId" runat="server" Text='<%# Eval("LanguageId") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblAppTypeId" runat="server" Text='<%# Eval("ApplicationTypeId") %>'
                                        Visible="false"></asp:Label>
                                    <br />
                                    <a class="dropdown-menu-hover-1-<%# Eval("FeedBackId") %>" href="#" data-dropdown="#dropdown-1-<%# Eval("FeedBackId") %>">
                                    </a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Width="6%" Wrap="false" />
                                <HeaderStyle Width="6%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle HorizontalAlign="Center" Width="3%" />
                                <HeaderStyle Width="3%" />
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
                <div class="clear5px">
                </div>
            </div>
        <%--</ContentTemplate>
        <Triggers>
        <asp:PostBackTrigger ControlID="btnXuatExcel" />
    </Triggers>
    </asp:UpdatePanel>--%>
</asp:Content>
