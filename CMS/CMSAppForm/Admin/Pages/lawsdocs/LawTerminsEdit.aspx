<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="LawTerminsEdit.aspx.cs" Inherits="Admin_LawTerminsEdit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td style="width:80px">
                <asp:Label ID="lblLanguage" runat="server" meta:resourcekey="lblLanguage" 
                    Text="Ngôn ngữ:"></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlLanguage" runat="server" 
                    CssClass="userselect" DataTextField="LanguageDesc" DataValueField="LanguageId" Width="250px">
                </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblTermName" runat="server" Text="Tên:" meta:resourcekey="lblTermName"></asp:Label>
                <span class="icon-required"></span>
                <asp:RequiredFieldValidator ID="rfvTermName" ValidationGroup="G1" runat="server" ErrorMessage="(*)" ControlToValidate="txtTermName" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            <td>
                <asp:TextBox ID="txtTermName" runat="server" CssClass="tukhoatimekiem" Width="240px"></asp:TextBox>
                <br /><asp:RequiredFieldValidator ID="rfvtxtTermName" runat="server" ErrorMessage="Tên thuật ngữ pháp lý" ForeColor="Red" ControlToValidate="txtTermName" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblTermDesc" runat="server" 
                    Text="Mô tả:" meta:resourcekey="lblTermDesc"></asp:Label>
                <span class="icon-required"></span>
                     <asp:RequiredFieldValidator ID="rfvTermDesc" runat="server" Display="Dynamic" 
                ErrorMessage="(*)" ValidationGroup="G1" 
                ControlToValidate="txtTermDesc" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            <td>
                <asp:TextBox ID="txtTermDesc" runat="server" CssClass="tukhoatimekiem" 
                    Width="240px" Height="60px" TextMode="MultiLine"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="rfvtxtTermDesc" runat="server" ErrorMessage="Mô tả thuật ngữ pháp lý" ForeColor="Red" ControlToValidate="txtTermDesc" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblTermSource" runat="server" 
                    Text="Nguồn:" meta:resourcekey="lblTermSource"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtTermSource" runat="server" CssClass="tukhoatimekiem" 
                    Width="240px"></asp:TextBox>
                </td>
        </tr>
         <tr>
            <td style="width:80px">
                <asp:Label ID="lblLawTerminGroup" runat="server" meta:resourcekey="lblLawTerminGroup" 
                    Text="Nhóm:"></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlLawTerminGroupID" runat="server" 
                    CssClass="userselect" DataTextField="LawTerminGroupDesc" DataValueField="LawTerminGroupId" Width="250px">
                </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblReviewStatus" runat="server" Text="Trạng thái:" meta:resourcekey="lblReviewStatus"></asp:Label>
                </td>
            <td><asp:DropDownList ID="ddlReviewStatus" runat="server" CssClass="userselect"  Width="250px"
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
    <div style="text-align:center">
        <asp:CheckBox ID="cblAddOther" runat="server" Text="Thêm tiếp dữ liệu khác" Checked="false" ToolTip="Khi tick chọn sẽ xóa form hiện tại để thêm dữ liệu khác"  />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom" 
                    Text="Lưu thông tin" meta:resourcekey="btnSave"  ValidationGroup="G1"
            onclick="btnSave_Click">
                        </asp:LinkButton>
    </div>
</asp:Content>

