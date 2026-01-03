using System;
using IronWorkoutTracker.Domain.Entities;

namespace IronWorkoutTracker.Application.IRepositories;
public interface IWorkoutDayExerciseSetRepository
{
    Task<WorkoutDayExerciseSet> AddAsync(WorkoutDayExerciseSet entity);
    Task<WorkoutDayExerciseSet> GetByIdAsync(int id);
    Task<List<WorkoutDayExerciseSet>> GetByWorkoutDayExerciseIdAsync(int workoutDayExerciseId);
    Task UpdateAsync(WorkoutDayExerciseSet entity);
    Task DeleteAsync(int id);
}
