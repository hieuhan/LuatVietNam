<%@ Page Language="C#" MasterPageFile="~/admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="PromotionCodes.aspx.cs" Inherits="admin_pages_PromotionCodes" Title="" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/admin/UserControls/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="uc1" %>
<%@ Import Namespace="ICSoft.HelperLib" %>
<%@ Import Namespace="ICSoft.LawDocsLib" %>
<asp:Content ID="contentAccountType" runat="server" ContentPlaceHolderID="m_contentBody">
    <script type="text/javascript">
        var cdialog = $('<div id="divEdit"></div>');
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);
            $("#<%= txtDateFrom.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
            $("#<%= txtDateTo.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });

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
            $("#<%= txtDateFrom.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
            $("#<%= txtDateTo.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        }
    </script>
        <asp:Label ID="lblPromotion" Font-Bold="true" ForeColor="Blue" Font-Size="Large" runat="server" Text="Label"></asp:Label>
        <div class="clear5px"></div>
        <table cellpadding="3" cellspacing="3" class="tableBorder" style="width: 98%;" >
            <tr>
            <td>
            <table cellpadding="3" cellspacing="3" style="width: 100%;">
                <tr>
                    <td>
                        <asp:Label ID="lblOrderBy" runat="server" Text="Sắp xếp:" meta:resourcekey="lblOrderBy"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlOrderBy" runat="server"  Width="250px" CssClass="userselect" 
                            AutoPostBack="True"  onselectedindexchanged="ddlOrderBy_SelectedIndexChanged">
                            <asp:ListItem Text="Mới thêm" Value="PromotionCodeId Desc"></asp:ListItem>
                             <asp:ListItem Text="Theo mã khuyến mại" Value="PromotionCode asc"></asp:ListItem>
                             <asp:ListItem Text="Thời gian dùng mới nhất" Value="UsingTime desc"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width:90px; white-space:nowrap;">
                      </td>
                    <td style="width:260px"> 
                       
                    </td>
                    
                </tr>
                <tr>
                    <td style="width:60px; white-space:nowrap;">
                        <asp:Label ID="lblDateFrom" runat="server" Text="Từ ngày:" meta:resourcekey="lblDateFrom"></asp:Label>
                    </td>
                    <td style="width:260px">
                        <asp:TextBox ID="txtDateFrom" runat="server" CssClass="tukhoatimekiem" Width="100px"></asp:TextBox>
                        <asp:Label ID="lblDateTo" runat="server" Text="đến:" meta:resourcekey="lblDateTo"></asp:Label>
                        <asp:TextBox ID="txtDateTo" runat="server" CssClass="tukhoatimekiem" Width="100px"></asp:TextBox>
                        </td>
                    <td style="width:90px; white-space:nowrap;">
                        <asp:Label ID="lblKeyword" runat="server" Text="Từ khóa:" meta:resourcekey="lblKeyword"></asp:Label>
                    </td>
                    <td><asp:TextBox ID="txtSearch" runat="server" CssClass="tukhoatimekiem" Width="240px"></asp:TextBox>&nbsp;&nbsp;
                        <asp:LinkButton ID="btnSearch" runat="server" CssClass="timkiembutom" 
                            Text="Tìm kiếm" meta:resourcekey="btnSearch" onclick="btnSearch_Click">
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
                <asp:LinkButton ID="lbDelete" runat="server" CssClass="xoatin" Text="Xóa"  OnClientClick="return confirm('Bạn có chắc muốn xóa toàn bộ dữ liệu đã chọn?')"
                    meta:resourcekey="lbDelete" onclick="lbDelete_Click">
                </asp:LinkButton>
                <asp:LinkButton ID="lbUnCheck" runat="server" CssClass="boduyet" Visible="false"
                    Text="Bỏ duyệt" meta:resourcekey="lbUnCheck" onclick="lbUnCheck_Click"> 
                </asp:LinkButton>
                <asp:LinkButton ID="lbReview" runat="server" CssClass="duyettin" Text="Duyệt" Visible="false"
                    meta:resourcekey="lbReview" onclick="lbReview_Click"> 
                </asp:LinkButton>					
        		<%--<a href="PromotionCodesEdit.aspx?PromotionId=<%= PromotionId %>" title="Thêm mới " class="themmoi popup" > 
                    <asp:Label ID="lblAddNew" runat="server" Text="Thêm mới" meta:resourcekey="lblAddNew"></asp:Label>
                </a>--%>
                <asp:LinkButton ID="lbAddAuto" runat="server" CssClass="themmoi" Text="Thêm mã tự động" OnClick="lbAddAuto_Click" > 
                </asp:LinkButton>	
                <a href="Promotions.aspx" title="Quay lại" class="timkiembutom" > 
                    Quay về danh sách khuyến mại
                </a>
        	</div>
            <div style="text-align:left; width:200px; float:right">
            </div>
        	<div class="clear5px"></div>
            <div class="contenbangdulieu">
                <asp:GridView ID="m_grid" DataKeyNames="PromotionCodeId" runat="server" ShowHeaderWhenEmpty="True"
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
                             ToolTip ='<%# Eval("PromotionCodeId").ToString() %>' >                                    
                            </asp:Label>
                            </ItemTemplate>      
                            <ItemStyle Width="5%" />            
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Mã khuyến mại">  
                            <ItemTemplate> 
                                <asp:Literal ID="ltPromotionCode" runat="server" EnableViewState="false" Text='<%# Eval("PromotionCode") %>'></asp:Literal> 
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Left" />
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Số lần đã dùng">  
                            <ItemTemplate> 
                                <asp:Literal ID="ltUsingCount" runat="server" EnableViewState="false" Text='<%# Eval("UsingCount").ToString() %>'></asp:Literal> 
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ID Khách hàng">  
                            <ItemTemplate> 
                                <%# Eval("UsingCustomerId") %>
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thời gian dùng">  
                            <ItemTemplate> 
                                <%# Eval("UsingTime") %>
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thời gian tạo">  
                            <ItemTemplate> 
                                <%# Eval("CrDateTime") %>
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sửa">
                            <ItemStyle HorizontalAlign="center"  Wrap="false" />
                            <HeaderStyle Width="6%" />
                            <ItemTemplate>
                                <a href='PromotionCodesEdit.aspx?id=<%# Eval("PromotionCodeId") %>' class="iconadmsua popup" ></a>
                                <asp:LinkButton ID="lbtDelete" runat="server" CausesValidation="False" OnClientClick="return confirm('Bạn có chắc muốn xóa dữ liệu này?');"
                                    CommandName="Delete" Text="" CssClass="iconadmxoa" ></asp:LinkButton>
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
            <div class="clear5px"></div>   
          </div>
          </td>
          </tr>
          </table>
 </asp:Content>
