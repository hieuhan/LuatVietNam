<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="Customers_ReportMember.aspx.cs" Inherits="Admin_Pages_lawsdocs_Customers_ReportMember" %>
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
        $('a#popup').live('click', function (e) {
            var page = $(this).attr("href")
            var cdialog = $('<div id="divEdit"></div>')
            .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="98%" height="98%"></iframe>')
            .dialog({
                autoOpen: false,
                modal: true,
                height: 600,
                width: 800,
                title: $(this).attr("title"),
                close: function (event, ui) {
                    $(this).remove();
                    try {
                        $('#<%= btnSearch.ClientID %>')[0].click();
                        } catch (ex) {
                        }

                    }
                });
                cdialog.dialog('open');
                e.preventDefault();
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
            <td>
                <asp:Label ID="lblOrderBy" runat="server" meta:resourcekey="lblOrderBy" 
                    Text="Khoảng thời gian:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlDatetime" runat="server" AutoPostBack="True" 
                    CssClass="userselect" OnSelectedIndexChanged="ddlDatetime_SelectedIndexChanged" Width="250px">
                    <asp:ListItem Value="0" Selected="True">Kỳ trước</asp:ListItem>
                    <asp:ListItem Value="1">Năm trước</asp:ListItem>
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
        <style>
            table#table {
                border-collapse: separate;
                border-spacing: 0;
            }

                table#table th, td {
                    padding: 10px 15px;
                }

                table#table thead {
                    background: #395870;
                    color: #fff;
                }

                table#table tbody tr:nth-child(even) {
                    background: #f0f0f2;
                }

                table#table td {
                    border-bottom: 1px solid #cecfd5;
                    border-right: 1px solid #cecfd5;
                }

                    table#table td:first-child {
                        border-left: 1px solid #cecfd5;
                    }
        </style>
    </table>
    <div class="clear5px"></div>
	<div class="vien"></div>
	<div class="clear5px"></div>  
    <div class="khungchucnang">
    <div class="chucnangleft">
		<span>
            <asp:Label ID="lblTotalText" runat="server" Text="Số thành viên:" meta:resourcekey="lblTotalText"></asp:Label>
        </span>
        <asp:Label ID="lblTong" runat="server" Text="" CssClass="tieudetongcong2"></asp:Label> 
        <table style="width:100%" id="table">
  <tr>
    <th>Sử dụng dịch vụ</th>
    <th>Đã đăng ký</th> 
    <th>Quan tâm dịch vụ</th>
    <th>Tình trạng</th>
  </tr>
  <tr>
    <td><asp:Label ID="lblCountFreeService" runat="server" Text="Miễn phí:" meta:resourcekey="lblTotalText"></asp:Label></td>
    <td><asp:Label ID="lblCountRegisterNewsletter" runat="server" Text="Đã đăng ký:" meta:resourcekey="lblTotalText"></asp:Label></td> 
    <td><asp:Label ID="lblCountByNewsletterGroupId1" runat="server" Text="Đã đăng ký:" meta:resourcekey="lblTotalText"></asp:Label></td>
    <td><asp:Label ID="lblCountByStatusId1" runat="server" Text="Tình trạng:" meta:resourcekey="lblTotalText"></asp:Label></td>
  </tr>
  <tr>
      <td><asp:Label ID="lblCountTrialService" runat="server" Text="Dùng thử:" meta:resourcekey="lblTotalText"></asp:Label></td>
      <td><asp:Label ID="lblCountByDocGroupId1" runat="server" Text="VB quan tâm:" meta:resourcekey="lblTotalText"></asp:Label></td> 
      <td><asp:Label ID="lblCountByNewsletterGroupId2" runat="server" Text="Tiếng Anh:" meta:resourcekey="lblTotalText"></asp:Label></td> 
      <td><asp:Label ID="lblCountByStatusId2" runat="server" Text="Tình trạng:" meta:resourcekey="lblTotalText"></asp:Label></td>
  </tr>
  <tr>
      <td></td>
      <td><asp:Label ID="lblCountByDocGroupId2" runat="server" Text="TCVN quan tâm:" meta:resourcekey="lblTotalText"></asp:Label></td> 
      <td><asp:Label ID="lblCountByNewsletterGroupId3" runat="server" Text="Cả hai:" meta:resourcekey="lblTotalText"></asp:Label></td> 
      <td></td>
  </tr>
  <tr>
      <td></td>
      <td><asp:Label ID="lblCountByDocGroupId3" runat="server" Text="TTHC quan tâm:" meta:resourcekey="lblTotalText"></asp:Label></td> 
      <td></td> 
      <td></td>
  </tr>
