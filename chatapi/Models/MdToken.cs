using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace chatapi.Models
{
    public class MdToken
    {
        public string Token { get; set; }
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string Expire { get; set; }
    }
}