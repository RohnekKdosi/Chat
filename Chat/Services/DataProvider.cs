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
        public IEnumerable<IdentityUser> GetUsers()
        {
            return _db.Users.ToList();
        }
        public Conversation GetConversation(string userId, string currentUserId)
        {
            if (_db.Conversations.ToList().Find(c => (c.User0Id == userId && c.User1Id == currentUserId) || (c.User1Id == userId && c.User0Id == currentUserId)) == null)
            {
                if (userId != "")
                {
                    _db.Conversations.Add(new Conversation { User0Id = currentUserId, User1Id = userId });
                    _db.SaveChanges();
                }

            }
            return _db.Conversations.ToList().Find(c => (c.User0Id == userId && c.User1Id == currentUserId) || (c.User1Id == userId && c.User0Id == currentUserId));
        }
        public IEnumerable<Conversation> GetConvos()
        {
            return _db.Conversations.ToList();
        }

        public void SendMessage(Conversation conversation, IdentityUser sender, string message)
        {
            if (conversation.User0 == sender || conversation.User1 == sender)
            {
                _db.Messages.Add(new Message { Conversation = conversation, Sender = sender, Body = message });
            }
        }

    }
}
