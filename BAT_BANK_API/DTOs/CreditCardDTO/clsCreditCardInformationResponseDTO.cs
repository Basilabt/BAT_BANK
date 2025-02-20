using Microsoft.OpenApi.Models;

namespace BAT_BANK_API.DTOs.CreditCardDTO
{
    public class clsCreditCardInformationResponseDTO
    {
        public int cardID { get; set; }
        public string cardNumber { set; get; }
        public string pin { set; get; }
        public DateTime issueDate { set; get; }
        public DateTime endDate { set; get; }
    }
}
