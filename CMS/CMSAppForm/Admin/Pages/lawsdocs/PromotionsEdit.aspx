<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="PromotionsEdit.aspx.cs" Inherits="Admin_PromotionsEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
<script type="text/javascript">
    $(document).ready(function () {
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(InitializeRequest);
        prm.add_endRequest(EndRequest);
        $("#<%= txtBeginTime.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy 00:00:00' });
        $("#<%= txtEndTime.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy 23:59:59' });

    });
    function InitializeRequest(sender, args) {
    }

    function EndRequest(sender, args) {
        SetStartup();
        $("#<%= txtBeginTime.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy 00:00:00' });
        $("#<%= txtEndTime.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy 23:59:59' });
    }
  </script>
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter;margin-bottom: 30px;">
        <tr>
            <td style="width:120px">Site:
                </td>
            <td>
                <asp:DropDownList ID="ddlSite" runat="server" DataTextField="SiteDesc" 
                            DataValueField="SiteId" Width="310px" CssClass="userselect"
                            AutoPostBack="True" onselectedindexchanged="ddlSite_SelectedIndexChanged">
                        </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td width="120px" ><asp:Label ID="lblServicePackages" runat="server" 
                    Text="Gói dịch vụ:" meta:resourcekey="lblServicePackages"></asp:Label><span class="icon-required"></span>
                </td>
            <td>
                <asp:DropDownList ID="ddlServicePackages" runat="server" 
                    CssClass="userselect" DataTextField="ServicePackageName" 
                    DataValueField="ServicePackageId" Width="310px">
                </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblPromotionName" runat="server" Text="Tên:" meta:resourcekey="lblPromotionName"></asp:Label><span class="icon-required"></span>
            
                </td>
            <td>
                <asp:TextBox ID="txtPromotionName" runat="server" CssClass="tukhoatimekiem" 
                    Width="300px" TextMode="MultiLine"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="rfvPromotionName" runat="server" Display="Dynamic" 
                ErrorMessage="Bạn cần nhập vào tên" ValidationGroup="G1" 
                ControlToValidate="txtPromotionName" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td >
                <asp:Label ID="lblPromotionDesc" runat="server" Text="Mô tả:" 
                    meta:resourcekey="lblPromotionDesc"></asp:Label><span class="icon-required"></span>
                    
                </td>
            <td>
                <asp:TextBox ID="txtPromotionDesc" runat="server" CssClass="tukhoatimekiem"
                    Width="300px" TextMode="MultiLine"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="rfvPromotionDesc" runat="server" Display="Dynamic" 
                ErrorMessage="Bạn cần nhập vào mô tả" ValidationGroup="G1" 
                ControlToValidate="txtPromotionDesc" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td >
                <asp:Label ID="lblBeginTime" runat="server" Text="Thời gian bắt đầu:" 
                    meta:resourcekey="lblBeginTime"></asp:Label><span class="icon-required"></span>
                    
                </td>
            <td>
                <asp:TextBox ID="txtBeginTime" runat="server" CssClass="tukhoatimekiem" 
                    Width="300px"></asp:TextBox>
                <a class="help-lnk" href="javascript:void(0)" title="Chọn hoặc nhập ngày tháng theo định dạng dd/MM/yyyy" >
                <span class="aui-icon aui-icon-small aui-iconfont-help">?</span></a><br />
                <asp:RequiredFieldValidator ID="rfvBeginTime" runat="server" Display="Dynamic" 
                ErrorMessage="Bạn cần nhập vào thời gian bắt đầu" ValidationGroup="G1" 
                ControlToValidate="txtBeginTime" ForeColor="Red"></asp:RequiredFieldValidator><br />               
                <asp:RegularExpressionValidator ID="rgltxtDisplayStartTime" runat="server" ForeColor="Red" ErrorMessage="Thời gian bắt đầu bạn nhập không hợp lệ" 
                    Display="Dynamic" ValidationExpression="\d{2}\/\d{2}\/\d{4}\s(\d{2}\:){2}\d{2}" ControlToValidate="txtBeginTime"></asp:RegularExpressionValidator><br />                          
                
                </td>
        </tr>
        <tr>
            <td >
                <asp:Label ID="lblEndTime" runat="server" Text="Thời gian kết thúc:" 
                    meta:resourcekey="lblEndTime"></asp:Label><span class="icon-required"></span>
                </td>
            <td>
                <asp:TextBox ID="txtEndTime" runat="server" CssClass="tukhoatimekiem" 
                    Width="300px"></asp:TextBox>
                <a class="help-lnk" href="javascript:void(0)" title="Chọn hoặc nhập ngày tháng theo định dạng dd/MM/yyyy" >
                <span class="aui-icon aui-icon-small aui-iconfont-help">?</span></a><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" 
                ErrorMessage="Bạn cần nhập vào thời gian kết thúc" ValidationGroup="G1" 
                ControlToValidate="txtEndTime" ForeColor="Red"></asp:RequiredFieldValidator><br />               
                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ForeColor="Red" ErrorMessage="Thời gian bắt đầu bạn nhập không hợp lệ" 
                    Display="Dynamic" ValidationExpression="\d{2}\/\d{2}\/\d{4}\s(\d{2}\:){2}\d{2}" ControlToValidate="txtEndTime"></asp:RegularExpressionValidator><br />                          
                
                
                </td>
        </tr>
        <tr>
            <td >
                <asp:Label ID="lblNumMonthFree" runat="server" Text="Số tháng miễn phí:" 
                    meta:resourcekey="lblNumMonthFree"></asp:Label><span class="icon-required"></span>
                    
                </td>
            <td>
                <asp:TextBox ID="txtNumMonthFree" runat="server" CssClass="tukhoatimekiem" 
                    Width="300px" Text="0"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="rfvNumMonthFree" runat="server" Display="Dynamic" 
                ErrorMessage="Bạn cần nhập vào số tháng miễn phí" ValidationGroup="G1" 
                ControlToValidate="txtNumMonthFree" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="rgltxtNumMonthFree" runat="server" ForeColor="Red" ErrorMessage="Chỉ chấp nhận kiểu số" 
                    Display="Dynamic" ValidationExpression="\d+" ControlToValidate="txtNumMonthFree"></asp:RegularExpressionValidator>
                </td>
        </tr>
        <tr>
            <td >
                <asp:Label ID="lblNumDayFree" runat="server" Text="Số ngày miễn phí:" 
                    meta:resourcekey="lblNumDayFree"></asp:Label><span class="icon-required"></span>
                </td>
            <td>
                <asp:TextBox ID="txtNumDayFree" runat="server" CssClass="tukhoatimekiem" 
                    Width="300px" Text="0"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" 
                ErrorMessage="Bạn cần nhập vào số ngày miễn phí" ValidationGroup="G1" 
                ControlToValidate="txtNumDayFree" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ForeColor="Red" ErrorMessage="Chỉ chấp nhận kiểu số" 
                    Display="Dynamic" ValidationExpression="\d+" ControlToValidate="txtNumDayFree"></asp:RegularExpressionValidator>
                </td>
        </tr>
        <tr>
            <td >
                <asp:Label ID="lblAmount" runat="server" Text="Số tiền:" 
                    meta:resourcekey="lblAmount"></asp:Label><span class="icon-required"></span>
                </td>
            <td>
                <asp:TextBox ID="txtAmount" runat="server" CssClass="tukhoatimekiem" 
                    Width="300px" Text="0"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" 
                ErrorMessage="Bạn cần nhập vào số tiền miễn phí" ValidationGroup="G1" 
                ControlToValidate="txtAmount" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ForeColor="Red" ErrorMessage="Chỉ chấp nhận kiểu số" 
                    Display="Dynamic" ValidationExpression="\d+" ControlToValidate="txtAmount"></asp:RegularExpressionValidator>
                </td>
        </tr>
        <tr>
            <td >
                <asp:Label ID="lblPercentDecrease" runat="server" Text="Phần trăm giảm giá:" 
                    meta:resourcekey="lblPercentDecrease"></asp:Label><span class="icon-required"></span>
                </td>
            <td>
                <asp:TextBox ID="txtPercentDecrease" runat="server" CssClass="tukhoatimekiem" 
                    Width="300px" Text="0"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" 
                ErrorMessage="Bạn cần nhập vào % giảm giá" ValidationGroup="G1" 
                ControlToValidate="txtPercentDecrease" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ForeColor="Red" ErrorMessage="Chỉ chấp nhận kiểu số" 
                    Display="Dynamic" ValidationExpression="\d+" ControlToValidate="txtPercentDecrease"></asp:RegularExpressionValidator>
                </td>
        </tr>
        <tr>
            <td >
                <asp:Label ID="lblUsingTypeId" runat="server" Text="Hình thức sử dụng:" 
                    meta:resourcekey="lblUsingTypeId"></asp:Label>
                </td>
            <td>
                <asp:DropDownList runat="server" ID="ddlUsingTypeId" CssClass="userselect" Width="310px" >
                    <asp:ListItem Value="1" Text="Một lần" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="2" Text="Nhiều lần"></asp:ListItem>
                </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td >
                <asp:Label ID="lblUsingCount" runat="server" Text="Số lần sử dụng:" 
                    meta:resourcekey="lblUsingCount"></asp:Label><span class="icon-required"></span>
                <br />
                <asp:RequiredFieldValidator ID="rfvUsingCount" runat="server" Display="Dynamic" 
                ErrorMessage="(*)" ValidationGroup="G1" 
                ControlToValidate="txtUsingCount" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="rfvUsingCountNumber" runat="server" ControlToValidate="txtUsingCount"
    ErrorMessage="Vui lòng nhập số lần sử dụng" ForeColor="Red" ValidationGroup="G1" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                </td>
            <td>
                <asp:TextBox ID="txtUsingCount" runat="server" CssClass="tukhoatimekiem" 
                    Width="300px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td >
                <asp:Label ID="Label1" runat="server" Text="Số lượng mã:" 
                    meta:resourcekey="lblUsingCount"></asp:Label><span class="icon-required"></span>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" 
                ErrorMessage="(*)" ValidationGroup="G1" 
                ControlToValidate="txtUsingCount" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNumberOfCode"
    ErrorMessage="Vui lòng nhập số lần sử dụng" ForeColor="Red" ValidationGroup="G1" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                </td>
            <td>
                <asp:TextBox ID="txtNumberOfCode" runat="server" CssClass="tukhoatimekiem" 
                    Width="300px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td >
                Độ dài mã:
                </td>
            <td>
                <asp:TextBox ID="txtLenOfCode" runat="server" CssClass="tukhoatimekiem" 
                    Width="300px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td >
                Kiểu mã:
                </td>
            <td>
                <asp:DropDownList runat="server" ID="ddlCodeGenType" CssClass="userselect" Width="310px" >
                    <asp:ListItem Value="Any" Text="Chữ và Số" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="DigitOnly" Text="Chỉ chữ số"></asp:ListItem>
                </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td >
                <asp:Label ID="lblReviewStatusId" runat="server" Text="Trạng thái:" 
                    meta:resourcekey="lblReviewStatusId"></asp:Label>
                </td>
            <td>
                <asp:DropDownList runat="server" ID="ddlReviewStatusId" DataTextField="ReviewStatusDesc" DataValueField="ReviewStatusId" CssClass="userselect" Width="310px" ></asp:DropDownList>
                </td>
        </tr>
        <%--<tr>
            <td >
                Số download miễn phí:
                </td>
            <td>
                <asp:TextBox ID="txtNumDownload" runat="server" CssClass="tukhoatimekiem" 
                    Width="300px"></asp:TextBox>
                </td>
        </tr>--%>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
   <div style="background-color: #FFFFFF; bottom: 0px;  padding: 8px;  position: fixed;   right: 1px;   text-align: right;  width: 100%;">
<asp:CheckBox ID="cblAddOther" runat="server" Text="Thêm tiếp dữ liệu khác" Checked="false" ToolTip="Khi tick chọn xóa form hiện tại để thêm dữ liệu khác"  />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom" 
                    Text="Lưu thông tin" meta:resourcekey="btnSave"  ValidationGroup="G1"
            onclick="btnSave_Click">
                        </asp:LinkButton></div>
</asp:Content>

