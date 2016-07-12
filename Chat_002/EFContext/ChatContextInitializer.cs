using Chat_002.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Chat_002.EFContext
{
    class ChatContextInitializer : DropCreateDatabaseAlways<ChatContext>
    {
        protected override void Seed(ChatContext db)
        {
            Role admin = new Role { Name = "Admin" };
            Role moderator = new Role{Name = "Moderator"};
            Role user = new Role { Name = "User" };
            db.Roles.AddRange(new List<Role>() {admin,  moderator, user });
            db.SaveChanges();
            User Alexey = new User() { Login = "graf", UserName = "Alexey", Password = "graf", Role = admin };
            User Alla = new User() { Login = "grafinay", UserName = "Alla", Password = "grafinay", Role = moderator };
            User Victor = new User() { Login = "dva", UserName = "Victor", Password = "dva", Role = user };
            db.Users.AddRange(new List<User>() { Alexey , Alla, Victor});
            db.SaveChanges();

            Topic topic = new Topic { TopicName = "the best topic", User = Victor };
            db.Topics.Add(topic);
            Topic topic2 = new Topic { TopicName = "topic number 2", User = Alla };
            db.Topics.Add(topic2);
            db.SaveChanges();

            Message message = new Message { Topic = topic, MessageContext = "some text for the best topic", Time = DateTime.Now, User = Victor };
            Message message2 = new Message { Topic = topic2, MessageContext = "some text for the topic number 2", Time = DateTime.Now, User = Alla };
            db.Messages.Add(message);
            db.Messages.Add(message2);
            db.SaveChanges();
        }
    }
}
