using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WikiSurfCore.Migrations;

namespace WikiSurfCore.Exceptions
{
    [Serializable]
    public class WikiException : Exception
    {
        private HttpStatusCode _statusCode;
        private HttpRequestMessage _request;
        private string _msg;
        public WikiException(HttpRequestMessage request, HttpStatusCode statusCode, string msg)
        :base(msg)
        {
            _statusCode = statusCode;
            _request = request;
            _msg = msg;
        }

        public WikiException(HttpStatusCode statusCode, string msg)
        :base(msg)
        {
            _statusCode = statusCode;
            _msg = msg;
        }
    }
}
