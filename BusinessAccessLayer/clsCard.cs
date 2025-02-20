using DataAccessLayer;
using DataAccessLayer.DTOs;
using Microsoft.Identity.Client;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    public class clsCard
    {
        public enum enMode
        {
            AddNew = 1 , Update = 2, Delete = 3
        }

        public int cardID {  get; set; }
        public string cardNumber { set; get; }
        public string pin { set; get; }
        public DateTime issueDate { set; get; }
        public DateTime endDate { set; get; }
        public enMode mode { set; get; }

        public clsCard()
        {
            this.cardID = -1;
            this.cardNumber = "";
            this.pin = "";
            this.issueDate = DateTime.MinValue;
            this.endDate = DateTime.MinValue;
            this.mode = enMode.AddNew;
        }

        private clsCard(int cardID , string cardNumber , string pin , DateTime issueDate , DateTime endDate)
        {
            this.cardID = cardID;
            this.cardNumber = cardNumber;
            this.pin = pin;
            this.issueDate = issueDate;
            this.endDate = endDate;
            this.mode = enMode.Update;
        }

        bool save()
        {
            switch (this.mode)
            {
                case enMode.AddNew:
                    {
                        return false;
                    }

                case enMode.Update:
                    {
                        return updateCard(this.cardID,new clsCardDTO { cardID = this.cardID , cardNumber = this.cardNumber , pin = this.pin , issueDate = this.issueDate , endDate = this.endDate });
                    }

                case enMode.Delete:
                    {
                        return false;
                    }
            }

                  return false;
        }


        public bool renewCard()
        {
            if(!this.isCreditCardRenewRequirementsSatisfied())
            {
                return false;
            }

            this.issueDate = DateTime.Now;
            this.endDate = DateTime.Now.AddYears(5);
            this.mode = enMode.Update;

            return this.save();
        }

        public bool isCreditCardRenewRequirementsSatisfied()
        {
            const int maxDaysToRenewBefore = 30;

            int diff = Math.Abs((this.endDate.Date - this.issueDate.Date).Days);

            Console.WriteLine($"DEBUG: Difference = {diff}");

            return diff <= maxDaysToRenewBefore;

        }

        // Static Methods

        public static clsCard findCardByCardID(int cardID)
        {
            clsCardDTO cardDTO = new clsCardDTO();

            if(clsCardDataAccess.findCardByCardID(cardID,cardDTO))
            {
                return new clsCard
                {
                    cardID = cardDTO.cardID ,
                    cardNumber = cardDTO.cardNumber ,
                    pin = cardDTO.pin ,
                    issueDate = cardDTO.issueDate ,
                    endDate = cardDTO.endDate 
                };
            }

            return null;
        }


        public static bool updateCard(int cardID,clsCardDTO cardDTO)
        {
            return clsCardDataAccess.updateCard(cardID,cardDTO);
        }

        public static clsCard getCardByOnlineAccountID(int onlineAccountID) 
        {
            clsCardDTO cardDTO = new clsCardDTO();

            if(clsCardDataAccess.getCardByOnlineAccountID(onlineAccountID,cardDTO))
            {
                return new clsCard(cardDTO.cardID,cardDTO.cardNumber,cardDTO.pin,cardDTO.issueDate,cardDTO.endDate);
            }

            return null;
        }

    }
}
