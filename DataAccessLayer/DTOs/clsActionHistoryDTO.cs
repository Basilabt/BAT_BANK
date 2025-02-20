using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class clsActionHistoryDTO
    {
        public string actionType { get; set; }
        public string receivedAccountNumber { get; set; }
        public decimal amount { get; set; }
    }
}
