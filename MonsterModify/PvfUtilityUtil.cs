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


        public async Task<string> GetFileAsync(string fileName, string encoding = "utf-8")
        {
            var response = await Client.GetAsync(FileUrl + fileName);
            //注册编码 支持big5 繁体
            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var responseBody = Encoding.GetEncoding(encoding).GetString(await response.Content.ReadAsByteArrayAsync());
            return responseBody;
        }

        public async Task<bool> SaveFileAsync(string fileName, string data, string encoding = "utf-8")
        {
            var response = await Client.PostAsync(FileUrl + fileName, new StringContent(data, Encoding.GetEncoding(encoding)));
            if (response.StatusCode == HttpStatusCode.OK) return true;
            return false;
        }

        public async Task<string> GetFileListAsync(string path, string encoding = "utf-8")
        {
            var response = await Client.GetAsync(GetFileListUrl + path);
            var responseBody = Encoding.GetEncoding(encoding).GetString(await response.Content.ReadAsByteArrayAsync());
            return responseBody;
        }
    }
}