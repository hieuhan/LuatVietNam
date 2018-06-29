<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="MediasDb.aspx.cs" Inherits="Admin_ArticleMediasDb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/admin/UserControls/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="uc1" %>
<%@ Import Namespace="ICSoft.CMSLib" %>
<%@ Import Namespace="ICSoft.HelperLib" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <script type="text/javascript">
        var cdialog = $('<div id="divEdit"></div>');
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);

            $('a.popup').live('click', function (e) {
                var page = $(this).attr("href");
                cdialog
                .html('<iframe id="divEdit" style="border: 0px; " src="' + page + '" width="100%" height="100%"></iframe>')
                .dialog({
                    autoOpen: false, 
                    modal: true,
                    height: 460,
                    width: 600,
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

            $('a.popupImg').live('click', function (e) {
                var page = $(this).attr("href");
                cdialog
                .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="100%" height="100%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 460,
                    width: 600,
                    title: $(this).attr("title"),
                    close: function (event, ui) {
                        $(this).remove();
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
        function submitButton(event) {
            if (event.which == 13) {
                $('#<%= btnSearch.ClientID %>').click();
            }
        }
    </script>
    <div style="text-align:center"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
    <table cellpadding="2" cellspacing="0" style="width:100%">
        <tr>
            <td style="width:60px; white-space:nowrap;"><asp:Label ID="lblMediaGroup" runat="server" Text="Nhóm:" meta:resourcekey="lblMediaGroup"></asp:Label>
            </td>
            <td style="width:260px">
                <asp:DropDownList ID="ddlMediaGroup" runat="server" DataTextField="MediaGroupDesc" 
                    DataValueField="MediaGroupId" Width="250px" CssClass="userselect"
                    AutoPostBack="True" onselectedindexchanged="ddlMediaGroup_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="width:90px; white-space:nowrap;">    
                <asp:Label ID="lblMediaType" runat="server" Text="Loại:" meta:resourcekey="lblMediaType"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlMediaType" runat="server" 
                    DataTextField="MediaTypeDesc" DataValueField="MediaTypeId" 
                    Width="250px" CssClass="userselect" AutoPostBack="True" 
                        onselectedindexchanged="ddlMediaType_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td></td>
        </tr>
        <tr>
            <td style="width:60px; white-space:nowrap;">
            <asp:Label ID="lblKeyword" runat="server" Text="Tên:" meta:resourcekey="lblKeyword"></asp:Label>
            
                </td>
            <td style="width:260px">
            <asp:TextBox ID="txtSearch" runat="server" CssClass="tukhoatimekiem" Width="240px"></asp:TextBox>&nbsp;&nbsp;
                </td>
            <td style="width:90px; white-space:nowrap;">
                
            </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" CssClass="timkiembutom" 
                    Text="Tìm kiếm" meta:resourcekey="btnSearch" onclick="btnSearch_Click">
                        </asp:Button>
                <asp:Button ID="btnRefresh" runat="server" CssClass="timkiembutom hidden-btn-dqs" 
                    Text="Làm mời" onclick="btnRefresh_Click">
                        </asp:Button>
            </td>
            <td style="text-align: right;">
                File:
                <asp:FileUpload ID="fUpl" runat="server" multiple />
                &nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="btnUpl" 
                    runat="server" CssClass="themmoi" 
                    Text="Upload" onclick="btnUpl_Click" ></asp:LinkButton>
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
        <a href="MediasEdit.aspx" title="Thêm mới" class="themmoi popup" > 
            <asp:Label ID="lblAddNew" runat="server" Text="Thêm mới" meta:resourcekey="lblAddNew"></asp:Label>
        </a> 
        <asp:LinkButton ID="lbDelete" runat="server" CssClass="xoatin" Text="Xóa" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa tất cả các media đã chọn?')"
            meta:resourcekey="lbDelete" onclick="lbDelete_Click">
        </asp:LinkButton>        
	</div>
    <div style="text-align:left; width:200px; float:right"></div>
	<div class="clear5px"></div>
    <div class="contenbangdulieu">
        <asp:GridView ID="m_grid" DataKeyNames="MediaId" runat="server" ShowHeaderWhenEmpty="True" PageSize="50"
            AutoGenerateColumns="False" CssClass="filter-table" OnRowDeleting="m_grid_RowDeleting" OnRowDataBound = "m_grid_OnRowDataBound"
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
                    <asp:Label ID="lbNo" runat="server" Text='<%# Container.DataItemIndex + (CustomPaging.PageIndex - 1) * m_grid.PageSize + 1 %>'>                                    
                    </asp:Label>
                    </ItemTemplate>      
                    <ItemStyle HorizontalAlign="Center" Width="3%" />  
                    <HeaderStyle Width="3%" />          
                </asp:TemplateField>        
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnName" runat="server" Text="Tên" meta:resourcekey="lblGridColumnName"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("MediaName") %>
                        <asp:Label ID="lblMediaName" runat="server" Text='<%# Eval("MediaName")%>' Visible="false"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
                </asp:TemplateField>
                    <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnName" runat="server" Text="File"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("FilePath").ToString() == "" ? "" : FileUploadHelpers.IsImageFile(Eval("FilePath").ToString()) ? "<a class=\"popupImg\" href=\"" + (Eval("FilePath").ToString().StartsWith("http://") ? "" : CmsConstants.WEBSITE_MEDIADOMAIN) + Eval("FilePath").ToString() + "\" /><img style=\"max-width:40px;max-height:40px\" src=\"" + (Eval("FilePath").ToString().StartsWith("http://") ? "" : CmsConstants.WEBSITE_MEDIADOMAIN) + Eval("FilePath").ToString().Replace("Original", "Icon") + "\" /></a>" : "<a class=\"popupImg\" href=\"" + (Eval("FilePath").ToString().StartsWith("http://") ? "" : CmsConstants.WEBSITE_MEDIADOMAIN) + Eval("FilePath").ToString() + "\" />View</a>"%>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Center" Width="50px" />
                    <HeaderStyle />          
                </asp:TemplateField>
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnTime" runat="server" Text="Thời gian" meta:resourcekey="lblGridColumnTime"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <span class="ngaythang">
                            <%# DateTimeHelpers.GetDateAndTime(Eval("CrDateTime"))%><br />
                        </span> 
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="10%" />        
                </asp:TemplateField>
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnUser" runat="server" Text="Tạo bởi" meta:resourcekey="lblGridColumnUser"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <span class="ngaythang">
                            <%# UserHelpers.Static_Get(int.Parse(Eval("CrUserId").ToString()), l_Users, "").Username %>
                        </span> 
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="10%" />        
                </asp:TemplateField>                       
                <asp:TemplateField>
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnOperation" runat="server" Text="Thao tác" meta:resourcekey="lblGridColumnOperation"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <a href='MediasEdit.aspx?MediaId=<%# Eval("MediaId") %>' class="iconadmsua popup"
                        title="Sửa thông tin" meta:resourcekey="lnkGridColumnEdit"></a>    
                        <asp:LinkButton ID="lbDelete" runat="server" CssClass="iconadmxoa" CommandName="Delete" ToolTip="Xóa" meta:resourcekey="lnkGridColumnDel" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa media này')"></asp:LinkButton> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="center"  Wrap="false" />
                    <HeaderStyle Width="8%" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <HeaderStyle Width="5%" />
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
   </div>
</asp:Content>
