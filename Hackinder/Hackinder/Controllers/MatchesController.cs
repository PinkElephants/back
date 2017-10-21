﻿using System.Collections.Generic;
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

        }

        [Route("match")]
        [HttpGet]
        public void Post(CreateMatchDto dto)
        {
           
            
            if (dto.Success)
            {
                _connector.Men.UpdateOne(x => x.Id == HttpContext.GetViewerId(),
                    Builders<Man>.Update.AddToSet(x => x.Matched, dto.ManId));
            }
            else
            {
                _connector.Men.UpdateOne(x => x.Id == HttpContext.GetViewerId(),
                    Builders<Man>.Update.AddToSet(x => x.Dismatched, dto.ManId));
            }
        }

        [Route("matches")]
        [HttpGet]
        public List<MatchDto> Get()
        {

            var mock = new List<MatchDto>();
            var ids = new[]
            {
                16172513,
                8644959,
                20142331,
                41835964,
                10155845,
                5134860,
                103296
            };
            foreach (var id in ids)
            {
                mock.Add(
                    new MatchDto
                    {
                        skills = new List<string> { ".Net", "JS", "aNgUlAr25", "вова пидор" },
                        idea = " давайте называть вову пидором",
                        summary = "рукожопый мудак",
                        user_id = id.ToString()
                    }
                );
            }
            return mock;
        }

    }
}
