using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WikiSurfCore.Repositories;
using WikiSurfModel;

namespace WikiSurfApi.Controllers
{
    public class GameEngineController : BaseApiController
    {
        public GameEngineController(WikiSurfRepository repo)
        :base(repo)
        {
        }
        [HttpPost()]
        public HttpResponseMessage StartGame()
        {
            var page = _repository.GetRandomWords(Session);
            var startingWord = page.First();
            var endingWord = page.Last();
            var startingPage = _repository.GetWikiPage(startingWord);
            
            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                startingWord = new
                {
                    startingWord.Word,
                    startingWord.WordBankId
                },
                endingWord = endingWord.Word,
                startingPage = startingPage?.PlainTextContent,
                pageLinks = startingPage?.Links.Select(lk=>new
                {
                    Word = lk.WordHtmlEncoded,
                    Id = lk.WikiLinksId,
                    lk.Additional
                }),
                Session.TotalClicks
            }, Configuration);
        }

        [HttpPost()]
        [AllowAnonymous]
        public HttpResponseMessage NewSession()
        {
            var session = _repository.GetNewSession();
            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                session.WikiSessionId,
                session.TotalClicks,
                session.PlayerName
            });
        }
    }
}
