using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace chatapi.Models
{
    public class MdConversationReply
    {
        public int Id { get; set; }
        public string Reply { get; set; }
        public int From_Id { get; set; }
        public int To_Id { get; set; }
        public string Timestamp { get; set; }
        public int Con_Id { get; set; }
    }
}