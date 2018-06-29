<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master"
    AutoEventWireup="true" CodeFile="MenuItemsEdit.aspx.cs" Inherits="Admin_MenuItemsEdit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" runat="Server">
<script type="text/javascript">
    function SelectName() {
        popup = window.open("MediasSelect.aspx?txtDemo=m_contentBody_txtIcon&ImgDemo=m_contentBody_imgDemo", "Popup", "width=650,height=550,scrollbars=1");
        popup.focus();
        return false
    }
</script>
<div style="width:auto; height:420px; overflow:auto; ">
    <table cellpadding="3" cellspacing="0" style="width: 100%; font-weight: lighter">
        <tr>
            <td style="width: 120px">
                <asp:Label ID="lblParentCategory" runat="server" meta:resourcekey="lblParentCategory"
                    Text="Menu cha:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlParentCategory" runat="server" DataTextField="ItemDesc"
                    DataValueField="MenuItemId" Width="355px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCategoryName" runat="server" Text="Tên:" meta:resourcekey="lblCategoryName"></asp:Label>
                <span class="icon-required"></span>
                <asp:RequiredFieldValidator ID="rfvCategoryName" runat="server" Display="Dynamic" 
                ErrorMessage="(*)" ValidationGroup="G1" 
                ControlToValidate="txtCategoryName" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:TextBox ID="txtCategoryName" runat="server" Width="350px"></asp:TextBox>
                 <br /><asp:RequiredFieldValidator ID="rfvtxtCategoryName" runat="server" ErrorMessage="Tên MenuItem" ForeColor="Red" ControlToValidate="txtCategoryName" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCategoryDesc" runat="server" Text="Mô tả:" meta:resourcekey="lblCategoryDesc"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtCategoryDesc" runat="server" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr id="Tr1" runat="server" visible="true">
            <td>
                <asp:Label ID="lblUrl" runat="server" Text="Url:" meta:resourcekey="lblUrl"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtUrl" runat="server" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblDisplayOrder" runat="server" Text="Thứ tự hiển thị:" meta:resourcekey="lblDisplayOrder"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDisplayOrder" runat="server" Width="350px"></asp:TextBox>
                <a class="help-lnk" href="javascript:void(0)" title="Thứ tự hiển thị có thể để trống, giá trị mặc định bằng 0" ><span class="aui-icon aui-icon-small aui-iconfont-help">?</span></a>
                <asp:RangeValidator ID="rvDisplayOrder" runat="server" 
                        ErrorMessage="Vui lòng nhập thứ tự hiển thị là số."
                        ControlToValidate="txtDisplayOrder"
                        MinimumValue="0"
                        MaximumValue="2000000"
                        ValidationGroup="G1"
                        ForeColor="Red"
                        SetFocusOnError="true"
                        Type="Integer">
                </asp:RangeValidator><br/>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblImage" runat="server" Text="Ảnh minh họa:" meta:resourcekey="lblImage"></asp:Label>
                &nbsp;</td>
            <td>
                <a title="Chọn ảnh" href="#" onclick="SelectName()">
                <img alt="txtIcon" id="imgDemo" runat="server" class="ImageSelectPath1" src="../../../Images/transparent.png" style="width: 60px;height: 40px;border: 1px solid black;" /></a>
                <asp:TextBox CssClass="HiddenInput" ID="txtIcon" runat="server" Text=""></asp:TextBox>
                &nbsp;
                <asp:CheckBox ID="cbDeleteImages" runat="server" Text="Xóa icon" meta:resourcekey="cbDeleteImages" />
            </td>
        </tr>
        <tr id="Tr2" runat="server">
            <td>
                <asp:Label ID="lblMetaTitle" runat="server" Text="Tiêu đề SEO:" meta:resourcekey="lblMetaTitle"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtMetaTitle" runat="server" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr id="Tr3" runat="server">
            <td>
                <asp:Label ID="lblMetaDesc" runat="server" Text="Mô tả SEO:" meta:resourcekey="lblMetaDesc"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtMetaDesc" TextMode="MultiLine" runat="server" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr id="Tr4" runat="server">
            <td>
                <asp:Label ID="lblMetaKeyword" runat="server" Text="Từ khóa SEO:" meta:resourcekey="lblMetaKeyword"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtMetaKeyword" runat="server" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblCanonicalTag" runat="server" Text="Thẻ Canonical:" meta:resourcekey="lblCanonicalTag"></asp:Label>
                </td>
            <td><asp:TextBox ID="txtCanonicalTag" runat="server" CssClass="tukhoatimekiem" 
                    Width="350px" TextMode="MultiLine"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblH1Tag" runat="server" Text="Nội dung thẻ H1:" meta:resourcekey="lblH1Tag"></asp:Label>
                </td>
            <td><asp:TextBox ID="txtH1Tag" runat="server" CssClass="tukhoatimekiem" 
                    Width="350px" TextMode="MultiLine"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblSeoFooter" runat="server" Text="SEO footer:" meta:resourcekey="lblSeoFooter"></asp:Label>
                </td>
            <td><asp:TextBox ID="txtSeoFooter" runat="server" CssClass="tukhoatimekiem" 
                    Width="350px" TextMode="MultiLine"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblReviewStatus" runat="server" Text="Trạng thái:" meta:resourcekey="lblReviewStatus"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlReviewStatus" runat="server" DataTextField="ReviewStatusDesc"
                    DataValueField="ReviewStatusId">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    </div>
    <div style="text-align: center">
        <hr />
        <div class="clear5px"></div>
        <asp:CheckBox ID="cblAddOther" runat="server" Text="Thêm tiếp dữ liệu khác" Checked="false" ToolTip="Khi tick chọn sẽ xóa form hiện tại để thêm dữ liệu khác"  />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom" Text="Lưu thông tin"
            meta:resourcekey="btnSave" ValidationGroup="G1" OnClick="btnSave_Click">
        </asp:LinkButton></div>
</asp:Content>
