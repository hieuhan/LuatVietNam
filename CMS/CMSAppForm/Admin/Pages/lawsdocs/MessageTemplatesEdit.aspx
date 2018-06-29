<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="MessageTemplatesEdit.aspx.cs" Inherits="Admin_MessageTemplatesEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td style="width:110px">
                <asp:Label ID="lblSendMethods" runat="server" meta:resourcekey="lblSendMethods" 
                    Text="Hình thức gửi:"></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlSendMethods" runat="server" 
                    CssClass="userselect" DataTextField="SendMethodDesc" 
                    DataValueField="SendMethodId" Width="250px">
                </asp:DropDownList>
                Site: <asp:DropDownList ID="ddlSite" runat="server" DataTextField="SiteDesc" 
                            DataValueField="SiteId" Width="250px" CssClass="userselect">
                        </asp:DropDownList>
                </td>
        </tr>
         <tr>
            <td><asp:Label ID="lblMessageName" runat="server" 
                    Text="Tên tin nhắn:" meta:resourcekey="lblMessageName"></asp:Label><span class="icon-required"></span>
                   
                </td>
            <td>
                <asp:TextBox ID="txtMessageName" runat="server" CssClass="tukhoatimekiem" 
                    Width="97%"></asp:TextBox><br />
 <asp:RequiredFieldValidator ID="rfvMessageName" 
                    runat="server" ControlToValidate="txtMessageName" Display="Dynamic" 
                    ErrorMessage="Bạn cần nhập tên" ForeColor="Red" Font-Bold="true" ValidationGroup="G1"></asp:RequiredFieldValidator>

                </td>
        </tr>
       <tr>
            <td ><asp:Label ID="lblSendFrom" runat="server" Text="Gửi từ:" meta:resourcekey="lblSendFrom"></asp:Label><span class="icon-required"></span>
                
                </td>
            <td>
                <asp:TextBox ID="txtSendFrom" runat="server" CssClass="tukhoatimekiem" Width="97%"></asp:TextBox>
                <br /><asp:RequiredFieldValidator ID="rfvSendFrom" runat="server" 
                    ControlToValidate="txtSendFrom" Display="Dynamic" ErrorMessage="Bạn cần nhập nơi gửi" 
                    Font-Bold="true" ForeColor="Red" ValidationGroup="G1"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblTitle" runat="server" 
                    Text="Tiêu đề:" meta:resourcekey="lblTitle"></asp:Label><span class="icon-required"></span>
            </td>
            <td>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="tukhoatimekiem" 
                    Width="97%" ></asp:TextBox>
                <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtTitle" Display="Dynamic" ErrorMessage="Bạn cần nhập tiêu đề mẫu tin" 
                    Font-Bold="true" ForeColor="Red" ValidationGroup="G1"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td >
                <asp:Label ID="lblMsgContent" runat="server" Text="Nội dung:" meta:resourcekey="lblMsgContent"></asp:Label><span class="icon-required"></span>
            </td>
            <td>
                <CKEditor:CKEditorControl ID="txtMsgContent" runat="server" 
                                BasePath="~/Integrated/ckeditor/" Height="200px" Width="98%"></CKEditor:CKEditorControl>
                <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtMsgContent" Display="Dynamic" ErrorMessage="Bạn cần nhập nội dung mẫu tin" 
                    Font-Bold="true" ForeColor="Red" ValidationGroup="G1"></asp:RequiredFieldValidator>
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

