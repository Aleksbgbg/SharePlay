namespace SharePlay.Views
{
    using Caliburn.Micro;

    using SharePlay.Services.Interfaces;

    public partial class PlayerView
    {
        public PlayerView()
        {
            InitializeComponent();

            IoC.Get<IMediaPlayerService>().Initialize(Player);
        }
    }
}