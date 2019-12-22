using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Chat.Data
{
    public class Message
    {
        public Message(Conversation conversation, IdentityUser sender, string body)
        {
            Conversation = conversation;
            Sender = sender;
            Body = body;
        }
        public Message(ApplicationDbContext dbContext)
        {
            Conversation = dbContext.Conversations.ToList().Find(c => (c.User0Id == "Dummy0" && c.User1Id == "Dummy1") || (c.User0Id == "Dummy1" && c.User1Id == "Dummy0"));
            Sender = dbContext.Users.ToList().Find(u => u.Id == "Dummy0");
        }

        [Key]
        [Required]
        public Guid ID { get; private set; }
        public Conversation Conversation { get; set; }
        [Required]
        public Guid ConversationId { get; set; }
        public IdentityUser Sender { get; private set; }
        [Required]
        public string SenderId { get; set; }
        public string Body { get; set; }
    }
}
