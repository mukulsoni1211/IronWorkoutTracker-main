using System;

namespace IronWorkoutTracker.Domain.Entities;

public class ProgramDayExercise
{	
	public int ProgramDayExerciseId { get; set; }
    public int ProgramDayId { get; set; }

    public int ExerciseId { get; set; }

    public string? TrainingStyle { get; set; }
    public string? Note { get; set; }
    public DateTime? CreatedAt { get; set; }
    public int? Order { get; set; }

    public ProgramDay? ProgramDay { get; set; }
    public Exercise? Exercise { get; set; }
}
