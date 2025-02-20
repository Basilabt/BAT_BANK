using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class clsEPurchaseHistoryDTO
    {
        public  string name { set; get; }
        public string description { set; get; }
        public  decimal price { set; get; }
        public DateTime purchaseDate { set; get; }
        public bool status {  set; get; }

    }
}
