<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="OrgansEdit.aspx.cs" Inherits="Admin_OrgansEdit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td >&nbsp;</td>
            <td>
                <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label>
                </td>
        </tr>
        <tr>
            <td style="width:120px" >
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
            <td><asp:Label ID="lblOrganName" runat="server" Text="Tên Cơ quan:" meta:resourcekey="lblOrganName"></asp:Label><span class="icon-required"></span>
            <asp:RequiredFieldValidator ID="rfvOrganName" runat="server" Display="Dynamic" 
                ErrorMessage="(*)" ValidationGroup="G1" 
                ControlToValidate="txtOrganName" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            <td>
                <asp:TextBox ID="txtOrganName" runat="server" CssClass="tukhoatimekiem" Width="240px"></asp:TextBox><br />
                
<asp:RequiredFieldValidator ID="rfvtxtOrganName" runat="server" ErrorMessage="Bạn cần nhập vào tên CQBH" ForeColor="Red" 
ControlToValidate="txtOrganName" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblOrganDesc" runat="server" 
                    Text="Mô tả:" meta:resourcekey="lblOrganDesc"></asp:Label><span class="icon-required"></span>
                    <asp:RequiredFieldValidator ID="rfvOrganDesc" runat="server" Display="Dynamic" 
                ErrorMessage="(*)" ValidationGroup="G1" 
                ControlToValidate="txtOrganDesc" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            <td>
                <asp:TextBox ID="txtOrganDesc" runat="server" CssClass="tukhoatimekiem" 
                    Width="240px" Height="100px" TextMode="MultiLine"></asp:TextBox><br />
                
<asp:RequiredFieldValidator ID="rfvtxtOrganDesc" runat="server" ErrorMessage="Bạn cần nhập mô tả" ForeColor="Red" 
ControlToValidate="txtOrganDesc" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td ><asp:Label ID="lblOrganTypes" runat="server" 
                    Text="Loại Cơ quan:" meta:resourcekey="lblOrganTypes"></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlOrganTypes" runat="server" 
                    CssClass="userselect" DataTextField="OrganTypeDesc" 
                    DataValueField="OrganTypeId" Width="250px">
                </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblDisplayOrder" runat="server" 
                    Text="Thứ tự:" meta:resourcekey="lblDisplayOrder"></asp:Label>
<%--                     <asp:RequiredFieldValidator ID="rfvDisplayOrder" runat="server" Display="Dynamic" 
                ErrorMessage="(*)" ValidationGroup="G1" 
                ControlToValidate="txtDisplayOrder" ForeColor="Red"></asp:RequiredFieldValidator>--%>
               
                </td>
            <td>
                <asp:TextBox ID="txtDisplayOrder" runat="server" CssClass="tukhoatimekiem" Text="10"
                    Width="240px"></asp:TextBox><br />
                 <asp:RegularExpressionValidator ID="rglDisplayOrder" runat="server" ForeColor="Red" ErrorMessage="Chỉ chấp nhận kiểu số"
                        Display="Dynamic" ValidationExpression="\d+" ControlToValidate="txtDisplayOrder"></asp:RegularExpressionValidator>
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
    <div style="background-color: #FFFFFF; bottom: 0px;  padding: 8px;  position: fixed;   right: 1px;   text-align: right;  width: 100%;">
<asp:CheckBox ID="cblAddOther" runat="server" Text="Thêm tiếp dữ liệu khác" Checked="false" ToolTip="Khi tick chọn sẽ xóa form hiện tại để thêm dữ liệu khác"  />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<asp:LinkButton ID="btnSave" ValidationGroup="G1" runat="server" CssClass="savebutom" meta:resourcekey="btnSave" Text=" Lưu " onclick="btnSave_Click">
</asp:LinkButton></div>
</asp:Content>

