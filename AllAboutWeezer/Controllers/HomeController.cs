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
        
        public async Task<IActionResult> StoriesPost(string messageId, Message model)
        {

            model.From = await _userManager.GetUserAsync(User);
            var messages = await _repository.GetMessagesAsync();
            // You might need to filter messages based on the messageId here
            return View(messages);
        }
        public IActionResult DeleteForumPost(int messageId)
        {
            // TODO: Do something like redirect if the delete fails
            _repository.DeleteMessage(messageId);
            return RedirectToAction("StoriesPost");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult History()
        {
            return View();
        }


        [Authorize]
        public IActionResult Stories()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Stories(Message model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Return the view with validation errors if model state is not valid
            }

            model.Date = DateOnly.FromDateTime(DateTime.Now);

            if (_userManager != null)
            {
                // Get the sender if not already set
                if (model.From == null)
                {
                    model.From = await _userManager.GetUserAsync(User);
                }
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

        [HttpPost]

        [Authorize]
        public async Task<IActionResult> Reply(Message model)
        {
            model.Date = DateOnly.FromDateTime(DateTime.Now);
            if (_userManager != null)
            {

                model.From = await _userManager.GetUserAsync(User);

            }

            Message originalMessage = await _repository.GetMessageByIdAsync(model.OriginalMessageId.Value);

            //  model.To = originalMessage.From;

            await _repository.StoreMessageAsync(model);

            originalMessage.Replies.Add(model);
            //  _repository.UpdateMessage(originalMessage);

            return RedirectToAction("StoriesPost", new { model.MessageId });
        }

    }
}
