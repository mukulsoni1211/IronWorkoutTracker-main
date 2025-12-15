using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IronWorkoutTracker.Infrastructure.Data.Context;
using IronWorkoutTracker.Domain.Entities;
using IronWorkoutTracker.Application.IRepositories;

namespace IronWorkoutTracker.Infrastructure.Repositories
{
    public class ProgramDayExerciseRepository : IProgramDayExerciseRepository
    {
        private readonly IronDbContext _db;

        public ProgramDayExerciseRepository(IronDbContext db)
        {
            _db = db;
        }

        public Task<List<ProgramDayExercise>> GetByProgramDayIdAsync(int programDayId) =>
            _db.ProgramDayExercises
               .Where(pde => pde.ProgramDayId == programDayId)
               .Include(pde => pde.Exercise)
               .OrderBy(pde => pde.Order)
               .ToListAsync();

        public Task<ProgramDayExercise?> GetByIdAsync(int id) =>
            _db.ProgramDayExercises
               .Include(pde => pde.Exercise)
               .FirstOrDefaultAsync(pde => pde.ProgramDayExerciseId == id);

        public async Task AddAsync(ProgramDayExercise entity)
        {
            // Set next order for this program day
            var maxOrder = await _db.ProgramDayExercises
                .Where(pde => pde.ProgramDayId == entity.ProgramDayId)
                .Select(pde => (int?)pde.Order)
                .MaxAsync() ?? -1;

            entity.Order = maxOrder + 1;
            entity.CreatedAt = DateTime.UtcNow;

            _db.ProgramDayExercises.Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProgramDayExercise entity)
        {
            _db.ProgramDayExercises.Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return;

            _db.ProgramDayExercises.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}
