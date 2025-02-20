namespace BAT_BANK_API.DTOs.ClientInfoDTO
{
    public class clsClientInfoResponseDTO
    {            
        public int personID { get; set; }
        public string ssn { get; set; }
        public string firstName { get; set; }
        public string secondName { get; set; }
        public string thirdName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public short gender { get; set; }
    }
}
