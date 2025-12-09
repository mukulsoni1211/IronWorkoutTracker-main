using System;
using IronWorkoutTracker.Domain.Entities;

namespace IronWorkoutTracker.Application.IRepositories;

public interface IUserRepository
{
    Task<User> GetUserByEmailAndPassword(string email , string password);
}
