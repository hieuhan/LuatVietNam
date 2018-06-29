<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master"
    AutoEventWireup="true" CodeFile="MenusEdit.aspx.cs" Inherits="Admin_MenusEdit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" runat="Server">
<div style="width:auto; overflow:auto; ">
    <table cellpadding="3" cellspacing="0" style="width: 100%; font-weight: lighter">
        <tr>
            <td  style="width: 120px">
                <asp:Label ID="lblSite" runat="server" Text="Site:" meta:resourcekey="lblSite"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlSite" runat="server" DataTextField="SiteDesc" DataValueField="SiteId"
                    Width="355px" AutoPostBack="True" 
                    onselectedindexchanged="ddlSite_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 120px">
                <asp:Label ID="lblParentCategory" runat="server" meta:resourcekey="lblParentCategory"
                    Text="Chuyên mục:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlParentCategory" runat="server" DataTextField="CategoryDesc"
                    DataValueField="CategoryId" Width="355px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCategoryName" runat="server" Text="Tên:" meta:resourcekey="lblCategoryName"></asp:Label>
                <span class="icon-required"></span>
                <asp:RequiredFieldValidator ID="rfvCategoryName" runat="server" Display="Dynamic" 
                ErrorMessage="(*)" ValidationGroup="G1" 
                ControlToValidate="txtCategoryName" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:TextBox ID="txtCategoryName" runat="server" Width="350px"></asp:TextBox>
                <br /><asp:RequiredFieldValidator ID="rfvtxtCategoryName" runat="server" ErrorMessage="Tên Menu" ForeColor="Red" ControlToValidate="txtCategoryName" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCategoryDesc" runat="server" Text="Mô tả:" meta:resourcekey="lblCategoryDesc"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtCategoryDesc" runat="server" Width="350px"></asp:TextBox>
            </td>
        </tr>
    </table>
    </div>
    <div style="text-align: center; margin-top:20px" >
        <div class="clear5px"></div>
         <asp:CheckBox ID="cblAddOther" runat="server" Text="Thêm tiếp dữ liệu khác" Checked="false" ToolTip="Khi tick chọn sẽ xóa form hiện tại để thêm dữ liệu khác"  />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom" Text="Lưu thông tin"
            meta:resourcekey="btnSave" ValidationGroup="G1" OnClick="btnSave_Click">
        </asp:LinkButton></div>
</asp:Content>
