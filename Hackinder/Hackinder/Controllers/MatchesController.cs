using System;
using System.Collections.Generic;
using System.Linq;
using Hackinder.Application;
using Hackinder.DB;
using Hackinder.Entities;
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

        public MatchesController(DbConnector connector)
        {
            _connector = connector;
        }

        [Route("newmatches")]
        [HttpGet]
        public List<NewMatchDto> Get(CreateNewmatchesDto dto)
        {
            return MockMatch();
            int total = 10;
            var result = new List<NewMatchDto>();
            var man = _connector.Men.Find(x => x.Id == HttpContext.GetViewerId()).First();
            if (man.MatchedMe.Count >0)
            {
                var wantMe = man.MatchedMe.Where(x => !man.Dismatched.Contains(x)).Take(total/ 2);
                
                result.AddRange(_connector.Men.Find(x => wantMe.Contains(x.Id)).ToList().Select(x => new NewMatchDto()
                {
                    
                }));
                total -= result.Count;
            }
            //_connector.Men

            return null;
        }

        [Route("match")]
        [HttpGet]
        public async void Post(CreateMatchDto dto)
        {
            //var man = await _userManager.GetUserAsync(HttpContext.User);
            //_connector.
        }


        [Route("matches")]
        [HttpGet]
        public List<MatchDto> Get()
        {
            return null;
        }


        public List<NewMatchDto> MockMatch()
        {
            var rnd = new Random();
            var mock = new List<NewMatchDto>();
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
                    new NewMatchDto
                    {
                        skills = new List<string> { ".Net", "JS", "aNgUlAr25", "вова пидор" },
                        idea = " давайте называть вову пидором",
                        isMatch = rnd.Next(0, 1) > 0,
                        summary = "рукожопый мудак",
                        user_id = id.ToString()
                    }
                );
            }
            return mock;
        }


    }

    //public class FindClosest
    //{
    //    private DbConnector _connector;

    //    public FindClosest(DbConnector connector)
    //    {
    //        _connector = connector;
    //    }

    //    public List<Man> Find(List<string> skills,int count)
    //    {
    //        skills = skills.Select(x => x.ToLower()).Distinct().ToList();
    //        skills = _connector.Skills
    //                           .Find(x => skills.Contains(x.Name))
    //                           .ToList()
    //                           .Where(x => x.Count > 1)
    //                           .OrderByDescending(x => x.Count)
    //                           .Select(x => x.Name)
    //                           .ToList();

    //        var result = new List<Man>();

    //        while ((int)Math.Log(skills.Count, 2) != 0 && result.Count < count)
    //        {
    //            _connector.Men.Find(x => skillInfo. x.LowerSkills.Contains())
    //        }

    //    }
    //}

    public class CreateNewmatchesDto
    {
        
    }

    public class NewMatchDto
    {
        public string user_id { get; set; }
        public List<string> skills { get; set; }
        public string summary { get; set; }
        public string idea { get; set; }
        public bool isMatch { get; set; }
    }
    public class MatchDto
    {

    }

    public class CreateMatchDto
    {
        public bool Success { get; set; }

        public string ManId { get; set; }
    }
  







}
