<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="DocIndexes.aspx.cs" Inherits="Admin_DocIndexes" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/admin/UserControls/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="uc1" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Import Namespace="ICSoft.CMSLib" %>
<%@ Import Namespace="ICSoft.LawDocsLib" %>
<%@ Import Namespace="ICSoft.HelperLib" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);

            $('.popup').live('click', function (e) {
                var page = $(this).attr("href")
                var cdialog = $('<div id="divEdit2"></div>')
                .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="98%" height="98%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 200,
                    width: 360,
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
    <div style="text-align:center">
        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label>
        <asp:Label ID="lblDocName" runat="server" ForeColor="Blue" Font-Size="18px" Text=""></asp:Label>
    </div>
    <table cellpadding="4" cellspacing="0" style="width:100%">
        <tr runat="server" visible="false">
            <td style="width:80px; white-space:nowrap;">
                <asp:Label ID="lblLanguage" runat="server" Text="Ngôn ngữ:" meta:resourcekey="lblLanguage"></asp:Label>
            </td>
            <td style="width:250px">
                <asp:DropDownList ID="ddlLanguage" runat="server" DataTextField="LanguageDesc" 
                    DataValueField="LanguageId" Width="250px" CssClass="userselect" 
                    AutoPostBack="True" onselectedindexchanged="ddlLanguage_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="width:90px; white-space:nowrap;">
                <asp:Label ID="lblOrderBy" runat="server" meta:resourcekey="lblOrderBy" 
                    Text="Sắp xếp:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlOrderBy" runat="server" AutoPostBack="True" 
                    CssClass="userselect" onselectedindexchanged="ddlOrderBy_SelectedIndexChanged" Width="250px">
                    <asp:ListItem Value="DisplayOrder asc">Theo thứ tự hiển thị</asp:ListItem>
                    <asp:ListItem Value="CrDateTime desc">Mới thêm</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr runat="server" visible="false">
            <td>
                <asp:Label ID="lblSearch" runat="server" meta:resourcekey="lblSearch" 
                    Text="Từ khóa:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtSearch" runat="server" 
                    CssClass="tukhoatimekiem input-filter"></asp:TextBox>
            </td>
            <td>
                <asp:LinkButton ID="btnSearch" runat="server" CssClass="timkiembutom" 
                    meta:resourcekey="btnSearch" onclick="btnSearch_Click" Text="Tìm kiếm">
                    </asp:LinkButton>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <table width="100%" cellpadding="3" cellspacing="3">
        <tr>
            <td style="width:25%; vertical-align:top;" >
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
	            <div class="chucnangright" style="display:none;">
		            <a id="popup" href="DocIndexEdit.aspx?DocId=<%=DocId %>&LanguageId=<%=LanguageId %>" title="Add new" class="themmoi popup" > 
                        <asp:Label ID="lblAddNew" runat="server" Text="Thêm mới" meta:resourcekey="lblAddNew"></asp:Label>
                    </a>
                    <asp:LinkButton ID="lbDelete" runat="server" CssClass="xoatin" Text="Xóa" 
                        meta:resourcekey="lbDelete" onclick="lbDelete_Click">
                    </asp:LinkButton>
	            </div>
                <div style="text-align:left; width:50%; float:right"></div>
	            <div class="clear5px"></div>
                <div class="contenbangdulieu">
    
                    <asp:GridView ID="m_grid" DataKeyNames="DocIndexId" runat="server" ShowHeaderWhenEmpty="True"
                        AutoGenerateColumns="False" CssClass="" ShowHeader="false"
                        OnRowDeleting="m_grid_RowDeleting" OnRowDataBound = "m_grid_OnRowDataBound"
                        Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" 
                        PageSize="50" >
                        <HeaderStyle CssClass="trbangdulieutieude" />
                        <RowStyle CssClass="trbangdulieutieudenoidung" />
                        <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                        <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />                                        
                        <Columns>                        
                            <asp:TemplateField Visible="false" >
                                <HeaderTemplate>                                
                                #
                                </HeaderTemplate>
                                <ItemTemplate>
                                <asp:Label ID="lbNo" runat="server" Text='<%# Container.DataItemIndex + (CustomPaging.PageIndex - 1) * m_grid.PageSize + 1%>' 
                                                 ToolTip ='<%# Eval("DocIndexId").ToString() %>'> </asp:Label>
                                </ItemTemplate>      
                                <ItemStyle HorizontalAlign="Center" Width="3%" />  
                                <HeaderStyle Width="3%" />          
                            </asp:TemplateField>        
                            <asp:TemplateField > 
                                    <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnName" runat="server" Text="Title"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <a href="#<%# Eval("Bookmark") %>" class="danhsachdemuc<%# Eval("LevelId") %>" ><%#  Eval("Title") %></a>
                                </ItemTemplate>
                                <ItemStyle  HorizontalAlign="Left"/>
                                <HeaderStyle />          
                            </asp:TemplateField>     
                        </Columns>
                    </asp:GridView>
                </div>
               </div>
                <div class="clear5px"></div>    
                <uc1:CustomPaging ID="CustomPaging" runat="server" Visible="false" />                
                <div class="clear5px"></div>  

            </td>
            <td style="vertical-align:top;">
                <CKEditor:CKEditorControl ID="txtDocContent" runat="server" Toolbar="DocIndex" 
                                BasePath="~/Integrated/ckeditor/" Height="380px"  Width="98%"></CKEditor:CKEditorControl>
                <br />
                <div style="text-align:center">
                    <asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom" 
                                Text="Lưu thông tin" ValidationGroup="G1" meta:resourcekey="btnSave" ToolTip="Lưu thông tin"
                        onclick="btnSave_Click"> </asp:LinkButton>
                    <asp:LinkButton ID="btnBack" runat="server" CssClass="quaylai" 
                                Text="Quay lại" meta:resourcekey="btnBack" 
                        onclick="btnBack_Click" >
                    </asp:LinkButton>
                </div>
                <div id="divDemo" runat="server" style="padding-top:50px;"></div>
            </td>
        </tr>
    </table>
</asp:Content>
