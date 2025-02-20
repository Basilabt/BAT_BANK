namespace BAT_BANK_API.DTOs.Withdraw
{
    public class clsWithdrawResponseDTO    
    {
        public bool isSucceed {  get; set; }
        public string message { get; set; }
        public decimal newBalance { set; get; }
    }
}
