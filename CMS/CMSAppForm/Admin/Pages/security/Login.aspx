<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPagePublic.master" 
CodeFile="Login.aspx.cs" Inherits="ICSoft.AppForm.Admin.PageLogin"  Title="Đăng nhập hệ thống" %>
<%@ Import Namespace="ICSoft.CMSLib" %>
<asp:Content ID="contentAccountType" runat="server" ContentPlaceHolderID="m_contentBody">

        <div class="khungcontenpading">
        <asp:ValidationSummary ID="m_validationSummary" runat="server" CssClass="ValidatorSummary" />
        <asp:Label ID="lblErrorMessage" runat="server" CssClass="ValidatorSummary" />
        <div align="center">
            <br />
            <br />
            <br />
            <br />
            <br />
            <table border="0" width="500" cellspacing="0" cellpadding="0" bgcolor="#f4f8f7" id="table82" align="center">
                <tr>
                    <td width="11" height="16" background="<%=CmsConstants.PRJ_ROOT %>images/top-left.gif">
                    </td>
                    <td height="16" background="<%=CmsConstants.PRJ_ROOT %>images/top-center.gif">
                    </td>
                    <td width="11" height="16" background="<%=CmsConstants.PRJ_ROOT %>images/top-right.gif">
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <div align="center">
                            <asp:Login ID="m_login" runat="server" class="forumline" BorderColor="Transparent"
                                BorderPadding="4" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana"
                                Font-Size="12px" LoginButtonText=" Đăng nhập " 
                                TitleText="ĐĂNG NHẬP HỆ THỐNG" TitleTextStyle-Font-Size="12px"
                                TitleTextStyle-Font-Names="Arial" TitleTextStyle-Font-Bold="true" InstructionText=""
                                InstructionTextStyle-ForeColor="red" OnAuthenticate="m_login_Authenticate" UserNameLabelText="Tên đăng nhập"
                                PasswordLabelText="Mật khẩu" RememberMeText="Nhớ mật khẩu" DisplayRememberMe="true"
                                FailureText="Sai tên hoặc mật khẩu đăng nhập hệ thống!" Width="461" Height="220"
                                VisibleWhenLoggedIn="False" RememberMeSet="false" meta:resourcekey="m_login">
                                <LoginButtonStyle  CssClass="button" />
                                <TextBoxStyle Font-Size="0.8em" />
                                <TitleTextStyle BackColor="Transparent" Font-Bold="True" Font-Size="1.2em" ForeColor="Black"
                                    Font-Names="Arial" />
                                <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
                            </asp:Login>
                        </div>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td width="11" height="16" background="<%=CmsConstants.PRJ_ROOT %>images/bottom-left.gif">
                    </td>
                    <td height="16" background="<%=CmsConstants.PRJ_ROOT %>images/bottom-center.gif">
                    </td>
                    <td width="11" height="16" background="<%=CmsConstants.PRJ_ROOT %>images/bottom-right.gif">
                    </td>
                </tr>
            </table>
        </div>
</div>

</asp:Content>
