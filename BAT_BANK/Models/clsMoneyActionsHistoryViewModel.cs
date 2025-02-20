using BusinessAccessLayer;
using DataAccessLayer.DTOs;

namespace BAT_BANK.Models
{
    public class clsMoneyActionsHistoryViewModel
    {
        public List<clsActionHistoryDTO> history { get; set; }

        public clsMoneyActionsHistoryViewModel()
        {
            this.history = clsAction.getAccountActionsHistory(clsGlobal.account.accountID);
        }
    }
}
