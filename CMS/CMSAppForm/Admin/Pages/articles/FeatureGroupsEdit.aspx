<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master"
    AutoEventWireup="true" CodeFile="FeatureGroupsEdit.aspx.cs" Inherits="Admin_Pages_articles_FeatureGroupsEdit" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Content ID="contentAccountType" ContentPlaceHolderID="m_contentBody" runat="Server">
    <table cellpadding="3" cellspacing="0" style="width: 100%; font-weight: lighter">
        <tr>
            <td class="td-edit-2">
                &nbsp;</td>
            <td>
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr> 
        <tr>
            <td class="td-edit-2" >
                <asp:Label ID="lblFeatureGroupName" runat="server" Text="Tên:" meta:resourcekey="lblFeatureGroupName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFeatureGroupName" runat="server" CssClass="tukhoatimekiem" Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td-edit-2" >
                <asp:Label ID="lblFeatureGroupDesc" runat="server" Text="Mô tả:" meta:resourcekey="lblFeatureGroupDesc"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFeatureGroupDesc" TextMode="MultiLine" runat="server" CssClass="tukhoatimekiem"  Width="400px"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td class="td-edit-2" colspan="2">
            <br />
           <div style="text-align:center"><asp:LinkButton ID="LinkButton1" runat="server" CssClass="savebutom" 
                        Text="Lưu thông tin" meta:resourcekey="btnSave" 
                onclick="btnSave_Click">
                        </asp:LinkButton></div>
            </td>
        </tr>
    </table>
    <br />
</asp:Content>
