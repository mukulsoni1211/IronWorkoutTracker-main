using System;
using IronWorkoutTracker.Domain.Entities;

namespace IronWorkoutTracker.Application.IRepositories
{
    public interface IExerciseRepository
    {
        Task AddAsync(Exercise entity);
    }
}