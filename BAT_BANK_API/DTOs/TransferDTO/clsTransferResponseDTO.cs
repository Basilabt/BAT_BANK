
namespace BAT_BANK_API.DTOs.Transfer
{
    public class clsTransferResponseDTO    {
        public bool isSucceed {  get; set; }
        public string message { get; set; }

        public decimal newBalance { get; set; }
    }
}
