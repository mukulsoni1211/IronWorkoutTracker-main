using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IronWorkoutTracker.Infrastructure.Data.Context;
using IronWorkoutTracker.Domain.Entities;
using IronWorkoutTracker.Application.IRepositories;

namespace IronWorkoutTracker.Infrastructure.Repositories
{
    public class ProgramDayExerciseSetRepository : IProgramDayExerciseSetRepository
    {
        private readonly IronDbContext _db;

        public ProgramDayExerciseSetRepository(IronDbContext db)
        {
            _db = db;
        }

        public Task<List<ProgramDayExerciseSet>> GetByProgramDayExerciseIdAsync(int programDayExerciseId) =>
            _db.ProgramDayExerciseSets
               .Where(pdes => pdes.ProgramDayExerciseId == programDayExerciseId)
               .OrderBy(pdes => pdes.Order)
               .ToListAsync();

        public Task<ProgramDayExerciseSet?> GetByIdAsync(int id) =>
            _db.ProgramDayExerciseSets.FirstOrDefaultAsync(pdes => pdes.ProgramDayExerciseSetId == id);

        public async Task AddAsync(ProgramDayExerciseSet entity)
        {
            // Set next order for this exercise
            var maxOrder = await _db.ProgramDayExerciseSets
                .Where(pdes => pdes.ProgramDayExerciseId == entity.ProgramDayExerciseId)
                .Select(pdes => (int?)pdes.Order)
                .MaxAsync() ?? -1;

            entity.Order = maxOrder + 1;
            entity.CreatedAt = DateTime.UtcNow;

            _db.ProgramDayExerciseSets.Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProgramDayExerciseSet entity)
        {
            _db.ProgramDayExerciseSets.Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return;

            _db.ProgramDayExerciseSets.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}
