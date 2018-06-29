<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master"
    AutoEventWireup="true" CodeFile="OrderDetails.aspx.cs" Inherits="Admin_Pages_articles_OrderDetails" %>

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
                var page = $(this).attr("href")
                var cdialog = $('<div id="divEdit1"></div>')
                .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" height="98%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 450,
                    width: 620,
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
            <asp:LinkButton ID="btnSearch" runat="server" Width="1px" Height="1px" Text="" OnClick="btnSearch_Click">
            </asp:LinkButton>
            <table cellpadding="2" cellspacing="0" style="width: 100%">
                <tr>
                    <td style="width: 80px">
                        Đơn hàng:
                    </td>
                    <td>
                        <asp:Label ID="lblPositionName" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
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
                    <asp:GridView ID="m_grid" DataKeyNames="OrderDetailId" runat="server" ShowHeaderWhenEmpty="True"
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
                                    <asp:Label ID="lblGridColumnName" runat="server" Text="Sản Phẩm" meta:resourcekey="lblGridColumnName"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("ProductName")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblQuantity" runat="server" Text="Số Lượng" meta:resourcekey="lblQuantity"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("Quantity")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblPrice" runat="server" Text="Giá" meta:resourcekey="lblPrice"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("Price", "{0:#,###}")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    Tổng giá trị
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# (int.Parse(Eval("Quantity").ToString()) * double.Parse(Eval("Price").ToString())).ToString("N0") %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle />
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnOperation" runat="server" Text="Thao tác" meta:resourcekey="lblGridColumnOperation"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
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
            </div><br /><br />
        <div style="width:auto; height:350px; overflow:auto;">
            <table id="tblEdit" title="a21" runat="server" cellspacing="0" cellpadding="2" width="100%"
                    border="0">
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblOrderValue" runat="server" Text="Giá trị đơn hàng:" meta:resourcekey="lblOrderValue"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="txtOrderValue" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblDeliveryFee" runat="server" Text="Phí giao hàng:" meta:resourcekey="lblDeliveryFee"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="txtDeliveryFee" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblOrderDesc" runat="server" Text="Mô tả:" meta:resourcekey="lblOrderDesc"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="txtOrderDesc" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblOrderCode" runat="server" Text="Mã hóa đơn:" meta:resourcekey="lblOrderCode"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="txtOrderCode" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblPaymentTypes" runat="server" Text="Thanh toán:" meta:resourcekey="lblPaymentTypes"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="txtPaymentType" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblOrderStatus" runat="server" Text="Trạng thái:" meta:resourcekey="lblOrderStatus"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="txtOrderStatus" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblDeliveryStatus" runat="server" Text="Giao hàng:" meta:resourcekey="lblDeliveryStatus"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="txtDeliveryStatus" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblPartners" runat="server" Text="Đối tác:" meta:resourcekey="lblPartners"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="txtPartners" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblCustomerId" runat="server" Text="Id khách hàng:" meta:resourcekey="lblCustomerId"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="txtCustomerId" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblFullName" runat="server" Text="Tên khách hàng:" meta:resourcekey="lblFullName"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="txtFullName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblAddress" runat="server" Text="Địa Chỉ:" meta:resourcekey="lblAddress"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="txtAddress" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            Quận/huyện:
                        </td>
                        <td>
                            <asp:Label ID="txtDistrict" runat="server"></asp:Label>
                            &nbsp;-&nbsp;
                            <asp:Label ID="txtProvince" runat="server"></asp:Label>
                            &nbsp;-&nbsp;
                            <asp:Label ID="txtCountries" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblMobile" runat="server" Text="Điện thoại:" meta:resourcekey="lblMobile"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="txtMobile" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblEmail" runat="server" Text="Email:" meta:resourcekey="lblEmail"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="txtEmail" runat="server"></asp:Label>
                        </td>
                    </tr>
                
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblCouponCode" runat="server" Text="Khuyến mại:" meta:resourcekey="lblCouponCode"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="txtCouponCode" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblCouponValue" runat="server" Text="Số khuyến mại:" meta:resourcekey="lblCouponValue"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="txtCouponValue" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblRequireInvoice" runat="server" Text="Yêu cầu Hóa đơn:" meta:resourcekey="lblRequireInvoice"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="txtRequireInvoice" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblCompanyName" runat="server" Text="Công Ty:" meta:resourcekey="lblCompanyName"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="txtCompanyName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblCompanyAddress" runat="server" Text="Địa Chỉ:" meta:resourcekey="lblCompanyAddress"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="txtCompanyAddress" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblCompanyTaxCode" runat="server" Text="Mã số thuế:" meta:resourcekey="lblCompanyTaxCode"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="txtCompanyTaxCode" runat="server"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblDeliveryNote" runat="server" Text="Ghi chú:" meta:resourcekey="lblDeliveryNote"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="txtDeliveryNote" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblGridColumnIP" runat="server" Text="Từ IP:" meta:resourcekey="lblGridColumnIP"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="txtFromIP" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="td-edit-2">
                            <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
