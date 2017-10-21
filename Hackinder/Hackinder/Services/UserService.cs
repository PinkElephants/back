using System;
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

        public void CreateUser(CreateUserDto request)
        {
            var user = _connector.Men.Find(x => x.Id == request.Id).FirstOrDefault();
            if (user != null)
                throw new ArgumentException("User exists");

            var createdUser = new Man
            {
                Id = request.Id,
                BirthDate = request.BirthDate
            };
            _connector.Men.InsertOne(createdUser);
        }

        public async Task UpdateUser(string userId, UpdateUserDto request)
        {
            await _connector.Men.UpdateOneAsync(x => x.Id == userId,
                Builders<Man>.Update
                    .Set(x => x.Idea, request.Idea)
                    .Set(x => x.Skills, request.Skills)
                    .Set(x => x.Specializations, request.Specializations)
            );
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