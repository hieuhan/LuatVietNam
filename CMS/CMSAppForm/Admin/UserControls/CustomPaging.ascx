<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomPaging.ascx.cs" Inherits="ICSoft.AppForm.Admin.CustomPaging" %>
<div class="pages">
<%
    if (PageIndex == 1)
    { %>
        <span class="dautrang curentpage" >«</span>
    <% } else { %>
        <a onclick="getvalue('1');" href="javascript:void(0);" class="dautrang" >«</a>
    <% }
        for (int i = startpage; (i <= TotalPage) && (i <= endpage); i++) {
        page = i.ToString();
        if (PageIndex != i) { %>
            <a onclick="getvalue(<%= page%>);" href="javascript:void(0);" class="dautrang" ><%= page %></a>
      <%} else { %>
            <span class="dautrang curentpage"><%= page%></span>       
      <%} }
          if ((PageIndex == TotalPage) || (TotalPage == 0)) { %>
        <span class="dautrang curentpage" >»</span>
    <% } else { %>
        <a onclick="getvalue('<%= TotalPage %>');" href="javascript:void(0);" class="dautrang" >»</a>
    <%} %>
</div>
 <div style="display:none;">
<asp:HiddenField ID="hdfPageIndex" Value="1" runat="server" />
<asp:HiddenField ID="hdfOldPage" Value="1" runat="server" />
<asp:HiddenField ID="hdfOldTotal" runat="server" />
 <asp:Button ID="btChange" runat="server" Text="Change" onclick="btChange_Click" CausesValidation="false"></asp:Button>
 </div>
 <script type="text/javascript">
    function getvalue(value) {
        $('#<%= hdfPageIndex.ClientID %>').val(value);
        $('#<%= btChange.ClientID %>').click();
    }
 </script>

