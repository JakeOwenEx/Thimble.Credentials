using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Thimble.Credentials.Controllers.Exceptions;

namespace Thimble.Credentials.Controllers.Authorization
{
    public class AuthorizationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var Configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var authHeader = context.HttpContext.Request.Headers.Where(x => x.Key == "Authorization").ToList();
            if (!authHeader.Any()) 
                throw new AuthHeaderRequiredException();
            if (authHeader.First(x => x.Key == "Authorization").Value != Configuration["apiKey"])
                throw new InvalidAuthHeaderException();
            base.OnActionExecuting(context);
        }
    }
}