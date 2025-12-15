using System.Collections.Generic;
using IronWorkoutTracker.Domain.Entities;

namespace IronWorkoutTracker.Presentation.ViewModels
{
    public class ProgramDayExerciseIndexViewModel
    {
        public ProgramDay ProgramDay { get; set; } = null!;
        public List<ProgramDayExercise> Exercises { get; set; } = new();
    }
}
