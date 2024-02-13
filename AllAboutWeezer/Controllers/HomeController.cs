using AllAboutWeezer.Data;
using AllAboutWeezer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AllAboutWeezer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IForumRepository _repository;

        public HomeController(IForumRepository repository)
        {
            _repository = repository;
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

            int result = await _repository.StoreMessageAsync(model);

            return RedirectToAction("StoriesPost", new { model.MessageId });
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
