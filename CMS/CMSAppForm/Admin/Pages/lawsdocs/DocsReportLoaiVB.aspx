<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="DocsReportLoaiVB.aspx.cs" Inherits="Admin_Pages_lawsdocs_DocsReportLoaiVB" %>
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
                        height: 360,
                        width: 580,
                        title: $(this).attr("title"),
                        close: function (event, ui) {
                            $(this).remove();
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
        }
    </script>

    <asp:UpdatePanel ID="upn_Grid" runat="server">
    <ContentTemplate>
    <div style="text-align:center"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
    <table cellpadding="4" cellspacing="0" style="width:100%">
          <%--<tr>
            <td style="width:90px; white-space:nowrap;">
                <asp:Label ID="lblLanguage" runat="server" Text="Nhóm:" meta:resourcekey="lblDocGroup"></asp:Label>
            </td>
            <td style="width:260px">
                <asp:DropDownList ID="ddlDocGroup" runat="server" DataTextField="DocGroupDesc" 
                    DataValueField="DocGroupId" Width="250px" CssClass="userselect" 
                    AutoPostBack="True" onselectedindexchanged="ddlDocGroups_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="white-space:nowrap;" colspan="2">
                
            </td>
        </tr>--%>
        <tr>
            <td>
                <asp:Label ID="lblWeek" runat="server" meta:resourcekey="lblWeek" 
                    Text="Tuần:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlWeek" runat="server" AutoPostBack="true" CssClass="userselect" Width="250px" onselectedindexchanged="ddlWeek_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td width="90px">
                <asp:Label ID="lblYear" runat="server" 
                    meta:resourcekey="lblYear" Text="Năm:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" CssClass="userselect" Width="250px" onselectedindexchanged="ddlYear_SelectedIndexChanged">
                </asp:DropDownList>
                
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblMonth" runat="server" meta:resourcekey="lblMonth" 
                    Text="Tháng:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="True" 
                    CssClass="userselect" 
                     Width="250px" onselectedindexchanged="ddlMonth_SelectedIndexChanged">
                </asp:DropDownList>
                
            </td>
            <td>
                <asp:Label ID="lblDateFrom" runat="server" Text="Từ ngày:" meta:resourcekey="lblDateFrom"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDateFrom" runat="server" CssClass="tukhoatimekiem" Width="100px"></asp:TextBox>
                <asp:Label ID="lblDateTo" runat="server" meta:resourcekey="lblDateTo" 
                    Text="Đến:"></asp:Label>
                <asp:TextBox ID="txtDateTo" runat="server" CssClass="tukhoatimekiem" Width="100px"></asp:TextBox>
                &nbsp;&nbsp;
               <asp:LinkButton ID="btnSearch" runat="server" CssClass="timkiembutom" 
                Text="Tìm kiếm" meta:resourcekey="btnSearch" onclick="btnSearch_Click" > </asp:LinkButton>
            </td>
        </tr>
    </table>
    <div class="clear5px"></div>
	<div class="vien"></div>
	<div class="clear5px"></div>  
    <div class="khungchucnang">

    <div style="text-align:left; width:30%; float:right"><asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                            <ProgressTemplate><img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...</ProgressTemplate>
                            </asp:UpdateProgress></div>
	<div class="clear5px"></div>
    <div class="contenbangdulieu">
    
        <asp:GridView ID="m_grid" DataKeyNames="DocId" runat="server" ShowHeaderWhenEmpty="True"
            AutoGenerateColumns="False" 
            Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" PageSize="100">
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
                    <asp:Label ID="lbNo" runat="server" Text='<%# Container.DataItemIndex + 1%>'> </asp:Label>
                    </ItemTemplate>      
                    <ItemStyle HorizontalAlign="Center" Width="3%" />  
                    <HeaderStyle Width="3%" />          
                </asp:TemplateField>        
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnName" runat="server" Text="Văn bản" meta:resourcekey="lblGridColumnName"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lbDocTypeId" runat="server" style="display:none"></asp:Label>                        
                        <%# Eval("Loaivanban")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left"/>
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnDesc" runat="server" Text="Đã duyệt" meta:resourcekey="lblGridColumnDaDuyet"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lbFieldDesc" runat="server" style="display:none"></asp:Label>                        
                        <%# Eval("DaDuyet")%> 
                    </ItemTemplate>
                    <ItemStyle Width="8%"  HorizontalAlign="Center" Wrap="false" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnDisplayOrder" runat="server" Text="Chờ duyệt" meta:resourcekey="lblGridColumnChoDuyet"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <span class="ngaythang">
                            <%# Eval("ChoDuyet")%>
                        </span> 
                    </ItemTemplate>
                    <ItemStyle Width="8%" HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="8%" />        
                </asp:TemplateField>
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnStatus" runat="server" Text="Tác động thay đổi" meta:resourcekey="lblGridColumnTacDongThayDoi"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <span class="ngaythang">
                            <%# Eval("TacDongThayDoi") %>
                        </span> 
                    </ItemTemplate>
                    <ItemStyle Width="9%" HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="9%" />        
                </asp:TemplateField>   
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnStatus" runat="server" Text="Nguồn LVN" meta:resourcekey="lblGridColumnTacDongThayDoi"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <span class="ngaythang">
                            <%# Eval("NguonLVN") %>
                        </span> 
                    </ItemTemplate>
                    <ItemStyle Width="9%" HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="9%" />        
                </asp:TemplateField> 
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnStatus" runat="server" Text="Nguồn Ngoài" meta:resourcekey="lblGridColumnTacDongThayDoi"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <span class="ngaythang">
                            <%# Eval("NguonNgoai") %>
                        </span> 
                    </ItemTemplate>
                    <ItemStyle Width="9%" HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="9%" />        
                </asp:TemplateField>       
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnTime" runat="server" Text="Lượt tải" meta:resourcekey="lblGridColumnDownload"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <span class="ngaythang">
                            <%# Eval("Luottai") %>
                        </span> 
                    </ItemTemplate>
                    <ItemStyle Width="8%" HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="8%" />        
                </asp:TemplateField>
            </Columns>
            
        </asp:GridView>
    </div>
           <div class="clear5px"></div>    
                <uc1:CustomPaging ID="CustomPaging" runat="server" />                
                <div class="clear5px"></div>   
   </div>
    </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

