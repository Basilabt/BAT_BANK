namespace BAT_BANK_API.DTOs.ClientInfoDTO
{
    public class clsUpdateClientInfoRequestDTO
    {
        public int onlineAccountID {  get; set; }
        public string email {  get; set; }
        public string phoneNumber { get; set; }
        public string jwt {  get; set; }

    }
}
