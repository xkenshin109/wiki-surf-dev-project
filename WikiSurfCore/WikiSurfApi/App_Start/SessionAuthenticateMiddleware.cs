using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Cors;
using Newtonsoft.Json;

namespace WikiSurfApi.App_Start
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SessionAuthenticateMiddleware : DelegatingHandler
    {
        public static readonly string SessionHeader = "sessionid";
        public static readonly string AccessControlHeader = "Access-Control-Request-Headers";

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (!request.Headers.Contains(SessionHeader) && !request.RequestUri.AbsoluteUri.Contains("NewSession"))
            {
                //Authenticate
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            
            return await base.SendAsync(request, cancellationToken);

        }

        private static T GetFirstHeaderValueOrDefault<T>(HttpRequestMessage request,string headerKey,
            Func<HttpRequestMessage, string> defaultValue,
            Func<string, T> valueTransform )
        {
            IEnumerable<string> headerValues;
            HttpRequestMessage message = request ?? new HttpRequestMessage();
            if (!message.Headers.TryGetValues(headerKey, out headerValues))
                return valueTransform(defaultValue(message));
            string firstHeaderValue = headerValues.FirstOrDefault() ?? defaultValue(message);
            return valueTransform(firstHeaderValue);
        }
    }
}