﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hackinder.DB;
using Hackinder.Entities.Dto;
using Hackinder.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Hackinder.Controllers
{
    [Produces("application/json")]
    [Route("api/mock")]
    public class MockController : Controller
    {
        private readonly IUserService _userService;
        private readonly MatchService _matchService;
        private readonly DbConnector _connector;

        public MockController(IUserService userService, MatchService matchService, DbConnector connector)
        {
            _userService = userService;
            _matchService = matchService;
            _connector = connector;
        }
        

        [HttpGet]
        [Route("new")]
        public async Task Get()
        {
            _connector.GetDB().DropCollection(_connector.Men.CollectionNamespace.CollectionName);
            _connector.GetDB().DropCollection(_connector.Skills.CollectionNamespace.CollectionName);
                var mocks = _matchService.MockMatch();

            foreach (var mock in mocks)
            {
                await _userService.CreateUser(mock.user_id, new CreateUserDto()
                {
                    Summary = mock.summary,
                    Skills = mock.skills,
                    Idea = mock.idea
                });
            }

        }
    }
}