using System.Threading.Tasks;
using Hackinder.Entities;
using Hackinder.Entities.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Hackinder.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    public class UserController : Controller
    {
        [HttpPost]
        public async Task Create(CreateUserDto request)
        {
            await Task.Delay(123);
        }

        [HttpPut]
        public async Task Create(UpdateUserDto request)
        {
            await Task.Delay(123);
        }

        [HttpPost]
        [Route("{userId}/settings")]
        public async Task UpdateSettings(int userId, [FromBody]Settings request)
        {
            await Task.Delay(123);
        }
    }
}