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

        public UserService(DbConnector connector)
        {
            _connector = connector;
        }

        public Man GetUser(string userId)
        {
            return _connector.Men.Find(x => x.Id == userId).FirstOrDefault();
        }

        public async Task CreateUser(string userId, CreateUserDto request)
        {
            var user = _connector.Men.Find(x => x.Id == userId).FirstOrDefault();
            if (user != null)
            {
                await _connector.Men.UpdateOneAsync(x => x.Id == userId,
                    Builders<Man>.Update
                        .Set(x => x.Idea, request.Idea)
                        .Set(x => x.Skills, request.Skills)
                        .Set(x => x.Summary, request.Summary)
                );
                return;
            }

            var createdUser = new Man
            {
                Id = userId,
                BirthDate = request.BirthDate
            };
            await _connector.Men.InsertOneAsync(createdUser);
        }


        public async Task UpdateSettings(string userId, Settings request)
        {
            await _connector.Men.UpdateOneAsync(x => x.Id == userId,
                Builders<Man>.Update
                    .Set(x => x.Settings, request)
            );
        }
    }
}