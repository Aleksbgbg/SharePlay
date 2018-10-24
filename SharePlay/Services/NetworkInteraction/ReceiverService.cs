namespace SharePlay.Services.NetworkInteraction
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;

    using Caliburn.Micro;

    using SharePlay.Services.NetworkInteraction.Interfaces;

    internal abstract class ReceiverService : IReceiverService
    {
        private readonly MethodInfo[] _methods;

        private protected ReceiverService()
        {
            _methods = GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        }

        private protected virtual int MethodNameIndex => 0;

        public void Receive(string message)
        {
            string[] commands = message.Split(' ');

            Process(commands);

            string methodName = commands[MethodNameIndex];

            MethodInfo targetMethod = _methods.Single(method => method.Name == methodName);

            ParameterInfo[] parameters = targetMethod.GetParameters();
            object[] arguments = new object[parameters.Length];

            for (int parameterIndex = 0; parameterIndex < parameters.Length; ++parameterIndex)
            {
                Type parameterType = parameters[parameterIndex].ParameterType;

                TypeConverter converter = TypeDescriptor.GetConverter(parameterType);

                arguments[parameterIndex] = converter.ConvertFromString(commands[MethodNameIndex + 1 + parameterIndex]);
            }

            Execute.BeginOnUIThread(() => targetMethod.Invoke(this, arguments));
        }

        private protected virtual void Process(string[] commands)
        {
        }
    }
}