<%@ Page Language="C#" AutoEventWireup="true" CodeFile="errMsg.aspx.cs" Inherits="Admin_errMsg" %>
<%@ Import Namespace="ICSoft.HelperLib" %>
<%@ Import Namespace="ICSoft.CMSLib" %>
<%@ Import Namespace="sms.common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Từ chối truy cập</title>
    <link href="Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="SubVina/style.css" rel="stylesheet" type="text/css" />
    <link href="Styles/ddsmoothmenu.css" rel="stylesheet" type="text/css" />
    <link href="Styles/gridviewStyles.css" rel="stylesheet" type="text/css" />
    <link href="Styles/jquery-ui-1.8.23.custom.css" rel="stylesheet" type="text/css" />
    <link href="Styles/jquery.dropdown.css" rel="stylesheet" type="text/css" />
    <link href="Styles/msgBoxLight.css" rel="stylesheet" type="text/css" />
    <link href="Styles/jquery.notification.css" rel="stylesheet" type="text/css" />

    <script src="../../Scripts/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui-1.8.23.custom.min.js" type="text/javascript"></script>
    <script src="../../Scripts/FilterTable.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.msgBox.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.editinplace.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.cookie.js" type="text/javascript"></script>   
    <script src="../../Scripts/jquery.dropdown.js" type="text/javascript"></script>   
    <script src="../../Scripts/jquery.filtertable.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.notification.js" type="text/javascript"></script>
    <script src="../../../Integrated/ckfinder/ckfinder.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmMain" runat="server" method="post" enctype="multipart/form-data">   
 
<!--Begin bgwebbody-->
<div class="wrapper-full-site">
    <div class="header-content">
        <div class="title-post-header">
            <a href="<%=CmsConstants.PRJ_ROOT %>Pages/subvina/ThongTinThueBao.aspx" class=""> Chăm sóc Khách Hàng</a>
           	<a href="<%=CmsConstants.PRJ_ROOT %>Pages/subvina/DoanhThuNgay.aspx" class=""> Doanh thu theo ngày</a>
            <a href="<%=CmsConstants.PRJ_ROOT %>Pages/subvina/SanLuongNgay.aspx" class="">Sản lượng theo ngày</a>
            <a href="<%=CmsConstants.PRJ_ROOT %>Pages/subvina/Users.aspx" class="">Quản trị hệ thống</a>
        </div>
        <div class="main-nav-2">
            <a href="<%=CmsConstants.PRJ_ROOT %>Pages/subvina/ThongTinThueBao.aspx" class="">
            <img class="icon-admin" src="<%=CmsConstants.PRJ_ROOT %>images/icon-tra-cuu.png"/>Tra cứu thuê bao</a>
            <a href="<%=CmsConstants.PRJ_ROOT %>Pages/subvina/LichSuSuDung.aspx" class=""><img class="icon-admin" src="<%=CmsConstants.PRJ_ROOT %>images/icon-lich-su.png"/>Tra cứu sử dụng dịch vụ</a>
            <a href="<%=CmsConstants.PRJ_ROOT %>Pages/subvina/DangKyHuyDV.aspx" class=""><img class="icon-admin" src="<%=CmsConstants.PRJ_ROOT %>images/icon-cai-dat.png"/>Cài đặt dịch vụ</a>
            <a href="<%=CmsConstants.PRJ_ROOT %>Pages/subvina/ThongTinThueBao.aspx" class=""><img class="icon-admin" src="<%=CmsConstants.PRJ_ROOT %>images/icon-news.png"/>Thông tin dịch vụ</a>
        </div>
        <div class="main-nav-3">
            <a href="<%=CmsConstants.PRJ_ROOT %>Pages/subvina/LichSuDangKy_Huy.aspx" class="">Lịch sử đăng ký / huỷ</a>
            <a href="<%=CmsConstants.PRJ_ROOT %>Pages/subvina/LichSuTruCuoc.aspx" class="">Lịch sử trừ cước</a>
            <a href="<%=CmsConstants.PRJ_ROOT %>Pages/subvina/LichSuSuDung.aspx" class="">Lịch sử sử dụng</a>
            <a href="<%=CmsConstants.PRJ_ROOT %>Pages/subvina/TraCuuMO_MT.aspx" class="">Lịch sử MO / MT</a>
        </div>
    </div>
<!--Begin noidungconten-->
    <div class="container">
        <div class="main-content">
        <h1 style=" color:Red;" >Bạn chưa được cấp quyền truy cập cho chức năng này, liên hệ với quản trị viên để biết thêm chi tiết</h1>
        <div class="clear5px"></div>
        </div>		
    <!--End noidungconten-->
    </div>
<div class="clear5px"></div>
<!--End bgwebbody-->
</div>
</form>
</body>
</html>
