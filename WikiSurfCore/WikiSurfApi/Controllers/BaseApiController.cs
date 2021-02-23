using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Cors;
using Microsoft.Ajax.Utilities;
using WikiSurfApi.App_Start;
using WikiSurfCore;
using WikiSurfCore.Repositories;
using WikiSurfModel;

namespace WikiSurfApi.Controllers
{
    [EnableCors(origins:"*",headers:"*", methods:"*")]
    public class BaseApiController : ApiController
    {
        public IWikiSurfRepository _repository;
        public WikiSession _session = null;
        public WikiSession Session
        {
            get
            {
                return GetSession();
            }
            set
            {
                if (_session != value)
                {
                    _session = GetSession();
                }
            }
        } 
        public BaseApiController(WikiSurfRepository repo)
        {
            _repository = repo;

        }

        public WikiSession GetSession()
        {
            if (Request != null)
            {
                if (_session == null && Request.Headers.Contains(SessionAuthenticateMiddleware.SessionHeader))
                {
                    var list =
                        Request.Headers.FirstOrDefault(x => x.Key == SessionAuthenticateMiddleware.SessionHeader).Value;
                    var sessionId = Guid.Parse(list.First());
                    if (_repository != null)
                    {
                        _session = _repository.GetExistingSession(sessionId);
                        Session = _session;
                    }
                }
            }

            return _session;
        }
        public override async Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
        {
            if (controllerContext.Request.Headers != null)
            {
                var r = controllerContext.Request.Headers.FirstOrDefault(x =>
                    x.Key == SessionAuthenticateMiddleware.SessionHeader);
                //if (SessionAuthenticateMiddleware.GetFirstHeaderValueOrDefault(request: controllerContext.Request, headerKey: SessionAuthenticateMiddleware.SessionHeader, defaultValue: request=>Guid.Empty().ToString()))
                //{
                //    var headersValues = controllerContext.Request.Headers.GetValues(SessionAuthenticateMiddleware.SessionHeader);
                //    var id = headersValues.FirstOrDefault();
                //    if (id != null)
                //    {
                //        var sessionId = Guid.Parse(id);
                //        SessionId = _repository.GetExistingSession(sessionId);
                //    }
                //}
            }
            return await base.ExecuteAsync(controllerContext, cancellationToken);
        }
    }
}
