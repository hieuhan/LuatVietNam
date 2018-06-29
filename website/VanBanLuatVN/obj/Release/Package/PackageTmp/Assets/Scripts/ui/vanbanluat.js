$(function() {
    vbluat.init();
    $(document).ajaxError(function (e, xhr) { 
        if (xhr.status === 403) {
            var response = $.parseJSON(xhr.responseText);
            if (response.ReturnUrl != null && response.ReturnUrl.length > 0) {
                window.location.href = response.ReturnUrl;
            }else
                window.location.href = vbluat.virtualPath('/user/dang-nhap.html?ReturnUrl=/');
        }
    });
});
var configs = {
    rootPath: '/'
}
var vbluat = {
    init: function() {
        this.events();
        VbLuatValidate.termsAndConditions();  
    },
    events: function () {
        vbluat.scrollTop();
        if ($('[data-toggle-target]').length) {
            $('[data-toggle-target]').each(function() {
                $(this).toggleTarget();
            });
        }
        var hasFieldIds = false;

        $('.inputPlaceholder').inputPlaceholder();

        $(document).mouseup(function(e) {
            var self = $('.sidebar-nav');
            var overlay = self.closest('div.sidebar-overlay');
            var closebtn = overlay.find('button');
            if (!self.is(e.target) &&
                self.has(e.target).length === 0 &&
                !closebtn.is(e.target) &&
                closebtn.has(e.target).length === 0) {
                overlay.removeClass('target-expanded').hide(300);
            }
        });
        $(document).on('change',
            'form .select-onchange',
            function(event) {
                event.preventDefault();
                $(this).closest('form').trigger('submit');
            });
        $('.dropdown-toggle').on('click',
            function(e) {
                e.preventDefault();
                var isHidden = $(this).parents('.button-dropdown').children('.dropdown-menu').is(':hidden');
                $('.button-dropdown .dropdown-menu').hide();
                if (isHidden) {
                    $(this).parents('.button-dropdown').children('.dropdown-menu').toggle().parents('.button-dropdown')
                        .children('.dropdown-toggle').addClass('active');
                }
            });

        $(document).bind('click',
            function(e) {
                var n = $(e.target);
                if (!n.parents().hasClass('button-dropdown') && !n.parents().hasClass('dong-the')) {
                    $('.button-dropdown .dropdown-menu').hide();
                    $('.button-dropdown .dropdown-toggle').removeClass('active');
                }
            });

        $(document).on('click',
            '.select-tab',
            function(event) {
                event.preventDefault();
                $('.select-tab').removeClass('active');
                var tabActive = $(this).attr('href');
                $('a.select-tab[href="' + tabActive + '"]', '.tab-title').addClass('active');
                $('.tab-item').hide();
                $(tabActive).show();
            });

        $(document).on('click',
            '.advanced-search',
            function(e) {
                e.preventDefault();
                $('#advanced-search-panel').toggleClass('show');
            });

        $(document).on('click',
            '.save-document',
            function(e) {
                e.preventDefault();
                $.icpost({
                    qname: 'SaveMyDoc',
                    url: vbluat.virtualPath('/Ajax/SaveMyDoc'),
                    data: { docId: $(this).data('id') },
                    success: function (resp) {
                        if (resp.Message != null && resp.Message.length > 0) {
                            $().icpopup({ messages: [resp.Message] });
                        }
                    }
                });
            });

        $(document).on('click',
            '.searchFieldId',
            function(e) {
                e.preventDefault();
                var form = $('#SearchForm');
                $('select[name="FieldId"]', form)
                    .removeAttr('selected')
                    .find('[value=' + $(this).data('id') + ']')
                    .attr('selected', 'selected');
                form.submit();
            });

        $(document).on('click',
            '#xoatimtheongay',
            function(e) {
                e.preventDefault();
                var form = $('#SearchForm');
                $('select[name="SearchByDate"]', form)
                    .removeAttr('selected')
                    .find(':first')
                    .attr('selected', 'selected');
                $('input[name="DateFrom"]', form).val('');
                $('input[name="DateTo"]', form).val('');
                form.submit();
            });

        $(document).on('click',
            '#xoanhomvanban',
            function(e) {
                e.preventDefault();
                var form = $('#SearchForm');
                $('select[name="DocGroupId"]', form)
                    .removeAttr('selected')
                    .find(':first')
                    .attr('selected', 'selected');
                form.submit();
            });

        $(document).on('click',
            '#xoaloaivanban',
            function(e) {
                e.preventDefault();
                var form = $('#SearchForm');
                $('select[name="DocTypeId"]', form)
                    .removeAttr('selected')
                    .find(':first')
                    .attr('selected', 'selected');
                form.submit();
            });

        $(document).on('click',
            '#xoatinhtranghieuluc',
            function(e) {
                e.preventDefault();
                var form = $('#SearchForm');
                $('select[name="EffectStatusId"]', form)
                    .removeAttr('selected')
                    .find(':first')
                    .attr('selected', 'selected');
                form.submit();
            });

        $(document).on('click',
            '#xoalinhvuc',
            function(e) {
                e.preventDefault();
                var form = $('#SearchForm');
                $('select[name="FieldId"]', form)
                    .removeAttr('selected')
                    .find(':first')
                    .attr('selected', 'selected');
                form.submit();
            });

        $(document).on('click',
            '#xoacoquanbanhanh',
            function(e) {
                e.preventDefault();
                var form = $('#SearchForm');
                $('select[name="OrganId"]', form)
                    .removeAttr('selected')
                    .find(':first')
                    .attr('selected', 'selected');
                form.submit();
            });

        $(document).on('click',
            '#xoanguoiky',
            function(e) {
                e.preventDefault();
                var form = $('#SearchForm');
                $('input[name="SignerName"]', form).val('');
                $('input[name="SignerId"]', form).val('');
                form.submit();
            });

        $(document).on('click',
            '.docGroupSearch',
            function(e) {
                e.preventDefault();
                var form = $('#SearchForm');
                $('#CountByDocGroup').val(1);
                $('select[name="DocGroupId"]', form)
                    .removeAttr('selected')
                    .find('[value=' + $(this).data('id') + ']')
                    .attr('selected', 'selected');
                form.submit();
            });

        $(document).on('click',
            '#xoadieukientimkiem',
            function(e) {
                e.preventDefault();
                var form = $('#SearchForm');
                form.resetForm('Keywords').submit();
            });

        $(document).on('click',
            '.tabs-nav-search-by-date',
            function(e) {
                e.preventDefault();
                $('.tabs-nav-search-by-date').removeClass('active');
                $(this).addClass('active');
                var box = $('#SearchByDateBox');
                $('input[name="SearchByDate"]', box).val($(this).data('id'));
                box.resetForm();
            });

        $(document).on('click', '.selectFieldsId', function(e) {
            e.preventDefault();
            var field = $(this).next(), id = field.data('id'), val = field.val();
            if (val==0) {
                $(this).find('img').attr('src', vbluat.virtualPath('/assets/images/arrow-left.png'));
                vbluat.moveItems(field.val(id).closest('.customer-fields'), '#mCSB_4_container1');
            } else {
                $(this).find('img').attr('src', vbluat.virtualPath('/assets/images/arrow-right.png'));
                vbluat.moveItems(field.val(0).closest('.customer-fields'), '#mCSB_3_container1');
            }
            
        });

        $(document).on('click',
            '#fieldIdAdd',
            function(e) {
                e.preventDefault();
                var field = $(this).next();
                $(this).find('img').attr('src', vbluat.virtualPath('/assets/images/arrow-right.png'));
                vbluat.moveItems(field.val(0).closest('.customer-fields'), '#mCSB_4_container1');
            });

        $(document).on('click',
            '#fieldIdRemove',
            function(e) {
                e.preventDefault();
                var chk = $('#mCSB_4_container1 input[type="checkbox"]').not(':checked');
                vbluat.moveItems(chk.attr('name', 'FieldIdSource').closest('div'), '#mCSB_3_container1');
            });

        $(document).on('click',
            '.docPrint',
            function(event) {
                event.preventDefault();
                $('#docPrint').lawsExists(function() { 
                    $(this).printThis({
                        pageTitle: 'Văn bản luật',
                        header:
                            '<div class="hearder-logo-print"><div class="logo-print"><img alt="van ban luat" src="' +
                                vbluat.virtualPath('/assets/images/logo-print.png') +
                                '"></div></div>',
                        loadCSS: vbluat.virtualPath('/assets/css/print.css')
                    });
                });
            });

        $(document).on('click',
            '.iconchitiet',
            function(e) {
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

        $(document).on('click',
            '#gop-y',
            function(event) {
                event.preventDefault();
                var title = $(this).data('name');
                $().icpopup({
                    loadForm: true,
                    formUrl: vbluat.virtualPath('/form-gui-gop-y-bai-viet.html'),
                    callback: function () {
                        $('#ArticleFeedbackForm #ArticleTitle').text(title);
                        $('#ArticleFeedbackForm #Title').val(title);
                        var form = $('#ArticleFeedbackForm');
                        form.removeData('validator');
                        form.removeData('unobtrusiveValidation');
                        $.validator.unobtrusive.parse(form);
                    }
                });
            });

        $('#gopy').lawsExists(function() {
            vbluat.closeDialogById('gopy');
        });
        $('.wg-tieudiem').lawsExists(function () {
            var self = this;
            self.posFixed({ classFixed: 'td-fixed', elementCompare: '.singer.chitiet' });
        });
        $(document).ready(function () {
            $("[data-js=open]").on("click", function () {
                vbluat.popupOpenClose($(this).next());
            });
        });
        $(window).load(function() {
            $('.content-scroll-1').mCustomScrollbar({
                snapAmount: 40,
                scrollButtons: { enable: true },
                keyboard: { scrollAmount: 40 },
                mouseWheel: { deltaFactor: 40 },
                scrollInertia: 400
            });
        });

        $(document).on('click',
            '#gui-yeu-cau-vb',
            function (event) {
                event.preventDefault();
                var docUrl = $(this).data('email');
                vbluat.gopy(docUrl);
        });
        $('.lvk').loadmoreContent();
        $('.lvk-mobi').loadmoreContent({ rowClass: '.vbluat-row-mobi', pageSize: 6 });
        $('.lvk-search').loadmoreContent({ rowClass: '.vbluat-row-search', pageSize: 10 });
        $('.lvk-doctype-search').loadmoreContent({ rowClass: '.vbluat-doctype-search-row', pageSize: 10 });
        $('.lvk5').loadmoreContent({ rowClass: '.vbluat-row-detail', pageSize: 11 });
        //$('.tabs-nav').TabUI({setValue: true});
        $('a.vblAjaxHome').on('click',
            function(e) {
                e.preventDefault();
                $('a.vblAjaxHome').removeClass('active');
                $(this).addClass('active');
                $.icget({
                    qname: 'QuereHomePage',
                    resultId: $(this).data('result'),
                    url: $(this).data('ajax-url'),
                    dataType: $(this).data('type')
                });
            });

        $(document).on('change',
            '#select-reateTypeId',
            function (event) {
                event.preventDefault(); 
                $.icget({
                    qname: 'QuereDocRelateGetPage',
                    resultId: $(this).find(':selected').data('result'),
                    url: $(this).find(':selected').data('ajax-url'),
                    dataType: $(this).find(':selected').data('type'),
                    complete: function() {
                        $('#contentsWrapper').vbluatInfiteScroll({
                            contentsWrapperSelector: '#contentsWrapper',
                            contentSelector: '.pcontent',
                            nextSelector: '#pnext'
                        });
                    }
                });
            });

        $(document).on('click',
            '.btn-reset',
            function(event) {
                event.preventDefault();
                vbluat.ResetForm($(this).closest('form'));
            });

        $(document).on('click',
            '.iconview',
            function(event) {
                event.preventDefault();
                $('.iconview ').removeClass('active');
                $(this).addClass('active');
                if ($(this).hasClass('show-attr')) {
                    $('div.post-tag').show();
                } else if ($(this).hasClass('hide-attr')) {
                    $('div.post-tag').hide();
                }
            });

        $(document).on('click',
            '.tie-drop',
            function (event) {
                event.preventDefault();
                //$('.rows-luocdo2').slideDown(300);
                //var e = $(this).next().find('.rows-luocdo2');
                //if (e.length) {
                //    e.slideToggle(300);
                //}
                $('.tieloto').removeClass('active');
                $(this).closest('tieloto').addClass('active');
                var checkElement = $(this).closest('.tieloto').find('.dropsub').first();
                if (checkElement.length) {
                    if ((checkElement.is('.dropsub')) && (checkElement.is(':visible'))) {
                        $(this).closest('.tieloto').removeClass('active');
                        checkElement.slideUp('normal');
                    }
                    if ((checkElement.is('.dropsub')) && (!checkElement.is(':visible'))) {
                        $('.dropcontent .dropsub:visible').slideUp('normal');
                        checkElement.slideDown('normal');
                    }
                }
                if ($(this).closest('.tieloto').find('.dropsub').children().length == 0) {
                    return true;
                } else {
                    return false;
                }  
            });

        $(document).on('click',
            '.doc-print',
            function (event) {
                event.preventDefault();
                $('.print-doc-content').lawsExists(function () {
                    $(this).printThis({
                        pageTitle: 'Văn bản luật',
                        header:
                            '<div class="hearder-logo-print"><div class="logo-print"><img alt="van ban luat" src="' +
                                vbluat.virtualPath('/assets/images/logo-print.png') +
                                '"></div></div>',
                        loadCSS: vbluat.virtualPath('/assets/css/print.css')
                    });
                    //$(this).print({
                    //    pageTitle: 'Vanbanluat.vn',
                    //    globalStyles: true,
                    //    prepend: "in văn bản <br/>"
                    //});
                });
            });

        $(document).on('click',
            '.inlucdo',
            function (event) {
                event.preventDefault();
                $('#DocDiagramPrint').lawsExists(function () {
                    $(this).printThis({
                        pageTitle: 'Văn bản luật',
                        header:
                            '<div class="hearder-logo-print"><div class="logo-print"><img alt="van ban luat" src="' +
                                vbluat.virtualPath('/assets/images/logo-print.png') +
                                '"></div></div><p style="padding: 10px;font-weight:bold;">' + $('.title-singer').text() + '</p>',
                        loadCSS: vbluat.virtualPath('/assets/css/print.css')
                    });
                });

            });

        $(document).on('click',
            '.doc-send-mail-form-popup',
            function (event) {
                event.preventDefault();
                $().icpopup({
                    loadForm: true,
                    formUrl: vbluat.virtualPath('/' + $(this).data('action')),
                    callback: function() {
                        $('#DocSendMailForm #DocUrl').val(window.location.href);
                        var form = $('#DocSendMailForm');
                        form.removeData('validator');
                        form.removeData('unobtrusiveValidation');
                        $.validator.unobtrusive.parse(form);
                    }
                });
            });

        $(document).on('click',
            '.doc-feedback-form-popup',
            function (event) {
                event.preventDefault();
                var docName = $(this).data('name');
                $().icpopup({
                    loadForm: true,
                    formUrl: vbluat.virtualPath('/' + $(this).data('action')),
                    callback: function () {
                        $('#DocFeedbackForm #DocTitle').text(docName);
                        $('#DocFeedbackForm #DocName').val(docName);
                        var form = $('#DocFeedbackForm');
                        form.removeData('validator');
                        form.removeData('unobtrusiveValidation');
                        $.validator.unobtrusive.parse(form);
                    }
                });
            });

        $(document).on('click',
            '.yeucauvanbanbtn',
            function (event) {
                event.preventDefault();
                var email = $(this).data('title');
                $().icpopup({
                    loadForm: true,
                    formUrl: vbluat.virtualPath('/' + $(this).data('action')),
                    callback: function () {
                        $('#GuiYeuCauVBForm #DocTitle').text(email);
                        $('#GuiYeuCauVBForm #DocName').val(email);
                        var form = $('#GuiYeuCauVBForm');
                        form.removeData('validator');
                        form.removeData('unobtrusiveValidation');
                        $.validator.unobtrusive.parse(form);
                    }
                });
            });
        $('.google-login').googleLogin();
        $('.facebook-login').facebookLogin();
        $('#SignerName').on('focus',
            function() {
                var input = $(this);
                input.data('SignerIdValue', input.val());
            });
        $('#SignerName').on('blur',
            function() {
                var input = $(this);
                if (input.val().trim() == '0') {
                    input.val('');
                }
                if (input.val() != input.data('SignerIdValue')) {
                    if (input.val() == '') {
                        $('#SignerId').val(0);
                    }
                }
            });
        $('#SignerName').lawsExists(function() {
            $(this).autocomplete({
                minLength: 1,
                dataType: 'json',
                async: false,
                cache: false,
                source: function(request, response) {
                    $('#SignerId').val(0);
                    $('#signer-message').text('');
                    $.icpost({
                        qname: 'AutocompleteSignerByName',
                        url: vbluat.virtualPath('/Ajax/AutocompleteSignerByName'),
                        cache: false,
                        data: { signerName: request.term },
                        success: function(resp) {
                            if (resp.Data == null || resp.Data.length == 0)
                                $('#signer-message')
                                    .html(
                                        '<span class="text-danger field-validation-error"><span>Không tìm thấy người ký phù hợp.</span></span>');
                            response($.map(resp.Data,
                                function(item) {
                                    return {
                                        label: item.SignerName,
                                        val: item.SignerId
                                    }
                                }));
                        }
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
                    $('#SignerId').val(ui.item.val);
                }
            });
        });
    },
    ajaxEvents: {
        btnDefault: '',
        OnBegin: function(text) {
            if ($('.btn-form').length) {
                vbluat.ajaxEvents.btnDefault = text;
                $('.btn-form').text('Đang xử lý');
                $('.btn-form').attr('disabled', 'disabled');
            }
            var el = $('#loadingbar');
            if (el.length === 0) {
                $('body').append('<div id="loadingbar"></div>');
                el.addClass('waiting').append($('<dt/><dd/>'));
                el.width((50 + Math.random() * 30) + "%");
            }
        },
        OnSuccess: function(data, status, xhr) {
            if (data.Completed) {
                if (data.ReturnUrl != null && data.ReturnUrl.length > 0) {
                    setTimeout(function() {
                            window.location.href = data.ReturnUrl;
                        },
                        100);
                } else window.location.href = configs.rootPath;
            } else if (data.Message != null && data.Message.length > 0) {
                if ($('.btn-form').length) {
                    $('.btn-form').text(vbluat.ajaxEvents.btnDefault);
                    $('.btn-form').removeAttr('disabled', 'disabled');
                }
                $().icpopup({
                    success: false,
                    messages: [data.Message]
                });
            }
        },
        OnComplete: function () {
            var el = $('#loadingbar');
            if (el.length) {
                el.width('101%').delay(200).fadeOut(400, function () {
                    el.remove();
                });
            }
        },
        OnSuccessAlert: function(data, status, xhr) {
            if ($('.btn-form').length) {
                $('.btn-form').text(vbluat.ajaxEvents.btnDefault);
                $('.btn-form').removeAttr('disabled', 'disabled');
            }
            $('form').resetForm();
            if (data.Message != null && data.Message.length > 0) {
                $().icpopup({
                    success: data.Completed ? true : false,
                    messages: [data.Message],
                    returnUrl: data.ReturnUrl != null && data.ReturnUrl.length > 0 ? data.ReturnUrl : ''
                });
            }
        },
        OnFailure: function(xhr, status, error) {
            if ($('.btn-form').length) {
                $('.btn-form').text(vbluat.ajaxEvents.btnDefault);
                $('.btn-form').removeAttr('disabled', 'disabled');
            }
            if (xhr.status === 0) {
                $().icpopup({ messages: ['Không có kết nối mạng. Vui lòng kiểm tra lại.'], success: false });
            } else if (xhr.status === 404) {
                $().icpopup({ messages: ['Không tìm thấy trang yêu cầu.'], success: false });
            } else if (xhr.status === 500) {
                $().icpopup({ messages: ['Lỗi máy chủ nội bộ. [500].'], success: false });
            } else if (error === 'parsererror') {
                $().icpopup({ messages: ['Yêu cầu phân tích cú pháp JSON lỗi.'], success: false });
            } else if (error === 'timeout') {
                $().icpopup({ messages: ['Hết thời gian yêu cầu.'], success: false });
            } else if (error === 'abort') {
                $().icpopup({ messages: ['Yêu cầu xử lý bị hủy.'], success: false });
            } else if (xhr.status !== 403) {
                $().icpopup({ messages: ['Lỗi :.n' + xhr.responseText], success: false });
            }
            var el = $('#loadingbar');
            if (el.length) {
                el.width('101%').delay(200).fadeOut(400, function () {
                    el.remove();
                });
            }
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
        deleteCustomerDocs: function (docGroupId, docId, type) { //type = 1 : Vb quan tâm, 2: thông báo hiệu lực
            var conf = confirm('Xác nhận xóa văn bản quan tâm ?');
            if (conf) {
                $.icget({
                    qname: 'DeleteDocument',
                    url: vbluat.virtualPath('/Ajax/DeleteDocument'),
                    data: { docGroupId: docGroupId, docId: docId, type: type },
                    success: function (resp) {
                        if (resp.Message != null && resp.Message.length > 0) {
                            $().icpopup({ messages: [resp.Message] });
                            location.reload();
                        }
                    }
                });
            }
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
        },
        dllMyDocumentChange: function () {
            var FieldId = $("#dllField option:selected").val();
            var EffectStatusId = $("#dllEffectStatus option:selected").val();
            var OrganId = $("#dllOrgan option:selected").val();
            var DocTypesId = $("#dllDocType option:selected").val();
            $.icget({
                qname: 'MyDocumentFilter',
                url: vbluat.virtualPath('/Ajax/MyDocumentFilter'),
                dataType: 'html',
                data: { FieldId: FieldId, EffectStatusId: EffectStatusId, OrganId: OrganId, DocTypesId: DocTypesId },
                success: function (resp) {
                    $("#Content").html(resp);
                }
            });
        }
    },
    logs: {
        articleLogs: function(articleId, categoryId) {
            $.lawsAjax({
                url: vbluat.virtualPath('/Ajax/ArticleLogs'),
                data: { articleId: articleId, categoryId: categoryId },
                beforeSend: {},
                success: function(resp) {
                    if (!resp.Completed) {
                        if (resp.Message != null && resp.Message.length > 0) {
                            console.log('ArticleLogs Error: ' + resp.Message);
                        }
                    }
                },
                error: function(resp) {
                    console.log('articleLogs Error: ' + resp.Message);
                }
            });
        },
        docViewLogs: function(docId, docGroupId, actionTypeId) {
            $.lawsAjax({
                url: lawsVn.virtualPath('/Ajax/DocViewLogs'),
                data: { docId: docId, docGroupId: docGroupId, actionTypeId: actionTypeId },
                beforeSend: {},
                success: function(resp) {
                    if (!resp.Completed) {
                        if (resp.Message != null && resp.Message.length > 0) {
                            console.log('DocViewLogs Error: ' + resp.Message);
                        }
                    }
                },
                error: function(resp) {
                    console.log('DocViewLogs Error: ' + resp.Message);
                }
            });
        },
        docSearchLogs: function(keywords, dateFrom, dateTo, docTypeId, organId, signerId, fieldId) {
            $.lawsAjax({
                url: lawsVn.virtualPath('/Ajax/DocSearchLogs'),
                data: {
                    keywords: keywords,
                    dateFrom: dateFrom,
                    dateTo: dateTo,
                    docTypeId: docTypeId,
                    organId: organId,
                    signerId: signerId,
                    fieldId: fieldId
                },
                beforeSend: {},
                success: function(resp) {
                    if (!resp.Completed) {
                        if (resp.Message != null && resp.Message.length > 0) {
                            console.log('docSearchLogs Error: ' + resp.Message);
                        }
                    }
                },
                error: function(resp) {
                    console.log('docSearchLogs Error: ' + resp.Message);
                }
            });
        }
    },
    closeDialogById: function(id) {
        $('#' + id).dialog('close');
    },
    popupOpenClose:function(popup) {
  
        var wrap = $(popup).find('.wrapper')
        /* Add div inside popup for layout if one doesn't exist */
        if (wrap.length == 0){
            $(popup).wrapInner("<div class='wrapper'></div>");
        }
  
        /* Open popup */
        $(popup).show();

        /* Close popup if user clicks on background */
        $(popup).click(function(e) {
            if ( e.target == this ) {
                if ($(popup).is(':visible')) {
                    $(popup).hide();
                }
            }
        });

        /* Close popup and remove errors if user clicks on cancel or close buttons */
        $(popup).find("button[name=close]").on("click", function() {
            if ($(".formElementError").is(':visible')) {
                $(".formElementError").remove();
            }
            $(popup).hide();
        });
    },
    ResetForm: function(form) {
        form.find('input:text, input:password, input:file, select, textarea').val('');
        form.find('input:radio, input:checkbox')
            .removeAttr('checked').removeAttr('selected');
    },
    moveItems: function (origin, dest) {
        origin.prependTo(dest);
    },
    virtualPath: function (patch) {
        var host = window.location.protocol + '//' + window.location.host;
        return host + patch;
    },
    progressInView: function () {
        var pageTop = $(window).scrollTop();
        var pageBottom = pageTop + $(window).innerHeight();
        var globalList = $(window).data('globalListDocIndex');
        for (var i = 0; i < globalList.length; i++) {
            var diList = globalList[i].docIndexList;
            for (var j = 0; j < diList.length; j++) {
                var di = diList[j];
                var muclucItem = $('#mucluc li.list-mluc a.' + di.obj[0].id);
                if (muclucItem.length > 0) {
                    if ((di.bottom >= pageTop) &&
                        (di.top <= pageBottom) &&
                        (di.bottom <= pageBottom) &&
                        (di.top >= pageTop)) {
                        setTimeout(function() {
                            $('.content-scroll-1').mCustomScrollbar('scrollTo', muclucItem.position().top);
                            },
                            100);
                        muclucItem.closest('.list-mluc').prevAll().find('a').css({ 'color': '#0077bf !important', 'font-weight': 'bold' });
                    } else if (pageTop <= di.top) {
                        muclucItem.closest('.list-mluc').nextAll().find('a').removeAttr('style');
                        return false;
                    }
                }
            }
        }
    },
    scrollTop: function() {
        var idButton = '#myBtn';
        // trượt xuống 20px
        var offset = 20;
        // Thời gian di trượt là 0.5 giây
        var duration = 500;
        jQuery(window).scroll(function () {
            if (jQuery(this).scrollTop() > offset) {
                jQuery(idButton).fadeIn(duration);
            } else {
                jQuery(idButton).fadeOut(duration);
            }
        });
        jQuery(idButton).click(function (event) {
            event.preventDefault();
            jQuery('html, body').animate({ scrollTop: 0 }, duration);
            return false;
        });
    }
}
//obj chua cac phuong thuc validate phia client
var VbLuatValidate = {
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
    }
}
$.fn.inputPlaceholder = function () {
    return this.each(function () {
        $(this).on('focus',
            function () {
                if (!$(this).data('defaultText')) $(this).data('defaultText', $(this).val());
                if ($(this).val() == $(this).data('defaultText')) $(this).val('');
            });
        $(this).on('blur',
            function () {
                if ($(this).val() == '') $(this).val($(this).data('defaultText'));
            });
    });
}

$.fn.extend({
    LawScrollTo: function () {
        $(this).lawsExists(function () {
            var x = $(this).offset().top - 100;
            jQuery('html,body').animate({ scrollTop: x }, 100);
        });
    }
});
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

(function () {
    $.fn.
        posFixed = function (options) {
        var defaults = {
            classFixed: 'fixed',
            elementCompare:''
        }, settings = $.extend(defaults, options), self = this;
        if (settings.elementCompare !== '') {
            if ($(settings.elementCompare).length) {
                var height = $(settings.elementCompare).height(), top = $(settings.elementCompare).offset().top;
                $(window).scroll(function() {
                    var scroll = $(window).scrollTop();
                    if (scroll > top && scroll + 200 < height) {
                        self.addClass(settings.classFixed);
                    } else if (scroll <= top || scroll + 200 >= height) {
                        self.removeClass(settings.classFixed);
                    }
                });
            }
        }
    };

    $.fn.resetForm = function (name) {
        var self = $(this);
        self.find('input:text' + (name !== undefined && name !== '' ? '[name!=' + name +']' : '') + ', input:password, input:file, select, textarea').val('');
        self.find('input:radio, input:checkbox')
            .removeAttr('checked').removeAttr('selected');
        return self;
    }

    $.fn.toggleTarget = function (options) {
        var defaults = {
            dataAttr: 'data-toggle-target',
            classHidden: 'target-hidden',
            classShow: 'target-expanded'
        }
        options = $.extend(defaults, options);
        var target = $(this).attr(options.dataAttr);
        $(target).addClass(options.classHidden);
        $(this).click(function (e) {
            e.preventDefault();
            $(target).toggleClass(options.classShow).css('display', '');
        });
    }

    $.fn.sharePanelFixed = function(options) {
        var defaults = {
            fixClass: 'fixed',
            mobiClass: 'scrolltop-mobi',
            elementCompare: '.tab-noidung'
        },
            settings = $.extend(defaults, options),
            self = this, el = $(settings.elementCompare), responsiveFn = function() {
                if (self.length) {
                    var width = $(window).width();
                    if (width < 640) {
                        var height = el.height();
                        $(window).scroll(function () {
                            var scroll = $(window).scrollTop();
                            if (scroll > height) {
                                self.addClass('scrolltop-mobi fixed');
                            } else if (scroll <= height) {
                                self.removeClass('scrolltop-mobi fixed');
                            }
                        });
                    } else {
                        self.removeClass('scrolltop-mobi fixed');
                    }
                }
            }
        $(window).ready(responsiveFn).resize(responsiveFn); 
    }

    $.fn.TabUI = function (options) {
        var defaults = {
            tabsNav: '.tabs-nav',
            tabsContent: '.tabs-content > div',
            activeTabClass: 'active',
            resetValue: true,
            setValue: false,
            elementSetValue:'input[name="SearchByDate"]'
        }
        options = $.extend(defaults, options);
        $(this).on('click',
            function (e) {
                e.preventDefault();
                $(options.tabsNav).removeClass(options.activeTabClass);
                $(this).addClass(options.activeTabClass);
                $(options.tabsContent).hide();
                $($(this).attr('href')).fadeIn(300);
                if (options.resetValue) {
                    $(options.tabsContent).resetForm();
                }
                if (options.setValue) {
                    $(options.elementSetValue, $('.tabs-content')).val($(this).data('id'));
                }
            });
    }

    $.fn.loadmoreContent = function (options) {
        var defaults = {
            rowClass: '.vbluat-row',
            container: '.content-scroll-1',
            pageSize: 15,
            userCustomScrollbar: true,
            callBack: {}
        }
        options = $.extend(defaults, options);
        if ($(options.rowClass).length <= options.pageSize) $(this).hide();
        $(options.rowClass).slice(0, options.pageSize).show();
        var execCallback = $.isFunction(options.callBack) ? options.callBack : $.noop;
        $(this).on('click',
            function (e) {
                e.preventDefault();
                var rows = $(options.rowClass).length;
                $(options.rowClass + ':hidden').slice(0, rows).slideDown();
                var elPos = $(options.rowClass + ':eq(' + options.pageSize + ')');
                if ($(options.rowClass + ':hidden').length == 0) {
                    $(this).fadeOut('slow');
                    execCallback();
                    var position = elPos.position().top - 10;
                    setTimeout(function () {
                        if (options.userCustomScrollbar) {
                            $(options.container).mCustomScrollbar('scrollTo',
                                position,
                                {
                                    timeout: 300
                                });
                        } else {
                            $(options.container).addClass('filter-box');
                        }
                    }, 300);
                }
            });
    }

    $.fn.googleLogin = function(options) {
        var defaults = {
            oAuthUrl: 'https://accounts.google.com/o/oauth2/auth?',
            validUrl: 'https://www.googleapis.com/oauth2/v1/tokeninfo?access_token=',
            userInfoUrl:'https://www.googleapis.com/oauth2/v1/userinfo?access_token=',
            scope: 'https://www.googleapis.com/auth/userinfo.profile https://www.googleapis.com/auth/userinfo.email',
            redirectUrl: 'https://vanbanluat.vn',
            clientId: '382397198478-5kq0rd1tqa3rmd5tpgmsi1pp78ar4dev.apps.googleusercontent.com',
            type: 'token',
            accessToken:''
        }
        options = $.extend(defaults, options);
        $(this).on('click',
            function(e) {
                e.preventDefault();
                var url = options.oAuthUrl + 'scope=' + options.scope + '&client_id=' + options.clientId + '&redirect_uri=' + options.redirectUrl + '&response_type=' + options.type;
                var gPopup = window.open(url, 'Văn bản luật đăng nhập tài khoản Google', 'width=800, height=600');
                var time = window.setInterval(function () {
                    try {
                        if (gPopup.document.URL.indexOf(options.redirectUrl) !== -1) {
                            window.clearInterval(time);
                            options.accessToken = getResponse(gPopup.document.URL, 'access_token');
                            gPopup.close();
                            requestAccountInfo(options.accessToken);
                        }
                    }
                    catch (e) {
                        console.log(e);
                    }
                }, 500);
            });
        function getResponse(url, name) {
            name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regexS = '[\\#&]' + name + '=([^&#]*)';
            var regex = new RegExp(regexS);
            var results = regex.exec(url);
            if (results == null)
                return '';
            else
                return results[1];
        }
        function requestAccountInfo(accessToken) {
            $.icget({
                qname: 'GoogeLoginQueue',
                url: options.userInfoUrl + accessToken,
                success: function(resp) {
                    $.icpost({
                        qname: 'GoogeLoginQueuePost',
                        url: vbluat.virtualPath('/Account/GoogleLogin'),
                        data: { googleId: resp.id, email: resp.email, name: typeof (resp.name) === 'undefined' ? '' : resp.name, givenname: typeof (resp.given_name) === 'undefined' ? '' : resp.given_name, familyname: typeof (resp.family_name) === 'undefined' ? '' : resp.family_name, picture: typeof (resp.picture) === 'undefined' ? '' : resp.picture},
                        success: function (response) {
                            if (!response.Completed) {
                                if (response.Data != null) {
                                    $().icpopup({ messages: [response.Data.Message], success: false });
                                }else
                                    $().icpopup({ messages: [response.Data.ActionMessage], success: false });
                            }
                            else if (response.ReturnUrl != null && response.ReturnUrl.length > 0) {
                                window.location.href = response.ReturnUrl;
                            } else console.log(response);
                        }
                    });
                }
            });
        }
    }

    $.fn.facebookLogin = function (options) {
        var defaults = {
            AppId: '193341217939183',
            showLogs: false,
            permissions: 'email',
            connected: false
        }
        options = $.extend(defaults, options);
        $(this).on('click',
            function (e) {
                e.preventDefault();
                fbLogin();
            });
        function fBOnSuccess(resp) {
            $.icpost({
                qname: 'FacebookLoginQueuePost',
                url: vbluat.virtualPath('/Account/FacebookLogin'),
                data: { facebookId: resp.id, email: typeof (resp.email) === 'undefined' ? '' : resp.email, name: typeof (resp.name) === 'undefined' ? '' : resp.name, firstname: typeof (resp.first_name) === 'undefined' ? '' : resp.first_name, lastname: typeof (resp.last_name) === 'undefined' ? '' : resp.last_name, gender: typeof (resp.gender) === 'undefined' ? '' : resp.gender },
                success: function (response) {
                    if (!response.Completed) {
                        if (response.Data != null) {
                            $().icpopup({ messages: [response.Data.Message], success: false });
                        }else
                            $().icpopup({ messages: [response.Data.ActionMessage], success: false });
                    }
                    else if (response.ReturnUrl != null && response.ReturnUrl.length > 0) {
                        window.location.href = response.ReturnUrl;
                    } else console.log(response);
                }
            });
        }
        function fbOnError(error) {
            console.log(error);
        }
        function fbLogin() {
            if (FB) {
                fbLog('login');
                FB.login($.proxy(fbcheckLoginState, this), {
                    scope: options.permissions,
                    return_scopes: true
                });
            }
            else {
                fbLog('Đăng nhập tài khoản Facebook không thành công (Facebook script chưa được load) - thử lại sau 1 giây...');
                setTimeout($.proxy(fbLogin, this), 1000);
            }
        }
        function fbLogout() {
            if (FB && options.connected) {
                fbLog('Đăng xuất tài khoản Facebook');
                FB.logout();
            }
        }
        function fbcheckLoginState() {
            fbLog('fbcheckLoginState');
            FB.getLoginStatus($.proxy(function (response) {
                fbstatusChangeCallback(response);
            }, this));
        }
        function fbLog(message) {
            if (options.showLogs) console.log('$.facebookLogin ' + message);
        }
        function fbstatusChangeCallback(response) {
            fbLog('fbstatusChangeCallback');
            if (response.status === 'connected') {
                options.connected = true;
                fbOnConnected();
            } else if (response.status === 'not_authorized') {
                fbLog('not authorized');
                options.connected = false;
                fbOnError('not authorized');
            } else {
                fbLog('not connected');
                options.connected = false;
                fbOnError('not connected');
            }
        }
        function fbOnConnected() {
            fbLog('fbOnConnected');
            FB.api('/me', 'GET', { fields: 'id,email,name,first_name,last_name,gender,birthday,location' }, $.proxy(function (response) {
                fbLog('successful login for: ' + response.name);
                fBOnSuccess(response);
            }, this));
        }
    }

    $.fn.birthday = function(options) {
        var defaults = {
            day: '#day',
            month: '#month',
            year: '#year'
        }
        options = $.extend(defaults, options);
        var date = new Date();
        var d = 0;
        //Số ngày theo tháng và năm
        function dayList(month, year) {
            var day = new Date(year, month, 0);
            return day.getDate();
        }

        function getBirthDay(d,m,y) {
            var days = dayList(m == 0 ? date.getMonth() + 1 : m, y == 0 ? date.getFullYear() : y );
            $(options.day).html('<option value="0"> Ngày </option>');
            for (var i = 1; i <= days; i++) {
                $(options.day).append('<option value="' + i + '" ' + (i == d ? 'selected' : '') +'>' + i + '</option>');
            };
        }

        $(document).on('change',
            options.year + ', ' + options.month,
            function () {
                var y = $(options.year + ' option:selected').val();
                var m = $(options.month + ' option:selected').val();
                d = $(options.day + ' option:selected').val();
                getBirthDay(d, m, y);
            });
    }

    $.fn.vbluatInfiteScroll = function(options) {
        var defaults = {
                contentsWrapperSelector: "#contentsWrapper",
                contentSelector: ".pcontent",
                nextSelector: "#pnext",
                article: false, // tin bai
                docByField: false, //ds vb theo linh vuc
                docDetail: false, //ko set url vào history khi vào chi tiết vb
                loadImage: ""
        }, settings = $.extend(defaults, options);
        var hiddenTitleAndUrl = function(_title, _path, _page) {
            return "<span class='hidden-title' style='display:none'>" +
                _title +
                "</span><span class='hidden-url' style='display:none'>" +
                _path +
                "</span><span class='hidden-page' style='display:none'>" +
                _page +
                "</span>";
        };
           var setTitleAndHistory = function(_title, _path) {
                // Set history
                if (window.history && window.history.pushState) {
                    history.pushState(null, _title, _path);
                }
                // Set title
                $('title').html(_title);
            };
            var changeTitleAndUrl = function(_value) { 
                var _title = $(_value).children('.hidden-title:first').text(),
                    _path = $(_value).children('.hidden-url:first').text();
                if ($('title').text() !== _title) {
                    // title co thay doi
                    setTitleAndHistory(_title, _path);
                }
            };
            var urlChangePage = function(_url, _page) {
                var regex = new RegExp("(page=)[0-9]+", 'ig');
                return _url.replace(regex, '$1' + _page);
            };
            var seoPagination = function (_prev, _next) {
                if (_prev !== undefined && _prev != '') {
                    if ($('link[rel="prev"]').length == 0) {
                        var prev = document.createElement('link');
                        prev.rel = 'prev';
                        prev.href = _prev;
                        document.getElementsByTagName('head')[0].appendChild(prev);
                    } else {
                        $('link[rel="prev"]').attr('href', _prev);
                    }
                } else $('link[rel="prev"]').remove();

                if (_next !== undefined && _next != '') {
                    if ($('link[rel="next"]').length == 0) {
                        var next = document.createElement('link');
                        next.rel = 'next';
                        next.href = _next;
                        document.getElementsByTagName('head')[0].appendChild(next);
                    } else {
                        $('link[rel="next"]').attr('href', _next);
                    }
                } else $('link[rel="next"]').remove();
            };
        //init
        var originalTitle = title = $('title').text(),
            path = $(location).attr('href'),
            originalUrl = path.indexOf('?') === -1 ? path : path.substr(0, path.indexOf('?')), page = $(settings.nextSelector).data('page') !== undefined ? $(settings.nextSelector).data('page') : 0,
            documentHeight = $(document).height(),
            windowHeight = (typeof window.outerHeight !== 'undefined') ? Math.max(window.outerHeight, $(window).height()) : $(window).height(),
            $contents = $(settings.contentSelector);
        if (page > 0) {
            $(settings.contentSelector + ':last').append(hiddenTitleAndUrl(title, path, page));
            if (!settings.docDetail) setTitleAndHistory(title, path);
            /**
            * scroll
            */
            var lastScroll = 0, currentScroll;
            $(window).scroll(function () {
                window.clearTimeout($.data('this', 'vbluatScrollTimer'));
                $.data(this, 'vbluatScrollTimer', window.setTimeout(function () {
                    currentScroll = $(window).scrollTop(); 
                    if (currentScroll > lastScroll) {
                        $contents.each(function (key, value) {
                            if ($(value).offset().top + $(value).height() > currentScroll) {
                                changeTitleAndUrl(value);
                                return false;
                            }
                        });
                    } else if (currentScroll < lastScroll) {
                        $contents.each(function (key, value) {
                            if ($(value).offset().top + $(value).height() - windowHeight / 2 > currentScroll) {
                                changeTitleAndUrl(value);
                                return false;
                            }
                        });
                    }
                    lastScroll = currentScroll;
                }, 200));
            }); //scroll
        }
        $(document).on('click',
            settings.nextSelector,
            function(e) {
                e.preventDefault();
                var query = [$(settings.nextSelector).data('query')],
                ajaxUrl = [$(settings.nextSelector).data('ajax')];
                $(settings.nextSelector).closest('.loadmore').remove();
                if (query[0] !== undefined && ajaxUrl[0] !== undefined) {
                    if (settings.article) {
                        var cateId = originalUrl.match(/-c([0-9]+)/);
                        if (cateId != null && $.isNumeric(cateId[1])) {
                            ajaxUrl = ajaxUrl + '?categoryId=' + cateId[1] + '&' + query;
                        }
                    }else if(settings.docByField)
                    {
                        var fieldId = originalUrl.match(/\d+/g);
                        var docGroupId = originalUrl.match(/-f([0-9]+)/);
                        if (fieldId != null && $.isNumeric(fieldId[1])) {
                            ajaxUrl = ajaxUrl + query + '&fieldId=' + fieldId[1];
                        } else ajaxUrl = ajaxUrl + query + '&fieldId=0';
                        if (docGroupId != null && $.isNumeric(docGroupId[1])) {
                            ajaxUrl = ajaxUrl + '&docGroupId=' + docGroupId[1];
                        } else ajaxUrl = ajaxUrl + '&docGroupId=0';
                    }
                    else ajaxUrl = ajaxUrl + query;
                    $.icget({
                        qname: 'InfiteScrollQuere',
                        url: ajaxUrl,
                        dataType: 'html',
                        success: function (resp) {
                            if (title.length > 0) {
                                var idx = title.indexOf('- trang');
                                if (idx > 0)
                                    title = title.substring(0, idx) + ' - trang ' + page;
                                else title = title + ' - trang ' + page;
                            } else title = ' trang ' + page;

                            path = settings.article ? '?' + query[0] : query[0];
                            var el = $(resp).find(settings.nextSelector);
                            $(settings.contentsWrapperSelector)
                                    .append($(resp).find(settings.contentSelector)
                                        .append(hiddenTitleAndUrl(title, originalUrl + path, el.data('page'))))
                                    .append($(resp).find(settings.nextSelector).closest('.loadmore'));
                            documentHeight = $(document).height();
                            $contents = $(settings.contentSelector);
                            page = $(settings.nextSelector).data('page'); 
                        }
                    });
                }
            });
    }

    $.fn.vbluatDetailFull = function(options) {
        var defaults = {
            contentTabClass: '.tab-noidung',
            contentLeft: '.content-left',
            contentLuocdo: '.content-luocdo',
            tabClass: '.tab-noidung-item',
            contentClass: '.detailfull',
            tomtatTab: '#tomtat',
            noidungTab: '#noidung',
            luocdoTab:'#luocdo',
            halfzone: '.half-zone',
            h1Tag: '.title-singer',
            mucluc: '#mucluc',
            sidebar:'.sidebar-270',
            docsConsolidation:false,
            register:true
        }, opts = $.extend(defaults, options);
        var setTitleAndHistory = function (_title, _path) {
            // Set history
            if (window.history && window.history.pushState) {
                history.pushState(null, _title, _path);
                // Set title
                if (_title != null)
                    $('title').html(_title);
            }
        }, activeTab = '', getHash = function () {
            if (window.location.hash) {
                return window.location.hash;
            }
            return '';
        }, docIndexScroll = function () {
                if ($(opts.noidungTab).length) {
                    var height = $(opts.noidungTab).height(), top = $(opts.noidungTab).offset().top;
                    var scroll = $(window).scrollTop();
                    if (scroll > top && scroll + 200 < height) {
                        $(opts.mucluc).addClass('fixed');
                    } else if (scroll <= top || scroll + 200 >= height) {
                        $(opts.mucluc).removeClass('fixed');
                    }
                }
            },
            showTab = function (tab) {
            activeTab = tab !== undefined && tab !== '' ? tab : getHash(); 
            if (activeTab === '') {
                activeTab = $(opts.noidungTab).hasClass('none') ? opts.tomtatTab : opts.noidungTab;
            } 
            $(opts.contentClass).addClass('none');
            $(activeTab).removeClass('none'); 
            $(opts.tabClass).removeClass('active');
            $('a[data-href$="' + activeTab + '"]', $(opts.contentTabClass)).addClass('active');
            //khong phai vb hop nhat
            if (!opts.docsConsolidation) {
                $(opts.halfzone).removeClass('none');
                $(opts.contentLuocdo).find('h1').remove();
                if (activeTab == opts.luocdoTab) {
                    $(opts.halfzone).addClass('none');
                    $('.article-luocdo', $(opts.contentLuocdo)).prepend(h1Tag);
                } else {
                    $($(opts.contentLeft)).prepend(h1Tag);
                }
            } 
            //xu ly scroll muc luc
            if (activeTab == opts.noidungTab && $(opts.mucluc).length) {
                $(window).on({
                    'scroll': vbluat.progressInView,
                    'resize': vbluat.progressInView
                });
                $(window).on('scroll', docIndexScroll);
            } else {
                $(window).off({
                    'scroll': vbluat.progressInView,
                    'resize': vbluat.progressInView
                });
                $(opts.mucluc).removeClass('fixed'); 
                $(window).off('scroll', docIndexScroll);
            }
        }, path = $(location).attr('href'), h1Tag = $(opts.h1Tag);
        //setTitleAndHistory(null, path);
        showTab();
        window.onpopstate = function (event) {
            if(opts.register)
            showTab();
        };
        $(document).on('click',
            opts.tabClass,
            function (e) {
                e.preventDefault(); 
                $(this).addClass('active');
                showTab($(this).data('href'));
            });
        $(document).on('click',
            'a[class^=demuc]', '.fixmobi-mucluc .list-mluc-mobi',
            function (e) {
                e.preventDefault();
                if ($('.dropdown-toggle').length) {
                    $($('.dropdown-toggle')[0]).trigger('click');
                }
                showTab(opts.noidungTab);
                $('html, body').animate({
                    scrollTop: $('#' + $(this).attr('class')).offset().top
                }, 500);
            });
        $(document).on('click',
            'a[class^=demuc]', '#mucluc .list-mluc',
            function (e) {
                e.preventDefault();
                showTab(opts.noidungTab); 
                $('html, body').animate({
                    scrollTop: $('#'+$(this).attr('class')).offset().top
                }, 500);
            });
        $('#noidung .entry span[id^="demuc"]').DocIndexInView();
    }

    $.fn.DocIndexInView = function (options) {
        var defaults = {
            DocIndexesElement: '#mucluc .list-mluc > a'
        }
        options = $.extend(defaults, options);
        var docIndexList = [], listLinks = [];
        //lấy ds obj demuc trong nội dung
        this.each(function () {
            var self = $(this);
            var docIndex = {};
            docIndex.top = self.offset().top;
            docIndex.height = self.innerHeight();
            docIndex.bottom = docIndex.top + docIndex.height;
            docIndex.id = self[0].id;
            docIndex.obj = self; 
            if (checkdemuc(self[0].id, docIndexList) < 0)
            docIndexList.push(docIndex);
        });
        
        function checkdemuc(value, arr) {
            var status = -1;
            for (var i = 0; i < arr.length; i++) {
                if (arr[i].id == value) {
                    status = 1;
                    break;
                }
            }
            return status;
        }
        // ds demuc ở box mục lục
        if ($(options.DocIndexesElement).length) {
            $(options.DocIndexesElement).each(function () {
                listLinks.push($(this));
            });
        } else return; 
        var globalList = $(window).data('globalListDocIndex') || [];
        globalList.push({
            'options': options,
            'docIndexList': docIndexList
        }); 
        $(window).data('globalListDocIndex', globalList);
        $(window).data('globalListLinks', listLinks);

        //if (!$(window).data('DocIndexInView.initialized')) {
        //    $(window).data('DocIndexInView.initialized', true);
        //    $(window).on({
        //        'scroll': vbluat.progressInView,
        //        'resize': vbluat.progressInView
        //    });
        //}
        //$(window).trigger('resize');
    }

    $.fn.vbluatSocial = function() {
        var _company = {
            facebook: {
                url: 'https://www.facebook.com/sharer/sharer.php?s=100&p[title]={{title}}&p[summary]={{description}}&p[url]={{url}}&p[images][0]={{media}}',
                popup: {
                    width: 626,
                    height: 436
                }
            }, twitter: {
                url: 'https://twitter.com/share?url={{url}}&via={{via}}&text={{description}}',
                popup: {
                    width: 685,
                    height: 500
                }
            }, googleplus: {
                url: 'https://plus.google.com/share?url={{url}}',
                popup: {
                    width: 600,
                    height: 600
                }
            }
        }, _popup = function (company, url) {
            // center window
            var left = (window.innerWidth / 2) - (company.popup.width / 2),
                top = (window.innerHeight / 2) - (company.popup.height / 2);
            return window.open(url, '', 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + company.popup.width + ', height=' + company.popup.height + ', top=' + top + ', left=' + left);
            }, _linkFix = function (company, link) {
            // thay the template url
                var url = company.url.replace(/{{url}}/g, encodeURIComponent(link.url))
                .replace(/{{title}}/g, encodeURIComponent(link.title))
                .replace(/{{description}}/g, encodeURIComponent(link.description))
                .replace(/{{media}}/g, encodeURIComponent(link.media))
                .replace(/{{via}}/g, encodeURIComponent(link.via));

            return url;
        }

        return this.each(function () {
            var self = $(this);
            var type = self.data('type'),
                company = _company[type] || null;
            if (!company) {
                console.log('chưa set giá trị data-type.');
            }

            var link = {
                url: self.data('url') || '',
                title: self.data('title') || '',
                description: self.data('description') || '',
                media: self.data('media') || '',
                via: self.data('via') || ''
            };

            var url = _linkFix(company, link);

            if (navigator.userAgent.match(/Android|IEMobile|BlackBerry|iPhone|iPad|iPod|Opera Mini/i)) {
                self
                    .bind('touchstart', function (e) {
                        if (e.originalEvent.touches.length > 1) {
                            return;
                        }

                        self.data('touchWithoutScroll', true);
                    })
                    .bind('touchmove', function () {
                        self.data('touchWithoutScroll', false);

                        return;
                    }).bind('touchend', function (e) {
                        e.preventDefault();

                        var touchWithoutScroll = self.data('touchWithoutScroll');

                        if (e.originalEvent.touches.length > 1 || !touchWithoutScroll) {
                            return;
                        }
                        _popup(company, url);
                    });
            } else {
                self.bind('click', function (e) {
                    e.preventDefault();
                    _popup(company, url);
                });
            }
        });
    }
})(jQuery);

(function ($) {
    var queues = {};
    var activeReqs = {};

    $.lawAjax = function (qname, opts) {

        if (typeof opts === 'undefined') {
            throw ('LawAjax: opts is underfined');
        }

        var deferred = $.Deferred(),
            promise = deferred.promise();

        promise.success = promise.done;
        promise.error = promise.fail;
        promise.complete = promise.always;

        var deferredOpts = typeof opts === 'function';
        var options = !deferredOpts ? $.extend(true, {}, opts) : null;
        enqueue(function () {
            var jqXHR = $.ajax.apply(window, [deferredOpts ? opts() : options]);

            jqXHR.done(function () {
                deferred.resolve.apply(this, arguments);
            });
            jqXHR.fail(function () {
                deferred.reject.apply(this, arguments);
            });

            jqXHR.always(dequeue); 

            return jqXHR;
        });

        return promise;

        function enqueue(cb) {
            if (!queues[qname]) {
                queues[qname] = [];
                var xhr = cb();
                activeReqs[qname] = xhr;
            }
            else {
                queues[qname].push(cb);
            }
        }

        function dequeue() {
            if (!queues[qname]) {
                return;
            }
            var nextCallback = queues[qname].shift();
            if (nextCallback) {
                var xhr = nextCallback();
                activeReqs[qname] = xhr;
            }
            else {
                delete queues[qname];
                delete activeReqs[qname];
            }
        }
    };
    //Đăng ký phương thức icget và icget
    $.each(['icget', 'icpost'], function (i, method) {
        $[method] = function (options) {
            var defaults = {
                qname: 'lawsQuereFirst',
                resultId: null,
                url: '',
                type: method,
                dataType: 'json',
                data: {},
                headers: {},
                statusCode: {},
                cache: false,
                timeout: 5000,
                removeLoadmore:true,
                //xhr: function () {
                //    var xhr = new window.XMLHttpRequest();
                //    if ($('.progress').length === 0) {
                //        $('body').append('<div class="progress"></div>');
                //        //$('#loadingbar').addClass('waiting').append($('<dt/><dd/>'));
                //        //$('#loadingbar').width((50 + Math.random() * 30) + "%");
                //    }
                //    xhr.upload.addEventListener('progress', function (evt) {
                //        if (evt.lengthComputable) {
                //            var percentComplete = evt.loaded / evt.total;
                //            $('.progress').css({
                //                width: percentComplete * 100 + '%'
                //            });
                //            if (percentComplete === 1) {
                //                $('.progress').addClass('hide');
                //            }
                //        }
                //    }, false);
                //    xhr.addEventListener('progress', function (evt) {
                //        if (evt.lengthComputable) {
                //            var percentComplete = evt.loaded / evt.total;
                //            $('.progress').css({
                //                width: percentComplete * 100 + '%'
                //            });
                //        }
                //    }, false);
                //    return xhr;
                //},
                beforeSend: function () {
                    if ($('#loadingbar').length === 0) {
                        $('body').append('<div id="loadingbar"></div>');
                        $('#loadingbar').addClass('waiting').append($('<dt/><dd/>'));
                        $('#loadingbar').width((50 + Math.random() * 30) + "%");
                    }
                    if (options.removeLoadmore && $('.loadmore').length) {
                        $('.loadmore').remove();
                    }
                },
                error: function (xhr, text, e) {
                    if (xhr.status === 0) {
                        $().icpopup({ messages: ['Không có kết nối mạng. Vui lòng kiểm tra lại.'], success: false });
                    } else if (xhr.status === 404) {
                        $().icpopup({ messages: ['Không tìm thấy trang yêu cầu.'], success: false });
                    } else if (xhr.status === 500) {
                        $().icpopup({ messages: ['Lỗi máy chủ nội bộ. [500].'], success: false });
                    } else if (text === 'parsererror') {
                        $().icpopup({ messages: ['Yêu cầu phân tích cú pháp JSON lỗi.'], success: false });
                    } else if (text === 'timeout') {
                        $().icpopup({ messages: ['Hết thời gian yêu cầu.'], success: false });
                    } else if (text === 'abort') {
                        $().icpopup({ messages: ['Yêu cầu xử lý bị hủy.'], success: false });
                    } else if (xhr.status !== 403) {
                        $().icpopup({ messages: ['Lỗi :.n' + xhr.responseText], success: false });
                    }
                },
                complete: function (xhr, text) { },
                success: function (data, text, xhr) {
                    if (options.resultId != null && options.resultId && options.dataType === 'html' && data != null && data.length > 0) {
                        console.log(data)
                        $(options.resultId).html(data);
                    }
                },
                always: function() {
                    $('#loadingbar').width('101%').delay(200).fadeOut(400, function () {
                        $('#loadingbar').remove();
                    });
                }
            }
            options = $.extend(defaults, options);
            //if ($.isFunction(options.data)) {
            //    options.type = options.type || options.success;
            //    options.success = data;
            //    options.data = undefined;
            //}
            return $.lawAjax(options.qname, {
                type: options.type === 'icpost' ? 'post' : 'get',
                url: options.url,
                data: options.data,
                dataType: options.dataType,
                //xhr:options.xhr,
                beforeSend: function () {
                    if ($.isFunction(options.beforeSend)) {
                        window.setTimeout(function () {
                            options.beforeSend();
                        }, 10);
                    }
                },
                complete: function (data, status, xhr) {
                    if ($.isFunction(options.complete)) {
                        window.setTimeout(function () {
                            options.complete(data, status, xhr);
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
        };
    });

    var isQueueRunning = function (qname) {
        return (queues.hasOwnProperty(qname) && queues[qname].length > 0) || activeReqs.hasOwnProperty(qname);
    };

    var isAnyQueueRunning = function () {
        for (var i in queues) {
            if (isQueueRunning(i)) return true;
        }
        return false;
    };

    $.lawAjax.isRunning = function (qname) {
        if (qname) return isQueueRunning(qname);
        else return isAnyQueueRunning();
    };

    $.lawAjax.getActiveRequest = function (qname) {
        if (!qname) throw ('LawAjax: queue name is required');

        return activeReqs[qname];
    };

    $.lawAjax.abort = function (qname) {
        if (!qname) throw ('LawAjax: queue name is required');

        var current = $.lawAjax.getActiveRequest(qname);
        delete queues[qname];
        delete activeReqs[qname];
        if (current) current.abort();
    };

    $.lawAjax.clear = function (qname) {
        if (!qname) {
            for (var i in queues) {
                if (queues.hasOwnProperty(i)) {
                    queues[i] = [];
                }
            }
        }
        else {
            if (queues[qname]) {
                queues[qname] = [];
            }
        }
    };

})(jQuery);

(function($) {
    $.fn.icpopup = function(options) {
        var defaults = {
            popupClass: 'popup',
            loadForm: false, //popup dạng form
            formUrl: '', //url get form html
            callback: {}, //popup form callback
            success: true,
            messages: [],
            returnUrl:''
        }
        options = $.extend(defaults, options);
        options.messages = $.isArray(options.messages) ? options.messages : [];
        var level = 1, maxLevel = 1, arrayLevel=[];
        if ($('.' + options.popupClass).length > 0) {
            level = $('.' + options.popupClass).data('level') + 1;
        }
        var htmlPopup = '<div class="' + options.popupClass + '" data-level="' + level + '" data-success="' + (options.success ? 1 : 0) +'"> \
                                <button name= "close" class="dong-the"> <img src="'+ vbluat.virtualPath('/assets/images/close.png') + '"></button> \
                                <div class="content-up">';
                                if (!options.loadForm) {
                                    htmlPopup += '<p><img src="' + vbluat.virtualPath('/assets/images/' + (options.success ? 'luu-ok' : 'luu-no') +'.png') + '"></p> \
                                                <p style="font-size: 16px;"><strong>'+ options.messages[0] + '</strong></p>';
                                                if (options.messages.length > 1) {
                                                    for (var i = 1; i < options.messages.length; i++) {
                                                        htmlPopup += '<p>' + options.messages[i] +'</p>';
                                                    }
                                                }
                                } else {
                                    var xhr = new XMLHttpRequest();
                                    xhr.onreadystatechange = function () {
                                        if (this.readyState == 4 && this.status == 200) {
                                            htmlPopup += this.responseText;
                                            showPopup();   
                                            if ($.isFunction(options.callback)) {
                                                window.setTimeout(function () {
                                                    options.callback();
                                                }, 10);
                                            }
                                        } else if (this.status == 403) { //chưa login=> ném về dang-nhap.html
                                            var response = $.parseJSON(xhr.responseText);
                                            if (response.ReturnUrl != null && response.ReturnUrl.length > 0) {
                                                window.location.href = response.ReturnUrl;
                                            } else
                                                window.location.href = vbluat.virtualPath('/user/dang-nhap.html?ReturnUrl=/');
                                        }
                                    };
                                    xhr.open('GET', options.formUrl, false);
                                    xhr.setRequestHeader('X-Requested-With', 'XMLHttpRequest');
                                    xhr.send(); 
                                }

        function showPopup() {
            //trước khi show thông báo thành công -> đóng các popup trước đó
            if (options.success) {
                $('.' + options.popupClass).remove();
            }
            htmlPopup += '</div></div>';
            $('body').append(htmlPopup);
            var popup = $('.' + options.popupClass);
            popup.wrapInner('<div class="wrapper"></div>');
            if (level > 1) {
                $('.' + options.popupClass).css('background', 'rgba(100, 100, 100, 0.60)');
            }
            $('body').addClass('body-prevent');
            popup.show();
        }

        if (!options.loadForm) {
            showPopup();
        }
          
        $('.' + options.popupClass).click(function (e) {
            if (e.target === this) {
                if ($(this).is(':visible')) {
                    $('body').removeClass('body-prevent');
                    $(this).remove();
                    if (options.returnUrl.length > 0) {
                        window.setTimeout(function () {
                            window.location.href = options.returnUrl;
                        }, 100);
                    }
                }
            }
        });

        $('.' + options.popupClass).find('button[name=close]').on('click', function () {
            $('body').removeClass('body-prevent');
            if ($('.formElementError').is(':visible')) {
                $('.formElementError').remove();
            }
            arrayLevel = $('.' + options.popupClass).map(function () {
                return $(this).data('level');
            }).get();//array tất cả level của popup
            maxLevel = Math.max.apply(Math, arrayLevel);
            var elMaxLevel = $('.' + options.popupClass).filter(function() {
                return $(this).data('level') == maxLevel; //trả về chú popup có level cao nhất
            });
            //chưa success thì đóng nó
            if (elMaxLevel.data('success') == 0) {
                elMaxLevel.remove();
            } else { //còn lại đóng tất cả popup
                $('.' + options.popupClass).remove();
            }
            if (options.returnUrl.length > 0) {
                window.setTimeout(function () {
                    window.location.href = options.returnUrl;
                }, 100);
            }
        });
    }
})(jQuery);

/*print*/
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

$.fn.lawsExists = function (callback) {
    var args = [].slice.call(arguments, 1);
    if (this.length) {
        callback.call(this, args);
    }
    return this;
}



