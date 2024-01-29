﻿using Microsoft.AspNetCore.Mvc;
using H6_ChicBotique.Authorization;
using H6_ChicBotique.Database.Entities;
using H6_ChicBotique.DTOs;
using H6_ChicBotique.Repositories;
using H5_Webshop.Database.Entities;

//Get, insert,delete,update

namespace H6_ChicBotique.Services
{
    // Interface definition for user service
    public interface IUserService
    {
        Task<List<UserResponse>> GetAll(); // Method to retrieve all users as UserResponse objects
        Task<UserResponse> GetById(int UserId); // Method to retrieve a user by ID as a UserResponse object
        Task<UserResponse> GetIdByEmail(string email); // Method to retrieve a user by email as a UserResponse object
        Task<LoginResponse> Authenticate(LoginRequest login); // Method to authenticate a user based on the provided login credentials.
        Task<UserResponse> Update(int UserId, UserRequest updateUser); 
        Task<bool> UpdatePassword(PasswordEntityRequest passwordEntityRequest);  
    }

    // Implementation of IUserService interface in UserService class
    public class UserService : IUserService
    {
        // creating instances of Interfaces
        private readonly IUserRepository _userRepository;
        private readonly IPasswordEntityRepository _PasswordEntityRepository;
        private readonly IHomeAddressRepository _HomeAddressRepository;
        private readonly IAccountInfoRepository _accountRepository;
        private readonly IJwtUtils _jwtUtils;


        // Constructor with dependency injection for IUserRepository
        public UserService(IUserRepository userRepository, IPasswordEntityRepository PasswordEntityRepository, IHomeAddressRepository homeAddressRepository, IAccountInfoRepository accountRepository, IJwtUtils jwtUtils)
        {
            _userRepository = userRepository;
        }

        // Implementation of GetAll method
        public async Task<List<UserResponse>> GetAll()
        {
            // Retrieve all users from the repository
            List<User> users = await _userRepository.SelectAll();

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
            User User = await _userRepository.SelectById(UserId);

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
            User User = await _userRepository.SelectByEmail(email);

            // If the user is not null, map the user to a UserResponse object
            if (User != null)
            {
                return MapUserToUserResponse(User);
            }

            return null; // Return null if the user is not found
        }

     
        public async Task<LoginResponse> Authenticate(LoginRequest login)
        {
            // Retrieve user information from the UserRepository based on the provided email.
            User user = await _userRepository.SelectByEmail(login.Email);

            // Check if the user with the provided email exists.
            if (user == null)
            {
                return null; // User not found.
            }

            // Retrieve the stored password information (including salt) from the PasswordEntityRepository.
            PasswordEntity pwd = await _PasswordEntityRepository.SelectByUserId(user.Id);

            // Validate the provided password against the stored hashed password.
            if (Helpers.PasswordHelpers.HashPassword($"{login.Password}{pwd.Salt}") == pwd.Password)
            {
                // If the passwords match, create a LoginResponse with user information and a JWT token.
                LoginResponse response = new LoginResponse
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    Role = user.Role,
                    Token = _jwtUtils.GenerateJwtToken(user)
                };
                return response; // Return the LoginResponse upon successful authentication.
            }

            return null; // Return null if the provided password doesn't match the stored hashed password.
        }

        // update function for password
        public async Task<bool> UpdatePassword(PasswordEntityRequest passwordEntityRequest)
        {
            var salt = DateTime.Now.ToString();  //making salt
            var HashedPW = Helpers.PasswordHelpers.HashPassword($"{passwordEntityRequest.Password}{salt}");///hashing the requested password with salt
            PasswordEntity pwd = await _PasswordEntityRepository.SelectByUserId(passwordEntityRequest.UserId); ///getting the user by userId
            //putting the new hashed password, salt, date in the object
            pwd.Salt = salt;
            pwd.Password = HashedPW;
            pwd.LastUpdated = DateTime.UtcNow;
            // updating the password in the database
            await _PasswordEntityRepository.UpdatePassword(pwd);

            return true;
        }
        // Updates user information for the specified UserId.
        public async Task<UserResponse> Update(int UserId, UserRequest updateUser)
        {
            // Create a new User object with updated information from UserRequest.
            User user = new User
            {
                FirstName = updateUser.FirstName,
                LastName = updateUser.LastName,
                Email = updateUser.Email
            };

            // Perform the update operation in the UserRepository.
            user = await _userRepository.Update(UserId, user);

            // Return a UserResponse with the updated user information, or null if the update was unsuccessful.
            return user == null ? null : new UserResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role
            };
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
                    Id = user.AccountInfo.Id,
                };
            }

            return response; // Return the mapped UserResponse object
        }

       
    }
}




