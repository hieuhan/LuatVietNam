<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="VietNamStandardsEdit.aspx.cs" Inherits="Admin_VietNamStandardsEdit" %>
<%@ Import Namespace="ICSoft.CMSLib" %>
<%@ Import Namespace="ICSoft.LawDocsLib" %>
<%@ Import Namespace="ICSoft.HelperLib" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
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
    function fncCreateSelectOption(ele)
    {
        var objSelect = ele;
        var Item = new Option("Chọn nguồn file", "0");
        objSelect.options[objSelect.length] = Item;

        Item = new Option("Incom", "18");
        objSelect.options[objSelect.length] = Item;

        Item = new Option("TTX", "19");
        objSelect.options[objSelect.length] = Item;

        Item = new Option("ASEM", "20");
        objSelect.options[objSelect.length] = Item;

        Item = new Option("Thu thập", "21");
        objSelect.options[objSelect.length] = Item;

        Item = new Option("Convert", "22");
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

    $(function () {
        createEditor('vi', <%=txtDocContent.ClientID%>);
    });

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
        .ui-autocomplete
        {
            max-height: 150px;
            overflow-y: auto; /* prevent horizontal scrollbar */
            overflow-x: hidden; /* add padding to account for vertical scrollbar */
            padding-right: 20px;
        }
        /* IE 6 doesn't support max-height
	     * we use height instead, but this forces the menu to always be this tall
	     */
        * html .ui-autocomplete
        {
            height: 150px;
        }
        .style2
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: small;
        }
        .style3
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 10pt;
        }
    </style>
    <div style="text-align:center"><asp:Label ID="lblMessage" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label></div>
    <table cellpadding="3" cellspacing="0" style="width: 90%; font-weight: lighter">
        <tr>
            <asp:ValidationSummary ID="ValidationSummary" runat="server" Visible="false"
                HeaderText="Bạn vui lòng kiểm tra lại thông tin sau:"
                ShowMessageBox="true"
                DisplayMode="BulletList"
                ShowSummary="false"
                ValidationGroup="G1"
                BackColor="Snow"
                Width="450"
                ForeColor="Red"
                Font-Size="X-Large"
                Font-Italic="true" />
        </tr>
        <tr runat="server" visible="false">
             <td style="width:15%;">
                <asp:Label ID="lblLanguage" runat="server" meta:resourcekey="lblLanguage"
                    Text="Ngôn ngữ:"></asp:Label>
                 </td>
            <td style="width:30%;">
                <asp:DropDownList ID="ddlLanguage" runat="server" DataTextField="LanguageDesc" CssClass="userselect" Enabled="false"
                    DataValueField="LanguageId" Width="100%">
                </asp:DropDownList>
            </td><td style="width:15%;"> </td>
            <td> </td>
        </tr>
        <tr>
            <td style="width: 15%">
                <asp:Label ID="lblDocName" runat="server" Text="Tên tiêu chuẩn:"></asp:Label>
                <span class="icon-required"></span>
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtDocName" runat="server" Width="99%"
                    CssClass="tukhoatimekiem" Height="50px" TextMode="MultiLine"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="rfvDocName" runat="server"
                    ControlToValidate="txtDocName"
                    Display="Dynamic"
                    ErrorMessage="Tên văn bản"
                    ForeColor="Red"
                    Text="Bạn cần nhập vào Tên tiêu chuẩn"
                    SetFocusOnError="true"
                    ValidationGroup="G1"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 15%">
                <asp:Label ID="lblDocTypes" runat="server" Text="Loại văn bản:" meta:resourcekey="lblDocTypes"></asp:Label>
            </td>
            <td style="width: 35%">
                <asp:DropDownList ID="ddlDocTypes" runat="server" CssClass="userselect"
                    DataTextField="DocTypeDesc" DataValueField="DocTypeId"
                    Width="250px">
                </asp:DropDownList></td>
            <td style="width: 15%">
                <asp:Label ID="lblDocIdentity" runat="server" Text="Số hiệu:"></asp:Label>
                <span class="icon-required"></span>
            </td>    
            <td>
                <asp:TextBox ID="txtDocIdentity" runat="server" Width="240px"
                    CssClass="tukhoatimekiem"></asp:TextBox>
                <asp:LinkButton ID="lnkSearchIdentity" runat="server" Visible="true" class="themmoi" Text="Tìm VB  "
                    meta:resourcekey="lnkSearchIdentity" OnClick="lnkSearchIdentity_Click">
                </asp:LinkButton>
                <br />
                <asp:RequiredFieldValidator ID="rfvDocIdentity" Text="Bạn cần nhập vào Số hiệu" runat="server" Display="Dynamic" SetFocusOnError="true"
                    ErrorMessage="Số hiệu văn bản" ValidationGroup="G1"
                    ControlToValidate="txtDocIdentity" ForeColor="Red"></asp:RequiredFieldValidator>

            </td>
        </tr>
        <tr>
            <td style="width: 15%">
                <asp:Label ID="lblIssueDate" runat="server" Text="Ngày ban hành:" meta:resourcekey="lblIssueDate"></asp:Label>
                <span class="icon-required"></span>                
            </td>
            <td style="width: 35%">
                <asp:TextBox ID="txtIssueDate" runat="server" Width="240px" CssClass="tukhoatimekiem"></asp:TextBox><a class="help-lnk" href="javascript:void(0)" title="Chọn hoặc nhập ngày tháng theo định dang dd/MM/yyyy" ><span class="aui-icon aui-icon-small aui-iconfont-help">?</span>
                  </a>  (dd/MM/yyyy)
                <br />
                <span class="node-help">                
                Bắt buộc nhập 1 trong hai ô: Ngày ban hành hoặc Năm ban hành <strong>;</strong></span>
                <br />
                <asp:CustomValidator ID="rfvIssueDate" runat="server" ErrorMessage="Bạn cần nhập vào ngày ban hành hoặc năm ban hành" ValidationGroup="G1" ForeColor="Blue" OnServerValidate="rfvIssueDate_ServerValidate" ControlToValidate="txtIssueDate" />
                
               <%-- <asp:RequiredFieldValidator ID="rfvIssueDate" Text="Bạn cần nhập vào ngày ban hành hoặc năm ban hành" runat="server" Display="Dynamic" SetFocusOnError="true"
                    ErrorMessage="Ngày ban hành" ValidationGroup="G1"
                    ControlToValidate="txtIssueDate" ForeColor="Red"></asp:RequiredFieldValidator>--%><br />
                <asp:CompareValidator ID="cvIssueDate" runat="server" ErrorMessage="Ngày bạn nhập không hợp lệ" ControlToValidate="txtIssueDate" ForeColor="Red" SetFocusOnError="true" Display="Dynamic" Operator="DataTypeCheck" Type="Date" ValidationGroup="G1"></asp:CompareValidator>
                
            </td>
            <td style="width: 15%">
                <asp:Label ID="lblFee" runat="server" Text="Năm ban hành:" meta:resourcekey="lblFee"></asp:Label>
                <span class="icon-required"></span>
               </td> 
            <td style="width: 35%">
                <asp:TextBox type="number" ID="txtFee" runat="server" Width="240px" CssClass="tukhoatimekiem"></asp:TextBox> 
                <br />
                <asp:RangeValidator ID="cvFee" ErrorMessage="Năm bạn nhập không hợp lệ" runat="server" Type="Integer"  MinimumValue="1500" MaximumValue="2500" ControlToValidate="txtFee" SetFocusOnError="true"  Display="Dynamic"  ForeColor="Red" Operator="DataTypeCheck" ValidationGroup="G1"/> 
                Bắt buộc nhập 1 trong hai ô: Ngày ban hành hoặc Năm ban hành <strong>;</strong></span> 
                <br />
                <%--<asp:RequiredFieldValidator ID="rfvtxtFee" runat="server" ControlToValidate="txtFee" MaximumValue="9999999" MinimumValue="0"
                    ValidationGroup="G1" ForeColor="Red" ErrorMessage="Năm ban hành" Text="Năm ban hành phải là số"/>--%>
            </td>
            
        </tr>
        <tr>
            <td style="width: 15%">
                <asp:Label ID="lblGazetteNumber" runat="server" Text="Số công báo:"
                    meta:resourcekey="lblGazetteNumber"></asp:Label>
            </td>
            <td style="width: 35%">
                <asp:TextBox ID="txtGazetteNumber" runat="server" Width="240px"
                    CssClass="tukhoatimekiem"></asp:TextBox></td>
            <td style="width: 15%">
                <asp:Label ID="lblEffectDate" runat="server" Text="Ngày có hiệu lực:" meta:resourcekey="lblEffectDate"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtEffectDate" runat="server" Width="240px" CssClass="tukhoatimekiem"></asp:TextBox>
                <a class="help-lnk" href="javascript:void(0)" title="Chọn hoặc nhập ngày tháng theo định dang dd/MM/yyyy" ><span class="aui-icon aui-icon-small aui-iconfont-help">?</span></a>
                (dd/MM/yyyy)
                <br />
                <asp:CompareValidator ID="cvEffectDate" runat="server" ErrorMessage="Ngày bạn nhập không hợp lệ" ControlToValidate="txtEffectDate" ForeColor="Red" SetFocusOnError="true" Display="Dynamic" Operator="DataTypeCheck" Type="Date" ValidationGroup="G1"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 15%">
                <asp:Label ID="lblGazetteDate" runat="server" Text="Ngày công báo:"
                    meta:resourcekey="lblGazetteDate"></asp:Label>
            </td>
            <td style="width: 35%">
                <asp:TextBox ID="txtGazetteDate" runat="server" Width="240px"
                    CssClass="tukhoatimekiem"></asp:TextBox>
                <a class="help-lnk" href="javascript:void(0)" title="Chọn hoặc nhập ngày tháng theo định dang dd/MM/yyyy" ><span class="aui-icon aui-icon-small aui-iconfont-help">?</span></a>(dd/MM/yyyy)
                <br />
                <asp:CompareValidator ID="cvGazetteDate" runat="server" ErrorMessage="Ngày bạn nhập không hợp lệ" ControlToValidate="txtGazetteDate" ForeColor="Red" SetFocusOnError="true" Display="Dynamic" Operator="DataTypeCheck" Type="Date" ValidationGroup="G1"></asp:CompareValidator>
            </td>
            <td style="width: 15%">
                <asp:Label ID="lblExpireDate" runat="server" Text="Ngày hết hiệu lực:" meta:resourcekey="lblExpireDate"></asp:Label>

            </td>
            <td>
                <asp:TextBox ID="txtExpireDate" runat="server" Width="240px" CssClass="tukhoatimekiem"></asp:TextBox>
                <a class="help-lnk" href="javascript:void(0)" title="Chọn hoặc nhập ngày tháng theo định dang dd/MM/yyyy" ><span class="aui-icon aui-icon-small aui-iconfont-help">?</span></a>
                (dd/MM/yyyy)
                <br />
                <asp:CompareValidator ID="cvExpireDate" runat="server" ErrorMessage="Ngày bạn nhập không hợp lệ" ControlToValidate="txtExpireDate" ForeColor="Red" SetFocusOnError="true" Display="Dynamic" Operator="DataTypeCheck" Type="Date" ValidationGroup="G1"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 15%">
                <asp:Label ID="lblSigners" runat="server" Text="Người ký:"
                    meta:resourcekey="lblSigners"></asp:Label>
               <%-- <span class="icon-required"></span>--%>
                
            </td>
            <td style="width: 35%">
                <asp:TextBox ID="txtSigners" runat="server" Height="85px" TextMode="MultiLine"
                    Width="90%"></asp:TextBox>
                <br />            
                <span class="node-help">                
                Gõ vào ký tự rồi chọn từ danh sách gợi ý, danh sách phân cách bằng dấu <strong>;</strong></span>
                <br />
                <%--<asp:RequiredFieldValidator ID="rfvSigners" Text="Bạn cần nhập vào Người ký" runat="server" Display="Dynamic" SetFocusOnError="true"
                    ErrorMessage="Người ký" ValidationGroup="G1"
                    ControlToValidate="txtSigners" ForeColor="Red"></asp:RequiredFieldValidator>--%>
            </td>
            <td style="width: 15%">
                <asp:Label ID="lblFields" runat="server" Text="Lĩnh vực:"
                    meta:resourcekey="lblFields"></asp:Label>
                <%--<span class="icon-required"></span>--%>
            </td>
            <td>
                <asp:TextBox ID="txtFields" runat="server" Height="85px" TextMode="MultiLine"
                    Width="90%"></asp:TextBox>
                <br />            
                <span class="node-help">                
                Gõ vào ký tự rồi chọn từ danh sách gợi ý, danh sách phân cách bằng dấu <strong>;</strong></span>
                <%--<br />
                <asp:RequiredFieldValidator ID="rfvFields" Text="Bạn cần nhập vào Lĩnh vực" runat="server" Display="Dynamic" SetFocusOnError="true"
                    ErrorMessage="Lĩnh vực" ValidationGroup="G1"
                    ControlToValidate="txtFields" ForeColor="Red"></asp:RequiredFieldValidator>--%>
            </td>
            
        </tr>
        <tr>
            <td style="width: 15%">
                <asp:Label ID="lblOrgans" runat="server" Text="CQBH:"
                    meta:resourcekey="lblOrgans"></asp:Label>
                <%--<span class="icon-required"></span>--%>
            </td>
            <td>
                <asp:TextBox ID="txtOrgans" runat="server" Height="85px" TextMode="MultiLine"
                    Width="90%"></asp:TextBox>
                <br />            
                <span class="node-help">                
                Gõ vào ký tự rồi chọn từ danh sách gợi ý, danh sách phân cách bằng dấu <strong>;</strong></span>
                <br />
                <%--<asp:RequiredFieldValidator ID="rfvOrgans" Text="Bạn cần nhập vào CQBH" runat="server" Display="Dynamic" SetFocusOnError="true"
                    ErrorMessage="Cơ quan ban hành" ValidationGroup="G1"
                    ControlToValidate="txtOrgans" ForeColor="Red"></asp:RequiredFieldValidator>--%>
            </td>
            
            <td class="style2">
                <asp:Label ID="lblProvices" runat="server" Text="Tỉnh thành :" meta:resourcekey="lblProvices"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtProvices" runat="server" Height="85px" TextMode="MultiLine"
                    Width="90%"></asp:TextBox>
                <br />
                <span class="node-help">Gõ vào ký tự rồi chọn từ danh sách gợi ý, danh sách phân cách bằng dấu <strong>;</strong></span>
            </td>
        </tr>
        <tr>
            <td style="width: 15%">
                <asp:Label ID="lblUseStatus" runat="server" Text="Trạng thái văn bản:"
                    meta:resourcekey="lblUseStatus"></asp:Label>
            </td>
            <td style="width: 35%">
                <asp:DropDownList ID="ddlUseStatus" runat="server" CssClass="userselect"
                    DataTextField="UseStatusDesc" DataValueField="UseStatusId"
                    Width="250px">
                </asp:DropDownList></td>
            
            <td style="width: 10%">
                <asp:Label ID="lblKeywords" runat="server" Text="Từ khóa:" 
                    meta:resourcekey="lblKeywords"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtKeywords" runat="server" Height="85px" TextMode="MultiLine"
                    Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr>
                <td style="width: 15%">
                <asp:Label ID="lblEffectStatus" runat="server" Text="Trạng thái hiệu lực:"
                    meta:resourcekey="lblEffectStatus"></asp:Label>
            </td>
            <td style="width: 35%">
                <asp:DropDownList ID="ddlEffectStatus" runat="server" CssClass="userselect"
                    DataTextField="EffectStatusDesc" DataValueField="EffectStatusId"
                    Width="250px">
                </asp:DropDownList></td>
            <td style="width: 15%"></td>
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
            <td style="width: 15%">
                <asp:Label ID="Label1" runat="server" Text="Trạng thái:"
                    meta:resourcekey="lblReviewStatus"></asp:Label>
            </td>
            <td style="width: 35%">
                <asp:DropDownList ID="ddlReviewStatus" runat="server" CssClass="userselect"
                    DataTextField="ReviewStatusDesc" DataValueField="ReviewStatusId"
                    Width="100%">
                </asp:DropDownList></td>
        </tr>
        <tr style="display:none;">
            <td style="width: 15%"></td>
            <td style="width: 35%"></td>
            <td style="width: 15%">
                <asp:Label ID="lblDocFileName" runat="server" Text="Tên tập tin:"
                    meta:resourcekey="lblDocFileName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDocFileName" runat="server" Width="98%"
                    CssClass="tukhoatimekiem"></asp:TextBox></td>
        </tr>
        <%-- End --%>
        
        <tr>
            <td>
                <asp:Label ID="lblFileUpload" runat="server" meta:resourcekey="lblFileUpload"
                    Text="File Upload:"></asp:Label>
            </td>
            <td align="left" colspan="3">
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
    <div style="background-color: #FFFFFF; bottom: 0px;  padding: 8px;  position: fixed;   right: 1px;   text-align: right;  width: 100%;">
        <asp:LinkButton ID="btnBack" runat="server" CssClass="quaylai" 
                    Text="Quay lại" meta:resourcekey="btnBack" 
            onclick="btnBack_Click" >
        </asp:LinkButton>
        <input type="button" name="ctl00$m_contentBody$btnSaveHide" value="Lưu thông tin" id="m_contentBody_btnSaveHide" title="Lưu và tiếp tục sửa văn bản hiện tại" class="luuvaquaylai" />
        <input type="button" name="ctl00$m_contentBody$btnSaveAndNewHide" value="Lưu và thêm mới" id="m_contentBody_btnSaveAndNewHide2" title="Lưu và tiếp tục thêm mới văn bản khác" class="luuvathemmoi"/>
        <asp:LinkButton ID="btnSave" runat="server" CssClass="luuvaquaylai hidden-save" ToolTip="Lưu và tiếp tục sửa văn bản hiện tại" 
            Text="Lưu thông tin" meta:resourcekey="btnSave" ValidationGroup="G1"  onclick="btnSave_Click">
        </asp:LinkButton>
        <asp:LinkButton ID="btnSaveAndNew" runat="server" CssClass="luuvathemmoi hidden-save" ToolTip="Lưu và thêm mới văn bản khác" 
                    Text="Lưu và thêm mới"  ValidationGroup="G1" meta:resourcekey="btnSaveAndNew" 
            onclick="btnSaveAndNew_Click"  >
        </asp:LinkButton>
        <asp:LinkButton ID="lnkSaveSHVB" runat="server" CssClass="luuvathemmoi" ToolTip="Lưu văn bản với số hiệu văn bản trùng"  Visible="false"
                    Text="Lưu "  ValidationGroup="G1" meta:resourcekey="lnkSaveSHVB" 
            onclick="lnkSaveSHVB_Click"  >
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

                var txtIssueDate = $('#<%:txtIssueDate.ClientID%>');
                var txtFee = $('#<%:txtFee.ClientID%>');
                var rfvIssueDate = $('#<%:rfvIssueDate.ClientID%>');
                var cvIssueDate = $('#<%:cvIssueDate.ClientID%>');
                if (isEmpty(txtIssueDate.val()) && isEmpty(txtFee.val())) {
                    rfvIssueDate.css({ 'display': 'inline','visibility':'visible'}); 
                    if (focusInput == null) {
                        focusInput = txtIssueDate;
                    }
                } else if (!isEmpty(txtIssueDate.val())) {
                    if(!isStrDateddMMyyyy(txtIssueDate.val())){
                        cvIssueDate.css({ 'display': 'inline' });
                        rfvIssueDate.hide();
                        if (focusInput == null) {
                            focusInput = txtIssueDate;
                        }
                    }
                } else {
                    rfvIssueDate.hide();
                    cvIssueDate.hide();
                }

                var txtGazetteDate = $('#<%:txtGazetteDate.ClientID%>');
                var cvGazetteDate = $('#<%:cvGazetteDate.ClientID%>');
                if (!isEmpty(txtGazetteDate.val())) {
                    if (!isStrDateddMMyyyy(txtGazetteDate.val())) {
                        cvGazetteDate.css({ 'display': 'inline' });
                        if (focusInput == null) {
                            focusInput = txtGazetteDate;
                        }
                    } else {
                        cvGazetteDate.hide();
                    }
                }
                var txtEffectDate = $('#<%:txtEffectDate.ClientID%>');
                var cvEffectDate = $('#<%:cvEffectDate.ClientID%>');
                if (!isEmpty(txtEffectDate.val())) {
                    if (!isStrDateddMMyyyy(txtEffectDate.val())) {
                        cvEffectDate.css({ 'display': 'inline' });
                        if (focusInput == null) {
                            focusInput = txtEffectDate;
                        }
                    } else {
                        cvEffectDate.hide();
                    }
                } 
                var txtExpireDate = $('#<%:txtExpireDate.ClientID%>');
                var cvExpireDate = $('#<%:cvExpireDate.ClientID%>');
                if (!isEmpty(txtExpireDate.val()) && !isStrDateddMMyyyy(txtExpireDate.val())) {
                    cvExpireDate.css({ 'display': 'inline' });
                    if (focusInput == null) {
                        focusInput = txtExpireDate;
                    }
                } else {
                    cvExpireDate.hide();
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
                            if(b.length <= 0)
                            {
                                b = '01/01/'+document.getElementById('<%=txtFee.ClientID %>').value;
                            }
                            if (a.length && b.length) {
                                if (btnSaveHide.data('running')) {
                                    return;
                                }
                                btnSaveHide.data('running', true);
                                $.ajax({
                                    url: "Docs_Valid_Identity_IssueDate.aspx/checkData",
                                    type: 'POST',
                                    data: '{Identity_:"' + a + '", IssueDate_:"' + b + '", DocGroupId_: "3" }',
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