</table>
	</div>
	<div class="chucnangright">
	</div>
    <div style="text-align:left; width:50%; float:right"><asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                            <ProgressTemplate><img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...</ProgressTemplate>
                            </asp:UpdateProgress></div>
	<div class="clear5px"></div>
    <div class="contenbangdulieu" runat="server" id="tblbydoctype">
        <asp:GridView ID="m_grid" DataKeyNames="CustomerId" runat="server" ShowHeaderWhenEmpty="True"
            AutoGenerateColumns="False" 
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
                    <asp:Label ID="lbNo" runat="server" Text='<%# Container.DataItemIndex + (CustomPaging.PageIndex - 1) * m_grid.PageSize + 1%>' 
                                     ToolTip ='<%# Eval("CustomerId").ToString() %>'> </asp:Label>
                    </ItemTemplate>      
                    <ItemStyle HorizontalAlign="Center" Width="3%" />  
                    <HeaderStyle Width="3%" />          
                </asp:TemplateField> 
                 <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnCustomerFullName" runat="server" Text="Họ tên" meta:resourcekey="lblGridColumnCustomerFullName"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <span id="CustomerName" class="properties ">T.khoản: </span><a id="popup" href='CustomersEdit.aspx?CustomerId=<%# Eval("CustomerId") %>'><span class="FontBoldMaroon "><%# Eval("CustomerName")%></span></a><br />
                        <span id="CustomerFullName" class="properties ">H.tên: </span><a id="popup" href='CustomersEdit.aspx?CustomerId=<%# Eval("CustomerId")%>'><%# Eval("CustomerFullName")%> </a>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" Width="10%" />
                </asp:TemplateField>      
                <asp:TemplateField > 
                        <HeaderTemplate> 
                        <asp:Label ID="lblGridColumnCustomerMail" runat="server" Text="Email" meta:resourcekey="lblGridColumnCustomerMail"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                       <span id="SĐT" class="properties">SĐT: </span> <%# Eval("CustomerMobile")%> <br />
                       <span id="Email" class="properties">Email: </span><%# Eval("CustomerMail")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnRegisterNewsletter" runat="server" Text="Nhận bản tin" meta:resourcekey="lblGridColumnRegisterNewsletter"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <%# Convert.ToInt16(Eval("RegisterNewsletter")) > 0 ? "x" : ""%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign = "Center" />  
                    <HeaderStyle/>        
                </asp:TemplateField>
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnVBQT" runat="server" Text="Văn bản quan tâm" meta:resourcekey="lblGridColumnVBQT"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                       <%# Convert.ToInt32(Eval("VBQT")) > 0 ? "x" : ""%> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign = "Center" />  
                    <HeaderStyle />        
                </asp:TemplateField>
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnNewsletterGroupId" runat="server" Text="Quan tâm dịch vụ" meta:resourcekey="lblGridColumnNewsletterGroupId"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("ApplicationId").ToString() == "3" ? "Cả hai" : Eval("ApplicationId").ToString() == "2" ? "Tiếng Anh" :  Eval("ApplicationId").ToString() == "1" ? "Tiếng Việt" : "" %>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnTTHC" runat="server" Text="TTHC quan tâm" meta:resourcekey="lblGridColumnTTHC"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                       <%# Convert.ToInt32(Eval("TTHC")) > 0 ? "x" : ""%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Center" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnTCVN" runat="server" Text="TCVN quan tâm" meta:resourcekey="lblGridColumnTCVN"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                       <%# Convert.ToInt32(Eval("TCVN")) > 0 ? "x" : ""%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Center" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnServicePackageId" runat="server" Text="Sử dụng dịch vụ" ></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                         <%# LanguageHelpers.IsCultureVietnamese() ? Services.Static_Get(byte.Parse(Eval("ServiceId").ToString()), l_Services).ServiceDesc : Services.Static_Get(byte.Parse(Eval("ServiceId").ToString()), l_Services).ServiceName%>
                        
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
                </asp:TemplateField>  
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnServiceId" runat="server" Text="Gói dịch vụ"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                       <%# ICSoft.LawDocsLib.ServicePackages.Static_Get(short.Parse(Eval("ServicePackageId").ToString())).ServicePackageDesc%>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
                </asp:TemplateField>
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnEndTime" runat="server" Text="Thời hạn (Tháng)" meta:resourcekey="lblGridColumnEndTime"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                       <%# ServicePackages_GetServicePackageNumMonthUse(Convert.ToInt16(Eval("ServicePackageId").ToString()))%>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Center" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnLastLoginTime" runat="server" Text="Hoạt động gần đây" meta:resourcekey="lblGridColumnLastLoginTime"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                       <%# DateTimeHelpers.GetDateAndTime(Eval("LastLoginTime"))%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
                </asp:TemplateField>
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnStatusId" runat="server" Text="Ngày đăng ký & Trạng thái" meta:resourcekey="lblGridColumnStatusId"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <span id="CrDateTime" class="properties">Ngày ĐK: </span>
                         <%# DateTimeHelpers.GetDateAndTime(Eval("CrDateTime")) %> <br />
                       <span style="width:100%;" class="xuatban<%#Eval("StatusId") %>"><%# LanguageHelpers.IsCultureVietnamese() ? Status.Static_Get(byte.Parse(Eval("StatusId").ToString()), l_Status).StatusDesc : Status.Static_Get(byte.Parse(Eval("StatusId").ToString()), l_Status).StatusName%></span>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
                </asp:TemplateField>
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnProvinceId" runat="server" Text="Địa chỉ" meta:resourcekey="lblGridColumnAddress"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                         <%# Eval("Address")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" Width="10%"/>
                    <HeaderStyle />          
                </asp:TemplateField>
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnProvinceId" runat="server" Text="Tỉnh T.Phố" meta:resourcekey="lblGridColumnProvinceId"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                       <%# LanguageHelpers.IsCultureVietnamese() ? Provinces.Static_Get(byte.Parse(Eval("ProvinceId").ToString()), l_Provinces).ProvinceDesc : Provinces.Static_Get(byte.Parse(Eval("ProvinceId").ToString()), l_Provinces).ProvinceName%>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
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

