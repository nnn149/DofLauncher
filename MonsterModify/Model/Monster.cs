using System.Collections.Generic;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace MonsterModify.Model
{
    public class Monster: ObservableObject
    {
        private Dictionary<string, MonsterAttribute> _monsterAttributes;
        public int Index { get; set; }
        public string Path { get; set; }

        public Dictionary<string, MonsterAttribute> MonsterAttributes
        {
            get => _monsterAttributes;
            set { _monsterAttributes = value; OnPropertyChanged();}
        }


        public Monster()
        {

        }

        public Monster(int index, string path)
        {
            Index = index;
            Path = path;
        }

        public Monster(string path)
        {

            Path = path;
        }
    }
}