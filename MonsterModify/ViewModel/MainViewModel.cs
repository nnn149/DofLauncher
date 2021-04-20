using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using MonsterModify.Model;
using MonsterModify.Util;

namespace MonsterModify.ViewModel
{
    public class MainViewModel : ObservableObject

    {
        #region field

        private int _allMonsterAttributeListIndex = -1;
        private double _normalMode;
        private double _adventureMode;
        private double _kingMode;
        private double _hellMode;
        private double _unknownMode;
        private MonsterUtil _monsterUtil;
        private ObservableCollection<Monster> _monsterList;
        private Monster _selectMonster;
        private ICollectionView _listCollectionView;
        private string _searchName;
        private int _currentProgress;

        #endregion

        #region prop

        public string Ip { get; set; }
        public string Port { get; set; }

        public int CurrentProgress
        {
            get => _currentProgress;
            set { _currentProgress = value; OnPropertyChanged(); }
        }

        public ICollectionView ListCollectionView
        {
            get => _listCollectionView;
            set
            {
                _listCollectionView = value;
                OnPropertyChanged();
            }
        }

        public string SearchName
        {
            get => _searchName;
            set
            {
                _searchName = value;
                OnPropertyChanged();
                if (_listCollectionView != null) _listCollectionView.Refresh();
            }
        }

        public Monster SelectMonster
        {
            get => _selectMonster;
            set
            {
                _selectMonster = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Monster> MonsterList
        {
            get => _monsterList;
            set
            {
                _monsterList = value;
                OnPropertyChanged();
            }
        }

        public double NormalMode
        {
            get => _normalMode;
            set
            {
                _normalMode = value;
                OnPropertyChanged();
            }
        }

        public double AdventureMode
        {
            get => _adventureMode;
            set
            {
                _adventureMode = value;
                OnPropertyChanged();
            }
        }

        public double KingMode
        {
            get => _kingMode;
            set
            {
                _kingMode = value;
                OnPropertyChanged();
            }
        }

        public double HellMode
        {
            get => _hellMode;
            set
            {
                _hellMode = value;
                OnPropertyChanged();
            }
        }

        public double UnknownMode
        {
            get => _unknownMode;
            set
            {
                _unknownMode = value;
                OnPropertyChanged();
            }
        }

        public int AllMonsterAttributeListIndex
        {
            get => _allMonsterAttributeListIndex;
            set
            {
                _allMonsterAttributeListIndex = value;
                OnPropertyChanged();
                UpdateMonsterAttribute();
            }
        }

        public IAsyncRelayCommand LoadOneMonstersCommand { get; set; }
        public IAsyncRelayCommand LoadAllMonstersCommand { get; set; }
        public IAsyncRelayCommand LoadAllMonsterAttributeCommand { get; set; }
        public IAsyncRelayCommand SaveMonsterAttributeCommand { get; set; }
        public IAsyncRelayCommand SaveAllMonsterAttributeCommand { get; set; }

        #endregion

        public MainViewModel()
        {
            Ip = "127.0.0.1";
            Port = "27000";
            LoadOneMonstersCommand = new AsyncRelayCommand(LoadOneMonsters);
            LoadAllMonstersCommand = new AsyncRelayCommand(LoadAllMonsters);
            LoadAllMonsterAttributeCommand = new AsyncRelayCommand(LoadAllMonsterAttribute);
            SaveMonsterAttributeCommand = new AsyncRelayCommand(SaveMonsterAttribute);
            SaveAllMonsterAttributeCommand = new AsyncRelayCommand(SaveAllMonsterAttributeAsync);
        }

        private void Init()
        {
            if (_monsterUtil == null)
            {
                _monsterUtil = new MonsterUtil(Ip, Port);
            }
        }

        private async Task SaveAllMonsterAttributeAsync()
        {
            if (_monsterUtil != null)
            {
                _monsterUtil.MainData[AllMonsterAttributeListIndex, 0] = NormalMode;
                _monsterUtil.MainData[AllMonsterAttributeListIndex, 1] = AdventureMode;
                _monsterUtil.MainData[AllMonsterAttributeListIndex, 2] = KingMode;
                _monsterUtil.MainData[AllMonsterAttributeListIndex, 3] = HellMode;
                _monsterUtil.MainData[AllMonsterAttributeListIndex, 4] = UnknownMode;
                if (await _monsterUtil.SaveTbl())
                    WeakReferenceMessenger.Default.Send("保存成功", "MessageBox");
                else
                    WeakReferenceMessenger.Default.Send("保存失败", "MessageBox");
            }
        }

        private async Task LoadAllMonsters()
        {
            Init();
            var progressIndicator = new Progress<int>(v => CurrentProgress = v);
            await _monsterUtil.LoadAllMonsters(progressIndicator);
            MonsterList = new ObservableCollection<Monster>(_monsterUtil.Monsters);
            ListCollectionView = CollectionViewSource.GetDefaultView(MonsterList);
            ListCollectionView.Filter = FilterTask;
        }

        private void UpdateMonsterAttribute()
        {
            if (_monsterUtil != null)
            {
                NormalMode = _monsterUtil.MainData[AllMonsterAttributeListIndex, 0];
                AdventureMode = _monsterUtil.MainData[AllMonsterAttributeListIndex, 1];
                KingMode = _monsterUtil.MainData[AllMonsterAttributeListIndex, 2];
                HellMode = _monsterUtil.MainData[AllMonsterAttributeListIndex, 3];
                UnknownMode = _monsterUtil.MainData[AllMonsterAttributeListIndex, 4];
            }
        }


        private async Task SaveMonsterAttribute()
        {
            if (_monsterUtil != null && SelectMonster != null)
            {
                if (await _monsterUtil.SaveMonster(SelectMonster))
                    WeakReferenceMessenger.Default.Send("保存成功", "MessageBox");
                else
                    WeakReferenceMessenger.Default.Send("保存失败", "MessageBox");
            }
        }

        private async Task LoadOneMonsters()
        {
            if (_monsterUtil != null && SelectMonster != null)
                SelectMonster.MonsterAttributes =
                    (await _monsterUtil.LoadOneMonster(SelectMonster.Index, SelectMonster.Path)).MonsterAttributes;
        }

        private async Task LoadAllMonsterAttribute()
        {
            Init();
            await _monsterUtil.LoadTbl();
            AllMonsterAttributeListIndex = 0;
        }

        public bool FilterTask(object value)
        {
            if (value is Monster entry)
            {
                if (!string.IsNullOrEmpty(SearchName))
                    return entry.MonsterAttributes["name"].Value.Contains(SearchName);

                return true;
            }

            return false;
        }
    }
}