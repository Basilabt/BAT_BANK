using DataAccessLayer;
using DataAccessLayer.DTOs;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    public class clsAccount
    {
        public enum enMode
        {
            AddNew = 1 , Update = 2 , Delete = 3
        }

        public int accountID {  get; set; }
        public int accountTypeID {  get; set; }
        public int clientID {  get; set; }
        public int cardID { get; set; }
        public int onlineAccountID {  get; set; }
        public string accountNumber { get; set; }
        public string IBAN {  get; set; }
        public DateTime creationDate { get; set; }
        public DateTime endDate { get; set; }
        public decimal balance {  get; set; }
        public enMode mode {  get; set; }

        public string accountTypeAsString
        {
            get
            {
                return accountNumber.ToString();
            }
        }
        
        public clsClient client {get; set; }
        public clsAccountType accountType { get; set; }
        public clsCard card { get; set; }
        public clsOnlineAccount onlineAccount { get; set; }

       

        public clsAccount()
        {
            this.accountID = -1;
            this.accountTypeID = -1;
            this.clientID = -1;
            this.cardID = -1;
            this.onlineAccountID = -1;
            this.accountNumber = "";
            this.IBAN = "";
            this.creationDate = DateTime.MinValue;
            this.endDate = DateTime.MinValue;
            this.balance = -1;
            this.mode = enMode.AddNew;
           
        }

        private clsAccount(int accountID, int accountTypeID, int clientID, int cardID, int onlineAccountID, string accountNumber, string IBAN, DateTime creationDate, DateTime endDate, decimal balance)
        {
            this.accountID = accountID;
            this.accountTypeID = accountTypeID;
            this.clientID = clientID;
            this.cardID = cardID;
            this.onlineAccountID = onlineAccountID;
            this.accountNumber = accountNumber;
            this.IBAN = IBAN;
            this.creationDate = creationDate;
            this.endDate = endDate;
            this.balance = balance;
            this.mode = enMode.Update;
            this.client = clsClient.findClientByClientID(clientID);
            this.accountType = clsAccountType.findAccountTypeByAccountTypeID(accountTypeID);
            this.card = clsCard.findCardByCardID(cardID);
            this.onlineAccount = clsOnlineAccount.findOnlineAccountByOnlineAccountID(onlineAccountID);
        }

        public override string ToString()
        {
            return $"Account Number: {this.accountNumber}, IBAN: {this.IBAN} , ClientID: {this.clientID} , CardID: {this.cardID} , AccountTypeID = {this.accountTypeID}";
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

        public void loadCompositedObjects()
        {
            this.client = clsClient.findClientByClientID(this.clientID);
            this.accountType = clsAccountType.findAccountTypeByAccountTypeID(this.accountTypeID);
            this.card = clsCard.findCardByCardID(this.cardID);

            this.client.loadCompositeObjects();
        }

        public bool refresh()
        {

            clsAccount updatedAccountInstance = findAccountByAccountNumber(this.accountNumber);
          
            if (updatedAccountInstance != null)
            {
                
                this.accountNumber = updatedAccountInstance.accountNumber;
                this.IBAN = updatedAccountInstance.IBAN;
                this.balance = updatedAccountInstance.balance;
                this.endDate = updatedAccountInstance.endDate;
                this.creationDate = updatedAccountInstance.creationDate;
                this.clientID = updatedAccountInstance.clientID;
                this.accountTypeID = updatedAccountInstance.accountTypeID;
                this.cardID = updatedAccountInstance.cardID;
                this.onlineAccountID = updatedAccountInstance.onlineAccountID;

               
                this.client = clsClient.findClientByClientID(this.clientID);
                this.accountType = clsAccountType.findAccountTypeByAccountTypeID(this.accountTypeID);
                this.card = clsCard.findCardByCardID(this.cardID);
                this.onlineAccount = clsOnlineAccount.findOnlineAccountByOnlineAccountID(this.onlineAccountID);


                return true; 
            }

            return false; 

        }


        // Static Methods

        public static clsAccount findAccountByOnlineAccountID(int onlineAccountID)
        {
            clsAccountDTO accountDTO = new clsAccountDTO();

            if(clsAccountDataAccess.findAccountByOnlineAccountID(onlineAccountID,accountDTO))
            {
                return new clsAccount{ 
                                          accountID = accountDTO.accountID ,
                                          accountTypeID = accountDTO.accountTypeID ,
                                          clientID = accountDTO.clientID ,
                                          cardID = accountDTO.cardID ,
                                          onlineAccountID = accountDTO.onlineAccountID ,
                                          accountNumber = accountDTO.accountNumber ,
                                          IBAN = accountDTO.IBAN ,
                                          creationDate = accountDTO.creationDate ,
                                          endDate = accountDTO.endDate ,
                                          balance = accountDTO.balance 
                                
                                     };
            }

            return null;
        }

        public static clsAccount findAccountByAccountNumber(string accountNumber)
        {

            clsAccountDTO accountDTO = new clsAccountDTO();

            if (clsAccountDataAccess.findAccountByAccountNumber(accountNumber,accountDTO))
            {
                return new clsAccount
                {
                    accountID = accountDTO.accountID,
                    accountTypeID = accountDTO.accountTypeID,
                    clientID = accountDTO.clientID,
                    cardID = accountDTO.cardID,
                    onlineAccountID = accountDTO.onlineAccountID,
                    accountNumber = accountDTO.accountNumber,
                    IBAN = accountDTO.IBAN,
                    creationDate = accountDTO.creationDate,
                    endDate = accountDTO.endDate,
                    balance = accountDTO.balance

                };
            }

            return null;
        }


        public static bool doesAccountExistByAccountNumber(string accountNumber)
        {
            return clsAccountDataAccess.doesAccountExistByAccountNumber(accountNumber);
        }

        public static bool performDepositAction(int instantiatorAccountID , decimal amount)
        {
            return clsAccountDataAccess.performDepositAction(instantiatorAccountID,amount);
        }

        public static bool performWithdrawAction(int instantiatorAccountID , decimal amount)
        {
            return clsAccountDataAccess.performWithdrawAction(instantiatorAccountID,amount);
        }

        public static bool performTransferAction(int instantiatorAccountID , int receiverAccountID , decimal amount)
        {
            return clsAccountDataAccess.performTransferAction(instantiatorAccountID,receiverAccountID,amount);
        }

        public static decimal getAccountBalanceByAccountID(int accountID)
        {
            return clsAccountDataAccess.getAccountBalanceByAccountID(accountID);
        }

    }
}
