using H6_ChicBotique.DTOs;

namespace H6_ChicBotique.Services
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetAll();
    }

    public class UserService : IUserService
    {
        public Task<List<UserResponse>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
