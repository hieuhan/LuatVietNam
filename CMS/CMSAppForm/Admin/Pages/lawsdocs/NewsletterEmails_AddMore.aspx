<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="NewsletterEmails_AddMore.aspx.cs" Inherits="Admin_NewsletterEmails_AddMore" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/admin/UserControls/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="uc1" %>
<%@ Import Namespace="ICSoft.CMSLib" %>
<%@ Import Namespace="ICSoft.LawDocsLib" %>
<%@ Import Namespace="ICSoft.HelperLib" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
     <script type="text/javascript">
         $(document).ready(function () {
             var prm = Sys.WebForms.PageRequestManager.getInstance();
             prm.add_initializeRequest(InitializeRequest);
             prm.add_endRequest(EndRequest);
            $('a#popup').live('click', function (e) {
                var page = $(this).attr("href")
                var cdialog = $('<div id="divEdit"></div>')
                .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="98%" height="98%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 400,
                    width: 500,
                    title: $(this).attr("title"),
                    close: function (event, ui) {
                        $(this).remove();
                        try {
                            $('#<%= btnRefresh.ClientID %>')[0].click();
                       } catch (ex) {
                       }

                    }
                });
                cdialog.dialog('open');
                e.preventDefault();
            });
        });
        function InitializeRequest(sender, args) {
        }

        function EndRequest(sender, args) {
        }
    </script>
    <div class="addmail" style="width:38%; float:left;">
        <asp:Label runat="server" ForeColor="Red" Font-Bold="true" Text=""></asp:Label>
    <table cellpadding="3" cellspacing="5" style="width:100%; font-weight:lighter">
        <tr><td></td><td>KH: <label runat="server" class="tieudetongcong" Font-Bold="true" ID="lblCustomerFullName"> <%: mCustomers.CustomerFullName %></label> (<%: mCustomers.CustomerMail %>)</td></tr>
       <tr>
           
            <td ><asp:Label ID="lblEmail" runat="server" Text="Email:" meta:resourcekey="lblEmail"></asp:Label><span class="icon-required"></span>
                </td>
            <td>
                <asp:TextBox TextMode="MultiLine" Rows ="10" ID="txtEmail" runat="server" CssClass="tukhoatimekiem" Width="300px"></asp:TextBox><br />
                <span class="node-help">Danh sách Email phân cách bằng dấu <strong>;</strong></span>
                <br />
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" 
                    ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="Bạn cần nhập Email" 
                    Font-Bold="true" ForeColor="Red" ValidationGroup="G1"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValid" runat="server" ErrorMessage="Email bạn nhập chưa đúng định dạng (vd: name@email.com)" ValidationGroup="G1" ForeColor="Red" ClientValidationFunction="CheckListEmail" ControlToValidate="txtEmail" />
                <%--<asp:RegularExpressionValidator ID="validateEmail"    
  runat="server" ErrorMessage=" Email bạn nhập chưa đúng định dạng (vd: abc@gmail.com)"
  ControlToValidate="txtEmail"  ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" />--%>
                </td>
        </tr>      
       <tr>
           <td colspan="2" style="text-align:center;">
                <asp:Button ID="btnSave" runat="server" CssClass="luuvathemmoi" 
                    Text="Lưu thông tin" ValidationGroup="G1" meta:resourcekey="btnSave" 
            onclick="btnSave_Click" > </asp:Button>  <asp:LinkButton ID="btnRefresh" runat="server" CssClass="timkiembutom hidden-btn-dqs" Text="Refresh" onclick="btnRefresh_Click"></asp:LinkButton>
           </td>
       </tr>
    </table>
        </div>
    <script type="text/javascript">
        var id_txtEmail = "<%:txtEmail.ClientID%>"; 
        function CheckListEmail(source, arguments) {
            arguments.IsValid = false;
            var ck = checkBeforeSaveEmail();
            if (!ck) {
                arguments.IsValid = false;
                $("#<%:CustomValid.ClientID%>").css({ visibility: "visible" });
            } else {
                arguments.IsValid = true;
                $("#<%:CustomValid.ClientID%>").css({ visibility: "hidden" });
            }
        }
        function checkBeforeSaveEmail() {
            var vl_txtEmail = $("#" + id_txtEmail).val();
            if (vl_txtEmail != null && vl_txtEmail.length) {
                var ls_Email = vl_txtEmail.split(";");
                if (ls_Email.length) {
                    for (var i = 0; i < ls_Email.length; i++) {
                        var ival_email = ls_Email[i];
                        if (ival_email.length&&!validateEmail(ival_email)) {
                            findWord(id_txtEmail, ival_email);
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        function validateEmail(emailField) {
            if (emailField != null && emailField.length) { 
                var reg = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;///[A-Z0-9._%+-]+@[A-Z0-9-]+.+.[A-Z]{2,4}/igm;
                if (!reg.test(emailField.trim())) {
                    return false;
                }
                return true;
            }
            return false;

        }
        function findWord(id_textarea, str) {
            try {
            if (id_textarea != null && str != null && id_textarea.length && str.length) {
                var my_textarea = $('#' + id_textarea); 
                var posi = my_textarea.val().indexOf(str); // take the position of the word in the text
                if (posi != -1) {
                    var target = document.getElementById(id_textarea);
                    // seleziono la parola
                    target.focus();
                    if (target.setSelectionRange)
                        target.setSelectionRange(posi, posi + str.length);
                    else {
                        var r = target.createTextRange();
                        r.collapse(true);
                        r.moveEnd('character', posi + str);
                        r.moveStart('character', posi);
                        r.select();
                    } var objDiv = document.getElementById(id_textarea);
                    var sh = objDiv.scrollHeight; //height in pixel of the textarea
                    var line_ht = 16;
                    try {
                        var line_ht =parseInt($(my_textarea[0]).css('line-height').replace('px', ''));//height in pixel of each row
                    } catch (ex) {
                    } 
                    var n_lines = sh / line_ht; // total amount of lines in the textarea
                    var char_in_line = my_textarea.val().length / n_lines; // the total amount of chars in each row
                    var height = Math.floor(posi / char_in_line); // height in number of rows of the searched word
                    my_textarea.scrollTop(height * line_ht); // scroll to the selected line containing the word
                } 
            }
            } catch (ex) { }
        }
    </script>
    <div style="width:60%; float:left;">

        <div class="khungchucnang">
            <div class="chucnangleft">
                <span class="tieudetongcong">Tổng cộng: </span>
                <asp:Label ID="lblTong" runat="server" Text="" CssClass="tieudetongcong2"></asp:Label>
                Email
            </div>
            <div class="chucnangright">
                <asp:Label ID="lblOrderBy" runat="server" Text="Sắp xếp: " CssClass="tieudetongcong2"></asp:Label>
                <asp:DropDownList ID="ddlOrderBy" runat="server" AutoPostBack="True"  CssClass="userselect" 
                    onselectedindexchanged="ddlOrderBy_SelectedIndexChanged" Width="150px">
                    <asp:ListItem Value="CrDateTime Desc">Mới thêm</asp:ListItem>
                    <asp:ListItem Value="Email ASC">Theo email</asp:ListItem> 
                </asp:DropDownList>
                <asp:LinkButton ID="lbDelete" runat="server" CssClass="xoatin" Text="Xóa" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa các email đã chọn?')"
                    meta:resourcekey="lbDelete" OnClick="lbDelete_Click"></asp:LinkButton>
            </div>
        </div>
        <div class="clear5px"></div>
    <div class="contenbangdulieu">
    
        <asp:GridView ID="m_grid" DataKeyNames="NewsletterMemberId" runat="server" ShowHeaderWhenEmpty="True"
            AutoGenerateColumns="False" 
            OnRowDeleting="m_grid_RowDeleting" OnRowDataBound = "m_grid_OnRowDataBound"
            Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" PageSize="200">
            <HeaderStyle CssClass="trbangdulieutieude" />
            <RowStyle CssClass="trbangdulieutieudenoidung" />
            <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
            <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />                                        
            <Columns>                        
                <asp:TemplateField >
                    <HeaderTemplate>                                
                    #
                    </HeaderTemplate>
                    <ItemTemplate>
                    <asp:Label ID="lbNo" runat="server" Text='<%# Container.DataItemIndex + (CustomPaging.PageIndex - 1) * m_grid.PageSize + 1%>' 
                                     ToolTip ='<%# Eval("NewsletterMemberId").ToString() %>'> </asp:Label>
                    </ItemTemplate>      
                    <ItemStyle HorizontalAlign="Center" Width="3%" />  
                    <HeaderStyle Width="3%" />          
                </asp:TemplateField>       
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnEmail" runat="server" Text="Email" meta:resourcekey="lblGridColumnEmail"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <%# Eval("Email")%> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                    <HeaderStyle/>          
                </asp:TemplateField>       
                <asp:TemplateField > 
                        <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnEmail" runat="server" Text="Trạng thái" ></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <span class="xuatban<%# Eval("NewsletterMemberStatusId") %>" >
                            <%# LanguageHelpers.IsCultureVietnamese() ? NewsletterMemberStatus.Static_Get(byte.Parse(Eval("NewsletterMemberStatusId").ToString()), l_NewsletterMemberStatus).NewsletterMemberStatusDesc : NewsletterMemberStatus.Static_Get(byte.Parse(Eval("NewsletterMemberStatusId").ToString()), l_NewsletterMemberStatus).NewsletterMemberStatusName%>
                        </span> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="20%" />
                    <HeaderStyle/>          
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>                                
                        <asp:Label ID="lblGridColumnOperation" runat="server" Text="Thao tác" meta:resourcekey="lblGridColumnOperation"></asp:Label>
                    </HeaderTemplate> 
                    <ItemTemplate> 
                        <a id="popup" onmouseover="ShowMenu('-1-<%# Eval("NewsletterMemberId") %>')"  WHeight="400" WWidth="550" href='NewsletterMembersEdit.aspx?NewsletterMemberId=<%# Eval("NewsletterMemberId") %>' class="iconadmsua"
                        title="" meta:resourcekey="lnkGridColumnEdit"></a>    
                        <asp:LinkButton ID="lbDelete" runat="server" CssClass="iconadmxoa" CommandName="Delete" ToolTip="Xóa" meta:resourcekey="lnkGridColumnDel"></asp:LinkButton> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="center"  Wrap="false" />
                    <HeaderStyle Width="12%" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <HeaderStyle Width="5%" />
                    <HeaderTemplate>
                        <input type="checkbox" name="SelectAllCheckBox" onclick="SelectAll(this)">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkAction" runat="server"></asp:CheckBox>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div class="clear5px"></div>    
                <uc1:CustomPaging ID="CustomPaging" runat="server" />                
                <div class="clear5px"></div>  

    </div>
</asp:Content>

