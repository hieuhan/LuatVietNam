<%@ Page Title="" Language="C#" MasterPageFile="~/admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="InternalLinksEdit.aspx.cs" Inherits="admin_pages_articles_InternalLinksEdit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
<script type="text/jscript">
    
</script>
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td colspan="2"><asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red" Font-Bold="true" ></asp:Label></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblSite" runat="server" Text="Trang:" ></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlSites" runat="server" AutoPostBack="True" CssClass="userselect"
                             DataTextField="SiteName" DataValueField="SiteId" OnSelectedIndexChanged="ddlddlSites_SelectedIndexChanged" Width="255px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblInternalName" runat="server" Text="Từ khóa:" ></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtInternalName" runat="server" Width="250px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblInternalLinkGroups" runat="server" Text="Nhóm:" ></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlInternalLinkGroups" runat="server" CssClass="userselect"
                        DataTextField="InternalLinkGroupName" DataValueField="InternalLinkGroupId" Width="255px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblUrl" runat="server" Text="Url:" ></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtInternalLinkUrl" runat="server" Width="250px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblStatusId" runat="server" Text="Trạng thái:" ></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="userselect" Width="255px">
                    <asp:ListItem Value="1" Text="Hoạt động"></asp:ListItem>
                    <asp:ListItem Value="0" Text="Dừng hoạt động"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblCrDateTime" runat="server" Text="Ngày tạo:" ></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtCrDateTime" runat="server" Width="250px" Enabled="false"></asp:TextBox>
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
            onclick="btnSave_Click"></asp:LinkButton>
        <asp:LinkButton ID="btnClose" runat="server" CssClass="savebutom" 
                    Text="Đóng" meta:resourcekey="btnClose" 
            onclick="btnClose_Click" ForeColor="Red">
                        </asp:LinkButton></div>
</asp:Content>

