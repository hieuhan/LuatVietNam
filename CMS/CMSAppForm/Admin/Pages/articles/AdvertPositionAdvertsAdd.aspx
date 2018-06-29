<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="AdvertPositionAdvertsAdd.aspx.cs" Inherits="Admin_AdvertPositionAdvertsAdd" %>
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

        $('a.popup2').live('click', function (e) {
            var page = $(this).attr("href")
            var cdialog = $('<div id="divEdit1"></div>')
                .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="98%" height="98%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 350,
                    width: 550,
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
    <Triggers>
     <asp:PostBackTrigger ControlID="btnAdd" />
    </Triggers>
    <ContentTemplate>
    <div style="text-align:center"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
	<div class="clear5px"></div>  
    <div class="khungchucnang">
    <div class="chucnangleft">
		<span class="tieudetongcong">
            <asp:Label ID="lblTotalText" runat="server" Text="Tổng cộng:" meta:resourcekey="lblTotalText"></asp:Label>
        </span>
        <asp:Label ID="lblTong" runat="server" Text="" CssClass="tieudetongcong2"></asp:Label>
	</div>
	<div class="chucnangright">
		<asp:LinkButton ID="btnAdd" runat="server" CssClass="savebutom" meta:resourcekey="btnSave" 
                    Text="Lưu" onclick="btnAdd_Click">
                        </asp:LinkButton>
	</div>
    <div style="text-align:left; width:200px; float:right"><asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                            <ProgressTemplate><img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...</ProgressTemplate>
                            </asp:UpdateProgress></div>
	<div class="clear5px"></div>
    <div class="contenbangdulieu">
    
        <asp:GridView ID="m_grid" DataKeyNames="AdvertId" runat="server" ShowHeaderWhenEmpty="True"
            AutoGenerateColumns="False" CssClass="filter-table" 
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
                        <asp:Label ID="lblGridColumnName" runat="server" Text="Quảng cáo" meta:resourcekey="lblGridColumnName"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("AdvertName") %> <br />
                        <%# Eval("URL") %> 
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
                         CssClass="popup2 dieuhuong123" meta:resourcekey="hplView">Xem</asp:HyperLink> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="center" Width="68px" Wrap="false" />
                    <HeaderStyle Width="68px" />
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
                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                    <HeaderStyle Width="20px" />
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
