<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" ValidateRequest="false"
AutoEventWireup="true" CodeFile="ArticlesEditRevTime.aspx.cs" Inherits="Admin_ArticlesEditRevTime" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <script type="text/javascript">
        
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);
            $("#<%= txtRevDateTime.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy 09:00:00' });
        });
        function InitializeRequest(sender, args) {

        }
        function EndRequest(sender, args) {
            $("#<%= txtRevDateTime.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy 09:00:00' });
        }
    </script>
    <div style="  text-align: center;  width: 100%; padding:6px;">
        <span class="icon-required"></span>
        <asp:TextBox ID="txtRevDateTime" runat="server" Width="150px"></asp:TextBox>
        <br /><asp:RequiredFieldValidator ID="rfvttxtRevDateTime" runat="server" ErrorMessage="Thời gian xuất bản" ForeColor="Red" ControlToValidate="txtRevDateTime" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
        <br />
        <br />
        <asp:LinkButton ID="btnSave" runat="server" CssClass="luuvaquaylai" 
            Text="Lưu thông tin" meta:resourcekey="btnSave" ValidationGroup="G1" onclick="btnSave_Click">
        </asp:LinkButton>
    </div>
</asp:Content>

