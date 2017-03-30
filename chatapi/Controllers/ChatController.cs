using chatapi.Models;
using PusherServer;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace chatapi.Controllers
{
    [Authorize]
    public class ChatController : ApiController
    {
        // GET: api/Chat
        public IEnumerable<MdChat> Get()
        {
            var tkn = Token.GetToken(Request);
            //string nama = tkn.Name;
            int id = tkn.IdUser;
            //string token = tkn.Token;
            //string newtoken = string.Empty;
            //var expire = Token.RefreshToken();
            //if(expire<DateTime.Now)
            //{
            //   newtoken =  Token.refresh(Request,id);

            //}
            //var msg = Token.GetMessage(Request);
            
            using (ChatContextDataContext db = new ChatContextDataContext())
            {
                var chatlist = (from a in db.TblUser
                                join b in db.Conversation on a.Id equals b.From_Id
                                join c in db.ConversationReply on b.Con_Id equals c.Con_Id
                                where b.To_Id==id
                                select new MdChat
                                {
                                    From_Id = b.From_Id,
                                    Name = a.Name,
                                    To_Id = b.To_Id,
                                    Con_Id = b.Con_Id
                                }).Distinct().ToList();
                return chatlist;
            }
        }

        // GET: api/Chat/5
        public OutputChat Get(int id)
        {
            using (ChatContextDataContext db = new ChatContextDataContext())
            {
                var tkn = Token.GetToken(Request);
                //string nama = tkn.Name;
                int userid = tkn.IdUser;
                //int userid = 1;
                OutputChat output = new OutputChat();
                List<Message> pesan= new List<Message>();
                 pesan = (from a in db.TblUser
                                join b in db.Conversation on a.Id equals b.From_Id
                                join c in db.ConversationReply on b.Con_Id equals c.Con_Id
                                join d in db.TblUser on c.From_Id equals d.Id
                          where b.Con_Id == id
                                  select new Message
                                  {
                                     Name = d.Name,
                                     Text = c.Reply,
                                     Date = c.Timestamp,
                                     Avatar = d.Photo,
                                     Type = "received",
                                     To_Id=c.To_Id,
                                     From_Id=c.From_Id
                                  }).ToList();
                string username = (from a in db.TblUser
                                   where a.Id == userid select a.Name).FirstOrDefault();
                List<Message> DataPesan = new List<Message>();
                var dd = (from a in pesan select a).ToList();
                foreach(var dt in dd)
                {  
                    if (dt.From_Id == userid)
                    {
                        dt.Type = "sent";
                    }
                }
                output.Messages = dd;
                output.Name = pesan.FirstOrDefault().Name;
                output.DateTime = pesan.FirstOrDefault().Date;
                output.Avatar = pesan.FirstOrDefault().Avatar;
                output.To_Id = pesan.FirstOrDefault().To_Id;
                int toid = output.To_Id;
                string userto = (from a in db.TblUser
                                   where a.Id == toid
                                   select a.Name).FirstOrDefault();
                output.userid = userid;
                output.Con_Id = id;
                output.NameLogin = username;
                output.From_Id=pesan.FirstOrDefault().From_Id;
                output.To_Name = userto;
                return output;
            }
        }

        // POST: api/Chat
        public ConversationReply Post([FromBody]ConversationReply chat)
        {
            using (ChatContextDataContext db = new ChatContextDataContext())
            {
                var tkn = Token.GetToken(Request);
                int userid = tkn.IdUser;
                string sentfromname = (from a in db.TblUser
                               where a.Id == userid
                               select a.Name).FirstOrDefault();
                ConversationReply con = new ConversationReply();
                con.Reply = chat.Reply;
                con.From_Id = chat.From_Id;
                con.Timestamp = DateTime.Now.ToString();
                con.To_Id = chat.To_Id;
                con.Con_Id = chat.Con_Id;
                int conid = con.Con_Id;
                string message = con.Reply.ToString();
                int nama = con.From_Id;
                int to = con.To_Id = chat.To_Id;
                var option = new PusherOptions();
                option.Encrypted = true;
                var pusher = new Pusher("318710", "e566743c8d2d940b3849", "6a71cf9d67948c9fd93e", option);
                var result = pusher.Trigger("my-channel", "my-event", new { message = message, nama = nama, type = "received", sentby = userid,setbyname= sentfromname,to=to });
                return con;
            }
        }

        // PUT: api/Chat/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Chat/5
        public void Delete(int id)
        {
        }
    }
}
