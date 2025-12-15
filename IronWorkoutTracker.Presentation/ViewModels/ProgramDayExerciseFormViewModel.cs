using System.Collections.Generic;
using IronWorkoutTracker.Domain.Entities;

namespace IronWorkoutTracker.Presentation.ViewModels
{
    public class ProgramDayExerciseFormViewModel
    {
        public ProgramDayExercise ProgramDayExercise { get; set; } = null!;
        public List<Exercise> Exercises { get; set; } = new();
    }
}
