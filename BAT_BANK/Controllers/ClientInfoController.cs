using BAT_BANK.Models;
using BusinessAccessLayer;
using Microsoft.AspNetCore.Mvc;

namespace BAT_BANK.Controllers
{
    public class ClientInfoController : Controller
    {

        [HttpGet]
        public IActionResult ClientInfo()
        {
           clsClientInfoViewModel model = new clsClientInfoViewModel();

            model.loadCurrentLoggedClientInfo();
            
            return View(model);
        }


        [HttpPost]
        public IActionResult UpdateClientInfo(clsClientInfoViewModel model) 
        {
            if (!ModelState.IsValid)
            {
                return View("ClientInfo",model);
            }

            clsGlobal.account.client.person.email = model.email;
            clsGlobal.account.client.person.phoneNumber = model.phoneNumber;
            clsGlobal.account.client.person.gender = model.gender ?? clsPerson.enGender.Male;

            clsGlobal.account.client.person.mode = clsPerson.enMode.Update;
           if(clsGlobal.account.client.person.save())
           {
                TempData["Message"] = "Client Info Updated Successfully";

           } else
           {
                TempData["Message"] = "Failed to update client info";
            }


            return RedirectToAction("Dashboard", "Main");
        }

    }
}
