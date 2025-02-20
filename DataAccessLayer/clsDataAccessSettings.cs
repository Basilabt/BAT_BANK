using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.ComponentModel.Design;


namespace DataAccessLayer
{
    public class clsDataAccessSettings
    {
       public static string getConnectionString()
        {
            //var appSettings = ConfigurationManager.AppSettings;

            //string connectionString = appSettings["TestDBConnectionString"];

            //return connectionString;

            return "Server=. ;Database=DB_BATBank;User Id=sa;Password=sa123456;Encrypt=False";
        }
              
        

    }
}
