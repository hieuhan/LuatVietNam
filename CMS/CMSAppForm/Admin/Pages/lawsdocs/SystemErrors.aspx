﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="SystemErrors.aspx.cs" Inherits="Admin_SystemErrors" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/admin/UserControls/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="uc1" %>
<%@ Import Namespace="ICSoft.HelperLib" %>
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
    <asp:UpdatePanel ID="upn_Grid" runat="server">
    <ContentTemplate>
    <div style="text-align:center;"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
            <table cellpadding="4" cellspacing="2" width="100%">
            <tr>
                <td style="width: 60px"><asp:Label ID="lblDatabase" runat="server" meta:resourcekey="lblDatabase" 
                    Text="Database:"></asp:Label></td>
                <td width="245px">
                    <asp:DropDownList ID="ddlDb" runat="server" AutoPostBack="True" 
                        CssClass="userselect" onselectedindexchanged="ddlDb_SelectedIndexChanged" 
                        Width="240px">
                        <asp:ListItem Value="1" Selected="True">LawDocs</asp:ListItem>
                        <asp:ListItem Value="2">LawCMS</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td width="50px">
                    &nbsp;</td>
                <td>
                        &nbsp;</td>
            </tr>
                <tr>
                    <td style="width: 60px">
                        <asp:Label ID="lblDateFrom" runat="server" meta:resourcekey="lblDateFrom" 
                            Text="Từ ngày:"></asp:Label>
                    </td>
                    <td >
                        <asp:TextBox ID="txtDateFrom" runat="server" CssClass="tukhoatimekiem" 
                            Width="96px"></asp:TextBox>
                        <asp:Label ID="lblDateTo" runat="server" meta:resourcekey="lblDateTo" 
                            Text="Đến"></asp:Label>
                        <asp:TextBox ID="txtDateTo" runat="server" CssClass="tukhoatimekiem" 
                            Width="96px"></asp:TextBox>
                    </td>
                    <td >
                        <asp:Label ID="lblKeyWords" runat="server" meta:resourcekey="lblKeyWords" 
                            Text="Từ khóa:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="tukhoatimekiem" 
                            Width="210px"></asp:TextBox>
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
	</div>
	<div class="chucnangright">
	</div>
    <div style="text-align:left; width:50%; float:right"><asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                            <ProgressTemplate><img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...</ProgressTemplate>
                            </asp:UpdateProgress></div>
	<div class="clear5px"></div>
    <div class="contenbangdulieu">
 <asp:GridView ID="m_grid" runat="server" AutoGenerateColumns="False" 
                            BorderWidth="0" CellPadding="0" CellSpacing="0" DataKeyNames="SystemErrorId" 
                            PageSize="50" ShowHeaderWhenEmpty="True" 
                            Width="100%">
                            <HeaderStyle CssClass="trbangdulieutieude" />
                            <RowStyle CssClass="trbangdulieutieudenoidung" />
                            <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                            <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        #
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbNo" runat="server" 
                                            Text="<%# Container.DataItemIndex + (CustomPaging.PageIndex - 1) * m_grid.PageSize + 1%>" 
                                            ToolTip='<%# Eval("SystemErrorId").ToString() %>'> </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="3%" />
                                    <HeaderStyle Width="3%" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lblGridColumnErrorLevelId" runat="server" 
                                            meta:resourcekey="lblGridColumnErrorLevelId" Text="ErrorLevelId"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("ErrorLevelId")%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="8%" />
                                    <HeaderStyle />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lblGridColumnSystemErrorInfo" runat="server" 
                                            meta:resourcekey="lblGridColumnSystemErrorInfo" Text="SystemErrorInfo"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("SystemErrorInfo")%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lblGridColumnObjectId" runat="server" 
                                            meta:resourcekey="lblGridColumnObjectId" Text="ObjectId"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("ObjectId")%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="5%" />
                                    <HeaderStyle Width="5%" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lblGridColumnObjectName" runat="server" 
                                            meta:resourcekey="lblGridColumnObjectName" Text="ObjectName"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("ObjectName") %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"/>
                                    <HeaderStyle/>
                                </asp:TemplateField>                               
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lblGridColumnTime" runat="server" 
                                            meta:resourcekey="lblGridColumnTime" Text="CrDateTime"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# DateTimeHelpers.GetDateAndTime(Eval("CrDateTime"))%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="false" />
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
