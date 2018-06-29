<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="Articles_ReportData.aspx.cs" Inherits="Admin_Pages_lawsdocs_Articles_ReportData" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/admin/UserControls/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="uc1" %>
<%@ Import Namespace="ICSoft.CMSLib" %>
<%@ Import Namespace="ICSoft.LawDocsLib" %>
<%@ Import Namespace="ICSoft.HelperLib" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
                <script type="text/javascript">
                    $(document).ready(function () {
                        var prm = Sys.WebForms.PageRequestManager.getInstance();
                        prm.add_initializeRequest(InitializeRequest);
                        prm.add_endRequest(EndRequest);
                        $("#<%= txtDateFrom.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
                    $("#<%= txtDateTo.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
                });
                function InitializeRequest(sender, args) {
                }

                function EndRequest(sender, args) {
                    $(function () {
                        $("#<%= txtDateFrom.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
                });
                $(function () {
                    $("#<%= txtDateTo.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
                });
            }


    </script>
    <asp:UpdatePanel ID="upn_Grid" runat="server">
    <ContentTemplate>
    <div style="text-align:center;"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
    <table cellpadding="2" cellspacing="0" style="width:100%">
          <tr runat="server" id="lblDateFromTo">
            <td style="width:60px; white-space:nowrap;">
                <asp:Label ID="lblDateFrom" runat="server" meta:resourcekey="lblDateFrom" 
                    Text="Từ ngày:"></asp:Label>
                </td>
            <td style="width:260px">
                <asp:TextBox ID="txtDateFrom" runat="server"  CssClass="tukhoatimekiem"
                            Width="240px"></asp:TextBox>
            </td>
            <td style="width:60px; white-space:nowrap;">
                <asp:Label ID="lblDateTo" runat="server" meta:resourcekey="lblDateTo" 
                    Text="Đến ngày"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtDateTo" runat="server"  CssClass="tukhoatimekiem"
                            Width="240px"></asp:TextBox>
            </td>
        </tr>
        
        <tr>
            <td style="width:10%;">
                <asp:Label ID="lblOrderBy" runat="server" meta:resourcekey="lblOrderBy" 
                    Text="Khoảng thời gian:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlOrderBy" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="OrderByDesc" DataValueField="OrderBy" 
                    onselectedindexchanged="ddlOrderBy_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
            <td>
            </td>
            <td>
                
                 <asp:LinkButton ID="btnSearch" runat="server" CssClass="timkiembutom" 
                    meta:resourcekey="btnSearch" onclick="btnSearch_Click" Text="Tìm kiếm">
                        </asp:LinkButton>
                &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="btnXuatExcel" runat="server" OnClick="btnXuatExcel_Click" CssClass="timkiembutom" Text="Xuất excel"> Xuất Excel</span>
                    </asp:LinkButton>
            </td>
        </tr>
        
    </table>
    <div class="clear5px"></div>
	<div class="vien"></div>
	<div class="clear5px"></div>  
    <div class="khungchucnang">
    <div class="chucnangleft">
		<%--<span class="tieudetongcong">
            <asp:Label ID="lblTotalText" runat="server" Text="Tổng:" meta:resourcekey="lblTotalText"></asp:Label>
        </span>
        <asp:Label ID="lblTong" runat="server" Text="" CssClass="tieudetongcong2"></asp:Label> --%>
	</div>
	<div class="chucnangright">
	</div>
    <div style="text-align:left; width:50%; float:right"><asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                            <ProgressTemplate><img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...</ProgressTemplate>
                            </asp:UpdateProgress></div>
	<div class="clear5px"></div>
    <div class="contenbangdulieu" runat="server" id="tblbydoctype">
        <asp:GridView ID="m_grid" DataKeyNames="CategoryId" runat="server" ShowHeaderWhenEmpty="True"
            AutoGenerateColumns="False" ShowFooter="true" OnRowDataBound = "m_grid_RowDataBound"
            Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" PageSize="50">
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
                    <asp:Label ID="lbNo" runat="server" Text='<%# Container.DataItemIndex +1%>' 
                                     ToolTip ='<%# Eval("CategoryId").ToString() %>'> </asp:Label>
                    </ItemTemplate>      
                    <ItemStyle HorizontalAlign="Center" Width="3%" />  
                    <HeaderStyle Width="3%" />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnCategoryName" runat="server" Text="Tin" meta:resourcekey="lblGridColumnCategoryName"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("Title")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                </asp:TemplateField> 
                 <asp:TemplateField > 
                        <HeaderTemplate>               
                        <asp:Label ID="lblGridColumnSumApproved" runat="server" Text="Đã duyệt" meta:resourcekey="lblGridColumnSumApproved"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("SumByStatus1")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Center" />
                </asp:TemplateField>   
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnSumPending" runat="server" Text="Chờ duyệt" meta:resourcekey="lblGridColumnSumPending"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("SumByStatus2")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Center" />
                    <HeaderStyle />          
                </asp:TemplateField>      
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnSumChanged" runat="server" Text="Tác động thay đổi" meta:resourcekey="lblGridColumnSumChanged"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                       <%# Eval("SumByStatus3")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Center" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnSumSourceLVN" runat="server" Text="Nguồn LVN" meta:resourcekey="lblGridColumnSumSourceLVN"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <%# Eval("SumBySource1")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign = "Center" />  
                    <HeaderStyle/>        
                </asp:TemplateField>
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnSumSourceCTV" runat="server" Text="Nguồn cộng tác viên" meta:resourcekey="lblGridColumnSumSourceCTV"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                       <%# Eval("SumBySource2")%> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign = "Center" />  
                    <HeaderStyle />        
                </asp:TemplateField>
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnSumView" runat="server" Text="Lượt xem" meta:resourcekey="lblGridColumnSumView"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                       <%# Eval("SumByView1")%>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Center" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnSumViewSame" runat="server" Text="Lượt xem cùng kỳ" meta:resourcekey="lblGridColumnSumViewSame"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("SumByView2")%>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Center" />
                    <HeaderStyle />          
                </asp:TemplateField> 
            </Columns>
        </asp:GridView>
    </div>
   </div>
    </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnXuatExcel" />
            <asp:AsyncPostBackTrigger ControlID ="txtDateFrom" EventName ="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID ="txtDateTo" EventName ="TextChanged" />
        </Triggers>
        
    </asp:UpdatePanel>
</asp:Content>

