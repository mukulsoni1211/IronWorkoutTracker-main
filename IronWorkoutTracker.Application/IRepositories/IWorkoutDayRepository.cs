using System;
using IronWorkoutTracker.Domain.Entities;

namespace IronWorkoutTracker.Application.IRepositories;
public interface IWorkoutDayRepository
{
    Task AddAsync(WorkoutDay workoutDay);
}
