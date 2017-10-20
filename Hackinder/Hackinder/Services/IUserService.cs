using System.Threading.Tasks;
using Hackinder.Entities;
using Hackinder.Entities.Dto;

namespace Hackinder.Services
{
    public interface IUserService
    {
        Task CreateUser(CreateUserDto request);
        Task UpdateUser(UpdateUserDto request);
        Task UpdateSettings(int userId, Settings request);
    }

    public class UserService : IUserService
    {
        public Task CreateUser(CreateUserDto request)
        {
            throw new System.NotImplementedException();
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
