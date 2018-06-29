<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="NewsletterSends.aspx.cs" Inherits="Admin_NewsletterSends" %>
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
                    height: 540,
                    width: 800,
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
        $("#<%= txtDateFrom.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#<%= txtDateTo.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
    }
    </script>
    <asp:UpdatePanel ID="upn_Grid" runat="server">
    <ContentTemplate>
    <div style="text-align:center;"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
    <table cellpadding="2" cellspacing="0" style="width:100%">
        <tr>
            <td style="width:60px; white-space:nowrap;">Site:
            </td>
            <td style="width:260px"><asp:DropDownList ID="ddlSite" runat="server" DataTextField="SiteDesc" 
                            DataValueField="SiteId" Width="250px" CssClass="userselect"
                            AutoPostBack="True" onselectedindexchanged="ddlSite_SelectedIndexChanged">
                        </asp:DropDownList>
            </td>
            <td style="width:60px; white-space:nowrap;">Bản tin:</td>
            <td><asp:DropDownList ID="ddlMessage" runat="server" DataTextField="Title" 
                            DataValueField="NewsletterId" Width="250px" CssClass="userselect"
                            AutoPostBack="True" onselectedindexchanged="ddlMessage_SelectedIndexChanged">
                        </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width:60px; white-space:nowrap;">
                <asp:Label ID="lblSendStatus" runat="server" meta:resourcekey="lblSendStatus" 
                    Text="Trạng thái:"></asp:Label>
                </td>
            <td style="width:260px">
                <asp:DropDownList ID="ddlSendStatus" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="SendStatusDesc" 
                    DataValueField="SendStatusId" 
                    onselectedindexchanged="ddlSendStatus_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
            <td style="width:60px; white-space:nowrap;">
                <asp:Label ID="lblOrderBy" runat="server" meta:resourcekey="lblOrderBy" 
                    Text="Sắp xếp:"></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlOrderBy" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="OrderByDesc" DataValueField="OrderBy" 
                    onselectedindexchanged="ddlOrderBy_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
                </td>
        </tr>
        
        <tr>
            <td >
                <asp:Label ID="lblDateFrom" runat="server" meta:resourcekey="lblDateFrom" 
                    Text="Từ ngày:"></asp:Label>
            </td>
            <td style="width:260px">
                <asp:TextBox ID="txtDateFrom" runat="server" CssClass="tukhoatimekiem" 
                    Width="100px"></asp:TextBox>
                <asp:Label ID="lblDateTo" runat="server" meta:resourcekey="lblDateTo" 
                    Text="Đến"></asp:Label>
                <asp:TextBox ID="txtDateTo" runat="server" CssClass="tukhoatimekiem" 
                    Width="100px"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblSearchKeyword" runat="server" 
                    meta:resourcekey="lblSearchKeyword" Text="Từ khóa:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtSearchKeyword" runat="server" CssClass="tukhoatimekiem" 
                    Width="240px"></asp:TextBox>
                &nbsp;<asp:LinkButton ID="btnSearch" runat="server" CssClass="timkiembutom" 
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
        <%=GetLocalResourceObject("NewsletterSends").ToString()%>
	</div>
	<div class="chucnangright">
		
	</div>
    <div style="text-align:left; width:50%; float:right"><asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                            <ProgressTemplate><img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...</ProgressTemplate>
                            </asp:UpdateProgress></div>
	<div class="clear5px"></div>
    <div class="contenbangdulieu">    
        <asp:GridView ID="m_grid" DataKeyNames="NewsletterSendId" runat="server" ShowHeaderWhenEmpty="True"
            AutoGenerateColumns="False" Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0">
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
                                     ToolTip ='<%# Eval("NewsletterSendId").ToString() %>'> </asp:Label>
                    </ItemTemplate>      
                    <ItemStyle HorizontalAlign="Center" Width="3%" />  
                    <HeaderStyle Width="3%" />          
                </asp:TemplateField> 
                  <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnTitle" runat="server" Text="Tiêu đề" meta:resourcekey="lblGridColumnTitle"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("Title")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                </asp:TemplateField> 
                 <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnSendFrom" runat="server" Text="Gửi từ" meta:resourcekey="lblGridColumnSendFrom"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("SendFrom")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                </asp:TemplateField> 
                 <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnEmail" runat="server" Text="Email" meta:resourcekey="lblGridColumnEmail"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("Email")%> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                    <HeaderStyle/>          
                </asp:TemplateField>        
               <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnSendStatusId" runat="server" Text="Trạng thái" meta:resourcekey="lblGridColumnSendStatusId"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <span class="xuatban<%# Eval("SendStatusId") %>" >
                            <%# LanguageHelpers.IsCultureVietnamese() ? SendStatus.Static_Get(byte.Parse(Eval("SendStatusId").ToString()), l_SendStatus).SendStatusDesc : SendStatus.Static_Get(byte.Parse(Eval("SendStatusId").ToString()), l_SendStatus).SendStatusName%>
                        </span> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign = "Left" Width="8%" Wrap="false"/>  
                    <HeaderStyle/>        
                </asp:TemplateField> 
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnTime" runat="server" Text="Thời gian gửi" meta:resourcekey="lblGridColumnTime1"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                       <%# DateTimeHelpers.GetDateAndTime(Eval("SendTime"))%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign = "Center" Wrap="false" />  
                    <HeaderStyle Width="10%" />        
                </asp:TemplateField>
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnTime" runat="server" Text="Thời gian tạo" meta:resourcekey="lblGridColumnTime1"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                       <%# DateTimeHelpers.GetDateAndTime(Eval("CrDateTime"))%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign = "Center" Wrap="false" />  
                    <HeaderStyle Width="10%" />        
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
