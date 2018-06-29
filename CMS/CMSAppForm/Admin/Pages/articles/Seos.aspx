<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="Seos.aspx.cs" Inherits="Admin_Seos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/admin/UserControls/CustomPaging.ascx" TagName="CustomPaging"
    TagPrefix="uc1" %>
<%@ Import Namespace="ICSoft.CMSLib" %>
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
                    height: 630,
                    width: 600,
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
    });
    function InitializeRequest(sender, args) {
    }

    function EndRequest(sender, args) {

    }
    function submitButton(event) {
        if (event.which == 13) {
            $('#<%= btnSearch.ClientID %>').click();
	}
}
    </script>
    <div style="text-align:center"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
        <table cellpadding="4" cellspacing="0" style="width: 100%">
            <tr>
                <td style="width: 90px; white-space: nowrap;">
                    <asp:Label ID="lblSite" runat="server"  Text="Site:"></asp:Label>
                </td>
                <td style="width: 260px; white-space: nowrap;">
                    <asp:DropDownList ID="ddlSite" runat="server" AutoPostBack="True" CssClass="userselect" OnSelectedIndexChanged="ddlOrderBy_SelectedIndexChanged"
                            Width="250px" DataTextField="SiteName" DataValueField="SiteId">
                        </asp:DropDownList>
                </td>
                <td style="width: 90px; white-space: nowrap;">
                    <asp:Label ID="lblOrderBy" runat="server"  Text="Sắp xếp:"></asp:Label>
                </td>
                <td style="white-space: nowrap;">
                    <asp:DropDownList ID="ddlOrderBy" runat="server" AutoPostBack="True" CssClass="userselect" OnSelectedIndexChanged="ddlOrderBy_SelectedIndexChanged"
                            Width="250px">
                            <asp:ListItem Value="SeoId desc"> Mới nhất</asp:ListItem>
                            <asp:ListItem Value="SeoName asc"> Theo tên</asp:ListItem>
                        </asp:DropDownList>
                </td>
                </tr>
            <tr>
                
                <td style="width: 90px; white-space: nowrap;">
                    <asp:Label ID="lblkeyword" runat="server" Text="Seo Name:"></asp:Label>
                </td>
                <td style="width: 260px; white-space: nowrap;">
                    <asp:TextBox ID="txtKey" runat="server" Width="240px" Text="" CssClass="tukhoatimekiem" />
                </td>
                <td style="width: 90px; white-space: nowrap;">
                    <asp:Label ID="lblUrl" runat="server" Text="Url:"></asp:Label>
                </td>
                <td style=" white-space: nowrap;">
                    <asp:TextBox ID="txtUrl" runat="server" Width="240px" Text="" CssClass="tukhoatimekiem" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" CssClass="timkiembutom" Text="Tìm kiếm" OnClick="btnSearch_Click">
                    </asp:Button>
                    <asp:LinkButton ID="btnRefresh" runat="server" CssClass="timkiembutom hidden-btn-dqs" Text="Làm mời" onclick="btnRefresh_Click"></asp:LinkButton>	
                </td>

            </tr>
        </table>
    <div class="clear5px"></div>
	<div class="vien"></div>
	<div class="clear5px"></div>  
    <div class="khungchucnang">
    <div class="chucnangleft">
		<span class="tieudetongcong">
            <asp:Label ID="lblTotalText" runat="server" Text="Tổng cộng:" ></asp:Label>
        </span>
        <asp:Label ID="lblTong" runat="server" Text="" CssClass="tieudetongcong2"></asp:Label> 
	</div>
	<div class="chucnangright">
		<a id="popup" href="Seos_Edit.aspx?SiteId=<%=siteId %>" title="Thêm mới" class="themmoi" > 
            <asp:Label ID="lblAddNew" runat="server" Text="Thêm mới" ToolTip="AddNew"></asp:Label>
        </a>
	</div>
	<div class="clear5px"></div>
    <div class="contenbangdulieu">
    
        <asp:GridView ID="m_grid" DataKeyNames="SeoId" runat="server" ShowHeaderWhenEmpty="True"
            AutoGenerateColumns="False" CssClass="filter-table" 
            OnRowDeleting="m_grid_RowDeleting" OnRowDataBound = "m_grid_OnRowDataBound"
            Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" PageSize="30" >
            <HeaderStyle CssClass="trbangdulieutieude" />
            <RowStyle CssClass="trbangdulieutieudenoidung" VerticalAlign="Top" />
            <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" VerticalAlign="Top" />
            <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />                                        
            <Columns>                        
                <asp:TemplateField >
                    <HeaderTemplate>                                
                    #
                    </HeaderTemplate>
                    <ItemTemplate>
                    <asp:Label ID="lbNo" runat="server" Text='<%# Container.DataItemIndex + (CustomPaging.PageIndex - 1) * m_grid.PageSize + 1%>'>                                    
                    </asp:Label>
                    </ItemTemplate>      
                    <ItemStyle HorizontalAlign="Center" Width="3%" />  
                    <HeaderStyle Width="3%" />          
                </asp:TemplateField>        
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblName" runat="server" Text="Tên/URL" ></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <b><%# Eval("SeoName") %> </b><br />
                        <%# Eval("Url") %> 
                    </ItemTemplate>
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblSeoInfo" runat="server" Text="Thông tin SEO" ></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <span style="color:#aaa">Title: </span><%# Eval("MetaTitle") %><br />
                        <span style="color:#aaa">Desc: </span><%# Eval("MetaDesc") %><br />   
                        <span style="color:#aaa">Keyword: </span><%# Eval("MetaKeyword") %><br />
                        <span style="color:#aaa">Canonical: </span><%# Eval("CanonicalTag") %><br />
                         <span style="color:#aaa">H1: </span><%# Eval("H1Tag") %><br />
                        <span style="color:#aaa">SeoFooter: </span><%# Eval("SeoFooter") %>  
                    </ItemTemplate>
                    <HeaderStyle />          
                </asp:TemplateField>  
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblCrUserId" runat="server" Text="Người tạo" ></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                    <span class="ngaythang">
                            <%# ICSoft.HelperLib.UserHelpers.Static_Get(int.Parse(Eval("CrUserId").ToString()), l_User, "").Username %>
                        </span>
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign = "Center" Wrap="false" />  
                    <HeaderStyle Width="10%" />        
                </asp:TemplateField> 
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblCrDateTime" runat="server" Text="Ngày tạo" ></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                    <span class="ngaythang">
                    <%# String.Format("{0:dd/MM/yyyy HH:mm}", Eval("CrDateTime"))%>
                        </span>
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign = "Center" Wrap="false" />  
                    <HeaderStyle Width="10%" />        
                </asp:TemplateField>                                      
                <asp:TemplateField>
                    <HeaderTemplate>                                
                        <asp:Label ID="lblThaotac" runat="server" Text="Thao tác" ></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <a id="popup" href='Seos_Edit.aspx?SeoId=<%# Eval("SeoId") %>' class="iconadmsua"
                        title="Edit" ></a>    
                        <asp:LinkButton ID="lbDelete" runat="server" CssClass="iconadmxoa" CommandName="Delete" ToolTip="Xóa" ></asp:LinkButton> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="center" Wrap="false" />
                    <HeaderStyle Width="90px" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </div>
        <div class="clear5px"></div>
        <uc1:CustomPaging ID="CustomPaging" runat="server" />
</asp:Content>
