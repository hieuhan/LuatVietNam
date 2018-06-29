<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true"
    CodeFile="Orders.aspx.cs" Inherits="Admin_Pages_articles_Orders" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/admin/UserControls/CustomPaging.ascx" TagName="CustomPaging"
    TagPrefix="uc1" %>
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
            $('a.popup').live('click', function (e) {
                var page = $(this).attr("href")
                var cdialog = $('<div id="divEdit"></div>')
                .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="98%" height="98%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 600,
                    width: 700,
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
            $('a.popup1').live('click', function (e) {
                var page = $(this).attr("href")
                var cdialog = $('<div id="divEdit"></div>')
                .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="98%" height="98%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 600,
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
                    <td>
                        <asp:DropDownList ID="ddlProvinces" runat="server" AutoPostBack="True" CssClass="userselect"
                            DataTextField="ProvinceDesc" DataValueField="ProvinceId" OnSelectedIndexChanged="ddlProvinces_SelectedIndexChanged"
                            Width="250px">
                        </asp:DropDownList>
                        &nbsp;&nbsp;Site:
                        <asp:DropDownList ID="ddlSite" runat="server" DataTextField="SiteDesc" 
                            DataValueField="SiteId" Width="250px" CssClass="userselect"
                            AutoPostBack="True" onselectedindexchanged="ddlSite_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 90px; white-space: nowrap;">
                        <asp:Label ID="lblPartners" runat="server" meta:resourcekey="lblPartners" Text="Partners :"></asp:Label>
                    </td>
                    <td style="width: 260px">
                        <asp:DropDownList ID="ddlPartners" runat="server" AutoPostBack="True" CssClass="userselect"
                            DataTextField="PartnerDesc" DataValueField="PartnerId" OnSelectedIndexChanged="ddlPartners_SelectedIndexChanged"
                            Width="250px">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 90px; white-space: nowrap;">
                        <asp:Label ID="lblDistricts" runat="server" meta:resourcekey="lblDistricts" Text="Districts :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDistricts" runat="server" AutoPostBack="True" CssClass="userselect"
                            DataTextField="DistrictDesc" DataValueField="DistrictId" OnSelectedIndexChanged="ddlDistricts_SelectedIndexChanged"
                            Width="250px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px; white-space: nowrap;">
                        <asp:Label ID="lblPaymentTypes" runat="server" meta:resourcekey="lblPaymentTypes"
                            Text="PaymentTypes :"></asp:Label>
                    </td>
                    <td style="width: 260px">
                        <asp:DropDownList ID="ddlPaymentTypes" runat="server" AutoPostBack="True" CssClass="userselect"
                            DataTextField="PaymentTypeDesc" DataValueField="PaymentTypeId" OnSelectedIndexChanged="dllPaymentTypes_SelectedIndexChanged"
                            Width="250px">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 90px; white-space: nowrap;">
                        <asp:Label ID="lblOrderStatus" runat="server" meta:resourcekey="lblOrderStatus" Text="OrderStatus :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlOrderStatus" runat="server" AutoPostBack="True" CssClass="userselect"
                            DataTextField="OrderStatusDesc" DataValueField="OrderStatusId" OnSelectedIndexChanged="ddlOrderStatus_SelectedIndexChanged"
                            Width="250px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px; white-space: nowrap;">
                        <asp:Label ID="lblDeliveryStatus" runat="server" meta:resourcekey="lblDeliveryStatus"
                            Text="DeliveryStatus :"></asp:Label>
                    </td>
                    <td style="width: 260px">
                        <asp:DropDownList ID="ddlDeliveryStatus" runat="server" AutoPostBack="True" CssClass="userselect"
                            DataTextField="DeliveryStatusDesc" DataValueField="DeliveryStatusId" OnSelectedIndexChanged="ddlDeliveryStatus_SelectedIndexChanged"
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
                        <asp:Label ID="lblDateFrom" runat="server" meta:resourcekey="lblDateFrom" Text="Từ ngày:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDateFrom" runat="server" CssClass="tukhoatimekiem" Width="100px"></asp:TextBox>
                        <asp:Label ID="lblDateTo" runat="server" meta:resourcekey="lblDateTo" Text="Đến:"></asp:Label>
                        <asp:TextBox ID="txtDateTo" runat="server" CssClass="tukhoatimekiem" Width="100px"></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" CssClass="timkiembutom" meta:resourcekey="btnSearch"
                            OnClick="btnSearch_Click" Text="Tìm kiếm">
                        </asp:Button>
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
                    <%-- <%=GetLocalResourceObject("Orders").ToString()%>--%>
                </div>
                <div class="chucnangright">
                    <a id="popup" href="OrdersEdit.aspx?LanguageId=<%=LanguageId %>&AppTypeId=<%=AppTypeId %>"
                        title="<%=GetLocalResourceObject("lnkAddNew.title").ToString() %>" class="themmoi popup">
                        <asp:Label ID="lblAddNew" runat="server" Text="Thêm mới" meta:resourcekey="lblAddNew"></asp:Label>
                    </a>
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
                    <asp:GridView ID="m_grid" DataKeyNames="OrderId" runat="server" ShowHeaderWhenEmpty="True"
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
                                        ToolTip='<%# Eval("OrderId").ToString() %>'> </asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="3%" />
                                <HeaderStyle Width="3%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumNameOrder" runat="server" Text="Thông Tin" meta:resourcekey="lblGridColumNameOrder"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <span style="color: Blue;">
                                        <asp:Label ID="lblGridCo" runat="server" Text="" meta:resourcekey="lblGridCo"></asp:Label></span>
                                    <span style="font-weight: bold;">
                                    <asp:HyperLink ID="hplAdvertList" ToolTip="Chi tiết" runat="server" NavigateUrl='<%# "OrderDetails.aspx?OrderId=" + Eval("OrderId").ToString() %>'
                                        CssClass="dieuhuong123 popup1"><%# Eval("OrderName")%></asp:HyperLink>
                                    </span>
                                    <asp:Label ID="lblOrderName" runat="server" Text='<%# Eval("OrderName")%>' Visible="false"></asp:Label>
                                    <br />
                                    <%# Eval("OrderDesc")%><br />
                                    <br />
                                    <span style="color: Blue;">
                                        <asp:Label ID="lblOrderCode" runat="server" Text="Mã Hóa Đơn:  " meta:resourcekey="lblOrderCode"></asp:Label></span>
                                    <%# Eval("OrderCode")%><br />
                                    <span style="color: Blue;">
                                        <asp:Label ID="lblCustomerId" runat="server" Text="Id Khách Hàng:  " meta:resourcekey="lblCustomerId"></asp:Label></span>
                                    <%# Eval("CustomerId")%><br />
                                    <span style="color: Blue;">
                                        <asp:Label ID="lblFullName" runat="server" Text="Khách Hàng:  " meta:resourcekey="lblFullName"></asp:Label></span>
                                    <%# Eval("FullName")%><br />
                                    <span style="color: Blue;">
                                        <asp:Label ID="lblAddress" runat="server" Text="Địa chỉ:  " meta:resourcekey="lblAddress"></asp:Label></span>
                                    <%# Eval("Address")%><br />
                                    <span style="color: Blue;">
                                        <asp:Label ID="lblMobile" runat="server" Text="Điện Thoại:  " meta:resourcekey="lblMobile"></asp:Label></span>
                                    <%# Eval("Mobile")%><br />
                                    <span style="color: Blue;">
                                        <asp:Label ID="lblEmail" runat="server" Text="Email:  " meta:resourcekey="lblEmail"></asp:Label></span>
                                    <%# Eval("Email")%><br />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnInfoplus" runat="server" Text="Đơn Hàng" meta:resourcekey="lblGridColumnInfoplus"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <span style="color: Blue;">
                                        <asp:Label ID="lblOrderValue" runat="server" Text="Giá trị đơn hàng:  " meta:resourcekey="lblOrderValue"></asp:Label></span>
                                    <b><%# Eval("OrderValue", "{0: #,###}") %></b><br />
                                    <span style="color: Blue;">
                                        <asp:Label ID="lblDeliveryFee" runat="server" Text="Phí giao hàng:  " meta:resourcekey="lblDeliveryFee"></asp:Label></span>
                                    <%# Eval("DeliveryFee", "{0: #,###}")%><br />
                                    <span style="color: Blue;">
                                        <asp:Label ID="lblCouponCode" runat="server" Text="Mã khuyến mại:  " meta:resourcekey="lblCouponCode"></asp:Label></span>
                                    <%# Eval("CouponCode")%><br />
                                    <span style="color: Blue;">
                                        <asp:Label ID="lblCouponValue" runat="server" Text="Giá trị khuyến mại:  " meta:resourcekey="lblCouponValue"></asp:Label></span>
                                    <%# Eval("CouponValue", "{0: #,###}")%><br />
                                    <span style="color: Blue;">
                                        <asp:Label ID="lblRequireInvoice" runat="server" Text="Hóa đơn yêu cầu:  " meta:resourcekey="lblRequireInvoice"></asp:Label></span>
                                    <%# Eval("RequireInvoice").ToString() == "1" ? "Có" : "Không" %><br />
                                    <span style="color: Blue;">
                                        <asp:Label ID="lblCompanyName" runat="server" Text="Công ty:  " meta:resourcekey="lblCompanyName"></asp:Label></span>
                                    <%# Eval("CompanyName")%><br />
                                    <span style="color: Blue;">
                                        <asp:Label ID="lblCompanyAddress" runat="server" Text="Địa Chỉ:  " meta:resourcekey="lblCompanyAddress"></asp:Label></span>
                                    <%# Eval("CompanyAddress")%><br />
                                    <span style="color: Blue;">
                                        <asp:Label ID="lblCompanyTaxCode" runat="server" Text="Mã số thuế:  " meta:resourcekey="lblCompanyTaxCode"></asp:Label></span>
                                    <%# Eval("CompanyTaxCode")%><br />
                                    <span style="color: Blue;">
                                        <asp:Label ID="lblDeliveryNote" runat="server" Text="Ghi chú:  " meta:resourcekey="lblDeliveryNote"></asp:Label></span>
                                    <%# Eval("DeliveryNote")%><br />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnStatus" runat="server" Text="Trạng thái"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <span style="color: Blue;">Đơn hàng: </span><b><%# OrderStatus.Static_Get(byte.Parse(Eval("OrderStatusId").ToString()), l_OrderStatus).OrderStatusDesc %></b><br />
                                    <span style="color: Blue;">Giao hàng: </span><%# DeliveryStatus.Static_Get(byte.Parse(Eval("DeliveryStatusId").ToString()), l_DeliveryStatus).DeliveryStatusDesc %><br />
                                    <span style="color: Blue;">Thanh toán: </span><%# PaymentTypes.Static_Get(byte.Parse(Eval("PaymentTypeId").ToString()), l_PaymentTypes).PaymentTypeDesc %><br /><br />
                                    <span style="color: Blue;">IP: </span> <%# Eval("FromIP").ToString()%><br />
                                    <span style="color: Blue;">Time:</span> <%# DateTimeHelpers.GetDateAndTime(Eval("CrDateTime"))%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                <HeaderStyle />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnOperation" runat="server" Text="Thao tác" meta:resourcekey="lblGridColumnOperation"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <a href='OrdersEdit.aspx?OrderId=<%# Eval("OrderId") %>'
                                        class="iconadmsua popup" title="<%# GetLocalResourceObject("lnkGridColumnEdit.title").ToString() %>"
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
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
