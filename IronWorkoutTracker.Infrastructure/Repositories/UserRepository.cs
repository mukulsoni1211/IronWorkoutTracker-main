using System;
using IronWorkoutTracker.Application.IRepositories;
using IronWorkoutTracker.Domain.Entities;
using IronWorkoutTracker.Infrastructure.Data.Context;
using System.Threading.Tasks;
namespace IronWorkoutTracker.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly IronDbContext _dbContext;

    public UserRepository(IronDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> GetUserByEmailAndPassword(string email , string password)
    {

        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user != null && password == user.Password)
        {
            return user;
        }

        return  null;
    }
}
