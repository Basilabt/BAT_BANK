using DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsEPurchaseDataAccess
    {
        public static int addNewEPurchase(clsEPurchaseDTO purchaseDTO)
        {
            int newPurchaseID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.getConnectionString()))
                {
                    connection.Open();


                    string cmd = "SP_AddNewEvoucherPurchase";
                    using(SqlCommand command = new SqlCommand(cmd, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(@"OnlineAccountID", purchaseDTO.onlineAccountID);
                        command.Parameters.AddWithValue(@"ProductID",purchaseDTO.productID);
                        command.Parameters.AddWithValue(@"PurchaseDate", purchaseDTO.purchaseDate);
                        command.Parameters.AddWithValue(@"Status", purchaseDTO.status);

                        newPurchaseID = command.ExecuteNonQuery();

                    }
                }

            } catch (Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }


            return newPurchaseID;
        }

        public static List<clsEPurchaseHistoryDTO> getAllPurchasesHistory(int onlineAccountID)
        {
            List<clsEPurchaseHistoryDTO> list = new List<clsEPurchaseHistoryDTO>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.getConnectionString()))
                {
                    connection.Open();

                    string cmd = "SP_GetAllPurchasesHistory";
                    using(SqlCommand command=new SqlCommand(cmd, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(@"OnlineAccountID",onlineAccountID);
                            
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                clsEPurchaseHistoryDTO purchaseHistoryDTO = new clsEPurchaseHistoryDTO
                                {
                                    name = reader.GetString(reader.GetOrdinal("Name")),
                                    description = reader.GetString(reader.GetOrdinal("Description")),
                                    price = reader.GetDecimal(reader.GetOrdinal("Price")) ,
                                    purchaseDate = reader.GetDateTime(reader.GetOrdinal("PurchaseDate")) ,
                                    status = reader.GetBoolean(reader.GetOrdinal("Status")) 

                                };

                               list.Add(purchaseHistoryDTO);

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
