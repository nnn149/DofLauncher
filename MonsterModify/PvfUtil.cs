using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MonsterModify
{
    public static class PvfUtil
    {
        private static readonly HttpClient Client = new();
        private static readonly string GetFileListUrl = @"http://127.0.0.1:27000/list?path=";
        private static readonly string FileUrl = @"http://127.0.0.1:27000/file?name=";






        public static async Task<string> GetFile(string fileName)
        {
            var response = await Client.GetAsync(FileUrl + fileName);
            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }

        public static async Task<bool> SaveFile(string fileName, string data)
        {
            var response = await Client.PostAsync(FileUrl + fileName, new StringContent(data, Encoding.UTF8));
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }
    }
}