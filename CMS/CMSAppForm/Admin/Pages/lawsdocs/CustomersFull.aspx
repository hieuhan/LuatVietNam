<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="CustomersFull.aspx.cs" Inherits="Admin_CustomersFull" %>
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
                    height: 600,
                    width: 1200,
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
            $('a.popup').live('click', function (e) {
                var page = $(this).attr("href")
                var cdialog = $('<div id="divEdit"></div>')
                .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="98%" height="98%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: $(this).attr("WHeight"),
                    width: $(this).attr("WWidth"),
                    title: $(this).attr("title"),
                    close: function (event, ui) {
                        $(this).remove();
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
        <tr runat="server" visible="false">
            <td style="width:62px; white-space:nowrap;">Site:
            </td>
            <td style="width:300px"><asp:DropDownList ID="ddlSite" runat="server" DataTextField="SiteDesc" 
                            DataValueField="SiteId" Width="250px" CssClass="userselect"
                            AutoPostBack="True" onselectedindexchanged="ddlSite_SelectedIndexChanged">
                        </asp:DropDownList>
            </td>
            <td style="width:62px; white-space:nowrap;"></td>
            <td></td>
        </tr>
        <tr>
            <td width="62px">
                <asp:Label ID="lblServices" runat="server" 
                    meta:resourcekey="lblServices" Text="Dịch vụ:"></asp:Label>
            </td>
            <td width="300px">
                <asp:DropDownList ID="ddlServices" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="ServiceDesc" 
                    DataValueField="ServiceId" 
                    onselectedindexchanged="ddlServices_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
                
            </td>
            <td width="90px">
                <asp:Label ID="lblCustomerGroups" runat="server" meta:resourcekey="lblCustomerGroups" 
                    Text="Nhóm:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlCustomerGroups" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="CustomerGroupDesc" DataValueField="CustomerGroupId" 
                    onselectedindexchanged="ddlCustomerGroups_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="62px">
                <asp:Label ID="lblDateFrom" runat="server" meta:resourcekey="lblDateFrom" 
                    Text="Từ ngày:"></asp:Label>
            </td>
            <td width="300px">
                <asp:TextBox ID="txtDateFrom" runat="server" CssClass="tukhoatimekiem" 
                    Width="100px"></asp:TextBox>
                <asp:Label ID="lblDateTo" runat="server" meta:resourcekey="lblDateTo" 
                    Text="Đến"></asp:Label>
                <asp:TextBox ID="txtDateTo" runat="server" CssClass="tukhoatimekiem" 
                    Width="100px"></asp:TextBox>
            </td>
            <td >
                <asp:Label ID="lblSearchByDate" runat="server" 
                    meta:resourcekey="lblSearchByDate" Text="Tìm theo:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlSearchByDate" runat="server" AutoPostBack="True" 
                    CssClass="userselect" onselectedindexchanged="ddlSearchByDate_SelectedIndexChanged" Width="250px">
                    <asp:ListItem Value="CrDateTime" Selected="True">Ngày tạo</asp:ListItem>
                    <asp:ListItem Value="LastLoginTime">Ngày truy cập cuối cùng</asp:ListItem>
                    <asp:ListItem Value="LastChangePassTime">Ngày thay đổi MK cuối cùng</asp:ListItem>
                    <asp:ListItem Value="FirstPaymentTime">Ngày thanh toán đầu tiên</asp:ListItem>
                    <asp:ListItem Value="LastPaymentTime">Ngày thanh toán cuối cùng</asp:ListItem>
                    <asp:ListItem Value="ServiceBeginTime">Ngày bắt đầu dịch vụ</asp:ListItem>
                    <asp:ListItem Value="ServiceEndTime">Ngày kết thúc dịch vụ</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="62px">
                <asp:Label ID="lblValueFrom" runat="server" meta:resourcekey="lblValueFrom" 
                    Text="Giá trị từ:"></asp:Label>
            </td>
            <td width="300px">
                <asp:TextBox ID="txtValueFrom" runat="server" CssClass="tukhoatimekiem" 
                    Width="100px"></asp:TextBox>
                <asp:Label ID="lblValueTo" runat="server" meta:resourcekey="lblValueTo" 
                    Text="Đến"></asp:Label>
                <asp:TextBox ID="txtValueTo" runat="server" CssClass="tukhoatimekiem" 
                    Width="100px"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblSearchByValue" runat="server" 
                    meta:resourcekey="lblSearchByValue" Text="Tìm theo:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlSearchByValue" runat="server" AutoPostBack="True" 
                    CssClass="userselect" 
                    onselectedindexchanged="ddlSearchByValue_SelectedIndexChanged" Width="250px">
                    <asp:ListItem Selected="True" Value="PaymentCount">Tổng số lần thanh toán</asp:ListItem>
                    <asp:ListItem Value="LoginCount">Tổng số lần truy cập</asp:ListItem>
                    <asp:ListItem Value="ChangePassCount">Tổng số lần đổi MK</asp:ListItem>
                    <asp:ListItem Value="TotalPaymentMoney">Tổng số tiền thanh toán</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="62px">
                <asp:Label ID="lblStatus" runat="server" meta:resourcekey="lblStatus" 
                    Text="Trạng thái:"></asp:Label>
            </td>
            <td width="300px">
                <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="StatusDesc" DataValueField="StatusId" 
                    onselectedindexchanged="ddlStatus_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lblOrderBy" runat="server" meta:resourcekey="lblOrderBy" 
                    Text="Sắp xếp:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlOrderBy" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="OrderByDesc" DataValueField="OrderBy" 
                    onselectedindexchanged="ddlOrderBy_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style9">
                <asp:Label ID="lblSearch" runat="server" meta:resourcekey="lblSearch" 
                    Text="Từ khóa:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtSearch" runat="server" CssClass="tukhoatimekiem" 
                             Width="240px"></asp:TextBox><br />
            </td>
            <td style="width: 120px;">
                <asp:Label ID="Label1" runat="server"
                           Text="Nhóm khách hàng:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlGroupCustomer" runat="server" AutoPostBack="True" 
                                  CssClass="userselect" 
                                  onselectedindexchanged="ddlSearchByValue_SelectedIndexChanged" Width="250px">
                    <asp:ListItem Selected="True" Value="">Tất cả khách hàng</asp:ListItem>
                    <asp:ListItem Value="CustomersAreUsing">Khách hàng đang sử dụng</asp:ListItem>
                    <asp:ListItem Value="CustomerExpired">Khách hàng hết hạn</asp:ListItem>
                    <asp:ListItem Value="UnpaidCustomers">Khách hàng chưa đóng phí</asp:ListItem>
                    <asp:ListItem Value="CustomersPayAFee">Khách hàng đóng phí</asp:ListItem>
                    <asp:ListItem Value="PayTheFirstFee">Khách hàng đóng phí lần đầu</asp:ListItem>
                    <asp:ListItem Value="CustomersContinueToPayFees">Khách hàng đóng phí tiếp tục</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnSearch" runat="server" CssClass="timkiembutom" 
                            meta:resourcekey="btnSearch" onclick="btnSearch_Click" Text="Tìm kiếm">
                </asp:Button>
                &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="btnXuatExcel" runat="server" OnClick="btnXuatExcel_Click" CssClass="timkiembutom" Text="Xuất excel"> Xuất Excel
                    </asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td width="62px">
            </td>
            <td colspan="3">
                <asp:RadioButtonList ID="rblFindTypes" runat="server" 
                                 Width="350px"   RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True"  Value="CustomerName">Tên truy cập</asp:ListItem>
                    <asp:ListItem Value="CustomerMail">Email</asp:ListItem>
                    <asp:ListItem Value="CustomerFullName">Họ tên</asp:ListItem>
                    <asp:ListItem Value="CustomerMobile">Điện thoại</asp:ListItem>
                    <asp:ListItem Value="All">Tất cả</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                
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
        <%=GetLocalResourceObject("Customers").ToString()%>&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="ddlTotalAmount" runat="server" Visible="False" CssClass="userselect" Width="350" />
	</div>
	<div class="chucnangright">
		<a href="CustomersEditFull.aspx"  title="<%=GetLocalResourceObject("lnkAddNew.title").ToString() %>" class="themmoi" > 
            <asp:Label ID="lblAddNew" runat="server" Text="Thêm mới" meta:resourcekey="lblAddNew"></asp:Label>
        </a>
        <asp:LinkButton ID="lbReview" runat="server" CssClass="duyettin" Text="Duyệt" 
            meta:resourcekey="lbReview" onclick="lbReview_Click"> </asp:LinkButton>					
        <asp:LinkButton ID="lbUnCheck" runat="server" CssClass="boduyet" 
            Text="Bỏ duyệt" meta:resourcekey="lbUnCheck" onclick="lbUnCheck_Click"> </asp:LinkButton>
        <asp:LinkButton ID="lbDelete" runat="server" CssClass="xoatin" Text="Xóa"  Visible="true" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa các văn bản đã chọn?')"
            meta:resourcekey="lbDelete" onclick="lbDelete_Click">
        </asp:LinkButton>
	</div>
    <div style="text-align:left; width:30%; float:right">
                            </div>
	<div class="clear5px"></div>
    <div class="contenbangdulieu">
    
        <asp:GridView ID="m_grid" DataKeyNames="CustomerId" runat="server" ShowHeaderWhenEmpty="True"
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
                                     ToolTip ='<%# Eval("CustomerId").ToString() %>'> </asp:Label>
                    </ItemTemplate>      
                    <ItemStyle HorizontalAlign="Center" Width="3%"/>  
                    <HeaderStyle Width="3%" />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnCustomerName" runat="server" Text="Tên tài khoản" meta:resourcekey="lblGridColumnCustomerName"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                       <a href='CustomersEditFull.aspx?CustomerId=<%# Eval("CustomerId") %>' 
                         meta:resourcekey="lnkGridColumnEdit"><asp:Label ID="lbCustomerName" runat="server"  style="display:none"></asp:Label>
                         <span class="FontBoldMaroon"> <%# Eval("CustomerName")%> </span>
                       </a><br /><br />
                        <img style="border-width:0px; margin-top:2px;margin-right:5px;" width="12px" height="13px" src="../../../images/logo_smartbank.png"><a id="popup" onmouseover="ShowMenu('-1-<%# Eval("CustomerId") %>')" href='PaymentTransactions_Add.aspx?CustomerId=<%# Eval("CustomerId") %>' class="docsproperties"
                        title="<%# GetLocalResourceObject("lnkGridColumnAddRolesEdit.title").ToString() %>" meta:resourcekey="lnkGridColumnEdit">Giao dịch</a>    <br />
                       <img style="border-width:0px; margin-top:2px;margin-right:5px;" width="12px" height="13px" src="../../../images/logo_smartbank.png"><a id="popup" onmouseover="ShowMenu('-1-<%# Eval("CustomerId") %>')" href='NewsletterEmails_AddMore.aspx?CustomerId=<%# Eval("CustomerId") %>' class="docsproperties"
                        title="Thêm Email nhận bản tin" >Thêm Email</a>  <br />
                       <img style="border-width:0px; margin-top:2px;margin-right:5px;" width="12px" height="13px" src="../../../images/logo_smartbank.png"><a WHeight="500" WWidth="800" onmouseover="ShowMenu('-1-<%# Eval("CustomerId") %>')" href='CustomerField_Add.aspx?CustomerId=<%# Eval("CustomerId") %>' class="docsproperties popup"
                        title="Danh sách lĩnh vực quan tâm" >Lĩnh vực KH</a> <br />
                       <img style="border-width:0px; margin-top:2px;margin-right:5px;" width="12px" height="13px" src="../../../images/logo_smartbank.png"><a WHeight="600" WWidth="800" onmouseover="ShowMenu('-1-<%# Eval("CustomerId") %>')" href='popup_CustomersCare.aspx?CustomerId=<%# Eval("CustomerId") %>&CustomerName=<%#Eval("CustomerName") %>' class="docsproperties popup"
                        title="Lịch sử chăm sóc khách hàng" >Lịch sử CSKH</a> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" Width="12%"/>
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnCustomerInfo" runat="server" Text="Thông tin khách hàng" meta:resourcekey="lblGridColumnCustomerInfo"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate>                                             
                     <img title="Họ tên" id="Image1" style="border-width:0px;" width="16px" height="16px" src="../../images/user.png"> <span style="color:Maroon; font-weight:bold;"><%# Eval("CustomerFullName")%></span>  <%# "(" + "<span style='color:Red; font-weight:bold;'>" + string.Format("{0:#,##}",Eval("MaxConcurrentLogin")) + "</span>" +")"%><br />
                     <img title="Email đăng ký" id="Image2" style="border-width:0px;"  width="16px" height="16px" src="../../images/email_go.png"> <%# Eval("CustomerMail")%><br />
                     <img title="Số điện thoại khách hàng" id="Image3" style="border-width:0px;"  width="16px" height="16px" src="../../images/phone_add.png"> <%# Eval("CustomerMobile")%><br />
                     <img title="Địa chỉ khách hàng" id="Img8" style="border-width:0px; margin-top:2px;" width="16px" height="16px" src="../../../images/house-icon.png"> <%# Eval("Address")%><br />
                     <img title="Tên cơ quan" id="Image4" style="border-width:0px;" width="16px" height="16px" src="../../../images/organization-icon.png"> <%# Eval("OrganName")%><br />
                     <img title="Số điện thoại cơ quan" id="Img6" style="border-width:0px; margin-top:2px;" width="16px" height="16px" src="../../../images/phone-icon.png"> <%# Eval("OrganPhone")%><br />
                     <img title="Fax" id="Img7" style="border-width:0px; margin-top:2px;"  width="16px" height="16px" src="../../../images/App-kde-print-fax-icon.png"> <%# Eval("OrganFax")%><br />
                     <img title="Địa chỉ làm việc" id="Img8" style="border-width:0px; margin-top:2px;" width="16px" height="16px" src="../../../images/house-icon.png"> <%# Eval("OrganAddress")%>
                     <%--<img id="Img2" style="border-width:0px; margin-top:2px;"  src="../../../images/group.png"><span class="xuatban<%# Eval("CustomerGroupId") %>" > <%# LanguageHelpers.IsCultureVietnamese() ? CustomerGroups.Static_Get(short.Parse(Eval("CustomerGroupId").ToString()), l_CustomerGroups).CustomerGroupDesc : CustomerGroups.Static_Get(short.Parse(Eval("CustomerGroupId").ToString()), l_CustomerGroups).CustomerGroupName%></span>--%>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" VerticalAlign="Top"/>
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnCustomerServiceInfo" runat="server" Text="Thông tin dịch vụ" meta:resourcekey="lblGridColumnCustomerServiceInfo"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate>                                             
                    <span style="color:Maroon; font-weight:bold;"><%# (Eval("ServiceDetail").ToString() != "" ? "<img id='Img31' style='border-width:0px; margin-top:2px;' width='12px' height='13px' src='../../../images/logo_smartbank.png'> " + Eval("ServiceDetail").ToString().Replace("<br />", "<br /><img id='Img11' style='border-width:0px; margin-top:2px;' width='12px' height='13px' src='../../../images/logo_smartbank.png'> ") + "<br />" : "")%></span>
                     <img id="Img3" style="border-width:0px; margin-top:2px;" width="12px" height="13px" src="../../../images/logo_smartbank.png"> Bắt đầu DV: <b><%# DateTimeHelpers.GetDateHH24(Eval("ServiceBeginTime"))%></b><br />
                     <img id="Img10" style="border-width:0px; margin-top:2px;" width="12px" height="13px" src="../../../images/logo_smartbank.png"> Hết hạn DV: <asp:Label ID="lblServiceEndTime" runat="server" Font-Bold="true" Visible='<%# Eval("ServiceEndTime").ToString()==DateTime.MinValue.ToString() ? false:true %>' Text='<%# DateTimeHelpers.GetDateHH24(Eval("ServiceEndTime")) %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" VerticalAlign="Top"/>
                    <HeaderStyle />          
                </asp:TemplateField>                
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnCustomerTransactionsInfo" runat="server" Text="Thông tin giao dịch" meta:resourcekey="lblGridColumnCustomerTransactionsInfo"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate>                                             
                      <img id="Img3" style="border-width:0px; margin-top:2px;" width="12px" height="13px" src="../../../images/logo_smartbank.png"> Số lần GD: <span style="color:Black; font-weight:bold;"><%# Eval("PaymentCount")%></span><br />
                      <img id="Img6" style="border-width:0px; margin-top:2px;" width="12px" height="13px" src="../../../images/logo_smartbank.png"> GD đầu: <span style="color:Black; font-weight:bold;"><%# DateTimeHelpers.GetDateAndTime(Eval("FirstPaymentTime"))%></span><br />
                      <img id="Img7" style="border-width:0px; margin-top:2px;" width="12px" height="13px" src="../../../images/logo_smartbank.png"> GD cuối: <span style="color:Black; font-weight:bold;"><%# DateTimeHelpers.GetDateAndTime(Eval("LastPaymentTime"))%></span><br />
                      <img id="Img8" style="border-width:0px; margin-top:2px;" width="12px" height="13px" src="../../../images/logo_smartbank.png"> Tổng tiền: <span style="color:Black; font-weight:bold;"><%# string.Format("{0:#,#}",Eval("TotalPaymentMoney")) + " đ"%></span><br />
                      <img id="Img9" style="border-width:0px; margin-top:2px;" width="12px" height="13px" src="../../../images/logo_smartbank.png"> Số tiền GD cuối: <span style="color:Black; font-weight:bold;"><%# string.Format("{0:#,#}", Eval("LastPaymentMoney")) + " đ"%></span>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" VerticalAlign="Top"/>
                    <HeaderStyle />          
                </asp:TemplateField> 
                  <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnCustomerAccessInfo" runat="server" Text="Thông tin truy cập" meta:resourcekey="lblGridColumnCustomerAccessInfo"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate>             
                     <img id="Img1" style="border-width:0px; margin-top:2px;" width="12px" height="13px" src="../../../images/logo_smartbank.png">Đăng ký: <b><%# DateTimeHelpers.GetDateAndTime(Eval("CrDateTime"))%></b><br />                                
                     <img id="Image4" style="border-width:0px; margin-top:2px;" width="12px" height="13px" src="../../../images/logo_smartbank.png"> Số lần login: <span style="color:Black; font-weight:bold;"><%# string.Format("{0:#,#}",Eval("LoginCount"))%></span><br />
                     <img id="Img3" style="border-width:0px; margin-top:2px;" width="12px" height="13px" src="../../../images/logo_smartbank.png"> Login cuối: <span style="color:Black; font-weight:bold;"><%# DateTimeHelpers.GetDateAndTime(Eval("LastLoginTime"))%></span><br />
                     <img id="Img4" style="border-width:0px; margin-top:2px;" width="12px" height="13px" src="../../../images/logo_smartbank.png"> Số lần đổi MK: <span style="color:Black; font-weight:bold;"><%# string.Format("{0:#,#}",Eval("ChangePassCount"))%></span><br />
                     <img id="Img5" style="border-width:0px; margin-top:2px;" width="12px" height="13px" src="../../../images/logo_smartbank.png"> Đổi MK cuối: <span style="color:Black; font-weight:bold;"><%# DateTimeHelpers.GetDateAndTime(Eval("LastChangePassTime"))%></span>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" VerticalAlign="Top" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnStatus" runat="server" Text="Trạng thái" meta:resourcekey="lblGridColumnStatus"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                    <span class="xuatban<%# Eval("StatusId") %>" style="width: 100px;"> <%# LanguageHelpers.IsCultureVietnamese() ? Status.Static_Get(byte.Parse(Eval("StatusId").ToString()), l_Status).StatusDesc : Status.Static_Get(byte.Parse(Eval("StatusId").ToString()), l_Status).StatusName%></span>                               
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign = "Left" Wrap="false" />  
                    <HeaderStyle/>        
                </asp:TemplateField>
                 <asp:TemplateField Visible="false">
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnOperation" runat="server" Text="Thao tác" meta:resourcekey="lblGridColumnOperation"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <a href='CustomersEdit.aspx?CustomerId=<%# Eval("CustomerId") %>' class="iconadmsua"
                         meta:resourcekey="lnkGridColumnEdit"></a>  
                        <asp:LinkButton ID="lbDelete" runat="server" CssClass="iconadmxoa" CommandName="Delete" ToolTip="Xóa" meta:resourcekey="lnkGridColumnDel"></asp:LinkButton> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="center" Width="6%" Wrap="false" />
                    <HeaderStyle Width="6%" />
                </asp:TemplateField>
                <asp:TemplateField Visible="true">
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
        <br />
         <asp:GridView ID="GridView1" Visible="false" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateDeleteButton="True" AutoGenerateEditButton="True" 
             CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDeleting="GridView1_RowDeleting" DataKeyNames="CustomerServiceId" 
             OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" >
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775"  />
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
    </div>
   </div>
   <div class="clear5px"></div>    
                <uc1:CustomPaging ID="CustomPaging" runat="server" />                
                <div class="clear5px">
                   
        </div>   
</ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnXuatExcel" />
            <asp:AsyncPostBackTrigger ControlID ="txtDateFrom" EventName ="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID ="txtDateTo" EventName ="TextChanged" />
        </Triggers>
        
    </asp:UpdatePanel>
</asp:Content>

