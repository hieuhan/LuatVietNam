<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="VoteContentsEdit.aspx.cs" Inherits="Admin_VoteContentsEdit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td style="width:160px">&nbsp;</td>
            <td>
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                </td>
        </tr>
        <tr>
            <td style="width:160px"><asp:Label ID="lblLanguage" runat="server" Text="Ngôn ngữ:" meta:resourcekey="lblLanguage"></asp:Label>
                </td>
            <td><asp:DropDownList ID="ddlLanguage" runat="server" DataTextField="LanguageDesc" 
                    DataValueField="LanguageId" Width="250px" Enabled="False">
                </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblVoteContent" runat="server" Text="Nội dung:" meta:resourcekey="lblVoteContent"></asp:Label>
                <span class="icon-required"></span>
                </td>
            <td>
                <asp:TextBox ID="txtVoteContent" runat="server" Width="350px" Height="65px" TextMode="MultiLine"></asp:TextBox>
                <br /><asp:RequiredFieldValidator ID="rfvtxtVoteContent" runat="server" ErrorMessage="Bạn chưa nhập nội dung bình luận" 
                    ForeColor="Red" ControlToValidate="txtVoteContent" SetFocusOnError="true" 
                    Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblDisplayOrder" runat="server" Text="Thứ tự:" meta:resourcekey="lblDisplayOrder"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtDisplayOrder" runat="server" Width="350px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblVoteCounter" runat="server" Text="Tổng bình chọn:" 
                    meta:resourcekey="lblVoteCounter"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtVoteCounter" runat="server" Width="350px"></asp:TextBox>
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
        <asp:CheckBox ID="cblAddOther" runat="server" Text="Thêm tiếp dữ liệu khác" Checked="false" ToolTip="Khi tick chọn sẽ xóa form hiện tại để thêm dữ liệu khác"  />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="btnSave" ValidationGroup="G1" runat="server" CssClass="savebutom"  Text=" Lưu "
            onclick="btnSave_Click">
        </asp:LinkButton>
        
    </div>
</asp:Content>

