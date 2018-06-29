<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="ServicePackagesEdit.aspx.cs" Inherits="Admin_ServicePackagesEdit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter;padding-bottom: 30px;">
        <tr>
            <td style="width:165px">Site:
                </td>
            <td>
                <asp:DropDownList ID="ddlSite" runat="server" DataTextField="SiteDesc" 
                            DataValueField="SiteId" Width="260px" CssClass="userselect" 
                            AutoPostBack="True" onselectedindexchanged="ddlSite_SelectedIndexChanged">
                        </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td width="165px" ><asp:Label ID="lblServices" runat="server" 
                    Text="Dịch vụ:" meta:resourcekey="lblServices"></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlServices" runat="server" CssClass="userselect"  Width="260px" AutoPostBack="True" onselectedindexchanged="ddlServices_SelectedIndexChanged"
                    DataTextField="ServiceDesc" DataValueField="ServiceId"></asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td width="165px" >Gói cha:
                </td>
            <td>
                <asp:DropDownList ID="ddlParent" runat="server" CssClass="userselect"  Width="260px"
                    DataTextField="ServicePackageDesc" DataValueField="ServicePackageId"></asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblServicePackageName" runat="server" Text="Tên gói dịch vụ:" meta:resourcekey="lblServicePackageName"></asp:Label><span class="icon-required"></span>
            
                </td>
            <td>
                <asp:TextBox ID="txtServicePackageName" runat="server" CssClass="tukhoatimekiem" Width="250px"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="rfvPromotionName" runat="server" Display="Dynamic" 
                ErrorMessage="Bạn cần nhập vào tên gói dịch vụ" ValidationGroup="G1" 
                ControlToValidate="txtServicePackageName" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td >
                <asp:Label ID="lblServicePackageDesc" runat="server" Text="Mô tả:" 
                    meta:resourcekey="lblServicePackageDesc"></asp:Label><span class="icon-required"></span>
                </td>
            <td>
                <asp:TextBox ID="txtServicePackageDesc" runat="server" CssClass="tukhoatimekiem"  Height="50px"
                    Width="250px" TextMode="MultiLine"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" 
                ErrorMessage="Bạn cần nhập vào tên" ValidationGroup="G1" 
                ControlToValidate="txtServicePackageDesc" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td >
                <asp:Label ID="lblNumMonthUse" runat="server" Text="Số tháng sử dụng:" 
                    meta:resourcekey="lblNumMonthUse"></asp:Label><span class="icon-required"></span>
                </td>
            <td>
                <asp:TextBox ID="txtNumMonthUse" runat="server" CssClass="tukhoatimekiem" 
                    Width="250px"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="rfvNumMonthFree" runat="server" Display="Dynamic" 
                ErrorMessage="Bạn cần nhập vào số tháng sử dụng" ValidationGroup="G1" 
                ControlToValidate="txtNumMonthUse" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="rgltxtNumMonthFree" runat="server" ForeColor="Red" ErrorMessage="Chỉ chấp nhận kiểu số" 
                    Display="Dynamic" ValidationExpression="\d+" ControlToValidate="txtNumMonthUse"></asp:RegularExpressionValidator>
                </td>
        </tr>
        <tr>
            <td >
                <asp:Label ID="lblNumDayUse" runat="server" Text="Số ngày sử dụng:" 
                    meta:resourcekey="lblNumDayUse"></asp:Label><span class="icon-required"></span>
                </td>
            <td>
                <asp:TextBox ID="txtNumDayUse" runat="server" CssClass="tukhoatimekiem" 
                    Width="250px"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" 
                ErrorMessage="Bạn cần nhập vào số ngày sử dụng" ValidationGroup="G1" 
                ControlToValidate="txtNumDayUse" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ForeColor="Red" ErrorMessage="Chỉ chấp nhận kiểu số" 
                    Display="Dynamic" ValidationExpression="\d+" ControlToValidate="txtNumDayUse"></asp:RegularExpressionValidator>
                </td>
        </tr>
        <tr>
            <td >
                Số lần download:
                </td>
            <td>
                <asp:TextBox ID="txtNumDownload" runat="server" CssClass="tukhoatimekiem" 
                    Width="250px"  Text="0"></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ForeColor="Red" ErrorMessage="Chỉ chấp nhận kiểu số" 
                    Display="Dynamic" ValidationExpression="\d+" ControlToValidate="txtNumDownload"></asp:RegularExpressionValidator>
                </td>
        </tr>
        <tr>
            <td >
                <asp:Label ID="lblConcurrentLogin" runat="server" Text="Số lần đăng nhập cùng lúc:" 
                    meta:resourcekey="lblConcurrentLogin"></asp:Label><span class="icon-required"></span>
                </td>
            <td>
                <asp:TextBox ID="txtConcurrentLogin" runat="server" CssClass="tukhoatimekiem" 
                    Width="250px" Text="1"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" 
                ErrorMessage="Bạn cần nhập vào số lần đăng nhập cùng lúc" ValidationGroup="G1" 
                ControlToValidate="txtConcurrentLogin" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ForeColor="Red" ErrorMessage="Chỉ chấp nhận kiểu số" 
                    Display="Dynamic" ValidationExpression="\d+" ControlToValidate="txtConcurrentLogin"></asp:RegularExpressionValidator>
                </td>
        </tr>
        <tr>
            <td >
                <asp:Label ID="lblPrice" runat="server" Text="Giá:" 
                    meta:resourcekey="lblPrice"></asp:Label><span class="icon-required"></span>
                </td>
            <td>
                <asp:TextBox ID="txtPrice" runat="server" CssClass="tukhoatimekiem" 
                    Width="250px" Text="0"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" 
                ErrorMessage="Bạn cần nhập vào giá tiền" ValidationGroup="G1" 
                ControlToValidate="txtPrice" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ForeColor="Red" ErrorMessage="Chỉ chấp nhận kiểu số" 
                    Display="Dynamic" ValidationExpression="\d+" ControlToValidate="txtPrice"></asp:RegularExpressionValidator>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblReviewStatus" runat="server" Text="Trạng thái:" meta:resourcekey="lblReviewStatus"></asp:Label>
                </td>
            <td><asp:DropDownList ID="ddlReviewStatus" runat="server" CssClass="userselect"  Width="260px"
                    DataTextField="ReviewStatusDesc" DataValueField="ReviewStatusId"></asp:DropDownList>
                </td>
        </tr>
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

