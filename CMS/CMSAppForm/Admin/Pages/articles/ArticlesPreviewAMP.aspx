﻿<%@ Page Title="" Language="C#"  ValidateRequest="false"
AutoEventWireup="true" CodeFile="ArticlesPreviewAMP.aspx.cs" Inherits="Admin_ArticlesPreviewAMP" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<!doctype html>
<html ⚡>
  <head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width,minimum-scale=1,initial-scale=1">
    <title><%= MetaTitle %></title>
    <meta name="description" content="<%= MetaDesc %>" />
    <meta name="robots" content="noindex,nofollow,noodp" /> 
    <meta name="mobile-web-app-capable" content="yes" />
    <meta name="abstract" content="Luật Việt Nam" />
    <meta name="distribution" content="Global" />
    <meta name="RATING" content="GENERAL" />
    <meta name="MobileOptimized" content="width" />
    <meta name="HandheldFriendly" content="true" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <link rel="canonical" href="<%= Url %>" />
    <link rel="apple-touch-startup-image" href="<%=Image %>" />
    <link rel="apple-touch-icon" href="<%=Image %>"/>
    <link rel="apple-touch-icon-precomposed" sizes="108x40" href="<%=Image %>" />
    <link rel="shortcut icon" sizes="20x20" href="http://luatvietnam.vn/assets/images/favicon.ico" />
    <link href="https://plus.google.com/u/1/communities/113796949546534964680" rel="publisher" />
    <meta name="copyright" content="Copyright © <%# DateTime.Now.Year %> by luatvietnam.vn" />
  
    <script async src="https://cdn.ampproject.org/v0.js"></script>
	<script async custom-element="amp-sidebar" src="https://cdn.ampproject.org/v0/amp-sidebar-0.1.js"></script>
    <script async custom-element="amp-form" src="https://cdn.ampproject.org/v0/amp-form-0.1.js"></script>
    <script async custom-element="amp-accordion" src="https://cdn.ampproject.org/v0/amp-accordion-0.1.js"></script>
    <script async custom-element="amp-analytics" src="https://cdn.ampproject.org/v0/amp-analytics-0.1.js"></script>
    <script async custom-element="amp-install-serviceworker" src="https://cdn.ampproject.org/v0/amp-install-serviceworker-0.1.js"></script>
    <script type="application/ld+json">
          {
            "@@context" : "http://schema.org",
            "@@type" : "Organization",
            "name" : "luatvietnam.vn",
            "url" : "http://luatvietnam.vn",
            "logo": "@Url.Content("~/assets//assets/images/amp/amp/logo.png")",
            "sameAs" : [
            "https://www.facebook.com/Luatvietnam.vn/",
            "https://plus.google.com/u/1/communities/113796949546534964680"]
          }
    </script> 
    <style amp-boilerplate>body{-webkit-animation:-amp-start 8s steps(1,end) 0s 1 normal both;-moz-animation:-amp-start 8s steps(1,end) 0s 1 normal both;-ms-animation:-amp-start 8s steps(1,end) 0s 1 normal both;animation:-amp-start 8s steps(1,end) 0s 1 normal both}@-webkit-keyframes -amp-start{from{visibility:hidden}to{visibility:visible}}@-moz-keyframes -amp-start{from{visibility:hidden}to{visibility:visible}}@-ms-keyframes -amp-start{from{visibility:hidden}to{visibility:visible}}@-o-keyframes -amp-start{from{visibility:hidden}to{visibility:visible}}@keyframes -amp-start{from{visibility:hidden}to{visibility:visible}}</style><noscript><style amp-boilerplate>body{-webkit-animation:none;-moz-animation:none;-ms-animation:none;animation:none}</style></noscript>
