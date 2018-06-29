<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true"
    CodeFile="ArticleComments.aspx.cs" Inherits="Admin_Pages_articles_ArticleComments" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/admin/UserControls/CustomPaging.ascx" TagName="CustomPaging"
    TagPrefix="uc1" %>
<%@ Import Namespace="ICSoft.CMSLib" %>
<%@ Import Namespace="ICSoft.HelperLib" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" runat="Server">
    <script type="text/javascript">
        function openPopupArticles() {
            var name = 'popUp';
            var popUrl = 'popupArticles.aspx?parent=articleComments';
            var appearence = 'dependent=yes,menubar=no,resizable=yes,scrollbars=yes,' +
                                          'status=no,toolbar=no,titlebar=no,' +
                                          'left=10,top=10,width=1300px,height=750px';
            var openWindow = window.open(popUrl, name, appearence);
            openWindow.focus();
            return false;
        }
        function updateArticlesList(articles) {
            document.getElementById("<%=txtArticles.ClientID %>").value = articles;
        }
        function submitButton(event) {
            if (event.which == 13) {
                $('#<%= btnSearch.ClientID %>').click();
	}
}
    </script>
    <asp:UpdatePanel ID="upn_Grid" runat="server">
        <ContentTemplate>
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
                    <td style="width: 80px; white-space: nowrap;">
                        <asp:Label ID="lblDataTypes" runat="server" meta:resourcekey="lblDataTypes" Text="Kiểu Dữ Liệu:"></asp:Label>
                    </td>
                    <td style="width: 260px">
                        <asp:DropDownList ID="ddlDataTypes" runat="server" AutoPostBack="True" CssClass="userselect"
                            DataTextField="DataTypeDesc" DataValueField="DataTypeId" OnSelectedIndexChanged="ddlDataTypes_SelectedIndexChanged"
                            Width="250px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr id="trArticle" runat="server" visible="false">
                    <td>
                        <asp:Label ID="lblArticles" runat="server" meta:resourcekey="lblArticles" Text="Tin Bài:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtArticles" runat="server" CssClass="tukhoatimekiem" Width="240px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button runat="server" ID="btnArticleSearch" Text="Chọn" meta:resourcekey="btnArticleSearch"
                            Width="60px" OnClientClick="return openPopupArticles();" />
                    </td>
                </tr>
                <tr id="trLanguage" runat="server" visible="false">
                    <td style="width: 80px; white-space: nowrap;">
                        <asp:Label ID="lblLanguage" runat="server" meta:resourcekey="lblLanguage" Text="Ngôn ngữ:"></asp:Label>
                    </td>
                    <td style="width: 260px">
                        <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="True" CssClass="userselect"
                            DataTextField="LanguageDesc" DataValueField="LanguageId" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged"
                            Width="250px">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 90px; white-space: nowrap;">
                        <asp:Label ID="lblAppType" runat="server" meta:resourcekey="lblAppType" Text="Loại ứng dụng:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAppType" runat="server" AutoPostBack="True" CssClass="userselect"
                            DataTextField="ApplicationTypeDesc" DataValueField="ApplicationTypeId" OnSelectedIndexChanged="ddlAppType_SelectedIndexChanged"
                            Width="250px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblDateFrom" runat="server" meta:resourcekey="lblDateFrom" Text="Từ ngày:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDateFrom" runat="server" CssClass="tukhoatimekiem" Width="100px"></asp:TextBox>
                        <asp:Label ID="lblDateTo" runat="server" meta:resourcekey="lblDateTo" Text="Đến:"></asp:Label>
                        <asp:TextBox ID="txtDateTo" runat="server" CssClass="tukhoatimekiem" Width="100px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblContentSearch" runat="server" meta:resourcekey="lblContentSearch"
                            Text="Từ khóa:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtContentSearch" runat="server" CssClass="tukhoatimekiem" Width="240px"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr id="trIP" runat="server" visible="false">
                    <td>
                        <asp:Label ID="lblFromIP" runat="server" meta:resourcekey="lblFromIP" Text="FromIP:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtFromIP" runat="server" CssClass="tukhoatimekiem" Width="240px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblUserAgent" runat="server" meta:resourcekey="lblUserAgent" Text="UserAgent:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtUserAgent" runat="server" CssClass="tukhoatimekiem" Width="240px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px; white-space: nowrap;">
                        <asp:Label ID="lblReviewStatus" runat="server" meta:resourcekey="lblReviewStatus"
                            Text="Trạng thái:"></asp:Label>
                    </td>
                    <td style="width: 260px;">
                        <asp:DropDownList ID="ddlReviewStatus" runat="server" AutoPostBack="True" CssClass="userselect"
                            DataTextField="ReviewStatusDesc" DataValueField="ReviewStatusId" OnSelectedIndexChanged="ddlReviewStatus_SelectedIndexChanged"
                            Width="250px">
                        </asp:DropDownList>
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
                    <td style="width: 550px;">
                        <asp:Button ID="btnSearch" runat="server" CssClass="timkiembutom" meta:resourcekey="btnSearch"
                            OnClick="btnSearch_Click" Text="Tìm kiếm">
                        </asp:Button>
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
                    <%-- <%=GetLocalResourceObject("ArticleComments").ToString()%>--%>
                </div>
                <div class="chucnangright">
                    <a style="display: none" id="popup" href="ArticleCommentsEdit.aspx?LanguageId=<%=LanguageId %>&AppTypeId=<%=AppTypeId %>"
                        title="<%=GetLocalResourceObject("lnkAddNew.title").ToString() %>" class="themmoi">
                        <asp:Label ID="lblAddNew" runat="server" Text="Thêm mới" meta:resourcekey="lblAddNew"></asp:Label>
                    </a>
                    <asp:LinkButton ID="lbReview" runat="server" CssClass="duyettin" Text="Duyệt" meta:resourcekey="lbReview"
                        OnClick="lbReview_Click"> 
                    </asp:LinkButton>
                    <asp:LinkButton ID="lbUnCheck" runat="server" CssClass="boduyet" Text="Bỏ duyệt"
                        meta:resourcekey="lbUnCheck" OnClick="lbUnCheck_Click"> 
                    </asp:LinkButton>
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
                    <asp:GridView ID="m_grid" DataKeyNames="ArticleCommentId" runat="server" ShowHeaderWhenEmpty="True"
                        AutoGenerateColumns="False" CssClass="filter-table" OnRowDeleting="m_grid_RowDeleting"
                        OnRowDataBound="m_grid_OnRowDataBound" Width="100%" CellPadding="0" CellSpacing="0"
                        BorderWidth="0" PageSize="50">
                        <HeaderStyle CssClass="trbangdulieutieude" />
                        <RowStyle CssClass="trbangdulieutieudenoidung"  VerticalAlign="Top" />
                        <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" VerticalAlign="Top" />
                        <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    #
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbNo" runat="server" Text='<%# Container.DataItemIndex + (CustomPaging.PageIndex - 1) * m_grid.PageSize + 1%>'
                                        ToolTip='<%# Eval("ArticleCommentId").ToString() %>'> </asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="3%" />
                                <HeaderStyle Width="3%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnFullName" runat="server" Text="Tin Bài" meta:resourcekey="lblGridColumnFullName"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <span style="font-weight: bold;">
                                        <%# Eval("Title") %></span><br /><br />
                                        <span style="color: Gray;">Tên: </span><%# Eval("FullName")%><br />
                                        <span style="color: Gray;">Email: </span><br />
                                        <span style="color: Gray;">Điện Thoại: </span><%# Eval("PhoneNumber")%><br />
                                        <span style="color: Gray;">IP: </span><%# Eval("FromIP")%><br />
                                        <span style="color: Gray;">UA: </span><%# Eval("UserAgent") %>
                                </ItemTemplate>
                                <ItemStyle Width="30%" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnComment" runat="server" Text="Bình Luận" meta:resourcekey="lblGridColumnComment"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <span style="color:Gray;"><asp:Label ID="ltRatingScore" runat="server" Text="Điểm đánh giá: " meta:resourcekey="ltRatingScore"></asp:Label></span> <%# Eval("RatingScore")%><br />
                                    <div style="width:auto; height:85px; overflow:auto;">
                                    <asp:Literal ID="ltGridColumnComment" runat="server" EnableViewState="false" Text='<%# Eval("Comment") %>'></asp:Literal>
                                    </div>
                                </ItemTemplate>
                                <ItemStyle Width="40%" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnReviewStatusId" runat="server" Text="Trạng thái" meta:resourcekey="lblGridColumnReviewStatusId"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <span class="xuatban<%# Eval("ReviewStatusId") %>">
                                        <%# LanguageHelpers.IsCultureVietnamese() ? ReviewStatus.Static_Get(byte.Parse(Eval("ReviewStatusId").ToString()), l_ReviewStatus).ReviewStatusDesc : ReviewStatus.Static_Get(byte.Parse(Eval("ReviewStatusId").ToString()), l_ReviewStatus).ReviewStatusName %></span>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                <HeaderStyle Width="7%" />
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
                                    <a style="display: none" id="popup" 
                                        href='ArticleCommentsEdit.aspx?ArticleCommentId=<%# Eval("ArticleCommentId") %>&LanguageId=<%# Eval("LanguageId") %>&AppTypeId=<%# Eval("ApplicationTypeId").ToString() %>'
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
                <div class="clear5px">
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
