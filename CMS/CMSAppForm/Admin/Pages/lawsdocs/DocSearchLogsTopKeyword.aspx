<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="DocSearchLogsTopKeyword.aspx.cs" Inherits="Admin_Pages_lawsdocs_DocSearchLogsTopKeyword" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <asp:GridView ID="m_grid"  runat="server" ShowHeader="true"
    AutoGenerateColumns="False" CssClass="grid" PageSize="50" 
    CellPadding="2" GridLines="None">
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
            <ItemStyle HorizontalAlign="Center" Width="3%" />  
            <HeaderStyle Width="3%" />          
        </asp:TemplateField> 
        <asp:TemplateField>
            <HeaderTemplate>                                
                Từ khóa
            </HeaderTemplate> 
            <ItemTemplate> 
                <%# Eval("SearchKeyword") %>
            </ItemTemplate>
        </asp:TemplateField>  
        <asp:TemplateField>
            <HeaderTemplate>                                
                Số lần
            </HeaderTemplate> 
            <ItemTemplate> 
                <%# Eval("Soluong") %>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Right" Width="100px" />
        </asp:TemplateField> 
    </Columns>
    <SortedAscendingCellStyle BackColor="#E9E7E2" />
    <SortedAscendingHeaderStyle BackColor="#506C8C" />
    <SortedDescendingCellStyle BackColor="#FFFDF8" />
    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
</asp:GridView>  
</asp:Content>


