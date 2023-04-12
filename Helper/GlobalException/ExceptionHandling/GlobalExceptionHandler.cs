using Helper.Response;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GenericException.ExceptionHandling
{
    public class GlobalExceptionHandler
    {
        //private readonly ILoggerManager logger;
        private readonly RequestDelegate next;
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(RequestDelegate next ,ILogger<GlobalExceptionHandler> logger)
        {
            this.next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            ErrorResponse response;
            HttpStatusCode status = HttpStatusCode.InternalServerError;

            if (ex is BusinessException)
            {
                var exception = (BusinessException)ex;
                response = new ErrorResponse(exception.ErrorMessage, exception.Status);
            }
            else
            {
                response = new ErrorResponse(ex.Message, status);
            }
            var feature = context.Features.Get<IExceptionHandlerFeature>();
            if(feature.Error is not null)
            {
                _logger.LogError(feature.Error, "Exception happened");
                var result = JsonConvert.SerializeObject(response);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)status;
                await context.Response.WriteAsync(result);

            }
        }


    }
}
