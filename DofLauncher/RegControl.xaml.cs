using System;
using System.Windows;
using System.Windows.Controls;

namespace DofLauncher
{
    /// <summary>
    /// RegControl.xaml 的交互逻辑
    /// </summary>
    public partial class RegControl : UserControl
    {
        public RegControl()
        {
            InitializeComponent();
        }

        private async void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TxtUser.Text.Length < 5 || TxtUser.Text.Length > 16 || Pwd1.Password.Length < 5 || Pwd1.Password.Length > 16)
                {
                    MessageBox.Show("用户名和密码长度在5-16", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (Pwd1.Password != Pwd2.Password)
                {
                    MessageBox.Show("两次密码不一样", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var result = await DofMysql.Reg(TxtUser.Text, Pwd1.Password, int.Parse(TxtCera.Text), TxtQQ.Text);
                MessageBox.Show(result, "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
