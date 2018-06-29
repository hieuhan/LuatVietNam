﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="ArticleEventStreams.aspx.cs" Inherits="Admin_ArticleEventStreams" %>
<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <div style="width:auto; height:auto; overflow:auto;">
        Site: <asp:DropDownList ID="ddlSite" runat="server" DataTextField="SiteDesc" 
            DataValueField="SiteId" Width="250px" CssClass="userselect"
            AutoPostBack="True" onselectedindexchanged="ddlSite_SelectedIndexChanged">
        </asp:DropDownList>
    </div>
    <div style="width:auto; height:350px; overflow:auto;">
        <asp:CheckBoxList ID="chkEventStreams" DataTextField="EventStreamName" DataValueField="EventStreamId" RepeatDirection="Vertical" runat="server">
        </asp:CheckBoxList>
    </div>
    <div style="text-align:center">
        <asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom" 
                    Text="Lưu thông tin" meta:resourcekey="btnSave" onclick="btnSave_Click">
        </asp:LinkButton>
    </div>
</asp:Content>
