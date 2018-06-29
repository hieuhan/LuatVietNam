var arrBackLinkUrls = new Array("http://xoso.com.vn/xo-so-mien-bac/xsmb-p1.html", "http://xosotailoc.com", "http://ketquaday.vn", "http://bongda24h.vn", "http://xosotailoc.com/", "http://chungcungoaigiaodoan.vn", "http://lichngaytot.com/xem-ngay-tot-xau.html", "http://lichngaytot.com/cung-hoang-dao.html", "http://xoso.tructiep.vn", "http://xoso.com.vn/xo-so-mien-nam/xsmn-p1.html", "http://xoso.com.vn/cau-mien-bac/cau-bach-thu.html", "http://nhacchohot.vn", "http://lichngaytot.com/van-trinh-nam.html", "http://nhacchoviet.vn", "http://xoso.com.vn/xo-so-tu-chon-mega-645.html", "http://lichngaytot.com/xong-dat.html", "http://seothetop.com/kien-thuc-seo/quy-trinh-seo-website-2017-len-top-google-nhanh-va-ben-vung-t3-277-157107.html", "http://seothetop.com/kien-thuc-seo/huong-dan-lam-seo-web-trong-9-ngay-voi-full-tai-lieu-pdf-t3-277-157116.html", "http://xoso.com.vn/xo-so-mien-bac/xsmb-p1.html", "http://lichngaytot.com/tu-vi-12-cung-hoang-dao.html", "http://lichngaytot.com/xem-ngay-tot-xau.html", "https://nhacpro.vn", "https://lichngaytot.com/tu-vi/tu-vi-2018-304-189308.html", "http://luatvietnam.vn/", "https://newhousing.com.vn/du-an-chung-cu-vinhomes-smart-city-nguyen-trai/", "http://luatvietnam.vn/doanh-nghiep/luat-68-2014-qh13-quoc-hoi-91359-d1.html", "http://luatvietnam.vn/dat-dai/luat-45-2013-qh13-quoc-hoi-83386-d1.html", "http://luatvietnam.vn/hinh-su/luat-100-2015-qh13-quoc-hoi-101324-d1.html");
var arrBackLinkNames = new Array("XSMB", "Xổ số tài lộc", "Kết quả xổ số", "Bong Da", "Xo so tai loc", "Chung cu Ngoai giao doan", "Xem Ngay Tot Xau", "12 Cung Hoang Dao", "Xo so truc tiep", "XSMN", "Cau MB", "Nhac Cho", "Xem Tu Vi 2018", "nhacchoviet.vn", "xo so mega", "Xông Đất 2018", "Quy Trình SEO", "Hướng dẫn làm SEO web", "SXMB", "Tử Vi Hàng Ngày", "Âm Lịch Hôm nay", "MV nhac hot", "Tử vi 2018", "Văn Bản Pháp Luật", "Vinhomes smart City", "Luật Doanh nghiệp", "Luật Đất đai", "Luật Hình sự 2015");
var arrBackLinkDescs = new Array("XSMB Xo so mien bac", "Xo so tai loc", "Kết quả xổ số, ket qua day, xo", "Bong da 24h", "Kết quả xổ số, Xo so tai loc, Ket qua xo so truc tiep XS TT, Xo so tai loc, kết quả xổ số, Thống kê, dự đoán, soi cầu", "Chung cu Ngoai giao doan", "Xem ngay tot xau", "Tu vi 12 cung hoang dao", "Tuong thuat KQXS truc tiep", "KQ Xo so mien Nam", "Soi cau mien bac", "Nhac cho cho de yeu", "Xem tu vi 2018 cho 12 con giap", "nhac cho viet", "xo so mega", "Tuoi Xong Dat 2017", "Quy trinh SEO", "Hướng dẫn làm SEO web full tai lieu PDF", "SXMB Hom nay", "Tu Vi Hang Ngay 12 Cung Hoang Dao", "Âm Lịch Hôm nay", "MV nhac hot", "Tử vi 2018", "Văn Bản Pháp Luật", "Vinhomes smart City", "Luật Doanh nghiệp", "Luật Đất đai", "Luật Hình sự 2015");
var arrWebsiteId = new Array("#15#", "#8#", "#12#", "#18#", "#5#6#7#8#12#", "#3#4#12#", "#14#", "#1#14#18#19#", "#13#", "#16#", "#3#4#6#10#12#", "#1#3#4#", "#1#14#", "#12#", "#13#", "#3#13#14#", "#1#2#13#", "#1#17#19#", "#15#", "#1#13#14#16#19#", "#2#14#18#", "#16#", "#1#", "#19#", "#17#", "#16#19#", "#16#19#", "#19#");
function getBackLinkByWebsiteId(webId)
{
    var result = '';
    var strWebId = '#' + webId + '#';
    //var strTestLength = '';
    for (var i = 0 ; i < arrWebsiteId.length; i++)
    {
        if (arrWebsiteId[i].indexOf(strWebId) != -1)
        {
            if (result == '')
            {
                result += "<a rel='nofollow' target='_blank' href='" + arrBackLinkUrls[i] + "' title='" + arrBackLinkDescs[i] + "'>" + arrBackLinkNames[i] + "</a>";
            }
            else
            {
                result += " | <a rel='nofollow' target='_blank' href='" + arrBackLinkUrls[i] + "' title='" + arrBackLinkDescs[i] + "'>" + arrBackLinkNames[i] + "</a>";
            }
//            if (webId == '1')
//            {
//                strTestLength += arrBackLinkNames[i] + " | ";
//                if (strTestLength.length >= 200)
//                {
//                    result += '<br />';
//                    strTestLength = '';
//                }
//                else
//                {
//                    if (result != '')
//                    {
//                        result += ' | ';
//                    }
//                }
//                result += "<a target='_blank' href='" + arrBackLinkUrls[i] + "' title='" + arrBackLinkDescs[i] + "'>" + arrBackLinkNames[i] + "</a>";
//            }
//            else
//            {
//                strTestLength += arrBackLinkNames[i] + " | ";
//                if (strTestLength.length >= 155)
//                {
//                    result += '<br />';
//                    strTestLength = '';
//                }
//                else
//                {
//                    if (result != '')
//                    {
//                        result += ' | ';
//                    }
//                }
//                result += "<a target='_blank' href='" + arrBackLinkUrls[i] + "' title='" + arrBackLinkDescs[i] + "'>" + arrBackLinkNames[i] + "</a>";
//            }
        }
    }
    document.write(result);
}

