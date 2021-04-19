using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterModify
{
    public class Monster
    {
        public string Path { get; set; }
        public Dictionary<string, MonsterAttribute> MonsterAttributes { get; set; }


        public Monster()
        {

        }


        public Monster(string path)
        {

            Path = path;
        }
    }
}