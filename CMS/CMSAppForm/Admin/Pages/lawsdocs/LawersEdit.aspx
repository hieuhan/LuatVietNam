<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="LawersEdit.aspx.cs" Inherits="Admin_Pages_lawsdocs_LawersEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <script type="text/javascript">
        var popup;
        function SelectName() {
            popup = window.open("../articles/MediasSelect.aspx?txtDemo=m_contentBody_txtIcon&ImgDemo=m_contentBody_imgDemo", "Popup", "width=650,height=550,scrollbars=1");
            popup.focus();
            return false
        }
        function SelectName1() {
            popup = window.open("../articles/MediasSelect.aspx?txtDemo=m_contentBody_txtIcon1&ImgDemo=m_contentBody_imgDemo1", "Popup", "width=650,height=550,scrollbars=1");
            popup.focus();
            return false
        }
        function InitializeRequest(sender, args) {

        }
        function EndRequest(sender, args) {
            
            $(".uiselect").chosen({ width: "100%", no_results_text: "Không có dữ liệu " });
            BindControls();
        }
        function split(val) {
            return val.split(/,\s*/);
        }
        function extractLast(term) {
            return split(term).pop();
        }

         $(document).ready(function () {
            
            if (typeof CKEDITOR == 'undefined') {
            
            }
            else {
                //var editor = CKEDITOR.replace('<%= txContent.ClientID %>');
               var editor = CKEDITOR.replace('<%= txContent.ClientID %>', {
                    filebrowserBrowseUrl: '../Articles/MediasSelect.aspx?SetMediaDomain=1',
                    filebrowserUploadUrl: '/uploader/upload.php?type=Files',
                    toolbar: "Profile"
                });
                //CKFinder.setupCKEditor(editor, '<%= ICSoft.CMSLib.CmsConstants.ROOT_PATH %>Integrated/ckfinder/');
            }
        });
        
    </script>   
    <div style="text-align: center; padding:5px;">
        <asp:Label ID="lblMessage" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label></div>
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter" >
        
        <tr>
            <td>
               Họ tên:
                <span class="icon-required"></span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="G1" runat="server" ErrorMessage="(*)" ControlToValidate="txFullName" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:TextBox ID="txFullName" runat="server" CssClass="tukhoatimekiem" Width="97%" ></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Bạn cần nhập vào Họ tên" ForeColor="Red" ControlToValidate="txFullName" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
            </td>
        </tr> 
        <tr>
            <td><asp:Label ID="lblImage" runat="server" Text="Ảnh minh họa:" meta:resourcekey="lblImage"></asp:Label>
                &nbsp;</td>
            <td>
                <a title="Chọn ảnh" href="#" onclick="SelectName()">
                <img alt="txtIcon" id="imgDemo" runat="server" class="ImageSelectPath1" src="../../../Images/transparent.png" style="width: 60px;height: 40px;border: 1px solid black;" /></a>
                <asp:TextBox CssClass="HiddenInput" ID="txtIcon" runat="server" Text=""></asp:TextBox>
                
                &nbsp;
                <asp:CheckBox ID="cbDeleteImages" runat="server" Text="Xóa ảnh" ToolTip="Tick chọn để xóa ảnh đại diện" />
            </td>
        </tr> 
        <tr>
            <td>
               Địa chỉ:
                <span class="icon-required"></span>
            </td>
            <td>
                <asp:TextBox ID="txAddress" runat="server" CssClass="tukhoatimekiem" Width="97%" ></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Bạn cần nhập vào Địa chỉ" ForeColor="Red" ControlToValidate="txAddress" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
            </td>
        </tr> 
        <tr>
            <td style="width:110px">
                <asp:Label ID="LabelGroups" runat="server" Text="Nhóm luật sư:" meta:resourcekey="LabelGroups"></asp:Label>
            </td>
            <td>
                <table width="100%">
                    <tr>
                        <td style="width: 160px">
                            <asp:DropDownList ID="ddlLawerGroups" runat="server" DataTextField="LawerGroupName" 
                                DataValueField="LawerGroupId" Width="130px"   CssClass="tukhoatimekiem">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 80px">
                             <asp:Label ID="LabelProvince" runat="server" Text="Tỉnh/T.Phố" meta:resourcekey="LabelProvince"></asp:Label>
                        </td>
                        <td style="width: 160px">
                            <asp:DropDownList ID="ddlProvince" runat="server" DataTextField="ProvinceDesc" 
                                DataValueField="ProvinceId" Width="130px"   CssClass="tukhoatimekiem"
                                AutoPostBack="True" onselectedindexchanged="ddlProvince_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 80px">
                             <asp:Label ID="LabelDistrict" runat="server" Text="Quận/Huyện" meta:resourcekey="lblDistrict"></asp:Label>
                        </td>
                        <td  style="width: 160px">
                            <asp:DropDownList ID="ddlDistrict" runat="server" DataTextField="DistrictDesc" 
                                DataValueField="DistrictId" Width="130px"   CssClass="tukhoatimekiem"
                                AutoPostBack="True" onselectedindexchanged="ddlDistrict_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                         <td style="width: 80px">
                             <asp:Label ID="LabelWard" runat="server" Text="Phường/xã:" meta:resourcekey="lblWard"></asp:Label>
                        </td>
                         <td>
                             <asp:DropDownList ID="ddlWard" runat="server" DataTextField="WardDesc" 
                                DataValueField="WardId" Width="130px"  CssClass="tukhoatimekiem">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
               Điện thoại:
            </td>
            <td>
                <table width="100%">
                    <tr>
                        <td style="width: 160px">
                            <asp:TextBox ID="txPhoneNumber" runat="server" CssClass="tukhoatimekiem" Width="120px" ></asp:TextBox>
                        </td>
                        <td style="width: 80px">
                             Di động: 
                        </td>
                        <td style="width: 160px">
                            <asp:TextBox ID="txMobile" runat="server" CssClass="tukhoatimekiem" Width="120px" ></asp:TextBox>
                        </td>
                         <td style="width: 80px">
                             Email:
                        </td>
                         <td>
                             <asp:TextBox ID="txEmail" runat="server" CssClass="tukhoatimekiem" Width="120px" ></asp:TextBox>
                             </td>
                        <td style="width:80px">Website:</td>
                        <td>
                            <asp:TextBox ID="txWebsite" runat="server" CssClass="tukhoatimekiem" Width="120px" ></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>         
        <tr>
            <td>
               Văn phòng:
            </td>
            <td>
                <table width="100%">
                    <tr>
                        <td style="width: 160px">
                            <asp:TextBox ID="txLawOfficeName" runat="server" CssClass="tukhoatimekiem" Width="120px" ></asp:TextBox>
                        </td>
                        <td style="width: 80px">
                            Kinh nghiệm:
                        </td>
                        <td style="width: 160px">
                            <asp:TextBox ID="txExperience" runat="server" CssClass="tukhoatimekiem" Width="120px" ></asp:TextBox>
                        </td>
                         <td style="width: 80px">
                             Học vấn:                
                        </td>
                         <td>
                            <asp:TextBox ID="txEducation" runat="server" CssClass="tukhoatimekiem" Width="120px" ></asp:TextBox>
                             </td>
                        <td style="width:80px"><asp:Label ID="lblReviewStatus" runat="server" Text="Trạng thái:" meta:resourcekey="lblReviewStatus"></asp:Label></td><td>
                             <asp:DropDownList ID="ddlReviewStatus" runat="server" Width="120px"
                    DataTextField="ReviewStatusDesc" DataValueField="ReviewStatusId"  CssClass="tukhoatimekiem"></asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
                        <td style="width: 100px;">
                             <asp:Label ID="LabelFields" runat="server" Text="Lĩnh vực hoạt động" meta:resourcekey="LabelFields"></asp:Label>
                        </td>
            <td style="width: 160px">
                            <asp:DropDownList ID="ddlFields" runat="server" DataTextField="FieldName" 
                                DataValueField="FieldId" Width="130px"   CssClass="tukhoatimekiem">
                            </asp:DropDownList>
                        </td>
        </tr>
        <tr>
            <td style="width:100px; vertical-align:top">
                <asp:Label ID="lblContent" runat="server" Text="Nội dung:" meta:resourcekey="lblContent"></asp:Label>
            </td>
            <td>
                <CKEditor:CKEditorControl ID="txContent" Toolbar="Profile" Height="180px" Width="88%" CssClass="tukhoatimekiem" runat="server" BasePath="~/Integrated/ckeditor/" ></CKEditor:CKEditorControl>            
            </td>
        </tr> 
       
    </table>
    <div style="background-color: #FFFFFF; bottom: 0px;  padding: 8px;  position: fixed;   right: 1px;   text-align: right;  width: 100%;">
        <asp:CheckBox ID="cblAddOther" runat="server" Text="Thêm tiếp dữ liệu khác" Checked="false" ToolTip="Khi tick chọn sẽ xóa form hiện tại để thêm dữ liệu khác"  />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="btnSave" ValidationGroup="G1" runat="server" CssClass="savebutom"  Text=" Lưu "
            onclick="btnSave_Click">
        </asp:LinkButton>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="lbtSaveAndNew" ValidationGroup="G1" runat="server" Visible="false" CssClass="savebutom"  Text="Lưu và thêm mới" OnClick="lbtSaveAndNew_Click" 
           >
        </asp:LinkButton>
    </div>
</asp:Content>

