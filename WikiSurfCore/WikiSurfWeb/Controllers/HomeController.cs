using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WikiSurfCore.Repositories;
using WikiSurfModel;

namespace WikiSurfWeb.Controllers
{
    public class HomeController : Controller
    {
        private IWikiSurfRepository _wikiSurfRepository;

        public HomeController(WikiSurfRepository wikiSurfRepository)
        {
            _wikiSurfRepository = wikiSurfRepository;
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetStartingWords()
        {
            //var words = _wikiSurfRepository.GetRandomWords();
            //var wikiPages = _wikiSurfRepository.GetWikiPage(words.First());
            //var content = wikiPages.HtlmFormatted;
            return Json(
                new
                {
                    //currentPage = content,
                    //startingWord = words[0],
                    //endingWord = words[1]
                }, 
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult UserClicked(string word, Guid wikiLinkId, WikiSession aSessionId)
        {
            var res = _wikiSurfRepository.GetWikiPage(wikiLinkId, word, aSessionId);
            return Json(new
            {
                currentPage = res.HtlmFormatted,
                title = res.Title
            }, JsonRequestBehavior.AllowGet);
        }
    }
}