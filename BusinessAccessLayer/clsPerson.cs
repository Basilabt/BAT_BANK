using DataAccessLayer;
using DataAccessLayer.DTOs;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;




namespace BusinessAccessLayer
{
    public class clsPerson
    {
        public enum enMode
        {
            AddNew = 1, Update = 2, Delete = 3
        }

        public enum enGender
        {
            Male = 0 , Female = 1
        }

        public int personID { get; set; }
        public string ssn { get; set; }
        public string firstName { get; set; }
        public string secondName { get; set; }
        public string thirdName { get; set; }
        public string lastName { get; set; }

        public string fullname
        {
            get
            {
                return this.firstName + " " + this.secondName + " " + this.thirdName + " " + this.lastName;
            }
        }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public enMode mode { get; set; }
        public enGender gender { get; set; }

        public string genderAsString
        {
            get
            {
                return this.gender.ToString();
            }
        }

        public clsPerson()
        {
            this.personID = -1;
            this.ssn = "";
            this.firstName = "";
            this.secondName = "";
            this.thirdName = "";
            this.lastName = "";
            this.email = "";
            this.phoneNumber = "";
            this.gender = enGender.Male;
            this.mode = enMode.AddNew;
        }

        private clsPerson(int personID, string ssn, string firstName, string secondName, string thirdName, string lastName, string email, string phoneNumber, enGender gender)
        {
            this.personID = personID;
            this.ssn = ssn;
            this.firstName = firstName;
            this.secondName = secondName;
            this.thirdName = thirdName;
            this.lastName = lastName;
            this.email = email;
            this.phoneNumber = phoneNumber;
            this.gender = gender;
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
                        return updatePerson(this.personID, new clsPersonDTO {personID = -1, ssn = this.ssn,firstName=this.firstName,secondName=this.secondName,thirdName=this.thirdName,lastName=this.lastName,email=this.email,phoneNumber=this.phoneNumber,gender=(short)this.gender }); 
                    }

                case enMode.Delete:
                    {
                        return false;
                    }
            }

            return false;
        }

        // Static Methods

        public static clsPerson findPersonByPersonID(int personID)
        {
            clsPersonDTO personDTO = new clsPersonDTO();

            if(clsPersonDataAccess.findPersonByPersonID(personID,personDTO))
            {
                return new clsPerson
                {
                    personID = personDTO.personID,
                    ssn = personDTO.ssn,
                    firstName = personDTO.firstName,
                    secondName = personDTO.secondName,
                    thirdName = personDTO.thirdName,
                    lastName = personDTO.lastName,
                    email = personDTO.email,
                    phoneNumber = personDTO.phoneNumber,
                    gender = (personDTO.gender == 0) ? enGender.Male : enGender.Female
                };
            }

            return null;
        }

        public static clsPerson findPersonByOnlineAccountID(int onlineAccountID)
        {
            clsPersonDTO personDTO = new clsPersonDTO();

            if (clsPersonDataAccess.findPersonByOnlineAccountID(onlineAccountID, personDTO))
            {
                return new clsPerson
                {
                    personID = personDTO.personID,
                    ssn = personDTO.ssn,
                    firstName = personDTO.firstName,
                    secondName = personDTO.secondName,
                    thirdName = personDTO.thirdName,
                    lastName = personDTO.lastName,
                    email = personDTO.email,
                    phoneNumber = personDTO.phoneNumber,
                    gender = (personDTO.gender == 0) ? enGender.Male : enGender.Female
                };
            }

            return null;
        }
        
        public static bool updatePerson(int personID , clsPersonDTO personDTO)
        {
            return clsPersonDataAccess.updatePersonByPersonID(personID, personDTO);
        }

    }
}
