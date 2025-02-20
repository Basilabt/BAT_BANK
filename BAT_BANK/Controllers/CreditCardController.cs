using BAT_BANK.Models;
using BusinessAccessLayer;
using Microsoft.AspNetCore.Mvc;

namespace BAT_BANK.Controllers
{
    public class CreditCardController : Controller
    {
        [HttpGet]
        public IActionResult CreditCard()
        {
            clsCreditCardViewModel model = new clsCreditCardViewModel();
            model.loadCurrentLoggedClientCreditCard();

            return View(model);
        }

        [HttpPost]
        public IActionResult RenewCreditCard(clsCreditCardViewModel model)
        {
         
            if (!clsGlobal.account.card.renewCard())
            {
                ModelState.AddModelError("endDate", "Your card is not expired nor near expiration yet !");               
                return View("CreditCard",model);
            }

            TempData["Message"] = "Credit card renewed succesfully, Please go to the nearerst bank to obtain your new card";
       
            return RedirectToAction("Dashboard", "Main");

        }
    }
}
