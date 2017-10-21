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

        public void UpdateSkillz(params string[] skillz)
        {

            foreach (var skill in skillz)
            {
                var trimmed = skill.Trim().ToLower();
                if (_connector.Skills.Find(x => x.Name == trimmed).FirstOrDefault() == null)
                    _connector.Skills.InsertOne(new Skill { Name = trimmed, Count = 1 });
                else
                    _connector.Skills.UpdateOne(x => x.Name == trimmed, Builders<Skill>.Update.Inc(x => x.Count, 1));
            }
        }
    }
}
