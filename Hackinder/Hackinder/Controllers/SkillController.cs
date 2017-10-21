using System.Collections.Generic;
using System.Threading.Tasks;
using Hackinder.DB;
using Hackinder.Entities;
using Hackinder.Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Hackinder.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    public class SkillController : Controller
    {
        private readonly DbConnector _connector;

        public SkillController(DbConnector connector)
        {
            _connector = connector;
        }

        [Route("skills")]
        [HttpGet]
        public  List<Skill> Get()
        {
            return  _connector.Skills.Find(x => true).ToList();
        }

        [Route("skill")]
        [HttpPost]
        public void Post(AddSkillDto skill)
        {
            if (_connector.Skills.Find(x => x.Name == skill.Name).FirstOrDefault() == null)
                _connector.Skills.InsertOne(new Skill { Name = skill.Name, Count = 1 });
            else
                _connector.Skills.UpdateOne(x => x.Name == skill.Name, Builders<Skill>.Update.Inc(x => x.Count, 1));

        }
    }
}
