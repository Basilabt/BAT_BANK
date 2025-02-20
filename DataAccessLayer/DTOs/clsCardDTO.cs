using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class clsCardDTO
    {
        public int cardID {  get; set; }
        public string cardNumber { set; get; }
        public string pin { set; get; }
        public DateTime issueDate {  set; get; }
        public DateTime endDate { set; get; }

    }
}
