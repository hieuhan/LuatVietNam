<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="DocsEditTTHC.aspx.cs" Inherits="Admin_DocsEdit" %>

<%@ Import Namespace="ICSoft.CMSLib" %>
<%@ Import Namespace="ICSoft.LawDocsLib" %>
<%@ Import Namespace="ICSoft.HelperLib" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" runat="Server">
    <script language="javascript" type="text/javascript">
        var StepCount = <%=StepCount%>;
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

            var input2 = document.createElement('input');
            input2.type = 'text';
            input2.setAttribute('placeholder', "Tên file");
            input2.name = "DocFileName" + uploadCount;
            uploads.appendChild(input2);

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
        function fncCreateSelectOption(ele)
        {
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
        function AddRow() {
            //Reference the GridView.
            var gridView = document.getElementById("<%=gvDocStep.ClientID %>");
 
            //Reference the TBODY tag.
            var tbody = gridView.getElementsByTagName("tbody")[0];
 
            //Reference the first row.
            var row = tbody.getElementsByTagName("tr")[1];
 
            //Check if row is dummy, if yes then remove.
            //if (row.getElementsByTagName("td")[0].innerHTML.replace(/\s/g, '') == "") {
            //    tbody.removeChild(row);
            //}
 
            //Clone the reference first row.
            row = row.cloneNode(true);
 
            //Add the Name value to first cell.
            StepCount += 1;
            var txtName = StepCount;
            SetValue(row, 0, "DocStepOrder", txtName);
 
            //Add the Country value to second cell.
            var txtCountry = "";
            SetValue2(row, 1, "DocStepContent", txtCountry);
            //set delete button
            row.getElementsByTagName("td")[2].innerHTML = "<a class='iconadmxoa' href='javascript:void(0)' onclick='DeleteRow(this)'></a>";
            //Add the row to the GridView.
            tbody.appendChild(row);
            return false;
        };
        function DeleteRow(obj) {
            //Reference the GridView.
    

    
            var gridView = document.getElementById("<%=gvDocStep.ClientID %>");
    
        //Reference the TBODY tag.
        var tbody = gridView.getElementsByTagName("tbody")[0];
 
        //Reference the first row.
        //var row = tbody.getElementsByTagName("tr");
        var row = $(obj).parents("tr").get(0);
        console.log($(obj).attr("class"));
        console.log(row);
        var index = tbody.getElementsByTagName("tr").length;
        console.log(index);
        //Check if row is dummy, if yes then remove.
        if(index <= 2){
            row.getElementsByTagName("td")[0].innerHTML = "<input type='text' name='DocStepOrder'>";
            row.getElementsByTagName("td")[1].innerHTML = "<input type='textare' name='DocStepContent' style='height: 35px; width: 100%;'>";
        }
        else
        {
            tbody.removeChild(row);
        }
    
        return false;
    };
 
    function SetValue(row, index, name, textbox) {
        //Reference the Cell and set the value.
        row.cells[index].innerHTML = "";
           
        //Create and add a Hidden Field to send value to server.
        var input = document.createElement("input");
        input.type = "text";
        input.name = name;
        input.style = "";
        input.value = textbox;
        row.cells[index].appendChild(input);
 
    }
    function SetValue2(row, index, name, textbox) {
        //Reference the Cell and set the value.
        row.cells[index].innerHTML = "";
           
        //Create and add a Hidden Field to send value to server.
        var input = document.createElement("input");
        input.type = "textare";
        input.name = name;
        input.style = "height:35px;width:100%;";
        input.value = textbox;
        row.cells[index].appendChild(input);
 
    }
    $(document).ready(function () {
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(InitializeRequest);
        prm.add_endRequest(EndRequest);
        AddUpload();
      <%--  $("#<%= txtIssueDate.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
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
        $(".OrganSelect")
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


    function InitializeRequest(sender, args) {
    }

    function EndRequest(sender, args) {
        $("#<%= txtIssueDate.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#<%= txtEffectDate.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#<%= txtGazetteDate.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#<%= txtExpireDate.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
    }
    function splitFile(){
        var files = document.getElementById("fileMultiple").files;

        for (var i = 0; i < files.length; i++)
        {
            console.log(files[i].name);
        }
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
    </style>

    <div style="text-align: center">
        <asp:Label ID="lblMessage" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
    </div>
    <table cellpadding="3" cellspacing="0" style="width: 90%; font-weight: lighter">
        <tr>
            <td style="width:15%">
                <asp:Label ID="lblDocName" runat="server" Text="Tên thủ tục:"
                    meta:resourcekey="lblDocName"></asp:Label>
                <span class="icon-required"></span>

                <asp:Label ID="lblDocTypes" runat="server" Text="Loại văn bản:" meta:resourcekey="lblDocTypes" Visible="false"></asp:Label>
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtDocName" runat="server" Width="96%"
                    CssClass="tukhoatimekiem" Height="30px" TextMode="MultiLine"></asp:TextBox><br />
                
                <asp:RequiredFieldValidator ID="rfvDocName" runat="server" Display="Dynamic" Text="Bạn cần nhập tên thủ tục hành chính"
                    ErrorMessage="Tên thủ tục" ValidationGroup="G1"
                    ControlToValidate="txtDocName" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlDocTypes" runat="server" Visible="false" CssClass="userselect uiselect"
                    DataTextField="DocTypeDesc" DataValueField="DocTypeId"
                    Width="92%">
                </asp:DropDownList>
                <asp:Label ID="lblLanguage" runat="server" meta:resourcekey="lblLanguage" Visible="false"
                    Text="Ngôn ngữ:"></asp:Label>
                <asp:DropDownList ID="ddlLanguage" runat="server" DataTextField="LanguageDesc" CssClass="userselect" Enabled="false"
                    DataValueField="LanguageId" Width="92%" Visible="false">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <div id="tblDocshowByIdentity" runat="server" visible="false">
                    <asp:DropDownList ID="ddlSelectDocs" runat="server" AutoPostBack="True"
                        CssClass="userselect" DataTextField="DocName" DataValueField="DocId"
                        OnSelectedIndexChanged="ddlSelectDocs_SelectedIndexChanged" Width="92%">
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
            <td style="width:15%;">
                <asp:Label ID="lblOrgans" runat="server" Text="CQBH:"
                    meta:resourcekey="lblOrgans"></asp:Label>
                <span class="icon-required"></span>
            </td>
            <td style="width:35%;">
                <asp:TextBox ID="txtOrgans" CssClass="OrganSelect" runat="server" Height="45px" TextMode="MultiLine"
                    Width="90%"></asp:TextBox><br /><span class="node-help">Gõ vào ký tự rồi chọn từ danh sách gợi ý, danh sách phân cách bằng dấu <strong>;</strong></span><br />
                
                <asp:RequiredFieldValidator ID="rfvOrgans" runat="server" Text="Bạn cần nhập vào cơ quan ban hành" Display="Dynamic"
                    ErrorMessage="Cơ quan ban hành" ValidationGroup="G1"
                    ControlToValidate="txtOrgans" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
            <td style="width:15%;">
                <asp:Label ID="lblDocIdentity" runat="server" Text="Số hồ sơ:"
                    meta:resourcekey="lblDocIdentity"></asp:Label>
                <span class="icon-required"></span>
                
            </td>
            <td style="width: 35%;">
                <asp:TextBox ID="txtDocIdentity" runat="server" Width="90%"
                    CssClass="tukhoatimekiem"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="rfvDocIdentity" runat="server" Text="Bạn cần nhập số hiệu văn bản" Display="Dynamic"
                    ErrorMessage="Số hồ sơ" ValidationGroup="G1"
                    ControlToValidate="txtDocIdentity" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:LinkButton ID="lnkSearchIdentity" runat="server" Visible="false" class="themmoi" Text="Tìm VB  "
                    meta:resourcekey="lnkSearchIdentity" OnClick="lnkSearchIdentity_Click">
                </asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="lblFields" runat="server" Text="Lĩnh vực:" meta:resourcekey="lblFields"></asp:Label>
                <span class="icon-required"></span>
                
            </td>
            <td>
                <asp:TextBox ID="txtFields" runat="server" Height="45px" TextMode="MultiLine"
                    Width="90%"></asp:TextBox>
                <br /><span class="node-help">Gõ vào ký tự rồi chọn từ danh sách gợi ý, danh sách phân cách bằng dấu <strong>;</strong></span><br />
                <asp:RequiredFieldValidator ID="rfvFields" runat="server" Text="Bạn cần nhập vào lĩnh vực" Display="Dynamic"
                    ErrorMessage="Lĩnh vực" ValidationGroup="G1"
                    ControlToValidate="txtFields" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Đối tượng thực hiện:"></asp:Label>
                <span class="icon-required"></span>
                <asp:Label ID="lblGazetteDate" runat="server" Text="Ngày công báo:" Visible="false"
                    meta:resourcekey="lblGazetteDate"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txDocObject" runat="server" CssClass="tukhoatimekiem" Width="90%" TextMode="MultiLine" Height="45px"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="rfvDocObject" runat="server" Text="Bạn cần nhập vào đối tượng thực hiện" Display="Dynamic"
                    ErrorMessage="Đối tượng thực hiện" ValidationGroup="G1"
                    ControlToValidate="txDocObject" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:TextBox ID="txtGazetteDate" runat="server" Width="90%" Visible="false"
                    CssClass="tukhoatimekiem"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="Label6" runat="server" Text="Cơ quan trực tiếp thực hiện:"></asp:Label>
                <%--<asp:RequiredFieldValidator ID="rfvOrgans5" runat="server" Text="(*)" Display="Dynamic" 
                ErrorMessage="Cơ quan trực tiếp thực hiện" ValidationGroup="G1" 
                ControlToValidate="txtOrgans5" ForeColor="Red"></asp:RequiredFieldValidator>--%>
            </td>
            <td>
                <asp:TextBox ID="txtOrgans5" CssClass="OrganSelect" runat="server" Height="45px" TextMode="MultiLine"
                    Width="90%"></asp:TextBox>
                <br /><span class="node-help">Gõ vào ký tự rồi chọn từ danh sách gợi ý, danh sách phân cách bằng dấu <strong>;</strong></span>
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Phạm vi áp dụng:"></asp:Label>
                <asp:Label ID="lblGazetteNumber" runat="server" Text="Số công báo:" Visible="false"
                    meta:resourcekey="lblGazetteNumber"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlGrantLevelId" runat="server" CssClass="userselect"
                    DataTextField="GrantLevelDesc" DataValueField="GrantLevelId"
                    Width="92%">
                </asp:DropDownList>
                <asp:TextBox ID="txDocScopes" runat="server" CssClass="tukhoatimekiem" Width="90%" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtGazetteNumber" runat="server" Width="90%" Visible="false"
                    CssClass="tukhoatimekiem"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="Label7" runat="server" Text="Cơ quan có thẩm quyền quyết định:"></asp:Label>
                <%--<asp:RequiredFieldValidator ID="rfvOrgans6" runat="server" Text="(*)" Display="Dynamic" 
                ErrorMessage="Cơ quan có thầm quyền quyết định" ValidationGroup="G1" 
                ControlToValidate="txtOrgans6" ForeColor="Red"></asp:RequiredFieldValidator>--%>
            </td>
            <td>
                <asp:TextBox ID="txtOrgans6" CssClass="OrganSelect" runat="server" Height="45px" TextMode="MultiLine"
                    Width="90%"></asp:TextBox>
                <br /><span class="node-help">Gõ vào ký tự rồi chọn từ danh sách gợi ý, danh sách phân cách bằng dấu <strong>;</strong></span>
            </td>
             <td>
                <asp:Label ID="Label3" runat="server" Text="Cách thức thực hiện:"></asp:Label>
                 <span class="icon-required"></span>
            </td>
            <td>
                <asp:TextBox ID="txPerformMethod" runat="server" CssClass="tukhoatimekiem" Width="90%" Height="45px" TextMode="MultiLine"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="rfvPerformMethod" runat="server" Text="Bạn cần nhập cách thức thực hiện" Display="Dynamic"
                    ErrorMessage="Cách thức thực hiện" ValidationGroup="G1"
                    ControlToValidate="txPerformMethod" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
             <td>
                <asp:Label ID="Label5" runat="server" Text="Kết quả thực hiện:"></asp:Label>
                <%--<asp:RequiredFieldValidator ID="rfvtxResult" runat="server" Text="(*)" Display="Dynamic"
                    ErrorMessage="Kết quả thực hiện" ValidationGroup="G1"
                    ControlToValidate="txResult" ForeColor="Red"></asp:RequiredFieldValidator>--%>
            </td>
            <td>
                <asp:TextBox ID="txResult" runat="server" CssClass="tukhoatimekiem" Width="90%" Height="45px" TextMode="MultiLine"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Thời gian giải quyết:"></asp:Label>
                <%--<asp:RequiredFieldValidator ID="rfvSettlementTime" runat="server" Text="(*)" Display="Dynamic" 
                ErrorMessage="Thời gian giải quyết" ValidationGroup="G1" 
                ControlToValidate="txSettlementTime" ForeColor="Red"></asp:RequiredFieldValidator>--%>
            </td>
            <td>
                <asp:TextBox ID="txSettlementTime" runat="server" CssClass="tukhoatimekiem" Width="90%" Height="45px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblIssueDate" runat="server" Text="Ngày ban hành:" meta:resourcekey="lblIssueDate"></asp:Label>
                <%--<asp:RequiredFieldValidator ID="rfvIssueDate" runat="server" Text="(*)" Display="Dynamic"
                    ErrorMessage="Ngày ban hành" ValidationGroup="G1"
                    ControlToValidate="txtIssueDate" ForeColor="Red"></asp:RequiredFieldValidator>--%>
            </td>
            <td>
                <asp:TextBox ID="txtIssueDate" runat="server" Width="90%" CssClass="tukhoatimekiem"></asp:TextBox>
                <a class="help-lnk" href="javascript:void(0)" title="Chọn hoặc nhập ngày tháng theo định dạng dd/MM/yyyy" >
<span class="aui-icon aui-icon-small aui-iconfont-help">?</span></a>(dd/MM/yyyy)<br />
<asp:CompareValidator ID="cvtxtIssueDate" runat="server" ErrorMessage="Ngày ban hành nhập không đúng định dạng" ControlToValidate="txtIssueDate" 
ForeColor="Red" SetFocusOnError="true" Display="Dynamic" Operator="DataTypeCheck" Type="Date" ValidationGroup="G1"></asp:CompareValidator>

            </td>
            <td>
                <asp:Label ID="lblEffectDate" runat="server" Text="Ngày có hiệu lực:" meta:resourcekey="lblEffectDate"></asp:Label>
                <%--<asp:RequiredFieldValidator ID="rfvEffectDate" runat="server" Text="(*)" Display="Dynamic" 
                ErrorMessage="Ngày có hiệu lực" ValidationGroup="G1" 
                ControlToValidate="txtEffectDate" ForeColor="Red"></asp:RequiredFieldValidator>--%>
            </td>
            <td>
                <asp:TextBox ID="txtEffectDate" runat="server" Width="90%" CssClass="tukhoatimekiem"></asp:TextBox>
                <a class="help-lnk" href="javascript:void(0)" title="Chọn hoặc nhập ngày tháng theo định dạng dd/MM/yyyy" >
<span class="aui-icon aui-icon-small aui-iconfont-help">?</span></a>(dd/MM/yyyy)<br />
<asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Ngày có hiệu lực nhập không đúng định dạng" ControlToValidate="txtIssueDate" 
ForeColor="Red" SetFocusOnError="true" Display="Dynamic" Operator="DataTypeCheck" Type="Date" ValidationGroup="G1"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
             <td>
                <asp:Label ID="lblExpireDate" runat="server" Text="Ngày hết hiệu lực:" meta:resourcekey="lblExpireDate"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtExpireDate" runat="server" Width="90%" CssClass="tukhoatimekiem"></asp:TextBox>
                <a class="help-lnk" href="javascript:void(0)" title="Chọn hoặc nhập ngày tháng theo định dạng dd/MM/yyyy" >
<span class="aui-icon aui-icon-small aui-iconfont-help">?</span></a>(dd/MM/yyyy)<br />
<asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Ngày có hiệu lực nhập không đúng định dạng" ControlToValidate="txtIssueDate" 
ForeColor="Red" SetFocusOnError="true" Display="Dynamic" Operator="DataTypeCheck" Type="Date" ValidationGroup="G1"></asp:CompareValidator>
            </td>
            <td>
                <asp:Label ID="lblEffectStatus" runat="server" Text="Trạng thái hiệu lực:"
                    meta:resourcekey="lblEffectStatus"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlEffectStatus" runat="server" CssClass="userselect"
                    DataTextField="EffectStatusDesc" DataValueField="EffectStatusId"
                    Width="92%">
                </asp:DropDownList>
                <asp:Label ID="lblReviewStatus" runat="server" Text="Trạng thái:" Visible="false" meta:resourcekey="lblReviewStatus"></asp:Label>
                <asp:DropDownList ID="ddlReviewStatus" runat="server" CssClass="userselect" Visible="false"
                    DataTextField="ReviewStatusDesc" DataValueField="ReviewStatusId" Width="20px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label9" runat="server" Text="Số bộ hồ sơ:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txNumDossier" runat="server" CssClass="tukhoatimekiem" Width="90%" Text="0"></asp:TextBox>
                <br />
                <asp:RegularExpressionValidator ID="rglNumDossier" runat="server" ForeColor="Red" ErrorMessage="Chỉ chấp nhận kiểu số"
                        Display="Dynamic" ValidationExpression="\d+" ControlToValidate="txNumDossier" ValidationGroup="G1"></asp:RegularExpressionValidator>
            </td>
            <td>
                <asp:Label ID="Label11" runat="server" Text="Lệ phí:"></asp:Label>
               <%-- <asp:RequiredFieldValidator ID="rfvFee" runat="server" Text="(*)" Display="Dynamic"
                    ErrorMessage="Lệ phí" ValidationGroup="G1"
                    ControlToValidate="txFee" ForeColor="Red"></asp:RequiredFieldValidator>--%>
            </td>
            <td>
                <asp:TextBox ID="txFee" runat="server" CssClass="tukhoatimekiem" Width="90%" Text="0"></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="rgltxFee" runat="server" ForeColor="Red" ErrorMessage="Chỉ chấp nhận kiểu số" Display="Dynamic" ValidationExpression="\d+" ControlToValidate="txFee"></asp:RegularExpressionValidator>

                <%--<asp:RegularExpressionValidator ID="rglFee" runat="server" ForeColor="Red" ErrorMessage="Lệ phí nhập chưa chính xác"
                        Display="Dynamic" ValidationExpression="\d+" ControlToValidate="txFee" ValidationGroup="G1"></asp:RegularExpressionValidator>--%>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label10" runat="server" Text="Thành phần hồ sơ:"></asp:Label>
                <%--<asp:RequiredFieldValidator ID="rfvElementDossier" runat="server" Text="(*)" Display="Dynamic"
                    ErrorMessage="Thành phần hồ sơ" ValidationGroup="G1"
                    ControlToValidate="txElementDossier" ForeColor="Red"></asp:RequiredFieldValidator>--%>
            </td>
            <td>
                <asp:TextBox ID="txElementDossier" runat="server" CssClass="tukhoatimekiem" Width="90%" Height="45px" TextMode="MultiLine"></asp:TextBox>
            </td>
            <td class="style2">
                <asp:Label ID="Label8" runat="server" CssClass="OrganSelect" Text="Tỉnh thành :"></asp:Label>
                <%--<asp:RequiredFieldValidator ID="rfvProvices" runat="server" Text="(*)" Display="Dynamic"
                    ErrorMessage="Tỉnh thành" ValidationGroup="G1"
                    ControlToValidate="txtProvices" ForeColor="Red"></asp:RequiredFieldValidator>--%>
            </td>
            <td>
                <asp:TextBox ID="txtProvices" runat="server" Height="45px" TextMode="MultiLine"
                    Width="90%"></asp:TextBox>
                <br /><span class="node-help">Gõ vào ký tự rồi chọn từ danh sách gợi ý, danh sách phân cách bằng dấu <strong>;</strong></span>
            </td>
        </tr>
        <%-- end --%>
        <tr runat="server" visible="false">
            <td class="style2">
                <asp:Label ID="lblSigners" runat="server" Text="Người ký:" meta:resourcekey="lblSigners"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtSigners" runat="server" Height="85px" TextMode="MultiLine"
                    Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr runat="server">
            <td class="style2">
                <asp:Label ID="lblKeywords" runat="server" Text="Từ khóa:" meta:resourcekey="lblKeywords"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtKeywords" runat="server" Height="50px" TextMode="MultiLine"
                    Width="90%"></asp:TextBox>
            </td>
        </tr>
        
        <tr runat="server" visible="false">
            <td>
                <asp:Label ID="lblUseStatus" runat="server" Text="Trạng thái sử dụng:"
                    meta:resourcekey="lblUseStatus"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlUseStatus" runat="server" CssClass="userselect"
                    DataTextField="UseStatusDesc" DataValueField="UseStatusId"
                    Width="92%">
                </asp:DropDownList>
            </td>
        </tr>
        <tr runat="server" visible="false">
            <td>
                <asp:Label ID="lblDataSources" runat="server" Text="Nguồn:"
                    meta:resourcekey="lblDataSources"></asp:Label>
                
            </td>
            <td>
                <asp:DropDownList ID="ddlDataSources" runat="server" CssClass="userselect"
                    DataTextField="DataSourceDesc" DataValueField="DataSourceId"
                    Width="92%">
                </asp:DropDownList>
            </td>
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
                                <%--<asp:FileUpload ID="fUpd" runat="server" /> --%>
                            </div>
                        </td>
                        <td valign="middle">
                            <div id="mySpan" style="display: inline"></div>
                        </td>
                    </tr>
                </table>
                <div id="Div1" style="display: inline">
                    <a href="javascript: void(0);" onclick="javascript: AddUpload();">
                        <asp:Label ID="lblAddFile" runat="server" Text="Thêm file:" meta:resourcekey="lblAddFile"></asp:Label>
                    </a>
                </div>
                <asp:GridView ID="m_grid_File" DataKeyNames="DocFileId" runat="server" ShowHeader="False"
                    AutoGenerateColumns="False" CssClass="grid" PageSize="50" Width="98%"
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
                                <%# Eval("DocFileName")%>
                            </ItemTemplate>
                        </asp:TemplateField>
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
        <tr runat="server" visible="false">
            <td>

                <asp:Label ID="lblContent" runat="server" Text="Nội dung:"
                    meta:resourcekey="lblContent"></asp:Label>

            </td>
            <td colspan="3">

                <CKEditor:CKEditorControl ID="txtDocContent" runat="server"
                    BasePath="~/Integrated/ckeditor/" Height="270px" Width="90%"></CKEditor:CKEditorControl>

            </td>
        </tr>
        <tr>
            <td>

                <asp:Label ID="Label12" runat="server" Text="Các bước thực hiện:"></asp:Label>

            </td>
            <td colspan="3">
                <div style="padding: 5px;">
                    <a href="javascript:void(0)" onclick="AddRow()" class="luuvathemmoi">Thêm bước</a>
                    <span class="node-help">                
                        Mọi sửa đổi: Thêm, sửa, xóa chỉ được cập nhật khi nhấn nút lưu.
                    </span>
                </div>
                <div>
                    <asp:GridView ID="gvDocStep" DataKeyNames="DocStepId" runat="server" ShowHeader="True"
                        AutoGenerateColumns="False" CssClass="grid" PageSize="50" Width="98%"
                        OnRowDeleting="gvDocStep_RowDeleting" CellPadding="2" ForeColor="#333333"
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
                            <asp:TemplateField HeaderText="Thư tự">
                                <ItemTemplate>
                                    <asp:TextBox ID="tbStepOrder" runat="server" Text='<%# Eval("StepOrder") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="8%" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                                <HeaderStyle Width="8%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lbHeadDes" runat="server" Text="Mô tả"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="tbStepContent" runat="server" Text='<%# Eval("StepContent") %>' TextMode="MultiLine" Height="35px" Width="100%"></asp:TextBox>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Xóa" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbDelete" runat="server" Visible="false" CssClass="iconadmxoa" CommandName="Delete" ToolTip="Xóa" OnClientClick="return confirm('Bạn có chắc muốn xóa bước thực hiện này?');"></asp:LinkButton>
                                    <a class="iconadmxoa" href="javascript:void(0)" onclick="DeleteRow(this)"></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:TemplateField>
                        </Columns>

                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td colspan="3"></td>
        </tr>
    </table>
    <div style="background-color: #FFFFFF; bottom: 0px; padding: 8px; position: fixed; right: 1px; text-align: right; width: 100%;">
        <asp:LinkButton ID="btnBack" runat="server" CssClass="quaylai"
            Text="Quay lại" meta:resourcekey="btnBack"
            OnClick="btnBack_Click">
        </asp:LinkButton>
        <asp:LinkButton ID="btnSave" runat="server" CssClass="luuvaquaylai" ToolTip="Lưu và tiếp tục sửa thủ tục hành chính hiện tại" 
            Text="Lưu thông tin" meta:resourcekey="btnSave" ValidationGroup="G1"  onclick="btnSave_Click">
        </asp:LinkButton>
        <asp:LinkButton ID="btnSaveAndNew" runat="server" CssClass="luuvathemmoi" ToolTip="Lưu và thêm mới thủ tục hành chính khác" 
                    Text="Lưu và thêm mới"  ValidationGroup="G1" meta:resourcekey="btnSaveAndNew" 
            onclick="btnSaveAndNew_Click"  >
        </asp:LinkButton>
        <asp:LinkButton ID="lnkSaveSHVB" runat="server" CssClass="luuvathemmoi" ToolTip="Lưu văn bản với số hiệu văn bản trùng"  Visible="false"
                    Text="Lưu "  ValidationGroup="G1" meta:resourcekey="lnkSaveSHVB" 
            onclick="lnkSaveSHVB_Click"  >
        </asp:LinkButton>
    </div>
</asp:Content>

