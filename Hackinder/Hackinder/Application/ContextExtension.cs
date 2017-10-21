using Microsoft.AspNetCore.Http;

namespace Hackinder.Application
{
    public static class ContextExtension
    {
        public static string GetViewerId(this HttpContext context)
        {
            return context.Request.Headers[VkAuthCodeAuthenticationOptions.ViewerIdHeaderName];
        }
    }
}