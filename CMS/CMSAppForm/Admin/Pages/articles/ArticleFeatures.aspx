<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="ArticleFeatures.aspx.cs" Inherits="Admin_ArticleFeatures" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <div style="width:auto; height:350px; overflow:auto;">
        <asp:GridView ID="mGridFeature" runat="server" AutoGenerateColumns="False" DataKeyNames="FeatureId"
            Width="99%" CellPadding="2" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="Thuộc tính">
                    <HeaderStyle Width="150px" />
                    <ItemTemplate>
                        <%# Eval("FeatureName")%>:
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Giá trị">
                    <ItemTemplate>
                        <asp:TextBox ID="txtFeatureValue" runat="server" Visible="false" Width="90%"></asp:TextBox>
                        <asp:DropDownList ID="ddlFeatureValue"  Visible="false" DataTextField="DataDictionaryName" DataValueField="DataDictionaryId" runat="server"></asp:DropDownList>
                        <asp:CheckBoxList ID="cblFeatureValue" runat="server" RepeatDirection="Horizontal" RepeatColumns="3" Visible="false" DataTextField="DataDictionaryName" DataValueField="DataDictionaryId"></asp:CheckBoxList>
                        <asp:RadioButtonList ID="rblFeatureValue" runat="server" RepeatDirection="Horizontal" RepeatColumns="3" Visible="false" DataTextField="DataDictionaryName" DataValueField="DataDictionaryId"></asp:RadioButtonList>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
    </div>
    <div style="text-align:center">
        <asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom" 
                    Text="Lưu thông tin" meta:resourcekey="btnSave" onclick="btnSave_Click">
        </asp:LinkButton>
    </div>
</asp:Content>

