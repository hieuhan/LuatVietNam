<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="MessageSendRequests.aspx.cs" Inherits="Admin_Pages_lawsdocs_MessageSendRequests" %>

<%@ Import Namespace="ICSoft.CMSLib" %>
<%@ Import Namespace="sms.admin.security" %>
<%@ Import Namespace="ICSoft.HelperLib" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/admin/UserControls/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="uc1" %>
<asp:Content ID="contentAccountType" runat="server" ContentPlaceHolderID="m_contentBody">
    <script type="text/javascript">
        var cdialog = $('<div id="divEdit"></div>');
        var isProcessing = false;
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);
            $("#<%= txtDateFrom.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
            $("#<%= txtDateTo.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });

            $('a.popup').live('click', function (e) {
                var page = $(this).attr("href");
                cdialog
                .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="100%" height="100%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 460,
                    width: 600,
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

            $('a.popup2').on('click',
                function (e) {
                    e.preventDefault();
                    var name = 'popUp';
                    var popUrl = $(this).attr("href");
                    var w = $(this).attr("WWidth");
                    var appearence = 'dependent=yes,menubar=no,resizable=yes,scrollbars=yes,' +
                        'status=no,toolbar=no,titlebar=no,' +
                        'left=' + (screen.width - w) / 2 + ',top=30,width=' + w + ',height=' + $(this).attr("WHeight");
                    var openWindow = window.open(popUrl, name, appearence);
                    openWindow.focus();
                    return false;
                });
        });
        function InitializeRequest(sender, args) {

        }

        function EndRequest(sender, args) {
            $("#<%= txtDateFrom.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
            $("#<%= txtDateTo.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        }
        function checkProcess() {
            if(isProcessing)
            {
                alert("Đang duyệt yêu cầu, vui lòng đợi");
                return false;
            }

            var confirmResult = confirm('Bạn có chắc muốn duyệt dữ liệu này?');
            if (confirmResult)
                isProcessing = true;
            return confirmResult;
        }
    </script>
    <table cellpadding="3" cellspacing="3" class="tableBorder" style="width: 98%;">
        <tr>
            <td>
                <table cellpadding="2" cellspacing="0" style="width: 100%">
                    <tr>
                        <td style="width: 60px; white-space: nowrap;">Người tạo:
                        </td>
                        <td style="width: 260px">
                            <asp:DropDownList ID="ddlUsers" runat="server" DataTextField="UserName"
                                DataValueField="UserId" Width="250px" CssClass="userselect" OnSelectedIndexChanged="ddlUsers_SelectedIndexChanged"
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 60px; white-space: nowrap;">Bản tin:</td>
                        <td>
                            <asp:DropDownList ID="ddlRequestTypes" runat="server" DataTextField="RequestTypeDesc"
                                DataValueField="RequestTypeId" Width="250px" CssClass="userselect" OnSelectedIndexChanged="ddlRequestTypes_OnSelectedIndexChanged"
                                AutoPostBack="True">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="width: 60px; white-space: nowrap;">
                            <asp:Label ID="lblRequestStatus" runat="server" meta:resourcekey="lblSendStatus"
                                Text="Trạng thái:"></asp:Label>
                        </td>
                        <td style="width: 260px">
                            <asp:DropDownList ID="ddlRequestStatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRequestStatus_OnSelectedIndexChanged"
                                CssClass="userselect" DataTextField="RequestStatusDesc"
                                DataValueField="RequestStatusId"
                                Width="250px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 60px; white-space: nowrap;">
                            <asp:Label ID="lblOrderBy" runat="server" meta:resourcekey="lblOrderBy"
                                Text="Sắp xếp:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlOrderBy" runat="server" AutoPostBack="True"
                                CssClass="userselect" DataTextField="OrderByDesc" DataValueField="OrderBy" OnSelectedIndexChanged="ddlOrderBy_SelectedIndexChanged"
                                Width="250px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDateFrom" runat="server" meta:resourcekey="lblDateFrom"
                                Text="Từ ngày:"></asp:Label>
                        </td>
                        <td style="width: 260px">
                            <asp:TextBox ID="txtDateFrom" runat="server" CssClass="tukhoatimekiem"
                                Width="100px"></asp:TextBox>
                            <asp:Label ID="lblDateTo" runat="server" meta:resourcekey="lblDateTo"
                                Text="Đến"></asp:Label>
                            <asp:TextBox ID="txtDateTo" runat="server" CssClass="tukhoatimekiem"
                                Width="100px"></asp:TextBox>
                        </td>
                        <td><asp:Label ID="lblFindBydate" runat="server" meta:resourcekey="lblFindBydate" 
                                       Text="Tìm theo:"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="ddlSearchByDate" runat="server" AutoPostBack="False" 
                                              CssClass="userselect" 
                                              Width="250px">
                                <asp:ListItem Value="CrDateTime" Selected="True">Ngày tạo</asp:ListItem>
                                <asp:ListItem Value="BeginDateTime" >Thời gian bắt đầu gửi tin</asp:ListItem>
                                <asp:ListItem Value="GenStartTime">Thời gian tạo bản tin</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;<asp:LinkButton ID="btnSearch" runat="server" CssClass="timkiembutom"
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
                    </div>
                    <div class="chucnangright">
                        <a href="MessageSendRequestsEdit.aspx" title="Thêm mới " class="themmoi popup">
                            <asp:Label ID="lblAddNew" runat="server" Text="Thêm mới" meta:resourcekey="lblAddNew"></asp:Label>
                        </a>
                    </div>
                    <div style="text-align: left; width: 200px; float: right">
                    </div>
                    <div class="clear5px"></div>
                    <div class="contenbangdulieu">
                        <asp:GridView ID="m_grid" DataKeyNames="MessageSendRequestId" runat="server" ShowHeaderWhenEmpty="True"
                            AutoGenerateColumns="False" CssClass="filter-table" OnRowDeleting="m_grid_RowDeleting" OnRowDataBound="m_grid_OnRowDataBound" OnRowCommand="m_grid_OnRowCommand"
                            Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" PageSize="50">
                            <HeaderStyle CssClass="trbangdulieutieude" />
                            <FooterStyle CssClass="grid_foot" />
                            <RowStyle CssClass="trbangdulieutieudenoidung" />
                            <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                            <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />
                            <EditRowStyle CssClass="trbangdulieutieudenoidung" />
                            <PagerStyle CssClass="grid_page" />
                            <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lbNo" runat="server" Text='<%# Container.DataItemIndex + (CustomPaging.PageIndex - 1) * m_grid.PageSize + 1%>'
                                            ToolTip='<%# Eval("MessageSendRequestId").ToString() %>'>                                    
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bản tin">
                                    <ItemTemplate>
                                        <asp:Literal ID="ltRequestTypeId" runat="server" EnableViewState="false" Text='<%#RequestTypes.Static_Get(int.Parse(Eval("RequestTypeId").ToString()),ListRequestTypes).RequestTypeDesc  %>'></asp:Literal>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="200px"/>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Danh sách KH nhận">
                                    <ItemTemplate>
                                        <asp:Label ID="lblXemThem" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mã văn bản">
                                    <ItemTemplate>
                                        <asp:Literal ID="ltDocId" runat="server" EnableViewState="false" Text='<%# Eval("DocId").ToString() %>'></asp:Literal>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="80px"/>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Thời gian">
                                    <ItemTemplate>
                                        <p style="margin: 0;"><b>Bắt đầu:</b></p>
                                        <asp:Literal ID="ltBeginDateTime" runat="server" EnableViewState="false" Text='<%# Eval("BeginDateTime").Equals(DateTime.MinValue) ? "" : string.Format("{0:dd/MM/yyyy HH:mm}", (DateTime)Eval("BeginDateTime")) %>'></asp:Literal>
                                        <p style="margin: 0;"><b>Kết thúc:</b></p>
                                        <asp:Literal ID="ltEndDateTime" runat="server" EnableViewState="false" Text='<%# Eval("EndDateTime").Equals(DateTime.MinValue) ? "" : string.Format("{0:dd/MM/yyyy HH:mm}", (DateTime)Eval("EndDateTime")) %>'></asp:Literal>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="120px"/>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Trạng thái">
                                    <ItemTemplate>
                                        <asp:Literal ID="ltRequestStatusId" runat="server" EnableViewState="false" Text='<%# RequestStatus.Static_Get(int.Parse(Eval("RequestStatusId").ToString()),ListRequestStatus).RequestStatusDesc  %>'></asp:Literal>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tạo bởi">
                                    <ItemTemplate>
                                        <asp:Literal ID="ltUsers" runat="server" EnableViewState="false" Text='<%# UserHelpers.Static_Get(int.Parse(Eval("CrUserId").ToString()), ListUsers, "").Username  %>'></asp:Literal>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="80px"/>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tạo bản tin">
                                    <ItemTemplate>
                                        <asp:Label runat="server" id="lblTaoBanTin"></asp:Label>
                                        </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="120px"/>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Thống kê">
                                    <ItemTemplate>
                                        <p style="margin:0 0 0 10px">Total:<asp:Label runat="server" id="ltTotalEmail" Text='<%# Eval("TotalEmail") != null ? Eval("TotalEmail") : "" %>' Font-Bold="True"></asp:Label></p>
                                        <p style="margin:0 0 0 10px">Success:<asp:Label runat="server" id="ltTotalSendSuccess" Text='<%# Eval("TotalSendSuccess") != null ? Eval("TotalSendSuccess") : "" %>' Font-Bold="True"></asp:Label></p>
                                        <p style="margin:0 0 0 10px">Open:<asp:Label runat="server" id="ltTotalOpenMail" Text='<%# Eval("TotalOpenMail") != null ? Eval("TotalOpenMail") : "" %>' Font-Bold="True"></asp:Label></p>
                                        <p style="margin:0 0 0 10px">Click:<asp:Label runat="server" id="ltTotalClickLink" Text='<%# Eval("TotalClickLink") != null ? Eval("TotalClickLink") : "" %>' Font-Bold="True"></asp:Label></p>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="120px"/>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ngày tạo">
                                    <ItemTemplate>
                                        <asp:Literal ID="ltCrDateTime" runat="server" EnableViewState="false" Text='<%# Eval("CrDateTime").Equals(DateTime.MinValue) ? "" : string.Format("{0:dd/MM/yyyy HH:mm}", (DateTime)Eval("CrDateTime")) %>'></asp:Literal>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="120px"/>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Thao tác">
                                    <ItemStyle HorizontalAlign="center" Wrap="false" />
                                    <HeaderStyle Width="6%" />
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" id="lbtMessageSendRequestsEdit" Visible="False"></asp:LinkButton>
                                        <asp:LinkButton runat="server" id="lbtReview" Visible="False"></asp:LinkButton>
                                        <asp:LinkButton runat="server" id="lbtDelete" Visible="False"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="clear5px"></div>
                    <uc1:CustomPaging ID="CustomPaging" runat="server" />
                    <div class="clear5px"></div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
