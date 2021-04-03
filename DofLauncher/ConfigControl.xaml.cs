using System;
using System.Collections.Generic;
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
                c.save();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private async void BtnTest_Click(object sender, RoutedEventArgs e)
        {

            var r = AppDb.Test(TxtIP.Text, TxtPort.Text, TxtAcc.Text, Pwd.Password);
            await r;
            if (r.Result == true)
            {
                MessageBox.Show("连接成功");
            }
            else
            {
                MessageBox.Show("连接失败");
            }

        }
    }
}
