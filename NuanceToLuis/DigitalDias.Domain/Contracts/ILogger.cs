using DigitalDias.Domain.Entities;
using System;
using System.Runtime.CompilerServices;

namespace DigitalDias.Domain.Contracts
{

    public interface ILogger
    {
        LogLevel Level { get; set; }

        Action<string> LogAction { get; set; }

        void Enter(object caller, [CallerMemberName] string methodName = "", params string[] args);

        void Info(string message);

        void Warning(string message);

        void Logerror(string message);
    }
}
