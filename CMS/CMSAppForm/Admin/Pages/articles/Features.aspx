<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPageAdmin.master" CodeFile="Features.aspx.cs" Inherits="Admin_Pages_articles_Features" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/admin/UserControls/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="uc1" %>
<%@ Import Namespace="ICSoft.CMSLib" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
<script type="text/javascript">
    $(document).ready(function () {
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(InitializeRequest);
        prm.add_endRequest(EndRequest);
        $('a#popup').live('click', function (e) {
            var page = $(this).attr("href")
            var cdialog = $('<div id="divEdit"></div>')
                .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="98%" height="98%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 500,
                    width: 600,
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

    }
    </script>
    <asp:UpdatePanel ID="upn_Grid" runat="server">
    <ContentTemplate>
    <div style="text-align:center"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
        <table cellpadding="4" cellspacing="0" style="width: 100%">
            <tr>
                <td style="width: 110px; white-space: nowrap;">
                    <asp:Label ID="lblFeatureGroup" runat="server" meta:resourcekey="lblFeatureGroup" Text="Nhóm thuộc tính:"></asp:Label>
                </td>
                <td style="width: 270px; white-space: nowrap;">
                    <asp:DropDownList ID="ddlFeatureGroup" runat="server" AutoPostBack="True" CssClass="userselect"
                        DataTextField="FeatureGroupName" DataValueField="FeatureGroupId" OnSelectedIndexChanged="ddlFeatureGroup_SelectedIndexChanged"
                        Width="250px">
                    </asp:DropDownList>
                </td>
                <td style="width: 50px; white-space: nowrap;">
                    </td>
                <td>
                    <asp:LinkButton ID="btnSearch" runat="server" CssClass="timkiembutom" Text="Tìm kiếm"
                        meta:resourcekey="btnSearch" OnClick="btnSearch_Click">
                    </asp:LinkButton>
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
		<a id="popup" href="FeaturesEdit.aspx?FeatureGroupId=<%=FeatureGroupId %>" title="<%=GetLocalResourceObject("lnkAddNew.Title").ToString() %>" class="themmoi" > 
            <asp:Label ID="lblAddNew" runat="server" Text="Thêm mới" meta:resourcekey="lblAddNew"></asp:Label>
        </a>
        <asp:LinkButton ID="lbDelete" runat="server" CssClass="xoatin" Text="Xóa"  OnClientClick="return confirm('Bạn có chắc muốn xóa toàn bộ dữ liệu đã chọn?')"
                    meta:resourcekey="lbDelete" onclick="lbDelete_Click">
                </asp:LinkButton>
	</div>
    <div style="text-align:left; width:200px; float:right"><asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                            <ProgressTemplate><img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...</ProgressTemplate>
                            </asp:UpdateProgress></div>
	<div class="clear5px"></div>
    <div class="contenbangdulieu">
    
        <asp:GridView ID="m_grid" DataKeyNames="FeatureId" runat="server" ShowHeaderWhenEmpty="True"
            AutoGenerateColumns="False" CssClass="filter-table" 
            OnRowDeleting="m_grid_RowDeleting" OnRowDataBound = "m_grid_OnRowDataBound"
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
                    <asp:Label ID="lbNo" runat="server" Text='<%# Container.DataItemIndex + 1%>'>                                    
                    </asp:Label>
                    </ItemTemplate>      
                    <ItemStyle HorizontalAlign="Center" Width="3%" />  
                    <HeaderStyle Width="3%" />          
                </asp:TemplateField>        
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblName" runat="server" Text="Tên" meta:resourcekey="lblName"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("FeatureName") %> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left"  Width="12%"  />
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblDesc" runat="server" Text="Mô tả" meta:resourcekey="lblDesc"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("FeatureDesc") %> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left"/>
                    <HeaderStyle />          
                </asp:TemplateField>  
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblDataDicType" runat="server" Text="Loại từ điển dữ liệu" meta:resourcekey="lblDataDicType"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                         <%# DataDictionaryTypes.Static_Get(short.Parse(Eval("DataDictionaryTypeId").ToString()), l_DataDicType).DataDictionaryTypeName%>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left"  Width="10%" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblInputType" runat="server" Text="Input type" meta:resourcekey="lblInputType"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# InputTypes.Static_Get(byte.Parse(Eval("InputTypeId").ToString()), l_InputTypes).InputTypeName %>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left"  Width="10%" />
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblDisplayOrder" runat="server" Text="Thứ tự" meta:resourcekey="lblDisplayOrder"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("DisplayOrder") %> 
                    </ItemTemplate>
                    <ItemStyle Width="5%" HorizontalAlign = "Center" Wrap="false" />  
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblCrUserId" runat="server" Text="Người tạo" meta:resourcekey="lblCrUserId"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                    <span class="ngaythang">
                           <%# ICSoft.HelperLib.UserHelpers.Static_Get(int.Parse(Eval("CrUserId").ToString()), l_User, "").Username %>
                        </span>
                    </ItemTemplate>
                    <ItemStyle Width="7%" HorizontalAlign = "Center" Wrap="false" />  
                    <HeaderStyle Width="7%" />        
                </asp:TemplateField> 
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblCrDateTime" runat="server" Text="Ngày tạo" meta:resourcekey="lblCrDateTime"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                    <span class="ngaythang">
                    <%# String.Format("{0:dd/MM/yyyy HH:mm}", Eval("CrDateTime"))%>
                        </span>
                    </ItemTemplate>
                    <ItemStyle Width="7%" HorizontalAlign = "Center" Wrap="false" />  
                    <HeaderStyle Width="7%" />        
                </asp:TemplateField>                                      
                <asp:TemplateField>
                    <HeaderTemplate>                                
                        <asp:Label ID="lblThaotac" runat="server" Text="Thao tác" meta:resourcekey="lblThaotac"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <a id="popup" href='FeaturesEdit.aspx?FeatureId=<%# Eval("FeatureId") %>' class="iconadmsua"
                        title="<%# GetLocalResourceObject("lnkGridColumnEdit.Title").ToString() %>" meta:resourcekey="lnkGridColumnEdit"></a>    
                        <asp:LinkButton ID="lbDelete" runat="server" CssClass="iconadmxoa" CommandName="Delete" ToolTip="Xóa" meta:resourcekey="lnkGridColumnDel"></asp:LinkButton> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="center" Width="5%" Wrap="false" />
                    <HeaderStyle Width="5%" />
                </asp:TemplateField>
                <asp:TemplateField>
                            <ItemStyle HorizontalAlign="Center" Width="40px" />
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
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
