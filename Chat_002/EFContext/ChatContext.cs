using Chat_002.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Chat_002.EFContext
{
    public class ChatContext:DbContext
    {
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }



        public ChatContext() 
            : base("ChatConection") 
        { }
        //static ChatContext()
        //{
        //    Database.SetInitializer<ChatContext>(new ChatContextInitializer());
        //}

    }
}