<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="CustomersRegisterNew.aspx.cs" Inherits="Admin_CustomersRegisterNew" %>
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
                    height: $(this).attr("WHeight"),
                    width: $(this).attr("WWidth"),
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
    $('#txtDateFrom').keyup(function () { alert('test'); });
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
    <table cellpadding="4" cellspacing="0" style="width:100%">
        <tr>
            <td width="62px">
                <asp:Label ID="lblServices" runat="server" meta:resourcekey="lblServices" 
                    Text="Dịch vụ:"></asp:Label>
            </td>
            <td width="250px">
                <asp:DropDownList ID="ddlServices" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="ServiceDesc" DataValueField="ServiceId" 
                    onselectedindexchanged="ddlServices_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
            <td width="70px" >
                <asp:Label ID="lblFindDate" runat="server" meta:resourcekey="lblFindDate" 
                    Text="Tìm theo:"></asp:Label>
            </td>
            <td>
                <asp:RadioButtonList ID="rbtDate" runat="server" AutoPostBack="true" 
                    onselectedindexchanged="rbtDate_SelectedIndexChanged" 
                    RepeatDirection="Horizontal">
                    <asp:ListItem Value="0">Tùy chọn</asp:ListItem>
                    <asp:ListItem Selected="True" Value="1">Ngày hôm nay</asp:ListItem>
                    <asp:ListItem Value="2">Hôm qua</asp:ListItem>
                    <asp:ListItem Value="3">1 Tuần</asp:ListItem>
                    <asp:ListItem Value="4">2 Tuần</asp:ListItem>
                    <asp:ListItem Value="5">1 Tháng</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td width="62px">
                <asp:Label ID="lblDateFrom" runat="server" meta:resourcekey="lblDateFrom"  Text="Từ ngày:"></asp:Label>
            </td>
            <td width="250px">
                <asp:TextBox ID="txtDateFrom" runat="server" CssClass="tukhoatimekiem"  Width="240px" OnTextChanged="txtDateFrom_TextChanged"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblDateTo" runat="server" meta:resourcekey="lblDateTo" Text="Đến ngày:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDateTo" runat="server" CssClass="tukhoatimekiem"  Width="240px" OnTextChanged="txtDateFrom_TextChanged"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="62px">
                <asp:Label ID="lblStatus" runat="server" meta:resourcekey="lblStatus" 
                    Text="Trạng thái:"></asp:Label>
            </td>
            <td width="250px">
                 <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="StatusDesc" DataValueField="StatusId" 
                    onselectedindexchanged="ddlStatus_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lblcared" runat="server" meta:resourcekey="lblcared" Text="Chăm sóc:"></asp:Label>
            </td>
            <td>
                 <asp:DropDownList ID="ddlCared" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="OrderByDesc" DataValueField="OrderBy" 
                    onselectedindexchanged="ddlCared_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style9">
                <asp:Label ID="lblOrderBy" runat="server" meta:resourcekey="lblOrderBy" 
                    Text="Sắp xếp:"></asp:Label>
            </td>
            <td width="250px">
                <asp:DropDownList ID="ddlOrderBy" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="OrderByDesc" DataValueField="OrderBy" 
                    onselectedindexchanged="ddlOrderBy_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lblSearch" runat="server" meta:resourcekey="lblSearch" 
                    Text="Từ khóa:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtSearch" runat="server" CssClass="tukhoatimekiem" 
                    Width="240px"></asp:TextBox>&nbsp;<asp:Button ID="btnSearch" runat="server" CssClass="timkiembutom" 
                    meta:resourcekey="btnSearch" onclick="btnSearch_Click" Text="Tìm kiếm">
                    </asp:Button><asp:LinkButton ID="btnRefresh" runat="server" CssClass="timkiembutom hidden-btn-dqs" Text="Làm mới" onclick="btnRefresh_Click"></asp:LinkButton>
                &nbsp;&nbsp;<asp:LinkButton ID="btnXuatExcel" runat="server" OnClick="btnXuatExcel_Click" CssClass="timkiembutom" Text="Xuất excel"> Xuất Excel</span>
                    </asp:LinkButton>
                <asp:DropDownList ID="ddlCustomerGroups" runat="server" AutoPostBack="True" Visible="false" 
                    CssClass="userselect" DataTextField="CustomerGroupDesc" 
                    DataValueField="CustomerGroupId" 
                    onselectedindexchanged="ddlCustomerGroups_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
                <br />
                    <asp:RadioButtonList ID="rblFindTypes" runat="server" 
                    RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="CustomerName">Tên truy cập</asp:ListItem>
                    <asp:ListItem Value="CustomerMail">Email</asp:ListItem>
                    <asp:ListItem Value="CustomerFullName">Họ tên</asp:ListItem>
                    <asp:ListItem Value="CustomerMobile">Điện thoại</asp:ListItem>
                </asp:RadioButtonList>
                </td>
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
        <%=GetLocalResourceObject("Customers").ToString()%>
	</div>
	<div class="chucnangright">
		
	</div>
    <div style="text-align:left; width:50%; float:right"><asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                            <ProgressTemplate><img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...</ProgressTemplate>
                            </asp:UpdateProgress></div>
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
                    <ItemStyle HorizontalAlign="Center" Width="3%" />  
                    <HeaderStyle Width="3%" />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnCustomerName" runat="server" Text="Tên tài khoản" meta:resourcekey="lblGridColumnCustomerName"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <a href='CustomersEditFull.aspx?CustomerId=<%# Eval("CustomerId") %>'
                         meta:resourcekey="lnkGridColumnEdit">
                       <asp:Label ID="lbCustomerName" runat="server"  style="display:none"></asp:Label>
                         <span class="FontBoldMaroon"> <%# Eval("CustomerName")%> </span>
                       </a><br />
                        <img style="border-width:0px; margin-top:2px;margin-right:5px;" width="12px" height="13px" src="../../../images/logo_smartbank.png"><a id="popup" onmouseover="ShowMenu('-1-<%# Eval("CustomerId") %>')" href='PaymentTransactions_Add.aspx?CustomerId=<%# Eval("CustomerId") %>' class="docsproperties"
                        title="<%# GetLocalResourceObject("lnkGridColumnAddRolesEdit.title").ToString() %>" meta:resourcekey="lnkGridColumnEdit">Giao dịch</a>    <br />
                       <img style="border-width:0px; margin-top:2px;margin-right:5px;" width="12px" height="13px" src="../../../images/logo_smartbank.png"><a id="popup" onmouseover="ShowMenu('-1-<%# Eval("CustomerId") %>')" href='NewsletterEmails_AddMore.aspx?CustomerId=<%# Eval("CustomerId") %>' class="docsproperties"
                        title="Thêm Email nhận bản tin" >Thêm Email</a>  <br />
                       <img style="border-width:0px; margin-top:2px;margin-right:5px;" width="12px" height="13px" src="../../../images/logo_smartbank.png"><a WHeight="500" WWidth="800" onmouseover="ShowMenu('-1-<%# Eval("CustomerId") %>')" href='CustomerField_Add.aspx?CustomerId=<%# Eval("CustomerId") %>' class="docsproperties popup"
                        title="Danh sách lĩnh vực quan tâm" >Lĩnh vực KH</a> <br />
                       <img style="border-width:0px; margin-top:2px;margin-right:5px;" width="12px" height="13px" src="../../../images/logo_smartbank.png"><a WHeight="600" WWidth="800" onmouseover="ShowMenu('-1-<%# Eval("CustomerId") %>')" href='popup_CustomersCare.aspx?CustomerId=<%# Eval("CustomerId") %>&CustomerName=<%#Eval("CustomerName") %>' class="docsproperties popup"
                        title="Lịch sử chăm sóc khách hàng" >Lịch sử CSKH</a> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left"  VerticalAlign="Top" Width="15%"/>
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnCustomerInfo" runat="server" Text="Liên hệ nhà riêng" meta:resourcekey="lblGridColumnCustomerInfo"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate>                                             
                     <span class="properties">Họ tên:  </span><span style="color:Maroon; font-weight:bold;"><%# Eval("CustomerFullName")%></span>  <%# "(" + "<span style='color:Red; font-weight:bold;'>" + string.Format("{0:#,##}",Eval("MaxConcurrentLogin")) + "</span>" +")"%><br />
                     <span class="properties">Email:  </span> <%# Eval("CustomerMail")%><br />
                     <span class="properties">SĐT:  </span> <%# Eval("CustomerMobile")%><br />
                     <span class="properties">Địa chỉ:  </span> <%# Eval("Address")%><br />
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" Width="20%"   VerticalAlign="Top"/>
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnCustomerInfo" runat="server" Text="Cơ quan" ></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate>                                             
                     <span class="properties">Cơ quan:  </span> <%# Eval("OrganName")%><br />
                     <span class="properties">SĐT:  </span> <%# Eval("OrganPhone")%><br />
                     <span class="properties">Fax:  </span> <%# Eval("OrganFax")%><br />
                     <span class="properties">Địa chỉ:  </span> <%# Eval("OrganAddress")%><br />
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" Width="20%"   VerticalAlign="Top"/>
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnTime" runat="server" Text="Thời gian" meta:resourcekey="lblGridColumnTime"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <span class="xuatban<%# Eval("StatusId") %>" style="text-align:center; width:100%;" > <%# LanguageHelpers.IsCultureVietnamese() ? Status.Static_Get(byte.Parse(Eval("StatusId").ToString()), l_Status).StatusDesc : Status.Static_Get(byte.Parse(Eval("StatusId").ToString()), l_Status).StatusName%></span>                               
                        <br /><%# DateTimeHelpers.GetDateAndTime(Eval("CrDateTime"))%>                        
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="12%" />        
                </asp:TemplateField> 
               <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnStatus" runat="server" Text="CSKH"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate>
                        <div style="width:100%; height:120px; overflow:scroll; overflow-x: hidden;margin-bottom:8px;">
                        <%# getInfo(Eval("CustomerId").ToString())%>
                        </div>
                    <a WHeight="200" WWidth="400" href='<%# "CustomerCareEdit.aspx?CustomerId=" + Eval("CustomerId").ToString() %>' class="popup docsproperties" title="Thêm nhật ký CSKH"><asp:Label ID="lblGridColumnSummary" runat="server" CssClass="timkiembutom" Text="Thêm nhật ký CSKH"></asp:Label></a>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign = "Left" Width="40%" Height="150px" />  
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

