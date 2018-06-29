<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master"
    AutoEventWireup="true" CodeFile="CategoriesEdit.aspx.cs" Inherits="Admin_CategoriesEdit" %>

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
            <td  style="width: 120px">
                <asp:Label ID="lblSite" runat="server" Text="Site:" meta:resourcekey="lblSite"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlSite" runat="server" DataTextField="SiteDesc" DataValueField="SiteId"
                    Width="355px" AutoPostBack="True" 
                    onselectedindexchanged="ddlSite_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblDataType" runat="server" Text="Loại dữ liệu:" meta:resourcekey="lblDataType"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlDataType" runat="server" DataTextField="DataTypeDesc" DataValueField="DataTypeId"
                    Width="355px" AutoPostBack="True" 
                    onselectedindexchanged="ddlDataType_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;
            </td>
        </tr>
        <tr id="trLanguage" runat="server" visible="false">
            <td style="width: 120px">
                <asp:Label ID="lblLanguage" runat="server" Text="Ngôn ngữ:" meta:resourcekey="lblLanguage"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlLanguage" runat="server" DataTextField="LanguageDesc" DataValueField="LanguageId"
                    Width="128px" AutoPostBack="True" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;&nbsp;
                <asp:Label ID="lblAppType" runat="server" Text="Loại ứng dụng:" meta:resourcekey="lblAppType"></asp:Label>
                &nbsp;&nbsp;
                <asp:DropDownList ID="ddlAppType" runat="server" DataTextField="ApplicationTypeDesc"
                    DataValueField="ApplicationTypeId" Width="120px" AutoPostBack="True" OnSelectedIndexChanged="ddlAppType_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblParentCategory" runat="server" Text="Chuyên mục cha:" meta:resourcekey="lblParentCategory"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlParentCategory" runat="server" DataTextField="CategoryDesc"
                    DataValueField="CategoryId" Width="355px">
                </asp:DropDownList>
                <br />
                <asp:CheckBox ID="chkShowAllCate" runat="server" Text="Hiển thị tất cả chuyên mục của site"
                    oncheckedchanged="chkShowAllCate_CheckedChanged" AutoPostBack="True" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCategoryName" runat="server" Text="Tên chuyên mục:" meta:resourcekey="lblCategoryName"></asp:Label>
                <span class="icon-required"></span>
            </td>
            <td>
                    <asp:TextBox ID="txtCategoryName" runat="server" Width="350px"></asp:TextBox><br />
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Bạn cần nhập tên chuyên mục" ForeColor="Red" 
    ControlToValidate="txtCategoryName" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>


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
        <tr>
            <td>
                <asp:Label ID="lblFeatureGroup" runat="server" Text="Nhóm thuộc tính:" meta:resourcekey="lblFeatureGroup"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlFeatureGroup" runat="server" DataTextField="FeatureGroupDesc"
                    DataValueField="FeatureGroupId" Width="355px">
                </asp:DropDownList>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblDisplayOrder" runat="server" Text="Thứ tự hiển thị:" meta:resourcekey="lblDisplayOrder"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDisplayOrder" runat="server" Width="350px" Text="10"></asp:TextBox>
                <asp:RegularExpressionValidator ID="rglDisplayOrder" runat="server" ForeColor="Red" ErrorMessage="Chỉ chấp nhận kiểu số" 
                    Display="Dynamic" ValidationExpression="\d+" ControlToValidate="txtDisplayOrder"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>
                Kiểu URLRewrite:
            </td>
            <td>
                <asp:TextBox ID="txtUrlRewrite" runat="server" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr runat="server" visible="true">
            <td>
                <asp:Label ID="lblUrl" runat="server" Text="Url:" meta:resourcekey="lblUrl"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtUrl" runat="server" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr runat="server">
            <td>
                <asp:Label ID="lblMetaTitle" runat="server" Text="Tiêu đề SEO:" meta:resourcekey="lblMetaTitle"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtMetaTitle" runat="server" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr runat="server">
            <td>
                <asp:Label ID="lblMetaDesc" runat="server" Text="Mô tả SEO:" meta:resourcekey="lblMetaDesc"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtMetaDesc" TextMode="MultiLine" runat="server" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr runat="server">
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
                <asp:Label ID="lblShow" runat="server" Text="Hiển thị:" meta:resourcekey="lblShow"></asp:Label>
                &nbsp;
            </td>
            <td>
                <asp:CheckBox ID="ckbShowTop" runat="server" Text="Top" />&nbsp;
                <asp:CheckBox ID="ckbShowBottom" runat="server" Text="Bottom" />&nbsp;
                <asp:CheckBox ID="ckbShowWeb" runat="server" Text="Web" />&nbsp;
                <asp:CheckBox ID="ckbShowWap" runat="server" Text="Wap" />&nbsp;
                <asp:CheckBox ID="ckbShowApp" runat="server" Text="App" />&nbsp;
            </td>
        </tr>
        <tr>
            <td>Vị trí hiển thị:</td>
            <td>
            <asp:CheckBoxList ID="chkDisplayType" DataTextField="DisplayTypeDesc" DataValueField="DisplayTypeId" RepeatDirection="Horizontal" RepeatColumns="5" runat="server">
        </asp:CheckBoxList>
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
        <asp:CheckBox ID="cblAddOther" runat="server" Text="Thêm tiếp dữ liệu khác" Checked="false" ToolTip="Khi tick chọn xóa form hiện tại để thêm dữ liệu khác"  />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

        <asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom" Text="Lưu thông tin" ValidationGroup="G1"
            meta:resourcekey="btnSave" OnClick="btnSave_Click">
        </asp:LinkButton></div>
</asp:Content>
