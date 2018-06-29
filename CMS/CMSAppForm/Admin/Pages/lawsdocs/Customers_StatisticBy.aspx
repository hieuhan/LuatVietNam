<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="Customers_StatisticBy.aspx.cs" Inherits="Admin_Customers_StatisticBy" %>
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
        <tr>
            <td style="width:100px; white-space:nowrap;">Site:
            </td>
            <td style="width:260px"><asp:DropDownList ID="ddlSite" runat="server" DataTextField="SiteDesc" 
                            DataValueField="SiteId" Width="250px" CssClass="userselect"
                            AutoPostBack="True" onselectedindexchanged="ddlSite_SelectedIndexChanged">
                        </asp:DropDownList>
            </td>
            <td style="width:110px; white-space:nowrap;"></td>
            <td></td>
        </tr>
        <tr>
            <td style="width:100px; white-space:nowrap;">
                <asp:Label ID="lblServices" runat="server" meta:resourcekey="lblServices" 
                    Text="Dịch vụ:"></asp:Label>
            </td>
            <td width="260px">
                <asp:DropDownList ID="ddlServices" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="ServiceDesc" DataValueField="ServiceId" 
                    onselectedindexchanged="ddlServices_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
            <td style="width:110px; white-space:nowrap;">
                <asp:Label ID="lblStatus" runat="server" meta:resourcekey="lblStatus" 
                    Text="Trạng thái:"></asp:Label>
            </td>
            <td >
                <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="StatusDesc" 
                    DataValueField="StatusId" 
                    onselectedindexchanged="ddlStatus_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblGenders" runat="server" meta:resourcekey="lblGenders" 
                    Text="Giới tính:"></asp:Label>
            </td>
            <td>
                <asp:RadioButtonList ID="rblGenders" runat="server" 
                     RepeatDirection="Horizontal" DataTextField="GenderDesc" 
                    DataValueField="GenderId" AutoPostBack="True" 
                    onselectedindexchanged="rblGenders_SelectedIndexChanged" >
                 </asp:RadioButtonList>
            </td>
            <td>
                <asp:Label ID="lblProvinces" runat="server" meta:resourcekey="lblProvinces" 
                    Text="Tỉnh thành:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlProvinces" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="ProvinceDesc" DataValueField="ProvinceId" 
                    onselectedindexchanged="ddlProvinces_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblOccupations" runat="server" meta:resourcekey="lblOccupations" 
                    Text="Nghề nghiệp:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlOccupations" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="OccupationDesc" DataValueField="OccupationId" 
                    onselectedindexchanged="ddlOccupations_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lblInfoChannels" runat="server" 
                    meta:resourcekey="lblInfoChannels" Text="Kênh thông tin:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlInfoChannels" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="InfoChannelDesc" 
                    DataValueField="InfoChannelId" 
                    onselectedindexchanged="ddlInfoChannels_SelectedIndexChanged" Width="250px">
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
                    <asp:ListItem Value="CrDateTime" Selected="True">Ngày đăng ký</asp:ListItem>
                    <asp:ListItem Value="Month">Tháng đăng ký</asp:ListItem>
                    <asp:ListItem Value="DailyLogin">Ngày login</asp:ListItem>
                    <asp:ListItem Value="MonthlyLogin">Tháng login</asp:ListItem>
                    <asp:ListItem Value="ServiceId">Dịch vụ</asp:ListItem>
                    <asp:ListItem Value="OccupationId">Nghề nghiệp</asp:ListItem>
                    <asp:ListItem Value="ProvinceId">Tỉnh thành</asp:ListItem>
                    <asp:ListItem Value="GroupId">Nhóm</asp:ListItem>
                    <asp:ListItem Value="InfoChannelId">Kênh thông tin</asp:ListItem>                    
                </asp:DropDownList>
           </td>
        </tr>
         <tr>
             <td>
                 <asp:Label ID="lblRegisterNewsletter" runat="server" 
                     meta:resourcekey="lblRegisterNewsletter" Text="Nhận bản tin tuần:"></asp:Label>
             </td>
             <td>
                 <asp:RadioButtonList ID="rblRegisterNewsletter" runat="server" 
                     RepeatDirection="Horizontal">
                     <asp:ListItem Selected="True" Value="255">Tất cả</asp:ListItem>
                     <asp:ListItem Value="1">Có</asp:ListItem>
                     <asp:ListItem Value="0">Không</asp:ListItem>
                 </asp:RadioButtonList>
             </td>
             <td>
                 <asp:Button ID="btnSearch" runat="server" CssClass="timkiembutom" 
                     meta:resourcekey="btnSearch" onclick="btnSearch_Click" Text="Tìm kiếm">
                    </asp:Button>
                 
             </td>
             <td>
                 &nbsp;</td>
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
        <%--<%=GetLocalResourceObject("Customers_StatisticBy").ToString()%>--%>
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
    </asp:UpdatePanel>
</asp:Content>
