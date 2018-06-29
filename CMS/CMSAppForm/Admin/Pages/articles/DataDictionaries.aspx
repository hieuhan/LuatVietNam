<%@ Page Language="C#" MasterPageFile="~/admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="DataDictionaries.aspx.cs" Inherits="admin_pages_DataDictionaries" Title="" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/admin/UserControls/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="uc1" %>
<%@ Import Namespace="ICSoft.CMSLib" %>
<%@ Import Namespace="ICSoft.HelperLib" %>
<asp:Content ID="contentAccountType" runat="server" ContentPlaceHolderID="m_contentBody">
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
                    height: 460,
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
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSearch" />
        </Triggers>
        <ContentTemplate>
            <table cellpadding="2" cellspacing="2" style="width: 100%;">
                <tr>
                    <td style="width: 50px; white-space: nowrap;">
                        <asp:Label ID="lblDataDictionaryTypes" runat="server" meta:resourcekey="lblDataDictionaryTypes"
                            Text="Kiểu Dữ Liệu:"></asp:Label>
                    </td>
                    <td style="width: 260px">
                        <asp:DropDownList ID="ddlDataDictionaryTypes" runat="server" AutoPostBack="True"
                            CssClass="userselect" DataTextField="DataDictionaryTypeDesc" DataValueField="DataDictionaryTypeId"
                            OnSelectedIndexChanged="ddlDataDictionaryTypes_SelectedIndexChanged" Width="250px">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 50px; white-space: nowrap;">
                        <asp:Label ID="lblKeyword" runat="server" Text="Từ khóa:" meta:resourcekey="lblKeyword"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="tukhoatimekiem" Width="240px"></asp:TextBox>&nbsp;&nbsp;
                        
                    </td>
                </tr>
                <tr>
                    <td style="width: 50px; white-space: nowrap;">
                        <asp:Label ID="lblOrderBy" runat="server" meta:resourcekey="lblOrderBy" Text="Sắp xếp:"></asp:Label>
                    </td>
                    <td><asp:DropDownList ID="ddlOrderBy" runat="server" AutoPostBack="True" CssClass="userselect"
                            DataTextField="OrderByDesc" DataValueField="OrderBy" OnSelectedIndexChanged="ddlOrderBy_SelectedIndexChanged"
                            Width="250px">
                        </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                    <td><asp:LinkButton ID="btnSearch" runat="server" CssClass="timkiembutom" Text="Tìm kiếm"
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
        	</div>
        	<div class="chucnangright">
            <a href="DataDictionariesEdit.aspx?DataDictionaryTypeId=<%=DataDictionaryTypeId %>" title="<%=GetLocalResourceObject("lnkAddNew.title").ToString() %>" class="themmoi popup" > 
                    <asp:Label ID="lblAddNew" runat="server" Text="Thêm mới" meta:resourcekey="lblAddNew"></asp:Label>
                </a>
                <asp:LinkButton ID="lbDelete" runat="server" CssClass="xoatin" Text="Xóa"  OnClientClick="return confirm('Bạn có chắc muốn xóa toàn bộ dữ liệu đã chọn?')"
                    meta:resourcekey="lbDelete" onclick="lbDelete_Click">
                </asp:LinkButton>	
        		
        	</div>
            <div style="text-align:left; width:200px; float:right">
                <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                    <ProgressTemplate>
                        <img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        	<div class="clear5px"></div>
            <div class="contenbangdulieu">
                <asp:GridView ID="m_grid" DataKeyNames="DataDictionaryId" runat="server" ShowHeaderWhenEmpty="True"
                    AutoGenerateColumns="False" CssClass="filter-table" OnRowDeleting="m_grid_RowDeleting"  OnRowDataBound = "m_grid_OnRowDataBound"
                    Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" PageSize="50" >
                    <HeaderStyle CssClass="trbangdulieutieude" />
                    <FooterStyle CssClass="grid_foot" />
                    <RowStyle CssClass="trbangdulieutieudenoidung" />
                    <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                    <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />
                    <EditRowStyle CssClass="trbangdulieutieudenoidung" />
                    <PagerStyle CssClass="grid_page" />
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                            <asp:Label ID="lbNo" runat="server" Text='<%# Container.DataItemIndex + (CustomPaging.PageIndex - 1) * m_grid.PageSize + 1%>' 
                             ToolTip ='<%# Eval("DataDictionaryId").ToString() %>' >                                    
                            </asp:Label>
                            </ItemTemplate>      
                            <ItemStyle Width="3%" HorizontalAlign="Center" />            
                        </asp:TemplateField>        
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblGridColumnName" runat="server" Text="Tên" meta:resourcekey="lblGridColumnName"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Literal ID="ltDataDictionaryName" runat="server" EnableViewState="false" Text='<%# Eval("DataDictionaryName") %>'></asp:Literal>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="25%" />
                        </asp:TemplateField>

                        <asp:TemplateField>  
                            <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnDataDesc" runat="server" Text="Mô tả" meta:resourcekey="lblGridColumnDataDesc"></asp:Label>
                                </HeaderTemplate>
                            <ItemTemplate> 
                                <asp:Literal ID="ltDataDictionaryDesc" runat="server" EnableViewState="false" Text='<%# Eval("DataDictionaryDesc") %>'></asp:Literal> 
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Left" Width="25%" />
                        </asp:TemplateField> 
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblGridColumnMinValue" runat="server" Text="Giá Trị NN" meta:resourcekey="lblGridColumnMinValue"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                            <span class="ngaythang">
                                <asp:Literal ID="ltMinValue" runat="server" EnableViewState="false" Text='<%# Eval("MinValue").ToString() %>'></asp:Literal> 
                                </span>
                            </Itemtemplate>
                                <itemstyle horizontalalign="Left" width="5%" />
                                </asp:TemplateField>
                                <asp:TemplateField>

                             <HeaderTemplate>
                                <asp:Label ID="lblGridColumnMaxValue" runat="server" Text="Giá Trị LN" meta:resourcekey="lblGridColumnMaxValue"></asp:Label>
                            </HeaderTemplate>  
                            <ItemTemplate> 
                                <asp:Literal ID="ltMaxValue" runat="server" EnableViewState="false" Text='<%# Eval("MaxValue").ToString() %>'></asp:Literal> 
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Left" Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thứ Tự">
                             <HeaderTemplate>
                                <asp:Label ID="lblGridColumnDisplayOrder" runat="server" Text="Thứ Tự Hiển Thị" meta:resourcekey="lblGridColumnDisplayOrder"></asp:Label>
                            </HeaderTemplate>  
                            <ItemTemplate> 
                                <asp:Literal ID="ltDisplayOrder" runat="server" EnableViewState="false" Text='<%# Eval("DisplayOrder").ToString() %>'></asp:Literal> 
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Left" Width="5%"/>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblGridColumnUser" runat="server" Text="Tạo bởi" meta:resourcekey="lblGridColumnUser"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <span class="ngaythang">
                                    <%# UserHelpers.Static_Get(int.Parse(Eval("CrUserId").ToString()), l_Users, "").Username %>
                                </span>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                       <asp:TemplateField>  
                            <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnTime" runat="server" Text="Thời gian" meta:resourcekey="lblGridColumnTime"></asp:Label>
                                </HeaderTemplate>
                            <ItemTemplate>  
                             <asp:Literal ID="ltCrDateTime" runat="server" EnableViewState="false" Text='<%# String.Format("{0:dd/MM/yyyy HH:mm}", Eval("CrDateTime")) %>'></asp:Literal> 
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnOperation" runat="server" Text="Thao tác" meta:resourcekey="lblGridColumnOperation"></asp:Label>
                                </HeaderTemplate>
                            <ItemStyle HorizontalAlign="center"  Wrap="false" />
                            <HeaderStyle Width="6%" />
                            <ItemTemplate>
                                 <a href='DataDictionariesEdit.aspx?id=<%# Eval("DataDictionaryId") %>' class="iconadmsua popup" 
                                title="<%# GetLocalResourceObject("lnkGridColumnEdit.title").ToString() %>" meta:resourcekey="lnkGridColumnEdit" ></a>
                                <asp:LinkButton ID="lbDelete" runat="server" CssClass="iconadmxoa" CommandName="Delete" ToolTip="Xóa" meta:resourcekey="lnkGridColumnDel"></asp:LinkButton>                                
                            </ItemTemplate>
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
            <div class="clear5px"></div>    
            <uc1:CustomPaging ID="CustomPaging" runat="server" />                
          </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

