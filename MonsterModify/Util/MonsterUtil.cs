using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MonsterModify.Model;
using Newtonsoft.Json;

namespace MonsterModify.Util
{
    public sealed class MonsterUtil
    {
        private static readonly string RegexAllMonsterAttributes = @"(PVF_File[\n|\r\n]*)([\s\S]*?)([\n|\r\n]*\[)";
        private static readonly string TblPath = "monster/monsterapcdifficultybonus.tbl";
        private static readonly string MonsterAttributeJson = File.ReadAllText("MonsterAttribute.json");
        private readonly IPvfUtil _pvfUtil;

        public double[,] MainData { get; set; } = new double[13, 5];
        private readonly double[,] _extraData = new double[26, 4];
        public List<Monster> Monsters { get; set; }

        public MonsterUtil(string ip, string port)

        {
            _pvfUtil = new PvfUtilityUtil(ip, port);
            Debug.WriteLine("MonsterUtil LoadTbl！");
        }

        public async Task LoadTbl()
        {
            var dataStr = await _pvfUtil.GetFileAsync(TblPath);
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
            var dataStr = await _pvfUtil.GetFileAsync(TblPath);
            var str = "";
            for (var i = 0; i < MainData.GetLength(0); i++)
            for (var j = 0; j < MainData.GetLength(1); j++)
                str += MainData[i, j].ToString("F2") + "\r\n";
            for (var i = 0; i < _extraData.GetLength(0); i++)
            for (var j = 0; j < _extraData.GetLength(1); j++)
                str += _extraData[i, j].ToString("F2") + "\r\n";
            str = str[..^2];
            var data = Regex.Replace(dataStr, RegexAllMonsterAttributes,
                m => m.Groups[1].Value + str + m.Groups[3].Value);
            if (await _pvfUtil.SaveFileAsync(TblPath, data)) return true;
            return false;
        }

        public async Task LoadAllMonsters(IProgress<int> progress)
        {
            var pathStr = await _pvfUtil.GetFileListAsync("monster");
            var res = new Regex(".*mob").Matches(pathStr);
            var totalCount = res.Count;
            int tempCount = 0;
            Monsters = new List<Monster>(totalCount);
            for (var i = 0; i < totalCount; i++)
            {
                var m = await LoadOneMonster(res[i].Groups[0].Value);
                if (m != null) Monsters.Add(m);
                if (progress != null)
                {
                    progress.Report((tempCount * 100 / totalCount));

                }
                tempCount++;
            }

            if (progress != null) progress.Report(100);
        }


        public async Task<Monster> LoadOneMonster(string path)
        {
            var mStr = await _pvfUtil.GetFileAsync(path);
            var monster = new Monster(path)
            {
                MonsterAttributes =
                    JsonConvert.DeserializeObject<Dictionary<string, MonsterAttribute>>(MonsterAttributeJson)
            };
            if (monster.MonsterAttributes != null)

                foreach (var attribute in monster.MonsterAttributes)
                {
                    var matchCollection = new Regex(attribute.Value.Pattern).Matches(mStr);
                    if (matchCollection.Count < 1)
                    {
                        monster.MonsterAttributes.Remove(attribute.Key);
                        continue;
                    }

                    attribute.Value.Value = matchCollection[0].Groups[attribute.Value.ReplaceIndex].Value;
                }
            else
                return null;

            if (monster.MonsterAttributes.ContainsKey("name"))
            {
                if (string.IsNullOrEmpty(monster.MonsterAttributes["name"].Value)) return null;
            }
            else
            {
                return null;
            }

            return monster;
        }

        public async Task<bool> SaveMonster(Monster monster)
        {
            var data = await _pvfUtil.GetFileAsync(monster.Path);
            if (!string.IsNullOrEmpty(data))
            {
                foreach (var attribute in monster.MonsterAttributes)
                    if (new Regex(attribute.Value.Pattern).Matches(data).Count < 1)
                        continue;
                    else
                        data = Regex.Replace(data, attribute.Value.Pattern, m =>
                        {
                            var str = "";
                            for (var i = 1; i <= m.Groups.Count; i++)
                                if (i != attribute.Value.ReplaceIndex)
                                    str += m.Groups[i].Value;
                                else
                                    str += attribute.Value.Value;

                            return str;
                        });

                if (await _pvfUtil.SaveFileAsync(monster.Path, data)) return true;
            }

            return false;
        }
    }
}