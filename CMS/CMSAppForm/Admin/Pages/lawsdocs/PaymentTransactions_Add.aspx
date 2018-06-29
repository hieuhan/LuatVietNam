<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="PaymentTransactions_Add.aspx.cs" Inherits="Admin_PaymentTransactions_Add" %>
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
         $("#<%= txtBeginTime.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
         
         $('a.popup').live('click', function (e) {
             var page = $(this).attr("href")
             var cdialog = $('<div id="divEdit"></div>')
             .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="98%" height="98%"></iframe>')
             .dialog({
                 autoOpen: false,
                 modal: true,
                 height: 400,
                 width: 600,
                 title: $(this).attr("title"),
                 close: function (event, ui) {
                     window.location = window.location;
                    }
                });
             cdialog.dialog('open');
             e.preventDefault();
         });
     });
     function InitializeRequest(sender, args) {
     }

     function EndRequest(sender, args) {
         SetStartup();
         $("#<%= txtBeginTime.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
     }
    </script>
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td colspan="3">
                <asp:Label ID="lblOfficeTypeName" runat="server" Text="Khách hàng:" meta:resourcekey="lblOfficeTypeName"></asp:Label>
                &nbsp;<asp:Label ID="lblCustomerName" runat="server" Font-Bold="true" ForeColor="Maroon" Text=""></asp:Label>
                </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblServices" runat="server" 
                    meta:resourcekey="lblServices" Text="Dịch vụ:"></asp:Label>
            &nbsp;<asp:DropDownList ID="ddlServices" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="ServiceDesc" 
                    DataValueField="ServiceId" 
                    onselectedindexchanged="ddlServices_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
                Gói:
                <asp:DropDownList ID="ddlServicePackage" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="ServicePackageDesc" 
                    DataValueField="ServicePackageId" 
                    onselectedindexchanged="ddlServicePackage_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
                <asp:CheckBox ID="chkVAT" Checked="true" AutoPostBack="true" OnCheckedChanged="chkVAT_CheckedChanged" Text="Tính VAT (10%)" runat="server" />
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Ngày bắt đầu:
                <asp:TextBox ID="txtBeginTime" runat="server" BorderStyle="Solid" CssClass="tukhoatimekiem" Font-Bold="true" ForeColor="Red" Width="100px"></asp:TextBox>
                &nbsp;Thời hạn:
                <asp:TextBox ID="txtEndTime" runat="server" BorderStyle="Solid" CssClass="tukhoatimekiem" Width="130px" Enabled="False" ReadOnly="True"></asp:TextBox>
                Số tiền:
                <asp:TextBox ID="txtAmount" runat="server" BorderStyle="Solid" CssClass="tukhoatimekiem" Width="70px"></asp:TextBox>
                VAT:
                <asp:TextBox ID="txtVAT" runat="server" BorderStyle="Solid" CssClass="tukhoatimekiem" Width="70px" />
                Tổng tiền:
                <asp:TextBox ID="txtTotal" runat="server" BorderStyle="Solid" CssClass="tukhoatimekiem" Width="70px" />
                <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="left" colspan="3"><span style="color:blue">Lưu ý: Ngày bắt đầu dùng để tính thời hạn sử dụng. Ví dụ: ngày bắt đầu là 15/8/2017 và chọn gói 1 tháng thì thời hạn sử dụng sẽ là: 15/9/2017.
                <br />Hệ thống sẽ tự động lấy ngày bắt đầu là ngày hiện tại hoặc ngày hết hạn của gói dịch vụ đang sử dụng.</span>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="3">
        <asp:LinkButton ID="btnSave" runat="server" CssClass="luuvathemmoi" 
            Text="Thực hiện giao dịch" meta:resourcekey="btnSave" ValidationGroup="G1"  onclick="btnSave_Click">
        </asp:LinkButton>
             <asp:LinkButton ID="btnBack" runat="server" CssClass="quaylai" 
                    Text="Quay lại" meta:resourcekey="btnBack" 
            onclick="btnBack_Click" >
        </asp:LinkButton>
             </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:GridView ID="m_GridService" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField > 
                            <HeaderTemplate>                                
                                <asp:Label  runat="server" Text="Dịch vụ" ></asp:Label>
                            </HeaderTemplate> 
                            <ItemTemplate>                      
                              <span class="FontBoldBlack"> <%# Services.Static_Get(short.Parse( Eval("ServiceId").ToString())).ServiceDesc%> </span>
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Left"/>
                            <HeaderStyle />          
                        </asp:TemplateField> 
                        <asp:TemplateField > 
                            <HeaderTemplate>                                
                                <asp:Label  runat="server" Text="Trạng thái" ></asp:Label>
                            </HeaderTemplate> 
                            <ItemTemplate>                
                              <span class="FontBoldBlack"> <%# Eval("StatusId").ToString() == "1" ? "Đang sử dụng" : "Hết hạn"%> </span>
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Left"/>
                            <HeaderStyle />          
                        </asp:TemplateField> 
                        <asp:BoundField DataField="BeginTime" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Bắt đầu" SortExpression="BeginTime">
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="EndTime" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Ngày hết hạn" SortExpression="EndTime">
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="RenewDateTime" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Ngày gia hạn cuối" SortExpression="RenewTime">
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Sửa ngày sử dụng">
                            <ItemTemplate>
                                <a class="popup" title="Cập nhật thời hạn sử dụng" href='CustomerServicesEdit.aspx?CustomerId=<%=CustomerId %>&ServicePackageId=<%# Eval("ServicePackageId") %>&EndTime=<%# Eval("EndTime") %>&CustomerServiceId=<%# Eval("CustomerServiceId") %>'>Sửa</a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="3">
            <asp:UpdatePanel ID="upn_Grid" runat="server">
    <ContentTemplate>
              <div class="clear5px"></div>
	<div class="vien"></div>
	<div class="clear5px"></div>  
    <div class="khungchucnang">
    <div class="chucnangleft">
		<span class="tieudetongcong">
            <asp:Label ID="lblTotalText" runat="server" Text="Tổng GD:" meta:resourcekey="lblTotalText"></asp:Label>
        </span>
        <asp:Label ID="lblTong" runat="server" Text="0" CssClass="tieudetongcong2"></asp:Label> 
         &nbsp; - &nbsp;
        <span class="tieudetongcong">           
            <asp:Label ID="lbtotalMoney" runat="server" Text="Tổng tiền:" meta:resourcekey="lbtotalMoney"></asp:Label>
        </span>
        <asp:Label ID="lblTotalMoneyALL" runat="server" Text="0" CssClass="tieudetongcong2"></asp:Label> 
	</div>
    <div style="text-align:left; width:30%; float:right"><asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                            <ProgressTemplate><img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...</ProgressTemplate>
                            </asp:UpdateProgress></div>
	<div class="clear5px"></div>
    <div class="contenbangdulieu">
    
        <asp:GridView ID="m_grid" DataKeyNames="PaymentTransactionId" runat="server" ShowHeaderWhenEmpty="True"
            AutoGenerateColumns="False" OnRowDeleting="m_grid_RowDeleting" OnRowEditing="m_grid_RowEditing" OnRowUpdating="m_grid_RowUpdating" OnRowCancelingEdit="m_grid_RowCancelingEdit" OnRowDataBound = "m_grid_OnRowDataBound" OnCellValidating="m_grid_CellValidating" PageSize="50"
            Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" >
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
                                     ToolTip ='<%# Eval("PaymentTransactionId").ToString() %>'> </asp:Label>
                    </ItemTemplate>      
                    <ItemStyle HorizontalAlign="Center" Width="3%" />  
                    <HeaderStyle Width="3%" />          
                </asp:TemplateField> 
                <asp:TemplateField Visible="false" > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnCustomerName" runat="server" Text="Tên tài khoản" meta:resourcekey="lblGridColumnCustomerName"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lbCustomerName" runat="server" style="display:none"></asp:Label>                        
                      <span class="FontBoldBlack"> <%# Eval("mCustomer.CustomerName")%> </span><br />
                      <span class="properties"> <%# Eval("mCustomer.CustomerFullName")%> </span>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left"/>
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnServiceName" runat="server" Text="Dịch vụ" meta:resourcekey="lblGridColumnServiceName"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lbServiceName" runat="server" style="display:none"></asp:Label>                        
                        <%# Eval("mService.ServiceDesc")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left"/>
                    <HeaderStyle />          
                </asp:TemplateField> 
                 <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnServicePackageName" runat="server" Text="Tên gói" meta:resourcekey="lblGridColumnServicePackageName"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lbServicePackageName" runat="server" style="display:none"></asp:Label>                        
                        <%# Eval("mServicePackage.ServicePackageDesc")%>
                        <%--<br />(<%# Eval("mServicePackage.NumMonthUse")%><asp:Label ID="lblGridColumnNumMonthFree" runat="server" Text=" tháng" meta:resourcekey="lblGridColumnNumMonthFree"></asp:Label> 
                            <asp:Label ID="lblAnd" runat="server" Text=" và " meta:resourcekey="lblAnd"></asp:Label>    
                             <%# Eval("mServicePackage.NumDayUse")%> <asp:Label ID="lblNumDayFree" runat="server" Text=" ngày" meta:resourcekey="lblNumDayFree"></asp:Label>)  --%>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Center"/>
                    <HeaderStyle />          
                </asp:TemplateField> 
                  <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnTransactionDesc" runat="server" Text="Mô tả" meta:resourcekey="lblGridColumnTransactionDesc"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lbTransactionDesc" runat="server" style="display:none"></asp:Label>                        
                        <%# Eval("TransactionDesc")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left"/>
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnMaxConcurrentLogin" runat="server" Text="Số người sử dụng" meta:resourcekey="lblGridColumnMaxConcurrentLogin"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lbMaxConcurrentLogin" runat="server" style="display:none"></asp:Label>    
                        <%# string.Format("{0:#,##}",Eval("mServicePackage.ConcurrentLogin"))%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left"/>
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnPaymentTypes" runat="server" Text="Kiểu thanh toán" meta:resourcekey="lblGridColumnPaymentTypes"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                    <span class="xuatban<%# Eval("PaymentTypeId") %>" > <%# LanguageHelpers.IsCultureVietnamese() ? ICSoft.LawDocsLib.PaymentTypes.Static_Get(byte.Parse(Eval("PaymentTypeId").ToString()), l_PaymentTypes).PaymentTypeDesc : PaymentTypes.Static_Get(byte.Parse(Eval("PaymentTypeId").ToString()), l_PaymentTypes).PaymentTypeName%></span>                               
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign = "Left" />  
                    <HeaderStyle/>        
                </asp:TemplateField>
               <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnAmount" runat="server" Text="Số tiền trả(đ)" meta:resourcekey="lblGridColumnAmount"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lbAmount" runat="server" style="display:none"></asp:Label>                        
                        <%# int.Parse(Eval("Amount").ToString())>0?string.Format("{0:#,##}", Eval("Amount")):Eval("Amount") %> 
                    </ItemTemplate>
                   <EditItemTemplate>  
                       <asp:TextBox ID="txtAmount" runat="server" Text='<%#Eval("Amount")%>' Width="80px" style="text-align: right; height: 24px; line-height: 24px; border: 2px solid #4169e1;" CausesValidation="True"></asp:TextBox>  
                   </EditItemTemplate> 
                    <ItemStyle  HorizontalAlign="Right" Width="8%"/>
                    <HeaderStyle />          
                </asp:TemplateField> 
                 <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnPrice" runat="server" Text="Giá DV(đ)" meta:resourcekey="lblGridColumnPrice"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lbPrice" runat="server" style="display:none"></asp:Label>                        
                        <%#int.Parse( Eval("mServicePackage.Price").ToString()) > 0 ? string.Format("{0:#,##}", Eval("mServicePackage.Price")):Eval("mServicePackage.Price")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Right" Width="8%"/>
                    <HeaderStyle />          
                </asp:TemplateField>
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnTransactionStatus" runat="server" Text="Trạng thái" meta:resourcekey="lblGridColumnTransactionStatus"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate>                         
                            <span class="xuatban<%# Eval("TransactionStatusId") %>" > <%# LanguageHelpers.IsCultureVietnamese() ? TransactionStatus.Static_Get(byte.Parse(Eval("TransactionStatusId").ToString()), l_TransactionStatus).TransactionStatusDesc : TransactionStatus.Static_Get(byte.Parse(Eval("TransactionStatusId").ToString()), l_TransactionStatus).TransactionStatusName%></span><br />
                             <%# DateTimeHelpers.GetDateAndTime(Eval("CrDateTime"))%><br />
                        <%# UserHelpers.Static_Get(int.Parse(Eval("CrUserId").ToString()), l_Users, "").Username %>                      
                    </ItemTemplate>
                    <ItemStyle Width="8%" HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="8%" />        
                </asp:TemplateField>  
                <asp:TemplateField>
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnOperation" runat="server" Text="Thao tác" meta:resourcekey="lblGridColumnOperation"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate>
                        
                        <asp:LinkButton ID="lbEditAmount" runat="server" CommandName="Edit" CausesValidation="false" Visible="True" ToolTip="Sửa số tiền giao dịch"><%# byte.Parse(Eval("PaymentTypeId").ToString()) != 3 ? "Sửa":"" %></asp:LinkButton>                                         
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="cmdUpdate" runat="server" CommandName="Update" CausesValidation="false" ToolTip="Cập nhật số tiền giao dịch">Cập nhật</asp:LinkButton>&nbsp;&nbsp;|&nbsp;
                        <asp:LinkButton ID="cmdCancel" runat="server" CommandName="Cancel" CausesValidation="false" ToolTip="Thoát">Thoát</asp:LinkButton>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="center" Width="6%" Wrap="false" />
                    <HeaderStyle Width="6%" />
                </asp:TemplateField>   
            </Columns>
        </asp:GridView>
    </div>
   </div>
   <div class="clear5px"></div>    
                <uc1:CustomPaging ID="CustomPaging" runat="server" />                
                <div class="clear5px"></div>   
   </div>
    </ContentTemplate>
    </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>

