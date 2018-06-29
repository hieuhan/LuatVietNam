<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="SignersEdit.aspx.cs" Inherits="Admin_SignersEdit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td style="width:160px"><asp:Label ID="lblSignerName" runat="server" Text="Tên người ký:" meta:resourcekey="lblSignerName"></asp:Label>
            <span class="icon-required"></span>
            <asp:RequiredFieldValidator ID="rfvSignerName" runat="server" Display="Dynamic" 
                ErrorMessage="(*)" ValidationGroup="G1" 
                ControlToValidate="txtSignerName" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            <td>
                <asp:TextBox ID="txtSignerName" runat="server" CssClass="tukhoatimekiem" Width="240px"></asp:TextBox><br/>
                <asp:RequiredFieldValidator ID="rfvtxtSignerName" runat="server" ErrorMessage="Vui lòng nhập Tên người ký" ForeColor="Red" ControlToValidate="txtSignerName" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td style="width:160px"><asp:Label ID="lblMissionYear" runat="server" 
                    Text="Năm công tác từ:" meta:resourcekey="lblMissionYear"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtMissionYearFrom" runat="server" CssClass="tukhoatimekiem" 
                    Width="97px"></asp:TextBox>
                <asp:Label ID="lblMissionYearTo" runat="server" 
                    Text="đến:" meta:resourcekey="lblMissionYearTo"></asp:Label>
                <asp:TextBox ID="txtMissionYearTo" runat="server" CssClass="tukhoatimekiem" 
                    Width="97px"></asp:TextBox>
                <a class="help-lnk" href="javascript:void(0)" title="Có thể để trống, giá trị mặc định bằng 0" ><span class="aui-icon aui-icon-small aui-iconfont-help">?</span></a><br/>
                </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:RangeValidator ID="rvMissionYearFrom" runat="server" 
                        ErrorMessage="Năm công tác hợp lệ trong khoảng 0 - 2099."
                        ControlToValidate="txtMissionYearFrom"
                        MinimumValue="0"
                        MaximumValue="2099"
                        ValidationGroup="G1"
                        ForeColor="Red"
                        SetFocusOnError="true"
                        Type="Integer">
                </asp:RangeValidator><br/>
                <asp:RangeValidator ID="rvtxtMissionYearTo" runat="server" 
                        ErrorMessage="Năm kết thúc công tác hợp lệ trong khoảng 0 - 2099."
                        ControlToValidate="txtMissionYearTo"
                        MinimumValue="0"
                        MaximumValue="2099"
                        ValidationGroup="G1"
                        ForeColor="Red"
                        SetFocusOnError="true"
                        Type="Integer">
                </asp:RangeValidator>
            </td>
        </tr>
        <tr>
            <td style="width:160px">
                <asp:Label ID="lblOrganId" runat="server" meta:resourcekey="lblOrganId" 
                    Text="Cơ quan:"></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlOrgans" runat="server" 
                    CssClass="userselect" DataTextField="OrganDesc" 
                    DataValueField="OrganId" Width="250px">
                </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td style="width:160px">
                <asp:Label ID="lblRegencies" runat="server" 
                    meta:resourcekey="lblRegencies" Text="Chức vụ:"></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlRegencies" runat="server" 
                    CssClass="userselect" DataTextField="RegencyDesc" DataValueField="RegencyId" Width="250px">
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

