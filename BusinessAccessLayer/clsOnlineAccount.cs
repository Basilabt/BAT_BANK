using DataAccessLayer;
using DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    public class clsOnlineAccount
    {
        public enum enMode
        {
            AddNew = 1 , Update = 2 , Delete = 3
        }

        public int onlineAccountID {  get; set; }   
        public string username { get; set; }
        public string password {  get; set; }
        public bool isActive { get; set; }
        public enMode mode { get; set; } 

        
        public clsOnlineAccount()
        {
            this.onlineAccountID = -1;
            this.username = "";
            this.password = "";
            this.isActive = false;
            this.mode = enMode.AddNew;
        }
        private clsOnlineAccount(int onlineAccountID , string username , string password , bool isActive)
        {
            this.onlineAccountID = onlineAccountID;
            this.username = username;
            this.password = password;
            this.isActive = isActive;
            this.mode = enMode.Update;
        }


        public bool save()
        {
            switch (this.mode)
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
        
        public static clsOnlineAccount login(string username , string password)
        {   
            clsOnlineAccountDTO onlineAccountDTO = new clsOnlineAccountDTO();

            if(clsOnlineAccountDataAccess.login(username,password,onlineAccountDTO))
            {
                return new clsOnlineAccount 
                                            { 
                                                onlineAccountID = onlineAccountDTO.onlineAccountID,
                                                username = onlineAccountDTO.username,
                                                password = onlineAccountDTO.password,
                                                isActive = onlineAccountDTO.isActive,
                    
                                            };
            }

            return null;
        }

        public static clsOnlineAccount findOnlineAccountByOnlineAccountID(int onlineAccountID)
        {
            clsOnlineAccountDTO onlineAccountDTO = new clsOnlineAccountDTO();

            if (clsOnlineAccountDataAccess.findOnlineAccountByOnlineAccountID(onlineAccountID, onlineAccountDTO))
            {
                return new clsOnlineAccount { onlineAccountID = onlineAccountID, username = onlineAccountDTO.username, password = onlineAccountDTO.password, isActive = onlineAccountDTO.isActive };
            }

            return null;
        }

        public static bool doesOnlineAccountExistByUsername(string username)
        {
            return clsOnlineAccountDataAccess.doesOnlineAccountExistByUsername(username);
        }

        public static bool isOnlineAccountActiveByUsername(string username)
        {
            return clsOnlineAccountDataAccess.isOnlineAccountActiveByUsername(username);
        }

        public static int getOnlineAccountIDByUsername(string username)
        {
            return clsOnlineAccountDataAccess.getOnlineAccountIDByUsername(username);
        }

    }
}
