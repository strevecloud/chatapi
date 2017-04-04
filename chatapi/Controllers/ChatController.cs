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
        public OutputListChat Get()
        {
            var tkn = Token.GetToken(Request);
            int id = tkn.IdUser;
            using (ChatContextDataContext db = new ChatContextDataContext())
            {
                //OutputListChat output = new OutputListChat();
                //List<OutputListChat> mdchatval = new List<OutputListChat>();
                //var chatlist = db.GetlastChat(id).ToList();
                //foreach(var item in chatlist)
                //{
                //    OutputListChat data = new OutputListChat();
                //    data.Name = item.Name;
                //    data.Reply = item.Reply;
                //    data.Photo = item.Photo;
                //    data.NameLogin = item.NameLogin;
                //    data.Timestamp = item.Timestamp;
                //    data.Con_Id = item.Con_Id;
                //    data.From_Id = item.From_Id;
                //    data.To_Id = item.To_Id;
                //    mdchatval.Add(data);
                //}
                string name = (from a in db.TblUser
                              where a.Id == id
                              select a.Name).FirstOrDefault();
                List<ChatOutput> chat = new List<ChatOutput>();
                OutputListChat output = new OutputListChat();
                var chatlist = db.GetlastChat(id).ToList();
                foreach (var item in chatlist)
                {
                    ChatOutput data = new ChatOutput();
                    data.Name = item.Name;
                    data.Reply = item.Reply;
                    data.Photo = item.Photo;
                    data.Timestamp = item.Timestamp;
                    data.Con_Id = item.Con_Id;
                    data.From_Id = item.From_Id;
                    data.To_Id = item.To_Id;
                    data.Name2 = item.Name2;
                    chat.Add(data);
                }
                output.ListChat = chat;
                output.NameLogin = name;
                var replace = (from a in chat select a).ToList();
                foreach (var isi in replace)
                {
                    if(isi.To_Id==id)
                    {
                        isi.Name = isi.Name;
                    }else
                    {
                        isi.Name = isi.Name2;
                    }
                }

                //var chatlist = (from a in db.TblUser
                //                join b in db.Conversation on a.Id equals b.From_Id
                //                join c in db.ConversationReply on b.Con_Id equals c.Con_Id
                //                where b.To_Id == id
                //                select new MdChat
                //                {
                //                    From_Id = b.From_Id,
                //                    Name = a.Name,
                //                    To_Id = b.To_Id,
                //                    Con_Id = b.Con_Id
                //                }).Distinct().ToList();
                //return chatlist;



                //var chatlist = from ta in from a in db.TblUser
                //                           join b in db.Conversation on a.Id equals b.From_Id
                //                           join c in db.ConversationReply on b.Con_Id equals c.Con_Id
                //                           select new { a.Id, b.To_Id, b.From_Id, c.Con_Id 
                //                join u in (from u in db.ConversationReply
                //                          group u by u.Con_Id into g
                //                          select new
                //                          {
                //                              conid = g.Key,
                //                              Maxstatus = g.Max(x => x.Id)
                //                          }) on new { ta.Con_Id } equals new { u.conid }
                //               select ta;
                //               //{
                //               //   Name=  a.Name,
                //               //   Con_Id = b.Con_Id,
                //               //   To_Id = b.To_Id,
                //               //   From_Id = b.From_Id
                //               //});


                return output;
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
                        dt.From_Id = dt.To_Id;
                    }
                }
                output.Messages = dd;
                
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
                output.From_Id=dd.FirstOrDefault().From_Id;
                string to = (from a in db.TblUser
                             where a.Id == pesan.FirstOrDefault().From_Id
                             select a.Name).FirstOrDefault();
                output.Name = to;
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
                if (chat.From_Id == userid)
                {
                    con.From_Id = chat.To_Id;
                }
                else if (chat.To_Id==userid)
                {
                    con.From_Id = chat.From_Id;
                }
                else
                {
                    con.From_Id = chat.From_Id;
                }
                //con.From_Id = chat.From_Id;
                con.Timestamp = DateTime.Now.ToString();
                con.To_Id = chat.To_Id;
                con.Con_Id = chat.Con_Id;
                int conid = con.Con_Id;
                string message = con.Reply.ToString();
                int nama = con.From_Id;
                //int to = con.To_Id;
                string sentTo = (from a in db.TblUser
                                       where a.Id == con.From_Id
                                       select a.Name).FirstOrDefault();
                //db.ConversationReply.InsertOnSubmit(con);
                //db.SubmitChanges();
                var option = new PusherOptions();
                option.Encrypted = true;
                var pusher = new Pusher("318710", "e566743c8d2d940b3849", "6a71cf9d67948c9fd93e", option);
                var result = pusher.Trigger("my-channel_"+conid, "my-event_"+conid, new { message = message, sentTo = sentTo, type = "received", sentby = userid,setbyname= sentfromname });
                var notif = pusher.Trigger("my-channel"+sentTo, "newmessage", new { message = "new message from "+sentfromname });
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
