using DataLibrary.DataAccess;
using DataLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public class ClientProcessor
    {
        public static int CreateClient(string clientId, string firstname, string lastname,
            string emailAddress, DateTime? date)
        {
            ClientModel data = new ClientModel
            {
                ClientId = clientId,
                FirstName = firstname,
                LastName = lastname,
                EmailAddress = emailAddress,
                Birthday = date
            };

            string sql = @"INSERT INTO dbo.Client (ClientId, FirstName, LastName, EmailAddress, Birthday)
                           VALUES (@ClientId, @FirstName, @LastName, @EmailAddress, @Birthday)";
            
            return SqlDataAccess.SaveData(sql, data);
        }

        public static List<ClientModel> LoadClients()
        {
            string sql = @"SELECT * FROM dbo.Client";

            return SqlDataAccess.LoadData<ClientModel>(sql);
        }

      
    }
}
