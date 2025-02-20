using DataAccessLayer;
using DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    public class clsAction
    {
        public enum enMode
        {
            AddNew = 1 , Delete = 2 , Update =3
        }
        public int actionID { get; set; }
        public int actionTypeID { get; set; }        
        public int istantiatorAccountID { get; set; } 
        public int receiverAccountID { get; set; }
        public decimal amount { get; set; }

        public enMode mode {  get; set; }

        public clsAction()
        {
            this.actionID = -1;
            this.actionTypeID = -1;
            this.istantiatorAccountID = -1;
            this.receiverAccountID = -1;
            this.amount = -1;
            this.mode = enMode.AddNew;
        }

        private clsAction(int actionID , int actionTypeID, int istantiatorAccountID , int receiverAccountID , decimal amount)
        {
            this.actionID = actionID;
            this.actionTypeID = actionTypeID;
            this.istantiatorAccountID = istantiatorAccountID;
            this.receiverAccountID = receiverAccountID;
            this.amount = amount;
            this.mode = enMode.Update;
        }

        public bool save()
        {
            switch (this.mode)
            {
                case enMode.AddNew:
                    {
                        this.actionID = addNewAction(new clsActionDTO { actionID = this.actionID, actionTypeID = this.actionTypeID, istantiatorAccountID = this.istantiatorAccountID, receiverAccountID = this.receiverAccountID, amount = this.amount });                     
                        return this.actionID != -1;
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

        // Static methods

        public static int addNewAction(clsActionDTO actionDTO)
        {
            return clsActionDataAccess.addNewAction(actionDTO);
        }

        public static List<clsActionHistoryDTO> getAccountActionsHistory(int accountID)
        {
            return clsActionDataAccess.getAccountActionsHistory(accountID);
        }

    }
}
