<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="NewsletterGroupsEdit.aspx.cs" Inherits="Admin_NewsletterGroupsEdit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td style="width:50px">Site:
                </td>
            <td>
                <asp:DropDownList ID="ddlSite" runat="server" DataTextField="SiteDesc" 
                            DataValueField="SiteId" Width="250px" CssClass="userselect">
                        </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td style="width:50px"><asp:Label ID="lblNewsletterGroupName" runat="server" Text="Tên:" meta:resourcekey="lblNewsletterGroupName"></asp:Label>
            <span class="icon-required"></span></td>
            <td>
                <asp:TextBox ID="txtNewsletterGroupName" runat="server" CssClass="tukhoatimekiem" Width="240px"></asp:TextBox>
                <br /><asp:RequiredFieldValidator ID="rfvtxtNewsletterGroupName" runat="server" 
                    ErrorMessage="Bạn chưa nhập tên danh sách nhóm" ForeColor="Red" 
                    ControlToValidate="txtNewsletterGroupName" SetFocusOnError="true" Display="Dynamic" 
                    ValidationGroup="G1"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td style="width:50px"><asp:Label ID="lblNewsletterGroupDesc" runat="server" 
                    Text="Mô tả:" meta:resourcekey="lblNewsletterGroupDesc"></asp:Label>
                   <span class="icon-required"></span>
                </td>
            <td>
                <asp:TextBox ID="txtNewsletterGroupDesc" runat="server" CssClass="tukhoatimekiem" 
                    Width="240px"></asp:TextBox>
                <br /><asp:RequiredFieldValidator ID="rfvtxtNewsletterGroupDesc" runat="server" 
                    ErrorMessage="Bạn chưa nhập mô tả danh sách nhóm" ForeColor="Red" 
                    ControlToValidate="txtNewsletterGroupDesc" SetFocusOnError="true" Display="Dynamic" 
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
        <asp:CheckBox ID="cblAddOther" runat="server" Text="Thêm tiếp dữ liệu khác" Checked="false" ToolTip="Khi tick chọn sẽ xóa form hiện tại để thêm dữ liệu khác"  />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom" 
                    Text="Lưu thông tin" meta:resourcekey="btnSave"  ValidationGroup="G1"
            onclick="btnSave_Click">
                        </asp:LinkButton>
    </div>
</asp:Content>


