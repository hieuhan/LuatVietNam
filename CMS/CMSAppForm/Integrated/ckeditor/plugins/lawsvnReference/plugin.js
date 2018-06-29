/**
 * 24/04/2017
 * Plugin lawsvnReference
 */

// Dang ky plugin.
CKEDITOR.plugins.add('lawsvnReference',
{
    // initialization
    init: function (editor) {
        var iconPath = this.path + 'images/icon.png';

        // dk command khi click btn toolbar
        editor.addCommand('lawsvnReferenceDialog', new CKEDITOR.dialogCommand('lawsvnReferenceDialog'));
        // dk command khi edit
        editor.addCommand('lawsvnReferenceEditDialog', new CKEDITOR.dialogCommand('lawsvnReferenceEditDialog'));

        // tao button tren toolbar
        editor.ui.addButton('lawsvnReferenceToolbar',
		{
		    label: 'Thêm dẫn chiếu',
		    // tham chieu den plugin
		    command: 'lawsvnReferenceDialog',
		    icon: iconPath
		});

        // Them menu
        if (editor.contextMenu) {
            // Dk nhom menu moi.
            editor.addMenuGroup('lawsvnReferenceGroup');
            // Dk menu item
            editor.addMenuItem('lawsvnReference',
			{
			    label: 'Sửa dẫn chiếu',
			    icon: iconPath,
			    // tham chieu den plugin
			    command: 'lawsvnReferenceEditDialog',
			    // nhom menu
			    group: 'lawsvnReferenceGroup'
			});
            // Chi show menu khi editor co the <lawsvn>
            editor.contextMenu.addListener(function (element) {
                // lay element <lawsvn> gan nhat con tro chuot
                if (element)
                    element = element.getAscendant('lawsvn', true);

                if (element && !element.isReadOnly() && !element.data('cke-realelement'))
                    return { lawsvnReference: CKEDITOR.TRISTATE_OFF };

                return null;
            });
        }

        // dinh nghia dialog: UI va su kien Listen
        CKEDITOR.dialog.add('lawsvnReferenceDialog', function (editor) {
            return {
                // cac thuoc tinh cua dialog
                title: 'Tạo mới dẫn chiếu',
                minWidth: 400,
                minHeight: 200,
                // Dialog contents.
                contents:
				[
					{
					    elements:
						[
                            {
                                type: 'text',
                                id: 'lawsvn',
                                label: 'Ký tự hiển thị dẫn chiếu, ví dụ hiển thị theo thứ tự: 1,2,3...',
                                validate: CKEDITOR.dialog.validate.notEmpty("Bạn chưa nhập ký tự hiển thị dẫn chiếu !"),
                                setup: function (element) {
                                    //this.setValue(element.getText());
                                },
                                commit: function (element) {
                                    element.setHtml('<span style="font-size: smaller;color:blue;vertical-align: super;"> [' + this.getValue() + ']</span>');
                                }
                            },
							{
							    type: 'text',
							    id: 'website',
							    label: 'Website:',
							    validate: CKEDITOR.dialog.validate.notEmpty("Bạn chưa nhập link website !"),
                                //phuong thuc dc chay khi settupContent dialog dc goi
							    setup: function (element) {
							        //this.setValue(element.getText());
							    },
							    //phuong thuc dc chay khi commitContent dialog dc goi
							    commit: function (element) {
							        element.setAttribute("website", this.getValue());
							    }
							},
							{
							    type: 'textarea',
							    id: 'reference',
							    label: 'Nội dung:',
							    validate: CKEDITOR.dialog.validate.notEmpty("Bạn chưa nhập nội dung dẫn chiếu !"),
							    setup: function (element) {
							        //this.setValue(element.getAttribute("reference"));
							    },
							    commit: function (element) {
							        element.setAttribute("reference", this.getValue());
							    }
							}
						]
					}
				],
                // phuong thuc dc goi khi mo popup
                onShow: function () {
                    // lay vi tri element dc trong trong editor
                    var sel = editor.getSelection(),
					// gan element cho vi tri vung da chon
						element = sel.getStartElement();

                    // lay element <lawsvn> gan nhat voi vung chon con tro chuot
                    if (element)
                        element = element.getAscendant('lawsvn', true);

                    // tao element <lawsvn> neu chua co va set insertMode flag = true
                    if (!element || element.getName() != 'lawsvn' || element.data('cke-realelement')) {
                        element = editor.document.createElement('lawsvn');
                        this.insertMode = true;
                    }
                    else
                        this.insertMode = false;

                    this.element = element;

                    this.setupContent(this.element);
                },
                // phuong thuc dc goi khi an btn ok tren popup
                onOk: function () {
                    // doi tuong dialog
                    var dialog = this,
						lawsvn = this.element;

                    if (this.insertMode)
                        editor.insertElement(lawsvn);

                    this.commitContent(lawsvn);
                }
            };
        });

        // dinh nghia dialog: UI va su kien Listen khi edit
        CKEDITOR.dialog.add('lawsvnReferenceEditDialog', function (editor) {
            return {
                // cac thuoc tinh cua dialog
                title: 'Sửa dẫn chiếu',
                minWidth: 400,
                minHeight: 200,
                // Dialog contents.
                contents:
				[
					{
					    elements:
						[
                            {
                                type: 'text',
                                id: 'lawsvn',
                                label: 'Ký tự hiển thị dẫn chiếu, ví dụ hiển thị theo thứ tự: 1,2,3...',
                                validate: CKEDITOR.dialog.validate.notEmpty("Bạn chưa nhập ký tự hiển thị dẫn chiếu !"),
                                setup: function (element) {
                                    this.setValue(element.getText());
                                },
                                commit: function (element) {
                                    element.setHtml('<span style="font-size: smaller;color:blue;vertical-align: super;"> [' + this.getValue() + ']</span>');
                                }
                            },
							{
							    type: 'text',
							    id: 'website',
							    label: 'Website:',
							    validate: CKEDITOR.dialog.validate.notEmpty("Bạn chưa nhập link website !"),
							    //phuong thuc dc chay khi settupContent dialog dc goi
							    setup: function (element) {
							        this.setValue(element.getAttribute("data-website"));
							    },
							    //phuong thuc dc chay khi commitContent dialog dc goi
							    commit: function (element) {
							        element.setAttribute("data-website", this.getValue());
							    }
							},
							{
							    type: 'textarea',
							    id: 'reference',
							    label: 'Nội dung:',
							    validate: CKEDITOR.dialog.validate.notEmpty("Bạn chưa nhập nội dung dẫn chiếu !"),
							    setup: function (element) {
							        this.setValue(element.getAttribute("title"));
							    },
							    commit: function (element) {
							        element.setAttribute("title", this.getValue());
							    }
							}
						]
					}
				],
                // phuong thuc dc goi khi mo popup
                onShow: function () {
                    // lay vi tri element dc trong trong editor
                    var sel = editor.getSelection(),
					// gan element cho vi tri vung da chon
						element = sel.getStartElement();

                    // lay element <lawsvn> gan nhat voi vung chon con tro chuot
                    if (element)
                        element = element.getAscendant('lawsvn', true);

                    // tao element <lawsvn> neu chua co va set insertMode flag = true
                    if (!element || element.getName() != 'lawsvn' || element.data('cke-realelement')) {
                        element = editor.document.createElement('lawsvn');
                        this.insertMode = true;
                    }
                    else
                        this.insertMode = false;

                    this.element = element;

                    this.setupContent(this.element);
                },
                // phuong thuc dc goi khi an btn ok tren popup
                onOk: function () {
                    // doi tuong dialog
                    var dialog = this,
						lawsvn = this.element;

                    if (this.insertMode)
                        editor.insertElement(lawsvn);

                    this.commitContent(lawsvn);
                }
            };
        });

    }
});