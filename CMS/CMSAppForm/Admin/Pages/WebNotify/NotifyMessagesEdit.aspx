<%@ Page Language="C#" MasterPageFile="~/admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="NotifyMessagesEdit.aspx.cs" Inherits="admin_pages_NotifyMessagesEdit" Title="" %>
<asp:Content ID="contentAccountType" runat="server" ContentPlaceHolderID="m_contentBody">   
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
        $(".tbdatepicker").datepicker({ dateFormat: 'dd/mm/yy 09:00' });
        
    });
    function InitializeRequest(sender, args) {
    }

    function EndRequest(sender, args) {
        $(function () {
            $(".tbdatepicker").datepicker({ dateFormat: 'dd/mm/yy 09:00' });
        });
    }   
    </script>
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter" >
        <tr><td></td><td></td><td><asp:Label Text="" ForeColor="Red" Font-Bold="true" ID="lblMesg" runat="server"></asp:Label></td></tr>
         <tr>
            <td>
                &nbsp;</td>
            <td>
               Loại:
            </td>
            <td>
                <asp:DropDownList ID="ddlNotifyMessageTypeId" runat="server" DataTextField="NotifyMessageTypeName" DataValueField="NotifyMessageTypeId">
                </asp:DropDownList>&nbsp;<font color="red">(*)</font>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
               Site
               &nbsp;<span style="color:red">(*)</span>:
            </td>
            <td>
                <asp:DropDownList ID="ddlSite" runat="server" DataTextField="SiteDesc" 
                            DataValueField="SiteId" Width="250px" CssClass="userselect" OnSelectedIndexChanged="ddlSite_SelectedIndexChanged" AutoPostBack="true" >
                        </asp:DropDownList>
            </td>
        </tr> 
        <tr>
            <td>
                &nbsp;</td>
            <td>
               Tiêu đề
               &nbsp;<span style="color:red">(*)</span>:
            </td>
            <td>
                <asp:TextBox ID="txTitle" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td>
                &nbsp;</td>
            <td>
               Nội dung
               :
            </td>
            <td>
                <asp:TextBox ID="txMessageContent" runat="server" CssClass="tukhoatimekiem" Width="90%" TextMode="MultiLine" Rows ="3" ></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td>
                &nbsp;</td>
            <td>
               Url
               :
            </td>
            <td>
                <asp:TextBox ID="txMessageUrl" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td>
                &nbsp;</td>
            <td>
               Icon
               :
            </td>
            <td>
                <asp:TextBox ID="txMessageIcon" runat="server" CssClass="tukhoatimekiem" Width="90%" >/Notify/images/icon.png</asp:TextBox>
            </td>
        </tr> 
          <tr>
            <td>
                &nbsp;</td>
            <td>
               Thời gian gửi:
            </td>
            <td>
                <asp:TextBox CssClass= "tbdatepicker" ID="txScheduleTime" runat="server" width="150px"></asp:TextBox>                 
            </td>
        </tr>
    </table>
    <div style="text-align:center; padding: 20px">
        <asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom"  Text="Lưu thông tin" meta:resourcekey="btnSave" 
            onclick="btnSave_Click">
        </asp:LinkButton>
    </div>
</asp:Content>

