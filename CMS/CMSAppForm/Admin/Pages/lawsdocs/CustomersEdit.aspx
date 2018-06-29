<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="CustomersEdit.aspx.cs" Inherits="Admin_CustomersEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Import Namespace="ICSoft.CMSLib" %>
<%@ Import Namespace="ICSoft.LawDocsLib" %>
<%@ Import Namespace="ICSoft.HelperLib" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
<script type="text/javascript">
    $(document).ready(function () {
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(InitializeRequest);
        prm.add_endRequest(EndRequest);
        $("#<%= txtDateOfBirth.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        
        
    });
    function InitializeRequest(sender, args) {
    }

    function EndRequest(sender, args) {
        $(function () {
            $("#<%= txtDateOfBirth.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        });
       
    }
    
   
    </script>
    <div style="text-align:center"><asp:Label ID="lblMessage" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label><input type="password" style="visibility:hidden" /></div>
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td style="width:100px">Site:
                </td>
            <td width="300px">
                <asp:DropDownList ID="ddlSite" runat="server" DataTextField="SiteDesc" 
                            DataValueField="SiteId" Width="270px" CssClass="userselect">
                        </asp:DropDownList>
                </td>
            <td width="100px">Map với User CMS:</td>
            <td><asp:DropDownList ID="ddlUser" runat="server" DataTextField="Username" 
                    DataValueField="UserId" Width="270px" CssClass="userselect">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width:100px">
             <asp:Label ID="lblCustomerName" runat="server" Text="Tên truy cập:" 
                    meta:resourcekey="lblCustomerName"></asp:Label>
             </td>
            <td width="300px">               
                <asp:TextBox ID="txtCustomerName" runat="server" Width="260px" CssClass="tukhoatimekiem"></asp:TextBox>                   
                <asp:RequiredFieldValidator ID="rfvCustomerName" ControlToValidate="txtCustomerName" Text="Nhập tên đăng nhập" runat="server" ValidationGroup="G1" ForeColor="Red" >* Nhập tên đăng nhập</asp:RequiredFieldValidator>
            </td>
            <td width="100px">               
                <asp:Label ID="lblAccountNumber" runat="server" Text="Số tài khoản:" 
                    meta:resourcekey="lblAccountNumber"></asp:Label>
                </td>
            <td>               
                <asp:TextBox ID="txtAccountNumber" runat="server" Width="260px" 
                    CssClass="tukhoatimekiem"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblCustomerPass" runat="server" Text="Mật khẩu:" 
                    meta:resourcekey="lblCustomerPass"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtCustomerPass" runat="server" Width="260px" CssClass="tukhoatimekiem" TextMode="Password" ></asp:TextBox>
                <asp:RegularExpressionValidator ID="rfvCustomerPass" runat="server" 
                    ControlToValidate="txtCustomerPass" Display="dynamic"  ForeColor="Red" ValidationGroup="G1"
                    ErrorMessage="Mật khẩu không hợp lệ!" ValidationExpression="\w{6,50}">* Mật khẩu không hợp lệ! </asp:RegularExpressionValidator>
                <br /><asp:CheckBox ID="ckbLockPassword" runat="server" Text="Không tự đổi mật khẩu" />
            </td>
            <td>
                <asp:Label ID="lblOrganName" runat="server" Text="Cơ quan:" 
                    meta:resourcekey="lblOrganName"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtOrganName" runat="server" CssClass="tukhoatimekiem" 
                    Width="260px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblConfirmPassword" runat="server" Text="Gõ lại mật khẩu:" 
                    meta:resourcekey="lblConfirmPassword"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtConfirmPassword" runat="server" Width="260px" TextMode="Password" 
                    CssClass="tukhoatimekiem"></asp:TextBox>
                <asp:CompareValidator ID="cmpPassword" runat="server" ControlToCompare="txtCustomerPass" ForeColor="Red" 
                    ControlToValidate="txtConfirmPassword" 
                    ErrorMessage="Mật khẩu không khớp! Hãy xác nhận lại mật khẩu!" 
                    Operator="Equal" />
                </td>
            <td>
                <asp:Label ID="lblOrganPhone" runat="server" Text="ĐT cơ quan:" 
                    meta:resourcekey="lblOrganPhone"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtOrganPhone" runat="server" CssClass="tukhoatimekiem" 
                    Width="260px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblCustomerFullName" runat="server" Text="Họ tên:" 
                    meta:resourcekey="lblCustomerFullName"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtCustomerFullName" runat="server" Width="260px" 
                    CssClass="tukhoatimekiem"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCustomerFullName" runat="server" ValidationGroup="G1"
                    ControlToValidate="txtCustomerFullName" Text="Nhập họ tên đầy đủ" ForeColor="Red" >*Nhập họ tên đầy đủ</asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblOrganFax" runat="server" Text="Fax cơ quan:" 
                    meta:resourcekey="lblOrganFax"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtOrganFax" runat="server" CssClass="tukhoatimekiem" 
                    Width="260px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblCustomerNickName" runat="server" Text="Biệt danh:" 
                    meta:resourcekey="lblCustomerNickName"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtCustomerNickName" runat="server" Width="260px" 
                    CssClass="tukhoatimekiem"></asp:TextBox>
                </td>
            <td>
                <asp:Label ID="lblOrganAddress" runat="server" Text="Địa chỉ cơ quan:" 
                    meta:resourcekey="lblOrganAddress"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtOrganAddress" runat="server" CssClass="tukhoatimekiem" 
                    Width="260px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblCustomerMail" runat="server" Text="Email:" 
                    meta:resourcekey="lblCustomerMail"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtCustomerMail" runat="server" Width="260px"
                    CssClass="tukhoatimekiem"></asp:TextBox>
                </td>
            <td>
                <asp:Label ID="lblRegisterNewsletter" runat="server" Text="Nhận bản tin tuần:" 
                    meta:resourcekey="lblRegisterNewsletter"></asp:Label>
                </td>
            <td>
                <asp:RadioButtonList ID="rblRegisterNewsletter" runat="server" 
                    RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="1">Có</asp:ListItem>
                    <asp:ListItem Value="0">Không</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>Website:
                </td>
            <td>
                <asp:TextBox ID="txtWebsite" runat="server" Width="260px"
                    CssClass="tukhoatimekiem"></asp:TextBox>
                </td>
            <td>Facebook:
                </td>
            <td>
                <asp:TextBox ID="txtFacebook" runat="server" Width="260px"
                    CssClass="tukhoatimekiem"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblDateOfBirth" runat="server" Text="Ngày sinh:" 
                    meta:resourcekey="lblDateOfBirth"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtDateOfBirth" runat="server" Width="260px"
                    CssClass="tukhoatimekiem"></asp:TextBox>
                </td>
            <td>
                <asp:Label ID="lblMaxConcurrentLogin" runat="server" Text="Số máy truy cập:" 
                    meta:resourcekey="lblMaxConcurrentLogin"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtMaxConcurrentLogin" runat="server" CssClass="tukhoatimekiem" 
                    Width="260px"></asp:TextBox>
               <%-- <asp:RequiredFieldValidator ID="rfvMaxConcurrentLogin" runat="server" 
                    ControlToValidate="txtMaxConcurrentLogin" ForeColor="Red" ValidationGroup="G1"
                    Text="Nhập số máy truy cập">* Nhập số máy truy cập</asp:RequiredFieldValidator>--%>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblGenders" runat="server" Text="Giới tính:" 
                    meta:resourcekey="lblGenders"></asp:Label>
                </td>
            <td>
                <asp:RadioButtonList ID="rblGenders" runat="server" 
                    RepeatDirection="Horizontal" DataTextField="GenderDesc" 
                    DataValueField="GenderId">
                </asp:RadioButtonList>
            </td>
            <td>
                <asp:Label ID="lblCustomerBalance" runat="server" Text="Số dư tài khoản:" 
                    meta:resourcekey="lblCustomerBalance"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtCustomerBalance" runat="server" Width="260px" 
                    CssClass="tukhoatimekiem"></asp:TextBox>
                <%--<asp:RequiredFieldValidator ID="rfvCustomerBalance" runat="server" 
                    ControlToValidate="txtCustomerBalance" ForeColor="Red" Text="Nhập số dư khách hàng" 
                    ValidationGroup="G1">* Nhập số dư khách hàng</asp:RequiredFieldValidator>--%>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblCustomerMobile" runat="server" Text="Điện thoại:" 
                    meta:resourcekey="lblCustomerMobile"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtCustomerMobile" runat="server" Width="260px"
                    CssClass="tukhoatimekiem"></asp:TextBox>
                </td>
            <td>
                <asp:Label ID="lblCustomerDayLeft" runat="server" Text="Số ngày còn lại:" 
                    meta:resourcekey="lblCustomerDayLeft"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtCustomerDayLeft" runat="server" Width="260px" 
                    CssClass="tukhoatimekiem"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblAddress" runat="server" Text="Địa chỉ:" 
                    meta:resourcekey="lblAddress"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtAddress" runat="server" Width="260px"
                    CssClass="tukhoatimekiem"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblCustomerGroups" runat="server" meta:resourcekey="lblCustomerGroups" 
                    Text="Nhóm:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlCustomerGroups" runat="server" 
                    CssClass="userselect" DataTextField="CustomerGroupDesc" DataValueField="CustomerGroupId" Width="270px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblProvinces" runat="server" Text="Tỉnh thành:" 
                    meta:resourcekey="lblProvinces"></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlProvinces" runat="server" DataTextField="ProvinceDesc" Width="270px"
                    DataValueField="ProvinceId">
                </asp:DropDownList>
                </td>
            <td>
                <asp:Label ID="lblStatus" runat="server" meta:resourcekey="lblStatus" 
                    Text="Trạng thái:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlStatus" runat="server" 
                    CssClass="userselect" DataTextField="StatusDesc" DataValueField="StatusId" Width="270px">
                </asp:DropDownList>
            </td>
        </tr>
        </table>
    <div style="text-align:center">
    <p><asp:LinkButton ID="btnBack" runat="server" CssClass="quaylai" 
                    Text="Quay lại" meta:resourcekey="btnBack" 
            onclick="btnBack_Click" >
        </asp:LinkButton>
        <asp:LinkButton ID="btnSave" runat="server" CssClass="luuvaquaylai" 
            Text="Lưu thông tin" meta:resourcekey="btnSave" ValidationGroup="G1"  onclick="btnSave_Click">
        </asp:LinkButton>
        <asp:LinkButton ID="btnSaveAndNew" runat="server" CssClass="luuvathemmoi" 
                    Text="Lưu và thêm mới"  ValidationGroup="G1" meta:resourcekey="btnSaveAndNew" 
            onclick="btnSaveAndNew_Click"  >
        </asp:LinkButton></p>
    </div>
</asp:Content>

