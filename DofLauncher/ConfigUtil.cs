using Newtonsoft.Json;
using System.IO;

namespace DofLauncher
{
    public class ConfigUtil
    {

        public int? Uid { get; set; }
        public string? Username { get; set; }
        public string? UserPwd { get; set; }
        public string? MysqlIP { get; set; }
        public string? MysqlPort { get; set; }
        public string? MysqlAcc { get; set; }
        public string? MysqlPwd { get; set; }
        private static readonly string configPath = "config.json";


        private static ConfigUtil? _ConfigUtil;

        private ConfigUtil()
        {

        }

        public ConfigUtil(int uid, string username, string userPwd, string mysqlIP, string mysqlPort, string mysqlAcc, string mysqlPwd)
        {
            Uid = uid;
            Username = username;
            UserPwd = userPwd;
            MysqlIP = mysqlIP;
            MysqlPort = mysqlPort;
            MysqlAcc = mysqlAcc;
            MysqlPwd = mysqlPwd;
        }

        public static ConfigUtil GetInstance()
        {

            if (_ConfigUtil == null)
            {

                if (File.Exists(configPath))
                {
                    try
                    {
                        var c = JsonConvert.DeserializeAnonymousType(File.ReadAllText(configPath), new { Uid = 0, Username = "", UserPwd = "", MysqlIP = "", MysqlPort = "", MysqlAcc = "", MysqlPwd = "" });
#pragma warning disable CS8602 // 解引用可能出现空引用。
                        _ConfigUtil = new ConfigUtil(c.Uid, c.Username, c.UserPwd, c.MysqlIP, c.MysqlPort, c.MysqlAcc, c.MysqlPwd);
#pragma warning restore CS8602 // 解引用可能出现空引用。


                    }
                    catch
                    {
                        File.Delete(configPath);
                        _ConfigUtil = new ConfigUtil(1, "", "", "127.0.0.1", "3306", "game", "uu5!^%jg");
                    }
                }
                else
                {
                    _ConfigUtil = new ConfigUtil(1, "", "", "127.0.0.1", "3306", "game", "uu5!^%jg");
                }

            }
            return _ConfigUtil;
        }

        public void Save()
        {
            File.WriteAllText(configPath, JsonConvert.SerializeObject(this));
        }
    }
}
