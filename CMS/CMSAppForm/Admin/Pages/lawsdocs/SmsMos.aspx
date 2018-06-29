<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="SmsMos.aspx.cs" Inherits="Admin_SmsMos" %>
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
    <div style="text-align:center;"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
    <table cellpadding="4" cellspacing="0" style="width:100%">
        <tr>
            <td style="width:70px; white-space:nowrap;">
                <asp:Label ID="lblUserId" runat="server" meta:resourcekey="lblUserId" 
                    Text="Số điện thoại:"></asp:Label>
                </td>
            <td style="width:260px">
                <asp:TextBox ID="txtUserId" runat="server" CssClass="tukhoatimekiem" 
                    Width="240px"></asp:TextBox>
            </td>
            <td style="width:100px; white-space:nowrap;">
                <asp:Label ID="lblMessageIn" runat="server" meta:resourcekey="lblMessageIn" 
                    Text="Nội dung tin nhắn:"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtMessageIn" runat="server" CssClass="tukhoatimekiem" 
                    Width="240px"></asp:TextBox>
            </td>
        </tr>
        
        <tr>
            <td style="width: 70px; white-space: nowrap;">
                <asp:Label ID="lblSmsServices" runat="server" meta:resourcekey="lblSmsServices" 
                    Text="Dịch vụ:"></asp:Label>
            </td>
            <td style="width: 260px">
                <asp:DropDownList ID="ddlSmsServices" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="SmsServiceDesc" 
                    DataValueField="SmsServiceId" 
                    onselectedindexchanged="ddlSmsServices_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
            <td style="width: 100px; white-space: nowrap;">
                <asp:Label ID="lblTelcos" runat="server" meta:resourcekey="lblTelcos" 
                    Text="Mạng:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlTelcos" runat="server" AutoPostBack="True" 
                    CssClass="userselect" onselectedindexchanged="ddlTelcos_SelectedIndexChanged" 
                    Width="250px">
                    <asp:ListItem Selected="True" Value="0">...</asp:ListItem>
                    <asp:ListItem value="2">VINA</asp:ListItem>
                    <asp:ListItem value="11">GTEL</asp:ListItem>
                    <asp:ListItem value="6">SFONE</asp:ListItem>
                    <asp:ListItem value="3">VIETTEL</asp:ListItem>
                    <asp:ListItem value="1">MOBI</asp:ListItem>
                    <asp:ListItem value="12">VNM</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        
        <tr>
            <td>
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
            <td>
                <asp:Label ID="lblSmsMostatus" runat="server" meta:resourcekey="lblSmsMostatus" 
                    Text="Trạng thái:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlSmsProcessStatus" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="SmsProcessStatusDesc" 
                    DataValueField="SmsProcessStatusId" 
                    onselectedindexchanged="ddlSmsProcessStatus_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        
        <tr>
            <td>
                <asp:Label ID="lblOrderBy" runat="server" meta:resourcekey="lblOrderBy" 
                    Text="Sắp xếp:"></asp:Label>
            </td>
            <td style="width: 260px">
                <asp:DropDownList ID="ddlOrderBy" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="OrderByDesc" DataValueField="OrderBy" 
                    onselectedindexchanged="ddlOrderBy_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lblPrefixs" runat="server" meta:resourcekey="lblPrefixs" 
                    Text="Đầu số:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlPrefixs" runat="server" AutoPostBack="True" 
                    CssClass="userselect" Width="250px" 
                    onselectedindexchanged="ddlPrefixs_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Value="">...</asp:ListItem>
                    <asp:ListItem value="6089">6089</asp:ListItem>
                    <asp:ListItem value="6589">6589</asp:ListItem>
                    <asp:ListItem value="6689">6689</asp:ListItem>
                    <asp:ListItem value="6789">6789</asp:ListItem>
                    <asp:ListItem value="998">998</asp:ListItem>
                    <asp:ListItem value="8599">8599</asp:ListItem>
                    <asp:ListItem value="8699">8699</asp:ListItem>
                    <asp:ListItem value="8799">8799</asp:ListItem>
                </asp:DropDownList>
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
        <%=GetLocalResourceObject("SmsMos").ToString()%>
	</div>
	<div class="chucnangright">
	</div>
    <div style="text-align:left; width:50%; float:right"><asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                            <ProgressTemplate><img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...</ProgressTemplate>
                            </asp:UpdateProgress></div>
	<div class="clear5px"></div>
    <div class="contenbangdulieu">
    
        <asp:GridView ID="m_grid" DataKeyNames="MoId" runat="server" ShowHeaderWhenEmpty="True"
            AutoGenerateColumns="False" OnRowDataBound = "m_grid_OnRowDataBound"
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
                                     ToolTip ='<%# Eval("MoId").ToString() %>'> </asp:Label>
                    </ItemTemplate>      
                    <ItemStyle HorizontalAlign="Center" Width="3%" />  
                    <HeaderStyle Width="3%" />          
                </asp:TemplateField> 
                 <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnMessageIn" runat="server" Text="Tin nhắn đến" meta:resourcekey="lblGridColumnMessageIn"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("MessageIn")%> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                    <HeaderStyle/>          
                </asp:TemplateField>        
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnUserId" runat="server" Text="Số điện thoại" meta:resourcekey="lblGridColumnUserId"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("UserId")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle Width="7%" />          
                </asp:TemplateField> 
               <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnPrefixId" runat="server" Text="Đầu số" meta:resourcekey="lblGridColumnPrefixId"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("PrefixId")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" Width="5%" />
                    <HeaderStyle Width="5%" />          
                </asp:TemplateField> 
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnRequestTime" runat="server" Text="Thời gian nhận" meta:resourcekey="lblGridColumnRequestTime"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <%# DateTimeHelpers.GetDateAndTime(Eval("RequestTime"))%>
                    </ItemTemplate>
                    <ItemStyle Width="8%" HorizontalAlign = "Left" Wrap="false" />  
                    <HeaderStyle Width="8%" />        
                </asp:TemplateField>
               <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnSmsServices" runat="server" Text="Dịch vụ" meta:resourcekey="lblGridColumnSmsServices"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <%# LanguageHelpers.IsCultureVietnamese() ? SmsServices.Static_Get(byte.Parse(Eval("SmsServiceId").ToString()), l_SmsServices).SmsServiceDesc : SmsServices.Static_Get(byte.Parse(Eval("SmsServiceId").ToString()), l_SmsServices).SmsServiceName%>
                    </ItemTemplate>
                    <ItemStyle Width="8%" HorizontalAlign = "Left" Wrap="false" />  
                    <HeaderStyle Width="8%" />        
                </asp:TemplateField> 
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnSmsProcessStatus" runat="server" Text="Trạng thái" meta:resourcekey="lblGridColumnSmsProcessStatus"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <%# LanguageHelpers.IsCultureVietnamese() ? SmsProcessStatus.Static_Get(byte.Parse(Eval("SmsProcessStatusId").ToString()), l_SmsProcessStatus).SmsProcessStatusDesc : SmsProcessStatus.Static_Get(byte.Parse(Eval("SmsProcessStatusId").ToString()), l_SmsProcessStatus).SmsProcessStatusName%>                        
                    </ItemTemplate>
                    <ItemStyle Width="8%" HorizontalAlign = "Left" Wrap="false" />  
                    <HeaderStyle Width="8%" />        
                </asp:TemplateField>
                  <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnTime" runat="server" Text="Thời gian" meta:resourcekey="lblGridColumnTime"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <%# DateTimeHelpers.GetDateAndTime(Eval("CrDateTime"))%>
                    </ItemTemplate>
                    <ItemStyle Width="8%" HorizontalAlign = "Left" Wrap="false" />  
                    <HeaderStyle Width="8%" />        
                </asp:TemplateField>
                <asp:TemplateField Visible="false">
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnOperation" runat="server" Text="Thao tác" meta:resourcekey="lblGridColumnOperation"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <a id="popup" onmouseover="ShowMenu('-1-<%# Eval("MoId") %>')" href='SmsMosEdit.aspx?MoId=<%# Eval("MoId") %>' class="iconadmsua"
                        title="<%# GetLocalResourceObject("lnkGridColumnEdit.title").ToString() %>" meta:resourcekey="lnkGridColumnEdit"></a>    
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="center"  Wrap="false" />
                    <HeaderStyle Width="6%" />
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
