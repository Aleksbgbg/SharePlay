namespace SharePlay.Factories.Interfaces
{
    using SharePlay.ViewModels.Interfaces;

    internal interface IConnectionFactory
    {
        IHostServerViewModel MakeHostServerViewModel();

        IJoinServerViewModel MakeJoinServerViewModel();
    }
}