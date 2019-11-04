using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.Services
{
    interface IExceptionHandlingService
    {
        void LogException();
        string GetExceptionDetails(Exception e);
    }
}
