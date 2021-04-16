using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MonsterModify
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly HttpClient client = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                var response = await client.GetAsync("http://127.0.0.1:27000/list?path=monster");
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                var a = ".*mob";
                var r = new Regex(a);
                var res = r.Matches(responseBody);
                Debug.WriteLine(res.Count);


                for (var i = 0; i < res.Count; i++)
                {
                    response = await client.GetAsync("http://127.0.0.1:27000/file?name=" + res[i].Groups[0].Value);
                    responseBody = await response.Content.ReadAsStringAsync();
                    // Above three lines can be replaced with new helper method below
                    // string responseBody = await client.GetStringAsync(uri);


                    var a2 = @"name].*\n.*`(.+)`";
                    var r2 = new Regex(a2);
                    var res2 = r2.Matches(responseBody);
                    if (res2.Count < 1)
                    {
                        Debug.WriteLine("continu" + i);
                        continue;
                    }

                    Debug.WriteLine("count " + i);

                    Debug.WriteLine(res[i].Groups[0].Value);
                    Debug.WriteLine(res2[0].Groups[1].Value);
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", ex.Message);
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var data = new double[13, 5];

            var quanju = "monster/monsterapcdifficultybonus.tbl";
            var zz = @"PVF_File([\s\S]*?)\[";
            var tbl = await GetFile(quanju);
            var r = new Regex(zz);
            var value = r.Match(tbl).Groups[1].Value.Trim().Replace("\r", "");
            var strings = value.Split('\n');
            for (var i = 0; i < 13; i++)
            {
                var a = $"第{i}组：";
                for (var j = 0; j < 5; j++)
                    // data[i] = new List<double>(5);
                    data[i, j] = double.Parse(strings[i * 5 + j]);
            }

            var data2 = new double[26, 4];
            for (var i = 0; i < 26; i++)
            {
                var a = $"第{i}组：";
                for (var j = 0; j < 4; j++)
                    // data[i] = new List<double>(5);
                    data2[i, j] = double.Parse(strings[i * 4 + j + 65]);
            }

            SaveTbl(data2);
        }


        public async Task<string> GetFile(string fileName)
        {
            var response = await client.GetAsync("http://127.0.0.1:27000/file?name=" + fileName);
            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }

        public async Task SaveTbl(double[,] data)
        {
            var a = "";
            for (var i = 0; i < data.GetLength(0); i++)
            {
                for (var j = 0; j < data.GetLength(1); j++) a += data[i, j] + "\r\n";
            }

            a = a.Substring(0, a.Length - 2);
            
            Debug.WriteLine(a);
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {



        }
    }
}