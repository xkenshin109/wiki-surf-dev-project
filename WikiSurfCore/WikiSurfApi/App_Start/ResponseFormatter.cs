using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Ninject.Activation;
using HttpContext = System.Web.HttpContext;

namespace WikiSurfApi.App_Start
{
    public class ResponseFormatter : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            
            var response = await base.SendAsync(request, cancellationToken);
            
            var content = await response.Content.ReadAsStringAsync();
            response = response.RequestMessage.CreateResponse(response.StatusCode, new
            {
                success = response.StatusCode == HttpStatusCode.OK? true : false,
                data = JsonConvert.DeserializeObject<object>(content),
                message="lets do it!"

            });

            response.Headers.Add("Access-Control-Allow-Origin", "*");
            return response;
        }
    }
}