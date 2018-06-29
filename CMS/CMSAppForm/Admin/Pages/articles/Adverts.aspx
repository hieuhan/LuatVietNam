<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true"
    CodeFile="Adverts.aspx.cs" Inherits="Admin_Adverts" %>

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

            $('a.popup').live('click', function (e) {
                var page = $(this).attr("href");
                var cdialog = $('<div id="divEdit"></div>')
                .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="98%" height="98%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 600,
                    width: 800,
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

            $('a.popup2').live('click', function (e) {
                var page = $(this).attr("href");
                var cdialog = $('<div id="divEdit"></div>')
                .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="98%" height="98%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 500,
                    width: 700,
                    title: $(this).attr("title"),
                    close: function (event, ui) {
                        $(this).remove();
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
    <%--<asp:UpdatePanel ID="upn_Grid" runat="server">
        <ContentTemplate>--%>
            <div style="text-align: center">
                <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
            <table cellpadding="2" cellspacing="0" style="width: 100%">
                <tr>
                    <td style="width: 100px">
                        <asp:Label ID="lblSite" runat="server" meta:resourcekey="lblSite" Text="Site:"></asp:Label>
                    </td>
                    <td style="width: 260px">
                        <asp:DropDownList ID="ddlSite" runat="server" AutoPostBack="True" CssClass="userselect"
                            DataTextField="SiteDesc" DataValueField="SiteId" OnSelectedIndexChanged="ddlSite_SelectedIndexChanged"
                            Width="250px">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 90px; white-space: nowrap;">
                        <asp:Label ID="lblPartner" runat="server" Text="Đối tác:" meta:resourcekey="lblPartner"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlPartner" runat="server" DataTextField="PartnerDesc" DataValueField="PartnerId"
                            Width="250px" CssClass="userselect" AutoPostBack="True" OnSelectedIndexChanged="ddlPartner_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td >
                        <asp:Label ID="lblType" runat="server" Text="Loại quảng cáo:" meta:resourcekey="lblType"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlType" runat="server" DataTextField="AdvertContentTypeDesc"
                            DataValueField="AdvertContentTypeId" Width="250px" CssClass="userselect" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblStatus" runat="server" Text="Trạng thái:" meta:resourcekey="lblStatus"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlStatus" runat="server" DataTextField="AdvertStatusDesc"
                            DataValueField="AdvertStatusId" Width="250px" CssClass="userselect" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 90px; white-space: nowrap;">
                        <asp:Label ID="lblKeyword" runat="server" Text="Từ khóa:" meta:resourcekey="lblKeyword"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="tukhoatimekiem" Width="240px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:LinkButton ID="btnSearch" runat="server" CssClass="timkiembutom" Text="Tìm kiếm"
                            meta:resourcekey="btnSearch" OnClick="btnSearch_Click">
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
                    <span class="tieudetongcong">
                        <asp:Label ID="lblTotalText" runat="server" Text="Tổng cộng:" meta:resourcekey="lblTotalText"></asp:Label>
                    </span>
                    <asp:Label ID="lblTong" runat="server" Text="" CssClass="tieudetongcong2"></asp:Label>
                    <%=GetLocalResourceObject("Advert").ToString() %>
                </div>
                <div class="chucnangright">
                    <asp:LinkButton ID="lbReview" runat="server" CssClass="duyettin" Text="Gen mã QC" ToolTip="Sau khi sửa quảng cáo, nhấn nút này để cập nhật hiển thị trên website" 
                         onclick="lbReview_Click"> 
                    </asp:LinkButton>
                    <a href="AdvertsEdit.aspx?SiteId=<%=SiteId %>" title="<%=GetLocalResourceObject("lnkAddNew.title").ToString() %>"
                        class="themmoi popup">
                        <asp:Label ID="lblAddNew" runat="server" Text="Thêm mới" meta:resourcekey="lblAddNew"></asp:Label>
                    </a>
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
                    <asp:GridView ID="m_grid" DataKeyNames="AdvertId" runat="server" ShowHeaderWhenEmpty="True"
                        AutoGenerateColumns="False" CssClass="filter-table" OnRowDeleting="m_grid_RowDeleting"
                        OnRowDataBound="m_grid_OnRowDataBound" Width="100%" CellPadding="0" CellSpacing="0"
                        BorderWidth="0">
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
                                    <asp:Label ID="lbNo" runat="server" Text='<%# Container.DataItemIndex + 1%>'>                                    
                                    </asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="3%" />
                                <HeaderStyle Width="3%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnName" runat="server" Text="Tên quảng cáo" meta:resourcekey="lblGridColumnName"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("AdvertName") %>
                                    <asp:Label ID="lblAdvertName" runat="server" Text='<%# Eval("AdvertName")%>' Visible="false"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnDesc" runat="server" Text="Mô tả" meta:resourcekey="lblGridColumnDesc"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("AdvertDesc") %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnURL" runat="server" Text="URL" meta:resourcekey="lblGridColumnURL"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <span class="ngaythang">
                                        <%# Eval("URL") %>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnFile" runat="server" Text="File" meta:resourcekey="lblGridColumnFile"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%--<asp:Label ID="lblImagePath" runat="server" Text=""></asp:Label>--%>
                                    <%# Eval("ImagePath").ToString() == "" ? "" : 
                                    FileUploadHelpers.IsImageFile(Eval("ImagePath").ToString()) 
                                    ? "<a class=\"popupImg\" target=\"_blank\" href=\"" + (Eval("ImagePath").ToString().StartsWith("http://") ? "" : CmsConstants.WEBSITE_MEDIADOMAIN) + Eval("ImagePath").ToString() + "\" /><img style=\"max-width:40px;max-height:40px\" src=\"" + (Eval("ImagePath").ToString().StartsWith("http://") ? "" : CmsConstants.WEBSITE_MEDIADOMAIN) + Eval("ImagePath").ToString().Replace("Original", "Icon") + "\" /></a>" 
                                    : "<a class=\"popupImg\"  href=\"" + (Eval("ImagePath").ToString().StartsWith("http://") ? "" : CmsConstants.WEBSITE_MEDIADOMAIN) + Eval("ImagePath").ToString() + "\" />Xem trước</a>"%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Width="68px" Wrap="false" />
                                <HeaderStyle Width="68px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnFile" runat="server" Text="File Hover"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("ImageHoverPath").ToString() == "" ? "" : 
                                    FileUploadHelpers.IsImageFile(Eval("ImageHoverPath").ToString()) 
                                    ? "<a class=\"popupImg\" target=\"_blank\" href=\"" + (Eval("ImageHoverPath").ToString().StartsWith("http://") ? "" : CmsConstants.WEBSITE_MEDIADOMAIN) + Eval("ImageHoverPath").ToString() + "\" /><img style=\"width:40px;height:40px\" src=\"" + (Eval("ImageHoverPath").ToString().StartsWith("http://") ? "" : CmsConstants.WEBSITE_MEDIADOMAIN) + Eval("ImageHoverPath").ToString().Replace("Original", "Icon") + "\" /></a>" 
                                    : "<a class=\"popupImg\"  href=\"" + (Eval("ImageHoverPath").ToString().StartsWith("http://") ? "" : CmsConstants.WEBSITE_MEDIADOMAIN) + Eval("ImageHoverPath").ToString() + "\" />Xem trước</a>"%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Width="68px" Wrap="false" />
                                <HeaderStyle Width="68px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnVitri" runat="server" Text="Vị trí" meta:resourcekey="lblGridColumnVitri"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <a href='AdvertPositionAdverts1.aspx?AdvertId=<%# Eval("AdvertId").ToString() %>'
                                        title="Vị trí" class="dieuhuong123 popup2">Xem</a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Width="68px" Wrap="false" />
                                <HeaderStyle Width="68px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnStatus" runat="server" Text="Trạng thái" meta:resourcekey="lblGridColumnStatus"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <span class="xuatban<%# Eval("AdvertStatusId") %>">
                                        <%# LanguageHelpers.IsCultureVietnamese() ? AdvertStatus.Static_Get(byte.Parse(Eval("AdvertStatusId").ToString()), l_AdvertStatus).AdvertStatusDesc : AdvertStatus.Static_Get(byte.Parse(Eval("AdvertStatusId").ToString()), l_AdvertStatus).AdvertStatusName%>
                                    </span>
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
                                        <%# DateTimeHelpers.GetDateAndTime(Eval("CrDateTime"))%>
                                    </span>
                                </ItemTemplate>
                                <ItemStyle Width="8%" HorizontalAlign="center" Wrap="false" />
                                <HeaderStyle Width="8%" />
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
                                <ItemStyle Width="8%" HorizontalAlign="center" Wrap="false" />
                                <HeaderStyle Width="8%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnOperation" runat="server" Text="Thao tác" meta:resourcekey="lblGridColumnOperation"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <a href='AdvertsEdit.aspx?AdvertId=<%# Eval("AdvertId") %>' class="iconadmsua popup"
                                        title="<%# GetLocalResourceObject("lnkGridColumnEdit.title").ToString() %>" meta:resourcekey="lnkGridColumnEdit">
                                    </a>
                                    <asp:LinkButton ID="lbDelete" runat="server" CssClass="iconadmxoa" CommandName="Delete"
                                        ToolTip="Xóa" meta:resourcekey="lnkGridColumnDel"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Width="68px" Wrap="false" />
                                <HeaderStyle Width="68px" />
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
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
            </div>
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
