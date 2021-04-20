using System.Threading.Tasks;

namespace MonsterModify.Util
{
    interface IPvfUtil
    {
        abstract Task<string> GetFileAsync(string fileName);
        abstract Task<bool> SaveFileAsync(string fileName, string data);
        abstract Task<string> GetFileListAsync(string path);
    }
}