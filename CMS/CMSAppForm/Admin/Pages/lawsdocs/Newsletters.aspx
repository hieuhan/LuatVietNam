<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="Newsletters.aspx.cs" Inherits="Admin_Newsletters" %>
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
                    height: 600,
                    width: 800,
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
    <%--<asp:UpdatePanel ID="upn_Grid" runat="server">
    <ContentTemplate>--%>
    <div style="text-align:center;"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
    <table cellpadding="2" cellspacing="0" style="width:100%">
        <tr>
            <td style="width:80px; white-space:nowrap;">Site:
            </td>
            <td style="width:260px"><asp:DropDownList ID="ddlSite" runat="server" DataTextField="SiteDesc" 
                            DataValueField="SiteId" Width="250px" CssClass="userselect"
                            AutoPostBack="True" onselectedindexchanged="ddlSite_SelectedIndexChanged">
                        </asp:DropDownList>
            </td>
            <td style="width:90px; white-space:nowrap;"></td>
            <td></td>
        </tr>
        <tr>
            <td style="width:80px; white-space:nowrap;">
                <asp:Label ID="lblNewsletterGroups" runat="server" meta:resourcekey="lblNewsletterGroups" 
                    Text="Nhóm bản tin:"></asp:Label>
                </td>
            <td style="width:260px">
                <asp:DropDownList ID="ddlNewsletterGroups" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="NewsletterGroupDesc" 
                    DataValueField="NewsletterGroupId" 
                    onselectedindexchanged="ddlNewsletterGroups_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
            <td style="width:90px; white-space:nowrap;">
                <asp:Label ID="lblNewsletterStatus" runat="server" 
                    meta:resourcekey="lblNewsletterStatus" Text="Trạng thái:"></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlNewsletterStatus" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="NewsletterStatusDesc" 
                    DataValueField="NewsletterStatusId" 
                    onselectedindexchanged="ddlNewsletterStatus_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        
        <tr>
            <td style="width: 80px; white-space: nowrap;">
                <asp:Label ID="lblDateFrom" runat="server" meta:resourcekey="lblDateFrom" 
                    Text="Từ ngày:"></asp:Label>
            </td>
            <td style="width: 260px">
                <asp:TextBox ID="txtDateFrom"  CssClass="tukhoatimekiem" runat="server" Width="98px"></asp:TextBox>
                <asp:Label ID="lblDateTo" runat="server" meta:resourcekey="lblDateTo" 
                    Text="đến"></asp:Label>
                <asp:TextBox ID="txtDateTo" runat="server" CssClass="tukhoatimekiem" 
                    Width="98px"></asp:TextBox>
                </td>
            <td style="width: 90px; white-space: nowrap;">
                <asp:Label ID="lblTitle" runat="server" meta:resourcekey="lblTitle" 
                    Text="Tiêu đề:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="tukhoatimekiem" Width="240px"></asp:TextBox>
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
                <asp:Label ID="lblSendFrom" runat="server" meta:resourcekey="lblSendFrom" 
                    Text="Gửi từ:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtSendFrom" runat="server" CssClass="tukhoatimekiem" Width="240px"></asp:TextBox>
                &nbsp;
                <asp:Button ID="btnSearch" runat="server" CssClass="timkiembutom" 
                     meta:resourcekey="btnSearch" onclick="btnSearch_Click" Text="Tìm kiếm">
                    </asp:Button>
                <asp:LinkButton ID="btnRefresh" runat="server" CssClass="timkiembutom hidden-btn-dqs" Text="Refresh" onclick="btnRefresh_Click"></asp:LinkButton>	
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
        <%=GetLocalResourceObject("Newsletters").ToString()%>
	</div>
	<div class="chucnangright">
		<a id="popup" href="NewslettersEdit.aspx" title="<%=GetLocalResourceObject("lnkAddNew.title").ToString() %>" class="themmoi" > 
            <asp:Label ID="lblAddNew" runat="server" Text="Thêm mới" meta:resourcekey="lblAddNew"></asp:Label>
        </a>
        <asp:LinkButton ID="lbDelete" runat="server" CssClass="xoatin" Text="Xóa" 
            meta:resourcekey="lbDelete" onclick="lbDelete_Click">
        </asp:LinkButton>
	</div>
    <%--<div style="text-align:left; width:30%; float:right"><asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                            <ProgressTemplate><img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...</ProgressTemplate>
                            </asp:UpdateProgress></div>--%>
	<div class="clear5px"></div>
    <div class="contenbangdulieu">
    
        <asp:GridView ID="m_grid" DataKeyNames="NewsletterId" runat="server" ShowHeaderWhenEmpty="True"
            AutoGenerateColumns="False" 
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
                                     ToolTip ='<%# Eval("NewsletterId").ToString() %>'> </asp:Label>
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
                    <ItemStyle HorizontalAlign="Left"/>
                    <HeaderStyle/>          
                </asp:TemplateField>        
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnSendFrom" runat="server" Text="Gửi từ" meta:resourcekey="lblGridColumnSendFrom"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("SendFrom")%> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left"/>
                    <HeaderStyle/>          
                </asp:TemplateField> 
               <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnNewsletterGroup" runat="server" Text="Nhóm bản tin" meta:resourcekey="lblGridColumnNewsletterGroup"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <%# LanguageHelpers.IsCultureVietnamese() ? NewsletterGroups.Static_Get(short.Parse(Eval("NewsletterGroupId").ToString()), l_NewsletterGroups).NewsletterGroupDesc : NewsletterGroups.Static_Get(short.Parse(Eval("NewsletterGroupId").ToString()), l_NewsletterGroups).NewsletterGroupName%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign = "Left"/>  
                    <HeaderStyle/>        
                </asp:TemplateField> 
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnNewsletterStatusId" runat="server" Text="Trạng thái" meta:resourcekey="lblGridColumnNewsletterStatusId"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <span style="font-weight:bold;"  >
                            <%# LanguageHelpers.IsCultureVietnamese() ? NewsletterStatus.Static_Get(byte.Parse(Eval("NewsletterStatusId").ToString()), l_NewsletterStatus).NewsletterStatusDesc : NewsletterStatus.Static_Get(byte.Parse(Eval("NewsletterStatusId").ToString()), l_NewsletterStatus).NewsletterStatusName%>
                        </span> 
                    </ItemTemplate>
                    <ItemStyle Width="8%" HorizontalAlign = "Left" Wrap="false" />  
                    <HeaderStyle Width="8%" />        
                </asp:TemplateField>                
                  <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnTime" runat="server" Text="Thời gian" meta:resourcekey="lblGridColumnTime"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <span class="ngaythang">
                           <asp:Label ID="lblCrDateTime" runat="server" Text="+Tạo: " meta:resourcekey="lblCrDateTime"></asp:Label> <%# DateTimeHelpers.GetDateAndTime(Eval("CrDateTime"))%><br />
                            <asp:Label ID="lblScheduleSendTime" runat="server" Text="+Lịch gửi: " meta:resourcekey="lblScheduleSendTime"></asp:Label> <%# DateTimeHelpers.GetDateAndTime(Eval("ScheduleSendTime"))%><br />
                             <asp:Label ID="lblSendTime" runat="server" Text="+Gửi: " meta:resourcekey="lblSendTime"></asp:Label> <%# DateTimeHelpers.GetDateAndTime(Eval("SendTime"))%>
                        </span> 
                    </ItemTemplate>
                    <ItemStyle Width="8%" HorizontalAlign = "Left" Wrap="false" />  
                    <HeaderStyle Width="8%" />        
                </asp:TemplateField>
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnUser" runat="server" Text="Tạo bởi" meta:resourcekey="lblGridColumnUser"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                       <%# UserHelpers.Static_Get(int.Parse(Eval("CrUserId").ToString()), l_Users, "").Username %>
                    </ItemTemplate>
                    <ItemStyle Width="8%" HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="8%" />        
                </asp:TemplateField>                       
                <asp:TemplateField>
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnOperation" runat="server" Text="Thao tác" meta:resourcekey="lblGridColumnOperation"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <a id="popup" onmouseover="ShowMenu('-1-<%# Eval("NewsletterId") %>')" href='NewslettersEdit.aspx?NewsletterId=<%# Eval("NewsletterId") %>' class="iconadmsua"
                        title="<%# GetLocalResourceObject("lnkGridColumnEdit.title").ToString() %>" meta:resourcekey="lnkGridColumnEdit"></a>    
                        <asp:LinkButton ID="lbDelete" runat="server" CssClass="iconadmxoa" CommandName="Delete" ToolTip="Xóa" meta:resourcekey="lnkGridColumnDel"></asp:LinkButton> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="center"  Wrap="false" />
                    <HeaderStyle Width="6%" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemStyle HorizontalAlign="Center" Width="3%" />
                    <HeaderStyle Width="3%" />
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
   <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
