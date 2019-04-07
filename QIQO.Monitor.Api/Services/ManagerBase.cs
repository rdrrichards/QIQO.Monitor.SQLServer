using Microsoft.Extensions.Logging;
using System;

namespace QIQO.Monitor.Api.Services
{
    public class ManagerBase
    {
        protected readonly ILogger<ManagerBase> _logger;
        public ManagerBase(ILogger<ManagerBase> logger)
        {
            _logger = logger;
        }
        protected T ExecuteOperation<T>(Func<T> codetoExecute)
        {
            try
            {
                return codetoExecute.Invoke();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        protected void ExecuteOperation(Action codetoExecute)
        {
            try
            {
                codetoExecute.Invoke();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
