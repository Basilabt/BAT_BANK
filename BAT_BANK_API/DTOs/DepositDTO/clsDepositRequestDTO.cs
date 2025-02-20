namespace BAT_BANK_API.DTOs.Deposit
{
    public class clsDepositRequestDTO
    {        
        public int onlineAccountID {  get; set; }
        public decimal amount { get; set; }

        public string jwt {  get; set; }
    }
}
