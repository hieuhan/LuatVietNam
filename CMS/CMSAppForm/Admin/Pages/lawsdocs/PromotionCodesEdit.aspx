<%@ Page Language="C#" MasterPageFile="~/admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="PromotionCodesEdit.aspx.cs" Inherits="admin_pages_PromotionCodesEdit" Title="" %>
<asp:Content ID="contentAccountType" runat="server" ContentPlaceHolderID="m_contentBody">   
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter" >
        <tr>
            <td>
                &nbsp;</td>
            <td>
               Mã khuyến mại
                <span class="icon-required"></span>
            </td>
            <td>
                <asp:TextBox ID="txPromotionCode" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox><br />
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Bạn cần nhập mã khuyến mại" ForeColor="Red" ControlToValidate="txPromotionCode" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>

            </td>
        </tr> 
        <tr>
            <td>
                &nbsp;</td>
            <td>
               Số lần đã dùng
                <span class="icon-required"></span>
            </td>
            <td>
                <asp:TextBox ID="txUsingCount" runat="server" CssClass="tukhoatimekiem" Width="90%" Text="0"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Bạn cần nhập số lần đã sử dụng" ForeColor="Red" ControlToValidate="txUsingCount" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="rgltxUsingCount" runat="server" ForeColor="Red" ErrorMessage="Chỉ chấp nhận kiểu số" Display="Dynamic" ValidationExpression="\d+" ControlToValidate="txUsingCount"></asp:RegularExpressionValidator>
            </td>
        </tr> 
       
    </table>
    <div style="text-align:center; padding: 20px">
        <asp:CheckBox ID="cblAddOther" runat="server" Text="Thêm tiếp dữ liệu khác" Checked="false" ToolTip="Khi tick chọn xóa form hiện tại để thêm dữ liệu khác"  />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

        <asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom"  Text="Lưu thông tin" meta:resourcekey="btnSave" ValidationGroup="G1"
            onclick="btnSave_Click">
        </asp:LinkButton>
    </div>
</asp:Content>

