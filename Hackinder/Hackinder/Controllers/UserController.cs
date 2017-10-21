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

        [HttpPost]
        public void Create(CreateUserDto request)
        {
            _userService.CreateUser(request);
        }

        [HttpPut]
        public async Task Update(string userId, [FromBody] UpdateUserDto request)
        {
            await _userService.UpdateUser(userId, request);
        }

        [HttpPost]
        [Route("{userId}/settings")]
        public async Task UpdateSettings(string userId, [FromBody]Settings request)
        {
            await _userService.UpdateSettings(userId, request);
        }
    }
}