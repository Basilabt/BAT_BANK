using System.ComponentModel.DataAnnotations;

namespace BAT_BANK.Models
{
    public class clsLoginViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string? username {  get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? password { get; set; }
    }
}
