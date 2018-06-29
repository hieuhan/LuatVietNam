<%@ Page Title="" ValidateRequest="false" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="DocsUpdateContentEdit.aspx.cs" Inherits="Admin_DocsUpdateContentEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" runat="Server">
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
            if (typeof CKEDITOR == 'undefined') {

            }
            else {
                //var editor = CKEDITOR.replace('<%= txtDocContent.ClientID %>');
                var editor = CKEDITOR.replace('<%= txtDocContent.ClientID %>', {
                    filebrowserBrowseUrl: '../Articles/MediasSelect.aspx?SetMediaDomain=1',
                    filebrowserUploadUrl: '/uploader/upload.php?type=Files'
                });
                //CKFinder.setupCKEditor(editor, '<%= ICSoft.CMSLib.CmsConstants.ROOT_PATH %>Integrated/ckfinder/');
            }
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);
     
        
        });

        function createEditor(languageCode, id) {
            CKEDITOR.replace(id, {
                extraPlugins: 'lawsvnReference',
            });
        }

        $(function () {
            createEditor('vi', <%=txtDocContent.ClientID%>);
    });

        function InitializeRequest(sender, args) {
        }

        function EndRequest(sender, args) {
      
        }
    
   
    </script>
    <table cellpadding="3" cellspacing="0" style="width: 100%; font-weight: lighter">
        <tr>
            <td width="90px">
                <asp:Label ID="lblDocName" runat="server"
                    Text="Tên văn bản:" meta:resourcekey="lblDocName"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblDocNameEdit" runat="server"></asp:Label>
                <asp:DropDownList ID="ddlLanguage" runat="server" Enabled="false" Visible="false"
                    CssClass="userselect" DataTextField="LanguageDesc" DataValueField="LanguageId" Width="150px">
                </asp:DropDownList>

                <asp:Label ID="lblLanguage" runat="server" meta:resourcekey="lblLanguage" Visible="false"
                    Text="Ngôn ngữ:"></asp:Label>

            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblDocContent" runat="server" Text="Nội dung:" meta:resourcekey="lblDocContent"></asp:Label>
                <%--<asp:RequiredFieldValidator ID="rfvDocContent" runat="server" Text="(*)" 
                    ControlToValidate="txtDocContent" Display="Dynamic" ErrorMessage="(*)" 
                    Font-Bold="true" ForeColor="Red" ValidationGroup="G1"></asp:RequiredFieldValidator>--%>
            </td>
            <td>
                <CKEditor:CKEditorControl ID="txtDocContent" runat="server"
                    BasePath="~/Integrated/ckeditor/" Height="450px" Width="98%"></CKEditor:CKEditorControl>
            </td>
        </tr>
    </table>
    <br />
    <div style="text-align: center">
        <asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom"
            Text="Lưu thông tin" ValidationGroup="G1" meta:resourcekey="btnSave" ToolTip="Lưu thông tin"
            OnClick="btnSave_Click"> </asp:LinkButton>
        <asp:LinkButton ID="btnBack" runat="server" CssClass="quaylai"
            Text="Quay lại" meta:resourcekey="btnBack"
            OnClick="btnBack_Click">
        </asp:LinkButton>
    </div>
</asp:Content>

