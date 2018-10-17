namespace SharePlay.ViewModels
{
    using System.Collections.Generic;

    using Caliburn.Micro;

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

        public IEnumerable<IResult> Connect(string targetAddress)
        {
            Status = "Connecting";

            TaskResult<bool> connectResult = _playClientService.TryConnect(NetworkAddress.Parse(targetAddress)).AsResult();

            yield return connectResult;

            Status = connectResult.Result ? "Connected" : "Disconnected";
        }
    }
}