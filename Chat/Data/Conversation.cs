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

        [Required]
        public string User0Id { get; set; }
        [ForeignKey("User0Id")]
        public IdentityUser User0 { get; set; }
        [Required]
        public string User1Id { get; set; }
        [ForeignKey("User1Id")]
        public IdentityUser User1 { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
