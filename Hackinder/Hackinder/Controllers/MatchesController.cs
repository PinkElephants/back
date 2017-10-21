using System.Collections.Generic;
using System.Linq;
using Hackinder.Application;
using Hackinder.DB;
using Hackinder.Entities;
using Hackinder.Entities.Dto;
using Hackinder.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

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
            //return _matchService.GetMatches("667976");

        }

        [Route("match")]
        [HttpPost]
        public void Post([FromBody]CreateMatchDto dto)
        {
           
            
            if (dto.isLike)
            {
                _connector.Men.UpdateOne(x => x.UserId == HttpContext.GetViewerId(),
                    Builders<Man>.Update.AddToSet(x => x.Matched, dto.user_id));
                _connector.Men.UpdateOne(x => x.UserId == dto.user_id,
                    Builders<Man>.Update.AddToSet(x => x.MatchedMe, HttpContext.GetViewerId()));
            }
            else
            {
                _connector.Men.UpdateOne(x => x.UserId == HttpContext.GetViewerId(),
                    Builders<Man>.Update.AddToSet(x => x.Dismatched, dto.user_id));
            }
        }

        [Route("matches")]
        [HttpGet]
        public List<MatchDto> GetMatches()
        {
            var man = _connector.Men.Find(x => x.UserId == HttpContext.GetViewerId()).First();
            return _connector.Men.Find(x => man.Matched.Contains(x.UserId)).ToList().Select(x => new MatchDto
            {
                skills = x.Skills,
                idea = x.Idea,
                user_id = x.UserId,
                summary = x.Summary
            }).ToList();
        }

    }
}
