using H6_ChicBotique.DTOs;

namespace H6_ChicBotique.Services
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetAll();
        Task<UserResponse> GetById(int UserId);
        
        Task<UserResponse> GetIdByEmail(string email);
    }

    public class UserService : IUserService
    {
        public Task<List<UserResponse>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<UserResponse> GetById(int UserId)
        {
            throw new NotImplementedException();
        }

        public Task<UserResponse> GetIdByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
