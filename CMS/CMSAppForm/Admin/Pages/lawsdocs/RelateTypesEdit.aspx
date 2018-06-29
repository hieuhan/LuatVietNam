<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="RelateTypesEdit.aspx.cs" Inherits="Admin_RelateTypesEdit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td style="width:110px">
                <asp:Label ID="lblLanguage" runat="server" meta:resourcekey="lblLanguage" 
                    Text="Ngôn ngữ:"></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlLanguage" runat="server" CssClass="userselect"
                    DataTextField="LanguageDesc" DataValueField="LanguageId" Width="300px">
                </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td style="width:110px">
                <asp:Label ID="lblDocGroups" runat="server" meta:resourcekey="lblDocGroups" 
                    Text="Nhóm văn bản:"></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlDocGroups" runat="server" CssClass="userselect"
                    DataTextField="DocGroupDesc" DataValueField="DocGroupId" Width="300px">
                </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblRelateTypeName" runat="server" Text="Tên liên kết:" meta:resourcekey="lblRelateTypeName"></asp:Label>
                <span class="icon-required"></span>
            <asp:RequiredFieldValidator ID="rfvRelateTypeName" runat="server" Display="Dynamic" 
                ErrorMessage="(*)" ValidationGroup="G1" 
                ControlToValidate="txtRelateTypeName" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            <td>
                <asp:TextBox ID="txtRelateTypeName" runat="server" CssClass="tukhoatimekiem" Width="290px"></asp:TextBox>
                <br /><asp:RequiredFieldValidator ID="rfvtxtRelateTypeName" runat="server" ErrorMessage="Tên liên kết" ForeColor="Red" ControlToValidate="txtRelateTypeName" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblRelateTypeDesc" runat="server" 
                    Text="Mô tả:" meta:resourcekey="lblRelateTypeDesc"></asp:Label>
                <span class="icon-required"></span>
                     <asp:RequiredFieldValidator ID="rfvRelateTypeDesc" runat="server" Display="Dynamic" 
                ErrorMessage="(*)" ValidationGroup="G1" 
                ControlToValidate="txtRelateTypeDesc" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            <td>
                <asp:TextBox ID="txtRelateTypeDesc" runat="server" CssClass="tukhoatimekiem" 
                    Width="290px" Height="60px" TextMode="MultiLine"></asp:TextBox>
                <br /><asp:RequiredFieldValidator ID="rfvtxtRelateTypeDesc" runat="server" ErrorMessage="Mô tả" ForeColor="Red" ControlToValidate="txtRelateTypeDesc" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
                </td>
        </tr>
         <tr>
            <td><asp:Label ID="lblRevertRelateTypeName" runat="server" Text="Tên liên kết ngược:" meta:resourcekey="lblRevertRelateTypeName"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtRevertRelateTypeName" runat="server" CssClass="tukhoatimekiem" Width="290px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblRevertRelateTypeDesc" runat="server" 
                    Text="Mô tả:" meta:resourcekey="lblRevertRelateTypeDesc"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtRevertRelateTypeDesc" runat="server" CssClass="tukhoatimekiem" 
                    Width="290px" Height="60px" TextMode="MultiLine"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblRelateTypeGroups" runat="server" 
                    Text="Loại liên quan:" meta:resourcekey="lblRelateTypeGroups"></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlRelateTypeGroupId" runat="server" 
                    CssClass="userselect" DataTextField="RelateTypeGroupDesc" 
                    DataValueField="RelateTypeGroupId" Width="300px">
                </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblDisplayOrder" runat="server" 
                    Text="Thứ tự:" meta:resourcekey="lblDisplayOrder"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtDisplayOrder" runat="server" CssClass="tukhoatimekiem" 
                    Width="290px"></asp:TextBox>
                <a class="help-lnk" href="javascript:void(0)" title="Thứ tự hiển thị hợp lệ trong khoảng 0 - 250. Có thể để trống, giá trị mặc định bằng 0" ><span class="aui-icon aui-icon-small aui-iconfont-help">?</span></a>
                <br />
                <asp:RangeValidator ID="rvMissionYearFrom" runat="server" 
                        ErrorMessage="Thứ tự hiển thị hợp lệ trong khoảng 0 - 250."
                        ControlToValidate="txtDisplayOrder"
                        MinimumValue="0"
                        MaximumValue="250"
                        ValidationGroup="G1"
                        ForeColor="Red"
                        SetFocusOnError="true"
                        Type="Integer">
                </asp:RangeValidator><br/>
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

