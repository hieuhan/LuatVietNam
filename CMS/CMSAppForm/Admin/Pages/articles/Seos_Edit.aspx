<%@ Page Language="C#" MasterPageFile="~/admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="Seos_Edit.aspx.cs" Inherits="admin_pages_SeosEdit" Title="" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    
    <script src="../../Scripts/jquery.plugin.min.js"></script>
    <script src="../../Scripts/jquery.maxlength.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $('textarea[id*="txtMetaTitle"]').maxlength({ feedbackText: 'Tổng số ký tự {c} (Tối đa {m}, khuyến nghị: 50-60)', overflowText: 'Tổng số ký tự {c} (Tối đa {m}, khuyến nghị: 50-60)', max: 80, truncate: false });
            $('textarea[id*="txtMetaDesc"]').maxlength({ feedbackText: 'Tổng số ký tự {c}  (Tối đa {m}, khuyến nghị: dưới 200)', overflowText: 'Tổng số ký tự {c} (Tối đa {m}, khuyến nghị: dưới 200)', max: 300, truncate: false });
            $('textarea[id*="txtH1Tag"]').maxlength({ feedbackText: 'Tổng số ký tự {c}  (Tối đa {m}), khuyến nghị: 50-60', overflowText: 'Tổng số ký tự {c} (Tối đa {m}, khuyến nghị: 50-60)', max: 80, truncate: false });

        });
    </script>

    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td style="width:120px"><asp:Label ID="lblSite" runat="server" Text="Site "></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlSite" runat="server" CssClass="userselect"
                    Width="95%" DataTextField="SiteName" DataValueField="SiteId">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            
            <td style="width:120px"><asp:Label ID="lblSeoName" runat="server" Text="Tên "></asp:Label><span class="icon-required"></span>
                </td>
            <td>
                <asp:TextBox ID="txtSeoName" runat="server" CssClass="tukhoatimekiem" 
                    Width="95%" TextMode="SingleLine"></asp:TextBox><br />
<asp:RequiredFieldValidator ID="rfvSeoName" runat="server" ErrorMessage="Bạn cần nhập tên Seo" ForeColor="Red" 
    ControlToValidate="txtSeoName" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>

            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblUrl" runat="server" Text="Url " ></asp:Label><span class="icon-required"></span></td>
            <td><asp:TextBox ID="txtUrl" runat="server" CssClass="tukhoatimekiem" 
                    Width="95%" TextMode="SingleLine"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfvUrl" runat="server" ValidationGroup="G1"
                    ControlToValidate="txtUrl" Text="Nhập Url Seo" ForeColor="Red" ErrorMessage="Bạn cần nhập Url Seo" ></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblMetaTitle" runat="server" Text="Tiêu đề Seo" ></asp:Label>
                <span class="icon-required"></span>
                </td>
            <td><asp:TextBox ID="txtMetaTitle" runat="server" CssClass="tukhoatimekiem" 
                    Width="95%" Rows="1" TextMode="MultiLine"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="rfvtxtMetaTitle" runat="server" ValidationGroup="G1"
                    ControlToValidate="txtMetaTitle" Text="Nhập Tiêu đề Seo" ForeColor="Red" ErrorMessage="Bạn cần nhập tiêu đề Seo"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblMetaDesc" runat="server" Text="Mô tả Seo ">
                </asp:Label><span class="icon-required"></span>
                </td>
            <td><asp:TextBox ID="txtMetaDesc" runat="server" CssClass="tukhoatimekiem" 
                    Width="95%" Rows="2" TextMode="MultiLine"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="rfvtxtMetaDesc" runat="server" ValidationGroup="G1"
                    ControlToValidate="txtMetaDesc" Text="Nhập Mô tả Seo" ForeColor="Red" ErrorMessage="Bạn cần nhập mô tả Seo"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblMetaKeyword" runat="server" Text="Từ khóa Seo " ></asp:Label>
                <span class="icon-required"></span>
                </td>
            <td><asp:TextBox ID="txtMetaKeyword" runat="server" CssClass="tukhoatimekiem" 
                    Width="95%" Rows="2" TextMode="MultiLine"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="rfvtxtMetaKeyword" runat="server" ValidationGroup="G1"
                    ControlToValidate="txtMetaKeyword" Text="Nhập Từ khóa Seo" ForeColor="Red" ErrorMessage="Bạn cần nhập từ khóa Seo" ></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblCanonicalTag" runat="server" Text="Thẻ Canonical:" ></asp:Label>
                </td>
            <td><asp:TextBox ID="txtCanonicalTag" runat="server" CssClass="tukhoatimekiem" 
                    Width="95%" Rows="2" TextMode="MultiLine"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblH1Tag" runat="server" Text="Nội dung thẻ H1:" ></asp:Label>
                </td>
            <td><asp:TextBox ID="txtH1Tag" runat="server" CssClass="tukhoatimekiem" 
                    Width="95%" Rows="2" TextMode="MultiLine"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblSeoFooter" runat="server" Text="SEO footer:" ></asp:Label>
                </td>
            <td><asp:TextBox ID="txtSeoFooter" runat="server" CssClass="tukhoatimekiem" 
                    Width="95%" Rows="2" TextMode="MultiLine"></asp:TextBox>
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
        <asp:CheckBox ID="cblAddOther" runat="server" Text="Thêm tiếp dữ liệu khác" Checked="false" ToolTip="Khi tick chọn xóa form hiện tại để thêm dữ liệu khác"  />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom" Text="Lưu thông tin" onclick="btnSave_Click" ValidationGroup="G1">
        </asp:LinkButton></div>
</asp:Content>

