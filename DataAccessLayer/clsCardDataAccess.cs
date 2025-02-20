using DataAccessLayer.DTOs;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsCardDataAccess
    {
        public static bool findCardByCardID(int cardID, clsCardDTO card)
        {
            bool isFound = false;

            try
            {
                using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.getConnectionString()))
                {
                    connection.Open();

                    string cmd = "SP_FindCardByCardID";
                    using (SqlCommand command = new SqlCommand(cmd, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(@"CardID", cardID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                isFound = true;

                                card.cardID = cardID;
                                card.cardNumber = (string)reader["CardNumber"];
                                card.pin = (string)reader["PIN"];
                                card.issueDate = (DateTime)reader["IssueDate"];
                                card.endDate = (DateTime)reader["EndDate"];

                            }

                        }
                    }
                }

            } catch(Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }

            return isFound ;
        }

        public static bool updateCard(int cardID , clsCardDTO card)
        {
            int numberOfAffectedRows = 0;

            try
            {
                using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.getConnectionString()))
                {
                    connection.Open();

                    string cmd = "SP_UpdateCard";
                    using(SqlCommand command = new SqlCommand(cmd,connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(@"CardID",cardID);
                        command.Parameters.AddWithValue(@"CardNumber", card.cardNumber);
                        command.Parameters.AddWithValue(@"PIN", card.pin);
                        command.Parameters.AddWithValue(@"IssueDate", card.issueDate);
                        command.Parameters.AddWithValue(@"EndDate", card.endDate);

                        numberOfAffectedRows = command.ExecuteNonQuery();



                    }

                }



            } catch(Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }

            return numberOfAffectedRows >= 1;
        }

        public static bool getCardByOnlineAccountID(int onlineAccountID, clsCardDTO card)
        {
            bool isFound = false;

            try
            {

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.getConnectionString()))
                {
                    connection.Open();

                    string cmd = "SP_GetCreditInformationByOnlineAccountID";
                    using (SqlCommand command = new SqlCommand(cmd, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(@"OnlineAccountID", onlineAccountID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                isFound = true;

                                card.cardID = reader.GetInt32(reader.GetOrdinal("CardID"));
                                card.cardNumber = reader.GetString(reader.GetOrdinal("CardNumber"));
                                card.pin = reader.GetString(reader.GetOrdinal("PIN"));
                                card.issueDate = reader.GetDateTime(reader.GetOrdinal("IssueDate"));
                                card.endDate = reader.GetDateTime(reader.GetOrdinal("EndDate"));

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

    }
}
