/*
 * @example An iframe-based dialog with frame window fit dialog size.
 */
(function () {
    CKEDITOR.plugins.add('article',
    {
        requires: ['iframedialog'],
        init: function (editor) {
            
            
            CKEDITOR.dialog.add('article', this.path + 'dialogs/article.js');
            editor.addCommand('Article', new CKEDITOR.dialogCommand('article'));

            editor.ui.addButton('Article',
            {
                label: 'Chèn bài viết',
                command: 'Article',
                icon: this.path + 'images/icon.png'
            });
        }
    });

})();
