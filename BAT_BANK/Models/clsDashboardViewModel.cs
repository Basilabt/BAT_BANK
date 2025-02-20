using BusinessAccessLayer;

namespace BAT_BANK.Models
{
    public class clsDashboardViewModel
    {
        public decimal balance {  get; set; }

        public clsDashboardViewModel()
        {
            this.balance = clsAccount.getAccountBalanceByAccountID(clsGlobal.account.accountID);
        }
    }
}
