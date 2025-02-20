using System.ComponentModel.DataAnnotations;

namespace BAT_BANK.Models
{
    public class clsTransferViewModel
    {

        [Required(ErrorMessage ="Receiver account number is required")]
        public string receiverAccountNumber { get; set; }

        [Required(ErrorMessage ="Amount is requried")]
        public decimal? amount {  get; set; }
    }


}
