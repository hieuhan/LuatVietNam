<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="FieldsSelected.aspx.cs" Inherits="Admin_FieldsSelected" %>
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

        });
    function InitializeRequest(sender, args) {
    }

    function EndRequest(sender, args) {
        SetStartup();
    }
    </script>
    <asp:UpdatePanel ID="upn_Grid" runat="server">
     <Triggers>
            <asp:PostBackTrigger ControlID="lbSelectedFields" />
            <asp:PostBackTrigger ControlID="lbSelectedFields2" />            
        </Triggers>
    <ContentTemplate>
    <div style="text-align:center"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
    <table cellpadding="4" cellspacing="0" style="width:100%">
        <tr>
            <td style="width:90px; white-space:nowrap;">
                <asp:Label ID="lblLanguage" runat="server" Text="Ngôn ngữ:" meta:resourcekey="lblLanguage"></asp:Label>
            </td>
            <td style="width:220px">
                <asp:DropDownList ID="ddlLanguage" runat="server" DataTextField="LanguageDesc" Enabled="false"
                    DataValueField="LanguageId" Width="220px" CssClass="userselect" 
                    AutoPostBack="True" onselectedindexchanged="ddlLanguage_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="white-space:nowrap;" colspan="2">
                <asp:CheckBox ID="chkFieldWithDefaultLanguage" runat="server" 
                    AutoPostBack="True" meta:resourcekey="chkFieldWithDefaultLanguage" 
                    oncheckedchanged="chkFieldWithDefaultLanguage_CheckedChanged" 
                    Text="Hiển thị dữ liệu Tiếng Việt" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblParentPage" runat="server" meta:resourcekey="lblParentPage" 
                    Text="Trang cấp cha:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlParentField" runat="server" AutoPostBack="true" 
                    CssClass="userselect" DataTextField="FieldName" DataValueField="FieldId" 
                    onselectedindexchanged="ddlParentField_SelectedIndexChanged" Width="220px">
                </asp:DropDownList>
            </td>
            <td style="width:90px; white-space:nowrap;">
                <asp:Label ID="lblReviewStatus" runat="server" 
                    meta:resourcekey="lblReviewStatus" Text="Trạng thái:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlReviewStatus" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="ReviewStatusDesc" 
                    DataValueField="ReviewStatusId" 
                    onselectedindexchanged="ddlReviewStatus_SelectedIndexChanged" Width="220px">
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
                <asp:Label ID="lblSearch" runat="server" Text="Tìm kiếm:" meta:resourcekey="lblSearch"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtSearch" runat="server" CssClass="tukhoatimekiem" Width="210px"></asp:TextBox>
                &nbsp;&nbsp;
               <asp:LinkButton ID="btnSearch" runat="server" CssClass="timkiembutom" 
                Text="Tìm kiếm" meta:resourcekey="btnSearch" onclick="btnSearch_Click">
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
        <%=GetLocalResourceObject("Fields").ToString() %>
	</div>
	<div class="chucnangright">
        <asp:Label ID="lblSelectedDisplayTypes" runat="server" meta:resourcekey="lblSelectedDisplayTypes" 
                    Text="Chọn loại hiển thị:"></asp:Label>
        &nbsp;<asp:DropDownList ID="ddlDisplayTypes" runat="server" Enabled="false" 
                    CssClass="userselect" DataTextField="DisplayTypeDesc" 
                    DataValueField="DisplayTypeId" Width="250px">
                </asp:DropDownList>
		&nbsp;
		<asp:LinkButton ID="lbSelectedFields" runat="server" class="themmoi"
            Text="Chọn lĩnh vực" meta:resourcekey="lbSelectedFields" onclick="lbSelectedFields_Click"> 
        </asp:LinkButton>
	</div>
    <div style="text-align:left; width:50%; float:right"><asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                            <ProgressTemplate><img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...</ProgressTemplate>
                            </asp:UpdateProgress></div>
	<div class="clear5px"></div>
    <div class="contenbangdulieu">
    
        <asp:GridView ID="m_grid" DataKeyNames="FieldId" runat="server" ShowHeaderWhenEmpty="True"
            AutoGenerateColumns="False" OnRowDataBound = "m_grid_OnRowDataBound"
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
                        <asp:Label ID="lblGridColumnName" runat="server" Text="Tên lĩnh vực" meta:resourcekey="lblGridColumnName"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lbFieldNameTree" runat="server" style="display:none"></asp:Label>                        
                        <%# Eval("FieldName")!="" ? Eval("FieldName") : "Chưa có dữ liệu:" + ddlLanguage.SelectedItem.Text.ToString()%> 
                         <br />                       
                          <span style="color: #999999;" >
                           <asp:Label ID="lblDefaultLanguage" runat="server" Text="Tiếng việt:" meta:resourcekey="lblDefaultLanguage" Visible="<%# chkFieldWithDefaultLanguage.Checked==true ? true : false %>" ></asp:Label> <%# (ddlLanguage.SelectedValue == "2") ? (chkFieldWithDefaultLanguage.Checked==true ? Eval("FieldNameDefault") : "") : "" %>
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
                        <asp:Label ID="lbFieldDesc" runat="server" style="display:none"></asp:Label>                        
                        <%# Eval("FieldDesc")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left"/>
                    <HeaderStyle />          
                </asp:TemplateField> 
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
               <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnTime" runat="server" Text="Thời gian" meta:resourcekey="lblGridColumnTime"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                      <%# DateTimeHelpers.GetDateAndTime(Eval("CrDateTime")) %>
                    </ItemTemplate>
                    <ItemStyle Width="8%" HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="8%" />        
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
   <div class="chucnangright">
		<asp:LinkButton ID="lbSelectedFields2" runat="server" class="themmoi"
            Text="Chọn lĩnh vực" meta:resourcekey="lbSelectedFields2" onclick="lbSelectedFields2_Click"> 
        </asp:LinkButton>
	</div>   
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
