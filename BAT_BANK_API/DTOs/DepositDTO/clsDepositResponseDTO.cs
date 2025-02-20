namespace BAT_BANK_API.DTOs.Deposit
{
    public class clsDepositResponseDTO
    {
        public bool isSucceed {  get; set; }
        public string message { get; set; }
        public decimal newBalance { set; get; }
    }
}
