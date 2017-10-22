using System.Linq;
using System.Threading.Tasks;
using Hackinder.DB;
using Hackinder.Entities;
using Hackinder.Entities.Dto;
using MongoDB.Driver;

namespace Hackinder.Services
{
    public class UserService : IUserService
    {
        private readonly DbConnector _connector;
        private readonly SkillService _skillService;

        public UserService(DbConnector connector, SkillService skillService)
        {
            _connector = connector;
            _skillService = skillService;
        }

        public Man GetUser(string userId)
        {
            return _connector.Men.Find(x => x.UserId == userId).FirstOrDefault();
        }

        public async Task<Man> CreateUser(string userId, CreateUserDto request)
        {
            var user = _connector.Men.Find(x => x.UserId == userId).FirstOrDefault();
            if (user == null)
            {
                var createdUser = new Man
                {
                    UserId = userId
                };
                await _connector.Men.InsertOneAsync(createdUser); ;
            }

            _skillService.UpdateSkillz(request.Skills.ToArray());
            await _connector.Men.UpdateOneAsync(x => x.UserId == userId,
                Builders<Man>.Update
                    .Set(x => x.Idea, request.Idea)
                    .Set(x => x.Skills, request.Skills)
                    .Set(x => x.LowerSkills, request.Skills.Select(x => x.ToLower().Trim()))
                    .Set(x => x.Summary, request.Summary)
            );

            return user;
        }


        public async Task UpdateSettings(string userId, Settings request)
        {
            await _connector.Men.UpdateOneAsync(x => x.UserId == userId,
                Builders<Man>.Update
                    .Set(x => x.Settings, request)
            );
        }
    }
}