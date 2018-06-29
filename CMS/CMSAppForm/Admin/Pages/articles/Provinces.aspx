<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="Provinces.aspx.cs" Inherits="Admin_Pages_articles_Provinces" %>

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
                    height: 300,
                    width: 500,
                    title: $(this).attr("title"),
                    close: function (event, ui) {
                        $(this).remove();
                        //window.location = $('#<%= btnSearch.ClientID %>').attr("href");
                        $('#<%= btnSearch.ClientID %>').click();
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
        <table cellpadding="2" cellspacing="0" style="width: 100%">
            <tr><td style="width:90px; white-space:nowrap;">
                    <asp:Label ID="lblCountries" runat="server" meta:resourcekey="lblCountries" Text="Quốc gia"></asp:Label>
                    </td>
                    <td style="width:260px; white-space:nowrap;">
                    <asp:DropDownList ID="ddlCountries" runat="server" AutoPostBack="True" CssClass="userselect"
                        DataTextField="CountryDesc" DataValueField="CountryId" OnSelectedIndexChanged="ddlCountries_SelectedIndexChanged"
                        Width="250px">
                    </asp:DropDownList>
                </td>
                <td style="width:90px; white-space:nowrap;">
                    <asp:Label ID="lblOrderBy" runat="server" meta:resourcekey="lblOrderBy" Text="Sắp xếp:"></asp:Label>
                    </td>
                    <td style="width:260px; white-space:nowrap;">
                    <asp:DropDownList ID="ddlOrderBy" runat="server" AutoPostBack="True" CssClass="userselect"
                        DataTextField="OrderByDesc" DataValueField="OrderBy" OnSelectedIndexChanged="ddlOrderBy_SelectedIndexChanged"
                        Width="250px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
            <td style="width:90px; white-space:nowrap;">
                <asp:Label ID="lblKey" runat="server" meta:resourcekey="lblKey" Text="Từ khóa:"></asp:Label>
                </td>
                <td style="width:260px; white-space:nowrap;">
                    <asp:TextBox ID="txtKey" runat="server" Width="240px" Text="" CssClass="tukhoatimekiem" />
                   
                    
                </td>
                <td></td>
                <td style="width:90px; white-space:nowrap;">
                    <asp:Button ID="btnSearch" runat="server" CssClass="timkiembutom" Text="Tìm kiếm"
                        meta:resourcekey="btnSearch" OnClick="btnSearch_Click">
                    </asp:Button>
                </td>
                <td>
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
		<a id="popup" href="ProvincesEdit.aspx?CountryId=<%=CountryId %>" title="<%=GetLocalResourceObject("lnkAddNew.Title").ToString() %>" class="themmoi" > 
            <asp:Label ID="lblAddNew" runat="server" Text="Thêm mới" meta:resourcekey="lblAddNew"></asp:Label>
        </a>
        <asp:LinkButton ID="lbDelete" runat="server" CssClass="xoatin" Text="Xóa"  OnClientClick="return confirm('Bạn có chắc muốn xóa toàn bộ dữ liệu đã chọn?')"
                    meta:resourcekey="lbDelete" onclick="lbDelete_Click">
                </asp:LinkButton>
	</div>
    <div style="text-align:left; width:200px; float:right"></div>
	<div class="clear5px"></div>
    <div class="contenbangdulieu">
    
        <asp:GridView ID="m_grid" DataKeyNames="ProvinceId" runat="server" ShowHeaderWhenEmpty="True"
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
                        <asp:Label ID="lblName" runat="server" Text="Tên Site" meta:resourcekey="lblName"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("ProvinceName") %> 
                        <asp:Label ID="lblProvinceName" runat="server" Text='<%# Eval("ProvinceName")%>' Visible="false"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left"  Width="15%" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblDesc" runat="server" Text="Mô tả" meta:resourcekey="lblDesc"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("ProvinceDesc") %> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left"/>
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblCountries" runat="server" Text="Quốc gia" meta:resourcekey="lblCountries"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <span class="ngaythang">
                            <%# Countries.Static_Get(short.Parse(Eval("CountryId").ToString()), l_Country).CountryName %>
                        </span> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Center" Width="15%" />
                    <HeaderStyle />
                </asp:TemplateField>
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblCrUserId" runat="server" Text="Người tạo" meta:resourcekey="lblCrUserId"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                    <span class="ngaythang">
                          <%# ICSoft.HelperLib.UserHelpers.Static_Get(int.Parse(Eval("CrUserId").ToString()), l_Users, "").Username %>
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
                        <asp:Label ID="lblDisplayOrder" runat="server" Text="Thứ tự" meta:resourcekey="lblDisplayOrder"></asp:Label>
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
                        <a id="popup" href='ProvincesEdit.aspx?ProvinceId=<%# Eval("ProvinceId") %>' class="iconadmsua"
                        title="<%# GetLocalResourceObject("lnkGridColumnEdit").ToString() %>" meta:resourcekey="lnkGridColumnEdit"></a>    
                        <asp:LinkButton ID="lbDelete" runat="server" CssClass="iconadmxoa" CommandName="Delete" ToolTip="Xóa" meta:resourcekey="lnkGridColumnDel"></asp:LinkButton> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="center" Width="7%" Wrap="false" />
                    <HeaderStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField>
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
    </div>
   </div>
</asp:Content>

