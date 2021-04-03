using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DofLauncher
{
    public class AppDb : IDisposable
    {
        public readonly MySqlConnection Connection;
        private static string ConnectionString;

        public AppDb()
        {
            if (String.IsNullOrEmpty(ConnectionString))
            {
                var config = ConfigUtil.GetInstance();
                ConnectionString = "server=" + config.MysqlIP + ";port=" + config.MysqlPort + ";User ID=" + config.MysqlAcc + ";password=" + config.MysqlPwd + ";Database=";
            }
            Connection = new MySqlConnection(ConnectionString);
        }

        public void Dispose()
        {
            Connection.Close();
        }
        public  async static Task<bool> Test(string server, string port, string username, string password)
        {
            try
            {

                using (MySqlConnection c = new MySqlConnection("server=" + server + ";port=" + port + ";User ID=" + username + ";password=" + password))
                {
                    await c.OpenAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
