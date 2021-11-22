using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.ObjectModel;

namespace Avalonia_ComboBox.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Greeting => "Welcome to Avalonia!";

        public ObservableCollection<String> Parameters { get; } =
            new (){ "param 1", "param 2", "param 3" };

        [Reactive]
        public String SelectedParameter { get; set; }

        public MainWindowViewModel()
        {
            SelectedParameter = Parameters[0];
        }
    }
}