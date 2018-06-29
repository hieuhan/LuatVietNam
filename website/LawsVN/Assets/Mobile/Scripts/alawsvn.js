$(function () {
    lawsVn.init();
    lawsInfo();
    $(document).ajaxError(function (e, xhr) {
        if (xhr.status === 403) {
            var response = $.parseJSON(xhr.responseText);
            lawsVnConfig.returnUrl = response.ReturnUrl + '?ReturnUrl=' + lawsVnConfig.pathName;
            var activeDialogs = $(".ui-dialog:visible").find('.ui-dialog-content');
            activeDialogs.dialog('close');
            lawsVn.login();
        }
    });
});
var lawsVnConfig = {
    rootPath: '/',
    docContentViewLimit: 10,
    pathName: window.location.pathname,
    LanguageId: 1,
    DocGroupId: 0,
    arrLinhvuctracuu: new Array(),
    arrTrangthaihieuluc: new Array(),
    arrLoaivanban: new Array(),
    arrCoquanbanhanh: new Array(),
    arrNambanhanh: new Array(),
    arrAdvs: [],
    currentStep: 0,
    currentFontSize: 14,
    slideIndex: 0,
    pageCount: 0
};
var lawterminConfig = {
    rootPath: '/',
    docContentViewLimit: 10,
    pathName: window.location.pathname
};

