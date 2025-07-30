﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BookingSystem.DTOs;
using BookingSystem.Entities;

namespace BookingSystem.Repository
{
    public interface IUserRepository
    {
        Task<User> ValidateUser(string email, string password);
        Task AddUsers(User newuser);
        Task<List<User>> GetAllUsers();
        Task<List<UserBasicDTO>> GetBasicUserInfo();
        Task<List<User>> GetUsersByName(string UserName);
        Task UpdateUser(long UserId, UserDTO newuser);
        Task DeleteUser(long userId);
        Task<User> GetUserById(long id);
        Task<int> GetTotalUsers();
        Task<List<User>> GetUsersByRole(string role);
    }
}
