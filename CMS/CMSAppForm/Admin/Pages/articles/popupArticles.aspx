<%@ Page Title="" Language="C#" MasterPageFile="~/admin/MasterPageAdmin.master" AutoEventWireup="true"
    CodeFile="popupArticles.aspx.cs" Inherits="Admin_Pages_articles_popupArticles" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/admin/UserControls/CustomPaging.ascx" TagName="CustomPaging"
    TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" runat="Server">
    <asp:HiddenField runat="server" ID="hdfSelectedItems" Value="" />
    <asp:HiddenField runat="server" ID="hdfParent" Value="" />
    <table class="tableBorder" cellspacing="1" cellpadding="2" width="98%">
        <tr>
            <th class="tableHeaderText" align="left">
                &nbsp;Danh mục tin bài
            </th>
        </tr>
        <tr>
            <td width="100%">
                <div class="main_search">
                    <div class="main-search">
                        <table cellpadding="2" cellspacing="0" style="width: 100%">
                            <tr>
                                <td style="width: 60px">
                                    <asp:Label ID="lblSite" runat="server" Text="Site:" meta:resourcekey="lblSite"></asp:Label>
                                </td>
                                <td style="width: 260px">
                                    <asp:DropDownList ID="ddlSite" runat="server" DataTextField="SiteDesc" DataValueField="SiteId"
                                        Width="250px" CssClass="userselect" AutoPostBack="True" OnSelectedIndexChanged="ddlSite_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 90px; white-space: nowrap;">
                                    <asp:Label ID="lblDataType" runat="server" Text="Loại dữ liệu:" meta:resourcekey="lblDataType"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlDataType" runat="server" DataTextField="DataTypeDesc" DataValueField="DataTypeId"
                                        Width="250px" CssClass="userselect" AutoPostBack="True" OnSelectedIndexChanged="ddlDataType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:CheckBox ID="chkShowTop" runat="server" Text="ShowTop" />&nbsp;
                                    <asp:CheckBox ID="chkShowBottom" runat="server" Text="ShowBottom" />&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 60px; white-space: nowrap;">
                                    <asp:Label ID="lblLanguage" runat="server" Text="Ngôn ngữ:" meta:resourcekey="lblLanguage"></asp:Label>
                                </td>
                                <td style="width: 260px">
                                    <asp:DropDownList ID="ddlLanguage" runat="server" DataTextField="LanguageDesc" DataValueField="LanguageId"
                                        Width="250px" CssClass="userselect" AutoPostBack="True" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 90px; white-space: nowrap;">
                                    <asp:Label ID="lblAppType" runat="server" Text="Loại ứng dụng:" meta:resourcekey="lblAppType"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlAppType" runat="server" DataTextField="ApplicationTypeDesc"
                                        DataValueField="ApplicationTypeId" Width="250px" CssClass="userselect" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlAppType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:CheckBox ID="chkShowWeb" runat="server" Text="ShowWeb" />&nbsp;
                                    <asp:CheckBox ID="chkShowWap" runat="server" Text="ShowWap" />&nbsp;
                                    <asp:CheckBox ID="chkShowApp" runat="server" Text="ShowApp" />&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 60px; white-space: nowrap;">
                                    <asp:Label ID="lblCategory" runat="server" Text="Chuyên mục:" meta:resourcekey="lblCategory"></asp:Label>
                                </td>
                                <td style="width: 260px">
                                    <asp:DropDownList ID="ddlCategory" runat="server" DataTextField="CategoryDesc" DataValueField="CategoryId"
                                        Width="250px" CssClass="userselect" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 90px; white-space: nowrap;">
                                    <asp:Label ID="lblSource" runat="server" Text="Nguồn:" meta:resourcekey="lblSource"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlDataSource" runat="server" DataTextField="DataSourceDesc"
                                        DataValueField="DataSourceId" Width="250px" CssClass="userselect" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlDataSource_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:CheckBox ID="chkIsVerify" runat="server" Text="Đã kiểm tra thông tin" meta:resourcekey="chkIsVerify" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblReviewStatus" runat="server" Text="Trạng thái:" meta:resourcekey="lblReviewStatus"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlReviewStatus" runat="server" DataTextField="ReviewStatusDesc"
                                        DataValueField="ReviewStatusId" Width="250px" CssClass="userselect" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlReviewStatus_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label ID="lblOrderBy" runat="server" Text="Sắp xếp:" meta:resourcekey="lblOrderBy"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlOrderBy" runat="server" DataTextField="OrderByDesc" DataValueField="OrderBy"
                                        Width="250px" CssClass="userselect" AutoPostBack="True" OnSelectedIndexChanged="ddlOrderBy_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 60px; white-space: nowrap;">
                                    <asp:Label ID="lblDateFrom" runat="server" Text="Từ ngày:" meta:resourcekey="lblDateFrom"></asp:Label>
                                </td>
                                <td style="width: 260px">
                                    <asp:TextBox ID="txtDateFrom" runat="server" CssClass="tukhoatimekiem" Width="100px"></asp:TextBox>
                                    <asp:Label ID="lblDateTo" runat="server" Text="đến:" meta:resourcekey="lblDateTo"></asp:Label>
                                    <asp:TextBox ID="txtDateTo" runat="server" CssClass="tukhoatimekiem" Width="100px"></asp:TextBox>
                                </td>
                                <td style="width: 90px; white-space: nowrap;">
                                    <asp:Label ID="lblKeyword" runat="server" Text="Từ khóa:" meta:resourcekey="lblKeyword"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="tukhoatimekiem" Width="240px"></asp:TextBox>&nbsp;&nbsp;
                                    <asp:LinkButton ID="btnSearch" runat="server" CssClass="timkiembutom" Text="Tìm kiếm"
                                        meta:resourcekey="btnSearch" OnClick="btnSearch_Click"> </asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <center>
                        <div class="main-search">
                            <table width='100%' border='0' cellpadding='0' cellspacing=''>
                                <tr>
                                    <td width='50%' align="left">
                                        <br />
                                        <b>
                                            <asp:Label ID="lblRecord" runat="server"></asp:Label></b>
                                    </td>
                                    <td align="right">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </center>
                    <asp:GridView ID="m_grid" DataKeyNames="ArticleId" runat="server" PageSize="20" AllowPaging="false"
                        PagerSettings-Mode="Numeric" PagerSettings-PageButtonCount="10" PagerSettings-Position="Bottom"
                        PagerStyle-HorizontalAlign="Center" PagerSettings-NextPageText="Next" PagerSettings-PreviousPageText="Prev"
                        CellPadding="4" ForeColor="#333333" ShowHeader="true" AutoGenerateColumns="False"
                        Width="100%" ShowFooter="false" OnRowDataBound="m_grid_RowDataBound">
                        <FooterStyle BackColor="#c2d7f8" Font-Bold="True" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#c2d7f8" Font-Bold="True" ForeColor="#000080" />
                        <EditRowStyle BackColor="#999999" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnTitle" runat="server" Text="Tiêu đề" meta:resourcekey="lblGridColumnTitle"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%--<a href='#' style="color:Blue; font-weight:bold;"><%#Eval("Title")%></a><br />
                                Mã tin: <%#Eval("ArticleId")%><br />--%>
                                    <a href='ArticlesEdit.aspx?articleId=<%#Eval("ArticleId")%>' style="color: Blue;
                                        font-weight: bold;">
                                        <%#Eval("Title")%></a><br />
                                    Mã tin:
                                    <%#Eval("ArticleId")%><br />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="68%" />
                            </asp:TemplateField>
                           <%-- <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblCate" runat="server" Text="Chuyên mục" meta:resourcekey="lblCate"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <span class="ngaythang">
                                        <%# Eval("CategoryId")%>
                                    </span>
                                </ItemTemplate>
                                <ItemStyle Width="5%" HorizontalAlign="Center" Wrap="false" />
                                <HeaderStyle Width="5%" />
                            </asp:TemplateField>--%>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblGridColumnSelect" runat="server" Text="Chọn" meta:resourcekey="lblGridColumnSelect"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblChkSelect"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="5%" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td align="right">
                <div class="clear5px">
                </div>
                <uc1:CustomPaging ID="CustomPaging" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Button runat="server" ID="btnArticleSearch" Text="Chọn" meta:resourcekey="btnArticleSearch"
                    Width="60px" OnClientClick="return btnSelect_Click();" />
                <asp:Button runat="server" ID="btnArticleClose" Text="Chọn" meta:resourcekey="btnArticleClose"
                    Width="60px" OnClientClick="btnClose_Click();" />
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        function itemCheck(itemObj, itemValue) {
            var objhdfSelectedItems = document.getElementById("<%=hdfSelectedItems.ClientID %>");
            if (itemObj.checked) {
                if (objhdfSelectedItems.value == '') {
                    objhdfSelectedItems.value += ',';
                }
                //            alert(itemValue);
                objhdfSelectedItems.value += itemValue + ',';
            }
            else {
                objhdfSelectedItems.value = objhdfSelectedItems.value.replace(',' + itemValue + ',', ',')
            }
            if (objhdfSelectedItems.value == ',') {
                objhdfSelectedItems.value = '';
            }
        }
        function btnSelect_Click() {
            var objhdfSelectedItems = document.getElementById("<%=hdfSelectedItems.ClientID %>");
            if (objhdfSelectedItems.value == '') {
                alert('Chưa có chuyên mục nào được chọn!');
                return false;
            }
            else {
                //            alert(objhdfSelectedItems.value);
                var parent = document.getElementById("<%=hdfParent.ClientID %>").value;
                objhdfSelectedItems.value = objhdfSelectedItems.value.substring(1, objhdfSelectedItems.value.length - 1);
                if (parent == 'article') {
                    window.opener.updateRelateArticles(objhdfSelectedItems.value);
                }
                else if (parent == 'articleComments') {
                    window.opener.updateArticlesList(objhdfSelectedItems.value);
                }
                window.close();
            }
        }
        function btnClose_Click() {
            window.close();
        }
    </script>
</asp:Content>
