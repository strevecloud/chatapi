using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace chatapi.Models
{
    public class OutputListChat
    {
        public int From_Id { get; set; }
        public int To_Id { get; set; }
        public string Chat { get; set; }
        public string LastMessage { get; set; }
        public string LastDate { get; set; }
        public string UserFrom { get; set; }
        public string UserTo { get; set; }
        public string Avatar { get; set; }
    }
}
public class ChatList
{

}