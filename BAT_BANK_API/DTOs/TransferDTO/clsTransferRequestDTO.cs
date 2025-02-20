namespace BAT_BANK_API.DTOs.Transfer
{
    public class clsTransferRequestDTO
    {
        public int instantiatorOnlineAccountID { get; set; }
        public string receiverAccountNumber { get; set; }
        public decimal amount { get; set; }
        public string jwt { get; set; }

    }
}
