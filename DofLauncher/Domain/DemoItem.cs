using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace MaterialDesignDemo.Domain
{
    public class DemoItem : INotifyPropertyChanged
    {
        private string _name;
        private object? _content;
        private string _title;

        private ScrollBarVisibility _horizontalScrollBarVisibilityRequirement;
        private ScrollBarVisibility _verticalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto;
        private Thickness _marginRequirement = new(16);

        public DemoItem(string name, object? content, string title)
        {
            _name = name;
            Content = content;
            _title = title;

        }

        public string Name
        {
            get => _name;
            set => this.MutateVerbose(ref _name, value, RaisePropertyChanged());
        }
        public string Title
        {
            get => _title;
            set => this.MutateVerbose(ref _title, value, RaisePropertyChanged());
        }
        public object? Content
        {
            get => _content;
            set => this.MutateVerbose(ref _content, value, RaisePropertyChanged());
        }

        public ScrollBarVisibility HorizontalScrollBarVisibilityRequirement
        {
            get => _horizontalScrollBarVisibilityRequirement;
            set => this.MutateVerbose(ref _horizontalScrollBarVisibilityRequirement, value, RaisePropertyChanged());
        }

        public ScrollBarVisibility VerticalScrollBarVisibilityRequirement
        {
            get => _verticalScrollBarVisibilityRequirement;
            set => this.MutateVerbose(ref _verticalScrollBarVisibilityRequirement, value, RaisePropertyChanged());
        }

        public Thickness MarginRequirement
        {
            get => _marginRequirement;
            set => this.MutateVerbose(ref _marginRequirement, value, RaisePropertyChanged());
        }



        public event PropertyChangedEventHandler? PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
            => args => PropertyChanged?.Invoke(this, args);
    }
}
