namespace SharePlay.ViewModels
{
    using SharePlay.ViewModels.Interfaces;

    internal class JoinServerViewModel : ViewModelBase, IJoinServerViewModel
    {
        public string Status { get; } = "Disconnected";

        public void Connect(string targetAddress)
        {
        }
    }
}