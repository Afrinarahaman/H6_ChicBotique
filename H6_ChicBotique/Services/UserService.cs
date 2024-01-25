using H5_Webshop.Database.Entities;
using H6_ChicBotique.DTOs;
using H6_ChicBotique.Repositories;

namespace H6_ChicBotique.Services
{
    // Interface definition for user service
    public interface IUserService
    {
        Task<List<UserResponse>> GetAll(); // Method to retrieve all users as UserResponse objects
        Task<UserResponse> GetById(int UserId); // Method to retrieve a user by ID as a UserResponse object
        Task<UserResponse> GetIdByEmail(string email); // Method to retrieve a user by email as a UserResponse object
    }

    // Implementation of IUserService interface in UserService class
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        // Constructor with dependency injection for IUserRepository
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Implementation of GetAll method
        public async Task<List<UserResponse>> GetAll()
        {
            // Retrieve all users from the repository
            List<User> users = await _userRepository.GetAll();

            // If users are not null, map each user to a UserResponse object
            return users == null ? null : users.Select(u => new UserResponse
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Role = u.Role
            }).ToList();
        }

        // Implementation of GetById method
        public async Task<UserResponse> GetById(int UserId)
        {
            // Retrieve a specific user by ID from the repository
            User User = await _userRepository.GetById(UserId);

            // If the user is not null, map the user to a UserResponse object
            if (User != null)
            {
                return MapUserToUserResponse(User);
            }

            return null; // Return null if the user is not found
        }

        // Implementation of GetIdByEmail method
        public async Task<UserResponse> GetIdByEmail(string email)
        {
            // Retrieve a specific user by email from the repository
            User User = await _userRepository.GetByEmail(email);

            // If the user is not null, map the user to a UserResponse object
            if (User != null)
            {
                return MapUserToUserResponse(User);
            }

            return null; // Return null if the user is not found
        }

        // Private method to map a User object to a UserResponse object
        private static UserResponse MapUserToUserResponse(User user)
        {
            UserResponse response = new UserResponse();

            // If the user is not null, map relevant properties to the UserResponse object
            if (user != null)
            {
                response.Id = user.Id;
                response.Email = user.Email;
                response.FirstName = user.FirstName;
                response.LastName = user.LastName;
                response.Role = user.Role;

                // Create an AccountInfoResponse object within UserResponse
                response.Account = new AccountInfoResponse
                {
                    Id = user.Account.Id,
                };
            }

            return response; // Return the mapped UserResponse object
        }
    }
}




