using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IronWorkoutTracker.Infrastructure.Data.Context;
using IronWorkoutTracker.Domain.Entities;
using IronWorkoutTracker.Application.IRepositories;

namespace IronWorkoutTracker.Infrastructure.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly IronDbContext _db;

        public ExerciseRepository(IronDbContext db)
        {
            _db = db;
        }

        public Task<List<Exercise>> GetAllAsync() =>
            _db.Exercises
               .OrderByDescending(e => e.CreatedDate)
               .ToListAsync();

        public Task<Exercise?> GetByIdAsync(int id) =>
            _db.Exercises.FirstOrDefaultAsync(e => e.ExerciseId == id);

        public async Task AddAsync(Exercise entity)
        {
            entity.CreatedDate = DateTime.UtcNow;
            _db.Exercises.Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Exercise entity)
        {
            _db.Exercises.Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return;

            _db.Exercises.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}
