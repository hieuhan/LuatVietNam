<%@ Page Language="C#"  AutoEventWireup="true"  CodeFile="RegistAction.aspx.cs" Inherits="ICSoft.AppForm.RegistAction" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Đăng ký chức năng quản trị</title>
     <script type="text/javascript">
         
         function SelectAll(CheckBoxControl) {
             if (CheckBoxControl.checked == true) {
                 var i;
                 for (i = 0; i < document.forms[0].elements.length; i++) {
                     if ((document.forms[0].elements[i].type == 'checkbox') && (document.forms[0].elements[i].name.indexOf('chkAction') > -1)) {
                         document.forms[0].elements[i].checked = true;
                     }
                 }
             }
             else {
                 var i;
                 for (i = 0; i < document.forms[0].elements.length; i++) {
                     if ((document.forms[0].elements[i].type == 'checkbox') && (document.forms[0].elements[i].name.indexOf('chkAction') > -1)) {
                         document.forms[0].elements[i].checked = false;
                     }
                 }
             }
         }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <a href="RegistAction.aspx">Home</a>
        </div>
        <asp:GridView ID="m_Grid" runat="server" AutoGenerateColumns="false" Width="100%">
        <Columns>
           
            <asp:TemplateField HeaderText="STT">
                <ItemTemplate>
                   <asp:TextBox ID="tbActionOrder" runat="server" Text='<%# Eval("ActionOrder").ToString() %>' ></asp:TextBox>
                </ItemTemplate>               
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Tên chức năng">
                <ItemTemplate>
                    <asp:TextBox ID="tbActionName" runat="server" Text='<%#Eval("ActionName")%>' ></asp:TextBox>
                    <br />
                   <a href="?Folder=<%#Eval("ActionName")%>"> <%#Eval("ActionName")%></a>
                </ItemTemplate>
                
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Mô tả">
                <ItemTemplate>
                    <asp:TextBox ID="tbActionDesc" runat="server" Text='<%#Eval("ActionDesc")%>' ></asp:TextBox>
                </ItemTemplate>
                
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Liên kết">
                <ItemTemplate>
                    <asp:TextBox ID="tbUrl" runat="server" Text='<%#Eval("Url")%>' ></asp:TextBox>
                </ItemTemplate>
                
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Chức năng cha">
                <ItemTemplate>
                    <asp:TextBox ID="tbParentActionId" runat="server" Text='<%#Eval("ParentActionId")%>' ></asp:TextBox>
                    <br />
                    <%# Actions_GetDesc(Convert.ToInt16(Eval("ParentActionId")))%>
                </ItemTemplate>
                
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Trạng thái">
                <ItemTemplate>
                    <asp:TextBox ID="tbActionStatusId" runat="server" Text='<%#Eval("ActionStatusId")%>' ></asp:TextBox>
                    <br />
                    <%# ActionStatus_GetDesc(Convert.ToByte(Eval("ActionStatusId")))%></ItemTemplate>
                
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Hiển thị" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="chkDisplay" runat="server"  Checked="true" />
                </ItemTemplate>
                
                <FooterStyle HorizontalAlign="Center" />
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
        <asp:GridView ID="m_GridFile" runat="server" AutoGenerateColumns="false" Width="100%">
        <Columns>
           
            <asp:TemplateField HeaderText="STT">
                <ItemTemplate>
                   <asp:TextBox ID="tbActionOrder" runat="server" Text='<%# Eval("ActionOrder").ToString() %>' ></asp:TextBox>
                </ItemTemplate>               
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Tên chức năng">
                <ItemTemplate>
                    <asp:TextBox ID="tbActionName" runat="server" Text='<%#Eval("ActionName")%>' ></asp:TextBox>
                </ItemTemplate>
                
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Mô tả">
                <ItemTemplate>
                    <asp:TextBox ID="tbActionDesc" runat="server" Text='<%#Eval("ActionDesc")%>' ></asp:TextBox>
                </ItemTemplate>
                
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Liên kết">
                <ItemTemplate>
                    <asp:TextBox ID="tbUrl" runat="server" Text='<%#Eval("Url")%>' ></asp:TextBox>
                </ItemTemplate>
                
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Chức năng cha">
                <ItemTemplate>
                    <asp:TextBox ID="tbParentActionId" runat="server" Text='<%#Eval("ParentActionId")%>' ></asp:TextBox>
                    <br />
                    <%# Actions_GetDesc(Convert.ToInt16(Eval("ParentActionId")))%>
                </ItemTemplate>
                
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Trạng thái">
                <ItemTemplate>
                    <asp:TextBox ID="tbActionStatusId" runat="server" Text='<%#Eval("ActionStatusId")%>' ></asp:TextBox>
                    <br />
                    <%# ActionStatus_GetDesc(Convert.ToByte(Eval("ActionStatusId")))%></ItemTemplate>
                
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Hiển thị" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="chkDisplay" runat="server" Checked="true" />
                </ItemTemplate>
                
                <FooterStyle HorizontalAlign="Center" />
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
    <div>
           <asp:Button ID="btSave" runat="server" Text="Lưu lại" onclick="btSave_Click" />
    </div>
    
    </form>
</body>
</html>

