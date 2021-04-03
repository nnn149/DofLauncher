using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace DofLauncher
{
    public class DofMysql
    {
        private string ConnectionString;
        public DofMysql(string server, string port, string username, string password)
        {

            ConnectionString = "server=" + server + ";port=" + port + ";user=" + username + ";password=" + password + ";Database=";
        }
        public DofMysql(MainWindowModel main)
        {
            ConnectionString = "server=" + main.MysqlIP + ";port=" + main.MysqlPort + ";user=" + main.MysqlAcc + ";password=" + main.MysqlPwd + ";Database=";
        }
        public DofMysql(ConfigUtil config)
        {
            ConnectionString = "server=" + config.MysqlIP + ";port=" + config.MysqlPort + ";user=" + config.MysqlAcc + ";password=" + config.MysqlPwd + ";Database=";
        }
        public string Reg(string usernmae, string pwd, int cera = 0, string qq = "", string ip = "")
        {

            using (MySqlConnection c = new MySqlConnection(ConnectionString + "d_taiwan"))
            {
                c.Open();
                MySqlCommand cmd = new MySqlCommand("select COUNT(*) from accounts where accountname='" + usernmae + "'", c);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    if (Convert.ToInt32(result) == 0)
                    {
                        cmd.CommandText = "select UID from d_taiwan.accounts order by UID desc limit 1";
                        result = cmd.ExecuteScalar();
                        var UID = 1;
                        if (result != null)
                        {
                            UID = Convert.ToInt32(result) + 1;
                        }
                        cmd.CommandText = "insert into accounts (UID,accountname,password,qq,ip) VALUES ('" + UID + "','" + usernmae + "','" + DofUtil.MD5Encrypt(pwd) + "','" + qq + "','" + ip + "')";
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            cmd.CommandText = "insert into limit_create_character (m_id) VALUES ('" + UID + "')";
                            cmd.ExecuteNonQuery();
                            cmd.CommandText = "insert into member_info (m_id,user_id) VALUES ('" + UID + "','" + UID + "')";
                            cmd.ExecuteNonQuery();
                            cmd.CommandText = "insert into member_join_info (m_id) VALUES ('" + UID + "')";
                            cmd.ExecuteNonQuery();
                            cmd.CommandText = "insert into member_miles (m_id) VALUES ('" + UID + "')";
                            cmd.ExecuteNonQuery();
                            cmd.CommandText = "insert into member_white_account (m_id,reg_date) VALUES ('" + UID + "',now())";
                            cmd.ExecuteNonQuery();
                            using (MySqlConnection c2 = new MySqlConnection(ConnectionString + "taiwan_login"))
                            {
                                c2.Open();
                                MySqlCommand cmd2 = new MySqlCommand("insert into member_login (m_id) VALUES ('" + UID + "')", c2);
                                cmd2.ExecuteNonQuery();
                            }
                            using (MySqlConnection c3 = new MySqlConnection(ConnectionString + "taiwan_billing"))
                            {
                                c3.Open();
                                MySqlCommand cmd3 = new MySqlCommand("insert into cash_cera (account,cera,mod_date,reg_date) VALUES ('" + UID + "'," + cera + ",now(),now())", c3);
                                cmd3.ExecuteNonQuery();
                                cmd3.CommandText = "insert into taiwan_billing.cash_cera_point (account,cera_point,reg_date,mod_date) VALUES ('" + UID + "'," + cera + ",now(),now())";
                                cmd3.ExecuteNonQuery();
                            }
                            return "注册成功! UID:" + UID + "  用户名:" + usernmae + "  密码:" + pwd;
                        }
                        else
                        {
                            throw new Exception("注册异常");
                        }
                    }
                    else
                    {
                        throw new Exception("相同用户名");
                    }
                }

            }

            throw new Exception("注册失败");
        }

        public int GetUID(string username, string pwd)
        {
            using (MySqlConnection c = new MySqlConnection(ConnectionString + "d_taiwan"))
            {
                c.Open();
                MySqlCommand cmd = new MySqlCommand("select UID from accounts where accountname='" + username + "' and password='" + DofUtil.MD5Encrypt(pwd) + "'", c);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    throw new Exception("用户名或密码错误");
                }
            }
        }
        public bool ModifyPwd(string username, string oldPwd, string newPwd)
        {
            return false;
        }

        public  bool Test()
        {

            using (MySqlConnection c = new MySqlConnection(ConnectionString + "d_taiwan"))
            {
                MessageBox.Show("1");
                c.Open();
                MessageBox.Show("2");
                return true;
            }
       
        }


    }
}
