namespace BAT_BANK_API.DTOs.Withdraw
{
    public class clsWithdrawRequestDTO
    {
        public int onlineAccountID { set; get; }
        public decimal amount { set; get; }       
        public string jwt {  set; get; }
    }
}
