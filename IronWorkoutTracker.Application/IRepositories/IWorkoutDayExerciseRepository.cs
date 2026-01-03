using IronWorkoutTracker.Domain.Entities;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IronWorkoutTracker.Application.IRepositories;

public interface IWorkoutDayExerciseRepository
{
    IQueryable<WorkoutDayExercise> GetQuery();
    Task<List<WorkoutDayExercise>> GetAllAsync();
    Task<WorkoutDayExercise> GetByIdAsync(int id);
    Task<WorkoutDayExercise> AddAsync(WorkoutDayExercise entity);
    Task UpdateAsync(WorkoutDayExercise entity);
    Task DeleteAsync(int id);
    
    // Specific methods for workout tracking
    Task<List<WorkoutDayExercise>> GetByWorkoutDayIdAsync(int workoutDayId);
    Task<WorkoutDayExercise> GetByIdWithWorkoutDayAsync(int id);
}