var lawsVn = {
    init: function() {
        this.events();
    },
    initArticlePagination: function () {
        var getPage = function () {
            var p, pn = $('.pagination-next');
            if (pn.length) {
                var ajaxUrl = pn.data('ajax-url'), elUpdate = pn.data('ajax-update');
                ajaxUrl = ajaxUrl.replace('GetViewByCateId_Paging', 'GetViewByCateId_PagingV2');
                $('#pagination-loadmore').lawsExists(function () {
                    p = $(this).data('page');
                    if (p !== undefined) {
                        var regex = new RegExp("(page=)[0-9]+", 'ig');
                        ajaxUrl = ajaxUrl.replace(regex, '$1' + p);
                    }
                });
                $.lawsAjax({
                    type: 'Get',
                    dataType: 'html',
                    url: ajaxUrl,
                    success: function (resp) {
                        $('.pagination').remove();
                        $('.renderPagination').remove();
                        if (resp.length > 0) {
                            var pageIndex = $('#ArticlesByCateBox').find('.post-listing:last').data('page');
                            if (p !== undefined) pageIndex = p;
                            var idx = lawsVn.ajaxEvents.pageTitle.indexOf('- trang');
                            if (idx > 0)
                                lawsVn.ajaxEvents.pageTitle = lawsVn.ajaxEvents.pageTitle.substring(0, idx);
                            var pageTitle = pageIndex > 1
                                ? lawsVn.ajaxEvents.pageTitle + ' - trang ' + pageIndex
                                : lawsVn.ajaxEvents.pageTitle;
                            var url = lawsVn.replaceUrlParam({ 'page': pageIndex > 1 ? pageIndex : '' });
                            lawsVn.setTitleAndHistory(pageTitle, url);
                            $(elUpdate).append(resp);
                            $('.renderPagination:last').show();
                            $('.post-listing:last').lawsExists(function () {
                                $(this).LawScrollTo();
                            });
                            if (lawsVnConfig.pageCount > 2) {
                                $('#pagination-loadmore').lawsExists(function () {
                                    $(this).remove();
                                });
                            }
                        }
                    }
                });
            } else $('#pagination-loadmore').remove();
        };
        var scrollTime = null, scrollTimeDelay = 100, w = $(window);
        w.off('scroll', scrollToBottomHandler).on('scroll', scrollToBottomHandler);
        function scrollToBottomHandler() {
            clearTimeout(scrollTime);
            scrollTime = setTimeout(function() {
                w.scrollTop() + 200 > $(document).height() - w.height() && lawsVnConfig.pageCount < 3 && (getPage(), lawsVnConfig.pageCount++);
            }, scrollTimeDelay);
        }

        $(document).on('click',
            '#pagination-loadmore',
            function (event) {
                event.preventDefault();
                $(this).text('Đang tải dữ liệu...');
                getPage();
                lawsVnConfig.pageCount++;
            });
    },
    events: function() {
        window.onscroll = function () { lawsVn.scrollFunction() };
        //$(document).tooltip();
        this.widgetUser();
        if ($('[data-toggle-target]').length) {
            $('[data-toggle-target]').each(function () {
                $(this).mobilePopup();
            });
        }
        //if ($('#tin-van-mobi').lawsExists(function () {
        //    lawsVn.showSlides();
        //}));
        //setInterval(function () {
        //    $('#tin-vanm > p:first')
        //        .fadeOut(1000)
        //        .next()
        //        .fadeIn(1000)
        //        .end()
        //        .appendTo('#tin-vanm');
        //}, 10000);
        $(document).on('click',
            '.accordion',
            function (event) {
                event.preventDefault();
                var nextElement = $(this).next();
                if (nextElement.is(':hidden')) {
                    $('.accordion-content').slideUp();
                    nextElement.slideDown();
                } else nextElement.slideUp();
            });
        $(document).on('click',
            '.tab-nav-item1',
            function(event) {
                event.preventDefault();
                $('.tab-nav-item1').removeClass('active');
                $(this).addClass('active');
                var tabActive = $(this).data('id');
                $('.tab-item').hide(); 
                $(tabActive).show();
            });
        $(document).on('click',
            '.tab-nav-item2',
            function(event) {
                event.preventDefault();
                $('.tab-nav-item2').removeClass('active');
                $(this).addClass('active');
            });
        $(document).on('click',
            '.tab-nav-item3',
            function (event) {
                event.preventDefault();
                //$('.tab-nav-item1').removeClass('active');
                $(this).addClass('active');
                var tabActive = $(this).data('id');
                $('.tab-item-p').hide();
                $(tabActive).show();
                $('#renewalOfService2').LawScrollTo();
            });
        $(document).on('click',
            '.ListFieldDisplays input[type="checkbox"]',
            function () {
                //$('input[value="' + this.value + '"]:checkbox').prop('checked', this.checked);
                $('input[data-fieldname="' + $(this).data('fieldname') + '"]:checkbox').prop('checked', this.checked);
            });
        $(document).on('click', '.dropdown-bank dt a',
            function (event) { 
                event.preventDefault();
                $('.dropdown-visa dd').empty();
                $('.dropdown-bank dd ul').toggle();
            });
        $(document).on('click', '.dropdown-bank dd ul li a',
            function (event) {
                event.preventDefault();
                var bankName = $(this).html();
                $(".dropdown-bank dt a span").html(bankName);
                $(".dropdown-bank dd ul").hide();
            });
        $(document).on('click', '.dropdown-visa dt a',
            function (event) {
                event.preventDefault();
                $('.dropdown-bank dd').empty();
                $('.dropdown-visa dd ul').toggle();
            });
        $(document).on('click', '.dropdown-visa dd ul li a',
            function (event) {
                event.preventDefault();
                var bankName = $(this).html();
                $(".dropdown-visa dt a span").html(bankName);
                $(".dropdown-visa dd ul").hide();
            });
        $(document).on('click',
            '.tab-nav-item3',
            function(event) {
                event.preventDefault();
                $('.tab-nav-item3').removeClass('active');
                $(this).addClass('active');
            });
        $(document).on('click',
            '.vb-khong-co-nd',
            function (event) {
                event.preventDefault();
                $().lawsDialog({
                    dialogClass: 'lawsVnDialogTitle',
                    messages: ['Văn bản không có nội dung download']
                });
            });
        $(document).on('click',
            '#fbShare',
            function (e) {
                e.preventDefault();
                var facebookWindow = window.open('https://www.facebook.com/sharer/sharer.php?u=' + document.URL, 'facebook-popup', 'channelmode=1,scrollbars=1,status=0,titlebar=0,toolbar=0,resizable=1');
                if (facebookWindow.focus) { facebookWindow.focus(); }
                return false;
            });

        $(document).on('click',
           '.socialShare',
           function (e) {
               e.preventDefault();
               var social = $(this).data('social');
               var shareUrl;
               switch (social) {
                   case 'facebook':
                       {
                           shareUrl = 'https://www.facebook.com/sharer/sharer.php?u=';
                           break;
                       }
                   case 'gplus':
                       {
                           shareUrl = 'https://plus.google.com/share?url=';
                           break;
                       }
                   case 'twitter':
                       {
                           shareUrl = 'https://twitter.com/share?url=';
                           break;
                       }
                   default:
                       {
                           shareUrl = '';
                           break;
                       }
               }
               //$.ajaxSetup({ cache: true });
               //$.getScript('//connect.facebook.net/en_US/sdk.js', function(){
               //    FB.init({
               //        appId: '1523947861021721',
               //        version: 'v2.3' // or v2.0, v2.1, v2.0
               //    });
               //    FB.ui({
               //        method: '',
               //        title: '',
               //        description: '',
               //        href: '',
               //    },
               //      function(response) {
               //          if (response && !response.error_code) {
               //              alert('Completed.');
               //          } else {
               //              alert('Cancel.');
               //          };
               //      });
               //});
               if (shareUrl !== '') {
                   var winTop = (screen.height / 2) - (350 / 2);
                   var winLeft = (screen.width / 2) - (600 / 2);
                   var socialWindow = window.open(shareUrl + document.URL,
                       'social-popup',
                       'menubar=no,toolbar=no,resizable=yes,scrollbars=yes,top=' +
                       winTop +
                       ',left=' +
                       winLeft +
                       ',height=350,width=600');
                   if (socialWindow.focus) {
                       socialWindow.focus();
                   }
               }
               return false;
           });

        $('.tab-nav-widget1').off('click').on('click',
            function(event) {
                event.preventDefault();
                $('.tab-nav-widget1').removeClass('active');
                $(this).addClass('active');
            });
        $('.sub-menu2-item').off('click').on('click',
            function() {
                $('.sub-menu2-item').removeClass('active');
                $(this).addClass('active');
            });
        $('.tab-nav-item-ad').off('click').on('click',
            function() {
                $('.tab-nav-item-ad').removeClass('active');
                $(this).addClass('active');
            });
        $('.fontpage').off('click').on('click',
            function() {
                $('.fontpage').removeClass('active');
                $(this).addClass('active');
            });
        $('.post-tag-abc').off('click').on('click',
            function() {
                $('.post-tag-abc').removeClass('active');
                $(this).addClass('active');
            });
        $(document).on('click',
            '.lawsVnLogin',
            function(e) {
                e.preventDefault();
                var activeDialogs = $(".ui-dialog:visible").find('.ui-dialog-content');
                activeDialogs.dialog('close');
                lawsVn.login();
            });
        $('.dv-tieuchuan').lawsExists(function () {
            $(this).find('p').slice(0, 7).show();
        });
        $('.law-step:first').lawsExists(function () {
            $(this).fadeIn();
        });
        $('.doc-content table').lawsExists(function () {

        });
        $('.doc-content-news table').lawsExists(function () {

        });
        $(document).on('click',
            '.forgot-password',
            function (event) {
                event.preventDefault();
                lawsVn.forgotPassword();
            });
        $(document).on('click',
            '.pass-no',
            function (event) {
                event.preventDefault();
                lawsVn.forgotPasswordMobile();
            });
        $('.xemthemdv-tieuchuan').on('click', function (e) {
            e.preventDefault();
            var item = $('.dv-tieuchuan').find('p:hidden');
            $(item).slice(0, 8).slideDown();
            //if (item.length === 0) {
            $(this).fadeOut('slow');
            //var childHeight = $('.rowsxemthem').outerHeight(true);
            //$(this).css('height', childHeight);
            //}
            $('.dv-tieuchuan').animate({
                    scrollTop: $(this).offset().top
                },
               1500).css('overflow-y', 'scroll');
        });
        $(document).on('click',
            '.send-question',
            function (event) {
                event.preventDefault();
                lawsVn.sendQuestion();
            });
        $(document).on('click',
            '.send-question-mobile',
            function (event) {
                event.preventDefault();
                lawsVn.sendQuestionMobile();
            });
        $('.doc-properties').tooltip({
            content: 'Vui lòng đợi...',
            track: true,
            open: function (evt, ui) {
                var id = this.id;
                var splitId = id.split('-');
                var docId = splitId[2];
                $.ajax({
                    url: lawsVn.virtualPath('/Ajax/GetDocProperties'),
                    type: 'Post',
                    data: { docId: docId },
                    dataType: 'html',
                    cache: true,
                    success: function (response) {
                        $('#' + id).tooltip('option', 'content', response);

                    }
                });
            },
            close: function (event, ui) {
                $(".ui-tooltip").hide();
            }
        });

        $(document).on('click',
            '.no-permission',
            function(e) {
                e.preventDefault();
                $().lawsDialog({
                    dialogClass: 'lawsVnDialogTitle',
                    messages: ['<div class="ui-tooltip-content"><p>Đây là tiện ích dành cho thành viên đăng ký dịch vụ.</p><em>Quý khách vui lòng <a href="#" class="lawsVnLogin" style="color: #a67942" title="Đăng nhập tài khoản"><strong>Đăng nhập</strong></a> tài khoản LuatVietNam và <a href="/dich-vu.html" title="Đăng ký dịch vụ"><strong style="color: #a67942">đăng ký sử dụng Dịch vụ tra cứu văn bản</strong></a>.</em></div>']
                });
            });

        $(document).on('click',
            '.no-permission-download',
            function (e) {
                e.preventDefault();
                $().lawsDialog({
                    dialogClass: 'lawsVnDialogTitle',
                    messages: ['Quý khách cần có quyền Tra cứu tiếng Anh hoặc Tra cứu Nâng cao để tải văn bản về.']
                });
            });

        //$('.no-permission').tooltip({
        //    disabled: true,
        //    close: function (event, ui) { $(this).tooltip('disable'); }
        //});

        //$(document).on('click','.no-permission', function () {
        //        $(this).tooltip({
        //            content: function () {
        //                return $(this).prop('title');
        //            },
        //            show: null,
        //            close: function (event, ui) {
        //                ui.tooltip.hover(
        //                    function () {
        //                        $(this).stop(true).fadeTo(400, 1);
        //                    },
        //                    function () {
        //                        $(this).fadeOut("400",
        //                            function () {
        //                                $(this).remove();
        //                            });
        //                    });
        //            }
        //        });
        //        $(this).tooltip("enable");
        //        $(this).tooltip("open");
        //    });

        //$('.no-permission-download').tooltip();

        //$('.no-permission-download').lawsExists(function () {
        //    $.each(this,
        //        function () {
        //            $(this).qtip({
        //                content: {
        //                    text: $(this).next('.tooltiptext')
        //                }, position: {
        //                    my: 'top left',
        //                    at: 'top right'
        //                }
        //            });
        //        });
        //});
        
        $(document).on('click',
            '.gop-y',
            function (event) {
                event.preventDefault();
                var docName = $(this).data('name');
                lawsVn.gopy(docName);
            });
        $(document).on('click',
            '.docPrint',
            function (event) {
                event.preventDefault();
                $('#docPrint').lawsExists(function() {
                    $(this).printThis({
                        pageTitle: 'Luật Việt Nam',
                        header: '<div class="hearder-left"><div class="logo"><img alt="luat viet nam" src="' + lawsVn.virtualPath('/assets/images/logo.png') + '"></div></div>',
                        loadCSS: lawsVn.virtualPath('/assets/print.css')
                    });
                });
            });
        $(document).on('click',
            '.doc-send-mail',
            function (event) {
                event.preventDefault();
                var docUrl = $(this).data('url');
                lawsVn.docSendMail(docUrl);
            });
        $('.lawsVnLawerQuestion').off('click').on('click',
            function(event) {
                event.preventDefault();
                var lawerid = $(this).data('id');
                lawsVn.LawerQuestion(lawerid);
            });
        $('.FaqViewDetail').off('click').on('click',
            function(event) {
                event.preventDefault();
                var FaqId = $(this).data('id');
                lawsVn.FaqViewDetail(FaqId);
            });
        $('.lawsVnChangePassword').off('click').on('click',
            function(event) {
                event.preventDefault();
                lawsVn.changePassword();
            });
        $('.lawsVnLogOut').off('click').on('click',
            function(event) {
                event.preventDefault();
                lawsVn.logOut();
            });
        $('.docUtilityPannel').off('click').on('click',
            function(event) {
                event.preventDefault();
                $('#docUtilityPannel').toggle();
            });
        $('.xoa-linh-vuc').off('click').on('click',
            function(event) {
                event.preventDefault();
                var customerFieldId = $(this).data('id');
                lawsVn.accountProfile.deleteOneFieldById(customerFieldId);
            });

        $('.select-customer-fields').off('click').on('click',
            function(event) {
                event.preventDefault();
                lawsVn.editCustomerFields();
            });
        $('.select-customer-fieldsV2').off('click').on('click',
            function (event) {
                event.preventDefault();
                lawsVn.editCustomerFieldsV2();
            });
        $('.select-customer-province').off('click').on('click',
            function(event) {
                event.preventDefault();
                lawsVn.editCustomerProvince();
            });
        $(document).on('click',
            '.getcaptcha',
            function (event) {
                event.preventDefault();
                var id = $(this).data('id');
                var prefix = $(this).data('prefix');
                $('#' + id).lawsExists(function () {
                    lawsVn.getCaptcha(id, prefix);
                });
            });
        $(document).on('click',
            '.quy-dinh-thanh-toan2',
            function (event) {
                event.preventDefault();
                var wWidth = $(window).width();
                var dWidth = wWidth * 0.9;
                $('<div id="lawsVnTermsConditionsView"></div>').lawsDialog({
                    title: '',
                    width: dWidth,
                    position: { my: "center", at: "top+50", of: window.top },
                    buttons: {},
                    hideClose: false,
                    onCreate: function () {
                        $('#loading').fadeIn('normal');
                    },
                    onOpen: function () {
                        $('#lawsVnTermsConditionsView').load(lawsVn.virtualPath('/Ajax/PartialTermsConditionsView'),
                            function () {
                            });
                        $('#loading').fadeOut('normal');
                    }
                });
            });

        //$(document).on('click',
        //    '.getcaptcha',
        //    function(event) {
        //        event.preventDefault();
        //        var prefix = $(this).data('prefix');
        //        $('#LawsCaptcha').lawsExists(function() {
        //            lawsVn.getCaptcha('LawsCaptcha', prefix);
        //        });
        //    });

        $('.btn-nap-the-luatvn').off('click').on('click',
            function(e) {
                e.preventDefault();
                var wWidth = $(window).width();
                var dWidth = wWidth * 0.9;
                $('<div id="NapTheLuatVN"></div>').lawsDialog({
                    title: '',
                    width: dWidth,
                    hideClose: false,
                    buttons: {},
                    onCreate: function () {
                        $('#loading').fadeIn('normal');
                    },
                    onOpen: function () {
                        $('#NapTheLuatVN').load(lawsVnConfig.rootPath + 'Ajax/PartialPaymentMethodsScratchCard',
                            function () {
                                var form = $('#PaymentMethodsScratchCardForm');
                                form.removeData('validator');
                                form.removeData('unobtrusiveValidation');
                                $.validator.unobtrusive.parse(form);
                            });
                        $('#loading').fadeOut('normal');
                    }
                });
            });
        $(document).on('click',
            '.dang-ky-dv',
            function (event) {
                event.preventDefault();
                var serviceId = $(this).data('id');
                $.lawsAjax({
                    url: lawsVn.virtualPath('/Ajax/KiemTraDvDangKy'),
                    data: { serviceId: serviceId },
                    success: function (resp) {
                        if (resp.Completed) {
                            if (resp.Data != null) {
                                if (resp.Data.IsRegistService < 1) { //chưa đăng ký dịch vụ
                                    if (resp.Data.ActionButton === 'Register') {
                                        if (resp.Data.FeeType === 'Free') {
                                            lawsVn.account.registerFreeService(serviceId);
                                            //window.location.href =
                                            //    lawsVn.virtualPath('/user/dang-ky-goi-dich-vu-mien-phi.html');
                                        } else if (resp.Data.FeeType === 'Trial') {
                                            lawsVn.account.registerTrialService(serviceId);
                                        } else if (resp.Data.FeeType === 'Sub') {
                                            window.location.href = lawsVn.virtualPath(
                                                '/user/dang-ky-goi-dich-vu.html?serviceId=' +
                                                serviceId);
                                        }
                                    } else if (resp.Data.ActionButton === 'ExpiredTrial') {
                                        if (resp.Data.FeeType === 'Trial') {
                                            window.location.href = lawsVn.virtualPath('/user/dang-ky-goi-dich-vu.html');
                                        }
                                    }
                                } else { //đã đăng ký dịch vụ: hiện thông báo tương ứng
                                    if (resp.Data.FeeType === 'Free' ||
                                        resp.Data.FeeType === 'Trial' ||
                                        resp.Data.ActionButton === '') {
                                        $().lawsDialog({
                                            dialogClass: 'lawsVnDialogTitle',
                                            messages: [
                                                resp.Data.Messages +
                                                '<br/><a href="' +
                                                lawsVn.virtualPath('/user/dang-ky-goi-dich-vu.html') +
                                                '" title="Đăng ký dịch vụ" style="color:#a67942">Đăng ký dịch vụ</a>'
                                            ]
                                        });
                                        // 
                                    } else if (resp.Data.ActionButton === 'NoAction') {
                                        $().lawsDialog({
                                            dialogClass: 'lawsVnDialogTitle',
                                            messages: [resp.Data.Messages]
                                        });
                                    } else if (resp.Data.ActionButton === 'ReNew' ||
                                        resp.Data.ActionButton === 'ReNewAndConvert') {
                                        var buttons = [
                                            {
                                                text: 'Gia hạn',
                                                'class': 'btn-nhap-lai dv',
                                                click: function () {
                                                    window.location.href =
                                                        lawsVn.virtualPath('/user/gia-han-dich-vu.html');
                                                }
                                            },
                                            {
                                                text: 'Chuyển đổi',
                                                'class': 'btn-nhap-lai dv',
                                                click: function () {
                                                    window.location.href =
                                                        lawsVn.virtualPath('/user/chuyen-doi-dich-vu.html?serviceId=' +
                                                            serviceId);
                                                }
                                            },
                                            {
                                                text: 'Đóng',
                                                'class': 'btn-nhap-lai dv2',
                                                click: function () {
                                                    $(this).dialog('close');
                                                }
                                            }
                                        ];
                                        //Không hỗ trợ nâng cấp dịch vụ - hiển thị button Gia hạn
                                        if (resp.Data.ActionButton === 'ReNew') {
                                            buttons.splice(1, 1);
                                        }
                                        var msg = '<div class="from-thong-bao-4">' +
                                            '<div class="content-thongbao">' +
                                            '<div class="rows-thongbao">' +
                                            resp.Data.Messages +
                                            '</div>' +
                                            '</div>' +
                                            '</div>';
                                        $().lawsDialog({
                                            dialogClass: 'lawsVnDialogTitle',
                                            width: 520,
                                            messages: [msg],
                                            onCreate: function () {
                                                var elem = $('.content-thongbao');
                                                elem.removeAttr('width');
                                                elem.css('width', '93.7%');
                                            },
                                            buttons: buttons
                                        });
                                    }
                                }
                            }
                        }
                    }
                });
            });

        $('.dang-ky-dv-mienphi').off('click').on('click',
            function (event) {
                event.preventDefault();
                var serviceId = $(this).data('id');
                lawsVn.account.registerFreeService(serviceId);
            });

        $('.getcaptchaScratchCard').off('click').on('click',
            function(event) {
                event.preventDefault();
                var prefix = $(this).data('prefix');
                $('#PaymentServiceUsingScratchCardCaptcha').lawsExists(function() {
                    lawsVn.getCaptcha('PaymentServiceUsingScratchCardCaptcha', prefix);
                });

            });

        $(document).on('change',
            'form .rbServices',
            function(event) {
                event.preventDefault();
                $(this).closest('form').submit();
            });

        $(document).on('click',
            '.service-reset',
            function (event) {
                event.preventDefault();
                //$('.rbServices').trigger('change');
                event.preventDefault();
                    $('.radio-label').removeClass('colora67942').addClass('color999999');
                    var serviceId = $('input[type="hidden"][name="ServiceIdFixed"]').val();
                    $('.rbServices[type="radio"]').prop('checked', false);
                    var rbService = $('.rbServices[type="radio"][value=' + serviceId + ']');
                    rbService.closest('.radio').find('label').removeClass('color999999').addClass('colora67942');
                    rbService.prop('checked', true);
                    $.lawsAjax({
                        url: lawsVn.virtualPath('/ajax/ServicePackagesByServiceId'),
                        type: 'Post',
                        data: { serviceId: serviceId },
                        dataType: 'json',
                        success: function (resp) {
                            if (resp.Completed) {
                                if (resp.Data != null) {
                                    var ddlParent = $('select[name="ServicePackageParentId"]');
                                    ddlParent.html('');
                                    var ddl = $('select[name="ServicePackageId"]');
                                    ddl.html('');
                                    lawsVn.registerServiceFormReset();
                                    if (resp.Data.ListServicePackagesParent != null && resp.Data.ListServicePackagesParent.length > 0) {
                                        $.each(resp.Data.ListServicePackagesParent, function (id, option) {
                                            ddlParent.append($('<option></option>').val(option.ServicePackageId).html(option.ServicePackageDesc));
                                        });
                                    } else ddlParent.html($('<option></option>').val(0).html('Chọn gói dịch vụ'));
                                    if (resp.Data.ListServicePackages != null && resp.Data.ListServicePackages.length > 0) {
                                        $.each(resp.Data.ListServicePackages, function (id, option) {
                                            ddl.append($('<option></option>').val(option.ServicePackageId).html(option.ServicePackageDesc));
                                        });
                                    } else ddl.html($('<option></option>').val(0).html('Chọn thời hạn thuê bao'));

                                    $('input[type="hidden"][name="ServiceId"]').lawsExists(function () {
                                        if (resp.Data.Services != null)
                                            $(this).val(resp.Data.Services.ServiceId);
                                        else
                                            $(this).val('');
                                    });
                                    $('#serviceDesc-span').lawsExists(function () {
                                        if (resp.Data.Services != null)
                                            $(this).text(resp.Data.Services.ServiceDesc);
                                        else
                                            $(this).val('');
                                    });
                                    $('input[type="hidden"][name="ServiceName"]').lawsExists(function () {
                                        if (resp.Data.Services != null)
                                            $(this).val(resp.Data.Services.ServiceDesc);
                                        else
                                            $(this).val('');
                                    });
                                }
                            } else if (resp.Message != null && resp.Message.length > 0) {
                                $().lawsDialog({
                                    dialogClass: 'lawsVnDialogTitle',
                                    messages: [resp.Message]
                                });
                            }
                        }
                    });

            });

        $(document).on('change',
            '.select-servicePackageParentId-onchange',
            function (event) {
                event.preventDefault();
                var serviceId = $('input[type="radio"][name="ServiceId"]:checked').val();
                var servicePackageParentId = $(this).val();
                $.lawsAjax({
                    url: lawsVn.virtualPath('/ajax/ServicePackagesByParentId'),
                    type: 'Post',
                    data: { serviceId: serviceId, servicePackageParentId: servicePackageParentId },
                    dataType: 'json',
                    success: function (resp) {
                        if (resp.Completed) {
                            if (resp.Data != null) {
                                var ddl = $('select[name="ServicePackageId"]');
                                ddl.html('');
                                lawsVn.registerServiceFormReset();
                                if (resp.Data.ListServicePackages != null && resp.Data.ListServicePackages.length > 0) {
                                    $.each(resp.Data.ListServicePackages, function (id, option) {
                                        ddl.append($('<option></option>').val(option.ServicePackageId).html(option.ServicePackageDesc));
                                    });
                                } else ddl.html($('<option></option>').val(0).html('Chọn thời hạn thuê bao'));
                                
                                $('input[type="hidden"][name="ServicePackageParentId"]').lawsExists(function () {
                                    $(this).val(servicePackageParentId);
                                });
                            }
                        } else if (resp.Message != null && resp.Message.length > 0) {
                            $().lawsDialog({
                                dialogClass: 'lawsVnDialogTitle',
                                messages: [resp.Message]
                            });
                        }
                    }
                });
            });

        $(document).on('change',
            '.select-servicePackageId-onchange',
            function (event) {
                event.preventDefault();
                var serviceId = $('input[type="radio"][name="ServiceId"]:checked').val();
                var servicePackageParentId = $('select[name="ServicePackageParentId"] option:selected').val();
                var servicePackageId = $(this).val();
                $.lawsAjax({
                    url: lawsVn.virtualPath('/ajax/ServicePackagesById'),
                    type: 'Post',
                    data: { serviceId: serviceId, servicePackageParentId: servicePackageParentId, servicePackageId: servicePackageId },
                    dataType: 'json',
                    success: function (resp) {
                        if (resp.Completed) {
                            if (resp.Data != null) {
                                lawsVn.registerServiceFormReset();
                                $('#price-selected-span').lawsExists(function () {
                                    $(this).html(lawsVn.formatNumber(resp.Data.Price, '.', '.') + ' VNĐ ' + ' - gói dịch vụ:  ' + resp.Data.ServicePackageDesc);
                                });
                                $('#termOfSubscription-span').lawsExists(function () {
                                    $(this).text(resp.Data.ServicePackageDesc);
                                });
                                $('#numberOfUsers-span').lawsExists(function () {
                                    $(this).text(resp.Data.NumberOfUsers);
                                });
                                $('#expiryDate-span').lawsExists(function () {
                                    $(this).text(resp.Data.ExpiryDateText);
                                });

                                $('input[type="hidden"][name="ServicePackageId"]').lawsExists(function () {
                                    $(this).val(resp.Data.ServicePackageId);
                                });
                                $('input[type="hidden"][name="ServicePackageName"]').lawsExists(function () {
                                    $(this).val(resp.Data.ServicePackageDesc);
                                });

                                $('input[type="hidden"][name="Total"]').lawsExists(function () {
                                    $(this).val(resp.Data.Total);
                                });
                                $('input[type="hidden"][name="Price"]').lawsExists(function () {
                                    $(this).val(resp.Data.Price);
                                });

                                $('.price-td').lawsExists(function () {
                                    $(this).text(resp.Data.PriceText);
                                });
                                $('.priceVat-td').lawsExists(function () {
                                    $(this).text(resp.Data.PriceVatText);
                                });
                                $('.total-td').lawsExists(function () {
                                    $(this).text(resp.Data.TotalText);
                                });
                                $('.total-promotion').lawsExists(function () {// khuyen mai 10%
                                    $(this).text(lawsVn.formatNumber((resp.Data.Total) * 0.9, '.', '.'));//bo vast va tru khuyen mai
                                });
                            }
                        } else if (resp.Message != null && resp.Message.length > 0) {
                            $().lawsDialog({
                                dialogClass: 'lawsVnDialogTitle',
                                messages: [resp.Message]
                            });
                        }
                    }
                });
            });
        
        $(document).on('change',
            '.rbServices',
            function (event) {
                event.preventDefault();
                if ($(this).is(':checked')) {
                    $('.radio-label').removeClass('colora67942').addClass('color999999');
                    $(this).closest('.radio').find('label').removeClass('color999999').addClass('colora67942');
                    var serviceId = $(this).val();
                    $.lawsAjax({
                        url: lawsVn.virtualPath('/ajax/ServicePackagesByServiceId'),
                        type: 'Post',
                        data: { serviceId: serviceId },
                        dataType: 'json',
                        success: function(resp) {
                            if (resp.Completed) {
                                if (resp.Data != null) {
                                    var ddlParent = $('select[name="ServicePackageParentId"]');
                                    ddlParent.html('');
                                    var ddl = $('select[name="ServicePackageId"]');
                                    ddl.html('');
                                    lawsVn.registerServiceFormReset();
                                    if (resp.Data.ListServicePackagesParent != null && resp.Data.ListServicePackagesParent.length > 0) {
                                        $.each(resp.Data.ListServicePackagesParent, function (id, option) {
                                            ddlParent.append($('<option></option>').val(option.ServicePackageId).html(option.ServicePackageDesc));
                                        });
                                    } else ddlParent.html($('<option></option>').val(0).html('Chọn gói dịch vụ'));
                                    if (resp.Data.ListServicePackages != null && resp.Data.ListServicePackages.length > 0) {
                                        $.each(resp.Data.ListServicePackages, function (id, option) {
                                            ddl.append($('<option></option>').val(option.ServicePackageId).html(option.ServicePackageDesc));
                                        });
                                    } else ddl.html($('<option></option>').val(0).html('Chọn thời hạn thuê bao'));
 
                                    $('input[type="hidden"][name="ServiceId"]').lawsExists(function () {
                                        if (resp.Data.Services != null)
                                            $(this).val(resp.Data.Services.ServiceId);
                                        else
                                            $(this).val('');
                                    });
                                    $('#serviceDesc-span').lawsExists(function () {
                                        if (resp.Data.Services != null)
                                            $(this).text(resp.Data.Services.ServiceDesc);
                                        else
                                            $(this).val('');
                                    });
                                    $('input[type="hidden"][name="ServiceName"]').lawsExists(function () {
                                        if (resp.Data.Services != null)
                                            $(this).val(resp.Data.Services.ServiceDesc);
                                            else 
                                            $(this).val('');
                                    });
                                }
                            }else if (resp.Message != null && resp.Message.length > 0) {
                                $().lawsDialog({
                                    dialogClass: 'lawsVnDialogTitle',
                                    messages: [resp.Message]
                                });
                            }
                        }
                    });
                }
            });

        $(document).on('click',
            '.btn-xacnhan',
            function(event) {
                event.preventDefault();
                $(this).closest('form').submit();
            });
        $(document).on('click',
            '.fieldIdTCVN',
            function (e) {
                e.preventDefault();
                var fieldId = $(this).data('id');
                $('.fieldIdTCVN').removeClass('active');
                $(this).addClass('active');
                lawsVn.CustomerInterFaceTCVNC(fieldId);
                $("#ddlFieldTCVN")
                    .removeAttr('selected')
                    .find(':first') //find('[value=0]')
                    .attr('selected', 'selected');
            });

        $(document).on('change',
            '#ddlFieldTCVN',
            function (event) {
                event.preventDefault();
                var fieldId = $(this).val();
                $('.fieldIdTCVN').removeClass('active');
                lawsVn.CustomerInterFaceTCVNC(fieldId);
            });
        $(document).on('click',
            '.btn-close',
            function() {
                $(this).closest('.ui-dialog-content').dialog('close');
                return false;
            });

        $('.lawTab').off('click').on('click',
            function(event) {
                event.preventDefault();
                var tab = $(this).data('id');
                var x = $('#' + tab);
                $('.lawTab').removeClass('active');
                $('.click-1').hide();
                $(this).addClass('active');
                x.show();
            });
        //tim kiem: đưa kết quả chứa từ khóa lên đầu
        $('.row-first-news').lawsExists(function() {
            //lawsVn.search.prioritizeKeyword();
        });

        $('.thugon').off('click').on('click',
            function(event) {
                event.preventDefault();
                $('table.table-tk-boder').find('tr.row-first-news-tk-boder').not('.title').slideToggle();
            }).toggle(function() {
                $(this).html(
                    'Hiện gợi ý tìm kiếm <img class="thugon-img" alt="thugon" src="/Assets/Images/thu-gon-tk.png">');
            },
            function() {
                $(this).html('Thu gọn <img class="thugon-img" alt="thugon" src="/Assets/Images/thu-gon-tk.png">');
            });
        //lawsVnConfig.arrAdvs = $(advsite_240);
        if (typeof advsite_240 != 'undefined') {
            var tmp = $(advsite_240).slice($(advsite_240));
            for (var i = 0; i < 8; i++) {
                var index = Math.floor(Math.random() * tmp.length);
                var removed = tmp.splice(index, 1);
                $('#partnerLink').append(removed[0]);
            }
        };
        $('.lawAdvs a').lawsExists(function() {
            var img = $(this).find('img');
            img.hover(lawsVn.swapImageUrl, lawsVn.swapImageUrl);
        });
        $('.uncheck-all').off('click').on('click',
            function() {
                lawsVnConfig.arrCoquanbanhanh = [];
                lawsVnConfig.arrLinhvuctracuu = [];
                lawsVnConfig.arrLoaivanban = [];
                lawsVnConfig.arrNambanhanh = [];
                lawsVnConfig.arrTrangthaihieuluc = [];
                $('input:checkbox[id^="checkboxFields"]').removeAttr('checked');
                $('input:checkbox[id^="checkboxEffectStatus"]').removeAttr('checked');
                $('input:checkbox[id^="checkboxDocTypes"]').removeAttr('checked');
                $('input:checkbox[id^="checkboxOrgans"]').removeAttr('checked');
                $('input:checkbox[id^="checkboxYearView"]').removeAttr('checked');
                $('#kq-loc-theo').html('<div id="linhvuctracuu" class="item-kqtk-noitem" style="border: 0;"></div>' +
                    '<div id="trangthaihieuluc" class="item-kqtk-noitem" ></div >' +
                    '<div id="loaivanban" class="item-kqtk-noitem"></div>' +
                    '<div id="coquanbanhanh" class="item-kqtk"></div>' +
                    '<div id="nambanhanh" class="item-kqtk-noitem"></div>');
                $('.resultsFilterBy').hide();
                lawsVn.search.start();
            });

        $('.tab-docgroupid').off('click').on('click',
            function(event) {
                event.preventDefault();
                $('.tab-docgroupid').removeClass('active');
                $(this).addClass('active');
                var id = $(this).data('id');
                var docGroupId = $('input[type="hidden"][id="DocGroupId"]');
                $('select[name="LanguageId"] option')
                    .removeAttr('selected')
                    .filter('[value=0]')
                    .attr('selected', true);
                switch (id) {
                case 1:
                    docGroupId.val(1); //VBPL
                    break;
                case 2:
                    docGroupId.val(6); //Công văn
                    break;
                case 3:
                    docGroupId.val(1); //Vb tiếng anh
                    $('select[name="LanguageId"] option')
                        .removeAttr('selected')
                        .filter('[value=2]')
                        .attr('selected', true);
                    break;
                case 4:
                    docGroupId.val(2); //UBND
                    break;
                default:
                    docGroupId.val(3); //TCVN
                    break;
                }
            });

        $(document).on('click',
            '.delete-message',
            function (event) {
                event.preventDefault();
                var messageId = $(this).data('id');
                lawsVn.myMesssages.deleteMessage(messageId);
            });
        $(document).on('click',
            '.delete-messages',
            function (event) {
                event.preventDefault();
                var actionTypeId = $(this).data('actiontypeid');
                lawsVn.myMesssages.deleteMessages(actionTypeId);
            });
        $(document).on('click',
            '.save-messages',
            function (event) {
                event.preventDefault();
                var actionTypeId = $(this).data('actiontypeid');
                lawsVn.myMesssages.saveMessages(actionTypeId);
            });
        $(document).on('click',
            '.save-message',
            function (event) {
                event.preventDefault();
                var messageId = $(this).data('id');
                lawsVn.myMesssages.saveMessage(messageId);
            });
        $(document).on('click',
            '.unsave-messages',
            function (event) {
                event.preventDefault();
                var actionTypeId = $(this).data('actiontypeid');
                lawsVn.myMesssages.unsaveMessages(actionTypeId);
            });

        $(document).on('click',
            '.unsave-message',
            function (event) {
                event.preventDefault();
                var messageId = $(this).data('id');
                lawsVn.myMesssages.unsaveMessage(messageId);
            });


        $(document).on('click',
            '.updateCustomerFields',
            function(event) {
                event.preventDefault();
                lawsVn.editCustomerFields();
            });

        $(document).on('click',
            '.customername-check',
            function(event) {
                event.preventDefault();
                $('#CustomerName').lawsExists(function() {
                    if ($(this).val().length === 0) {
                        $().lawsDialog({
                            dialogClass: 'lawsVnDialogTitle',
                            messages: ['Quý khách vui lòng nhập tên truy cập cần kiểm tra.']
                        });
                        return false;
                    }
                    $.lawsAjax({
                        url: lawsVnConfig.rootPath + 'Ajax/CustomerNameCheck',
                        type: 'Post',
                        data: { customerName: $(this).val() },
                        success: function(resp) {
                            if (resp.Message !== void 0 && resp.Message.length > 0) {
                                $().lawsDialog({
                                    dialogClass: 'lawsVnDialogTitle',
                                    messages: [resp.Message]
                                });
                            }
                        }
                    });
                });
                return false;
            });

        $(document).on('change',
            'input[id^="TaxInvoice"]',
            function(event) {
                event.preventDefault();
                if (this.checked) {
                    $('<div id="TaxInvoiceFormLoad"></div>').lawsDialog({
                        title: '',
                        width: 640,
                        hideClose: false,
                        buttons: {},
                        onCreate: function() {
                            $('#loading').fadeIn('normal');
                        },
                        onOpen: function() {
                            $('#TaxInvoiceFormLoad').load(lawsVnConfig.rootPath + 'Ajax/PartialTaxInvoice',
                                function() {
                                    var form = $('#TaxInvoiceForm');
                                    form.removeData('validator');
                                    form.removeData('unobtrusiveValidation');
                                    $.validator.unobtrusive.parse(form);
                                });
                            $('#loading').fadeOut('normal');
                        }
                    });
                } else {
                    $('input[name="CompanyName"]').lawsExists(function() {
                        $(this).val('');
                    });
                    $('input[name="CompanyAddress"]').lawsExists(function() {
                        $(this).val('');
                    });
                    $('input[name="CompanyTaxCode"]').lawsExists(function() {
                        $(this).val('');
                    });
                    $('input[name="InternalContent"]').lawsExists(function() {
                        $(this).val('');
                    });
                }
            });

        $(document).on('click',
            'input[type="radio"]',
            function() {
                var fieldId = $(this).data('id');
                $('#rbFieldId' + fieldId).lawsExists(function() {
                    $(this).trigger('click');
                });
            });
        $(document).on('click',
            '.dang-ky-dich-vu-mien-phi',
            function(event) {
                event.preventDefault();
                var serviceid = $(this).data('serviceid');
                lawsVn.account.registerFreeService(serviceid);
            });
        $(document).on('click',
            '.dang-ky-dich-vu-dung-thu',
            function(event) {
                event.preventDefault();
                var serviceid = $(this).data('serviceid');
                lawsVn.account.registerTrialService(serviceid);
            });
        $(document).on('click',
            '.btn-reset',
            function(event) {
                event.preventDefault();
                lawsVn.ResetForm($(this).closest('form'));
            });

        $('.UncheckSearchAll').off('click').on('click',
            function(e) {
                e.preventDefault();
                var checkboxid = "";
                $('input:checkbox[id^="checkboxFields"]:checked').each(function() {
                    checkboxid = $(this).attr("id");
                    $('#' + checkboxid).prop('checked', false);
                });
                $('#linhvuctracuu').html('').removeClass('item-kqtk').addClass('item-kqtk-noitem');
                lawsVnConfig.arrLinhvuctracuu = [];

                $('input:checkbox[id^="checkboxEffectStatus"]:checked').each(function() {
                    checkboxid = $(this).attr("id");
                    $('#' + checkboxid).prop('checked', false);
                });
                $('#trangthaihieuluc').html('').removeClass('item-kqtk').addClass('item-kqtk-noitem');
                lawsVnConfig.arrTrangthaihieuluc = [];

                $('input:checkbox[id^="checkboxDocTypes"]:checked').each(function() {
                    checkboxid = $(this).attr("id");
                    $('#' + checkboxid).prop('checked', false);
                });
                $('#loaivanban').html('').removeClass('item-kqtk').addClass('item-kqtk-noitem');
                lawsVnConfig.arrLoaivanban = [];

                $('input:checkbox[id^="checkboxOrgans"]:checked').each(function() {
                    checkboxid = $(this).attr("id");
                    $('#' + checkboxid).prop('checked', false);
                });
                $('#coquanbanhanh').html('').removeClass('item-kqtk').addClass('item-kqtk-noitem');
                lawsVnConfig.arrCoquanbanhanh = [];

                $('input:checkbox[id^="checkboxYearView"]:checked').each(function() {
                    checkboxid = $(this).attr("id");
                    $('#' + checkboxid).prop('checked', false);
                });
                $('#nambanhanh').html('').removeClass('item-kqtk').addClass('item-kqtk-noitem');
                lawsVnConfig.arrNambanhanh = [];
                lawsVn.search.start();
            });

        $('input[type="checkbox"][class="search2"]').on('change',
            function() {
                var id = $(this).data('id');
                var item = $('input[type="checkbox"][data-id="' + id + '"]:checked');
                $('#' + id).html('');
                $.each(item,
                    function() {
                        var title = $(this).data('title');
                        var value = $(this).val();
                        var retval = $(this).data('value');
                        $('#' + id).append('<div class="item-sub-qktk">' +
                            '<span class="texttk" > ' +
                            title +
                            '</span >' +
                            '<a href="#" data-id="' +
                            value +
                            '" title="Xóa điều kiện lọc" class="xoa ' +
                            retval +
                            '"><img alt="xóa" src="/assets/Images/xoa-tiemkiem.png"></a>' +
                            '</div>');
                        $('#' + id).removeClass('item-kqtk-noitem').addClass('item-kqtk');
                    });
                lawsVn.search.start();
            });
        //////
        $('input[id^="checkboxFields"]').on('change',
            function() {
                var parrent = $('#linhvuctracuu');
                var html = '';
                if ($(this).attr('checked'))
                    lawsVnConfig.arrLinhvuctracuu.push({ title: $(this).data('title'), id: $(this).val() });
                else
                    lawsVn.removeVal(lawsVnConfig.arrLinhvuctracuu, $(this).val());
                if (lawsVnConfig.arrLinhvuctracuu.length > 0) {
                    parrent.removeClass('item-kqtk-noitem').addClass('item-kqtk');
                    for (var i = 0; i < lawsVnConfig.arrLinhvuctracuu.length; i++) {
                        html += '<div class="item-sub-qktk">' +
                            '<span class="texttk" > ' +
                            lawsVnConfig.arrLinhvuctracuu[i].title +
                            '</span >' +
                            '<a href="#" data-id="' +
                            lawsVnConfig.arrLinhvuctracuu[i].id +
                            '" title="Xóa điều kiện lọc theo lĩnh vực tra cứu" class="xoa xoa-dk-loc-linhvuctracuu"><img alt="xóa" src="/assets/Images/xoa-tiemkiem.png"></a>' +
                            '</div>';
                    }
                } else parrent.removeClass('item-kqtk').addClass('item-kqtk-noitem');
                parrent.html(html);//.promise().done(function() {
                    $('.resultsFilterBy').show();
                //});
                    $('select[id="ddlFieldId"] option').removeAttr('selected').filter('[value=0]').attr('selected', true);
                    lawsVn.search.start();
            });

        $('input[id^="checkboxEffectStatus"]').on('change',
            function() {
                var parrent = $('#trangthaihieuluc');
                var html = '';
                if ($(this).attr('checked'))
                    lawsVnConfig.arrTrangthaihieuluc.push({ title: $(this).data('title'), id: $(this).val() });
                else
                    lawsVn.removeVal(lawsVnConfig.arrTrangthaihieuluc, $(this).val());
                if (lawsVnConfig.arrTrangthaihieuluc.length > 0) {
                    parrent.removeClass('item-kqtk-noitem').addClass('item-kqtk');
                    for (var i = 0; i < lawsVnConfig.arrTrangthaihieuluc.length; i++) {
                        html += '<div class="item-sub-qktk">' +
                            '<span class="texttk" > ' +
                            lawsVnConfig.arrTrangthaihieuluc[i].title +
                            '</span >' +
                            '<a href="#" data-id="' +
                            lawsVnConfig.arrTrangthaihieuluc[i].id +
                            '" title="Xóa điều kiện lọc theo trạng thái hiệu lực" class="xoa xoa-dk-loc-trangthaihieuluc"><img alt="xóa" src="/assets/Images/xoa-tiemkiem.png"></a>' +
                            '</div>';
                    }
                } else parrent.removeClass('item-kqtk').addClass('item-kqtk-noitem');
                parrent.html(html);//.promise().done(function() {
                    $('.resultsFilterBy').show();
                    $('select[id="ddlEffectStatusId"] option').removeAttr('selected').filter('[value=0]').attr('selected', true);
                    lawsVn.search.start();
                //});
            });

        $('input[id^="checkboxDocTypes"]').on('change',
            function() {
                var parrent = $('#loaivanban');
                var html = '';
                if ($(this).attr('checked'))
                    lawsVnConfig.arrLoaivanban.push({ title: $(this).data('title'), id: $(this).val() });
                else
                    lawsVn.removeVal(lawsVnConfig.arrLoaivanban, $(this).val());
                if (lawsVnConfig.arrLoaivanban.length > 0) {
                    parrent.removeClass('item-kqtk-noitem').addClass('item-kqtk');
                    for (var i = 0; i < lawsVnConfig.arrLoaivanban.length; i++) {
                        html += '<div class="item-sub-qktk">' +
                            '<span class="texttk" > ' +
                            lawsVnConfig.arrLoaivanban[i].title +
                            '</span >' +
                            '<a href="#" data-id="' +
                            lawsVnConfig.arrLoaivanban[i].id +
                            '" title="Xóa điều kiện lọc theo loại văn bản" class="xoa xoa-dk-loc-loaivb"><img alt="xóa" src="/assets/Images/xoa-tiemkiem.png"></a>' +
                            '</div>';
                    }
                } else parrent.removeClass('item-kqtk').addClass('item-kqtk-noitem');
                parrent.html(html);//.promise().done(function() {
                    $('.resultsFilterBy').show();
                    $('select[id="ddlDocTypeId"] option').removeAttr('selected').filter('[value=0]').attr('selected', true);
                    lawsVn.search.start();
                //});
            });

        $('input[id^="checkboxOrgans"]').on('change',
            function() {
                var parrent = $('#coquanbanhanh');
                var html = '';
                if ($(this).attr('checked'))
                    lawsVnConfig.arrCoquanbanhanh.push({ title: $(this).data('title'), id: $(this).val() });
                else
                    lawsVn.removeVal(lawsVnConfig.arrCoquanbanhanh, $(this).val());
                if (lawsVnConfig.arrCoquanbanhanh.length > 0) {
                    parrent.removeClass('item-kqtk-noitem').addClass('item-kqtk');
                    for (var i = 0; i < lawsVnConfig.arrCoquanbanhanh.length; i++) {
                        html += '<div class="item-sub-qktk">' +
                            '<span class="texttk" > ' +
                            lawsVnConfig.arrCoquanbanhanh[i].title +
                            '</span >' +
                            '<a href="#" data-id="' +
                            lawsVnConfig.arrCoquanbanhanh[i].id +
                            '" title="Xóa điều kiện lọc theo cơ quan ban hành" class="xoa xoa-dk-loc-cqbh"><img alt="xóa" src="/assets/Images/xoa-tiemkiem.png"></a>' +
                            '</div>';
                    }
                } else parrent.removeClass('item-kqtk').addClass('item-kqtk-noitem');
                parrent.html(html);//.promise().done(function() {
                    $('.resultsFilterBy').show();
                    lawsVn.search.start();
                //});
            });

        $('input[id^="checkboxYearView"]').on('change',
            function() {
                var parrent = $('#nambanhanh');
                var html = '';
                if ($(this).attr('checked'))
                    lawsVnConfig.arrNambanhanh.push({ title: $(this).data('title'), id: $(this).val() });
                else
                    lawsVn.removeVal(lawsVnConfig.arrNambanhanh, $(this).val());
                if (lawsVnConfig.arrNambanhanh.length > 0) {
                    parrent.removeClass('item-kqtk-noitem').addClass('item-kqtk');
                    for (var i = 0; i < lawsVnConfig.arrNambanhanh.length; i++) {
                        html += '<div class="item-sub-qktk">' +
                            '<span class="texttk" > ' +
                            lawsVnConfig.arrNambanhanh[i].title +
                            '</span >' +
                            '<a href="#" data-id="' +
                            lawsVnConfig.arrNambanhanh[i].id +
                            '" title="Xóa điều kiện lọc theo năm ban hành" class="xoa xoa-dk-loc-nambanhanh"><img alt="xóa" src="/assets/Images/xoa-tiemkiem.png"></a>' +
                            '</div>';
                    }
                } else parrent.removeClass('item-kqtk').addClass('item-kqtk-noitem');
                parrent.html(html);//.promise().done(function() {
                    $('.resultsFilterBy').show();
                    lawsVn.search.start();
                //});
            });

        $(document).on('click',
            '.xoa-dk-loc-linhvuctracuu',
            function(event) {
                event.preventDefault();
                var id = $(this).data('id');
                var parrent = $('#linhvuctracuu');
                var html = '';
                lawsVn.removeVal(lawsVnConfig.arrLinhvuctracuu, id);
                $('input[id^="checkboxFields"][type="checkbox"][value="' + id + '"]').prop('checked', false);
                if (lawsVnConfig.arrLinhvuctracuu.length > 0) {
                    parrent.removeClass('item-kqtk-noitem').addClass('item-kqtk');
                    for (var i = 0; i < lawsVnConfig.arrLinhvuctracuu.length; i++) {
                        html += '<div class="item-sub-qktk">' +
                            '<span class="texttk" > ' +
                            lawsVnConfig.arrLinhvuctracuu[i].title +
                            '</span >' +
                            '<a href="#" data-id="' +
                            lawsVnConfig.arrLinhvuctracuu[i].id +
                            '" title="Xóa điều kiện lọc" class="xoa xoa-dk-loc-linhvuctracuu"><img alt="xóa" src="/assets/Images/xoa-tiemkiem.png"></a>' +
                            '</div>';
                    }
                } else parrent.removeClass('item-kqtk').addClass('item-kqtk-noitem');
                parrent.html(html);//.promise().done(function() {
                    $('.resultsFilterBy').show();
                    lawsVn.search.start();
                //});
            });

        $(document).on('click',
            '.xoa-dk-loc-trangthaihieuluc',
            function(event) {
                event.preventDefault();
                var id = $(this).data('id');
                var parrent = $('#trangthaihieuluc');
                var html = '';
                lawsVn.removeVal(lawsVnConfig.arrTrangthaihieuluc, id);
                $('input[id^="checkboxEffectStatus"][type="checkbox"][value="' + id + '"]').prop('checked', false);
                if (lawsVnConfig.arrTrangthaihieuluc.length > 0) {
                    parrent.removeClass('item-kqtk-noitem').addClass('item-kqtk');
                    for (var i = 0; i < lawsVnConfig.arrTrangthaihieuluc.length; i++) {
                        html += '<div class="item-sub-qktk">' +
                            '<span class="texttk" > ' +
                            lawsVnConfig.arrTrangthaihieuluc[i].title +
                            '</span >' +
                            '<a href="#" data-id="' +
                            lawsVnConfig.arrTrangthaihieuluc[i].id +
                            '" title="Xóa điều kiện lọc" class="xoa xoa-dk-loc-trangthaihieuluc"><img alt="xóa" src="/assets/Images/xoa-tiemkiem.png"></a>' +
                            '</div>';
                    }
                } else parrent.removeClass('item-kqtk').addClass('item-kqtk-noitem');
                parrent.html(html);//.promise().done(function() {
                    lawsVn.search.start();
                //});
            });

        $(document).on('click',
            '.xoa-dk-loc-loaivb',
            function(event) {
                event.preventDefault();
                var id = $(this).data('id');
                var parrent = $('#loaivanban');
                var html = '';
                lawsVn.removeVal(lawsVnConfig.arrLoaivanban, id);
                $('input[id^="checkboxDocTypes"][type="checkbox"][value="' + id + '"]').prop('checked', false);
                if (lawsVnConfig.arrLoaivanban.length > 0) {
                    parrent.removeClass('item-kqtk-noitem').addClass('item-kqtk');
                    for (var i = 0; i < lawsVnConfig.arrLoaivanban.length; i++) {
                        html += '<div class="item-sub-qktk">' +
                            '<span class="texttk" > ' +
                            lawsVnConfig.arrLoaivanban[i].title +
                            '</span >' +
                            '<a href="#" data-id="' +
                            lawsVnConfig.arrLoaivanban[i].id +
                            '" title="Xóa điều kiện lọc" class="xoa xoa-dk-loc-loaivb"><img alt="xóa" src="/assets/Images/xoa-tiemkiem.png"></a>' +
                            '</div>';
                    }
                } else parrent.removeClass('item-kqtk').addClass('item-kqtk-noitem');
                parrent.html(html);//.promise().done(function() {
                    lawsVn.search.start();
                //});
            });

        $(document).on('click',
            '.xoa-dk-loc-cqbh',
            function(event) {
                event.preventDefault();
                var id = $(this).data('id');
                var parrent = $('#coquanbanhanh');
                var html = '';
                lawsVn.removeVal(lawsVnConfig.arrCoquanbanhanh, id);
                $('input[id^="checkboxOrgans"][type="checkbox"][value="' + id + '"]').prop('checked', false);
                if (lawsVnConfig.arrCoquanbanhanh.length > 0) {
                    parrent.removeClass('item-kqtk-noitem').addClass('item-kqtk');
                    for (var i = 0; i < lawsVnConfig.arrCoquanbanhanh.length; i++) {
                        html += '<div class="item-sub-qktk">' +
                            '<span class="texttk" > ' +
                            lawsVnConfig.arrCoquanbanhanh[i].title +
                            '</span >' +
                            '<a href="#" data-id="' +
                            lawsVnConfig.arrCoquanbanhanh[i].id +
                            '" title="Xóa điều kiện lọc" class="xoa xoa-dk-loc-cqbh"><img alt="xóa" src="/assets/Images/xoa-tiemkiem.png"></a>' +
                            '</div>';
                    }
                } else parrent.removeClass('item-kqtk').addClass('item-kqtk-noitem');
                parrent.html(html);//.promise().done(function() {
                    lawsVn.search.start();
                //});
            });

        $(document).on('click',
            '.xoa-dk-loc-nambanhanh',
            function(event) {
                event.preventDefault();
                var id = $(this).data('id');
                var parrent = $('#nambanhanh');
                var html = '';
                lawsVn.removeVal(lawsVnConfig.arrNambanhanh, id);
                $('input[id^="checkboxYearView"][type="checkbox"][value="' + id + '"]').prop('checked', false);
                if (lawsVnConfig.arrNambanhanh.length > 0) {
                    parrent.removeClass('item-kqtk-noitem').addClass('item-kqtk');
                    for (var i = 0; i < lawsVnConfig.arrNambanhanh.length; i++) {
                        html += '<div class="item-sub-qktk">' +
                            '<span class="texttk" > ' +
                            lawsVnConfig.arrNambanhanh[i].title +
                            '</span >' +
                            '<a href="#" data-id="' +
                            lawsVnConfig.arrNambanhanh[i].id +
                            '" title="Xóa điều kiện lọc" class="xoa xoa-dk-loc-nambanhanh"><img alt="xóa" src="/assets/Images/xoa-tiemkiem.png"></a>' +
                            '</div>';
                    }
                } else parrent.removeClass('item-kqtk').addClass('item-kqtk-noitem');
                parrent.html(html);//.promise().done(function() {
                    lawsVn.search.start();
                //});
            });

        $('#ToggleDocIndexes').off('click').on('click',
            function(event) {
                event.preventDefault();
                $('#DocIndexes').slideToggle();
            }).toggle(function() {
                $(this).html(
                        '<img class="icon-hidden-article" alt="icon-hidden-article" src="/assets/images/an-dmuc.png">Hiện mục lục')
                    .attr('title', 'Hiện mục lục');
            },
            function() {
                $(this).html(
                        '<img class="icon-hidden-article" alt="icon-hidden-article" src="/assets/images/an-dmuc.png">Ẩn mục lục')
                    .attr('title', 'Ẩn mục lục');
            });;

        $('#advancedSearch').off('click').on('click',
            function(event) {
                event.preventDefault();
                $("#advancedSearchPannel").toggle();
            });

        $('.doc-thong-bao').off('click').on('click',
            function(event) {
                event.preventDefault();
                var self = $(this);
                var messageId = $(this).data('id');
                var targetUrl = $(this).data('url');
                $.lawsAjax({
                    url: lawsVnConfig.rootPath + 'Ajax/ReadNotifyMessages',
                    type: 'Post',
                    data: { messageId: messageId, targetUrl: targetUrl },
                    success: function(resp) {
                        $(self).parent().remove();
                        if (resp.ReturnUrl !== void 0 && resp.ReturnUrl.length > 0) {
                            window.location.href = resp.ReturnUrl;
                        }
                    }
                });
            });

        $('.danh-dau-tat-ca-da-doc-thong-bao').off('click').on('click',
            function(event) {
                event.preventDefault();
                $.lawsAjax({
                    url: lawsVnConfig.rootPath + 'Ajax/ReadAllNotifyMessages',
                    type: 'Post',
                    success: function(resp) {
                        if (resp.Message !== void 0 && resp.Message > 0) {
                            $().lawsDialog({
                                messages: [resp.Message],
                                showIcon: false,
                                onClose: function() {
                                    if (resp.ReturnUrl !== void 0 && resp.ReturnUrl > 0) {
                                        window.location.href = resp.ReturnUrl;
                                    }
                                }
                            });
                        }
                    }
                });
            });

        $("#checkbox_All").change(function() {
            $(".mail-content input:checkbox").prop('checked', $(this).prop("checked"));
        });
        $(document).on('click',
            '.closeDialog',
            function(event) {
                event.preventDefault();
                $(this).parents('.ui-dialog-content').dialog('close'); //or closest
            });
        $(document).on('click',
            'form .submit-link',
            function(event) {
                event.preventDefault();
                $(this).closest('form').submit();
            });

        $(document).on('click',
            '.save-doc-of-interest',
            function(event) {
                event.preventDefault();
                var docId = $(this).data('id');
                $.lawsAjax({
                    url: lawsVnConfig.rootPath + 'Ajax/SaveDocument',
                    data: { docId: docId },
                    beforeSend: {},
                    success: function(resp) {
                        if (resp.Message !== void 0 && resp.Message.length > 0) {
                            $().lawsDialog({
                                dialogClass: 'lawsVnDialogTitle',
                                messages: [resp.Message],
                                showIcon: false
                            });
                        }
                    }
                });
            });

        $(document).on('change',
            'form .select-onchange',
            function(event) {
                event.preventDefault();
                $('input[name="page"]').lawsExists(function() {
                    $(this).val(1);
                    lawsVn.ajaxEvents.pageIndex = 1; //reset page
                });
                lawsVn.ajaxEvents.showNumberOfResults = $(this).val();
                $(this).closest('form').submit();
            });

        $(document).on('change',
            'form .select-service-packages-onchange',
            function (event) {
                event.preventDefault();
                $('input[name="page"]').lawsExists(function () {
                    $(this).val(1);
                });
                $('input[name="typeChange"]').lawsExists(function () {
                    $(this).val(1);
                });
                $(this).closest('form').submit();
            });

        $(document).on('change',
            '.newslettergroup-onchange',
            function(event) {
                event.preventDefault();
                var id = $(this).val();
                if (id == 4 || id == 6) {
                    $('.newsLetterInfo').show();
                } else $('.newsLetterInfo').hide();
            });

        $(document).on('change',
            '.select-filterbyfield-onchange',
            function(event) {
                event.preventDefault();
                var fieldId = $(this).data('fieldid');
                var selectFieldId = $('#ddlFieldId option:selected').val();
                var effectStatusId = $('#ddlEffectStatusId option:selected').val();
                var effectStatusName = $('input[name="effectStatusName"]').val();
                var organsId = $('#ddlOrganId option:selected').val();
                var docTypesId = $('#ddlDocTypeId option:selected').val();
                if (selectFieldId > 0) {
                    fieldId = selectFieldId;
                }
                $.lawsAjax({
                    url: lawsVnConfig.rootPath + 'Ajax/Docs_GetViewSearch',
                    dataType: 'html',
                    data: {
                        fieldId: fieldId,
                        effectStatusId: effectStatusId,
                        effectStatusName: effectStatusName,
                        organId: organsId,
                        docTypeId: docTypesId
                    },
                    success: function(resp) {
                        $("#ListByField").html(resp);
                        var totalpage = $("#tblcontent").attr('data-totalpage');
                        var pageindex = $("#tblcontent").attr('data-pageindex');
                        var pageCount = $("#tblcontent").attr('data-pagecount');
                        if (totalpage === undefined) {
                            totalpage = 0;
                        }
                        if (pageindex === undefined) {
                            pageindex = 0;
                        }
                        if (pageindex <= 0) {
                            pageindex = 1;
                        }
                        if (totalpage > 0) {
                            var data;
                            if (keywords.length > 0) {
                                data = " <strong>Tìm thấy <span class='color2'>" +
                                    lawsVn.formatNumber(totalpage, '.', '.') +
                                    "</span> văn bản với từ khóa \"<span class='color2'>" +
                                    keywords +
                                    "</span>\"</strong>";
                                $('#txtnumberresultfound').html(data);
                            } else {
                                data = " <strong>Tìm thấy <span class='color2'>" +
                                    lawsVn.formatNumber(totalpage, '.', '.') +
                                    "</span> văn bản.</strong>";
                                $('#txtnumberresultfound').html(data);
                            }
                        }

                        //var data = '';
                        //if (pageCount > 0) {
                        //    data += "<span>Tìm thấy <strong> " +
                        //        totalpage +
                        //        " văn bản </strong>(" +
                        //        pageindex +
                        //        "/" +
                        //        pageCount +
                        //        " trang)";
                        //} else data = "<span>Không tìm thấy kết quả.</strong>";
                        //data += "</span>";
                        //$("#txtnumberresultfound").html(data);
                    }
                });
            });

        $(document).on('change',
            'form .select-province-onchange',
            function(event) {
                event.preventDefault();
                $('select[name="DistrictId"]').html($('<option></option>').val(0).html('Quận / huyện'));
                $('select[name="WardId"]').html($('<option></option>').val(0).html('Phường / xã'));
                $('input[name="page"]').lawsExists(function() {
                    $(this).val(1);
                });
                $('select[name="DistrictId"]').load(
                    lawsVnConfig.rootPath + 'Ajax/GetDistrictsByProvinceId?provinceId=' + $(this).val(),
                    function(resp, status, xhr) {
                        if (status == 'success') {
                            $(this).html(resp).promise().done(function() {
                                $(this).closest('form').submit();
                            });
                        }
                    });
            });

        $(document).on('change',
            'form .select-district-onchange',
            function(event) {
                event.preventDefault();
                $('input[name="page"]').lawsExists(function() {
                    $(this).val(1);
                });
                $('select[name="WardId"]').load(
                    lawsVnConfig.rootPath + 'Ajax/GetWardsByDistrictId?districtId=' + $(this).val(),
                    function(resp, status, xhr) {
                        if (status == 'success') {
                            $(this).html(resp).promise().done(function() {
                                $(this).closest('form').submit();
                            });
                        }
                    });
            });

        $(document).on('change', 'input[type=file]', lawsVn.fileUpload);
        $(document).on('click',
            '#uploadAvatar',
            function(e) {
                e.preventDefault();
                $('#avatarFile').click();
            });
        $(document).on('drop dragover',
            function(e) {
                e.preventDefault();
            });

        $('#SignerName').lawsExists(function() {
            $(this).autocomplete({
                minLength: 2,
                dataType: "json",
                async: false,
                cache: false,
                source: function(request, response) {
                    //var signers = new Array();
                    $('#SignerId').val(0);
                    $('#signer-message').text('');
                    var dataGetter = { signerName: request.term };
                    var url = lawsVnConfig.rootPath + 'Ajax/AutocompleteSignerByName';
                    $.ajaxSetup({ cache: false, async: false });
                    $.lawsVnAjax(url,
                        'Get',
                        dataGetter,
                        function(data) {
                            var json = JSON.parse(data.jsonRetval);
                            if (json.length == 0)
                                $('#signer-message')
                                    .html(
                                        '<span class="text-danger field-validation-error"><span>Không tìm thấy người ký phù hợp.</span></span>');
                            response($.map(json,
                                function(item) {
                                    return {
                                        label: item.SignerName,
                                        val: item.SignerId
                                    }
                                }));
                            //for (var i = 0; i < json.length; i++) {
                            //signers[i] = { label: json[i].SignerName, id: json[i].SignerId };
                            //}
                        });
                    //response(signers); 
                },
                search: function() {
                    $(this).addClass('ui-autocomplete-loading');
                },
                open: function() { $(this).removeClass('ui-autocomplete-loading'); },
                focus: function() {
                    // prevent value inserted on focus
                    return false;
                },
                select: function(event, ui) {
                    $('#SignerId').val(ui.item.val);
                }
            });
        });

        $("#txtTNPL").autocomplete({
            minLength: 1,
            dataType: "json",
            async: false,
            cache: false,
            source: function(request, response) {
                //var signers = new Array();
                $('#LawTerminId').val(0);
                $('#signer-message').text('');
                var dataGetter = { lawterminName: request.term };
                var url = lawsVnConfig.rootPath + 'Ajax/AutocompleteTNPLByName';
                $.ajaxSetup({ cache: false, async: false });
                $.lawsVnAjax(url,
                    'Get',
                    dataGetter,
                    function(data) {
                        var json = JSON.parse(data.jsonRetval);
                        if (json.length == 0)
                            $('#signer-message')
                                .html(
                                    '<span class="text-danger field-validation-error"><span>Không tìm thấy thuật ngữ pháp lý phù hợp.</span></span>');
                        response($.map(json,
                            function(item) {
                                return {
                                    label: item.TermName,
                                    val: item.LawTerminId
                                }
                            }));
                    });
            },
            search: function() {
                $(this).addClass('ui-autocomplete-loading');
            },
            open: function() { $(this).removeClass('ui-autocomplete-loading'); },
            focus: function() {
                return false;
            },
            select: function(event, ui) {
                $('#LawTerminId').val(ui.item.val);
            }
        });


        $('#cssmenu ul ul li:odd').addClass('odd');
        $('#cssmenu ul ul li:even').addClass('even');
        $('#cssmenu > ul > li > a').click(function() {
            var parent_cssmenu = $(this).parent().parent().parent().parent().parent();
            if (parent_cssmenu.length) {
                var cssmenu = parent_cssmenu.children().children();
                cssmenu.each(function() {
                    var item_cssmenu = $(this);
                    item_cssmenu.find('.has-sub').removeClass('active');
                    item_cssmenu.find('.has-sub').first().children('ul').slideUp('normal');
                });
            }

            //$('#cssmenu li').removeClass('active');
            $(this).closest('li').addClass('active');
            //$('a[class^="rows-huong-dan-item"]').removeClass('active');
            //$('a[href="#'+$(this).attr('id')+'"]').addClass('active');
            //var checkElement = $(this).next();
            var checkElement = $(this).closest('li').find('ul').first();
            if (checkElement.length) {
                if ((checkElement.is('ul')) && (checkElement.is(':visible'))) {
                    $(this).closest('li').removeClass('active');
                    checkElement.slideUp('normal');
                }
                if ((checkElement.is('ul')) && (!checkElement.is(':visible'))) {
                    $('#cssmenu ul ul:visible').slideUp('normal');
                    checkElement.slideDown('normal');
                }
            }
            if ($(this).closest('li').find('ul').children().length == 0) {
                return true;
            } else {
                return false;
            }
        });

        $(document).on('change',
            '#txtChangePage',
            function() {
                var newValue = $(this).val();
                $(this).val(newValue);
                //lawsVn.delay();
                $(this.form).submit();
            });

        $(document).on('click',
            '#prevPage',
            function(event) {
                event.preventDefault();
                var newValue = $('#txtChangePage').val();
                $('#txtChangePage').val(parseInt(newValue) - 1);
                var $form = $(this).closest('form');
                $form.submit();
            });

        $(document).on('click',
            '#nextPage',
            function(event) {
                event.preventDefault();
                var newValue = $('#txtChangePage').val();
                $('#txtChangePage').val(parseInt(newValue) + 1);
                var $form = $(this).closest('form');
                $form.submit();
            });


    },
    fileUpload: function(event) {
        var fileApiSupported = !!(window.File && window.FileReader && window.FileList && window.Blob);
        if (fileApiSupported) {
            var files = event.target.files;
            var file = files[0];
            if (file) {
                if (/^image\//i.test(file.type)) {
                    if (file.size > 10485760) {
                        $().lawsDialog({
                            dialogClass: 'lawsVnDialogTitle',
                            messages: ['Dung lượng ảnh vượt quá 10MB cho phép.'],
                            showIcon: false
                        });
                        return;
                    } else {
                        lawsVn.readFile(file);
                    }
                } else {
                    $().lawsDialog({
                        dialogClass: 'lawsVnDialogTitle',
                        messages: ['Quý khách vui lòng chọn file ảnh.'],
                        showIcon: false
                    });
                    return;
                }
            }
        } else {
            $().lawsDialog({
                dialogClass: 'lawsVnDialogTitle',
                messages: [
                    'Trình duyệt hiện tại đã cũ. Quý khách vui lòng nâng cấp trình duyệt để sử dụng chức năng này.'
                ],
                showIcon: false
            });
            return;
        }
    },
    readFile:function(file) {
        var reader = new FileReader();
        reader.onloadend = function () {
            lawsVn.processFile(reader.result, file.type, file.name);
        }
        reader.onerror = function () {
            $().lawsDialog({
                dialogClass: 'lawsVnDialogTitle',
                messages: ['Quý khách vui lòng thử lại sau.'],
                showIcon: false
            });
            return;
        }
        reader.readAsDataURL(file);
    },
    processFile: function (dataUrl, fileType, fileName) {
        var maxWidth = 800;
        var maxHeight = 800;
        var image = new Image();
        image.src = dataUrl;

        image.onload = function () {
            var width = image.width;
            var height = image.height;
            var shouldResize = (width > maxWidth) || (height > maxHeight);

            if (!shouldResize) {
                lawsVn.sendFile(dataUrl, fileName);
                return;
            }

            var newWidth;
            var newHeight;

            if (width > height) {
                newHeight = height * (maxWidth / width);
                newWidth = maxWidth;
            } else {
                newWidth = width * (maxHeight / height);
                newHeight = maxHeight;
            }

            var canvas = document.createElement('canvas');

            canvas.width = newWidth;
            canvas.height = newHeight;

            var context = canvas.getContext('2d');

            context.drawImage(this, 0, 0, newWidth, newHeight);

            dataUrl = canvas.toDataURL(fileType);

            lawsVn.sendFile(dataUrl, fileName);
        };

        image.onerror = function () {
            $().lawsDialog({
                dialogClass: 'lawsVnDialogTitle',
                messages: ['Quý khách vui lòng thử lại sau.'],
                showIcon: false
            });
        };
    },
    sendFile: function (fileData, fileName) {
        var formData = new FormData();
        fileData = fileData.replace(/^data:image\/[a-z]+;base64,/, '');
        formData.append('imageData', fileData);
        formData.append('imageName', fileName);
        var progress = $('.container-bar');
        var progressBar = $('.progress > .progress-bar');
        progress.show();
        $.ajax({
            url: lawsVn.virtualPath('/Ajax/UploadFile'),
            type: 'POST',
            contentType: false,
            cache: false,
            processData: false,
            data: formData,
            xhr: function () {
                var jqXHR = null;
                if (window.ActiveXObject) {
                    jqXHR = new window.ActiveXObject("Microsoft.XMLHTTP");
                }
                else {
                    jqXHR = new window.XMLHttpRequest();
                }
                //Upload progress
                jqXHR.upload.addEventListener("progress", function (evt) {
                    if (evt.lengthComputable) {
                        var percentComplete = Math.round((evt.loaded * 100) / evt.total);
                        progressBar.css({
                            'width': percentComplete + '%',
                            'background-color': '#49c5c7'
                        });
                    }
                }, false);
                //Download progress
                jqXHR.addEventListener("progress", function (evt) {
                    if (evt.lengthComputable) {
                        var percentComplete = Math.round((evt.loaded * 100) / evt.total);
                        setTimeout(function() {
                                progress.hide();
                                progressBar.css({
                                    'width': 100 - percentComplete + '%',
                                    'background-color': 'none'
                                });
                            },
                            1000);
                    }
                }, false);
                return jqXHR;
            },
            success: function (resp) {
                if (resp.Completed) {
                    if (resp.Data != null && resp.Data.length > 0) {
                            $('input#Avatar').val(resp.Data);
                            $('#AccountAvatar').attr('src', resp.Data);
                        }
                } else {
                    $().lawsDialog({
                        dialogClass: 'lawsVnDialogTitle',
                        messages: ['Quý khách vui lòng thử lại sau.'],
                        showIcon: false
                    });
                }
            }
        });
    },
    virtualPath: function(patch) {
        var host = window.location.protocol + '//' + window.location.host;
        return host + patch;
    },
    showSlides:function() {
        var slides = $('.tinvanSlides');
        slides.hide();
        lawsVnConfig.slideIndex++;
        if (lawsVnConfig.slideIndex > slides.length) { lawsVnConfig.slideIndex = 1 }
        $(slides[lawsVnConfig.slideIndex - 1]).show();
        setTimeout(lawsVn.showSlides, 10000);
    },
    LawTerminsReset: function () {
        $('input[name="Tername"]').lawsExists(function () {
            $(this).val('');
        });
    },
    LawTerminTagReset: function () {
        lawsVn.ajaxEvents.pageIndex = 1; //reset pageIndex
        $('a[class="post-tag-abc"]').lawsExists(function () {
            $(this).removeClass('active');
        });
    },
    registerServiceFormReset: function () {
        var countSteps = $('div.law-steps', '.law-wizard').length;
        var i = countSteps == 3 ? 1 : (countSteps == 4 ? 2 : 1);
        $('div.law-steps', '.law-wizard').animate({
            opacity: 0
        }, 250, function () {
            $('div.law-steps' + ':nth-child('+i+')', '.law-wizard').animate({
                opacity: 1
            }, 250, function () {
                $('.navstep').LawScrollTo();
            }).show();
        }).hide();
        $('select[name="ServicePackageParentId"]').lawsExists(function () {
            $(this).removeClass('border-warning');
        });
        $('select[name="ServicePackageId"]').lawsExists(function () {
            $(this).removeClass('border-warning');
        });
        $('#price-selected-span').lawsExists(function () {
            $(this).text(0);
        });
        //form khuyến mại
        $('#PromotionCode').lawsExists(function () {
            $(this).val('');
        });
        $('#PromotionCodeCheckForm input').lawsExists(function () {
            $('#PromotionCodeCheckForm input').clearErrors();
        });
        $('input[type="hidden"][name="ServicePackageId"]').lawsExists(function () {
            $(this).val(0);
        });
        $('#promotionCodeCheckResult').lawsExists(function () {
            $(this).html('');
        });
        //thông tin đơn hàng
        $('#termOfSubscription-span').lawsExists(function () {
            $(this).text('');
        });
        $('#numberOfUsers-span').lawsExists(function () {
            $(this).text('');
        });
        $('#expiryDate-span').lawsExists(function () {
            $(this).text('');
        });
        $('.price-td').lawsExists(function () {
            $(this).text(0);
        });
        $('.priceVat-td').lawsExists(function () {
            $(this).text(0);
        });
        $('.promotionPrice-td').lawsExists(function () {
            $(this).text(0);
        });
        $('.total-td').lawsExists(function () {
            $(this).text(0);
        });
        //thanh toán đơn hàng qua thẻ atm

        $('input[type="hidden"][name="ServicePackageName"]').lawsExists(function () {
            $(this).val('');
        });
        $('input[type="hidden"][name="Total"]').lawsExists(function () {
            $(this).val(0);
        });
        $('input[type="hidden"][name="Price"]').lawsExists(function () {
            $(this).val(0);
        });
        $('input[type="hidden"][name="Amount"]').lawsExists(function () {
            $(this).val(0);
        });
        $('input[type="hidden"][name="PercentDecrease"]').lawsExists(function () {
            $(this).val(0);
        });
        $('input[type="hidden"][name="PromotionCodeBankAccount"]').lawsExists(function () {
            $(this).val('');
        });
        $('input[type="hidden"][name="CompanyName"]').lawsExists(function () {
            $(this).val('');
        });
        $('input[type="hidden"][name="CompanyAddress"]').lawsExists(function () {
            $(this).val('');
        });
        $('input[type="hidden"][name="CompanyTaxCode"]').lawsExists(function () {
            $(this).val('');
        });
        $('input[type="hidden"][name="InternalContent"]').lawsExists(function () {
            $(this).val('');
        });
    },
    removeVal: function(arr, val) {
        for (var i = 0; i < arr.length; i++) {
            if (arr[i].id == val)
                arr.splice(i, 1);
        }
    },
    numbersResultFound: function() {
        var totalpage = $('#tblcontent').data('totalpage');
        var pageindex = $('#tblcontent').data('pageindex');
        var pageCount = $('#tblcontent').data('pageCount');
        if (totalpage === undefined) {
            totalpage = 0;
        }
        if (pageindex === undefined) {
            pageindex = 0;
        }
        if (pageindex <= 0) {
            pageindex = 1;
        }
        var data = '';
        if (pageCount > 0) {
            data += '<span>Tìm thấy <strong> ' +
                totalpage +
                ' văn bản </strong>(' +
                pageindex +
                '/' +
                pageCount +
                ' trang)</span>';
        }
        $('#txtnumberresultfound').html(data);
    },
    ajaxEvents: {
        pageIndex: $('#txtChangePage').val(),
        showNumberOfResults: $('#dllNumberOfResults').length ? $('#dllNumberOfResults option:selected').val() : 20,
        pageTitle: $('title').text(),
        OnBegin: function() {
            $('#loading').fadeIn('normal');
        },
        OnComplete: function (element) {
            $('#loading').fadeOut('normal');
            if (element && typeof element == 'string' && lawsVn.isDomElement($('#' + element))) {
                $('#' + element).lawsExists(function () {
                    $(this).LawScrollTo();
                });
            }
            $('.no-permission').lawsExists(function () {
                if (!$('.no-permission').is(':ui-tooltip')) {
                    $('.no-permission').tooltip({
                        content: function () {
                            return $(this).prop('title');
                        },
                        position: {
                            at: 'center bottom',
                            my: 'left top'
                        },
                        show: {
                            effect: "slideDown",
                            delay: 250
                        },
                        close: function (event, ui) {
                            ui.tooltip.hover(
                                function () {
                                    $(this).stop(true).fadeTo(400, 1);
                                },
                                function () {
                                    $(this).fadeOut("400",
                                        function () {
                                            $(this).remove();
                                        });
                                });
                        }
                    });
                }
            });
        },
        OnCompleteV2: function (element) { 
            $('#loading').fadeOut('normal');
            if (element && typeof element == 'string' && lawsVn.isDomElement($('#' + element))) {
                $('#' + element).lawsExists(function () {
                    $(this).LawScrollTo();
                });
            }
            $('.no-permission').lawsExists(function () {
                if (!$('.no-permission').is(':ui-tooltip')) {
                    $('.no-permission').tooltip({
                        content: function () {
                            return $(this).prop('title');
                        },
                        position: {
                            at: 'center bottom',
                            my: 'left top'
                        },
                        show: {
                            effect: "slideDown",
                            delay: 250
                        },
                        close: function (event, ui) {
                            ui.tooltip.hover(
                                function () {
                                    $(this).stop(true).fadeTo(400, 1);
                                },
                                function () {
                                    $(this).fadeOut("400",
                                        function () {
                                            $(this).remove();
                                        });
                                });
                        }
                    });
                }
            });
            lawsVn.ajaxEvents.pageIndex = $('#txtChangePage').val();
            var pageTitle = lawsVn.ajaxEvents.pageIndex > 1
                ? lawsVn.ajaxEvents.pageTitle + ' - trang ' + lawsVn.ajaxEvents.pageIndex
                : lawsVn.ajaxEvents.pageTitle;
            var url = lawsVn.replaceUrlParam({ 'page': lawsVn.ajaxEvents.pageIndex > 1 ? lawsVn.ajaxEvents.pageIndex : '', 'pSize': lawsVn.ajaxEvents.showNumberOfResults });
            lawsVn.setTitleAndHistory(pageTitle, url);
        },
        OnSuccess: function(data, status, xhr) {
            if (data.Message !== void 0 && data.Message.length > 0) {
                if (data.Message === 'ModelStateInValid') {
                    $().lawsDialog({
                        messages: ['Quý khách vui lòng kiểm tra lại các thông tin có cảnh báo màu đỏ.'],
                        dialogClass: 'lawsVnDialogTitle',
                        showIcon: false
                    });
                } else
                    $().lawsDialog({
                        messages: [data.Message],
                        dialogClass: 'lawsVnDialogTitle',
                        showIcon: false,
                        onClose: function () {
                            var activeDialogs = $(".ui-dialog:visible").find('.ui-dialog-content');
                            activeDialogs.dialog('close');
                        }
                    });
            }
        },
        AccountLoginOnSuccess: function (data, status, xhr) {
            if (data.Completed) {
                //$('#lawsVnLoginForm').bind('submit', function (e) { e.preventDefault(); });
                //window.location.replace(data.ReturnUrl);
                if (data.ReturnUrl != null && data.ReturnUrl.length > 0) {
                    //Kích hoạt tk thành công => chuyển hướng về homepage
                    if (data.ReturnUrl.indexOf('NewUserActive') !== -1) {
                        window.location.assign(lawsVnConfig.rootPath);
                    } else {
                        setTimeout(function () {
                                window.location.href = data.ReturnUrl;
                                location.reload();
                            },
                            100);
                    }
                } else window.location.href = lawsVnConfig.rootPath;
            } else if (data.Message != null && data.Message.length > 0) {
                //$('#lawsVnLoginForm').unbind('submit');
                var username = '';
                if (data.Message.indexOf('The provided anti-forgery token was meant for user') > -1) {
                    data.Message = data.Message.replace(/"/g, "'");
                    var str = data.Message.match(/'(.*?)'/g);
                    if (str != null) username = str[1];
                } 
                $().lawsDialog({
                    messages: [
                        username.length > 0
                        ? 'Tài khoản: <b>' +
                        username +
                        '</b> hiện đã đăng nhập. Quý khách vui lòng refresh lại trang web để tiếp tục sử dụng.'
                        : data.Message
                    ],
                    dialogClass: 'lawsVnDialogTitle',
                    showIcon: false
                });
            }
        },
        LaweQuestionOnSuccess: function(data, status, xhr) {
            if (data.Completed) {
                $().lawsDialog({
                    messages: [data.Message],
                    dialogClass: 'lawsVnDialogTitle',
                    showIcon: false,
                    onClose: function() {
                        $('#lawsVnLawerQuestion').lawsExists(function() {
                            $(this).dialog('close');
                        });
                    }
                });
            } else if (data.Message !== void 0 && data.Message.length > 0) {
                $().lawsDialog({
                    messages: [data.Message],
                    dialogClass: 'lawsVnDialogTitle',
                    showIcon: false
                });
            }
        },
        LoginOnSuccessful: function(data, status, xhr) {
            if (data.Completed) {
                $().lawsDialog({
                    messages: [data.Message],
                    dialogClass: 'lawsVnDialogTitle',
                    showIcon: false,
                    onOpen: function() {
                        $("#lawsVnLogin").dialog("close");
                    },
                    onClose: function () {
                        if (data.ReturnUrl != null && data.ReturnUrl.length > 0)
                            setTimeout(function () {
                                    window.location.href = data.ReturnUrl;
                                    //location.reload();
                                },
                                100);
                        else window.location.href = lawsVnConfig.rootPath;
                        return false;
                    }
                });
            } else if (data.Message !== void 0 && data.Message.length > 0) {
                $().lawsDialog({
                    messages: [data.Message],
                    dialogClass: 'lawsVnDialogTitle',
                    showIcon: false
                });
            }
        },
        RedirectOnSuccess: function (data, status, xhr) {
            if (data.Completed) {
                if (data.ReturnUrl != null && data.ReturnUrl.length > 0)
                    setTimeout(function () {
                            window.location.href = data.ReturnUrl;
                            //location.reload();
                        },
                        100);
                else window.location.href = lawsVnConfig.rootPath;

            } else if (data.Message !== void 0 && data.Message.length > 0) {
                $().lawsDialog({
                    messages: [data.Message],
                    dialogClass: 'lawsVnDialogTitle',
                    showIcon: false
                });
            }
        },
        UpdateFieldOnSuccessful: function (data, status, xhr) {
            if (data.Completed) {
                $().lawsDialog({
                    messages: [data.Message],
                    dialogClass: 'lawsVnDialogTitle',
                    showIcon: false,
                    onOpen: function () {
                        $("#lawsVnLogin").dialog("close");
                    },
                    onClose: function () {
                        if (data.ReturnUrl != null && data.ReturnUrl.length > 0)
                            setTimeout(function () {
                                window.location.href = data.ReturnUrl;
                                location.reload();
                            },
                                100);
                        else window.location.href = lawsVnConfig.rootPath;
                    }
                });
            } else if (data.Message !== null && data.Message.length > 0) {
                $().lawsDialog({
                    messages: [data.Message],
                    dialogClass: 'lawsVnDialogTitle',
                    showIcon: false
                });
            }
        },
        LoginOnSuccess: function(data, status, xhr) {
            if (data.Completed) {
                $().lawsDialog({
                    messages: [data.Message],
                    dialogClass: 'lawsVnDialogTitle',
                    onOpen: function() {
                        $("#lawsVnLogin").lawsExists(function() {
                            $(this).dialog("close");
                        });
                        $("#changePassword").lawsExists(function() {
                            $(this).dialog("close");
                        });
                    },
                    onClose: function() {
                        if (data.ReturnUrl != null && data.ReturnUrl.length > 0)
                        setTimeout(function () {
                            window.location.href = data.ReturnUrl;
                        //location.reload();
                        }, 100);
                        else window.location.href = lawsVnConfig.rootPath;
                    }
                });
                $('#gopy').lawsExists(function () {
                    lawsVn.closeDialogById('gopy');
                });
                $('#docSendMail').lawsExists(function () {
                    lawsVn.closeDialogById('docSendMail');
                });
            } else {
                if (data.Message === 'ModelStateInValid') {
                    $().lawsDialog({
                        messages: ['Quý khách vui lòng kiểm tra lại các thông tin có cảnh báo màu đỏ.'],
                        dialogClass: 'lawsVnDialogTitle',
                        showIcon: false
                    });
                } else
                    $().lawsDialog({
                        messages: [data.Message],
                        dialogClass: 'lawsVnDialogTitle',
                        showIcon: false
                    });
            }
        },
        RedirectOnSuccessV2: function (data, status, xhr) {
            if (data.Completed) {
                if (data.ReturnUrl != null && data.ReturnUrl.length > 0) {
                    setTimeout(function () {
                        window.location.href = data.ReturnUrl;
                    }, 100);
                }
            } else if (data.Message !== null && data.Message.length > 0) {
                var username = '';
                if (data.Message.indexOf('The provided anti-forgery token was meant') > -1) {
                    var str = data.Message.match(/\"(.*?)\"/);
                    if (str != null) username = str[1];
                }

                $().lawsDialog({
                    messages: [username.length > 0 ? 'Tài khoản: <b>' + username + '</b> hiện đã hết thời gian đăng nhập. Quý khách vui lòng refresh lại trang web để tiếp tục sử dụng.' : data.Message],
                    dialogClass: 'lawsVnDialogTitle',
                    showIcon: false
                });
            }
        },
        ServiceRegistrationOnSuccess: function(data, status, xh) {
            if (data.Completed) {
                $('#OrderCode').html('Mã đơn hàng dịch vụ của Quý khách:<strong style="color: #a67942;"> ' +
                    data.Message +
                    ' </strong>');
            } else {
                $().lawsDialog({
                    dialogClass: 'lawsVnDialogTitle',
                    messages: [data.Message],
                    showIcon: false
                });
            }
        },
        PromotionCodeCheckOnSuccess: function (data, status, xhr) {
            var price = 0;
            $('input[type="hidden"][name="Price"]').lawsExists(function () {
                if ($.isNumeric($(this).val())) {
                    price = parseInt($(this).val());
                }
            });
            if (data.Completed) {
                if (data.Data != null) {
                    var result =
                        '<p style="margin-top:10px; line-height: 24px; color: #222;">Thông tin mã khuyến mại:<strong style="color:#a67942;"> ' +
                            data.Data.PromotionDesc +
                            ' </strong></p>';
                    if (data.Data.NumMonthFree > 0) {
                        result +=
                            '<p style="line-height: 24px; color: #222;">Khuyến mại:<strong style="color:#a67942;"> ' +
                            data.Data.NumMonthFree +
                            ' tháng</strong></p>';
                    }
                    if (data.Data.NumDayFree > 0) {
                        result +=
                            '<p style="line-height: 24px; color: #222;">Khuyến mại:<strong style="color:#a67942;"> ' +
                            data.Data.NumDayFree +
                            ' ngày</strong></p>';
                    }
                    if (data.Data.PercentDecrease > 0) {
                        result +=
                            '<p style="line-height: 24px; color: #222;">Khuyến mại:<strong style="color:#a67942;"> ' +
                            data.Data.PercentDecrease +
                            '%</strong></p>';
                    } else {
                        if (data.Data.Amount > 0) {
                            result +=
                                '<p style="line-height: 24px; color: #222;">Khuyến mại:<strong style="color:#a67942;"> ' +
                                lawsVn.formatNumber(data.Data.Amount, '.', '.') +
                                ' VNĐ</strong></p>';
                        }
                    }
                    $('#promotionCodeCheckResult').lawsExists(function() {
                        $(this).html(result);
                    });
                    $('input[type="hidden"][name="PromotionCodeBankAccount"]').lawsExists(function () {
                        $(this).val(data.Data.PromotionDesc);
                    });
                    $('input[type="hidden"][name="PromotionCodeScratchCard"]').lawsExists(function () {
                        $(this).val(data.Data.PromotionDesc);
                    });
                    if (data.Data.Amount > 0) {
                        $('input[type="hidden"][name="Amount"]').lawsExists(function() {
                            $(this).val(data.Data.Amount);
                        });
                    } else {
                        $('input[type="hidden"][name="PercentDecrease"]').lawsExists(function () {
                            $(this).val(data.Data.PercentDecrease);
                        });
                    }
                    $('.promotionPrice-td').lawsExists(function () {
                        if (price > 0) {
                            $(this).text(data.Data.Amount > 0
                                ? lawsVn.formatNumber(data.Data.Amount, '.', '.')
                                : (data.Data.PercentDecrease > 0
                                    ? lawsVn.formatNumber(price * data.Data.PercentDecrease / 100, '.', '.') + ' VNĐ'
                                    : 0));
                        }
                    });
                    $('.total-td').lawsExists(function () {
                        if (price > 0) {
                            var vat = price * 10 / 100;
                            var pricePromotion = data.Data.Amount > 0
                                ? data.Data.Amount
                                : (data.Data.PercentDecrease > 0
                                    ? price * data.Data.PercentDecrease / 100
                                    : 0);
                            var total = price + vat - pricePromotion;
                            $(this).text(lawsVn.formatNumber(total, '.', '.') + ' VNĐ');
                        }
                    });
                }
            } else {
                if (data.Message !== void 0 && data.Message.length > 0) {
                    $().lawsDialog({
                        dialogClass: 'lawsVnDialogTitle',
                        messages: [data.Message]
                    });
                }
                $('#promotionCodeCheckResult').lawsExists(function () {
                    $(this).empty();
                });
                $('#PromotionCodeBankAccount').lawsExists(function() {
                    $(this).val('');
                });
                $('#PromotionCodeScratchCard').lawsExists(function() {
                    $(this).val('');
                });
                //tính lại tổng tiền
                $('.promotionPrice-td').lawsExists(function () {
                    $(this).text(0);
                });
                $('.total-td').lawsExists(function () {
                    var vat = price * 10 / 100;
                    var total = price + vat;
                    $(this).text(lawsVn.formatNumber(total, '.', '.'));
                });
            }
        },
        TaxInvoiceOnComplete: function(data, status, xhr) {
            if (data.Completed) {
                if (data.Data !== void 0 && data.Data !== null) {
                    $('input[name="CompanyName"]').val(data.Data.CompanyName);
                    $('input[name="CompanyAddress"]').val(data.Data.CompanyAddress);
                    $('input[name="CompanyTaxCode"]').val(data.Data.CompanyTaxCode);
                    $('input[name="InternalContent"]').val(data.Data.InternalContent);
                }
                $('#TaxInvoiceFormLoad').dialog('close');
            } else if (data.Message !== void 0 && data.Message.length > 0) {
                $().lawsDialog({
                    dialogClass: 'lawsVnDialogTitle',
                    messages: [data.Message],
                    showIcon: false
                });
            }

        },
        PaymentServiceUsingBankAccountOnSuccess: function (data, status, xhr) {
            if (data.Completed && data.Data != null) {
                if (data.Data.PayGateUrl != null && data.Data.PayGateUrl != '') {
                    window.location = data.Data.PayGateUrl;
                    return;
                }
                else if (data.Message !== void 0 && data.Message.length > 0) {
                    $().lawsDialog({
                        dialogClass: 'lawsVnDialogTitle',
                        messages: [data.Message],
                        showIcon: false
                    });
                }
            } else if (data.Message !== void 0 && data.Message.length > 0) {
                $().lawsDialog({
                    dialogClass: 'lawsVnDialogTitle',
                    messages: [data.Message],
                    showIcon: false
                });
            }
        },
        OnSuccessValid: function(element) {
            $('#loading').fadeOut('normal');
            $('#' + element).lawsExists(function() {
                var form = $('#' + element);
                form.removeData('validator');
                form.removeData('unobtrusiveValidation');
                $.validator.unobtrusive.parse(form);
            });
        },
        LawTerminsOnSuccess: function (data, status, xhr) {
            if (status == 'success') {
                $('#txtnumberresultfound').html('');
                $("#txtnumberpage").html('');
                $('#tblcontent').lawsExists(function () {
                    var totalpage = $(this).data('totalpage');
                    var pageIndex = $(this).data('pageindex');
                    var pageCount = $(this).data('pageCount');
                    var data = '';
                    if (typeof totalpage == 'undefined') {
                        totalpage = 0;
                    }
                    if (typeof pageIndex == 'undefined') {
                        pageIndex = 1;
                    }
                    if (pageIndex <= 0) {
                        pageIndex = 1;
                    }
                    if (totalpage > 0) {
                        data = " <span>Tìm thấy <span class='color22'><strong>" +
                            lawsVn.formatNumber(totalpage, '.', '.') +
                            "</span> thuật ngữ</strong></span>";
                        data += ' (' + pageIndex + '/' + pageCount + ' trang)';
                        $('#txtnumberresultfound').lawsExists(function () {
                            $(this).html(data);
                        });
                    }
                });

                var keyword = '', keywordTemp = '';
                var lawTerminsTitle = '';
                var temp = '';
                $('#Tername').lawsExists(function () {
                    keyword = $(this).val();
                });
                keywordTemp = lawsVn.stringUtils(keyword);
                $('td.xs3 > .rowthuatngu').each(function (i, item) {
                    lawTerminsTitle = $(item).html();
                    temp = lawsVn.stringUtils(lawTerminsTitle);
                    var location = temp.indexOf(keywordTemp);
                    if (location > 0) {
                        var str1 = lawTerminsTitle.substr(location, keyword.length);
                        lawTerminsTitle =
                            lawTerminsTitle.replace(str1, '<span style="background-color:Yellow;">' + str1 + '</span>');
                        $(this).html(lawTerminsTitle);
                    }
                });
            };
        },
        SearchOnSuccess: function (data, status, xhr) {
            lawsVn.searchOnSuccess();
        }
    },
    replaceUrlParam: function (obj, reset) {
        var url = document.URL,
            baseUrl = url.split('?')[0],
            query = url.split('?')[1], params = {}, hash, index = 0;
        if (typeof (obj) == 'object') {
            if (query != undefined) {
                if (reset == undefined || reset !== 1) {
                    query = query.split('&');
                    for (var i = 0; i < query.length; i++) {
                        hash = query[i].split('=');
                        params[hash[0]] = hash[1];
                    }
                }
            }

            for (var key in obj) {
                params[key] = obj[key];
            }

            for (var key in params) {
                if (params[key] !== '') {
                    baseUrl += (index == 0 ? '?' : '&') + key + '=' + params[key];
                    index++;
                }
            }
            return baseUrl;
        }
        return url;
    },
    urlParamAddOrUpdate: function (key, value) {
        var baseUrl = [location.protocol, '//', location.host, location.pathname].join(''),
            urlQueryString = document.location.search,
            newParam = key + '=' + value,
            params = '?' + newParam;
        //if (urlQueryString) {
        //    var keyRegex = new RegExp('([\?&])' + key + '[^&]*');
        //    // neu param da co => update
        //    if (urlQueryString.match(keyRegex) !== null) {
        //        params = key=='page' && value == 1 ? '' : urlQueryString.replace(keyRegex, "$1" + newParam);
        //    } else { // chua co => them param vao cuoi url
        //        params = urlQueryString + '&' + newParam;
        //    }
        //}
        if (urlQueryString) {
            var updateRegex = new RegExp('([\?&])' + key + '[^&]*');
            var removeRegex = new RegExp('([\?&])' + key + '=[^&;]+[&;]?');

            if (typeof value == 'undefined' || value == null || value == '') { // xoa param neu ko co value
                params = urlQueryString.replace(removeRegex, "$1");
                params = params.replace(/[&;]$/, "");

            } else if (urlQueryString.match(updateRegex) !== null) { // cap nhat value cho param
                params = key == 'page' && value == 1 ? '' : urlQueryString.replace(updateRegex, "$1" + newParam);

            } else { // them param vao cuoi url
                params = urlQueryString + '&' + newParam;
            }
        }
        params = params == '?' ? '' : params;
        return baseUrl + params;
    },
    SetGetParameter: function (paramName, paramValue, link) {
        var url = link == undefined ? window.location.href.replace(window.location.hash, '') : link;
        if (url.indexOf(paramName + '=') >= 0) {
            var prefix = url.substring(0, url.indexOf(paramName));
            var suffix = url.substring(url.indexOf(paramName)); 
            suffix = suffix.substring(suffix.indexOf('=') + 1);
            suffix = (suffix.indexOf('&') >= 0) ? suffix.substring(suffix.indexOf('&')) : '';
            url = prefix + paramName + '=' + paramValue + suffix;
        } else {
            if (url.indexOf('?') < 0)
                url += '?' + paramName + '=' + paramValue;
            else
                url += '&' + paramName + '=' + paramValue;
        }
        url = url.replace('?page=1&', '?').replace('?page=1', '').replace('&page=1', '');
        url += window.location.hash;
        return url;
    },
    setTitleAndHistory: function (title, path) {
        // Set history
        if (window.history && window.history.pushState) {
            history.pushState(null, title, path);
        }
        // Set title
        $('title').html(title);
    },
    isDomElement: function (el) {
        return el instanceof HTMLElement ||
            el[0] instanceof HTMLElement
            ? true
            : false;
    },
    stringUtils:function(string) {
        var str = string;
        str = str.toLowerCase();
        str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ  |ặ|ẳ|ẵ/g, "a");
        str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
        str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
        str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ  |ợ|ở|ỡ/g, "o");
        str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
        str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
        str = str.replace(/đ/g, "d");
        str = str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'| |\"|\&|\#|\[|\]|~|$|_/g, " ");
        /* tìm và thay thế các kí tự đặc biệt trong chuỗi sang kí tự - */
        str = str.replace(/-+-/g, " "); //thay thế 2- thành 1-
        str = str.replace(/^\-+|\-+$/g, "");
        //cắt bỏ ký tự - ở đầu và cuối chuỗi 
        return str;
    },
    searchOnSuccess: function () {
        var keywords = '';
        $('#Keywords', $('.row-tim-kiem')).lawsExists(function () {
            keywords = $(this).val();
        });
        $('#txtnumberresultfound').html('');
        $("#txtnumberpage").html('');
        $('#tblcontent').lawsExists(function () {
            var totalpage = $(this).data('totalpage');
            var indexFrom = $(this).find(' th:first').text();
            var indexTo = $(this).find('th:last').text();
            if (typeof totalpage == 'undefined') {
                totalpage = 0;
            }
            if (totalpage > 0) {
                var data;
                if (keywords.length > 0) {
                    data = " <strong>Tìm thấy <span class='color2'>" +
                        lawsVn.formatNumber(totalpage, '.', '.') +
                        "</span> văn bản với từ khóa \"<span class='color2'>" +
                        keywords +
                        "</span>\"</strong>";
                    $('#txtnumberresultfound').html(data);
                } else {
                    data = " <strong>Tìm thấy <span class='color2'>" +
                        lawsVn.formatNumber(totalpage, '.', '.') +
                        "</span> văn bản.</strong>";
                    $('#txtnumberresultfound').html(data);
                }
                if (indexFrom != indexTo) {
                    var datanumerpage = "<strong>Kết quả " +
                        indexFrom +
                        "-" +
                        indexTo +
                        " trong " +
                        totalpage +
                        " văn bản </strong>";
                    $("#txtnumberpage").html(datanumerpage);
                }
            }
        });
        $('.resultsFilterBy').lawsExists(function() {
            if (lawsVnConfig.arrLinhvuctracuu.length == 0 &&
                lawsVnConfig.arrCoquanbanhanh.length == 0 &&
                lawsVnConfig.arrTrangthaihieuluc.length == 0 &&
                lawsVnConfig.arrLoaivanban.length == 0 & lawsVnConfig.arrNambanhanh.length == 0) {
                $('.resultsFilterBy').hide();
            }
        });
    },
    closeDialog:function() {
        var activeDialogs = $(".ui-dialog:visible").find('.ui-dialog-content');
        activeDialogs.dialog('close');
    },
    closeDialogById: function (id) {
       $('#' + id).dialog('close');
    },
    getCaptcha: function (id, prefix) {
        var uuid = this.generateUUID();
        var captchaUrl = lawsVn.virtualPath('/lawvn-captcha.html?id=' + uuid + '&prefix=' + prefix);
        $('#' + id).attr('src', captchaUrl);
        return false;
    },
    openTerms: function () {
        return;
    },
    generateUUID: function() {
        var d = new Date().getTime();
        if (window.performance && typeof window.performance.now === "function") {
            d += performance.now();
        }
        var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g,
            function(c) {
                var r = (d + Math.random() * 16) % 16 | 0;
                d = Math.floor(d / 16);
                return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
            });
        return uuid;
    },
    formatNumber: function(nStr, decSeperate, groupSeperate) {
        nStr += '';
        var x = nStr.split(decSeperate);
        var x1 = x[0];
        var x2 = x.length > 1 ? '.' + x[1] : '';
        var rgx = /(\d+)(\d{3})/;
        while (rgx.test(x1)) {
            x1 = x1.replace(rgx, '$1' + groupSeperate + '$2');
        }
        return x1 + x2;
    },
    swapImageUrl: function() {
        var $this = $(this);
        var newUrl = $this.data('hover');
        $this.data('hover', $this.attr('src'));
        $this.attr('src', newUrl);
    },
    topFunction: function() {
        document.body.scrollTop = 0;
        document.documentElement.scrollTop = 0;
    },
    scrollFunction: function() {
        if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
            document.getElementById("myBtn").style.display = "block";
        } else {
            document.getElementById("myBtn").style.display = "none";
        }
        if ($(window).scrollTop() + $(window).height() > $(document).height() - 100) {
            document.getElementById("myBtn").style.display = "none";
        }
    },
    successfulNewsletter: function(text) {
        if (text !== null && text.length) {
            $().lawsDialog({
                dialogClass: 'lawsVnDialogTitle',
                messages: [text],
                showIcon: false,
                onClose: function() {
                    $('#Email').val('');
                }
            });
        }
    },
    delay: function() {
        var timer = 0;
        return function(callback, ms) {
            clearTimeout(timer);
            timer = setTimeout(callback, ms);
        };
    },
    clearAvatar: function() {
        $('input[id="avatarFile"]').lawsExists(function() {
            $(this).val('');
            $('input#Avatar').val('');
            $('#AccountAvatar').attr('src', lawsVnConfig.rootPath + 'assets/images/150x180.png');
        });
    },
    RegisterAccount: function() {
        lawsValidate.termsAndConditions();
        lawsValidate.multiCheckboxRequired();
        lawsValidate.fileType();
        lawsValidate.maxFileSize();
        //lawsValidate.age();
        lawsValidate.formatDate();
    },
    FormReset: function() {
        lawsVn.clearAvatar();
        $('#RegisterForm')[0].reset();
        return false;
    },
    ResetForm: function(form) {
        form.find('input:text, input:password, input:file, select, textarea').val('');
        form.find('input:radio, input:checkbox')
            .removeAttr('checked').removeAttr('selected');
    },
    widgetUser: function() {
        $('#user-vip').lawsExists(function() {
            $('.user-vip').lawsExists(function() {
                $(this).show();
            });
        });
        $('#countMyMessages').lawsExists(function() {
            $('.countMyMessages').lawsExists(function() {
                $(this).text($('#countMyMessages').text());
            });
        });
        $('#countMyDocuments').lawsExists(function() {
            $('.countMyDocuments').lawsExists(function() {
                $(this).text($('#countMyDocuments').text());
            });
        });
        $('#countNoticeOfValidity').lawsExists(function() {
            $('.countNoticeOfValidity').lawsExists(function() {
                $(this).text($('#countNoticeOfValidity').text());
            });
        });
        $('#countPaymentTransactionSuccess').lawsExists(function () {
            $('.countPaymentTransactionSuccess').lawsExists(function () {
                $(this).text($('#countPaymentTransactionSuccess').text());
            });
        });
        $('#gia-han-dv-header').lawsExists(function() {
            $('#gia-han-dv').lawsExists(function() {
                $(this).html($('#gia-han-dv-header').html());
            });
        });
    },
    TCVNFilter: function(doctypeid) {
        var url = lawsVnConfig.rootPath + 'Ajax/TCVN_GetViewSearch';
        var fieldId = $("#dllField option:selected").val();
        var EffectStatusId = $("#dllEffectStatus option:selected").val();
        var OrgansId = $("#dllOrgans option:selected").val();
        var DocTypesId = $("#dllDocTypes option:selected").val();
        if (DocTypesId > 61) {
            doctypeid = DocTypesId;
        }
        var dataGetter = {
            'fieldId': fieldId,
            'effectStatusId': EffectStatusId,
            'organId': OrgansId,
            'docTypeId': doctypeid,
            'updateTargetId': "ListByField",
            'actionName': "TCVN_GetViewSearch"
        };
        $.lawsVnAjax(url,
            'Get',
            dataGetter,
            function(resp) {
                $("#ListByField").html(resp);
            });
    },

    VBHNFilter: function(doctypeid) { //cancel
        var url = lawsVnConfig.rootPath + 'Ajax/DocsConsolidation_GetViewSearch';
        var fieldId = $("#dllField option:selected").val();
        //var EffectStatusId = $("#dllEffectStatus option:selected").val();
        var OrgansId = $("#dllOrgans option:selected").val();
        var DocTypesId = $("#dllDocTypes option:selected").val();
        var dataGetter = {
            'fieldId': fieldId,
            'organId': OrgansId,
            'docTypeId': doctypeid,
            'updateTargetId': "ListByField",
            'actionName': "DocsConsolidation_GetViewSearch"
        };
        $.lawsVnAjax(url,
            'Get',
            dataGetter,
            function(resp) {
                $("#ListByField").html(resp).focus();
            });
    },
    LawerFilter: function() {
        var url = lawsVnConfig.rootPath + 'Ajax/Lawer_GetViewSearch';
        var fieldId = $("#dllFields option:selected").val();
        //var EffectStatusId = $("#dllEffectStatus option:selected").val();
        var ProvinceId = $("#dllProvinces option:selected").val();
        var DistrictId = $("#dllDistricts option:selected").val();
        var WardId = $("#dllWards option:selected").val();
        var Keyword = $("#Keyword").val();
        var dataGetter = {
            'ProvinceId': ProvinceId,
            'DistrictId': DistrictId,
            'WardId': WardId,
            'Keyword': Keyword,
            'FieldId': fieldId,
            'updateTargetId': "ListByField",
            'actionName': "Lawer_GetViewSearch"
        };
        $.lawsVnAjax(url,
            'Get',
            dataGetter,
            function(resp) {
                $("#ListByField").html(resp).focus();
            });
    },
    LawerQuestion: function(lawerid) {
        $('<div id="lawsVnLawerQuestion"></div>').lawsDialog({
            title: '',
            width: 640,
            buttons: {},
            hideClose: false,
            closeText: 'Đóng',
            onCreate: function() {
                $('#loading').fadeIn('normal');
            },
            onOpen: function() {
                $('#lawsVnLawerQuestion').load(lawsVnConfig.rootPath + 'Ajax/PartialLawerQuestion?lawerid=' + lawerid,
                    function() {
                        var form = $('#lawsVnQuestionForm');
                        $('#ReturnUrl').val(lawsVnConfig.returnUrl);
                        form.removeData('validator');
                        form.removeData('unobtrusiveValidation');
                        $.validator.unobtrusive.parse(form);
                    });
                $('#loading').fadeOut('normal');
            }
        });
    },
    FaqViewDetail: function(faqId) {
        $('<div id="lawsVnFaqViewDetail"></div>').lawsDialog({
            title: '',
            width: 800,
            buttons: {},
            hideClose: false,
            closeText: 'Đóng',
            position: { at: 'center top' },
            onCreate: function() {
                $('#loading').fadeIn('normal');
            },
            onOpen: function() {
                $('#lawsVnFaqViewDetail').load(lawsVnConfig.rootPath + 'Ajax/FaqViewDetail?FaqId=' + faqId,
                    function() {
                        var form = $('#lawsVnFaqViewDetailForm');
                        $('#ReturnUrl').val(lawsVnConfig.returnUrl);
                        form.removeData('validator');
                        form.removeData('unobtrusiveValidation');
                        $.validator.unobtrusive.parse(form);
                    });
                $('#loading').fadeOut('normal');
            }
        });
    },
    UBNDFilter: function(doctypeid) {
        var url = lawsVnConfig.rootPath + 'Ajax/DocsUBND_GetViewSearch';
        var Provinceid = 0;
        var Year = 0;
        var fieldId = $("#dllField option:selected").val();
        var EffectStatusId = $("#dllEffectStatus option:selected").val();
        var DocTypesId = $("#dllDocTypes option:selected").val();
        var searchParams = new URLSearchParams(window.location.search);
        if (searchParams.get("provinceid") != null) {
            Provinceid = searchParams.get("provinceid");
        } else if (searchParams.get("year") != null) {
            Year = searchParams.get("year");
        } else if (searchParams.get("fieldid") != null && fieldId == 0) {
            fieldId = searchParams.get("fieldid");
        }

        var dataGetter = {
            'fieldId': fieldId,
            'effectStatusId': EffectStatusId,
            'docTypeId': DocTypesId,
            'ProvinceId': Provinceid,
            'year': Year,
            'updateTargetId': "ListByField",
            'actionName': "DocsUBND_GetViewSearch"
        };
        $.lawsVnAjax(url,
            'Get',
            dataGetter,
            function(resp) {
                $("#ListByField").html(resp);
            });
    },
    CustomerUBNDFilter: function(DocGroupId) {
        var url = lawsVnConfig.rootPath + 'Ajax/Customers_InterfaceLocation';
        var Provinceid = 0;
        var Year = 0;
        var fieldId = $("#dllField option:selected").val();
        var EffectStatusId = $("#dllEffectStatus option:selected").val();
        var DocTypesId = $("#dllDocTypes option:selected").val();
        Provinceid = $("#dllProvinces option:selected").val();
        var OrderByClauses = $("#dllOrderByClauses option:selected");
        var datefrom = OrderByClauses.val();
        var dateto = OrderByClauses.attr('id');

        var dataGetter = {
            'docGroupId': DocGroupId,
            'fieldId': fieldId,
            'effectStatusId': EffectStatusId,
            'docTypeId': DocTypesId,
            'ProvinceId': Provinceid,
            'datefrom': datefrom,
            'dateto': dateto,
            'updateTargetId': "FirstBox",
            'actionName': "Customers_InterfaceLocation",
        };
        $.lawsVnAjax(url,
            'Get',
            dataGetter,
            function(resp) {
                $("#FirstBox").html(resp);
            });
    },

    CustomerUBNDFilterSecond: function(DocGroupId, Provinceid) {
        var url = lawsVnConfig.rootPath + 'Ajax/Customers_InterfaceLocation';
        var Year = 0;
        var fieldId = $("#dllFieldSecond option:selected").val();
        var organId = $("#dllOrgansSecond option:selected").val();
        var DocTypesId = $("#dllDocTypes option:selected").val();
        var OrderByClauses = "";
        var datefrom = "";
        var dateto = "";
        var dataGetter = {
            'fieldId': fieldId,
            'organId': organId,
            'docTypeId': DocTypesId,
            'ProvinceId': Provinceid,
            'datefrom': datefrom,
            'dateto': dateto,
            'updateTargetId': "SecondBox",
            'actionName': "Customers_InterfaceLocation",
        };
        $.lawsVnAjax(url,
            'Get',
            dataGetter,
            function(resp) {
                $("#SecondBox").html(resp);
            });
    },
    CustomerInterFaceChangeField: function(fieldId) {
        var EffectStatusId = $("#dllEffectStatus option:selected").val();
        var DocTypesId = $("#dllDocTypes option:selected").val();
        var ProvinceId = $("#dllCustomerProvinces option:selected").val();
        //var fieldId = $("#sectionContent").attr('data-fieldid');
        $("#sectionContent").attr("data-fieldid", fieldId);
        lawsVn.CustomerInterFaceChangeUpdate(fieldId, EffectStatusId, DocTypesId,ProvinceId);
    },
    CustomerInterFaceChangeDllField: function() {
        var EffectStatusId = $("#dllEffectStatus option:selected").val();
        var DocTypesId = $("#dllDocTypes option:selected").val();
        var fieldId = $("#dllAllField option:selected").val();
        var ProvinceId = $("#dllCustomerProvinces option:selected").val();
        if (fieldId == 0) {
            fieldId = $("#sectionContent").attr('data-fieldid');
        }
        lawsVn.CustomerInterFaceChangeUpdate(fieldId, EffectStatusId, DocTypesId,ProvinceId);
    },
    CustomerInterFaceChange: function() {
        var EffectStatusId = $("#dllEffectStatus option:selected").val();
        var DocTypesId = $("#dllDocTypes option:selected").val();
        var ProvinceId = $("#dllCustomerProvinces option:selected").val();
        var fieldId = $("#sectionContent").attr('data-fieldid');
        lawsVn.CustomerInterFaceChangeUpdate(fieldId, EffectStatusId, DocTypesId, ProvinceId);
    },
    CustomerInterFaceChangeUpdate: function (fieldId, EffectStatusId, DocTypesId, ProvinceId) {
        var url = lawsVnConfig.rootPath + 'Ajax/Customers_InterfaceByField';
        var Provinceid = 0;
        var Year = 0;
        if (fieldId == null) {
            fieldId = 0;
        }
        if (EffectStatusId == null) {
            EffectStatusId = 0;
        }
        if (DocTypesId == null) {
            DocTypesId = 0;
        }
        if (ProvinceId == null) {
            ProvinceId = 0;
        }
        var dataGetter = {
            'ProvinceId':ProvinceId,
            'fieldId': fieldId,
            'effectStatusId': EffectStatusId,
            'docTypeId': DocTypesId,
            'updateTargetId': "ContentByField",
            'actionName': "Customers_InterfaceByField",
        };
        $.lawsVnAjax(url,
            'Get',
            dataGetter,
            function(resp) {
                $("#ContentByField").html(resp);
                $("#sectionContent").attr("data-fieldid", fieldId);
            });
    },
    CustomerInterFaceTCVNC: function(fieldId, DocTypesId) {
        var url = lawsVnConfig.rootPath + 'Ajax/Customers_InterfaceTCVN';
        if (fieldId == null) {
            fieldId = 0;
        }
        if (DocTypesId == null) {
            DocTypesId = 0;
        }
        if (DocTypesId == null || DocTypesId == 0)
        {
            $('#tabcontentDoctype').lawsExists(function () {
                DocTypesId = $(this).attr('data-doctype');
                if (typeof DocTypesId == 'undefined') {
                    DocTypesId = 61;
                }
            });
        }
        var dataGetter = {
            'fieldId': fieldId,
            'docTypeId': DocTypesId,
            'updateTargetId': "Content",
            'actionName': "Customers_InterfaceTCVN",
        };
        $.lawsVnAjax(url,
            'Get',
            dataGetter,
            function(resp) {
                $("#Content").html(resp);
            });
    },
    TCVNResetField: function () {
        $('.fieldIdTCVN').removeClass('active');
        $("#ddlFieldTCVN")
            .removeAttr('selected')
            .find(':first') //find('[value=0]')
            .attr('selected', 'selected');
    },
    TCVNResetTitle: function (id) {
        var title = "Tiêu chuẩn Việt Nam";
        var DocTypes = 61;
        switch(id)
        {
            case 0: title = "Tiêu chuẩn Việt Nam";DocTypes = 61; break;
            case 1: title = "Quy chuẩn Việt Nam"; DocTypes = 62; break;
            case 2: title = "Tiêu chuẩn ngành"; DocTypes = 63; break;
            case 3: title = "Tiêu chuẩn XDVN"; DocTypes = 64; break;
        }
        $("#tabcontentDoctype").html(title);
        $("#tabcontentDoctype").attr("data-doctype", DocTypes);
    },
    CustomerInterFaceTCVNCChangeDoctype: function(DocTypesId, fieldId) {
        $("#tabcontentDoctype").attr("data-doctype", DocTypesId);
        lawsVn.CustomerInterFaceTCVNC(fieldId, DocTypesId);
    },
    CustomerInterFaceTCVNChangeField: function(fieldId) {
        var DocTypesId = $("#tabcontentDoctype").attr('data-doctype');
        lawsVn.CustomerInterFaceTCVNC(fieldId, DocTypesId);
    },
    CustomerInterFaceTCVNFieldFilter: function() {
        var fieldId = $("#dllField option:selected").val();
        var DocTypesId = $("#tabcontentDoctype").attr('data-doctype');
        if (fieldId == null) {
            fieldId = 0;
        }
        if (DocTypesId == null) {
            DocTypesId = 0;
        }
        lawsVn.CustomerInterFaceTCVNC(fieldId, DocTypesId);
    },
    CustomerProvincesDelete: function(CustomerProvinceId) {
        var url = lawsVnConfig.rootPath + 'Ajax/DeleteCustomerProvinces';
        var dataGetter = {
            'CustomerProvinceId': CustomerProvinceId,
        };
        $.lawsVnAjax(url,
            'Get',
            dataGetter,
            function(resp) {
                if (resp > 0) {
                    alert("Xóa Tỉnh/TP thành công!");
                    $("#CustomerProvinces" + CustomerProvinceId).remove();
                } else {
                    alert("Xóa Tỉnh/TP lỗi. Xin vui lòng thử lại.");
                }
            });
    },
    AddCustomerProvince: function(CustomerId) {

        var url = lawsVnConfig.rootPath + 'Ajax/AddCustomerProvinces';
        var Province = $("#dllProvince option:selected");
        var ProvinceId = Province.val();
        var provinceName = Province.attr('data-title');
        if (ProvinceId > 0) {
            var dataGetter = {
                'CustomerId': CustomerId,
                'ProvinceId': ProvinceId,
            };
            $.lawsVnAjax(url,
                'Get',
                dataGetter,
                function(resp) {
                    if (resp > 0) {
                        alert("Thêm Tỉnh/TP thành công!");
                        var addhtml = "<div class='item-vbdp' id='CustomerProvinces" +
                            resp +
                            "'>" +
                            provinceName +
                            "<a href='javascript:lawsVn.CustomerProvincesDelete(\"" +
                            resp +
                            "\")' class='icon-my'><img alt='avata-canhan' src='" +
                            lawsVnConfig.rootPath +
                            "assets/images/xoa2.png')\"/></a>" +
                            "</div>";
                        $("#ListProvinces").html($("#ListProvinces").html() + addhtml);
                    } else {
                        alert("Thêm Tỉnh/TP lỗi. Xin vui lòng thử lại.");
                    }
                });
        } else {
            alert("Xin vui lòng chọn Tỉnh/TP!");
        }
    },
    ListByFieldFilter: function(fieldId) {
        var selectFieldId = $("#dllField option:selected").val();
        var effectStatusId = $("#dllEffectStatus option:selected").val();
        var effectStatusName = $('input[name="effectStatusName"]').val();
        var organsId = $("#dllOrgans option:selected").val();
        var docTypesId = $("#dllDocTypes option:selected").val();
        if (selectFieldId > 0) {
            fieldId = selectFieldId;
        }
        $.lawsAjax({
            url: lawsVnConfig.rootPath + 'Ajax/Docs_GetViewSearch',
            dataType: 'html',
            data: {
                fieldId: fieldId,
                effectStatusId: effectStatusId,
                effectStatusName: effectStatusName,
                organId: organsId,
                docTypeId: docTypesId
            },
            success: function(resp) {
                $("#ListByField").html(resp);
                var totalpage = $("#tblcontent").attr('data-totalpage');
                var pageindex = $("#tblcontent").attr('data-pageindex');
                var pageCount = $("#tblcontent").attr('data-pagecount');
                if (totalpage === undefined) {
                    totalpage = 0;
                }
                if (pageindex === undefined) {
                    pageindex = 0;
                }
                if (pageindex <= 0) {
                    pageindex = 1;
                }
                var data = '';
                if (pageCount > 0) {
                    data += "<span>Tìm thấy <strong> " +
                        lawsVn.formatNumber(totalpage, '.', '.') +
                        " văn bản </strong>(" +
                        pageindex +
                        "/" +
                        pageCount +
                        " trang)";
                } else data = "<span>Không tìm thấy kết quả.</strong>";
                data += "</span>";
                $("#txtnumberresultfound").html(data);
            }
        });
    },
    DropdownlistDefaulValue: function() {
        $('#ddlFieldId').lawsExists(function() {
            $('#ddlFieldId option')
                .removeAttr('selected')
                .filter('[value=0]')
                .attr('selected', true);
        });

        $('#ddlEffectStatusId').lawsExists(function() {
            $('#ddlEffectStatusId option')
                .removeAttr('selected')
                .filter('[value=0]')
                .attr('selected', true);
        });

        $('#ddlDocTypeId').lawsExists(function() {
            $('#ddlDocTypeId option')
                .removeAttr('selected')
                .filter('[value=0]')
                .attr('selected', true);
        });

        $('#ddlOrganId').lawsExists(function() {
            $('#ddlOrganId option')
                .removeAttr('selected')
                .filter('[value=0]')
                .attr('selected', true);
        });
    },
    ListByFieldUpdate: function(fieldId) {
        $('#ddlFieldId option')
            .removeAttr('selected')
            .filter('[value=' + fieldId + ']')
            .attr('selected', true);
        $('#ddlEffectStatusId option')
            .removeAttr('selected')
            .filter('[value=0]')
            .attr('selected', true);
        $('#ddlDocTypeId option')
            .removeAttr('selected')
            .filter('[value=0]')
            .attr('selected', true);
        $('#ddlOrganId option')
            .removeAttr('selected')
            .filter('[value=0]')
            .attr('selected', true);
    },
    updateEffectStatusName: function(effectStatusName) {
        $('input[name="effectStatusName"]').lawsExists(function() {
            $(this).val(effectStatusName);
        });
    },
    selectBankCode: function (BankCode, obj) {
        $("div.gallery a").find("img").removeClass("bankselected");
        $(obj).find("img").addClass("bankselected");
        $('input[id="hdfBankCode"]').lawsExists(function () {
            $(this).val(BankCode);
        });
        $('#bankSelectedName').lawsExists(function () {
            $(this).html($(obj).attr('title'));
        });
       
    },
    onbirthdaychange: function () {
        var day = $("#dlldaybirthday option:selected").val();
        var month = $("#dllmonthbirthday option:selected").val();
        var year = $("#dllyearbirthday option:selected").val();
        var DateOfBirth = $("#DateOfBirth");
        var dlldaybirthday = $("#dlldaybirthday");
        var sodu = year % 4;

        if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12) {
            dlldaybirthday.html('');
            for (var i = 1; i < 32; i++) {
                dlldaybirthday.append($('<option ' + (i == day ? 'selected' : '') + '></option>').val(i).html(i));
            }
        }

        if ((month == 2 && sodu == 0 && day > 29) || (month == 2 && sodu != 0 && day > 28))
        {
            dlldaybirthday.html('');
            for(var i=1;i<29;i++)
            {
                dlldaybirthday.append($('<option></option>').val(i).html(i));
            }
            if(sodu == 0)
            {
                dlldaybirthday.append($('<option></option>').val(29).html(29));
            }
        }
        else if (day == 31 && (month == 4 || month == 6 || month == 9 || month == 11))
        {
            dlldaybirthday.html('');
            for (var i = 1; i < 31; i++) {
                dlldaybirthday.append($('<option></option>').val(i).html(i));
            }
        }
        else {
            $("#DateOfBirth").val((day < 10 ? '0' : '') + day + "/" + (month < 10 ? '0' : '') + month + "/" + year);
        }
    },
    goBack: function () {
        window.history.back();
    },
    FontZoom: function (zoomValue) {
        var curSize = parseInt($('div.doc-content, .doc-content-new, .entry').css('font-size')) + zoomValue;
        var curLineHeight = parseInt($('div.doc-content, .doc-content-new, .entry span').css('line-height'));
        if (!$.isNumeric(curLineHeight)) {
            curLineHeight = 20;
        }
        curLineHeight += zoomValue;
        if (curSize > 32)
            curSize = 32;
        if (curSize < 10)
            curSize = 10;
        if (curLineHeight > 32)
            curLineHeight = 32;
        if (curLineHeight < 10)
            curLineHeight = 10;
        $('div.doc-content *, .doc-content-new *, .entry *').attr('style',
            function () { return 'font-size: ' + curSize.toString() + 'px' + ' !important;' + '; line-height: ' + curLineHeight.toString() + 'px' + ' !important;' });
    },
    SaveDoc: function(docId) {
        $.lawsAjax({
            url: lawsVnConfig.rootPath + 'Ajax/SaveDocument',
            data: { docId: docId },
            beforeSend: {},
            success: function(resp) {
                if (resp.Message !== void 0 && resp.Message.length > 0) {
                    $().lawsDialog({
                        dialogClass: 'lawsVnDialogTitle',
                        messages: [resp.Message],
                        showIcon: false
                    });
                }
            }
        });
    },
    editCustomerFieldsV2: function () {
        $('<div id="editCustomerFieldsV2"></div>').lawsDialog({
            title: 'Lĩnh vực văn bản quan tâm',
            width: 800,
            position: { my: "center", at: "top+10", of: window.top },
            dialogClass: 'lawsVnDialogTitle',
            hideClose: true,
            buttons: {},
            onCreate: function () {
                $('#loading').fadeIn('normal');
            },
            onOpen: function () {
                $('#editCustomerFieldsV2').load(lawsVn.virtualPath('/Ajax/EditCustomerFieldsV2'),
                    function () {
                        //$(".content-scroll-1").mCustomScrollbar({
                        //    snapAmount: 40,
                        //    scrollButtons: { enable: true },
                        //    keyboard: { scrollAmount: 40 },
                        //    mouseWheel: { deltaFactor: 40 },
                        //    scrollInertia: 400
                        //});
                        lawsValidate.multiCheckboxRequired();
                        var form = $('#EditCustomerFieldsV2');
                        form.removeData('validator');
                        form.removeData('unobtrusiveValidation');
                        $.validator.unobtrusive.parse(form);
                    });
                $('#loading').fadeOut('normal');
            }
        });
    },
    editCustomerFields: function() {
        $('<div id="editCustomerFields"></div>').lawsDialog({
            title: 'Lĩnh vực văn bản quan tâm',
            width: 800,
            dialogClass: 'lawsVnDialogTitle',
            hideClose: true,
            buttons: {},
            onCreate: function() {
                $('#loading').fadeIn('normal');
            },
            onOpen: function() {
                $('#editCustomerFields').load(lawsVnConfig.rootPath + 'Ajax/EditCustomerFields',
                    function() {
                        $(".content-scroll-1").mCustomScrollbar({
                            snapAmount: 40,
                            scrollButtons: { enable: true },
                            keyboard: { scrollAmount: 40 },
                            mouseWheel: { deltaFactor: 40 },
                            scrollInertia: 400
                        });
                    });
                $('#loading').fadeOut('normal');
            }
        });
    },
    editCustomerProvince: function() {
        $('<div id="editCustomerProvinces"></div>').lawsDialog({
            title: 'Tỉnh/Tp quan tâm:',
            width: 800,
            dialogClass: 'lawsVnDialogTitle',
            hideClose: true,
            buttons: {},
            onCreate: function() {
                $('#loading').fadeIn('normal');
            },
            onOpen: function() {
                $('#editCustomerProvinces').load(lawsVnConfig.rootPath + 'Ajax/EditCustomerProvices',
                    function() {
                        //$(".content-scroll-1").mCustomScrollbar({
                        //    snapAmount: 40,
                        //    scrollButtons: { enable: true },
                        //    keyboard: { scrollAmount: 40 },
                        //    mouseWheel: { deltaFactor: 40 },
                        //    scrollInertia: 400
                        //});
                    });
                $('#loading').fadeOut('normal');
            }
        });
    },

    login: function() {
        $('<div id="lawsVnLogin"></div>').lawsDialog({
            title: '',
            width: 300,
            buttons: {},
            hideClose: true,
            closeText: 'Đóng',
            onCreate: function() {
                $('#loading').fadeIn('normal');
            },
            onOpen: function() {
                $('#lawsVnLogin').load(lawsVnConfig.rootPath + 'Ajax/PartialLogin',
                    function() {
                        var form = $('#lawsVnLoginForm');
                        $('#ReturnUrl').val(lawsVnConfig.returnUrl);
                        form.removeData('validator');
                        form.removeData('unobtrusiveValidation');
                        $.validator.unobtrusive.parse(form);
                    });
                $('#loading').fadeOut('normal');
            }
        });
    },
    changePassword: function() {
        $('<div id="changePassword"></div>').lawsDialog({
            title: '',
            width: 300,
            buttons: {},
            hideClose: false,
            closeText: 'Đóng',
            onCreate: function() {
                $('#loading').fadeIn('normal');
            },
            onOpen: function() {
                $('#changePassword').load(lawsVnConfig.rootPath + 'Ajax/PartialChangePassword',
                    function() {
                        var form = $('#lawsVnChangePasswordForm');
                        form.removeData('validator');
                        form.removeData('unobtrusiveValidation');
                        $.validator.unobtrusive.parse(form);
                    });
                $('#loading').fadeOut('normal');
            }
        });
    },
    forgotPassword: function() {
        $('<div id="lawForgotPassword"></div>').lawsDialog({
            title: '',
            width: 400,
            buttons: {},
            hideClose: false,
            closeText: 'Đóng',
            onCreate: function () {
                $('#loading').fadeIn('normal');
            },
            onOpen: function () {
                $('#lawForgotPassword').load(lawsVnConfig.rootPath + 'Ajax/PartialForgotPassword',
                    function () {
                        var form = $('#ForgotPasswordForm');
                        form.removeData('validator');
                        form.removeData('unobtrusiveValidation');
                        $.validator.unobtrusive.parse(form);
                    });
                $('#loading').fadeOut('normal');
            }
        });
    },
    forgotPasswordMobile: function () {
        $('<div id="lawForgotPassword"></div>').lawsDialog({
            title: '',
            width: 300,
            left:10,
            buttons: {},
            hideClose: true,
            closeText: 'Đóng',
            onCreate: function () {
                $('#loading').fadeIn('normal');
            },
            onOpen: function () {
                $('#lawForgotPassword').load(lawsVnConfig.rootPath + 'Ajax/PartialForgotPassword',
                    function () {
                        var form = $('#ForgotPasswordForm');
                        form.removeData('validator');
                        form.removeData('unobtrusiveValidation');
                        $.validator.unobtrusive.parse(form);
                    });
                $('#loading').fadeOut('normal');
            }
        });
    },
    sendQuestion:function() {
        $('<div id="lawSendQuestions"></div>').lawsDialog({
            title: '',
            width: 800,
            buttons: {},
            hideClose: false,
            closeText: 'Đóng',
            onCreate: function () {
                $('#loading').fadeIn('normal');
            },
            onOpen: function () {
                $('#lawSendQuestions').load(lawsVnConfig.rootPath + 'Ajax/PartialSendQuestions',
                    function () {
                        var form = $('#SendQuestionForm');
                        form.removeData('validator');
                        form.removeData('unobtrusiveValidation');
                        $.validator.unobtrusive.parse(form);
                    });
                $('#loading').fadeOut('normal');
            }
        });
    },
    sendQuestionMobile: function () {
        $('<div id="lawSendQuestions"></div>').lawsDialog({
            title: '',
            width: 304,
            buttons: {},
            hideClose: false,
            closeText: 'Đóng',
            onCreate: function () {
                $('#loading').fadeIn('normal');
            },
            onOpen: function () {
                $('#lawSendQuestions').load(lawsVnConfig.rootPath + 'Ajax/PartialSendQuestions',
                    function () {
                        var form = $('#SendQuestionForm');
                        form.removeData('validator');
                        form.removeData('unobtrusiveValidation');
                        $.validator.unobtrusive.parse(form);
                    });
                $('#loading').fadeOut('normal');
            }
        });
    },
    gopy: function (docName) {
        $('<div id="gopy"></div>').lawsDialog({
            title: '',
            width: 600,
            buttons: {},
            hideClose: false,
            closeText: 'Đóng',
            onCreate: function () {
                $('#loading').fadeIn('normal');
            },
            onOpen: function () {
                $('#gopy').load(lawsVnConfig.rootPath + 'Ajax/PartialGopY',
                    function () {
                        $('#FormGopY #Title').lawsExists(function() {
                            $(this).val(docName);
                        });
                        $('#FormGopY #TitleText').lawsExists(function () {
                            $(this).text(docName);
                        });
                        var form = $('#FormGopY');
                        form.removeData('validator');
                        form.removeData('unobtrusiveValidation');
                        $.validator.unobtrusive.parse(form);
                    });
                $('#loading').fadeOut('normal');
            }
        });
    },
    docSendMail: function (docUrl) {
        $('<div id="docSendMail"></div>').lawsDialog({
            title: '',
            width: 290,
            buttons: {},
            hideClose: false,
            closeText: 'Đóng',
            onCreate: function () {
                $('#loading').fadeIn('normal');
                    var elem = $('.content-thongbao');
                    elem.removeAttr('width');
                    elem.css('width', '100%');
            },
            onOpen: function () {
                $('#docSendMail').load(lawsVn.virtualPath('/Ajax/PartialDocSendMail'),
                    function () {
                        $('#DocSendMailForm #DocUrl').val(docUrl);
                        var form = $('#DocSendMailForm');
                        form.removeData('validator');
                        form.removeData('unobtrusiveValidation');
                        $.validator.unobtrusive.parse(form);
                    });
                $('#loading').fadeOut('normal');
            }
        });
    },
    logOut: function() {
        $().lawsDialog({
            dialogClass: 'lawsVnDialogTitle',
            messages: ['Quý khách muốn đăng xuất khỏi hệ thống?'],
            buttons: [
                {
                    text: 'Hủy',
                    'class': 'btn-thongbao1',
                    click: function() {
                        $(this).dialog('close');
                    }
                },
                {
                    text: 'Đồng ý',
                    'class': 'btn-thongbao31',
                    click: function() {
                        $(this).dialog('close');
                        window.location.href = lawsVn.virtualPath('/user/dang-xuat-tai-khoan.html');
                    }
                }
            ]
        });
    },
    promotionCodeCheckForm:function(a) {
        $('#promotionCodeCheckForm').lawsExists(function() {
            if (a === 1) {
                $(this).show();
            }else if (a === 2) {
                $(this).hide();
            }
        });
    },
    SearchFilter: function() {
        var keyword = $("#divcontent").attr('data-keyword');
        var SearchOptions = $("#divcontent").attr('data-searchoptions');
        var docgroupId = $("#divcontent").attr('data-docgroupid');
        var datefrom = $("#divcontent").attr('data-datefrom');
        var dateto = $("#divcontent").attr('data-dateto');
        var OrganId = $("#divcontent").attr('data-organid');
        var LanguageId = $("#divcontent").attr('data-languageid');
        var SignerName = $("#divcontent").attr('data-signername');
        var doctypeId = $("#dllDocTypes option:selected").val();
        var EffectStatusId = $("#dllEffectStatus option:selected").val();
        var FieldId = $("#dllField option:selected").val();

        lawsVn.SearchSelect(keyword,
            SearchOptions,
            docgroupId,
            datefrom,
            dateto,
            OrganId,
            LanguageId,
            SignerName,
            doctypeId,
            EffectStatusId,
            FieldId);
    },
    
    SearchSelect: function(keyword,
        SearchOptions,
        docgroupId,
        datefrom,
        dateto,
        OrganId,
        LanguageId,
        SignerName,
        doctypeId,
        EffectStatusId,
        FieldId) {
        var url = lawsVnConfig.rootPath + 'Ajax/Search_GetViewSearch';
        if (keyword == null) {
            keyword = "";
        }
        if (docgroupId == null) {
            docgroupId = 0;
        }
        if (SearchOptions == null) {
            SearchOptions = 0;
        }
        if (doctypeId == null) {
            doctypeId = 0;
        }
        if (OrganId == null) {
            OrganId = 0;
        }
        if (EffectStatusId == null) {
            EffectStatusId = 0;
        }
        if (FieldId == null) {
            FieldId = 0;
        }
        if (LanguageId == null) {
            LanguageId = 0;
        }
        if (SignerName == null) {
            OrganId = "";
        }
        if (datefrom == null) {
            datefrom = "";
        }
        if (dateto == null) {
            dateto = "";
        }
        var OrderBy = $("#dllOrderByClauses option:selected").val();
        if (OrderBy == null) {
            OrderBy = "";
        }
        var dataGetter = {
            'keyword': keyword, //encodeURIComponent(keyword),
            'DocGroupId': docgroupId,
            'SearchOptions': SearchOptions,
            'DateFrom': datefrom,
            'DateTo': dateto,
            'languageid': LanguageId,
            'SignerName': SignerName,
            'fieldId': FieldId,
            'effectStatusId': EffectStatusId,
            'organId': OrganId,
            'docTypeId': doctypeId,
            'OrderBy': OrderBy
        };
        $.lawsVnAjax(url,
            'Get',
            dataGetter,
            function(resp) {
                $("#ListDocsViews").html(resp);
                var totalpage = $("#tblcontent").attr('data-totalpage');
                var pageindex = $("#tblcontent").attr('data-pageindex');
                var pageCount = $("#tblcontent").attr('data-pagecount');
                var pagesize = $("#tblcontent").attr('data-pagesize');
                if (typeof totalpage == 'undefined') {
                    totalpage = 0;
                }
                if (typeof pageindex == 'undefined') {
                    pageindex = 0;
                }
                if (pageindex <= 0) {
                    pageindex = 1;
                }
                if (totalpage > 0) {
                    var data;
                    if (keywords.length > 0) {
                        data = " <strong>Tìm thấy <span class='color2'>" +
                            lawsVn.formatNumber(totalpage, '.', '.') +
                            "</span> văn bản với từ khóa \"<span class='color2'>" +
                            keywords +
                            "</span>\"</strong>";
                        $('#txtnumberresultfound').html(data);
                    } else {
                        data = " <strong>Tìm thấy <span class='color2'>" +
                            lawsVn.formatNumber(totalpage, '.', '.') +
                            "</span> văn bản.</strong>";
                        $('#txtnumberresultfound').html(data);
                    }
                    var datanumerpage = "<strong>Kết quả 1-" + pagesize + " trong " + lawsVn.formatNumber(totalpage, '.', '.') + " văn bản </strong>";
                    $("#txtnumberpage").html(datanumerpage);
                }


                //var data = " <strong>Tìm thấy <span class='color2'>" +
                //    lawsVn.formatNumber(totalpage, '.', '.') +
                //    "</span> văn bản với từ khóa \"<span class='color2'>" +
                //    keyword +
                //    "</span>\"</strong>";
                //if (pageCount >= 0) {
                //    data += "(" + pageindex + "/" + pageCount + " trang)";
                //}
                //var datanumerpage = "<strong>Kết quả 1-" + pagesize + " trong " + lawsVn.formatNumber(totalpage, '.', '.') + " văn bản </strong>";
                //$("#txtnumberresultfound").html(data);
                //$("#txtnumberpage").html(datanumerpage);

            });
    },
    //HomeMobile
    HomeChangeTile: function (id, title, url) {
        $("#" + id).attr("href", url);
        $("#" + id).html("<h2 class='title-cat'>" + title + "</h2>");
    },
    SearchChangeNumberFind: function () {
        var keywords = '';
        var k = $("#hiddensearch").attr('data-keywords');
        var number = $("#hiddensearch").attr('data-rowcount');
        if (typeof number == 'undefined') {
            number = 0;
        }
        if (typeof k == 'undefined') {
            k = '';
        } keywords = k;
        var content = '';
        if (number > 0) {
            if (keywords.length)
                content = "Tìm thấy <strong class='color1'>" +
                    lawsVn.formatNumber(number, '.', '.') +
                    "</strong> văn bản với từ khóa \"<strong class='color1'>" +
                    keywords +
                    "\"</strong>";
            else {
                content = "Tìm thấy <strong class='color1'>" + lawsVn.formatNumber(number, '.', '.') + "</strong> văn bản";
            }
        }
        $("#numberresultfound").html(content);
    },
    
    //End HomeMobile
    accountProfile: {
        deleteOneFieldById: function(customerFieldId) {
            $().lawsDialog({
                dialogClass: 'lawsVnDialogTitle',
                width:360,
                messages: ['Xác nhận xóa lĩnh vực quan tâm ?'],
                onCreate: function () {
                    var elem = $('.content-thongbao');
                    elem.removeAttr('width');
                    elem.css('width', '91.7%');
                },
                buttons: [
                    {
                        text: 'Đóng',
                        'class': 'btn-thongbao12',
                        click: function() {
                            $(this).dialog('close');
                        }
                    },
                    {
                        text: 'Đồng ý',
                        'class': 'btn-thongbao31',
                        click: function() {
                            $(this).dialog('close');
                            $.lawsAjax({
                                url: lawsVnConfig.rootPath + 'Ajax/DeleteOneFieldById',
                                data: { customerFieldId: customerFieldId },
                                success: function (resp) {
                                    if (resp.Completed) {
                                        if (resp.ReturnUrl !== void 0 && resp.ReturnUrl.length > 0)
                                            setTimeout(function () {
                                                    window.location.href = resp.ReturnUrl;
                                                    location.reload();
                                                },
                                                100);
                                        else window.location.href = lawsVnConfig.rootPath;
                                    }
                                    //if (resp.Message !== void 0 && resp.Message.length > 0) {
                                    //    $().lawsDialog({
                                    //        messages: [resp.Message],
                                    //        showIcon: false,
                                    //        onClose: function() {
                                    //            if (resp.Completed) {
                                    //                if (resp.ReturnUrl !== void 0 && resp.ReturnUrl.length > 0)
                                    //                    setTimeout(function() {
                                    //                            window.location.href = resp.ReturnUrl;
                                    //                            location.reload();
                                    //                        },
                                    //                        100);
                                    //                else window.location.href = lawsVnConfig.rootPath;
                                    //            }
                                    //        }
                                    //    });
                                    //}
                                }
                            });
                        }
                    }
                ]
            });
        }
    },
    account: {
        registerFreeService: function (serviceId) {
            $().lawsDialog({
                dialogClass: 'lawsVnDialogTitle',
                messages: ['Xác nhận đăng ký gói dịch vụ miễn phí ?'],
                buttons: [
                    {
                        text: 'Đóng',
                        'class': 'btn-thongbao1',
                        click: function () {
                            $(this).dialog('close');
                        }
                    },
                    {
                        text: 'Đồng ý',
                        'class': 'btn-thongbao31',
                        click: function () {
                            $(this).dialog('close');
                            $.lawsAjax({
                                url: lawsVnConfig.rootPath + 'Ajax/RegisterFreeService',
                                type: 'Post',
                                data: { serviceId: serviceId },
                                success: function (resp) {
                                    if (resp.Completed) {
                                        if (resp.Completed && resp.ReturnUrl !== void 0 && resp.ReturnUrl.length > 0) {
                                            window.location.href = resp.ReturnUrl;
                                        }
                                    } else if (resp.Message !== void 0 && resp.Message.length > 0) {
                                        $().lawsDialog({
                                            dialogClass: 'lawsVnDialogTitle',
                                            messages: [resp.Message],
                                            showIcon: false
                                        });
                                    } 
                                }
                            });
                        }
                    }
                ]
            });
        },
        registerTrialService: function (serviceId) {
            $().lawsDialog({
                dialogClass: 'lawsVnDialogTitle',
                messages: ['Xác nhận đăng ký gói dịch vụ dùng thử ?'],
                buttons: [
                    {
                        text: 'Đóng',
                        'class': 'btn-thongbao1',
                        click: function () {
                            $(this).dialog('close');
                        }
                    },
                    {
                        text: 'Đồng ý',
                        'class': 'btn-thongbao31',
                        click: function () {
                            $(this).dialog('close');
                            $.lawsAjax({
                                url: lawsVnConfig.rootPath + 'Ajax/RegisterTrialService',
                                type: 'Post',
                                data: { serviceId: serviceId },
                                success: function (resp) {
                                    if (resp.Message !== void 0 && resp.Message.length > 0) {
                                        $().lawsDialog({
                                            messages: [resp.Message],
                                            showIcon: false,
                                            onClose: function () {
                                                if (resp.Completed && resp.ReturnUrl !== void 0 && resp.ReturnUrl.length > 0) {
                                                    window.location.href = resp.ReturnUrl;
                                                }
                                            }
                                        });
                                    }
                                }
                            });
                        }
                    }
                ]
            });
        }
    },
    myMesssages: {
        deleteMessage: function (messageId) {
            $().lawsDialog({
                dialogClass: 'lawsVnDialogTitle',
                messages: ['Xác nhận xóa tin nhắn ?'],
                buttons: [
                    {
                        text: 'Đóng',
                        'class': 'btn-thongbao1',
                        click: function () {
                            $(this).dialog('close');
                        }
                    },
                    {
                        text: 'Đồng ý',
                        click: function () {
                            $(this).dialog('close');
                            $.lawsAjax({
                                url: lawsVn.virtualPath('/Ajax/MessageDelete'),
                                type: 'Post',
                                traditional: true,
                                data: { messageId: messageId },
                                success: function (resp) {
                                    if (resp.Message != null && resp.Message.length > 0) {
                                        $().lawsDialog({
                                            dialogClass: 'lawsVnDialogTitle',
                                            messages: [resp.Message],
                                            showIcon: false,
                                            onClose: function () {
                                                if (resp.Completed && resp.ReturnUrl != null && resp.ReturnUrl.length > 0) {
                                                    window.location.href = resp.ReturnUrl;
                                                }
                                            }
                                        });
                                    }
                                }
                            });
                        }
                    }
                ]
            });
        },
        deleteMessages: function (actionTypeId) {
            var messageId = $('.checkbox-mail input[type=checkbox]:checked').map(function (_, el) {
                return $(el).val();
            }).get();
            if (!messageId.length) {
                $().lawsDialog({
                    dialogClass: 'lawsVnDialogTitle',
                    messages: ['Quý khách chưa chọn tin nhắn cần xóa.'],
                    showIcon: false
                });
                return;
            }
            $().lawsDialog({
                dialogClass: 'lawsVnDialogTitle',
                messages: ['Xác nhận xóa tin nhắn ?'],
                buttons: [
                    {
                        text: 'Đóng',
                        'class': 'btn-thongbao1',
                        click: function () {
                            $(this).dialog('close');
                        }
                    },
                    {
                        text: 'Đồng ý',
                        click: function () {
                            $(this).dialog('close');
                            $.lawsAjax({
                                url: lawsVn.virtualPath('/Ajax/MessageDelete'),
                                type: 'Post',
                                traditional: true,
                                data: { messageId: messageId, actionTypeId: actionTypeId },
                                success: function (resp) {
                                    if (resp.Message != null && resp.Message.length > 0) {
                                        $().lawsDialog({
                                            dialogClass: 'lawsVnDialogTitle',
                                            messages: [resp.Message],
                                            showIcon: false,
                                            onClose: function () {
                                                if (resp.Completed && resp.ReturnUrl != null && resp.ReturnUrl.length > 0) {
                                                    window.location.href = resp.ReturnUrl;
                                                }
                                            }
                                        });
                                    }
                                }
                            });
                        }
                    }
                ]
            });
        },
        saveMessage: function (messageId) {
            $().lawsDialog({
                dialogClass: 'lawsVnDialogTitle',
                messages: ['Xác nhận lưu tin nhắn ?'],
                buttons: [
                    {
                        text: 'Đóng',
                        'class': 'btn-thongbao1',
                        click: function () {
                            $(this).dialog('close');
                        }
                    },
                    {
                        text: 'Đồng ý',
                        click: function () {
                            $(this).dialog('close');
                            $.lawsAjax({
                                url: lawsVnConfig.rootPath + 'Ajax/MessageSave',
                                type: 'Post',
                                traditional: true,
                                data: { messageId: messageId },
                                success: function (resp) {
                                    if (resp.Message !== null && resp.Message.length > 0) {
                                        $().lawsDialog({
                                            dialogClass: 'lawsVnDialogTitle',
                                            messages: [resp.Message],
                                            onClose: function () {
                                                if (resp.Completed && resp.ReturnUrl != null && resp.ReturnUrl.length > 0) {
                                                    window.location.href = resp.ReturnUrl;
                                                }
                                            }
                                        });
                                    }
                                }
                            });
                        }
                    }
                ]
            });
        },
        saveMessages: function (actionTypeId) {
            var messageId = $('.checkbox-mail input[type=checkbox]:checked').map(function (_, el) {
                return $(el).val();
            }).get();
            if (!messageId.length) {
                $().lawsDialog({
                    dialogClass: 'lawsVnDialogTitle',
                    messages: ['Quý khách chưa chọn tin nhắn cần lưu.'],
                    showIcon: false
                });
                return;
            }
            $().lawsDialog({
                dialogClass: 'lawsVnDialogTitle',
                messages: ['Xác nhận lưu tin nhắn ?'],
                buttons: [
                    {
                        text: 'Đóng',
                        'class': 'btn-thongbao1',
                        click: function () {
                            $(this).dialog('close');
                        }
                    },
                    {
                        text: 'Đồng ý',
                        click: function () {
                            $(this).dialog('close');
                            $.lawsAjax({
                                url: lawsVnConfig.rootPath + 'Ajax/MessageSave',
                                type: 'Post',
                                traditional: true,
                                data: { messageId: messageId, actionTypeId: actionTypeId },
                                success: function (resp) {
                                    if (resp.Message !== void 0 && resp.Message.length > 0) {
                                        $().lawsDialog({
                                            dialogClass: 'lawsVnDialogTitle',
                                            messages: [resp.Message],
                                            showIcon: false,
                                            onClose: function () {
                                                if (resp.Completed && resp.ReturnUrl != null && resp.ReturnUrl.length > 0) {
                                                    window.location.href = resp.ReturnUrl;
                                                }
                                            }
                                        });
                                    }
                                }
                            });
                        }
                    }
                ]
            });
        },
        unsaveMessage: function (messageId) {
            $().lawsDialog({
                dialogClass: 'lawsVnDialogTitle',
                messages: ['Xác nhận bỏ lưu tin nhắn ?'],
                buttons: [
                    {
                        text: 'Đóng',
                        'class': 'btn-thongbao1',
                        click: function () {
                            $(this).dialog('close');
                        }
                    },
                    {
                        text: 'Đồng ý',
                        click: function () {
                            $(this).dialog('close');
                            $.lawsAjax({
                                url: lawsVnConfig.rootPath + 'Ajax/MessageUnSave',
                                type: 'Post',
                                traditional: true,
                                data: { messageId: messageId },
                                success: function (resp) {
                                    if (resp.Message != null && resp.Message.length > 0) {
                                        $().lawsDialog({
                                            dialogClass: 'lawsVnDialogTitle',
                                            messages: [resp.Message],
                                            showIcon: false,
                                            onClose: function () {
                                                if (resp.Completed && resp.ReturnUrl !== null && resp.ReturnUrl.length > 0) {
                                                    window.location.href = resp.ReturnUrl;
                                                }
                                            }
                                        });
                                    }
                                }
                            });
                        }
                    }
                ]
            });
        },
        unsaveMessages: function (actionTypeId) {
            var messageId = $('.checkbox-mail input[type=checkbox]:checked').map(function (_, el) {
                return $(el).val();
            }).get();
            if (!messageId.length) {
                $().lawsDialog({
                    dialogClass: 'lawsVnDialogTitle',
                    messages: ['Quý khách chưa chọn tin nhắn cần bỏ lưu.'],
                    showIcon: false
                });
                return;
            }
            $().lawsDialog({
                dialogClass: 'lawsVnDialogTitle',
                messages: ['Xác nhận bỏ lưu tin nhắn ?'],
                buttons: [
                    {
                        text: 'Đóng',
                        'class': 'btn-thongbao1',
                        click: function () {
                            $(this).dialog('close');
                        }
                    },
                    {
                        text: 'Đồng ý',
                        click: function () {
                            $(this).dialog('close');
                            $.lawsAjax({
                                url: lawsVnConfig.rootPath + 'Ajax/MessageUnSave',
                                type: 'Post',
                                traditional: true,
                                data: { messageId: messageId, actionTypeId: actionTypeId },
                                success: function (resp) {
                                    if (resp.Message != null && resp.Message.length > 0) {
                                        $().lawsDialog({
                                            dialogClass: 'lawsVnDialogTitle',
                                            messages: [resp.Message],
                                            showIcon: false,
                                            onClose: function () {
                                                if (resp.Completed && resp.ReturnUrl !== null && resp.ReturnUrl.length > 0) {
                                                    window.location.href = resp.ReturnUrl;
                                                }
                                            }
                                        });
                                    }
                                }
                            });
                        }
                    }
                ]
            });
        },
        readMessage: function (messageId) {
            $('<div id="readMessage"></div>').lawsDialog({
                title: 'Nội dung tin nhắn:',
                dialogClass: 'lawsVnDialogTitle',
                width: 600,
                minHeight: 300,
                onCreate: function () {
                    $('#loading').fadeIn('normal');
                },
                onOpen: function () {
                    $('#readMessage').load(lawsVnConfig.rootPath + 'Ajax/MessageRead?messageId=' + messageId, function () {
                        $('#message-' + messageId).addClass('fontnomal');
                        $('#message-time-' + messageId).addClass('fontnomal');
                    });
                    $('#loading').fadeOut('normal');
                }
            });
        },
        setStart: function (messageId, actionType) {
            $().lawsDialog({
                dialogClass: 'lawsVnDialogTitle',
                messages: ['Xác nhận gắn sao cho tin nhắn ?'],
                buttons: [
                    {
                        text: 'Đóng',
                        'class': 'btn-thongbao1',
                        click: function () {
                            $(this).dialog('close');
                        }
                    },
                    {
                        text: 'Đồng ý',
                        'class': 'btn-thongbao31',
                        click: function () {
                            $(this).dialog('close');
                            $.lawsAjax({
                                url: lawsVnConfig.rootPath + 'Ajax/MessagesSetStar',
                                data: { messageId: messageId, actionType: actionType },
                                success: function (resp) {
                                    if (resp.Message !== void 0 && resp.Message.length > 0) {
                                        $().lawsDialog({
                                            dialogClass: 'lawsVnDialogTitle',
                                            messages: [resp.Message],
                                            showIcon: false,
                                            onClose: function () {
                                                if (resp.Completed && resp.ReturnUrl != null && resp.ReturnUrl.length > 0) {
                                                    window.location.href = resp.ReturnUrl;
                                                }
                                            }
                                        });
                                    }
                                }
                            });
                        }
                    }
                ]
            });
        },
        unStart: function (messageId, actionType) {
            $().lawsDialog({
                dialogClass: 'lawsVnDialogTitle',
                messages: ['Xác nhận bỏ gắn sao cho tin nhắn ?'],
                buttons: [
                    {
                        text: 'Đóng',
                        'class': 'btn-thongbao1',
                        click: function () {
                            $(this).dialog('close');
                        }
                    },
                    {
                        text: 'Đồng ý',
                        'class': 'btn-thongbao31',
                        click: function () {
                            $(this).dialog('close');
                            $.lawsAjax({
                                url: lawsVn.virtualPath('/Ajax/MessagesUnStar'),
                                data: { messageId: messageId, actionType: actionType },
                                success: function (resp) {
                                    if (resp.Message !== void 0 && resp.Message.length > 0) {
                                        $().lawsDialog({
                                            dialogClass: 'lawsVnDialogTitle',
                                            messages: [resp.Message],
                                            showIcon: false,
                                            onClose: function () {
                                                if (resp.Completed && resp.ReturnUrl != null && resp.ReturnUrl.length > 0) {
                                                    window.location.href = resp.ReturnUrl;
                                                }
                                            }
                                        });
                                    }
                                }
                            });
                        }
                    }
                ]
            });
        }
    },
    myDocuments: {
        getPage: function (docGroupId, showNumberOfResults, languageId) {
            var url = lawsVnConfig.rootPath + '';
            var dataGetter = {
                docGroupId: docGroupId,
                fieldId: $('select[name="fieldId"] option:selected').val(),
                organId: $('select[name="organId"] option:selected').val(),
                effectStatusId: $('select[name="effectStatusId"] option:selected').val(),
                docTypeId: $('select[name="docTypeId"] option:selected').val(),
                showNumberOfResults: showNumberOfResults
            }
        },
        deleteCustomerDocs: function (docGroupId, docId,type) { //type = 1 : Vb quan tâm, 2: thông báo hiệu lực
            $().lawsDialog({
                dialogClass: 'lawsVnDialogTitle',
                width:360,
                messages: ['Xác nhận xóa ' + (type === 1 ? ' văn bản quan tâm' : (type === 2 ? ' thông báo hiệu lực' : '')) + ' ?'],
                onCreate: function () {
                    var elem = $('.content-thongbao');
                    elem.removeAttr('width');
                    elem.css('width', '91.7%');
                },
                buttons: [
                    {
                        text: 'Đóng',
                        'class': 'btn-thongbao1',
                        click: function () {
                            $(this).dialog('close');
                        }
                    },
                    {
                        text: 'Đồng ý',
                        'class': 'btn-thongbao31',
                        click: function () {
                            $(this).dialog('close');
                            $.lawsAjax({
                                url: lawsVn.virtualPath('/Ajax/DeleteDocument'),
                                data: { docGroupId: docGroupId, docId: docId , type: type},
                                success: function (resp) {
                                    if (resp.Completed &&
                                        resp.ReturnUrl != null &&
                                        resp.ReturnUrl.length > 0) {
                                        window.location.href = resp.ReturnUrl;
                                    }
                                }
                            });
                        }
                    }
                ]
            });
        },
        subscriptionNoticeOfValidity: function (docId) {
            $.lawsAjax({
                url: lawsVn.virtualPath('/Ajax/SubscriptionNoticeOfValidity'),
                data: { docId: docId },
                success: function (resp) {
                    if (resp.Message != null && resp.Message.length > 0) {
                        $().lawsDialog({
                            dialogClass: 'lawsVnDialogTitle',
                            messages: [resp.Message],
                            showIcon: false
                        });
                    }
                }
            });
        }
    },
    logs: {
        articleLogs: function (articleId, categoryId) {
            $.lawsAjax({
                url: lawsVn.virtualPath('/Ajax/ArticleLogs'),
                data: { articleId: articleId, categoryId: categoryId },
                beforeSend: {},
                success: function (resp) {
                    if (!resp.Completed) {
                        if (resp.Message != null && resp.Message.length > 0) {
                            console.log('ArticleLogs Error: ' + resp.Message);
                        }
                    }
                },
                error: function (resp) {
                    console.log('articleLogs Error: ' + resp.Message);
                }
            });
        },
        docViewLogs: function (docId, docGroupId, actionTypeId) {
            $.lawsAjax({
                url: lawsVn.virtualPath('/Ajax/DocViewLogs'),
                data: { docId: docId, docGroupId: docGroupId, actionTypeId: actionTypeId },
                beforeSend: {},
                success: function (resp) {
                    if (!resp.Completed) {
                        if (resp.Message != null && resp.Message.length > 0) {
                            console.log('DocViewLogs Error: ' + resp.Message);
                        }
                    }
                },
                error: function (resp) {
                    console.log('DocViewLogs Error: ' + resp.Message);
                }
            });
        },
        docSearchLogs: function (keywords, dateFrom, dateTo, docTypeId, organId, signerId, fieldId) {
            $.lawsAjax({
                url: lawsVn.virtualPath('/Ajax/DocSearchLogs'),
                data: { keywords: keywords, dateFrom: dateFrom, dateTo: dateTo, docTypeId: docTypeId, organId: organId, signerId: signerId, fieldId: fieldId },
                beforeSend: {},
                success: function (resp) {
                    if (!resp.Completed) {
                        if (resp.Message != null && resp.Message.length > 0) {
                            console.log('DocViewLogs Error: ' + resp.Message);
                        }
                    }
                },
                error: function (resp) {
                    console.log('docSearchLogs Error: ' + resp.Message);
                }
            });
        }
    },
    search: {
        getId: function (el) {
            var id = 0;
            $('.xoa.' + el).lawsExists(function () {
                var item = $('.item-sub-qktk').children('.xoa.' + el).last();
                id = item.data('id');
            });
            return id;
        },
        start: function () {
            var fieldId = lawsVnConfig.arrLinhvuctracuu.length > 0 ? lawsVnConfig.arrLinhvuctracuu[lawsVnConfig.arrLinhvuctracuu.length - 1].id : 0;
            var organId = lawsVnConfig.arrCoquanbanhanh.length > 0 ? lawsVnConfig.arrCoquanbanhanh[lawsVnConfig.arrCoquanbanhanh.length - 1].id : 0;
            var effectStatusId = lawsVnConfig.arrTrangthaihieuluc.length > 0 ? lawsVnConfig.arrTrangthaihieuluc[lawsVnConfig.arrTrangthaihieuluc.length - 1].id : 0;
            var docTypeId = lawsVnConfig.arrLoaivanban.length > 0 ? lawsVnConfig.arrLoaivanban[lawsVnConfig.arrLoaivanban.length - 1].id : 0;
            var year = lawsVnConfig.arrNambanhanh.length > 0 ? lawsVnConfig.arrNambanhanh[lawsVnConfig.arrNambanhanh.length - 1].id : 0;
            var dateFrom = $('#DateFrom').val();
            var dateTo = $('#DateTo').val();
            var keywords = $('#Keywords').val();
            var languageId = $('select[name="LanguageId"] option:selected').val();
            var signerId = $('#SignerId').val();
            var docGroupId = $('#DocGroupId').val();
            var orderBy = $('select[id="ddlOrderByClauses"] option:selected').val();
            var fieldIdSelected = $('select[id="ddlFieldId"] option:selected').val();
            var effectStatusIdSelected = $('select[id="ddlEffectStatusId"] option:selected').val();
            var docTypeIdSelected = $('select[id="ddlDocTypeId"] option:selected').val();
            if (fieldIdSelected > 0) fieldId = fieldIdSelected;
            if (effectStatusIdSelected > 0) effectStatusId = effectStatusIdSelected;
            if (docTypeIdSelected > 0) docTypeId = docTypeIdSelected;
            var isSearchExact = 0;
            if ($('#SearchExact').is(":checked")) {
                isSearchExact = 1;
            }
            var searchOptions = $("input[name=SearchOptions]:checked").val(); 

            $.lawsAjax({
                url: lawsVn.virtualPath('/Ajax/Docs_GetViewSearchWithKeyword'),
                type: 'Post',
                dataType: 'html',
                data: {
                    keywords: keywords,
                    docGroupId: docGroupId,
                    searchOptions: searchOptions,
                    isSearchExact: isSearchExact,
                    dateFrom: dateFrom,
                    dateTo: dateTo,
                    languageid: languageId,
                    signerId: signerId,
                    fieldId: fieldId,
                    effectStatusId: effectStatusId,
                    organId: organId,
                    docTypeId: docTypeId,
                    year: year,
                    orderBy: orderBy
                },
                success: function (resp) {
                    $("#ListDocsViews").html(resp).promise().done(function () {
                        lawsVn.searchOnSuccess();
                        $(this).LawScrollTo();
                    });
                }
            });

        },
        prioritizeKeyword: function () {
            var temp = { left: null, right: null };
            var temp2 = { left: null, right: null };
            var swap = true;
            var keywords = '';
            $('#Keywords').lawsExists(function () {
                keywords = $(this).val().toLowerCase();
            });
            $('.row-first-news').lawsExists(function () {
                var n = $('.row-first-news');
                var l = n.length;
                for (var i = 0; i < l && swap === true ; i++) {
                    swap = false;
                    for (var k = 0; k < l - 1; k++) {
                        var e = $(n[k]);
                        var h = $(n[k + 1]);
                        var f = e.find('.news-left-post>a');
                        var g = h.find('.news-left-post>a');
                        if (f.text().toLowerCase().indexOf(keywords) === -1 && g.text().toLowerCase().indexOf(keywords) !== -1) {
                            swap = true;
                            temp.left = f.html();
                            temp.right = e.find('.post-time-right-timkiem').html();
                            temp2.left = g.html();
                            temp2.right = h.find('.post-time-right-timkiem').html();

                            f.html(temp2.left);
                            e.find('.post-time-right-timkiem').html(temp2.right);
                            g.html(temp.left);
                            h.find('.post-time-right-timkiem').html(temp.right);
                        }
                    }
                }
            });
        }
    },
    bankSelectReset: function (a, b) {
        var parent = $(a).closest('.' + b)
        $('div.' + b).not(parent).each(function () {
            $(this).show();
        });
        $('input[id="hdfBankCode"]').lawsExists(function () {
            $(this).val('');
        });
    },
    bankSelectSet: function (a, b,c) {
        var parent = $(a).closest('.' + b)
        $('div.' + b).not(parent).each(function () {
            $(this).hide();
        });
        $('input[id="hdfBankCode"]').lawsExists(function () {
            $(this).val(c);
        });
    }
}
//obj chua cac phuong thuc validate phia client
var lawsValidate = {
    isValidDate: function (controlName, format) {
        var isValid = true;
        try {
            jQuery.datepicker.parseDate(format, jQuery('#' + controlName).val(), null);
        }
        catch (error) {
            isValid = false;
        }
        return isValid;
    },
    fileType: function () {
        jQuery.validator.unobtrusive.adapters.add('filetype', ['validtypes'], function (options) {
            options.rules['filetype'] = { validtypes: options.params.validtypes.split(',') };
            options.messages['filetype'] = options.message;
        });

        jQuery.validator.addMethod("filetype", function (value, element, param) {
            for (var i = 0; i < element.files.length; i++) {
                var extension = validateFn.getFileExtension(element.files[0].name);
                if ($.inArray(extension, param.validtypes) === -1) {
                    return false;
                }
            }
            return true;
        });
    },
    maxFileSize: function () {
        jQuery.validator.unobtrusive.adapters.add(
            'filesize', ['maxsize'], function (options) {
                options.rules['filesize'] = options.params;
                if (options.message) {
                    options.messages['filesize'] = options.message;
                }
            }
        );

        jQuery.validator.addMethod('filesize', function (value, element, params) {
            if (element.files.length < 1) {
                // ko co file nao duoc chon
                return true;
            }

            if (!element.files || !element.files[0].size) {
                // trinh duyet ko ho tro HTML5 API
                return true;
            }

            return element.files[0].size < params.maxsize;
        }, '');
    },
    age: function () {
        jQuery.validator.addMethod(
            'validateage',
            function (value, element, params) {
                return Date.parse(value) >= Date.parse(params.minumumdate) && Date.parse(value) <= Date.parse(params.maximumdate);
            });

        jQuery.validator.unobtrusive.adapters.add(
            'validateage', ['minumumdate', 'maximumdate'], function (options) {
                var params = {
                    minumumdate: options.params.minumumdate,
                    maximumdate: options.params.maximumdate
                };
                options.rules['validateage'] = params;
                options.messages['validateage'] = options.message;
            });
    },
    termsAndConditions: function () {
        var defaultRangeValidator = jQuery.validator.methods.range;
        jQuery.validator.methods.range = function (value, element, param) {
            if (element.type === 'checkbox') {
                // neu la checkbox: tra ve true neu checkbox checked
                return element.checked;
            } else {
                // neu ko thi goi ham range validator default
                return defaultRangeValidator.call(this, value, element, param);
            }
        }
    },
    multiCheckboxRequired: function () {
        jQuery.validator.unobtrusive.adapters.add("multicheckboxrequired", function (options) {
            if (options.element.tagName.toUpperCase() == "INPUT" && options.element.type.toUpperCase() == "HIDDEN") {
                options.rules["required"] = true;
                if (options.message) {
                    options.messages["required"] = options.message;
                }
            }
        });
    },
    validateFn: {
        getFileExtension: function (fileName) {
            if (/[.]/.exec(fileName)) {
                return /[^.]+$/.exec(fileName)[0].toLowerCase();
            }
            return null;
        }
    },
    formatDate: function () {
        jQuery.validator.methods.date = function (value, element) {
            if (value) {
                try {
                    $.datepicker.parseDate('dd/mm/yy', value);
                } catch (ex) {
                    return false;
                }
            }
            return true;
        };
    }
}

