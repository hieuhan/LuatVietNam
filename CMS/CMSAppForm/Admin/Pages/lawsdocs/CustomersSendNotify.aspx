<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="CustomersSendNotify.aspx.cs" Inherits="Admin_CustomersSendNotify" %>
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
                    width: 900,
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
        $("#<%= txtDateFrom.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#<%= txtDateTo.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
    }
    </script>
    <asp:UpdatePanel ID="upn_Grid" runat="server">
    <ContentTemplate>
    <div style="text-align:center"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">        
        <tr>
            <td style="width:100px">
                <asp:Label ID="Label2" runat="server" Text="Tiều đề:"  ></asp:Label>
                </td>
            <td colspan="3">     
            <asp:TextBox ID="txtTitle" runat="server" Width="668px" 
                    CssClass="tukhoatimekiem"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width:100px">
                <asp:Label ID="Label1" runat="server" Text="Nội dung:"  ></asp:Label>
                </td>
            <td colspan="2" valign="bottom" style="vertical-align:bottom;" >     
            <asp:TextBox ID="txtContent" runat="server" Width="674px" Height="60px" >
                                </asp:TextBox>    
          
                <asp:LinkButton ID="btnBack" runat="server" CssClass="quaylai" Visible="false"
                                Text="Quay lại" meta:resourcekey="btnBack" 
                        onclick="btnBack_Click" >
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnSave" runat="server" CssClass="luuvaquaylai"  Text="Gửi đi" onclick="btnSave_Click">
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnSaveAndNew" runat="server" CssClass="luuvathemmoi"  Text="Gửi và thêm mới"  
                        onclick="btnSaveAndNew_Click" Visible="false"  >
                    </asp:LinkButton>  
            </td>
        </tr>
    </table>
        
    <div class="clear5px"></div>
    <div style="font-size: 16px;font-weight: bold; padding: 6px;">
        Danh sách nhận
    </div>
    <div class="vien"></div>
    <div style="font-size: 16px;font-weight: bold; padding: 6px;">
    <asp:RadioButtonList ID="rblSendType" runat="server" RepeatDirection="Horizontal" 
        OnSelectedIndexChanged="rblSendType_SelectedIndexChanged" AutoPostBack="true" >
        <asp:ListItem Selected="True" Value="0">Tất cả thành viên</asp:ListItem>
        <asp:ListItem Value="1">Tất cả danh sách lọc</asp:ListItem>
        <asp:ListItem Value="2">Tất cả trong danh sách chọn</asp:ListItem>
    </asp:RadioButtonList>
    </div>
    <div id="ListCustomerPannel" runat="server" visible="false">
    <table cellpadding="2" cellspacing="0" style="width:100%">
        <tr>
            <td style="width:62px; white-space:nowrap;">Site:
            </td>
            <td style="width:250px"><asp:DropDownList ID="ddlSite" runat="server" DataTextField="SiteDesc" 
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
            <td width="250px">
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
                <asp:DropDownList ID="ddlCustomerGroups" runat="server" AutoPostBack="True"  Enabled="false"
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
            <td width="250px">
                <asp:TextBox ID="txtDateFrom" runat="server" CssClass="tukhoatimekiem" 
                    Width="100px"></asp:TextBox>
                <asp:Label ID="lblDateTo" runat="server" meta:resourcekey="lblDateTo" 
                    Text="Đến"></asp:Label>
                <asp:TextBox ID="txtDateTo" runat="server" CssClass="tukhoatimekiem" 
                    Width="100px"></asp:TextBox>
            </td>
            <td >
                <asp:Label ID="lblStatus" runat="server" meta:resourcekey="lblStatus" 
                    Text="Trạng thái:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="StatusDesc" DataValueField="StatusId" 
                    onselectedindexchanged="ddlStatus_SelectedIndexChanged" Width="250px">
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
                    Width="240px"></asp:TextBox>&nbsp;<asp:LinkButton ID="btnSearch" runat="server" CssClass="timkiembutom" 
                    meta:resourcekey="btnSearch" onclick="btnSearch_Click" Text="Tìm kiếm">
                    </asp:LinkButton><br />
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
    </div>
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
	<div class="chucnangright" style="display:none;">
		<a href="CustomersEdit.aspx"  title="<%=GetLocalResourceObject("lnkAddNew.title").ToString() %>" class="themmoi" > 
            <asp:Label ID="lblAddNew" runat="server" Text="Thêm mới" meta:resourcekey="lblAddNew"></asp:Label>
        </a>
        <asp:LinkButton ID="lbReview" runat="server" CssClass="duyettin" Text="Duyệt" 
            meta:resourcekey="lbReview" onclick="lbReview_Click"> </asp:LinkButton>					
        <asp:LinkButton ID="lbUnCheck" runat="server" CssClass="boduyet" 
            Text="Bỏ duyệt" meta:resourcekey="lbUnCheck" onclick="lbUnCheck_Click"> </asp:LinkButton>
        <asp:LinkButton ID="lbDelete" runat="server" CssClass="xoatin" Text="Xóa"  Visible="true"
            meta:resourcekey="lbDelete" onclick="lbDelete_Click">
        </asp:LinkButton>
	</div>
    <div style="text-align:left; width:30%; float:right"><asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
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
                       <a href='CustomersEdit.aspx?CustomerId=<%# Eval("CustomerId") %>' 
                         meta:resourcekey="lnkGridColumnEdit"><asp:Label ID="lbCustomerName" runat="server"  style="display:none"></asp:Label>
                         <span class="FontBoldMaroon"> <%# Eval("CustomerName")%> </span>
                       </a><br /><br />
                       <a id="popup" onmouseover="ShowMenu('-1-<%# Eval("CustomerId") %>')" href='PaymentTransactions_Add.aspx?CustomerId=<%# Eval("CustomerId") %>' class="thuphi"
                        title="<%# GetLocalResourceObject("lnkGridColumnAddRolesEdit.title").ToString() %>" meta:resourcekey="lnkGridColumnEdit">Thu phí</a>  
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left"/>
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnCustomerInfo" runat="server" Text="Thông tin khách hàng" meta:resourcekey="lblGridColumnCustomerInfo"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate>                                             
                     <img id="Image1" style="border-width:0px;" width="16px" height="16px" src="../../images/user.png"> <span style="color:Maroon; font-weight:bold;"><%# Eval("CustomerFullName")%></span>  <%# "(" + "<span style='color:Red; font-weight:bold;'>" + string.Format("{0:#,##}",Eval("MaxConcurrentLogin")) + "</span>" +")"%><br />
                     <img id="Image2" style="border-width:0px;"  width="16px" height="16px" src="../../images/email_go.png"> <%# Eval("CustomerMail")%><br />
                     <img id="Image3" style="border-width:0px;"  width="16px" height="16px" src="../../images/phone_add.png"> <%# Eval("CustomerMobile")%><br />
                     <img id="Image4" style="border-width:0px;" width="16px" height="16px" src="../../../images/organization-icon.png"> <%# Eval("OrganName")%><br />
                     <img id="Img6" style="border-width:0px; margin-top:2px;" width="16px" height="16px" src="../../../images/phone-icon.png"> <%# Eval("OrganPhone")%><br />
                     <img id="Img7" style="border-width:0px; margin-top:2px;"  width="16px" height="16px" src="../../../images/App-kde-print-fax-icon.png"> <%# Eval("OrganFax")%><br />
                     <img id="Img8" style="border-width:0px; margin-top:2px;" width="16px" height="16px" src="../../../images/house-icon.png"> <%# Eval("OrganAddress")%>
                     <%--<img id="Img2" style="border-width:0px; margin-top:2px;"  src="../../../images/group.png"><span class="xuatban<%# Eval("CustomerGroupId") %>" > <%# LanguageHelpers.IsCultureVietnamese() ? CustomerGroups.Static_Get(short.Parse(Eval("CustomerGroupId").ToString()), l_CustomerGroups).CustomerGroupDesc : CustomerGroups.Static_Get(short.Parse(Eval("CustomerGroupId").ToString()), l_CustomerGroups).CustomerGroupName%></span>--%>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left"/>
                    <HeaderStyle />          
                </asp:TemplateField> 
                <%-- <asp:TemplateField > 
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
                </asp:TemplateField>--%>
               <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnStatus" runat="server" Text="Trạng thái" meta:resourcekey="lblGridColumnStatus"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                    <span class="xuatban<%# Eval("StatusId") %>" > <%# LanguageHelpers.IsCultureVietnamese() ? Status.Static_Get(byte.Parse(Eval("StatusId").ToString()), l_Status).StatusDesc : Status.Static_Get(byte.Parse(Eval("StatusId").ToString()), l_Status).StatusName%></span>                               
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign = "Left" Wrap="false" />  
                    <HeaderStyle/>        
                </asp:TemplateField>
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnTime" runat="server" Text="Thời gian" meta:resourcekey="lblGridColumnTime"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <%# DateTimeHelpers.GetDateAndTime(Eval("CrDateTime"))%>                        
                    </ItemTemplate>
                    <ItemStyle Width="8%" HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="8%" />        
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
    </div>
   </div>
   <div class="clear5px"></div>    
                <uc1:CustomPaging ID="CustomPaging" runat="server" />                
                <div class="clear5px"></div>   
   </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

