<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="PaymentTransactionsForPartner.aspx.cs" Inherits="Admin_PaymentTransactionsForPartner" %>
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
                    height: 400,
                    width: 600,
                    title: $(this).attr("title"),
                    close: function (event, ui) {
                        $(this).remove();
                        //window.location = $('#<%= btnSearch.ClientID %>').attr("href");
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
    <table cellpadding="2" cellspacing="0" style="width:100%">
        <tr style="display:none;">
            <td style="width:62px; white-space:nowrap;">Site:
            </td>
            <td style="width:250px"><asp:DropDownList ID="ddlSite" runat="server" DataTextField="SiteDesc" 
                            DataValueField="SiteId" Width="250px" CssClass="userselect"
                            AutoPostBack="True" onselectedindexchanged="ddlSite_SelectedIndexChanged">
                        </asp:DropDownList>
            </td>
            <td style="width:62px; white-space:nowrap;">Kiểu giao dịch:</td>
            <td><asp:DropDownList ID="ddlTranType" runat="server" DataTextField="TransactionTypeDesc" 
                            DataValueField="TransactionTypeId" Width="250px" CssClass="userselect"
                            AutoPostBack="True" onselectedindexchanged="ddlTranType_SelectedIndexChanged">
                        </asp:DropDownList>
            </td>
            <td>    
                Partner:
            </td>
            <td>
                <asp:DropDownList ID="ddlPartner" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="PartnerDesc" 
                    DataValueField="PartnerId" 
                    onselectedindexchanged="ddlPartner_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="62px">
                <asp:Label ID="lblServices" runat="server" 
                    meta:resourcekey="lblServices" Text="Dịch vụ:"></asp:Label>
            </td>
            <td width="250px">
                <asp:DropDownList ID="ddlServices" runat="server" CssClass="userselect" Width="250px">
                    <asp:ListItem Selected="True" Text="Tra cứu Tiếng Anh" Value="17">Tra cứu Tiếng Anh </asp:ListItem>
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
            <td width="80px"> <asp:Label runat="server" Visible="false"> Loại tài khoản:</asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlAccountType" runat="server" AutoPostBack="True"   Visible="false"
                    CssClass="userselect" DataTextField="AccountTypeDesc" 
                    DataValueField="AccountTypeId" 
                    onselectedindexchanged="ddlAccountType_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
            <td></td>
        </tr>
        <tr style="display:none;">
            <td width="62px" >
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
                    Width="100px"></asp:TextBox>
                <asp:Label ID="lblDateTo" runat="server" meta:resourcekey="lblDateTo" 
                    Text="Đến"></asp:Label>
                <asp:TextBox ID="txtDateTo" runat="server" CssClass="tukhoatimekiem" 
                    Width="100px"></asp:TextBox>
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
            <td><asp:Label runat="server" Visible="false"> Cộng/trừ:</asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlPlusSub" runat="server" AutoPostBack="True"  Visible="false"
                    CssClass="userselect" onselectedindexchanged="ddlPlusSub_SelectedIndexChanged" Width="250px">
                    <asp:ListItem Text="..." Value="."></asp:ListItem>
                    <asp:ListItem Text="Cộng tiền" Value="+"></asp:ListItem>
                    <asp:ListItem Text="Trừ tiền" Value="-"></asp:ListItem>
                </asp:DropDownList>
                
                <asp:Button ID="btnSearch" runat="server" CssClass="timkiembutom" 
                            meta:resourcekey="btnSearch" onclick="btnSearch_Click" Text="Tìm kiếm">
                </asp:Button>
            </td>
        </tr>
        <tr>
            <td class="style9">
                <asp:Label ID="lblSearch" runat="server" meta:resourcekey="lblSearch"  Visible="false"
                    Text="Từ khóa:"></asp:Label>
            </td>
            <td width="250px">
                <asp:TextBox ID="txtSearch" runat="server" CssClass="tukhoatimekiem" Visible="false"
                    Width="240px"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
&nbsp;
                </td>
            <td></td>
            <td></td>
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
         &nbsp;  &nbsp;
        <span class="tieudetongcong">           
            <asp:Label ID="lblTotalMoney" runat="server" Text="Tổng tiền:" meta:resourcekey="lblTotalMoney"></asp:Label>
        </span>
        <asp:Label ID="lblTotalMoneys" runat="server" Text="" CssClass="tieudetongcong2" ></asp:Label> 
	</div>
	<div class="chucnangright">
		 <asp:Button ID="btnXuatExcel" runat="server" Text="Xuất Excel" width="90px" CssClass="timkiembutom" OnClick="btnXuatExcel_Click"/>
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
            AutoGenerateColumns="False" OnRowDeleting="m_grid_RowDeleting" OnRowDataBound = "m_grid_OnRowDataBound" PageSize="50"
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
                                       
                      <span class="FontBoldBlack"> <%# DataBinder.Eval(Container.DataItem, "mCustomer.CustomerName")%> </span><br />
                      <span class="properties"> <%# DataBinder.Eval(Container.DataItem, "mCustomer.CustomerFullName")%> </span> 
                        <%--<a href='CustomersEdit.aspx?CustomerId=<%# Eval("CustomerId") + "&backUrl=PaymentTransactions.aspx"%>'></a>--%>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" Width="150"/>
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnCustomerName" runat="server" Text="Thông tin tài khoản" ></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                       <span class="properties">Họ tên:  </span><span style="color:Maroon; font-weight:bold;"><%#DataBinder.Eval(Container.DataItem, "mCustomer.CustomerFullName")%></span> <br />
                     <span class="properties">Email:   </span><%#DataBinder.Eval(Container.DataItem, "mCustomer.CustomerMail")%><br />
                     <span class="properties">Mobile:  </span><%# DataBinder.Eval(Container.DataItem, "mCustomer.CustomerMobile")%><br />
                     <span class="properties">Cơ quan: </span><%# DataBinder.Eval(Container.DataItem, "mCustomer.OrganName")%>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" Width="150"/>
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnServiceName" runat="server" Text="Ngày đóng phí" ></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# DateTimeHelpers.GetDateAndTime(Eval("CrDateTime"))%>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Center" Width="10%"/>
                    <HeaderStyle />          
                </asp:TemplateField> 
                 <asp:TemplateField> 
                        <HeaderTemplate>                                
                        Số tiền nộp
                    </HeaderTemplate> 
                    <ItemTemplate>                                           
                        <%# string.Format("{0:#,##}", Eval("Amount")).Replace(".",",")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Right" Width="10%"/>
                    <HeaderStyle />          
                </asp:TemplateField>
                 <asp:TemplateField> 
                        <HeaderTemplate>                                
                        Số tháng
                    </HeaderTemplate> 
                    <ItemTemplate>  
                        <%#ServicePackages_GetServicePackageNumMonthUse(short.Parse(Eval("ServicePackageId").ToString())) %>                                         
                        <%--<span class="properties">Thời hạn:</span><%# DataBinder.Eval(Container.DataItem, "mServicePackage.NumMonthUse")%> tháng<br />
                        <span style="color:#036AA4"><%# DataBinder.Eval(Container.DataItem, "mServicePackage.ConcurrentLogin")%> Acc sử dụng đồng thời</span>--%>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" Width="10%"/>
                    <HeaderStyle />          
                </asp:TemplateField>
                 <asp:TemplateField> 
                        <HeaderTemplate>                                
                        Thời hạn sử dụng
                    </HeaderTemplate> 
                    <ItemTemplate>                                           
                        <%# Eval("TransactionDesc")%>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" Width="25%"/>
                    <HeaderStyle />          
                </asp:TemplateField>
                 <asp:TemplateField Visible="false">
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnOperation" runat="server" Text="Thao tác" meta:resourcekey="lblGridColumnOperation"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:LinkButton ID="lbDelete" runat="server" CssClass="iconadmxoa" CommandName="Delete" ToolTip="Xóa" meta:resourcekey="lnkGridColumnDel"></asp:LinkButton> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="center" Width="6%" Wrap="false" />
                    <HeaderStyle Width="6%" />
                </asp:TemplateField>
                <asp:TemplateField Visible="false">
                    <ItemStyle HorizontalAlign="Center" Width="3%" />
                    <HeaderStyle Width="3%" />
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

