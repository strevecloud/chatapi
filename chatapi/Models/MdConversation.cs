using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace chatapi.Models
{
    public class MdConversation
    {
        public int Id { get; set; }
        public int Form_Id { get; set; }
        public int To_Id { get; set; }
        public DateTime Timestamp { get; set; }
        public int Con_Id { get; set; }
    }
}