CKEDITOR.dialog.add('article', function (editor) {
    return {
        title: 'Chèn bài viết vào nội dung', minWidth: 800, minHeight: 550,
        contents: [{
            id: 'tab1', label: '', title: '', expand: true, padding: 0,
            elements: [{
                type: 'iframe',
                src: '/admin/pages/articles/InsertArticle.aspx',
                width: 800, height: 550 - (CKEDITOR.env.ie ? 10 : 0)
            }]
        }]
       , buttons: [CKEDITOR.dialog.cancelButton]
    };
});