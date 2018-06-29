<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master"
    AutoEventWireup="true" CodeFile="WardsEdit.aspx.cs" Inherits="Admin_Pages_articles_DistrictsEdit" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" runat="Server">
    <table id="tblEdit" runat="server" cellspacing="0" cellpadding="3" width="100%" border="0">
        <tr>
            <td colspan="2" class="td-edit-2">
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 120px">
                <asp:Label ID="lblCountries" runat="server" Text="Countries:" meta:resourcekey="lblCountries"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlCountries" runat="server" DataTextField="CountryDesc" DataValueField="CountryId"
                    Width="355px" AutoPostBack="True" OnSelectedIndexChanged="ddlCountries_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblProvinces" runat="server" Text="Provinces:" meta:resourcekey="lblProvinces"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlProvinces" runat="server" DataTextField="ProvinceDesc" DataValueField="ProvinceId"
                    Width="355px" AutoPostBack="True" OnSelectedIndexChanged="ddlProvinces_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblDistricts" runat="server" Text="Districts:" meta:resourcekey="lblDistricts"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlDistrict" runat="server" DataTextField="DistrictDesc" DataValueField="DistrictId"
                    Width="355px">
                </asp:DropDownList>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 120px">
                <asp:Label ID="lblWardName" runat="server" Text="Tên:" meta:resourcekey="lblWardName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtWardName" runat="server" Width="355px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 120px">
                <asp:Label ID="lblWardDesc" runat="server" Text="Mô tả:" meta:resourcekey="lblWardDesc"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtWardDesc" TextMode="MultiLine" runat="server" Width="355px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 120px">
                <asp:Label ID="lblDisplayOrder" runat="server" Text="Thứ Tự Hiển Thị:" meta:resourcekey="lblDisplayOrder"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDisplayOrder" runat="server" Width="355px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td-edit-2" colspan="2">
                <br />
                <div style="text-align: center">
                    <asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom" Text="Lưu thông tin"
                        meta:resourcekey="btnSave" OnClick="btnSave_Click">
                    </asp:LinkButton></div>
            </td>
        </tr>
    </table>
</asp:Content>
