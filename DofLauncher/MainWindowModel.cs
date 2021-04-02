using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DofLauncher
{
   public class MainWindowModel
    {

        public int Uid { get; set; }
        public string Username { get; set; }
        public string UserPwd { get; set; }
        public string MysqlIP { get; set; }
        public string MysqlPort { get; set; }
        public string MysqlAcc { get; set; }
        public string MysqlPwd { get; set; }

        public MainWindowModel()
        {
        }

        public MainWindowModel(int uid, string username, string userPwd, string mysqlIP, string mysqlPort, string mysqlAcc, string mysqlPwd)
        {
            Uid = uid;
            Username = username;
            UserPwd = userPwd;
            MysqlIP = mysqlIP;
            MysqlPort = mysqlPort;
            MysqlAcc = mysqlAcc;
            MysqlPwd = mysqlPwd;
        }
    }
}
