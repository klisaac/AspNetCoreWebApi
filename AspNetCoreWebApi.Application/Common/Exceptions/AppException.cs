using System;

namespace AspNetCoreWebApi.Application.Common.Exceptions
{
    public class AppException : Exception
    {
        public AppException()
        {
        }
        public AppException(string businessMessage)
               : base(businessMessage)
        {
        }
    }
}
