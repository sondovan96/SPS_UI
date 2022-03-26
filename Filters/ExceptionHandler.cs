using System;

namespace SPS.UI.Filters
{
    public class ExceptionHandler
    {
        public string HandleUnhandleException(Exception exception)
        {
            throw exception;
        }

        public string HandleUnauthorizedException(Exception exception)
        {
            return "/Account/Index";
        }

    }
}
