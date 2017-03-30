using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace chatapi.Models
{
    public class MdTblUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Photo { get; set; }
        public string Status { get; set; }
    }
}