namespace SharePlay.Services.NetworkInteraction
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    using Caliburn.Micro;

    using SharePlay.Services.NetworkInteraction.Interfaces;

    internal abstract class SenderService : ISenderService
    {
        private Action<string> _broadcastMethod;

        public void Initialize(Action<string> broadcastMethod)
        {
            _broadcastMethod = broadcastMethod;
        }

        private protected void TransmitMemberCall([CallerMemberName] string memberName = default, params object[] arguments)
        {
            List<string> transmissionArguments = new List<string>();

            transmissionArguments.Add(memberName);
            transmissionArguments.AddRange(arguments.Select(argument => argument.ToString()));

            InsertArgumentsBeforeTransmission(transmissionArguments);

            _broadcastMethod(string.Join(" ", transmissionArguments));
        }

        private protected void TransmitMemberCall<TProperty>(Expression<Func<TProperty>> propertyNameExpression, params object[] arguments)
        {
            TransmitMemberCall(propertyNameExpression.GetMemberInfo().Name, arguments);
        }

        private protected virtual void OnInitialize()
        {
        }

        private protected virtual void InsertArgumentsBeforeTransmission(List<string> transmissionArguments)
        {
        }
    }
}