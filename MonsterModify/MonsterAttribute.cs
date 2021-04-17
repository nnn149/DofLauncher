using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterModify
{
    public class MonsterAttribute<T>
    {
        /// <summary>
        /// 中文名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// 正则表达式
        /// </summary>
        public string Pattern { get; set; }

        /// <summary>
        /// 替换位置（group）
        /// </summary>
        public int ReplaceIndex { get; set; } = 2;

        /// <summary>
        /// mob文件中标签名
        /// </summary>
        public string Tag { get; set; }

        public MonsterAttribute()
        {
        }
        public MonsterAttribute(string name,string tag)
        {
            Name = name;
            Tag = tag;
            Pattern = @$"(\[{tag}\][\n|\r\n|\s]*)(.+?)([\n|\r\n]+)";
        }

        public MonsterAttribute(string name, string pattern, string tag)
        {
            Name = name;
            Pattern = pattern;
            Tag = tag;
        }

        public MonsterAttribute(string name, string pattern, int replaceIndex, string tag)
        {
            Name = name;
            Pattern = pattern;
            ReplaceIndex = replaceIndex;
            Tag = tag;
        }
    }
}