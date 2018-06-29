<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="DocsConsolidation.aspx.cs" Inherits="Admin_DocsConsolidation" %>
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

            $('.popup').live('click', function (e) {
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
                        //window.location = $('#<%= btnSearch.ClientID %>').attr("href");
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
    
<div style="text-align:center"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
<asp:Panel ID="panSearch" runat="server" DefaultButton="btnSearch" Width="100%" >
  <table cellpadding="3" cellspacing="0" style="width:100%">  
        <tr>
            
            <td style="width:120px;">
                 <asp:Label ID="lblDocGroups" runat="server" 
                    meta:resourcekey="lblDocGroups" Text="Nhóm văn bản:"></asp:Label>
            </td>
            <td style="width:250px;">
               <asp:DropDownList ID="ddlDocGroups" runat="server" AutoPostBack="true" Enabled="false"
                    CssClass="userselect" onselectedindexchanged="ddlLanguage_SelectedIndexChanged" Width="250px">                        
                        <asp:ListItem Value="5" Text="Văn bản hợp nhất" Selected="True"></asp:ListItem>
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
                <asp:DropDownList ID="ddlDocTypes" runat="server" AutoPostBack="False" Enabled="false" 
                    CssClass="userselect" DataTextField="DocTypeDesc" DataValueField="DocTypeId" 
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
                    <asp:ListItem Value="IssueDate">Ngày ký xác thực</asp:ListItem>
                    <asp:ListItem Value="RevDateTime">Ngày duyệt</asp:ListItem>
                    <asp:ListItem Value="UpdDateTime">Ngày sửa</asp:ListItem>
                    <asp:ListItem Value="CrDateTime" Selected="True">Ngày tạo</asp:ListItem>
                    <asp:ListItem Value="EffectDate">Ngày có hiệu lực</asp:ListItem>
                    <asp:ListItem Value="ExpireDate">Ngày hết hiệu lực</asp:ListItem>
                    <asp:ListItem Value="GazetteDate">Ngày công báo</asp:ListItem>
                </asp:DropDownList>
                <span style="margin: 0 38px 0 10px; display:none;">
                    <asp:Label ID="Label2" runat="server" Text="Tỉnh:"></asp:Label>
                </span>
                <asp:DropDownList ID="ddlProvinces" runat="server" AutoPostBack="False" CssClass="userselect" Visible="false"
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
                <asp:Label ID="lblSearch0" runat="server" meta:resourcekey="lblSearch" 
                    Text="Tìm trong:" Visible="false"></asp:Label>
            </td>
            <td valign="middle" align="left">                              
               <asp:TextBox ID="txtSearch" runat="server" CssClass="tukhoatimekiem" 
                    Width="240px"></asp:TextBox>
                 &nbsp;<asp:LinkButton ID="btnSearch" runat="server" CssClass="timkiembutom" 
                    meta:resourcekey="btnSearch" onclick="btnSearch_Click" Text="Tìm kiếm">
                    </asp:LinkButton>
                <%--&nbsp;&nbsp;<asp:Button ID="btnRefresh" ToolTip="Refesh trang để cập nhật thay đổi" runat="server" CssClass="timkiembutom"
                     meta:resourcekey="btnRefresh" OnClick="btnRefresh_Click" Text="Refresh"></asp:Button>--%>
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
    </asp:Panel>
    <div class="clear5px"></div>
	<div class="vien"></div>
	<div class="clear5px"></div>  
    <div class="khungchucnang">
    <div class="chucnangleft">
		<span class="tieudetongcong">
            <asp:Label ID="lblTotalText" runat="server" Text="Tổng cộng:" meta:resourcekey="lblTotalText"></asp:Label>
        </span>
        <asp:Label ID="lblTong" runat="server" Text="" CssClass="tieudetongcong2"></asp:Label> 
        <%=GetLocalResourceObject("Docs").ToString() %>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:Repeater ID="rptStatusCount" runat="server">
            <ItemTemplate>
                <span><b><%# Eval("Trang Thai").ToString() %> (<%# Eval("So van ban").ToString() %>)</b></span>&nbsp;&nbsp;
            </ItemTemplate>
        </asp:Repeater>
	</div>
	<div class="chucnangright">
         <a href="DocsConsolidationEdit.aspx?LanguageId=<%=LanguageId + "&backUrl=DocsConsolidation.aspx" %>" title="<%=GetLocalResourceObject("lnkAddNew.title").ToString() %>" class="themmoi" > 
            <asp:Label ID="lblAddNew" runat="server" Text="Thêm mới" meta:resourcekey="lblAddNew"></asp:Label>
        </a>
        <asp:LinkButton ID="lbDelete" runat="server" CssClass="xoatin" Text="Xóa" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa các văn bản đã chọn?')" 
            meta:resourcekey="lbDelete" onclick="lbDelete_Click">
        </asp:LinkButton>
	</div>
    <div style="text-align:left; width:40%; float:right"></div>
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
                        <a href='<%# "DocsConsolidationEdit.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + Eval("LanguageId")  %>' title="Sửa văn bản" style="color:black;" > 
                            <asp:Label ID="lbDocName" runat="server" Text='<%# Eval("DocName")%>' style="font-weight:bold;"></asp:Label>                        
                        </a> 
                        <br />
                        <br />
                        <a WHeight="550" WWidth="980" title="Thêm/Sửa thuộc tính" href="<%# "DocsEditPropertie.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + Eval("LanguageId")  %>" class="popup docsproperties" onmouseover="<%# "ShowTooltip('docstips.aspx?DocId=" + Eval( "DocId").ToString() +"&LanguageId=" + Eval( "LanguageId").ToString() + "','tt'); return false;" %>" onmouseout="HideTooltip();"  >
                            <asp:Label ID="lblGridColumnProperties" runat="server" CssClass="docsproperties" Text="Thuộc tính" meta:resourcekey="lblGridColumnProperties" ></asp:Label>
                          </a>
                            <span class="docsproperties"> | </span> 
                        
                          <a WHeight="600" WWidth="1100" href='<%# "DocRelatesEditVBHN.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + Eval("LanguageId") %>' class="docsproperties popup <%#HasProperties(Eval("HasDocRelate").ToString())  ? "":"active" %>" title="<%=GetLocalResourceObject("lblGridColumnDocRelateEdit.title").ToString() %>">
                              <asp:Label ID="lblGridColumnLienQuan" runat="server" CssClass="docsproperties" Text="Liên quan" meta:resourcekey="lblGridColumnLienQuan"></asp:Label>
                          </a>  
                        <span class="docsproperties"> | </span> 
                          
                          <a WHeight="500" WWidth="850" href='<%# "DocsFielsEdit.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + Eval("LanguageId") %>' class="docsproperties popup <%#HasProperties(Eval("HasFile").ToString())  ? "":"active" %>"  title="<%=GetLocalResourceObject("lblGridColumnFile.title").ToString() %>">
                              <asp:Label ID="lblGridColumnFile" runat="server" CssClass="docsproperties" Text="File" meta:resourcekey="lblGridColumnFile"></asp:Label>
                          </a>
                        <span class="docsproperties"> | </span>   

                         <a href='<%# "DocsUpdateContentEdit.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + Eval("LanguageId") %>' class="docsproperties <%#HasProperties(Eval("HasContent").ToString())  ? "":"active" %>" title="<%=GetLocalResourceObject("lblGridColumnContent.title").ToString() %>">
                             <asp:Label ID="lblGridColumnContent" runat="server" CssClass="docsproperties" Text="Nội dung" meta:resourcekey="lblGridColumnContent"></asp:Label>
                         </a>
                        <span class="docsproperties"> | </span>   
                         
                       <a href='<%# "DocIndexes.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + Eval("LanguageId") %>' class="docsproperties <%#HasProperties(Eval("HasDocIndex").ToString())  ? "":"active" %>" title="Mục lục văn bản">
                           <asp:Label ID="Label1" runat="server" CssClass="docsproperties" Text="Mục lục" ></asp:Label>
                       </a>   
                        <span class="docsproperties"> | </span> 

                        <a  WHeight="500" WWidth="800" href='<%# "DocsUpdateSummaryEdit.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + Eval("LanguageId") %>' class="docsproperties popup <%# getClassSeo(Eval("DocSummary"),Eval("UpdUserId"))%>"  title="<%=GetLocalResourceObject("lblGridColumnSummary.title").ToString() %>">
                            <asp:Label ID="lblGridColumnSummary" runat="server" CssClass="docsproperties" Text="Trích dẫn" meta:resourcekey="lblGridColumnSummary"></asp:Label>
                        </a>
                        
                        <span class="docsproperties"> | </span>  
                         
                        <a href='<%# "DocsContentRelation.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + Eval("LanguageId") %>' class="docsproperties <%#HasProperties(Eval("HasDocRelate").ToString())  ? "":"active" %>"  title="<%=GetLocalResourceObject("lblGridColumnContentRelation.title").ToString() %>">
                            <asp:Label ID="lblGridColumnContentRelation" runat="server" CssClass="docsproperties" Text="Lược đồ" meta:resourcekey="lblGridColumnContentRelation"></asp:Label>
                        </a> 
                          <span class="docsproperties"> | </span> 
                          
                          <a WHeight="600" WWidth="800" href='<%# "DocSeoEdit.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + Eval("LanguageId") %>' class="popup docsproperties <%# getClassSeo(Eval("MetaTitle"),Eval("UpdUserId"))%>"  title="Seo Info">
                              <asp:Label ID="Label3" runat="server" CssClass="docsproperties" Text="Seos"></asp:Label>
                          </a>
                          
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
                                <td style="width:60px; text-align:left;"><asp:Label ID="lblGridColumnBH" CssClass="properties" runat="server" Text="XT:" ></asp:Label></td>
                                 <td><span style="color:Black;"> <%# DateTimeHelpers.GetDateHH24(Eval("IssueDate"))%></span></td>
                            </tr>
                            <tr>
                                <td ><asp:Label ID="lblGridColumnHL" runat="server" CssClass="properties" Text="HL:" meta:resourcekey="lblGridColumnHL"></asp:Label></td>
                                 <td><span style="color:Black;"> <%# DateTimeHelpers.GetDateHH24(Eval("EffectDate"))%></span></td>
                            </tr>
                            <tr style ="display:none">
                                <td ><asp:Label ID="lblGridColumnCQBH" runat="server" CssClass="properties" Text="CQBH:" meta:resourcekey="lblGridColumnCQBH"></asp:Label></td>
                                 <td><span style="color:Black;"><%# DocOrgans_GetOrganName(byte.Parse(Eval("LanguageId").ToString()),int.Parse(Eval("DocId").ToString())) %></span></td>
                            </tr>
                            <tr>
                                <td><asp:Label ID="lblGridColumnFields" runat="server" CssClass="properties" Text="Lĩnh vực:" meta:resourcekey="lblGridColumnFields"></asp:Label></td>
                                 <td><span style="color:Black;"><%# DocFields_GetFieldName(byte.Parse(Eval("LanguageId").ToString()),int.Parse(Eval("DocId").ToString())) %></span></td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign = "Left" VerticalAlign="Top" Width="15%"/>  
                    <HeaderStyle/>        
                </asp:TemplateField>
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblFiles" runat="server" Text="Files" meta:resourcekey="lblFiles"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                       <a WHeight="400" WWidth="850" href='<%# "DocsFielsEdit.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + Eval("LanguageId") %>' class="popup docsproperties"  title="<%=GetLocalResourceObject("lblGridColumnFile.title").ToString() %>"> <%# Eval("DocId").ToString()+".law" %></a>
                    </ItemTemplate>
                    <ItemStyle Width="5%" HorizontalAlign = "Left" VerticalAlign="Top" Wrap="false" />  
                    <HeaderStyle Width="5%" />        
                </asp:TemplateField>
               <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnStatus" runat="server" Text="Trạng thái" meta:resourcekey="lblGridColumnStatus"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                       <span class="xuatban<%# Eval("ReviewStatusId") %>" > <%# LanguageHelpers.IsCultureVietnamese() ? ReviewStatus.Static_Get(byte.Parse(Eval("ReviewStatusId").ToString()), l_ReviewStatus).ReviewStatusDesc : ReviewStatus.Static_Get(byte.Parse(Eval("ReviewStatusId").ToString()), l_ReviewStatus).ReviewStatusName%></span><br />
                         <a WHeight="400" WWidth="550" href="<%=CmsConstants.ROOT_PATH %>admin/pages/WebNotify/NotifyMessagesEdit.aspx?DocId=<%# Eval("DocId") %>&LanguageId=<%# Eval("LanguageId") %>&BackUrl=Pages/lawsdocs/DocsConsolidation.aspx" class="popup" style="color:blue;" title="Gửi Web Notify"> Gửi Notify</a>    
                    </ItemTemplate>
                    <ItemStyle Width="8%" HorizontalAlign = "Left" VerticalAlign="Top" Wrap="false" />  
                    <HeaderStyle Width="8%" />        
                </asp:TemplateField>
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnUserCreate" runat="server" Text="Tạo bởi" meta:resourcekey="lblGridColumnUserCreate"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <span class="ngaythang">
                            <font style="color:Blue"><%# UserHelpers.Static_Get(int.Parse(Eval("CrUserId").ToString()), l_Users, "").Username %></font><br />
                            <%# DateTimeHelpers.GetDateAndTime(Eval("CrDateTime")).Replace(" ","<br />") %>
                        </span> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="80px" />        
                </asp:TemplateField> 
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnUserUpdate" runat="server" Text="Sửa bởi" meta:resourcekey="lblGridColumnUserUpdate"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <span class="ngaythang">
                            <font style="color:Blue"><%# UserHelpers.Static_Get(int.Parse(Eval("UpdUserId").ToString()), l_Users, "").Username %></font><br />
                            <%# DateTimeHelpers.GetDateAndTime(Eval("UpdDateTime")).Replace(" ","<br />") %>
                        </span> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="80px" />        
                </asp:TemplateField> 
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnUserReview" runat="server" Text="Duyệt bởi" meta:resourcekey="lblGridColumnUserReview"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <span class="ngaythang">
                            <font style="color:Blue"><%# UserHelpers.Static_Get(int.Parse(Eval("RevUserId").ToString()), l_Users, "").Username %></font><br />
                            <%# DateTimeHelpers.GetDateAndTime(Eval("RevDateTime")).Replace(" ","<br />") %>
                        </span> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="80px" />        
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
   <div class="clear5px"></div>    
                <uc1:CustomPaging ID="CustomPaging" runat="server" />                
                <div class="clear5px"></div>   
</asp:Content>
