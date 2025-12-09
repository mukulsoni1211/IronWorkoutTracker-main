using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IronWorkoutTracker.Infrastructure.Data.Context;
using IronWorkoutTracker.Domain.Entities;
using IronWorkoutTracker.Application.IRepositories;

namespace IronWorkoutTracker.Infrastructure.Repositories
{
    public class ProgramDayRepository : IProgramDayRepository
    {
        private readonly IronDbContext _db;

        public ProgramDayRepository(IronDbContext db)
        {
            _db = db;
        }

        public Task<List<ProgramDay>> GetByProgramIdAsync(int workoutProgramId) =>
            _db.ProgramDays
               .Where(d => d.WorkoutProgramId == workoutProgramId)
               .OrderBy(d => d.Order)
               .ToListAsync();

        public Task<ProgramDay?> GetByIdAsync(int id) =>
            _db.ProgramDays.FirstOrDefaultAsync(d => d.ProgramDayId == id);

        public async Task AddAsync(ProgramDay entity)
        {
            // Set next order for this program
            var maxOrder = await _db.ProgramDays
                .Where(d => d.WorkoutProgramId == entity.WorkoutProgramId)
                .Select(d => (int?)d.Order)
                .MaxAsync() ?? -1;

            entity.Order = maxOrder + 1;
            entity.CreatedAt = DateTime.UtcNow;

            _db.ProgramDays.Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProgramDay entity)
        {
            _db.ProgramDays.Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return;
            
            _db.ProgramDays.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}
