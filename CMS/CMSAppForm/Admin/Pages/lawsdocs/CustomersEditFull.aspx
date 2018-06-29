<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="CustomersEditFull.aspx.cs" Inherits="Admin_CustomersEditFull" %>
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
        chkFields
        
    });
    function ValidateCheckBoxList(sender, args) {
        var checkBoxList = document.getElementById("<%=chkFields.ClientID %>");
        var checkboxes = checkBoxList.getElementsByTagName("input");
        var isValid = false;
        for (var i = 0; i < checkboxes.length; i++) {
            if (checkboxes[i].checked) {
                isValid = true;
                break;
            }
        }
        args.IsValid = isValid;
    }
    function InitializeRequest(sender, args) {

    }
    </script>
    <div style="text-align:center"><asp:Label ID="lblMessage" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label><input type="password" style="visibility:hidden" /></div>
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr runat="server" visible="false">
            <td style="width:120px">Site:
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
            <td style="width:120px">
             <asp:Label ID="lblCustomerName" runat="server" Text="Tên truy cập:" 
                    meta:resourcekey="lblCustomerName"></asp:Label>
                <span class="icon-required"></span>
             </td>
            <td width="300px">               
                <asp:TextBox ID="txtCustomerName" runat="server" Width="170px" CssClass="tukhoatimekiem"></asp:TextBox> 
                &nbsp;<asp:LinkButton ID="btnSearch" runat="server" CssClass="timkiembutom"  onclick="btnSearch_Click" Text="Kiểm tra"></asp:LinkButton>  
                <br />    
                <asp:RequiredFieldValidator ID="rfvCustomerName" ControlToValidate="txtCustomerName" Text="Nhập tên đăng nhập" runat="server" 
                    ValidationGroup="G1" ForeColor="Red" ErrorMessage="Bạn chưa nhập tên đăng nhập" Display="Dynamic" >Bạn chưa nhập tên đăng nhập</asp:RequiredFieldValidator>
                
            </td>
            <td width="100px">               
                <asp:Label ID="lblAccountNumber" runat="server" Text="Số tài khoản:" 
                    meta:resourcekey="lblAccountNumber"></asp:Label>
                </td>
            <td>               
                <asp:TextBox ID="txtAccountNumber" runat="server" Width="260px" 
                    CssClass="tukhoatimekiem"></asp:TextBox>
                </td>
            <td rowspan = "14">
                
    <div style="width:100%; height:auto; overflow:auto; padding-left:15px;">
        <b style="color:red; font-size:18px; margin:10px;"> Lĩnh vực quan tâm</b>
        <asp:CheckBoxList ID="chkFields" DataTextField="FieldDesc" DataValueField="FieldId" RepeatLayout="table" RepeatColumns="2" RepeatDirection="Vertical" runat="server">
        </asp:CheckBoxList>
        <asp:CustomValidator ID="CustomValidator1" ErrorMessage="Bạn phải chọn ít nhất một lĩnh vực."
    ForeColor="Red" ClientValidationFunction="ValidateCheckBoxList" runat="server" />
    </div>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblCustomerPass" runat="server" Text="Mật khẩu:" 
                    meta:resourcekey="lblCustomerPass"></asp:Label>
                <span class="icon-required"></span>
                </td>
            <td style="margin-top: 10px;">
                <asp:TextBox ID="txtCustomerPass" runat="server" Width="260px" CssClass="tukhoatimekiem" TextMode="Password" ></asp:TextBox>
                <br />
                <asp:RegularExpressionValidator ID="rfvCustomerPass" runat="server" 
                    ControlToValidate="txtCustomerPass" Display="dynamic"  ForeColor="Red" ValidationGroup="G1"
                    ErrorMessage="Mật khẩu không hợp lệ!" 
                    ValidationExpression="\w{6,50}">
                    Mật khẩu từ 6 đến 50 ký tự</asp:RegularExpressionValidator>
                <br />
                <asp:RequiredFieldValidator ID="rfvtxtCustomerPass" runat="server" ValidationGroup="G1"
                    ControlToValidate="txtCustomerPass" Text="Bạn chưa nhập mật khẩu" ErrorMessage="Bạn chưa nhập mật khẩu"
                    ForeColor="Red" ></asp:RequiredFieldValidator>
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
                <span class="icon-required"></span>
                </td>
            <td>
                <br />
                <asp:TextBox ID="txtConfirmPassword" runat="server" Width="260px" TextMode="Password" 
                    CssClass="tukhoatimekiem"></asp:TextBox>
                <br />
                <asp:CompareValidator ID="cmpPassword" runat="server" ControlToCompare="txtCustomerPass" ForeColor="Red" 
                    ControlToValidate="txtConfirmPassword" 
                    ErrorMessage="Mật khẩu không khớp! Hãy xác nhận lại mật khẩu!" 
                    Operator="Equal" />
                <br />
                <asp:RequiredFieldValidator ID="rfvtxtConfirmPassword" runat="server" ValidationGroup="G1"
                    ControlToValidate="txtConfirmPassword" Text="Bạn chưa nhập mật khẩu" ErrorMessage="Bạn chưa nhập mật khẩu"
                    ForeColor="Red" ></asp:RequiredFieldValidator>
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
                <span class="icon-required"></span>
                </td>
            <td>
                <asp:TextBox ID="txtCustomerFullName" runat="server" Width="260px" 
                    CssClass="tukhoatimekiem"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="rfvCustomerFullName" runat="server" ValidationGroup="G1"
                    ControlToValidate="txtCustomerFullName" Text="Bạn chưa nhập họ tên" ErrorMessage="Bạn chưa nhập họ tên"
                    ForeColor="Red" ></asp:RequiredFieldValidator>
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
                <span class="icon-required"></span>
                </td>
            <td>
                <asp:TextBox ID="txtCustomerMail" runat="server" Width="260px"
                    CssClass="tukhoatimekiem"></asp:TextBox>
                <br />
                <asp:RegularExpressionValidator ID="rfvtxtCustomerMail" runat="server" 
                    ControlToValidate="txtCustomerMail" Display="dynamic"  ForeColor="Red" ValidationGroup="G1"
                    ErrorMessage="Mật khẩu không hợp lệ!" 
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                    Email không đúng định dạng</asp:RegularExpressionValidator>
                <br />
                <asp:RequiredFieldValidator ID="rfvtxtCustomerMail1" runat="server" ValidationGroup="G1"
                    ControlToValidate="txtCustomerMail" Text="Bạn chưa nhập Email" ErrorMessage="Bạn chưa nhập Email"
                    ForeColor="Red" ></asp:RequiredFieldValidator>
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
                <a class="help-lnk" href="javascript:void(0)" title="Chọn hoặc nhập ngày tháng theo định dang dd/MM/yyyy" >
                    <span class="aui-icon aui-icon-small aui-iconfont-help">?</span></a>
                <br />(dd/MM/yyyy)
                </td>
            <td>
                <asp:Label ID="lblMaxConcurrentLogin" runat="server" Text="Số máy truy cập:" 
                    meta:resourcekey="lblMaxConcurrentLogin"></asp:Label>
                </td>
            
            <td>
                <asp:DropDownList runat="server" ID ="ddlMaxConcurrentLogin" Width="270px" CssClass="userselect" Enabled="false">
                    <asp:ListItem Value="1" Text="1 người dùng tại một thời điểm"></asp:ListItem>
                    <asp:ListItem Value="3" Text="3 người dùng tại một thời điểm"></asp:ListItem>
                    <asp:ListItem Value="5" Text="5 người dùng tại một thời điểm"></asp:ListItem>
                    <asp:ListItem Value="10" Text="10 người dùng tại một thời điểm"></asp:ListItem>
                    <asp:ListItem Value="20" Text="20 người dùng tại một thời điểm"></asp:ListItem>
                    <asp:ListItem Value="50" Text="50 người dùng tại một thời điểm"></asp:ListItem>
                </asp:DropDownList>
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
                    ValidationGroup="G1">*</asp:RequiredFieldValidator>--%>
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
        <tr>
            <td><asp:Label ID="lblOccupation" runat="server" Text="Nghề Nghiệp:" 
                    meta:resourcekey="lblOccupation"></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlOccupation" runat="server" DataTextField="OccupationDesc" Width="270px"
                    DataValueField="OccupationId">
                </asp:DropDownList>
                </td>
            <td>
                <asp:Label ID="lblOrganOccupation" runat="server" meta:resourcekey="lblOrganOccupation" 
                    Text="Lĩnh vực hoạt động công ty"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlOrganOccupation" runat="server" 
                    CssClass="userselect" DataTextField="OrganOccupationDesc" DataValueField="OrganOccupationId" Width="270px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td>
                <asp:Label ID="lblOrganTaxCode" runat="server" Text="Mã số thuế:" 
                    meta:resourcekey="lblOrganTaxCode"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtOrganTaxCode" runat="server" CssClass="tukhoatimekiem" 
                    Width="260px"></asp:TextBox>
            </td>
        </tr>
        </table>
    
    <div style="text-align:center">
    <p><asp:LinkButton ID="btnBack" runat="server" CssClass="quaylai" 
                    Text="Quay lại" meta:resourcekey="btnBack" 
            onclick="btnBack_Click" >
        </asp:LinkButton>
        <asp:LinkButton ID="btnSave" runat="server" CssClass="luuvaquaylai" ToolTip="Lưu và tiếp tục sửa thông tin người dùng này." 
            Text="Lưu thông tin" meta:resourcekey="btnSave" ValidationGroup="G1"  onclick="btnSave_Click">
        </asp:LinkButton>
        <asp:LinkButton ID="btnSaveAndNew" runat="server" CssClass="luuvathemmoi" ToolTip="Lưu và tiếp tục thêm mới người dùng khác." 
                    Text="Lưu và thêm mới"  ValidationGroup="G1" meta:resourcekey="btnSaveAndNew" 
            onclick="btnSaveAndNew_Click"  >
        </asp:LinkButton></p>
    </div>
</asp:Content>

