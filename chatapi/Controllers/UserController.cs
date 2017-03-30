using chatapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace chatapi.Controllers
{
    public class UserController : ApiController
    {
        // GET: api/User
        public IEnumerable<MdTblUser> Get()
        {
            using (ChatContextDataContext db = new ChatContextDataContext())
            {
                var user = (from a in db.TblUser
                            select new MdTblUser
                            {
                               Id= a.Id,
                               Name= a.Name,
                               Password= a.Password,
                               Photo= a.Photo,
                               Status= a.Status
                            }).ToList();
                return user;
            }
        }

        // GET: api/User/5
        public MdTblUser Get(int id)
        {
            using (ChatContextDataContext db = new ChatContextDataContext())
            {
                var singleuser = (from a in db.TblUser
                                  where a.Id == id
                                  select new MdTblUser
                                  {
                                      Id = a.Id,
                                      Name = a.Name,
                                      Password = a.Password,
                                      Photo = a.Photo,
                                      Status = a.Status
                                  }).FirstOrDefault();
                return singleuser;
            }
        }

        // POST: api/User
        public void Post([FromBody]TblUser user)
        {
            using (ChatContextDataContext db = new ChatContextDataContext())
            {
                db.TblUser.InsertOnSubmit(user);
                db.SubmitChanges();   
            }
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]MdTblUser user)
        {
            using (ChatContextDataContext db = new ChatContextDataContext())
            {
                var edit = (from a in db.TblUser
                            where a.Id == id
                            select a).FirstOrDefault();
                edit.Name = user.Name;
                edit.Password = user.Password;
                edit.Photo = user.Photo;
                edit.Status = user.Status;
                db.SubmitChanges();
            }
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
            using (ChatContextDataContext db = new ChatContextDataContext())
            {
                var delete = (from a in db.TblUser
                              where a.Id == id select a).FirstOrDefault();
                db.TblUser.DeleteOnSubmit(delete);
                db.SubmitChanges();
            }
        }
    }
}
