using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MonsterModify.Util
{
    public class PvfUtilityUtil : IPvfUtil
    {
        private readonly HttpClient _client;
        private readonly string _getFileListUrl;
        private readonly string _fileUrl;

        public string Ip { get; }

        public string Port { get; }

        public Encoding Encoder { get; set; }

        public PvfUtilityUtil(string ip, string port)
        {
            Ip = ip;
            Port = port;
            _getFileListUrl = @$"http://{ip}:{port}/list?path=";
            _fileUrl = @$"http://{ip}:{port}/file?name=";
            Encoder = Encoding.UTF8;
            _client = new HttpClient();
        }

        public async Task<string> GetFileAsync(string fileName)
        {
            var response = await _client.GetAsync(_fileUrl + fileName);
            //注册编码 支持big5 繁体
            //Encoder.RegisterProvider(CodePagesEncodingProvider.Instance);
            var responseBody = Encoder.GetString(await response.Content.ReadAsByteArrayAsync());
            return responseBody;
        }

        public async Task<bool> SaveFileAsync(string fileName, string data)
        {
            var response = await _client.PostAsync(_fileUrl + fileName, new StringContent(data, Encoder));
            if (response.StatusCode == HttpStatusCode.OK) return true;
            return false;
        }

        public async Task<string> GetFileListAsync(string path)
        {
            var response = await _client.GetAsync(_getFileListUrl + path);
            var responseBody = Encoder.GetString(await response.Content.ReadAsByteArrayAsync());
            return responseBody;
        }
    }
}