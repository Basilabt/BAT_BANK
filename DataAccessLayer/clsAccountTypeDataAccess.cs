using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccessLayer.DTOs;

namespace DataAccessLayer
{
    public class clsAccountTypeDataAccess
    {
        public static bool findAccountTypeByAccountTypeID(int accountTypeID, clsAccountTypeDTO accountType)
        {

            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.getConnectionString()))
                {
                    connection.Open();

                    string cmd = "SP_FindAccountTypeByAccountTypeID";
                    using (SqlCommand command = new SqlCommand(cmd, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(@"AccountTypeID", accountTypeID);


                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;

                                accountType.accountTypeID = accountTypeID;
                                accountType.accountType = (int)reader["AccountType"];
                                accountType.description = (string)reader["Description"];
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
