using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chat_002.Models
{
    public class Topic
    {
        public int TopicId { get; set; }
        public string TopicName { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
        public Topic()
        {
            Messages = new List<Message>();
        }

        public int? UserId { get; set; }
        public virtual User User { get; set; }

    }
}