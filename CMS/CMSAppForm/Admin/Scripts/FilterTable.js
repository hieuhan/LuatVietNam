var caution = false;
var rootpath = '/';
//---------------------------------------------------------------------------------------------------------------
//---------------------------------------------------------------------------------------------------------------
var c_name = "FilterItems";
var split_1	= '|'; //-- Ngăn cách giữa các cặp giá trị tạo nên một sản phẩm.
var split_2	= ','; //-- Ngăn cách giữa ID và số lượng.
var FilterWindow = null;
function addToFilter(Page_id, Page_State)
{

    c = $.cookie(c_name);
	if(!c) c = "";
	else
	{		
		cc = c.split(split_1);
		for(i = 0; i < cc.length; i++)
		{
			if(cc[i])
			{
				s = cc[i].split(split_2);
				if (s[0] == Page_id) {				    
					return false;//
				}
 				
			}
        }
        if (i >=60) {
            return false; 
        }
	}
	if (c != "")
	    c = Page_id + split_2 + Page_State + split_1 + c;
	else
	    c = Page_id + split_2 + Page_State;

	$.cookie(c_name, c, { expires: 1,  path: '/' });

}
function getItemCount() {
    c = $.cookie(c_name);    
    if (!c) return 0;
    else {
        cc = c.split(split_1);        
        return cc.length;
        
    }
}
function getItem(Page_id) {
    c = $.cookie(c_name);
    var nc = '';
    if (!c) return '';
    else {
        cc = c.split(split_1);
        for (i = 0; i < cc.length; i++) {
            if (cc[i]) {
                s = cc[i].split(split_2);
                if (s[0] == Page_id) {
                    return s[1];
                }               
            }
        }

    }
    return nc;
}
function updateItem(Page_id, Page_State) {
    c = $.cookie(c_name);
    var nc = '';
    if (!c) return false;
    else {
        cc = c.split(split_1);
        for (i = 0; i < cc.length; i++) {
            if (cc[i]) {
                s = cc[i].split(split_2);
                if (s[0] != Page_id) {
                    if (nc == '')
                        nc = s[0] + split_2 + s[1];
                    else
                        nc = nc + split_1 + s[0] + split_2 + s[1];
                }
                else {
                    s = cc[i].split(split_2);                    
                    if (nc == '')
                        nc = s[0] + split_2 + Page_State;
                    else
                        nc = nc + split_1 + s[0] + split_2 + Page_State;
                }
            }
        }
        if (nc == '') {
            $.cookie(c_name, null, { path: '/' });
        }
        else {
            $.cookie(c_name, nc, { expires: 1,  path: '/' });
        }
    }
}
function removeFromFilter(Page_id)
{
    c = $.cookie(c_name); 
    var nc = '';	
	if(!c) return false;
	else
	{
	    cc = c.split(split_1);        
		for (i = 0; i < cc.length; i++)
		{
			if(cc[i])
			{
			    s = cc[i].split(split_2);			    
				if( s[0] != Page_id )
				{
                    if(nc == '')
                        nc = s[0] + split_2 + s[1];
                    else
                        nc = nc + split_1 + s[0] + split_2 + s[1];
				}
			}
        }
        if (nc == '') {
            $.cookie(c_name, null, { path: '/' });           
        }
        else {
            $.cookie(c_name, nc, { expires: 1,  path: '/' });
        }
	}	
}
function IsNumeric(input) {
    return (input - 0) == input && input.length > 0;
}

