namespace SharePlay.Services.Interfaces
{
    using SharePlay.ViewModels.Interfaces;

    internal interface IConnectionWindowService
    {
        IHostServerViewModel ShowHostWindow();

        IJoinServerViewModel ShowJoinWindow();
    }
}