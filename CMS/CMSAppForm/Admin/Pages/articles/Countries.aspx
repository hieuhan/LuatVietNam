<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="Countries.aspx.cs" Inherits="Admin_Pages_articles_Countries" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/admin/UserControls/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="uc1" %>
<%@ Import Namespace="ICSoft.CMSLib" %>
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
                    height: 500,
                    width: 600,
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
    <div style="text-align:center"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
        <table cellpadding="4" cellspacing="0" style="width: 100%">
            <tr>
                <td style="width: 90px; white-space: nowrap;">
                    <asp:Label ID="lblKey" runat="server" meta:resourcekey="lblKey" Text="Từ khóa:"></asp:Label>
                </td>
                <td style="width: 260px; white-space: nowrap;">
                    <asp:TextBox ID="txtKey" runat="server" Width="240px" Text="" CssClass="tukhoatimekiem" />
                </td>
                <td style="width: 90px; white-space: nowrap;">
                    <asp:Label ID="lblOrderBy" runat="server" meta:resourcekey="lblOrderBy" Text="Sắp xếp:"></asp:Label>
                </td>
                <td style="width: 260px; white-space: nowrap;">
                    <asp:DropDownList ID="ddlOrderBy" runat="server" AutoPostBack="True" CssClass="userselect"
                        DataTextField="OrderByDesc" DataValueField="OrderBy" OnSelectedIndexChanged="ddlOrderBy_SelectedIndexChanged"
                        Width="250px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:LinkButton ID="btnSearch" runat="server" CssClass="timkiembutom" Text="Tìm kiếm"
                        meta:resourcekey="btnSearch" OnClick="btnSearch_Click">
                    </asp:LinkButton>
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
        <%=GetLocalResourceObject("Countries").ToString()%>
	</div>
	<div class="chucnangright">
		<a id="popup" href="CountriesEdit.aspx" title="<%=GetLocalResourceObject("lnkAddNew.Title").ToString() %>" class="themmoi" > 
            <asp:Label ID="lblAddNew" runat="server" Text="Thêm mới" meta:resourcekey="lblAddNew"></asp:Label>
        </a>
	</div>
    <div style="text-align:left; width:200px; float:right"><asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                            <ProgressTemplate><img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...</ProgressTemplate>
                            </asp:UpdateProgress></div>
	<div class="clear5px"></div>
    <div class="contenbangdulieu">
    
        <asp:GridView ID="m_grid" DataKeyNames="CountryId" runat="server" ShowHeaderWhenEmpty="True"
            AutoGenerateColumns="False" CssClass="filter-table" 
            OnRowDeleting="m_grid_RowDeleting" OnRowDataBound = "m_grid_OnRowDataBound"
            Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" 
            PageSize="20" >
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
                        <asp:Label ID="lblName" runat="server" Text="Tên" meta:resourcekey="lblName"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("CountryName") %> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left"  Width="15%" />
                    <HeaderStyle />          
                </asp:TemplateField>
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblDesc" runat="server" Text="Mô tả" meta:resourcekey="lblDesc"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("CountryDesc") %> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" Width="20%" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblIcon" runat="server" Text="Ảnh" meta:resourcekey="lblIcon"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%--<%# Eval("IconPath") %>--%>
                        <img alt="icon" src='<%# Eval("IconPath").ToString() == "" ? "" : CmsConstants.ROOT_PATH + Eval("IconPath") %>' width = "90px" height="90px"/>
                        
                        
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Center"  Width="15%" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblCrUserId" runat="server" Text="Người tạo" meta:resourcekey="lblCrUserId"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                    <span class="ngaythang">
                           <%--<%# sms.admin.security.Users.Static_GetUserNameFullName(int.Parse(Eval("CrUserId").ToString()))  %>--%>
                           <%# getUserName(Int16.Parse(Eval("CrUserId").ToString()))%>
                        </span>
                    </ItemTemplate>
                    <ItemStyle Width="8%" HorizontalAlign = "Center" Wrap="false" />  
                    <HeaderStyle Width="8%" />        
                </asp:TemplateField> 
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblCrDateTime" runat="server" Text="Ngày tạo" meta:resourcekey="lblCrDateTime"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                    <span class="ngaythang">
                            <%# String.Format("{0:dd/MM/yyyy HH:mm}", Eval("CrDateTime"))%>
                        </span>
                    </ItemTemplate>
                    <ItemStyle Width="8%" HorizontalAlign = "Center" Wrap="false" />  
                    <HeaderStyle Width="8%" />        
                </asp:TemplateField>                    
                <asp:TemplateField>
                    <HeaderTemplate>                                
                        <asp:Label ID="lblDisplayOrder" runat="server" Text="Thứ tự hiển thị" meta:resourcekey="lblDisplayOrder"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                    <span class="ngaythang">
                            <%# Eval("DisplayOrder")%> 
                        </span>
                    </ItemTemplate>
                    <ItemStyle Width="5%" HorizontalAlign = "Center" Wrap="false" />  
                    <HeaderStyle Width="5%" />        
                </asp:TemplateField>                   
                <asp:TemplateField>
                    <HeaderTemplate>                                
                        <asp:Label ID="lblThaotac" runat="server" Text="Thao tác" meta:resourcekey="lblThaotac"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <a id="popup" href='CountriesEdit.aspx?CountryId=<%# Eval("CountryId") %>' class="iconadmsua"
                        title="<%# GetLocalResourceObject("lnkGridColumnEdit.Title").ToString() %>" meta:resourcekey="lnkGridColumnEdit"></a>    
                        <asp:LinkButton ID="lbDelete" runat="server" CssClass="iconadmxoa" CommandName="Delete"  meta:resourcekey="lnkGridColumnDel"></asp:LinkButton> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="center" Width="5%" Wrap="false" />
                    <HeaderStyle Width="5%" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
    </div>
        <div class="clear5px"></div>    
        <uc1:CustomPaging ID="CustomPaging" runat="server" />                
        <div class="clear5px"></div> 
   </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
