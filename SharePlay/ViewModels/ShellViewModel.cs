namespace SharePlay.ViewModels
{
    using SharePlay.Services.Interfaces;
    using SharePlay.ViewModels.Interfaces;

    internal sealed class ShellViewModel : ViewModelBase, IShellViewModel
    {
        private readonly IConnectionWindowService _connectionWindowService;

        public ShellViewModel(IConnectionWindowService connectionWindowService, IMainViewModel mainViewModel)
        {
            DisplayName = "SharePlay";

            _connectionWindowService = connectionWindowService;
            MainViewModel = mainViewModel;
        }

        public IMainViewModel MainViewModel { get; }

        public void ShowHostWindow()
        {
            _connectionWindowService.ShowHostWindow();
        }

        public void ShowJoinWindow()
        {
            _connectionWindowService.ShowJoinWindow();
        }
    }
}