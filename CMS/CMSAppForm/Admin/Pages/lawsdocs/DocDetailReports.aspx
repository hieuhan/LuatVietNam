<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="DocDetailReports.aspx.cs" Inherits="Admin_Pages_lawsdocs_DocDetailReports" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/admin/UserControls/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="uc1" %>
<%@ Import Namespace="ICSoft.CMSLib" %>
<%@ Import Namespace="ICSoft.LawDocsLib" %>
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
            var page = $(this).attr("href");
            var cdialog = $('<div id="divEdit"></div>')
            .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="98%" height="98%"></iframe>')
            .dialog({
                autoOpen: false,
                modal: true,
                height: $(this).attr("WHeight"),
                width: $(this).attr("WWidth"),
                title: $(this).attr("title"),
                close: function (event, ui) {
                    $(this).remove();
                    //window.location = $('#<%= btnSearch.ClientID %>').attr("href");
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
            SetStartup();
            $("#<%= txtDateFrom.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
            $("#<%= txtDateTo.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
            $('input[type="hidden"][id*="hdfOldPage"]').val(0);
            var x = $('.khungcontenpading').offset().top - 100;
            jQuery('html,body').animate({ scrollTop: x }, 100);
        }
    </script>
    <%--<asp:UpdatePanel ID="upn_Grid" runat="server">
        <ContentTemplate>--%>
            <div style="text-align: center">
                <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
            <table cellpadding="3" cellspacing="0" style="width: 100%">
                <tr>
                    <td style="width: 120px;">
                        <asp:Label ID="lblOrgans" runat="server" meta:resourcekey="lblLanguage1"
                            Text="Cơ quan ban hành:"></asp:Label>
                    </td>
                    <td style="width: 250px; padding-right: 20px;">
                        <asp:DropDownList ID="ddlOrgans" runat="server" AutoPostBack="True"
                            CssClass="userselect" DataTextField="OrganName" DataValueField="OrganId"
                            OnSelectedIndexChanged="ddlOrgans_SelectedIndexChanged" Width="250px">
                        </asp:DropDownList>
                    </td>

                    <td valign="top" style="width: 120px; height: 32px; line-height: 32px;">
                        <asp:Label ID="lblWeek" runat="server" meta:resourcekey="lblWeek"
                            Text="Xem báo cáo theo:"></asp:Label>
                    </td>
                    <td valign="top">
                        <asp:DropDownList ID="ddlDatetime" runat="server" CssClass="userselect"
                            Width="250px" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlDatetime_SelectedIndexChanged">
                            <asp:ListItem Value="0" Selected="True">Tùy chỉnh</asp:ListItem>
                            <asp:ListItem Value="1">Ngày</asp:ListItem>
                            <asp:ListItem Value="2">Tuần</asp:ListItem>
                            <asp:ListItem Value="3">Tháng</asp:ListItem>
                            <asp:ListItem Value="4">Năm</asp:ListItem>
                        </asp:DropDownList>
                    </td>

                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblReviewStatus" runat="server" meta:resourcekey="lblReviewStatus"
                            Text="Tình trạng:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlReviewStatus" runat="server" AutoPostBack="True"
                            CssClass="userselect" DataTextField="ReviewStatusDesc" DataValueField="ReviewStatusId"
                            OnSelectedIndexChanged="ddlReviewStatus_SelectedIndexChanged" Width="250px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblCrUserId" runat="server" meta:resourcekey="lblCrUserId"
                            Text="Biên tập viên:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCrUserId" runat="server" AutoPostBack="True"
                            CssClass="userselect" DataTextField="UserName" DataValueField="UserId"
                            OnSelectedIndexChanged="ddlCrUserId_SelectedIndexChanged" Width="250px">
                        </asp:DropDownList>
                    </td>

                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblRevUserId" runat="server" meta:resourcekey="lblRevUserId"
                            Text="Người duyệt:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlRevUserIds" runat="server" AutoPostBack="True"
                            CssClass="userselect" DataTextField="UserName" DataValueField="UserId"
                            OnSelectedIndexChanged="ddlRevUserId_SelectedIndexChanged" Width="250px">
                        </asp:DropDownList>
                    </td>

                    <td valign="top" style="width: 120px; height: 32px; line-height: 32px;">
                        <asp:Label ID="Label1" runat="server" meta:resourcekey="lblOrderBy"
                            Text="Sắp xếp:"></asp:Label>
                    </td>
                    <td valign="top">
                        <asp:DropDownList ID="ddlOrderBy" runat="server" AutoPostBack="True"
                            CssClass="userselect" DataTextField="OrderByDesc" DataValueField="OrderBy"
                            OnSelectedIndexChanged="ddlOrderBy_SelectedIndexChanged" Width="250px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>

                    <td>
                        <asp:Label ID="lblDateFrom" runat="server" meta:resourcekey="lblDateFrom"
                            Text="Từ ngày:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDateFrom" runat="server" CssClass="tukhoatimekiem"
                            Width="100px"></asp:TextBox>
                        <asp:Label ID="lblDateTo" runat="server" meta:resourcekey="lblDateTo"
                            Text="Đến"></asp:Label>
                        <asp:TextBox ID="txtDateTo" runat="server" CssClass="tukhoatimekiem"
                            Width="100px"></asp:TextBox>
                    </td>

                    <td>
                        <asp:Label ID="Label2" runat="server"
                            meta:resourcekey="lblDataSources" Text="Nguồn dữ liệu:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDataSources" runat="server" AutoPostBack="True"
                            CssClass="userselect" DataTextField="DataSourceDesc"
                            DataValueField="DataSourceId"
                            OnSelectedIndexChanged="ddlDataSources_SelectedIndexChanged" Width="250px">
                        </asp:DropDownList>
                    </td>

                </tr>
                <tr>
                    <td></td>
                    <td></td>
                </tr>
                <tr>

                    <td>
                        <asp:Label ID="lblFields" runat="server" meta:resourcekey="lblDataSources1"
                            Text="Lĩnh vực:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlFields" runat="server" AutoPostBack="True"
                            CssClass="userselect" DataTextField="FieldDesc"
                            DataValueField="FieldId"
                            OnSelectedIndexChanged="ddlDataSources_SelectedIndexChanged" Width="250px">
                        </asp:DropDownList>
                    </td>

                    <td></td>
                    <td>
                        <asp:LinkButton ID="btnSearch" runat="server" CssClass="timkiembutom"
                            meta:resourcekey="btnSearch" OnClick="btnSearch_Click" Text="Tìm kiếm">
                        </asp:LinkButton>

                    </td>
                </tr>
            </table>
            <div class="clear5px"></div>
            <div class="vien"></div>
            <div class="clear5px"></div>
            <div class="khungchucnang">
                <div class="chucnangleft">
                    <span class="tieudetongcong">
                        <asp:Label ID="lblTotalText" runat="server" Text="Tổng cộng:" meta:resourcekey="lblTotalText"></asp:Label>
                    </span>
                    <asp:Label ID="lblTong" runat="server" Text="" CssClass="tieudetongcong2"></asp:Label>
                    <%=GetLocalResourceObject("Docs").ToString() %>
                    &nbsp;| <span class="tieudetongcong">
                        <asp:Label ID="Label3" runat="server" Text="Đã duyệt:" meta:resourcekey="lblTotalText1"></asp:Label>
                    </span>
        <asp:Label ID="lblReviewed" runat="server" Text="" CssClass="tieudetongcong2"></asp:Label>
                    | 
        <span class="tieudetongcong">
            <asp:Label ID="Label4" runat="server" Text="Chờ duyệt:" meta:resourcekey="lblTotalText1"></asp:Label>
        </span>
                    <asp:Label ID="lblPending" runat="server" Text="" CssClass="tieudetongcong2"></asp:Label>
                    | 
        <span class="tieudetongcong">
            <asp:Label ID="Label5" runat="server" Text="Thay đổi:" meta:resourcekey="lblTotalText1"></asp:Label>
        </span>
                    <asp:Label ID="lblUnreview" runat="server" Text="" CssClass="tieudetongcong2"></asp:Label>

                </div>
                <div class="chucnangright">
                    <asp:Button ID="btnXuatExcel" runat="server" Text="Xuất Excel" Width="90px" CssClass="timkiembutom" OnClick="btnXuatExcel_Click" />
                </div>

                <%--<div style="text-align: left; width: 40%; float: right">
                    <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                        <ProgressTemplate>
                            <img style="text-align: center; vertical-align: middle" alt="loading..." src="../../Icons/loading.gif" />
                            Loading...</ProgressTemplate>
                    </asp:UpdateProgress>
                </div>--%>
                <div class="clear5px"></div>
                <div class="contenbangdulieu">
                    <asp:GridView ID="m_grid" DataKeyNames="DocId" runat="server" ShowHeaderWhenEmpty="True"
                        AutoGenerateColumns="False"
                        Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0"
                        PageSize="50">
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
                                        ToolTip='<%# Eval("DocId").ToString() %>'> </asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="3%" />
                                <HeaderStyle Width="3%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnDocName" runat="server" Text="Tên văn bản" meta:resourcekey="lblGridColumnDocName"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <a id="edit" runat="server" href='<%# "DocsEdit.aspx?DocId=" + Eval("DocId").ToString()  %>' class="docsproperties">
                                        <asp:Label ID="lbDocName" runat="server" Text='<%# Eval("DocName")%>' Style="font-weight: bold;"></asp:Label>
                                    </a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="40%" VerticalAlign="Top" />
                                <HeaderStyle />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnGazetteDate" runat="server" Text="Ngày ban hành" meta:resourcekey="lblGridColumnGazetteDate"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbDocIdentity" runat="server" Style="display: none"></asp:Label>
                                    <%# GetIssueDate(Eval("DocGroupId").ToString(),Eval("IssueDate").ToString(),Eval("IssueYear").ToString())%>
                                    <br />
                                    <br />
                                </ItemTemplate>
                                <ItemStyle Width="8%" HorizontalAlign="Center" VerticalAlign="Top" Wrap="false" />
                                <HeaderStyle Width="8%" />
                                <HeaderStyle />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnDocOrgan" runat="server" Text="Cơ quan ban hành" meta:resourcekey="lblGridColumnDocOrgan"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbDocIdentity" runat="server" Style="display: none"></asp:Label>
                                    <%# Eval("OrganName") %>
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                </ItemTemplate>
                                <ItemStyle Width="10%" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                                <HeaderStyle Width="10%" />
                                <HeaderStyle />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnDocField" runat="server" Text="Lĩnh vực" meta:resourcekey="lblGridColumnDocField"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbDocIdentity" runat="server" Style="display: none"></asp:Label>
                                    <%# Eval("FieldName") %>
                                    <br />
                                    <br />
                                </ItemTemplate>
                                <ItemStyle Width="8%" HorizontalAlign="Left" VerticalAlign="Top" Wrap="false" />
                                <HeaderStyle Width="8%" />
                                <HeaderStyle />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnStatus" runat="server" Text="Tình trạng" meta:resourcekey="lblGridColumnStatus"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <span style="color: #0C0C0C;"><%# LanguageHelpers.IsCultureVietnamese() ? ReviewStatus.Static_Get(byte.Parse(Eval("ReviewStatusId").ToString()), l_ReviewStatus).ReviewStatusDesc : ReviewStatus.Static_Get(byte.Parse(Eval("ReviewStatusId").ToString()), l_ReviewStatus).ReviewStatusName%></span><br />
                                </ItemTemplate>
                                <ItemStyle Width="8%" HorizontalAlign="Center" VerticalAlign="Top" Wrap="false" />
                                <HeaderStyle Width="8%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnCrUser" runat="server" Text="Biên tập viên" meta:resourcekey="lblGridColumnCrUser"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <span style="color: #0C0C0C;"><%# UserHelpers.Static_Get(int.Parse(Eval("CrUserId").ToString()), l_Users, "").Username %></span><br />
                                </ItemTemplate>
                                <ItemStyle Width="8%" HorizontalAlign="Center" VerticalAlign="Top" Wrap="false" />
                                <HeaderStyle Width="8%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnRevUser" runat="server" Text="Người duyệt" meta:resourcekey="lblGridColumnRevUser"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <span style="color: #0C0C0C;"><%# UserHelpers.Static_Get(int.Parse(Eval("RevUserId").ToString()), l_Users, "").Username %></span><br />
                                </ItemTemplate>
                                <ItemStyle Width="8%" HorizontalAlign="Center" VerticalAlign="Top" Wrap="false" />
                                <HeaderStyle Width="8%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnDataSource" runat="server" Text="Nguồn dữ liệu" meta:resourcekey="lblGridColumnDataSource"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <span style="color: #0C0C0C;"><%# DataSources.Static_Get(short.Parse(Eval("DataSourceId").ToString()),l_DataSources).DataSourceDesc %></span><br />
                                </ItemTemplate>
                                <ItemStyle Width="8%" HorizontalAlign="Center" VerticalAlign="Top" Wrap="true" />
                                <HeaderStyle Width="8%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnDownload" runat="server" Text="Lượt tải" meta:resourcekey="lblGridColumnDownload"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <span style="color: #0C0C0C;"><%# Eval("DownloadCount")%></span><br />
                                </ItemTemplate>
                                <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Top" Wrap="false" />
                                <HeaderStyle Width="5%" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="clear5px"></div>
            <uc1:CustomPaging ID="CustomPaging" runat="server" />
            <div class="clear5px"></div>
<%--</div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnXuatExcel" />
        </Triggers>
    </asp:UpdatePanel>--%>
</asp:Content>

