<%@ Page Language="C#" MasterPageFile="~/admin/MasterPageAdmin.master" AutoEventWireup="true"
    CodeFile="changeProfile.aspx.cs" Inherits="ICSoft.AppForm.Admin.PageChangeProfile"
    Title="Cập nhật Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="m_contentBody" runat="Server">
<script type="text/javascript">
    $(function () {
        $(".txtdatetime").datepicker({ dateFormat: 'dd/mm/yy' });
    });
    
</script>
    <table border="0" id="table1" align="center" cellpadding="1" cellspacing="1">
        <tr>
            <td class="td-edit-4">
                
            </td>
            <td>
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td-edit-4">
                <asp:Label ID="lblUserName" runat="server" Text="Tên truy nhập:" meta:resourcekey="lblUserName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtUsername" runat="server" MaxLength="255" CssClass="tbSearch" ReadOnly="true"></asp:TextBox>&nbsp;
            </td>
        </tr>
        <tr>
            <td class="td-edit-4">
                <asp:Label ID="lblPassword" runat="server" Text="Mật khẩu:" meta:resourcekey="lblPassword"></asp:Label>
            </td>
            <td style="height: 26px">
                <asp:TextBox ID="txtPass" runat="server" MaxLength="50" CssClass="tbSearch" TextMode="Password"></asp:TextBox>
            </td>
            <asp:RegularExpressionValidator ID="regPassValid" runat="server" ControlToValidate="txtPass"
                ValidationExpression="\w{6,50}" ErrorMessage="Password khong hop le!" Display="dynamic">* </asp:RegularExpressionValidator></tr>
        <tr>
            <td class="td-edit-4">
                <asp:Label ID="lblRePass" runat="server" Text="Gõ lại mật khẩu:" meta:resourcekey="lblPassword"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtConfirmPass" runat="server" MaxLength="50" CssClass="tbSearch" TextMode="Password"></asp:TextBox>
                <asp:CompareValidator ID="cmpPass" runat="server" ControlToValidate="txtConfirmPass"
                    ControlToCompare="txtPass" Operator="Equal" ErrorMessage="Mật khẩu không khớp! Hãy xác nhận lại mật khẩu!" />
            </td>
        </tr>
        <tr>
            <td class="td-edit-4">
                <asp:Label ID="lblFullname" runat="server" Text="Họ tên:" meta:resourcekey="lblFullname"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFullname" runat="server" CssClass="tbSearch" MaxLength="255"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFullname" ControlToValidate="txtFullname"
                    Text="Nhập họ tên đầy đủ" runat="server"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="td-edit-4">
                <asp:Label ID="lblEmail" runat="server" Text="Email:" meta:resourcekey="lblEmail"></asp:Label>
            </td>
            <td >
                <asp:TextBox ID="txtEmail" runat="server" CssClass="tbSearch" MaxLength="255"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredEmail" ControlToValidate="txtEmail" Text="Nhập thư điện tử."
                    runat="server"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revEmail" ControlToValidate="txtEmail" ErrorMessage="Email không đúng khuôn dạng!"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="td-edit-4">
                <asp:Label ID="lblOrgan" runat="server" Text="Đơn vị:" meta:resourcekey="lblOrgan"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="cboOrgans" runat="server" CssClass="businesses-select" DataTextField="OrganDesc"
                    DataValueField="OrganId">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="td-edit-4">
                <asp:Label ID="lblRank" runat="server" Text="Chức vụ:" meta:resourcekey="lblRank"></asp:Label>
            </td>
            <td >
                <asp:DropDownList ID="cboRanks" runat="server" CssClass="businesses-select" DataTextField="RankDesc"
                    DataValueField="RankId">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="td-edit-4">
                <asp:Label ID="lblBirthDay" runat="server" Text="Ngày sinh:" meta:resourcekey="lblBirthDay"></asp:Label>
            </td>
            <td >
                <asp:TextBox ID="txtDatebirth" runat="server" CssClass="txtdatetime tbSearch"></asp:TextBox>&nbsp; (dd/mm/yyyy)
            </td>
        </tr>
        <tr>
            <td class="td-edit-4">
                <asp:Label ID="lblGender" runat="server" Text="Giới tính:" meta:resourcekey="lblGender"></asp:Label>
            </td>
            <td >
                <asp:DropDownList ID="cboGenders" runat="server" CssClass="businesses-select" DataTextField="GenderDesc" DataValueField="GenderId">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="td-edit-4">
                <asp:Label ID="lblPhone" runat="server" Text="Điện thoại:" meta:resourcekey="lblPhone"></asp:Label>
            </td>
            <td >
                <asp:TextBox ID="txtTelephone" runat="server" MaxLength="15" CssClass="tbSearch"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td-edit-4">
                <asp:Label ID="lblMobile" runat="server" Text="Mobile:" meta:resourcekey="lblMobile"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtMobile" runat="server" MaxLength="15" CssClass="tbSearch"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td-edit-4">
                <asp:Label ID="lblAddress" runat="server" Text="Địa chỉ:" meta:resourcekey="lblAddress"></asp:Label>
            </td>
            <td >
                <asp:TextBox ID="txtAddress" TextMode="MultiLine" CssClass="tbSearch" runat="server" MaxLength="1000"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td-edit-4">
                <asp:Label ID="lblComment" runat="server" Text="Ghi chú:" meta:resourcekey="lblComment"></asp:Label>
            </td>
            <td >
                <asp:TextBox ID="txtDesc" TextMode="MultiLine" CssClass="tbSearch" runat="server" MaxLength="1000"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td-edit-4">
                <asp:Label ID="lblAction" runat="server" Text="Trang mặc định:" meta:resourcekey="lblAction"></asp:Label>
            </td>
            <td >
                <asp:DropDownList ID="cboActions" runat="server" CssClass="businesses-select" DataTextField="ActionDesc" DataValueField="ActionId">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="td-edit-4">
            </td>
            <td >
                <asp:Button ID="btnUpdate" runat="server" Text="Lưu" OnClick="btnUpdate_Click" CssClass="btSearch button" meta:resourcekey="btnSave" />&nbsp;&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
