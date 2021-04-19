using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterModify
{
    interface IPvfUtil
    {
        abstract Task<string> GetFileAsync(string fileName, string encoding = "utf-8");
        abstract Task<bool> SaveFileAsync(string fileName, string data, string encoding = "utf-8");
        abstract Task<string> GetFileListAsync(string path, string encoding = "utf-8");
    }
}