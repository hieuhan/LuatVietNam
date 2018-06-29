<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="NewsletterMembersEdit.aspx.cs" Inherits="Admin_NewsletterMembersEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <div id="dialogCustomers" title="Chọn khách hàng"><iframe id="ifCustomers" style="border: 0px; " width="98%" height="98%" src=""></iframe></div>
    <script type="text/javascript">
    $(document).ready(function () {
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(InitializeRequest);
        prm.add_endRequest(EndRequest);
     
        $('#popupCustomers').on('click', function () {
            $('#dialogCustomers').dialog('open');
        });

        $("#dialogCustomers").dialog({
            autoOpen: false,
            modal: true,
            width: 250,
            height: 420,
            //open: function(ev, ui){
            //    $('#ifCustomers').attr('src', 'popupCustomers.aspx?parent=');
            //},
            close:function(event,ui)
            {
                $(this).dialog('close');
            }
        });
    });

    function openPopupCustomers() {
        var name = 'popUp';
        var popUrl = 'popupCustomers.aspx?parent=NewsletterMembersEdit';
        var appearence = 'dependent=yes,menubar=no,resizable=yes,scrollbars=yes,' +
                                          'status=no,toolbar=no,titlebar=no,' +
                                          'left=' + (screen.width - 900) / 2 + ',top=30,width=900px,height=600px';
        var openWindow = window.open(popUrl, name, appearence);
        openWindow.focus();
        return false;
    }
    function updateCustomerId(CustomerId) {
        document.getElementById("<%=txtCustomerId.ClientID %>").value = CustomerId;
    }
    function updateCustomerName(CustomerName) {
        document.getElementById("<%=txtCrUserName.ClientID %>").value = CustomerName;
    }

    function InitializeRequest(sender, args) {
    }

    function EndRequest(sender, args) {
        
    }
    </script>
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td style="width:120px">
                <asp:Label ID="lblLanguage" runat="server" Text="Nhóm Email:"></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlNewsletterGroups" runat="server"
                    CssClass="userselect" DataTextField="NewsletterGroupDesc" 
                    DataValueField="NewsletterGroupId" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
         <tr>
            <td><asp:Label ID="lblCustomers" runat="server" 
                    Text="Khách hàng:" meta:resourcekey="lblCustomers"></asp:Label>
                <span class="icon-required"></span>
                <asp:RequiredFieldValidator ID="rfvCrUserName" runat="server" Display="Dynamic" 
                ErrorMessage="(*)" ValidationGroup="G1" 
                ControlToValidate="txtCrUserName" ForeColor="Red"></asp:RequiredFieldValidator>
             </td>
            <td>
                <asp:TextBox ID="txtCrUserName" runat="server" disabled CssClass="tukhoatimekiem" 
                    Width="240px"></asp:TextBox>
                <a id="popupCustomers2" title="Chọn khách hàng" href="javascript:openPopupCustomers()" >Chọn</a>
                <asp:HiddenField runat="server" ID="txtCustomerId" Value="0" />
                <br/>
                <asp:RequiredFieldValidator ID="rfvtxtCrUserName" runat="server" ErrorMessage="Vui lòng chọn khách hàng" ForeColor="Red" ControlToValidate="txtCrUserName" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width:120px"><asp:Label ID="lblEmail" runat="server" Text="Email:" meta:resourcekey="lblEmail"></asp:Label>
                <span class="icon-required"></span>
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" 
                    ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="(*)" 
                    Font-Bold="true" ForeColor="Red" ValidationGroup="G1"></asp:RequiredFieldValidator>
                </td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="tukhoatimekiem" Width="240px"></asp:TextBox>
                <br/>
                <asp:RequiredFieldValidator ID="rfvtxtEmail" runat="server" ErrorMessage="Địa chỉ Email" ForeColor="Red" ControlToValidate="txtEmail" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator><br/>
                <asp:RegularExpressionValidator ID="revtxtEmail" runat="server" 
                    ControlToValidate="txtEmail" 
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    ErrorMessage="Địa chỉ Email không hợp lệ."
                    ForeColor="Red"
                    ValidationGroup="G1" />
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblNewsletterMemberStatus" runat="server" Text="Trạng thái:" meta:resourcekey="lblNewsletterMemberStatus"></asp:Label>
                </td>
            <td><asp:DropDownList ID="ddlNewsletterMemberStatus" runat="server" CssClass="userselect"  Width="250px"
                    DataTextField="NewsletterMemberStatusDesc" DataValueField="NewsletterMemberStatusId"></asp:DropDownList>
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
                    Text="Lưu thông tin" ValidationGroup="G1" meta:resourcekey="btnSave" 
            onclick="btnSave_Click"> </asp:LinkButton>
    </div>
</asp:Content>

