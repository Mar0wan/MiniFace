using Helper.enums;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Helper.Response
{
    public class GenericResponse
    {
        public string Message { get; set; }
        public HttpStatusCode Status { get; set; } = HttpStatusCode.OK;
        public object Data { get; set; }
        public Result Result { get; set; }


        public GenericResponse(string message)
        {
            Message = message;
        }
        public GenericResponse(string message, object data)
        {
            Message = message;
            Data = data;
        }
        public GenericResponse(string message, object data, HttpStatusCode status)
        {
            Message = message;
            Data = data;
            Status = status;
        }

        public GenericResponse(string message, Result result)
        {
            Message = message;
            Result = result;
        }
    }
    public class ResponseMessage
    {
        public string Key { get; set; }
        public string Message { get; set; }
    }
}
