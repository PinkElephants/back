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
            var user = _connector.Mans.Find(x => x.Id == request.Id).FirstOrDefault();
            if (user != null)
                throw new ArgumentException("User exists");

            var createdUser = new Man
            {
                Id = request.Id,
                BirthDate = request.BirthDate
            };
            _connector.Mans.InsertOne(createdUser);
        }

        public Task UpdateUser(UpdateUserDto request)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateSettings(int userId, Settings request)
        {
            throw new System.NotImplementedException();
        }
    }
}