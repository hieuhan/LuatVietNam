<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="NewsletterEmailGroupsEdit.aspx.cs" Inherits="Admin_NewsletterEmailGroupsEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Import Namespace="ICSoft.LawDocsLib" %>
<%@ Import Namespace="ICSoft.HelperLib" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
<table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td width="10px">
                <asp:Label ID="lblEmail" runat="server" meta:resourcekey="lblEmail" 
                    Text="Email:"></asp:Label>
                </td>
            <td>
                <asp:Label ID="lblEmailShow" runat="server" Font-Bold="true" Text=""></asp:Label>
                </td>
        </tr>
         <tr>
            <td colspan="2">
                <asp:GridView ID="m_grid" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                    CellPadding="2" DataKeyNames="NewsletterEmailGroupId" ForeColor="#333333" 
                    GridLines="None" Width="100%">
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
                            <ItemStyle HorizontalAlign="Center" Width="6%" />  
                            <HeaderStyle Width="6%" />          
                        </asp:TemplateField> 
                        <asp:TemplateField>
                            <HeaderTemplate>                                
                                <asp:Label ID="lblGridColumnNewsletterGroups" runat="server" Text="Tên nhóm" meta:resourcekey="lblGridColumnNewsletterGroups"></asp:Label>
                            </HeaderTemplate> 
                            <ItemTemplate>
                                <%# LanguageHelpers.IsCultureVietnamese() ? NewsletterGroups.Static_Get(short.Parse(Eval("NewsletterGroupId").ToString()), l_NewsletterGroups).NewsletterGroupDesc : NewsletterGroups.Static_Get(short.Parse(Eval("NewsletterGroupId").ToString()), l_NewsletterGroups).NewsletterGroupName%>
                                <asp:Label ID="lblNewsletterGroupId" runat="server" Text='<%#Eval("NewsletterGroupId").ToString() %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                             <HeaderTemplate>                                
                                <asp:Label ID="lblGridColumnExpireTime" runat="server" Text="Ngày hết hạn" meta:resourcekey="lblGridColumnExpireTime"></asp:Label>
                            </HeaderTemplate> 
                            <ItemTemplate>
                                <asp:TextBox ID="txtExpireTime" runat="server" CssClass="tukhoatimekiem" Width="150px" Text='<%# DateTimeHelpers.GetDateHH24(Eval("ExpireTime")) !="" ? DateTimeHelpers.GetDateHH24(Eval("ExpireTime")) : StrDateTime %>' ></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="150px" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderStyle Width="3%" />
                            <HeaderTemplate>
                                <input type="checkbox" name="SelectAllCheckBox" onclick="SelectAll(this)">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkAction" runat="server" Checked='<%#Eval("NewsletterEmailGroupId").ToString() !="0" ? true : false %>'/>
                            </ItemTemplate>
                            <ItemStyle Width="3%" HorizontalAlign="center" />
                        </asp:TemplateField>
                    </Columns>                    
                </asp:GridView>
                </td>
        </tr>
              <tr>
                  <td></td>
                  <td>&nbsp;</td>
              </tr>
    </table>
    <div style="text-align:center"><asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom" 
                    Text="Lưu thông tin" ValidationGroup="G1" meta:resourcekey="btnSave" 
            onclick="btnSave_Click"> </asp:LinkButton></div>
</asp:Content>

