<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master"
    AutoEventWireup="true" CodeFile="VotesEdit.aspx.cs" Inherits="Admin_VotesEdit" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" runat="Server">
    <script type="text/javascript">
        $(function () {
            $("#<%= txtStartTime.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        });
        $(function () {
            $("#<%= txtEndTime.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        });
    </script>

    <div style="text-align:center"><asp:Label ID="lblMessage" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label></div>
    <table  runat="server" cellspacing="0" cellpadding="3" width="100%" border="0">
        <tr>
            <td class="td-edit-2">
                &nbsp;
            </td>
            <td>
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td-edit-2">
                <asp:Label ID="lblSite" runat="server" Text="Site:" meta:resourcekey="lblSite"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlSite" runat="server" DataTextField="SiteDesc" DataValueField="SiteId"
                    CssClass="userselect" Width="250px" AutoPostBack="True" 
                    onselectedindexchanged="ddlSite_SelectedIndexChanged">
                </asp:DropDownList>
                
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblDataType" runat="server" Text="Loại dữ liệu:" meta:resourcekey="lblDataType"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlDataType" runat="server" DataTextField="DataTypeDesc" DataValueField="DataTypeId"
                    Width="250px" AutoPostBack="True" CssClass="userselect"
                    onselectedindexchanged="ddlDataType_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="td-edit-2">
                <asp:Label ID="lblParentCategory" runat="server" meta:resourcekey="lblParentCategory"
                    Text="Chuyên mục:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlParentCategory" runat="server" CssClass="userselect" DataTextField="CategoryDesc"
                    DataValueField="CategoryId" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr id="trLanguage" runat="server" visible="false">
            <td class="td-edit-2">
                <asp:Label ID="lblLanguage" runat="server" Text="Ngôn ngữ:" meta:resourcekey="lblLanguage"></asp:Label>
                
            </td>
            <td>
                <asp:DropDownList ID="ddlLanguage" runat="server" DataTextField="LanguageDesc" DataValueField="LanguageId"
                    CssClass="userselect" Width="250px">
                </asp:DropDownList>
                
            </td>
        </tr>
        <tr id="trApp" runat="server" visible="false">
            <td class="td-edit-2">
                <asp:Label ID="lblAppType" runat="server" meta:resourcekey="lblAppType" Text="Loại ứng dụng:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlAppType" runat="server" CssClass="userselect" DataTextField="ApplicationTypeDesc"
                    DataValueField="ApplicationTypeId" Width="250px">
                </asp:DropDownList>
                
            </td>
        </tr>
        <tr>
            <td class="td-edit-2">
                <asp:Label ID="lblVoteName" runat="server" Text="Tên:" meta:resourcekey="lblVoteName"></asp:Label>
                <span class="icon-required"></span>
            </td>
            <td>
                <asp:TextBox ID="txtVoteName" runat="server" CssClass="tukhoatimekiem" Width="400px"></asp:TextBox>
                <br />
               <br /><asp:RequiredFieldValidator ID="rfvtxtVoteName" runat="server" ErrorMessage="Bạn chưa nhập tên bình chọn" ForeColor="Red" ControlToValidate="txtVoteName" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator> 
            </td>
        </tr>
        <tr>
            <td class="td-edit-2">
                <asp:Label ID="lblVoteDesc" runat="server" Text="Mô tả:" meta:resourcekey="lblVoteDesc"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtVoteDesc" runat="server" CssClass="tukhoatimekiem" Width="400px"
                    Height="53px" TextMode="MultiLine"></asp:TextBox>
                
            </td>
        </tr>
        <tr>
            <td class="td-edit-2">
                <asp:Label ID="lblVoteTypes" runat="server" meta:resourcekey="lblVoteTypes" Text="Kiểu bình chọn:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlVoteTypes" runat="server" CssClass="userselect" DataTextField="VoteTypeDesc"
                    DataValueField="VoteTypeId" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="td-edit-2">
                <asp:Label ID="lblStartTime" runat="server" Text="Thời gian bắt đầu:" meta:resourcekey="lblStartTime"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtStartTime" runat="server" CssClass="tukhoatimekiem" Width="100px"></asp:TextBox>
                <a class="help-lnk" href="javascript:void(0)" title="Chọn hoặc nhập ngày tháng theo định dang dd/MM/yyyy" ><span class="aui-icon aui-icon-small aui-iconfont-help">?</span></a>
                <br />
                <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="Ngày bạn nhập không hợp lệ" ControlToValidate="txtStartTime" ForeColor="Red" SetFocusOnError="true" Display="Dynamic" Operator="DataTypeCheck" Type="Date" ValidationGroup="G1"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td class="td-edit-2">
                <asp:Label ID="lblEndTime" runat="server" Text="Thời gian kết thúc:" meta:resourcekey="lblEndTime"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtEndTime" runat="server" CssClass="tukhoatimekiem" Width="100px"></asp:TextBox>
                <a class="help-lnk" href="javascript:void(0)" title="Chọn hoặc nhập ngày tháng theo định dang dd/MM/yyyy" ><span class="aui-icon aui-icon-small aui-iconfont-help">?</span></a>
                <br />
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Ngày bạn nhập không hợp lệ" ControlToValidate="txtEndTime" ForeColor="Red" SetFocusOnError="true" Display="Dynamic" Operator="DataTypeCheck" Type="Date" ValidationGroup="G1"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td class="td-edit-2">
                <asp:Label ID="lblReviewStatus" runat="server" meta:resourcekey="lblReviewStatus"
                    Text="Trạng thái:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlReviewStatus" runat="server" CssClass="userselect" DataTextField="ReviewStatusDesc"
                    DataValueField="ReviewStatusId" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="td-edit-2" colspan="2">
                <br />
                 <div style="text-align:center">
        <asp:CheckBox ID="cblAddOther" runat="server" Text="Thêm tiếp dữ liệu khác" Checked="false" ToolTip="Khi tick chọn sẽ xóa form hiện tại để thêm dữ liệu khác"  />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom" 
                    Text="Lưu thông tin" meta:resourcekey="btnSave"  ValidationGroup="G1"
            onclick="btnSave_Click">
                        </asp:LinkButton>
    </div>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="m_header">
    <style type="text/css">
        .tbInputLong
        {
        }
    </style>
</asp:Content>
