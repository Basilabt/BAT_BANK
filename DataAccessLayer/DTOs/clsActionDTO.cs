using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class clsActionDTO
    {
        public int actionID { get; set; }
        public int actionTypeID { get; set; }
        public int istantiatorAccountID { get; set; }
        public int receiverAccountID { get; set; }
        public decimal amount { get; set; }
    }
}
