<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="Lawers.aspx.cs" Inherits="Admin_Pages_lawsdocs_Lawers" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/admin/UserControls/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="uc1" %>
<%@ Import Namespace="ICSoft.HelperLib" %>
<asp:Content ID="contentAccountType" runat="server" ContentPlaceHolderID="m_contentBody">
    <script type="text/javascript">
        var cdialog = $('<div id="divEdit"></div>');
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);
            $("#<%= txtDateFrom.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
            $("#<%= txtDateTo.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });

            $('a.popup').live('click', function (e) {
                var page = $(this).attr("href");
                var pwidth = $(this).attr("data-width");
                var pheight = $(this).attr("data-height");
                if (!pwidth)
                    pwidth = 1100;
                if (!pheight)
                    pheight = 550;
                cdialog
                .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="100%" height="100%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: pheight,
                    width: pwidth,
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
            $("#<%= txtDateFrom.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
            $("#<%= txtDateTo.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        }
        function submitButton(event) {
            if (event.which == 13) {
                $('#<%= btnSearch.ClientID %>').click();
            }
        }
    </script>
        <table cellpadding="3" cellspacing="3" class="tableBorder" style="width: 98%;" >
            <tr>
            <td>
            <table cellpadding="3" cellspacing="3" style="width: 100%">
                <tr>
                <td style="width:90px">Nhóm luật sư</td>                  
                    <td style="width:260px">
                        <asp:DropDownList ID="ddlLawerGroups" runat="server" AutoPostBack="True" 
                            CssClass="userselect" DataTextField="LawerGroupName" 
                            DataValueField="LawerGroupId" 
                            onselectedindexchanged="ddlReviewStatus_SelectedIndexChanged" Width="250px">
                        </asp:DropDownList>
                    </td>
                    <td style="width:60px; white-space:nowrap;">
                        <asp:Label ID="lblKeyword" runat="server" Text="Từ khóa:" meta:resourcekey="lblKeyword"></asp:Label>
                    </td>
                    <td style="width:260px">
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="tukhoatimekiem" Width="250px"></asp:TextBox>&nbsp;&nbsp;
                        
                    </td>
                
                    <td style="width:60px; white-space:nowrap;">
                        <asp:Label ID="lblDateFrom" runat="server" Text="Từ ngày:" meta:resourcekey="lblDateFrom"></asp:Label>
                    </td>
                    <td >
                        <asp:TextBox ID="txtDateFrom" runat="server" CssClass="tukhoatimekiem" Width="100px"></asp:TextBox>
                        <asp:Label ID="lblDateTo" runat="server" Text="đến:" meta:resourcekey="lblDateTo"></asp:Label>
                        <asp:TextBox ID="txtDateTo" runat="server" CssClass="tukhoatimekiem" Width="100px"></asp:TextBox>
                        </td>
                
            </tr>
            <tr>
                <td style="width:60px">Lĩnh vực</td>                  
                    <td style="width:260px">
                        <asp:DropDownList ID="ddlFields" runat="server" AutoPostBack="True" 
                            CssClass="userselect" DataTextField="FieldName" 
                            DataValueField="FieldId" 
                            onselectedindexchanged="ddlReviewStatus_SelectedIndexChanged" Width="250px">
                        </asp:DropDownList>
                    </td>
                    <td style="width:60px">
                        <asp:Label ID="lblOrderBy" runat="server" Text="Sắp xếp:" meta:resourcekey="lblOrderBy"></asp:Label>
                    </td>
                    <td  style="width:260px">
                        <asp:DropDownList ID="ddlOrderBy" runat="server" DataTextField="OrderByDesc" 
                            DataValueField="OrderBy" Width="250px" CssClass="userselect" 
                            AutoPostBack="True" 
                            onselectedindexchanged="ddlOrderBy_SelectedIndexChanged"></asp:DropDownList>
                    </td>  
                    <td style="width:60px">Trạng thái</td>                  
                    <td >
                        <asp:DropDownList ID="ddlReviewStatus" runat="server" AutoPostBack="True" 
                            CssClass="userselect" DataTextField="ReviewStatusDesc" 
                            DataValueField="ReviewStatusId" 
                            onselectedindexchanged="ddlReviewStatus_SelectedIndexChanged" Width="250px">
                        </asp:DropDownList>&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnSearch" runat="server" CssClass="timkiembutom" 
                            Text="Tìm kiếm" meta:resourcekey="btnSearch" onclick="btnSearch_Click">
                                </asp:Button><asp:LinkButton ID="btnRefresh" runat="server" CssClass="timkiembutom hidden-btn-dqs" Text="Refresh" onclick="btnRefresh_Click"></asp:LinkButton>	
                    </td>
                
                </tr>
            </table>
            <div class="clear5px" ></div>
        	<div class="vien"></div>
        	<div class="clear5px"></div>  
            <div class="khungchucnang">
            <div class="chucnangleft">
        		<span class="tieudetongcong">
                    <asp:Label ID="lblTotalText" runat="server" Text="Tổng cộng:" meta:resourcekey="lblTotalText"></asp:Label>
                </span>
                <asp:Label ID="lblTong" runat="server" Text="" CssClass="tieudetongcong2"></asp:Label> 
        	</div>
        	<div class="chucnangright">
                <a  href="LawersEdit.aspx" title="Thêm mới luật sư " class="themmoi popup" > 
                    <asp:Label ID="lblAddNew" runat="server" Text="Thêm mới" meta:resourcekey="lblAddNew"></asp:Label>
                </a>
                <asp:LinkButton ID="lbUnCheck" runat="server" CssClass="boduyet" 
                    Text="Bỏ duyệt" meta:resourcekey="lbUnCheck" onclick="lbUnCheck_Click"> 
                </asp:LinkButton>
                <asp:LinkButton ID="lbReview" runat="server" CssClass="duyettin" Text="Duyệt" 
                    meta:resourcekey="lbReview" onclick="lbReview_Click"> 
                </asp:LinkButton>	
                <asp:LinkButton ID="lbDelete" runat="server" CssClass="xoatin" Text="Xóa"  OnClientClick="return confirm('Bạn có chắc muốn xóa toàn bộ dữ liệu đã chọn?')"
                    meta:resourcekey="lbDelete" onclick="lbDelete_Click">
                </asp:LinkButton>				
        		
        	</div>
            <div style="text-align:left; width:200px; float:right">
            </div>
        	<div class="clear5px"></div>
            <div class="contenbangdulieu">
                <asp:GridView ID="m_grid" DataKeyNames="LawerID" runat="server" ShowHeaderWhenEmpty="True"
                    AutoGenerateColumns="False" CssClass="filter-table" OnRowDeleting="m_grid_RowDeleting"  OnRowDataBound = "m_grid_OnRowDataBound"
                    Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" PageSize="50" >
                    <HeaderStyle CssClass="trbangdulieutieude" />
                    <FooterStyle CssClass="grid_foot" />
                    <RowStyle CssClass="trbangdulieutieudenoidung" />
                    <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                    <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />
                    <EditRowStyle CssClass="trbangdulieutieudenoidung" />
                    <PagerStyle CssClass="grid_page" />
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                            <asp:Label ID="lbNo" runat="server" Text='<%# Container.DataItemIndex + (CustomPaging.PageIndex - 1) * m_grid.PageSize + 1%>' 
                             ToolTip ='<%# Eval("LawerID").ToString() %>' >                                    
                            </asp:Label>
                            </ItemTemplate>      
                            <ItemStyle Width="3%" HorizontalAlign="Center" />            
                        </asp:TemplateField>    
                         <asp:TemplateField HeaderText="Họ tên">  
                            <ItemTemplate> 
                                <a href='LawersEdit.aspx?id=<%# Eval("LawerID") %>' class="popup" style="font-weight:bold;color:black;"><asp:Literal ID="ltFullName" runat="server" EnableViewState="false" Text='<%# Eval("FullName") %>'></asp:Literal></a>
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Left" Width="12%" />
                        </asp:TemplateField> 
                         <asp:TemplateField HeaderText="Ảnh">  
                            <ItemTemplate> 
                                <asp:Label ID="lblImage" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Center" Width="5%"   />
                        </asp:TemplateField> 
                         <asp:TemplateField HeaderText="Địa chỉ">  
                            <ItemTemplate> 
                                <table style="width:100%;">
                                    <tr>
                                        <td class="properties" style="width:60px">Địa chỉ</td>
                                        <td>
                                            <asp:Literal ID="ltAddress" runat="server" EnableViewState="false" Text='<%# Eval("Address") %>'></asp:Literal> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="properties" >Điện thoại</td>
                                        <td>
                                            <asp:Literal ID="ltPhoneNumber" runat="server" EnableViewState="false" Text='<%# Eval("PhoneNumber") %>'></asp:Literal> 
                                        </td>
                                    </tr>
                                     <tr>
                                        <td class="properties" >Di động</td>
                                        <td>
                                            <asp:Literal ID="ltMobile" runat="server" EnableViewState="false" Text='<%# Eval("Mobile") %>'></asp:Literal>
                                        </td>
                                    </tr>
                                </table>                                
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Left" Width="25%"  />
                        </asp:TemplateField> 
                         <asp:TemplateField HeaderText="Văn phòng">  
                            <ItemTemplate> 
                                <table style="width:100%;">
                                    <tr>
                                        <td class="properties"  style="width:60px">Văn phòng</td>
                                        <td>
                                            <asp:Literal ID="ltLawOfficeName" runat="server" EnableViewState="false" Text='<%# Eval("LawOfficeName") %>'></asp:Literal> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="properties" >Email</td>
                                        <td>
                                            <asp:Literal ID="ltEmail" runat="server" EnableViewState="false" Text='<%# Eval("Email") %>'></asp:Literal> 
                                        </td>
                                    </tr>
                                     <tr>
                                        <td class="properties" >Website</td>
                                        <td>
                                            <asp:Literal ID="ltWebsite" runat="server" EnableViewState="false" Text='<%# Eval("Website") %>'></asp:Literal> 
                                        </td>
                                    </tr>
                                </table>        
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Left" Width="22%"  />
                        </asp:TemplateField> 
                        
                         <asp:TemplateField HeaderText="Thông tin khác">  
                            <ItemTemplate> 
                                <table style="width:100%;">
                                    <tr>
                                        <td class="properties"  style="width:80px">Kinh nghiệm</td>
                                        <td>
                                            <asp:Literal ID="ltExperience" runat="server" EnableViewState="false" Text='<%# Eval("Experience") %>'></asp:Literal> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="properties" >Học vấn</td>
                                        <td>
                                            <asp:Literal ID="ltEducation" runat="server" EnableViewState="false" Text='<%# Eval("Education") %>'></asp:Literal> 
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Left" Width="18%"  />
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Trạng thái">  
                            <ItemTemplate> 
                                <span class="xuatban<%# Eval("ReviewStatusId") %>" >
                                    <%# LanguageHelpers.IsCultureVietnamese() ? ICSoft.CMSLib.ReviewStatus.Static_Get(byte.Parse(Eval("ReviewStatusId").ToString()), l_ReviewStatus).ReviewStatusDesc : ICSoft.CMSLib.ReviewStatus.Static_Get(byte.Parse(Eval("ReviewStatusId").ToString()), l_ReviewStatus).ReviewStatusName %>
                                </span> 
                                <%--<asp:Literal ID="ltReviewStatusId" runat="server" EnableViewState="false" Text='<%# Eval("ReviewStatusId").ToString() %>'></asp:Literal> --%>
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Left" />
                        </asp:TemplateField>                       
                        <asp:TemplateField HeaderText="Sửa">
                            <ItemStyle HorizontalAlign="center"  Wrap="false" />
                            <HeaderStyle Width="6%" />
                            <ItemTemplate>
                                <a title="Sửa thông tin luật sư" href='LawersEdit.aspx?id=<%# Eval("LawerID") %>' class="iconadmsua popup" ></a>
                                <asp:LinkButton ID="lbtDelete" ToolTip="Xóa luật sư này" runat="server" CausesValidation="False" OnClientClick="return confirm('Bạn có chắc muốn xóa dữ liệu này?');"
                                    CommandName="Delete" Text="" CssClass="iconadmxoa" ></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemStyle HorizontalAlign="Center" Width="40px" />
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
          </td>
          </tr>
          </table>
 </asp:Content>



