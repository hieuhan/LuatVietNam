<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="DocDisplaysByField.aspx.cs" Inherits="Admin_DocDisplays" %>
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

            $('a#popup').live('click', function (e) {
                var page = $(this).attr("href")
                var cdialog = $('<div id="divEdit"></div>')
                .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="98%" height="98%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 600,
                    width: 1200,
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
            $('a#popup1').live('click', function (e) {
                var page = $(this).attr("href")
                var cdialog = $('<div id="divEdit"></div>')
                .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="98%" height="98%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 260,
                    width: 420,
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
    }
    </script>
    <%--<asp:UpdatePanel ID="upn_Grid" runat="server">
    <ContentTemplate>--%>
    <div style="text-align:center"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
    <table cellpadding="4" cellspacing="0" style="width:100%">
        <tr>
            <td width="80px">
                <asp:Label ID="lblLanguage" runat="server" meta:resourcekey="lblLanguage" 
                    Text="Ngôn ngữ:"></asp:Label>
            </td>
            <td width="250px">
                <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="LanguageDesc" DataValueField="LanguageId" 
                    onselectedindexchanged="ddlLanguage_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
            <td width="80px">
                <asp:Label ID="lblDisplayTypes" runat="server" 
                    meta:resourcekey="lblDisplayTypes" Text="Loại hiển thị:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlDisplayTypes" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="DisplayTypeDesc" 
                    DataValueField="DisplayTypeId" 
                    onselectedindexchanged="ddlOrganTypes_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="80px">
                <asp:Label ID="lblOrderBy" runat="server" meta:resourcekey="lblOrderBy" 
                    Text="Sắp xếp:"></asp:Label>
            </td>
            <td width="250px">
                <asp:DropDownList ID="ddlOrderBy" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="OrderByDesc" DataValueField="OrderBy" 
                    onselectedindexchanged="ddlOrderBy_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
            <td width="80px">
                <asp:Label ID="Label1" runat="server" Text="Lĩnh vực:"></asp:Label>
            </td>
            <td width="250px">
                <asp:DropDownList ID="ddlFields" runat="server" AutoPostBack="False" 
                                  CssClass="userselect" DataTextField="FieldDesc" 
                                  DataValueField="FieldId" 
                                  onselectedindexchanged="ddlFields_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
            <td>
                <asp:LinkButton ID="btnSearch" runat="server" CssClass="timkiembutom" 
                    meta:resourcekey="btnSearch" onclick="btnSearch_Click" Text="Tìm kiếm"> </asp:LinkButton>
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
	</div>
	<div class="chucnangright">
		<a id="popup" href="DocsSelected.aspx?DocGroupId=<%= DocGroupId %>&DisplayTypeId=<%= ddlDisplayTypes.SelectedValue %>&LanguageId=<%=ddlLanguage.SelectedValue %>&FileTypeId=<%=FileTypeId %>&FieldId=<%=FieldId %>" title="<%=GetLocalResourceObject("lnkAddNew.title").ToString() %>" class="themmoi" > 
            <asp:Label ID="lblAddNew" runat="server" Text="Chọn văn bản" meta:resourcekey="lblAddNew"></asp:Label>
        </a>
        <asp:LinkButton ID="lbDelete" runat="server" CssClass="xoatin" Text="Xóa" 
            meta:resourcekey="lbDelete" onclick="lbDelete_Click"> </asp:LinkButton>
	</div>
    <%--<div style="text-align:left; width:30%; float:right"><asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                            <ProgressTemplate><img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...</ProgressTemplate>
                            </asp:UpdateProgress></div>--%>
	<div class="clear5px"></div>
    <div class="contenbangdulieu">
    
        <asp:GridView ID="m_grid" DataKeyNames="DocDisplayId" runat="server" ShowHeaderWhenEmpty="True"
            AutoGenerateColumns="False" 
            OnRowDeleting="m_grid_RowDeleting" OnRowDataBound = "m_grid_OnRowDataBound" 
            OnRowEditing="m_grid_RowEditing" OnRowCancelingEdit="m_grid_RowCancelingEdit" OnRowUpdating="m_grid_RowUpdating"
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
                     <asp:Label ID="lbNo" runat="server" Text='<%# Container.DataItemIndex + 1%>'> </asp:Label>
                    </ItemTemplate>      
                    <ItemStyle HorizontalAlign="Center" Width="3%" />  
                    <HeaderStyle Width="3%" />          
                </asp:TemplateField>                  
                <asp:TemplateField Visible="false" > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnDisplayTypeId" runat="server" Text="Loại hiển thị" meta:resourcekey="lblGridColumnDisplayTypeId"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# LanguageHelpers.IsCultureVietnamese() ? DisplayTypes.Static_Get(byte.Parse(Eval("DisplayTypeId").ToString()), l_DisplayTypes).DisplayTypeDesc : DisplayTypes.Static_Get(byte.Parse(Eval("DisplayTypeId").ToString()), l_DisplayTypes).DisplayTypeName%>                       
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" Width="15%" />
                    <HeaderStyle Width="15%"/>          
                </asp:TemplateField> 
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnDocId" runat="server" Text="Tên văn bản" meta:resourcekey="lblGridColumnDocId"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate>                       
                        <b><%# Eval("DocName") %></b>
                        <br />
                        <br />
                          <a WHeight="510" WWidth="1090" title="" href="javascript:void(0)" class="docsproperties" onmouseover="<%# "ShowTooltip('docstips.aspx?DocId=" + Eval( "DocId").ToString() +"&LanguageId=" + Eval( "LanguageId").ToString() + "','tt'); return false;" %>" onmouseout="HideTooltip();"  >
                            <asp:Label ID="lblGridColumnProperties" runat="server" CssClass="docsproperties" Text="Thuộc tính" meta:resourcekey="lblGridColumnProperties" ></asp:Label>
                          </a><span class="docsproperties"> | </span> 
                        <a WHeight="600" WWidth="800" href='<%# "DocSeoEdit.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + Eval("LanguageId") %>' class="popup docsproperties"  title="Seo Info"><asp:Label ID="Label3" runat="server" CssClass="docsproperties" Text="Seos"></asp:Label></a>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign = "Left" />  
                    <HeaderStyle/>        
                </asp:TemplateField>
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnProperties" runat="server" Text="Thuộc tính" meta:resourcekey="lblGridColumnProperties"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width:60px; text-align:left;"><asp:Label ID="lblGridColumnBH" CssClass="properties" runat="server" Text="BH:" meta:resourcekey="lblGridColumnBH"></asp:Label></td>
                                 <td><span style="color:Black;"><%# GetIssueDate(int.Parse(Eval("DocId").ToString()),LanguageId) %></span></td>
                            </tr>
                            <tr>
                                <td ><asp:Label ID="lblGridColumnHL" runat="server" CssClass="properties" Text="HL:" meta:resourcekey="lblGridColumnHL"></asp:Label></td>
                                 <td><span style="color:Black;"> <%# DateTimeHelpers.GetDateHH24(Eval("EffectDate"))%></span></td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign = "Left" VerticalAlign="Top" Width="15%"/>  
                    <HeaderStyle/>        
                </asp:TemplateField>
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        Trạng thái
                    </HeaderTemplate>  
                    <ItemTemplate> 
                       <span class="xuatban<%# Eval("ReviewStatusId") %>" > <%# LanguageHelpers.IsCultureVietnamese() ? ReviewStatus.Static_Get(byte.Parse(Eval("ReviewStatusId").ToString()), l_ReviewStatus).ReviewStatusDesc : ReviewStatus.Static_Get(byte.Parse(Eval("ReviewStatusId").ToString()), l_ReviewStatus).ReviewStatusName%></span>
                    </ItemTemplate>
                    <ItemStyle Width="80px" HorizontalAlign = "Left" VerticalAlign="Top" Wrap="false" />  
                    <HeaderStyle Width="80px" />        
                </asp:TemplateField>
                <%--<asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnDisplayOrder" runat="server" Text="Thứ tự" meta:resourcekey="lblGridColumnDisplayOrder"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <asp:LinkButton ID="lbTop" runat="server" CommandName="OrderTop" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" ToolTip="" ><img src="../../Images/uptop.gif" /></asp:LinkButton>
                        <asp:LinkButton ID="lbUp" runat="server" CommandName="OrderUp" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" ToolTip="" ><img src="../../Images/stepup.gif" /></asp:LinkButton>
                        <asp:LinkButton ID="lbDown" runat="server" CommandName="OrderDown" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" ToolTip="" ><img src="../../Images/stepdown.gif" /></asp:LinkButton>
                        <asp:LinkButton ID="lbBottom" runat="server" CommandName="OrderBottom" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" ToolTip='<%# Eval("DisplayOrder").ToString() %>' ><img src="../../Images/upbottom.gif" /></asp:LinkButton>
                    </ItemTemplate>
                     <EditItemTemplate>
                         <asp:TextBox ID="txtDisplayOrder" runat="server" Columns="8" MaxLength="8" Text='<%# Bind("DisplayOrder") %>' />
                     </EditItemTemplate>
                    <ItemStyle Width="5%" HorizontalAlign = "Left" Wrap="false" />  
                    <HeaderStyle Width="5%" />        
                </asp:TemplateField> --%>         
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnUser" runat="server" Text="Tạo bởi" meta:resourcekey="lblGridColumnUser"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <span class="ngaythang">
                            <b><%# UserHelpers.Static_Get(int.Parse(Eval("CrUserId").ToString()), l_Users, "").Username %></b>
                        </span> <br />
                        <span class="ngaythang">
                            <%# DateTimeHelpers.GetDateAndTime(Eval("CrDateTime")) %>
                        </span> 
                    </ItemTemplate>
                    <ItemStyle Width="7%" HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="7%" />        
                </asp:TemplateField>                       
                <asp:TemplateField>
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnOperation" runat="server" Text="Thao tác" meta:resourcekey="lblGridColumnOperation"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%--<asp:LinkButton ID="lbEditREL" runat="server" CommandName="Edit" CausesValidation="false" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ToolTip="Sửa" class="iconadmsua"></asp:LinkButton>&nbsp;--%>
                        <asp:LinkButton ID="lbDelete" runat="server" CssClass="iconadmxoa" CommandName="Delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ToolTip="Xóa" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa văn bản đã chọn?')" meta:resourcekey="lnkGridColumnDel"></asp:LinkButton> 
                         <asp:Label ID="lblDocId" runat="server" Text='<%# Eval("DocId") %>' Visible="false" ></asp:Label>
                          <asp:Label ID="lblDisplayTypeId" runat="server" Text='<%# Eval("DisplayTypeId") %>' Visible="false" ></asp:Label>
                           <asp:Label ID="lblLanguageId" runat="server" Text='<%# Eval("LanguageId") %>' Visible="false"></asp:Label>
                    </ItemTemplate>
                     <EditItemTemplate>
                         <asp:LinkButton ID="cmdUpdate" runat="server" CommandName="Update" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CssClass="themmoi" >Cập nhật</asp:LinkButton>&nbsp;
                         <asp:LinkButton ID="cmdCancel" runat="server" CommandName="Cancel" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" CssClass="boduyet" >Tho&#225;t</asp:LinkButton>
                     </EditItemTemplate>
                    <ItemStyle HorizontalAlign="center" Width="6%" Wrap="false" />
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
   </div> 
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
