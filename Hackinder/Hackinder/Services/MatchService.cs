using System;
using System.Collections.Generic;
using System.Linq;
using Hackinder.Controllers;
using Hackinder.DB;
using Hackinder.Entities;
using Hackinder.Entities.Dto;
using MongoDB.Driver;

namespace Hackinder.Services
{
    public class MatchService
    {
        private DbConnector _connector;

        public MatchService(DbConnector connector)
        {
            _connector = connector;
        }

        public List<NewMatchDto> GetMatches(string userId)
        {
            return MockMatch();
            int total = 10;
            var result = new List<NewMatchDto>();
            var man = _connector.Men.Find(x => x.Id == userId).First();
            var dontMatch = man.Dismatched;

            if (man.MatchedMe.Count > 0)
            {
                var wantMe = man.MatchedMe.Where(x => !dontMatch.Contains(x)).Take(total / 3);

                result.AddRange(_connector.Men
                    .Find(x => wantMe.Contains(x.Id))
                    .ToList()
                    .Select(x => new NewMatchDto
                    {
                        skills = x.Skills,
                        idea = x.Idea,
                        isMatch = true,
                        user_id = x.Id,
                        summary = x.Summary
                    }));
                total -= result.Count;
                dontMatch.AddRange(result.Select(x => x.user_id));
            }
            result.AddRange(FindMatch(man.LowerSkills, total, dontMatch));

            return result;
        }

        private List<NewMatchDto> MockMatch()
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

        private List<NewMatchDto> FindMatch(List<string> skills, int count, List<string> dontMatch)
        {
            skills = skills.Select(x => x.ToLower()).Distinct().ToList();
            var orderedSkills = skills = _connector.Skills
                .Find(x => skills.Contains(x.Name))
                .ToList()
                .Where(x => x.Count > 1)
                .OrderByDescending(x => x.Count)
                .Select(x => x.Name)
                .ToList();

            var result = new List<Man>();

            while ((int)Math.Log(skills.Count, 2) != 0 && result.Count < count)
            {
                var close = _connector.Men.Find(x => !dontMatch.Contains(x.Id) && skills.All(s => x.LowerSkills.Contains(s))).ToList();
                result.AddRange(close);
                dontMatch.AddRange(close.Select(x => x.Id));
            }
            if (result.Count < count)
            {
                var rnd = new Random();
                var skill = orderedSkills[rnd.Next(0, orderedSkills.Count)];
                var randomSkilled = _connector.Men.Find(x => !dontMatch.Contains(x.Id) && x.LowerSkills.Contains(skill),
                    new FindOptions { BatchSize = count - result.Count }).ToList();
                dontMatch.AddRange(randomSkilled.Select(x => x.Id));
                result.AddRange(randomSkilled);
            }
            if (result.Count < count)
            {
                var any = _connector.Men.Find(x => !dontMatch.Contains(x.Id), new FindOptions { BatchSize = count - result.Count }).ToList();
                result.AddRange(any);
            }

            return result.Select(x => new NewMatchDto()
            {
                idea = x.Idea,
                skills = x.Skills,
                summary = x.Summary,
                user_id = x.Id,
                isMatch = false
            }).ToList();

        }
    }
}
