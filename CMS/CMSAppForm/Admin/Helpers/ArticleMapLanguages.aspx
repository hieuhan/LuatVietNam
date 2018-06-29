<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPagePublic.master" AutoEventWireup="true" CodeFile="ArticleMapLanguages.aspx.cs" Inherits="Admin_ArticleMapLanguages" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/admin/UserControls/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="uc1" %>
<%@ Import Namespace="ICSoft.CMSLib" %>
<%@ Import Namespace="ICSoft.HelperLib" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <script type="text/javascript">
        var cdialog = $('<div id="divEdit"></div>');
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);
            $("#<%= txtDateFrom.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
            $("#<%= txtDateTo.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });

            $('a.popup').live('click', function (e) {
                var page = $(this).attr("href");
                cdialog
                .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="100%" height="100%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 460,
                    width: 600,
                    title: $(this).attr("title"),
                    close: function (event, ui) {
                        $(this).remove();
                        window.location = $('#<%= btnSearch.ClientID %>').attr("href");
                        //$('#<%= btnSearch.ClientID %>').click();
                    }
                });
                cdialog.dialog('open');
                e.preventDefault();
            });
            $('a.popup2').live('click', function (e) {
                var page = $(this).attr("href");
                cdialog
                .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="100%" height="100%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 600,
                    width: 1000,
                    title: $(this).attr("title"),
                    close: function (event, ui) {
                        $(this).remove();
                       
                    }
                });
                cdialog.dialog('open');
                e.preventDefault();
            });
        });
        function InitializeRequest(sender, args) {
            
        }

        function EndRequest(sender, args) {
            $("#<%= txtDateFrom.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
            $("#<%= txtDateTo.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        }
    </script>
    <div style="text-align:center"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
    <table cellpadding="4" cellspacing="0" style="width:100%">
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
            <td style="width:90px; white-space:nowrap;"><asp:Label ID="lblAppType" runat="server" Text="Loại ứng dụng:" meta:resourcekey="lblAppType"></asp:Label>
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
                <asp:Label ID="lblSource" runat="server" Text="Nguồn:" meta:resourcekey="lblSource"></asp:Label>
            </td>
            <td><asp:DropDownList ID="ddlDataSource" runat="server" 
                    DataTextField="DataSourceDesc" DataValueField="DataSourceId" 
                    Width="250px" CssClass="userselect" AutoPostBack="True" 
                    onselectedindexchanged="ddlDataSource_SelectedIndexChanged">
                </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblReviewStatus" runat="server" Text="Trạng thái:" meta:resourcekey="lblReviewStatus"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlReviewStatus" runat="server" 
                    DataTextField="ReviewStatusDesc" DataValueField="ReviewStatusId" Width="250px" 
                    CssClass="userselect" AutoPostBack="True" 
                    onselectedindexchanged="ddlReviewStatus_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lblOrderBy" runat="server" Text="Sắp xếp:" meta:resourcekey="lblOrderBy"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlOrderBy" runat="server" DataTextField="OrderByDesc" 
                    DataValueField="OrderBy" Width="250px" CssClass="userselect" 
                    AutoPostBack="True" 
                    onselectedindexchanged="ddlOrderBy_SelectedIndexChanged"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width:60px; white-space:nowrap;">
                <asp:Label ID="lblDateFrom" runat="server" Text="Từ ngày:" meta:resourcekey="lblDateFrom"></asp:Label>
            </td>
            <td style="width:260px">
                <asp:TextBox ID="txtDateFrom" runat="server" CssClass="tukhoatimekiem" Width="100px"></asp:TextBox>
                <asp:Label ID="lblDateTo" runat="server" Text="đến:" meta:resourcekey="lblDateTo"></asp:Label>
                <asp:TextBox ID="txtDateTo" runat="server" CssClass="tukhoatimekiem" Width="100px"></asp:TextBox>
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
	</div>
	<div class="chucnangright">
        <asp:LinkButton ID="lbDelete" runat="server" CssClass="xoatin" Text="Xóa" 
            meta:resourcekey="lbDelete" onclick="lbDelete_Click">
        </asp:LinkButton>
        <asp:LinkButton ID="lbUnCheck" runat="server" CssClass="boduyet" 
            Text="Bỏ duyệt" meta:resourcekey="lbUnCheck" onclick="lbUnCheck_Click"> 
        </asp:LinkButton>
        <asp:LinkButton ID="lbReview" runat="server" CssClass="duyettin" Text="Duyệt" 
            meta:resourcekey="lbReview" onclick="lbReview_Click"> 
        </asp:LinkButton>					
		<a href="ArticlesEdit.aspx?LanguageId=<%=LanguageId %>&AppTypeId=<%=AppTypeId %>&CategoryId=<%=CategoryId %>" class="themmoi" > 
            <asp:Label ID="lblAddNew" runat="server" Text="Thêm mới" meta:resourcekey="lblAddNew"></asp:Label>
        </a>
	</div>
    <div style="text-align:left; width:200px; float:right">
    </div>
	<div class="clear5px"></div>
    <div class="contenbangdulieu">
    
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
                        <asp:Label ID="lblGridColumnName" runat="server" Text="Tên chuyên mục" meta:resourcekey="lblGridColumnName"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <b><%# Eval("Title") %></b>
                        
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                
                <asp:TemplateField>
                   <ItemStyle  HorizontalAlign="Left" Width="50%" />
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlArticleMap" runat="server" DataTextField="Title" DataValueField= "ArticleId"
                         EnableViewState="true" ></asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div class="clear5px"></div>    
                <uc1:CustomPaging ID="CustomPaging" runat="server" />                
   </div>
</asp:Content>
