<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="ActionsEdit.aspx.cs" Inherits="Admin_ActionsEdit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td style="width:120px"><asp:Label ID="lblParentAction" runat="server" Text="Chức năng cha:" meta:resourcekey="lblParentAction"></asp:Label>
                </td>
            <td><asp:DropDownList ID="ddlParentAction" runat="server" AutoPostBack="True" 
                    DataTextField="ActionDesc" DataValueField="ActionId" onselectedindexchanged="ddlParentAction_SelectedIndexChanged" 
                    Width="350px"></asp:DropDownList>
                &nbsp;</td>
        </tr>
       <%-- <tr>
            <td style="width:120px"><asp:Label ID="lblParentActionLevel2" runat="server" Text="Chức năng cha cấp 2:" meta:resourcekey="lblParentActionLevel2"></asp:Label>
                </td>
            <td><asp:DropDownList ID="ddlParentActionLevel2" runat="server" 
                    DataTextField="ActionDesc" DataValueField="ActionId" 
                    Width="350px"></asp:DropDownList>
                &nbsp;</td>
        </tr>--%>
        <tr>
            <td><asp:Label ID="lblActionName" runat="server" Text="Tên chuyên mục:" meta:resourcekey="lblActionName"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtActionName" runat="server" Width="350px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblActionDesc" runat="server" Text="Mô tả:" meta:resourcekey="lblActionDesc"></asp:Label>
                </td>
            <td><asp:TextBox ID="txtActionDesc" runat="server" Width="350px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblUrl" runat="server" Text="Url:" meta:resourcekey="lblUrl"></asp:Label>
                </td>
            <td><asp:TextBox ID="txtUrl" runat="server" Width="350px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblDisplayOrder" runat="server" Text="Thứ tự hiển thị:" meta:resourcekey="lblDisplayOrder"></asp:Label>
                </td>
            <td><asp:TextBox ID="txtDisplayOrder" runat="server" Width="350px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblDisplay" runat="server" Text="Hiển thị:" meta:resourcekey="lblDisplay"></asp:Label>
                </td>
            <td>
                <asp:CheckBox ID="chkDisplay" runat="server" Checked="true" />
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblActionStatus" runat="server" Text="Trạng thái:" meta:resourcekey="lblActionStatus"></asp:Label>
                </td>
            <td><asp:DropDownList ID="ddlActionStatus" runat="server" 
                    DataTextField="ActionStatusDesc" DataValueField="ActionStatusId"></asp:DropDownList>
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

