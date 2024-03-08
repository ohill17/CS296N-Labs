using AllAboutWeezer.Models;
using Microsoft.EntityFrameworkCore;


namespace AllAboutWeezer.Data
{
    public class ForumRepository : IForumRepository
    {
        private readonly AppDbContext _context;

        public ForumRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Message> GetMessageByIdAsync(int id)
        {
            return await _context.Message.FindAsync(id);
        }

        public async Task<List<Message>> GetMessagesAsync()
        {
            return await _context.Message.ToListAsync();
        }

        public async Task<int> StoreMessageAsync(Message message)
        {
            _context.Add(message);
            return await _context.SaveChangesAsync();
        }
        public int DeleteMessage(int messageId)
        {
            Message message = GetMessageByIdAsync(messageId).Result;
            // If the message has replies, remove them first to avoid a FK constraint violation
            if (message.Replies.Count > 0)
            {
                foreach (var reply in message.Replies)
                {
                    _context.Message.Remove(reply);
                }
            }
            _context.Message.Remove(message);
            return _context.SaveChanges();
        }
    }
}
