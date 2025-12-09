using IronWorkoutTracker.Application.IRepositories;
using IronWorkoutTracker.Domain.Entities;
using IronWorkoutTracker.Infrastructure.Data.Context;

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
    }
}
