<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" ValidateRequest="false"
AutoEventWireup="true" CodeFile="CustomersShareEdit.aspx.cs" Inherits="Admin_CustomersShareEdit" %>
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
        function SelectName() {
            popup = window.open("MediasSelect.aspx?txtDemo=m_contentBody_txtIcon&ImgDemo=m_contentBody_imgDemo", "Popup", "width=650,height=550,scrollbars=1");
            popup.focus();
            return false
        }
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
            <td><asp:Label ID="lblTitle" runat="server" Text="Tên:" ></asp:Label>
                <span class="icon-required"></span>
                <asp:RequiredFieldValidator ID="rfvTitle" runat="server" Display="Dynamic" 
                ErrorMessage="(*)" ValidationGroup="G1" 
                ControlToValidate="txtTitle" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:TextBox ID="txtTitle" runat="server" Width="98%"></asp:TextBox>
                <br /><asp:RequiredFieldValidator ID="rfvtxtTitle" runat="server" ErrorMessage="Tên bài viết" ForeColor="Red" ControlToValidate="txtTitle" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
            </td>
         </tr>
        <tr>
            <td>
                <asp:Label ID="lblArticleUrl" runat="server" Text="Địa chỉ:" ></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtUrl" runat="server" Width="98%"></asp:TextBox>
               <asp:TextBox ID="txtArticleUrl" runat="server" Width="98%" Visible="false"></asp:TextBox>                        
            </td>
         </tr>
        <tr>
            <td>
                <asp:Label ID="lblSummary" runat="server" Text="Nội dung:" ></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtSummaryPlain" runat="server" Height="120px" TextMode="MultiLine" Width="98%"></asp:TextBox>
                <CKEditor:CKEditorControl ID="txtSummary" runat="server" BasePath="~/Integrated/ckeditor/" Height="40px" Toolbar="Basic" Visible="false" Width="99%"></CKEditor:CKEditorControl>
            </td>
         </tr>
        
        <tr>
            <td>
                Ảnh
            </td>
            <td>
                <a title="Chọn ảnh" href="#" onclick="SelectName()">
                        <img alt="txtIcon" id="imgDemo" runat="server" class="ImageSelectPath1" src="../../../Images/transparent.png" style="width: 60px;height: 40px;border: 1px solid black;" /></a>
                        <asp:TextBox CssClass="HiddenInput" ID="txtIcon" runat="server" Text=""></asp:TextBox>
                <asp:CheckBox ID="cbDeleteImages" runat="server" Text="Xóa ảnh" ToolTip="Tick chọn vào ô này để xóa ảnh hiện tại" />
            </td>
         </tr>
        <tr runat="server" visible="false">
            <td>
                Đặt lịch hiển thị
            </td>
            <td>
                <asp:TextBox ID="txtDisplayStartTime" runat="server" Width="25%"></asp:TextBox>
                            &nbsp;Kết thúc:
                            <asp:TextBox ID="txtDisplayEndTime" runat="server" Width="25%"></asp:TextBox>
                            &nbsp;Thứ tự:
                            <asp:TextBox ID="txtDisplayOrder" runat="server" Width="25%"></asp:TextBox>&nbsp;
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
        <asp:LinkButton ID="btnBack" runat="server" CssClass="quaylai" 
                    Text="Quay lại" meta:resourcekey="btnBack" Visible="False"
            onclick="btnBack_Click" >
        </asp:LinkButton>
        
        <asp:CheckBox ID="cblAddOther" runat="server" Text="Thêm tiếp dữ liệu khác" Checked="false" ToolTip="Khi tick chọn sẽ xóa form hiện tại để thêm dữ liệu khác"  />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

        <asp:LinkButton ID="btnSave" runat="server" CssClass="luuvaquaylai" 
            Text="Lưu thông tin" meta:resourcekey="btnSave" ValidationGroup="G1" onclick="btnSave_Click">
        </asp:LinkButton>

        <asp:LinkButton ID="btnSaveAndNew" runat="server" CssClass="luuvathemmoi" 
                    Text="Lưu và thêm mới" meta:resourcekey="btnSaveAndNew" Visible="False"
            onclick="btnSaveAndNew_Click"  >
        </asp:LinkButton>
    </div>
    
</asp:Content>