<style amp-custom>
/* Begin fix */
  body { margin:0; padding:0; font-family:Arial,Helvetica,sans-serif; font-size:14px; color:#111; }
  img { border:none; }
  a { text-decoration:none; color:#111; }
  a:hover { color:#a67942; }
  h1,h2,h3,h4,h5,h6 { margin:0; padding:0; font-weight:normal; }
  p { margin:0; padding:0; color:#333; }
  ul,li { margin:0; padding:0; list-style:none; }
  button { background:0; border:0;cursor: pointer; }
  input, button, select, textarea:focus {  outline: 0; font-family: Arial, Helvetica, sans-serif;font-size:13px;}
  input{ -webkit-appearance: none; padding: 0;}
  .breadcrumb {    float: left;    background: #f4f4f4;    padding: 8px 0; font-size: 14px;    width: 100%;    margin-bottom: 10px;color:#555;}
  .breadcrumb-item { color:#555; font-size:14px; padding: 0 5px; }
  .box-search{float: left; width: 100%; margin-bottom: 10px;}
.search-form {  position: relative;  width: 100%;}
.search-form:focus {  outline: 0;  box-shadow: 0 0 2px rgba(85, 168, 236, 0.9);}
.search-form :focus + .search-dropdown {  display: block; }

.search-input {box-shadow: 0 0 2px 0 rgba(0,0,0,0.16),0 0 0 1px rgba(0,0,0,0.08); color: #777;
   -webkit-appearance: none; height: 35px; line-height: 35px;  width: 100%; text-indent: 5px;  border: none; float: left;font-style: italic;}
.search-input:focus, .search-input:hover { outline: 0;box-shadow: 0 1px 5px 0 rgba(0,0,0,0.2),0 0 0 1px rgba(0,0,0,0.08) }
  
.search-dropdown { background: #fff;  display: none;  position: absolute; margin: 0; padding: 0;  top: 36px;  left: 0;  right: 0;  z-index: 10; box-shadow: 0 2px 2px 0 rgba(0,0,0,0.16),0 0 0 1px rgba(0,0,0,0.08); max-height: 250px; overflow: auto;}
.search-dropdown:hover {  display: block;}
.item-search {    padding: 8px 0;    font-size: 13px;    list-style: none;    color: #333;    float: left;    width: 100%;}
.item-search:hover{ background: #f2f2f2; }

.keyword {    float: left; padding: 0 10px;    width: 80%; font-weight: bold;}
.delete-keyword {    color: #555;    float: right; cursor: pointer; margin-right:5px;}

.icon-search {    position: absolute;    float: right;    right: 10px; cursor: pointer;    margin-top: 10px;}
.search-q {    height: 33px;    float: left;    width: 100%;     line-height: 33px;    background: none;border: none;    text-indent: 5px;}
.btn-isearch {background:url(/assets/images/amp/fa-search1.png) no-repeat 10px 10px;    float: right;    cursor: pointer;    height: 35px; width: 35px;    border: none;    background-color: #fff;  position: absolute;  right: 5px;}
/* End fix */

/* Begin sidebar */
  #btn-menu { float:left; left:0; top:10px; position:absolute; z-index:9999;  }
  #btn-search { margin-right:5px; right:0; color:#fff; top:0; display:inline-block; position:fixed; padding:0 5px; line-height:1; z-index:99; }
  #top-search-form { background:#666; display:none; font-size:14px; color:#fff; height:45px; line-height:45px; position:relative; width:100%; }
  #top-search-form { display:block; }
  #top-search-form.active { display:block; }
  #top-search-form #search-term { border:none; border-radius:0; color:#fff; background:none; width:84%; text-indent:10px; }
  #top-search-form #btn-search-form,#top-search-form #btn-close-form { position:absolute; height:45px; top:0; }
  #top-search-form #btn-close-form { left:0; }
  #top-search-form #btn-search-form { right:0; }
  #sidebar { top:0; left:-270px; background:#fff; position:fixed; height:100%; width:270px; overflow-x:scroll; }
  #sidebar .close-sidebar button { position:absolute; right:0; border:0; background:0; color:#fff; text-align:right; top:52px; }
 .navitemli a {  display:block;  }
.navitemli {    border-top: 1px solid #ddd;    text-transform: uppercase;font-weight: bold;font-size:14px;     font-weight: bold; padding: 13px 0;    padding-left: 15px;}
/* End sidebar */

	.select-op {
     -webkit-appearance: none;  /*Removes default chrome and safari style*/
      -moz-appearance: none; /* Removes Default Firefox style*/
      background:url(/assets/images/amp/mt.png) no-repeat 95% 12px;
      text-overflow: "";  /*Removes default arrow from firefox*/
    width: 100%;    height: 30px;    line-height: 30px;margin-bottom: 10px;    border-radius: 0;    border: 0;    color: #222; cursor: pointer;    font-size: 14px;    font-weight: bold;    font-family: arial;    float: left;    background-color: #fff;}
.select-op:hover {color:#a67942;}
.select-op:focus {    background-color: #fff;}
/* Begin header */
    #main-header { background:#f5f5f5; height:45px; width:100%; z-index:9; position: relative;}
  .logomobile { float:left; position:absolute; text-align:center; width:100%; height:45px; }
  .logo { background:url(/assets/images/amp/logo.png) no-repeat center center; -moz-box-align:center; -moz-box-pack:center; display:-webkit-box; height:45px; display:inline-block; width:150px; text-indent:-20000px; }
  .icon-lich { float:right; width:30px; position:absolute; right:0; }
  .icon-lich-item { background:url(/assets/images/amp/chon-ngay.png) no-repeat 6px 6px; float:left; height:30px; width:100%; margin-top:10px; z-index:9999; }
.right-btn {    float: right;    position: absolute;    right: 4px;}
  .select-btn {font-weight: bold; height: 30px;
     -webkit-appearance: none;  /*Removes default chrome and safari style*/
      -moz-appearance: none; /* Removes Default Firefox style*/
      background:url(/assets/images/amp/more-nv.png) no-repeat 95% 6px;
      text-overflow: "";  /*Removes default arrow from firefox*/
    width: 100%;  padding-right: 20px;text-align: right;
    border-radius: 0;    border: 0;  cursor: pointer;    font-size: 14px;       font-family: arial;    float: left;    background-color: none;}
.select-btn:focus {    background-color: 0;}
.id0 {    display: none;}
/* End header */

/* Begin menutop_bar */
  .nav-main { background: #fff; float: left; overflow: hidden; width: 100%; height: 35px; line-height: 35px; border-bottom: solid 1px #ddd;margin-bottom: 5px;}  
  .nav-main_item {    border-right: 1px solid #ddd;    float: left;  width: 24.6%; text-align: center;}
  .nav-main_item:nth-child(4n+4) { border-right:0; }
  .nav-main_item > a { float: left; font-weight: bold; width: 100%; font-size: 12px; text-transform: uppercase; }
  .nav-main_item > a:hover { color: #d42424;  }
.subnav li {    border: 0;    text-transform: none;    padding: 8px 0px;    font-weight: bold; }
.subnav li a {padding-left: 3px;}
.subnav {    padding-left: 30px;}
.icon-home{background:url(/assets/images/amp/ic_home_24px.png) no-repeat 0 3px; float: left; width: 16px; height: 16px;margin-right: 5px;}
.icon-about{background:url(/assets/images/amp/pen.png) no-repeat 0 3px; float: left; width: 16px; height: 16px;margin-right: 5px;}
.icon-tracuu{background:url(/assets/images/amp/ic_find_in_page_24px.png) no-repeat 0 0; float: left; width: 16px; height: 16px;margin-right: 5px;}
.icon-new{background:url(/assets/images/amp/ic_library_books_24px.png) no-repeat 0 3px; float: left; width: 16px; height: 16px;margin-right: 5px;}
.icon-hd{background:url(/assets/images/amp/ic_help_24px.png) no-repeat 0 0; float: left; width: 16px; height: 16px;margin-right: 5px;}
.icon-contact{background:url(/assets/images/amp/ic_perm_phone_msg_24px.png) no-repeat 0 3px; float: left; width: 16px; height: 16px;margin-right: 5px;}
/* End menutop_bar */

/* Begin container */
  .container { padding:0 5px;padding-top: 5px;}
  .content-section { margin-bottom:15px; float:left; width:100%; }
  .cat-box-content { float:left; width:100%; }
  .first-news { float:left; width:100%; }
  .post-title.first { float:left; font-size:16px; font-weight:bold; padding-bottom: 10px; width:100%; }
  .sapo { line-height:20px; }
  .other-news {float:left; padding:10px 0; width:100%; }
  .post-title { font-size:15px; }
  .post-title.other { float:left; width:100%; font-weight:bold; }
  .post-thumbnail { float:left; width:40%; margin-right:10px; }
  .xem-them { float:left; text-align:center; width:100%; }
  .xem-them > a { background:#f2f2f2; color:#444; float:left; height:40px; line-height:40px; width:100%; }
  .xem-them > a:hover { background:#f8f8f8; color:#c50000; }
  .page-header { border-top:2px solid #c50000; padding:8px 0; }
  .box-ads { float:left; text-align:center; margin-bottom:15px; width:100%; }
  .title-header { color:#c50000; font-weight:bold; text-transform:uppercase; font-size: 18px;}
/* End container */

/* Bengin footer */
.theme-footer { float:left; width:100%; background: #f2f2f2; padding: 10px 0;}
.nav-bottom { float:left; width:100%; }
.nav-bottom-item { float:left; font-size:13px; font-weight:bold; padding:10px 0; text-indent:10px; text-transform:uppercase; width:50%; }
.copyright { background:#333 none repeat scroll 0 0; color:#fff; float:left; width:100%; padding:10px 0; }
.copyright > p { color:#fff; font-size:13px; padding:0 5px; line-height:20px; }

.backtop {    bottom: 5px;    float: right;    position: fixed;    right: 5px;}
.iconbacktop{ background:url(/assets/images/amp/backtop.png) no-repeat; float:left; width:36px; height:36px; }
/* End footer */
.title {    float: left;    background: #a67942;    width: 100%;    height: 30px;    margin-bottom: 10px;}
.title-cat {    float: left;    color: #fff;    line-height: 30px;    text-transform: uppercase;    font-size: 15px;    font-weight: bold;    padding-left: 10px;}
.more-new { background:url(/assets/images/amp/xem-tiep2.png) no-repeat 0 10px;    float: right; text-indent: 15px;    color: #fff;    padding-top: 7px;    margin-right: 10px;font-size: 12px;}
.more-new:hover {    color: #fff; text-decoration: underline; }
.fa-m {    float: left;    margin-top: 5px;    width: 4px;    height: 8px;    margin-right: 5px;    background: url(/assets/images/amp/mtdp.png) no-repeat;}
.post-time {    font-size: 15px;    float: left;    width: 100%;    margin-top: 6px;    color: #444;}
.post-time-item {    float: left;    margin-right: 3%;}
.other-news.dashed {    border-top: dashed 1px #ddd;}
.color2 {    color: #90622a;    font-weight: bold;}

/* Begin footer*/
.footer{float: left; width: 100%; background: #f4f4f4;padding: 5px 0;}
.col2-bottom{float: left; width: 50%;}
.tie-bottom {    text-transform: uppercase;    font-weight: bold; padding: 5px 0;padding-top: 10px;}
.item-bottom {    padding: 5px 0;line-height: 20px;}
.nav-bottom {    float: left;    width: 100%;}
.vien-btom.m12 {    margin-top: 0;}
.vien-btom {    float: left;    width: 100%;    height: 1px;    background: #ddd;    position: relative;    margin-top: 20px;margin-bottom: 10px;}
.logo-bottom {    float: left;    background: #fff;    position: absolute;    z-index: 9;}
.logo-bottom img {    float: left; height: 24px;}
.item-bottom-2 {    padding: 5px 0;}
.fa-dot {    float: left;    background: #666666;    height: 4px;
    width: 4px;    border-radius: 100%;    margin-top: 5px;    margin-right: 5px;    margin-left: 10px;}
.tiedv { background: #fff;    float: left; z-index: 1;    text-transform: uppercase;    font-weight: bold;    position: absolute;}
.send-mail {    float: left;    width: 100%; margin: 10px 0;}
.icon-search.mail {    margin: 0;    right: 0;}
.icon-search.mail img {float: right; height: 35px;}
.fditem1 {    float: left; font-size: 12px;    margin-right: 15px;}
.footer-item {    float: left;    width: 100%;    padding: 5px 0;}
.fa-footer {    float: left;    margin-left: 10px;    margin-right: 5px;}
.socal{float: right}
.fa-i1 {    float: left;    margin-top: 1px;margin-left: 5px;    width: 13px;    height: 13px;    margin-right: 5px;    background: url(/assets/images/amp/ic_local_phone_24px.png) no-repeat;}
.fa-i2 {    float: left;    margin-top: 1px;margin-left: 5px;    width: 13px;    height: 13px;    margin-right: 5px;    background: url(/assets/images/amp/ic_phone_android_24px.png) no-repeat;}
.fa-i3 {    float: left;    margin-top: 1px;margin-left: 5px;    width: 13px;    height: 13px;    margin-right: 5px;    background: url(/assets/images/amp/famail-1.png) no-repeat;}
.fb1 {background: url(/assets/images/amp/Group-4.png) no-repeat;    float: left;    height: 20px;    width: 20px;    color: #a67942;   margin-right: 10px;}
.fb2 {background: url(/assets/images/amp/Group-5.png) no-repeat;    float: left;    height: 20px;    width: 20px;    color: #a67942;    margin-right: 10px;}
.fb3 {background: url(/assets/images/amp/Group-6.png) no-repeat;    float: left;    height: 20px;    width: 20px;    color: #a67942;    margin-right: 10px;}
.fb4{background: url(/assets/images/amp/Group-7.png) no-repeat;    float: left;    height: 20px;    width: 20px;    color: #a67942;    margin-right: 10px;}
.footer-item.copyr {    text-align: center;    font-size: 12px;}
/* End footer*/
.box-tinvan {
    line-height: 20px;
    float: left;
    margin-bottom: 10px;
    width: 100%;
    background: #fff5e9;
}
.box-tinvan p {
    padding: 10px;
    line-height: 20px;
}

.imgtvan {margin-right: 5px; background: url(/assets/images/amp/icon-sms.png) no-repeat; float: left; width: 19px; height: 15px; }
.post-title.bold {
    font-weight: bold;
}
.other-news.p5 {
    padding: 5px 0;
}
.menu-icon{float: left;
    width: 16px;
    height: 16px;
    margin-right: 5px;}
.item-time {    font-size: 12px;    color: #666;}
.pks {    padding-left: 44%;}
  .title2 {
      float: left;
      width: 100%;
      margin-bottom: 10px;
  }
  .vien {
      float: left;
      border-bottom: solid 2px #b28247;
      line-height: 30px;
  }
  .title-cat2 {
      font-size: 14px;
      text-transform: uppercase;
      line-height: 24px;
      font-weight: bold;
  }
  .the-post {    float: left;    margin-bottom: 15px;    width: 100%;}
  .title-post-singer {    font-size: 18px; padding-top: 10px; font-weight: bold;}
  .time-post {    color: #666;    font-size: 14px;}
  .entry {    color: #333;    font-size: 16px;    line-height: 24px;}
  .entry > h2 {    font-size: 16px;    padding-top: 10px;}
  .img-entry {    padding-top: 10px;}
  .entry > p {    color: #333;    padding: 10px 0;}
  .title2 {    float: left;    width: 100%;    margin-bottom: 10px;}
  .vien {    float: left;    border-bottom: solid 2px #b28247;    line-height: 30px;}
  .title-cat2 {    font-size: 14px;    text-transform: uppercase;    line-height: 24px;    font-weight: bold;}
  .post-title.bold {    font-weight: bold;}
</style>
</head>
<body>
    <div class="container">
        <article class="the-post">
    <div class="singer">
        <div class="time-post"><%= PublishTime %></div>
        <h1 class="title-post-singer"><%= Title %></h1>
        <div class="entry">
            <%= Content %>
        </div>
    </div>
</article>
    </div>
</body>
</html>