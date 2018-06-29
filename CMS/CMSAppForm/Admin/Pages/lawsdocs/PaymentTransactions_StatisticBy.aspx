<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="PaymentTransactions_StatisticBy.aspx.cs" Inherits="Admin_PaymentTransactions_StatisticBy" %>
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

            $('a#popup').live('click', function (e) {
                var page = $(this).attr("href")
                var cdialog = $('<div id="divEdit"></div>')
                .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="98%" height="98%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 420,
                    width: 580,
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
        SetStartup();
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
    <div style="text-align:center"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
     <table cellpadding="2" cellspacing="0" style="width:100%">
        <tr runat="server" visible="false">
            <td style="width:60px; white-space:nowrap;">Site:
            </td>
            <td style="width:250px"><asp:DropDownList ID="ddlSite" runat="server" DataTextField="SiteDesc" 
                            DataValueField="SiteId" Width="250px" CssClass="userselect"
                            AutoPostBack="True" onselectedindexchanged="ddlSite_SelectedIndexChanged">
                        </asp:DropDownList>
            </td>
            <td style="width:90px; white-space:nowrap;">Kiểu giao dịch:</td>
            <td><asp:DropDownList ID="ddlTranType" runat="server" DataTextField="TransactionTypeDesc" 
                            DataValueField="TransactionTypeId" Width="250px" CssClass="userselect"
                            AutoPostBack="True" onselectedindexchanged="ddlTranType_SelectedIndexChanged">
                        </asp:DropDownList>
                <asp:DropDownList ID="ddlPartner" runat="server" AutoPostBack="True" Visible="false"
                    CssClass="userselect" DataTextField="PartnerDesc" 
                    DataValueField="PartnerId" 
                    onselectedindexchanged="ddlPartner_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="60px" >
                <asp:Label ID="lblServices" runat="server" meta:resourcekey="lblServices" 
                    Text="Dịch vụ:"></asp:Label>
            </td>
            <td width="250px">
                <asp:DropDownList ID="ddlServices" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="ServiceDesc" DataValueField="ServiceId" 
                    onselectedindexchanged="ddlServices_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
            <td width="90px">
                <asp:Label ID="lblServicePackages" runat="server" 
                    meta:resourcekey="lblServicePackages" Text="Gói dịch vụ:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlServicePackages" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="ServicePackageDesc" 
                    DataValueField="ServicePackageId" 
                    onselectedindexchanged="ddlServicePackages_SelectedIndexChanged" Width="250px">
                    <asp:ListItem Text="..." Value="0" />
                </asp:DropDownList>
                
                <asp:DropDownList ID="ddlPSC" runat="server" AutoPostBack="True" Visible="false" 
                    CssClass="userselect" 
                    onselectedindexchanged="ddlPSC_SelectedIndexChanged" Width="250px">
                    <asp:ListItem Value="0">...</asp:ListItem>
                    <asp:ListItem Value="1">Có phát sinh cước</asp:ListItem>
                    <asp:ListItem Value="2">Không phát sinh cước</asp:ListItem> 
                </asp:DropDownList>
            </td>
        </tr>
         <tr>
             <td>
                 <asp:Label ID="lblTransactionStatus" runat="server" 
                     meta:resourcekey="lblTransactionStatus" Text="Trạng thái:"></asp:Label>
             </td>
             <td width="250px">
                 <asp:DropDownList ID="ddlTransactionStatus" runat="server" AutoPostBack="True" 
                     CssClass="userselect" DataTextField="TransactionStatusDesc" 
                     DataValueField="TransactionStatusId" 
                     onselectedindexchanged="ddlTransactionStatus_SelectedIndexChanged" 
                     Width="250px">
                 </asp:DropDownList>
             </td>
             <td width="90px">
                 <asp:Label ID="lblPaymentTypes" runat="server" 
                     meta:resourcekey="lblPaymentTypes" Text="Kênh thanh toán:"></asp:Label>
             </td>
             <td>
                 <asp:DropDownList ID="ddlPaymentTypes" runat="server" AutoPostBack="True" 
                     CssClass="userselect" DataTextField="PaymentTypeDesc" 
                     DataValueField="PaymentTypeId" 
                     onselectedindexchanged="ddlPaymentTypes_SelectedIndexChanged" Width="250px">
                 </asp:DropDownList>
             </td>
         </tr>
        <tr>
            <td>
                <asp:Label ID="lblDateFrom" runat="server" meta:resourcekey="lblDateFrom" 
                    Text="Từ ngày:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDateFrom" runat="server" CssClass="tukhoatimekiem" 
                    Width="100px"></asp:TextBox>
                <asp:Label ID="lblDateTo" runat="server" meta:resourcekey="lblDateTo" 
                    Text="Đến"></asp:Label>
                <asp:TextBox ID="txtDateTo" runat="server" CssClass="tukhoatimekiem" 
                    Width="100px"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblFindBydate" runat="server" meta:resourcekey="lblFindBydate" 
                    Text="Thống kê theo:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" 
                    CssClass="userselect" 
                    onselectedindexchanged="ddlSearchBy_SelectedIndexChanged" Width="250px">
                    <asp:ListItem Value="CrDateTime" Selected="True">Ngày tạo</asp:ListItem>
                    <asp:ListItem Value="Month">Tháng</asp:ListItem>
                    <asp:ListItem Value="PaymentTypeId">Kênh thanh toán</asp:ListItem> 
                    <asp:ListItem Value="ServiceId">Dịch vụ</asp:ListItem> 
                    <asp:ListItem Value="ServicePackageId">Gói dịch vụ</asp:ListItem> 
                    <asp:ListItem Value="TransactionTypeId">Loại giao dịch</asp:ListItem> 
                </asp:DropDownList>
                &nbsp;<asp:LinkButton ID="btnSearch" runat="server" CssClass="timkiembutom" 
                    meta:resourcekey="btnSearch" onclick="btnSearch_Click" Text="Tìm kiếm">
                    </asp:LinkButton>&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="btnXuatExcel" runat="server" OnClick="btnXuatExcel_Click" CssClass="timkiembutom" Text="Xuất excel"> Xuất Excel</span>
                    </asp:LinkButton>&nbsp;
            </td>
        </tr>
    </table>
    <div class="clear5px"></div>
	<div class="vien"></div>
	<div class="clear5px"></div>  
    <div class="khungchucnang">
    <div class="chucnangleft">
        <table style="border:gainsboro;" width="90%"><tr>
            <td style="width:30%"><span style="color:red; font-size:15px;">Tổng giao dịch: </span><asp:Label ID="lblTong" runat="server" style="font-size:15px;" Text=""></asp:Label> </td>
            <td style="width:30%"><span style="color:red; font-size:15px;">Tổng tiền: </span><asp:Label ID="lblTongTien" runat="server" style="font-size:15px;" Text="" ></asp:Label></td>
            <td style="width:30%"><span style="color:red; font-size:15px;">Tổng tiền chưa có VAT: </span><asp:Label ID="lblTongTienVAT" style="font-size:15px;" runat="server" Text="" ></asp:Label> </td>
               </tr></table>
	</div>
	<div class="chucnangright">
	</div>
    <div style="text-align:left; width:50%; float:right"><asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                            <ProgressTemplate><img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...</ProgressTemplate>
                            </asp:UpdateProgress></div>
	<div class="clear5px"></div>
    <div class="contenbangdulieu">
        <asp:GridView ID="m_grid" runat="server" ShowHeaderWhenEmpty="True" AllowSorting="true" 
            AutoGenerateColumns="true" Width="100%" OnRowDataBound = "m_grid_OnRowDataBound"  CellPadding="0" CellSpacing="0" BorderWidth="0" >
            <HeaderStyle CssClass="trbangdulieutieude" />
            <RowStyle CssClass="trbangdulieutieudenoidung" />
            <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
            <SelectedRowStyle CssClass="trbangdulieutieudenoidung" /> 
        </asp:GridView>        
    </div>
   </div>
    </ContentTemplate>
         <Triggers>
            <asp:PostBackTrigger ControlID="btnXuatExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
