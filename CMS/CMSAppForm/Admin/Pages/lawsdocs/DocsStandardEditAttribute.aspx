<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="DocsStandardEditAttribute.aspx.cs" Inherits="Admin_DocsStandardEditAttribute" %>

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
        //AddUpload();
    <%--    $("#<%= txtIssueDate.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
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

            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div id="tblMsg" runat="server" style="padding: 5px; text-align: center;">
                    <asp:Label ID="lblMsg" runat="server" Font-Bold="true" ForeColor="Red"
                        meta:resourcekey="lblMsg"
                        Text=""></asp:Label>
                </div>
                <asp:Label ID="Label13" runat="server" Text="Tên văn bản :"></asp:Label>
                <asp:TextBox ID="txtDocName" runat="server" Width="97.7%"
                    CssClass="tukhoatimekiem"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <table width="100%" cellpadding="3" cellspacing="3">
                    <tr>
                        <td>

                            <asp:Label ID="lblDocIdentity" runat="server" Text="Số hồ sơ:"
                                meta:resourcekey="lblDocIdentity"></asp:Label>
                            <span class="icon-required"></span>
                            <asp:RequiredFieldValidator ID="rfvDocIdentity" runat="server" Text="(*)" Display="Dynamic"
                                ErrorMessage="(*)" ValidationGroup="G1"
                                ControlToValidate="txtDocIdentity" ForeColor="Red"></asp:RequiredFieldValidator>

                        </td>
                        <td>

                            <asp:TextBox ID="txtDocIdentity" runat="server" Width="240px"
                                CssClass="tukhoatimekiem"></asp:TextBox>
                            <asp:LinkButton ID="lnkSearchIdentity" runat="server" Visible="false" class="themmoi" Text="Tìm VB  "
                                meta:resourcekey="lnkSearchIdentity" OnClick="lnkSearchIdentity_Click">
                            </asp:LinkButton><br />
                            <asp:RequiredFieldValidator ID="rfvtxtDocIdentity" runat="server" Display="Dynamic"
                                ErrorMessage="Bạn cần nhập Số hồ sơ" ValidationGroup="G1"
                                ControlToValidate="txtDocIdentity" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblIssueDate" runat="server" Text="Ngày ban hành:" meta:resourcekey="lblIssueDate"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtIssueDate" runat="server" Width="240px" CssClass="tukhoatimekiem"></asp:TextBox>
                            <a class="help-lnk" href="javascript:void(0)" title="Chọn hoặc nhập ngày tháng theo định dạng dd/MM/yyyy"><span class="aui-icon aui-icon-small aui-iconfont-help">?</span></a>
                            (dd/MM/yyyy)<br />
                            <asp:CompareValidator ID="cvtxtIssueDate" runat="server" ErrorMessage="Ngày bạn nhập không hợp lệ" ControlToValidate="txtIssueDate" ForeColor="Red" SetFocusOnError="true" Display="Dynamic" Operator="DataTypeCheck" Type="Date" ValidationGroup="G1"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblFee" runat="server" Text="Năm ban hành:" meta:resourcekey="lblFee"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFee" runat="server" Width="240px" CssClass="tukhoatimekiem"></asp:TextBox>
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
                    <tr runat="server" visible="false">
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Đối tượng thực hiện:"></asp:Label>
                            <asp:Label ID="lblGazetteDate" runat="server" Text="Ngày công báo:" Visible="false"
                                meta:resourcekey="lblGazetteDate"></asp:Label>

                        </td>
                        <td>
                            <asp:TextBox ID="txDocObject" runat="server" CssClass="tukhoatimekiem" Width="240px"></asp:TextBox>
                            <asp:TextBox ID="txtGazetteDate" runat="server" Width="240px" Visible="false"
                                CssClass="tukhoatimekiem"></asp:TextBox>

                        </td>
                    </tr>
                    <tr runat="server" visible="false">
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Phạm vi áp dụng:"></asp:Label>
                            <asp:Label ID="lblGazetteNumber" runat="server" Text="Số công báo:" Visible="false"
                                meta:resourcekey="lblGazetteNumber"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlGrantLevelId" runat="server" CssClass="userselect"
                                DataTextField="GrantLevelDesc" DataValueField="GrantLevelId"
                                Width="250px">
                            </asp:DropDownList>
                            <asp:TextBox ID="txDocScopes" runat="server" CssClass="tukhoatimekiem" Width="240px" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="txtGazetteNumber" runat="server" Width="240px" Visible="false"
                                CssClass="tukhoatimekiem"></asp:TextBox>
                        </td>
                    </tr>
                    <tr runat="server" visible="false">
                        <td>
                            <asp:Label ID="Label9" runat="server" Text="Số bộ hồ sơ:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txNumDossier" runat="server" CssClass="tukhoatimekiem" Width="240px">2</asp:TextBox>

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
                    <tr runat="server" visible="false">
                        <td>
                            <asp:Label ID="lblDataSources" runat="server" Text="Nguồn:"
                                meta:resourcekey="lblDataSources"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDataSources" runat="server" CssClass="userselect"
                                DataTextField="DataSourceDesc" DataValueField="DataSourceId"
                                Width="250px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr runat="server" visible="false">
                        <td>
                            <asp:Label ID="Label11" runat="server" Text="Lệ phí:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txFee" runat="server" CssClass="tukhoatimekiem" Width="240px" Height="45px" TextMode="MultiLine"></asp:TextBox>

                        </td>
                    </tr>
                    <tr runat="server" visible="false">
                        <td>
                            <asp:Label ID="Label10" runat="server" Text="Thành phần hồ sơ:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txElementDossier" runat="server" CssClass="tukhoatimekiem" Width="240px" Height="45px" TextMode="MultiLine"></asp:TextBox>

                        </td>
                    </tr>
                    <tr runat="server" visible="false">
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
                    <tr runat="server" visible="false">
                        <td>

                            <asp:Label ID="lblContent" runat="server" Text="Nội dung:"
                                meta:resourcekey="lblContent"></asp:Label>

                        </td>
                        <td colspan="3">

                            <CKEditor:CKEditorControl ID="txtDocContent" runat="server"
                                BasePath="~/Integrated/ckeditor/" Height="270px" Width="98%"></CKEditor:CKEditorControl>

                        </td>
                    </tr>
                    <tr runat="server" visible="false">
                        <td>

                            <asp:Label ID="Label12" runat="server" Text="Các bước thực hiện:"></asp:Label>

                        </td>
                        <td colspan="3">
                            <div style="padding: 5px;">
                                <a href="javascript:AddRow()" class="luuvathemmoi">Thêm bước</a>
                            </div>
                            <div>
                                <asp:GridView ID="gvDocStep" DataKeyNames="DocStepId" runat="server" ShowHeader="True"
                                    AutoGenerateColumns="False" CssClass="grid" PageSize="50" Width="60%"
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
                </table>
            </td>
            <td valign="top">
                <table cellpadding="2" cellspacing="0" border="0">
                    <tr>
                        <td class="style2">
                            <asp:Label ID="lblFields" runat="server" Text="Lĩnh vực:" meta:resourcekey="lblFields"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFields" runat="server" Height="45px" TextMode="MultiLine"
                                Width="327px"></asp:TextBox><br/>
                            <span class="node-help">Gõ vào ký tự rồi chọn từ danh sách gợi ý, danh sách phân cách bằng dấu <strong>;</strong></span><br/>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">

                            <asp:Label ID="lblOrgans" runat="server" Text="CQBH:"
                                meta:resourcekey="lblOrgans"></asp:Label>

                        </td>
                        <td>

                            <asp:TextBox ID="txtOrgans" CssClass="OrganSelect" runat="server" Height="45px" TextMode="MultiLine"
                                Width="327px"></asp:TextBox><br/>
                            <span class="node-help">Gõ vào ký tự rồi chọn từ danh sách gợi ý, danh sách phân cách bằng dấu <strong>;</strong></span><br/>
                        </td>
                    </tr>
                    <tr runat="server" visible="false">
                        <td class="style2">
                            <asp:Label ID="Label6" runat="server" Text="Cơ quan thực hiện:"></asp:Label>
                        </td>
                        <td>

                            <asp:TextBox ID="txtOrgans5" CssClass="OrganSelect" runat="server" Height="45px" TextMode="MultiLine"
                                Width="300px"></asp:TextBox><br/>
                            <span class="node-help">Gõ vào ký tự rồi chọn từ danh sách gợi ý, danh sách phân cách bằng dấu <strong>;</strong></span><br/>
                        </td>
                    </tr>
                    <tr runat="server" visible="false">
                        <td class="style2">

                            <asp:Label ID="Label7" runat="server" Text="Cơ quan quyết định:"></asp:Label>

                        </td>
                        <td>

                            <asp:TextBox ID="txtOrgans6" CssClass="OrganSelect" runat="server" Height="45px" TextMode="MultiLine"
                                Width="300px"></asp:TextBox>

                        </td>
                    </tr>
                    <tr runat="server" visible="false">
                        <td class="style2">

                            <asp:Label ID="Label8" runat="server" Text="Tỉnh thành :"></asp:Label>

                        </td>
                        <td>

                            <asp:TextBox ID="txtProvices" runat="server" Height="45px" TextMode="MultiLine"
                                Width="300px"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="lblSigners" runat="server" Text="Người ký:" meta:resourcekey="lblSigners"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSigners" runat="server" Height="45px" TextMode="MultiLine"
                                Width="300px"></asp:TextBox><br/>
                            <span class="node-help">Gõ vào ký tự rồi chọn từ danh sách gợi ý, danh sách phân cách bằng dấu <strong>;</strong></span><br/>
                        </td>
                    </tr>
                    <tr runat="server">
                        <td class="style2">
                            <asp:Label ID="lblKeywords" runat="server" Text="Từ khóa:" meta:resourcekey="lblKeywords"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtKeywords" runat="server" Height="45px" TextMode="MultiLine"
                                Width="300px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr runat="server" visible="false">
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Thời gian giải quyết:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txSettlementTime" runat="server" CssClass="tukhoatimekiem" Width="300px" Height="45px" TextMode="MultiLine"></asp:TextBox>

                        </td>
                    </tr>
                    <tr runat="server" visible="false">
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Cách thức thực hiện:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txPerformMethod" runat="server" CssClass="tukhoatimekiem" Width="300px" Height="45px" TextMode="MultiLine"></asp:TextBox>

                        </td>
                    </tr>
                    <tr runat="server" visible="false">
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Kết quả thực hiện:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txResult" runat="server" CssClass="tukhoatimekiem" Width="300px" Height="45px" TextMode="MultiLine"></asp:TextBox>

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
        <asp:LinkButton ID="btnSave" runat="server" CssClass="luuvaquaylai"
            Text="Lưu thông tin" ToolTip="Lưu thông tin" meta:resourcekey="btnSave" ValidationGroup="G1" OnClick="btnSave_Click">
        </asp:LinkButton>
        <asp:LinkButton ID="btnSaveAndNew" runat="server" CssClass="luuvathemmoi"
            Text="Lưu và thêm mới" ValidationGroup="G1" meta:resourcekey="btnSaveAndNew"
            OnClick="btnSaveAndNew_Click">
        </asp:LinkButton>
    </div>
</asp:Content>

