<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="DocsEditPropertie.aspx.cs" Inherits="Admin_DocsEditPropertie" %>

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
            var Item = new Option("...", "0");
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
        function AddRow() {
            //Reference the GridView.
            var gridView = document.getElementById("dgd");
 
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
    

    
            var gridView = document.getElementById("sdfds");
    
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
            //AddUpload();
            <%--$("#<%= txtIssueDate.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#<%= txtEffectDate.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#<%= txtGazetteDate.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#<%= txtExpireDate.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });--%>

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
        <asp:Label ID="lblMessage" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label></div>
    <table cellpadding="3" cellspacing="0" style="width: 100%; font-weight: lighter">
        <tr runat="server" visible="false">
            <td style="width: 120px" rowspan="2"></td>
            <td style="width: 350px"></td>
            <td style="width: 120px">


                <asp:Label ID="lblDocTypes" runat="server" Text="Loại văn bản:" meta:resourcekey="lblDocTypes" Visible="false"></asp:Label>
            </td>
            <td>

                <asp:DropDownList ID="ddlDocTypes" runat="server" Visible="false" CssClass="userselect uiselect"
                    DataTextField="DocTypeDesc" DataValueField="DocTypeId"
                    Width="250px">
                </asp:DropDownList>
                <asp:Label ID="lblLanguage" runat="server" meta:resourcekey="lblLanguage" Visible="false"
                    Text="Ngôn ngữ:"></asp:Label>
                <asp:DropDownList ID="ddlLanguage" runat="server" DataTextField="LanguageDesc" CssClass="userselect" Enabled="false"
                    DataValueField="LanguageId" Width="250px" Visible="false">
                </asp:DropDownList>
            </td>
        </tr>
        <tr runat="server" visible="false">
            <td colspan="3">
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
            <td colspan="2" >
                <asp:Label ID="Label1" runat="server" Text="Tên văn bản :"></asp:Label>
                <asp:TextBox ID="txtDocName" runat="server" Width="99%" Height="45px" TextMode="MultiLine"
                    CssClass="tukhoatimekiem"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <table width="100%" cellpadding="3" cellspacing="3">
                    <tr>
                        <td>
                            <asp:Label ID="lblDocIdentity" runat="server" Text="Số hiệu:"></asp:Label>
                            <span class="icon-required"></span>
                            <asp:RequiredFieldValidator ID="rfvDocIdentity" runat="server" Text="(*)" Display="Dynamic"
                                ErrorMessage="(*)"
                                ValidationGroup="G1"
                                ControlToValidate="txtDocIdentity"
                                ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDocIdentity" runat="server" Width="240px"
                                CssClass="tukhoatimekiem"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="rfvtxtDocIdentity" runat="server" ErrorMessage="Bạn cần nhập vào Số hiệu văn bản" ForeColor="Red" ControlToValidate="txtDocIdentity" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
                            <asp:LinkButton ID="lnkSearchIdentity" runat="server" Visible="false" class="themmoi" Text="Tìm VB  "
                                meta:resourcekey="lnkSearchIdentity" OnClick="lnkSearchIdentity_Click">
                            </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblIssueDate" runat="server" Text="Ngày ban hành:" meta:resourcekey="lblIssueDate"></asp:Label>
                            <span class="icon-required"></span>
                            <asp:RequiredFieldValidator ID="rfvIssueDate" runat="server" Text="(*)" Display="Dynamic"
                                ErrorMessage="(*)"
                                ValidationGroup="G1"
                                ControlToValidate="txtIssueDate"
                                ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtIssueDate" runat="server" Width="240px" CssClass="tukhoatimekiem"></asp:TextBox>
                            <a class="help-lnk" href="javascript:void(0)" title="Chọn hoặc nhập ngày tháng theo định dạng dd/MM/yyyy"><span class="aui-icon aui-icon-small aui-iconfont-help">?</span></a>
                            (dd/MM/yyyy)<br />
                            <asp:RequiredFieldValidator ID="rfvtxtIssueDate" runat="server" ErrorMessage="Bạn cần nhập Ngày ban hành" ForeColor="Red" ControlToValidate="txtIssueDate" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cvtxtIssueDate" runat="server" ErrorMessage="Ngày bạn nhập không hợp lệ" ControlToValidate="txtIssueDate" ForeColor="Red" SetFocusOnError="true" Display="Dynamic" Operator="DataTypeCheck" Type="Date" ValidationGroup="G1"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblEffectDate" runat="server" Text="Ngày có hiệu lực:" meta:resourcekey="lblEffectDate"></asp:Label>
                        </td>
                        <td>

                            <asp:TextBox ID="txtEffectDate" runat="server" Width="240px" CssClass="tukhoatimekiem"></asp:TextBox>
                            <a class="help-lnk" href="javascript:void(0)" title="Chọn hoặc nhập ngày tháng theo định dạng dd/MM/yyyy"><span class="aui-icon aui-icon-small aui-iconfont-help">?</span></a>
                            (dd/MM/yyyy)<br />
                            <asp:CompareValidator ID="cvtxtEffectDate" runat="server" ErrorMessage="Ngày bạn nhập không hợp lệ" ControlToValidate="txtEffectDate" ForeColor="Red" SetFocusOnError="true" Display="Dynamic" Operator="DataTypeCheck" Type="Date" ValidationGroup="G1"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>

                            <asp:Label ID="lblExpireDate" runat="server" Text="Ngày hết hiệu lực:" meta:resourcekey="lblExpireDate"></asp:Label>
                        </td>
                        <td>

                            <asp:TextBox ID="txtExpireDate" runat="server" Width="240px" CssClass="tukhoatimekiem"></asp:TextBox>
                            <a class="help-lnk" href="javascript:void(0)" title="Chọn hoặc nhập ngày tháng theo định dạng dd/MM/yyyy"><span class="aui-icon aui-icon-small aui-iconfont-help">?</span></a>
                            (dd/MM/yyyy)<br />
                            <asp:CompareValidator ID="cvtxtExpireDate" runat="server" ErrorMessage="Ngày bạn nhập không hợp lệ" ControlToValidate="txtExpireDate" ForeColor="Red" SetFocusOnError="true" Display="Dynamic" Operator="DataTypeCheck" Type="Date" ValidationGroup="G1"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblGazetteDate" runat="server" Text="Ngày công báo:"
                                meta:resourcekey="lblGazetteDate"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtGazetteDate" runat="server" Width="240px"
                                CssClass="tukhoatimekiem"></asp:TextBox>
                            <a class="help-lnk" href="javascript:void(0)" title="Chọn hoặc nhập ngày tháng theo định dạng dd/MM/yyyy"><span class="aui-icon aui-icon-small aui-iconfont-help">?</span></a>
                            (dd/MM/yyyy)<br /> 
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblGazetteNumber" runat="server" Text="Số công báo:"
                                meta:resourcekey="lblGazetteNumber"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtGazetteNumber" runat="server" Width="240px"
                                CssClass="tukhoatimekiem"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblUseStatus" runat="server" Text="Trạng thái sử dụng:"
                                meta:resourcekey="lblUseStatus"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlUseStatus" runat="server" CssClass="userselect"
                                DataTextField="UseStatusDesc" DataValueField="UseStatusId"
                                Width="250px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblEffectStatus" runat="server" Text="Trạng thái hiệu lực:"
                                meta:resourcekey="lblEffectStatus"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEffectStatus" runat="server" CssClass="userselect"
                                DataTextField="EffectStatusDesc" DataValueField="EffectStatusId"
                                Width="250px">
                            </asp:DropDownList>
                            <asp:Label ID="lblReviewStatus" runat="server" Text="Trạng thái:" Visible="false" meta:resourcekey="lblReviewStatus"></asp:Label>
                            <asp:DropDownList ID="ddlReviewStatus" runat="server" CssClass="userselect" Visible="false"
                                DataTextField="ReviewStatusDesc" DataValueField="ReviewStatusId" Width="20px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <table cellpadding="2" cellspacing="0" border="0">
                    <tr>
                        <td class="style2">
                            <asp:Label ID="lblFields" runat="server" Text="Lĩnh vực:" meta:resourcekey="lblFields"></asp:Label>
                            <span class="icon-required" id="rqFields" runat="server"></span>
                            <asp:RequiredFieldValidator ID="rfvFields" runat="server" Text="(*)" Display="Dynamic"
                                ErrorMessage="(*)"
                                ValidationGroup="G1"
                                ControlToValidate="txtFields"
                                ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFields" runat="server" Height="45px" TextMode="MultiLine"
                                Width="355px"></asp:TextBox><br />
                            <span class="node-help">Gõ vào ký tự rồi chọn từ danh sách gợi ý, danh sách phân cách bằng dấu <strong>;</strong></span><br/>
                            <asp:RequiredFieldValidator ID="rfvtxtFields" runat="server" ErrorMessage="Bạn cần nhập vào Lĩnh vực" ForeColor="Red" ControlToValidate="txtFields" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="lblOrgans" runat="server" Text="CQBH:"
                                meta:resourcekey="lblOrgans"></asp:Label>
                            <span class="icon-required"></span>
                            <asp:RequiredFieldValidator ID="rfvOrgans" runat="server" Text="(*)" Display="Dynamic"
                                ErrorMessage="(*)"
                                ValidationGroup="G1"
                                ControlToValidate="txtOrgans"
                                ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOrgans" CssClass="OrganSelect" runat="server" Height="45px" TextMode="MultiLine"
                                Width="355px"></asp:TextBox><br />
                            <span class="node-help">Gõ vào ký tự rồi chọn từ danh sách gợi ý, danh sách phân cách bằng dấu <strong>;</strong></span><br/>
                            <asp:RequiredFieldValidator ID="rfvtxtOrgans" runat="server" ErrorMessage="Bạn cần nhập vào Cơ quan ban hành" ForeColor="Red" ControlToValidate="txtOrgans" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">

                            <asp:Label ID="Label8" runat="server" Text="Tỉnh thành :"></asp:Label>

                        </td>
                        <td>

                            <asp:TextBox ID="txtProvices" runat="server" Height="45px" TextMode="MultiLine"
                                Width="355px"></asp:TextBox><br />
                            <span class="node-help">Gõ vào ký tự rồi chọn từ danh sách gợi ý, danh sách phân cách bằng dấu <strong>;</strong></span>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="lblSigners" runat="server" Text="Người ký:" meta:resourcekey="lblSigners"></asp:Label>
                            <span class="icon-required" id="rqSigners" runat="server"></span>
                            <asp:RequiredFieldValidator ID="rfvSigners" runat="server" Text="(*)" Display="Dynamic"
                                ErrorMessage="(*)"
                                ValidationGroup="G1"
                                ControlToValidate="txtSigners"
                                ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSigners" runat="server" Height="45px" TextMode="MultiLine"
                                Width="355px"></asp:TextBox><br />
                            <span class="node-help">Gõ vào ký tự rồi chọn từ danh sách gợi ý, danh sách phân cách bằng dấu <strong>;</strong></span><br/>
                            <asp:RequiredFieldValidator ID="rfvtxtSigners" runat="server" ErrorMessage="Bạn cần nhập vào Người ký" ForeColor="Red" ControlToValidate="txtSigners" SetFocusOnError="true" Display="Dynamic" ValidationGroup="G1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="lblKeywords" runat="server" Text="Từ khóa:" meta:resourcekey="lblKeywords"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtKeywords" runat="server" Height="45px" TextMode="MultiLine" 
                                Width="355px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>

        </tr>
        <tr style="height: 50px;">
            <td></td>
            <td></td>
        </tr>
    </table>
    <div style="background-color: #FFFFFF; bottom: 0px; padding: 8px; position: fixed; right: 1px; text-align: center; width: 100%;">
        <asp:LinkButton ID="btnBack" runat="server" CssClass="quaylai" Visible="false"
            Text="Quay lại" meta:resourcekey="btnBack"
            OnClick="btnBack_Click">
        </asp:LinkButton>
        <asp:LinkButton ID="btnSave" runat="server" CssClass="luuvaquaylai hidden-save"
            Text="Lưu thông tin" meta:resourcekey="btnSave"  ValidationGroup="G1" OnClick="btnSave_Click">
        </asp:LinkButton>
        <asp:LinkButton ID="btnSaveAndNew" runat="server" Visible="false" CssClass="luuvathemmoi"
            Text="Lưu và thêm mới" ValidationGroup="G1" meta:resourcekey="btnSaveAndNew"
            OnClick="btnSaveAndNew_Click">
        </asp:LinkButton>
        <input type="button" name="ctl00$m_contentBody$btnSaveHide" value="Lưu thông tin" id="m_contentBody_btnSaveHide" title="Lưu và tiếp tục sửa văn bản hiện tại" class="luuvaquaylai" />
        
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
                var ddlDocGroups = '<%:DocGroupId%>';
                
                var txtDocIdentity = $('#<%:txtDocIdentity.ClientID%>');
                var rfvDocIdentity = $('#<%:rfvtxtDocIdentity.ClientID%>');
                if (isEmpty(txtDocIdentity.val())) {
                    rfvDocIdentity.css({ 'display': 'inline' });
                    if (focusInput == null) {
                        focusInput = txtDocIdentity;
                    }
                } else {
                    rfvDocIdentity.hide();
                }
                var txtIssueDate = $('#<%:txtIssueDate.ClientID%>');
                var rfvIssueDate = $('#<%:rfvtxtIssueDate.ClientID%>');
                var CompareValidator2 = $('#<%:cvtxtIssueDate.ClientID%>');
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
                var txtSigners = $('#<%:txtSigners.ClientID%>');
                var rfvSigners = $('#<%:rfvSigners.ClientID%>');
                var txtFields = $('#<%:txtFields.ClientID%>');
                var rfvFields = $('#<%:rfvFields.ClientID%>');
                if (ddlDocGroups.length && ddlDocGroups.val() != '8') {
                    
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
                    
                    var txtOrgans = $('#<%:txtOrgans.ClientID%>');
                    var rfvOrgans = $('#<%:rfvOrgans.ClientID%>');
                    if (isEmpty(txtSigners.val())) {
                        rfvSigners.css({ 'display': 'inline' });
                        if (focusInput == null) {
                            focusInput = txtOrgans;
                        }
                    } else {
                        rfvOrgans.hide();
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
                    rfvSigners.hide();
                    rfvSigners.css({ 'display': 'none' });
                    rfvFields.hide();
                    rfvFields.css({ 'display': 'none' });
                    
                }
                if (focusInput != null) {
                    focusInput.focus();
                    return false;
                }
            } catch (ex) {
            }
            return true;
        }
        function actionCheckDataAfterSave(btnSaveHide, idSaveShow) {
            try {
                if (!validDocsEditForm()) {
                    return false;
                }
                $('#' + idSaveShow)[0].click();
            }catch(ex){
                console.log(ex);
            }
        }
        $('#m_contentBody_btnSaveHide').off('click').on('click', function (evt) {
            evt.preventDefault();
            actionCheckDataAfterSave($(this),'<%:btnSave.ClientID%>');
        });
        $('#m_contentBody_btnSaveAndNewHide2').off('click').on('click', function (evt) {
            evt.preventDefault();
            actionCheckDataAfterSave($(this),'<%:btnSaveAndNew.ClientID%>');
        });
    </script>
</asp:Content>

