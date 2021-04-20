using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace MonsterModify.Model
{
    public class MonsterAttribute:ObservableObject
    {
        private string _name;
        private string _value;

        /// <summary>
        /// 中文名
        /// </summary>
        public string Name
        {
            get => _name;
            set { _name = value;OnPropertyChanged(); }
        }

        /// <summary>
        /// 值
        /// </summary>
        public string Value
        {
            get => _value;
            set { _value = value;OnPropertyChanged(); }
        }

        /// <summary>
        /// 正则表达式
        /// </summary>
        public string Pattern { get; set; }

        /// <summary>
        /// 替换位置（group）
        /// </summary>
        public int ReplaceIndex { get; set; } = 2;


        public MonsterAttribute()
        {
        }
        
    }
}