using LuisTools.Domain.Contracts;
using LuisTools.Domain.Entities;
using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace LuisTools
{
    class ConsoleLogger : ILogger
    {
        public LogLevel Level { get; set; }

        public Action<string> LogAction { get; set; }


        public void Enter(object caller, [CallerMemberName] string methodName = "", params string[] args)
        {            
            if (Level < LogLevel.Verbose)
                return;

            var argsList  = args?.Select(a => $"'{a.ToString()}'");
            var finalArgs = string.Join(',', argsList);

            LogAction.Invoke($"-> {caller.GetType().Name}.{methodName}({finalArgs})" );
        }

        public void Logerror(string message)
        {
            if (Level < LogLevel.Error)
                return;

            LogAction.Invoke($">>> ERROR: {message} <<<");
        }

        public void Info(string message)
        {
            if (Level < LogLevel.Information)
                return;

            LogAction.Invoke(message);
        }

        public void Warning(string message)
        {
            if (Level < LogLevel.Warning)
                return;

            LogAction.Invoke($"Warning: {message}");
        }
    }
}
