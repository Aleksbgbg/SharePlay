namespace SharePlay.ViewModels
{
    using SharePlay.ViewModels.Interfaces;

    internal sealed class JoinServerViewModel : ViewModelBase, IJoinServerViewModel
    {
        public JoinServerViewModel()
        {
            DisplayName = "Join Server";
        }

        public string Status { get; } = "Disconnected";

        public void Connect(string targetAddress)
        {
        }
    }
}