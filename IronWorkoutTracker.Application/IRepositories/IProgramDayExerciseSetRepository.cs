using System.Collections.Generic;
using System.Threading.Tasks;
using IronWorkoutTracker.Domain.Entities;

namespace IronWorkoutTracker.Application.IRepositories
{
    public interface IProgramDayExerciseSetRepository
    {
        Task<List<ProgramDayExerciseSet>> GetByProgramDayExerciseIdAsync(int programDayExerciseId);
        Task<ProgramDayExerciseSet?> GetByIdAsync(int id);
        Task AddAsync(ProgramDayExerciseSet entity);
        Task UpdateAsync(ProgramDayExerciseSet entity);
        Task DeleteAsync(int id);
    }
}
