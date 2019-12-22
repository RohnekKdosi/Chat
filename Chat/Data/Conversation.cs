using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Chat.Data;

namespace Chat.Data
{
    public class Conversation
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        public Conversation(IdentityUser user0, IdentityUser user1)
        {
            User0 = user0;
            User1 = user1;
            Messages = new List<Message>();
        }
        public Conversation(ApplicationDbContext dbContext)
        {
            User0 = dbContext.Users.Find("Dummy0");
            User1 = dbContext.Users.Find("Dummy1");
        }
        [Required]
        public string User0Id { get; private set; }
        
        public IdentityUser User0 { get; private set; }
        [Required]
        public string User1Id { get; private set; }
        
        public IdentityUser User1 { get; private set; }
        public List<Message> Messages { get; private set; }

        public void SendMessage (IdentityUser user, string message)
        {
            Messages.Add(new Message(this, user, message));
        }
    }
}
