<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="DocsUpdateSummaryEdit.aspx.cs" Inherits="Admin_DocsUpdateSummaryEdit" %>

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
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);


        });
        function InitializeRequest(sender, args) {
        }

        function EndRequest(sender, args) {

        }


    </script>
    <table cellpadding="3" cellspacing="0" style="width: 100%; font-weight: lighter">
        <tr>
            <td style="width: 90px">
                <asp:Label ID="lblDocName" runat="server"
                    Text="Tên văn bản:" meta:resourcekey="lblDocName"></asp:Label>
            </td>
            <td>
                <div style="text-align: center">
                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
                <asp:Label ID="lblDocNameEdit" runat="server"></asp:Label>
                <asp:DropDownList ID="ddlLanguage" runat="server" Enabled="false" Visible="false"
                    CssClass="userselect" DataTextField="LanguageDesc" DataValueField="LanguageId" Width="150px">
                </asp:DropDownList>

                <asp:Label ID="lblLanguage" runat="server" meta:resourcekey="lblLanguage" Visible="false"
                    Text="Ngôn ngữ:"></asp:Label>

            </td>
        </tr>
        <tr id="trResult" runat="server">
            <td>
                Tiêu đề rút gọn:
            </td>
            <td>
                <asp:TextBox ID="txtResult" TextMode="MultiLine" Height="40px" runat="server" Width="98%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblDocSummary" runat="server" Text="Trích dẫn:" meta:resourcekey="lblDocSummary"></asp:Label>
                <%-- <asp:RequiredFieldValidator ID="rfvDocSummary" runat="server"  Text="(*)"
                    ControlToValidate="txtDocSummary" Display="Dynamic" ErrorMessage="(*)" 
                    Font-Bold="true" ForeColor="Red" ValidationGroup="G1"></asp:RequiredFieldValidator>--%>
            </td>
            <td>
                <CKEditor:CKEditorControl ID="txtDocSummary" runat="server"
                    BasePath="~/Integrated/ckeditor/" Height="240px" Toolbar="Basic" Width="98%"></CKEditor:CKEditorControl>
            </td>
        </tr>
    </table>
    <br />
    <div style="text-align: center">
        <asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom"
            Text="Lưu thông tin" ValidationGroup="G1" ToolTip="Lưu thông tin"
            OnClick="btnSave_Click"> </asp:LinkButton>
    </div>
</asp:Content>

