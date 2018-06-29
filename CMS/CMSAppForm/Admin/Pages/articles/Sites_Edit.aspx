<%@ Page Language="C#" MasterPageFile="~/admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="Sites_Edit.aspx.cs" Inherits="admin_pages_SitesEdit" Title="" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td style="width:120px"><asp:Label ID="lblSiteName" runat="server" Text="Tên " meta:resourcekey="lblSiteName"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtSiteName" runat="server" CssClass="tukhoatimekiem" 
                    Width="350px" TextMode="SingleLine"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblSiteDesc" runat="server" Text="Mô tả " meta:resourcekey="lblSiteDesc"></asp:Label></td>
            <td><asp:TextBox ID="txtSiteDesc" runat="server" CssClass="tukhoatimekiem" 
                    Width="350px" Rows="2" TextMode="MultiLine"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblMetaTitle" runat="server" Text="Seo" meta:resourcekey="lblMetaTitle"></asp:Label>
                </td>
            <td><asp:TextBox ID="txtMetaTitle" runat="server" CssClass="tukhoatimekiem" 
                    Width="350px" Rows="1" TextMode="MultiLine"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblMetaDesc" runat="server" Text="Mô tả Seo " meta:resourcekey="lblMetaDesc"></asp:Label>
                </td>
            <td><asp:TextBox ID="txtMetaDesc" runat="server" CssClass="tukhoatimekiem" 
                    Width="350px" Rows="2" TextMode="MultiLine"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblMetaKeyword" runat="server" Text="Từ khóa Seo " meta:resourcekey="lblMetaKeyword"></asp:Label>
                </td>
            <td><asp:TextBox ID="txtMetaKeyword" runat="server" CssClass="tukhoatimekiem" 
                    Width="350px" Rows="2" TextMode="MultiLine"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblMetaTagAll" runat="server" Text="Thông tin Seo chung" meta:resourcekey="lblMetaTagAll"></asp:Label>
                </td>
            <td><asp:TextBox ID="txtMetaTagAll" runat="server" CssClass="tukhoatimekiem" 
                    Width="350px" Rows="2" TextMode="MultiLine"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblCanonicalTag" runat="server" Text="Thẻ Canonical:" meta:resourcekey="lblCanonicalTag"></asp:Label>
                </td>
            <td><asp:TextBox ID="txtCanonicalTag" runat="server" CssClass="tukhoatimekiem" 
                    Width="350px" Rows="2" TextMode="MultiLine"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblH1Tag" runat="server" Text="Nội dung thẻ H1:" meta:resourcekey="lblH1Tag"></asp:Label>
                </td>
            <td><asp:TextBox ID="txtH1Tag" runat="server" CssClass="tukhoatimekiem" 
                    Width="350px" Rows="2" TextMode="MultiLine"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblSeoFooter" runat="server" Text="SEO footer:" meta:resourcekey="lblSeoFooter"></asp:Label>
                </td>
            <td><asp:TextBox ID="txtSeoFooter" runat="server" CssClass="tukhoatimekiem" 
                    Width="350px" Rows="2" TextMode="MultiLine"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <div style="text-align:center"><asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom" 
                    Text="Lưu thông tin" meta:resourcekey="btnSave" 
            onclick="btnSave_Click">
                        </asp:LinkButton></div>
</asp:Content>

