<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="DocViewCount.aspx.cs" Inherits="Admin_DocViewCount" %>
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
        $('input[type="hidden"][id*="hdfOldPage"]').val(1);
        var x = $('.khungcontenpading').offset().top - 100;
        jQuery('html,body').animate({ scrollTop: x }, 100);
    }
        function submitButton(event) {
            if (event.which == 13) {
                $('#<%= btnSearch.ClientID %>').click();
            }
        }
    </script>
    <style>.margin-right10{padding: 0 10px 0 0 !important;}</style>
    <%--<asp:UpdatePanel ID="upn_Grid" runat="server">
    <ContentTemplate>--%>
    <div style="text-align:center"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
    <table cellpadding="4" cellspacing="0" style="width:100%">
        <tr>
            <td style="width:120px; white-space:nowrap;" >
                <asp:Label ID="lblLanguage" runat="server" Text="Ngôn ngữ:" meta:resourcekey="lblLanguage"></asp:Label>
            </td>
            <td style="width:255px; white-space:nowrap;" >
                <asp:DropDownList ID="ddlLanguage" runat="server" DataTextField="LanguageDesc" 
                    DataValueField="LanguageId" Width="250px" CssClass="userselect" 
                    AutoPostBack="True" onselectedindexchanged="ddlLanguage_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="width:110px; white-space:nowrap;">
                <asp:Label ID="lblReviewStatus" runat="server" 
                    meta:resourcekey="lblReviewStatus" Text="Trạng thái:"></asp:Label>
            </td>
            <td >
                <asp:DropDownList ID="ddlReviewStatus" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="ReviewStatusDesc" 
                    DataValueField="ReviewStatusId" 
                    onselectedindexchanged="ddlReviewStatus_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblDateFrom" runat="server" meta:resourcekey="lblDateFrom" 
                    Text="Từ ngày:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDateFrom" CssClass="tukhoatimekiem" runat="server" Width="100px"></asp:TextBox>
                <asp:Label ID="lblDateTo" runat="server" meta:resourcekey="lblDateTo" 
                    Text="đến"></asp:Label>
                <asp:TextBox ID="txtDateTo" CssClass="tukhoatimekiem" runat="server" Width="100px"></asp:TextBox>
                </td>
            <td>
                <asp:Label ID="lblFindBydate" runat="server" 
                    meta:resourcekey="lblFindBydate" Text="Tìm theo:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlSearchByDate" runat="server" AutoPostBack="True" 
                    CssClass="userselect" onselectedindexchanged="ddlSearchByDate_SelectedIndexChanged" Width="250px">
                    <asp:ListItem Value="CrDateTime" Selected="True">Ngày tạo</asp:ListItem>
                    <asp:ListItem Value="IssueDate">Ngày ban hành</asp:ListItem>
                    <asp:ListItem Value="EffectDate">Ngày có hiệu lực</asp:ListItem>
                    <asp:ListItem Value="ExpireDate">Ngày hết hiệu lực</asp:ListItem>
                    <asp:ListItem Value="GazetteDate">Ngày công báo</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblDocIdentity" runat="server" meta:resourcekey="lblDocIdentity" 
                    Text="Số hiệu:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDocIdentity" runat="server" CssClass="tukhoatimekiem" 
                    Width="240px"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblDocTypes" runat="server" 
                    meta:resourcekey="lblDocTypes" Text="Loại văn bản:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlDocTypes" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="DocTypeDesc" 
                    DataValueField="DocTypeId" 
                    onselectedindexchanged="ddlDocTypes_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
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
            <td>
                <asp:Label ID="lblOrgans" runat="server" meta:resourcekey="lblOrgans" 
                    Text="Cơ quan ban hành:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlOrgans" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="OrganDesc" DataValueField="OrganId" 
                    onselectedindexchanged="ddlOrgans_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblSigners" runat="server" meta:resourcekey="lblSigners" 
                    Text="Người ký:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlSigners" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="SignerDesc" DataValueField="SignerId" 
                    onselectedindexchanged="ddlSigners_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lblDataSources" runat="server" meta:resourcekey="lblDataSources" 
                    Text="Nguồn văn bản:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlDataSources" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="DataSourceDesc" DataValueField="DataSourceId" 
                    onselectedindexchanged="ddlDataSources_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
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
            <td>
                <asp:Label ID="lblUseStatus" runat="server" meta:resourcekey="lblUseStatus" 
                    Text="Trạng thái sử dụng:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlUseStatus" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="UseStatusDesc" DataValueField="UseStatusId" 
                    onselectedindexchanged="ddlUseStatus_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
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
            <td>
                <asp:Label ID="lblSearchKeyword" runat="server" Text="Tìm kiếm:" meta:resourcekey="lblSearchKeyword"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtSearchKeyword" runat="server" CssClass="tukhoatimekiem"></asp:TextBox> <%--input-filter--%>
                &nbsp;&nbsp;
               <asp:Button ID="btnSearch" runat="server" CssClass="timkiembutom" 
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
        <%=GetLocalResourceObject("DocViewCount").ToString() %>
	</div>
	<div class="chucnangright">
	</div>
    <div style="text-align:left; width:50%; float:right"><%--<asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                            <ProgressTemplate><img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...</ProgressTemplate>
                            </asp:UpdateProgress>--%></div>
	<div class="clear5px"></div>
    <div class="contenbangdulieu">
    
        <asp:GridView ID="m_grid" DataKeyNames="DocViewCountId" runat="server" ShowHeaderWhenEmpty="True" PageSize="50"
            AutoGenerateColumns="False" OnRowDataBound = "m_grid_OnRowDataBound"
            Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" > <%--CssClass="jquery-filter-table"--%>
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
                                     ToolTip ='<%# Eval("DocViewCountId").ToString() %>'> </asp:Label>
                    </ItemTemplate>      
                    <ItemStyle HorizontalAlign="Center" Width="3%" />  
                    <HeaderStyle Width="3%" />          
                </asp:TemplateField>        
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnDocName" runat="server" Text="Tên văn bản" meta:resourcekey="lblGridColumnDocName"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lbDocName" runat="server" style="display:none"></asp:Label>                        
                        <%# Eval("DocName")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left"/>
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnDocIdentity" runat="server" Text="Số hiệu" meta:resourcekey="lblGridColumnDocIdentity"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lbDocIdentity" runat="server" style="display:none"></asp:Label>                        
                        <%# Eval("DocIdentity")%> 
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign="Left"/>
                    <HeaderStyle/>               
                </asp:TemplateField>  
                 <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnViewCount" runat="server" Text="Số lượt xem" meta:resourcekey="lblGridColumnViewCount"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lbViewCount" runat="server" style="display:none"></asp:Label>                        
                        <%# string.Format("{0:#,#}",Eval("ViewCount"))%> 
                    </ItemTemplate>
                    <ItemStyle CssClass="margin-right10" HorizontalAlign="Right" Width="7%" />                     
                    <HeaderStyle Width="7%" />          
                </asp:TemplateField>
                 <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnLastViewTime" runat="server" Text="Lượt xem cuối" meta:resourcekey="lblGridColumnLastViewTime"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <%# DateTimeHelpers.GetDateAndTime(Eval("LastViewTime"))%>
                    </ItemTemplate>
                    <ItemStyle Width="8%" HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="8%" />        
                </asp:TemplateField>                        
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnTime" runat="server" Text="Thời gian cập nhật" meta:resourcekey="lblGridColumnTime"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <%# DateTimeHelpers.GetDateAndTime(Eval("CrDateTime")) %>
                    </ItemTemplate>
                    <ItemStyle Width="8%" HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="8%" />        
                </asp:TemplateField>
                <asp:TemplateField Visible="false"  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnUser" runat="server" Text="Tạo bởi" meta:resourcekey="lblGridColumnUser"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <%# UserHelpers.Static_Get(int.Parse(Eval("CrUserId").ToString()), l_Users, "").Username %>
                    </ItemTemplate>
                    <ItemStyle Width="8%" HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="8%" />        
                </asp:TemplateField>                  
            </Columns>
        </asp:GridView>
    </div>
   </div>
   <div class="clear5px"></div>    
                <uc1:CustomPaging ID="CustomPaging" runat="server" />                
                <div class="clear5px"></div>   
   <%--</div>
    </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
