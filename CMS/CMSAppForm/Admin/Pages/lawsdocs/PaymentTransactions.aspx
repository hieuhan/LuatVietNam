<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="PaymentTransactions.aspx.cs" Inherits="Admin_PaymentTransactions" %>
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

            $('a.popup').live('click', function (e) {
                var page = $(this).attr("href")
                var cdialog = $('<div id="divEdit"></div>')
                .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="98%" height="98%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 600,
                    width: 900,
                    title: $(this).attr("title"),
                    close: function (event, ui) {
                        $(this).remove();
                        //window.location = $('#<%= btnSearch.ClientID %>').attr("href");
                        //$('#<%= btnSearch.ClientID %>').click();
                    }
                });
                cdialog.dialog('open');
                e.preventDefault();
            });
             $('a.popup2').live('click', function (e) {
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
                        //window.location = $('#<%= btnSearch.ClientID %>').attr("href");
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
            SetStartup();
            $("#<%= txtDateFrom.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#<%= txtDateTo.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        }
        function submitButton(event) {
            if (event.which == 13) {
                $('#<%= btnSearch.ClientID %>').click();
            }
        }
    </script>
    <asp:UpdatePanel ID="upn_Grid" runat="server">
    <ContentTemplate>
    <div style="text-align:center"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
    <asp:Panel ID="panSearch" runat="server" DefaultButton="btnSearch" Width="100%">
    <table cellpadding="2" cellspacing="0" style="width:100%">
        <tr>
            <td width="62px">
                <asp:Label ID="lblServices" runat="server" 
                    meta:resourcekey="lblServices" Text="Dịch vụ:"></asp:Label>
            </td>
            <td width="250px">
                <asp:DropDownList ID="ddlServices" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="ServiceDesc" 
                    DataValueField="ServiceId" 
                    onselectedindexchanged="ddlServices_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
                
            </td>
            <td width="100px">
                <asp:Label ID="lblServicePackages" runat="server" 
                    meta:resourcekey="lblServicePackages" Text="Gói dịch vụ:"></asp:Label>
            </td>
            <td width="250px">
                <asp:DropDownList ID="ddlServicePackages" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="ServicePackageDesc" 
                    DataValueField="ServicePackageId" 
                    onselectedindexchanged="ddlServicePackages_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
            <td width="80px">
            </td>
            <td>
            </td>
            <td></td>
        </tr>
        <tr>
            <td width="62px">
                <asp:Label ID="lblTransactionStatus" runat="server" 
                    meta:resourcekey="lblTransactionStatus" Text="Trạng thái:"></asp:Label>
            </td>
            <td width="250px">
                <asp:DropDownList ID="ddlTransactionStatus" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="TransactionStatusDesc" 
                    DataValueField="TransactionStatusId" 
                    onselectedindexchanged="ddlTransactionStatus_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
            <td width="90px">
                <asp:Label ID="lblPaymentTypes" runat="server" 
                    meta:resourcekey="lblPaymentTypes" Text="Loại thanh toán:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlPaymentTypes" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="PaymentTypeDesc" 
                    DataValueField="PaymentTypeId" 
                    onselectedindexchanged="ddlPaymentTypes_SelectedIndexChanged" 
                    Width="250px">
                </asp:DropDownList>
            </td>
            <td></td>
            <td>
                <asp:TextBox ID="txtTransactionCode" runat="server" Visible="false" CssClass="tukhoatimekiem" 
                    Width="240px"></asp:TextBox>
                <asp:Label ID="lblTransactionCode" runat="server"  Visible="false"
                    meta:resourcekey="lblTransactionCode" Text="Mã giao dịch:"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="62px">
                <asp:Label ID="lblDateFrom" runat="server" meta:resourcekey="lblDateFrom" 
                    Text="Từ ngày:"></asp:Label>
            </td>
            <td width="250px">
                <asp:TextBox ID="txtDateFrom" runat="server" CssClass="tukhoatimekiem" 
                    Width="99px"></asp:TextBox>
                <asp:Label ID="lblDateTo" runat="server" meta:resourcekey="lblDateTo" 
                    Text="Đến"></asp:Label>
                <asp:TextBox ID="txtDateTo" runat="server" CssClass="tukhoatimekiem" 
                    Width="99px"></asp:TextBox>
            </td>
            <td >
                <asp:Label ID="lblOrderBy" runat="server" meta:resourcekey="lblOrderBy" 
                    Text="Sắp xếp:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlOrderBy" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="OrderByDesc" DataValueField="OrderBy" 
                    onselectedindexchanged="ddlOrderBy_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
            <td></td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="style9">
                <asp:Label ID="lblSearch" runat="server" meta:resourcekey="lblSearch" 
                    Text="Từ khóa:"></asp:Label>
            </td>
            <td width="250px">
                <asp:TextBox ID="txtSearch" runat="server" CssClass="tukhoatimekiem" 
                    Width="240px"></asp:TextBox>
            </td>
            <td width="62px">
                <asp:Label ID="lblValueFrom" runat="server" meta:resourcekey="lblValueFrom" 
                    Text="Giá trị từ:"></asp:Label>
            </td>
            <td width="300px">
                <asp:TextBox ID="txtAmountFrom" runat="server" CssClass="tukhoatimekiem" 
                    Width="100px"></asp:TextBox>
                <asp:Label ID="lblAmountTo" runat="server" meta:resourcekey="lblAmountTo" 
                    Text="Đến"></asp:Label>
                <asp:TextBox ID="txtAmountTo" runat="server" CssClass="tukhoatimekiem" 
                    Width="100px"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" CssClass="timkiembutom" 
                    meta:resourcekey="btnSearch" onclick="btnSearch_Click" Text="Tìm kiếm">
                    </asp:Button>
            </td>
        </tr>
    </table>
    </asp:Panel>
    <div class="clear5px"></div>
	<div class="vien"></div>
	<div class="clear5px"></div>  
    <div class="khungchucnang">
    <div class="chucnangleft">
		<span class="tieudetongcong">
            <asp:Label ID="lblTotalText" runat="server" Text="Tổng cộng:" meta:resourcekey="lblTotalText"></asp:Label>
        </span>
        <asp:Label ID="lblTong" runat="server" Text="" CssClass="tieudetongcong2"></asp:Label> 
        <%--<%=GetLocalResourceObject("PaymentTransactions").ToString()%>--%>
         &nbsp;  &nbsp; <asp:Button ID="btnXuatExcel" runat="server" Text="Xuất Excel" width="90px" CssClass="timkiembutom" OnClick="btnXuatExcel_Click"/>
        <span class="tieudetongcong">           
            <asp:Label ID="lblTotalMoney" runat="server" Text="Tổng tiền:" meta:resourcekey="lblTotalMoney"></asp:Label>
        </span>
        <asp:Label ID="lblTotalMoneys" runat="server" Text="" CssClass="tieudetongcong2"></asp:Label> VNĐ
	</div>
	<div class="chucnangright">
		<%--<a id="popup" href="PaymentTransactionsEdit.aspx"  title="<%=GetLocalResourceObject("lnkAddNew.title").ToString() %>" class="themmoi" > 
            <asp:Label ID="lblAddNew" runat="server" Text="Thêm mới" meta:resourcekey="lblAddNew"></asp:Label>
        </a>--%>
        <asp:LinkButton ID="lbDelete" runat="server" CssClass="xoatin" Text="Xóa"  Visible="false"
            meta:resourcekey="lbDelete" onclick="lbDelete_Click">
        </asp:LinkButton>
	</div>
    <div style="text-align:left; width:30%; float:right"><asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                            <ProgressTemplate><img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...</ProgressTemplate>
                            </asp:UpdateProgress></div>
	<div class="clear5px"></div>
    <div class="contenbangdulieu">
    
        <asp:GridView ID="m_grid" DataKeyNames="PaymentTransactionId" runat="server" ShowHeaderWhenEmpty="True"
            AutoGenerateColumns="False" OnRowDeleting="m_grid_RowDeleting" OnRowEditing="m_grid_RowEditing" OnRowUpdating="m_grid_RowUpdating" OnRowCancelingEdit="m_grid_RowCancelingEdit" OnRowDataBound = "m_grid_OnRowDataBound" PageSize="50"
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
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnCustomerName" runat="server" Text="Tên tài khoản" meta:resourcekey="lblGridColumnCustomerName"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lbCustomerName" runat="server" style="display:none"></asp:Label>  
                        <a class="popup" href='CustomersEdit.aspx?CustomerId=<%# Eval("CustomerId") + "&backUrl=PaymentTransactions.aspx"%>'    >                  
                      <span class="FontBoldBlack"> <%# DataBinder.Eval(Container.DataItem, "mCustomer.CustomerName")%> </span><br />
                      <span class="properties"> <%# DataBinder.Eval(Container.DataItem, "mCustomer.CustomerFullName")%> </span> </a>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" Width="150"/>
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnServiceName" runat="server" Text="Dịch vụ" meta:resourcekey="lblGridColumnServiceName"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lbServiceName" runat="server" style="display:none"></asp:Label>                        
                        <%# DataBinder.Eval(Container.DataItem, "mService.ServiceDesc")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" Width="10%"/>
                    <HeaderStyle />          
                </asp:TemplateField> 
                 <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnServicePackageName" runat="server" Text="Tên gói" meta:resourcekey="lblGridColumnServicePackageName"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lbServicePackageName" runat="server"  style="display:none"></asp:Label>                        
                        <a class="popup" title="Chi tiết gói dịch vụ" href='ServicePackagesEdit.aspx?ViewOnly=1&ServicePackageId=<%# Eval("ServicePackageId")%>'><%# Eval("mServicePackage.ServicePackageDesc")%> </a>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Center" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                 <asp:TemplateField Visible="false" > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnTransactionCode" runat="server" Text="Mã giao dịch" meta:resourcekey="lblGridColumnTransactionCode"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lbTransactionCode" runat="server" style="display:none"></asp:Label>                        
                        <%# Eval("TransactionCode")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left"/>
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
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                
                <%--<asp:TemplateField> 
                        <HeaderTemplate>                                
                        +/-
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("PlusSub") %>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Center" Width="3%"/>
                    <HeaderStyle />          
                </asp:TemplateField>--%>
   <%--              <asp:TemplateField> 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnAmount" runat="server" Text="Số tiền trả" meta:resourcekey="lblGridColumnAmount"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lbPrice" runat="server" style="display:none"></asp:Label>                        
                        <%# string.Format("{0:#,##}", Eval("mServicePackage.Price"))%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Right" Width="5%"/>
                    <HeaderStyle />          
                </asp:TemplateField>--%>
                 <%--<asp:TemplateField> 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnPromotion" runat="server" Text="Khuyến mại" meta:resourcekey="lblGridColumnPromotion"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Orders.Static_Get(int.Parse(Eval("OrderId").ToString())).CouponValue.ToString() == "0" ? "Không áp dụng" : Orders.Static_Get(int.Parse(Eval("OrderId").ToString())).CouponValue.ToString() %>   
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Right" Width="8%"/>
                    <HeaderStyle />          
                </asp:TemplateField>--%>
                 <asp:TemplateField> 
                        <HeaderTemplate>                                
                        Số tiền
                    </HeaderTemplate> 
                    <ItemTemplate> 
                            <%# String.Format(System.Globalization.CultureInfo.InvariantCulture,"{0:0,0}",double .Parse(Eval("Amount").ToString())==0?0:Eval("Amount"))=="00"?"0": String.Format(System.Globalization.CultureInfo.InvariantCulture,"{0:0,0}",double .Parse(Eval("Amount").ToString())==0?0:Eval("Amount"))%>                                      
                        <%--<%# string.Format("{0:0,0}", Eval("Amount"))%> --%>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Right" Width="5%"/>
                    <HeaderStyle />          
                </asp:TemplateField>
                 <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnTransactionStatus" runat="server" Text="Trạng thái" meta:resourcekey="lblGridColumnTransactionStatus"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate>                         
                        <span class="xuatban<%# Eval("TransactionStatusId") %>" > <%# LanguageHelpers.IsCultureVietnamese() ? TransactionStatus.Static_Get(byte.Parse(Eval("TransactionStatusId").ToString()), l_TransactionStatus).TransactionStatusDesc : TransactionStatus.Static_Get(byte.Parse(Eval("TransactionStatusId").ToString()), l_TransactionStatus).TransactionStatusName%></span><br />
                        <span style="color:Blue"><%# AccountTypes.Static_Get(byte.Parse(Eval("AccountTypeId").ToString()), l_AccountTypes).AccountTypeDesc %></span>
                        <br />
                        
                        <a style="display:<%# Eval("TransactionStatusId").ToString() == "1" && Eval("PaymentTypeId").ToString() == "3" ? "block":"none" %>" class="popup2" title="Hoàn tiền cho giao dịch này" href='PaymentransactionRefund.aspx?PaymentTransactionId=<%# Eval("PaymentTransactionId")%>'>Hoàn tiền</a>
                       
                    </ItemTemplate>
                    <ItemStyle Width="8%" HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="8%" />        
                </asp:TemplateField>
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnPaymentTypes" runat="server" Text="Hình thức thanh toán" meta:resourcekey="lblGridColumnPaymentTypes"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                    <b><%# LanguageHelpers.IsCultureVietnamese() ? ICSoft.LawDocsLib.PaymentTypes.Static_Get(byte.Parse(Eval("PaymentTypeId").ToString()), l_PaymentTypes).PaymentTypeDesc : PaymentTypes.Static_Get(byte.Parse(Eval("PaymentTypeId").ToString()), l_PaymentTypes).PaymentTypeName%></b>                               
                    <br /><%# ICSoft.LawDocsLib.TransactionTypes.Static_Get(byte.Parse(Eval("TransactionTypeId").ToString()), l_TransactionTypes).TransactionTypeDesc%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign = "Left" Wrap="false" Width="10%"/>  
                    <HeaderStyle/>        
                </asp:TemplateField>                 
                 <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnTime" runat="server" Text="Thời gian" meta:resourcekey="lblGridColumnTime"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <%# DateTimeHelpers.GetDateAndTime(Eval("CrDateTime"))%><br />
                        <%# UserHelpers.Static_Get(int.Parse(Eval("CrUserId").ToString()), l_Users, "").Username %> 
                    </ItemTemplate>
                    <ItemStyle Width="8%" HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="8%" />        
                </asp:TemplateField>
                
                 <asp:TemplateField Visible="False">
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnOperation" runat="server" Text="Thao tác" meta:resourcekey="lblGridColumnOperation"></asp:Label>
                    </HeaderTemplate> 
                     <ItemTemplate>
                         <asp:LinkButton ID="lbEditAmount" runat="server" CommandName="Edit" CausesValidation="false" Visible="True" ToolTip="Sửa số tiền giao dịch">Sửa</asp:LinkButton>                                         
                     </ItemTemplate>
                     <EditItemTemplate>
                         <asp:LinkButton ID="cmdUpdate" runat="server" CommandName="Update" CausesValidation="false" ToolTip="Cập nhật số tiền giao dịch" ValidationGroup="G3">Cập nhật</asp:LinkButton>&nbsp;&nbsp;|&nbsp;
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
    <Triggers>
        <asp:PostBackTrigger ControlID="btnXuatExcel" />
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>

