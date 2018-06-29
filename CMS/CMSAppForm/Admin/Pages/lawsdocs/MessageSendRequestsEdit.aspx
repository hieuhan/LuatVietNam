<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="MessageSendRequestsEdit.aspx.cs" Inherits="Admin_Pages_lawsdocs_MessageSendRequestsEdit" %>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="m_header">
    <link href="../../Styles/jquery.datetimepicker.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.datetimepicker.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="contentAccountType" runat="server" ContentPlaceHolderID="m_contentBody">
    <script type="text/javascript">
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);
            jQuery(function () {
                jQuery('#<%= txtBeginDateTime.ClientID %>').datetimepicker({
                    lang: 'vi',
                    format: 'd-m-Y H:i'
                });
                jQuery('#<%= txtEndDateTime.ClientID %>').datetimepicker({
                    lang: 'vi',
                    format: 'd-m-Y H:i'
                });
            });

            $('a.popup').on('click',
                function (e) {
                    e.preventDefault();
                    var name = 'popUp';
                    var popUrl = $(this).attr("href");
                    var w = $(this).attr("WWidth");
                    var appearence = 'dependent=yes,menubar=no,resizable=yes,scrollbars=yes,' +
                        'status=no,toolbar=no,titlebar=no,' +
                        'left=' + (screen.width - w) / 2 + ',top=30,width=' + w + ',height=' + $(this).attr("WHeight");
                    var openWindow = window.open(popUrl, name, appearence);
                    openWindow.focus();
                    return false;
                });
        });
        function updateDocId(docId) {
            document.getElementById("<%=txtDocId.ClientID %>").value = docId;
        }
        function updateDocName(docName) {
            document.getElementById("<%=txtDocName.ClientID %>").value = docName;
        }
        function updateResult(eventName) {
            document.getElementById("<%=txtEventName.ClientID %>").value = eventName;
        }
        function toJSDate(dateTime) {
            var dt = dateTime.split(" ");
            var date = dt[0].split("-");
            var time = dt[1].split(":");
            return new Date(date[2], (date[1] - 1), date[0], time[0], time[1], 0, 0);

        }
        function ValidateDate() {
            var date1 = $('#<%= txtBeginDateTime.ClientID %>').val(), date2 = $('#<%= txtEndDateTime.ClientID %>').val();
            var future = true;
            if (Date.parse(toJSDate(date1)) >= Date.parse(toJSDate(date2))) {
                alert('Thời gian bắt đầu phải nhỏ hơn thời gian kết thúc !');
                future = false;
            }
            return future;
        }
        function InitializeRequest(sender, args) {
        }

        function EndRequest(sender, args) {
            $(function () {
                $('#<%= txtBeginDateTime.ClientID %>').datetimepicker({
                    lang: 'vi',
                    format: 'd-m-Y H:i:s'
                });
                $('#<%= txtEndDateTime.ClientID %>').datetimepicker({
                    lang: 'vi',
                    format: 'd-m-Y H:i:s'
                });
            });
        }
    </script>
    <table cellpadding="3" cellspacing="0" style="width: 100%; font-weight: lighter">
        <tr>
            <td width="145px">Bản tin:
            </td>
            <td>
                <asp:DropDownList ID="ddlRequestTypes" runat="server" DataTextField="RequestTypeDesc"
                    DataValueField="RequestTypeId" Width="250px" CssClass="userselect" OnSelectedIndexChanged="ddlRequestTypes_OnSelectedIndexChanged"
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td>
                <div id="SelectDoc" runat="server" Visible="False" ><a wheight="600" wwidth="1200" href="popupDocuments.aspx?docId=<%= docId %>" class="themmoi popup" title="Chọn văn bản">
                    <asp:Label ID="lblAddNew" runat="server" Text="Chọn văn bản" meta:resourcekey="lblAddNew"></asp:Label>
                </a></div>

            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblScheduleTime" runat="server" Text="Thời gian bắt đầu:" meta:resourcekey="lblScheduleTime"></asp:Label><span class="icon-required"></span>
            </td>
            <td colspan="2">
                <asp:TextBox CssClass="tukhoatimekiem" ID="txtBeginDateTime" runat="server" Width="242px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvScheduleTime" Text="Bạn cần chọn thời gian bắt đầu" runat="server" Display="Dynamic"
                    ErrorMessage="Thời gian bắt đầu" ValidationGroup="G1" SetFocusOnError="true"
                    ControlToValidate="txtBeginDateTime" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Thời gian kết thúc:" meta:resourcekey="lblScheduleTime"></asp:Label><span class="icon-required"></span>
            </td>
            <td colspan="2">
                <asp:TextBox CssClass="tukhoatimekiem" ID="txtEndDateTime" runat="server" Width="242px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Text="Bạn cần chọn thời gian kết thúc" runat="server" Display="Dynamic"
                                            ErrorMessage="Thời gian bắt đầu" ValidationGroup="G1" SetFocusOnError="true"
                                            ControlToValidate="txtEndDateTime" ForeColor="Red"></asp:RequiredFieldValidator><br />
                <span class="node-help" style="line-height:25px;">Lấy danh sách văn bản được duyệt trong khoảng thời gian này để tạo bản tin.</span>
            </td>
        </tr>
        <tr id="trDocName" runat="server">
            <td><asp:Label ID="Label2" runat="server" Text="Tên văn bản" meta:resourcekey="lblScheduleTime"></asp:Label><span class="icon-required"></span></td>
            <td colspan="2"><asp:TextBox ID="txtDocName" runat="server" disabled CssClass="tukhoatimekiem" 
                             Width="80%" TextMode="MultiLine" Rows="5"></asp:TextBox>
                <asp:HiddenField runat="server" ID="txtDocId" Value="0" />
                <br/>
                <asp:RequiredFieldValidator ID="rfvtxtDocName" runat="server" ErrorMessage="Vui lòng chọn văn bản" ForeColor="Red" 
                    ControlToValidate="txtDocName" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1" Enabled="False"></asp:RequiredFieldValidator></td>
        </tr>
        
        <tr id="trEvent" runat="server">
            <td><asp:Label ID="Label3" runat="server" Text="Sự kiện"></asp:Label><span class="icon-required"></span></td>
            <td colspan="2"><asp:TextBox ID="txtEventName" runat="server" CssClass="tukhoatimekiem" 
                             Width="80%" TextMode="MultiLine" Rows="2" Text=""></asp:TextBox> <br/>
                <asp:RequiredFieldValidator ID="rfvtxtEventName" runat="server" ErrorMessage="Vui lòng nhập sự kiện" ForeColor="Red" 
                    ControlToValidate="txtEventName" SetFocusOnError="true" ValidationGroup="G1" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
        </tr>
    </table>
    <div style="text-align: center; padding: 20px">
        <asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom" ValidationGroup="G1" Text="Lưu thông tin" meta:resourcekey="btnSave"
                        OnClientClick="return ValidateDate();"  OnClick="btnSave_Click">
        </asp:LinkButton>
    </div>
</asp:Content>

