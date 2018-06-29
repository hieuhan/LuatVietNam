<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="AdvertPositionAdverts.aspx.cs" Inherits="Admin_AdvertPositionAdverts" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/admin/UserControls/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="uc1" %>
<%@ Import Namespace="ICSoft.CMSLib" %>
<%@ Import Namespace="ICSoft.HelperLib" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);

            $('a.popup').live('click', function (e) {
                var page = $(this).attr("href")
                var cdialog = $('<div id="divEdit1"></div>')
                .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="98%" height="98%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 450,
                    width: 620,
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

            $('a.popup2').live('click', function (e) {
                var page = $(this).attr("href")
                var cdialog = $('<div id="divEdit1"></div>')
                .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="98%" height="98%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 400,
                    width: 580,
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
    <asp:UpdatePanel ID="upn_Grid" runat="server">
    <ContentTemplate>
    <div style="text-align:center"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
    <asp:LinkButton ID="btnSearch" runat="server" Width="1px" Height="1px"
                    Text="" onclick="btnSearch_Click">
                        </asp:LinkButton>
    <table cellpadding="2" cellspacing="0" style="width:100%">
        <tr>
            <td style="width:80px"><asp:Label ID="lblPosition" runat="server" Text="Vị trí QC: " meta:resourcekey="lblPosition"></asp:Label>
                </td>
            <td>
                <asp:Label ID="lblPositionName" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width:80px"><asp:Label ID="lblSite" runat="server" Text="Site:" meta:resourcekey="lblSite"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlSite" runat="server" DataTextField="SiteDesc" Enabled="false"
                    DataValueField="SiteId" Width="250px" CssClass="userselect"
                    AutoPostBack="True" onselectedindexchanged="ddlSite_SelectedIndexChanged">
                </asp:DropDownList>&nbsp;&nbsp;
                <asp:Label ID="lblDataType" runat="server" Text="Loại dữ liệu:" meta:resourcekey="lblDataType"></asp:Label>
                &nbsp;&nbsp;
                <asp:DropDownList ID="ddlDataType" runat="server" 
                    DataTextField="DataTypeDesc" DataValueField="DataTypeId" 
                    Width="180px" CssClass="userselect" AutoPostBack="True" 
                        onselectedindexchanged="ddlDataType_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width:80px; white-space:nowrap;">
                <asp:Label ID="lblCategory" runat="server" Text="Chuyên mục:" meta:resourcekey="lblCategory"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlCategory" runat="server" DataTextField="CategoryDesc" 
                    DataValueField="CategoryId" Width="250px" CssClass="userselect" 
                    AutoPostBack="True" onselectedindexchanged="ddlCategory_SelectedIndexChanged">
                </asp:DropDownList>
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
        <asp:HyperLink ID="btnAdd" runat="server" CssClass="themmoi popup" title="Chọn quảng cáo">Chọn quảng cáo</asp:HyperLink>
        <asp:LinkButton ID="btnSavePosition" runat="server" CssClass="savebutom"
                    Text="Lưu vị trí" onclick="btnSavePosition_Click" meta:resourcekey="btnSavePosition">
                        </asp:LinkButton>
	</div>
    <div style="text-align:left; width:200px; float:right"><asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                            <ProgressTemplate><img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...</ProgressTemplate>
                            </asp:UpdateProgress></div>
	<div class="clear5px"></div>
    <div class="contenbangdulieu">
    
        <asp:GridView ID="m_grid" DataKeyNames="AdvertId" runat="server" ShowHeaderWhenEmpty="True"
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
                        <asp:Label ID="lbNo" runat="server" Text='<%# Container.DataItemIndex + 1%>' Visible="false"></asp:Label>                                 
                        <asp:TextBox ID="txtDisplayOrder" runat="server" Text='<%# Container.DataItemIndex + 1%>' style="text-align: center; width: 25px;"></asp:TextBox>
                    </ItemTemplate>      
                    <ItemStyle HorizontalAlign="Center" Width="5%" />  
                    <HeaderStyle Width="5%" />          
                </asp:TemplateField>        
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnName" runat="server" Text="Quảng cáo"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("AdvertName") %>
                        <asp:Label ID="lblAdvertName" runat="server" Visible="false" Text='<%# Eval("AdvertName") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                 <asp:TemplateField > 
                        <HeaderTemplate>                                
                        Chuyên mục
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("CategoryName") %><asp:Label ID="lblCategoryId" runat="server" Visible="false" Text='<%# Eval("CategoryId") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                 <asp:TemplateField > 
                        <HeaderTemplate>                                
                        Vị trí
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("PositionName") %><asp:Label ID="lblAdvertPositionId" runat="server" Visible="false" Text='<%# Eval("AdvertPositionId") %>'></asp:Label>
                        <asp:Label ID="lblPositionName" runat="server" Visible="false" Text='<%# Eval("PositionName") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField>
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnFile" runat="server" Text="File" meta:resourcekey="lblGridColumnFile"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate>                         
                        <asp:HyperLink ID="hplAdvertList" runat="server" NavigateUrl= '<%# Eval("ImagePath").ToString() == "" ? "#" : Eval("ImagePath").ToString().StartsWith("http://") ? Eval("ImagePath").ToString() : CmsConstants.ROOT_PATH + Eval("ImagePath") %>'
                         CssClass="dieuhuong123 popup2" meta:resourcekey="hplView">Xem</asp:HyperLink> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="center" Width="68px" Wrap="false" />
                    <HeaderStyle Width="40px" />
                </asp:TemplateField>
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnStatus" runat="server" Text="Trạng thái" meta:resourcekey="lblGridColumnStatus"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                            <%# LanguageHelpers.IsCultureVietnamese() ? AdvertStatus.Static_Get(byte.Parse(Eval("AdvertStatusId").ToString()), l_AdvertStatus).AdvertStatusDesc : AdvertStatus.Static_Get(byte.Parse(Eval("AdvertStatusId").ToString()), l_AdvertStatus).AdvertStatusName%>
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign = "Left" Wrap="false" />  
                    <HeaderStyle Width="10%" />        
                </asp:TemplateField>    
                <asp:TemplateField>
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnOperation" runat="server" Text="Thao tác" meta:resourcekey="lblGridColumnOperation"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:LinkButton ID="lbDelete" runat="server" CssClass="iconadmxoa" CommandName="Delete" ToolTip="Xóa" meta:resourcekey="lnkGridColumnDel"></asp:LinkButton> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="center" Width="68px" Wrap="false" />
                    <HeaderStyle Width="68px" />
                </asp:TemplateField>
                <asp:TemplateField Visible="false">
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
   </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
