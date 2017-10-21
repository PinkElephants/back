using System.Security.Authentication;
using System.Threading.Tasks;
using Hackinder.Application;
using Hackinder.Entities;
using Hackinder.Entities.Dto;
using Hackinder.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hackinder.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    //[Authorize(AuthenticationSchemes = VkAuthCodeAuthenticationOptions.AuthSchemeName)]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public Man Get()
        {
            var user = _userService.GetUser(HttpContext.GetViewerId());
            if (user == null)
                throw new AuthenticationException();
            return user;
        }

        [HttpPost]
        public async Task Create(CreateUserDto request)
        {
            await _userService.CreateUser(HttpContext.GetViewerId(), request);
        }


        [HttpPost]
        [Route("{userId}/settings")]
        public async Task UpdateSettings(string userId, [FromBody]Settings request)
        {
            await _userService.UpdateSettings(userId, request);
        }
    }
}