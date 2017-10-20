using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Hackinder.Application
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = new InternalServerErrorObjectResult(context.Exception.Message);
        }
    }

    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object value)
            : base(value)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }
}
