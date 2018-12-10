using System;

namespace LuisTools.Domain.Contracts
{
    public interface IExceptionHandler
    {
        TResult Get<TResult>(Func<TResult> unsafeFunction);

        void Run(Action unsafeAction);
    }
}
