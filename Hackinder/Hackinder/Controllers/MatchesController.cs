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
        [HttpPost]
        public List<NewMatchDto> Post(CreateNewmatchesDto dto)
        {
            var result = new List<NewMatchDto>();
            var man = _connector.Men.Find(x => x.Id == HttpContext.GetViewerId()).First();
            if (man.MatchedMe.Count >0)
            {
                var wantMe = man.MatchedMe.Where(x => !man.Dismatched.Contains(x));
                
                result.AddRange(_connector.Men.Find(x => wantMe.Contains(x.Id)).ToList().Select(x => new NewMatchDto()
                {
                    VkId = x.Id
                }));
            }
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


    }

    public class CreateNewmatchesDto
    {
        
    }
    
    public class NewMatchDto
    {
        public string VkId { get; set; }
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
