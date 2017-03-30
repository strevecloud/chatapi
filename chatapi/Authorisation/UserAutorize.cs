using chatapi.Controllers;
using chatapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace chatapi.Authorisation
{
    public class UserAutorize : DelegatingHandler
    {
        //set a default API key 
        private const string yourApiKey = "X-some-key";

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
                MdToken tk = new MdToken();
                IEnumerable<string> headerValues;

                if (request.Headers.TryGetValues("authorization", out headerValues))
                {
                    tk.Token = headerValues.First();
                }
                if (tk.Token != null)
                {
                    Token.GetToken(request);
                    var auth = Token.AutorizeToken(request);
                    if (auth.MessageResponse != null)
                   return request.CreateErrorResponse(HttpStatusCode.Unauthorized, auth.MessageResponse);
            }

                //Allow the request to process further down the pipeline
                var response = await base.SendAsync(request, cancellationToken);

                //Return the response back up the chain
                return response;
        }
    }
}