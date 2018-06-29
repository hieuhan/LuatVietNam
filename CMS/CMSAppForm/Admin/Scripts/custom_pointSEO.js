var id_content = "m_contentBody";
var id_txtArticleTitle = "txtTitle";
var id_txtArticleLead = "txtSummaryPlain";
var id_txtArticleContent = "txtContent";
var id_txSEOTitle = "txtSEOTitle";
var id_txSEODescription = "txtSEODesc";
var id_txSEOKeyword = "txtSEOKeyword";
var id_hdfSEOPoint = "hdfSEOPoint";
var l_keyword = [];
var l_pkeyword = [];
var pSeo = 0;
var strMes = "";
function getPointSeoTitle() {
    var NumExitKeyWordTitle = 0;
    var txSEOTitle = $("#" + id_content + "_" + id_txSEOTitle);
    var valSEOTitle = txSEOTitle.val();
    if (valSEOTitle.length > 0) {
        //Tiều đề bài viết không vượt quá 65 ký tự
        if (valSEOTitle.length != 0) {
            if (valSEOTitle.length <= 65) {
                pSeo += 15;
                strMes += "» TitleSeo có độ dài: " + valSEOTitle.length + ", +15<br/>";
            } else {
                strMes += "» TitleSeo nên có độ dài không quá 65 ký tự, +0<br/>";
            }
        } else {
            strMes += "» Không có TitleSeo<br/>";
            return false;
        }
        //Tiêu bài viết chứa từ khóa SEO (Từ khóa nội dung)
        //Từ khóa SEO có xu thế từ trên xuống dưới, từ trái sang phải 
        l_keyword = []; l_pkeyword = [];
        var txSEOKeyword = $("#" + id_content + "_" + id_txSEOKeyword);
        l_keyword = txSEOKeyword.val().toLowerCase().split(',');
        var isKeyTitle = 0;
        var isPos = 0;
        var Pos_mes = "";
        var Exit_mes = "";
        if (l_keyword.length > 0) {
            valSEOTitle = ConvertToUnSign(valSEOTitle);
            var titleP = allIndexOf(valSEOTitle, ConvertToUnSign(l_keyword[0].trim()));
            l_pkeyword.push(titleP);
            if (titleP.length > 0) {
                NumExitKeyWordTitle = titleP.length;
                Exit_mes += l_keyword[0].trim() + ",";
            }
            if (isKeyTitle == 0 && titleP.length > 0) {
                isKeyTitle = 1;
            }
            if (isPos == 0 && titleP.length > 0) {
                for (var i_titleP in titleP) {
                    if (isPos == 0) {
                        if (i_titleP <= (valSEOTitle.length / 2)) {
                            isPos = 1;
                            Pos_mes = "» Vị trí của từ khóa nằm ở nửa đầu của SeoTitle, +5";
                        } else {
                            isPos = 2;
                            Pos_mes = "» Vị trí của từ khóa nằm ở nửa dưới của SeoTitle, +3";
                        }
                    }
                    if (isPos == 1) {
                        break;
                    }
                }
            }
        } else {
            return;
        }
        if (isKeyTitle == 1) {
            pSeo += 15;
            strMes += "» Trong SeoTitle có chứa những từ khóa: " + Exit_mes + " +15<br/>";
        }
        if (isPos == 1) {
            pSeo += 5;
            strMes += Pos_mes + "<br/>";
        }
        if (isPos == 2) {
            pSeo += 3;
            strMes += Pos_mes + "<br/>";
        }
        if (isPos == 0) {
            strMes += "» Không tồn tại từ khoá trong SEOTitle, +0<br/>";
        }
    }
    if (NumExitKeyWordTitle > 0) {
        $("#kf_ptitle").addClass("c_Yes_K");
        $("#kf_ptitle").removeClass("c_No_K");
        $("#kf_ptitle").text("Yes(" + NumExitKeyWordTitle + ")");

        $("#kf_purl").removeClass("c_No_K");
        $("#kf_purl").addClass("c_Yes_K");
        $("#kf_purl").text("Yes(" + NumExitKeyWordTitle + ")");
    } else {
        $("#kf_ptitle").addClass("c_No_K");
        $("#kf_ptitle").removeClass("c_Yes_K");
        $("#kf_ptitle").text("No");

        $("#kf_purl").removeClass("c_Yes_K");
        $("#kf_purl").addClass("c_No_K");
        $("#kf_purl").text("No");
    }
}
function getPointSeoDesc() {
    var NumExitKeyWordDesc = 0;
    var txSEODescription = $("#" + id_content + "_" + id_txSEODescription);
    var valSEODescription = txSEODescription.val();
    if (valSEODescription.length > 0) {
        //Tiều đề bài viết không vượt quá 65 ký tự
        if (valSEODescription.length != 0) {
            if (valSEODescription.length <= 165) {
                pSeo += 15;
                strMes += "» SEODescription có độ dài: " + valSEODescription.length + ", +15<br/>";
            } else {
                strMes += "» SEODescription nên có độ dài không vượt quá 165 ký tự, +0<br/>";
            }
        } else {
            strMes += "» Không có SEODescription<br/>";
            return false;
        }
        //Tiêu bài viết chứa từ khóa SEO (Từ khóa nội dung)
        //Từ khóa SEO có xu thế từ trên xuống dưới, từ trái sang phải
        l_keyword = []; l_pkeyword = [];
        var txSEOKeyword = $("#" + id_content + "_" + id_txSEOKeyword);
        l_keyword = txSEOKeyword.val().toLowerCase().split(',');
        var isSEODescription = 0;
        var isPos = 0;
        var Pos_mes = "";
        var Exit_mes = "";
        if (l_keyword.length > 0) {
            valSEODescription = ConvertToUnSign(valSEODescription); 0
            var titleP = allIndexOf(valSEODescription, ConvertToUnSign(l_keyword[0].trim()));
            l_pkeyword.push(titleP);
            if (titleP.length > 0) {
                NumExitKeyWordDesc = titleP.length;
                Exit_mes += l_keyword[0].trim() + ",";
            }
            if (isSEODescription == 0 && titleP.length > 0) {
                isSEODescription = 1;
            }
            if (isPos == 0 && titleP.length > 0) {
                for (var i_titleP in titleP) {
                    if (isPos == 0) {
                        if (i_titleP <= (valSEODescription.length / 2)) {
                            isPos = 1;
                            Pos_mes = "» Vị trí của từ khóa nằm ở nửa đầu của SEODescription, +5";
                        } else {
                            isPos = 2;
                            Pos_mes = "» Vị trí của từ khóa nằm ở nửa dưới của SEODescription, +3";
                        }
                    }
                    if (isPos == 1) {
                        break;
                    }
                }
            } 0
        } else {
            return;
        }
        if (isSEODescription == 1) {
            pSeo += 15;
            strMes += "» Trong SEODescription có chứa những từ khóa: " + Exit_mes + " +15<br/>";
        }
        if (isPos == 1) {
            pSeo += 5;
            strMes += Pos_mes + "<br/>";
        }
        if (isPos == 2) {
            pSeo += 3;
            strMes += Pos_mes + "<br/>";
        }
        if (isPos == 0) {
            strMes += "» Không tồn tại từ khoá trong SEODescription, +0<br/>";
        }
    }
    if (NumExitKeyWordDesc > 0) {
        $("#kf_mdesc").addClass("c_Yes_K");
        $("#kf_mdesc").removeClass("c_No_K");
        $("#kf_mdesc").text("Yes(" + NumExitKeyWordDesc + ")");
    } else {
        $("#kf_mdesc").addClass("c_No_K");
        $("#kf_mdesc").removeClass("c_Yes_K");
        $("#kf_mdesc").text("No");
    }
}

