using System.Collections.Generic;
using System.Threading.Tasks;
using Hackinder.DB;
using Hackinder.Entities;
using Hackinder.Entities.Dto;
using Hackinder.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Hackinder.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    public class SkillController : Controller
    {
        private readonly DbConnector _connector;
        private readonly SkillService _skillService;

        public SkillController(DbConnector connector, SkillService skillService)
        {
            _connector = connector;
            _skillService = skillService;
        }

        [Route("skills")]
        [HttpGet]
        public  List<Skill> Get()
        {
            return  _connector.Skills.Find(x => true).ToList();
        }

        //[Route("skill")]
        //[HttpPost]
        //public void Post(AddSkillDto skill)
        //{
        //    _skillService.UpdateSkill(skill.Name);
        //}
    }
}
