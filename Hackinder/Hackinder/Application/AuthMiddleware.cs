using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Hackinder.DB;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Hackinder.Application
{
    public static class AuthenticationBuilderExtensions
    {
        public static void AddVkAuthCode(this AuthenticationBuilder builder)
        {
            builder.AddScheme<VkAuthCodeAuthenticationOptions, VkAuthCodeAuthenticationHandler>(
                VkAuthCodeAuthenticationOptions.AuthSchemeName,
                VkAuthCodeAuthenticationOptions.AuthSchemeName, null
            );
        }
    }


    public class VkAuthCodeAuthenticationOptions : AuthenticationSchemeOptions
    {
        public string AuthCodeHeaderName => "AuthCode";
        public string ViewerIdHeaderName => "ViewerId";

        public string ApiId => "6227851";
        public string ApiSecret => "4fKP3l1muJFszTzHBTMY";

        public const string AuthSchemeName = "authCode";
    }

    public class VkAuthCodeAuthenticationHandler : AuthenticationHandler<VkAuthCodeAuthenticationOptions>
    {
        private readonly VkAuthCodeAuthenticationOptions _options;
        private readonly DbConnector _connector;

        public VkAuthCodeAuthenticationHandler(
            IOptionsMonitor<VkAuthCodeAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock, 
            DbConnector connector) : base(options, logger, encoder, clock)
        {
            _connector = connector;
            _options = options.CurrentValue;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
           
            if (!Context.Request.Cookies.TryGetValue(_options.AuthCodeHeaderName, out var authCode))
            {
                return AuthenticateResult.Fail("Missing or malformed 'AuthCode' header");
            }

            if (!Context.Request.Cookies.TryGetValue(_options.ViewerIdHeaderName, out var viewerId))
            {
                return AuthenticateResult.Fail("Missing or malformed 'ViewerId' header");
            }

            var user = await _connector.Mans.FindAsync(x => x.Id == viewerId);
            if (user == null)
            {
                return AuthenticateResult.Fail("User doesn't exist");
            }


            var calculatedAuthKey = CreateMd5(_options.ApiId + '_' + viewerId + '_' + _options.ApiSecret).ToUpper();
            if (calculatedAuthKey != authCode.ToString().ToUpper())
            {
                return AuthenticateResult.Fail("Auth doesn't match");
            }

            // success! Now we just need to create the auth ticket
            var identity = new ClaimsIdentity("authCode"); // the name of our auth scheme
            // you could add any custom claims here
            var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), null, "authCode");
            return AuthenticateResult.Success(ticket);
        }

        private string CreateMd5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                var inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                var hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                var sb = new StringBuilder();
                foreach (var t in hashBytes)
                {
                    sb.Append(t.ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}