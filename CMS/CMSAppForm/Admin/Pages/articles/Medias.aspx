<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="Medias.aspx.cs" Inherits="Admin_Medias" %>

<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#iframemedia").height( Math.max($(window).height()) - 240);
        });
        
    </script>
    <iframe frameborder="0" width="100%" height="500px" id="iframemedia" src="<%= ICSoft.CMSLib.CmsConstants.ROOT_PATH %>Integrated/ckfinder/ckfinder.html?langCode=<%= LanguageCode %>" ></iframe>
</asp:Content>
