using System;
using IronWorkoutTracker.Domain.Entities;

namespace IronWorkoutTracker.Application.IRepositories;

public interface IWorkoutProgramRepository
{   
    IQueryable<WorkoutProgram> GetQuery();
    Task<List<WorkoutProgram>> GetAllAsync();
    Task<WorkoutProgram?> GetByIdAsync(int id);
    Task AddAsync(WorkoutProgram program);
    Task UpdateAsync(WorkoutProgram program);
    Task DeleteAsync(int id);
}
