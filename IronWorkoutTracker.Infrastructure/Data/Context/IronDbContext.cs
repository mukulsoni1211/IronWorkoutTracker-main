// File: IronWorkoutTracker.Infrastructure/Data/Context/IronDbContext.cs
using System;
using Microsoft.EntityFrameworkCore;
using IronWorkoutTracker.Domain.Entities;

namespace IronWorkoutTracker.Infrastructure.Data.Context
{
    public class IronDbContext : DbContext
    {	
        public IronDbContext(DbContextOptions<IronDbContext> options) : base(options)
        {
        }

        // DbSets
        public DbSet<User> Users { get; set; }
    }
}