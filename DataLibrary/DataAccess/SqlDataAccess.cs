using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// added later
using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataLibrary.DataAccess
{
    public static class SqlDataAccess
    {
        public static string GetConnectionString(string connectionName = "BankDB")
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        public static List<T> LoadData<T>(string sql)
        {
            using (IDbConnection con = new SqlConnection(GetConnectionString()))
            {
                return con.Query<T>(sql).ToList();
            }
        }

        //per te pare nese ka kliente me nje id te caktuar
        public static bool CountData(string id)
        {
            using (IDbConnection con = new SqlConnection(GetConnectionString()))
            {
                return con.ExecuteScalar<bool>("select count(1) from Client where ClientId = @id", new { id });
            }
        }

        public static int SaveData<T>(string sql, T data)
        {
            using (IDbConnection con = new SqlConnection(GetConnectionString()))
            {
                return con.Execute(sql, data);
            }
        }
    }
}
