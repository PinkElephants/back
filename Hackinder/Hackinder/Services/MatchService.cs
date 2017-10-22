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
            //return MockMatch();
            int total = 10;
            var result = new List<NewMatchDto>();
            var man = _connector.Men.Find(x => x.UserId == userId).First();
            var dontMatch = man.Dismatched.ToArray().ToList();
            dontMatch.Add(man.UserId);
            dontMatch.AddRange(man.Matched);

            if (man.MatchedMe.Count > 0)
            {
                var wantMe = man.MatchedMe.Where(x => !dontMatch.Contains(x)).Take(total / 3);

                result.AddRange(_connector.Men
                    .Find(x => wantMe.Contains(x.UserId))
                    .ToList()
                    .Select(x => new NewMatchDto
                    {
                        skills = x.Skills,
                        idea = x.Idea,
                        isMatch = true,
                        user_id = x.UserId,
                        summary = x.Summary
                    }));
                total -= result.Count;
                dontMatch.AddRange(result.Select(x => x.user_id));
            }
            result.AddRange(FindMatch(man.LowerSkills, total, dontMatch));

            return result;
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
                41835964
                
            };
            foreach (var id in ids)
            {
                mock.Add(
                    new NewMatchDto
                    {
                        skills = new List<string> { ".Net", "JS", "aNgUlAr" },
                        idea = "",
                        isMatch = rnd.Next(0, 1) > 0,
                        summary = "Fullstack",
                        user_id = id.ToString()
                    }
                );
            }
            mock.Add(
                new NewMatchDto
                {
                    skills = new List<string> { "XSLT", "JAVA", ".Net", "sql" },
                    idea = "приложение для музея",
                    isMatch = rnd.Next(0, 1) > 0,
                    summary = "Backend developer",
                    user_id = 10155845.ToString()
                }
            );
            mock.Add(
                new NewMatchDto
                {
                    skills = new List<string> { "XSLT", "JS", "css" },
                    idea = "приложение для буллинга",
                    isMatch = rnd.Next(0, 1) > 0,
                    summary = "Frontend developer",
                    user_id = 5134860.ToString()
                }
            );
            mock.Add(
                new NewMatchDto
                {
                    skills = new List<string> { "sql", "JS", "React" },
                    idea = "приложение для хакатона",
                    isMatch = rnd.Next(0, 1) > 0,
                    summary = "Frontend-db developer",
                    user_id = 103296.ToString()
                }
            );
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


            var closeSkills = orderedSkills.ToArray();
            while ((int)Math.Log(closeSkills.Length, 2) > 0 && result.Count < count)
            {
                var close = _connector.Men.Find(x => !dontMatch.Contains(x.UserId) && closeSkills.All(s => x.LowerSkills.Contains(s))).ToList();
                result.AddRange(close);
                dontMatch.AddRange(close.Select(x => x.UserId));
                closeSkills = closeSkills.Take(closeSkills.Length / 2).ToArray();
            }
            if (orderedSkills.Count != 0 && result.Count < count)
            {
                var rnd = new Random();
                var skill = orderedSkills[rnd.Next(0, orderedSkills.Count)];
                var randomSkilled = _connector.Men.Find(x => !dontMatch.Contains(x.UserId) && x.LowerSkills.Contains(skill),
                    new FindOptions { BatchSize = count - result.Count }).ToList();
                dontMatch.AddRange(randomSkilled.Select(x => x.UserId));
                result.AddRange(randomSkilled);
            }
            if (result.Count < count)
            {
                var any = _connector.Men.Find(x => !dontMatch.Contains(x.UserId), new FindOptions { BatchSize = count - result.Count }).ToList();
                result.AddRange(any);
            }

            return result.Select(x => new NewMatchDto()
            {
                idea = x.Idea,
                skills = x.Skills,
                summary = x.Summary,
                user_id = x.UserId,
                isMatch = false
            }).ToList();

        }
    }
}
