<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="DataSourcesEdit.aspx.cs" Inherits="Admin_DataSourcesEdit" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">   
<table cellspacing="2" cellpadding="2" width="100%" border="0">        
                    <tr>
                        <td class="td-edit-2" style="width: 87px">
                            &nbsp;</td>
                        <td class="style13">
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </td>
                    </tr> 
                    <tr>
                        <td class="td-edit-2" style="width: 87px">
                            <asp:Label ID="lblDataTypes" runat="server" Text="Loại dữ liệu:" meta:resourcekey="lblDataTypes"></asp:Label>
                        </td>
                        <td class="style13">
                            <asp:DropDownList ID="ddlDataTypes" runat="server" DataTextField="DataTypeDesc" CssClass="userselect"
                    DataValueField="DataTypeId" Width="310px">
                </asp:DropDownList>
                        </td>
                    </tr> 
                    <tr>
                        <td class="td-edit-2" style="width: 100px">
                            <asp:Label ID="lblDataSourceName" runat="server" Text="Tên:" meta:resourcekey="lblDataSourceName"></asp:Label>
                        </td>
                        <td >
                            <asp:TextBox ID="txtDataSourceName" runat="server" CssClass="tukhoatimekiem"  Width="300px"></asp:TextBox>
                        </td>
                    </tr> 
                    <tr>
                        <td class="td-edit-2" style="width: 87px">
                            <asp:Label ID="lblDataSourceDesc" runat="server" Text="Mô tả:"
                                meta:resourcekey="lblDataSourceDesc"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDataSourceDesc" runat="server" CssClass="tukhoatimekiem"  TextMode="MultiLine"
                                Height="101px" Width="300px"></asp:TextBox>
                        </td>
                    </tr> 
                    <tr>
                        <td class="td-edit-2" style="width: 87px">
                            <asp:Label ID="lblDisplayOrder" runat="server" Text="Thứ tự:" 
                                meta:resourcekey="lblDisplayOrder"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDisplayOrder" runat="server" CssClass="tukhoatimekiem"  Width="300px"></asp:TextBox>
                        </td>
                    </tr> 
                    </table>
    <br />       
        <div style="text-align:center"><asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom" 
                    Text="Lưu thông tin" meta:resourcekey="btnSave" 
            onclick="btnSave_Click">
                        </asp:LinkButton></div>&nbsp;&nbsp;
   
</asp:Content>


