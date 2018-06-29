<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="DocsFielsEdit.aspx.cs" Inherits="Admin_DocsFielsEdit" %>
<%@ Import Namespace="ICSoft.CMSLib" %>
<%@ Import Namespace="ICSoft.LawDocsLib" %>
<%@ Import Namespace="ICSoft.HelperLib" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
<div style="padding:10px;">
 Tên file: <asp:TextBox ID="TextBox1" runat="server" CssClass="tukhoatimekiem"> </asp:TextBox>&nbsp;
    Nguồn file:
    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="userselect" Width="250px">
        <asp:ListItem Value="0"> ... </asp:ListItem>
        <asp:ListItem Value="18">Incom</asp:ListItem>
        <asp:ListItem Value="19">TTX</asp:ListItem>
        <asp:ListItem Value="20">ASEM</asp:ListItem>
        <asp:ListItem Value="21">Thu thập</asp:ListItem>
        <asp:ListItem Value="22">Convert</asp:ListItem>
         <asp:ListItem Value="48">Dành cho website khác</asp:ListItem>
    </asp:DropDownList>
&nbsp;<asp:LinkButton ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" CssClass="timkiembutom" /><br/><br/>  
    Chọn file: <asp:FileUpload ID="FileUpload1" runat="server" />
</div>
 <div class="chucnangleft">
		<span class="tieudetongcong">
            <asp:Label ID="lblTotalText" runat="server" Text="Tổng cộng:" meta:resourcekey="lblTotalText"></asp:Label>
        </span>
        <asp:Label ID="lblTong" runat="server" Text="" CssClass="tieudetongcong2"></asp:Label> 
	</div>
    <div class="chucnangright">
        <asp:LinkButton ID="lbCheckfile" runat="server" CssClass="duyettin" Text="Duyệt" 
            meta:resourcekey="lbCheckfile" onclick="lbCheckfile_Click"> 
        </asp:LinkButton>
        <asp:LinkButton ID="lbUnCheck" runat="server" CssClass="boduyet" 
            Text="Bỏ duyệt" meta:resourcekey="lbUnCheck" onclick="lbUnCheck_Click"> 
        </asp:LinkButton>
        <asp:LinkButton ID="lbDelete" runat="server" CssClass="xoatin" Text="Xóa" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa tất cả các file đã chọn?')" 
            meta:resourcekey="lbDelete" onclick="lbDelete_Click">
        </asp:LinkButton>		
	</div><br />
<table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td align="left">            
                     <asp:GridView ID="m_grid_File" DataKeyNames="DocFileId" runat="server" ShowHeader="true"
                        AutoGenerateColumns="False" CssClass="grid" PageSize="50" 
                        OnRowDeleting="m_grid_File_RowDeleting" CellPadding="2" GridLines="None">
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
                                    Tên file
                                </HeaderTemplate> 
                                <ItemTemplate>                                    
                                    <%# Eval("DocFileName")%>
                                    <asp:Label ID="lblDocFileName" runat="server" Text='<%# Eval("DocFileName")%>' Visible="false"></asp:Label>
                                </ItemTemplate>
                                 <ItemStyle HorizontalAlign="Center" Width="20%" />  
                                <HeaderStyle Width="20%" />   
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnFilePath" runat="server" Text="Đường dẫn" meta:resourcekey="lblGridColumnFilePath"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate>                                    
                                    <a href='/Download.aspx?file=<%# Eval("FilePath") %>'><%# Eval("FilePath")%></a>   
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnFileSize" runat="server" Text="Kích thước" meta:resourcekey="lblGridColumnFileSize"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate>                                    
                                    <%# int.Parse(Eval("FileSize").ToString())/1024%>Kb
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField  > 
                               <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnDataSources" runat="server" Text="Nguồn" meta:resourcekey="lblGridColumnDataSources"></asp:Label>
                                </HeaderTemplate>  
                                <ItemTemplate> 
                                   <%#  LanguageHelpers.IsCultureVietnamese() ? DataSources.Static_Get(short.Parse(Eval("DataSourceId").ToString()), l_DataSources).DataSourceDesc : DataSources.Static_Get(short.Parse(Eval("DataSourceId").ToString()), l_DataSources).DataSourceName%>
                                </ItemTemplate>
                                <ItemStyle Width="8%" HorizontalAlign = "Left" VerticalAlign="Top" Wrap="false" />  
                                <HeaderStyle Width="8%" />        
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnStatus" runat="server" Text="Trạng thái"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate>  
                                    <span class="xuatban<%# Eval("ReviewStatusId") %>" > <%# LanguageHelpers.IsCultureVietnamese() ? ReviewStatus.Static_Get(byte.Parse(Eval("ReviewStatusId").ToString()), l_ReviewStatus).ReviewStatusDesc : ReviewStatus.Static_Get(byte.Parse(Eval("ReviewStatusId").ToString()), l_ReviewStatus).ReviewStatusName%></span>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField >
                                <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnDel" runat="server" Text="Xóa" meta:resourcekey="lblGridColumnDel"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate>
                                         <asp:LinkButton ID="lbDelete" runat="server" CssClass="iconadmxoa" CommandName="Delete" ToolTip="Xóa" meta:resourcekey="lbDelete"  OnClientClick="return confirm('Bạn có thực sự muốn xóa dữ liệu này?');"></asp:LinkButton> 
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:TemplateField> 
                            <asp:TemplateField>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle Width="30px" />
                                <HeaderTemplate>
                                    <input type="checkbox" name="SelectAllCheckBox" onclick="SelectAll(this)">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkAction" runat="server"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>               
                        </Columns>
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
            </td>
        </tr>
        </table>    
</asp:Content>

