<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="DocsEditEnglish.aspx.cs" Inherits="Admin_DocsEditEnglish" %>

<%@ Import Namespace="ICSoft.CMSLib" %>
<%@ Import Namespace="ICSoft.LawDocsLib" %>
<%@ Import Namespace="ICSoft.HelperLib" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" runat="Server">
    <script language="javascript" type="text/javascript">
        var uploadCount = 1;
        function AddUpload() {
            var uploads = document.getElementById("uploads");
            var mySpan = document.getElementById('mySpan');
            var id = "upload" + uploadCount;
            var input = document.createElement('input');
            input.type = 'file';
            input.name = id;
            uploads.appendChild(document.createElement('br'));
            uploads.appendChild(input);

            var myElement3 = document.createElement('select');
            myElement3.setAttribute('name', "selSelect" + uploadCount);
            myElement3.setAttribute('id', "sel" + uploadCount);
            myElement3.setAttribute('runat', "server");
            mySpan.appendChild(document.createElement('br'));
            mySpan.appendChild(myElement3);

            //        // Create Option //
            fncCreateSelectOption(myElement3)

            uploadCount++;

        }
        function fncCreateSelectOption(ele) {
            var objSelect = ele;
            var Item = new Option("Chọn nguồn file", "0");
            objSelect.options[objSelect.length] = Item;

            var Item = new Option("Incom", "18");
            objSelect.options[objSelect.length] = Item;

            var Item = new Option("TTX", "19");
            objSelect.options[objSelect.length] = Item;

            var Item = new Option("ASEM", "20");
            objSelect.options[objSelect.length] = Item;

            var Item = new Option("Thu thập", "21");
            objSelect.options[objSelect.length] = Item;

            var Item = new Option("Convert", "22");
            objSelect.options[objSelect.length] = Item;
        }

        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);
            AddUpload();
           <%-- $("#<%= txtIssueDate.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#<%= txtEffectDate.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#<%= txtGazetteDate.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#<%= txtExpireDate.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });--%>
            if (typeof CKEDITOR == 'undefined') {

            }
            else {
                //var editor = CKEDITOR.replace('<%= txtDocContent.ClientID %>');
                var editor = CKEDITOR.replace('<%= txtDocContent.ClientID %>', {
                    filebrowserBrowseUrl: '../Articles/MediasSelect.aspx?SetMediaDomain=1',
                    filebrowserUploadUrl: '/uploader/upload.php?type=Files'
                });
                //CKFinder.setupCKEditor(editor, '<%= ICSoft.CMSLib.CmsConstants.ROOT_PATH %>Integrated/ckfinder/');
            }
    });

    ////
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
			        $.getJSON("Fields_GetNameByJson.aspx", {
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

    $(function () {
        function split(val) {
            return val.split(/;\s*/);
        }
        function extractLast(term) {
            return split(term).pop();
        }

        $("#<%= txtSigners.ClientID %>")
			.bind("keydown", function (event) {
			    if (event.keyCode === $.ui.keyCode.TAB &&
						$(this).data("autocomplete").menu.active) {
			        event.preventDefault();
			    }
			})
			.autocomplete({
			    source: function (request, response) {
			        $.getJSON("Signers_GetNameByJson.aspx", {
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



    $(function () {
        function split(val) {
            return val.split(/;\s*/);
        }
        function extractLast(term) {
            return split(term).pop();
        }
        $("#<%= txtOrgans.ClientID %>")
			.bind("keydown", function (event) {
			    if (event.keyCode === $.ui.keyCode.TAB &&
						$(this).data("autocomplete").menu.active) {
			        event.preventDefault();
			    }
			})
			.autocomplete({
			    source: function (request, response) {
			        $.getJSON("Organs_GetNameByJson.aspx", {
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
        //// aurtocomplete keywword
        $(function () {
            function split(val) {
                return val.split(/;\s*/);
            }
            function extractLast(term) {
                return split(term).pop();
            }
            $("#<%= txtKeywords.ClientID %>")
		.bind("keydown", function (event) {
		    if (event.keyCode === $.ui.keyCode.TAB &&
					$(this).data("autocomplete").menu.active) {
		        event.preventDefault();
		    }
		})
		.autocomplete({
		    source: function (request, response) {
		        $.getJSON("DocKeywords_GetNameByJson.aspx", {
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

    function createEditor(languageCode, id) {
        CKEDITOR.replace(id, {
            extraPlugins: 'lawsvnReference',
        });
    }

    <%--$(function () {
        createEditor('vi', <%=txtDocContent.ClientID%>);
    });--%>

        function InitializeRequest(sender, args) {
        }

        function EndRequest(sender, args) {
            $("#<%= txtIssueDate.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#<%= txtEffectDate.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#<%= txtGazetteDate.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#<%= txtExpireDate.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
    }

    </script>
    <style type="text/css">
        .ui-autocomplete {
            max-height: 150px;
            overflow-y: auto; /* prevent horizontal scrollbar */
            overflow-x: hidden; /* add padding to account for vertical scrollbar */
            padding-right: 20px;
        }
        /* IE 6 doesn't support max-height
	     * we use height instead, but this forces the menu to always be this tall
	     */
        * html .ui-autocomplete {
            height: 150px;
        }

        .style2 {
            font-family: Arial, Helvetica, sans-serif;
            font-size: small;
        }

        .style3 {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 10pt;
        }

        #over {
            display: none;
            background: #000;
            position: fixed;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            opacity: 0.8;
            z-index: 999;
        }

        a, a:visited, a:active {
            text-decoration: none;
        }

        .lawsrefer {
            background-color: #85B561;
            height: auto;
            width: 450px;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 14px;
            padding-bottom: 5px;
            display: none;
            overflow: hidden;
            position: absolute;
            z-index: 999999;
            top: 10%;
            left: 50%;
            margin-left: -300px;
        }

            .lawsrefer .lawsrefer_title {
                color: white;
                font-size: 16px;
                padding: 8px 0 5px 8px;
                text-align: left;
            }

        .lawsrefer-content label {
            display: block;
            padding-bottom: 7px;
        }

        .lawsrefer-content span {
            display: block;
        }

        .lawsrefer-content {
            padding-left: 35px;
            background-color: white;
            margin-left: 5px;
            margin-right: 5px;
            height: auto;
            padding-top: 15px;
            overflow: hidden;
        }

        .img-close {
            float: right;
            margin-top: -43px;
            margin-right: 5px;
        }

        .button {
            display: inline-block;
            min-width: 46px;
            text-align: center;
            color: #444;
            font-size: 14px;
            font-weight: 700;
            height: 36px;
            padding: 0px 8px;
            line-height: 36px;
            border-radius: 4px;
            transition: all 0.218s ease 0s;
            border: 1px solid #DCDCDC;
            background-color: #F5F5F5;
            background-image: -moz-linear-gradient(center top, #F5F5F5, #F1F1F1);
            cursor: pointer;
        }

            .button:hover {
                border: 1px solid #DCDCDC;
                text-decoration: none;
                -moz-box-shadow: 0 1px 1px rgba(0,0,0,0.1);
                -webkit-box-shadow: 0 1px 1px rgba(0,0,0,0.1);
                box-shadow: 0 2px 2px rgba(0,0,0,0.1);
            }

        .lawsrefer input {
            border: 1px solid #D5D5D5;
            border-radius: 5px;
            box-shadow: 1px 1px 5px rgba(0,0,0,.07) inset;
            color: black;
            font: 12px/25px "Droid Sans","Helvetica Neue",Helvetica,Arial,sans-serif;
            height: 28px;
            padding: 0px 8px;
            word-spacing: 0.1em;
            width: 300px;
        }

        .submit-button {
            display: inline-block;
            padding: auto;
            margin: 15px 75px;
            width: 150px;
        }

        span.dotted {
            border-bottom: 1px dashed #999;
            text-decoration: none;
        }
    </style>

    <div id="lawsrefer-box" class="lawsrefer">
        <p class="lawsrefer_title"></p>
        <a href="#" class="close">
            <img src="../../images/close.png" class="img-close" title="Đóng" alt="Close" /></a>
        <div class="lawsrefer-content">
            <label class="website">
                <span>Website:</span>
                <input id="website" name="website" value="" type="text" autocomplete="on" placeholder="">
            </label>
            <label class="reference">
                <span>Dẫn chiếu:</span>
                <input id="reference" name="reference" value="" placeholder="">
            </label>
            <button class="button submit-button save-refer" type="button">Lưu</button>
        </div>
    </div>

    <div style="text-align: center">
        <asp:Label ID="lblMessage" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label></div>
    <table cellpadding="3" cellspacing="0" style="width: 100%; font-weight: lighter">
        <tr>
            <td colspan="4">
                 <span class="node-help">
                     Nhập số hiệu của văn bản tiếng Việt vào ô "Số hiệu văn bản" rồi nhấn nút "Tìm VB" để lấy thông tin văn bản tiếng Việt cần nhập nội dung tiếng Anh, các ô nhập có nền màu xám dữ liệu được lấy tự động từ văn bản tiếng Việt, không thể sửa
                 </span>
            </td>
        </tr>
        <tr>
            <td style="width:10%;">
                <asp:Label ID="lblDocGroups" runat="server" Text="Nhóm văn bản:" meta:resourcekey="lblDocGroups"></asp:Label>
            </td>
            <td style="width:40%;">
                <asp:DropDownList ID="ddlDocGroups" runat="server" AutoPostBack="False" CssClass="userselect" Width="250px" OnSelectedIndexChanged="ddlDocGroups_SelectedIndexChanged">
                    <asp:ListItem Value="1" Text="Văn bản pháp quy"></asp:ListItem>
                    <asp:ListItem Value="2" Text="Văn bản UBND"></asp:ListItem>
                    <asp:ListItem Value="5" Text="Văn bản hợp nhất"></asp:ListItem>
                    <asp:ListItem Value="6" Text="Công văn"></asp:ListItem>
                    <asp:ListItem Value="7" Text="VB Chỉ đạo điều hành"></asp:ListItem>
                    <asp:ListItem Value="8" Text="VB Không có ND download"></asp:ListItem>
                </asp:DropDownList>
            </td>
             <td style="width:10%;">
                <asp:Label ID="lblLanguage" runat="server" meta:resourcekey="lblLanguage"
                    Text="Ngôn ngữ:"></asp:Label>
                 </td>
            <td >
                <asp:DropDownList ID="ddlLanguage" runat="server" DataTextField="LanguageDesc" CssClass="userselect" Enabled="false"
                    DataValueField="LanguageId" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        
        <tr>
            <td style="width: 10%">
                <asp:Label ID="lblDocName" runat="server" Text="Tên văn bản:"
                    meta:resourcekey="lblDocName"></asp:Label>
                <span class="icon-required"></span>
                
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtDocName" runat="server" Width="90%"
                    CssClass="tukhoatimekiem" Height="50px" TextMode="MultiLine"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="rfvDocName" runat="server" SetFocusOnError="true"
                    ControlToValidate="txtDocName"
                    Display="Dynamic"
                    ErrorMessage="Tên văn bản"
                    ForeColor="Red"
                    Text="Bạn cần nhập vào Tên văn bản"
                    ValidationGroup="G1"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 10%">
                <asp:Label ID="Label2" runat="server" Text="Văn bản tiếng Việt:"></asp:Label>
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtDocNameVN" runat="server" Width="90%" 
                    CssClass="tukhoatimekiem" Height="30px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 10%">
                <asp:Label ID="lblDocTypes" runat="server" Text="Loại văn bản:" meta:resourcekey="lblDocTypes"></asp:Label></td>
            <td style="width: 40%">
                <asp:DropDownList ID="ddlDocTypes" runat="server" CssClass="userselect"
                    DataTextField="DocTypeDesc" DataValueField="DocTypeId"
                    Width="250px">
                </asp:DropDownList></td>
            <td style="width: 10%">
                <asp:Label ID="lblDocIdentity" runat="server" Text="Số hiệu văn bản:"
                    meta:resourcekey="lblDocIdentity"></asp:Label>
                <span class="icon-required"></span>
            </td>
            <td>
                <asp:TextBox ID="txtDocIdentity" runat="server" Width="240px"
                    CssClass="tukhoatimekiem"></asp:TextBox>
                <asp:LinkButton ID="lnkSearchIdentity" runat="server" Visible="true" class="themmoi" Text="Tìm VB  " ToolTip="Nhập số hiệu văn bản tiếng Việt để tìm kiếm"
                    meta:resourcekey="lnkSearchIdentity" OnClick="lnkSearchIdentity_Click">
                </asp:LinkButton>
                <br />
                <asp:RequiredFieldValidator ID="rfvDocIdentity" Text="Bạn cần nhập vào số hiệu VB" runat="server" Display="Dynamic"
                    ErrorMessage="Số hiệu văn bản" ValidationGroup="G1"
                    ControlToValidate="txtDocIdentity" ForeColor="Red"></asp:RequiredFieldValidator>
                <div id="tblDocshowByIdentity" runat="server" visible="false">
                    <asp:DropDownList ID="ddlSelectDocs" runat="server" AutoPostBack="True"
                        CssClass="userselect" DataTextField="DocName" DataValueField="DocId"
                        OnSelectedIndexChanged="ddlSelectDocs_SelectedIndexChanged" Width="91%">
                    </asp:DropDownList>
                    <asp:LinkButton ID="lnkSelectDocs" runat="server" class="themmoi"
                        meta:resourcekey="lnkSelectDocs" OnClick="lnkSelectDocs_Click" Text="Chọn »">
                    </asp:LinkButton>
                </div>
                <div id="tblMsg" runat="server" visible="false">
                    <asp:Label ID="lblMsg" runat="server" Font-Bold="true" ForeColor="Red"
                        meta:resourcekey="lblMsg"
                        Text="Không có văn bản có số hiệu đó trong hệ thống tiếng việt"></asp:Label>
                </div>
            </td>
        </tr>
        <tr>
            <td style="width: 10%">
                <asp:Label ID="lblIssueDate" runat="server" Text="Ngày ban hành:" meta:resourcekey="lblIssueDate"></asp:Label>
                <span class="icon-required"></span>
                <asp:RequiredFieldValidator ID="rfvIssueDate" Text="(*)" runat="server" Display="Dynamic"
                    ErrorMessage="Ngày ban hành" ValidationGroup="G1"
                    ControlToValidate="txtIssueDate" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
            <td style="width: 40%">
                <asp:TextBox ID="txtIssueDate" runat="server" Width="240px" CssClass="tukhoatimekiem"></asp:TextBox>

            </td>
            <td style="width: 10%">
                <asp:Label ID="lblEffectDate" runat="server" Text="Ngày có hiệu lực:" meta:resourcekey="lblEffectDate"></asp:Label>
               </td> 
            <td>
                <asp:TextBox ID="txtEffectDate" runat="server" Width="240px" CssClass="tukhoatimekiem"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 10%">
                <asp:Label ID="lblGazetteNumber" runat="server" Text="Số công báo:"
                    meta:resourcekey="lblGazetteNumber"></asp:Label>
            </td>
            <td style="width: 40%">
                <asp:TextBox ID="txtGazetteNumber" runat="server" Width="240px"
                    CssClass="tukhoatimekiem"></asp:TextBox></td>
            <td style="width: 10%">
                <asp:Label ID="lblExpireDate" runat="server" Text="Ngày hết hiệu lực:" meta:resourcekey="lblExpireDate"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtExpireDate" runat="server" Width="240px" CssClass="tukhoatimekiem"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 10%">
                <asp:Label ID="lblGazetteDate" runat="server" Text="Ngày công báo:"
                    meta:resourcekey="lblGazetteDate"></asp:Label>
            </td>
            <td style="width: 40%">
                <asp:TextBox ID="txtGazetteDate" runat="server" Width="240px"
                    CssClass="tukhoatimekiem"></asp:TextBox>
                <a class="help-lnk" href="javascript:void(0)" title="Chọn hoặc nhập ngày tháng theo định dang dd/MM/yyyy" ><span class="aui-icon aui-icon-small aui-iconfont-help">?</span></a>
                (dd/MM/yyyy)
                <br />
                <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="Ngày bạn nhập không hợp lệ" ControlToValidate="txtExpireDate" ForeColor="Red" SetFocusOnError="true" Display="Dynamic" Operator="DataTypeCheck" Type="Date" ValidationGroup="G1"></asp:CompareValidator>
            </td>
            <td style="width: 10%">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 10%">
                <asp:Label ID="lblSigners" runat="server" Text="Người ký:"
                    meta:resourcekey="lblSigners"></asp:Label>
                <span class="icon-required"></span>
                <asp:RequiredFieldValidator ID="rfvSigners" Text="(*)" runat="server" Display="Dynamic"
                    ErrorMessage="Người ký" ValidationGroup="G1"
                    ControlToValidate="txtSigners" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
            <td style="width: 40%">
                <asp:TextBox ID="txtSigners" runat="server" Height="45px" TextMode="MultiLine"
                    Width="300px"></asp:TextBox></td>
            <td style="width: 10%">
                <asp:Label ID="lblFields" runat="server" Text="Lĩnh vực:"
                    meta:resourcekey="lblFields"></asp:Label>
                <span class="icon-required"></span>
            </td>
            <td>
                <asp:TextBox ID="txtFields" runat="server" Height="45px" TextMode="MultiLine"
                    Width="300px"></asp:TextBox></td>
            
        </tr>
        <tr>
            <td style="width: 10%">
                <asp:Label ID="lblOrgans" runat="server" Text="CQBH:"
                    meta:resourcekey="lblOrgans"></asp:Label>
                <span class="icon-required"></span>
                <asp:RequiredFieldValidator ID="rfvOrgans" Text="(*)" runat="server" Display="Dynamic"
                    ErrorMessage="Cơ quan ban hành" ValidationGroup="G1"
                    ControlToValidate="txtOrgans" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:TextBox ID="txtOrgans" runat="server" Height="85px" TextMode="MultiLine"
                    Width="300px"></asp:TextBox></td>
            <td class="style2">
                <asp:Label ID="Label8" runat="server" Text="Tỉnh thành :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtProvices" runat="server" Height="85px" TextMode="MultiLine"
                    Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 10%">
                <asp:Label ID="lblUseStatus" runat="server" Text="Trạng thái văn bản:"
                    meta:resourcekey="lblUseStatus"></asp:Label>
            </td>
            <td style="width: 40%">
                <asp:DropDownList ID="ddlUseStatus" runat="server" CssClass="userselect"
                    DataTextField="UseStatusDesc" DataValueField="UseStatusId"
                    Width="250px">
                </asp:DropDownList></td>
            <td style="width: 10%">
                <asp:Label ID="lblKeywords" runat="server" Text="Từ khóa:" 
                    meta:resourcekey="lblKeywords"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtKeywords" runat="server" Height="50px" TextMode="MultiLine" 
                    Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
                <td style="width: 10%">
                <asp:Label ID="lblEffectStatus" runat="server" Text="Trạng thái hiệu lực:"
                    meta:resourcekey="lblEffectStatus"></asp:Label>
            </td>
            <td style="width: 40%">
                <asp:DropDownList ID="ddlEffectStatus" runat="server" CssClass="userselect"
                    DataTextField="EffectStatusDesc" DataValueField="EffectStatusId"
                    Width="250px">
                </asp:DropDownList></td>
            <td style="width: 10%"></td>
            <td>
                
            </td>
        </tr>
        <tr style="display: none;">
            <td style="width: 10%">
                <asp:Label ID="Label1" runat="server" Text="Trạng thái:"
                    meta:resourcekey="lblReviewStatus"></asp:Label>
            </td>
            <td style="width: 40%">
                <asp:DropDownList ID="ddlReviewStatus" runat="server" CssClass="userselect"
                    DataTextField="ReviewStatusDesc" DataValueField="ReviewStatusId"
                    Width="250px">
                </asp:DropDownList></td>
            <td style="width: 10%"></td>
            <td></td>
        </tr>
        <tr style="display:none;">
            <td style="width: 10%"></td>
            <td style="width: 40%"></td>
            <td style="width: 10%">
                <asp:Label ID="lblDocFileName" runat="server" Text="Tên tập tin:"
                    meta:resourcekey="lblDocFileName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDocFileName" runat="server" Width="240px"
                    CssClass="tukhoatimekiem"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblFileUpload" runat="server" meta:resourcekey="lblFileUpload"
                    Text="File Upload:"></asp:Label>
            </td>
            <td colspan="3" align="left">
                <table cellpadding="2" cellspacing="1">
                    <tr>
                        <td>
                            <div id="uploads" style="display: inline">
                            </div>
                        </td>
                        <td valign="middle">
                            <div id="mySpan" style="display: inline"></div>
                        </td>
                    </tr>
                </table>
                <div id="Div1" style="display: inline">
                    <a href="javascript: void(0);" onclick="javascript: AddUpload();">
                        <asp:Label ID="lblAddFile" runat="server" Text="Thêm file:"
                            meta:resourcekey="lblAddFile"></asp:Label></a>
                </div>
                <asp:GridView ID="m_grid_File" DataKeyNames="DocFileId" runat="server" ShowHeader="False"
                    AutoGenerateColumns="False" CssClass="grid" PageSize="50" Width="60%"
                    OnRowDeleting="m_grid_File_RowDeleting" CellPadding="2" ForeColor="#333333"
                    GridLines="None">
                    <HeaderStyle CssClass="grid_head" BackColor="#5D7B9D" Font-Bold="True"
                        ForeColor="White" />
                    <FooterStyle CssClass="grid_foot" BackColor="#5D7B9D" Font-Bold="True"
                        ForeColor="White" />
                    <RowStyle CssClass="grid_item" BackColor="#F7F6F3" ForeColor="#333333" />
                    <AlternatingRowStyle CssClass="grid_item_alternating" BackColor="White"
                        ForeColor="#284775" />
                    <SelectedRowStyle CssClass="grid_selected" BackColor="#E2DED6" Font-Bold="True"
                        ForeColor="#333333" />
                    <EditRowStyle CssClass="grid_edit" BackColor="#999999" />
                    <PagerStyle CssClass="grid_page" BackColor="#284775" ForeColor="White"
                        HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <a href='../../../<%# Eval("FilePath") %>'><%# Eval("FilePath")%></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblGridColumnDataSources" runat="server" Text="Nguồn" meta:resourcekey="lblGridColumnDataSources"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# LanguageHelpers.IsCultureVietnamese() ? DataSources.Static_Get(short.Parse(Eval("DataSourceId").ToString()), l_DataSources).DataSourceDesc : DataSources.Static_Get(short.Parse(Eval("DataSourceId").ToString()), l_DataSources).DataSourceName%>
                            </ItemTemplate>
                            <ItemStyle Width="8%" HorizontalAlign="Left" VerticalAlign="Top" Wrap="false" />
                            <HeaderStyle Width="8%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Xóa" ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbDelete" runat="server" CssClass="iconadmxoa" CommandName="Delete" ToolTip="Xóa" meta:resourcekey="lnkGridColumnDel" Visible='<%# ((ReviewStatusId.ToString() == "2") ? false : true) %>' OnClientClick="return confirm('Ban co thuc su muon xoa du lieu nay?');"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                        </asp:TemplateField>
                    </Columns>
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblContent" runat="server" Text="Nội dung:"
                    meta:resourcekey="lblContent"></asp:Label>
            </td>
            <td colspan="3">
                <CKEditor:CKEditorControl ID="txtDocContent" runat="server"  PasteFromWordRemoveStyles="false" PasteFromWordPromptCleanup="true" 
                    BasePath="~/Integrated/ckeditor/" Height="270px" Width="90%"></CKEditor:CKEditorControl>
                <%----%>
            </td>
        </tr>
    </table>
    <div style="background-color: #FFFFFF; bottom: 0px; padding: 8px; position: fixed; right: 1px; text-align: right; width: 100%;">
        <asp:LinkButton ID="btnBack" runat="server" CssClass="quaylai"
            Text="Quay lại" meta:resourcekey="btnBack"
            OnClick="btnBack_Click">
        </asp:LinkButton>
        <asp:LinkButton ID="btnSave" runat="server" CssClass="luuvaquaylai"
            Text="Lưu thông tin" meta:resourcekey="btnSave" ValidationGroup="G1" OnClick="btnSave_Click">
        </asp:LinkButton>
        <asp:LinkButton ID="btnSaveAndNew" runat="server" CssClass="luuvathemmoi"
            Text="Lưu và thêm mới" ValidationGroup="G1" meta:resourcekey="btnSaveAndNew"
            OnClick="btnSaveAndNew_Click">
        </asp:LinkButton>
    </div>
</asp:Content>

