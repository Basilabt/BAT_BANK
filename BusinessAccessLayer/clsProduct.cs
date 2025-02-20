using DataAccessLayer;
using DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    public class clsProduct
    {
        public enum enMode
        {
            AddNew = 1, Update = 2 , Delete = 3
        }
        public int productID { get; set; }
        public string productName { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }

        public clsProduct()
        {
            this.productID = -1;
            this.productName = "";
            this.description = "";
            this.price = 0;
        }

        private clsProduct(int productID, string productName, string description, decimal price)
        {
            this.productID = productID;
            this.productName = productName;
            this.description = description;
            this.price = price;
        }


        // Static Methods

        public static List<clsProduct> getAllProducts()
        {
            List<clsProduct> list = new List<clsProduct>();

            List<clsProductDTO> productsDTOList = clsProductDataAccess.getAllProducts();

            foreach(var dto in productsDTOList)
            {
                clsProduct product = new clsProduct
                {
                    productID = dto.productID,
                    productName = dto.productName,
                    description = dto.description,
                    price = dto.price
                };

                list.Add(product);
            }

            return list;
        }

        public static clsProduct getProducctByProductID(int productID)
        {
            return getAllProducts().Where(p => p.productID == productID).FirstOrDefault();
        }
    }
}
