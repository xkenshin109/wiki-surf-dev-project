using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WikiSurfCore.Repositories;

namespace WikiSurfApi.Controllers
{
    public class SessionController : BaseApiController
    {
        public SessionController(WikiSurfRepository repo)
        :base(repo)
        {
            
        }

        [HttpPost()]
        //[Route("api/GameEngine/LinkClicked")]
        public HttpResponseMessage LinkClicked(SessionRequest request)
        {
            var session = _repository.IncreaseClick(Session);
            var nextPage = _repository.GetWikiPage(request.WikiLinkId, request.Word, session);
            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                session = new
                {
                    session.WikiSessionId,
                    session.TotalClicks,
                    session.PlayerName
                },
                content = new
                {
                    pageContent = nextPage?.PlainTextContent,
                    pageLinks = nextPage?.Links.Select(lk => new
                    {
                        Word = lk.WordHtmlEncoded,
                        Id = lk.WikiLinksId,
                        lk.Additional
                    })
                }
            });
        }

        public class SessionRequest
        {
            public Guid WikiLinkId { get; set; }
            public string Word { get; set; }
        }
    }
}
