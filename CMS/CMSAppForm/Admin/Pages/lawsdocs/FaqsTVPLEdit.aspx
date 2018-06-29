<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="FaqsTVPLEdit.aspx.cs" Inherits="Admin_FaqsTVPLEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
<script type="text/javascript">
    var uploadCount = 1;
    function AddUpload() {
        var uploads = document.getElementById("uploads");
        var id = "upload" + uploadCount;
        var input = document.createElement('input');
        input.type = 'file';
        input.name = id;
        uploads.appendChild(document.createElement('br'));
        uploads.appendChild(input);
        uploadCount++;
    }

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
    <div style="width:auto; height:485px; overflow:auto;"> 
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td style="width:90px">
                
                <asp:Label ID="lblLanguage" runat="server" meta:resourcekey="lblLanguage" 
                    Text="Ngôn ngữ:"></asp:Label>
                
                </td>
            <td width="250px">
                
                          <asp:DropDownList ID="ddlLanguage" runat="server"
                    CssClass="userselect" DataTextField="LanguageDesc" DataValueField="LanguageId" Width="250px">
                </asp:DropDownList>
            
                </td>
            <td width="90px">
            
            <asp:Label ID="lblFaqTypes" runat="server" meta:resourcekey="lblFaqTypes" 
                    Text="Loại:"></asp:Label>
            
            </td>
            <td>
                        
                 <asp:DropDownList ID="ddlFaqTypes" runat="server"
                    CssClass="userselect" DataTextField="FaqTypeDesc" 
                    DataValueField="FaqTypeId" Width="250px">
                </asp:DropDownList>
            
                 <asp:DropDownList ID="ddlFaqGroups" runat="server" Enabled="false" Visible="false"
                    CssClass="userselect" DataTextField="FaqGroupDesc" 
                    DataValueField="FaqGroupId" Width="20px">
                </asp:DropDownList>
                
                <asp:Label ID="lblFaqGroups" runat="server" meta:resourcekey="lblFaqGroups"  Visible="false"
                    Text="Nhóm:"></asp:Label>
                        
            </td>
        </tr>       
        <tr>
            <td style="width:90px">
                <asp:Label ID="lblTitle" runat="server" 
                    Text="Tiêu đề:" meta:resourcekey="lblTitle"></asp:Label>
                     <asp:RequiredFieldValidator ID="rfvTitle" runat="server" 
                    ControlToValidate="txtTitle" Display="Dynamic" ErrorMessage="(*)" 
                    Font-Bold="true" ForeColor="Red" ValidationGroup="G1"></asp:RequiredFieldValidator>
                </td>
            <td colspan="3">
                <asp:TextBox ID="txtTitle" runat="server" CssClass="tukhoatimekiem" 
                    Width="97%" ></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td>
             <asp:Label ID="lblMsgContent" runat="server" Text="Nội dung:" meta:resourcekey="lblMsgContent"></asp:Label>
            </td>
            <td colspan="3">
                <CKEditor:CKEditorControl ID="txtMsgContent" runat="server" 
                                BasePath="~/Integrated/ckeditor/" Height="200px" Width="98%"></CKEditor:CKEditorControl>
                </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblFaqCode" runat="server" 
                    Text="Mã:" meta:resourcekey="lblFaqCode"></asp:Label>
                </td>
            <td width="250px">
                <asp:TextBox ID="txtFaqCode" runat="server" CssClass="tukhoatimekiem" 
                    Width="240px"></asp:TextBox>
                </td>
            <td width="90px">
                <asp:Label ID="lblDataSources" runat="server" meta:resourcekey="lblDataSources" 
                    Text="Nguồn:"></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlDataSources" runat="server" 
                    CssClass="userselect" DataTextField="DataSourceDesc" 
                    DataValueField="DataSourceId" Width="250px">
                </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblFields" runat="server" meta:resourcekey="lblFields" 
                    Text="Lĩnh vực:"></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlFields" runat="server"
                    CssClass="userselect" DataTextField="FieldDesc" DataValueField="FieldId" Width="250px">
                </asp:DropDownList>
            
            </td>
            <td>
            <asp:Label ID="lblReviewStatus" runat="server" meta:resourcekey="lblReviewStatus" 
                    Text="Trạng thái:"></asp:Label>
            </td>
            <td>
                 <asp:DropDownList ID="ddlReviewStatus" runat="server" 
                    CssClass="userselect" DataTextField="ReviewStatusDesc" 
                    DataValueField="ReviewStatusId" Width="250px">
                </asp:DropDownList>
                
            </td>
        </tr>
        <tr>
            <td valign="top"><asp:Label ID="lblFileUpload" runat="server" Text="File Upload:" 
                    meta:resourcekey="lblFileUpload"></asp:Label>
                </td>
            <td colspan="3">
                <div id="uploads" style="display:inline">
                    <asp:FileUpload ID="fUpd" runat="server" />
                </div>
                <div id="Div1" style="display:inline">
                    <a href="javascript: void(0);" onclick="javascript: AddUpload();"><asp:Label ID="lblAddFile" runat="server" Text="Thêm file:" 
                    meta:resourcekey="lblAddFile"></asp:Label></a></div>
                     <asp:GridView ID="m_grid_File" DataKeyNames="FaqFileId" runat="server" ShowHeader="False"
                        AutoGenerateColumns="False" CssClass="grid" PageSize="50" 
        OnRowDeleting="m_grid_File_RowDeleting" CellPadding="2" ForeColor="#333333" 
        GridLines="None">
                        <HeaderStyle CssClass="grid_head" BackColor="#5D7B9D" Font-Bold="True" 
                            ForeColor="White" />
                        <FooterStyle CssClass="grid_foot" BackColor="#5D7B9D" Font-Bold="True" 
                            ForeColor="White" />
                        <RowStyle CssClass="grid_item" BackColor="#F7F6F3" ForeColor="#333333" />
                        <AlternatingRowStyle CssClass="grid_item_alternating" BackColor="White" 
                            ForeColor="#284775" />
                        <SelectedRowStyle CssClass="grid_selected" BackColor="#E2DED6" Font-Bold="True" 
                            ForeColor="#333333" />
                        <EditRowStyle CssClass="grid_edit" BackColor="#999999" />
                        <PagerStyle CssClass="grid_page" BackColor="#284775" ForeColor="White" 
                            HorizontalAlign="Center" />
                        <Columns>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>                                    
                                    <a href='../../../<%# Eval("FilePath") %>'><%# Eval("FileName")%></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Xóa" ShowHeader="False">
                                <ItemTemplate>
                                         <asp:LinkButton ID="lbDelete" runat="server" CssClass="iconadmxoa" CommandName="Delete" ToolTip="Xóa" meta:resourcekey="lnkGridColumnDel" OnClientClick="return confirm('Ban co thuc su muon xoa du lieu nay?');"></asp:LinkButton> 
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
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
    </div>
    <hr style="width:100%;" /><br />
    <div style="text-align:center"><asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom" 
                    Text="Lưu thông tin" ValidationGroup="G1" meta:resourcekey="btnSave" 
            onclick="btnSave_Click"> </asp:LinkButton></div>
</asp:Content>

