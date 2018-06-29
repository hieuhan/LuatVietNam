<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="AdvertPositions.aspx.cs" Inherits="Admin_AdvertPositions" %>
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
                var cdialog = $('<div id="divEdit"></div>')
                .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="98%" height="98%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 520,
                    width: 750,
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
    function submitButton(event) {
        if (event.which == 13) {
            $('#<%= btnSearch.ClientID %>').click();
	}
}
    </script>
    <%--<asp:UpdatePanel ID="upn_Grid" runat="server">
    <Triggers>
        <asp:PostBackTrigger ControlID="lbReview" />
    </Triggers>
    <ContentTemplate>--%>
    <div style="text-align:center"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
    <table cellpadding="2" cellspacing="0" style="width:100%">
     <tr>
            <td style="width:100px">  <asp:Label ID="lblSite" runat="server" 
                    meta:resourcekey="lblSite" Text="Site:"></asp:Label>
                </td>
            <td style="width:260px"> <asp:DropDownList ID="ddlSite" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="SiteDesc" 
                    DataValueField="SiteId" 
                    onselectedindexchanged="ddlSite_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
            <td style="width:60px; white-space:nowrap;"><asp:Label ID="lblCategory" runat="server" Visible="false" Text="Chuyên mục:" meta:resourcekey="lblCategory"></asp:Label>
                </td>
            <td>
                 <asp:DropDownList ID="ddlParentCategory" runat="server" CssClass="userselect" Visible="false"
                    DataTextField="CategoryDesc" DataValueField="CategoryId" Width="250px">
                </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td style="width:100px"><asp:Label ID="lblAppType" runat="server" Text="Loại ứng dụng:" meta:resourcekey="lblAppType"></asp:Label>
                </td>
            <td style="width:260px"><asp:DropDownList ID="ddlAppType" runat="server" 
                    DataTextField="ApplicationTypeDesc" DataValueField="ApplicationTypeId" 
                    Width="250px" CssClass="userselect" AutoPostBack="True" 
                    onselectedindexchanged="ddlAppType_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="width:60px; white-space:nowrap;"><asp:Label ID="lblKeyword" runat="server" Text="Từ khóa:" meta:resourcekey="lblKeyword"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtSearch" runat="server" CssClass="tukhoatimekiem" Width="240px"></asp:TextBox>&nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" CssClass="timkiembutom" 
                    Text="Tìm kiếm" meta:resourcekey="btnSearch" onclick="btnSearch_Click">
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
        <%=GetLocalResourceObject("AdvertPosition").ToString()%>
	</div>
	<div class="chucnangright">
		<a id="popup" href="AdvertPositionsEdit.aspx?SiteId=<%=SiteId %>" title="<%=GetLocalResourceObject("lnkAddNew.title").ToString() %>" class="themmoi popup" > 
            <asp:Label ID="lblAddNew" runat="server" Text="Thêm mới" meta:resourcekey="lblAddNew"></asp:Label>
        </a>
        <asp:LinkButton ID="lbReview" runat="server" CssClass="duyettin" Text="Gen mã QC"  ToolTip="Sau khi sửa quảng cáo, nhấn nút này để cập nhật hiển thị trên website" 
             onclick="lbReview_Click"> 
        </asp:LinkButton>
        <a href="AdvertPositionsViewJs.aspx?SiteId=<%=SiteId %>" title="Js Script" class="duyettin popup" > 
            Xem Js QC
        </a>
	</div>
    <%--<div style="text-align:left; width:200px; float:right"><asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                            <ProgressTemplate><img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...</ProgressTemplate>
                            </asp:UpdateProgress></div>--%>
	<div class="clear5px"></div>
    <div class="contenbangdulieu">
    
        <asp:GridView ID="m_grid" DataKeyNames="AdvertPositionId" runat="server" ShowHeaderWhenEmpty="True"
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
                    <asp:Label ID="lbNo" runat="server" Text='<%# Container.DataItemIndex + 1%>'>                                    
                    </asp:Label>
                    </ItemTemplate>      
                    <ItemStyle HorizontalAlign="Center" Width="3%" />  
                    <HeaderStyle Width="3%" />          
                </asp:TemplateField>        
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnName" runat="server" Text="Tên vị trí" meta:resourcekey="lblGridColumnName"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("PositionName") %> 
                        <asp:Label ID="lblPositionName" runat="server" Text='<%# Eval("PositionName")%>' Visible="false"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnDesc" runat="server" Text="Mô tả" meta:resourcekey="lblGridColumnDesc"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("PositionDesc") %> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnAppType" runat="server" Text="Loại ứng dụng" meta:resourcekey="lblGridColumnAppType"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <span class="ngaythang">
                            <%# LanguageHelpers.IsCultureVietnamese() ? ApplicationTypes.Static_Get(byte.Parse(Eval("ApplicationTypeId").ToString()), l_ApplicationTypes).ApplicationTypeDesc : ApplicationTypes.Static_Get(byte.Parse(Eval("ApplicationTypeId").ToString()), l_ApplicationTypes).ApplicationTypeName %>
                        </span> 
                    </ItemTemplate>
                    <ItemStyle Width="8%"  HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        Site
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <span class="ngaythang">
                            <%# Sites.Static_Get(short.Parse(Eval("SiteId").ToString()), l_Sites).SiteName %>
                        </span> 
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnAdvert" runat="server" Text="Quảng cáo" meta:resourcekey="lblGridColumnAdvert"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:HyperLink ID="hplAdvertList" runat="server" NavigateUrl= '<%# "AdvertPositionAdverts.aspx?AdvertPositionId=" + Eval("AdvertPositionId").ToString() + "&SiteId=" + Eval("SiteId") %>'
                         CssClass="dieuhuong123 popup" meta:resourcekey="hplView">Xem</asp:HyperLink>
                          
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="center" Width="68px" Wrap="false" />
                    <HeaderStyle Width="68px" />
                </asp:TemplateField>
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnTime" runat="server" Text="Thời gian" meta:resourcekey="lblGridColumnTime"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <span class="ngaythang">
                            <%# DateTimeHelpers.GetDateAndTime(Eval("CrDateTime"))%>
                        </span> 
                    </ItemTemplate>
                    <ItemStyle Width="8%" HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="8%" />        
                </asp:TemplateField>
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnUser" runat="server" Text="Tạo bởi" meta:resourcekey="lblGridColumnUser"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <span class="ngaythang">
                            <%# UserHelpers.Static_Get(int.Parse(Eval("CrUserId").ToString()), l_Users, "").Username%>
                        </span> 
                    </ItemTemplate>
                    <ItemStyle Width="8%" HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="8%" />        
                </asp:TemplateField>                  
                <asp:TemplateField>
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnOperation" runat="server" Text="Thao tác" meta:resourcekey="lblGridColumnOperation"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <a id="popup" href='AdvertPositionsEdit.aspx?AdvertPositionId=<%# Eval("AdvertPositionId") %>' class="iconadmsua popup"
                        title="<%# GetLocalResourceObject("lnkGridColumnEdit.title").ToString() %>" meta:resourcekey="lnkGridColumnEdit"></a>    
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
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
