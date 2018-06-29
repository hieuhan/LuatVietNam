<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master"
    AutoEventWireup="true" CodeFile="OrdersEdit.aspx.cs" Inherits="Admin_Pages_articles_OrdersEdit" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" runat="Server">
    <table id="tblEdit" title="a21" runat="server" cellspacing="0" cellpadding="2" width="100%"
                    border="0">
                    <tr>
                        <td style="width:120px; color:Blue;">Site:
                            </td>
                        <td>
                            <asp:DropDownList ID="ddlSite" runat="server" DataTextField="SiteDesc" 
                                        DataValueField="SiteId" Width="98%" CssClass="userselect">
                                    </asp:DropDownList>
                            </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblOrderName" runat="server" Text="Tên:" meta:resourcekey="lblOrderName"></asp:Label>
                            <span class="icon-required"></span>
                            <asp:RequiredFieldValidator ID="rfvOrderName" ValidationGroup="G1" ControlToValidate="txtOrderName" runat="server" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOrderName" runat="server" Width="98%"></asp:TextBox>
                            <br /><asp:RequiredFieldValidator ID="rfvtxtOrderName" runat="server" ErrorMessage="Tên đơn hàng" ForeColor="Red" ControlToValidate="txtOrderName" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblOrderDesc" runat="server" Text="Mô tả:" meta:resourcekey="lblOrderDesc"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOrderDesc" TextMode="MultiLine" runat="server" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblOrderCode" runat="server" Text="Mã hóa đơn:" meta:resourcekey="lblOrderCode"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOrderCode" runat="server" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblPaymentTypes" runat="server" Text="PaymentTypes:" meta:resourcekey="lblPaymentTypes"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPaymentTypes" runat="server" DataTextField="PaymentTypeDesc"
                                DataValueField="PaymentTypeId">
                            </asp:DropDownList>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblOrderStatus" runat="server" Text="OrderStatus:" meta:resourcekey="lblOrderStatus"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlOrderStatus" runat="server" DataTextField="OrderStatusDesc"
                                DataValueField="OrderStatusId">
                            </asp:DropDownList>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblDeliveryStatus" runat="server" Text="DeliveryStatus:" meta:resourcekey="lblDeliveryStatus"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDeliveryStatus" runat="server" DataTextField="DeliveryStatusDesc"
                                DataValueField="DeliveryStatusId">
                            </asp:DropDownList>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblPartners" runat="server" Text="Partners:" meta:resourcekey="lblPartners"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPartners" runat="server" DataTextField="PartnerDesc" DataValueField="PartnerId">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblCustomerId" runat="server" Text="Số mã khách hàng:" meta:resourcekey="lblCustomerId"></asp:Label>
                            <asp:RegularExpressionValidator ID="revtxtCustomerId" runat="server" ControlToValidate="txtCustomerId" ValidationExpression="^\d*\.?\d*$" ValidationGroup="G1" Text="(*)" ErrorMessage="Số mã khách hàng: vui lòng giá trị số." ForeColor="Red"></asp:RegularExpressionValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCustomerId" runat="server" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblFullName" runat="server" Text="Tên khách hàng:" meta:resourcekey="lblFullName"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFullName" runat="server" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblAddress" runat="server" Text="Địa Chỉ:" meta:resourcekey="lblAddress"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddress" runat="server" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            Quê quán:
                        </td>
                        <td>
                            
                            <asp:DropDownList ID="ddlCountries" Width="150px" runat="server" DataTextField="CountryDesc" DataValueField="CountryId" OnSelectedIndexChanged="ddlCountries_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>&nbsp;-&nbsp;
                            <asp:DropDownList ID="ddlProvinces" Width="150px" runat="server" DataTextField="ProvinceDesc" DataValueField="ProvinceId" OnSelectedIndexChanged="ddlProvinces_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>&nbsp;-&nbsp;
                            <asp:DropDownList ID="ddlDistricts" Width="150px" runat="server" DataTextField="DistrictDesc" DataValueField="DistrictId" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblMobile" runat="server" Text="Điện thoại:" meta:resourcekey="lblMobile"></asp:Label>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Text="(*)" ErrorMessage="Số điện thoại không đúng.(Số ĐT hợp lệ dạng: 84912345678 hoặc 0123456789)" ValidationGroup="G1" ControlToValidate="txtMobile" ValidationExpression="^(\\84|0)[0-9]{9,10}$" ForeColor="Red"></asp:RegularExpressionValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMobile" runat="server" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblEmail" runat="server" Text="Email:" meta:resourcekey="lblEmail"></asp:Label>
                            <asp:RegularExpressionValidator ID="revtxtEmail" Text="(*)" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" runat="server" ValidationGroup="G1" ErrorMessage="Email: không đúng định dạng." ForeColor="Red"></asp:RegularExpressionValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmail" runat="server" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            Giá trị đơn hàng:
                            <asp:RegularExpressionValidator ID="revtxtOrderValue" runat="server" ControlToValidate="txtOrderValue" ValidationExpression="^\d*\.?\d*$" ValidationGroup="G1" ErrorMessage="Số Giá Trị Gia Tăng: vui lòng giá trị số." Text="(*)" ForeColor="Red"></asp:RegularExpressionValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOrderValue" runat="server" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblCouponCode" runat="server" Text="Khuyến mại:" meta:resourcekey="lblCouponCode"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCouponCode" runat="server" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblCouponValue" runat="server" Text="Số khuyến mại:" meta:resourcekey="lblCouponValue"></asp:Label>
                            <asp:RegularExpressionValidator ID="revtxtCouponValue" runat="server" ControlToValidate="txtCouponValue" ValidationExpression="^\d*\.?\d*$" ValidationGroup="G1" Text="(*)" ErrorMessage="Số khuyến mại: vui lòng giá trị số." ForeColor="Red"></asp:RegularExpressionValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCouponValue" runat="server" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            Yêu cầu hóa đơn:
                            <asp:RegularExpressionValidator ID="revtxtRequireInvoice" runat="server" ControlToValidate="txtRequireInvoice" ValidationExpression="^\d*\.?\d*$" ValidationGroup="G1" ErrorMessage="Số Hóa đơn: vui lòng giá trị số." Text="(*)" ForeColor="Red"></asp:RegularExpressionValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRequireInvoice" runat="server" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblCompanyName" runat="server" Text="Công Ty:" meta:resourcekey="lblCompanyName"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCompanyName" runat="server" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblCompanyAddress" runat="server" Text="Địa Chỉ:" meta:resourcekey="lblCompanyAddress"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCompanyAddress" runat="server" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblCompanyTaxCode" runat="server" Text="Mã số thuế:" meta:resourcekey="lblCompanyTaxCode"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCompanyTaxCode" runat="server" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblDeliveryFee" runat="server" Text="Phí giao hàng:" meta:resourcekey="lblDeliveryFee"></asp:Label>
                            <asp:RegularExpressionValidator ID="revtxtDeliveryFee" runat="server" ControlToValidate="txtDeliveryFee" ValidationExpression="^\d*\.?\d*$" ValidationGroup="G1" ErrorMessage="Phí giao hàng: vui lòng giá trị số." Text="(*)" ForeColor="Red"></asp:RegularExpressionValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDeliveryFee" runat="server" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblDeliveryNote" runat="server" Text="Ghi chú:" meta:resourcekey="lblDeliveryNote"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDeliveryNote" runat="server" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; color:Blue;">
                            <asp:Label ID="lblGridColumnIP" runat="server" Text="Từ IP:" meta:resourcekey="lblGridColumnIP"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFromIP" runat="server" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="td-edit-2">
                            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-edit-2" colspan="2">
                            <br />
                            <div style="text-align: center">
                                <asp:CheckBox ID="cblAddOther" runat="server" Text="Thêm tiếp dữ liệu khác" Checked="false" ToolTip="Khi tick chọn sẽ xóa form hiện tại để thêm dữ liệu khác"  />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                                <asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom" Text="Lưu thông tin" ValidationGroup="G1"
                                    meta:resourcekey="btnSave" OnClick="btnSave_Click">
                                </asp:LinkButton>
                            </div>
                        </td>
                    </tr>
                </table>
</asp:Content>
