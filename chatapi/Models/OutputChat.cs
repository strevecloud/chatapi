using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace chatapi.Models
{
    public class OutputChat
    {
        public string Name { get; set; }
        public string DateTime { get; set; }
        public string Avatar { get; set; }
        public int To_Id { get; set; }
        public int userid { get; set; }
        public int Con_Id { get; set; }
        public string NameLogin { get; set; }
        public int From_Id { get; set; }
        public List<Message> Messages { get; set; }
    }

    public class Message
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public string Date { get; set; }
        public string Avatar { get; set; }
        public string Type { get; set; }
        public int To_Id { get; set; }
        public int From_Id { get; set; }
    }
}