using H5_Webshop.Database.Entities;
using H6_ChicBotique.Database;
using Microsoft.EntityFrameworkCore;

namespace H6_ChicBotique.Repositories
{
    //Creating Interface of IUserRepository
    public interface IUserRepository //Interface which declares only the methods
    {
        Task<List<User>> SelectAll();     //For getting all User Details 
        Task<User> SelectByEmail(string email); //For getting User by specific unique Email
        Task<User> SelectById(int userId); //For getting User by specific Id
        Task<User> Update(int userId, User user); //For Updating the User entity


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

        // Implementation of GetAll method
        public async Task<List<User>> SelectAll()
        {
            // Retrieve all users from the database
            return await _context.User.ToListAsync();
        }

        // Implementation of GetById method
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

        //Using this method existing user info can be updated by giving specific userId
        public async Task<User> Update(int user_Id, User user)
        {
            User updateUser = await _context.User
                .FirstOrDefaultAsync(a => a.Id == user_Id);

            if (updateUser != null)
            {
                updateUser.Email = user.Email;
                updateUser.FirstName = user.FirstName;

                updateUser.LastName = user.LastName;

                updateUser.Role = user.Role;

                // _context.Entry(updateUser).CurrentValues.SetValues(user);
                await _context.SaveChangesAsync();
            }
            return updateUser;
        }
    }
}