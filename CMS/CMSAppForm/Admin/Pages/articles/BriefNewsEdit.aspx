<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" ValidateRequest="false"
AutoEventWireup="true" CodeFile="BriefNewsEdit.aspx.cs" Inherits="Admin_BriefNewsEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <script type="text/javascript">
        var cdialog = $('<div id="divEdit"></div>');
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);
            $("#<%= txtDisplayStartTime.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
            $("#<%= txtDisplayEndTime.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });

        });

        function InitializeRequest(sender, args) {
            
        }

        function EndRequest(sender, args) {
            $("#<%= txtDisplayStartTime.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
            $("#<%= txtDisplayEndTime.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        }
    </script>
    <div  style=" width:auto; height:auto; overflow:auto;">
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td><asp:Label ID="lblTitle" runat="server" Text="Tiêu đề:" meta:resourcekey="lblTitle"></asp:Label>
                        <span class="icon-required"></span>
<asp:RequiredFieldValidator ID="rfvtxtTitle1" ValidationGroup="G1" runat="server" ErrorMessage="(*)" 
ControlToValidate="txtTitle" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    <td>
                        <asp:TextBox ID="txtTitle" runat="server" Width="98%"></asp:TextBox><br />
<asp:RequiredFieldValidator ID="rfvtxtTitle" runat="server" ErrorMessage="Bạn cần nhập vào tiêu đề tin vắn" ForeColor="Red" 
ControlToValidate="txtTitle" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
                        </td>
         </tr>
        <tr>
            <td>
                <asp:Label ID="lblSummary" runat="server" Text="Mô tả:" meta:resourcekey="lblSummary"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtSummaryPlain" runat="server" Height="120px" TextMode="MultiLine" Width="98%"></asp:TextBox>
                <CKEditor:CKEditorControl ID="txtSummary" runat="server" BasePath="~/Integrated/ckeditor/" Height="40px" Toolbar="Basic" Visible="false" Width="99%"></CKEditor:CKEditorControl>
            </td>
         </tr>
        <tr>
            <td><asp:Label ID="lblArticleUrl" runat="server" Text="Url bài viết:" meta:resourcekey="lblArticleUrl"></asp:Label>
                        <span class="icon-required"></span>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="G1" runat="server" ErrorMessage="(*)" 
ControlToValidate="txtArticleUrl" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    <td>
                         <asp:TextBox ID="txtArticleUrl" runat="server" Width="98%"></asp:TextBox>   <br />
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Bạn cần nhập vào url tin vắn" ForeColor="Red" 
ControlToValidate="txtArticleUrl" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
                        </td>
         </tr>
        <tr>
            <td>
                Đặt lịch hiển thị
            </td>
            <td>
                
                            
                <asp:TextBox ID="txtDisplayStartTime" runat="server" Width="35%"></asp:TextBox>&nbsp;Kết thúc:
                            <asp:TextBox ID="txtDisplayEndTime" runat="server" Width="35%"></asp:TextBox><a class="help-lnk" href="javascript:void(0)" title="Chọn hoặc nhập ngày tháng theo định dang dd/MM/yyyy hh:mm:ss" >
<span class="aui-icon aui-icon-small aui-iconfont-help">?</span></a>
                            
<br />  
<asp:RegularExpressionValidator ID="rgltxtDisplayStartTime" runat="server" ForeColor="Red" ErrorMessage="Ngày hiển thị bạn nhập không hợp lệ " Display="Dynamic" ValidationExpression="\d{2}\/\d{2}\/\d{4}\s(\d{2}\:){2}\d{2}" ControlToValidate="txtDisplayStartTime"></asp:RegularExpressionValidator>                        
<asp:RegularExpressionValidator ID="rgltxtDisplayEndTime" runat="server" ForeColor="Red" ErrorMessage="Ngày kết thúc bạn nhập không hợp lệ" Display="Dynamic" ValidationExpression="\d{2}\/\d{2}\/\d{4}\s(\d{2}\:){2}\d{2}" ControlToValidate="txtDisplayEndTime"></asp:RegularExpressionValidator>
                
            </td>
         </tr>
        <tr><td>Thứ tự</td>
            <td>
                <asp:TextBox ID="txtDisplayOrder" runat="server" Width="25%" Text="10"></asp:TextBox>
                <a class="help-lnk" href="javascript:void(0)" title="Thứ tự hiển thị có thể để trống, giá trị mặc định bằng 0" ><span class="aui-icon aui-icon-small aui-iconfont-help">?</span></a>
                <br />
                <asp:RangeValidator ID="rvtxtDisplayOrder" runat="server" 
                        ErrorMessage="Thứ tự hiển thị hợp lệ trong khoảng 0 -  30000."
                        ControlToValidate="txtDisplayOrder"
                        MinimumValue="0"
                        MaximumValue="30000"
                        ValidationGroup="G1"
                        ForeColor="Red"
                        SetFocusOnError="true"
                        Type="Integer">
                </asp:RangeValidator>  
            </td>
        </tr>
    </table>
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter" runat="server" visible="false">
        <tr>
            <td style="width:50%">
            <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
                <tr runat="server" visible="false">
                    <td style="width:110px"><asp:Label ID="lblSite" runat="server" Text="Site:" meta:resourcekey="lblSite"></asp:Label>
                        </td>
                    <td>
                        <asp:DropDownList ID="ddlSite" runat="server" DataTextField="SiteDesc" 
                            DataValueField="SiteId" Width="40%" 
                            AutoPostBack="True" onselectedindexchanged="ddlSite_SelectedIndexChanged">
                        </asp:DropDownList>
                        <div style="text-align: right; float: right; width: 60%;" >
                        <asp:Label ID="lblDataType" runat="server" Text="Loại dữ liệu:" meta:resourcekey="lblDataType"></asp:Label>
                        <asp:DropDownList ID="ddlDataType" runat="server" style=" margin-right: 2%"
                            DataTextField="DataTypeDesc" DataValueField="DataTypeId" 
                            Width="66%" AutoPostBack="True" 
                                onselectedindexchanged="ddlDataType_SelectedIndexChanged">
                        </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <tr id="trLanguage" runat="server" visible="false">
                    <td style="width:110px"><asp:Label ID="lblLanguage" runat="server" Text="Ngôn ngữ:" meta:resourcekey="lblLanguage"></asp:Label>
                        </td>
                    <td>
                        <asp:DropDownList ID="ddlLanguage" runat="server" DataTextField="LanguageDesc" 
                            DataValueField="LanguageId" Width="40%" 
                            AutoPostBack="True" onselectedindexchanged="ddlLanguage_SelectedIndexChanged">
                        </asp:DropDownList>
                        <div style="text-align: right; float: right; width: 60%;" >
                        <asp:Label ID="lblAppType" runat="server" Text="Loại ứng dụng:" meta:resourcekey="lblAppType"></asp:Label>
                        <asp:DropDownList ID="ddlAppType" runat="server" style=" margin-right: 2%"
                            DataTextField="ApplicationTypeDesc" DataValueField="ApplicationTypeId" 
                            Width="66%" AutoPostBack="True" 
                            onselectedindexchanged="ddlAppType_SelectedIndexChanged">
                        </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <tr runat="server" visible="false">
                <td style="width:110px"><asp:Label ID="lblArticleType" runat="server" Text="Loại:" meta:resourcekey="lblArticleType"></asp:Label>
                        </td>
                    <td>
                        <asp:DropDownList ID="ddlArticleType" runat="server" DataTextField="ArticleTypeDesc" 
                            DataValueField="ArticleTypeId" Width="40%">
                        </asp:DropDownList>
                        <div style="text-align: right; float: right; width: 60%;" >
                        <asp:Label ID="lblIcon" runat="server" Text="Icon:" meta:resourcekey="lblIcon"></asp:Label>
                        <asp:DropDownList ID="ddlIconStatus" runat="server" style=" margin-right: 2%"
                            DataTextField="IconStatusDesc" DataValueField="IconStatusId" 
                            Width="66%">
                        </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <tr id="trCategory"  runat="server" visible="false">
                    <td><asp:Label ID="lblCategory" runat="server" Text="Chuyên mục chính:" meta:resourcekey="lblCategory"></asp:Label>
                        </td>
                    <td><asp:DropDownList ID="ddlCategory" runat="server" Enabled="false"
                            DataTextField="CategoryDesc" DataValueField="CategoryId" 
                            Width="99%" onselectedindexchanged="ddlCategory_SelectedIndexChanged" 
                            AutoPostBack="True"></asp:DropDownList>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        </td>
                    <td>
                        
                        </td>
                </tr>
                <tr id="trSummary" runat="server">
                    <td>
                        
                    </td>
                    <td>
                        
                    </td>
                </tr>
            </table>
            </td>
            
            <td style="vertical-align:top;">
                <asp:Label ID="lblCategoryOther" runat="server" visible="false" Text="Chuyên mục khác:" meta:resourcekey="lblCategoryOther" ></asp:Label>
                <div id="divCategory" runat="server" visible="false" style="width:auto; height:150px; overflow:auto; border: 1px solid #BEC7D2;">
                    <asp:CheckBoxList ID="chkCategory" DataTextField="CategoryDesc" DataValueField="CategoryId" RepeatDirection="Vertical" runat="server">
                    </asp:CheckBoxList>
                </div>
                <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
                    <tr id="trSourceUrl" runat="server" visible="false">
                        <td width="80px">
                            <asp:Label ID="lblUrl" runat="server" Text="Url gốc:" meta:resourcekey="lblUrl"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUrl" runat="server" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trArticleUrl" runat="server">
                        <td>
                            
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr id="trSource" runat="server" visible="false">
                        <td>
                            <asp:Label ID="lblSource" runat="server" Text="Nguồn:" meta:resourcekey="lblSources"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSource" runat="server" Width="99%" CssClass="uiselect"
                                DataTextField="DataSourceDesc" DataValueField="DataSourceId"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr runat="server" visible="false">
                    <td>
                        <asp:Label ID="lblReviewStatus" runat="server" Text="Trạng thái:" meta:resourcekey="lblReviewStatus"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlReviewStatus" runat="server" Width="99%"
                            DataTextField="ReviewStatusDesc" DataValueField="ReviewStatusId"></asp:DropDownList>
                    </td>
                    </tr>
                    <tr id="trTime" runat="server">
                        <td>Đặt lịch h/thị:</td>
                        <td>
                            <asp:CheckBox ID="chkIsVerify" runat="server" visible="false" Text="Đã kiểm tra thông tin" />
                        </td>
                    </tr>
                    <tr runat="server" visible="false">
                        <td>Hiển thị:</td>
                        <td>
                            <asp:CheckBox ID="chkShowTop" runat="server" Text="Top" />&nbsp;
                            <asp:CheckBox ID="chkShowBottom" runat="server" Text="Bottom" />&nbsp;
                            <asp:CheckBox ID="chkShowWeb" runat="server" Text="Home web" Checked="true" />&nbsp;
                            <asp:CheckBox ID="chkShowWap" runat="server" Text="Home wap" />&nbsp;
                            <asp:CheckBox ID="chkShowApp" runat="server" Text="Home app" />&nbsp;
                            
                                                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </div>
    <div style="background-color: #FFFFFF; bottom: 0px;  padding: 8px;  position: fixed;   right: 1px;   text-align: right;  width: 100%;">
        <asp:CheckBox ID="cblAddOther" runat="server" Text="Thêm tiếp dữ liệu khác" Checked="false" ToolTip="Khi tick chọn sẽ xóa form hiện tại để thêm dữ liệu khác"  />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<asp:LinkButton ID="btnSave" ValidationGroup="G1" runat="server" CssClass="savebutom" meta:resourcekey="btnSave" Text=" Lưu " onclick="btnSave_Click">
</asp:LinkButton></
    </div>
    
</asp:Content>