$.fn.lawsExists = function (callback) {
    var args = [].slice.call(arguments, 1);
    if (this.length) {
        callback.call(this, args);
    }
    return this;
}

$.extend({
    lawsVnAjax: function (url, type, dataGetter, onsuccess) {
        var execOnSuccess = $.isFunction(onsuccess) ? onsuccess : $.noop;
        var getData = $.isFunction(dataGetter) ? dataGetter : function () { return dataGetter; };
        $.ajax({
            url: url,
            type: type,
            traditional: true,
            data: getData(),
            beforeSend: function () {
                $('#loadmore').prop('disabled', true).css('cursor', 'wait').text('Đang tải dữ liệu...');
            },
            error: function (jqXhr, errorMessage) {
                if (jqXhr.status === 0) {
                    lawsVn.dialog({
                        messages: ['Không có kết nối mạng. Vui lòng kiểm tra lại.']
                    , showIcon: false
                    });
                } else if (jqXhr.status == 404) {
                    lawsVn.dialog({
                        messages: ['Không tìm thấy trang yêu cầu. [404]']
                        , showIcon: false
                    });
                } else if (jqXhr.status == 500) {
                    lawsVn.dialog({
                        messages: ['Lỗi máy chủ nội bộ. [500].']
                        , showIcon: false
                    });
                } else if (errorMessage === 'parsererror') {
                    lawsVn.dialog({
                        messages: ['Yêu cầu phân tích cú pháp JSON lỗi.']
                        , showIcon: false
                    });
                } else if (errorMessage === 'timeout') {
                    lawsVn.dialog({
                        messages: ['Hết thời gian yêu cầu.']
                        , showIcon: false
                    });
                } else if (errorMessage === 'abort') {
                    lawsVn.dialog({
                        messages: ['Yêu cầu xử lý bị hủy.']
                        , showIcon: false
                    });
                } else if (jqXhr.status != 403) {
                    lawsVn.dialog({
                        messages: ['Lỗi :.n' + jqXhr.responseText]
                        , showIcon: false
                    });
                }
                $('#loadmore').prop('disabled', true).css('cursor', 'default').text('Xem thêm');
            },
            success: function (data, status, xhr) {
                window.setTimeout(function () {
                    execOnSuccess(data);
                }, 10);
                $('#loadmore').prop('disabled', true).css('cursor', 'default').text('Xem thêm');
            }
        });
    }
});

