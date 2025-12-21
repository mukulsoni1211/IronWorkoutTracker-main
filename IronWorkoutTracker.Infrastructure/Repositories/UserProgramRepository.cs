using IronWorkoutTracker.Application.IRepositories;
using IronWorkoutTracker.Domain.Entities;
using IronWorkoutTracker.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace IronWorkoutTracker.Infrastructure.Repositories
{
    public class UserProgramRepository : IUserProgramRepository
    {
        private readonly IronDbContext _db;

        public UserProgramRepository(IronDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(UserProgram entity)
        {
            _db.UserPrograms.Add(entity);
            await _db.SaveChangesAsync();
        }

    
        public async Task DeleteByUserAndProgramAsync(int userId, int workoutProgramId)
        {
            var userPrograms =  _db.UserPrograms
                .Where(up => up.UserId == userId && up.WorkoutProgramId == workoutProgramId).ToList();

            _db.UserPrograms.RemoveRange(userPrograms);
            _db.SaveChangesAsync();
        }

    }
}
