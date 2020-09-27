using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Thermostat.ViewModels
{
    public class MainViewModel: BaseViewModel
    {
        public MainViewModel(ICommand openHistory, ICommand openSettings)
        {
            OpenHistoryCommand = openHistory;
            OpenSettingsCommand = openSettings;
        }

        public ICommand OpenHistoryCommand { get; }

        public ICommand OpenSettingsCommand { get; }
    }
}
