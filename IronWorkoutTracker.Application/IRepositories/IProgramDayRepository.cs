using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IronWorkoutTracker.Domain.Entities;

namespace IronWorkoutTracker.Application.IRepositories
{
    public interface IProgramDayRepository
    {
        Task<List<ProgramDay>> GetByProgramIdAsync(int workoutProgramId);
        Task<ProgramDay?> GetByIdAsync(int id);
        Task AddAsync(ProgramDay entity);
        Task UpdateAsync(ProgramDay entity);
        Task DeleteAsync(int id);
    }
}
