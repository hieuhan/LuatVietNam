<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="ArticleMedias.aspx.cs" Inherits="Admin_ArticleMedias" %>
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
                .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="100%" height="100%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 400,
                    width: 500,
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
    </script>
    <asp:UpdatePanel ID="upn_Grid" runat="server" UpdateMode="Conditional">
    <Triggers>
     <asp:PostBackTrigger ControlID="btnUpl" />
    </Triggers>
    <ContentTemplate>
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
        </tr>
        <tr>
            <td style="width:60px; white-space:nowrap;">
            <asp:Label ID="lblKeyword" runat="server" Text="Tên:" meta:resourcekey="lblKeyword"></asp:Label>
            
                </td>
            <td style="width:260px">
            <asp:TextBox ID="txtSearch" runat="server" CssClass="tukhoatimekiem" Width="240px"></asp:TextBox>&nbsp;&nbsp;
                </td>
            <td style="width:90px; white-space:nowrap;">
                File:
            </td>
            <td><asp:FileUpload ID="fUpl" runat="server" multiple />
                        <asp:LinkButton ID="btnSearch" runat="server" CssClass="timkiembutom" 
                    Text="Tìm kiếm" meta:resourcekey="btnSearch" onclick="btnSearch_Click">
                        </asp:LinkButton>&nbsp;&nbsp;&nbsp;
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
        <asp:LinkButton ID="lbDelete" runat="server" CssClass="xoatin" Text="Xóa" 
            meta:resourcekey="lbDelete" onclick="lbDelete_Click">
        </asp:LinkButton>        
	</div>
    <div style="text-align:left; width:200px; float:right"><asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                            <ProgressTemplate><img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...</ProgressTemplate>
                            </asp:UpdateProgress></div>
	<div class="clear5px"></div>
    <div>
        <table width="100%" border="0">
            <tr>
                <td valign="top">
                    <div class="contenbangdulieu" style=" width:auto; height:380px; overflow:auto;">
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
                                    <b><%# Eval("MediaName")%></b>
                                </ItemTemplate>
                                <ItemStyle  HorizontalAlign="Left" />
                                <HeaderStyle />          
                            </asp:TemplateField>
                             <asp:TemplateField > 
                                    <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnName" runat="server" Text="File"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <%# Eval("FilePath").ToString() == "" ? "" : Eval("MediaTypeId").ToString() == "1" ? "<a class=\"popup\" href=\"" + (Eval("FilePath").ToString().StartsWith("http://") ? "" : CmsConstants.WEBSITE_MEDIADOMAIN) + Eval("FilePath").ToString() + "\" /><img style=\"width:40px;height:40px\" src=\"" + (Eval("FilePath").ToString().StartsWith("http://") ? "" : CmsConstants.WEBSITE_MEDIADOMAIN) + Eval("FilePath").ToString().Replace("Original", "Icon") + "\" /></a>" : "<a class=\"popup\" href=\"" + (Eval("FilePath").ToString().StartsWith("http://") ? "" : CmsConstants.WEBSITE_MEDIADOMAIN) + Eval("FilePath").ToString() + "\" />View</a>"%>
                                </ItemTemplate>
                                <ItemStyle  HorizontalAlign="Center" Width="50px" />
                                <HeaderStyle />          
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnOperation" runat="server" Text="Thao tác" meta:resourcekey="lblGridColumnOperation"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <asp:LinkButton ID="lbDelete" runat="server" CssClass="themmoi" Text="Chọn" CommandName="Delete" ToolTip="" meta:resourcekey="lnkGridColumnDel"></asp:LinkButton> 
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Width="68px" Wrap="false" />
                                <HeaderStyle Width="68px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    </div>
                    <div class="clear5px"></div>    
                    <uc1:CustomPaging ID="CustomPaging" runat="server" />  
                </td>
                <td valign="top" width="50%">
                    <div class="contenbangdulieu" style=" width:auto; height:380px; overflow:auto;">
                    <asp:GridView ID="m_gridRelate" DataKeyNames="MediaId" runat="server" ShowHeaderWhenEmpty="True" PageSize="50"
                        AutoGenerateColumns="False" CssClass="filter-table" OnRowDeleting="m_gridRelate_RowDeleting" OnRowDataBound = "m_gridRelate_OnRowDataBound"
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
                                    <b><%# Eval("MediaName")%></b>
                                </ItemTemplate>
                                <ItemStyle  HorizontalAlign="Left" />
                                <HeaderStyle />          
                            </asp:TemplateField>
                             <asp:TemplateField > 
                                    <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnName" runat="server" Text="File"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <%# Eval("FilePath").ToString() == "" ? "" : Eval("MediaTypeId").ToString() == "1" ? "<a class=\"popup\" href=\"" + (Eval("FilePath").ToString().StartsWith("http://") ? "" : CmsConstants.WEBSITE_MEDIADOMAIN) + Eval("FilePath").ToString() + "\" /><img style=\"width:40px;height:40px\" src=\"" + (Eval("FilePath").ToString().StartsWith("http://") ? "" : CmsConstants.WEBSITE_MEDIADOMAIN) + Eval("FilePath").ToString().Replace("Original", "Icon") + "\" /></a>" : "<a class=\"popup\" href=\"" + (Eval("FilePath").ToString().StartsWith("http://") ? "" : CmsConstants.WEBSITE_MEDIADOMAIN) + Eval("FilePath").ToString() + "\" />View</a>"%>
                                </ItemTemplate>
                                <ItemStyle  HorizontalAlign="Center" Width="50px" />
                                <HeaderStyle />          
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnOperation" runat="server" Text="Thao tác" meta:resourcekey="lblGridColumnOperation"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <asp:LinkButton ID="lbDelete" runat="server" CssClass="iconadmxoa" CommandName="Delete" ToolTip="Xóa" meta:resourcekey="lnkGridColumnDelRelate"></asp:LinkButton> 
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Width="68px" Wrap="false" />
                                <HeaderStyle Width="68px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                                <HeaderStyle Width="40px" />
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
                </td>
            </tr>
        </table>
        
    </div>
                              
   </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
