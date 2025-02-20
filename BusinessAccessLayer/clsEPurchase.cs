using DataAccessLayer;
using DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    public class clsEPurchase
    {
        public enum enMode
        {
            AddNew = 1 , Update = 2 , Delete = 3 
        }

        public int purchaseID { get; set; }
        public int onlineAccountID { get; set; }
        public int productID { get; set; }
        public DateTime purchaseDate { get; set; }
        public bool status { get; set; }

        public enMode mode { get; set; }

        public clsEPurchase()
        {
            this.purchaseID = -1;
            this.onlineAccountID = -1;
            this.productID = -1;
            this.purchaseDate = DateTime.MinValue;
            this.status = false;
            this.mode = enMode.AddNew;
        }

        private clsEPurchase(int purchaseID, int onlineAccountID, int productID, DateTime purchaseDate, bool status)
        {
            this.purchaseID = purchaseID;
            this.onlineAccountID = onlineAccountID;
            this.productID = productID;
            this.purchaseDate = purchaseDate;
            this.status = status;
            this.mode = enMode.Update;
        }


        public bool save()
        {

            switch(this.mode)
            {
                case enMode.AddNew:
                    {
                        this.purchaseID = addNewEPurchase(new clsEPurchaseDTO {purchaseID=-1,onlineAccountID=this.onlineAccountID,productID=this.productID,purchaseDate=this.purchaseDate,status=this.status});
                        return this.productID != -1;
                    }

                case enMode.Update:
                    {
                        return false;
                    }

                case enMode.Delete:
                    {
                        return false;
                    }


            }

            return false;
        }

        // Static Methods.

        public static int addNewEPurchase(clsEPurchaseDTO purchaseDTO)
        {
            return clsEPurchaseDataAccess.addNewEPurchase(purchaseDTO);
        }

        public static List<clsEPurchaseHistoryDTO> getAllPurchasesHistory(int onlineAccountID)
        {
            return clsEPurchaseDataAccess.getAllPurchasesHistory(onlineAccountID);
        }
    }
}
