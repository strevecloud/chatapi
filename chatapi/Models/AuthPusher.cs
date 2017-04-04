using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace chatapi.Models
{
    public class AuthPusher
    {
        public string socket_id { get; set; }
        public string channel_name { get; set; }
    }
}