<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="ArticlesReport.aspx.cs" Inherits="Admin_Pages_articles_ArticlesReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/admin/UserControls/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="uc1" %>
<%@ Import Namespace="ICSoft.LawDocsLib" %>
<%@ Import Namespace="ICSoft.HelperLib" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);
            $("#<%= txtDateFrom.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
            $("#<%= txtDateTo.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        });
            $('a#popup').live('click', function (e) {
                var page = $(this).attr("href")
                var cdialog = $('<div id="divEdit"></div>')
                .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="98%" height="98%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 600,
                    width: 800,
                    title: $(this).attr("title"),
                    close: function (event, ui) {
                        $(this).remove();
                        try {
                            $('#<%= btnRefresh.ClientID %>')[0].click();
                        } catch (ex) {
                        }

                    }
                });
                 cdialog.dialog('open');
                 e.preventDefault();
             });
        function InitializeRequest(sender, args) {
        }

        function EndRequest(sender, args) {
            $(function () {
                $("#<%= txtDateFrom.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
            });
            $(function () {
                $("#<%= txtDateTo.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
                    });
                }
        
    </script>
    
    <asp:UpdatePanel ID="upn_Grid" runat="server">
    <ContentTemplate>
    <div style="text-align:center;"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
    <table cellpadding="2" cellspacing="0" style="width:100%">
        <tr>
            <td style="width:90px">
                <asp:Label ID="lblSite" runat="server" meta:resourcekey="lblOrderBy" 
                    Text="Site:"></asp:Label>
            </td>
            <td style="width:210px">
                <asp:DropDownList ID="ddlSites" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="SiteName" DataValueField="SiteId" 
                    onselectedindexchanged="ddlSites_SelectedIndexChanged" Width="200px">
                </asp:DropDownList>
            </td>
            <td style="width:90px">
                <asp:Label ID="lblReportBy" runat="server" meta:resourcekey="lblReportBy" 
                    Text="Chuyên mục:"></asp:Label>
            </td>
            <td style="width:210px">
                 <asp:DropDownList ID="ddlReportBy" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="CategoryDesc" DataValueField="CategoryId" 
                    onselectedindexchanged="ddlReportBy_SelectedIndexChanged" Width="200px">
                </asp:DropDownList>
            </td>
            <td style="width:90px">
                <asp:Label ID="lblGridOrderBy" runat="server" meta:resourcekey="lblReportBy" 
                    Text="Sắp xếp theo:"></asp:Label>
            </td>
            <td>
                 <asp:DropDownList ID="ddlGridOrderBy" runat="server" AutoPostBack="True" 
                    CssClass="userselect"  onselectedindexchanged="ddlReportBy_SelectedIndexChanged" Width="200px">
                     <asp:ListItem Text ="Mới nhất" Value="CrDateTime Desc"></asp:ListItem>
                     <asp:ListItem Text ="Lượt View" Value="ViewCount Desc"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
          <tr runat="server" id="lblDateFromTo">
            <td style="width:90px; white-space:nowrap;">
                <asp:Label ID="lblDateFrom" runat="server" meta:resourcekey="lblDateFrom" 
                    Text="Từ ngày:"></asp:Label>
                </td>
            <td style="width:210px">
                <asp:TextBox ID="txtDateFrom" runat="server"  CssClass="tukhoatimekiem" 
                    Width="190px"></asp:TextBox>
            </td>
            <td style="width:90px; white-space:nowrap;">
                <asp:Label ID="lblDateTo" runat="server"  meta:resourcekey="lblDateTo" 
                    Text="Đến ngày"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtDateTo" runat="server"  CssClass="tukhoatimekiem" 
                    Width="190px"></asp:TextBox>
            </td>
              <td></td><td></td>
        </tr>
        
        <tr>
            <td>
                <asp:Label ID="lblOrderBy" runat="server" Text="Khoảng thời gian:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlOrderBy" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="OrderByDesc" DataValueField="OrderBy" 
                    onselectedindexchanged="ddlOrderBy_SelectedIndexChanged" Width="200px">
                </asp:DropDownList>
            </td><td>
                <asp:Label ID="lblStatus" runat="server" meta:resourcekey="lblOrderBy" 
                    Text="Trạng thái:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlReviewStatus" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="ReviewStatusDesc" DataValueField="ReviewStatusId" 
                    onselectedindexchanged="ddlReportBy_SelectedIndexChanged" Width="200px">
                </asp:DropDownList></td>
            <td></td><td>
                &nbsp;<asp:LinkButton ID="btnSearch" runat="server" CssClass="timkiembutom" 
                    meta:resourcekey="btnSearch" onclick="btnSearch_Click" Text="Tìm kiếm">
                        </asp:LinkButton><asp:LinkButton ID="btnRefresh" runat="server" CssClass="timkiembutom hidden-btn-dqs" Text="Refresh" onclick="btnRefresh_Click"></asp:LinkButton></td>
        </tr>
        
    </table>
    <div class="clear5px"></div>
	<div class="vien"></div>
	<div class="clear5px"></div>  
    <div class="khungchucnang">
    <div class="chucnangleft">
		<span class="tieudetongcong">
            <asp:Label ID="lblTotalText" runat="server" Text="Tổng:" meta:resourcekey="lblTotalText"></asp:Label>
        </span>
        <asp:Label ID="lblTong" runat="server" Text="" CssClass="tieudetongcong2"></asp:Label> 
        <span> | </span>
        <span class="tieudetongcong">
            <asp:Label ID="lblSumPendingText" runat="server" Text="Tin chưa duyệt:" meta:resourcekey="lblSumPending"></asp:Label>
        </span>
        <asp:Label ID="lblSumPending" runat="server" Text="" CssClass="tieudetongcong2"></asp:Label>
        <span> | </span>
        <span class="tieudetongcong">
            <asp:Label ID="lblSumReviewedText" runat="server" Text="Tin đã duyệt:" meta:resourcekey="lblSumReviewed"></asp:Label>
        </span>
        <asp:Label ID="lblSumReviewed" runat="server" Text="" CssClass="tieudetongcong2"></asp:Label>
	</div>
	<div class="chucnangright">
	</div>
    <div style="text-align:left; width:50%; float:right"><asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                            <ProgressTemplate><img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...</ProgressTemplate>
                            </asp:UpdateProgress></div>
	<div class="clear5px"></div>
    <div class="contenbangdulieu" runat="server" id="tblbydoctype">
    
        <asp:GridView ID="m_grid" DataKeyNames="ArticleId" runat="server" ShowHeaderWhenEmpty="True"
            AutoGenerateColumns="False" OnRowDataBound = "m_grid_OnRowDataBound"
            Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" PageSize="50" >
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
                        <asp:Label ID="lbNo" runat="server" Text='<%# Container.DataItemIndex + (CustomPaging.PageIndex - 1) * m_grid.PageSize + 1%>' 
                                     ToolTip ='<%# Eval("ArticleId").ToString() %>'> </asp:Label>
                    </ItemTemplate>      
                    <ItemStyle HorizontalAlign="Center" Width="3%" />  
                    <HeaderStyle Width="3%" />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnTitle" runat="server" Text="Tên bài viết" meta:resourcekey="lblGridColumnTitle"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("Title")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnCrDateTime" runat="server" Text="Ngày đăng" meta:resourcekey="lblGridColumnCrDateTime"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# DateTimeHelpers.GetDateAndTime(Eval("CrDateTime"))%>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                 <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnCustomerReviewStatusId" runat="server" Text="Trạng thái" meta:resourcekey="lblGridColumnCustomerReviewStatusId"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <span class="xuatban<%# Eval("ReviewStatusId") %>" >
                            <%# LanguageHelpers.IsCultureVietnamese() ? ICSoft.CMSLib.ReviewStatus.Static_Get(byte.Parse(Eval("ReviewStatusId").ToString()), l_ReviewStatus).ReviewStatusDesc : ICSoft.CMSLib.ReviewStatus.Static_Get(byte.Parse(Eval("ReviewStatusId").ToString()), l_ReviewStatus).ReviewStatusName %>
                        </span> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnUser" runat="server" Text="Biên tập" meta:resourcekey="lblGridColumnUser"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <span class="ngaythang">
                            <font style="color:Blue"><%# UserHelpers.Static_Get(int.Parse(Eval("CrUserId").ToString()), l_Users, "").Username %></font>
                        </span> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="80px" />        
                </asp:TemplateField> 
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnRevUserId" runat="server" Text="Người duyệt" meta:resourcekey="lblGridColumnRevUserId"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <span class="ngaythang">
                            <font style="color:Blue"><%# UserHelpers.Static_Get(int.Parse(Eval("RevUserId").ToString()), l_Users, "").Username %></font>
                        </span> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="80px" />        
                </asp:TemplateField>  
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnDataSourceId" runat="server" Text="Nguồn" meta:resourcekey="lblGridColumnDataSourceId"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                       <span class="xuatban<%# Eval("DataSourceId") %>" >
                                    <%# ICSoft.CMSLib.DataSources. Static_Get(short.Parse(Eval("DataSourceId").ToString()), l_DataSources).DataSourceName %>
                                </span>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Center" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnViewCount" runat="server" Text="Lượt view" meta:resourcekey="lblGridColumnViewCount"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <%# Eval("ViewCount")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign = "Center" />  
                    <HeaderStyle/>        
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

     <div class="clear5px"></div>    
                <uc1:CustomPaging ID="CustomPaging" runat="server" />                
                <div class="clear5px"></div> 
   </div>
    </ContentTemplate>
         <Triggers>
            <asp:AsyncPostBackTrigger ControlID ="txtDateFrom" EventName ="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID ="txtDateTo" EventName ="TextChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>


