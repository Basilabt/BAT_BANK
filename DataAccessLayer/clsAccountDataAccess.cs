using DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsAccountDataAccess
    {
        public static bool findAccountByOnlineAccountID(int onlineAccountID , clsAccountDTO accountDTO)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.getConnectionString()))
                {
                    connection.Open();

                    string cmd = "SP_FindAccountByOnlineAccountID";
                    using (SqlCommand command = new SqlCommand(cmd, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(@"OnlineAccountID", onlineAccountID);

                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read()) 
                            {
                                isFound = true;

                                accountDTO.accountID = (int)reader["AccountID"];
                                accountDTO.accountTypeID = (int)reader["AccountTypeID"];
                                accountDTO.clientID = (int)reader["ClientID"];
                                accountDTO.cardID = (int)reader["CardID"];
                                accountDTO.onlineAccountID = onlineAccountID;
                                accountDTO.accountNumber = (string)reader["AccountNumber"];
                                accountDTO.IBAN = (string)reader["IBAN"];
                                accountDTO.creationDate = (DateTime)reader["CreationDate"];
                                accountDTO.endDate = (DateTime)reader["EndDate"];
                                accountDTO.balance = (decimal)reader["Balance"];
                                                                                            
                            }
                        }

                    }


                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }


            return isFound;
        }

        public static bool findAccountByAccountNumber(string accountNumber , clsAccountDTO accountDTO)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.getConnectionString()))
                {
                    connection.Open();

                    string cmd = "SP_FindAccountByAccountNumber";
                    using (SqlCommand command = new SqlCommand(cmd, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(@"AccountNumber", accountNumber);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;

                                accountDTO.accountID = (int)reader["AccountID"];
                                accountDTO.accountTypeID = (int)reader["AccountTypeID"];
                                accountDTO.clientID = (int)reader["ClientID"];
                                accountDTO.cardID = (int)reader["CardID"];
                                accountDTO.onlineAccountID = (int)reader["OnlineAccountID"];                                
                                accountDTO.IBAN = (string)reader["IBAN"];
                                accountDTO.creationDate = (DateTime)reader["CreationDate"];
                                accountDTO.endDate = (DateTime)reader["EndDate"];
                                accountDTO.balance = (decimal)reader["Balance"];

                            }
                        }

                    }


                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }


            return isFound;
        }

        public static bool performDepositAction(int instantiatorAccountID , decimal amount)
        {
            int numberOfAffectedRows = -1;

            try
            {
                using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.getConnectionString()))
                {
                    connection.Open();

                    string cmd = "SP_PerformDepositAction";
                    using(SqlCommand command = new SqlCommand(cmd, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(@"AccountID", instantiatorAccountID);
                        command.Parameters.AddWithValue(@"Amount", amount);

                        numberOfAffectedRows = command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception exception) 
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }
       
            return numberOfAffectedRows >= 1;
        }

        public static bool performWithdrawAction(int instantiatorAccountID , decimal amount)
        {
            int numberOfAffectedRows = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.getConnectionString()))
                {
                    connection.Open();

                    string cmd = "SP_PerformWithdrawAction";
                    using (SqlCommand command = new SqlCommand(cmd, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(@"AccountID", instantiatorAccountID);
                        command.Parameters.AddWithValue(@"Amount", amount);

                        numberOfAffectedRows = command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }

            return numberOfAffectedRows >= 1;
        }

        public static bool performTransferAction(int instantiatorAccountID , int receiverAccountID , decimal amount)
        {
            int numberOfAffectedRows = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.getConnectionString()))
                {
                    connection.Open();

                    string cmd = "SP_PerformTransferAction";
                    using (SqlCommand command = new SqlCommand(cmd, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(@"IstantiatorAccountID", instantiatorAccountID);
                        command.Parameters.AddWithValue(@"ReceiverAccountID", receiverAccountID);                          
                        command.Parameters.AddWithValue(@"Amount", amount);

                        numberOfAffectedRows = command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }

            return numberOfAffectedRows >= 1;
        }

        public static bool doesAccountExistByAccountNumber(string accountNumber) 
        {
            bool isFound = false;

            try
            {
                using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.getConnectionString()))
                {
                    connection.Open();

                    string cmd = "SP_DoesAccountExistByAccountNumber";
                    using (SqlCommand command = new SqlCommand(cmd, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(@"AccountNumber",accountNumber);

                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if(reader.HasRows)
                            {
                                isFound = true;
                            }
                        }

                    }
                }

            } catch (Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }

            return isFound;
        }

        public static decimal getAccountBalanceByAccountID(int accountID)
        {
            decimal balace = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.getConnectionString()))
                {
                    connection.Open();

                    string cmd = "SP_GetAccountBalanceByAccountID";
                    using(SqlCommand command = new SqlCommand(cmd,connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(@"AccountID", accountID);

                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if(reader.Read())
                            {
                                balace = reader.GetDecimal(reader.GetOrdinal("Balance"));
                            }
                        }
                    }

                }


            } catch (Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }

            return balace;
        }

    }

}
