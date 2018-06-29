﻿/*
 * CKFinder
 * ========
 * http://cksource.com/ckfinder
 * Copyright (C) 2007-2014, CKSource - Frederico Knabben. All rights reserved.
 *
 * The software, this file, and its contents are subject to the CKFinder
 * License. Please read the license.txt file before using, installing, copying,
 * modifying, or distributing this file or part of its contents. The contents of
 * this file is part of the Source Code of CKFinder.
 *
 */

/**
 * @fileOverview Defines the {@link CKFinder.lang} object for the Vietnamese
 *		language.
 */

/**
 * Contains the dictionary of language entries.
 * @namespace
 */
CKFinder.lang['vi'] =
{
	appTitle : 'CKFinder',

	// Common messages and labels.
	common :
	{
		// Put the voice-only part of the label in the span.
		unavailable		: '%1<span class="cke_accessibility">, không khả dụng</span>',
		confirmCancel	: 'Vài tùy chọn đã thay đổi. Bạn có muốn đóng hộp thoại?',
		ok				: 'OK',
		cancel			: 'Hủy',
		confirmationTitle	: 'Xác nhận',
		messageTitle	: 'Thông tin',
		inputTitle		: 'Câu hỏi',
		undo			: 'Hoàn tác',
		redo			: 'Làm lại',
		skip			: 'Bỏ qua',
		skipAll			: 'Bỏ qua tất cả',
		makeDecision	: 'Chọn hành động nào?',
		rememberDecision: 'Ghi nhớ quyết định này'
	},


	// Language direction, 'ltr' or 'rtl'.
	dir : 'ltr',
	HelpLang : 'en',
	LangCode : 'vi',

	// Date Format
	//		d    : Day
	//		dd   : Day (padding zero)
	//		m    : Month
	//		mm   : Month (padding zero)
	//		yy   : Year (two digits)
	//		yyyy : Year (four digits)
	//		h    : Hour (12 hour clock)
	//		hh   : Hour (12 hour clock, padding zero)
	//		H    : Hour (24 hour clock)
	//		HH   : Hour (24 hour clock, padding zero)
	//		M    : Minute
	//		MM   : Minute (padding zero)
	//		a    : Firt char of AM/PM
	//		aa   : AM/PM
	DateTime : 'd/m/yyyy h:MM aa',
	DateAmPm : ['SA', 'CH'],

	// Folders
	FoldersTitle	: 'Thư mục',
	FolderLoading	: 'Đang tải...',
	FolderNew		: 'Xin chọn tên cho thư mục mới: ',
	FolderRename	: 'Xin chọn tên mới cho thư mục: ',
	FolderDelete	: 'Bạn có chắc muốn xóa thư mục "%1"?',
	FolderRenaming	: ' (Đang đổi tên...)',
	FolderDeleting	: ' (Đang xóa...)',
	DestinationFolder	: 'Thư mục đích', // MISSING

	// Files
	FileRename		: 'Xin nhập tên tập tin mới: ',
	FileRenameExt	: 'Bạn có chắc muốn đổi phần mở rộng? Tập tin có thể sẽ không dùng được.',
	FileRenaming	: 'Đang đổi tên...',
	FileDelete		: 'Bạn có chắc muốn xóa tập tin "%1"?',
	FilesDelete	: 'Bạn có chắc muốn xóa %1 tệp tin?', // MISSING
	FilesLoading	: 'Đang tải...',
	FilesEmpty		: 'Thư mục trống.',
	DestinationFile	: 'file đích', // MISSING
	SkippedFiles	: 'Danh sách tệp tin bỏ qua:', // MISSING

	// Basket
	BasketFolder		: 'Thư mục tạm',
	BasketClear			: 'Dọn thư mục tạm',
	BasketRemove: 'Xóa khỏi thư mục tạm',
	BasketOpenFolder	: 'Mở thư mục cha',
	BasketTruncateConfirm: 'Bạn có chắc muốn bỏ tất cả tập tin trong thư mục tạm?',
	BasketRemoveConfirm: 'Bạn có chắc muốn bỏ tập tin "%1" khỏi thư mục tạm?',
	BasketRemoveConfirmMultiple	: 'Bạn có chắc muốn xóa %1 tệp tin khỏi thư mục tạm?', // MISSING
	BasketEmpty: 'Không có tập tin trong thư mục tạm, hãy kéo và thả tập tin vào thư mục tạm.',
	BasketCopyFilesHere: 'Chép tập tin từ thư mục tạm',
	BasketMoveFilesHere: 'Chuyển tập tin từ thư mục tạm',

	// Global messages
	OperationCompletedSuccess	: 'Thao tác hoàn thành thành công.', // MISSING
	OperationCompletedErrors		: 'Thao tác thất bại.', // MISSING
	FileError				: '%s: %e', // MISSING

	// Move and Copy files
	MovedFilesNumber		: 'Số tệp tin đã chuyển: %s.', // MISSING
	CopiedFilesNumber	: 'Số tệp tin đã sao chép: %s.', // MISSING
	MoveFailedList		: 'Các tệp tin sau không thể di chuyển:<br />%s', // MISSING
	CopyFailedList		: 'Các tệp tin sau không thể sao chép:<br />%s', // MISSING

	// Toolbar Buttons (some used elsewhere)
	Upload		: 'Tải lên',
	UploadTip	: 'Tải tập tin mới',
	Refresh		: 'Làm mới',
	Settings	: 'Thiết lập',
	Help		: 'Hướng dẫn',
	HelpTip		: 'Hướng dẫn',

	// Context Menus
	Select			: 'Chọn',
	SelectThumbnail : 'Chọn ảnh mẫu',
	View			: 'Xem',
	Download		: 'Tải về',

	NewSubFolder	: 'Tạo thư mục con',
	Rename			: 'Đổi tên',
	Delete			: 'Xóa',
	DeleteFiles		: 'Xóa nhiều tệp tin', // MISSING

	CopyDragDrop	: 'Sao chép ở đây',
	MoveDragDrop	: 'Di chuyển ở đây',

	// Dialogs
	RenameDlgTitle		: 'Đổi tên',
	NewNameDlgTitle		: 'Tên mới',
	FileExistsDlgTitle	: 'Tập tin đã tồn tại',
	SysErrorDlgTitle : 'Lỗi hệ thống',

	FileOverwrite	: 'Ghi đè',
	FileAutorename	: 'Tự đổi tên',
	ManuallyRename	: 'Đổi tên', // MISSING

	// Generic
	OkBtn		: 'OK',
	CancelBtn	: 'Hủy bỏ',
	CloseBtn	: 'Đóng',

	// Upload Panel
	UploadTitle			: 'Tải tập tin mới',
	UploadSelectLbl		: 'Chọn tập tin tải lên',
	UploadProgressLbl	: '(Đang tải lên, vui lòng chờ...)',
	UploadBtn			: 'Tải tập tin đã chọn',
	UploadBtnCancel		: 'Hủy bỏ',

	UploadNoFileMsg		: 'Xin chọn một tập tin trong máy tính.',
	UploadNoFolder		: 'Xin chọn thư mục trước khi tải lên.',
	UploadNoPerms		: 'Không được phép tải lên.',
	UploadUnknError		: 'Lỗi khi tải tập tin.',
	UploadExtIncorrect	: 'Kiểu tập tin không được chấp nhận trong thư mục này.',

	// Flash Uploads
	UploadLabel			: 'Tập tin sẽ tải:',
	UploadTotalFiles	: 'Tổng số tập tin:',
	UploadTotalSize		: 'Dung lượng tổng cộng:',
	UploadSend			: 'Tải lên',
	UploadAddFiles		: 'Thêm tập tin',
	UploadClearFiles	: 'Xóa tập tin',
	UploadCancel		: 'Hủy tải',
	UploadRemove		: 'Xóa',
	UploadRemoveTip		: 'Xóa !f',
	UploadUploaded		: 'Đã tải !n%',
	UploadProcessing	: 'Đang xử lí...',

	// Settings Panel
	SetTitle		: 'Thiết lập',
	SetView			: 'Xem:',
	SetViewThumb	: 'Ảnh mẫu',
	SetViewList		: 'Danh sách',
	SetDisplay		: 'Hiển thị:',
	SetDisplayName	: 'Tên tập tin',
	SetDisplayDate	: 'Ngày',
	SetDisplaySize	: 'Dung lượng',
	SetSort			: 'Sắp xếp:',
	SetSortName		: 'theo tên',
	SetSortDate		: 'theo ngày',
	SetSortSize		: 'theo dung lượng',
	SetSortExtension		: 'theo phần mở rộng',

	// Status Bar
	FilesCountEmpty : '<Thư mục rỗng>',
	FilesCountOne	: '1 tập tin',
	FilesCountMany	: '%1 tập tin',

	// Size and Speed
	Kb				: '%1 KB',
	Mb				: '%1 MB',
	Gb				: '%1 GB',
	SizePerSecond	: '%1/s',

	// Connector Error Messages.
	ErrorUnknown	: 'Không thể hoàn tất yêu cầu. (Lỗi %1)',
	Errors :
	{
	 10 : 'Lệnh không hợp lệ.',
	 11 : 'Kiểu tài nguyên không được chỉ định trong yêu cầu.',
	 12 : 'Kiểu tài nguyên yêu cầu không hợp lệ.',
	102 : 'Tên tập tin hay thư mục không hợp lệ.',
	103 : 'Không thể hoàn tất yêu cầu vì giới hạn quyền.',
	104 : 'Không thể hoàn tất yêu cầu vì giới hạn quyền của hệ thống tập tin.',
	105 : 'Phần mở rộng tập tin không hợp lệ.',
	109 : 'Yêu cầu không hợp lệ.',
	110 : 'Lỗi không xác định.',
	111 : 'Kích cỡ tệp tin quá giới hạn.', // MISSING
	115 : 'Tập tin hoặc thư mục cùng tên đã tồn tại.',
	116 : 'Không thấy thư mục. Hãy làm mới và thử lại.',
	117 : 'Không thấy tập tin. Hãy làm mới và thử lại.',
	118 : 'Đường dẫn nguồn và đích giống nhau.',
	201 : 'Tập tin cùng tên đã tồn tại. Tập tin vừa tải lên được đổi tên thành "%1".',
	202 : 'Tập tin không hợp lệ.',
	203 : 'Tập tin không hợp lệ. Dung lượng quá lớn.',
	204 : 'Tập tin tải lên bị hỏng.',
	205 : 'Không có thư mục tạm để tải tập tin.',
	206 : 'Huỷ tải lên vì lí do bảo mật. Tập tin chứa dữ liệu giống HTML.',
	207 : 'Tập tin được đổi tên thành "%1".',
	300 : 'Di chuyển tập tin thất bại.',
	301 : 'Chép tập tin thất bại.',
	500 : 'Bạn chưa đăng nhập hoặc phiên làm việc hết hạn, mời bạn đăng nhập lại.',
	501: 'Chức năng hỗ trợ ảnh mẫu bị vô hiệu.',
    502 : 'Thư mục này chỉ để chứa các ảnh với kích thước khác được sinh ra tự động.'
	},

	// Other Error Messages.
	ErrorMsg :
	{
		FileEmpty		: 'Không thể để trống tên tập tin.',
		FileExists		: 'Tập tin %s đã tồn tại.',
		FolderEmpty		: 'Không thể để trống tên thư mục.',
		FolderExists	: 'Thư mục %s đã tồn tại.', // MISSING
		FolderNameExists	: 'Thư mục đã tồn tại.', // MISSING

		FileInvChar		: 'Tên tập tin không thể chưa các kí tự: \n\\ / : * ? " < > |',
		FolderInvChar	: 'Tên thư mục không thể chứa các kí tự: \n\\ / : * ? " < > |',

		PopupBlockView	: 'Không thể mở tập tin trong cửa sổ mới. Hãy kiểm tra trình duyệt và tắt chức năng chặn popup trên trang web này.',
		XmlError		: 'Không thể nạp hồi đáp XML từ máy chủ web.',
		XmlEmpty		: 'Không thể nạp hồi đáp XML từ máy chủ web. Dữ liệu rỗng.',
		XmlRawResponse	: 'Hồi đáp thô từ máy chủ: %s'
	},

	// Imageresize plugin
	Imageresize :
	{
		dialogTitle		: 'Đổi kích thước %s',
		sizeTooBig		: 'Không thể đặt chiều cao hoặc rộng to hơn kích thước gốc (%size).',
		resizeSuccess	: 'Đổi kích thước ảnh thành công.',
		thumbnailNew	: 'Tạo ảnh mẫu mới',
		thumbnailSmall	: 'Nhỏ (%s)',
		thumbnailMedium	: 'Vừa (%s)',
		thumbnailLarge	: 'Lớn (%s)',
		newSize			: 'Chọn kích thước mới',
		width			: 'Rộng',
		height			: 'Cao',
		invalidHeight	: 'Chiều cao không hợp lệ.',
		invalidWidth	: 'Chiều rộng không hợp lệ.',
		invalidName		: 'Tên tập tin không hợp lệ.',
		newImage		: 'Tạo ảnh mới',
		noExtensionChange : 'Không thể thay đổi phần mở rộng.',
		imageSmall		: 'Ảnh nguồn quá nhỏ.',
		contextMenuName	: 'Đổi kích thước',
		lockRatio		: 'Khoá tỉ lệ',
		resetSize		: 'Đặt lại kích thước'
	},

	// Fileeditor plugin
	Fileeditor :
	{
		save			: 'Lưu',
		fileOpenError	: 'Không thể mở tập tin.',
		fileSaveSuccess	: 'Lưu tập tin thành công.',
		contextMenuName	: 'Sửa',
		loadingFile		: 'Đang tải tập tin, xin chờ...'
	},

	Maximize :
	{
		maximize : 'Toàn màn hình',
		minimize : 'Thu nhỏ'
	},

	Gallery :
	{
		current : 'Hình thứ {current} trên {total}'
	},

	Zip :
	{
		extractHereLabel	: 'Giải nén tại đây', // MISSING
		extractToLabel		: 'Giải nén tới...', // MISSING
		downloadZipLabel	: 'Tải với định dạng nén zip', // MISSING
		compressZipLabel	: 'Nén với định dạng zip', // MISSING
		removeAndExtract	: 'Xóa tệp tin đang có và giải nén', // MISSING
		extractAndOverwrite	: 'Giải nén và ghi đè tất cả', // MISSING
		extractSuccess		: 'Giải nén thành công.' // MISSING
	},

	Search :
	{
		searchPlaceholder : 'Tìm kiếm'
	}
};