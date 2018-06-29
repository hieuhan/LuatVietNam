<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master"
    AutoEventWireup="true" CodeFile="EventStreamsEdit.aspx.cs" Inherits="Admin_EventStreamsEdit" %>

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
    <table id="tblEdit" runat="server" cellspacing="0" cellpadding="3" width="100%" border="0">
        <tr>
            <td colspan="2" class="td-edit-2">
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 120px">
                <asp:Label ID="lblSite" runat="server" Text="Site:" meta:resourcekey="lblSite"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlSite" runat="server" DataTextField="SiteDesc" DataValueField="SiteId"
                    Width="355px" AutoPostBack="True" 
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
                    Width="355px" AutoPostBack="True" 
                    onselectedindexchanged="ddlDataType_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;
            </td>
        </tr>
        <tr id="trLanguage" runat="server" visible="false">
            <td style="width: 120px">
                <asp:Label ID="lblLanguage" runat="server" Text="Ngôn ngữ:" meta:resourcekey="lblLanguage"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlLanguage" runat="server" DataTextField="LanguageDesc" DataValueField="LanguageId"
                    Width="355px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr id="trApp" runat="server" visible="false">
            <td style="width: 120px">
                <asp:Label ID="lblAppType" runat="server" meta:resourcekey="lblAppType" Text="Loại ứng dụng:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlAppType" runat="server" DataTextField="ApplicationTypeDesc"
                    DataValueField="ApplicationTypeId" Width="355px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 120px">
                <asp:Label ID="lblParentCategory" runat="server" meta:resourcekey="lblParentCategory"
                    Text="Chuyên mục:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlParentCategory" runat="server" DataTextField="CategoryDesc"
                    DataValueField="CategoryId" Width="355px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 120px">
                <asp:Label ID="lblEventStreamName" runat="server" Text="Tên:" meta:resourcekey="lblEventStreamName"></asp:Label>
                <span class="icon-required"></span>
<asp:RequiredFieldValidator ID="rfvEventStreamName" runat="server" ErrorMessage="(*)" ForeColor="Red" 
ControlToValidate="txtEventStreamName" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:TextBox ID="txtEventStreamName" runat="server"  Width="355px"></asp:TextBox>
                <br />
<asp:RequiredFieldValidator ID="rfvtxtEventStreamName" runat="server" ErrorMessage="Bạn cần nhập vào tên sự kiện" ForeColor="Red" 
ControlToValidate="txtEventStreamName" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 120px">
                <asp:Label ID="lblEventStreamDesc" runat="server" Text="Mô tả:" meta:resourcekey="lblEventStreamDesc"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtEventStreamDesc" TextMode="MultiLine" runat="server" Width="355px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 120px">
                <asp:Label ID="lblImage" runat="server" Text="Ảnh đại diện:" meta:resourcekey="lblImage"></asp:Label>
            </td>
             <td>
               <img alt="txtIcon" class="ImageSelectPath" src="../../../Images/transparent.png" width="60px" height="40px" />
                <asp:TextBox CssClass="HiddenInput" ID="txtIcon" runat="server" Text=""></asp:TextBox>
                &nbsp;
                <asp:CheckBox ID="cbDeleteImages" runat="server" Text="Xóa ảnh" meta:resourcekey="cbDeleteImages" />
            </td>
        </tr>
        <tr>
            <td style="width: 120px">
                <asp:Label ID="lblStartTime" runat="server" Text="Ngày bắt đầu:" meta:resourcekey="lblStartTime"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtStartTime" runat="server" Width="150px"></asp:TextBox>
                
<a class="help-lnk" href="javascript:void(0)" title="Chọn hoặc nhập ngày tháng theo định dạng dd/MM/yyyy" >
<span class="aui-icon aui-icon-small aui-iconfont-help">?</span></a><br />               
<asp:CompareValidator ID="cvtxtStartTime" runat="server" ErrorMessage="Ngày bạn nhập không hợp lệ" ControlToValidate="txtStartTime" 
ForeColor="Red" SetFocusOnError="true" Display="Dynamic" Operator="DataTypeCheck" Type="Date" ValidationGroup="G1"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 120px">
                <asp:Label ID="lblEndTime" runat="server" Text="Ngày kết thúc:" meta:resourcekey="lblEndTime"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtEndTime" runat="server" Width="150px"></asp:TextBox>
                <a class="help-lnk" href="javascript:void(0)" title="Chọn hoặc nhập ngày tháng theo định dạng dd/MM/yyyy" >
<span class="aui-icon aui-icon-small aui-iconfont-help">?</span></a><br />               
<asp:CompareValidator ID="cvtxtEndTime" runat="server" ErrorMessage="Ngày bạn nhập không hợp lệ" ControlToValidate="txtEndTime" 
ForeColor="Red" SetFocusOnError="true" Display="Dynamic" Operator="DataTypeCheck" Type="Date" ValidationGroup="G1"></asp:CompareValidator>
            </td>
        </tr>
         <tr>
         <td>
                <asp:Label ID="lblShow" runat="server" Text="Hiển thị:" meta:resourcekey="lblShow"></asp:Label>
                &nbsp;
            </td>
            <td>
                <asp:CheckBox ID="ckbShowTop" runat="server" Text="Top" />&nbsp;
                <asp:CheckBox ID="ckbShowBottom" runat="server" Text="Bottom" />&nbsp;
                <asp:CheckBox ID="ckbShowWeb" runat="server" Text="Web" />&nbsp;
                <asp:CheckBox ID="ckbShowWap" runat="server" Text="Wap" />&nbsp;
                <asp:CheckBox ID="ckbShowApp" runat="server" Text="App" />&nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 120px">
                <asp:Label ID="lblReviewStatus" runat="server" meta:resourcekey="lblReviewStatus"
                    Text="Trạng thái:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlReviewStatus" runat="server" DataTextField="ReviewStatusDesc"
                    DataValueField="ReviewStatusId" Width="150px">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <div style="background-color: #FFFFFF; bottom: 0px;  padding: 8px;  position: fixed;   right: 1px;   text-align: right;  width: 100%;">
        <asp:CheckBox ID="cblAddOther" runat="server" Text="Thêm tiếp dữ liệu khác" Checked="false" ToolTip="Khi tick chọn sẽ xóa form hiện tại để thêm dữ liệu khác"  />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="btnSave" ValidationGroup="G1" runat="server" CssClass="savebutom" meta:resourcekey="btnSave" Text=" Lưu "
            onclick="btnSave_Click">
        </asp:LinkButton>
    </div>
</asp:Content>
