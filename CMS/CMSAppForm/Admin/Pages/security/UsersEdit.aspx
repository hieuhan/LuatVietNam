<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="UsersEdit.aspx.cs" Inherits="Admin_UsersEdit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
<script type="text/javascript">
    $(function () {
        $(".txtdatetime").datepicker({ dateFormat: 'dd/mm/yy' });
    });
    
</script>
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td style="width:120px"><asp:Label ID="lblUserName" runat="server" Text="Tên truy nhập:" meta:resourcekey="lblUserName"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtUserName" runat="server" Width="350px" Text=""></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblPassword" runat="server" Text="Mật khẩu:" meta:resourcekey="lblPassword"></asp:Label>
                </td>
            <td><asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="350px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblFullname" runat="server" Text="Họ tên:" meta:resourcekey="lblFullname"></asp:Label>
                </td>
            <td><asp:TextBox ID="txtFullname" runat="server" Width="350px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblBirthDay" runat="server" Text="Ngày sinh:" meta:resourcekey="lblBirthDay"></asp:Label>
                </td>
            <td><asp:TextBox ID="txtBirthDay" runat="server" Width="350px"  CssClass="txtdatetime"></asp:TextBox>&nbsp; (dd/mm/yyyy)
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblGender" runat="server" Text="Giới tính:" meta:resourcekey="lblGender"></asp:Label>
                </td>
            <td>
                <asp:RadioButtonList ID="rdoGender" runat="server" DataTextField="GenderDesc" DataValueField="GenderId" RepeatDirection="Horizontal">
                </asp:RadioButtonList>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblMobile" runat="server" Text="Mobile:" meta:resourcekey="lblMobile"></asp:Label>
                </td>
            <td><asp:TextBox ID="txtMobile" runat="server" Width="350px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblAddress" runat="server" Text="Địa chỉ:" meta:resourcekey="lblAddress"></asp:Label>
                </td>
            <td><asp:TextBox ID="txtAddress" runat="server" Width="350px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblEmail" runat="server" Text="Email:" meta:resourcekey="lblEmail"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" Width="350px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblOrgan" runat="server" Text="Đơn vị:" meta:resourcekey="lblOrgan"></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlOrgan" runat="server" DataTextField="OrganDesc" DataValueField="OrganId">
                </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblRank" runat="server" Text="Chức vụ:" meta:resourcekey="lblRank"></asp:Label>
                </td>
            <td><asp:DropDownList ID="ddlRank" runat="server" DataTextField="RankDesc" DataValueField="RankId">
                </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblComment" runat="server" Text="Ghi chú:" meta:resourcekey="lblComment"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtComment" runat="server" Width="350px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblAction" runat="server" Text="Trang mặc định:" meta:resourcekey="lblAction"></asp:Label>
                </td>
            <td><asp:DropDownList ID="ddlAction" runat="server" DataTextField="ActionDesc" DataValueField="ActionId">
                </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblUserStatus" runat="server" Text="Trạng thái:" meta:resourcekey="lblUserStatus"></asp:Label>
                </td>
            <td><asp:DropDownList ID="ddlUserStatus" runat="server" 
                    DataTextField="UserStatusDesc" DataValueField="UserStatusId"></asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblRole" runat="server" Text="Nhóm chức năng:" meta:resourcekey="lblRole"></asp:Label>
                </td>
            <td>
                <asp:CheckBoxList ID="chkRole" runat="server" DataTextField="RoleDesc" DataValueField="RoleId" RepeatColumns="3" RepeatDirection="Horizontal">
                </asp:CheckBoxList>
                </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <div style="text-align:center"><asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom" 
                    Text="Lưu thông tin" meta:resourcekey="btnSave" 
            onclick="btnSave_Click">
                        </asp:LinkButton></div>
</asp:Content>

