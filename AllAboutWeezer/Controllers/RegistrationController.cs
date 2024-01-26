using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AllAboutWeezer.Models;

namespace AllAboutWeezer.Controllers
{
    public class RegistrationController : Controller
    {
        private UserManager<SignUp> userManager;

        public RegistrationController(UserManager<SignUp> userMngr)
        {
            userManager = userMngr;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Registration model)
        {
            if (ModelState.IsValid)
            {
                var user = new SignUp { UserName = model.Username, Name = model.Name };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }







        public ViewResult AccessDenied()
        {
            return View();
        }




    }
}