<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="NewsletterEmailsEdit.aspx.cs" Inherits="Admin_NewsletterEmailsEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td width="100px">Site:
                </td>
            <td><asp:DropDownList ID="ddlSite" runat="server" DataTextField="SiteDesc" 
                            DataValueField="SiteId" Width="250px" CssClass="userselect" 
                    AutoPostBack="True" onselectedindexchanged="ddlSite_SelectedIndexChanged">
                        </asp:DropDownList>
                </td>
        </tr>
         <tr>
            <td width="100px"><asp:Label ID="lblCustomerId" runat="server" 
                    Text="ID khách hàng:" meta:resourcekey="lblCustomerId"></asp:Label><span class="icon-required"></span>

                </td>
            <td>
                <asp:TextBox ID="txtCustomerId" runat="server" CssClass="tukhoatimekiem" 
                    Width="250px"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="rfvCustomerId" 
                    runat="server" ControlToValidate="txtCustomerId" Display="Dynamic" 
                    ErrorMessage="Bạn cần nhập Id khách hàng" ForeColor="Red" Font-Bold="true" ValidationGroup="G1"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revtxtCustomerId" runat="server" ForeColor="Red" ErrorMessage="Id khách hàng chỉ chấp nhận kiểu số" Display="Dynamic" ValidationExpression="\d+" ControlToValidate="txtCustomerId"></asp:RegularExpressionValidator>

                </td>
        </tr>
       <tr>
            <td ><asp:Label ID="lblEmail" runat="server" Text="Email:" meta:resourcekey="lblEmail"></asp:Label><span class="icon-required"></span>
                </td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="tukhoatimekiem" Width="250px"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" 
                    ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="Bạn cần nhập Email" 
                    Font-Bold="true" ForeColor="Red" ValidationGroup="G1"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="validateEmail"    
  runat="server" ErrorMessage=" Email bạn nhập chưa đúng định dạng (vd: abc@gmail.com)"
  ControlToValidate="txtEmail"  ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" />
                </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblIsReceiveNews" runat="server" 
                    meta:resourcekey="lblIsReceiveNews" Text="Trạng thái:"></asp:Label>
            </td>
            <td>
                <asp:RadioButtonList ID="rbtIsReceiveNews" runat="server" 
                    RepeatDirection="Horizontal">
                <asp:ListItem Value="0" Selected="True">Không nhận</asp:ListItem>
                <asp:ListItem Value="1">Có nhận</asp:ListItem>
                </asp:RadioButtonList>
                </td>
        </tr>
       <tr>
            <td >
                Nhóm:</td>
            <td>
                <asp:CheckBoxList ID="chkGroup" DataTextField="NewsletterGroupName" DataValueField="NewsletterGroupId" RepeatDirection="Horizontal" RepeatColumns="3" runat="server">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td >
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>       
       
    </table>
    <div style="background-color: #FFFFFF; bottom: 0px;  padding: 8px;  position: fixed;   right: 1px;   text-align: right;  width: 100%;">
<asp:CheckBox ID="cblAddOther" runat="server" Text="Thêm tiếp dữ liệu khác" Checked="false" ToolTip="Khi tick chọn sẽ xóa form hiện tại để thêm dữ liệu khác"  />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom" 
                    Text="Lưu thông tin" ValidationGroup="G1" meta:resourcekey="btnSave" 
            onclick="btnSave_Click"> </asp:LinkButton></div>
</asp:Content>

