using System.Collections.Generic;
using System.Threading.Tasks;
using Hackinder.DB;
using Hackinder.Entities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Hackinder.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    public class SkillController : Controller
    {
        private DbConnector _connector;

        public SkillController(DbConnector connector)
        {
            _connector = connector;
        }

        [Route("skills")]
        public  List<Skill> Get()
        {
            return  _connector.Skills.Find(x => true).ToList();
        }

        [Route("skill")]
        public void Post(AddSkillRequest skill)
        {
            if (_connector.Skills.Find(x => x.Name == skill.Name).FirstOrDefault() == null)
                _connector.Skills.InsertOne(new Skill { Name = skill.Name, Count = 1 });
            else
                _connector.Skills.UpdateOne(x => x.Name == skill.Name, Builders<Skill>.Update.Inc(x => x.Count, 1));

        }
    }

    public class AddSkillRequest
    {
        public string Name { get; set; }
    }
}
