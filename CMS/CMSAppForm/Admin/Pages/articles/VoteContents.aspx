<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="VoteContents.aspx.cs" Inherits="Admin_VoteContents" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/admin/UserControls/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="uc1" %>
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
                    height: 300,
                    width: 500,
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

     }
</script>
<table  class="filterTable" cellpadding="5" cellspacing="2" style="width: 100%;">                
    <tr>
        <td width="100px">
                 <asp:LinkButton ID="btnSearch" runat="server" CssClass="timkiembutom" 
                    meta:resourcekey="btnSearch" onclick="btnSearch_Click" Text="Tìm kiếm">
                        </asp:LinkButton>
                </td>
        <td class="businesses-view-2"> 
                &nbsp;</td>
        <td>
                &nbsp;&nbsp;&nbsp;
            </td>
        </tr> 
                
</table>
<table cellpadding="1" cellspacing="1" style="width: 100%;">
        <tr>
        <td>
                       
            <table cellpadding="2" cellspacing="0" width="100%">
                <tr>
                    <td  width="33%">
                    <span class="tieudetongcong">
                        <asp:Label ID="lblTotalText" runat="server" Text="Tổng cộng:" meta:resourcekey="lblTotalText"></asp:Label>
                    </span>
                    <asp:Label ID="lblTong" runat="server" Text="" CssClass="tieudetongcong2"></asp:Label> 
                    </td>  
                    <td align="center"  width="33%" >
                        <div style="position:relative;">
                                        
                        </div>
                    </td>
                    <td class="actionright" align="right">
                           <%--<asp:LinkButton ID="lbDelete" runat="server" CssClass="xoatin" Text="Xóa" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa các thuật ngữ đã chọn?')"
                                meta:resourcekey="lbDelete" onclick="lbDelete_Click">
                            </asp:LinkButton>--%>
		                    <a id="popup" href="VoteContentsEdit.aspx?VoteId=<%=VoteId %>&LanguageId=<%=LanguageId %>" title="<%=GetLocalResourceObject("lnkAddNew.title").ToString() %>" class="themmoi" > 
                                <asp:Label ID="lblAddNew" runat="server" Text="Thêm mới" meta:resourcekey="lblAddNew"></asp:Label>
                            </a>         
                    </td>
                </tr>
            </table>
                               
        </td>
    </tr>
    <tr>
        <td  id="gridSort">
            <asp:GridView ID="m_grid" DataKeyNames="VoteContentId" runat="server" ShowHeaderWhenEmpty="True"
                AutoGenerateColumns="False" CssClass="grid" PageSize="50" OnRowDeleting="m_grid_RowDeleting">
                <HeaderStyle CssClass="trbangdulieutieude" />
                <RowStyle CssClass="trbangdulieutieudenoidung" />
                <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />  
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                        <asp:Label ID="lbNo" runat="server" Text='<%# Container.DataItemIndex + 1%>'>                                    
                        </asp:Label>
                        </ItemTemplate>      
                        <ItemStyle Width="5%" HorizontalAlign="Center" />            
                    </asp:TemplateField>        
                                
                    <asp:TemplateField>  
                    <HeaderTemplate>                                
                        <asp:Label ID="lblVoteContent" runat="server" Text="Nội dung" meta:resourcekey="lblGridColumnVoteContent"></asp:Label>
                    </HeaderTemplate> 
                        <ItemTemplate> 
                            <asp:Literal ID="ltVoteContent" runat="server" EnableViewState="false" Text='<%# Eval("VoteContent") %>'></asp:Literal>
                        </ItemTemplate>
                        <ItemStyle  HorizontalAlign="Left" Width="40%" />
                    </asp:TemplateField>
                    <asp:TemplateField>  
                     <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnDisplayOrder" runat="server" Text="Thứ tự" meta:resourcekey="lblGridColumnDisplayOrder"></asp:Label>
                    </HeaderTemplate> 
                        <ItemTemplate> 
                                <div id='<%# Eval("VoteContentId").ToString() %>' class="divId" title='<%# Eval("VoteContentId").ToString() %>'>
                                <span class="ui-icon ui-icon-arrowthick-2-n-s" style="width: 20px; float: left; display: inline-block;
                                    cursor: move"></span>
                                <asp:TextBox ID="tbSortOrder" CssClass="stt" runat="server" EnableViewState="false"
                                    Text='<%# Eval("DisplayOrder").ToString() %>' Style="width: 30px; float: left;"></asp:TextBox>
                            </div>
                                        
                        </ItemTemplate> 
                        <ItemStyle  HorizontalAlign="Left" Width="8%" />
                    </asp:TemplateField>
                    <asp:TemplateField>  
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnVoteCounter" runat="server" Text="Bình chọn" meta:resourcekey="lblGridColumnVoteCounter"></asp:Label>
                    </HeaderTemplate> 
                        <ItemTemplate> 
                            <asp:Literal ID="ltVoteCounter" runat="server" EnableViewState="false" Text='<%# Eval("VoteCounter") %>'></asp:Literal> 
                        </ItemTemplate>
                        <ItemStyle  HorizontalAlign="Center" Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>                                
                            <asp:Label ID="lblGridColumnCrDateTime" runat="server" Text="Thời gian" meta:resourcekey="lblGridColumnCrDateTime"></asp:Label>
                        </HeaderTemplate> 
                        <ItemTemplate>
                            <span class="ngaythang">
                            <%# DateTimeHelpers.GetDateAndTime(Eval("CrDateTime"))%><br />
                        </span> 
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="false" Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sửa" Visible="false">
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                        <ItemTemplate>                                        
                            <a id="popup" href='VoteContentsEdit.aspx?id=<%# Eval("VoteContentId") %>' title="Sửa ">
                            <img class="IconFunction button2" src='../../admin/Images/Icons/app_edit.png' title="Sửa " />
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField>
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnOperation" runat="server" Text="Thao tác" meta:resourcekey="lblGridColumnOperation"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <a id="popup" onmouseover="ShowMenu('-1-<%# Eval("VoteContentId") %>')" href='VoteContentsEdit.aspx?VoteContentId=<%# Eval("VoteContentId") %>&VoteId=<%=VoteId %>&LanguageId=<%# Eval("LanguageId") %>' class="iconadmsua"
                        title="<%# GetLocalResourceObject("lnkGridColumnEdit.title").ToString() %>" meta:resourcekey="lnkGridColumnEdit"></a>    
                        <asp:LinkButton ID="lbDelete" runat="server" CssClass="iconadmxoa" CommandName="Delete" ToolTip="Xóa" 
                            OnClientClick="return confirm('Bạn có chắc chắn muốn xóa các thuật ngữ đã chọn?')" meta:resourcekey="lnkGridColumnDel"></asp:LinkButton> 
                        <br />
                        <a class="dropdown-menu-hover-1-<%# VoteContentId %>" href="#" data-dropdown="#dropdown-1-<%# VoteContentId %>" > 
                        </a>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="center"  Wrap="false" />
                    <HeaderStyle Width="8%" />
                </asp:TemplateField>
                    <asp:TemplateField Visible="false">
                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                        <HeaderTemplate>
                            <input type="checkbox" name="SelectAllCheckBox" onclick="SelectAll(this)">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkAction" runat="server"></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>                       
        </td>
    </tr>
</table>
</asp:Content>



