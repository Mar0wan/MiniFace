using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Helper.Response
{
    public class ErrorResponse
    {
        public string ErrorMessage { get; set; }
        public List<ResponseMessage> ErrorMessages{ get; set; }
        public HttpStatusCode Status { get; set; } = HttpStatusCode.InternalServerError;
        public bool IsError { get; set; } = true;

        public ErrorResponse(string errorMessage, HttpStatusCode status)
        {
            ErrorMessage = errorMessage;
            Status = status;
        }

        public ErrorResponse(List<ResponseMessage> errormessages, HttpStatusCode status)
        {
                ErrorMessages = errormessages;
                Status = status;
        }

        public ErrorResponse(string errorMessage, List<ResponseMessage> errormessages, HttpStatusCode status)
        {
            ErrorMessage = errorMessage;
            ErrorMessages = errormessages;
            Status = status;
        }
            
    }
}
