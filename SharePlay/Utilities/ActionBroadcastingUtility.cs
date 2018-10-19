namespace SharePlay.Utilities
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;

    using Caliburn.Micro;

    using SharePlay.Models;
    using SharePlay.Services.NetworkInteraction.Interfaces;

    internal class ActionBroadcastingUtility
    {
        private readonly IReceiverService _receiverService;

        private readonly MethodInfo[] _receiverMethods;

        internal ActionBroadcastingUtility(IReceiverService receiverService)
        {
            _receiverService = receiverService;

            _receiverMethods = receiverService.GetType().GetMethods();
        }

        internal void ReceiveAction(string action)
        {
            string[] commands = action.Split(' ');

            int methodNameIndex;

            if (DateTime.TryParse(string.Concat(commands[0], " ", commands[1]), out DateTime sendTime))
            {
                ((IClientReceiverService)_receiverService).MessageContext = new MessageContext(sendTime);
                methodNameIndex = 2;
            }
            else
            {
                methodNameIndex = 0;
            }

            string methodName = commands[methodNameIndex];

            MethodInfo targetMethod = _receiverMethods.Single(method => method.Name == methodName);

            ParameterInfo[] parameters = targetMethod.GetParameters();
            object[] arguments = new object[parameters.Length];

            for (int parameterIndex = 0; parameterIndex < parameters.Length; ++parameterIndex)
            {
                Type parameterType = parameters[parameterIndex].ParameterType;

                TypeConverter converter = TypeDescriptor.GetConverter(parameterType);

                arguments[parameterIndex] = converter.ConvertFromString(commands[methodNameIndex + 1 + parameterIndex]);
            }

            Execute.BeginOnUIThread(() => targetMethod.Invoke(_receiverService, arguments));
        }
    }
}