using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterModify
{
    public class Monster
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public Monster()
        {
        }

        public Monster(string name, string path)
        {
            Name = name;
            Path = path;
        }
    }
}