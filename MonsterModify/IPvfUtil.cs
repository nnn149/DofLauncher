using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterModify
{
    interface IPvfUtil
    {
         abstract   Task<string> GetFileAsync(string fileName);
         abstract   Task<bool> SaveFileAsync(string fileName, string data);
    }
}
