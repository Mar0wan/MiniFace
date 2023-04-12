using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace GenericException
{
    public class BusinessException : Exception
    {
        public string ErrorMessage { get; set; }
        public HttpStatusCode Status { get; set; } = HttpStatusCode.InternalServerError;
        public BusinessException()
        {

        }
        public BusinessException(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
        public BusinessException(string errorMessage, HttpStatusCode status)
        {
            ErrorMessage = errorMessage;
            Status = status;
        }
    }
}
