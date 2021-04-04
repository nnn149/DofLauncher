using System;
using System.Windows;
using System.Windows.Controls;

namespace DofLauncher
{
    /// <summary>
    /// ConfigControl.xaml 的交互逻辑
    /// </summary>
    public partial class ConfigControl : UserControl
    {
        public ConfigControl()
        {
            InitializeComponent();

            var c = ConfigUtil.GetInstance();
            TxtAcc.Text = c.MysqlAcc;
            TxtIP.Text = c.MysqlIP;
            TxtPort.Text = c.MysqlPort;
            Pwd.Password = c.MysqlPwd;

        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var c = ConfigUtil.GetInstance();
                c.MysqlAcc = TxtAcc.Text;
                c.MysqlIP = TxtIP.Text;
                c.MysqlPort = TxtPort.Text;
                c.MysqlPwd = Pwd.Password;
                c.Save();
                MessageBox.Show("保存成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private async void BtnTest_Click(object sender, RoutedEventArgs e)
        {

            var r = AppDb.Test(TxtIP.Text, TxtPort.Text, TxtAcc.Text, Pwd.Password);
            await r;
            if (r.Result == true)
            {
                MessageBox.Show("连接成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("连接失败", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