function emptyFilter()
{
	
	    $.cookie(c_name, null, { path: '/' });   

}
//===================== other ==========
function ShowOtherInfo(divId) {
    $('#OtherInfoHide' + divId).fadeToggle("fast");
}
function ShowTrueImage(divId) {
    $('#TrueImageHide' + divId).fadeToggle("fast");
}
function FindSelect(txtId, ContainerId, SelectId) {
    var valuesearch = $(txtId).val();
    if (valuesearch.length < 3)
        return;
    var i;
    var options = $('select[id *= "' + SelectId + '"]').find('option');
    $('#' + ContainerId).html("");
    $('#' + ContainerId).show();
    for (i = 0; i < options.length; i++) {
        if (options[i].innerHTML.toLowerCase().search(valuesearch.toLowerCase()) > -1) {
            AddRadio(ContainerId, SelectId, options[i].getAttribute('value'), options[i].innerHTML);
        }
    }
}
function AddRadio(ContainerId, SelectId, instr, strname) {
    var newhtml = $('#' + ContainerId).html();
    newhtml += "<p class='searchitem' onclick = ChangeSelect('" + ContainerId + "','" + SelectId + "','" + instr + "',this) >";
    newhtml += "<input type='radio' name='selectcate' rel='" + instr + "'  onclick = ChangeSelect('" + ContainerId + "','" + SelectId + "','" + instr + "',this)  >";
    newhtml += strname + "</p>";
    $('#' + ContainerId).html(newhtml);
}
function ChangeSelect(ContainerId, SelectId, chkid, status) {
    var strid = '';
    strid += chkid + '';
    var options = $('select[id *= "' + SelectId + '"]').find('option');
    for (i = 0; i < options.length; i++) {
        if (options[i].value == chkid) {
            options[i].selected = 'selected';
            break;
        }
    }
    $('#' + ContainerId).hide();
}
function SelectAll(CheckBoxControl) {
    if (CheckBoxControl.checked == true) {
        var i;
        for (i = 0; i < document.forms[0].elements.length; i++) {
            if ((document.forms[0].elements[i].type == 'checkbox') && (document.forms[0].elements[i].name.indexOf('chkAction') > -1)) {
                document.forms[0].elements[i].checked = true;
            }
        }
    }
    else {
        var i;
        for (i = 0; i < document.forms[0].elements.length; i++) {
            if ((document.forms[0].elements[i].type == 'checkbox') && (document.forms[0].elements[i].name.indexOf('chkAction') > -1)) {
                document.forms[0].elements[i].checked = false;
            }
        }
    }
}
function HideEdit(EditId) {
    $(EditId).hide('fast');
}
function ShowMenu(MenuId) {
    if ($("#dropdown" + MenuId).css("display") != "block")
        $(".dropdown-menu-hover" + MenuId).dropdown("show");
}
function SetLanguage(LanguageName) {
    var d = new Date();
    var n = d.getTime();
    var LanguagePannel = $('<div id="LanguagePannel" style="display:none"></div>');
    var url = rootpath + "Admin/Ajaxs/SetLanguage.aspx?LanguageName=" + LanguageName + "&tst=" + n;
    LanguagePannel.load(url, function (response, status, xhr) {
        if (status == "error") {
            return;
        }
        else {
            window.location = window.location;
        }
    });
}
function MsgConfirm(objCall, TitleMsg, MessageConfirm, OkValue, CancelValue) {
    $.msgBox({
        type: "confirm",
        title: TitleMsg,
        content: MessageConfirm,
        buttons: [{ value: OkValue }, { value: CancelValue}],
        success: function (result) {
            if (result == OkValue) {
                //alert($(objCall).attr("href"))
                window.location = $(objCall).attr("href");
            }
            else {
                return false;
            }
        }
    });
    return false;
}
function MsgAlert(TitleMsg, MessageConfirm, OkValue) {
    $.msgBox({
        type: "alert",
        title: TitleMsg,
        content: MessageConfirm,
        buttons: [{ value: OkValue }]
    });
}
function closefilter() {
    $('.filterare').fadeToggle("fast", function () {

        if (getItem(PageId) == '')
            addToFilter(PageId, $('.filterare').css("display"));
        else
            updateItem(PageId, $('.filterare').css("display"));
    });
}
function SelectImage(alt) {
    var finder = new CKFinder();
    finder.basePath = '/Integrated/ckfinder/';

    //Startup path in a form: "Type:/path/to/directory/"
    finder.startupPath = 'Images:/images/';
    // Name of a function which is called when a file is selected in CKFinder.
    finder.selectActionFunction = SetImageField;
    // Additional data to be passed to the selectActionFunction in a second argument.
    // We'll use this feature to pass the Id of a field that will be updated.
    finder.selectActionData = alt;
    finder.selectThumbnailActionFunction = SetImageField;
    // Launch CKFinder
    finder.popup();
}
function SetImageField( fileUrl, data )
{	    
    if(fileUrl != '')
    {
        var alt = data["selectActionData"];
        $('img[alt="' + alt + '"]').attr("src", fileUrl);
        $('input[id*="' + alt + '"]').val(fileUrl);
    }
}
$(document).ready(function () {
    SetStartup();
    $(".uiselect").chosen({ width: "100%", no_results_text: "Không có dữ liệu " });

});
function SetStartup() {
    var txtImagePath = $('img.ImageSelectPath').attr("alt");
    var curentImagePath = $('input[id*="' + txtImagePath + '"]').val();
    if (curentImagePath != "") {
        $('img.ImageSelectPath').attr("src", curentImagePath);
    }
    
    $('img.ImageSelectPath').live('dblclick', function (e) {
        var alt = $(this).attr("alt")
        SelectImage(alt);
        e.preventDefault();
    });
    // filer table
    $('table.jquery-filter-table').filterTable({ filterSelector: 'input.input-filter' });
}