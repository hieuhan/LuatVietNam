<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="ArticleDocUpdate.aspx.cs" Inherits="Admin_ArticleDocUpdate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/admin/UserControls/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="uc1" %>
<%@ Import Namespace="ICSoft.CMSLib" %>
<%@ Import Namespace="ICSoft.HelperLib" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    
    <asp:UpdatePanel ID="upn_Grid" runat="server">
    <ContentTemplate>
    <div style="text-align:center"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
    <table cellpadding="2" cellspacing="0" style="width:100%">  
        <tr>
            
            <td style="width:120px;">
                 <asp:Label ID="lblDocGroups" runat="server" 
                    meta:resourcekey="lblDocGroups" Text="Nhóm văn bản:"></asp:Label>
            </td>
            <td style="width:250px;">
               <asp:DropDownList ID="ddlDocGroups" runat="server" AutoPostBack="true" 
                    CssClass="userselect" onselectedindexchanged="ddlDocGroups_SelectedIndexChanged" Width="250px">
                        <asp:ListItem Value="0" Text=" Tất cả "></asp:ListItem>
                        <asp:ListItem Value="1" Text="Văn bản pháp quy"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Văn bản UBND"></asp:ListItem>
                        <asp:ListItem Value="5" Text="Văn bản hợp nhất"></asp:ListItem>
                        <asp:ListItem Value="6" Text="Công văn"></asp:ListItem>
                        <asp:ListItem Value="8" Text="VB Không có ND download"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width:100px;">
            <asp:Label ID="lblLanguages" runat="server" meta:resourcekey="lblLanguages" 
                    Text="Ngôn ngữ:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="True" 
                    CssClass="userselect" DataTextField="LanguageDesc" DataValueField="LanguageId" 
                    onselectedindexchanged="ddlLanguage_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
                
                <span style="margin: 0 20px 0 10px;">Hiệu lực:</span>
                
                <asp:DropDownList ID="ddlEffectStatus" runat="server" AutoPostBack="False" 
                    CssClass="userselect" DataTextField="EffectStatusDesc" 
                    DataValueField="EffectStatusId" 
                    onselectedindexchanged="ddlEffectStatus_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>      
        <tr>
            <td>
                 <asp:Label ID="lblFields" runat="server" meta:resourcekey="lblFields" 
                    Text="Lĩnh vực:"></asp:Label>
               
            </td>
            <td>
                 <asp:DropDownList ID="ddlFields" runat="server" AutoPostBack="False" 
                    CssClass="userselect uiselect" DataTextField="FieldDesc" DataValueField="FieldId" 
                    onselectedindexchanged="ddlFields_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
                </td>
            <td>
                <asp:Label ID="lblUseStatus" runat="server" meta:resourcekey="lblUseStatus" 
                    Text="Sử dụng:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlUseStatus" runat="server" AutoPostBack="False" 
                    CssClass="userselect" DataTextField="UseStatusDesc" 
                    DataValueField="UseStatusId" 
                    onselectedindexchanged="ddlUseStatus_SelectedIndexChanged" Width="250px">
                </asp:DropDownList>
                <span style="margin: 0 35px 0 10px;">Duyệt:</span>
                <asp:DropDownList ID="ddlReviewStatus" runat="server" AutoPostBack="False" 
                    CssClass="userselect" DataTextField="ReviewStatusDesc" 
                    DataValueField="ReviewStatusId" 
                    onselectedindexchanged="ddlReviewStatus_SelectedIndexChanged" Width="250px">
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
                    Text="Tìm theo:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlSearchByDate" runat="server" AutoPostBack="False" 
                    CssClass="userselect" 
                    onselectedindexchanged="ddlSearchByDate_SelectedIndexChanged" Width="250px">
                    <asp:ListItem Value="IssueDate" Selected="True">Ngày ban hành</asp:ListItem>
                    <asp:ListItem Value="RevDateTime">Ngày duyệt</asp:ListItem>
                    <asp:ListItem Value="CrDateTime">Ngày tạo</asp:ListItem>
                    <asp:ListItem Value="EffectDate">Ngày có hiệu lực</asp:ListItem>
                    <asp:ListItem Value="ExpireDate">Ngày hết hiệu lực</asp:ListItem>
                    <asp:ListItem Value="GazetteDate">Ngày công báo</asp:ListItem>
                </asp:DropDownList>
                <span style="margin: 0 40px 0 10px;">
                    <asp:Label ID="Label2" runat="server" Text="Tỉnh:"></asp:Label>
                </span>
                <asp:DropDownList ID="ddlProvinces" runat="server" AutoPostBack="False" CssClass="userselect"
                            DataTextField="ProvinceDesc" DataValueField="ProvinceId" 
                            Width="250px">
                        </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td valign="top">
                Từ khóa:
            </td>
            <td valign="top">
                <asp:TextBox ID="txtSearch" runat="server" CssClass="tukhoatimekiem" 
                    Width="240px"></asp:TextBox>
            </td>
            <td valign="top">
                <asp:Label ID="lblSearch0" runat="server" meta:resourcekey="lblSearch" onKeyDown="return submitButton(event);"
                    Text="Tìm trong:" Visible="false"></asp:Label>
            </td>
            <td valign="middle" align="left">
               <table cellpadding="0" cellspacing="0" border="0">   
               <tr>
                    <td>                  
                    <asp:RadioButtonList ID="rblFindTypes" runat="server" 
                    RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="DocIdentity">Số hiệu</asp:ListItem>
                    <asp:ListItem Value="DocName">Tiêu đề</asp:ListItem>
                    <asp:ListItem Value="DocContent">Nội dung</asp:ListItem>                   
                    </asp:RadioButtonList></td>
                    <td>                 
                    <asp:CheckBox ID="ckbIsSearchExact" runat="server" AutoPostBack="False" 
                    Checked="false" oncheckedchanged="ckbIsSearchExact_CheckedChanged" 
                    Text="Chính xác cụm từ" /></td> 
                    <td>                  
                    &nbsp;&nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" CssClass="timkiembutom" 
                    meta:resourcekey="btnSearch" onclick="btnSearch_Click" Text="Tìm kiếm">
                    </asp:Button></td>
                </tr>
                </table>  
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
        <a href="ArticleDocUpdateGen.aspx?LanguageId=<%=LanguageId %>&ArticleId=<%=ArticleId %>&GenType=<%=GenType %>" title="Tạo bản tin" class="themmoi" > 
            <asp:Label ID="lblAddNew" runat="server" Text="Tạo bản tin" meta:resourcekey="lblAddNew"></asp:Label>
        </a> 
        <asp:LinkButton ID="lbDelete" runat="server" CssClass="xoatin" Text="Xóa" 
            meta:resourcekey="lbDelete" onclick="lbDelete_Click">
        </asp:LinkButton>        
	</div>
    <div style="text-align:left; width:200px; float:right"><asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                            <ProgressTemplate><img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...</ProgressTemplate>
                            </asp:UpdateProgress></div>

    <div>
        <table width="100%" border="0">
            <tr>
                <td valign="top">
                    <div class="contenbangdulieu" style=" width:auto; height:360px; overflow:auto;">
                    <asp:GridView ID="m_grid" DataKeyNames="DocId" runat="server" ShowHeaderWhenEmpty="True" PageSize="50"
                        AutoGenerateColumns="False" CssClass="filter-table" OnRowDeleting="m_grid_RowDeleting" OnRowDataBound = "m_grid_OnRowDataBound"
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
                                <asp:Label ID="lbNo" runat="server" Text='<%# Container.DataItemIndex + (CustomPaging.PageIndex - 1) * m_grid.PageSize + 1%>'>                                    
                                </asp:Label>
                                </ItemTemplate>      
                                <ItemStyle HorizontalAlign="Center" Width="3%" />  
                                <HeaderStyle Width="3%" />          
                            </asp:TemplateField>        
                            <asp:TemplateField > 
                                    <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnName" runat="server" Text="Văn bản" meta:resourcekey="lblGridColumnName"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <b><%# Eval("DocName")%></b><br />
                                    <span style="color:blue">BH: <%# DateTimeHelpers.GetDateHH24(Eval("IssueDate"))%></span>
                                    <span style="color:blue">; HL: <%# DateTimeHelpers.GetDateHH24(Eval("EffectDate"))%></span>
                                    <span style="color:Red">; <%# ICSoft.LawDocsLib.EffectStatus.Static_Get(byte.Parse(Eval("EffectStatusId").ToString()), l_EffectStatus).EffectStatusDesc%></span>
                                    <span style="color:blue">; <%# DocFields_GetFieldName(byte.Parse(Eval("LanguageId").ToString()),int.Parse(Eval("DocId").ToString())) %></span>
                                    <span style="color:Red">; <%# DocOrgans_GetOrganName(byte.Parse(Eval("LanguageId").ToString()),int.Parse(Eval("DocId").ToString())) %></span>
                                </ItemTemplate>
                                <ItemStyle  HorizontalAlign="Left" />
                                <HeaderStyle />          
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnOperation" runat="server" Text="Thao tác" meta:resourcekey="lblGridColumnOperation"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <asp:LinkButton ID="lbDelete" runat="server" CssClass="themmoi" CommandName="Delete" ToolTip="Chọn" Text="Chọn"></asp:LinkButton> 
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Width="68px" Wrap="false" />
                                <HeaderStyle Width="68px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    </div>
                </td>
                <td valign="top" width="50%">
                    <div class="contenbangdulieu" style=" width:auto; height:360px; overflow:auto;">
                    <asp:GridView ID="m_gridRelate" runat="server" ShowHeaderWhenEmpty="True" PageSize="50"
                        AutoGenerateColumns="False" CssClass="filter-table" OnRowDeleting="m_gridRelate_RowDeleting" OnRowDataBound = "m_gridRelate_OnRowDataBound"
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
                                <asp:Label ID="lbNo" runat="server" Text='<%# Container.DataItemIndex + (CustomPaging.PageIndex - 1) * m_grid.PageSize + 1%>'>                                    
                                </asp:Label>
                                </ItemTemplate>      
                                <ItemStyle HorizontalAlign="Center" Width="3%" />  
                                <HeaderStyle Width="3%" />          
                            </asp:TemplateField>        
                            <asp:TemplateField > 
                                    <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnName" runat="server" Text="Văn bản" meta:resourcekey="lblGridColumnName"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <b><%# Eval("DocName")%></b><asp:Label ID="lblDocId" runat="server" Visible="false" Text='<%# Eval("DocId")%>'></asp:Label>
                                    <br />
                                    <span style="color:blue">BH: <%# DateTimeHelpers.GetDateHH24(Eval("IssueDate"))%></span>
                                    <span style="color:blue">; HL: <%# DateTimeHelpers.GetDateHH24(Eval("EffectDate"))%></span>
                                    <span style="color:Red">; <%# ICSoft.LawDocsLib.EffectStatus.Static_Get(byte.Parse(Eval("EffectStatusId").ToString()), l_EffectStatus).EffectStatusDesc%></span>
                                    <span style="color:blue">; <%# DocFields_GetFieldName(byte.Parse(Eval("LanguageId").ToString()),int.Parse(Eval("DocId").ToString())) %></span>
                                    <span style="color:Red">; <%# DocOrgans_GetOrganName(byte.Parse(Eval("LanguageId").ToString()),int.Parse(Eval("DocId").ToString())) %></span>
                                </ItemTemplate>
                                <ItemStyle  HorizontalAlign="Left" />
                                <HeaderStyle />          
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnOperation" runat="server" Text="Thao tác" meta:resourcekey="lblGridColumnOperation"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <asp:LinkButton ID="lbDelete" runat="server" CssClass="iconadmxoa" CommandName="Delete"  ToolTip="Xóa" Text=""></asp:LinkButton> 
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Width="68px" Wrap="false" />
                                <HeaderStyle Width="68px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                                <HeaderStyle Width="40px" />
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
                </td>
            </tr>
        </table>
        
    </div>
    <div class="clear5px" style="float:left"></div>    
       <div style="float:left"><uc1:CustomPaging ID="CustomPaging" runat="server" /></div>             
   </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
