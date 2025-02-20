using DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsActionDataAccess
    {
        public static int addNewAction(clsActionDTO action)
        {
            int newActionID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.getConnectionString()))
                {
                    connection.Open();

                    string cmd = "SP_AddNewAction";
                    using (SqlCommand command = new SqlCommand(cmd, connection))
                    {   
                       

                        command.CommandType = System.Data.CommandType.StoredProcedure;                  
                        command.Parameters.AddWithValue(@"ActionTypeID", action.actionTypeID);
                        command.Parameters.AddWithValue(@"InstantiatorAccountID", action.istantiatorAccountID);
                        command.Parameters.AddWithValue(@"ReceiverAccountID", action.receiverAccountID);                       
                        command.Parameters.AddWithValue(@"Amount", action.amount);

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString() ,out int id))
                        {
                            newActionID = id;
                        }

                    }
                }

            }
            catch (Exception exception) 
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }

            return newActionID;
        }

        public static List<clsActionHistoryDTO> getAccountActionsHistory(int accountID)
        {
            List<clsActionHistoryDTO> list = new List<clsActionHistoryDTO>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.getConnectionString()))
                {
                    connection.Open();

                    string cmd = "SP_GetAccountMoneyActionsHistory";
                    using(SqlCommand command = new SqlCommand(cmd,connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue($"AccountID", accountID);

                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                clsActionHistoryDTO actionHistory = new clsActionHistoryDTO
                                {
                                    actionType = reader.GetString(reader.GetOrdinal("Type")),
                                    receivedAccountNumber = reader.GetString(reader.GetOrdinal("AccountNumber")),
                                    amount = reader.GetDecimal(reader.GetOrdinal("Amount"))

                                };

                                list.Add(actionHistory);
                            }
                        }

                    }
                }

            } catch (Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }



            return list;
        }

    }
}
