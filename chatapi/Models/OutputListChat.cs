using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace chatapi.Models
{
    public class OutputListChat
    {
        public string NameLogin { get; set; }
        public List<ChatOutput> ListChat { get; set; }
    }
    public class ChatOutput
    {
        public string Name { get; set; }
        public int From_Id { get; set; }
        public int To_Id { get; set; }
        public int Con_Id { get; set; }
        public string Reply { get; set; }
        public string Timestamp { get; set; }
        public string Photo { get; set; }
        public string Name2 { get; set; }
    }
}