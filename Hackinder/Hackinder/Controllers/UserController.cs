using System.Threading.Tasks;
using Hackinder.Entities;
using Hackinder.Entities.Dto;
using Hackinder.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hackinder.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task Create(CreateUserDto request)
        {
            await _userService.CreateUser(request);
        }

        [HttpPut]
        public async Task Update(UpdateUserDto request)
        {
            await _userService.UpdateUser(request);
        }

        [HttpPost]
        [Route("{userId}/settings")]
        public async Task UpdateSettings(int userId, [FromBody]Settings request)
        {
            await _userService.UpdateSettings(userId, request);
        }
    }
}