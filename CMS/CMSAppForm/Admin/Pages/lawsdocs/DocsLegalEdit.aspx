<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="DocsLegalEdit.aspx.cs" Inherits="Admin_DocsEdit" %>
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

    $(document).ready(function () {
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(InitializeRequest);
        prm.add_endRequest(EndRequest);
        AddUpload();
        $("#<%= txtIssueDate.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#<%= txtEffectDate.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#<%= txtGazetteDate.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#<%= txtExpireDate.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });

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
    ////  


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
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter">
        <tr>
            <td style="width:120px" rowspan="2">
             <asp:Label ID="lblDocIdentity" runat="server" Text="Số hiệu văn bản:" 
                    meta:resourcekey="lblDocIdentity"></asp:Label>
                    <asp:RequiredFieldValidator ID="rfvDocIdentity" runat="server" Text="(*)" Display="Dynamic" 
                ErrorMessage="(*)" ValidationGroup="G1" 
                ControlToValidate="txtDocIdentity" ForeColor="Red"></asp:RequiredFieldValidator>
             </td>
            <td style="width:350px">                
                <asp:TextBox ID="txtDocIdentity" runat="server" Width="240px" 
                    CssClass="tukhoatimekiem"></asp:TextBox>
                    <asp:LinkButton ID="lnkSearchIdentity" runat="server" Visible="false" class="themmoi" Text="Tìm VB  " 
            meta:resourcekey="lnkSearchIdentity" onclick="lnkSearchIdentity_Click">
        </asp:LinkButton>
            
            </td>
            <td>
                <asp:Label ID="lblLanguage" runat="server" meta:resourcekey="lblLanguage" 
                    Text="Ngôn ngữ:"></asp:Label>
                <asp:DropDownList ID="ddlLanguage" runat="server" DataTextField="LanguageDesc" CssClass="userselect"  Enabled="false"
                    DataValueField="LanguageId" Width="250px">
                </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td colspan="3">
                <div id="tblDocshowByIdentity" runat="server" visible="false">
                    <asp:DropDownList ID="ddlSelectDocs" runat="server" AutoPostBack="True" 
                        CssClass="userselect" DataTextField="DocName" DataValueField="DocId" 
                        onselectedindexchanged="ddlSelectDocs_SelectedIndexChanged" Width="91%">
                    </asp:DropDownList>
                    <asp:LinkButton ID="lnkSelectDocs" runat="server" class="themmoi" 
                        meta:resourcekey="lnkSelectDocs" onclick="lnkSelectDocs_Click" Text="Chọn »">
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
            <td><asp:Label ID="lblDocName" runat="server" Text="Tên văn bản:" 
                    meta:resourcekey="lblDocName"></asp:Label>
                     <asp:RequiredFieldValidator ID="rfvDocName" runat="server" Display="Dynamic" 
                ErrorMessage="(*)" ValidationGroup="G1" 
                ControlToValidate="txtDocName" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            <td colspan="3">
                <asp:TextBox ID="txtDocName" runat="server" Width="98%" 
                    CssClass="tukhoatimekiem" Height="30px" TextMode="MultiLine"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td>
                                          
                <asp:Label ID="lblIssueDate" runat="server" Text="Ngày ban hành:" meta:resourcekey="lblIssueDate"></asp:Label>
                                          
                </td>
            <td style="width:250px">
                
                <asp:TextBox ID="txtIssueDate" runat="server" Width="240px" CssClass="tukhoatimekiem"></asp:TextBox>
                                            
                </td>
            <td colspan="2" rowspan="9" valign="top">
                 <table cellpadding="2" cellspacing="0" border="0">
                                <tr>
                                        <td class="style2">
                                           
                <asp:Label ID="lblFields" runat="server" Text="Lĩnh vực:" 
                    meta:resourcekey="lblFields"></asp:Label>
                                           
                                        </td>
                                        <td>                                            
                                    <asp:TextBox ID="txtFields" runat="server" Height="85px" TextMode="MultiLine"  
                                        Width="300px"></asp:TextBox>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style2">
                                            
                <asp:Label ID="lblOrgans" runat="server" Text="CQBH:" 
                    meta:resourcekey="lblOrgans"></asp:Label>
                                            
                                        </td>
                                        <td>
                                            
                <asp:TextBox ID="txtOrgans" runat="server" Height="85px" TextMode="MultiLine" 
                    Width="300px"></asp:TextBox>
                                            
                                        </td>
                                    </tr>
                                     <tr>
                                        <td class="style2"> <asp:Label ID="lblSigners" runat="server" Text="Người ký:" 
                    meta:resourcekey="lblSigners"></asp:Label> </td>
                                        <td>                                            
                                           <asp:TextBox ID="txtSigners" runat="server" Height="85px" TextMode="MultiLine" 
                                        Width="300px"></asp:TextBox>
                                         </td>   
                                     </tr>
                                      </table>
                                <asp:Label ID="lblKeywords" runat="server" Text="Từ khóa:"  Visible="false"
                    meta:resourcekey="lblKeywords"></asp:Label>
                 <asp:TextBox ID="txtKeywords" runat="server" Height="50px" TextMode="MultiLine"  Visible="false"
                                                Width="300px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td>
                                          
                <asp:Label ID="lblEffectDate" runat="server" Text="Ngày có hiệu lực:" meta:resourcekey="lblEffectDate"></asp:Label>
                                          
                </td>
            <td>
                                            
                <asp:TextBox ID="txtEffectDate" runat="server" Width="240px" CssClass="tukhoatimekiem"></asp:TextBox>
                                            
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
                                           
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblGazetteNumber" runat="server" Text="Số công báo:" 
                    meta:resourcekey="lblGazetteNumber"></asp:Label>
                </td>
            <td><asp:TextBox ID="txtGazetteNumber" runat="server" Width="240px" 
                    CssClass="tukhoatimekiem"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td>
                                            
                <asp:Label ID="lblExpireDate" runat="server" Text="Ngày hết hiệu lực:" meta:resourcekey="lblExpireDate"></asp:Label>
                                            
                </td>
            <td>
                                           
                <asp:TextBox ID="txtExpireDate" runat="server" Width="240px" CssClass="tukhoatimekiem"></asp:TextBox>
                                           
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblUseStatus" runat="server" Text="Trạng thái sử dụng:" 
                    meta:resourcekey="lblUseStatus"></asp:Label>
                </td>
            <td><asp:DropDownList ID="ddlUseStatus" runat="server" CssClass="userselect"  
                    DataTextField="UseStatusDesc" DataValueField="UseStatusId" 
                    Width="250px"></asp:DropDownList>
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
                    Width="250px"></asp:DropDownList>
                <asp:Label ID="lblReviewStatus" runat="server" Text="Trạng thái:" Visible="false" meta:resourcekey="lblReviewStatus"></asp:Label>
                <asp:DropDownList ID="ddlReviewStatus" runat="server" CssClass="userselect"   Visible="false"
                    DataTextField="ReviewStatusDesc" DataValueField="ReviewStatusId" Width="20px"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblDataSources" runat="server" Text="Nguồn:" 
                    meta:resourcekey="lblDataSources"></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlDataSources" runat="server" CssClass="userselect"  
                    DataTextField="DataSourceDesc" DataValueField="DataSourceId" 
                    Width="250px"></asp:DropDownList>
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
                    <td > <div id="uploads" style="display:inline">
                      <%--<asp:FileUpload ID="fUpd" runat="server" /> --%>             
                      </div>
                  </td >
                    <td valign="middle"><div id="mySpan" style="display:inline" ></div></td>
                </tr>
             </table>              
                <div id="Div1" style="display:inline">
                    <a href="javascript: void(0);" onclick="javascript: AddUpload();"><asp:Label ID="lblAddFile" runat="server" Text="Thêm file:" 
                    meta:resourcekey="lblAddFile"></asp:Label></a></div>
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
                            <asp:TemplateField  > 
                               <HeaderTemplate>                                
                                    <asp:Label ID="lblGridColumnDataSources" runat="server" Text="Nguồn" meta:resourcekey="lblGridColumnDataSources"></asp:Label>
                                </HeaderTemplate>  
                                <ItemTemplate> 
                                   <%# LanguageHelpers.IsCultureVietnamese() ? DataSources.Static_Get(short.Parse(Eval("DataSourceId").ToString()), l_DataSources).DataSourceDesc : DataSources.Static_Get(short.Parse(Eval("DataSourceId").ToString()), l_DataSources).DataSourceName%>
                                </ItemTemplate>
                                <ItemStyle Width="8%" HorizontalAlign = "Left" VerticalAlign="Top" Wrap="false" />  
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
            
                <CKEditor:CKEditorControl ID="txtDocContent" runat="server" 
                                BasePath="~/Integrated/ckeditor/" Height="270px"  Width="98%"></CKEditor:CKEditorControl>
            
            </td>
        </tr>
        <tr>
            <td>
            
                &nbsp;</td>
                 <td colspan="3">
            
            </td>
        </tr>
        </table>
    <div style="background-color: #FFFFFF; bottom: 0px;  padding: 8px;  position: fixed;   right: 1px;   text-align: right;  width: 100%;">
        <asp:LinkButton ID="btnBack" runat="server" CssClass="quaylai" 
                    Text="Quay lại" meta:resourcekey="btnBack" 
            onclick="btnBack_Click" >
        </asp:LinkButton>
        <asp:LinkButton ID="btnSave" runat="server" CssClass="luuvaquaylai" 
            Text="Lưu thông tin" meta:resourcekey="btnSave" ValidationGroup="G1"  onclick="btnSave_Click">
        </asp:LinkButton>
        <asp:LinkButton ID="btnSaveAndNew" runat="server" CssClass="luuvathemmoi" 
                    Text="Lưu và thêm mới"  ValidationGroup="G1" meta:resourcekey="btnSaveAndNew" 
            onclick="btnSaveAndNew_Click"  >
        </asp:LinkButton>
    </div>
</asp:Content>

