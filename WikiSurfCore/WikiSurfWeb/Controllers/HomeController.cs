using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WikiSurfCore.Repositories;
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
            var words = _wikiSurfRepository.GetRandomWords();
            var wikiPages = _wikiSurfRepository.GetWikiPage(words.First());
            var content = wikiPages.HtlmFormatted;
            return Json(
                new
                {
                    currentPage = content,
                    startingWords = words
                }
                , 
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult UserClicked(string word, Guid wikiLinkId)
        {
            var res = _wikiSurfRepository.GetWikiPage(wikiLinkId, word);
            return Json(new
            {
                currentPage = res.HtlmFormatted
            }, JsonRequestBehavior.AllowGet);
        }
    }
}