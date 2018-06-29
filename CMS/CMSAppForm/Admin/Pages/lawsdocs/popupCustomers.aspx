<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="popupCustomers.aspx.cs" Inherits="Admin_Pages_lawsdocs_popupCustomers" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/admin/UserControls/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="uc1" %>
<%@ Import Namespace="ICSoft.CMSLib" %>
<%@ Import Namespace="ICSoft.HelperLib" %>
<%@ Import Namespace="ICSoft.LawDocsLib" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);
            $("#<%= txtDateFrom.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
            $("#<%= txtDateTo.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
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
    <asp:HiddenField runat="server" ID="hdfSelectedItems" Value="" />
    <asp:HiddenField runat="server" ID="hdfParent" Value="" />
    <asp:HiddenField runat="server" ID="hdfCrUserName" Value="" />
    <asp:UpdatePanel ID="upn_Grid" runat="server">
    <ContentTemplate>
        <h2 style="color:#1e87c5;">Danh sách khách hàng</h2>
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
                    onselectedindexchanged="ddlChange_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
                
            </td>
            <td width="90px">
                <asp:Label ID="lblCustomerGroups" runat="server" meta:resourcekey="lblCustomerGroups" 
                    Text="Nhóm:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlCustomerGroups" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="CustomerGroupDesc" DataValueField="CustomerGroupId" 
                    onselectedindexchanged="ddlChange_SelectedIndexChanged" Width="250px">
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
                    onselectedindexchanged="ddlChange_SelectedIndexChanged" Width="250px">
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
                    onselectedindexchanged="ddlChange_SelectedIndexChanged" Width="250px">
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
    <div class="clear5px"></div>
	<div class="vien"></div>
	<div class="clear5px"></div>  
    <div class="khungchucnang">
    <div class="chucnangleft">
        <span class="tieudetongcong">
            <asp:Label ID="lblTotalText" runat="server" Text="Tổng cộng:" meta:resourcekey="lblTotalText"></asp:Label>
        </span>
        <asp:Label ID="lblTong" runat="server" Text="" CssClass="tieudetongcong2"></asp:Label> 
	</div>
	<div class="chucnangright">		
	</div>
    <div style="text-align:left; width:200px; float:right"><asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                            <ProgressTemplate><img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...</ProgressTemplate>
                            </asp:UpdateProgress></div>
	<div class="clear5px"></div>
    <div class="contenbangdulieu">
    
        <asp:GridView ID="m_grid" DataKeyNames="CustomerId" runat="server" ShowHeaderWhenEmpty="True" PageSize="50"
            AutoGenerateColumns="False" CssClass="filter-table" OnRowDataBound="m_grid_RowDataBound"
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
                    <asp:Label ID="lbNo" runat="server" Text='<%# Container.DataItemIndex + (CustomPaging.PageIndex - 1) * m_grid.PageSize + 1 %>'>                                    
                    </asp:Label>
                    </ItemTemplate>      
                    <ItemStyle HorizontalAlign="Center" Width="3%" />  
                    <HeaderStyle Width="3%" />          
                </asp:TemplateField>        
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnName" runat="server" Text="Tài khoản" meta:resourcekey="lblGridColumnName"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <b><%# Eval("CustomerName") %></b> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle Width="30%" />          
                </asp:TemplateField>  
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnName" runat="server" Text="Họ tên" meta:resourcekey="lblGridColumnName"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <b><%# Eval("CustomerFullName") %></b> <br />
                        <span>Nhóm:</span><%# CustomerGroups.Static_Get(byte.Parse(Eval("CustomerGroupId").ToString()), l_CustomerGroups).CustomerGroupDesc %>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle Width="30%" />          
                </asp:TemplateField>  
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnStatus" runat="server" Text="Trạng thái" meta:resourcekey="lblGridColumnStatus"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <span class="xuatban<%# Eval("StatusId") %>" >
                            <%# Status.Static_Get(byte.Parse(Eval("StatusId").ToString()), l_Status).StatusDesc %>
                        </span> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign = "Left" Wrap="false" />  
                    <HeaderStyle Width="25%" />        
                </asp:TemplateField>                         
                <asp:TemplateField>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="15%" />
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblChkSelect"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div class="clear5px"></div>    
    <uc1:CustomPaging ID="CustomPaging" runat="server" />  
    <div class="clear5px"></div>
    <div style="text-align:right; margin-bottom: 30px">
        <input type="button" value="Lựa chọn" onclick="return btnSelect_Click();" /> 
        <input type="button" value="Đóng" onclick="btnClose_Click();" />
    </div>            
   </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function itemCheck(itemObj, itemValue, UserName) {
            var objhdfSelectedItems = 0;
            var hdfUserName = '';
            objhdfSelectedItems = document.getElementById("<%=hdfSelectedItems.ClientID %>");
            hdfUserName = document.getElementById("<%=hdfCrUserName.ClientID %>");
            if (itemObj.checked) {
                objhdfSelectedItems.value = itemValue;
                hdfUserName.value = UserName;
            }
        }
        function btnSelect_Click() {
            var objhdfSelectedItems = document.getElementById("<%=hdfSelectedItems.ClientID %>");
            var hdfUserName = document.getElementById("<%=hdfCrUserName.ClientID %>");

            if (objhdfSelectedItems.value == '') {
                objhdfSelectedItems.value = '0';
                alert('Chưa có khách hàng nào được chọn!');

                return false;
            }
            else {
                var parent = document.getElementById("<%=hdfParent.ClientID %>").value;
                    objhdfSelectedItems.value = objhdfSelectedItems.value;
                    //window.parent.$("[id*=txtCustomerId]").val(objhdfSelectedItems.value);
                    //window.parent.$("[id*=txtCrUserName]").val(hdfUserName.value);
                    window.opener.updateCustomerId(objhdfSelectedItems.value);
                    window.opener.updateCustomerName(hdfUserName.value);
                    window.close();
                    //window.opener.updateFocusId(objhdfSelectedItems.value);
                    //window.parent.jQuery('#dialogCustomers').dialog('close');
            }
        }
        function btnClose_Click() {
            window.close();
            //window.parent.jQuery('#dialogCustomers').dialog('close');
        }
</script>
</asp:Content>

