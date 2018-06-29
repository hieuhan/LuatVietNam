<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="CustomersServicesReport.aspx.cs" Inherits="Admin_Pages_lawsdocs_CustomersServicesReport" %>
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

            $('a.popup').live('click', function (e) {
                var page = $(this).attr("href")
                var cdialog = $('<div id="divEdit"></div>')
                .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="98%" height="98%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 600,
                    width: 1100,
                    title: $(this).attr("title"),
                    close: function (event, ui) {
                        $(this).remove();
                        try {
                            $('#<%= btnRefresh.ClientID %>')[0].click();
                       } catch (ex) {
                       }

                    }
                });
                cdialog.dialog('open');
                e.preventDefault();
            });
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
        <tr>
            <td style="width:90px; white-space:nowrap;"> <asp:Label ID="lblServices" runat="server"
                    Text="Dịch vụ:"></asp:Label>
            </td>
            <td  style="width:240px; white-space:nowrap;">
                <asp:DropDownList ID="ddlServices" runat="server" DataTextField="ServiceDesc" DataValueField="ServiceId" AutoPostBack="True" 
                   OnSelectedIndexChanged="ddlServices_SelectedIndexChanged" CssClass="userselect" Width="230px">
                </asp:DropDownList></td>
            <td style="width:90px; white-space:nowrap;">
            <asp:Label ID="Label1" runat="server" Text="Tình trạng sử dụng:"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPaymentTypes_SelectedIndexChanged" CssClass="userselect" Width="230px">
                    <asp:ListItem Value="0">...</asp:ListItem>
                    <asp:ListItem Value="1">Còn hạn</asp:ListItem>
                    <asp:ListItem Value="2">Hết hạn</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td  style="width:90px; white-space:nowrap;"></td>
            <td></td>
            
        </tr>
        <tr>
            <td style="width:90px; white-space:nowrap;">
            <asp:Label ID="lblServicePackages" runat="server" Text="Gói dịch vụ:"></asp:Label></td>
            <td style="width:240px; white-space:nowrap;">
                <asp:DropDownList ID="ddlServicePackages" runat="server" DataTextField="ServicePackageDesc" DataValueField="ServicePackageId" AutoPostBack="True" 
                   OnSelectedIndexChanged="ddlServicePackages_SelectedIndexChanged" CssClass="userselect" Width="230px">
                    <asp:ListItem Text="..." Value="0"></asp:ListItem>
                </asp:DropDownList>
            </td>
            
            <td style="width:90px; white-space:nowrap;">
                <asp:Label ID="lblDateFrom" runat="server" meta:resourcekey="lblDateFrom" 
                    Text="Từ ngày:"></asp:Label>
                </td>
            <td style="width:240px">
                <asp:TextBox ID="txtDateFrom" runat="server" CssClass="tukhoatimekiem" Width="90px"></asp:TextBox>&nbsp;đến &nbsp;
                <asp:TextBox ID="txtDateTo" runat="server"  CssClass="tukhoatimekiem" Width="90px"></asp:TextBox>
            </td>
            
             <td>
                <asp:Label ID="lblOrderBy" runat="server" Text="Khoảng thời gian:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlOrderBy" runat="server" AutoPostBack="True"  CssClass="userselect" 
                    onselectedindexchanged="ddlOrderBy_SelectedIndexChanged" Width="200px">
                    <asp:ListItem Text="Ngày" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Tuần" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Tháng" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Năm" Value="3"></asp:ListItem>
                    <asp:ListItem Text="Tùy chỉnh" Value="4"></asp:ListItem>
                </asp:DropDownList>
            </td>

        </tr>
          <tr runat="server" id="lblDateFromTo">
              <td style="width:90px; white-space:nowrap;">
            <asp:Label ID="lblPaymentTypes" runat="server" Text="Kênh thanh toán:"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlPaymentTypes" runat="server" DataTextField="PaymentTypeDesc" DataValueField="PaymentTypeId" AutoPostBack="True" 
                   OnSelectedIndexChanged="ddlPaymentTypes_SelectedIndexChanged" CssClass="userselect" Width="230px">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lblCompare" runat="server" Text="So sánh với:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlCompare" runat="server" AutoPostBack="True" 
                    CssClass="userselect" onselectedindexchanged="ddlCompare_OnSelectedIndexChanged" Width="230px">
                    <asp:ListItem Value="1" Selected="True" Text="Kỳ trước đó"></asp:ListItem>
                    <asp:ListItem Value="2" Text="Năm trước"></asp:ListItem>
                </asp:DropDownList>
            </td>
              
            <td style="width:90px; white-space:nowrap;">
                </td>
            <td>
                &nbsp;<asp:LinkButton ID="btnSearch" runat="server" CssClass="timkiembutom" 
                    meta:resourcekey="btnSearch" onclick="btnSearch_Click" Text="Tìm kiếm">
                        </asp:LinkButton>
                 &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="btnXuatExcel" runat="server" OnClick="btnXuatExcel_Click" CssClass="timkiembutom" Text="Xuất excel"> Xuất Excel</span>
                    </asp:LinkButton><asp:LinkButton ID="btnRefresh" runat="server" CssClass="timkiembutom hidden-btn-dqs" Text="Refresh" onclick="btnRefresh_Click"></asp:LinkButton>	
            </td>
        </tr>
        
        
    </table>
    <div class="clear5px"></div>
	<div class="vien"></div>
	<div class="clear5px"></div>  
    <div class="khungchucnang">
    <div class="chucnangleft" style="width:80%;">
        <table style="width:100%; border-collapse: collapse;">
  <tr>
    <th style="border: 1px solid #ddd;">Khách thuê bao(<asp:Label  id="lblTotalCustomerServices" runat="server" Text="0" meta:resourcekey="lblTotalText"></asp:Label>)</th>
    <th style="border: 1px solid #ddd;">Tình trạng sử dụng</th> 
    <th style="border: 1px solid #ddd;">Đăng ký dịch vụ</th>
    <th style="border: 1px solid #ddd;">Doanh số: <asp:Label  id="lblTotalMoney" runat="server" Text="0" meta:resourcekey="lblTotalMoney"></asp:Label></th>
    <th style="border: 1px solid #ddd;">Kênh thanh toán</th>
    <th style="border: 1px solid #ddd;">Số lần thuê bao</th>
  </tr>
  <tr>
    <td style="border: 1px solid #ddd;padding:5px;">Online(<asp:Label ID="lblCountPayOnline" runat="server" Text="0" meta:resourcekey="lblCountPayOnline"></asp:Label>)</td>
    <td style="border: 1px solid #ddd;padding:5px;">Còn hạn(<asp:Label ID="lblCountStatusStillExpired" runat="server" Text="0" meta:resourcekey="lblCountStatusStillExpired"></asp:Label>)</td>
    <td style="border: 1px solid #ddd;padding:5px;">Tiêu chuẩn(<asp:Label ID="lblCountServicePackageTC" runat="server" Text="0" meta:resourcekey="lblCountServicePackageTC"></asp:Label>)</td>
    <td style="border: 1px solid #ddd;padding:5px; text-align:right;"><asp:Label ID="lblTotalMoneyTC" runat="server" Text="0" meta:resourcekey="lblTotalText"></asp:Label></td>
    <td style="border: 1px solid #ddd;padding:5px;">Ngân hàng(<asp:Label ID="lblCountPaymentTypeBank" runat="server" Text="0" meta:resourcekey="lblCountPaymentTypeBank"></asp:Label>)</td>
    <td style="border: 1px solid #ddd;padding:5px;">Lần 1(<asp:Label ID="lblCountNumberPayFirst" runat="server" Text="0" meta:resourcekey="lblCountNumberPayFirst"></asp:Label>)</td>
    
  </tr>
   <tr>
    <td style="border: 1px solid #ddd;padding:5px;">Trực tiếp(<asp:Label ID="lblCountPayOffline" runat="server" Text="0" meta:resourcekey="lblCountPayOffline"></asp:Label>)</td>
    <td style="border: 1px solid #ddd;padding:5px;">Sắp hết hạn(<asp:Label ID="lblCountStatusExpiringSoon" runat="server" Text="0" meta:resourcekey="lblCountStatusExpiringSoon"></asp:Label>)</td>
    <td style="border: 1px solid #ddd;padding:5px;">Nâng cao(<asp:Label ID="lblCountServicePackageNC" runat="server" Text="0" meta:resourcekey="lblCountServicePackageNC"></asp:Label>)</td>
    <td style="border: 1px solid #ddd;padding:5px; text-align:right;"><asp:Label ID="lblTotalMoneyNC" runat="server" Text="0" meta:resourcekey="lblTotalText"></asp:Label></td> 
    <td style="border: 1px solid #ddd;padding:5px;">Văn phòng INCOM(<asp:Label ID="lblCountPaymentTypeIncomOffice" runat="server" Text="0" meta:resourcekey="lblCountPaymentTypeIncomOffice"></asp:Label>)</td>
    <td style="border: 1px solid #ddd;padding:5px;">Lần 2(<asp:Label ID="lblCountNumberPaySecond" runat="server" Text="0" meta:resourcekey="lblCountNumberPaySecond"></asp:Label>)</td>
  </tr>
  <tr>
    <td style="border: 1px solid #ddd;padding:5px;"></td>
    <td style="border: 1px solid #ddd;padding:5px;"></td>
    <td style="border: 1px solid #ddd;padding:5px;">Tiếng Anh(<asp:Label ID="lblCountServicePackageTA" runat="server" Text="0" meta:resourcekey="lblCountServicePackageTA"></asp:Label>)</td>
    <td style="border: 1px solid #ddd;padding:5px; text-align:right;"><asp:Label ID="lblTotalMoneyTA" runat="server" Text="0" meta:resourcekey="lblTotalText"></asp:Label></td> 
    <td style="border: 1px solid #ddd;padding:5px;">Thẻ LuatVN(<asp:Label ID="lblCountPaymentTypeLuatVNCard" runat="server" Text="0" meta:resourcekey="lblCountPaymentTypeLuatVNCard"></asp:Label>)</td>
    <td style="border: 1px solid #ddd;padding:5px;">Lần 3(<asp:Label ID="lblCountNumberPay3rd" runat="server" Text="0" meta:resourcekey="lblCountNumberPay3rd"></asp:Label>)</td>
   
  </tr>
  <tr>
    <td style="border: 1px solid #ddd;padding:5px;"></td>
    <td style="border: 1px solid #ddd;padding:5px;"></td>
    <td style="border: 1px solid #ddd;padding:5px;">Nghiệp vụ(<asp:Label ID="lblCountServicePackageNV" runat="server" Text="0" meta:resourcekey="lblCountServicePackageNV"></asp:Label>)</td>
    <td style="border: 1px solid #ddd;padding:5px; text-align:right;"><asp:Label ID="lblTotalMoneyNV" runat="server" Text="0" meta:resourcekey="lblTotalText"></asp:Label></td>
    <td style="border: 1px solid #ddd;padding:5px;"></td>
    <td style="border: 1px solid #ddd;padding:5px;">Lần 4(<asp:Label ID="lblCountNumberPay4rd" runat="server" Text="0" meta:resourcekey="lblCountNumberPay4rd"></asp:Label>)</td>
  </tr>
  
