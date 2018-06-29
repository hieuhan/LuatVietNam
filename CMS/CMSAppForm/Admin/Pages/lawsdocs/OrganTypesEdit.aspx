<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="OrganTypesEdit.aspx.cs" Inherits="Admin_OrganTypesEdit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">


    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td style="width:80px"><asp:Label ID="lblOrganTypeName" runat="server" Text="Tên:" meta:resourcekey="lblOrganTypeName">
                </asp:Label>
                <span class="icon-required"></span>
                
                </td>
            <td>
            <asp:TextBox ID="txtOrganTypeName" runat="server" CssClass="tukhoatimekiem" Width="240px"></asp:TextBox>
            <br /><asp:RequiredFieldValidator ID="rfvtxtOrganTypeName" runat="server" ErrorMessage="bạn chưa nhập loại cơ quan ban hành" ForeColor="Red" ControlToValidate="txtOrganTypeName" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblOrganTypeDesc" runat="server" 
                    Text="Mô tả:" meta:resourcekey="lblOrganTypeDesc"></asp:Label>
                    <span class="icon-required"></span>
                
                </td>
            <td>
                <asp:TextBox ID="txtOrganTypeDesc" runat="server" CssClass="tukhoatimekiem" 
                    Width="240px"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldOrganTypeDesc" runat="server" ErrorMessage="Bạn chưa nhập mô tả" ForeColor="Red" ControlToValidate="txtOrganTypeDesc" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblDisplayOrder" runat="server" meta:resourcekey="lblDisplayOrder" 
                    Text="Thứ tự:"></asp:Label> 
                </td>
            <td>
                <asp:TextBox ID="txtDisplayOrder" runat="server" CssClass="tukhoatimekiem" 
                    Width="240px"></asp:TextBox>
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

