<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="popupDocRelates.aspx.cs" Inherits="Admin_DocRelatesEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/admin/UserControls/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="uc1" %>
<%@ Import Namespace="ICSoft.CMSLib" %>
<%@ Import Namespace="ICSoft.LawDocsLib" %>
<%@ Import Namespace="ICSoft.HelperLib" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" runat="Server">
    <script src="../../Js/ajaxTooltip/ajax-tooltip.js" type="text/javascript"></script>
    <script src="../../Js/ajaxTooltip/ajax-dynamic-content.js" type="text/javascript"></script>
    <script src="../../Js/ajaxTooltip/ajax.js" type="text/javascript"></script>
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

    <table cellpadding="2" cellspacing="0" style="width: 100%">
        <tr>
            <td style="width: 90px; white-space: nowrap;">
                <asp:Label ID="lblKeyword" runat="server" Text="Từ khóa:" meta:resourcekey="lblKeyword"></asp:Label>
            </td>
            <td>
                <asp:Panel runat="server" DefaultButton="btnSearch">
                <asp:TextBox ID="txtSearchKeyword" runat="server" CssClass="tukhoatimekiem" Width="240px"></asp:TextBox>&nbsp;&nbsp;
                    </asp:Panel></td>
            <td>
                
                <asp:LinkButton ID="btnSearch" runat="server" CssClass="timkiembutom"
                    Text="Tìm kiếm" OnClick="lbSearch_Click" meta:resourcekey="btnSearch">
                </asp:LinkButton>
            </td>
                
            <td></td>
        </tr>
        <tr>
            <td style="width: 60px; white-space: nowrap;">
                <asp:Label ID="lblDateFrom" runat="server" Text="Tìm theo:"></asp:Label>
            </td>
            <td style="width: 260px">
                <asp:RadioButtonList ID="rblFindTypes" runat="server" Style="display: inline-block; vertical-align: bottom;"
                    RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="DocIdentity">Số hiệu</asp:ListItem>
                    <asp:ListItem  Value="DocId">ID văn bản</asp:ListItem>
                    <%--<asp:ListItem Value="DocName">Tiêu đề</asp:ListItem>
                    <asp:ListItem Value="DocContent">Nội dung</asp:ListItem>--%>
                </asp:RadioButtonList>
            </td>
            <td></td>
            <td></td>
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
            <%=GetLocalResourceObject("Docs").ToString() %>
        </div>
        <div class="chucnangright">
            <asp:LinkButton ID="lbAddDocRelates" runat="server" CssClass="themmoi" OnClick="lbAddDocRelates_Click" Text="Thêm"> </asp:LinkButton>
        </div>
        <div style="text-align: left; width: 200px; float: right"></div>
        <div class="clear5px"></div>
        <div class="contenbangdulieu">

            <asp:GridView ID="m_grid" DataKeyNames="DocId" runat="server" ShowHeaderWhenEmpty="True" PageSize="50" 
                AutoGenerateColumns="False" CssClass="filter-table"
                Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0">
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
                            <asp:Label ID="lbNo" runat="server" Text='<%# Container.DataItemIndex + (CustomPaging.PageIndex - 1) * m_grid.PageSize + 1%>'
                                ToolTip='<%# Eval("DocId").ToString() %>'> </asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="3%" />
                        <HeaderStyle Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblGridColumnDocName" runat="server" Text="Tên văn bản" meta:resourcekey="lblGridColumnDocName"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbDocName" runat="server" Text='<%# Eval("DocName")%>' Style="font-weight: bold;"></asp:Label>

                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <HeaderStyle />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblGridColumnDocIdentity" runat="server" Text="Ký hiệu" meta:resourcekey="lblGridColumnDocIdentity"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbDocIdentity" runat="server" Style="display: none"></asp:Label>
                            <%# Eval("DocIdentity")%>
                            <br />
                            <br />
                            <span style="color: Blue;"><%# LanguageHelpers.IsCultureVietnamese() ? EffectStatus.Static_Get(byte.Parse(Eval("EffectStatusId").ToString()), l_EffectStatus).EffectStatusDesc : EffectStatus.Static_Get(byte.Parse(Eval("EffectStatusId").ToString()), l_EffectStatus).EffectStatusName%></span>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblGridColumnProperties" runat="server" Text="Thuộc tính" meta:resourcekey="lblGridColumnProperties"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 60px; text-align: left;">
                                        <asp:Label ID="lblGridColumnBH" CssClass="properties" runat="server" Text="BH:" meta:resourcekey="lblGridColumnBH"></asp:Label></td>
                                    <td><span style="color: Black;"><%# DateTimeHelpers.GetDateHH24(Eval("IssueDate"))%></span></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblGridColumnHL" runat="server" CssClass="properties" Text="HL:" meta:resourcekey="lblGridColumnHL"></asp:Label></td>
                                    <td><span style="color: Black;"><%# DateTimeHelpers.GetDateHH24(Eval("EffectDate"))%></span></td>
                                </tr>
                                <tr style="display: none">
                                    <td>
                                        <asp:Label ID="lblGridColumnCQBH" runat="server" CssClass="properties" Text="CQBH:" meta:resourcekey="lblGridColumnCQBH"></asp:Label></td>
                                    <td><span style="color: Black;"><%# DocOrgans_GetOrganName(byte.Parse(Eval("LanguageId").ToString()),int.Parse(Eval("DocId").ToString())) %></span></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblGridColumnFields" runat="server" CssClass="properties" Text="Lĩnh vực:" meta:resourcekey="lblGridColumnFields"></asp:Label></td>
                                    <td><span style="color: Black;"><%# DocFields_GetFieldName(byte.Parse(Eval("LanguageId").ToString()),int.Parse(Eval("DocId").ToString())) %></span></td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                        <HeaderStyle />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblGridColumnStatus" runat="server" Text="Trạng thái" meta:resourcekey="lblGridColumnStatus"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <span class="xuatban<%# Eval("ReviewStatusId") %>" style="width: 100%;text-align:center;"><%# LanguageHelpers.IsCultureVietnamese() ? ReviewStatus.Static_Get(byte.Parse(Eval("ReviewStatusId").ToString()), l_ReviewStatus).ReviewStatusDesc : ReviewStatus.Static_Get(byte.Parse(Eval("ReviewStatusId").ToString()), l_ReviewStatus).ReviewStatusName%></span><br />
                            <br />
                            <span style="color: #0C0C0C;"><%# UserHelpers.Static_Get(int.Parse(Eval("CrUserId").ToString()), l_Users, "").Username %></span><br />
                            <span style="color: #0C0C0C;"><%# DateTimeHelpers.GetDateAndTime(Eval("CrDateTime"))%></span>
                        </ItemTemplate>
                        <ItemStyle Width="8%" HorizontalAlign="Left" VerticalAlign="Top" Wrap="false" />
                        <HeaderStyle Width="8%" />
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false">
                        <HeaderTemplate>
                            <asp:Label ID="lblGridColumnOperation" runat="server" Text="Thao tác" meta:resourcekey="lblGridColumnOperation"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbDelete" runat="server" CssClass="iconadmxoa" CommandName="Delete" ToolTip="Xóa" meta:resourcekey="lnkGridColumnDel"></asp:LinkButton>
                            <asp:Label ID="lblLanguageId" runat="server" Text='<%# Eval("LanguageId") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" Width="3%" Wrap="false" />
                        <HeaderStyle Width="3%" />
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
        <div class="clear5px"></div>
        <uc1:CustomPaging ID="CustomPaging" runat="server" />
    </div>
</asp:Content>
