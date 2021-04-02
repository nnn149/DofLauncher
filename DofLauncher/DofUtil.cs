
using System;
using System.Security.Cryptography;
using System.Text;

namespace DofLauncher
{
    public static class DofUtil
    {


        public static string GenerateLoginParam(int uid, string privateKey)
        {
            if (uid < 1)
            {
                return"";
            }
            string tou = "0001FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF";
            string wei = "010101010101010101010101010101010101010101010101010101010101010155914510010403030101";
            int zh = Convert.ToInt16(uid);
            string zh_hex = Convert.ToString(zh, 16).ToUpper().PadLeft(10, '0');
            byte[] buffer = StrToToHexByte(tou + zh_hex + wei);
            return Decrypt(buffer, privateKey);
        }
        private static string Decrypt(byte[] payLoad, string privateKey)
        {
            using (var key = OpenSSL.Crypto.CryptoKey.FromPrivateKey(Convert.FromBase64String(privateKey)))
            {
                using (var rsa = key.GetRSA())
                {
                    return Convert.ToBase64String(rsa.PrivateDecrypt(payLoad, OpenSSL.Crypto.RSA.Padding.None));
                }
            }
        }
        public static byte[] StrToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        public static string MD5Encrypt(string str, string key="")
        {
            string signStr = key + str;
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] hashedDataBytes;
            hashedDataBytes = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(signStr));
            StringBuilder tmp = new StringBuilder();
            foreach (byte i in hashedDataBytes)
            {
                tmp.Append(i.ToString("x2"));
            }
            return tmp.ToString();
        }

    }
}
