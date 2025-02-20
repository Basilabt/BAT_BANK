using BAT_BANK.Models;
using BusinessAccessLayer;
using Microsoft.AspNetCore.Mvc;

namespace BAT_BANK.Controllers
{
    public class MoneyActionController : Controller
    {
        [HttpGet]
        public IActionResult Deposit()
        {   
            return View();
        }

        [HttpPost]
        public IActionResult PerformDepositAction(clsDepositViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View("Deposit", model);
            }

            clsAction action = new clsAction();

            action.mode = clsAction.enMode.AddNew;
            action.actionTypeID = (int)clsActionType.enAction.Deposit;
            action.istantiatorAccountID = clsGlobal.account.accountID;
            action.receiverAccountID = clsGlobal.account.accountID;
            action.amount = model.amount.Value;



            if(action.save() && clsAccount.performDepositAction(action.istantiatorAccountID,action.amount)) 
            {
                TempData["Message"] = "Operation performed succsessfully";                
                return RedirectToAction("Dashboard", "Main");
            }

            return View("Deposit", model);
        }






        [HttpGet]
        public IActionResult Withdraw()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PerformWithdrawAction(clsWithdrawViewModel model)
        {

            if (model.amount > clsGlobal.account.balance)
            {
                ModelState.AddModelError("amount", "Your balance is not enough to perform this action");
                return View("Withdraw",model);
            }
            

            if (!ModelState.IsValid)
            {
                return View("Withdraw", model);
            }

            clsAction action = new clsAction();

            action.mode = clsAction.enMode.AddNew;
            action.actionTypeID = (int)clsActionType.enAction.Withdraw;
            action.istantiatorAccountID = clsGlobal.account.accountID;
            action.receiverAccountID = clsGlobal.account.accountID;
            action.amount = model.amount.Value;



            if (action.save() && clsAccount.performWithdrawAction(action.istantiatorAccountID, action.amount))
            {
                TempData["Message"] = "Operation performed succsessfully";
                return RedirectToAction("Dashboard", "Main");
            }

            return View("Withdraw", model);
        }







        [HttpGet]
        public IActionResult Transfer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PerformTransferAction(clsTransferViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View("Transfer",model);
            }


            if (!clsAccount.doesAccountExistByAccountNumber(model.receiverAccountNumber))
            {
                ModelState.AddModelError("receiverAccountNumber", "There is no account exist with this account number");
                return View("Transfer", model);
            }


            clsAction action = new clsAction();

            action.mode = clsAction.enMode.AddNew;
            action.actionTypeID = (int)clsActionType.enAction.Transfer;
            action.istantiatorAccountID = clsGlobal.account.accountID;
            action.receiverAccountID = clsAccount.findAccountByAccountNumber(model.receiverAccountNumber).accountID;
            action.amount = model.amount.Value;



            if (action.save() && clsAccount.performTransferAction(action.istantiatorAccountID,action.receiverAccountID,action.amount))
            {
                TempData["Message"] = "Operation performed succsessfully";
                return RedirectToAction("Dashboard", "Main");
            }

            return View("Transfer", model);
        }





        [HttpGet]
        public IActionResult MoneyActionHistory()
        {
            clsMoneyActionsHistoryViewModel model = new clsMoneyActionsHistoryViewModel();

            return View("History",model);
        }


    }
}





            


           