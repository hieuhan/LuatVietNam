<%@ Page Title="" Language="C#" MasterPageFile="~/admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="InternalLinkConfig.aspx.cs" Inherits="admin_pages_SeoInfo_InternalLinkConfig" %>

<asp:Content ID="Content2" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <div style="margin-left: 10px;">
            <p>Ví dụ:</p>
            <p>xổ số, xoso, xosotructiep, xsmb, http://xoso.com.vn</p>
            <p>lich am, lich ngay tot, lịch ngày tốt, lich, http://lichngaytot.vn</p>
            <p>
                <asp:TextBox ID="icp_keywords" TextMode="multiline" Columns="100" Rows="10" runat="server" />
            </p>
            <p>Số link chèn max trong một bài viết (Một bài viết được chèn bao nhiêu link nội bộ)</p>
            <p>
                <asp:TextBox ID="icp_maxlink" runat="server" Text="3" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="icp_maxlink" runat="server" ErrorMessage="Chỉ cho phép nhập số" ValidationExpression="\d+"></asp:RegularExpressionValidator>
            </p>
            <p>Số link được lặp lại trong một bài viết (Link chèn được sử dụng một hay nhiều lần trong bài viết)</p>
            <p>
                <asp:TextBox ID="icp_looplink" runat="server" Text="1" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="icp_looplink" runat="server" ErrorMessage="Chỉ cho phép nhập số" ValidationExpression="\d+"></asp:RegularExpressionValidator>
            </p>
            <p>Domain http://</p>
            <p>
                <asp:TextBox ID="icp_domain" runat="server" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="icp_domain" runat="server" ErrorMessage="Không được rỗng" ValidationExpression="([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?"></asp:RegularExpressionValidator>
            </p>
            <p>
                <asp:CheckBox ID="icp_giveil" runat="server" Text="Giữ lại Internal Links đã có" Visible="false"/>
            </p>
            <p>
                <asp:CheckBox ID="icp_nofollow" runat="server" Text="Add nofollow attribute" />
            </p>
            <p>
                <asp:CheckBox ID="icp_targetblank" runat="server" Text="Open in new window" />
            </p>
            <p>
                <asp:Button runat="server" ID="btnInsert" Text="Cập nhật"
                    OnClick="btnInsert_Click" />
            </p> 
            
        </div>
</asp:Content>

