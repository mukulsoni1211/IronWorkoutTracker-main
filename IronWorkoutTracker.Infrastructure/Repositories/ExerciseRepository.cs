using Microsoft.EntityFrameworkCore;
using IronWorkoutTracker.Infrastructure.Data.Context;
using IronWorkoutTracker.Domain.Entities;
using IronWorkoutTracker.Application.IRepositories;

public class ExerciseRepository : IExerciseRepository
{
    private readonly IronDbContext _db;

    public ExerciseRepository(IronDbContext db)
    {
        _db = db;
    }

    public Task<List<Exercise>> GetAllAsync()
        => _db.Exercises.ToListAsync();

    public Task<Exercise?> GetByIdAsync(int id)
        => _db.Exercises.FirstOrDefaultAsync(p => p.ExerciseId == id);

    public async Task AddAsync(Exercise exercise)
    {
        _db.Exercises.Add(exercise);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Exercise exercise)
    {
        _db.Exercises.Update(exercise);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity is null) return;
        _db.Exercises.Remove(entity);
        await _db.SaveChangesAsync();
    }
}