function getPointContent() {
    var NumExitKeyWordContent = 0;
    var editor = CKEDITOR.instances.m_contentBody_txtContent;
    var htmldata = editor.getData();
    if (!htmldata) {
        var myVarhtmData = setInterval(function () { CheckhtmlData() }, 1000);
        function CheckhtmlData() {
            htmldata = CKEDITOR.instances.m_contentBody_txtContent.getData();
            if (htmldata) {
                clearInterval(myVarhtmData);
            }
        }
    }
    if (htmldata && htmldata.length > 0) {
        var textdata = $(htmldata).text();
        var countWord = countWords(textdata);
        l_keyword = [];
        var txSEOKeyword = $("#" + id_content + "_" + id_txSEOKeyword);
        l_keyword = txSEOKeyword.val().toLowerCase().split(',');
        var isKeyContent = 0;
        var cWKey = 0;
        var Exit_mes = "";
        if (l_keyword.length > 0) {
            textdata = ConvertToUnSign(textdata);
            var titleP = allIndexOf(textdata, ConvertToUnSign(l_keyword[0].trim()));
            if (titleP.length > 0) {
                NumExitKeyWordContent = titleP.length;
                Exit_mes += l_keyword[0].trim() + ",";
                isKeyContent = titleP.length;
                cWKey = countWords(l_keyword[0].trim()) * titleP.length;
            }
        } else {
            return;
        }
        if (isKeyContent > 0) {
            var gt_pt = (cWKey * 100) / countWord;
            gt_pt = Math.round(parseFloat(gt_pt) * 1000) / 1000;
            pSeo += 6;
            strMes += "» Trong Content có chứa những từ khóa: " + Exit_mes + " có " + isKeyContent + " lần xuất hiện gồm cả các từ khóa biến thể, +6<br/>";

            if (gt_pt < 2 || (gt_pt <= 5 & gt_pt > 4)) {
                pSeo += 2;
                strMes += "» Trong Content có chứa những từ khóa: " + Exit_mes + " chiếm " + gt_pt + "% nội dung Content, +2<br/>";
            }
            if (gt_pt >= 2 & gt_pt <= 4) {
                pSeo += 4;
                strMes += "» Trong Content có chứa những từ khóa: " + Exit_mes + " chiếm " + gt_pt + "% nội dung Content, +4<br/>";
            }
        } else {
            strMes += "» Trong Content không chứa từ khóa, +0<br/>";
        }
        var $strhtmldata = $("<html><body><div>" + htmldata + "</div></body></html>");
        var counKeyHeading = 0;
        //H1
        var h1s = $strhtmldata.find("h1");
        if (h1s.length > 0) {
            strMes += "» Trong Content có chứa " + (parseInt(h1s.length) + 1) + " thẻ h1<br/>";
        } else {
            strMes += "» Trong Content có chứa 1 thẻ h1, +3<br/>";
            pSeo += 3;
        }
        //Sapo 
        var h2s = $strhtmldata.find('h2');
        if (h2s.length > 0) {
            var isSapo = 0;
            for (var i = 0; i < h2s.length; i++) {
                var ih2s = h2s[i];
                var text_ih2s = $(ih2s).text().trim();
                if (text_ih2s.length > 0) {
                    var valSEOKeyword = txSEOKeyword.val().trim().toLowerCase().split(',')[0];
                    if (valSEOKeyword.length > 0) {
                        var text_ih2s_ = ConvertToUnSign(text_ih2s);
                        var numKeyH2 = allIndexOf(text_ih2s_, ConvertToUnSign(valSEOKeyword));
                        if (numKeyH2.length > 0) {
                            counKeyHeading += numKeyH2.length;
                        }
                    }
                    var index_text_ih2s = ConvertToUnSign(textdata.trim()).indexOf(ConvertToUnSign(text_ih2s));
                    if (index_text_ih2s == 0) {
                        isSapo = 1;
                        strMes += "» Trong Content có chứa thẻ h2 là Sapo của bài viết, +4<br/>";
                        pSeo += 4;
                        break;
                    }

                }
                if (isSapo == 0) {
                    strMes += "» Trong Content có chứa thẻ h2 nhưng không phải là Sapo<br/>";
                }
            }
        } else {
            strMes += "» Trong Content không có chứa thẻ h2<br/>";
        }

        var h3s = $strhtmldata.find("h3");
        if (h3s.length > 0) {
            strMes += "» Trong Content có chứa thẻ h3, +3<br/>";
            pSeo += 3;
            for (var i = 0; i < h3s.length; i++) {
                var ih3s = h3s[i];
                var text_ih3s = $(ih3s).text().trim();
                if (text_ih3s.length > 0) {
                    var valSEOKeyword = txSEOKeyword.val().trim().toLowerCase().split(',')[0];
                    if (valSEOKeyword.length > 0) {
                        var text_ih3s_ = ConvertToUnSign(text_ih3s);
                        var numKeyH3 = allIndexOf(text_ih3s_, ConvertToUnSign(valSEOKeyword));
                        if (numKeyH3.length > 0) {
                            counKeyHeading += numKeyH3.length;
                        }
                    }
                }
            }
        } else {
            strMes += "» Trong Content không có chứa thẻ h3<br/>";
        }

        var imgTag = $strhtmldata.find("img");
        var str_img_mes = "";
        var i_img_mes = 0;
        var nImgOk = 0;
        var nImgContainer = 0;
        if (imgTag.length > 0) {
            for (var i = 0; i < imgTag.length; i++) {
                var iimgTag = imgTag[i];
                if ($(iimgTag).hasClass("imagecontainer")) {
                    nImgContainer += 1;
                } else {
                    if (iimgTag.hasAttribute("title") && iimgTag.hasAttribute("alt")) {
                        var attrtitle = $(iimgTag).attr('title');
                        var attralt = $(iimgTag).attr('alt');
                        var isKeyAttr = 0; var isKeyAttr2 = 0;
                        attrtitle = ConvertToUnSign(attrtitle);
                        attralt = ConvertToUnSign(attralt);
                        var titleP = allIndexOf(attrtitle, ConvertToUnSign(l_keyword[0].trim()));
                        var titleP2 = allIndexOf(attralt, ConvertToUnSign(l_keyword[0].trim()));
                        if (isKeyAttr == 0 && titleP.length > 0) {
                            isKeyAttr = 1;
                        }
                        if (isKeyAttr2 == 0 && titleP2.length > 0) {
                            isKeyAttr2 = 1;
                        }
                        if (isKeyAttr2 == 1 && isKeyAttr == 1) {
                            nImgOk += 1;
                        } else if (isKeyAttr2 == 0 && isKeyAttr == 1) {
                            i_img_mes += 1;
                            str_img_mes += "» " + i_img_mes + ". Ảnh có thẻ alt không chứa từ khóa: " + $(iimgTag).attr('src') + "<br/>";
                        } else if (isKeyAttr2 == 1 && isKeyAttr == 0) {
                            i_img_mes += 1;
                            str_img_mes += "» " + i_img_mes + ". Ảnh có thẻ title không chứa từ khóa: " + $(iimgTag).attr('src') + "<br/>";
                        } else {
                            i_img_mes += 1;
                            str_img_mes += "» " + i_img_mes + ". Ảnh có thẻ title và thẻ alt không chứa từ khóa: " + $(iimgTag).attr('src') + "<br/>";
                        }
                    } else {
                        i_img_mes += 1;
                        str_img_mes += "» " + i_img_mes + ". Ảnh chưa có thẻ alt hoặc title: " + $(iimgTag).attr('src') + "<br/>";
                    }
                }
            }
            var pImgOk = Math.floor(10 * ((nImgOk + nImgContainer) / imgTag.length));
            strMes += "» Có " + imgTag.length + " ảnh trong nội dung Content, " + nImgOk + " ảnh thỏa mãn, +" + (nImgOk + nImgContainer) + "<br/>" + str_img_mes;
            pSeo += pImgOk;
        } else {
            strMes += "» Không có ảnh trong bài viết, +10<br/>";
        }
    } else {
        strMes += "» Không tìm thấy Content hoặc không có dữ liệu, +0<br/>";
    } 

    var num_h2_h3 = counKeyHeading
    var num_h1 = parseInt($("#kf_ahead").attr("data-h1"));
    var num_head = num_h1 + num_h2_h3;
    if (num_head > 0) {
        $("#kf_ahead").removeClass("c_No_K");
        $("#kf_ahead").addClass("c_Yes_K");
        $("#kf_ahead").attr("data-h2-h3", num_h2_h3);
        $("#kf_ahead").text("Yes(" + num_head + ")");

    } else {
        $("#kf_ahead").removeClass("c_Yes_K");
        $("#kf_ahead").addClass("c_No_K");
        $("#kf_ahead").text("No");
    }
    if (NumExitKeyWordContent > 0) {
        $("#kf_content").text("Yes(" + NumExitKeyWordContent + ")");
        $("#kf_content").addClass("c_Yes_K");
        $("#kf_content").removeClass("c_No_K");
    } else {
        $("#kf_content").text("No");
        $("#kf_content").addClass("c_No_K");
        $("#kf_content").removeClass("c_Yes_K");
    }
}
function getPointSeoArticle() {
    pSeo = 0;
    strMes = "";
    if ($("#" + id_content + "_" + id_txSEOKeyword).val().trim().length > 0) {
        getPointSeoTitle();
        getPointSeoDesc();
        getPointContent();
    } else {
        strMes = "» Không có từ khóa";
    }
    $("div.divShowMesSeo").html(strMes);
    drawPointSeo(parseInt(pSeo));
    $("span#sppointSeo").html(pSeo + "/100");
    $("#" + id_content + "_" + id_hdfSEOPoint).val("" + pSeo);
    return false;
}

