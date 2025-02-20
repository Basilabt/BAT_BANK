using BusinessAccessLayer;
using System.ComponentModel.DataAnnotations;

namespace BAT_BANK.Models
{
    public class clsClientInfoViewModel
    {
        [Required(ErrorMessage = "SSN is required")]
        public string? ssn { get; set; }

     

        [Required(ErrorMessage = "Firstname is required")]
        public string? firstName { get; set; }

        [Required(ErrorMessage = "Secondname is required")]
        public string? secondName { get; set; }

        [Required(ErrorMessage = "Thirdname is required")]
        public string? thirdName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string? lastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string? email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        public string? phoneNumber { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public clsPerson.enGender? gender { get; set; }





        public void loadCurrentLoggedClientInfo()
        {
            this.ssn = clsGlobal.account.client.person.ssn;
            this.firstName = clsGlobal.account.client.person.firstName;
            this.secondName = clsGlobal.account.client.person.secondName;
            this.thirdName = clsGlobal.account.client.person.thirdName;
            this.lastName = clsGlobal.account.client.person.lastName;   
            this.email = clsGlobal.account.client.person.email; 
            this.phoneNumber = clsGlobal.account.client.person.phoneNumber;
            this.gender = clsGlobal.account.client.person.gender;
        }

    }
}