</table>

		<span class="tieudetongcong">
            <asp:Label ID="lblTotalText" runat="server" Text="Tổng cộng:" meta:resourcekey="lblTotalText"></asp:Label>
        </span>
        <asp:Label ID="lblTong" runat="server" Text="" CssClass="tieudetongcong2"></asp:Label> 

	</div>
	<div class="chucnangright">
	</div>
    <div style="text-align:left; width:50%; float:right"><asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                            <ProgressTemplate><img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...</ProgressTemplate>
                            </asp:UpdateProgress></div>
	<div class="clear5px"></div>
    <div class="contenbangdulieu">
    
        <asp:GridView ID="m_grid" DataKeyNames="CustomerId" runat="server" ShowHeaderWhenEmpty="True"
            AutoGenerateColumns="False" OnRowDataBound = "m_grid_OnRowDataBound"
            Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" PageSize="50" >
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
                    <asp:Label ID="lbNo" runat="server" Text='<%# Container.DataItemIndex + (CustomPaging.PageIndex - 1) * m_grid.PageSize + 1%>' 
                                     ToolTip ='<%# Eval("CustomerId").ToString() %>'> </asp:Label>
                    </ItemTemplate>      
                    <ItemStyle HorizontalAlign="Center" Width="3%" />  
                    <HeaderStyle Width="3%" />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnCustomerName" runat="server" Text="Tài khoản" meta:resourcekey="lblGridColumnCustomerName"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("CustomerName")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                </asp:TemplateField> 
                 <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnCustomerFullName" runat="server" Text="Họ và tên" meta:resourcekey="lblGridColumnCustomerFullName"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("CustomerFullName")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                </asp:TemplateField>   
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnServicePackageId" runat="server" Text="Dịch vụ" ></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <span class="xuatban<%# Eval("ServicePackageId") %>" >
                                    <%# Eval("ServiceDesc") %>
                                </span> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Center" />
                    <HeaderStyle />          
                </asp:TemplateField>      
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnPaymentTypeId" runat="server" Text="Hình thức mua" meta:resourcekey="lblGridColumnPaymentTypeId"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <span class="xuatban<%# Eval("PaymentTypeId") %>" >
                                    <%# (int.Parse(Eval("PaymentTypeId").ToString()) ==5? "Trực tiếp": (int.Parse(Eval("PaymentTypeId").ToString()) >0? "Online": "")) %>
                                </span>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Center" />
                    <HeaderStyle />          
                </asp:TemplateField>
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnPaymentTypeId1" runat="server" Text="Kênh thanh toán" meta:resourcekey="lblGridColumnPaymentTypeId1"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <span class="xuatban<%# Eval("PaymentTypeId") %>" >
                                    <%# PaymentTypes.Static_Get(byte.Parse(Eval("PaymentTypeId").ToString()), l_PaymentTypes).PaymentTypeDesc %>
                                </span> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Center" />
                    <HeaderStyle />          
                </asp:TemplateField>   
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnMaxConcurrentLogin" runat="server" Text="Số tài khoản<br/><i> (dùng đồng thời)</i>" meta:resourcekey="lblGridColumnMaxConcurrentLogin"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate>
                        <%# byte.Parse(Eval("MaxConcurrentLogin").ToString()) <= 3 ? "1 đến 3" : Eval("MaxConcurrentLogin")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Center" />
                </asp:TemplateField> 
                 <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridServicePackageId1" runat="server" Text="Thời gian<br/>thuê bao" meta:resourcekey="lblGridColumnServicePackageId1"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <span class="xuatban<%# Eval("ServicePackageId") %>" >
                            <%# ServicePackages_GetServicePackageNumMonthUse(Convert.ToInt16(Eval("ServicePackageId").ToString())) %>
                        </span> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Center" />
                    <HeaderStyle />          
                </asp:TemplateField>
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnBeginTime" runat="server" Text="Ngày đăng ký" meta:resourcekey="lblGridColumnBeginTime"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                       <%# DateTimeHelpers.GetDateAndTime(Eval("BeginTime"))%>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Center" />
                    <HeaderStyle Width="8%" />          
                </asp:TemplateField> 
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnEndTime" runat="server" Text="Hạn sử dụng" meta:resourcekey="lblGridColumnEndTime"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <%# DateTimeHelpers.GetDateAndTime(Eval("EndTime"))%>
                    </ItemTemplate>
                    <ItemStyle Width="8%" HorizontalAlign = "Center" Wrap="false" />  
                    <HeaderStyle Width="8%" />        
                </asp:TemplateField>
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnEndTime" runat="server" Text="Tình trạng sử dụng" meta:resourcekey="lblGridColumnEndTime"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <%#(DateTimeHelpers.GetDateAndTime(Eval("EndTime")).Length>0? (DateTime.Compare(DateTime.ParseExact(DateTimeHelpers.GetDateAndTime(Eval("EndTime")), "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture),DateTime.Now.Date)<0?"Hết hạn":"Còn hạn"):"")%>
                    </ItemTemplate>
                    <ItemStyle Width="8%" HorizontalAlign = "Left" Wrap="false" />  
                    <HeaderStyle Width="8%" />        
                </asp:TemplateField>
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnTotalPaymentMoney" runat="server" Text="Tổng tiền" meta:resourcekey="lblGridColumnTotalPaymentMoney"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                       <%# int.Parse(Eval("TotalPaymentMoney").ToString()).ToString("N0")%> 
                    </ItemTemplate>
                    <ItemStyle Width="8%" HorizontalAlign = "Center" Wrap="false" />  
                    <HeaderStyle Width="8%" />        
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Lịch sử<br/>thuê bao">
                    <ItemStyle HorizontalAlign="center"  Wrap="false" />
                    <HeaderStyle Width="6%" />
                    <ItemTemplate>
                        <a title="Lịch sử thuê bao" href='CustomersServicesReport_HistoryPay.aspx?id=<%# Eval("CustomerId") %>' class="iconadmsua popup" ></a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
     <div class="clear5px"></div>    
    <uc1:CustomPaging ID="CustomPaging" runat="server" />                
    <div class="clear5px"></div>   
   </div>
    </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnXuatExcel" />
            <asp:AsyncPostBackTrigger ControlID ="txtDateFrom" EventName ="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID ="txtDateTo" EventName ="TextChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

