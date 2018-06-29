<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="MediasEdit.aspx.cs" Inherits="Admin_MediasEdit" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">   
<table id="tblEdit" runat="server" cellspacing="0" cellpadding="3" width="100%" border="0">
        
        <tr>
            <td class="td-edit-2">
                            &nbsp;</td>
            <td>
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr> 
        
        <tr>
            <td class="td-edit-2">
                            <asp:Label ID="lblMediaGroup" runat="server" Text="Nhóm:" meta:resourcekey="lblMediaGroup"></asp:Label>
            </td>
            <td>
                            <asp:DropDownList ID="ddlMediaGroup" runat="server" DataTextField="MediaGroupDesc" 
                    DataValueField="MediaGroupId" Width="250px" CssClass="userselect">
                </asp:DropDownList>
            </td>
        </tr> 
        
        <tr>
            <td class="td-edit-2">
                <asp:Label ID="lblMediaType" runat="server" Text="Loại:" meta:resourcekey="lblMediaType"></asp:Label>
            </td>
            <td>
                            <asp:DropDownList ID="ddlMediaType" runat="server" 
                    DataTextField="MediaTypeDesc" DataValueField="MediaTypeId" 
                    Width="250px" CssClass="userselect" >
                </asp:DropDownList>
            </td>
        </tr> 
        <tr>
            <td class="td-edit-2">
               <asp:Label ID="lblMediaName" runat="server" Text="Tên:" meta:resourcekey="lblMediaName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtMediaName" runat="server" CssClass="tukhoatimekiem" width="95%"></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td class="td-edit-2">
               <asp:Label ID="lblMediaDesc" runat="server" Text="Mô tả:" 
                    meta:resourcekey="lblMediaDesc"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtMediaDesc" runat="server" CssClass="tukhoatimekiem" Width="95%"></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td class="td-edit-2">
               <asp:Label ID="lblFile" runat="server" Text="File:" meta:resourcekey="lblFile"></asp:Label>
            </td>
            <td>
                <asp:FileUpload ID="fUpl" runat="server"  multiple />
            </td>
        </tr> 
        <tr>
            <td class="td-edit-2">
               FilePath:
            </td>
            <td>
                <asp:TextBox ID="txtFilePath" runat="server" CssClass="tukhoatimekiem" Width="95%"></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td class="td-edit-2" colspan="2">
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
</asp:Content>



