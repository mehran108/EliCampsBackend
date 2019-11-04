using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging.AzureAppServices.Internal;
using ELI.Domain.Helpers;

namespace ELI.Domain.Services
{
  public  class ExceptionHandlingService :  Exception
    {
        private readonly string _username;
        private readonly Exception _appException;
        private string _fileName;

        public ExceptionHandlingService(Exception exp, string userName = "", string fileName = "PLRA")
            : base(exp.Message, exp)
        {
            this._username = userName;
            this._appException = exp;
            this._fileName = fileName;
        }

        public void LogException()
        {
            string exceptionDetails = GetExceptionDetails(this._appException);
            Helpers.LogMessage.LogMessageNow(exceptionDetails, this._username);
        }

        public string GetExceptionDetails(Exception e)
        {
            string eMessage = "";
            eMessage += "Exp: " + e.Message + "\t\t";
            if (e.InnerException == null)
                return eMessage;
            return GetExceptionDetails(e.InnerException);
        }


    }
}
