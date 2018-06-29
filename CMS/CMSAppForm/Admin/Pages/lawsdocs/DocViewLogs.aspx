<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="DocViewLogs.aspx.cs" Inherits="Admin_DocViewLogs" %>
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
                <asp:Label ID="lblActionTypes" runat="server" meta:resourcekey="lblActionTypes" 
                    Text="Loại hành động:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlActionTypes" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="ActionTypeDesc" DataValueField="ActionTypeId" 
                    onselectedindexchanged="ddlActionTypes_SelectedIndexChanged" Width="250px">
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
                <asp:Label ID="lblFromIP" runat="server" Text="Tìm theo IP:" meta:resourcekey="lblFromIP"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFromIP" runat="server" CssClass="tukhoatimekiem input-filter"></asp:TextBox>
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
        <%=GetLocalResourceObject("DocViewLogs").ToString() %>
	</div>
	<div class="chucnangright">
	</div>
    <div style="text-align:left; width:50%; float:right"><asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                            <ProgressTemplate><img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...</ProgressTemplate>
                            </asp:UpdateProgress></div>
	<div class="clear5px"></div>
    <div class="contenbangdulieu">
    
        <asp:GridView ID="m_grid" DataKeyNames="DocViewLogId" runat="server" ShowHeaderWhenEmpty="True"
            AutoGenerateColumns="False" CssClass="jquery-filter-table" OnRowDataBound = "m_grid_OnRowDataBound" PageSize="50"
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
                                     ToolTip ='<%# Eval("DocViewLogId").ToString() %>'> </asp:Label>
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
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnDocFileId" runat="server" Text="File văn bản" meta:resourcekey="lblGridColumnDocFileId"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                       <%# Eval("DocFileId") %>                    
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" Width="10%" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnActionTypeId" runat="server" Text="Loại hành động" meta:resourcekey="lblGridColumnActionTypeId"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <%# LanguageHelpers.IsCultureVietnamese() ? ActionTypes.Static_Get(byte.Parse(Eval("ActionTypeId").ToString()), l_ActionTypes).ActionTypeDesc : ActionTypes.Static_Get(byte.Parse(Eval("ActionTypeId").ToString()), l_ActionTypes).ActionTypeName%>
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign = "Left" Wrap="false" />  
                    <HeaderStyle Width="10%" />        
                </asp:TemplateField>
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnCustomerId" runat="server" Text="Khách hàng" meta:resourcekey="lblGridColumnCustomerId"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lbCustomerId" runat="server" style="display:none"></asp:Label>                        
                        <%# Eval("CustomerId")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" Width="10%" />
                    <HeaderStyle Width="10%" />               
                </asp:TemplateField>  
                 <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnFromIP" runat="server" Text="Từ IP" meta:resourcekey="lblGridColumnFromIP"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lbFromIP" runat="server" style="display:none"></asp:Label>                        
                        <%# Eval("FromIP")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" Width="8%" />                     
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
