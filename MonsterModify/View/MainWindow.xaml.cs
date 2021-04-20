using System.Windows;
using Microsoft.Toolkit.Mvvm.Messaging;

namespace MonsterModify.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            WeakReferenceMessenger.Default.Register<string,string>(this, "MessageBox", (sender, arg) =>
            {
                MessageBox.Show(arg);
            });
        }

    }
}