using BusinessAccessLayer;
using System.ComponentModel.DataAnnotations;

namespace BAT_BANK.Models
{
    public class clsCreditCardViewModel
    {
        public int cardID { get; set; }
        public string cardNumber { set; get; }
        public string pin { set; get; }
        public DateTime creationDate { set; get; }
        public DateTime endDate { set; get; }

        public string fullname {  set; get; }

        
        public void loadCurrentLoggedClientCreditCard()
        {
            this.cardID = clsGlobal.account.card.cardID;
            this.cardNumber = clsGlobal.account.card.cardNumber;
            this.pin = clsGlobal.account.card.pin;
            this.creationDate = clsGlobal.account.card.issueDate;
            this.endDate = clsGlobal.account.card.endDate;
            this.fullname = clsGlobal.account.client.person.fullname;
        }

         
    }
}
