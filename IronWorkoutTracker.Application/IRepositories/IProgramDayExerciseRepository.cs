using System.Collections.Generic;
using System.Threading.Tasks;
using IronWorkoutTracker.Domain.Entities;

namespace IronWorkoutTracker.Application.IRepositories
{
    public interface IProgramDayExerciseRepository
    {
        Task<List<ProgramDayExercise>> GetByProgramDayIdAsync(int programDayId);
        Task<ProgramDayExercise?> GetByIdAsync(int id);
        Task AddAsync(ProgramDayExercise entity);
        Task UpdateAsync(ProgramDayExercise entity);
        Task DeleteAsync(int id);
    }
}
