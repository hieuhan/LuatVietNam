<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="FeaturesEdit.aspx.cs" Inherits="Admin_Pages_articles_FeaturesEdit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
       <tr>
            <td style="width: 110px; white-space: nowrap;">
                    <asp:Label ID="lblFeatureGroup" runat="server" meta:resourcekey="lblFeatureGroup" Text="Nhóm thuộc tính:"></asp:Label>
                </td>
                <td style="width: 270px; white-space: nowrap;">
                    <asp:DropDownList ID="ddlFeatureGroup" runat="server" CssClass="userselect"
                        DataTextField="FeatureGroupName" DataValueField="FeatureGroupId" 
                        Width="250px" AutoPostBack="True" 
                        onselectedindexchanged="ddlFeatureGroup_SelectedIndexChanged1">
                    </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td style="width:120px"><asp:Label ID="lblName" runat="server" Text="Tên " meta:resourcekey="lblName"></asp:Label>
                <span style="color:Red">&nbsp;* </span></td>
            <td>
                <asp:TextBox ID="txtName" runat="server" CssClass="tukhoatimekiem" 
                    Width="350px" TextMode="SingleLine"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblDesc" runat="server" Text="Mô tả " meta:resourcekey="lblDesc"></asp:Label><span style="color:Red">&nbsp;*</span></td>
            <td><asp:TextBox ID="txtDesc" runat="server" CssClass="tukhoatimekiem" 
                    Width="350px" Rows="2" TextMode="MultiLine"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td style="width: 110px; white-space: nowrap;">
                    <asp:Label ID="lblParentFeature" runat="server" meta:resourcekey="lblParentFeature" Text="Thuộc tính cha:"></asp:Label>
                </td>
                <td style="width: 270px; white-space: nowrap;">
                    <asp:DropDownList ID="ddlParentFeature" runat="server" CssClass="userselect"
                        DataTextField="FeatureName" DataValueField="FeatureId" 
                        Width="250px">
                    </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td style="width: 90px; white-space: nowrap;">
                    <asp:Label ID="lblInputType" runat="server" meta:resourcekey="lblInputType" Text="Kiểu nhập liệu:"></asp:Label>
                </td>
                <td style=" white-space: nowrap; width: 260px;">
                    <asp:DropDownList ID="ddlInputType" runat="server" CssClass="userselect"
                        DataTextField="InputTypeDesc" DataValueField="InputTypeId" 
                        Width="250px">
                    </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td style="width: 110px; white-space: nowrap;">
                    <asp:Label ID="lblDataDicType" runat="server" meta:resourcekey="lblDataDicType" Text="Loại từ điển dữ liệu:"></asp:Label>
                </td>
                <td style="width: 270px; white-space: nowrap;">
                    <asp:DropDownList ID="ddlDataDicType" runat="server" CssClass="userselect"
                        DataTextField="DataDictionaryTypeName" DataValueField="DataDictionaryTypeId" 
                        Width="250px">
                    </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblImage" runat="server" Text="Ảnh:" meta:resourcekey="lblImage"></asp:Label>
            </td>
            <td>
               <img alt="txtIcon" class="ImageSelectPath" src="../../../Images/transparent.png" width="60px" height="40px" />
                <asp:TextBox CssClass="HiddenInput" ID="txtIcon" runat="server" Text=""></asp:TextBox>
                &nbsp;
                <asp:CheckBox ID="cbDeleteImages" runat="server" meta:resourcekey="cbDeleteImages" />
                <asp:Label ID="lblDelete" runat="server" Text="delete" meta:resourcekey="lblDelete"></asp:Label>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblDisplayOrder" runat="server" Text="Thứ tự hiển thị" meta:resourcekey="lblDisplayOrder"></asp:Label>
                </td>
            <td><asp:TextBox ID="txtDisplayOrder" runat="server" CssClass="tukhoatimekiem" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr>
         <td>
                <asp:Label ID="lblShow" runat="server" Text="" meta:resourcekey="lblShow"></asp:Label>
                &nbsp;
            </td>
            <td>
                <asp:CheckBox ID="ckbIsDisplay" runat="server" Text="Display" />&nbsp;
                <asp:CheckBox ID="ckbIsData" runat="server" Text="Data" />&nbsp;
                <asp:CheckBox ID="ckbIsSearch" runat="server" Text="Search" />&nbsp;    
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

