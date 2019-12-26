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
        [Key]
        [Required]
        public Guid ID { get; set; }
        public Conversation Conversation { get; set; }
        [Required]
        public Guid ConversationId { get; set; }
        public IdentityUser Sender { get; set; }
        [Required]
        public string SenderId { get; set; }
        public string Body { get; set; }
    }
}
