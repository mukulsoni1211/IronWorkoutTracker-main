using System;
using IronWorkoutTracker.Domain.Entities;

namespace IronWorkoutTracker.Application.IRepositories
{
    public interface IUserProgramRepository
    {
        Task AddAsync(UserProgram entity);
        Task DeleteByUserAndProgramAsync(int userId, int workoutProgramId);
    }
}
