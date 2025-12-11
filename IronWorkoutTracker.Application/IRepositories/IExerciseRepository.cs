using System.Collections.Generic;
using System.Threading.Tasks;
using IronWorkoutTracker.Domain.Entities;

namespace IronWorkoutTracker.Application.IRepositories
{
    public interface IExerciseRepository
    {
        Task<List<Exercise>> GetAllAsync();
        Task<Exercise?> GetByIdAsync(int id);
        Task AddAsync(Exercise entity);
        Task UpdateAsync(Exercise entity);
        Task DeleteAsync(int id);
    }
}
