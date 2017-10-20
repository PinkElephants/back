using System.Threading.Tasks;
using Hackinder.Entities;
using Hackinder.Entities.Dto;

namespace Hackinder.Services
{
    public interface IUserService
    {
        void CreateUser(CreateUserDto request);
        Task UpdateUser(UpdateUserDto request);
        Task UpdateSettings(int userId, Settings request);
    }
}
