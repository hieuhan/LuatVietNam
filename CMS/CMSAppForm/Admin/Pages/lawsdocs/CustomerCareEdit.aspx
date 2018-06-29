<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="CustomerCareEdit.aspx.cs" Inherits="Admin_CustomerCareEdit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td style="width:90px"><asp:Label ID="lblInfo" runat="server" 
                    Text="Nội dung:" ></asp:Label>
                   <span class="icon-required"></span>
                </td>
            <td>
                <asp:TextBox ID="txtInfo" runat="server" CssClass="tukhoatimekiem" 
                    Width="90%" TextMode="MultiLine" Height="50px"></asp:TextBox>
                <br /><asp:RequiredFieldValidator ID="rfvtxtNewsletterGroupDesc" runat="server" 
                    ErrorMessage="Bạn chưa nhập nội dung CSKH" ForeColor="Red" 
                    ControlToValidate="txtInfo" SetFocusOnError="true" Display="Dynamic" 
                    ValidationGroup="G1"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <div style="text-align:center">
        <asp:CheckBox ID="cblAddOther" Visible="false" runat="server" Text="Thêm tiếp dữ liệu khác" Checked="false" ToolTip="Khi tick chọn sẽ xóa form hiện tại để thêm dữ liệu khác"  />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom" 
                    Text="Lưu thông tin" meta:resourcekey="btnSave"  ValidationGroup="G1"
            onclick="btnSave_Click">
                        </asp:LinkButton>
    </div>
</asp:Content>


