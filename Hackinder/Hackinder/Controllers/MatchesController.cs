using System.Collections.Generic;
using Hackinder.Application;
using Hackinder.DB;
using Hackinder.Entities.Dto;
using Hackinder.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hackinder.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    //[Authorize(AuthenticationSchemes = VkAuthCodeAuthenticationOptions.AuthSchemeName)]
    public class MatchesController : Controller
    {
        private DbConnector _connector;
        private readonly MatchService _matchService;


        public MatchesController(DbConnector connector, MatchService matchService)
        {
            _connector = connector;
            _matchService = matchService;
        }

        [Route("newmatches")]
        [HttpGet]
        public List<NewMatchDto> Get()
        {
            return _matchService.GetMatches(HttpContext.GetViewerId());

        }

        [Route("match")]
        [HttpGet]
        public void Post(CreateMatchDto dto)
        {
            //var man = await _userManager.GetUserAsync(HttpContext.User);
            //_connector.
        }


        //[Route("matches")]
        //[HttpGet]
        //public List<MatchDto> Get()
        //{
        //    return null;
        //}




    }
}
