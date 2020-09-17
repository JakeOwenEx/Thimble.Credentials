using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Thimble.Credentials.AWS.Dynamo.Credentials.Exceptions;
using Thimble.UserAccount.Middleware.ErrorHandling.ErrorResponses;

namespace Thimble.Credentials.Middleware.ErrorHandling
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.Clear();
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var responseObject = new ErrorResponse { Message = ex.Message };

                if (ex.GetType() == typeof(InvalidEmailOrPasswordException)) 
                    SetResponse(StaticErrorResponses.Unauthorized, context, responseObject);

                var serializedBody = JsonConvert.SerializeObject(responseObject, Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented,
                        DateFormatHandling = DateFormatHandling.IsoDateFormat,
                        NullValueHandling = NullValueHandling.Ignore,
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });
                
                await context.Response.WriteAsync(serializedBody, Encoding.UTF8);
            }
        }

        private void SetResponse(BaseErrorResponse errorResponse, HttpContext context, ErrorResponse responseObject)
        {
            context.Response.StatusCode = errorResponse.StatusCode;
            responseObject.Message = errorResponse.Message;
        }
    }
}