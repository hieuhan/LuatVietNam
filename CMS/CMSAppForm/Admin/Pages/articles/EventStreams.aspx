<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="EventStreams.aspx.cs" Inherits="Admin_EventStreams" %>
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
            $("#<%= txtDateFrom.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
            $("#<%= txtDateTo.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
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
                        //window.location = $('#<%= btnSearch.ClientID %>').attr("href");
                        $('#<%= btnSearch.ClientID %>').click();
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
    <div style="text-align:center"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
    <table cellpadding="2" cellspacing="0" style="width:100%">
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
                <asp:Label ID="lblParentCategory" runat="server" 
                    meta:resourcekey="lblParentCategory" Text="Chuyên mục:"></asp:Label>
                
            </td>
            <td >
                <asp:DropDownList ID="ddlParentCategory" runat="server" CssClass="userselect"
                    DataTextField="CategoryDesc" DataValueField="CategoryId" Width="250px">
                </asp:DropDownList>
                
            </td>
           
            <td>&nbsp;
                </td>
                <td>&nbsp;
                </td>
        </tr>
        <tr id="trLanguage" runat="server" visible="false">
         <td style="width:80px; white-space:nowrap;">
                <asp:Label ID="lblLanguage" runat="server" meta:resourcekey="lblLanguage" 
                    Text="Ngôn ngữ:"></asp:Label>
                
            </td>
            <td style="width:260px">
                <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="LanguageDesc" DataValueField="LanguageId" 
                    onselectedindexchanged="ddlLanguage_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
                
            </td>
            <td style="width:90px; white-space:nowrap;">
                <asp:Label ID="lblAppType" runat="server" meta:resourcekey="lblAppType" 
                    Text="Loại ứng dụng:"></asp:Label>
                </td>
            <td >
                <asp:DropDownList ID="ddlAppType" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="ApplicationTypeDesc" 
                    DataValueField="ApplicationTypeId" 
                    onselectedindexchanged="ddlAppType_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
           
            <td>&nbsp;
                </td>
                <td>&nbsp;
                </td>
        </tr>
        
        <tr>
            <td>
                <asp:Label ID="lblDateFrom" runat="server" 
                    meta:resourcekey="lblDateFrom" Text="Từ ngày:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDateFrom" runat="server" CssClass="tukhoatimekiem" Width="100px"></asp:TextBox>
                <asp:Label ID="lblDateTo" runat="server" meta:resourcekey="lblDateTo" 
                    Text="Đến:"></asp:Label>
                <asp:TextBox ID="txtDateTo" runat="server" CssClass="tukhoatimekiem" 
                    Width="100px"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblContentSearch" runat="server" 
                    meta:resourcekey="lblContentSearch" Text="Từ khóa:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtContentSearch" runat="server" CssClass="tukhoatimekiem" Width="240px"></asp:TextBox>
                </td>
                <td>&nbsp;
                </td>
                <td>&nbsp;
                </td>
        </tr>
        <tr>
         <td style="width:80px; white-space:nowrap;">
                <asp:Label ID="lblReviewStatus" runat="server" 
                    meta:resourcekey="lblReviewStatus" Text="Trạng thái:"></asp:Label>
            </td>
            <td style="width:260px;">
                <asp:DropDownList ID="ddlReviewStatus" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="ReviewStatusDesc" 
                    DataValueField="ReviewStatusId" 
                    onselectedindexchanged="ddlReviewStatus_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lblOrderBy" runat="server" meta:resourcekey="lblOrderBy" 
                    Text="Sắp xếp:"></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlOrderBy" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="OrderByDesc" DataValueField="OrderBy" 
                    onselectedindexchanged="ddlOrderBy_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
                </td>
            <td style="width:550px;">
                <asp:Button ID="btnSearch" runat="server" CssClass="timkiembutom" 
                    meta:resourcekey="btnSearch" onclick="btnSearch_Click" Text="Tìm kiếm">
                        </asp:Button>
                </td>
            <td>&nbsp;
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
        <a id="popup" href="EventStreamsEdit.aspx?SiteId=<%=SiteId %>&LanguageId=<%=LanguageId %>&AppTypeId=<%=AppTypeId %>" title="<%=GetLocalResourceObject("lnkAddNew.title").ToString() %>" class="themmoi" > 
            <asp:Label ID="lblAddNew" runat="server" Text="Thêm mới" meta:resourcekey="lblAddNew"></asp:Label>
        </a>
        <asp:LinkButton ID="lbDelete" runat="server" CssClass="xoatin" Text="Xóa" 
            meta:resourcekey="lbDelete" onclick="lbDelete_Click">
        </asp:LinkButton>   		
	</div>
	<div class="clear5px"></div>
    <div class="contenbangdulieu">
    
        <asp:GridView ID="m_grid" DataKeyNames="EventStreamId" runat="server" ShowHeaderWhenEmpty="True"
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
                                     ToolTip ='<%# Eval("EventStreamId").ToString() %>'> </asp:Label>
                    </ItemTemplate>      
                    <ItemStyle HorizontalAlign="Center" Width="3%" />  
                    <HeaderStyle Width="3%" />          
                </asp:TemplateField>  
                <asp:TemplateField>  
                  <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnVoteName" runat="server" Text="Tên" meta:resourcekey="lblGridColumnVoteName"></asp:Label>
                    </HeaderTemplate> 
                  <ItemTemplate>
                    <asp:Literal ID="ltEventStreamName" runat="server" EnableViewState="false" Text='<%# Eval("EventStreamName") %>'></asp:Literal> 
                  </ItemTemplate>
                  <ItemStyle width="20%" HorizontalAlign="Left" />
                </asp:TemplateField>  
                <asp:TemplateField>  
                  <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnDesc" runat="server" Text="Mô tả" meta:resourcekey="lblGridColumnDesc"></asp:Label>
                    </HeaderTemplate> 
                  <ItemTemplate>
                    <asp:Literal ID="ltEventStreamDesc" runat="server" EnableViewState="false" Text='<%# Eval("EventStreamDesc") %>'></asp:Literal>
                  </ItemTemplate>
                  <ItemStyle width="40%" HorizontalAlign="Left" />
                </asp:TemplateField> 
                <asp:TemplateField> 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnReviewStatusId" runat="server" Text="Trạng thái" meta:resourcekey="lblGridColumnReviewStatusId"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                         <span class="xuatban<%# Eval("ReviewStatusId") %>" >
                         <%# LanguageHelpers.IsCultureVietnamese() ? ReviewStatus.Static_Get(byte.Parse(Eval("ReviewStatusId").ToString()), l_ReviewStatus).ReviewStatusDesc : ReviewStatus.Static_Get(byte.Parse(Eval("ReviewStatusId").ToString()), l_ReviewStatus).ReviewStatusName %></span>
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
                            <%# DateTimeHelpers.GetDateAndTime(Eval("CrDateTime"))%><br />
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
                        <a id="popup" onmouseover="ShowMenu('-1-<%# Eval("EventStreamId") %>')" href='EventStreamsEdit.aspx?EventStreamId=<%# Eval("EventStreamId") %>&LanguageId=<%# Eval("LanguageId") %>&AppTypeId=<%# Eval("ApplicationTypeId").ToString() %>' class="iconadmsua"
                        title="<%# GetLocalResourceObject("lnkGridColumnEdit.title").ToString() %>" meta:resourcekey="lnkGridColumnEdit"></a>    
                        <asp:LinkButton ID="lbDelete" runat="server" CssClass="iconadmxoa" CommandName="Delete" ToolTip="Xóa" meta:resourcekey="lnkGridColumnDel"></asp:LinkButton> 
                         <asp:Label ID="lblLanguageId" runat="server" Text='<%# Eval("LanguageId") %>' Visible="false"></asp:Label>
                          <asp:Label ID="lblAppTypeId" runat="server" Text='<%# Eval("ApplicationTypeId") %>' Visible="false"></asp:Label>
                        <asp:Repeater ID="rptLanguage" runat="server" Visible="false">
                            <HeaderTemplate>
                            <div  class="dropdown dropdown-tip dropdown-anchor-right" id="dropdown-1-<%# EventStreamId %>" style="display: none; text-align:left;" >
		                        <ul class="dropdown-menu">	
                            </HeaderTemplate>
                            <ItemTemplate>
                                <li>
                                    <a id="popup" href="EventStreamsEdit.aspx?EventStreamId=<%# EventStreamId %>&LanguageId=<%# Eval("LanguageId") %>&AppTypeId=<%# AppTypeId %>" 
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
                        <a class="dropdown-menu-hover-1-<%# Eval("EventStreamId") %>" href="#" data-dropdown="#dropdown-1-<%# Eval("EventStreamId") %>" > 
                        </a>
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
</asp:Content>
