using System.ComponentModel.DataAnnotations;

namespace BAT_BANK_API.DTOs.LoginDTO
{
    public class clsLoginRequestDTO
    {
        public string username { set; get; }

        public string password { set; get; }
    }
}
