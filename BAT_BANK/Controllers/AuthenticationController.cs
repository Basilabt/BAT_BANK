using BAT_BANK.Models;
using BusinessAccessLayer;
using Microsoft.AspNetCore.Mvc;

namespace BAT_BANK.Controllers
{
    [Controller]
    public class AuthenticationController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(clsLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else if(!clsOnlineAccount.doesOnlineAccountExistByUsername(model.username))
            {
                ModelState.AddModelError("username", "Username does not exist !");
                return View(model);

            }
            else if(!clsOnlineAccount.isOnlineAccountActiveByUsername(model.username))
            {
                ModelState.AddModelError("username", "Account is not active !");
                return View(model);
                
            } 

            clsOnlineAccount onlineAccount = clsOnlineAccount.login(model.username, model.password);

            
            clsLog log = new clsLog();
            log.onlineAccountID = clsOnlineAccount.getOnlineAccountIDByUsername(model.username); 
            log.mode = clsLog.enMode.AddNew;
            log.loginDate = DateTime.Now;
          

            if(onlineAccount == null)
            {
                log.status = false;
                log.save();
                ModelState.AddModelError("username", "Incorrect password");
                return View(model);                
            }

            // Logged In Succesfully
            log.status = true;
            log.save();

            clsGlobal.account = clsAccount.findAccountByOnlineAccountID(onlineAccount.onlineAccountID);
            clsGlobal.onlineAccount = onlineAccount;

            clsGlobal.account.loadCompositedObjects();


           


            return RedirectToAction("Dashboard","Main");
        }



        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register(clsRegisterViewModel model)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            clsGlobal.account = null;
            clsGlobal.onlineAccount = null;

            return RedirectToAction("Login", "Authentication");
        }

    }
}
