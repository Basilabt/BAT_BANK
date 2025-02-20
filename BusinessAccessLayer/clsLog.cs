using DataAccessLayer;
using DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    public class clsLog
    {
        public enum enMode
        {
            AddNew = 1, Delete = 2, Update = 3
        }

        public int logID { get; set; }
        public int onlineAccountID { get; set; }
        public DateTime loginDate { get; set; }
        public bool status { get; set; }

        public string statusAsString
        {
            get
            {
                return (this.status) ? "Succeed" : "Failed";
            }
        } 

        public enMode mode { get; set; }

        public clsLog()
        {
            this.logID = -1;
            this.onlineAccountID = -1;
            this.loginDate = DateTime.Now;
            this.status = false;
            this.mode = enMode.AddNew;

        }

        private clsLog(int logID, int onlineAccountID, DateTime loginDate, bool isLoggedInSuccessfully)
        {
            this.logID = logID;
            this.onlineAccountID = onlineAccountID;
            this.loginDate = loginDate;
            this.status = isLoggedInSuccessfully;
        }

        public bool save()
        {
            switch (this.mode)
            {
                case enMode.AddNew:
                    {
                        this.logID = addNewLog(new clsLogDTO { logID = -1 , onlineAccountID = this.onlineAccountID , loginDate = this.loginDate , status = this.status});

                        return this.logID != -1;
                    }

                case enMode.Delete:
                    {

                        return false;
                    }

                case enMode.Update:
                    {
                        return false;
                    }

            }

            return false;
        }

        // Static Methods

        public static int addNewLog(clsLogDTO logDTO)
        {
            return clsLogDataAccess.addNewLog(logDTO);
        }

        public static List<clsLogDTO> getAllLogs(int onlineAccountID)
        {
            return clsLogDataAccess.getAllLogs(onlineAccountID);
        }

    }
}
