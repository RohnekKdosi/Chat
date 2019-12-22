using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Chat.Services
{
    public class DataProvider
    {
        private ApplicationDbContext _db;
        //private ConvoDbContext _db;
        public DataProvider(ApplicationDbContext dbContext)
        {
            _db = dbContext;
            //_db = convoDb;
        }
        public IEnumerable<IdentityUser> GetUsers ()
        {
            return _db.Users.ToList();
        }
        public Conversation GetConversation (string userId, string currentUserId)
        {
            if(_db.Conversations.ToList().Find(c => (c.User0Id == userId && c.User1Id == currentUserId) || (c.User1Id == userId && c.User0Id == currentUserId)) == null)
            {
                _db.Conversations.Add(new Conversation(_db.Users.Find(currentUserId), _db.Users.Find(userId)));
            }
            return _db.Conversations.ToList().Find(c => (c.User0Id == userId && c.User1Id == currentUserId) || (c.User1Id == userId && c.User0Id == currentUserId));
        }
        public IEnumerable<Conversation> GetConvos ()
        {
            return _db.Conversations.ToList();
        }

        public void SendMessage (string target, IdentityUser sender, string message)
        {
            var conversation = _db.Conversations.ToList().Find(c => (c.User0Id == target && c.User1Id == sender.Id) || (c.User1Id == target && c.User0Id == sender.Id));
            conversation.SendMessage(sender, message);
        }
        
    }
}
