using AllAboutWeezer.Data;
using AllAboutWeezer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AllAboutWeezer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IForumRepository _repository;
        readonly UserManager<AppUser> _userManager;

        public HomeController(IForumRepository repository, UserManager<AppUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public async Task<IActionResult> StoriesPost(string messageId)
        {
            var messages = await _repository.GetMessagesAsync();
            // You might need to filter messages based on the messageId here
            return View(messages);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult History()
        {
            return View();
        }

        public IActionResult Stories()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Stories(Message model)
        {
            model.Date = DateOnly.FromDateTime(DateTime.Now);

            if (_userManager != null) // Don't get a user when doing unit tests
            {
                // Get the sender
                model.From = await _userManager.GetUserAsync(User);
            }

            if (model.From != null) // Ensure the sender is valid
            {
                
                    int result = await _repository.StoreMessageAsync(model);
                    return RedirectToAction("StoriesPost", new { messageId = model.MessageId });
                
               
            }
            else
            {
                ModelState.AddModelError("", "Sender not authenticated or not found");
                return View(model);
            }
        }
        [Authorize]
        public IActionResult Reply(int? originalMessageId)
        {
            Message message = new Message();
            message.OriginalMessageId = originalMessageId;
            return View(message);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Reply(Message model)
        {
            model.Date = DateOnly.FromDateTime(DateTime.Now);
            if (_userManager != null) // Don't get a user when doing unit tests
            {
                // Get the sender
                model.From = _userManager.GetUserAsync(User).Result;
            }

            // Get the message being replied to (guaranteed to be not null by design)
            Message originalMessage = await _repository.GetMessageByIdAsync(model.OriginalMessageId.Value);

            // Save the message
            await _repository.StoreMessageAsync(model);

            // Add the reply to the original message
            if (originalMessage.Replies == null)
            {
                originalMessage.Replies = new List<Message>(); // Ensure the Replies collection is initialized
            }
            originalMessage.Replies.Add(model);
           // _repository.UpdateMessage(originalMessage);

            //TODO: Do something interesting/useful with the MessageId or don't send it. It's not currently used.
            return RedirectToAction("StoriesPost", new { messageId = model.MessageId }); // Redirect to StoriesPost with messageId parameter
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
