/**
 * @license Copyright (c) 2003-2014, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.html or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    //CKEDITOR.basePath = 'http://cxo.vn/ckeditor/';
    config.removeDialogTabs = 'link:upload;image:Upload;flash:Upload';
    config.enterMode = CKEDITOR.ENTER_BR;
    config.language = 'vi';
    config.allowedContent = true;
    config.height = 300;
    config.pasteFromWordRemoveFontStyles = false;
    config.pasteFromWordRemoveStyles = false;
    config.pasteFromWordPromptCleanup = false;
    // config.uiColor = '#AADC6E';
    //config.extraPlugins = 'youtube';
    //config.extraPlugins = 'jwplayer,youtube,flash';//ok
    config.extraPlugins = 'jwplayer,youtube,video,audio,article,listtable,lawsvnReference';
    //config.extraPlugins = 'media';
    config.toolbar_Basic = [
        ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', 'TextColor', 'BGColor', '-', 'RemoveFormat']
    ];
    config.toolbar_Profile = [
        ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', 'TextColor', 'BGColor', '-'
        , '/',
        'Image', 'Table', 'SpecialChar', 'PageBreak', 'Styles', 'Format', 'Font', 'FontSize', '-', 'RemoveFormat']
    ];
    config.toolbar_DocIndex = [
        ['ListTable', 'Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', 'TextColor', 'BGColor', '-', 'RemoveFormat', 'Source', 'Maximize']
    ];
    config.toolbar = [['lawsvnReferenceToolbar'],['Source', '-', 'Save', 'NewPage', 'Preview', '-', 'Templates'],
    ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Print', 'SpellChecker', 'Scayt'],
    ['Undo', 'Redo', '-', 'Find', 'Replace', '-', 'SelectAll', 'RemoveFormat'],
    ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', 'Blockquote', 'CreateDiv'],
    ['Link', 'Unlink', 'Anchor'],
    '/',
    ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
    ['Bold', 'Italic', 'Underline', 'Strike', '-', 'Subscript', 'Superscript'],
    ['Styles', 'Format', 'Font', 'FontSize'],
    ['TextColor', 'BGColor'],
    ['Image', 'Youtube', 'Video', 'Audio', 'jwplayer', 'Flash', 'Iframe', 'Table', 'Article', 'HorizontalRule', 'Smiley', 'SpecialChar'],
    ['Maximize', 'ShowBlocks']];
    
};
