using System;
using IronWorkoutTracker.Domain.Entities;

namespace IronWorkoutTracker.Application.IRepositories
{
    public interface IUserProgramRepository
    {   
        Task<UserProgram?> GetByUserAndProgramAsync(int userId, int workoutProgramId);
        Task AddAsync(UserProgram entity);
        Task DeleteByUserAndProgramAsync(int userId, int workoutProgramId);
        Task<UserProgram?> GetByIdAsync(int id);
        Task UpdateAsync(UserProgram userProgram);
    }
}
