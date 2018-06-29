<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="PagesEdit.aspx.cs" Inherits="Admin_PagesEdit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
<script type="text/javascript">
    $(document).ready(function () {
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(InitializeRequest);
        prm.add_endRequest(EndRequest);
        SetStartup();
        ChangePageType();
        $('.txtPageName')
        .focusout(function () {
            if ($('.txtMetaTitle').val() == '') {
                $('.txtMetaTitle').val($('.txtPageName').val());
            }
            if ($('.txtMetaDesc').val() == '') {
                $('.txtMetaDesc').val($('.txtPageName').val());
            }
            if ($('.txtMetaKeyword').val() == '') {
                $('.txtMetaKeyword').val($('.txtPageName').val());
            }
        });
    });
    function InitializeRequest(sender, args) {
    }

    function EndRequest(sender, args) {
        SetStartup();
        ChangePageType();
        $('.txtPageName')
        .focusout(function () {
            if ($('.txtMetaTitle').val() == '') {
                $('.txtMetaTitle').val($('.txtPageName').val());
            }
            if ($('.txtMetaDesc').val() == '') {
                $('.txtMetaDesc').val($('.txtPageName').val());
            }
            if ($('.txtMetaKeyword').val() == '') {
                $('.txtMetaKeyword').val($('.txtPageName').val());
            }
        });
    }
    function ChangeUrl() {
        $('#<%= txtUrl.ClientID %>').val($('select[id *= "ddlOtherLink"]').val());
    }
    function ChangePageType() {
        $('tr[id *= "PageType_"]').hide();
        var PageTypeId = $('select[id *= "ddlPageTypeId"]').val();
        if (PageTypeId == "1" || PageTypeId == "2") {
            $("#PageType_1").show();
        }
        else if (PageTypeId == "11" || PageTypeId == "12") {
            $("#PageType_11").show();
        }
        else if (PageTypeId == "3") {
            $("#PageType_3").show();
        }
    }
    
    </script>
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td valign="top" style="width: 50%">
                <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">                               
                    <tr>
                        <td>
                            <asp:Label ID="lblParentPage" runat="server" Text="Trang cấp cha:" meta:resourcekey="lblParentPage"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlParentPage" runat="server"  CssClass="userselect" 
                                DataTextField="PageName" DataValueField="PageId"  Width="406px"></asp:DropDownList>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblPageName" runat="server" Text="Tên chuyên mục:" meta:resourcekey="lblPageName"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPageName" runat="server" Width="396px" CssClass="tukhoatimekiem txtPageName" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblPageType" runat="server" Text="Loại trang:" meta:resourcekey="lblPageType"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPageTypeId" runat="server" onchange="ChangePageType()"
                                DataTextField="PageTypeDesc" DataValueField="PageTypeId" 
                                Width="406px"  CssClass="userselect"  >
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSites" runat="server" Text="Sites:" meta:resourcekey="lblSites"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSites" runat="server" onchange="ChangePageType()"
                                DataTextField="SiteDesc" DataValueField="SiteId" 
                                Width="406px"  CssClass="userselect"  >
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="PageType_3" style="display: none" >
                        <td>
                            <asp:Label ID="lblUrl" runat="server" Text="Url:" meta:resourcekey="lblUrl"></asp:Label>
                        </td>
                        <td> 
                            <asp:DropDownList ID="ddlOtherLink" runat="server"  CssClass="userselect" onchange="ChangeUrl()" 
                                DataTextField="PageName" DataValueField="Url"  Width="197px"></asp:DropDownList>
                            <asp:TextBox ID="txtUrl" runat="server" Width="196px" CssClass="tukhoatimekiem" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="PageType_1" style="display: none" >
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Url:" meta:resourcekey="lblUrl"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server" Width="396px" CssClass="tukhoatimekiem input-filter" ></asp:TextBox>
                            <br />
                            <div style="width:404px; height:220px; overflow:auto; border: 1px solid #BEC7D2; margin-top: 5px" >
                            <asp:GridView ID="m_GridCategories" runat="server" AutoGenerateColumns="false" DataKeyNames="CategoryId" 
                            CssClass="jquery-filter-table" Width="100%"  BorderStyle="None" BorderWidth="0" ShowHeader="false"
                            OnRowDataBound = "m_GridCategories_OnRowDataBound" >
                                <HeaderStyle CssClass="trbangdulieutieude" />
                                <RowStyle CssClass="trbangdulieutieudenoidung" />
                                <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                                <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />             
                                <Columns>
                                <asp:TemplateField>
                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                    <HeaderStyle Width="40px" />
                                    <HeaderTemplate>
                                        <input type="checkbox" name="SelectAllCheckBox" onclick="SelectAll(this)">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkAction" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField > 
                                    <HeaderTemplate>                                
                                        <asp:Label ID="lblGridColumnName" runat="server" Text="Tên trang" meta:resourcekey="lblGridColumnName"></asp:Label>
                                    </HeaderTemplate> 
                                    <ItemTemplate> 
                                        <asp:Label ID="lbCateNameTree" runat="server" style="display:none"></asp:Label>                        
                                        <%# Eval("CategoryName") %> 
                                    </ItemTemplate>
                                    <ItemStyle  HorizontalAlign="Left" />
                                    <HeaderStyle />          
                                </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr id="PageType_11" style="display: none" >
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Url:" meta:resourcekey="lblUrl"></asp:Label>
                        </td>
                        <td>                            
                            <div style="width:404px; height:220px; overflow:auto; border: 1px solid #BEC7D2; margin-top: 5px" >
                            <asp:CheckBoxList ID="chkMeetingGroup" DataTextField="MeetingGroupName" DataValueField="MeetingGroupId" 
                             runat="server" RepeatColumns="1" >
                            </asp:CheckBoxList>
                            <asp:GridView ID="m_GridMeetingGroup" runat="server" AutoGenerateColumns="false" DataKeyNames="MeetingGroupId" 
                            CssClass="" Width= "100%" ShowHeader="false">
                                <HeaderStyle CssClass="trbangdulieutieude" />
                                <RowStyle CssClass="trbangdulieutieudenoidung" />
                                <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                                <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />    
                                <Columns>
                                <asp:TemplateField>
                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                    <HeaderStyle Width="40px" />
                                    <HeaderTemplate>
                                        <input type="checkbox" name="SelectAllCheckBox" onclick="SelectAll(this)">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkAction" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField > 
                                    <HeaderTemplate>                                
                                        <asp:Label ID="lblGridColumnName" runat="server" Text="Tên trang" meta:resourcekey="lblGridColumnName"></asp:Label>
                                    </HeaderTemplate> 
                                    <ItemTemplate> 
                                        <asp:Label ID="lbCateNameTree" runat="server" style="display:none"></asp:Label>                        
                                        <%# Eval("MeetingGroupName") %> 
                                    </ItemTemplate>
                                    <ItemStyle  HorizontalAlign="Left" />
                                    <HeaderStyle />          
                                </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblMenu" runat="server" Text="Chọn vào menu:" meta:resourcekey="lblMenu"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBoxList ID="chkMenu" Font-Size="11px" ForeColor="Blue" DataTextField="MenuName" 
                            DataValueField="MenuId" RepeatDirection="Horizontal" runat="server" RepeatColumns="3" >
                            </asp:CheckBoxList>
                            </td>
                    </tr>
                </table>
            </td>
            <td  valign="top">
                <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
                    <tr>
                        <td style="width:100px">
                            <asp:Label ID="lblLanguage" runat="server" Text="Ngôn ngữ:" meta:resourcekey="lblLanguage"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlLanguage" runat="server" DataTextField="LanguageDesc" AutoPostBack="true"
                                DataValueField="LanguageId" Width="150px" CssClass="userselect" 
                                onselectedindexchanged="ddlLanguage_SelectedIndexChanged1" >
                            </asp:DropDownList>
                            &nbsp;&nbsp;
                            <asp:Label ID="lblAppType" runat="server" Text="Loại ứng dụng:" meta:resourcekey="lblAppType"></asp:Label>
                            &nbsp;&nbsp;
                            <asp:DropDownList ID="ddlAppType" runat="server"  AutoPostBack="true"
                                DataTextField="ApplicationTypeDesc" DataValueField="ApplicationTypeId" 
                                Width="103px"  CssClass="userselect" 
                                onselectedindexchanged="ddlAppType_SelectedIndexChanged1" >
                            </asp:DropDownList>
                        </td>
                    </tr>         
                    <tr>
                        <td style="width: 100px" >
                            <asp:Label ID="lblMetaTitle" runat="server" Text="Tiêu đề trang:" meta:resourcekey="lblMetaTitle"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMetaTitle" runat="server" Width="350px" CssClass="tukhoatimekiem txtMetaTitle" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblMetaDesc" runat="server" Text="Mô tả trang:" meta:resourcekey="lblMetaDesc" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMetaDesc" runat="server" Width="354px" Height="66px" TextMode="MultiLine" CssClass="txtMetaDesc" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblMetaKeyword" runat="server" Text="Từ khóa trang:" meta:resourcekey="lblMetaKeyword"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMetaKeyword" runat="server" Width="350px" CssClass="tukhoatimekiem txtMetaKeyword" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblImage" runat="server" Text="Icon:" meta:resourcekey="lblImage"></asp:Label>
                            &nbsp;
                        </td>
                        <td>
                            <img alt="txtIcon" class="ImageSelectPath" src="../../../Images/transparent.png" width="60px" height="40px" />
                            <asp:TextBox CssClass="HiddenInput" ID="txtIcon" runat="server" Text=""></asp:TextBox>
                            &nbsp;
                            <asp:CheckBox ID="cbDeleteImages" runat="server" meta:resourcekey="cbDeleteImages" />
                        </td>
            
                    </tr>        
                    <tr>
                        <td>
                            <asp:Label ID="lblReviewStatus" runat="server" Text="Trạng thái:" meta:resourcekey="lblReviewStatus"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlReviewStatus" runat="server" CssClass="userselect" Width="130px"
                                DataTextField="ReviewStatusDesc" DataValueField="ReviewStatusId"></asp:DropDownList>
                            &nbsp;
                            <asp:Label ID="lblDisplayOrder" runat="server" Text="Thứ tự hiển thị:" meta:resourcekey="lblDisplayOrder"></asp:Label>
                            &nbsp;
                            <asp:TextBox ID="txtDisplayOrder" runat="server" Width="120px" CssClass="tukhoatimekiem" >1</asp:TextBox>
                        </td>
                    </tr>
                    
                </table>
            </td>
        </tr>
        
    </table>
    <div style="text-align:center">
        <asp:LinkButton ID="btnBack" runat="server" CssClass="quaylai" 
                    Text="Quay lại" meta:resourcekey="btnBack" 
            onclick="btnBack_Click" >
        </asp:LinkButton>
        <asp:LinkButton ID="btnSave" runat="server" CssClass="luuvaquaylai" 
                    Text="Lưu và quay lại" meta:resourcekey="btnSave"  onclick="btnSave_Click">
        </asp:LinkButton>
        <asp:LinkButton ID="btnSaveAndNew" runat="server" CssClass="luuvathemmoi" 
                    Text="Lưu và thêm mới" meta:resourcekey="btnSaveAndNew" 
            onclick="btnSaveAndNew_Click"  >
        </asp:LinkButton>
    </div>
</asp:Content>

