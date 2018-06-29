<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InsertArticle.aspx.cs" Inherits="fckplugins_InsertArticle" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Chèn bài viết vào nội dung</title>
    
    <style>
        body {font-family:Arial; font-size: 12px;}
    </style>
    <script>
		// Helper function to display messages below CKEditor.
		function ShowMessage( msg ) {
			document.getElementById( 'eMessage' ).innerHTML = msg;
		}
		
		function InsertHTML(value) {
			// Get the editor instance that you want to interact with.
		    var editor = window.parent.CKEDITOR.instances.m_contentBody_txtContent;
			
			// Check the active editing mode.
			if ( editor.mode == 'wysiwyg' )
			{
				// Insert HTML code.
				// http://docs.ckeditor.com/#!/api/CKEDITOR.editor-method-insertHtml
			    editor.insertHtml(value);
			    window.parent.CKEDITOR.dialog.getCurrent().hide();
			}
			else
				alert( 'You must be in WYSIWYG mode!' );
		}

		function InsertText() {
			// Get the editor instance that you want to interact with.
			var editor = CKEDITOR.instances.editor1;
			var value = document.getElementById( 'txtArea' ).value;

			// Check the active editing mode.
			if ( editor.mode == 'wysiwyg' )
			{
				// Insert as plain text.
				// http://docs.ckeditor.com/#!/api/CKEDITOR.editor-method-insertText
				editor.insertText( value );
			}
			else
				alert( 'You must be in WYSIWYG mode!' );
		}

		function SetContents() {
			// Get the editor instance that you want to interact with.
			var editor = CKEDITOR.instances.editor1;
			var value = document.getElementById( 'htmlArea' ).value;

			// Set editor content (replace current content).
			// http://docs.ckeditor.com/#!/api/CKEDITOR.editor-method-setData
			editor.setData( value );
		}

		function GetContents() {
			// Get the editor instance that you want to interact with.
			var editor = CKEDITOR.instances.editor1;

			// Get editor content.
			// http://docs.ckeditor.com/#!/api/CKEDITOR.editor-method-getData
			alert( editor.getData() );
		}

		function ExecuteCommand( commandName ) {
			// Get the editor instance that you want to interact with.
			var editor = CKEDITOR.instances.editor1;

			// Check the active editing mode.
			if ( editor.mode == 'wysiwyg' )
			{
				// Execute the command.
				// http://docs.ckeditor.com/#!/api/CKEDITOR.editor-method-execCommand
				editor.execCommand( commandName );
			}
			else
				alert( 'You must be in WYSIWYG mode!' );
		}

		function CheckDirty() {
			// Get the editor instance that you want to interact with.
			var editor = CKEDITOR.instances.editor1;
			// Checks whether the current editor content contains changes when compared
			// to the content loaded into the editor at startup.
			// http://docs.ckeditor.com/#!/api/CKEDITOR.editor-method-checkDirty
			alert( editor.checkDirty() );
		}

		function ResetDirty() {
			// Get the editor instance that you want to interact with.
			var editor = CKEDITOR.instances.editor1;
			// Resets the "dirty state" of the editor.
			// http://docs.ckeditor.com/#!/api/CKEDITOR.editor-method-resetDirty
			editor.resetDirty();
			alert( 'The "IsDirty" status was reset.' );
		}

		function Focus() {
			// Get the editor instance that you want to interact with.
			var editor = CKEDITOR.instances.editor1;
			// Focuses the editor.
			// http://docs.ckeditor.com/#!/api/CKEDITOR.editor-method-focus
			editor.focus();
		}
	</script>

</head>
<body style="padding:0; margin:0;">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />
    <asp:HiddenField ID="hdfSelectedItems" runat="server" />
    <table class="tableBorder" cellspacing="1" cellpadding="2" width="98%">
        <tr>
        <td colspan="2">
        <div class="main-search">
                    <table width='100%' border='0' cellpadding='0' cellspacing=''>
                        <tr>
                            
                           <td width="80px">
                                <b style="color:Blue;">Site: </b>
                           </td>
                           <td>
                               <asp:DropDownList ID="ddlSite" runat="server" DataTextField="SiteDesc" 
                        DataValueField="SiteId" Width="200px" CssClass="userselect"
                        AutoPostBack="True" onselectedindexchanged="ddlSite_SelectedIndexChanged">
                    </asp:DropDownList>
                               <b style="color:Blue;">&nbsp;Chuyên mục: </b>
                                <asp:DropDownList runat="server" ID="ddlCategories" Width="200px" OnSelectedIndexChanged="ddlCategories_SelectedIndexChanged"></asp:DropDownList>
                           </td>
                        </tr>                        
                        <tr>
                            <td>
                                <b style="color:Blue;">Từ ngày: </b>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtDateTimeFrom" Width="196px"></asp:TextBox> 
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="MM/dd/yyyy" TargetControlID="txtDateTimeFrom" />
                                <b style="color:Blue;">&nbsp;Đến ngày:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </b>
                                <asp:TextBox runat="server" ID="txtDateTimeTo" Width="195px"></asp:TextBox> 
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="txtDateTimeTo" />
                               
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b style="color:Blue;">Từ khóa: </b>
                           </td>
                           <td>
                                <asp:TextBox runat="server" ID="txtKeyword" Width="350px"></asp:TextBox>
                                <asp:Button runat="server" ID="btnSearch" Text="Tìm kiếm" 
                                    onclick="btnSearch_Click" />
                           </td>
                        </tr>
                    </table>
                </div>
                <center>
                    <div class="main-search">
                        <table width='100%' border='0' cellpadding='0' cellspacing=''>
                            <tr>
                                <td width='50%' align="left">
                                    <br />
                                    <b><asp:Label ID="lblRecord" runat="server"></asp:Label></b>
                                </td>
                                <td align="right">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </div>
                </center>
        </td>
        </tr>
        <tr>
            <td width="50%" valign="top">
                <div class="main_search">
                   
                     <asp:GridView ID="m_grid" DataKeyNames="ArticleId" runat="server" 
                        PageSize="10"
                        PagerSettings-Mode="Numeric"
                        PagerSettings-PageButtonCount="10"
                        PagerSettings-Position="Bottom"
                        PagerStyle-HorizontalAlign="Center" 
                        PagerSettings-NextPageText="Next"
                        PagerSettings-PreviousPageText="Prev"
                        CellPadding="4" ForeColor="#333333"
                        AutoGenerateColumns="False" Width="100%"  
                        OnRowDeleting="m_grid_RowDeleting" GridLines="None" >
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"/>

