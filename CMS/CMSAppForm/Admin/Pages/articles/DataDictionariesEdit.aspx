<%@ Page Language="C#" MasterPageFile="~/admin/MasterPageAdminEdit.master" AutoEventWireup="true"
    CodeFile="DataDictionariesEdit.aspx.cs" Inherits="admin_pages_DataDictionariesEdit"
    Title="" %>

<asp:Content ID="contentAccountType" runat="server" ContentPlaceHolderID="m_contentBody">
    <table cellpadding="3" cellspacing="0" style="width: 100%; font-weight: lighter">
        <tr>
            <td class="td-edit-2">
                <asp:Label ID="lblDataDictionaryTypeId" runat="server" Text="Loại Dữ Liệu:" meta:resourcekey="lblDataDictionaryTypeId"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlDataDictionaryTypeId" runat="server" CssClass="userselect" Width="409px"
                    DataTextField="DataDictionaryTypeName" DataValueField="DataDictionaryTypeId">
                </asp:DropDownList>
              
            </td>
        </tr>
        <tr>
            <td class="td-edit-2">
                <asp:Label ID="lblDataDictionaryName" runat="server" Text="Tên:" meta:resourcekey="lblDataDictionaryName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txDataDictionaryName" runat="server" CssClass="tukhoatimekiem" Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td-edit-2">
                <asp:Label ID="lblDataDictionaryDesc" runat="server" Text="Mô Tả:" meta:resourcekey="lblDataDictionaryDesc"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txDataDictionaryDesc" TextMode="MultiLine" runat="server" CssClass="tukhoatimekiem" Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td-edit-2">
                <asp:Label ID="lblMinValue" runat="server" Text="Giá Trị Nhỏ Nhất:" meta:resourcekey="lblMinValue"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txMinValue" runat="server" CssClass="tukhoatimekiem" Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td-edit-2">
                <asp:Label ID="lblMaxValue" runat="server" Text="Giá Trị Lớn Nhất:" meta:resourcekey="lblMaxValue"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txMaxValue" runat="server" CssClass="tukhoatimekiem" Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td-edit-2">
                <asp:Label ID="lblDisplayOrder" runat="server" Text="Thứ Tự Hiển Thị:" meta:resourcekey="lblDisplayOrder"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txDisplayOrder" runat="server" CssClass="tukhoatimekiem" Width="400px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div style="text-align: center; padding: 20px">
        <asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom" Text="Lưu thông tin"
            meta:resourcekey="btnSave" OnClick="btnSave_Click">
        </asp:LinkButton>
    </div>
</asp:Content>
