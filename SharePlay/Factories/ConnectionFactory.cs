namespace SharePlay.Factories
{
    using Caliburn.Micro;

    using SharePlay.Factories.Interfaces;
    using SharePlay.ViewModels.Interfaces;

    internal class ConnectionFactory : IConnectionFactory
    {
        public IHostServerViewModel MakeHostServerViewModel()
        {
            return IoC.Get<IHostServerViewModel>();
        }

        public IJoinServerViewModel MakeJoinServerViewModel()
        {
            return IoC.Get<IJoinServerViewModel>();
        }
    }
}