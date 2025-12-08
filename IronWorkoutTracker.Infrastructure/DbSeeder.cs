using IronWorkoutTracker.Infrastructure.Data.Context;
using IronWorkoutTracker.Domain.Entities;

public static class DbSeeder
{
    public static void Seed(IronDbContext context)
    {
        if (!context.Users.Any())
        {
            context.Users.AddRange(
                new User { Name = "Alice", Email = "a@example.com", Password = "password123", Role = "Admin" },
                new User { Name = "Bob", Email = "b@example.com", Password = "password123", Role = "User" }
            );
            context.SaveChanges();
        }
    }
}
