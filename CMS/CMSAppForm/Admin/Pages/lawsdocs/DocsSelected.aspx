<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="DocsSelected.aspx.cs" Inherits="Admin_DocsSelected" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/admin/UserControls/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="uc1" %>
<%@ Import Namespace="ICSoft.CMSLib" %>
<%@ Import Namespace="ICSoft.LawDocsLib" %>
<%@ Import Namespace="ICSoft.HelperLib" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
<script src="../../Js/ajaxTooltip/ajax-tooltip.js" type="text/javascript"></script>
<script src="../../Js/ajaxTooltip/ajax-dynamic-content.js" type="text/javascript"></script>
<script src="../../Js/ajaxTooltip/ajax.js" type="text/javascript"></script>
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
                    height: $(this).attr("WHeight"),
                    width: $(this).attr("WWidth"),
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
      $("#<%= txtDateFrom.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#<%= txtDateTo.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
    }
    </script>
    
    <asp:UpdatePanel ID="upn_Grid" runat="server">
        <%--<Triggers>
            <asp:PostBackTrigger ControlID="lbSelectedDocs" />
            <asp:PostBackTrigger ControlID="lbSelectedDocs2" />            
        </Triggers>--%>
    <ContentTemplate>
    <div style="text-align:center"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
    <table cellpadding="3" cellspacing="0" style="width:100%">  
        <tr>
            
            <td style="width:120px;">
                 <asp:Label ID="lblDocGroups" runat="server" 
                    meta:resourcekey="lblDocGroups" Text="Nhóm văn bản:"></asp:Label>
            </td>
            <td style="width:250px;">
               <asp:DropDownList ID="ddlDocGroups" runat="server" AutoPostBack="true" 
                    CssClass="userselect" DataTextField="DocGroupName" DataValueField="DocGroupId"  onselectedindexchanged="ddlDocGroups_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
            <td style="width:100px;">
            <asp:Label ID="lblLanguages" runat="server" meta:resourcekey="lblLanguages" 
                    Text="Ngôn ngữ:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="LanguageDesc" DataValueField="LanguageId" 
                    onselectedindexchanged="ddlLanguage_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
                
                <span style="margin: 0 20px 0 10px;">
                <asp:Label ID="lblCrUserId" runat="server" meta:resourcekey="lblCrUserId" 
                    Text="Tạo bởi:"></asp:Label></span>
                
                <asp:DropDownList ID="ddlCrUserId" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="UserName" DataValueField="UserId" 
                    onselectedindexchanged="ddlCrUserId_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>      
        <tr>
            <td style="width:100px;">
                <asp:Label ID="lblDocTypes" runat="server" meta:resourcekey="lblDocTypes" 
                    Text="Loại VB:"></asp:Label>
            
            </td>
            <td style="width:250px;">
                <asp:DropDownList ID="ddlDocTypes" runat="server" AutoPostBack="False" 
                    CssClass="userselect uiselect" DataTextField="DocTypeDesc" DataValueField="DocTypeId" 
                    onselectedindexchanged="ddlDocTypes_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
                 
            </td>
            <td style="width:120px;">
                 <asp:Label ID="lblEffectStatus" runat="server" 
                    meta:resourcekey="lblEffectStatus" Text="Trạng thái hiệu lực:"></asp:Label>
            </td>
            <td>
               <asp:DropDownList ID="ddlEffectStatus" runat="server" AutoPostBack="False" 
                    CssClass="userselect" DataTextField="EffectStatusDesc" 
                    DataValueField="EffectStatusId" 
                    onselectedindexchanged="ddlEffectStatus_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
                 <span style="margin: 0 18px 0 10px;">
                <asp:Label ID="lblUpdUserId" runat="server" meta:resourcekey="lblUpdUserId" 
                    Text="Sửa bởi:"></asp:Label></span>
                <asp:DropDownList ID="ddlUpdUserId" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="UserName" DataValueField="UserId" 
                    onselectedindexchanged="ddlUpdUserId_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>        
        <tr>
            <td>
                <asp:Label ID="lblOrgans" runat="server" meta:resourcekey="lblOrgans" 
                    Text="CQBH:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlOrgans" runat="server" AutoPostBack="False" 
                    CssClass="userselect uiselect" DataTextField="OrganDesc" DataValueField="OrganId" 
                    onselectedindexchanged="ddlOrgans_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
            <td>
               
                 <asp:Label ID="lblReviewStatus" runat="server" meta:resourcekey="lblReviewStatus" 
                    Text="Trạng thái duyệt:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlReviewStatus" runat="server" AutoPostBack="False" 
                    CssClass="userselect" DataTextField="ReviewStatusDesc" 
                    DataValueField="ReviewStatusId" 
                    onselectedindexchanged="ddlReviewStatus_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
                <span style="margin: 0 10px 0 10px;">
                <asp:Label ID="lblRevUserId" runat="server" meta:resourcekey="lblRevUserId" 
                    Text="Duyệt bởi:"></asp:Label></span>
                <asp:DropDownList ID="ddlRevUserId" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="UserName" DataValueField="UserId" 
                    onselectedindexchanged="ddlRevUserId_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                 <asp:Label ID="lblFields" runat="server" meta:resourcekey="lblFields" 
                    Text="Lĩnh vực:"></asp:Label>
               
            </td>
            <td>
                 <asp:DropDownList ID="ddlFields" runat="server" AutoPostBack="False" 
                    CssClass="userselect uiselect" DataTextField="FieldDesc" DataValueField="FieldId" 
                    onselectedindexchanged="ddlFields_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
                </td>
            <td>
                <asp:Label ID="lblUseStatus" runat="server" meta:resourcekey="lblUseStatus" 
                    Text="Trạng thái sử dụng:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlUseStatus" runat="server" AutoPostBack="False" 
                    CssClass="userselect" DataTextField="UseStatusDesc" 
                    DataValueField="UseStatusId" 
                    onselectedindexchanged="ddlUseStatus_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
                <span style="margin: 0 25px 0 10px;">
                    <asp:Label ID="lblDataSources" runat="server" meta:resourcekey="lblDataSources" 
                    Text="Nguồn:"></asp:Label>
                </span>
                <asp:DropDownList ID="ddlDataSources" runat="server" AutoPostBack="False" 
                    CssClass="userselect" DataTextField="DataSourceDesc" 
                    DataValueField="DataSourceId" 
                    onselectedindexchanged="ddlDataSources_SelectedIndexChanged" Width="250px">
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
                    Text="Tìm theo:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlSearchByDate" runat="server" AutoPostBack="False" 
                    CssClass="userselect" 
                    onselectedindexchanged="ddlSearchByDate_SelectedIndexChanged" Width="250px">
                    <asp:ListItem Value="IssueDate">Ngày ban hành</asp:ListItem>
                    <asp:ListItem Value="RevDateTime">Ngày duyệt</asp:ListItem>
                    <asp:ListItem Value="UpdDateTime">Ngày sửa</asp:ListItem>
                    <asp:ListItem Value="CrDateTime" Selected="True">Ngày tạo</asp:ListItem>
                    <asp:ListItem Value="EffectDate">Ngày có hiệu lực</asp:ListItem>
                    <asp:ListItem Value="ExpireDate">Ngày hết hiệu lực</asp:ListItem>
                    <asp:ListItem Value="GazetteDate">Ngày công báo</asp:ListItem>
                </asp:DropDownList>
                <span style="margin: 0 38px 0 10px;">
                    <asp:Label ID="Label2" runat="server" Text="Tỉnh:"></asp:Label>
                </span>
                <asp:DropDownList ID="ddlProvinces" runat="server" AutoPostBack="False" CssClass="userselect"
                            DataTextField="ProvinceDesc" DataValueField="ProvinceId" 
                            Width="250px">
                        </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <asp:Label ID="lblOrderBy" runat="server" meta:resourcekey="lblOrderBy" 
                    Text="Sắp xếp:"></asp:Label>
            </td>
            <td valign="top">
                <asp:DropDownList ID="ddlOrderBy" runat="server" AutoPostBack="False" 
                    CssClass="userselect" DataTextField="OrderByDesc" DataValueField="OrderBy" 
                    onselectedindexchanged="ddlOrderBy_SelectedIndexChanged" Width="120px">
                </asp:DropDownList>
                 <asp:DropDownList ID="ddlPageSize" runat="server"  CssClass="userselect"  
                    Width="125px" AutoPostBack="False" 
                    onselectedindexchanged="ddlPageSize_SelectedIndexChanged">
                    <asp:ListItem Value="10">10/trang</asp:ListItem>
                    <asp:ListItem Value="20">20/trang</asp:ListItem>
                    <asp:ListItem Value="30">30/trang</asp:ListItem>
                    <asp:ListItem Selected="True" Value="50">50/trang</asp:ListItem>
                    <asp:ListItem Value="100">100/trang</asp:ListItem>
                    <asp:ListItem Value="150">150/trang</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td valign="top">
                <asp:Label ID="lblSearch" runat="server" meta:resourcekey="lblSearch" 
                    Text="Tìm kiếm:"></asp:Label>
                <asp:Label ID="lblSearch0" runat="server" meta:resourcekey="lblSearch" onKeyDown="return submitButton(event);"
                    Text="Tìm trong:" Visible="false"></asp:Label>
            </td>
            <td valign="middle" align="left">                              
               <asp:TextBox ID="txtSearch" runat="server" CssClass="tukhoatimekiem" 
                    Width="240px"></asp:TextBox>
                 &nbsp;<asp:Button ID="btnSearch" runat="server" CssClass="timkiembutom" 
                    meta:resourcekey="btnSearch" onclick="btnSearch_Click" Text="Tìm kiếm">
                    </asp:Button>
               <br />
               <asp:RadioButtonList ID="rblFindTypes" runat="server" 
                    RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="DocIdentity">Số hiệu</asp:ListItem>
                    <asp:ListItem Value="DocName">Tiêu đề</asp:ListItem>
                    <asp:ListItem Value="DocContent">Nội dung</asp:ListItem>                   
                </asp:RadioButtonList>
                 <asp:CheckBox ID="ckbIsSearchExact" runat="server" AutoPostBack="False" 
                    Checked="false" oncheckedchanged="ckbIsSearchExact_CheckedChanged" 
                    Text="Chính xác cụm từ" />
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
        <%=GetLocalResourceObject("Docs").ToString() %>
	</div>
	<div class="chucnangright">
        <asp:Label ID="lblSelectedDisplayTypes" runat="server" meta:resourcekey="lblSelectedDisplayTypes" 
                    Text="Chọn loại hiển thị:"></asp:Label>
        &nbsp;<asp:DropDownList ID="ddlDisplayTypes" runat="server" Enabled="false" 
                    CssClass="userselect" DataTextField="DisplayTypeDesc" 
                    DataValueField="DisplayTypeId" Width="250px">
                </asp:DropDownList>
		&nbsp;<asp:LinkButton ID="lbSelectedDocs" runat="server" class="themmoi"
            Text="Chọn văn bản" meta:resourcekey="lbSelectedDocs" onclick="lbSelectedDocs_Click"> 
        </asp:LinkButton>
	</div>
    <div style="text-align:left; width:20%; float:right"><asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                            <ProgressTemplate><img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...</ProgressTemplate>
                            </asp:UpdateProgress></div>
	<div class="clear5px"></div>
    <div class="contenbangdulieu">
    
        <asp:GridView ID="m_grid" DataKeyNames="DocId" runat="server" ShowHeaderWhenEmpty="True"
            AutoGenerateColumns="False" OnRowDeleting="m_grid_RowDeleting" OnRowDataBound = "m_grid_OnRowDataBound"
            Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" 
            PageSize="50" >
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
                                     ToolTip ='<%# Eval("DocId").ToString() %>'> </asp:Label>
                    </ItemTemplate>      
                    <ItemStyle HorizontalAlign="Center" Width="3%" />  
                    <HeaderStyle Width="3%" />          
                </asp:TemplateField>        
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnDocName" runat="server" Text="Tên văn bản" meta:resourcekey="lblGridColumnDocName"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lbDocName" runat="server" Text='<%# Eval("DocName")%>' style="font-weight:bold;"></asp:Label>    
                        <br />
                        <br />
                          <a WHeight="510" WWidth="1090" title="" href="#" class="popup docsproperties" onmouseover="<%# "ShowTooltip('docstips.aspx?DocId=" + Eval( "DocId").ToString() +"&LanguageId=" + Eval( "LanguageId").ToString() + "','tt'); return false;" %>" onmouseout="HideTooltip();"  >
                            <asp:Label ID="lblGridColumnProperties" runat="server" CssClass="docsproperties" Text="Thuộc tính" meta:resourcekey="lblGridColumnProperties" ></asp:Label>
                          </a><span class="docsproperties"> | </span> 
                        <a WHeight="600" WWidth="800" href='<%# "DocSeoEdit.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + Eval("LanguageId") %>' class="popup docsproperties <%#string.IsNullOrEmpty(Eval("MetaTitle").ToString())?"active":"" %>"  title="Seo Info"><asp:Label ID="Label3" runat="server" CssClass="docsproperties" Text="Seos"></asp:Label></a>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" VerticalAlign="Top" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnDocIdentity" runat="server" Text="Ký hiệu" meta:resourcekey="lblGridColumnDocIdentity"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lbDocIdentity" runat="server" style="display:none"></asp:Label>                        
                        <%# Eval("DocIdentity")%> <br /><br />
                      <span style="color:Blue;"> <%# LanguageHelpers.IsCultureVietnamese() ? EffectStatus.Static_Get(byte.Parse(Eval("EffectStatusId").ToString()), l_EffectStatus).EffectStatusDesc : EffectStatus.Static_Get(byte.Parse(Eval("EffectStatusId").ToString()), l_EffectStatus).EffectStatusName%></span>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"/>
                    <HeaderStyle />          
                </asp:TemplateField>                        
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnProperties" runat="server" Text="Thuộc tính" meta:resourcekey="lblGridColumnProperties"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <table border="0px" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width:60px; text-align:left;"><asp:Label ID="lblGridColumnBH" CssClass="properties" runat="server" Text="BH:" meta:resourcekey="lblGridColumnBH"></asp:Label></td>
                                 <td><span style="color:Black;"> <%# GetIssueDate(int.Parse(Eval("DocId").ToString()),LanguageId) %></span></td>
                            </tr>
                            <tr>
                                <td ><asp:Label ID="lblGridColumnHL" runat="server" CssClass="properties" Text="HL:" meta:resourcekey="lblGridColumnHL"></asp:Label></td>
                                 <td><span style="color:Black;"> <%# DateTimeHelpers.GetDateHH24(Eval("EffectDate"))%></span></td>
                            </tr>
                            <tr style ="display:none">
                                <td ><asp:Label ID="lblGridColumnCQBH" runat="server" CssClass="properties" Text="CQBH:" meta:resourcekey="lblGridColumnCQBH"></asp:Label></td>
                                 <td><span style="color:Black;"><%# DocOrgans_GetOrganName(byte.Parse(Eval("LanguageId").ToString()),int.Parse(Eval("DocId").ToString())) %></span></td>
                            </tr>
                            <tr style ="display: none" >
                                <td><asp:Label ID="lblGridColumnFields" runat="server" CssClass="properties" Text="Lĩnh vực:" meta:resourcekey="lblGridColumnFields"></asp:Label></td>
                                 <td><span style="color:Black;"><%# DocFields_GetFieldName(byte.Parse(Eval("LanguageId").ToString()),int.Parse(Eval("DocId").ToString())) %></span></td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign = "Left" VerticalAlign="Top" Width="20%"/>  
                    <HeaderStyle/>        
                </asp:TemplateField>
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnStatus" runat="server" Text="Trạng thái" meta:resourcekey="lblGridColumnStatus"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                       <span class="xuatban<%# Eval("ReviewStatusId") %>" > <%# LanguageHelpers.IsCultureVietnamese() ? ReviewStatus.Static_Get(byte.Parse(Eval("ReviewStatusId").ToString()), l_ReviewStatus).ReviewStatusDesc : ReviewStatus.Static_Get(byte.Parse(Eval("ReviewStatusId").ToString()), l_ReviewStatus).ReviewStatusName%></span><br /><br />
                        <span style="color:#0C0C0C;"><%# DateTimeHelpers.GetDateAndTime(Eval("CrDateTime"))%></span><br />
                        <span style="color:#0C0C0C;"><%# UserHelpers.Static_Get(int.Parse(Eval("CrUserId").ToString()), l_Users, "").Username %></span>
                    </ItemTemplate>
                    <ItemStyle Width="8%" HorizontalAlign = "Left" VerticalAlign="Top" Wrap="false" />  
                    <HeaderStyle Width="8%" />        
                </asp:TemplateField>                       
                <asp:TemplateField Visible="false">
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnOperation" runat="server" Text="Thao tác" meta:resourcekey="lblGridColumnOperation"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:LinkButton ID="lbDelete" runat="server" CssClass="iconadmxoa" CommandName="Delete" ToolTip="Xóa" meta:resourcekey="lnkGridColumnDel"></asp:LinkButton> 
                        <asp:Label ID="lblLanguageId" runat="server" Text='<%# Eval("LanguageId") %>' Visible="false"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="center" Width="3%" Wrap="false" />
                    <HeaderStyle Width="3%" />
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
   </div>
    <div class="chucnangright">
		<asp:LinkButton ID="lbSelectedDocs2" runat="server" class="themmoi"
            Text="Chọn văn bản" meta:resourcekey="lbSelectedDocs2" onclick="lbSelectedDocs2_Click"> 
        </asp:LinkButton>
	</div> 
   <div class="clear5px"></div>    
                <uc1:CustomPaging ID="CustomPaging" runat="server" />                
                <div class="clear5px"></div>   
   </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
