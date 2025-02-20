using BAT_BANK.Models;
using BusinessAccessLayer;
using Microsoft.AspNetCore.Mvc;

namespace BAT_BANK.Controllers
{
    [Controller]
    public class MainController : Controller
    {
        [HttpGet]
        public IActionResult Dashboard()
        {

            clsDashboardViewModel model = new clsDashboardViewModel();

            ViewData["GreetingMessage"] = $"Welcome back {clsGlobal.account.client.person.firstName},";
            ViewData["Balance"] = $"Balance = {model.balance}";

            return View();
        }
    }
}
