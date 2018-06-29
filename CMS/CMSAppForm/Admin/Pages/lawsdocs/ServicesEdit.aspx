<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="ServicesEdit.aspx.cs" Inherits="Admin_ServicesEdit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <table cellpadding="2" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td style="width:90px">Site:
                </td>
            <td>
                <asp:DropDownList ID="ddlSite" runat="server" DataTextField="SiteDesc" 
                            DataValueField="SiteId" Width="250px" CssClass="userselect">
                        </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td width="90px" ><asp:Label ID="lblServiceName" runat="server" Text="Tên:" meta:resourcekey="lblServiceName"></asp:Label>
                <span class="icon-required"></span>
                <asp:RequiredFieldValidator ID="rfvServiceName" runat="server" Display="Dynamic" 
                ErrorMessage="(*)" ValidationGroup="G1" 
                ControlToValidate="txtServiceName" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            <td>
                <asp:TextBox ID="txtServiceName" runat="server" CssClass="tukhoatimekiem" Width="250px"></asp:TextBox><br/>
                <asp:RequiredFieldValidator ID="rfvtxtServiceName" runat="server" ErrorMessage="Tên dịch vụ" ForeColor="Red" ControlToValidate="txtServiceName" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td >
                <asp:Label ID="lblServiceDesc" runat="server" Text="Mô tả:" 
                    meta:resourcekey="lblServiceDesc"></asp:Label>
                <span class="icon-required"></span>
                    <asp:RequiredFieldValidator ID="rfvServiceDesc" runat="server" Display="Dynamic" 
                ErrorMessage="(*)" ValidationGroup="G1" 
                ControlToValidate="txtServiceDesc" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            <td>
                <asp:TextBox ID="txtServiceDesc" runat="server" CssClass="tukhoatimekiem"  Height="50px"
                    Width="250px" TextMode="MultiLine"></asp:TextBox><br/>
                <asp:RequiredFieldValidator ID="rfvtxtServiceDesc" runat="server" ErrorMessage="Mô tả dịch vụ" ForeColor="Red" ControlToValidate="txtServiceDesc" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td >
                <asp:Label ID="lblServiceCode" runat="server" Text="Mã dịch vụ:" 
                    meta:resourcekey="lblServiceCode"></asp:Label>
                <span class="icon-required"></span>
                    <asp:RequiredFieldValidator ID="rfvServiceCode" runat="server" Display="Dynamic" 
                ErrorMessage="(*)" ValidationGroup="G1" 
                ControlToValidate="txtServiceCode" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            <td>
                <asp:TextBox ID="txtServiceCode" runat="server" CssClass="tukhoatimekiem" 
                    Width="250px"></asp:TextBox>
                <br/>
                <asp:RequiredFieldValidator ID="rfvtxtServiceCode" runat="server" ErrorMessage="Mã tả dịch vụ" ForeColor="Red" ControlToValidate="txtServiceCode" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblReviewStatus" runat="server" Text="Trạng thái:" meta:resourcekey="lblReviewStatus"></asp:Label>
                </td>
            <td><asp:DropDownList ID="ddlReviewStatus" runat="server" CssClass="userselect"  Width="260px"
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

