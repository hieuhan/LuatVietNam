<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="PositionDisplayRows.aspx.cs" Inherits="Admin_PositionDisplayRows" %>
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
        });
    function InitializeRequest(sender, args) {
    }

    function EndRequest(sender, args) {
        
    }
    </script>
    <asp:UpdatePanel ID="upn_Grid" runat="server">
    <ContentTemplate>
    <div style="width:auto; height:60px; overflow:auto;">
    <div style="text-align:center"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
    <asp:LinkButton ID="btnSearch" runat="server" Width="1px" Height="1px"
                    Text="" onclick="btnSearch_Click">
                        </asp:LinkButton>
    <table cellpadding="2" cellspacing="0" style="width:100%">
        <tr>
            <td style="width:60px"><asp:Label ID="lblSite" runat="server" Text="Site:" meta:resourcekey="lblSite"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlSite" runat="server" DataTextField="SiteDesc" 
                    DataValueField="SiteId" Width="250px" CssClass="userselect"
                    AutoPostBack="True" onselectedindexchanged="ddlSite_SelectedIndexChanged">
                </asp:DropDownList>&nbsp;&nbsp;
            </td>
            <td style="text-align:right"><asp:LinkButton ID="btnSavePosition" runat="server" CssClass="savebutom"
                    Text="Lưu thông tin" onclick="btnSavePosition_Click" meta:resourcekey="btnSavePosition">
                        </asp:LinkButton>
            </td>
        </tr>
    </table>
    <div class="clear5px"></div>
	<div class="vien"></div>
	<div class="clear5px"></div>  
    </div>
    <div style="width:auto; height:390px; overflow:auto;">
    <div class="khungchucnang">
    <div style="text-align:left; width:200px; float:right"><asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                            <ProgressTemplate><img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...</ProgressTemplate>
                            </asp:UpdateProgress></div>
	<div class="clear5px"></div>
    <div class="contenbangdulieu">
    
        <asp:GridView ID="m_grid" DataKeyNames="DisplayTypeId" runat="server" ShowHeaderWhenEmpty="True"
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
                        <asp:Label ID="lbNo" runat="server" Text='<%# Container.DataItemIndex + 1%>'></asp:Label>                                 
                    </ItemTemplate>      
                    <ItemStyle HorizontalAlign="Center" Width="5%" />  
                    <HeaderStyle Width="5%" />          
                </asp:TemplateField>        
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnName" runat="server" Text="Vị trí" meta:resourcekey="lblGridColumnName"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("DisplayTypeName")%>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                 <asp:TemplateField > 
                        <HeaderTemplate>                                
                        RowDisplay
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:TextBox ID="txtRowDisplay" runat="server" Text='<%# Eval("RowDisplay") %>' Width="50px"></asp:TextBox>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Center" />
                    <HeaderStyle Width="10%" />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        UseSummary
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:CheckBox ID="chkUseSummary" Checked='<%# Eval("UseSummary").ToString() == "1" ? true:false %>' runat="server" />
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Center" />
                    <HeaderStyle Width="10%" />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        UseArticleContent
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:CheckBox ID="chkUseArticleContent" Checked='<%# Eval("UseArticleContent").ToString() == "1" ? true:false %>' runat="server" />
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Center" />
                    <HeaderStyle Width="10%" />          
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
   </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
