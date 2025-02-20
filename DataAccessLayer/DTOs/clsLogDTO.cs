using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class clsLogDTO
    {
        public int logID {  get; set; }
        public int onlineAccountID {  get; set; }
        public DateTime loginDate { get; set; }
        public bool status { get; set; }

        public string statusAsString
        {
            get
            {
                return (this.status) ? "Succeed" : "Failed";
            }
        }

    }
}
