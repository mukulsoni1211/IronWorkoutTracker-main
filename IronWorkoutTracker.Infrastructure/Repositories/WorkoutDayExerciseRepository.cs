using IronWorkoutTracker.Application.IRepositories;
using IronWorkoutTracker.Domain.Entities;
using IronWorkoutTracker.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronWorkoutTracker.Infrastructure.Repositories;

public class WorkoutDayExerciseRepository : IWorkoutDayExerciseRepository
{
    private readonly IronDbContext _db;

    public WorkoutDayExerciseRepository(IronDbContext db)
    {
        _db = db;
    }

    public IQueryable<WorkoutDayExercise> GetQuery()
    {
        return _db.WorkoutDayExercises.AsQueryable();
    }

    public async Task<List<WorkoutDayExercise>> GetAllAsync()
    {
        return await _db.WorkoutDayExercises.ToListAsync();
    }

    public async Task<WorkoutDayExercise> GetByIdAsync(int id)
    {
        return await _db.WorkoutDayExercises.FirstOrDefaultAsync(e => e.WorkoutDayExerciseId == id);
    }

    public async Task<WorkoutDayExercise> GetByIdWithWorkoutDayAsync(int id)
    {
        return await _db.WorkoutDayExercises
            .Include(e => e.WorkoutDay)
            .FirstOrDefaultAsync(e => e.WorkoutDayExerciseId == id);
    }

    public async Task<List<WorkoutDayExercise>> GetByWorkoutDayIdAsync(int workoutDayId)
    {
        return await _db.WorkoutDayExercises
            .Where(e => e.WorkoutDayId == workoutDayId)
            .ToListAsync();
    }

    public async Task<WorkoutDayExercise> AddAsync(WorkoutDayExercise entity)
    {
        await _db.WorkoutDayExercises.AddAsync(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(WorkoutDayExercise entity)
    {
        _db.WorkoutDayExercises.Update(entity);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _db.WorkoutDayExercises.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}
