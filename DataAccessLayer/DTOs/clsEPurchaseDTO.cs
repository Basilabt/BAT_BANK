using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class clsEPurchaseDTO
    {
        public int purchaseID { get; set; }
        public int onlineAccountID { get; set; }
        public int productID { get; set; }
        public DateTime purchaseDate {  get; set; }
        public bool  status { get; set; }

    }
}
