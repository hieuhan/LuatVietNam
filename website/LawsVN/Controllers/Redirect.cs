using System.Web.Mvc;
using ICSoft.CMSLib;
using System.Collections.Generic;
using ICSoft.LawDocsLib;

namespace LawsVN.Controllers
{
    [AllowAnonymous]
    public class RedirectController : Controller
    {
        public ActionResult OldUrl(string OldUrl = "")
        {
            object newUrl = "/";
            Redirects m_Redirects = new Redirects();
            if (OldUrl.Contains("/"))
                OldUrl = "/VL/" + OldUrl;
            else
                OldUrl = "/" + OldUrl + ".aspx";
            m_Redirects = m_Redirects.GetBySourceUrl(OldUrl);
            if (m_Redirects.RedirectId > 0)
                newUrl = m_Redirects.DestUrl;
            else
            {
                // cac trang can thiet
                if (OldUrl.Contains("VB_Download"))//tai van ban
                {
                    Docs m_Docs = new Docs();
                    List<Docs> l_Docs = m_Docs.GetListByDocGUId(Request["ID"], 1);
                    if (l_Docs.Count > 0)
                    {
                        m_Docs = l_Docs[0];
                    }
                    if (m_Docs.DocId > 0)
                    {
                        newUrl = ICSoft.CMSViewLib.DocsView.Static_GetDocUrl(m_Docs.DocUrl,"taive");

                    }
                    else
                    {
                        newUrl = "/Error/Error404";
                    }
                }
            }
            if (!newUrl.ToString().StartsWith("/"))
            {
                newUrl = "/" + newUrl;
            }
            if (Library.Constants.IsRedirectOldLink)
            {
                Response.RedirectPermanent(newUrl.ToString(), true);
            }
            return View(newUrl);
        }
        public ActionResult OldUrlDinamic(string OldParam = "")
        {
            object newUrl = "/";
            if (OldParam == "docsproperties")// thuoc tinh van ban
            {
                Docs m_Docs = new Docs();
                List<Docs> l_Docs = m_Docs.GetListByDocGUId(Request["ID"], 1);
                if (l_Docs.Count > 0)
                {
                    m_Docs = l_Docs[0];
                }
                if (m_Docs.DocId > 0)
                {
                    newUrl =  m_Docs.DocUrl;

                }
                else
                {
                    newUrl="/Error/Error404";
                }
            }
            else
            {
                switch (Request["tabid"])//tabid
                {
                    case "704"://tim kiem
                        short FieldId = 0;
                        if(Request["q"] != null)
                        {
                            newUrl = "/tim-van-ban.html?Keywords=&SearchOptions=1&SearchExact=1&DateFrom=&DateTo=&DocTypeId=0&OrganId=0&EffectStatusId=0&FieldId=" + Request["lv"] + "&LanguageId=0&SignerName=&SignerId=0";
                        }
                        else if (!string.IsNullOrEmpty(Request["lv"]) && short.TryParse(Request["lv"], out FieldId))//linh vuc
                        {
                            Fields m_Fields = new Fields();
                            m_Fields = m_Fields.Get(FieldId, 1);
                            newUrl = m_Fields.GetUrl(1);
                        }
                        else
                        {
                            Redirects m_Redirects = new Redirects();
                            string OldUrl = Request.RawUrl;
                            m_Redirects = m_Redirects.GetBySourceUrl(OldUrl);
                            if (m_Redirects.RedirectId > 0)
                                newUrl = m_Redirects.DestUrl;
                        }
                        break;
                    case "441":// trang noi dung tin don le
                    case "428":
                        int ArticleId = 0;
                        if (int.TryParse(Request["ID"], out ArticleId))
                        {
                            Articles m_Articles = new Articles();
                            m_Articles = m_Articles.Get(ArticleId, 1, 1);
                            newUrl = m_Articles.ArticleUrl;
                        }                        
                        break;
                    case "684"://tim kiem                        
                        if (Request["q"] != null)
                        {
                            if (Request["i"] != "all")
                                newUrl = "/tim-van-ban.html?Keywords=" + Request["q"].Trim() + "&SearchExact=1&SearchOptions=3";
                            else
                                newUrl = "/tim-van-ban.html?Keywords=" + Request["q"].Trim() + "&SearchExact=1&SearchOptions=1";
                        }
                        
                        break;
                    case "651"://dang nhap va chuyen sang trang chi tiet van ban
                        Docs m_Docs = new Docs();
                        List<Docs> l_Docs = m_Docs.GetListByDocGUId(Request["id"], 1);
                        if (l_Docs.Count > 0)
                        {
                            m_Docs = l_Docs[0];
                        }
                        if (m_Docs.DocId > 0)
                        {
                            if (m_Docs.DocUrl.StartsWith("/"))
                                newUrl = m_Docs.DocUrl;
                            else
                                newUrl = "/" + m_Docs.DocUrl;

                        }
                        else
                        {
                            newUrl = "/Error/Error404";
                        }
                        break;
                }
            }
            if (!newUrl.ToString().StartsWith("/"))
            {
                newUrl = "/" + newUrl;
            }
            if (Library.Constants.IsRedirectOldLink)
            {
                Response.RedirectPermanent(newUrl.ToString(), true);
            }
            return View(newUrl);
        }
    }
}
