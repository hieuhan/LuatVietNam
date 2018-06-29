<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="popupDocuments.aspx.cs" Inherits="Admin_Pages_lawsdocs_popupDocuments" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/admin/UserControls/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="uc1" %>
<%@ Import Namespace="ICSoft.HelperLib" %>
<%@ Import Namespace="ICSoft.LawDocsLib" %>
<%@ Import Namespace="ICSoft.CMSLib" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" runat="Server">
    <script src="../../Js/ajaxTooltip/ajax-tooltip.js" type="text/javascript"></script>
    <script src="../../Js/ajaxTooltip/ajax-dynamic-content.js" type="text/javascript"></script>
    <script src="../../Js/ajaxTooltip/ajax.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);

            $('a.popup').live('click', function (e) {
                var page = $(this).attr("href")
                var cdialog = $('<div id="divEdit"></div>')
                    .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="98%" height="98%"></iframe>')
                    .dialog({
                        autoOpen: false,
                        modal: true,
                        height: $(this).attr("WHeight"),
                        width: $(this).attr("WWidth"),
                        title: $(this).attr("title"),
                        close: function (event, ui) {
                            $(this).remove();
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
    <asp:HiddenField runat="server" ID="hdfDocId" Value="" />
    <asp:HiddenField runat="server" ID="hdfDocName" Value="" />
    <asp:HiddenField runat="server" ID="hdfResult" Value="" />
    <div style="padding: 10px;">
    <div class="clear5px"></div>
    <table cellpadding="2" cellspacing="0" style="width: 100%">
        <tr>
            <td style="width: 90px; white-space: nowrap;">
                <asp:Label ID="lblKeyword" runat="server" Text="Từ khóa:" meta:resourcekey="lblKeyword"></asp:Label>
            </td>
            <td style="width: 356px;">
                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearch">
                    <asp:TextBox ID="txtSearchKeyword" runat="server" CssClass="tukhoatimekiem" Width="330px"></asp:TextBox>&nbsp;&nbsp;
                </asp:Panel>
            </td>
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
            <td>
                <asp:RadioButtonList ID="rblFindTypes" runat="server" Style="display: inline-block; vertical-align: bottom;"
                    RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="DocIdentity">Số hiệu</asp:ListItem>
                    <asp:ListItem Value="DocName">Tiêu đề</asp:ListItem>
                    <asp:ListItem Value="DocContent">Nội dung</asp:ListItem>
                </asp:RadioButtonList>
                <asp:CheckBox ID="ckbIsSearchExact" runat="server" AutoPostBack="False"
                    Checked="false" OnCheckedChanged="ckbIsSearchExact_CheckedChanged"
                    Text="Chính xác cụm từ" />
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

            <%--<%=GetLocalResourceObject("Docs").ToString() %>--%>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:Repeater ID="rptStatusCount" runat="server">
            <ItemTemplate>
                <span><b><%# Eval("Trang Thai").ToString() %> (<%# Eval("So van ban").ToString() %>)</b></span>&nbsp;&nbsp;
            </ItemTemplate>
        </asp:Repeater>
        </div>
        <div class="chucnangright">
            <input type="button" value="Lựa chọn" onclick="return btnSelect_Click();" />
        </div>
        <div style="text-align: left; width: 40%; float: right"></div>
        <div class="clear5px"></div>
        <div class="contenbangdulieu">

            <asp:GridView ID="m_grid" DataKeyNames="DocId" runat="server" ShowHeaderWhenEmpty="True"
                AutoGenerateColumns="False" OnRowDataBound="m_grid_RowDataBound"
                Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0"
                PageSize="50">
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
                            <a href='<%# Eval("DocGroupId").ToString() == "5" ? "DocsConsolidationEdit.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + Eval("LanguageId") + "&backUrl=Docs.aspx"  :  "DocsEdit.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + Eval("LanguageId") %>' title="Sửa văn bản" style="color: black;">
                                <asp:Label ID="lbDocName" runat="server" Text='<%# Eval("DocName")%>' Style="font-weight: bold;"></asp:Label>
                            </a>
                            <br />
                            <br />
                            <a wheight="510" wwidth="1090" title="Thêm/Sửa thuộc tính" href="<%# "DocsEditPropertie.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + Eval("LanguageId")  %>" class="popup docsproperties" onmouseover="<%# "ShowTooltip('docstips.aspx?DocId=" + Eval( "DocId").ToString() +"&LanguageId=" + Eval( "LanguageId").ToString() + "','tt'); return false;" %>" onmouseout="HideTooltip();">
                                <asp:Label ID="lblGridColumnProperties" runat="server" CssClass="docsproperties" Text="Thuộc tính" meta:resourcekey="lblGridColumnProperties"></asp:Label>
                            </a>
                            <span class="docsproperties">| </span>

                            <a wheight="430" wwidth="580" href='<%# "DocsUpdateTDADEdit.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + Eval("LanguageId") %>' class="popup docsproperties" wheight="430" wwidth="580" title="">
                                <asp:Label ID="lblGridColumnTDAD" runat="server" CssClass="docsproperties" Text="Công báo"></asp:Label>
                            </a><span class="docsproperties">| </span>

                            <a wheight="600" wwidth="1050" href='<%# "DocRelatesEdit.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + Eval("LanguageId") %>' class="popup docsproperties <%#HasProperties(Eval("HasDocRelate").ToString()) ? "":"active" %>" title="">
                                <asp:Label ID="lblGridColumnLienQuan" runat="server" CssClass="docsproperties" Text="Liên quan" meta:resourcekey="lblGridColumnLienQuan"></asp:Label>
                            </a><span class="docsproperties">| </span>

                            <a wheight="400" wwidth="850" href='<%# "DocsFielsEdit.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + Eval("LanguageId") %>' class="popup docsproperties <%#HasProperties(Eval("HasFile").ToString()) ? "":"active" %>" title="">
                                <asp:Label ID="lblGridColumnFile" runat="server" CssClass="docsproperties" Text="File" meta:resourcekey="lblGridColumnFile"></asp:Label>
                            </a><span class="docsproperties">| </span>

                            <a href='<%# "DocsUpdateContentEdit.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + Eval("LanguageId") + "&backUrl=Docs.aspx" %>' class="docsproperties <%#HasProperties(Eval("HasContent").ToString()) ? "":"active" %>" title="">
                                <asp:Label ID="lblGridColumnContent" runat="server" CssClass="docsproperties" Text="Nội dung" meta:resourcekey="lblGridColumnContent"></asp:Label>
                            </a><span class="docsproperties">| </span>

                            <a href='<%# "DocIndexes.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + Eval("LanguageId") + "&backUrl=Docs.aspx" %>' class="docsproperties <%#HasProperties(Eval("HasDocIndex").ToString()) ? "":"active" %>" title="Mục lục văn bản">
                                <asp:Label ID="Label1" runat="server" CssClass="docsproperties" Text="Mục lục"></asp:Label>
                            </a><span class="docsproperties">| </span>

                            <a wheight="500" wwidth="800" href='<%# "DocsUpdateSummaryEdit.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + Eval("LanguageId") %>' class="popup docsproperties <%# getClassSeo(Eval("DocSummary"),Eval("UpdUserId"))%>" title="">
                                <asp:Label ID="lblGridColumnSummary" runat="server" CssClass="docsproperties" Text="Trích dẫn" meta:resourcekey="lblGridColumnSummary"></asp:Label>
                            </a><span class="docsproperties">| </span>

                            <a href='<%# "DocsContentRelation.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + Eval("LanguageId") + "&backUrl=Docs.aspx" %>' class="docsproperties <%#HasProperties(Eval("HasDocRelate").ToString()) ? "":"active" %>" title="">
                                <asp:Label ID="lblGridColumnContentRelation" runat="server" CssClass="docsproperties" Text="Lược đồ" meta:resourcekey="lblGridColumnContentRelation"></asp:Label>
                            </a><span class="docsproperties">| </span>

                            <a wheight="600" wwidth="800" href='<%# "DocSeoEdit.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + Eval("LanguageId") %>'
                                class="popup docsproperties  <%# getClassSeo(Eval("MetaTitle"),Eval("UpdUserId"))%>" title="Seo Info">
                                <asp:Label ID="Label3" runat="server" CssClass="docsproperties" Text="Seos"></asp:Label>

                            </a>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50%" />
                        <HeaderStyle />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblGridColumnDocIdentity" runat="server" Text="Ký hiệu" meta:resourcekey="lblGridColumnDocIdentity"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbDocIdentity" runat="server" Style="display: none"></asp:Label>
                            <a href='<%# "DocsEdit.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + Eval("LanguageId")  %>' title="Nội dung" style="color: black;"><b><%# LanguageId==1 ? Eval("DocIdentity"):Eval("DocIdentityClear")%></b></a>
                            <br />
                            <br />
                            <span style="color: Blue;"><%# LanguageHelpers.IsCultureVietnamese() ? EffectStatus.Static_Get(byte.Parse(Eval("EffectStatusId").ToString()), l_EffectStatus).EffectStatusDesc : EffectStatus.Static_Get(byte.Parse(Eval("EffectStatusId").ToString()), l_EffectStatus).EffectStatusName%></span>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                        <HeaderStyle />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblGridColumnProperties" runat="server" Text="Thuộc tính" meta:resourcekey="lblGridColumnProperties"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <table border="0px" cellpadding="0" cellspacing="0">
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
                                <%--<tr style ="display:none">
                                <td ><asp:Label ID="lblGridColumnCQBH" runat="server" CssClass="properties" Text="CQBH:" meta:resourcekey="lblGridColumnCQBH"></asp:Label></td>
                                 <td><span style="color:Black;"><%# DocOrgans_GetOrganName(byte.Parse(Eval("LanguageId").ToString()),int.Parse(Eval("DocId").ToString())) %></span></td>
                            </tr>--%>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblGridColumnFields" runat="server" CssClass="properties" Text="Lĩnh vực:" meta:resourcekey="lblGridColumnFields"></asp:Label></td>
                                    <td><span style="color: Black;"><%# DocFields_GetFieldName(byte.Parse(Eval("LanguageId").ToString()),int.Parse(Eval("DocId").ToString())) %></span></td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="15%" />
                        <HeaderStyle />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblFiles" runat="server" Text="Files" meta:resourcekey="lblFiles"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <a wheight="400" wwidth="850" href='<%# "DocsFielsEdit.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + Eval("LanguageId") %>' class="popup docsproperties" title=""><%# Eval("DocId").ToString()+".law" %></a>
                        </ItemTemplate>
                        <ItemStyle Width="5%" HorizontalAlign="Left" VerticalAlign="Top" Wrap="false" />
                        <HeaderStyle Width="5%" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblGridColumnStatus" runat="server" Text="Trạng thái" meta:resourcekey="lblGridColumnStatus"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <span class="xuatban<%# Eval("ReviewStatusId") %>"><%# LanguageHelpers.IsCultureVietnamese() ? ReviewStatus.Static_Get(byte.Parse(Eval("ReviewStatusId").ToString()), l_ReviewStatus).ReviewStatusDesc : ReviewStatus.Static_Get(byte.Parse(Eval("ReviewStatusId").ToString()), l_ReviewStatus).ReviewStatusName%></span><br />
                            <a wheight="400" wwidth="550" href="<%=CmsConstants.ROOT_PATH %>admin/pages/WebNotify/NotifyMessagesEdit.aspx?DocId=<%# Eval("DocId") %>&LanguageId=<%# Eval("LanguageId") %>&BackUrl=Pages/lawsdocs/Docs.aspx" class="popup" style="color: blue;" title="Gửi Web Notify">Gửi Notify</a>
                        </ItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="Left" VerticalAlign="Top" Wrap="false" />
                        <HeaderStyle Width="80px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblGridColumnUserCreate" runat="server" Text="Tạo bởi" meta:resourcekey="lblGridColumnUserCreate"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <span class="ngaythang">
                                <font style="color: Blue"><%# UserHelpers.Static_Get(int.Parse(Eval("CrUserId").ToString()), l_Users, "").Username %></font><br />
                                <%# DateTimeHelpers.GetDateAndTime(Eval("CrDateTime")).Replace(" ","<br />") %>
                            </span>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" Wrap="false" />
                        <HeaderStyle Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblGridColumnUserUpdate" runat="server" Text="Sửa bởi" meta:resourcekey="lblGridColumnUserUpdate"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <span class="ngaythang">
                                <font style="color: Blue"><%# UserHelpers.Static_Get(int.Parse(Eval("UpdUserId").ToString()), l_Users, "").Username %></font><br />
                                <%# DateTimeHelpers.GetDateAndTime(Eval("UpdDateTime")).Replace(" ","<br />") %>
                            </span>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" Wrap="false" />
                        <HeaderStyle Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblGridColumnUserReview" runat="server" Text="Duyệt bởi" meta:resourcekey="lblGridColumnUserReview"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <span class="ngaythang">
                                <font style="color: Blue"><%# UserHelpers.Static_Get(int.Parse(Eval("RevUserId").ToString()), l_Users, "").Username %></font><br />
                                <%# DateTimeHelpers.GetDateAndTime(Eval("RevDateTime")).Replace(" ","<br />") %>
                            </span>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" Wrap="false" />
                        <HeaderStyle Width="50px" />
                    </asp:TemplateField>
                    <%--<asp:TemplateField Visible="false">
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnOperation" runat="server" Text="Thao tác" meta:resourcekey="lblGridColumnOperation"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:LinkButton ID="lbDelete" runat="server" CssClass="iconadmxoa" CommandName="Delete" ToolTip="Xóa" meta:resourcekey="lnkGridColumnDel"></asp:LinkButton> 
                        <asp:Label ID="lblLanguageId" runat="server" Text='<%# Eval("LanguageId") %>' Visible="false"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="center" Width="3%" Wrap="false" />
                    <HeaderStyle Width="3%" />
                </asp:TemplateField>--%>
                    <asp:TemplateField>
                        <ItemStyle HorizontalAlign="center" Width="20px" />
                        <HeaderTemplate>
                            <asp:Label ID="lblGridColumnSelect" runat="server" Text="Chọn" meta:resourcekey="lblGridColumnSelect"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblChkSelect"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div class="clear5px"></div>
    <uc1:CustomPaging ID="CustomPaging" runat="server" />
    <div class="clear5px"></div>
    <div style="text-align: right; margin-bottom: 30px">
        <input type="button" value="Lựa chọn" onclick="return btnSelect_Click();" />
        <input type="button" value="Đóng" onclick="btnClose_Click();" />
    </div>
    </div>
    <script type="text/javascript">
        function docSelect(itemObj, itemValue, itemName, itemResult) {
            var objhdfSelectedItems = 0;
            var hdfDocName = '';
            objhdfSelectedItems = document.getElementById("<%=hdfDocId.ClientID %>");
            hdfDocName = document.getElementById("<%=hdfDocName.ClientID %>");
            hdfResult = document.getElementById("<%=hdfResult.ClientID %>");
            if (itemObj.checked) {
                objhdfSelectedItems.value = itemValue;
                hdfDocName.value = itemName;
                hdfResult.value = itemResult;
            }
        }
        function btnSelect_Click() {
            var objhdfSelectedItems = document.getElementById("<%=hdfDocId.ClientID %>");
        var hdfDocName = document.getElementById("<%=hdfDocName.ClientID %>");
            var hdfResult = document.getElementById("<%=hdfResult.ClientID %>");
        if (objhdfSelectedItems.value == '') {
            objhdfSelectedItems.value = '0';
            alert('Chưa có văn bản nào được chọn!');
            return false;
        }
        else {
            window.opener.updateDocId(objhdfSelectedItems.value);
            window.opener.updateDocName(hdfDocName.value);
            window.opener.updateResult(hdfResult.value);
            window.close();
        }
    }
    function btnClose_Click() {
        window.close();
    }
    </script>
</asp:Content>

