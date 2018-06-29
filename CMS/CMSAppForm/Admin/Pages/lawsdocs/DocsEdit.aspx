﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="DocsEdit.aspx.cs" Inherits="Admin_DocsEdit" %>

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
            myElement3.setAttribute('id', "selSelect" + uploadCount);
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
            var Item = new Option("Dành cho website khác", "48");
            objSelect.options[objSelect.length] = Item;
        }

        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);
            AddUpload();
            <%--$("#<%= txtIssueDate.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
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
			        console.log(request.term);
			        $.getJSON("Organs_GetNameByJson.aspx", {
			            term: extractLast(request.term)
			        }, function (data) {
			            console.log(data);
			            response(data);
			        });
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
    ////  
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

        textarea {
            font: 400 13.3333px Arial;
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
        <asp:Label ID="lblMessage" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
    </div>
    <table cellpadding="3" cellspacing="0" style="width: 100%; font-weight: lighter">
        <tr>
            <asp:ValidationSummary ID="ValidationSummary" runat="server"
                HeaderText="Bạn vui lòng kiểm tra lại thông tin sau:"
                ShowMessageBox="true"
                DisplayMode="BulletList"
                ShowSummary="false"
                ValidationGroup="G10"
                BackColor="Snow"
                Width="450"
                ForeColor="Red"
                Font-Size="X-Large"
                Font-Italic="true" />
        </tr>
        <tr>
            <td style="width: 10%;">
                <asp:Label ID="lblDocGroups" runat="server" Text="Nhóm văn bản:" meta:resourcekey="lblDocGroups"></asp:Label>
            </td>
            <td style="width: 40%;">
                <asp:DropDownList ID="ddlDocGroups" runat="server" AutoPostBack="true" CssClass="userselect" Width="250px"
                    OnSelectedIndexChanged="ddlDocGroups_SelectedIndexChanged">
                    <asp:ListItem Value="1" Text="Văn bản pháp quy"></asp:ListItem>
                    <asp:ListItem Value="2" Text="Văn bản UBND"></asp:ListItem>
                    <asp:ListItem Value="6" Text="Công văn"></asp:ListItem>
                    <asp:ListItem Value="8" Text="VB Không có ND download"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width: 10%;">
                <asp:Label ID="lblLanguage" runat="server" meta:resourcekey="lblLanguage"
                    Text="Ngôn ngữ:"></asp:Label>
            </td>
            <td>
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
                <asp:RequiredFieldValidator ID="rfvDocName" runat="server"
                    ControlToValidate="txtDocName"
                    Display="Dynamic"
                    ErrorMessage="Tên văn bản"
                    ForeColor="Red"
                    Text="Bạn cần nhập vào tên văn bản"
                    SetFocusOnError="true"
                    ValidationGroup="G1"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 10%">
                <asp:Label ID="lblDocTypes" runat="server" Text="Loại văn bản:" meta:resourcekey="lblDocTypes"></asp:Label>
                <span class="icon-required"></span>

            </td>
            <td style="width: 40%">
                <asp:DropDownList ID="ddlDocTypes" runat="server" CssClass="userselect"
                    DataTextField="DocTypeDesc" DataValueField="DocTypeId"
                    Width="250px">
                </asp:DropDownList>
                <br />
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Loại văn bản"
                    ControlToValidate="ddlDocTypes" Display="Dynamic" Text="Bạn cần chọn Loại văn bản" ValidationGroup="G1" ForeColor="Red" ValueToCompare="0" Operator="NotEqual" SetFocusOnError="True"></asp:CompareValidator>
            </td>
            <td style="width: 10%">
                <asp:Label ID="lblDocIdentity" runat="server" Text="Số hiệu văn bản:"
                    meta:resourcekey="lblDocIdentity"></asp:Label>
                <span class="icon-required"></span>
            </td>
            <td>
                <asp:TextBox ID="txtDocIdentity" runat="server" Width="240px"
                    CssClass="tukhoatimekiem"></asp:TextBox>
                <asp:LinkButton ID="lnkSearchIdentity" runat="server" Visible="true" class="themmoi" Text="Tìm VB  "
                    meta:resourcekey="lnkSearchIdentity" OnClick="lnkSearchIdentity_Click">
                </asp:LinkButton>
                <br />
                <asp:RequiredFieldValidator ID="rfvDocIdentity" Text="Bạn cần nhập số hiệu văn bản" runat="server" Display="Dynamic"
                    ErrorMessage="Số hiệu văn bản" ValidationGroup="G1" SetFocusOnError="true"
                    ControlToValidate="txtDocIdentity" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 10%">
                <asp:Label ID="lblIssueDate" runat="server" Text="Ngày ban hành:" meta:resourcekey="lblIssueDate"></asp:Label>
                <span class="icon-required"></span>
            </td>
            <td style="width: 40%">
                <asp:TextBox ID="txtIssueDate" runat="server" Width="240px" CssClass="tukhoatimekiem"></asp:TextBox>
                <a class="help-lnk" href="javascript:void(0)" title="Chọn hoặc nhập ngày tháng theo định dang dd/MM/yyyy"><span class="aui-icon aui-icon-small aui-iconfont-help">?</span></a>
                (dd/MM/yyyy)<br />
                <asp:RequiredFieldValidator ID="rfvIssueDate" Text="Bạn cần nhập Ngày ban hành" runat="server" Display="Dynamic"
                    ErrorMessage="Ngày ban hành" ValidationGroup="G1" SetFocusOnError="true"
                    ControlToValidate="txtIssueDate" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Ngày bạn nhập không hợp lệ" ControlToValidate="txtIssueDate" ForeColor="Red" SetFocusOnError="true" Display="Dynamic" Operator="DataTypeCheck" Type="Date" ValidationGroup="G1"></asp:CompareValidator>
                <asp:Label runat="server" ID="lblMess" Text=""></asp:Label>
            </td>
            <td style="width: 10%">
                <asp:Label ID="lblEffectDate" runat="server" Text="Ngày có hiệu lực:" meta:resourcekey="lblEffectDate"></asp:Label><span class="icon-required" ID="rqEffectDate" runat="server"></span>
            </td>
            <td>
                <asp:TextBox ID="txtEffectDate" runat="server" Width="240px" CssClass="tukhoatimekiem"></asp:TextBox>
                <a class="help-lnk" href="javascript:void(0)" title="Chọn hoặc nhập ngày tháng theo định dang dd/MM/yyyy"><span class="aui-icon aui-icon-small aui-iconfont-help">?</span></a>
                (dd/MM/yyyy)<br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Text="Bạn cần nhập Ngày có hiệu lực" runat="server" Display="Dynamic"
                    ErrorMessage="Ngày ban hành" ValidationGroup="G1" SetFocusOnError="true"
                    ControlToValidate="txtEffectDate" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Ngày bạn nhập không hợp lệ" ControlToValidate="txtEffectDate" ForeColor="Red" SetFocusOnError="true" Display="Dynamic" Operator="DataTypeCheck" Type="Date" ValidationGroup="G1"></asp:CompareValidator>
            </td>
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
                <asp:TextBox ID="txtExpireDate" runat="server" Width="240px" CssClass="tukhoatimekiem"></asp:TextBox>
                <a class="help-lnk" href="javascript:void(0)" title="Chọn hoặc nhập ngày tháng theo định dang dd/MM/yyyy"><span class="aui-icon aui-icon-small aui-iconfont-help">?</span></a>
                (dd/MM/yyyy)<br />
                <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Ngày bạn nhập không hợp lệ" ControlToValidate="txtExpireDate" ForeColor="Red" SetFocusOnError="true" Display="Dynamic" Operator="DataTypeCheck" Type="Date" ValidationGroup="G1"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 10%">
                <asp:Label ID="lblGazetteDate" runat="server" Text="Ngày công báo:"
                    meta:resourcekey="lblGazetteDate"></asp:Label>
            </td>
            <td style="width: 40%">
                <asp:TextBox ID="txtGazetteDate" runat="server" Width="240px"
                    CssClass="tukhoatimekiem"></asp:TextBox>
                <a class="help-lnk" href="javascript:void(0)" title="Chọn hoặc nhập ngày tháng theo định dang dd/MM/yyyy"><span class="aui-icon aui-icon-small aui-iconfont-help">?</span></a>
                (dd/MM/yyyy)<br />
                <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="Ngày bạn nhập không hợp lệ" ControlToValidate="txtGazetteDate" ForeColor="Red" SetFocusOnError="true" Display="Dynamic" Operator="DataTypeCheck" Type="Date" ValidationGroup="G1"></asp:CompareValidator>
            </td>
            <td style="width: 10%">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 10%">
                <asp:Label ID="lblSigners" runat="server" Text="Người ký:"
                    meta:resourcekey="lblSigners"></asp:Label>
                <span id="SignerRequired" runat="server" class="icon-required"></span>
            </td>
            <td style="width: 40%">
                <asp:TextBox ID="txtSigners" runat="server" Height="45px" TextMode="MultiLine"
                    Width="300px"></asp:TextBox>
                <br />
                <span class="node-help">Gõ vào ký tự rồi chọn từ danh sách gợi ý, danh sách phân cách bằng dấu <strong>;</strong></span>
                <br />
                <asp:RequiredFieldValidator ID="rfvSigners" Text="Bạn cần nhập vào Người ký" runat="server" Display="Dynamic"
                    ErrorMessage="Người ký" ValidationGroup="G1"
                    ControlToValidate="txtSigners" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
            <td style="width: 10%">
                <asp:Label ID="lblFields" runat="server" Text="Lĩnh vực:"
                    meta:resourcekey="lblFields"></asp:Label>
                <span id="fieldRequired" runat="server" class="icon-required"></span>

            </td>
            <td>
                <asp:TextBox ID="txtFields" runat="server" Height="45px" TextMode="MultiLine"
                    Width="300px"></asp:TextBox>
                <br />
                <span class="node-help">Gõ vào ký tự rồi chọn từ danh sách gợi ý, danh sách phân cách bằng dấu <strong>;</strong></span>
                <br />
                <asp:RequiredFieldValidator ID="rfvFields" Text="Bạn cần nhập vào Lĩnh vực" runat="server" Display="Dynamic"
                    ErrorMessage="Lĩnh vực" ValidationGroup="G1"
                    ControlToValidate="txtFields" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>

        </tr>

        <tr>
            <td style="width: 10%">
                <asp:Label ID="lblOrgans" runat="server" Text="CQBH:"
                    meta:resourcekey="lblOrgans"></asp:Label>
                <span class="icon-required"></span>

            </td>
            <td>
                <asp:TextBox ID="txtOrgans" runat="server" Height="85px" TextMode="MultiLine"
                    Width="300px"></asp:TextBox>

                <br />
                <span class="node-help">Gõ vào ký tự rồi chọn từ danh sách gợi ý, danh sách phân cách bằng dấu <strong>;</strong></span>
                <br />
                <asp:RequiredFieldValidator ID="rfvOrgans" Text="Bạn cần nhập vào Cơ quan ban hành" runat="server" Display="Dynamic"
                    ErrorMessage="Cơ quan ban hành" ValidationGroup="G1"
                    ControlToValidate="txtOrgans" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

            </td>
            <td class="style2">
                <asp:Label ID="lblProvices" runat="server" Text="Tỉnh thành :" meta:resourcekey="lblProvices"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtProvices" runat="server" Height="85px" TextMode="MultiLine"
                    Width="300px"></asp:TextBox>
                <br />
                <span class="node-help">Gõ vào ký tự rồi chọn từ danh sách gợi ý, danh sách phân cách bằng dấu <strong>;</strong></span>
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
            <%--<td style="width: 10%">--%>
            <%-- <asp:Label ID="lblDataSources" runat="server" Text="Nguồn:"
                    meta:resourcekey="lblDataSources"></asp:Label>
                <asp:RequiredFieldValidator ID="rfvddlDataSources" Text="(*)" runat="server" Display="Dynamic"
                    ErrorMessage="Nguồn văn bản" ValidationGroup="G1"
                    ControlToValidate="ddlDataSources" InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>--%>
            <%--</td>--%>
            <%--<td>--%>
            <%--<asp:DropDownList ID="ddlDataSources" runat="server" CssClass="userselect"
                    DataTextField="DataSourceDesc" DataValueField="DataSourceId"
                    Width="250px">
                </asp:DropDownList>--%>
            <%--</td>--%>
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
        <tr style="display: none;">
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
                <asp:GridView ID="m_grid_File" DataKeyNames="DocId" runat="server" ShowHeader="False"
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
                                <a href='/Download.aspx?file=<%# Eval("FilePath") %>'><%# Eval("FilePath")%></a>
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
                <CKEditor:CKEditorControl ID="txtDocContent" runat="server" PasteFromWordRemoveStyles="false" PasteFromWordPromptCleanup="true"
                    BasePath="~/Integrated/ckeditor/" Height="270px" Width="90%"></CKEditor:CKEditorControl>
                <br />
                <asp:CheckBox ID="cbAutoProcessTable" runat="server" Text="Tự động xử lý bảng" Checked="true" ToolTip="Nếu tick chọn sẽ tự động bỏ định dạng của bảng biểu" />
                <asp:CheckBox ID="cbAutoProcessFootNote" runat="server" Text="Tự động xử footnote" Checked="true" ToolTip="Nếu tick chọn sẽ tự động tạo footnote dựa vào định dạng " />
                <asp:CheckBox ID="cbAutoProcessImage" runat="server" Text="Tự động xử lý ảnh lỗi" Checked="true" ToolTip="Nếu tick chọn sẽ tự động xóa các ảnh lỗi do dán từ word " />

                <asp:CheckBox ID="cbAutoRemoveNeo" runat="server" Text="Tự động bỏ NEO" Checked="true" ToolTip="Nếu tick chọn sẽ tự động xóa các NEO do dán từ word " />

            </td>
        </tr>
    </table>
    <div style="background-color: #FFFFFF; bottom: 0px; padding: 8px; position: fixed; right: 1px; text-align: right; width: 100%;">
        <asp:LinkButton ID="btnBack" runat="server" CssClass="quaylai"
            Text="Quay lại" meta:resourcekey="btnBack"
            OnClick="btnBack_Click">
        </asp:LinkButton>
        <asp:LinkButton ID="btnSave" runat="server" CssClass="luuvaquaylai hidden-save" ToolTip="Lưu và tiếp tục sửa văn bản hiện tại" Visible="true"
            Text="Lưu thông tin" meta:resourcekey="btnSave" ValidationGroup="G1" OnClick="btnSave_Click">
        </asp:LinkButton>

        <input type="button" name="ctl00$m_contentBody$btnSaveHide" value="Lưu thông tin" id="m_contentBody_btnSaveHide" title="Lưu và tiếp tục sửa văn bản hiện tại" class="luuvaquaylai" />
        <asp:LinkButton ID="btnSaveAndNew" runat="server" CssClass="luuvathemmoi hidden-save" ToolTip="Lưu và tiếp tục thêm mới văn bản khác" Visible="true"
            Text="Lưu và thêm mới" ValidationGroup="G1" meta:resourcekey="btnSaveAndNew"
            OnClick="btnSaveAndNew_Click">
        </asp:LinkButton>
        <input type="button" name="ctl00$m_contentBody$btnSaveAndNewHide" value="Lưu và thêm mới" id="m_contentBody_btnSaveAndNewHide2" title="Lưu và tiếp tục thêm mới văn bản khác" class="luuvathemmoi" />
        <asp:LinkButton ID="lnkSaveSHVB" runat="server" CssClass="luuvathemmoi" ToolTip="Lưu văn bản với số hiệu văn bản trùng" Visible="false"
            Text="Lưu " ValidationGroup="G1" meta:resourcekey="lnkSaveSHVB"
            OnClick="lnkSaveSHVB_Click">
        </asp:LinkButton>

    </div>
    <script type="text/javascript">
        function isEmpty(str) {
            return (!str || 0 === str.length);
        }
        function isStrDateddMMyyyy(str) {
            var dateformat = /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/;
            // Match the date format through regular expression  
            if (str.match(dateformat)) {
                //Test which seperator is used '/' or '-'  
                var opera1 = str.split('/');
                var opera2 = str.split('-');
                lopera1 = opera1.length;
                lopera2 = opera2.length;
                // Extract the string into month, date and year  
                if (lopera1 > 1) {
                    var pdate = str.split('/');
                }
                else if (lopera2 > 1) {
                    var pdate = str.split('-');
                }
                var dd = parseInt(pdate[0]);
                var mm = parseInt(pdate[1]);
                var yy = parseInt(pdate[2]);
                // Create list of days of a month [assume there is no leap year by default]  
                var ListofDays = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
                if (mm == 1 || mm > 2) {
                    if (dd > ListofDays[mm - 1]) {
                        return false;
                    }
                }
                if (mm == 2) {
                    var lyear = false;
                    if ((!(yy % 4) && yy % 100) || !(yy % 400)) {
                        lyear = true;
                    }
                    if ((lyear == false) && (dd >= 29)) {
                        return false;
                    }
                    if ((lyear == true) && (dd > 29)) {
                        return false;
                    }
                }
            }
            else {
                return false;
            }
            return true;
        }
        function validDocsEditForm() {
            try {
                var focusInput = null;
                var txtDocName = $('#<%:txtDocName.ClientID%>');
                var rfvDocName = $('#<%:rfvDocName.ClientID%>');
                if (isEmpty(txtDocName.val())) {
                    rfvDocName.css({ 'display': 'inline' });
                    if (focusInput == null) {
                        focusInput = txtDocName;
                    }
                } else {
                    rfvDocName.hide();
                }
                var ddlDocTypes = $('#<%:ddlDocTypes.ClientID%>');
                var CompareValidator1 = $('#<%:CompareValidator1.ClientID%>');
                if (ddlDocTypes.val() == 0) {
                    CompareValidator1.css({ 'display': 'inline' });
                    if (focusInput == null) {
                        focusInput = ddlDocTypes;
                    }
                } else {
                    CompareValidator1.hide();
                }
                var txtIssueDate = $('#<%:txtIssueDate.ClientID%>');
                var rfvIssueDate = $('#<%:rfvIssueDate.ClientID%>');
                var CompareValidator2 = $('#<%:CompareValidator2.ClientID%>');
                if (isEmpty(txtIssueDate.val())) {
                    rfvIssueDate.css({ 'display': 'inline' });
                    if (focusInput == null) {
                        focusInput = txtIssueDate;
                    }
                } else if (!isStrDateddMMyyyy(txtIssueDate.val())) {
                    CompareValidator2.css({ 'display': 'inline' });
                    if (focusInput == null) {
                        focusInput = txtIssueDate;
                    }
                } else {
                    rfvIssueDate.hide();
                    CompareValidator2.hide();
                }
                var txtGazetteDate = $('#<%:txtGazetteDate.ClientID%>');
                var CompareValidator5 = $('#<%:CompareValidator5.ClientID%>');
                if (!isEmpty(txtGazetteDate.val())) {
                    if (!isStrDateddMMyyyy(txtGazetteDate.val())) {
                        CompareValidator5.css({ 'display': 'inline' });
                        if (focusInput == null) {
                            focusInput = txtGazetteDate;
                        }
                    } else {
                        CompareValidator5.hide();
                    }
                }

                var txtOrgans = $('#<%:txtOrgans.ClientID%>');
                var rfvOrgans = $('#<%:rfvOrgans.ClientID%>');
                if (isEmpty(txtOrgans.val())) {
                    rfvOrgans.css({ 'display': 'inline' });
                    if (focusInput == null) {
                        focusInput = txtOrgans;
                    }
                } else {
                    rfvOrgans.hide();
                }

                var txtDocIdentity = $('#<%:txtDocIdentity.ClientID%>');
                var rfvDocIdentity = $('#<%:rfvDocIdentity.ClientID%>');
                if (isEmpty(txtDocIdentity.val())) {
                    rfvDocIdentity.css({ 'display': 'inline' });
                    if (focusInput == null) {
                        focusInput = txtDocIdentity;
                    }
                } else {
                    rfvDocIdentity.hide();
                }
                var ddlDocGroups = $('#<%:ddlDocGroups.ClientID%>');
                var txtEffectDate = $('#<%:txtEffectDate.ClientID%>');
                var RequiredFieldValidator1 = $('#<%:RequiredFieldValidator1.ClientID%>');
                var CompareValidator3 = $('#<%:CompareValidator3.ClientID%>');
                if (ddlDocGroups.length && ddlDocGroups.val() != '8') {
                    if (isEmpty(txtEffectDate.val())) {
                        RequiredFieldValidator1.css({ 'display': 'inline' });
                        if (focusInput == null) {
                            focusInput = txtEffectDate;
                        }
                    } else if (!isStrDateddMMyyyy(txtEffectDate.val())) {
                        CompareValidator3.css({ 'display': 'inline' });
                        if (focusInput == null) {
                            focusInput = txtEffectDate;
                        }
                    } else {
                        RequiredFieldValidator1.hide();
                        CompareValidator3.hide();
                    }

                    var txtFields = $('#<%:txtFields.ClientID%>');
                    var rfvFields = $('#<%:rfvFields.ClientID%>');
                    if (isEmpty(txtFields.val())) {
                        rfvFields.css({ 'display': 'inline' });
                        if (focusInput == null) {
                            focusInput = txtFields;
                        }
                    } else {
                        rfvFields.hide();
                    }

                    var txtSigners = $('#<%:txtSigners.ClientID%>');
                    var rfvSigners = $('#<%:rfvSigners.ClientID%>');
                    if (isEmpty(txtSigners.val())) {
                        rfvSigners.css({ 'display': 'inline' });
                        if (focusInput == null) {
                            focusInput = txtSigners;
                        }
                    } else {
                        rfvSigners.hide();
                    }
                }
                else {
                    if (!isEmpty(txtEffectDate.val()) && !isStrDateddMMyyyy(txtEffectDate.val())) {
                        CompareValidator3.css({ 'display': 'inline' });
                        if (focusInput == null) {
                            focusInput = txtEffectDate;
                        }
                    } else {
                        RequiredFieldValidator1.hide();
                        RequiredFieldValidator1.css({ 'display': 'none' });
                        CompareValidator3.hide();
                        CompareValidator3.css({ 'display': 'none' });
                    }
                }
                var txtExpireDate = $('#<%:txtExpireDate.ClientID%>');
                var CompareValidator4 = $('#<%:CompareValidator4.ClientID%>');
                if (!isEmpty(txtExpireDate.val()) && !isStrDateddMMyyyy(txtExpireDate.val())) {
                    CompareValidator4.css({ 'display': 'inline' });
                    if (focusInput == null) {
                        focusInput = txtExpireDate;
                    }
                } else {
                    CompareValidator4.hide();
                }
                if (focusInput != null) {
                    focusInput.focus();
                    return false;
                }
            } catch (ex) {
            }

            return true;
        }
        //Check văn bản trùng số hiệu + ngày ban hành
        function actionCheckDataAfterSave(btnSaveHide, idSaveShow) {
            try {
                if (!validDocsEditForm()) {
                    return false;
                }
                if (btnSaveHide != null && idSaveShow != null) {
                    try {
                        var docId = '<%:DocId%>';
                        if (!btnSaveHide.hasClass('dangrequestcheck')) {
                            btnSaveHide.addClass('dangrequestcheck');
                        }
                        if (docId != null && docId.length && docId != '0') {
                            if (btnSaveHide.hasClass('dangrequestcheck')) {
                                btnSaveHide.removeClass('dangrequestcheck');
                            }
                            $('#' + idSaveShow)[0].click();
                        } else {
                            var a = document.getElementById('<%=txtDocIdentity.ClientID %>').value;
                            var b = document.getElementById('<%=txtIssueDate.ClientID %>').value;
                            if (a.length && b.length) {
                                if (btnSaveHide.data('running')) {
                                    return;
                                }
                                btnSaveHide.data('running', true);
                                $.ajax({
                                    url: "Docs_Valid_Identity_IssueDate.aspx/checkData",
                                    type: 'POST',
                                    data: '{Identity_:"' + a + '", IssueDate_:"' + b + '", DocGroupId_: "0" }',
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    async: true,
                                    success: function (result) {
                                        btnSaveHide.data('running', false);
                                        if (result != null && result.d == 'true') {
                                            if (confirm("Văn bản có số hiệu: " + a + "\nĐã tồn tại bạn có tiếp tục lưu không?")) {
                                                if (btnSaveHide.hasClass('dangrequestcheck')) {
                                                    btnSaveHide.removeClass('dangrequestcheck');
                                                }
                                                $('#' + idSaveShow)[0].click();
                                            } else {
                                                if (btnSaveHide.hasClass('dangrequestcheck')) {
                                                    btnSaveHide.removeClass('dangrequestcheck');
                                                }
                                            }
                                        } else {
                                            if (btnSaveHide.hasClass('dangrequestcheck')) {
                                                btnSaveHide.removeClass('dangrequestcheck');
                                            }
                                            $('#' + idSaveShow)[0].click();
                                        }
                                    }
                                });
                            } else {
                                if (btnSaveHide.hasClass('dangrequestcheck')) {
                                    btnSaveHide.removeClass('dangrequestcheck');
                                }
                                $('#' + idSaveShow)[0].click();
                            }

                        }
                    } catch (ex) {
                        console.log(ex);
                        if (btnSaveHide.hasClass('dangrequestcheck')) {
                            btnSaveHide.removeClass('dangrequestcheck');
                        }
                        if (btnSaveHide.data('running')) {
                            btnSaveHide.data('running', false);
                        }
                    }
                }
            } catch (ex) {
                console.log(ex);
            }
        }
        $('#m_contentBody_btnSaveHide').off('click').on('click', function (evt) {
            evt.preventDefault();
            actionCheckDataAfterSave($(this), '<%:btnSave.ClientID%>');
        });
        $('#m_contentBody_btnSaveAndNewHide2').off('click').on('click', function (evt) {
            evt.preventDefault();
            actionCheckDataAfterSave($(this), '<%:btnSaveAndNew.ClientID%>');
        });
    </script>
</asp:Content>

