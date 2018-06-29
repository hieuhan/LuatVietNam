<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="ShowGenBTHL.aspx.cs" Inherits="Admin_ShowGenBTHL" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">    
   <%-- <asp:UpdatePanel ID="upn_Grid" runat="server">
    <ContentTemplate>--%>
    <div style="width:auto; height:530px; overflow:auto;" >     
   <asp:Label ID="lblMessage" runat="server" Font-Bold="true" ForeColor="Red" Text=""></asp:Label>
    <table width="100%" cellpadding="0" cellspacing="0" class="bthl-box-countfield">               
        <tr>
         <td valign="top">
           <table width="100%" border="0" class="bthl-box-countfield100">
            <tr>
                <td style="background-color:#ECD6AA; text-align:center; vertical-align:top;">
                 <span style="color:#A40000; font-weight:bold;">DANH SÁCH LĨNH VỰC CẬP NHẬT</span>
                </td>
            </tr>
            <tr>
                <td valign="top" class="bthl-top">
                    <asp:DataList ID="ListFields" runat="server" BackColor="#F3F3F3"
                     RepeatColumns="4" CellPadding="0" CellSpacing="0" Width="100%" class="bthl-listfield">  
                    <ItemTemplate >
                      <table width="100%" style="border:solid 1px #ffffff;" class="bthl-tabfield">
                        <tr>
                        <td valign="middle" class="bthl-img-field"> <img alt="ban tin hieu luc" src="https://luatvietnam.vn/assets/images/icon-luat.png" class="bthl-imgicon" width="15" height="15" />  <%# Eval("FieldDesc")%> <span style="color:Blue;" class="bthl-count-field">(<%# Eval("TotalDoc")%>)</span></td>
                        </tr>
                      </table>                  
                    </ItemTemplate>
                </asp:DataList>  
                </td>
            </tr>
           </table> 
         </td>
        </tr>        
    </table>  
    <table width="100%"  border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style="height:5px;">
                <a id="popup" href="DocRelatesGenBTHLSelect.aspx?ArticleId=<%=ArticleId %>&LanguageId=<%=LanguageId %>" title="Chọn văn bản" class="themmoi" > 
                    Thêm văn bản
                </a>
            </td>
        </tr>
        <tr>
            <td valign="top">
             <asp:GridView ID="m_grid" DataKeyNames="FieldId" runat="server" 
                    ShowHeaderWhenEmpty="True" ShowFooter="false" ShowHeader="false" OnRowDataBound="m_grid_OnRowDataBound"
            AutoGenerateColumns="False" Width="100%" CellPadding="0" CellSpacing="0" 
                    BorderColor="White" BorderWidth="0px" GridLines="None" CssClass="bthl-box">
            <Columns>  
                <asp:TemplateField ItemStyle-VerticalAlign="Top" >                        
                    <ItemTemplate> 
                       <table width="100%" border="0" class="bthl-field">
                        <tr>
                            <td style="background-color:#ECD6AA; color:#A40000; text-align:left; vertical-align:top;" class="bthl-colfield">
                               <b class="bthl-fieldname"><%# ToUpper(Eval("FieldDesc").ToString()) %></b>     
                            </td>                                                        
                        </tr>       
                        <tr>
                            <td valign="top">
                            <asp:Label ID="lblDocRealates" runat="server"></asp:Label>  
                            </td>
                        </tr>                
                       </table>                                         
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" VerticalAlign="Top"/>
                    <HeaderStyle />          
                </asp:TemplateField>                                 
            </Columns>
                 <EditRowStyle BorderColor="White" BorderStyle="Dotted" BorderWidth="0px" />
        </asp:GridView>
            </td>
        </tr>
    </table>   
   
    </div>
     <div style="background-color: #FFFFFF; bottom: 0px;  padding: 2px;  position: fixed;   right: 1px;   text-align: right;  width: 100%;">
         
         &nbsp;<asp:LinkButton ID="btnAddBTHL" runat="server" CssClass="luuvaquaylai" 
            Text="Gen html"  onclick="btnAddBTHL_Click">
        </asp:LinkButton>
        <asp:LinkButton ID="btnExportFileWord" runat="server" CssClass="luuvathemmoi" 
                    Text="Export file Word" onclick="btnExportFileWord_Click"  >
        </asp:LinkButton>
        <asp:LinkButton ID="btnExportFilePDF" runat="server" CssClass="luuvathemmoi"  Visible="false"
                    Text="Export file PDF" onclick="btnExportFilePDF_Click"  >
        </asp:LinkButton>
    </div>
<%--</ContentTemplate>
    </asp:UpdatePanel>--%>
    
</asp:Content>

