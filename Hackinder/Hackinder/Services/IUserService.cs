using System.Threading.Tasks;
using Hackinder.Entities;
using Hackinder.Entities.Dto;

namespace Hackinder.Services
{
    public interface IUserService
    {
        Man GetUser(string userId);
        Task CreateUser(string userId, CreateUserDto request);
        Task UpdateSettings(string userId, Settings request);
    }
}
