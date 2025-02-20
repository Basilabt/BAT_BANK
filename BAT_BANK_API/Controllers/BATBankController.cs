using BAT_BANK_API.DTOs;
using BusinessAccessLayer;
using DataAccessLayer.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.IdentityModel.Tokens;
using BAT_BANK_API.Services;
using BAT_BANK_API.DTOs.LoginDTO;
using Microsoft.AspNetCore.Authorization;
using BAT_BANK_API.DTOs.CreditCardDTO;
using BAT_BANK_API.DTOs.LogDTO;
using BAT_BANK_API.DTOs.ClientInfoDTO;
using BAT_BANK_API.DTOs.Deposit;
using BAT_BANK_API.DTOs.Withdraw;
using BAT_BANK_API.DTOs.Transfer;
using Microsoft.Identity.Client;
using BAT_BANK_API.DTOs.ActionsHistoryDTO;
using BAT_BANK_API.DTOs.EvoucherDTO;



namespace BAT_BANK_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BATBankController : ControllerBase
    {

        [HttpPost("OnlineAccount/Login",Name ="Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
       public IActionResult Login([FromBody] clsLoginRequestDTO loginRequestDTO)
       {
            if(string.IsNullOrEmpty(loginRequestDTO.username) || string.IsNullOrEmpty(loginRequestDTO.password))
            {               
                return BadRequest("Username and password are required");
            }

            clsOnlineAccount onlineAccount = clsOnlineAccount.login(loginRequestDTO.username, loginRequestDTO.password);

            if(onlineAccount == null)
            {   
                if(clsOnlineAccount.doesOnlineAccountExistByUsername(loginRequestDTO.username))
                {
                    clsLog.addNewLog(new clsLogDTO { logID = -1, onlineAccountID = clsOnlineAccount.getOnlineAccountIDByUsername(loginRequestDTO.username), loginDate = DateTime.Now, status = false });
                }
               
                return Unauthorized("Incorrect Username Or Password");
            }

            if (!clsOnlineAccount.isOnlineAccountActiveByUsername(loginRequestDTO.username))
            {
                clsLog.addNewLog(new clsLogDTO { logID = -1, onlineAccountID = onlineAccount.onlineAccountID, loginDate = DateTime.Now, status = false });
                return Unauthorized("Account Is Not Active");
            }


            string jwt = clsTokenService.GenerateJWT(onlineAccount);


            // Log User Correct Login (Instead of performing another api call from client side) 
            clsLog.addNewLog(new clsLogDTO {logID=-1,onlineAccountID = onlineAccount.onlineAccountID,loginDate=DateTime.Now,status=true});
          

            return Ok(new clsLoginResponseDTO { onlineAccountID = onlineAccount.onlineAccountID , username = onlineAccount.username , jwt = jwt });
       }











        [HttpPost("OnlineAccount/Client/Info", Name = "GetClientInfo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetClientInfo([FromBody] clsClientInfoRequestDTO clientInfoRequestDTO)
        {

            if (clientInfoRequestDTO.onlineAccountID <= 0)
            {
                return BadRequest("Invalid Online Account ID");
            }

            clsPerson perosn = clsPerson.findPersonByOnlineAccountID(clientInfoRequestDTO.onlineAccountID);

            if(perosn == null)
            {
                return Unauthorized("No Client Is Available");
            }


            return Ok(new clsClientInfoResponseDTO {personID = perosn.personID, ssn = perosn.ssn, firstName = perosn.firstName, secondName = perosn.secondName, thirdName = perosn.thirdName, lastName = perosn.lastName, email = perosn.email,phoneNumber=perosn.phoneNumber,gender=(short)perosn.gender });
        }


        [HttpPost("OnlineAccount/Client/Info/Update", Name = "UpdateClientInfo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateClientInfo([FromBody] clsUpdateClientInfoRequestDTO updateClientInfoRequestDTO) 
        {

            if (updateClientInfoRequestDTO.onlineAccountID <= 0)
            {
                return BadRequest("Invalid Online Account ID");
            }

            if (string.IsNullOrEmpty(updateClientInfoRequestDTO.email) || string.IsNullOrEmpty(updateClientInfoRequestDTO.phoneNumber))
            {
                return BadRequest("Invalid Input");
            }


            clsPerson perosn = clsPerson.findPersonByOnlineAccountID(updateClientInfoRequestDTO.onlineAccountID);

            if (perosn == null)
            {
                return Unauthorized("No Client Is Available");
            }

            perosn.email = updateClientInfoRequestDTO.email;
            perosn.phoneNumber = updateClientInfoRequestDTO.phoneNumber;
            perosn.mode = clsPerson.enMode.Update;

            bool isSucceed = perosn.save();

            string message = isSucceed ? "Information updated successfully" : "Failed to update information"; 

            return Ok(new clsUpdateClientInfoResponseDTO {isSucceed=isSucceed , message = message });
        }










        //[Authorize]
        [HttpPost("OnlineAccount/CreditCard/Info", Name = "GetCreditCardInformation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetCreditCardInformation([FromBody] clsCreditCardInformationRequestDTO creditCardInformationRequestDTO)
        {
            if(creditCardInformationRequestDTO.onlineAccountID <= 0)
            {
                return BadRequest("Invalid Online Account ID");
            }

            clsCard card = clsCard.getCardByOnlineAccountID(creditCardInformationRequestDTO.onlineAccountID);

            if (card == null) 
            {
                return Unauthorized("No Card Is Available");
            }
           
          
            return Ok(new clsCreditCardInformationResponseDTO {cardID = card.cardID , cardNumber = card.cardNumber , pin = card.pin , issueDate = card.issueDate , endDate = card.endDate });
        }


        [HttpPost("OnlineAccount/CreditCard/Renew", Name = "RenewCreditCard")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult RenewCreditCard([FromBody] clsCreditCardRenewalRequestDTO creditCardRenewalRequestDTO)
        {
            if(creditCardRenewalRequestDTO.onlineAccountID <= 0)
            {
                return BadRequest("Invalid Online Account ID");
            }

            clsCard card = clsCard.getCardByOnlineAccountID(creditCardRenewalRequestDTO.onlineAccountID);

            if (card == null)
            {
                return Unauthorized("No Card Is Available");
            }

            bool isRenewalSucceed = card.renewCard();
            string message = (isRenewalSucceed) ? "Renewd successfully, please visit your bank to obtain new card" : "Your card is not expired yet nor near expairation"; 

 
            return Ok(new clsCreditCardRenewalResponseDTO { isRenewalProcessSucceed = isRenewalSucceed ,message = message});
        }









        [HttpPost("OnlineAccount/Actions/Deposit" , Name = "Deposit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Deposit([FromBody] clsDepositRequestDTO depositRequestDTO)
        {
            if(depositRequestDTO.onlineAccountID <= 0)
            {
                return BadRequest("Invalid Online Account ID");
            }

            clsAccount account = clsAccount.findAccountByOnlineAccountID(depositRequestDTO.onlineAccountID);

            if(account == null)
            {
                return Unauthorized("No Account Is Available");
            }

            if(depositRequestDTO.amount <= 0 || depositRequestDTO.amount > 2000)
            {
                return Unauthorized("Invalid Operation");
            }

            bool isSucceed = clsAccount.performDepositAction(depositRequestDTO.onlineAccountID,depositRequestDTO.amount);         
            decimal newBalance = (isSucceed) ? account.balance + depositRequestDTO.amount : account.balance;
            string message = (isSucceed) ? $"Operation performed successfully, your balance is {newBalance}" : "Failed to perform operation";

            return Ok(new clsDepositResponseDTO { isSucceed =isSucceed , message = message , newBalance = newBalance });
        }


        [HttpPost("OnlineAccount/Actions/Withdraw", Name = "Withdraw")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Withdraw([FromBody] clsWithdrawRequestDTO withdrawRequestDTO)
        {
            if (withdrawRequestDTO.onlineAccountID <= 0)
            {
                return BadRequest("Invalid Online Account ID");
            }


            clsAccount account = clsAccount.findAccountByOnlineAccountID(withdrawRequestDTO.onlineAccountID);

            if(account == null)
            {
                return Unauthorized("No Account Is Available");
            }

            if(withdrawRequestDTO.amount <= 0 || withdrawRequestDTO.amount > account.balance)
            {
                return Unauthorized("Invalid Operation, Insufficient Balance");
            }

            bool isSucceed = clsAccount.performWithdrawAction(withdrawRequestDTO.onlineAccountID,withdrawRequestDTO.amount);
            decimal newBalance = (isSucceed) ? account.balance - withdrawRequestDTO.amount : account.balance;
            string message = (isSucceed) ? $"Operation performed successfully, your balance is {newBalance}" : "Failed to perform operation";


            return Ok(new clsWithdrawResponseDTO { isSucceed = isSucceed , message = message , newBalance = newBalance});
        }


        [HttpPost("OnlineAccount/Actions/Transfer", Name = "Transfer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Transfer([FromBody] clsTransferRequestDTO transferRequestDTO)
        {
            if(transferRequestDTO.receiverAccountNumber == "")
            {
                return BadRequest("Invalid Online Account ID");
            }

            clsAccount sender = clsAccount.findAccountByOnlineAccountID(transferRequestDTO.instantiatorOnlineAccountID);
            clsAccount receiver = clsAccount.findAccountByAccountNumber(transferRequestDTO.receiverAccountNumber);

            if(sender == null || receiver == null)
            {
                return Unauthorized("No Online Account Find");
            }

            if(transferRequestDTO.amount <= 0 || transferRequestDTO.amount > sender.balance)
            {
                return Unauthorized("Invalid Operation, Insufficient Balance");
            }

            bool isSucceed = clsAccount.performTransferAction(sender.onlineAccountID,receiver.onlineAccountID,transferRequestDTO.amount);
            string message = (isSucceed) ? "Operation performed successfully" : "Failed to perform opeeration";
            decimal newBalance = (isSucceed) ? sender.balance - transferRequestDTO.amount : sender.balance;


            return Ok(new clsTransferResponseDTO { isSucceed = isSucceed , message = message , newBalance = newBalance});
        }


        [HttpPost("OnlineAccount/Actions/Summary",Name ="GetActionsSummary")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<clsActionHistoryDTO>> GetActionsSummary([FromBody] clsActionsHistoryRequestDTO actionsHistoryRequestDTO)
        {   
            if(actionsHistoryRequestDTO.onlineAccountID <= 0)
            {
                return BadRequest("Invalid Online Account ID");
            }

            clsAccount account = clsAccount.findAccountByOnlineAccountID(actionsHistoryRequestDTO.onlineAccountID);

            if(account == null)
            {
                return Unauthorized("No Online Account Find");
            }

            List<clsActionHistoryDTO> list = clsAction.getAccountActionsHistory(account.onlineAccountID);

            if(list.Count == 0)
            {
                return BadRequest("No Actions Found");
            }

            list.Reverse();

            return Ok(list);
        }





        [HttpPost("OnlineAccount/Logs", Name = "GetOnlineAccountLogs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetOnlineAccountLogs([FromBody] clsLogRequestDTO logRequestDTO) 
        {

            if (logRequestDTO.onlineAccountID <= 0)
            {
                return BadRequest("Invalid Online Account ID");
            }

            List<clsLogDTO> list = clsLog.getAllLogs(logRequestDTO.onlineAccountID);

            if (list.Count == 0)
            {
                return Unauthorized("No Logs Available");
            }



            return Ok(list);
        }



        [HttpPost("Products/Evouchers", Name = "GetEvouchers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetEvouchers()
        {
            List<clsProduct> list = clsProduct.getAllProducts();

            if(list.Count == 0)
            {
                return Unauthorized("No products available");
            }

            return Ok(list);
        }



        [HttpPost("Products/Evouchers/Purchase", Name = "PurchaseEvoucher")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PurchaseEvoucher([FromBody] clsEvoucherPurchaseRequestDTO voucherPurchaseRequestDTO)
        {
            if(voucherPurchaseRequestDTO.onlineAccountID <= 0)
            {
                return BadRequest("Invalid Online Account ID");
            }

            clsProduct product = clsProduct.getProducctByProductID(voucherPurchaseRequestDTO.productID);

            if(product == null)
            {
                return Unauthorized("Product Not Found");
            }

            clsAccount account = clsAccount.findAccountByOnlineAccountID(voucherPurchaseRequestDTO.onlineAccountID);

            if(account == null)
            {
                return Unauthorized("Account Not Found");
            }

            if(account.balance < product.price)
            {
                return Unauthorized("Insufficient Balance");
            }

            clsEPurchase purchase = new clsEPurchase();
            purchase.productID = product.productID;
            purchase.onlineAccountID = account.onlineAccountID;
            purchase.purchaseDate = DateTime.Now;
            purchase.status = true;
            purchase.mode = clsEPurchase.enMode.AddNew;

            bool isSucceed = purchase.save();
            string message = (isSucceed) ? "Item bought succesfully, an sms with the voucher code will be sent to you soon" : "Failed to purchase";

            return Ok(new clsEvoucherPurchaseResponseDTO {isSucceed=isSucceed,message=message});
        }



        [HttpPost("Products/Evouchers/Purchase/History", Name = "GetEVouchersPurchaseHistory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetEVouchersPurchaseHistory([FromBody] clsVouchersPurchaseHistoryRequestDTO voucherPurchaseHistoryRequstDTO)
        {
            if(voucherPurchaseHistoryRequstDTO.onlineAccountID <= 0)
            {
                return BadRequest("Invalid Online Account ID");
            }

            List<clsEPurchaseHistoryDTO> history = clsEPurchase.getAllPurchasesHistory(voucherPurchaseHistoryRequstDTO.onlineAccountID);

            if(history.Count == 0)
            {
                return Unauthorized("No purchases found");
            }


            return Ok(history);
        }




    }
}
