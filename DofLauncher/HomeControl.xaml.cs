using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DofLauncher
{
    /// <summary>
    /// HomeControl.xaml 的交互逻辑
    /// </summary>
    public partial class HomeControl : UserControl
    {
        readonly string privateKey = @"MIIEpAIBAAKCAQEAnA5hdnlSGwUNA24oQH+l3wLwha6M9HL4HVKYE3d29Xj9AkRJ
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
        public HomeControl()
        {
            InitializeComponent();

            var c = ConfigUtil.GetInstance();
            TxtUser.Text = c.Username;
            Pwd.Password = c.UserPwd;
            TxtUID.Text = c.Uid.ToString();
        }

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                var UID = await DofMysql.GetUID(TxtUser.Text, Pwd.Password);
                await Login(UID, (Button)sender, "用户名密码登录");
                var c = ConfigUtil.GetInstance();
                c.Username = TxtUser.Text;
                c.UserPwd = Pwd.Password;
                c.Save();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void BtnUIDLogin_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                await Login(int.Parse(TxtUID.Text), (Button)sender, "UID登录");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async Task Login(int UID, Button btn, string Content)
        {
            btn.IsEnabled = false;
            MaterialDesignThemes.Wpf.ButtonProgressAssist.SetIsIndeterminate(btn, true);
            MaterialDesignThemes.Wpf.ButtonProgressAssist.SetIsIndicatorVisible(btn, true);
            btn.Content = "正在启动...";

            await Task.Run(async () =>
            {
                var c = ConfigUtil.GetInstance();
                c.Uid = UID;
                c.Save();
                Process.Start("dnf.exe", DofUtil.GenerateLoginParam(UID, privateKey));
                //检测30秒内
                for (int i = 0; i < 120; i++)
                {
                    Debug.WriteLine(i);
                    await Task.Delay(500);
                    Process[] processList = Process.GetProcessesByName("DNF");
                    if (processList.Length > 0)
                    {
                        break;
                    }
                }
                await Task.Delay(2000);

            });
            btn.IsEnabled = true;
            btn.Content = Content;
            MaterialDesignThemes.Wpf.ButtonProgressAssist.SetIsIndeterminate(btn, false);
            MaterialDesignThemes.Wpf.ButtonProgressAssist.SetIsIndicatorVisible(btn, false);
        }
    }
}
