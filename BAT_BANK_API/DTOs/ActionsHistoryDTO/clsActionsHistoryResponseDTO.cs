namespace BAT_BANK_API.DTOs.ActionsHistoryDTO
{
    public class clsActionsHistoryResponseDTO
    {
        public string actionType { get; set; }
        public string receivedAccountNumber { get; set; }
        public decimal amount { get; set; }
    }
}
