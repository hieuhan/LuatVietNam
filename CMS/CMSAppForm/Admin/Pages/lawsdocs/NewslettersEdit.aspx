<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="NewslettersEdit.aspx.cs" Inherits="Admin_NewslettersEdit" %>
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
        $("#<%= txtScheduleSendTime.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy 09:00' });
        
    });
    function InitializeRequest(sender, args) {
    }

    function EndRequest(sender, args) {
        $(function () {
            $("#<%= txtScheduleSendTime.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy 09:00' });
        });
    }   
    </script>
    <div style="width:auto; height:480px; overflow:auto;"> 
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td style="width:160px">
                <asp:Label ID="lblNewsletterGroups" runat="server" meta:resourcekey="lblNewsletterGroups" 
                    Text="Nhóm bản tin:"></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlNewsletterGroups" runat="server" 
                    CssClass="userselect" DataTextField="NewsletterGroupDesc" 
                    DataValueField="NewsletterGroupId" Width="250px">
                </asp:DropDownList>
                </td>
        </tr>
         <tr>
            <td><asp:Label ID="lblScheduleSendTime" runat="server" 
                    Text="Lịch gửi:" meta:resourcekey="lblScheduleSendTime"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtScheduleSendTime" runat="server" CssClass="tukhoatimekiem" 
                    Width="240px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td style="width:160px"><asp:Label ID="lblSendFrom" runat="server" Text="Gửi từ:" meta:resourcekey="lblSendFrom"></asp:Label>
                <span class="icon-required"></span>
                </td>
            <td>
                <asp:TextBox ID="txtSendFrom" runat="server" CssClass="tukhoatimekiem" Width="240px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvSendFrom" runat="server" 
                    ControlToValidate="txtSendFrom" Display="Dynamic" ErrorMessage="Bạn cần nhập nơi gửi" 
                    Font-Bold="true" ForeColor="Red" ValidationGroup="G1"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td style="width:160px"><asp:Label ID="lblTitle" runat="server" 
                    Text="Tiêu đề:" meta:resourcekey="lblTitle"></asp:Label><span class="icon-required"></span>

                </td>
            <td>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="tukhoatimekiem" 
                    Width="96%" ></asp:TextBox>
                     <asp:RequiredFieldValidator ID="rfvTitle" runat="server" 
                    ControlToValidate="txtTitle" Display="Dynamic" ErrorMessage="Bạn cần nhập tiêu đề" 
                    Font-Bold="true" ForeColor="Red" ValidationGroup="G1"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td style="width:160px">
            <asp:Label ID="lblMsgContent" runat="server" Text="Nội dung:" meta:resourcekey="lblMsgContent"></asp:Label><span class="icon-required"></span>

            </td>
            <td>
                <CKEditor:CKEditorControl ID="txtMsgContent" runat="server" 
                                BasePath="~/Integrated/ckeditor/" Height="200px" Toolbar="Basic" Width="97%"></CKEditor:CKEditorControl>
            <asp:RequiredFieldValidator ID="rfvMsgContent" runat="server" 
                    ControlToValidate="txtMsgContent" Display="Dynamic" ErrorMessage="Bạn cần nhập nội dung" 
                    Font-Bold="true" ForeColor="Red" ValidationGroup="G1"></asp:RequiredFieldValidator>
                </td>
        </tr>       
        <tr>
            <td><asp:Label ID="lblFileUpload" runat="server" Text="File bản tin:" 
                    meta:resourcekey="lblFileUpload"></asp:Label>
                </td>
            <td>
                <div id="uploads" style="display:inline">
                    <asp:FileUpload ID="fUpd" runat="server" />
                </div>
                <div id="Div1" style="display:inline">
                    <a href="javascript: void(0);" onclick="javascript: AddUpload();"><asp:Label ID="lblAddFile" runat="server" Text="Thêm file:" 
                    meta:resourcekey="lblAddFile"></asp:Label></a></div>
                     <asp:GridView ID="m_grid_File" DataKeyNames="NewsletterFileId" runat="server" ShowHeader="False"
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
        <tr>
            <td><asp:Label ID="lblNewsletterStatus" runat="server" Text="Trạng thái:" meta:resourcekey="lblNewsletterStatus"></asp:Label>
                </td>
            <td><asp:DropDownList ID="ddlNewsletterStatus" runat="server" CssClass="userselect"  Width="250px"
                    DataTextField="NewsletterStatusDesc" DataValueField="NewsletterStatusId"></asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    </div>
    <hr style="width:100%;" /><br />
        <div style="background-color: #FFFFFF; bottom: 0px;  padding: 8px;  position: fixed;   right: 1px;   text-align: right;  width: 100%;">
<asp:CheckBox ID="cblAddOther" runat="server" Text="Thêm tiếp dữ liệu khác" Checked="false" ToolTip="Khi tick chọn sẽ xóa form hiện tại để thêm dữ liệu khác"  />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

        <asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom" 
                    Text="Lưu thông tin" ValidationGroup="G1" meta:resourcekey="btnSave" 
            onclick="btnSave_Click"> </asp:LinkButton></div>
</asp:Content>

