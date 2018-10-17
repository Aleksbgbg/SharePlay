namespace SharePlay.ViewModels
{
    using SharePlay.Models;
    using SharePlay.Services.Interfaces;
    using SharePlay.ViewModels.Interfaces;

    internal class JoinServerViewModel : ViewModelBase, IJoinServerViewModel
    {
        private readonly IPlayClientService _playClientService;

        public JoinServerViewModel(IPlayClientService playClientService)
        {
            _playClientService = playClientService;
        }

        private string _status = "Disconnected";
        public string Status
        {
            get => _status;

            set
            {
                if (_status == value) return;

                _status = value;
                NotifyOfPropertyChange(() => Status);
            }
        }

        public void Connect(string targetAddress)
        {
            _playClientService.Connect(NetworkAddress.Parse(targetAddress));
            Status = "Connected";
        }
    }
}