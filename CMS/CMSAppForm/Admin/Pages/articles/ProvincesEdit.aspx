<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="ProvincesEdit.aspx.cs" Inherits="Admin_Pages_articles_ProvincesEdit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td><asp:Label ID="lblCountries" runat="server" Text="Quốc gia:" meta:resourcekey="lblCountries"></asp:Label>
                </td>
            <td>
            <asp:DropDownList ID="ddlCountries" runat="server" AutoPostBack="True" CssClass="userselect"
                        DataTextField="CountryDesc" DataValueField="CountryId" Width="310px">
                    </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td style="width:120px"><asp:Label ID="lblProvinceName" runat="server" Text="Tên:" meta:resourcekey="lblProvinceName"></asp:Label>
                <span class="icon-required"></span>
                <asp:RequiredFieldValidator ID="rfvProvinceName" runat="server" Display="Dynamic" 
                ErrorMessage="(*)" ValidationGroup="G1" 
                ControlToValidate="txtProvinceName" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            <td>
                <asp:TextBox ID="txtProvinceName" runat="server" CssClass="tukhoatimekiem" 
                    Width="300px"></asp:TextBox>
                <br /><asp:RequiredFieldValidator ID="rfvtxtProvinceName" runat="server" ErrorMessage="Tên thành phố" ForeColor="Red" ControlToValidate="txtProvinceName" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblProvinceDesc" runat="server" Text="Mô tả:" meta:resourcekey="lblProvinceDesc"></asp:Label>
                <span class="icon-required"></span>
                <asp:RequiredFieldValidator ID="rfvProvinceDesc" runat="server" Display="Dynamic" 
                ErrorMessage="(*)" ValidationGroup="G1" 
                ControlToValidate="txtProvinceDesc" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            <td><asp:TextBox ID="txtProvinceDesc" runat="server" CssClass="tukhoatimekiem" 
                    Width="300px" Rows="3" TextMode="MultiLine"></asp:TextBox>
                <br /><asp:RequiredFieldValidator ID="rfvtxtProvinceDesc" runat="server" ErrorMessage="Mô tả" ForeColor="Red" ControlToValidate="txtProvinceDesc" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblDisplayOrder" runat="server" Text="Thứ tự hiển thị" meta:resourcekey="lblDisplayOrder"></asp:Label>
                </td>
            <td><asp:TextBox ID="txtDisplayOrder" runat="server" CssClass="tukhoatimekiem" 
                    Width="300px"></asp:TextBox>
                <a class="help-lnk" href="javascript:void(0)" title="Thứ tự hiển thị có thể để trống, giá trị mặc định bằng 0" ><span class="aui-icon aui-icon-small aui-iconfont-help">?</span></a>
                <br />
                <asp:RangeValidator ID="rvtxtDisplayOrder" runat="server" 
                        ErrorMessage="Thứ tự hiển thị hợp lệ trong khoảng 0 -  30000."
                        ControlToValidate="txtDisplayOrder"
                        MinimumValue="0"
                        MaximumValue="30000"
                        ValidationGroup="G1"
                        ForeColor="Red"
                        SetFocusOnError="true"
                        Type="Integer">
                </asp:RangeValidator>
                </td>
        </tr>
        
    </table><br />
    <div style="text-align:center">
        <asp:CheckBox ID="cblAddOther" runat="server" Text="Thêm tiếp dữ liệu khác" Checked="false" ToolTip="Khi tick chọn sẽ xóa form hiện tại để thêm dữ liệu khác"  />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom" 
                    Text="Lưu thông tin" meta:resourcekey="btnSave" ValidationGroup="G1"
            onclick="btnSave_Click">
                        </asp:LinkButton>
    </div>
</asp:Content>



