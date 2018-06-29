<%@ Page Title="" Language="C#" MasterPageFile="~/admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="InternalLinksAdm.aspx.cs" Inherits="admin_pages_articles_InternalLinksAdm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/admin/UserControls/CustomPaging.ascx" TagName="CustomPaging"    TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" runat="Server">
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
                    width: 400,
                    title: $(this).attr("title"),
                    close: function (event, ui) {
                        $(this).remove();
                        window.location = $('#<%= btnSearch.ClientID %>').attr("href");
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
            <div style="text-align: center">
                <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
            <table cellpadding="2" cellspacing="0" style="width: 100%; background-color:#fff"> 
                <tr>
                    <td style="width:30px; white-space:nowrap;">
                        Trang:
                    </td>
                    <td style="width:160px">
                        <asp:DropDownList ID="ddlSites" runat="server" AutoPostBack="True" CssClass="userselect"
                             DataTextField="SiteName" DataValueField="SiteId" OnSelectedIndexChanged="ddlddlSites_SelectedIndexChanged" Width="150px">
                        </asp:DropDownList>
                    </td>
                    <td style="width:30px; white-space:nowrap;">
                        Nhóm:
                    </td>
                    <td style="width:160px">
                        <asp:DropDownList ID="ddlInternalLinkGroups" runat="server" AutoPostBack="True" CssClass="userselect"
                             DataTextField="InternalLinkGroupName" DataValueField="InternalLinkGroupId" OnSelectedIndexChanged="ddlInternalLinkGroups_SelectedIndexChanged" Width="150px">
                        </asp:DropDownList>
                    </td>
                    <td style="width:60px; white-space:nowrap;">
                        Từ khóa:
                    </td>
                    <td style="width:160px">
                       <asp:TextBox ID="txtSearch" runat="server" CssClass="tukhoatimekiem" Width="150px"></asp:TextBox>
                    </td>
                    <td style="width:90px; white-space:nowrap;">
                        <asp:Label ID="lblOrderBy" runat="server" meta:resourcekey="lblOrderBy" Text="Sắp xếp theo:"></asp:Label>
                    </td>
                    <td style="width:100px">
                        <asp:DropDownList ID="ddlOrderBy" runat="server" AutoPostBack="True" CssClass="userselect"
                             OnSelectedIndexChanged="ddlOrderBy_SelectedIndexChanged" Width="90px">
                            <asp:ListItem Value="InternalLinkId">Ngày tạo</asp:ListItem>
                            <asp:ListItem Value="LinkCounter">Số Link đã có</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width:100px">
                        <asp:DropDownList ID="ddlOrderTypes" runat="server" AutoPostBack="True" CssClass="userselect"
                             OnSelectedIndexChanged="ddlOrderTypes_SelectedIndexChanged" Width="90px">
                            <asp:ListItem Value="DESC">Giảm dần</asp:ListItem>
                            <asp:ListItem Value="ASC">Tăng dần</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width:90px; white-space:nowrap;">
                        <asp:LinkButton ID="btnSearch" runat="server" CssClass="timkiembutom" 
                        Text="Tìm kiếm" onclick="btnSearch_Click">
                        </asp:LinkButton>
                    </td>
                    <td>
                       
                    </td>
                </tr>
            </table>            
            <div class="khungchucnang" style=" background-color:#fff; padding-top: 10px;">
                <div class="chucnangleft">
                    <span class="tieudetongcong">
                        <asp:Label ID="lblTotalText" runat="server" Text="Tổng cộng:" ></asp:Label>
                    </span>
                    <asp:Label ID="lblTong" runat="server" Text="" CssClass="tieudetongcong2"></asp:Label>
                </div>
                <div class="chucnangright">
                    <a id="popup" href="InternalLinksEdit.aspx" class="themmoi">
                        <asp:Label ID="lblAddNew" runat="server" Text="Thêm mới" ></asp:Label>
                    </a>
                    <asp:LinkButton ID="lbDelete" runat="server" CssClass="xoatin" Text="Xóa" OnClick="lbDelete_Click">
                    </asp:LinkButton>
                </div>
                <div style="text-align: left; width: 200px; float: right">
                    
                </div>
                <div class="clear5px">
                </div>
                <div class="contenbangdulieu">
                    <asp:GridView ID="m_grid" DataKeyNames="InternalLinkId" runat="server" ShowHeaderWhenEmpty="True"
                        AutoGenerateColumns="False" CssClass="filter-table" OnRowDeleting="m_grid_RowDeleting"
                        OnRowDataBound="m_grid_OnRowDataBound" Width="100%" CellPadding="0" CellSpacing="0"
                        BorderWidth="0" PageSize="50">
                        <HeaderStyle CssClass="trbangdulieutieude" />
                        <RowStyle CssClass="trbangdulieutieudenoidung" />
                        <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                        <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    #
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbNo" runat="server" Text='<%# Container.DataItemIndex + (CustomPaging.PageIndex - 1) * m_grid.PageSize + 1%>'
                                        ToolTip='<%# Eval("InternalLinkId").ToString() %>'> </asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="3%" />
                                <HeaderStyle Width="3%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnName" runat="server" Text="Tên Tags" ></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("InternalLinkName")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle Width="30%" />
                            </asp:TemplateField>   
                             <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblUrl" runat="server" Text="Url" ></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("InternalLinkUrl")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle Width="35%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblCrDateTime" runat="server" Text="Ngày tạo" ></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("CrDateTime", "{0:dd/MM/yyyy HH:mm}")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle Width="13%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblLinkCounter" runat="server" Text="Số Link" ></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("LinkCounter")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Width="7%" />
                            </asp:TemplateField> 
                                                                
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnOperation" runat="server" Text="Thao tác" ></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>                 
                                    <a id="popup" href='InternalLinksEdit.aspx?InternalLinkId=<%# Eval("InternalLinkId") %>' class="iconadmsua" ></a>                  
                                    <asp:LinkButton ID="lbDelete" runat="server" CssClass="iconadmxoa" CommandName="Delete" ToolTip="Xóa" ></asp:LinkButton>                                    
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Wrap="false" />
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
                <div class="clear5px">
                </div>
                <uc1:CustomPaging ID="CustomPaging" runat="server" />
                <div class="clear5px">
                </div>
            </div>
</asp:Content>
