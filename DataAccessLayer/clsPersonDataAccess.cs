using DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsPersonDataAccess
    {
        public static bool findPersonByPersonID(int personID,clsPersonDTO person)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.getConnectionString()))
                {
                    connection.Open();

                    string cmd = "SP_FindPersonByPersonID";
                    using (SqlCommand command = new SqlCommand(cmd, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(@"PersonID", personID);

                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            if (reader.Read()) 
                            {
                               

                                isFound = true;

                                person.personID = personID;
                                person.ssn = reader.GetString(1);
                                person.firstName = reader.GetString(2);
                                person.secondName = reader.GetString(3);
                                person.thirdName = reader.GetString(4);
                                person.lastName = reader.GetString(5);
                                person.email = reader.GetString(6);
                                person.phoneNumber = reader.GetString(7);

                                bool gender = Convert.ToBoolean(reader["Gender"]);

                                short male = 0;
                                short female = 1; 
                                person.gender = gender ? female :male;
                                    
                                
                            
                            }
                                                                       
                        }

                    }

                }

            } catch(Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }


            return isFound;
        }

        public static bool findPersonByOnlineAccountID(int onlineAccountID , clsPersonDTO person)
        {

            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.getConnectionString()))
                {
                    connection.Open();

                    string cmd = "SP_GetPersonInformationByOnlineAccountID";
                    using (SqlCommand command = new SqlCommand(cmd, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(@"OnlineAccountID", onlineAccountID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                

                                isFound = true;

                                person.personID = reader.GetInt32(reader.GetOrdinal("PersonID"));
                                person.ssn = reader.GetString(1);
                                person.firstName = reader.GetString(2);
                                person.secondName = reader.GetString(3);
                                person.thirdName = reader.GetString(4);
                                person.lastName = reader.GetString(5);
                                person.email = reader.GetString(6);
                                person.phoneNumber = reader.GetString(7);

                                bool gender = Convert.ToBoolean(reader["Gender"]);

                                short male = 0;
                                short female = 1;
                                person.gender = gender ? female : male;



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

        public static bool updatePersonByPersonID(int personID , clsPersonDTO person)
        {
            int numberOfAffectedRows = 0;

            try
            {
                using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.getConnectionString()))
                {
                    connection.Open();

                    string cmd = "SP_UpdatePersonByPersonID";
                    using (SqlCommand command = new SqlCommand(cmd, connection)) 
                    {

                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.AddWithValue(@"PersonID", personID);
                        command.Parameters.AddWithValue(@"SSN", person.ssn);
                        command.Parameters.AddWithValue(@"FirstName", person.firstName);
                        command.Parameters.AddWithValue(@"SecondName", person.secondName);
                        command.Parameters.AddWithValue(@"ThirdName", person.thirdName);
                        command.Parameters.AddWithValue(@"LastName",person.lastName);
                        command.Parameters.AddWithValue(@"Email",person.email);
                        command.Parameters.AddWithValue(@"PhoneNumber",person.phoneNumber);
                        command.Parameters.AddWithValue(@"Gender", person.gender);

                        numberOfAffectedRows = command.ExecuteNonQuery();

                    }
                }

            }catch(Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }


            return numberOfAffectedRows >= 1;
        }



    }
}
