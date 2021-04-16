using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MonsterModify
{
    public class MonsterUtil
    {
        private static readonly string RegexAllMonsterAttributes = @"PVF_File([\s\S]*?)\[";
        private static readonly string TblPath = "monster/monsterapcdifficultybonus.tbl";

        private string _dataStr;
        public double[,] MainData = new double[13, 5];
        private double[,] _extraData = new double[26, 4];

        public async Task Init()
        {
            _dataStr = await PvfUtil.GetFile(TblPath);
            var strings = new Regex(RegexAllMonsterAttributes).Match(_dataStr).Groups[1].Value.Trim().Replace("\r", "")
                .Split('\n');
            for (var i = 0; i < 13; i++)
            {
                for (var j = 0; j < 5; j++)
                    MainData[i, j] = double.Parse(strings[i * 5 + j]);
            }
            for (var i = 0; i < 26; i++)
            {
                for (var j = 0; j < 4; j++)
                    _extraData[i, j] = double.Parse(strings[i * 4 + j + 65]);
            }
        }

        public async Task<bool> SaveTbl(double[,] data)
        {
            for (var i = 0; i < data.GetLength(0); i++)
            for (var j = 0; j < data.GetLength(1); j++)
                _dataStr += data[i, j] + "\r\n";
            _dataStr = _dataStr.Substring(0, _dataStr.Length - 2);
            if (await PvfUtil.SaveFile(TblPath, _dataStr))
            {
                return true;
            }

            return false;
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