(function ($) {
    $.fn.lawsDialog = function (options) {
        var defaultOptions = {
            title: 'Thông báo',
            width: 'auto',
            height: 'auto',
            minWidth: 'auto',
            minHeight: 'auto',
            resizable: false,
            autoOpen: true,
            modal: true,
            show: { effect: 'fade', duration: 250 },
            hide: { effect: 'fade', duration: 250 },
            closeText: "Đóng",
            position: { my: "center", at: "top+150", of: window.top },
            dialogClass: 'lawsVnDialog',
            buttons: null,
            onCreate: {},
            onOpen: {},
            onClose: {},
            hideClose: true,
            showIcon: false, //hiện icon chuông hay ko
            isDestroy: true,
            messages: []
        };
        if (typeof options == 'object') {
            options = $.extend(defaultOptions, options);
        } else {
            options = defaultOptions;
        }
        var self = this;
        var execOnClose = $.isFunction(options.onClose) ? options.onClose : $.noop;
        var execOnOpen = $.isFunction(options.onOpen) ? options.onOpen : $.noop;
        var execOnCreate = $.isFunction(options.onCreate) ? options.onCreate : $.noop;
        options.messages = $.isArray(options.messages) ? options.messages : [];

        var html = '<div class="content-thongbao">' +
                        '<div class="rows-thongbao" style=" font-size: 12px;font-weight: bold; line-height: 24px; text-align: center;">';
        if (options.showIcon) {
            html += '<img alt="img-tb" class="img-tb" src="' + lawsVnConfig.rootPath + 'assets/images/icon-tb.png">';
        }
        html += options.messages[0] +
                   '</div>';
        if (options.messages.length > 1) {
            html +=
                '<div class="rows-thongbao center" style="font-size: 13px; font-style: italic; line-height: 24px;">' +
                '<span>' + options.messages[1] + '</span> <br>';
        }
        if (options.messages.length > 2) {
            html += '<span style="color: #d81c22">' + options.messages[2] + '</span>';
        }
        html += '</div>';
        
        if (!self.length) {
            self = $(html);
        }

        self.dialog({
            title: options.title,
            width: options.width,
            height: options.height,
            minWidth: options.minWidth,
            minHeight: options.minHeight,
            resizable: options.resizable,
            autoOpen: options.autoOpen,
            modal: options.modal,
            closeText: options.closeText,
            position: options.position,
            dialogClass: options.dialogClass,
            show: options.show,
            hide: options.hide,
            buttons: options.buttons || [
                {
                    text: 'Đóng',
                    'class': 'btn-thongbao1',
                    click: function () {
                        $(self).dialog('close');
                        window.setTimeout(function () {
                            execOnClose();
                        }, 10);
                    }
                }
            ],
            create: function () {
                window.setTimeout(function () {
                    execOnCreate();
                }, 10);
            },
            open: function (event, ui) {
                $(this).closest('.ui-dialog').focus();
                $('.btn-thongbao1:eq(1)').focus();
                //ẩn nút đóng x
                if (options.hideClose) {
                    $(self).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                if (options.title === '') {
                    //$(this).siblings('.ui-dialog-titlebar').remove();
                    $(this).dialog("widget").find(".ui-dialog-title").remove();
                }
                $('.ui-widget-overlay').bind('click',
                    function() {
                        $(self).dialog('close');
                    });
                window.setTimeout(function () {
                    execOnOpen(event, ui);
                }, 10);
            },
            close: function (event, ui) {
                $(self).dialog('close');
                if (options.isDestroy) {
                    $(self).dialog('destroy').remove();
                }
                window.setTimeout(function () {
                    execOnClose(event, ui);
                }, 10);
            }
        });
        if(self.dialog('isOpen') === false) {
            self.dialog('open');
        }
    }
})(jQuery);

$.extend({
    lawsAjax: function (options) {
        var defaults =
        {
            url: '',
            type: 'Get',
            data: {},
            dataType: 'json',
            async: false,
            cache: false,
            traditional: false,
            timeout: 5000,
            beforeSend: function () {
                $('#loading').fadeIn('normal');
            },
            success: function (data, status, xhr) {
                if (data.Message != null && data.Message.length > 0) {
                    $().lawsDialog({
                        dialogClass: 'lawsVnDialogTitle',
                        messages: [data.Message],
                        showIcon: false,
                        onClose: function () {
                            if (data.ReturnUrl != null && data.ReturnUrl.length > 0) {
                                window.location.href = data.ReturnUrl;
                            }
                        }
                    });
                }
            },
            error: function (jqXhr, errorMessage) {
                $('#loading').fadeOut('normal');
                if (jqXhr.status === 0) {
                    $().lawsDialog({
                        dialogClass: 'lawsVnDialogTitle',
                        messages: ['Không có kết nối mạng. Vui lòng kiểm tra lại.']
                        , showIcon: false
                    });
                } else if (jqXhr.status === 404) {
                    $().lawsDialog({
                        dialogClass: 'lawsVnDialogTitle',
                        messages: ['Không tìm thấy trang yêu cầu. [404]']
                        , showIcon: false
                    });
                } else if (jqXhr.status === 500) {
                    $().lawsDialog({
                        dialogClass: 'lawsVnDialogTitle',
                        messages: ['Lỗi máy chủ nội bộ. [500].']
                        , showIcon: false
                    });
                } else if (errorMessage === 'parsererror') {
                    $().lawsDialog({
                        dialogClass: 'lawsVnDialogTitle',
                        messages: ['Yêu cầu phân tích cú pháp JSON lỗi.']
                        , showIcon: false
                    });
                } else if (errorMessage === 'timeout') {
                    $().lawsDialog({
                        dialogClass: 'lawsVnDialogTitle',
                        messages: ['Hết thời gian yêu cầu.']
                        , showIcon: false
                    });
                } else if (errorMessage === 'abort') {
                    $().lawsDialog({
                        dialogClass: 'lawsVnDialogTitle',
                        messages: ['Yêu cầu xử lý bị hủy.']
                        , showIcon: false
                    });
                } else if (jqXhr.status !== 403) {
                    $().lawsDialog({
                        dialogClass: 'lawsVnDialogTitle',
                        messages: ['Lỗi :.n' + jqXhr.responseText]
                        , showIcon: false
                    });
                }
            },
            always: function () {
                $('#loading').fadeOut('normal');
            }
        }
        options = $.extend(defaults, options);
        if (options.url.length) {
            $.ajax({
                url: options.url,
                type: options.type,
                data: options.data,
                dataType: options.dataType,
                async: options.async,
                cache: options.cache,
                traditional: options.traditional,
                timeout: options.timeout,
                beforeSend: function () {
                    if ($.isFunction(options.beforeSend)) {
                        window.setTimeout(function () {
                            options.beforeSend();
                        }, 10);
                    }
                },
                success: function (data, status, xhr) {
                    if ($.isFunction(options.success)) {
                        window.setTimeout(function () {
                            options.success(data, status, xhr);
                        }, 10);
                    }
                },
                error: function (jqXhr, errorMessage) {
                    window.setTimeout(function () {
                        options.error(jqXhr, errorMessage);
                    }, 10);
                }
            }).always(function () {
                window.setTimeout(function () {
                    options.always();
                }, 10);
            });
        }
    }
});

(function ($) {
    $.fn.lawsVnContentViewed = function (options) {
        var defaultOptions = {
            limit: 10,
            image: lawsVn.virtualPath('/assets/images/van-ban.png'),
            doc: {}
        }
        options = $.extend(defaultOptions, options);
        var proc = {
            getAllDocs: function () {
                try {
                    var lawsVnContentViewed =
                        localStorage.lawsVnContentViewed ? localStorage.lawsVnContentViewed : '[]';
                    var doc = JSON.parse(lawsVnContentViewed);
                    return doc;
                } catch (e) {
                    return [];
                }
            },
            setAllDocs: function (docs) {
                localStorage.lawsVnContentViewed = JSON.stringify(docs);
            },
            setDoc: function () {
                if (options.doc.id > 0) {
                    var index = proc.getIndexDoc(options.doc.id);
                    if (index < 0) {
                        proc.addDoc();
                    }
                }
            },
            getIndexDoc: function (id) {
                var index = -1;
                var docs = proc.getAllDocs();
                $.each(docs, function (i, value) {
                    if (value.id === id) {
                        index = i;
                        return;
                    }
                });
                return index;
            },
            removeDoc: function (id) {
                var docs = proc.getAllDocs();
                //lọc các văn bản có id khác id vb muốn xóa
                docs = $.grep(docs,
                    function (value) {
                        return value.id !== id;
                    });
                proc.setAllDocs(docs);
            },
            addDoc: function () {
                var docs = proc.getAllDocs();
                docs.push({
                    id: options.doc.id,
                    name: options.doc.name,
                    effect: options.doc.effect,
                    title: options.doc.title,
                    summary: options.doc.summary,
                    datetime: options.doc.datetime,
                    url: options.doc.url,
                    image: options.doc.image.length > 0 ? options.doc.image : options.image
                });
                proc.setAllDocs(docs);
                if (docs.length > options.limit) {
                    proc.removeDoc(docs[0].id);
                }
            },
            resetAllDocs: function () {
                proc.setAllDocs([]);
            },
            getTotalDocs: function () {
                var docs = proc.getAllDocs();
                return docs.length;
            },
            getContent: function (el) {
                var docs = proc.getAllDocs();
                var html = '';
                var j = 1;
                for (var i = docs.length - 1; i >= 0; i--) {
                    if (j > options.limit/2) break;
                    //var effect = (typeof docs[i].effect != 'undefined') ? docs[i].effect : ' ';
                    html += '<div class="first-news ' + (i == 0 ? 'last' : 'dashed') + '">' +
                        '<h3 class="post-title"><a  href="' + docs[i].url + '" title="' + docs[i].title + '" >' + docs[i].title + '</a></h3>' +
                        '<div class="post-time">';
                            if (docs[i].url.match(/article.html/g)) {
                                html += '<div class="post-time-item">(' + docs[i].datetime + ')</div>';
                            } else {
                                html += '<div class="post-time-item">Ban hành: <span class="color2"> ' +
                                    docs[i].datetime +
                                    '</span></div>';
                                    //'<div class="post-time-item">Hiệu lực: <span class="color2">' + effect + '</span></div>';
                            }
                    html+='</div>' +
                    '</div>';
                    j++;
                }
                el.append(
			            '<div class="title" style="margin-bottom: 0px;">' +
				            '<h2 class="title-cat">Nội dung vừa xem</h2>' +
			            '</div>' +
			            '<div id="owl-demo-c3" class="cat-box-content">' + html +
			            '</div>'
                );
            }
        }
        return this.each(function () {
            var self = $(this);
            self.lawsExists(function () {
                proc.setDoc();
                proc.getContent(self);
            });
        });
    }
})(jQuery);

(function ($) {
    $.fn.extend({
        lawsVnFlip: function (options) {
            var defaults = {
                btnFront: '.lawsVnFlipFront',
                divFront: '.divFront',
                btnBack: '.lawsVnFlipBack',
                divBack: '.divBack',
                duration: 300
            };
            options = $.extend({}, defaults, options);
            return this.each(function () {
                var front = $(options.divFront);
                var back = $(options.divBack);

                var margin = front.width() / 2;
                var width = front.width();
                var height = front.height();
                back.css({
                    width: '0',
                    height: '' + height + 'px',
                    marginLeft: '' + margin + 'px',
                    opacity: '0'
                });


                $(options.btnFront).on('click', function (e) {
                    e.preventDefault();
                    //hiện div back, ẩn div front
                    var toShow = back,
                        toHide = front;

                    // ẩn slide
                    (toHide).animate({
                        width: 0,
                        height: height,
                        marginLeft: margin,
                        opacity: 0
                    }, options.duration, function () {
          
                        // hiện slide
                        (toShow).animate({
                            width: width,
                            height: height,
                            marginLeft: 0,
                            opacity: 1
                        }, options.duration).show();
                    });
                });

                $(options.btnBack).on('click', function (e) {
                    e.preventDefault();
                    //hiện div front, ẩn div back
                    var toShow = front,
                        toHide = back;

                    // ẩn slide
                    (toHide).animate({
                        width: 0,
                        height: height,
                        marginLeft: margin,
                        opacity: 0
                    }, options.duration, function () {

                        // hiện slide
                        (toShow).animate({
                            width: width,
                            height: height,
                            marginLeft: 0,
                            opacity: 1
                        }, options.duration).show();
                    });
                });
            });
        }
    });

})(jQuery);

$.fn.extend({
    LawScrollTo: function () {
        $(this).lawsExists(function() {
            var x = $(this).offset().top - 100;
            jQuery('html,body').animate({ scrollTop: x }, 100);
        });
    }
});


(function ($) {

    function appendContent($el, content) {
        if (!content) return;

        // Simple test for a jQuery element
        $el.append(content.jquery ? content.clone() : content);
    }

    function appendBody($body, $element, opt) {
        // Clone for safety and convenience
        var $content = $element.clone();

        if (opt.removeScripts) {
            $content.find('script').remove();
        }

        if (opt.printContainer) {
            // grab $.selector as container
            $body.append($("<div/>").html($content).html());
        } else {
            // otherwise just print interior elements of container
            $content.each(function () {
                $body.append($(this).html());
            });
        }
    }

    var opt;
    $.fn.printThis = function (options) {
        opt = $.extend({}, $.fn.printThis.defaults, options);
        var $element = this instanceof jQuery ? this : $(this);

        var strFrameName = "printThis-" + (new Date()).getTime();

        if (window.location.hostname !== document.domain && navigator.userAgent.match(/msie/i)) {
            // Ugly IE hacks due to IE not inheriting document.domain from parent
            // checks if document.domain is set by comparing the host name against document.domain
            var iframeSrc = "javascript:document.write(\"<head><script>document.domain=\\\"" + document.domain + "\\\";</s" + "cript></head><body></body>\")";
            var printI = document.createElement('iframe');
            printI.name = "printIframe";
            printI.id = strFrameName;
            printI.className = "MSIE";
            document.body.appendChild(printI);
            printI.src = iframeSrc;

        } else {
            // other browsers inherit document.domain, and IE works if document.domain is not explicitly set
            var $frame = $("<iframe id='" + strFrameName + "' name='printIframe' />");
            $frame.appendTo("body");
        }

        var $iframe = $("#" + strFrameName);

        // show frame if in debug mode
        if (!opt.debug) $iframe.css({
            position: "absolute",
            width: "0px",
            height: "0px",
            left: "-600px",
            top: "-600px"
        });

        // $iframe.ready() and $iframe.load were inconsistent between browsers
        setTimeout(function () {

            // Add doctype to fix the style difference between printing and render
            function setDocType($iframe, doctype) {
                var win, doc;
                win = $iframe.get(0);
                win = win.contentWindow || win.contentDocument || win;
                doc = win.document || win.contentDocument || win;
                doc.open();
                doc.write(doctype);
                doc.close();
            }

            if (opt.doctypeString) {
                setDocType($iframe, opt.doctypeString);
            }

            var $doc = $iframe.contents(),
                $head = $doc.find("head"),
                $body = $doc.find("body"),
                $base = $('base'),
                baseURL;

            // add base tag to ensure elements use the parent domain
            if (opt.base === true && $base.length > 0) {
                // take the base tag from the original page
                baseURL = $base.attr('href');
            } else if (typeof opt.base === 'string') {
                // An exact base string is provided
                baseURL = opt.base;
            } else {
                // Use the page URL as the base
                baseURL = document.location.protocol + '//' + document.location.host;
            }

            $head.append('<base href="' + baseURL + '">');

            // import page stylesheets
            if (opt.importCSS) $("link[rel=stylesheet]").each(function () {
                var href = $(this).attr("href");
                if (href) {
                    var media = $(this).attr("media") || "all";
                    $head.append("<link type='text/css' rel='stylesheet' href='" + href + "' media='" + media + "'>");
                }
            });

            // import style tags
            if (opt.importStyle) $("style").each(function () {
                $(this).clone().appendTo($head);
            });

            // add title of the page
            if (opt.pageTitle) $head.append("<title>" + opt.pageTitle + "</title>");

            // import additional stylesheet(s)
            if (opt.loadCSS) {
                if ($.isArray(opt.loadCSS)) {
                    jQuery.each(opt.loadCSS, function (index, value) {
                        $head.append("<link type='text/css' rel='stylesheet' href='" + this + "'>");
                    });
                } else {
                    $head.append("<link type='text/css' rel='stylesheet' href='" + opt.loadCSS + "'>");
                }
            }

            // copy 'root' tag classes
            var tag = opt.copyTagClasses;
            if (tag) {
                tag = tag === true ? 'bh' : tag;
                if (tag.indexOf('b') !== -1) {
                    $body.addClass($('body')[0].className);
                }
                if (tag.indexOf('h') !== -1) {
                    $doc.find('html').addClass($('html')[0].className);
                }
            }

            // print header
            appendContent($body, opt.header);

            if (opt.canvas) {
                // add canvas data-ids for easy access after cloning.
                var canvasId = 0;
                $element.find('canvas').each(function () {
                    $(this).attr('data-printthis', canvasId++);
                });
            }

            appendBody($body, $element, opt);

            if (opt.canvas) {
                // Re-draw new canvases by referencing the originals
                $body.find('canvas').each(function () {
                    var cid = $(this).data('printthis'),
                        $src = $('[data-printthis="' + cid + '"]');

                    this.getContext('2d').drawImage($src[0], 0, 0);

                    // Remove the markup from the original
                    $src.removeData('printthis');
                });
            }

            // capture form/field values
            if (opt.formValues) {
                // loop through inputs
                var $input = $element.find('input');
                if ($input.length) {
                    $input.each(function () {
                        var $this = $(this),
                            $name = $(this).attr('name'),
                            $checker = $this.is(':checkbox') || $this.is(':radio'),
                            $iframeInput = $doc.find('input[name="' + $name + '"]'),
                            $value = $this.val();

                        // order matters here
                        if (!$checker) {
                            $iframeInput.val($value);
                        } else if ($this.is(':checked')) {
                            if ($this.is(':checkbox')) {
                                $iframeInput.attr('checked', 'checked');
                            } else if ($this.is(':radio')) {
                                $doc.find('input[name="' + $name + '"][value="' + $value + '"]').attr('checked', 'checked');
                            }
                        }

                    });
                }

                // loop through selects
                var $select = $element.find('select');
                if ($select.length) {
                    $select.each(function () {
                        var $this = $(this),
                            $name = $(this).attr('name'),
                            $value = $this.val();
                        $doc.find('select[name="' + $name + '"]').val($value);
                    });
                }

                // loop through textareas
                var $textarea = $element.find('textarea');
                if ($textarea.length) {
                    $textarea.each(function () {
                        var $this = $(this),
                            $name = $(this).attr('name'),
                            $value = $this.val();
                        $doc.find('textarea[name="' + $name + '"]').val($value);
                    });
                }
            } // end capture form/field values

            // remove inline styles
            if (opt.removeInline) {
                // $.removeAttr available jQuery 1.7+
                if ($.isFunction($.removeAttr)) {
                    $doc.find("body *").removeAttr("style");
                } else {
                    $doc.find("body *").attr("style", "");
                }
            }

            // print "footer"
            appendContent($body, opt.footer);

            setTimeout(function () {
                if ($iframe.hasClass("MSIE")) {
                    // check if the iframe was created with the ugly hack
                    // and perform another ugly hack out of neccessity
                    window.frames["printIframe"].focus();
                    $head.append("<script>  window.print(); </s" + "cript>");
                } else {
                    // proper method
                    if (document.queryCommandSupported("print")) {
                        $iframe[0].contentWindow.document.execCommand("print", false, null);
                    } else {
                        $iframe[0].contentWindow.focus();
                        $iframe[0].contentWindow.print();
                    }
                }

                // remove iframe after print
                if (!opt.debug) {
                    setTimeout(function () {
                        $iframe.remove();
                    }, 1000);
                }

            }, opt.printDelay);

        }, 333);

    };

    // defaults
    $.fn.printThis.defaults = {
        debug: false,           // show the iframe for debugging
        importCSS: true,        // import parent page css
        importStyle: false,     // import style tags
        printContainer: false,   // print outer container/$.selector
        loadCSS: "",            // load an additional css file - load multiple stylesheets with an array []
        pageTitle: "",          // add title to print page
        removeInline: false,    // remove all inline styles
        printDelay: 333,        // variable print delay
        header: null,           // prefix to html
        footer: null,           // postfix to html
        formValues: true,       // preserve input/form values
        canvas: false,          // copy canvas content (experimental)
        base: false,            // preserve the BASE tag, or accept a string for the URL
        doctypeString: '<!DOCTYPE html>', // html doctype
        removeScripts: false,   // remove script tags before appending
        copyTagClasses: false   // copy classes from the html & body tag
    };
})(jQuery);

$.fn.clearErrors = function () {
    $(this).each(function () {
        $(this).trigger('reset.unobtrusiveValidation');
    });
};

(function ($) {
    $.fn.lawServicesWizard = function (options) {
        var defaults = {
            itemNav: '.navstep',
            navStep: '.nav-step',
            itemstep: 'span.itemstep-m',
            divSteps: 'div.law-steps', 
            backStep: '.back-step',
            nextStep: '.next-step',
            duration: 250,
            validate: false,
            fnValidate: function () {
                var servicePackageParentId = $('select[name="ServicePackageParentId"] option:selected').val();
                var servicePackageId = $('select[name="ServicePackageId"] option:selected').val();
                if (servicePackageParentId > 0 && servicePackageId > 0) {
                    if (servicePackageParentId > 0) {
                        $('select[name="ServicePackageParentId"]').lawsExists(function () {
                            $(this).removeClass('border-warning');
                        });
                    }
                    if (servicePackageId > 0) {
                        $('select[name="ServicePackageId"]').lawsExists(function () {
                            $(this).removeClass('border-warning');
                        });
                    }
                    return true;
                }
                else if (servicePackageParentId == 0) {
                    $('select[name="ServicePackageParentId"]').lawsExists(function () {
                        $(this).addClass('border-warning');
                    });
                    $().lawsDialog({
                        dialogClass: 'lawsVnDialogTitle',
                        messages: ['Quý khách vui lòng chọn số người sử dụng.']
                    });
                } else if (servicePackageId == 0) {
                    $('select[name="ServicePackageId"]').lawsExists(function () {
                        $(this).addClass('border-warning');
                    });
                    $().lawsDialog({
                        dialogClass: 'lawsVnDialogTitle',
                        messages: ['Quý khách vui lòng chọn thời hạn thuê bao.']
                    });
                }
                return false;
            },
            stepValidate: 1
        };
        options = $.extend({}, defaults, options);
        var el = this;
        var i = 1;
        var length = $(options.divSteps, el).size();
        var proc = {
            displayStep: function (index) {
                var s = index > 0 ? index - 1 : index;
                $(options.itemstep).removeClass('active').addClass('undone');
                $($(options.itemstep)[s]).removeClass('undone').addClass('active');

                $(options.divSteps, el).animate({
                    opacity: 0
                }, 50, function () {
                    $(options.divSteps + ':nth-child(' + index + ')', el).animate({
                        opacity: 1
                    }, 50, function () {
                        $(options.itemNav).LawScrollTo();
                    }).show();
                }).hide();
            }
        }
        $(options.itemstep).removeClass('active').addClass('undone');
        $(options.navStep, $(options.itemNav)).unbind().click(function (event) {
            event.preventDefault();
            var id = $(this).data('id'); 
            if (options.validate && id > options.stepValidate) {
                var execOnValid = $.isFunction(options.fnValidate) ? options.fnValidate : $.noop;
                if (!execOnValid()) {
                    return;
                }
            }
            i = id;
            proc.displayStep(id);
            $('#PromotionCodeCheckForm input').lawsExists(function () {
                $('#PromotionCodeCheckForm input').clearErrors();
            });
            $('#ValidPaymentMethodsBankAccountForm input').lawsExists(function () {
                $('#ValidPaymentMethodsBankAccountForm input').clearErrors();
            });
        });
        $(options.nextStep, el).unbind().click(function (event) {
            event.preventDefault(); 
            if (options.validate && options.stepValidate == i) {
                var execOnValid = $.isFunction(options.fnValidate) ? options.fnValidate : $.noop;
                if (!execOnValid()) {
                    return;
                }
            }
            if (i < length) i++;
            proc.displayStep(i);
            $('#PromotionCodeCheckForm input').lawsExists(function () {
                $('#PromotionCodeCheckForm input').clearErrors();
            });
            $('#ValidPaymentMethodsBankAccountForm input').lawsExists(function () {
                $('#ValidPaymentMethodsBankAccountForm input').clearErrors();
            });
        });

        $(options.backStep, el).unbind().click(function (event) {
            event.preventDefault();
            if (i > 1) i--;
            proc.displayStep(i);
            $('#PromotionCodeCheckForm input').lawsExists(function () {
                $('#PromotionCodeCheckForm input').clearErrors();
            });
            $('#ValidPaymentMethodsBankAccountForm input').lawsExists(function () {
                $('#ValidPaymentMethodsBankAccountForm input').clearErrors();
            });
        });
        
        return this.each(function () {
            proc.displayStep(1);
        });
    }
})(jQuery);

(function () {
    $.fn.lawFields = function (data) {
        var el = $(this).find(':input').get();
        var fields;
        if (arguments.length === 0) {
            fields = {};
            $.each(el, function () {
                if (this.name && !this.disabled && (this.checked
                    || /select|textarea/i.test(this.nodeName)
                    || /text|hidden|password/i.test(this.type))) {
                    if (fields[this.name] == undefined) {
                        fields[this.name] = [];
                    }
                    fields[this.name] = $(this);
                }
            });
            return fields;
        } else {
            $.each(el, function () {
                if (this.name && data[this.name]) {
                    var names = data[this.name];
                    var self = $(this);
                    if (Object.prototype.toString.call(names) !== '[object Array]') {
                        names = [names];
                    }
                    if (this.type == 'checkbox' || this.type == 'radio') {
                        var val = self.val();
                        var found = false;
                        for (var i = 0; i < names.length; i++) {
                            if (names[i] == val) {
                                found = true;
                                break;
                            }
                        }
                        self.attr('checked', found);
                    } else {
                        self.val(names[0]);
                    }
                }
            });
            return this;
        }
    };
})(jQuery);

(function() {
    $.fn.mobilePopup = function(options) {
        var defaults = {
            dataAttr: 'data-toggle-target',
            classHidden: 'target-hidden',
            classShow: 'target-expanded'
        }
        options = $.extend(defaults, options);
        var targetPopup = $(this).attr(options.dataAttr);
        $(targetPopup).addClass(options.classHidden);
        $(this).click(function(e) {
            e.preventDefault();
            $(targetPopup).toggleClass(options.classShow).css('display', '');
        });
    }
})(jQuery);

(function ($) {
    $.fn.bankSelect = function (options) {
        var defaults = {
            divContainerClassName: 'bankSelect',
            divSelectedClassName: 'selectBox',
            ulClassName: 'dropDown',
            skipData: false,
            dropDownExpandClass: 'expanded'
        };
        options = $.extend(defaults, options);
        var self = this;
        var wWidth = $(window).width();
        var dWidth = wWidth * 0.9;
        var selectBoxContainer = $('<div>', {
            width: dWidth,//self.outerWidth(),
            'class': options.divContainerClassName,
            html: '<div class="' + options.divSelectedClassName + '"></div>'
        });
        var dropDown = $('<ul>', { 'class': options.ulClassName });
        var selectBox = selectBoxContainer.find('.' + options.divSelectedClassName);
        self.find('option').each(function (i) {
            var option = $(this);
            if (i == 0) {
                selectBox.html(option.text());
            }
            if (options.skipData && option.data('skip')) { return true; }
            var li = option.data('skip') ? $('<li>', {
                onClick: 'lawsVn.bankSelectReset(this,"' + options.divContainerClassName + '")',
                html: '<span style="left:15px;">' + option.data('html-text') + '</span>'
            }) : $('<li>', {
                    onClick: 'lawsVn.bankSelectSet(this,"' + options.divContainerClassName + '","' + option.val()+'");',
                html: '<img src="' + option.data('icon') + '" /><span>' +
                option.data('html-text') + '</span>'
            });

            li.click(function () {
                selectBox.html(option.text());
                dropDown.trigger('hide');
                self.val(option.val());
                return false;
            });
            dropDown.append(li);
        });

        selectBoxContainer.append(dropDown.hide());
        self.hide().after(selectBoxContainer);
        dropDown.bind('show', function () {

            if (dropDown.is(':animated')) {
                return false;
            }
            $('ul.' + options.ulClassName).not(dropDown).each(function() {
                $(this).trigger('hide');
            });
            selectBox.addClass(options.dropDownExpandClass);
            dropDown.slideDown();

        }).bind('hide', function () {

            if (dropDown.is(':animated')) {
                return false;
            }

            selectBox.removeClass(options.dropDownExpandClass);
            dropDown.slideUp();

        }).bind('toggle', function () {
            if (selectBox.hasClass(options.dropDownExpandClass)) {
                dropDown.trigger('hide');
            }
            else dropDown.trigger('show');
        });

        selectBox.click(function () {
            dropDown.trigger('toggle');
            return false;
        });

        $(document).click(function () {
            dropDown.trigger('hide');
        });
    };

}(jQuery));

var lawsInfo = function () {
    console.log("%c LuatVietnam.Vn - Cơ sở dữ liệu văn bản pháp luật lớn nhất Việt Nam. %c \n Bản quyền \xa9 2000-2017 bởi LuatVietNam - Thành viên INCOM Communications ., JSC \n Giấy phép thiết lập trang Thông tin điện tử tổng hợp số: 692/GP-TTĐT cấp ngày 29/10/2010 bởi Sở TT-TT Hà Nội, thay thế giấy phép số: 322/GP - BC, ngày 26/07/2007, cấp bởi Bộ Thông tin và Truyền thông \n" +
        "Chứng nhận bản quyền tác giả số 280/ 2009 / QTG ngày 16/ 02 / 2009, cấp bởi Bộ Văn hoá - Thể thao - Du lịch \n" +
        "Cơ quan chủ quản: Công ty Cổ phần Truyền thông Quốc tế INCOM.Chịu trách nhiệm: Ông Vũ Mạnh Cường \n" +
        "Giấy chứng nhận đăng ký DN số: 0102011152, do Sở Kế hoạch và Đầu tư Hà Nội cấp ngày 11/ 08 / 2006.\n" +
        "Địa chỉ: Tầng 3, Tòa nhà IC, 82 phố Duy Tân, Cầu Giấy, Hà Nội.\n" +
        "Điện thoại: (024) 37833688 - Fax: (024) 37833699 * Hỗ trợ: 1900561589 * Email: lawdata@luatvietnam.vn", 'font-family: "Helvetica Neue", Helvetica, Arial, sans-serif;font-size:24px;color:#a67942;-webkit-text-fill-color:#a67942;-webkit-text-stroke: 1px #a67942;', "font-size:12px;color:#000;");
}

function focusguidbyid(input) {
    try {
        if (input != null) {
            var input_ = $(input);
            var str_id_guid = input_.attr('href');
            var btn_guid = $(str_id_guid);
            if (btn_guid.length) {
                var li_parent = btn_guid.parent();
                if (!li_parent.hasClass('active')) {
                    btn_guid.trigger('click');
                }
            }
        }
    } catch (ex) {
        console.log(ex);
    }
}
$(document).ready(function () {
    var url_cur = location.href;
    if (url_cur.indexOf("#") > -1 && url_cur.indexOf('huong-dan.html') > -1) {
        idx = url_cur.indexOf("#");
        var str_id_guid = idx != -1 ? url_cur.substring(idx + 1) : "";
        if (str_id_guid.length) {
            var btn_guid = $('#' + str_id_guid);
            if (btn_guid.length) {
                var li_parent = btn_guid.parent();
                if (!li_parent.hasClass('active')) {
                    btn_guid.trigger('click');
                }
            }
        }
    }
});

