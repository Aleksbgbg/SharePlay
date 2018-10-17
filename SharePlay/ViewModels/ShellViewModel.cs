namespace SharePlay.ViewModels
{
    using Caliburn.Micro;

    using SharePlay.Factories.Interfaces;
    using SharePlay.ViewModels.Interfaces;

    internal sealed class ShellViewModel : ViewModelBase, IShellViewModel
    {
        private readonly IConnectionFactory _connectionFactory;

        private readonly IWindowManager _windowManager;

        public ShellViewModel(IConnectionFactory connectionFactory, IWindowManager windowManager, IMainViewModel mainViewModel)
        {
            DisplayName = "SharePlay";

            _connectionFactory = connectionFactory;
            _windowManager = windowManager;
            MainViewModel = mainViewModel;
        }

        public IMainViewModel MainViewModel { get; }

        public void ShowHostWindow()
        {
            _windowManager.ShowWindow(_connectionFactory.MakeHostServerViewModel());
        }

        public void ShowJoinWindow()
        {
            _windowManager.ShowWindow(_connectionFactory.MakeJoinServerViewModel());
        }
    }
}