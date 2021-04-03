using MaterialDesignDemo.Domain;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace DofLauncher
{
    public partial class MainWindow : Window
    {
        
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

       

        private async void BtnUpdate_Click(object sender, RoutedEventArgs e)
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
                var resp = (HttpWebResponse)await req.GetResponseAsync();
                Stream stream = resp.GetResponseStream();
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    string result =await reader.ReadToEndAsync();
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
              
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error); }

        }
        private void MenuToggleButton_OnClick(object sender, RoutedEventArgs e)
               => DemoItemsSearchBox.Focus();

        private void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //until we had a StaysOpen glag to Drawer, this will help with scroll bars
            var dependencyObject = Mouse.Captured as DependencyObject;

            while (dependencyObject != null)
            {
                if (dependencyObject is ScrollBar) return;
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }

            MenuToggleButton.IsChecked = false;
        }
        private  void MenuPopupButton_OnClick(object sender, RoutedEventArgs e)
        {

        }
        private void OnSelectedItemChanged(object sender, DependencyPropertyChangedEventArgs e)
         => MainScrollViewer.ScrollToHome();

        private async void BtnAbout_Click(object sender, RoutedEventArgs e)
        {
            var about = new AboutControl();
            var result = await DialogHost.Show(about, "RootDialog", ClosingEventHandler);

           
        }

  

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
            => Debug.WriteLine("About Closed");

        private void BtnUpdate_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }

}
