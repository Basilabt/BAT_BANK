using System.ComponentModel.DataAnnotations;

namespace BAT_BANK.Models
{
    public class clsDepositViewModel
    {

        [Required(ErrorMessage = "Invalid amount")]
        [Range(0, 2000,ErrorMessage = "Amount must be greater than zero and less that 2000 in one action")]
        public decimal? amount { get; set; }
    }
}
