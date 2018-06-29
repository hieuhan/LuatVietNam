<%@ Page Language="C#" MasterPageFile="~/admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="DocsProcessAuto.aspx.cs" Inherits="admin_pages_DocsProcessAuto" Title="" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/admin/UserControls/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="uc1" %>
<%@ Import Namespace="ICSoft.HelperLib" %>
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
        <table cellpadding="3" cellspacing="3" class="tableBorder" style="width: 98%;" >
            <tr>
            <td>
            <table cellpadding="3" cellspacing="3" style="width: 100%;">
                 <tr>
                    <td style="width:90px;">
                        <asp:Label ID="Label1" runat="server" Text="Trạng thái:" ></asp:Label>
                    </td>
                    <td style="width:260px;">
                        <asp:DropDownList ID="ddlProcessStatusId" runat="server" DataTextField="StatusDesc" 
                            DataValueField="StatusId" Width="250px" CssClass="userselect" 
                            AutoPostBack="True" OnSelectedIndexChanged="ddlProcessStatusId_SelectedIndexChanged" >
                            <asp:ListItem Value="1">Chờ xử lý</asp:ListItem>
                            <asp:ListItem Value="2">Đang xử lý</asp:ListItem>
                            <asp:ListItem Value="3">Thành công</asp:ListItem>
                            <asp:ListItem Value="4"> Lỗi </asp:ListItem>
                            <asp:ListItem Value="0">Tất cả</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width:90px;">
                        <asp:Label ID="lblOrderBy" runat="server" Text="Sắp xếp:" meta:resourcekey="lblOrderBy"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlOrderBy" runat="server" DataTextField="OrderByDesc" 
                            DataValueField="OrderBy" Width="250px" CssClass="userselect" 
                            AutoPostBack="True" 
                            onselectedindexchanged="ddlOrderBy_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    
                </tr>
                <tr>
                    <td style="width:90px; white-space:nowrap;">
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
        	</div>
            <div style="text-align:left; width:200px; float:right">
            </div>
        	<div class="clear5px"></div>
            <div class="contenbangdulieu">
                <asp:GridView ID="m_grid" DataKeyNames="DocProcessAutoId" runat="server" ShowHeaderWhenEmpty="True"
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
                             ToolTip ='<%# Eval("DocProcessAutoId").ToString() %>' >                                    
                            </asp:Label>
                            </ItemTemplate>      
                            <ItemStyle Width="5%" />            
                        </asp:TemplateField>  
                         <asp:TemplateField HeaderText="Tên văn bản">  
                            <ItemTemplate> 
                                <asp:Literal ID="ltDocName" runat="server" EnableViewState="false" Text='<%# Eval("DocName") %>'></asp:Literal> 
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Left" />
                        </asp:TemplateField> 
                         <asp:TemplateField HeaderText="Số hiệu">  
                            <ItemTemplate> 
                                <asp:Literal ID="ltDocIdentity" runat="server" EnableViewState="false" Text='<%# Eval("DocIdentity") %>'></asp:Literal> 
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Left" />
                        </asp:TemplateField> 
                         <asp:TemplateField HeaderText="Tên file">  
                            <ItemTemplate> 
                                <asp:Literal ID="ltDocFilePath" runat="server" EnableViewState="false" Text='<%# Eval("DocFilePath") %>'></asp:Literal> 
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Left" />
                        </asp:TemplateField> 
                       <asp:TemplateField HeaderText="Thời gian xử lý">  
                            <ItemTemplate>  
                             <asp:Literal ID="ltProcessTime" runat="server" EnableViewState="false" Text='<%# String.Format("{0:dd/MM/yyyy}", Eval("ProcessTime")) %>'></asp:Literal> 
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Trạng thái">  
                            <ItemTemplate> 
                               <span class="errorlog<%# Eval("ProcessStatusId") %>" style="width:100%;" > <asp:Literal ID="ltProcessStatusId" runat="server" EnableViewState="false" Text='<%# getStatusDesc(Eval("ProcessStatusId")) %>'></asp:Literal> </span>
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Left" Width="8%" />
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
