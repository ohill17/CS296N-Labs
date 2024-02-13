using AllAboutWeezer.Models;

namespace AllAboutWeezer.Data
{
    namespace AllAboutWeezer.Data
    {
        public class FakeForumRepository : IForumRepository
        {
            private List<Message> _messages = new List<Message>();

            public Task<Message> GetMessageByIdAsync(int id)
            {
                throw new NotImplementedException();
            }

            public Task<List<Message>> GetMessagesAsync()
            {
                throw new NotImplementedException();
            }

            public Task<int> StoreMessageAsync(Message message)
            {
                message.MessageId = _messages.Count + 1; // Temp
                _messages.Add(message);
                return Task.FromResult(message.MessageId);
            }
        }
    }
}