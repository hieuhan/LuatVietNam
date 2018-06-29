<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="AdvertPositionsEdit.aspx.cs" Inherits="Admin_AdvertPositionsEdit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
<script type="text/javascript">
    $(document).ready(function () {
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(InitializeRequest);
        prm.add_endRequest(EndRequest);
        $(".clDate").datepicker({ dateFormat: 'dd/mm/yy' });
    });
    function InitializeRequest(sender, args) {
    }

    function EndRequest(sender, args) {
        // after update occur on UpdatePanel re-init the DatePicker
        $(".clDate").datepicker({ dateFormat: 'dd/mm/yy' });
    }
    </script>

    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td style="width:90px">
            </td>
            <td>
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width:90px">
                <asp:Label ID="lblSite" runat="server" Text="Site:" meta:resourcekey="lblSite"></asp:Label>
                </td>
            <td><asp:DropDownList ID="ddlSite" runat="server" DataTextField="SiteDesc" DataValueField="SiteId"
                     Width="455px" AutoPostBack="True" 
                    onselectedindexchanged="ddlSite_SelectedIndexChanged">
                </asp:DropDownList>
                </td>
        </tr>
         <tr>
            <td  style="width:90px">
                <asp:Label ID="lblAppType" runat="server" Text="Loại ứng dụng:" meta:resourcekey="lblAppType"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlAppType" runat="server" DataTextField="ApplicationTypeDesc" 
                    DataValueField="ApplicationTypeId" Width="455px">
                </asp:DropDownList>
                
            </td>
        </tr>
          <tr id="trCate" runat="server" visible="false">
            <td style="width:90px">
                <asp:Label ID="lblCategory" runat="server" 
                    meta:resourcekey="lblCategory" Text="Chuyên mục:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlParentCategory" runat="server" 
                    DataTextField="CategoryDesc" DataValueField="CategoryId" Width="455px">
                </asp:DropDownList>
            </td>
        </tr> 
        <tr>
            <td><asp:Label ID="lblName" runat="server" Text="Tên:" meta:resourcekey="lblName"></asp:Label>
                <span class="icon-required"></span>
                <asp:RequiredFieldValidator ID="rfvName" runat="server" Display="Dynamic" 
                ErrorMessage="(*)" ValidationGroup="G1" 
                ControlToValidate="txtName" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            <td>
                <asp:TextBox ID="txtName" runat="server" Width="450px"></asp:TextBox>
                <br /><asp:RequiredFieldValidator ID="rfvtxtName" runat="server" ErrorMessage="Tên vị trí quảng cáo" ForeColor="Red" ControlToValidate="txtName" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblDesc" runat="server" Text="Mô tả:" meta:resourcekey="lblDesc"></asp:Label>
                </td>
            <td><asp:TextBox ID="txtDesc" runat="server" Width="450px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblType" runat="server" Text="Loại:" meta:resourcekey="lblType"></asp:Label>
                </td>
            <td><asp:DropDownList ID="ddlType" runat="server" 
                    DataTextField="AdvertDisplayTypeDesc" DataValueField="AdvertDisplayTypeId" 
                    Width="455px">
                </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblWidth" runat="server" Text="Rộng:" meta:resourcekey="lblWidth"></asp:Label>
                </td>
            <td><asp:TextBox ID="txtWidth" runat="server" Width="450px"></asp:TextBox>
               </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblHeight" runat="server" Text="Cao:" meta:resourcekey="lblHeight"></asp:Label>
                </td>
            <td><asp:TextBox ID="txtHeight" runat="server" Width="450px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblOverWidth" runat="server" Text="Rộng overflow:" meta:resourcekey="lblOverWidth"></asp:Label>
                </td>
            <td><asp:TextBox ID="txtOverWidth" runat="server" Width="450px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblOverHeight" runat="server" Text="Cao overflow:" meta:resourcekey="lblOverHeight"></asp:Label>
                </td>
            <td><asp:TextBox ID="txtOverHeight" runat="server" Width="450px"></asp:TextBox>
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
                    Text="Lưu thông tin" meta:resourcekey="btnSave" ValidationGroup="G1"
            onclick="btnSave_Click">
                        </asp:LinkButton>
    </div>
</asp:Content>

