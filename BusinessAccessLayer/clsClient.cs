using DataAccessLayer;
using DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    public class clsClient
    {
        public enum enMode
        {
            AddNew = 1 , Update = 2 , Delete = 3
        }

        public int clientID {  get; set; }        
        public int personID { get; set; }
        public enMode mode { get; set; }
        public clsPerson person { get; set; }

        public clsClient() 
        {
            this.clientID = -1;
            this.personID = -1;
            this.mode = enMode.AddNew;
        }

        private clsClient(int clientID , int personID)
        {
            this.clientID = clientID;
            this.personID = personID;
            this.mode = enMode.Update;
            this.person = clsPerson.findPersonByPersonID(personID);
        }

        public void loadCompositeObjects()
        {
            this.person = clsPerson.findPersonByPersonID(this.personID);
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

        public static clsClient findClientByClientID(int clientID)
        {
            clsClientDTO clientDTO = new clsClientDTO();

            if (clsClientDataAccess.findClientByClientID(clientID,clientDTO))
            {
                return new clsClient
                {
                    clientID = clientDTO.clientID ,
                    personID = clientDTO.personID ,
                };

            }

            return null;
        }


    }
}
