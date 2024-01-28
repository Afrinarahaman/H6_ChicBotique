using H5_Webshop.Database.Entities;
using H6_ChicBotique.Database;
using Microsoft.EntityFrameworkCore;

namespace H6_ChicBotique.Repositories
{
    //Creating Interface of IUserRepository
    public interface IUserRepository
    {
        Task<List<User>> SelectAll();      
        Task<User> SelectByEmail(string email);
        Task<User> SelectById(int userId);
      

    }
    // Implementation of IUserRepository interface in UserRepository class
    public class UserRepository : IUserRepository
    {
        private readonly ChicBotiqueDatabaseContext _context; // Instance of ChicBotiqueDatabaseContext class

        // Constructor with dependency injection
        public UserRepository(ChicBotiqueDatabaseContext context)
        {
            _context = context;
        }

        // Implementation of <selectAll method
        public async Task<List<User>> SelectAll()
        {
            // Retrieve all users from the database
            return await _context.User.ToListAsync();
        }

        // Implementation of SelectById method
        public async Task<User> SelectById(int userId)
        {
            // Retrieve a specific user based on user ID
            return await _context.User.FirstOrDefaultAsync(u => u.Id == userId);
        }

        // Implementation of SelectByEmail method
        public async Task<User> SelectByEmail(string email)
        {
            // Retrieve a specific user based on email address and also include user account information
            return await _context.User.Include(a => a.Account).FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}