<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="CustomersServicesReport_HistoryPay.aspx.cs" Inherits="Admin_Pages_lawsdocs_CustomersServicesReport_HistoryPay" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Import Namespace="ICSoft.LawDocsLib" %>
<%@ Import Namespace="ICSoft.HelperLib" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_header" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <asp:UpdatePanel ID="upn_Grid" runat="server">
    <ContentTemplate>
    <div style="text-align:left;"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
    
    <div class="clear5px"></div>
	<div class="vien"></div>
	<div class="clear5px"></div>  
    <div class="khungchucnang">
    <div style="text-align:left; width:50%; float:right"><asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                            <ProgressTemplate><img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...</ProgressTemplate>
                            </asp:UpdateProgress></div>
	<div class="clear5px"></div>
    <div class="contenbangdulieu">
    
        <asp:GridView ID="m_grid" DataKeyNames="PaymentTransactionId" runat="server" ShowHeaderWhenEmpty="True"
            AutoGenerateColumns="False" OnRowDataBound = "m_grid_OnRowDataBound"
            Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" PageSize="50" >
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
                    <asp:Label ID="lbNo" runat="server" Text='<%# Container.DataItemIndex + 1%>' 
                                     ToolTip ='<%# Eval("PaymentTransactionId").ToString() %>'> </asp:Label>
                    </ItemTemplate>      
                    <ItemStyle HorizontalAlign="Center" Width="3%" />  
                    <HeaderStyle Width="3%" />          
                </asp:TemplateField> 
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnServiceName" runat="server" Text="Dịch vụ" meta:resourcekey="lblGridColumnServiceName"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate>                       
                        <%# Eval("mService.ServiceDesc")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left"/>
                    <HeaderStyle />          
                </asp:TemplateField> 
                 <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnServicePackageName" runat="server" Text="Tên gói" meta:resourcekey="lblGridColumnServicePackageName"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate>                      
                        <%# Eval("mServicePackage.ServicePackageDesc")%>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Center"/>
                    <HeaderStyle />          
                </asp:TemplateField> 
                  <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnTransactionDesc" runat="server" Text="Mô tả" meta:resourcekey="lblGridColumnTransactionDesc"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate>                       
                        <%# Eval("TransactionDesc")%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Left" Width="25%"/>
                    <HeaderStyle />          
                </asp:TemplateField> 
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnPaymentTypes" runat="server" Text="Kiểu thanh toán" meta:resourcekey="lblGridColumnPaymentTypes"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate> 
                    <span class="xuatban<%# Eval("PaymentTypeId") %>" > <%# LanguageHelpers.IsCultureVietnamese() ? ICSoft.LawDocsLib.PaymentTypes.Static_Get(byte.Parse(Eval("PaymentTypeId").ToString()), l_PaymentTypes).PaymentTypeDesc : PaymentTypes.Static_Get(byte.Parse(Eval("PaymentTypeId").ToString()), l_PaymentTypes).PaymentTypeName%></span>                               
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign = "Left" />  
                    <HeaderStyle/>        
                </asp:TemplateField>
               <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnAmount" runat="server" Text="Số tiền trả" meta:resourcekey="lblGridColumnAmount"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lbAmount" runat="server" style="display:none"></asp:Label>                        
                        <%# string.Format("{0:#,##}", Eval("Amount"))%> 
                    </ItemTemplate>
                   <EditItemTemplate>  
                       <asp:TextBox ID="txtAmount" runat="server" Text='<%#Eval("Amount")%>' Width="80px" style="text-align: right; height: 24px; line-height: 24px; border: 2px solid #4169e1;" CausesValidation="True"></asp:TextBox>  
                   </EditItemTemplate> 
                    <ItemStyle  HorizontalAlign="Right" Width="8%"/>
                    <HeaderStyle />          
                </asp:TemplateField> 
                 <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnPrice" runat="server" Text="Giá DV" meta:resourcekey="lblGridColumnPrice"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lbPrice" runat="server" style="display:none"></asp:Label>                        
                        <%# string.Format("{0:#,##}", Eval("mServicePackage.Price"))%> 
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Right" Width="8%"/>
                    <HeaderStyle />          
                </asp:TemplateField>
                <asp:TemplateField  > 
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnTransactionStatus" runat="server" Text="Trạng thái" meta:resourcekey="lblGridColumnTransactionStatus"></asp:Label>
                    </HeaderTemplate>  
                    <ItemTemplate>                         
                            <span class="xuatban<%# Eval("TransactionStatusId") %>" > <%# LanguageHelpers.IsCultureVietnamese() ? TransactionStatus.Static_Get(byte.Parse(Eval("TransactionStatusId").ToString()), l_TransactionStatus).TransactionStatusDesc : TransactionStatus.Static_Get(byte.Parse(Eval("TransactionStatusId").ToString()), l_TransactionStatus).TransactionStatusName%></span><br />
                             <%# DateTimeHelpers.GetDateAndTime(Eval("CrDateTime"))%><br />
                                             
                    </ItemTemplate>
                    <ItemStyle Width="8%" HorizontalAlign = "center" Wrap="false" />  
                    <HeaderStyle Width="8%" />        
                </asp:TemplateField>    
            </Columns>
        </asp:GridView>
    </div>
   </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

