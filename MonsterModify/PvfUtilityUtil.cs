using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MonsterModify
{
    public class PvfUtilityUtil : IPvfUtil
    {
        private static readonly HttpClient Client = new();
        private static readonly string GetFileListUrl = @"http://127.0.0.1:27000/list?path=";
        private static readonly string FileUrl = @"http://127.0.0.1:27000/file?name=";


        public async Task<string> GetFileAsync(string fileName)
        {
            var response = await Client.GetAsync(FileUrl + fileName);
            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }

        public async Task<bool> SaveFileAsync(string fileName, string data)
        {
            var response = await Client.PostAsync(FileUrl + fileName, new StringContent(data, Encoding.UTF8));
            if (response.StatusCode == HttpStatusCode.OK) return true;
            return false;
        }

        public async Task<string> GetFileListAsync(string path)
        {
            var response = await Client.GetAsync(GetFileListUrl + path);
            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
    }
}