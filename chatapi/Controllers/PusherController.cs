using chatapi.Models;
using PusherServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace chatapi.Controllers
{
    public class PusherController : ApiController
    {
        // GET: api/Pusher
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Pusher/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Pusher
        [AllowAnonymous]
        public object Post([FromBody] AuthPusher authpusher)
        {
            var pusher = new Pusher("318710", "e566743c8d2d940b3849", "6a71cf9d67948c9fd93e");
            var auth = pusher.Authenticate(authpusher.channel_name, authpusher.socket_id);
            var json = auth.ToJson();
            return auth;
        }

        // PUT: api/Pusher/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Pusher/5
        public void Delete(int id)
        {
        }
    }
}
