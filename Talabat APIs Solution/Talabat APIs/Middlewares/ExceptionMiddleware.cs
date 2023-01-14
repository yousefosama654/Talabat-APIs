using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat_APIs.Errors;

namespace Talabat_APIs.Middlewares
{
    public class ExceptionMiddleware
    {
        public RequestDelegate Next { get; }
        public ILogger<ExceptionMiddleware> Logger { get; }
        public IWebHostEnvironment Env { get; }
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment env)
        {
            Next = next;
            Logger = logger;
            Env = env;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await Next.Invoke(context); //trying to move to a next middleware
            }
            catch (Exception ex)
            {
                Logger.LogError(string.Empty, ex.Message);
                var errorResponse = Env.IsDevelopment() ?
                    new ApiExceptionError(500, ex.StackTrace.ToString(), ex.Message)
                    : new ApiExceptionError(500);
                // to handle the response type and format

                //determine the content type of response
                context.Response.ContentType = "application/json";
                //determine the StatusCode of response
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                // to make options to the json format
                var Jsonoptions = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                // to serilize the text into json
                var jsonResponse = JsonSerializer.Serialize(errorResponse, Jsonoptions);
                //to overwrite the response  
                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}
