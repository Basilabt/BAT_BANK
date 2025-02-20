using BAT_BANK.Models;
using BusinessAccessLayer;
using Microsoft.AspNetCore.Mvc;

namespace BAT_BANK.Controllers
{
    public class ProductsController : Controller
    {



        [HttpGet] 
        public IActionResult EVouchers()
        {
            clsProductsViewModel model = new clsProductsViewModel();


            return View(model);
        }



        [HttpPost]
        public IActionResult PurchaseEvoucher(int productID)
        {
            clsProduct product = clsProduct.getProducctByProductID(productID);
          
            clsEPurchase purchase = new clsEPurchase();
            purchase.onlineAccountID = clsGlobal.onlineAccount.onlineAccountID;
            purchase.productID = productID;
            purchase.purchaseDate = DateTime.Now;
            purchase.mode = clsEPurchase.enMode.AddNew;


            if (product.price <= clsGlobal.account.balance)
            {
                purchase.status = true;
                TempData["Message"] = "Item bought succesfully, an sms with the voucher code will be sent to you soon";

            } else
            {
                purchase.status = false;
                TempData["Message"] = "Insufficient balace";
            }

            
            purchase.save();
                
            return RedirectToAction("Dashboard","Main");
        }
    }
}
