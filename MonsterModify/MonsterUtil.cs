using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MonsterModify
{
    public sealed class MonsterUtil
    {
        private static readonly Lazy<MonsterUtil> Lazy = new(() => new MonsterUtil());
        private static readonly IPvfUtil PvfUtil = new PvfUtilityUtil();
        public static MonsterUtil Instance => Lazy.Value;

        private MonsterUtil()
        {
            Init();
            Debug.WriteLine("MonsterUtil Init！");
        }

        private static readonly string RegexAllMonsterAttributes = @"(PVF_File[\n|\r\n]*)([\s\S]*?)([\n|\r\n]*\[)";
        private static readonly string TblPath = "monster/monsterapcdifficultybonus.tbl";

        private string dataStr;
        public double[,] MainData = new double[13, 5];
        private double[,] _extraData = new double[26, 4];
        public List<Monster> Monsters;


        public async void Init()
        {
            if (!string.IsNullOrEmpty(dataStr)) return;

            dataStr = await PvfUtil.GetFileAsync(TblPath);
            var strings = new Regex(RegexAllMonsterAttributes).Match(dataStr).Groups[2].Value.Trim().Replace("\r", "")
                .Split('\n');
            for (var i = 0; i < 13; i++)
            for (var j = 0; j < 5; j++)
                MainData[i, j] = double.Parse(strings[i * 5 + j]);

            for (var i = 0; i < 26; i++)
            for (var j = 0; j < 4; j++)
                _extraData[i, j] = double.Parse(strings[i * 4 + j + 65]);
        }

        public async Task<bool> SaveTbl()
        {
            var str = "";
            for (var i = 0; i < MainData.GetLength(0); i++)
            for (var j = 0; j < MainData.GetLength(1); j++)
                str += MainData[i, j].ToString("F2") + "\r\n";
            for (var i = 0; i < _extraData.GetLength(0); i++)
            for (var j = 0; j < _extraData.GetLength(1); j++)
                str += _extraData[i, j].ToString("F2") + "\r\n";
            str = str.Substring(0, str.Length - 2);
            var data = Regex.Replace(dataStr, RegexAllMonsterAttributes,
                m => m.Groups[1].Value + str + m.Groups[3].Value);
            if (await PvfUtil.SaveFileAsync(TblPath, data)) return true;
            return false;
        }

        public async Task LoadMonsters()
        {
            try
            {
                var pathStr = await PvfUtil.GetFileListAsync("monster");
                await Task.Run(async () =>
                {
                    var res = new Regex(".*mob").Matches(pathStr);
                    Monsters = new List<Monster>(res.Count);
                    for (var i = 0; i < res.Count; i++)
                    {
                        var mStr = await PvfUtil.GetFileAsync(res[i].Groups[0].Value);
                        var res2 = new Regex(@"name].*\n.*`(.+)`").Matches(mStr);
                        if (res2.Count < 1)
                        {
                            Debug.WriteLine("Regular processing error" + i);
                            continue;
                        }

                        Monster m = new Monster(res2[0].Groups[1].Value, res[i].Groups[0].Value);
                        Monsters.Add(m);
                    }
                });
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", ex.Message);
            }
        }


        /* MainData
0 好战
1 视野
2 命中增加
3 回避增加
4 怪物血量倍率
5 攻击动作速度
6 移动速度增加
7 异常状态抵抗
8 伤害增加
9 防御增加 （这2个数据 应该有差别 和普通攻击防御）
10 硬直抵抗
11 无视防御的攻击
12 无视攻击的防御
 */
    }
}