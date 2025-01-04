using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpEgitimKampi501.Connection
{
    public class DatabaseConnectionString
    {
        public static void SQLDatabaseConnectionString()
        {
           SqlConnection connection = new SqlConnection("Server =DESKTOP-7CKJMG6;initial Catalog = EgitimKampi501Db;integrated security = true");
        }
        
    }
}
