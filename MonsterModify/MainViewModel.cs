using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using MonsterModify.Annotations;

namespace MonsterModify
{
    public class MainViewModel : ObservableObject
    {
        private int _allMonsterAttributeListIndex;

        public int AllMonsterAttributeListIndex
        {
            get => _allMonsterAttributeListIndex;
            set
            {
                _allMonsterAttributeListIndex = value;
                OnPropertyChanged();
                UpdateMonsterAttribute();
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

        private void UpdateMonsterAttribute()
        {
            Debug.WriteLine("asd");
        }
    }
}