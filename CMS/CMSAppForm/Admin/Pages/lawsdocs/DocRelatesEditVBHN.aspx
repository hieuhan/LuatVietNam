<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="DocRelatesEditVBHN.aspx.cs" Inherits="Admin_DocRelatesEditVBHN" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/admin/UserControls/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="uc1" %>
<%@ Import Namespace="ICSoft.CMSLib" %>
<%@ Import Namespace="ICSoft.LawDocsLib" %>
<%@ Import Namespace="ICSoft.HelperLib" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
<script src="../../Js/ajaxTooltip/ajax-tooltip.js" type="text/javascript"></script>
<script src="../../Js/ajaxTooltip/ajax-dynamic-content.js" type="text/javascript"></script>
<script src="../../Js/ajaxTooltip/ajax.js" type="text/javascript"></script>
<script type="text/javascript">
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);          

            $('a#popup').live('click', function (e) {
                var page = $(this).attr("href")
                var cdialog = $('<div id="divEdit"></div>')
                .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="98%" height="98%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 600,
                    width: 1020,
                    title: $(this).attr("title"),
                    close: function (event, ui) {
                        $(this).remove();
                      
                    }
                });
              cdialog.dialog('open');
              e.preventDefault();
          });
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
    <%--<asp:UpdatePanel ID="upn_Grid" runat="server">
    <ContentTemplate>--%>
    <div style="text-align:center"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
    <%--<div style="text-align:left; width:50%; float:right"><asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upn_Grid" runat="server">
                            <ProgressTemplate><img style="text-align:center; vertical-align:middle" alt="loading..." src="../../Icons/loading.gif" /> Loading...</ProgressTemplate>
                            </asp:UpdateProgress></div>--%>
    <table cellpadding="3" cellspacing="0" style="width:100%">
        <tr style="display:none">
            <td width="140px">
                <asp:Label ID="lblDocname" runat="server"  Text="Tên văn bản:"></asp:Label>
            </td>
            <td colspan="3">
                <asp:Label ID="lblDocNameEdit" Font-Bold="true" ForeColor="Maroon" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td  width="155px">
                <asp:Label ID="lblRelateTypes" runat="server" Font-Bold="true" 
                    Text="Kiểu liên kết"></asp:Label>
            </td>
            <td width="320px">
            <asp:Label ID="lblDescriptionsNhap" runat="server" Font-Bold="true" 
                    Text="Nhập"></asp:Label>
                    &nbsp;<asp:Label ID="lblDescriptionIdentity" runat="server" ForeColor="Red" Font-Bold="true"
                    Text="Số hiệu văn bản"></asp:Label>
                    &nbsp;<asp:Label ID="lblDescriptionphancach" runat="server" Font-Bold="true" 
                    Text="phân cách bằng dấu"></asp:Label>
                    &nbsp;<asp:Label ID="lblDescriptiondauphay" runat="server" ForeColor="Red" Font-Bold="true" 
                    Text="';'"></asp:Label>
            </td>
            <td width="150px">
                &nbsp;</td>
            <td>
                Đi đến &gt;&gt;<a target="_blank" href="DocsContentRelation.aspx?DocId=<%=DocId %>&LanguageId=<%=LanguageId%>">Lược đồ</a> &nbsp;</td>
        </tr>       
        <tr>
            
            <td>
                <asp:Label ID="lblAddVBHNGoc" runat="server" Font-Bold="false"  Text="» VBHN gốc:"></asp:Label>
                <span class="icon-required"></span>
            </td>
            <td>
                <asp:Panel runat="server" DefaultButton="lbAddVBHNGoc">
                <asp:TextBox ID="txtAddVBHNGoc" runat="server" CssClass="tukhoatimekiem" 
                    Width="210px"></asp:TextBox>
                &nbsp;<asp:Button ID="lbAddVBHNGoc" runat="server" CssClass="themmoi" onclick="lbAddVBHNGoc_Click" Text="Thêm" ValidationGroup="G1" > </asp:Button><br/>
                <asp:RequiredFieldValidator ID="rfvtxtAddVBHNGoc" runat="server" Display="Dynamic" 
                ErrorMessage="Bạn cần nhập VBHN gốc" ValidationGroup="G1" 
                ControlToValidate="txtAddVBHNGoc" ForeColor="Red"></asp:RequiredFieldValidator>
                    </asp:Panel>
            </td>
            <td>
                <asp:Label ID="lblAddVBHNSuaDoi" runat="server" Font-Bold="false"  Text="» VBHN sửa đổi:"></asp:Label>
                <span class="icon-required"></span>
            </td>
            <td>
                <asp:Panel runat="server" DefaultButton="lbAddVBHNSuaDoi">
                <asp:TextBox ID="txtAddVBHNSuaDoi" runat="server" CssClass="tukhoatimekiem" 
                    Width="210px"></asp:TextBox>
                &nbsp;<asp:Button ID="lbAddVBHNSuaDoi" runat="server" CssClass="themmoi" onclick="lbAddVBHNSuaDoi_Click" ValidationGroup="G2" Text="Thêm"> </asp:Button><br/>
                <asp:RequiredFieldValidator ID="rfvlbAddVBHNSuaDoi" runat="server" Display="Dynamic" 
                ErrorMessage="Bạn cần nhập VBHN sửa đổi" ValidationGroup="G2" 
                ControlToValidate="txtAddVBHNSuaDoi" ForeColor="Red"></asp:RequiredFieldValidator>
                    </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td align="left" colspan="2">
                <asp:LinkButton ID="lbAdd" runat="server" CssClass="themmoi" 
                     onclick="lbAdd_Click" Text="Cập nhật"> </asp:LinkButton>
                <asp:LinkButton ID="lbUnCheck" runat="server" CssClass="boduyet" 
                    onclick="lbUnCheck_Click" Text="Hủy chọn"> </asp:LinkButton>
                <asp:Label ID="lblDocId" runat="server" Text="" Visible="false"></asp:Label>
            </td>
        </tr>
        
        <tr>
            <td colspan="2">
                <asp:Label ID="lblTitleDocLink" runat="server" Visible="False"
                    Text="Danh sách văn bản liên quan sẽ được thêm, ấn Cập nhật để lưu thay đổi."></asp:Label>
            </td>
        </tr>

        <tr>
            <td colspan="4">
                <asp:GridView ID="m_grid_DocLinkList" runat="server" AutoGenerateColumns="False" 
                    BorderWidth="1" CellPadding="0" CellSpacing="0" DataKeyNames="DocId" OnRowDataBound = "m_grid_DocLinkList_OnRowDataBound" OnRowDeleting="m_grid_DocLinkList_RowDeleting"
                   ShowHeaderWhenEmpty="True" ShowFooter="true" ShowHeader="true" Width="100%">
                    <HeaderStyle CssClass="trbangdulieutieude" />
                    <FooterStyle CssClass="trbangdulieutieude" />
                    <RowStyle CssClass="trbangdulieutieudenoidung" />
                    <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                    <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                #
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbNo" runat="server" Text='<%# Container.DataItemIndex + 1%>'> </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="3%" />
                            <HeaderStyle Width="3%" />
                            <FooterTemplate>#</FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblGridColumnDocID" runat="server"  Text=" văn bản"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbDocID" runat="server" style="display:none"></asp:Label>
                                <a WHeight="400" WWidth="850" href='<%# "DocsFielsEdit.aspx?DocId=" + Eval("DocId").ToString()+ "&LanguageId=" +LanguageId.ToString()%>' class="popup docsproperties">
                                                 <%# Eval("DocId")%>
                                             </a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            <FooterTemplate>  <asp:Label ID="lblGridColumnDocIDFt" runat="server"  Font-Bold="true"  Text="Mã văn bản"></asp:Label></FooterTemplate>   
                            <FooterStyle HorizontalAlign="Center" />                         
                        </asp:TemplateField> 

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblGridColumnDocName" runat="server"  Text="Tên văn bản"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbDocName" runat="server" style="display:none"></asp:Label>
                                <%# Eval("DocName")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            <FooterTemplate>  <asp:Label ID="lblGridColumnDocNameFt" runat="server"  Font-Bold="true"  Text="Tên văn bản"></asp:Label></FooterTemplate>   
                            <FooterStyle HorizontalAlign="Center" />                         
                        </asp:TemplateField>                        
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblGridColumnIdentity" runat="server"   Text="số hiệu"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbDocIdentity" runat="server" style="display:none"></asp:Label>
                                <%# Eval("DocIdentity")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            <FooterTemplate>  <asp:Label ID="lblGridColumnIdentityFt" runat="server"  Font-Bold="true"  Text="số hiệu"></asp:Label></FooterTemplate>    
                             <FooterStyle HorizontalAlign="Center" />                        
                        </asp:TemplateField> 

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblGridColumnIssueDate" runat="server"   Text="Ngày BH"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <span style="color:#0C0C0C;">
                                <%# DateTimeHelpers.GetDateHH24(Eval("IssueDate"))%></span>                               
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="8%" Wrap="false" />
                            <FooterTemplate> <asp:Label ID="lblGridColumnIssueDateFt" runat="server"  Font-Bold="true"  Text="Ngày BH"></asp:Label></FooterTemplate>
                            <FooterStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblGridColumnRelateTypeId" runat="server"   Text="Kiểu liên kết"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                              <%# LanguageHelpers.IsCultureVietnamese() ? RelateTypes.Static_Get(byte.Parse(Eval("RelateTypeId").ToString()), l_RelateTypes).RelateTypeDesc : RelateTypes.Static_Get(byte.Parse(Eval("RelateTypeId").ToString()), l_RelateTypes).RelateTypeName%> 
                                
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="8%" Wrap="false" />
                            <FooterTemplate> <asp:Label ID="lblGridColumnRelateTypeIdFt" runat="server" Font-Bold="true"  Text="Kiểu liên kết"></asp:Label></FooterTemplate>
                            <FooterStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <%--<asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblGridColumnRelateTypeId" runat="server" 
                                    meta:resourcekey="lblGridColumnRelateTypeId" Text="Trạng thái"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                              <%# LanguageHelpers.IsCultureVietnamese() ? RelateTypes.Static_Get(byte.Parse(Eval("RelateTypeId").ToString()), l_RelateTypes).RelateTypeDesc : RelateTypes.Static_Get(byte.Parse(Eval("RelateTypeId").ToString()), l_RelateTypes).RelateTypeName%>                          
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="8%" Wrap="false" />
                            <FooterTemplate> <asp:Label ID="lblGridColumnRelateTypeIdFt" runat="server" Font-Bold="true"
                                    meta:resourcekey="lblGridColumnRelateTypeId" Text="Kiểu liên kết"></asp:Label></FooterTemplate>
                            <FooterStyle HorizontalAlign="Center" />
                        </asp:TemplateField>--%>

                       <asp:TemplateField Visible="true">
                        <HeaderTemplate>                                
                            <asp:Label ID="lblGridColumnOperation" runat="server" Text="Thao tác"  ></asp:Label>
                        </HeaderTemplate> 
                        <ItemTemplate> 
                            <asp:LinkButton ID="lbDelete" runat="server" CssClass="iconadmxoa" CommandName="Delete" ToolTip="Xóa"></asp:LinkButton> 
                            <asp:Label ID="lblLanguageId" runat="server" Text='<%# Eval("LanguageId") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" Width="3%" Wrap="false" />
                        <HeaderStyle Width="3%" />
                    </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="4">
            <asp:LinkButton ID="lbtnShowDDL" ForeColor="Red" Font-Bold="True" Font-Size="9pt" Visible="false"
                    Font-Names="Arial" runat="server" CausesValidation="False" 
                    Font-Underline="True" onclick="lbtnShowDDL_Click" Text="[+] Hiển thị liên kết trong CSDL" ></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <table  id="tblREL" runat="server" visible="true" width="100%">
                    <tr>
                        <td width="90px">
                            <asp:Label ID="lblDocnameREL" runat="server"  
                                Text="Tên văn bản:"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:Label ID="lblDocNameEditREL" runat="server" Font-Bold="true" 
                                ForeColor="Maroon"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="m_grid_REL" runat="server" 
                                AutoGenerateColumns="False" BorderWidth="1" CellPadding="0" CellSpacing="0" 
                                DataKeyNames="DocRelateId" OnRowDeleting="m_grid_REL_RowDeleting" OnRowDataBound = "m_grid_REL_OnRowDataBound"
                                OnRowEditing="m_grid_REL_RowEditing" OnRowCancelingEdit="m_grid_REL_RowCancelingEdit" OnRowUpdating="m_grid_REL_RowUpdating"
                                ShowFooter="false" ShowHeader="true" ShowHeaderWhenEmpty="True" 
                                Width="100%" PageSize="50">
                                <HeaderStyle CssClass="trbangdulieutieude" />
                                <FooterStyle CssClass="trbangdulieutieude" />
                                <RowStyle CssClass="trbangdulieutieudenoidung" />
                                <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                                <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            #:
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbNoRef" runat="server" Text='<%# Container.DataItemIndex + (CustomPaging.PageIndex - 1) * m_grid_REL.PageSize + 1%>' 
                                     ToolTip ='<%# Eval("DocRelateId").ToString() %>'> </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="3%" />
                                        <HeaderStyle Width="3%" />
                                        <FooterTemplate>
                                            #
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblGridColumnDocIDRef" runat="server"  Text="Mã văn bản"></asp:Label>                                            
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbDocIDRef" runat="server" style="display:none"></asp:Label>
                                             <a WHeight="400" WWidth="850"  href='<%# "DocsFielsEdit.aspx?DocId=" + (Eval("DocId").ToString() == Request.Params["DocId"].ToString() ? Eval("DocReferenceId") : Eval("DocId"))+ "&LanguageId=" +LanguageId.ToString()%>' class="popup docsproperties" >
                                            <%# Eval("DocId").ToString() == Request.Params["DocId"].ToString() ? Eval("DocReferenceId") : Eval("DocId")%>
                                             </a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />                                       
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate >
                                            <asp:Label ID="lblGridColumnDocNameRef" runat="server"  Text="Tên văn bản"></asp:Label>                                  
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbDocNameRef" runat="server" style="display:none; width:47%;"></asp:Label>
                                             <%# Eval("DocId").ToString() == Request.Params["DocId"].ToString() ? Eval("DocNameRef") : Eval("DocName")%>
                                            <asp:Label ID="lblDocName" runat="server" Text='<%# Eval("DocId").ToString() == Request.Params["DocId"].ToString() ? Eval("DocNameRef") : Eval("DocName")%>' Visible="false"></asp:Label>
                                             <br />
                                            <asp:HyperLink ID="hlkproperties" runat="server" NavigateUrl="#" class="docsproperties"
                                                 onmouseover='<%# "ShowTooltip(" + chr + "docstips.aspx?DocId=" + (Eval("DocId").ToString() == Request.Params["DocId"].ToString() ? Eval("DocReferenceId") : Eval("DocId")) +"&LanguageId=" + LanguageId.ToString() + chr +"," + chr + "tt" + chr + "); return true;" %>' 
                                                 onmouseout='HideTooltip();' EnableViewState="False"  ><asp:Label ID="lblGridColumnProperties" runat="server" CssClass="docsproperties" Text="[Thuộc tính]"  ></asp:Label></asp:HyperLink></a> <span class="docsproperties"> | </span>  
                         
                          <a WHeight="450" WWidth="800" href='<%# "DocSeoEdit.aspx?DocId=" +  (Eval("DocId").ToString() == Request.Params["DocId"].ToString() ? Eval("DocReferenceId") : Eval("DocId")) + "&LanguageId=" + Eval("LanguageId") %>' 
                              class="popup docsproperties  <%#string.IsNullOrEmpty(Eval("DocId").ToString() == Request.Params["DocId"].ToString() ? Eval("MetaTitleRef").ToString() : Eval("MetaTitle").ToString())?"active":"" %>"  title="Seo Info">
                              <asp:Label ID="Label3" runat="server" CssClass="docsproperties" Text="Seos"></asp:Label>

                          </a>

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"  Width="47%"/>                                       
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblGridColumnIdentityRef" runat="server"  Text="số hiệu"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbDocIdentityRef" runat="server" style="display:none"></asp:Label>
                                            <%# Eval("DocId").ToString() == Request.Params["DocId"].ToString() ? Eval("DocIdentityRef") : Eval("DocIdentity")%><br />
                                            <span style="color:#0073EE;"><%#  Eval("DocId").ToString() == Request.Params["DocId"].ToString() ? EffectStatus.Static_Get((byte) Eval("EffectStatusIdRef"),l_EffectStatus).EffectStatusDesc : EffectStatus.Static_Get((byte) Eval("EffectStatusId"),l_EffectStatus).EffectStatusDesc%></span>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="false" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblGridColumnIssueDateRef" runat="server"  Text="Ngày BH"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <span style="color:#0C0C0C;">
                                            <%#  Eval("DocId").ToString() == Request.Params["DocId"].ToString() ? DateTimeHelpers.GetDateHH24(Eval("IssueDateRef")) : DateTimeHelpers.GetDateHH24(Eval("IssueDate"))%></span>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="false" />                                       
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                           <asp:Label ID="lblGridColumnRelateTypeIdRef" runat="server"  Text="Kiểu liên kết"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                           <span style="font-weight:normal; color:Blue;" > <%# LanguageHelpers.IsCultureVietnamese() ? (Eval("DocId").ToString() == Request.Params["DocId"].ToString() ? RelateTypes.Static_Get(byte.Parse(Eval("RelateTypeId").ToString()), l_RelateTypes).RelateTypeDesc : RelateTypes.Static_Get(byte.Parse(Eval("RelateTypeId").ToString()), l_RelateTypes).RevertRelateTypeDesc) : (Eval("DocId").ToString() == Request.Params["DocId"].ToString() ? RelateTypes.Static_Get(byte.Parse(Eval("RelateTypeId").ToString()), l_RelateTypes).RelateTypeName : RelateTypes.Static_Get(byte.Parse(Eval("RelateTypeId").ToString()), l_RelateTypes).RevertRelateTypeName)%> </span>                                            
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlRelateTypes" runat="server" DataSource='<%# l_RelateTypes %>' Width="200px"
                                                DataTextField="RelateTypeDesc" DataValueField="RelateTypeId" SelectedValue='<%#Bind("RelateTypeId") %>' />
                                        </EditItemTemplate>                                        
                                        <ItemStyle HorizontalAlign="Left"/>  
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblGridColumnReviewStatusIdRef" runat="server"  Text="Trạng thái"></asp:Label>                                            
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbReviewStatusIdRef" runat="server" style="display:none"></asp:Label>
                                             <span class="xuatban<%# Eval("ReviewStatusId") %>" > <%# LanguageHelpers.IsCultureVietnamese() ? ReviewStatus.Static_Get(byte.Parse(Eval("ReviewStatusId").ToString()), l_ReviewStatus).ReviewStatusDesc : ReviewStatus.Static_Get(byte.Parse(Eval("ReviewStatusId").ToString()), l_ReviewStatus).ReviewStatusName%></span>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />                                       
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="true">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblGridColumnOperationRef" runat="server"  Text="Thao tác"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbEditREL" runat="server" CommandName="Edit" CausesValidation="false" Visible='<%# Eval("ReviewStatusId").ToString()=="2" ? false : true %>' ToolTip="Sửa" class="iconadmsua"></asp:LinkButton>&nbsp;
                                            <asp:LinkButton ID="lbDeleteREL" runat="server" CommandName="Delete" Visible='<%# Eval("ReviewStatusId").ToString()=="2" ? false : true %>'
                                                CssClass="iconadmxoa"  ToolTip="Xóa"></asp:LinkButton>                                           
                                        </ItemTemplate>
                                         <EditItemTemplate>
                                            <asp:LinkButton ID="cmdUpdate" runat="server" CommandName="Update" CausesValidation="false" CssClass="themmoi" >Cập nhật</asp:LinkButton>&nbsp;
                                            <asp:LinkButton ID="cmdCancel" runat="server" CommandName="Cancel" CausesValidation="false" CssClass="boduyet" >Tho&#225;t</asp:LinkButton>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="center" Width="3%" Wrap="false" />
                                        <HeaderStyle Width="3%" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <div class="clear5px"></div>    
                                        <uc1:CustomPaging ID="CustomPaging" runat="server" />                
                                        <div class="clear5px"></div>   
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
   
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
  <%--  <script type="text/javascript">
        var txtVBCancu = document.getElementById("txtVBCancu");
        var txtVBHethieuluc = document.getElementById("txtVBHethieuluc");
        var txtVBHethieulucmotphan = document.getElementById("txtVBHethieulucmotphan");
        var txtVBBisuadoi = document.getElementById("txtVBBisuadoi");
        var txtVBBidinhchihieuluc = document.getElementById("txtVBBidinhchihieuluc");
        var txtVBBidinhchimotphanhieuluc = document.getElementById("txtVBBidinhchimotphanhieuluc");
        var txtVBDuochuongdan = document.getElementById("txtVBDuochuongdan");
        var txtVBDuocthamchieu = document.getElementById("txtVBDuocthamchieu");
        var txtVBLienquan = document.getElementById("txtVBLienquan");
        var txtVBDuochopnhatgoc = document.getElementById("txtVBDuochopnhatgoc");
        var txtVBDuochopnhatsuadoibosung = document.getElementById("txtVBDuochopnhatsuadoibosung");
        var txtVBDuocdinhchinh = document.getElementById("txtVBDuocdinhchinh");
        function submitButton(event) {
            console.log(txtVBCancu.value);
            if (event.which == 13) {
                $('#lbAddVBCancu').click();
            }
        }
    </script>--%>
</asp:Content>
