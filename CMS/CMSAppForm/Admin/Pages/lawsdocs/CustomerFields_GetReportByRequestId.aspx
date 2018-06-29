<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="CustomerFields_GetReportByRequestId.aspx.cs" Inherits="Admin_Pages_lawsdocs_CustomerFields_GetReportByRequestId" %>

<%@ Register Src="~/admin/UserControls/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" runat="Server">
    <script src="../../Js/ajaxTooltip/ajax-tooltip.js" type="text/javascript"></script>
    <script src="../../Js/ajaxTooltip/ajax-dynamic-content.js" type="text/javascript"></script>
    <script src="../../Js/ajaxTooltip/ajax.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);

            $('a.popup').live('click', function (e) {
                var page = $(this).attr("href")
                var cdialog = $('<div id="divEdit"></div>')
                    .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="98%" height="98%"></iframe>')
                    .dialog({
                        autoOpen: false,
                        modal: true,
                        height: 500,
                        width: 900,
                        title: 'Nội dung bản tin',
                        close: function (event, ui) {
                            $(this).remove();
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
        }
    </script>
    <div style="padding: 10px;">
        <table cellpadding="4" cellspacing="0" style="width: 100%">
            <tr>
                <td>Từ khóa:
                <asp:TextBox ID="txtSearch" runat="server" CssClass="tukhoatimekiem" Width="180px"></asp:TextBox>
                    <asp:LinkButton ID="btnSearch" runat="server" CssClass="timkiembutom"
                        Text="Tìm kiếm" meta:resourcekey="btnSearch" OnClick="btnSearch_Click">
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
            <div class="chucnangright" style="text-align:center; float:none;">
                <asp:Label ID="lblMsg" runat="server" Text="" Font-Bold="true" ForeColor="Red" CssClass="tieudetongcong2"></asp:Label>
            </div>
            <div class="clear5px"></div>
            <div class="contenbangdulieu">
                <asp:GridView ID="m_grid" DataKeyNames="CustomerId" runat="server" ShowHeaderWhenEmpty="True"
                    AutoGenerateColumns="False" CssClass="filter-table" OnRowDataBound="m_grid_OnRowDataBound"
                    Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" PageSize="20">
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
                                <asp:Label ID="lblGridColumnName" runat="server" Text="Tên truy cập" meta:resourcekey="lblGridColumnName"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="hlCustomerName" runat="server"></asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblGridColumnName" runat="server" Text="Họ và tên" meta:resourcekey="lblGridColumnName"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCustomerFullName" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblGridColumnFullname" runat="server" Text="Email" meta:resourcekey="lblGridColumnFullname"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCustomerEmail" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblGridColumnFullname" runat="server" Text="Nội dung gửi" meta:resourcekey="lblGridColumnFullname"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="hlContent" runat="server"><b style="color:blue">Xem</b></asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="clear5px"></div>
        <uc1:CustomPaging ID="CustomPaging" runat="server" />
        <div class="clear5px"></div>
    </div>
</asp:Content>