$("#" + id_content + "_" + id_txSEOTitle).on('blur, change', function () {
    $("#" + id_content + "_sp_title").html($(this).val());
    getNumKeyInArticleTitle();
});
$("#" + id_content + "_" + id_txSEODescription).on('blur, change', function () {
    getNumKeyInArticleLead();
});
$("#" + id_content + "_" + id_txtArticleLead).on('blur, change', getNumKeyInArticleLead);
$("#" + id_content + "_" + id_txSEOTitle).on('blur, change', function () {
    getPointSeoArticle();
});
$("#" + id_content + "_" + id_txSEODescription).on('blur, change', function () {
    getPointSeoArticle();
});
$("#" + id_content + "_" + id_txSEOKeyword).on('blur, change', function () {
    getPointSeoArticle();
    getNumKeyInArticleTitle();
    getNumKeyInArticleLead();
});
//FCKeditorAPI.GetInstance(id_content + "_" + id_txtArticleContent).Events.AttachEvent('OnBlur', aXY);
function FCKeditor_OnComplete(editorInstance) {
    editorInstance.Events.AttachEvent('OnBlur,OnChange', getPointSeoArticle);
}
function getNumKeyInArticleTitle() {
    var txtArticleTitles = $("#" + id_content + "_" + id_txtArticleTitle);
    var valArticleTitles = txtArticleTitles.val().trim();
    var txSEOKeyword = $("#" + id_content + "_" + id_txSEOKeyword);
    var numKeyATitle = [];
    if (valArticleTitles.length > 0 && txSEOKeyword.val().trim().length > 0) {
        var valSEOKeyword = txSEOKeyword.val().trim().toLowerCase().split(',')[0];
        if (valSEOKeyword.length > 0) {
            valArticleTitles = ConvertToUnSign(valArticleTitles);
            numKeyATitle = allIndexOf(valArticleTitles, ConvertToUnSign(valSEOKeyword));
        }
    }
    var num_h2_h3 = parseInt($("#kf_ahead").attr("data-h2-h3"));
    var num_h1 = numKeyATitle.length;
    var num_head = num_h1 + num_h2_h3;
    if (num_head > 0) {
        $("#kf_ahead").removeClass("c_No_K");
        $("#kf_ahead").addClass("c_Yes_K");
        $("#kf_ahead").attr("data-h1", num_h1);
        $("#kf_ahead").text("Yes(" + num_head + ")");

    } else {
        $("#kf_ahead").removeClass("c_Yes_K");
        $("#kf_ahead").addClass("c_No_K");
        $("#kf_ahead").text("No");
    }
}
function getNumKeyInArticleLead() {
    var txtArticleLead = $("#" + id_content + "_" + id_txSEODescription);
    var valArticleLead = txtArticleLead.val().trim();
    var txSEOKeyword = $("#" + id_content + "_" + id_txSEOKeyword);
    var numKeyALead = -1;
    if (valArticleLead.length > 0 && txSEOKeyword.val().trim().length > 0) {
        var valSEOKeyword = txSEOKeyword.val().trim().toLowerCase().split(',')[0];
        if (valSEOKeyword.length > 0) {
            var valArticleLead_ = ConvertToUnSign(valArticleLead);
            numKeyALead = allIndexOf(valArticleLead_, ConvertToUnSign(valSEOKeyword));
            if (numKeyALead >= 0) {
                var valKey = valArticleLead.substring(numKeyALead, eval(parseInt(numKeyALead) + valSEOKeyword.length));
                valArticleLead = replaceStringPosition(valArticleLead.toString(), numKeyALead, valSEOKeyword.length, "<font style='font-weight:bold'>" + valKey + "</font>");
            }
        }
    }
    $("#sp_summary").html("<p>" + valArticleLead + "<p/>");
}
var isData = 0;
function ActionCheckData() {
    getPointSeoArticle();
    if (strMes != "") {
        isData = 1;
    }
}
var myVarData = setInterval(function () { ActionCheckData() }, 1000);
function ActionCheckData() {
    getPointSeoArticle();
    getNumKeyInArticleTitle();
    getNumKeyInArticleLead();
    if (strMes != "") {
        clearInterval(myVarData);
    }
}
function allIndexOf(str, toSearch) {
    var indices = [];
    for (var pos = str.indexOf(toSearch) ; pos !== -1; pos = str.indexOf(toSearch, pos + 1)) {
        indices.push(pos);
    }
    return indices;
}
function ConvertToUnSign(obj) {
    var str = obj;

    str = str.toLowerCase();
    str = str.replace(/(^\s*)|(\s*$)/gi, "");//exclude  start and end white-space
    str = str.replace(/[ ]{2,}/gi, " ");//2 or more space to 1
    str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
    str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
    str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
    str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
    str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
    str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
    str = str.replace(/đ/g, "d");
    return str;
}
function countWords(s) {
    s = s.replace(/(^\s*)|(\s*$)/gi, "");//exclude  start and end white-space
    s = s.replace(/[ ]{2,}/gi, " ");//2 or more space to 1
    s = s.replace(/\n /, "<br/>"); // exclude newline with a start spacing
    return s.split(' ').length;
}
function replaceStringPosition(str, index, len, restr) {
    var str1 = str.substring(0, index);
    var str2 = str.substring(parseInt(index) + parseInt(len));
    return str1 + restr + str2;
}
function drawPointSeo(p) {
    var color = "white ";
    if (p < 50) {
        color = "red";
    } else if (p < 65) {
        color = "orange";
    } else if (p < 80) {
        color = "yellow";
    } else {
        color = "green";
    }
    $("#pProcessSeo").width(2.9 * p);
    $("#pProcessSeo").css("background", color);
}