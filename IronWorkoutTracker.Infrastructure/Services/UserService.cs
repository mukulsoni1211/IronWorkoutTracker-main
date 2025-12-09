using System;
using System.Diagnostics;
using IronWorkoutTracker.Application.IRepositories;
using IronWorkoutTracker.Application.IServices;
using IronWorkoutTracker.Application.ServiceDtos;
using IronWorkoutTracker.Infrastructure.InfraUtilities;

namespace IronWorkoutTracker.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<UserServiceModel> GetUserByEmailAndPassword(string email, string password)
    {
        // var passwordHash =  PasswordHelper.HashPassword(password); // Will later use hashed password for comparison
        var domainuser = await _userRepository.GetUserByEmailAndPassword(email, password);

        if (domainuser != null)
        {
            return new UserServiceModel
            {
                UserId = domainuser.Id,
                Email = domainuser.Email,
                UserName = domainuser.Name ?? string.Empty,
                Role = domainuser.Role
            };
        }

        return null;
    }
}
