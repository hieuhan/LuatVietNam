<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="RelateTypes.aspx.cs" Inherits="Admin_RelateTypes" %>
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

            $('a#popup').live('click', function (e) {
                var page = $(this).attr("href")
                var cdialog = $('<div id="divEdit"></div>')
                .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="98%" height="98%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 500,
                    width: 550,
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
    function submitButton(event) {
        if (event.which == 13) {
            $('#<%= btnSearch.ClientID %>').click();
        }
    }
    </script>
    <%--<asp:UpdatePanel ID="upn_Grid" runat="server">
    <ContentTemplate>--%>
    <div style="text-align:center"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
    <table cellpadding="4" cellspacing="0" style="width:100%">
        <tr>
            <td width="90px">
                <asp:Label ID="lblDocGroups" runat="server" meta:resourcekey="lblDocGroups" 
                    Text="Nhóm văn bản:"></asp:Label>
            </td>
            <td width="250px">
                <asp:DropDownList ID="ddlDocGroups" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="DocGroupDesc" DataValueField="DocGroupId" 
                    onselectedindexchanged="ddlOrderBy_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
                
            </td>
            <td width="90px">
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
           <td width="90px">
                <asp:Label ID="lblLanguage" runat="server" meta:resourcekey="lblLanguage" 
                    Text="Ngôn ngữ:"></asp:Label>
            </td>
            <td width="250px">
                <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="LanguageDesc" DataValueField="LanguageId" 
                    onselectedindexchanged="ddlLanguage_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
                
            </td>
            <td style="text-align:left;" >
                <asp:CheckBox ID="chkRelateTypeWithDefaultLanguage" runat="server" 
                    AutoPostBack="True" meta:resourcekey="chkRelateTypeWithDefaultLanguage" 
                    oncheckedchanged="chkRelateTypeWithDefaultLanguage_CheckedChanged" 
                    Text="Hiển thị dữ liệu Tiếng Việt" />
            </td>
             
            <td>
                <asp:Button ID="btnSearch" runat="server" CssClass="timkiembutom" 
                    meta:resourcekey="btnSearch" onclick="btnSearch_Click" Text="Tìm kiếm">
                    </asp:Button>
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
        <%=GetLocalResourceObject("RelateTypes").ToString()%>
	</div>
	<div class="chucnangright">
		<a id="popup" href="RelateTypesEdit.aspx?LanguageId=<%=LanguageId %>" title="<%=GetLocalResourceObject("lnkAddNew.title").ToString() %>" class="themmoi" > 
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
    
        <asp:GridView ID="m_grid" DataKeyNames="RelateTypeId" runat="server" ShowHeaderWhenEmpty="True"
            AutoGenerateColumns="False" OnRowDeleting="m_grid_RowDeleting" OnRowDataBound = "m_grid_OnRowDataBound"
            Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" PageSize="100" >
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
                    <asp:Label ID="lbNo" runat="server" Text='<%# Container.DataItemIndex + 1%>'>                                    
                    </asp:Label>
                    </ItemTemplate>      
                    <ItemStyle HorizontalAlign="Center" Width="3%" />  
                    <HeaderStyle Width="3%" />          
                </asp:TemplateField>        
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnName" runat="server" Text="Tên liên kết" meta:resourcekey="lblGridColumnName"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lbRelateTypeName" runat="server" style="display:none"></asp:Label>                        
                        <%# Eval("RelateTypeName") != "" ? Eval("RelateTypeName") : "Chưa có dữ liệu:" + ddlLanguage.SelectedItem.Text.ToString()%> 
                         <asp:Label ID="lblRelateTypeName" runat="server" Text='<%# Eval("RelateTypeName")%>' Visible="false"></asp:Label>
                        <br />                       
                          <span style="color: #999999;" >
                           <asp:Label ID="lblDefaultLanguage" runat="server" Text="Tiếng việt:" meta:resourcekey="lblDefaultLanguage" Visible="<%# chkRelateTypeWithDefaultLanguage.Checked==true ? true : false %>" ></asp:Label> <%# (ddlLanguage.SelectedValue == "2") ? (chkRelateTypeWithDefaultLanguage.Checked == true ? Eval("RelateTypeNameDefault") : "") : ""%>
                        </span> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnDesc" runat="server" Text="Mô tả" meta:resourcekey="lblGridColumnDesc"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lbRelateTypeDesc" runat="server" style="display:none"></asp:Label>                        
                        <%# Eval("RelateTypeDesc")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left"/>
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnRevertRelateTypeName" runat="server" Text="Tên liên kết ngược" meta:resourcekey="lblGridColumnRevertRelateTypeName"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lbRevertRelateTypeName" runat="server" style="display:none"></asp:Label>                        
                        <%# Eval("RevertRelateTypeName")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left"/>
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnRevertRelateTypeDesc" runat="server" Text="Mô tả" meta:resourcekey="lblGridColumnRevertRelateTypeDesc"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lbRevertRelateTypeDesc" runat="server" style="display:none"></asp:Label>                        
                        <%# Eval("RevertRelateTypeDesc")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left"/>
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnRelateTypeGroup" runat="server" Text="Loại liên quan" meta:resourcekey="lblGridColumnRelateTypeGroup"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# LanguageHelpers.IsCultureVietnamese() ? RelateTypeGroups.Static_Get(byte.Parse(Eval("RelateTypeGroupId").ToString()), l_RelateTypeGroups).RelateTypeGroupDesc : RelateTypeGroups.Static_Get(byte.Parse(Eval("RelateTypeGroupId").ToString()), l_RelateTypeGroups).RelateTypeGroupName%>                       
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
               </asp:TemplateField> 
                <asp:TemplateField > 
                    <HeaderTemplate>                                
                        Nhóm văn bản
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# DocGroups.Static_GetDisplayString(byte.Parse(Eval("DocGroupId").ToString()))%>                       
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
               </asp:TemplateField> 
               <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnDisplayOrder" runat="server" Text="Thứ tự" meta:resourcekey="lblGridColumnDisplayOrder"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                       <%# Eval("DisplayOrder")%>
                    </ItemTemplate>
                    <ItemStyle Width="5%" HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="5%" />        
                </asp:TemplateField>         
                 <asp:TemplateField>
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnOperation" runat="server" Text="Thao tác" meta:resourcekey="lblGridColumnOperation"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <a id="popup" onmouseover="ShowMenu('-1-<%# Eval("RelateTypeId") %>')" href='RelateTypesEdit.aspx?RelateTypeId=<%# Eval("RelateTypeId") %>&LanguageId=<%# Eval("LanguageId") %>' class="iconadmsua"
                        title="<%# GetLocalResourceObject("lnkGridColumnEdit.title").ToString() %>" meta:resourcekey="lnkGridColumnEdit"></a>    
                        <asp:LinkButton ID="lbDelete" runat="server" CssClass="iconadmxoa" CommandName="Delete" ToolTip="Xóa" meta:resourcekey="lnkGridColumnDel"></asp:LinkButton> 
                        <asp:Label ID="lblLanguageId" runat="server" Text='<%# Eval("LanguageId") %>' Visible="false"></asp:Label>
                        <asp:Repeater ID="rptLanguage" runat="server">
                            <HeaderTemplate>
                            <div  class="dropdown dropdown-tip dropdown-anchor-right" id="dropdown-1-<%# RelateTypeId %>" style="display: none; text-align:left;" >
		                        <ul class="dropdown-menu">	
                            </HeaderTemplate>
                            <ItemTemplate>
                                <li>
                                    <a id="popup" href="RelateTypesEdit.aspx?RelateTypeId=<%# RelateTypeId %>&LanguageId=<%# Eval("LanguageId") %>" 
                                    title="<%# GetLocalResourceObject("lnkGridColumnEdit.title").ToString() %>" >
                                        <img src="../../<%# Eval("IconPath").ToString() %>" title="" alt="" /> 
                                            <%# GetLocalResourceObject("EditLanguage").ToString() + Eval("LanguageDesc").ToString() %> 
                                    </a>
                                </li>
                            </ItemTemplate>
                            <FooterTemplate>
                                    </ul>
	                        </div>        
                            </FooterTemplate>
                        </asp:Repeater>
                        <br />
                        <a class="dropdown-menu-hover-1-<%# Eval("RelateTypeId") %>" href="#" data-dropdown="#dropdown-1-<%# Eval("RelateTypeId") %>" > 
                        </a>
                    </ItemTemplate>
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
