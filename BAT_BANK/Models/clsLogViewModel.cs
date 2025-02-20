using BusinessAccessLayer;
using DataAccessLayer.DTOs;

namespace BAT_BANK.Models
{
    public class clsLogViewModel
    {
        public int logID {  get; set; }
        public int onlineAccountID {  get; set; }
        public DateTime loginDate { get; set; }
        public bool status {  get; set; }

        public List<clsLogDTO> logList = clsLog.getAllLogs(clsGlobal.onlineAccount.onlineAccountID);

    }
}
