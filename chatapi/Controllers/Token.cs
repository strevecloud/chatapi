using chatapi.Models;
using JWT;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace chatapi.Controllers
{
    public class Token
    {
        public static string expire;
        public static DateTime expiredtoken;
        public static HttpResponseMessage msg;
        public static MdToken GetToken(HttpRequestMessage Request)
        {
            MdToken tk = new MdToken();
            IEnumerable<string> headerValues;

            if (Request.Headers.TryGetValues("authorization", out headerValues))
            {
                tk.Token = headerValues.First();
            }
            var jwtToken = new JwtSecurityToken(tk.Token);
            var payload = jwtToken.Payload;
            tk.IdUser = Convert.ToInt32(payload["userId"]);
            tk.Name = payload["name"].ToString();
            tk.Expire = payload["expire"].ToString();
            expire = tk.Expire;
            return tk;
        }
        public static DateTime RefreshToken()
        {
            //GetToken();
            long expired = Convert.ToInt64(expire);
            long baseTicks = 621355968000000000;
            long tickResolution = 10000000;
            long epoch = expired;
            long epochTicks = (epoch * tickResolution) + baseTicks;
            var date = new DateTime(epochTicks, DateTimeKind.Utc);
            expiredtoken = date; 
            return date;
        }
        public static string tkn;
        public static string refresh(HttpRequestMessage Request,int id)
        {
            
            if(expiredtoken < DateTime.Now )
            {
                using (ChatContextDataContext db = new ChatContextDataContext())
                {
                    var user = (from a in db.TblUser
                                where a.Id == id
                                select new MdTblUser
                                {
                                    Id = a.Id,
                                    Name = a.Name,
                                    Photo = a.Photo,
                                    Status=a.Status}).FirstOrDefault();
                    object dbUser;
                    var token = Token.CreateToken(user, out dbUser);
                    var response = Request.CreateResponse(new { dbUser, token });
                    tkn = token.ToString();
                }
            }
            return tkn.ToString();
        }
        public static string CreateToken(MdTblUser user, out object dbUser)
        {
            var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local);
            var expiry = Math.Round((DateTime.Now.AddMinutes(30) - unixEpoch).TotalSeconds);
            var issuedAt = Math.Round((DateTime.UtcNow - unixEpoch).TotalSeconds);
            var notBefore = Math.Round((DateTime.UtcNow.AddMonths(6) - unixEpoch).TotalSeconds);


            var payload = new Dictionary<string, object>
            {
                {"userId", user.Id},
                {"name", user.Name  },
                {"expire",expiry }
            };

            //var secret = ConfigurationManager.AppSettings.Get("jwtKey");
            const string apikey = "tGv78M2ywDP2LJ3n3bWpqeG-N1VciK8YLpynB535QqOibS_bvZOVa_zVtpbrRjvb";

            var token = JsonWebToken.Encode(payload, apikey, JwtHashAlgorithm.HS256);

            dbUser = new { user.Name, user.Id };
            return token;
        }

        public static ResponseToken AutorizeToken(HttpRequestMessage response)
        {
            ResponseToken rsp = new ResponseToken();
            var message = string.Empty;
            var expire = RefreshToken();
            if (expire < DateTime.Now)
            {
                //newtoken = Token.refresh(request, id);
                message = "Token Expired";
                rsp.MessageResponse = message;
                //msg = rsp.MessageResponse;
            }
            return rsp;
        }

        public static HttpResponseMessage GetMessage(HttpRequestMessage response)
        {
            //AutorizeToken(response);
            return msg;
        }
    }
}