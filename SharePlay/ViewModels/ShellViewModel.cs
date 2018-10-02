namespace SharePlay.ViewModels
{
    using SharePlay.ViewModels.Interfaces;

    internal sealed class ShellViewModel : ViewModelBase, IShellViewModel
    {
        public ShellViewModel(IMainViewModel mainViewModel)
        {
            DisplayName = "SharePlay";

            MainViewModel = mainViewModel;
        }

        public IMainViewModel MainViewModel { get; }
    }
}