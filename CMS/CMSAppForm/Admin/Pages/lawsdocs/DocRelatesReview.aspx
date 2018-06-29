<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="DocRelatesReview.aspx.cs" Inherits="Admin_DocRelatesReview" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/admin/UserControls/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="uc1" %>
<%@ Import Namespace="ICSoft.CMSLib" %>
<%@ Import Namespace="ICSoft.LawDocsLib" %>
<%@ Import Namespace="ICSoft.HelperLib" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
<script src="../../Js/ajaxTooltip/ajax-tooltip.js" type="text/javascript"></script>
<script src="../../Js/ajaxTooltip/ajax-dynamic-content.js" type="text/javascript"></script>
<script src="../../Js/ajaxTooltip/ajax.js" type="text/javascript"></script>


    <div style="text-align:center"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
<asp:Panel ID="panSearch" runat="server" DefaultButton="btnSearch" Width="100%" >
    <table cellpadding="4" cellspacing="0" style="width:100%">
        <tr>
            <td style="width:70px; white-space:nowrap;">
                <asp:Label ID="lblLanguage" runat="server" meta:resourcekey="lblLanguage" 
                    Text="Ngôn ngữ:"></asp:Label>
            </td>
            <td width="240px">
                <asp:DropDownList ID="ddlLanguage" runat="server" 
                    CssClass="userselect" DataTextField="LanguageDesc" 
                    DataValueField="LanguageId" Width="240px" Visible="true" 
                    AutoPostBack="True" onselectedindexchanged="ddlLanguage_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="width:90px; white-space:nowrap;">
                <asp:Label ID="lblRelateTypes" runat="server" meta:resourcekey="lblRelateTypes" 
                    Text="Kiểu liên kết:"></asp:Label>
            </td>
            <td align="left" valign="top" rowspan="3" >
                <asp:CheckBoxList ID="cblRelateTypes" runat="server" 
                    DataTextField="RelateTypeDesc" DataValueField="RelateTypeId" RepeatColumns="4">
                </asp:CheckBoxList>
            </td>            
                      
        </tr>
        <tr>
             <td style="width:70px; white-space:nowrap;">
                <asp:Label ID="lblDocGroups" runat="server" meta:resourcekey="lblDocGroups" 
                    Text="Nhóm văn bản:"></asp:Label>
            </td>
            <td width="240px">
                <asp:DropDownList ID="ddlDocGroups" runat="server" 
                    CssClass="userselect" DataTextField="DocGroupDesc" 
                    DataValueField="DocGroupId" Width="240px" Visible="true" 
                    AutoPostBack="True" onselectedindexchanged="ddlLanguage_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            
            <td>
                &nbsp;</td>
        </tr>
        <tr>
             <td>
                <asp:Label ID="lblDocIdentity" runat="server" meta:resourcekey="lblDocIdentity" 
                    Text="Số hiệu:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDocIdentity" runat="server" CssClass="tukhoatimekiem"  onKeyDown="return submitButton(event);"
                    Width="230px"></asp:TextBox>
                </td>
            
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width:70px; white-space:nowrap;">
                <asp:Label ID="lblReviewStatus" runat="server" 
                    meta:resourcekey="lblReviewStatus" Text="Trạng thái:"></asp:Label>
            </td>
            <td width="240px">
                <asp:DropDownList ID="ddlReviewStatus" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="ReviewStatusDesc" 
                    DataValueField="ReviewStatusId" 
                    onselectedindexchanged="ddlReviewStatus_SelectedIndexChanged" Width="240px">
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
            </tr>
        <tr>
            <td>
                <asp:Label ID="lblDateFrom" runat="server" meta:resourcekey="lblDateFrom" 
                    Text="Từ ngày:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDateFrom" runat="server" CssClass="tukhoatimekiem"  onKeyDown="submitButton(event)"
                    Width="90px"></asp:TextBox>
                <asp:Label ID="lblDateTo" runat="server" meta:resourcekey="lblDateTo"  
                    Text="Đến"></asp:Label>
                <asp:TextBox ID="txtDateTo" runat="server" CssClass="tukhoatimekiem"  onKeyDown="submitButton(event)"
                    Width="90px"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblSearchByDate" runat="server" 
                    meta:resourcekey="lblSearchByDate" Text="Tìm theo:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlSearchByDate" runat="server" AutoPostBack="True" 
                    CssClass="userselect" Width="150px" 
                    onselectedindexchanged="ddlSearchByDate_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Value="RevDateTime">Ngày duyệt</asp:ListItem>
                    <asp:ListItem Value="CrDateTime">Ngày tạo</asp:ListItem>
                    <asp:ListItem Value="IssueDate">Ngày ban hành</asp:ListItem>
                    <asp:ListItem Value="EffectDate">Ngày có hiệu lực</asp:ListItem>
                    <asp:ListItem Value="ExpireDate">Ngày hết hiệu lực</asp:ListItem>
                </asp:DropDownList>
              &nbsp;  <asp:Button meta:resourcekey="btnSearch" Text="Tìm kiếm" ID="btnSearch" runat="server" CssClass="timkiembutom" OnClick="btnSearchS_Click"/>
                
               <%-- &nbsp;&nbsp;<asp:Button ID="btnRefresh" ToolTip="Refesh trang để cập nhật thay đổi" runat="server" CssClass="timkiembutom"
                     meta:resourcekey="btnRefresh" OnClick="btnRefresh_Click" Text="Refresh"></asp:Button>
                &nbsp;<asp:LinkButton ID="btnSearch" runat="server" CssClass="timkiembutom" 
                    meta:resourcekey="btnSearch" onclick="btnSearch_Click" Text="Tìm kiếm"> </asp:LinkButton>--%>
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
        <%--<%=GetLocalResourceObject("DocRelatesReview").ToString()%>--%>
	</div>
	<div class="chucnangright">
        <asp:LinkButton ID="lbReview" runat="server" CssClass="duyettin" Text="Duyệt" 
            meta:resourcekey="lbReview" onclick="lbReview_Click"> </asp:LinkButton>					
        <asp:LinkButton ID="lbUnCheck" runat="server" CssClass="boduyet" 
            Text="Bỏ duyệt" meta:resourcekey="lbUnCheck" onclick="lbUnCheck_Click"> </asp:LinkButton>
        <asp:LinkButton ID="lbDelete" runat="server" CssClass="xoatin" Text="Xóa" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa các liên kết văn bản đã chọn?')" 
            meta:resourcekey="lbDelete" onclick="lbDelete_Click" ToolTip="Xóa các liên kết được tick chọn" > </asp:LinkButton>
	</div>
    <%--<div style="text-align:left; width:200px; float:right"><asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                            <ProgressTemplate><img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...</ProgressTemplate>
                            </asp:UpdateProgress></div>--%>
	<div class="clear5px"></div>
    <div class="contenbangdulieu">
    
        <asp:GridView ID="m_grid" DataKeyNames="DocRelateId" runat="server" ShowHeaderWhenEmpty="True"
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
                                     ToolTip ='<%# Eval("DocRelateId").ToString() %>'> </asp:Label>
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
                       <b><%# Eval("DocName")%></b>        
                        <br />
                        <br />
                        <asp:Label ID="lblGridColumnIssueDate" runat="server" Text="Ngày BH:" CssClass="properties" meta:resourcekey="lblGridColumnIssueDateRef"></asp:Label>
                        <span style="color:Blue;" ><%# DateTimeHelpers.GetDateHH24(Eval("IssueDate"))%></span> - 
                        <asp:Label ID="lblGridColumnEffectDate" runat="server" Text="Ngày HL:" CssClass="properties" meta:resourcekey="lblGridColumnEffectDateRef"></asp:Label>
                        <span style="color:Blue;" ><%# DateTimeHelpers.GetDateHH24(Eval("EffectDate"))%></span>&nbsp;<br />
                        <a WHeight="550" WWidth="950" href="<%# "DocsEditPropertie.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + ddlLanguage.SelectedValue.ToString()  %>" class="popup docsproperties" onmouseover="<%# "ShowTooltip('docstips.aspx?DocId=" + Eval( "DocId").ToString() +"&LanguageId=" + ddlLanguage.SelectedValue.ToString() + "','tt'); return false;" %>" onmouseout="HideTooltip();"  >
                            <asp:Label ID="lblGridColumnProperties" runat="server" CssClass="docsproperties" Text="Thuộc tính" meta:resourcekey="lblGridColumnProperties" ></asp:Label>
                        </a>
                        <span class="docsproperties"> | </span>                            
                        <a WHeight="400" WWidth="850" href='<%# "DocsFielsEdit.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + ddlLanguage.SelectedValue.ToString() %>' class="popup docsproperties"  title="Thêm Sửa file"><asp:Label ID="lblGridColumnFile" runat="server" CssClass="docsproperties" Text="[File]" ></asp:Label></a>
                        <span class="docsproperties"> | </span> 
                        <a WHeight="600" WWidth="1050" href='<%# "DocRelatesEdit.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + ddlLanguage.SelectedValue.ToString() %>' class="popup docsproperties" ><asp:Label  runat="server" CssClass="docsproperties" ID="lblGridColumnLienQuan" Text="[Liên quan]" meta:resourcekey="lblGridColumnLienQuan"></asp:Label></a>
                        <span class="docsproperties"> | </span>  
                          <a WHeight="450" WWidth="800" href='<%# "DocSeoEdit.aspx?DocId=" +  Eval("DocId").ToString() + "&LanguageId=" + Eval("LanguageId") %>' 
                              class="popup docsproperties  <%#string.IsNullOrEmpty(Eval("MetaTitle").ToString())?"active":"" %>"  title="Seo Info">
                              <asp:Label ID="lblSeos" runat="server" CssClass="docsproperties" Text="[Seos]"></asp:Label>
                          </a>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" VerticalAlign="Top" Width="35%"/>
                    <HeaderStyle />          
                </asp:TemplateField> 
                
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnDocNameRef" runat="server" Text="Văn bản liên quan" meta:resourcekey="lblGridColumnDocNameRef"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lbDocNameRef" runat="server" style="display:none"></asp:Label>
                       <b><%# Eval("DocNameRef")%></b>          
                        <br />
                        <br />
                         <asp:Label ID="lblGridColumnIssueDateRef" runat="server" Text="Ngày BH:" CssClass="properties" meta:resourcekey="lblGridColumnIssueDateRef"></asp:Label>
                        <span style="color:Blue;" ><%# DateTimeHelpers.GetDateHH24(Eval("IssueDateRef"))%></span> - <asp:Label ID="lblGridColumnEffectDateRef" runat="server" Text="Ngày HL:" CssClass="properties" meta:resourcekey="lblGridColumnEffectDateRef"></asp:Label>
                        <span style="color:Blue;" ><%# DateTimeHelpers.GetDateHH24(Eval("EffectDateRef"))%></span>&nbsp;<br />
                        <a WHeight="510" WWidth="1090" href="<%# "DocsEditPropertie.aspx?DocId=" + Eval("DocReferenceId").ToString() + "&LanguageId=" + ddlLanguage.SelectedValue.ToString()  %>" class="popup docsproperties" onmouseover="<%# "ShowTooltip('docstips.aspx?DocId=" + Eval( "DocReferenceId").ToString() +"&LanguageId=" + ddlLanguage.SelectedValue.ToString() + "','tt'); return false;" %>" onmouseout="HideTooltip();"  >
                            <asp:Label ID="lblGridColumnPropertiesREF" runat="server" CssClass="docsproperties" Text="Thuộc tính" meta:resourcekey="lblGridColumnProperties" ></asp:Label>
                        </a>
                        <span class="docsproperties"> | </span>                            
                        <a WHeight="400" WWidth="850" href='<%# "DocsFielsEdit.aspx?DocId=" + Eval("DocReferenceId").ToString() + "&LanguageId=" + ddlLanguage.SelectedValue.ToString() %>' class="popup docsproperties"  title="Thêm Sửa file"><asp:Label ID="lblGridColumnFileREF" runat="server" CssClass="docsproperties" Text="[File]" ></asp:Label></a>
                        <span class="docsproperties"> | </span> 
                        <a WHeight="600" WWidth="1050" href='<%# "DocRelatesEdit.aspx?DocId=" + Eval("DocReferenceId").ToString() + "&LanguageId=" + ddlLanguage.SelectedValue.ToString() %>' class="popup docsproperties" ><asp:Label ID="lblGridColumnLienQuanREF" runat="server" CssClass="docsproperties" Text="[Liên quan]" meta:resourcekey="lblGridColumnLienQuanREF"></asp:Label></a>                        
                        <span class="docsproperties"> | </span>  
                          <a WHeight="450" WWidth="800" href='<%# "DocSeoEdit.aspx?DocId=" +  Eval("DocReferenceId").ToString() + "&LanguageId=" + Eval("LanguageId") %>' 
                              class="popup docsproperties  <%#string.IsNullOrEmpty(Eval("MetaTitleRef").ToString())?"active":"" %>"  title="Seo Info">
                              <asp:Label ID="lblSeosRef" runat="server" CssClass="docsproperties" Text="[Seos]"></asp:Label>
                          </a>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" VerticalAlign="Top" Width="35%"/>
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblFiles" runat="server" Text="Files" meta:resourcekey="lblFiles"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                       <a WHeight="400" WWidth="850" href='<%# "DocsFielsEdit.aspx?DocId=" + Eval("DocReferenceId").ToString() + "&LanguageId=" + ddlLanguage.SelectedValue.ToString() %>' class="popup docsproperties"  title="Files Văn bản liên kết"> <%# Eval("DocReferenceId").ToString()+".law" %></a>
                    </ItemTemplate>
                    <ItemStyle Width="5%" HorizontalAlign = "Left" VerticalAlign="Top" Wrap="false" />  
                    <HeaderStyle Width="5%" />        
                </asp:TemplateField>
                 <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnRelateType" runat="server" Text="Kiểu liên kết" meta:resourcekey="lblGridColumnRelateType"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lbRelateType" runat="server" style="display:none"></asp:Label>                        
                        <a id="popup3"  href='<%# "DocRelatesEdit.aspx?DocId=" + Eval("DocId").ToString() +"&LanguageId=" + ddlLanguage.SelectedValue.ToString() %>' style="color:Blue;" title="<%=GetLocalResourceObject("lblGridColumnDocRelateEdit.title").ToString() %>"><asp:Label ID="lblGridColumnlbRelateType" runat="server" Text='<%# LanguageHelpers.IsCultureVietnamese() ? RelateTypes.Static_Get(byte.Parse(Eval("RelateTypeId").ToString()), l_RelateTypes).RelateTypeDesc : RelateTypes.Static_Get(byte.Parse(Eval("RelateTypeId").ToString()), l_RelateTypes).RelateTypeName%>' meta:resourcekey="lblGridColumnLienQuan"></asp:Label></a>
                        <br /><br />
                         <span class="xuatban<%# Eval("ReviewStatusId") %>" > <%# LanguageHelpers.IsCultureVietnamese() ? ReviewStatus.Static_Get(byte.Parse(Eval("ReviewStatusId").ToString()), l_ReviewStatus).ReviewStatusDesc : ReviewStatus.Static_Get(byte.Parse(Eval("ReviewStatusId").ToString()), l_ReviewStatus).ReviewStatusName%></span>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"/>
                    <HeaderStyle />          
                </asp:TemplateField>
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnTime" runat="server" Text="Người tạo" ></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <br />
                        <span style="color:#0C0C0C;"><%# DateTimeHelpers.GetDateAndTime(Eval("CrDateTime"))%></span>
                        <br />
                        <br />
                        <span style="color:#0C0C0C;"><%# UserHelpers.Static_Get(int.Parse(Eval("CrUserId").ToString()), l_Users, "").Username %></span>
                    </ItemTemplate>
                    <ItemStyle Width="5%" HorizontalAlign ="Center" VerticalAlign="Top" Wrap="false" />  
                    <HeaderStyle Width="5%" />        
                </asp:TemplateField> 
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnTime" runat="server" Text="Người duyệt" ></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <br />
                        <span style="color:#0C0C0C;"><%# DateTimeHelpers.GetDateAndTime(Eval("RevDateTime"))%></span>
                        <br />
                        <br />
                        <span style="color:#0C0C0C;"><%# UserHelpers.Static_Get(int.Parse(Eval("RevUserId").ToString()), l_Users, "").Username %></span>
                    </ItemTemplate>
                    <ItemStyle Width="5%" HorizontalAlign ="Center" VerticalAlign="Top" Wrap="false" />  
                    <HeaderStyle Width="5%" />        
                </asp:TemplateField>                       
                <asp:TemplateField Visible="false">
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnOperation" runat="server" Text="Thao tác" meta:resourcekey="lblGridColumnOperation"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:LinkButton ID="lbDelete" runat="server" CssClass="iconadmxoa" CommandName="Delete" ToolTip="Xóa" meta:resourcekey="lnkGridColumnDel"></asp:LinkButton> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="center" Width="3%" Wrap="false" />
                    <HeaderStyle Width="3%" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemStyle HorizontalAlign="Center" Width="3%" />
                    <HeaderStyle Width="3%" />
                    <HeaderTemplate>
                        <input type="checkbox" name="SelectAllCheckBox" onclick="SelectAll(this)" title="Chọn/Bỏ chọn tất cả">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkAction" runat="server" ToolTip="Tick chọn để xóa"></asp:CheckBox>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
   </div>
   <div class="clear5px"></div>    
                <uc1:CustomPaging ID="CustomPaging" runat="server" />                
                <div class="clear5px"></div>   
    <script type="text/javascript">
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);
            $("#<%= txtDateFrom.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
            $("#<%= txtDateTo.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });

            $('a#popup3').live('click', function (e) {
                var page = $(this).attr("href")
                var cdialog = $('<div id="divEdit"></div>')
                .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="98%" height="98%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 600,
                    width: 1020,
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
    function submitButton(event) {
        if (event.which == 13) { 
           $('#<%= btnSearch.ClientID %>').click();
        }
    }
    </script>
</asp:Content>
