using System.Windows.Input;

namespace Thermostat.ViewModels
{
    public interface IChangeViewCommandProvider
    {
        public ICommand ShowHistoryViewCommand { get; }

        public ICommand ShowSettingsViewCommand { get; }

        public ICommand ShowMainViewCommand { get; }

        public ICommand ShowScreenSaverCommand { get; }
    }
}
