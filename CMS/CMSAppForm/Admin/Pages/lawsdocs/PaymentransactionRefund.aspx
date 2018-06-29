<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="PaymentransactionRefund.aspx.cs" Inherits="Admin_SignersEdit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td style="width:160px"><asp:Label ID="lblSignerName" runat="server" Text="Lý do hoàn tiền:" meta:resourcekey="lblSignerName"></asp:Label>
            <span class="icon-required"></span>
                </td>
            <td>
                <asp:TextBox ID="txtSignerName" runat="server" CssClass="tukhoatimekiem" Width="240px"></asp:TextBox><br/>
                <asp:RequiredFieldValidator ID="rfvtxtSignerName" runat="server" ErrorMessage="Vui lòng nhập lý do" ForeColor="Red" ControlToValidate="txtSignerName" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
                </td>
        </tr>
       
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
     <div style="text-align:center; font-size:16px; margin: 10px 0; padding: 10px;" id="divResult" runat="server">
         </div>
    <div style="text-align:center">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom" 
                    Text="Thực hiện" meta:resourcekey="btnSave"  ValidationGroup="G1"
            onclick="btnSave_Click">
                        </asp:LinkButton>
    </div>
</asp:Content>

