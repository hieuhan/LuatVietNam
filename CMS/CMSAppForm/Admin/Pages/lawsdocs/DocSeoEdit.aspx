<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="DocSeoEdit.aspx.cs" Inherits="Admin_Pages_lawsdocs_DocSeoEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <script src="../../Scripts/jquery.plugin.min.js"></script>
    <script src="../../Scripts/jquery.maxlength.js"></script>
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
            
            $('input[id*="txtMetaTitle"]').maxlength({ feedbackText: 'Tổng số ký tự {c} (Tối đa {m}, khuyến nghị: 50-60)', overflowText: 'Tổng số ký tự {c} (Tối đa {m}, khuyến nghị: 50-60)', max: 80, truncate: false });
            $('textarea[id*="txtMetaDesc"]').maxlength({ feedbackText: 'Tổng số ký tự {c}  (Tối đa {m}, khuyến nghị: dưới 200)', overflowText: 'Tổng số ký tự {c} (Tối đa {m}, khuyến nghị: dưới 200)', max: 300, truncate: false });
            $('input[id*="txtH1Tag"]').maxlength({ feedbackText: 'Tổng số ký tự {c}  (Tối đa {m}, khuyến nghị: 50-60)', overflowText: 'Tổng số ký tự {c} (Tối đa {m}, khuyến nghị: 50-60)', max: 80, truncate: false });
            
        });
    </script>   
    <div style="text-align: center; padding:5px;">
        <asp:Label ID="lblMessage" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label></div>
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter; padding-bottom: 30px; margin-bottom: 30px" >
        
        <tr>
            <td>
               Thẻ H1:
                </td>
            <td>
                <asp:TextBox ID="txtH1Tag" runat="server"  CssClass="tukhoatimekiem" Width="97%" ></asp:TextBox>
                </td>
        </tr> 
        <tr>
            <td style="width:120px;">
               Tiêu đề Seo:
                </td>
            <td>
                <asp:TextBox ID="txtMetaTitle" runat="server" CssClass="tukhoatimekiem" Width="97%" ></asp:TextBox><span style="color:red"></span>
                <br />
                 </td>
        </tr> 
        <tr>
            <td>
               Mô tả Seo:
                 </td>
            <td>
                <asp:TextBox ID="txtMetaDesc" runat="server" TextMode="MultiLine" Rows ="3" CssClass="tukhoatimekiem" Width="97%" ></asp:TextBox>
                 </td>
        </tr> 
        <tr>
            <td>
               Từ khóa Seo:
                </td>
            <td>
                <asp:TextBox ID="txtMetaKeyword" runat="server" TextMode="MultiLine" Rows ="3" CssClass="tukhoatimekiem" Width="97%" ></asp:TextBox>
                </td>
        </tr> 
        <tr>
            <td>
               Url:
                </td>
            <td>
                <asp:TextBox ID="txturl" runat="server"  CssClass="tukhoatimekiem" Width="97%" ></asp:TextBox>
                </td>
        </tr> 
        <tr>
            <td>
               Seo Header:
               </td>
            <td>
                <asp:TextBox ID="txtSeoHeader" runat="server" TextMode="MultiLine" Rows ="2" CssClass="tukhoatimekiem" Width="97%" ></asp:TextBox>
               </td>
        </tr> 
        <tr>
            <td>
               Seo Footer:
                </td>
            <td>
                <asp:TextBox ID="txtSeoFooter" runat="server" TextMode="MultiLine" Rows ="2" CssClass="tukhoatimekiem" Width="97%" ></asp:TextBox>
                 </td>
        </tr> 
        <tr>
            <td>
               Social Title:
                </td>
            <td>
                <asp:TextBox ID="txtSocialTitle" runat="server" CssClass="tukhoatimekiem" Width="97%" ></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td>
               Social Desc:
               </td>
            <td>
                <asp:TextBox ID="txtSocialDesc" runat="server" TextMode="MultiLine" Rows ="2" CssClass="tukhoatimekiem" Width="97%" ></asp:TextBox>
                </td>
        </tr> 
        <tr>
            <td><asp:Label ID="lblImage" runat="server" Text="Social Image:" meta:resourcekey="lblImage"></asp:Label>
                &nbsp;</td>
            <td>
                <a title="Chọn ảnh" href="#" onclick="SelectName()">
                <img alt="txtIcon" id="imgDemo" runat="server" class="ImageSelectPath1" src="../../../Images/transparent.png" style="width: 60px;height: 40px;border: 1px solid black;" /></a>
                <asp:TextBox CssClass="HiddenInput" ID="txtIcon" runat="server" Text=""></asp:TextBox>
                
                &nbsp;
                <asp:CheckBox ID="cbDeleteImages" runat="server" Text="Xóa ảnh" ToolTip="Tick chọn để xóa ảnh" />
            </td>
        </tr> 
    </table>
    <div style="background-color: #FFFFFF; bottom: 0px;  padding: 8px;  position: fixed;   right: 1px;   text-align: right;  width: 100%;">
        
        <asp:LinkButton ID="btnSave" ValidationGroup="G1" runat="server" CssClass="savebutom"  Text=" Lưu "
            onclick="btnSave_Click">
        </asp:LinkButton>
    </div>
</asp:Content>

