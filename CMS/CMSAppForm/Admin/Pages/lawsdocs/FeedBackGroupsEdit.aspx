<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master"
    AutoEventWireup="true" CodeFile="FeedBackGroupsEdit.aspx.cs" Inherits="Admin_Pages_lawsdocs_FeedBackGroupsEdit" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Content ID="contentAccountType" ContentPlaceHolderID="m_contentBody" runat="Server">
    <table cellpadding="3" cellspacing="0" style="width: 100%; font-weight: lighter">
        <tr>
            <td class="td-edit-2">
                &nbsp;
            </td>
            <td>
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td-edit-2">
                <asp:Label ID="lblSite" runat="server" Text="Site:" meta:resourcekey="lblSite"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlSite" runat="server" DataTextField="SiteDesc" 
                            DataValueField="SiteId" Width="250px" CssClass="userselect">
                        </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="td-edit-2">
                <asp:Label ID="lblFeedBackGroupName" runat="server" Text="Tên:" meta:resourcekey="lblFeedBackGroupName"></asp:Label>
                <span class="icon-required"></span>
                <asp:RequiredFieldValidator ID="rfvFeedBackGroupName" runat="server" Display="Dynamic" 
                ErrorMessage="(*)" ValidationGroup="G1" 
                ControlToValidate="txtFeedBackGroupName" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:TextBox ID="txtFeedBackGroupName" runat="server" CssClass="tukhoatimekiem" Width="400px"></asp:TextBox>
                <br /><asp:RequiredFieldValidator ID="rfvtxtFeedBackGroupName" runat="server" ErrorMessage="Nhóm phản hồi" ForeColor="Red" ControlToValidate="txtFeedBackGroupName" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="td-edit-2">
                <asp:Label ID="lblFeedBackGroupDesc" runat="server" Text="Mô tả:" meta:resourcekey="lblFeedBackGroupDesc"></asp:Label>
                <span class="icon-required"></span>
                <asp:RequiredFieldValidator ID="rfvFeedBackGroupDesc" runat="server" Display="Dynamic" 
                ErrorMessage="(*)" ValidationGroup="G1" 
                ControlToValidate="txtFeedBackGroupDesc" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:TextBox ID="txtFeedBackGroupDesc" TextMode="MultiLine" runat="server" CssClass="tukhoatimekiem"
                    Width="400px" Height="81px"></asp:TextBox>
                <br /><asp:RequiredFieldValidator ID="rfvtxtFeedBackGroupDesc" runat="server" ErrorMessage="Mô tả" ForeColor="Red" ControlToValidate="txtFeedBackGroupDesc" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="td-edit-2" colspan="2">
                <br />
                <div style="text-align: center">
                    <asp:CheckBox ID="cblAddOther" runat="server" Text="Thêm tiếp dữ liệu khác" Checked="false" ToolTip="Khi tick chọn sẽ xóa form hiện tại để thêm dữ liệu khác"  />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="savebutom" Text="Lưu thông tin"
                        meta:resourcekey="btnSave" ValidationGroup="G1" OnClick="btnSave_Click">
                    </asp:LinkButton>
                </div>
            </td>
        </tr>
    </table>
    <br />
</asp:Content>
