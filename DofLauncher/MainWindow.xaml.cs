using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DofLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
        private static byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string privateKey = @"MIIEpAIBAAKCAQEAnA5hdnlSGwUNA24oQH+l3wLwha6M9HL4HVKYE3d29Xj9AkRJ
A/GFGJS4rxn2DlE5rWRFfB0aFyUNHakMtzWSWj8szEWxlEp9cceAOhUK+tGaz3BW
bWCIOPENR6XBG8Oo9qd9z7KOOyDGJtxsRWSFuOeCEeTHMsYcUDgqTsycNx5E5qyK
Ztg1TFM1UFNwB7ACtYuBDPUyQAt+3feHlF34Nc+cEMPNEGrFWqBTZHwyYOPdO2OS
pPT+5+ZZiuK7wK+C1waWF9tJ9qIuGcSAFFMF2oVY42VGbhNp8/0Fvt4yNmFWdB4R
Znk0BkAdtbbyt29u4ZCjJq7jRRnJHXux0XDFTQIDAQABAoIBAGvokcI/b+PZIT9+
+3xmB8dmm/SEV1ls6l40T44ebHae+6yGlUqRxivSIsaJmBgcWFqqXFXPNcxNRX19
+JnzBEk9J/f0NS/KNmXnwqXnCRmYuIi6MDkfp/Jf1IP3fMl7CSnNdXSaDjmalwom
HwP413KdOtausINOdCOQQskMOPTu99JqjBkZuVusrpRU4c5daRl65km1IDCqWHNQ
aLl6h7f7FUcvrbfIrgsIIz0jqOVERm2Y7NEgPzOrwsiaZRZW6pG1yX6HbnICCODi
ZYuiCZAryI/mr4wYDZQQ9Lp7hvZxBQjerfP3bDxzMcH8aQcnz8vyn/xmSAW17L9h
u/vg5y0CgYEAy9QqXl7UYKg6XwfXRy4/WO381GGFfq7Luk2qoONb/Jbr+ObURnSn
3dX8qg57av6R/zxlVWJ+KrIE5KX1IABjki+qGY5iqspkM8xfvMekKE5c3CliLLse
5YCimZLMad2uXClPOOxlSkieLGmh0njXRk/KkaDQfHyehvLiOYit/wsCgYEAw//t
MPE+lZJ/9ykp76xqY9x+LL+HH4dFD3Skqk2iEksRroFzn/EoDWiBe6CoCXqXzf+h
JUGfQfYnScRavrh2GYnR9U6kVaifA0vPcB3QnKwP8YMZ/Ox+UU2TH6iI1bIKJt6u
J8Ic579RuWjhcq21EiB+5VjMfF+/YNhLjMI75AcCgYEAkn5UTSsevLFr8mzyPohw
ovu44POOPHRom+fCIIwHysy1oFhWbKTfGUL4q0hpT4bTa3v+4JU/VHRJrAPS30Mo
TSLQwDljlJiN1+SlUkqyIv3fI6TimH+MPypqsrGdFOFstXRDKghM7Eyw0f7BfUG4
hyJF1tCbxzzRuu/Jw8wGMe0CgYBcp250VYb1ZDT0HVSCxanhnUlUVBJHeEXQYZ66
F0sHhM9OBEopkPITLJURYUguevKqYi7Gkvf7UacO+zC+uiqyNfG4Gj4bdEP/ZeYh
JScJ+VjsHcK6Sv4H5zkmnSBajPi5mUkQ6HWLpGi40njJIo7Xi98RAmJgZU7uNDG6
z9NKHwKBgQCnTNYJ4aoVjpI59xf6/xBY/ga7FCh8MlnXwU1T93NG89+cMW+zMMR6
ceOgPsangrwzD5YF8oeeNZqVrx+QBYy+ILNz4zqjJRv78xQaVeL47GZHo1t7iiA0
rm1dLzMzyruMe+wX5tGzHzMGlRBAaIc3B0wxx/BZpqRnFnAKXBeIXw==";
            string tou = "0001FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF";
            string wei = "010101010101010101010101010101010101010101010101010101010101010155914510010403030101";
            int zh = Convert.ToInt16(txtUid.Text);
            string zh_hex = Convert.ToString(zh, 16).ToUpper().PadLeft(10, '0');
            Console.WriteLine(tou + zh_hex + wei);
            byte[] buffer = strToToHexByte(tou + zh_hex + wei);
            string p = Decrypt(buffer, privateKey);
            Console.WriteLine(p);
            Process.Start("dnf.exe", p);
        }
    }
}
