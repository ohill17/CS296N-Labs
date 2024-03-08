using AllAboutWeezer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AllAboutWeezer.Data
{
    public interface IForumRepository
    {
        Task<List<Message>> GetMessagesAsync();
        Task<Message> GetMessageByIdAsync(int id);
        Task<int> StoreMessageAsync(Message message);
        public int DeleteMessage(int messageId);
    }
}
