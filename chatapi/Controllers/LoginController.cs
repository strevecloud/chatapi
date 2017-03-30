using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JWT;
using chatapi.Models;

namespace chatapi.Controllers
{
    [AllowAnonymous]
    public class LoginController : ApiController
    {
        // GET: api/Login
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Login/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Login
        [AllowAnonymous]
        [Authorize]
        public object Post([FromBody]MdTblUser value)
        {
            using (ChatContextDataContext db = new ChatContextDataContext())
            {
                var existingUser = (from a in db.TblUser
                                    where a.Name == value.Name && a.Password == value.Password
                                    select new MdTblUser
                                    {
                                        Id=a.Id,
                                        Name=a.Name,
                                        Password=a.Password,
                                        Photo=a.Photo,
                                        Status=a.Status
                                    }).FirstOrDefault();
                object dbUser;
                var token = Token.CreateToken(existingUser, out dbUser);
                var response = Request.CreateResponse(new { dbUser, token });
                return response;
            }
        }

        // PUT: api/Login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Login/5
        public void Delete(int id)
        {
        }
    }
}
