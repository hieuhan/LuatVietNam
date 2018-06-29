<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="FaqViewCount.aspx.cs" Inherits="Admin_FaqViewCount" %>
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
            <td style="width:60px; white-space:nowrap;">
                <asp:Label ID="lblLanguage" runat="server" meta:resourcekey="lblLanguage" 
                    Text="Ngôn ngữ:"></asp:Label>
                </td>
            <td style="width:260px">
                <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="LanguageDesc" DataValueField="LanguageId" 
                    onselectedindexchanged="ddlLanguage_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
            <td style="width:60px; white-space:nowrap;">
                <asp:Label ID="lblFaqGroups" runat="server" 
                    meta:resourcekey="lblFaqGroups" Text="Nhóm:"></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlFaqGroups" runat="server" AutoPostBack="True"  
                    CssClass="userselect" DataTextField="FaqGroupDesc" 
                    DataValueField="FaqGroupId" 
                    onselectedindexchanged="ddlFaqGroups_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        
        <tr>
            <td>
                <asp:Label ID="lblDateFrom" runat="server" meta:resourcekey="lblDateFrom" 
                    Text="Từ ngày:"></asp:Label>
            </td>
            <td style="width: 260px">
                <asp:TextBox ID="txtDateFrom" runat="server" CssClass="tukhoatimekiem" 
                    Width="98px"></asp:TextBox>
                <asp:Label ID="lblDateTo" runat="server" meta:resourcekey="lblDateTo" 
                    Text="đến"></asp:Label>
                <asp:TextBox ID="txtDateTo" runat="server" CssClass="tukhoatimekiem" 
                    Width="98px"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblFaqTypes" runat="server" meta:resourcekey="lblFaqTypes" 
                    Text="Loại:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlFaqTypes" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="FaqTypeDesc" 
                    DataValueField="FaqTypeId" 
                    onselectedindexchanged="ddlFaqTypes_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        
        <tr>
            <td>
                <asp:Label ID="lblReviewStatus" runat="server" meta:resourcekey="lblReviewStatus" 
                    Text="Trạng thái:"></asp:Label>
            </td>
            <td style="width: 260px">
                
                <asp:DropDownList ID="ddlReviewStatus" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="ReviewStatusDesc" 
                    DataValueField="ReviewStatusId" 
                    onselectedindexchanged="ddlReviewStatus_SelectedIndexChanged" Width="250px">
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
                <asp:Label ID="lblDataSources" runat="server" meta:resourcekey="lblDataSources" 
                    Text="Nguồn:"></asp:Label>
            </td>
            <td style="width: 260px">
                <asp:DropDownList ID="ddlDataSources" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="DataSourceDesc" 
                    DataValueField="DataSourceId" 
                    onselectedindexchanged="ddlDataSources_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lblFaqCode" runat="server" meta:resourcekey="lblFaqCode" 
                    Text="Mã:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFaqCode" runat="server" CssClass="tukhoatimekiem" 
                    Width="240px"></asp:TextBox>
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
                <asp:Label ID="lblQuestion" runat="server" meta:resourcekey="lblQuestion" 
                    Text="Từ khóa:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtQuestion" runat="server" CssClass="tukhoatimekiem" Width="240px"></asp:TextBox>
                &nbsp;<asp:Button ID="btnSearch" runat="server" CssClass="timkiembutom" 
                    meta:resourcekey="btnSearch" onclick="btnSearch_Click" Text="Tìm kiếm"> </asp:Button>
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
        <%=GetLocalResourceObject("FaqViewCount").ToString() %>
	</div>
	<div class="chucnangright">
	</div>
    <div style="text-align:left; width:50%; float:right"><asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                            <ProgressTemplate><img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...</ProgressTemplate>
                            </asp:UpdateProgress></div>
	<div class="clear5px"></div>
    <div class="contenbangdulieu">
    
        <asp:GridView ID="m_grid" DataKeyNames="FaqId" runat="server" ShowHeaderWhenEmpty="True" PageSize="50"
            AutoGenerateColumns="False" 
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
                                     ToolTip ='<%# Eval("FaqId").ToString() %>'> </asp:Label>
                    </ItemTemplate>      
                    <ItemStyle HorizontalAlign="Center" Width="3%" />  
                    <HeaderStyle Width="3%" />          
                </asp:TemplateField>        
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnQuestion" runat="server" Text="Câu hỏi" meta:resourcekey="lblGridColumnQuestion"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lbQuestion" runat="server" style="display:none"></asp:Label>                        
                        <%# Eval("Question")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left"/>
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnFaqCode" runat="server" Text="Mã" meta:resourcekey="lblGridColumnFaqCode"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lbFaqCode" runat="server"></asp:Label>                        
                        <%# Eval("FaqCode")%> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" Width="3%"/>
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
                    <ItemStyle  HorizontalAlign="Right" Width="8%" />                     
                    <HeaderStyle Width="8%" />          
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
                        <asp:Label ID="lblGridColumnReviewStatusId" runat="server" Text="Trạng thái" meta:resourcekey="lblGridColumnReviewStatusId"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <span class="xuatban<%# Eval("ReviewStatusId") %>" >
                            <%# LanguageHelpers.IsCultureVietnamese() ? ReviewStatus.Static_Get(byte.Parse(Eval("ReviewStatusId").ToString()), l_ReviewStatus).ReviewStatusDesc : ReviewStatus.Static_Get(byte.Parse(Eval("ReviewStatusId").ToString()), l_ReviewStatus).ReviewStatusName%>
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
                        <%# DateTimeHelpers.GetDateAndTime(Eval("CrDateTime")) %>
                    </ItemTemplate>
                    <ItemStyle Width="8%" HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="8%" />        
                </asp:TemplateField>
                <asp:TemplateField Visible="true"  > 
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
   </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
