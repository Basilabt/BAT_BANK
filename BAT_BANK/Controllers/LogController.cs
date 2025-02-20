using BAT_BANK.Models;
using Microsoft.AspNetCore.Mvc;

namespace BAT_BANK.Controllers
{
    public class LogController : Controller
    {
        [HttpGet]
        public IActionResult Logs()
        {

            clsLogViewModel model = new clsLogViewModel();

            return View(model);
        }
    }
}