<PagerSettings NextPageText="Next" PreviousPageText="Prev"></PagerSettings>

<PagerStyle HorizontalAlign="Center" BackColor="#284775" ForeColor="White"></PagerStyle>

                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                        <ItemTemplate> <%# Container.DataItemIndex + 1 %> </ItemTemplate>    
                        <ItemStyle HorizontalAlign="Left" Width="2%" />              
                        </asp:TemplateField>
              
                        <asp:TemplateField HeaderText="Tiêu đề">
                            <ItemTemplate> 
                                <%#Eval("Title")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="68%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-BorderWidth="0px" HeaderText="Chọn">
				            <ItemTemplate>
					            <asp:LinkButton id="cmdDelete" runat="server" CommandName="Delete" CausesValidation="false">Chọn</asp:LinkButton>
				            </ItemTemplate>

<HeaderStyle BorderWidth="0px"></HeaderStyle>

                            <ItemStyle HorizontalAlign="Left" Width="8%" BorderWidth="0px" />
			            </asp:TemplateField>
                        
                    </Columns>
                         <SortedAscendingCellStyle BackColor="#E9E7E2" />
                         <SortedAscendingHeaderStyle BackColor="#506C8C" />
                         <SortedDescendingCellStyle BackColor="#FFFDF8" />
                         <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
                </div>
            </td>
            <td valign="top">
                <asp:GridView ID="m_gridSelected" DataKeyNames="ArticleId" runat="server" 
                        PageSize="10"
                        PagerSettings-Mode="Numeric"
                        PagerSettings-PageButtonCount="10"
                        PagerSettings-Position="Bottom"
                        PagerStyle-HorizontalAlign="Center" 
                        PagerSettings-NextPageText="Next"
                        PagerSettings-PreviousPageText="Prev"
                        CellPadding="4" ForeColor="#333333"
                        AutoGenerateColumns="False" Width="100%"  
                        OnRowDeleting="m_gridSelected_RowDeleting" GridLines="None" >
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"/>

<PagerSettings NextPageText="Next" PreviousPageText="Prev"></PagerSettings>

<PagerStyle HorizontalAlign="Center" BackColor="#284775" ForeColor="White"></PagerStyle>

                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                        <ItemTemplate> <%# Container.DataItemIndex + 1 %> </ItemTemplate>    
                        <ItemStyle HorizontalAlign="Left" Width="2%" />              
                        </asp:TemplateField>              
                        <asp:TemplateField HeaderText="Tiêu đề">
                            <ItemTemplate> 
                                <%#Eval("Title")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="68%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-BorderWidth="0px" HeaderText="Xóa">
				            <ItemTemplate>
					            <asp:LinkButton id="cmdDelete" runat="server" CommandName="Delete" CausesValidation="false">Xóa</asp:LinkButton>
				            </ItemTemplate>

<HeaderStyle BorderWidth="0px"></HeaderStyle>

                            <ItemStyle HorizontalAlign="Left" Width="8%" BorderWidth="0px" />
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
            <td align="right">
                <asp:Label ID="lblCurrentPage" runat="server"></asp:Label>
                <asp:Button ID="btnPrev" Text="Trang trước" runat="server" 
                    onclick="btnPrev_Click" /> 
                <asp:Button ID="btnNext" Text="Trang sau"  runat="server" 
                    onclick="btnNext_Click"/>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:CheckBox ID="cbInsertImage" runat="server" Text="Có ảnh và trích dẫn" />
                <asp:Button runat="server" ID="btnSelect" Text="Chèn vào bài viết" 
                    onclick="btnSelect_Click" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
