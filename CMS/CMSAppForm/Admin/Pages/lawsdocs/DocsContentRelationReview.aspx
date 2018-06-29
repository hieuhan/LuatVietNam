<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="DocsContentRelationReview.aspx.cs" Inherits="Admin_DocsContentRelationReview" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Import Namespace="ICSoft.CMSLib" %>
<%@ Import Namespace="ICSoft.LawDocsLib" %>
<%@ Import Namespace="ICSoft.HelperLib" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
<script src="../../Js/ajaxTooltip/ajax-tooltip.js" type="text/javascript"></script>
<script src="../../Js/ajaxTooltip/ajax-dynamic-content.js" type="text/javascript"></script>
<script src="../../Js/ajaxTooltip/ajax.js" type="text/javascript"></script>
 <style type="text/css"> 
        br { display: block; margin: 4px 0;}       
        .contenttooltip { text-align:center; font-family:Arial; font-size:12px;font-weight:bold;color:#990002; vertical-align:middle;}
        .ajaxtooltip { position: absolute; display: none; width: 450px; left: 0; top: 0; background:#F7F7CB; border: 1px solid gray; border-width: 1px 2px 2px 1px; padding: 5px; }        
        .VL_DocName {text-align:left; font-family:Arial; font-size:12px; font-weight:bold; color:Black;}
        .VL_DocName_Detail {text-align:left; font-family:Arial; font-size:12px; color:Black;}
        .DocRowCount {text-align:left; font-family:Arial; font-size:12px; font-weight:bold; color:Red;}
        .VL_Tip_Property_C1 {text-align:left; font-family:Arial; font-size:12px; font-weight:bold; color:#000000; width:120px;}
        .VL_Tip_Property_C2 {text-align:left; font-family:Arial; font-size:12px; font-weight:normal; color:#000000; width:330px;}
      </style>
<script type="text/javascript">    
    $(document).ready(function () {
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(InitializeRequest);
        prm.add_endRequest(EndRequest);
       
    });
    function InitializeRequest(sender, args) {
    }

    function EndRequest(sender, args) {
      
    }   
    </script>
   <div style="text-align:center"><asp:Label ID="lblMessage" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label></div>
    <table cellpadding="4" cellspacing="4" style="width:100%;">
        <tr>
            <td width="30%" valign="top">
                <table width="100%" cellpadding="4px" cellspacing="0px" style=" border-collapse:collapse; border : 1px solid #BEC7D2;">
                <tr>
                  <td style="border-bottom: 1px solid #BEC7D2;">
                    <asp:Label ID="lblVBDuochuongdan" runat="server" CssClass="VL_DocName" meta:resourcekey="lblVBDuochuongdan" 
                    Text="VB được hướng dẫn"></asp:Label>
                    </td>
                </tr>
                <tr>
                  <td >
                    <asp:GridView ID="m_grid_Duochuongdan" DataKeyNames="DocReferenceId" runat="server" 
                          ShowHeader="false" ShowHeaderWhenEmpty="true"
                        AutoGenerateColumns="False"
                        Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" 
                        PageSize="50" >
                        <HeaderStyle CssClass="trbangdulieutieude" />
                        <RowStyle CssClass="trbangdulieutieudenoidung" />
                        <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                        <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />                                        
                        <Columns>                        
                            <asp:TemplateField Visible="false" >
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
                            <asp:TemplateField > 
                                    <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnDocNameDuochuongdanRef" runat="server" Text="Tên văn bản" 
                                            meta:resourcekey="lblGridColumnDocNameDuochuongdanRef"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <asp:HyperLink ID="hlkpropertiesDuochuongdanRef" runat="server"  
                                        NavigateUrl='<%# "DocsContentRelation.aspx?DocId=" + Eval("DocReferenceId").ToString() + "&LanguageId=" + LanguageId.ToString()  %>' 
                                        class="VL_DocName_Detail" 
                                        onmouseover='<%# "ShowTooltip(" + chr + "/vietlaws/Admin/Pages/lawsdocs/docstips.aspx?DocId=" + Eval( "DocReferenceId") +"&LanguageId=" + LanguageId.ToString() + chr +"," + chr + "tt" + chr + "); return false;" %>' 
                                        onmouseout='HideTooltip();' EnableViewState="False"  > <%# Eval("DocNameRef")%></asp:HyperLink></ItemTemplate>
                                <ItemStyle  HorizontalAlign="Left" VerticalAlign="Top" />
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
              <table width="100%" cellpadding="4px" cellspacing="0px" style=" border-collapse:collapse; border : 1px solid #BEC7D2;">
                <tr>
                  <td style="border-bottom: 1px solid #BEC7D2;">
                    <asp:Label ID="lblVBBisuadoi" runat="server" CssClass="VL_DocName" meta:resourcekey="lblVBBisuadoi" 
                    Text="VB bị sửa đổi, bổ sung"></asp:Label>
                    </td>
                </tr>
                <tr>
                  <td >
                    <asp:GridView ID="m_grid_Bisuadoi" DataKeyNames="DocReferenceId" runat="server" 
                          ShowHeader="false" ShowHeaderWhenEmpty="true"
                        AutoGenerateColumns="False"
                        Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" 
                        PageSize="50" >
                        <HeaderStyle CssClass="trbangdulieutieude" />
                        <RowStyle CssClass="trbangdulieutieudenoidung" />
                        <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                        <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />                                        
                        <Columns>                        
                            <asp:TemplateField Visible="false" >
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
                            <asp:TemplateField > 
                                    <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnDocNameBisuadoiRef" runat="server" Text="Tên văn bản" 
                                            meta:resourcekey="lblGridColumnDocNameBisuadoiRef"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <asp:HyperLink ID="hlkpropertiesBisuadoiRef" runat="server"  
                                        NavigateUrl='<%# "DocsContentRelation.aspx?DocId=" + Eval("DocReferenceId").ToString() + "&LanguageId=" + LanguageId.ToString()  %>' 
                                        class="VL_DocName_Detail" 
                                        onmouseover='<%# "ShowTooltip(" + chr + "/vietlaws/Admin/Pages/lawsdocs/docstips.aspx?DocId=" + Eval( "DocReferenceId") +"&LanguageId=" + LanguageId.ToString() + chr +"," + chr + "tt" + chr + "); return false;" %>' 
                                        onmouseout='HideTooltip();' EnableViewState="False"  > <%# Eval("DocNameRef")%></asp:HyperLink></ItemTemplate>
                                <ItemStyle  HorizontalAlign="Left" VerticalAlign="Top" />
                                <HeaderStyle />          
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                <asp:Label ID="lblshowEmptyVBBisuadoi" runat="server" meta:resourcekey="lblshowEmptyVBBisuadoi" 
                    Text="..."></asp:Label>
                    </td>
                </tr>
             </table>
             <br />
              <table width="100%" cellpadding="4px" cellspacing="0px" style=" border-collapse:collapse; border : 1px solid #BEC7D2;">
                <tr>
                  <td style="border-bottom: 1px solid #BEC7D2;">
                    <asp:Label ID="lblVBHethieuluc" runat="server" CssClass="VL_DocName" meta:resourcekey="lblVBHethieuluc" 
                    Text="VB hết hiệu lực"></asp:Label>
                    </td>
                </tr>
                <tr>
                  <td >
                    <asp:GridView ID="m_grid_Hethieuluc" DataKeyNames="DocReferenceId" runat="server" 
                          ShowHeader="false" ShowHeaderWhenEmpty="true"
                        AutoGenerateColumns="False"
                        Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" 
                        PageSize="50" >
                        <HeaderStyle CssClass="trbangdulieutieude" />
                        <RowStyle CssClass="trbangdulieutieudenoidung" />
                        <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                        <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />                                        
                        <Columns>                        
                            <asp:TemplateField Visible="false" >
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
                            <asp:TemplateField > 
                                    <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnDocNameHethieulucRef" runat="server" Text="Tên văn bản" 
                                            meta:resourcekey="lblGridColumnDocNameHethieulucRef"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <asp:HyperLink ID="hlkpropertiesHethieulucRef" runat="server"  
                                        NavigateUrl='<%# "DocsContentRelation.aspx?DocId=" + Eval("DocReferenceId").ToString() + "&LanguageId=" + LanguageId.ToString()  %>' 
                                        class="VL_DocName_Detail" 
                                        onmouseover='<%# "ShowTooltip(" + chr + "/vietlaws/Admin/Pages/lawsdocs/docstips.aspx?DocId=" + Eval( "DocReferenceId") +"&LanguageId=" + LanguageId.ToString() + chr +"," + chr + "tt" + chr + "); return false;" %>' 
                                        onmouseout='HideTooltip();' EnableViewState="False"  > <%# Eval("DocNameRef")%></asp:HyperLink></ItemTemplate>
                                <ItemStyle  HorizontalAlign="Left" VerticalAlign="Top" />
                                <HeaderStyle />          
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                <asp:Label ID="lblshowEmptyVBHethieuluc" runat="server" meta:resourcekey="lblshowEmptyVBHethieuluc" 
                    Text="..."></asp:Label>
                    </td>
                </tr>
             </table>
             
             <br />
              <table width="100%" cellpadding="4px" cellspacing="0px" style=" border-collapse:collapse; border : 1px solid #BEC7D2;">
                <tr>
                  <td style="border-bottom: 1px solid #BEC7D2;">
                    <asp:Label ID="lblVBHethieulucmotphan" runat="server" CssClass="VL_DocName" meta:resourcekey="lblVBHethieulucmotphan" 
                    Text="Văn bản hết hiệu lực một phần"></asp:Label>
                    </td>
                </tr>
                <tr>
                  <td >
                    <asp:GridView ID="m_grid_Hethieulucmotphan" DataKeyNames="DocReferenceId" runat="server" 
                          ShowHeader="false" ShowHeaderWhenEmpty="true"
                        AutoGenerateColumns="False"
                        Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" 
                        PageSize="50" >
                        <HeaderStyle CssClass="trbangdulieutieude" />
                        <RowStyle CssClass="trbangdulieutieudenoidung" />
                        <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                        <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />                                        
                        <Columns>                        
                            <asp:TemplateField Visible="false" >
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
                            <asp:TemplateField > 
                                    <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnDocNameHethieulucmotphanRef" runat="server" Text="Tên văn bản" 
                                            meta:resourcekey="lblGridColumnDocNameHethieulucmotphanRef"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <asp:HyperLink ID="hlkpropertiesHethieulucmotphanRef" runat="server"  
                                        NavigateUrl='<%# "DocsContentRelation.aspx?DocId=" + Eval("DocReferenceId").ToString() + "&LanguageId=" + LanguageId.ToString()  %>' 
                                        class="VL_DocName_Detail" 
                                        onmouseover='<%# "ShowTooltip(" + chr + "/vietlaws/Admin/Pages/lawsdocs/docstips.aspx?DocId=" + Eval( "DocReferenceId") +"&LanguageId=" + LanguageId.ToString() + chr +"," + chr + "tt" + chr + "); return false;" %>' 
                                        onmouseout='HideTooltip();' EnableViewState="False"  > <%# Eval("DocNameRef")%></asp:HyperLink></ItemTemplate>
                                <ItemStyle  HorizontalAlign="Left" VerticalAlign="Top" />
                                <HeaderStyle />          
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                <asp:Label ID="lblshowEmptyVBHethieulucmotphan" runat="server" meta:resourcekey="lblshowEmptyVBDuochuongdan" 
                    Text="..."></asp:Label>
                    </td>
                </tr>
             </table>
             <br />
              <table width="100%" cellpadding="4px" cellspacing="0px" style=" border-collapse:collapse; border : 1px solid #BEC7D2;">
                <tr>
                  <td style="border-bottom: 1px solid #BEC7D2;">
                    <asp:Label ID="lblVBDuochopnhatgoc" runat="server" CssClass="VL_DocName" meta:resourcekey="lblVBDuochopnhatgoc" 
                    Text="VB được hợp nhất"></asp:Label>
                    </td>
                </tr>
                <tr>
                  <td >
                    <asp:GridView ID="m_grid_DuochopnhatGoc" DataKeyNames="DocReferenceId" runat="server" 
                          ShowHeader="false" ShowHeaderWhenEmpty="true"
                        AutoGenerateColumns="False"
                        Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" 
                        PageSize="50" >
                        <HeaderStyle CssClass="trbangdulieutieude" />
                        <RowStyle CssClass="trbangdulieutieudenoidung" />
                        <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                        <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />                                        
                        <Columns>                        
                            <asp:TemplateField Visible="false" >
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
                            <asp:TemplateField > 
                                    <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnDocNameDuochopnhatgocRef" runat="server" Text="Tên văn bản" 
                                            meta:resourcekey="lblGridColumnDocNameDuochopnhatgocRef"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <asp:HyperLink ID="hlkpropertiesDuochopnhatgocRef" runat="server"  
                                        NavigateUrl='<%# "DocsContentRelation.aspx?DocId=" + Eval("DocReferenceId").ToString() + "&LanguageId=" + LanguageId.ToString()  %>' 
                                        class="VL_DocName_Detail" 
                                        onmouseover='<%# "ShowTooltip(" + chr + "/vietlaws/Admin/Pages/lawsdocs/docstips.aspx?DocId=" + Eval( "DocReferenceId") +"&LanguageId=" + LanguageId.ToString() + chr +"," + chr + "tt" + chr + "); return false;" %>' 
                                        onmouseout='HideTooltip();' EnableViewState="False"  > <%# Eval("DocNameRef")%></asp:HyperLink></ItemTemplate>
                                <ItemStyle  HorizontalAlign="Left" VerticalAlign="Top" />
                                <HeaderStyle />          
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                <asp:Label ID="lblshowEmptyVBDuochopnhatgoc" runat="server" meta:resourcekey="lblshowEmptyVBDuochopnhatgoc" 
                    Text="..."></asp:Label>
                    </td>
                </tr>
             </table>
             <table width="100%" cellpadding="4px" cellspacing="0px" style=" border-collapse:collapse; border : 1px solid #BEC7D2;">
                <tr>
                  <td style="border-bottom: 1px solid #BEC7D2;">
                    <asp:Label ID="lblVBDuochopnhatsuadoi" runat="server" CssClass="VL_DocName" meta:resourcekey="lblVBDuochopnhatsuadoi" 
                    Text="VB được hợp nhất (sửa đổi)"></asp:Label>
                    </td>
                </tr>
                <tr>
                  <td >
                    <asp:GridView ID="m_grid_DuochopnhatSuadoi" DataKeyNames="DocReferenceId" runat="server" 
                          ShowHeader="false" ShowHeaderWhenEmpty="true"
                        AutoGenerateColumns="False"
                        Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" 
                        PageSize="50" >
                        <HeaderStyle CssClass="trbangdulieutieude" />
                        <RowStyle CssClass="trbangdulieutieudenoidung" />
                        <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                        <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />                                        
                        <Columns>                        
                            <asp:TemplateField Visible="false" >
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
                            <asp:TemplateField > 
                                    <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnDocNameDuochopnhatsuadoiRef" runat="server" Text="Tên văn bản" 
                                            meta:resourcekey="lblGridColumnDocNameDuochopnhatsuadoiRef"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <asp:HyperLink ID="hlkpropertiesDuochopnhatsuadoiRef" runat="server"  
                                        NavigateUrl='<%# "DocsContentRelation.aspx?DocId=" + Eval("DocReferenceId").ToString() + "&LanguageId=" + LanguageId.ToString()  %>' 
                                        class="VL_DocName_Detail" 
                                        onmouseover='<%# "ShowTooltip(" + chr + "/vietlaws/Admin/Pages/lawsdocs/docstips.aspx?DocId=" + Eval( "DocReferenceId") +"&LanguageId=" + LanguageId.ToString() + chr +"," + chr + "tt" + chr + "); return false;" %>' 
                                        onmouseout='HideTooltip();' EnableViewState="False"  > <%# Eval("DocNameRef")%></asp:HyperLink></ItemTemplate>
                                <ItemStyle  HorizontalAlign="Left" VerticalAlign="Top" />
                                <HeaderStyle />          
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                <asp:Label ID="lblshowEmptyVBDuochopnhatsuadoi" runat="server" meta:resourcekey="lblshowEmptyVBDuochopnhatsuadoi" 
                    Text="..."></asp:Label>
                    </td>
                </tr>
             </table>
             <br />
              <table width="100%" cellpadding="4px" cellspacing="0px" style=" border-collapse:collapse; border : 1px solid #BEC7D2;">
                <tr>
                  <td style="border-bottom: 1px solid #BEC7D2;">
                    <asp:Label ID="lblVBBidinhchihieuluc" runat="server" CssClass="VL_DocName" meta:resourcekey="lblVBBidinhchihieuluc" 
                    Text="VB bị đình chỉ"></asp:Label>
                    </td>
                </tr>
                <tr>
                  <td >
                    <asp:GridView ID="m_grid_Bidinhchihieuluc" DataKeyNames="DocReferenceId" runat="server" 
                          ShowHeader="false" ShowHeaderWhenEmpty="true"
                        AutoGenerateColumns="False"
                        Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" 
                        PageSize="50" >
                        <HeaderStyle CssClass="trbangdulieutieude" />
                        <RowStyle CssClass="trbangdulieutieudenoidung" />
                        <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                        <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />                                        
                        <Columns>                        
                            <asp:TemplateField Visible="false" >
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
                            <asp:TemplateField > 
                                    <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnDocNameBidinhchihieulucRef" runat="server" Text="Tên văn bản" 
                                            meta:resourcekey="lblGridColumnDocNameBidinhchihieulucRef"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <asp:HyperLink ID="hlkpropertiesBidinhchihieulucRef" runat="server"  
                                        NavigateUrl='<%# "DocsContentRelation.aspx?DocId=" + Eval("DocReferenceId").ToString() + "&LanguageId=" + LanguageId.ToString()  %>' 
                                        class="VL_DocName_Detail" 
                                        onmouseover='<%# "ShowTooltip(" + chr + "/vietlaws/Admin/Pages/lawsdocs/docstips.aspx?DocId=" + Eval( "DocReferenceId") +"&LanguageId=" + LanguageId.ToString() + chr +"," + chr + "tt" + chr + "); return false;" %>' 
                                        onmouseout='HideTooltip();' EnableViewState="False"  > <%# Eval("DocNameRef")%></asp:HyperLink></ItemTemplate>
                                <ItemStyle  HorizontalAlign="Left" VerticalAlign="Top" />
                                <HeaderStyle />          
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                <asp:Label ID="lblshowEmptyVBBidinhchihieuluc" runat="server" meta:resourcekey="lblshowEmptyVBBidinhchihieuluc" 
                    Text="..."></asp:Label>
                    </td>
                </tr>
             </table>
             <br />
              <table width="100%" cellpadding="4px" cellspacing="0px" style=" border-collapse:collapse; border : 1px solid #BEC7D2;">
                <tr>
                  <td style="border-bottom: 1px solid #BEC7D2;">
                    <asp:Label ID="lblVBBidinhchimotphanhieuluc" runat="server" CssClass="VL_DocName" meta:resourcekey="lblVBBidinhchimotphanhieuluc" 
                    Text="VB bị đình chỉ một phần"></asp:Label>
                    </td>
                </tr>
                <tr>
                  <td >
                    <asp:GridView ID="m_grid_Bidinhchimotphanhieuluc" DataKeyNames="DocReferenceId" runat="server" 
                          ShowHeader="false" ShowHeaderWhenEmpty="true"
                        AutoGenerateColumns="False"
                        Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" 
                        PageSize="50" >
                        <HeaderStyle CssClass="trbangdulieutieude" />
                        <RowStyle CssClass="trbangdulieutieudenoidung" />
                        <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                        <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />                                        
                        <Columns>                        
                            <asp:TemplateField Visible="false" >
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
                            <asp:TemplateField > 
                                    <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnDocNameBidinhchimotphanhieulucRef" runat="server" Text="Tên văn bản" 
                                            meta:resourcekey="lblGridColumnDocNameBidinhchimotphanhieulucRef"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <asp:HyperLink ID="hlkpropertiesBidinhchimotphanhieulucRef" runat="server"  
                                        NavigateUrl='<%# "DocsContentRelation.aspx?DocId=" + Eval("DocReferenceId").ToString() + "&LanguageId=" + LanguageId.ToString()  %>' 
                                        class="VL_DocName_Detail" 
                                        onmouseover='<%# "ShowTooltip(" + chr + "/vietlaws/Admin/Pages/lawsdocs/docstips.aspx?DocId=" + Eval( "DocReferenceId") +"&LanguageId=" + LanguageId.ToString() + chr +"," + chr + "tt" + chr + "); return false;" %>' 
                                        onmouseout='HideTooltip();' EnableViewState="False"  > <%# Eval("DocNameRef")%></asp:HyperLink></ItemTemplate>
                                <ItemStyle  HorizontalAlign="Left" VerticalAlign="Top" />
                                <HeaderStyle />          
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                <asp:Label ID="lblshowEmptyVBBidinhchimotphanhieuluc" runat="server" meta:resourcekey="lblshowEmptyVBBidinhchimotphanhieuluc" 
                    Text="..."></asp:Label>
                    </td>
                </tr>
             </table>
              <br />
              <table width="100%" cellpadding="4px" cellspacing="0px" style=" border-collapse:collapse; border : 1px solid #BEC7D2;">
                <tr>
                  <td style="border-bottom: 1px solid #BEC7D2;">
                    <asp:Label ID="lblVBDuocdinhchinh" runat="server" CssClass="VL_DocName" meta:resourcekey="lblVBDuocdinhchinh" 
                    Text="VB được đính chính"></asp:Label>
                    </td>
                </tr>
                <tr>
                  <td >
                    <asp:GridView ID="m_grid_duocdinhchinh" DataKeyNames="DocReferenceId" runat="server" 
                          ShowHeader="false" ShowHeaderWhenEmpty="true"
                        AutoGenerateColumns="False"
                        Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" 
                        PageSize="50" >
                        <HeaderStyle CssClass="trbangdulieutieude" />
                        <RowStyle CssClass="trbangdulieutieudenoidung" />
                        <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                        <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />                                        
                        <Columns>                        
                            <asp:TemplateField Visible="false" >
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
                            <asp:TemplateField > 
                                    <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnDocNameDuocdinhchinhRef" runat="server" Text="Tên văn bản" 
                                            meta:resourcekey="lblGridColumnDocNameDuocdinhchinhRef"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <asp:HyperLink ID="hlkpropertiesDuocdinhchinhRef" runat="server"  
                                        NavigateUrl='<%# "DocsContentRelation.aspx?DocId=" + Eval("DocReferenceId").ToString() + "&LanguageId=" + LanguageId.ToString()  %>' 
                                        class="VL_DocName_Detail" 
                                        onmouseover='<%# "ShowTooltip(" + chr + "/vietlaws/Admin/Pages/lawsdocs/docstips.aspx?DocId=" + Eval( "DocReferenceId") +"&LanguageId=" + LanguageId.ToString() + chr +"," + chr + "tt" + chr + "); return false;" %>' 
                                        onmouseout='HideTooltip();' EnableViewState="False"  > <%# Eval("DocNameRef")%></asp:HyperLink></ItemTemplate>
                                <ItemStyle  HorizontalAlign="Left" VerticalAlign="Top" />
                                <HeaderStyle />          
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                <asp:Label ID="lblshowEmptyVBduocdinhchinh" runat="server" meta:resourcekey="lblshowEmptyVBduocdinhchinh" 
                    Text="..."></asp:Label>
                    </td>
                </tr>
             </table>

            </td>
            <td width="40%" valign="top">
             <table width="100%" cellpadding="2px" cellspacing="0px" style=" border-collapse:collapse; border : 1px solid #FF5C00;">
                <tr>
                  <td style="border-bottom: 1px solid #FF5C00; text-align:center;  vertical-align:middle; height:30px;"  >
                    <asp:Label ID="lblVBDangxem" runat="server" meta:resourcekey="lblVBDangxem" 
                    Text="VB đang xem" Font-Bold="true" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                  <td>  
                   <asp:Label ID="lblDocName" Visible="true" runat="server"  EnableViewState="false"></asp:Label><br /><br />                    
                    <asp:Label ID="lbTip" Visible="true" runat="server"  EnableViewState="false"></asp:Label>                    
                  </td>
                </tr>
             </table>
             <br />
              <table width="100%" cellpadding="4px" cellspacing="0px" style=" border-collapse:collapse; border : 1px solid #BEC7D2;">
                <tr>
                  <td style="border: 1px solid #BEC7D2;">
                    <asp:Label ID="lblDocRelateEn" runat="server" CssClass="VL_DocName" meta:resourcekey="lblDocRelateEn" 
                    Text="VB liên quan ngôn ngữ"></asp:Label>
                    </td>
                </tr>
                <tr>
                  <td>
                  <asp:GridView ID="m_grid_ngonngu" DataKeyNames="DocId" runat="server" ShowHeader="false" ShowHeaderWhenEmpty="true"
                        AutoGenerateColumns="False"
                        Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" 
                        PageSize="50" >
                        <HeaderStyle CssClass="trbangdulieutieude" />
                        <RowStyle CssClass="trbangdulieutieudenoidung" />
                        <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                        <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />                                        
                        <Columns>                        
                            <asp:TemplateField Visible="false" >
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
                            <asp:TemplateField > 
                                    <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnDocNameNgonnguRef" runat="server" Text="Tên văn bản" meta:resourcekey="lblGridColumnDocNameNgonnguRef"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <asp:HyperLink ID="hlkpropertiesNgonnguRef" runat="server"  NavigateUrl='<%# "DocsContentRelation.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" +  Eval("LanguageId").ToString()  %>' class="VL_DocName_Detail" onmouseover='<%# "ShowTooltip(" + chr + "/vietlaws/Admin/Pages/lawsdocs/docstips.aspx?DocId=" + Eval( "DocId") +"&LanguageId=" +  Eval("LanguageId").ToString() + chr +"," + chr + "tt" + chr + "); return false;" %>' onmouseout='HideTooltip();' EnableViewState="False"  > <%# Eval("DocName")%></asp:HyperLink></ItemTemplate>
                                <ItemStyle  HorizontalAlign="Left" VerticalAlign="Top" />
                                <HeaderStyle />          
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                <asp:Label ID="lblshowEmptyVBNgonngu" runat="server" Visible="false" meta:resourcekey="lblshowEmptyVBNgonngu" 
                    Text="..."></asp:Label>
                    </td>
                </tr>
             </table>
             <br />
             <table width="100%" cellpadding="4px" cellspacing="0px" style=" border-collapse:collapse; border : 1px solid #BEC7D2;">
                <tr>
                  <td style="border-bottom: 1px solid #BEC7D2;">
                    <asp:Label ID="lblVBDuocCanCu" runat="server" CssClass="VL_DocName"  meta:resourcekey="lblVBDuocCanCu" 
                    Text="VB căn cứ"></asp:Label>
                    </td>
                </tr>
                <tr>
                  <td >
                    <asp:GridView ID="m_grid_Duoccancu" DataKeyNames="DocReferenceId" runat="server" ShowHeader="false" ShowHeaderWhenEmpty="true"
                        AutoGenerateColumns="False"
                        Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" 
                        PageSize="50" >
                        <HeaderStyle CssClass="trbangdulieutieude" />
                        <RowStyle CssClass="trbangdulieutieudenoidung" />
                        <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                        <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />                                        
                        <Columns>                        
                            <asp:TemplateField Visible="false" >
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
                            <asp:TemplateField > 
                                    <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnDocNameDuoccancuRef" runat="server" Text="Tên văn bản" meta:resourcekey="lblGridColumnDocNameDuoccancuRef"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <asp:HyperLink ID="hlkpropertiesDuoccancuRef" runat="server"  NavigateUrl='<%# "DocsContentRelation.aspx?DocId=" + Eval("DocReferenceId").ToString() + "&LanguageId=" + LanguageId.ToString()  %>' class="VL_DocName_Detail" onmouseover='<%# "ShowTooltip(" + chr + "/vietlaws/Admin/Pages/lawsdocs/docstips.aspx?DocId=" + Eval( "DocReferenceId") +"&LanguageId=" + LanguageId.ToString() + chr +"," + chr + "tt" + chr + "); return false;" %>' onmouseout='HideTooltip();' EnableViewState="False"  > <%# Eval("DocNameRef")%></asp:HyperLink></ItemTemplate>
                                <ItemStyle  HorizontalAlign="Left" VerticalAlign="Top" />
                                <HeaderStyle />          
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:Label ID="lblshowEmptyVBDuoccancu" runat="server" Visible="false" meta:resourcekey="lblshowEmptyVBCancu" 
                    Text="..."></asp:Label>
                    </td>
                </tr>
             </table>
             <br />
              <table width="100%" cellpadding="4px" cellspacing="0px" style=" border-collapse:collapse; border : 1px solid #BEC7D2;">
                <tr>
                  <td style="border-bottom: 1px solid #BEC7D2;">
                    <asp:Label ID="lblVBDuocthamchieu" runat="server" CssClass="VL_DocName" meta:resourcekey="lblVBDuocthamchieu" 
                    Text="VB dẫn chiếu"></asp:Label>
                    </td>
                </tr>
                <tr>
                  <td >
                    <asp:GridView ID="m_grid_Duocthamchieu" DataKeyNames="DocReferenceId" runat="server" 
                          ShowHeader="false" ShowHeaderWhenEmpty="true"
                        AutoGenerateColumns="False"
                        Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" 
                        PageSize="50" >
                        <HeaderStyle CssClass="trbangdulieutieude" />
                        <RowStyle CssClass="trbangdulieutieudenoidung" />
                        <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                        <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />                                        
                        <Columns>                        
                            <asp:TemplateField Visible="false" >
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
                            <asp:TemplateField > 
                                    <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnDocNameDuocthamchieuRef" runat="server" Text="Tên văn bản" 
                                            meta:resourcekey="lblGridColumnDocNameDuocthamchieuRef"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <asp:HyperLink ID="hlkpropertiesDuocthamchieuRef" runat="server"  
                                        NavigateUrl='<%# "DocsContentRelation.aspx?DocId=" + Eval("DocReferenceId").ToString() + "&LanguageId=" + LanguageId.ToString()  %>' 
                                        class="VL_DocName_Detail" 
                                        onmouseover='<%# "ShowTooltip(" + chr + "/vietlaws/Admin/Pages/lawsdocs/docstips.aspx?DocId=" + Eval( "DocReferenceId") +"&LanguageId=" + LanguageId.ToString() + chr +"," + chr + "tt" + chr + "); return false;" %>' 
                                        onmouseout='HideTooltip();' EnableViewState="False"  > <%# Eval("DocNameRef")%></asp:HyperLink></ItemTemplate>
                                <ItemStyle  HorizontalAlign="Left" VerticalAlign="Top" />
                                <HeaderStyle />          
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                <asp:Label ID="lblshowEmptyVBDuocthamchieu" runat="server" meta:resourcekey="lblshowEmptyVBDuocthamchieu" 
                    Text="..."></asp:Label>
                    </td>
                </tr>
             </table>
              <br />
              <table width="100%" cellpadding="4px" cellspacing="0px" style=" border-collapse:collapse; border : 1px solid #BEC7D2;">
                <tr>
                  <td style="border-bottom: 1px solid #BEC7D2;">
                    <asp:Label ID="lblVBDuoclienquan" runat="server" CssClass="VL_DocName" meta:resourcekey="lblVBDuoclienquan" 
                    Text="VB liên quan khác"></asp:Label>
                    </td>
                </tr>
                <tr>
                  <td >
                    <asp:GridView ID="m_grid_Duoclienquan" DataKeyNames="DocReferenceId" runat="server" 
                          ShowHeader="false" ShowHeaderWhenEmpty="true"
                        AutoGenerateColumns="False"
                        Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" 
                        PageSize="50" >
                        <HeaderStyle CssClass="trbangdulieutieude" />
                        <RowStyle CssClass="trbangdulieutieudenoidung" />
                        <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                        <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />                                        
                        <Columns>                        
                            <asp:TemplateField Visible="false" >
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
                            <asp:TemplateField > 
                                    <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnDocNameDuoclienquanRef" runat="server" Text="Tên văn bản" 
                                            meta:resourcekey="lblGridColumnDocNameDuoclienquanRef"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <asp:HyperLink ID="hlkpropertiesDuoclienquanRef" runat="server"  
                                        NavigateUrl='<%# "DocsContentRelation.aspx?DocId=" + Eval("DocReferenceId").ToString() + "&LanguageId=" + LanguageId.ToString()  %>' 
                                        class="VL_DocName_Detail" 
                                        onmouseover='<%# "ShowTooltip(" + chr + "/vietlaws/Admin/Pages/lawsdocs/docstips.aspx?DocId=" + Eval( "DocReferenceId") +"&LanguageId=" + LanguageId.ToString() + chr +"," + chr + "tt" + chr + "); return false;" %>' 
                                        onmouseout='HideTooltip();' EnableViewState="False"  > <%# Eval("DocNameRef")%></asp:HyperLink></ItemTemplate>
                                <ItemStyle  HorizontalAlign="Left" VerticalAlign="Top" />
                                <HeaderStyle />          
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                <asp:Label ID="lblshowEmptyVBDuoclienquan" runat="server" meta:resourcekey="lblshowEmptyVBDuoclienquan" 
                    Text="..."></asp:Label>
                    </td>
                </tr>
             </table>
            </td>
            <td width="30%" valign="top">
            <table width="100%" cellpadding="4px" cellspacing="0px" style=" display:none; border-collapse:collapse; border : 1px solid #BEC7D2;">
                <tr>
                  <td style="border-bottom: 1px solid #BEC7D2;">
                    <asp:Label ID="lblVBCancu" runat="server" CssClass="VL_DocName" meta:resourcekey="lblVBCancu" 
                    Text="Văn bản căn cứ"></asp:Label>
                    </td>
                </tr>
                <tr>
                  <td >

                    <asp:GridView ID="m_grid_Cancu" DataKeyNames="DocId" runat="server" 
                          ShowHeader="false" ShowHeaderWhenEmpty="true"
                        AutoGenerateColumns="False"
                        Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" 
                        PageSize="50" >
                        <HeaderStyle CssClass="trbangdulieutieude" />
                        <RowStyle CssClass="trbangdulieutieudenoidung" />
                        <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                        <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />                                        
                        <Columns>                        
                            <asp:TemplateField Visible="false" >
                                <HeaderTemplate>                                
                                #
                                </HeaderTemplate>
                                <ItemTemplate>
                                 <asp:Label ID="lbNo0" runat="server" Text='<%# Container.DataItemIndex + 1%>'>                                    
                                  </asp:Label>
                                </ItemTemplate>      
                                <ItemStyle HorizontalAlign="Center" Width="3%" />  
                                <HeaderStyle Width="3%" />          
                            </asp:TemplateField>        
                            <asp:TemplateField > 
                                    <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnDocNameCancu" runat="server" Text="Tên văn bản" 
                                            meta:resourcekey="lblGridColumnDocNameCancu"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <asp:HyperLink ID="hlkpropertiesCancu" runat="server"  
                                        NavigateUrl='<%# "DocsContentRelation.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + LanguageId.ToString()  %>' 
                                        class="VL_DocName_Detail" 
                                        onmouseover='<%# "ShowTooltip(" + chr + "/vietlaws/Admin/Pages/lawsdocs/docstips.aspx?DocId=" + Eval( "DocId") +"&LanguageId=" + LanguageId.ToString() + chr +"," + chr + "tt" + chr + "); return false;" %>' 
                                        onmouseout='HideTooltip();' EnableViewState="False"  > <%# Eval("DocName")%></asp:HyperLink></ItemTemplate>
                                <ItemStyle  HorizontalAlign="Left" VerticalAlign="Top" />
                                <HeaderStyle />          
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                <asp:Label ID="lblshowEmptyVBCancu" runat="server" Visible="false" meta:resourcekey="lblshowEmptyVBCancu" 
                    Text="..."></asp:Label>
                    </td>
                </tr>
             </table>
             <table width="100%" cellpadding="4px" cellspacing="0px" style=" border-collapse:collapse; border : 1px solid #BEC7D2;">
                <tr>
                  <td style="border-bottom: 1px solid #BEC7D2;">
                    <asp:Label ID="lblVBHuongdan" runat="server" CssClass="VL_DocName" meta:resourcekey="lblVBHuongdan" 
                    Text="VB hướng dẫn"></asp:Label>
                    </td>
                </tr>
                <tr>
                  <td >
                    <asp:GridView ID="m_grid_Huongdan" DataKeyNames="DocId" runat="server" 
                          ShowHeader="false" ShowHeaderWhenEmpty="true"
                        AutoGenerateColumns="False"
                        Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" 
                        PageSize="50" >
                        <HeaderStyle CssClass="trbangdulieutieude" />
                        <RowStyle CssClass="trbangdulieutieudenoidung" />
                        <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                        <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />                                        
                        <Columns>                        
                            <asp:TemplateField Visible="false" >
                                <HeaderTemplate>                                
                                #
                                </HeaderTemplate>
                                <ItemTemplate>
                                 <asp:Label ID="lbNo2" runat="server" Text='<%# Container.DataItemIndex + 1%>'>                                    
                                  </asp:Label>
                                </ItemTemplate>      
                                <ItemStyle HorizontalAlign="Center" Width="3%" />  
                                <HeaderStyle Width="3%" />          
                            </asp:TemplateField>        
                            <asp:TemplateField > 
                                    <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnDocNameHuongdanRef" runat="server" Text="Tên văn bản" 
                                            meta:resourcekey="lblGridColumnDocNameHuongdanRef"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <asp:HyperLink ID="hlkpropertiesHuongdanRef" runat="server"  
                                        NavigateUrl='<%# "DocsContentRelation.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + LanguageId.ToString()  %>' 
                                        class="VL_DocName_Detail" 
                                        onmouseover='<%# "ShowTooltip(" + chr + "/vietlaws/Admin/Pages/lawsdocs/docstips.aspx?DocId=" + Eval( "DocId") +"&LanguageId=" + LanguageId.ToString() + chr +"," + chr + "tt" + chr + "); return false;" %>' 
                                        onmouseout='HideTooltip();' EnableViewState="False"  > <%# Eval("DocName")%></asp:HyperLink></ItemTemplate>
                                <ItemStyle  HorizontalAlign="Left" VerticalAlign="Top" />
                                <HeaderStyle />          
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                <asp:Label ID="lblshowEmptyVBhuongdan" runat="server" meta:resourcekey="lblshowEmptyVBhuongdan" 
                    Text="..."></asp:Label>
                    </td>
                </tr>
             </table>
             <br />
             <table width="100%" cellpadding="4px" cellspacing="0px" style=" border-collapse:collapse; border : 1px solid #BEC7D2;">
                <tr>
                  <td style="border-bottom: 1px solid #BEC7D2;">
                    <asp:Label ID="lblVBSuadoi" runat="server" CssClass="VL_DocName" meta:resourcekey="lblVBSuadoi" 
                    Text="VB sửa đổi, bổ sung"></asp:Label>
                    </td>
                </tr>
                <tr>
                  <td >
                    <asp:GridView ID="m_grid_Suadoi" DataKeyNames="DocId" runat="server" 
                          ShowHeader="false" ShowHeaderWhenEmpty="true"
                        AutoGenerateColumns="False"
                        Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" 
                        PageSize="50" >
                        <HeaderStyle CssClass="trbangdulieutieude" />
                        <RowStyle CssClass="trbangdulieutieudenoidung" />
                        <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                        <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />                                        
                        <Columns>                        
                            <asp:TemplateField Visible="false" >
                                <HeaderTemplate>                                
                                #
                                </HeaderTemplate>
                                <ItemTemplate>
                                 <asp:Label ID="lbNo2" runat="server" Text='<%# Container.DataItemIndex + 1%>'>                                    
                                  </asp:Label>
                                </ItemTemplate>      
                                <ItemStyle HorizontalAlign="Center" Width="3%" />  
                                <HeaderStyle Width="3%" />          
                            </asp:TemplateField>        
                            <asp:TemplateField > 
                                    <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnDocNameSuadoiRef" runat="server" Text="Tên văn bản" 
                                            meta:resourcekey="lblGridColumnDocNameSuadoiRef"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <asp:HyperLink ID="hlkpropertiesSuadoiRef" runat="server"  
                                        NavigateUrl='<%# "DocsContentRelation.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + LanguageId.ToString()  %>' 
                                        class="VL_DocName_Detail" 
                                        onmouseover='<%# "ShowTooltip(" + chr + "/vietlaws/Admin/Pages/lawsdocs/docstips.aspx?DocId=" + Eval( "DocId") +"&LanguageId=" + LanguageId.ToString() + chr +"," + chr + "tt" + chr + "); return false;" %>' 
                                        onmouseout='HideTooltip();' EnableViewState="False"  > <%# Eval("DocName")%></asp:HyperLink></ItemTemplate>
                                <ItemStyle  HorizontalAlign="Left" VerticalAlign="Top" />
                                <HeaderStyle />          
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                <asp:Label ID="lblshowEmptyVBSuadoi" runat="server" meta:resourcekey="lblshowEmptyVBSuadoi" 
                    Text="..."></asp:Label>
                    </td>
                </tr>
             </table>
             <br />
             <table width="100%" cellpadding="4px" cellspacing="0px" style=" border-collapse:collapse; border : 1px solid #BEC7D2;">
                <tr>
                  <td style="border-bottom: 1px solid #BEC7D2;">
                    <asp:Label ID="lblVBThaythe" runat="server" CssClass="VL_DocName" meta:resourcekey="lblVBThaythe" 
                    Text="VB thay thế"></asp:Label>
                    </td>
                </tr>
                <tr>
                  <td >
                    <asp:GridView ID="m_grid_thaythe" DataKeyNames="DocId" runat="server" 
                          ShowHeader="false" ShowHeaderWhenEmpty="true"
                        AutoGenerateColumns="False"
                        Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" 
                        PageSize="50" >
                        <HeaderStyle CssClass="trbangdulieutieude" />
                        <RowStyle CssClass="trbangdulieutieudenoidung" />
                        <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                        <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />                                        
                        <Columns>                        
                            <asp:TemplateField Visible="false" >
                                <HeaderTemplate>                                
                                #
                                </HeaderTemplate>
                                <ItemTemplate>
                                 <asp:Label ID="lbNo2" runat="server" Text='<%# Container.DataItemIndex + 1%>'>                                    
                                  </asp:Label>
                                </ItemTemplate>      
                                <ItemStyle HorizontalAlign="Center" Width="3%" />  
                                <HeaderStyle Width="3%" />          
                            </asp:TemplateField>        
                            <asp:TemplateField > 
                                    <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnDocNameThaytheRef" runat="server" Text="Tên văn bản" 
                                            meta:resourcekey="lblGridColumnDocNameThaytheRef"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <asp:HyperLink ID="hlkpropertiesThaytheRef" runat="server"  
                                        NavigateUrl='<%# "DocsContentRelation.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + LanguageId.ToString()  %>' 
                                        class="VL_DocName_Detail" 
                                        onmouseover='<%# "ShowTooltip(" + chr + "/vietlaws/Admin/Pages/lawsdocs/docstips.aspx?DocId=" + Eval( "DocId") +"&LanguageId=" + LanguageId.ToString() + chr +"," + chr + "tt" + chr + "); return false;" %>' 
                                        onmouseout='HideTooltip();' EnableViewState="False"  > <%# Eval("DocName")%></asp:HyperLink></ItemTemplate>
                                <ItemStyle  HorizontalAlign="Left" VerticalAlign="Top" />
                                <HeaderStyle />          
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                <asp:Label ID="lblshowEmptyVBThaythe" runat="server" meta:resourcekey="lblshowEmptyVBThaythe" 
                    Text="..."></asp:Label>
                    </td>
                </tr>
             </table>
              
             <table width="100%" cellpadding="4px" cellspacing="0px" style=" display:none; border-collapse:collapse; border : 1px solid #BEC7D2;">
                <tr>
                  <td style="border-bottom: 1px solid #BEC7D2;">
                    <asp:Label ID="lblVBThamchieu" runat="server" CssClass="VL_DocName" meta:resourcekey="lblVBThamchieu" 
                    Text="VB dẫn chiếu"></asp:Label>
                    </td>
                </tr>
                <tr>
                  <td >
                    <asp:GridView ID="m_grid_Thamchieu" DataKeyNames="DocId" runat="server" 
                          ShowHeader="false" ShowHeaderWhenEmpty="true"
                        AutoGenerateColumns="False"
                        Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" 
                        PageSize="50" >
                        <HeaderStyle CssClass="trbangdulieutieude" />
                        <RowStyle CssClass="trbangdulieutieudenoidung" />
                        <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                        <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />                                        
                        <Columns>                        
                            <asp:TemplateField Visible="false" >
                                <HeaderTemplate>                                
                                #
                                </HeaderTemplate>
                                <ItemTemplate>
                                 <asp:Label ID="lbNo2" runat="server" Text='<%# Container.DataItemIndex + 1%>'>                                    
                                  </asp:Label>
                                </ItemTemplate>      
                                <ItemStyle HorizontalAlign="Center" Width="3%" />  
                                <HeaderStyle Width="3%" />          
                            </asp:TemplateField>        
                            <asp:TemplateField > 
                                    <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnDocNameThamchieuRef" runat="server" Text="Tên văn bản" 
                                            meta:resourcekey="lblGridColumnDocNameThamchieuRef"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <asp:HyperLink ID="hlkpropertiesThamchieuRef" runat="server"  
                                        NavigateUrl='<%# "DocsContentRelation.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + LanguageId.ToString()  %>' 
                                        class="VL_DocName_Detail" 
                                        onmouseover='<%# "ShowTooltip(" + chr + "/vietlaws/Admin/Pages/lawsdocs/docstips.aspx?DocId=" + Eval( "DocId") +"&LanguageId=" + LanguageId.ToString() + chr +"," + chr + "tt" + chr + "); return false;" %>' 
                                        onmouseout='HideTooltip();' EnableViewState="False"  > <%# Eval("DocName")%></asp:HyperLink></ItemTemplate>
                                <ItemStyle  HorizontalAlign="Left" VerticalAlign="Top" />
                                <HeaderStyle />          
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                <asp:Label ID="lblshowEmptyVBThamchieu" runat="server" meta:resourcekey="lblshowEmptyVBThamchieu" 
                    Text="..."></asp:Label>
                    </td>
                </tr>
             </table>
              <table width="100%" cellpadding="4px" cellspacing="0px" style=" display:none; border-collapse:collapse; border : 1px solid #BEC7D2;">
                <tr>
                  <td style="border-bottom: 1px solid #BEC7D2;">
                    <asp:Label ID="lblVBLienquan" runat="server" CssClass="VL_DocName" meta:resourcekey="lblVBLienquan" 
                    Text="VB liên quan"></asp:Label>
                    </td>
                </tr>
                <tr>
                  <td >
                    <asp:GridView ID="m_grid_lienquan" DataKeyNames="DocId" runat="server" 
                          ShowHeader="false" ShowHeaderWhenEmpty="true"
                        AutoGenerateColumns="False"
                        Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" 
                        PageSize="50" >
                        <HeaderStyle CssClass="trbangdulieutieude" />
                        <RowStyle CssClass="trbangdulieutieudenoidung" />
                        <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                        <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />                                        
                        <Columns>                        
                            <asp:TemplateField Visible="false" >
                                <HeaderTemplate>                                
                                #
                                </HeaderTemplate>
                                <ItemTemplate>
                                 <asp:Label ID="lbNo2" runat="server" Text='<%# Container.DataItemIndex + 1%>'>                                    
                                  </asp:Label>
                                </ItemTemplate>      
                                <ItemStyle HorizontalAlign="Center" Width="3%" />  
                                <HeaderStyle Width="3%" />          
                            </asp:TemplateField>        
                            <asp:TemplateField > 
                                    <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnDocNameLienquanRef" runat="server" Text="Tên văn bản" 
                                            meta:resourcekey="lblGridColumnDocNameLienquanRef"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <asp:HyperLink ID="hlkpropertiesLienquanRef" runat="server"  
                                        NavigateUrl='<%# "DocsContentRelation.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + LanguageId.ToString()  %>' 
                                        class="VL_DocName_Detail" 
                                        onmouseover='<%# "ShowTooltip(" + chr + "/vietlaws/Admin/Pages/lawsdocs/docstips.aspx?DocId=" + Eval( "DocId") +"&LanguageId=" + LanguageId.ToString() + chr +"," + chr + "tt" + chr + "); return false;" %>' 
                                        onmouseout='HideTooltip();' EnableViewState="False"  > <%# Eval("DocName")%></asp:HyperLink></ItemTemplate>
                                <ItemStyle  HorizontalAlign="Left" VerticalAlign="Top" />
                                <HeaderStyle />          
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                <asp:Label ID="lblshowEmptyVBLienquan" runat="server" meta:resourcekey="lblshowEmptyVBLienquan" 
                    Text="..."></asp:Label>
                    </td>
                </tr>
             </table>
             <br />
             <table width="100%" cellpadding="4px" cellspacing="0px" style=" border-collapse:collapse; border : 1px solid #BEC7D2;">
                <tr>
                  <td style="border-bottom: 1px solid #BEC7D2;">
                    <asp:Label ID="lblVBThaythemotphan" runat="server" CssClass="VL_DocName" meta:resourcekey="lblVBThaythemotphan" 
                    Text="VB quy định hết hiệu lực một phần"></asp:Label>
                    </td>
                </tr>
                <tr>
                  <td >
                    <asp:GridView ID="m_grid_thaythemotphan" DataKeyNames="DocId" runat="server" 
                          ShowHeader="false" ShowHeaderWhenEmpty="true"
                        AutoGenerateColumns="False"
                        Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" 
                        PageSize="50" >
                        <HeaderStyle CssClass="trbangdulieutieude" />
                        <RowStyle CssClass="trbangdulieutieudenoidung" />
                        <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                        <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />                                        
                        <Columns>                        
                            <asp:TemplateField Visible="false" >
                                <HeaderTemplate>                                
                                #
                                </HeaderTemplate>
                                <ItemTemplate>
                                 <asp:Label ID="lbNo2" runat="server" Text='<%# Container.DataItemIndex + 1%>'>                                    
                                  </asp:Label>
                                </ItemTemplate>      
                                <ItemStyle HorizontalAlign="Center" Width="3%" />  
                                <HeaderStyle Width="3%" />          
                            </asp:TemplateField>        
                            <asp:TemplateField > 
                                    <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnDocNameThaythemotphanRef" runat="server" Text="Tên văn bản" 
                                            meta:resourcekey="lblGridColumnDocNameThaythemotphanRef"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <asp:HyperLink ID="hlkpropertiesThaythemotphanRef" runat="server"  
                                        NavigateUrl='<%# "DocsContentRelation.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + LanguageId.ToString()  %>' 
                                        class="VL_DocName_Detail" 
                                        onmouseover='<%# "ShowTooltip(" + chr + "/vietlaws/Admin/Pages/lawsdocs/docstips.aspx?DocId=" + Eval( "DocId") +"&LanguageId=" + LanguageId.ToString() + chr +"," + chr + "tt" + chr + "); return false;" %>' 
                                        onmouseout='HideTooltip();' EnableViewState="False"  > <%# Eval("DocName")%></asp:HyperLink></ItemTemplate>
                                <ItemStyle  HorizontalAlign="Left" VerticalAlign="Top" />
                                <HeaderStyle />          
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                <asp:Label ID="lblshowEmptyVBThaythemotphan" runat="server" meta:resourcekey="lblshowEmptyVBThaythemotphan" 
                    Text="..."></asp:Label>
                    </td>
                </tr>
             </table>
              <br />
             <table width="100%" cellpadding="4px" cellspacing="0px" style=" border-collapse:collapse; border : 1px solid #BEC7D2;">
                <tr>
                  <td style="border-bottom: 1px solid #BEC7D2;">
                    <asp:Label ID="lblVBHopnhatgoc" runat="server" CssClass="VL_DocName" meta:resourcekey="lblVBHopnhatgoc" 
                    Text="VB hợp nhất"></asp:Label>
                    </td>
                </tr>
                <tr>
                  <td >
                    <asp:GridView ID="m_grid_hopnhatgoc" DataKeyNames="DocId" runat="server" 
                          ShowHeader="false" ShowHeaderWhenEmpty="true"
                        AutoGenerateColumns="False"
                        Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" 
                        PageSize="50" >
                        <HeaderStyle CssClass="trbangdulieutieude" />
                        <RowStyle CssClass="trbangdulieutieudenoidung" />
                        <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                        <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />                                        
                        <Columns>                        
                            <asp:TemplateField Visible="false" >
                                <HeaderTemplate>                                
                                #
                                </HeaderTemplate>
                                <ItemTemplate>
                                 <asp:Label ID="lbNo2" runat="server" Text='<%# Container.DataItemIndex + 1%>'>                                    
                                  </asp:Label>
                                </ItemTemplate>      
                                <ItemStyle HorizontalAlign="Center" Width="3%" />  
                                <HeaderStyle Width="3%" />          
                            </asp:TemplateField>        
                            <asp:TemplateField > 
                                    <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnDocNameHopnhatgocRef" runat="server" Text="Tên văn bản" 
                                            meta:resourcekey="lblGridColumnDocNameHopnhatgocRef"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <asp:HyperLink ID="hlkpropertiesHopnhatgocRef" runat="server"  
                                        NavigateUrl='<%# "DocsContentRelation.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + LanguageId.ToString()  %>' 
                                        class="VL_DocName_Detail" 
                                        onmouseover='<%# "ShowTooltip(" + chr + "/vietlaws/Admin/Pages/lawsdocs/docstips.aspx?DocId=" + Eval( "DocId") +"&LanguageId=" + LanguageId.ToString() + chr +"," + chr + "tt" + chr + "); return false;" %>' 
                                        onmouseout='HideTooltip();' EnableViewState="False"  > <%# Eval("DocName")%></asp:HyperLink></ItemTemplate>
                                <ItemStyle  HorizontalAlign="Left" VerticalAlign="Top" />
                                <HeaderStyle />          
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                <asp:Label ID="lblshowEmptyVBHopnhatgoc" runat="server" meta:resourcekey="lblshowEmptyVBHopnhatgoc" 
                    Text="..."></asp:Label>
                    </td>
                </tr>
             </table>
              <table width="100%" cellpadding="4px" cellspacing="0px" style="border-collapse:collapse; border : 1px solid #BEC7D2;">
                <tr>
                  <td style="border-bottom: 1px solid #BEC7D2;">
                    <asp:Label ID="lblVBHopnhatsuadoi" runat="server" CssClass="VL_DocName" meta:resourcekey="lblVBHopnhatsuadoi" 
                    Text="VB hợp nhất sửa đổi"></asp:Label>
                    </td>
                </tr>
                <tr>
                  <td >
                    <asp:GridView ID="m_grid_hopnhatsuadoi" DataKeyNames="DocId" runat="server" 
                          ShowHeader="false" ShowHeaderWhenEmpty="true"
                        AutoGenerateColumns="False"
                        Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" 
                        PageSize="50" >
                        <HeaderStyle CssClass="trbangdulieutieude" />
                        <RowStyle CssClass="trbangdulieutieudenoidung" />
                        <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                        <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />                                        
                        <Columns>                        
                            <asp:TemplateField Visible="false" >
                                <HeaderTemplate>                                
                                #
                                </HeaderTemplate>
                                <ItemTemplate>
                                 <asp:Label ID="lbNo2" runat="server" Text='<%# Container.DataItemIndex + 1%>'>                                    
                                  </asp:Label>
                                </ItemTemplate>      
                                <ItemStyle HorizontalAlign="Center" Width="3%" />  
                                <HeaderStyle Width="3%" />          
                            </asp:TemplateField>        
                            <asp:TemplateField > 
                                    <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnDocNameHopnhatsuadoiRef" runat="server" Text="Tên văn bản" 
                                            meta:resourcekey="lblGridColumnDocNameHopnhatsuadoiRef"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <asp:HyperLink ID="hlkpropertiesHopnhatsuadoiRef" runat="server"  
                                        NavigateUrl='<%# "DocsContentRelation.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + LanguageId.ToString()  %>' 
                                        class="VL_DocName_Detail" 
                                        onmouseover='<%# "ShowTooltip(" + chr + "/vietlaws/Admin/Pages/lawsdocs/docstips.aspx?DocId=" + Eval( "DocId") +"&LanguageId=" + LanguageId.ToString() + chr +"," + chr + "tt" + chr + "); return false;" %>' 
                                        onmouseout='HideTooltip();' EnableViewState="False"  > <%# Eval("DocName")%></asp:HyperLink></ItemTemplate>
                                <ItemStyle  HorizontalAlign="Left" VerticalAlign="Top" />
                                <HeaderStyle />          
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                <asp:Label ID="lblshowEmptyVBHopnhatsuadoi" runat="server" meta:resourcekey="lblshowEmptyVBHopnhatsuadoi" 
                    Text="..."></asp:Label>
                    </td>
                </tr>
             </table>
              <br />
             <table width="100%" cellpadding="4px" cellspacing="0px" style=" border-collapse:collapse; border : 1px solid #BEC7D2;">
                <tr>
                  <td style="border-bottom: 1px solid #BEC7D2;">
                    <asp:Label ID="lblVBDinhchihieuluc" runat="server" CssClass="VL_DocName" meta:resourcekey="lblVBDinhchihieuluc" 
                    Text="VB đình chỉ"></asp:Label>
                    </td>
                </tr>
                <tr>
                  <td >
                    <asp:GridView ID="m_grid_dinhchihieuluc" DataKeyNames="DocId" runat="server" 
                          ShowHeader="false" ShowHeaderWhenEmpty="true"
                        AutoGenerateColumns="False"
                        Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" 
                        PageSize="50" >
                        <HeaderStyle CssClass="trbangdulieutieude" />
                        <RowStyle CssClass="trbangdulieutieudenoidung" />
                        <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                        <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />                                        
                        <Columns>                        
                            <asp:TemplateField Visible="false" >
                                <HeaderTemplate>                                
                                #
                                </HeaderTemplate>
                                <ItemTemplate>
                                 <asp:Label ID="lbNo2" runat="server" Text='<%# Container.DataItemIndex + 1%>'>                                    
                                  </asp:Label>
                                </ItemTemplate>      
                                <ItemStyle HorizontalAlign="Center" Width="3%" />  
                                <HeaderStyle Width="3%" />          
                            </asp:TemplateField>        
                            <asp:TemplateField > 
                                    <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnDocNameDinhchihieulucRef" runat="server" Text="Tên văn bản" 
                                            meta:resourcekey="lblGridColumnDocNameDinhchihieulucRef"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <asp:HyperLink ID="hlkpropertiesDinhchihieulucRef" runat="server"  
                                        NavigateUrl='<%# "DocsContentRelation.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + LanguageId.ToString()  %>' 
                                        class="VL_DocName_Detail" 
                                        onmouseover='<%# "ShowTooltip(" + chr + "/vietlaws/Admin/Pages/lawsdocs/docstips.aspx?DocId=" + Eval( "DocId") +"&LanguageId=" + LanguageId.ToString() + chr +"," + chr + "tt" + chr + "); return false;" %>' 
                                        onmouseout='HideTooltip();' EnableViewState="False"  > <%# Eval("DocName")%></asp:HyperLink></ItemTemplate>
                                <ItemStyle  HorizontalAlign="Left" VerticalAlign="Top" />
                                <HeaderStyle />          
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                <asp:Label ID="lblshowEmptyVBDinhchihieuluc" runat="server" meta:resourcekey="lblshowEmptyVBDinhchihieuluc" 
                    Text="..."></asp:Label>
                    </td>
                </tr>
             </table>
             <br />
             <table width="100%" cellpadding="4px" cellspacing="0px" style=" border-collapse:collapse; border : 1px solid #BEC7D2;">
                <tr>
                  <td style="border-bottom: 1px solid #BEC7D2;">
                    <asp:Label ID="lblVBDinhchimotphanhieuluc" runat="server" CssClass="VL_DocName" meta:resourcekey="lblVBDinhchimotphanhieuluc" 
                    Text="VB đình chỉ một phần"></asp:Label>
                    </td>
                </tr>
                <tr>
                  <td >
                    <asp:GridView ID="m_grid_dinhchimotphanhieuluc" DataKeyNames="DocId" runat="server" 
                          ShowHeader="false" ShowHeaderWhenEmpty="true"
                        AutoGenerateColumns="False"
                        Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" 
                        PageSize="50" >
                        <HeaderStyle CssClass="trbangdulieutieude" />
                        <RowStyle CssClass="trbangdulieutieudenoidung" />
                        <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                        <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />                                        
                        <Columns>                        
                            <asp:TemplateField Visible="false" >
                                <HeaderTemplate>                                
                                #
                                </HeaderTemplate>
                                <ItemTemplate>
                                 <asp:Label ID="lbNo2" runat="server" Text='<%# Container.DataItemIndex + 1%>'>                                    
                                  </asp:Label>
                                </ItemTemplate>      
                                <ItemStyle HorizontalAlign="Center" Width="3%" />  
                                <HeaderStyle Width="3%" />          
                            </asp:TemplateField>        
                            <asp:TemplateField > 
                                    <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnDocNameDinhchimotphanhieulucRef" runat="server" Text="Tên văn bản" 
                                            meta:resourcekey="lblGridColumnDocNameDinhchimotphanhieulucRef"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <asp:HyperLink ID="hlkpropertiesDinhchimotphanhieulucRef" runat="server"  
                                        NavigateUrl='<%# "DocsContentRelation.aspx?DocId=" + Eval("DocId").ToString() + "&LanguageId=" + LanguageId.ToString()  %>' 
                                        class="VL_DocName_Detail" 
                                        onmouseover='<%# "ShowTooltip(" + chr + "/vietlaws/Admin/Pages/lawsdocs/docstips.aspx?DocId=" + Eval( "DocId") +"&LanguageId=" + LanguageId.ToString() + chr +"," + chr + "tt" + chr + "); return false;" %>' 
                                        onmouseout='HideTooltip();' EnableViewState="False"  > <%# Eval("DocName")%></asp:HyperLink></ItemTemplate>
                                <ItemStyle  HorizontalAlign="Left" VerticalAlign="Top" />
                                <HeaderStyle />          
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                <asp:Label ID="lblshowEmptyVBDinhchimotphanhieuluc" runat="server" meta:resourcekey="lblshowEmptyVBDinhchimotphanhieuluc" 
                    Text="..."></asp:Label>
                    </td>
                </tr>
             </table>
             <br />
              <table width="100%" cellpadding="4px" cellspacing="0px" style=" border-collapse:collapse; border : 1px solid #BEC7D2;">
                <tr>
                  <td style="border-bottom: 1px solid #BEC7D2;">
                    <asp:Label ID="lblVBDinhchinh" runat="server" CssClass="VL_DocName" meta:resourcekey="lblVBDinhchinh" 
                    Text="VB đính chính"></asp:Label>
                    </td>
                </tr>
                <tr>
                  <td >
                    <asp:GridView ID="m_grid_dinhchinh" DataKeyNames="DocReferenceId" runat="server" 
                          ShowHeader="false" ShowHeaderWhenEmpty="true"
                        AutoGenerateColumns="False"
                        Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" 
                        PageSize="50" >
                        <HeaderStyle CssClass="trbangdulieutieude" />
                        <RowStyle CssClass="trbangdulieutieudenoidung" />
                        <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                        <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />                                        
                        <Columns>                        
                            <asp:TemplateField Visible="false" >
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
                            <asp:TemplateField > 
                                    <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnDocNameDinhchinhRef" runat="server" Text="Tên văn bản" 
                                            meta:resourcekey="lblGridColumnDocNameDinhchinhRef"></asp:Label>
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <asp:HyperLink ID="hlkpropertiesDinhchinhRef" runat="server"  
                                        NavigateUrl='<%# "DocsContentRelation.aspx?DocId=" + Eval("DocReferenceId").ToString() + "&LanguageId=" + LanguageId.ToString()  %>' 
                                        class="VL_DocName_Detail" 
                                        onmouseover='<%# "ShowTooltip(" + chr + "/vietlaws/Admin/Pages/lawsdocs/docstips.aspx?DocId=" + Eval( "DocReferenceId") +"&LanguageId=" + LanguageId.ToString() + chr +"," + chr + "tt" + chr + "); return false;" %>' 
                                        onmouseout='HideTooltip();' EnableViewState="False"  > <%# Eval("DocNameRef")%></asp:HyperLink></ItemTemplate>
                                <ItemStyle  HorizontalAlign="Left" VerticalAlign="Top" />
                                <HeaderStyle />          
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                <asp:Label ID="lblshowEmptyVBDinhchinh" runat="server" meta:resourcekey="lblshowEmptyVBDinhchinh" 
                    Text="..."></asp:Label>
                    </td>
                </tr>
             </table>
            </td>
        </tr>
        </table>
   <div style="background-color: #FFFFFF; bottom: 0px;  padding: 8px;  position: fixed;   right: 1px;   text-align: right;  width: 100%;">
        <asp:LinkButton ID="btnBack" runat="server" CssClass="quaylai" 
                    Text="Quay lại" meta:resourcekey="btnBack" 
            onclick="btnBack_Click" >
        </asp:LinkButton>        
    </div>
</asp:Content>

