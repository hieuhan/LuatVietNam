<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="FieldsEdit.aspx.cs" Inherits="Admin_FieldsEdit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td style="width:120px">
                <asp:Label ID="lblLanguage" runat="server" Text="Ngôn ngữ:" meta:resourcekey="lblLanguage"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlLanguage" runat="server" DataTextField="LanguageDesc" CssClass="userselect" 
                    DataValueField="LanguageId" Width="350px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblParentField" runat="server" Text="Chuyên mục cha:" meta:resourcekey="lblParentField"></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlParentField" runat="server"  CssClass="userselect" 
                    DataTextField="FieldDesc" DataValueField="FieldId" 
                    Width="350px"></asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblFieldName" runat="server" Text="Tên Lĩnh vực:" meta:resourcekey="lblFieldName"></asp:Label>
            <span class="icon-required"></span>
            <asp:RequiredFieldValidator ID="rfvFieldName" runat="server" Display="Dynamic" 
                ErrorMessage="(*)" ValidationGroup="G1" 
                ControlToValidate="txtFieldName" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            <td>
                <asp:TextBox ID="txtFieldName" runat="server" Width="340px" CssClass="tukhoatimekiem"></asp:TextBox>
                <br /><asp:RequiredFieldValidator ID="rfvtxtFieldName" runat="server" ErrorMessage="Tên lĩnh vực" ForeColor="Red" ControlToValidate="txtFieldName" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblFieldDesc" runat="server" Text="Mô tả:" meta:resourcekey="lblFieldDesc"></asp:Label>
            <span class="icon-required"></span>
            <asp:RequiredFieldValidator ID="rfvFieldDesc" runat="server" Display="Dynamic" 
                ErrorMessage="(*)" ValidationGroup="G1" 
                ControlToValidate="txtFieldDesc" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            <td><asp:TextBox ID="txtFieldDesc" runat="server" Width="340px" CssClass="tukhoatimekiem"></asp:TextBox>
                <br /><asp:RequiredFieldValidator ID="rfvtxtFieldDesc" runat="server" ErrorMessage="Mô tả lĩnh vực" ForeColor="Red" ControlToValidate="txtFieldDesc" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblDisplayOrder" runat="server" Text="Thứ tự hiển thị:" meta:resourcekey="lblDisplayOrder"></asp:Label>
                </td>
            <td><asp:TextBox ID="txtDisplayOrder" runat="server" Width="340px" CssClass="tukhoatimekiem"></asp:TextBox>
                <a class="help-lnk" href="javascript:void(0)" title="Thứ tự hiển thị có thể để trống, giá trị mặc định bằng 0" ><span class="aui-icon aui-icon-small aui-iconfont-help">?</span></a>
                <br />
                <asp:RangeValidator ID="rvtxtDisplayOrder" runat="server" 
                        ErrorMessage="Thứ tự hiển thị hợp lệ trong khoảng 0 -  30000."
                        ControlToValidate="txtDisplayOrder"
                        MinimumValue="0"
                        MaximumValue="30000"
                        ValidationGroup="G1"
                        ForeColor="Red"
                        SetFocusOnError="true"
                        Type="Integer">
                </asp:RangeValidator>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblReviewStatus" runat="server" Text="Trạng thái:" meta:resourcekey="lblReviewStatus"></asp:Label>
                </td>
            <td><asp:DropDownList ID="ddlReviewStatus" runat="server" CssClass="userselect"  
                    DataTextField="ReviewStatusDesc" DataValueField="ReviewStatusId" Width="350px"></asp:DropDownList>
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

