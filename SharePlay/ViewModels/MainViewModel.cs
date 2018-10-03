namespace SharePlay.ViewModels
{
    using SharePlay.ViewModels.Interfaces;

    internal class MainViewModel : ViewModelBase, IMainViewModel
    {
        public MainViewModel(IPlayerViewModel playerViewModel)
        {
            PlayerViewModel = playerViewModel;
        }

        public IPlayerViewModel PlayerViewModel { get; }
    }
}