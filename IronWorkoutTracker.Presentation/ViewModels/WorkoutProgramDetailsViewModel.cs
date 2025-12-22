using IronWorkoutTracker.Domain.Entities;

namespace IronWorkoutTracker.Presentation.ViewModels
{
    public class WorkoutProgramDetailsViewModel
    {
        public WorkoutProgram Program { get; set; } = null!;
        public List<ProgramDay> Days { get; set; } = new();
        public UserProgram? UserProgram { get; set; }
    }
}
