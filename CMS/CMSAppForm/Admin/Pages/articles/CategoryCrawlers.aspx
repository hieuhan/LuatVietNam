<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="CategoryCrawlers.aspx.cs" Inherits="Admin_CategoryCrawlers" %>
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
            $('a#popup').live('click', function (e) {
                var page = $(this).attr("href")
                var cdialog = $('<div id="divEdit"></div>')
                .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="98%" height="98%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 500,
                    width: 650,
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
        <td  style="width:80px; white-space:nowrap;">
                Nguồn dữ liệu:
            </td>
            <td style="width:260px">
                <asp:DropDownList ID="ddlDataSource" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="DataSourceDesc" 
                    DataValueField="DataSourceId" 
                    onselectedindexchanged="ddlDataSource_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
            <td style="width:90px; white-space:nowrap;">
            </td>
            <td >
            </td>
        </tr>
        <tr>
        <td  style="width:80px; white-space:nowrap;">
                <asp:Label ID="lblSite" runat="server" 
                    meta:resourcekey="lblSite" Text="Site:"></asp:Label>
            </td>
            <td style="width:260px">
                <asp:DropDownList ID="ddlSite" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="SiteDesc" 
                    DataValueField="SiteId" 
                    onselectedindexchanged="ddlSite_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
            <td style="width:90px; white-space:nowrap;">
                <asp:Label ID="lblReviewStatus" runat="server" 
                    meta:resourcekey="lblReviewStatus" Text="Trạng thái:"></asp:Label>
            </td>
            <td >
                <asp:DropDownList ID="ddlReviewStatus" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="StatusDesc" 
                    DataValueField="StatusId" 
                    onselectedindexchanged="ddlReviewStatus_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
                   <asp:LinkButton ID="btnSearch" runat="server" CssClass="timkiembutom" 
                    meta:resourcekey="btnSearch" onclick="btnSearch_Click" Text="Tìm kiếm">
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
        <a id="popup" href="CategoryCrawlersEdit.aspx?SiteId=<%=SiteId %>" title="Thêm mới" class="themmoi" > 
            <asp:Label ID="lblAddNew" runat="server" Text="Thêm mới" meta:resourcekey="lblAddNew"></asp:Label>
        </a>
        <asp:LinkButton ID="lbDelete" runat="server" CssClass="xoatin" Text="Xóa" 
            meta:resourcekey="lbDelete" onclick="lbDelete_Click">
        </asp:LinkButton>   		
	</div>
    <div style="text-align:left; width:200px; float:right"><asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                            <ProgressTemplate><img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...</ProgressTemplate>
                            </asp:UpdateProgress></div>
	<div class="clear5px"></div>
    <div class="contenbangdulieu">
    
        <asp:GridView ID="m_grid" DataKeyNames="CategoryCrawlerId" runat="server" ShowHeaderWhenEmpty="True"
            AutoGenerateColumns="False" CssClass="filter-table" 
            OnRowDeleting="m_grid_RowDeleting" OnRowDataBound = "m_grid_OnRowDataBound"
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
                                     ToolTip ='<%# Eval("CategoryCrawlerId").ToString() %>'> </asp:Label>
                    </ItemTemplate>      
                    <ItemStyle HorizontalAlign="Center" Width="3%" />  
                    <HeaderStyle Width="3%" />          
                </asp:TemplateField>  
                <asp:TemplateField>  
                  <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnVoteName" runat="server" Text="Chuyên mục" meta:resourcekey="lblGridColumnVoteName"></asp:Label>
                    </HeaderTemplate> 
                  <ItemTemplate>
                    <%# Categories.Static_Get(short.Parse(Eval("CategoryId").ToString()), l_Cate).CategoryDesc %>
                  </ItemTemplate>
                  <ItemStyle width="20%" HorizontalAlign="Left" />
                </asp:TemplateField>  
                <asp:TemplateField>  
                  <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnDesc" runat="server" Text="Nguồn dữ liệu" meta:resourcekey="lblGridColumnDesc"></asp:Label>
                    </HeaderTemplate> 
                  <ItemTemplate>
                    <%#  DataSources.Static_Get(short.Parse(Eval("DataSourceId").ToString()), l_DataSource).DataSourceDesc %>
                  </ItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField>  
                  <HeaderTemplate>                                
                        Url
                    </HeaderTemplate> 
                  <ItemTemplate>
                    <%# Eval("LinkGetList") %>
                  </ItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField>  
                  <HeaderTemplate>                                
                        Loại
                    </HeaderTemplate> 
                  <ItemTemplate>
                    <%# Eval("LinkType") %>
                  </ItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField>  
                  <HeaderTemplate>                                
                        Số mục tin
                    </HeaderTemplate> 
                  <ItemTemplate>
                    <%# Eval("MaxItem") %>
                  </ItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField> 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnReviewStatusId" runat="server" Text="Trạng thái" meta:resourcekey="lblGridColumnReviewStatusId"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                         <span class="xuatban<%# Eval("StatusId") %>" >
                         <%# Status.Static_Get(byte.Parse(Eval("StatusId").ToString()), l_Status).StatusDesc %></span>
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign = "Left" Wrap="false" />  
                    <HeaderStyle Width="10%" />        
                </asp:TemplateField>  
                    <asp:TemplateField> 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnTime" runat="server" Text="Thời gian" meta:resourcekey="lblGridColumnTime"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <span class="ngaythang">
                            <%# DateTimeHelpers.GetDateAndTime(Eval("CrDateTime"))%>
                        </span> 
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="10%" />        
                </asp:TemplateField>
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnUser" runat="server" Text="Tạo bởi" meta:resourcekey="lblGridColumnUser"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <span class="ngaythang">
                            <%# UserHelpers.Static_Get(int.Parse(Eval("CrUserId").ToString()), l_Users, "").Username %>
                        </span> 
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="10%" />        
                </asp:TemplateField>  
                <asp:TemplateField>
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnOperation" runat="server" Text="Thao tác" meta:resourcekey="lblGridColumnOperation"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <a id="popup" href='CategoryCrawlersEdit.aspx?CategoryCrawlerId=<%# Eval("CategoryCrawlerId") %>' class="iconadmsua"
                        title="Sửa" meta:resourcekey="lnkGridColumnEdit"></a>    
                        <asp:LinkButton ID="lbDelete" runat="server" CssClass="iconadmxoa" CommandName="Delete" ToolTip="Xóa" meta:resourcekey="lnkGridColumnDel"></asp:LinkButton> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="center" Width="8%" Wrap="false" />
                    <HeaderStyle Width="8%" />
                </asp:TemplateField>

                <asp:TemplateField>
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <HeaderStyle Width="5%" />
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
     <div class="clear5px"></div>    
                <uc1:CustomPaging ID="CustomPaging" runat="server" />                
                <div class="clear5px"></div>   
   </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
