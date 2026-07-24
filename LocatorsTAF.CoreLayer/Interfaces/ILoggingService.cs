using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsTAF.CoreLayer.Interfaces
{
    public interface ILoggingService
    {
        void Info(string message);

        void Warn(string message);

        void Error(string message);

        void Error(string message, Exception exception);

        void Debug(string message);
    }
}
