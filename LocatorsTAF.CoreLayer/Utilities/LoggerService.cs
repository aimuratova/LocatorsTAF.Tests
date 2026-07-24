using LocatorsTAF.CoreLayer.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsTAF.CoreLayer.Utilities
{
    public class LoggerService : ILoggingService
    {
        private readonly ILog _logger;

        public LoggerService()
        {
            _logger = LogManager.GetLogger(typeof(LoggerService));
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(string message, Exception exception)
        {
            _logger.Error(message, exception);
        }
    }
}
