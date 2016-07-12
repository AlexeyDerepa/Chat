using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chat_002.Models
{
    public class MessageListViewModel
    {
        public IEnumerable<Message> Messages { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentTopic { get; set; }
        public Message Message { get; set; }
        public Topic Topic { get; set; }
    }
}