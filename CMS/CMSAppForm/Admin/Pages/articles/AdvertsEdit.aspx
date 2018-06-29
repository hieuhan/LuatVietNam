<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="AdvertsEdit.aspx.cs" ValidateRequest="false" Inherits="Admin_AdvertsEdit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
<script type="text/javascript">
    function SelectName() {
        popup = window.open("MediasSelect.aspx?SetMediaDomain=1&txtDemo=m_contentBody_txtIcon&ImgDemo=m_contentBody_imgDemo", "Popup", "width=650,height=550,scrollbars=1");
        popup.focus();
        return false
    }
    function SelectName2() {
        popup = window.open("MediasSelect.aspx?SetMediaDomain=1&txtDemo=m_contentBody_txtIcon2&ImgDemo=m_contentBody_imgDemo2", "Popup", "width=650,height=550,scrollbars=1");
        popup.focus();
        return false
    }

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

    <div style="width:auto; height:410px; overflow:auto;">
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
            <td>
                <asp:DropDownList ID="ddlSite" runat="server" DataTextField="SiteDesc" DataValueField="SiteId"
                     Width="455px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width:90px"><asp:Label ID="lblPartner" runat="server" Text="Đối tác:" meta:resourcekey="lblPartner"></asp:Label>
                </td>
            <td><asp:DropDownList ID="ddlPartner" runat="server" DataTextField="PartnerDesc" 
                    DataValueField="PartnerId" Width="455px">
                </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblName" runat="server" Text="Tên:" meta:resourcekey="lblName"></asp:Label>
                <span class="icon-required"></span>
                <asp:RequiredFieldValidator ID="rfvtName" runat="server" Display="Dynamic" 
                ErrorMessage="(*)" ValidationGroup="G1" 
                ControlToValidate="txtName" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            <td>
                <asp:TextBox ID="txtName" runat="server" Width="450px"></asp:TextBox>
                <br /><asp:RequiredFieldValidator ID="rfvtxtName" runat="server" ErrorMessage="Tên quảng cáo" ForeColor="Red" ControlToValidate="txtName" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblDesc" runat="server" Text="Mô tả:" meta:resourcekey="lblDesc"></asp:Label>
                </td>
            <td><asp:TextBox ID="txtDesc" runat="server" Width="450px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblUrl" runat="server" Text="Url:" meta:resourcekey="lblUrl"></asp:Label>
                </td>
            <td><asp:TextBox ID="txtUrl" runat="server" Width="450px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblType" runat="server" Text="Loại:" meta:resourcekey="lblType"></asp:Label>
                </td>
            <td><asp:DropDownList ID="ddlType" runat="server" 
                    DataTextField="AdvertContentTypeDesc" DataValueField="AdvertContentTypeId" 
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
            <td><asp:Label ID="lblImage" runat="server" Text="Ảnh minh họa:" meta:resourcekey="lblImage"></asp:Label>
                &nbsp;</td>
            <td>
                <a href="#" title="Chọn ảnh" onclick="SelectName()">
                <img alt="txtIcon" id="imgDemo" runat="server" class="ImageSelectPath1" src="../../../Images/transparent.png" style="width: 60px;height: 40px;border: 1px solid black;" /></a>
                <asp:TextBox CssClass="HiddenInput" ID="txtIcon" runat="server" Text=""></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblImageHover" runat="server" Text="Image Hover"></asp:Label>
                &nbsp;
                <a href="#" title="Chọn ảnh" onclick="SelectName2()">
                <img alt="txtIcon2" id="imgDemo2" runat="server" class="ImageSelectPath1" src="../../../Images/transparent.png" style="width: 60px;height: 40px;border: 1px solid black;" /></a>
                <asp:TextBox CssClass="HiddenInput" ID="txtIcon2" runat="server" Text=""></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblScript" runat="server" Text="Script:" meta:resourcekey="lblScript"></asp:Label>
                </td>
            <td><asp:TextBox ID="txtScript" runat="server" TextMode="MultiLine" Height="150px" Width="450px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblStartTime" runat="server" Text="Bắt đầu:" meta:resourcekey="lblStartTime"></asp:Label>
                </td>
            <td><asp:TextBox ID="txtStartTime" runat="server" Width="150px" CssClass="clDate"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblEndTime" runat="server" Text="Kết thúc:" meta:resourcekey="lblEndTime"></asp:Label>
                </td>
            <td><asp:TextBox ID="txtEndTime" runat="server" Width="150px" CssClass="clDate"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblStatus" runat="server" Text="Trạng thái:" meta:resourcekey="lblStatus"></asp:Label>
                </td>
            <td><asp:DropDownList ID="ddlAdvertStatus" runat="server"  Width="150px"
                    DataTextField="AdvertStatusDesc" DataValueField="AdvertStatusId"></asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    </div>
    <hr style="width:100%;" /><br />
    <div style="text-align:center">
        <asp:CheckBox ID="cblAddOther" runat="server" Text="Thêm tiếp dữ liệu khác" Checked="false" ToolTip="Khi tick chọn sẽ xóa form hiện tại để thêm dữ liệu khác"  />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom" 
                    Text="Lưu thông tin" meta:resourcekey="btnSave" ValidationGroup="G1"
            onclick="btnSave_Click">
                        </asp:LinkButton>
    </div>
</asp:Content>

