<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" ValidateRequest="false"
AutoEventWireup="true" CodeFile="ArticlesEdit.aspx.cs" Inherits="Admin_ArticlesEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <script src="../../Scripts/jquery.plugin.min.js"></script>
    <script src="../../Scripts/jquery.maxlength.js"></script>
    <%--<script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDRsEu3B3q8iP2WpMNoShkhGmIiP71maEI&libraries=places"></script>--%>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBX5UQeOWkH1f4Kj4K-vZMG7n-qB0g9YN0&libraries=places"></script>
    <script src="/Admin/Scripts/locationpicker.jquery.js"></script>
    <style>
        #sppointSeo {
            font-size: 20PX;
        }

        .btnDetailPointSeo:hover {
            cursor: pointer;
        }

        .divShowMesSeo {
            color: #000;
        }

        .sp_title {
            font-size: 18px;
            font-weight: bold;
        }

        .sp_url {
            color: green;
            font-size: 15px;
            font-weight: bold;
        }

        #sp_summary {
            float: left;
            font-size: 14px;
            max-width:750px;
        }
        .div_kf {
            font-size: 14px;
        }

        .div_kf2 {
            margin-left: 20px;
        }

        .c_Yes_K {
            color: green;
            font-weight: bold;
        }

        .c_No_K {
            color: red;
            font-weight: bold;
        }
    </style>
    <script type="text/javascript" src="<%= ICSoft.CMSLib.CmsConstants.ROOT_PATH %>Integrated/ckfinder/ckfinder.js"></script>
    <script type="text/javascript" src="<%=ICSoft.CMSLib.CmsConstants.PRJ_ROOT %>Scripts/custom_pointSEO.js"></script>
    <script type="text/javascript">
        var popup;
        function SelectName() {
            popup = window.open("MediasSelect.aspx?txtDemo=m_contentBody_txtIcon&ImgDemo=m_contentBody_imgDemo", "Popup", "width=650,height=550,scrollbars=1");
            popup.focus();
            return false
        }
        function SelectName1() {
            popup = window.open("MediasSelect.aspx?txtDemo=m_contentBody_txtIcon1&ImgDemo=m_contentBody_imgDemo1", "Popup", "width=650,height=550,scrollbars=1");
            popup.focus();
            return false
        }
        
        $(document).ready(function () {
            
            if (typeof CKEDITOR == 'undefined') {

            }
            else {
                //var editor = CKEDITOR.replace('<%= txtContent.ClientID %>');
                var editor = CKEDITOR.replace('<%= txtContent.ClientID %>', {
                    filebrowserBrowseUrl: 'MediasSelect.aspx?SetMediaDomain=1',
                    filebrowserUploadUrl: '/uploader/upload.php?type=Files'
                });
                //CKFinder.setupCKEditor(editor, '<%= ICSoft.CMSLib.CmsConstants.ROOT_PATH %>Integrated/ckfinder/');
            }

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);
            $("#<%= txtDisplayStartTime.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy 09:00:00' });
            $("#<%= txtDisplayEndTime.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy 09:00:00' });
            $(".uiselect").chosen({ width: "100%", no_results_text: "Không có dữ liệu " });
            BindControls();
        });
        //// Load Provinces
        $(function () {
            function split(val) {
                return val.split(/;\s*/);
            }
            function extractLast(term) {
                return split(term).pop();
            }

            $("#<%= txtProvices.ClientID %>")
			.bind("keydown", function (event) {
			    if (event.keyCode === $.ui.keyCode.TAB &&
						$(this).data("autocomplete").menu.active) {
			        event.preventDefault();
			    }
			})
			.autocomplete({
			    source: function (request, response) {
			        $.getJSON("../Articles/Provinces_GetNameByJson.aspx", {
			            term: extractLast(request.term)
			        }, response);
			    },
			    search: function () {
			        // custom minLength
			        var term = extractLast(this.value);
			        if (term.length < 0) {
			            return false;
			        }
			    },
			    minLength: 0,
			    focus: function () {
			        // prevent value inserted on focus
			        return false;
			    },
			    select: function (event, ui) {
			        var terms = split(this.value);
			        // remove the current input
			        terms.pop();
			        // add the selected item
			        terms.push(ui.item.value);
			        // add placeholder to get the comma-and-space at the end
			        terms.push("");
			        this.value = terms.join("; ");
			        return false;
			    }
			});
    });
        ////  Load Fields
        $(function () {
            function split(val) {
                return val.split(/;\s*/);
            }
            function extractLast(term) {
                return split(term).pop();
            }
            $("#<%= txtFields.ClientID %>")
			.bind("keydown", function (event) {
			    if (event.keyCode === $.ui.keyCode.TAB &&
						$(this).data("autocomplete").menu.active) {
			        event.preventDefault();
			    }
			})
			.autocomplete({
			    source: function (request, response) {
			        $.getJSON("../lawsdocs/Fields_GetNameByJson.aspx", {
			            term: extractLast(request.term)
			        }, response);
			    },
			    search: function () {
			        // custom minLength
			        var term = extractLast(this.value);
			        if (term.length < 0) {
			            return false;
			        }
			    },
			    minLength: 0,
			    focus: function () {
			        // prevent value inserted on focus
			        return false;
			    },
			    select: function (event, ui) {
			        var terms = split(this.value);
			        // remove the current input
			        terms.pop();
			        // add the selected item
			        terms.push(ui.item.value);
			        // add placeholder to get the comma-and-space at the end
			        terms.push("");
			        this.value = terms.join("; ");
			        return false;
			    }
			});
        });
        
        function InitializeRequest(sender, args) {

        }
        function EndRequest(sender, args) {
            $("#<%= txtDisplayStartTime.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy 09:00:00' });
            $("#<%= txtDisplayStartTime.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy 09:00:00' });
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

            $('input[id*="txtSEOTitle"]').maxlength({ feedbackText: 'Tổng số ký tự {c} (Tối đa {m}, khuyến nghị: 50-60)', overflowText: 'Tổng số ký tự {c} (Tối đa {m}, khuyến nghị: 50-60)', max: 80, truncate: false });
            $('textarea[id*="txtSEODesc"]').maxlength({ feedbackText: 'Tổng số ký tự {c}  (Tối đa {m}, khuyến nghị: dưới 200)', overflowText: 'Tổng số ký tự {c} (Tối đa {m}, khuyến nghị: dưới 200)', max: 300, truncate: false });
            //$('input[id*="txtH1Tag"]').maxlength({ feedbackText: 'Tổng số ký tự {c}  (Tối đa {m}), khuyến nghị: 50-60', overflowText: 'Tổng số ký tự <span style="color:red">{c}</span> (Tối đa {m}, khuyến nghị: 50-60)', max: 80, truncate: false });

        });
        function BindControls() {
            //auto complete Author 
            $("#<%= txtTag.ClientID %>")
			        .bind("keydown", function (event) {
			            if (event.keyCode === $.ui.keyCode.TAB &&
						        $(this).data("autocomplete").menu.active) {
			                event.preventDefault();
			            }
			        })
			        .autocomplete({
			            source: function (request, response) {
			                $.getJSON("TagsJson.aspx", {
			                    term: extractLast(request.term)
			                }, response);
			            },
			            search: function () {
			                // custom minLength
			                var term = extractLast(this.value);
			                if (term.length < 2) {
			                    return false;
			                }
			            },
			            minLength: 2,
			            focus: function () {
			                // prevent value inserted on focus
			                return false;
			            },
			            select: function (event, ui) {
			                var terms = split(this.value);
			                // remove the current input
			                terms.pop();
			                // add the selected item
			                terms.push(ui.item.value);
			                // add placeholder to get the comma-and-space at the end
			                terms.push("");
			                this.value = terms.join(",");
			                return false;
			            }
			        });
            //end auto complete
        }
        function checkItem(ItemValue) {
            $("input[type=checkbox][value=" + ItemValue + "]").prop("checked", true);
        }
    </script>
    <div  style=" width:auto; height:auto; overflow:auto;">
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td style="width:60%">
            <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
                <tr>
                    <td style="width:120px"><asp:Label ID="lblSite" runat="server" Text="Site:" meta:resourcekey="lblSite"></asp:Label>
                        </td>
                    <td>
                        <asp:DropDownList ID="ddlSite" runat="server" DataTextField="SiteDesc" 
                            DataValueField="SiteId" Width="40%" 
                            AutoPostBack="False" onselectedindexchanged="ddlSite_SelectedIndexChanged">
                        </asp:DropDownList>
                        <div style="text-align: right; float: right; width: 60%;" >
                        <asp:Label ID="lblDataType" runat="server" Text="Loại dữ liệu:" meta:resourcekey="lblDataType"></asp:Label>
                        <asp:DropDownList ID="ddlDataType" runat="server" style=" margin-right: 2%"
                            DataTextField="DataTypeDesc" DataValueField="DataTypeId" 
                            Width="66%" AutoPostBack="False" 
                                onselectedindexchanged="ddlDataType_SelectedIndexChanged">
                        </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <tr id="trLanguage" runat="server">
                    <td style="width:110px"><asp:Label ID="lblLanguage" runat="server" Text="Ngôn ngữ:" meta:resourcekey="lblLanguage"></asp:Label>
                        </td>
                    <td>
                        <asp:DropDownList ID="ddlLanguage" runat="server" DataTextField="LanguageDesc" 
                            DataValueField="LanguageId" Width="40%" 
                            AutoPostBack="False" onselectedindexchanged="ddlLanguage_SelectedIndexChanged">
                        </asp:DropDownList>
                        <div style="text-align: right; float: right; width: 60%;" >
                        <asp:Label ID="lblAppType" runat="server" Text="Loại ứng dụng:" meta:resourcekey="lblAppType"></asp:Label>
                        <asp:DropDownList ID="ddlAppType" runat="server" style=" margin-right: 2%"
                            DataTextField="ApplicationTypeDesc" DataValueField="ApplicationTypeId" 
                            Width="66%" AutoPostBack="False" 
                            onselectedindexchanged="ddlAppType_SelectedIndexChanged">
                        </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <tr>
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
                <tr id="trCategory" runat="server">
                    <td><asp:Label ID="lblCategory" runat="server" Text="Chuyên mục chính:" meta:resourcekey="lblCategory"></asp:Label><span class="icon-required"></span>
                        </td>
                    <td><asp:DropDownList ID="ddlCategory" runat="server" 
                            DataTextField="CategoryDesc" DataValueField="CategoryId" 
                            Width="99%" onselectedindexchanged="ddlCategory_SelectedIndexChanged" 
                            AutoPostBack="False"></asp:DropDownList>
                        &nbsp;<br />
                        <asp:Label ID="lbMsgCate" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><asp:Label ID="lblTitle" runat="server" Text="Tiêu đề:" meta:resourcekey="lblTitle"></asp:Label>
                        <span class="icon-required"></span>
<asp:RequiredFieldValidator ID="rfvtxtTitle1" ValidationGroup="G1" runat="server" ErrorMessage="(*)" 
ControlToValidate="txtTitle" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    <td>
                        <asp:TextBox ID="txtTitle" runat="server" Width="98%"></asp:TextBox><br />
<asp:RequiredFieldValidator ID="rfvtxtTitle" runat="server" ErrorMessage="Bạn cần nhập vào tiêu đề bài viết" ForeColor="Red" 
ControlToValidate="txtTitle" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
                        </td>
                </tr>
                <tr id="trSummary" runat="server">
                    <td>
                        <asp:Label ID="lblSummary" runat="server" Text="Mô tả:" meta:resourcekey="lblSummary"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSummary" runat="server" TextMode="MultiLine" Width="98%" Height="120px" Visible="false"></asp:TextBox>
                        <CKEditor:CKEditorControl ID="txtSummaryPlain" runat="server"
                                BasePath="~/Integrated/ckeditor/" Height="100px" Toolbar="Basic" Width="99%"></CKEditor:CKEditorControl>
                    </td>
                </tr>
                
                <tr>
                    <td><asp:Label ID="lblImage" runat="server" Text="Ảnh minh họa:" meta:resourcekey="lblImage"></asp:Label>
                        &nbsp;</td>
                    <td>
                        <a title="Chọn ảnh" href="#" onclick="SelectName()">
                        <img alt="txtIcon" id="imgDemo" runat="server" class="ImageSelectPath1" src="../../../Images/transparent.png" style="width: 60px;height: 40px;border: 1px solid black;" /></a>
                        <asp:TextBox CssClass="HiddenInput" ID="txtIcon" runat="server" Text=""></asp:TextBox>
                        <div style="display:inline" id="divImageCover" runat="server">
                        &nbsp;Ảnh cover:
                        <a title="Chọn ảnh" href="#" onclick="SelectName1()">
                        <img alt="txtIcon" id="imgDemo1" runat="server" class="ImageSelectPath1" src="../../../Images/transparent.png" style="width: 60px;height: 40px;border: 1px solid black;" /></a>
                        <asp:TextBox CssClass="HiddenInput" ID="txtIcon1" runat="server" Text=""></asp:TextBox>
                        </div>
                        &nbsp;
                        <asp:CheckBox ID="cbDeleteImages" runat="server" meta:resourcekey="cbDeleteImages" />
                    </td>
                </tr>
                <tr>
                    <td>Điểm SEO</td>
                    <td align="center">
                        <p class="styled">
                            <asp:HiddenField runat="server" ID="hdfSEOPoint" Value="0"/>
                            <span id="sppointSeo">0/100</span>
                            <span style="width: 290px; height: 19px; background: #f9f8f8; border: 1px solid #fff; float: left; margin-bottom: 10px;">
                                <span id="pProcessSeo" style="width: 0; height: 19px; background: #f00; float: left;"></span>
                            </span>
                        </p>
                        <button id="btnCheckPointSeo" style="width: 140px;" title="Cập nhật điểm SEO" onclick="getPointSeoArticle(); return false;">Cập nhật điểm SEO</button>
                        <span class="btnDetailPointSeo" onclick="$('#seoPointDetailPannel').toggle();" >Xem chi tiết điểm SEO</span>
                        <div class="divShowMesSeo" id="seoPointDetailPannel" style="display: none; width: 100%; float: left; text-align: left;">
                        </div>
                    </td>
                </tr>
            </table>
            </td>
            
            <td style="vertical-align:top;">
                <asp:Label ID="lblCategoryOther" runat="server" Text="Chuyên mục khác:" meta:resourcekey="lblCategoryOther" Visible="true"></asp:Label>
                <div id="divCategory" runat="server" style="width:auto; height:150px; overflow:auto; border: 1px solid #BEC7D2;">
                    <asp:CheckBoxList ID="chkCategory" DataTextField="CategoryDesc" DataValueField="CategoryId" RepeatDirection="Vertical" runat="server">
                    </asp:CheckBoxList>
                </div>
                <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
                    <tr id="trSourceUrl" runat="server">
                        <td width="80px">
                            <asp:Label ID="lblUrl" runat="server" Text="Url gốc:" meta:resourcekey="lblUrl"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUrl" runat="server" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trArticleUrl" runat="server">
                        <td>
                            <asp:Label ID="lblArticleUrl" runat="server" Text="Url bài viết:" meta:resourcekey="lblArticleUrl"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtArticleUrl" runat="server" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trSource" runat="server">
                        <td>
                            <asp:Label ID="lblSource" runat="server" Text="Nguồn:" meta:resourcekey="lblSources"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSource" runat="server" Width="99%" CssClass="uiselect"
                                DataTextField="DataSourceDesc" DataValueField="DataSourceId"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
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
                        <td><asp:TextBox ID="txtDisplayStartTime" runat="server" Width="35%"></asp:TextBox>&nbsp;Kết thúc:
                            <asp:TextBox ID="txtDisplayEndTime" runat="server" Width="35%"></asp:TextBox><a class="help-lnk" href="javascript:void(0)" title="Chọn hoặc nhập ngày tháng theo định dang dd/MM/yyyy hh:mm:ss" >
<span class="aui-icon aui-icon-small aui-iconfont-help">?</span></a><br />
<br />  
<asp:RegularExpressionValidator ID="rgltxtDisplayStartTime" runat="server" ForeColor="Red" ErrorMessage="Ngày hiển thị bạn nhập không hợp lệ" Display="Dynamic" ValidationExpression="\d{2}\/\d{2}\/\d{4}\s(\d{2}\:){2}\d{2}" ControlToValidate="txtDisplayStartTime"></asp:RegularExpressionValidator><br />                          
<asp:RegularExpressionValidator ID="rgltxtDisplayEndTime" runat="server" ForeColor="Red" ErrorMessage="Ngày kết thúc bạn nhập không hợp lệ" Display="Dynamic" ValidationExpression="\d{2}\/\d{2}\/\d{4}\s(\d{2}\:){2}\d{2}" ControlToValidate="txtDisplayEndTime"></asp:RegularExpressionValidator>                          
                        </td>
                    </tr>
                    <tr>
                        <td>Thứ tự:</td>
                        <td><asp:TextBox ID="txtDisplayOrder" runat="server" Width="35%"></asp:TextBox>
                            <asp:CheckBox ID="chkIsVerify" runat="server" Text="Đã kiểm tra thông tin" /><br />
                            <asp:RegularExpressionValidator ID="rglDisplayOrder" runat="server" ForeColor="Red" ErrorMessage="Chỉ chấp nhận kiểu số" Display="Dynamic" ValidationExpression="\d+" ControlToValidate="txtDisplayOrder"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
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
            <td style="vertical-align:top">
                <div class="clear5px"></div>
                <div class="clear5px"></div>
                <div style="text-align:Right">
                
                </div>
                </td>
        </tr>
    </table>
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td style="width:110px; vertical-align:top">
            
                </td>
            <td>
                
            </td>
        </tr>
        <tr>
            <td style="width:100px; vertical-align:top">
                <asp:Label ID="lblContent" runat="server" Text="Nội dung:" meta:resourcekey="lblContent"></asp:Label>
            </td>
            <td>
                <CKEditor:CKEditorControl ID="txtContent" Height="700px" Width="99%" runat="server" BasePath="~/Integrated/ckeditor/" ></CKEditor:CKEditorControl>            
            </td>
        </tr>
        <tr id="tr1" runat="server">
            <td>
                Internal Link:</td>
            <td>
                
                <asp:CheckBox runat="server" ID="cbInsertAutoInternalLinks" Checked="true" ForeColor="DarkGreen" Text="Tự động thêm Internal Links" />
            </td>
        </tr>
        
        <tr id="trTag" runat="server">
            <td>
                Tag:</td>
            <td>
                <asp:TextBox ID="txtTag" runat="server" Width="98%"></asp:TextBox></td>
        </tr>
        <tr id="trProduct" runat="server">
            <td>
                Mã SP:</td>
            <td>
                <asp:TextBox ID="txtArticleCode" runat="server" Width="10%"></asp:TextBox>&nbsp;&nbsp;
                Giá gốc: <asp:TextBox ID="txtOriginalPrice" runat="server" Width="10%"></asp:TextBox>&nbsp;&nbsp;
                Giá bán: <asp:TextBox ID="txtSalePrice" runat="server" Width="10%"></asp:TextBox>&nbsp;&nbsp;
                Giá liên hệ: <asp:TextBox ID="txtContactPrice" runat="server" Width="10%"></asp:TextBox>&nbsp;&nbsp;
                <asp:DropDownList ID="ddlCurrency" runat="server" DataTextField="CurrencyName" DataValueField="CurrencyId"></asp:DropDownList>&nbsp;&nbsp;
                Kho: <asp:DropDownList ID="ddlInventoryStatus" runat="server" DataTextField="InventoryStatusDesc" DataValueField="InventoryStatusId"></asp:DropDownList>
            </td>
        </tr>
        <%if (DataTypeId == 7 || DataTypeId == 1)
          { %>
        <tr runat="server">
            
            <td>
                Tỉnh thành:</td>
            <td>
                <span style="width:45%; float:left">
                <asp:TextBox ID="txtProvices" runat="server" Height="50px" TextMode="MultiLine"
                    Width="98%"></asp:TextBox>
                <br />
                <span class="node-help">Gõ vào ký tự rồi chọn từ danh sách gợi ý, danh sách phân cách bằng dấu <strong>;</strong></span>
                </span>

               <span style="width:50%; float:left"><span style="width:20%; justify-content:center;"> Lĩnh vực : </span><asp:TextBox ID="txtFields" runat="server" Height="50px" TextMode="MultiLine"
                    Width="80%"></asp:TextBox>
                <br />
                <span class="node-help">Gõ vào ký tự rồi chọn từ danh sách gợi ý, danh sách phân cách bằng dấu <strong>;</strong></span>
               </span> 
            </td>
                
        </tr>
        <tr style ="display:none;">
        <td style="width:110px"><asp:Label ID="LabelProvince" runat="server" Text="Tỉnh/thành phố:" meta:resourcekey="lblProvince"></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlProvince" runat="server" DataTextField="ProvinceDesc" 
                    DataValueField="ProvinceId" Width="98%" 
                    AutoPostBack="False" onselectedindexchanged="ddlProvince_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr id="tr2" runat="server" style ="display:none;">
            <td style="width:110px"><asp:Label ID="LabelDistrict" runat="server" Text="Quận/Huyện" meta:resourcekey="lblDistrict"></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlDistrict" runat="server" DataTextField="DistrictDesc" 
                    DataValueField="DistrictId" Width="98%" 
                    AutoPostBack="False" onselectedindexchanged="ddlDistrict_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr style ="display:none;">
        <td style="width:110px"><asp:Label ID="LabelWard" runat="server" Text="Phường/xã:" meta:resourcekey="lblWard"></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlWard" runat="server" DataTextField="WardDesc" 
                    DataValueField="WardId" Width="98%">
                </asp:DropDownList>
            </td>
        </tr>
        
        <tr style="display:none;">
            <td><asp:Label ID="LabelAddress" runat="server" Text="Địa chỉ:" meta:resourcekey="lblAddress"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="TextBoxAddress" runat="server" Width="98%"></asp:TextBox>
                </td>
        </tr>
        <tr style="display:none;" runat="server" >
            <td><asp:Label ID="LabelLongitude" runat="server" Text="Kinh độ:" meta:resourcekey="lblLongitude"></asp:Label>
                </td>
            <td>
                <input type="text" id="TextBoxLongitude" runat="server" value="" style="width: 98%;" />
                </td>
        </tr>
        <tr style="display:none;" runat="server">
            <td><asp:Label ID="LabelLatitude" runat="server" Text="Vĩ độ:" meta:resourcekey="lblLatitude"></asp:Label>
                </td>
            <td>
                <input type="text" id="TextBoxLatitude" runat="server" value="" style="width: 98%;" />
                </td>
        </tr>
        <tr style="display:none;">
                <td></td>
                <td>

                    <div class="form-horizontal">
                        <div id="map-location" style="width: 100%; height: 250px; margin-top: 10px;"></div>
                        <div class="clearfix">&nbsp;</div>
                    </div>
                </td>
            </tr>
        <%} %>
        <%if(CategoryId == 553)
          { %>
        <tr>
            <td>
                Tiêu đề bản tin:</td>
            <td>
                <asp:TextBox ID="txtNewsTitle" runat="server" Width="98%"></asp:TextBox></td>
        </tr>
        <%} %>
        <tr>
            <td>
                Tiêu đề SEO:</td>
            <td>
                <asp:TextBox ID="txtSEOTitle" runat="server" Width="98%"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                Mô tả SEO:</td>
            <td>
                <asp:TextBox ID="txtSEODesc" TextMode="MultiLine" Rows ="3" runat="server" Width="98%"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                Từ khóa SEO:</td>
            <td>
                <asp:TextBox ID="txtSEOKeyword" runat="server" Width="98%"></asp:TextBox></td>
        </tr>
        
        <tr>
            <td><h2>Snippet Preview</h2></td>
            <td>
                <div class="div_kf">

                    
                    <div class="div_kf2">
                        <span>Keyword was found in:</span><br />
                        <ul>
                            <li>Article Heading: <span id="kf_ahead" data-h1="0" data-h2-h3="0" class="c_No_K">No</span>
                            </li>
                            <li>Page title: <span id="kf_ptitle" class="c_No_K">No</span>
                            </li>
                            <li>Page Url: <span id="kf_purl" class="c_No_K">No</span>
                            </li>
                            <li>Content: <span id="kf_content" class="c_No_K">No</span>
                            </li>
                            <li>Meta description: <span id="kf_mdesc" class="c_No_K">No</span>
                            </li>
                        </ul>
                    </div>
                </div>
            </td>

        </tr>
        <tr>
            <td style="vertical-align:top">Thông tin khác:</td>
            <td>
                <asp:GridView ID="mGridFeature" runat="server" AutoGenerateColumns="False" DataKeyNames="FeatureId"
                    Width="60%" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField HeaderText="Thuộc tính">
                            <HeaderStyle Width="150px" />
                            <ItemTemplate>
                                <%# Eval("FeatureName")%>:
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Giá trị">
                            <ItemTemplate>
                                <asp:TextBox ID="txtFeatureValue" runat="server" Visible="false" Width="90%"></asp:TextBox>
                                <asp:DropDownList ID="ddlFeatureValue"  Visible="false" DataTextField="DataDictionaryName" DataValueField="DataDictionaryId" runat="server"></asp:DropDownList>
                                <asp:CheckBoxList ID="cblFeatureValue" runat="server" RepeatDirection="Horizontal" RepeatColumns="3" Visible="false" DataTextField="DataDictionaryName" DataValueField="DataDictionaryId"></asp:CheckBoxList>
                                <asp:RadioButtonList ID="rblFeatureValue" runat="server" RepeatDirection="Horizontal" RepeatColumns="3" Visible="false" DataTextField="DataDictionaryName" DataValueField="DataDictionaryId"></asp:RadioButtonList>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </td>
        </tr>
    </table>
    </div>
    <div style="background-color: #FFFFFF; bottom: 0px;  padding: 8px;  position: fixed;   right: 1px;   text-align: right;  width: 100%;">
        <asp:LinkButton ID="btnBack" runat="server" CssClass="quaylai" 
                    Text="Quay lại" meta:resourcekey="btnBack" ToolTip="Quay lại trang danh sách tin bài"
            onclick="btnBack_Click" >
        </asp:LinkButton>
        <asp:LinkButton ID="btnSave" runat="server" CssClass="luuvaquaylai" 
            Text="Lưu thông tin" ValidationGroup="G1" OnClientClick="javascript: return edit_confirm();" onclick="btnSave_Click" ToolTip="Lưu và tiếp tục sửa bài viết hiện tại">
        </asp:LinkButton>
        <asp:LinkButton ID="btnSaveAndNew" runat="server" OnClientClick="javascript: return edit_confirm();" CssClass="luuvathemmoi" ToolTip="Lưu và tiếp tục thêm bài viết khác " 
                    Text="Lưu và thêm mới" ValidationGroup="G1" meta:resourcekey="btnSaveAndNew" 
            onclick="btnSaveAndNew_Click"  >
        </asp:LinkButton>
    </div>
    <script type="text/javascript">
        var longdefault = $('#<%:TextBoxLongitude.ClientID%>').val();
        var latidefault = $('#<%:TextBoxLatitude.ClientID%>').val();
        if (longdefault.length == 0 || latidefault.length == 0 || (longdefault == '0' && latidefault == '0')) {
            longdefault = 105.78289610000002;
            latidefault = 21.0311856;
            GoogleMaps(latidefault, longdefault);
            //navigator.geolocation.getCurrentPosition(function (location) {
            //    latidefault = location.coords.latitude;
            //    longdefault = location.coords.longitude;
            //    GoogleMaps(latidefault, longdefault);
            //});
        } else {
            GoogleMaps(latidefault, longdefault);
        }
        //navigator.geolocation.getCurrentPosition(function (location) {
        //    console.log(location.coords.latitude);
        //    console.log(location.coords.longitude);
        //    console.log(location.coords.accuracy);
        //});

        function GoogleMaps(lat, long) {
            if (lat === void 0) {
                lat = 1;
            }
            if (long === void 0) {
                long = 1;
            }
            lat = lat.toString().replace(",", ".");
            long = long.toString().replace(",", ".");
            var popup = $('.overlay_googlemap');

            $('#map-location').locationpicker({
                location: {
                    latitude: lat,
                    longitude: long
                },
                radius: 100,
                inputBinding: {
                    latitudeInput: $('#<%:TextBoxLatitude.ClientID%>'),
                    longitudeInput: $('#<%:TextBoxLongitude.ClientID%>'),
                    locationNameInput: $('#address')
                },
                enableAutocomplete: true
            });
            if (popup.length > 0)
                $(".overlay_googlemap").fadeToggle("fast");
        }
        //    navigator.geolocation.getCurrentPosition(function (location) { 
        //        console.log(location.coords.latitude);
        //        console.log(location.coords.longitude);
        //        console.log(location.coords.accuracy);
        //    }); 
        function edit_confirm()
        {
            var e = $('#<%=ddlCategory.ClientID %>').val();
            if (e <= 0)
            {
                $('#<%=lbMsgCate.ClientID%>').html("Mời bạn chọn chuyên mục chính."); 
            return false;
        } 
        return true;
}</script>
</asp:Content>

