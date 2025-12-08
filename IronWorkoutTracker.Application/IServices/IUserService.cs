using System;
using IronWorkoutTracker.Application.ServiceDtos;

namespace IronWorkoutTracker.Application.IServices;

public interface IUserService
{
    Task<UserServiceModel> GetUserByEmailAndPassword(string email, string password);
}
