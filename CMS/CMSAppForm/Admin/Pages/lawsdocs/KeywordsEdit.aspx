<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="KeywordsEdit.aspx.cs" Inherits="Admin_KeywordsEdit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
       <tr>
            <td style="width:80px">
                <asp:Label ID="lblLanguage" runat="server" meta:resourcekey="lblLanguage" 
                    Text="Ngôn ngữ:"></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlLanguage" runat="server" CssClass="userselect"
                    DataTextField="LanguageDesc" DataValueField="LanguageId" Width="250px">
                </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblKeywordName" runat="server" Text="Từ khóa:" meta:resourcekey="lblKeywordName"></asp:Label>
                <span class="icon-required"></span>
                <asp:RequiredFieldValidator ID="rfvKeywordName" runat="server" Display="Dynamic" 
                ErrorMessage="(*)" ValidationGroup="G1" 
                ControlToValidate="txtKeywordName" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            <td>
                <asp:TextBox ID="txtKeywordName" runat="server" CssClass="tukhoatimekiem" Width="240px"></asp:TextBox>
                <br /><asp:RequiredFieldValidator ID="rfvtxtKeywordName" runat="server" ErrorMessage="Tên từ khóa" ForeColor="Red" ControlToValidate="txtKeywordName" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
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

