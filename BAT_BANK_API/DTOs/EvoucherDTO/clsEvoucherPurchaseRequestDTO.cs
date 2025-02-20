namespace BAT_BANK_API.DTOs.EvoucherDTO
{
    public class clsEvoucherPurchaseRequestDTO
    {
        public int onlineAccountID {  get; set; }
        public int productID { get; set; }
        public string jwt {  get; set; }
    }
}
