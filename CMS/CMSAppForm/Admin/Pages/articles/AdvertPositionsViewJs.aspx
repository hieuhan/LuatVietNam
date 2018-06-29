<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="AdvertPositionsViewJs.aspx.cs" Inherits="Admin_AdvertPositionsViewJs" %>
<%@ Import Namespace="ICSoft.CMSLib" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
<script type="text/javascript">
    $(document).ready(function () {
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(InitializeRequest);
        prm.add_endRequest(EndRequest);

        $('a.popup').live('click', function (e) {
            var page = $(this).attr("href");
            var cdialog = $('<div id="divEdit"></div>')
                .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="98%" height="98%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 450,
                    width: 650,
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

    <asp:GridView ID="m_grid" runat="server" AutoGenerateColumns="False" 
        CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Js Script">
                <ItemTemplate>
                    <asp:HyperLink CssClass="popup" ID="HyperLink1" NavigateUrl='<%# CmsConstants.ROOT_PATH + Eval("JsUrl") %>' runat="server"><%# Eval("JsUrl") %></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="View on webpage">
                <ItemTemplate>
                    <asp:HyperLink CssClass="popup" ID="HyperLink2" NavigateUrl='<%# "AdvertPositionsViewWeb.aspx?SiteId=" + Eval("SiteId") + "&CategoryId=" +Eval("CategoryId") %>' runat="server">View</asp:HyperLink>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>
</asp:Content>

