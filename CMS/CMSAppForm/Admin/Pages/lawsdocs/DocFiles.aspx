<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="DocFiles.aspx.cs" Inherits="Admin_Pages_lawsdocs_DocFiles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/admin/UserControls/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="uc1" %>
<%@ Import Namespace="ICSoft.CMSLib" %>
<%@ Import Namespace="ICSoft.LawDocsLib" %>
<%@ Import Namespace="ICSoft.HelperLib" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
     <style type="text/css">
     input[type=checkbox] + label {
      font-style: italic;
    } 
    input[type=checkbox]:checked + label {
      color: blue;
      font-style: normal;
} 
 </style>
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

            $('a#popup').live('click', function (e) {
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
                        try {
                            $('#<%= btnRefresh.ClientID %>')[0].click();
                        } catch (ex) {
                        }
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
                        window.location.href = window.location;
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
    function submitButton(event) {
        if (event.which == 13) {
            $('#<%= btnSearch.ClientID %>').click();
	}
}
    </script>   
     <div style="text-align:center"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
    <table cellpadding="3" cellspacing="0" style="width:100%">  
             
        <tr>
            <td style="width:120px;">
                <asp:Label ID="lblDateFrom" runat="server" meta:resourcekey="lblDateFrom" 
                    Text="Từ ngày:"></asp:Label>
            </td>
            <td style="width:260px;">
                <asp:TextBox ID="txtDateFrom" runat="server" CssClass="tukhoatimekiem" 
                    Width="100px"></asp:TextBox>
                <asp:Label ID="lblDateTo" runat="server" meta:resourcekey="lblDateTo" 
                    Text="Đến"></asp:Label>
                <asp:TextBox ID="txtDateTo" runat="server" CssClass="tukhoatimekiem" 
                    Width="100px"></asp:TextBox>
            </td>
             <td style="width:120px;">
               
                 <asp:Label ID="lblDataSources" runat="server" meta:resourcekey="lblDataSource" 
                    Text="Nguồn:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlDataSources" runat="server" 
                    CssClass="userselect" DataTextField="DataSourceDesc"  AutoPostBack="true" 
                    DataValueField="DataSourceId" 
                    onselectedindexchanged="ddlReviewStatus_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <asp:Label ID="lblSearch" runat="server" meta:resourcekey="lblSearch" 
                    Text="Doc ID:"></asp:Label>
            </td>
            <td valign="middle" align="left">                              
               <asp:TextBox ID="txtSearch" runat="server" CssClass="tukhoatimekiem" 
                    Width="240px"></asp:TextBox>
               <br />
            </td>
             <td style="width:120px;">
               
                 <asp:Label ID="lblReviewStatus" runat="server" meta:resourcekey="lblReviewStatus" 
                    Text="Trạng thái duyệt:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlReviewStatus" runat="server" 
                    CssClass="userselect" DataTextField="ReviewStatusDesc"  AutoPostBack="true" 
                    DataValueField="ReviewStatusId" 
                    onselectedindexchanged="ddlReviewStatus_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <asp:Label ID="lblDocIdentity" runat="server" 
                    Text="Số hiệu:"></asp:Label>
            </td>
            <td valign="middle" align="left">                              
               <asp:TextBox ID="txtDocIdentity" runat="server" CssClass="tukhoatimekiem" 
                    Width="240px"></asp:TextBox>
               <br />

            </td>
            
            <td valign="top">
                <asp:Label ID="lblOrderBy" runat="server" meta:resourcekey="lblOrderBy" 
                    Text="Sắp xếp:"></asp:Label>
            </td>
            <td valign="top">
                <asp:DropDownList ID="ddlOrderBy" runat="server" AutoPostBack="true" 
                    CssClass="userselect" DataTextField="OrderByDesc" DataValueField="OrderBy" 
                    onselectedindexchanged="ddlOrderBy_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
                 &nbsp;<asp:Button ID="btnSearch" runat="server" CssClass="timkiembutom" 
                    meta:resourcekey="btnSearch" onclick="btnSearch_Click" Text="Tìm kiếm">
                    </asp:Button><asp:LinkButton ID="btnRefresh" runat="server" CssClass="timkiembutom hidden-btn-dqs" Text="Làm mời" onclick="btnRefresh_Click"></asp:LinkButton>
            </td>
        </tr>
    </table>
    <div class="clear5px"></div>

 <div class="chucnangleft">
		<span class="tieudetongcong">
            <asp:Label ID="lblTotalText" runat="server" Text="Tổng cộng:" meta:resourcekey="lblTotalText"></asp:Label>
        </span>
        <asp:Label ID="lblTong" runat="server" Text="" CssClass="tieudetongcong2"></asp:Label> 
	</div><br />
    <div class="chucnangright">
         <%--<a href="DocsEdit.aspx?LanguageId=<%=LanguageId %>" title="<%=GetLocalResourceObject("lnkAddNew.title").ToString()+ "&backUrl=DocsReview.aspx" %>" class="themmoi" > 
            <asp:Label ID="lblAddNew" runat="server" Text="Thêm mới" meta:resourcekey="lblAddNew"></asp:Label>
        </a>--%>
        <asp:LinkButton ID="lbReview" runat="server" CssClass="duyettin" Text="Duyệt" 
            meta:resourcekey="lbReview" onclick="lbReview_Click"> </asp:LinkButton>					
        <asp:LinkButton ID="lbUnCheck" runat="server" CssClass="boduyet" 
            Text="Bỏ duyệt" meta:resourcekey="lbUnCheck" onclick="lbUnCheck_Click"> </asp:LinkButton> Với trạng thái: 
        <asp:DropDownList ID="ddlStatusUnCheck" runat="server" AutoPostBack="False" 
                    CssClass="userselect" DataTextField="ReviewStatusDesc" 
                    DataValueField="ReviewStatusId" Width="150px">
                </asp:DropDownList>
        <asp:LinkButton ID="lbDelete" runat="server" CssClass="xoatin" Text="Xóa" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa các văn bản đã chọn?')" 
            meta:resourcekey="lbDelete" onclick="lbDelete_Click">
        </asp:LinkButton>
	</div>
<table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td align="left">            
                     <asp:GridView ID="m_grid_File" DataKeyNames="DocFileId" runat="server" ShowHeader="true"
                        AutoGenerateColumns="False" CssClass="grid" PageSize="50" 
                        OnRowDeleting="m_grid_File_RowDeleting" CellPadding="2" GridLines="None">
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
                                  <asp:Label ID="lbNo" runat="server" Text='<%# Container.DataItemIndex + (CustomPaging.PageIndex - 1) * m_grid_File.PageSize + 1%>' 
                                     ToolTip ='<%# Eval("DocFileId").ToString() %>'> </asp:Label>
                                </ItemTemplate>      
                                <ItemStyle HorizontalAlign="Center" Width="3%" />  
                                <HeaderStyle Width="3%" />          
                            </asp:TemplateField> 
                            <asp:TemplateField>
                                <HeaderTemplate>                                
                                    Doc ID
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <a href='<%# "DocsEdit.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=0&backUrl=DocFiles.aspx"  %>' title="Nội dung" style="color:black;" > <b><%# Eval("DocId")%></b></a>                                       
                                    
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField>
                                <HeaderTemplate>                                
                                    Tên văn bản
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <a href='<%# "DocsEdit.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=0&backUrl=DocFiles.aspx"  %>' title="Nội dung" style="color:black;" > <b> <%# Docs.Static_Get( int.Parse(Eval("DocId").ToString()),0).DocName%></b></a> 
                                       
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField>
                                <HeaderTemplate>                                
                                    Số hiệu văn bản
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <a href='<%# "DocsEdit.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=0&backUrl=DocFiles.aspx"  %>' title="Nội dung" style="color:black;" > <b> <%# Docs.Static_Get( int.Parse(Eval("DocId").ToString()),0).DocIdentity%></b></a>    
                                    
                                </ItemTemplate>
                            </asp:TemplateField>  
                            <asp:TemplateField>
                                <HeaderTemplate>                                
                                    Tên file - Đường dẫn 
                                </HeaderTemplate> 
                                <ItemTemplate>                                    
                                    <%# Eval("DocFileName")%>
                                    <asp:Label ID="lblDocFileName" runat="server" Text='<%# Eval("DocFileName")%>' Visible="false"></asp:Label><br />
                                    
                                    <a href='/Download.aspx?file=<%# Eval("FilePath") %>'><%# Eval("FilePath")%></a>  
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnFileSize" runat="server" Text="Kích thước" meta:resourcekey="lblGridColumnFileSize"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate>                                    
                                    <%# int.Parse(Eval("FileSize").ToString())/1024%>Kb
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField  > 
                               <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnDataSources" runat="server" Text="Nguồn" meta:resourcekey="lblGridColumnDataSources"></asp:Label>
                                </HeaderTemplate>  
                                <ItemTemplate> 
                                   <%#  LanguageHelpers.IsCultureVietnamese() ? DataSources.Static_Get(short.Parse(Eval("DataSourceId").ToString()), l_DataSources).DataSourceDesc : DataSources.Static_Get(short.Parse(Eval("DataSourceId").ToString()), l_DataSources).DataSourceName%>
                                </ItemTemplate>
                                <ItemStyle Width="8%" HorizontalAlign = "Left" VerticalAlign="Top" Wrap="false" />  
                                <HeaderStyle Width="8%" />        
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnStatus" runat="server" Text="Trạng thái"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate>  
                                    <span class="xuatban<%# Eval("ReviewStatusId") %>" > <%# LanguageHelpers.IsCultureVietnamese() ? ReviewStatus.Static_Get(byte.Parse(Eval("ReviewStatusId").ToString()), l_ReviewStatus).ReviewStatusDesc : ReviewStatus.Static_Get(byte.Parse(Eval("ReviewStatusId").ToString()), l_ReviewStatus).ReviewStatusName%></span>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField  > 
                                <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnUserCreate" runat="server" Text="Tạo bởi" meta:resourcekey="lblGridColumnUserCreate"></asp:Label>
                                </HeaderTemplate>  
                                <ItemTemplate> 
                                    <span class="ngaythang">
                                        <font style="color:Blue"><%# UserHelpers.Static_Get(int.Parse(Eval("CrUserId").ToString()), l_Users, "").Username %></font><br />
                                        <%# DateTimeHelpers.GetDateAndTime(Eval("CrDateTime")) %>
                                    </span> 
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign = "center" Wrap="false" />  
                                <HeaderStyle Width="80px" />        
                            </asp:TemplateField> 
                            <asp:TemplateField >
                                <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnDel" runat="server" Text="Xóa" meta:resourcekey="lblGridColumnDel"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate>
                                         <asp:LinkButton ID="lbDelete" runat="server" CssClass="iconadmxoa" CommandName="Delete" ToolTip="Xóa" meta:resourcekey="lbDelete"  OnClientClick="return confirm('Bạn có thực sự muốn xóa dữ liệu này?');"></asp:LinkButton> 
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
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
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
            </td>
        </tr>
        </table>    
     <div class="clear5px"></div>    
                <uc1:CustomPaging ID="CustomPaging" runat="server" />                
                <div class="clear5px"></div>   
</asp:Content>


