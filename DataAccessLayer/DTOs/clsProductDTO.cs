using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class clsProductDTO
    {
        public int productID { get; set; }
        public string productName { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
    }
}
