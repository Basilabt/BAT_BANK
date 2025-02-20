using DataAccessLayer;
using DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    public class clsAccountType
    {
        public enum enMode
        {
            AddNew = 1 , Update = 2 , Delete = 3
        }

        public enum enAccountType
        {
            Commercial = 1
        }

        public int accountTypeID {  get; set; }
        public int accountType { set; get; }
        public string description {  set; get; }
        public enMode mode { get; set; }

        public enAccountType type { set; get; }

        public clsAccountType()
        {
            this.accountTypeID = -1;
            this.accountType = -1;
            this.description = "";
            this.mode = enMode.AddNew;
        }

        private clsAccountType(int accountTypeID , int accountType , string description)
        {
            this.accountTypeID = accountTypeID;
            this.accountType = accountType;
            this.description = description;
            this.mode = enMode.Update;
            this.type = (enAccountType)(accountType);
        }

        public bool save()
        {
            switch(this.mode)
            {
                case enMode.AddNew:
                    {
                        return false;
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

        // Static Methods

        public static clsAccountType findAccountTypeByAccountTypeID(int accountTypeID)
        {
            clsAccountTypeDTO accountTypeDTO = new clsAccountTypeDTO();

            if(clsAccountTypeDataAccess.findAccountTypeByAccountTypeID(accountTypeID,accountTypeDTO))
            {
                return new clsAccountType
                {
                    accountTypeID = accountTypeDTO.accountTypeID , 
                    accountType = accountTypeDTO.accountType , 
                    description = accountTypeDTO.description

                };
            }

            return null;
        }

    }
}
