<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="DocReport.aspx.cs" Inherits="Admin_Pages_lawsdocs_DocReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/admin/UserControls/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="uc1" %>
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
        function submitButton(event) {
            if (event.which == 13) {
                $('#<%= btnSearch.ClientID %>').click();
            }
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
                <asp:TextBox ID="txtDateFrom" runat="server" CssClass="tukhoatimekiem"
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
                    <asp:ListItem Value="0">Cùng kỳ</asp:ListItem>
                    <asp:ListItem Value="1">Năm trước</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width:10%;">
                <asp:Label ID="lblReportBy" runat="server" meta:resourcekey="lblReportBy" 
                    Text="Thống kê theo:"></asp:Label>
            </td>
            <td>
                 <asp:DropDownList ID="ddlReportBy" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="OrderByDesc" DataValueField="OrderBy" 
                    onselectedindexchanged="ddlReportBy_SelectedIndexChanged" Width="250px">
                     <asp:ListItem Value="0">Loại văn bản</asp:ListItem>
                     <asp:ListItem Value="1">Cơ quan ban hành</asp:ListItem>
                     <asp:ListItem Value="2">Lĩnh vực</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width:10%;">
                <asp:Label ID="lblReviewStatus" runat="server" meta:resourcekey="lblReviewStatus" 
                    Text="Trạng thái duyệt:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlReviewStatus" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="ReviewStatusDesc" 
                    DataValueField="ReviewStatusId" 
                    onselectedindexchanged="ddlReviewStatus_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
            
            <td style="width:10%;">
                <asp:Label ID="lblOrgans" runat="server" meta:resourcekey="lblOrgans" 
                    Text="CQBH:"></asp:Label>
            </td>
            <td>
               <asp:DropDownList ID="ddlOrgans" runat="server" AutoPostBack="False" Enabled="true"
                    CssClass="userselect" DataTextField="OrganDesc" DataValueField="OrganId" 
                    onselectedindexchanged="ddlOrgans_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td style="width:10%;">
                <asp:Label ID="lblDocGroups" runat="server" 
                    meta:resourcekey="lblDocGroups" Text="Nhóm văn bản:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlDocGroups" runat="server" AutoPostBack="true" Enabled="true"
                    CssClass="userselect" onselectedindexchanged="ddlDocGroups_SelectedIndexChanged" Width="250px">
                        <asp:ListItem Value="0" Text="Tất cả"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Văn bản pháp quy"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Văn bản UBND"></asp:ListItem>
                        <asp:ListItem Value="6" Text="Công văn"></asp:ListItem>
                        <asp:ListItem Value="8" Text="VB Không có ND download"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width:10%;">
                <asp:Label ID="lblDocTypes" runat="server" meta:resourcekey="lblDocTypes" 
                    Text="Loại VB:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlDocTypes" runat="server" AutoPostBack="False" Enabled="false"
                    CssClass="userselect" DataTextField="DocTypeDesc" DataValueField="DocTypeId" 
                    onselectedindexchanged="ddlDocTypes_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
                
            </td>
        </tr>
        <tr>
            
            <td style="width:10%;">
                <asp:Label ID="lblDataSources" runat="server" meta:resourcekey="lblDataSources" 
                    Text="Nguồn:"></asp:Label>
            </td>
            <td>
                 <asp:DropDownList ID="ddlDataSources" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="DataSourceDesc" 
                    DataValueField="OrderBy" 
                    onselectedindexchanged="ddlDataSources_SelectedIndexChanged" Width="250px">
                     <asp:ListItem Value="0">Tất cả</asp:ListItem>
                     <asp:ListItem Value="1">Luật Việt Nam</asp:ListItem>
                     <asp:ListItem Value="2">Đối tác</asp:ListItem>
                     <asp:ListItem Value="3">Cộng tác viên</asp:ListItem>
                </asp:DropDownList>
            </td>

            <td style="width:10%;">
                <asp:Label ID="lblFields" runat="server" meta:resourcekey="lblFields" 
                    Text="Lĩnh vực:"></asp:Label>
            </td>
            <td>
                 <asp:DropDownList ID="ddlFields" runat="server" AutoPostBack="False" Enabled="true"
                    CssClass="userselect" DataTextField="FieldDesc" DataValueField="FieldId" 
                    onselectedindexchanged="ddlFields_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
                &nbsp;<asp:Button ID="btnSearch" runat="server" CssClass="timkiembutom" 
                    meta:resourcekey="btnSearch" onclick="btnSearch_Click" Text="Tìm kiếm">
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
            <asp:Label ID="lblTotalText" runat="server" Text="Tổng:" meta:resourcekey="lblTotalText"></asp:Label>
        </span>
        <asp:Label ID="lblTong" runat="server" Text="" CssClass="tieudetongcong2"></asp:Label> 
        &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="btnXuatExcel" runat="server" OnClick="btnXuatExcel_Click" CssClass="timkiembutom" Text="Xuất excel"> Xuất Excel</span>
                    </asp:LinkButton>
	</div>
	<div class="chucnangright">
	</div>
    <div style="text-align:left; width:50%; float:right"><asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                            <ProgressTemplate><img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...</ProgressTemplate>
                            </asp:UpdateProgress></div>
	<div class="clear5px"></div>
    <div class="contenbangdulieu" runat="server" id="tblbydoctype">
    
        <asp:GridView ID="m_grid_doctype" DataKeyNames="DocTypeId" runat="server" ShowHeaderWhenEmpty="True"
            AutoGenerateColumns="False" OnRowDataBound = "m_grid_doctype_RowDataBound"
            Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" PageSize="50" ShowFooter="true" >
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
                    <asp:Label ID="lbNo" runat="server" Text='<%# Container.DataItemIndex +1 %>' 
                                     ToolTip ='<%# Eval("DocTypeId").ToString() %>'> </asp:Label>
                    </ItemTemplate>      
                    <ItemStyle HorizontalAlign="Center" Width="3%" />  
                    <HeaderStyle Width="3%" />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnDocTypeName" runat="server" Text="Loại văn bản" meta:resourcekey="lblGridColumnDocTypeName"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("DocTypeName")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                </asp:TemplateField> 
                 <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnCustomerSumByStatus1" runat="server" Text="Đã duyệt" meta:resourcekey="lblGridColumnCustomerSumByStatus1"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("SumByStatus1")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                </asp:TemplateField>   
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnSumByStatus2" runat="server" Text="Chờ duyệt" meta:resourcekey="lblGridColumnSumByStatus2"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("SumByStatus2")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
                </asp:TemplateField>      
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnSumByStatus3" runat="server" Text="Xuất bản lại" meta:resourcekey="lblGridColumnSumByStatus3"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                       <%# Eval("SumByStatus3")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnSumBySource1" runat="server" Text="Nguồn LVN" meta:resourcekey="lblGridColumnSumBySource1"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <%# Eval("SumBySource1")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign = "Left" />  
                    <HeaderStyle/>        
                </asp:TemplateField>
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnTotalSumBySource2" runat="server" Text="Nguồn đối tác" meta:resourcekey="lblGridColumnTotalSumBySource2"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                       <%# Eval("SumBySource2")%> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign = "Left" />  
                    <HeaderStyle />        
                </asp:TemplateField>
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnSumBySource3" runat="server" Text="Nguồn cộng tác viên" meta:resourcekey="lblGridColumnSumBySource3"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                       <%# Eval("SumBySource3")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnSumByDownload1" runat="server" Text="Lượt tải" meta:resourcekey="lblGridColumnSumByDownload1"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                       <%# Eval("SumByDownload1")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnSumByDownload2" runat="server" Text="Lượt tải cùng kỳ" meta:resourcekey="lblGridColumnSumByDownload2"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                       <%# Eval("SumByDownload2")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
                </asp:TemplateField> 
            </Columns>
        </asp:GridView>
    </div>
        <div class="contenbangdulieu" runat="server" id="tblbyOrgan">
        <asp:GridView ID="m_grid_Organ" DataKeyNames="OrganId" runat="server" ShowHeaderWhenEmpty="True"
            AutoGenerateColumns="False" OnRowDataBound = "m_grid_Organ_OnRowDataBound"
            Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" PageSize="50" ShowFooter="true">
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
                    <asp:Label ID="lbNo" runat="server" Text='<%# Container.DataItemIndex +1 %>' 
                                     ToolTip ='<%# Eval("OrganId").ToString() %>'> </asp:Label>
                    </ItemTemplate>      
                    <ItemStyle HorizontalAlign="Center" Width="3%" />  
                    <HeaderStyle Width="3%" />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnOrganName" runat="server" Text="Lĩnh vực văn bản" meta:resourcekey="lblGridColumnOrganName"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("OrganName")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                </asp:TemplateField> 
                 <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnCustomerSumByStatus1" runat="server" Text="Đã duyệt" meta:resourcekey="lblGridColumnCustomerSumByStatus1"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("SumByStatus1")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                </asp:TemplateField>   
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnSumByStatus2" runat="server" Text="Chờ duyệt" meta:resourcekey="lblGridColumnSumByStatus2"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("SumByStatus2")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
                </asp:TemplateField>      
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnSumByStatus3" runat="server" Text="Xuất bản lại" meta:resourcekey="lblGridColumnSumByStatus3"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                       <%# Eval("SumByStatus3")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnSumBySource1" runat="server" Text="Nguồn LVN" meta:resourcekey="lblGridColumnSumBySource1"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <%# Eval("SumBySource1")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign = "Left" />  
                    <HeaderStyle/>        
                </asp:TemplateField>
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnTotalSumBySource2" runat="server" Text="Nguồn đối tác" meta:resourcekey="lblGridColumnTotalSumBySource2"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                       <%# Eval("SumBySource2")%> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign = "Left" />  
                    <HeaderStyle />        
                </asp:TemplateField>
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnSumBySource3" runat="server" Text="Nguồn cộng tác viên" meta:resourcekey="lblGridColumnSumBySource3"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                       <%# Eval("SumBySource3")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnSumByDownload1" runat="server" Text="Lượt tải" meta:resourcekey="lblGridColumnSumByDownload1"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                       <%# Eval("SumByDownload1")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnSumByDownload2" runat="server" Text="Lượt tải cùng kỳ" meta:resourcekey="lblGridColumnSumByDownload2"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                       <%# Eval("SumByDownload2")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
                </asp:TemplateField> 
            </Columns>
        </asp:GridView>
    </div>
        <div class="contenbangdulieu" runat="server" id="tblbyField">
        <asp:GridView ID="m_grid_field" DataKeyNames="FieldId" runat="server" ShowHeaderWhenEmpty="True"
            AutoGenerateColumns="False" OnRowDataBound = "m_grid_Field_OnRowDataBound"
            Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" PageSize="50" ShowFooter="true">
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
                                     ToolTip ='<%# Eval("FieldId").ToString() %>'> </asp:Label>
                    </ItemTemplate>      
                    <ItemStyle HorizontalAlign="Center" Width="3%" />  
                    <HeaderStyle Width="3%" />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnFieldName" runat="server" Text="Lĩnh vực hoạt động" meta:resourcekey="lblGridColumnFieldName"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("FieldName")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                </asp:TemplateField> 
                 <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnCustomerSumByStatus1" runat="server" Text="Đã duyệt" meta:resourcekey="lblGridColumnCustomerSumByStatus1"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("SumByStatus1")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                </asp:TemplateField>   
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnSumByStatus2" runat="server" Text="Chờ duyệt" meta:resourcekey="lblGridColumnSumByStatus2"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("SumByStatus2")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
                </asp:TemplateField>      
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnSumByStatus3" runat="server" Text="Xuất bản lại" meta:resourcekey="lblGridColumnSumByStatus3"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                       <%# Eval("SumByStatus3")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnSumBySource1" runat="server" Text="Nguồn LVN" meta:resourcekey="lblGridColumnSumBySource1"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <%# Eval("SumBySource1")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign = "Left" />  
                    <HeaderStyle/>        
                </asp:TemplateField>
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnTotalSumBySource2" runat="server" Text="Nguồn đối tác" meta:resourcekey="lblGridColumnTotalSumBySource2"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                       <%# Eval("SumBySource2")%> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign = "Left" />  
                    <HeaderStyle />        
                </asp:TemplateField>
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnSumBySource3" runat="server" Text="Nguồn cộng tác viên" meta:resourcekey="lblGridColumnSumBySource3"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                       <%# Eval("SumBySource3")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnSumByDownload1" runat="server" Text="Lượt tải" meta:resourcekey="lblGridColumnSumByDownload1"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                       <%# Eval("SumByDownload1")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnSumByDownload2" runat="server" Text="Lượt tải cùng kỳ" meta:resourcekey="lblGridColumnSumByDownload2"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                       <%# Eval("SumByDownload2")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
                </asp:TemplateField> 
            </Columns>
        </asp:GridView>
    </div>
     <%--<div class="clear5px"></div>    
                <uc1:CustomPaging ID="CustomPaging" runat="server" />                
                <div class="clear5px"></div>   --%>
   </div>
    </ContentTemplate>
         <Triggers>
            <asp:PostBackTrigger ControlID="btnXuatExcel" />
             <asp:AsyncPostBackTrigger ControlID ="txtDateFrom" EventName ="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID ="txtDateTo" EventName ="TextChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

