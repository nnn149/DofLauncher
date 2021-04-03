using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows;


namespace DofLauncher
{
    public partial class MainWindow : Window
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
        MainWindowModel mainWindowModel;
        private string configPath = "config.json";
        public MainWindow()
        {

            InitializeComponent();
            if (File.Exists(configPath))
            {
                try
                {
                    mainWindowModel = JsonConvert.DeserializeObject<MainWindowModel>(File.ReadAllText(configPath));

                }
                catch
                {
                    File.Delete(configPath);
                    mainWindowModel = new MainWindowModel(1, "", "", "127.0.0.1", "3306", "game", "uu5!^%jg");
                }
            }
            else
            {
                mainWindowModel = new MainWindowModel(1, "", "", "127.0.0.1", "3306", "game", "uu5!^%jg");
            }
            this.DataContext = mainWindowModel;
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var UID = new DofMysql(mainWindowModel).GetUID(mainWindowModel.Username, mainWindowModel.UserPwd);
                Process.Start("dnf.exe", DofUtil.GenerateLoginParam(UID, privateKey));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            File.WriteAllText(configPath, JsonConvert.SerializeObject(mainWindowModel));
        }

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (mainWindowModel.Username.Length < 5 || mainWindowModel.Username.Length > 16 || mainWindowModel.UserPwd.Length < 5 || mainWindowModel.UserPwd.Length > 16)
                {
                    MessageBox.Show("用户名和密码长度在5-16", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var result = new DofMysql(mainWindowModel).Reg(mainWindowModel.Username, mainWindowModel.UserPwd);
                MessageBox.Show(result, "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void BtnUIDLogin_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("dnf.exe", DofUtil.GenerateLoginParam(mainWindowModel.Uid, privateKey));
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string currentVersion = "v" + Assembly.GetEntryAssembly().GetName().Version.ToString();
                string url = "https://api.github.com/repos/nnn149/DofLauncher/releases/latest";
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "GET";
                req.ContentType = "application/json; charset=utf-8";
                req.Accept = "application/vnd.github.v3+json";
                req.UserAgent = "Nannan";
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Stream stream = resp.GetResponseStream();
                string result = "a";
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    result = reader.ReadToEnd();
                }
                var release = JsonConvert.DeserializeAnonymousType(result, new { tag_name = "", html_url = "", published_at = "" });
                if (release.tag_name == currentVersion)
                {
                    MessageBox.Show("当前是最新版本:" + currentVersion, "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    if (MessageBox.Show("当前版本:" + currentVersion + "\n最新版本:" + release.tag_name + "\n发布日期:" + DateTime.Parse(release.published_at) + "\n是否下载最新版本？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                    {
                        System.Diagnostics.Process.Start(release.html_url);

                    }

                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error); }

        }


    }

}
