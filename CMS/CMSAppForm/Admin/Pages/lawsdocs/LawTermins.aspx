﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="LawTermins.aspx.cs" Inherits="Admin_LawTermins" %>
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
                    height: 350,
                    width: 450,
                    title: $(this).attr("title"),
                    close: function (event, ui) {
                        $(this).remove();
                        //window.location = $('#<%= btnSearch.ClientID %>').attr("href");
                        $('#<%= btnSearch.ClientID %>').click();
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
    <div style="text-align:center"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
    <table cellpadding="4" cellspacing="0" style="width:100%">
        <tr>
            <td style="width:80px; white-space:nowrap;">
                <asp:Label ID="lblLanguage" runat="server" meta:resourcekey="lblLanguage" 
                    Text="Ngôn ngữ:"></asp:Label>
                </td>
            <td style="width:260px">
                <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="LanguageDesc" DataValueField="LanguageId" 
                    onselectedindexchanged="ddlLanguage_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
            <td style="width:90px; white-space:nowrap;">
                <asp:Label ID="lblReviewStatus" runat="server" 
                    meta:resourcekey="lblReviewStatus" Text="Trạng thái:"></asp:Label>
                </td>
            <td style="width: 260px;">
                <asp:DropDownList ID="ddlReviewStatus" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="ReviewStatusDesc" 
                    DataValueField="ReviewStatusId" 
                    onselectedindexchanged="ddlReviewStatus_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
         <td style="width:90px; white-space:nowrap;">
                <asp:Label ID="lblLawTerminGroup" runat="server" 
                    meta:resourcekey="lblLawTerminGroup" Text="Nhóm:"></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlLawTerminGroupID" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="LawTerminGroupDesc"  DataValueField="LawTerminGroupId" 
                    onselectedindexchanged="ddlLawTerminGroupID_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        
        <tr>
            <td style="width: 80px; white-space: nowrap;">
                <asp:Label ID="lblDateFrom" runat="server" meta:resourcekey="lblDateFrom" 
                    Text="Từ ngày:"></asp:Label>
            </td>
            <td style="width: 260px">
                <asp:TextBox ID="txtDateFrom"  CssClass="tukhoatimekiem" runat="server" Width="240px"></asp:TextBox>
            </td>
            <td style="width: 90px; white-space: nowrap;">
                <asp:Label ID="lblDateTo" runat="server" meta:resourcekey="lblDateTo" 
                    Text="Đến ngày:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDateTo"  CssClass="tukhoatimekiem" runat="server" Width="240px"></asp:TextBox>
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
                <asp:Label ID="lblKeyword" runat="server" meta:resourcekey="lblKeyword" 
                    Text="Từ khóa:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtSearch" runat="server" 
                    CssClass="tukhoatimekiem input-filter"></asp:TextBox>
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
        <%=GetLocalResourceObject("LawTermins").ToString()%>
	</div>
	<div class="chucnangright">
		<a id="popup" href="LawTerminsEdit.aspx" title="<%=GetLocalResourceObject("lnkAddNew.title").ToString() %>" class="themmoi" > 
            <asp:Label ID="lblAddNew" runat="server" Text="Thêm mới" meta:resourcekey="lblAddNew"></asp:Label>
        </a>
        <asp:LinkButton ID="lbDelete" runat="server" CssClass="xoatin" Text="Xóa" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa các thuật ngữ đã chọn?')"
            meta:resourcekey="lbDelete" onclick="lbDelete_Click">
        </asp:LinkButton>
	</div>
    <%--<div style="text-align:left; width:30%; float:right"><asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                            <ProgressTemplate><img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...</ProgressTemplate>
                            </asp:UpdateProgress></div>--%>
	<div class="clear5px"></div>
    <div class="contenbangdulieu">
    
        <asp:GridView ID="m_grid" DataKeyNames="LawTerminId" runat="server" ShowHeaderWhenEmpty="True"
            AutoGenerateColumns="False" CssClass="jquery-filter-table" 
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
                                     ToolTip ='<%# Eval("LawTerminId").ToString() %>'> </asp:Label>
                    </ItemTemplate>      
                    <ItemStyle HorizontalAlign="Center" Width="3%" />  
                    <HeaderStyle Width="3%" />          
                </asp:TemplateField>        
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnName" runat="server" Text="Tên" meta:resourcekey="lblGridColumnName"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("TermName")%> 
                        <asp:Label ID="lblTermName" runat="server" Text='<%# Eval("TermName")%>' Visible="false"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                    <HeaderStyle/>          
                </asp:TemplateField> 
                <asp:TemplateField > 
                     <HeaderTemplate>
                            <asp:Label ID="lblGridColumnDesc" runat="server" Text="Mô tả" meta:resourcekey="lblGridColumnDesc"></asp:Label>
                         
                         
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <div style=" width:100%; max-height: 80px; overflow: auto">  
                        <%# Eval("TermDesc")%> 
                        </div>
                        <asp:Label ID="lblLanguageId" runat="server" Text='<%# Eval("LanguageId")%>' Visible="false"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" Width="30%" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnTermSource" runat="server" Text="Nguồn thuật ngữ" meta:resourcekey="lblGridColumnTermSource"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("TermSource")%> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                    <HeaderStyle/>          
                </asp:TemplateField> 
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnStatus" runat="server" Text="Trạng thái" meta:resourcekey="lblGridColumnStatus"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <span class="xuatban<%# Eval("ReviewStatusId") %>" >
                            <%# LanguageHelpers.IsCultureVietnamese() ? ReviewStatus.Static_Get(byte.Parse(Eval("ReviewStatusId").ToString()), l_ReviewStatus).ReviewStatusDesc : ReviewStatus.Static_Get(byte.Parse(Eval("ReviewStatusId").ToString()), l_ReviewStatus).ReviewStatusName %>
                        </span> 
                    </ItemTemplate>
                    <ItemStyle Width="8%" HorizontalAlign = "Center" Wrap="false" />  
                    <HeaderStyle Width="8%" />        
                </asp:TemplateField> 
                  <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnTime" runat="server" Text="Thời gian" meta:resourcekey="lblGridColumnTime"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                            <%# DateTimeHelpers.GetDateAndTime(Eval("CrDateTime"))%>
                    </ItemTemplate>
                    <ItemStyle Width="8%" HorizontalAlign = "center" Wrap="false" />  
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
                        <a id="popup" onmouseover="ShowMenu('-1-<%# Eval("LawTerminId") %>')" href='LawTerminsEdit.aspx?LawTerminId=<%# Eval("LawTerminId") %>' class="iconadmsua"
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
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>