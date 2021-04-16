using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using MonsterModify.Annotations;

namespace MonsterModify
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private int _allMonsterAttributeListIndex;

        public int AllMonsterAttributeListIndex
        {
            get => _allMonsterAttributeListIndex;
            set
            {
                _allMonsterAttributeListIndex = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand TestCommand { get; set; }

        public MainViewModel()
        {
            TestCommand = new RelayCommand(Test);
        }

        private void Test()
        {
            AllMonsterAttributeListIndex++;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}