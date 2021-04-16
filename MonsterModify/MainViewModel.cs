﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using MonsterModify.Annotations;

namespace MonsterModify
{
    public class MainViewModel : ObservableObject
    {
        private int _allMonsterAttributeListIndex = -1;
        private double _normalMode;
        private double _adventureMode;
        private double _kingMode;
        private double _hellMode;
        private double _unknownMode;
        private readonly MonsterUtil _monsterUtil = MonsterUtil.Instance;

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
                // new Task(async () => { await UpdateMonsterAttribute(); }
                // ).Start();
                UpdateMonsterAttribute();
            }
        }

        public IAsyncRelayCommand SaveAllMonsterAttributeCommand { get; set; }


        public MainViewModel()
        {
            SaveAllMonsterAttributeCommand = new AsyncRelayCommand(SaveAllMonsterAttribute);
        }

        private async Task SaveAllMonsterAttribute()
        {
            _monsterUtil.MainData[AllMonsterAttributeListIndex, 0] = NormalMode;
            _monsterUtil.MainData[AllMonsterAttributeListIndex, 1] = AdventureMode;
            _monsterUtil.MainData[AllMonsterAttributeListIndex, 2] = KingMode;
            _monsterUtil.MainData[AllMonsterAttributeListIndex, 3] = HellMode;
            _monsterUtil.MainData[AllMonsterAttributeListIndex, 4] = UnknownMode;
            if (await _monsterUtil.SaveTbl())
                MessageBox.Show("保存成功");
            else
                MessageBox.Show("保存失败");
        }

        private void UpdateMonsterAttribute()
        {
            NormalMode = _monsterUtil.MainData[AllMonsterAttributeListIndex, 0];
            AdventureMode = _monsterUtil.MainData[AllMonsterAttributeListIndex, 1];
            KingMode = _monsterUtil.MainData[AllMonsterAttributeListIndex, 2];
            HellMode = _monsterUtil.MainData[AllMonsterAttributeListIndex, 3];
            UnknownMode = _monsterUtil.MainData[AllMonsterAttributeListIndex, 4];
        }
    }
}