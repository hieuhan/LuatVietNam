<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="DocsContentRelation.aspx.cs" Inherits="Admin_DocsContentRelation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Import Namespace="ICSoft.CMSLib" %>
<%@ Import Namespace="ICSoft.LawDocsLib" %>
<%@ Import Namespace="ICSoft.HelperLib" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" runat="Server">
    <script src="../../Js/ajaxTooltip/ajax-tooltip.js" type="text/javascript"></script>
    <script src="../../Js/ajaxTooltip/ajax-dynamic-content.js" type="text/javascript"></script>
    <script src="../../Js/ajaxTooltip/ajax.js" type="text/javascript"></script>
    <style type="text/css">
        br {
            display: block;
            margin: 4px 0;
        }

        .contenttooltip {
            text-align: center;
            font-family: Arial;
            font-size: 12px;
            font-weight: bold;
            color: #990002;
            vertical-align: middle;
        }

        .ajaxtooltip {
            position: absolute;
            display: none;
            width: 450px;
            left: 0;
            top: 0;
            background: #F7F7CB;
            border: 1px solid gray;
            border-width: 1px 2px 2px 1px;
            padding: 5px;
        }

        .VL_DocName {
            text-align: left;
            font-family: Arial;
            font-size: 12px;
            font-weight: bold;
            color: Black;
        }

        .VL_DocName_Detail {
            text-align: left;
            font-family: Arial;
            font-size: 12px;
            color: Black;
        }

        .DocRowCount {
            text-align: left;
            font-family: Arial;
            font-size: 12px;
            font-weight: bold;
            color: Red;
        }

        .VL_Tip_Property_C1 {
            text-align: left;
            font-family: Arial;
            font-size: 12px;
            font-weight: bold;
            color: #000000;
            width: 120px;
        }

        .VL_Tip_Property_C2 {
            text-align: left;
            font-family: Arial;
            font-size: 12px;
            font-weight: normal;
            color: #000000;
            width: 330px;
        }
    </style>
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
                        window.parent.location = window.parent.location;
                    }
                });
                cdialog.dialog('open');
                e.preventDefault();
            });
            $('a.popup1').live('click', function (e) {
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
    <style>
        .tblTop1 {
            float: left;
        }

        .tblTop2 {
            float: right;
        }
    </style>
    <div style="text-align: center">
        <a WHeight="400" WWidth="1000" href='<%= "DocRelatesEdit.aspx?ViewOnly=1&DocId=" + DocId + "&LanguageId=" + LanguageId %>' class="popup1 docsproperties" title="Văn bản liên quan">D/s liên quan</a>  
        <asp:Label ID="lblMessage" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
    </div>
    <asp:Table ID="tblTop1"
        runat="server"
        Width="48%"
        Visible="False"
        BorderColor="DarkRed"
        BorderWidth="1"
        CellPadding="4"
        CellSpacing="4"
        CssClass="tblTop1">
        <asp:TableRow>
            <asp:TableCell>
                <asp:Repeater ID="rptRelateTypeTop1" runat="server" OnItemDataBound="rptRelateTypeTop1_ItemDataBound">
                    <ItemTemplate>
                        <table width="100%" cellpadding="4px" cellspacing="0px" style="border-collapse: collapse; border: 1px solid #BEC7D2;">
                            <tr>
                                <td style="border-bottom: 1px solid #BEC7D2;">
                                    <asp:Label ID="lbRelateTypeName" runat="server" CssClass="VL_DocName" Text='<%# Eval("RelateTypeName") %>'>                                      
                                    </asp:Label>
                                    <asp:HiddenField ID="hdfRelateTypeId" runat="server" Value='<%# Eval("RelateTypeId") %>' />
                                    <a wheight="600" wwidth="1020" href='<%# "popupDocRelates.aspx?DocId=" + DocId.ToString() + "&LanguageId=1&RelateTypeId=" + Eval("RelateTypeId").ToString() %>' class="docsproperties popup" title="Thêm sửa văn bản liên quan">Chọn</a>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="m_grid" DataKeyNames="DocReferenceId" runat="server"
                                        ShowHeader="false" ShowHeaderWhenEmpty="true"
                                        AutoGenerateColumns="False"
                                        Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0"
                                        PageSize="50">
                                        <HeaderStyle CssClass="trbangdulieutieude" />
                                        <RowStyle CssClass="trbangdulieutieudenoidung" />
                                        <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                                        <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />
                                        <Columns>
                                            <asp:TemplateField Visible="false">
                                                <HeaderTemplate>
                                                    #
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbNo1" runat="server" Text='<%# Container.DataItemIndex + 1%>'>                                    
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="3%" />
                                                <HeaderStyle Width="3%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblGridColumnDocNameDuochuongdanRef" runat="server" Text="Tên văn bản"
                                                        meta:resourcekey="lblGridColumnDocNameDuochuongdanRef"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlkpropertiesDuochuongdanRef" runat="server"
                                                        NavigateUrl='<%# "DocsContentRelation.aspx?DocId=" + Eval("DocReferenceId").ToString() + "&LanguageId=" + LanguageId.ToString()  %>'
                                                        class="VL_DocName_Detail"
                                                        onmouseover='<%# "ShowTooltip(" + chr + "docstips.aspx?DocId=" + Eval( "DocReferenceId") +"&LanguageId=" + LanguageId.ToString() + chr +"," + chr + "tt" + chr + "); return false;" %>'
                                                        onmouseout='HideTooltip();' EnableViewState="False"> <%# Eval("DocNameRef")%></asp:HyperLink>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                <HeaderStyle />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Label ID="lblshowEmptyVBDuochuongdan" runat="server" meta:resourcekey="lblshowEmptyVBDuochuongdan"
                                        Text="..."></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:Repeater>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

    <asp:Table ID="tblTop2"
        runat="server"
        Width="48%"
        Visible="False"
        BorderColor="DarkRed"
        BorderWidth="1"
        CellPadding="4"
        CellSpacing="4"
        CssClass="tblTop2">
        <asp:TableRow>
            <asp:TableCell>
                <asp:Repeater ID="rptRelateTypeTop2" runat="server" OnItemDataBound="rptRelateTypeTop2_ItemDataBound">
                    <ItemTemplate>
                        <table width="100%" cellpadding="4px" cellspacing="0px" style="border-collapse: collapse; border: 1px solid #BEC7D2;">
                            <tr>
                                <td style="border-bottom: 1px solid #BEC7D2;">
                                    <asp:Label ID="lbRelateTypeName" runat="server" CssClass="VL_DocName" Text='<%# Eval("RelateTypeName") %>'>                                      
                                    </asp:Label>
                                    <asp:HiddenField ID="hdfRelateTypeId" runat="server" Value='<%# Eval("RelateTypeId") %>' />
                                    <a wheight="600" wwidth="1020" href='<%# "popupDocRelates.aspx?DocId=" + DocId.ToString() + "&LanguageId=1&RelateTypeId=" + Eval("RelateTypeId").ToString() %>' class="docsproperties popup" title="Thêm sửa văn bản liên quan">Chọn</a>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="m_grid" DataKeyNames="DocReferenceId" runat="server"
                                        ShowHeader="false" ShowHeaderWhenEmpty="true"
                                        AutoGenerateColumns="False"
                                        Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0"
                                        PageSize="50">
                                        <HeaderStyle CssClass="trbangdulieutieude" />
                                        <RowStyle CssClass="trbangdulieutieudenoidung" />
                                        <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                                        <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />
                                        <Columns>
                                            <asp:TemplateField Visible="false">
                                                <HeaderTemplate>
                                                    #
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbNo1" runat="server" Text='<%# Container.DataItemIndex + 1%>'>                                    
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="3%" />
                                                <HeaderStyle Width="3%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblGridColumnDocNameDuochuongdanRef" runat="server" Text="Tên văn bản"
                                                        meta:resourcekey="lblGridColumnDocNameDuochuongdanRef"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlkpropertiesDuochuongdanRef" runat="server"
                                                        NavigateUrl='<%# "DocsContentRelation.aspx?DocId=" + Eval("DocReferenceId").ToString() + "&LanguageId=" + LanguageId.ToString()  %>'
                                                        class="VL_DocName_Detail"
                                                        onmouseover='<%# "ShowTooltip(" + chr + "docstips.aspx?DocId=" + Eval( "DocReferenceId") +"&LanguageId=" + LanguageId.ToString() + chr +"," + chr + "tt" + chr + "); return false;" %>'
                                                        onmouseout='HideTooltip();' EnableViewState="False"> <%# Eval("DocNameRef")%></asp:HyperLink>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                <HeaderStyle />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Label ID="lblshowEmptyVBDuochuongdan" runat="server" meta:resourcekey="lblshowEmptyVBDuochuongdan"
                                        Text="..."></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:Repeater>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <div style="float: left; width: 100%; height: 20px;"></div>
    <table cellpadding="4" cellspacing="4" style="width: 100%;">
        <tr>
            <td width="30%" valign="top">
                <asp:Repeater ID="rptRelateType1" runat="server" OnItemDataBound="rptRelateType1_ItemDataBound">
                    <ItemTemplate>
                        <table width="100%" cellpadding="4px" cellspacing="0px" style="border-collapse: collapse; border: 1px solid #BEC7D2;">
                            <tr>
                                <td style="border-bottom: 1px solid #BEC7D2;">
                                    <asp:Label ID="lbRelateTypeName" runat="server" CssClass="VL_DocName" Text='<%# Eval("RelateTypeName") %>'>                                      
                                    </asp:Label>
                                    <asp:HiddenField ID="hdfRelateTypeId" runat="server" Value='<%# Eval("RelateTypeId") %>' />
                                    <a wheight="600" wwidth="1020" href='<%# "popupDocRelates.aspx?DocId=" + DocId.ToString() + "&LanguageId=1&RelateTypeId=" + Eval("RelateTypeId").ToString() %>' class="docsproperties popup" title="Thêm sửa văn bản liên quan">Chọn</a>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="m_grid" DataKeyNames="DocReferenceId" runat="server"
                                        ShowHeader="false" ShowHeaderWhenEmpty="true"
                                        AutoGenerateColumns="False"
                                        Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0"
                                        PageSize="50">
                                        <HeaderStyle CssClass="trbangdulieutieude" />
                                        <RowStyle CssClass="trbangdulieutieudenoidung" />
                                        <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                                        <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />
                                        <Columns>
                                            <asp:TemplateField Visible="false">
                                                <HeaderTemplate>
                                                    #
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbNo1" runat="server" Text='<%# Container.DataItemIndex + 1%>'>                                    
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="3%" />
                                                <HeaderStyle Width="3%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblGridColumnDocNameDuochuongdanRef" runat="server" Text="Tên văn bản"
                                                        meta:resourcekey="lblGridColumnDocNameDuochuongdanRef"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlkpropertiesDuochuongdanRef" runat="server"
                                                        NavigateUrl='<%# "DocsContentRelation.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + LanguageId.ToString()  %>'
                                                        class="VL_DocName_Detail"
                                                        onmouseover='<%# "ShowTooltip(" + chr + "docstips.aspx?DocId=" + Eval( "DocId") +"&LanguageId=" + LanguageId.ToString() + chr +"," + chr + "tt" + chr + "); return false;" %>'
                                                        onmouseout='HideTooltip();' EnableViewState="False"> <%# Eval("DocName")%></asp:HyperLink>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                <HeaderStyle />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Label ID="lblshowEmptyVBDuochuongdan" runat="server" meta:resourcekey="lblshowEmptyVBDuochuongdan"
                                        Text="..."></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <br />
                    </ItemTemplate>
                </asp:Repeater>
            </td>
            <td width="40%" valign="top">
                <table width="100%" cellpadding="2px" cellspacing="0px" style="border-collapse: collapse; border: 1px solid #FF5C00;">
                    <tr>
                        <td style="border-bottom: 1px solid #FF5C00; text-align: center; vertical-align: middle; height: 30px;">
                            <asp:Label ID="lblVBDangxem" runat="server" meta:resourcekey="lblVBDangxem"
                                Text="VB đang xem" Font-Bold="true" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDocName" Visible="true" runat="server" EnableViewState="false"></asp:Label><br />
                            <br />

                            <asp:Label ID="lbTip" Visible="true" runat="server" EnableViewState="false"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />

                <asp:Table ID="tblVbNgonNguLienQuan"
                    runat="server"
                    Width="100%"
                    BorderColor="#BEC7D2"
                    BorderWidth="1"
                    CellPadding="4"
                    CellSpacing="4">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblDocRelateEn" runat="server" CssClass="VL_DocName" meta:resourcekey="lblDocRelateEn"
                                Text="VB liên quan ngôn ngữ"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:GridView ID="m_grid_ngonngu" DataKeyNames="DocId" runat="server" ShowHeader="false" ShowHeaderWhenEmpty="true"
                                AutoGenerateColumns="False"
                                Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0"
                                PageSize="50">
                                <HeaderStyle CssClass="trbangdulieutieude" />
                                <RowStyle CssClass="trbangdulieutieudenoidung" />
                                <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                                <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />
                                <Columns>
                                    <asp:TemplateField Visible="false">
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
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblGridColumnDocNameNgonnguRef" runat="server" Text="Tên văn bản" meta:resourcekey="lblGridColumnDocNameNgonnguRef"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlkpropertiesNgonnguRef" runat="server" NavigateUrl='<%# "DocsContentRelation.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" +  Eval("LanguageId").ToString()  %>' class="VL_DocName_Detail" onmouseover='<%# "ShowTooltip(" + chr + "docstips.aspx?DocId=" + Eval( "DocId") +"&LanguageId=" +  Eval("LanguageId").ToString() + chr +"," + chr + "tt" + chr + "); return false;" %>' onmouseout='HideTooltip();' EnableViewState="False"> <%# Eval("DocName")%></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        <HeaderStyle />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:Label ID="lblshowEmptyVBNgonngu" runat="server" Visible="false" meta:resourcekey="lblshowEmptyVBNgonngu"
                                Text="..."></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <br />
                <asp:Repeater ID="rptRelateType2" runat="server" OnItemDataBound="rptRelateType2_ItemDataBound">
                    <ItemTemplate>
                        <table width="100%" cellpadding="4px" cellspacing="0px" style="border-collapse: collapse; border: 1px solid #BEC7D2;">
                            <tr>
                                <td style="border-bottom: 1px solid #BEC7D2;">
                                    <asp:Label ID="lbRelateTypeName" runat="server" CssClass="VL_DocName" Text='<%# Eval("RelateTypeName") %>'>                                      
                                    </asp:Label>
                                    <asp:HiddenField ID="hdfRelateTypeId" runat="server" Value='<%# Eval("RelateTypeId") %>' />
                                    <a wheight="600" wwidth="1020" href='<%# "popupDocRelates.aspx?DocId=" + DocId.ToString() + "&LanguageId=1&RelateTypeId=" + Eval("RelateTypeId").ToString() %>' class="docsproperties popup" title="Thêm sửa văn bản liên quan">Chọn</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="m_grid" DataKeyNames="DocReferenceId" runat="server"
                                        ShowHeader="false" ShowHeaderWhenEmpty="true"
                                        AutoGenerateColumns="False"
                                        Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0"
                                        PageSize="50">
                                        <HeaderStyle CssClass="trbangdulieutieude" />
                                        <RowStyle CssClass="trbangdulieutieudenoidung" />
                                        <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                                        <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />
                                        <Columns>
                                            <asp:TemplateField Visible="false">
                                                <HeaderTemplate>
                                                    #
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbNo1" runat="server" Text='<%# Container.DataItemIndex + 1%>'>                                    
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="3%" />
                                                <HeaderStyle Width="3%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblGridColumnDocNameDuochuongdanRef" runat="server" Text="Tên văn bản"
                                                        meta:resourcekey="lblGridColumnDocNameDuochuongdanRef"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlkpropertiesDuochuongdanRef" runat="server"
                                                        NavigateUrl='<%# "DocsContentRelation.aspx?DocId=" + Eval("DocReferenceId").ToString() + "&LanguageId=" + LanguageId.ToString()  %>'
                                                        class="VL_DocName_Detail"
                                                        onmouseover='<%# "ShowTooltip(" + chr + "docstips.aspx?DocId=" + Eval( "DocReferenceId") +"&LanguageId=" + LanguageId.ToString() + chr +"," + chr + "tt" + chr + "); return false;" %>'
                                                        onmouseout='HideTooltip();' EnableViewState="False"> <%# Eval("DocNameRef")%></asp:HyperLink>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                <HeaderStyle />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Label ID="lblshowEmptyVBDuochuongdan" runat="server" meta:resourcekey="lblshowEmptyVBDuochuongdan"
                                        Text="..."></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <br />
                    </ItemTemplate>
                </asp:Repeater>
            </td>
            <td width="30%" valign="top">
                <asp:Repeater ID="rptRelateType3" runat="server" OnItemDataBound="rptRelateType3_ItemDataBound">
                    <ItemTemplate>
                        <table width="100%" cellpadding="4px" cellspacing="0px" style="border-collapse: collapse; border: 1px solid #BEC7D2;">
                            <tr>
                                <td style="border-bottom: 1px solid #BEC7D2;">
                                    <asp:Label ID="lbRelateTypeName" runat="server" CssClass="VL_DocName" Text='<%# Eval("RelateTypeName") %>'>                                      
                                    </asp:Label>
                                    <asp:HiddenField ID="hdfRelateTypeId" runat="server" Value='<%# Eval("RelateTypeId") %>' />
                                    <a wheight="600" wwidth="1020" href='<%# "popupDocRelates.aspx?DocId=" + DocId.ToString() + "&LanguageId=1&RelateTypeId=" + Eval("RelateTypeId").ToString()+ "&Revert=1" %>' class="docsproperties popup" title="Thêm sửa văn bản liên quan">Chọn</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="m_grid" DataKeyNames="DocReferenceId" runat="server"
                                        ShowHeader="false" ShowHeaderWhenEmpty="true"
                                        AutoGenerateColumns="False"
                                        Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0"
                                        PageSize="50">
                                        <HeaderStyle CssClass="trbangdulieutieude" />
                                        <RowStyle CssClass="trbangdulieutieudenoidung" />
                                        <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                                        <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />
                                        <Columns>
                                            <asp:TemplateField Visible="false">
                                                <HeaderTemplate>
                                                    #
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbNo1" runat="server" Text='<%# Container.DataItemIndex + 1%>'>                                    
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="3%" />
                                                <HeaderStyle Width="3%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblGridColumnDocNameDuochuongdanRef" runat="server" Text="Tên văn bản"
                                                        meta:resourcekey="lblGridColumnDocNameDuochuongdanRef"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlkpropertiesDuochuongdanRef" runat="server"
                                                        NavigateUrl='<%# "DocsContentRelation.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + LanguageId.ToString()  %>'
                                                        class="VL_DocName_Detail"
                                                        onmouseover='<%# "ShowTooltip(" + chr + "docstips.aspx?DocId=" + Eval( "DocId") +"&LanguageId=" + LanguageId.ToString() + chr +"," + chr + "tt" + chr + "); return false;" %>'
                                                        onmouseout='HideTooltip();' EnableViewState="False"> <%# Eval("DocName")%></asp:HyperLink>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                <HeaderStyle />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Label ID="lblshowEmptyVBDuochuongdan" runat="server" meta:resourcekey="lblshowEmptyVBDuochuongdan"
                                        Text="..."></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <br />
                    </ItemTemplate>
                </asp:Repeater>
            </td>
        </tr>
    </table>
    <div style="background-color: #FFFFFF; bottom: 0px; padding: 8px; position: fixed; right: 1px; text-align: right; width: 100%;">
        <asp:LinkButton ID="btnBack" runat="server" CssClass="quaylai" OnClientClick="windows.history(-1)"
            Text="Quay lại" meta:resourcekey="btnBack"
            OnClick="btnBack_Click">
        </asp:LinkButton>
    </div>
</asp:Content>

