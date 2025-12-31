using System;
using IronWorkoutTracker.Application.IRepositories;
using IronWorkoutTracker.Domain.Entities;
using IronWorkoutTracker.Infrastructure.Data.Context;
using System.Threading.Tasks;
namespace IronWorkoutTracker.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

public class WorkoutDayExerciseSetRepository : IWorkoutDayExerciseSetRepository
{
    private readonly IronDbContext _db;

    public WorkoutDayExerciseSetRepository(IronDbContext db)
    {
        _db = db;
    }

    public async Task<WorkoutDayExerciseSet> AddAsync(WorkoutDayExerciseSet entity)
    {
        await _db.WorkoutDayExerciseSets.AddAsync(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<WorkoutDayExerciseSet> GetByIdAsync(int id)
    {
        return await _db.WorkoutDayExerciseSets.FirstOrDefaultAsync(s => s.WorkoutDayExerciseSetId == id);
    }

    public async Task UpdateAsync(WorkoutDayExerciseSet entity)
    {
        _db.WorkoutDayExerciseSets.Update(entity);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var set = await GetByIdAsync(id);
        if (set != null)
        {
            _db.WorkoutDayExerciseSets.Remove(set);
            await _db.SaveChangesAsync();
        }
    }
}