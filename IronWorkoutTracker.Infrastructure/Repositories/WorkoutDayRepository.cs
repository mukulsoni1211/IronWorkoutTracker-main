using System;
using IronWorkoutTracker.Application.IRepositories;
using IronWorkoutTracker.Domain.Entities;
using IronWorkoutTracker.Infrastructure.Data.Context;
using System.Threading.Tasks;
namespace IronWorkoutTracker.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

public class WorkoutDayRepository : IWorkoutDayRepository
{
    private readonly IronDbContext _db;

    public WorkoutDayRepository(IronDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(WorkoutDay workoutDay)
    {
        await _db.WorkoutDays.AddAsync(workoutDay);
        await _db.SaveChangesAsync();
    }
}
