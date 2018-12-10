using LuisTools.Domain.Contracts;
using System;

namespace LuisTools.Business
{
    public class ExceptionHandler : IExceptionHandler
    {
        private readonly ILogger _logger;

        public ExceptionHandler(ILogger logger)
        {
            _logger = logger;
        }


        public TResult Get<TResult>(Func<TResult> unsafeFunction)
        {
            if (unsafeFunction == null)
                return default(TResult);

            try
            {
                return unsafeFunction.Invoke();
            }
            catch(Exception ex)
            {
                _logger.Logerror(ex.Message);
            }
            return default(TResult);
        }


        public void Run(Action unsafeAction)
        {
            if(unsafeAction == null)
            {
                _logger.Warning("ExceptionHandler.Run() called without a valid Action");
                return;
            }

            try
            {
                unsafeAction.Invoke();
            }
            catch(Exception ex)
            {
                _logger.Logerror(ex.Message);
            }
        }
    }
}
