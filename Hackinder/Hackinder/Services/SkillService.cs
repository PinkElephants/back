using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hackinder.DB;
using Hackinder.Entities;
using MongoDB.Driver;

namespace Hackinder.Services
{
    public class SkillService
    {
        private readonly DbConnector _connector;
        public SkillService(DbConnector connector)
        {
            _connector = connector;
        }

        public void UpdateSkill(string skill)
        {
            skill = skill.Trim().ToLower();
            if (_connector.Skills.Find(x => x.Name == skill).FirstOrDefault() == null)
                _connector.Skills.InsertOne(new Skill { Name = skill, Count = 1 });
            else
                _connector.Skills.UpdateOne(x => x.Name == skill, Builders<Skill>.Update.Inc(x => x.Count, 1));
        }
    }
}
