using Microsoft.EntityFrameworkCore;
using IronWorkoutTracker.Infrastructure.Data.Context;
using IronWorkoutTracker.Domain.Entities;
using IronWorkoutTracker.Application.IRepositories;

public class WorkoutProgramRepository : IWorkoutProgramRepository
{
    private readonly IronDbContext _db;

    public WorkoutProgramRepository(IronDbContext db)
    {
        _db = db;
    }

    public IQueryable<WorkoutProgram> GetQuery()
    {
        return _db.WorkoutPrograms.AsQueryable();
    }

    public Task<List<WorkoutProgram>> GetAllAsync()
        => _db.WorkoutPrograms.Include(p => p.CreatedBy).Include(p => p.UserPrograms).ToListAsync();

    public Task<WorkoutProgram?> GetByIdAsync(int id)
        => _db.WorkoutPrograms.Include(p => p.CreatedBy).FirstOrDefaultAsync(p => p.WorkoutProgramId == id);

    public async Task AddAsync(WorkoutProgram program)
    {
        _db.WorkoutPrograms.Add(program);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(WorkoutProgram program)
    {
        _db.WorkoutPrograms.Update(program);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity is null) return;
        _db.WorkoutPrograms.Remove(entity);
        await _db.SaveChangesAsync();
    }
}
