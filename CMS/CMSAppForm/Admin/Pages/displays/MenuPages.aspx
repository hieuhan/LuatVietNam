<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="MenuPages.aspx.cs" Inherits="Admin_MenuPages" %>
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

            $('a#popup').live('click', function (e) {
                var page = $(this).attr("href");
                cdialog
                .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="98%" height="98%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 520,
                    width: 750,
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
        SetStartup();
    }
    </script>
    <asp:UpdatePanel ID="upn_Grid" runat="server">
    <ContentTemplate>
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
            <td style="width:90px; white-space:nowrap;">
                <asp:Label ID="lblAppType" runat="server" Text="Loại ứng dụng:" meta:resourcekey="lblAppType"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlAppType" runat="server" 
                    DataTextField="ApplicationTypeDesc" DataValueField="ApplicationTypeId" 
                    Width="250px" CssClass="userselect" AutoPostBack="True" 
                    onselectedindexchanged="ddlAppType_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblMenuName" runat="server" Text="Trạng thái:" meta:resourcekey="lblMenuName"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlMenu" runat="server" 
                    DataTextField="MenuName" DataValueField="MenuId" Width="250px" 
                    CssClass="userselect" AutoPostBack="True" 
                    onselectedindexchanged="ddlMenu_SelectedIndexChanged" >
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lblParentPage" runat="server" Text="Trang cấp cha:" meta:resourcekey="lblParentPage"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlParentPage" runat="server" CssClass="userselect" 
                    DataTextField="PageName" DataValueField="PageId" AutoPostBack="true"
                    Width="250px" onselectedindexchanged="ddlParentPage_SelectedIndexChanged"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblSearch" runat="server" Text="Tìm kiếm:" meta:resourcekey="lblSearch"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtSearch" runat="server" CssClass="tukhoatimekiem input-filter"></asp:TextBox>
                &nbsp;&nbsp;
                
            </td>
            <td>
                <asp:LinkButton ID="btnSearch" runat="server" CssClass="timkiembutom" 
                Text="Tìm kiếm" meta:resourcekey="btnSearch" onclick="btnSearch_Click">
                    </asp:LinkButton>
            </td>
            <td>
               
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
        <%=GetLocalResourceObject("Pages").ToString() %>
	</div>
	<div class="chucnangright">
        <asp:LinkButton ID="lbDelete" runat="server" CssClass="xoatin" Text="Xóa" 
            meta:resourcekey="lbDelete" onclick="lbDelete_Click">
        </asp:LinkButton>        
		<a id="popup" href="MenuPagesEdit.aspx?LanguageId=<%=LanguageId %>&AppTypeId=<%=AppTypeId %>&MenuId=<%= MenuId %>" title="<%=GetLocalResourceObject("lnkAddNew.title").ToString() %>" class="themmoi" > 
            <asp:Label ID="lblAddNew" runat="server" Text="Thêm mới" meta:resourcekey="lblAddNew"></asp:Label>
        </a>
	</div>
    <div style="text-align:left; width:200px; float:right"><asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                            <ProgressTemplate><img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...</ProgressTemplate>
                            </asp:UpdateProgress></div>
	<div class="clear5px"></div>
    <div class="contenbangdulieu">
    
        <asp:GridView ID="m_grid" DataKeyNames="PageId" runat="server" ShowHeaderWhenEmpty="True"
            AutoGenerateColumns="False" CssClass="jquery-filter-table" OnRowDeleting="m_grid_RowDeleting" OnRowDataBound = "m_grid_OnRowDataBound"
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
                        <asp:Label ID="lblGridColumnName" runat="server" Text="Tên trang" meta:resourcekey="lblGridColumnName"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lbCateNameTree" runat="server" style="display:none"></asp:Label>                        
                        <%# Eval("PageName") %> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" Width="35%" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnPageType" runat="server" Text="Loại trang" meta:resourcekey="lblGridColumnPageType"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# LanguageHelpers.IsCultureVietnamese() ? PageTypes.Static_Get(l_PageTypes, byte.Parse(Eval("PageTypeId").ToString())).PageTypeDesc : PageTypes.Static_Get(l_PageTypes, byte.Parse(Eval("PageTypeId").ToString())).PageTypeName %>
                       <%-- - 
                        <%# GetUrl(Eval("PageTypeId").ToString(),Eval("PageId").ToString(),Eval("Url").ToString()) %> --%>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
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
                    <ItemStyle Width="10%" HorizontalAlign = "Left" Wrap="false" />  
                    <HeaderStyle Width="10%" />        
                </asp:TemplateField>          
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnTime" runat="server" Text="Thời gian" meta:resourcekey="lblGridColumnTime"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <span class="ngaythang">
                            <%# DateTime.Parse(Eval("CrDateTime").ToString()).ToString("g", System.Threading.Thread.CurrentThread.CurrentCulture)%>
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
                        <asp:LinkButton ID="lbDelete" runat="server" CssClass="iconadmxoa" CommandName="Delete" ToolTip="Xóa" meta:resourcekey="lnkGridColumnDel"></asp:LinkButton> 
                        <asp:Label ID="lblLanguageId" runat="server" Text='<%# Eval("LanguageId") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lblAppTypeId" runat="server" Text='<%# Eval("ApplicationTypeId") %>' Visible="false"></asp:Label>
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
   </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
