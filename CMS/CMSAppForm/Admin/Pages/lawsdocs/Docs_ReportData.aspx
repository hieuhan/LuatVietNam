<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="Docs_ReportData.aspx.cs" Inherits="Admin_Pages_Docs_ReportData" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/admin/UserControls/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="uc1" %>
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
                    <td style="width: 70px; white-space: nowrap;">
                        <asp:Label ID="lblDateFrom" runat="server" meta:resourcekey="lblDateFrom"
                            Text="Từ ngày:"></asp:Label>
                    </td>
                    <td style="width: 260px">
                        <asp:TextBox ID="txtDateFrom" runat="server" OnTextChanged="txtDateFrom_OnTextChanged" CssClass="tukhoatimekiem"
                            Width="240px"></asp:TextBox>
                    </td>
                    <td style="width: 60px; white-space: nowrap;">
                        <asp:Label ID="lblDateTo" runat="server" meta:resourcekey="lblDateTo"
                            Text="Đến ngày"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDateTo" runat="server" OnTextChanged="txtDateTo_OnTextChanged" CssClass="tukhoatimekiem"
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
                        <asp:Label ID="lblDateToSame" Visible="False" runat="server" meta:resourcekey="lblDateToSame"
                            Text="Đến ngày (cùng kỳ):"></asp:Label>
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
                <div class="chucnangleft"></div>
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
                    <asp:GridView ID="m_grid" DataKeyNames="DocGroupId" runat="server" ShowHeaderWhenEmpty="True"
                        AutoGenerateColumns="False" ShowFooter="True" OnRowDataBound="m_grid_RowDataBound"
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
                                        ToolTip='<%# Eval("DocGroupId").ToString() %>'> </asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="3%" />
                                <HeaderStyle Width="3%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnDataName" runat="server" Text="Văn bản" meta:resourcekey="lblGridColumnDataName"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("DocName")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnSumApproved" runat="server" Text="Đã duyệt" meta:resourcekey="lblGridColumnSumApproved"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("SumByStatus1")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnSumPending" runat="server" Text="Chờ duyệt" meta:resourcekey="lblGridColumnSumPending"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("SumByStatus2")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnSumChanged" runat="server" Text="Tác động thay đổi" meta:resourcekey="lblGridColumnSumChanged"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("SumByStatus3")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnSumSourceLVN" runat="server" Text="Nguồn LVN" meta:resourcekey="lblGridColumnSumSourceLVN"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("SumBySource1")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnSumSourceCTV" runat="server" Text="Nguồn cộng tác viên" meta:resourcekey="lblGridColumnSumSourceCTV"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("SumBySource2")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnSumDownload" runat="server" Text="Lượt tải" meta:resourcekey="lblGridColumnSumDownload"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("SumByDownload1")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnSumDownloadSame" runat="server" Text="Lượt tải cùng kỳ" meta:resourcekey="lblGridColumnSumDownloadSame"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("SumByDownload2")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnSumView" runat="server" Text="Lượt xem" meta:resourcekey="lblGridColumnSumView"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("SumByView1")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnSumViewSame" runat="server" Text="Lượt xem cùng kỳ" meta:resourcekey="lblGridColumnSumViewSame"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("SumByView2")%>
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

