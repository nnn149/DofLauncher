using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;

namespace DofLauncher
{
    /// <summary>
    /// AboutControl.xaml 的交互逻辑
    /// </summary>
    public partial class AboutControl : UserControl
    {
        public AboutControl()
        {
            InitializeComponent();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);

        }

        private void BtnDonate_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "https://github.com/nnn149/DofLauncher");
        }
    }
}
