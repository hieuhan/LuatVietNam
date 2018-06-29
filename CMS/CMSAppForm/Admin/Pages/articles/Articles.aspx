<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="Articles.aspx.cs" Inherits="Admin_Articles" %>
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
                        //window.location = $('#<%= btnSearch.ClientID %>').attr("href");
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
            $('a.popup3').live('click', function (e) {
                var page = $(this).attr("href");
                cdialog
                .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="100%" height="100%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 300,
                    width: 500,
                    title: $(this).attr("title"),
                    close: function (event, ui) {
                        $(this).remove();
                        //window.location = $('#<%= btnSearch.ClientID %>').attr("href");
                        $('#<%= btnSearch.ClientID %>').click();
                    }
                });
                cdialog.dialog('open');
                e.preventDefault();
            });
            $('a.popup4').live('click', function (e) {
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
                        //window.location = $('#<%= btnSearch.ClientID %>').attr("href");
                        $('#<%= btnSearch.ClientID %>').click();
                    }
                });
                cdialog.dialog('open');
                e.preventDefault();
            });
            $('a.popupDoc').live('click', function (e) {
                var page = $(this).attr("href");
                cdialog
                .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="100%" height="100%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 630,
                    width: 1250,
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
        function submitButton(event) {
            if (event.which == 13) {
                $('#<%= btnSearch.ClientID %>').click();
            }
        }
    </script>
    <div style="text-align:center; padding:5px;"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
<asp:Panel ID="panSearch" runat="server" DefaultButton="btnSearch" Width="100%" >
    <table cellpadding="2" cellspacing="0" style="width:100%">
            <tr>
                <td style="width:60px"><asp:Label ID="lblSite" runat="server" Text="Site:" meta:resourcekey="lblSite"></asp:Label>
                </td>
                <td style="width:260px">
                    <asp:DropDownList ID="ddlSite" runat="server" DataTextField="SiteDesc" 
                        DataValueField="SiteId" Width="250px" CssClass="userselect"
                        AutoPostBack="True" onselectedindexchanged="ddlSite_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td style="width:90px; white-space:nowrap;">    
                    <asp:Label ID="lblDataType" runat="server" Text="Loại dữ liệu:" meta:resourcekey="lblDataType" Visible="false"></asp:Label>
                    <asp:Label ID="Label3" runat="server" Text="User:" ></asp:Label>
                </td>
                <td>
                    <asp:DropDownList runat="server" AutoPostBack="true"
                                        ID="ddlUser" Width="250px" DataTextField="Username" 
                                        DataValueField="UserId" OnSelectedIndexChanged="ddlUser_SelectedIndexChanged"></asp:DropDownList>
                    <asp:DropDownList ID="ddlDataType" runat="server" Visible="false"
                        DataTextField="DataTypeDesc" DataValueField="DataTypeId" 
                        Width="250px" CssClass="userselect" AutoPostBack="True" 
                            onselectedindexchanged="ddlDataType_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:CheckBox ID="chkShowTop" runat="server" Text="ShowTop" />&nbsp;
                    <asp:CheckBox ID="chkShowBottom" runat="server" Text="ShowBottom" />&nbsp;
                    
                </td>
            </tr>
            <tr id="trArticleType" runat="server">
            <td style="width:60px; white-space:nowrap;">
                <asp:Label ID="Label1" runat="server" Text="Loại tin:" ></asp:Label>
                </td>
            <td style="width:260px">
                <asp:DropDownList ID="ddlArticleType" runat="server" DataTextField="ArticleTypeName" 
                    DataValueField="ArticleTypeId" Width="250px" CssClass="userselect" 
                    AutoPostBack="True" OnSelectedIndexChanged="ddlArticleType_SelectedIndexChanged" >
                </asp:DropDownList>
                </td>
            <td style="width:90px; white-space:nowrap;"><asp:Label ID="Label2" runat="server" Text="Icon:" ></asp:Label>
                </td>
            <td><asp:DropDownList ID="ddlIconStatus" runat="server" 
                    DataTextField="IconStatusName" DataValueField="IconStatusId" 
                    Width="250px" CssClass="userselect" AutoPostBack="True" OnSelectedIndexChanged="ddlIconStatus_SelectedIndexChanged" >
                </asp:DropDownList>

                <asp:CheckBox ID="chkShowWeb" runat="server" Text="ShowWeb" />&nbsp;
                <asp:CheckBox ID="chkShowWap" runat="server" Text="ShowWap" />&nbsp;
                <asp:CheckBox ID="chkShowApp" runat="server" Text="ShowApp" />&nbsp;
                </td>
        </tr>
            <tr id="trLanguage" runat="server">
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
        <tr id="trCategory" runat="server">
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
                <asp:CheckBox ID="chkIsVerify" runat="server" Text="Đã kiểm tra thông tin" />
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
                <asp:Button ID="btnSearch" runat="server" CssClass="timkiembutom" 
                    Text="Tìm kiếm" meta:resourcekey="btnSearch" onclick="btnSearch_Click">
                        </asp:Button>
                </td>
        </tr>
    </table>
    </asp:Panel>
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
        <a href="ArticlesEdit.aspx?SiteId=<%=SiteId %>&DataTypeId=<%=DataTypeId %>&EnableDataType=<%=EnableDataType %>&ActionId=<%=ActionId %>&LanguageId=<%=LanguageId %>&AppTypeId=<%=AppTypeId %>&CategoryId=<%=CategoryId %>" title="<%=GetLocalResourceObject("lnkAddNew.title").ToString() %>" class="themmoi" > 
            <asp:Label ID="lblAddNew" runat="server" Text="Thêm mới" meta:resourcekey="lblAddNew"></asp:Label>
        </a>       
        <asp:LinkButton ID="lbReview" runat="server" CssClass="duyettin" Text="Duyệt" 
            meta:resourcekey="lbReview" onclick="lbReview_Click"> 
        </asp:LinkButton>
        <asp:LinkButton ID="lbUnCheck" runat="server" CssClass="boduyet" 
            Text="Bỏ duyệt" meta:resourcekey="lbUnCheck" onclick="lbUnCheck_Click"> 
        </asp:LinkButton>
        <asp:LinkButton ID="lbDelete" runat="server" CssClass="xoatin" Text="Xóa" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa tất cả các bài viết đã chọn?')" 
            meta:resourcekey="lbDelete" onclick="lbDelete_Click">
        </asp:LinkButton>		
	</div>
    <div style="text-align:left; width:200px; float:right"></div>
	<div class="clear5px"></div>
    <div class="contenbangdulieu">
    
        <asp:GridView ID="m_grid" DataKeyNames="ArticleId" runat="server" ShowHeaderWhenEmpty="True" PageSize="50"
            AutoGenerateColumns="False" CssClass="filter-table" OnRowDeleting="m_grid_RowDeleting" OnRowDataBound = "m_grid_OnRowDataBound" OnRowCommand="m_grid_OnRowCommand"
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
                        <a class="popup4" style="color:#000;" title="Xem trước" href='ArticlesPreview.aspx?ArticleId=<%# Eval("ArticleId") %>&SiteId=<%=SiteId %>&DataTypeId=<%=DataTypeId %>&EnableDataType=<%=EnableDataType %>&ActionId=<%=ActionId %>&LanguageId=<%# Eval("LanguageId") %>&AppTypeId=<%# Eval("ApplicationTypeId").ToString() %>'>
                            <b><%# Eval("Title") %></b>
                        </a>
                        <div style="float: right;" >
                        <a onmouseover="ShowMenu('-2-<%# Eval("ArticleId") %>')" href='ArticlesEdit.aspx?ArticleId=<%# Eval("ArticleId") %>&SiteId=<%=SiteId %>&DataTypeId=<%=DataTypeId %>&EnableDataType=<%=EnableDataType %>&ActionId=<%=ActionId %>&LanguageId=<%# Eval("LanguageId") %>&AppTypeId=<%# Eval("ApplicationTypeId").ToString() %>' class="iconadmsua"
                        title=""></a>  
                        <div  class="dropdown dropdown-tip dropdown-anchor-right" id="dropdown-2-<%# Eval("ArticleId").ToString() %>" style="display: none; text-align:left;" >
		                <ul class="dropdown-menu">	
                            <li>
                                <asp:HyperLink id="popup" runat="server" NavigateUrl='<%# "ArticleCategories.aspx?SiteId=" + Eval("SiteId").ToString() + "&ArticleId=" + Eval("ArticleId").ToString() + "&LanguageId=" + Eval("LanguageId").ToString() + "&DataTypeId=" + Eval("DataTypeId").ToString() %>' CssClass="popup"
                                 meta:resourcekey="lnkGridColumnEditCategory">Chọn chuyên mục
                                </asp:HyperLink>
                            </li>
                            <li>
                                <asp:HyperLink id="popup2" runat="server" NavigateUrl='<%# "ArticleRelates.aspx?ArticleId=" + Eval("ArticleId").ToString() + "&LanguageId=" + Eval("LanguageId").ToString() + "&AppTypeId=" + Eval("ApplicationTypeId").ToString() + "&SiteId=" + Eval("SiteId").ToString() + "&DataTypeId=" + Eval("DataTypeId").ToString() %>'  CssClass="popup2"
                                 meta:resourcekey="lnkGridColumnEditRelate">Chọn tin liên quan
                                </asp:HyperLink>
                            </li>
                            <li>
                                <asp:HyperLink id="HyperLink1" runat="server" NavigateUrl='<%# "ArticleDisplayTypesSelect.aspx?ArticleId=" + Eval("ArticleId").ToString() + "&LanguageId=" + Eval("LanguageId").ToString() + "&AppTypeId=" + Eval("ApplicationTypeId").ToString() + "&DataTypeId=" + Eval("DataTypeId").ToString() %>' CssClass="popup"
                                 title="Chọn hiển thị">Chọn hiển thị
                                </asp:HyperLink>
                            </li>
                            <li>
                                <asp:HyperLink id="popup3" runat="server" NavigateUrl='<%# "ArticleEventStreams.aspx?SiteId=" + Eval("SiteId").ToString() + "&ArticleId=" + Eval("ArticleId").ToString() + "&LanguageId=" + Eval("LanguageId").ToString() + "&DataTypeId=" + Eval("DataTypeId").ToString() %>'  CssClass="popup"
                                 title="Chọn sự kiện">Chọn sự kiện
                                </asp:HyperLink>
                            </li>
                            <li>
                                <asp:HyperLink id="HyperLink2" runat="server" NavigateUrl='<%# "ArticleFeatures.aspx?ArticleId=" + Eval("ArticleId").ToString() + "&LanguageId=" + Eval("LanguageId").ToString() + "&AppTypeId=" + Eval("ApplicationTypeId").ToString() + "&DataTypeId=" + Eval("DataTypeId").ToString() %>' CssClass="popup"
                                 title="Thuộc tính mở rộng">Thuộc tính mở rộng
                                </asp:HyperLink>
                            </li>
                            <li>
                                <asp:HyperLink id="popup4" runat="server" NavigateUrl='<%# "ArticleMedias.aspx?ArticleId=" + Eval("ArticleId").ToString() %>'  CssClass="popup2"
                                 title="File media">File media
                                </asp:HyperLink>
                            </li>
                            <li>
                                <asp:HyperLink id="popupDoc" runat="server" NavigateUrl='<%# "ArticleDocUpdate.aspx?ArticleId=" + Eval("ArticleId").ToString() + "&LanguageId=" + Eval("LanguageId").ToString() + "&AppTypeId=" + Eval("ApplicationTypeId").ToString() + "&SiteId=" + Eval("SiteId").ToString() + "&DataTypeId=" + Eval("DataTypeId").ToString() %>'  CssClass="popupDoc"
                                 title="Chọn văn bản cập nhật hàng tuần">Chọn văn bản cập nhật hàng tuần
                                </asp:HyperLink>
                            </li>
                            <li>
                                <asp:HyperLink id="popupDoc1" runat="server" NavigateUrl='<%# "ArticleDocUpdate.aspx?GenType=Hot&ArticleId=" + Eval("ArticleId").ToString() + "&LanguageId=" + Eval("LanguageId").ToString() + "&AppTypeId=" + Eval("ApplicationTypeId").ToString() + "&SiteId=" + Eval("SiteId").ToString() + "&DataTypeId=" + Eval("DataTypeId").ToString() %>'  CssClass="popupDoc"
                                 title="Chọn văn bản tiêu biểu">Chọn văn bản tiêu biểu
                                </asp:HyperLink>
                            </li>
                            <li>
                                <asp:HyperLink id="HyperLink3" runat="server" NavigateUrl='<%# "ArticleDocUpdate.aspx?GenType=Newest&ArticleId=" + Eval("ArticleId").ToString() + "&LanguageId=" + Eval("LanguageId").ToString() + "&AppTypeId=" + Eval("ApplicationTypeId").ToString() + "&SiteId=" + Eval("SiteId").ToString() + "&DataTypeId=" + Eval("DataTypeId").ToString() %>'  CssClass="popupDoc"
                                 title="Chọn điểm tin văn bản mới">Chọn điểm tin văn bản mới
                                </asp:HyperLink>
                            </li>
                            <li>
                                <asp:HyperLink id="HyperLink4" runat="server" NavigateUrl='<%# "../lawsdocs/ShowGenBTHL.aspx?ActionType=Edit&ArticleId=" + Eval("ArticleId").ToString() + "&LanguageId=" + Eval("LanguageId").ToString() + "&AppTypeId=" + Eval("ApplicationTypeId").ToString() + "&SiteId=" + Eval("SiteId").ToString() + "&DataTypeId=" + Eval("DataTypeId").ToString() %>'  CssClass="popupDoc"
                                 title="Chọn điểm tin văn bản mới">Chọn văn bản Bản tin hiệu lực 
                                </asp:HyperLink>
                            </li>
                        </ul>
                        </div>
                        <a class="dropdown-menu-hover-2-<%# Eval("ArticleId") %>" href="#" data-dropdown="#dropdown-2-<%# Eval("ArticleId") %>" > </a>
                        </div>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnCategory" runat="server" Text="View AMP" ></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <a href="<%=CmsConstants.ROOT_PATH %>admin/pages/articles/ArticlesPreviewAMP.aspx?ArticleId=<%# Eval("ArticleId") %>" target="_blank" class="" style="color:blue;" title="Check AMP">- Preview AMP</a><br />    
                        <a href="<%=CmsConstants.ROOT_PATH %>admin/pages/WebNotify/NotifyMessagesEdit.aspx?ArticleId=<%# Eval("ArticleId") %>" class="popup" style="color:blue;" title="Gửi Web Notify">- Gửi Notify</a>    
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" Width="100px" />
                    <HeaderStyle Width="65px" />          
                </asp:TemplateField>  
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnCategory" runat="server" Text="Chuyên mục" meta:resourcekey="lblGridColumnCategory"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Categories.Static_GetListDesc(Eval("CategoryId").ToString(), l_Categories) %>
                        <asp:Label ID="lblCatName" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle Width="150px" />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        Ảnh
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lblImage" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Center" />
                    <HeaderStyle Width="60px" />          
                </asp:TemplateField> 
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnStatus" runat="server" Text="Trạng thái" meta:resourcekey="lblGridColumnStatus"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <span class="xuatban<%# Eval("ReviewStatusId") %>" >
                            <%# LanguageHelpers.IsCultureVietnamese() ? ReviewStatus.Static_Get(byte.Parse(Eval("ReviewStatusId").ToString()), l_ReviewStatus).ReviewStatusDesc : ReviewStatus.Static_Get(byte.Parse(Eval("ReviewStatusId").ToString()), l_ReviewStatus).ReviewStatusName %>
                        </span> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign = "Left" Wrap="false" />  
                    <HeaderStyle Width="80px" />        
                </asp:TemplateField>  
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        Sắp xếp
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <asp:LinkButton ID="lbTop" runat="server" CommandName="OrderTop" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" ToolTip='<%# ddlCategory.SelectedValue=="0" ? "Cần chọn chuyên mục trước khi sắp xếp":"" %>' Enabled='<%# ddlCategory.SelectedValue=="0" ? false:true %>' ><img src="../../Images/uptop.gif" /></asp:LinkButton>
                        <asp:LinkButton ID="lbUp" runat="server" CommandName="OrderUp" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" ToolTip='<%# ddlCategory.SelectedValue=="0" ? "Cần chọn chuyên mục trước khi sắp xếp":"" %>' Enabled='<%# ddlCategory.SelectedValue=="0" ? false:true %>' ><img src="../../Images/stepup.gif" /></asp:LinkButton>
                        <asp:LinkButton ID="lbDown" runat="server" CommandName="OrderDown" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" ToolTip='<%# ddlCategory.SelectedValue=="0" ? "Cần chọn chuyên mục trước khi sắp xếp":"" %>' Enabled='<%# ddlCategory.SelectedValue=="0" ? false:true %>' ><img src="../../Images/stepdown.gif" /></asp:LinkButton>
                        <asp:LinkButton ID="lbBottom" runat="server" CommandName="OrderBottom" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" ToolTip='<%# ddlCategory.SelectedValue=="0" ? "Cần chọn chuyên mục trước khi sắp xếp":"" %>' Enabled='<%# ddlCategory.SelectedValue=="0" ? false:true %>' ><img src="../../Images/upbottom.gif" /></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="5%" HorizontalAlign = "Left" Wrap="false" />  
                    <HeaderStyle Width="5%" />        
                </asp:TemplateField>   
                <asp:TemplateField  >   
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnUser" runat="server" Text="Tạo bởi" meta:resourcekey="lblGridColumnUser"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <span class="ngaythang">
                            <font style="color:Blue"><%# UserHelpers.Static_Get(int.Parse(Eval("CrUserId").ToString()), l_Users, "").Username %></font><br />
                            <%# DateTimeHelpers.GetDateAndTime(Eval("CrDateTime")) %>
                        </span> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="80px" />        
                </asp:TemplateField>
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnUserUpdate" runat="server" Text="Sửa bởi" meta:resourcekey="lblGridColumnUserUpdate"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <span class="ngaythang">
                            <font style="color:Blue"><%# UserHelpers.Static_Get(int.Parse(Eval("UpdUserId").ToString()), l_Users, "").Username %></font><br />
                            <%# DateTimeHelpers.GetDateAndTime(Eval("UpdDateTime")) %>
                        </span> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="80px" />        
                </asp:TemplateField> 
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnUserReview" runat="server" Text="Duyệt bởi" meta:resourcekey="lblGridColumnUserReview"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <span class="ngaythang">
                            <font style="color:Blue"><%# UserHelpers.Static_Get(int.Parse(Eval("RevUserId").ToString()), l_Users, "").Username %></font><br />
                            <%# DateTimeHelpers.GetDateAndTime(Eval("RevDateTime")) %>
                            <a  title="Sửa thời gian xuất bản" class="popup3 iconadmsua" 
                                href="ArticlesEditRevTime.aspx?ArticleId=<%# Eval("ArticleId") %>&SiteId=<%=SiteId %>&DataTypeId=<%=DataTypeId %>&EnableDataType=<%=EnableDataType %>&ActionId=<%=ActionId %>&LanguageId=<%# Eval("LanguageId") %>&AppTypeId=<%# Eval("ApplicationTypeId").ToString() %>">
                               
                                </a>
                        </span> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="80px" />        
                </asp:TemplateField>                        
                <asp:TemplateField Visible="false">
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnOperation" runat="server" Text="Thao tác" meta:resourcekey="lblGridColumnOperation"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <a onmouseover="ShowMenu('-1-<%# Eval("ArticleId") %>')" href='ArticlesEdit.aspx?ArticleId=<%# Eval("ArticleId") %>&SiteId=<%=SiteId %>&DataTypeId=<%=DataTypeId %>&EnableDataType=<%=EnableDataType %>&ActionId=<%=ActionId %>&LanguageId=<%# Eval("LanguageId") %>&AppTypeId=<%# Eval("ApplicationTypeId").ToString() %>' class="iconadmsua"
                        title="<%# GetLocalResourceObject("lnkGridColumnEdit.title").ToString() %>" meta:resourcekey="lnkGridColumnEdit"></a>    
                        <asp:LinkButton ID="lbDelete" runat="server" CssClass="iconadmxoa" CommandName="Delete" ToolTip="Xóa" meta:resourcekey="lnkGridColumnDel"></asp:LinkButton> 
                        <asp:Label ID="lblLanguageId" runat="server" Text='<%# Eval("LanguageId") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lblAppTypeId" runat="server" Text='<%# Eval("ApplicationTypeId") %>' Visible="false"></asp:Label>
                        <asp:Repeater ID="rptLanguage" runat="server">
                            <HeaderTemplate>
                            <div  class="dropdown dropdown-tip dropdown-anchor-right" id="dropdown-1-<%# ArticleId %>" style="display: none; text-align:left;" >
		                        <ul class="dropdown-menu">	
                            </HeaderTemplate>
                            <ItemTemplate>
                                <li>
                                    <a href="ArticlesEdit.aspx?ArticleId=<%# ArticleId %>&SiteId=<%=SiteId %>&DataTypeId=<%=DataTypeId %>&EnableDataType=<%=EnableDataType %>&ActionId=<%=ActionId %>&LanguageId=<%# Eval("LanguageId") %>&AppTypeId=<%# AppTypeId %>" 
                                    title="<%# GetLocalResourceObject("lnkGridColumnEdit.title").ToString() %>" >
                                        <img src="../../<%# Eval("IconPath").ToString() %>" title="" alt="" /> 
                                            <%# GetLocalResourceObject("EditLanguage").ToString() + Eval("LanguageDesc").ToString() %> 
                                    </a>
                                </li>
                            </ItemTemplate>
                            <FooterTemplate>
                                    </ul>
	                        </div>        
                            </FooterTemplate>
                        </asp:Repeater>
                        <br />
                        <a class="dropdown-menu-hover-1-<%# Eval("ArticleId") %>" href="#" data-dropdown="#dropdown-1-<%# Eval("ArticleId") %>" > 
                        </a>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="center" Width="68px" Wrap="false" />
                    <HeaderStyle Width="68px" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="30px" />
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
   </div>
</asp:Content>
