using MaterialDesignDemo;
using MaterialDesignDemo.Domain;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DofLauncher
{
    public class MainWindowModel : INotifyPropertyChanged
    {

        public int Uid { get; set; }
        public string Username { get; set; }
        public string UserPwd { get; set; }
        public string MysqlIP { get; set; }
        public string MysqlPort { get; set; }
        public string MysqlAcc { get; set; }
        public string MysqlPwd { get; set; }





        public MainWindowModel(int uid, string username, string userPwd, string mysqlIP, string mysqlPort, string mysqlAcc, string mysqlPwd)
        {
            Uid = uid;
            Username = username;
            UserPwd = userPwd;
            MysqlIP = mysqlIP;
            MysqlPort = mysqlPort;
            MysqlAcc = mysqlAcc;
            MysqlPwd = mysqlPwd;
            DemoItems = new ObservableCollection<DemoItem>(new[]
           {
               new DemoItem("登录",new HomeControl(),"滑里稽登陆器"),
               new DemoItem("注册",new RegControl(),"注册"),
                new DemoItem("配置",new ConfigControl(),"Mysql连接配置")
            });


            _demoItemsView = CollectionViewSource.GetDefaultView(DemoItems);
            _demoItemsView.Filter = DemoItemsFilter;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly ICollectionView _demoItemsView;
        private DemoItem? _selectedItem;
        private int _selectedIndex;
        private string? _searchKeyword;





        public string? SearchKeyword
        {
            get => _searchKeyword;
            set
            {
                _searchKeyword = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SearchKeyword)));
                _demoItemsView.Refresh();
            }
        }

        public ObservableCollection<DemoItem> DemoItems { get; }

        public DemoItem? SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (value == null || value.Equals(_selectedItem)) return;

                _selectedItem = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedItem)));
            }
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedIndex)));
            }
        }

        private bool DemoItemsFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchKeyword))
            {
                return true;
            }

            return obj is DemoItem item
                   && item.Name.ToLower().Contains(_searchKeyword!.ToLower());
        }
    }
}
