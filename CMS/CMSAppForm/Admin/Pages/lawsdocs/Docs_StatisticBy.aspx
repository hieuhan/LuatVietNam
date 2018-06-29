<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="Docs_StatisticBy.aspx.cs" Inherits="Admin_Docs_StatisticBy" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/admin/UserControls/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="uc1" %>
<%@ Import Namespace="ICSoft.CMSLib" %>
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

            $('a#popup').live('click', function (e) {
                var page = $(this).attr("href")
                var cdialog = $('<div id="divEdit"></div>')
                .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="98%" height="98%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 420,
                    width: 580,
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
        $(function () {
            $("#<%= txtDateFrom.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        });
        $(function () {
            $("#<%= txtDateTo.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        });
    }
        function submitButton(event) {
            if (event.which == 13) {
                $('#<%= btnSearch.ClientID %>').click();
	}
}
    </script>
    <asp:UpdatePanel ID="upn_Grid" runat="server">
    <ContentTemplate>
    <div style="text-align:center"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
     <table cellpadding="4" cellspacing="0" style="width:100%">
        <tr>
            <td style="width:90px; white-space:nowrap;">
                <asp:Label ID="lblLanguage" runat="server" meta:resourcekey="lblLanguage" 
                    Text="Ngôn ngữ:"></asp:Label>
            </td>
            <td width="260px">
                <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="LanguageDesc" DataValueField="LanguageId" 
                    onselectedindexchanged="ddlLanguage_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
            <td style="width:110px; white-space:nowrap;">
                <asp:Label ID="lblDataSources" runat="server" meta:resourcekey="lblDataSources" 
                    Text="Nguồn:"></asp:Label>
            </td>
            <td >
                <asp:DropDownList ID="ddlDataSources" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="DataSourceDesc" 
                    DataValueField="DataSourceId" 
                    onselectedindexchanged="ddlDataSources_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblDocTypes" runat="server" meta:resourcekey="lblDocTypes" 
                    Text="Loại VB:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlDocTypes" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="DocTypeDesc" DataValueField="DocTypeId" 
                    onselectedindexchanged="ddlDocTypes_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lblFields" runat="server" meta:resourcekey="lblFields" 
                    Text="Lĩnh vực:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlFields" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="FieldDesc" DataValueField="FieldId" 
                    onselectedindexchanged="ddlFields_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblOrgans" runat="server" meta:resourcekey="lblOrgans" 
                    Text="CQBH:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlOrgans" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="OrganDesc" DataValueField="OrganId" 
                    onselectedindexchanged="ddlOrgans_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lblEffectStatus" runat="server" 
                    meta:resourcekey="lblEffectStatus" Text="Trạng thái hiệu lực:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlEffectStatus" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="EffectStatusDesc" 
                    DataValueField="EffectStatusId" 
                    onselectedindexchanged="ddlEffectStatus_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblReviewStatus" runat="server" meta:resourcekey="lblReviewStatus" 
                    Text="Trạng thái duyệt:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlReviewStatus" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="ReviewStatusDesc" 
                    DataValueField="ReviewStatusId" 
                    onselectedindexchanged="ddlReviewStatus_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
                </td>
            <td>
                <asp:Label ID="lblUseStatus" runat="server" meta:resourcekey="lblUseStatus" 
                    Text="Trạng thái sử dụng:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlUseStatus" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="UseStatusDesc" 
                    DataValueField="UseStatusId" 
                    onselectedindexchanged="ddlUseStatus_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblDateFrom" runat="server" meta:resourcekey="lblDateFrom" 
                    Text="Từ ngày:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDateFrom" runat="server" CssClass="tukhoatimekiem" 
                    Width="100px"></asp:TextBox>
                <asp:Label ID="lblDateTo" runat="server" meta:resourcekey="lblDateTo" 
                    Text="Đến"></asp:Label>
                <asp:TextBox ID="txtDateTo" runat="server" CssClass="tukhoatimekiem" 
                    Width="100px"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblFindBydate" runat="server" meta:resourcekey="lblFindBydate" 
                    Text="Thống kê theo:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" 
                    CssClass="userselect" 
                    onselectedindexchanged="ddlSearchBy_SelectedIndexChanged" Width="250px">
                    <asp:ListItem Value="CrDateTime" Selected="True">Ngày tạo</asp:ListItem>
                    <asp:ListItem Value="CrUserId">Người tạo</asp:ListItem>
                    <asp:ListItem Value="DataSourceId">Nguồn</asp:ListItem>                    
                    <asp:ListItem Value="DocTypeId">Loại văn bản</asp:ListItem>
                    <asp:ListItem Value="EffectStatusId">Trạng thái hiệu lực</asp:ListItem>
                    <asp:ListItem Value="FieldId">Lĩnh vực</asp:ListItem>
                    <asp:ListItem Value="IssueYear">Năm ban hành</asp:ListItem>
                    <asp:ListItem Value="OrganId">Cơ quan ban hành</asp:ListItem>
                    <asp:ListItem Value="UseStatusId">Trạng thái sử dụng</asp:ListItem>
                </asp:DropDownList>
                &nbsp;<asp:Button ID="btnSearch" runat="server" CssClass="timkiembutom" 
                    meta:resourcekey="btnSearch" onclick="btnSearch_Click" Text="Tìm kiếm">
                    </asp:Button>
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
        <%=GetLocalResourceObject("Docs_StatisticBy").ToString()%>
	</div>
	<div class="chucnangright">
	</div>
    <div style="text-align:left; width:50%; float:right"><asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                            <ProgressTemplate><img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...</ProgressTemplate>
                            </asp:UpdateProgress></div>
	<div class="clear5px"></div>
    <div class="contenbangdulieu">
        <asp:GridView ID="m_grid" runat="server" ShowHeaderWhenEmpty="True" AllowSorting="true" 
            AutoGenerateColumns="True" Width="100%" OnRowDataBound = "m_grid_OnRowDataBound"  CellPadding="0" CellSpacing="0" BorderWidth="0" >
            <HeaderStyle CssClass="trbangdulieutieude" />
            <RowStyle CssClass="trbangdulieutieudenoidung" />
            <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
            <SelectedRowStyle CssClass="trbangdulieutieudenoidung" /> 
            <%--<Columns>                        
                <asp:TemplateField >
                    <HeaderTemplate>                                
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbNo" runat="server" Text='' > </asp:Label>
                    </ItemTemplate>      
                    <ItemStyle HorizontalAlign="Center" Width="3%" />  
                    <HeaderStyle Width="3%" />          
                </asp:TemplateField>        
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblC" runat="server" Text=""></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lblL" runat="server" Text='' style="font-weight:bold;"></asp:Label>                        
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" VerticalAlign="Top" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblLink" runat="server" Text="Link"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lblLinkto" runat="server" Text=''></asp:Label>                        
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" VerticalAlign="Top" />
                    <HeaderStyle />          
                </asp:TemplateField> 
            </Columns>--%>
        </asp:GridView>        
    </div>
   </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
