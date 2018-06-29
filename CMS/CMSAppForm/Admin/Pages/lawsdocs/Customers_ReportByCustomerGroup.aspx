<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="Customers_ReportByCustomerGroup.aspx.cs" Inherits="Admin_Pages_lawsdocs_Customers_ReportByCustomerGroup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/admin/UserControls/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="uc1" %>
<%@ Import Namespace="ICSoft.CMSLib" %>
<%@ Import Namespace="ICSoft.LawDocsLib" %>
<%@ Import Namespace="ICSoft.HelperLib" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" runat="Server">
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
    <%--<asp:UpdatePanel ID="upn_Grid" runat="server">
        <ContentTemplate>--%>
            <div style="text-align: center;">
                <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
            <table cellpadding="2" cellspacing="0" style="width: 100%">
                <tr runat="server" id="lblDateFromTo">
                    <td style="width: 60px; white-space: nowrap;">
                        <asp:Label ID="lblDateFrom" runat="server" meta:resourcekey="lblDateFrom"
                            Text="Từ ngày:"></asp:Label>
                    </td>
                    <td style="width: 260px">
                        <asp:TextBox ID="txtDateFrom" runat="server" OnTextChanged="txtDateFrom_OnTextChanged" AutoPostBack="False" CssClass="tukhoatimekiem"
                            Width="240px"></asp:TextBox>
                    </td>
                    <td style="width: 60px; white-space: nowrap;">
                        <asp:Label ID="lblDateTo" runat="server" meta:resourcekey="lblDateTo"
                            Text="Đến ngày"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDateTo" runat="server" OnTextChanged="txtDateTo_OnTextChanged" AutoPostBack="False" CssClass="tukhoatimekiem"
                            Width="240px"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="lblCompare" runat="server"
                                   Text="So sánh với:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCompare" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCompare_OnSelectedIndexChanged"
                                          CssClass="userselect" Width="250px">
                            <asp:ListItem Value="1" Selected="True">Kỳ trước đó</asp:ListItem>
                            <asp:ListItem Value="2">Năm trước</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:LinkButton ID="btnSearch" runat="server" CssClass="timkiembutom"
                            meta:resourcekey="btnSearch" OnClick="btnSearch_Click" Text="Tìm kiếm">
                        </asp:LinkButton>
                    </td>
                </tr>

            </table>
            <div class="clear5px"></div>
            <div class="vien"></div>
            <div class="clear5px"></div>
            <asp:Button ID="btnXuatExcel" runat="server" Text="Xuất Excel" Width="90px" CssClass="timkiembutom" OnClick="btnXuatExcel_Click" />
            <div class="khungchucnang">
                <div class="chucnangleft">
                    <%--<span class="tieudetongcong">
            <asp:Label ID="lblTotalText" runat="server" Text="Tổng:" meta:resourcekey="lblTotalText"></asp:Label>
        </span>
        <asp:Label ID="lblTong" runat="server" Text="" CssClass="tieudetongcong2"></asp:Label> --%>
                </div>
                <div class="chucnangright">
                </div>
                <div style="text-align: left; width: 50%; float: right">
                    <%--<asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                        <ProgressTemplate>
                            <img style="text-align: center; vertical-align: middle" alt="loading..." src="../../Icons/loading.gif" />
                            Loading...</ProgressTemplate>
                    </asp:UpdateProgress>--%>
                </div>
                <div class="clear5px"></div>
                <div class="contenbangdulieu" runat="server" id="tblbydoctype">
                    <asp:GridView ID="m_grid" DataKeyNames="CustomerGroupId" runat="server" ShowHeaderWhenEmpty="True"
                        AutoGenerateColumns="False" ShowFooter="true" OnRowDataBound="m_grid_RowDataBound"
                        Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" PageSize="50">
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
                                    <asp:Label ID="lbNo" runat="server" Text='<%# Container.DataItemIndex+1 %>'
                                        ToolTip='<%# Eval("CustomerGroupId").ToString() %>'> </asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="3%" />
                                <HeaderStyle Width="3%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnCustomerGroupName" runat="server" Text="Thành viên" meta:resourcekey="lblGridColumnCustomerGroupName"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("CustomerName")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnSumActivated" runat="server" Text="Đã kích hoạt" meta:resourcekey="lblGridColumnSumActivated"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("SumByStatus1")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnSumNotYetActive" runat="server" Text="Chưa kích hoạt" meta:resourcekey="lblGridColumnSumNotYetActive"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("SumByStatus2")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnSumConHan" runat="server" Text="Còn hạn" meta:resourcekey="lblGridColumnSumConHan"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("SumByTime1")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnSumHetHan" runat="server" Text="Hết hạn" meta:resourcekey="lblGridColumnSumHetHan"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("SumByTime2")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        <%--</ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnXuatExcel" />
            <asp:AsyncPostBackTrigger ControlID ="txtDateFrom" EventName ="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID ="txtDateTo" EventName ="TextChanged" />
        </Triggers>
    </asp:UpdatePanel>--%> 
</asp:Content>

