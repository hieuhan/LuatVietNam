<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="CategoriesPosition.aspx.cs" Inherits="Admin_CategoriesPosition" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/admin/UserControls/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="uc1" %>
<%@ Import Namespace="ICSoft.CMSLib" %>
<%@ Import Namespace="ICSoft.HelperLib" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);

            $('a.popup').live('click', function (e) {
                var page = $(this).attr("href")
                var cdialog = $('<div id="divEdit"></div>')
                .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="98%" height="98%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 520,
                    width: 680,
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
        });
    function InitializeRequest(sender, args) {
    }

    function EndRequest(sender, args) {
        
    }
    </script>
    <asp:UpdatePanel ID="upn_Grid" runat="server">
    <ContentTemplate>
    <div style="text-align:center"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
    <table cellpadding="2" cellspacing="0" style="width:100%">
     <tr>
         <td style="width:60px; white-space:nowrap;"><asp:Label ID="lblSite" runat="server" Text="Site:" meta:resourcekey="lblSite"></asp:Label>
                </td>
            <td  style="width:260px"><asp:DropDownList ID="ddlSite" runat="server" 
                    DataTextField="SiteDesc" DataValueField="SiteId" 
                    Width="250px" CssClass="userselect" AutoPostBack="True" 
                    onselectedindexchanged="ddlSite_SelectedIndexChanged">
                </asp:DropDownList>
                </td>
            <td style="width:90px; white-space:nowrap;">
                <asp:Label ID="lblDataType" runat="server" Text="Loại dữ liệu:" meta:resourcekey="lblDataType"></asp:Label>
                </td>
            <td >
                <asp:DropDownList ID="ddlDataType" runat="server" DataTextField="DataTypeDesc" 
                    DataValueField="DataTypeId" Width="250px" CssClass="userselect" 
                    AutoPostBack="True" onselectedindexchanged="ddlDataType_SelectedIndexChanged">
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
            <td><asp:Label ID="lblReviewStatus" runat="server" Text="Trạng thái:" meta:resourcekey="lblReviewStatus"></asp:Label>
                </td>
            <td><asp:DropDownList ID="ddlReviewStatus" runat="server" 
                    DataTextField="ReviewStatusDesc" DataValueField="ReviewStatusId" Width="250px" 
                    CssClass="userselect" AutoPostBack="True" 
                    onselectedindexchanged="ddlReviewStatus_SelectedIndexChanged">
                </asp:DropDownList>
                </td>
            <td><asp:Label ID="lblOrderBy" runat="server" Text="Sắp xếp:" meta:resourcekey="lblOrderBy"></asp:Label>
                </td>
            <td><asp:DropDownList ID="ddlOrderBy" runat="server" DataTextField="OrderByDesc" 
                    DataValueField="OrderBy" Width="250px" CssClass="userselect" 
                    AutoPostBack="True" 
                    onselectedindexchanged="ddlOrderBy_SelectedIndexChanged"></asp:DropDownList>&nbsp;&nbsp;
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
        <asp:LinkButton ID="lbReview" runat="server" CssClass="duyettin" Text="Cập nhật hiển thị" 
             onclick="lbReview_Click"> 
        </asp:LinkButton>	
        <a href="CategoryDisplayTypes.aspx" title="Cập nhật thứ tự hiển thị" class="duyettin popup" > 
            Cập nhật thứ tự
        </a>
        <a href="PositionDisplayRows.aspx" title="Cấu hình hiển thị" class="duyettin popup" > 
            Cấu hình hiển thị
        </a>
	</div>
    <div style="text-align:left; width:200px; float:right"><asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                            <ProgressTemplate><img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...</ProgressTemplate>
                            </asp:UpdateProgress></div>
	<div class="clear5px"></div>
    <div class="contenbangdulieu">
    
        <asp:GridView ID="m_grid" DataKeyNames="CategoryId" runat="server" ShowHeaderWhenEmpty="True"
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
                    <asp:Label ID="lbNo" runat="server" Text='<%# Container.DataItemIndex + 1%>'>                                    
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
                        <%# Eval("CategoryName").ToString() %> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" VerticalAlign="Top" />
                    <HeaderStyle  Width="30%" />          
                </asp:TemplateField> 
                <asp:TemplateField> 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnShow" runat="server" Text="Hiển thị"></asp:Label>
                    </HeaderTemplate> 
                     <ItemTemplate>
                       <asp:CheckBox id="ckbShowTop" runat="server" Text="Top" BackColor='<%# Eval("ShowTop").ToString()=="1"?System.Drawing.Color.Yellow:System.Drawing.Color.White %>' Checked='<%# Eval("ShowTop").ToString()=="1"?true:false%>' />
                       <asp:CheckBox id="ckbShowBottom" runat="server" Text="Bottom" BackColor='<%# Eval("ShowBottom").ToString()=="1"?System.Drawing.Color.Yellow:System.Drawing.Color.White %>' Checked='<%# Eval("ShowBottom").ToString()=="1"?true:false%>' />
                       <asp:CheckBox id="ckbShowWeb" runat="server" Text="HomeWeb" BackColor='<%# Eval("ShowWeb").ToString()=="1"?System.Drawing.Color.Yellow:System.Drawing.Color.White %>' Checked='<%# Eval("ShowWeb").ToString()=="1"?true:false%>' />
                       <asp:CheckBox id="ckbShowWap" runat="server" Text="HomeWap" BackColor='<%# Eval("ShowWap").ToString()=="1"?System.Drawing.Color.Yellow:System.Drawing.Color.White %>' Checked='<%# Eval("ShowWap").ToString()=="1"?true:false%>' />
                       <asp:CheckBox id="ckbShowApp" runat="server" Text="HomeApp" BackColor='<%# Eval("ShowApp").ToString()=="1"?System.Drawing.Color.Yellow:System.Drawing.Color.White %>' Checked='<%# Eval("ShowApp").ToString()=="1"?true:false%>' />    <br />
                       <asp:CheckBoxList ID="chkDisplayType" DataTextField="DisplayTypeDesc" DataValueField="DisplayTypeId" RepeatDirection="Horizontal" RepeatColumns="10" runat="server">
                       </asp:CheckBoxList>                
                     </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
   </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
