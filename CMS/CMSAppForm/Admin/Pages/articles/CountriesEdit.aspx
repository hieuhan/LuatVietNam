<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="CountriesEdit.aspx.cs" Inherits="Admin_Pages_articles_CountriesEdit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td style="width:120px"><asp:Label ID="lblName" runat="server" Text="Tên " meta:resourcekey="lblName"></asp:Label>
                <span style="color:Red">*</span></td>
            <td>
                <asp:TextBox ID="txtName" runat="server" CssClass="tukhoatimekiem" 
                    Width="300px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblImage" runat="server" Text="File ảnh:" meta:resourcekey="lblImage"></asp:Label>
            </td>
            <td>
               <img alt="txtIcon" class="ImageSelectPath" src="../../../Images/transparent.png" width="60px" height="40px" />
                <asp:TextBox CssClass="HiddenInput" ID="txtIcon" runat="server" Text=""></asp:TextBox>
                &nbsp;
                <asp:CheckBox ID="cbDeleteImages" runat="server" meta:resourcekey="cbDeleteImages" />
                <asp:Label ID="lblDelete" runat="server" Text="xóa" meta:resourcekey="lblDelete"></asp:Label>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblDesc" runat="server" Text="Mô tả:" meta:resourcekey="lblDesc"></asp:Label>
                <span style="color:Red">*</span></td>
            <td><asp:TextBox ID="txtDesc" runat="server" CssClass="tukhoatimekiem" 
                    Width="300px" Rows="3" TextMode="MultiLine"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblDisplayOrder" runat="server" Text="Thứ tự hiển thị" meta:resourcekey="lblDisplayOrder"></asp:Label>
                </td>
            <td><asp:TextBox ID="txtDisplayOrder" runat="server" CssClass="tukhoatimekiem" 
                    Width="300px"></asp:TextBox>
                </td>
        </tr>
        
    </table><br />
    <div style="text-align:center"><asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom" 
                    Text="Lưu thông tin" meta:resourcekey="btnSave" 
            onclick="btnSave_Click">
                        </asp:LinkButton></div>
</asp:Content>

