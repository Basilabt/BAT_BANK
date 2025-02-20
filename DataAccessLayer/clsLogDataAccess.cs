using DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsLogDataAccess
    {
        public static int addNewLog(clsLogDTO logDTO)
        {
            int newLogID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.getConnectionString()))
                {
                    connection.Open();

                    string cmd = "SP_AddNewLog";
                    using (SqlCommand command = new SqlCommand(cmd, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(@"OnlineAccountID", logDTO.onlineAccountID);
                        command.Parameters.AddWithValue(@"LoginDate", logDTO.loginDate);
                        command.Parameters.AddWithValue(@"Status", logDTO.status);

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int id))
                        {
                            newLogID = id;
                        }

                    }

                }


            }
            catch (Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }


            return newLogID;
        }

        public static List<clsLogDTO> getAllLogs(int onlineAccountID)
        {
            List<clsLogDTO> list = new List<clsLogDTO>();

            try
            {
                using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.getConnectionString()))
                {
                    connection.Open();

                    string cmd = "SP_GetAllLogs";
                    using(SqlCommand command = new SqlCommand(cmd,connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(@"OnlineAccountID",onlineAccountID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new clsLogDTO 
                                        { logID = reader.GetInt32(reader.GetOrdinal("LogID")) ,
                                          onlineAccountID = reader.GetInt32(reader.GetOrdinal("OnlineAccountID")) ,
                                          loginDate = reader.GetDateTime(reader.GetOrdinal("LoginDate")),
                                          status = reader.GetBoolean(reader.GetOrdinal("Status")) ,
                                        }
                                        );
                            }

                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }

            return list;
        }
    }
}
