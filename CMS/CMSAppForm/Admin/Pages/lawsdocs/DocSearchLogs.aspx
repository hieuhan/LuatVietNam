<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="DocSearchLogs.aspx.cs" Inherits="Admin_DocSearchLogs" %>
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

            $('a.popup').live('click', function (e) {
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
    </script>
    <asp:UpdatePanel ID="upn_Grid" runat="server">
    <ContentTemplate>
    <div style="text-align:center"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
    <table cellpadding="4" cellspacing="0" style="width:100%">
        <tr>
            <td style="width:65px; white-space:nowrap;" >
                <asp:Label ID="lblLanguage" runat="server" Text="Ngôn ngữ:" meta:resourcekey="lblLanguage"></asp:Label>
            </td>
            <td style="width:255px; white-space:nowrap;" >
                <asp:DropDownList ID="ddlLanguage" runat="server" DataTextField="LanguageDesc" 
                    DataValueField="LanguageId" Width="250px" CssClass="userselect" 
                    AutoPostBack="True" onselectedindexchanged="ddlLanguage_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="width:110px; white-space:nowrap;">
                <asp:Label ID="lblDocTypes" runat="server" meta:resourcekey="lblDocTypes" 
                    Text="Loại văn bản:"></asp:Label>
            </td>
            <td >
                <asp:DropDownList ID="ddlDocTypes" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="DocTypeDesc" DataValueField="DocTypeId" 
                    onselectedindexchanged="ddlDocTypes_SelectedIndexChanged" Width="250px">
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
                <asp:TextBox ID="txtSearchKeyword" runat="server" CssClass="tukhoatimekiem input-filter"></asp:TextBox>
                &nbsp;&nbsp;
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
        <%=GetLocalResourceObject("DocSearchLogs").ToString() %>
        | <a href="DocSearchLogsTopKeyword.aspx?DateFrom=<%=txtDateFrom.Text %>&DateTo=<%=txtDateTo.Text %>&Language=<%=ddlLanguage.SelectedValue %>" class="popup" title="Top từ khóa">Xem top từ khóa</a>
	</div>
	<div class="chucnangright">
	</div>
    <div style="text-align:left; width:50%; float:right"><asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                            <ProgressTemplate><img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...</ProgressTemplate>
                            </asp:UpdateProgress></div>
	<div class="clear5px"></div>
    <div class="contenbangdulieu">
    
        <asp:GridView ID="m_grid" DataKeyNames="DocSearchLogId" runat="server" ShowHeaderWhenEmpty="True"
            AutoGenerateColumns="False" CssClass="jquery-filter-table" PageSize="50"
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
                    <asp:Label ID="lbNo" runat="server" Text='<%# Container.DataItemIndex + (CustomPaging.PageIndex - 1) * m_grid.PageSize + 1%>' 
                                     ToolTip ='<%# Eval("DocSearchLogId").ToString() %>'> </asp:Label>
                    </ItemTemplate>      
                    <ItemStyle HorizontalAlign="Center" Width="3%" />  
                    <HeaderStyle Width="3%" />          
                </asp:TemplateField>        
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnSearchKeyword" runat="server" Text="Từ khóa tìm kiếm" meta:resourcekey="lblGridColumnSearchKeyword"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lbSearchKeyword" runat="server" style="display:none"></asp:Label>                        
                        <%# Eval("SearchKeyword")%> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left"/>
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnDateFrom" runat="server" Text="Từ ngày" meta:resourcekey="lblGridColumnDateFrom"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <%# DateTimeHelpers.GetDateAndTime(Eval("DateFrom"))%>
                     </ItemTemplate>
                    <ItemStyle Width="8%" HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="8%" />        
                </asp:TemplateField> 
                 <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnDateTo" runat="server" Text="Đến ngày" meta:resourcekey="lblGridColumnDateTo"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <%# DateTimeHelpers.GetDateAndTime(Eval("DateTo"))%>
                    </ItemTemplate>
                    <ItemStyle Width="8%" HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="8%" />        
                </asp:TemplateField>
                 <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnDocTypeId" runat="server" Text="Loại văn bản" meta:resourcekey="lblGridColumnDocTypeId"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <%# LanguageHelpers.IsCultureVietnamese() ? DocTypes.Static_Get(byte.Parse(Eval("DocTypeId").ToString()), l_DocTypes).DocTypeDesc : DocTypes.Static_Get(byte.Parse(Eval("DocTypeId").ToString()), l_DocTypes).DocTypeName%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign = "Left"/>  
                    <HeaderStyle/>        
                </asp:TemplateField> 
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnFieldId" runat="server" Text="Lĩnh vực" meta:resourcekey="lblGridColumnFieldId"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <%# LanguageHelpers.IsCultureVietnamese() ? Fields.Static_Get(short.Parse(Eval("FieldId").ToString()), l_Fields).FieldDesc : Fields.Static_Get(short.Parse(Eval("FieldId").ToString()), l_Fields).FieldName%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign = "Left"/>  
                    <HeaderStyle/>        
                </asp:TemplateField> 
                 <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnOrganId" runat="server" Text="Cơ quan ban hành" meta:resourcekey="lblGridColumnOrganId"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <%# Eval("OrganId").ToString() != "0" ? ICSoft.LawDocsLib.Organs.Static_Get(short.Parse(Eval("OrganId").ToString()), 1).OrganDesc : "" %>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign = "Left"/>  
                    <HeaderStyle/>        
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnCustomerId" runat="server" Text="Khách hàng" meta:resourcekey="lblGridColumnCustomerId"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lbCustomerId" runat="server" style="display:none"></asp:Label>                        
                        <%# Eval("CustomerId")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" Width="8%"/>
                    <HeaderStyle Width="8%" />               
                </asp:TemplateField>  
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnTime" runat="server" Text="Thời gian" meta:resourcekey="lblGridColumnTime"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <%# DateTimeHelpers.GetDateAndTime(Eval("CrDateTime")) %>
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
   </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
