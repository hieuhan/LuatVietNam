<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="DisplayTypesEdit.aspx.cs" Inherits="Admin_DisplayTypesEdit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td width="70px"><asp:Label ID="lblDisplayTypeName" runat="server" Text="Tên:" meta:resourcekey="lblDisplayTypeName"></asp:Label>
            <span class="icon-required"></span>
                </td>
            <td>
                <asp:TextBox ID="txtDisplayTypeName" runat="server" CssClass="tukhoatimekiem" Width="240px"></asp:TextBox>
                 <br /><asp:RequiredFieldValidator ID="rfvtxtDisplayTypeName" runat="server"
                      ErrorMessage="Bạn chưa nhập tên loại hiển thị" ForeColor="Red" 
                     ControlToValidate="txtDisplayTypeName" SetFocusOnError="true" Display="Dynamic" 
                     ValidationGroup="G1"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblDisplayTypeDesc" runat="server" 
                    Text="Mô tả:" meta:resourcekey="lblDisplayTypeDesc"></asp:Label>
                    <span class="icon-required"></span>
                </td>
            <td>
                <asp:TextBox ID="txtDisplayTypeDesc" runat="server" CssClass="tukhoatimekiem" 
                    Width="240px" Height="100px" TextMode="MultiLine"></asp:TextBox>
                <br /><asp:RequiredFieldValidator ID="rfvtxtDisplayTypeDesc" runat="server"
                      ErrorMessage="Mô tả loại hiển thị" ForeColor="Red" 
                     ControlToValidate="txtDisplayTypeDesc" SetFocusOnError="true" Display="Dynamic" 
                     ValidationGroup="G1"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblDataTypes" runat="server" Text="Loại dữ liệu:" 
                    meta:resourcekey="lblDataTypes"></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlDataTypes" runat="server"
                    CssClass="userselect" DataTextField="DataTypeDesc" DataValueField="DataTypeId" Width="250px">
                </asp:DropDownList>
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

