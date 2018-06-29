$(function () {
    lawsVn.init();
    lawsInfo();
    $(document).ajaxError(function (e, xhr) {
        if (xhr.status === 403) {
            var response = $.parseJSON(xhr.responseText);
            if (response.Message != null && response.Message == 'dichvu') {
                lawsVnConfig.response = response.Message;
                $().lawsDialog({
                    title: lawResource.getMessageByName('Message'),
                    dialogClass: 'lawsVnDialogTitle',
                    messages: [lawResource.getMessageByName('gia-dich-vu')],
                    buttons: [
                        {
                            text: lawResource.getMessageByName('dang-nhap'),
                            'class': 'btn-thongbao1 lawsVnLogin'
                        },
                        {
                            text: lawResource.getMessageByName('Close'),
                            click: function () {
                                $(this).dialog('close');
                            }
                        }
                    ]
                });
                return;
            }
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
    searchUrl: 'tim-van-ban-phap-luat.html',
    LanguageId: 2,
    DocGroupId: 0,
    arrLinhvuctracuu: new Array(),
    arrTrangthaihieuluc: new Array(),
    arrLoaivanban: new Array(),
    arrCoquanbanhanh: new Array(),
    arrNambanhanh: new Array(),
    arrAdvs: [],
    CultureName: 'LawsVnENCulture',
    currentFontSize: 14,
    response:''
};
var lawterminConfig = {
    rootPath: '/',
    docContentViewLimit: 10,
    pathName: window.location.pathname,
    searchUrl: 'thuat-ngu-phap-ly.html'
};

var lawsVn = {
    init: function () {
        this.events();
    },
    events: function () {
        window.onscroll = function () { lawsVn.scrollFunction() };
        //$(document).tooltip();
        $(document).on('click',
            '.tab-nav-item1',
            function (event) {
                event.preventDefault();
                $('.tab-nav-item1').removeClass('active');
                $(this).addClass('active');
            });
        $(document).on('click',
            '.tab-nav-item2',
            function (event) {
                event.preventDefault();
                $('.tab-nav-item2').removeClass('active');
                $(this).addClass('active');
            });
        $(document).on('click',
            '.tab-nav-item3',
            function (event) {
                event.preventDefault();
                $('.tab-nav-item3').removeClass('active');
                $(this).addClass('active');
            });

        $('.tab-nav-widget1').off('click').on('click',
            function (event) {
                event.preventDefault();
                $('.tab-nav-widget1').removeClass('active');
                $(this).addClass('active');
            });
        $('.sub-menu2-item').off('click').on('click',
            function () {
                $('.sub-menu2-item').removeClass('active');
                $(this).addClass('active');
            });
        $('.tab-nav-item-ad').off('click').on('click',
            function () {
                $('.tab-nav-item-ad').removeClass('active');
                $(this).addClass('active');
            });
        $('.fontpage').off('click').on('click',
            function () {
                $('.fontpage').removeClass('active');
                $(this).addClass('active');
            });
        $('.post-tag-abc').off('click').on('click',
            function () {
                $('.post-tag-abc').removeClass('active');
                $(this).addClass('active');
            });
        $(document).on('click',
            '.lawsVnLogin',
            function (event) {
                event.preventDefault();
                lawsVn.login();
            });
        $(document).on('click',
            '#fbShare',
            function (e) {
                e.preventDefault();
                var winTop = (screen.height / 2) - (350 / 2);
                var winLeft = (screen.width / 2) - (600 / 2);
                var facebookWindow = window.open('https://www.facebook.com/sharer/sharer.php?u=' + document.URL, 'facebook-popup', 'menubar=no,toolbar=no,resizable=yes,scrollbars=yes,top=' + winTop + ',left=' + winLeft + ',height=350,width=600');
                if (facebookWindow.focus) { facebookWindow.focus(); }
                return false;
            });

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
        $('.no-permission-view').tooltip({
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
        $('select').each(function () {
            var item = $(this).find('option:selected').val();
            if (item > 0) {
                $(this).addClass('select-background-selected');
            } else $(this).removeClass('select-background-selected');
        });
        $(document).on('change',
            'select',
            function () {
                var item = $(this).find('option:selected').val();
                if (item > 0) {
                    $(this).addClass('select-background-selected');
                } else $(this).removeClass('select-background-selected');
                //$('option', $(this)).each(function (index) {
                //    if ($(this).is(":selected") && $(this).val() > 0) {
                //        $(this).addClass('select-background-selected');
                //    }
                //    else {
                //        $(this).removeClass('select-background-selected');
                //    }
                //});
            });
        $(document).on('click',
            '.forgot-password',
            function (event) {
                event.preventDefault();
                lawsVn.forgotPassword();
            });
        $(document).on('click',
            '.send-question',
            function (event) {
                event.preventDefault();
                lawsVn.sendQuestion();
            });
        $(document).on('click',
            '.btn-reset',
            function (event) {
                event.preventDefault();
                lawsVn.ResetForm($(this).closest('form'));
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
                                    if (resp.Data.FeeType === 'Free') {
                                        //lawsVn.account.registerFreeService(serviceId);
                                    } else if (resp.Data.FeeType === 'Trial') {
                                        //lawsVn.account.registerTrialService(serviceId);
                                    } else if (resp.Data.FeeType === 'Sub') {
                                        window.location.href = lawsVn.virtualPath('/user/subscriber.html');
                                    }
                                } else { //đã đăng ký dịch vụ: hiện thông báo tương ứng
                                    if (resp.Data.FeeType === 'Free' ||
                                        resp.Data.FeeType === 'Trial' ||
                                        resp.Data.ActionButton === '') {
                                        $().lawsDialog({
                                            dialogClass: 'lawsVnDialogTitle',
                                            messages: [resp.Data.Messages + '<a href="' + lawsVn.virtualPath('/user/subscriber.html') + '">' + lawResource.getMessageByName('dang-ky-dich-vu') + '</a>']
                                        });
                                    } else if (resp.Data.ActionButton === 'NoAction') {
                                        $().lawsDialog({
                                            dialogClass: 'lawsVnDialogTitle',
                                            messages: [resp.Data.Messages]
                                        });
                                    }
                                    else if (resp.Data.ActionButton === 'ReNew' || resp.Data.ActionButton === 'ReNewAndConvert') {
                                        var buttons = [
                                            {
                                                text: lawResource.getMessageByName('gia-han'),
                                                'class': 'btn-nhap-lai dv',
                                                click: function () {
                                                    window.location.href =
                                                        lawsVn.virtualPath('/user/service-renewal.html');
                                                }
                                            },
                                            {
                                                text: lawResource.getMessageByName('chuyen-doi'),
                                                'class': 'btn-nhap-lai dv',
                                                click: function () {
                                                    if (resp.Data.ListServicesIdUpgradeAllowed != null) {
                                                        var btnConversion = [];
                                                        var listServicesIdUpgradeAllowed = resp.Data.ListServicesIdUpgradeAllowed;
                                                        $.each(listServicesIdUpgradeAllowed,
                                                            function(i,item) {
                                                                btnConversion.push({
                                                                    text: lawResource.getMessageByName(item),
                                                                    'class': 'btn-thongbao1',
                                                                    click: function () {
                                                                        window.location.href =
                                                                            lawsVn.virtualPath('/user/service-conversion.html?serviceId=' + item);
                                                                        $(this).dialog('close');
                                                                    }
                                                                });
                                                            });
                                                        
                                                        $().lawsDialog({
                                                            title : '',
                                                            dialogClass: 'lawsVnDialogTitle',
                                                            width: 360,
                                                            messages: [lawResource.getMessageByName('chon-dich-vu-chuyen-doi')],
                                                            onCreate: function () {
                                                                var elem = $('.content-thongbao');
                                                                elem.removeAttr('width');
                                                                elem.css('width', '91.7%');
                                                            },
                                                            buttons: btnConversion
                                                        });
                                                    }else 
                                                    window.location.href =
                                                        lawsVn.virtualPath('/user/service-conversion.html');
                                                }
                                            },
                                            {
                                                text: lawResource.getMessageByName('Close'),
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
                                            width: 620,
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
        $(document).on('click',
            '.btn-xacnhan',
            function (event) {
                event.preventDefault();
                $(this).closest('form').submit();
            });
        $('.TermsConditionsView').off('click').on('click',
            function (event) {
                event.preventDefault();
                lawsVn.TermsConditionsView();
            });

        $('.no-permission-download').tooltip();

        $(document).on('click',
            '.docPrint',
            function (event) {
                event.preventDefault();
                $('#docPrint').lawsExists(function () {
                    $(this).printThis({
                        pageTitle: 'Luật Việt Nam',
                        header: '<div class="hearder-left"><div class="logo"><img alt="luat viet nam" src="' + lawsVn.virtualPath('/assets/images/logo.png') + '"></div></div>',
                        loadCSS: lawsVn.virtualPath('/assets/print.css')
                    });
                });
            });

        $(document).on('click',
            '.continute-to-pay',
            function (event) {
                event.preventDefault();
                $('.itemstep').removeClass('active').addClass('undone');
                $('#step-1').lawsExists(function() {
                    $(this).fadeOut(100);
                });
                $('#step-2').lawsExists(function () {
                    $(this).fadeIn(100,function() {
                        $(('.itemstep:eq(1)')).removeClass('undone').addClass('active');
                        $('.navstep').LawScrollTo();
                    });
                });
            });
        $(document).on('click',
            '.back-to-step-1',
            function (event) {
                event.preventDefault();
                $('.itemstep').removeClass('active').addClass('undone');
                $('#step-2').lawsExists(function () {
                    $(this).fadeOut(500);
                });
                $('#step-1').lawsExists(function () {
                    $(this).fadeIn(500, function () {
                        $(('.itemstep:eq(0)')).removeClass('undone').addClass('active');
                        $('.navstep').LawScrollTo();
                    });
                });
            });
        $(document).on('click',
            '.back-to-step-2',
            function (event) {
                event.preventDefault();
                $('.itemstep').removeClass('active').addClass('undone');
                $('#step-3').lawsExists(function () {
                    $(this).fadeOut(500);
                });
                $('#step-2').lawsExists(function () {
                    $(this).fadeIn(500, function () {
                        $(('.itemstep:eq(1)')).removeClass('undone').addClass('active');
                        $('.navstep').LawScrollTo();
                    });
                });
            });
        $(document).on('click',
        '.btn-close',
        function () {
            $(this).closest('.ui-dialog-content').dialog('close');
            return false;
        });
        $(document).on('click',
            '.goto-step-3',
            function (event) {
                event.preventDefault();
                $('.itemstep').removeClass('active').addClass('undone');
                $('#step-2').lawsExists(function () {
                    $(this).fadeOut(500);
                });
                $('#step-3').lawsExists(function () {
                    $(this).fadeIn(500, function () {
                        $(('.itemstep:eq(2)')).removeClass('undone').addClass('active');
                        $('.navstep').LawScrollTo();
                    });
                });
            });
        
        $(document).on('change',
            'input[type="radio"][name="ServicePackageParentId"]',
            function () {
                if ($(this).is(':checked')) {
                    var serviceId = $('input[type="hidden"][name="ServiceId"]').val();
                    var servicePackageParentId = $(this).val();
                    $('td.center-post').lawsExists(function () {
                        $(this).removeClass('border-warning');
                    });
                    var ddl = $('select[name="ServicePackageId"]');
                    
                    $.lawsAjax({
                        url: lawsVn.virtualPath('/Ajax/SubscriberByServicePackageParentId'),
                        data: { serviceId:serviceId, servicePackageParentId: servicePackageParentId },
                        type: 'Post',
                        dataType:'json',
                        success:function(resp) {
                            if (resp.Data != null) {
                                ddl.html('');
                                lawsVn.registerServiceFormReset();
                                if (resp.Data.ListServicePackages != null && resp.Data.ListServicePackages.length > 0) {
                                    $.each(resp.Data.ListServicePackages, function (id, option) {
                                        var servicePackageName = resp.Data.LanguageId == 1
                                            ? option.ServicePackageDesc
                                            : option.ServicePackageName;
                                        ddl.append($('<option></option>').val(option.ServicePackageId).html(servicePackageName));
                                    });
                                }
                            }
                        }
                    });
                }
            });
        $(document).on('change',
            'select[name="ServicePackageId"]',
            function () {
                var serviceId = $('input[type="hidden"][name="ServiceId"]').val();
                var servicePackageId = $(this).val();
                $.lawsAjax({
                    url: lawsVn.virtualPath('/Ajax/SubscriberByServicePackageId'),
                    data: { serviceId: serviceId, servicePackageId: servicePackageId },
                    type: 'Post',
                    dataType: 'json',
                    success: function (resp) {
                        if (resp.Data != null) {
                            lawsVn.registerServiceFormReset();
                            if (resp.Data.ServicePackage != null) {
                                if (resp.Data.ServicePackage != null) {
                                    var servicePackageName = resp.Data.LanguageId == 1
                                        ? resp.Data.ServicePackage.ServicePackageDesc
                                        : resp.Data.ServicePackage.ServicePackageName;
                                    $('#BankPaymentForm').lawFields({
                                        ServicePackageId: resp.Data.ServicePackage.ServicePackageId,
                                        ServicePackageName: servicePackageName,
                                        Total: resp.Data.Total,
                                        Price: resp.Data.ServicePackage.Price
                                    });
                                    $('#step2-ServicePackageName').lawsExists(function () {
                                        $(this).text(servicePackageName);
                                    });

                                    $('#step2-ConcurrentLogin').lawsExists(function () {
                                        $(this).text(resp.Data.ConcurrentLoginText);
                                    });

                                    $('#step2-Expirydate').lawsExists(function () {
                                        $(this).text(resp.Data.ExpiryDate);
                                    });

                                    $('#step2-price').lawsExists(function () {
                                        $(this).text(resp.Data.PriceText);
                                    });

                                    $('#step2-priceVat').lawsExists(function () {
                                        $(this).text(resp.Data.PriceVatText);
                                    });

                                    $('#step2-total').lawsExists(function () {
                                        $(this).text(resp.Data.TotalText);
                                    });
                                }
                            }
                        }
                    }
                });
            });
        $(document).on('change',
            'input[id^="TaxInvoice"]',
            function (event) {
                event.preventDefault();
                if (this.checked) {
                    $('<div id="TaxInvoiceFormLoad"></div>').lawsDialog({
                        title: '',
                        width: 640,
                        hideClose: false,
                        buttons: {},
                        onCreate: function () {
                            $('#loading').fadeIn('normal');
                        },
                        onOpen: function () {
                            $('#TaxInvoiceFormLoad').load(lawsVn.virtualPath('/Ajax/PartialTaxInvoice'),
                                function () {
                                    var form = $('#TaxInvoiceForm');
                                    form.removeData('validator');
                                    form.removeData('unobtrusiveValidation');
                                    $.validator.unobtrusive.parse(form);
                                });
                            $('#loading').fadeOut('normal');
                        }
                    });
                } else {
                    $('input[name="CompanyName"]').lawsExists(function () {
                        $(this).val('');
                    });
                    $('input[name="CompanyAddress"]').lawsExists(function () {
                        $(this).val('');
                    });
                    $('input[name="CompanyTaxCode"]').lawsExists(function () {
                        $(this).val('');
                    });
                    $('input[name="InternalContent"]').lawsExists(function () {
                        $(this).val('');
                    });
                }
            });
        $(document).on('click',
            '.gop-y',
            function (event) {
                event.preventDefault();
                var docName = $(this).data('name');
                lawsVn.gopy(docName);
            });
        $(document).on('click',
            '.doc-send-mail',
            function (event) {
                event.preventDefault();
                var docUrl = $(this).data('url');
                lawsVn.docSendMail(docUrl);
            });
        $('.lawsVnLawerQuestion').off('click').on('click',
            function (event) {
                event.preventDefault();
                var Lawerid = $(this).data('id');
                lawsVn.LawerQuestion(Lawerid);
            });
        $('.FaqViewDetail').off('click').on('click',
            function (event) {
                event.preventDefault();
                var FaqId = $(this).data('id');
                lawsVn.FaqViewDetail(FaqId);
            });
        $('.lawsVnChangePassword').off('click').on('click',
            function (event) {
                event.preventDefault();
                lawsVn.changePassword();
            });
        $('.lawsVnLogOut').off('click').on('click',
            function (event) {
                event.preventDefault();
                lawsVn.logOut();
            });
        $('.docUtilityPannel').off('click').on('click',
            function (event) {
                event.preventDefault();
                $('#docUtilityPannel').toggle();
            });
        $('.xoa-linh-vuc').off('click').on('click',
            function (event) {
                event.preventDefault();
                var customerFieldId = $(this).data('id');
                lawsVn.accountProfile.deleteOneFieldById(customerFieldId);
            });

        $('.select-customer-fields').off('click').on('click',
            function (event) {
                event.preventDefault();
                lawsVn.editCustomerFields();
            });
        $('.select-customer-province').off('click').on('click',
            function (event) {
                event.preventDefault();
                lawsVn.editCustomerProvince();
            });
        $('#advancedSearch').off('click').on('click',
            function (event) {
                event.preventDefault();
                $("#advancedSearchPannel").toggle();
            });
        $(document).on('click',
            '.getcaptcha',
            function (event) {
                event.preventDefault();
                var prefix = $(this).data('prefix');
                $('#LawsCaptcha').lawsExists(function () {
                    lawsVn.getCaptcha('LawsCaptcha', prefix);
                });
            });
        $(document).on('click',
            '.save-doc-of-interest',
            function (event) {
                event.preventDefault();
                var docId = $(this).data('id');
                var countMyDocuments = $('#countMyDocuments');
                var count = countMyDocuments.data('count');
                if (!$.isNumeric(count))
                    count = 0;
                $.lawsAjax({
                    url: lawsVn.virtualPath('/Ajax/SaveDocument'),
                    data: { docId: docId },
                    beforeSend: {},
                    success: function (resp) {
                        if (resp.Message !== null && resp.Message.length > 0) {
                            $().lawsDialog({
                                dialogClass: 'lawsVnDialogTitle',
                                messages: [resp.Message],
                            });
                        }
                        if (resp.Data.RegistTypeId !== 3) { //Đã thêm rồi -> ko tăng count
                            count++;
                            countMyDocuments.data('count', count);
                            countMyDocuments.text('(' + count + ')');
                        }
                    }
                });
            });
        $('#SignerName').lawsExists(function () {
            $(this).autocomplete({
                minLength: 1,
                dataType: "json",
                async: false,
                cache: false,
                source: function (request, response) {
                    //var signers = new Array();
                    $('#SignerId').val(0);
                    $('#signer-message').text('');
                    var dataGetter = { signerName: request.term };
                    var url = lawsVnConfig.rootPath + 'Ajax/AutocompleteSignerByName';
                    $.ajaxSetup({ cache: false, async: false });
                    $.lawsVnAjax(url,
                        'Get',
                        dataGetter,
                        function (data) {
                            var json = JSON.parse(data.jsonRetval);
                            if (json.length == 0)
                                $('#signer-message')
                                    .html(
                                        '<span class="text-danger field-validation-error"><span>' + lawResource.getMessageByName('SignerNotFound') + '</span></span>');
                            response($.map(json,
                                function (item) {
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
                search: function () {
                    $(this).addClass('ui-autocomplete-loading');
                },
                open: function () { $(this).removeClass('ui-autocomplete-loading'); },
                focus: function () {
                    // prevent value inserted on focus
                    return false;
                },
                select: function (event, ui) {
                    $('#SignerId').val(ui.item.val);
                }
            });
        });
        $(document).on('change',
            'form .select-onchange',
            function (event) {
                event.preventDefault();
                $('input[name="page"]').lawsExists(function () {
                    $(this).val(1);
                    lawsVn.ajaxEvents.pageIndex = 1; //reset page
                });
                $(this).closest('form').submit();
            });
        
        $('#cssmenu ul ul li:odd').addClass('odd');
        $('#cssmenu ul ul li:even').addClass('even');
        $('#cssmenu > ul > li > a').click(function () {
            var parent_cssmenu = $(this).parent().parent().parent().parent().parent();
            if (parent_cssmenu.length) {
                var cssmenu = parent_cssmenu.children().children();
                console.log(cssmenu.length)
                cssmenu.each(function () {
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
        $(document).on('click',
           'form .submit-link',
           function (event) {
               event.preventDefault();
               $(this).closest('form').submit();
           });
        $(document).on('change',
            '#txtChangePage',
            function () {
                var newValue = parseInt($(this).val());
                if (isNaN(newValue) || newValue <= 0) {
                    newValue = 1;
                }
                $(this).val(newValue);
                lawsVn.ajaxEvents.pageIndex = newValue;
                //lawsVn.delay();
                $(this.form).submit();
            });

        $(document).on('click',
            '#prevPage',
            function (event) {
                event.preventDefault();
                lawsVn.ajaxEvents.pageIndex = lawsVn.ajaxEvents.pageIndex > 1 ? lawsVn.ajaxEvents.pageIndex - 1 : 1;
                var newValue = $('#txtChangePage').val();
                var newchange = parseInt(newValue) - 1;
                $('#txtChangePage').val(newchange);
                var $form = $(this).closest('form');
                $form.submit();
            });

        $(document).on('click',
            '#nextPage',
            function (event) {
                event.preventDefault();
                lawsVn.ajaxEvents.pageIndex++;
                var newValue = $('#txtChangePage').val();
                var newchange = parseInt(newValue)+1;
                $('#txtChangePage').val(newchange);
                var $form = $(this).closest('form');
                $form.submit();

            });
    },
    ajaxEvents: {
        pageIndex: $('#txtChangePage').val(),
        //showNumberOfResults: $('#dllNumberOfResults option:selected').val(),
        pageTitle: $('title').text(),
        OnBegin: function () {
            $('#loading').fadeIn('normal');
        },
        OnComplete: function (element) {
            $('#loading').fadeOut('normal');
            $('#' + element).LawScrollTo();
        },
        OnCompleteV2: function (element) {
            $('#loading').fadeOut('normal');
            $('#' + element).LawScrollTo();
            var pageTitle = lawsVn.ajaxEvents.pageIndex > 1
                ? lawsVn.ajaxEvents.pageTitle + ' - ' + lawResource.getMessageByName('page') + ' ' + + lawsVn.ajaxEvents.pageIndex
                : lawsVn.ajaxEvents.pageTitle;
            var newUrl = lawsVn.urlParamAddOrUpdate('page', lawsVn.ajaxEvents.pageIndex);
            //newUrl = lawsVn.urlParamAddOrUpdate('showNumberOfResults', lawsVn.ajaxEvents.showNumberOfResults);
            lawsVn.setTitleAndHistory(pageTitle, newUrl);
        },
        OnSuccess: function (data, status, xhr) {
            if (data.Message !== void 0 && data.Message.length > 0) {
                if (data.Message === 'ModelStateInValid') {
                    $().lawsDialog({
                        dialogClass: 'lawsVnDialogTitle',
                        messages: [lawsVnConfig.PleaseCheckTheInformationWithRedWarning],
                        showIcon: false
                    });
                } else
                    $().lawsDialog({
                        dialogClass: 'lawsVnDialogTitle',
                        messages: [data.Message],
                        showIcon: false
                    });
            }
        },
        AccountLoginOnSuccess: function (data, status, xhr) {
            if (data.Completed) {
                if (data.ReturnUrl !== void 0 && data.ReturnUrl.length > 0)
                    setTimeout(function () {
                        window.location.href = data.ReturnUrl;
                        location.reload();
                    },
                        100);
                else window.location.href = lawsVnConfig.rootPath;
            } else if (data.Message !== void 0 && data.Message.length > 0) {
                $().lawsDialog({
                    dialogClass: 'lawsVnDialogTitle',
                    messages: [data.Message],
                    showIcon: false
                });
            }
        },
        LaweQuestionOnSuccess: function (data, status, xhr) {
            if (data.Completed) {
                $().lawsDialog({
                    dialogClass: 'lawsVnDialogTitle',
                    messages: [data.Message],
                    showIcon: false,
                    onClose: function () {
                        $('#lawsVnLawerQuestion').lawsExists(function () {
                            $(this).dialog('close');
                        });
                    }
                });
            } else if (data.Message !== void 0 && data.Message.length > 0) {
                $().lawsDialog({
                    dialogClass: 'lawsVnDialogTitle',
                    messages: [data.Message],
                    showIcon: false
                });
            }
        },
        LoginOnSuccessful: function (data, status, xhr) {
            if (data.Completed) {
                $().lawsDialog({
                    dialogClass: 'lawsVnDialogTitle',
                    messages: [data.Message],
                    showIcon: false,
                    onOpen: function () {
                        $("#lawsVnLogin").dialog("close");
                    },
                    onClose: function () {
                        if (data.ReturnUrl !== void 0 && data.ReturnUrl.length > 0)
                            setTimeout(function () {
                                window.location.href = data.ReturnUrl;
                                location.reload();
                            },
                                100);
                        else window.location.href = lawsVnConfig.rootPath;
                    }
                });
            } else if (data.Message !== void 0 && data.Message.length > 0) {
                $().lawsDialog({
                    dialogClass: 'lawsVnDialogTitle',
                    messages: [data.Message],
                    showIcon: false
                });
            }
        },
        LoginOnSuccess: function (data, status, xhr) {
            if (data.Completed) {
                $().lawsDialog({
                    dialogClass: 'lawsVnDialogTitle',
                    messages: [data.Message],
                    showIcon: false,
                    onOpen: function () {
                        $("#lawsVnLogin").lawsExists(function () {
                            $(this).dialog("close");
                        });
                        $("#changePassword").lawsExists(function () {
                            $(this).dialog("close");
                        });
                    },
                    onClose: function () {
                        //var url = window.location.href;
                        if (data.ReturnUrl !== null && data.ReturnUrl.length > 0)
                            setTimeout(function () {
                                window.location.href = data.ReturnUrl;
                                //location.reload();
                            }, 100);
                        //else window.location.href = lawsVnConfig.rootPath;
                        var activeDialogs = $(".ui-dialog:visible").find('.ui-dialog-content');
                        activeDialogs.dialog('close');
                    }
                });
            } else {
                if (data.Message === 'ModelStateInValid') {
                    $().lawsDialog({
                        dialogClass: 'lawsVnDialogTitle',
                        messages: [lawsVnConfig.PleaseCheckTheInformationWithRedWarning],
                    });
                } else
                    $().lawsDialog({
                        dialogClass: 'lawsVnDialogTitle',
                        messages: [data.Message],
                        showIcon: false
                    });
            }
        },
        ServiceRegistrationOnSuccess: function (data, status, xh) {
            if (data.Completed) {
                $('#OrderCode').html(lawResource.getMessageByName('YourServiceOrderCode') + ':<strong style="color: #a67942;"> ' +
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
            if (data.Completed) {
                if (data.Data != null) {
                    var result =
                        '<p style="padding-left: 180px; margin-top:10px; line-height: 24px; color: #222;">' + lawResource.getMessageByName('PromotionalCodeInformation') + '<strong style="color:#a67942;"> ' +
                            data.Data.PromotionDesc +
                            ' </strong></p>';
                    if (data.Data.NumMonthFree > 0) {
                        result +=
                            '<p style="padding-left: 180px; line-height: 24px; color: #222;">' + lawResource.getMessageByName('Promotion') + ':<strong style="color:#a67942;"> ' +
                            data.Data.NumMonthFree 
                            + lawResource.getMessageByName('Months') +'</strong></p>';
                    }
                    if (data.Data.NumDayFree > 0) {
                        result +=
                            '<p style="padding-left: 180px; line-height: 24px; color: #222;">' + lawResource.getMessageByName('Promotion') + ':<strong style="color:#a67942;"> ' +
                            data.Data.NumDayFree 
                            + lawResource.getMessageByName('Days') + '</strong></p>';
                    }
                    if (data.Data.PercentDecrease > 0) {
                        result +=
                            '<p style="padding-left: 180px; line-height: 24px; color: #222;">' + lawResource.getMessageByName('Promotion') + ':<strong style="color:#a67942;"> ' +
                            data.Data.PercentDecrease +
                            ' %</strong></p>';
                    } else {
                        if (data.Data.Amount > 0) {
                            result +=
                                '<p style="padding-left: 180px; line-height: 24px; color: #222;">' + lawResource.getMessageByName('Promotion') + ':<strong style="color:#a67942;"> ' +
                                lawsVn.formatNumber(data.Data.Amount, '.', '.') +
                                ' VNĐ</strong></p>';
                        }
                    }
                    $('#promotionCodeCheckResult').lawsExists(function () {
                        $(this).html(result);
                    });
                    $('#PromotionCodeBankAccount').lawsExists(function () {
                        $(this).val(data.Data.PromotionDesc);
                    });
                    $('#PromotionCodeScratchCard').lawsExists(function () {
                        $(this).val(data.Data.PromotionDesc);
                    });
                    if (data.Data.Amount > 0) {
                        $('#Amount').lawsExists(function () {
                            $(this).val(data.Data.Amount);
                        });
                    } else {
                        $('#PercentDecrease').lawsExists(function () {
                            $(this).val(data.Data.PercentDecrease);
                        });
                    }
                    $('.promotion-td').lawsExists(function () {
                        $(this).text(data.Data.Amount > 0
                            ? lawsVn.formatNumber(data.Data.Amount, '.', '.')
                            : (data.Data.PercentDecrease > 0
                                ? lawsVn.formatNumber(data.Data.CrUserId * data.Data.PercentDecrease / 100, '.', '.')
                                : ''));
                    });
                    $('#tdTotal').lawsExists(function () {
                        var vat = data.Data.CrUserId * 10 / 100;
                        var pricePromotion = data.Data.Amount > 0
                            ? data.Data.Amount
                            : (data.Data.PercentDecrease > 0
                                ? data.Data.CrUserId * data.Data.PercentDecrease / 100
                                : 0);
                        var total = data.Data.CrUserId + vat - pricePromotion;
                        $(this).text(lawsVn.formatNumber(total, '.', '.'));
                    });
                }
            } else {
                if (data.Message !== void 0 && data.Message.length > 0) {
                    $().lawsDialog({
                        messages: [data.Message]
                    });
                }
                $('#promotionCodeCheckResult').empty();
                $('#PromotionCodeBankAccount').lawsExists(function () {
                    $(this).val('');
                });
                $('#PromotionCodeScratchCard').lawsExists(function () {
                    $(this).val('');
                });
                //tính lại tổng tiền
                $('.promotion-td').lawsExists(function () {
                    $(this).text('0');
                });
                $('#tdTotal').lawsExists(function () {
                    var vat = data.Data.CrUserId * 10 / 100;
                    var total = data.Data.CrUserId + vat;
                    $(this).text(lawsVn.formatNumber(total, '.', '.'));
                });
            }
        },
        TaxInvoiceOnComplete: function (data, status, xhr) {
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
                else if (data.Message != null && data.Message.length > 0) {
                    $().lawsDialog({
                        dialogClass: 'lawsVnDialogTitle',
                        messages: [data.Message],
                        showIcon: false
                    });
                }
            } else if (data.Message != null && data.Message.length > 0) {
                $().lawsDialog({
                    dialogClass: 'lawsVnDialogTitle',
                    messages: [data.Message],
                    showIcon: false
                });
            }
        },
        OnSuccessValid: function (element) {
            $('#loading').fadeOut('normal');
            $('#' + element).lawsExists(function () {
                var form = $('#' + element);
                form.removeData('validator');
                form.removeData('unobtrusiveValidation');
                $.validator.unobtrusive.parse(form);
            });
        },
        SearchOnSuccess: function (data, status, xhr) {
            lawsVn.searchOnSuccess();
        }
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
    setTitleAndHistory: function (title, path) {
        // Set history
        if (window.history && window.history.pushState) {
            history.pushState(null, title, path);
        }
        // Set title
        $('title').html(title);
    },
    changePassword: function () {
        $('<div id="changePassword"></div>').lawsDialog({
            title: '',
            width: 300,
            buttons: {},
            hideClose: false,
            closeText: lawsVnConfig.close,
            onCreate: function () {
                $('#loading').fadeIn('normal');
            },
            onOpen: function () {
                $('#changePassword').load(lawsVnConfig.rootPath + 'Ajax/PartialChangePassword',
                    function () {
                        var form = $('#lawsVnChangePasswordForm');
                        form.removeData('validator');
                        form.removeData('unobtrusiveValidation');
                        $.validator.unobtrusive.parse(form);
                    });
                $('#loading').fadeOut('normal');
            }
        });
    },
    forgotPassword: function () {
        $('<div id="lawForgotPassword"></div>').lawsDialog({
            title: '',
            width: 400,
            buttons: {},
            hideClose: false,
            closeText: lawsVnConfig.close,
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
    logOut: function () {
        $().lawsDialog({
            dialogClass: 'lawsVnDialogTitle',
            messages: [lawResource.getMessageByName('DoYouWantToLogOut')],
            buttons: [
                {
                    text: lawResource.getMessageByName('Cancel'),
                    'class': 'btn-thongbao1',
                    click: function () {
                        $(this).dialog('close');
                    }
                },
                {
                    text: lawResource.getMessageByName('Agree'),
                    click: function () {
                        $(this).dialog('close');
                        window.location.href = lawsVn.virtualPath('/user/logout.html');
                    }
                }
            ]
        });
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
            var pageIndex = $(this).data('pageindex');
            var pageCount = $(this).data('pagecount');
            //var indexFrom = $(this).find(' th:first').text();
            //var indexTo = $(this).find('th:last').text();
            var data = '';
            if (typeof totalpage == 'undefined') {
                totalpage = 0;
            }
            if (typeof pageIndex == 'undefined') {
                pageIndex = 1;
            }
            if (totalpage > 0) {
                if (keywords.length > 0) {
                    data = " <strong>"+ lawResource.getMessageByName('tim-thay') +" <span class='color2'>" +
                        totalpage +
                        "</span> " + lawResource.getMessageByName('ket-qua-theo-tu-khoa') + " \"<span class='color2'>" +
                        keywords +
                        "</span>\"</strong>";
                } else {
                    data = " <strong>" + lawResource.getMessageByName('tim-thay') + " <span class='color2'>" +
                        totalpage +
                        "</span> " + lawResource.getMessageByName('ketqua') + "</strong>";
                }
                data += ' (' + (pageIndex + 1) + '/' + pageCount + lawResource.getMessageByName('page') + ')';
                $('#txtnumberresultfound').lawsExists(function() {
                    $(this).html(data);
                });
            }
        });
        $('.resultsFilterBy').lawsExists(function () {
            if (lawsVnConfig.arrLinhvuctracuu.length == 0 &&
                lawsVnConfig.arrCoquanbanhanh.length == 0 &&
                lawsVnConfig.arrTrangthaihieuluc.length == 0 &&
                lawsVnConfig.arrLoaivanban.length == 0 & lawsVnConfig.arrNambanhanh.length == 0) {
                $('.resultsFilterBy').hide();
            }
        });
    },
    gopy: function (docName) {
        $('<div id="gopy"></div>').lawsDialog({
            title: '',
            width: 600,
            buttons: {},
            hideClose: false,
            onCreate: function () {
                $('#loading').fadeIn('normal');
            },
            onOpen: function () {
                $('#gopy').load(lawsVn.virtualPath('/Ajax/PartialGopY'),
                    function () {
                        $('#FormGopY #Title').lawsExists(function () {
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
            width: 400,
            buttons: {},
            hideClose: false,
            closeText: 'Đóng',
            onCreate: function () {
                $('#loading').fadeIn('normal');
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
    RegisterAccount: function () {
        lawsValidate.termsAndConditions();
        lawsValidate.multiCheckboxRequired();
        lawsValidate.fileType();
        lawsValidate.maxFileSize();
        //lawsValidate.age();
        lawsValidate.formatDate();
    },
    FormReset: function () {
        $('#RegisterForm')[0].reset();
        return false;
    },
    ResetChangePass: function () {
        $('#ForgotPasswordForm')[0].reset();
        return false;
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
    ResetForm: function (form) {
        form.find('input:text, input:password, input:file, select, textarea').val('');
        form.find('input:radio, input:checkbox')
            .removeAttr('checked').removeAttr('selected');
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
        deleteCustomerDocs: function (docGroupId, docId) {
            $().lawsDialog({
                dialogClass: 'lawsVnDialogTitle',
                messages: [lawResource.getMessageByName('AreYouSureYouWantToDeleteItem')],
                buttons: [
                    {
                        text: lawResource.getMessageByName('Close'),
                        'class': 'btn-thongbao1',
                        click: function () {
                            $(this).dialog('close');
                        }
                    },
                    {
                        text: lawResource.getMessageByName('Agree'),
                        click: function () {
                            $(this).dialog('close');
                            $.lawsAjax({
                                url: lawsVnConfig.rootPath + 'Ajax/DeleteDocument',
                                data: { docGroupId: docGroupId, docId: docId },
                                success: function (resp) {
                                    if (resp.Message !== void 0 && resp.Message.length > 0) {
                                        $().lawsDialog({
                                            dialogClass: 'lawsVnDialogTitle',
                                            messages: [resp.Message],
                                            onClose: function () {
                                                if (resp.Completed &&
                                                    resp.ReturnUrl !== void 0 &&
                                                    resp.ReturnUrl.length > 0) {
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
        subscriptionNoticeOfValidity: function (docId) {
            $.lawsAjax({
                url: lawsVnConfig.rootPath + 'Ajax/SubscriptionNoticeOfValidity',
                data: { docId: docId },
                success: function (resp) {
                    if (resp.Message !== void 0 && resp.Message.length > 0) {
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
    registerServiceFormReset: function () {
        var countSteps = $('div.law-steps', '.law-wizard').length;
        var i = countSteps == 3 ? 1 : (countSteps == 4 ? 2 : 1);
        $('div.law-steps', '.law-wizard').animate({
            opacity: 0
        }, 250, function () {
            $('div.law-steps' + ':nth-child(' + i + ')', '.law-wizard').animate({
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
    successfulNewsletter: function (text) {
        if (text !== void 0 && text.length) {
            $().lawsDialog({
                dialogClass: 'lawsVnDialogTitle',
                messages: [text],
                showIcon: false,
                onClose: function () {
                    $('#Email').val('');
                }
            });
        }
    },
    topFunction: function () {
        document.body.scrollTop = 0;
        document.documentElement.scrollTop = 0;
    },
    scrollFunction: function () {
        if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
            document.getElementById("myBtn").style.display = "block";
        } else {
            document.getElementById("myBtn").style.display = "none";
        }
    },
    ListbyFieldOrderBy: function (fieldId) {
        var OrderbyName = $("#dllOrderbyName option:selected").val();
        var Orderby = $("#dllOrderby option:selected").val();
        var Order = OrderbyName + " " + Orderby;
        var url = lawsVnConfig.rootPath + 'Ajax/Docs_ViewSearch';
        var dataGetter = { 'orderBy': Order, 'fieldId': fieldId };
        $.lawsVnAjax(url,
            'Get',
            dataGetter,
            function (resp) {
                $("#ListByField").html(resp);
            });
    },
    validateForm: function () {
        return true;
    },
    virtualPath: function (patch) {
        var host = window.location.protocol + '//' + window.location.host;
        return host + patch;
    },
    logs: {
        articleLogs: function (articleId, categoryId) {
            $.lawsAjax({
                url: lawsVn.virtualPath('/Ajax/ArticleLogs'),
                data: { articleId: articleId, categoryId: categoryId },
                beforeSend: {},
                success: function (resp) {
                    if (!resp.Completed) {
                        if (resp.Message !== null && resp.Message.length > 0) {
                            console.log('articleLogs: ' + resp.Message);
                        }
                    }
                }
            });
        }, docViewLogs: function (docId, docGroupId, actionTypeId) {
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
            var fieldId = lawsVnConfig.arrLinhvuctracuu.length > 0 ? lawsVnConfig.arrLinhvuctracuu[lawsVnConfig.arrLinhvuctracuu.length - 1].id : 0;//  lawsVn.search.getId('linhvuctracuuValue');
            var organId = lawsVnConfig.arrCoquanbanhanh.length > 0 ? lawsVnConfig.arrCoquanbanhanh[lawsVnConfig.arrCoquanbanhanh.length - 1].id : 0;// lawsVn.search.getId('coquanbanhanhValue');
            var effectStatusId = lawsVnConfig.arrTrangthaihieuluc.length > 0 ? lawsVnConfig.arrTrangthaihieuluc[lawsVnConfig.arrTrangthaihieuluc.length - 1].id : 0;// lawsVn.search.getId('trangthaihieulucValue');
            var docTypeId = lawsVnConfig.arrLoaivanban.length > 0 ? lawsVnConfig.arrLoaivanban[lawsVnConfig.arrLoaivanban.length - 1].id : 0;//lawsVn.search.getId('loaivanbanValue');
            var year = lawsVnConfig.arrNambanhanh.length > 0 ? lawsVnConfig.arrNambanhanh[lawsVnConfig.arrNambanhanh.length - 1].id : 0;//lawsVn.search.getId('nambanhanhValue');
            var orderbyName = $("#dllOrderbyName option:selected").val();
            var orderby = $("#dllOrderby option:selected").val();
            var order = orderbyName + " " + orderby;
            var dateFrom = $('#DateFrom').val();
            var signerName = $('#SignerName').val();
            var dateTo = $('#DateTo').val();
            var keywords = $('#Keywords').val();
            var languageId = $('select[name="LanguageId"] option:selected').val();
            var signerId = $('#SignerId').val();
            var docGroupId = $('#DocGroupId').val();
            var orderBy = order;
            var isSearchExact = 0;
            if ($('#SearchExact').is(":checked"))
            {
                isSearchExact = 1;
            }
            var organIdSelected = $('select[name="OrganId"] option:selected').val();
            var fieldIdSelected = $('select[name="FieldId"] option:selected').val();
            var effectStatusIdSelected = $('select[name="EffectStatusId"] option:selected').val();
            var docTypeIdSelected = $('select[name="DocTypeId"] option:selected').val();
            if (organIdSelected > 0) organId = organIdSelected;
            if (fieldIdSelected > 0) fieldId = fieldIdSelected;
            if (effectStatusIdSelected > 0) effectStatusId = effectStatusIdSelected;
            if (docTypeIdSelected > 0) docTypeId = docTypeIdSelected;
            var searchOptions = $("input[name=SearchOptions]:checked").val();
            $.lawsAjax({
                url: lawsVnConfig.rootPath + 'Ajax/Docs_GetViewSearchWithKeyword',
                type: 'Post',
                dataType: 'html',
                data: {
                    keywords: keywords,
                    docGroupId: docGroupId,
                    searchOptions: searchOptions,
                    dateFrom: dateFrom,
                    dateTo: dateTo,
                    languageid: languageId,
                    signerId: signerId,
                    signerName : signerName,
                    fieldId: fieldId,
                    effectStatusId: effectStatusId,
                    organId: organId,
                    docTypeId: docTypeId,
                    year: year,
                    orderBy: orderBy,
                    isSearchExact : isSearchExact
                },
                success: function (resp) {
                    $("#ListDocsViews").html(resp);
                }
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
        deleteCustomerDocs: function () {
            var listdocId = '';
            var docGroupId = 1;
            var docsId = 0;
            $('input[name="checkboxDocument"]:checked').each(function () {
                if (listdocId == '') {
                    listdocId = this.value;
                }
                else {
                    listdocId += ',' + this.value;
                }
                docsId++;
            });

            if (!listdocId.length) {
                $().lawsDialog({
                    dialogClass: 'lawsVnDialogTitle',
                    messages: [lawResource.getMessageByName('chua-chon-vb-quantam-can-xoa')]
                });
                return;
            }

            $().lawsDialog({
                dialogClass: 'lawsVnDialogTitle',
                messages: [lawResource.getMessageByName(docsId === 1 ? 'AreYouSureYouWantToDeleteItem' : 'AreYouSureYouWantToDeleteItems')],
                buttons: [
                    {
                        dialogClass: 'lawsVnDialogTitle',
                        text: lawResource.getMessageByName('Cancel'),
                        'class': 'btn-thongbao1',
                        click: function () {
                            $(this).dialog('close');
                        }
                    },
                    {
                        text: lawResource.getMessageByName('Agree'),
                        click: function () {
                            $(this).dialog('close');
                            $.lawsAjax({
                                url: lawsVnConfig.rootPath + 'Ajax/DeleteDocument',
                                data: { docGroupId: docGroupId, listdocID: listdocId, docsId: docsId },
                                success: function (resp) {
                                    if (resp.Message !== void 0 && resp.Message.length > 0) {
                                        $().lawsDialog({
                                            dialogClass: 'lawsVnDialogTitle',
                                            messages: [resp.Message],
                                            showIcon: false,
                                            onClose: function () {
                                                if (resp.Completed &&
                                                    resp.ReturnUrl !== void 0 &&
                                                    resp.ReturnUrl.length > 0) {
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
        deleteCustomerDocsAll: function () {
            var listdocId = '';
            var docGroupId = 1;
            var docsId = 0;
            $('input[name="checkboxDocument"]').each(function () {
                if (listdocId == '') {
                    listdocId = this.value;
                }
                else {
                    listdocId += ',' + this.value;
                }
                docsId++;
            });
            if (!listdocId.length) {
                $().lawsDialog({
                    dialogClass: 'lawsVnDialogTitle',
                    messages: [lawResource.getMessageByName('chua-chon-vb-quantam-can-xoa')]
                });
                return;
            }
            $().lawsDialog({
                dialogClass: 'lawsVnDialogTitle',
                messages: [lawResource.getMessageByName('AreYouSureYouWantToDeleteAllItem')],
                buttons: [
                    {
                        dialogClass: 'lawsVnDialogTitle',
                        text: lawResource.getMessageByName('Cancel'),
                        'class': 'btn-thongbao1',
                        click: function () {
                            $(this).dialog('close');
                        }
                    },
                    {
                        text: lawResource.getMessageByName('Agree'),
                        click: function () {
                            $(this).dialog('close');
                            $.lawsAjax({
                                url: lawsVn.virtualPath('/Ajax/DeleteDocument'),
                                data: { docGroupId: docGroupId, listdocID: listdocId, docsId: docsId },
                                success: function (resp) {
                                    if (resp.Message != null && resp.Message.length > 0) {
                                        $().lawsDialog({
                                            dialogClass: 'lawsVnDialogTitle',
                                            messages: [resp.Message],
                                            onClose: function () {
                                                if (resp.Completed &&
                                                    resp.ReturnUrl != null &&
                                                    resp.ReturnUrl.length > 0) {
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
        subscriptionNoticeOfValidity: function (docId) {
            $.lawsAjax({
                url: lawsVnConfig.rootPath + 'Ajax/SubscriptionNoticeOfValidity',
                data: { docId: docId },
                success: function (resp) {
                    if (resp.Message !== void 0 && resp.Message.length > 0) {
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
    FontZoom: function (zoomValue) {
        var curSize = parseInt($('div.content-entry').css('font-size')) + zoomValue;
        var curLineHeight = parseInt($('div.content-entry span').css('line-height'));
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
        $('div.content-entry,div.content-entry *').attr('style',
            function () { return 'font-size: ' + curSize.toString() + 'px' + ' !important;' + '; line-height: ' + curLineHeight.toString() + 'px' + ' !important;' });
    },
    SaveDoc: function (docId) {
        $.lawsAjax({
            url: lawsVnConfig.rootPath + 'Ajax/SaveDocument',
            data: { docId: docId },
            beforeSend: {},
            success: function (resp) {
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
    InterestedDocumentsOrder: function () {
        var OrderbyName = $("#dllOrderbyName option:selected").val();
        var Orderby = $("#dllOrderby option:selected").val();
        var Order = OrderbyName + " " + Orderby;
        var url = lawsVnConfig.rootPath + 'Ajax/MyDocuments_GetPage';
        var dataGetter = { 'orderBy': Order };
        $.lawsVnAjax(url,
            'Get',
            dataGetter,
            function (resp) {
                $("#MyDocumentsBox").html(resp);
            });
    },
    login: function () {
        $('<div id="lawsVnLogin"></div>').lawsDialog({
            title: '',
            width: 300,
            buttons: {},
            hideClose: true,
            closeText: lawsVnConfig.close,
            onCreate: function () {
                $('#loading').fadeIn('normal');
            },
            onOpen: function () {
                $('#lawsVnLogin').load(lawsVnConfig.rootPath + 'Ajax/PartialLogin',
                    function () {
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
    ShowCountNumber: function () {
        if ($('#txtnumberresultfound').length && $('#tblcontent').length)
        {
            var totalpage = $("#tblcontent").attr('data-totalpage');
            var pageindex = $("#tblcontent").attr('data-pageindex');
            var pagecount = $("#tblcontent").attr('data-pagecount');
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
            if (pagecount > 0) {
                data += "<span>" + lawsVnConfig.Find + "  <strong> " +
                    totalpage +
                    " result  </strong>(" +
                    pageindex +
                    "/" +
                    pagecount +
                    " page)";
            } else data = "<span>" + lawsVnConfig.NotFound+ ".</strong>";
            data += "</span>";
            $("#txtnumberresultfound").html(data);
        }
    },
    TermsConditionsView: function () {
        $('<div id="lawsVnTermsConditionsView"></div>').lawsDialog({
            title: '',
            width: 890,
            position: { my: "center", at: "top+50", of: window.top },
            buttons: {},
            hideClose: false,
            closeText: lawResource.getMessageByName('Close'),
            onCreate: function () {
                $('#loading').fadeIn('normal');
            },
            onOpen: function () {
                $('#lawsVnTermsConditionsView').load(lawsVn.virtualPath('/Ajax/PartialTermsConditionsView'),
                    function () {
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
    }
};
(function ($) {
    $.fn.lawsDialog = function (options) {
        var defaultOptions = {
            title: lawResource.getMessageByName('Message'),
            width: 'auto',
            height: 'auto',
            minWidth: 'auto',
            minHeight: 'auto',
            resizable: false,
            autoOpen: true,
            modal: true,
            show: { effect: 'fade', duration: 250 },
            hide: { effect: 'fade', duration: 250 },
            closeText: lawResource.getMessageByName('Close'),
            position: { my: "center", at: "top+150", of: window.top },
            dialogClass: 'lawsVnDialog',
            buttons: null,
            onCreate: {},
            onOpen: {},
            onClose: {},
            hideClose: true,
            showIcon: false, //hiện icon chuông hay ko
            isDestroy:true,
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
                    text: lawResource.getMessageByName('Close'),
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
                //setTimeout(function () {
                //    $(self).closest(".ui-dialog").find(":button").blur();
                //    $(this).parents('.ui-dialog-buttonpane').find('button:eq(0)').focus();
                //}, 420);
                if (options.hideClose && options.title === '') {
                    $(this).siblings('.ui-dialog-titlebar').remove();
                }
                //ẩn nút đóng x
                else if (options.hideClose) {
                    $(self).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
                //loại bỏ khi tiêu đề trống
                else if (options.title === '') {
                    $(this).dialog("widget").find(".ui-dialog-title").remove();
                }
                $('.ui-widget-overlay').bind('click',
                    function () {
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
        if (self.dialog('isOpen') === false) {
            self.dialog('open');
        }
    }
})(jQuery);
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
    },

}
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
                        messages: [lawResource.getMessageByName('MessageErrorNetwork')]
                        , showIcon: false
                    });
                } else if (jqXhr.status === 404) {
                    $().lawsDialog({
                        messages: [lawResource.getMessageByName('PageNotFound')]
                        , showIcon: false
                    });
                } else if (jqXhr.status === 500) {
                    $().lawsDialog({
                        messages: [lawResource.getMessageByName('ServerError')]
                        , showIcon: false
                    });
                } else if (errorMessage === 'parsererror') {
                    $().lawsDialog({
                        messages: [lawResource.getMessageByName('ErrorParserJson')]
                        , showIcon: false
                    });
                } else if (errorMessage === 'timeout') {
                    $().lawsDialog({
                        messages: [lawResource.getMessageByName('TimeOut')]
                        , showIcon: false
                    });
                } else if (errorMessage === 'abort') {
                    $().lawsDialog({
                        messages: [lawResource.getMessageByName('ProcessingRequestCanceled')]
                        , showIcon: false
                    });
                } else if (jqXhr.status !== 403) {
                    $().lawsDialog({
                        messages: [lawResource.getMessageByName('Error') +':.n' + jqXhr.responseText]
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
                $('#loading').fadeIn('normal');
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
                $('#loading').fadeOut('normal');
            },
            success: function (data, status, xhr) {
                window.setTimeout(function () {
                    execOnSuccess(data);
                }, 10);
                $('#loadmore').prop('disabled', true).css('cursor', 'default').text('Xem thêm');
                $('#loading').fadeOut('normal');
            }
        });
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
                var index = proc.getIndexDoc(options.doc.id);
                if (index < 0) {
                    proc.addDoc();
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
                el.append(
                    '<section class="view-post-content">' +
                        '<div class="padding-view">' +
                            '<div class="cat-title">' +
                                '<h4><span class="cat-box-title vien">' + lawResource.getMessageByName('noi-dung-da-xem') + '</span></h4>' +
                            '</div>' +
                            '<div id="owl-demo-c3" class="owl-carousel"></div>' +
                            '<div class="customNavigation">' +
                                '<a class="btn prev33 btn1"></a>' +
                                '<a class="btn next33 btn2"></a>' +
                            '</div>' +
                        '</div>' +
                    '</section>'
                );
                for (var i = docs.length - 1; i >= 0; i--) {
                    $('#owl-demo-c3').append(
                        '<div class="item-recent-post">' +
                            '<div class="post-thumbnail wg">' +
                                '<a href="' + docs[i].url + '" title="' + docs[i].title + '" class="thumb80"><img alt="thumb90" src="' + docs[i].image + '"></a>' +
                            '</div>' +
                            '<div class="recent-view">' +
                                '<a href="' + docs[i].url + '" title="' + docs[i].title + '" class="post-title normal">' + docs[i].name + '</a>' +
                                '<p class="tie-date daxem"> (' + docs[i].datetime + ')</p>' +
                            '</div>' +
                        '</div>'
                    );
                }
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

$.fn.extend({
    LawScrollTo: function () {
        var x = $(this).offset().top - 100;
        jQuery('html,body').animate({ scrollTop: x }, 100);
    }
});

(function ($) {
    $.fn.lawServicesWizard = function (options) {
        var defaults = {
            itemNav: '.navstep',
            navStep: '.nav-step',
            itemstep: 'span.itemstep',
            divSteps: 'div.law-steps',
            backStep: '.back-step',
            nextStep: '.next-step',
            duration: 50,
            validate: true,
            fnValidate: function () {
                var wizard = $('.law-wizard');
                var servicePackageParentId = $('input[name="ServicePackageParentId"]:checked', wizard).map(function () {
                    return $(this).val();
                }).get();
                var form = wizard.lawFields();
                var servicePackageId = form.ServicePackageId;
                if (servicePackageParentId.length > 0 && servicePackageId > 0) {
                    if (servicePackageParentId.length > 0) {
                        $('td.center-post', wizard).lawsExists(function () {
                            $(this).removeClass('border-warning');
                        });
                    }
                    if (servicePackageId > 0) {
                        $('select[name="ServicePackageId"]', wizard).lawsExists(function () {
                            $(this).removeClass('border-warning');
                        });
                    }
                    return true;
                }
                else if (servicePackageParentId == 0) {
                    $('td.center-post', wizard).lawsExists(function () {
                        $(this).addClass('border-warning');
                    });
                    $().lawsDialog({
                        title: lawResource.getMessageByName('Message'),
                        dialogClass: 'lawsVnDialogTitle',
                        messages: [lawResource.getMessageByName('chon-so-nguoi-su-dung')],
                        onClose:function() {
                            $('.tie-step-cat').LawScrollTo();
                        }
                    });
                } else if (servicePackageId == 0) {
                    $('select[name="ServicePackageId"]',wizard).lawsExists(function () {
                        $(this).addClass('border-warning');
                    });
                    $().lawsDialog({
                        title: lawResource.getMessageByName('Message'),
                        dialogClass: 'lawsVnDialogTitle',
                        messages: [lawResource.getMessageByName('chon-thoi-han-thue-bao')],
                        onClose: function () {
                            $('.tiechooising').LawScrollTo();
                        }
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
                }, options.duration, function () {
                    $(options.divSteps + ':nth-child(' + index + ')', el).animate({
                        opacity: 1
                    }, options.duration, function () {
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

(function() {
    $.fn.lawFields = function (agrs) {
        var el = this.find(':input').get();
        //trường hợp ko có tham số truyền vào -> GET
        if (arguments.length === 0) {
            var results = {};
            var fields = {};
            $.each(el,
                function() {
                    if (this.name &&
                        !this.disabled &&
                        (this.checked ||
                            /select|textarea/i.test(this.nodeName) ||
                            /text|hidden|password/i.test(this.type))) {
                        if (results[this.name] == undefined) {
                            results[this.name] = [];
                        }
                        results[this.name].push(fields[this.name] = $(this).val());
                    }
                });
            return fields;
        }
            //trường hợp ko có tham số truyền vào -> SET
        else {
            $.each(el,
                function() {
                    if (this.name && agrs[this.name]) {
                        var names = agrs[this.name]; 
                        var self = $(this);
                        if (Object.prototype.toString.call(names) !== '[object Array]') { // chuyen doi gia tri sang Array
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
                            self.attr("checked", found);
                        } else {
                            self.val(names[0]);
                        }
                    }
                });
            return this;
        }
    };
})(jQuery);

var lawsInfo = function () {
    console.log("%c LuatVietnam.Vn - Cơ sở dữ liệu văn bản pháp luật lớn nhất Việt Nam. %c \n Bản quyền \xa9 2000-2017 bởi LuatVietNam - Thành viên INCOM Communications ., JSC \n Giấy phép thiết lập trang Thông tin điện tử tổng hợp số: 692/GP-TTĐT cấp ngày 29/10/2010 bởi Sở TT-TT Hà Nội, thay thế giấy phép số: 322/GP - BC, ngày 26/07/2007, cấp bởi Bộ Thông tin và Truyền thông \n" +
        "Chứng nhận bản quyền tác giả số 280/ 2009 / QTG ngày 16/ 02 / 2009, cấp bởi Bộ Văn hoá - Thể thao - Du lịch \n" +
        "Cơ quan chủ quản: Công ty Cổ phần Truyền thông Quốc tế INCOM.Chịu trách nhiệm: Ông Vũ Mạnh Cường \n" +
        "Giấy chứng nhận đăng ký DN số: 0102011152, do Sở Kế hoạch và Đầu tư Hà Nội cấp ngày 11/ 08 / 2006.\n" +
        "Địa chỉ: Tầng 3, Tòa nhà IC, 82 phố Duy Tân, Cầu Giấy, Hà Nội.\n" +
        "Điện thoại: (04) 37833688 - Fax: (04) 37833699 * Hỗ trợ: 1900561589 * Email: lawdata@luatvietnam.vn", 'font-family: "Helvetica Neue", Helvetica, Arial, sans-serif;font-size:24px;color:#a67942;-webkit-text-fill-color:#a67942;-webkit-text-stroke: 1px #a67942;', "font-size:12px;color:#000;");
}

function openCity(tabName, tabClass) {
    var i;
    var x = document.getElementsByClassName(tabClass);
    for (i = 0; i < x.length; i++) {
        x[i].style.display = "none";
    }
    document.getElementById(tabName).style.display = "block";
}

var lawResource = {
    getCulture: function () {
        var culture = $.cookie(lawsVnConfig.CultureName);
        if (typeof culture == 'undefined') {
            culture = 'en';
        }
        return culture;
    },
    designer: Array(
        { msgName: 'Message', culture: { en: 'Message', vi: 'Thông báo' } },
        { msgName: 'Close', culture: { en: 'Close', vi: 'Đóng' } },
        { msgName: 'Agree', culture: { en: 'Agree', vi: 'Đồng ý' } },
        { msgName: 'Cancel', culture: { en: 'Cancel', vi: 'Hủy' } },
        { msgName: 'noi-dung-da-xem', culture: { en: 'WATCHED CONTENTS', vi: 'NỘI DUNG ĐÃ XEM' } },
        { msgName: 'TimeOut', culture: { en: 'TimeOut', vi: 'Hết thời gian yêu cầu.' } },
        { msgName: 'PageNotFound', culture: { en: 'PageNotFound', vi: 'Không tìm thấy trang yêu cầu.' } },
        { msgName: 'ErrorParserJson', culture: { en: 'Error Parser Json', vi: 'Yêu cầu phân tích cú pháp JSON lỗi.' } },
        { msgName: 'Error', culture: { en: 'Error', vi: 'Lỗi :' } },
        { msgName: 'PleaseCheckTheInformationWithRedWarning', culture: { en: 'Please check the information with red warning.', vi: 'Quý khách vui lòng kiểm tra lại các thông tin có cảnh báo màu đỏ.' } },
        { msgName: 'ServerError', culture: { en: 'ServerError', vi: 'Lỗi máy chủ nội bộ.' } },
        { msgName: 'AreYouSureYouWantToDeleteItem', culture: { en: 'Do you want to delete the selection?', vi: 'Quý khách có chắc muốn xóa ?' } },
        { msgName: 'AreYouSureYouWantToDeleteItems', culture: { en: 'Do you want to delete the selections?', vi: 'Quý khách có chắc muốn xóa ?' } },
        { msgName: 'AreYouSureYouWantToDeleteAllItem', culture: { en: 'Are you sure you want to delete all items?', vi: 'Quý khách có chắc muốn xóa tất cả ?' } },
        { msgName: 'DoYouWantToLogOut', culture: { en: 'Are you sure you want to log out?', vi: 'Quý khách muốn đăng xuất khỏi hệ thống ?' } },
        { msgName: 'ProcessingRequestCanceled', culture: { en: 'Processing request canceled.', vi: 'Yêu cầu xử lý bị hủy.' } },
        { msgName: 'Find', culture: { en: 'Find', vi: 'Tìm thấy' } },
        { msgName: 'NotFound', culture: { en: 'Not Found', vi: 'Không tìm thấy trang yêu cầu.' } },
        { msgName: 'YourServiceOrderCode', culture: { en: 'Your service order code', vi: 'Mã đơn hàng dịch vụ của Quý khách' } },
        { msgName: 'SignerNotFound', culture: { en: 'Signer not found.', vi: 'Không tìm thấy người ký phù hợp.' } },
        { msgName: 'PromotionalCodeInformation', culture: { en: 'Promotional code information:', vi: 'Thông tin mã khuyến mại:' } },
        { msgName: 'Promotion', culture: { en: 'Promotion', vi: 'Khuyến mại:' } },
        { msgName: 'Months', culture: { en: ' months', vi: ' tháng' } },
        { msgName: 'Days', culture: { en: ' days', vi: ' ngày' } },
        { msgName: 'chon-thoi-han-thue-bao', culture: { en: 'Please select the subscription period.', vi: 'Quý khách vui lòng chọn thời hạn thuê bao.' } },
        { msgName: 'chon-so-nguoi-su-dung', culture: { en: 'Please select the number of users.', vi: 'Quý khách vui lòng chọn số người sử dụng.' } },
        { msgName: 'gia-han', culture: { en: 'Renewal', vi: 'Gia hạn' } },
        { msgName: 'chuyen-doi', culture: { en: 'Conversion', vi: 'Chuyển đổi' } },
        { msgName: '23', culture: { en: 'Advanced search', vi: 'Tra cứu nâng cao' } },
        { msgName: '17', culture: { en: 'Lookup English', vi: 'Tra cứu tiếng Anh' } },
        { msgName: 'chon-dich-vu-chuyen-doi', culture: { en: 'Select the service to convert?', vi: 'Lựa chọn dịch vụ cần chuyển đổi ?' } },
        { msgName: 'page', culture: { en: ' page', vi: ' trang' } },
        { msgName: 'tim-thay', culture: { en: ' Find', vi: ' Tìm thấy' } },
        { msgName: 'ketqua', culture: { en: ' result', vi: ' kết quả' } },
        { msgName: 'ket-qua-theo-tu-khoa', culture: { en: ' results by keyword', vi: 'kết quả theo từ khóa' } },
        { msgName: 'dang-ky-dich-vu', culture: { en: ' Register', vi: 'Đăng ký dịch vụ' } },
        { msgName: 'dang-nhap', culture: { en: ' Login', vi: 'Đăng nhập' } },
        { msgName: 'chua-chon-vb-quantam-can-xoa', culture: { en: 'Please, select item to remove.', vi: 'Quý khách vui lòng chọn văn bản quan tâm cần xóa.' } },
        { msgName: 'gia-dich-vu', culture: { en: 'To sign up for the text search service on LuatVietnam.vn, please <br/> login to Vietnam Law Account and make registration according to instructions.<br/><span style="font-weight:normal;color:#a67942;">If you do not have an account? Click <a href="' + lawsVn.virtualPath('/user/subscriber.html') + '" title="Register Account" style="color:#a67942;"><b>Register Account</b></a>.</span>', vi: 'Để đăng ký dịch vụ Tra cứu văn bản trên LuatVietnam.vn, Quý khách vui lòng <br/> đăng nhập tài khoản Luật Việt Nam và thực hiện đăng ký theo hướng dẫn.<br/><span style="font-weight:normal;color:#a67942;">Nếu Quý khách chưa có tài khoản? Bấm vào <a href="' + lawsVn.virtualPath('/user/subscriber.html') + '" title="Đăng ký tài khoản" style="color:#a67942;"><b>Đăng ký tài khoản</b></a>.</span>' } }
    ),
    getMessageByName: function (msgName) {
        var culture = this.getCulture();
        var retval = $.map(this.designer, function (value, key) {
            if (value.msgName == msgName) {
                return value.culture[culture];
            }
        });
        return retval.length > 0 ? retval[0] : '';
    }
};