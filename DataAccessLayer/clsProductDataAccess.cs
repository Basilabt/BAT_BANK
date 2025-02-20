using DataAccessLayer.DTOs;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsProductDataAccess
    {
        public static List<clsProductDTO> getAllProducts()
        {
            List<clsProductDTO> list = new List<clsProductDTO> ();

            try
            {
                using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.getConnectionString()))
                {
                    connection.Open ();

                    string cmd = "SP_GetAllProducts";
                    using(SqlCommand command  = new SqlCommand(cmd, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read ())
                            {

                                clsProductDTO productDTO = new clsProductDTO
                                {
                                    productID = reader.GetInt32(reader.GetOrdinal("ProductID")),
                                    productName = reader.GetString(reader.GetOrdinal("Name")),
                                    description = reader.GetString(reader.GetOrdinal("Description")),
                                    price = reader.GetDecimal(reader.GetOrdinal("Price"))

                                };

                                list.Add(productDTO);   

                            }

                        }

                    }

                }

            } catch(Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }




            return list;
        }





    }
}
