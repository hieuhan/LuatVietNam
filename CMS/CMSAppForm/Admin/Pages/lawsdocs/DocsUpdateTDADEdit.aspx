<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="DocsUpdateTDADEdit.aspx.cs" Inherits="Admin_DocsUpdateTDADEdit" %>
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
        <%--$("#<%= txtGazetteDate.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#<%= txtEffectDate.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#<%= txtExpireDate.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });--%>
        
    });
    function InitializeRequest(sender, args) {
    }

    function EndRequest(sender, args) {
        <%--$("#<%= txtGazetteDate.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#<%= txtEffectDate.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#<%= txtExpireDate.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });--%>
    }
    
   
    </script>
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td class="VL_IssueDate_C1">
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
            <td class="VL_IssueDate_C1">
            <asp:Label ID="lblDocIdentity" runat="server" 
                    Text="Số hiệu:" meta:resourcekey="lblDocIdentity"></asp:Label>
            </td>
            <td>
             <asp:Label ID="lblDocIdentityEdit" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="VL_IssueDate_C1">
            <asp:Label ID="lblDocTypes" runat="server" 
                    Text="Loại văn bản:" meta:resourcekey="lblDocTypes"></asp:Label>
            </td>
            <td>
            <asp:Label ID="lblDocTypesEdit" runat="server"></asp:Label>
            
            </td>
        </tr>
        <tr>
            <td class="VL_IssueDate_C1">
             <asp:Label ID="lblIssueDate" runat="server" 
                    Text="Ngày ban hành:" meta:resourcekey="lblIssueDate"></asp:Label>
            </td>
            <td>
            <asp:Label ID="lblIssueDateEdit" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="VL_IssueDate_C1">
             <asp:Label ID="lblOrgans" runat="server" 
                    Text="Cơ quan ban hành:" meta:resourcekey="lblOrgans"></asp:Label>
            </td>
            <td>
            <asp:Label ID="lblOrganEdit" runat="server"></asp:Label>
            </td>
        </tr>
         <tr>
            <td class="VL_IssueDate_C1">
             <asp:Label ID="lblSigners" runat="server" 
                    Text="Người ký:" meta:resourcekey="lblSigners"></asp:Label>
            </td>
            <td>
            <asp:Label ID="lblSignersEdit" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="VL_IssueDate_C1">
             <asp:Label ID="lblFields" runat="server" 
                    Text="Lĩnh vực:" meta:resourcekey="lblFields"></asp:Label>
            </td>
            <td>
            <asp:Label ID="lblFieldEdit" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="VL_IssueDate_C1">
             <asp:Label ID="lblGazetteNumber" runat="server" Text="Số công báo:" meta:resourcekey="lblGazetteNumber"></asp:Label>
             </td>
            <td>
                <asp:TextBox ID="txtGazetteNumber" runat="server" CssClass="tukhoatimekiem" 
                    Width="240px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="VL_IssueDate_C1">
             <asp:Label ID="lblGazetteDate" runat="server" Text="Ngày công báo:" 
                    meta:resourcekey="lblGazetteDate"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtGazetteDate" runat="server" CssClass="tukhoatimekiem" 
                    Width="240px"></asp:TextBox>
                <a class="help-lnk" href="javascript:void(0)" title="Chọn hoặc nhập ngày tháng theo định dang dd/MM/yyyy" ><span class="aui-icon aui-icon-small aui-iconfont-help">?</span></a>
                (dd/MM/yyyy)<br />
                <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="Ngày bạn nhập không hợp lệ" ControlToValidate="txtGazetteDate" ForeColor="Red" SetFocusOnError="true" Display="Dynamic" Operator="DataTypeCheck" Type="Date" ValidationGroup="G1"></asp:CompareValidator>
            </td>
        </tr>
        <%--<tr>
            <td class="VL_IssueDate_C1">
             <asp:Label ID="lblEffectDate" runat="server" Text="Ngày hiệu lực:" 
                    meta:resourcekey="lblEffectDate"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtEffectDate" runat="server" CssClass="tukhoatimekiem" 
                    Width="240px"></asp:TextBox>
                <a class="help-lnk" href="javascript:void(0)" title="Chọn hoặc nhập ngày tháng theo định dang dd/MM/yyyy" ><span class="aui-icon aui-icon-small aui-iconfont-help">?</span></a>
                 (dd/MM/yyyy)<br />
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Ngày bạn nhập không hợp lệ" ControlToValidate="txtEffectDate" ForeColor="Red" SetFocusOnError="true" Display="Dynamic" Operator="DataTypeCheck" Type="Date" ValidationGroup="G1"></asp:CompareValidator>
            </td>
        </tr>--%>
        <%--<tr>
            <td class="VL_IssueDate_C1">
             <asp:Label ID="lblExpireDate" runat="server" Text="Ngày hết hiệu lực:" 
                    meta:resourcekey="lblExpireDate"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtExpireDate" runat="server" CssClass="tukhoatimekiem" 
                    Width="240px"></asp:TextBox> 
                <a class="help-lnk" href="javascript:void(0)" title="Chọn hoặc nhập ngày tháng theo định dang dd/MM/yyyy" ><span class="aui-icon aui-icon-small aui-iconfont-help">?</span></a>
                (dd/MM/yyyy)<br />
                <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Ngày bạn nhập không hợp lệ" ControlToValidate="txtExpireDate" ForeColor="Red" SetFocusOnError="true" Display="Dynamic" Operator="DataTypeCheck" Type="Date" ValidationGroup="G1"></asp:CompareValidator>
            </td>
        </tr>--%>
    </table>
    <br />
    <div style="text-align:center"><asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom" 
                    Text="Lưu thông tin" meta:resourcekey="btnSave" ValidationGroup="G1"
            onclick="btnSave_Click"> </asp:LinkButton></div>
</asp:Content>

