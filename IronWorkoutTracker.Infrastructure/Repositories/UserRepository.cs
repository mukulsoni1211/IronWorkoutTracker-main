using System;
using IronWorkoutTracker.Application.IRepositories;
using IronWorkoutTracker.Domain.Entities;


namespace IronWorkoutTracker.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    public UserRepository(/*Inject Db Context Here*/ )
    {
        
    }

    public async Task<User> GetUserByEmailAndPassword(string email , string HashedPassword)
    {
        //EF method , _dbcontext.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == HashedPassword);
        if(email=="abc@example.com" && HashedPassword == "dsrfgkdnfognoq")
        {
            return new User
            {
                Id=    1,
                Email = email,
                Name = "John Doe",
                Password = "dsrfgkdnfognoq",
                Role = "ADMIN"
            };
        }

        if(email=="pqr@example.com" && HashedPassword == "dsrfgkdnfognoq")
        {
            return new User
            {
                Id=    1,
                Email = email,
                Name = "John Doe",
                Password = "dsrfgkdnfognoq",
                Role = "USER"
            };
        }

        return  null;
    }


}
