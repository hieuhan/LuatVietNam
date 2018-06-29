<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="OrgansSelected.aspx.cs" Inherits="Admin_OrgansSelected" %>
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

            $('a#popup').live('click', function (e) {
                var page = $(this).attr("href")
                var cdialog = $('<div id="divEdit"></div>')
                .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="98%" height="98%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 420,
                    width: 560,
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
    }
    </script>
    <asp:UpdatePanel ID="upn_Grid" runat="server">
      <Triggers>
            <asp:PostBackTrigger ControlID="lbSelectedOrgans" />
            <asp:PostBackTrigger ControlID="lbSelectedOrgans2" />
        </Triggers>
    <ContentTemplate>
    <div style="text-align:center"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
    <table cellpadding="4" cellspacing="0" style="width:100%">
        <tr>
            <td style="width:60px; white-space:nowrap;">
                <asp:Label ID="lblLanguage" runat="server" Text="Ngôn ngữ:" meta:resourcekey="lblLanguage"></asp:Label>
            </td>
            <td style="width:200px">
                <asp:DropDownList ID="ddlLanguage" runat="server" DataTextField="LanguageDesc" Enabled="false"
                    DataValueField="LanguageId" Width="220px" CssClass="userselect" 
                    AutoPostBack="True" onselectedindexchanged="ddlLanguage_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="white-space:nowrap;" colspan="2">
                <asp:CheckBox ID="chkOrganWithDefaultLanguage" runat="server" 
                    AutoPostBack="True" meta:resourcekey="chkOrganWithDefaultLanguage" 
                    oncheckedchanged="chkOrganWithDefaultLanguage_CheckedChanged" 
                    Text="Hiển thị dữ liệu Tiếng Việt" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblReviewStatus" runat="server" Text="Trạng thái:" meta:resourcekey="lblReviewStatus"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlReviewStatus" runat="server" 
                    DataTextField="ReviewStatusDesc" DataValueField="ReviewStatusId" Width="220px" 
                    CssClass="userselect" AutoPostBack="True" 
                    onselectedindexchanged="ddlReviewStatus_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td width="80px">
                <asp:Label ID="lblOrganTypes" runat="server" meta:resourcekey="lblOrganTypes" 
                    Text="Loại cơ quan:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlOrganTypes" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="OrganTypeDesc" 
                    DataValueField="OrganTypeId" 
                    onselectedindexchanged="ddlOrganTypes_SelectedIndexChanged" Width="220px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblOrderBy" runat="server" meta:resourcekey="lblOrderBy" 
                    Text="Sắp xếp:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlOrderBy" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="OrderByDesc" DataValueField="OrderBy" 
                    onselectedindexchanged="ddlOrderBy_SelectedIndexChanged" Width="220px">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lblSearch" runat="server" meta:resourcekey="lblSearch" 
                    Text="Từ khóa:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtSearch" runat="server" CssClass="tukhoatimekiem" Width="210px"></asp:TextBox>
                &nbsp;<asp:LinkButton ID="btnSearch" runat="server" CssClass="timkiembutom" 
                    meta:resourcekey="btnSearch" onclick="btnSearch_Click" Text="Tìm kiếm">
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
        <%=GetLocalResourceObject("Organs").ToString() %>
	</div>
	<div class="chucnangright">
        <asp:Label ID="lblSelectedDisplayTypes" runat="server" meta:resourcekey="lblSelectedDisplayTypes" 
                    Text="Chọn loại hiển thị:"></asp:Label>
        &nbsp;<asp:DropDownList ID="ddlDisplayTypes" runat="server" Enabled="false" 
                    CssClass="userselect" DataTextField="DisplayTypeDesc" 
                    DataValueField="DisplayTypeId" Width="250px">
                </asp:DropDownList>
		&nbsp;<asp:LinkButton ID="lbSelectedOrgans" runat="server" class="themmoi"
            Text="Chọn CQBH" meta:resourcekey="lbSelectedOrgans" onclick="lbSelectedOrgans_Click"> 
        </asp:LinkButton>
	</div>
    <div style="text-align:left; width:50%; float:right"><asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                            <ProgressTemplate><img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...<asp:Label 
                                    ID="lblMessageErrors" runat="server" Text="Label"></asp:Label>
                            </ProgressTemplate>
                            </asp:UpdateProgress></div>
	<div class="clear5px"></div>
    <div class="contenbangdulieu">    
        <asp:GridView ID="m_grid" DataKeyNames="OrganId" runat="server" ShowHeaderWhenEmpty="True"
            AutoGenerateColumns="False"  OnRowDataBound = "m_grid_OnRowDataBound"
            Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" 
            PageSize="20" >
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
                                     ToolTip ='<%# Eval("OrganId").ToString() %>'> </asp:Label>
                    </ItemTemplate>      
                    <ItemStyle HorizontalAlign="Center" Width="3%" />  
                    <HeaderStyle Width="3%" />          
                </asp:TemplateField>        
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnName" runat="server" Text="Tên" meta:resourcekey="lblGridColumnName"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lbOrganName" runat="server" style="display:none"></asp:Label>                        
                        <%# Eval("OrganName") != "" ? Eval("OrganName") : "Chưa có dữ liệu:" + ddlLanguage.SelectedItem.Text.ToString()%> 
                         <br />                       
                          <span style="color: #999999;" >
                           <asp:Label ID="lblDefaultLanguage" runat="server" Text="Tiếng việt:" meta:resourcekey="lblDefaultLanguage" Visible="<%# chkOrganWithDefaultLanguage.Checked==true ? true : false %>" ></asp:Label> <%# (ddlLanguage.SelectedValue == "2") ? (chkOrganWithDefaultLanguage.Checked == true ? Eval("OrganNameDefault") : "") : ""%>
                        </span> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left"/>
                    <HeaderStyle />          
                </asp:TemplateField>
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnDesc" runat="server" Text="Mô tả" meta:resourcekey="lblGridColumnDesc"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lbOrganDesc" runat="server" style="display:none"></asp:Label>                        
                        <%# Eval("OrganDesc")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
                </asp:TemplateField>  
                <%--<asp:TemplateField > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnPageType" runat="server" Text="Loại cơ quan" meta:resourcekey="lblGridColumnPageType"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# LanguageHelpers.IsCultureVietnamese() ? OrganTypes.Static_Get(byte.Parse(Eval("OrganTypeId").ToString()), l_OrganTypes).OrganTypeDesc : OrganTypes.Static_Get(byte.Parse(Eval("OrganTypeId").ToString()), l_OrganTypes).OrganTypeName%>                       
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" />
                    <HeaderStyle />          
                </asp:TemplateField> --%>
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnStatus" runat="server" Text="Trạng thái" meta:resourcekey="lblGridColumnStatus"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <span class="xuatban<%# Eval("ReviewStatusId") %>" >
                            <%# LanguageHelpers.IsCultureVietnamese() ? ReviewStatus.Static_Get(byte.Parse(Eval("ReviewStatusId").ToString()), l_ReviewStatus).ReviewStatusDesc : ReviewStatus.Static_Get(byte.Parse(Eval("ReviewStatusId").ToString()), l_ReviewStatus).ReviewStatusName %>
                        </span> 
                    </ItemTemplate>
                    <ItemStyle Width="8%" HorizontalAlign = "Left" Wrap="false" />  
                    <HeaderStyle Width="8%" />        
                </asp:TemplateField>
                <%--<asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnDisplayOrder" runat="server" Text="Thứ tự" meta:resourcekey="lblGridColumnDisplayOrder"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <span class="thutu">
                            <%# Eval("DisplayOrder").ToString() %>
                        </span> 
                    </ItemTemplate>
                    <ItemStyle Width="7%" HorizontalAlign = "Left" Wrap="false" />  
                    <HeaderStyle Width="7%" />        
                </asp:TemplateField> --%>         
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnTime" runat="server" Text="Thời gian" meta:resourcekey="lblGridColumnTime"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <span class="ngaythang">
                            <%# DateTimeHelpers.GetDateAndTime(Eval("CrDateTime")) %>
                        </span> 
                    </ItemTemplate>
                    <ItemStyle Width="8%" HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="8%" />        
                </asp:TemplateField>
               <%-- <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnUser" runat="server" Text="Tạo bởi" meta:resourcekey="lblGridColumnUser"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <span class="ngaythang">
                            <%# UserHelpers.Static_Get(int.Parse(Eval("CrUserId").ToString()), l_Users, "").Username %>
                        </span> 
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="10%" />        
                </asp:TemplateField> --%>                      
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
   <div class="chucnangright">
		<asp:LinkButton ID="lbSelectedOrgans2" runat="server" class="themmoi"
            Text="Chọn CQBH" meta:resourcekey="lbSelectedOrgans2" onclick="lbSelectedOrgans2_Click"> 
        </asp:LinkButton>
	</div>        
   <div class="clear5px"  ></div>    
                <uc1:CustomPaging ID="CustomPaging" runat="server" />  
                      
                <div class="clear5px"></div>   
   </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
