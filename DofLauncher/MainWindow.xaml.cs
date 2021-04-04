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


        public MainWindow()
        {

            InitializeComponent();
            this.DataContext = new MainWindowModel();
        }

        private async void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
#pragma warning disable CS8602 // 解引用可能出现空引用。
                string currentVersion = $"v{Assembly.GetEntryAssembly().GetName().Version}";
#pragma warning restore CS8602 // 解引用可能出现空引用。
                string url = "https://api.github.com/repos/nnn149/DofLauncher/releases/latest";
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "GET";
                req.ContentType = "application/json; charset=utf-8";
                req.Accept = "application/vnd.github.v3+json";
                req.UserAgent = "Nannan";
                var resp = (HttpWebResponse)await req.GetResponseAsync();
                Stream stream = resp.GetResponseStream();
                using StreamReader reader = new(stream, Encoding.UTF8);
                string result = await reader.ReadToEndAsync();
                var release = JsonConvert.DeserializeAnonymousType(result, new { tag_name = "", html_url = "", published_at = "" });
#pragma warning disable CS8602 // 解引用可能出现空引用。
                if (release.tag_name != currentVersion)
#pragma warning restore CS8602 // 解引用可能出现空引用。
                {
                    if (MessageBox.Show("当前版本:" + currentVersion + "\n最新版本:" + release.tag_name + "\n发布日期:" + DateTime.Parse(release.published_at) + "\n是否下载最新版本？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                    {
                        System.Diagnostics.Process.Start("explorer.exe", release.html_url);
                    }
                }
                else
                {
                    MessageBox.Show("当前是最新版本:" + currentVersion, "提示", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void OnSelectedItemChanged(object sender, DependencyPropertyChangedEventArgs e)
         => MainScrollViewer.ScrollToHome();

        private async void BtnAbout_Click(object sender, RoutedEventArgs e)
        {
            var about = new AboutControl();
            var result = await DialogHost.Show(about, "RootDialog", ClosingEventHandler);


        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
            => Debug.WriteLine("About Closed");

    }

}
