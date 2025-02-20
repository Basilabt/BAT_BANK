using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class clsAccountDTO
    {
        public int accountID { get; set; }
        public int accountTypeID { get; set; }
        public int clientID { get; set; }
        public int cardID { get; set; }
        public int onlineAccountID { get; set; }
        public string accountNumber { get; set; }
        public string IBAN { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime endDate { get; set; }
        public decimal balance { get; set; }
      

    }
}
