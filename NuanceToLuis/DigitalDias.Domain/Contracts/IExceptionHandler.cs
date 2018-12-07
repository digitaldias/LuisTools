using System;

namespace DigitalDias.Domain.Contracts
{
    public interface IExceptionHandler
    {
        TResult Get<TResult>(Func<TResult> unsafeFunction);

        void Run(Action unsafeAction);
    }
}
