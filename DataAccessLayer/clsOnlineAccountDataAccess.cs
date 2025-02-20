using DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DataAccessLayer
{
    public class clsOnlineAccountDataAccess
    {
        public static bool doesOnlineAccountExistByUsername(string username)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.getConnectionString()))
                {
                    connection.Open();

                    string cmd = "SP_DoesOnlineAccountExistByUsername";
                    using (SqlCommand command = new SqlCommand(cmd,connection)) 
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(@"Username", username);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if(reader.HasRows)
                            {                                
                                isFound = true;
                            }
                        }
                    
                    }


                }
                

            }
            catch (Exception exception) 
            { 
                Console.WriteLine(exception);
            }


            return isFound;
        }

        public static bool login(string username , string password , clsOnlineAccountDTO onlineAccount)
        {
            bool isLoggedInSucceed = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.getConnectionString()))
                {
                    connection.Open();

                    string cmd = "SP_Login";
                    using(SqlCommand command = new SqlCommand(cmd,connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(@"Username", username);
                        command.Parameters.AddWithValue(@"Password", password);

                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if(reader.Read())
                            {
                                isLoggedInSucceed = true;

                                onlineAccount.onlineAccountID = (int)reader["OnlineAccountID"];
                                onlineAccount.username = username;
                                onlineAccount.password = password;
                                onlineAccount.isActive = (bool)reader["IsActive"];
                            }
                        }

                    }

                }

            }
            catch (Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }


            return isLoggedInSucceed;
        }

        public static bool isOnlineAccountActiveByUsername(string username)
        {
            bool isOnlineAccountActive = false;

            try
            {
                using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.getConnectionString()))
                {
                    connection.Open();

                    string cmd = "SP_IsOnlineAccountActiveByUsername";
                    using (SqlCommand command = new SqlCommand(cmd, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(@"Username", username);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if(reader.HasRows)
                            {
                                isOnlineAccountActive = true;
                            }
                        }
                    }
                }


            } catch (Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }



            return isOnlineAccountActive;
        }

        public static int getOnlineAccountIDByUsername(string username)
        {
            int onlineAccountID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.getConnectionString()))
                {
                    connection.Open();

                    string cmd = "SP_GetOnlineAccountIDByUsername";
                    using (SqlCommand command = new SqlCommand(cmd, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(@"Username", username);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                onlineAccountID = (int)reader["OnlineAccountID"];
                            }
                        }
                    }
                }


            }
            catch (Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }



            return onlineAccountID;
        }

        public static bool findOnlineAccountByOnlineAccountID(int onlineAccountID , clsOnlineAccountDTO onlineAccount)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.getConnectionString()))
                {
                    connection.Open();

                    string cmd = "SP_FindOnlineAccountByOnlineAccountID";
                    using(SqlCommand command = new SqlCommand(cmd,connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(@"OnlineAccountID",onlineAccountID);

                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;

                                onlineAccount.username = reader.GetString(reader.GetOrdinal("Username"));
                                onlineAccount.password = reader.GetString(reader.GetOrdinal("Password"));
                                onlineAccount.isActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
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
