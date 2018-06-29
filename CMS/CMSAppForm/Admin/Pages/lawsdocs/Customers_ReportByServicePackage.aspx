<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="Customers_ReportByServicePackage.aspx.cs" Inherits="Admin_Pages_lawsdocs_Customers_ReportByServicePackage" %>
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
            <td style="width:90px; white-space:nowrap;">
                <asp:Label ID="lblDateFrom" runat="server" meta:resourcekey="lblDateFrom" 
                    Text="Từ ngày:"></asp:Label>
                </td>
            <td style="width:210px">
                <asp:TextBox ID="txtDateFrom" runat="server"  CssClass="tukhoatimekiem" 
                    Width="200px"></asp:TextBox>
            </td>
            <td style="width:90px; white-space:nowrap;">
                <asp:Label ID="lblDateTo" runat="server" meta:resourcekey="lblDateTo" 
                    Text="Đến ngày"></asp:Label>
                </td>
            <td style="width:210px">
                <asp:TextBox ID="txtDateTo" runat="server"  CssClass="tukhoatimekiem" 
                    Width="200px"></asp:TextBox>
            </td>
            <td style="width:90px; white-space:nowrap;">
                <asp:Label ID="lblOrderBy" runat="server"
                    Text="Sắp xếp theo:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlOrderBy" runat="server" AutoPostBack ="true" OnSelectedIndexChanged="ddlCompare_OnSelectedIndexChanged" CssClass="userselect" Width="210px">
                    <asp:ListItem Value="SumByServicePackage desc" Selected="True">Số lượng</asp:ListItem>
                    <asp:ListItem Value="TotalByServicePackage desc">Doanh số</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        
        <tr>
            <td>
                <asp:Label ID="lblCompare" runat="server"
                    Text="So sánh với:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlCompare" runat="server" AutoPostBack="true" CssClass="userselect" Width="210px" OnSelectedIndexChanged="ddlCompare_OnSelectedIndexChanged">
                    <asp:ListItem Value="1" Selected="True">Kỳ trước đó</asp:ListItem>
                    <asp:ListItem Value="2">Năm trước</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td> <asp:Label ID="lblServices" runat="server"
                    Text="Dịch vụ:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlServices" runat="server" DataTextField="ServiceDesc" DataValueField="ServiceId" AutoPostBack="True" 
                   OnSelectedIndexChanged="ddlServices_SelectedIndexChanged" CssClass="userselect" Width="210px">
                </asp:DropDownList></td>
            <td>
            <asp:Label ID="lblServicePackages" runat="server" Text="Gói dịch vụ:" Visible="false"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlServicePackages"  Visible="false" runat="server" DataTextField="ServicePackageDesc" DataValueField="ServicePackageId" AutoPostBack="True" 
                   OnSelectedIndexChanged="ddlServicePackages_SelectedIndexChanged" CssClass="userselect" Width="210px">
                    <asp:ListItem Text="..." Value="0"></asp:ListItem>
                </asp:DropDownList>
                 <asp:LinkButton ID="btnSearch" runat="server" CssClass="timkiembutom" 
                    meta:resourcekey="btnSearch" onclick="btnSearch_Click" Text="Tìm kiếm">
                        </asp:LinkButton>&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="btnXuatExcel" runat="server" OnClick="btnXuatExcel_Click" CssClass="timkiembutom" Text="Xuất excel"> Xuất Excel</span>
                    </asp:LinkButton>&nbsp;</td>
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
        <asp:GridView ID="m_grid" DataKeyNames="ServicePackageId" runat="server" ShowHeaderWhenEmpty="True"
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
                    <asp:Label ID="lbNo" runat="server" Text='<%# Container.DataItemIndex + 1%>' 
                                     ToolTip ='<%# Eval("ServicePackageId").ToString() %>'> </asp:Label>
                    </ItemTemplate>      
                    <ItemStyle HorizontalAlign="Center" Width="3%" />  
                    <HeaderStyle Width="3%" />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnServicesName" runat="server" Text="Dịch vụ" meta:resourcekey="lblGridColumnServices"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("ServiceDesc")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnServicePackageName" runat="server" Text="Khách hàng thuê bao" meta:resourcekey="lblGridColumnServicePackageName"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("ServicePackageDesc")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left"  Width="25%" />
                </asp:TemplateField>  
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnServicePackageName" runat="server" Text="Số tài khoản<br/><i> (dùng đồng thời)</i>" ></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# ServicePackages.Static_Get(short.Parse(Eval("ServicePackageId").ToString())).ConcurrentLogin%>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Center" Width="10%" />
                </asp:TemplateField>
                 <asp:TemplateField > 
                        <HeaderTemplate>               
                        <asp:Label ID="lblGridColumnSumByServicePackage" runat="server" Text="Số lượng" meta:resourcekey="lblGridColumnSumByServicePackage"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# double.Parse( Eval("SumByServicePackage").ToString()).ToString("N0").Replace(".",",")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Right" Width="10%" />
                </asp:TemplateField>   
                 <asp:TemplateField > 
                        <HeaderTemplate>               
                        <asp:Label ID="lblGridColumnSumByServicePackage" runat="server" Text="Số lượng cùng kỳ" meta:resourcekey="lblGridColumnSumByServicePackage"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%#  double.Parse( Eval("SumByServicePackage1").ToString()).ToString("N0").Replace(".",",")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Right" Width="10%" />
                </asp:TemplateField>   
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnTotalByServicePackage" runat="server" Text="Doanh số" meta:resourcekey="lblGridColumnTotalByServicePackage"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%#  double.Parse( Eval("TotalByServicePackage").ToString()).ToString("N0").Replace(".",",")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Right" Width="10%" />
                    <HeaderStyle />          
                </asp:TemplateField>      
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnTotalByServicePackage" runat="server" Text="Doanh số cùng kỳ" meta:resourcekey="lblGridColumnTotalByServicePackage"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%#  double.Parse( Eval("TotalByServicePackage1").ToString()).ToString("N0").Replace(".",",")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Right" Width="10%" />
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

