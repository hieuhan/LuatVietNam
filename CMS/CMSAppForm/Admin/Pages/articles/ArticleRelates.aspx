<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="ArticleRelates.aspx.cs" Inherits="Admin_ArticleRelates" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/admin/UserControls/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="uc1" %>
<%@ Import Namespace="ICSoft.CMSLib" %>
<%@ Import Namespace="ICSoft.HelperLib" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    
    <asp:UpdatePanel ID="upn_Grid" runat="server">
    <ContentTemplate>
    <div style="text-align:center"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
    <table cellpadding="2" cellspacing="0" style="width:100%">
        <tr>
            <td style="width:60px; white-space:nowrap;"><asp:Label ID="lblSite" runat="server" Text="Site:" meta:resourcekey="lblSite"></asp:Label>
            </td>
            <td style="width:260px">
                <asp:DropDownList ID="ddlSite" runat="server" DataTextField="SiteDesc" 
                    DataValueField="SiteId" Width="250px" CssClass="userselect"
                    AutoPostBack="True" onselectedindexchanged="ddlSite_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="width:90px; white-space:nowrap;">    
                <asp:Label ID="lblDataType" runat="server" Text="Loại dữ liệu:" meta:resourcekey="lblDataType"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlDataType" runat="server" 
                    DataTextField="DataTypeDesc" DataValueField="DataTypeId" 
                    Width="250px" CssClass="userselect" AutoPostBack="True" 
                        onselectedindexchanged="ddlDataType_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width:60px; white-space:nowrap;">
                <asp:Label ID="lblLanguage" runat="server" Text="Ngôn ngữ:" meta:resourcekey="lblLanguage"></asp:Label>
            </td>
            <td style="width:260px">
                <asp:DropDownList ID="ddlLanguage" runat="server" DataTextField="LanguageDesc" 
                    DataValueField="LanguageId" Width="250px" CssClass="userselect" 
                    AutoPostBack="True" onselectedindexchanged="ddlLanguage_SelectedIndexChanged">
                </asp:DropDownList>
                </td>
            <td style="width:90px; white-space:nowrap;">
                <asp:Label ID="lblAppType" runat="server" Text="Loại ứng dụng:" meta:resourcekey="lblAppType"></asp:Label>
            </td>
            <td><asp:DropDownList ID="ddlAppType" runat="server" 
                    DataTextField="ApplicationTypeDesc" DataValueField="ApplicationTypeId" 
                    Width="250px" CssClass="userselect" AutoPostBack="True" 
                    onselectedindexchanged="ddlAppType_SelectedIndexChanged">
                </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td style="width:60px; white-space:nowrap;">
                <asp:Label ID="lblCategory" runat="server" Text="Chuyên mục:" meta:resourcekey="lblCategory"></asp:Label>
                </td>
            <td style="width:260px">
                <asp:DropDownList ID="ddlCategory" runat="server" DataTextField="CategoryDesc" 
                    DataValueField="CategoryId" Width="250px" CssClass="userselect" 
                    AutoPostBack="True" onselectedindexchanged="ddlCategory_SelectedIndexChanged">
                </asp:DropDownList>
                </td>
            <td style="width:90px; white-space:nowrap;">
                <asp:Label ID="lblKeyword" runat="server" Text="Từ khóa:" meta:resourcekey="lblKeyword"></asp:Label>
            </td>
            <td><asp:TextBox ID="txtSearch" runat="server" CssClass="tukhoatimekiem" Width="240px"></asp:TextBox>&nbsp;&nbsp;
                <asp:LinkButton ID="btnSearch" runat="server" CssClass="timkiembutom" 
                    Text="Tìm kiếm" meta:resourcekey="btnSearch" onclick="btnSearch_Click">
                        </asp:LinkButton>
                </td>
        </tr>
    </table>
    <div class="clear5px"></div>
	<div class="vien"></div>
	<div class="clear5px"></div>  
    <div class="khungchucnang">
    <div class="chucnangleft">
		<span class="tieudetongcong">
            <asp:Label ID="lblTotalText" runat="server" Text="Tổng cộng:" meta:resourcekey="lblTotalText"></asp:Label>
        </span>
        <asp:Label ID="lblTong" runat="server" Text="" CssClass="tieudetongcong2"></asp:Label> 
        <%=GetLocalResourceObject("Article").ToString() %>
	</div>
	<div class="chucnangright">
        <asp:LinkButton ID="lbDelete" runat="server" CssClass="xoatin" Text="Xóa" 
            meta:resourcekey="lbDelete" onclick="lbDelete_Click">
        </asp:LinkButton>        
	</div>
    <div style="text-align:left; width:200px; float:right"><asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                            <ProgressTemplate><img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...</ProgressTemplate>
                            </asp:UpdateProgress></div>
	<div class="clear5px"></div>
    <div>
        <table width="100%" border="0">
            <tr>
                <td valign="top">
                    <div class="contenbangdulieu" style=" width:auto; height:350px; overflow:auto;">
                    <asp:GridView ID="m_grid" DataKeyNames="ArticleId" runat="server" ShowHeaderWhenEmpty="True" PageSize="50"
                        AutoGenerateColumns="False" CssClass="filter-table" OnRowDeleting="m_grid_RowDeleting" OnRowDataBound = "m_grid_OnRowDataBound"
                        Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" >
                        <HeaderStyle CssClass="trbangdulieutieude" />
                        <RowStyle CssClass="trbangdulieutieudenoidung" />
                        <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                        <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />                                        
                        <Columns>                        
                            <asp:TemplateField >
                                <HeaderTemplate>                                
                                #
                                </HeaderTemplate>
                                <ItemTemplate>
                                <asp:Label ID="lbNo" runat="server" Text='<%# Container.DataItemIndex + (CustomPaging.PageIndex - 1) * m_grid.PageSize + 1 %>'>                                    
                                </asp:Label>
                                </ItemTemplate>      
                                <ItemStyle HorizontalAlign="Center" Width="3%" />  
                                <HeaderStyle Width="3%" />          
                            </asp:TemplateField>        
                            <asp:TemplateField > 
                                    <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnName" runat="server" Text="Tên chuyên mục" meta:resourcekey="lblGridColumnName"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <b><%# Eval("Title") %></b>
                                </ItemTemplate>
                                <ItemStyle  HorizontalAlign="Left" />
                                <HeaderStyle />          
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnOperation" runat="server" Text="Thao tác" meta:resourcekey="lblGridColumnOperation"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <asp:LinkButton ID="lbDelete" runat="server" CssClass="themmoi" CommandName="Delete" ToolTip="Chọn" meta:resourcekey="lnkGridColumnDel"></asp:LinkButton> 
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Width="68px" Wrap="false" />
                                <HeaderStyle Width="68px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    </div>
                </td>
                <td valign="top" width="50%">
                    <div class="contenbangdulieu" style=" width:auto; height:350px; overflow:auto;">
                    <asp:GridView ID="m_gridRelate" DataKeyNames="ArticleReferenceId" runat="server" ShowHeaderWhenEmpty="True" PageSize="50"
                        AutoGenerateColumns="False" CssClass="filter-table" OnRowDeleting="m_gridRelate_RowDeleting" OnRowDataBound = "m_gridRelate_OnRowDataBound"
                        Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" >
                        <HeaderStyle CssClass="trbangdulieutieude" />
                        <RowStyle CssClass="trbangdulieutieudenoidung" />
                        <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                        <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />                                        
                        <Columns>                        
                            <asp:TemplateField >
                                <HeaderTemplate>                                
                                #
                                </HeaderTemplate>
                                <ItemTemplate>
                                <asp:Label ID="lbNo" runat="server" Text='<%# Container.DataItemIndex + (CustomPaging.PageIndex - 1) * m_grid.PageSize + 1 %>'>                                    
                                </asp:Label>
                                </ItemTemplate>      
                                <ItemStyle HorizontalAlign="Center" Width="3%" />  
                                <HeaderStyle Width="3%" />          
                            </asp:TemplateField>        
                            <asp:TemplateField > 
                                    <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnName" runat="server" Text="Tên chuyên mục" meta:resourcekey="lblGridColumnName"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <b><%# Articles.Static_Get(int.Parse(Eval("ArticleReferenceId").ToString()), LanguageId, AppTypeId).Title %></b>
                                </ItemTemplate>
                                <ItemStyle  HorizontalAlign="Left" />
                                <HeaderStyle />          
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnOperation" runat="server" Text="Thao tác" meta:resourcekey="lblGridColumnOperation"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <asp:LinkButton ID="lbDelete" runat="server" CssClass="iconadmxoa" CommandName="Delete" ToolTip="Xóa" meta:resourcekey="lnkGridColumnDelRelate"></asp:LinkButton> 
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Width="68px" Wrap="false" />
                                <HeaderStyle Width="68px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                                <HeaderStyle Width="40px" />
                                <HeaderTemplate>
                                    <input type="checkbox" name="SelectAllCheckBox" onclick="SelectAll(this)">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkAction" runat="server"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
        
    </div>
    <div class="clear5px"></div>    
                <uc1:CustomPaging ID="CustomPaging" runat="server" />                
   </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
