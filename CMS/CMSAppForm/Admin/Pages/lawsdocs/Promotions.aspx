<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="Promotions.aspx.cs" Inherits="Admin_Promotions" %>
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
                    height: 480,
                    width: 600,
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
    <div style="text-align:center"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
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
                <asp:Label ID="lblServicePackages" runat="server" 
                    meta:resourcekey="lblServicePackages" Text="Gói dịch vụ:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlServicePackages" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="ServicePackageName" 
                    DataValueField="ServicePackageId" 
                    onselectedindexchanged="ddlServicePackages_SelectedIndexChanged" Width="250px">
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
            <td width="62px">
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
            <td width="250px">
                <asp:TextBox ID="txtSearch" runat="server" CssClass="tukhoatimekiem"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" CssClass="timkiembutom" 
                            meta:resourcekey="btnSearch" onclick="btnSearch_Click" Text="Tìm kiếm">
                </asp:Button>
                <asp:LinkButton ID="btnRefresh" runat="server" CssClass="timkiembutom hidden-btn-dqs" Text="Làm mời" onclick="btnRefresh_Click"></asp:LinkButton>
            </td>
            <td>
                &nbsp;&nbsp;
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
        <%=GetLocalResourceObject("Promotions").ToString()%>
	</div>
	<div class="chucnangright">
		<a id="popup" href="PromotionsEdit.aspx" title="<%=GetLocalResourceObject("lnkAddNew.title").ToString() %>" class="themmoi" > 
            <asp:Label ID="lblAddNew" runat="server" Text="Thêm mới" meta:resourcekey="lblAddNew"></asp:Label>
        </a>
        <asp:LinkButton ID="lbDelete" runat="server" CssClass="xoatin" Text="Xóa" 
            meta:resourcekey="lbDelete" onclick="lbDelete_Click">
        </asp:LinkButton>
	</div>
	<div class="clear5px"></div>
    <div class="contenbangdulieu">
    
        <asp:GridView ID="m_grid" DataKeyNames="PromotionId" runat="server" ShowHeaderWhenEmpty="True"
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
                                     ToolTip ='<%# Eval("PromotionId").ToString() %>'> </asp:Label>
                    </ItemTemplate>      
                    <ItemStyle HorizontalAlign="Center" Width="3%" />  
                    <HeaderStyle Width="3%" />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnName" runat="server" Text="Tên" meta:resourcekey="lblGridColumnName"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lbPromotionName" runat="server" style="display:none"></asp:Label>                        
                        <%# Eval("PromotionName")%> 
                    </ItemTemplate>
                    <ItemStyle Width="15%" HorizontalAlign="Left"/>
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnDesc" runat="server" Text="Mô tả" meta:resourcekey="lblGridColumnDesc"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lbPromotionDesc" runat="server" style="display:none"></asp:Label>                        
                        <%# Eval("PromotionDesc")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left"/>
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnServicePackages" runat="server" Text="Gói dịch vụ" meta:resourcekey="lblGridColumnServicePackages"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# LanguageHelpers.IsCultureVietnamese() ? ServicePackages.Static_Get(short.Parse(Eval("ServicePackageId").ToString()), l_ServicePackages).ServicePackageName : ServicePackages.Static_Get(short.Parse(Eval("ServicePackageId").ToString()), l_ServicePackages).ServicePackageName%>
                    </ItemTemplate>
                    <ItemStyle  Width="10%" HorizontalAlign="Left"/>
                    <HeaderStyle />          
                </asp:TemplateField>
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnPromotionsTime" runat="server" Text="TG khuyến mại" meta:resourcekey="lblGridColumnPromotionsTime"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        Từ: <%# DateTimeHelpers.GetDateAndTime(Eval("BeginTime")) %>
                        <br />
                        Đến: <%# DateTimeHelpers.GetDateAndTime(Eval("EndTime"))%>
                    </ItemTemplate>
                    <ItemStyle Width="12%" HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="12%" />        
                </asp:TemplateField>
               <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnPromotionsValue" runat="server" Text="Giá trị khuyến mại" meta:resourcekey="lblGridColumnPromotionsValue"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate>                        
                            <%# Eval("NumMonthFree")%> <asp:Label ID="lblGridColumnNumMonthFree" runat="server" Text=" tháng" meta:resourcekey="lblGridColumnNumMonthFree"></asp:Label> 
                            <asp:Label ID="lblAnd" runat="server" Text=" và " meta:resourcekey="lblAnd"></asp:Label>    
                             <%# Eval("NumDayFree")%> <asp:Label ID="lblNumDayFree" runat="server" Text=" ngày" meta:resourcekey="lblNumDayFree"></asp:Label> <br />
                             <%--Download: <%# Eval("NumDownload")%>--%> Số lần SD: <%#Eval("UsingCount") %> <br /> Số tiền: <%#Eval("Amount") %> <br /> Phần trăm giảm giá: <%#Eval("PercentDecrease") %> %               
                    </ItemTemplate>
                    <ItemStyle Width="12%" HorizontalAlign = "Left" Wrap="false" />  
                    <HeaderStyle Width="12%" />        
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

                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnUsingType" runat="server" Text="Hình thức sử dụng" meta:resourcekey="lblGridColumnUsingType"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <%# Convert.ToByte(Eval("UsingTypeId")) > 0 ? (Eval("UsingTypeId").ToString() == "1" ? "Một lần" : "Nhiều lần" ) : ""%>
                    </ItemTemplate>
                    <ItemStyle Width="8%" HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="8%" />        
                </asp:TemplateField>

                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnReviewStatus" runat="server" Text="Trạng thái" meta:resourcekey="lblGridColumnReviewStatus"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <%# ReviewStatus.Static_Get(Convert.ToInt32(Eval("ReviewStatusId")),l_ReviewStatus).ReviewStatusDesc%>
                    </ItemTemplate>
                    <ItemStyle Width="8%" HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="8%" />        
                </asp:TemplateField>
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        Mã KM
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <a href='PromotionCodes.aspx?PromotionId=<%# Eval("PromotionId") %>' class="iconadmsua"
                        title="Danh sách mã KM" >
                        </a>   
                    </ItemTemplate>
                    <ItemStyle Width="5%" HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="5%" />        
                </asp:TemplateField>
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnUser" runat="server" Text="Tạo bởi" meta:resourcekey="lblGridColumnUser"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate>                         
                            <%# UserHelpers.Static_Get(int.Parse(Eval("CrUserId").ToString()), l_Users, "").Username %>                        
                    </ItemTemplate>
                    <ItemStyle Width="5%" HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="5%" />        
                </asp:TemplateField>      
                 <asp:TemplateField>
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnOperation" runat="server" Text="Thao tác" meta:resourcekey="lblGridColumnOperation"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <a id="popup" onmouseover="ShowMenu('-1-<%# Eval("PromotionId") %>')" href='PromotionsEdit.aspx?PromotionId=<%# Eval("PromotionId") %>' class="iconadmsua"
                        title="<%# GetLocalResourceObject("lnkGridColumnEdit.title").ToString() %>" meta:resourcekey="lnkGridColumnEdit"></a>    
                        <asp:LinkButton ID="lbDelete" runat="server" CssClass="iconadmxoa" CommandName="Delete" ToolTip="Xóa" meta:resourcekey="lnkGridColumnDel"></asp:LinkButton> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="center" Width="6%" Wrap="false" />
                    <HeaderStyle Width="6%" />
                </asp:TemplateField>
                <asp:TemplateField>
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
</asp:Content>

