<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="FeedBacksEdit.aspx.cs" Inherits="Admin_FeedBacksEdit" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">   
<table id="tblEdit" runat="server" cellspacing="0" cellpadding="2" width="100%" border="0">
        <tr>
            <td class="td-edit-2" width="110px">
                &nbsp;</td>
            <td colspan="3">
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
             </td>
        </tr> 
         <tr>
            <td class="td-edit-2">
               <asp:Label ID="lblSite" runat="server" Text="Site:" meta:resourcekey="lblSite"></asp:Label>
            </td>
            <td colspan="3">
                <asp:DropDownList ID="ddlSite" runat="server" DataTextField="SiteDesc" DataValueField="SiteId"
                    CssClass="userselect" Width="100%">
                </asp:DropDownList>
            </td>
        </tr> 
        <tr>
            <td class="td-edit-2">
                <asp:Label ID="lblLanguage" runat="server" meta:resourcekey="lblLanguage" 
                    Text="Ngôn ngữ:"></asp:Label>
            </td>
            <td width="240px">
                <asp:DropDownList ID="ddlLanguage" runat="server" 
                    CssClass="userselect" DataTextField="LanguageDesc" DataValueField="LanguageId"  Width="250px">
                </asp:DropDownList>
            </td>
            <td width="110px">
                <asp:Label ID="lblAppType" runat="server" meta:resourcekey="lblAppType" 
                    Text="Loại ứng dụng:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlAppType" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="ApplicationTypeDesc" 
                    DataValueField="ApplicationTypeId" 
                    onselectedindexchanged="ddlAppType_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
        </tr> 
        
        <tr>
            <td class="style9">
                <asp:Label ID="lblFeedBackGroups" runat="server" Text="Nhóm:" 
                    meta:resourcekey="lblFeedBackGroups"></asp:Label>
                </td>
            <td class="style9">
                <asp:DropDownList ID="ddlFeedBackGroups" runat="server"
                    CssClass="userselect" DataTextField="FeedBackGroupDesc" 
                    DataValueField="FeedBackGroupId" Width="250px">
                </asp:DropDownList>
            </td>
            <td class="style9">
                <asp:Label ID="lblReviewStatus" runat="server" 
                    meta:resourcekey="lblReviewStatus" Text="Trạng thái:"></asp:Label>
            </td>
            <td class="style9">
                <asp:DropDownList ID="ddlReviewStatus" runat="server" 
                    CssClass="userselect" DataTextField="ReviewStatusDesc" 
                    DataValueField="ReviewStatusId" Width="250px">
                </asp:DropDownList>
            </td>
        </tr> 
        
        <tr>
            <td class="style9">
               <asp:Label ID="lblFullName" runat="server" Text="Họ tên:" 
                    meta:resourcekey="lblFullName"></asp:Label>
                <span class="icon-required"></span>
                <asp:RequiredFieldValidator ID="rfvFullName" runat="server" Display="Dynamic" 
                ErrorMessage="(*)" ValidationGroup="G1" 
                ControlToValidate="txtFullName" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            <td class="style9">
                <asp:TextBox ID="txtFullName" runat="server" CssClass="tukhoatimekiem" width="240px" ></asp:TextBox>
                <br /><asp:RequiredFieldValidator ID="rfvtxtFullName" runat="server" ErrorMessage="Họ và tên" ForeColor="Red" ControlToValidate="txtFullName" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
            </td>
            <td class="style9">
               <asp:Label ID="lblUserId" runat="server" Text="UserId:" 
                    meta:resourcekey="lblUserId"></asp:Label>
                </td>
            <td class="style9">
                <asp:TextBox ID="txtUserId" runat="server" CssClass="tukhoatimekiem" width="240px"></asp:TextBox>
            </td>
        </tr> 
        
        <tr>
            <td class="style9">
               <asp:Label ID="lblPhoneNumber" runat="server" Text="Số điện thoại:" 
                    meta:resourcekey="lblPhoneNumber"></asp:Label>
            </td>
            <td class="style9">
                <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="tukhoatimekiem" width="240px"></asp:TextBox>
            </td>
            <td class="style9">
               <asp:Label ID="lblOrganName" runat="server" Text="Tên cơ quan:" 
                    meta:resourcekey="lblOrganName"></asp:Label>
            </td>
            <td class="style9">
                <asp:TextBox ID="txtOrganName" runat="server" CssClass="tukhoatimekiem" 
                    width="240px"></asp:TextBox>
            </td>
        </tr> 
        
        <tr>
            <td class="style9">
               <asp:Label ID="lblEmail" runat="server" Text="Email:" 
                    meta:resourcekey="lblEmail"></asp:Label>
            </td>
            <td class="style9">
                <asp:TextBox ID="txtEmail" runat="server" CssClass="tukhoatimekiem" width="240px"></asp:TextBox>
            </td>
            <td class="style9">
               <asp:Label ID="lblAddress" runat="server" Text="Địa chỉ:" 
                    meta:resourcekey="lblAddress"></asp:Label>
            </td>
            <td class="style9">
                <asp:TextBox ID="txtAddress" runat="server" CssClass="tukhoatimekiem" width="240px"></asp:TextBox>
            </td>
        </tr> 
        
        <tr>
            <td class="style9">
               <asp:Label ID="lblRatingScore" runat="server" Text="Điểm đánh giá:" 
                    meta:resourcekey="lblRatingScore"></asp:Label>
            </td>
            <td class="style9">
                <asp:TextBox ID="txtRatingScore" runat="server" CssClass="tukhoatimekiem" width="240px"></asp:TextBox>
            </td>
            <td class="style9">
                <asp:Label ID="lblFromIP" runat="server" 
                    meta:resourcekey="lblFromIP" Text="Từ IP:"></asp:Label>
            </td>
            <td class="style9">
                <asp:TextBox ID="txtFromIP" runat="server" CssClass="tukhoatimekiem" width="240px"></asp:TextBox>
             </td>
        </tr> 
        
        <tr>
            <td class="td-edit-2">
               <asp:Label ID="lblTitle" runat="server" Text="Tiêu đề:" meta:resourcekey="lblTitle"></asp:Label>
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtTitle" runat="server" CssClass="tukhoatimekiem" width="98%"></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td class="td-edit-2">
               <asp:Label ID="lblComment" runat="server" Text="Nội dung:" 
                    meta:resourcekey="lblComment"></asp:Label>
                <span class="icon-required"></span>
                <asp:RequiredFieldValidator ID="rfvComment" runat="server" Display="Dynamic" 
                ErrorMessage="(*)" ValidationGroup="G1" 
                ControlToValidate="txtComment" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine"  
                   CssClass="tukhoatimekiem" width="98%" Height="130px"></asp:TextBox>
                <br /><asp:RequiredFieldValidator ID="rfvtxtComment" runat="server" ErrorMessage="Nội dung" ForeColor="Red" ControlToValidate="txtComment" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
            </td>
        </tr> 
       
        <tr>
            <td class="td-edit-2" colspan="4">
                 <br />
    <div style="text-align:center">
        <asp:CheckBox ID="cblAddOther" runat="server" Text="Thêm tiếp dữ liệu khác" Checked="false" ToolTip="Khi tick chọn sẽ xóa form hiện tại để thêm dữ liệu khác"  />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

        <asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom" 
                    Text="Lưu thông tin" meta:resourcekey="btnSave" ValidationGroup="G1"
            onclick="btnSave_Click">
                        </asp:LinkButton>
    </div>
            </td>
        </tr>
    </table>
</asp:Content>


<asp:Content ID="Content3" runat="server" contentplaceholderid="m_header">
    <style type="text/css">
        .style9
        {
            height: 31px;
        }
    </style>
</asp:Content>



