using System;
using System.Threading.Tasks;

namespace DofLauncher
{
    //https://mysqlconnector.net/
    public class DofMysql
    {

        public async static Task<string> Reg(string usernmae, string pwd, int cera = 0, string qq = "", string ip = "")
        {


            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                using var cmd = db.Connection.CreateCommand();
                cmd.CommandText = "select COUNT(*) from d_taiwan.accounts where accountname='" + usernmae + "'";
                var result = await cmd.ExecuteScalarAsync();
                if (result != null)
                {
                    if (Convert.ToInt32(result) == 0)
                    {
                        cmd.CommandText = "select UID from d_taiwan.accounts order by UID desc limit 1";
                        result = await cmd.ExecuteScalarAsync();
                        var UID = 1;
                        if (result != null)
                        {
                            UID = Convert.ToInt32(result) + 1;
                        }
                        cmd.CommandText = "insert into d_taiwan.accounts (UID,accountname,password,qq) VALUES ('" + UID + "','" + usernmae + "','" + DofUtil.MD5Encrypt(pwd) + "','" + qq + "')";
                        if (await cmd.ExecuteNonQueryAsync() > 0)
                        {
                            cmd.CommandText = "insert into d_taiwan.limit_create_character (m_id) VALUES ('" + UID + "')";
                            await cmd.ExecuteNonQueryAsync();
                            cmd.CommandText = "insert into d_taiwan.member_info (m_id,user_id) VALUES ('" + UID + "','" + UID + "')";
                            await cmd.ExecuteNonQueryAsync();
                            cmd.CommandText = "insert into d_taiwan.member_join_info (m_id) VALUES ('" + UID + "')";
                            await cmd.ExecuteNonQueryAsync();
                            cmd.CommandText = "insert into d_taiwan.member_miles (m_id) VALUES ('" + UID + "')";
                            await cmd.ExecuteNonQueryAsync();
                            cmd.CommandText = "insert into d_taiwan.member_white_account (m_id,reg_date) VALUES ('" + UID + "',now())";
                            await cmd.ExecuteNonQueryAsync();
                            cmd.CommandText = "insert into taiwan_login.member_login (m_id) VALUES ('" + UID + "')";
                            await cmd.ExecuteNonQueryAsync();
                            cmd.CommandText = "insert into taiwan_billing.cash_cera (account,cera,mod_date,reg_date) VALUES ('" + UID + "'," + cera + ",now(),now())";
                            await cmd.ExecuteNonQueryAsync();
                            cmd.CommandText = "insert into taiwan_billing.cash_cera_point (account,cera_point,reg_date,mod_date) VALUES ('" + UID + "'," + cera + ",now(),now())";
                            await cmd.ExecuteNonQueryAsync();
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

        public async static Task<int> GetUID(string username, string pwd)
        {

            using var db = new AppDb();
            await db.Connection.OpenAsync();
            using var cmd = db.Connection.CreateCommand();
            cmd.CommandText = "select UID from d_taiwan.accounts where accountname='" + username + "' and password='" + DofUtil.MD5Encrypt(pwd) + "'";
            var result = await cmd.ExecuteScalarAsync();
            if (result != null)
            {
                return Convert.ToInt32(result);
            }
            else
            {
                throw new Exception("用户名或密码错误");
            }

        }

        private static bool ModifyPwd(string username, string oldPwd, string newPwd)
        {
            return false;
        }


    }
}
