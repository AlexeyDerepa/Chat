using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chat_002.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public string MessageContext { get; set; }

        public DateTime Time { get; set; }

        public int? TopicId { get; set; }
        public virtual Topic Topic { get; set; }

        public int? UserId { get; set; }
        public virtual User User { get; set; }
    }
}