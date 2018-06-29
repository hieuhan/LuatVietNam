<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="CustomerField_Add.aspx.cs" Inherits="Admin_CustomerField_Add" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <div style="text-align:center; padding:5px;"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></div>
    
    <b style="color:red; font-size:18px; margin:10px;"> Lĩnh vực quan tâm</b>
    <div style="width:100%; height:auto; overflow:auto;">
        <asp:CheckBoxList ID="chkFields" DataTextField="FieldDesc" DataValueField="FieldId" RepeatLayout="table" RepeatColumns="3" RepeatDirection="Vertical" runat="server">
        </asp:CheckBoxList>
    </div>
    <%--<hr />
    <b style="color:red; font-size:18px; margin:10px;"> Lĩnh vực TCVN quan tâm</b>
    <div style="width:100%; height:200px; overflow:auto;">
        <asp:CheckBoxList ID="chkFieldsTCVN" DataTextField="FieldDesc" DataValueField="FieldId" RepeatLayout="table" RepeatColumns="4" RepeatDirection="Vertical" runat="server">
        </asp:CheckBoxList>
    </div>--%>
    <div style="text-align:center; margin-top:30px;">
        <asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom" 
                    Text="Lưu thông tin" onclick="btnSave_Click">
        </asp:LinkButton>
    </div>
</asp:Content>

